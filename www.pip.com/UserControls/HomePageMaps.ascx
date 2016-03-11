<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageMaps.ascx.cs"
    Inherits="UserControls_HomePageMaps" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<div class="main_find_location_wrapper  clearfix">
    <div class="main_find_location clearfix">
        <div id="main_find_location"> <!-- section -->
	<div class="container_24">
	<div class="grid_3 headline-block headline-block-white col-height-equal">
	  <div class="headline-content-outer">
	    <div class="headline-content-inner">
			<span class="headline-block-icon-white"></span>
			<h2 class="headline">Find A<br/>Location</h2>
			 <a class="cta-button-text" href="/Find-Locator/#all_locations">
				<div class="cta-button-wrap white-btn">
				  <span>View All Locations</span>
				</div>
			   </a>
		   </div><!--headline content-->
	   </div><!--headline content-->
	</div>
        <div class="grid_21 main_find_location_content col-height-equal">
            <div class="form" id="main_find_location_form">
                <asp:TextBox ID="txtAddress" runat="server" CssClass="location" placeholder="City, State, Zip*"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAddress" ForeColor="Red"
                    ValidationGroup="HomePageFindLocation">*</asp:RequiredFieldValidator>
                <asp:DropDownList CssClass="custom-select" ID="ddlDistance" runat="server">
                    <asp:ListItem Selected="True" Text="- Choose Distance -" Value="0"></asp:ListItem>
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                    <asp:ListItem Text="25" Value="25"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                    <asp:ListItem Text="100" Value="100"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlDistance" ForeColor="Red"
                    ValidationGroup="HomePageFindLocation" InitialValue="0">*</asp:RequiredFieldValidator>
                <asp:Button ID="btnFindLocation" CssClass="orange_btn search btnHomePageFindLocation" ValidationGroup="HomePageFindLocation"
                    runat="server" Text="Search" OnClick="btnFindLocation_Click" />
            </div>
            <div class="find_united_kingdom">
                <a href="http://www.pipprinting.co.uk/" target="_blank">
                    <img src="/images/united-kingdom-icon.png" alt="United Kingdom">
                    <span>Visit us in the United Kingdom</span>
                </a>
            </div>
        </div>
	</div><!-- container 24-->
</div> <!-- section -->
    </div>
    <!-- end find_locations -->
</div>
<!-- end find_locations_wrapper  -->
<div class="clear">
</div>
