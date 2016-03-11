<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="project_experts_index.aspx.cs" Inherits="UserControls_project_experts_index" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="current_projects_wrapper clearfix">
        <div class="current_projects clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="current_projects_content clearfix">
                        <div class="grid_12">
                            <CMS:ContentBlock ID="cbCurrentProject" runat="server" DoInitFill="false" />
                        </div>
                        <div class="current_project_sections grid_12">
                            <asp:ListView ID="lvProjectExpertsIcons" runat="server">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="grid_4">
                                        <div class="centered">
                                            <a class="scroll" href="#<%# Eval("id") %>">
                                                <span>
                                                    <img src="<%# Eval("imgSRC") %>"></span>
                                                <h3 class="headline"><%# Eval("titletxt") %></h3>
                                            </a>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <!--//.current_project_sections-->
                    </div>
                    <!--//.current_projects_content-->
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- .current_projects-->
    </div>
    <!--// .current_projects_wrapper -->
    <div class="clear"></div>
    <asp:ListView ID="lvProjectExpertsDetail" runat="server">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <ItemTemplate>
            <div id="<%# Eval("id") %>" class="img-holder-experts" data-image="<%# Eval("bgimgSRC") %>" data-image-mobile="<%# Eval("bgimgSRC") %>" data-width="1600" data-height="892"></div>
            <div class="network_design_wrapper clearfix">
                <div class="network experts clearfix">
                    <div class="container_24">
                        <div class="grid_24">
                            <div class="network_content grid_12 caption">
                                <%# Eval("titlehtml") %>
                                <div class="clear_text"></div>
                                <div class="square_button">
                                    <a href="<%# Eval("hreftext") %>">Read More</a>
                                </div>
                            </div>
                            <div class="grid_12 description">
                                <h2 class="headline white"><%# Eval("subtitle") %></h2>
                                <h3 class="sub_headline white"><%# Eval("headline") %></h3>
                                <%# Eval("desc") %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <div id="<%# Eval("id") %>" class="img-holder-experts" data-image="<%# Eval("bgimgSRC") %>" data-image-mobile="<%# Eval("bgimgSRC") %>" data-width="1600" data-height="892"></div>
            <div class="upgrades_migrations_wrapper clearfix">
                <div class="upgradesmigrations experts experts_right clearfix">
                    <div class="container_24">
                        <div class="grid_24">                            
                            <div class="upgradesmigrations_content grid_12 caption">
                                <%# Eval("titlehtml") %>
                                <div class="clear_text"></div>
                                <div class="square_button right"><a href="<%# Eval("hreftext") %>">Read More</a> </div>
                            </div>
                            <div class="grid_12 description">
                                <h2 class="headline white"><%# Eval("subtitle") %></h2>
                                <h3 class="sub_headline white"><%# Eval("headline") %></h3>
                                <%# Eval("desc") %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </AlternatingItemTemplate>
    </asp:ListView>
</asp:Content>

