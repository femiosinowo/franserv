<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="partners.aspx.cs" Inherits="partners" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_2.jpg" alt=""> --%>
                <CMS:ContentBlock ID="cbPartnerHeaderImage" runat="server" DynamicParameter="id"
                    CacheInterval="300" />
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
        <div class="sub_navigation about-us">
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
                </div>
                <div class="menu-items-block">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us National -->
                    <ul id="about-desktop-nav">
                        <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                        <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                        <li class="partners-link active"><a href="/company-info/partners/">Partners</a></li>
                        <li class="history-link"><a href="/company-info/history/">History</a></li>
                        <li class="news-link news2-link"><a href="/company-info/news/">News</a></li>
                        <li class="media-link media2-link"><a href="/company-info/in-the-media/">In the Media</a></li>
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
    <!-- mmm Partners mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Partners mmm -->
    <div class="partners_wrapper clearfix" style="background-color: white;">
        <div class="main_content partners clearfix">
            <div class="container_24" id="partners_content">
                <div class="grid_6 headline-block">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbPartnersSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block">
                    <div class="grid_24" id="partners_container">
                        <asp:Repeater runat="server" ID="UxPartnersRows" OnItemDataBound="UxPartnersRows_ItemDataBound">
                            <ItemTemplate>
                                <div class="container_24 partner_row" id="row_<%#Eval("RowId") %>">
                                    <div class="grid_24 partner_logo_wrapper">
                                        <asp:Repeater runat="server" ID="UxPartnersRow">
                                            <ItemTemplate>
                                                <div class="grid_6 <%#Eval("cssClassText") %> partner_logo">
                                                    <a href="#" class="logo" id="<%#Eval("companyName") %>">
                                                        <img src="<%#Eval("imgSRC") %>" alt="" /></a>
                                                    <!-- Partner details -->
                                                    <div id="<%#Eval("companyName") %>_detail" class="partner_detail clearfix">
                                                        <div class="grid_10 prefix_2 alpha partner_logo_large">
                                                            <img src="<%#Eval("imgSRC") %>" alt="" />
                                                        </div>
                                                        <!-- end partner_logo_large -->
                                                        <div class="grid_10 suffix_2 omega partner_text">
                                                            <div class="partner_text_inner">
                                                                <h3>
                                                                    <%#Eval("tagline") %></h3>
                                                                <p>
                                                                    <%#Eval("teaser") %>
                                                                </p>
                                                                <div class="cta-button-wrap purple">
                                                                    <a class="cta-button-text" href="<%#Eval("url") %>" target="_new"><span>
                                                                        <%#Eval("url") %></span></a>
                                                                </div>
                                                                <!-- end cta-button-wrap -->
                                                            </div>
                                                        </div>
                                                        <!-- end partner_text -->
                                                    </div>
                                                    <!-- end partenr_detail -->
                                                </div>
                                                <!-- grid_6 -->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <!--end grid_24 partner_logo_wrapper -->
                                    <div class="clear">
                                    </div>
                                    <div class="partners_detail_wrapper clearfix" id="detail_box_<%#Eval("RowId") %>">
                                        <div class="grid_24">
                                            <a class="close_button"><span class="visuallyhidden">X</span></a>
                                            <div class="detail_content">
                                            </div>
                                            <!-- end content -->
                                        </div>
                                        <!--end grid_24-->
                                    </div>
                                    <!-- partners_detail_wrapper-->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <!--end container_24-->
                    </div>
                    <!--end grid_24-->
                </div>
                <!--end grid_18-->
            </div>
            <!--end container_24-->
        </div>
        <!--end insights_case_studies -->
    </div>
    <!-- end insights_case_studies_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
