<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageCaseStudies.ascx.cs"
    Inherits="UserControls_HomePageCaseStudies" %>
<div class="case_studies_section_wrapper  clearfix">
    <div class="case_studies_section clearfix">
        <div class="case_studies_section">
            <div class="container_24">
                <div class="grid_6 headline-block col-height-equal">
                    <div class="int-headline-block headline-block-black int-block-1">
                        <div class="headline-content-outer">
                            <div class="headline-content-inner">
                                <span class="headline-block-icon-black"></span>
                                <h2 class="headline">
                                    Case <span>Studies</span></h2>
                                <a class="cta-button-text" href="/Case-Studies/">
                                    <div class="cta-button-wrap black-btn">
                                        <span>View All<br />
                                            Case Studies</span>
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
                        <asp:Repeater runat="server" ID="UxCaseStudiesSlider">
                            <ItemTemplate>
                                <li>
                                    <div class="img-wrap">
                                        <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>">
                                        <div class="img-desc">
                                            <h3>
                                                <%# Eval("title") %></h3>
                                            <p>
                                                <%# Eval("desc") %></p>
                                            <a class="cta-button-text" href="<%# Eval("hreftext") %>">
                                                <div class="cta-button-wrap white-btn">
                                                    <span>View Case Study</span>
                                                </div>
                                            </a>
                                        </div>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!-- #Case Studies-wrapper -->
            </div>
            <!--container 24-->
        </div>
    </div>
    <!-- end case_studies -->
</div>
<!--end case_studies_wrapper-->
<div class="clear">
</div>
