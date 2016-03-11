<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" ValidateRequest="false" 
    CodeFile="Career.aspx.cs" Inherits="AdminTool_Templates_Career" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/Manage-Careers.aspx">Manage All Careers</a> >> Career
    <h3>Manage Career</h3>
    <asp:Panel ID="pnlJobProfile" runat="server">
        <asp:Label CssClass="errorMessage" ID="lblError" runat="server"></asp:Label>
        <table>
            <tbody>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtJobTitle">Job Title:</label></strong></span></td>
                    <td>
                        <asp:TextBox ID="txtJobTitle" runat="server" PlaceHolder="Job Title" ValidationGroup="AddJob" size="30" MaxLength="255" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requiredField1" runat="server" ValidationGroup="AddJob"
                            ControlToValidate="txtJobTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="txtLocation">Location:</label></span></td>
                    <td>
                        <asp:TextBox ID="txtLocation" runat="server" PlaceHolder="Location" ValidationGroup="AddJob" size="30" MaxLength="255" TabIndex="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddJob"
                            ControlToValidate="txtLocation" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="ddlCareerType">Career Type:</label></strong></span></td>
                    <td>
                        <asp:DropDownList ID="ddlCareerType" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ValidationGroup="AddProfile"
                            ControlToValidate="ddlCareerType" InitialValue="-Select One-" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="chkFullTime">Is Full Time Job ?:</label></strong></span></td>
                    <td>
                        <asp:CheckBox ID="chkFullTime" runat="server" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="chkPartTime">Is Part Time Job ?:</label></strong></span></td>
                    <td>
                        <asp:CheckBox ID="chkPartTime" runat="server" />
                    </td>
                </tr>
                <tr id="jobPostedDate" runat="server">
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="datePosted">Date Posted:</label></strong></span></td>
                    <td>
                        <ektronUI:Datepicker ID="datePosted" runat="server"></ektronUI:Datepicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddProfile"
                            ControlToValidate="datePosted" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="dateExpire">Date Expire:</label></strong></span></td>
                    <td>
                        <ektronUI:Datepicker ID="dateExpire" runat="server"></ektronUI:Datepicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddProfile"
                            ControlToValidate="dateExpire" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </tbody>
        </table>
        <table>
            <tr>
                <td class="CS_Form_Required_Baseline">
                    <span class="CS_Form_Required_Baseline"><br />
                        <label for="txtJobDescription">Job Description:</label></span><br /><br />
                    <asp:TextBox runat="server" ID="txtJobDescription" TextMode="MultiLine" Columns="50" Rows="10" />
                    <ajaxToolkit:HtmlEditorExtender EnableSanitization="false" ID="htmlEditorExtender1"
                        TargetControlID="txtJobDescription" runat="server">
                    </ajaxToolkit:HtmlEditorExtender>
                </td>
                <td>
                    <div id="populateNationalJob" runat="server" style="padding-left:30px;">
                        <span class="CS_Form_Required_Baseline">
                        <label for="txtJobDescription">Default Job Description:</label></span><br />
                    <asp:RadioButtonList ID="radioButtonList" AutoPostBack="true" OnSelectedIndexChanged="nationalJObDesc_CheckedChanged" runat="server"></asp:RadioButtonList>
                    </div>                    
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
                    <asp:Button ID="btnAddJobPost" CssClass="btnAddJobPost" runat="server" Text="Save" ValidationGroup="AddJob" OnClick="btnAddJobPost_Click" /></td>
                <td>                    
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlSaveMessage" Visible="false" runat="server">
        <p>Job post saved Successfully!!!</p>
    </asp:Panel>
</asp:Content>

