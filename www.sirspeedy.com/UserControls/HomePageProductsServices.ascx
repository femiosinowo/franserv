<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageProductsServices.ascx.cs" Inherits="UserControls_HomePageProductsServices" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="HomePageProductsServices" %>

<!-- mmm Products and Services (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Products and Services (both) mmm -->
<div class="products_services_wrapper  clearfix">
    <div class="products_services clearfix">
        <div class="products_services">
            <h2 class="headline">Our Products &amp; Services</h2>
            <div class="slider-wrapper">
                <ul>
                    <asp:Repeater runat="server" ID="UxProdAndServicesSlider">
                        <ItemTemplate>
                            <li><a href="<%# Eval("hrefText") %>">
                                <img src="<%# Eval("imageSliderSRC") %>" alt="Products and Services"><h3>
                                    <%# Eval("title") %></h3>
                                <span>
                                    <%# Eval("subtitle") %></span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!-- #products-slider-wrapper -->
            <div class="cta-button-wrap purple">
                <a class="cta-button-text" href="/product-services/"><span>All Products &amp; Services</span></a>
            </div>
        </div>
    </div>
    <!-- end main_rotator products_services -->
</div>
<!--end main_rotator_wrapper products_services_wrapper-->
<div class="clear"></div>
