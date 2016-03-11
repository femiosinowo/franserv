<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="in_the_media.aspx.cs" Inherits="in_the_media" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
     <!--//************************************************************************/
     //In the Media - MosaicFlow
     //************************************************************************/ -->
    <!-- InTheMedia pinterest columns plugin -->
    <script src="/js/jquery.mosaicflow.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            if ($('#media_container').length > 0) {
                $('#media_container').mosaicflow({
                    itemSelector: '.media_article_wrapper',
                    minItemWidth: 250
                });
            }
        });
    </script>
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_1.jpg" alt="">--%>
                <CMS:ContentBlock ID="cbMediaHeaderImage" runat="server" DynamicParameter="id" CacheInterval="300" />
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
                    <div id="desktopNavNational" runat="server">
                        <ul id="about-desktop-nav">
                             <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                        <li class="mgmt-team-link"><a href="/company-info/management-team/">Management Team</a></li>
                        <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                        <li class="history-link"><a href="/company-info/history/">History</a></li>
                        <li class="news-link"><a href="/company-info/news/">News</a></li>
                        <li class="media-link active"><a href="/company-info/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                    <div id="desktopNavLocal" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link"><a href="/company-info/">About PIP</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link"><a href="/news/">News</a></li>
                            <li class="local-media-link active"><a href="/in-the-media/">In the Media</a></li>
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
    <!-- mmm Media Articles mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Media Articles mmm -->
    <div class="media_content_wrapper main_about_us clearfix" style="background-color: white;">
        <%--media_wrapper  clearfix--%>
        <div class="media_content clearfix">
            <%--main_content media clearfix--%>
            <div class="container_24" id="media_content">
                <div class="grid_6 headline-block">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbMediaSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block">
                    <div class="grid_24" id="media_container">
<!-- mmm Media Articles mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Media Articles mmm -->                        
                       <div class="clearfix mosaicflow" data-item-selector=".media_article_wrapper" data-min-item-width="250">
                            <!-- ccccccccccccccccccccccc COLUMN 1 ccccccccccccccccccccccc -->
                            <div class="media_col grid_6 alpha">
                                <asp:Repeater runat="server" ID="uxMediaWrapperColumnOne">
                                    <ItemTemplate>
 <!----------------------------- Start Media Article Here ----------------------------->
                                    <div class="media_article_wrapper">
                                            <div class="media_article <%# Eval("logoCss") %>">
                                                <div class="logo_wrapper">
                                                    <%# Eval("logoImage") %>
                                                </div>

                                                <p class="source">
                                                    <%# Eval("source") %><br />
                                                    <span class="media_date"><%# Eval("date") %></span>
                                                </p>

                                                <h3><%# Eval("title") %></h3>

                                                <a href="<%# Eval("url") %>" class="cta-button-text">
                                                    <div class="cta-button-wrap black-btn">
                                                        <span>More</span>
                                                    </div>
                                                </a>
                                                <!-- end cta-button-wrap -->
                                            </div>
                                        <!-- media_article -->
                                    </div>
                                    <!-- media_article_wrapper -->
                                    <!----------------------------- End Media Article Here ----------------------------->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- end media_col -->
                            <!-- ccccccccccccccccccccccc COLUMN 2 ccccccccccccccccccccccc -->
                            <div class="media_col grid_6">
                                <asp:Repeater runat="server" ID="uxMediaWrapperColumnTwo">
                                    <ItemTemplate>
 <!----------------------------- Start Media Article Here ----------------------------->
                                    <div class="media_article_wrapper">
                                            <div class="media_article <%# Eval("logoCss") %>">
                                                <div class="logo_wrapper">
                                                    <%# Eval("logoImage") %>
                                                </div>

                                                <p class="source">
                                                    <%# Eval("source") %><br />
                                                    <span class="media_date"><%# Eval("date") %></span>
                                                </p>

                                                <h3><%# Eval("title") %></h3>

                                                <a href="<%# Eval("url") %>" class="cta-button-text">
                                                    <div class="cta-button-wrap black-btn">
                                                        <span>More</span>
                                                    </div>
                                                </a>
                                                <!-- end cta-button-wrap -->
                                            </div>
                                        <!-- media_article -->
                                    </div>
                                    <!-- media_article_wrapper -->
                                    <!----------------------------- End Media Article Here ----------------------------->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- end media_col -->
                            <!-- ccccccccccccccccccccccc COLUMN 3 ccccccccccccccccccccccc -->
                            <div class="media_col grid_6">
                                <asp:Repeater runat="server" ID="uxMediaWrapperColumnThree">
                                    <ItemTemplate>
 <!----------------------------- Start Media Article Here ----------------------------->
                                    <div class="media_article_wrapper">
                                            <div class="media_article <%# Eval("logoCss") %>">
                                                <div class="logo_wrapper">
                                                    <%# Eval("logoImage") %>
                                                </div>

                                                <p class="source">
                                                    <%# Eval("source") %><br />
                                                    <span class="media_date"><%# Eval("date") %></span>
                                                </p>

                                                <h3><%# Eval("title") %></h3>

                                                <a href="<%# Eval("url") %>" class="cta-button-text">
                                                    <div class="cta-button-wrap black-btn">
                                                        <span>More</span>
                                                    </div>
                                                </a>
                                                <!-- end cta-button-wrap -->
                                            </div>
                                        <!-- media_article -->
                                    </div>
                                    <!-- media_article_wrapper -->
                                    <!----------------------------- End Media Article Here ----------------------------->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- end media_col -->
                            <!-- ccccccccccccccccccccccc COLUMN 4 ccccccccccccccccccccccc -->
                            <div class="media_col grid_6 omega">
                                <asp:Repeater runat="server" ID="uxMediaWrapperColumnFour">
                                    <ItemTemplate>
 <!----------------------------- Start Media Article Here ----------------------------->
                                    <div class="media_article_wrapper">
                                            <div class="media_article <%# Eval("logoCss") %>">
                                                <div class="logo_wrapper">
                                                    <%# Eval("logoImage") %>
                                                </div>

                                                <p class="source">
                                                    <%# Eval("source") %><br />
                                                    <span class="media_date"><%# Eval("date") %></span>
                                                </p>

                                                <h3><%# Eval("title") %></h3>

                                                <a href="<%# Eval("url") %>" class="cta-button-text">
                                                    <div class="cta-button-wrap black-btn">
                                                        <span>More</span>
                                                    </div>
                                                </a>
                                                <!-- end cta-button-wrap -->
                                            </div>
                                        <!-- media_article -->
                                    </div>
                                    <!-- media_article_wrapper -->
                                    <!----------------------------- End Media Article Here ----------------------------->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <!-- end media_col -->
                        </div>
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end grid_18 -->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end media_articles -->
    </div>
    <!-- end media_articles_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
