/*global window,module,test,ok,alert*/
/// <reference path="../setup.js" />
/// <reference path="../../js/widgets/throbber.js" />
var Throbber, throbber, $, ko;

module('Throbber', {
    setup: function () {
        'use strict';
        $ = function () { };
        ko =
        {
            applyBindings: function () { },
            bindingHandlers: {},
            observable: function () { }
        };
        Throbber = window.createModule($, ko);
    }
});

test('Binding builds throbber', function () {
    'use strict';
    var element = {}, valueAccessor, allBindingsAccessor = function () { }, bindingContext = {},
        foundElement, valueAccessed = false, container = {}, selection, foundContainer, foundModel, template = '<span />',
        foundHtml;

    selection = {
        after: function () { }
    };

    $ = function (el) {
        // el is a string
        if (typeof el === typeof '') {
            // el is HTML
            if (el[0] === '<') {
                foundHtml = el;
                return {
                    0: container,
                    find: function () {
                        return {
                            append: function () { }
                        };
                    }
                };
            }

            // el is selector
            return selection;
        } else {
            // el is an element
            foundElement = el;

            return selection;
        }
    };

    ko =
    {
        applyBindings: function (model, element) {
            foundModel = model;
            foundContainer = element;
        },
        bindingHandlers: {},
        observable: function () { }
    };

    Throbber = window.createModule($, ko, template);

    ok("undefined" !== typeof ko.bindingHandlers.throbber, 'Throbber constructor creates binding');

    valueAccessor = function () {
        valueAccessed = true;
        return true;
    };

    ko.bindingHandlers.throbber.init(element, valueAccessor, allBindingsAccessor, {}, bindingContext);

    equal(foundElement, element, 'Binding references target element');
    ok(valueAccessed, 'Binding unboxes valueAccessor');
    equal(foundContainer, container, 'Created container bound to view model');
    equal(foundHtml, template, 'View model bound to supplied template');
});