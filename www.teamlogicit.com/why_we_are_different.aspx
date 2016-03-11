<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="why_we_are_different.aspx.cs" Inherits="why_we_are_different" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <p></p>
    <p></p>
    <!-- mmm About Us - Why We Are Different mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm About Us - Why We Are Different mmm -->
    <div class="why_different_wrapper  clearfix">
        <div class="why_different main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_9">
                        <h2 class="headline">
                            <asp:Literal ID="ltrContentTitle" runat="server"></asp:Literal></h2>
                        <p class="tagline_inner">
                            <asp:Literal ID="ltrContentTagLine" runat="server"></asp:Literal>
                        </p>
                        <asp:Literal ID="ltrDescription" runat="server"></asp:Literal>
                    </div>
                    <!-- end grid_9 -->
                    <div class="grid_13 prefix_2 omega video_section">
                        <div class="video_wrapper">
                            <iframe id="videoIframe" runat="server" src="#" visible="false" frameborder="0" webkitallowfullscreen mozallowfullscreen allowfullscreen></iframe>
                        </div>                        
                        <!-- end video_wrapper -->
                        <div class="statement-text">
                            <p>
                                <asp:Literal ID="ltrStatementText" runat="server"></asp:Literal>
                            </p>
                        </div>
                        <!-- end statement-text -->
                    </div>
                    <!-- end grid_15 -->
                    <div class="clear"></div>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!--end why_different -->
    </div>
    <!-- end why_different_wrapper -->
    <div class="clear"></div>
    <!-- mmm We're Local (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We're Local (loc) mmm -->
    <div class="were_local_wrapper clearfix">
        <div class="were_local clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
                    <script src="../js/location_map_black_marker.js" type="text/javascript"></script>
                    <div class="location_wrapper">
                        <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                        <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                        <div id="location_map_black">
                            <!-- Google map container -->
                        </div>
                        <!-- #location_map_black -->
                    </div>
                    <!-- end your location wrapper -->
                    <div class="were_local_section">
                        <ul class="local_contact_info about">
                            <asp:Literal ID="litLocAddress" runat="server" />
                            <asp:Literal ID="ltrPhoneNumber" runat="server" />
                            <asp:Literal ID="ltrEmailAddress" runat="server" />
                        </ul>
                    </div>
                    <!--were_local_section-->
                    <div class="divider">/</div>
                    <!--divider-->
                    <div class="were_local_section about">
                        <div class="title_about">HOURS</div>
                        <asp:Literal ID="ltrHour" runat="server" />
                    </div>
                    <!--were_local_section-->
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- we're local-->
    </div>
    <!-- were_local_wrapper-->
    <div class="clear"></div>
    <!-- mmm Meet Our Team (local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Meet Our Team (local) mmm -->
    <div class="meet_our_team clearfix">
        <div class="meet_our_team_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">our team</h2>
                </div>
            </div>
            <asp:Literal ID="ltrOurTeam" runat="server"></asp:Literal>
        </div>
        <!--// meet_our_team_content -->
    </div>
    <div class="clear"></div>
    <CMS:ContentBlock ID="cbOurTeamCompnayInfo" runat="server" DoInitFill="false" />
    <!-- //meet_our_team -->
    <div class="clear"></div>
</asp:Content>

