<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductAndServicesCategoryMobileSubNav.ascx.cs" Inherits="UserControls_ProductAndServicesCategorySubNav" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="ProductAndServicesCategoryMobileSubNav" %>

<!-- mmm Sub Nav (Mobile Only) mmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (Mobile only) mmm -->
    <div class="sub_navigation_wrapper printing_copying clearfix">
        <div class="sub_navigation">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Printing & Copying page -->
                    <ul id="printing-copying-mobile-nav">
                        <asp:Repeater runat="server" ID="RptPSCategoryMobileSubNav">
                            <ItemTemplate>
                                <li><a href="<%# Eval("url") %>"><%# Eval("title") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>