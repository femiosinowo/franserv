(function (global) {
    'use strict';
    if ('undefined' === typeof (global.Ektron)) { global.Ektron = {}; }
    global.define = function (deps, callback) {
        if ('undefined' === typeof global.modules) {
            global.modules = [];
        }
        global.modules.push(callback);
        global.createModule = callback;
    };
})(this);