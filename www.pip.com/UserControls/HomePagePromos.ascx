<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePagePromos.ascx.cs" Inherits="UserControls_HomePagePromos" %>
<script type="text/javascript">
    function ActiveReqestAQuote() {        
        $('.quote_icon > a.utility_link').click();
    }
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $('.CallRAQ').click(function () {
            $('.utility_nav li').removeClass('active');
            $('.utility_content_wrapper').css('background-color', '#EFEFEF');
            $('#request_quote_national').show();
            $('.utility_raq').parent().addClass('active');
            var u_link = $('.utility_saf').attr('href');
            //var u_content = $('.utility_content');
            $('.utility_nav_wrapper').addClass('open');
            $('.close_utility_btn').show();
            //if (u_content.is(':visible')) { u_content.hide(); $(u_link).show(); }
            //else { $(u_link).slideDown('slow'); }
            if (u_link == '#social_media') { $('.utility_content_wrapper').css('background-color', '#1A9EFF'); }
            $('html, body').animate({ scrollTop: $(".site_container").offset().top }, 1000);
        });
    });
</script>

<div class="local_promo_wrapper  clearfix" id="centerPromosList" runat="server" visible="false">
    <div class="local_promo clearfix">
        <section id="local_promo">
            <div class="headline-block headline-block-white col-height-equal">
                <div class="headline-content-outer">
                    <div class="headline-content-inner">
                        <span class="headline-block-icon-white"></span>
                        <h2 class="headline">Your<br />
                            Local PIP</h2>
                        <h2 class="subheadline">
                            <asp:Literal ID="ltrAddress1" runat="server" />
                            <asp:Literal ID="ltrAddress2" runat="server" />
                        </h2>
                        <p>
                            <asp:Literal ID="ltrDecription" runat="server"></asp:Literal></p>
                    </div>
                    <!--headline content-->
                </div>
                <!--headline content-->
            </div>
            <div class="local_promo_content col-height-equal">
                <div class="container_24">
                    <div class="grid_24">                       
                        <div class="grid_12" runat="server" visible="false" id="promo_1">
                            <div class="promo image-left">
                                <asp:Literal ID="ltrPromoImage1" runat="server"></asp:Literal>
                            </div>
                            <!-- end .promo.image-left  -->
                            <div class="largePromoCTA">
                                <h3>Request a Quote</h3>
                                <a class="cta-button-text CallRAQ" href="javascript:void('0')">
                                    <div class="cta-button-wrap white-btn">
                                        <span>Tell us more</span>
                                    </div>
                                </a>
                            </div>
                            <div id="largePromoSecondCTA" runat="server" visible="false" class="largePromoCTA">
                                <h3>Get Started Today!</h3>
                                <a class="cta-button-text" href="/contact-us/">
                                    <div class="cta-button-wrap white-btn">
                                        <span>Contact Us</span>
                                    </div>
                                </a>
                            </div>                            
                        </div>
                        <!--grid 12 promo 1-->
                        <div class="grid_12" runat="server" visible="false" id="promo_2">
                            <div class="promo image-right">
                                <asp:Literal ID="ltrPromoImage2" runat="server"></asp:Literal>
                            </div>
                            <!-- end .promo.image-right  -->
                            <h3>Get Started Today!</h3>
                            <a class="cta-button-text" href="/contact-us/">
                                <div class="cta-button-wrap white-btn">
                                    <span>Contact Us</span>
                                </div>
                            </a>
                        </div>
                    </div>
                    <!--end grid 24-->
                </div>
                <!-- end container_24 -->
            </div>
        </section>
    </div>
    <!--end products_services -->
    <!--</div>end grid 24-->
    <!-- </div>end container_24 -->
    <!--</div> end products_services -->
</div>
<!--end products_services_wrapper -->
<div class="clear"></div>
