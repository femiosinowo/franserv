<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="EditTestimonial.aspx.cs" Inherits="AdminTool_Templates_AddTestimonial" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="scriptManager" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/Manage-Testimonials.aspx">All Testimonials</a> >> Manage Testimonial
    <h3>Manage Testimonial:</h3>
    <asp:Panel ID="pnlManageTestimonial" runat="server" DefaultButton="btnAddTestimonial">
        <p><b>*Note</b>: For best results, please make sure that the content you paste into this text box is free from formatting. To ensure your text is clean, copy & paste your text into a program such as Notepad or TextEdit and remove all formatting before pasting into the testimonials text box. 
        The recommended character length, for testimonials that you choose to display through out your site on pages like Managed Services, Consulting & Projects, etc, is 225 characters. </p>
        <br />
        <asp:Label CssClass="errorMessage" ID="lblError" runat="server"></asp:Label>
        <table>
            <tbody>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtTitle">Title:</label></strong></span></td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" PlaceHolder="Title" size="30" MaxLength="255" TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requiredField1" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtTitle" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <label for="txtFirstName">First Name:</label></span></td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server" placeHolder="First Name" size="30" MaxLength="255" TabIndex="2"></asp:TextBox>                     
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <label for="txtLastName">Last Name:</label></span></td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" placeHolder="Last Name" size="30" MaxLength="255" TabIndex="3"></asp:TextBox>        
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                       <strong> <label for="txtCompany">Company:</label></strong></span></td>
                    <td>
                        <asp:TextBox ID="txtCompany" runat="server" placeHolder="Company" size="30" MaxLength="255" TabIndex="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtCompany" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>  
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="txtPhone">Phone:</label></span></td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" placeHolder="Phone Number" size="30" MaxLength="255" TabIndex="5"></asp:TextBox>
                    </td>
                </tr> 
                                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="txtCompany">Email:</label></span></td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" placeHolder="Email Address" size="30" MaxLength="255" TabIndex="6"></asp:TextBox>
                    </td>
                </tr> 
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="txtCompany">Image:</label></span></td>
                    <td>
                        <img alt="" src="#" id="testimonialImg" runat="server" visible="false" />
                        <br />
                        <asp:FileUpload ID="imgUpload" runat="server" />
                    </td>
                </tr>              
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <strong>
                            <label for="txtStatement">Statement/Testimonial:</label></strong></span></td>
                    <td>
                        <textarea id="txtStatement" cols="50" rows="10" runat="server" tabindex="7" /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddTestimonial"
                            ControlToValidate="txtStatement" ForeColor="Red">*</asp:RequiredFieldValidator>       
                    </td>
                </tr>
                <tr>
                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                        <label for="chkIsNational">IsNational:</label></span></td>
                    <td>  
                        <br />                      
                        <asp:CheckBox ID="chkIsNational" runat="server" />
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

