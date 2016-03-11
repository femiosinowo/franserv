<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NationalAboutUsSubNav.ascx.cs"
    Inherits="UserControls_NationalAboutUsSubNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
<div class="sub_navigation_wrapper  clearfix">
    <div class="sub_navigation about-us">
        <div class="container_24">
            <div class="grid_24">
                <div class="lvl-2-title-wrap" id="mobile-nav-header">
                    <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                        &nbsp;</a>
                </div>
                <!-- end lvl-2-title-wrap -->
                <!-- About Us National -->
                <ektron:MenuModelSource ID="UxNationalAboutUsSubNav" runat="server">
                </ektron:MenuModelSource>
                <ektron:MenuView ID="uxAboutUsSubNavLinks" runat="server" ModelSourceID="UxNationalAboutUsSubNav">
                    <ListTemplate>
                        <ul>
                            <asp:PlaceHolder ID="listPlaceholder" runat="server" />
                        </ul>
                    </ListTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="nodeLink" runat="server" Text='<%# Eval("Text") %>' NavigateUrl='<%# Eval("NavigateUrl") %>' />
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </li>
                    </ItemTemplate>
                </ektron:MenuView>
                <%--<ul id="about-desktop-nav">
                    <li class="company-info-link"><a href="aboutUs.aspx">Company Info</a></li>
                    <li class="mgmt-team-link"><a href="management_team.aspx">Management Team</a></li>
                    <li class="partners-link"><a href="partners.aspx">Partners</a></li>
                    <li class="history-link"><a href="#">History</a></li>
                    <li class="news-link"><a href="news.aspx">News</a></li>
                    <li class="media-link"><a href="in_the_media.aspx">In the Media</a></li>
                </ul>--%>
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
