<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ResponsiveImage.ascx.cs" Inherits="Widgets_ResponsiveImage" %>

<asp:MultiView ID="ViewSet" runat="server">
    <asp:View ID="View" runat="server">
        <asp:PlaceHolder ID="phContent" runat="server">
            <asp:Label ID="errorLb" runat="server" />
            <div id="container">
                <asp:Literal ID="ltrImage" runat="server"></asp:Literal>
                <script language="javascript" type="text/javascript">
                    Ektron.ready(function () {
                        $ektron("#picture<%=uniqueId%>").picture();
                });
                </script>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phHelpText" runat="server">
            <div id="divHelpText" runat="server" style="font: normal 12px/15px arial; width: 100%; height: 100%;">
                Click on the 'Edit' icon (<img alt="edit icon" title="edit icon" src="<%=appPath %>PageBuilder/PageControls/Themes/TrueBlue/images/edit_on.png" width="12" height="12" border="0" />) in the top-right corner of this widget
                to select an image you wish to display.
            </div>
        </asp:PlaceHolder>
    </asp:View>
    <asp:View ID="Edit" runat="server">
        <div id="<%=uniqueId%>" class="ImageWidget">
            <div class="CBEdit">
                <div class="responsiveImageWidget-info clearfix">
                    <asp:Panel ID="uxEditError" CssClass="error" Visible="false" runat="server">
                        <asp:Label ID="uxEditErrorText" runat="server" />
                    </asp:Panel>
                    <div class="responsiveImageWidget-thumbWrapper">
                        <asp:Panel id="noThumb" CssClass="responsiveImageWidget-noThumb" Visible="false" runat="server">No image selected</asp:Panel>
                        <asp:Image ID="imageThumb" CssClass="responsiveImageWidget-thumb" Visible="false" runat="server" />
                    </div>
                    <div class="responsiveImageWidget-Fields clearfix">
                        <ektronUI:Label ID="fileNameLabel" AssociatedControlID="imageSource" CssClass="responsiveImageWidget-label" Text="File Name:" runat="server" />
                        <ektronUI:Label ID="imageSource" CssClass="responsiveImageWidget-imageSource" runat="server" />
                        <asp:HiddenField ID="hdnImageSource" runat="server" />

                        <ektronUI:Label ID="toolTipLabel" AssociatedControlID="toolTip" CssClass="responsiveImageWidget-label" Text="Title:" runat="server" />
                        <ektronUI:TextField ID="toolTip" runat="server" />

                        <ektronUI:Label ID="borderLabel" AssociatedControlID="txtBorder" CssClass="responsiveImageWidget-label" Text="Border:" runat="server" />
                        <asp:TextBox ID="txtBorder" CssClass="imgborder" runat="server" Text="0" MaxLength="3" onkeypress="return AllowOnlyNumeric(event);" oncopy="return MouseClickEvent();" onpaste="return MouseClickEvent();" oncut="return MouseClickEvent();"></asp:TextBox>
                    </div>
                </div>
                <iframe id="mediaManagerIframe" class="responsiveImageWidget-mediaManager" frameborder="0" border="0" runat="server" width="100%" />
                <input type="hidden" id="hdnFolderId" class="hdnFolderId responsiveImageWidget-hdnFolderId" name="hdnFolderId" value="-1"  runat="server" />
                <input type="hidden" id="hdnContentId" class="hdnContentId responsiveImageWidget-hdnContentId" name="hdnContentId" value="0"
                    runat="server" />
                <input type="hidden" id="hdnMediaManagerPath" class="hdnMediaManagerPath responsiveImageWidget-hdnMediaManagerPath" name="hdnMediaManagerPath" value="0"
                    runat="server" />
                <div class="CBEditControls">
                    <ektronUI:Button ID="toggleMediaManager" DisplayMode="Anchor" CssClass="responsiveImageWidget-toggleMediaManager" runat="server" Text="Select Image" OnClientClick="return Ektron.PFWidgets.Image.toggleMediaManager(this);" />
                    <ektronUI:Button ID="SaveButton" CssClass="CBSave" DisplayMode="Anchor" runat="server" Text="Save" OnClick="SaveButton_Click" OnClientClick="Ektron.PFWidgets.Image.Save(this);" />
                    <ektronUI:Button ID="CancelButton"  CssClass="CBCancel" DisplayMode="Anchor" runat="server" Text="Cancel" OnClick="CancelButton_Click" />                    
                </div>
            </div>
        </div>
    </asp:View>
</asp:MultiView>