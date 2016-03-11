<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClearCache.aspx.cs" Inherits="Utility_ClearCache" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clear Cache</title>    
</head>
<body>
    <form id="form1" runat="server">
     <h3>Helper Page to Clear Site Cache</h3>
        <asp:Panel ID="pnlAdmin" DefaultButton="btnClearCache" runat="server">
            <p style="color:red;">Please note, if you clear the site cache then other users on the site will experinece the delay of page loads.</p>
            <asp:Button ID="btnClearCache" runat="server" ValidationGroup="AddSubMarket" Text="Clear Cache" OnClick="btnClearCache_Click" />
            <br />
            <br />
            <asp:Literal ID="ltrStatus" runat="server"></asp:Literal>
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlAnonymous" runat="server" Visible="false">
            <p>PLease login as CMS Admin user!!!</p>            
            <a href="/cmslogin.aspx" target="_blank">Login</a>
        </asp:Panel>
    </form>
</body>
</html>
