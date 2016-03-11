/*global console*/
define([
    'ektronjs',
    'Vendor/knockout/knockout'
],
    function ($, ko) {
        'use strict';

        // Replace ID-based "for" attribute with jQuery expression-based "for" binding
        function For() {
            var me = this;

            function guid() {
                /* 
                 * Stack overflow
                 * Question: http://stackoverflow.com/questions/105034
                 * Solution author: broofa (http://stackoverflow.com/users/109538/broofa)
                 */
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }

            me.assignFor = function (label, target) {
                // If target is found assign its ID to element's "for" attribute
                if ('undefined' !== typeof target) {

                    // If target has no ID, give it one
                    if ('undefined' === typeof $(target).attr('id')) {
                        $(target).attr('id', 'input' + guid());
                    }

                    // Set the for attribute
                    $(label).attr('for', $(target).attr('id'));
                }
            };

            me.init = function (element, valueAccessor) {
                var target,
                    selector = $.trim(valueAccessor());

                // prev(ious): constant to select immediately previous element
                if (selector === 'prev' || selector === 'previous') {
                    target = $(element).prev();

                    me.assignFor(element, target);
                }

                // next: constant to select immediately following element
                else if (selector === 'next') {
                    // Return execution to the browser to allow time for sibling 
                    // elements to be added to the DOM - this is volatile
                    setTimeout(function () {
                        target = $(element).next();

                        me.assignFor(element, target);
                    }, 2);
                }

                // selector: jQuery selector evaluated from the element's parent
                else if (selector[0] !== '$') {
                    // Return execution to the browser to allow time for sibling 
                    // elements to be added to the DOM - this is volatile
                    setTimeout(function () {
                        target = $(element).parent().find(selector);

                        me.assignFor(element, target);
                    }, 2);
                }

                // eval: JavaScript/jQuery statement to evaluate for the target element
                else if (selector[0] === '$') {
                    $(element).each(function () {
                        target = eval(selector);

                        me.assignFor(element, target);
                    });
                }
            };
        }

        ko.bindingHandlers.for = ko.bindingHandlers.for || new For();
    }
);