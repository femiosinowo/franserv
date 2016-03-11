<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="AddLanguage.aspx.cs" Inherits="AdminTool_Templates_AddLanguage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" Runat="Server">
    <div>
        <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/ManageLanguages.aspx">Manage Languages</a> >> Add Language
        <h3 align="left">Add Langauges</h3>
        <asp:Panel ID="pnlAddLang" runat="server">
            <div>
                <asp:Label CssClass="errorMessage" ID="lblError" Visible="false" runat="server"></asp:Label>
            </div>            
            <asp:TextBox ID="txtAddLang" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAddLang" ForeColor="Red"
                ValidationGroup="AddLang">Enter Language Name</asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Button ID="btnAddLang" runat="server" Text="Add Language" ValidationGroup="AddLang" OnClick="btnAddLang_Click" />
            <br />
            <div class="successMessage" id="addLangMessage" runat="server" visible="false">
                Language Added Successfully!!!
            </div>
        </asp:Panel>
   </div>
</asp:Content>

