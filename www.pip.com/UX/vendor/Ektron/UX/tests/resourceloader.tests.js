/// <reference path="setup.js" />
/// <reference path="../js/constants.js" />
/// <reference path="../js/resourceloader.js" />

var ResourceLoader, resourceLoader, requirejs, ektronjs, extend, constants;

module('resource loader', {
    setup: function () {
        'use strict';
        requirejs = {
            require: function (deps, callback) {
                callback('Test Data');
            }
        };

        ektronjs = function () {
            return ektronjs;
        };

        constants = {
            ResourceType: {
                css: 'css',
                html: 'html',
                text: 'text',
                image: 'image'
            }
        };

        ektronjs.extend = extend = function () {
            var argumentsLength = arguments.length, key, i;
            for (i = 1; i < argumentsLength; i++) { //ignore jslint
                for (key in arguments[i]) {
                    if (arguments[i].hasOwnProperty(key)) {
                        arguments[0][key] = arguments[i][key];
                    }
                }
            }

            return arguments[0];
        };
        ektronjs.prepend = function (text) { };
        ektronjs.append = function (text) { };
        ektronjs.trim = function (str) { return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };
        ektronjs.isFunction = function (obj) {
            return 'function' === typeof (obj);
        };
        ektronjs.isArray = Array.isArray;

        ResourceLoader = window.createModule(ektronjs, constants, requirejs);
        resourceLoader = new ResourceLoader();
    }
});

test('resource loader has  the .load() method', function () {
    'use strict';
    ok('undefined' !== typeof resourceLoader.load, 'resourceLoader has load method');
});

test('loader only calls into requirejs when resources are provided as an array or as a string.', function () {
    'use strict';
    var requireCount = 0;

    requirejs.require = function (deps, callback) {
        requireCount++; //ignore jslint
    };

    resourceLoader = new ResourceLoader();
    resourceLoader.load();
    resourceLoader.load({ files: [] });
    resourceLoader.load({ files: {} });
    resourceLoader.load({ files: 'test' });
    resourceLoader.load({ files: ['test', 'test'] });

    equal(requireCount, 2, 'Require was only called under the correct conditions.');
});

test('loader uses default settings\' selector value (AppWindow) to insert the template', function () {
    'use strict';
    var templateData = '<b>Hello World</b>',
        settings = {
            files: 'helloworld'
        },
        foundCorrectResponseText = false,
        foundDefaultSelector = false,
        foundCorrectTemplateId = false;

    requirejs.require = function (deps, callback) {
        foundCorrectTemplateId = (deps.join() === '_text!views/' + settings.files + '.html');
        callback(templateData);
    };

    ektronjs = function (selector) {
        foundDefaultSelector = (selector === '.ux-appWindow');
        return ektronjs;
    };

    ektronjs.append = function (text) {
        foundCorrectResponseText = (text === templateData);
    };

    ektronjs.trim = function (str) { return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };

    ektronjs.extend = extend;

    ektronjs.isFunction = function (obj) {
        return 'function' === typeof (obj);
    };

    ektronjs.isArray = Array.isArray;

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader();
    resourceLoader.load(settings);

    ok(foundCorrectTemplateId, 'found correct resource from require js');
    ok(foundDefaultSelector, 'default appWindow is found.');
    ok(foundCorrectResponseText, 'found text append to AppWindow.');
});

test('loader prepend templates to body', function () {
    'use strict';
    var templateData = '<b>Hello World</b>',
        bodySelector = 'body',
        foundBodySelector = false,
        foundCorrectResponseText = false,
        settings = {
            files: 'helloworld',
            selector: bodySelector,
            callback: function () { }
        };

    requirejs.require = function (deps, callback) {
        callback(templateData);
    };

    ektronjs = function (selector) {
        foundBodySelector = (selector === bodySelector);
        return ektronjs;
    };
    ektronjs.trim = function (str) { return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };
    ektronjs.extend = extend;
    ektronjs.prepend = function (text) {
        foundCorrectResponseText = (text === templateData);
    };
    ektronjs.isFunction = function (obj) {
        return 'function' === typeof (obj);
    };

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader();
    resourceLoader.load(settings);

    ok(foundBodySelector, 'Body selector called');
    ok(foundCorrectResponseText, 'Template data passed into jQuery');
});

