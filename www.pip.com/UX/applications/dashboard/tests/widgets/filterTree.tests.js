/*global window,module,test,ok,alert*/
/// <reference path="../setup.js" />
/// <reference path="../../js/widgets/filterTree.js" />
var ko, ViewAdapter, element, valueAccessor, allBindingsAccessor, viewModel;

module('Filter Tree', {
    setup: function () {
        'use strict';

        ko = {
            bindingHandlers: {},
            virtualElements: {
                allowedBindings: {},
                firstChild: function (el) { return el; }
            }
        };

        ViewAdapter = function () { };
        ViewAdapter.prototype.click = function () { };
        ViewAdapter.prototype.getFilters = function () { };
        ViewAdapter.prototype.getFilterIndex = function () { return 0; };
        ViewAdapter.prototype.isExpanded = function () { return false; };
        ViewAdapter.prototype.toggleHeaderState = function () { };
        ViewAdapter.prototype.togglePanelState = function () { }

        window.createModule(ko, ViewAdapter);

        element = {};
        valueAccessor = function () { return {}; };
        allBindingsAccessor = {}
        viewModel = {
            expandedFilters: function () { return { foo: { expanded: false}}; },
            filters: [{ filterType: 'foo' }]
        };
    }
});

test('filterTree binding sets up click on filters', function () {
    'use strict';
    var foundElement, filters = {}, foundFilters;

    // Extend ViewAdapter mock to capture test data
    ViewAdapter.prototype.getFilters = function (el) {
        foundElement = el;

        return filters;
    }

    ViewAdapter.prototype.click = function (filters) {
        foundFilters = filters;
    }

    // Perform tested action
    window.createModule(ko, ViewAdapter);
    ko.bindingHandlers.filterTree.update(element, valueAccessor, allBindingsAccessor, viewModel);

    equal(foundElement, element, 'Bound element found and used to get filters.');
    equal(foundFilters, filters, 'Expected filters bound to click event.');
});


test('filterTree toggles state on click', function () {
    'use strict';
    var foundClickCallback = null,
        headersToggled = false,
        panelsToggled = false;

    // Extend ViewAdapter mock to capture click callback
    ViewAdapter.prototype.click = function (filters, callback) {
        foundClickCallback = callback;
    }

    // Capture click callback
    window.createModule(ko, ViewAdapter);
    ko.bindingHandlers.filterTree.update(element, valueAccessor, allBindingsAccessor, viewModel);

    notEqual(foundClickCallback, null, 'Callback provided for filter click');

    // Extend ViewAdapter mock to capture test conditions
    ViewAdapter.prototype.toggleHeaderState = function () {
        headersToggled |= true;
    }

    ViewAdapter.prototype.togglePanelState = function () {
        panelsToggled |= true;
    }

    // Perform tested action with element as "this"
    foundClickCallback.apply(element);

    ok(headersToggled, 'Headers toggled by click event');
    ok(panelsToggled, 'Panels toggled by click event');
});

test('filterTree saves state on click', function () {
    'use strict';
    var foundClickCallback = null,
        filterType = 'test',
        expandedFilters = { };

    expandedFilters[filterType] = { expanded: false };

    viewModel.expandedFilters = function () { return expandedFilters };
    viewModel.filters[0] = {
        filterType: filterType
    }

    // Extend ViewAdapter mock to capture click callback
    ViewAdapter.prototype.click = function (filters, callback) {
        foundClickCallback = callback;
    }

    // Capture click callback
    window.createModule(ko, ViewAdapter);
    ko.bindingHandlers.filterTree.update(element, valueAccessor, allBindingsAccessor, viewModel);

    // Extend ViewAdapter mock to report filter "element" as expanded
    ViewAdapter.prototype.isExpanded = function (el) {
        return el === element;
    }

    // Perform tested action with element as "this"
    foundClickCallback.apply(element);

    ok(expandedFilters[filterType].expanded, 'State dictionary updated to indicate filter was updated.');
});