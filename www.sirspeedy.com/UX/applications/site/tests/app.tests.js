/// <reference path="setup.js" />
/// <reference path="../../../vendor/qunit/js/qunit.js" />
/// <reference path="../js/app.js" />

var ko,
    ektronjs,
    app,
    App,
    AppSettings,
    MobilePreviewConstructorCount = 0,
    labels,
    ToolbarSwitch,
    ToolbarButtonSet,
    childrenCallCount,
    addSwitchCalled,
    addButtonSetCalled,
    eachCount,
    mousemoveCalled,
    mousestopCalled,
    onCalled,
    offCalled,
    removeCallCount,
    returnElement = '<div><div></div></div>',
    getElementWasCalledOnce,
    removeNodeCallCount,
    Dialog,
    Accordion,
    LinkList;

function ResourceType() {
    'use strict';
    this.css = 'css';
    this.html = 'html';
    this.text = 'text';
}

function MobilePreview() {
    'use strict';
    MobilePreviewConstructorCount++; //ignore jslint
}
MobilePreview.prototype.init = function () { };

module('Site Application Core Tests', {
    setup: function () {
        'use strict';
        ko = {
            observable: function () { },
            observableArray: function () { },
            applyBindings: function () { }
        };

        ektronjs = function () {
            return ektronjs;
        };

        ektronjs.html = function (data) { };
        eachCount = 0;
        ektronjs.each = function () {
            eachCount++ //ignore jslint
            return ektronjs;
        };
        ektronjs.find = function () {
            return ektronjs;
        };
        ektronjs.appendTo = function () {
            return ektronjs;
        }

        childrenCallCount = 0;
        ektronjs.children = function () {
            childrenCallCount = childrenCallCount + 1;
            return ektronjs;
        };
        removeCallCount = 0;
        ektronjs.remove = function () {
            removeCallCount++ //ignore jslint
        }
        
        mousestopCalled = 0;
        ektronjs.mousestop = function () {
            mousestopCalled++; //ignore jslint
        };
        onCalled = 0;
        ektronjs.on = function () {
            onCalled++; //ignore jslint
        };
        offCalled = 0;
        ektronjs.off = function () {
            offCalled++; //ignore jslint
        };

        getElementWasCalledOnce = 0;
        removeNodeCallCount = 0;

        AppSettings = {
            toolbar: {},
            resourceLoader: {
                Types: new ResourceType(),
                type: new ResourceType().html,
                load: function () {
                    if ('undefined' !== typeof(arguments[0].callback)) {
                        arguments[0].callback();
                    }
                }
            },
            files: ['app'],
            appWindow: {
                clear: function () { },
                getElement: function (elem) { return {}; },
                show: function () { }
            }
        };

        addSwitchCalled = 0;
        AppSettings.toolbar.addSwitch = function () {
            addSwitchCalled++; //ignore jslint
        };
        addButtonSetCalled = 0;
        AppSettings.toolbar.addButtonset = function () {
            addButtonSetCalled++; //ignore jslint
        };

        labels = {
            'editSwitchOnLabel': 'edit',
            'editSwitchOffLabel': 'view',
            'contentLabel': 'content',
            'designLabel': 'design',
            'switchLanguage': 'Switch Language',
            'switchMobileDevice': 'Switch Mobile Device',
            'switchPersona': 'Switch Persona'
        };

        ToolbarButtonSet = function () {

        };

        ToolbarSwitch = function () {

        };

        Dialog = function () {
            this.element = function () { };
            this.html = function (data) {
                return '<div>test</div>'
            };
        };

        LinkList = function () {

        }

        Accordion = function () {
            this.element = function () {
                return;
            }
            this.panels = function () {
                return [ektronjs, ektronjs];
            };
        };

        App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
        app = new App(AppSettings);
    }
});

test('App exists.', function () {
    'use strict';
    ok('undefined' !== typeof (App), 'Application is defined.');
});

