<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="briefs_whitepapers_details.aspx.cs" Inherits="briefs_whitepapers_details" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="briefs-whitepapers">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <CMS:ContentBlock ID="cbBriefWhitepapersDetailsHeaderImage" runat="server" />
                    <%--<img src="images/headers/header_2.jpg" alt="">--%>
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
            <div class="sub_navigation insights">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="insights-menu-h2">
                            <h2 id="menu-case-studies">
                                Case Studies</h2>
                            <h2 id="menu-briefs-whitepapers">
                                Briefs &amp; Whitepapers</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                                &nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us National -->
                        <ul id="insights-desktop-nav">
                            <li class="case-studies-link"><a href="/Case-Studies/">Case Studies</a></li>
                            <li class="briefs-whitepapers-link active"><a href="/Briefs-Whitepapers/">Briefs
                                &amp; Whitepapers</a></li>
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
        <!-- mmm Insights -- Briefs & Whitepapers Details Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Insights -- Briefs & Whitepapers Details  Content mmm -->
        <div class="briefs_whitepapers_content_wrapper insights clearfix" style="background-color: white;">
            <div class="briefs_whitepapers_content clearfix">
                <div class="container_24" id="briefs_wps_content">
                    <div class="grid_6 headline-block">
                        <div class="int-headline-block int-block-grey">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <h2 class="headline">
                                        Get Your Free Download</h2>
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                        <div class="int-headline-block int-block-blue-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <div id="brief_form_body" class="clearfix">
                                        <div class="form" id="download_brief">
                                            <asp:TextBox ID="fname" runat="server" required="required" placeholder="First Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="fname" CssClass="required"
                                                ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="lname" required="required" runat="server" placeholder="Last Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="lname" CssClass="required"
                                                ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="emailAdd" required="required" runat="server" placeholder="Email"
                                                TextMode="Email"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="emailAdd"
                                                CssClass="required" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="url" required="required" runat="server" placeholder="Website URL"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv4" runat="server" ControlToValidate="url" CssClass="required"
                                                ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="zipcode" required="required" runat="server" placeholder="Zip Code"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="zipcode" CssClass="required"
                                                ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <div class="clear">
                                            </div>
                                            <div id="select_group">
                                                <label for="business_mode">
                                                    What is your primary mode of business?</label>
                                                <asp:DropDownList ID="ddlPrimaryBusiness" CssClass="custom-select" runat="server"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Text="- Please Select One -" Value="0" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv5" runat="server" ControlToValidate="ddlPrimaryBusiness"
                                                    CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                <div class="clear">
                                                </div>
                                                <label for="mkt_employee_num">
                                                    Number of Marketing Employees</label>
                                                <asp:DropDownList ID="ddlMarketingEmployee" CssClass="custom-select" runat="server"
                                                    AppendDataBoundItems="true">
                                                    <asp:ListItem Text="- Please Select One -" Value="0" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv6" runat="server" ControlToValidate="ddlMarketingEmployee"
                                                    CssClass="required" InitialValue="0" ValidationGroup="Whitepapers" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                <div class="clear">
                                                </div>
                                            </div>
                                            <!-- end select_group -->
                                            <asp:Button ID="download_now" runat="server" Text="Download Now" OnClick="btnDownloadNow_Click"
                                                ValidationGroup="Whitepapers" />
                                        </div>
                                    </div>
                                    <!-- end brief_form_body -->
                                </div>
                                <!--headline content inner-->
                            </div>
                            <!--headline content outer-->
                        </div>
                    </div>
                    <!--headline block-->
                    <div class="grid_18 content-block">
                        <div class="prefix_1 grid_22 suffix_1 briefs_wps_details_article clearfix">
                            <!-- mmmmmmmmmmmmmmmmmmmm briefs_wps ARTICLE mmmmmmmmmmmmmmmmmmmm -->
                            <asp:Repeater runat="server" ID="UxBriefsWhitepapers">
                                <ItemTemplate>
                                    <div class="grid_17 briefs_wps_details">
                                        <h3>
                                            <%#Eval("title") %></h3>
                                        <div class="briefs_wps_links">
                                            <script type="text/javascript">
                                                function myPrintFunction() {
                                                    window.print();
                                                }
                                            </script>
                                            <ul class="ps-btns">
                                                <li class="print-btn"><a onclick="myPrintFunction();" href="javascript:void('0')"><span>Print</span></a></li>
                                                <li class="email-btn"><a href="javascript:void('0')"><span class="st_email"></span></a></li>
                                                <li class="share-btn"><a href="javascript:void('0')"><span class="st_sharethis_custom">Share</span></a></li>
                                            </ul>
                                            <div class="clear">
                                            </div>
                                            <h4>
                                                <%#Eval("abstract") %></h4>
                                            <div>
                                                <%#Eval("content") %></div>
                                        </div>
                                        <!-- end briefs_wps_links -->
                                    </div>
                                    <!-- end briefs_wps_details -->
                                    <div class="grid_7 briefs_wps_img">
                                        <img src="<%#Eval("imgSRC") %>" alt="<%#Eval("title") %>">
                                    </div>
                                    <div class="clear">
                                    </div>
                                    <div class="grid_24 briefs_wps_note clearfix">
                                        <span>Download your free guide</span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <!--end grid_18-->
                </div>
                <!--end container_24-->
            </div>
            <!--end insights_briefs_details -->
        </div>
        <!-- end iinsights_briefs_details_wrapper -->
        <div class="clear">
        </div>
    </div>
    <asp:HiddenField ID="hddnWhitePaperTitle" runat="server" Value="" />
</asp:Content>
