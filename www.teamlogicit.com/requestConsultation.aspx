<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="requestConsultation.aspx.cs" Inherits="requestConsultation" %>

<%@ Register Src="~/UserControls/RequestConsultationLocal.ascx" TagPrefix="uc1" TagName="RequestConsultationLocal" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" Runat="Server">
        <script>
            $(document).ready(function () {
                $('#requestconsultation_form').show();
            });

</script>
    <style>
        #requestconsultation_form {
             width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPBPageHostPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BannerContentPlaceHolder" Runat="Server">
           <div class="it_our_solutions clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">Request a Consultation</h2>
                </div>
            </div></div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
   
    <uc1:RequestConsultationLocal runat="server" ID="RequestConsultationLocal" />
</asp:Content>

