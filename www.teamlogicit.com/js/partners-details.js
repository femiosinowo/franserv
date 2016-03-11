/************************************************************************/
/* PARTNERS (NATIONAL SITE) - DISPLAY DETAILS							*/	
/* SECTION: ABOUT US													*/	
/* PAGE: partners.html												*/
/************************************************************************/
	$(function() {
 
$("#proactive_it").click(function(e){
	e.preventDefault();
        $('div').filter(':visible').slideUp();
          $("#proactive_it_details").slideDown(600);
    });
    
    $("#preventative_it").click(function(e){
    	e.preventDefault();
      $('div').filter(':visible').slideUp(); 
        $("#preventative_it_details").slideToggle(600);
    });
    
    $("#responsive_it").click(function(e){
    	e.preventDefault();
      $('div').filter(':visible').slideUp(); 
        $("#responsive_it_details").slideToggle(600);
    });

    $('a.close').click(function(e){
		e.preventDefault();
    $('div').slideUp();
    return false;
    
});

});