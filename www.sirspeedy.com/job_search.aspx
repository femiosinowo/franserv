<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="job_search.aspx.cs" Inherits="job_search" %>
<%@ Register Src="~/UserControls/JobSearch.ascx" TagPrefix="ux" TagName="JobSearch" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <div class="clear"></div>
    <div id="subpage_tagline_wrapper" runat="server" class="subpage_tagline_wrapper job_profiles clearfix">
        <div class="subpage_tagline">
            <cms:ContentBlock ID="cbTagline" runat="server" CacheInterval="300" DynamicParameter="id" Visible="false" />
        </div>
        <!-- end subpage_tagline -->
    </div>
    <div class="clear"></div>
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation join_our_team">
            <div class="container_24">
                <div class="grid_24">
                    <div id="mobile-nav-header" class="lvl-2-title-wrap">
                        <a id="page-title" class="lvl-2-title" href="#"></a><a class="arrow-plus-minus" href="#">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Join Our Team -->
                    <ul id="join-our-team-desktop-nav">
                        <li class="join-our-team-link"><a href="/join-our-team/">Join Our Team</a></li>
                        <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                        <li class="job-search-link active"><a href="/job-search/">Job Search</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <div class="clear"></div>
    <ux:JobSearch ID="uxJobSearch1" runat="server" />
    <!--end job_search -->
</asp:Content>

