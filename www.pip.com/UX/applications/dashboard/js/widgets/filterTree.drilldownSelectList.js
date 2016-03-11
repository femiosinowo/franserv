/*global console*/
define([
    'Vendor/knockout/knockout',
    './filterTree.drilldownSelectListView'
],
    function (ko, DrilldownSelectListView) {
        'use strict';

        function DrilldownSelectList() {
            var me = this;

            me.update = function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                var options = valueAccessor() || {},
                    // This depends on having filterTree as a parent
                    filterType = bindingContext.$parent.filterType,
                    view;

                if (!viewModel.isNested) {
                    // Initialize state
                    viewModel.state[filterType] = viewModel.state[filterType] || {};

                    view = new DrilldownSelectListView(element, viewModel.state[filterType], viewModel.onSelect);
                    
                    view.attachSelectionsToAncestors();

                    // Bind click to drilldown with UI effect
                    view.clickItem(function (event) {
                        view.drilldown(event, this, viewModel.state[filterType]);
                    });

                    // Restore state if rebinding
                    view.restoreViewState();
                }
            };
        }

        ko.bindingHandlers.drilldownSelectList = ko.bindingHandlers.drilldownSelectList || new DrilldownSelectList();
    }
);