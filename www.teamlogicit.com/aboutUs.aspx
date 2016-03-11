<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="aboutUs.aspx.cs" Inherits="about_national" %>
<%@ Register Src="~/UserControls/AboutUsNational.ascx" TagPrefix="ux" TagName="AboutNational" %>
<%@ Register Src="~/UserControls/AboutUsLocal.ascx" TagPrefix="ux" TagName="AboutLocal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">    
    <ux:AboutNational ID="uxNationalAboutUs" runat="server" Visible="false" />
    <ux:AboutLocal ID="uxAboutLocal" runat="server" Visible="false" />
</asp:Content>
