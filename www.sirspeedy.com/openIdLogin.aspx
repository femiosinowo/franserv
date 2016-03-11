<%@ Page Language="C#" %>
<script runat="server">
  protected void Page_Load(object sender, EventArgs e)
  {
    if (OpenID.IsOpenIdRequest)
    {
      OpenIdData data = OpenID.Authenticate();
      if (data.IsSuccess)
      {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("<b>OpenID: {0}</b><br />", data.Identity);
        sb.AppendFormat("email: {0}<br />", data.Parameters["email"]);
        sb.AppendFormat("fullname: {0}<br />", data.Parameters["fullname"]);
        sb.AppendFormat("country: {0}<br />", data.Parameters["country"]);
        sb.AppendFormat("language: {0}<br />", data.Parameters["language"]);

        Response.Write(sb.ToString());
      }
    }
  }

  protected void btnLogon_Click(object sender, EventArgs e)
  {
    
    bool success = OpenID.Login(txtOpenId.Text, "email,fullname,country", "language");

    if (!success)
    {
      Response.Write("The OpenID is not valid");
    }
  }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>OpenID login</title>

  <!-- global styles -->
   <%-- <link href="/css/reset.css" rel="stylesheet" type="text/css"/>
    <link href="/css/text.css" rel="stylesheet" type="text/css"/>
    <link href="/css/futura.css" rel="stylesheet" type="text/css"/>
    <link href="/css/960_grid.css" rel="stylesheet" type="text/css"/>
    <link href="/css/master.css" rel="stylesheet" type="text/css"/>--%>
</head>
<body>
  
  <form id="form" runat="server">
    <asp:TextBox runat="server" ID="txtOpenId" Width="200" />
    <asp:Button runat="server" ID="btnLogon" Text="Login" OnClick="btnLogon_Click" class="red_btn" />
    <%--<input type="submit" value="Login" class="red_btn" onclick="btnLogon_Click" />--%>
  </form>
  
</body>
</html>