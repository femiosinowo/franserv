<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="case_studies.aspx.cs" Inherits="case_studies" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
<div id="case-studies">
    <!-- mmm Header Image (both)  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image (both) mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <%--<img src="images/headers/header_1.jpg" alt=""> --%>
                <CMS:ContentBlock ID="cbCaseStudiesHeaderImage" runat="server" DynamicParameter="id"
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
                        <li class="case-studies-link active"><a href="/case_studies.aspx">Case Studies</a></li>
                        <li class="briefs-whitepapers-link"><a href="/briefs_whitepapers.aspx">Briefs &amp;
                            Whitepapers</a></li>
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
    <!-- mmm Insights -- Case Studies mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Case Studies mmm -->
    <div class="case_studies_content_wrapper main_about_us clearfix" style="background-color: white;">
        <div class="case_studies_content clearfix">
            <div class="container_24" id="case_studies_content">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbCaseStudiesSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!-- headline block-->
                <div class="grid_18 content-block col-height-equal" >
                    <div class="grid_24" id="case_studies_main">
                        <asp:ListView ID="lvCaseStudiesPhotos" runat="server">
                            <%--GroupItemCount="4"--%>
                            <%--<LayoutTemplate>
                            <div class="portfolio-images">
                                <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                            </div>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </GroupTemplate>--%>
                            <ItemTemplate>
                                <div class="grid_8 cs_container <%# Eval("cssClass") %>">
                                    <a class="cs_image" href="<%# Eval("hreftext") %>">
                                        <img alt="<%# Eval("title") %>" src="<%# Eval("imgSRC") %>" />
                                    </a>
                                    <!-- end cs_image_01 -->
                                    <div class="cs_image_content_wrapper">
                                        <div class="cs_image_content">
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <p>
                                                <%# Eval("desc") %></p>
                                            <div class="cta-button-wrap white">
                                                <a href="<%# Eval("hreftext") %>" class="cta-button-text view-more view-more"><span>
                                                    View Case Study</span></a>
                                            </div>
                                        </div>
                                        <!-- end cs_image_content -->
                                    </div>
                                    <!-- end cs_image_01_content_wrapper -->
                                </div>
                                <!-- grid_6 -->
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!--end grid_24-->
                </div>
                <!--end grid_18-->
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
