<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SocialRegisterLocal.ascx.cs"
    Inherits="UserControls_SocialRegisterLocal" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<script type="text/javascript">
    function ClientUploadComplete(sender, e) {
        var fileName = e._fileName.toLowerCase();
        if(fileName.indexOf(".exe") > -1 || fileName.indexOf(".bat") > -1 || fileName.indexOf(".cmd") > -1)
        {
            e.stopPropagation();
            e.preventDefault();
            ClientUploadError();            
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

    function ClientUploadCompleteAll(sender, e)
    {
        //save form data
        //call Handler 
        SendSAFData();
    }

    function ClientUploadError() {
        FormatUploadControl_Jobs();
        $('.hddn_upload_files').val('');
        $('.upload_files').attr('required', 'required');
        $('.ajax__fileupload_queueContainer').html('');
        $('.ajax__fileupload_footer').html('');   
        $('.ajax__fileupload_queueContainer').css('visibility', 'hidden');
        alert('Sorry, the file type you selected is not supported. Please try again with a valid file.');
        $('.safWaitImg').hide();
        return false;
    }

    function FormatUploadControl_Jobs() {         
        var userAgent = window.navigator.userAgent;
        var msie = userAgent.indexOf("MSIE ");
        var trident = userAgent.indexOf('Trident/');
        //if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If IE       
        if (msie > 0 || trident > 0) // If IE
        {            
            var ieVersion = parseInt(userAgent.substring(msie + 5, userAgent.indexOf(".", msie)));           
            if ((ieVersion > 9) || (ieVersion == 'NaN' && trident > 0)) {
                setTimeout(function () {
                    $('#ajaxFileUpload_Html5InputFile').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                    $('#ajaxFileUpload_SelectFileContainer').hide();
                }, 200);
            }            
        }        
        else {
            setTimeout(function () {
                $('#ajaxFileUpload_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                $('#ajaxFileUpload_SelectFileContainer').hide();
            }, 200);
        }
    } 

    function OpenBrowseWindow() {
        $('#ajaxFileUpload_Html5InputFile').click();
    }

    function MoveStatusBar() {
        var firstPendingUploadFile = $('.pendingState').first();
        $('.ajax__fileupload_footer').appendTo(firstPendingUploadFile);
    }

    function SendSAFData()
    {
        var firstName = JSON.stringify(document.getElementById('<%=fname.ClientID%>').value);
        var lastName  = JSON.stringify(document.getElementById('<%=lname.ClientID%>').value);
        var emailAddress  = document.getElementById('<%=txtEmail.ClientID%>').value;
        var selectedCenter  = document.getElementById('<%=selectedCenter.ClientID%>').value;
        var phoneNumber  = JSON.stringify(document.getElementById('<%=phone.ClientID%>').value);
        var jobTitle  = JSON.stringify(document.getElementById('<%=job_title.ClientID%>').value);
        var companyName  = JSON.stringify(document.getElementById('<%=company.ClientID%>').value);
        var projectName  = JSON.stringify(document.getElementById('<%=project_name.ClientID%>').value);
        var projectQuantity  = JSON.stringify(document.getElementById('<%=project_quantity.ClientID%>').value);
        var projectDueDate  = JSON.stringify(document.getElementById('DueDate_iso').value);<%--JSON.stringify(document.getElementById('<%=project_due_date.ClientID%>').value);--%>
        var projectDescription  = JSON.stringify(document.getElementById('<%=project_desc.ClientID%>').value);
        var uploadedFilesIds  = document.getElementById('<%=hdnFileIDs.ClientID%>').value;
        var externalFileLinks = document.getElementById('<%=hdnFileLinks.ClientID%>').value;

        var responseData = '';
        $.ajax({
            type: "POST",
            url: "/Handlers/SaveSAFData.ashx",
            data: '{"firstName": ' + firstName + ',"lastName":' + lastName + ',"emailAddress":"' + emailAddress + '","selectedCenter":"' + selectedCenter + '","phoneNumber":' + phoneNumber + ',"jobTitle": ' + jobTitle + ',"companyName":' + companyName + ',"projectName":' + projectName + ',"projectQuantity":' + projectQuantity + ',"projectDueDate":' + projectDueDate + ',"projectDescription":' + projectDescription + ',"uploadedFilesIds":"' + uploadedFilesIds + '","externalFileLinks":"' + externalFileLinks + '" }',
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
            
        if(responseData != '')
        {
            alert(responseData);
            $('.safWaitImg').hide();
            $('.hddn_upload_files').val('');
            document.getElementById('<%=hdnFileIDs.ClientID%>').value = '';
            $('.uploadedState').html('');
        }
        else
        {
            var thankYouPage ='/thank-you/?type=sendafile&centerId=';
            var localSiteCenterId =$('.hddnCenterId').val();
            if(localSiteCenterId != '') 
                thankYouPage = '/' + localSiteCenterId + thankYouPage + localSiteCenterId;
            else
                thankYouPage = thankYouPage + selectedCenter;
            window.location.href = thankYouPage;
        }
    }

    $(document).ready(function () {

        $('ul.transformSelectDropdown li').click(function () {
            var selectedStateVal = $(this).find('span').text();
            if (selectedStateVal != undefined && selectedStateVal != '- State -') {
                $('.txtStateName').val(selectedStateVal);
                $('#job_app .transformSelect li:first').removeClass('requiredField');
            }
            else {
                $('.txtStateName').val('');
                $('#job_app .transformSelect li:first').addClass('requiredField');
            }
        });

        $('.coverNotes').keyup(function () {
            var tlength = $(this).val().length;
            $(this).val($(this).val().substring(0, 350));
        });

        FormatUploadControl_Jobs();
        Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "X";        

        //submit button click
        $('.btnSubmitSAF').click(function (e) { 
            var fieldsStatus = false;
            $('.guest_register_saf input.required').each(function(){
                var fieldId = $(this).attr('id')
                var fieldVal = document.getElementById(fieldId).value;
                if(fieldVal != ''){
                    fieldsStatus = true;
                }
                else{
                    fieldsStatus = false; //alert($(this).attr('id')); 
                }
            });

            if(fieldsStatus)
            {
                //check if file is selected via google drive, box, drop box
                var externalAppLink = document.getElementById('<%=hdnFileLinks.ClientID%>').value;
                if((($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo')) && 
                     ($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo').length > 1)) || 
                    ($('.hddn_upload_files').val() != '') || (externalAppLink != ''))
                {
                    $('.safWaitImg').show();
                    //check if files are selected
                    if($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo').length > 1)
                    {
                        MoveStatusBar();
                        //trigger fileupload control to save files
                        $('#ajaxFileUpload_UploadOrCancelButton').click();
                    }
                    else
                    {
                        SendSAFData();
                    }
                    return false;
                }
                else
                {
                    e.stopPropagation();
                    e.preventDefault();
                    $('.safWaitImg').hide();
                    alert('Please upload the files to continue.');                
                    return false;
                }   
            }
            else
            {
                return false;
            }     
        });

    });
   </script>
<div class="clear"></div>
<!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
<div class="header_image_wrapper clearfix">
    <div class="header_image">
        <div class="header_image_content">
            <CMS:ContentBlock ID="cbHeader" runat="server" CacheInterval="300" DynamicParameter="id" />
        </div>
        <!-- header image-->
    </div>
    <!-- end header_image -->
</div>
<!-- end header_image_wrapper-->
<div class="clear"></div>
<!-- mmm Send File Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Send File Content mmm -->
<div class="send_file_content_wrapper  clearfix">
    <div class="send_file_content clearfix">
        <div id="social_send_file_content" class="container_24">
            <div class="send_file_main grid_18 col-height-equal">
                <div class="headline-block headline-block-white">
                    <div class="headline-content-outer">
                        <div class="headline-content-inner">
                            <span class="headline-block-icon-white"></span>
                            <h2 class="headline">Send a File</h2>
                        </div>
                        <!--headline content-->
                    </div>
                    <!--headline content-->
                </div>
                <div class="prefix_1 grid_22 suffix_1 alpha guest_register_saf">
                    <div style="margin-top: 10px;">
                        <div id="fileAlreadyExist" class="required"></div>
                        <asp:Label ID="lblError" runat="server" CssClass="form-required"></asp:Label>
                        <asp:ValidationSummary ID="validationSummary" CssClass="required" HeaderText="Please fill out all the required fields(*). "
                            ShowSummary="true" runat="server" ValidationGroup="SocialRegister" />
                    </div> 
                    <div class="clear"></div>
                    <div id="contact_project_info_pageform" class="form">
                        <h3>Contact Information</h3>
                        <div class="send_file_contact_info">
                            <ul>
                                <li>
                                    <asp:Label runat="server" ID="lblUserName" style="margin-left:10px"></asp:Label></li>
                                <li>
                                    <asp:Label runat="server" ID="lblUserEmail"></asp:Label></li>
                            </ul>
                            <asp:HiddenField ID="fname" runat="server" Value="" />
                            <asp:HiddenField ID="lname" runat="server" Value="" />
                            <asp:HiddenField ID="selectedCenter" runat="server" Value=""></asp:HiddenField>
                        </div>
                        <asp:TextBox ID="phone" CssClass="no-margin-bottom required" runat="server" placeholder="Phone Number*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="phone" CssClass="required"
                            ValidationGroup="SocialRegister" ErrorMessage="Phone Number is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        <p class="sample-entry">Example 845-222-6666</p>
                        <hr />
                        <div class="clear"></div>
                        <h3>Let Us Know More About You</h3>
                        <p id="emailFieldSection" runat="server" style="display:none;">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="required" placeholder="Email*"></asp:TextBox>                   
                        </p>
                        <p>
                            <%--<input type="text" runat="server" required="required" placeholder="Job Title" id="job_title" />--%>
                            <asp:TextBox ID="job_title" runat="server" placeholder="Job Title"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="job_title"
                                CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </p>
                        <p>
                            <%--<input type="text" runat="server" required="required" placeholder="Company" id="company" />--%>
                            <asp:TextBox ID="company" runat="server" placeholder="Company"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="company"
                                CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        </p>
                        <p>
                            <%--<input type="text" runat="server" required="required" placeholder="Website" id="website" />--%>
                            <asp:TextBox ID="website" runat="server" CssClass="required" placeholder="Website*"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="website"
                                CssClass="required" ValidationGroup="SocialRegister" ErrorMessage="Website is required" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </p>
                        <hr />
                        <h3>Project Description</h3>
                        <div class="sf_project_desc">
                            <%--<input type="text" runat="server" required="required" placeholder="Project Name" id="project_name">--%>
                            <asp:TextBox ID="project_name" runat="server" CssClass="required" placeholder="Project Name*"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="project_name"
                                CssClass="required" ErrorMessage="Project Name" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                            <div class="form_input_two_col project_desc_two_col">
                                <div class="input_col input_col_one">
                                    <%--<input type="text" runat="server" required="required" placeholder="Quantity" id="project_quantity">--%>
                                    <asp:TextBox ID="project_quantity" runat="server" CssClass="required" placeholder="Quantity*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="project_name"
                                        CssClass="required" ErrorMessage="Quantity is required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </div>
                                <div class="input_col input_col_two">
                                     <CMS:FormBlock ID="DateFormField1" runat="server" IncludeTags="False" SuppressWrapperTags="True" />
                                  <%--  <asp:TextBox ID="project_due_date" runat="server" CssClass="required" placeholder="Due Date*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="project_due_date"
                                        CssClass="required" ErrorMessage="Project due date is required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <textarea id="project_desc" placeholder="Project Description" runat="server"></textarea>
                        </div>
                        <!-- end sf_project_desc -->
                        <hr />
                        <h3>Upload Files</h3>
                        <div style="display: none;">
                            <asp:TextBox ID="txtUploadFiles" CssClass="hddn_upload_files" runat="server"></asp:TextBox>
                        </div>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="margin: 0px;" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtUploadFiles"
                             ValidationGroup="GuestRegister">*</asp:RequiredFieldValidator>--%>
                        <p>Upload files from:</p>
                        <div class="upload_files clearfix">
                            <div class="grid_9 alpha logo" id="container">
                                <a id="link" href="javascript:void('0')">
                                    <img src="/images/upload-logo-dropbox.png" id="chooser-image" alt="Dropbox" /></a>
                            </div>
                            <script type="text/javascript">
                                document.getElementById("chooser-image").onclick = function () {
                                    var linkText = document.getElementById('file-hidden-text');
                                    var objHdnFileLinks = document.getElementById('<%=hdnFileLinks.ClientID%>');
                                    var button = Dropbox.choose({
                                        linkType: "direct",
                                        success: function (files) {
                                            for (var i = 0; i < files.length; i++) {
                                                linkText.removeAttribute('style');
                                                linkText.setAttribute('class', 'small_text');
                                                linkText.setAttribute('style', 'color:green;');
                                                objHdnFileLinks.value = objHdnFileLinks.value + files[i].link + ',';
                                                $("#file-hidden-text").append('- ', files[i].name, '<br />');
                                                $('.hddn_upload_files').val('done');
                                                $('.upload_files').removeAttr('required');
                                            }
                                        },
                                        multiselect: true,
                                        extensions: ['.pdf', '.doc', '.docx', '.rtf', '.txt', '.jpg', '.gif', '.png', '.bmp', '.ai', '.ps', '.xls', '.xlsx', '.csv', '.tab', '.zip'],
                                    });
                                    //document.getElementById('container').appendChild(button);
                                };
                            </script>
                            <!-- end grid -->
                            <div class="grid_6 logo" id="box-container">
                                <a id="box-select" href="javascript:void('0')">
                                    <img src="/images/upload-logo-box.png" alt="Box" id="box" /></a>
                            </div>
                            <script type="text/javascript">
                                $(document).ready(function () {
                                    var options = {
                                        clientId: <%=this.BoxClientId%>,
                                        linkType: 'shared',
                                        multiselect: true
                                    };
                                    var boxSelect = new BoxSelect(options);
                                    // Register a success callback handler
                                    boxSelect.success(function (response) {
                                        var linkText = document.getElementById('file-hidden-text');
                                        var objHdnFileLinks = document.getElementById('<%=hdnFileLinks.ClientID%>');
                                        for (var i = 0; i < response.length; i++) {
                                            var contains = (response[i].name.indexOf('.doc') > -1 || response[i].name.indexOf('.docx') > -1 || response[i].name.indexOf('.txt') > -1 || response[i].name.indexOf('.rtf') > -1 || response[i].name.indexOf('.pdf') > -1 || response[i].name.indexOf('.jpg') > -1 || response[i].name.indexOf('.gif') > -1 || response[i].name.indexOf('.png') > -1 || response[i].name.indexOf('.bmp') > -1 || response[i].name.indexOf('.ai') > -1 || response[i].name.indexOf('.ps') > -1 || response[i].name.indexOf('.xls') > -1 || response[i].name.indexOf('.xlsx') > -1 || response[i].name.indexOf('.csv') > -1 || response[i].name.indexOf('.tab') > -1 || response[i].name.indexOf('.zip') > -1); //true
                                            if (contains) {
                                                linkText.removeAttribute('style');
                                                linkText.setAttribute('class', 'small_text');
                                                linkText.setAttribute('style', 'color:green;');
                                                $("#file-hidden-text").append('- ', response[i].name, '<br />');
                                                objHdnFileLinks.value = objHdnFileLinks.value + response[i].url + ',';
                                                $('.hddn_upload_files').val('done');
                                                $('.upload_files').removeAttr('required');
                                            }
                                        }
                                        // Closes the file picker window if its open
                                        boxSelect.closePopup();
                                    });
                                    // Register a cancel callback handler
                                    boxSelect.cancel(function () {
                                        console.log("The user clicked cancel or closed the popup");
                                    });
                                });
                            </script>
                            <!-- end grid -->
                            <div class="grid_9 omega logo" id="result">
                                <a href="javascript:void('0')" onclick="loadPicker();">
                                    <img src="/images/upload-logo-google-drive.png" alt="Google Drive" /><script
                                        type="text/javascript" src="https://apis.google.com/js/api.js?onclick=loadPicker"></script></a>
                            </div>
                            <script type="text/javascript">
                                // The API developer key obtained from the Google Developers Console.
                                var developerKey = <%=this.GoogleDriveAPIDeveloperKey%>; //'AIzaSyB3rZ3uaWsxzbQi2sCURVSmgVmnZuJM_l0'; //TO DO change developer key from the client

                                // The Client ID obtained from the Google Developers Console.
                                var clientId = <%=this.GoogleAPIClientId%>; //'574155198618-himns0ci3t54lob800llsrilgd2nlb02.apps.googleusercontent.com';

                                // Scope to use to access user's photos.
                                var scope = ['https://www.googleapis.com/auth/drive.readonly'];

                                var pickerApiLoaded = false;
                                var oauthToken;

                                // Use the API Loader script to load google.picker and gapi.auth.
                                function loadPicker() {
                                    gapi.load('auth', { 'callback': onAuthApiLoad });
                                    gapi.load('picker', { 'callback': onPickerApiLoad });
                                }

                                function onAuthApiLoad() {
                                    window.gapi.auth.authorize(
                                                    {
                                                        'client_id': clientId,
                                                        'scope': scope,
                                                        'immediate': false
                                                    },
                                                    handleAuthResult);
                                }

                                function onPickerApiLoad() {
                                    pickerApiLoaded = true;
                                    createPicker();
                                }

                                function handleAuthResult(authResult) {
                                    if (authResult && !authResult.error) {
                                        oauthToken = authResult.access_token;
                                        createPicker();
                                    }
                                }

                                // Create and render a Picker object for picking user Photos.
                                function createPicker() {
                                    if (pickerApiLoaded && oauthToken) {
                                        var picker = new google.picker.PickerBuilder().
                                                                      addView(google.picker.ViewId.DOCS).
                                                                      enableFeature(google.picker.Feature.MULTISELECT_ENABLED).
                                                                      setOAuthToken(oauthToken).
                                                                      setDeveloperKey(developerKey).
                                                                      setCallback(pickerCallback).
                                                                      build();
                                        picker.setVisible(true);
                                    }
                                }

                                // A simple callback implementation. https://developers.google.com/picker/docs/reference#Response.Documents
                                function pickerCallback(data) {
                                    var url = '';
                                    var name = '';
                                    var linkText = document.getElementById('file-hidden-text');
                                    var objHdnFileLinks = document.getElementById('<%=hdnFileLinks.ClientID%>');
                                    if(data.docs){
                                        for (var i = 0; i < data.docs.length; i++) {
                                            if (data[google.picker.Response.ACTION] == google.picker.Action.PICKED) {
                                                var doc = data[google.picker.Response.DOCUMENTS][i];
                                                url = doc[google.picker.Document.URL];
                                                name = doc[google.picker.Document.NAME];
                                            }
                                            linkText.removeAttribute('style');
                                            linkText.setAttribute('class', 'small_text');
                                            linkText.setAttribute('style', 'color:green;');
                                            $("#file-hidden-text").append('- ', name, '<br />');
                                            objHdnFileLinks.value = objHdnFileLinks.value + url + ',';
                                            $('.hddn_upload_files').val('done');
                                            $('.upload_files').removeAttr('required');
                                        }
                                    }
                                }
                            </script>
                    <p class="small_text gray" style="text-align: center; color:grey">
                                    If you plan to use one of these services, please make sure your files<br />
                                    are saved in a public/shared folder before making your selection.
                                    </p>
                        </div>
                        <!-- end upload_files -->
                        <asp:HiddenField ID="hdnFileLinks" runat="server" Value="" />
                        <p id="file-hidden-text" style="display: none;">
                    File(s) to upload:<br />
                        </p>
                        <br />
                        <p>Upload files from your computer</p>
                        <div class="upload_files clearfix">
                            <div id="drag_drop_file">
                                <div class="drag_drop_placeholder">
                                    <ajaxToolkit:AjaxFileUpload ID="ajaxFileUpload" BorderStyle="None" runat="server"
                                        OnUploadComplete="Upload_LocalComplete" OnClientUploadComplete="ClientUploadComplete"
                                        OnClientUploadCompleteAll="ClientUploadCompleteAll"
                                        OnClientUploadError="ClientUploadError" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnFileIDs" runat="server" Value="" />
                                    <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                                        <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                                </div>
                                <!-- end drag_drop_placeholder -->
                            </div>
                            <!--- end drag_drop_file -->
                        </div>
                        <!-- end upload_files -->
                        <asp:Button ID="submit_file" CssClass="white_btn search btnSubmitSAF" runat="server" Text="Submit"
                            ValidationGroup="SocialRegister" />
                        <span class="safWaitImg" style="display: none;" id="safWaitImg">
                            <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
                        </span>
                    </div>
                </div>
                <!-- end single_col -->
            </div>
            <!-- social main content -->
            <div class="send_file_sidebar grid_6 col-height-equal">
                <div class="prefix_2 grid_20 suffix_2">
                    <span class="headline-block-icon-black"></span>
                    <h2 class="headline">
                        Your Location
                    </h2>
                    <div id="sf_location_map" class="static_map">
                        <img alt="map" id="googleMapImage" runat="server" src="#" />
                        <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server"
                            title="Your Location" href="#">VIEW MAP</a>
                        <!-- Mobile Link: go to google map page on devices smaller than 768px -->
                        <a class="red-text view-map-mobile" id="viewDirectionMobile" title="Your Location" runat="server" target="_blank"
                            href="#">VIEW MAP</a>
                    </div>
                    <asp:Literal runat="server" ID="litFranchiseContactInfo"></asp:Literal>
                    <hr />
                    <div class="sf_social_icons">
                        <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                    </div>
                    <!-- end social_icons -->
                </div>
            </div>
            <!-- sidebar-->
        </div>
        <!-- social_send_file content-->
    </div>
    <!--end send_file_content -->
</div>
<!-- end send_filecontent_wrapper -->
<div class="clear"></div>
