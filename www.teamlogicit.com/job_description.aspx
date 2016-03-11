<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="job_description.aspx.cs" Inherits="job_description" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>    
    <!-- mmm Job Description content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Description content  mmm -->
     <div class="job_app_wrapper clearfix">
     	<div class="container_24">
        	<div class="grid_20 suffix_4">
                    <!--end container_24-->
                    <div id="job_description_content" class="container_24 clearfix">
                        <div class="grid_24">
                            <h2 class="subpage">
                                <asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal> <span><asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></span></h2>
                            <div class="date_links">
                                <p class="gray">
                                    <strong>Posted On</strong>: <asp:Literal ID="ltrJobPostedDate" runat="server"></asp:Literal></p>                               
                            </div>
                            <!-- end date_links -->
                        </div>
                        <!--end grid_24-->
                        <div class="grid_13 suffix_3 desc_col_1">
                            <asp:Literal ID="ltrJobDescription" runat="server"></asp:Literal>
                            <div class="square_button submit_application">
                                <a id="applyJobLink" runat="server" href="/apply-to-job/"><span>APPLY TO THIS JOB</span></a>
                            </div>
                            <!-- end cta-button-wrap -->
                        </div>
                        <!-- end desc_col_1 -->
                        <div class="grid_7 prefix_1 desc_col_2">
                            <div id="job_location_map_wrapper">
                                <img id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=312x312&zoom=15&markers=icon:http://author.sirspeedy.com/sandbox/sirspeedy/images/location-map-marker.png%7C33.976463,-118.036812&style=feature:landscape%7Ccolor:0xe9e9e9&style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&sensor=true"
                                    alt="placeholder map" />
                            </div>
                            <h3>
                                Location</h3>
                            <p>
                                <strong><asp:Literal ID="ltrCenterAddress" runat="server"></asp:Literal></strong></p>
                            <h3>
                                Contact</h3>
                            <p class="no-margin-bottom">
                                <strong><asp:Literal ID="ltrCenterContactName" runat="server"></asp:Literal></strong></p>
                            <ul class="contact_info_icons_small gray">
                                <li class="phone"><span><asp:Literal ID="ltrCenterContactNumber" runat="server"></asp:Literal></span></li>
                                <li class="email"><span><asp:Literal ID="ltrCenterEmailAdress" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <h3>
                                How to Apply</h3>
                            <CMS:ContentBlock ID="cbHowToApplyText" runat="server" DoInitFill="false" /> 
                            <div class="square_button submit_application"><a id="applyJobLink2" runat="server" href="/apply-to-job/">APPLY NOW</a></div>
                            <!-- end cta-button-wrap -->
                        </div>
                        <!-- end desc_col_1 -->
                    </div>
                    <!--end job_description_content container_24 -->
                </div>
            <!--//grid_24-->
        </div>
        <!--container 24-->
    </div>
    <div class="clear">
    </div>
</asp:Content>
