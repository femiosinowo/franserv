<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="history.aspx.cs" Inherits="history_national" %>
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
                            <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                            <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                            <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                            <li class="history-link active"><a href="/company-info/history/">History</a></li>
                            <li class="news-link"><a href="/company-info/news/">News</a></li>
                            <li class="media-link"><a href="/company-info/in-the-media/">In the Media</a></li>
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
                   <CMS:ContentBlock ID="cbHistory" runat="server" />
                </div>
                <!-- end grid_24 -->
            </div>
            <!--end grid_24-->
        </div>
        <!--end container_24-->
    </div>
    <!--end about_us_company -->
    <!-- end about_us_company wrapper -->
    <div class="clear"></div>
</asp:Content>
