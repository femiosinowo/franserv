
var $ = jQuery.noConflict();

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


//*******************************************************************************/
// SUBPAGE SUBNAVIGATION  
// SECTION: SUB NAVIGATION	
//*******************************************************************************/
$(window).load(function () {
    //get the page title from the nav link
    var page_title = $(".bottom_subnav ul li.active a").html();
    $("#mobile-nav-header #page-title").html(page_title);
    $("#mobile-nav-header").click(function (e) {
        e.preventDefault();
        $('.bottom_subnav').toggleClass('sub_visible');
    });
});

/******************/
/* MOBILE MENU	 */
/* PAGE: all */
/******************/
var originalNavClasses;

function toggleNav() {

    var elem = document.getElementById('bottom_nav');
    var classes = elem.className;
    if (originalNavClasses === undefined) {
        originalNavClasses = classes;
    }
    elem.className = /expanded/.test(classes) ? originalNavClasses : originalNavClasses + ' expanded';

    $('body').trigger('executeManualParallax');
}



/************************************************************************/
/* SLIDER (window height) */
/* http://goo.gl/HbJEou	 */
/* PAGE: national/index.html	local/index.html */
/************************************************************************/

$(window).load(function () {
    var windowH = $(window).height();
    var wrapperH = $('.flexslider').height();
    if (windowH > wrapperH) {
        // $('.slider').css({'height':($(window).height())+'px'});
    }
    $(window).resize(function () {
        var windowH = $(window).height();
        var wrapperH = $('.flexslider').height();
        var differenceH = windowH - wrapperH;
        var newH = wrapperH + differenceH;
        var truecontentH = $('.slides').height();
        if (windowH > truecontentH) {
            $('.flexslider').css('height', (newH) + 'px');
        }

    });


    /************************************************************************/
    /* OUR APPROACH/OUR TEAM  */
    /* SECTION: MANAGED IT SERVICES/ABOUT */
    /* PAGE: local/managed_it_services.html / about/about.html				*/
    /************************************************************************/



    /*ABOUT*/


    $("#our_team_tabs_row1").tabs({
        collapsible: true,
        active: false,
        closable: true
    });
    $("#our_team_tabs_row2").tabs({
        collapsible: true,
        active: false,
        closable: true
    });
    //Close opened row
    $('.close').click(function () {
        var row = $(this).parent().parent().parent().parent().attr('id'); //get row id: #our_team_tabs_row1

        $("#" + row).tabs("option", "active", false); //set active to "false" to collapse row
    });


    /*OUR TEAM HOVER*/
    /*http://goo.gl/lgn3ZY*/

    $('.fade').hover(
            function () {
                $(this).find('.caption').fadeIn(250);
                $(this).find('.caption_content').fadeIn(250);
                $(this).find('.square_button').attr('style', 'z-index:999');
            },
            function () {
                $(this).find('.caption').fadeOut(250);
                $(this).find('.caption_content').fadeOut(250);
                $(this).find('.square_button').attr('style', 'z-index:-1');
            }
    );



    /************************************************************************/
    /* PARALLAX SCROLLING*/
    /* SECTION: ALL PAGES													*/
    /* AUTHOR: Peder Andreas Nielsen										*/
    /* https://github.com/pederan/Parallax-ImageScroll						*/
    /************************************************************************/

    var site_container = $('.site_container');
    var touch = Modernizr.touch,
        badIE = $('html').hasClass('ie8');


    $('.img-holder').imageScroll({
        container: site_container,
        //holderMinHeight: 700,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        //coverRatio:0.8,
        parallax: !badIE,
        holderClass: 'imageHolder',

        touch: touch
    });

    //sections above the footer
    $('.img-holder-footer-upper').imageScroll({
        container: $('.img_holder_wrapper'),
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-briefs-local').imageScroll({
        container: site_container,
        // holderMinHeight: 420,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch

    });

    $('.img-holder-briefs').imageScroll({
        container: site_container,
        holderMinHeight: 620,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-managedit').imageScroll({
        container: site_container,
        //holderMinHeight: 250
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-index').imageScroll({
        container: site_container,
        // holderMinHeight: 250,
        // imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        // parallax: !badIE,
        touch: touch
    });

    $('.img-holder-why-work').imageScroll({
        container: site_container,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-experts').imageScroll({
        container: site_container,
        holderMinHeight: 892,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-help').imageScroll({
        container: $('#here_to_help_wrapper'),
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-connect').imageScroll({
        container: site_container,
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('#how_we_can_help_img').imageScroll({
        container: $('#how_we_can_help_wrapper'),
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });

    $('.img-holder-join').imageScroll({
        container: $('#join_team_wrapper'),
        imageAttribute: (touch === true) ? 'image-mobile' : 'image',
        parallax: !badIE,
        touch: touch
    });


    /*note: imageScroll works best when there are unique wrapper and img-holder div id's. The code below applies the id to the call for the Job section profiles images*/
    $('.img-holder-jobs').each(function () {
        var id = $(this).attr('id');

        $('#' + id).imageScroll({
            container: $('#' + id + '_wrapper'),
            imageAttribute: (touch === true) ? 'image-mobile' : 'image',
            parallax: !badIE,

            touch: touch
        });
    });

    /*img holder LESS than 760*/
    if ($(window).innerWidth() < 760 && touch == true) {
        $('.img-holder-index, .img-holder.solutions-parallax-div').css({
            display: "none"
        });
        $('.slider.local_slider').css({
            height: "273px",
            margin: "0"
        });
        $('.flexslider .slides img').css({
            width: "150%"
        });

        $('.img-holder-index img').css({
            height: "100%",
            maxWidth: "none",
            width: "100%",
            left: "0",
            top: "0",
            overflow: "hidden",

        });

        $('.img-holder,.img-holder-jobs,#here_to_help_img,.img-holder-join').css({
            visibility: "visible",
            height: "320px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder-why-work,.img-holder-footer-upper,.img-holder-managedit,.img-holder-briefs-local').css({
            height: "600px",
            overflow: "hidden",
            width: "auto"
        });



        $('.img-holder-experts').css({
            height: "920px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder-experts img').css({
            height: "100%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });

        $('.img-holder-help,.img-holder-connect').css({
            height: "430px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder-help img').css({
            height: "100%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });
        $('.img-holder-briefs').css({
            height: "1090px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder-briefs img').css({
            height: "100%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });

        $('.img-holder img,.img-holder-jobs img,#here_to_help_img img,.img-holder-why-work img,.img-holder-join img,img-holder-footer-upper img,.img-holder-connect img,.img-holder-managedit img,.img-holder-briefs-local img').css({
            height: "100%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });

    }

    /*img-holder GREATER than 761*/

    if ($(window).innerWidth() > 761 && touch == true) {
        $('.img-holder.solutions-parallax-div').css({
            display: "none"
        });

        $('.img-holder-index').css({
            height: "590px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder-index img').css({
            height: "100%",
            maxWidth: "none",
            width: "100%",
            left: "0",
            top: "0",
            overflow: "hidden"
        });

        $('.slider.local_slider').css({
            height: "440px",
            margin: "0 0"
        });
        $('.slider.local_slider .flex-caption').css({ margin: "-50px 0 0" });

        $('.img-holder,.img-holder-jobs,#here_to_help_img,.img-holder-join,.img-holder-why-work,.img-holder-footer-upper,.img-holder-connect,.img-holder-managedit').css({
            visibility: "visible",
            height: "620px",
            overflow: "hidden",
            width: "auto"
        });



        $('.img-holder-experts').css({
            height: "720px",
            overflow: "hidden",
            width: "auto"
        });


        $('.img-holder-help,.img-holder-briefs-local').css(
            { height: "430px", overflow: "hidden", width: "auto" }

            );

        $('.img-holder-connect').css(
        { height: "380px", overflow: "hidden", width: "auto" }

        );

        $('.img-holder-briefs').css({
            height: "850px",
            overflow: "hidden",
            width: "auto"
        });

        $('.img-holder img,.img-holder-jobs img,#here_to_help_img img,.img-holder-join img,.img-holder-why-work img,.img-holder-footer-upper img,.img-holder-briefs img,.img-holder-experts img,.img-holder-managedit img').css(
            { height: "100%", maxWidth: "none", width: "auto", left: "0", top: "0", overflow: "hidden", });

        $('.img-holder-help img,.img-holder-briefs-local img,.img-holder-connect img').css(
        { height: "auto", maxWidth: "none", width: "100%", left: "0", top: "0", overflow: "hidden", });

    }



    /*NATIONAL HOME  - img-holder*/
    if ($(window).innerWidth() < 760 && touch == true) {
        $('.national .img-holder').css({
            height: "320px",
            overflow: "hidden",
            width: "auto"
        });

        $('.national .img-holder img').css({
            height: "100%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });
    }


    if ($(window).innerWidth() > 761 && touch == true) {
        $('.national .img-holder').css({
            height: "620px",
            overflow: "hidden",
            width: "auto"
        });

        $('.national .img-holder img').css({
            height: "120%",
            maxWidth: "none",
            width: "auto",
            left: "0",
            top: "0",
            overflow: "hidden",

        });
    }

    if ($(window).innerWidth() > 1020 && touch == true) {

        $('.slider.local_slider').css({ height: "580px", margin: "0 0" });
        $('.slider.local_slider .flex-caption').css({ margin: "50px 0 0" });
    }




    /************************************************************************/
    /* SCROLL TO ANCHOR*/
    /* SECTION: MANAGED IT SERVICE										*/
    /* AUTHOR: hanoo (STACK OVERFLOW)										*/
    /* http://goo.gl/InEVVt*/
    /*************************************************************************/
    $(".scroll").click(function (event) {
        event.preventDefault();
        //calculate destination place
        var dest = 0;
        if ($(this.hash).offset().top > $(document).height() - $(window).height()) {
            dest = $(document).height() - $(window).height();
        } else {
            dest = $(this.hash).offset().top;
        }
        //go to destination
        $('html,body').animate({
            scrollTop: dest
        }, 2000, 'swing');
    });



    /************************************************************************/
    /* ADD ACTIVE LINK*/
    /* SECTION: ABOUT													*/
    /* AUTHOR: Tom Tu (STACK OVERFLOW)									*/
    /* http://goo.gl/VdQLQF												*/
    /*************************************************************************/
    var url = window.location.pathname,
            urlRegExp = new RegExp(url.replace(/\/$/, '') + "$"); // create regexp to match current url pathname and remove trailing slash if present as it could collide with the link in navigation in case trailing slash wasn't present there
    // now grab every link from the navigation
    $('.bottom_subnav a').each(function () {
        // and test its normalized href against the url pathname regexp
        if (urlRegExp.test(this.href.replace(/\/$/, ''))) {
            $(this).addClass('active');
        }
    });

    /************************************************************************/
    /* SUBSCRIBE OVERLAY*/
    /* SECTION: LOCAL													*/
    /* AUTHOR: Buck Wilson												*/
    /* http://buckwilson.me/lightboxme/									*/
    /*************************************************************************/
    $('.subscribe').click(function (e) {
        $('#subscribe_form').lightbox_me({
            centered: true,
            overlayCSS: {
                background: 'black',
                opacity: 0.7
            },

            onLoad: function () {
                $('#subscribe_form').appendTo('form:first');
                $('#subscribe_form').find('input:first').focus();
            }
        });
        e.preventDefault();
    });
    /************************************************************************/
    /* requestconsultation OVERLAY*/
    /* SECTION: LOCAL													*/
    /*************************************************************************/
    $('.consultation_flag').click(function (e) {
        $('#requestconsultation_form').lightbox_me({
            centered: true,
            overlayCSS: {
                background: 'black',
                opacity: 0.7
            },

            onLoad: function () {
                $('#requestconsultation_form').appendTo('form:first');
                $('#requestconsultation_form').find('input:first').focus();
            }
        });
        e.preventDefault();
    });
    $(function ()
    { $("a.consultation_flag").css('right', $(".location_nav.header_content").css('width')); }

    );


    /************************************************************************/
    /* Request a Call OVERLAY*/
    /* SECTION: LOCAL													*/
    /* AUTHOR: Buck Wilson												*/
    /* http://buckwilson.me/lightboxme/									*/
    /*************************************************************************/
    $('.request_call').click(function (e) {

        //as per the client they don't want this form and for national site users should re-direct to Locator page.
        //for local site users, it should go to contact us page.
        var centerId = $('.hddnCenterId').val();
        if (centerId != '')
            document.location.href = '/' + centerId + '/contact-us/';
        else
            document.location.href = '/find-locator/';


        //$('#request_call_form').lightbox_me({

        //    centered: true,
        //    overlayCSS: {
        //        background: 'black',
        //        opacity: 0.7
        //    },

        //    onLoad: function () {
        //        $('#request_call_form').appendTo('form:first');
        //        $('#request_call_form').find('input:first').focus();
        //    }
        //});
        //e.preventDefault();

    });
    /************************************************************************/
    /* TESTIMONIALS SLIDER*/
    /* SECTION: PROJECT EXPERTS											*/
    /* AUTHOR: Steven Wanderski											*/
    /* http://bxslider.com/												*/
    /*************************************************************************/
    $('.large_testimonial_slider').bxSlider({
        slideWidth: 290,
        minSlides: 2,
        maxSlides: 8,
        slideMargin: 10,
        adaptiveHeight: true,
        onSlideAfter: function () {
            $('body').trigger('executeManualParallax');
        }
    });

    $('.bxslider-large').bxSlider({
        controls: false,
        adaptiveHeight: true,
        onSlideAfter: function () {
            $('body').trigger('executeManualParallax');
        }
    });

    //$('#small_testimonial_slider').bxSlider({
    //				slideWidth:200,
    //					
    //});





    /************************************************************************/
    /* SUBSCRIBE MODAL CUSTOM DROPDOWN	*/
    /* SECTION: LOCAL													*/
    /* AUTHOR: HUGO GIRAUDEL												*/
    /* http://goo.gl/vOVuJ												*/
    /*************************************************************************/
    function DropDown(el) {
        this.dd = el;
        this.initEvents();
    }
    DropDown.prototype = {
        initEvents: function () {
            var obj = this;

            obj.dd.on('click', function (event) {
                $(this).toggleClass('active');
                event.stopPropagation();
            });
        }
    };

    $(function () {

        var dd = new DropDown($('#dd'));

        $(document).click(function () {
            // all dropdowns
            $('.wrapper-dropdown-2').removeClass('active');
        });

    });



    /************************************************************************/
    /* SUBSCRIBE CUSTOM DROPDOWN	*/
    /* SECTION: LOCAL													*/
    /* AUTHOR: HUGO GIRAUDEL												*/
    /* http://goo.gl/vOVuJ												*/
    /*************************************************************************/
    function DropDown(el) {
        this.dd = el;
        this.initEvents();
    }
    DropDown.prototype = {
        initEvents: function () {
            var obj = this;

            obj.dd.on('click', function (event) {
                $(this).toggleClass('active');
                event.stopPropagation();
            });
        }
    };

    $(function () {

        var dd = new DropDown($('#dd-form'));

        $(document).click(function () {
            // all dropdowns
            $('.wrapper-dropdown-2').removeClass('active');
        });

    });

    //********************************/
    //* FANCYFORM INIT  */	
    //* SECTION: MULTIPLE			*/	
    //*******************************/

    var customSelect = $(".custom-select").length;

    if (customSelect !== 0) {
        $(".custom-select").transformSelect();
    }

    /*************************************************************************
    COLUMNIZER: http://welcome.totheinter.net/columnizer-jquery-plugin/
    SECTION: FIND LOCATION
    *************************************************************************/

    var cols = $('.columnizer').length;

    if (cols) {

        $('#location_columns .location_list').addClass('dontsplit');

        $('#location_columns h4').addClass('dontend');

        $('#location_columns').columnize({
            buildOnce: false,
            width: 400
        });
    }

    /****************************************/
    /*FAQ Q&A HIDE/SHOW SCRIPT*/
    /*SECTION: ABOUT > FAQ */
    /****************************************/

    $('.question').click(function () {
        var q_id = $(this).attr('id');
        var answer = $('#' + q_id + '_answer');

        //Close open answers except for the one corresponding to the clicked question
        $('.answer_wrapper').not(answer).slideUp(function () {
            $('.question').not(q_id).removeClass('active');
        });

        //Open answer of clicked question
        answer.slideToggle(function () {
            if (answer.is(":visible")) {
                $('#' + q_id).addClass('active');
            }
            else {
                $('#' + q_id).removeClass('active');
            }
        });

    });



    /****************************************/
    /*FAQ SLIDER*/
    /*SECTION: ABOUT > FAQ > FAQ DETAILS */
    /****************************************/

    function setText(n, p) {
        var prevText = $('.topic_text').eq(p).html(), /*get the topic name from the .topic_text span in the slide content*/
			nextText = $('.topic_text').eq(n).html();

        $('.bx-prev').html(prevText);
        $('.bx-next').html(nextText);
    }

    var totalSlides = $('.topic_content_wrapper').length,
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


    $('#topic_slider').bxSlider({
        slideSelector: '.topic_content_wrapper',
        nextSelector: '.slider_control.next span',
        prevSelector: '.slider_control.prev span',
        prevText: ' ',
        nextText: ' ',
        pager: false,
        adaptiveHeight: true,
        startSlide: startSlide,
        touchEnabled: false,
		adaptiveHeight: true,

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

            prevNum = (oldIndex + 1);

            //if the next slide number = the total number of slides, set the next number to 1				
            if ((newIndex + 1) == totalSlides) {
                nextNum = 1;
            }
            else {
                nextNum = newIndex + 2; //add 2 to display next slide number
            }

            setText(nextNum, prevNum);
            $('.news_article p:last-child').css('margin-bottom', '50px');
			$('body').trigger('executeManualParallax');
        }
    });
    var newsHeight = $('.news_details_wrapper .bx-viewport').height();
    $('.news_details_wrapper .bx-viewport').css('height', newsHeight + 50);

    $('.news_article p:last-child').css('margin-bottom', '50px');
    //************************************************************************/
    //* PARTNERS (NATIONAL SITE) - DISPLAY DETAILS							*/	
    //* SECTION: ABOUT US													*/	
    //* PAGE: partners.html											*/
    //************************************************************************/
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



        if (w > 625) {
            //alert(row_id);
            //open the details box located beneath the partner logo's row
            $('#detail_box_' + row_id).slideDown(function () {

                var content_container = $(this).find('.detail_content');

                content_container.html(''); //clear previous content

                $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div

                $('body').trigger('executeManualParallax');
            });
        }
        else {

            $('.partner_detail').hide(); //hide other partner's detail

            $('#' + p_id + '_detail').show();

        }

        //Close partner details box
        $('#partners_container a.close_button').click(function () {

            var d_row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .partners_detail_row div

            d_row = d_row.split('detail_box_'); // get the # from the id string

            $(this).parent().parent().parent().slideUp(function () {
                $('body').trigger('executeManualParallax');
            });

            $('.partner_row#row_' + d_row[1] + ' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
        });



    });


    /****************************************/
    /*Management Team*/
    /*Details */
    /****************************************/

    //*Display team details */
    $('#mgmt_team_main .cs_container a').click(function (e) {

        e.preventDefault();

        $(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only

        var p_id = $(this).attr('id'); //get the id from the clicked logo
        var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
        row = row.split('_'); // split the row id string at the underscore
        var row_id = row[1]; // extract the number from the id string

        $(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo


        $(window).resize(function () {
            w = $(window).innerWidth();
            //var w = window.innerWidth;
        });

        //console.log(w);

        if (w > 465) {
            //open the details box located beneath the partner logo's row
            $('#profile_detail_box_' + row_id).slideDown(function () {

                var content_container = $(this).find('.profile_detail_content');

                content_container.html(''); //clear previous content

                $('#' + p_id + '_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box

                $(this).find('#' + p_id + '_detail').show(); //display the hidden partner_detail div

                $('body').trigger('executeManualParallax');
            });
        }
        else {

            $('.profile_detail').hide(); //hide other partner's detail

            $('#' + p_id + '_detail').show();
        }


        //Close partner details box
        $('a.close_button').click(function () {

            var d_row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
            d_row = d_row.split('profile_detail_box_'); // get the # from the id string

            $(this).parent().parent().parent().slideUp(function () {
                $('body').trigger('executeManualParallax');
            });

            $('.mgmt_profile_row#row_' + d_row[1] + ' .cs_container').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
        });
    });

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
        // some code..
        $('#mgmt_team_main .cs_image_content_wrapper').css('display', 'block');
    }


    //***********************************************************************/
    // JOBS ACCORDION
    // SECTION: JOBS
    // Source: http://stackoverflow.com/questions/9026867/jquery-accordion-effect-on-a-table
    //************************************************************************/

    var $jobs_table = $('.jobs_table');
    $jobs_table.find("tbody tr").not('.acc_content').hide();
    $jobs_table.find("tbody tr.acc_content").show();

    $jobs_table.find(".acc_content").click(function () {

        //	if($(this).hasClass('expanded')){
        //			$(this).removeClass('expanded');
        //			$(this).next().removeClass('expanded');
        //		}
        //		else{
        //			$(this).addClass('expanded');
        //			$(this).next().addClass('expanded');
        //		}
        $(this).toggleClass('expanded');
        $(this).next().toggleClass('expanded').fadeToggle(500);

    })

    //************************************************************************/
    //Click function for the IT Solutions Details more button	
    //************************************************************************/

    if (w > 959) {

        $('#more_drop').click(function (e) {
            e.preventDefault();
            $('.more_dropdown').slideToggle();

        }
		);


    } else {
        $('#more_drop').click(function (e) {
            e.preventDefault();
        });
        $('.bottom_subnav ul.it_solutions_subnav li#more_drop').css('background', 'none');

    }

    if (w > 959 && touch == true) {
        $('#more_drop').on('touchstart', function (e) {
            e.preventDefault();
            e.stopPropagation()
            $('.more_dropdown').slideToggle();

        }
		);
    }

}); //jQuery function

//************************************************************************/
//In the Media - MosaicFlow
//************************************************************************/
$(function () {
    $('#media_container').mosaicflow({
        itemSelector: '.media_article_wrapper',
        minItemWidth: 350
    });
});

//************************************************************************/
//IT Solutions - mobile hover/click state
//************************************************************************/
$(document).ready(function ($) {
    var deviceAgent = navigator.userAgent.toLowerCase();
    var agentID = deviceAgent.match(/(iphone|ipod|ipad)/);
    if (agentID) {

        $(".complete_grid .grid_6").click(function () {

        });

    }
});