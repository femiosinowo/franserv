<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="AdminTool_Templates_ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sirspeedy: Change Password</title>
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
                            <h3>Change Password:</h3>
                            <br />
                            <table>
                                <tr>
                                    <td>User Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtUserName" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="UserName is required"
                                            ControlToValidate="txtUserName" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Current Password:</td>
                                    <td>
                                        <asp:TextBox ID="txtCurrentPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Current Password is required"
                                            ControlToValidate="txtCurrentPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>New Password:</td>
                                    <td>
                                        <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="New Password is required"
                                            ControlToValidate="txtNewPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Confirm Password:</td>
                                    <td>
                                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Confirm Password is required"
                                            ControlToValidate="txtConfirmPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" OnClick="btnChangePassword_Click" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" CssClass="errorMessage" ID="errMessage"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="successMessagePln" runat="server" visible="false">
                                    <td>
                                        <br />
                                        <asp:Label CssClass="successMessage" runat="server" ID="successMessage"></asp:Label>
                                        <br />
                                        <a href="/admintool/templates/login.aspx">Login In Now</a>
                                    </td>
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
