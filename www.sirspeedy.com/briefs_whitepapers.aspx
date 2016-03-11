<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers.aspx.cs" Inherits="briefs_whitepapers" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline briefs_wp">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Briefs & Whitepapers -->
                     <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />                    
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
        <div class="sub_navigation briefs_wp">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- <div class="desktop-nav"> -->
                    <!-- About Us National -->
                    <ul id="insights-desktop-nav">
                        <li class="case-studies-link"><a href="/case-studies/">Case Studies</a></li>
                        <li class="briefs-wp-link active"><a href="/briefs-whitepapers/">Briefs &amp; Whitepapers</a></li>
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
    <!-- mmm Insights -- Briefs & Whitepapers Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Briefs & Whitepapers Content mmm -->
    <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix briefs_wp">
            <div class="container_24">
                <div class="grid_24">
                    <div class="statement-text" id="briefs">
                        <p>
                            Find out how your marketing &mdash; through your website, social media channels,
                            and beyond &mdash; stack up as <span>an entire marketing machine.</span></p>
                    </div>
                    <!-- end statement-text -->
                    <asp:Repeater runat="server" ID="UxBriefAndWhitepapers">
                        <ItemTemplate>
                            <div class="grid_24 brief_wrapper clearfix">
                                <div class="grid_6 brief_img alpha">
                                    <a href="<%# Eval("hrefText") %>">
                                        <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>"></a>
                                </div>
                                <!-- end brief_img -->
                                <div class="grid_17 prefix_1 omega brief_details">
                                    <h3>
                                        <%# Eval("title") %></h3>
                                    <p>
                                        <%# Eval("abstract")%></p>
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
                                    <!-- end email_print_share white -->
                                </div>
                                <!-- grid_17 prefix_1 brief_details -->
                            </div>
                            <!-- grid_24 brief_wrapper -->
                        </ItemTemplate>
                    </asp:Repeater>
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
