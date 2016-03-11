<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="case_studies.aspx.cs" Inherits="case_studies" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline case_studies">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Case Studies -->
                   <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation case_studies">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Insights -->
                    <ul id="insights-desktop-nav">
                        <li class="case-studies-link active"><a href="/case-studies/">Case Studies</a></li>
                        <li class="briefs-wp-link"><a href="/briefs-whitepapers/">Briefs &amp; Whitepapers</a></li>
                    </ul>
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
    <!-- mmm Insights -- Case Studies mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Case Studies mmm -->
    <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <asp:ListView ID="lvCaseStudiesPhotos" runat="server" > <%--GroupItemCount="4"--%>
                        <%--<LayoutTemplate>
                            <div class="portfolio-images">
                                <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                            </div>
                        </LayoutTemplate>
                        <GroupTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </GroupTemplate>--%>
                        <ItemTemplate>
                            <div class="grid_6 cs_container <%# Eval("cssClass") %>">
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
            <!--end container_24-->
            <div class="clear">
            </div>
            <div class="load_more_wrapper">
                <div class="cta-button-wrap purple">
                    <a href="#" class="cta-button-text"><span>LOAD MORE</span></a>
                </div>
                <!-- end cta-button-wrap -->
            </div>
            <!-- end load_more_wrapper -->
        </div>
        <!--end insights_case_studies -->
    </div>
    <!-- end insights_case_studies_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
