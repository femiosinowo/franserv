/*global window,module,test,ok,alert*/
/// <reference path="setup.js" />
/// <reference path="../js/app.js" />

var App, app, ko, resourceLoader, appWindow, queryService, $;

module('Dashboard App', {
    setup: function () {
        'use strict';
        queryService = {};

        ko = {
            applyBindings: function () { }
        };

        resourceLoader = {
            Types: {
                html: 'html',
                css: 'css'
            },
            load: function (settings) {
                if (typeof (settings.callback) === 'function') {
                    settings.callback();
                }
            }
        };

        appWindow = {
            getElement: function () { return {}; },
            show: function () { }
        };

        $ = function () {
            return {
                append: function () { return $(); },
                html: function () { return $(); }
            };
        };

        App = window.createModule(function () { }, {}, ko, $);

        app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });
    }
});

test('App loads viewmodel with query manager', function () {
    'use strict';
    var queryService = {}, foundqueryService;

    function AppViewModel(queryService) {
        foundqueryService = queryService;
    }

    App = window.createModule(AppViewModel, queryService, ko, $);
    app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });

    app.onOpen();

    equal(foundqueryService, queryService, "Passed the correct query manager through to the AppViewModel");
});

test('App binds correct knockout viewmodel', function () {
    'use strict';
    var viewModel, foundViewModel, element = {}, foundElement;

    function AppViewModel(queryService) {
        viewModel = this;
    }

    appWindow.getElement = function () {
        return element;
    };

    ko.applyBindings = function (viewModel, element) {
        foundViewModel = viewModel;
        foundElement = element;
    };

    App = window.createModule(AppViewModel, {}, ko, $);
    app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });

    app.onOpen();

    equal(foundViewModel, viewModel, "Bound the correct viewmodel to the application template");
    equal(foundElement, element, "Bound the viewmodel to the correct element");
});

test('App loads correct template', function () {
    'use strict';
    var foundAppTemplate = false;

    resourceLoader.load = function (settings) {
        foundAppTemplate |= (settings.files[0] === 'app' && settings.type === 'html');
    };

    app.onOpen();

    ok(foundAppTemplate, "Loaded correct template");
});

test('App adds template contents to appWindow', function () {
    'use strict';
    var html = 'this isn\'t real html', element = {}, foundHtml = false, foundElement = false, $ref, $extend;

    resourceLoader.load = function (settings) {
        if (settings.files[0] === 'app' && settings.type === 'html') {
            settings.callback(html);
        }
    };

    appWindow.getElement = function () {
        return element;
    };

    $extend = function (selector) {
        $ref = $();

        if (selector === element) {
            foundElement = true;

            $ref.html = function (contents) {
                foundHtml = contents === html;
            };
        }

        return $ref;
    };

    App = window.createModule(function () { }, {}, ko, $extend);

    app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });

    app.onOpen();

    ok(foundElement, "Found correct appWindow element in jQuery");
    ok(foundHtml, "Passed correct html into appWindow");
});

test('Application shows appWindow on open', function () {
    'use strict';
    var shown = false;

    appWindow.show = function () {
        shown = true;
    };

    app.onOpen();

    ok(shown, "Called appWindow.show");
});

test('App loads viewmodel with query data source', function () {
    'use strict';
    var queryService = {}, queryDataSource = {}, foundQueryDataSource;

    function AppViewModel(queryService, queryDataSource) {
        foundQueryDataSource = queryDataSource;
    }

    App = window.createModule(AppViewModel, queryService, ko, $, queryDataSource);
    app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });

    app.onOpen();

    equal(foundQueryDataSource, queryDataSource, "Passed the correct query data source through to the AppViewModel");
});

test('App loads CSS onOpen', function () {
    'use strict';
    var foundCss = false;

    resourceLoader.load = function (settings) {
        if (settings.files[0] === 'app' && settings.type === 'css') {
            foundCss = true;
        }
    };

    App = window.createModule(function () { }, {}, ko, $, {});
    app = new App({ resourceLoader: resourceLoader, appWindow: appWindow });

    app.onOpen();

    ok(foundCss, "Loaded the app's CSS");
});