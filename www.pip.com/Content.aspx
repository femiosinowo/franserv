<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="Content" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>
<%@ MasterType VirtualPath="/MasterPages/Main.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainContent" Runat="Server">
     <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix portfolio">
            <div class="container_24">
                <div class="grid_24">
                     <cms:ContentBlock ID="cb1" runat="server" DoInitFill="false" DynamicParameter="id" />
                     <cms:FormBlock ID="fb1" runat="server" DynamicParameter="ekfrm" />
                </div>
            </div>
        </div>
    </div>   
</asp:Content>