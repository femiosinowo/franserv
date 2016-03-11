<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="OneColumnWireframe.aspx.cs" Inherits="OneColumnWireframe" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/PageHost.ascx" TagPrefix="PH" TagName="PageHost" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/DropZone.ascx" TagPrefix="DZ" TagName="DropZone" %>
<%@ Register Assembly="Ektron.Cms.Widget" Namespace="Ektron.Cms.PageBuilder" TagPrefix="PB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" runat="Server">    
    <PH:PageHost ID="PageHost1" runat="server" />        
    <div id="one-column-dropzones">
        <DZ:DropZone ID="DropZone1" runat="server" AllowAddColumn="true" AllowColumnResize="true"></DZ:DropZone>
        <DZ:DropZone ID="DropZone2" runat="server" AllowAddColumn="true" AllowColumnResize="true"></DZ:DropZone>
        <DZ:DropZone ID="DropZone3" runat="server" AllowAddColumn="true" AllowColumnResize="true"></DZ:DropZone>
        <DZ:DropZone ID="DropZone4" runat="server" AllowAddColumn="true" AllowColumnResize="true"></DZ:DropZone>
        <DZ:DropZone ID="DropZone5" runat="server" AllowAddColumn="true" AllowColumnResize="true"></DZ:DropZone>
    </div>    
</asp:Content>

