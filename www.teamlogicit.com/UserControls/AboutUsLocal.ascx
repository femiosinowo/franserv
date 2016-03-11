<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AboutUsLocal.ascx.cs" Inherits="UserControls_AboutUsLocal" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/TestimonialSlider.ascx" TagPrefix="ux" TagName="TestimonialSlider" %>

<script type="text/javascript">
    $(document).ready(function () {

        var count1 = 1;
        $('.about_our_team .mgmt_profile_row').each(function () {                       
            var id = $(this).attr('id');
            id = id + count1;
            $(this).attr('id', id);
            count1++;
        });

        var count2 = 1;
        $('.about_our_team .mgmt_profile_detail_wrapper').each(function () {
            var id = $(this).attr('id');
            id = id + count2;
            $(this).attr('id', id);
            count2++;
        });

        $('.mgmt_profile_wrapper').each(function () {
            $(this).find('.grid_8:first-child').addClass("alpha");
            $(this).find('.grid_8:last-child').addClass("omega");
        });

    });
</script>

<!-- mmm Our Story (local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Our Story (local) mmm -->
<div class="our_story clearfix">
    <div class="our_story_content clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="container_24">
                    <div class="grid_24">                                             
                        <h2 class="headline"><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></h2>                   
                        <div class="grid_12">
                           <p class="intro_text"><asp:Literal ID="ltrDescription1" runat="server"></asp:Literal></p>
                        </div>
                        <!--//.grid_11-->
                        <div class="grid_12">
                           <p class="intro_text"><asp:Literal ID="ltrDescription2" runat="server"></asp:Literal></p>
                        </div>
                        <!--//.grid_11-->
                        <div class="clear"></div>
                        <ux:TestimonialSlider ID="uxTestimonialSlider" runat="server" />                        
                    </div>
                    <!--//.grid_24-->
                </div>
                <!--//.container_24-->
            </div>
            <!--// grid 24-->
        </div>
        <!-- // container_24  -->
    </div>
    <!--// our_story_content -->
</div>
<!-- //our_story -->
<div class="clear"></div>

<!-- mmm We're Local (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We're Local (loc) mmm -->
<div class="were_local_wrapper clearfix">
    <div class="were_local clearfix">
        <div class="container_24">
            <div class="grid_24">
                <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
                <script src="/js/location_map_black_marker.js" type="text/javascript"></script>
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
                    <div class="divider">/</div>
                    <!--divider-->
                    <div class="social_media">
                        <asp:Literal ID="ltrSocialIcons" runat="server" />                    
                    </div>
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
<div class="clear"></div>
<!-- mmm Meet Our Team (local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Meet Our Team (local) mmm -->
<div class="meet_our_team clearfix">
    <div class="meet_our_team_content clearfix">
        <div class="container_24">
            <div class="grid_24">
                <h2 class="headline" id="ourTeamHeadLine" visible="false" runat="server">our team</h2>                
            </div>
            <div class="grid_24 about_our_team" id="mgmt_team_main">
                    <asp:ListView ID="lvOurTeam" GroupItemCount="4" runat="server">
                        <LayoutTemplate>
                           <asp:PlaceHolder runat="server" id="groupPlaceholder" />
                        </LayoutTemplate>
                        <GroupTemplate>
                            <div id="row_" class="container_24 mgmt_profile_row">
                                <div class="grid_24 mgmt_profile_wrapper">
                                    <asp:PlaceHolder runat="server" id="itemPlaceholder" />     
                                </div>
                            </div>
                            <div class="clear"></div>
                            <div id="profile_detail_box_" class="mgmt_profile_detail_wrapper">
                                <div class="container_24">
                                    <div class="grid_24">
                                        <a class="close_button"><span class="visuallyhidden">X</span></a>
                                        <div class="profile_detail_content"></div>
                                    </div>
                                </div>
                            </div>                                                   
                        </GroupTemplate>                        
                        <ItemTemplate>
                            <div class="grid_8 cs_container">
                                <a href="#" class="cs_image" id="profile_<%# Eval("EmployeeId") %>">
                                    <img alt="" src="<%# GetEmployeeImage( Eval("PicturePath").ToString(), Eval("Gender").ToString() )%>" />
                                    <div class="cs_image_content_wrapper">
                                        <div class="cs_image_content">
                                            <h3><%# Eval("FirstName") %> <%# Eval("LastName") %></h3>
                                            <p><%# Eval("Title") %></p>
                                            <ul>
                                                <li class="mt_phone"><%# Eval("WorkPhone") %></li>
                                                <li class="mt_mobile"><%# Eval("MobileNumber") %></li>
                                                <li class="mt_email"><%# Eval("Email") %></li>
                                            </ul>
                                            <span href="#" class="more_button">more</span>
                                        </div>
                                        <!-- end cs_image_01_content -->
                                    </div>
                                    <!-- end cs_image_01_content_wrapper -->
                                </a>
                                <div class="profile_detail clearfix" id="profile_<%# Eval("EmployeeId") %>_detail">
                                    <div class="prefix_1 grid_22 suffix_1 omega profile_text">
                                        <div class="profile_text_inner">
                                            <div class="grid_12">
                                                <h3><%# Eval("FirstName") %> <%# Eval("LastName") %></h3>
                                                <h4><%# Eval("Title") %></h4>
                                                <h2 class="headline" style='<%# Eval("Company") == null || Eval("Company").ToString() == "" ? "display:none;" : "display:block;" %>' ><%# Eval("Company") %></h2>
                                                <ul>
                                                    <li class="mt_phone"><%# Eval("WorkPhone") %></li>
                                                    <li class="mt_mobile"><%# Eval("MobileNumber") %></li>
                                                    <li class="mt_email"><%# Eval("Email") %></li>
                                                </ul>
                                            </div>
                                            <div class="grid_12">
                                                <p><%# Eval("Bio") %></p>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end profile_text -->
                                </div>
                            </div>
                        </ItemTemplate>                        
                    </asp:ListView>
                </div>
        </div>        
    </div>
    <!--// meet_our_team_content -->
</div>
<div class="clear"></div>
<CMS:ContentBlock ID="cbOurTeamCompnayInfo" runat="server" Visible="false" DoInitFill="false" />
<!-- //meet_our_team -->
<div class="clear"></div>
<!-- mmm Join Team (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Join Team (nat) mmm -->
<div id="join_team_wrapper">
    <div class="img-holder-join" id="joinTeamImg" runat="server" data-image="/images/join_team_bg.jpg" data-width="1600" data-height="670"></div>
    <div class="join_team clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="join_team_content caption">
                    <CMS:ContentBlock ID="cbJoinOurTeam" runat="server" DoInitFill="false" />
                </div>
                <!--join team content-->
            </div>
            <!--//.grid_24-->
        </div>
        <!--//.container_24-->
    </div>
</div>
<!-- join_team-->
<!--join_team_wrapper-->
<div class="clear"></div>
