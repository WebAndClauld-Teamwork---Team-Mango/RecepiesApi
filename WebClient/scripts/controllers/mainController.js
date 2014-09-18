/**
 * Created by nzhul on 9/16/2014.
 */
define(['jquery','fileHelper','handlebars','constants'], function ($, FileHelper) {
    var MainController = (function () {
        function MainController(rootUrl) {
            this.rootUrl = rootUrl;
        }

        MainController.prototype = {

            function generateTags(data)
            {
                function fillTagsList(template)
                {
                    var tagsListContainer = $("#tags-list");
                    var generateTagsList= Handlebars.compile(template);
                    // empty the container
                    tagsListContainer.children().remove();
                    //fill container with recepies
                    tagsListContainer.html(generateTagsList(data));
                }
                //
                //create file helper
                var fileHelper=new FileHelper();
                //read tags list template
                fileHelper.loadTextFile(TAGS_LIST_TEMPLATE,function(result){
                    fillTagsList(result);
                },function(error){
                    //report error here...
                    console.log(error);
                });                
            }
        };

        return MainController;
    }());
    return MainController;
});