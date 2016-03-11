/*global console*/
define([
    'ektronjs',
    'Vendor/knockout/knockout'
],
    function ($, ko) {
        'use strict';

        function Label() {
            var me = this;

            //
            // Properties
            //
            me.namespaces = [];
            me.labels = {};

            //
            // Methods
            //
            me.init = function (element, valueAccessor) {
                // Read out the options object (or only the label name)
                var options = valueAccessor() || {},
                    lbl = options.label || options || '',
                    prefix = options.prefix || '',
                    namespace = options.namespace || null,
                    attr = options.attr || null,
                    labels;

                // Set the label context to a namespace or the global labels
                if (namespace) {
                    labels = me.namespaces[namespace] || {};
                } else {
                    labels = me.labels;
                }

                // Output the label as text or the supplied attribute
                if (attr == null) {
                    $(element).text(labels[prefix + lbl] || lbl);
                } else {
                    $(element).attr(attr, labels[prefix + lbl] || lbl);
                }
            };

            // Register a label object with an optional namespace
            me.register = function (labels, namespace) {
                if (namespace) {
                    me.namespaces[namespace] = labels;
                } else {
                    me.labels = labels;
                }
            }
        }

        ko.bindingHandlers.label = ko.bindingHandlers.label || new Label();
    }
);