<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterNav.ascx.cs" Inherits="UserControls_FooterNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SubscribeLocal.ascx" TagPrefix="ux" TagName="SubscribeLocal" %>
<div class="footer_upper_wrapper clearfix">
    <div class="footer_upper clearfix">
        <div class="center_footer_wrapper home_wrapper clearfix">
            <div class="footer">
                <div class="grid_3 col-height-equal" id="footer_section_wrapper">
                    <div class="col content" id="footer_section_4">
                        <asp:Panel ID="pnlLocal" runat="server" Visible="false">
                            <div class="footer_help cta">
                                <CMS:ContentBlock ID="cbHaveQuestion" runat="server" DoInitFill="false" />                                
								 <asp:Literal ID="ltrPhoneNUmber" runat="server"></asp:Literal>
                                <!--cta-button-wrap -->
                            </div>
                            <div class="footer_subscribe cta">
                                <CMS:ContentBlock ID="cbSubscribeNow" runat="server" DoInitFill="false" />
                                <div class="cta-button-wrap white-btn">
                                    <a href="#subscribe" id="subscribe_lb" class="cta-button-text white-btn"><span>SUBSCRIBE NOW</span></a>
                                </div>
                                <!--cta-button-wrap -->
                            </div>
                            <!-- mmmmmmmmmmmm SUBSCRIBE (LOCAL) mmmmmmmmmmmmmm -->
                            <div class="utility_content local" id="subscribe" style="display: none;">
                                <ux:SubscribeLocal ID="uxSubscribe1" runat="server" />
                            </div>
                            <!-- end subscribe -->
                        </asp:Panel>
                        <asp:Panel ID="pnlNational" runat="server">
                            <div class="footer_more_1 cta">
                                <CMS:ContentBlock ID="cbFindLocation" runat="server" DoInitFill="false" />
                                <!--cta-button-wrap -->
                            </div>
                            <div class="footer_more_2 cta">
                                <CMS:ContentBlock ID="cbFransOpport" runat="server" DoInitFill="false" />
                                <!--cta-button-wrap -->
                            </div>
                        </asp:Panel>
                    </div>
                    <!-- #footer_section_4 -->
                </div>
                <!-- #footer_section_wrapper -->
                <div class="content grid_21 col-height-equal" id="col-group-2">
                    <!-- section 1 -->
                    <div id="footer_section_1" class="col">
                        <h4>
                            <a href="/product-services/">Products & Services</a></h4>
                        <asp:ListView ID="lvfooterProductsServices" runat="server">
                            <LayoutTemplate>
                                <ul id="products">
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                </ul>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <%--<li><a href="/product_services_subcategory.aspx?taxId=<%# Eval("Id") %>"><%# Eval("Name") %></a></li>--%>
                                <li><a href="<%# Eval("url") %>">
                                    <%# Eval("title") %></a></li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- #footer_section_1 -->
                    <!-- section 2 -->
                    <div id="footer_section_2" class="col">
                        <div class="top_footer_section_2">
                            <!-- secondary navigation -->
                            <asp:Literal ID="ltrSubNav" runat="server"></asp:Literal>
                            <!--top_footer_section_2_col -->
                        </div>
                        <!--top footer section 2-->
                    </div>
                    <!-- footer section 2-->                    
                <!-- section 3 -->
                <div class="bottom_footer_section_2">
                    <div class="clearfix" id="social-networks">
                        <asp:Literal ID="ltrSocialIcons" runat="server"></asp:Literal>
                    </div>
                    <div id="twitter-feed">
                        <div id="twitter">
                            <a class="twitter-timeline" id="twitterWidget" runat="server" href="https://twitter.com/pipcorp"
                                data-chrome="nofooter transparent noheader noscrollbar noborders" data-tweet-limit="1"
                                data-link-color="#FFFFFF" data-theme="light" data-widget-id="619545467509059586">
                                Tweets by @pipcorp</a>
                        <!--[if gt IE 8]><!-->
                            <script type="text/javascript">!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");</script>
                        <!--<![endif]-->
                        </div>
                        <!-- #twitter -->
                    </div>
                    <!-- .twitter-timeline -->
                </div>
                <!-- end bottom_footer_section_2 -->
            </div>
            <!-- col-group-2 -->
        </div>
        <!--end footer_section_wrapper-->
         </div>
                <!-- #footer_section_2 -->
        <!-- .center-footer-wrapper -->
    </div>
    <!--end grid 24-->
</div>
