<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageBanner.ascx.cs" Inherits="UserControls_PageBanner" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Panel ID="pnlPageBanner" Visible="false" runat="server">
    <div class="clearfix" role="main" id="main">
        <asp:Literal ID="bgImage" runat="server"></asp:Literal>     
        <div class="main_header_text clearfix">
            <div class="caption">
                <asp:Literal ID="title" runat="server"></asp:Literal>           
                <div class="clear_text"></div>
                <asp:Literal ID="desc" runat="server"></asp:Literal>
                <asp:Panel ID="pnlShare" runat="server" Visible="false">
                    <div class="print_share_links">
                        <ul class="print_share_btns">
                            <li class="print-btn"><a class="print" href="javascript:void('0')"><span>Print</span></a></li>
                            <li class="email-btn"><a href="javascript:void('0')"><span class='st_email'>Email</span></a></li>
                            <li class="share-btn"><a href="javascript:void('0')"><span class="st_sharethis_custom">Share</span></a></li>
                        </ul>
                    </div>
                </asp:Panel>
            </div>          
            <div class="layered_image">             
                <asp:Literal ID="bannerImage" Visible="false" runat="server"></asp:Literal>
            </div>
        </div>
    </div>    
</asp:Panel>
<asp:Panel ID="pnlNoBanner" Visible="false" runat="server">
     <div class="clearfix nobanner" role="main" id="mainWithNoBanner">
     </div>
</asp:Panel>
<div class="clear"></div>
<CMS:flexmenu id="mainNav" runat="server" visible="false"
    menudepth="2" suppressaddedit="true" />
 <asp:Literal ID="ltrMenuItems" runat="server" />
