(function () {
    require.config({
        paths: {
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'handlebars':'libs/handlebars',
            'requestModule' : 'libs/requestModule',
            'mainController' : 'controllers/mainController',
            'recipesController':'controllers/recipesController'
        }
    });

    require(['recipesController','sammy'], function (RecipesController,sammy) {

        //define endpoints
        var rootUrl='http://localhost/';
        var recipesEndpoint = rootUrl+'recipes';

        var container='#content-box';

        //var theController = new mainController(rootUrl);
        //theController.loadUI('#content-box');
        //var recipesController=new RecipesController(recipesEndpoint);
        //recipesController.loadUI();

        //sammy here
        var app = sammy(container, function() {

            //home page is here
            this.get("#/recipes/", function() { 
                
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