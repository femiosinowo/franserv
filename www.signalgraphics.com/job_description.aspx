<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="job_description.aspx.cs" Inherits="job_description" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Job Search Header mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Search Header (Image)  mmm -->
    <div id="subpage_tagline_wrapper" runat="server" class="subpage_tagline_wrapper job_profiles clearfix">
        <div class="subpage_tagline">
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation join_our_team">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Join Our Team -->
                    <ul id="join-our-team-desktop-nav">
                        <li class="join-our-team-link"><a href="/join-our-team/">Join Our Team</a></li>
                        <li class="job-profiles-link"><a href="/job-profiles/">Job Profiles</a></li>
                        <li class="job-search-link"><a href="/job-search/">Job Search</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Job Description content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Description content  mmm -->
    <div class="job_description_wrapper  clearfix">
        <div class="job_description main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24 clearfix">
                        <div class="grid_24">
                            <div class="cta-button-wrap white small back-button prev-page">
                                <a href="javascript:history.go(-1)" id="backUrl" class="cta-button-text"><span>Back</span></a>
                            </div>
                            <!--  cta-button-wrap -->
                        </div>
                        <!--end grid_24-->
                    </div>
                    <!--end container_24-->
                    <div id="job_description_content" class="container_24 clearfix">
                        <div class="grid_24">
                            <h2 class="subpage">
                                <asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal> <span><asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></span></h2>
                            <div class="date_links">
                                <p class="gray">
                                    <strong>Posted On</strong>: <asp:Literal ID="ltrJobPostedDate" runat="server"></asp:Literal></p>
                                <script type="text/javascript">
                                    function myPrintFunction() {
                                        window.print();
                                    }
                                </script>
                                <div class="email_print_share">
                                    <ul>
                                        <li><a onclick="myPrintFunction();">
                                            <img alt="Print" src="/images/doc-print-gray.png"></a></li>
                                        <li><span class='st_email_gray'></span><%--
                                            <asp:LinkButton runat="server" ID="email_btn" OnClick="EmailBtn_Click">
                                            <img alt="Email" src="/images/doc-email-white.png"></asp:LinkButton>--%></li>
                                        <li><span class="st_sharethis_custom_gray"></span><%--<a href="#">
                                            <img alt="Share" src="/images/doc-share-white.png"></a>--%></li>
                                    </ul>
                                </div>
                                <%--<div class="email_print_share">
                                    <ul>
                                        <li><a href="#">
                                            <img src="/images/doc-print-gray.png" alt="Print" /></a></li>
                                        <li><a href="#">
                                            <img src="/images/doc-email-gray.png" alt="Email" /></a></li>
                                        <li><a href="#">
                                            <img src="/images/doc-share-gray.png" alt="Share" /></a></li>
                                    </ul>
                                </div>--%>
                            </div>
                            <!-- end date_links -->
                        </div>
                        <!--end grid_24-->
                        <div class="grid_13 suffix_3 desc_col_1">
                            <asp:Literal ID="ltrJobDescription" runat="server"></asp:Literal>
                            <div class="cta-button-wrap purple">
                                <a class="cta-button-text" id="applyJobLink" runat="server" href="/apply-to-job/"><span>APPLY TO THIS JOB</span></a>
                            </div>
                            <!-- end cta-button-wrap -->
                        </div>
                        <!-- end desc_col_1 -->
                        <div class="grid_7 prefix_1 desc_col_2">
                            <div id="job_location_map_wrapper">
                                <img id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=312x312&zoom=15&markers=icon:http://author.sirspeedy.com/sandbox/sirspeedy/images/location-map-marker.png%7C33.976463,-118.036812&style=feature:landscape%7Ccolor:0xe9e9e9&style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&sensor=true"
                                    alt="placeholder map" />
                            </div>
                            <h3>
                                Location</h3>
                            <p>
                                <strong><asp:Literal ID="ltrCenterAddress" runat="server"></asp:Literal></strong></p>
                            <h3>
                                Contact</h3>
                            <p class="no-margin-bottom">
                                <strong><asp:Literal ID="ltrCenterContactName" runat="server"></asp:Literal></strong></p>
                            <ul class="contact_info_icons_small gray">
                                <li class="phone"><span><asp:Literal ID="ltrCenterContactNumber" runat="server"></asp:Literal></span></li>
                                <li class="email"><span><asp:Literal ID="ltrCenterEmailAdress" runat="server"></asp:Literal></span></li>
                            </ul>
                            <hr />
                            <h3>
                                How to Apply</h3><br />
                            <CMS:ContentBlock ID="cbHowToApplyText" Visible="false" runat="server" DoInitFill="false" />                            
                            <div class="cta-button-wrap purple small">
                                <a class="cta-button-text" id="applyJobLink2" runat="server" href="/apply-to-job/"><span>APPLY NOW</span></a>
                            </div>
                            <!-- end cta-button-wrap -->
                        </div>
                        <!-- end desc_col_1 -->
                    </div>
                    <!--end job_description_content container_24 -->
                </div>
                <!--end grid_24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end job_description -->
    </div>
    <!-- end job_description_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
