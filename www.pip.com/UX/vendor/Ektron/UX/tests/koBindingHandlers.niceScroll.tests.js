/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../jQuery/jquery.min.js" />
/// <reference path="../../../jQuery/jquery.migrate.js" />
/// <reference path="../../ektron.js" />
/// <reference path="../../../jQuery/plugins/niceScroll/jquery.nicescroll.js" />
/// <reference path="../../../fakequery/fake-query-0.2.js" />
/// <reference path="../js/koBindingHandlers.js" />

var fakeQuery, $ektron;

module('UX Custom Binding Handlers Tests (ektronUXNiceScroll)', {
    setup: function () {
        'use strict';

        ////var $ektron = window.$ektron,
        fakeQuery = window.$$;
        $ektron = window.$ektron;

        // start with a fresh fakeQuery
        fakeQuery.reset();
    },
    teardown: function () {
        ko.bindingHandlers = {};
    }
});

test('Checking default options', function () {
    'use strict';
    var defaultOptions = {
            autohidemode: false,
            cursorcolor: 'rgba(0, 0, 0, .3)',
            cursorborder: 0,
            useparentcontainer: false,
            background: 'transparent',
            preservenativescrolling: false,
            pixelsToScroll: 0,
            invoke: false
        },
        passedOptions = {};
    window.createModule(ko, $ektron);

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.init();

    // stop recording calls now
    fakeQuery.stopRecording();

    passedOptions = fakeQuery.lastCallTo.niceScroll.argument;

    equal(fakeQuery.getCallCount('niceScroll'), 1, 'niceScroll is called once during init.');
    equal(passedOptions.autohidemode, defaultOptions.autohidemode, 'autohidemode was passed correctly.');
    equal(passedOptions.cursorcolor, defaultOptions.cursorcolor, 'cursorcolor was passed correctly.');
    equal(passedOptions.cursorborder, defaultOptions.cursorborder, 'cursorborder was passed correctly.');
    equal(passedOptions.useparentcontainer, defaultOptions.useparentcontainer, 'useparentcontainer was passed correctly.');
    equal(passedOptions.background, defaultOptions.background, 'background was passed correctly.');
    equal(passedOptions.preservenativescrolling, defaultOptions.preservenativescrolling, 'preservenativescrolling was passed correctly.');
    equal(passedOptions.pixelsToScroll, defaultOptions.pixelsToScroll, 'pixelsToScroll was passed correctly.');
    equal(passedOptions.invoke, defaultOptions.invoke, 'invoke was passed correctly.');
});

test('Passed options if passed are used instead of default', function () {
    'use strict';

    var element,
        extendArg1,
        extendArg2,
        valueAccessor = function () {
            return {
                testProp: true
            };
        };

    window.createModule(ko, $ektron);

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.init(element, valueAccessor);

    // stop recording calls now
    fakeQuery.stopRecording();

    extendArg1 = fakeQuery.lastCallTo.extend.arguments[0];
    extendArg2 = fakeQuery.lastCallTo.extend.arguments[1];

    equal(fakeQuery.getCallCount('extend'), 1, 'extend is called once during init.');
    equal(extendArg1.autohidemode, false, 'extend uses the default options object for the first argument.');
    equal(extendArg2.testProp, valueAccessor().testProp, 'extend uses the valueAccessor function to get user supplied options.');
});

test('Uses the element provided by knockout for the niceScroll method call.', function () {
    'use strict';

    var element = 'simulated element',
        isSameElement;

    window.createModule(ko, $ektron);

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.init(element);

    // stop recording calls now
    fakeQuery.stopRecording();

    isSameElement = (fakeQuery.callsTo['$'][0].argument === element);

    ok(isSameElement, 'is same element');
});

test('On update, update calls .resize() method appropriately based on options.invoke parameter.', function () {
    'use strict';

    var element,
        getNiceScrollCount = 0,
        getResizeCount = 0,
        valueAccessor = function () {
            return {
                invoke: true
            };
        };
    
    window.createModule(ko, $ektron);

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.update(element, valueAccessor);

    // stop recording calls now
    fakeQuery.stopRecording();

    getNiceScrollCount = fakeQuery.getCallCount('getNiceScroll');
    getResizeCount = fakeQuery.getCallCount('resize');


    equal(getNiceScrollCount, 1, 'options.invoke is true and getNiceScroll() is called once.');
    equal(getResizeCount, 1, 'options.invoke is true and niceScroll .resize() is called once.');

    // retest with options.invoke false, and ensure neither is called.

    // reset fakeQuery
    fakeQuery.reset();

    getNiceScrollCount = 0;
    getResizeCount = 0;
    valueAccessor = function () {
        return {
            invoke: false
        };
    };

    ko.bindingHandlers.ektronUXNiceScroll.update(element, valueAccessor);

    equal(getNiceScrollCount, 0, 'options.invoke is false and getNiceScroll() is not called.');
    equal(getResizeCount, 0, 'options.invoke is false and niceScroll .resize() is not called.');
});

test('On update, element is scrolled to a desired pont appropriately', function () {
    'use strict';

    var element,
        parentCount = 0,
        heightCount = 0,
        scrollTopCount = 0,
        pixelsScrolled,
        settings = {
            invoke: false,
            pixelsToScroll: 100
        },
        valueAccessor = function () {
            return settings;
        };

    // mock jQuery
    $ektron = function (selector) {
        return {
            parent: function () {
                parentCount = parentCount + 1;
                return this;
            },
            height: function () {
                heightCount = heightCount + 1;
                return 100;
            },
            scrollTop: function (value) {
                scrollTopCount = scrollTopCount + 1;
                pixelsScrolled = parseInt(value, 10);
                return this;
            },
            getNiceScroll: function () {
                return this;
            },
            resize: function () {
                return this;
            }
        };
    };

    window.createModule(ko, $ektron);

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.update(element, valueAccessor);

    // stop recording calls now
    fakeQuery.stopRecording();

    equal(parentCount, 0, 'options.invoke is false, .parent() is not called.');
    equal(heightCount, 0, 'options.invoke is false, .height() is not called.');

    // reset fakeQuery
    fakeQuery.reset();

    parentCount = 0;
    heightCount = 0;
    settings = {
        invoke: true,
        pixelsToScroll: 'this should prevent scrolling'
    };

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.update(element, valueAccessor);

    equal(parentCount, 1, 'options.invoke is true, pixelsToScroll is NaN, and .parent() is called once.');
    equal(heightCount, 0, 'options.invoke is true, pixelsToScroll is NaN, and .height() is not called.');

    // reset fakeQuery
    fakeQuery.reset();

    parentCount = 0;
    heightCount = 0;
    scrollTopCount = 0;
    settings = {
        invoke: true,
        pixelsToScroll: 100
    };

    // call the code to be tested
    ko.bindingHandlers.ektronUXNiceScroll.update(element, valueAccessor);

    equal(parentCount, 1, 'options.invoke is true, pixelsToScroll is a number, and .parent() is called once.');
    equal(heightCount, 1, 'options.invoke is true, pixelsToScroll is a number, and .height() is called once.');
    equal(scrollTopCount, 1, '.scrollTop() is called.');
    equal(pixelsScrolled, settings.pixelsToScroll, '.scrollTop() is called with the correct argument.');
});