<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageMaps.ascx.cs" Inherits="UserControls_HomePageMaps" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<div class="ww_locations_wrapper  clearfix">
        <div class="ww_locations clearfix">
            <!--<div class="container_24">
      		  <div class="grid_24"> -->
            <div id="home-locations">
                <%--<div id="locations-wrapper">
                    <div class="maps-wrapper">
                        <div class="container_24">
                            <div class="grid_24">
                                <CMS:ContentBlock ID="cbWorlMaps" runat="server" DoInitFill="false" />
                                <!-- .maps-inner -->
                            </div>
                            <!-- .grid_24 -->
                        </div>
                        <!-- .container_24-->
                        <div class="maps-wrapper-arrow">
                        </div>
                    </div>
                    <!-- .map-wrapper -->
                </div>--%>
                <!-- #locations-wrapper -->
                <div class="statement-wrapper">
                    <div class="container_24">
                        <div class="grid_24">
                            <CMS:ContentBlock ID="cbMapsDescp" runat="server" DoInitFill="false" />
                            <!-- .statement-text -->
                        </div>
                        <!-- .container_24 -->
                    </div>
                    <!-- .grid_24 -->
                </div>
                <!-- .statement-wrapper -->
            </div>
            <!--</div>-->
            <!--end grid_24-->
            <!--</div>-->
            <!--end container_24-->
        </div>
        <!-- end ww_locations -->
    </div>
    <!-- end ww_locations_wrapper  -->
    <div class="clear"></div>