/// <reference path="../../../../knockout/knockout.js" />
/// <reference path="../../../../knockout/knockout-mapping.js" />
/// <reference path="../../../../jQuery/jquery.min.js" />
/// <reference path="../../../ektron.js" />
/// <reference path="../../../../jQuery/UI/js/jquery-ui-complete.min.js" />
/// <reference path="../../../../fakequery/fake-query-0.2.js" />
/// <reference path="../setup.js" />

/// <reference path="../../js/widgets/ux.accordion.js" />

var Accordion, accordion, $$, $ektron;

module('UX Accordion', {
    setup: function () {
        'use strict';
        ////var $ektron = window.$ektron,
        $$ = window.$$;
        $ektron = $ = window.$ = window.$ektron;

        Accordion = window.createModule($ektron, ko, '');
    }
});

test('Loads template', function () {
    'use strict';
    var template = 'template wuz here';

    // start with a fresh fakeQuery
    $$.reset();

    Accordion = window.createModule(window.$ektron, ko, template);

    // stop recording calls now
    $$.stopRecording();

    ok($$.wasCalled($ektron('head')), 'referenced head');
    equal($$.getCallCount('append'), 1, 'append was called on the head');
    ok($$.lastCallTo.append.argument.toString().indexOf(template) > 0, 'template was inserted into the head');
});

test('Triggering binding handler update for options', function () {
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

        selected.is = function () {
            return false;
        };

        selected.closest = selected.addClass = function () {
            return selected;
        };

        selected.accordion = function (options) {
            foundOptions = options;
            return selected;
        };

        selected.off = selected.on = function () { return selected; };

        return selected;
    };

    Accordion = window.createModule($mock, ko, '');

    ko.bindingHandlers.uxAccordion.update(elem, mockValueAccessor);

    equal(foundOptions, mockOptions, 'Mock options object used when calling accordion plugin');
});

test('Accordion constructor checks if panelArray is actually an array of Panels.', function () {
    'use strict';

    var settings = {
        options: {},
        panelsArray: [{}]
    }
    // start with a fresh fakeQuery
    $$.reset();

    // mock extend method
    $.extend = function () {
        arguments[1] = settings;
        return settings;
    }

    accordion = new Accordion(settings);

    // stop recording calls now
    $$.stopRecording();
    
    equal((accordion.panels()[0]).constructor.name, 'Panel', 'object passed to Accordion constructor was converted to Panel object.');
});

test('Triggering binding handler update for options', function () {
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

        selected.is = function () {
            return false;
        };

        selected.closest = selected.addClass = function () {
            return selected;
        };

        selected.accordion = function (options) {
            foundOptions = options;
            return selected;
        };

        selected.off = selected.on = function () { return selected; };

        return selected;
    };

    Accordion = window.createModule($mock, ko, '');

    ko.bindingHandlers.uxAccordion.update(elem, mockValueAccessor);

    equal(foundOptions, mockOptions, 'Mock options object used when calling dialog plugin');
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

        selected.closest = selected.addClass = function () {
            return selected;
        };

        selected.accordion = function (options) {
            foundOptions = options;
        };

        return selected;
    };

    Accordion = window.createModule($mock, ko, '');
    ko.bindingHandlers.uxAccordion.init(elem, mockValueAccessor, null, mockViewModel);

    equal(returnedElement, elem, '.init() method of the binding handler properly sets the view model\'s element observable.');
});