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
            <br />
            <asp:Panel ID="pnl1" runat="server">
            <h4>Reset Password</h4>
            <p>This feature will un-lock user account and email the temp password.</p>
                <asp:Label ID="lblError1" runat="server"></asp:Label>
            <table>
                <tr>
                    <td>User Name</td>
                    <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtUsername" ForeColor="Red" ValidationGroup="Reset">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" ValidationGroup="Reset" OnClick="btnResetPassword_Click" /></td>
                </tr>
            </table>
            <br />
            <br />
            </asp:Panel>
            <asp:Panel ID="pnl2" runat="server">
            <h4>Unlock Account</h4>
            <p>This feature will un-lock user account and same password is still valid.</p>
            <asp:Label ID="lblError2" runat="server"></asp:Label>
            <table>
                <tr>
                    <td>User Name</td>
                    <td><asp:TextBox ID="txtUserName2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName2" ForeColor="Red" ValidationGroup="Unlock">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnUnlockAccount" runat="server" Text="Unlock User Account" ValidationGroup="Unlock" OnClick="btnUnlockAccount_Click" /></td>
                </tr>
            </table>
            <br />
            <br />
            </asp:Panel>
            <asp:Panel ID="pnl3" runat="server">
            <h4>Change Password</h4>
            <p>This feature will un-lock user account and change password.</p>
            <asp:Label ID="lblError3" runat="server"></asp:Label>
            <table>
                <tr>
                    <td>User Name</td>
                    <td><asp:TextBox ID="txtUserName3" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName3" ForeColor="Red" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>Current Password</td>
                    <td><asp:TextBox ID="txtCurrentPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCurrentPassword" ForeColor="Red" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>New Password</td>
                    <td><asp:TextBox ID="txtNewPassword" TextMode="Password"  runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNewPassword" ForeColor="Red" ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="btnChangePassword" runat="server" Text="Change Password" ValidationGroup="ChangePassword" OnClick="btnChangePassword_Click" /></td>
                </tr>
            </table>
            </asp:Panel>
            <asp:Panel ID="pnl4" Visible="false" runat="server">
                <p>Request Completed.</p>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                <br />
                <a href="/test.aspx">Back To the Page</a>
            </asp:Panel>
        
        </div>
    </form>
</body>
</html>
