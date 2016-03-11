<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductsAndServicesIndexFooterUpper.ascx.cs" Inherits="UserControls_ProductsAndServicesIndexFooterUpper" %>

 <div class="footer_upper_wrapper  clearfix">
        <div class="footer_upper clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div id="footer_upper_content">
                        <!-- Local site section -->
                        <div class="local_content">
                            <div class="content_wrapper">
                                <div class="grid_12 alpha col1">
                                    <div class="header clearfix">
                                        <img src="images/logo-marketing-tango.png" alt="Marketing Tango">
                                        <div class="cta-button-wrap">
                                            <a href="http://www.marketingtango.com/" class="cta-button-text"><span>Visit <span class="shorten-header">the</span>
                                                Blog</span></a>
                                        </div>
                                    </div>
                                    <!-- end .header -->
                                    <div class="content">
                                        <asp:Repeater runat="server" ID="uxBlogs">
                                            <ItemTemplate>
                                                <div class="item blog">
                                                    <div class="item-image">
                                                        <img src="<%# Eval("Image") %>" alt="<%# Eval("Title") %>"></div>
                                                    <!-- end .item-image -->
                                                    <div class="item-desc">
                                                        <h3>
                                                            <span class="post-date"><%# Eval("PostDate") %></span><%# Eval("Title") %></h3>
                                                        <p>
                                                            <%# Eval("Description") %></p>
                                                        <p>
                                                            <a target="_blank" href="<%# Eval("MoreLink") %>" class="more-link">More</a></p>
                                                    </div>
                                                    <!-- end .item-desc -->
                                                </div>
                                                <!-- end item -->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <!-- end .content -->
                                </div>
                                <!-- end .content-wrapper -->
                            </div>
                            <!--end grid 12-->
                            <div class="grid_12 omega col2">
                                <div class="content_wrapper">
                                    <div class="header clearfix">
                                        <h2 class="headline gray">
                                            Briefs &amp; Whitepapers</h2>
                                        <div class="cta-button-wrap">
                                            <a href="/Briefs-Whitepapers/" class="cta-button-text"><span>View All</span></a>
                                        </div>
                                    </div>
                                    <!-- end .header -->
                                    <div class="content gray-border-bottom">
                                        <asp:Repeater runat="server" ID="UxBriefWhitepapers">
                                            <ItemTemplate>
                                                <div class="item brief">
                                                    <div class="item-image">
                                                        <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>" width="127"></div>
                                                    <!-- end .item-image -->
                                                    <div class="item-desc">
                                                        <h3><%# Eval("title") %></h3>
                                                        <p><%# Eval("teaserMM")%></p>
                                                        <div class="cta-button-wrap">
                                                            <a href="<%# Eval("hrefText") %>" class="cta-button-text"><span>Download</span></a>
                                                        </div>
                                                        <!-- end .cta-button-wrap -->
                                                    </div>
                                                    <!-- end .item-desc -->
                                                </div>
                                                <!-- end item -->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <!-- end item -->
                                    </div>
                                    <!-- end .content -->
                                </div>
                                <!-- end .content-wrapper -->
                            </div>
                            <!--end grid 12-->
                        </div>
                        <!--  end .local_content -->
                    </div>
                    <!-- end #footer_upper_content -->
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end upperfooter -->
    </div>
