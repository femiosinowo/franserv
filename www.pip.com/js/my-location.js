//this map is shown on the local home page, aboutUs, why_we're_different
$(document).ready(function () {
    var hasMap = $('#location_map').length;
    if (hasMap != 0) {
        var w = $(window).innerWidth();
        function initialize() {
            var myLatLng;
            var loc = $('#location_map').attr('class');
            var lat = $('.hiddenCenterLat').val();
            var long = $('.hiddenCenterLong').val();

            // Map coordinates for the National and local maps. 
            if (loc == 'corporate') {
                //myLatLng = new google.maps.LatLng(33.565330, -117.664140);
                myLatLng = new google.maps.LatLng(33.565330, -117.664140);
            }
            else {
                //myLatLng = new google.maps.LatLng(33.565330, -117.664140);
                myLatLng = new google.maps.LatLng(lat, long);
            }

            //disable map dragging on mobile devices
            var isDraggable;
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
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
                    stylers: [{
                        saturation: -100
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

            var map = new google.maps.Map(document.getElementById("location_map"), mapOptions);
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
            
        }
        //google.maps.event.addDomListener(window, 'resize', initialize); //allow map to resize with browser window. Note: Infobox will disappear. Refresh at new browser size to see infoBox.
        google.maps.event.addDomListener(window, 'load', initialize);
    } // end if

});