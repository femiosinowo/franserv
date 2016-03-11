<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/NoAuthentication.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="AdminTool_Templates_ForgotPassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <div>
        <h3>Forgot Password:</h3>
        <br />
        <asp:Panel ID="pnlNewRequest" runat="server">
            <table>

                <tr>
                    <td>Centers:</td>
                    <td>
                        <asp:DropDownList ID="ddlCenterId" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Center Name is required."
                            ControlToValidate="ddlCenterId" ValidationGroup="CheckUser" InitialValue="-Select One-" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>UserName:</td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="UserName is required"
                            ControlToValidate="txtUserName" ValidationGroup="CheckUser" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <asp:Button ID="btnForgotPassword" runat="server" ValidationGroup="CheckUser" Text="Reset Password" OnClick="btnForgotPassword_Click" />
                    </td>
                    <td>
                        <br />
                        <asp:Label CssClass="errorMessage" runat="server" ID="lblNewReq"></asp:Label></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="ChangePassword" Visible="false" runat="server">
            <div>
                <table>
                    <tr>
                        <td>New Password:</td>
                        <td>
                            <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="New Password is required"
                                ControlToValidate="txtNewPassword" ValidationGroup="ChangePassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>Confirm Password:</td>
                        <td>
                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Confirm Password is required"
                                ControlToValidate="txtConfirmPassword" ValidationGroup="ChangePassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnChangePassword" runat="server" ValidationGroup="ChangePassword" Text="Change Password" OnClick="btnChangePassword_Click" />
                        </td>
                        <td>
                            <asp:Label CssClass="errorMessage" runat="server" ID="errMessage"></asp:Label>
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
        </asp:Panel>
    </div>
</asp:Content>
