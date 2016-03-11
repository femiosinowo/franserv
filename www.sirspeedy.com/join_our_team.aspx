<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="join_our_team.aspx.cs" Inherits="join_our_team" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/JoinOurTeamDepartmentSlider.ascx" TagPrefix="ux" TagName="DepartmentSlider" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Rotating Banner (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Rotating Banner (both) mmm -->
    <%--<div class="main_rotator_wrapper  clearfix">
        <div class="main_rotator join_our_team">
            <!-- rotator section mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm rotator section -->
            <div id="top_slider">
              <div class="flexslider">
                <ul class="slides">
                  <asp:Repeater runat="server" ID="UxJoinOurTeamSlider">
                        <ItemTemplate>
                            <li>
                                <%# Eval("imageSRC")%>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
              </div><!--end flexslider-->
            </div>
            <!--end #top_slider section-->
            <div class="clear">
            </div>
        </div>
        <!-- end main_rotator -->
    </div>
    <!--end main_rotator_wrapper-->--%>
    <!-- mmm Header (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header (both) mmm -->
    <div class="join_header_img_wrapper clearfix">
        <img src="/images/header-img-job-search.jpg" alt="img" />
    </div>
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation join_our_team">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Join Our Team -->
                    <ul id="join-our-team-desktop-nav">
                        <li class="join-our-team-link active"><a href="/join-our-team/">Join Our Team</a></li>
                        <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                        <li class="job-search-link"><a href="/job-search/">Job Search</a></li>
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
    <!-- mmm Join Our Team Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Join Our Team Content mmm -->
    <div class="join_content_wrapper  clearfix">
        <div class="join_content clearfix">
            <div class="container_24 clearfix">
                <div class="grid_24 join_content_headline">
                    <!--  -->
                    <CMS:ContentBlock ID="cbTagline" runat="server" CacheInterval="300" DynamicParameter="id" />
                </div>
                <!--end grid_24-->
                <div class="grid_13 alpha">
                    <h2 class="red large">
                        <CMS:ContentBlock ID="cbContentTitle" runat="server" DoInitFill="false" />
                    </h2>
                    <CMS:ContentBlock ID="cbJoinOurTeamsMainContent" runat="server" DoInitFill="false" />
                </div>
                <!-- end grid_13 -->
                <div class="grid_10 prefix_1">
                    <img class="inner-border" src="/images/Website-Join-Team_480x300.jpg" alt="Group Photo">
                </div>
                <!-- end grid_10 -->
            </div>
            <!--end container_24-->
            <div id="find_job_wrapper" class="clearfix">
                <div id="find_job">
                    <CMS:ContentBlock ID="cbFindJob" runat="server" />
                    <!-- .statement-text -->
                    <div class="cta-button-wrap purple dark_bg">
                        <a class="cta-button-text" href="/job-search/"><span>Start Your Search</span></a>
                    </div>
                    <!-- button -->
                </div>
                <!-- find_job -->
            </div>
            <!-- find_job_wrapper -->
            <div class="container_24 clearfix" id="job_profile_intro">
                <div class="grid_24">
                    <div class="grid_20 alpha">
                        <CMS:ContentBlock ID="cbSliderTagline" runat="server" DoInitFill="false" />
                        <CMS:ContentBlock ID="cbSliderTeaser" runat="server" DoInitFill="false" />
                    </div>
                    <!-- end grid_20 -->
                    <div class="grid_4 omega">
                        <div class="cta-button-wrap purple">
                            <a class="cta-button-text" href="/job-profiles/"><span>All Job Profiles</span></a>
                        </div>
                        <!-- button -->
                    </div>
                    <!-- end grid_4 -->
                </div>
                <!-- end grid_24 -->
            </div>
            <!-- end container_24 -->
            <ux:DepartmentSlider ID="UxJoinOurTeamDepartmentSlider" runat="server" />
            <!-- end job_profile_slider -->
        </div>
    <!--end join_content -->
    </div>
    <!-- end join_content_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
