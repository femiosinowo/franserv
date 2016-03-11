<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UtilityNav.ascx.cs" Inherits="UserControls_UtilityNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%--
<%@ Register Src="~/UserControls/deprecated/RequestAQuoteNational.ascx" TagPrefix="ux" TagName="RequestAQuoteNational" %>
<%@ Register Src="~/UserControls/deprecated/RequestAQuoteLocal.ascx" TagPrefix="ux" TagName="RequestAQuoteLocal" %>--%>
<%@ Register Src="~/UserControls/SendAFileOnHeader.ascx" TagPrefix="ux" TagName="SendAFileOnHeader" %>
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
                var distance = $('#find_location_form .transformSelectDropdown .selected span').text();
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

            var distanceVal = $('#find_location_form .transformSelectDropdown .selected span').text();
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
                status = false;
            else
                status = true;


            return status;
        });
    });
</script>
<!-- mmm Utility Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Utility Nav (both) mmm -->
<div class="utility_nav_wrapper  clearfix">
    <div class="utility_nav clearfix">
        <div class="container_24">
            <div class="grid_20">
                <asp:Panel ID="pnlNationalLeft" runat="server">
                    <div class="utility_nav_left">
                        <div class="utility_nav_sub_left">
                            <ul>
                                <li class="franchise_ops" id="franchise_ops" runat="server"><a class="utility_link"
                                    href="#franchise_national"><span>Learn About </span>Franchise Op<span>portunities</span></a></li>
                            </ul>
                        </div>
                        <!--utility nav sub left-->
                        <div class="utility_nav_sub_right">
                            <ul>
                                <li><asp:HyperLink runat="server" id="HyperLink1" NavigateUrl="/requestquote">Request a Quote</asp:HyperLink>
                                   <%-- <a class="utility_link utility_raq" href="#request_quote_national">Request a Quote</a>--%>
                                </li>
                                <li><asp:HyperLink  CssClass="utility_link utility_saf"  id="nationalsendafilelink" runat="server">Send a File</asp:HyperLink>
                                </li>
                                <%--<li class="search_link"><span>Search</span><input type="search"></li>--%>
                                <%--<li class="search_link_mobile"><a class="utility_link" href="#site_search"></a></li>--%>
                            </ul>
                        </div>
                        <!--utility nav sub right-->
                    </div>
                    <!--utility_nav_left-->
                </asp:Panel>
                <asp:Panel ID="pnlLocalLeft" runat="server" Visible="false">
                    <div class="utility_nav_left">
                        <div class="utility_nav_sub_left">
                            <ul>
                                <li class="request_quote_local">
                                    <asp:HyperLink runat="server" id="HyperLink2" >Request a Quote</asp:HyperLink>
                                    <%--<a class="utility_link" href="#request_quote_national">
                                    Request a Quote</a>--%>
                                </li>
                                <li class="send_file_local"><asp:HyperLink runat="server" CssClass="utility_link" id="localsendafilelink">Send a File</asp:HyperLink>
                                </li>
                            </ul>
                            <ul>
                                <li class="easydocs"><a id="mydocs_link" href="/login" target="_blank">EasyDocs</a></li>
                                <%-- <li class="share_icon"><a class="utility_link" href="#social_media">Social Media</a></li>--%>
                                <!-- <li class="search_icon"><a class="utility_link" href="#site_search">Search</a></li> -->
                            </ul>
                        </div>
                        <div class="utility_nav_sub_right">
                            <ul>
                                <li class="local_phone"><span>Call Us</span>
                                    <asp:Literal ID="ltrContactUnmber" runat="server"></asp:Literal></li>
                                <li class="share_icon"><a class="utility_link" href="#social_media">Social Media</a></li>
                            </ul>
                        </div>
                        <!--utility nav sub right-->
                    </div>
                </asp:Panel>
            </div>
            <!--top header left-->
            <div class="grid_4">
                <asp:Panel ID="pnlNational" runat="server">
                    <div class="utility_nav_right">
                        <ul>
                            <li class="share_icon"><a class="utility_link" href="#social_media">Social Media</a></li>
                            <li class=""><a href="http://www.marketingtango.com/" target="_blank">Blog</a></li>
                            <li class="find_a_location_icon"><a href="/find-locator/">Find a <span>Location</span></a></li>
                        </ul>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlLocal" Visible="false" runat="server">
                    <div class="utility_nav_right">
                        <ul>
                            <li class=""><a id="blogsLink" runat="server" href="http://www.marketingtango.com/" target="_blank">Blog</a></li>
                            <li class="shop_link"><a href="/Shop/">Shop</a></li>
                            <li class="contact_link"><a class="utility_link" href="/Contact-Us/">Contact</a></li>
                        </ul>
                    </div>
                </asp:Panel>
            </div>
            <!--top header right-->
            <div class="clear">
            </div>
            <div class="utility_content_wrapper clearfix">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="close_utility_btn clearfix">
                            <a href="#">
                                <img src="/images/close_x_black.png" alt="Close" />
                            </a>
                        </div>
                        <!-- end close_utility_btn -->
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
                            <asp:Literal ID="ltrSocialIcons" runat="server"></asp:Literal>
                        </div>
                        <!-- end social_media -->
                        <div class="utility_content national" id="request_quote_national">
                            <!-- mmmmmmmmmmmmmmmmmm REQUEST QUOTE (NATIONAL) mmmmmmmmmmmmmmmmmm -->
                           <%-- <ux:RequestAQuoteNational ID="uxRequestNational" runat="server" Visible="False"/>--%>
                            <!-- end request_quote national -->
                            <!-- mmmmmmmmmmmmmmmmmm REQUEST QUOTE (LOCAL) mmmmmmmmmmmmmmmmmm -->
                         <%--   <ux:RequestAQuoteLocal ID="uxRequestLocal" Visible="false" runat="server" />--%>
                            <!-- end request_quote national -->
                        </div>
                        <!-- mmmmmmmmmmmm FRANCHISE OPPORTUNITIES (NATIONAL) mmmmmmmmmmmmmm -->
                        <div class="utility_content national" id="franchise_national">
                            <CMS:ContentBlock ID="cbFransOpport" runat="server" DoInitFill="false" />
                        </div>
                        <!-- end franchise_national -->
                        <!-- mmmmmmmmmmmm SEND FILE (BOTH) mmmmmmmmmmmmmm -->
                        <div id="temp" runat="server">
                        </div>
                        <%--<div class="utility_content" id="send_file">--%>
                        <ux:SendAFileOnHeader ID="uxSendAFile" runat="server" Visible="true" />
                        <%--</div>--%>
                        <!-- end send_file -->
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24 -->
                <!-- mmmmmmmmmmmm FIND LOCATION (NATIONAL) mmmmmmmmmmmmmm -->
                <!-- end find_location -->
            </div>
            <!-- end utility_content_wrapper -->
        </div>
        <!-- end container_24 -->
    </div>
    <!-- end utility_nav -->
</div>
<!-- end utility_nav_wrapper -->
<div class="clear">
</div>
