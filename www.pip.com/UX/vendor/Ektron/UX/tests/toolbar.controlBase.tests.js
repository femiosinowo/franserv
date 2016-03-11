/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.controlBase.js" />

var ControlBase, control, ektronjs;

function ektronjs() { }

module('toolbar control base class', {
    setup: function () {
        'use strict';

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
        ektronjs.trim = function (result) { return result.trim(); }

        ControlBase = window.createModule(ektronjs, ko);
        control = new ControlBase('controlName');
    }
});

test('control base is found', function () {
    'use strict';
    ok('undefined' !== typeof control, 'control base is found');
});

test('control has an id when constructor called with single string parameter passed.', function () {
    'use strict';
    equal(control.id.length > 0, true, 'control has an id');
});

test('default id is blank', function () {
    'use strict';

    control = new ControlBase();
    equal(control.id, '', 'id is blank');
});

test('control accepts options containing id', function () {
    'use strict';

    control = new ControlBase({
        id: 'controlId'
    });

    equal(control.id, 'controlId', 'id is set from options');
});

test('getClass returns the correct values', function () {
    'use strict';
    equal(control.getClass(), 'applicationControl', 'control getClass returns "applicationControl" by default.');
    control = new ControlBase({
        cssClass: 'test'
    });
    equal(control.getClass(), 'applicationControl test', 'control getClass returns "applicationControl test" when options.cssClass = "test".');

    control = new ControlBase({
        cssClass: 'test',
        floatRight: true
    });
    equal(control.getClass(), 'applicationControl test ux-floatRight', 'control getClass returns "applicationControl test ux-floatRight" when options.cssClass = "test".');
});

test('control click is fired', function () {
    'use strict';
    var controlClickCount = 0;

    control.click = function () {
        controlClickCount++; //ignore jslint
    };
    control.fire();

    ok(controlClickCount === 1, 'control click event is fired once.');
});

test('control click is not fired when control is disabled', function () {
    'use strict';
    var controlClickCount = 0;
    control.options.disabled(true);
    control.click = function () {
        controlClickCount++; //ignore jslint
    };
    control.fire();
    ok(controlClickCount === 0, 'control click event is not fired.');
});