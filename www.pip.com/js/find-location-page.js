/*this js code is used on find the location page*/

var locationsArray = [],
infoBoxContentArray = [],
markerArray = [],
markerImg,
markerImgActive;
var fullUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');

$(document).ready(function () {

    //var country_id = 'NA'; //default country
    var continent_Code = 'NA'; //default continent
    $('#find_location_page_submit').click(function (e) {
        e.preventDefault();       
        //enter_location
        $('#searchLocWaitImg').show();
        var validation = fnDoValidationForLocation();
        if (validation) {
            var address = $('.findLocationCity').val();
            var distance = $('.findLocationSearchForm .transformSelectDropdown .selected span').text();
            var response = fnGetLocationsData(address, distance, false);
            fnOnSuccess(response);
            LoadLocationMapData(false, response);
        }
        else {
            $('#searchLocWaitImg').hide();
        }
    });   

    function fnDoValidationForLocation() {
        var status = false;
        if ($('.findLocationCity').val() != '') {
            status = true; $('.findLocationCity').removeClass('requiredField');
        }
        else {
            $('.findLocationCity').addClass('requiredField');
            status = false;
        }

        var distanceVal = $('.findLocationSearchForm .transformSelectDropdown .selected span').text();
        if (distanceVal != undefined && distanceVal != '- Choose Distance -') {
            status = true; $('.findLocationSearchForm .transformSelect li:first').removeClass('requiredField');
        }
        else {
            $('.findLocationSearchForm .transformSelect li:first').addClass('requiredField');
            status = false;
        }

        return status;
    }

    function fnGetLocationsData(address, distance, isInitalLoad) {
        var data = '';
        $.ajax({
            type: "POST",
            url: "/Handlers/GetLocationsByAddress.ashx",
            data: '{address: "' + address + '",distance:"' + distance + '",allUsLocations:"' + isInitalLoad + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
    
    function fnOnSuccess(response) {      
        $('#searchLocWaitImg').hide();
        if (response != "" && response != "[]") {
            var json = response;
            $("#map-locations-no-results").hide();
            $('#find_location_results').show();
            $("#map-locations-results").show();
            $("#map-locations-results").html('');
            for (i = 0; i < json.length; i++) {
                var html;
                if (i < json.length - 1)
                    html = "<p><a href=\"" + fullUrl + '/' + json[i].FransId + "/\"><span><strong>" + json[i].CenterName + "</strong><br/>" + json[i].Address1 + "<br/>" + FormatAddress(json[i].Address2) + json[i].City + ", " + json[i].State + " " + json[i].Zipcode + ". " + json[i].Country + "<br/>" + json[i].Miles + " Miles</span><span class='markerImage'><span class='markerText'>" + (i + 1) + "</span></span></a></p>";
                else
                    html = "<p class=\'noBorder\'><a href=\"" + fullUrl + '/' + json[i].FransId + "/\"><span><strong>" + json[i].CenterName + "</strong><br/>" + json[i].Address1 + "<br/>" + FormatAddress(json[i].Address2) + json[i].City + ", " + json[i].State + " " + json[i].Zipcode + ". " + json[i].Country + "<br/>" + json[i].Miles + " Miles</span><span class='markerImage'><span class='markerText'>" + (i + 1) + "</span></span></a></p>";
                $("#map-locations-results").append(html);
            }
        }
        else {
            $('#find_location_results').show();
            $("#map-locations-results").hide();
            $("#map-locations-no-results").show();
        }
    }

    function FormatAddress(address2) {
        var formattedAddress = '';
        if (address2 != null && address2 != undefined && address2 != '')
            formattedAddress = address2 + '<br/>';
        return formattedAddress;
    }

    function LoadLocationMapData(initialLoad, locationsData) {       
        var myLatLngData, mapZoomData = 4;
        var panMapXData, panMapYData = -50;
        //default US map
        if (initialLoad) {
            myLatLngData = new google.maps.LatLng(39.625702, -98.125937); // map center 
            panMapXData = -150;
            locationsArray = locationsData;
        }
        else
        {
            myLatLngData = new google.maps.LatLng(39.625702, -98.125937); // map center 
            panMapXData = -150;
            locationsArray = locationsData;
        }

        //disable map dragging on mobile devices
        var isdraggable;

        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
            isdraggable = false;
        }
        else {
            isdraggable = true;
        }

        var mapOptionsData = {
            center: myLatLngData,
            zoom: mapZoomData,
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            disableDefaultUI: true,
            scrollwheel: false,
            draggable: isdraggable,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.DEFAULT,
                position: google.maps.ControlPosition.RIGHT_BOTTOM
            }
        };        

        var mapData = new google.maps.Map(document.getElementById("location_map_page"), mapOptionsData);

        mapData.panBy(panMapXData, panMapYData);
        //var styledMapType = new google.maps.StyledMapType(styles, { name: 'Styled' });

        //mapData.mapTypes.set('Styled', styledMapType);
        var markerData, infoBoxContentData;

        /****** Set custom orange marker *****/
        markerImg = '/images/location-map-marker-orange.png';
        markerImgActive = '/images/location-map-marker-orange.png';

        /***** INFOBOX 
			The js/infobox.js file is required for the custom infoBox to work.
		*****/
        var infoBoxOptions = {
            boxClass: 'find_location_page_infobox',
            pixelOffset: new google.maps.Size(20, -75), // float infoBox left
            closeBoxURL: '/images/close_x_white_orange_bg.png'
        };

        var infoBoxData = new InfoBox(infoBoxOptions);

        //Loop through test locations array to place markers and build infoBox content
        if (locationsArray) {
            for (var i = 0; i < locationsArray.length; i++) {
                var data = locationsArray[i];
                var pos = new google.maps.LatLng(data.Latitude, data.Longitude);
                markerData = new google.maps.Marker({
                    position: pos,
                    map: mapData,
                    icon: markerImg //'https://chart.googleapis.com/chart?chst=d_map_pin_letter&chld=' + '|7D247E|000000',
                });

                markerArray.push(markerData);
                markerData.setMap(mapData);
                
                infoBoxContentData = '<ul class="contact_info">';
                infoBoxContentData += '<li class="contact-icon-location bottom-divider"><span>' + data.Address1 + '<br/>' + data.City + ' ' + data.State + ' ' + data.Zipcode + '</span></li>';
                infoBoxContentData += '<li class="contact-icon-phone"><span>' + data.PhoneNumber + '</span></li>';
                infoBoxContentData += '<li class="contact-icon-fax"><span>' + data.FaxNumber + '</span></li>';
                infoBoxContentData += '<li class="contact-icon-email bottom-divider"><span><a href="mailto:' + data.Email + '">' + data.Email + '</a></span></li>';
                infoBoxContentData += '</ul>';
                infoBoxContentData += '<p class="contact_info_location"><a href="' + fullUrl + '/' + data.FransId + '/"><img src="/images/btn_choose_location.png" alt="Choose Location" /></a></p>';
               
                infoBoxContentArray.push(infoBoxContentData); //add infobox content to array to be called later in the marker click function
                google.maps.event.addListener(markerData, 'click', (function (markerData, i) {
                    return function () {
                        //ResetMarkers();
                        infoBoxData.setContent(infoBoxContentArray[i]);
                        infoBoxData.open(mapData, markerData);
                        markerData.setIcon(markerImgActive);
                    }
                })(markerData, i));

            }
        }// end loop

        //reset custom markers when infoBox is closed
        google.maps.event.addListener(infoBoxData, 'closeclick', ResetMarkers);
    } 

    //allow map to resize with browser window. 
    google.maps.event.addDomListener(window, 'resize', LoadLocationMapData);
    
    //Change markers  from active to default
    function ResetMarkers() {
        //for (var m = 0; m < markerArray.length; m++) {
        //    markerArray[m].setIcon(markerImg);
        //}
    }

    //initial load all locations for US map
    var loadAllLocations = $('.loadAllLocations').val();
    if (loadAllLocations == 'true') {
        var response = fnGetLocationsData('US', '0', true);
        LoadLocationMapData(true, response);
    } else {
        $('#find_location_page_submit').click();
    }
});