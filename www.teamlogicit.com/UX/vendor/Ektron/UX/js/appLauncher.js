define(['ektronjs'], function ($) {
    var config = {
        launcherVisibleClass: 'ux-launcher-visible',
        launcherContextSelector: 'body'
    };

    return {
        on: function (eventName, unloadCallback) {
            $(window).on('beforeunload', unloadCallback);
        },
        visible: function (value) {
            if (value) {
                $(config.launcherContextSelector).addClass(config.launcherVisibleClass);
            } else {
                $(config.launcherContextSelector).removeClass(config.launcherVisibleClass);
            }
        }
    };
});