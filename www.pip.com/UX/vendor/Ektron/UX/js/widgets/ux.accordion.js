define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_text!Vendor/Ektron/UX/views/widgets/ux.accordion.html',
    'Vendor/jQuery/UI/js/jquery-ui-complete.min',
    '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min'
], function ($, ko, template) {
    'use strict';
    function Panel(title, html) {
        this.title = ko.observable(title);
        this.html = ko.observable(html);
    }

    function Accordion(settings) {
        var me = this,
            index = 0,
            s;

        s = {
            options: {},
            panelsArray: []
        }

        s = $.extend(true, s, settings);

        this.element = ko.observable();
        this.options = ko.isObservable(s.options) ? s.options : ko.observable(s.options);
        this.Panel = Panel;
        this.panels = ko.isObservable(s.panelsArray) ? s.panelsArray : ko.observableArray(s.panelsArray);

        // ensure objects in the are Panel objects
        for (index; index < this.panels().length; index += 1) {
            if ('Panel' !== this.panels()[index].constructor.name) {
                this.panels()[index] = new Panel(this.panels()[index].title || '', this.panels()[index].html || '');
            }
        }

    }

    $('head').append('<script type="text/html" id="ux-accordion">' + template + '</script>');

    ko.bindingHandlers.uxAccordion = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            viewModel.element(element);

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).accordion('destroy');
            });
        },
        update: function (element, valueAccessor) {
            var options = valueAccessor() || {};

            element = $(element);
            if (element.is('.ux-accordion')) {
                element.accordion('destroy');
            }
            element.accordion(options());
        }
    };

    return Accordion;
});