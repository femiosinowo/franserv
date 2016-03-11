$(document).ready(function(){
	
	
	var hasMap = $('#location_map').length;
	
if (hasMap != 0) {
	
	var w = $(window).innerWidth();
	
    function initialize() {
    	
    	var myLatLng;
    	
    	var loc = $('#location_map').attr('class');
    	var lat = $('.hiddenCenterLat').val();
    	var long = $('.hiddenCenterLong').val();
    	
		// Map coordinates for the National and local maps. 
    	if (loc=='corporate') {
    	    myLatLng = new google.maps.LatLng(lat, long);
    	}	
    	else {    	
    	    myLatLng = new google.maps.LatLng(lat, long);
    	}
    	
		//disable map dragging on mobile devices
		var isDraggable;
		
		if(/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
			isDraggable = false;
		}
		else {
			isDraggable = true;
		}
		
    	var mapOptions = {
    	  center: myLatLng,
    	  zoom: 16,
    	  mapTypeId: 'Styled',
    	  disableDefaultUI: true,
    	  scrollwheel: false,
		  draggable: isDraggable 
    	};
    	
    	var styles = [
			{ stylers: [{ 
				saturation:-100 
			},
			{
				  featureType: "road",
				  elementType: "geometry",
				  stylers: [
					{ hue: -45 },
					{ saturation: 100 }
				  ]
			}
		]
	}];
    
    	var map = new google.maps.Map(document.getElementById("location_map"),
    		mapOptions);
    	
    	/*map.panBy(-300, -50);*/ //move center point of the map to the right
    		
    	var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });
    	
		map.mapTypes.set('Styled', styledMapType);	
		
		
    	/****** Set custom purple marker *****/
    	var markerImg = '/images/location-map-marker.png';
    	
    	var marker = new google.maps.Marker({
    		position: myLatLng,
    		map: map,
    		icon: markerImg
    	});
    
    	marker.setMap(map);
    	
    	/***** INFOBOX 
			The js/infobox.js file is required for the custom infoBox to work.
		*****/

		var i_width = -825, i_height= -275; //infoBox offset from point
		var pan_x = -300, pan_y = -50; //center point default coordinates
		
		
		//Position center point based on browser width
		if(w < 735) {
			pan_x = -200;
		}
		if(w < 555) {
			pan_x = 0;
			pan_y=0;
		}
		
		map.panBy(pan_x, pan_y); 
		
		//Set infoBox position based on browser width
		if (w < 1067){
			i_width= -(w/1.65);
		}
		if (w < 700){
			
			i_width= -(w/1.55);
		}
		if (w < 600){
			
			i_width= -(w/1.35);
		}
		
		if (w > 560){
			
			var infoBoxContent = document.getElementById("location_info_wrapper");
    	
    		//infoBoxContent.style.display='block';
		
			var infoBoxOptions = {
				 content: infoBoxContent,
				 disableAutoPan: true,
				 position: myLatLng,
				 pixelOffset: new google.maps.Size(i_width, i_height), // float infoBox left
				 maxWidth: 0,
				 isHidden: false,
				 closeBoxURL: '/images/infobox-close.png'
			};
			
			var infoBox = new InfoBox(infoBoxOptions);
				
			infoBox.open(map); //display map on load				
			
			/* click on map top open infoBox*/
			google.maps.event.addListener(marker, 'click', function() {
			  infoBox.open(map);
			});
		
		}
    						  
    }
    
	//google.maps.event.addDomListener(window, 'resize', initialize); //allow map to resize with browser window. Note: Infobox will disappear. Refresh at new browser size to see infoBox.
	google.maps.event.addDomListener(window, 'load', initialize);

} // end if

});