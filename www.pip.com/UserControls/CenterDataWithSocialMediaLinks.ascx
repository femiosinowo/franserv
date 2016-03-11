<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CenterDataWithSocialMediaLinks.ascx.cs" Inherits="UserControls_CenterDataWithSocialMediaLinks" %>

<div class="left_col_address" id="localInfoData" runat="server" visible="false">    
    <ul>
        <li>
            <asp:Literal ID="ltrAddress1" runat="server" /></li>
        <li>
            <asp:Literal ID="ltrAddress2" runat="server" /></li>
    </ul>
    <ul>
        <li>
            <asp:Literal ID="ltrPhone" runat="server" /></li>
        <li>
            <asp:Literal ID="ltrFax" runat="server" /></li>
        <li>
            <asp:Literal ID="ltrEmail" runat="server" /></li>
    </ul>
</div>
<asp:Literal ID="ltrSocialIcons" runat="server"></asp:Literal>