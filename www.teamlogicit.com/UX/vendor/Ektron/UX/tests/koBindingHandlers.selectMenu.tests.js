/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../jQuery/jquery.min.js" />
/// <reference path="../../../jQuery/jquery.migrate.js" />
/// <reference path="../../ektron.js" />
/// <reference path="../../../jQuery/UI/js/jquery-ui-complete.min.js" />
/// <reference path="../js/koBindingHandlers.js" />

var jQmock, valueAccessor, labels;

module('UX Custom Binding Handlers Tests (ektronUXSelectMenu)', {
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
        correctSelectMenuAction = false,
        selectMenuAction = 'destroy',
        addDisposeCallbackCount = 0;

    jQmock = function (elem) {
        correctElem = elem === element;
        return {
            selectmenu: function (input) {
                correctSelectMenuAction = selectMenuAction === input;
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

    ko.bindingHandlers.ektronUXSelectMenu = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXSelectMenu.init(element, valueAccessor);

    ok(correctSelectMenuAction, 'When the disposal callback is executed by ko we call destroy on the accordion instance.');
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
            selectmenu: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXSelectMenu = undefined;
    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXSelectMenu.update(rightSelector, valueAccessor, {});

    equal(callCount, 1, 'accordion was called only once on update.');
    ok(correctSelector, 'the correct element was passed to autocomplete.');
});

test('update correctly passes options to selectmenu.', function () {
    'use strict';

    var correctOptions = false,
        Options = {
            popup: true
        };

    valueAccessor = function () {
        return Options;
    };

    jQmock = function (selector) {
        return {
            selectmenu: function (options) { correctOptions = options.popup === Options.popup; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (first, second) { first.popup = second.popup; return first; };
    //clean up our handler first.
    ko.bindingHandlers.ektronUXSelectMenu = undefined;

    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXSelectMenu.update({}, valueAccessor, {});

    ok(correctOptions, 'The handler sent the correct options to the selectmenu function.');
});

test('checking default options', function () {
    'use strict';

    var defaultOptions = {
            popup: false,
            appendTo: '.ektron-ux .ux-appWindow .ux-app'
        },
        passedOptions = {},
        extendedObj = {},
        element,
        extendCallCount;

    jQmock = function (selector) {
        return {
            selectmenu: function (options) { return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (firstObj, secondObj) {
        extendCallCount = extendCallCount + 1;
        passedOptions = secondObj;
        extendedObj = firstObj;
        return firstObj;
    };

    ko.bindingHandlers.ektronUXSelectMenu = undefined;
    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXSelectMenu.update(element, valueAccessor);

    deepEqual(passedOptions, {}, 'selectmenu was passed the correct options.');
    deepEqual(extendedObj, defaultOptions, 'passedOptions are correctly extended with the default options.');
});