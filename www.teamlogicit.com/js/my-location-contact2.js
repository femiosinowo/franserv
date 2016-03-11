$(document).ready(function () {

    var hasMap = $('#location_map_contact').length;
	
    if (hasMap != 0) {
        
        function init() {

            var lat = $('.hiddenCenterLat').val();
            var long = $('.hiddenCenterLong').val();

            var myLatlng = new google.maps.LatLng(lat, long);

            var mapOptions = {
                zoom: 18,
                center: myLatlng,
                scrollwheel: false,
                styles: [
      {
          "featureType": "road",
          "elementType": "geometry.stroke",
          "stylers": [
            { "visibility": "on" },
            { "color": "#cfcfcf" }
          ]
      }, {
          "featureType": "poi",
          "stylers": [
            { "visibility": "off" }
          ]
      }, {
          "featureType": "transit",
          "stylers": [
            { "visibility": "off" }
          ]
      }, {
          "featureType": "landscape.natural",
          "elementType": "geometry.fill",
          "stylers": [
            { "visibility": "on" },
            { "color": "#e1f0dd" }
          ]
      }, {
          "featureType": "landscape.man_made",
          "elementType": "geometry.fill",
          "stylers": [
            { "visibility": "on" },
            { "color": "#eeefef" }
          ]
      }
                ]
            };


            var mapElement = document.getElementById('location_map_contact');
            var map = new google.maps.Map(mapElement, mapOptions);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                icon: '/images/black-location-map-marker.png'
            });

            google.maps.event.addListener(marker, 'click', function () {
                infowindow.open(map, marker);
            });

            var boxText = document.createElement("div");
            boxText.style.cssText = "height:160px;";
            boxText.innerHTML = $(".hiddenCenterAddress").val();
            var myOptions1 = {
                content: boxText,
                disableAutoPan: false,
                maxWidth: 0,
                pixelOffset: new google.maps.Size(15, -95),
                zIndex: null,
                boxStyle: {
                    background: "url('/images/contact_us_infowindow.png') no-repeat", width: "380px"
                },
                closeBoxMargin: "10px 0 2px 0",
                closeBoxURL: "",
                infoBoxClearance: new google.maps.Size(1, 1),
                isHidden: false,
                pane: "floatPane",
                enableEventPropagation: false
            };
            var ib = new InfoBox(myOptions1);
            ib.open(map, marker);
            google.maps.event.addListener(marker, "click", function () {
                ib.open(map, marker);
            });

        }

        google.maps.event.addDomListener(window, 'load', init);
    }
});