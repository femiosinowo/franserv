/// <reference path="setup.js" />
/// <reference path="../../../knockout/knockout.js" />
/// <reference path="../../../knockout/knockout-mapping.js" />
/// <reference path="../js/toolbar.js" />

var Toolbar, toolbar, resourceLoader, ResourceLoader;
function ToolbarButton(id) {
    'use strict';
    this.id = id;
}
function ToolbarPopOver(id) {
    'use strict';
    this.id = id;
}

function ToolbarSwitch(id) {
    'use strict';
    this.id = id;
}

function ToolbarButtonSet(id) {
    'use strict';
    this.id = id;
}

module('Toolbar Tests', {
    setup: function () {
        'use strict';
        var controls = [];
        resourceLoader = {
            load: function (settings) { settings.callback(); },
            Types: { html: 'html', css: 'css', text: 'text' }
        };
        Toolbar = window.createModule(ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko);

        toolbar = new Toolbar(resourceLoader);
    }
});

test('Add a button to the toolbar', function () {
    'use strict';
    var button = {};
    toolbar.addButton(button);
    ok(toolbar.controls().length > 0, 'Button added to toolbar');
});

test('Add a popover to the toolbar', function () {
    'use strict';
    var popover = {};
    toolbar.addPopOver(popover);
    ok(toolbar.controls().length > 0, 'Popover added to toolbar');
});

test('Add a switch to the toolbar', function () {
    'use strict';
    var toolbarSwitch = {};
    toolbar.addSwitch(toolbarSwitch);
    ok(toolbar.controls().length > 0, 'Switch added to toolbar');
});

test('Add a buttonset to the toolbar', function () {
    'use strict';
    var ToolbarButtonSet = {};
    toolbar.addButtonset(ToolbarButtonSet);
    ok(toolbar.controls().length > 0, 'Buttonset added to toolbar');
});

test('Toolbar loads button templates', function () {
    'use strict';
    var foundName = false, loadedOptions = {};

    resourceLoader.load = function (settings) {
        foundName = foundName || (settings.files[0] === 'ux.toolbar.button');
        if (settings.files[0] === 'ux.toolbar.button') {
            loadedOptions = settings;
        }
        settings.callback();
    };

    toolbar.load([]);

    ok(foundName, 'Loaded toolbar button template');
    ok(loadedOptions.reusable, 'Loaded a reusable template');
});

test('Toolbar loads popover templates', function () {
    'use strict';
    var foundName = false, loadedOptions = {};

    resourceLoader.load = function (settings) {
        foundName = foundName || (settings.files[1] === 'ux.toolbar.popover');
        if (settings.files[1] === 'ux.toolbar.popover') {
            loadedOptions = settings;
        }
        settings.callback();
    };

    toolbar.load([]);

    ok(foundName, 'Loaded toolbar popover template');
    ok(loadedOptions.reusable, 'Loaded a reusable popover template');
});

test('Toolbar loads switch templates', function () {
    'use strict';
    var foundName = false, loadedOptions = {};

    resourceLoader.load = function (settings) {
        foundName = foundName || (settings.files[2] === 'ux.toolbar.switch');
        if (settings.files[2] === 'ux.toolbar.switch') {
            loadedOptions = settings;
        }
        settings.callback();
    };

    toolbar.load([]);

    ok(foundName, 'Loaded toolbar switch template');
    ok(loadedOptions.reusable, 'Loaded a reusable switch template');
});

test('Toolbar loads buttonset templates', function () {
    'use strict';
    var foundName = false, loadedOptions = {};

    resourceLoader.load = function (settings) {
        foundName = foundName || (settings.files[3] === 'ux.toolbar.buttonset');
        if (settings.files[3] === 'ux.toolbar.buttonset') {
            loadedOptions = settings;
        }
        settings.callback();
    };

    toolbar.load([]);

    ok(foundName, 'Loaded toolbar buttonset template');
    ok(loadedOptions.reusable, 'Loaded a reusable buttonset template');
});

test('Toolbar clears button array', function () {
    'use strict';
    toolbar.addButton({
        text: 'abc'
    });

    toolbar.addButton({
        text: 'def'
    });
    equal(toolbar.controls().length, 2, 'Toolbar loaded initial buttons');

    toolbar.clear();

    equal(toolbar.controls().length, 0, 'Toolbar cleared initial buttons');
});

test('Toolbar calls callback after loading', function () {
    'use strict';
    var called = false;

    toolbar.load(function () {
        called = true;
    });

    ok(called, 'Called callback');
});

test('Toolbar addButton sets an ID on the button if it doesn\'t already have one', function () {
    'use strict';
    var button = new ToolbarButton(),
        button2 = new ToolbarButton('');

    toolbar.addButton(button);
    toolbar.addButton(button2);

    equal(button.id, 'ektron-ux-ToolbarButton0', 'Button\'s id is its index in the array of children');
    equal(button2.id, 'ektron-ux-ToolbarButton1', 'Button\'s id is its index in the array of children');
});

