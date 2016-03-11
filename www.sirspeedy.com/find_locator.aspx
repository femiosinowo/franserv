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
        <div class="subpage_tagline_wrapper find_location_page find_location_usa clearfix">
            <div class="subpage_tagline">
                <div class="container_24">
                    <div class="grid_24">
                        <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                    </div>
                    <!--end grid_24 -->
                </div>
                <!-- end container_24 -->
            </div>
            <!-- end subpage_tagline -->
        </div>
        <!-- end subpage_tagline_wrapper -->
        <div class="clear"></div>
        <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
        <div class="sub_navigation_wrapper find_location_page clearfix">
            <div class="sub_navigation main_content">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="form findLocationSearchForm" id="find_location_search_form" runat="server">
                            <div class="grid_6 suffix_1 alpha">
                                <input type="text" placeholder="City, State, Zip" runat="server" class="findLocationCity" id="FindLocationCity" />
                            </div>
                            <!-- end grid -->                            
                            <div class="grid_6 suffix_1">
                                <select class="custom-select fnd_location_distance" runat="server" id="find_location_distance">
                                    <option selected="selected" value="25">25</option>                                        
                                    <option value="50">50</option>
                                    <option value="100">100</option>
                                    <option value="25">200</option>
                                </select>                                
                            </div>
                            <!-- end grid -->
                            <div class="grid_3 suffix_1 find_location_page_btn">
                                <input type="submit" id="find_location_page_submit" class="search" value="Search" />                               
                            </div>
                            <div id="searchLocWaitImg" style="display: none;" class="grid_3 suffix_1 ">
                                <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                            </div>
                            <!-- end grid -->
                            <div class="prefix_1 grid_5 omega divider_left see_all_locations findLoc_see_all_locations">
                                <div class="cta-button-wrap">
                                    <a class="cta-button-text" href="#all_locations"><span>See All Locations</span></a>
                                </div>
                                <!-- end cta-button-wrap -->
                            </div>
                        </div>                        
                        <!-- end grid -->
                    </div>
                    <!--end grid_24-->
                </div>
                <!--end container_24-->
                <!-- end sub_nav -->
            </div>
        </div> 
        <!-- end sub_nav_wrapper-->
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
                                <h3>Sir Speedy Locations</h3>
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
                                <asp:Literal ID="ltrStateData" runat="server"></asp:Literal>
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
    </div>
    <input type="hidden" id="loadAllLocations" class="loadAllLocations" runat="server" value="true" />
    <div class="clear"></div>
</asp:Content>

