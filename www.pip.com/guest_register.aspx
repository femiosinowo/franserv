<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="guest_register.aspx.cs" Inherits="guest_register" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/GuestRegisterNational.ascx" TagPrefix="ux" TagName="GuestRegisterNational" %>
<%@ Register Src="~/UserControls/GuestRegisterLocal.ascx" TagPrefix="ux" TagName="GuestRegisterLocal" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
    <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
    <div class="header_image_wrapper clearfix">
        <div class="header_image">
            <div class="header_image_content">
               <!-- <img src="/images/headers/header_3.jpg" alt="Send a File"> -->
                <CMS:ContentBlock ID="cbHeader" runat="server" DynamicParameter="id" CacheInterval="600" />
            </div>
            <!-- header image-->
        </div>
        <!-- end header_image -->
    </div>
    <!-- end header_image_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Register Content (Local) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Register Content (Local) mmm -->
    <div class="guest_register_content_wrapper clearfix">
        <div class="guest_register_content send_file clearfix main_content">
            <div class="container_24">
               <ux:GuestRegisterNational ID="uxNationalRegister" runat="server" />
               <ux:GuestRegisterLocal ID="uxLocalRegister" runat="server" />
            </div>
            <!-- end container_24  -->
        </div>
        <!--end guest_register_content -->
    </div>
    <!-- end guest_register_content_wrapper -->
    <div class="clear">
    </div>
</asp:Content>

