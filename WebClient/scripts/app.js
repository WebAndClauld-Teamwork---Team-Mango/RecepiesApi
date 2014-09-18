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

        //define endpoints
        var rootUrl='http://localhost:1937/api/';    
        var recipesEndpoint = rootUrl+'recepies';
        var container='#content-box';

        //var theController = new mainController(rootUrl);
        //theController.loadUI('#content-box');
        //var recipesController=new RecipesController(recipesEndpoint);
        //recipesController.loadUI();

        //sammy here
        var app = sammy(container, function() {

            //home page is here
            this.get("#/recipes", function() { 
                var recipeController=new RecipesController(recipesEndpoint);
                //template test
                var recepies=[];
                for(var i=0;i<10;i++){
                    var time=((Math.random()*180)+10)|0;
                    recepies.push(new Recipe(i,'recipe '+i,'recipe desc'+i,'images/thumbnails/food-image-1.jpg',time));
                }
                //
                //var recipesPersister=new RecipesPersister(recipesEndpoint);
                //recipesPersister.loadAllRecepies();
                //data
                var data={
                    'recepies':recepies
                };
                //console.log(data);
                var recipesController=new RecipesController(recipesEndpoint);    
                //
                recipesController.generateThumbnails(data);
            });

            //recipes page
            this.get("#/recipes/new", function() {
                alert("new bace");         
            });
            
            //about page is here
            this.get("#/about",function(){
                alert("about!!!");
                //$("#content").load(PAGES_URL+'document.html',function(){               
            });                                            
        });
        //run application
        app.run("#/recipes");         
    });
}());