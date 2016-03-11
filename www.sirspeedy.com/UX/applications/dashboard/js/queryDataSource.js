/*global Ektron,alert,console*/
define([
        'ektronjs',
        './config'
], function ($, config) {
    'use strict';

    function GetAllQueries (success, error) {
        $.ajax({
            url: config.apiResources.topicsUrl,
            dataType: 'json',
            type: 'GET',
            success: success,
            error: function (jqXHR, status, msg) {
                error('Could not load topics from configured URL with a status of "' + status + '" due to the error "' + msg + '"');
            }
        });
    }

    return {
        all: GetAllQueries
    };
}
);