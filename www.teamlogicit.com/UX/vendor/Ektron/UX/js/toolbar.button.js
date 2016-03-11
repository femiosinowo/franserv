define(['ektronjs', 'Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping', '_css!Vendor/Ektron/UX/css/ux.utility.css'], function ($, ko, mapping) {
    'use strict';

    if ('undefined' === typeof (ko.mapping)) {
        ko.mapping = mapping;
    }

    function ToolbarButton(settings) {
        var me = this,
            defaultSettings = {
                disabled: false,
                title: '',
                text: 'Button',
                icon: '',
                checked: false
            };

        if ('undefined' === typeof (settings) || 'string' === typeof (settings)) {
            settings = {
                id: settings
            };
        }

        $.extend(defaultSettings, settings);

        this.id = 'undefined' === typeof (defaultSettings.id) ? '' : defaultSettings.id;
        this.options = ko.mapping.fromJS(defaultSettings);
        this.value = ko.observable(me.options.value ? me.options.value() : me.id);
        this.baseCssClass = 'ux-button';
        this.checked = ko.observable(me.options.checked ? me.options.checked() : false);
        this.isVisible = ko.observable(me.options.isVisible ? me.options.isVisible() : true);
        this.disabled = ko.observable(me.options.disabled ? me.options.disabled() : true);
        this.template = ko.observable('ux.toolbar.button');
        this.getIcon = ko.computed(function () {
            if (this.options.icon().length > 0) {
                return 'url(' + this.options.icon() + ')';
            }
            return 'none';
        }, this);
        this.getClass = ko.computed(function () {
            var result = 'applicationControl ' + this.baseCssClass + ' ';
            if (this.options.text().length > 0) {
                result += 'hasText ';
            }
            if (this.options.icon().length > 0) {
                result += 'hasIcon ';
            }
            if (this.options.floatRight && this.options.floatRight() === true) {
                result += 'ux-floatRight ';
            }
            return $.trim(result);
        }, this);

        if ('function' === typeof (defaultSettings.click)) {
            this.click = defaultSettings.click;
        }
    }

    ToolbarButton.prototype.click = function () { };
    ToolbarButton.prototype.fire = function () {
        if (!this.options.disabled()) {
            this.click();
            return true;
        }
    };

    return ToolbarButton;
});
