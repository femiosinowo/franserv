<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageLocations.ascx.cs" Inherits="UserControls_HomePageLocations" %>

<div class="clear"></div>
<div class="your_location_wrapper  clearfix">
    <!--<div class="your_location clearfix">
    	  <div class="container_24">
    		  <div class="grid_24">-->
    <div class="your_location clearfix">
        <div id="location_info_map_wrapper" class="clearfix">
            <div id="location_info_wrapper">
                <div class="container_24">
                    <div id="location_info" class="clearfix">
                        <h2 class="headline"><span>Your Location</span></h2>
                        <hr />
                        <div class="grid_11 alpha" id="location_address_hours">
                            <a class="logo" href="#">
                                <p class="visuallyhidden">Sir Speedy</p>
                                <img alt="Sir Speedy" src="/images/logo-infobox.png"/>
                            </a>
                            <asp:Literal ID="litLocAddress" runat="server"/>
                            <asp:Literal ID="ltrWorkingHours" runat="server" />
                        </div>
                        <!-- end .col 1-->
                        <div class="grid_12 omega" id="location_contact_info">
                            <asp:Literal id="litLocContact" runat="server"/>                           
                        </div>
                        <!-- end .col 2-->
                        <div class="clear"></div>
                        <hr />
                        <div id="location_footer">
                            <div class="grid_18 alpha">
                                <asp:Literal ID="ltrSocialIcons" runat="server"></asp:Literal>                                
                            </div>
                            <!-- end grid -->
                            <div class="grid_6 omega">
                                <div class="cta-button-wrap purple">
                                    <a class="cta-button-text fancybox" runat="server" href="https://www.google.com/maps?daddr=q=" target="_blank" rel="group" id="directions_lb"><span>DIRECTIONS</span></a>
                                </div>
                                <!--lvl-2-title-wrap -->
                            </div>
                            <!-- end #location_footer -->
                        </div>
                        <!-- end container_24 -->
                    </div>
                    <!-- end #location_info -->
                </div>
                <!-- end #location_infobox_wrapper -->
            </div>
            <!-- end location_info_wrapper -->
            <div id="location_map_wrapper" class="clearfix">
                <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />                
                <div id="location_map"></div>
            <!-- end #location_map_wrapper -->
        </div>
        <!-- end #location_info_map_wrapper -->
    </div>
    <!-- end your location -->
    <!--</div>  end grid_24-->
    <!--</div> end container_24-->
    <!--</div> end your location -->
</div>
<!-- end your location wrapper -->
<div class="clear"></div>
    </div>
