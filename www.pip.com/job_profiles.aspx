<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="job_profiles.aspx.cs" Inherits="job_profiles" %>

<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ Register Src="~/UserControls/JobSearch.ascx" TagPrefix="ux" TagName="JobSearch" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        $(document).ready(function () {
            var queryTerm = getParameterByName('type');
            if ((queryTerm) && (queryTerm != '')) {
                $('ul#profileTabsList li').each(function () {
                    var tabText = $(this).find('span').text();
                    if ((tabText) && (tabText == queryTerm)) {
                        $(this).click();
                    }
                });
            }
        });
    </script>
    <div class="clear">
    </div>
    <div id="job-profiles">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="images/headers/header_1.jpg" alt="">--%>
                    <cms:ContentBlock ID="cbJobProfilesHeaderImage" runat="server" DynamicParameter="id"
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
                            <li class="job-profiles-link active"><a href="/job-profiles/">Job Profiles</a></li>
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
        <!-- mmm Job Profiles Top Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Profiles Top Content mmm -->
        <div class="join_team_content_wrapper main_about_us clearfix" style="background-color: white;">
            <div class="join_team_content clearfix">
                <div id="job_profiles_content" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-black int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <cms:ContentBlock ID="cbJobProfilesSideContent" runat="server" />
                                    <a class="cta-button-text" href="/job_search.aspx">
                                        <div class="cta-button-wrap int-button black-btn">
                                            <span>Search All Jobs</span>
                                        </div>
                                    </a>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!--grid_6-->
                    <div class="grid_18 content-block col-height-equal">
                        <div id="profile_tabs" class="responsive_tabs clearfix">
                            <asp:ListView ID="lvProfileTabs" runat="server">
                                <LayoutTemplate>
                                    <ul id="profileTabsList" class="resp-tabs-list">
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li data-tabname="<%# Eval("Name").ToString().ToLower().Replace(" ", "_") %>" id="<%# Eval("Name").ToString().ToLower().Replace(" ", "") %>-tab">
                                        <%# Eval("Name") %><span id="jobCategory" class="<%# Eval("Name") %>" style="display: none;"><%# Eval("Name").ToString().ToLower().Replace(" ", "") %></span></li>
                                </ItemTemplate>
                            </asp:ListView>
                            <div class="resp-tabs-container">
                                <asp:ListView ID="lvTabsContent" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <!-- TAB 1 -->
                                        <div class="profile_tabs_content container_24">
                                            <div class="grid_24">
                                                <div class="grid_11">
                                                    <h2>
                                                        <%# Eval("TypeName") %></h2>
                                                    <p class="tagline_inner">
                                                        <%# Eval("Abstract") %></p>
                                                    <p><%# Eval("Description") %></p>
                                                </div>
                                                <!-- end grid_11 -->
                                                <div class="grid_13">
                                                    <%-- <div class="video_wrapper">
                                                        <iframe frameborder="0" allowfullscreen="" mozallowfullscreen="" webkitallowfullscreen="" src="<%# Eval("VideoUrl") %>"></iframe>
                                                    </div>--%>
                                                    <%--<img src="/images/job-img-placeholder.jpg" alt="job image">--%>
                                                    <%# Eval("Image") %>
                                                    <!-- end video_wrapper -->
                                                </div>
                                                <!-- end grid_13 -->
                                            </div>
                                            <!-- end grid_24 -->
                                        </div>
                                        <!-- end profile_tabs_content -->
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <!-- end responsive-tabs-container -->
                        </div>
                        <!-- end profile_tabs -->
                    </div>
                </div>
            </div>            
            <!-- end job_profiles_wrapper -->
            <div class="clear">
            </div>
            <!--BOTTOM ROW-->
            <div id="job_profiles_tagline" class="container_24">
                <div class="grid_6 headline-block headline-white col-height-equal">
                    <div class="int-headline-block tagline-block int-block-2">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <h4>
                                    <cms:ContentBlock ID="cbJobProfilesQuotes" runat="server" />
                                </h4>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block col-height-equal">
                    <div class="grid_12 image-container alpha">
                        <cms:ContentBlock ID="imageJobProfile1" runat="server" />
                        <%--<img src="/images/job_profile_1.jpg" alt="job profile 1">--%>
                    </div>
                    <div class="grid_12 image-container omega">
                        <cms:ContentBlock ID="imageJobProfile2" runat="server" />
                        <%--<img src="/images/job_profile_2.jpg" alt="job profile 2">--%>
                    </div>
                </div>
                <!-- grid 18 content block-->
            </div>
            <!-- job_profiles_content-->
        </div>
        <!--end join_team_content -->
    <!-- end join_team_content_wrapper -->
    <div class="clear">
    </div>
    <ux:JobSearch ID="uxJobSearch1" runat="server" />
    <!--end job_search -->
    </div>
</asp:Content>
