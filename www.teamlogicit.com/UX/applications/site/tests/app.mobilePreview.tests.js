/// <reference path="setup.js" />
/// <reference path="../js/app.mobilePreview.js" />

var ko, ektronjs, mobilePreview, MobilePreview, mpSettings, config, labels, nicescroll;
function ResourceType() {
    'use strict';
    this.css = 'css';
    this.html = 'html';
    this.text = 'text';
}

function ToolbarButton(settings) {
    'use strict';
    this.id = '';
    this.options = {
        disabled: false,
        title: '',
        text: 'Button',
        icon: ''
    };
    this.baseCssClass = 'button';
    this.template = '';
    this.getIcon = function () { },
    this.getClass = function () { return this.baseCssClass; };
}

function ToolbarPopOver(id) {
    'use strict';
    ToolbarButton.call(this, id);

    this.dropbox = {
        visible: false,
        top: '50px'
    };
    this.baseCssClass = 'button popover';
    this.template = '';
    this.getClass = function () { return this.baseCssClass; };
}

ToolbarPopOver.prototype = new ToolbarButton();
ToolbarPopOver.prototype.constructor = ToolbarPopOver;

function ApiResource(name, verbs) {
    'use strict';
    this.availableVerbs = verbs;
    this.resourceName = name;
}

module('Site Application Core Tests', {
    setup: function () {
        'use strict';
        ko = {
            computed: function () { },
            observable: function () { },
            observableArray: function () { },
            applyBindings: function () { },
            utils: {
                arrayForEach: function (array, action) {
                    /*ignore jslint start*/
                    for (var i = 0, j = array.length; i < j; i++) {
                    /*ignore jslint end*/
                        action(array[i]);
                    }
                }
            },
            mapping: {
                fromJS: function () { }
            }
        };

        ektronjs = function () {
            ektronjs.append = ektronjs;

            return ektronjs;
        };

        config = {
            'serverSettings': {
                'domain': 'localhost:50094',
                'apiResources': {
                    'previewDevicesApi': new ApiResource('mockPreviewDeviceApiResponse', [
                        'GET'
                    ])
                }
            }
        };

        labels = {
            'switchLanguage': 'Switch Language',
            'switchMobileDevice': 'Switch Mobile Device',
            'switchPersona': 'Switch Persona'
        };

        mpSettings = {
            toolbar: {},
            resourceLoader: { Types: new ResourceType(), type: new ResourceType().html },
            appWindow: {
                clear: function () { },
                getElement: function (elem) { return {}; },
                show: function () { }
            },
            previewTemplate: '',
            carouselTemplate: ''
        };

        nicescroll = {};

        MobilePreview = window.createModule(ektronjs, ToolbarPopOver, config, labels, nicescroll, ko);
        mobilePreview = new MobilePreview(mpSettings);
    }
});

test('MobilePreview exists.', function () {
    'use strict';
    ok('undefined' !== typeof (MobilePreview), 'MobilePreview is defined.');
});

test('MobilePreview adds popover to the toolbar', function () {
    'use strict';
    var createButtonCount = 0,
        popOverPanelHtmlSet = false;

    ektronjs.html = function () {
        popOverPanelHtmlSet = true;
    };

    ektronjs.ajax = function (options) {
        options.statusCode['200']('mockData');
    };

    ektronjs.children = function () {
        return [0];
    };

    ektronjs.jcarousel = function () { };

    mpSettings.toolbar.addPopOver = function () {
        /*ignore jslint start*/
        createButtonCount++
        /*ignore jslint end*/
    };

    MobilePreview = window.createModule(ektronjs, ToolbarPopOver, config, labels, nicescroll, ko);
    mobilePreview = new MobilePreview(mpSettings);
    mobilePreview.init();

    equal(createButtonCount, 1, 'MobilePreview added popover to the toolbar.');
    ok(popOverPanelHtmlSet, 'Popover panel HTML set to template');
});

test('Mobile Preview cookie', function () {
    
    var App = window.createModule(),
        aCookie,
        aDocument = {
            cookie: {}
        },
        app = new App({ appWindow: { hide: function () { } }, document: aDocument });
    app.close();
    equal(aDocument.cookie, 'ektMobilePreview=;Max-Age=0', 'cookie is destroyed on close');
    
});

test('Mobile Preview cookie', function () {
    var App = window.createModule(),
        expected,
        aDocument = {
            cookie: {}
        },
        app = new App({ document: aDocument });
    app.dvcResWidth = function () { return 'rwidth' };
    app.dvcResHeight = function () { return 'rheight' };
    app.dvcWidth = function () { return 'width' };
    app.dvcHeight = function () { return 'height' };
    app.dvcUserAgent = function () { return 'agent' };
    app.setCookieDeviceInfo();
    expected = 'ektMobilePreview=dvcResWidth=rwidth&dvcResHeight=rheight&dvcWidth=width&dvcHeight=height&dvcUserAgent=' + encodeURIComponent(app.dvcUserAgent()) + '; path=/'
    equal(aDocument.cookie, expected, 'is set with the correct dimension values');
});
