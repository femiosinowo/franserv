<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Shop" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="clear"></div>
    <div class="subpage_tagline_wrapper shop clearfix">
        <div class="subpage_tagline shop">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Local - Shop -->
                    <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <div class="clear"></div>
    <!-- mmm Shop Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Shop Content mmm -->
    <div class="shop_content_wrapper  clearfix">
        <div class="shop_content clearfix main_content">
            <div class="container_24">
                <div class="grid_24">
                    <asp:ListView ID="lvShpos" GroupItemCount="3" runat="server">
                        <GroupTemplate>                          
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            <div class="clear"></div>
                        </GroupTemplate>
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>                            
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="grid_8 shop_category_wrapper">
                                <div class="shop_category clearfix">
                                    <h3><span><%# Eval("Title") %></span></h3>
                                    <div class="shop_img">
                                        <img src="<%# Eval("Image") %>" alt="placeholder" />
                                    </div>
                                    <!-- end shop_img -->
                                    <p><%# Eval("Teaser") %></p>
                                    <div class="cta-button-wrap purple">
                                        <a href="<%# Eval("Link") %>" target="_blank" class="cta-button-text"><span>Shop Now</span></a>
                                    </div>
                                    <!-- end cta-button-wrap -->
                                </div>
                                <!-- end shop_category clearfix -->
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end shop_content -->
    </div>
    <!-- end shop_content_wrapper -->
</asp:Content>

