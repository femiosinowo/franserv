define([
    'ektronjs',
    'Vendor/knockout/knockout',
    '_i18n!./nls/labels'
], function ($, ko, labels) {
    'use strict';

    function ContentHeader(settings) {
        this.title = ko.observable('');
        this.data = ko.observable('');
    }
    
    return ContentHeader;
});