<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers.aspx.cs" Inherits="briefs_whitepapers" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="briefs-whitepapers">
        <!-- mmm Header Image (both)  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image (both) mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="images/headers/header_1.jpg" alt=""> --%>
                    <CMS:ContentBlock ID="cbBriefWhitepapersHeaderImage" runat="server" DynamicParameter="id"
                        CacheInterval="300" />
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
            <div class="sub_navigation insights">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="insights-menu-h2">
                            <h2 id="menu-case-studies">
                                Case Studies</h2>
                            <h2 id="menu-briefs-whitepapers">
                                Briefs &amp; Whitepapers</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                                &nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us National -->
                        <ul id="insights-desktop-nav">
                            <li class="case-studies-link"><a href="/Case-Studies/">Case Studies</a></li>
                            <li class="briefs-whitepapers-link active"><a href="/Briefs-Whitepapers/">Briefs
                                &amp; Whitepapers</a></li>
                        </ul>
                    </div>
                </div>
                <!--end container_24-->
            </div>
            <!-- end sub_nav -->
        </div>
        <!-- end sub_nav_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Insights -- Briefs & Whitepapers Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Briefs & Whitepapers Content mmm -->
        <div class="briefs_whitepapers_content_wrapper insights clearfix" style="background-color: white;">
            <div class="briefs_whitepapers_content clearfix">
                <div id="briefs_wps_content" class="container_24">
                    <div class="grid_6 headline-block">
                        <div class="int-headline-block int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <CMS:ContentBlock ID="cbBriefWhitepapersSideContent" runat="server" />
                                    <%----%>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!-- headline block-->
                    <div class="grid_18 content-block">
                        <div class="grid_24">
                            <!-- end statement-text -->
                            <asp:Repeater runat="server" ID="UxBriefAndWhitepapers">
                                <ItemTemplate>
                                    <div class="prefix_1 grid_22 suffix_1 briefs_wps_article clearfix">
                                        <a href="<%# Eval("hrefText") %>"><img class="briefs_wps_img" src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>"></a>
                                        <div class="briefs_wps_details">
                                            <h3><a href="<%# Eval("hrefText") %>"><%# Eval("title") %></a></h3>
                                            <p> <%# Eval("abstract")%></p>
                                            <div class="briefs_wps_links">
                                                <a href="<%# Eval("hrefText") %>" class="cta-button-text">
                                                    <div class="cta-button-wrap black-btn">
                                                        <span>READ MORE</span>
                                                    </div>
                                                </a>
                                                <!-- end cta-button-wrap -->
                                            </div>
                                            <!-- end briefs_wps_links -->
                                        </div>
                                        <!-- briefs_wps_details -->
                                    </div>
                                    <!-- briefs_wps_article -->
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <!--end grid_24-->
                    </div>
                    <!-- end grid_18 -->
                </div>
                <!--end container_24-->
                <%--<div class="clear">
                </div>
                <div class="load_more_wrapper">
                    <div class="cta-button-wrap purple">
                        <a href="#" class="cta-button-text"><span>LOAD MORE</span></a>
                    </div>
                    <!-- end cta-button-wrap -->
                </div>--%>
                <!-- end load_more_wrapper -->
            </div>
            <!--end insights_case_studies -->
        </div>
        <!-- end insights_case_studies_wrapper -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
