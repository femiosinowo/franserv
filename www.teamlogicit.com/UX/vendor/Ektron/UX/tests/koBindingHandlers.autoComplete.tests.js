/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../../../jQuery/jquery.min.js" />
/// <reference path="../../../jQuery/jquery.migrate.js" />
/// <reference path="../../ektron.js" />
/// <reference path="../../../jQuery/plugins/niceScroll/jquery.nicescroll.js" />
/// <reference path="../../../fakequery/fake-query-0.2.js" />
/// <reference path="../js/koBindingHandlers.js" />

var jQmock, valueAccessor;

module('UX Custom Binding Handlers Tests (ektronUXAutoComplete)', {
    setup: function () {
        'use strict';
        jQmock = function (selector) {

            return {
                parent: function () { return this; },
                scrollTop: function () { return this; },
                autocomplete: function () { return this; },
                is: function () { return this; }
            };
        };

        ko.isObservable = function (itemToTest) {
            return false;
        }
        jQmock.extend = function () { };
        ko.bindingHandlers.ektronUXAutocomplete = undefined;
        valueAccessor = function () { return { AutoCompleteOptions: null, invoke: true }; };
    }
});

test('passes the correct element to jquery', function () {
    'use strict';

    var correctSelector = false, rightSelector = '<input/>', callCount = 0;

    jQmock = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            autocomplete: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update(rightSelector, valueAccessor, {});

    equal(callCount, 1, 'Autocomplete was called only once on update.');
    ok(correctSelector, 'the correct element was passed to autocomplete.');
});

