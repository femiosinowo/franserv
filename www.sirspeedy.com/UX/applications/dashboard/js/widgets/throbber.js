/*global console*/
define([
        'ektronjs',
        'Vendor/knockout/knockout',
        '_text!../../views/widgets/throbber.html',
        '_css!../../css/widgets/throbber'
    ],
    function ($, ko, template) {
        'use strict';

        function VisibleFadeOut() {
            var me = this;

            me.init = function (element, valueAccessor) {
                if (ko.utils.unwrapObservable(valueAccessor())) {
                    $(element).show();
                } else {
                    $(element).hide();
                }
            };

            me.update = function (element, valueAccessor) {
                if (ko.utils.unwrapObservable(valueAccessor())) {
                    $(element).show();
                } else {
                    $(element).fadeOut('slow');
                }
            };
        }
    
        ko.bindingHandlers.visibleFadeOut = ko.bindingHandlers.visibleFadeOut || new VisibleFadeOut();

        function ThrobberModel(element, options) {
            var me = this;

            me.options = ko.observable(options);
        }

        function Throbber() {
            var me = this;

            me.init = function (element, valueAccessor, allBindingsAccessor, model, bindingContext) {
                var throbberModel = new ThrobberModel(element, valueAccessor()), throbberElement;

                throbberElement = $(template);

                $(element).after(throbberElement);
                ko.applyBindings(throbberModel, throbberElement[0]);

                throbberElement.find('.contents').append(element);
            };
        }

        ko.bindingHandlers.throbber = ko.bindingHandlers.throbber || new Throbber();

        return Throbber;
    }
);