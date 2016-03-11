/*global window,module,test,ok,alert*/
/// <reference path="../setup.js" />
/// <reference path='../../../../vendor/jQuery/jquery.min.js' />
if ('undefined' === typeof jQuery) {
    var jQueryReference = document.createElement('script');
    // Path relative to host page in parent directory
    jQueryReference.setAttribute('src', '../../../vendor/jQuery/jquery.min.js');
    document.head.appendChild(jQueryReference);
}   
/// <reference path="../../js/widgets/filterTreeView.js" />
var html, $doc, ko, labels, template, FilterTreeView, view;

function loadTestHtml(html) {
    var $ref;

    $('iframe#testFrame').remove();
    $ref = $('<iframe id="testFrame" style="display: none;" />').appendTo('body').contents();
    $doc = $ref.extend(function (selector) { return $ref.find(selector); }, $ref);

    // For IE (head missing immediately on iframe add)
    if ($doc('head').length === 0) {
        $doc[0].write('<head />');
    }

    // For IE (body missing immediately on iframe add)
    if ($doc('body').length === 0) {
        $doc[0].write('<body />');
    }

    if (html) {
        $doc('body').append(html);
    }

    FilterTreeView = window.createModule($doc, ko, labels, template);
    view = new FilterTreeView();
}

module('Filter Tree', {
    setup: function () {
        'use strict';

        ko = {
            bindingHandlers: {
                'label': { register: function () { } }
            },
            virtualElements: {
                allowedBindings: {}
            }
        };
        labels = {};
        template = '';

        loadTestHtml();
    }
});

test('Module loads template', function () {
    'use strict';
    template = '';

    window.createModule($doc, ko, labels, template);

    ok($doc('#filterTree').length === 1, 'Module loads template into document head');
});

test('click binds handler to click event of element', function () {
    'use strict';
    var element,
        callback,
        called = false;

    loadTestHtml('<span id="psuedoButton" />');

    callback = function () {
        called |= true;
    }

    element = $doc('#psuedoButton');

    view.click(element, callback);

    element.click();

    ok(called, 'click binds handler to click event of element');
});

test('getFilters returns sibling filterHeaders from handle within filter tree', function () {
    'use strict';
    var handle, filters;

    loadTestHtml('<h3 id="handle" class="filterHeader">One</h3><h3 class="filterHeader">Two</h3><h3 class="filterHeader">Three</h3><h3 class="filterHeader">Four</h3>');

    handle = $doc('#handle');

    filters = view.getFilters(handle);

    equal(filters.length, 4, 'getFilters returns the number of filters expected from known HTML');
});

test('getFilterIndex returns the index of a filter among its siblings', function () {
    'use strict';
    var handle, index;

    loadTestHtml('<h3 class="filterHeader">One</h3><h3 class="filterHeader">Two</h3><h3 id="third" class="filterHeader">Three</h3><h3 class="filterHeader">Four</h3>');

    handle = $doc('#third');

    index = view.getFilterIndex(handle);

    equal(index, 2, 'getFilterIndex returns the index of a filter among its siblings');
});

test('isExpanded returns filter expansion state from HTML', function () {
    'use strict';
    var filter;

    loadTestHtml('<h3 class="expanded filterHeader">One</h3>');

    filter = $doc('.filterHeader');

    ok(view.isExpanded(filter), 'isExpanded reports expected filter expansion state from known HTML');
});

test('toggleHeaderState swaps active and inactive styles', function () {
    'use strict';
    var header;

    loadTestHtml('<h3 class="filterHeader ui-accordion-header ui-helper-reset ui-state-default ui-accordion-icons ui-corner-all expanded ui-accordion-header-active ui-state-active"><span class="ui-accordion-header-icon ui-icon ui-icon-triangle-1-e"></span></h3>');

    header = $doc('.filterHeader');

    view.toggleHeaderState(header);

    ok(!header.hasClass('expanded'), 'toggleHeaderState swaps out expanded state');
    ok(!header.hasClass('ui-accordion-header-active'), 'toggleHeaderState swaps out jQuery UI accordion active state');
    ok(!header.hasClass('ui-state-active'), 'toggleHeaderState swaps out jQuery UI widget active state');

    ok(!header.children('.ui-accordion-header-icon').hasClass('ui-icon-triangle-1-e'), 'toggleHeaderState swaps out inactive arrow');
    ok(header.children('.ui-accordion-header-icon').hasClass('ui-icon-triangle-1-s'), 'toggleHeaderState swaps in active arrow');
});

test('toggleHeaderState swaps active and inactive styles', function () {
    'use strict';
    var header;

    loadTestHtml('<h3 class="filterHeader"></h3><div class="filterPanel"></div>');

    header = $doc('.filterHeader');

    view.togglePanelState(header);

    ok(!$doc('.filterPanel').is(':visible'), 'togglePanelState hides panel when visible');
});