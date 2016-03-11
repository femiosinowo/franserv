<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageBanners.ascx.cs" Inherits="UserControls_HomePageBanners" %>

<div class="clear"></div>
<div id="main" role="main" class="clearfix">
        <!-- Start Slider -->
        <section class="slider local_slider">
            <div class="flex-container">
                <!--IMAGES starts here-->
                <div class="flexslider local">
                    <asp:ListView ID="lvHeaderMenuItems" runat="server">
                        <LayoutTemplate>
                            <ul class="slides">
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>                                
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                            <img src="<%# Eval("ImagePath") %>" alt="" />
                            <div class="flex-caption caption">
                                <%# Eval("Title") %>                               
                                <%# Eval("SubTitle") %>
                            </div>
                        </li>
                        </ItemTemplate>
                    </asp:ListView>                    
                </div>
                <!--flex slider -->
            </div>
            <!-- flex container-->
        </section>
        <!-- End Slider -->    
    <!-- Custom Controls starts here-->
    <div class="flex-custom-control-nav-row">
        <asp:ListView ID="lvBannerTabs" runat="server">
            <LayoutTemplate>
                <ol class="flex-custom-control-nav">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </ol>
            </LayoutTemplate>
            <ItemTemplate>
                <li class="control_nav_<%# Container.DisplayIndex + 1 %>"><a class="<%# Eval("ClassName") %>"><span><%# Eval("TabTitle") %></span></a></li>
            </ItemTemplate>
        </asp:ListView>        
    </div>
    <!--custom control nav row-->
    <script type="text/javascript">
        $(window).load(function () {
            $('.flexslider').flexslider({
                controlsContainer: ".flex-container",
                manualControls: ".flex-custom-control-nav li a",
                start: function (slider) {
                    $('.flex-custom-control-nav li').click(function (event) {
                        event.preventDefault();
                        //slider.flexAnimate(slider.getTarget("next"));
                        var count = $(this).index();
                        slider.flexAnimate(count);
                        $(this).find('a').addClass('flex-active');
                    });
                }
            });
        });
    </script>
</div>
<!--main-->
<div class="clear"></div>
<div class="bottom_header clearfix">
    <div class="container_24">
        <asp:Literal ID="ltrSupplementOutSourcing" runat="server" />
    </div>
    <!--container 24-->
</div>
<!--bottom_header-->
