<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<div class="top_header clearfix">
    <!-- mmmmmmm location_nav  mmmmmmmm -->
    <div class="location_nav header_content">
        <ul>
            <li class="nav_icons icon_facebook"><a id="fbHeadLink" target="_blank" runat="server" href="#">
                <img alt="Facebook" src="/images/facebook_footer_icon.png" /></a></li>
            <li class="nav_icons icon_twitter"><a id="twHeadLink" target="_blank" runat="server" href="#">
                <img alt="Twitter" src="/images/twitter_footer_icon.png" /></a></li>
            <li class="nav_icons icon_google"><a id="inHeadLink" target="_blank" runat="server" href="#">
                <img alt="LinkedIn" src="/images/linkedin_footer_icon.png" /></a></li>
            <li class="nav_icons icon_search"><a href="#">Search</a></li>
        </ul>
        <CMS:ContentBlock ID="cbFindLocation" runat="server" Visible="false" DoInitFill="false" SuppressWrapperTags="true" />
        <asp:Literal ID="ltrContactNumber" Visible="false" runat="server" />        
    </div>
    <!--location nav-->
    <!-- mmmmmmm logo wrapper  mmmmmmmm -->
    <div class="logo header_content">
        <a href="/">
            <CMS:ContentBlock ID="cbLogo" runat="server" DoInitFill="false" SuppressWrapperTags="true" />
        </a>
    </div>
    <!--logo-->
    <div id="mobileNav">
        <!--SITE MENU for mobile version-->
        <a class="menu_button" href="#" onclick="toggleNav(); return false;">MENU</a>
    </div>
    <!--mobileNav-->
    <!-- mmmmmmm navigation  mmmmmmmm -->
    <div class="navigation header_content">
        <CMS:FlexMenu ID="headerMenu" runat="server" WrapTag="div" AutoCollapseBranches="True" Visible="false"
            StartCollapsed="True" EnableMouseOverPopUp="True" EnableSmartOpen="False" StartLevel="1" MenuDepth="2" EnableAjax="False"
            SuppressAddEdit="true" />
        <div class="top_nav">
            <ul>
                <li><asp:HyperLink runat="server" ID="ClientLoginURLHyperLink1" Visible="False" Text="Client Login" Target="_blank"></asp:HyperLink></li>
                <asp:ListView ID="lvHeaderMenuItems" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li><a target="<%# Eval("Target") %>" class="<%# Eval("Description") %>" href="<%# Eval("Link") %>"><%# Eval("Title") %></a></li>
                    </ItemTemplate>
                </asp:ListView>
                <li class="nav_icons icon_facebook"><a id="fbLink" target="_blank" runat="server" href="#">
                    <img alt="Facebook" src="/images/facebook_footer_icon.png" /></a></li>
                <li class="nav_icons icon_twitter"><a id="twitterLink" target="_blank" runat="server" href="#">
                    <img alt="Twitter" src="/images/twitter_footer_icon.png" /></a></li>
                <li class="nav_icons icon_google"><a id="linkedInLink" target="_blank" runat="server" href="#">
                    <img alt="LinkedIn" src="/images/linkedin_footer_icon.png" /></a></li>
                <li style="display: none;" class="nav_icons icon_search"><a href="#">Search</a></li>
            </ul>
        </div>
        <!--top_nav-->        
        <div class="bottom_nav" id="bottom_nav">
            <CMS:FlexMenu ID="mainNav" runat="server" WrapTag="div" AutoCollapseBranches="True" Visible="false"
                StartCollapsed="True" EnableMouseOverPopUp="True" EnableSmartOpen="False" StartLevel="1" MenuDepth="1" EnableAjax="False"
                SuppressAddEdit="true" />
            <asp:ListView ID="lvMainMenuItems" runat="server"  OnLayoutCreated="lvMainMenuUtems_LayoutCreated">
                <LayoutTemplate>
                    <ul>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        <li id="mobile_consultation" class="consultation_flag_mobile"><asp:HyperLink runat="server" id="RequestConsultationLink1">REQUEST A CONSULTATION</asp:HyperLink>
                        </li>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>
                    <li><a class="<%# Eval("Description") %>" href="<%# Eval("Link") %>"><%# Eval("Title") %></a></li>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <!--bottom_nav-->
    </div>
    <!--navigation-->
</div>
<div class="clear"></div>
