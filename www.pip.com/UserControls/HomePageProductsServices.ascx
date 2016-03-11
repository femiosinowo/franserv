<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageProductsServices.ascx.cs"
    Inherits="UserControls_HomePageProductsServices" %>
<!-- mmm Products and Services (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Products and Services (both) mmm -->
<div class="products_services_wrapper  clearfix">
    <div class="products_services clearfix">
        <div class="products_services">
            <div class="container_24">
                <div class="grid_3 headline-block headline-block-black col-height-equal">
                    <div class="headline-content-outer">
                        <div class="headline-content-inner">
                            <span class="headline-block-icon-black"></span>
                            <h2 class="headline">Products<br />
                                &amp; Services</h2>
                            <a class="cta-button-text" href="/Product-Services/">
                                <div class="cta-button-wrap black-btn">
                                    <span>View All<br />
                                        Products &amp; Services</span>
                                </div>
                            </a>
                        </div>
                        <!--headline content-->
                    </div>
                    <!--headline content-->
                </div>
                <div class="grid_21 slider-wrapper col-height-equal">
                    <ul>
                        <asp:Repeater runat="server" ID="UxProdAndServicesSlider">
                            <ItemTemplate>
                                <li><a href="<%# Eval("hrefText") %>">
                                    <div class="img-wrap">
                                        <img src="<%# Eval("imageSliderSRC") %>" alt="Products and Services">
                                        <div class="img-desc">
                                            <h3><%# Eval("title") %></h3>
                                            <p><%# Eval("subtitle") %></p>
                                        </div>
                                    </div>
                                </a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
        <!-- end main_rotator products_services -->
    </div>
    <!--end main_rotator_wrapper products_services_wrapper-->
</div>
<div class="clear"></div>
