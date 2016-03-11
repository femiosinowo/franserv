/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/uxViewModel.js" />

var UXViewModel, AppViewModel, viewModel, ux, appWindow, $, appLauncher;

module('UX View Model Tests', {
    setup: function () {
        'use strict';
        AppViewModel = function () { };
        
        ux = {
            listofapps: [{
                Name: 'TestApp1',
                id: 'app1',
                Icon: '/abc'
            }, {
                Name: 'TestApp2',
                id: 'app2',
                Icon: '/efg'
            }],
            apps: function () {
                return ux.listofapps;
            },

            toolbar: {}
        };

        appWindow = {
            clear: function () {

            },
            show: function () { },
            hide: function () { }
        };

        $ = function () {
            return {
                on: function () { },
                toggleClass: function () { }
            };
        };

        appLauncher = { on: function () { }, visible: function () { } };

        UXViewModel = window.createModule(AppViewModel, appWindow, ko, appLauncher);

        viewModel = new UXViewModel(ux);
    }
});

test('UX view model loads app list', function () {
    'use strict';
    var foundApps = [];

    function AppViewModel(app) {
        foundApps.push(app);
    }

    UXViewModel = window.createModule(AppViewModel, null, ko, appLauncher);

    viewModel = new UXViewModel(ux);


    equal(foundApps[0], ux.listofapps[0], 'First application found');
    equal(foundApps[1], ux.listofapps[1], 'Second application found');
});

test('UX view model loads toolbar', function () {
    'use strict';
    var foundApps = [];

    viewModel = new UXViewModel(ux);

    equal(viewModel.toolbar, ux.toolbar, 'First application found');
});

test('UX does toggleLauncher make the launcher window appear and disappear', function () {
    'use strict';
    UXViewModel = window.createModule(AppViewModel, null, ko, appLauncher);

    viewModel = new UXViewModel(ux);

    ok(!viewModel.isLauncherVisible(), 'The launcher is invisible');
    ok(!viewModel.isOverlayVisible(), 'The overlay window is invisible');
    ok(!viewModel.isAppWindowVisible(), 'The app window window is invisible');

    viewModel.toggleLauncher();

    ok(viewModel.isLauncherVisible(), 'The launcher is visible');
    ok(!viewModel.isAppWindowVisible(), 'The app window window is invisible');
    ok(viewModel.isOverlayVisible(), 'The overlay window is visible');

    viewModel.toggleAppWindow();

    ok(viewModel.isLauncherVisible(), 'The launcher is visible');
    ok(viewModel.isAppWindowVisible(), 'The app window window is visible');
    ok(viewModel.isOverlayVisible(), 'The overlay window is visible');

    viewModel.toggleLauncher();

    ok(!viewModel.isLauncherVisible(), 'The launcher is invisible');
    ok(viewModel.isAppWindowVisible(), 'The app window window is visible');
    ok(viewModel.isOverlayVisible(), 'The overlay window is visible');

    viewModel.toggleAppWindow();

    ok(!viewModel.isLauncherVisible(), 'The launcher is invisible');
    ok(!viewModel.isOverlayVisible(), 'The overlay window is invisible');
    ok(!viewModel.isAppWindowVisible(), 'The app window window is invisible');
});

test('Viewmodel has window property', function () {
    'use strict';
    ok('undefined' !== typeof viewModel.window(), 'viewModel has window');

    viewModel.window().show();

    ok(viewModel.isAppWindowVisible(), 'viewModel window has show');

    viewModel.window().hide();

    ok(!viewModel.isAppWindowVisible(), 'viewModel window has hidden');
});

test('UX view model loads toolbar', function () {
    'use strict';
    viewModel = new UXViewModel(ux);

    ok('undefined' !== typeof viewModel.runningApplication, 'UX view model has running application property');
});

test('Does window.clear() call appwindow.clear()', function () {
    'use strict';
    viewModel = new UXViewModel(ux);

    var clearcalled = false;
    appWindow.clear = function () {
        clearcalled = true;
    };
    viewModel.window().clear();
    ok(clearcalled, 'Appwindow clear has been called when window.clear called');
});

test('Does window.getElement() call appwindow.getElement()', function () {
    'use strict';
    viewModel = new UXViewModel(ux);

    var clearcalled = false;
    appWindow.getElement = function () {
        clearcalled = true;
    };
    viewModel.window().getElement();
    ok(clearcalled, 'Appwindow clear has been called when window.getElement called');
});

test('initializing viewmodel subscribes appLauncher\'s visibility to launcher visible observable', function () {
    var eventCalled = false, closeCalled = false, foundCallback;

    UXViewModel = window.createModule(AppViewModel, null, ko, {
        on: function (eventName, callback) {
            eventCalled = 'beforeunload' === eventName;

            foundCallback = callback;
        }
    });

    ux.closeApplication = function () {
        closeCalled = true;
    };

    viewModel = new UXViewModel(ux);

    ok(eventCalled, 'Appwindow initializes appLauncher upon load');

    foundCallback();

    ok(closeCalled, 'Appwindow closes appLauncher upon load');
});

test('setting launcher visibility toggles appLauncher visible property', function () {
    var foundVisible = false;

    appLauncher.visible = function (value) {
        foundVisible = value;
    };

    viewModel = new UXViewModel(ux);

    viewModel.isLauncherVisible(true);

    ok(foundVisible, 'setting ux isLauncherVisible correctly sets appLauncher visible property');
});

test('setting launcher invisibility toggles appLauncher visible property', function () {
    var foundInvisible = false;

    appLauncher.visible = function (value) {
        foundInvisible = !value;
    };

    viewModel = new UXViewModel(ux);

    viewModel.isLauncherVisible(true);
    viewModel.isLauncherVisible(false);

    ok(foundInvisible, 'setting ux isLauncherVisible correctly sets appLauncher visible property');
});
