define(['jquery', 'requestModule','fileHelper','handlebars','constants'], function ($, requestModule,FileHelper) {
    var RecipesController = (function () {
        function RecipesController(rootUrl) {
            this.rootUrl = rootUrl;
        }

        RecipesController.prototype = {
            
            generateThumbnails:function(data)
            {				                                
                function fillContentBoxWithRecipes(template)
                {
                    var recipesListContainer = $("#content-box");
                    var generateRecipes = Handlebars.compile(template);
                    // empty the container
                    recipesListContainer.children().remove();
                    //fill container with recepies
                    recipesListContainer.html(generateRecipes(data));
                }

                //create file helper
                var fileHelper=new FileHelper();
                //read recipes template and fill recipes
                fileHelper.loadTextFile(RECEPIES_TEMPLATE,function(result){
                    fillContentBoxWithRecipes(result);
                },function(error){
                    //report error here...
                    console.log(error);
                });                
                //
            },

			generateBreadCrums:function(breadcrums)
			{
				function fillBreadCrumsContainer(template){
					var breadcrumsBox=$('#breadcrumbs-box');
					//
					var generateBreadCrums=Handlebars.compile(template);
					breadcrumsBox.children().remove();
					breadcrumsBox.html(generateBreadCrums(breadcrums));
				}
				
				var fileHelper=new FileHelper();
				fileHelper.loadTextFile(BREADCRUMS_TEMPLATE,function(result){
					fillBreadCrumsContainer(result);
				},function(errors){
					//report errors here
					console.log(errors);
				});
			},
			
            generateSingleUser:function(data){
                //
                function fillContentBoxWithUserInfo(template){
                    var userInfoContainer = $("#content-box");
                    userInfoContainer.children().remove();
                    //                    
                    var generateUserData = Handlebars.compile(template);
                    //
                    userInfoContainer.html(generateUserData(data));
                }

                var fileHelper=new FileHelper();
                fileHelper.loadTextFile(USER_PAGE_TEMPLATE,function(result){
                    fillContentBoxWithUserInfo(result);
                },function(error){
                    //report errors here...
                    console.log(error);
                });
            },

            generateUsersList: function(data){
                
                function fillUsersList(template)
                {
                    var usersListContainer = $("#users-list");
                    var generateUsersList= Handlebars.compile(template);
                    //   
                    usersListContainer.children().remove();
                    //
                    usersListContainer.html(generateUsersList(data));
                }
                //
                //create file helper
                var fileHelper=new FileHelper();
                //read tags list template
                fileHelper.loadTextFile(USERS_LIST_TEMPLATE,function(result){
                    fillUsersList(result);
                },function(error){
                    //report error here...
                    console.log(error);
                });                
            },

            loadUI: function (selector) {                

                //load recipes here...
                console.log("here!!!");
                this.attachEventHandlers(selector); // !! Call all Events
            },
            
            attachEventHandlers: function (selector) {
                var wrapper = $(selector);
                var self = this;
                /*wrapper.on('click', '#btn-login', function () {

                });*/
            },
			
			generateSingleRecipe:function(recipeObj)
			{
                function fillPageWithRecipeData(template)
                {
                    var recipeContainer = $("#content-box");
                    var generateRecipe = Handlebars.compile(template);
                    // empty the container                   
                    recipeContainer.children().remove();
                    //fill the container
                    recipeContainer.html(generateRecipe({'recipe':recipeObj}));
                }
                //load template
                var fileHelper=new FileHelper();
                //read recipes template and fill recipes
                fileHelper.loadTextFile(RECEPIE_TEMPLATE,function(result){
                    fillPageWithRecipeData(result);
                });
			}
        };

        return RecipesController;
    }());
    return RecipesController;
});