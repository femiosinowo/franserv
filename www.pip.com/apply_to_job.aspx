<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="apply_to_job.aspx.cs" Inherits="apply_to_job" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        function ClientUploadComplete(sender, e) {
            //FormatUploadControl_Jobs();
            $('.hddn_resume_files').val('done');
            $('.resume_files').removeAttr('required');
            var id = e.get_fileId();
            var objHdnFileIDs = document.getElementById('<%=hdnResumeIDs.ClientID%>');
            objHdnFileIDs.value = objHdnFileIDs.value + id + ',';

            //check if the file already exist on the server
            var hdnUploadedFiles = document.getElementById('<%=hdnResumeIDs.ClientID%>').value;
            var pageType = 'resumes';
            var data = '';
            $.ajax({
                type: "POST",
                url: "/Handlers/CheckFileExistOnSharedDrive.ashx",
                data: '{fileType: "' + pageType + '",uploadedFileIds:"' + hdnUploadedFiles + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "text",
                async: false,
                cache: false,
                success: function (response) {
                    //console.log(response);
                    data = response;
                },
                error: function (response, status, error) {
                    //console.log(error);
                }
            });

            if (data != '') {
                alert(responseData);
                $('.safWaitImg').hide();
                $('#fileAlreadyExist').html(data);
                $('.hddn_resume_files').val('');
                document.getElementById('<%=hdnResumeIDs.ClientID%>').value = '';
                $('.uploadedState').html('');
                FormatUploadControl_Jobs();
                if (window.location.href.indexOf('fileAlreadyExist') < 0)
                    window.location.href = window.location.href + '#fileAlreadyExist';
                else
                    window.location.href = window.location.href;
                status = false;
            }
            else {
                $('#fileAlreadyExist').html('');
                SaveData();
            }
        }

        function SaveData() {
            var firstName = document.getElementById('<%=txtFirstName.ClientID%>').value;
            var lastName = document.getElementById('<%=txtLastName.ClientID%>').value;
            var emailAddress = document.getElementById('<%=txtEmail.ClientID%>').value;
            var selectedCenter = document.getElementById('<%=hddnCenterId.ClientID%>').value;
            var city = document.getElementById('<%=txtCity.ClientID%>').value;
            var state = document.getElementById('<%=txtStateName.ClientID%>').value;
            var zipCode = document.getElementById('<%=txtZip.ClientID%>').value;
            var coverNotes = document.getElementById('<%=coverNotes.ClientID%>').value;
            var jobUrl = window.location.href;
            var jobId = document.getElementById('<%=hddnJobId.ClientID%>').value;
            var uploadedFilesIds = document.getElementById('<%=hdnResumeIDs.ClientID%>').value;

            var responseData = '';
            $.ajax({
                type: "POST",
                url: "/Handlers/SaveJobApplicationData.ashx",
                data: '{firstName: "' + firstName + '",lastName:"' + lastName + '",emailAddress:"' + emailAddress + '",selectedCenter:"' + selectedCenter + '",city:"' + city + '",state: "' + state + '",zipCode:"' + zipCode + '",coverNotes:"' + coverNotes + '",jobUrl:"' + jobUrl + '",jobId:"' + jobId + '",uploadedFilesIds:"' + uploadedFilesIds + '" }',
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
                    $('.safWaitImg').hide();
                    $('.hddn_resume_files').val('');
                    document.getElementById('<%=hdnResumeIDs.ClientID%>').value = '';
                    $('.uploadedState').html('');
                }
            });

            if (responseData != '') {
                alert(responseData);
                $('.safWaitImg').hide();
                $('.hddn_resume_files').val('');
                document.getElementById('<%=hdnResumeIDs.ClientID%>').value = '';
                $('.uploadedState').html('');
            }
            else {
                var thankYouPage = '/thank-you/?type=jobapp&centerId=';
                var localSiteCenterId = $('.hddnCenterId').val();
                if (localSiteCenterId != '')
                    thankYouPage = '/' + localSiteCenterId + thankYouPage + localSiteCenterId;
                else
                    thankYouPage = thankYouPage + selectedCenter;
                window.location.href = thankYouPage;
            }
        }

        function ClientUploadError() {
            FormatUploadControl_Jobs();
            $('.hddn_resume_files').val('');
            $('.resume_files').attr('required', 'required');
            var objHdnFileIDs = document.getElementById('<%=hdnResumeIDs.ClientID%>');
            objHdnFileIDs.value = '';
        }

        function FormatUploadControl_Jobs() {
            var userAgent = window.navigator.userAgent;
            var msie = userAgent.indexOf("MSIE ");
            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) // If IE
            {
                var ieVersion = parseInt(userAgent.substring(msie + 5, userAgent.indexOf(".", msie)));
                if (ieVersion > 9) {
                    setTimeout(function () {
                        $('#ajaxFileUploadResume_Html5InputFile').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                        $('#ajaxFileUploadResume_SelectFileContainer').hide();
                    }, 200);
                }
            }
            else {
                setTimeout(function () {
                    $('#ajaxFileUploadResume_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                    $('#ajaxFileUploadResume_SelectFileContainer').hide();
                }, 200);
            }
        }
        Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "X";

        function OpenBrowseWindow() {
            $('#ajaxFileUploadResume_Html5InputFile').click();
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

            //char limit
            $('.coverNotes').keypress(function (e) {
                var tval = $(this).val(),
                    tlength = tval.length,
                    set = 350,
                    remain = parseInt(set - tlength);
                $('.charLimitCount').text(remain);
                if (remain <= 0 && e.which !== 0 && e.charCode !== 0) {
                    $(this).val((tval).substring(0, tlength - 1))
                }
            });

            $('.btnSubmitApplication').click(function () {
                var status = false;
                $('.safWaitImg').show();
                if (($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo')) &&
                 ($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo').length > 1)) {                   
                    //trigger fileupload control to save files
                    $('#ajaxFileUploadResume_UploadOrCancelButton').click();
                    status = false;
                }
                else
                {
                    alert('Please Upload a File and all the required fields.');
                    $('.safWaitImg').hide();
                    status = false;
                }
                return status;
            });
        });
    </script>

    <div class="clear"></div>
    <!-- mmm join_team Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm join_team Content mmm -->
    <div class="job_application_content_wrapper main_about_us clearfix">
        <div class="job_application_content clearfix">
            <asp:Panel ID="plnJobDetails" runat="server">
                <div id="job_application_content" class="container_24">
                    <div class="grid_24 job_header">
                        <div class="job_back_btn">
                            <a href="#" id="jobDescription" runat="server" class="black-btn">Back to Job Post</a>
                        </div>
                        <span class="headline-block-icon-black"></span>
                        <h2 class="headline">
                            <asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal></h2>
                        <h3>
                            <asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></h3>
                        <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
                        <asp:HiddenField ID="hddnJobId" runat="server" Value="" />
                        <div class="job_description_links">
                            <script type="text/javascript">
                                function myPrintFunction() {
                                    window.print();
                                }
                            </script>
                            <ul class="ps-btns">
                                <li class="print-btn"><a onclick="myPrintFunction();"><span>Print</span></a></li>
                                <li class="email-btn"><a><span class="st_email"></span></a></li>
                                <li class="share-btn"><a><span class="st_sharethis_custom">Share</span></a></li>
                            </ul>
                        </div>
                        <!-- end briefs_wps_links -->
                    </div>
                    <div class="grid_24 job_details">
                        <div class="grid_18">
                            <div id="job_application_form" class="form">
                                <div id="fileAlreadyExist" Class="required"></div>
                                <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
                                <asp:ValidationSummary ID="validationSummary" CssClass="required" HeaderText="Please fill out all the required fields(*). " 
                                      ShowSummary="true" runat="server" ValidationGroup="applyJob" />
                                <br />
                                <div id="personal_info_pageform">
                                    <h3>Personal Information</h3>
                                    <ul class="personal_info_two_col">
                                        <li>
                                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name*" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                ControlToValidate="txtFirstName" ErrorMessage="First Name">*</asp:RequiredFieldValidator></li>
                                        <li>
                                            <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name*" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                ErrorMessage="Last Name" ControlToValidate="txtLastName">*</asp:RequiredFieldValidator></li>
                                    </ul>
                                    <ul class="personal_info_two_col">
                                        <li>
                                            <asp:TextBox ID="txtCity" runat="server" placeholder="City*" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                ErrorMessage="City" ControlToValidate="txtCity">*</asp:RequiredFieldValidator></li>
                                        <li>
                                            <asp:TextBox ID="txtStateName" CssClass="txtStateName" runat="server" Style="display: none;" required="required"></asp:TextBox>
                                            <select class="custom-select" id="apply_state">
                                                <option value="">- State* -</option>
                                                <option value="AL">Alabama</option>
                                                <option value="AK">Alaska</option>
                                                <option value="AZ">Arizona</option>
                                                <option value="AR">Arkansas</option>
                                                <option value="CA">California</option>
                                                <option value="CO">Colorado</option>
                                                <option value="CT">Connecticut</option>
                                                <option value="DE">Delaware</option>
                                                <option value="DC">District Of Columbia</option>
                                                <option value="FL">Florida</option>
                                                <option value="GA">Georgia</option>
                                                <option value="HI">Hawaii</option>
                                                <option value="ID">Idaho</option>
                                                <option value="IL">Illinois</option>
                                                <option value="IN">Indiana</option>
                                                <option value="IA">Iowa</option>
                                                <option value="KS">Kansas</option>
                                                <option value="KY">Kentucky</option>
                                                <option value="LA">Louisiana</option>
                                                <option value="ME">Maine</option>
                                                <option value="MD">Maryland</option>
                                                <option value="MA">Massachusetts</option>
                                                <option value="MI">Michigan</option>
                                                <option value="MN">Minnesota</option>
                                                <option value="MS">Mississippi</option>
                                                <option value="MO">Missouri</option>
                                                <option value="MT">Montana</option>
                                                <option value="NE">Nebraska</option>
                                                <option value="NV">Nevada</option>
                                                <option value="NH">New Hampshire</option>
                                                <option value="NJ">New Jersey</option>
                                                <option value="NM">New Mexico</option>
                                                <option value="NY">New York</option>
                                                <option value="NC">North Carolina</option>
                                                <option value="ND">North Dakota</option>
                                                <option value="OH">Ohio</option>
                                                <option value="OK">Oklahoma</option>
                                                <option value="OR">Oregon</option>
                                                <option value="PA">Pennsylvania</option>
                                                <option value="RI">Rhode Island</option>
                                                <option value="SC">South Carolina</option>
                                                <option value="SD">South Dakota</option>
                                                <option value="TN">Tennessee</option>
                                                <option value="TX">Texas</option>
                                                <option value="UT">Utah</option>
                                                <option value="VT">Vermont</option>
                                                <option value="VA">Virginia</option>
                                                <option value="WA">Washington</option>
                                                <option value="WV">West Virginia</option>
                                                <option value="WI">Wisconsin</option>
                                                <option value="WY">Wyoming</option>
                                            </select>
                                           <asp:RequiredFieldValidator ID="rfv6" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                 ErrorMessage="State Selection" ControlToValidate="txtStateName">*</asp:RequiredFieldValidator>
                                        </li>
                                    </ul>
                                    <ul class="personal_info_two_col">
                                        <li>
                                            <asp:TextBox ID="txtZip" runat="server" placeholder="Zip*" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                 ErrorMessage="Zip" ControlToValidate="txtZip">*</asp:RequiredFieldValidator></li>
                                        <li>
                                            <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" placeholder="Email Address*" required="required"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv5" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                                ErrorMessage="Email Address" ControlToValidate="txtEmail">*</asp:RequiredFieldValidator></li>
                                    </ul>
                                </div>
                                <!--personal info form-->
                                <div class="clear"></div>
                                <hr />
                                <div class="upload_resume clearfix">
                                    <h3>Upload Resume*</h3>
                                    <div id="drag_drop_file">
                                        <div class="drag_drop_placeholder">
                                            <ajaxToolkit:AjaxFileUpload ID="ajaxFileUploadResume" BorderStyle="None" runat="server"
                                                OnUploadComplete="Upload_NationalComplete"
                                                OnClientUploadComplete="ClientUploadComplete"
                                                AllowedFileTypes="txt,doc,docx,pdf,rtf"
                                                MaximumNumberOfFiles="1"
                                                ChunkSize="10000000"
                                                OnClientUploadError="ClientUploadError"
                                                ThrobberID="loader" ClientIDMode="Static" />
                                            <asp:HiddenField ID="hdnResumeIDs" runat="server" Value="" />
                                            <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                                             <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                                        </div>
                                        <!-- end drag_drop_placeholder -->    
                                        <div Style="display: none;">
                                            <asp:TextBox ID="txtResume" required="required" CssClass="hddn_resume_files" runat="server"></asp:TextBox>
                                        </div>                                         
                                        <!-- end drag_drop_placeholder -->
                                    </div>
                                    <!--- end drag_drop_file -->
                                </div>
                                <!-- end upload_files -->
                                <hr />
                                <div class="cover_note clearfix">
                                    <h3>Cover Note <span>(Optional)</span></h3>
                                    <textarea id="coverNotes" class="cover_note_content coverNotes" runat="server"></textarea>
                                    <div class="clear"></div>
                                    <p class="small_text gray"><span class="charLimitCount">350</span> Character Max</p>
                                </div>
                                <!-- end cover_note -->
                                <asp:Button ID="btnSubmit" CssClass="black-btn btnSubmitApplication" runat="server"
                                    Text="Send Application" ValidationGroup="applyJob" />
                                <br />
                                <span class="safWaitImg" style="display: none;" id="safWaitImg">
                                    <img alt="ajax-loader" src="/Workarea/images/application/ajax-loader_circle_lg.gif" />
                                </span>
                            </div>
                        </div>
                        <div class="grid_6">
                        </div>
                    </div>
                </div>
                <!-- company info content-->
            </asp:Panel>
            <asp:Panel ID="pnlNoResult" runat="server" Visible="false">
                <p>Sorry!!! No job found matching your Search criteria.</p>
            </asp:Panel>
        </div>
        <!--end join_team_content -->
    </div>
    <!-- end join_team_content_wrapper -->
    <div class="clear"></div>
</asp:Content>
