<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="register.aspx.cs" Inherits="register" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
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

                var distanceVal = $('#choose_center_pageform .transformSelectDropdown .selected span').text();
                if (distanceVal != undefined && distanceVal != 'Distance') {
                    distanceStatus = true; $('#choose_center_pageform .transformSelect li:first').removeClass('requiredField');
                }
                else {
                    $('#choose_center_pageform .transformSelect li:first').addClass('requiredField');
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
                var distance = $('#choose_center_pageform .transformSelectDropdown .selected span').text();
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
    <asp:Panel ID="pnlNationalRegister" DefaultButton="btnNationalRegister" runat="server">
        <div class="site_container national send_file" id="register-send-file">
            <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
            <div class="header_image_wrapper clearfix">
                <div class="header_image">
                    <div class="header_image_content">
                        <%--<img src="images/headers/header_3.jpg" alt="Send a File"> --%>
                        <CMS:ContentBlock ID="cbRegisterHeaderImage" runat="server" DynamicParameter="id"
                            CacheInterval="300" />
                    </div>
                    <!-- header image-->
                </div>
                <!-- end header_image -->
            </div>
            <!-- end header_image_wrapper-->
            <div class="clear">
            </div>
            <!-- mmm Register Content (National/Local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Register Content (National/Local) mmm -->
            <div class="send_file_content_wrapper  clearfix">
                <div class="send_file_content clearfix">
                    <div id="register_send_file_content" class="container_24">
                        <div class="send_file_main grid_18 col-height-equal">
                            <div class="headline-block headline-block-white">
                                <div class="headline-content-outer">
                                    <div class="headline-content-inner">
                                        <span class="headline-block-icon-white"></span>
                                        <h2 class="headline">Register</h2>
                                    </div>
                                    <!--headline content-->
                                </div>
                                <!--headline content-->
                            </div>
                            <!-- mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm choose center page form mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm -->
                            <div class="prefix_1 grid_22 suffix_1 alpha">
                                <div id="choose_center_pageform" class="form clearfix">
                                    <asp:Label ID="lblError" runat="server" CssClass="required"></asp:Label>
                                    <asp:ValidationSummary ID="validationSummary" CssClass="required" ValidationGroup="NationalRegister"
                                        HeaderText="Please fill out all the required fields(*). " ShowSummary="true" runat="server" />
                                    <br />
                                    <h3>Choose a Center</h3>
                                    <asp:TextBox ID="txtRegisterLocation" CssClass="txtRegisterLocation" runat="server"
                                        placeholder="Enter City, State, Zip*" required="required"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtRegisterLocation"
                                        CssClass="required" ErrorMessage="Enter City, State, Zip" ValidationGroup="NationalRegister">*</asp:RequiredFieldValidator>
                                    <select id="distance" class="custom-select">
                                        <option selected="selected" value="25">25</option>
                                        <option value="50">50</option>
                                        <option value="100">100</option>
                                        <option value="25">200</option>
                                    </select>
                                    <span class="required">*</span>
                                    <div class="clear"></div>
                                    <asp:TextBox ID="txtRegisterChooseLocation" CssClass="txtRegisterChooseLocation"
                                        runat="server" Style="display: none;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtRegisterChooseLocation"
                                        CssClass="required" ErrorMessage="Choose a location" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="clear"></div>
                                    <input class="white_btn search registerSearchLocation" type="button" id="registerSearchLocation"
                                        value="Search" />
                                    <div class="clear"></div>
                                    <div id="searchLocationWaitImg_register" style="display: none;">
                                        <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                                    </div>
                                    <div class="clear"></div>
                                    <br />
                                    <div id="rq_pick_location_register" style="display: none" class="clearfix">
                                        <input id="viewDirectionRegisterDesktop" type="hidden" class="viewDirectionRegisterDesktop" runat="server" value="" />
                                        <input id="viewDirectionRegisterMobile" type="hidden" class="viewDirectionRegisterMobile" runat="server" value="" />
                                        <h3>Pick a Location       
                                            <div class="cta-button-wrap white-btn small cta-button-wrap-register view-map">
                                                <a class="cta-button-text register-view-map view-map fancybox iframe" href="#"><span>View a Map</span></a>
                                            </div>
                                            <!-- end cta-button-wrap -->
                                            <!-- go to google map page on devices smaller than 768px -->
                                            <div class="cta-button-wrap gold small view-map-mobile">
                                                <a class="cta-button-text register-view-map" href="#" target="_blank"><span>View a Map</span></a>
                                            </div>
                                        </h3>
                                        <p id="viewCenterMapError" style="display: none; color: red;">Please select a center.</p>
                                        <div class="form" id="rq_pick_location_list_register">
                                            <!--The html for the div will be binded using the javascript -->
                                            <div id="location_register_results_scroll" class="custom_form_scroll"></div>
                                            <div id="location_register_no_results" style="display: none" class="custom_form_scroll">
                                                <p>No locations found.</p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end rq_pick_location -->
                                    <!--<div class="center_location_map">
							           <div class="custom_form_scroll">
											        <p><input type="radio" value="1" name="rq_list"> <span>16500 Monterey Road<br>
									        Morgan Hill, CA 95037</span></p>									
											        <p><input type="radio" value="2" name="rq_list"> <span>185 Park Ave., Ste. 187<br>
									        San Jose, CA 95113</span></p>									
									        <p><input type="radio" value="3" name="rq_list"> <span>16500 Monterey Road<br>
									        Morgan Hill, CA 95037</span></p>									
											        <p><input type="radio" value="4" name="rq_list"> <span>185 Park Ave., Ste. 187<br>
									        San Jose, CA 95113</span></p>
								        </div>								
								        <div class="center_location_map_content">
								        </div>	
							        </div>-->
                                    <!-- center location map-->
                                    <hr />
                                    <div class="clear"></div>
                                    <div class="form" id="contact_project_info_pageform">
                                        <h3>Contact Information</h3>
                                        <ul class="contact_project_info_two_col">
                                            <li>
                                                <%--<input type="text" placeholder="First Name" id="contact_info_fname"><span class="required">*</span--%>
                                                <asp:TextBox ID="national_fname" runat="server" placeholder="First Name*"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="national_fname"
                                                    CssClass="required" ErrorMessage="First name is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </li>
                                            <li>
                                                <%--<input type="text" placeholder="Last Name" id="contact_info_lname"> <span class="required">*</span>--%>
                                                <asp:TextBox ID="national_lname" runat="server" placeholder="Last Name*"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="national_lname"
                                                    CssClass="required" ErrorMessage="Last name is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </li>
                                        </ul>
                                        <ul class="contact_project_info_two_col">
                                            <li>
                                                <%--<input type="text" placeholder="Email Address" id="contact_info_email"><span class="required">*</span>--%>
                                                <asp:TextBox ID="national_email" runat="server" placeholder="Email*" TextMode="Email"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="national_email"
                                                    CssClass="required" ErrorMessage="Email is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            </li>
                                            <li>
                                                <%--<input type="text" placeholder="Phone Number" id="contact_info_phone">  <span class="required">*</span>--%>
                                                <asp:TextBox ID="national_phone" runat="server" placeholder="Phone Number*"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="national_phone"
                                                    CssClass="required" ErrorMessage="Phone Number is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                <p class="sample-entry">
                                                    Example 845-222-6666
                                                </p>
                                            </li>
                                        </ul>
                                        <hr />
                                        <div class="clear"></div>
                                        <h3>Let Us Know More About You</h3>
                                        <p>
                                            <%--<input type="text" placeholder="Job Title" id="job_title" />--%>
                                            <asp:TextBox ID="national_job_title" runat="server" placeholder="Job Title"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="job_title"
                                    CssClass="required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                        </p>
                                        <p>
                                            <%--<input type="text" placeholder="Company" id="company" />--%>
                                            <asp:TextBox ID="national_company" runat="server" placeholder="Company"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="company"
                                    CssClass="required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                        </p>
                                        <p>
                                            <%--<input type="text" placeholder="Website" id="website" />--%>
                                            <asp:TextBox ID="national_website" runat="server" placeholder="Website" TextMode="Url"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfv9" runat="server" ControlToValidate="website"
                                    CssClass="required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                        </p>
                                        <hr />
                                        <div class="clear"></div>
                                        <h3>User Information</h3>
                                        <%--<p><input type="text" placeholder="Username" id="register_username" /></p>
						    <p><input type="text" placeholder="Password" id="register_password" /></p>
						    <p><input type="text" placeholder="Confirm Password" id="register_confirm_password" /></p>--%>
                                        <p>
                                            <%--<input type="password" placeholder="Password" required="required" id="password" />
                                <span class="required">*</span>--%>
                                            <asp:TextBox ID="national_password" runat="server" placeholder="Password*" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv11" runat="server" ControlToValidate="national_password"
                                                CssClass="required" ErrorMessage="Password is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <%--<input type="password" placeholder="Confirm Password" required="required" id="confirm_password" />
                                <span class="required">*</span>--%>
                                            <asp:TextBox ID="national_confirm_password" runat="server" placeholder="Confirm Password*"
                                                TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv12" runat="server" ControlToValidate="national_confirm_password"
                                                CssClass="required" ErrorMessage="Confirm password is required" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="compareValidatior" runat="server" ControlToCompare="national_password" ControlToValidate="national_confirm_password"
                                                CssClass="required" ErrorMessage="Password & Confirm password do not match" ValidationGroup="NationalRegister" ForeColor="Red">*</asp:CompareValidator>
                                        </p>
                                        <div class="email_signup_checkbox">
                                            <input type="checkbox" id="national_opt_in" value="1" />
                                            <label for="national_opt_in" <%--class="small-text"--%>>
                                                <strong>Yes!</strong> Send me e-mail updates about the latest products and promotions.</label>
                                        </div>
                                        <!-- email signup checkbox-->
                                        <%--<input class="white_btn" type="submit" id="register_btn" value="Register" />--%>
                                        <asp:Button ID="btnNationalRegister" runat="server" CssClass="white_btn" ValidationGroup="NationalRegister"
                                            Text="Register" OnClick="btnNationalRegister_Click" />
                                    </div>
                                </div>
                            </div>
                            <!-- end prefix_1 grid_22 suffix_1 alpha -->
                        </div>
                        <!-- end send_file_main grid_18 col-height-equal -->
                        <div class="send_file_sidebar grid_6 col-height-equal">
                            <div class="prefix_2 grid_20 suffix_2">
                            </div>
                        </div>
                        <!-- sidebar-->
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlLocalRegister" Visible="false" DefaultButton="btnLocalRegister" runat="server">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image">
                <div class="header_image_content">
                    <%--<img src="images/headers/header_3.jpg" alt="Send a File"> --%>
                    <CMS:ContentBlock ID="Contentblock1" runat="server" DynamicParameter="id"
                        CacheInterval="300" />
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear">
        </div>
        <div class="send_file_content_wrapper  clearfix">
            <div class="send_file_content clearfix">
                <div id="register_local_send_file_content" class="container_24">
                    <div class="send_file_main grid_18 col-height-equal">
                        <div class="headline-block headline-block-white">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-white"></span>
                                    <h2 class="headline">Register</h2>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                        <!-- mmmmmmmmmmmmmmmmmmmm local register form mmmmmmmmmmmmmmmmmmmmmmmmmm -->
                        <div class="prefix_1 grid_22 suffix_1 alpha">
                            <h3>Your Location</h3>
                            <div class="grid_8" id="sf_location_info">
                                <asp:Literal runat="server" ID="litFranchiseContactInfo"></asp:Literal>
                            </div>
                            <!-- end your_location_info -->
                            <div class="grid_13 suffix_1 static_map" id="sf_location_map">
                                <img id="googleMapImage" runat="server" src="#" alt="map" />
                                <!-- DESKTOP/TABLET BUTTON -->
                                <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server"
                                    title="Your Location" href="#">VIEW MAP</a>
                                <!-- Mobile Link: go to google map page on devices smaller than 768px -->
                                <a class="red-text view-map-mobile" id="viewDirectionMobile" runat="server" target="_blank"
                                    href="#">VIEW MAP</a>
                            </div>
                            <!-- end sf_location_map -->
                            <hr />
                            <div class="form" id="contact_project_info">
                                <asp:Label ID="localErrorMessage" runat="server" CssClass="required"></asp:Label>
                                <asp:ValidationSummary ID="validationSummary1" CssClass="required" ValidationGroup="LocalRegister"
                                    HeaderText="Please fill out all the required fields(*). " ShowSummary="true" runat="server" />
                                <br />
                                <h3>Contact Information</h3>
                                <ul class="contact_project_info_two_col">
                                    <li>
                                        <asp:TextBox ID="local_fname" runat="server" placeholder="First Name*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="local_fname"
                                            CssClass="required" ErrorMessage="First name is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="local_lname" runat="server" placeholder="Last Name*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="local_lname"
                                            CssClass="required" ErrorMessage="Last name is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </li>
                                </ul>
                                <ul class="contact_project_info_two_col">
                                    <li>
                                        <asp:TextBox ID="local_email" runat="server" placeholder="Email*" TextMode="Email"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="local_email"
                                            CssClass="required" ErrorMessage="Email is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    </li>
                                    <li>
                                        <asp:TextBox ID="local_phone" runat="server" placeholder="Phone Number*"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="local_phone"
                                            CssClass="required" ErrorMessage="Phone Number is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <p class="sample-entry">
                                            Example 845-222-6666
                                        </p>
                                    </li>
                                </ul>
                                <hr />
                                <div class="clear"></div>
                                <h3>Let Us Know More About You</h3>
                                <p>
                                    <asp:TextBox ID="local_job_title" runat="server" placeholder="Job Title"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfv7" runat="server" ControlToValidate="job_title"
                                CssClass="required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </p>
                                <p>
                                    <asp:TextBox ID="local_company" runat="server" placeholder="Company"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfv8" runat="server" ControlToValidate="company"
                                CssClass="required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </p>
                                <p>
                                    <asp:TextBox ID="local_website" runat="server" placeholder="Website" TextMode="Url"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfv9" runat="server" ControlToValidate="website"
                                CssClass="required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>--%>
                                </p>
                                <hr />
                                <div class="clear"></div>
                                <h3>User Information</h3>
                                <%--<p>
                            <input type="text" placeholder="Username" required="required" id="username" />
                            <span class="required">*</span>
                            <asp:TextBox ID="local_username" runat="server" placeholder="Username"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="local_username"
                                CssClass="required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </p>--%>
                                <p>
                                    <%--<input type="password" placeholder="Password" required="required" id="password" />
                            <span class="required">*</span>--%>
                                    <asp:TextBox ID="local_password" runat="server" placeholder="Password*" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="local_password"
                                        CssClass="required" ErrorMessage="Password is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                </p>
                                <p>
                                    <%--<input type="password" placeholder="Confirm Password" required="required" id="confirm_password" />
                            <span class="required">*</span>--%>
                                    <asp:TextBox ID="local_confirm_password" runat="server" placeholder="Confirm Password*"
                                        TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="local_confirm_password"
                                        CssClass="required" ErrorMessage="Confirm Password is required" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="local_password" ControlToValidate="local_confirm_password"
                                        CssClass="required" ErrorMessage="Password & Confirm password do not match" ValidationGroup="LocalRegister" ForeColor="Red">*</asp:CompareValidator>
                                </p>
                                <div class="email_signup_checkbox">
                                    <input type="checkbox" id="Checkbox1" value="1" />
                                    <label>
                                        <strong>Yes!</strong> Send me e-mail updates about the latest products and promotions.</label>
                                </div>
                                <div class="clear">
                                </div>
                                <asp:Button ID="btnLocalRegister" runat="server" CssClass="white_btn" Text="Register"
                                    ValidationGroup="LocalRegister" OnClick="btnLocalRegister_Click" />
                            </div>
                        </div>                        
                    </div>
                    <div class="send_file_sidebar grid_6 col-height-equal">
                        <div class="prefix_2 grid_20 suffix_2">
                        </div>
                    </div>
                    <!-- sidebar-->
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>


