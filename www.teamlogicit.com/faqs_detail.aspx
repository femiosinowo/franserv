<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="faqs_detail.aspx.cs" Inherits="faqs_detail" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!-- mmm Slider NAV - header mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Slider NAV - header mmmmmmmmmmmm -->
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
    <!-- mmm FAQs: detail Content (resources) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm FAQs: detail tent mmm -->
    <div class="faqdetail_content_wrapper resources clearfix">
        <div class="faqdetail_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div id="topic_slider">
                        <asp:ListView ID="lvFaqs" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div id="topic<%# Container.DataItemIndex + 1 %>" class="topic_content_wrapper">
                                    <div class="container_24">
                                        <div class="grid_24">
                                            <p class="breadcrumb"><a href="/faqs/">All FAQs</a> / <span class="topic_text"><%# Eval("title") %></span></p>
                                            <div class="grid_3 alpha">
                                                <img src="<%# Eval("image") %>" alt="<%# Eval("title") %>" class="faq_topic_icon" />
                                            </div>
                                            <!--grid4-->
                                            <div class="grid_17 suffix_4 topic_content">
                                                <h2 class="headline">
                                                    <%# Eval("question") %>
                                                </h2>
                                                <%# Eval("answer") %>
                                            </div>
                                            <!--grid15 topic_content-->
                                        </div>
                                        <!-- grid24 -->
                                    </div>
                                    <!--container24-->
                                    <div class="clear"></div>
                                    <div class="all_questions_wrapper clearfix">
                                        <div class="all_questions_content">
                                            <div class="container_24">
                                                <div class="prefix_3 grid_17 suffix_4">
                                                    <%# Eval("description") %>
                                                </div>
                                                <!-- grid -->
                                            </div>
                                            <!--container_24-->
                                        </div>
                                        <!-- all_questions_content -->
                                    </div>
                                    <!--all_questions_wrapper clearfix-->
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                        <!-- topic_content_wrapper -->
                    </div>
                    <!-- topic_slider -->
                </div>
                <!--grid24-->
            </div>
            <!--container_24-->
        </div>
        <!-- faqdetail_content -->
    </div>
    <!-- faqdetail_content_wrapper -->
    <div class="clear"></div>
    <!-- mmm Slider NAV - content footer mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Slider NAV - content footer mmmmmmmmmmmm -->
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
    <div data-height="670" data-width="1600" id="lets_connect" runat="server" data-image="/images/lets_connect_bg.jpg" class="img-holder lets_connect_wrapper local"></div>
    <div class="lets_connect_local clearfix">
        <div class="lets_connect_content">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_12 left">
                        <div class="caption">
                            <CMS:ContentBlock ID="cbLetsConnect" runat="server" SuppressWrapperTags="true" DoInitFill="false" />
                        </div>
                        <!--let's connect content-->
                    </div>
                    <!-- grid -->
                    <div class="prefix_4 grid_8 right">
                        <div class="request_button"><a class="request_call" href="#">Request a Call from our team</a></div>
                        <p>or call
                            <br>
                            <asp:Label ID="lblPhoneNumber" runat="server"></asp:Label></p>
                    </div>
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- lets_connect_content -->
    </div>
    <div class="clear"></div>
</asp:Content>

