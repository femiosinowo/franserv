/*global console*/
define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_i18n!./nls/labels',
    '_text!../../views/widgets/filterTree.html',
    '_css!../../css/widgets/filterTree',
    './filterTree.drilldownSelectList',
    '_css!UX/css/ux.application.core',
    '../ko.label'
],
    function ($, ko, labels, template) {
        'use strict';

        // Register the template in the head
        if ($('#filterTree').length === 0) {
            $('head').append('<script type="text/html" id="filterTree">' + template + '</script>');
        }

        // Register our labels for the view
        ko.bindingHandlers.label.register(labels, 'filterTree');

        //
        // View Adapter
        //
        function FilterTreeView() {
        }

        FilterTreeView.prototype.click = function (element, callback) {
            var me = this;

            element = $(element);

            element.off('click.filterTree');
            element.on('click.filterTree', callback);
        };

        FilterTreeView.prototype.getFilters = function (handle) {
            var me = this;

            return $(handle).siblings('.filterHeader').andSelf();
        };

        FilterTreeView.prototype.getFilterIndex = function (filter) {
            var me = this;

            return me.getFilters(filter).index(filter);
        };

        FilterTreeView.prototype.isExpanded = function (filter) {
            var me = this;

            return $(filter).hasClass('expanded');
        };

        FilterTreeView.prototype.toggleHeaderState = function (header) {
            var me = this;

            $(header).toggleClass('expanded ui-accordion-header-active ui-state-active');
            $(header).find('.ui-accordion-header-icon').toggleClass('ui-icon-triangle-1-e ui-icon-triangle-1-s');
        };

        FilterTreeView.prototype.togglePanelState = function (filter) {
            var me = this;

            $(filter).next('.filterPanel').slideToggle();
        };

        return FilterTreeView;
    }
);