define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_text!Vendor/Ektron/UX/views/widgets/ux.dialog.html',
    '_i18n!./nls/labels',
    'Vendor/jQuery/UI/js/jquery-ui-complete.min',
    '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min'
], function ($, ko, template, labels) {
    'use strict';

    function Dialog(settings) {
        //options, title, html
        var me = this,
            s;

        s = {
            options: {
                autoOpen: false,
                appendTo: '.ektron-ux .ux-appWindow',
                closeText: '<span data-ux-icon="&#xe01a;" title="' + labels.close + '" aria-hidden="true" />'
            },
            title: null,
            html: ''
        };

        $.extend(true, s, settings);

        this.element = ko.observable();
        this.html = ko.isObservable(s.html) ? s.html : ko.observable(s.html);
        this.options = ko.isObservable(s.options) ? s.options : ko.observable(s.options);
        this.title = ko.isObservable(s.title) ? s.title : ko.observable(s.title);
    }

    $('head').append('<script type="text/html" id="ux-dialog">' + template + '</script>');

    ko.bindingHandlers.uxDialog = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            viewModel.element(element);

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).dialog('destroy');
            });
        },
        update: function (element, valueAccessor) {
            var options = valueAccessor() || {},
                dialogClass = 'ektron-ux-dialog';

            element = $(element);

            if (element.is('ui-dialog-content')) {
                element.dialog('destroy');
            }


            if (options().appendTo === 'body' || options().appendTo === '') {
                dialogClass = 'ektron-ux-reset ektron-ux-dialog';
            }

            element.dialog(options()).closest('.ui-dialog').addClass(dialogClass);
        }
    };

    return Dialog;
});
