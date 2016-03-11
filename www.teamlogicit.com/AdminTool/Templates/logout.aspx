<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="AdminTool_Templates_logout" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <h3>Log out:</h3>
    <p align="left">
       <strong>Are you sure you want to log out ?</strong>
    </p>
    <br />
    <asp:Button ID="btnLogOut" Text="Yes, I want to log out."  runat="server" OnClick="btnLogOut_Click" />&nbsp;&nbsp;
    <asp:Button ID="btnCancel" Text="No, I want to stay logged in." runat="server" OnClick="btnCancel_Click" />
</asp:Content>

