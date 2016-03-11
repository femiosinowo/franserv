<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="AdminTool_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Tool Login: Team Logic IT</title>
    <link href="/AdminTool/css/admin.css" rel="stylesheet" />
    <link href="/css/flexslider.css" rel="stylesheet" />
    <style type="text/css">
        .mw {
            color: #000000;
            font-family: Verdana,Arial,Helvetica;
            font-weight: bold;
            font-size: xx-small;
            text-decoration: none;
        }

        a.mw:link {
            color: #000000;
            font-family: Verdana,Arial,Helvetica;
            font-weight: bold;
            font-size: xx-small;
            text-decoration: none;
        }

        a.mw:visited {
            color: #000000;
            font-family: Verdana,Arial,Helvetica;
            font-weight: bold;
            font-size: xx-small;
            text-decoration: none;
        }

        a.mw:hover {
            color: #0000FF;
            font-family: Verdana,Arial,Helvetica;
            font-weight: bold;
            font-size: xx-small;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
        <div id="admin-body">
            <div class="clearfix" id="header">
                <div id="brand">
                    <img id="logo" alt="TeamLogicIT logo" src="/Admintool/images/TLITLogo.png" />
                </div>
                <span id="brandTitle">Management Console</span>
            </div>
            <div id="headerBottom">&nbsp;</div>
            <div id="wrapper">
                <div class="clearfix" id="wrapper1">
                    <div class="hasLayout" id="content">
                        <div>
                            <h3>Login:</h3>
                            <asp:Label CssClass="errorMessage" ID="lblError" runat="server"></asp:Label>
                            <table>
                                <tr>
                                    <td>UserName:</td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="UserName is required"
                                            ControlToValidate="txtUserName" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Password:</td>
                                    <td>
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Password is required"
                                            ControlToValidate="txtPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:HyperLink runat="server" ID="changePasswordPg" NavigateUrl="~/AdminTool/Templates/ChangePassword.aspx" Text="Change Password"></asp:HyperLink>
                                    </td>

                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:HyperLink runat="server" ID="forgotPasswordPg" NavigateUrl="~/AdminTool/Templates/ForgotPassword.aspx" Text="Forgot Password"></asp:HyperLink>
                                    </td>

                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Button ID="btnLoginUser" runat="server" Text="Login" OnClick="btnLoginUser_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

