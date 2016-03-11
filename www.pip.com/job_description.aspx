<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="job_description.aspx.cs" Inherits="job_description" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="site-container" id="job-search">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="images/headers/header_1.jpg" alt="">--%>
                    <CMS:ContentBlock ID="cbJobDescriptionHeaderImage" runat="server" DynamicParameter="id"
                        CacheInterval="300" />
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image_content -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
        <div class="sub_navigation_wrapper  clearfix">
            <div class="sub_navigation join-team">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="join-menu-h2">
                            <h2 id="menu-job-pip">
                                Jobs at PIP</h2>
                            <h2 id="menu-job-profiles">
                                Job Profiles</h2>
                            <h2 id="menu-job-search">
                                Job Search</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                                &nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us National -->
                        <ul id="join-our-team-desktop-nav">
                            <li class="job-pip-link"><a href="/Join-Our-Team/">Jobs at PIP</a></li>
                            <li class="job-profiles-link active"><a href="/Job-Profiles/">Job Profiles</a></li>
                            <li class="job-search-link"><a href="/Job-Search/">Job Search</a></li>
                        </ul>
                    </div>
                </div>
                <!--end container_24-->
            </div>
            <!-- end sub_nav -->
        </div>
        <!-- end sub_nav_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm join_team Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm join_team Content mmm -->
        <div class="job_description_content_wrapper main_about_us clearfix">
            <div class="job_description_content clearfix">
                <div id="job_description_content" class="container_24">
                    <div class="grid_24 job_header">
                        <div class="job_back_btn">
                            <a href="/Job-Search/" id="backUrl" class="black-btn">Back to Job List</a>
                        </div>
                        <span class="headline-block-icon-black"></span>
                        <h2 class="headline">
                            <asp:Literal ID="ltrJobTitle" runat="server"></asp:Literal></h2>
                        <h3>
                            <span>Posted on:
                                <asp:Literal ID="ltrJobPostedDate" runat="server"></asp:Literal></span> | <span>
                                <asp:Literal ID="ltrJobProfileType_location" runat="server"></asp:Literal></span></h3>
                        <div class="job_description_links">
                            <script type="text/javascript">
                                function myPrintFunction() {
                                    window.print();
                                }
                            </script>
                            <ul class="ps-btns">
                                <li class="print-btn"><a onclick="myPrintFunction();"><span>Print</span></a></li>
                                <li class="email-btn"><a><span class="st_email"></span></a></li>
                                <li class="share-btn"><a><span class="st_sharethis_custom">Share</span></a></li>
                            </ul>
                        </div>
                        <!-- end job_description_links -->
                    </div>
                    <div class="grid_24 job_details">
                        <div class="grid_16 description_details">
                            <asp:Literal ID="ltrJobDescription" runat="server"></asp:Literal>                            
                            <a class="cta-button-text" id="applyJobLink" runat="server" href="/apply-to-job/">
                                <div class="cta-button-wrap black-btn">
                                    <span>Apply to this Job</span>
                                </div>
                            </a>
                        </div>
                        <div class="grid_6 prefix_2 description_sidebar">
                            <%--<img src="http://maps.googleapis.com/maps/api/staticmap?center=33.976303,-118.037004&zoom=15&size=312x240&markers=icon:http://goo.gl/3NrQ1C|33.976303,-118.037004&sensor=false&key=AIzaSyA3TlW7oOcFVQwkJwL9kHxc6wyd2IdBrQ0"
                                alt="my location">--%>
                            <div id="job_location_map_wrapper">
                                <img id="googleMapImage" runat="server" src="http://maps.googleapis.com/maps/api/staticmap?size=312x312&zoom=15&markers=icon:http://author.pip.com/images/location-map-marker.png%7C33.976463,-118.036812&style=feature:landscape%7Ccolor:0xe9e9e9&style=feature:poi%7Celement:geometry%7Ccolor:0xd8d8d8&sensor=true"
                                    alt="placeholder map" />
                            </div>
                            <h3 class="job_description_title">
                                Location</h3>
                            <p><asp:Literal ID="ltrCenterAddress" runat="server"></asp:Literal></p>
                            <h3 class="job_description_title">
                                Contact</h3>
                            <p>
                               <asp:Literal ID="ltrCenterContactName" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltrCenterContactNumber" runat="server"></asp:Literal><br />
                                <asp:Literal ID="ltrCenterEmailAdress" runat="server"></asp:Literal></p>
                            <hr>
                            <h3 class="job_description_title">
                                How to Apply</h3>
                            <p><CMS:ContentBlock ID="cbHowToApplyText" runat="server" DoInitFill="false" /></p>
                            <a class="cta-button-text" id="applyJobLink2" runat="server" href="/apply-to-job/">
                                <div class="cta-button-wrap black-btn">
                                    <span>Apply Now</span>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end job_description_content -->
        </div>
        <!-- end job_description_content_wrapper -->
        <div class="clear">
        </div>
    </div>
</asp:Content>
