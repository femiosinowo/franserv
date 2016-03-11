<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterNav.ascx.cs" Inherits="UserControls_FooterNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>
<%@ Register Src="~/UserControls/SubscribeLocal.ascx" TagPrefix="ux" TagName="SubscribeLocal" %>


<div class="footer_lower_wrapper  clearfix">
    <div class="footer_lower clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="center_footer_wrapper home_wrapper clearfix">
                    <div class="footer">
                        <!-- section 4 -->
                        <div id="footer_section_wrapper">
                            <div id="footer_section_4" class="col content">
                                <asp:Panel ID="pnlLocal" runat="server" Visible="false">
                                    <div class="footer_help cta">
                                        <CMS:ContentBlock ID="cbHaveQuestion" runat="server" DoInitFill="false" />
                                        <asp:Literal ID="ltrPhoneNUmber" runat="server"></asp:Literal>
                                        <!--cta-button-wrap -->
                                    </div>
                                    <div class="footer_subscribe cta">
                                        <CMS:ContentBlock ID="cbSubscribeNow" runat="server" DoInitFill="false" />
                                        <div class="cta-button-wrap purple">
                                            <a href="#subscribe" id="subscribe_lb" class="cta-button-text"><span>SUBSCRIBE NOW</span></a>
                                        </div>                                         
                                        <!--cta-button-wrap -->
                                    </div>
                                    <!-- mmmmmmmmmmmm SUBSCRIBE (LOCAL) mmmmmmmmmmmmmm -->  
                                    <div class="utility_content local" id="subscribe" style=" display:none;">
                                        <ux:SubscribeLocal ID="uxSubscribe1" runat="server" />
                                    </div>                                  
                                    <!-- end subscribe -->
                                </asp:Panel>
                                <asp:Panel ID="pnlNational" runat="server">
                                    <div class="footer_help cta">
                                        <CMS:ContentBlock ID="cbFindLocation" runat="server" DoInitFill="false" />
                                        <!--cta-button-wrap -->
                                    </div>
                                    <div class="footer_subscribe cta">
                                        <CMS:ContentBlock ID="cbFransOpport" runat="server" DoInitFill="false" />
                                        <!--cta-button-wrap -->
                                    </div>
                                </asp:Panel>                                
                            </div>
                            <!-- #footer_section_4 -->
                        </div>
                        <!-- #footer_section_wrapper -->

                        <div class="content" id="col-group-2">
                            <!-- section 1 -->
                            <div id="footer_section_1" class="col">
                                <h4><a href="/product-services/">
                                Products & Services</a></h4>
                                <asp:ListView ID="lvfooterProductsServices" runat="server">
                                    <LayoutTemplate>
                                        <ul id="products">
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <%--<li><a href="/product_services_subcategory.aspx?taxId=<%# Eval("Id") %>"><%# Eval("Name") %></a></li>--%>
                                        <li><a href="<%# Eval("url") %>"><%# Eval("title") %></a></li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <!-- #footer_section_1 -->
                            <!-- section 2 -->
                            <div id="footer_section_2" class="col">
                                <!-- secondary navigation -->
                                <asp:ListView ID="lvFooterSecondaryNav" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <h4>
                                          <%# FormatLink(Eval("Href").ToString(), Eval("Text").ToString(), Eval("Description").ToString()) %></h4>                                           
                                        <asp:ListView ID="listViewSecondLevel" DataSource='<%# Eval("Items")%>' runat="server">
                                            <LayoutTemplate>
                                                <ul>
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li><%# FormatLink(Eval("Href").ToString(), Eval("Text").ToString(), Eval("Description").ToString()) %></li>                                               
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <!-- #footer_section_2 -->
                            <!-- section 3 -->
                            <div id="footer_section_3" class="col">
                                <div class="clearfix" id="social-networks">
                                    <h4>Follow Us</h4>
                                    <ux:SocialIcons ID="uxSocialIcons" runat="server" />                                    
                                </div>
                                <div id="twitter-feed">
                                    <h4>Latest Tweets</h4>
                                    <div id="twitter">
                                        <a class="twitter-timeline" id="twitterWidget" runat="server" href="https://twitter.com/signalgraphics" data-chrome="nofooter transparent noheader noscrollbar" data-tweet-limit="2" data-link-color="#FEF137" data-widget-id="568854850364923904">Tweets by @Signal Graphics</a>
                                        <script type="text/javascript">!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>
                                    </div>
                                    <!-- #twitter -->
                                </div>
                                <!-- .twitter-timeline -->
                            </div>
                            <!-- #footer_section_3 -->
                        </div>
                        <!-- .container-wrapper -->

                    </div>
                </div>
                <!--end footer_section_wrapper-->
            </div>
            <!-- .footer -->

        </div>
        <!-- .center-footer-wrapper -->

    </div>
    <!--end grid 24-->
</div>
