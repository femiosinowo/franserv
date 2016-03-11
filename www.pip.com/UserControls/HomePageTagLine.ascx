<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageTagLine.ascx.cs" Inherits="UserControls_HomePageTagLine" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<!-- mmm Tagline (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline (lcl) mmm -->
<%--<div class="main_tagline_wrapper  clearfix">
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
<div class="clear"></div>--%>

<div id="tagLineContent" runat="server" class="main_quote_wrapper background_wrapper clearfix" data-image="../images/main_quote_bg.jpg">
    <div class="main_quote clearfix">
		<div class="main_quote_content">
		<div class="container_24">
	        <div class="grid_24">
	            <CMS:ContentBlock ID="cbTagLines" runat="server" DoInitFill="false" />
		    </div>
			</div>
		</div>
	</div><!-- end main_quote -->
</div><!-- end main_quote_wrapper  -->
    
<div class="clear"></div>
