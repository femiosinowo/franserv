<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="in_the_media.aspx.cs" Inherits="in_the_media" %>

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
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper split_header clearfix">
        <div class="subpage_tagline media">
            <div class="container_24">
                <div class="grid_24">
                    <!-- Shared - In the Media  -->
                    <div id="media_tagline">
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
    <div class="sub_navigation_wrapper  clearfix">
        <div class="sub_navigation about-us">
            <div class="container_24">
                <div class="grid_24">
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
                        <%--<li class="history-link"><a href="/company-info/history/">History</a></li>--%>
                        <li class="news-link"><a href="/company-info/news/">News</a></li>
                        <li class="media-link active"><a href="/company-info/in-the-media/">In the Media</a></li>
                        </ul>
                    </div>
                    <div id="desktopNavLocal" class="sub_navigation about-us-local" visible="false" runat="server">
                        <ul id="about-local-desktop-nav">
                            <li class="about-local-link"><a href="/company-info/">Company Info</a></li>
                            <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                            <li class="testimonials-link"><a href="/testimonials/">Testimonials</a></li>
                            <li class="local-news-link"><a href="/news/">News</a></li>
                            <li class="local-media-link active"><a href="/in-the-media/">In the Media</a></li>
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
    <!-- mmm Media Articles mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Media Articles mmm -->
    <div class="media_wrapper  clearfix">
        <div class="main_content media clearfix">
            <div class="container_24">
                <div class="grid_24" id="media_container">
                    <div class="clearfix mosaicflow" >
                        <!-- ccccccccccccccccccccccc COLUMN 1 ccccccccccccccccccccccc -->
                        <div class="media_col grid_6 alpha">
                            <asp:Repeater runat="server" ID="uxMediaWrapperColumnOne">
                                <ItemTemplate>
                                    <div class="media_article_wrapper">
                                        <div class="media_article <%# Eval("logoCss") %>">
                                            <%# Eval("logoImage") %>
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <div class="cta-button-wrap gold small">
                                                <a href="<%# Eval("url") %>" class="cta-button-text" target="_blank"><span>Read More</span></a>
                                            </div>
                                            <!-- end cta-button-wrap -->
                                            <p class="source">
                                                <%# Eval("source") %><br />
                                                <%# Eval("date") %>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- end media_col -->
                        <!-- ccccccccccccccccccccccc COLUMN 2 ccccccccccccccccccccccc -->
                        <div class="media_col grid_6">
                            <asp:Repeater runat="server" ID="uxMediaWrapperColumnTwo">
                                <ItemTemplate>
                                    <div class="media_article_wrapper">
                                        <div class="media_article <%# Eval("logoCss") %>">
                                            <%# Eval("logoImage") %>
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <div class="cta-button-wrap gold small">
                                                <a href="<%# Eval("url") %>" class="cta-button-text" target="_blank"><span>Read More</span></a>
                                            </div>
                                            <!-- end cta-button-wrap -->
                                            <p class="source">
                                                <%# Eval("source") %><br />
                                                <%# Eval("date") %>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- end media_col -->
                        <!-- ccccccccccccccccccccccc COLUMN 3 ccccccccccccccccccccccc -->
                        <div class="media_col grid_6">
                            <asp:Repeater runat="server" ID="uxMediaWrapperColumnThree">
                                <ItemTemplate>
                                    <div class="media_article_wrapper">
                                        <div class="media_article <%# Eval("logoCss") %>">
                                            <%# Eval("logoImage") %>
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <div class="cta-button-wrap gold small">
                                                <a href="<%# Eval("url") %>" class="cta-button-text" target="_blank"><span>Read More</span></a>
                                            </div>
                                            <!-- end cta-button-wrap -->
                                            <p class="source">
                                                <%# Eval("source") %><br />
                                                <%# Eval("date") %>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- end media_col -->
                        <!-- ccccccccccccccccccccccc COLUMN 4 ccccccccccccccccccccccc -->
                        <div class="media_col grid_6 omega">
                            <asp:Repeater runat="server" ID="uxMediaWrapperColumnFour">
                                <ItemTemplate>
                                    <div class="media_article_wrapper">
                                        <div class="media_article <%# Eval("logoCss") %>">
                                            <%# Eval("logoImage") %>
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <div class="cta-button-wrap gold small">
                                                <a href="<%# Eval("url") %>" class="cta-button-text" target="_blank"><span>Read More</span></a>
                                            </div>
                                            <!-- end cta-button-wrap -->
                                            <p class="source">
                                                <%# Eval("source") %><br />
                                                <%# Eval("date") %>
                                            </p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!-- end media_col -->
                    </div>
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end media_articles -->
    </div>
    <!-- end media_articles_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
