define(['ektronjs', 'UX/js/toolbar.controlBase', 'Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping'], function ($, ToolbarControl, ko) {
    'use strict';

    function ToolbarSwitch(options) {
        var me = this;

        ToolbarControl.call(this, options);

        this.checked = ko.observable(this.options.checked === true ? true : false);
        this.checked.subscribe(function (newValue) {
            me.fire();
        });
        this.baseCssClass = 'ux-switch';
        this.isVisible = ko.observable(this.options.isVisible ? this.options.isVisible() : true);
        this.disabled = ko.observable(this.options.disabled ? this.options.disabled() : false);
        this.template = ko.observable('ux.toolbar.switch');
        // methods
        this.val = ko.computed(function () {
            if (me.checked() && me.options.onLabel) {
                return me.options.onLabel();
            }
            else if (me.checked()) {
                return 'on';
            }

            return '';
        }, this);

        if ('function' === typeof (me.options.click)) {
            this.click = me.options.click;
        }
    }

    ToolbarSwitch.prototype.getClass = function () {
        return ToolbarControl.prototype.getClass.call(this);
    };
    ToolbarSwitch.prototype = new ToolbarControl();
    ToolbarSwitch.prototype.constructor = ToolbarSwitch;
    ToolbarSwitch.prototype.click = function () { };
    ToolbarSwitch.prototype.fire = function () {
        if (!this.options.disabled()) {
            this.click();
            return true;
        }
    };
    ToolbarSwitch.prototype.afterRender = function (element) {
        var toolbarSwitch = $(element),
            onWidth = toolbarSwitch.find('label:first-child > span').outerWidth(true),
            handleWidth = toolbarSwitch.find('label.ux-switch-slider > span').outerWidth(true),
            offWidth = toolbarSwitch.find('label:last-child > span').outerWidth(true);

        toolbarSwitch.width(Math.max(onWidth, offWidth) + handleWidth + 'px');
    };

    return ToolbarSwitch;
});