﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SocialRegisterNational.ascx.cs"
    Inherits="UserControls_SocialRegisterNational" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<script type="text/javascript">
    //code for national google map
    $(document).ready(function () {
        $('.registerSearchLocation').click(function (e) {
            e.preventDefault();
            $('#searchLocationWaitImg_register').show();
            var validation = DoValidation_Register();
            if (validation)
                GetLocationsData();
            else
                $('#searchLocationWaitImg_register').hide();
        });

        function DoValidation_Register() {
            var status = false;
            var locationStatus = false;
            var distanceStatus = false;
            if ($('.txtRegisterLocation').val() != '') {
                locationStatus = true;
                $('.txtRegisterLocation').removeClass('requiredField');
            }
            else {
                $('.txtRegisterLocation').addClass('requiredField');
                locationStatus = false;
            }

            var distanceVal = $('#socialReg_choose_center .transformSelectDropdown .selected span').text();
            if (distanceVal != undefined && distanceVal != 'Distance') {
                distanceStatus = true; $('#socialReg_choose_center .transformSelect li:first').removeClass('requiredField');
            }
            else {
                $('#socialReg_choose_center .transformSelect li:first').addClass('requiredField');
                distanceStatus = false;
            }

            if (locationStatus == false || distanceStatus == false)
                status = false;
            else
                status = true;

            return status;
        }

        function GetLocationsData(address, distance) {
            var address = $('.txtRegisterLocation').val();
            var distance = $('#socialReg_choose_center .transformSelectDropdown .selected span').text();
            $.ajax({
                type: "POST",
                url: "/Handlers/GetLocationsByAddress.ashx",
                data: '{address: "' + address + '",distance:"' + distance + '",allUsLocations:"' + false + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: OnRegisterSuccess,
                failure: function (response) {
                    //alert(response.d);
                    $('#searchLocationWaitImg_register').hide();
                }
            });
        }

        $('#rq_pick_location_list_register').on("click", ".radioBtnCenterRegister", function () {
            $('.txtRegisterChooseLocation').val($(this).attr('value'));
            $('#viewCenterMapError').hide();
            var selectedAddress = $(this).attr('itemid');
            var viewMapLink;
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) || screen.width <= 480) {
                viewMapLink = $('.viewDirectionRegisterMobile').val();
            }
            else {
                viewMapLink = $('.viewDirectionRegisterDesktop').val();
            }
            var formattedLink = viewMapLink.replace('{0}', selectedAddress);
            $('a.register-view-map').attr('href', formattedLink);
        });

        function OnRegisterSuccess(response) {
            $('#searchLocationWaitImg_register').hide();
            if (response != "" && response != "[]") {
                $("#location_register_no_results").hide();
                var json = response;
                $("#rq_pick_location_register").show();
                $("#location_register_results_scroll").html("");
                for (i = 0; i < json.length; i++) {
                    $("#location_register_results_scroll").append("<p><input class='radioBtnCenterRegister' itemid='" + json[i].Address1 + "+" + json[i].City + "," + json[i].State + "+" + json[i].Zipcode + "' type='radio' name='rq_list' value='" + json[i].FransId + "'/><span><strong>" + json[i].Address1 + "</strong><br/>" + json[i].City + ", " + json[i].State + "  " + json[i].Zipcode + " </span></p>");
                }
            }
            else {
                $("#rq_pick_location_register").show();
                $("#location_register_no_results").show();
            }
        }

        $('.register-view-map').click(function () {
            var selectedCenter = $('.txtRegisterChooseLocation').val();
            if (selectedCenter != "") {
                $('#viewCenterMapError').hide();
                return true;
            }
            else {
                $('#viewCenterMapError').show();
                return false;
            }
        });
    });
