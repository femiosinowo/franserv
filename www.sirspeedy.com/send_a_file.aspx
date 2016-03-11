<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="send_a_file.aspx.cs" Inherits="login_national" %>

<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
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
            //save form data
            //call Handler  
            var firstName = JSON.stringify(document.getElementById('<%=fname.ClientID%>').value);
            var lastName  = JSON.stringify(document.getElementById('<%=lname.ClientID%>').value);
            var emailAddress  = document.getElementById('<%=email.ClientID%>').value;
            var selectedCenter  = document.getElementById('<%=selectedCenter.ClientID%>').value;
            var phoneNumber  = JSON.stringify(document.getElementById('<%=phone.ClientID%>').value);
            var jobTitle  = JSON.stringify(document.getElementById('<%=job_title.ClientID%>').value);
            var companyName  = JSON.stringify(document.getElementById('<%=company.ClientID%>').value);
            var webSite  = JSON.stringify(document.getElementById('<%=webSite.ClientID%>').value);
            var projectName  = JSON.stringify(document.getElementById('<%=project_name.ClientID%>').value);
            var projectQuantity  = JSON.stringify(document.getElementById('<%=project_quantity.ClientID%>').value);
            var projectDueDate  = JSON.stringify(document.getElementById('<%=project_due_date.ClientID%>').value);
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
    <!-- mmm Login Content (National) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Login Content (National) mmm -->
    <div class="login_content_wrapper clearfix">
        <div class="login_content clearfix send_file main_content">
            <div class="container_24">
                <div class="grid_24 clearfix">
                    <div class="grid_16 sf_col two_col col1">
                        <h2 class="header">
                            SEND A FILE</h2>
                        <div class="prefix_1 grid_22 suffix_1 alpha guest_register_saf">
                            <h3>
                                Contact Information</h3>
                            <p>
                                <strong>
                                    <asp:Label runat="server" ID="lblUserName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblJobTitle"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCompanyName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblUserEmail"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblPhoneNumber"></asp:Label>
                                </strong>
                            </p>
                            <hr />
                            <div class="form" id="contact_project_info">
                                <div id="fileAlreadyExist" Class="required"></div>
                                <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
                                <asp:ValidationSummary ID="validationSummary" CssClass="required" HeaderText="Please fill out all the required fields(*). " ShowSummary="true" runat="server" ValidationGroup="SendAFile" />
                                <br />
                                 <asp:HiddenField ID="fname" runat="server" Value="" />
                                 <asp:HiddenField ID="lname" runat="server" Value="" /> 
                                 <asp:HiddenField ID="email" runat="server" Value="" />
                                 <asp:HiddenField ID="phone" runat="server" Value="" />
                                 <asp:HiddenField ID="job_title" runat="server" Value="" />
                                 <asp:HiddenField ID="company" runat="server" Value="" />  
                                 <asp:HiddenField ID="webSite" runat="server" Value="" /> 
                                 <asp:HiddenField ID="selectedCenter" runat="server" Value="" />
                                <h3>
                                    Project Description</h3>
                                <div class="sf_project_desc">
                                    <%--<input type="text" runat="server" required="required" placeholder="Project Name" id="project_name">--%>
                                    <asp:TextBox ID="project_name" runat="server" CssClass="required" placeholder="Project Name*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="project_name"
                                         ErrorMessage="Project Name is required." CssClass="required" ValidationGroup="SendAFile" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="grid_11 suffix_1 form_two_col">
                                        <%--<input type="text" runat="server" required="required" placeholder="Quantity" id="project_quantity">--%>
                                        <asp:TextBox ID="project_quantity" runat="server" CssClass="required" placeholder="Quantity*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="project_quantity"
                                            ErrorMessage="Quantity is required." CssClass="required" ValidationGroup="SendAFile" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="grid_12 form_two_col">
                                        <%--<input type="text" runat="server" required="required" placeholder="Due Date" id="project_due_date">--%>
                                        <asp:TextBox ID="project_due_date" runat="server" CssClass="required" required="required" placeholder="Due Date*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv9" runat="server" ControlToValidate="project_due_date"
                                            CssClass="required" ValidationGroup="GuestRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </div>
                                    <textarea id="project_desc" placeholder="Project Description" runat="server"></textarea>
                                </div>
                                <!-- end sf_project_desc -->
                                <hr />
                                <h3>
                                    Upload Files*</h3>
                                <div Style="display: none;">
                                    <asp:TextBox ID="txtUploadFiles"  CssClass="hddn_upload_files" runat="server"></asp:TextBox>
                                </div>                                
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="margin: 0px;" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtUploadFiles"
                                     ErrorMessage="Select a file to upload." ValidationGroup="SendAFile">*</asp:RequiredFieldValidator>--%>
                                <p class="small_text gray">
                                    Upload files from:
                                </p>
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
                                        var developerKey = <%=this.GoogleDriveAPIDeveloperKey%>;  //TO DO change developer key from the client

                                        // The Client ID obtained from the Google Developers Console.
                                        var clientId = <%=this.GoogleAPIClientId%>;

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
                                    <p class="small_text gray" style="text-align: center">
                                    If you plan to use one of these services, please make sure your files<br />
                                    are saved in a public/shared folder before making your selection.
                                    </p></div>
                                <!-- end upload_files -->
                                <asp:HiddenField ID="hdnFileLinks" runat="server" Value="" />
                                <p id="file-hidden-text" style="display: none;">
                                    File(s) to upload:<br />
                                </p>
                                <p class="small_text gray">
                                    Upload files from your computer
                                </p>
                                <div class="upload_files clearfix">
                                    <div id="drag_drop_file">
                                        <div class="drag_drop_placeholder">
                                            <AjaxToolkit:AjaxFileUpload ID="ajaxFileUpload" BorderStyle="None" runat="server"
                                                OnUploadComplete="Upload_NationalComplete" OnClientUploadComplete="ClientUploadComplete"   
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
                                    ValidationGroup="SendAFile" />
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
                            Your Location</h2>
                        <div class="prefix_2 grid_20 suffix_2">
                            <div id="sf_location_map" class="static_map">
                                <img alt="map" id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=336x206&amp;zoom=15&amp;markers=icon:http://author.sirspeedy.com/sandbox/sirspeedy/images/location-map-marker.png%7C33.976463,-118.036812&amp;style=feature:landscape%7Ccolor:0xe9e9e9&amp;style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&amp;sensor=true" />
                                <div class="cta-button-wrap gold small view-map">
                                    <a class="cta-button-text view-map fancybox iframe" id="viewDirectionDesktop" runat="server"
                                        title="Your Location" href="https://www.google.com/maps/embed/v1/place?q=7240%20Greenleaf%20Ave%2C%20Whittier%2C%20CA%2090602%2C%20United%20States&key=AIzaSyDGF1KG6WSbJVdZ9TN66U3EMNA9wYIalFc&zoom=15">
                                        <span>VIEW MAP</span></a>
                                </div>
                                <!-- end -->
                                <!-- go to google map page on devices smaller than 768px -->
                                <div class="cta-button-wrap gold small view-map-mobile">
                                    <a class="cta-button-text" id="viewDirectionMobile" title="Your Location" runat="server" href="https://www.google.com/maps?daddr=q=7240+Greenleaf+Ave+Whittier+CA+90602"
                                        target="_blank"><span>VIEW MAP</span></a>
                                </div>
                            </div>
                            <ul class="contact_info">
                                <li class="contact-icon-location"><span>
                                    <asp:Literal ID="ltrCenterAddress" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <ul class="contact_info">
                                <li class="contact-icon-phone"><span>
                                    <asp:Literal ID="ltrPhone" runat="server"></asp:Literal></span></li>
                                <li class="contact-icon-fax"><span>
                                    <asp:Literal ID="ltrFax" runat="server"></asp:Literal></span></li>
                                <li class="contact-icon-email"><span>
                                    <asp:Literal ID="ltrEmail" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <ul class="contact_info">
                                <li class="contact-icon-hours"><span>Hours<br />
                                    <asp:Literal ID="ltrWorkingHours" runat="server"></asp:Literal>
                                </span></li>
                            </ul>
                            <hr />
                            <div class="sf_social_icons">
                                <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                            </div>
                            <!-- end social_icons -->
                        </div>
                        <!-- end grid -->
                    </div>
                    <!-- end sf_col 2 -->
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end login_content -->
    </div>
    <!-- end login_content_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
