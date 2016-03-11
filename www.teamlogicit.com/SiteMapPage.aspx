<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="SiteMapPage.aspx.cs" 
    Inherits="SiteMap" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="about_us_company_wrapper  clearfix">
        <div class="about_us_company clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24">
                        <div class="grid_24">    
                            <h2 class="headline sitemap">SITE MAP</h2>                       
                            <asp:Literal ID="ltrSiteMap" runat="server"></asp:Literal>
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

