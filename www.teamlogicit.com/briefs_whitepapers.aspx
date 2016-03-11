<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers.aspx.cs" Inherits="briefs_whitepapers" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear">
    </div>
    <div class="bwindex_content_wrapper resources clearfix">
        <asp:ListView ID="lvbBriefAndWhitepapers" runat="server">
            <LayoutTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </LayoutTemplate>
            <ItemTemplate>
                <div class="divider clearfix">
                    <div class="container_24 brief">
                        <div class="grid_24">
                            <div class="grid_15">
                                <h2 class="headline"><a href="<%# Eval("hrefText") %>"><%# Eval("title") %></a></h2>
                                <p><%# Eval("abstract")%></p>
                                <div class="bw_meta">
                                    <ul>
                                        <li class="read_more">
                                            <div class="square_button"><a href="<%# Eval("hrefText") %>">Read More</a></div>
                                        </li>

                                    </ul>
                                </div>
                                <!--//.bw_meta -->
                            </div>
                            <!--// end left content -->

                            <div class="grid_6 push_3 thumbnail">
                                <img src="<%# Eval("imgSRC") %>" alt="<%# Eval("title") %>">
                            </div>
                            <!--//.thumbnail -->
                        </div>
                        <!--// .grid_24-->
                    </div>
                    <!--// .brief-->
                </div>
                <!--//.divider story 01-->
            </ItemTemplate>
        </asp:ListView>
    </div>
    <div class="clear">
    </div>
    <!-- mmm How Can We Help You (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm How Can We Help You (loc) mmm -->
    <div class="img-holder" id="how_we_can_help_Img" runat="server" data-image="/images/how_we_can_help_bkg.jpg" data-width="1600" data-height="670"></div>
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
