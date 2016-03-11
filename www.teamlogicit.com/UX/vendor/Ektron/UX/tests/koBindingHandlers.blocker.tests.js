/*globals module,test*/
/// <reference path="setup.js" />
/// <reference path="../js/koBindingHandlers.js" />
var ko, $, labels;

module('UX Custom Binding Handlers Tests (ektronUXBlocker)', {
    setup: function () {
        ko = {
            bindingHandlers: {
            },
            isObservable: function (value) {
                return typeof value === "function" && value.isObservable;
            },
            observable: function (orig) {
                var value = orig;

                function property(set) {
                    if (typeof set !== "undefined") {
                        value = set;
                    } else {
                        return value;
                    }
                }

                property.isObservable = true;

                return property;
            },
            utils: {
                unwrapObservable: function (value) {
                    return ko.isObservable(value) ? value() : value;
                }
            },
            toJS: function (model) {
                var unwrappedModel = {};

                for (var i in model) {
                    unwrappedModel[i] = ko.utils.unwrapObservable(model[i]);
                }

                return unwrappedModel;
            }
        };

        $ = function () {
            return $;
        };
        $.block = function () { };
        $.unblock = function () { };
        $.extend = function (left, right) {
            for (var i in right) {
                left[i] = right[i];
            }

            return left;
        };

        labels = {};
    }
});

test('update correctly passes options to block', function () {
    'use strict';
    var options =
        {
            testOption: 1,
            exampleOption: 2
        },
        foundOptions = null;

    // Set up mock to capture tested data
    $.block = function (opts) {
        foundOptions = opts;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return options; }, {});

    // Test captured data
    for (var i in options) {
        equal(foundOptions[i], options[i], i + ' in found options correctly matches value passed in options');
    }
});

test('update applies block to bound element', function () {
    'use strict';
    var element = {},
        blockApplied = false,
        extend;

    // Set up mock to test block being called only on the bound element
    $ = $.extend(
        function (selector) {
            if (selector === element) {
                return {
                    block: function () {
                        blockApplied = true;
                    }
                };
            }
        },
        $);

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update(element, function () { return {}; }, {});

    // Test captured data
    ok(blockApplied, 'Block applied to the bound element');
});

test('default options use the jQuery theme', function () {
    'use strict';
    var foundOptions;

    // Set up mock to capture options
    $.block = function (options) {
        foundOptions = options;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return {}; }, {});

    // Test captured data
    ok(foundOptions.useTheme, 'default options use the jQuery theme');
});

test('allow options as an unobserved object', function () {
    'use strict';
    var options = {},
        optionsFound;

    // Set up mock to require use of unwrapObservable for options
    ko.utils = {
        unwrapObservable: function (value) {
            optionsFound |= value === options;
            return value;
        }
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return options; }, {});

    // Test captured data
    ok(optionsFound, 'options found match provided unobserved object');
});

test('do not call block if observable isBlocked is false', function () {
    'use strict';
    var isBlocked = ko.observable(false),
        blockCalled = false;

    // Set up mock to capture whether block is called
    $.block = function (options) {
        blockCalled = true;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return { isBlocked: isBlocked }; }, {});

    // Test captured data
    ok(!blockCalled, 'block not invoked when isBlocked is false');
});

test('update applied unblock to the bound element if isBlocked is false', function () {
    'use strict';
    var isBlocked = false,
        element = {},
        unblockCalled = false,
        $ref = $;

    // Set up mock to test unblock being called only on the bound element
    $ = $.extend(
        function (selector) {
            if (selector === element) {
                return {
                    unblock: function () {
                        unblockCalled = true;
                    }
                };
            }
            else {
                return $ref(selector);
            }
        },
        $);

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update(element, function () { return { isBlocked: isBlocked }; }, {});

    // Test captured data
    ok(unblockCalled, 'unblock invoked when isBlocked is false');
});

test('update uses toJS to unwrap all possibly observable options', function () {
    'use strict';
    var foundOptions;

    ko.toJS = function () {
        return { unwrapped: true };
    };
    $.block = function (opts) {
        foundOptions = opts;
    }

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return {}; }, {});

    // Test captured data
    ok(foundOptions.unwrapped, 'options found by block were unwrapped');
});

test('callback to update isBlocked observable as false after unblock event is registered', function () {
    'use strict';
    var isBlocked = ko.observable(true),
        foundCallback;

    // Set up mock to immediately call onUnblock callback
    $.block = function (options) {
        options.onUnblock();
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return { isBlocked: isBlocked }; }, {});

    // Test conditions
    ok(!isBlocked(), 'isBlocked observable is updated as false on unblock');
});

test('onUnblock callback provided in binding options is called by callback in plugin options', function () {
    'use strict';
    var callbackCalled = false,
        callback = function () {
            callbackCalled = true;
        };

    // Set up mock to immediately call onUnblock callback
    $.block = function (options) {
        options.onUnblock();
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return { onUnblock: callback }; }, {});

    // Test conditions
    ok(callbackCalled, 'optional callback provided in options to block');
});

test('default style class for message block follows UX namespacing', function () {
    'use strict';
    var foundCssClass;

    // Set up mock to capture class option
    $.block = function (options) {
        foundCssClass = options.blockMsgClass;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return {}; }, {});

    // Test captured data
    equal(foundCssClass, 'ux-block-container', 'Found default style class matches expected value consistent with UX conventions');
});

test('default message is only the UX busy indicator', function () {
    'use strict';
    var foundMessage;

    // Set up mock to capture class option
    $.block = function (options) {
        foundMessage = options.message;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return {}; }, {});

    equal(foundMessage, '<span class="ux-busy-indicator" />', 'Found default message is only the UX busy indicator');
});

test('default CSS removes message box', function () {
    'use strict';
    var foundBorder,
        foundBackground,
        foundColor;

    // Set up mock to capture class option
    $.block = function (options) {
        foundBackground = options.css.backgroundColor;
        foundBorder = options.css.border;
        foundColor = options.css.color;
    };

    // Perform tested action
    window.createModule(ko, $, labels);
    ko.bindingHandlers.ektronUXBlocker.update({}, function () { return {}; }, {});

    equal(foundBackground, 'none', 'There is no background in the default CSS');
    equal(foundBorder, 'none', 'There is no border in the default CSS');
    equal(foundColor, '#fff', 'The color will be white in the default CSS');
});