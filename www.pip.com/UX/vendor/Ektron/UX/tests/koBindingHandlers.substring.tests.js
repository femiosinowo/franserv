/// <reference path="setup.js" />
/// <reference path="../js/koBindingHandlers.js" />
/*global module,test,window,equal*/
var ko, $;

module('Campaign Management custom Knockout binding handlers', {
    setup: function () {
        'use strict';
        ko = {
            bindingHandlers: {
                text: function () { }
            },
            observable: function (value) {
                return function () {
                    return value;
                };
            },
            utils: {
                unwrapObservable: function (observable) {
                    if ("function" === typeof observable) {
                        return observable();
                    }
                    return observable;
                }
            }
        },
        $ = {};

        $.extend = function (standard, extension) {
            for (var prop in extension) {
                standard[prop] = extension[prop];
            }

            return standard;
        };
    }
});

test('substring update outputs requested substring using standard text binding', function () {
    'use strict';
    // Data to test outcome
    var foundText,
        expectedText = 'Neque porro…',
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        start = 0,
        end = 20,
        element = {},
        foundElement;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundElement = element;
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        element,
        function () {
            return {
                end: end,
                text: text
            };
        });

    // Test outcome
    equal(foundElement, element, 'Text updated on the correct element');
    equal(foundText, expectedText, 'Found correct substring of test text');
});

test('substring update outputs to end of string when no end is specified', function () {
    'use strict';
    // Data to test outcome
    var element = {},
        foundText,
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        start = 10;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        element,
        function () {
            return {
                start: start,
                text: text
            };
        });

    // Test outcome
    equal(foundText, text.substring(start, text.length), 'text continues to end of string');
});

test('substring update outputs nothing when a non-string is specified', function () {
    'use strict';
    // Data to test outcome
    var element = {},
        foundText = null,
        text = {};

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        element,
        function () {
            return {
                text: text
            };
        });

    // Test outcome
    equal(foundText, null, 'text is not set for a non-string');
});

test('substring update outputs to end of string when "end" extends past the end of the string', function () {
    'use strict';
    // Data to test outcome
    var element = {},
        foundText,
        text = 'Neque porro',
        end = 50;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        element,
        function () {
            return {
                end: end,
                text: text
            };
        });

    // Test outcome
    equal(foundText, text.substring(start, text.length), 'text continues to end of string');
});

test('text binding receives all expected parameters', function () {
    'use strict';
    // Data to test outcome
    var element = {}, allBindingsAccessor = {}, viewModel = {}, bindingContext = {},
        foundElement, foundallBindingsAccessor, foundViewModel, foundBindingContext;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (el, valueAccessor, aba, vm, bc) {
        foundElement = el;
        foundallBindingsAccessor = aba;
        foundViewModel = vm;
        foundBindingContext = bc;
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(element, function () { return { text: '' }; }, allBindingsAccessor, viewModel, bindingContext);

    // Test outcome
    equal(foundElement, element, 'text binding was passed correct element');
    equal(foundallBindingsAccessor, allBindingsAccessor, 'text binding was passed correct allBindingsAccessor');
    equal(foundViewModel, viewModel, 'text binding was passed correct viewModel');
    equal(foundBindingContext, bindingContext, 'text binding was passed correct bindingContext');
});

test('substring binding appropriately handles an observable text value', function () {
    'use strict';
    // Data to test outcome
    var text = 'Neque porro',
        foundText;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (el, valueAccessor, aba, vm, bc) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                text: ko.observable(text)
            };
        });

    // Test outcome
    equal(foundText, text, 'found text wrapped in an observable');
});

test('substring binding does not terminate string mid-word', function () {
    'use strict';
    // Data to test outcome
    var foundText,
        expectedText = 'Neque porro quisquam est qui…',
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        end = expectedText.length + 4;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                end: end,
                text: text
            };
        });

    // Test outcome
    equal(foundText, expectedText, 'found text matched expected text (without word fragments)');
});

test('substring binding accepts option for a cutoff indicator', function () {
    'use strict';
    var cutoff = '[EOM]',
        foundText,
        expectedText = 'Neque porro' + cutoff,
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        end = expectedText.length;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                cutoffIndicator: cutoff,
                end: end,
                text: text
            };
        });

    // Test outcome
    equal(foundText, expectedText, 'found text matched expected text inlcuding configured cutoff indicator');
});

test('substring binding accepts option to omit cutoff indicator', function () {
    'use strict';
    var foundText,
        expectedText = 'Neque porro quisquam',
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        end = expectedText.length;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                omitCutoffIndicator: true,
                end: end,
                text: text
            };
        });

    // Test outcome
    equal(foundText, expectedText, 'found text omits cutoff indicator when binding is so configured');
});

test('substring binding accepts option for string length which overrides end', function () {
    'use strict';
    var foundText,
        expectedText = 'Neque porro',
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        length = 15,
        end = 20;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                omitCutoffIndicator: true, // this makes testing easier
                length: length,
                text: text
            };
        });

    // Test outcome
    equal(foundText, expectedText, 'found text was correctly cuf off by the configured length option');
});

test('substring binding accepts option for allowing partial words', function () {
    'use strict';
    var foundText,
        expectedText = 'Neque porro qu…',
        text = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit',
        length = 15;

    // Mock to capture test data
    ko.bindingHandlers.text.update = function (element, valueAccessor) {
        foundText = valueAccessor();
    };

    // Perform test
    window.createModule(ko, $);
    ko.bindingHandlers.ektronUXSubstring.update(
        {},
        function () {
            return {
                allowPartialWords: true,
                length: length,
                text: text
            };
        });

    // Test outcome
    equal(foundText, expectedText, 'found text was correctly cuf off mid-word by the configured allowPartialWords option');
});