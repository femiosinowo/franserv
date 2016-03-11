<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageCaseStudies.ascx.cs" Inherits="UserControls_HomePageCaseStudies" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="HomePageCaseStudies" %>

 <div class="our_portfolio_studies_wrapper  clearfix">
        <div class="our_portfolio_studies clearfix">
            <h2 class="headline">
                Read Our Case Studies</h2>
            <div class="our_portfolio_studies_home">
                <!-- <h2 class="headline">Related Case Studies</h2> -->
                <div class="slider-wrapper">
                    <ul>
                    <asp:Repeater runat="server" ID="UxCaseStudiesSlider">
                        <ItemTemplate>
                            <li><a href="<%# Eval("hreftext") %>">
                            <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>"><span></span></span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    </ul>
                </div>
                <!-- #products-slider-wrapper -->
                <div id="case-studies-cta" class="cta-button-wrap purple">
                    <a class="cta-button-text" href="/case-studies/"><span>All Case Studies</span></a>
                </div>
            </div>
        </div>
        <!-- end our_portfolio_sudies -->
    </div>
    <!-- end our_portfolio_studies_wrapper -->
    <div class="clear"></div>
