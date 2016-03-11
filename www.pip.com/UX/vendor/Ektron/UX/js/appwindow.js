define(['ektronjs', 'UX/js/overlay'], function ($, overlay) {
    'use strict';
    var me = {
        clear: function ()
        {
            $(me.getElement()).empty();
        },
        show: function () {
            $('html').addClass('ektron-ux-bodyNoScroll');
        },
        hide: function () {
            $('html').removeClass('ektron-ux-bodyNoScroll');
        },
        getElement: function () {
            return $('.ektron-ux .ux-appWindow')[0];
        }
    };

    return me;
});