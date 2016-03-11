<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="management_team.aspx.cs" Inherits="_management_team" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline mgmt_team">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- National - About Us - Management Team -->
                    <cms:ContentBlock ID="cbTaglineContent" runat="server" DynamicParameter="id" CacheInterval="300" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper  clearfix">
        <div class="sub_navigation about-us">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us National -->
                    <ul id="about-desktop-nav">
                        <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                        <li class="mgmt-team-link active"><a href="/company-info/management-team/">Management Team</a></li>
                        <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                        <li class="history-link"><a href="/company-info/history/">History</a></li>
                        <li class="news-link"><a href="/company-info/news/">News</a></li>
                        <li class="media-link"><a href="/company-info/in-the-media/">In the Media</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Management Team mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Management Team mmm -->
    <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix mgmt_team">
            <div class="container_24" id="mgmt_team_main">
                <div class="grid_24">
                    <asp:Repeater runat="server" ID="UxManagementTeam">
                        <ItemTemplate>
                            <div class="grid_6 cs_container <%#Eval("cssClass")%>">
                                <div class="cs_image">
                                    <img src="<%#Eval("imageSRC")%>" alt="<%#Eval("firstName")%> <%#Eval("lastName")%>" />
                                </div>
                                <!-- end cs_image_02 -->
                                <div class="cs_image_content_wrapper" ID="<%#Eval("counter")%>">
                                    <div class="cs_image_content">
                                        <div class="name_title">
                                            <h3>
                                                <%#Eval("firstName")%>
                                                <%#Eval("lastName")%></h3>
                                            <p>
                                                <%# Eval("jobTitle")%></p>
                                        </div>
                                        <div class="view_bio_button">
                                            <a href="#"><span class="visuallyhidden">View Bio</span></a></div> <%--<%#Eval("counter")%>--%>
                                    </div>
                                    <!-- end cs_image_02_content -->
                                </div>
                                <!-- end cs_image_02_content_wrapper -->
                            </div>
                            <!-- grid_6 -->
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
            <div class="clear">
            </div>
            <div id="mgmt_team_slider_wrapper" class="clearfix">
                <div class="container_24 clearfix">
                    <div class="grid_24">
                        <div class="cta-button-wrap white small back-button">
                            <a class="cta-button-text" id="hide_mgmt_slider" href="#"><span>Back</span></a>
                        </div>
                        <!--  cta-button-wrap -->
                    </div>
                    <!--end grid_24-->
                </div>
                <!--end container_24-->
                <div id="mgmt_team_slider" class="clearfix">
                    <div class="slider-wrapper">
                        <ul>
                            <asp:Repeater runat="server" ID="UxManagementTeamSlider">
                                <ItemTemplate>
                                    <li>
                                        <div class="container_24">
                                            <div class="grid_10 <%#Eval("cssClass")%> bio_slide_img"> <!-- alpha -->
                                                <img src="<%#Eval("imageSRC")%>" alt="<%#Eval("firstName")%> <%#Eval("lastName")%>" />
                                            </div>
                                            <!--end bio_slide_img -->
                                            <div class="grid_13 suffix_1 <%#Eval("cssClass")%> bio_slide_text"> <!-- omega -->
                                                <h2 class="red-text">
                                                    <%#Eval("firstName")%>
                                                    <%#Eval("lastName")%>
                                                    <span>
                                                        <%#Eval("jobTitle")%></span>
                                                </h2>
                                                <p class="special">
                                                    <%#Eval("abstract")%></p>
                                                <p>
                                                    <%#Eval("bio")%>
                                                </p>
                                                <p>
                                                    <%#Eval("socialMedia")%>
                                                </p>
                                            </div>
                                            <!--end bio_slide_text -->
                                        </div>
                                        <!--end container_24-->
                                    </li>
                                    <!-- end slide -->
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <!-- end slide-wrapper -->
                </div>
                <!-- end mgmt_team_slider -->
            </div>
            <!-- end mgmt_team_slider_wrapper -->
        </div>
        <!--end insights_case_studies -->
    </div>
    <!-- end insights_case_studies_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
