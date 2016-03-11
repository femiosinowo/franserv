/*global console*/
define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_i18n!./nls/labels',
    '_text!../../views/widgets/filterTree.drilldownSelectList.html',
    '../ko.label',
    '../ko.for'
],
    function ($, ko, labels, template) {
        'use strict';

        // Register the template in the head
        if ($('#drilldownSelectList').length === 0) {
            $('head').append('<script type="text/html" id="drilldownSelectList">' + template + '</script>');
        }

        // Register our labels for the view
        ko.bindingHandlers.label.register(labels, 'drilldownSelectList');

        // View adapter
        function DrilldownSelectListView(element, state, onSelect) {
            var me = this;

            me.element = $(element);
            me.onSelect = onSelect;
            me.state = state;

            me.state.activeLevelSelector = me.state.activeLevelSelector || '';
        }

        // Private method: hidden lexically, accepts "this" as "me" parameter
        function drilldown(me, li) {
            li = $(li);

            var ul = li.parent(),
                button,
                breadcrumbTitleHtml = '<span>{0}</span>',
                next = li.children('ul'),
                breadcrumb = ul.parents('.filterPanel').children('.breadcrumb');

            // Update our state
            me.state.activeLevelSelector += 'li > .title:contains("' + li.children('.title').text().replace(/"/g, '\\"') + '") ~ ul ';

            // Select current title label to become our restore button
            button = breadcrumb.children().last();

            // If this is the first drilling down, insert a title label for the top-level
            if (button.length === 0) {
                button = $(breadcrumbTitleHtml.replace(/\{0\}/g, labels.All))
                    .appendTo(breadcrumb)
                    .click(function () { breadcrumb.hide(); })
                    .data('state', '');
            }

            // Convert the current title label into a button to bring back the departing view
            $(button)
                .addClass('ux-button')
                .data('html', ul.show().clone())

                // On button click, show this level as the active view
                .on('click.breadcrumb', function () {
                    // Remove the buttons for child nodes
                    $(this).nextAll().remove();

                    // Return button to a title label
                    $(this)
                        .removeClass('ux-button')
                        .off('click.breadcrumb');

                    // Restore active list state                                
                    me.state.activeLevelSelector = $(this).data('state');

                    // Restore the HTML from the associated view as the drilldown
                    var html = $(this).data('html');
                    $(this).parent().next('ul.drilldownSelectList').replaceWith(html);

                    me.attachSelectionsToAncestors(html);

                    // Rebind the checkbox change event to the onSelect handler
                    $(html).find(':checkbox').on('change', function (event) {
                        me.onSelect($(this).data('filter'), event);
                    });

                    // Rebind the drilldown event since we render from HTML
                    $(html).find('li:has(ul)').click(function (event) {
                        me.drilldown(event, this);
                    });
                });

            // Append breadcrumb indicator
            $(breadcrumb).append('<span data-ux-icon="&#xe00f;" />');

            // Append a title label for the current view
            $(breadcrumbTitleHtml.replace(/\{0\}/g, li.children('.title').text()))
                .appendTo(breadcrumb)
                .data('state', me.state.activeLevelSelector);

            // Show the breadcrumbs
            breadcrumb.show();

            // Perform the actual switch to the next level
            ul.replaceWith(next.addClass('drilldownSelectList'));
        };

        //
        // Public Methods
        //

        // Mark which descendants, if any, are selected on an LI
        // or the entire list if no item is provided
        DrilldownSelectListView.prototype.attachSelectionsToAncestors = function (item) {
            var me = this;

            (item || me.element).find('label.selected').each(function () {
                var target = this;

                // Attach to all ancestor LIs within the drilldown
                $(this).parentsUntil('ul.drilldownSelectList', 'li').each(function () {
                    var selectedChildValues = me.getSelectedChildValues(this);

                    // Add the target's filter info to the LI's list of selected children
                    selectedChildValues.push(
                    {
                        type: $(target).siblings('input[type="hidden"][name="type"]').val(),
                        value: $(target).siblings('input[type="hidden"][name="value"]').val()
                    });

                    // Update the list of selected children on the LI
                    $(this).data('selectedChildValues', selectedChildValues);
                });
            });
        };

        DrilldownSelectListView.prototype.clickItem = function (callback) {
            var me = this;

            me.element.find('li:has(ul)').click(callback);
        };

        // Wrapper for private drilldown to add UI effects
        DrilldownSelectListView.prototype.drilldown = function (event, li) {
            var me = this;

            var li = $(li),
                ul = li.parent();

            // Don't drill down if action is on the checkbox (or its label)
            if ($(event.target).parentsUntil(li).andSelf().filter('label,:checkbox').length === 0) {
                ul.hide('slide', function () { drilldown(me, li); });
            }
        };

        DrilldownSelectListView.prototype.getSelectedChildValues = function (element) {
            var me = this;

            return $(this).data('selectedChildValues') || [];
        };

        DrilldownSelectListView.prototype.restoreViewState = function () {
            var me = this;

            if (me.state.activeLevelSelector !== '') {
                var activeItems;

                // Get the path to drill down
                activeItems = me.element.find(me.state.activeLevelSelector).parentsUntil('ul.drilldownSelectList', 'li').toArray().reverse();

                // The current state selector will be rebuilt as we drill back in
                me.state.activeLevelSelector = '';

                // Drill into the list
                $.each(activeItems, function () {
                    drilldown(me, this);
                });
            }

        }

        return DrilldownSelectListView;
    }
);