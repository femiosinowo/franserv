<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageFransFooter.ascx.cs" Inherits="UserControls_HomePageFransFooter" %>

<!-- mmm  Briefs & Whitepapers mmmmmmmmmmmmmmmmmmm   Briefs & Whitepapers mmm -->
<div class="briefs_wps_section_wrapper  clearfix">
    <div class="briefs_wps_section clearfix">
        <section id="briefs_wps_section">
            <div class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">Briefs &amp;<br />
                                    Whitepapers</h2>
                                <a class="cta-button-text" href="/Briefs-Whitepapers/">
                                    <div class="cta-button-wrap white-btn">
                                        <span>View All<br />
                                            Briefs &amp; Whitepapers</span>
                                    </div>
                                </a>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block briefs_wps_section_content col-height-equal">
                    <asp:ListView ID="lvBriefsWhitePapers" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div class="item brief">
                                <div class="item-image">
                                    <a href="<%# Eval("Link") %>" class="cta-button-text">
                                        <img src="<%# Eval("ImagePath") %>" alt="<%# Eval("ImageTitle") %>" />
                                    </a>
                                </div>
                                <!-- end .item-image -->
                                <div class="item-desc">
                                    <h3><%# Eval("Title") %></h3>
                                    <a class="cta-button-text" href="<%# Eval("Link") %>">
                                        <div class="cta-button-wrap black-btn">
                                            <span>Download</span>
                                        </div>
                                    </a>                                    
                                    <!-- end .cta-button-wrap -->
                                </div>
                                <!-- end .item-desc -->
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                    <!-- item brief-->
                </div>
            </div>
            <!--container 24-->
        </section>
    </div>
    <!-- end briefs_whitepapers -->
</div>
<!-- end briefs_whitepapers_wrapper  -->
<div class="clear"></div>
<!-- mmm  Marketing mmmmmmmmmmmmmmmmmmm   Marketing mmm -->
<div class="marketing_wrapper  clearfix">
    <div class="marketing clearfix">
        <section id="marketing">
            <div class="container_24">
                <div class="grid_3 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">marketing tango</h2>
                                <a id="blogsLink" runat="server" class="cta-button-text" target="_blank" href="#">
                                    <div class="cta-button-wrap white-orange-btn">
                                        <span>Visit the Blog</span>
                                    </div>
                                </a>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_3-->
                <div class="grid_21 col-height-equal">
                    <asp:Repeater runat="server" ID="uxBlogs">
                        <ItemTemplate>
                            <div class="item-marketing">
                                <div class="item-image">
                                    <img src="<%# Eval("Image") %>" alt="<%# Eval("Title") %>" />
                                </div>
                                <!-- end .item-image -->
                                <div class="item-desc">
                                    <span class="sml-date"><%# Eval("PostDate") %></span>
                                    <h3><%# Eval("Title") %></h3>
                                    <a class="cta-button-text" target="_blank" href="<%# Eval("MoreLink") %>">
                                        <div class="cta-button-wrap black-btn">
                                            <span>More</span>
                                        </div>
                                    </a>
                                </div>
                                <!-- end .item-desc -->
                            </div>
                            <!-- end item -->
                        </ItemTemplate>
                    </asp:Repeater>                    
                </div>
                <!--grid 16-->
            </div>
        </section>
    </div>
    <!-- end marketing -->
</div>
<!-- end marketing_wrapper  -->
<div class="clear"></div>
