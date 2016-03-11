<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageLocalPortfolio.ascx.cs"
    Inherits="UserControls_HomePageLocalPortfolio" %>

<!-- mmm  Our Portfolio Wrapper mmmmmmmmmmmmmmmmmmm   Our Portfolio Wrapper mmm -->
<div class="our_portfolio_wrapper  clearfix">
    <div class="our_portfolio clearfix">
        <section class="our_portfolio">
            <div class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <h2 class="headline">Our<br />
                                    Portfolio</h2>
                                <a class="cta-button-text" href="/Portfolio/">
                                    <div class="cta-button-wrap black-btn">
                                        <span>View All Portfolio</span>
                                    </div>
                                </a>
                            </div>
                            <!--headline content-->
                        </div>
                        <!--headline content-->
                    </div>
                </div>
                <!--grid_6-->
                <div class="grid_18 content-block slider-wrapper col-height-equal">
                    <ul>
                        <asp:Repeater ID="rptPortfolio" runat="server">
                            <ItemTemplate>
                                <li>
                                    <div class="img-wrap">
                                        <img src="<%# Eval("PhotosetSmallUrl") %>" alt="<%# Eval("Title") %>">
                                        <div class="img-desc">
                                            <h3><%# Eval("Title") %></h3>
                                            <p><%# Eval("Description") %></p>
                                            <a class="cta-button-text" href="<%# FormatUrl(Eval("PhotosetId").ToString()) %>">
                                                <div class="cta-button-wrap white-btn">
                                                    <span>View Portfolio</span>
                                                </div>
                                            </a>

                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </section>
    </div>
</div>
