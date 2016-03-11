/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../jQuery/jquery.min.js" />
/// <reference path="../../../jQuery/jquery.migrate.js" />
/// <reference path="../../ektron.js" />
/// <reference path="../../../jQuery/UI/js/jquery-ui-complete.min.js" />
/// <reference path="../js/koBindingHandlers.js" />

var jQmock, valueAccessor, labels;

module('UX Custom Binding Handlers Tests (ektronUXDialog)', {
    setup: function () {
        'use strict';

        valueAccessor = function () {
            return {};
        };

        labels = {
            'close': 'close'
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
        correctDialogAction = false,
        dialogAction = 'destroy',
        addDisposeCallbackCount = 0;

    jQmock = function (elem) {
        correctElem = elem === element;
        return {
            dialog: function (input) {
                correctDialogAction = dialogAction === input;
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

    ko.bindingHandlers.ektronUXDialog = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXDialog.init(element, valueAccessor);


    ok(correctDialogAction, 'When the disposal callback is executed by ko we call destroy on the dialog instance.');
    ok(correctElem, 'The correct element was passed to jquery to destroy the dialog.');
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
            dialog: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXDialog = undefined;
    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXDialog.update(rightSelector, valueAccessor, {});

    equal(callCount, 1, 'Dialog was called only once on update.');
    ok(correctSelector, 'the correct element was passed to autocomplete.');
});

test('update correctly passes options to dialog.', function () {
    'use strict';

    var correctOptions = false,
        Options = {
            autoOpen: true
        };

    valueAccessor = function () {
        return {
            autoOpen: true
        };
    };

    jQmock = function (selector) {
        return {
            dialog: function (options) { correctOptions = options.autoOpen === Options.autoOpen; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (first, second) { first.autoOpen = second.autoOpen; return first;};
    //clean up our handler first.
    ko.bindingHandlers.ektronUXDialog = undefined;

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXDialog.update({}, valueAccessor, {});

    ok(correctOptions, 'The handler sent the correct options to the dialog function.');
});

test('checking default options', function () {
    'use strict';

    var defaultOptions = {
            autoOpen: false,
            appendTo: '.ektron-ux .ux-appWindow',
            closeText: '<span data-ux-icon="&#xe01a;" title="' + labels.close + '" aria-hidden="true" />'
        },
        passedOptions = {},
        extendedObj = {},
        element,
        extendCallCount;

    jQmock = function (selector) {
        return {
            dialog: function (options) { return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (firstObj, secondObj) {
        extendCallCount = extendCallCount + 1;
        passedOptions = secondObj;
        extendedObj = firstObj;
        return firstObj;
    };

    ko.bindingHandlers.ektronUXDialog = undefined;
    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.ektronUXDialog.update(element, valueAccessor);

    deepEqual(passedOptions, {}, 'dialog was passed the correct options.');
    deepEqual(extendedObj, defaultOptions, 'passedOptions are correctly extended with the default options.');
});