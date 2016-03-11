<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/HomePageBanners.ascx" TagPrefix="ux" TagName="Banners" %>
<%@ Register Src="~/UserControls/HomePageTagLine.ascx" TagPrefix="ux" TagName="TagLines" %>
<%@ Register Src="~/UserControls/HomePageProductsServices.ascx" TagPrefix="ux" TagName="ProductsServices" %>
<%@ Register Src="~/UserControls/HomePagePromos.ascx" TagPrefix="ux" TagName="Promos" %>
<%@ Register Src="~/UserControls/HomePageLocations.ascx" TagPrefix="ux" TagName="Locations" %>
<%@ Register Src="~/UserControls/HomePageMaps.ascx" TagPrefix="ux" TagName="worldMaps" %>
<%@ Register Src="~/UserControls/HomePageCaseStudies.ascx" TagPrefix="ux" TagName="CaseStudies" %>
<%@ Register Src="~/UserControls/HomePageFransFooter.ascx" TagPrefix="ux" TagName="FransFooter" %>
<%@ Register Src="~/UserControls/HomePageNationalFooter.ascx" TagPrefix="ux" TagName="NationalFooter" %>
<%@ Register Src="~/UserControls/CenterSpecificJSCode.ascx" TagPrefix="ux" TagName="JSCode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">
    <ux:Banners ID="uxPageBanners" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <ux:TagLines ID="uxTagLines" Visible="false" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <ux:ProductsServices ID="uxProductsAndServices" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <!-- mmm Advertisments (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Advertisments (lcl) mmm -->
    <ux:Promos ID="uxPromos" Visible="false" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <ux:Locations ID="uxLocations" Visible="false" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <!-- mmm World Wide Locations (ntl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm World Wide Locations (ntl) mmm -->
    <ux:worldMaps ID="uxMaps" Visible="false" runat="server" />
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <!-- mmm Our Portfolio / Case Studies Wrapper mmmmmmmmmmmmmmmmmmm  Our Portfolio / Case Studies Wrapper mmm -->
    <ux:CaseStudies ID="uxCaseStudies" runat="server" /> 
    <!-- xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx -->
    <!-- mmm footer upper mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm footer upper mmm -->
    <ux:NationalFooter ID="uxNationalFooter" runat="server" />
    <ux:FransFooter ID="uxFransFooter" Visible="false" runat="server" />          
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyScripts" runat="Server">
    <ux:JSCode ID="centerSpecificJSCode" runat="server" Visible="false" />
</asp:Content>