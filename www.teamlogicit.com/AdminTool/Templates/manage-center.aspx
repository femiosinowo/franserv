<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="manage-center.aspx.cs" Inherits="AdminTool_Templates_manage_center" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />    
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript">
        //script for complete page
        $(document).ready(function () {
            $("#tabs").tabs({activate: function() {
                var selectedTab = $('#tabs').tabs('option', 'active');
                $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
            },
                active: <%= hdnSelectedTab.Value %>
                });           
        });
    </script>
    <script type="text/javascript">
        //add center scripts
        $(function () {           

            $('.btnAddCenter').click(function (e) {              
                var status = false;
                var rx = /^\d{3}\-?\d{3}\-?\d{4}$/;
                var phoneValidation = true;
                var faxValidation = true;
                var stateValidation = true;
                var consultationValidation = true;

                //phone validation
                var phoneNumber = $('.txtPhone').val();

                if (!$('#<%= chkPhoneInternational.ClientID %>').is(':checked')) {
                    if (rx.test(phoneNumber)) {
                        $('.txtPhone').removeClass('addClassError');
                        phoneValidation = true;
                    }
                    else {
                        $('.txtPhone').addClass('addClassError');
                        phoneValidation = false;
                    }
                }
                else {
                    $('.txtPhone').removeClass('addClassError');
                    phoneValidation = true;
                }

                //fax validation
               <%--var faxNumber = $('.txtFax').val();
                if (!$('#<%= chkFaxInternational.ClientID %>').is(':checked')) {
                    if (rx.test(faxNumber)) {
                        $('.txtFax').removeClass('addClassError');
                        faxValidation = true;
                    }
                    else {
                        $('.txtFax').addClass('addClassError');
                        faxValidation = false;
                    }
                }
                else {
                    $('.txtFax').removeClass('addClassError');
                    faxValidation = true;
                }--%>

                var seledtedStateVal = $('.ddlState').val();
                if (seledtedStateVal == 'N/A') {
                    var stateVal = $('.txtInternationalState').val();
                    if (stateVal == "") {
                        $('.txtInternationalState').addClass('addClassError');
                        stateValidation = false;
                    } else {
                        $('.txtInternationalState').removeClass('addClassError');
                        stateValidation = true;
                    }
                }
                else {
                    $('.txtInternationalState').removeClass('addClassError');
                    stateValidation = true;
                }              
                
                var selectedConsultationVal = $('#<%= ddlConsultation.ClientID%>').val();
                if (selectedConsultationVal != '0') {
                    var consultationVal = $('#<%= txtRequestConsultation.ClientID%>').val();
                    if (consultationVal == "") {
                        $('#<%= txtRequestConsultation.ClientID%>').addClass('addClassError');
                        consultationValidation = false;
                    } else {
                        $('#<%= txtRequestConsultation.ClientID%>').removeClass('addClassError');
                        consultationValidation = true;
                    }
                }
                else {
                    $('#<%= txtRequestConsultation.ClientID%>').removeClass('addClassError');
                    consultationValidation = true;
                }


                if ((phoneValidation == false) || (faxValidation == false) || (stateValidation == false) || (consultationValidation == false))
                { status = false; }
                else
                { status = true; }

                return status;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
  
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Business Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>    
    <h3>Manage Business Information:</h3>
    <br />       
    <asp:Panel ID="pnlCenterInfo" runat="server">
        <p class="errorMessage">
            <asp:Label ID="lblError" runat="server" Visible="false" />
        </p>
         <div id="tabs">
            <ul class="addCenterTabs">
                <li><a href="#tabs-1">Business Info</a></li>                
                <li><a href="#tabs-2">Manage Social Media Links</a></li>
            </ul>
            <div id="tabs-1">
                <asp:Panel ID="Panel1" DefaultButton="btnUpdateCenter" runat="server">
                    <p class="errorMessage">
                        <asp:Label ID="Label1" runat="server" Visible="false" />
                    </p>
                    <table width="100%" border="0" cellpadding="2" summary="">
                        <tbody>
                            <tr>
                                <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                    <strong>
                                        <label for="txtName">Name:</label></strong></span></td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="255" size="50" ReadOnly="true" placeHolder="Center Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <strong>
                                        <label for="txtAddress1">Address 1:</label></strong></span></td>
                                <td>
                                    <asp:TextBox ID="txtAddress1" runat="server" MaxLength="255" ReadOnly="true" size="50" placeHolder="Address line 1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtAddress1" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                    <label for="txtAddress2">Address 2:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtAddress2" runat="server" MaxLength="255" size="50" placeHolder="Address line 2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <strong>
                                        <label for="txtCity">City:</label></strong></span></td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="255" size="50" placeHolder="City"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtCity" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <strong>
                                        <label for="ddlState">State:</label></strong></span></td>
                                <td>
                                    <asp:DropDownList ID="ddlState" CssClass="ddlState" runat="server">
                                        <asp:ListItem Selected="True" Value="--Select--">--Select--</asp:ListItem>
                                        <asp:ListItem Value="N/A">N/A</asp:ListItem>
                                        <asp:ListItem Value="AL">AL</asp:ListItem>
                                        <asp:ListItem Value="AK">AK</asp:ListItem>
                                        <asp:ListItem Value="AZ">AZ</asp:ListItem>
                                        <asp:ListItem Value="AR">AR</asp:ListItem>
                                        <asp:ListItem Value="CA">CA</asp:ListItem>
                                        <asp:ListItem Value="CO">CO</asp:ListItem>
                                        <asp:ListItem Value="CT">CT</asp:ListItem>
                                        <asp:ListItem Value="DC">DC</asp:ListItem>
                                        <asp:ListItem Value="DE">DE</asp:ListItem>
                                        <asp:ListItem Value="FL">FL</asp:ListItem>
                                        <asp:ListItem Value="GA">GA</asp:ListItem>
                                        <asp:ListItem Value="HI">HI</asp:ListItem>
                                        <asp:ListItem Value="ID">ID</asp:ListItem>
                                        <asp:ListItem Value="IL">IL</asp:ListItem>
                                        <asp:ListItem Value="IN">IN</asp:ListItem>
                                        <asp:ListItem Value="IA">IA</asp:ListItem>
                                        <asp:ListItem Value="KS">KS</asp:ListItem>
                                        <asp:ListItem Value="KY">KY</asp:ListItem>
                                        <asp:ListItem Value="LA">LA</asp:ListItem>
                                        <asp:ListItem Value="ME">ME</asp:ListItem>
                                        <asp:ListItem Value="MD">MD</asp:ListItem>
                                        <asp:ListItem Value="MA">MA</asp:ListItem>
                                        <asp:ListItem Value="MI">MI</asp:ListItem>
                                        <asp:ListItem Value="MN">MN</asp:ListItem>
                                        <asp:ListItem Value="MS">MS</asp:ListItem>
                                        <asp:ListItem Value="MO">MO</asp:ListItem>
                                        <asp:ListItem Value="MT">MT</asp:ListItem>
                                        <asp:ListItem Value="NE">NE</asp:ListItem>
                                        <asp:ListItem Value="NV">NV</asp:ListItem>
                                        <asp:ListItem Value="NH">NH</asp:ListItem>
                                        <asp:ListItem Value="NJ">NJ</asp:ListItem>
                                        <asp:ListItem Value="NM">NM</asp:ListItem>
                                        <asp:ListItem Value="NY">NY</asp:ListItem>
                                        <asp:ListItem Value="NC">NC</asp:ListItem>
                                        <asp:ListItem Value="ND">ND</asp:ListItem>
                                        <asp:ListItem Value="OH">OH</asp:ListItem>
                                        <asp:ListItem Value="OK">OK</asp:ListItem>
                                        <asp:ListItem Value="OR">OR</asp:ListItem>
                                        <asp:ListItem Value="PA">PA</asp:ListItem>
                                        <asp:ListItem Value="PR">PR</asp:ListItem>
                                        <asp:ListItem Value="RI">RI</asp:ListItem>
                                        <asp:ListItem Value="SC">SC</asp:ListItem>
                                        <asp:ListItem Value="SD">SD</asp:ListItem>
                                        <asp:ListItem Value="TN">TN</asp:ListItem>
                                        <asp:ListItem Value="TX">TX</asp:ListItem>
                                        <asp:ListItem Value="UT">UT</asp:ListItem>
                                        <asp:ListItem Value="VT">VT</asp:ListItem>
                                        <asp:ListItem Value="VA">VA</asp:ListItem>
                                        <asp:ListItem Value="WA">WA</asp:ListItem>
                                        <asp:ListItem Value="WV">WV</asp:ListItem>
                                        <asp:ListItem Value="WI">WI</asp:ListItem>
                                        <asp:ListItem Value="WY">WY</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="ddlState" InitialValue="--Select--" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>International State:
                                    <asp:TextBox ID="txtInternationalState" CssClass="txtInternationalState" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" class="CS_Form_Label_Baseline">
                                    <span class="CS_Form_Label_Baseline"><strong>
                                        <label for="txtZipcode">Zip:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtZipcode" runat="server" MaxLength="255" size="50" placeHolder="Zip Code"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtZipcode" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:CheckBox ID="chkZipInternational" CssClass="chkZipInternational" runat="server" />
                                    Is International?
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                    <strong>
                                        <label for="txtCountry">Country:</label></strong></span></td>
                                <td>
                                    <asp:DropDownList ID="ddlCountryList" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="-Select One-" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="ddlCountryList" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="CS_Form_Label_Baseline"><strong>
                                        <label for="txtPhone">Phone:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPhone" CssClass="txtPhone" runat="server" MaxLength="255" size="50" placeHolder="Phone Number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    format: 555-555-1234<br />
                                    <asp:CheckBox ID="chkPhoneInternational" CssClass="chkPhoneInternational" runat="server" />
                                    Is International?
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="CS_Form_Label_Baseline">                                       
                                            <label for="fic_1342_2590">Fax:</label></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFax" CssClass="txtFax" runat="server" MaxLength="255" size="50" placeHolder="Fax Number"></asp:TextBox>
                                        format: 555-555-1234<br />
                                    <asp:CheckBox ID="chkFaxInternational" CssClass="chkFaxInternational" runat="server" />
                                    Is International?
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span class="CS_Form_Label_Baseline">
                                        <strong>
                                            <label for="fic_1342_2590">Working Hours:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkingHoursWeekDays" runat="server" MaxLength="255" size="50" placeHolder="Week Days Working Hours like Mon-Sat 9am-9:30pm"></asp:TextBox><br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtWorkingHoursWeekDays" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:TextBox ID="txtWorkingHoursWeekend" runat="server" MaxLength="255" size="50" placeHolder="Week End Working Hours like Sun 9am-8pm"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtWorkingHoursWeekend" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" class="CS_Form_Label_Baseline">
                                    <span class="CS_Form_Label_Baseline"><strong>
                                        <label for="txtEmail">Business Email:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Business Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Label_Baseline">
                                    <span class="CS_Form_Label_Baseline"><strong>
                                        <label for="txtEmail">Business Contact FirstName:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactFirstName" runat="server" MaxLength="255" size="50" placeHolder="Business Contact First Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtContactFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="CS_Form_Label_Baseline">
                                    <span class="CS_Form_Label_Baseline"><strong>
                                        <label for="txtEmail">Business Contact LastName:</label></strong></span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContactLastName" runat="server" MaxLength="255" size="50" placeHolder="Business Contact Last Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="AddCenter"
                                        ForeColor="Red" ControlToValidate="txtContactLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                             <tr style="display:none;">
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <label for="txtSAFEmail"><strong>Send-A-File Email:</strong></label></span></td>
                                <td>
                                    <asp:TextBox ID="txtSAFEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Send-A-File Email"></asp:TextBox>
                                </td>
                            </tr>
                             <tr style="display:none;">
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <label for="txtRAQEmail"><strong>Request a Quote Email:</strong></label></span></td>
                                <td>
                                    <asp:TextBox ID="txtRAQEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Request a Quote Email"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtWhitePaperDownloadEmail"><strong>White Paper Download Email:</strong></label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtWhitePaperDownloadEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="White Paper Download Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtWhitePaperDownloadEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtJobApplicationEmail"><strong>Job Application Email:</strong></label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtJobApplicationEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Job Application Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtJobApplicationEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtSubscriptionEmail"><strong>Subscription Email:</strong></label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtSubscriptionEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Subscription Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtSubscriptionEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                             <tr>
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <label for="txtClientLoginURL">Client Login URL:</label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtClientLoginURL" TextMode="Url" runat="server" MaxLength="255" size="50" placeHolder="Client Login URL like http://www.teamlogicit.com/login"> </asp:TextBox>
                                    </td>
                                </tr>
                              <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtRequestConsultation"><strong>Request Consultation Email:</strong></label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtRequestConsultation" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Request Consultation Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRequestConsultation" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtRequestConsultation" ErrorMessage="*" Enabled="False"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                         <tr>
                        <td><span class="CS_Form_Required_Baseline">
                            <label for="ddlConsultation">Consultation Messaging:</label></span></td>
                        <td>
                            <asp:DropDownList ID="ddlConsultation"  runat="server" >                               
                            </asp:DropDownList>
                        </td>
                    </tr>  
                            <tr style="display:none;">
                                <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                    <label for="txtRAQEmail"><strong>Is Center Active ?:</strong></label></span></td>
                                <td>
                                    <asp:CheckBox ID="chkIsCenterActive" runat="server" Checked="true" />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>     
                                    <br />
                                    <br />                               
                                    <asp:Button ID="btnUpdateCenter" CssClass="btnAddCenter" runat="server" ValidationGroup="AddCenter" Text="Save" OnClick="btnUpdateCenter_Click" />
                                    <br />
                                    <div id="centerInfoUpdateMsg" class="successMessage" runat="server" visible="false">
                                        Center data saved Successfully!!!
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>                       
            <div id="tabs-2">
                <asp:Panel ID="pnlThirdParty" DefaultButton="btnThirdParty" runat="server">
                    <table width="100%" border="0" cellpadding="2" summary="">
                        <tbody>                            
                            <tr>
                                <td><span>
                                    <strong>
                                        <label for="txtFlickrUserId">Twitter Feed URL:</label></strong></span></td>
                                <td>
                                    <asp:TextBox ID="txtTwitterFeedUrl" runat="server" MaxLength="255" size="50" placeHolder="Twitter Feed URL: 'https://twitter.com/sirspeedycorp'"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ValidationGroup="ThirdParty"
                                        ForeColor="Red" ControlToValidate="txtTwitterFeedUrl" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtFB">Facebook URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtFB" runat="server" MaxLength="255" size="50" placeHolder="Facebook URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtTwitter">Twitter URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtTwitterUrl" runat="server" MaxLength="255" size="50" placeHolder="Twitter URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtGooglePlus">Google+ URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtGooglePlus" runat="server" MaxLength="255" size="50" placeHolder="Google+ URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtLinkedinUrl">LinkedIn URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtLinkedinUrl" runat="server" MaxLength="255" size="50" placeHolder="LinkedIn URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtStumbleUrl">Stumble Upon URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtStumbleUrl" runat="server" MaxLength="255" size="50" placeHolder="Stumble Upon URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td><span>
                                    <label for="txtFlickrUrl">Flickr URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtFlickrUrl" runat="server" MaxLength="255" size="50" placeHolder="Flickr URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtYouTubeUrl">Youtube URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtYouTubeUrl" runat="server" MaxLength="255" size="50" placeHolder="Youtube URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td><span>
                                    <label for="txtMarketingTangoUrl">ITInflections URL:</label></span></td>
                                <td>
                                    <asp:TextBox ID="txtMarketingTangoUrl" runat="server" MaxLength="255" size="50" placeHolder="ITInflections URL"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnThirdParty" runat="server" ValidationGroup="ThirdParty" Text="Save" OnClick="btnThirdParty_Click" />
                                <br />
                                    <div id="thirdPartyInfoMsg" class="successMessage" runat="server" visible="false">
                                        Third Party data saved Successfully!!!
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </asp:Panel>
            </div>
        </div>
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnCenterId" runat="server" Value="" />
    </asp:Panel>
</asp:Content>

