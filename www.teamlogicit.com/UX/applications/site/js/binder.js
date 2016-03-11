define(['Vendor/knockout/knockout', 'ektronjs'],
    function (ko, $) {
        var delay = 40, tryCount = 5;

        function bind(viewModel, selector, remainingTryCount, callback) {
            var $element = $(selector);

            if ($element.length > 0) {
                ko.applyBindings(viewModel, $element[0]);
                if ($.isFunction(callback)) {
                    callback();
                }
            } else if (remainingTryCount > 0) {
                setTimeout(function () {
                    bind(viewModel, selector, remainingTryCount - 1, callback);
                }, delay);
            }
        }

        return {
            applyBindings: function (viewModel, selector, callback) {
                bind(viewModel, selector, tryCount, callback);
            }
        };
    });