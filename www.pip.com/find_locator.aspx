<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="find_locator.aspx.cs" Inherits="find_locator" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" runat="Server">
    <script type="text/javascript" src="/js/find-location-page.js"></script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div>
        <div class="clear"></div>
        <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
        <!-- <div class="subpage_tagline_wrapper find_location_page find_location_usa clearfix">
            <div class="subpage_tagline">
                <div class="container_24">
                    <div class="grid_24">
                        <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                    </div>
                </div>
            </div>
        </div> -->
        <!-- end subpage_tagline_wrapper -->
        <div class="clear"></div>
        <div class="find_location_search_wrapper clearfix">
            <div class="find_location_search_wrapper clearfix">
                <div class="find_location_search clearfix">
                    <div class="container_24">
                        <div class="grid_24">
                            <div class="grid_8 prefix_1">
                                <span class="headline-block-icon-black"></span>
                                <h2 class="headline"><span>Center Locator</span></h2>
                            </div>
                            <!--grid 8-->
                            <div class="grid_15">
                                <div id="find_location_search_form" class="form findLocationSearchForm">
                                    <input type="text" placeholder="City, State, Zip" runat="server" class="findLocationCity" id="findLocationCity" />
                                    <select class="custom-select fnd_location_distance" runat="server" id="find_location_distance">
                                        <option selected="selected" value="25">25</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                        <option value="25">200</option>
                                    </select>
                                    <input type="submit" id="find_location_page_submit" class="search black-btn search-location-btn" value="Search" />
                                    <div id="searchLocWaitImg" style="display: none;" class="grid_3 suffix_1 ">
                                        <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                                    </div>
                                    <a href="#all_locations" class="cta-button-text"><span>US Locations</span></a>
                                </div>
                                <!-- end form -->
                            </div>
                            <!-- grid_15-->
                        </div>
                        <!-- grid_24-->
                    </div>
                    <!-- container 24-->
                </div>
                <!--end find_location_search -->
            </div>
        </div>
        <div class="clear"></div>
        <!-- mmm Map Container mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Map Container mmm -->
        <div class="find_location_map_wrapper  clearfix">
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
            <div class="find_location_map clearfix">
                <div id="location_map_page_wrapper" class="clearfix">
                    <div id="location_map_page" style="height: 600px;"></div>
                </div>
            </div>
        </div>
        <!--end media_articles -->
        <!-- end media_articles_wrapper -->
        <div class="clear"></div>
        <!-- Locations List mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Locations List -->
        <div id="all_locations" class="locations_list_wrapper clearfix">
            <div class="locations_list_subheader_wrapper clearfix">
                <div class="locations_list_subheader clearfix">
                    <div class="container_24">
                        <div class="grid_24">
                            <div class="grid_16 alpha">
                                <h3>PIP Locations, United States</h3>
                            </div>
                            <!-- end grid -->
                            <div class="grid_8 omega">
                                <div class="form" id="choose_country">
                                    <asp:DropDownList ID="ddlCountryList" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlCountryList_SelectedIndexChanged" class="custom-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <!-- end location_list_subheader -->
                        </div>
                        <!-- end location_list_subheader_wrapper -->
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24  -->
                <div class="locations_list_content_wrapper clearfix">
                    <div class="locations_list_content clearfix">
                        <div class="container_24">
                            <div class="grid_24">
                                <div id="locations_columns" class="columnizer">
                                    <asp:Literal ID="ltrStateData" runat="server"></asp:Literal>
                                </div>
                                <!-- end locations_columns -->
                            </div>
                            <!--end grid 24-->
                        </div>
                        <!-- end container_24  -->
                    </div>
                    <!-- end locations_list_content  -->
                </div>
                <!-- end locations_list_content_wrapper  -->
            </div>
            <!-- end locations_list_wrapper -->
        </div>
        <input type="hidden" id="loadAllLocations" class="loadAllLocations" runat="server" value="true" />
        <div class="clear"></div>
    </div>
</asp:Content>
