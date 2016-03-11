/*global window,module,test,ok,alert*/
/// <reference path="setup.js" />
/// <reference path="../js/queryService.js" />

var queryService, $, appConfig, Query;

function $() {
    'use strict';
    return $;
}

module('Query Service', {
    setup: function () {
        'use strict';

        appConfig = {
            apiResources: {
                queryUrl: ''
            }
        };

        Query = function (t) {
            this.text = t;
        };

        queryService = window.createModule($, appConfig, Query);
    }
});

test('query service queries', function () {
    'use strict';
    var foundCorrectUrl = false, query = new Query('hello bruce'), foundQuery, foundResults,
        results = [{ title: 'PINKESH', summary: 'Is in da house' }],
        configuredUrl = '/api/query/', foundRequest = false;

    $.ajax = function (settings) {
        foundCorrectUrl |= settings.url === configuredUrl;
        foundQuery = settings.data.query;
        foundRequest |= settings.dataType === 'json' && settings.type === 'GET';
        settings.success(results);
    };

    appConfig.apiResources.queryUrl = configuredUrl;

    queryService = window.createModule($, appConfig, Query);

    queryService.query(query, function (results) {
        foundResults = results;
    });

    ok(foundCorrectUrl, "Query service queried the correct URL");
    deepEqual(foundQuery, query, "Query service submitted the correct query");
    ok(foundRequest, 'Query service correctly submitted the query request');
    equal(foundResults, results, "Query service returned correct results");
});

test('query service source reports errors', function () {
    'use strict';
    var errorReported = false;

    $.ajax = function (settings) {
        settings.error();
    };

    queryService.query(
        new Query(),
        function () {
        },
        function (err) {
            errorReported = true;
        });

    ok(errorReported, 'query service reports errors');
});