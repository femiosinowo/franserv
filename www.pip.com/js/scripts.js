// JavaScript Document


//*Fix for iPhone orientation bug
//Source: http://www.blog.highub.com/mobile-2/a-fix-for-iphone-viewport-scale-bug/
//*/
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

    //Get Inner Width
    var w = $(window).innerWidth();

    //Get Inner Width after screen resize
    $(window).resize(function () {
        w = $(window).innerWidth();
    });

    function winWidth() {
        //Get Inner Width
        var ww = $(window).innerWidth();

        //Get Inner Width after screen resize
        $(window).resize(function () {
            ww = $(window).innerWidth();
        });

        return ww;
    }


    //*******************************************************************************/
    //* SUBPAGE SUBNAVIGATION - Add active class to subnav items (TEMPORARY)*/
    //* SECTION: SUB NAVIGATION												*/
    //*******************************************************************************/

    //var page_id = $(".site_container").attr("data-id");

    var page_id = $(".site_container").attr('class').split(' ')[1];


    $("#sub_navigation .menu-items-block ul li").each(function (i) {

        var list_id = $(this).attr("class").split("-link");

        //compare id in the .site_container to the li class
        if (page_id == list_id[0]) {

            $("li." + list_id[0] + "-link").addClass('active');
        }

    });


    ////get the page title from the nav link
    var page_title = $("#sub_navigation .menu-items-block ul li.active a").html();

    $("#mobile-nav-header #page-title").html(page_title);

    $("#mobile-nav-header").click(function (e) {
        e.preventDefault();
        $('.sub_navigation_wrapper').toggleClass('sub_visible');

    });

    //products & services
    if (page_id == "products-services") {

        $("#mobile-nav-header #page-title").html("All Products &amp; Services");

    }






    //********************************************************/
    //* FLEXSLIDER INIT - PAGE HEADER SLIDERS */
    //* SECTIONS: HOME (NATIONAL & LOCAL) & JOIN OUR TEAM	*/			
    //********************************************************/

    //* Flexslider Init for main home page slider */
    $(".main_rotator .flexslider").flexslider({
        animation: "slide",
        slideshow: true
    });


    //********************************/
    //* BXSLIDER INIT  */
    //* SECTION: MULTIPLE			*/	
    //*******************************/

    //*Set the maximum number of slides shown based on the screen width*/
    var maxSlides;

    function setMaxSlides() {
        //var w = $(window).innerWidth();

        if (w < 465) {
            maxSlides = 1;
        } else {
            maxSlides = 5;
        }
    }

    setMaxSlides();

    $(window).resize(setMaxSlides);



    //Products & Services slider	
    $(".products_services ul").bxSlider({
        pager: false,
        slideWidth: 380,
        minSlides: 1,
        maxSlides: 3,
        moveSlides: 1
    });


    //Case study slider
    $(".case_studies_section ul").bxSlider({
        pager: false,
        slideWidth: 380,
        minSlides: 1,
        maxSlides: 3,
        moveSlides: 1
    });

    //Our Portolio slider
    $(".our_portfolio ul").bxSlider({
        pager: false,
        slideWidth: 380,
        minSlides: 1,
        maxSlides: 3,
        moveSlides: 1
    });

    //Awards Slider - About Us Landing Page
    $(".awards_slider ul").bxSlider({
        pager: false,
        slideWidth: 200,
        slideMargin: 15,
        minSlides: 1,
        maxSlides: 5,
        moveSlides: 1

    });

    //Job Profiles Slider - Join Our Team Landing Page
    $("#job_profile_slider ul").bxSlider({
        pager: false,
        slideWidth: 390,
        slideMargin: (w < 610) ? 0 : 10,
        //minSlides: 1,
        //maxSlides: 3,
        maxSlides: (w < 610) ? 1 : 3,
        moveSlides: 1,
        responsive: true
    });

    //Products & Services Landing page Top Slider
    $("#products-services .products_services_top_page_slider ul").bxSlider({
        pager: false,
        slideWidth: 150,
        slideMargin: 21,
        minSlides: 1,
        maxSlides: 7,
        moveSlides: 1
    });

    // Products & Services Category and Details Top Slider	
    $(".products_category_top_page_slider ul").bxSlider({
        pager: false,
        slideWidth: 82,
        slideMargin: 3,
        minSlides: 1,
        maxSlides: 14,
        moveSlides: 1
    });

    // Products & Services Category Inner (Content) Slider
    $(".products_category_inner_slider .flexslider").flexslider({
        animation: "slide",
        slideshow: false,
        directionNav: false,
        itemWidth: 578,
        maxItems: 1,
        itemMargin: 0
    });




    //************************************************************************/
    //* PARTNERS (NATIONAL SITE) - DISPLAY DETAILS							*/	
    //* SECTION: ABOUT US													*/	
    //* PAGE: partners.html											*/
    //************************************************************************/

    //*Display partner details */
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
        $('#partners_container .partners_detail_wrapper a.close_button').click(function () {

            var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
            d_row = d_row.split('detail_box_'); // get the # from the id string

            $(this).parent().parent().slideUp();
            $('.partner_row#row_' + d_row[1] + ' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
        });

        //Close partner details box
        $('#partners_container .partner_logo a.close_button').click(function () {

            $('partner_logo').removeClass('active');
            //var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
            //d_row = d_row.split('detail_box_'); // get the # from the id string

            //$(this).parent().parent().slideUp();
            //$('.partner_row#row_'+d_row[1]+' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
        });

    });

    //************************************************************************/

    //*Display partner details */
    $('.mgmt-team .cs_container a').click(function (e) {

        e.preventDefault();

        $(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only

        var p_id = $(this).attr('id'); //get the id from the clicked logo
        var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
        row = row.split('_'); // split the row id string at the underscore
        var row_id = row[1]; // extract the number from the id string

        $(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo



        $(window).resize(function () {
            w = $(window).innerWidth();
        });



        if (w > 465) {

            //open the details box located beneath the partner logo's row
            $('#profile_detail_box_' + row_id).slideDown(function () {

                var content_container = $(this).find('.profile_detail_content');

                content_container.html(''); //clear previous content

                $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div
            });
        }
        else {

            $('.profile_detail').hide(); //hide other partner's detail

            $('#' + p_id + '_detail').show();



        }



        //Close partner details box
        $('a.close_button').click(function () {

            var d_row = $(this).parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
            d_row = d_row.split('profile_detail_box_'); // get the # from the id string

            $(this).parent().parent().slideUp();
            $('.mgmt_profile_row#row_' + d_row[1] + ' .cs_container').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
        });
    });

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        // some code..
        $('#mgmt_team_main .cs_image_content_wrapper').css('display', 'block');

    }

    //*****************************************************************************************************/
    //* PRODUCTS & SERVICES - Inititalize content and thumbnail sliders in each product description area  */	
    //* SECTION: PRODUCTS & SERVICES																		 */	
    //* PAGE: products_services.html																		 */
    //*****************************************************************************************************/
    //* Loop through each instance of the sliders and apply the flexslider settings then sync the top and bottom sliders*/
    //$(".product-detail-wrapper").each(function (i) {

    //    var sectionID = $(this).attr("id");
    //    var topSlider = "#" + sectionID + "-large-slider";
    //    var bottomSlider = "#" + sectionID + "-thumb-slider";



    //    // The slider being synced (thumb slider) must be initialized first
    //    $(bottomSlider).flexslider({
    //        animation: "slide",
    //        controlNav: false,
    //        animationLoop: false,
    //        slideshow: false,
    //        itemWidth: 107,
    //        itemMargin: 10,
    //        maxItems: 5,
    //        move: 1,
    //        asNavFor: topSlider
    //    });

    //    $(topSlider).flexslider({
    //        animation: "slide",
    //        itemWidth: 578,
    //        maxItems: 1,
    //        move: 0,
    //        controlNav: false,
    //        animationLoop: false,
    //        slideshow: false,
    //        sync: bottomSlider
    //    });

    //    //Hide navigation if there are 5 or less thumbs
    //    var tCount = $(bottomSlider + " ul.slides li").length;
    //    if (tCount <= 5) {
    //        $(bottomSlider + " .flex-direction-nav").hide();
    //    }

    //});

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


    //*******************************************************************************************/
    //* DISPLAY OVERLAY & DETAILS ON CLICK
    //* SECTIONS/PAGES: 
    //	INSIGHTS > CASE STUDIES (NATIONAL): case_studies.html
    //	PORTFOLIO (LOCAL): portfolio.html
    //	ABOUT US > WHY DIFFERENT (LOCAL): why_we_are_different.html (Our Team section)
    //*******************************************************************************************/

    //Case Studies & Portfolio Pages - Hide/Show Content
    $("#case_studies_main .cs_image").hover(function (e) {
        e.preventDefault();
        $(".cs_image_content_wrapper").hide();
        $(this).parent().find(".cs_image_content_wrapper").show();
    });

    //Portfolio Pages - Hide/Show Content
    $(".portfolio_content .cs_image").hover(function (e) {
        e.preventDefault();
        $(".cs_image_content_wrapper").hide();
        $(this).parent().find(".cs_image_content_wrapper").show();
    });

    // About (Local) - Our Team Section - Hide/Show Team member info
    //$(".our_team .cs_image").click(function(e) {
    // e.preventDefault();
    // $(".our_team .cs_image_content_wrapper:not(.our_team .no_photo .cs_image_content_wrapper)").hide();
    // $(this).parent().find(".cs_image_content_wrapper").show();
    //	});


    //**********************************************************/
    //* FANCYBOX (LIGHTBOX) INIT - Display lightbox on click   */	
    //* SECTION: PORTFOLIO & SUBSCRIBE MENU ITEM (LOCAL)	*/	
    //* PAGE: portfolio.html								*/
    //**********************************************************/

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
                $('.fancybox-wrap').attr('id', 'subscribe-fancybox'); /*Add ID to .fancybox-skin for custom styling*/
            }

        });

        //Portfolio
        $(".portfolio_content .cs_image_content a").fancybox({

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

    //********************************************************************************/
    //* EASY RESPONSIVE TABS   */
    //* SECTION: JOIN OUR TEAM > JOB PROFILES	*/	
    //* PAGE: job_profiles.html														*/
    //* PLUGIN INFO: https://github.com/samsono/Easy-Responsive-Tabs-to-Accordion	*/
    //********************************************************************************/

    var tabs_ul = $('.resp-tabs-list').length; //*check for existence of tabs */

    if (tabs_ul !== 0) {
        $(".responsive_tabs").easyResponsiveTabs({
            type: 'default', //Types: default, vertical, accordion           
            width: 'auto', //auto or any custom width
            fit: true   // 100% fits in a container
        });
    }

    var active_tab = $(".resp-tab-active");

    //display quote
    $(".statement-text p#quote_" + active_tab.attr('data-tabname')).show();

    //change button text
    $(".profiles_red_bg .cta-button-text span").html(active_tab.html());

    //update quote and button text on click
    $(".resp-tab-item").click(function () {

        //clear current quote and button text
        $(".statement-text p").hide();
        $(".profiles_red_bg .cta-button-text span").html(" ");

        active_quote = $(this).attr('data-tabname');
        active_button = $(this).html();

        $(".statement-text p#quote_" + active_quote).show();
        $(".profiles_red_bg .cta-button-text span").html(active_button);
    });


    //***********************************************************/
    //* BACK TO PREVIOUS PAGE */
    //* SECTION/PAGES: 
    //	ABOUT US > MANAGEMENT TEAM - management_team.html
    //	JOIN OUR TEAM > JOB DESCRIPTION - job_description.html */
    //***********************************************************/

    $('.cta-button-wrap.back-button.prev-page').click(function (e) {
        e.preventDefault();
        history.go(-1);
    });

    //************************************************************************/
    //* NEWS (ARTICLE) - DISPLAY NEXT ARTICLE TITLE							*/	
    //* SECTION: ABOUT US													*/	
    //* PAGE: news_detail.html												*/
    //************************************************************************/	

    //*detect mobile browsers. Use click event to open on mobile and hover for desktop*/
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        $('#faux-slider a.bx-next').click(function (e) {
            e.preventDefault();
            $('#faux-slider .next_article').toggle();
        });
    }
    else {
        $('#faux-slider a.bx-next, #faux-slider .next_article ').hover(function () {
            $('.next_article').toggle();
        });
    }

    /****************************************/
    /*NEWS ARTICLE SLIDER */
    /*SECTION: ABOUT > NEWS > NEWS DETAILS */
    /****************************************/

    function setText(n, p) {
        var prevText = $('.article_title').eq(p).html(), /*get the topic name from the h3.article_title  in the slide content*/
			nextText = $('.article_title').eq(n).html();

        //Display article title inside of the bx controls
        $('.bx-prev').html(prevText);
        $('.bx-next').html(nextText);
    }

    var totalSlides = $('.news_article_wrapper').length,
		prevNum,
		nextNum,
		hash = (window.location.hash),
		startSlide;

    //get hash value from url to set starting slide
    if (hash) {

        hash = hash.split('#'); //remove '#'

        hash = parseInt(hash[1]); //convert string into integer then subtract below to get starting slide number

        startSlide = (hash <= totalSlides && hash > 0) ? (hash - 1) : 0; //only set startSlide number if the hash value is valid -- less than or equal to total slides AND greater than 0
    }
    else {
        startSlide = 0;
    }


    $('.news_article_slider').bxSlider({
        slideSelector: '.news_article_wrapper',
        nextSelector: '.news-next',
        prevSelector: '.news-prev',
        prevText: ' ',
        nextText: ' ',
        pager: false,
        startSlide: startSlide,

        //set topic numbers on slider load
        onSliderLoad: function ($currentIndex) {
            if (hash) {

                setText(($currentIndex + 2), ($currentIndex));
            }
            else {
                setText(($currentIndex + 2), totalSlides);
            }

        },

        //update slide number after slide transistion
        onSlideAfter: function ($slideElement, oldIndex, newIndex) {
            
            if (oldIndex == 0)
                prevNum = (totalSlides - 1);
            else
                prevNum = (oldIndex - 1);

            //if the next slide number = the total number of slides, set the next number to 1				
            if ((newIndex + 1) == totalSlides) {
                nextNum = 1;
            }
            else {
                nextNum = newIndex + 2; //add 2 to display next slide number
            }

            setText(nextNum, prevNum);

        }
    });


    //*************************************************************************/
    //* COLUMNIZER: http://welcome.totheinter.net/columnizer-jquery-plugin/ */	
    //* SECTION: FIND LOCATION - DETAIL PAGE									 */	
    //*************************************************************************/

    var cols = $('.columnizer').length;

    if (cols) {

        $('#locations_columns .location_list').addClass('dontsplit');

        $('#locations_columns h4').addClass('dontend');

        $('#locations_columns').columnize({
            buildOnce: false,
            width: 400
        });
    }

    //*************************************************************************/
    //* Products and Services page */	
    //* Adds class to nth child								 */	
    //*************************************************************************/

    $('.product-detail-wrapper:nth-child(2n)').addClass('even');


    //end document  
});

