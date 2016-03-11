<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageTagLine.ascx.cs" Inherits="UserControls_HomePageTagLine" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<!-- mmm Tagline (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline (lcl) mmm -->
<div class="main_tagline_wrapper  clearfix">
    <div class="main_tagline">
        <div class="container_24">
            <div class="prefix_1 grid_22 suffix_1">
                <div class="statement-text">
                    <CMS:ContentBlock ID="cbTagLines" runat="server" DoInitFill="false" />
                </div>
                <!-- end statement-text" -->
            </div>
            <!--end refix_1 grid_22 suffix_1 -->
        </div>
        <!-- end container_24 -->
    </div>
    <!-- end main_tagline -->
</div>
<!-- end main_tagline_wrapper -->
<div class="clear"></div>
