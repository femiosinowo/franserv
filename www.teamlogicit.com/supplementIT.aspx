<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" 
    CodeFile="supplementIT.aspx.cs" Inherits="supplement_it" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($(".slider-wrapper")) {
                $('.bxslider').bxSlider({
                    slideWidth: 496,
                    controls: false
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BannerContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <div class="bottom_header clearfix Managed-it-child">
        <div class="container_24">
            <asp:Literal ID="ltrSupplementOutSourcing" runat="server" />
        </div>
        <!--container 24-->
    </div>
    <div class="clear"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">    
    <div class="our_services_wrapper Managed-it-supplementIT-wrapper clearfix">
        <div class="our_services clearfix">
            <div class="container_24">
                <div class="grid_24 Managed-it-child">
                    <asp:Literal ID="ltrSectionTitle" runat="server"></asp:Literal>
                     <asp:ListView ID="lvOurServices" runat="server">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>                                
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>                            
                            <li>
                                <div class="service_section">
                                    <img src="<%# Eval("ImagePath") %>" alt="" />
                                    <div class="service_section_content">
                                        <a href="<%# Eval("Link") %>">
                                            <h3><%# Eval("Title") %></h3>
                                        </a>
                                        <p><%# Eval("SubTitle") %></p>
                                    </div>
                                </div>
                            <!--service_section-->
                        </li>
                      </ItemTemplate>
                    </asp:ListView>                     
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- our services-->
    </div> 
    <div class="clear"></div>
    <div class="what_we_offer_wrapper local clearfix">
        <div class="what_we_offer clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">
                        <asp:Literal runat="server" ID="title"></asp:Literal></h2>
                    <div class="what_we_offer_video clearfix">
                        <asp:Literal runat="server" ID="video"></asp:Literal>
                    </div>
                    <!--what_we_offer video -->
                    <div class="what_we_offer_columns clearfix">
                        <!-- Columns -->
                        <div class="grid_12 alpha">
                            <p>
                                <asp:Literal ID="litDesc1" runat="server"></asp:Literal>
                            </p>
                        </div>
                        <!--//column 1-->
                        <div class="grid_12 omega">
                            <p>
                                <asp:Literal ID="litDesc2" runat="server"></asp:Literal>
                            </p>
                        </div>
                        <!-- //column 2-->
                    </div>
                    <!--//approach_columns-->
                    <div id="large_testimonial_slider">
                        <div class="slider-wrapper">
                            <asp:ListView ID="lvTesimonials1" runat="server">
                                <LayoutTemplate>
                                    <ul class="bxslider-large">
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </ul>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li class="testimonial-quote">
                                        <img style='<%# Eval("PicturePath") == null || Eval("PicturePath").ToString() == "" ? "display:none;" : "display:block;" %>' class="testimonialImg" width="125" height="125" src="<%# Eval("PicturePath") %>" alt="Testimonial" />
                                        <h3>/ <%# Eval("Statement") %></h3>
                                        <span class="testimonial-author">- <%# Eval("FirstName") %> <%# FormatLastName(Eval("FirstName").ToString(), Eval("LastName").ToString()) %><%# Eval("Title") %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>                            
                        </div>
                        <!--//slider_wrapper-->
                    </div>
                    <!--//large_testimonial_slider-->
                    <!--//large_testimonial_slider-->
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div class="how_work_wrapper local">
        <div class="how_work clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_12">
                        <CMS:ContentBlock ID="cbHowWeWork" runat="server" DoInitFill="false" />
                        <div id="small_testimonial_slider">
                            <div class="slider-wrapper">
                                <asp:ListView ID="lvTesimonials2" runat="server">
                                    <LayoutTemplate>
                                        <ul class="bxslider">
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li class="testimonial-quote">
                                            <img style='<%# Eval("PicturePath") == null || Eval("PicturePath").ToString() == "" ? "display:none;" : "display:block;" %>' class="testimonialImg" width="125" height="125" src="<%# Eval("PicturePath") %>" alt="Testimonial" />
                                            <h3>/ <%# Eval("Statement") %></h3>
                                            <span class="testimonial-author">- <%# Eval("FirstName") %> <%# Eval("LastName") %>, <%# Eval("Title") %></span>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>                                  
                            </div>
                            <!--//slider_wrapper-->
                        </div>
                        <!--//small_testimonial_slider-->
                    </div>
                    <!--//grid_12-->
                    <div class="grid_12 how_work_block">                       
                            <asp:ListView ID="lvManagedItServicesIcons" runat="server">                                
                                <LayoutTemplate>
                                     <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <GroupTemplate>
                                    <div class="grid_5">
                                        <ul>
                                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                        </ul> 
                                    </div>                                  
                                </GroupTemplate>
                                <ItemTemplate>                                   
                                    <li>
                                        <div class="how_work_div">
                                            <a class="scroll" href="#<%# Eval("id") %>">
                                                <div class="how_work_content">
                                                    <img src="<%# Eval("imgSRC") %>"></span>
									                 <h3>
                                                         <%# Eval("titletxt") %>
                                                     </h3>
                                                </div>
                                                <!--how we work block content-->
                                            </a>
                                        </div>
                                        <!--how we work block div-->
                                    </li>                                    
                                </ItemTemplate>
                            </asp:ListView>
                    </div>
                    <!--//grid_12 how_work_block-->
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
            <div class="clear"></div>
        </div>
        <!--//how_work-->
    </div>
    <asp:ListView ID="lvManagedItServicesDetails" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="clear"></div>
            <div id="<%# Eval("id") %>" class="img-holder" data-image="<%# Eval("bgimgSRC") %>" data-image-mobile="<%# Eval("bgimgSRC") %>" data-width="1600" data-height="670" style="visibility: hidden; height: 580px;"></div>
            <div class="strategic_guidance">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="caption it_services">
                            <%# Eval("titlehtml") %>
                            <p>
                                <%# Eval("subtitle") %>
                            </p>
                        </div>
                        <!--strategic_guidance content-->

                    </div>
                    <!-- grid 24-->
                </div>
                <!--container 24-->
                <div class="clear"></div>
            </div>
            <div class="clear"></div>
            <div class="key_strategies_wrapper local">
                <div class="key_strategies clearfix">
                    <div class="container_24">
                        <div class="grid_24 CustomOrderedList">
                            <%# Eval("desc") %>
                        </div>
                        <!-- grid 24-->
                    </div>
                    <!--container 24-->
                    <div class="clear"></div>
                </div>
                <!--//key_strategies-->
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
