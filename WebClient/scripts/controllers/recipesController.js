define(['jquery', 'requestModule','recipesPersister','handlebars',], function ($, requestModule,RecipesPersister) {
    var RecipesController = (function () {
        function RecipesController(rootUrl) {
            this.rootUrl = rootUrl;
        }

        RecipesController.prototype = {
            
            generateThumbnails:function(data)
            {				
                //
                var recipesListContainer = $("#content-box");
				var generateRecipes = Handlebars.compile($('#recipes-template').html());
				// empty the container
				while (recipesListContainer.firstChild) {
					recipesListContainer.removeChild(recipesListContainer.firstChild);
				}
				
				recipesListContainer.html(generateRecipes(data));
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
            }
        };

        return RecipesController;
    }());
    return RecipesController;
});