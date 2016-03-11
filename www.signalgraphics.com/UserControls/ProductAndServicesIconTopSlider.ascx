<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAndServicesIconTopSlider.ascx.cs"
    Inherits="UserControls_ProductAndServicesIconTopSlider" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="ProductAndServicesIconTopSlider" %>

<div class="products_services_top_page_slider_wrapper clearfix">
    <div class="products_services_top_page_slider no-repeat-slides">
        <!-- Top Slider -->
        <div class="slider-wrapper">
            <ul>
                <asp:Repeater runat="server" ID="UxPSHeaderSlider">
                    <ItemTemplate>
                        <li><a href="<%# Eval("hrefId") %>">
                            <img src="<%# Eval("iconLarge") %>" alt="icon" />
                            <%# Eval("title") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>        
    </div>
    <!-- end sub_nav -->
</div>
<!-- end sub_nav_wrapper-->
<div class="clear">
</div>
