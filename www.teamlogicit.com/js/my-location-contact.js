function initialize() {
	var myLatLng;
	var loc = $('#location_map_contact').attr('class');
	// Map coordinates for the National and local maps. 
	myLatLng = new google.maps.LatLng(37.276494, -121.932453);
	//disable map dragging on mobile devices
	var isDraggable;
	if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
		alert('yes');
		isDraggable = false;
	} else {
		alert('no');
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
	var styles = [{
		"featureType": "road",
		"elementType": "geometry.stroke",
		"stylers": [{
			"visibility": "on"
		}, {
			"color": "#cfcfcf"
		}]
	}, {
		"featureType": "poi",
		"stylers": [{
			"visibility": "off"
		}]
	}, {
		"featureType": "transit",
		"stylers": [{
			"visibility": "off"
		}]
	}, {
		"featureType": "landscape.natural",
		"elementType": "geometry.fill",
		"stylers": [{
			"visibility": "on"
		}, {
			"color": "#e1f0dd"
		}]
	}, {
		"featureType": "landscape.man_made",
		"elementType": "geometry.fill",
		"stylers": [{
			"visibility": "on"
		}, {
			"color": "#eeefef"
		}]
	}];
	
	var map = new google.maps.Map(document.getElementById("location_map_contact"), mapOptions); /*map.panBy(-300, -50);*/
	//move center point of the map to the right
	var styledMapType = new google.maps.StyledMapType(styles, {
		name: 'Styled'
	});
	map.mapTypes.set('Styled', styledMapType); /****** Set custom purple marker *****/
	var markerImg = '../images/black-location-map-marker.png';
	var marker = new google.maps.Marker({
		position: myLatLng,
		map: map,
		icon: markerImg
	});
	marker.setMap(map);
}

function loadScript() {
	var script = document.createElement('script');
	script.type = 'text/javascript';
	script.src = 'https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=true&' + 'callback=initialize';
	document.body.appendChild(script);
}
window.onload = loadScript;