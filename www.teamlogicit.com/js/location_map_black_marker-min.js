$(document).ready(function(){var e=$("#location_map_black").length;if(e!=0){var t=$(window).innerWidth();function n(){var e,t=$("#location_map_black").attr("class");e=new google.maps.LatLng(37.276494,-121.932453);var n;/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)?n=!1:n=!0;var r={center:e,zoom:16,mapTypeId:"Styled",disableDefaultUI:!0,scrollwheel:!1,draggable:n},i=[{stylers:[{hue:"#e1f0de"}]},{featureType:"road",elementType:"labels",stylers:[{visibility:"on"}]},{featureType:"road",elementType:"geometry",stylers:[{lightness:0},{saturation:-55},{visibility:"on"}]},{featureType:"landscape.man_made",stylers:[{color:"#eeefef"}]},{featureType:"landscape.natural.landcover",stylers:[{color:"#e1f0dd"}]},{featureType:"water",elementType:"labels",stylers:[{visibility:"on"}]},{featureType:"water",stylers:[{lightness:0},{saturation:-70},{visibility:"on"}]}],s=new google.maps.Map(document.getElementById("location_map_black"),r),o=new google.maps.StyledMapType(i,{name:"Styled"});s.mapTypes.set("Styled",o);var u="../images/black-location-map-marker.png",a=new google.maps.Marker({position:e,map:s,icon:u});a.setMap(s)}google.maps.event.addDomListener(window,"load",n)}});