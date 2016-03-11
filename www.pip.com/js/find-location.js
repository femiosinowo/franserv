//this map is shown on National ulitlity nav "Find my location" click

var locationsArray = [],
infoBoxContentArray = [],
markerArray = [],
markerImg,
markerImgActive;

$(document).ready(function () {

    function initialize() {
        var mapProp = {
            center: new google.maps.LatLng(21.699825, 80.664063),
            zoom: 5,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("find_location_map"), mapProp);
    }

    function findLocationInit(loc) {

        var hasLocationMap = $('#find_location_map').length;
        if (hasLocationMap != 0) {

            $('#find_location_map_ajaxImg').show();
            var myLatLng, mapZoom = 4;
            var panMapX, panMapY = -50;
            if (loc == 'AF') {

                myLatLng = new google.maps.LatLng(3.575607, 20.414192); // map center
                mapZoom = 3;
                panMapX = -200;
                panMapY = 20;
                locationsArray = GetLocationsData(loc);
            }


            if (loc == 'AS') {

                myLatLng = new google.maps.LatLng(21.699825, 80.664063); // map center
                mapZoom = 5;
                panMapX = -300;
                locationsArray = GetLocationsData(loc);
            }

            if (loc == 'EU') {

                myLatLng = new google.maps.LatLng(48.110850, 11.409180); // map center
                mapZoom = 5;
                panMapX = -300;
                locationsArray = GetLocationsData(loc);
            }

            if (loc == 'SA') {

                myLatLng = new google.maps.LatLng(-20.957432, -54.083632); // map center
                panMapX = -300;
                locationsArray = GetLocationsData(loc);
            }

            if (loc == 'NA') {
                myLatLng = new google.maps.LatLng(39.625702, -98.125937); // map center 
                panMapX = -150;
                locationsArray = GetLocationsData(loc);
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
                zoom: mapZoom,
                mapTypeId: 'Styled',
                disableDefaultUI: true,
                scrollwheel: false,
                draggable: isDraggable,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.DEFAULT,
                    position: google.maps.ControlPosition.RIGHT_BOTTOM
                },
            };

            var styles = [
                //{
                //    "featureType": "water",
                //    "elementType": "all",
                //    "stylers": [
                //        {
                //            "color": "#651b64"
                //        },
                //        {
                //            "visibility": "on"
                //        }
                //    ]
                //},
                //{
                //    "featureType": "road",
                //    "stylers": [
                //        {
                //            "visibility": "off"
                //        }
                //    ]
                //},
                //{
                //    "featureType": "transit",
                //    "stylers": [
                //        {
                //            "visibility": "off"
                //        }
                //    ]
                //},
                //{
                //    "featureType": "administrative",
                //    "stylers": [
                //        {
                //            "visibility": "off"
                //        }
                //    ]
                //},
                //{
                //    "featureType": "landscape",
                //    "elementType": "all",
                //    "stylers": [
                //        {
                //            "color": "#420a48"
                //        }
                //    ]
                //},
                //{
                //    "featureType": "poi",
                //    "stylers": [
                //        {
                //            "color": "#420a48"
                //        }
                //    ]
                //},
                //{
                //    "elementType": "labels",
                //    "stylers": [
                //        {
                //            "visibility": "off"
                //        }
                //    ]
                //}
            ];


            var map = new google.maps.Map(document.getElementById("find_location_map"), mapOptions);
            map.panBy(panMapX, panMapY);
            var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });

            map.mapTypes.set('Styled', styledMapType);
            var marker, infoBoxContent;

            /****** Set custom orange marker *****/
            markerImg = '/images/location-map-marker-orange.png';
            markerImgActive = '/location-map-marker-orange.png';

            /***** INFOBOX 
                The js/infobox.js file is required for the custom infoBox to work.
            *****/
            var infoBoxOptions = {
                boxClass: 'find_location_infobox',
                pixelOffset: new google.maps.Size(20, -75), // float infoBox left
                //closeBoxURL: 'images/close_x_white_purple_bg.png'
            };

            var infoBox = new InfoBox(infoBoxOptions);

            //Loop through test locations array to place markers and build infoBox content
            if (locationsArray) {
                for (var i = 0; i < locationsArray.length; i++) {
                    var data = locationsArray[i];
                    var pos = new google.maps.LatLng(data.Latitude, data.Longitude);
                    marker = new google.maps.Marker({
                        position: pos,
                        map: map,
                        icon: markerImg
                    });

                    markerArray.push(marker);
                    marker.setMap(map);
                    var fullUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');

                    infoBoxContent = '<div class="infobox_content">';
                    infoBoxContent += '<h3>' + data.CenterName + '</h3>';
                    infoBoxContent += '<p>' + data.Address1 + '<br>';
                    infoBoxContent += data.City + ', ' + data.State + ' ' + data.Zipcode + '<br />' + data.PhoneNumber + '</p>';
                    infoBoxContent += '<p><a href="' + fullUrl + '/' + data.FransId + '/"><img src="/images/locations/btn_choose_location.png" alt="Choose Location"/></a>';
                    infoBoxContent += '</div>';

                    infoBoxContentArray.push(infoBoxContent); //add infobox content to array to be called later in the marker click function
                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            resetMarkers();
                            infoBox.setContent(infoBoxContentArray[i]);
                            infoBox.open(map, marker);
                            marker.setIcon(markerImgActive);
                        }
                    })(marker, i));

                }
            }// end loop

            //reset custom markers when infoBox is closed
            google.maps.event.addListener(infoBox, 'closeclick', resetMarkers);
            $('#find_location_map_ajaxImg').hide();
        }

    } // end findLocationInit function

    //allow map to resize with browser window. 
    google.maps.event.addDomListener(window, 'resize', findLocationInit);

    //var country_id = 'NA'; //default country
    var continentCode = 'NA'; //default continent

    //Change markers  from active to default
    function resetMarkers() {
        for (var m = 0; m < markerArray.length; m++) {
            markerArray[m].setIcon(markerImg);
        }
    }

    function loadMap() {
        setTimeout(function () {
            if ($('#find_location_map').length > 0) {
                findLocationInit(continentCode);
            }
        }, 1500);

        $('#find_location_map').fadeIn();
    }

    //Display default map when Find Location button is clicked
    $('#find_location_link').click(loadMap);

    //Change map location when map menu links are clicked
