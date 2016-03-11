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

        function OpenBrowseWindow()
        {
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
    <div class="subpage_tagline_wrapper job_search clearfix">
        <div class="subpage_tagline">
        </div>
        <!-- end main_tagline -->
    </div>
    <div class="clear"></div>
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation join_our_team">
            <div class="container_24">
                <div class="grid_24">
                    <div id="mobile-nav-header" class="lvl-2-title-wrap">
                        <a id="page-title" class="lvl-2-title" href="#"></a><a class="arrow-plus-minus" href="#">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Join Our Team -->
                    <ul id="join-our-team-desktop-nav">
                        <li class="join-our-team-link"><a href="/join-our-team/">Join Our Team</a></li>
                        <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                        <li class="job-search-link"><a href="/job-search/">Job Search</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <div class="clear"></div>
    <div class="job_app_wrapper  clearfix">
        <div class="job_app clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24 clearfix">
                        <div class="grid_24">
                            <div class="cta-button-wrap white small back-button prev-page">                                
								<a href="#" id="jobDescription" runat="server" class="cta-button-text">Back</a>
                            </div>
                            <!--  cta-button-wrap -->
                        </div>
                        <!--end grid_24-->
                    </div>
                    <!--end container_24-->
                    <asp:Panel ID="plnJobDetails" runat="server">
                        <div class="container_24 clearfix main_content" id="job_app_content">
                        <div class="grid_24">
                            <h4>Apply to Job</h4>
                            <h2 class="subpage"><asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal>
                                <span><asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></span></h2>
                                <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
								<asp:HiddenField ID="hddnJobId" runat="server" Value="" />
                            <hr/>
                        </div>
                        <!--end grid_24-->
                        <div class="clear"></div>
                        <div class="form" id="job_app">
                            <div id="fileAlreadyExist" Class="required"></div>
                            <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
                            <asp:ValidationSummary ID="validationSummary" CssClass="required" HeaderText="Please fill out all the required fields(*). " 
                                  ShowSummary="true" runat="server" ValidationGroup="applyJob" />
                            <br />
                            <div class="app_section clearfix" id="section_1">
                                <div class="grid_2 app_section_num"><span></span></div><!-- end grid_2 -->
                                <div class="grid_20 suffix_2 app_section_form">                                    
                                    <h3>Personal Information</h3>
                                    <div id="personal_info">                                        
                                        <asp:TextBox ID="txtFirstName" runat="server" placeholder="First Name*" required="required" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server" 
                                            ControlToValidate="txtFirstName" ErrorMessage="First Name" >*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtLastName" runat="server" placeholder="Last Name*" required="required" ></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server" 
                                            ControlToValidate="txtLastName" ErrorMessage="Last Name" >*</asp:RequiredFieldValidator>
                                        <div class="clear"></div>
                                        <asp:TextBox ID="txtCity" runat="server" placeholder="City*" required="required" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server" 
                                            ControlToValidate="txtCity" ErrorMessage="City" >*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtStateName" CssClass="txtStateName" runat="server" style="display:none;" required="required" ></asp:TextBox>
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
                                            ControlToValidate="txtStateName" ErrorMessage="State" >*</asp:RequiredFieldValidator>
                                        <div class="clear"></div>
                                        <asp:TextBox ID="txtZip" runat="server" placeholder="Zip*" required="required" ></asp:TextBox>                                        
                                        <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server" 
                                            ControlToValidate="txtZip" ErrorMessage="Zip" >*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" placeholder="Email Address*" required="required" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv5" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server" 
                                            ControlToValidate="txtEmail" ErrorMessage="Email Address" >*</asp:RequiredFieldValidator>
                                    </div>
                                    <!-- end personal_info -->
                                </div>
                                <!-- end grid_18 -->
                            </div>
                            <!-- end section_1 -->
                            <div class="app_section clearfix" id="section_2">
                                <div class="grid_2 app_section_num"><span></span></div>
                                <!-- end grid_2 -->
                                <div class="grid_20 suffix_2 app_section_form">
                                    <h3>Upload Resume</h3>
                                    <div id="drag_drop_file">
                                        <div class="drag_drop_placeholder">
                                            <AjaxToolkit:AjaxFileUpload ID="ajaxFileUploadResume" BorderStyle="None" runat="server"
                                                OnUploadComplete="Upload_ajaxFileUploadResume"
                                                OnClientUploadComplete="ClientUploadComplete"
                                                AllowedFileTypes="txt,doc,docx,pdf,rtf"
                                                MaximumNumberOfFiles="1"
                                                ChunkSize="10000000"
                                                OnClientUploadError="ClientUploadError"
                                                ClientIDMode="Static"/>												
                                            <asp:HiddenField ID="hdnResumeIDs" runat="server" Value="" />
                                            <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                                             <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                                        </div>										
                                        <!-- end drag_drop_placeholder -->										
                                       <div Style="display: none;">
                                        <asp:TextBox ID="txtResume" Style="display: none;" required="required" CssClass="hddn_resume_files" runat="server"></asp:TextBox>
                                       </div>
                                    </div>
                                    <!-- end drag_drop_file -->
                                </div>
                                <!-- end grid_18 -->
                            </div>
                            <!-- end section_2 -->
                            <div class="app_section clearfix" id="section_3">
                                <div class="grid_2 app_section_num"><span></span></div>
                                <!-- end grid_2 -->
                                <div class="grid_20 suffix_2 app_section_form">
                                    <h3>Cover Note <span>(Optional)</span></h3>
                                    <textarea id="coverNotes" class="coverNotes" runat="server"></textarea>
                                    <div class="clear"></div>                                    
                                    <p class="small_text"><span class="charLimitCount">350</span> character max</p>
                                </div>
                                <!-- end grid_18 -->
                            </div>
                            <!-- end section_3 -->
                            <div class="app_section clearfix" id="section_4">
                                <div class="prefix_2 grid_20 suffix_2 app_section_form">
                                    <asp:Button ID="btnSubmit" CssClass="btnSubmitApplication" runat="server" Text="Send Application" ValidationGroup="applyJob" />
                                </div>
                                <!-- end grid_18 -->
                            </div>
                            <!-- end section_4 -->
                        </div>
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoResult" runat="server" Visible="false">
                        <p>Sorry!!! No job found matching your Search criteria.</p>
                    </asp:Panel>
                    <!--end job_app_content container_24 -->
                </div>
                <!--end grid_24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end job_app -->
    </div>
</asp:Content>
