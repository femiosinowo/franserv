define(['require', 'ektronjs'], function (require, $, appWindow) {
    'use strict';

    var Applications = {
        options: {},
        loadApplication: function (id, options) {
            var mypath = 'applications/' + id + '/js/app'; // should not have file extension.
            require([mypath], function (App) {
                if (typeof (App) === 'function') {
                    App = new App({
                        toolbar: options.toolbar,
                        resourceLoader: options.resourceLoader,
                        appWindow: options.appWindow
                    });
                }

                if ('undefined' !== typeof (options) && 'undefined' !== typeof (options.callback)) {
                    options.callback(App);
                }

                App.onOpen();
            });
        }
    };

    return Applications;
});