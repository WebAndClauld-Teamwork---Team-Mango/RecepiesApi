(function () {
    require.config({
        paths: {
            'jquery' : 'libs/jquery-2.1.1.min',
            'sammy' : 'libs/sammy-latest.min',
            'requestModule' : 'libs/requestModule',
            'mainController' : 'controllers/mainController'
        }
    });

    require(['mainController'], function (mainController) {

        var rootUrl = 'http://localhost/recipes';
        var theController = new mainController(rootUrl);

        theController.loadUI('#content-box');
    })
}());