test('only invokes when the options passed are told to do so.', function () {
    'use strict';

    var callCount = 0;

    jQmock = function () {
        return {
            autocomplete: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };
    valueAccessor = function () {
        return {
            invoke: false,
            AutoCompleteOptions: {}
        };

    };
    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update({}, valueAccessor, {});

    equal(0, callCount, 'Setting Invoke to false did not invoke the autocomplete function.');

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    valueAccessor = function () {
        return {
            invoke: true,
            AutoCompleteOptions: {}
        };

    };

    ko.bindingHandlers.ektronUXAutocomplete.update({}, valueAccessor, {});

    equal(1, callCount, 'Setting Invoke to true did invoke the autocomplete function.');
});

test('handler checks for the correct dom element, namely type of input.', function () {
    'use strict';

    var correctTypeCheck = false, typeToCheck = 'input', callCount = 0,
        correctElementPassed = false, correctElement = '<input/>',
        autoCompleteCallCount = 0;

    jQmock = function (elem) {
        correctElementPassed = elem === correctElement;
        return {
            autocomplete: function () { autoCompleteCallCount = autoCompleteCallCount + 1; return this; },
            is: function (input) { correctTypeCheck = input === typeToCheck; callCount = callCount + 1; return false; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update(correctElement, valueAccessor, {});

    ok(correctElementPassed, 'The handler ran the type check on the correct element.');
    ok(correctTypeCheck, 'The handler checked that the type of the element was of type input.');
    equal(1, callCount, 'The handler only called is function once.');
    equal(0, autoCompleteCallCount, 'The handler did not invoke autocomplete on an element that was not an input.');
});

test('update correctly passes options to autocomplete.', function () {
    'use strict';

    var correctOptions = false, Options = { invoke: true };

    valueAccessor = function () { return { invoke: true }; };

    jQmock = function (selector) {
        return {
            autocomplete: function (options) { correctOptions = options.invoke === Options.invoke; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function (first, second) { first.invoke = second.invoke; };
    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update({}, valueAccessor, {});

    ok(correctOptions, 'The handler sent the correct options to the autocomplete function.');
});

test('update passes the correct element to jquery', function () {
    'use strict';

    var correctSelector = false, rightSelector = '<input/>', callCount = 0;

    jQmock = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            autocomplete: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update(rightSelector, valueAccessor, {});

    equal(callCount, 1, 'Autocomplete was called only once on init.');
    ok(correctSelector, 'the correct element was passed to autocomplete.');

});

test('update only invokes when the options passed are told to do so.', function () {
    'use strict';

    var callCount = 0;

    jQmock = function () {
        return {
            autocomplete: function () { callCount = callCount + 1; return this; },
            is: function (input) { return true; }
        };
    };
    jQmock.extend = function () { };
    valueAccessor = function () {
        return {
            invoke: false,
            AutoCompleteOptions: {}
        };

    };
    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update({}, valueAccessor, {});

    equal(0, callCount, 'Setting Invoke to false did not invoke the autocomplete function.');

    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    valueAccessor = function () {
        return {
            invoke: true,
            AutoCompleteOptions: {}
        };

    };

    ko.bindingHandlers.ektronUXAutocomplete.update({}, valueAccessor, {});

    equal(1, callCount, 'Setting Invoke to true did invoke the autocomplete function.');
});

test('handler update checks for the correct dom element, namely type of input.', function () {
    'use strict';

    var correctTypeCheck = false, typeToCheck = 'input', callCount = 0,
        correctElementPassed = false, correctElement = '<input/>',
        autoCompleteCallCount = 0;

    jQmock = function (elem) {
        correctElementPassed = elem === correctElement;
        return {
            autocomplete: function () { autoCompleteCallCount = autoCompleteCallCount + 1; return this; },
            is: function (input) { correctTypeCheck = input === typeToCheck; callCount = callCount + 1; return false; }

        };
    };

    jQmock.extend = function () { };


    //clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update(correctElement, valueAccessor, {});

    ok(correctElementPassed, 'The handler ran the type check on the correct element.');
    ok(correctTypeCheck, 'The handler checked that the type of the element was of type input.');
    equal(1, callCount, 'The handler only called is function once.');
    equal(0, autoCompleteCallCount, 'The handler did not invoke autocomplete on an element that was not an input.');


});

test('extends passed in settings with default settings.', function () {
    'use strict';

    var correctDefaultSettings = false,
        correctElement = 'test', correctMergeSettings = false, sourceItems = ['test'], callCount = 0,
        valueAccessor = function () { return { source: ko.observable(sourceItems) }; };

    jQmock.extend = function (firstObj, secondObj) {
        correctDefaultSettings = 'undefined' !== typeof (firstObj);
        correctMergeSettings = secondObj.source()[0] === sourceItems[0];
        callCount = callCount + 1;
    };

    // clean up our handler first.
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update(correctElement, valueAccessor, {});


    ok(correctDefaultSettings, 'The correct default settings were passed to extend as the first object');
    ok(correctMergeSettings, 'The correct user settings were passed to be merged into the defaults.');
    equal(callCount, 1, 'Extend was called once.');

});

test('respects use parent.', function () {
    'use strict';

    var parentCalled = false, valueAccessor = function () { return { useParent: true }; };

    jQmock = function () {
        return {
            autocomplete: function () { return this; },
            is: function (input) { return false; },
            parent: function () { return 'parent'; }

        };
    };

    jQmock.extend = function (first, second) {
        parentCalled = first.appendTo === 'parent';
    };

    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update('elem', valueAccessor, {});

    ok(parentCalled);
});

test('checks for observable array for source, and if it is observable, converts it to just js.', function () {
    'use strict';

    var isObservableCount = 0, toJSCount = 0, correctItemToTest = false, valueAccessor = function () { return { source: ko.observableArray(['test']) }; };
    ko.isObservable = function (itemToTest) {
        correctItemToTest = itemToTest()[0] === valueAccessor().source()[0];
        isObservableCount = isObservableCount + 1;
        return true;
    };

    ko.toJS = function (itemToConvert) {
        toJSCount = toJSCount + 1;
    };
    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.update('elem', valueAccessor, {});

    equal(isObservableCount, 1, 'isObservable is called once.');
    equal(toJSCount, 1, 'toJS is called once.');
    ok(correctItemToTest, 'The correct Item was checked to be an observable.');
});

test('adds a disposal callback to prevent memory leaks when the binding element is destroyed.', function () {
    'use strict';

    if ('undefined' === typeof (ko.isObservable)) {
        ko.isObservable = function () { return true; };
    }

    var correctElem = false, element = 'the test elem', 
        correctAutoCompleteInputAction = false, autoCompleteAction = 'destroy',
        addDisposeCallbackCount = 0;

    jQmock = function (elem) {
        correctElem = elem === element;
        return {
            autocomplete: function (input) {
                correctAutoCompleteInputAction = autoCompleteAction === input;
                return this;
            },
            scrollTop: function () { return this; },
            is: function () { return this; }
            

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

    ko.bindingHandlers.ektronUXAutocomplete = undefined;
    window.createModule(ko, jQmock);

    ko.bindingHandlers.ektronUXAutocomplete.init(element, valueAccessor);

    ok(correctAutoCompleteInputAction, 'When the disposal callback is executed by ko we call destroy on the autocomplete instance.');
    ok(correctElem, 'The correct element was passed to jquery to destroy the autocomplete.');
    equal(addDisposeCallbackCount, 1, 'AddDisposalCallback was called once.');
});