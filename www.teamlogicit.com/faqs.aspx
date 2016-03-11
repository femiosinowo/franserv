<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="faqs.aspx.cs" Inherits="faqs" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="faqindex_content_wrapper resources clearfix">
        <div class="faq_content">
            <div class="container_24 clearfix">
                <div class="grid_24">
                    <h2 class="headline">common questions</h2>
                </div>
            </div>
            <asp:ListView ID="lvfaqCommonQuestions" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <div class="container_24 clearfix">
                        <div class="grid_24">
                            <div class="alpha prefix_1 grid_20 suffix_3">
                                <div class="question" id="question_<%# Eval("counter") %>">
                                    <p>
                                        <%# Eval("question") %>
                                    </p>
                                </div>
                                <!-- question-->
                            </div>
                            <!-- grid -->
                        </div>
                        <!-- grid24-->
                    </div>
                    <div class="answer_wrapper clearfix" id="question_<%# Eval("counter") %>_answer">
                        <div class="container_24">
                            <div class="prefix_1 grid_20 suffix_3">
                                <div class="answer">
                                    <%# Eval("answer") %>
                                </div>
                                <!-- answer -->
                            </div>
                            <!-- grid-->
                        </div>
                        <!-- container -->
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="clear"></div>
        <div class="faq_topics_wrapper clearfix">
            <div class="faq_topics_content">
                <div class="container_24">
                    <h2 class="headline">topics</h2>
                    <div class="grid_24">
                        <asp:ListView ID="lvfaqs" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <a href="<%# Eval("url") %>">
                                    <div class="grid_7 suffix_1 faq_topic <%# Eval("cssClass") %>">
                                        <div class="faq_topic_icon">
                                            <img src="<%# Eval("image") %>" alt="<%# Eval("title") %>">
                                        </div>

                                        <h3><%# Eval("title") %></h3>
                                        <%# Eval("shortdesc") %>
                                    </div>
                                </a>
                                <%# Eval("cssClear") %>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- grid 24 -->
                </div>
                <!-- container_24 -->
            </div>
            <!-- faq_topics_content -->
        </div>
        <!-- faq_topics_wrapper -->
    </div>
    <div class="load_more" id="loadMoreNews" runat="server">
        <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
        <asp:LinkButton ID="LinkButton1" runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
        <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
    </div>
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

