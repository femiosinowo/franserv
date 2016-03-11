/*global window,module,test,ok,alert*/
/// <reference path="setup.js" />
/// <reference path="../js/ko.for.js" />
var $, ko;

module('Knockout "for" binding', {
    setup: function () {
        'use strict';

        $ = function () {
            return {
                each: function (callback) {
                    callback();
                }
            };
        };
        $.trim = function (str) {
            return str;
        };

        ko = {
            bindingHandlers: {}
        };
    }
});


test('KO "for" binding builds "for" attribute', function () {
    'use strict';
    var $extend,
        element = {},
        target = {},
        selectFunction = function () { return String('prev'); },
        foundForId,
        generateId = false,
        id = 'test ID',
        foundGeneratedId;

    // Extend base mock for specific calls/test cases
    $extend = function (el) {
        var $ref = $();

        if (el === element) {
            $ref.attr = function (attribute, value) {
                if (attribute === 'for') {
                    foundForId = value;
                }
            };
            $ref.prev = function () {
                return target;
            };
        } else if (el === target) {
            if (generateId) {
                $ref.attr = function (attribute, value) {
                    if (arguments.length === 1) {
                        return undefined;
                    } else {
                        foundGeneratedId = arguments[1];
                    }
                };
            } else {
                $ref.attr = function () {
                    return id;
                };
            }
        }

        return $ref;
    }
    $extend.trim = $.trim;

    window.createModule($extend, ko);

    ko.bindingHandlers.for.init(element, selectFunction);
    equal(foundForId, id, 'KO "for" binding assigns input ID to label for attribute');

    generateId = true;
    ko.bindingHandlers.for.init(element, selectFunction);
    ok(typeof foundGeneratedId === typeof '' && foundGeneratedId.length > 0, 'KO "for" binding generates an ID for an input without one');
});
