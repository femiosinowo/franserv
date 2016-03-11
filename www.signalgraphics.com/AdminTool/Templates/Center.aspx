<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Center.aspx.cs" Inherits="AdminTool_Templates_Center" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //script for complete page
        $(document).ready(function () {
            $("#tabs").tabs({activate: function() {
                //var selectedTab = $('#tabs').tabs('option', 'active');
                //$("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
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
                <%-- var faxNumber = $('.txtFax').val();
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


                if ((phoneValidation == false) || (faxValidation == false) || (stateValidation == false))
                { status = false; }
                else
                { status = true; }

                return status;
            });
        });
    </script>
    <script type="text/javascript">
        //add product & services scripts  
        $(document).ready(function () {
            $('.btnProductServices').click(function (e) {
                var status = false;   
                var selectedPSCat = $('.selectedPSCat'); 

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedPSCat.length; i++){
                    jq.context = jq[0] = selectedPSCat[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnPSIds').val(ids); 
                
                if(status == false)
                    $('.psErrorMessage').show();
                else
                    $('.psErrorMessage').hide();

                return status;
            });               
        });   

    </script>
    <script type="text/javascript">
        //add Banners scripts  
        $(function () {             
            $('.btnBanners').click(function (e) { 
                var status = false;   
                var selectedBanners = $(".selectedBanners");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedBanners.length; i++){
                    jq.context = jq[0] = selectedBanners[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedBanners').val(ids); 
                
                if(status == false)
                    $('.errorMessage').show();
                else
                    $('.errorMessage').hide();

                return status;
            });
        });
    </script>    
    <script type="text/javascript">
        //add News scripts  
        $(function () {             
            $('.btnNews').click(function (e) { 
                var status = false;   
                var selectedNews = $(".selectedNews");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedNews.length; i++){
                    jq.context = jq[0] = selectedNews[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedNews').val(ids); 
                
                if(status == false)
                    $('.errorMessage').show();
                else
                    $('.errorMessage').hide();

                return status;
            });
        });
    </script>
    <script type="text/javascript">
        //add Partners scripts  
        $(function () {             
            $('.btnPartners').click(function (e) { 
                var status = false;   
                var selectedPartners = $(".selectedPartners");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedPartners.length; i++){
                    jq.context = jq[0] = selectedPartners[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedPartners').val(ids); 
                
                if(status == false)
                    $('.errorMessage').show();
                else
                    $('.errorMessage').hide();

                return status;
            });
        });
    </script>
    <script type="text/javascript">
        //add promotional scripts  
        $(function () {             
            $('.btnPromotional').click(function (e) { 
                var status = false;   
                var selectedPromos = $(".selectedPromo");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedPromos.length; i++){
                    jq.context = jq[0] = selectedPromos[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hdnSelectedPromos').val(ids); 

                return true; //promos are optional
            });
        });
    </script>
    <script type="text/javascript">
        //add Shops scripts  
        $(function () {             
            $('.btnShops').click(function (e) { 
                var status = false;   
                var selectedShops = $(".selectedShops");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedShops.length; i++){
                    jq.context = jq[0] = selectedShops[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedShops').val(ids); 
                
                if(status == false)
                    $('.errorMessageShops').show();
                else
                    $('.errorMessageShops').hide();

                return status;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <a href="/admintool/index.aspx">Home</a> >> <a href="/admintool/templates/AllCenters.aspx">All Centers</a> >> Manage Center
    <div>
        <h3 id="addCenterTitle" runat="server">Add Center</h3>
        <h3 id="editCenterTitle" runat="server" visible="false">Edit Center:</h3>
        <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" /><br /><br />
        </div>
        <div id="tabs">
            <ul class="addCenterTabs">
                <li><a href="#tabs-1">Center Information</a></li>
                <li><a href="#tabs-2">Third Party Info</a></li>
                <li><a href="#tabs-3">Clone Center?</a></li>
                <li><a href="#tabs-4">Products & Services</a></li>
                <li><a href="#tabs-5">Banners</a></li> 
                <li><a href="#tabs-6">News</a></li>
                <li><a href="#tabs-7">Partners</a></li>               
                <li><a href="#tabs-8">Promotions</a></li>                
                <li><a href="#tabs-9">Shops</a></li>
            </ul>
            <div id="drag">
                <div id="tabs-1">
                    <asp:Panel ID="pnlCenterInfo" DefaultButton="btnAddCenter" runat="server">
                        <p class="errorMessage">
                            <asp:Label ID="lblError" runat="server" Visible="false" />
                        </p>
                        <table width="100%" border="0" cellpadding="2" summary="">
                            <tbody>
                                <tr>
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <strong>
                                            <label for="txtName">Name:</label></strong></span></td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" MaxLength="255" size="50" placeHolder="Center Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr id="franservId" runat="server">
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <strong>
                                            <label for="txtFranservId">Franserv Id:</label></strong></span></td>
                                    <td>
                                        <asp:TextBox ID="txtFranservId" runat="server" MaxLength="255" size="50" placeHolder="Franserv Id"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtFranservId" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <!--temp removing the flag such that Michael Can add centers by JS bookmark-->
                                       <%-- <asp:RegularExpressionValidator ID="regularExpressionValidator" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtFranservId" ValidationExpression="\d+" ErrorMessage="Numbers Only"></asp:RegularExpressionValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <strong>
                                            <label for="txtAddress1">Address 1:</label></strong></span></td>
                                    <td>
                                        <asp:TextBox ID="txtAddress1" runat="server" MaxLength="255" size="50" placeHolder="Address line 1"></asp:TextBox>
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
                                                <label for="fic_1342_2590">Working Hours:</label></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWorkingHoursWeekDays" runat="server" MaxLength="255" size="50" placeHolder="Week Days Working Hours like Mon-Sat 9am-9:30pm"></asp:TextBox><br />
                                        <br />
                                        <asp:TextBox ID="txtWorkingHoursWeekend" runat="server" MaxLength="255" size="50" placeHolder="Week End Working Hours like Sun 9am-8pm"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="txtEmail">Center Email:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" MaxLength="255" size="50" placeHolder="Center Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="txtEmail">Center Contact FirstName:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactFirstName" runat="server" MaxLength="255" size="50" placeHolder="Center Contact First Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtContactFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="txtEmail">Center Contact LastName:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactLastName" runat="server" MaxLength="255" size="50" placeHolder="Center Contact Last Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtContactLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtSAFEmail"><strong>Send-A-File Email:</strong></label>
                                         (to enter multiple email addresses, use the semicolon ";" to separate them)
                                                                          </span></td>
                                    <td>
                                        <asp:TextBox ID="txtSAFEmail"  runat="server" MaxLength="255" TextMode="MultiLine" Width="400" 
                                            Height="100" placeHolder="Send-A-File Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtSAFEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="AddCenter" ValidationExpression="^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                            ForeColor="Red" ControlToValidate="txtSAFEmail" ErrorMessage="Please enter valid email addresses." runat="server"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtRAQEmail"><strong>Request a Quote Email:</strong></label>
                                         (to enter multiple email addresses, use the semicolon ";" to separate them)
                                                                          </span></td>
                                    <td>
                                        <asp:TextBox ID="txtRAQEmail" runat="server" MaxLength="255" TextMode="MultiLine" Width="400" 
                                            Height="100" placeHolder="Request a Quote Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtRAQEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RAQEmailRegexValidator1" ValidationGroup="AddCenter" ValidationExpression="^^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                            ForeColor="Red" ControlToValidate="txtRAQEmail" ErrorMessage="Please enter valid email addresses." runat="server"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtWhitePaperDownloadEmail"><strong>White Paper Download Email:</strong></label>
                                         (to enter multiple email addresses, use the semicolon ";" to separate them)
                                                                          </span></td>
                                    <td>
                                        <asp:TextBox ID="txtWhitePaperDownloadEmail"  runat="server" MaxLength="255" TextMode="MultiLine" Width="400" 
                                            Height="100" placeHolder="White Paper Download Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtWhitePaperDownloadEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="AddCenter" ValidationExpression="^^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                            ForeColor="Red" ControlToValidate="txtWhitePaperDownloadEmail" ErrorMessage="Please enter valid email addresses." runat="server"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtJobApplicationEmail"><strong>Job Application Email:</strong></label>
                                         (to enter multiple email addresses, use the semicolon ";" to separate them)
                                                                          </span></td>
                                    <td>
                                        <asp:TextBox ID="txtJobApplicationEmail"  runat="server" MaxLength="255" TextMode="MultiLine" Width="400" 
                                            Height="100" placeHolder="Job Application Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtJobApplicationEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="AddCenter" ValidationExpression="^^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                            ForeColor="Red" ControlToValidate="txtJobApplicationEmail" ErrorMessage="Please enter valid email addresses." runat="server"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtSubscriptionEmail"><strong>Subscription Email:</strong></label>
                                         (to enter multiple email addresses, use the semicolon ";" to separate them)
                                                                          </span></td>
                                    <td>
                                        <asp:TextBox ID="txtSubscriptionEmail"  runat="server" MaxLength="255" TextMode="MultiLine" Width="400" 
                                            Height="100" placeHolder="Subscription Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ValidationGroup="AddCenter"
                                            ForeColor="Red" ControlToValidate="txtSubscriptionEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="AddCenter" ValidationExpression="^^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$"
                                            ForeColor="Red" ControlToValidate="txtSubscriptionEmail" ErrorMessage="Please enter valid email addresses." runat="server"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr style="display: none;">
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="txtRAQEmail"><strong>Is Center Active ?:</strong></label></span></td>
                                    <td>
                                        <asp:CheckBox ID="chkIsCenterActive" runat="server" Checked="true" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <br />
                        <asp:Button ID="btnAddCenter" CssClass="btnAddCenter AddCenterBtn" runat="server" ValidationGroup="AddCenter" Text="Save" OnClick="btnAddCenter_Click" />
                        <asp:Button ID="btnUpdateCenter" CssClass="btnAddCenter AddCenterBtn" runat="server" ValidationGroup="AddCenter" Text="Update & Continue" OnClick="btnUpdateCenter_Click" />

                    </asp:Panel>
                    <asp:Panel ID="pnlAddedMessage" Visible="false" runat="server">
                        <p>Center Successfully Added to the system!!!</p>
                    </asp:Panel>
                    <asp:Panel ID="pnlEditMessage" Visible="false" runat="server">
                        <p>Center Successfully Updated!!!</p>
                    </asp:Panel>
                </div>
                <div id="tabs-2">
                    <asp:Panel ID="pnlThirdParty" DefaultButton="btnThirdParty" runat="server">
                        <br />
                         Get National's Third-party social sites data: <asp:CheckBox ID="chkNationlalData" runat="server" AutoPostBack="true" OnCheckedChanged="chkNationlalData_CheckedChanged" /> 
                        <br />
                        <br />
                        <table width="100%" border="0" cellpadding="2" summary="">
                            <tbody>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="txtFlickrUserId">Portfolio via Flickr UserId:</label></strong></span></td>
                                    <td>
                                        <asp:TextBox ID="txtFlickrUserId" runat="server" MaxLength="255" size="50" placeHolder="Flickr UserId: 123594637@N04"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ValidationGroup="ThirdParty"
                                            ForeColor="Red" ControlToValidate="txtFlickrUserId" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <br />                                       
                                        <span><i>Note: If you don't know your Flickr User Id then login to your Flickr account and hover over "You" and then click on "Photostream". After the page re-fresh, you will find your Flickr UserId in the browser's address bar. Example: 'https://www.flickr.com/photos/123594637@N04/'</i></span>
                                    </td>
                                </tr>
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
                                <tr>
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
                                        <label for="txtMarketingTangoUrl">Marketing Tango URL:</label></span></td>
                                    <td>
                                        <asp:TextBox ID="txtMarketingTangoUrl" runat="server" MaxLength="255" size="50" placeHolder="Marketing Tango URL"></asp:TextBox>
                                    </td>
                                </tr>                                
                            </tbody>
                        </table>
                        <br />
                        <asp:Button ID="btnThirdParty" runat="server" ValidationGroup="ThirdParty" Text="Save" OnClick="btnThirdParty_Click" />
                    </asp:Panel>
                </div>
                <div id="tabs-3">
                    <asp:Panel ID="pnlCloneCenter" runat="server">
                        <p>Would you like to clone the remaining settings from an existing center?</p>
                        <br />
                        <asp:Label ID="lblCloneCenterError" runat="server" />
                        <table width="100%" border="0" cellpadding="2" summary="">
                            <tbody>
                                <tr>
                                    <td>Select a Center:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlCenterId" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="CloneCenter"
                                            ControlToValidate="ddlCenterId" InitialValue="-Select One-" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <br />
                        <asp:Button ID="btnCloneCenter" runat="server" ValidationGroup="CloneCenter" Text="Clone Center" OnClick="btnCloneCenter_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnNoCloning" runat="server" Text="No, Skip Cloning" OnClick="btnNoCloning_Click" />
                        <div id="cloneCenterSuccessMsg" class="successMessage" runat="server" visible="false">
                            Center data settings saved Successfully!!!
                        </div>
                    </asp:Panel>
                </div>
                <div id="tabs-4">
                    <asp:Panel ID="pnlProductsServices" CssClass="pnlProductsServices" runat="server">
                        <p class="note">Drag & drop the category from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available Products & Services</th>
                                    <th colspan="1" class="mark" title="Message line">Delegated Products & Services</th>
                                </tr>
                                <asp:ListView ID="lvAvailPs" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availPSCat"><%#Eval("AvailableCategory") %></td>
                                            <td class="selectedPSCat"><%#Eval("SelectedCategory") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <div style="clear: both;"></div>
                        <asp:Button ID="btnProductServices" CssClass="btnProductServices" runat="server" Text="Save" OnClick="btnProductServices_Click" />
                        <input type="hidden" id="hddnPSIds" class="hddnPSIds" runat="server" value="" />
                        <div class="psErrorMessage errorMessage" style="display: none;">
                            <p>Please select atleast One category!!!</p>
                        </div>
                    </asp:Panel>
                </div>
                <div id="tabs-5">
                    <asp:Panel ID="pnlBanners" runat="server">
                        <p class="note">Drag & drop the Banner content from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available Banners</th>
                                    <th colspan="1" class="mark" title="Message line">Delegated Banners</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvBanners" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availBanners"><%#Eval("AvailableBanners") %></td>
                                            <td class="selectedBanners"><%#Eval("SelectedBanners") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnBanners" CssClass="btnBanners" runat="server" Text="Save" OnClick="btnBanners_Click" />
                        <input type="hidden" id="hddnSelectedBanners" class="hddnSelectedBanners" runat="server" value="" />
                        <div class="errorMessage" style="display: none;">
                            <p>Please select atleast One Banner!!!</p>
                        </div>
                    </asp:Panel>
                </div>                
                <div id="tabs-6">
                    <asp:Panel ID="pnlNews" runat="server">
                        <p class="note">Drag & drop the News content from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available News</th>
                                    <th colspan="1" class="mark" title="Message line">Selected News</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvNews" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availNews"><%#Eval("AvailableNews") %></td>
                                            <td class="selectedNews"><%#Eval("SelectedNews") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnNews" CssClass="btnNews" runat="server" Text="Save" OnClick="btnNews_Click" />
                        <input type="hidden" id="hddnSelectedNews" class="hddnSelectedNews" runat="server" value="" />
                        <div class="errorMessage" style="display: none;">
                            <p>Please select atleast One News!!!</p>
                        </div>
                    </asp:Panel>
                </div>                
                <div id="tabs-7">
                    <asp:Panel ID="pnlPartners" runat="server">
                        <p class="note">Drag & drop the Partners content from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available Partners</th>
                                    <th colspan="1" class="mark" title="Message line">Selected Partners</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvPartners" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availPartners"><%#Eval("AvailablePartners") %></td>
                                            <td class="selectedPartners"><%#Eval("SelectedPartners") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnPartners" CssClass="btnPartners" runat="server" Text="Save" OnClick="btnPartners_Click" />
                        <input type="hidden" id="hddnSelectedPartners" class="hddnSelectedPartners" runat="server" value="" />
                        <div class="errorMessage" style="display: none;">
                            <p>Please select atleast One Partner!!!</p>
                        </div>
                    </asp:Panel>
                </div> 
                <div id="tabs-8">
                    <asp:Panel ID="pnlPromotions" runat="server">
                        <p class="note">Promotional Selection is OPTIONAL.</p>
                        <p class="note">Drag & drop the Promotions content from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available Promotionals</th>
                                    <th colspan="1" class="mark" title="Message line">Delegated Promotionals</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvPromotionals" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availPromo"><%#Eval("AvailablebPromo") %></td>
                                            <td class="selectedPromo"><%#Eval("SelectedPromo") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnPromotional" CssClass="btnPromotional" runat="server" Text="Save" OnClick="btnPromotional_Click" />
                        <input type="hidden" id="hdnSelectedPromos" class="hdnSelectedPromos" runat="server" value="" />
                    </asp:Panel>
                    <asp:Panel ID="pnlNoPromoResults" runat="server" Visible="false">
                        <p>No Promotionals are available in the system currently. Please Add Promotions through the following link: <a href="/AdminTool/Templates/AddPromotion.aspx">Add Promotion</a></p>
                    </asp:Panel>
                </div>                
                <div id="tabs-9">
                    <asp:Panel ID="Panel1" runat="server">
                        <p class="note">Drag & drop the Shop content from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Available Shops Content</th>
                                    <th colspan="1" class="mark" title="Message line">Selected Shops Content</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvShops" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availShops"><%#Eval("AvailableShops") %></td>
                                            <td class="selectedShops"><%#Eval("SelectedShops") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnShops" CssClass="btnShops" runat="server" Text="Save" OnClick="btnShops_Click" />
                        <input type="hidden" id="hddnSelectedShops" class="hddnSelectedShops" runat="server" value="" />
                        <div class="errorMessageShops" style="display: none;">
                            <p>Please select atleast One Shop Content!!!</p>
                        </div>
                        <div id="centerUpdateMsg" class="successMessage" runat="server" visible="false">
                            Data saved Successfully!!!
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
    </div>
</asp:Content>

