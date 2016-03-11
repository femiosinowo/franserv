<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="why_we_are_different.aspx.cs" Inherits="why_we_are_different" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="clear"></div>
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper clearfix">
        <div class="subpage_tagline why_different">
            <div class="container_24">
                <div class="grid_24">
                    <!-- Local - Why We Are Different  -->
                    <h2 id="local_why_different_tagline"><asp:Literal ID="ltrBannerTitle" runat="server"></asp:Literal><span><asp:Literal ID="ltrBannerSubTitle" runat="server"></asp:Literal></span></h2>
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->

    <div class="clear"></div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation about-us-local">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us Local -->
                    <ul id="about-local-desktop-nav">
                        <li class="about-local-link"><a href="/company-info/">Company Info</a></li>
                        <li class="why-different-link active"><a href="/why-we-are-different/">Why We Are Different</a></li>
                        <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                        <li class="local-news-link"><a href="/news/">News</a></li>
                        <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear"></div>
    <!-- mmm About Us - Why We Are Different mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About Us - Why We Are Different mmm -->
    <div class="why_different_wrapper  clearfix">
        <div class="why_different main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_9">
                        <h2 class="red-text">
                            <asp:Literal ID="ltrContentTitle" runat="server"></asp:Literal></h2>
                        <p class="tagline_inner">
                            <asp:Literal ID="ltrContentTagLine" runat="server"></asp:Literal></p>
                        <p>
                            <asp:Literal ID="ltrDescription" runat="server"></asp:Literal></p>
                    </div>
                    <!-- end grid_9 -->
                    <div class="grid_13 prefix_2 omega">
                        <div class="video_wrapper" id="videoSection" runat="server" visible="false">
                            <iframe id="videoIframe" runat="server" src="#" visible="false" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                        </div>
                        <div class="image_wrapper" id="ImageSection" runat="server" visible="false">
                             <img src="#" alt="" id="mainImage" runat="server" />
                         </div>
                        <!-- end video_wrapper -->
                        <div class="statement-text">
                            <p>
                                <asp:Literal ID="ltrStatementText" runat="server"></asp:Literal></p>
                        </div>
                        <!-- end statement-text -->
                    </div>
                    <!-- end grid_15 -->
                    <div class="clear"></div>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!--end why_different -->
    </div>
    <!-- end why_different_wrapper -->
    <div class="clear"></div>
    <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
    <div class="your_location_wrapper why_different clearfix">
        <div class="your_location clearfix">
            <div id="location_info_map_wrapper" class="clearfix">
                <div id="location_info_wrapper">
                    <div class="container_24">
                        <div id="location_info" class="clearfix">
                            <h2 class="headline"><span>Our Location</span></h2>
                            <hr />
                            <div class="grid_11 alpha" id="location_address_hours">
                                <a class="logo" href="#">
                                    <p class="visuallyhidden">Signal Graphics</p>
                                    <img alt="Sir Speedy" src="/images/logo-infobox.png" />
                                </a>
                                <asp:Literal ID="litLocAddress" runat="server" />
                                <asp:Literal ID="ltrWorkingHours" runat="server" />
                            </div>
                            <!-- end .col 1-->
                            <div class="grid_12 omega" id="location_contact_info">
                                <asp:Literal ID="litLocContact" runat="server" />
                            </div>
                            <!-- end .col 2-->
                            <div class="clear"></div>
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
    <div class="clear"></div>
    <!-- mmm Our Team (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Our Team (lcl) mmm -->
    <div class="our_team_wrapper  clearfix">
        <div class="our_team clearfix">
            <div class="container_24 franchiseLocalTeam" id="our_team_main">
                <div class="grid_24">
                    <h2 class="red-text">Our Team</h2>
                    <asp:ListView ID="lvOurTeam" GroupItemCount="4" runat="server">
                        <GroupTemplate>                          
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </GroupTemplate>
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="grid_6 <%#(Container.DataItemIndex == 0 ? "alpha" : "")%> cs_container">
                                <a href="#" class="cs_image">
                                    <img src="<%# Eval("Image") %>" alt="" />
                                </a>
                                <!-- end cs_image_01 -->
                                <div class="cs_image_content_wrapper">
                                    <div class="cs_image_content">
                                        <div class="team_info">
                                            <h3> <%# Eval("Name") %> <span> <%# Eval("Title") %></span></h3>
                                            <ul>
                                                <%# Eval("Phone") %>
                                                <%# Eval("Mobile") %>
                                                <%# Eval("Email") %>
                                            </ul>
                                        </div>
                                    </div>
                                    <!-- end cs_image_01_content -->
                                </div>
                                <!-- end cs_image_01_content_wrapper -->
                            </div>
                            <!-- grid_6 -->
                        </ItemTemplate>
                    </asp:ListView>
                   
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!--end our_team -->
    </div>
    <!-- end our_team_wrapper -->
    <div class="clear"></div>
</asp:Content>

