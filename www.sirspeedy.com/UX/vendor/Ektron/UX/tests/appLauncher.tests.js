/// <reference path="setup.js" />
/// <reference path='../vendor/qunit/js/qunit.js' />
/// <reference path="../js/appLauncher.js" />

var AppLauncher, viewModel, app, AppViewModel, $, launcher;

module('UX AppLauncher Tests', {
    setup: function () {
        'use strict';

        $ = function () {
            return {
                on: function () { },
                toggleClass: function () { }
            };
        };

        launcher = window.createModule($);
    }
});

test('Launcher "initialize" subscribes to window "beforeunload" to close itself', function () {
    'use strict';

    var foundSelector = false, foundEvent = false, foundCallback = false, 
        unloadCallback = function () {};

    function $(selector) {
        if (selector === window) {
            foundSelector = true;
            return {
                on: function (event, callback) {
                    if (event === 'beforeunload') {
                        foundEvent = true;
                        foundCallback = unloadCallback === callback;
                    }
                }
            }
        }

        return {
            on: function () {}
        };
    }

    launcher = window.createModule($);

    launcher.on('beforeunload', unloadCallback);

    ok(foundSelector, "Selected the window element");
    ok(foundEvent, "Subscribed to the correct unload event");
    ok(foundCallback, "Subscribed unloadCallback to the correct unload event");
});

test('setting visible flag adds class to body tag', function () {
    'use strict';
    var foundSelector = false, foundClass = false;

    function $(selector) {
        if (selector === 'body') {
            foundSelector = true;

            return {
                addClass: function (className) {
                    foundClass = className === 'ux-launcher-visible';
                }
            };
        }

        return null;
    }

    launcher = window.createModule($);
    launcher.visible(true);

    ok(foundSelector, "Selected body element");
    ok(foundClass, "Added correct class to the body");
});

test('setting visible flag adds class to body tag', function () {
    'use strict';
    var foundSelector = false, foundClass = false;

    function $(selector) {
        if (selector === 'body') {
            foundSelector = true;

            return {
                removeClass: function (className) {
                    foundClass = className === 'ux-launcher-visible';
                }
            };
        }

        return null;
    }

    launcher = window.createModule($);
    launcher.visible(false);

    ok(foundSelector, "Selected body element");
    ok(foundClass, "Removed correct class from the body");
});