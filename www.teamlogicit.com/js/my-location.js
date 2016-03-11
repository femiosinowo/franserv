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
    	
    	myLatLng = new google.maps.LatLng(lat, long);
    	
    	
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
  {
    "featureType": "road",
    "elementType": "geometry.stroke",
    "stylers": [
      { "visibility": "on" },
      { "color": "#cfcfcf" }
    ]
  },{
    "featureType": "poi",
    "stylers": [
      { "visibility": "off" }
    ]
  },{
    "featureType": "transit",
    "stylers": [
      { "visibility": "off" }
    ]
  },{
    "featureType": "landscape.natural",
    "elementType": "geometry.fill",
    "stylers": [
      { "visibility": "on" },
      { "color": "#e1f0dd" }
    ]
  },{
    "featureType": "landscape.man_made",
    "elementType": "geometry.fill",
    "stylers": [
      { "visibility": "on" },
      { "color": "#eeefef" }
    ]
  }
];



    
    	var map = new google.maps.Map(document.getElementById("location_map"),
    		mapOptions);
    	
    	/*map.panBy(-300, -50);*/ //move center point of the map to the right
    		
    	var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });
    	
		map.mapTypes.set('Styled', styledMapType);	
		
		
    	/****** Set custom purple marker *****/
    	var markerImg = '../images/location-map-marker.png';
    	
    	var marker = new google.maps.Marker({
    		position: myLatLng,
    		map: map,
    		icon: markerImg
    	});
    
    	marker.setMap(map);
    	
    	
    						  
    }
    
	//google.maps.event.addDomListener(window, 'resize', initialize); //allow map to resize with browser window. Note: Infobox will disappear. Refresh at new browser size to see infoBox.
	google.maps.event.addDomListener(window, 'load', initialize);

} // end if

});