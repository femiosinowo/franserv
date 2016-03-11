<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/NoAuthentication.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="AdminTool_Templates_ChangePassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
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
</asp:Content>
