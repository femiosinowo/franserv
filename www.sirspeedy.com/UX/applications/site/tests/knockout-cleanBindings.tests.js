/// <reference path="setup.js" />
/// <reference path="../js/knockout-cleanBindings.js" />

var ko, $ektron;

module('ko.cleanBindings Tests', {
    setup: function () {
        'use strict';
        ko = {};
    }
});

test('', function () {
    'use strict';
    var providedSelector,
        selectedElement,
        dummyNode = {};

    ko.cleanNode = function (element) {
        selectedElement = element;
    };
    $ektron = function (selector) {
        providedSelector = selector;
        return {
            each: function (callback) {
                callback.call(dummyNode);
            }
        };
    };

    window.createModule(ko, $ektron);

    ko.cleanBindings('ektron');

    equal(providedSelector, '[data-bind-ektron]', 'selected namespaced bindings.');
    equal(selectedElement, dummyNode, 'removed the bindings from the correct node.');
});

test('cleanBindings works with multiple namespaces', function () {
    'use strict';
    var providedSelectors = [];

    $ektron = function (selector) {
        providedSelectors.push(selector);
        return {
            each: function () {
            }
        };
    };

    window.createModule(ko, $ektron);

    ko.cleanBindings('ektron');

    ko.cleanBindings('ektron_v2');

    equal(providedSelectors[0], '[data-bind-ektron]', 'correctly selected first namespace.');
    equal(providedSelectors[1], '[data-bind-ektron_v2]', 'correctly selected second namespace.');
});

test('running plugin twice does not override the binding', function () {
    'use strict';
    var foundBinding;

    window.createModule(ko, $ektron);
    foundBinding = ko.cleanBindings;
    window.createModule(ko, $ektron);

    equal(foundBinding, ko.cleanBindings, 'found same bindings on multiple calls.');
});