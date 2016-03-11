<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="testimonials.aspx.cs" Inherits="testimonials" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">    
    <div class="clear"></div>    
    <!-- mmm What Clients Think (local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm What Clients Think (local) mmm -->
<%--    <asp:ScriptManager ID="ScriptManager" runat="server" /><!-- Moved to master page-->--%>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="testimonials_wrapper  clearfix">
                <div class="main_content clearfix">
                    <div class="container_24">
                        <div class="grid_24">
                            <!-- mmmmmmmmmmmmmmmmmmmm TESTIMONIALS mmmmmmmmmmmmmmmmmmmm -->
                            <div class="grid_24 testimonials_article bottom-divider clearfix">
                                <CMS:ContentBlock ID="cbTestimonialsIntro" runat="server" DoInitFill="false" />
                                <asp:Repeater runat="server" ID="uxTestimonials">
                                    <ItemTemplate>
                                        <div id="testimonial_<%# Container.ItemIndex %>" class="testimonial_details">
                                            <div class="testimonial_content">
                                                <%# Eval("ImagePath") %>
                                                <h3>/ <%# Eval("Statement") %>
                                                    <span class="testimonial-author">- <%# Eval("FirstName") %> <%# FormatLastName(Eval("FirstName").ToString(), Eval("LastName").ToString()) %>
                                                        <%# FormatTitleCompany(Eval("Title") , Eval("company")) %>
                                                        <%# FormatContactDetails( Eval("emailAdress"),Eval("phonenumber")) %>
                                                    </span>
                                                </h3>
                                            </div>
                                            <!--// .testimonial_content -->
                                        </div>
                                    </ItemTemplate>                                    
                                </asp:Repeater>
                                <asp:Panel ID="pnlNoTestimonials" runat="server" Visible="false">
                                    <p>No Testimonials to display</p>
                                </asp:Panel>
                                <div class="clear"></div>
                            </div>
                            <!--// testimonials_article -->
                        </div>
                        <!--end grid 24-->
                    </div>
                    <!-- end container_24  -->
                </div>
                <!--end testimonials_articles -->
            </div>
            <!-- testimonials_wrapper -->
            <div class="load_more" id="loadMoreNews" runat="server">
                <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
                <asp:LinkButton ID="linkBtnLoadMore" runat="server" class="cta-button-text linkBtnLoadMore" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
                <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
                <input type="hidden" id="hdnPreviousCount" class="hdnPreviousCount" runat="server" value="10" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="clear"></div>
    <!-- mmm We Are Here to Help mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We Are Here to Help mmm -->
    <div id="here_to_help_wrapper">
        <div id="here_to_help_img" class="img-holder-help" runat="server" data-image="/images/here_to_help_testimonials_bkg.jpg" data-width="1600" data-height="580"></div>
        <div class="img_holder_content clearfix">
            <div class="container_24">
                <div class="grid_11 suffix_13">
                    <div class="caption">
                        <CMS:ContentBlock ID="cbHereToHelp" runat="server" DoInitFill="false" />
                    </div>
                    <!--caption-->
                </div>
                <!--//.grid_24-->
            </div>
            <!--//.container_24-->
        </div>
        <!--img_holder_content-->
    </div>
    <!--here_to_help_wrapper-->
    <div class="clear"></div>
</asp:Content>
