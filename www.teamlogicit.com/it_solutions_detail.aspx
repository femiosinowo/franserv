<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="it_solutions_detail.aspx.cs" Inherits="it_solutions_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" runat="Server"> 
    <script src="/js/jquery.bxslider.min.js" type="text/javascript"></script>
    <script src="/js/dropit.js" type="text/javascript"></script>
    <script type="text/javascript">
         $(document).ready(function () {
             $('#it_solutions').dropit();

             //solution detail page slider 
             var w = $(window).innerWidth();
             var maxSlides;
             if (w < 465) {
                 maxSlides = 1;
             }
             else {
                 maxSlides = 5;
             }

             $(".case_studies_block ul").bxSlider({
                 pager: false,
                 slideWidth: 300,                 
                 minSlides: 1,
                 maxSlides: maxSlides,
                 moveSlides: 1
             });
         });
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <div class="our_solutions_wrapper">
        <div class="it_our_solutions clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="row clearfix solutions_details_header">
                        <div class="grid_10">
                            <h2 class="headline">our solutions</h2>
                        </div>
                        <div class="grid_10 prefix_4">
                            <div class="square_button" id="pdfLinkButton" runat="server" visible="false">
                                <%--<a href="#">--%>
                                <asp:Literal ID="prflink" runat="server"></asp:Literal>
                                <img src="/images/request_call_icon_white_small.png" alt="request_call_icon_white_small" width="32" height="32">Download Info Sheet                                   
                                    <asp:Literal ID="endAtag" runat="server" Text="</a>"></asp:Literal>
                            </div>
                        </div>
                        <!-- grid 24-->
                    </div>
                    <!--container 24-->
                </div>
                <!--// .it_our_solutions-->
            </div>
        </div>
    </div>
    <!--// our_solutions_wrapper-->
    <div class="clear"></div>
    <asp:ListView ID="lvSolutionsDetail" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="solutions_details_content_nonparallax">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="row clearfix solutions_details_content">
                            <div class="grid_12">
                                <img src="<%# Eval("imgsrc") %>">
                            </div>
                            <div class="grid_10 prefix_2">
                                <h2 class="headline"><%# Eval("headline") %></h2>
                                <h2 class="sub_headline"><%# Eval("subheadline") %></h2>
                                <p><%# Eval("description") %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div class="clear"></div>
            <div class="img-holder solutions-parallax-div" data-image="/images/parallax-transparent.png" data-image-mobile="/images/parallax-transparent.png" data-width="1600" data-height="670"></div>
            <div class="solutions_details_content_parallax">
                <div class="container_24">
                    <div class="grid_24">
                        <div class="row clearfix solutions_details_content">
                            <div class="grid_12">
                                <h2 class="headline"><%# Eval("headline") %></h2>
                                <h2 class="sub_headline"><%# Eval("subheadline") %></h2>
                                <p><%# Eval("description") %></p>
                            </div>
                            <div class="grid_10 prefix_2">
                                <img src="<%# Eval("imgsrc") %>">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </AlternatingItemTemplate>
    </asp:ListView>    
    <div class="clear"></div>
    <div class="solutions_related_case_studies">
        <div class="container_24">
            <div class="grid_24">
                <h2 class="headline">related case studies</h2>
                <div class="case_studies_block">
                    <asp:ListView ID="lvCaseStudies" runat="server">
                        <LayoutTemplate>
                            <ul class="bxslider">
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="case_studies_div case_studies_<%# Eval("counter") %>" style="background-image: url('<%# Eval("imgSRC") %>')">
                                    <a href="<%# Eval("hreftext") %>">
                                        <div class="case_studies_content">
                                            <img src="<%# Eval("iconimgSRC") %>" />
                                            <h3><%# Eval("title") %></h3>
                                            <h4><%# Eval("desc") %></h4>
                                        </div>
                                        <!--case studies content-->
                                    </a>
                                </div>
                                <!--case studies div-->
                            </li>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <div data-height="670" data-width="1600" runat="server" data-image-mobile="/images/how_we_can_help_bkg.jpg" data-image="/images/how_we_can_help_bkg.jpg" id="how_we_can_help" class="img-holder"></div>
    <div class="how_we_can_help clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="how_we_can_help_content">
                    <asp:Literal ID="ltrHowWeCanHelpTitle" runat="server" />
                </div>
                <!--how_we_can_help content-->
                <div class="clear"></div>
                <div class="bottom_header clearfix">
                    <div class="container_24">
                        <asp:Literal ID="ltrHowWeCanHelp" runat="server" />
                    </div>
                    <!--container 24-->
                </div>
                <!--bottom_header-->
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <div class="clear"></div>
</asp:Content>

