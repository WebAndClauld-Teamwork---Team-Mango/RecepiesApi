(function () {
    require.config({
        paths: {
            //libs
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'handlebars':'libs/handlebars',
            'requestModule' : 'libs/requestModule',            
            //controllers
            'mainController' : 'controllers/mainController',
            'recipesController':'controllers/recipesController',
            //configs
            'constants':'configs/constants',
            //persisters
            'recepiesPersister':'persisters/recepiesPersister',
            'tagsPersister':'persisters/tagsPersister',
            'usersPersister':'persisters/usersPersister',
            //entities
            'recipe':'entities/recipe',
            //helpers
            'restHelper':'helpers/restHelper',
            'requestModule':'helpers/requestModule',
            'fileHelper':'helpers/fileHelper'
        },
        shim: {
            
            'libs/underscore': {
                exports: '_'
            }
        }
    });

    require(['recepiesPersister',
        'usersPersister',
        'recipesController',
        'sammy',
        'recipe','libs/underscore',
        'constants'], 
        function (RecepiesPersister,UsersPersister,RecipesController,sammy,Recipe) {
        //enable cors for app
        $.support.cors=true;
        
        //main content container
        var contentSelector='#content-box';

        function loadUsers(onSuccess,onFail){
            var usersPersister=new UsersPersister(USERS_ENDPOINT);
                usersPersister.loadAllUsers(function(users){ 
                    if(onSuccess!==undefined)
                    {
                        onSuccess(users);
                    }                   
                },function(errors){
                    if(onFail!==undefined)
                    {
                        onFail(errors);
                    }
                });
        }

        function showRecipesPage(filterObj)
        {
            var recepiesController=new RecipesController(RECEPIES_ENDPOINT);                
            //
            loadUsers(function(users){
                //                    
                //console.log(data);                    
                var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                //
                recepiesController.generateUsersList({'users':users});
                //
                recepiesPersister.loadAllRecepies(function(results){
                    var readyResults=results;
                    //
                    if(filterObj!==undefined)
                    {
                        //user filter
                        if(filterObj.userId!==undefined){
                            readyResults=_.filter(results,function(result){return(result.UserInfoId+''===''+filterObj.userId)});
                        }
                        //search filter
                        if(filterObj.filterQuery!==undefined){
                            readyResults=_.filter(results,function(result){     
                            var queryStr=filterObj.filterQuery;
                            var nameCond=result.Name.indexOf(queryStr)>-1;
                            var userCond=result.Nickname.indexOf(queryStr)>-1;
                            var descCond=result.Description.indexOf(queryStr)>-1;
                            return (nameCond || userCond || descCond);
                        });   
                        }

                    }         
                    //
                    var data={'recepies':readyResults};           
                    recepiesController.generateThumbnails(data);        
                });
            }); 
        }

        var app = sammy(contentSelector, function() {

            //recepies page
            this.get("#/recipes", function() { 
                showRecipesPage();
            });

            //users filter
            this.get("#/recipes/user/:id",function(){
                var userId=this.params['id'];
                showRecipesPage({'userId':userId});
            });

            //search filter
            this.get("#/recipes/filter/:query",function(){
                var query=this.params['query'];
                showRecipesPage({'filterQuery':query});                
            });

            this.get("#/recipe/:id",function(){
                //get recipe id
                var recipeId=this.params['id'];

                function loadRecipePage(recipeObj,users)
                {
                    $(contentSelector).load(RECIPE_PAGE,function(){                                        
                        var recipesController=new RecipesController(RECEPIES_ENDPOINT);

                        recipesController.generateUsersList({'users':users});

                        recipesController.generateSingleRecipe(recipeObj);
                    });
                }

                var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                //
                loadUsers(function(users){

                    //
                    recepiesPersister.loadRecipe(recipeId,function(data){                               
                        loadRecipePage(data,users);
                    },function(error){
                        //report errors here
                        console.log(error);
                    });                
                });
            });

            //new recipe page
            this.get("#/recipes/new", function() {                            
                $(contentSelector).load(INSERT_RECIPE_PAGE,function(){                                        
                    //get recipe from rest by id                    
                });       
            });

            //about page
            this.get("#/about",function(){
                alert("about!!!");
                //              
            });                                            
        });
        //run application
        app.run("#/recipes");     

        //events
        $(document).ready(function(){
            $('#search-submit').click(function(){
                var queryStr=$('#tb-search').val();
                if((queryStr+'').length>0)
                {
                    window.location.href='#/recipes/filter/'+queryStr;
                }
                else
                {
                    window.location.href='#/recipes';
                }
            }); 
        });    
    });
}());