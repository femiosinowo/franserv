<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GuestRegisterLocal.ascx.cs"
    Inherits="UserControls_GuestRegisterLocal" %>
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
            FileTypeUploadError();           
        }
        else
        {
            //FormatUploadControl_Jobs();
            MoveStatusBar();
            $('.hddn_upload_files').val('done');
            $('.upload_files').removeAttr('required');
            $('.hddn_upload_files').val('done');
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
        alert('Sorry, There was an error uploading your file.  Please try again.');
        $('.safWaitImg').hide();
        return false;
    }

    function FileTypeUploadError() {
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
        var emailAddress  = document.getElementById('<%=email.ClientID%>').value;
        var selectedCenter  = document.getElementById('<%=selectedCenter.ClientID%>').value;
        var phoneNumber  = JSON.stringify(document.getElementById('<%=phone.ClientID%>').value);
        var jobTitle  = JSON.stringify(document.getElementById('<%=job_title.ClientID%>').value);
        var companyName  = JSON.stringify(document.getElementById('<%=company.ClientID%>').value);
        var projectName  = JSON.stringify(document.getElementById('<%=project_name.ClientID%>').value);
        var projectQuantity  = JSON.stringify(document.getElementById('<%=project_quantity.ClientID%>').value);
        var projectDueDate  = JSON.stringify(document.getElementById('DueDate_iso').value);//JSON.stringify(document.getElementById('<%--<%=project_due_date.ClientID%>--%>').value);
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

        $('.btnSubmitSAF').click(function (e) {      
            
            var fieldsStatus = false;
            $('.guest_register_saf input.required').each(function(){
                var fieldId = $(this).attr('id')
                var fieldVal = document.getElementById(fieldId).value;
                if(fieldVal != ''){
                    fieldsStatus = true;
                    $(this).removeClass('requiredField');
                }
                else{
                    fieldsStatus = false; //alert($(this).attr('id')); 
                    $(this).addClass('requiredField');
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
<script type="text/javascript">    
    function doUserLoginValidation() {
        var matched;
        var password = document.getElementById('<%=password_user_login.ClientID%>');
        var confirm_password = document.getElementById('<%=confirm_password_user_login.ClientID%>');
        match = (password == confirm_password) ? true : false;
        if (matched) {
            console.log(matched);
        } else {
            console.log(matched);
        }
    }
    function DoPostForAddUserLogin() {
        doUserLoginValidation();
        //__doPostBack('<%=btnUserLogin.ClientID %>', 'onAddUserLoginSubmit');
    }  
</script>
<div class="grid_24 clearfix">
    <div class="grid_16 sf_col two_col col1">
        <h2 class="header">
            SEND A FILE</h2>
        <div class="prefix_1 grid_22 suffix_1 alpha guest_register_saf">
            <div id="user_login_info">
                <h3>
                    User Login Information</h3>
                <p>
                    <strong>USERNAME:</strong> username<br />
                    <strong>PASSWORD:</strong> password12345</p>
                <hr />
            </div>
            <!-- user_login_info -->
            <h3>
                Your Location</h3>
            <div class="grid_12" id="sf_location_info">
                <asp:Literal runat="server" ID="litFranchiseContactInfo"></asp:Literal>
            </div>
            <!-- end your_location_info -->
            <div class="grid_12 static_map" id="sf_location_map">
                <img id="googleMapImage" runat="server" src="#" alt="map" />
                <!-- DESKTOP/TABLET BUTTON -->
                <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server"
                    title="Your Location" href="#">VIEW MAP</a>
                <!-- Mobile Link: go to google map page on devices smaller than 768px -->
                <a class="red-text view-map-mobile" id="viewDirectionMobile" runat="server" target="_blank"
                    href="#" title="Your Location">VIEW MAP</a>
            </div>
            <!-- end sf_location_map -->
            <hr />
            <h3>
                Contact Information</h3>
            <%--<form id="contact_project_info">--%>
            <div class="form" id="contact_project_info">
                <div id="fileAlreadyExist" Class="required"></div>
                <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
                <asp:ValidationSummary ID="validationSummary" CssClass="required" HeaderText="Please fill out all the required fields(*). " ShowSummary="true" runat="server" ValidationGroup="GuestRegister" />
                <br />                
                <asp:HiddenField ID="selectedCenter" runat="server" Value=""></asp:HiddenField>
                <div class="grid_12 alpha form_two_col">
                    <%--<input type="text" placeholder="First Name" id="fname" /><span class="required">*</span>--%>
                    <asp:TextBox ID="fname" runat="server" TabIndex="100" CssClass="required" placeholder="First Name*"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="fname"
                        ErrorMessage="First Name is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <%--<input type="text" placeholder="Job Title" id="job_title" />--%>
                    <asp:TextBox ID="job_title" runat="server" TabIndex="102" placeholder="Job Title"></asp:TextBox>
                    <%--<input type="text" placeholder="Email Address" id="email" />
                    <span class="required">*</span>--%>
                    <asp:TextBox ID="email" runat="server" CssClass="required" TabIndex="104" placeholder="Email Address*"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="email"
                        ErrorMessage="Email Address is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </div>
                <!-- end grid -->
                <div class="grid_12 omega form_two_col">
                    <%--<input type="text" placeholder="Last Name" id="lname" />
                    <span class="required">*</span>--%>
                    <asp:TextBox ID="lname" runat="server" CssClass="required" TabIndex="101" placeholder="Last Name*"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="lname"
                        ErrorMessage="Last Name is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <%--<input type="text" placeholder="Company" id="company" />--%>
                    <asp:TextBox ID="company" runat="server" TabIndex="103" placeholder="Company"></asp:TextBox>
                    <%--<input class="no-margin-bottom" type="text" placeholder="Phone Number" id="phone" />
                    <span class="required">*</span>--%>
                    <asp:TextBox ID="phone" runat="server" TabIndex="105" placeholder="Phone Number*" CssClass="no-margin-bottom required"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="phone"
                        ErrorMessage="Phone Number is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <p class="sample-entry">
                        <span>Example 845-222-6666</span>
                    </p>
                </div>
                <!-- end grid -->
                <div class="clear">
                </div>
                <hr />
                <h3>
                    Project Description</h3>
                <div class="sf_project_desc">
                    <%--<input type="text" runat="server" required="required" placeholder="Project Name" id="project_name">--%>
                    <asp:TextBox ID="project_name" runat="server" CssClass="required" placeholder="Project Name*"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="project_name"
                        ErrorMessage="Project Name is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <div class="grid_11 suffix_1 form_two_col">
                        <%--<input type="text" runat="server" required="required" placeholder="Quantity" id="project_quantity">--%>
                        <asp:TextBox ID="project_quantity" runat="server" CssClass="required" placeholder="Quantity*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="project_quantity"
                            ErrorMessage="Quantity is required." CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="grid_12 form_two_col">
                        <%--<input type="text" runat="server" required="required" placeholder="Due Date" id="project_due_date">
                        <asp:TextBox ID="project_due_date" runat="server" CssClass="required" placeholder="Due Date*"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv9" runat="server" ControlToValidate="project_due_date"
                            CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                        
                         <CMS:FormBlock ID="DateFormField1" runat="server" IncludeTags="False" SuppressWrapperTags="True" />
                    </div>
                    <textarea id="project_desc" placeholder="Project Description" runat="server"></textarea>
                </div>
                <!-- end sf_project_desc -->
                <hr />
                <h3>
                    Upload Files*</h3>
                <div style="display:none;">
                    <asp:TextBox ID="txtUploadFiles" CssClass="hddn_upload_files" runat="server"></asp:TextBox>
                </div>                
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="margin: 0px;" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtUploadFiles"
                     ErrorMessage="Select a file to upload." ValidationGroup="GuestRegister">*</asp:RequiredFieldValidator>--%>
                <p class="small_text gray">
                    Upload files from:</p>
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
                                    for(var i = 0; i < files.length; i++){
                                        linkText.removeAttribute('style');
                                        linkText.setAttribute('class','small_text');
                                        linkText.setAttribute('style', 'color:green;');
                                        objHdnFileLinks.value = objHdnFileLinks.value + files[i].link + ',';
                                        $("#file-hidden-text").append('- ', files[i].name, '<br />');
                                        $('.hddn_upload_files').val('done');
                                    }
                                },
                                multiselect: true,
                                extensions: ['.pdf', '.doc', '.docx', '.rtf', '.txt', '.jpg', '.gif', '.png', '.bmp', '.ai', '.ps', '.xls', '.xlsx', '.csv', '.tab', '.zip'],
                            });
                            document.getElementById('container').appendChild(button);
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
                            debugger;
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
                                }
                            }
                        }
                    </script>
                    <p class="small_text gray" style="text-align: center">
                                    If you plan to use one of these services, please make sure your files<br />
                                    are saved in a public/shared folder before making your selection.
                                    </p>
                </div>
                <asp:HiddenField ID="hdnFileLinks" runat="server" Value="" />
                <p id="file-hidden-text" style="display: none;">
                    File(s) to upload:<br />
                </p>
                <!-- end upload_files -->
                <p class="small_text gray">
                    Upload files from your computer</p>
                <div class="upload_files ajaxFileUpload_section clearfix">
                    <div id="drag_drop_file">
                        <div class="drag_drop_placeholder">
                            <AjaxToolkit:AjaxFileUpload ID="ajaxFileUpload" BorderStyle="None" runat="server"
                                OnUploadComplete="Upload_LocalComplete" OnClientUploadComplete="ClientUploadComplete" 
                                OnClientUploadCompleteAll="ClientUploadCompleteAll"                               
                                OnClientUploadError="ClientUploadError" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnFileIDs" runat="server" Value="" />                            
                            <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                                <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                        </div>
                        <!-- end drag_drop_placeholder -->
                    </div>
                    <!-- end drag_drop_file -->
                </div>
                <!-- end upload_files -->             
                
                <asp:Button ID="submit_file" CssClass="purple_btn search btnSubmitSAF" runat="server" Text="Submit" 
                     ValidationGroup="GuestRegister" />
                 <span class="safWaitImg" style="display: none;" id="safWaitImg">
                    <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
                </span>              
                <%--<input class="purple_btn search" type="submit" id="submit_file" value="Submit" />--%> 
            </div>
        </div>
        
        <!-- end single_col -->
    </div>
    <!-- end sf_col 1 -->
    <div class="grid_8 sf_col two_col col2">
        <h2 class="header">
            Save Time</h2>
        <div class="prefix_2 grid_20 suffix_2">
            <h3 class="special">
                Register Today.</h3>
            <p>
                Enjoy the benefit of having your contact information already entered. It's simple.
                <strong>Just add a username and password.</strong></p>
            <div id="user_login_confirmation">
                <h4>
                    <span class="red-text">Thank you!</span> Your user login has been added.</h4>
                <p class="small_text">
                    <span class="red-text"><strong>&lt;</strong></span> Please complete the form to
                    upload your files.</p>
            </div>
            <!-- end user_login_confirmation -->
             <div class="form" id="user_login_form">
                <p>
                    <%--<input type="text" placeholder="Email" id="email_user_login" />
                    <span class="required">*</span></p>--%>
                    <asp:TextBox ID="email_user_login" runat="server" placeholder="Email Address" TextMode="Email" CssClass="emailUserLogin"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_email_user_login" runat="server" ControlToValidate="email_user_login"
                        CssClass="required" ValidationGroup="AddUserLogin" ForeColor="Red">*</asp:RequiredFieldValidator></p>
                <p>
                    <%--<input type="password" placeholder="Password" id="password_user_login" />
                    <span class="required">*</span></p>--%>
                    <asp:TextBox ID="password_user_login" runat="server" placeholder="Password" TextMode="Password" CssClass="passwordUserLogin"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_password_user_login" runat="server" ControlToValidate="password_user_login"
                        CssClass="required" ValidationGroup="AddUserLogin" ForeColor="Red">*</asp:RequiredFieldValidator></p>
                <p>
                    <%--<input type="password" placeholder="Confirm Password" id="confirm_password_user_login" />
                    <span class="required">*</span></p>--%>
                    <asp:TextBox ID="confirm_password_user_login" runat="server" placeholder="Confirm Password" TextMode="Password" CssClass="confirmedPasswordUserLogin"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv_confirm_password_user_login" runat="server" ControlToValidate="confirm_password_user_login"
                        CssClass="required" ValidationGroup="AddUserLogin" ForeColor="Red">*</asp:RequiredFieldValidator></p>
               <!-- <p class="terms_conditions">
                    <input type="checkbox" id="opt_in" value="1" />
                    <label for="opt_in" class="small-text">
                        <strong>Yes!</strong> Send me e-mail updates about the latest products and promotions.</label></p> -->
                <%--<input class="purple_btn" type="submit" id="user_login" value="Add User Login" />--%>
                <asp:Button ID="btnUserLogin" CssClass="purple_btn userLoginBtn" runat="server" Text="Add User Login"
                    OnClientClick="javascript:DoPostForAddUserLogin()" ValidationGroup="AddUserLogin"
                    UseSubmitBehavior="true" />
            </div>
            <div id="responseDiv">
            </div>
            <hr />
            <div class="sf_social_icons">
                <ux:SocialIcons ID="uxSocialIcons" runat="server" />
            </div>
            <!-- end social_icons -->
        </div>
        <!-- end grid -->
    </div>
    <!-- sf_col 2 -->
</div>
<!--end grid 24-->
