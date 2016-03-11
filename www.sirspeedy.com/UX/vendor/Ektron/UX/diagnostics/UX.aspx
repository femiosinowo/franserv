<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UX.aspx.cs" Inherits="UX_UX" %>
<%@ Register Assembly="Ektron.Cms.Widget" Namespace="Ektron.Cms.PageBuilder" TagPrefix="PB" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/PageHost.ascx" TagName="PageHost" TagPrefix="CMS" %>
<%@ Register Src="~/Workarea/PageBuilder/PageControls/DropZone.ascx" TagName="DropZone" TagPrefix="CMS" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>UX Test Page</title>
    </head>
    <body>
        <form runat="server">
            <h1>UX Test Page</h1>
            <CMS:PageHost ID="uxPageHost" runat="server" />
            <CMS:DropZone ID="uxDropZone1" runat="server" AllowColumnResize="true" AllowAddColumn="true">
                <ColumnDefinitions>
                    <PB:ColumnData columnID="0" unit="percent" width="100" />
                </ColumnDefinitions>
            </CMS:DropZone>
            <CMS:DropZone ID="uxDropZone2" runat="server" AllowColumnResize="true" AllowAddColumn="true">
                <ColumnDefinitions>
                    <PB:ColumnData columnID="0" unit="percent" width="100" />
                </ColumnDefinitions>
            </CMS:DropZone>
            <CMS:DropZone ID="uxDropZone3" runat="server" AllowColumnResize="false" AllowAddColumn="true">
                <ColumnDefinitions>
                    <PB:ColumnData columnID="0" unit="percent" width="100" />
                </ColumnDefinitions>
            </CMS:DropZone>
            <CMS:DropZone ID="uxDropZone4" runat="server" AllowColumnResize="true" AllowAddColumn="true">
                <ColumnDefinitions>
                    <PB:ColumnData columnID="0" unit="percent" width="100" />
                </ColumnDefinitions>
            </CMS:DropZone>
        </form>
    </body>
</html>
