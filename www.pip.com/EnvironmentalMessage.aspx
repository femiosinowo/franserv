<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="EnvironmentalMessage.aspx.cs"
    Inherits="EnvironmentalMessage" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/UserControls/CenterDataWithSocialMediaLinks.ascx" TagPrefix="ux" TagName="CenterDataWithSocialMediaLinks" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <cms:ContentBlock ID="cbHeader" runat="server" DoInitFill="false" />
                <!--<img src="images/headers/header_1.jpg" alt="" />-->
            </div>
            <!-- header image-->
        </div>
        <!-- end header_image_content -->
    </div>
    <!-- end header_image_wrapper-->
    <div class="clear"></div>
    <!-- mmm Interior Menu mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Interior Menu mmm -->
    <div class="sub_navigation_wrapper  clearfix">
        <div class="sub_navigation footer-nav">
            <div id="sub_navigation">
                <div class="menu-title-block">                    
                    <!-- Footer -->
                    <div id="footer-menu-h2">
                        <h2 id="menu-privacy-policy">Privacy Policy</h2>
                        <h2 id="menu-terms-conditions">Terms &amp; Conditions</h2>
                        <h2 id="menu-environment-msg">Environmental Message</h2>
                        <h2 id="menu-sitemap">Sitemap</h2>
                    </div>
                </div>
                <div class="menu-items-block">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->                    
                    <!-- Footer -->
                    <ul id="footer-nav">
                        <li class="footer-privacy-policy-link"><a href="/privacy-policy/">Privacy Policy</a></li>
                        <li class="footer-terms-conditions-link"><a href="/terms-and-conditions/">Terms &amp; Conditions</a></li>
                        <li class="footer-environment-msg-link"><a href="/environmental-message/">Environmental Message</a></li>
                        <li class="footer-sitemap-link"><a href="/site-map/">Sitemap</a></li>
                    </ul>
                </div>
            </div>
            <!-- sub navigation-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear"></div>
    <!-- mmm terms_conditions Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm terms_conditions Content mmm -->
    <div class="terms_conditions_wrapper national_footer_page clearfix">
        <div class="terms_conditions clearfix">
            <div id="terms_conditions" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <cms:ContentBlock ID="cbSiteMapSideContent" runat="server" DoInitFill="false" />
                                <ux:CenterDataWithSocialMediaLinks ID="centerInfoSocialMediaLinks1" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block col-height-equal">
                    <!-- mmmmmmmmmmmmmmmmmmmm Terms & Conditions Content mmmmmmmmmmmmmmmmmmmm -->
                    <div class="prefix_1 suffix_1 grid_22 footer_content clearfix">
                        <cms:ContentBlock ID="cbTermsContent" runat="server" DynamicParameter="id" CacheInterval="300" />
                    </div>
                    <!-- news_article -->
                    <div class="clear"></div>
                </div>
                <!-- grid 18-->
            </div>
            <!-- news content-->
        </div>
        <!--end terms_conditions -->
    </div>
    <!-- end terms_conditions_wrapper -->
    <div class="clear"></div>
</asp:Content>
