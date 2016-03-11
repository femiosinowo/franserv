/*global window,module,test,ok,alert*/
/// <reference path='../../../vendor/knockout/knockout.js' />
/// <reference path="setup.js" />
/// <reference path="../js/AppViewModel.js" />

var AppViewModel, viewModel, queryService, queryDataSource, Query, ko, $, SelectMenu, onClose, mapResponseViews;

module('Dashboard View Model', {
    setup: function () {
        'use strict';

        queryService = {
            query: function (query, callback) {
                callback({});
            }
        };

        queryDataSource = {
            all: function () { }
        };

        Query = function (t, f) {
            this.text = t;
            this.filters = [];
            if (typeof f !== "undefined") {
                this.filters = f;
            }
            this.isEmpty = function () {
            };
        };
        Query.clone = function (q) {
            return q;
        }

        //
        // KNOCKOUT - START
        //
        var computedObserved = [];

        function observable() {
            var value = arguments[0], topics = [];

            function notifySubscribers(value, topic) {
                if ('undefined' !== typeof topics[topic]) {
                    for (var i = 0; i < topics[topic].length; i++) {
                        topics[topic][i](value);
                    }
                }
            }

            function property() {
                if (arguments.length > 0) {
                    value = arguments[0];
                    notifySubscribers(value, '');
                } else {
                    computedObserved[computedObserved.length] = property;
                    return value;
                }
            }

            property.subscribe = function (callback, model, topic) {
                if (arguments.length < 3) {
                    topic = '';
                }

                if ('undefined' === typeof topics[topic]) {
                    topics[topic] = [];
                }

                topics[topic].push(callback);
            };

            property.notifySubscribers = notifySubscribers;

            property.push = function (pushed) {
                value.push(pushed);
                property(value);
            };

            property.splice = function (start, end) {
                value.splice(start, end);
                property(value);
            };

            return property;
        }

        function computed() {
            var read = function () { }, write = function (value) { }, subscriptions = [];

            if ("undefined" !== typeof arguments[0]) {
                if ("undefined" !== typeof arguments[0].read) {
                    read = arguments[0].read;
                }

                if ("undefined" !== typeof arguments[0].write) {
                    write = arguments[0].write;
                }
            }

            function callSubscriptions(value) {
                for (var i = 0; i < subscriptions.length; i++) {
                    subscriptions[i](value);
                }
            }

            function property() {
                if (arguments.length > 0) {
                    write(arguments[0]);
                    callSubscriptions(arguments[0]);
                } else {
                    return read();
                }
            }

            property.subscribe = function (callback) {
                subscriptions[subscriptions.length] = callback;

                return {
                    dispose: function () { }
                };
            };

            // Discover component properties
            computedObserved = [];
            read();
            for (var i = 0; i < computedObserved.length; i++) {
                computedObserved[i].subscribe(function () { callSubscriptions(property()); });
            }

            return property;
        }

        ko = {
            applyBindings: function () { },
            observable: observable,
            observableArray: observable,
            computed: computed,
            subscribable: observable,
            bindingHandlers: {}
        };

        ko.bindingHandlers.label = {
            register: function () { }
        };        
        //
        // KNOCKOUT - END
        //

        $ = function () {
            return {
                height: function () { return $(); },
                off: function () { return $() },
                offset: function () { return $(); },
                on: function () { return $(); },
                scroll: function () { return $(); },
                scrollTop: function () { return $(); }
            };
        }

        SelectMenu = function () {
        };
        SelectMenu.SelectionOption = function () { };

        AppViewModel = window.createModule({}, Query, ko, $, SelectMenu);

        onClose = function () { };
        mapResponseViews = function () { };

        viewModel = new AppViewModel(queryService, queryDataSource, onClose, mapResponseViews);
    }
});

test('clicking query button queries service and binds results', function () {
    'use strict';
    var foundQuery, results = [
        { title: 'Bruce', summary: 'Summary for Bruce' }
    ];

    queryService.query = function (query, callback) {
        foundQuery = query;

        callback({ queryResults: results });
    };

    viewModel.queryText('pinkesh!');

    equal(foundQuery.text, 'pinkesh!', "Passed query text from textbox to service");
    deepEqual(viewModel.results(), results, "Bound query service results to the results observable");
});

test('view model loads and binds saved queries upon initialization', function () {
    'use strict';
    var queries = [{ title: 'pinkesh!', 'query': new Query('bruce') }];

    queryDataSource.all = function (callback) {
        callback(queries);
    };

    viewModel = new AppViewModel(queryService, queryDataSource, onClose, mapResponseViews);

    deepEqual(viewModel.queries(), queries, "View model bound queries from the data source to the view");
});

test('view model does not perform an empty query', function () {
    'use strict';
    var queryServiceCalled = false;

    Query = function (t, f) {
        this.text = t;
        this.filters = [];
        if (typeof f !== "undefined") {
            this.filters = f;
        }
        this.isEmpty = function () {
            return true;
        };
    };

    AppViewModel = window.createModule({}, Query, ko, $, SelectMenu);

    queryService.query = function (queryText, callback) {
        queryServiceCalled = true;
    };

    viewModel = new AppViewModel(queryService, queryDataSource);

    viewModel.query(new Query());

    ok(!queryServiceCalled, 'View model does not perform an empty query');
});

