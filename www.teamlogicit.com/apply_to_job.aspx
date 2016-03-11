<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="apply_to_job.aspx.cs" Inherits="apply_to_job" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function ClientUploadComplete(sender, e) {
            FormatUploadControl_Jobs();
            $('.hddn_resume_files').val('done');
            $('.resume_files').removeAttr('required');
            var id = e.get_fileId();
            var objHdnFileIDs = document.getElementById('<%=hdnResumeIDs.ClientID%>');
            objHdnFileIDs.value = objHdnFileIDs.value + id + ',';
        }

        function ClientUploadError() {
            FormatUploadControl_Jobs();
            $('.hddn_resume_files').val('');
            $('.resume_files').attr('required', 'required');
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

        function OpenBrowseWindow() {
            $('#ajaxFileUploadResume_Html5InputFile').click();
        }

        $(document).ready(function () {

            $('ul.transformSelectDropdown li').live("click", function () {               
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
        });
    </script>
    <%--<AjaxToolkit:ToolkitScriptManager ID="scriptMngr" runat="server" />--%>
    <div class="clear"></div>
    <!-- mmm Job Application Form mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Application Form mmm -->
    <div class="job_app_wrapper clearfix">
        <div class="container_24">
            <div class="grid_20 suffix_4">
                <h2 class="headline">apply to job</h2>
                <asp:Panel ID="plnJobDetails" runat="server">
                    <h3>
                        <asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal>
                        <span>
                            <asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></span>
                    </h3>
                    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
                    <div class="form">
                        <h4>Personal Information</h4>
                        <div class="grid_9 suffix_1 alpha">
                            <span>
                                <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1" placeholder="First Name*" required="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtFirstName"></asp:RequiredFieldValidator></span>
                            <span>
                                <asp:TextBox ID="txtEmail" TextMode="Email" TabIndex="3" runat="server" placeholder="Email Address*" required="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv5" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtEmail"></asp:RequiredFieldValidator></span>

                            <span class="application_state">
                                <asp:TextBox ID="txtStateName" CssClass="txtStateName" runat="server" Style="display: none;" required="required"></asp:TextBox>
                                <select class="custom-select" tabindex="5" id="application_state">
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
                                <span class="required"></span>
                                <asp:RequiredFieldValidator ID="rfv6" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtStateName"></asp:RequiredFieldValidator>
                            </span>

                        </div>
                        <!--grid-->
                        <div class="grid_10 omega">
                            <span>
                                <asp:TextBox ID="txtLastName" runat="server" TabIndex="2" placeholder="Last Name*" required="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv2" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtLastName"></asp:RequiredFieldValidator></span>
                            <span>
                                <asp:TextBox ID="txtCity" runat="server" TabIndex="4" placeholder="City*" required="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv3" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtCity"></asp:RequiredFieldValidator></span>
                            <span>
                                <asp:TextBox ID="txtZip" runat="server" TabIndex="6" placeholder="Zip*" required="required"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="applyJob" ForeColor="Red" CssClass="required" runat="server"
                                    ControlToValidate="txtZip"></asp:RequiredFieldValidator></span>
                        </div>
                        <!--grid-->
                        <div class="clear"></div>
                        <div class="grid_20">
                            <h4>Upload Resume</h4>
                            <div class="upload">
                                <div class="upload_overlay">
                                    <AjaxToolkit:AjaxFileUpload ID="ajaxFileUploadResume" TabIndex="7" BorderStyle="None" runat="server"
                                        OnUploadComplete="Upload_NationalComplete"
                                        OnClientUploadComplete="ClientUploadComplete"
                                        AllowedFileTypes="txt,doc,docx,pdf,rtf"
                                        MaximumNumberOfFiles="1"
                                        ChunkSize="10000000"
                                        OnClientUploadError="ClientUploadError"
                                        ThrobberID="loader" ClientIDMode="Static" />
                                    <asp:HiddenField ID="hdnResumeIDs" runat="server" Value="" />
                                    <!-- <h5>Drag &amp; Drop your files here</h5>
                                    <p>File size limit: 10MB / Valid Formats: .pdf, .doc, .docx, .rtf, .txt. You can also <a href="#">browse for a file</a></p>-->
                                </div>
                                <!--upload_overlay-->
                                <textarea name="upload"></textarea>
                                <asp:TextBox ID="txtResume" Style="display: none;" required="required" CssClass="hddn_resume_files" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv7" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtResume"
                                    ValidationGroup="applyJob"></asp:RequiredFieldValidator>
                            </div>
                            <!--upload-->
                            <div class="clear"></div>
                            <h4>Cover Note <span>(optional)</span></h4>
                            <textarea id="coverNotes" TabIndex="8" class="coverNotes" runat="server"></textarea>
                            <p class="small_text"><span class="charLimitCount">350</span> character max</p>
                            <div class="square_button submit_application">
                                <asp:LinkButton ID="btnSubmit" TabIndex="9" CssClass="btnSubmitApplication" runat="server" Text="Send Application" ValidationGroup="applyJob" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlNoResult" runat="server" Visible="false">
                    <p>Sorry!!! No job found matching your Search criteria.</p>
                </asp:Panel>
            </div>
            <!-- grid_24 -->
        </div>
        <!--container_24-->
    </div>
    <!--job_app_wrapper-->
    <div class="clear"></div>
</asp:Content>
