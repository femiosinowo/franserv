<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="social_register.aspx.cs" Inherits="social_register" %>
<%@ Register Src="~/UserControls/SocialRegisterNational.ascx" TagPrefix="ux" TagName="SocialRegisterNational" %>
<%@ Register Src="~/UserControls/SocialRegisterLocal.ascx" TagPrefix="ux" TagName="SocialRegisterLocal" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">   
    <!-- mmm social Register Content (National) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm social Register Content (National) mmm -->
    <div class="social_register_content_wrapper clearfix">
        <div class="social_register_content send_file clearfix main_content">
            <div class="container_24">
                <ux:SocialRegisterNational ID="uxNationalRegister" runat="server" />
                <ux:SocialRegisterLocal ID="uxLocalRegister" runat="server" />
                <!--end grid 24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end social_register_content -->
    </div>
    <!-- end social_register_content_wrapper -->
    <div class="clear"></div>
</asp:Content>
