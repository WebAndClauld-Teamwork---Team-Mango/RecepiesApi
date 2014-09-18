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
			
			generateSingleRecipe:function(data)
			{
				var recipeListContainer = $("#content-box");
				var generateRecipe = Handlebars.compile($('#recipe-template').html());
				// empty the container
				while (recipeListContainer.firstChild) {
					recipeListContainer.removeChild(recipeListContainer.firstChild);
				}
				
				recipeListContainer.html(generateRecipe(data));
			}
        };

        return RecipesController;
    }());
    return RecipesController;
});