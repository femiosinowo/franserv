<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="case_studies_details.aspx.cs" Inherits="case_studies_details" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="clear">
    </div>
    <div class="client_stories_wrapper resources clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="client_details">
                    <asp:Literal ID="scribIdIframe" runat="server"></asp:Literal>
                </div>
            </div>
            <!--//.grid_24-->
        </div>
        <!--//.container_24-->
    </div>
    <div class="img-holder" runat="server" id="how_we_can_help_img" data-image="../images/how_we_can_help_bkg.jpg" data-width="1600" data-height="670"></div>
    <div class="how_we_can_help clearfix">
        <div class="container_24">
            <div class="grid_24">
                <asp:Literal ID="ltrSupplementOutSourcing" runat="server"></asp:Literal>
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <div class="clear">
    </div>
</asp:Content>
