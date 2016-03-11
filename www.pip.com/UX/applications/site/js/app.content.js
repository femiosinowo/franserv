define([
    'ektronjs',
	'Vendor/knockout/knockout'
], function ($, ko) {
    'use strict';

    function ContentViewModel() {
        var me = this;

        me.hasContentItems = ko.observable();

        me.contentDialog = null;
        me.contentDialogAccordion = null;
        me.contentDialogAccordionLinkList = ko.observable({
            Items: []
        });
    }

    return ContentViewModel;
});