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
        'tagsPersister',
        'recipesController',
        'sammy',
        'recipe',
        'constants'], function (RecepiesPersister,TagsPersister,RecipesController,sammy,Recipe) {
        //enable cors for app
        $.support.cors=true;
        
        //main content container
        var contentSelector='#content-box';

        function loadTags(onSuccess,onFail){
            var tagsPersister=new TagsPersister(TAGS_ENDPOINT);
                tagsPersister.loadAllTags(function(tags){ 
                    if(onSuccess!==undefined)
                    {
                        onSuccess(tags);
                    }                   
                },function(errors){
                    if(onFail!==undefined)
                    {
                        onFail(errors);
                    }
                });
        }

        var app = sammy(contentSelector, function() {

            //recepies page
            this.get("#/recipes", function() { 
                var recepiesController=new RecipesController(RECEPIES_ENDPOINT);                
                //
                loadTags(function(tags){
                    //console.log(data);                    
                    var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                    //
                    recepiesController.generateTagsList({'tags':tags});
                    //
                    recepiesPersister.loadAllRecepies(function(results){
                        var data={'recepies':results};                    
                        recepiesController.generateThumbnails(data);        
                    });
                });            
            });

            this.get("#/recipe/:id",function(){
                //get recipe id
                var recipeId=this.params['id'];

                function loadRecipePage(recipeObj,tags)
                {
                    $(contentSelector).load(RECIPE_PAGE,function(){                                        
                        var recipesController=new RecipesController(RECEPIES_ENDPOINT);

                        recipesController.generateTagsList({'tags':tags});

                        recipesController.generateSingleRecipe(recipeObj);
                    });
                }

                var recepiesPersister=new RecepiesPersister(RECEPIES_ENDPOINT);
                //
                loadTags(function(tags){

                    //
                    recepiesPersister.loadRecipe(recipeId,function(data){                               
                        loadRecipePage(data,tags);
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
    });
}());