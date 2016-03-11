<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="AddTestimonial.aspx.cs" Inherits="AdminTool_Templates_AddTestimonial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/Center-Manage-Testimonials.aspx">All Testimonials</a> >> Manage Testimonial
    <h3>Manage Testimonial:</h3>
    <asp:Panel ID="pnlManageTestimonial" runat="server" DefaultButton="btnAddTestimonial">
        <asp:Label CssClass="errorMessage" ID="lblError" runat="server"></asp:Label>
        <table>
            <tbody>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtTitle">Title:</label></strong>*</span></td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" PlaceHolder="Title" size="30" MaxLength="255" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requiredField1" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtFirstName">First Name:</label></strong></span></td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server" placeHolder="First Name" size="30" MaxLength="255" TabIndex="2"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtFirstName" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtLastName">Last Name:</label></strong></span></td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" placeHolder="Last Name" size="30" MaxLength="255" TabIndex="3"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtLastName" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong><label for="txtCompany">Company:</label></strong>*</span></td>
                    <td>
                        <asp:TextBox ID="txtCompany" runat="server" placeHolder="Company" size="30" MaxLength="255" TabIndex="4"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="rqvdCompany" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtCompany" ForeColor="Red" >*</asp:RequiredFieldValidator>
                    </td>
                </tr>  
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong><label for="txtCompany">Image:</label></strong>(400 * 400)</span></td>
                    <td>
                        <img alt="" src="#" id="testimonialImg" runat="server" visible="false" />
                        <br />
                        <asp:FileUpload ID="imgUpload" runat="server" />
                         <asp:Label ID="lblImageError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    </td>
                </tr>              
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtStatement">Statement/Testimonial:</label></strong></span></td>
                    <td>
                        <asp:TextBox runat="server" ID="txtStatement" TextMode="MultiLine" Columns="50" Rows="10" TabIndex="5" />
                       <%-- <ajaxToolkit:HtmlEditorExtender EnableSanitization="false" ID="HtmlEditorExtender1"
                            TargetControlID="txtStatement" runat="server">
                        </ajaxToolkit:HtmlEditorExtender>--%>
                    </td>
                </tr>
            </tbody>
        </table>
        <br />
        <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
        <asp:Button ID="btnAddTestimonial" runat="server" Text="Save" ValidationGroup="AddTestimonial" OnClick="btnAddTestimonial_Click" />
    </asp:Panel>
    <asp:Panel ID="pnlAddTestimonialMsg" CssClass="successMessage" runat="server" Visible="false">
        <p>Testimonial saved Successfully.</p>
    </asp:Panel>
</asp:Content>

