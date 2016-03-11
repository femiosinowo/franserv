/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.button.js" />

var ToolbarButton, button, ektronjs;

module('toolbar button class', {
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
        ektronjs.trim = function (str) {
            return str.trim ? str.trim() : str.replace(/^[\s\xA0]+/, '').replace(/[\s\xA0]+$/, '');
        };

        ToolbarButton = window.createModule(ektronjs, ko);
        button = new ToolbarButton('buttonName');
        button.options.text('Button');
        button.options.icon('');
        button.options.title('Button Title');
    }
});

test('button is found', function () {
    'use strict';
    ok('undefined' !== typeof button, 'button is found');
});

test('button has an id', function () {
    'use strict';
    equal(button.id.length > 0, true, 'button has an id');
});

test('options text return correct default button text', function () {
    'use strict';
    ok(button.options.text() === 'Button', 'button text is default to "Button"');
});

test('options text return correct button text', function () {
    'use strict';
    button.options.text('abc');
    ok(button.options.text() === 'abc', 'button text is default to "abc"');
});

test('options icon return correct default icon value', function () {
    'use strict';
    ok(button.options.icon() === '', 'button icon is default to null');
});

test('getIcon returns the correct values', function () {
    'use strict';
    ok(button.getIcon() === 'none', 'button getIcon returns "none" if no value is set.');
    button.options.icon('test.png');
    ok(button.getIcon() === 'url(test.png)', 'button getIcon returns expected "url(test.png)" value.');
});

test('getClass returns the correct values', function () {
    'use strict';
    button.options.text('');
    equal(button.getClass(), 'applicationControl ux-button', 'button getClass returns "button" if no value is set.');
    button.options.text('Text');
    equal(button.getClass(), 'applicationControl ux-button hasText', 'button getClass returns correct class when only options text is set.');
    button.options.icon('test.png');
    equal(button.getClass(), 'applicationControl ux-button hasText hasIcon', 'button getClass returns correct class when options icon and text are set');
    button.options.text('');
    equal(button.getClass(), 'applicationControl ux-button hasIcon', 'button getClass returns correct class when only options icon is set');
});

test('button click is fired', function () {
    'use strict';
    var buttonClickCount = 0;
    
    button.click = function () {
        buttonClickCount++; //ignore jslint
    };
    button.fire();

    ok(buttonClickCount === 1, 'button click event is fired once.');
});

test('button click is not fired when button is disabled', function () {
    'use strict';
    var buttonClickCount = 0;
    button.options.disabled(true);
    button.click = function () {
        buttonClickCount++; //ignore jslint
    };
    button.fire();
    ok(buttonClickCount === 0, 'button click event is not fired.');
});

test('default button template is button', function () {
    'use strict';
    equal(button.template(), 'ux.toolbar.button', 'found correct template');
});

test('default id is blank', function () {
    'use strict';

    button = new ToolbarButton();

    equal(button.id, '', 'id is blank');
});

test('button accepts options containing id', function () {
    'use strict';

    button = new ToolbarButton({
        id: 'buttonID'
    });

    equal(button.id, 'buttonID', 'id is set from options');
});

test('button passes click function through settings to object instance', function () {
    'use strict';
    var clicked = false;

    button = new ToolbarButton({
        click: function () { clicked = true; }
    });

    button.click();

    ok(clicked, 'button passed click from settings into button object');
});