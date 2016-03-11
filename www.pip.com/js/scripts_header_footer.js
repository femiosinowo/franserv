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


    //************************/
    //* UTILITY MENU LINKS   */
    //***********************/
    var u_link, u_content = $('.utility_content');

    $('a.utility_link, a.mm_utility_link').click(function (e) {

        //$(this).parent().addClass('active').siblings().removeClass('active');
        $('.utility_nav li').removeClass('active');
        $('.utility_content_wrapper').css('background-color', '#EFEFEF');

        $(this).parent().addClass('active');

        e.preventDefault();

        u_link = $(this).attr('href');

        $('.utility_nav_wrapper').addClass('open');

        $('.close_utility_btn').show();

        if (u_content.is(':visible')) {

            u_content.hide(); //hide other open utility content
            $(u_link).show();
        }
        else {
            $(u_link).slideDown('slow');
        }

        //social media - background blue
        if (u_link == '#social_media') {

            $('.utility_content_wrapper').css('background-color', '#1A9EFF');
        }

        var link_class = $(this).attr('class');

        //if the link clicked was from the minimenu, close it then go to the top of the page
        if (link_class.indexOf('mm_utility_link') != -1) {
            $("#menu_button").removeClass("expanded");
            $("#navigation_list").removeClass("expanded");
            $("#minimenu").removeClass("child-expanded"); //apply padding when menu is expanded
            scrollTo(0, 0);
        }

    });


    $('.close_utility_btn a').click(function (e) {

        e.preventDefault();

        $('.utility_content').addClass('no-padding');

        u_content.hide().slideUp(1200, function () {

            $('.close_utility_btn').hide();
            $('.utility_nav_wrapper').removeClass('open');
            $('.utility_content_wrapper').css('background-color', '#EFEFEF');
            $('.utility_nav li').removeClass('active');
        });

    });


    //********************/
    //* MEGA MENU		*/	
    //* SECTIONS: GLOBAL	*/			
    //********************/

    //* show mini menu */
    $("#menu_button").click(function () {
        $("#menu_button").toggleClass("expanded");
        $("#navigation_list").toggleClass("expanded");
        $("#minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
    });

    //* show sub navigation mini menu */
    $(".mobile-nav-wrapper a").click(function () {
        var id = $(this).attr('id');

        $(".sub_menu_header").toggleClass("expanded");
        $("ul#" + id + "-menu").toggleClass("expanded");
        $("#sub-minimenu").toggleClass("child-expanded"); //apply padding when menu is expanded
    });


    //* mini menu expand children */
    $(".arrow-plus-minus").click(function () {
        $(this).parent().next(".lvl-3-list").slideToggle();
        event.preventDefault();
    });

    //* mega menu show and hide */

    $(".megamenu-outer-wrap").hide();

    /* $(document.body).on('click', '.desktop-nav-link.faux-hover > a' ,function() {
         // cache selectors to improve efficency a tiny tiny bit
         $parentOfThis = $(this).parent();
         $megamenuOuterWrap = $(".megamenu-outer-wrap");
         // button 
         $parentOfThis.removeClass("faux-hover").addClass("no-hover");
         $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
         // content 
         $megamenuOuterWrap.css('z-index','1000').stop().slideUp(900);
         
         // move main content down
         if (!$(".desktop-nav-link.faux-hover").length) {
             $(".main_nav_wrapper").stop().animate({ marginBottom: '0px'}, 900);
         }
     }); 
     
      $(document.body).on('click', '.desktop-nav-link.no-hover > a' ,function() {
         // cache selectors to improve efficency a tiny tiny bit
         $parentOfThis = $(this).parent();
         $megamenuOuterWrap = $(".megamenu-outer-wrap");
         // button 
         $parentOfThis.removeClass("no-hover").addClass("faux-hover");
         //$('.desktop-nav-link.faux-hover').removeClass("faux-hover").addClass("no-hover");
         $parentOfThis.siblings().removeClass("faux-hover").addClass("no-hover");
         
         // content 
         $parentOfThis.siblings().find($megamenuOuterWrap).stop().css('z-index','1000').slideUp(900);
         $parentOfThis.find($megamenuOuterWrap).stop().css('z-index','1001').slideDown(900);
         
         // move main content down
         $(".main_nav_wrapper").stop().animate({ marginBottom: '590px'}, 600);
     });*/


    $(document.body).on('click', '.desktop-nav-link.faux-hover > a', function () {
        // cache selectors to improve efficency a tiny tiny bit
        $parentOfThis = $(this).parent();
        $megamenuOuterWrap = $(".megamenu-outer-wrap");

        // button 
        $parentOfThis.removeClass("faux-hover").addClass("no-hover");

        $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

        $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

        // content 
        $megamenuOuterWrap.css('z-index', '1000').stop().slideUp(900);

        // move main content down
        if (!$(".desktop-nav-link.faux-hover").length) {
            $(".main_nav_wrapper").stop().animate({ marginBottom: '0px' }, 900);
        }
    });

    $(document.body).on('click', '.desktop-nav-link.no-hover > a', function () {
        // cache selectors to improve efficency a tiny tiny bit
        $parentOfThis = $(this).parent();
        $megamenuOuterWrap = $(".megamenu-outer-wrap");

        // button 
        $parentOfThis.removeClass("no-hover").addClass("faux-hover");

        $(".desktop-nav-link").not($parentOfThis).removeClass("faux-hover").addClass("no-hover");

        // content 
        $(".desktop-nav-link").not($parentOfThis).find($megamenuOuterWrap).stop().css('z-index', '1000').slideUp(900);
        $parentOfThis.find($megamenuOuterWrap).stop().css('z-index', '1001').slideDown(900);

        // move main content down
        $(".main_nav_wrapper").stop().animate({ marginBottom: '590px' }, 600);
    });

    if ($(document.body).on('click', '.find-location-wrap .desktop-nav-link.no-hover > a', function () {
        // alert('help');
    })
        )

        //********************************/
        //* FANCYFORM INIT  */	
        //* SECTION: MULTIPLE			*/	
        //*******************************/

        var customSelect = $(".custom-select").length;

    if (customSelect !== 0) {
        $(".custom-select").transformSelect();
    }


    //*************************************************************************
    //* Equal Heights: http://codepen.io/micahgodbolt/pen/FgqLc								 
    //*************************************************************************
    //alert($(window).width())//

    //if ($(window).width() < 760) {
    // alert('Less than 960');

    //$('.col-height-equal').css("height","100% !important");
    //}
    //else {



    equalheight = function (container) {
        var currentTallest = 0,
          currentRowStart = 0,
          rowDivs = new Array(),
          $el,
          topPosition = 0;
        $(container).each(function () {

            $el = $(this);
            $($el).height('auto');
            topPosition = $el.position().top;

            if (currentRowStart != topPosition) {
                for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
                    rowDivs[currentDiv].height(currentTallest);
                }
                rowDivs.length = 0; // empty the array
                currentRowStart = topPosition;
                currentTallest = $el.height();
                rowDivs.push($el);
            } else {
                rowDivs.push($el);
                currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
            }

            for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
                rowDivs[currentDiv].height(currentTallest);
            }
        });
    };

    $(window).load(function () {
        equalheight('.col-height-equal');
    });

    $(window).resize(function () {
        equalheight('.col-height-equal');
    });

    //Register page - resizes height of column divs


    var oldHeight = $('.guest_register_saf').height();



    $(document).on('change', '.guest_register_saf', function () {
        // $('.guest_register_saf').on('resize change', function() {
        // console.log( $('.guest_register_saf').outerHeight() );

        $('.send_file_content .send_file_main').css('background', '#1084DB');

        var newHeight = $('.guest_register_saf').height();

        var adjustedHeight = newHeight - oldHeight;

        $('.send_file_content .col-height-equal').height($('.send_file_content .col-height-equal').height() + adjustedHeight);

        $('.guest_register_saf').css('height', 'auto');

        oldHeight = $('.guest_register_saf').height();


    });




    //end document
});