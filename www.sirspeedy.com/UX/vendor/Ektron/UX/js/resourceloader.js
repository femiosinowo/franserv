define(['require', 'ektronjs', 'UX/js/constants'], function (require, $, constants) {
    'use strict';

    function ResourceLoader(baseUrl) {
        this.Types = constants.ResourceType;
        this.defaultSettings = {
            callback: null,
            files: [],
            reusable: false,
            selector: '.ux-appWindow',
            type: this.Types.html
        };
        this._baseUrl = ('string' === typeof (baseUrl)) ? baseUrl + '/' : '';
    }
    
    ResourceLoader.prototype.load = function (settings) {
        var s = {},
            me = this,
            ids = [],
            index = 0,
            dataArray = [],
            idString = function (id, type) {
                var prefix = 'text',
                    suffix = s.type + '/',
                    rtnVal;
                switch (s.type) {                    
                case 'html':
                    prefix = 'text';
                    suffix = 'views/';
                    break;
                case 'image':
                    prefix = 'image';
                    suffix = 'img/';
                    break;
                default:
                    prefix = s.type;
                    break;
                }
                switch (s.type) {
                case 'image':
                    rtnVal = '_' + prefix + '!' + me._baseUrl + suffix + id;
                    break;
                default:
                    rtnVal = '_' + prefix + '!' + me._baseUrl + suffix + id + '.' + s.type;
                    break;
                }
                return rtnVal;
                
            };

        // ensure settings is a viable setttings object
        settings = $.extend({}, me.defaultSettings, settings);//('object' == typeof (settings)) ? settings : me.defaultSettings;

        s = $.extend(s, settings);

        // if no filename is provided, there's nothing to get
        if ('string' === typeof (s.files)  && s.files.length > 0) {
            $.trim(s.files);
            s.files = [s.files];
            ids.push(idString(s.files, s.type));
        }
        else if ($.isArray(s.files) && s.files.length > 0) {
            for (index = 0; index < s.files.length; index++) { //ignore jslint
                ids.push(idString(s.files[index], s.type));
            }
        }        
        else {
            // resources were not provided in a viable format
            return;
        }        
        switch (s.type) {
        case this.Types.image:
        case this.Types.css:
            require(ids, function (data) {
                if ($.isFunction(s.callback)) {
                    s.callback.call(this, data);
                }
            });
            break;
        default: // 'html' || 'text'
            require(ids, function (data) {
                    /*ignore jslint start*/
                    for (index = 0; index < arguments.length; index++) {
                        /*ignore jslint end*/
                        if (s.reusable) {
                            $('head').append('<script type="text/html" id="' + s.files[index] + '">' + arguments[index] + '</script>');
                        }
                        else if (typeof (s.selector) !== 'undefined' && 'string'=== typeof(s.selector) && 'body' === s.selector.toLowerCase()) {
                            $('body').prepend(arguments[index]);
                        }
                        else {
                            $(s.selector).append(arguments[index]);
                        }
                    }
                    if ($.isFunction(s.callback)) {
                        s.callback.apply(this, arguments);
                    }
                });
            break;
        }
    };

    return ResourceLoader;
});