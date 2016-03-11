define(['Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping'], function (ko) {
    'use strict';

    function AppViewModel(app) {
        this.name = ko.observable(app.Name);
        this.iconUrl = ko.observable(app.Icon);
        this.id = ko.observable(app.id);
        this.loader = app.loader;

        this.load = function () {
            this.loader.loadApplication(app);
        };
    }

    return AppViewModel;
});