<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequestAQuoteNational.ascx.cs"
    Inherits="UserControls_RequestAQuoteNational" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>
<script type="text/javascript">
    $(document).ready(function () {
        //request a quote code starts here
        $('.search_location_requestQuote').click(function (e) {
            e.preventDefault();
            $('#searchLocationWaitImg').show();
            var validation = DoValidation();
            if (validation)
                GetLocationsData();
            else
                $('#searchLocationWaitImg').hide();
        });

        function DoValidation() {
            var status = false;
            var locationStatus = false;
            var distanceStatus = false;
            if ($('#location').val() != '') {
                locationStatus = true; $('#location').removeClass('requiredField');
            }
            else {
                $('#location').addClass('requiredField');
                locationStatus = false;
            }
            var distanceVal = $('#choose_center ul.transformSelect li').eq('0').find('span:first').text();
            if (distanceVal != undefined && distanceVal != 'Distance') {
                distanceStatus = true; $('#choose_center .transformSelect li:first').removeClass('requiredField');
            }
            else {
                $('#choose_center .transformSelect li:first').addClass('requiredField');
                distanceStatus = false;
            }

            if (locationStatus == false || distanceStatus == false)
                status = false;
            else
                status = true;

            return status;
        }

        function GetLocationsData() {
            var address = $('#location').val();
            var distance = $('#choose_center ul.transformSelect li').eq('0').find('span:first').text();
            $.ajax({
                type: "POST",
                url: "/Handlers/GetLocationsByAddress.ashx",
                data: '{"address": "' + address + '","distance":"' + distance + '","allUsLocations":"' + false + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                cache: false,
                success: OnSuccess,
                failure: function (response) {
                    //alert(response.d);
                    $('#searchLocationWaitImg').hide();
                }
            });
        }

        $('#rq_pick_location_list').on("click", ".radioBtnCenter", function () {
            $('.hdnTxtChooseCenter').val($(this).attr('value'));
            $('#viewQuoteMapError').hide();
            var selectedQuoteAddress = $(this).attr('itemid');
            var viewMapQuoteLink;
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent) || screen.width <= 480) {
                viewMapQuoteLink = $('.viewDirectionQuoteMobile').val();
            }
            else {
                viewMapQuoteLink = $('.viewDirectionQuoteDesktop').val();
            }
            var formattedQuoteLink = viewMapQuoteLink.replace('{0}', selectedQuoteAddress);
            $('a.quote-view-map').attr('href', formattedQuoteLink);
        });

        function OnSuccess(response) {
            $('#searchLocationWaitImg').hide();
            if (response != "" && response != "[]") {
                $("#location_no_results").hide();
                var json = response;
                $("#rq_pick_location").show();
                $("#location_results_scroll").html("");
                for (i = 0; i < json.length; i++) {
                    $("#location_results_scroll").append("<p><input class='radioBtnCenter' itemid='" + json[i].Address1 + "+" + json[i].City + "," + json[i].State + "+" + json[i].Zipcode + "' type='radio' name='rq_list' value='" + json[i].FransId + "'/><span><strong>" + json[i].Address1 + "</strong><br/>" + json[i].City + ", " + json[i].State + "  " + json[i].Zipcode + " </span></p>");
                }
            }
            else {
                $("#rq_pick_location").show();
                $("#location_no_results").show();
            }
        }

        $('.quote-view-map').click(function () {
            var selectedQuoteCenter = $('.hdnTxtChooseCenter').val();
            if (selectedQuoteCenter != "") {
                $('#viewQuoteMapError').hide();
                return true;
            }
            else {
                $('#viewQuoteMapError').show();
                return false;
            }
        });

        //request a quot code ends here 

        $('#<%=btnRequestQuote.ClientID%>').click(function (e) {
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

            var description = $('.descProjectNational').val();
            if (description === undefined || description === null || description === "")
            {
                //$('.desc_project').addClass('requiredField');
                status2 = false;
            } else {
                //$('.descProjectNational').removeClass('required');
                status2 = true;
            }

            if (status1 == false || status2 == false)
                status = false;
            else
                status = true;

            var requiredcontrolsstatus = false; 
            //also check the validation for other controls            
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
            //upload files and submit data
            if (status) {
                $('.safWaitImg').show();
                if ((($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo')) &&
                 ($('.ajax__fileupload_queueContainer .ajax__fileupload_fileItemInfo').length > 1)) ||
                ($('.hddn_upload_files').val() != '')) {
                    MoveStatusBar();
                    //trigger fileupload control to save files
                    $('#ajaxFileUploadNational_UploadOrCancelButton').click();
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

    //ajax upload control code starts here
    function ClientUploadNationalComplete(sender, e) {
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

    function ClientUploadNationalCompleteAll(sender, e) {
        //doing file check with shared drive        
        var hdnUploadedFiles = document.getElementById('<%=hdnFileIDs.ClientID%>').value;
        var pageType = 'requestquotefiles';
        var fransId = '';
       
        //save form data
        //call Handler  
        SendRAQData();        
    }

    function ClientUploadNationalError() {
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
                    $('#ajaxFileUploadNational_Html5InputFile').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                    $('#ajaxFileUploadNational_SelectFileContainer').hide();
                }, 200);
            }
        }
        else {
            setTimeout(function () {
                $('#ajaxFileUploadNational_Html5DropZone').html('<h2 class="gray">Drag &amp; Drop Your Files Here</h2><p class="small_text gray">File size limit: 1GB. <br/>You can also <a href="javascript:void(0)" onclick="OpenBrowseWindow()" class="red-text uploadBrowseBtn">browse for a file.</a></p>');
                $('#ajaxFileUploadNational_SelectFileContainer').hide();
            }, 200);
        }
    }

    function OpenBrowseWindow() {
        $('#ajaxFileUploadNational_Html5InputFile').click();
    }

    function MoveStatusBar() {
        var firstPendingUploadFile = $('.pendingState').first();
        $('.ajax__fileupload_footer').appendTo(firstPendingUploadFile);
    }

    function SendRAQData() {
        var firstName = JSON.stringify(document.getElementById('<%=fnameNational.ClientID%>').value);
        var lastName = JSON.stringify(document.getElementById('<%=lnameNational.ClientID%>').value);
        var emailAddress = document.getElementById('<%=emailNational.ClientID%>').value;
        var selectedCenter = $('.hdnTxtChooseCenter').val();
        var phoneNumber = JSON.stringify(document.getElementById('<%=phoneNational.ClientID%>').value);
        var jobTitle = JSON.stringify(document.getElementById('<%=jobTitleNational.ClientID%>').value);
        var companyName = JSON.stringify(document.getElementById('<%=companyNational.ClientID%>').value);
        var projectName = JSON.stringify(document.getElementById('<%=pnameNational.ClientID%>').value);
        var projectBudget = JSON.stringify(document.getElementById('<%=txtProjectBudget.ClientID%>').value);
        var projectDescription = JSON.stringify(document.getElementById('<%=descProjectNational.ClientID%>').value);
        var uploadedFilesIds = document.getElementById('<%=hdnFileIDs.ClientID%>').value;

        var responseData = '';
        $.ajax({
            type: "POST",
            url: "/Handlers/SaveREQData.ashx",
            data: '{"firstName": ' + firstName + ',"lastName":' + lastName + ',"emailAddress":"' + emailAddress + '","selectedCenter":"' + selectedCenter + '","phoneNumber":' + phoneNumber + ',"jobTitle": ' + jobTitle + ',"companyName":' + companyName + ',"projectName":' + projectName + ',"projectBudget":' + projectBudget + ',"projectDescription":' + projectDescription + ',"uploadedFilesIds":"' + uploadedFilesIds + '" }',
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
                var fullUrl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
                var thankYouPage = '/thank-you/?type=requestaquote&centerId=';
                var localSiteCenterId = $('.hddnCenterId').val();
                if (localSiteCenterId != '')
                    thankYouPage = fullUrl + '/' + localSiteCenterId + thankYouPage + localSiteCenterId;
                else
                    thankYouPage = fullUrl + thankYouPage + selectedCenter;
                window.location.href = thankYouPage;
            }
        }

        $(document).ready(function () {
            FormatUploadControl_Quotes();
            Sys.Extended.UI.Resources.AjaxFileUpload_Remove = "X";            
        });
        //ajax upload control code ends here
</script>
<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 1 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
<div class="grid_8 request_quote_wrapper choose_center clearfix raq">
    <div class="request_quote">
        <h2>
            Choose a Center</h2>
        <div class="clearfix rq_form_body">
            <div class="form" id="choose_center">
                <asp:TextBox ID="hdnTxtChooseCenter" ValidationGroup="requestQuote" CssClass="hdnTxtChooseCenter required"
                    Style="display: none;" runat="server" />
                <asp:RequiredFieldValidator ID="rfv1" CssClass="error" runat="server" ControlToValidate="hdnTxtChooseCenter"
                    ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <input type="text" placeholder="Enter City, State, Zip" class="required" id="location" />
                <span class="required">*</span>
                <select class="custom-select" id="choose_center_distance">
                    <option selected="selected" value="25">25</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                    <option value="25">200</option>
                </select>
                <span class="required">*</span>
                <div class="clear">
                </div>
                <input class="white_btn search_location_requestQuote" type="button" id="choose_center_search"
                    value="Search" />
                <div id="searchLocationWaitImg" style="display: none;">
                    <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                </div>
            </div>
            <div class="clear">
            </div>
            <p id="viewQuoteMapError" style="display: none; color: red;">
                Please select a center.</p>
            <div id="rq_pick_location" style="display: none" class="clearfix">
                <input id="viewDirectionQuoteDesktop" type="hidden" class="viewDirectionQuoteDesktop"
                    runat="server" value="" />
                <input id="viewDirectionQuoteMobile" type="hidden" class="viewDirectionQuoteMobile"
                    runat="server" value="" />
                <div id="rq_pick_location_header">                    
                    <h3>
                        Pick a Location</h3>                     
                    <a class="cta-button-text quote-view-map view-map fancybox iframe" title="Your Location" href="#">
	                   <div class="cta-button-wrap black small cta-button-wrap-quote view-map">
		                <span>View Map</span>
	                   </div>
	                </a> <!-- end cta-button-wrap -->
                    <!-- go to google map page on devices smaller than 768px -->                 
                    <a class="cta-button-text quote-view-map" href="#" title="Your Location" target="_blank">
                        <div class="cta-button-wrap black small view-map-mobile"><span>View a Map</span></div>
                    </a>
                 <!-- end cta-button-wrap -->
                </div>
                <div class="form" id="rq_pick_location_list">
                    <!--The html for the div will be binded using the javascript -->
                    <div id="location_results_scroll" class="custom_form_scroll">
                    </div>
                    <div id="location_no_results" style="display: none" class="custom_form_scroll">
                        <p>
                            No locations found.</p>
                    </div>
                </div>
            </div>
            <!-- end rq_pick_location -->
        </div>
        <!-- end rq_form_body -->
    </div>
    <!-- end request_quote -->
</div>
<!-- end request_quote_wrapper -->
<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 2 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
<div class="grid_8 request_quote_wrapper  contact_info clearfix raq">
    <div class="request_quote">
        <h2>
            Contact Information</h2>
        <div class="rq_form_body clearfix">
            <div class="form" id="contact_project_info">
                <asp:TextBox ID="fnameNational" placeholder="First Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="fnameNational"
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="lnameNational" placeholder="Last Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="lnameNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="jobTitleNational" placeholder="Job Title" runat="server"   />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="jobTitleNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>--%>

                <asp:TextBox ID="companyNational" placeholder="Company" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="companyNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="emailNational" placeholder="Email Address" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="emailNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>

                <asp:TextBox ID="phoneNational" runat="server" placeholder="Phone Number"   class="no-margin-bottom required" />
                <span class="required">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="phoneNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>                
            </div>
            <p class="sample-entry" style="display: none;">
                Example 845-222-6666</p>
        </div>
        <!-- end rq_form_body -->
    </div>
    <!-- end request_quote -->
</div>
<!-- end request_quote_wrapper -->
<!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm COL 3 
mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
<div class="grid_8 request_quote_wrapper project_info clearfix project_info_national raq">
    <div class="request_quote">
        <h2>
            Project Information</h2>
        <div class="rq_form_body clearfix">
            <div class="form" id="project_info">
                <asp:TextBox ID="pnameNational" placeholder="Project Name" CssClass="required" runat="server"   />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="pnameNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>
                <textarea cols="20" rows="4" placeholder="Describe Your Project" runat="server" class="descProjectNational required"   id="descProjectNational"></textarea>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="descProjectNational" ForeColor="Red" CssClass="required" runat="server" 
                     ValidationGroup="requestQuote"></asp:RequiredFieldValidator>
                <span class="required">*</span>
                <div class="clear"></div>
                <div class="drag_drop_file">
                    <div class="drag_drop_placeholder">
                        <AjaxToolkit:AjaxFileUpload ID="ajaxFileUploadNational" BorderStyle="None" runat="server"
                            OnUploadComplete="Upload_NationalComplete"
                            OnClientUploadComplete="ClientUploadNationalComplete"                                                   
                            ClientIDMode="Static"
                            OnClientUploadCompleteAll="ClientUploadNationalCompleteAll"
                            OnClientUploadError="ClientUploadNationalError"
                            ThrobberID="loader" />
                        <asp:HiddenField ID="hdnFileIDs" runat="server" Value="" />
                        <%--<h2 class="gray">Drag &amp; Drop Your Files Here</h2>
                         <p class="small_text gray">File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT. You can also <a class="red-text" href="#">browse for a file.</a></p>--%>
                    </div>
                    <!-- end drag_drop_placeholder -->
                  <%--  <input type="text" id="upload_files" class="upload_files"  >--%>
                    <asp:TextBox ID="txtUploadFiles" Style="display: none;"   CssClass="hddn_upload_files" runat="server"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator ID="rfv3" Style="margin: 0px;" ForeColor="Red" CssClass="required" runat="server" ControlToValidate="txtUploadFiles"
                        ValidationGroup="requestQuote">*</asp:RequiredFieldValidator> --%> 
                    <br />                  
                </div>
                <!-- end drag_drop_file
                <span class="required">*</span> -->
                <div class="clear"></div> 
                <asp:TextBox ID="txtProjectBudget" runat="server" placeholder="Project Budget" /> 
                <div class="clear"></div>
                <p class="terms_conditions">
                    <input type="checkbox" id="termsNational" runat="server"   />
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
<div class="clear">
</div>
<div class="request_quote_tagline">
    <div class="grid_20">
        <div class="statement-text">
            <p>
                We're here to help. Tell us about your next project.</p>
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
        <div class="form" id="request_quote_submit_form">
            <asp:Button ID="btnRequestQuote" CssClass="request_quote_submit btnRequestQuote" runat="server"
                Text="Submit" ValidationGroup="requestQuote" />
        </div>
    </div>
    <!-- end grid_4 -->
</div>
<!-- end request_quote_tagline -->

