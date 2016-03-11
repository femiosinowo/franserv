<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAndServicesCategoryIconSlider.ascx.cs" Inherits="UserControls_ProductAndServicesCategoryIconSlider" %>

<div class="products_services_top_page_slider_wrapper clearfix">
    <div class="products_services_top_page_slider no-repeat-slides">
        <!-- Top Slider -->
        <div class="slider-wrapper">
            <ul>
                <asp:Repeater runat="server" ID="UxPSHeaderSlider">
                    <ItemTemplate>
                        <li><a href="<%# Eval("url") %>">
                            <img src="<%# Eval("iconLarge") %>" alt="icon" />
                            <%# Eval("title") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!-- end slider-wrapper -->
        <div class="cta-button-wrap purple">
            <a class="cta-button-text" href="#"><span>Show All</span></a>
        </div>
    </div>
    <!-- end sub_nav -->
</div>
<!-- end sub_nav_wrapper-->
<div class="clear">
</div