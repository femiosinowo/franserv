define(['ektronjs', 'UX/js/toolbar.button', 'Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping', '_css!Vendor/Ektron/UX/css/ux.utility.css'], function ($, ToolbarButton, ko) {
    'use strict';

    function ToolbarPopOver(id) {
        ToolbarButton.call(this, id);

        this.dropbox = {
            visible: ko.observable(false),
            top: ko.observable('50px')
        };
        this.baseCssClass = 'ux-button ux-popover';
        this.template = ko.observable('ux.toolbar.popover');

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
    }

    ToolbarPopOver.prototype = new ToolbarButton();
    ToolbarPopOver.prototype.constructor = ToolbarPopOver;
    ToolbarPopOver.prototype.click = function () { };
    ToolbarPopOver.prototype.fire = function () {
        if (!this.options.disabled()) {
            this.click();
            return true;
        }
    };

    ToolbarPopOver.prototype.toggleDropbox = function () {
        this.dropbox.visible(!this.dropbox.visible());
    };

    return ToolbarPopOver;
});
