﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="CenterProfile.aspx.cs" Inherits="AdminTool_Templates_Profile" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <script type="text/javascript">
        //add center scripts
        $(document).ready(function () {            
            $('.btnAddProfile').click(function (e) {                
                var status = false;
                var rx = /^\d{3}\-?\d{3}\-?\d{4}$/;
                var workPhoneValidation = true;
                var officePhoneValidation = true;
                var faxValidation = true;
                var mobileValidation = true;

                //work phone validation
                var phoneNumber = $('.txtWorkPhone').val();
                if (!$('#<%= workPhoneIsInternatioanl.ClientID %>').is(':checked')) {
                    if (rx.test(phoneNumber)) {
                        $('.txtWorkPhone').removeClass('addClassError');
                        workPhoneValidation = true;
                    }
                    else {
                        $('.txtWorkPhone').addClass('addClassError');
                        workPhoneValidation = false;
                    }
                }
                else {
                    $('.txtWorkPhone').removeClass('addClassError');
                    workPhoneValidation = true;
                }

                //office phone validation
                <%--var officePhoneNumber = $('.txtOfficePhone').val();
                if (!$('#<%= officePhoneIsInternatioanl.ClientID %>').is(':checked')) {
                    if (rx.test(officePhoneNumber)) {
                        $('.txtOfficePhone').removeClass('addClassError');
                        officePhoneValidation = true;
                    }
                    else {
                        $('.txtOfficePhone').addClass('addClassError');
                        officePhoneValidation = false;
                    }
                }
                else {
                    $('.txtOfficePhone').removeClass('addClassError');
                    officePhoneValidation = true;
                }--%>

                //fax validation
                <%--var faxNumber = $('.txtFax').val();
                if (!$('#<%= faxIsInternatioanl.ClientID %>').is(':checked')) {
                    if (faxNumber != '') {
                        if (rx.test(faxNumber)) {
                            $('.txtFax').removeClass('addClassError');
                            faxValidation = true;
                        }
                        else {
                            $('.txtFax').addClass('addClassError');
                            faxValidation = false;
                        }
                    }
                }
                else {
                    $('.txtFax').removeClass('addClassError');
                    faxValidation = true;
                }--%>

                //mobile validation
                var mobileNumber = $('.txtMobile').val();
                if (!$('#<%= mobileIsInternatioanl.ClientID %>').is(':checked')) {
                    if (rx.test(mobileNumber)) {
                        $('.txtMobile').removeClass('addClassError');
                        mobileValidation = true;
                    }
                    else {
                        $('.txtMobile').addClass('addClassError');
                        mobileValidation = false;
                    }
                }
                else {
                    $('.txtMobile').removeClass('addClassError');
                    mobileValidation = true;
                }


                if ((workPhoneValidation == false) || (officePhoneValidation == false) || (faxValidation == false) || mobileValidation == false)
                { status = false; }
                else
                { status = true; }

                return status;
            });
        });
    </script>
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/AllCenterProfiles.aspx">All Profiles</a> >> Manage Profile
    <div id="drag">
         <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div> 
        <h3 id="addCenterTitle" runat="server">Add Profile:</h3>
        <h3 id="editCenterTitle" runat="server" visible="false">Edit Profile:</h3>
        <asp:Label ID="lblError" CssClass="errorMessage" Visible="false" runat="server"></asp:Label>
        <asp:Panel ID="pnlAddProfile" runat="server" DefaultButton="btnAddProfile">
        <div id="tabs">
            <table>
                <tbody>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="txtFirstName">First Name:</label></strong></span></td>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" placeHolder="First Name" ValidationGroup="AddProfile" size="30" MaxLength="255" TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requiredField1" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtFirstName" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="txtLastName">Last Name:</label></strong></span></td>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" placeHolder="Last Name" ValidationGroup="AddProfile" size="30" MaxLength="255" TabIndex="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtLastName" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="ddlGender">Gender:</label></strong></span></td>
                        <td>
                            <asp:DropDownList ID="ddlGender" runat="server" TabIndex="3">
                                <asp:ListItem Selected="True" Value="-Select One-">-Select One-</asp:ListItem>
                                <asp:ListItem Value="Male">Male</asp:ListItem>
                                <asp:ListItem Value="Female">Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="ddlGender" InitialValue="-Select One-" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="CS_Form_Label_Baseline"><strong>
                                <label for="txtWorkPhone">Work Phone:</label></strong></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkPhone" CssClass="txtWorkPhone" runat="server" placeHolder="Work Phone" TabIndex="4" size="30" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtWorkPhone" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span style="white-space: nowrap; font-size: 75%;">format: 555-555-1234</span><br />
                            <asp:CheckBox ID="workPhoneIsInternatioanl" CssClass="workPhoneIsInternatioanl" runat="server" />
                            Is International?
                        </td>
                    </tr>
                   <!-- <tr>
                        <td>
                            <span class="CS_Form_Label_Baseline">
                                <label for="txtOfficePhone">Office Phone:</label></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOfficePhone" CssClass="txtOfficePhone" runat="server" placeHolder="Office Phone" TabIndex="5" size="30" MaxLength="255"></asp:TextBox>
                            <span style="white-space: nowrap; font-size: 75%;">format: 555-555-1234</span><br />
                            <asp:CheckBox ID="officePhoneIsInternatioanl" CssClass="officePhoneIsInternatioanl" runat="server" />
                            Is International?
                        </td>
                    </tr>-->
                    <tr>
                        <td>
                            <span class="CS_Form_Label_Baseline">
                                <strong><label for="txtMobile">Mobile:</label></strong></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMobile" CssClass="txtMobile" runat="server" placeHolder="Mobile Number" TabIndex="7" size="30" MaxLength="255"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtMobile" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <span style="white-space: nowrap; font-size: 75%;">format: 555-555-1234</span><br />
                            <asp:CheckBox ID="mobileIsInternatioanl" CssClass="mobileIsInternatioanl" runat="server" />
                            Is International?
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span class="CS_Form_Label_Baseline">
                                <label for="txtFax">Fax:</label></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFax" CssClass="txtFax" runat="server" placeHolder="Fax Number" TabIndex="6" size="30" MaxLength="255"></asp:TextBox>
                            <span style="white-space: nowrap; font-size: 75%;">format: 555-555-1234</span><br />
                            <asp:CheckBox ID="faxIsInternatioanl" CssClass="faxIsInternatioanl" runat="server" />
                            Is International?
                        </td>
                    </tr>                    
                    <tr>
                        <td>
                            <span class="CS_Form_Label_Baseline"><strong>
                                <label for="fic_2269_2296">Email:</label></strong></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeHolder="Email" ValidationGroup="AddProfile" size="30" MaxLength="255" TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtEmail" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="fic_2269_2297">IM Screen Name:</label></span></td>
                        <td>
                            <asp:TextBox ID="txtIMScreenName" placeHolder="IM Screen Name" size="30" MaxLength="255" TabIndex="9" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="ddlIMList">IM Service:</label></span></td>
                        <td>
                            <asp:DropDownList ID="ddlIMList" TabIndex="10" runat="server">
                                <asp:ListItem Value="">-Select One-</asp:ListItem>
                                <asp:ListItem Value="AIM">AIM</asp:ListItem>
                                <asp:ListItem Value="Google Talk">Google Talk</asp:ListItem>
                                <asp:ListItem Value="ICQ">ICQ</asp:ListItem>
                                <asp:ListItem Value="Skype">Skype</asp:ListItem>
                                <asp:ListItem Value="Windows Live">Windows Live</asp:ListItem>
                                <asp:ListItem Value="Yahoo">Yahoo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="txtTitle">Title:</label></span></td>
                        <td>
                            <asp:TextBox ID="txtTitle" placeHolder="Title" size="30" MaxLength="255" TabIndex="11" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="fic_2269_2307">Company:</label></span></td>
                        <td>
                            <asp:TextBox ID="txtCompany" placeHolder="Company" size="30" MaxLength="255" TabIndex="12" runat="server" />
                        </td>
                    </tr>
                    <tr style="visibility: visible;">
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="txtBio">Bio:</label></span></td>
                        <td>
                            <textarea id="txtBio" placeholder="Bio" cols="40" rows="8" runat="server" tabindex="13"></textarea>                            
                        </td>
                    </tr>                    
                    <tr style="visibility: visible;">
                        <td><span class="CS_Form_Label_Baseline">
                            <label for="imageUpload">Picture:</label></span></td>
                        <td>
                            <img alt="" src="#" id="userImage" visible="false" runat="server" />
                            <asp:FileUpload ID="imageUpload" TabIndex="14" runat="server" />
                        </td>
                    </tr>
                    <tr id="rolesOptions" runat="server" visible="false">
                        <td><span class="CS_Form_Required_Baseline">
                            <strong><label for="ddlRole">Roles:</label></strong></span></td>
                        <td>
                            <asp:DropDownList ID="ddlRole" TabIndex="15" runat="server">
                                <asp:ListItem Selected="True" Value="Center User">Center User</asp:ListItem> 
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="ddlRole" InitialValue="-Select One-" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>                    
                    <tr id="userName" runat="server">
                        <td>
                            <span class="CS_Form_Label_Baseline"><strong>
                                <label for="fic_2269_2305">Username:</label></strong></span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtUserName" placeHolder="User Name" size="30" MaxLength="255" TabIndex="16" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtUserName" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><span class="CS_Form_Required_Baseline">
                            <strong><label for="txtPassword">Password:</label></strong></span></td>
                        <td>
                            <asp:TextBox ID="txtPassword" placeHolder="Password" TextMode="Password" size="30" MaxLength="255" TabIndex="17" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="AddProfile"
                                ControlToValidate="txtPassword" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                            <strong><label for="ddlCenterId">IsActive:</label></strong></span></td>
                        <td>
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAddProfile" CssClass="btnAddProfile" runat="server" Text="Save" ValidationGroup="AddProfile" OnClick="btnAddProfile_Click" />
                            <asp:Button ID="btnUpdateProfile" CssClass="btnAddProfile" runat="server" Text="Update" ValidationGroup="AddProfile" OnClick="btnUpdateProfile_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        </asp:Panel>
        <asp:Panel ID="pnlAddProfileMsg" CssClass="successMessage" runat="server" Visible="false">
            <p>Profile successfully Created!!!</p>
        </asp:Panel>
        <asp:Panel ID="pnlEditProfileMsg" CssClass="successMessage" runat="server" Visible="false">
            <p>Profile successfully updated!!!</p>
        </asp:Panel>
        <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
    </div>
</asp:Content>
