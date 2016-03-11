<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductsAndServicesCategoryInteriorMenu.ascx.cs" Inherits="UserControls_ProductsAndServicesCategoryInteriorMenu" %>

<!-- mmm Interior Menu mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Interior Menu mmm -->
<div class="sub_navigation_wrapper  clearfix">
    <div class="sub_navigation products-services-page">
        <div class="sub_navigation_products_services">
            <div class="menu-title-block col-height-equal">
                <!-- Products and Services -->
                <div id="about-products-services-h2">
                    <h2 id="menu-products-services">
                        All Products &amp; Services</h2>
                </div>
            </div>
            <div class="menu-items-block col-height-equal">
                <div id="products-services-desktop-nav">
                    <div class="top-menu-items">
                        <ul>
                            <asp:Repeater runat="server" ID="RptTopPSMenuItems">
                                <ItemTemplate>
                                    <li><a href="<%# Eval("url") %>"><span><%# Eval("title") %></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="bottom-menu-items">
                        <ul>
                            <asp:Repeater runat="server" ID="RptBottomPSMenuItems">
                                <ItemTemplate>
                                    <li class="products-services-link"><a href="<%# Eval("url") %>"><span><%# Eval("title") %></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>                            
                        </ul>
                    </div>
                    <div class="bottom-menu-items">
                        <ul>
                            <asp:Repeater runat="server" ID="RptBottomPSMenuItems2">
                                <ItemTemplate>
                                    <li class="products-services-link"><a href="<%# Eval("url") %>"><span><%# Eval("title") %></span></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <!--desktop nav-->
                <div class="lvl-2-title-wrap" id="mobile-nav-header">
                    <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                        &nbsp;</a>
                </div>
                <!-- end lvl-2-title-wrap -->
                <ul id="products-services-mobile-nav">
                    <li class="products-services-link hide"><a href="/Products-Services/" class="active">All Products &amp; Services</a></li>
                    <asp:Repeater runat="server" ID="RptPSCategoryMobileSubNav">
                        <ItemTemplate>
                            <li class="products-services-link"><a href="<%# Eval("url") %>"><%# Eval("title") %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <!-- products_services_menu-->
    </div>
    <!-- end sub_nav -->
</div>
<!-- end sub_nav_wrapper-->
<div class="clear">
</div>