/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../jQuery/jquery.min.js" />
/// <reference path="../../../jQuery/jquery.migrate.js" />
/// <reference path="../../ektron.js" />
/// <reference path="../../../jQuery/UI/js/jquery-ui-complete.min.js" />
/// <reference path="../js/koBindingHandlers.js" />

var jQmock, valueAccessor, labels;

module('UX Custom Binding Handlers Tests (ektronUXAccordion)', {
    setup: function () {
        'use strict';

        valueAccessor = function () {
            return {};
        };
    },
    teardown: function () {
        ko.bindingHandlers = {};
    }
});

test('adds a disposal callback to prevent memory leaks when the binding element is destroyed.', function () {
    'use strict';

    if ('undefined' === typeof (ko.isObservable)) {
        ko.isObservable = function () { return true; };
    }

    var correctElem = false,
        element = 'the test elem',
        correctAccordionAction = false,
        accordionAction = 'destroy',
        addDisposeCallbackCount = 0;

    jQmock = function (elem) {
        correctElem = elem === element;
        return {
            accordion: function (input) {
                correctAccordionAction = accordionAction === input;
                return this;
            }
        };
    };

    jQmock.extend = function () { };

    //handle disposal (if KO removes by the template binding)
    ko.utils.domNodeDisposal.addDisposeCallback = function (elem, callback) {
        addDisposeCallbackCount = addDisposeCallbackCount + 1;
        if (elem === element) {
            callback();
        }
    }

    ko.bindingHandlers.ektronUXAccordion = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAccordion.init(element, valueAccessor);

    ok(correctAccordionAction, 'When the disposal callback is executed by ko we call destroy on the accordion instance.');
    ok(correctElem, 'The correct element was passed to jquery to destroy the accordion.');
    equal(addDisposeCallbackCount, 1, 'AddDisposalCallback was called once.');
});

test('passes the correct element to jquery', function () {
    'use strict';

    var correctSelector = false,
        rightSelector = '<div/>',
        callCount = 0;

    jQmock = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            accordion: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAccordion = undefined;
    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXAccordion.update(rightSelector, valueAccessor, {});

    equal(callCount, 1, 'accordion was called only once on update.');
    ok(correctSelector, 'the correct element was passed to autocomplete.');
});

test('update correctly passes options to accordion.', function () {
    'use strict';

    var correctOptions = false,
        Options = {
            active: true
        };

    valueAccessor = function () {
        return {
            active: true
        };
    };

    jQmock = function (selector) {
        return {
            accordion: function (options) { correctOptions = options.active === Options.active; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (first, second) { first.autoOpen = second.autoOpen; return first; };
    //clean up our handler first.
    ko.bindingHandlers.ektronUXAccordion = undefined;

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXAccordion.update({}, valueAccessor, {});

    ok(correctOptions, 'The handler sent the correct options to the accordion function.');
});