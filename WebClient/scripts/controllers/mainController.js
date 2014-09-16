/**
 * Created by nzhul on 9/16/2014.
 */
define(['jquery', 'requestModule'], function ($, requestModule) {
    var MainController = (function () {
        function MainController(rootUrl) {
            this.rootUrl = rootUrl;
        }

        MainController.prototype = {
            loadUI: function (selector) {
                this.attachEventHandlers(selector); // !! Call all Events
            },

            // Attach all events to the wrapper!
            // That way the newly created elements will have event listeners
            attachEventHandlers: function (selector) {
                var wrapper = $(selector);
                var self = this;

                wrapper.on('click', '#btn-login', function () {

                });
            }
        };

        return MainController;
    }());
    return MainController;
});