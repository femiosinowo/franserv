define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_text!Vendor/Ektron/UX/views/widgets/ux.selectmenu.html',
    'Vendor/jQuery/UI/js/jquery-ui-complete.min',
    '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min'
], function ($, ko, template) {
    'use strict';

    function SelectionGroup(name, options, disabled) {
        var me = this;

        me.name = name;
        me.options = options;
        me.disabled = (true === disabled);
    }

    function SelectionOption(value, text, disabled) {
        var me = this,
            index = 0;

        me.value = value;
        me.text = value;
        me.disabled = (true === disabled);

        if (arguments.length > 1) {
            me.text = text;
        }
    }

    function SelectMenu(settings) {
        var me = this,
            s,
            initialValue;

        s = {
            options: {
                popup: false,
                appendTo: '.ektron-ux .ux-appWindow'
            },
            contents: [],
            selectedValue: ''
        };

        $.extend(true, s, settings);

        this.contents = ko.isObservable(s.contents) ? s.contents : ko.observableArray(s.contents);
        this.options = ko.isObservable(s.options) ? s.options : ko.observable(s.options);
        this.selectedValue = ko.isObservable(s.selectedValue) ? s.selectedValue : ko.observable(s.selectedValue);
        this.element = ko.observable();

        if (!me.selectedValue() && me.contents().length > 0) {
            initialValue = me.contents()[0].value ? me.contents()[0].value : (me.contents()[0]).options[0].value;
            me.selectedValue(initialValue);
        }
    }

    SelectMenu.SelectionOption = SelectionOption;
    SelectMenu.SelectionGroup = SelectionGroup;

    $('head').append('<script type="text/html" id="ux-selectmenu">' + template + '</script>');

    ko.bindingHandlers.uxSelectMenu = {
        init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            viewModel.element(element);

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $(element).selectmenu('destroy').off('change.uxSelectMenu');
            });
        },
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            var options = valueAccessor() || {};

            element = $(element);

            element.selectmenu(options());

            element.off('change.uxSelectMenu').on('change.uxSelectMenu', function () { viewModel.selectedValue(this.value); });
        }
    };

    return SelectMenu;
});