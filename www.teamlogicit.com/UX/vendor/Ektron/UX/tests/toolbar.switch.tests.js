/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.controlBase.js" />
/// <reference path="../js/toolbar.switch.js" />

var ToolbarSwitch, toolbarSwitch, ToolbarControl, ektronjs;

module('toolbar switch class', {
    setup: function () {
        'use strict';

        ektronjs = function () { };
        ektronjs.extend = function () {
            var argumentsLength = arguments.length, key, i;
            for (i = 1; i < argumentsLength; i++) { //ignore jslint
                for (key in arguments[i]) {
                    if (arguments[i].hasOwnProperty(key)) {
                        arguments[0][key] = arguments[i][key];
                    }
                }
            }

            return arguments[0];
        };
        ektronjs.trim = String.trim ? String.trim : function (str) {
            str = str.replace(/^\s+/, '');
            for (var i = str.length - 1; i >= 0; i--) { //ignore jslint
                if (/\S/.test(str.charAt(i))) {
                    str = str.substring(0, i + 1);
                    break;
                }
            }
            return str;
        };

        ToolbarControl = window.modules[0](ektronjs, ko, ko.mapping);
        ToolbarSwitch = window.createModule(ektronjs, ToolbarControl, ko);
        toolbarSwitch = new ToolbarSwitch('switchName');
    }
});

test('switch is found', function () {
    'use strict';
    ok('undefined' !== typeof toolbarSwitch, 'switch is found');
});

test('switch has an id', function () {
    'use strict';
    equal(toolbarSwitch.id.length > 0, true, 'toolbarSwitch has an id');
});

test('default switch template is switch', function () {
    'use strict';
    equal(toolbarSwitch.template(), 'ux.toolbar.switch', 'found correct template');
});

test('getClass returns the correct values', function () {
    'use strict';
    equal(toolbarSwitch.getClass(), 'applicationControl ux-switch', 'toolbarSwitch getClass returns "applicationControl switch" if no value is set.');
    toolbarSwitch = new ToolbarSwitch({
        cssClass: 'test'
    });
    equal(toolbarSwitch.getClass(), 'applicationControl ux-switch test', 'toolbarSwitch getClass returns "applicationControl ux-switch test" when options.cssClass is set to "test".');
});

test('switch.checked is false by default', function () {
    'use strict';
    equal(toolbarSwitch.checked(), false, 'toolbarSwitch.checked() is false by default');
});

test('switch click is fired on click, and .val() method returns the correct value for the control.', function () {
    'use strict';
    var switchClickCount = 0, currentVal = '';

    toolbarSwitch = new ToolbarSwitch();
    currentVal = toolbarSwitch.val();
    equal(currentVal, '', '.val() returns "" value by default (no onLabel or offLabel options set)');

    toolbarSwitch.click = function () {
        switchClickCount++; //ignore jslint
    };
    toolbarSwitch.fire();
    console.log(toolbarSwitch.checked());
    ok(switchClickCount === 1, 'toolbarSwitch click event is fired.');
    currentVal = toolbarSwitch.val();
    equal(currentVal, 'on', '.val() returns "on" when checked and no onLabel is specified.');

    toolbarSwitch = new ToolbarSwitch({
        onLabel: 'On',
        offLabel: 'Off'
    });
    toolbarSwitch.click = function () {
        switchClickCount++; //ignore jslint
    };
    toolbarSwitch.fire();
    ok(switchClickCount === 2, 'toolbarSwitch click event is fired.');

    currentVal = toolbarSwitch.val();
    equal(currentVal, 'On', '.val() returns the onValue after first click');

    toolbarSwitch.fire();
    ok(switchClickCount === 3, 'toolbarSwitch click event is fired for second time.');

    currentVal = toolbarSwitch.val();
    equal(currentVal, '', '.val() returns "" when clicked a second time.');
});

test('switch passes click function through settings to object instance', function () {
    'use strict';
    var switchClickCount = 0;

    toolbarSwitch = new ToolbarSwitch({
        click: function () {
            switchClickCount++; //ignore jslint
        }
    });

    toolbarSwitch.click();

    equal(switchClickCount, 1, 'switch passed click from settings into button object');
});