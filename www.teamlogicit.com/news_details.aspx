<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="news_details.aspx.cs" Inherits="news_details" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear">
    </div>
    <!-- mmm Pagination Top (shared) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Pagination Top (shared) mmm -->
    <div class="news_topic_slider">
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
        <!--topic_slider_nav_wrapper -->
        <div class="clear"></div>
    </div>
    <!--news_topic_slider-->
    <!-- mmm News Details (shared) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm News Details (shared) mmm -->
    <div class="media_wrapper news_details_wrapper clearfix">
        <div class="container_24">
            <div class="grid_24 clearfix">
                <div id="topic_slider" class="news_article full">
                    <asp:ListView ID="UxNewsDetails" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div id="topic<%# Eval("id") %>" class="topic_content_wrapper">
                                <div class="grid_4">
                                    <div class="date">
                                        <h2><span><%# Eval("nmonth") %></span><%# Eval("ndate") %></h2>
                                        <span class="year"><%# Eval("nyear") %></span>
                                    </div>
                                    <!--// .date -->
                                </div>
                                <!-- meta -->
                                <div class="grid_16 article_title">
                                    <h1 class="headline topic_text"><%#Eval("title") %></h1>
                                    <h4><%#Eval("city") %>, <%#Eval("state") %></h4>
                                </div>
                                <!-- article_title -->
                                <div class="grid_4 social_share news_details">
                                    <ul>
                                        <li class="news_print"><a class="print" href="javascript:void('0')">
                                            <img src="/images/black_print_icon.png" alt="black_printer_icon" /></a></li>
                                        <li class="news_email"><span class='st_email'><a href="javascript:void('0')">
                                            <img src="/images/black_mail_icon.png" alt="black_email_icon" /></a></span></li>                                       
                                        <li class="news_share"><span class="st_sharethis_custom" st_url="<%#Eval("quicklink") %>"><a href="javascript:void('0')">Share</a></span></li>
                                    </ul>
                                </div>
                                <!-- grid4 -->
                                <div class="clear"></div>
                                <div class="prefix_4 grid_11 news_article_content">
                                   <%-- <p>--%>
                                        <%#Eval("content") %>
                                    <%--</p>--%>
                                </div>
                                <!-- end news_article_content -->
                                <div class="grid_9	 omega news_article_img">
                                    <%#Eval("imgTag") %>
                                </div>
                                <!-- end news_article_content -->
                            </div>
                            <!-- topic_content_wrapper -->
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!--topic_slider-->
            </div>
            <!-- end grid_24  -->
        </div>
        <!-- end container_24  -->
    </div>
    <!-- news_details -->
    <div class="clear"></div>
    <div class="news_topic_slider">
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
        <!--topic_slider_nav_wrapper -->
        <div class="clear"></div>
    </div>
    <!--news_topic_slider-->
    <div class="clear"></div>
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
