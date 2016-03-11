<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="product_services.aspx.cs" Inherits="product_services" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/ProductsAndServicesIndexFooterUpper.ascx" TagPrefix="ux" TagName="PSIFooterUpper" %>
<%@ Register Src="~/UserControls/ProductAndServicesInteriorMenu.ascx" TagPrefix="ux" TagName="PSCategoryInteriorMenu" %>
<%@ Register Src="~/UserControls/HomePageCaseStudies.ascx" TagPrefix="ux" TagName="CaseStudies" %>
<%@ Register Src="~/UserControls/HomePageBriefsAndWhitepapers.ascx" TagPrefix="ux" TagName="BriefAndWhitepapers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="products-services">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image">
                <div class="header_image_content">
                    <CMS:ContentBlock ID="cbPSHeaderImage" runat="server" DynamicParameter="id" CacheInterval="300" />
                    <%--<img src="images/headers/header_1.jpg" alt="Products &amp; Services">--%>
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Interior Menu mmmmmmmmmmmmmmmmmmmmmmmm Interior Menu mmm -->
        <ux:PSCategoryInteriorMenu ID="uxPSCategoryInteriorMenu" runat="server" />
        <!-- mmm Products & Services Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Products & Services Content mmm -->
        <div class="products_services_page_content_wrapper  clearfix">
            <div class="products_services_page_content clearfix">
                <!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm   PRODUCT DETAIL mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
                <asp:Repeater runat="server" ID="UxPSIndex">
                    <ItemTemplate>
                        <div class="product-detail-wrapper clearfix" id="<%# Eval("hrefId") %>">
                            <div class="container_24">
                                <div class="alpha grid_12 product-detail-text">
                                    <span class="headline-block-icon-black"></span>
                                    <h2>
                                        <span>
                                            <%# Eval("title") %></span></h2>
                                    <h3>
                                        <%# Eval("subtitle") %></h3>
                                    <p><%# Eval("teaser")%></p>
                                    <div class="product-detail-links">
                                        <a href="<%# Eval("hrefText") %>" class="cta-button-text">
                                            <div class="cta-button-wrap black-btn">
                                                <span>Read More</span>
                                            </div>
                                        </a>
                                        <!-- end cta-button-wrap -->
                                    </div>
                                    <!-- end buttons -->
                                </div>
                                <!-- end grid 12 -->
                                <div class="omega grid_10 <%# Container.ItemIndex % 2 == 0 ? "prefix_2" : "suffix_2" %>">
                                    <div class="product-detail-image-content">
                                        <img alt="placeholder" src="<%# Eval("subcategoryImageSRC") %>">
                                        <div class="caption">
                                            <h4><%# Eval("subcategoryTitle") %></h4>
                                            <p><%# Eval("caption") %></p>
                                        </div>
                                    </div>
                                    <!-- end thumb-slider flexslider -->
                                </div>
                                <!-- end product-detail-slider -->
                            </div>
                            <!-- end container_24 -->
                        </div>
                        <!-- end product-detail-wrapper -->
                    </ItemTemplate>
                </asp:Repeater>
                <!-- end product-detail-wrapper -->
            </div>
            <!--end products_services_page_content -->
        </div>
        <!-- end products_services_page_content_wrapper -->
        <div class="clear">
        </div>
        <!-- mmm Products & Services - Find A Location mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Products & Services  - Find A Location mmm -->
        <div class="sub_find_location_wrapper  clearfix">
            <div class="sub_find_location clearfix">
                <section id="sub_find_location" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-white int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-white"></span>
                                    <h2 class="headline">
                                        Find A<br />
                                        Location</h2>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!--grid_6-->
                    <div class="grid_18 content-block col-height-equal background_wrapper" id="findLocationText" runat="server" data-image="../images/sub_find_location_bg.jpg">
                        <div class="sub_find_location_content">
                            <CMS:ContentBlock ID="cbFindLocationText" runat="server" DoInitFill="false" />
                            <!-- end cta-button-wrap -->
                        </div>
                    </div>
                    <!--grid_18-->
                </section>
            </div>
            <!-- end find_locations -->
        </div>
        <!-- end find_locations_wrapper  -->        
        <!-- end our_portfolio_studies_wrapper -->
        <!-- mmm Our Portfolio / Case Studies Wrapper mmmmmmmmmmmmmmmmmmm  Our Portfolio / Case Studies Wrapper mmm -->
       <%-- <ux:CaseStudies ID="uxCaseStudies" runat="server" /> --%>
        <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
        <!-- mmm BriefAndWhitepapers Wrapper mmmmmmmmmmmmmmmmmmm  BriefAndWhitepapers Wrapper mmm -->
        <%--<ux:BriefAndWhitepapers ID="uxBriefAndWhitepapers" runat="server" />--%> 
        <div class="clear">
        </div>
        <!-- mmm footer upper mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer upper mmm -->
        <%--<ux:PSIFooterUpper ID="PSFooterUpper" runat="server" />--%>
        <!-- end upperfooter_wrapper-->
        <div class="clear">
        </div>
    </div>
</asp:Content>
