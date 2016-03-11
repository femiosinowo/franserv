<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageLocations.ascx.cs"
    Inherits="UserControls_HomePageLocations" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<div class="clear">
</div>
<!-- mmm OUR Location (local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm OUR Location (local) mmm -->
<div class="our_location_wrapper  clearfix">
    <div class="our_location clearfix">
        <section id="our_location">
            <div class="grid_6 headline-block headline-block-white col-height-equal">
                <div class="headline-content-outer">
                    <div class="headline-content-inner">
                        <span class="headline-block-icon-white"></span>
                        <h2 class="headline">Our<br />
                            Location</h2>
                        <asp:Literal ID="litLocAddress" runat="server" />
                        <asp:Literal ID="litLocContact" runat="server" />
                        <asp:Literal ID="ltrWorkingHours" runat="server" />
                        <a class="cta-button-text" href="#" target="_blank" id="directions_lb" runat="server">
                            <div class="cta-button-wrap white-btn">
                                <span>Directions</span>
                            </div>
                        </a>
                        <div class="location_social_media">
                            <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                        </div>
                    </div>
                    <!--headline content-->
                </div>
                <!--headline content-->
            </div>
            <div class="grid_18 our_location_content col-height-equal">
                <div id="location_map_wrapper" class="clearfix">
                    <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                    <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                    <div id="location_map">
                        <!-- Google map container -->
                    </div>
                    <!-- #location_map -->
                </div>
                <!-- end #location_map_wrapper -->
            </div>
            <!-- our_location_content -->
        </section>
    </div>
    <!-- end our_locations -->
</div>
<!-- end our_locations_wrapper  -->
<!-- end your location wrapper -->
<div class="clear">
</div>
