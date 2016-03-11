<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="product_services_category.aspx.cs" Inherits="product_services_category" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/ProductsAndServicesCategoryInteriorMenu.ascx" TagPrefix="ux" TagName="PSCategoryInteriorMenu" %>
<%@ Register Src="~/UserControls/HomePageCaseStudies.ascx" TagPrefix="ux" TagName="CaseStudies" %>
<%@ Register Src="~/UserControls/HomePageBriefsAndWhitepapers.ascx" TagPrefix="ux" TagName="BriefAndWhitepapers" %>
<%@ Register Src="~/UserControls/HomePageNationalFooter.ascx" TagPrefix="ux" TagName="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="products-services">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="products_services_page_wrapper clearfix">
            <div class="products_services_page">
                <div class="header_image">
                    <CMS:ContentBlock ID="cbPSHeaderImage" runat="server" CacheInterval="300" />
                    <%-- <img src="images/headers/header_1.jpg" alt="Products &amp; Services"> --%>
                </div>
                <!-- header image-->
            </div>
            <!-- end sub_nav -->
        </div>
        <!-- end sub_nav_wrapper-->
        <!-- end main_tagline_wrapper -->
        <div class="clear">
        </div>
        <!-- mmm Interior Menu mmmmmmmmmmmmmmmmmmmmmmmm Interior Menu mmm -->
        <ux:PSCategoryInteriorMenu ID="PSCategoryInteriorMenu1" runat="server" />
        <!-- mmm Product Category Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm  Product Category Content  mmm -->
        <div class="products_services_page_content_wrapper clearfix">
            <div class="products_services_page_content products_services_page clearfix">
                <div class="container_24" id="products_services_category_content">
                    <asp:Repeater runat="server" ID="UxProdServDetail" OnItemDataBound="UxProdServDetail_ItemDataBound">
                        <ItemTemplate>
                            <div class="grid_6 headline-block col-height-equal">
                                <div class="int-headline-block int-block-1">
                                    <div class="headline-content-outer">
                                        <div class="headline-content-inner">
                                            <span class="headline-block-icon-black"></span>
                                            <h2 class="headline">
                                                <%# Eval("Title") %></h2>
                                            <p>
                                                <%# Eval("pageDescription")%>
                                            </p>
                                            <script type="text/javascript">
                                                function myPrintFunction() {
                                                    window.print();
                                                }
                                            </script>
                                            <div class="products_services_links">
                                                <ul class="ps-btns">
                                                    <li class="print-btn"><a onclick="myPrintFunction();"><span>Print</span></a></li>
                                                    <li class="email-btn"><a><span class="st_email"></span></a></li>
                                                    <li class="share-btn"><a><span st_image="<%=this.MainImagePath%>" class="st_sharethis_custom">Share</span></a></li>                                                    
                                                </ul>
                                            </div>
                                            <!-- end products_services_links -->
                                        </div>
                                        <!--headline content-->
                                    </div>
                                    <!--headline content-->
                                </div>
                                <div class="int-headline-block tagline-block int-block-dgrey">
                                    <%# Eval("tagline") %> <%--<h4></h4>--%>
                                </div>
                            </div>
                            <div class="grid_18 content-block col-height-equal">
                                <div class="grid_10 prefix_1 suffix_1">
                                    <h4 class="sub">
                                        <%# Eval("subtitle") %></h4>
                                    <%# Eval("content") %>
                                    <h4 class="sub">All the Latest Related Marketing Insight from MarketingTango</h4>
                                    <ul class="red_arrows">
                                        <asp:Repeater runat="server" ID="uxBlogList">
                                            <ItemTemplate>
                                                <li><a style="color: #0D85DB;" href="<%#Eval("MoreLink")%>">
                                                    <%#Eval("Title")%></a></li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class=" grid_12 clearfix">
                                    <div class="products_category_inner_slider">
                                        <div class="flexslider inner-border">
                                            <ul class="slides">
                                                <asp:Repeater runat="server" ID="uxSubCategorySlider">
                                                    <ItemTemplate>
                                                        <li>
                                                            <img src="<%# Eval("imageSRC") %>" alt="placeholder" />
                                                            <div class="caption">
                                                                <h4><%# Eval("title") %></h4>
                                                                <%# Eval("LearnMoreButton")%>
                                                            </div>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="clear"></div>
                                    <div id="product_sub_links">
                                        <asp:Repeater runat="server" ID="UxSubProductLinks">
                                            <ItemTemplate>
                                                <div class="grid_8 <%#Eval("cssClass") %>">
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
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!-- grid 12-->
            </div>
            <!-- products_services_category_content-->
        </div>
        <!--end products_services_page_content -->
    </div>
    <!-- end products_services_page_content_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Products & Services - Find A Location mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Products & Services  - Find A Location mmm -->
    <div class="sub_find_location_wrapper  clearfix" id ="findAlocationDiv" runat="server">
        <div class="sub_find_location clearfix">
            <section id="sub_find_location" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                              <a href="/Find-Locator/">  <h2 class="headline">Find A<br />
                                    Location</h2></a>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div id="findLocationSection" runat="server" class="grid_18 content-block col-height-equal">
                    <div class="sub_find_location_content">
                        <CMS:ContentBlock ID="cbFindLocation" runat="server" DoInitFill="false" />                       
                        <!-- end cta-button-wrap -->
                    </div>
                </div>
                <!--grid_18-->
            </section>
        </div>
        <!-- end find_locations -->
    </div>
    <!-- end find_locations_wrapper  -->
    <div class="clear">
    </div>
    <!-- mmm Our Portfolio / Case Studies Wrapper mmmmmmmmmmmmmmmmmmm  Our Portfolio / Case Studies Wrapper mmm -->
    <ux:CaseStudies ID="uxCaseStudies" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <!-- mmm BriefAndWhitepapers Wrapper mmmmmmmmmmmmmmmmmmm  BriefAndWhitepapers Wrapper mmm -->
    <ux:BriefAndWhitepapers ID="uxBriefAndWhitepapers" runat="server" />
    <div class="clear">
    </div>
    <!-- mmm footer upper mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer upper mmm -->
    <ux:Blog ID="uxNationalFooter" runat="server" />
</asp:Content>
