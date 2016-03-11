<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="testimonials.aspx.cs" Inherits="testimonials" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline testimonials">
            <div class="container_24">
                <div class="grid_24">
                    <!-- Local - Testimonials  -->
                    <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation about-us-local">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">&nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- About Us Local -->
                    <ul id="about-local-desktop-nav">
                        <li class="about-local-link"><a href="/company-info/">Company Info</a></li>
                        <li class="why-different-link"><a href="/why-we-are-different/">Why We Are Different</a></li>
                        <li class="testimonials-link active"><a href="/testimonials/">Testimonials</a></li>
                        <li class="local-news-link"><a href="/news/">News</a></li>
                        <li class="local-media-link"><a href="/in-the-media/">In the Media</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm About - Testimonials mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-Testimonials mmm -->    
    <asp:UpdatePanel ID="updatePanelTestimonials" runat="server">
        <ContentTemplate>
            <div class="testimonials_main_wrapper  clearfix">
                <div class="testimonials_main clearfix">
                    <div class="container_24">
                        <asp:Panel ID="pnlTestimonials" runat="server">
                            <asp:Repeater runat="server" ID="UxTestimonials">
                                <ItemTemplate>
                                    <div class="testimonial_wrapper grid_24 clearfix">
                                        <div class="testimonial <%# Eval("cssClassText") %>">
                                            <%# Eval("logo_wrapper") %>
                                            <div class="statement-text">
                                                <p>
                                                    <%# Eval("statement") %>
                                                </p>
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
                            <p>No Testimonials available.</p>
                        </asp:Panel>
                    </div>
                    <!-- end container_24  -->
                </div>
                <!--end testimonials_main -->
            </div>
            <!-- end testimonials_main_wrapper -->
            <!-- testimonials_wrapper -->  
            <div class="load_more_wrapper" id="loadMoreTestimonials" runat="server">
                <div class="cta-button-wrap purple">
                    <asp:LinkButton ID="linkBtnLoadMore" runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
                    <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
                </div>
            </div>         
        </ContentTemplate>
    </asp:UpdatePanel>     
    <div class="clear"></div>
</asp:Content>
