<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="job_profiles.aspx.cs" Inherits="job_profiles" %>

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
            if( (queryTerm) && (queryTerm != ''))
            {
                $('ul#profileTabsList li').each(function () {
                    var tabText = $(this).find('span').text();
                    if((tabText) && (tabText == queryTerm))
                    {
                        $(this).click();
                    }
                });
            }            
        });
    </script>
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
                        <li class="job-profiles-link active"><a href="/job-profiles/">Job Profiles</a></li>
                        <li class="job-search-link"><a href="/job-search/">Job Search</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- mmm Job Profiles Top Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Profiles Top Content mmm -->
    <div class="job_profiles_content_wrapper  clearfix">
        <div class="job_profiles_content clearfix">
            <div class="job_profiles_headline_wrapper">
                <div class="container_24 clearfix">
                    <div class="grid_24 join_content_headline">
                        <cms:ContentBlock ID="cbLandingContent" runat="server" DoInitFill="false" />
                        <!-- end statement-text -->
                    </div>
                    <!--end grid_24-->
                </div>
                <!-- end container_24 -->
            </div>
            <!-- end job_profiles_headline_wrapper -->
            <div id="profile_tabs" class="responsive_tabs clearfix">
                <asp:ListView ID="lvProfileTabs" runat="server">
                    <LayoutTemplate>
                        <ul id="profileTabsList" class="resp-tabs-list">
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li id="<%# Eval("Description") %>"><%# Eval("Name") %><span id="jobCategory" class="<%# Eval("Name") %>" style="display:none;"><%# Eval("Name").ToString().ToLower().Replace(" ", "") %></span></li>
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
                                    <div class="grid_9 alpha">
                                        <h2><%# Eval("TypeName") %></h2>
                                        <p class="tagline_inner"><%# Eval("Abstract") %></p>
                                        <p><%# Eval("Description") %></p>
                                    </div>
                                    <!-- end grid_9 -->
                                    <div class="grid_13 prefix_2 omega">
                                       <%# Eval("VideoUrl") %>
                                       <%-- <div class="video_wrapper">
                                            <iframe frameborder="0" allowfullscreen="" mozallowfullscreen="" webkitallowfullscreen="" src="<%# Eval("VideoUrl") %>"></iframe>
                                        </div>--%>
                                        <!-- end video_wrapper -->
                                        <%--<div class="image_wrapper">
                                          <img src="" />
                                        </div>--%>
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
            <div class="clear"></div>
            <div class="profiles_red_bg_wrapper clearfix">
                <div class="profiles_red_bg">
                    <div class="container_24 clearfix">
                        <div class="prefix_8 grid_16">
                            <div class="statement-text">
                                <asp:ListView ID="lvJobProfileStatementTxt" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <p class="quote_tab_item-<%#(Container.DataItemIndex) %>"><%# Eval("TagLine") %></p>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="cta-button-wrap white">
                                    <a href="#job_search" id="statementTxtJobSearch" class="cta-button-text">Search All&nbsp;<span>Sales</span>&nbsp;Jobs</a>
                                </div>
                                <!-- end cta-button-wrap -->
                            </div>
                            <!-- end statement-text -->
                        </div>
                        <!--end prefix_8 grid_16-->
                    </div>
                    <!--end container_24 -->
                </div>
                <!-- end profiles_red_bg -->
            </div>
            <!-- end profiles_red_bg_wrapper -->
        </div>
        <!--end job_profiles -->
    </div>
    <!-- end job_profiles_wrapper -->
    <div class="clear"></div>
    <ux:JobSearch ID="uxJobSearch1" runat="server" />
    <!--end job_search -->
</asp:Content>

