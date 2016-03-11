<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="join_our_team.aspx.cs" Inherits="join_our_team" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/JoinOurTeamDepartmentSlider.ascx" TagPrefix="ux"
    TagName="DepartmentSlider" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="job-pip">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="images/headers/header_1.jpg" alt="">--%>
                    <CMS:ContentBlock ID="cbJoinOurTeamHeaderImage" runat="server" DynamicParameter="id"
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
                            <li class="job-pip-link active"><a href="/join-our-team/">Jobs at PIP</a></li>
                            <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                            <li class="job-search-link"><a href="/job-search/">Job Search</a></li>
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
        <!-- mmm join_team Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm join_team Content mmm -->
        <div class="join_team_content_wrapper main_about_us clearfix" style="background-color: white;">
            <div class="join_team_content clearfix">
                <div id="join_our_team_content" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-black int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <CMS:ContentBlock ID="cbJoinOurTeamSideContent" runat="server" />
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                        <div class="int-headline-block tagline-block int-block-2">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <CMS:ContentBlock ID="cbJoinOurTeamSideQuotes" runat="server" />
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!--grid_6-->
                    <div class="grid_18 content-block col-height-equal">
                        <div class="grid_10 prefix_1 suffix_1 content-text">
                            <CMS:ContentBlock ID="cbJoinOurTeamsMainContent" runat="server" />
                            <%-- <h3>Where all talents apply.</h3>
					        <h4>Be a part of something amazing.</h4>
					            <p>We’re people who love technology and people who love people. We’re also musicians, photographers, mountain climbers, students, and artists whose interests can’t be defined by a job description. Whether you’re analytical or creative, tech savvy or insightful, there’s a place to share your talents while you learn, develop, and inspire. </p>
                            <h4>Opportunities near and far.</h4>
				            <p>Join us at Sir Speedy. We have stores all over the world, from Cupertino to London to Shanghai. Off-site and at-home opportunities are available as well. We’re looking for people who are innovative, talented, creative, and driven. Whether you’re analytical or creative, tech savvy or insightful, there’s a place to share your talents while you learn, develop, and inspire.</p>--%>
                        </div>
                        <div class="grid_12 omega">
                            <img src="/images/join_our_team.jpg" alt="Jobs at PIP">
                        </div>
                    </div>
                    <!-- grid 18 content block-->
                </div>
                <!-- join our team content-->
                <div class="clear">
                </div>
                <div id="job_search_section" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-white int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-white"></span>
                                    <h2 class="headline">
                                        Find a Job</h2>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!--grid_6-->
                    <div class="grid_18 content-block col-height-equal background_wrapper" id="findAJobText" runat="server" data-image="../images/job_search_section_bg.jpg">
                        <CMS:ContentBlock ID="cbFindAJobText" runat="server" DoInitFill="false" />
                    </div>
                    <!-- grid 18 content block-->
                </div>
                <!-- job_search_section-->
                <div class="clear">
                </div>
                <div id="job_profiles_section" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-white int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-white"></span>
                                     <CMS:ContentBlock ID="cbSliderTagline" runat="server" DoInitFill="false" />
                                     <CMS:ContentBlock ID="cbSliderTeaser" runat="server" DoInitFill="false" />
                                    <%--<h2 class="headline">
                                        The Many Faces of Sir Speedy</h2>
                                    <p>
                                        When there’s an opening at Sir Speedy, we always look to fill it with the best.
                                        If that sounds like you, then start your search right here.</p>--%>
                                    <a href="/job-profiles/" class="cta-button-text cta-button-wrap white-btn"><span>All Jobs Profiles</span></a>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!--grid_6-->
                    <ux:DepartmentSlider ID="UxJoinOurTeamDepartmentSlider" runat="server" />
                </div>
                <!-- job_profiles_section-->
            </div>
            <!--end join_team_content -->
        </div>
        <!-- end join_team_content_wrapper -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
