/// <reference path="../../../../knockout/knockout.js" />
/// <reference path="../../../../knockout/knockout-mapping.js" />
/// <reference path="../../../../jQuery/jquery.min.js" />
/// <reference path="../../../ektron.js" />
/// <reference path="../../../../jQuery/UI/js/jquery-ui-complete.min.js" />
/// <reference path="../../../../jQuery/UI/js/jquery-ui-selectmenu.js" />
/// <reference path="../../../../jQuery/UI/js/jquery-ui-selectmenu.popupStyle.js" />
/// <reference path="../setup.js" />
/// <reference path="../../../../fakequery/fake-query-0.2.js" />
/// <reference path="../../js/widgets/ux.selectmenu.js" />
var SelectMenu, selectMenu, $$, $ektron;

module('UX SelectMenu', {
    setup: function () {
        'use strict';

        ////var $ektron = window.$ektron,
        $$ = window.$$;
        $ektron = window.$ektron;

        // Backup mocked-over jQuery
        if ('undefined' === window.$data) {
            window.$data = window.$.data;
        }

        // Restore mocked-over jQuery
        window.$.data = window.$data;

        SelectMenu = window.createModule(window.$ektron, ko, '');
    }
});

test('Loads template', function () {
    'use strict';
    var template = 'template wuz here';

    // start with a fresh fakeQuery
    $$.reset();

    SelectMenu = window.createModule(window.$ektron, ko, template);

    // stop recording calls now
    $$.stopRecording();

    ok($$.wasCalled($ektron('head')), 'referenced head');
    equal($$.getCallCount('append'), 1, 'append was called on the head');
    ok($$.lastCallTo.append.argument.toString().indexOf(template) > 0, 'template was inserted into the head');
});

test('Triggering binding handler for options', function () {
    'use strict';
    var elem = '<div />',
        mockOptions = { testProp: true },
        mockValueAccessor = function () {
            return function () { return mockOptions; };
        },
        $mock,
        foundOptions;

    $mock = function () {
        var selected = {};

        selected.append = function () { };

        selected.data = function () {
            return undefined;
        };

        selected.selectmenu = function (options) {
            foundOptions = options;
        };

        selected.off = selected.on = function () { return selected; };

        return selected;
    };

    SelectMenu = window.createModule($mock, ko, '');

    ko.bindingHandlers.uxSelectMenu.update(elem, mockValueAccessor);

    equal(foundOptions, mockOptions, 'Mock options object used when calling selectmenu plugin');
});

test('Triggering binding handler init', function () {
    'use strict';
    var elem = '<div />',
        mockOptions = { testProp: true },
        mockValueAccessor = function () {
            return function () { return mockOptions; };
        },
        $mock,
        foundOptions,
        returnedElement,
        mockViewModel = {
            element: function (data) {
                returnedElement = data;
            }
        };

    $mock = function () {
        var selected = {};

        selected.append = function () { };

        selected.data = function () {
            return undefined;
        };

        selected.selectmenu = function (options) {
            foundOptions = options;
        };

        return selected;
    };

    SelectMenu = window.createModule($mock, ko, '');
    ko.bindingHandlers.uxSelectMenu.init(elem, mockValueAccessor, null, mockViewModel);

    equal(returnedElement, elem, '.init() method of the binding handler properly sets the view model\'s element observable.');
});

test('Binding update destroys and recreates selectmenu', function () {
    'use strict';
    var $mock,
        destroyCount = 0,
        elem = '<span />',
        mockOptions = { testProp: true },
        mockValueAccessor = function () {
            return function () { return mockOptions; };
        };

    $mock = function () {
        var selected = {};

        selected.append = function () { };

        selected.data = function (item) {
            if (item === 'uiSelectmenu') {
                return {};
            }

            return undefined;
        };

        selected.selectmenu = function (args) {
            if (args === 'destroy') {
                destroyCount++; //ignore jslint
            }
        };

        selected.off = selected.on = function () { return selected; };

        return selected;
    };

    SelectMenu = window.createModule($mock, ko, '');
    ko.bindingHandlers.uxSelectMenu.update(elem, mockValueAccessor);

    equal(destroyCount, 1, 'destroy called on subsequent update calls');
});