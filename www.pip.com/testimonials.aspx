<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="testimonials.aspx.cs" Inherits="testimonials" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="site_container local" id="testimonials">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <CMS:ContentBlock ID="cbTestimonialsHeaderImage" runat="server" DynamicParameter="id"
                        CacheInterval="300" />
                    <%--<img src="images/headers/header_1.jpg" alt="">--%>
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image_content -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
        <div class="sub_navigation_wrapper  clearfix">
            <div class="sub_navigation about-us-local">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="about-local-menu-h2">
                            <h2 id="menu-about-pip">Company Info</h2>
                            <h2 id="menu-why-different">Why We're Different</h2>
                            <h2 id="menu-testimonials">Testimonials</h2>
                            <h2 id="menu-news">News</h2>
                            <h2 id="menu-media">In The Media</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us Local -->
                        <ul id="about-local-desktop-nav">
                            <li class="about-us-local-link"><a href="/Company-Info/">About PIP</a></li>
                            <li class="why-different-link"><a href="/Why-We-Are-Different/">Why We're Different</a></li>
                            <li class="testimonials-link active"><a href="/Testimonials/">Testimonials</a></li>
                            <li class="local-news-link"><a href="/News/">News</a></li>
                            <li class="local-media-link"><a href="/In-The-Media/">In the Media</a></li>
                        </ul>
                    </div>
                </div>
                <!--end container_24-->
            </div>
            <!-- end sub_nav -->
        </div>
        <!-- end sub_nav_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm About - Testimonials mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-Testimonials mmm -->
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="testimonials_content_wrapper local_about_us clearfix">
                    <div class="testimonials_content clearfix">
                        <div class="container_24" id="testimonials_content">
                            <asp:Panel ID="pnlTestimonials" runat="server">
                                <asp:Repeater runat="server" ID="UxTestimonials">
                                    <ItemTemplate>
                                        <div class="testimonial_wrapper grid_24 clearfix">
                                            <div class="testimonial <%# Eval("cssClassText") %>">
                                                <%# Eval("logo_wrapper") %>
                                                <div class="testimonial-text">
                                                    <blockquote>
                                                        <%# Eval("statement") %>
                                                    </blockquote>
                                                    <p class="testimonial_source">
                                                        -           
                                                <%# Eval("firstName") %> <%# Eval("lastName") %>

                                                        <%#((!string.IsNullOrEmpty(Eval("firstName").ToString()) || 
                                                        !string.IsNullOrEmpty(Eval("lastName").ToString()) ) && !string.IsNullOrEmpty(Eval("title").ToString()) ?  " - " :" ")%>


                                                        <%# Eval("title")%> <%# Eval("organization") %>
                                                    </p>
                                                </div>
                                                <!-- end statement-text -->
                                            </div>
                                            <!-- end testimonial -->
                                        </div>
                                        <!-- end testimonial_wrapper grid_24 clearfix -->
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:Panel ID="pnlNoTestimonials" Visible="false" runat="server">
                                <p>
                                    No Testimonials available.
                                </p>
                            </asp:Panel>
                        </div>
                        <!-- end container_24  -->
                    </div>
                    <!--end testimonials_main -->
                </div>
                <!-- end testimonials_main_wrapper -->
                <div class="clear">
                </div>
             <%--   <div class="int_load_more_wrapper" runat="server" id="loadMoreContent">
                    <asp:LinkButton ID="linkBtnLoadMore" runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><div class="cta-button-wrap white-orange-btn"><span>LOAD MORE</span></div></asp:LinkButton>
                    <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
                    <!-- end cta-button-wrap -->
                </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
