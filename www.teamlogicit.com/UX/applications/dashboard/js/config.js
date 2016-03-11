define([], function () {
    'use strict';

    var config = {
        'apiResources': {
            'queryUrl': window.Ektron.PathData.sitePath + 'api/ux/dashboard/query/',
            //'topicsUrl': '../../../Mocks/topicService.js'
            'topicsUrl': window.Ektron.PathData.sitePath + 'api/ux/dashboard/topics/'
        }
    };

    return config;
});