<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="case_studies.aspx.cs" Inherits="case_studies" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="client_stories_wrapper resources clearfix">
        <div class="container_24">
            <div class="grid_24">
                <CMS:ContentBlock ID="cbIntro" runat="server" DoInitFill="false" />
                <div class="case_studies_block">
                    <asp:ListView ID="lvCaseStudiesPhotos" runat="server">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <li>
                                <div class="case_studies_div case_studies_<%# Eval("Id") %>" style="background-image: url('<%# Eval("imgSRC") %>');">
                                    <a href="<%# Eval("hreftext") %>">
                                        <div class="case_studies_content">
                                            <img src="<%# Eval("iconimgSRC") %>">
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
                <!--case studies block-->
            </div>
            <!--//.grid_24-->
        </div>
        <!--//.container_24-->
    </div>
    <div class="clear">
    </div>
    <div class="load_more" id="loadMoreNews" runat="server">
        <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
        <asp:LinkButton ID="LinkButton1" runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
        <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
    </div>
    <div class="clear">
    </div>
    <!-- mmm How Can We Help You (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm How Can We Help You (loc) mmm -->
    <div class="img-holder" runat="server" id="how_we_can_help_img" data-image="/images/how_we_can_help_bkg.jpg" data-width="1600" data-height="670"></div>
    <div class="how_we_can_help clearfix">
        <div class="container_24">
            <div class="grid_24">
                <asp:Literal ID="ltrSupplementOutSourcing" runat="server"></asp:Literal>
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- how_we_can_help-->
    <!--how_we_can_help_wrapper-->
    <div class="clear"></div>
</asp:Content>