test('Load calls callback only once regardless of the number of files loaded.', function () {
    'use strict';
    var callbackFiredCount = 0;

    ektronjs.extend = extend;

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader();
    resourceLoader.load({
        files: '1, 2, 3',
        callback: function () {
            callbackFiredCount++; //ignore jslint
        }
    });
    ok(callbackFiredCount === 1, 'Callback was fired');
});

test('Load wraps reusable templates in script blocks', function () {
    'use strict';
    var selectedHead = false,
        appendedText = '';

    ektronjs = function (selector) {
        selectedHead = (selector === 'head');
        return ektronjs;
    };
    ektronjs.extend = extend;
    ektronjs.append = function (text) {
        appendedText = text;
    };
    ektronjs.trim = function (str) { return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };
    ektronjs.isFunction = function (obj) {
        return 'function' === typeof (obj);
    };

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader();
    resourceLoader.load({
        files: 'testTemplate1',
        reusable: true,
        callback: function () {
        }
    });

    ok(selectedHead, 'Selected the head tag');
    equal(appendedText, '<script type="text/html" id="testTemplate1">Test Data</script>', 'Appended correct script tag to head');
});

test('Base Url prepends every require path', function () {
    'use strict';
    var templateData = '<b>Hello World</b>',
        options = {
            files: 'helloworld'
        },
        foundCorrectResponseText = false,
        foundDefaultSelector = false,
        foundCorrectTemplateId = false;

    requirejs.require = function (deps, callback) {
        foundCorrectTemplateId = (deps.join() === '_text!applications/app/views/' + options.files + '.html');
        callback(templateData);
    };

    ektronjs = function (selector) {
        foundDefaultSelector = (selector === '.ux-appWindow');
        return ektronjs;
    };
    ektronjs.extend = extend;
    ektronjs.append = function (text) {
        foundCorrectResponseText = (text === templateData);
    };
    ektronjs.trim = function (str) { return str.replace(/^\s\s*/, '').replace(/\s\s*$/, ''); };
    ektronjs.isFunction = function (obj) {
        return 'function' === typeof (obj);
    };

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader('applications/app');
    resourceLoader.load(options);

    ok(foundCorrectTemplateId, 'found correct template Id from require js');
    ok(foundDefaultSelector, 'default appWindow is found.');
    ok(foundCorrectResponseText, 'found text append to AppWindow.');
});

test('Setting type to "css" correctly loads CSS content', function () {
    'use strict';
    var foundDeps = [],
        callbackFiredCount = 0;

    requirejs.require = function (deps, callback) {
        foundDeps = deps;
        callback();
    };

    ektronjs.extend = extend;

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader('applications/app');

    resourceLoader.load({
        files: 'app',
        type: 'css',
        callback: function () {
            callbackFiredCount++; //ignore jslint
        }
    });

    equal(foundDeps, '_css!applications/app/css/app.css', 'Required correct CSS file');
    ok(callbackFiredCount === 1, 'Callback fired');
});

test('Setting type to "img" correctly loads the image content.', function () {
    'use strict';
    var foundDeps = [],
        callbackFiredCount = 0,
        callBackData;

    requirejs.require = function (deps, callback) {
        foundDeps = deps;
        callback();
    };

    ektronjs.extend = extend;

    ResourceLoader = window.createModule(ektronjs, constants, requirejs);
    resourceLoader = new ResourceLoader('applications/app');

    resourceLoader.load({
        files: ['testImage.png', 'tImage.jpg'],
        type: 'image',
        callback: function (data) {
            callbackFiredCount = callbackFiredCount + 1;
        }
    });

    equal(foundDeps[0], '_image!applications/app/img/testImage.png', 'Required correct Image file 1');
    equal(foundDeps[1], '_image!applications/app/img/tImage.jpg', 'Required correct Image file 2');
    ok(callbackFiredCount === 1, 'Callback fired');
});