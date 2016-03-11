<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="news_details.aspx.cs" Inherits="news_details" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_1.jpg" alt=""> --%>
                <CMS:ContentBlock ID="cbNewsHeaderImage" runat="server" />
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
    <!-- mmm About - News Details mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-News Details mmm -->
    <div class="news_content_wrapper main_about_us clearfix">
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
                        <ul class="news-direction-nav">
                            <li>
                                <div class="news-prev">
                                    <span>Previous</span>
                                    <%--<div class="news-next-article">
                                        <span>Sir Speedy Maintains Good Standing in Industry Rankings</span>
                                    </div>--%>
                                </div>
                            </li>
                            <li>
                                <div class="news-next">
                                    <span>Next</span>
                                    <%--<div class="news-next-article">
                                        <span>Sir Speedy Maintains Good Standing in Industry Rankings</span>
                                    </div>--%>
                                </div>
                            </li>
                        </ul>
                        <div class="clear"></div>
                        <div class="news_article_slider">
                            <asp:Repeater runat="server" ID="UxNews">
                                <ItemTemplate>
                                    <div class="news_article_wrapper">
                                        <div class="prefix_1 grid_22 suffix_1 news_article clearfix">
                                            <h4 class="news_date">
                                                <%#Eval("date") %>
                                                    |
                                                    <%#Eval("city") %>,
                                                    <%#Eval("state") %></h4>
                                            <h3 class="article_title">
                                                <%#Eval("title") %></h3>
                                            <div class="news_links">
                                                <script type="text/javascript">
                                                    function myPrintFunction() {
                                                        window.print();
                                                    }
                                            </script>
                                            <ul class="ps-btns">
                                                <li class="print-btn"><a onclick="myPrintFunction();" href="javascript:void('0')"><span>Print</span></a></li>
                                                <li class="email-btn"><a href="javascript:void('0')"><span class="st_email"></span></a></li>
                                                <li class="share-btn"><a href="javascript:void('0')"><span class="st_sharethis_custom">Share</span></a></li>
                                            </ul>
                                            </div>
                                            <!-- end news_links -->
                                            <div class="clear">
                                            </div>
                                            <div class="news_details">
                                                <div class="grid_16 news_text">
                                                    <h4>
                                                        <%#Eval("teaser") %></h4>
                                                    <div>
                                                        <%#Eval("content1") %>
                                                        <div class="pull_quote">
                                                            <%#Eval("pullquote") %>
                                                        </div>
                                                        <%#Eval("content2") %>
                                                    </div>                                                    
                                                </div>
                                                <!-- news_text-->
                                                <div class="grid_8 news_image">
                                                    <%#Eval("imgTag") %>
                                                </div>
                                                <!-- news_image-->
                                            </div>
                                            <!-- news_details -->
                                        </div>
                                        <!-- news_article -->
                                    </div>
                                    <!-- news_article_wrapper -->
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="clear"></div>
                        <div class="about_bottom_wrapper">
                            <p>
                                <strong>About Franchise Services</strong><br />
                                <CMS:ContentBlock ID="cbAboutFranchise" runat="server" DoInitFill="false" />
                        </div>
                        <!-- end about_bottom_wrapper -->
                    </div>
                    <!-- end grid 24 -->
                </div>
                <!-- end grid 18 -->
            </div>
            <!-- end container 24 -->
        </div>
        <!-- end news content -->
    </div>
    <!-- end news content wrapper -->
    <!-- end news_details_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
