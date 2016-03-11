<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageLocalControls.ascx.cs" Inherits="UserControls_HomePageLocalControls" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/HomePageCaseStudies.ascx" TagPrefix="ux" TagName="CaseStudies" %>


<!-- mmm We're Local (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm We're Local (loc) mmm -->
<div class="were_local_wrapper clearfix">
    <div class="were_local clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="grid_14">
                    <h2 class="headline">we're local</h2>
                </div>
                <!--headline-->
                <div class="grid_7 push_3 social_media">
                    <asp:Literal ID="ltrSocialLinks" runat="server"></asp:Literal>
                </div>
                <!--social_media-->
                <div class="clear"></div>
                <!-- mmm Your Location (lcl) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Your Location (lcl) mmm -->
                <div class="location_wrapper">
                    <input type="hidden" value="" id="hiddenCenterLat" class="hiddenCenterLat" runat="server" />
                    <input type="hidden" value="" id="hiddenCenterLong" class="hiddenCenterLong" runat="server" />
                    <div id="location_map">
                        <!-- Google map container -->
                    </div>
                    <!-- #location_map -->
                </div>
                <!-- end your location wrapper -->
                <div class="were_local_section">
                    <asp:Literal ID="ltrDecription" runat="server" />
                </div>
                <!--were_local_section-->
                <div class="divider">/</div>
                <!--divider-->

                <div class="were_local_section">
                    <ul class="local_contact_info">
                        <asp:Literal ID="litLocAddress" runat="server" />
                        <asp:Literal ID="ltrPhoneNumber" runat="server" />
                        <asp:Literal ID="ltrEmailAddress" runat="server" />
                    </ul>
                </div>
                <!--were_local_section-->
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- we're local-->
</div>
<!-- our services wrapper -->
<div class="clear"></div>
<!-- mmm Why Work (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Why Work (loc) mmm -->
<div class="img-holder-why-work" id="why_work" runat="server" data-image="/images/why_work_bg.jpg" data-width="1600" data-height="670"></div>
<div class="why_work clearfix">
    <div class="container_24">
        <div class="grid_24">
            <asp:Literal ID="ltrWorkWithUs" runat="server"></asp:Literal>
        </div>
        <!-- grid 24-->
    </div>
    <!--container 24-->
</div>
<!-- why_work-->
<!--why work wrapper-->
<div class="clear"></div>
<!-- mmm Our Services (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Our Services (loc) mmm -->
<div class="our_services_wrapper local clearfix">
    <div class="our_services clearfix">
        <div class="container_24">
            <div class="grid_24">
                <h2 class="headline">
                    <asp:Literal ID="ltrSectionTitle" runat="server"></asp:Literal></h2>
                <asp:ListView ID="lvOurServices" runat="server">
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <div class="service_section">
                                <a href="<%# Eval("Link") %>">
                                    <img src="<%# Eval("ImagePath") %>" alt="" />                               
                                    <h3><%# Eval("Title") %></h3>
                                </a>
                                <p><%# Eval("SubTitle") %></p>
                            </div>
                            <!--service_section-->
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- our services-->
</div>
<!-- our services wrapper -->
<div class="clear"></div>
<!-- mmm Briefs Whitepapers (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Briefs Whitepapers (loc) mmm -->
<div class="img-holder-briefs-local" id="briefs_whitepapers" runat="server" data-image="/images/briefs_whitepapers_bg.jpg" data-width="1600" data-height="450"></div>
<div class="briefs_whitepapers clearfix">
    <div class="container_24">
        <div class="grid_12">
            <h2 class="headline">read the latest insights</h2>
            <div class="blog">
                <a id="blogsLink" runat="server" href="http://www.itinflections.com/" target="_blank">
                    <CMS:ContentBlock ID="cbITInflectionImg" runat="server" SuppressWrapperTags="true" DoInitFill="false" />
                </a>
            </div>
            <!--blog-->
        </div>
        <!-- grid 12-->
        <div class="grid_12">
            <div class="blog">
                <asp:ListView ID="lvITInflectionRssFeed" runat="server">
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <span class="blog_date"><%# Eval("PostDate") %></span>
                            <span class="blog_title"><a target="_blank" href="<%# Eval("MoreLink") %>"><%# Eval("Title") %></a></span>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <!--blog-->
        </div>
        <!-- grid 12-->
    </div>
    <!--container 24-->
</div>
<!-- blog_whitepapers-->
<!--blog_whitepapers_wrapper-->
<div class="clear"></div>
<!-- mmm Case Studies (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Case Studies (loc) mmm -->
<div class="case_studies_wrapper">
    <div class="case_studies clearfix">
        <div class="container_24">
            <div class="grid_24">
                <h2 class="headline">case studies</h2>
                <div class="case_studies_block">
                    <ux:CaseStudies ID="uxCaseStudies" runat="server" />
                </div>
                <!--case studies block-->
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- case_studies-->
</div>
<!--case_studies_wrapper-->
<div class="clear"></div>
<!-- mmm Let's Connect (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Let's Connect (loc) mmm -->
<div class="img-holder-connect" runat="server" id="lets_connect" data-image="../images/lets_connect_bg.jpg" data-width="1600" data-height="670"></div>
<div class="lets_connect_local clearfix">
    <div class="lets_connect_content">
        <div class="container_24">
            <div class="grid_24">
                <div class="grid_12 left">
                    <div class="caption">
                        <CMS:ContentBlock ID="cbLetsConnect" runat="server" SuppressWrapperTags="true" DoInitFill="false" />
                    </div>
                    <!--let's connect content-->
                </div>
                <!-- grid -->
                <div class="prefix_4 grid_8 right">
                    <div class="request_button"><a class="request_call" href="#">Request a Call from our team</a></div>
                    <p>or call
                        <br>
                       <asp:Literal ID="ltrPhoneNumber2" runat="server"></asp:Literal></p>
                </div>
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- lets_connect_content -->
</div>
<!-- lets_connect_wrapper-->
<!--lets_connect_wrapper-->
<div class="clear"></div>

