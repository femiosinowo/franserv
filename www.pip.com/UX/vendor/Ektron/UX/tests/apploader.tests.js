/// <reference path="setup.js" />
/// <reference path="../js/apploader.js" />
var Applications, requirejs, appWindow, toolbar, ektronjs;

module('Application Tests', {
    setup: function () {
        'use strict';
        requirejs = {
            require: function (deps, callback) {
                callback({
                    onOpen: function () { }
                });
            }
        };
        ektronjs = function () {
            return ektronjs;
        };
        ektronjs.extend = function (def, options) { };
        ektronjs.prepend = function (text) { };
        ektronjs.append = function (text) { };

        toolbar = {};

        appWindow = {
            clear: function () { }
        };

        Applications = window.createModule(ektronjs, appWindow, requirejs);
    }
});

test('Does Application object exist', function () {
    'use strict';
    ok('undefined' !== typeof (Applications), 'Applications is defined');
});

test('Load an application', function () {
    'use strict';
    var appId = 'app', requireCount = 0, appLoaded = false, foundCorrectPath = false, path = '';

    requirejs.require = function (deps, callback) {
        requireCount++;//ignore jslint
        path = deps[0];
        callback({ onOpen: function () { appLoaded = true; } });
    };

    Applications.loadApplication(appId);

    ok(requireCount === 1, 'RequireJS called only once');
    ok(appLoaded, 'Application has been loaded');
    equal(path, 'applications/app/js/app', 'Require used correct path to file.');
});

test('Load an application with the toolbar', function () {
    'use strict';
    var appId = 'app',
        requireCount = 0,
        appLoaded = false,
        foundToolbar = false,
        openCount = 0;
    requirejs.require = function (deps, callback) {
        requireCount++;//ignore jslint

        function DummyApp(settings) {
            foundToolbar = (typeof (settings.toolbar) !== 'undefined');
        }

        DummyApp.prototype.onOpen = function () {
            /*ignore jslint start*/
            openCount++;
            /*ignore jslint end*/
        };

        callback(DummyApp);
    };

    Applications.loadApplication(appId, {
        toolbar: toolbar
    });

    ok(requireCount === 1, 'RequireJS called only once');
    ok(openCount === 1, 'onOpen called only once');
    ok(foundToolbar, 'Toolbar has been found');
});

test('Load an application with the toolbar and resourceloader and appWindow', function () {
    'use strict';
    var resourceLoader = {},
        appId = 'app',
        requireCount = 0,
        appLoaded = false,
        foundResourceLoader = false,
        foundToolbar = false,
        foundAppWindow = false,
        openCount = 0,
        appWindow = {};
    requirejs.require = function (deps, callback) {
        requireCount++;//ignore jslint

        function DummyApp(settings) {
            foundToolbar = (typeof (settings.toolbar) !== 'undefined');
            foundResourceLoader = (typeof (settings.resourceLoader) !== 'undefined');
            foundAppWindow = (typeof (settings.appWindow) !== 'undefined');
        }

        DummyApp.prototype.onOpen = function () {
            openCount++;//ignore jslint
        };

        callback(DummyApp);
    };

    Applications.loadApplication(appId, {
        toolbar: toolbar,
        resourceLoader: resourceLoader,
        appWindow: appWindow
    });

    ok(requireCount === 1, 'RequireJS called only once');
    ok(openCount === 1, 'onOpen called only once');
    ok(foundToolbar, 'Toolbar exists');
    ok(foundResourceLoader, 'ResourceLoader exists');
    ok(foundAppWindow, 'AppWindow exists');
});

test('Loading an application passes the created application into a callback', function () {
    'use strict';
    var foundApp;

    Applications.loadApplication('nothing', {
        callback: function (app) {
            foundApp = app;
        }
    });

    notEqual(typeof(foundApp), 'undefined', 'Application defined');
});