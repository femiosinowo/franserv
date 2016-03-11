<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Why_we_are_diff.aspx.cs" 
    Inherits="AdminTool_Templates_Why_we_are_diff" ValidateRequest="false"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <AjaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server"></AjaxToolkit:ToolkitScriptManager>
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Business Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>
    <h3>Manage Our Story:</h3>
    <asp:Panel ID="pnlForm" runat="server">
        <div id="tabs">
            <asp:Label ID="lblError" CssClass=" errorMessage" runat="server"></asp:Label>
            <div id="updateContentMsg" runat="server" class="successMessage" visible="false">
                Data Saved Successfully!!!
            </div>
            <br />
            <table>
                <tbody>
                    <tr style="display:none;">
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong>
                                <label for="txtBannerTitle">Banner Title:</label></strong></span></td>
                        <td>
                            <asp:TextBox ID="txtBannerTitle" runat="server" Text="Why We Are Different" ValidationGroup="AddProfile" size="30" MaxLength="255" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredField1" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtBannerTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <label for="txtBannerSubTitle">Banner Sub-Title:</label></span></td>
                        <td>
                            <textarea id="txtBannerSubTitle" cols="62" rows="6" runat="server" tabindex="2"></textarea>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong>
                                <label for="txtContentTitle">Content Title:</label></strong></span></td>
                        <td>
                            <asp:TextBox ID="txtContentTitle" runat="server" Width="505px" Text="Our Story" size="30" MaxLength="255" TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtContentTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong>
                                <label for="txtContentTagLine">We're Local Copy (225 Charater Limit):</label></strong></span></td>
                        <td>
                            <textarea id="txtContentTagLine" cols="62" rows="6" runat="server" tabindex="2"></textarea>
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtContentTagLine" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="txtDescription">Content Description1:</label></strong></span></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Columns="50" Rows="10" />
                            <ajaxToolkit:HtmlEditorExtender EnableSanitization="false" ID="htmlEditorExtender"
                                TargetControlID="txtDescription" runat="server">
                            </ajaxToolkit:HtmlEditorExtender>
                            <asp:RequiredFieldValidator ID="rfv4" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtDescription" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="display:none;">
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <label for="txtDescription">Video Link:</label></span></td>
                        <td>
                            <asp:TextBox ID="txtVideoLink" Width="505px" PlaceHolder="http://player.vimeo.com/video/34414313?title=0&amp;byline=0&amp;portrait=0" runat="server" size="30" MaxLength="255" TabIndex="6"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="txtVideoStatementText">Content Description2:</label></strong></span></td>
                       <td>
                            <asp:TextBox runat="server" ID="txtVideoStatementText" TextMode="MultiLine" Columns="50" Rows="10" />
                            <ajaxToolkit:HtmlEditorExtender EnableSanitization="false" ID="htmlEditorExtender1"
                                TargetControlID="txtVideoStatementText" runat="server">
                            </ajaxToolkit:HtmlEditorExtender>
                           <asp:RequiredFieldValidator ID="rfv5" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtVideoStatementText" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
                            <asp:Button ID="btnAddContent" CssClass="btnAddProfile" runat="server" Text="Save" ValidationGroup="AddProfile" OnClick="btnAddContent_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <asp:HiddenField ID="hdnCenterId" runat="server" Value="" />
    </asp:Panel>
</asp:Content>

