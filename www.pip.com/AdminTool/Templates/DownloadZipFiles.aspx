<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="DownloadZipFiles.aspx.cs" Inherits="AdminTool_Templates_DownloadZipFiles" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" Runat="Server">
    <asp:Panel ID="pnlZipReady" runat="server">
        <p>Your Zip file is downloading now...</p>
        <br />
        <p>Please check your computer's Downloads folder.</p>
        <br />
        <a href="#" id="backLink" runat="server">Go back</a>
    </asp:Panel>
    <iframe id="frame1" style="display:none;" src="/downloadFile.aspx" runat="server" scrolling="auto"></iframe>
</asp:Content>

