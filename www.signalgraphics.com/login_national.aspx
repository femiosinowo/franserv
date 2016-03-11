<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="login_national.aspx.cs" Inherits="login_national" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Login Content (National) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Login Content (National) mmm -->
    <div class="login_content_wrapper clearfix">
        <div class="login_content clearfix send_file main_content">
            <div class="container_24">
                <div class="grid_24 clearfix">
                    <div class="grid_16 sf_col two_col col1">
                        <h2 class="header">Send a File</h2>
                        <div class="prefix_1 grid_22 suffix_1 alpha">
                            <CMS:ContentBlock ID="cbTagLine" runat="server" DynamicParameter="id" CacheInterval="300" />
                            <hr />
                            <div class="form" id="contact_project_info">
                            <h3>
                                Upload Files</h3>
                            <p class="small_text gray">
                                Upload files from:</p>
                            <div class="upload_files clearfix">
                                <div class="grid_9 alpha logo">
                                    <a href="#">
                                        <img src="/images/upload-logo-dropbox.png" alt="Dropbox" /></a>
                                </div>
                                <!-- end grid -->
                                <div class="grid_6 logo">
                                    <a href="#">
                                        <img src="/images/upload-logo-box.png" alt="Box" /></a>
                                </div>
                                <!-- end grid -->
                                <div class="grid_9 omega logo">
                                    <a href="#">
                                        <img src="/images/upload-logo-google-drive.png" alt="Google Drive" /></a>
                                </div>
                                <!-- end grid -->
                            </div>
                            <!-- end upload_files -->
                            <p class="small_text gray">
                                Upload files from your computer</p>
                            <div class="upload_files clearfix">
                                <div id="drag_drop_file">
                                    <div class="drag_drop_placeholder">
                                        <h2 class="gray">
                                            Drag &amp; Drop Your Files Here</h2>
                                        <p class="small_text gray">
                                            File size limit: 10MB. | Valid Formats: PDF, DOC, DOCX, RTF, TXT.<br />
                                            You can also <a class="red-text" href="#">browse for a file.</a></p>
                                    </div>
                                    <!-- end drag_drop_placeholder -->
                                </div>
                                <!--- end drag_drop_file -->
                            </div>
                            <!-- end upload_files -->
                            <hr />
                            <h3>
                                Project Description</h3>
                            <div class="sf_project_desc">
                                <input type="text" placeholder="Project Name" id="project_name">
                                <div class="grid_11 suffix_1 form_two_col">
                                    <input type="text" placeholder="Quantity" id="project_quantity">
                                </div>
                                <div class="grid_12 form_two_col">
                                    <input type="text" placeholder="Due Date" id="project_due_date">
                                </div>
                                <textarea id="project_desc"></textarea>
                            </div>
                            <!-- end sf_project_desc -->
                            <input class="purple_btn search" type="submit" id="submit_file" value="Submit" />
                            </div>
                        </div>
                        <!-- end single_col -->
                    </div>
                    <!-- end sf_col 1 -->
                    <div class="grid_8 sf_col two_col col2">
                        <h2 class="header">
                            Your Location</h2>
                        <div class="prefix_2 grid_20 suffix_2">
                            <div id="sf_location_map" class="static_map">
                                <img alt="map" id="googleMapImage" runat="server" src="#" />
                                <a class="view-map red-text fancybox iframe" id="viewDirectionDesktop" runat="server" title="Your Location" href="#">VIEW MAP</a>
                                <!-- Mobile Link: go to google map page on devices smaller than 768px -->
                                <a class="red-text view-map-mobile" id="viewDirectionMobile" runat="server" target="_blank" href="#">VIEW MAP</a>
                            </div>
                            <asp:Literal runat="server" ID="litFranchiseContactInfo"></asp:Literal>                
                            <hr />
                            <div class="sf_social_icons">
                               <ux:SocialIcons ID="uxSocialIcons" runat="server" />
                            </div>
                            <!-- end social_icons -->
                        </div>
                        <!-- end grid -->
                    </div>
                    <!-- sf_col 2 -->
                </div>
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end login_content -->
    </div>
    <!-- end login_content_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
