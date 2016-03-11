<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/NoAuthentication.master" AutoEventWireup="true" 
    CodeFile="login.aspx.cs" Inherits="AdminTool_login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
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

                    <td>
                    </td>
                </tr>
                  <tr>
                    <td>                        
                       <asp:HyperLink runat="server" ID="forgotPasswordPg" NavigateUrl="~/AdminTool/Templates/ForgotPassword.aspx" Text="Forgot Password"></asp:HyperLink>
                    </td>

                    <td>
                    </td>
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
</asp:Content>

