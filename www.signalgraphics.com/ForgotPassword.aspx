<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <!-- global styles -->
    <%--<link href="/css/reset.css" rel="stylesheet" type="text/css"/>
    <link href="/css/text.css" rel="stylesheet" type="text/css"/>
    <link href="/css/futura.css" rel="stylesheet" type="text/css"/>
    <link href="/css/960_grid.css" rel="stylesheet" type="text/css"/> 
    <link href="/css/master.css" rel="stylesheet" type="text/css"/>

    <link href="/css/header_footer.css" rel="stylesheet" type="text/css"/> --%>
</head>
<body>
    <%--<form id="form" runat="server">--%>
    <form id="forgot_password_form" runat="server">
        <div>
            <asp:TextBox runat="server" ID="txtEmailAddress" Width="200" TextMode="Email" Placeholder="Email" />
            <asp:Button runat="server" ID="btnLogon" Text="Submit" OnClick="btnSubmit_Click" class="red_btn" />
        </div>
    </form>
    <%--/form>--%>
</body>
</html>