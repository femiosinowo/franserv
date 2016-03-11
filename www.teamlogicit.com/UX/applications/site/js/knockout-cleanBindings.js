define([
    'Vendor/knockout/knockout',
    'ektronjs'
],
    function (ko, $) {
        'use strict';
        if ('undefined' === typeof (ko.cleanBindings)) { 
            ko.cleanBindings = function (namespace) {
                $('[data-bind-' + namespace + ']').each(function () {
                    ko.cleanNode(this);
                });
            };
        }
    }
);