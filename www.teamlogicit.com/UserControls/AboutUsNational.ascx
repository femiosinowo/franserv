<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AboutUsNational.ascx.cs" Inherits="UserControls_AboutUsNational" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<%@ Register Src="~/UserControls/TestimonialSlider.ascx" TagPrefix="ux" TagName="TestimonialSlider" %>

<!-- mmm About Us - Company Info mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About Us -Company Info mmm -->
<div class="about_us_company_wrapper  clearfix">
    <div class="about_us_company clearfix">
        <div class="container_24">
            <div class="grid_24">
                <asp:ListView ID="lvAboutUs" runat="server">
                    <LayoutTemplate>
                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <h2 class="headline"><%# Eval("Title") %></h2>
                        </h2>
                        <div class="grid_12">
                            <%# Eval("DespSection1") %>
                        </div>
                        <!--//.grid_11-->
                        <div class="grid_12">
                            <%# Eval("DespSection2") %>
                        </div>
                        <!--//.grid_11-->
                        <div class="clear"></div>
                        <div class="grid_24 CustomOrderedList">
                            <%# Eval("Description") %>
                        </div>
                        <div class="clear"></div>
                        <ux:TestimonialSlider ID="uxTestimonialSlider"  runat="server" />                    
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <!-- end grid_24 -->
        </div>
        <!--end grid_24-->
    </div>
    <!--end container_24-->
</div>
<!--end about_us_company -->
<!-- end about_us_company wrapper -->
<div class="clear">
</div>
<!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
<div class="were_local_wrapper clearfix">
    <div class="were_local clearfix">
        <div class="container_24">
            <h2 class="headline">Corporate Location</h2>
            <div class="grid_24">
                <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
                <script src="../js/location_map_black_marker.js" type="text/javascript"></script>
                <div class="location_wrapper">
                    <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                    <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                    <div id="location_map_black">
                        <!-- Google map container -->
                    </div>
                    <!-- #location_map_black -->
                </div>
                <!-- end your location wrapper -->
                <div class="were_local_section">
                    <ul class="local_contact_info about">
                        <asp:Literal ID="litLocAddress" runat="server" />
                        <asp:Literal ID="ltrPhoneNumber" runat="server" />
                        <asp:Literal ID="ltrEmailAddress" runat="server" />
                    </ul>
                </div>
                <!--were_local_section-->
            </div>
            <!-- grid 24-->
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- we're local-->
</div>
<!-- end your location wrapper -->
<div class="clear"></div>
<!-- mmm Start Franchise (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Start Franchise (nat) mmm -->
<div class="img_holder_wrapper footer_upper">
    <div class="img-holder-footer-upper" id="StartFranchise_img" runat="server" data-image="../images/noc_bkg.jpg" data-width="1600" data-height="580"></div>
    <div class="img_holder_content clearfix">
        <div class="container_24">
            <div class="grid_11 suffix_13">
                <CMS:ContentBlock ID="cbStartFranchise" runat="server" DoInitFill="false" />
                <!--caption-->
            </div>
            <!--//.grid_24-->
        </div>
        <!--//.container_24-->
    </div>
    <!--here_to_help_content-->
</div>
<!---img-holder-wrapper-->
<div class="clear"></div>

