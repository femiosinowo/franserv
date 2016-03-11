<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageJoinOurTeam.ascx.cs"
    Inherits="UserControls_HomePageJoinOurTeam" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<div class="join_our_team_wrapper  clearfix">
    <div class="join_our_team clearfix">
        <div id="join_our_team">
            <div class="container_24">
                <div class="grid_4 headline-block headline-block-white col-height-equal">
                    <div class="headline-content-outer">
                        <div class="headline-content-inner">
                            <span class="headline-block-icon-white"></span>
                            <h2 class="headline">
                                Join <span>Our Team</span></h2>
                            <a class="cta-button-text" href="/Job-Search/">
                                <div class="cta-button-wrap white-btn">
                                    <span>Search All Jobs</span>
                                </div>
                            </a>
                        </div>
                        <!--headline content-->
                    </div>
                    <!--headline content-->
                </div>
                <!--grid 4-->
                <div class="grid_17 col-height-equal">
                    <div class="item-image">
                        <CMS:ContentBlock ID="cbJoinOurTeamImg" SuppressWrapperTags="true" runat="server"  DoInitFill="false" />                        
                    </div>
                </div>
                <!--grid 16-->
                <div class="grid_3 recent_jobs_content col-height-equal">
                    <h3 class="sm-headline">
                        Recent Jobs</h3>
                    <ul>
                        <asp:ListView ID="lvJobsList" GroupItemCount="3" runat="server">
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
                                    <a class="title" href="/job-description/?jobid=<%# Eval("JobId") %>"><%# Eval("Title") %></a>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>     
                    </ul>
                </div>
                <!--grid 3-->
            </div>
        </div>
    </div>
    <!-- end join_our_team -->
</div>
<!-- end join_our_team_wrapper  -->
<div class="clear">
</div>
