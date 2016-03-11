define([
    'ektronjs',
    'UX/js/toolbar.popover',
    './config',
    '_i18n!./nls/labels',
    'Vendor/jQuery/plugins/niceScroll/jquery.nicescroll',
    './binder',
    'Vendor/knockout/knockout',
    'Vendor/jQuery/plugins/jcarousel/jquery.jcarousel.min',
    '_css!Vendor/jQuery/Plugins/jCarousel/css/ektron.ux.jcarousel'
], function ($, ToolbarPopOver, config, labels, niceScroll, binder, ko) {
    'use strict';
    var mobileDevicePopover,
        mobilePreviewCookieName = 'ektMobilePreview';
    function MobilePreview(settings) {
        var me = this;
        this.toolbar = settings.toolbar;
        this.resourceLoader = settings.resourceLoader;
        this.appWindow = settings.appWindow;
        this.previewTemplate = settings.previewTemplate;
        this.carouselTemplate = settings.carouselTemplate;
        //Mobile device vars
        this.dvcUserAgent = '';
        this.dvcResWidth = 0;
        this.dvcResHeight = 0;
        this.dvcWidth = 0;
        this.dvcHeight = 0;
        me.document = settings.document || window.document;
        me.destroyMobilePreviewCookie = function () {
            me.document.cookie = mobilePreviewCookieName + '=;Max-Age=0';
        };

        me.setCookie = function(name, value)  {

            me.document.cookie = name + '=' + value + '; path=/';
        }
        me.setCookieDeviceInfo = function() {
            var cookie,
                stripped;
            stripped = [
                'dvcResWidth=' + me.dvcResWidth().replace('px', ''),
                'dvcResHeight=' + me.dvcResHeight().replace('px', ''),
                'dvcWidth=' + me.dvcWidth().replace('px', ''),
                'dvcHeight=' + me.dvcHeight().replace('px', ''),
                // User agent specifies model and OS to server
                'dvcUserAgent=' + encodeURIComponent(me.dvcUserAgent())
            ];
            cookie = stripped.join('&');
            me.setCookie(mobilePreviewCookieName, cookie);
    }
    }

    MobilePreview.prototype.init = function () {
        var me = this;
        me.appWindow.clear();
        $(me.appWindow.getElement()).append(me.previewTemplate);
        createToolbarUI(me.toolbar, labels);
        createPopOverCarousel(me.carouselTemplate, me);
    };

    // Cleanly return to site app
    MobilePreview.prototype.close = function () {
        var me = this;

        me.appWindow.hide();
        me.destroyMobilePreviewCookie();
    };

    // Exit mobile preview to the last page previewed
    MobilePreview.prototype.finish = function () {
        var me = this, iframe, iframeLocation, doc, currentLocation;

        // Close the mobile preview first
        me.close();

        // redirect host page to location of mobile preview iframe when it closes:
        iframe = document.getElementById('mobilePreviewIframe');
        doc = iframe.documentWindow || iframe.contentDocument;
        currentLocation = window.location;
        currentLocation = currentLocation.hostname + currentLocation.port + currentLocation.pathname;
        iframeLocation = doc.location;
        iframeLocation = iframeLocation.hostname + iframeLocation.port + iframeLocation.pathname;

        if (currentLocation !== iframeLocation) {
            document.location.href = doc.location.href;
        }
    };

    MobilePreview.prototype._loadDevice = function (device) {
        var me = this,
            viewModel,
            f,
            element;
        me.appWindow.show();
        element = $('.ux-app-site .mobilePreview')[0];

        viewModel = {
            appwindow: me.appWindow,
            device: device,
            mobilePreviewClose: function () { me.finish(); },
            positionMobileDevice: function (data, event) {
                me._positionDevice(data, event);
            },

            resizeMobilePreviewIframeContents: function (data, event) {
                var iframe = $(event.target),
                    device = data.device,
                    screenHeight = parseInt(device.ScreenHeight().slice(0, -2), 10),
                    screenWidth = parseInt(device.ScreenWidth().slice(0, -2), 10),
                    readyCallback,
                    iframeDocument;

                readyCallback = function () {
                    if (device.orientation() === 'portrait') {
                        // Constrain the width first to allow for liquid layouts
                        iframe.width(screenWidth);

                        // Expand the iframe/viewport to contents if necessary, height first for liquid layouts
                        iframe.height(Math.max(screenHeight, iframe.contents().outerHeight()));
                        iframe.width(Math.max(screenWidth, iframe.contents().outerWidth()));
                    } else {
                        // Constrain the width first to allow for liquid layouts
                        iframe.width(screenHeight);

                        // Expand the iframe/viewport to contents if necessary, height first for liquid layouts
                        iframe.height(Math.max(screenWidth, iframe.contents().outerHeight()));
                        iframe.width(Math.max(screenHeight, iframe.contents().outerWidth()));
                    }

                    iframe.parent().getNiceScroll().resize();
                    iframe.contents().find('body').css({overflow: 'hidden'});
                    iframe.removeClass('loading');
                };

                // Look for the iframe document
                iframeDocument = iframe.find('document');

                // If the document is accessible, attach to its ready event
                if (iframeDocument.length > 0) {
                    iframeDocument.ready(readyCallback);
                } else {
                    // Otherwise use a delay
                    setTimeout(readyCallback, 50);
                }

                return true;
            }
        };

        ko.cleanNode(element);
        ko.applyBindings(viewModel, element);

        // custom devices don't have a user agent (and it may not be in the wurlf file even if 
        // we added one), so flag this as an custom device; the server will use the values we supply:
        me.dvcUserAgent = function() {
            if ((device.UserAgent() == null || device.UserAgent().length == 0)
                && device.BrandName() == 'Ektron') {
                return 'EktronDevice';
            }
            
            return device.UserAgent();
        };
        
        me.dvcResWidth = device.ScreenWidth;
        me.dvcResHeight = device.ScreenHeight;
        me.dvcWidth = device.DeviceWidth;
        me.dvcHeight = device.DeviceHeight;

        f = $(document.getElementById('mobilePreviewIframe'));
        f.addClass('loading');
        me.setCookieDeviceInfo();
        f[0].contentWindow.location.href = window.location.href;
        f.load(function () {
            var ifr = document.getElementById('mobilePreviewIframe'),
                images,
                dateTimeString;
            $(ifr).contents().find('a').click(function (event) {
                me.setCookieDeviceInfo();
            });

            dateTimeString = encodeURIComponent((new Date()).toString());
            images = ifr.contentDocument.getElementsByTagName('img');
            for (var i = 0; i < images.length; i++) { //ignore jslint
                if (images[i].parentNode.tagName !== 'FIGURE') {
                    images[i].setAttribute('src', images[i].getAttribute('src') + '?' + dateTimeString);
                }
            }
        });
        $(document.getElementById('mobilePreviewIframeWrapper')).niceScroll('#mobilePreviewIframe', {
            autohidemode: false,
            cursorcolor: 'rgba(0, 0, 0, .3)'
        });
        mobileDevicePopover.toggleDropbox();
        return true;
    };

    MobilePreview.prototype._positionDevice = function (data, event) {
        var device, iframe, iframeContents, newOrientation, element, landscapeShift, preview, me = this;

        // determine orientation based on user click of either the label or its child span tag
        if (event.target.tagName.toLowerCase() === 'label') {
            newOrientation = event.target.htmlFor;
        }
        else {
            newOrientation = event.target.parentNode.htmlFor;
        }

        device = data.device;
        iframe = device.iframe;

        if (device.orientation() !== newOrientation) {
            preview = $(document.getElementById('mobilePreviewIframe')).addClass('loading');
            iframeContents = preview[0].documentWindow || preview[0].contentDocument;
            // change orientation
            device.orientation(newOrientation);

            // recalculation position
            switch (newOrientation) {
                case 'landscape':
                    iframe.width(data.device.ScreenHeight());
                    iframe.height(data.device.ScreenWidth());

                    landscapeShift = (iframe.height().replace('px', '') - iframe.width().replace('px', '')) * 0.5;

                    // Truncate ('floor' for positive, 'ceiling' for negative)
                    landscapeShift = landscapeShift - landscapeShift % 1;

                    iframe.top((Math.floor(iframe.top().replace('px', '')) - landscapeShift) + 'px');
                    iframe.left((landscapeShift + Math.floor(iframe.left().replace('px', ''))) + 'px');
                    break;
                default:
                    iframe.width(data.device.ScreenWidth());
                    iframe.height(data.device.ScreenHeight());
                    iframe.top(data.device.OffsetHeight());
                    iframe.left(data.device.OffsetWidth());
                    break;
            }

            me.dvcResWidth = device.ScreenHeight;
            me.dvcResHeight = device.ScreenWidth;
            me.dvcWidth = device.DeviceHeight;
            me.dvcHeight = device.DeviceWidth;

            // reload the iframe in case the change in display mode requires new dynamic content to be loaded based on the new view area's size
            if (!preview.data('mobilePreviewIframeBindingSet')) {
                preview.on('load.uxSiteApp', function () {
                    preview.contents().find('a').click(function (event) {
                        me.setCookieDeviceInfo();
                    });

                    var ifr = preview[0],
                        images = ifr.contentDocument.getElementsByTagName('img');
                    /*ignore jslint start*/
                    for (var i = 0; i < images.length; i++) {
                        /*ignore jslint end*/
                        if (images[i].parentNode.tagName !== 'FIGURE') {
                            images[i].setAttribute('src', images[i].getAttribute('src') + '?test');
                        }
                    }
                });
                preview.data('mobilePreviewIframeBindingSet', true);
            }
            iframeContents.location.href = iframeContents.location.href;
        }
        return true;
    };

    function createToolbarUI(toolbar, labels) {
        mobileDevicePopover = new ToolbarPopOver({
            click: function () {
                this.toggleDropbox();
                return true;
            },
            id: 'mobileDeviceOptions',
            text: '',
            title: labels.switchMobileDevice,
            icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAABYCAYAAACKwsXrAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAXxJREFUeNrs2bFKw1AUgOGmOMXFUfARHIKLb+AaIUs20bEPkEkI6CNk6eBiNoUQ0NWxICh5AmehnUKQ6BpP5Lrf5ERj7X/g9Ga4h/M1TYeT6zRNM1nHcIADBw4c+GbDgyA4l2UmuWdZspKc53l+ORpc0LEsFz3LzwR/3bf3lvIXm5n1WBD3ll82lOXG1PaGT5Xw3fbDFm323prLQ03j6WRNAzhw4MCBAwcOHDjw4eArMxz4Haam0Fw+axprJ6C5Gd3uBNSndrxhOUmSRVEUB3Vdb9vsd133w/O8xyiKjsZ+PbEveSK5Y7n/TfJK8mVs+CgBHDhw4MCBAwcOHDhw4MCB/1QM8SarPct/lWwscykZq7u2d1yRcdM/TjW9tfClQfgdakJT86TprX3Gv4udX6ob7M8JHDhw4MCBAwcOHPgfgA9yli/hd6gZ5Cx/M0e3NtI0XZRlWdtqq6p6z7LsQXnDdM+443w9or3O8qUvZ/nAgQMHDhw48H8L/xRgAFK8b29OlKb5AAAAAElFTkSuQmCC'
        });
        toolbar.addPopOver(mobileDevicePopover);
    }

    function createPopOverCarousel(carouselTemplate, mobilePreview) {
        var popoverName = 'mobileDeviceOptions',
            popoverPanel = $('.dropbox_' + mobileDevicePopover.id);
        popoverPanel.html(carouselTemplate);
        $.ajax({
            dataType: 'json',
            type: 'GET',
            async: true,
            url: getUrl(),
            statusCode: {
                200: function (data) {
                    ko.utils.arrayForEach(data, function (entry) {
                        entry.id = popoverName + Math.random().toString(36).substr(2, 5);
                        entry.value = ko.computed(function () {
                            return entry.BrandName + ' ' + entry.Model;
                        });
                        entry.orientation = ko.observable('portrait');
                        entry.iframe = new IframeClass(entry.ScreenWidth, entry.ScreenHeight, entry.OffsetWidth, entry.OffsetHeight);
                    });
                    var viewModel = {
                        entries: ko.mapping.fromJS(data),
                        loadDevice: function (data, event) {
                            mobilePreview._loadDevice(data);
                        }
                    },
                        element = popoverPanel.children()[0];

                    binder.applyBindings(viewModel, '.dropbox_' + mobileDevicePopover.id + ' > *', function () {
                        $('.jcarousel').jcarousel();
                    });

                    if (data.length === 1) {
                        $('.jcarousel').css({ 'left': 0 });
                    }

                    mobileDevicePopover.isVisible(data.length > 0);
                },
                404: function () {
                    if ('undefined' !== typeof (console) && 'function' === typeof (console.log)) {
                        console.log('(404) ' + getUrl());
                    }
                },
                403: function () {
                    // do nothing
                }
            }
        });
    }

    // define iframe class for later use in entry mappings
    function IframeClass(width, height, offsetX, offsetY) {
        this.width = ko.observable(width);
        this.height = ko.observable(height);
        this.top = ko.observable(offsetY);
        this.left = ko.observable(offsetX);
    }

    function getCookie(c_name) {
        var i, x, y, ARRcookies = document.cookie.split(';');
        /*ignore jslint start*/
        for (i = 0; i < ARRcookies.length; i++) {
            /*ignore jslint end*/
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf('='));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf('=') + 1);
            x = x.replace(/^\s+|\s+$/g, '');
            if (x === c_name) {
                return unescape(y);
            }
        }
    }

    function validObject(obj) {
        return ('undefined' !== typeof (obj) && null !== obj);
    }



    function getUrl() {
        return '//' + config.serverSettings.domain + '/' + config.serverSettings.apiResources.previewDevicesApi.resourceName;
    }

    return MobilePreview;
});