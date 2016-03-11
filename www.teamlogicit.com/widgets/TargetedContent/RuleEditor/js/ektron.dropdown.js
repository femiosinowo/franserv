
(function($){
	$.fn.dropdowns = function(o){
	
	    var settings = $.extend({
            event : 'click',
            timeout : 300,
            button : null
        }, o);
        
	    var $this = $(this);
	    var timeoutTimer = null;
	    
	    var title = $this.attr("title");
	    var $button;
	    
	    if(settings.button === null)
	    {
	        $button = $('<a href="#" class="ektron dropDownMenuButton ui-state-default" title="'+title+'"><span class="dropDownMenuText">'+title+'</span></a>")');
	        $this.before($button);
	    }
	    else
	    {
	        $button = settings.button;
	    }
	    
	    var $body = $("body");
	    $body.prepend($this);
	    
	    $this.hide();
	    
	    var clearTimer = function()
	    {
	        if(timeoutTimer != null)
	        {
	            window.clearTimeout(timeoutTimer);
	            timeoutTimer = null;
	        }
	    }
	    
	    var hideMenu = function()
	    {
	        clearTimer();
	        $this.hide();
            $this.css('z-index', '0');
	    }
	    
	    var showMenu = function()
	    {
	        var offset = $button.offset(),
	            zIndex = 999,
                isUx = $ektron("[data-ektron-pagebuilder-page]").length > 0 ? true : false;
	            
	        //if awesomizer - set z-index to 1 higher than parent dialog
	        if (isUx) {
	            zIndex = 1000000001;
	            $button.parents().each(function (i) {
	                var parentZIndex = $ektron(this).css("z-index");
	                if (!isNaN(parentZIndex)) {
	                    zIndex = Math.max(zIndex, parseInt(parentZIndex, 10));
	                }
	            });
	            zIndex = zIndex + 1;
	        }

	        $this.css('position', 'absolute');
	        $this.css('left', offset.left);
    	    
	        $this.css('position', 'absolute');
	        $this.css('top', offset.top + $button.outerHeight());
            $this.css('z-index', zIndex);
	        
	        $this.show();
	    };
	    
	    var resetTimeoutTimer = function()
	    {
	        clearTimer();
	        timeoutTimer = window.setTimeout(hideMenu, settings.timeout);
	    };
	    
	    $("> li > a",this).bind("click",
	        function(evt)
	        {
	            hideMenu();
	        });
	    
	    $button.bind(settings.event, function(evt)
	    {
	        evt.preventDefault();
	        
	        showMenu();
	    });
	    
	    $button.bind("mouseover", function(evt)
	    {
	        clearTimer();
	    });
	    
	    $button.bind("mouseout", function(evt)
	    {
	        resetTimeoutTimer();
	    });
	    
	    $this.bind("mouseover", function(evt)
	    {
	        clearTimer();
	    });
	    
	    $this.bind("mouseout", function(evt)
	    {
	        resetTimeoutTimer();
	    });
	    
	    return $button;
	};
})($ektron);
