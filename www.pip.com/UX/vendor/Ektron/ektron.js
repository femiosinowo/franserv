(function () {
    'use strict';
    var $ektron, Ektron;
    if ('undefined' === typeof (window.$ektron)) {
        $ektron = window.$ektron = window.jQuery;
        window.jQuery.noConflict(true);
        if ('undefined' === typeof (window.$)) {
            window.$ = window.$ektron;
        }
        if ('undefined' === typeof (window.jQuery)) {
            window.jQuery = window.$ektron;
        }
    }
    if ('undefined' === typeof (window.Ektron)) {
        if ('undefined' === typeof Ektron) {
            Ektron = window.Ektron = {};
        }
    }
})();