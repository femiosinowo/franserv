<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageBriefsAndWhitepapers.ascx.cs"
    Inherits="UserControls_HomePageBriefsAndWhitepapers" %>


<div class="briefs_wps_section_wrapper  clearfix">
    <div class="briefs_wps_section clearfix">
        <div id="briefs_wps_section">
            <div class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">
                                    Briefs &amp;<br />
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
                                    <img src="<%# Eval("ImagePath") %>" alt="<%# Eval("ImageTitle") %>" />
                                </div>
                                <!-- end .item-image -->
                                <div class="item-desc">
                                    <h3>
                                        <%# Eval("Title") %></h3>
                                    <div class="cta-button-wrap black-btn">
                                        <a href="<%# Eval("Link") %>" class="cta-button-text"><span>Download</span></a>
                                    </div>
                                    <!-- end .cta-button-wrap -->
                                </div>
                                <!-- end .item-desc -->
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
            <!--container 24-->
        </div>
    </div>
    <!-- end briefs_whitepapers -->
</div>
<!-- end briefs_whitepapers_wrapper  -->
<div class="clear">
</div>
