<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestSocialMedia.aspx.cs" Inherits="TestSocialMedia" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <h3>Helper Page to Verify Social links:</h3>
            <asp:Panel ID="pnlAdmin" runat="server">
                <h3>National Social Icons:</h3>
                <asp:Literal ID="ltrNational" runat="server"></asp:Literal>
                <br />
                <br />
                <h3>Local Social Icons:</h3>
                <asp:Literal ID="ltrLocal" runat="server"></asp:Literal>
            </asp:Panel>
            <asp:Panel ID="pnlAnonymous" runat="server" Visible="false">
                <p>PLease login as CMS Admin user!!!</p>
                <a href="/cmslogin.aspx" target="_blank">Login</a>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
