$(document).ready(function () {   

        google.maps.event.addDomListener(window, 'load', init);

        var lat = $('.hiddenCenterLat').val();
        var long = $('.hiddenCenterLong').val();
        var myLatlng = new google.maps.LatLng(lat, long);
        function init() {
            var mapOptions = {
                zoom: 18,
                center: myLatlng,
                scrollwheel: false,
                styles: [
                    {
                        stylers: [
                            { "hue": "#e1f0de" }
                        ]
                    },
                    {
                        featureType: "road",
                        elementType: "labels",
                        stylers: [
                            { "visibility": "on" }
                        ]
                    },
                    {
                        featureType: "road",
                        elementType: "geometry",
                        stylers: [
                            { "lightness": 0 },
                            { "saturation": -55 },
                            { "visibility": "on" }
                        ]
                    },
                    {
                        featureType: "landscape.man_made",
                        stylers: [
                            { "color": "#eeefef" }
                        ]
                    },
                    {
                        featureType: "landscape.natural.landcover",
                        stylers: [
                            { "color": "#e1f0dd" }
                        ]
                    },
                    {
                        featureType: "water",
                        elementType: "labels",
                        stylers: [
                            { "visibility": "on" }
                        ]
                    },
                    {
                        featureType: "water",
                        stylers: [
                            { "lightness": 0 },
                            { "saturation": -70 },
                            { "visibility": "on" }
                        ]
                    }
                ]
            };


            var mapElement = document.getElementById('location_map_black');
            var map = new google.maps.Map(mapElement, mapOptions);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                icon: '/images/location-map-marker.png'
            });


        }
});