<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageNationalFooter.ascx.cs" Inherits="UserControls_HomePageNationalFooter" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<div class="footer_upper_wrapper  clearfix" id="national_site">
    <div class="footer_upper clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div id="footer_upper_content">
                    <!-- National site section ****** REMOVE INLINE STYLE AFTER SECTIONS ARE SEPARATED ******* -->
                    <div class="national_content">
                        <div class="grid_15 alpha col1">
                            <div class="content_wrapper">
                                <div class="header clearfix">
                                    <h2 class="headline gray">Briefs &amp; Whitepapers</h2>
                                    <div class="cta-button-wrap">
                                        <a href="/briefs-whitepapers/" class="cta-button-text"><span>View All</span></a>
                                    </div>
                                </div>
                                <!-- end .header -->
                                <div class="content">
                                    <asp:ListView ID="lvBriefsWhitePapers" runat="server">
                                            <LayoutTemplate>
                                               <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                            </LayoutTemplate>                                            
                                            <ItemTemplate>
                                                <div class="item brief">
                                                    <div class="item-image">
                                                        <a href="<%# Eval("Link") %>" class="cta-button-text">
                                                            <img src="<%# Eval("ImagePath") %>" alt="<%# Eval("ImageTitle") %>" />
                                                         </a>
                                                     </div>
                                                    <!-- end .item-image -->
                                                    <div class="item-desc">
                                                        <h3><%# Eval("Title") %></h3>
                                                        <p><%# Eval("Teaser") %></p>
                                                        <div class="cta-button-wrap">
                                                            <a href="<%# Eval("Link") %>" class="cta-button-text"><span>Download</span></a>
                                                        </div>
                                                        <!-- end .cta-button-wrap -->
                                                    </div>
                                                    <!-- end .item-desc -->
                                                </div>                                                
                                            </ItemTemplate>
                                        </asp:ListView> 
                                </div>
                                <!-- end .content -->
                                <!-- Marketing Tango -->
                                <hr class="gray-divider">
                                <div class="header clearfix">
                                    <img src="/images/logo-marketing-tango.png" alt="Marketing Tango">
                                    <div class="cta-button-wrap medium light-bg">
                                        <a href="http://www.marketingtango.com/" class="cta-button-text" target="_blank"><span>Visit <span class="shorten-header">the</span> Blog</span></a>
                                    </div>
                                </div>
                                <!-- end .header -->
                                <div class="content">
                                     <asp:Repeater runat="server" ID="uxBlogs">
                                            <ItemTemplate>
                                                <div class="item blog">
                                                    <div class="item-image">
                                                        <img src="<%# Eval("Image") %>" alt="<%# Eval("Title") %>"></div>
                                                    <!-- end .item-image -->
                                                    <div class="item-desc">
                                                        <h3>
                                                            <span class="post-date"><%# Eval("PostDate") %></span><%# Eval("Title") %></h3>
                                                        <p>
                                                            <%# Eval("Description") %></p>
                                                        <p>
                                                            <a target="_blank" href="<%# Eval("MoreLink") %>" class="more-link">More</a></p>
                                                    </div>
                                                    <!-- end .item-desc -->
                                                </div>
                                                <!-- end item -->
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </div>
                                <!-- end .content -->
                            </div>
                            <!-- end .content-wrapper -->
                        </div>
                        <!--end grid_15-->
                        <div class="grid_9 omega col2">
                            <div class="content_wrapper">
                                <div id="join" class="gray_divider">
                                    <CMS:ContentBlock ID="cbJoinOurTeam"  runat="server" DoInitFill="false" />                        
                                </div>
                                <!-- end #join -->
                                <hr class="gray-divider">
                                <div id="jobs">
                                    <div class="header clearfix">
                                        <h2 class="headline gray">Recent Jobs</h2>
                                        <div class="cta-button-wrap">
                                            <a href="/job-search/" class="cta-button-text"><span>View All</span></a>
                                        </div>
                                        <!-- end cta-button-wrap -->
                                    </div>
                                    <!-- end .header -->
                                    <div class="content">
                                        <asp:ListView ID="lvJobsList" GroupItemCount="2" runat="server">
                                            <LayoutTemplate>
                                                <ul>
                                                    <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                                                </ul>
                                            </LayoutTemplate>
                                            <GroupTemplate>
                                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                            </GroupTemplate>
                                            <ItemTemplate>
                                                <li>
                                                    <span class="sml-date"><%# DateTime.Parse(Eval("DatePosted").ToString()).ToString("MMM dd, yyyy") %> | <%# Eval("Location") %>.</span>
                                                    <a class="title" href="/job-description/?id=<%# Eval("JobId") %>"><%# Eval("Title") %></a>
                                                </li>
                                            </ItemTemplate>
                                        </asp:ListView>                                      
                                    </div>
                                    <!-- end .content -->
                                </div>
                                <!-- end #jobs -->
                            </div>
                            <!-- end .content-wrapper -->
                        </div>
                        <!--end grid_9 -->
                    </div>
                    <!-- end .national_content -->
                </div>
                <!-- end #footer_upper_content -->
            </div>
            <!--end grid 24-->
        </div>
        <!-- end container_24 -->

    </div>
    <!-- end upperfooter -->
</div>
<!-- end upperfooter_wrapper-->
<div class="clear"></div>
