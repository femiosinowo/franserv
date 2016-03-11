/*global console*/
define([
    'Vendor/knockout/knockout',
    './filterTreeView'
],
    function (ko, FilterTreeView) {
        'use strict';

        function FilterTree(view) {
            var me = this;

            me.view = view;

            me.update = function (element, valueAccessor, allBindingsAccessor, viewModel) {
                var tree = ko.virtualElements.firstChild(element) || element.firstChild,
                    filters;

                if (tree !== null) {
                    filters = view.getFilters(tree);

                    view.click(filters, function () {
                        var data = viewModel.filters[view.getFilterIndex(this)];

                        view.toggleHeaderState(this);

                        // Update state dictionary with whether the clicked filter type is expanded or not
                        viewModel.expandedFilters()[data.filterType] = viewModel.expandedFilters()[data.filterType] || {};
                        viewModel.expandedFilters()[data.filterType].expanded = view.isExpanded(this);

                        view.togglePanelState(this);
                    });
                }
            }
        }

        ko.bindingHandlers.filterTree = ko.bindingHandlers.filterTree || new FilterTree(new FilterTreeView());

        // Enable this for virtual elements
        ko.virtualElements.allowedBindings.filterTree = true;
    }
);