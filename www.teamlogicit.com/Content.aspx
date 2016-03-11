<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Content" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="about_us_company_wrapper  clearfix">
        <div class="about_us_company clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24">
                        <div class="grid_24">
                            <cms:ContentBlock ID="cb1" runat="server" DoInitFill="false" DynamicParameter="id" />
                            <cms:FormBlock ID="fb1" runat="server" DynamicParameter="ekfrm" />
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