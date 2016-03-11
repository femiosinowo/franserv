﻿// http://stackoverflow.com/questions/931207/is-there-a-jquery-autogrow-plugin-for-text-fields
(function($){

    $.fn.autoGrowInput = function(o) {

        o = $.extend({
            maxWidth: 1000,
            minWidth: 0,
            comfortZone: 20 // Ektron
        }, o);

        this.filter('input:text').each(function(){

            var minWidth = o.minWidth || $(this).width(),
                val = '',
                input = $(this),
                testSubject = null, /*$('<tester/>').css({ // Ektron
                    position: 'absolute',
                    top: -9999,
                    left: -9999,
                    width: 'auto',
                    fontSize: input.css('fontSize'),
                    fontFamily: input.css('fontFamily'),
                    fontWeight: input.css('fontWeight'),
                    letterSpacing: input.css('letterSpacing'),
                    whiteSpace: 'nowrap'
                }),*/
                check = function() {

                    if (val === (val = input.val())) {return;}

					if (null == testSubject) // Ektron
					{
						testSubject = $('<tester/>').css({
							position: 'absolute',
							top: -9999,
							left: -9999,
							width: 'auto',
							fontSize: input.css('fontSize'),
							fontFamily: input.css('fontFamily'),
							fontWeight: input.css('fontWeight'),
							letterSpacing: input.css('letterSpacing'),
							whiteSpace: 'nowrap'
						});
						testSubject.insertAfter(input);
					}
                    // Enter new content into testSubject
                    var escaped = val.replace(/&/g, '&amp;').replace(/\s/g,'&nbsp;').replace(/</g, '&lt;').replace(/>/g, '&gt;');
                    testSubject.html(escaped);
                    
                    // Calculate new width + whether to change
                    var testerWidth = testSubject.width(),
                        newWidth = (testerWidth + o.comfortZone) >= minWidth ? testerWidth + o.comfortZone : minWidth,
                        currentWidth = input.width(),
                        isValidWidthChange = (newWidth < currentWidth && newWidth >= minWidth)
                                             || (newWidth > minWidth && newWidth < o.maxWidth);

                    // Animate width
                    if (isValidWidthChange) {
                        input.width(newWidth);
                    }

                };

            //testSubject.insertAfter(input); // Ektron

            $(this).bind('keyup keydown blur update', check);

        });

        return this;

    };

})($ektron);