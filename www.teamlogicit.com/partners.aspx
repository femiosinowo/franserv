<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="partners.aspx.cs" Inherits="partners" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear"></div>
    <!-- mmm Partners (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Partners (nat) mmm -->
    <div class="partners national clearfix">
        <div class="partners_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <CMS:ContentBlock ID="cbIntro" runat="server" DoInitFill="false" />
                </div>
                <!--container 24-->
            </div>
            <!--grid 24-->
            <div id="partners_container">
                <asp:Literal ID="ltrPartners" runat="server"></asp:Literal>
            </div>
            <!--end grid_24-->
        </div>
        <!--partners_content -->
    </div>
    <!-- partners -->
    <div class="clear"></div>
    <!-- mmm How Can We Help You (loc) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm How Can We Help You (loc) mmm -->
    <div id="how_we_can_help_wrapper">
        <div id="how_we_can_help_img" class="img-holder" runat="server" data-image="/images/how_we_can_help_bkg.jpg" data-width="1600" data-height="580"></div>
        <div class="how_we_can_help clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <asp:Literal ID="ltrSupplementOutSourcing" runat="server"></asp:Literal>
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- how_we_can_help-->
    </div>
    <!--here_to_help_wrapper-->
    <div class="clear"></div>    
</asp:Content>
