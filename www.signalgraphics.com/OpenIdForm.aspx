<%@ Page Title="" Language="C#"  AutoEventWireup="true"
    CodeFile="OpenIdForm.aspx.cs" Inherits="OpenIdForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>OpenID login</title>

  <!-- global styles -->
    <link href="/css/reset.css" rel="stylesheet" type="text/css"/>
    <link href="/css/text.css" rel="stylesheet" type="text/css"/>
    <link href="/css/futura.css" rel="stylesheet" type="text/css"/>
    <link href="/css/960_grid.css" rel="stylesheet" type="text/css"/>
    <link href="/css/master.css" rel="stylesheet" type="text/css"/>
</head>
<body>
  <form id="form" runat="server">
    <asp:TextBox runat="server" ID="txtOpenId" Width="200" />
    <asp:Button runat="server" ID="btnLogon" Text="Login" OnClick="btnLogon_Click" class="red_btn" />
  </form>
</body>
</html>
