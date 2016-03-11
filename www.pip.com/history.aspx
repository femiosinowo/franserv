<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="history.aspx.cs" Inherits="history_national" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_2.jpg" alt=""> --%>
                <CMS:ContentBlock ID="cbHistoryHeaderImage" runat="server" DynamicParameter="id"
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
                        <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                        <li class="history-link active"><a href="/company-info/history/">History</a></li>
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
    <!-- mmm History mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm History mmm -->
    <div class="history_wrapper clearfix" style="background-color: white;">
        <div class="main_content history clearfix">
            <div class="container_24" id="history_content">
                <div class="grid_6 headline-block">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbHistorySideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block">
                    <div class="grid_24" id="history_container">
                        <CMS:ContentBlock ID="cbHistory" runat="server" />
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
    <div class="clear"></div>
</asp:Content>
