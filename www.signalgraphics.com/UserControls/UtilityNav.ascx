<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UtilityNav.ascx.cs" Inherits="UserControls_UtilityNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/RequestAQuoteNational.ascx" TagPrefix="ux" TagName="RequestAQuoteNational" %>
<%@ Register Src="~/UserControls/RequestAQuoteLocal.ascx" TagPrefix="ux" TagName="RequestAQuoteLocal" %>
<%@ Register Src="~/UserControls/SendAFileOnHeader.ascx" TagPrefix="ux" TagName="SendAFileOnHeader" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxToolkit" %>

<AjaxToolkit:ToolkitScriptManager ID="toolKitScriptMngrNational1" runat="server" />
<script type="text/javascript">
    $(document).ready(function () {
        // find location code starts here
        $('#my_location_search_btn').click(function (e) {
            e.preventDefault();
            //enter_location           
            var validation = DoValidationForLocation();
            if (validation) {
                var address = $('.txtFindLocation').val();
                address = address.replace(' ', '+');
                var distance = $('#find_location_form ul.transformSelect li').eq('0').find('span:first').text();
                window.location.href = '/find-locator/?location=' + address + '&distance=' + distance + '';
            }            
        });

        function DoValidationForLocation() {            
            var status = false;
            var locationStatus = false;
            var distanceStatus = false;

            if ($('.txtFindLocation').val() != '') {
                locationStatus = true; $('.txtFindLocation').removeClass('requiredField');
            }
            else {
                $('.txtFindLocation').addClass('requiredField');
                locationStatus = false;
            }

            var distanceVal = $('#find_location_form ul.transformSelect li').eq('0').find('span:first').text();
            if (distanceVal != undefined && distanceVal != 'Distance') {
                distanceStatus = true; $('#find_location_form .transformSelect li:first').removeClass('requiredField');
            }
            else {
                $('#find_location_form .transformSelect li:first').addClass('requiredField');
                distanceStatus = false;
            }
              
            if (locationStatus == false || distanceStatus == false)
                status = false;
            else
                status = true;

            return status;
        }

        //find location code ends here

        $('.btnRequestQuote').click(function (e) {
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
            }

            var description = $('.descProjectNational').val();
            if (description != "") {
                //$('.descProjectNational').removeClass('required');
                status2 = true;
            }
            else {
                //$('.desc_project').addClass('requiredField');
                status2 = false;
            }

            if (status1 == false || status2 == false)
                status = false
            else
                status = true;


            return status;
        });
    });
