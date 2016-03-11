/*global window,module,test,ok,alert*/
/// <reference path="setup.js" />
/// <reference path="../js/queryDataSource.js" />

var queryDataSource, $, appConfig, Query;

function $() {
    'use strict';
    return $;
}

module('Query Data Source / Content Topics', {
    setup: function () {
        'use strict';

        appConfig = {
            apiResources: {
                topicsUrl: ''
            }
        };

        Query = function (t) {
            this.text = t;
        };

        queryDataSource = window.createModule($, appConfig);
    }
});

test('query data source queries service', function () {
    'use strict';

    var foundCorrectUrl = false, configuredUrl = '/api/topics/', foundRequest = false,
        results = [{ 'title': 'Saved Query', 'query': new Query('hooray') }], foundResults;

    $.ajax = function (settings) {
        foundCorrectUrl |= settings.url === configuredUrl;
        foundRequest |= settings.dataType === 'json' && settings.type === 'GET';

        settings.success(results);
    };

    appConfig.apiResources.topicsUrl = configuredUrl;

    queryDataSource = window.createModule($, appConfig);

    queryDataSource.all(function (results) {
        foundResults = results;
    });

    ok(foundCorrectUrl, "Query data source queried the correct URL");
    ok(foundRequest, 'Query data source correctly submitted the query request');
    equal(foundResults, results, "Query data source returned correct results");
});

test('query data source reports errors', function () {
    'use strict';
    var errorReported = false;

    $.ajax = function(settings) {
        settings.error();
    };
    
    queryDataSource.all(
        function () {
        },
        function (err) {
            errorReported = true;
        });

    ok(errorReported, 'query data source reports errors');
});