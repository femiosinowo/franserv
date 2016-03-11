<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="management_team.aspx.cs" Inherits="_management_team" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <!-- mmm Management Team (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Management Team (nat) mmm -->
    <div class="management_team national clearfix">
      <div class="management_team_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">our team</h2>
                </div>
                <!--grid 24-->
            </div>
            <!-- container 24-->
            <div class="grid_24" id="mgmt_team_main">
                <asp:Literal ID="ltrOurTeam" runat="server"></asp:Literal>                
            </div>
            <!--end grid_24-->
        </div>
        <!--// our_story_content -->
    </div>
    <!-- //our_story -->
    <div class="clear"></div>
    <!-- mmm Join Our Team (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Join Our Team (nat) mmm -->
    <div id="join_team_wrapper">
        <div id="join_team_img" class="img-holder-join" runat="server" data-image="/images/join_team_bg.jpg" data-width="1600" data-height="580"></div>
        <div class="join_team clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="join_team_content caption">
                        <CMS:ContentBlock ID="cbJoinTeam" runat="server" DoInitFill="false" />                        
                    </div>
                    <!--join team content-->
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- join_team-->
    </div>
    <!--join_team_wrapper-->
    <div class="clear"></div>

</asp:Content>
