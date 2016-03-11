<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="news.aspx.cs" Inherits="news" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear">
    </div>
    <!-- mmm About -News mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About-News mmm -->
    <div class="media_wrapper  clearfix">
        <div class="media main_content clearfix">
            <div class="container_24">
                <div class="grid_24">                    
                    <!-- mmmmmmmmmmmmmmmmmmmm NEWS ARTICLE mmmmmmmmmmmmmmmmmmmm -->
                    <asp:Repeater runat="server" ID="UxNewsArticle">
                        <ItemTemplate>
                            <div class="grid_24 news_article bottom-divider clearfix">

                                <div class="grid_4 meta">
                                    <div class="date">
                                        <h2><span><%# Eval("nmonth") %></span><%# Eval("ndate") %></h2>
                                        <span class="year"><%# Eval("nyear") %></span>
                                    </div>
                                    <!--// .date -->
                                </div>
                                <!--// meta -->
                                <div class="grid_20">
                                    <h3>
                                        <a href="<%# Eval("hrefText") %>">
                                            <%# Eval("title") %></a></h3>
                                    <div class="news_details">
                                        <%#Eval("imgTag") %>
                                        <h4>
                                            <%--<%# Eval("date") %>
                                        |--%>
                                            <%# Eval("city") %>,
                                        <%# Eval("state") %></h4>
                                        <p>
                                            <%# Eval("teaser") %>
                                        </p>
                                        <div class="square_button">
                                            <a href="<%# Eval("hrefText") %>" class="cta-button-text"><span>READ MORE</span></a>
                                        </div>
                                        <div class="social_share pull_5">
                                        </div>
                                        <!--// social_share -->
                                        <!-- end email_print_share -->
                                    </div>
                                </div>
                                <!-- news_details -->
                            </div>
                            <!-- news_article -->
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
            <div class="clear">
            </div>
            <div class="load_more" id="loadMoreNews" runat="server">
                <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
                <asp:LinkButton runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
                <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
            </div>
            <!-- end load_more_wrapper -->
        </div>
        <!--end news -->
    </div>
    <!-- end news_wrapper -->
    <div class="clear"></div>
    <!-- mmm We Are Here to Help mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We Are Here to Help mmm -->
    <div id="here_to_help_wrapper">
        <div id="here_to_help_img" class="img-holder-help" runat="server" data-image="/images/here_to_help_bkg.jpg" data-width="1600" data-height="580"></div>
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
