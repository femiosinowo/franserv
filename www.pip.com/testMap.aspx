<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testMap.aspx.cs" Inherits="testMap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- locations Google map scripts -->
    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDGF1KG6WSbJVdZ9TN66U3EMNA9wYIalFc&sensor=true"></script>--%>
    <%--<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC9QHKUFfhowUUFzezFygBgEMui4iUJnvs&sensor=false"></script>--%>

    <%--<style>
      #find_location_map {
        width: 500px;
        height: 400px;
      }
    </style>--%>

    <!-- find location  -->
    <link href="/css/find_location.css" rel="stylesheet" type="text/css">

    <!-- javascript libs -->
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js" type="text/javascript"></script>
    <script src="/js/respond.min.js" type="text/javascript"></script>

    <script src="https://maps.googleapis.com/maps/api/js"></script>
    <!-- extend Google map infobox functions -->
  <script src="/js/infobox.js" type="text/javascript"></script>
    <%-- <script src="/js/my-location.js" type="text/javascript"></script>--%>
    <script src="/js/find-location.js" type="text/javascript"></script>
    <%--<script src="/js/find-location-page.js" type="text/javascript"></script>--%>
    <%--<script src="/js/SirSpeedy.js" type="text/javascript"></script>--%>

    <%--<script type="text/javascript">
        function initialize() {
            var map_canvas = document.getElementById('find_location_map');
            var map_options = {
                center: new google.maps.LatLng(44.5403, -78.5463),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            }
            var map = new google.maps.Map(map_canvas, map_options)
        }
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>--%>
	
    

    <%--<script type="text/javascript">
        $(document).ready(function () {
            // find location code starts here
            $('#my_location_search_btn').click(function (e) {
                e.preventDefault();
                //enter_location           
                var validation = DoValidationForLocation();
                if (validation) {
                    var address = $('.txtFindLocation').val();
                    console.log(address);
                    address = address.replace(' ', '+');
                    var distance = $('#find_location_form .transformSelectDropdown .selected span').text();
                    window.location.href = '/find_locator.aspx?location=' + address + '&distance=' + distance + '';
                }
            });

            function DoValidationForLocation() {
                var status = false;
                var locationStatus = false;
                var distanceStatus = false;

                if ($('.txtFindLocation').val() != '') {
                    locationStatus = true; $('.txtFindLocation').removeClass('requiredField');
                }
                else {
                    $('.txtFindLocation').addClass('requiredField');
                    locationStatus = false;
                }

                var distanceVal = $('#find_location_form .transformSelectDropdown .selected span').text();
                if (distanceVal != undefined && distanceVal != 'Distance') {
                    distanceStatus = true; $('#find_location_form .transformSelect li:first').removeClass('requiredField');
                }
                else {
                    $('#find_location_form .transformSelect li:first').addClass('requiredField');
                    distanceStatus = false;
                }

                if (locationStatus == false || distanceStatus == false)
                    status = false;
                else
                    status = true;

                return status;
            }

</script>--%>

<script>
     function initialize() {
         var mapProp = {
             center: new google.maps.LatLng(21.699825, 80.664063),
             zoom: 5,
             pan: -300,
             mapTypeId: google.maps.MapTypeId.ROADMAP
         };
         var map = new google.maps.Map(document.getElementById("find_location_map"), mapProp);
         console.log(map);
     }

    google.maps.event.addDomListener(window, 'load', initialize);
</script>

</head>
<body>
    <%--<form id="form1" runat="server">--%>
     <!-- mmmmmmmmmmmm FIND LOCATION (NATIONAL) mmmmmmmmmmmmmm -->
        <div class="utility_content national clearfix" id="find_location">
            <div id="find_location_map_wrapper" class="clearfix">
               <div id="find_location_map_ajaxImg">
                    <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                </div>
                <div id="find_location_map">
                </div>
            </div>
            <a class="find-location-link" id="find_location_link" href="#">Find a
                            <span>Location</span></a>
            <!-- end find_location_map_wrapper -->
            <%--<div id="find_location_searchbox_wrapper">
                <div class="container_24">
                    <div class="grid_8">
                        <div id="find_location_searchbox" class="request_quote clearfix">
                            <h2>
                                Find a Location</h2>
                            <div class="prefix_2 grid_20 suffix_2">
                                <div class="form" id="find_location_form">
                                    <asp:TextBox ID="txtFindLocation" CssClass="txtFindLocation" runat="server" placeholder="Enter City, State, Zip"
                                        required="required"></asp:TextBox>
                                    <!-- <span class="required">*</span> -->
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFindLocation"
                                        CssClass="required"></asp:RequiredFieldValidator>
                                    <select class="custom-select" id="distance">
                                        <option value="">Distance</option>
                                        <option value="5">5</option>
                                        <option value="10">10</option>
                                        <option value="25">25</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                        <option value="200">200</option>
                                    </select>
                                    <!-- <span class="required">*</span> -->
                                    <asp:TextBox ID="txtFindLocationChoose" CssClass="txtFindLocationChoose" runat="server"
                                        Style="display: none;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtFindLocationChoose"
                                        CssClass="required" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="clear">
                                    </div>
                                    <input type="submit" id="my_location_search_btn" value="Find Now" />
                                </div>
                                <div class="clear">
                                </div>
                                <!-- end rq_pick_location -->
                            </div>
                            <!-- end grid -->
                        </div>
                        <!-- end find_location_searchbox -->
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24 -->
            </div>
            <!-- end find_location_searchbox_wrapper -->
            <div class="clear">
            </div>--%>
            <%--<div id="find_location_menu" class="clearfix">
                <div class="container_24">
                    <div class="grid_24">
                        <ul>
                            <li class="active"><a href="#" id="fl-north-america" class="fl-menu-link">
                                <div class="find-location-figure">
                                    <div>
                                    </div>
                                </div>
                                <div class="find-location-name">
                                    <span>North America</span></div>
                            </a></li>
                            <li><a href="#" id="fl-south-america" class="fl-menu-link">
                                <div class="find-location-figure">
                                    <div>
                                    </div>
                                </div>
                                <div class="find-location-name">
                                    <span>South America</span></div>
                            </a></li>
                            <li><a href="#" id="fl-europe" class="fl-menu-link">
                                <div class="find-location-figure">
                                    <div>
                                    </div>
                                </div>
                                <div class="find-location-name">
                                    <span>Europe</span></div>
                            </a></li>
                            <li><a href="#" id="fl-africa" class="fl-menu-link">
                                <div class="find-location-figure">
                                    <div>
                                    </div>
                                </div>
                                <div class="find-location-name">
                                    <span>Africa &amp; Middle East</span></div>
                            </a></li>
                            <li><a href="#" id="fl-asia" class="fl-menu-link">
                                <div class="find-location-figure">
                                    <div>
                                    </div>
                                </div>
                                <div class="find-location-name">
                                    <span>Asia</span></div>
                            </a></li>
                        </ul>
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24 -->
            </div>
            <!-- end find_location_menu_wrapper -->--%>
        <%--</div>--%>
        <!-- end find_location -->
    <%--</form>--%>
</body>
</html>
