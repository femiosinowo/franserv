<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" ValidateRequest="false"
    CodeFile="job_index.aspx.cs" Inherits="job_index" %>
<%@ Register Src="~/UserControls/newJobSearch.ascx" TagPrefix="ux" TagName="JobSearch" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <!-- mmm Join Us Today (shared) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Join Us Today (shared) mmm -->
    <div class="jobs_join_us clearfix">
        <div class="container_24">
            <div class="grid_24">                
               <CMS:ContentBlock ID="cbJoinToday" runat="server" DoInitFill="false" />
            </div>
            <!--//grid_24-->
        </div>
        <!--container 24-->
    </div>
    <!--jobs_join_us-->
    <div class="clear"></div>

    <link href="/css/jquery.datatable.css" rel="stylesheet" />
   <script type="text/javascript"  src="/js/moment.min.js"></script>
   <script type="text/javascript"  src="/js/datetime-moment.js"></script>

    <ux:JobSearch ID="uxJobSearch1" runat="server" DisplayLocalJobs="True" />
    <ux:JobSearch ID="uxJobSearch2" runat="server" DisplayLocalJobs="False" />
    <div class="clear"></div>
    <!-- mmm Job Profiles (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Job Profiles (loc) mmm -->
    <div id="view_profiles" class="job_profiles_wrapper clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="grid_24">
                    <div class="search_header">
                        <h2 class="headline">job profiles</h2>
                        <div class="grid_8 job_profiles">
                           <CMS:ContentBlock ID="cbJobProfile" runat="server" DoInitFill="false" />
                        </div>
                        <div class="job_profiles_grid grid_15 push_1">
                            <asp:ListView ID="lvProfileTabs" runat="server">
                                <LayoutTemplate>                                   
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>                                  
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="job_profiles_box" style='<%# bool.Parse(Eval("ShowContent").ToString()) == true ? "display:table" : "display:none;" %>'>
                                        <div class="centered">
                                            <a class="scroll" href="#<%# Eval("AnchorName") %>">
                                                <img src="<%# Eval("SmallImage") %>" alt="" />
                                                <h3><%# Eval("TypeName") %></h3>
                                            </a>
                                        </div>
                                        <!--//.centered-->
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>                           
                            <!--//.job_profiles_box-->
                        </div>
                        <!--//.job_profiles-->
                    </div>
                    <!--//search_header-->
                </div>
                <!--//grid_24-->
            </div>
        </div>
        <!--container 24-->
    </div>
    <!--job_profiles-->    
    <asp:ListView ID="lvProfileContent" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="clear"></div> 
            <div id="<%# Eval("AnchorName") %>_wrapper" style='<%# bool.Parse(Eval("ShowContent").ToString()) == true ? "display:block" : "display:none;" %>' class="<%# bool.Parse(Eval("ShowContent").ToString()) == true ? "img_holder_wrapper" : "" %>">
                <div class="<%# bool.Parse(Eval("ShowContent").ToString()) == true ? "img-holder-jobs" : "" %>" id="<%# Eval("AnchorName") %>" data-image="<%# Eval("BackGroundImage") %>" data-image-mobile="<%# Eval("BackGroundImage") %>" data-width="1600" data-height="475"></div>
                <div class="profile_wrapper clearfix">
                    <div class="profile_content clearfix">
                        <div class="container_24">
                            <div class="grid_24">
                                <div class="grid_12">
                                    <div class="caption">
                                        <h2><%# Eval("TypeName") %></h2>
                                    </div>
                                    <!--//.caption-->
                                </div>
                                <div class="grid_12 graphic">
                                    <img src="<%# Eval("LargeImage") %>" alt="" />
                                </div>
                            </div>
                            <!-- grid 24-->
                        </div>
                        <!--container 24-->
                    </div>
                    <!--profile_content-->
                    <div class="clear"></div>
                    <div class="container_24 roles">
                        <div class="grid_24 CustomOrderedList job_profiles">
                            <p class="intro_text"><%# Eval("Description") %></p>
                        </div>
                        <!-- grid 24-->
                    </div>
                    <!--container 24
                    <div class="container_24">
                        <div class="grid_24">
                            <div class="tab_down">
                                <a class="scroll" href="#sales">
                                    <img src="/images/tab_down_arrow.png" alt="tab_down_arrow" /></a>
                            </div>
                        </div>
                    </div>-->
                </div>
            </div>
            <div class="clear"></div> 
        </ItemTemplate>
    </asp:ListView> 
</asp:Content>

