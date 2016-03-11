/*global Ektron,alert,console*/
define([
    './AppViewModel',
    './queryService',
    'Vendor/knockout/knockout',
    'ektronjs',
    './queryDataSource',
    '_i18n!./nls/labels',
    '_css!UX/css/ux.application.core',
    './widgets/filterTree',
    './ko.label',
    'Vendor/jQuery/UI/js/jquery-ui-complete.min',
    '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min'
],
    function (AppViewModel, queryService, ko, $, queryDataSource, labels) {
        'use strict';

        function Application(settings) {
            var me = this;

            me.resourceLoader = settings.resourceLoader;
            me.appWindow = settings.appWindow;
        }

        function mapResponseViews(response) {
            for (var i = response.filters.length - 1; i >= 0; i--) {
                switch (response.filters[i].filterType) {
                    // Omitted filter types
                    case 'contentsubtype':
                    case 'foldername':
                    case 'xmlconfigid':
                        response.filters.splice(i, 1);
                        break;
                    default:
                        // Assign view property
                        response.filters[i].view = 'drilldownSelectList';
                        break;
                }
            }
        }

        Application.prototype.onOpen = function () {
            var me = this;

            me.viewModel = new AppViewModel(queryService, queryDataSource, function () {
                Ektron.UX.closeApplication();
            }, mapResponseViews);

            me.resourceLoader.load({
                files: ['app'],
                type: me.resourceLoader.Types.css
            });

            me.resourceLoader.load({
                files: ['app', 'app.lineview', 'app.tileview'],
                type: me.resourceLoader.Types.html,
                callback: function (apphtml, lineviewhtml, tileviewhtml) {
                    
                    var element = me.appWindow.getElement();

                    $(element).html(apphtml);
                    $(element).append(lineviewhtml);
                    $(element).append(tileviewhtml);

                    ko.applyBindings(me.viewModel, element);

                    me.appWindow.show(); 
                }
            }); 
        };

        Application.prototype.onClose = function () {
            // Boilerplate knockout cleanup
            var me = this,
                element;
            element = $(me.appWindow.getElement()).children()[0];
            if (typeof (element) !== "undefined") {
                ko.removeNode(element);
            }

            // Unhook events
            $(window).off('.dashboard');
            $(element).off('.dashboard');
        };

        return Application;
    }
);