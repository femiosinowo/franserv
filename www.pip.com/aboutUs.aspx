<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="aboutUs.aspx.cs" Inherits="about_national" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<%@ Register Src="~/UserControls/HomePageNationalFooter.ascx" TagPrefix="ux" TagName="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        function show() {
            if (($("#iframe").attr("src") == "") || ($("#iframe").attr("src") == null)) {
                $("#dvImage").attr("visible", true);
            }
            else {
                $("#dvImage").attr("visible", false);
            }
        }
    </script>

    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <CMS:ContentBlock ID="cbCompanyInfoHeaderImage" runat="server" DynamicParameter="id"
                    CacheInterval="300" />
                <%--<img src="images/headers/header_1.jpg" alt="">--%>
            </div>
            <!-- header image-->
        </div>
        <!-- end header_image_content -->
    </div>
    <!-- end header_image_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper  clearfix">
        <div id="aboutUsSubNav" runat="server" class="sub_navigation about-us">
            <div id="sub_navigation">
                <div class="menu-title-block">
                    <div id="about-menu-h2">
                        <h2 id="menu-company-info">Company Info</h2>
                        <h2 id="menu-mgmt-team">Management Team</h2>
                        <h2 id="menu-partners">Partners</h2>
                        <h2 id="menu-history">History</h2>
                        <h2 id="menu-news">News</h2>
                        <h2 id="menu-media">In The Media</h2>
                    </div>
                    <div id="about-local-menu-h2">
                        <h2 id="menu-about-pip">Company Info</h2>
                        <h2 id="menu-why-different">Why We're Different</h2>
                        <h2 id="menu-testimonials">Testimonials</h2>
                        <h2 id="menu-news2">News</h2>
		                <h2 id="menu-media2">In The Media</h2>
                    </div>
                </div>
                <div class="menu-items-block">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us National -->
                    <div id="desktopNavNational" visible="false" runat="server">
                        <ul id="about-desktop-nav">
                            <li class="company-info-link active"><a href="/company-info/">Company Info</a></li>
                            <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                            <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                            <li class="history-link"><a href="/company-info/history/">History</a></li>
                            <li class="news-link news2-link"><a href="/company-info/news/">News</a></li>
                            <li class="media-link media2-link"><a href="/company-info/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                    <div id="desktopNavLocal" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link active"><a href="/company-info/">Company Info</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link"><a href="/news/">News</a></li>
                            <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Company Info Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Company Info Content mmm -->
    <asp:ListView ID="lvAboutUs" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="company_info_content_wrapper main_about_us clearfix" id="dv">
                <div class="company_info_content clearfix">
                    <div id="company_info_content" class="container_24">
                        <div class="grid_6 headline-block col-height-equal">
                            <div class="int-headline-block headline-block-black int-block-1">
                                <div class="headline-content-outer">
                                    <div class="headline-content-inner">
                                        <span class="headline-block-icon-black"></span>
                                        <h2 class="headline">
                                            <%# Eval("Title") %></h2>
                                        <p>
                                            <%# Eval("SubTitle") %>
                                        </p>
                                    </div>
                                    <!--headline content-->
                                </div>
                                <!--headline content-->
                            </div>
                        </div>
                        <!--grid_6-->
                        <div class="grid_18 content-block col-height-equal">
                            <div class="grid_8 prefix_1 content-text">
                                <span>
                                    <%# Eval("Description") %></span>
                            </div>
                            <div class="grid_13 prefix_1 suffix_1 omega">

                                <div class="video_wrapper" style="<%# Eval("videoSRC") == "" ? "display:none;": "display:block;" %>">
                                    <iframe src="<%# Eval("videoSRC") %>" frameborder="0" webkitallowfullscreen mozallowfullscreen
                                        allowfullscreen></iframe>
                                </div>
                                <div class="image_wrapper" style="<%# Eval("videoImagePath") == "" ? "display:none;": "display:block;" %>">
                                    <img src="<%# Eval("videoImagePath") %>" alt="" />
                                </div>
                                <!-- end video_wrapper -->
                            </div>
                        </div>
                        <!-- grid 18 content block-->
                    </div>
                    <!-- company info content-->
                </div>
                <!--end company_info_content -->
            </div>
            <!-- end company_info_content_wrapper -->
            <div class="clear">
            </div>
            <!-- mmm Interior Tagline Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Interior Tagline Content mmm -->
            <div class="int_tagline_content_wrapper clearfix">
                <div class="int_tagline_content clearfix">
                    <div id="int_tagline_content" class="container_24">
                        <div class="grid_6 headline-block headline-white col-height-equal">
                            <div class="int-headline-block headline-block-black int-block-1">
                                <div class="headline-content-outer">
                                    <div class="headline-content-inner">
                                        <h4>
                                            <%# Eval("statement") %></h4>
                                    </div>
                                    <!--headline content-->
                                </div>
                                <!--headline content-->
                            </div>
                        </div>
                        <!--grid_6-->
                        <div class="grid_18 content-block col-height-equal">
                            <div class="grid_13 prefix_1 content-text">
                                <p>
                                    <%# Eval("Disclaimer")%>
                                </p>
                            </div>
                            <div class="grid_8 prefix_1 suffix_1 omega">
                                <div class="fs_logo">
                                    <img alt="Franchise Services" src="<%# Eval("ImagePath")%>">
                                </div>
                            </div>
                        </div>
                        <!-- grid 18 content block-->
                    </div>
                    <!--container 24-->
                </div>
                <!--end int_tagline -->
            </div>
            <!-- end int_tagline -->
        </ItemTemplate>
    </asp:ListView>
    <div class="clear">
    </div>
    <!-- mmm Awards Rotator (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Awards Rotator (both) mmm -->
    <div class="awards_slider_wrapper clearfix">
        <div class="awards_slider clearfix">
            <div class="awards_slider container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">Awards &amp; Industry Recognition</h2>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <!-- rotator section mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm rotator section -->
                <div class="grid_18 slider-wrapper col-height-equal">
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
            </div>
        </div>
        <!-- end awards_slider -->
    </div>
    <!-- end awards_slider_wrapper  -->
    <div class="clear">
    </div>
    <!-- mmm CORP Location mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm CORP Location  mmm -->
    <div class="our_location_wrapper  clearfix">
        <div class="our_location clearfix">
            <div id="corp_location" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <asp:Repeater runat="server" ID="UxCompanyLocation" OnItemDataBound="UxCompanyLocation_OnItemDataBound">
                                    <ItemTemplate>
                                        <span class="headline-block-icon-white"></span>
                                        <h2 class="headline">Corporate<br />
                                            Location</h2>
                                        <ul class="location_address">
                                            <li>
                                                <%# Eval("address1") %></li>
                                            <li>
                                                <%# Eval("city") %>,
                                                    <%# Eval("state") %>
                                                <%# Eval("zipcode") %></li>
                                        </ul>
                                        <a href="#" class="cta-button-text cta-button-wrap white-btn" target="_blank" id="directions_lb" runat="server"><span>Directions</span>
                                        </a>
                                        <ul class="location_hours">
                                            <li>Contact Us:</li>
                                            <li>
                                                <%# Eval("daysOperation") %></li>
                                            <li>
                                                <%# Eval("hoursOperation") %></li>
                                            <br>
                                        </ul>
                                        <ul class="location_contact">
                                            <li>
                                                <%# Eval("phone") %></li>
                                            <li>
                                                <%# Eval("fax") %></li>
                                            <li><a href="mailto:'<%# Eval("email") %>'">
                                                <%# Eval("email") %></a></li>
                                        </ul>
                                        <div class="location_social_media">
                                            <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 our_location_content col-height-equal">
                    <%--<div class="grid_6 omega">
                            <div class="cta-button-wrap purple">
                                <a class="cta-button-text" target="_blank" id="directions_lb" runat="server" href="#">
                                    <span>DIRECTIONS</span></a>
                            </div>
                            <!--lvl-2-title-wrap -->
                        </div>--%>
                    <div id="location_map_wrapper" class="clearfix">
                        <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                        <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                        <div id="location_map">
                            <!-- Google map container -->
                        </div>
                        <!-- #location_map -->
                    </div>
                    <!-- end #location_map_wrapper -->
                </div>
                <!-- our_location_content -->
            </div>
        </div>
        <!-- end our_locations -->
    </div>
    <!-- end our_locations_wrapper  -->
    <div class="clear">
    </div>
    <!-- mmm footer upper mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer upper mmm -->
    <ux:Blog ID="uxBlog" runat="server" />
    <!-- end your location wrapper -->
    <div class="clear">
    </div>
</asp:Content>
