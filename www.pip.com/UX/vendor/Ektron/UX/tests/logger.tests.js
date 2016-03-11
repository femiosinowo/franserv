/// <reference path="setup.js" />
/// <reference path="../js/logger.js" />

var unitUnderTest, $, Logger;

function MessageTypes() {
    'use strict';
    this.error = 'error';
    this.info = 'info';
    this.success = 'success';
}
module('Logger Tests', {
    setup: function () {
        'use strict';

        $ = function (selector) {
            return this; 
        };

        $.append = function (itemToAppend) {};
        $.extend = function (obj1, obj2) {};

        
        Logger = window.createModule($);

    }
});

test('Logger logs to the console if available.', function () {
    'use strict';
    var helloString = 'hello world', correctItemLogged = false;

    unitUnderTest = new Logger();

    console = undefined;
    //console is undefined first.
    unitUnderTest.log(helloString);

    console = {};
    unitUnderTest = new Logger(console);
    //console has no method log
    unitUnderTest.log(helloString);
   
    console.log = function (itemToLog) {
        correctItemLogged = itemToLog === helloString;
    };

    unitUnderTest.log(helloString);

    ok(correctItemLogged, 'The item was logged to the console and no errors were thrown.');
});

test('Logger showMessage inserts domElement with correct message text.', function () {
    'use strict';
    var msg = 'hello world',
        appendCallCount = 0,
        extendCallCount = 0,
        correctSelector = false,
        rightSelector = 'mydom',
        correctItem = false,
        rightErrorItemToAppend = '<div class="ektronloggermessages ektronuxerror"><div>hello world</div></div>',
        rightInfoItemToAppend = '<div class="ektronloggermessages ektronuxinfo"><div>hello world</div></div>',
        rightSuccessItemToAppend = '<div class="ektronloggermessages ektronuxsuccess"><div>hello world</div></div>',
        settings = {},
        types = new MessageTypes();

    settings.type = types.error;
    settings.message = msg;
    settings.appendMessageTo = rightSelector;

    $ = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            append: function (itemToAppend) {
                appendCallCount = appendCallCount + 1;
                correctItem = itemToAppend === rightErrorItemToAppend;
                return this;
            }
        };
    };

    $.extend = function (obj1, obj2) {
        extendCallCount = extendCallCount + 1;
        obj1.type = obj2.type;
        obj1.appendMessageTo = obj2.appendMessageTo;
        obj1.message = obj2.message;
    };
    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.showMessage(settings);

    ok(correctSelector, 'The correct selector was passed to jQuery to append the message to.');
    ok(correctItem, 'The correct item (an error) was appened.');
    equal(appendCallCount, 1, 'Append was called the correct amount of times.');
    equal(extendCallCount, 1, 'jQuery extend was called once.');

    //reset up
    appendCallCount = 0;
    settings = {};
    settings.message = msg;
    settings.appendMessageTo = rightSelector;
    settings.type = types.info;

    $ = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            append: function (itemToAppend) {
                appendCallCount = appendCallCount + 1;
                correctItem = itemToAppend === rightInfoItemToAppend;
                return this;
            }
        };
    };

   

    $.extend = function (obj1, obj2) {
        obj1.type = obj2.type;
        obj1.appendMessageTo = obj2.appendMessageTo;
        obj1.message = obj2.message;
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.showMessage(settings);

    ok(correctItem, 'The correct item (info) was appened.');
    equal(appendCallCount, 1, 'Append was called the correct amount of times.');

    //reset up
    appendCallCount = 0;
    settings = {};
    settings.message = msg;
    settings.appendMessageTo = rightSelector;
    settings.type = types.success;

    $ = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            append: function (itemToAppend) {
                appendCallCount = appendCallCount + 1;
                correctItem = itemToAppend === rightSuccessItemToAppend;
                return this;
            }
        };
    };

   

    $.extend = function (obj1, obj2) {
        obj1.type = obj2.type;
        obj1.appendMessageTo = obj2.appendMessageTo;
        obj1.message = obj2.message;
    };
    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.showMessage(settings);

    ok(correctItem, 'The correct item (success) was appened.');
    equal(appendCallCount, 1, 'Append was called the correct amount of times.');


});

