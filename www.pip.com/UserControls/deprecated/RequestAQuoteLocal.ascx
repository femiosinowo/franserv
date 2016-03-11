<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequestAQuoteLocal.ascx.cs" Inherits="UserControls_RequestAQuoteLocal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%=btnRequestQuoteLocal.ClientID%>').click(function (e) {
            var status = false;
            var status1 = false;
            var status2 = false;

            //do validation
            if ($(".terms_conditions input:checkbox").is(':checked')) {
                $('.termsLocal').hide();
                status1 = true;
            }
            else {
                $('.termsLocal').show();
                status1 = false;
                $(".terms_conditions input:checkbox").focus();
            }

            var description = $('#<%=descProject.ClientID%>').val();
            if (description === undefined || description === null || description === "") {
                //$('.desc_project').addClass('requiredField');
                status2 = false;
            } else {
                //$('.desc_project').removeClass('requiredField');
                status2 = true;
            }

            if (status1 == false || status2 == false)
                status = false;
            else
                status = true;

            //also check the validation for other controls  
            var requiredcontrolsstatus = false;
            $('.raq input.required').each(function () {
                var fieldId = $(this).attr('id');
                var fieldVal = document.getElementById(fieldId).value;
                if (fieldVal != '') {
                    requiredcontrolsstatus = true;
                    $(this).removeClass('requiredField');
                }
                else {
                    requiredcontrolsstatus = false; //alert($(this).attr('id')); 
                    $(this).addClass('requiredField');
                }
            });

            if (status == false || requiredcontrolsstatus == false)
                status = false;

            //check if file exist on the server
            if (status) {
                if ((($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo')) &&
                 ($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo').length > 1)) ||
                ($('.hddn_upload_files').val() != '')) {
                    $('.safWaitImg').show();
                    MoveStatusBar();
                    //trigger fileupload control to save files
                    $('#ajaxFileUploadLocal_UploadOrCancelButton').click();
                    return false;
                }
                else {
                    $('.safWaitImg').show();
                    SendRAQData();
                    return false;
                }
            }
            return status;
        });
    });

    function CheckBoxRequired_ClientValidate(sender, e) {
        e.IsValid = $(".terms_conditions input:checkbox").is(':checked');
    }

    //ajax upload control code starts here
    function ClientUploadLocalComplete(sender, e) {
        var fileName = e._fileName.toLowerCase();
        if (fileName.indexOf(".exe") > -1 || fileName.indexOf(".bat") > -1 || fileName.indexOf(".cmd") > -1) {
            ClientUploadError();
            e.stopPropagation();
            e.preventDefault();
        }
        else {
            //FormatUploadControl_Jobs();
            MoveStatusBar();
            $('.hddn_upload_files').val('done');
            $('.upload_files').removeAttr('required');
            var id = e.get_fileId();
            var objHdnFileIDs = document.getElementById('<%=hdnFileIDs.ClientID%>');
            objHdnFileIDs.value = objHdnFileIDs.value + id + ',';
        }
    }

    function ClientUploadLocalCompleteAll(sender, e) {
        //doing file check with shared drive        
        var hdnUploadedFiles = document.getElementById('<%=hdnFileIDs.ClientID%>').value;
        var pageType = 'requestquotefiles';
        var fransId = '';

        //save form data
        //call Handler  
        SendRAQData();
    }

    function SendRAQData() {
        var firstName = JSON.stringify(document.getElementById('<%=fname.ClientID%>').value);
        var lastName = JSON.stringify(document.getElementById('<%=lname.ClientID%>').value);
        var emailAddress = document.getElementById('<%=email.ClientID%>').value;
        var selectedCenter = $('.hddnCenterId').val();
        var phoneNumber = JSON.stringify(document.getElementById('<%=phone.ClientID%>').value);
        var jobTitle = JSON.stringify(document.getElementById('<%=txtJobTitle.ClientID%>').value);
        var companyName = JSON.stringify(document.getElementById('<%=company.ClientID%>').value);
        var projectName = JSON.stringify(document.getElementById('<%=pname.ClientID%>').value);
        var projectBudget = JSON.stringify(document.getElementById('<%=txtProjectBudget.ClientID%>').value);
        var projectDescription = JSON.stringify(document.getElementById('<%=descProject.ClientID%>').value);
        var uploadedFilesIds = document.getElementById('<%=hdnFileIDs.ClientID%>').value;

        var responseData = '';
        $.ajax({
            type: "POST",
            url: "/Handlers/SaveREQData.ashx",
            data: '{"firstName": ' + firstName + ',"lastName":' + lastName + ',"emailAddress":"' + emailAddress + '","selectedCenter":"' + selectedCenter + '","phoneNumber":' + phoneNumber + ',"jobTitle": ' + jobTitle + ',"companyName":' + companyName + ',"projectName":' + projectName + ',"projectDescription":' + projectDescription + ',"uploadedFilesIds":"' + uploadedFilesIds + '","projectBudget":' + projectBudget + ' }',
            contentType: "application/json; charset=utf-8",
            dataType: "text",
            async: false,
            cache: false,
            success: function (response) {
                //console.log(response);
                responseData = response;
            },
            error: function (response, status, error) {
                alert(error);
                ga('send', 'event', 'Error Message In Browser', 'Display Error Message Saving Data');
                $('.safWaitImg').hide();
                $('.hddn_upload_files').val('');
                document.getElementById('<%=hdnFileIDs.ClientID%>').value = '';
                $('.uploadedState').html('');
            }
        });

        if (responseData != '') {
            alert(responseData);
            $('.safWaitImg').hide();
            $('.hddn_upload_files').val('');
            document.getElementById('<%=hdnFileIDs.ClientID%>').value = '';
            $('.uploadedState').html('');
        }
        else {
            var thankYouPage = '/thank-you/?type=requestaquote&centerId=';
            var localSiteCenterId = $('.hddnCenterId').val();
            if (localSiteCenterId != '')
                thankYouPage = '/' + localSiteCenterId + thankYouPage + localSiteCenterId;
            else
                thankYouPage = thankYouPage + selectedCenter;
            window.location.href = thankYouPage;
        }
    }

    function ClientUploadLocalError() {
        FormatUploadControl_Quotes();
        $('.hddn_upload_files').val('');
        $('.upload_files').attr('required', 'required');
        $('.ajax__fileupload_queueContainer').html('');
        $('.ajax__fileupload_footer').html('');
        $('.ajax__fileupload_queueContainer').css('visibility', 'hidden');
        alert('Sorry, the file type you selected is not supported. Please try again with a valid file.');
        $('.safWaitImg').hide();
    }

    function FormatUploadControl_Quotes() {
        var userAgent = window.navigator.userAgent;
        var msie = userAgent.indexOf("MSIE ");
        var trident = userAgent.indexOf('Trident/');
        //if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If IE
        if (msie > 0) // If IE
        {
            var ieVersion = parseInt(userAgent.substring(msie + 5, userAgent.indexOf(".", msie)));
            if (ieVersion > 9) {
                setTimeout(function () {
                    $('#ajaxFileUploadLocal_Html5InputFile').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                    $('#ajaxFileUploadLocal_SelectFileContainer').hide();
                }, 200);
            }
        }
        else {
            setTimeout(function () {
                $('#ajaxFileUploadLocal_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                $('#ajaxFileUploadLocal_SelectFileContainer').hide();
            }, 200);
        }
    }

    function OpenBrowseWindow() {
        $('#ajaxFileUploadLocal_Html5InputFile').click();
    }

    function MoveStatusBar() {
        var firstPendingUploadFile = $('.pendingState').first();
        $('.ajax__fileupload_footer').appendTo(firstPendingUploadFile);
    }

    $(document).ready(function () {
        FormatUploadControl_Quotes();
        Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "X";
    });
    //ajax upload control code ends here
</script>

<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 1 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
<div class="grid_8 request_quote_wrapper clearfix choose_center raq">
    <div class="request_quote">
        <h2>Your Location</h2>
        <div id="rq_location_map">
            <asp:HiddenField ID="hiddenCenterLat" runat="server" Value="" />
            <asp:HiddenField ID="hiddenCenterLong" runat="server" Value="" />
            <img alt="map" src="#" id="googleMapImage" runat="server" />
            <!-- DESKTOP/TABLET BUTTON -->
            <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server" title="Your Location" href="#">VIEW MAP</a>
            <!-- Mobile Link: go to google map page on devices smaller than 768px -->
            <a class="red-text view-map-mobile" id="viewDirectionMobile" runat="server" title="Your Location" target="_blank" href="#">VIEW MAP</a>
        </div>
        <!-- end rq_location_map -->
        <asp:Literal runat="server" ID="litFranchiseContactInfo"></asp:Literal>
    </div>
    <!-- end request_quote -->
</div>
<!-- end request_quote_wrapper -->
<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 2 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->

<div class="grid_8 request_quote_wrapper clearfix contact_info raq">
    <div class="request_quote">
        <h2>Contact Information</h2>
        <div class="rq_form_body clearfix">
            <div class="form" id="contact_project_info">
                <asp:TextBox ID="fname" placeholder="First Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="fname"
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="lname" placeholder="Last Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="lname" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="txtJobTitle" placeholder="Job Title" runat="server"   />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtJobTitle" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>--%>

                <asp:TextBox ID="company" placeholder="Company" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="company" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="email" placeholder="Email Address" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="email" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="phone" runat="server" placeholder="Phone Number"   class="no-margin-bottom required" />
                <span class="required">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="phone" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>               
            </div>
            <p class="sample-entry" style="display: none;">Example 845-222-6666</p>
        </div>
        <!-- end rq_form_body -->
    </div>
    <!-- end request_quote -->

</div>
<!-- end request_quote_wrapper -->

<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 3 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
<div class="grid_8 request_quote_wrapper clearfix project_info project_info_national raq">
    <div class="request_quote">
        <h2>Project Information</h2>
        <div class="rq_form_body clearfix">
            <div class="form" id="project_info">
                <asp:TextBox ID="pname" placeholder="Project Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="pname" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>
                <asp:TextBox ID="descProject" placeholder="Describe Your Project" CssClass="descProject required" runat="server"   Rows="4" Columns="20" />
      <%--          <textarea cols="20" rows="4" placeholder="Describe Your Project" runat="server" class="descProject required"   id="descProject"></textarea>--%>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="descProject" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>
                <div class="clear"></div>
                <div class="drag_drop_file">
                    <div class="drag_drop_placeholder">                        
                        <AjaxToolkit:AjaxFileUpload ID="ajaxFileUploadLocal" CssClass="ajaxFileUploadLocal" BorderStyle="None" runat="server"
                            OnUploadComplete="Upload_LocalComplete"
                            OnClientUploadComplete="ClientUploadLocalComplete"                                                      
                            ClientIDMode="Static"
                            OnClientUploadCompleteAll="ClientUploadLocalCompleteAll"
                            OnClientUploadError="ClientUploadLocalError"
                            ThrobberID="loader" />
                        <asp:HiddenField id="hdnFileIDs" runat="server" Value="" />                            
                         <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                         <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                    </div>
                    <!-- end drag_drop_placeholder -->
                    <%--<input type="text" id="upload_files" class="upload_files"  >--%>
                    <asp:TextBox ID="txtUploadFiles" style=" display:none;"   CssClass="hddn_upload_files" runat="server"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfv3" Style="margin: 0px;" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtUploadFiles"
                       ValidationGroup="requestQuote">*</asp:RequiredFieldValidator>--%>
                    <br />
                </div>
                <!-- end drag_drop_file 
               <span class="required">*</span>-->              
                <div class="clear"></div>               
                <asp:TextBox ID="txtProjectBudget" runat="server" placeholder="Project Budget" /> 
                <div class="clear"></div>
                <p class="terms_conditions">
                    <input type="checkbox" id="termsNational" runat="server"  ClientValidationFunction="CheckBoxRequired_ClientValidate" />
                    <label for="terms" class="small-text">I agree with the <a href="/Terms-and-Conditions/" class="red-text">Terms &amp; Conditions</a></label>
                    <span class="errorMessage termsLocal" style="display:none;">*You must select this box to proceed.</span>
                </p>
            </div>
        </div>
        <!-- end rq_form_body -->
    </div>
    <!-- end request_quote -->
</div>
<!-- end request_quote_wrapper -->

<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm END COLUMNS!
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->

<div class="clear"></div>
<div class="request_quote_tagline">
    <div class="grid_20">
        <div class="statement-text">
            <p>We're here to help. Tell us about your next project.</p>
        </div>
        <!-- end statement-text -->
    </div>
    <!-- end grid_20 -->
    <div class="grid_4">
        <div>
            <div id="fileAlreadyExist" Class="required"></div>
            <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
            <asp:ValidationSummary ID="validationSummaryRequestQuoteNational" CssClass="required" HeaderText="Please fill out all the required fields(*). " 
                  ShowSummary="true" runat="server" ValidationGroup="requestQuote" />
            <span class="safWaitImg" style="display: none;" id="safWaitImg">
                  <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
             </span>
        </div>
        <div class="form" id="request_quote_local_submit">
             <asp:Button ID="btnRequestQuoteLocal" CssClass="request_quote_submit btnRequestQuoteLocal" runat="server"
                Text="Send" ValidationGroup="requestQuote" />
        </div>
    </div>
    <!-- end grid_4 -->
</div>
<!-- end request_quote_tagline -->
