define(['UX/js/resourceloader', 'UX/js/uxViewModel', 'Vendor/knockout/knockout'], function (ResourceLoader, UXViewModel, ko) {
    'use strict';

    // shim for IE not supporting constructor.name
    if (Function.prototype.name === undefined && Object.defineProperty !== undefined) {
        Object.defineProperty(Function.prototype, 'name', {
            get: function () {
                var results,
                    funcNameRegex = /function\s([^(]{1,})\(/;
                results = (funcNameRegex).exec((this).toString());
                return (results && results.length > 1) ? $.trim(results[1]) : '';
            },
            set: function (value) { }
        });
    }

    function UX(toolbar, apploader, resourceloader) {
        //properties
        this.toolbar = toolbar;
        this.apploader = apploader;
        this.resourceloader = resourceloader;
        this.viewModel = {
            window: function () { return {}; },
            runningApplication: function () { }
        };
    }

    UX.prototype.load = function (settings) {
        var me = this;
        //Now that settings are loaded, check authorization before anything else
        if (settings.isAuthorized === true) {
            this.toolbar.load(function () {
                me.resourceloader.load({
                    files: 'ux',
                    selector: 'body',
                    type: me.resourceloader.Types.html,
                    callback: function (data) {
                        me.resourceloader.load({
                            files: ['ux', 'ux.reset', 'ux.utility', 'ux.icons', 'icons'],
                            type: me.resourceloader.Types.css,
                            callback: function (data) {
                                me.resourceloader.load({
                                    files: 'ux.launcher',
                                    type: me.resourceloader.Types.html,
                                    selector: '.ektron-ux .launcher',
                                    callback: function (data) {
                                        me._apps = settings.apps;

                                        me.viewModel = new UXViewModel(me);
                                        me.loadApplication(me._apps[0]);
                                        ko.applyBindings(me.viewModel, $('.ektron-ux')[0]);
                                    }
                                });
                            }
                        });
                    }
                });
            });
        }
    };

    UX.prototype.closeApplication = function () {
        var me = this,
            currentAppObj = {},
            index = 0;

        for (index; index < me._apps.length; index += 1) {
            if (me._apps[index].id === me.runningApplicationId) {
                currentAppObj = me._apps[index];
            }
        }
        if (currentAppObj.id === me._apps[0].id) {
            // current app is the default app.  Force close before calling loadApplication
            closeAndClean(me);
        }
        me.runningApplicationId = '';
        me.loadApplication(me._apps[0]);
    };

    UX.prototype.loadApplication = function (application) {
        var me = this,
            toolbar,
            apploader,
            viewModel,
            appid,
            resourceloader;

        toolbar = me.toolbar ? me.toolbar : application.ux.toolbar;
        apploader = me.apploader ? me.apploader : application.ux.apploader;
        viewModel = me.viewModel || application.ux.viewModel;
        appid = (typeof (application.id) === 'string') ? application.id : application.id();

        if ('undefined' === typeof (me.runningApplicationId) || me.runningApplicationId !== appid) {
            resourceloader = new ResourceLoader('applications/' + appid);

            toolbar.clear();
            apploader.loadApplication(appid, {
                toolbar: toolbar,
                resourceLoader: resourceloader,
                appWindow: viewModel.window(),
                callback: function (app) {
                    if ('undefined' !== typeof (me.runningApplication)) {
                        closeAndClean(me);
                    }

                    me.runningApplicationId = appid;
                    me.runningApplication = app;

                    me.viewModel.runningApplication(app);
                }
            });
        }
    };

    UX.prototype.apps = function () {
        return this._apps;
    };

    // helper functions
    function closeAndClean(me) {
        if ('undefined' !== typeof (me.runningApplication)) {
            me.runningApplication.onClose();
            me.viewModel.window().clear();
            me.viewModel.window().hide();
        }
    }

    return UX;
});