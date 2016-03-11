/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../js/koBindingHandlers.js" />

var valueAccessor, allBindingsAccessor, viewModel, bindingContext, labels;

module('UX Custom Binding Handlers Tests (ektronUXLabel)', {
    setup: function () {
        'use strict';
        ko.bindingHandlers.ektronUXLabel = undefined;

        labels = {
            'test': 'test',
            'i18nError' : 'i18n error: test message'
        };

        viewModel = {
            'labels': labels
        };

        valueAccessor = function () {
            return {};
        };

        allBindingsAccessor = function () {
            return {};
        }

        bindingContext = {
            '$parent': {},
            '$parents': {},
            '$root': {}
        };
    },
    teardown: function () {
        ko.bindingHandlers = {};
    }
});

test('binding calls ko.bindingshandlers.text.update with the correctly translated string when available.', function () {
    var element = 'the test elem',
        passedElement,
        foundTranslation,
        passedAllBindingsAccessor,
        passedViewModel,
        passedBindingContext,
        jQmock = function () { };

    window.createModule(ko, jQmock, labels);

    valueAccessor = function () {
        return 'test';
    };

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            passedElement = element;
            foundTranslation = valueAccessor();
            passedAllBindingsAccessor = allBindingsAccessor;
            passedViewModel = viewModel;
            passedBindingContext = bindingContext;
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    deepEqual(passedElement, element, 'element was passed to text binding correctly');
    equal(foundTranslation, labels['test'], 'valueAccessor was passed to text binding correctly');
    deepEqual(passedAllBindingsAccessor, allBindingsAccessor, 'allBindingsAccessor was passed to text binding correctly');
    deepEqual(passedViewModel, viewModel, 'viewModel was passed to text binding correctly');
    deepEqual(passedBindingContext, bindingContext, 'bindingContext was passed to text binding correctly');
});

test('binding results in appropriate error message if i18n key is not found.', function () {
    var element = 'the test elem',
        jQmock = function () { };

    valueAccessor = function () {
        return 'unknownKey';
    };

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            foundTranslation = valueAccessor();
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    equal(foundTranslation, labels['i18nError'], 'valueAccessor was passed to text binding correctly');
});

test('binding results in appropriate translation when the value accessor is an observable.', function () {
    var element = 'the test elem',
        jQmock = function () { };

    valueAccessor = ko.computed(function () {
        return 'test';
    });

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            foundTranslation = valueAccessor();
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    equal(foundTranslation, labels['test'], 'valueAccessor was passed to text binding correctly');
});

test('valueAccessor returns undefined is handled correctly', function () {
    var element = 'the test elem',
        jQmock = function () { };

    valueAccessor = ko.computed(function () {
        return undefined;
    });

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            foundTranslation = valueAccessor();
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    equal(foundTranslation, labels['i18nError'], 'the binding returns an appropriate error in this use case.');
});

test('if .labels property is not found in the current context, the handler loops up the parents tree attempting to find it.', function () {
    var element = 'the test elem',
        jQmock = function () { },
        foundTranslation,
        parentViewModel = {
            'labels': {
                'test' : 'test'
            }
        };

    bindingContext = {
        '$parent': parentViewModel,
        '$parents': [parentViewModel],
        '$root': parentViewModel
    };

    viewModel = {};

    valueAccessor = ko.computed(function () {
        return 'test';
    });

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            foundTranslation = (valueAccessor() === bindingContext.$parents[0].labels['test']) ? true : false;
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    ok(foundTranslation, 'the correct translation was found ');
});

test('looping through ancestry works with the labels object being an observable', function () {
    var element = 'the test elem',
        jQmock = function () { },
        foundTranslation,
        parentViewModel = {
            'labels': ko.observable({
                'test': 'test'
            })
        };

    bindingContext = {
        '$parent': parentViewModel,
        '$parents': [parentViewModel],
        '$root': parentViewModel
    };

    viewModel = {};

    valueAccessor = ko.computed(function () {
        return 'test';
    });

    window.createModule(ko, jQmock, labels);

    ko.bindingHandlers.text = {
        update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
            foundTranslation = valueAccessor();
        }
    };

    ko.bindingHandlers.ektronUXLabel.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    equal(foundTranslation, ko.utils.unwrapObservable(bindingContext.$parents[0].labels)['test'], 'the correct translation was found ');
});