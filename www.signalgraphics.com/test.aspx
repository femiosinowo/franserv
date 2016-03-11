<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Test</h2>  
			<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
			<ajaxToolkit:AjaxFileUpload ID="ajaxFileUpload" BorderStyle="None" runat="server"                                
                                OnUploadComplete="Upload_NationalComplete"
                               ClientIDMode="Static" />
        </div>
    </form>
</body>
</html>
