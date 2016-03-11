<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="history.aspx.cs" Inherits="history_national" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
     <div class="about_us_company_wrapper  clearfix">
        <div class="about_us_company clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24">
                        <div class="grid_24">
                            <CMS:ContentBlock ID="cbHistory" runat="server" />
                        </div>
                    </div>
                </div>
                <!-- end grid_24 -->
            </div>
            <!--end grid_24-->
        </div>
        <!--end container_24-->
    </div>
    <div class="clear"></div>
</asp:Content>
