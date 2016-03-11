<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePagePromos.ascx.cs" Inherits="UserControls_HomePagePromos" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="HomePagePromos" %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#reqAQuoteLink').click(function () {
            $('.quote_icon > a.utility_link').click();
        });
    });
</script>

<div class="red_promos_wrapper  clearfix" id="centerPromosList" runat="server" visible="false">
    <!--<div class="red_promos clearfix">
    	  <div class="container_24">
    		  <div class="grid_24"> -->
    <div class="red_promos clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="grid_14 alpha">
                    <!--<img src="/images/promo1.jpg" alt="promo"/>  
                      <img src="/images/promo2.jpg" alt="promo"/>-->  
                    <!-- LARGE IMAGE 
                    <img src="/images/promo3.jpg" alt="promo"/>-->
                    <asp:Literal ID="promoImages" runat="server"></asp:Literal>
                </div>
                <!-- end .alpha -->
                <div class="grid_10 omega">
                    <h2 class="headline red_border_bottom"><asp:Literal ID="lblCenterName" runat="server"></asp:Literal>&nbsp;<span><asp:Literal ID="lblAddress" runat="server"></asp:Literal></span>
                    </h2>
                    <div class="promos_text red_border_bottom">
                        <p><asp:Literal ID="ltrDecription" runat="server"></asp:Literal></p>
                    </div>
                    <!-- .promos_text -->
                    <div id="promos-contact">
                        <ul>
                            <li class="promos-icon-phone">
                                <h4>Call Us Today! <span><asp:Literal ID="ltrContactNumber" runat="server"></asp:Literal></span></h4>
                            </li>
                            <li class="promos-icon-quote">
                                <a id="reqAQuoteLink" href="#"><h4>Request a Quote <span>Tell Us More</span></h4></a>
                            </li>
                        </ul>
                    </div>
                    <!-- end promos-contact -->
                </div>
                <!-- end .omega -->
            </div>
            <!--end grid 24-->
        </div>
        <!-- end container_24 -->
    </div>
    <!--end products_services -->
    <!--</div>end grid 24-->
    <!-- </div>end container_24 -->
    <!--</div> end products_services -->
</div>
<!--end products_services_wrapper -->
<div class="clear"></div>
