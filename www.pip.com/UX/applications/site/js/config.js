define([], function () {
    'use strict';

    function ApiResource(name, verbs) {
        this.availableVerbs = verbs;
        this.resourceName = name;
    }

    var config =
    {
        'serverSettings': {
            'domain': window.location.host,
            'apiResources': {
                'previewDevicesApi': new ApiResource(window.Ektron.PathData.sitePath + 'api/ux/site/devicepreviews/', [
                    'GET'
                ])
            }
        }
    };

    return config;
});