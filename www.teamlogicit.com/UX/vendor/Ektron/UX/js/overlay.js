define(['ektronjs'], function ($) {
    'use strict';
    var me = {
        clear: function ()
        {
            $('.ektron-ux .appOverlay').empty();
        },
        show: function () {
            $('.ektron-ux').addClass('showOverlay');
        },
        hide: function () {
            $('.ektron-ux').removeClass('showOverlay');
        },
        getElement: function () {
            return $('.ektron-ux .appOverlay')[0];
        },
        getLauncherList: function () {
            return $('.ektron-ux .launcher>ul')[0];
        }
    };

    return me;
});