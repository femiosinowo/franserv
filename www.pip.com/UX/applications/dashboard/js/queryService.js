/*global Ektron,alert,console*/
define([
        'ektronjs',
        './config'
], function ($, config) {
    'use strict';

    function Query(query, success, error) {
        $.ajax({
            url: config.apiResources.queryUrl,
            dataType: 'json',
            data: { 'query': query },
            type: 'GET',
            success: success,
            error: function (jqXHR, status, msg) {
                error('Could not retrieve query results from the server with a status of "' + status + '" due to the error "' + msg + '"');
            }
        });
    }

    return {
        query: Query
    };
}
);