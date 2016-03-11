/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.controlBase.js" />
/// <reference path="../js/toolbar.button.js" />
/// <reference path="../js/constants.js" />
/// <reference path="../js/toolbar.buttonSet.js" />

var ToolbarButtonSet, buttonset, ToolbarControl, ToolbarButton, ektronjs, constants;

module('toolbar buttonset class', {
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

        ToolbarControl = window.modules[0](ektronjs, ko);
        ToolbarButton = window.modules[1](ektronjs, ko);
        constants = {
            ButtonTypes: {
                checkbox: 'checkbox',
                radio: 'radio',
                button: 'button'
            }
        };
        ToolbarButtonSet = window.createModule(ektronjs, ToolbarControl, ToolbarButton, constants, ko);
        buttonset = new ToolbarButtonSet('buttonset');
    }
});
test('buttonset is found', function () {
    'use strict';
    ok('undefined' !== typeof buttonset, 'buttonset is found');
});

test('buttonset has an id', function () {
    'use strict';
    equal(buttonset.id, 'buttonset', 'buttonset has an id');
});

test('buttonset types', function () {
    'use strict';
    equal(buttonset.type(), 'button', 'buttonset default type is "button"');

    buttonset = new ToolbarButtonSet({
        type: 'radio'
    });

    equal(buttonset.type(), 'radio', 'buttonset returns type "radio" when the options.type property is set to "radio"');

    buttonset = new ToolbarButtonSet({
        type: 'checkbox'
    });

    equal(buttonset.type(), 'checkbox', 'buttonset returns type "checkbox" when the options.type property is set to "checkbox"');

    buttonset = new ToolbarButtonSet({
        type: 'garbage'
    });

    equal(buttonset.type(), 'button', 'buttonset returns type "button" when the options.type property is not one of the known types');
});

test('getClass returns the correct values', function () {
    'use strict';
    equal(buttonset.getClass(), 'applicationControl ux-buttonset', 'buttonset getClass returns "applicationControl buttonset" if no value is set.');

    buttonset = new ToolbarButtonSet({
        cssClass: 'test',
        floatRight: true
    });

    equal(buttonset.getClass(), 'applicationControl ux-buttonset test ux-floatRight', 'buttonset getClass returns correct class when a custom class is provided.');

    buttonset = new ToolbarButtonSet({
        floatRight: true
    });

    equal(buttonset.getClass(), 'applicationControl ux-buttonset ux-floatRight', 'buttonset getClass returns correct class when floatRight property is set to true.');
});

test('buttonset buttons array contains button objects', function () {
    'use strict';
    var buttonsArray = buttonset.buttons();

    ok(Array.isArray(buttonsArray), 'buttonset.buttons is an array.');
    equal(buttonsArray.length, 1, 'buttonset.buttons contains a single button by default.');
});

test('buttonset buttons array has the correct objects passed in.', function () {
    'use strict';
    var options = {
            buttons: [new ToolbarButton(), new ToolbarButton(), new ToolbarButton()]
        },
        buttonsArray;

    buttonset = new ToolbarButtonSet(options);

    buttonsArray = buttonset.buttons();

    equal(buttonsArray.length, 3, 'buttonset.buttons contains a single button by default.');
});