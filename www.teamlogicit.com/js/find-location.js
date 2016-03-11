$(document).ready(function(){
	
	
	var hasMap = $('#find_location_map').length;
	
if (hasMap != 0) {
	
	
    function initialize() {
    	
    	
       var myLatLng = new google.maps.LatLng(39.625702, -98.125937); 
    	
    	
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
    	  zoom: 5,
    	  mapTypeId: 'Styled',
    	  disableDefaultUI: true,
    	  scrollwheel: true,
		  draggable: isDraggable 
    	};
    	
    	var styles = [
			{
				featureType: 'water',
				elementType: 'all',
				stylers: [
					{ hue: '#000000' },
					{ saturation: -100 },
					{ lightness: -100 },
					{ visibility: 'on' }
				]
			},{
				featureType: 'landscape.man_made',
				elementType: 'all',
				stylers: [
					{ hue: '#fdf0ea' },
					{ saturation: 76 },
					{ lightness: 59 },
					{ visibility: 'on' }
				]
			},{
				featureType: 'road',
				elementType: 'all',
				stylers: [
					{ hue: '#000000' },
					{ saturation: -100 },
					{ lightness: -100 },
					{ visibility: 'off' }
				]
			}
		];

    	var map = new google.maps.Map(document.getElementById("find_location_map"), mapOptions);
    	
    	var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });
    	
		map.panBy(0, -125);
		map.mapTypes.set('Styled', styledMapType);	
		
		
    	/****** Set custom purple marker *****/
    	var markerImg = '../images/location_nav_icon.png';
    	
    	var marker = new google.maps.Marker({
    		position: myLatLng,
    		map: map,
    		icon: false
    	});
    
    	marker.setMap(map);
    	
    	
    						  
    }
    
	//google.maps.event.addDomListener(window, 'resize', initialize); //allow map to resize with browser window. Note: Infobox will disappear. Refresh at new browser size to see infoBox.
	google.maps.event.addDomListener(window, 'load', initialize);

} // end if

});