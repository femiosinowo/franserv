<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="/MasterPages/main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/HomePageBanners.ascx" TagPrefix="ux" TagName="Banners" %>
<%@ Register Src="~/UserControls/HomePageNationalControls.ascx" TagPrefix="ux" TagName="National" %>
<%@ Register Src="~/UserControls/HomePageLocalControls.ascx" TagPrefix="ux" TagName="Local" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeaderContent" runat="Server">
    <ux:Banners ID="uxPageBanners" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <ux:National ID="uxNational" Visible="false" runat="server" />
    <ux:Local ID="uxLocal" Visible="false" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphBodyScripts" runat="Server">
    <asp:Literal ID="ltrScripts" runat="server"></asp:Literal>
</asp:Content>
