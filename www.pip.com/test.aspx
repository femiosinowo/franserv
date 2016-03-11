<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Test</h2>   
            <h3>National Social Icons:</h3>        
            <asp:Literal ID="ltrNational" runat="server"></asp:Literal>
            <br />
            <br />
            <h3>Local Social Icons:</h3>
            <asp:Literal ID="ltrLocal" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>

