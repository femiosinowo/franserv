<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="thank_you.aspx.cs" Inherits="thank_you" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>


<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <script type="text/javascript">
        function DoPostForLogOut() {
            __doPostBack('<%=log_out.ClientID %>', 'onClickLogOut');
        }
    </script>
    <!-- mmm Thank You Content (National) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Thank You Content (National) mmm -->
    <div class="thankyou_content_wrapper clearfix">
        <div class="thankyou_content clearfix send_file main_content">
            <div class="container_24">
                <div class="grid_24 clearfix">
                    <div class="grid_16 sf_col two_col col1">
                        <div class="prefix_1 grid_22 suffix_1 alpha">
                            <h2 class="header">
                                Thank You!</h2>
                            <h3> <CMS:ContentBlock ID="cbContentTitle" runat="server" DoInitFill="false" /></h3>
                            <p><CMS:ContentBlock ID="cbThankYouContent" runat="server" DoInitFill="false" /></p>                            
                            <div class="form" id="btnLogOutSection" runat="server" visible="false">  <%--cta-button-wrap--%>
                                <hr />
                                <asp:Button ID="log_out" CssClass="cta-button-text" runat="server" Text="LOG OUT" OnClientClick="javascript:DoPostForLogOut()" UseSubmitBehavior="true" />
                                <%--<a class="cta-button-text" href="#"><span>LOG OUT</span></a>--%>
                            </div>
                        </div>
                        <!-- end single_col -->
                    </div>
                    <!-- end sf_col 1 -->
                    <div class="grid_8 sf_col two_col col2">
                        <div class="prefix_2 grid_20 suffix_2" id="localCenterInfo" runat="server" visible="false">
                            <div id="sf_location_map" class="static_map">
                                <img alt="map" id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=336x206&amp;zoom=15&amp;markers=icon:http://author.sirspeedy.com/sandbox/sirspeedy/images/location-map-marker.png%7C33.976463,-118.036812&amp;style=feature:landscape%7Ccolor:0xe9e9e9&amp;style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&amp;sensor=true" />
                                    <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server" title="Our Location" href="https://www.google.com/maps/embed/v1/place?q=7240%20Greenleaf%20Ave%2C%20Whittier%2C%20CA%2090602%2C%20United%20States&key=AIzaSyDGF1KG6WSbJVdZ9TN66U3EMNA9wYIalFc&zoom=15">VIEW MAP</a>
                                <!-- end -->
                                <!-- go to google map page on devices smaller than 768px -->
                                    <a class="red-text" style="display:none" id="viewDirectionMobile" runat="server" href="https://www.google.com/maps?daddr=q=7240+Greenleaf+Ave+Whittier+CA+90602" target="_blank"><span>VIEW MAP</span></a>
                            </div>
                            <ul class="contact_info">
                                <li class="contact-icon-location"><span><asp:Literal ID="ltrCenterAddress" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <ul class="contact_info">
                                <li class="contact-icon-phone"><span><asp:Literal ID="ltrPhone" runat="server"></asp:Literal></span></li>
                                <li class="contact-icon-fax"><span><asp:Literal ID="ltrFax" runat="server"></asp:Literal></span></li>
                                <li class="contact-icon-email"><span><asp:Literal ID="ltrEmail" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <ul class="contact_info">
                                <li class="contact-icon-hours"><span>Hours<br />
                                    <asp:Literal ID="ltrWorkingHours" runat="server"></asp:Literal> </span></li>
                            </ul>
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
            </div>
            <!-- end container_24  -->
        </div>
        <!--end login_content -->
    </div>
    <!-- end login_content_wrapper -->
    <div class="clear">
    </div>    
</asp:Content>
