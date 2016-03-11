<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="in_the_media.aspx.cs" Inherits="in_the_media" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <div class="media_wrapper  clearfix">
        <div class="media main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div id="media_container" class="clearfix">
                        <div style="visibility: hidden; width: 100%;"></div>
                           <asp:ListView runat="server" ID="uxMediaWrapperListView1" GroupItemCount="3">
                              <GroupTemplate>
                                  <div class="media_col grid_8">
                                          <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                     </div>
                              </GroupTemplate>
                            <LayoutTemplate>
                                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="media_article_wrapper" id='mosaic-0-itemid-<%# Eval("contentId") %>'>
                                    <div class="media_article">
                                        <div class="date">
                                            <h2><span><%# Eval("nmonth") %></span><%# Eval("ndate") %></h2>
                                            <span class="year"><%# Eval("nyear") %></span>
                                        </div>
                                        <h3><%# Eval("source") %></h3>
                                        <p><%# Eval("title") %></p>
                                    </div>
                                    <!-- media_article -->
                                    <div class="grid_7 share_button"><a href="#">Share</a></div>
                                    <div class="grid_10 read_more_button"><a href="<%# Eval("url") %>">Read More</a></div>
                                    <div class="clearfix"></div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!--mosaicflow-->
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end media_articles -->
    </div>
    <div class="load_more" id="loadMoreNews" runat="server">
        <!-- <a href="#" class="cta-button-text"><span>LOAD MORE</span></a> -->
        <asp:LinkButton runat="server" class="cta-button-text" OnClick="LoadMoreLinkButton_Click"><span>LOAD MORE</span></asp:LinkButton>
        <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
    </div>
    <div class="clear">
    </div>
</asp:Content>
