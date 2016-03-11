<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="news.aspx.cs" Inherits="news" %>

<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_1.jpg" alt=""> --%>
                <CMS:ContentBlock ID="cbNewsHeaderImage" runat="server" DynamicParameter="id" CacheInterval="300" />
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
        <div class="sub_navigation about-us about-us-local">
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
                    <div id="desktopNavNational" runat="server">
                        <ul id="about-desktop-nav">
                             <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                            <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                            <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                            <li class="history-link"><a href="/company-info/history/">History</a></li>
                            <li class="news-link active"><a href="/company-info/news/">News</a></li>
                            <li class="media-link"><a href="/company-info/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                    <div id="desktopNavLocal" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link"><a href="/company-info/">Company Info</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link active"><a href="/news/">News</a></li>
                            <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm About -News mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-News mmm -->
    <div class="news_content_wrapper main_about_us clearfix" style="background-color: white;">
        <div class="news_content clearfix">
            <div class="container_24" id="news_content">
                <div class="grid_6 headline-block">
                    <div class="int-headline-block int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbNewsSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                    <div class="int-headline-block int-block-blue-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">Media<br />
                                    Inquires</h2>
                                <CMS:ContentBlock ID="cbMediaInquires" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!-- grid_6 -->
                <div class="grid_18 content-block">
                    <div class="grid_24">
                        <!-- mmmmmmmmmmmmmmmmmmmm NEWS ARTICLE mmmmmmmmmmmmmmmmmmmm -->
                        <asp:Repeater runat="server" ID="UxNewsArticle">
                            <ItemTemplate>
                                <div class="prefix_1 grid_22 suffix_1 news_article clearfix">
                                    <h4 class="news_date">
                                        <%# Eval("date") %>
                                            |
                                            <%# Eval("city") %>,
                                            <%# Eval("state") %></h4>
                                    <h3>
                                        <a href="<%# Eval("hrefText") %>">
                                            <%# Eval("title") %></a></h3>
                                    <div class="news_details">
                                        <%#Eval("imgTag") %>
                                        <p>
                                            <%# Eval("teaser") %>
                                        </p>
                                        <div class="news_links">
                                            <a href="<%# Eval("hrefText") %>" class="cta-button-text">
                                                <div class="cta-button-wrap black-btn">
                                                    <span>READ MORE</span>
                                                </div>
                                            </a>
                                            <!-- end cta-button-wrap -->
                                        </div>
                                        <!-- end news_links -->
                                    </div>
                                    <!-- news_details -->
                                </div>
                                <!-- news_article -->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!--end grid_24-->
                </div>
                <!-- end grid_18 -->
            </div>
            <!--end container_24-->
            <div class="clear">
            </div>
            <div class="int_load_more_wrapper" id="loadMoreContent" runat="server" visible="false">
                <asp:LinkButton ID="LinkButton1" runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><div class="cta-button-wrap white-orange-btn">
                        <span>LOAD MORE</span>
                    </div></asp:LinkButton>
                <!-- end cta-button-wrap -->
            </div>
            <!-- end load_more_wrapper -->
        </div>
        <!--end news -->
    </div>
    <!-- end news_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
