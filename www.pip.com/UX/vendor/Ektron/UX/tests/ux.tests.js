/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/ux.js" />

var UX, ux, SettingsMock, toolbar, apploader, TemplateLoader, UXViewModel, ResourceTypes, ResourceLoader, resourceloader;

function $() { return {}; }

module('UX Core Tests', {
    setup: function () {
        'use strict';
        var Toolbar = {
            buttons: [],
            addButton: function (button) {
                Toolbar.buttons.push(button);
            }
        };

        ResourceTypes = function () {
            this.css = 'css';
            this.html = 'html';
            this.text = 'text';
        };

        ResourceLoader = function (baseUrl) {
            this.Types = new ResourceTypes();
            this._baseUrl = ('string' === typeof (baseUrl)) ? baseUrl + '/' : '';
        };
        ResourceLoader.prototype.load = function (settings) {
            settings.callback();
        };

        SettingsMock = {
            'apps': [{
                'Name': 'Site',
                'id': 'app',
                'Icon': '/applications/site/img/icon.png'
            }],
            'isAuthorized': true,
            'sitePath': '/phoenix'
        };

        toolbar = {
            load: function (callback) { callback(); },
            clear: function () { }
        };

        apploader = {
            loadApplication: function () { }
        };

        resourceloader = new ResourceLoader();

        UXViewModel = function () {
        };

        UXViewModel.prototype.window = function () { return {}; };
        
        UX = window.createModule(ResourceLoader, UXViewModel, ko, $);
        ux = new UX(toolbar, apploader, resourceloader);
    }
});

test('Does UX exist', function () {
    'use strict';
    ok('undefined' !== typeof (ux), 'UX is defined');
});

test('Does UX have apps available', function () {
    'use strict';
    var SettingsMock2 = {
        'apps': [{
            'Name': 'TestApp1',
            'id': 'app',
            'Icon': '/applications/site/img/icon.png'
        }, {
            'Name': 'TestApp2',
            'id': 'app2',
            'Icon': '/applications/site/img/icon.png'            
        }],
        'isAuthorized': true,
        'sitePath': '/phoenix'
    };

    ux.load(SettingsMock2);
    ok('undefined' !== typeof (ux.apps()), 'Apps list present');
    ok('TestApp1' === ux.apps()[0].Name, 'App1 present');
    ok('TestApp2' === ux.apps()[1].Name, 'App2 present');
});

test('Does UX initially clear toolbar when application loads', function () {
    'use strict';
    var toolbarcleared = false;
    toolbar.clear = function () {
        toolbarcleared = true;
    };
    ux = new UX(toolbar, apploader, resourceloader);
    ux.load(SettingsMock);
    ux.loadApplication(ux.apps()[0]);
    ok(toolbarcleared === true, 'Toolbar has been cleared');
});

test('UX passes toolbar into application launcher', function () {
    'use strict';
    var foundToolbar = false;
    apploader.loadApplication = function (path, options) {
        foundToolbar = (options.toolbar === window.toolbar);
    };

    ux.loadApplication(SettingsMock.apps[0]);

    ok(foundToolbar, 'Passed toolbar into application launcher');
});

test('UX passes app window into application launcher', function () {
    'use strict';
    var foundAppWindow = false;

    ux.viewModel = {
        window: function () { }
    };

    apploader.loadApplication = function (path, options) {
        foundAppWindow = (options.window === ux.viewModel.window());
    };

    ux.loadApplication(SettingsMock.apps[0]);

    ok(foundAppWindow, 'Passed toolbar into application launcher');
});

test('ux calls loadApplication on load', function () {
    'use strict';
    var loadedappcalled = false;
    ux.loadApplication = function (application) {
        loadedappcalled = true;
    };

    ux.load(SettingsMock);
    ok(loadedappcalled === true, 'UX load function calls loadApplication');
});

test('UX has loaded default application', function () {
    'use strict';
    var apploaded = false;
    ux.loadApplication = function (application) {
        apploaded = application === ux.apps()[0];
    };

    ux.load(SettingsMock);
    ok(apploaded, 'The default app is loaded when UX loads');
});

test('UX passes application specific temploate loader into loaded applications', function () {
    'use strict';
    var loadedPath = '', loadedResourceLoader = null, resourceloader = null;

    ResourceLoader = function (path) {
        loadedPath = path;
        resourceloader = this;
    };

    UX = window.createModule(ResourceLoader, UXViewModel);
    ux = new UX(toolbar, apploader);

    apploader.loadApplication = function (settings) {
        loadedResourceLoader = settings;
    };

    ux.loadApplication(SettingsMock.apps[0]);

    equal(loadedPath, 'applications/app', 'Loaded correct application baseUrl into new template loader');
    ok(loadedResourceLoader, resourceloader, 'Called apploader.loadApplication with correct template loader');
});

