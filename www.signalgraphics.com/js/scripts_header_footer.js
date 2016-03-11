/************************
FIX FOR IPHONE ORIENTATION BUG  
Source: http://www.blog.highub.com/mobile-2/a-fix-for-iphone-viewport-scale-bug
**************************/

var metas = document.getElementsByTagName('meta');
var i;

if (navigator.userAgent.match(/iPhone/i)) {
    for (i = 0; i < metas.length; i++) {
        if (metas[i].name == "viewport") {
            metas[i].content = "width=device-width, minimum-scale=1.0, maximum-scale=1.0";
        }
    }
    document.addEventListener("gesturestart", gestureStart, false);
}
function gestureStart() {
    for (i = 0; i < metas.length; i++) {
        if (metas[i].name == "viewport") {
            metas[i].content = "width=device-width, minimum-scale=0.25, maximum-scale=1.6";
        }
    }
}

$(document).ready(function () {

    /************************
    CALCULATE DEVICE WIDTH    
    ***********************/
    var w = $(window).innerWidth();

    //Get Inner Width after screen resize
    $(window).resize(function () {
        w = $(window).innerWidth();
    });

	
    /************************
    UTILITY MENU LINKS   
    ***********************/
	
	//test for user login div
	var loginStatus = $('.user_logged_in').length;	
	
	if(loginStatus > 0) {
		$('.send_icon').addClass('logged_in');
	}	
	else {
		$('.send_icon').removeClass('logged_in');
	}

    var u_link, u_content = $('.utility_content');

    $('a.utility_link, a.mm_utility_link').click(function (e) {

        $(this).parent().addClass('active').siblings().removeClass('active');

        e.preventDefault();

        u_link = $(this).attr('href');

        if (u_link == '#find_location') {

            $('.utility_nav_left li').removeClass('active');
            $('#search_social_close_wrapper').delay(1500).addClass('find_location');

        }
        else {
            $('#search_social_close_wrapper').removeClass('find_location');
        }
		
		//open utility menu if user is not logged in or is logged in an clicks links other than Send file
		if(((loginStatus != 0) && (u_link != '#send_file')) || (loginStatus == 0)) {
			
			$('.utility_nav_wrapper').addClass('open');

        	$('.close_utility_btn').show();

			if (u_content.is(':visible')) {
	
				u_content.hide(); //hide other open utility content
				$(u_link).show();
			}
			else {
				$(u_link).slideDown('slow');
			}
		}

		
        var link_class = $(this).attr('class');

        //if the link clicked was from the minimenu, close it then go to the top of the page
        if (link_class.indexOf('mm_utility_link') != -1) {
            $("#menu_button").removeClass("expanded");
            $("#navigation_list").removeClass("expanded");
            $("#minimenu").removeClass("child-expanded"); //apply padding when menu is expanded
            scrollTo(0, 0);
        }
        $('#request_quote_national.utility_content').show();
    });


    $('.close_utility_btn a').click(function (e) {

        e.preventDefault();

		$('.utility_nav_left li').removeClass('active'); //clear all active classes 
       
	   $('.utility_content').addClass('no-padding');

        u_content.hide().slideUp(1200, function () {
 			
            $('.close_utility_btn').hide();
            $('.utility_nav_wrapper').removeClass('open');
			
			 $('.quote_icon.local, .send_icon.local').addClass('active'); //restore active state to links on local site only
        });
        $('#request_quote_national.utility_content').show();
    });


	//Keep Send a File utility menu open if there is an error message
	
	var sendFileError = $('#ctl00_uxUtilityNav1_uxSendAFile_lblError');
	
	if (sendFileError.html()) {
		$('#send_file.utility_content').show(); 
		$('.utility_nav_wrapper').addClass('open'); 
		$('.close_utility_btn').show();
		sendFileError.addClass('error_on');
	}
	else {
		sendFileError.removeClass('error_on');
	}

    /********************
    MEGA MENU		
    SECTIONS: GLOBAL				
    /********************/

    /* show mini menu */
    $("#menu_button").click(function () {
        $("#menu_button").toggleClass("expanded");
        $("#navigation_list").toggleClass("expanded");
        $("#minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
    });

    /* show sub navigation mini menu */
    $(".mobile-nav-wrapper a").click(function () {
        var id = $(this).attr('id');

        $(".sub_menu_header").toggleClass("expanded");
        $("ul#" + id + "-menu").toggleClass("expanded");
        $("#sub-minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
    });


    /* mini menu expand children */
    $(".arrow-plus-minus, #minimenu .lvl-2-title").click(function () {
        $(this).parent().next(".lvl-3-list").slideToggle();
        event.preventDefault();
    });

   /* mega menu show and hide */
  
  $megamenuOuterWrap = $(".megamenu-outer-wrap");
  
  $megamenuOuterWrap.hide();
    
  $(document.body).on('click', '.desktop-nav-link.faux-hover > a' ,function() {
     
	  // cache selectors to improve efficency a tiny tiny bit
      $parentOfThis = $(this).parent();
     
	  // button 
      $parentOfThis.removeClass("faux-hover").addClass("no-hover");
      $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
      
	  // content slide up
	  $megamenuOuterWrap.css('z-index','1000').stop().slideUp(900, function(){
		
			setTimeout (function() {$('.main-navigation-wrap').removeClass('menu-open')}, 1000); //replace shadow and border
		});
	  
      // move main content down
      if (!$(".desktop-nav-link.faux-hover").length) {
          $(".main_nav_wrapper").stop().animate({ marginBottom: '0px'}, 900);
      }
	  
  });
  
  $(document.body).on('click', '.desktop-nav-link.no-hover > a' ,function() {
      
	  
	  // cache selectors to improve efficency a tiny tiny bit
      $parentOfThis = $(this).parent();
      // button 
      $parentOfThis.removeClass("no-hover").addClass("faux-hover");
      $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
      // content 
      $parentOfThis.siblings().find($megamenuOuterWrap).stop().css('z-index','1000').slideUp(900);
      
	  $('.main-navigation-wrap').addClass('menu-open'); // remove shadow and border
	  
	  	$parentOfThis.find($megamenuOuterWrap).stop().css('z-index','1001').slideDown(900);

	  
      // move main content down
      $(".main_nav_wrapper").stop().animate({ marginBottom: '550px'}, 600);
      

  });
  

    /**********************************************************
    FANCYFORM INIT 
    USED FOR: CUSTOM STYLES FOR THE FORM SELECT INPUT TYPE 	
    SECTION: MULTIPLE	
    REQUIRES: jquery.fancyform.js		
    ***********************************************************/

    var customSelect = $(".custom-select").length;

    if (customSelect != 0) {
        $(".custom-select").transformSelect();
    }


    /**********************************************************
    FANCYBOX (LIGHTBOX) INIT - Display lightbox on click   	
    SECTION: SUBSCRIBE MENU ITEM (LOCAL SITE ONLY)		  	
    REQUIRES: jquery.fancybox.js									  
    **********************************************************/

    var fancybox = $(".fancybox");

    // Call fancybox on pages with the fancybox class only
    if (fancybox.length > 0) {

        //View Map link
        $('.view-map.fancybox').fancybox({
            'type': 'iframe',
            'afterLoad': function () {
                $('.fancybox-wrap').attr('id', 'view-map-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
                this.title = '<h2>' + this.title + '</h2>'; /*Use <a> title attribute as the header */
            },
            'helpers': {
                'title': {
                    'type': 'inside', //move title above iframe
                    'position': 'top'
                }
            }
        });

        //Subscribe (Local)

        $("#subscribe_lb, #minimenu #subscribe_lb").fancybox({
            'type': 'inline',
            'autoScale': true,
            'autoDimensions': true,
            'showNavArrows': false,
            'afterLoad': function () {
                $('.fancybox-skin').attr('id', 'subscribe-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
            }

        });
    }


    // end
});