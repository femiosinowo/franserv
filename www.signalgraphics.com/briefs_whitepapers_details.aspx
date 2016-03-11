<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers_details.aspx.cs" Inherits="briefs_whitepapers_details" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">   
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline briefs_wp">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Briefs & Whitepapers -->
                     <CMS:ContentBlock ID="cbTagLine" runat="server" DefaultContentID="1196" CacheInterval="300" />                    
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation briefs_wp">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Insights -->
                    <ul id="insights-desktop-nav">
                        <li class="case-studies-link"><a href="/case-studies/">Case Studies</a></li>
                        <li class="briefs-wp-link active"><a href="/briefs-whitepapers/">Briefs &amp; Whitepapers</a></li>
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
    <!-- mmm Insights -- Briefs & Whitepapers Details Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Briefs & Whitepapers Details  Content mmm -->
    <div class="insights_briefs_details_wrapper  clearfix">
        <div class="insights_briefs_details main_content clearfix briefs_wp">
            <div class="container_24">
                <div class="grid_24">
                    <div class="grid_15 suffix_1 alpha" id="brief_detail_content">
                        <asp:Repeater runat="server" ID="UxBriefsWhitepapers">
                            <ItemTemplate>
                                <h2 class="bottom-divider">
                                    <%#Eval("title") %></h2>
                                <div class="clear">
                                </div>
                                <div class="grid_14 suffix_1 brief_desc alpha">
                                    <p class="tagline_inner">
                                        <%#Eval("abstract") %></p>
                                    <div>
                                        <%#Eval("content") %></div>
                                </div>
                                <!-- end brief_desc -->
                                <div class="grid_9 brief_img omega">
                                    <img src="<%#Eval("imgSRC") %>" alt="cover title">
                                </div>
                                <!-- end brief_img -->
                                <div class="clear">
                                </div>
                                <div class="bottom-divider">
                                    &nbsp;</div>
                                <!-- end bottom-divider -->
                                <script type="text/javascript">
                                    function myPrintFunction() {
                                        window.print();
                                    }
                                </script>
                                <div class="email_print_share white">
                                    <ul>
                                        <li><a onclick="myPrintFunction();">
                                            <img alt="Print" src="/images/doc-print-white.png"></a></li>
                                        <li><span class='st_email'></span><%--
                                            <asp:LinkButton runat="server" ID="email_btn" OnClick="EmailBtn_Click">
                                            <img alt="Email" src="/images/doc-email-white.png"></asp:LinkButton>--%></li>
                                        <li><span class="st_sharethis_custom"></span>
                                            <%--<a href="#">
                                            <img alt="Share" src="/images/doc-share-white.png"></a>--%></li>
                                    </ul>
                                </div>
                                <%--<div class="email_print_share">
                                    <ul>
                                        <li><a href="#">
                                            <img alt="Print" src="/images/doc-print-white.png"></a></li>
                                        <li><a href="#">
                                            <img alt="Email" src="/images/doc-email-white.png"></a></li>
                                        <li><a href="#">
                                            <img alt="Share" src="/images/doc-share-white.png"></a></li>
                                    </ul>
                                </div>--%>
                                <!-- end email_print_share white -->
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <!-- end brief_detail_content -->
                    <div class="grid_8 omega" id="brief_detail_form">
                        <div id="brief_form_container">
                            <h2>
                                Get Your Free Download</h2>
                            <div id="brief_form_body" class="clearfix">
                                <div class="form" id="download_brief">
                                    <asp:TextBox ID="fname" runat="server" required="required" placeholder="First Name*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="fname"
                                            CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="lname" required="required" runat="server" placeholder="Last Name*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="lname"
                                            CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="emailAdd" required="required" runat="server" placeholder="Email*" TextMode="Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="emailAdd"
                                            CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="url" required="required" runat="server" placeholder="Website URL*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="url"
                                            CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                     <asp:TextBox ID="txtZipCode" required="required" runat="server" placeholder="ZipCode*"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtZipCode"
                                            CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <div class="clear">
                                    </div>
                                    <%--<input type="checkbox" value="1" id="subscribe" />
                                    <label class="small-text" for="subscribe">
                                        Subscribe me to Marketing Tango</label>
                                    <div class="clear">
                                    </div>--%>
                                    <div id="select_group">
                                        <label for="business_mode">
                                            What is your primary mode of business?*</label>
                                         <asp:DropDownList ID="ddlPrimaryBusiness" CssClass="custom-select" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="- Please Select One -" Value="0" />
                                         </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlPrimaryBusiness"
                                            CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <div class="clear">
                                        </div>
                                        <label for="mkt_employee_num">
                                            Number of Marketing Employees*</label>
                                        <asp:DropDownList ID="ddlMarketingEmployee" CssClass="custom-select" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Text="- Please Select One -" Value="0" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="ddlMarketingEmployee"
                                            CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                       <div class="clear"></div>
                                    </div>
                                    <!-- end select_group -->
                                    <asp:Button ID="download_now" runat="server" Text="Download Now" OnClick="btnDownloadNow_Click" ValidationGroup="Whitepapers"  />
                                </div>
                            </div>
                            <!-- end brief_form_body -->
                        </div>
                        <!-- end brief_form_container -->
                    </div>
                    <!-- end brief_detail_content -->
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!--end insights_briefs_details -->
    </div>
    <!-- end iinsights_briefs_details_wrapper -->
    <div class="clear">
    </div>
    <asp:HiddenField ID="hddnWhitePaperTitle" runat="server" Value="" />
</asp:Content>
