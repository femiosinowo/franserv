<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Locator.master" AutoEventWireup="true"
    CodeFile="find_locator.aspx.cs" Inherits="find_locator" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" runat="Server">
    <script type="text/javascript" src="/js/find-location-page.js"></script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeaderContent" runat="Server">
    <div class="find_location_form_wrapper clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="find_location_form">
                    <h2 class="headline white">find a location</h2>
                    <div class="grid_15 prefix_1 alpha">
                        <div class="form findLocationSearchForm">
                            <ul>
                                <li>
                                    <input type="text" placeholder="City, State, Zip*" runat="server" class="findLocationCity" id="FindLocationCity" /></li>
                                <li>
                                   <select class="custom-select fnd_location_distance" runat="server" id="find_location_distance">
                                    <option selected="selected" value="- Choose Distance -">- Choose Distance -</option>
                                    <option value="5">5</option>
                                    <option value="10">10</option>
                                    <option value="25">25</option>
                                    <option value="50">50</option>
                                    <option value="100">100</option>
                                </select> 
                                    <input type="hidden" runat="server" id="hddnDistance" class="hddnDistance" value="" />
                                </li>
                                <li class="location_submit">
                                    <div class="square_button" id="find_location_submit">
                                        <a id="find_location_page_submit" class="search" href="javascript:void('0')">Search</a>
                                    </div>                                     
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="grid_6 prefix_2 omega allLocationBtn">
                        <span id="searchLocWaitImg" style="display: none;" class="grid_3 suffix_1 ">
                            <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                        </span>
                        <div class="square_button" id="all_locations_button"><a href="#all_locations">See All Locations</a></div>
                    </div>
                </div>
                <!-- .find_location_form -->
            </div>
            <!-- grid_24 -->
        </div>
        <!-- container_24 -->
    </div>
    <!-- .find_location_form_wrapper -->
    <div id="find_location_results" style="display: none">
                <h3>Pick a Location</h3>
                <div id="rq_pick_location_list" class="form">
                    <!--The html for the div will be binded using the javascript -->
                    <div class="custom_form_scroll" id="map-locations-results"></div>
                    <div class="custom_form_scroll" style="display: none" id="map-locations-no-results">
                        <p><span>No locations found for the provided address.</span></p>
                    </div>
                </div>
            </div>
    <div class="find_location_map_wrapper clearfix">         
        <div id="find_location_map">
            <!-- Google map container -->
        </div>
        <!-- #find_location_map -->
    </div>
    <!-- map_wrapper -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="all_locations" class="location_listing_wrapper clearfix">
        <div class="location_subheader_wrapper clearfix">
            <div class="location_subheader">
                <div class="container_24">
                    <div class="grid_14 alpha">
                        <h2>TeamLogic IT Locations</h2>
                    </div>
                    <!-- grid -->
                    <!-- hidding the drop down as per client request -->
                    <div style="display:none;" class="grid_8 prefix_2 omega">
                        <%--<a class="square_button" href="#">#### Locations</a>--%>
                        <asp:DropDownList ID="ddlCountryList" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCountryList_SelectedIndexChanged" class="custom-select">
                        </asp:DropDownList>
                    </div>
                    <!-- grid -->
                </div>
                <!-- container -->
            </div>
            <!-- location_subheader -->
        </div>
        <!-- location_subheader_wrapper -->
        <div class="clear"></div>
        <div class="location_listing_content">
            <div class="container_24">
                <div class="grid_24">
                    <div id="location_columns" class="columnizer">
                         <asp:Literal ID="ltrStateData" runat="server"></asp:Literal>                        
                    </div>
                    <!-- end locations_columns -->
                </div>
            </div>
            <!-- containter_24 -->
        </div>
        <!-- location-listing-content -->
    </div>
    <!-- location-listing-wrapper -->
    <input type="hidden" id="loadAllLocations" class="loadAllLocations" runat="server" value="true" />
    <div class="clear"></div>
</asp:Content>
