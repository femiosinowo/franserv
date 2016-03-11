define(['ektronjs'], function ($) {
    'use strict';

    //message types
    function MessageTypes() {
        this.error = 'error';
        this.info = 'info';
        this.success = 'success';

        //todo
        //this.warning = 'warning';
        //this.help = 'help';
    }



    function Logger(console) {
        this.Types = new MessageTypes();
        this.defaultSettings = {
            type: this.Types.info,
            appendMessageTo: 'body'
        };
        this.console = console;


    }

    Logger.prototype.getMessageTypeCssClass = function (type) {
        var me = this, rtnVal;
        /*ignore jslint start*/
        switch (type) {
            case me.Types.error:
                rtnVal = '.ektronuxerror';
                break;
            case me.Types.success:
                rtnVal = '.ektronuxsuccess';
                break;
            default:
                rtnVal = '.ektronuxinfo';
                break;
        }
        /*ignore jslint end*/
        return rtnVal;
    };

    Logger.prototype.log = function (itemToLog) {
        if ('undefined' !== typeof(this.console) && 'undefined' !== typeof(this.console.log)) {
            this.console.log(itemToLog);
        }
    };

    Logger.prototype.showMessage = function (settings) {
        var me = this, domElemToAppend;
        $.extend(me.defaultSettings, settings);
        /*ignore jslint start*/
        switch (me.defaultSettings.type) {
            case me.Types.error:
                domElemToAppend = '<div class="ektronloggermessages ektronuxerror"><div>' + me.defaultSettings.message + '</div></div>';
                break;
            case me.Types.success:
                domElemToAppend = '<div class="ektronloggermessages ektronuxsuccess"><div>' + me.defaultSettings.message + '</div></div>';
                break;
            default:
                domElemToAppend = '<div class="ektronloggermessages ektronuxinfo"><div>' + me.defaultSettings.message + '</div></div>';
                break;
        }
        /*ignore jslint end*/
        $(me.defaultSettings.appendMessageTo).append(domElemToAppend);

    };

    Logger.prototype.clear = function (settings) {
        var me = this, msgSelector;
        if (!settings) {
            $('body .ektronloggermessages').remove();
            return;
        }

        //remove the specific message appended to some dom element.
        if (settings.messageAppendedTo && 'undefined' === typeof(settings.type)) {
            $(settings.messageAppendedTo).remove(settings.messageToClear);
        }
        //remove messages of a certain type that are children of some dom element
        else if (settings.messageAppendedTo && 'undefined' !== typeof (settings.type)) {
            msgSelector = me.getMessageTypeCssClass(settings.type);
            $(settings.messageAppendedTo).find(msgSelector).remove();
        }
        //remove all messages from the dom of a certain type
        else if ('undefined' !== typeof (settings.type)) {
            msgSelector = me.getMessageTypeCssClass(settings.type);
            $(msgSelector).remove();
        }
        //remove the specific message from the dom
        else {
            $(settings.messageToClear).remove();
        }

    };

    return Logger;

});