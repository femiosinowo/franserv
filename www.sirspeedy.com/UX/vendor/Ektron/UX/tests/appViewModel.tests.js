/// <reference path="setup.js" />
/// <reference path='../vendor/qunit/js/qunit.js' />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../js/appViewModel.js" />

var UXViewModel, viewModel, app, AppViewModel;

module('UX Core Tests', {
    setup: function () {
        'use strict';
        app = {
            Name: 'TestApp',
            id: 'app',
            Icon: '/abc',
            loader: {
                loadApplication: function () { }
            }
        };

        AppViewModel = window.createModule(ko);

        viewModel = new AppViewModel(app);
    }
});

test('UX view model correctly copies application name and icon url', function () {
    'use strict';
    equal(viewModel.name(), 'TestApp', 'Name copied correctly');
    equal(viewModel.iconUrl(), '/abc', 'Icon url copied correctly');
});

test('App view model correctly launches application when load() is called', function () {
    'use strict';
    var foundApp;
    viewModel.loader.loadApplication = function (localApp) {
        foundApp = localApp;
    };
    viewModel.load();

    equal(foundApp, app, 'Loaded correct application');
});