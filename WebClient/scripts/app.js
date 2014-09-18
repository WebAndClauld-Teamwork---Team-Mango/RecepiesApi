(function () {
    require.config({
        paths: {
            //libs
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'handlebars':'libs/handlebars',
            'requestModule' : 'libs/requestModule',
            'mainController' : 'controllers/mainController',
            'recipesController':'controllers/recipesController',
            //configs
            'constants':'configs/constants',
            //persisters
            'recipesPersister':'persisters/recepiesPersister',
            //entities
            'recipe':'entities/recipe',
            //helpers
            'restHelper':'helpers/restHelper',
            'requestModule':'helpers/requestModule',
            'fileHelper':'helpers/fileHelper'
        }
    });

    require(['recipesPersister','recipesController','sammy','recipe'], function (RecipesPersister,RecipesController,sammy,Recipe) {

        //main content container
        var contentSelector='#content-box';

        var app = sammy(contentSelector, function() {

            //recepies page
            this.get("#/recipes", function() { 
                var recepiesController=new RecipesController(RECEPIES_ENDPOINT);
                //template test
                var recepies=[];
                for(var i=0;i<10;i++){
                    var time=((Math.random()*180)+10)|0;
                    recepies.push(new Recipe(i,'recipe '+i,'recipe desc'+i,'images/thumbnails/food-image-1.jpg',time));
                }
                //
                var data={
                    'recepies':recepies
                };
                //
                recepiesController.generateThumbnails(data);
            });

            this.get("#/recipe/:id",function(){
                //get recipe id
                var recipeId=this.params['id'];  
                $(contentSelector).load(RECIPE_PAGE,function(){                                        
                    //get recipe from rest by id
                    console.log(recipeId);
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