<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="Contact_Us.aspx.cs" Inherits="Contact_Us" %>

<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="site_container local" id="contact_us">
        <!-- mmm Contact Us Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Contact Us Content mmm -->
        <div class="contact_us_content_wrapper local_about_us clearfix">
            <div class="contact_us_content clearfix">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="grid_13 prefix_1" id="contact_col_1">
                            <div class="contact_col col-height-equal">
                                <h2>
                                    Drop Us A Line</h2>
                                <asp:Label ID="lblError" CssClass="errorMessage" runat="server"></asp:Label>
                                <div class="form" id="contact_form">
                                    <asp:TextBox ID="txtFirstName" required="required" runat="server" placeholder="First Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFirstName"
                                        ValidationGroup="ContactUs" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtLastName" required="required" runat="server" placeholder="Last Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtLastName"
                                        ValidationGroup="ContactUs" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="clear">
                                    </div>
                                    <asp:TextBox ID="txtEmail" required="required" TextMode="Email" runat="server" placeholder="Email Address"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtEmail"
                                        ValidationGroup="ContactUs" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="clear">
                                    </div>
                                    <textarea id="contact_message" required="required" runat="server" placeholder="Your Message"></textarea>
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="contact_message"
                                        ValidationGroup="ContactUs" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="clear">
                                    </div>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Send" ValidationGroup="ContactUs" CssClass="black_btn"
                                        OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                            <!-- end contact_col1 -->
                        </div>
                        <!-- end #contact_col_1 -->
                        <div class="grid_9 suffix_1" id="contact_col_2">
                            <div class="contact_col col-height-equal">
                                <h2>
                                    Contact Info</h2>
                                <div class="contact_location_img" id="sf_location_map">
                                    <img alt="map" id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=336x206&amp;zoom=15&amp;markers=icon:http://author.sirspeedy.com/sandbox/sirspeedy/images/location-map-marker.png%7C33.976463,-118.036812&amp;style=feature:landscape%7Ccolor:0xe9e9e9&amp;style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&amp;sensor=true" />
                                <%--<div class="cta-button-wrap gold small view-map">
                                    <a class="cta-button-text view-map fancybox iframe" id="viewDirectionDesktop" runat="server"
                                        title="Your Location" href="#"><span>Map</span></a>
                                </div>--%>
                                <!-- end -->
                                <!-- go to google map page on devices smaller than 768px -->
                                <%--<div class="cta-button-wrap gold small view-map-mobile">
                                    <a class="cta-button-text" id="viewDirectionMobile" runat="server" href="#" target="_blank">
                                        <span>Map</span></a>
                                </div>--%>
                                
                                    <%--<img src="http://maps.googleapis.com/maps/api/staticmap?center=33.976303,-118.037004&zoom=15&size=502x186&markers=icon:http://goo.gl/3NrQ1C|33.976303,-118.037004&sensor=false&key=AIzaSyA3TlW7oOcFVQwkJwL9kHxc6wyd2IdBrQ0" alt="contact location">--%>
                                </div>
                                <div class="contact_location_info location_info">
                                    <ul>
                                        <li id="location_address">
                                            <asp:Literal ID="ltrAddress1" runat="server" /></li>
                                        <li id="location_address2">
                                            <asp:Literal ID="ltrAddress2" runat="server" /></li>
                                        <li id="location_phone">
                                            <asp:Literal ID="ltrPhone" runat="server" /></li>
                                        <li id="location_phone2">
                                            <asp:Literal ID="ltrFax" runat="server" /></li>
                                        <li id="location_email">
                                            <asp:Literal ID="ltrEmail" runat="server" /></li>
                                    </ul>
                                    <hr />
                                    <ul>
                                        <asp:Literal ID="ltrWorkingHours" runat="server" />
                                    </ul>
                                    <hr />
                                </div>
                                <div id="contact_social_icons">
                                    <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                                </div>
                                <!-- contact_social_icons -->
                            </div>
                            <!-- end .contact_col -->
                        </div>
                        <!-- end contact_col1 -->
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24  -->
            </div>
            <!--end contact_us_content -->
        </div>
        <!-- end contact_us_content_wrapper -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
