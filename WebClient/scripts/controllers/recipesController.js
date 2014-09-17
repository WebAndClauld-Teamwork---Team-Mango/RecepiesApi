define(['jquery', 'requestModule','handlebars'], function ($, requestModule) {
    var RecipesController = (function () {
        function RecipesController(rootUrl) {
            this.rootUrl = rootUrl;
        }

        RecipesController.prototype = {
            
            generateThumbnails:function(data)
            {                

                console.log(data);
                //             
                var selectTemplate = $("#content-box").html();                 
                //var selectTemplate = $("#select-template").html();
                //var generateSelect = Handlebars.compile(selectTemplate);
                //$("#select-container").html(generateSelect(data));
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
            }
        };

        return RecipesController;
    }());
    return RecipesController;
});