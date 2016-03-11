<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="news.aspx.cs" Inherits="news" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper split_header  clearfix">
        <div class="subpage_tagline news">
            <div class="container_24">
                <div class="grid_24">
                    <!-- Shared - About Us - News  -->
                    <div id="news_tagline">
                        <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                        <!-- end media_inquiries -->
                    </div>
                    <!-- end split_header -->
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
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation about-us">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
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
                    <div id="desktopNavLocal" class="sub_navigation about-us-local" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link"><a href="/company-info/">Company Info</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link active"><a href="/news/">News</a></li>
                            <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
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
    <!-- mmm About -News mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-News mmm -->
    <div class="news_wrapper  clearfix">
        <div class="main_content news clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="statement-text bottom-divider">
                        <CMS:ContentBlock ID="cbNewsTagline" runat="server" DoInitFill="false" />
                    </div>
                    <!-- mmmmmmmmmmmmmmmmmmmm NEWS ARTICLE mmmmmmmmmmmmmmmmmmmm -->
                    <asp:Repeater runat="server" ID="UxNewsArticle">
                        <ItemTemplate>
                            <div class="prefix_1 grid_22 suffix_1 news_article bottom-divider  clearfix">
                                <h3>
                                    <a href="<%# Eval("hrefText") %>">
                                        <%# Eval("title") %></a></h3>
                                <div class="news_details">
                                    <%#Eval("imgTag") %>
                                    <h4>
                                        <%# Eval("date") %>
                                        |
                                        <%# Eval("city") %>,
                                        <%# Eval("state") %></h4>
                                    <p>
                                        <%# Eval("teaser") %>
                                    </p>
                                    <div class="email_print_share">
                                        <div class="cta-button-wrap purple">
                                            <a href="<%# Eval("hrefText") %>" class="cta-button-text"><span>READ MORE</span></a>
                                        </div>
                                        <!-- end cta-button-wrap -->
                                        <%--<ul>
                                            <li><a href="#">
                                                <img alt="Print" src="/images/doc-print-white.png"></a></li>
                                            <li><a href="#">
                                                <img alt="Email" src="/images/doc-email-white.png"></a></li>
                                            <li><a href="#">
                                                <img alt="Share" src="/images/doc-share-white.png"></a></li>
                                        </ul>--%>
                                    </div>
                                    <!-- end email_print_share -->
                                </div>
                                <!-- news_details -->
                            </div>
                            <!-- news_article -->
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
            <div class="clear">
            </div>
            <div class="load_more_wrapper" id="loadMoreContent" runat="server" visible="false">
                <div class="cta-button-wrap purple">
                    <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
                    <asp:LinkButton runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
                </div>
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
