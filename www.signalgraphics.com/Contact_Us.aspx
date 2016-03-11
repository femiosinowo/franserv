<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Contact_Us.aspx.cs" Inherits="Contact_Us" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Contact Us Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Contact Us Content mmm -->
    <div class="contact_us_content_wrapper  clearfix">
        <div class="contact_us_content main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_14" id="contact_col_1">
                        <div class="contact_col">
                            <h2>Drop Us A Line</h2>
                            <asp:Label ID="lblError" CssClass="errorMessage" runat="server"></asp:Label>
                            <div class="form" id="contact_form">
                                <asp:TextBox ID="txtFirstName" required="required" runat="server" placeholder="First Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFirstName" ValidationGroup="ContactUs"
                                    ForeColor="Red" >*</asp:RequiredFieldValidator>                               
                                <asp:TextBox ID="txtLastName" required="required" runat="server" placeholder="Last Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtLastName" ValidationGroup="ContactUs"
                                    ForeColor="Red" >*</asp:RequiredFieldValidator> 
                                <div class="clear"></div>
                                <asp:TextBox ID="txtEmail" required="required" TextMode="Email" runat="server" placeholder="Email Address"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtEmail" ValidationGroup="ContactUs"
                                    ForeColor="Red" >*</asp:RequiredFieldValidator>
                                <div class="clear"></div>
                                <textarea id="contact_message" required="required" runat="server" placeholder="Your Message"></textarea>
                                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="contact_message" ValidationGroup="ContactUs"
                                    ForeColor="Red" >*</asp:RequiredFieldValidator>
                                <div class="clear"></div>
                                <asp:Button ID="btnSubmit" runat="server" Text="Send" ValidationGroup="ContactUs" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <!-- end contact_col1 -->
                    </div>
                    <!-- end #contact_col_1 -->
                    <div class="grid_9 suffix_1" id="contact_col_2">
                        <div class="contact_col">
                            <h2>Contact Info</h2>
                            <ul class="contact_info">
                                <li class="contact-icon-phone"><span><asp:Literal ID="ltrPhone" runat="server" /></span></li>
                                <asp:Literal ID="ltrFax" runat="server" />
                                <li class="contact-icon-email"><span><asp:Literal ID="ltrEmail" runat="server" /></span></li>
                                <li class="contact-icon-location">
                                    <asp:Literal ID="ltrAddress" runat="server" />
                                    <!--
                                        <div class="cta-button-wrap gold small">
                                              <a class="cta-button-text" href="#"><span>Map</span></a>
                                          </div> 
                                      -->
                                    <div class="cta-button-wrap gold small view-map">
                                        <a class="cta-button-text view-map fancybox iframe" id="viewDirectionDesktop" runat="server" title="Your Location" href="#"><span>Map</span></a>
                                    </div>
                                    <!-- end -->
                                    <!-- go to google map page on devices smaller than 768px -->
                                    <div class="cta-button-wrap gold small view-map-mobile">
                                        <a class="cta-button-text" id="viewDirectionMobile" runat="server" href="#" target="_blank"><span>Map</span></a>
                                    </div>
                                </li>
                                <li class="contact-icon-hours"><span>Hours<br />
                                   <asp:Literal ID="ltrWorkingHours" runat="server" />
                                  </span>
                                </li>
                            </ul>
                            <div id="contact_social_icons">
                                <hr />
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
    <div class="clear"></div>
</asp:Content>

