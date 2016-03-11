<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="project_experts_detail.aspx.cs" Inherits="project_experts_detail" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <div class="topic_slider_nav_wrapper">
        <div class="topic_slider_content">
            <div class="container_24">
                <div class="grid_12 alpha">
                    <div class="slider_control prev"><span></span></div>
                    <!-- slider_control -->
                </div>
                <!-- grid -->
                <div class="grid_12 omega">
                    <div class="slider_control next"><span></span></div>
                    <!-- slider_control -->
                </div>
                <!-- grid-->
            </div>
            <!--container24 -->
        </div>
        <!-- topic_slider_content -->
    </div>
    <div class="clear"></div>
    <div class="expert_detail_wrapper clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div id="topic_slider">
                    <asp:ListView ID="lvProjectExpertsDetail" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div id="topic<%# Eval("id") %>" class="topic_content_wrapper">
                                <h2 class="headline topic_text"><%# Eval("titletxt") %></h2>
                                <div class="grid_12">
                                    <p><%# Eval("sec1desc") %></p>
                                </div>
                                <!--//.grid_11-->
                                <div class="grid_12">
                                    <p><%# Eval("sec2desc") %></p>
                                </div>
                                <!--//.grid_11-->
                                <div class="clear"></div>
                                <div id="large_testimonial_slider" class="expert_testimonials">
                                    <div class="slider-wrapper">
                                        <asp:ListView ID="lvTesimonials1" DataSource='<%# Eval("Testimonials") %>' runat="server">
                                            <LayoutTemplate>
                                                <ul class="bxslider-large">
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li class="testimonial-quote">
                                                    <img style='<%# Eval("PicturePath") == null || Eval("PicturePath").ToString() == "" ? "display:none;" : "display:block;" %>' class="testimonialImg" width="125" height="125" src="<%# Eval("PicturePath") %>" alt="Testimonial" />
                                                    <h3>/ <%# Eval("Statement") %></h3>
                                                    <span class="testimonial-author">- <%# Eval("FirstName") %> <%# FormatLastName(Eval("FirstName").ToString(), Eval("LastName").ToString()) %>
                                                        <%# FormatTitleCompany(Eval("Title") , Eval("Organization")) %>
                                                        <%# FormatContactDetails( Eval("EmailAddress"),Eval("PhoneNumber")) %>
                                                    </span>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView> 
                                    </div>
                                    <!--//slider_wrapper-->
                                    <!--//slider_wrapper-->
                                </div>
                                <!--//large_testimonial_slider-->
                                <div class="grid_24">
                                    <%# Eval("fulldesc") %>
                                </div>                                
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!-- topic_slider -->
            </div>
            <!-- grid24 -->
        </div>
        <!-- container24 -->
    </div>
    <div class="clear"></div>
    <div class="topic_slider_nav_wrapper">
        <div class="topic_slider_content">
            <div class="container_24">
                <div class="grid_12 alpha">
                    <div class="slider_control prev"><span></span></div>
                    <!-- slider_control -->
                </div>
                <!-- grid -->
                <div class="grid_12 omega">
                    <div class="slider_control next"><span></span></div>
                    <!-- slider_control -->
                </div>
                <!-- grid-->
            </div>
            <!--container24 -->
        </div>
        <!-- topic_slider_content -->
    </div>
    <div class="clear"></div>
    <div class="clear"></div>
    <!-- mmm We Are Here to Help mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We Are Here to Help mmm -->
    <div id="here_to_help_wrapper">
        <div id="here_to_help_img" class="img-holder-help" runat="server" data-image="/images/here_to_help_bkg.jpg" data-image-mobile="/images/here_to_help_bkg.jpg" data-width="1600" data-height="580"></div>
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
    </div> <!--here_to_help_wrapper-->
    <div class="clear"></div>
    <div class="clear"></div>
</asp:Content>

