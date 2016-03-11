<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="aboutUs.aspx.cs" Inherits="about_national" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline about_us">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- National - About Us - Company Info -->
                    <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
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
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us National -->
                    <div id="desktopNavNational" runat="server">
                        <ul id="about-desktop-nav">
                            <li class="company-info-link active"><a href="/company-info/">Company Info</a></li>
                            <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                            <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                            <%--<li class="history-link"><a href="/company-info/history/">History</a></li>--%>
                            <li class="news-link"><a href="/company-info/news/">News</a></li>
                            <li class="media-link"><a href="/company-info/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                    <div id="desktopNavLocal" class="sub_navigation about-us-local" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link active"><a href="/company-info/">Company Info</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link"><a href="/news/">News</a></li>
                            <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
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
    <!-- mmm About Us - Company Info mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About Us -Company Info mmm -->
    <div class="about_us_company_wrapper  clearfix">
        <div class="about_us_company clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <asp:ListView ID="lvAboutUs" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="grid_9">
                                <h2 class="red">
                                    <%# Eval("Title") %></h2>
                                <p class="tagline_inner">
                                    <%# Eval("SubTitle") %>
                                </p>
                                <span>
                                    <%# Eval("Description") %></span>
                            </div>
                            <!-- end grid_9 -->
                            <div class="grid_13 prefix_2 omega">
                                <div class="video_wrapper" style="<%# Eval("videoSRC") == "" ? "display:none;" : "display:block;" %>">
                                    <iframe src="<%# Eval("videoSRC") %>" frameborder="0" webkitallowfullscreen mozallowfullscreen
                                        allowfullscreen></iframe>
                                </div>
                                <div class="image_wrapper" style="<%# Eval("videoImagePath") == "" ? "display:none;" : "display:block;" %>">
                                  <img src="<%# Eval("videoImagePath") %>" alt="" />
                                </div>
                                <!-- end video_wrapper -->
                                <div class="statement-text">
                                    <p>
                                        <%# Eval("statement") %>
                                    </p>
                                </div>
                                <!-- end statement-text -->
                            </div>
                            <!-- end grid_15 -->
                            <div class="clear">
                            </div>
                            <div class="grid_24 about_lower">
                                <div class="grid_9 alpha">
                                    <p class="disclaimer">
                                        <%# Eval("Disclaimer") %>
                                    </p>
                                </div>
                                <!-- end grid_20 -->
                                <div class="grid_15 omega">
                                    <div class="fs_logo">
                                        <img src="<%# Eval("ImagePath") %>" alt="Franchise Services" />
                                    </div>
                                    <!-- end logo -->
                                </div>
                                <!-- end grid_4 -->
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!-- end grid_24 -->
            </div>
            <!--end grid_24-->
        </div>
        <!--end container_24-->
    </div>
    <!--end about_us_company -->
    <!-- end about_us_company wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Awards Rotator (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Awards Rotator (both) mmm -->
    <div class="awards_slider_wrapper clearfix">
        <div class="awards_slider">
            <!-- rotator section mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm rotator section -->
            <h2 class="headline">Awards and Industry Recognition</h2>
            <div class="slider_wrapper">
                <ul>
                    <asp:Repeater runat="server" ID="UxAwards">
                        <ItemTemplate>
                            <li>
                                <h4>
                                    <%# Eval("title") %><span><%#Eval ("achievement") %></span></h4>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!--end slider_wrapper -->
            <div class="clear">
            </div>
        </div>
        <!-- end awards_rotator -->
    </div>
    <!--end awards_rotator_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
    <div id="corporateLocation" runat="server" class="your_location_wrapper about_us clearfix">
        <div class="your_location clearfix">
            <div id="location_info_map_wrapper" class="clearfix">
                <div id="location_info_wrapper">
                    <div class="container_24">
                        <div id="location_info" class="clearfix">
                            <CMS:ContentBlock ID="cbGoogleInfoBoxImgLogo" runat="server" Visible="false" DoInitFill="false" />
                            <asp:Repeater runat="server" ID="UxCompanyLocation">
                                <ItemTemplate>
                                    <h2 class="headline">
                                        <span>Corporate Location</span></h2>
                                    <hr />
                                    <div class="grid_11 alpha" id="location_address_hours">
                                        <a class="logo" href="#">
                                            <p class="visuallyhidden">
                                                Signal Graphics
                                            </p>
                                            <%# Eval("logo") %>
                                        </a>
                                        <p class="location_address">
                                            <%# Eval("address1") %>
                                            <%# Eval("city") %>,
                                            <%# Eval("state") %>
                                            <%# Eval("zipcode") %>
                                        </p>
                                        <p class="store_hours">
                                            <span>Call Us:</span>
                                            <%# Eval("daysOperation") %><br>
                                            <%# Eval("hoursOperation") %>
                                        </p>
                                    </div>
                                    <!-- end .col 1-->
                                    <div class="grid_12 omega" id="location_contact_info">
                                        <ul>
                                            <li class="location-icon-phone"><span>
                                                <%# Eval("phone") %></span></li>
                                            <li class="location-icon-fax"><span>
                                                <%# Eval("fax") %></span></li>
                                            <li class="location-icon-email"><span><a href="mailto:' <%# Eval("email") %>'">
                                                <%# Eval("email") %></a></span></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <!-- end .col 2-->
                            <div class="clear">
                            </div>
                            <hr />
                            <div id="location_footer">
                                <div class="grid_18 alpha">
                                    <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                                </div>
                                <!-- end grid -->
                                <div class="grid_6 omega">
                                    <div class="cta-button-wrap purple">
                                        <a class="cta-button-text" target="_blank" id="directions_lb" runat="server" href="#"><span>DIRECTIONS</span></a>
                                    </div>
                                    <!--lvl-2-title-wrap -->
                                </div>
                                <!-- end #location_footer -->
                            </div>
                            <!-- end #location_info -->
                        </div>
                        <!-- end #location_infobox_wrapper -->
                    </div>
                    <!-- end container_24 -->
                </div>
                <!-- end location_info_wrapper -->
                <div id="location_map_wrapper" class="clearfix">
                    <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                    <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                    <div id="location_map" class="corporate">
                        <!-- Google map container -->
                    </div>
                    <!-- #location_map -->
                </div>
                <!-- end #location_map_wrapper -->
            </div>
            <!-- end #location_info_map_wrapper -->
        </div>
        <!-- end your location -->
    </div>
    <!-- end your location wrapper -->
    <div class="clear">
    </div>
</asp:Content>
