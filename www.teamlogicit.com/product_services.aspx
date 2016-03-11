<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="product_services.aspx.cs" Inherits="product_services" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/ProductAndServicesCategoryMobileSubNav.ascx" TagPrefix="ux"
    TagName="PSCategoryMobileSubNav" %>
<%@ Register Src="~/UserControls/ProductAndServicesIconTopSlider.ascx" TagPrefix="ux"
    TagName="PSIconSlider" %>
<%@ Register Src="~/UserControls/ProductsAndServicesIndexFooterUpper.ascx" TagPrefix="ux"
    TagName="PSIFooterUpper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper clearfix">
        <div class="subpage_tagline products_services_page">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Products & Services -->
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
    <!-- mmm Sub Nav (Mobile Only) mmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (Mobile only) mmm -->
    <ux:PSCategoryMobileSubNav ID="uxPSCategoryMobileSubNav" runat="server" />
    <!-- mmm Slider (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Slider mmm -->
    <ux:PSIconSlider ID="UxPSIconSlider" runat="server" />
    <!-- mmm Products & Services Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Products & Services Content mmm -->
    <div class="products_services_page_content_wrapper  clearfix">
        <div class="products_services_page_content clearfix">
            <!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm   PRODUCT DETAIL mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
            <asp:Repeater runat="server" ID="UxPSIndex" OnItemDataBound="UxPSIndex_ItemDataBound">
                <ItemTemplate>
                    <div class="product-detail-wrapper clearfix" id="<%# Eval("hrefId") %>">
                        <div class="container_24">
                            <div class="grid_24">
                                <div class="quote">
                                    <p>
                                        <%# Eval("statement") %>
                                    </p>
                                    <p class="quote-source">
                                        <%# Eval("quotesBy_Name") %>
                                        <%# Eval("quotesBy_Organization") %>
                                    </p>
                                </div>
                                <!-- end testimonial -->
                            </div>
                            <!--end grid 24-->
                            <div class="alpha grid_10 product-detail-text">
                                <div class="detail-header bottom-divider">
                                    <h3>
                                        <img src="<%# Eval("iconWhite") %>" alt="icon"><span><%# Eval("title") %></span></h3>
                                </div>
                                <!-- end detail-header bottom-divider -->
                                <p>
                                    <strong><%# Eval("teaser") %></strong>
                                </p>
                                <div class="product-detail-links">
                                    <div class="cta-button-wrap purple">
                                        <a href="<%# Eval("hrefText") %>" class="cta-button-text"><span>Learn More</span></a>
                                    </div>
                                    <!-- end cta-button-wrap -->
                                    <%--<ul>
                                        <li class="brief_print"><span class="visuallyhidden">Print</span><a href="#"></a></li>
                                        <li class="brief_email"><span class="visuallyhidden">Email</span><a href="#"></a></li>
                                        <li class="brief_share"><span class="visuallyhidden">Share</span><a href="#"></a></li>
                                    </ul>--%>
                                </div>
                                <!-- end buttons -->
                            </div>
                            <!-- end product-detail-text -->
                            <div class="omega grid_14 product-detail-slider">
                                <div class="large-slider flexslider" id="<%# Eval("hrefId") %>-large-slider">
                                    <ul class="slides">
                                        <asp:Repeater runat="server" ID="UxLargerSlider">
                                            <ItemTemplate>
                                                <li>
                                                    <img src="<%# Eval("imageSRC") %>" alt="placeholder" />
                                                    <div class="caption">
                                                        <h4>
                                                            <%# Eval("title") %></h4>
                                                            <%# Eval("tagline") %>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <!-- end large-slider -->
                                <div class="thumb-slider flexslider" id="<%# Eval("hrefId") %>-thumb-slider">
                                    <ul class="slides">
                                        <asp:Repeater runat="server" ID="UxThumbSlider">
                                            <ItemTemplate>
                                                <li>
                                                    <img src="<%# Eval("imageSRC") %>" />
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
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
    <!-- mmm Products & Services - Red Find Location mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Products & Services  - Find Location mmm -->
    <div class="red_find_location_wrapper  clearfix">
        <div class="red_find_location clearfix">
            <h2 class="headline">
                Speak with a Local Professional</h2>
            <div class="statement-text">
                <p>
                    Sir Speedy is here to help with all your Printing & Marketing Needs.
                </p>
            </div>
            <!-- .statement-text -->
            <div class="cta-button-wrap dark_bg">
                <a href="/find-locator/" class="cta-button-text"><span>Find a Location</span></a>
            </div>
            <!-- button -->
        </div>
        <!--end pred_find_location -->
    </div>
    <!-- end red_find_location_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Related Case Studies Wrapper mmmmmmmmmmmmmmmmmmm  Related / Case Studies Wrapper mmm -->
    <div class="our_portfolio_studies_wrapper  clearfix">
        <div class="our_portfolio_studies clearfix">
            <h2 class="headline">
                Case Studies</h2>
            <div class="our_portfolio_studies_inner no-repeat-slides">
                <div class="slider-wrapper">
                    <ul>
                        <asp:Repeater runat="server" ID="UxCaseStudiesSlider">
                        <ItemTemplate>
                            <li><a href="<%# Eval("hreftext") %>">
                            <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>"><span></span></span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                </div>
                <!-- #products-slider-wrapper -->

                <div id="case-studies-cta" class="cta-button-wrap purple">
                    <a class="cta-button-text" href="/case-studies/"><span>All Case Studies</span></a>
                </div>

            </div>
        </div>
        <!-- end our_portfolio_sudies -->
    </div>
    <!-- end our_portfolio_studies_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm footer upper mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer upper mmm -->
    <ux:PSIFooterUpper ID="PSFooterUpper" runat="server" />
    <!-- end upperfooter_wrapper-->
    <div class="clear">
    </div>
</asp:Content>