</script>
<!-- mmm Utility Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Utility Nav (both) mmm -->
<div class="utility_nav_wrapper  clearfix">
    <div class="utility_nav clearfix">
        <div class="utility_nav_wrapper clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <asp:Panel ID="pnlNationalLeft" runat="server">
                         <div class="utility_nav_left">
                            <ul>
                                <li id="franchise_ops" runat="server" class="franchise_ops"><a href="#franchise_national" class="utility_link">Franchise Opportunities</a></li>
                                <li class="quote_icon"><asp:HyperLink runat="server" id="HyperLink1" NavigateURL="/requestquote">Request a Quote</asp:HyperLink>
                                  <%-- <a class="utility_link" href="#request_quote_national">Request a Quote</a>--%></li>
                                <li class="send_icon"><asp:HyperLink  CssClass="utility_link utility_saf"  id="nationalsendafilelink" runat="server">Send a File</asp:HyperLink></li>
                                <li class="share_icon"><a class="utility_link" href="#social_media">Social Media</a></li>
                                <!-- <li class="search_icon"><a class="utility_link" href="#site_search">Search</a></li> -->
                            </ul>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlLocalLeft" runat="server" Visible="false">
                         <div class="utility_nav_left">
                        <ul>
                            <li class="quote_icon local active">
                                <asp:HyperLink runat="server" id="HyperLink2">Request a Quote</asp:HyperLink>
                                <%--<a class="utility_link" href="#request_quote_national">Request a Quote</a>--%>
                            </li>
                            <li class="send_icon local active"><asp:HyperLink runat="server" CssClass="utility_link" id="localsendafilelink">Send a File</asp:HyperLink></li>
                            <%--<li class="subscribe"><a class="fancybox" id="subscribe_lb" rel="group" href="#subscribe">Subscribe</a></li>--%>
                            <li class="mydocs"><a id="mydocs_link" href="/login" target="_blank">QuickDocs</a></li>
                            <li class="share_icon"><a class="utility_link" href="#social_media">Social Media</a></li>
                            <!-- <li class="search_icon"><a class="utility_link" href="#site_search">Search</a></li> -->
                        </ul>
                    </div>
                    </asp:Panel>
                   
                    <!--top header left-->
                    <asp:Panel ID="pnlNational" runat="server">
                        <div class="utility_nav_right active">
                             <a id="find_location_link" class="utility_link" href="#find_location">Find a Location</a>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlLocal" Visible="false" runat="server">
                        <div class="utility_nav_right">
                            <ul>
                                <li class="phone"><span>
                                    <asp:Literal ID="ltrContactUnmber" runat="server"></asp:Literal></span></li>
                                <li class="contact_us purple"><a href="/contact-us/">Contact Us</a></li>
                                <li class="shop purple"><a href="/shop/">Shop</a></li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <!--top header right-->
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- utility_nav_wrapper-->
        <div class="clear"></div>
        <div class="utility_content_wrapper clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div id="search_social_close_wrapper">
                        <div id="search_social">
                            <!-- mmmmmmmmmmmmmmmmmm SEARCH (BOTH) mmmmmmmmmmmmmmmmmm -->
                            <%--<div class="utility_content" id="site_search">
                                <div class="form" id="search">
                                    <input type="text" id="search_term" />
                                    <input type="submit" class="red_btn" value="Search" />
                                </div>
                            </div>--%>
                            <!-- end site_search -->
                            <!-- mmmmmmmmmmmmmmmmmm SOCIAL MEDIA ICONS (BOTH) mmmmmmmmmmmmmmmmmm -->
                            <div class="utility_content" id="social_media">
                                <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                            </div>
                            <!-- end social_media -->
                        </div>
                        <!-- end search_social -->
                        <div class="close_utility_btn clearfix">
                            <a href="#">
                                <img src="/images/close_x_white.png" alt="Close" />
                            </a>
                        </div>
                        <!-- end close_utility_btn -->
                    </div>
                    <!-- end search-social-close wrapper -->                    
                    <div class="utility_content national" id="request_quote_national" >
                        <!-- mmmmmmmmmmmmmmmmmm REQUEST QUOTE (NATIONAL) mmmmmmmmmmmmmmmmmm -->
     <%--                    <ux:RequestAQuoteNational id="uxRequestNational" runat="server" Visible="False" />--%>
                        <!-- end request_quote national -->
                        <!-- mmmmmmmmmmmmmmmmmm REQUEST QUOTE (LOCAL) mmmmmmmmmmmmmmmmmm -->
              <%--           <ux:RequestAQuoteLocal ID="uxRequestLocal" Visible="false" runat="server" />--%>
                        <!-- end request_quote national -->
                    </div> 
                    <!-- mmmmmmmmmmmm FRANCHISE OPPORTUNITIES (NATIONAL) mmmmmmmmmmmmmm -->
                    <div class="utility_content national" id="franchise_national">
                        <CMS:ContentBlock ID="cbFransOpport" runat="server" DoInitFill="false" />
                        <!-- end franchise_content_wrapper -->
                    </div>
                    <!-- end franchise_national -->
                    <!-- mmmmmmmmmmmm SEND FILE (BOTH) mmmmmmmmmmmmmm -->
                    <div id="temp" runat="server"></div>
                    <%--<div class="utility_content" id="send_file">--%>
                        <ux:SendAFileOnHeader id="uxSendAFile" runat="server" Visible="true" />
                    <%--</div>--%>
                    <!-- end send_file --> 
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24 -->
            <!-- mmmmmmmmmmmm FIND LOCATION (NATIONAL) mmmmmmmmmmmmmm -->
            <div class="utility_content national clearfix" id="find_location">
                <div id="find_location_map_wrapper" class="clearfix">
                    <div id="find_location_map_ajaxImg">
                        <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                    </div>
                    <div id="find_location_map"></div>
                </div>
                <!-- end find_location_map_wrapper -->
                <div id="find_location_searchbox_wrapper">
                    <div class="container_24">
                        <div class="grid_8">
                            <div id="find_location_searchbox" class="request_quote clearfix">
                                <h2>Find a Location</h2>
                                <div class="prefix_2 grid_20 suffix_2">
                                    <div class="form" id="find_location_form">
                                        <asp:TextBox ID="txtFindLocation" CssClass="txtFindLocation" runat="server" placeholder="Enter City, State, Zip" ></asp:TextBox>
                                        <!-- <span class="required">*</span> -->
                                        <%--<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFindLocation" CssClass="required"></asp:RequiredFieldValidator>--%>
                                        <select class="custom-select" id="distance">
                                            <option selected="selected" value="25">25</option>                                        
											<option value="50">50</option>
											<option value="100">100</option>
											<option value="25">200</option>                                           
                                        </select>
                                        <!-- <span class="required">*</span> -->
                                        <asp:TextBox ID="txtFindLocationChoose" CssClass="txtFindLocationChoose" runat="server" Style="display: none;"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtFindLocationChoose" CssClass="required"
                                             ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        <div class="clear"></div>
                                        <input type="submit" id="my_location_search_btn" value="Find Now" />
                                        <div class="utilityNavFindAllLoc" style="float:right;"><a href="/find-locator/#all_locations">See All Locations</a></div>
                                    </div>
                                    <div class="clear"></div>
                                    <!-- end rq_pick_location -->
                                </div>
                                <!-- end grid -->
                            </div>
                            <!-- end find_location_searchbox -->
                        </div>
                        <!--end grid 24-->

                    </div>
                    <!-- end container_24 -->
                </div>
                <!-- end find_location_searchbox_wrapper -->
                <div class="clear"></div>
                <%--<div id="find_location_menu" class="clearfix">
                    <div class="container_24">
                        <div class="grid_24">
                            <ul>
                                <li class="active">
                                    <a href="#" id="fl-north-america" class="fl-menu-link">
                                        <div class="find-location-figure">
                                            <div></div>
                                        </div>
                                        <div class="find-location-name"><span>North America</span></div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" id="fl-south-america" class="fl-menu-link">
                                        <div class="find-location-figure">
                                            <div></div>
                                        </div>
                                        <div class="find-location-name"><span>South America</span></div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" id="fl-europe" class="fl-menu-link">
                                        <div class="find-location-figure">
                                            <div></div>
                                        </div>
                                        <div class="find-location-name"><span>Europe</span></div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" id="fl-africa" class="fl-menu-link">
                                        <div class="find-location-figure">
                                            <div></div>
                                        </div>
                                        <div class="find-location-name"><span>Africa &amp; Middle East</span></div>
                                    </a>
                                </li>
                                <li>
                                    <a href="#" id="fl-asia" class="fl-menu-link">
                                        <div class="find-location-figure">
                                            <div></div>
                                        </div>
                                        <div class="find-location-name"><span>Asia</span></div>
                                    </a>
                                </li>
                            </ul>

                        </div>
                        <!--end grid 24-->

                    </div>
                    <!-- end container_24 -->
                </div>--%>
                <!-- end find_location_menu_wrapper -->

            </div> <!-- end find_location -->
        </div>
        <!-- end utility_content_wrapper -->
    </div>
    <!-- end utility_nav -->
</div>
<!-- end utility_nav_wrapper -->
<div class="clear"></div>
