/// <reference path="../../Vendor/Ektron/UX/js/widgets/ux.selectmenu.js" />
/*global Ektron,alert,console*/
define([
    '_i18n!./nls/labels',
    './Query',
    'Vendor/knockout/knockout',
    'ektronjs',
    'Vendor/Ektron/UX/js/widgets/ux.selectmenu',
    './widgets/throbber',
    './ko.for',
    '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min',
    'Vendor/jQuery/UI/js/jquery-ui-complete.min'
],
    function (labels, Query, ko, $, SelectMenu) {
        'use strict';

        function AppViewModel(queryService, queryDataSource, onClose, mapResponseViews) {
            var me = this;

            //
            // Properties
            //
            me.isAllContent = false;
            me.labels = ko.observableArray();
            me.queries = ko.observableArray();
            me.queryTextInput = ko.observable('');
            me.queryText = ko.observable('');
            me.error = ko.observable(null);
            me.filters = ko.observableArray([]);
            me.checkedFacet = ko.observable(false);
            me.viewmode = ko.observable('ux-app-dashboard-result-view-grid');
            me.results = ko.observableArray();
            me.response = ko.observable({});
            me.sortOption = ko.observable();
            me.orderAscending = ko.observable();
            me.nextPageUrl = ko.observable();
            me.loading = ko.observable(false);
            me.querySubscription = {};
            me.scrollAjaxPending = false;
            me.eventRegistry = new ko.subscribable();
            me.eventRegistry.filterSelectionChange = 'filterSelectionChange';
            me.expandedFilters = ko.observable({});
            me.viewModeIcons = [];

            //
            // Methods
            //

            me.close = onClose;

            me.clearError = function () {
                me.error(null);
            };

            me.deferredTabifyTopics = function () {
                setTimeout(me.tabifyTopics, 1);
            };

            // Explicit query call -- USE OBSERVABLE me.query 
            me.executeQuery = function () {
                if (!me.isAllContent) {
                me.unselectAllTabs();
                }
                me.isAllContent = false;
                if (!me.query().isEmpty()) {
                    me.loading(true);
                    queryService.query(
                        me.query(),
                        function (response) {
                            me.clearError();
                            me.loading(false);

                            mapResponseViews(response);

                            me.results(response.queryResults);
                            me.response(response);

                            $('div.ux-app-dashboard div.scroll-panel').off('scroll.dashboard').on('scroll.dashboard', function () {
                                if (!me.scrollAjaxPending) {
                                    if (me.isTargetVisible()) {
                                        me.getResults();
                                    }
                                }
                            });

                            $('div.ux-app-dashboard div.scroll-panel').scrollTop(0);
                        },
                        function (msg) {
                            me.showError(me.labels().QueryError, msg);
                            me.loading(false);
                        });
                }
                else {
                    me.response({});
                    me.results([]);
                }
            };

            me.executeTopic = function (data) { 
                me.query(Query.clone(data.query));
            };

            me.getFilterIndex = function (filter) {
                var index = -1;

                for (var i in me.filters()) {
                    if (me.filters()[i].filterValue === filter.value && me.filters()[i].filterName === filter.type) {
                        index = i;
                    }
                }

                return parseInt(index);
            };

            me.getIconTemplate = function (data) {
                // Default to generic
                var template = 'ux-icon-type',
                    defaultTemplate = template + '1';

                template += data.type;

                if (data.subtype === '2') {
                    template += '_2';
                }

                if ((data.type === '101' || data.type === '9') && data.subtype === '0') {
                    // office documents or library office documents
                    if (data.extension != null && data.extension.length > 0) {
                        // has a usable file extension
                        template += '_0_';
                        var defaultValue = data.type === '101'
                            ? 'contenttype101' // generic office document
                            : 'contenttype9';  // library document (non-image)
                        
                        switch (data.extension) {
                            
                            // word document
                            case 'doc':
                            case 'docm':
                            case 'docx':
                            case 'dot':
                            case 'dotm':
                            case 'dotx':
                                template += 'doc';
                                break;

                            // project document
                            case 'mpd':
                            case 'mpp':
                            case 'mpt':
                                template += 'mpp';
                                break;

                            // power-point document
                            case 'ppt':
                            case 'pptm':
                            case 'pptx':
                            case 'pot':
                            case 'potm':
                            case 'potx':
                            case 'pps':
                            case 'ppsm':
                            case 'ppsx':
                                template += 'ppt';
                                break;

                            // publisher document
                            case 'pub':
                            case 'puz':
                                template += 'pub';
                                break;

                            // visio document
                            case 'vdx':
                            case 'vsd':
                            case 'vss':
                            case 'vst':
                            case 'vsx':
                            case 'vtx':
                                template += 'vsd';
                                break;

                            // excel document
                            case 'xl':
                            case 'xlc':
                            case 'xlm':
                            case 'xls':
                            case 'xlsb':
                            case 'xlsm':
                            case 'xlsx':
                            case 'xlt':
                            case 'xltm':
                            case 'xltx':
                            case 'xlw':
                                template += 'xls';
                                break;
                            
                            default:
                                template += defaultValue;
                                break;
                        }
                    }
                }

                if ($('#' + template).length == 0) {
                    template = defaultTemplate;
                }

                return template;
            };

            me.getResults = function () {
                //call the web api to get next set of results
                if (me.response().atLastPage === false) {
                    me.scrollAjaxPending = true;
                    $.ajax({
                        url: me.response().nextPageLink,
                        dataType: 'json',
                        type: 'GET',
                        success: function (response) {
                            me.clearError();

                            mapResponseViews(response);

                            me.results.push.apply(me.results, response.queryResults);
                            me.scrollAjaxPending = false;

                            me.response(response);
                        },
                        error: function (jqXHR, status, msg) {
                            me.showError(me.labels().NextPageScrollError, msg);
                            me.scrollAjaxPending = false;
                        }
                    });
                }
            };

            me.getviewmode = function () {
                return me.viewmode();
            };

            me.isSlideRequired = function () {
                var liWidth = 0;
                
                var ulWidth = $('#toggle ul').width();

                $('#toggle ul li[style!="display: none;"]').each(function () {
                    var margin = parseInt($(this).css('margin-right'));
                    liWidth += $(this).width() + margin;
                });
                return ulWidth < liWidth;
            };

            me.isTargetVisible = function () {
                var isVisible = false;
                var container = $('div.ux-app-dashboard div.scroll-panel');
                var height = parseInt(container.height(), 10);
                var scrollHeight = parseInt(container.get(0).scrollHeight, 10);
                var scrollTop = parseInt(container.scrollTop(), 10);

                var distanceToBottom = (scrollHeight - scrollTop) - height;

                if ($('.ux-throbber-wrapper').length !== 0) {
                    if (distanceToBottom <= 30) {
                        isVisible = true;
                    }
                }

                return isVisible;
            };

            // Perform a new query based on the text input
            me.newQuery = function () {
                var query = new Query(me.queryTextInput());

                if (!query.isEmpty()) {
                    me.results([]);

                    me.query(query);
                }
            };

            me.nextTab = function () {
                
                var isSlideRequired = me.isSlideRequired();
                var hiddenTabs = $('#toggle li[style*="display: none;"]');
                if (isSlideRequired) { 
                    var oneVisibleTab = $('#toggle li[style!="display: none;"]');
                    if (oneVisibleTab.length === 1) {
                    }
                    else if (hiddenTabs.length === 0) {
                        $('#toggle li:first').toggle('slide', { direction: 'left' });
                    }
                    else {
                        $(hiddenTabs[hiddenTabs.length - 1]).next().toggle('slide', { direction: 'left' });
                    }
                }
            };
            me.previousTab = function () {
                
                var isSlideRequired = me.isSlideRequired();
                var hiddenTabs = $('#toggle li[style*="display: none;"]'); 
                if (isSlideRequired) {
                    var firstTabVisible = $('#toggle li:first').is(':visible');
                    if (!firstTabVisible) {
                        $(hiddenTabs[hiddenTabs.length - 1]).toggle('slide', { direction: 'left' });
                    }
                }
                else {
                    $(hiddenTabs[hiddenTabs.length - 1]).toggle('slide', { direction: 'left' });
                }
            };

            me.publishFilterSelection = function (filter, event) {
                
                var key, selectedChildValues, actions = [], removalIndices = [];

                // Queue all child selections for removal
                if (filter.childSelected && !filter.selected) {
                    selectedChildValues = $(event.target).parents('li').eq(0).data('selectedChildValues') || [];

                    for (var i in selectedChildValues) {
                        removalIndices.push(me.getFilterIndex(selectedChildValues[i]));
                    }
                }

                // Queue action for selected filter
                if (filter.selected) {
                    removalIndices.push(me.getFilterIndex(filter));
                } else if (!filter.childSelected) {
                    actions.push({ action: 'add', data: { filterName: filter.type, filterValue: filter.value } });
                }

                // Sort removals by index
                removalIndices = removalIndices.sort();

                // Prepend removals to actions to be in descending order before any add
                for (var i in removalIndices) {
                    actions.unshift({ action: 'remove', index: removalIndices[i] });
                }
                
                // The number of pills in the pills pane.
                var pillsCount = me.response().activeFilters ? me.response().activeFilters.length : 0;
                
                // Check if the number of pills is one, and the action is remove and the search box is empty.
                if (pillsCount == 1 && actions[0].action == 'remove' && me.queryText() == '') {
                    // Programatically click the All Content tab and set it as the active tab.
                    var allContentTab = $('li.ux-topic-allcontent a');
                    allContentTab.click();
                    me.isAllContent = true;
                }

                // Notify subscribers of filter selection change of the actions
                me.eventRegistry.notifySubscribers(actions, me.eventRegistry.filterSelectionChange);
            };

            me.redrawViewModeButton = function () {
                var select = $(this || arguments[0]);
                var button = select.next();
                var sash = $('#' + select.attr('id') + '-menu').parent();

                // Insert view mode icon
                button.children('.ui-selectmenu-text').html(me.viewModeIcons[button.text()]);

                // Clear the width set by the plugin
                button.css({ width: 'inherit' });
                sash.css({ width: 'inherit' });;
            };

            me.redrawViewModeChoices = function () {
                var select = $(this || arguments[0]);
                var items = $('#' + select.attr('id') + '-menu').find('li.ui-menu-item a');

                items.each(function () {
                    var item = $(this);

                    item.html(me.viewModeIcons[item.text()]);
                });
            };

            me.removeFilter = function (filter) {
                me.filters.remove(filter);
            };

            me.showError = function (i18nMessage, raw) {
                me.error(i18nMessage);

                if ("undefined" !== typeof raw && "undefined" !== typeof console) {
                    console.log(raw);
                }
            };

            me.tabifyTopics = function () {
                me.topicTabs = $('#topics').tabs({ collapsible: true });
            };

            me.unselectAllTabs = function () {
                if ('undefined' != typeof (me.topicTabs)) {
                    me.topicTabs.tabs({ active: false, hide: false });

                    // Don't let jQuery deactivate the spacer panel
                    $('#emptyPanel').show();
                }
            };

            //
            // Constructor logic
            //

            // Set the icon HTML for the different view modes
            me.viewModeIcons[labels.ViewGrid] = '<span data-ux-icon="&#xe004" title="' + labels.ViewGrid + '"></span>';
            me.viewModeIcons[labels.ViewTile] = '<span data-ux-icon="&#xe002" title="' + labels.ViewTile + '"></span>';
            me.viewModeIcons[labels.ViewLine] = '<span data-ux-icon="&#xe003" title="' + labels.ViewLine + '"></span>';

            // Build model for the view mode select menu
            me.viewmodeSettings = new SelectMenu({
                options: {
                    open: me.redrawViewModeChoices,
                    select: me.redrawViewModeButton,
                    change: me.redrawViewModeButton
                },
                contents: [
                    new SelectMenu.SelectionOption('ux-app-dashboard-result-view-grid', labels.ViewGrid),
                    new SelectMenu.SelectionOption('ux-app-dashboard-result-view-tile', labels.ViewTile),
                    new SelectMenu.SelectionOption('ux-app-dashboard-result-view-line', labels.ViewLine)
                ],
                selectedValue: me.viewmode
            });

            // Build model for the sort order select menu
            me.sortSettings = new SelectMenu({
                contents: [
                    new SelectMenu.SelectionOption('MostRelevant', labels.MostRelevant),
                    new SelectMenu.SelectionOption('DateModifiedDescending', labels.DateModifiedDescending),
                    new SelectMenu.SelectionOption('DateModifiedAscending', labels.DateModifiedAscending),
                    new SelectMenu.SelectionOption('TitleAscending', labels.TitleAscending),
                    new SelectMenu.SelectionOption('TitleDescending', labels.TitleDescending)
                ],
                selectedValue: me.sortOption
            });

            // Compute observable for query object
            me.query = ko.computed({
                read: function () {
                    return new Query(me.queryText(), me.filters(), me.sortOption(), me.orderAscending());
                },
                write: function (value) {
                    // Dispose of subscription to prevent multiple calls as we update
                    me.querySubscription.dispose();

                    // Update the constituent values
                    me.queryText(value.text);
                    me.filters(value.filters);

                    // logic should probably live in another location
                    var orderByValue;

                    if (value.orderBy == 'datemodified') {
                        orderByValue = (value.orderAscending) ? 'DateModifiedAscending' : 'DateModifiedDescending';
                    }
                    else if (value.orderBy == 'title') {
                        orderByValue = (value.orderAscending) ? 'TitleAscending' : 'TitleDescending';
                    }
                    else {
                        orderByValue = 'MostRelevant';
                    }

                    me.sortOption(orderByValue);
                    me.orderAscending(value.orderAscending);

                    // Recreate and call the subscription
                    me.querySubscription = me.query.subscribe(me.executeQuery);
                    me.executeQuery();
                }
            });

            // Compute observables for loading status
            me.loadingRefinement = ko.computed(function () { return me.loading() && me.results().length > 0; });
            me.atLastPageLoading = ko.computed(function () { return !me.response().atLastPage; });

            // Text i18n
            me.labels(labels);
            ko.bindingHandlers.label.register(labels);

            // Implicit query on observable change
            me.querySubscription = me.query.subscribe(me.executeQuery);

            // Overwrite query text input with hidden field
            me.queryText.subscribe(function (value) {
                me.queryTextInput(value);
            });

            // Load content topics and default query
            me.loading(true);
            queryDataSource.all(
                function (queries) {
                    me.queries(queries);

                    me.executeTopic(queries[0]);
                },
                function (msg) {
                    me.showError(me.labels().TopicsError, msg);
                    me.loading(false);
                });

            // Listen for filter selection changes to update filters
            me.eventRegistry.subscribe(
                function (actions) {
                    // Act on underlying array of the obvservable
                    var filterArray = me.filters();

                    // Convert single action calls to an array
                    if ('undefined' === typeof actions.length) {
                        actions = [actions];
                    }
                    for (var i in actions) {
                        if (actions[i].action === 'add') {
                            filterArray.push(actions[i].data);
                        } else if (actions[i].action === 'remove') {
                            filterArray.splice(actions[i].index, 1);
                        }
                    }

                    // Update observable as having mutated
                    me.filters(filterArray);
                },
                me,
                me.eventRegistry.filterSelectionChange
            );
        }

        return AppViewModel;
    }
);
