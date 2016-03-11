define(['ektronjs', 'UX/js/toolbar.controlBase', 'UX/js/toolbar.button', 'UX/js/constants', 'Vendor/knockout/knockout', 'Vendor/knockout/knockout-mapping'], function ($, ToolbarControl, ToolbarButton, constants, ko) {
    'use strict';

    function ToolbarButtonSet(options) {
        this.Types = constants.ButtonTypes;

        var me = this,
            defaultOptions = {
                disabled: false,
                type: me.Types.button,
                buttons: []
            },
            x = 0;

        $.extend(defaultOptions, options);

        ToolbarControl.call(this, options);
        this.baseCssClass = 'ux-buttonset';
        this.options = ko.mapping.fromJS(defaultOptions);
        this.buttons = ko.observableArray();
        this.template = ko.observable('ux.toolbar.buttonset');
        this.type = ko.computed(function () {
            var result = me.Types.button;
            switch (me.options.type()) {
                /*ignore jslint start*/
                case me.Types.radio:
                    result = me.Types.radio; //jsLint ignore
                    break;
                case me.Types.checkbox:
                    result = me.Types.checkbox;
                    break;
                case me.Types.button:
                    result = me.Types.button;
                    break;
                    /*ignore jslint end*/
            }
            return result;
        });
        this.isVisible = ko.computed({
            read: function () {
                var visible = false,
                    buttons = ko.utils.unwrapObservable(me.buttons),
                    i = 0;
                for(i; i < buttons.length; i = i + 1) {
                    if (buttons[i].isVisible()) {
                        visible = true;
                        break;
                    }
                }
                return visible;
            },
            write: function (value) {
                var buttons = ko.utils.unwrapObservable(me.buttons), i = 0;
                for (i; i < buttons.length; i = i + 1) {
                    buttons[i].isVisible(value);
                }
            }
        });
        this.disabled = ko.observable();
        this.disabled.subscribe(function (newValue) {
            x = 0;
            for (x; x < me.buttons().length; x += 1) {
                me.buttons()[x].disabled(newValue);
            }
        });
        this.val = (me.type() === 'checkbox') ? ko.observableArray() : ko.observable();

        // recast buttons array items as ToolbarButtons is they aren't already
        if (options.buttons) {
            for (x; x < options.buttons.length; x += 1) {
                this.addButton(options.buttons[x]);
            }
        }
        else {
            this.addButton({});
        }

        // if options.disabled is set, set the disabled property of the control
        if (me.options.disabled) {
            this.disabled(me.options.disabled ? me.options.disabled() : false);
        }

        // set the initial val() based on the checked state of the buttons in the buttons array
        switch (me.type()) {
            /*ignore jslint start*/
            case me.Types.radio:
                x = 0;
                for (x; x < me.buttons().length; x += 1) {
                    if (me.buttons()[x].checked()) {
                        me.val(me.buttons()[x].value());
                    }
                }
                break;
            case me.Types.checkbox:
                x = 0;
                for (x; x < me.buttons().length; x += 1) {
                    if (me.buttons()[x].checked()) {
                        this.val.push(me.buttons()[x].value());
                    }
                }
                break;
            default:
                me.val('');
                break;
                /*ignore jslint end*/
        }
    }
    ToolbarButtonSet.prototype = new ToolbarControl();
    ToolbarButtonSet.prototype.constructor = ToolbarButtonSet;
    ToolbarButtonSet.prototype.getClass = function () {
        return ToolbarControl.prototype.getClass.call(this);
    };
    ToolbarButtonSet.prototype.addButton = function (button) {
        if (button.constructor.name !== 'ToolbarButton') {
            button = new ToolbarButton(button);
        }
        this.buttons.push(button);
    };

    return ToolbarButtonSet;
});