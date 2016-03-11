/*global window,module,test,ok,alert*/
/// <reference path="setup.js" />
/// <reference path="../js/ko.label.js" />
var $, ko, labels;

module('Knockout "label" binding', {
    setup: function () {
        'use strict';

        $ = function () {
            return {
                text: function () { return $(); }
            };
        };

        ko = {
            bindingHandlers: {}
        };

        labels = {};
    }
});

// Filter tree
test('KO "label" binding provides text translation', function () {
    'use strict';
    var
        label = 'labelName',
        $extend,
        element = {},
        foundLabelValue;

    // Extend jQuery mock to have text function
    $extend = function (args) {
        var $ref = $();

        if (args === element) {
            $ref.text = function (text) {
                foundLabelValue = text;
            };
        }

        return $ref;
    };

    window.createModule($extend, ko);

    // Register our labels
    ko.bindingHandlers.label.register(labels);

    // Perform tested action
    ko.bindingHandlers.label.init(element, function () { return label; });
    equal(foundLabelValue, label, 'label binding sets element text for unknown strings');

    // Extend labels with test key
    labels.labelName = 'labelValue';

    // Perform tested action
    ko.bindingHandlers.label.init(element, function () { return label; });
    equal(foundLabelValue, labels.labelName, 'label binding sets element text for known strings');
});