//    $('a.fl-menu-link').click(function (e) {
//        e.preventDefault();
//        //must clear infobox content when selecting another country
//        infoBoxContentArray = [];
//        $(this).parent().addClass('active').siblings().removeClass('active');
//        var country_id = $(this).attr('id');
//        if (country_id) {
//            continentCode = getContinentCode(country_id);
//            loadMap(continentCode);
//        }
//    });

    //home page world maps click action
    $('#worldMap a').click(function () {
        var region_id = $(this).attr('id');
        $('html,body').animate({ scrollTop: 0 }, 'slow', function () {
            if (region_id) {
                continentCode = getContinentCode(region_id);
                $('.utility_nav_wrapper').addClass('open');
                $('.close_utility_btn').show();
                $('.utility_nav_left li').removeClass('active');
                $('#search_social_close_wrapper').delay(1500).addClass('find_location');
                $('#find_location').show();
                loadMap(continentCode);
                MakeContinentTabActive(continentCode);
            }
        });
    });


    //footer find location click action
    $('#footerFindLocation').click(function () {
        $('html,body').animate({ scrollTop: 0 }, 'slow', function () {
            var continentCode = 'NA'; //default
            $('.utility_nav_wrapper').addClass('open');
            $('.close_utility_btn').show();
            $('.utility_nav_left li').removeClass('active');
            $('#search_social_close_wrapper').delay(1500).addClass('find_location');
            $('#find_location').show();
            loadMap(continentCode);
            MakeContinentTabActive(continentCode);
        });
    });


    function GetLocationsData(cCode) {
        var data = '';
        $.ajax({
            type: "POST",
            url: "/Handlers/GetCenterLocations.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{'continentCode':'" + cCode + "'}",
            async: false,
            cache: false,
            success: function (response) {
                //console.log(response);
                data = response;
            },
            error: function (response, status, error) {
                //console.log(error);
            }
        });
        return data;
    }

    function getContinentCode(country_id) {
        var code;
        if (country_id == 'fl-north-america' || country_id == 'north-america') {
            code = 'NA';
        }
        else if (country_id == 'fl-south-america' || country_id == 'south-america') {
            code = 'SA';
        }
        else if (country_id == 'fl-europe' || country_id == 'europe') {
            code = 'EU';
        }
        else if (country_id == 'fl-africa' || country_id == 'africa') {
            code = 'AF';
        }
        else if (country_id == 'fl-asia' || country_id == 'asia') {
            code = 'AS';
        }
        else {
            code = 'NA';
        }
        return code;
    }


    function MakeContinentTabActive(code) {
        if (code == 'NA') {
            $('#fl-north-america').parent().addClass('active').siblings().removeClass('active');
        }
        else if (code == 'SA') {
            $('#fl-south-america').parent().addClass('active').siblings().removeClass('active');
        }
        else if (code == 'EU') {
            $('#fl-europe').parent().addClass('active').siblings().removeClass('active');
        }
        else if (code == 'AF') {
            $('#fl-africa').parent().addClass('active').siblings().removeClass('active');
        }
        else if (code == 'AS') {
            $('#fl-asia').parent().addClass('active').siblings().removeClass('active');
        }
        else {
            $('#fl-north-america').parent().addClass('active').siblings().removeClass('active');
        }
    }


});