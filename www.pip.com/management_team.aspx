<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="management_team.aspx.cs" Inherits="_management_team" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image_content">
            <div class="header_image">
                <CMS:ContentBlock ID="cbMgtTeamHeaderImage" runat="server" DynamicParameter="id"
                    CacheInterval="300" />
                <%--<img src="images/headers/header_2.jpg" alt="">--%>
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
        <div class="sub_navigation about-us">
            <div id="sub_navigation">
                <div class="menu-title-block">
                    <div id="about-menu-h2">
                        <h2 id="menu-company-info">Company Info</h2>
                        <h2 id="menu-mgmt-team">Management Team</h2>
                        <h2 id="menu-partners">Partners</h2>
                        <h2 id="menu-history">History</h2>
                        <h2 id="menu-news">News</h2>
                        <h2 id="menu-media">In The Media</h2>
                    </div>
                </div>
                <div class="menu-items-block">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us National -->
                    <ul id="about-desktop-nav">
                        <li class="company-info-link"><a href="/company-info/">Company Info</a></li>
                        <li class="mgmt-team-link active"><a href="/company-info/management-team/">Management Team</a></li>
                        <li class="partners-link"><a href="/company-info/partners/">Partners</a></li>
                        <li class="history-link"><a href="/company-info/history/">History</a></li>
                        <li class="news-link news2-link"><a href="/company-info/news/">News</a></li>
                        <li class="media-link media2-link"><a href="/company-info/in-the-media/">In the Media</a></li>
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
    <!-- mmm Management Team mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Management Team mmm -->
    <div class="mgmt_team_content_wrapper main_about_us clearfix" style="background-color: white;">
        <div class="mgmt_team_content clearfix">
            <div class="container_24" id="mgmt_team_content">
                <div class="grid_6 headline-block">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <CMS:ContentBlock ID="cbMgmtTeamSideContent" runat="server" />
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block">
                    <div class="grid_24" id="mgmt_team_main">
                        <asp:Repeater runat="server" ID="UxManagementTeamRows" OnItemDataBound="UxManagementTeamRow_ItemDataBound">
                            <ItemTemplate>
                                <div id="row_<%#Eval("RowId") %>" class="container_24 mgmt_profile_row">
                                    <div class="grid_24 mgmt_profile_wrapper">
                                        <asp:Repeater runat="server" ID="UxManagementTeamRow">
                                            <ItemTemplate>
                                                <div class="grid_8 cs_container <%#Eval("cssClass")%>">
                                                    <a href="#" class="cs_image" id="profile_<%#Eval("counter")%>">
                                                        <img src="<%#Eval("imageSRC")%>" alt="<%#Eval("firstName")%> <%#Eval("lastName")%>" />
                                                        <div class="cs_image_content_wrapper">
                                                            <div class="cs_image_content">
                                                                <h3>
                                                                    <%#Eval("firstName")%>
                                                                    <%#Eval("lastName")%></h3>
                                                                <p>
                                                                    <%# Eval("jobTitle")%>
                                                                </p>
                                                            </div>
                                                            <!-- end cs_image_02_content -->
                                                        </div>
                                                        <!-- end cs_image_02_content_wrapper -->
                                                    </a>
                                                    <!-- end cs_image_02 -->
                                                    <div id="profile_<%#Eval("counter")%>_detail" class="profile_detail clearfix">
                                                        <div class="prefix_1 grid_22 suffix_1 omega profile_text">
                                                            <div class="profile_text_inner">
                                                                <h3>
                                                                    <%#Eval("firstName")%>
                                                                    <%#Eval("lastName")%>,
                                                                <%# Eval("jobTitle")%></h3>
                                                                <blockquote>
                                                                    <%#Eval("abstract")%>
                                                                </blockquote>
                                                                <p>
                                                                    <%#Eval("bio")%>
                                                                </p>
                                                                <p>
                                                                    <%#Eval("socialMedia")%>
                                                                </p>
                                                            </div>
                                                        </div>
                                                        <!-- end profile_text -->
                                                    </div>
                                                    <!-- end profile details-->
                                                </div>
                                                <!-- grid_8 -->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <!-- mgmt_profile_wrapper -->
                                    <div class="clear">
                                    </div>
                                    <div id="profile_detail_box_<%#Eval("RowId") %>" class="mgmt_profile_detail_wrapper">
                                        <div class="grid_24">
                                            <a class="close_button"><span class="visuallyhidden">X</span></a>
                                            <div class="profile_detail_content">
                                            </div>
                                            <!-- end content -->
                                        </div>
                                        <!-- grid_24 -->
                                    </div>
                                    <!-- mgmt_profile_detail_wrapper -->
                                </div>
                                <!-- mgmt_profile_row -->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
            <div class="clear">
            </div>
        </div>
        <!--end insights_case_studies -->
    </div>
    <!-- end insights_case_studies_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
