(function (global) {
    'use strict';

    global.define = function (deps, callback) {
        global.createModule = callback;
    };
})(this);