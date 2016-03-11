define(['UX/js/appViewModel', 'UX/js/appwindow', 'Vendor/knockout/knockout', 'UX/js/appLauncher', '_i18n!./nls/labels'], function (AppViewModel, appWindow, ko, appLauncher, labels) {
    'use strict';

    function UXViewModel(ux) {
        var me = this,
            apps = ux.apps();

        me.apps = ko.observableArray();
        me.toolbar = ux.toolbar;
        me.labels = labels;
        me.isLauncherVisible = ko.observable(false);
        me.isLauncherVisible.subscribe(function (newValue) {
            appLauncher.visible(newValue);
        });

        me.isAppWindowVisible = ko.observable(false);
        me.isOverlayVisible = ko.computed(function () {
            return me.isLauncherVisible() || me.isAppWindowVisible();
        });

        me.ux = ux;

        ko.utils.arrayForEach(apps, function (app) {
            app.loader = me.ux;
            me.apps.push(new AppViewModel(app));
        });

        me.runningApplication = ko.observable();

        me.toggleLauncher = function () {
            this.isLauncherVisible(!this.isLauncherVisible());
        }.bind(this);

        me.toggleAppWindow = function () {
            this.isAppWindowVisible(!this.isAppWindowVisible());
        }.bind(this);

        me.window = ko.observable({
            show: function () {
                me.isAppWindowVisible(true);
                appWindow.show();
            },
            hide: function () {
                me.isAppWindowVisible(false);
                appWindow.hide();
            },
            clear: function () {
                appWindow.clear();
            },
            getElement: function () {
                return appWindow.getElement();
            }
        });

        // Close the current UX application before window unload
        if ('function' === typeof appLauncher.on) {
            $ektron(window).on('unload', function () {
                ux.closeApplication();
            });
        }
    }

    return UXViewModel;
});