/* global window, define */
define(['Vendor/knockout/knockout',
        'ektronjs',
        '_i18n!UX/js/nls/labels',
        'Vendor/jQuery/UI/js/jquery-ui-complete.min',
        '_css!Vendor/jQuery/UI/css/ektron-ux/jquery-ui-complete.min',
        'Vendor/jQuery/plugins/niceScroll/jquery.nicescroll',
        'Vendor/jQuery/plugins/blockUI/jquery.blockui'
],
    function (ko, $, labels) {
        'use strict';
        // ektronUXNiceScroll
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXNiceScroll)) {
            ko.bindingHandlers.ektronUXNiceScroll = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    if ('function' !== typeof valueAccessor) {
                        valueAccessor = function () {
                            return {};
                        };
                    }

                    var options = ko.utils.unwrapObservable(valueAccessor()),
                        settings = {
                            autohidemode: false,
                            cursorcolor: 'rgba(0, 0, 0, .3)',
                            cursorborder: 0,
                            useparentcontainer: false,
                            background: 'transparent',
                            preservenativescrolling: false,
                            pixelsToScroll: 0,
                            invoke: false
                        };
                    $.extend(settings, options);
                    $(element).niceScroll(settings);
                },
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var $element = $(element),
                        options = ko.utils.unwrapObservable(valueAccessor()),
                        scrollPixels = options.pixelsToScroll,
                        parent;

                    if (options.invoke) {
                        $element.getNiceScroll().resize();
                        parent = $element.parent();

                        //reset scroll to top if someone has scrolled this element previously.
                        if (!isNaN(scrollPixels) && (parent.height() > 0)) {
                            parent.scrollTop(scrollPixels);
                        }
                    }
                }
            };
        }

        // ektronUXAccordion
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXAccordion)) {
            ko.bindingHandlers.ektronUXAccordion = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).accordion('destroy');
                    });
                },
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var options = ko.utils.unwrapObservable(valueAccessor());

                    $(element).accordion(options);
                }
            };
        }

        // ektronUXAutocomplete
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXAutocomplete)) {
            ko.bindingHandlers.ektronUXAutocomplete = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).autocomplete('destroy');
                    });
                },

                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    ko.bindingHandlers.ektronUXAutocomplete._invokeAutoComplete(element, valueAccessor);
                },

                _invokeAutoComplete: function (element, valueAccessor) {
                    var AutoCompleteOptions = ko.utils.unwrapObservable(valueAccessor()),
                        isInput = $(element).is('input'),
                        settings = {};

                    if (AutoCompleteOptions.useParent === true) {
                        settings.appendTo = $(element).parent();
                    }
                    if (ko.isObservable(AutoCompleteOptions.source)) {
                        AutoCompleteOptions.source = ko.toJS(AutoCompleteOptions.source);
                    }
                    $.extend(settings, AutoCompleteOptions);
                    if (AutoCompleteOptions.invoke && isInput) {
                        $(element).autocomplete(settings);
                    }
                }
            };
        }

        // ektronUXDialog
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXDialog)) {
            ko.bindingHandlers.ektronUXDialog = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).dialog('destroy');
                    });
                },
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var options = ko.utils.unwrapObservable(valueAccessor()),
                        defaultOptions = {
                            autoOpen: false,
                            appendTo: '.ektron-ux .ux-appWindow .ux-app',
                            closeText: '<span data-ux-icon="&#xe01a;" title="' + labels.close + '" aria-hidden="true" />'
                        };

                    options = $.extend(defaultOptions, options);

                    $(element).dialog(options);
                }
            }
        }

        // ektronUXLabels
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXLabel)) {
            ko.bindingHandlers.ektronUXLabel = {
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var valueUnwrapped = ko.utils.unwrapObservable(valueAccessor()),
                        translatedString = labels['i18nError'],
                        newValueAccessor = function () {
                            return translatedString;
                        },
                        checkLabelsForKey = function (obj) {
                            var result = false;
                            if (obj.labels) {
                                i18nUnwrapped = ko.utils.unwrapObservable(obj.labels);
                                if (i18nUnwrapped[valueUnwrapped]) {
                                    result = true;
                                }
                            }
                            return result;
                        },
                        i = 0,
                        ancestor,
                        i18nUnwrapped;

                    if (checkLabelsForKey(viewModel)) {
                        // desired i18n key was found, use it
                        translatedString = i18nUnwrapped[valueUnwrapped];
                    }
                    else {
                        // walk the bindingContext $parents tree to see if we can find it among the ancestors
                        for (i; i < bindingContext.$parents.length; i = i + 1) {
                            ancestor = bindingContext.$parents[i];
                            if (checkLabelsForKey(ancestor)) {
                                translatedString = i18nUnwrapped[valueUnwrapped];
                                break;
                            }
                        }
                    }

                    ko.bindingHandlers.text.update(element, newValueAccessor, allBindingsAccessor, viewModel, bindingContext);
                }
            };
        }

        // ektronUXSelectMenu
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXSelectMenu)) {
            ko.bindingHandlers.ektronUXSelectMenu = {
                init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    //handle disposal (if KO removes by the template binding)
                    ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                        $(element).selectmenu('destroy');
                    });
                },
                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var options = ko.utils.unwrapObservable(valueAccessor()),
                        defaultOptions = {
                            popup: false,
                            appendTo: '.ektron-ux .ux-appWindow .ux-app'
                        };

                    options = $.extend(defaultOptions, options);

                    $(element).selectmenu(options);
                }
            };
        }

        // ektronUXSubstring
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXSubstring)) {
            ko.bindingHandlers.ektronUXSubstring = {
                _completeWordSubstring: function (text, length) {
                    var regex = new RegExp('.{0,' + length.toString() + '}(?=\\s|$)');

                    return text.match(regex)[0];
                },

                update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
                    var options = ko.utils.unwrapObservable(valueAccessor()),
                        length = 0,
                        textFromStart,
                        text;

                    // Ensure text is fully unwrapped
                    options.text = ko.utils.unwrapObservable(options.text);

                    if ("string" === typeof options.text) {
                        // All available options
                        options = $.extend({
                            // Standard options
                            cutoffIndicator: '…',
                            length: undefined,
                            text: '',
                            // Advanced options
                            allowPartialWords: false,
                            omitCutoffIndicator: false,
                            start: 0,
                            end: undefined
                        }, options);

                        // Get the right-hand side of the substring
                        textFromStart = options.text.substring(options.start);

                        // Calculate length to get left-hand side of the substring
                        length = options.length || (options.end || options.text.length - options.start);

                        // Take the substring
                        text = !options.allowPartialWords
                            ? ko.bindingHandlers.ektronUXSubstring._completeWordSubstring(textFromStart, length)
                            : textFromStart.substring(0, length);

                        // If we are showing a cutoff indicator, was the text cut off?
                        if (!options.omitCutoffIndicator && text !== textFromStart) {
                            // Make room for the cutoff indicator
                            text = !options.allowPartialWords
                                ? ko.bindingHandlers.ektronUXSubstring._completeWordSubstring(text, length - options.cutoffIndicator.length)
                                : text.substring(0, length - options.cutoffIndicator.length);

                            // Append the cuttoff indicator
                            text += options.cutoffIndicator;
                        }

                        ko.bindingHandlers.text.update(
                            element,
                            function () {
                                return text;
                            },
                            allBindingsAccessor,
                            viewModel,
                            bindingContext);
                    }
                }
            }
        }

        // ektronUXBlocker
        if ('undefined' === typeof (ko.bindingHandlers.ektronUXBlocker)) {
            ko.bindingHandlers.ektronUXBlocker = {
                update: function (element, valueAccessor) {
                    var options = ko.utils.unwrapObservable(valueAccessor()),
                        pluginOptions,
                        onUnblock;

                    // Register callback to update isBlocked on unblock
                    onUnblock = options.onUnblock;
                    options.onUnblock = function (element, opts) {
                        if (ko.isObservable(options.isBlocked)) {
                            options.isBlocked(false);
                        }
                        else {
                            options.isBlocked = false;
                        }

                        if (typeof onUnblock === "function") {
                            onUnblock(element, opts);
                        }
                    }

                    // Apply defaults to unwrapped option values for the plugin
                    pluginOptions = $.extend({
                        blockMsgClass: 'ux-block-container',
                        css: {
                            backgroundColor: 'none',
                            border: 'none',
                            color: '#fff'
                        },
                        isBlocked: true,
                        message: '<span class="ux-busy-indicator" />',
                        useTheme: true
                    },
                    ko.toJS(options));

                    // Perform (un)blocking
                    if (pluginOptions.isBlocked) {
                        $(element).block(pluginOptions);
                    }
                    else {
                        $(element).unblock();
                    }
                }
            }
        }
    }
);