// JavaScript Document

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
	}

/************************************************************************/
/* SLIDER (window height) */	
/* http://goo.gl/HbJEou	 */	
/* PAGE: national/index.html	local/index.html */
/************************************************************************/

$(window).load(function() {
    var windowH = $(window).height();
    var wrapperH = $('.flexslider').height();
    if(windowH > wrapperH) {                            
        $('.slider').css({'height':($(window).height())+'px'});
    }                                                                               
    $(window).resize(function(){
        var windowH = $(window).height();
        var wrapperH = $('.flexslider').height();
        var differenceH = windowH - wrapperH;
        var newH = wrapperH + differenceH;
        var truecontentH = $('.slides').height();
        if(windowH > truecontentH) {
            $('.flexslider').css('height', (newH)+'px');
        }

    });
  

/************************************************************************/
/* OUR APPROACH/OUR TEAM  */	
/* SECTION: MANAGED IT SERVICES/ABOUT */	
/* PAGE: local/managed_it_services.html / about/about.html				*/
/************************************************************************/

/*MANAGED IT SERVICES*/ 
$("#our_approach_tabs").tabs({
		collapsible: true,
		active: false,
		closable: true
		});
$('.our_approach_details .close').click(function() {
	
	$("#our_approach_tabs").tabs({
		collapsible: true,
		active: false,
		closable: true
		});
	
});



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
$('.close').click(function() {
	var row = $(this).parent().parent().parent().parent().attr('id'); //get row id: #our_team_tabs_row1
	
	$( "#"+row ).tabs( "option", "active", false ); //set active to "false" to collapse row
});


/*OUR TEAM HOVER*/
/*http://goo.gl/lgn3ZY*/

$('.fade').hover(
		function(){
			$(this).find('.caption').fadeIn(250);
			$(this).find('.caption_content').fadeIn(250);
			$(this).find('.square_button').attr('style', 'z-index:999');
		},
		function(){
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
var touch = Modernizr.touch;

   $('.img-holder').imageScroll({
              container: site_container,
              holderMinHeight: 580,
			  imageAttribute: (touch === true) ? 'image-mobile' : 'image',
             touch: touch
        });
	
	//sections above the footer
	$('.img-holder-footer-upper').imageScroll({
           container: $('.img_holder_wrapper')
     });	
	
	$('.img-holder-briefs-local').imageScroll({
            container: site_container,
              holderMinHeight: 420,
			
        });
	$('.img-holder-index').imageScroll({
              container: site_container,
              holderMinHeight: 750
        });

	$('.img-holder-experts').imageScroll({
              container: site_container,
              holderMinHeight: 892
        });
		
	$('#here_to_help_img').imageScroll({
    		container: $('#here_to_help_wrapper')
    });
	
	$('#how_we_can_help_img').imageScroll({
    		container: $('#how_we_can_help_wrapper')
    });
	
	$('#join_team_img').imageScroll({
    		container: $('#join_team_wrapper')
    });
	
	
	
	/*note: imageScroll works best when there are unique wrapper and img-holder div id's. The code below applies the id to the call for the Job section profiles images*/
	$('.img-holder-jobs').each(function(){
		var id = $(this).attr('id');
		
		$('#'+id).imageScroll({
      		container: $('#'+id+'_wrapper')
        });
	});
			
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
/* JOBS ACCORDION*/	
/* SECTION: JOBS														*/	
/* AUTHOR: Smartik89													*/
/* http://goo.gl/WuzE3h*/
/*************************************************************************/
 
$(".tbody").smk_Accordion({
	showIcon: true, // Show the expand/collapse icons.
	animation: true, // Expand/collapse sections with slide aniamtion.
	closeAble: true, // Closeable section.
	slideSpeed: 200 // the speed of slide animation.
});

/************************************************************************/
/* ADD ACTIVE LINK*/	
/* SECTION: ABOUT													*/	
/* AUTHOR: Tom Tu (STACK OVERFLOW)									*/
/* http://goo.gl/VdQLQF												*/
/*************************************************************************/
var url = window.location.pathname, 
        urlRegExp = new RegExp(url.replace(/\/$/,'') + "$"); // create regexp to match current url pathname and remove trailing slash if present as it could collide with the link in navigation in case trailing slash wasn't present there
        // now grab every link from the navigation
        $('.bottom_subnav a').each(function(){
            // and test its normalized href against the url pathname regexp
            if(urlRegExp.test(this.href.replace(/\/$/,''))){
                $(this).addClass('active');
            }
        });

/************************************************************************/
/* SUBSCRIBE OVERLAY*/	
/* SECTION: LOCAL													*/	
/* AUTHOR: Buck Wilson												*/
/* http://buckwilson.me/lightboxme/									*/
/*************************************************************************/
$('.subscribe').click(function(e) {
    $('#subscribe_form').lightbox_me({
        centered: true,
		overlayCSS: {background: 'black', 
opacity: 0.7},
		
        onLoad: function() { 
            $('#subscribe_form').find('input:first').focus();
            }
        });
    e.preventDefault();
});

/************************************************************************/
/* Request a Call OVERLAY*/	
/* SECTION: LOCAL													*/	
/* AUTHOR: Buck Wilson												*/
/* http://buckwilson.me/lightboxme/									*/
/*************************************************************************/
$('.request_call').click(function(e) {
    $('#request_call_form').lightbox_me({
        centered: true,
		overlayCSS: {background: 'black', 
opacity: 0.7},
		
        onLoad: function() { 
            $('#request_call_form').find('input:first').focus();
            }
        });
    e.preventDefault();
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
slideMargin: 10
				});

$('.bxslider-large').bxSlider({
		controls: false
});


/************************************************************************/
/* JOB APPLY OVERLAY*/	
/* SECTION: ABOUT													*/	
/* AUTHOR: Buck Wilson												*/
/* http://buckwilson.me/lightboxme/									*/
/*************************************************************************/
$('.apply').click(function(e) {
    $('#job_apply_form').lightbox_me({
        centered: true,
		overlayCSS: {background: 'black', 
opacity: 0.7},
		
        onLoad: function() { 
            $('#job_apply_form').find('input:first').focus();
            }
        });
    e.preventDefault();
});

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
				initEvents : function() {
					var obj = this;

					obj.dd.on('click', function(event){
						$(this).toggleClass('active');
						event.stopPropagation();
					});	
				}
			};

			$(function() {

				var dd = new DropDown( $('#dd') );

				$(document).click(function() {
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
				initEvents : function() {
					var obj = this;

					obj.dd.on('click', function(event){
						$(this).toggleClass('active');
						event.stopPropagation();
					});	
				}
			};

			$(function() {

				var dd = new DropDown( $('#dd-form') );

				$(document).click(function() {
					// all dropdowns
					$('.wrapper-dropdown-2').removeClass('active');
				});

			});
			
//********************************/
//* FANCYFORM INIT  */	
//* SECTION: MULTIPLE			*/	
//*******************************/
	
	var customSelect =  $(".custom-select").length;
	
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
				columns: 3
		});	
	}

/****************************************/
/*FAQ Q&A HIDE/SHOW SCRIPT*/
/*SECTION: ABOUT > FAQ */
/****************************************/

	$('.question').click(function(){
		var q_id = $(this).attr('id');
		var answer = $('#'+q_id+'_answer');
		
		//Close open answers except for the one corresponding to the clicked question
		$('.answer_wrapper').not(answer).slideUp(function(){
			$('.question').not(q_id).removeClass('active');
		});
		
		//Open answer of clicked question
		answer.slideToggle(function() {
			if (answer.is(":visible")) {
				$('#'+q_id).addClass('active');
			} 
			else {
				$('#'+q_id).removeClass('active');
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
		if(hash) {
		
			hash=hash.split('#'); //remove '#'
			
			hash = parseInt(hash[1]); //convert string into integer then subtract below to get starting slide number
			
			startSlide = (hash <= totalSlides && hash > 0) ? (hash-1) : 0; //only set startSlide number if the hash value is valid -- less than or equal to total slides AND greater than 0
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
		startSlide: startSlide,
		
		//set topic numbers on slider load
		onSliderLoad:function($currentIndex) {
			if (hash) {			
				
				setText(($currentIndex+2), ($currentIndex));
			}
			else {			
				setText(($currentIndex+2),totalSlides);
			}
			
		},
		
		//update slide number after slide transistion
		onSlideAfter: function($slideElement, oldIndex, newIndex){ 
			
			prevNum = (oldIndex+1);
			
			//if the next slide number = the total number of slides, set the next number to 1				
			if ((newIndex+1)==totalSlides) {
				nextNum = 1;
			}
			else {
				nextNum = newIndex+2; //add 2 to display next slide number
			}
			
			setText(nextNum, prevNum);
							
		}
	});
//************************************************************************/
//* PARTNERS (NATIONAL SITE) - DISPLAY DETAILS							*/	
//* SECTION: ABOUT US													*/	
//* PAGE: partners.html											*/
//************************************************************************/

	//*Display partner details */
	$('.partners .partner_logo a').click(function(e){
		
		e.preventDefault();
		
		$(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only
		
		var p_id = $(this).attr('id'); //get the id from the clicked logo
		
		var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
		
		row = row.split('_'); // split the row id string at the underscore
		var row_id = row[1]; // extract the number from the id string
		
		$(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo
		
		//if device width is greater than ~420px display the details box below the row of logos
		
		$(window).resize(function(){
			w = $(window).innerWidth();
		});
		
		//if (w > 640) {
		//alert(row_id);
			//open the details box located beneath the partner logo's row
			$('#detail_box_'+row_id).slideDown(function(){
				
				var content_container = $(this).find('.detail_content');
				
				content_container.html(''); //clear previous content
				
				$('#'+p_id+'_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box
	
				$(this).find('#'+p_id+'_detail').show(); //display the hidden partner_detail div
			});
	//	}
	//	else {
			
	//		$('.partner_detail').hide(); //hide other partner's detail
			
	//		$('#'+p_id+'_detail').show();
		
	//	}
		
		//Close partner details box
		$('#partners_container a.close_button').click(function(){
			
			var d_row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
			
			d_row = d_row.split('detail_box_'); // get the # from the id string
			
			$(this).parent().parent().parent().slideUp();
			$('.partner_row#row_'+d_row[1]+' .partner_logo').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
		});
		
		
		
	});
	
	
/****************************************/
/*Management Team*/
/*Details */
/****************************************/
	
		//*Display team details */
	$('#mgmt_team_main .cs_container a').click(function(e){
		
		e.preventDefault();
		
		$(this).parent().siblings().removeClass('active'); // remove active class (down arrow) from logo div in same row only
		
		var p_id = $(this).attr('id'); //get the id from the clicked logo
		var row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .parter_row div
		row = row.split('_'); // split the row id string at the underscore
		var row_id = row[1]; // extract the number from the id string
		
		$(this).parent().addClass('active'); // display white pointer arrow at the bottom of the logo
		
		
		$(window).resize(function(){
			w = $(window).innerWidth();
			//var w = window.innerWidth;
		});
		
		//console.log(w);
		
		//if (w > 465) {
		
			//open the details box located beneath the partner logo's row
			$('#profile_detail_box_'+row_id).slideDown(function(){
				
				var content_container = $(this).find('.profile_detail_content');
				
				content_container.html(''); //clear previous content
				
				$('#'+p_id+'_detail').clone().appendTo(content_container); //copy the hidden partner_detail div to the details box
	
				$(this).find('#'+p_id+'_detail').show(); //display the hidden partner_detail div
			});
		//}
		//else {
			
		//	  $('.profile_detail').hide(); //hide other partner's detail
			
		//	  $('#'+p_id+'_detail').show();
		   
			
		
		//}
		
	
		//Close partner details box
		$('a.close_button').click(function(){
			
			var d_row = $(this).parent().parent().parent().attr('id'); //get the id from the parent .partners_detail_row div
			d_row = d_row.split('profile_detail_box_'); // get the # from the id string
			
			$(this).parent().parent().parent().slideUp();
			$('.mgmt_profile_row#row_'+d_row[1]+' .cs_container').removeClass('active'); // remove active class (down arrow) from logo divs on the row //where the box is closed only
		});
	});
	
	if( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ) {
		// some code..
		$('#mgmt_team_main .cs_image_content_wrapper').css('display','block');

	}



}); //jQuery function