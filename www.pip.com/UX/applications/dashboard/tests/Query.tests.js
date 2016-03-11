/*global module,test,ok*/
/// <reference path="setup.js" />
/// <reference path="../js/Query.js" />

var Query, query;

module('Query', {
    setup: function () {
        'use strict';
        Query = window.createModule();

        query = new Query();
    }
});

test('Query reports emptiness only for BOTH no text or filters', function () {
    'use strict';
    var success = false;
    
    // Both are empty, this should be false
    success |= !query.isEmpty();
    
    query.text = 'test';
    success |= query.isEmpty();

    query.text = '';
    query.filters = [{ 'name': 'name', 'value': 'value' }];
    success |= query.isEmpty();

    ok(success, 'Query is empty when no text or filters are set');
});

test('Query clones itself', function () {
    'use strict';
    var filters = [{ 'name': 'name', 'value': 'value' }, { 'name': 'name1', 'value': 'value1' }],
        clone;

    query = new Query('test', filters);

    clone = query.clone();

    notEqual(clone, query, 'Clone does not return the same query');
    notEqual(clone.filters, filters, 'Clone does not return the same filters');
    deepEqual(clone, query, 'Clone returns an equivalent copy of the query');
});

test('Query clone clones passed-in query', function () {
    'use strict';
    var filters = [{ 'name': 'name', 'value': 'value' }, { 'name': 'name1', 'value': 'value1' }],
        clone;

    query = new Query('test', filters);

    clone = Query.clone(query);

    notEqual(clone, query, 'Clone does not return the same query');
    notEqual(clone.filters, filters, 'Clone does not return the same filters');
    deepEqual(clone, query, 'Clone returns an equivalent copy of the query');
});