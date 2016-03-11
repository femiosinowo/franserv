define(['UX/js/toolbar.button', 'UX/js/toolbar.popover', 'UX/js/toolbar.switch', 'UX/js/toolbar.buttonset', 'Vendor/knockout/knockout'], function (ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko) {
    'use strict';

    function Toolbar(resourceLoader) {
        this.controls = ko.observableArray();
        this.resourceLoader = resourceLoader;

        var body = document.querySelector('body'),
            bodyClass = body.getAttribute('class') ? body.getAttribute('class') : '',
            showToolbar = 'ektron-ux-showToolbar';

        body.setAttribute('class', bodyClass.length > 0 ? bodyClass + ' ' + showToolbar : showToolbar);
    }

    Toolbar.prototype._generateId = function (item) {
        var id = item.id;
        if ('undefined' === typeof (item.id) || item.id === '') {
            id = 'ektron-ux-' + item.constructor.name + this.controls().length;
        }
        return id;
    };

    Toolbar.prototype.addButton = function (button) {
        if (!button.constructor || button.constructor.name !== 'ToolbarButton') {
            var toolbarButton = new ToolbarButton(button);
            button = toolbarButton;
        }

        button.id = this._generateId(button);
        this.controls.push(button);
        return this;
    };

    Toolbar.prototype.addPopOver = function (popover) {
        if (!popover.constructor || popover.constructor.name !== 'ToolbarPopOver') {
            var toolbarPopOver = new ToolbarPopOver(popover);
            popover = toolbarPopOver;
        }

        popover.id = this._generateId(popover);
        this.controls.push(popover);
        return this;
    };

    Toolbar.prototype.addSwitch = function (switchObj) {
        if (!switchObj.constructor || switchObj.constructor.name !== 'ToolbarSwitch') {
            var toolbarSwitch = new ToolbarSwitch(switchObj);
            switchObj = toolbarSwitch;
        }

        switchObj.id = this._generateId(switchObj);
        this.controls.push(switchObj);
        return this;
    };

    Toolbar.prototype.addButtonset = function (buttonset) {
        if (!buttonset.constructor || buttonset.constructor.name !== 'ToolbarButtonSet') {
            var toolbarButtonSet = new ToolbarButtonSet(buttonset);
            buttonset = toolbarButtonSet;
        }

        buttonset.id = this._generateId(buttonset);
        this.controls.push(buttonset);
        return this;
    };

    Toolbar.prototype.load = function (callback) {
        var me = this;
        this.resourceLoader.load({
            files: [
                'ux.toolbar.button',
                'ux.toolbar.popover',
                'ux.toolbar.switch',
                'ux.toolbar.buttonset'
            ],
            type: me.resourceLoader.Types.html,
            reusable: true,
            callback: function () {
                if ('function' === typeof (callback)) {
                    callback();
                }
            }
        });
        return this;
    };

    Toolbar.prototype.clear = function () {
        this.controls.removeAll();
        return this;
    };

    return Toolbar;
});