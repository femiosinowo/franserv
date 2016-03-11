<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="why_we_are_different.aspx.cs" Inherits="why_we_are_different" %>

<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var count = 1;
            $('.mainRow').each(function () {
                $(this).attr('id', 'row_' + count);
                count++;
            });
        });
    </script>

    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <CMS:ContentBlock ID="cbWhyWeAreDifferentHeaderImage" runat="server" DynamicParameter="id"
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
        <div class="sub_navigation about-us-local">
            <div id="sub_navigation">
                <div class="menu-title-block">
                    <div id="about-local-menu-h2">
                        <!-- <h2 id="menu-about-pip">Company Info</h2> -->
                        <h2 id="menu-why-different">Why We're Different</h2>
                        <h2 id="menu-testimonials">Testimonials</h2>
                        <h2 id="menu-news">News</h2>
                        <h2 id="menu-media">In The Media</h2>
                    </div>
                </div>
                <div class="menu-items-block">
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
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm why_we_are_different Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm why_we_are_different Content mmm -->
    <div class="why_we_are_different_content_wrapper local_about_us clearfix">
        <div class="why_we_are_different_content clearfix">
            <div id="why_different_content" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbWhyWeAreDifferentSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                    <div class="int-headline-block tagline-block int-block-2">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <h4>
                                    <CMS:ContentBlock ID="cbWhyWeAreDifferentSideQuotesContent" runat="server" />
                                </h4>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <div class="grid_18 content-block col-height-equal">
                    <div class="grid_8 prefix_1 content-text">
                        <p class="sub-headline">
                            <asp:Literal ID="ltrContentTagLine" runat="server"></asp:Literal>
                        </p>
                        <p>
                            <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <div class="grid_13 prefix_1 suffix_1 omega">
                        <div class="video_wrapper" id="videoSection" runat="server" visible="false">
                            <iframe id="videoIframe" runat="server" src="#" visible="false" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                        </div>
                        <div class="image_wrapper" id="ImageSection" runat="server" visible="false">
                            <img src="#" alt="" id="mainImage" runat="server" />
                        </div>
                        <!-- end video_wrapper -->
                        <ul class="images_wrapper">
                            <li>
                                <CMS:ContentBlock ID="cbWhyweDiffImg1" runat="server" />
                            </li>
                            <li>
                                <CMS:ContentBlock ID="cbWhyweDiffImg2" runat="server" />
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- grid 18 content block-->
            </div>
            <!-- company info content-->
        </div>
        <!--end why_we_are_different_content -->
    </div>
    <!-- end why_we_are_different_content_wrapper -->
    <div class="clear">
    </div>
    <div class="our_location_wrapper  clearfix">
        <div class="our_location clearfix">
            <div id="our_location">
                <div class="grid_6 headline-block headline-block-white col-height-equal">
                    <div class="headline-content-outer">
                        <div class="headline-content-inner">
                            <span class="headline-block-icon-white"></span>
                            <h2 class="headline">Our<br />
                                Location</h2>
                            <asp:Literal ID="litLocAddress" runat="server" />
                            <asp:Literal ID="litLocContact" runat="server" />
                            <asp:Literal ID="ltrWorkingHours" runat="server" />
                            <div class="cta-button-text">
                                <a class="cta-button-wrap white-btn" target="_blank" id="directions_lb" runat="server"
                                    href="#"><span>Directions</span> </a>
                            </div>
                            <div class="location_social_media">
                                <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                            </div>
                        </div>
                        <!--headline content-->
                    </div>
                    <!--headline content-->
                </div>
                <div class="grid_18 our_location_content col-height-equal">
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
        <!-- end our_location -->
    </div>
    <div class="clear">
    </div>
    <!-- mmmmmmmmmmmmmm  our_team mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm   our_team mmmmmmmmmmmmm -->
    <div class="our_team  clearfix">
        <div class="our_team clearfix">
            <div id="our_team_content" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbWhyWeAreDifferentOurTeamSideContentId" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--end grid_6-->
                <div class="grid_18 content-block col-height-equal">
                    <div class="grid_24" id="our_team_main">
                        <asp:ListView ID="lvOurTeam" GroupItemCount="3" runat="server">
                            <GroupTemplate>
                                <div id="row_1" class="container_24 mgmt_profile_row mainRow">
                                    <div class="grid_24 mgmt_profile_wrapper">
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </div>
                                </div>
                            </GroupTemplate>
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="grid_8 <%#(Container.DataItemIndex == 0 ? "alpha" : "")%> cs_container">
                                    <%--<a href="#" class="cs_image">--%>
                                    <div id="profile_<%# Container.DataItemIndex+1 %>" class="cs_image">
                                        <img src="<%# Eval("Image") %>" alt="<%# Eval("Name") %>" />
                                         <!-- end cs_image_01 -->
                                        <div class="cs_image_content_wrapper">
                                            <div class="cs_image_content">
                                                <%--<div class="team_info">--%>
                                                <h3>
                                                    <%# Eval("Name") %>
                                                    <span>
                                                        <%# Eval("Title") %></span></h3>
                                                <ul>
                                                    <li class="phone"><span>
                                                        <%# Eval("Phone") %></span></li>
                                                    <li class="mobile"><span>
                                                        <%# Eval("Mobile") %></span></li>
                                                    <li class="email"><span><a href="mailto:<%# Eval("Email") %>">
                                                        <%# Eval("Email") %></a></span></li>
                                                </ul>
                                                <%--</div>--%>
                                            </div>
                                            <!-- end cs_image_01_content -->
                                        </div>
                                        <!-- end cs_image_01_content_wrapper -->
                                    </div>
                                    <%-- </a>--%>                                   
                                </div>
                                <!-- grid_8 -->
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- end our_location_wrapper  -->
    <div class="clear">
    </div>
</asp:Content>
