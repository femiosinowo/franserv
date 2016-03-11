<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FormControl.ascx.cs" Inherits="widgets.WidgetsFormControl" %>
<asp:MultiView ID="ViewSet" runat="server">
    <asp:View ID="View" runat="server">
        <ektron:FormControl ID="uxFormControl" runat="server" Visible="true" DynamicParameter="ekfrm" />
        <asp:Label ID="errorLb" runat="server" />
    </asp:View>
    <asp:View ID="Edit" runat="server">
        <div id="<%=ClientID%>" class="FCWidget">
            <div class="FCEdit">
                <asp:Label ID="editError" runat="server" />
                <div class="BySearch FCTabPanel">
                    <asp:Label ID="uxFormIdLabel" runat="server" Text="Form Id:" AssociatedControlID="uxFormId" />
                    <asp:TextBox ID="uxFormId" runat="server"></asp:TextBox>
                </div>
                <div class="FCEditControls">
                    <asp:Button ID="CancelButton" CssClass="FCCancel" runat="server" UseSubmitBehavior="false" Text="Cancel" OnClick="CancelButton_Click" />
                    <asp:Button ID="SaveButton" CssClass="FCSave" UseSubmitBehavior="false" runat="server" Text="Save" OnClick="SaveButton_Click"/>
                </div>
            </div>
    </asp:View>
</asp:MultiView>