test('App meets the requirements of the application contract.', function () {
    'use strict';
    ok('undefined' !== typeof (app.onOpen), 'onOpen is defined');
    ok('function' === typeof (app.onOpen), 'onOpen is a function.');

    ok('undefined' !== typeof (app.onClose), 'onClose is defined');
    ok('function' === typeof (app.onClose), 'onClose is a function.');
});

test('App loads the necessary views and resources', function () {
    'use strict';
    var appLoadCount = 0,
        mobilePreviewInitCount = 0;
    // mock the resource loader's load function to test incoming variables from our unit test
    AppSettings.resourceLoader.load = function (settings) {
        if (settings.type === AppSettings.resourceLoader.Types.css) {
            if (settings.files[0] === 'app.mobilePreview') {
                appLoadCount++; //ignore jslint
            }
        }
        if (settings.type === AppSettings.resourceLoader.Types.html) {
            if (settings.files[0] === 'app') {
                appLoadCount++; //ignore jslint
            }
            if (settings.files[1] === 'app.carousel') {
                appLoadCount++; //ignore jslint
            }
        }

        if ('function' === typeof (settings.callback)) {
            settings.callback();
        }
    };
    MobilePreview.prototype.init = function () {
        mobilePreviewInitCount++ //ignore jslint
    };

    App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
    app = new App(AppSettings);

    app.onOpen();
    
    equal(appLoadCount, 3, 'The application called the resource loader to load the CSS and necesary views.');
    equal(MobilePreviewConstructorCount, 1, 'The application invoked the MobilePreview object\'s constructor once.');
    equal(mobilePreviewInitCount, 1, 'The application called the initialization method for mobile preview.');
});

test('App cleans up the app window on close.', function () {
    'use strict';
    ko.removeNode = function (element) {
        removeNodeCallCount++; //ignore jslint
    };

    AppSettings.appWindow.getElement = function () {
        getElementWasCalledOnce++; //ignore jslint
        return returnElement;
    };

    App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
    app = new App(AppSettings);
    app.onClose();

    equal(getElementWasCalledOnce, 1, 'appWindow getElement was called once');
    equal(childrenCallCount, 1, 'children was called once.');
    equal(offCalled, 1, '.off() method was called once to remove namespaced bindings.');
});

test('App adds necessaary controls to the toolbar', function () {
    'use strict';
    App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
    app = new App(AppSettings);

    app.onOpen();

    equal(addSwitchCalled, 1, 'toolbar.addSwitch() was called once');
    equal(addButtonSetCalled, 1, 'toolbar.addButtonSet() was called once');
});

test('Mouse bindings applied', function () {
    'use strict';
    App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
    app = new App(AppSettings);

    app.onOpen();

    equal(onCalled, 2, 'on() binding applied');
    equal(mousestopCalled, 1, 'mousestop() binding applied');
});

test('Content headers inserted into the document', function () {
    'use strict';
    var appLoadCount = 0, contentHeaderLoadCount = 0;
    AppSettings.resourceLoader.load = function (settings) {
        if (settings.type === AppSettings.resourceLoader.Types.css) {
            if (settings.files[0] === 'app.mobilePreview') {
                appLoadCount++; //ignore jslint
            }
        }
        if (settings.type === AppSettings.resourceLoader.Types.html) {
            if (settings.files[0] === 'app') {
                appLoadCount++; //ignore jslint
            }
            if (settings.files[1] === 'app.carousel') {
                appLoadCount++; //ignore jslint
            }
            if (settings.files[0] === 'app.contentHeader') {
                contentHeaderLoadCount++; //ignore jslint
            }
        }

        if ('function' === typeof (settings.callback)) {
            settings.callback();
        }
    };

    App = window.createModule(ektronjs, ko, MobilePreview, labels, ToolbarSwitch, ToolbarButtonSet, Dialog, Accordion, LinkList);
    app = new App(AppSettings);

    app.onOpen();

    equal(contentHeaderLoadCount, 1, 'Content header template loaded');
    equal(eachCount, 1, 'A jQuery .each() method is called to loop over the content cotnrols.');
});