</script>
<script type="text/javascript">
    function ClientUploadComplete(sender, e) {
        FormatUploadControl_Jobs();
        $('.hddn_upload_files').val('done');
        $('.upload_files').removeAttr('required');
        var id = e.get_fileId();
        var objHdnFileIDs = document.getElementById('<%=hdnFileIDs.ClientID%>');
        objHdnFileIDs.value = objHdnFileIDs.value + id + ',';
    }

    function ClientUploadError() {
        FormatUploadControl_Jobs();
        $('.hddn_upload_files').val('');
        $('.upload_files').attr('required', 'required');
    }

    function FormatUploadControl_Jobs() {
        var userAgent = window.navigator.userAgent;
        var msie = userAgent.indexOf("MSIE ");
        var trident = userAgent.indexOf('Trident/');
        //if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If IE
        if (msie > 0) // If IE
        {            
            var ieVersion = parseInt(userAgent.substring(msie + 5, userAgent.indexOf(".", msie)));
            if (ieVersion > 9) {
                setTimeout(function () {
                    $('#ajaxFileUpload_Html5InputFile').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT, JPG, GIF, PNG, BMP, AI, PS, XLS, XLSX, CSV, TAB, ZIP. You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                    $('#ajaxFileUpload_SelectFileContainer').hide();
                }, 200);
            }
        }
        else if(trident > 0)
        {
            setTimeout(function () {
                $('#ajaxFileUpload_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT, JPG, GIF, PNG, BMP, AI, PS, XLS, XLSX, CSV, TAB, ZIP. You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                $('#ajaxFileUpload_SelectFileContainer').hide();
            }, 200);
        }
        else {
            setTimeout(function () {
                $('#ajaxFileUpload_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT, JPG, GIF, PNG, BMP, AI, PS, XLS, XLSX, CSV, TAB, ZIP. You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                $('#ajaxFileUpload_SelectFileContainer').hide();
            }, 200);
        }
    }

    function OpenBrowseWindow() {
        $('#ajaxFileUpload_Html5InputFile').click();
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
    });
</script>
<script type="text/javascript">
    function DoPostForSubmission() {
        __doPostBack('<%= submit_file.ClientID %>', 'onClickSubmit');
    }
</script>
<div class="grid_24 clearfix">
    <div class="grid_16 sf_col two_col col1">
        <h2 class="header">
            Send a File</h2>
        <div class="prefix_1 grid_22 suffix_1 alpha">
            <h3>
                Choose a Center</h3>
            <div class="form" id="socialReg_choose_center">
                <asp:TextBox ID="txtRegisterLocation" CssClass="txtRegisterLocation" runat="server"
                    placeholder="Enter City, State, Zip" required="required"></asp:TextBox>
                <span class="required">*</span>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtRegisterLocation"
                    CssClass="required" ValidationGroup="SocialRegisterMap"></asp:RequiredFieldValidator>
                <select id="distance" class="custom-select">
                    <option value="">Distance</option>
                    <option value="5">5</option>
                    <option value="10">10</option>
                    <option value="25">25</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                </select>
                <span class="required">*</span>
                <asp:TextBox ID="txtRegisterChooseLocation" CssClass="txtRegisterChooseLocation"
                    runat="server" Style="display: none;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtRegisterChooseLocation"
                    CssClass="required" ValidationGroup="SocialRegisterMap" ForeColor="Red">*</asp:RequiredFieldValidator>
                <div class="clear">
                </div>
                <input class="gray_btn search registerSearchLocation" type="submit" id="registerSearchLocation"
                    value="Search" />
                <div id="searchLocationWaitImg_register" style="display: none;">
                    <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                </div>
            </div>
            <div class="clear">
            </div>
            <div id="rq_pick_location_register" style="display: none" class="clearfix">
                <input id="viewDirectionRegisterDesktop" type="hidden" class="viewDirectionRegisterDesktop"
                    runat="server" value="" />
                <input id="viewDirectionRegisterMobile" type="hidden" class="viewDirectionRegisterMobile"
                    runat="server" value="" />
                <h3>
                    Pick a Location
                    <div class="cta-button-wrap purple small cta-button-wrap-register view-map">
                        <a class="cta-button-text register-view-map view-map fancybox iframe" href="#"><span>
                            View a Map</span></a>
                    </div>
                    <!-- end cta-button-wrap -->
                    <!-- go to google map page on devices smaller than 768px -->
                    <div class="cta-button-wrap gold small view-map-mobile">
                        <a class="cta-button-text register-view-map" href="#" target="_blank"><span>View a Map</span></a>
                    </div>
                </h3>
                <p id="viewCenterMapError" style="display: none; color: red;">
                    Please select a center.</p>
                <div class="form" id="rq_pick_location_list_register">
                    <!--The html for the div will be binded using the javascript -->
                    <div id="location_register_results_scroll" class="custom_form_scroll">
                    </div>
                    <div id="location_register_no_results" style="display: none" class="custom_form_scroll">
                        <p>
                            No locations found.</p>
                    </div>
                </div>
            </div>
            <hr />
            <h3>
                Contact Information</h3>
            <p>
                <strong>
                    <asp:Label runat="server" ID="lblUserName" style="margin-left:10px"></asp:Label><br />
                    <asp:Label runat="server" ID="lblUserEmail" style="margin-left:10px"></asp:Label></strong>
            </p>
            <div class="form" id="contact_project_info">
                <p>
                    <asp:TextBox ID="email" runat="server" placeholder="Email" Visible="false"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="email"
                        CssClass="required" ValidationGroup="TwitterSocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </p>
                <%--<input class="no-margin-bottom" type="text" placeholder="Phone Number" id="phone" runat="server" required="required" />--%>
                <p>
                    <asp:TextBox ID="phone" CssClass="no-margin-bottom" runat="server" placeholder="Phone Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="phone" CssClass="required"
                        ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </p>
                <p class="sample-entry">
                    <span>Example 845-222-6666</span>
                </p>
                <div class="clear">
                </div>
                <hr />
                <h3>Let Us Know More About You</h3>
                <p>
                    <%--<input type="text" runat="server" required="required" placeholder="Job Title" id="job_title" />--%>
                    <asp:TextBox ID="job_title" runat="server" placeholder="Job Title"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="job_title"
                        CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <%--<input type="text" runat="server" required="required" placeholder="Company" id="company" />--%>
                    <asp:TextBox ID="company" runat="server" placeholder="Company"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="company"
                        CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </p>
                <p>
                    <%--<input type="text" runat="server" required="required" placeholder="Website" id="website" />--%>
                    <asp:TextBox ID="website" runat="server" placeholder="Website"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="website"
                        CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                </p>
                <hr />
                <h3>
                    Upload Files</h3>
                <p class="small_text gray">
                    Upload files from:
                </p>
                <div class="upload_files clearfix">
                    <div class="grid_9 alpha logo" id="container">
                        <a id="link">
                            <img src="/images/upload-logo-dropbox.png" id="chooser-image" alt="Dropbox" /></a>
                    </div>
                    <script type="text/javascript">
                    document.getElementById("chooser-image").onclick = function () {
                        var linkText = document.getElementById('file-hidden-text');
                        var objHdnFileLinks = document.getElementById('<%=hdnFileLinks.ClientID%>');
                        var button = Dropbox.choose({
                            linkType: "direct",
                            success: function (files) {
                                //console.log(files);
                                for(var i = 0; i < files.length; i++){
                                    linkText.removeAttribute('style');
                                    linkText.setAttribute('class','small_text');
                                    linkText.setAttribute('style', 'color:green;');
                                    objHdnFileLinks.value = objHdnFileLinks.value + files[i].link + ',';
                                    $("#file-hidden-text").append('- ', files[i].name, '<br />');
                                    //files[i].link;
                                    //files[i].bytes;
                                    //files[i].icon;
                                    //files[i].thumbnailLink;
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
                        <a id="box-select">
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
                                //console.log(response);
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
                                        //response[i].url;
                                        //response[i].id;
                                        //response[i].type; //file or folder
                                        //response[i].access; //collaborators, company or open
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
                        <a>
                            <img src="/images/upload-logo-google-drive.png" alt="Google Drive" onclick="loadPicker();" /><script
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
                            console.log(data);
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
                            }
                        }
                    </script>
                </div>
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
                                AllowedFileTypes="txt,doc,docx,pdf,rtf,jpg,gif,png,bmp,ai,ps,xls,xlsx,csv,tab,zip" MaximumNumberOfFiles="1" ChunkSize="10000000"
                                OnClientUploadError="ClientUploadError" ThrobberID="loader" ClientIDMode="Static" />
                            <asp:HiddenField ID="hdnFileIDs" runat="server" Value="" />
                            <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                                <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                        </div>
                        <!-- end drag_drop_placeholder -->
                    </div>
                    <!-- end drag_drop_file -->
                </div>
                <!-- end upload_files -->
                <hr />
                <h3>
                    Project Description</h3>
                <div class="sf_project_desc">
                    <%--<input type="text" runat="server" required="required" placeholder="Project Name" id="project_name">--%>
                    <asp:TextBox ID="project_name" runat="server" placeholder="Project Name"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="project_name"
                        CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <div class="grid_11 suffix_1 form_two_col">
                        <%--<input type="text" runat="server" required="required" placeholder="Quantity" id="project_quantity">--%>
                        <asp:TextBox ID="project_quantity" runat="server" placeholder="Quantity"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="project_name"
                            CssClass="required" ValidationGroup="SocialRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </div>
                    <div class="grid_12 form_two_col">
                        <input type="text" runat="server" required="required" placeholder="Due Date" id="project_due_date">
                    </div>
                    <textarea id="project_desc" placeholder="Project Description" runat="server"></textarea>
                </div>
                <!-- end sf_project_desc -->
                <asp:Button ID="submit_file" CssClass="purple_btn search" runat="server" Text="Submit"
                    OnClientClick="javascript:DoPostForSubmission()" ValidationGroup="SocialRegister"
                    UseSubmitBehavior="true" />
                <%--<input class="purple_btn search" runat="server" type="submit" id="submit_file" value="Submit" onclick="btnSocialRegisterSubmit_Click" />--%>
            </div>
        </div>
        <!-- end single_col -->
    </div>
    <!-- end sf_col 1 -->
    <div class="grid_8 sf_col two_col col2">
        <h2 class="header">
            <CMS:ContentBlock ID="cbSendAFileSideTitle" runat="server" DoInitFill="false" />
        </h2>
        <div class="prefix_2 grid_20 suffix_2">
            <h3 class="special gray">
                <CMS:ContentBlock ID="cbSendAFileSideSubTitle" runat="server" DoInitFill="false" />
            </h3>
            <p>
                <CMS:ContentBlock ID="cbSendAFileDescription" runat="server" DoInitFill="false" />
            </p>
            <div class="cta-button-wrap">
                <a class="cta-button-text" href="/why-we-are-different/"><span>WHY WE ARE DIFFERENT</span></a>
            </div>
        </div>
        <!-- end grid -->
    </div>
    <!-- sf_col 2 -->
</div>
