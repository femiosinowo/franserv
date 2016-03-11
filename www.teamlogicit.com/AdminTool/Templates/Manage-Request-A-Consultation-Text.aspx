<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Manage-Request-A-Consultation-Text.aspx.cs" Inherits="AdminTool_Templates_Manage_Request_A_Consultation_Text" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">

    <h3 align="left">Manage Request A ConsultationText&nbsp;</h3>
    <p align="left">This&nbsp;section&nbsp;enables&nbsp;you to&nbsp; manage the&nbsp;the Request A Consultation text that local franchisees use.</p>
    <br />
    <br />
    <table>
        <tbody>
            <tr>
                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">

                    <label for="txtContentRequestConsultation"><strong>Request Consultation Title :</strong></label></span></td>
                <td>
                    <asp:TextBox ID="txtTitleRequestConsultation" runat="server" MaxLength="100" size="50" placeHolder="Title"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator1" runat="server" ValidationGroup="AddConsultation"
                        ControlToValidate="txtTitleRequestConsultation" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">

                    <label for="txtContentRequestConsultation"><strong>Request Consultation Text (4000 Charater Limit): </strong></label></span></td>
                <td>
                    <textarea id="txtContentRequestConsultation" cols="62" rows="6" runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddConsultation"
                        ControlToValidate="txtContentRequestConsultation" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">

                    <label for="txtContentFreeConsultation"><strong>Free Consultation Title :</strong></label></span></td>
                <td>
                    <asp:TextBox ID="txtTitleContentFreeConsultation" runat="server" MaxLength="100" size="50" placeHolder="Title"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator3" runat="server" ValidationGroup="AddConsultation"
                        ControlToValidate="txtTitleContentFreeConsultation" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">

                    <label for="txtContentRequestConsultation"><strong>Free Consultation Text (4000 Charater Limit) :</strong></label></span></td>
                <td>
                    <textarea id="txtContentFreeConsultation" cols="62" rows="6" runat="server"></textarea>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator4" runat="server" ValidationGroup="AddConsultation"
                        ControlToValidate="txtContentFreeConsultation" ForeColor="Red">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <asp:Button ID="SaveButton1" CssClass="btnConsultingProject" runat="server" Text="Save" OnClick="SaveButton1_Click" />
    <br/>
    <asp:Label runat="server" ID="statusLabel1" class="successMessage"></asp:Label>
</asp:Content>

