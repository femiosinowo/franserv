<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Shop" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="clear"></div>
    <div class="site_container local" id="shop">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
      <div class="header_image_content">
              <div class="header_image">
				  <%--<img src="images/headers/header_1.jpg" alt="">--%>
                  <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
			   </div><!-- header image-->
      </div><!-- end header_image_content -->
    </div><!-- end header_image_wrapper-->
    <div class="clear"></div>
     <!-- mmm Shop Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Shop Content mmm -->
    <div class="shop_content_wrapper local_about_us clearfix">
    	<div class="shop_content clearfix">
            <div id="shop_content" class="container_24">
                <div class="grid_6 headline-block col-height-equal">
		           <div class="int-headline-block headline-block-white int-block-1">
			           <div class="headline-content-outer">
			                <div class="headline-content-inner">
					            <span class="headline-block-icon-white"></span>
				                <%--<h2 class="headline">Shop Online</h2>
				                <p>At PIP we provide printing and marketing services designed to help customers meet their business growth objectives.</p>--%>
			                    <CMS:ContentBlock ID="cbShopSideContent" runat="server" />
                           </div><!--headline content-->
			           </div><!--headline content-->
			         </div>
		        </div><!--end grid_6-->
                <div class="grid_18 content-block col-height-equal">
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
                                    <div class="cta-button-wrap black-btn">
                                        <a href="<%# Eval("Link") %>" target="_blank" class="cta-button-text"><span>Shop Now</span></a>
                                    </div>
                                    <!-- end cta-button-wrap -->
                                </div>
                                <!-- end shop_category clearfix -->
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div> <!-- end grid 18 -->

            </div> <!-- end container_24 -->
        </div><!-- end shop_content-->
    </div> <!-- end shop_content_wrapper -->
</div>
<!-- end site_container -->
</asp:Content>