test('Toolbar addPopOver sets an ID on the popover if it doesn\'t already have one', function () {
    'use strict';

    var popover = new ToolbarPopOver(),
        popover2 = new ToolbarPopOver();
    
    toolbar.addPopOver(popover);
    toolbar.addPopOver(popover2);

    equal(popover.id, 'ektron-ux-ToolbarPopOver0', 'Popover\'s id is its index in the array of children');
    equal(popover2.id, 'ektron-ux-ToolbarPopOver1', 'Popover\'s id is its index in the array of children');
});

test('Toolbar addSwitch sets an ID on the switch if it doesn\'t already have one', function () {
    'use strict';
    var switch1 = new ToolbarSwitch(),
        switch2 = new ToolbarSwitch('');

    toolbar.addSwitch(switch1);
    toolbar.addSwitch(switch2);

    equal(switch1.id, 'ektron-ux-ToolbarSwitch0', 'Switch\'s id is its index in the array of children');
    equal(switch2.id, 'ektron-ux-ToolbarSwitch1', 'Switch\'s id is its index in the array of children');
});

test('Toolbar addButtonset sets an ID on the buttonset if it doesn\'t already have one', function () {
    'use strict';
    var buttonset = new ToolbarButtonSet(),
        buttonset2 = new ToolbarButtonSet();

    toolbar.addButtonset(buttonset);
    toolbar.addButtonset(buttonset2);

    equal(buttonset.id, 'ektron-ux-ToolbarButtonSet0', 'Buttonset\'s id is its index in the array of children');
    equal(buttonset2.id, 'ektron-ux-ToolbarButtonSet1', 'Buttonset\'s id is its index in the array of children');
});

test('Toolbar addButton converts settings object to ToolbarButton', function () {
    'use strict';
    var foundSettings, settings = {
        text: 'I&rsquo;m a button'
    };

    function ToolbarButton(settings) {
        foundSettings = settings;
    }

    Toolbar = window.createModule(ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko);
    toolbar = new Toolbar(resourceLoader);

    toolbar.addButton(settings);

    equal(toolbar.controls()[0].constructor.name, 'ToolbarButton', 'First control is a toolbar button');
    equal(foundSettings, settings, 'Passed settings into toolbar button');
});

test('Toolbar addPopOver converts settings object to ToolbarPopOver', function () {
    'use strict';
    var foundSettings, settings = {
        text: 'I&rsquo; m a popover'
    };

    function ToolbarPopOver(settings) {
        foundSettings = settings;
    }

    Toolbar = window.createModule(ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko);
    toolbar = new Toolbar(resourceLoader);

    toolbar.addPopOver(settings);

    equal(toolbar.controls()[0].constructor.name, 'ToolbarPopOver', 'First control is a toolbar popover');
    equal(foundSettings, settings, 'Passed settings into toolbar popover');
});

test('Toolbar addSwitch converts settings object to ToolbarSwitch', function () {
    'use strict';
    var foundSettings, settings = {
        cssClass: 'test'
    };

    function ToolbarSwitch(settings) {
        foundSettings = settings;
    }

    Toolbar = window.createModule(ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko);
    toolbar = new Toolbar(resourceLoader);

    toolbar.addSwitch(settings);

    equal(toolbar.controls()[0].constructor.name, 'ToolbarSwitch', 'First control is a toolbar switch');
    equal(foundSettings, settings, 'Passed settings into toolbar switch');
});

test('Toolbar addButtonset converts settings object to ToolbarButtonSet', function () {
    'use strict';
    var foundSettings, settings = {
        cssClass: 'test'
    };

    function ToolbarButtonSet(settings) {
        foundSettings = settings;
    }

    Toolbar = window.createModule(ToolbarButton, ToolbarPopOver, ToolbarSwitch, ToolbarButtonSet, ko);
    toolbar = new Toolbar(resourceLoader);

    toolbar.addButtonset(settings);
    equal(toolbar.controls()[0].constructor.name, 'ToolbarButtonSet', 'First control is a toolbar buttonset');
    equal(foundSettings, settings, 'Passed settings into toolbar buttonset');
});

test('All methods return the toolbar object', function () {
    'use strict';

    var foundPopOver, foundButton, foundSwitch, foundLoad, foundClear, foundButtonset;

    foundButton = toolbar.addButton({});
    foundPopOver = toolbar.addPopOver({});
    foundSwitch = toolbar.addSwitch({});
    foundButtonset = toolbar.addButtonset({});
    foundLoad = toolbar.load();
    foundClear = toolbar.clear();

    equal(toolbar, foundButton, '.addButton() Toolbar object returned is the same as the original toolbar');
    equal(toolbar, foundPopOver, '.addPopOver Toolbar object returned is the same as the original toolbar');
    equal(toolbar, foundSwitch, '.addSwitch() Toolbar object returned is the same as the original toolbar');
    equal(toolbar, foundButtonset, '.addButtonset() Toolbar object returned is the same as the original toolbar');
    equal(toolbar, foundLoad, '.load() Toolbar object returned is the same as the original toolbar');
    equal(toolbar, foundClear, '.clear() Toolbar object returned is the same as the original toolbar');
});