test('UX loads view during load', function () {
    'use strict';
    var foundBaseUrls = [], resourceloader, foundIds = [], foundSelectors = [];

    function ResourceLoader(baseUrl) {
        foundBaseUrls.push(baseUrl);
        resourceloader = this;
        resourceloader.Types = new ResourceTypes();
    }

    ResourceLoader.prototype.load = function (settings) {
        foundIds.push(settings.files);
        foundSelectors.push(settings.selector);
        settings.callback();
    };

    resourceloader = new ResourceLoader();

    UX = window.createModule(ResourceLoader, UXViewModel, ko, $);
    ux = new UX(toolbar, apploader, resourceloader);

    equal(typeof (foundBaseUrls[0]), 'undefined', 'UX doesn\'t pass in a baseUrl to its template loader');
    equal(typeof (foundBaseUrls[1]), 'undefined', 'UX doesn\'t pass in a baseUrl to its template loader');

    ux.load(SettingsMock);

    equal(foundIds[0], 'ux', 'UX loads template');
    equal(foundSelectors[0], 'body', 'UX loads template');
    deepEqual(foundIds[1], ['ux', 'ux.reset', 'ux.utility', 'ux.icons', 'icons'], 'UX loads its own css');
    equal(foundIds[2], 'ux.launcher', 'UX loads launcher template');
    equal(foundSelectors[2], '.ektron-ux .launcher', 'UX loads launcher template');
});

test('UX binds itself to a UX view model', function () {
    'use strict';
    var viewModel, foundUX, boundViewModel;
    UXViewModel = function (ux) {
        foundUX = ux;
        this.window = function () { return {}; };
        viewModel = this;
    };

    ko = {
        applyBindings: function (model) {
            boundViewModel = model;
        }
    };

    UX = window.createModule(ResourceLoader, UXViewModel, ko, $);
    ux = new UX(toolbar, apploader, resourceloader);

    ux.load(SettingsMock);

    equal(foundUX, ux, 'Created an UX viewModel');
    equal(boundViewModel, viewModel, 'Bound the correct UX viewModel');
});

test('UX loads toolbar', function () {
    'use strict';
    var loaded = false;
    toolbar.load = function (callback) {
        loaded = true;
    };

    ux.load(SettingsMock);

    ok(loaded, 'Toolbar loaded');
});

test('UX closes previous application before opening new application', function () {
    'use strict';
    var isAppClosed = false,
        app1 = {
            'Name': 'App1',
            'id': 'app1',
            'Icon': '/applications/site/img/icon.png'
        },
        app2 = {
            'Name': 'App2',
            'id': 'app2',
            'Icon': '/applications/site/img/icon.png'
        },
        currentApp,
        closed = false,
        foundApp,
        viewModel,
        foundUX,
        boundViewModel,
        loadedApp,
        appwindowcleared = false,
        appwindowhidden = false;

    UXViewModel = function (ux) {
        foundUX = ux;
        this.window = function () {
            return {
                clear: function () {
                    appwindowcleared = true;
                },
                hide: function () {
                    appwindowhidden = true;
                }
            };
        };
        viewModel = this;
        this.runningApplication = function (app) {
            foundApp = app;
        };
    };

    ko = {
        applyBindings: function (model) {
            boundViewModel = model;
        }
    };

    UX = window.createModule(ResourceLoader, UXViewModel, ko, $);
    ux = new UX(toolbar, apploader, resourceloader);

    apploader.loadApplication = function (id, options) {
        loadedApp = {
            onClose: function () {
                closed = true;
            }
        };
        options.callback(loadedApp);
    };

    ux.load({
        'apps': [app1, app2],
        'isAuthorized': true,
        'sitePath': '/phoenix'
    });

    ux.loadApplication(app1);

    ok(foundApp, loadedApp, 'Ran app 1');

    ux.loadApplication(app2);

    ok(closed, 'First running application was closed');
    ok(appwindowcleared, 'The appwindow has been cleared');
    ok(appwindowhidden, 'The appwindow has been hidden');
    equal(foundApp, loadedApp, 'Ran app 2');
    
    closed = appwindowcleared = appwindowhidden = false;

    ux.loadApplication(app1);

    ok(closed, 'Second running application was closed');
    ok(appwindowcleared, 'The appwindow has been cleared');
    ok(appwindowhidden, 'The appwindow has been hidden');
    equal(foundApp, loadedApp, 'Ran app 1');
    closed = appwindowcleared = appwindowhidden = false;

    ux.loadApplication(app1);

    ok(!closed, 'Re-running application did not close itself');
    ok(!appwindowcleared, 'The appwindow has not been cleared');
    ok(!appwindowhidden, 'The appwindow has not been hidden');
    equal(foundApp, loadedApp, 'Still running app 1');
    closed = appwindowcleared = appwindowhidden = false;

    ux.closeApplication(app1);

    ok(closed, 'Application 1 was forced close.');
    ok(appwindowcleared, 'The appwindow was cleared');
    ok(appwindowhidden, 'The appwindow was hidden');
    equal(foundApp, loadedApp, 'Still running app 1');

    closed = appwindowcleared = appwindowhidden = false;
    ux.closeApplication(app2);
    equal(foundApp, loadedApp, 'Now running app 1');
});