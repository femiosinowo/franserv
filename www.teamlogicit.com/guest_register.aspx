<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="guest_register.aspx.cs" Inherits="guest_register" %>
<%@ Register Src="~/UserControls/GuestRegisterNational.ascx" TagPrefix="ux" TagName="GuestRegisterNational" %>
<%@ Register Src="~/UserControls/GuestRegisterLocal.ascx" TagPrefix="ux" TagName="GuestRegisterLocal" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" Runat="Server">
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

