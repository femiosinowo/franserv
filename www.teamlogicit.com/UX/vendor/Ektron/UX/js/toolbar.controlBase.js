define(['ektronjs', 'Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping', '_css!Vendor/Ektron/UX/css/ux.utility.css'], function ($, ko, mapping) {
    'use strict';
    if ('undefined' === typeof (ko.mapping)) { 
        ko.mapping = mapping;
    }
    function ControlBase(settings) {
        var defaultSettings = {
            disabled: false,
            title: ''
        },
            me = this;

        if ('undefined' === typeof (settings) || 'string' === typeof (settings)) {
            settings = {
                id: settings
            };
        }

        $.extend(defaultSettings, settings);

        this.id = 'undefined' === typeof (defaultSettings.id) ? '' : defaultSettings.id;
        this.baseCssClass = '';
        this.options = ko.mapping.fromJS(defaultSettings);
    }

    ControlBase.prototype.getClass = function () {
        var result = 'applicationControl ' + this.baseCssClass + ' ';
        if (this.options.cssClass) {
            result = $.trim(result) + ' ' + this.options.cssClass() + ' ';
        }
        if (this.options.floatRight && this.options.floatRight() === true) {
            result += 'ux-floatRight ';
        }
        return $.trim(result);
    };
    ControlBase.prototype.click = function () { };
    ControlBase.prototype.fire = function () {
        if (!this.options.disabled()) {
            this.click();
            return true;
        }        
    };
    
    return ControlBase;
});