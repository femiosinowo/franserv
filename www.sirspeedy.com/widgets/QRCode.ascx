<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QRCode.ascx.cs" Inherits="widgets_QRCode" %>
<asp:MultiView ID="ViewSet" runat="server" ActiveViewIndex="0">
    <asp:View ID="View" runat="server">
        <asp:Label ID="OutputLabel" runat="server"></asp:Label>
    </asp:View>
    <asp:View ID="Edit" runat="server">
        <div id="<%=ClientID%>_edit">
            <asp:Label ID="QRDataLabel" runat="server" Text="Data" />:
            <asp:RadioButtonList ID="QRDataRaido" runat="server">
                <asp:ListItem Text="Current Page" Selected="True" Value="" />
                <asp:ListItem Text="Custom Data" />
            </asp:RadioButtonList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="QRCustomDataText" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Label ID="SizeLabel" runat="server" Text="Size" /><br />
            <asp:DropDownList ID="SizeList" runat="server">
            <asp:ListItem Text="Small" Value="200x200" />
            <asp:ListItem Selected="True" Text="Medium" Value="300x300" />
            <asp:ListItem Text="Large" Value="500x500" />
            </asp:DropDownList>                       
            <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" /> &nbsp;&nbsp;
            <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="SaveButton_Click" />
        </div>
    </asp:View>
</asp:MultiView>