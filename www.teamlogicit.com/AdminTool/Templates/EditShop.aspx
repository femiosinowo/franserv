<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="EditShop.aspx.cs" Inherits="AdminTool_Templates_AddTestimonial" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">    
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/Center-Manage-Shop.aspx">All Shops Content</a> >> Manage Shop
    <h3>Edit Shop Content:</h3>
    <asp:Panel ID="pnlManageTestimonial" runat="server" DefaultButton="btnUpdateShop">
        <asp:Label CssClass="errorMessage" ID="lblError" runat="server"></asp:Label>
        <table>
            <tbody>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">                        
                            <label for="ltrTitle">Title:</label></span></td>
                    <td>
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">                        
                            <label for="ltrTeaser">Teaser:</label></span></td>
                    <td>
                       <asp:Literal ID="ltrTeaser" runat="server"></asp:Literal>
                    </td>
                </tr>                
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="shopImg">Image:</label></span></td>
                    <td>
                        <img alt="" src="#" id="shopImg" runat="server" />                        
                    </td>
                </tr>              
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtStatement">Shop Content URL:</label></strong></span></td>
                    <td>
                        <asp:TextBox runat="server" Width="300" ID="txtLink" />
                        <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="txtLink" ValidationGroup="UpdateShop"
                             ForeColor="Red" ErrorMessage="*" ></asp:RequiredFieldValidator> 
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
        <asp:Button ID="btnUpdateShop" runat="server" Text="Save" ValidationGroup="UpdateShop" OnClick="btnUpdateShop_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlUpdateShopMsg" CssClass="successMessage" runat="server" Visible="false">
        <br />
        <p class="successMessage">Shop Content URL saved Successfully.</p>
    </asp:Panel>
</asp:Content>

