<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/SiteContent.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="AdminTool_Templates_Content" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" Runat="Server">
    <cms:ContentBlock ID="cb1" runat="server" DynamicParameter="id" />
</asp:Content>

