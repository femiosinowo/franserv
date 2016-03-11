<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="product_services_subcategory.aspx.cs" Inherits="product_services_subcategory" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/ProductAndServicesSubCategoryMobileSubNav.ascx" TagPrefix="ux" TagName="PSSubCategoryMobileSubNav" %>
<%@ Register Src="~/UserControls/ProductAndServicesCategorySmallIconSlider.ascx" TagPrefix="ux" TagName="PSSubIconSlider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper clearfix">
        <div class="subpage_tagline products_category_details book_printing">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Products & Services: Printing Page -->
                    <h2 id="products_category_printing_tagline">
                       <asp:Repeater runat="server" ID="UxHeaderTagline">
                        <ItemTemplate>
                            <%#Eval("title") %><span> <%#Eval("pageDescription")%></span>
                        </ItemTemplate>
                    </asp:Repeater>
                    </h2>
                </div>
                <!--end prefix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
     <!-- mmm Sub Nav (Mobile Only) mmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (Mobile only) mmm -->
    <ux:PSSubCategoryMobileSubNav ID="uxPSCategoryMobileSubNav" runat="server" />
    <!-- mmm Slider (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Slider mmm -->
    <ux:PSSubIconSlider ID="UxPSIconSlider" runat="server" />
    <!-- mmm Product Category Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm  Product Category Content  mmm -->
    <div class="category_content_wrapper clearfix">
        <div class="category_content main_content clearfix">
            <div class="container_24">
                <asp:Repeater runat="server" ID="UxProdServSubCategoryDetail" OnItemDataBound="UxProdServSubCategoryDetail_ItemDataBound">
                    <ItemTemplate>
                        <div class="grid_24">
                            <div class="grid_24">
                                <script type="text/javascript">
                                    function myPrintFunction() {
                                        window.print();
                                    }
                                </script>
                                <div class="email_print_share white">
                                    <ul>
                                        <li><a onclick="myPrintFunction();">
                                            <img alt="Print" src="/images/doc-print-white.png"></a></li>
                                        <li><span class='st_email'></span><%--
                                            <asp:LinkButton runat="server" ID="email_btn" OnClick="EmailBtn_Click">
                                            <img alt="Email" src="/images/doc-email-white.png"></asp:LinkButton>--%></li>
                                        <li><span class="st_sharethis_custom"></span><%--<a href="#">
                                            <img alt="Share" src="/images/doc-share-white.png"></a>--%></li>
                                    </ul>
                                </div>
                                <%--<div class="email_print_share white">
                                   <ul>
                                        <li><a href="#"><img alt="Print" src="/images/doc-print-white.png"></a></li>
                                        <li><a href="#"><img alt="Email" src="/images/doc-email-white.png"></a></li>
                                        <li><a href="#"><img alt="Share" src="/images/doc-share-white.png"></a></li>
                                    </ul>
                                </div>--%>
                                <!-- end email_print_share -->
                                <h2 class="red-text">
                                    <%# Eval("subtitle") %></h2>
                            </div>
                            <!-- end grid_24 -->
                            <div class="grid_11 main_content aplha">
                                <p class="tagline_inner">
                                    <%# Eval("teaser") %></p>
                                <div id="PSSubcategoryContent">
                                    <%# Eval("content") %></div>
                                <div class="bottom-divider">
                                    &nbsp;
                                </div>
                                <!-- end bottom-divider -->
                               <h4>
                                    All the Latest Related Marketing Insight from MarketingTango</h4>
                                <ul class="red_arrows">
                                    <asp:Repeater runat="server" ID="uxBlogList">
                                        <ItemTemplate>
                                            <li><a style="color:#DF1B23;" href="<%#Eval("MoreLink")%>"><%#Eval("Title")%></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <!-- end grid_10 -->
                            <div class="grid_12 prefix_1 omega">
                                <div class="products_category_img_wrapper inner-border">
                                    <img src="<%# Eval("imageSRC") %>" alt="placeholder" />
                                </div>
                                <!-- end products_category_img_wrapper -->
                                <div class="statement-text">
                                    <p><%# Eval("tagline") %></p>
                                </div>
                                <!-- end statement-text -->
                                <%--<div class="cta-button-wrap purple">
                                    <a class="cta-button-text special" href="#"><span>Get started today! CONTACT US</span></a>
                                </div>--%>
                                <div class="bottom-divider">
                                    &nbsp;
                                </div>
                                <div id="product_sub_links">
                                    <asp:Repeater runat="server" ID="UxProductSubLinksTitle">
                                        <ItemTemplate>
                                            <h3> <%# Eval("title") %> Services</h3>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     <asp:Repeater runat="server" ID="UxSubProductLinks">
                                        <ItemTemplate>
                                            <div class="grid_8">
                                                <div class="img_wrapper">
                                                    <a href="<%#Eval("url") %>">
                                                        <img src="<%#Eval("imageSRC") %>" alt="<%#Eval("title") %>" />
                                                        <p>
                                                            <%#Eval("title") %>
                                                        </p>
                                                    </a>
                                                </div>
                                                <!-- end img_wrapper -->
                                            </div>
                                            <!-- end grid_8 -->
                                            <%#Eval("clearDiv") %>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <!-- end product_sub_links -->
                            </div>
                            <!-- end grid_12 -->
                        </div>
                        <!--end grid_24-->
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!--end container_24-->
        </div>
        <!--end acategory_content -->
    </div>
    <!-- end acategory_content wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Related Case Studies Wrapper mmmmmmmmmmmmmmmmmmm  Related / Case Studies Wrapper mmm -->
    <div class="our_portfolio_studies_wrapper  clearfix">
        <div class="our_portfolio_studies clearfix">
            <h2 class="headline">Case Studies</h2>
            <section class="our_portfolio_studies_inner no-repeat-slides">
                <!-- <h2 class="headline">Related Case Studies</h2> -->
                <div class="slider-wrapper">
                    <ul>
                         <asp:Repeater runat="server" ID="uxCaseStudiesSlider">
                            <ItemTemplate>
                                <li><a href="<%#Eval("hreftext") %>">
                                <img src="<%#Eval("imgSRC") %>" alt="<%#Eval("title") %>"></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!-- #products-slider-wrapper -->
                <div id="case-studies-cta" class="cta-button-wrap purple">
                    <a class="cta-button-text" href="/case-studies/"><span>All Case Studies</span></a>
                </div>

            </section>
        </div>
        <!-- end our_portfolio_sudies -->
    </div>
    <!-- end our_portfolio_studies_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
