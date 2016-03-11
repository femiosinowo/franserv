/*global window,module,test,ok,alert*/
/// <reference path="../setup.js" />
/// <reference path="../../js/widgets/filterTree.drilldownSelectList.js" />
var ko, ViewAdapter, element, valueAccessor, allBindingsAccessor, viewModel, bindingContext,
    foundElement, foundState, foundOnSelect;

module('Filter Tree Drilldown Select List', {
    setup: function () {
        'use strict';

        ko = {
            bindingHandlers: {}
        };
         
        // Mocked to capture constructor arguments here, rather than redefining entire class in tests
        ViewAdapter = function (element, state, onSelect) {
            foundElement = element;
            foundState = state;
            foundOnSelect = onSelect;
        };
        ViewAdapter.prototype.attachSelectionsToAncestors = function () { };
        ViewAdapter.prototype.clickItem = function () { };
        ViewAdapter.prototype.drilldown = function () { return false; };
        ViewAdapter.prototype.restoreViewState = function () { };

        window.createModule(ko, ViewAdapter);

        element = {};
        valueAccessor = function () { return {}; };
        allBindingsAccessor = {}
        viewModel = {
            state: {}
        };
        bindingContext = {
            $parent: {
                filterType: ''
            }
        };
    }
});

test('Click on an item drills down', function () {
    'use strict';
    var foundClickCallback = null,
        drilledDown = false;

    // Extend view adapater mock to capture click callback
    ViewAdapter.prototype.clickItem = function (callback) {
        foundClickCallback = callback;
    }

    // Extend view adapater mock to test drilling down
    ViewAdapter.prototype.drilldown = function () {
        drilledDown |= true;
    }

    window.createModule(ko, ViewAdapter);
    ko.bindingHandlers.drilldownSelectList.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    notEqual(foundClickCallback, null, 'drilldownSelectList binding sets up click event handler on items.');

    // Perform tested action
    foundClickCallback();

    ok(drilledDown, 'Callback for click event on an item drills down into the list.');
});

test('State is restored on update', function () {
    'use strict';
    var filterType = 'test', state = {}, restoreViewStateCalled = false;

    ViewAdapter.prototype.restoreViewState = function () {
        restoreViewStateCalled |= true;
    };

    // Extend view model and binding context to have state for test filter type
    bindingContext.$parent.filterType = filterType;
    viewModel.state[filterType] = state;

    window.createModule(ko, ViewAdapter);
    ko.bindingHandlers.drilldownSelectList.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

    equal(foundState, state, 'Previous state is passed to view adapter.');
    ok(restoreViewStateCalled, 'Restore view state called on the view adapter.');
});


// Untested, required behavior
// * Drilldown builds/updates breadcrumbs
// * Breadcrumb click restores state
// * Select fires onSelect
// * Select deselects descendants
// * Selected children are accessible to select event consumers