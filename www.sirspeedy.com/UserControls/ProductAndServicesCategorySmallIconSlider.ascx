<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAndServicesCategorySmallIconSlider.ascx.cs" Inherits="UserControls_ProductAndServicesCategoryIconSlider" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="ProductAndServicesCategorySmallIconSlider" %>

<div class="products_category_top_page_slider_wrapper clearfix">
      <div class="products_category_top_page_slider no-repeat-slides">
         <div class="slider-wrapper">
            <ul>
                <asp:Repeater runat="server" ID="UxPSHeaderSlider">
                    <ItemTemplate>
                        <li><a href="<%# Eval("url") %>">
                            <img src="<%# Eval("iconSmall") %>" alt="icon" />
                            <%# Eval("title") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div> <!-- end slider-wrapper -->
      </div><!-- end sub_nav -->
    </div><!-- end sub_nav_wrapper-->
    <div class="clear"></div>
    <!-- end sub_nav -->
</div>
<!-- end sub_nav_wrapper-->
<div class="clear">
</div>