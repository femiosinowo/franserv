/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.button.js" />
/// <reference path="../js/toolbar.popover.js" />

var ToolbarPopOver, popover, ToolbarButton, ektronjs;

module('toolbar popover class', {
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

        ToolbarButton = window.modules[0](ektronjs);
        ToolbarPopOver = window.createModule(ektronjs, ToolbarButton, ko);
        popover = new ToolbarPopOver('popoverName'); 
    }
});

test('popover is found', function () {
    'use strict';
    ok('undefined' !== typeof popover, 'popover is found');
});

test('options text return correct default button text', function () {
    'use strict';
    ok(popover.options.text() === 'Button', 'popover text is default to "Button"');
});

test('getClass returns the correct values', function () {
    'use strict';
    popover.options.text('');
    equal(popover.getClass(), 'applicationControl button popover', 'button getClass returns "button" if no value is set.');
    popover.options.text('Text');
    equal(popover.getClass(), 'applicationControl button popover hasText', 'button getClass returns correct class when only options text is set.');
    popover.options.icon('test.png');
    equal(popover.getClass(), 'applicationControl button popover hasText hasIcon', 'button getClass returns correct class when options icon and text are set');
    popover.options.text('');
    equal(popover.getClass(), 'applicationControl button popover hasIcon', 'button getClass returns correct class when only options icon is set');
});

test('popover has an id', function () {
    'use strict';
    equal(popover.id.length > 0, true, 'popover has an id');
});

test('default popover template is popover', function () {
    'use strict';
    equal(popover.template(), 'ux.toolbar.popover', 'found correct template');
});

test('popover box by default is not visible', function () {
    'use strict';
    equal(popover.dropbox.visible(), false, 'popover box by default is not visible');
});

test('popover box can be set visible', function () {
    'use strict';
    popover.dropbox.visible(true);
    equal(popover.dropbox.visible(), true, 'popover box is set to visible');
});

test('popover click is fired and show the dropbox.', function () {
    'use strict';
    var buttonClickCount = 0;
    
    popover.dropbox.visible(false);
    popover.click = function () {
        buttonClickCount++; //ignore jslint
        popover.dropbox.visible(true);
    };
    popover.fire();

    ok(buttonClickCount === 1, 'button click event is fired.');
    equal(popover.dropbox.visible(), true, 'popover click event by default makes dropbox visible.');
});

test('popover is clicked twice and hide the dropbox.', function () {
    'use strict';
    var buttonClickCount = 0;

    popover.dropbox.visible(false);
    popover.click = function () {
        buttonClickCount++; //ignore jslint
        popover.toggleDropbox();
    };
    popover.fire();
    equal(popover.dropbox.visible(), true, 'popover clicked once would show dropbox.');

    popover.fire();
    ok(buttonClickCount === 2, 'button click event is fired twice.');
    equal(popover.dropbox.visible(), false, 'popover clicked twice would hide dropbox.');
});

test('template loaded in the dropbox after popover click is fired once.', function () {
    'use strict';
    var buttonClickCount = 0;

    popover.dropbox.visible(false);
    popover.template('<ul><li>sample content</li></ul>');
    popover.click = function () {
        buttonClickCount++; //ignore jslint
        popover.toggleDropbox();
    };
    popover.fire();

    ok(buttonClickCount === 1, 'button click event is fired.');
    equal(popover.template().indexOf('<ul>') > -1, true, 'popover click once and template loaded.');
});

test('default id is blank', function () {
    'use strict';

    popover = new ToolbarPopOver();

    equal(popover.id, '', 'id is blank');
});

test('popover accepts options containing id', function () {
    'use strict';

    popover = new ToolbarPopOver({
        id: 'popoverID'
    });

    equal(popover.id, 'popoverID', 'id is set from options');
});

test('popover passes click function through settings to object instance', function () {
    'use strict';
    var clicked = false;

    popover = new ToolbarPopOver({
        click: function () { clicked = true; }
    });

    popover.click();

    ok(clicked, 'popover passed click from settings into popover object');
});

