<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="Contact_Us.aspx.cs" Inherits="Contact_Us" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="img-holder contact_us_map_wrapper" data-image="/images/parallax-transparent.png" data-image-mobile="/images/parallax-transparent.png" data-width="1600" data-height="900"></div>
    <div class="contact_us_map clearfix">
        <div class="contact_us_map_content">
            <div class="location_wrapper_contact">               
                <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                <input type="hidden" value="" id="hiddenCenterAddress" class="hiddenCenterAddress" runat="server" />     
                <script src="/js/my-location-contact2.js" type="text/javascript"></script>           
                <div id="location_map_contact">
                    <!-- Google map container -->
                </div>
                <!-- #location_map_contact -->
            </div>
            <!-- // location_wrapper_contact -->
        </div>
        <!--contact_us_map content-->
    </div>
    <!-- contact_us_map-->
    <div class="clear"></div>
    <div class="contact_us_form clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="contact_us_form_content">
                    <div class="grid_12">
                        <h2 class="headline">drop us a line</h2>
                        <asp:Label ID="lblError" CssClass="errorMessage" runat="server"></asp:Label>
                        <div class="form" id="contact_form">
                            <div class="grid_11">
                                <asp:TextBox ID="txtFirstName" required="required" runat="server" placeholder="First Name*" onblur="if(this.value == '') { this.value = 'First Name*'; }" onfocus="if(this.value == 'First Name*') { this.value = ''; }"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFirstName" ValidationGroup="ContactUs"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="grid_11 push_1">
                                <asp:TextBox ID="txtLastName" required="required" runat="server" placeholder="Last Name*" onblur="if(this.value == '') { this.value = 'Last Name*'; }" onfocus="if(this.value == 'Last Name*') { this.value = ''; }"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtLastName" ValidationGroup="ContactUs"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="grid_23">
                                <asp:TextBox ID="txtEmail" required="required" TextMode="Email" runat="server" placeholder="Email*" onblur="if(this.value == '') { this.value = 'Email*'; }" onfocus="if(this.value == 'Email*') { this.value = ''; }"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="txtEmail" ValidationGroup="ContactUs"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="grid_23">
                                <textarea id="contact_message" required="required" runat="server" placeholder="Message*" onblur="if(this.value == '') { this.value = 'Message*'; }" onfocus="if(this.value == 'Message*') { this.value = ''; }"></textarea>
                                <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="contact_message" ValidationGroup="ContactUs"
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </div>
                            <div class="grid_3">
                                <asp:Button ID="btnSubmit" UseSubmitBehavior="false" CssClass="contactUsBtn" runat="server" Text="Send" ValidationGroup="ContactUs" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="grid_10 push_2 contact_column">
                        <h2 class="headline">contact</h2>
                        <div class="grid_18">
                           <!-- <ul class="request_call_button">
                                <li><a class="request_call" href="#">Request a Call From Our Team</a></li>
                            </ul> -->
                            <ul class="local_contact_info">
                                <asp:Literal ID="litLocAddress" runat="server" />
                                <asp:Literal ID="ltrPhoneNumber" runat="server" />
                                <asp:Literal ID="ltrEmailAddress" runat="server" />
                            </ul>
                        </div>
                        <!--//grid_18-->
                        <div class="grid_14 social_media">
                            <asp:Literal ID="ltrSocialLinks" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <!--//contact_column-->
                </div>
                <!--contact_us_form content-->
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <div class="clear"></div>    
</asp:Content>

