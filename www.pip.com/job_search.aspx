<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="job_search.aspx.cs" Inherits="job_search" %>
<%@ Register Src="~/UserControls/JobSearch.ascx" TagPrefix="ux" TagName="JobSearch" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div id="job-search">
    <div class="clear"></div>
   <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="images/headers/header_1.jpg" alt="">--%>
                    <cms:ContentBlock ID="cbJobSearchHeaderImage" runat="server" DynamicParameter="id"
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
            <div class="sub_navigation join-team">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="join-menu-h2">
                            <h2 id="menu-job-pip">
                                Jobs at PIP</h2>
                            <h2 id="menu-job-profiles">
                                Job Profiles</h2>
                            <h2 id="menu-job-search">
                                Job Search</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                                &nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us National -->
                        <ul id="join-our-team-desktop-nav">
                            <li class="job-pip-link"><a href="/join-our-team/">Jobs at PIP</a></li>
                            <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                            <li class="job-search-link active"><a href="/job-search/">Job Search</a></li>
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
        <!-- mmm Job Search Table mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Search Table mmm -->
        <ux:JobSearch ID="uxJobSearch1" runat="server" />
    <!--end job_search -->
    </div>
</asp:Content>

