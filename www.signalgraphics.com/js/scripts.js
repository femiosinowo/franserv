// JavaScript Document


$(document).ready(function () {

    //Get Inner Width
    var w = $(window).innerWidth();

    //Get Inner Width after screen resize
    $(window).resize(function () {
        w = $(window).innerWidth();
    });

    /*******************************************************************************
    SUBPAGE SUBNAVIGATION - Add active class to subnav items (TEMPORARY)	   
    SECTION: SUB NAVIGATION	   
    *******************************************************************************/
    //get the page title from the nav link
    var page_title = $(".sub_navigation ul li.active a").html();

    $("#mobile-nav-header #page-title").html(page_title);

    $("#mobile-nav-header").click(function (e) {
        e.preventDefault();
        $('.sub_navigation_wrapper').toggleClass('sub_visible');

    });



    /********************************************************
    FLEXSLIDER INIT - PAGE HEADER SLIDERS	
    SECTIONS: HOME (NATIONAL & LOCAL) & JOIN OUR TEAM	
    ********************************************************/

    /* Flexslider Init for main home page slider */
    $(".main_rotator .flexslider").flexslider({
        animation: "slide",
        slideshow: true
    });


    /******************************* 
    BXSLIDER INIT  				
    SECTION: MULTIPLE			
    *******************************/

    /*Set the maximum number of slides shown based on the screen width*/
    var maxSlides;

    function setMaxSlides() {
        //var w = $(window).innerWidth();

        if (w < 465) {
            maxSlides = 1;
        }
        else {
            maxSlides = 5;
        }
    }

    setMaxSlides();

    $(window).resize(setMaxSlides);

    //Products & Services slider	
    $(".products_services ul").bxSlider({
        pager: false,
        slideWidth: 300,
        minSlides: 1,
        maxSlides: maxSlides,
        moveSlides: 1
    });

    //Case study slider - Home page
    $(".our_portfolio_studies_home ul").bxSlider({
        pager: false,
        slideWidth: 238,
        minSlides: 1,
        maxSlides: maxSlides,
        moveSlides: 1
    });

    //Case study slider on inner pages (Products & Services pages)
    $(".our_portfolio_studies_inner ul").bxSlider({
        pager: false,
        slideWidth: 238,
        minSlides: 1,
        maxSlides: maxSlides,
        moveSlides: 1,
        infiniteLoop: false
    });

    //Awards Slider - About Us Landing Page
    $(".awards_slider ul").bxSlider({
        pager: false,
        slideWidth: 172,
        minSlides: 1,
        maxSlides: 6,
        moveSlides: 1,
        infiniteLoop: false
    });

    //Job Profiles Slider - Join Our Team Landing Page
    $("#job_profile_slider ul").bxSlider({
        pager: false,
        slideWidth: 390,
        slideMargin: (w < 610) ? 0 : 10,
        maxSlides: (w < 610) ? 1 : 3,
        moveSlides: 1,
        responsive: true,
        infiniteLoop: false
    });

    //Products & Services Landing page Top Slider
    $(".products_services_top_page_slider ul").bxSlider({
        pager: false,
        slideWidth: 150,
        slideMargin: 21,
        minSlides: 1,
        maxSlides: 7,
        moveSlides: 1,
        infiniteLoop: false
        //	hideControlOnEnd: true
    });

    // Products & Services Category and Details Top Slider	
    $(".products_category_top_page_slider ul").bxSlider({
        pager: false,
        slideWidth: 82,
        slideMargin: 3,
        minSlides: 1,
        maxSlides: 14,
        moveSlides: 1,
        infiniteLoop: false
    });

    // FLEXSLIDER: Products & Services Category Inner (Content) Slider
    $(".products_category_inner_slider .flexslider").flexslider({
        animation: "slide",
        slideshow: false,
        directionNav: false,
        itemWidth: 578,
        maxItems: 1,
        itemMargin: 0
    });


    /*set max-width on .bx-wrapper to display controls at the edges of the screen*/
    var slideWrapper = $('.bx-wrapper');

    if (slideWrapper.length != 0) {
        slideWrapper.each(function () {
            $(this).addClass('full-width'); // set max-width
        });
    }

    //Add class to slider wrappers in HTML with infiniteScroll disabled
    var noRepeat = $('.no-repeat-slides');



    if (noRepeat.length != 0) {

        /*Wait until slider is loaded before removing the full-width class*/
        setTimeout(function () {
            noRepeat.each(function (i) {

                var pControls = $(this).find('.bx-controls-direction a');

                //if controls are visible (based on the number of slides displayed)
                if (pControls.is(':hidden')) {

                    //remove class to center slider
                    $(this).find('.bx-wrapper').removeClass('full-width');
                }

                /*delay appearance of slider until after it is centered on a slow loading page.*/
                $(this).find('.bx-wrapper').animate({ zIndex: 0 }, 400);

            }); //end loop

        }, 800);
    }


    /************************************************************************
    MANAGEMENT TEAM SLIDER (NATIONAL SITE) 
    SECTION: ABOUT US	
    PAGE: management_team.html	
    ************************************************************************/
    var mgmtSlider = $("#mgmt_team_slider ul").bxSlider({
        pager: false,
        mode: 'fade',
        speed: 0,
        slideWidth: 1200,
        minSlides: 1,
        maxSlides: 1,
        moveSlides: 1,
		adaptiveHeight: true
    });

    //Display slider
    $("#mgmt_team_main .cs_image_content_wrapper").click(function (e) {
        e.preventDefault();
        var n = $(this).attr('id');

        //Hide team pictures and display slider
        $("#mgmt_team_main").fadeOut(function () {

            $("#mgmt_team_slider_wrapper").css({'visibility': 'visible', 'height': '100%'}).fadeIn(function () {

                mgmtSlider.goToSlide((parseInt(n) - 1)); //skip to slide based on href value minus 1

                /* By default bxSlider will display the first slide then skip to the desired slide. Use the animate effect to hide slide content until the slide transition is complete. */
                $("#mgmt_team_slider li").animate({
                    opacity: '1'
                }, 500);
            });
        });
    });

    //Close Management slider and display pictures
    $("#hide_mgmt_slider").click(function (e) {
        e.preventDefault();
        $("#mgmt_team_slider_wrapper").css({'visibility': 'hidden', 'height': '0'}).fadeOut();
        $("#mgmt_team_main").fadeIn();
        $(".cs_image_content_wrapper").hide();
        $("#mgmt_team_slider li").css({ opacity: '0' }); //reset opacity to 0
    });

    /************************************************************************
    PARTNERS (NATIONAL SITE) - DISPLAY DETAILS	
    SECTION: ABOUT US	
    PAGE: partners.html	
    ************************************************************************/

    /*Display partner details */
    $('.partners .partner_logo a').click(function (e) {

        e.preventDefault();

        $(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only

        var p_id = $(this).attr('id'); //get the id from the clicked logo
        var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
        row = row.split('_'); // split the row id string at the underscore
        var row_id = row[1]; // extract the number from the id string

        $(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo

        //if device width is greater than ~420px display the details box below the row of logos

        $(window).resize(function () {
            w = $(window).innerWidth();
        });

        if (w > 640) {

            //open the details box located beneath the partner logo's row
            $('#detail_box_' + row_id).slideDown(function () {

                var content_container = $(this).find('.detail_content');

                content_container.html(''); //clear previous content

                $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div
            });
        }
        else {

            $('.partner_detail').hide(); //hide other partner's detail

            $('#' + p_id + '_detail').show();

        }

        //Close partner details box
        $('a.close_button').click(function () {

            var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
            d_row = d_row.split('detail_box_'); // get the # from the id string

            $(this).parent().parent().slideUp();
            $('.partner_row#row_' + d_row[1] + ' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row where the box is closed only
        });
    });

    /*****************************************************************************************************
    PRODUCTS & SERVICES - Inititalize content and thumbnail sliders in each product description area  
    SECTION: PRODUCTS & SERVICES	
    PAGE: products_services.html	
    *****************************************************************************************************/
    /* Loop through each instance of the sliders and apply the flexslider settings then sync the top and bottom sliders*/
    $(".product-detail-wrapper").each(function (i) {
		
		var id = $(this).attr("id");
		
		//remove punctuation from id	
		var sectionID = id.replace(/[\.,\/#!$%\^&\*;:{}=\_`~()]/g,"");
		
        //Apply filtered id to the section slider divs
		$(this).find('.large-slider.flexslider').attr("id", sectionID+"-large-slider");
		$(this).find('.thumb-slider.flexslider').attr("id", sectionID+"-thumb-slider");
        
		var topSlider = "#" + sectionID + "-large-slider";
        var bottomSlider = "#" + sectionID + "-thumb-slider";

        // The slider being synced (thumb slider) must be initialized first
        $(bottomSlider).flexslider({
            animation: "slide",
            controlNav: false,
            animationLoop: false,
            slideshow: false,
            itemWidth: 107,
            itemMargin: 10,
            maxItems: 5,
            move: 1,
            asNavFor: topSlider
        });

        $(topSlider).flexslider({
            animation: "slide",
            itemWidth: 578,
            maxItems: 1,
            move: 0,
            controlNav: false,
            animationLoop: false,
            slideshow: false,
            sync: bottomSlider
        });

        //Hide navigation if there are 5 or less thumbs
        var tCount = $(bottomSlider + " ul.slides li").length;
        if (tCount <= 5) {
            $(bottomSlider + " .flex-direction-nav").hide();
        }

    });

    // "Hide" captions on the Product & Services page sliders

    $('.product-detail-slider .large-slider .slides li').append('<a class="hide-caption">Hide</a>');

    $('a.hide-caption').click(function () {

        var caption = $(this).parent().find('.caption');

        caption.toggle();

        if (caption.is(':hidden')) {
            $(this).html('Show Caption').addClass('display-hidden');
        }
        else {
            $(this).html('Hide').removeClass('display-hidden');
        }
    });


    /*******************************************************************************************
    DISPLAY RED OVERLAY & DETAILS ON CLICK   
    SECTIONS/PAGES: 
    INSIGHTS > CASE STUDIES (NATIONAL): case_studies.html
    PORTFOLIO (LOCAL): portfolio.html
    ABOUT US > WHY DIFFERENT (LOCAL): why_we_are_different.html (Our Team section) 
    *******************************************************************************************/

    //Case Studies & Portfolio Pages - Hide/Show Content
    $(".insights_case_studies .cs_image").hover(function (e) {
        e.preventDefault();
        $(".cs_image_content_wrapper").hide();
        $(this).parent().find(".cs_image_content_wrapper").show();
    });

    // About (Local) - Our Team Section - Hide/Show Team member info
    $(".our_team .cs_image").hover(function (e) {
        e.preventDefault();
        $(".our_team .cs_image_content_wrapper:not(.our_team .no_photo .cs_image_content_wrapper)").hide();
        $(this).parent().find(".cs_image_content_wrapper").show();
    });


    /**********************************************************
    FANCYBOX (LIGHTBOX) INIT - Display lightbox on click   
    SECTION: PORTFOLIO & SUBSCRIBE MENU ITEM (LOCAL)	  
    PAGE: portfolio.html	  
    **********************************************************/

    var fancybox = $(".fancybox");

    // Call fancybox on pages with the fancybox class only
    if (fancybox.length > 0) {

        //Subscribe (Local)
        $("#subscribe_lb, #minimenu #subscribe_lb").fancybox({
            'type': 'inline',
            'autoScale': true,
            'autoDimensions': true,
            'showNavArrows': false,
            'afterLoad': function () {
                $('.fancybox-wrap').attr('id', 'subscribe-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
            }

        });

        //Portfolio 
        $(".portfolio .cs_image_content a").fancybox({

            prevEffect: 'none',
            nextEffect: 'none',

            //display content inside cs_image_content divs as text below image
            beforeLoad: function () {
                var el, id = $(this.element).data('content-id');

                if (id) {
                    el = $('#' + id);

                    if (el.length) {
                        this.title = el.html();
                    }
                }
            }
        });
    }


    /********************************************************************************
    EASY RESPONSIVE TABS    	    
    SECTION: JOIN OUR TEAM > JOB PROFILES	  	    
    PAGE: job_profiles.html	
    PLUGIN INFO: https://github.com/samsono/Easy-Responsive-Tabs-to-Accordion	
    ********************************************************************************/

    var tabs_ul = $('.resp-tabs-list').length; /*check for existence of tabs */

    if (tabs_ul != 0) {

        $(".responsive_tabs").easyResponsiveTabs({
            type: 'default', //Types: default, vertical, accordion           
            width: 'auto', //auto or any custom width
            fit: true   // 100% fits in a container
        });
    }

    var active_tab = $(".resp-tab-active");

    //display quote
    $(".profiles_red_bg .statement-text p").hide();
    $(".statement-text p.quote_" + active_tab.attr('aria-controls')).show();

    //change button text
    $(".profiles_red_bg .cta-button-text span").html(active_tab.html());

    //update quote and button text on click
    $('.responsive_tabs *[role="tab"]').click(function () {
        
        $(".statement-text p").show();
        $(".profiles_red_bg .statement-text p").hide();
        $(".profiles_red_bg .cta-button-text span").html(" ");

        active_quote = $(this).attr('aria-controls');
        active_button = $(this).html();

        $(".statement-text p.quote_" + active_quote).show();
        $(".profiles_red_bg .cta-button-text span").html(active_button);

    });


    /***********************************************************
    BACK TO PREVIOUS PAGE  	  
    SECTION/PAGES: 
    ABOUT US > MANAGEMENT TEAM - management_team.html
    JOIN OUR TEAM > JOB DESCRIPTION - job_description.html 
    ***********************************************************/

    $('.cta-button-wrap.back-button.prev-page').click(function (e) {
        e.preventDefault();
        history.go(-1);
    });

    /************************************************************************
    NEWS (ARTICLE) - DISPLAY NEXT ARTICLE TITLE	
    SECTION: ABOUT US	
    PAGE: news_detail.html	
    ************************************************************************/

    /*detect mobile browsers. Use click event to open on mobile and hover for desktop*/
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        $('#faux-slider a.bx-next').click(function (e) {
            e.preventDefault();
            $('#faux-slider .next_article').toggle();
        });

        $('#faux-slider a.bx-prev').click(function (e) {
            e.preventDefault();
            $('#faux-slider .prev_article').toggle();
        });
    }
    else {
        $('#faux-slider a.bx-next, #faux-slider .next_article').hover(function () {
            $('.next_article').toggle();
        });
        $('#faux-slider a.bx-prev, #faux-slider .prev_article').hover(function () {
            $('.prev_article').toggle();
        });
    }


    /*************************************************************************
    COLUMNIZER: http://welcome.totheinter.net/columnizer-jquery-plugin/ 
    SECTION: FIND LOCATION - DETAIL PAGE	
    *************************************************************************/

    var cols = $('.columnizer').length;

    if (cols) {

        $('#locations_columns .location_list').addClass('dontsplit');

        $('#locations_columns h4').addClass('dontend');

        $('#locations_columns').columnize({
            buildOnce: false,
            width: 400
        });
    }

    //end document  
});
