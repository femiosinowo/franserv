<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageNationalControls.ascx.cs" Inherits="UserControls_HomePageNationalControls" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/HomePageCaseStudies.ascx" TagPrefix="ux" TagName="CaseStudies" %>

<!-- mmm Our Services (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Our Services (nat) mmm -->
    <div class="our_services_wrapper clearfix">
        <div class="our_services clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline"><asp:Literal ID="ltrSectionTitle" runat="server"></asp:Literal></h2>
                     <asp:ListView ID="lvOurServices" runat="server">
                        <LayoutTemplate>
                            <ul>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>                                
                            </ul>
                        </LayoutTemplate>
                        <ItemTemplate>                            
                            <li>
                                <div class="service_section">
                                    <a href="<%# Eval("Link") %>"><img src="<%# Eval("ImagePath") %>" alt="" /></a>
                                    <div class="service_section_content">
                                        <a href="<%# Eval("Link") %>">
                                            <h3><%# Eval("Title") %></h3>
                                        </a>
                                        <p><%# Eval("SubTitle") %></p>
                                    </div>
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
    <!-- mmm Why Work (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Why Work (nat) mmm -->
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
    <!-- mmm Find Location (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Find Location (nat) mmm -->
    <div class="find_location_wrapper">
        <div class="home_find_location clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="find_location_form">
                        <CMS:ContentBlock ID="cbFindLocation" runat="server" DoInitFill="false" />
                        <div class="grid_15 prefix_1 alpha">
                            <div class="form">                               
                                <ul>
                                    <li>                                        
                                        <asp:TextBox ID="txtAddress" runat="server" placeholder="City, State, Zip*" ></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtAddress" ForeColor="Red"
                                             ValidationGroup="HomePageFindLocation"></asp:RequiredFieldValidator>
                                    </li>
                                    <li>                                       
                                        <asp:DropDownList CssClass="custom-select" ID="ddlDistance" runat="server">
                                            <asp:ListItem Selected="True" Text="- Choose Distance -" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="ddlDistance" ForeColor="Red"
                                             ValidationGroup="HomePageFindLocation" InitialValue="0" ></asp:RequiredFieldValidator>
                                    </li>
                                    <li class="location_submit">
                                        <div class="square_button" id="find_location_submit">                                           
                                            <asp:Button ID="btnFindLocation" CssClass="btnHomePageFindLocation" ValidationGroup="HomePageFindLocation" runat="server" Text="Search" OnClick="btnFindLocation_Click" />
                                        </div>
                                    </li>
                                </ul>                               
                            </div>
                        </div>
                        <div class="grid_6 prefix_2 omega">
                            <div class="square_button" id="all_locations_button"><a href="/locations/#all_locations">See All Locations</a></div>
                        </div>
                    </div>
                    <!-- .find_location_form -->
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- home_find_location-->
    </div>
    <!--find_location_wrapper-->
    <div class="clear"></div>
    <!-- mmm Case Studies (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Case Studies (nat) mmm -->
    <div class="case_studies_wrapper">
        <div class="case_studies clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="case_studies_block">
                        <h2 class="headline">case studies</h2>
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
    <!-- mmm Briefs Whitepapers (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Briefs Whitepapers (nat) mmm -->
    <div class="img-holder-briefs" id="briefs_whitepapers" runat="server" data-image="/images/briefs_whitepapers_bg.jpg" data-width="1600" data-cover-ratio="50%" data-height="475"></div>
    <div class="homepage briefs_whitepapers clearfix">
        <div class="container_24">
            <div class="grid_12">
                <h2 class="headline">read the latest insights</h2>
                <div class="blog">
                    <a href="http://www.itinflections.com/" target="_blank"><CMS:ContentBlock ID="cbITInflectionImg" runat="server" SuppressWrapperTags="true" DoInitFill="false" /></a>
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
            <div class="grid_12">
                <h2 class="headline">current briefs & whitepapers</h2>
                <asp:ListView ID="lvBriefsWhitePapers" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>   
                        </LayoutTemplate>
                        <ItemTemplate> 
                            <div class="whitepapers_block">
                                <div class="floatLeft">
                                    <h3>/ <%# FormatTitle(Eval("Title").ToString()) %></h3>
                                    <ul>
                                        <li><a href="<%# Eval("Link") %>" class="read_more">Read more</a></li>
                                        <li><a href="javascript:void('0')" class="print_icon print">Print</a></li>
                                        <li><span class='st_email'><a href="javascript:void('0')" class="email_icon">Email</a></span></li>
                                        <li><span class="st_sharethis_custom" st_url="<%# Eval("Link") %>"><a href="javascript:void('0')" class="share_icon">Share</a></span></li>
                                    </ul>
                                </div>
                                <!--floatLeft-->
                                <div class="floatRight">
                                    <img src="<%# Eval("ImagePath") %>" alt="<%# Eval("ImageTitle") %>" />
                                </div>
                                <!--floatRight-->
                            </div>
                            <!--whitepapers block-->
                      </ItemTemplate>
                    </asp:ListView>
            </div>
            <!-- grid 12-->
        </div>
        <!--container 24-->
    </div>
    <!-- blog_whitepapers-->
    <!--blog_whitepapers_wrapper-->
    <div class="clear"></div>
    <!-- mmm Join Team (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Join Team (nat) mmm -->
    <div class="img-holder" id="join_team" runat="server" data-image="/images/join_team_bg.jpg" data-width="1600" data-height="670"></div>
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
    <!--join_team_wrapper-->
    <div class="clear"></div>