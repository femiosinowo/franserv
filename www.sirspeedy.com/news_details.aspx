<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="news_details.aspx.cs" Inherits="news_details" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.blockquote').insertAfter('.newsDescription p:nth-child(3)');
        });
    </script>
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper split_header clearfix">
        <div class="subpage_tagline news">
            <div class="container_24">
                <div class="grid_24">
                    <!-- Shared - About Us - News  -->
                    <div id="news_tagline">
                        <CMS:ContentBlock ID="cbTagLine" runat="server" DefaultContentID="1195" CacheInterval="300" />
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
    <!-- mmm About - News Details mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-News Details mmm -->
    <div class="news_details_wrapper  clearfix">
        <div class="news_details_content main_content clearfix">
            <div class="bx-wrapper" id="faux-slider">
                <div class="bx-controls bx-has-controls-direction">
                    <div class="bx-controls-direction">
                        <asp:Repeater runat="server" ID="UxPrevNews">
                            <ItemTemplate>
                                <a href="<%#Eval("HrefText") %>" class="bx-prev">Prev</a>
                                <div class="prev_article">
                                    <a href="<%#Eval("HrefText") %>"><%#Eval("title") %></a>
                                </div>
                                <!-- end prev_article -->
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater runat="server" ID="UxNextNews">
                            <ItemTemplate>
                                <a href="<%#Eval("HrefText") %>" class="bx-next">Next </a>
                                <div class="next_article">
                                    <a href="<%#Eval("HrefText") %>"><%#Eval("title")%></a>
                                </div>
                                <!-- end next_article -->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- end bx-controls-direction -->
                </div>
                <!-- end bx-controls -->
            </div>
            <!-- end bx-wrapper faux-slider -->
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1 clearfix">
                    <asp:Repeater runat="server" ID="UxNewsDetails">
                        <ItemTemplate>
                            <h2 class="subpage">
                                <%#Eval("title") %>
                            </h2>
                            <script type="text/javascript">
                                function myPrintFunction() {
                                    window.print();
                                }
                            </script>
                            <div class="date_links bottom-divider">
                                <p class="gray">
                                    <%#Eval("date") %>
                                    |
                                    <%#Eval("city") %>,
                                    <%#Eval("state") %></p>
                                    
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
                                <%--<div class="email_print_share">
                                    <ul>
                                        <li><a href="#">
                                            <img src="/images/doc-print-white.png" alt="Print"></a></li>
                                        <li><a href="#">
                                            <img src="/images/doc-email-white.png" alt="Email"></a></li>
                                        <li><a href="#">
                                            <img src="/images/doc-share-white.png" alt="Share"></a></li>
                                    </ul>
                                </div>--%>
                            </div>
                            <!-- end date_links -->
                            <div class="clear">
                            </div>
                            <div class="grid_13 alpha news_article_content">
                                <p class="tagline_inner">
                                    <%#Eval("teaser") %></p>
                                <div class="newsDescription">
                                    <%#Eval("content1") %>
                                    <div class="blockquote"><%#Eval("pullquote") %></div>
                                    <%#Eval("content2") %>
                                </div>
                            </div>
                    <!-- end news_article_content -->
                    <div class="grid_9 prefix_2 omega news_article_img">
                        <%#Eval("imgTag") %>
                    </div>
                    </ItemTemplate>
                    </asp:Repeater>
                    <!-- end news_article_content -->
                    <div class="clear">
                    </div>
                    <div class="bottom-divider">
                        &nbsp;</div>
                    <div id="news_about_footer" class="grid_13 suffix_11">
                        <h3 class="red-text">
                            About Franchise Services</h3>
                        <CMS:ContentBlock ID="cbAboutFranchise" runat="server" DoInitFill="false" />
                    </div>
                    <!-- end news_about_footer -->
                </div>
                <!--end grid_22-->
            </div>
            <!--end container_24-->
        </div>
        <!--end news_details -->
    </div>
    <!-- end news_details_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