test('view model performs first saved query on initialization', function () {
    'use strict';
    var queries = [{ title: 'pinkesh!', 'query': new Query('bruce') }, { title: 'pinkesh2!', 'query': new Query('bruce2') }],
        foundQuery;

    queryDataSource.all = function (callback) {
        callback(queries);
    };

    queryService.query = function (query, callback) {
        foundQuery = query;
    };

    viewModel = new AppViewModel(queryService, queryDataSource);

    deepEqual(foundQuery, queries[0].query, "View model performs first saved query");
});

test('view model loads filters from data source and performs query with filters', function () {
    'use strict';
    var query = new Query('bruce', [{ 'pinkesh': true }]), foundFilters;

    queryDataSource.all = function (callback) {
        callback([{ title: 'pinkesh!', 'query': query }]);
    };

    queryService.query = function (query, callback) {
        foundFilters = query.filters;
    };

    viewModel = new AppViewModel(queryService, queryDataSource);

    deepEqual(foundFilters, query.filters, 'view model performs queries with filters');
});

test('view model executes a query with filters without any text', function () {
    'use strict';
    var queryServiceCalled = false;

    queryService.query = function (query, callback) {
        queryServiceCalled = true;
    };

    viewModel.query(new Query('', [{ 'filtered': true }]));

    ok(queryServiceCalled, 'View model executes filtered queries regardless of text');
});

test('newQuery does not include filters in query', function () {
    'use strict';
    var foundFilters;

    queryService.query = function (query, callback) {
        foundFilters = query.filters;
    };

    viewModel.queryText('bruce');
    viewModel.filters([{ 'pinkesh': true }]);

    viewModel.newQuery();

    deepEqual(foundFilters, [], 'textQuery excludes filters in query');
});

test('query is subscribed to observables', function () {
    'use strict';
    var queryCounter = 0;

    queryService.query = function (query, callback) {
        queryCounter++;
    };

    viewModel.queryText('bruce');
    viewModel.filters([{ 'pinkesh': true }]);
    viewModel.sortOption('joe');

    ok(queryCounter === 3, 'query is called as text, filter and sort observables are updated');
});

test('view model executes callback on close', function () {
    'use strict';
    var closeCalledBack = false;

    viewModel = new AppViewModel(queryService, queryDataSource, function () {
        closeCalledBack = true;
    });

    viewModel.close();

    ok(closeCalledBack, 'view model executes callback to close');
});

test('view model reports loading between query request and response', function () {
    'use strict';
    var loadingStarted = false, loadingStopped = false;

    viewModel.loading = function (value) {
        if ("undefined" !== typeof value) {
            if (value) {
                loadingStarted = true;
            } else {
                loadingStopped = true;
            }
        }
    };

    viewModel.query(new Query('test'));

    ok(loadingStarted && loadingStopped, 'view model reports loading between query request and response');
});

test('Filters update when selection event is published', function () {
    'use strict';
    var expectedFilters = [{ 'filter1': 'value1' }, { 'filter2': 'value2' }];

    viewModel.eventRegistry.notifySubscribers({ action: 'add', data: expectedFilters[0] }, viewModel.eventRegistry.filterSelectionChange);
    viewModel.eventRegistry.notifySubscribers({ action: 'add', data: expectedFilters[1] }, viewModel.eventRegistry.filterSelectionChange);

    deepEqual(viewModel.filters(), expectedFilters, 'Filters added correctly after publication');

    viewModel.eventRegistry.notifySubscribers({ action: 'remove', index: 0 }, viewModel.eventRegistry.filterSelectionChange);

    equal(viewModel.filters().length, 1, 'Filter removed from list');
    deepEqual(viewModel.filters()[0], expectedFilters[1], 'Correct filter removed from list');
});

test('View model publishes the expected actions for a filter selection', function () {
    'use strict';
    var $extend,
        preselectedChild = {
            type: 'taxonomy',
            value: 'baby goat'
        },
        filters = [{
            filterName: 'taxonomy',
            filterValue: 'baby goat'
        }],
        selection = {
            childSelected: true,
            type: 'taxonomy',
            value: 'valval'
        },
        event = { target: {} },
        foundActions;

    // Mock out jQuery to return selected children data on target parent
    $extend = function (args) {
        var $ref = $();

        if (args === event.target) {
            $ref.parents = function () {
                return {
                    eq: function (index) {
                        return {
                            data: function () {
                                return index === 0 ? [preselectedChild] : null;
                            }
                        }
                    }
                };
            };
        }

        return $ref;
    };

    // Inject jQuery and recreate the view model
    AppViewModel = window.createModule({}, Query, ko, $extend, SelectMenu);

    viewModel = new AppViewModel(queryService, queryDataSource, onClose, mapResponseViews);

    // Update filters with our test data
    viewModel.filters(filters);

    // Receive the publication to verify the list of actions
    viewModel.eventRegistry.subscribe(
        function (actions) {
            foundActions = actions;
        },
        {},
        viewModel.eventRegistry.filterSelectionChange);

    // Perform the tested action
    viewModel.publishFilterSelection(selection, event);

    deepEqual(foundActions, [{ action: 'remove', index: 0 }], 'View model publishes the expected actions for a filter selection');
});

test('view model performs topic query', function () {
    'use strict';
    var topic = [{ title: 'topic title', 'query': new Query('topic query') }],
        foundTopic;

    queryDataSource.all = function (callback) {
        callback(topic);
    };

    queryService.query = function (query, callback) {
        foundTopic = topic.query;
    };

    viewModel = new AppViewModel(queryService, queryDataSource);

    deepEqual(foundTopic, topic.query, "View model performs topic query");
});