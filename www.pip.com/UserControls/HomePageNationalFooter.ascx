<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageNationalFooter.ascx.cs"
    Inherits="UserControls_HomePageNationalFooter" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<div class="marketing_wrapper  clearfix">
    <div class="marketing clearfix">
        <div id="marketing">
            <div class="container_24">
                <div class="grid_3 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-white int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-white"></span>
                                <h2 class="headline">
                                    marketing tango</h2>
                                <a class="cta-button-text" href="http://www.marketingtango.com/" target="_blank">
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
                            <div  class="item-marketing">
                                <div class="item-image">
                                    <img src="<%# Eval("Image") %>" alt="<%# Eval("Title") %>"></div>
                                <!-- end .item-image -->
                                <div class="item-desc">
                                    <h3>
                                        <span class="sml-date">
                                            <%# Eval("PostDate") %></span><%# Eval("Title") %></h3>
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
                <!--grid 21-->
            </div>
        </div>
    </div>
    <!-- end marketing -->
</div>
<!-- end marketing_wrapper  -->
<div class="clear">
</div>