test('Logger removes the error when asked to do so using appended dom element.', function () {
    'use strict';
    var correctItemToRemove = false, correctAppendedItem = false,
        ItemToRemove = 'testDom', AppendedItem = 'testParent',
        jQueryRemoveCallCount = 0, settings = {};

    settings.messageToClear = ItemToRemove;
    settings.messageAppendedTo = AppendedItem;

    $ = function (selector) {
        correctAppendedItem = selector === AppendedItem;
        return {
            remove: function (itemToRemove) {
                jQueryRemoveCallCount = jQueryRemoveCallCount + 1;
                correctItemToRemove = itemToRemove === ItemToRemove;
                return this;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    
    unitUnderTest.clear(settings);

    equal(jQueryRemoveCallCount, 1, 'jQuery remove was called once.');
    ok(correctItemToRemove, 'The correct item was removed');
    ok(correctAppendedItem, 'The correct element which item to be removed from was provided.');

});

test('Logger removes the error when asked to do so without parent element.', function () {
    'use strict';
    var correctItemToRemove = false, ItemToRemove = 'testDom', itemToRemoveIsUndefined = false,
        removeCallCount = 0, settings = {};

    settings.messageToClear = ItemToRemove;

    $ = function (selector) {
        correctItemToRemove = selector === ItemToRemove;
        return {
            remove: function (itemToRemove) {
                itemToRemoveIsUndefined = 'undefined' === typeof (itemToRemove);
                removeCallCount = removeCallCount + 1;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.clear(settings);

    ok(correctItemToRemove, 'The correct item was selected to be removed.');
    ok(itemToRemoveIsUndefined, 'An element was not passed to the remove function as the selector was meant to be deleted.');
    equal(removeCallCount, 1, 'jQuery remove was called once.');

});

test('Logger removes messages of a certain type', function () {
    'use strict';

    var correctItemToRemove = false, errorItemToRemove = '.ektronuxerror',
        successItemToRemove = '.ektronuxsuccess', infoItemToRemove = '.ektronuxinfo',
        removeCallCount = 0, settings = {};

    settings.type = 'error';

    $ = function (selector) {
        correctItemToRemove = selector === errorItemToRemove;
        return {
            remove: function (itemToRemove) {
                removeCallCount = removeCallCount + 1;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.clear(settings);
    ok(correctItemToRemove, 'The error item(s) were selected for removal.');

    //reset
    settings.type = 'success';
    correctItemToRemove = false;
    $ = function (selector) {
        correctItemToRemove = selector === successItemToRemove;
        return {
            remove: function (itemToRemove) {
                removeCallCount = removeCallCount + 1;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    unitUnderTest.clear(settings);
    ok(correctItemToRemove, 'The success item(s) were selected for removal.');

    //reset
    settings.type = 'info';
    correctItemToRemove = false;
    $ = function (selector) {
        correctItemToRemove = selector === infoItemToRemove;
        return {
            remove: function (itemToRemove) {
                removeCallCount = removeCallCount + 1;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    unitUnderTest.clear(settings);
    ok(correctItemToRemove, 'The info item(s) were selected for removal.');
    equal(removeCallCount, 3, 'jQuery remove was called the correct amount of times.');


});

test('Logger removes messages of a certain type given a selector.', function () {
    'use strict';
    var correctSelectorUsed = false, correctSelector = 'test',
        removeCallCount = 0, settings = {},
        errorItemToRemove = '.ektronuxerror',
        successItemToRemove = '.ektronuxsuccess', infoItemToRemove = '.ektronuxinfo',
        correctItemToRemove = false;

    settings.type = 'info';
    settings.messageAppendedTo = correctSelector;

    $ = function (selector) {
        correctSelectorUsed = selector === correctSelector;
        return {
            find: function (itemToRemove) {
                correctItemToRemove = itemToRemove === infoItemToRemove;
                
                return {
                    remove: function (itemToRemove) {
                        removeCallCount = removeCallCount + 1;
                    }
                };
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    unitUnderTest.clear(settings);

    ok(correctSelectorUsed, 'The correct selector was used to find the messages to remove.');
    ok(correctItemToRemove, 'The correct message (info) was selected for removal.');


    //reset

    settings.type = 'success';
    settings.messageAppendedTo = correctSelector;
    correctItemToRemove = false;
    $ = function (selector) {
        correctSelectorUsed = selector === correctSelector;
        return {
            find: function (itemToRemove) {
                correctItemToRemove = itemToRemove === successItemToRemove;

                return {
                    remove: function (itemToRemove) {
                        removeCallCount = removeCallCount + 1;
                    }
                };
            }
        };
    };
    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    unitUnderTest.clear(settings);

    ok(correctSelectorUsed, 'The correct selector was used to find the messages to remove.');
    ok(correctItemToRemove, 'The correct message (success) was selected for removal.');

    //reset

    settings.type = 'error';
    settings.messageAppendedTo = correctSelector;
    correctItemToRemove = false;
    $ = function (selector) {
        correctSelectorUsed = selector === correctSelector;
        return {
            find: function (itemToRemove) {
                correctItemToRemove = itemToRemove === errorItemToRemove;

                return {
                    remove: function (itemToRemove) {
                        removeCallCount = removeCallCount + 1;
                    }
                };
            }
        };
    };
    Logger = window.createModule($);
    unitUnderTest = new Logger(console);
    unitUnderTest.clear(settings);

    ok(correctSelectorUsed, 'The correct selector was used to find the messages to remove.');
    ok(correctItemToRemove, 'The correct message (error) was selected for removal.');
    equal(removeCallCount, 3, 'jquery remove was called the correct amount of times.');

});

test('Logger handles no no settings appropriately.', function () {
    'use strict';

    var correctSelector = false, rightSelector = 'body .ektronloggermessages', removeCallCount = 0;

    $ = function (selector) {
        correctSelector = selector === rightSelector;
        return {
            remove: function () {
                removeCallCount = removeCallCount + 1;
            }
        };
    };

    Logger = window.createModule($);
    unitUnderTest = new Logger(console);

    unitUnderTest.clear();

    ok(correctSelector, 'The correct selector of body .messages was used to get all messages.');
    equal(removeCallCount, 1, 'jQuery remove was called once');

});







