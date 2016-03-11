<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeLocal.ascx.cs"
    Inherits="UserControls_subscribeLocal" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<style type="text/css">
    .SubscribeChkType {
        float: left;
    }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $("#subscribe_lb").fancybox({
            parent: "form:first",
            fitToView: true,
            openEffect: 'none',
            closeEffect: 'none',
            modal: false
        });
    });
</script>
<!-- <div id="subscribe_wrapper" class="clearfix prefix_1 grid_22 suffix_1"> -->
<div id="subscribe_wrapper" class="clearfix">
    <div id="subscribe_content">
        <img width="160" height="85" alt="Sir Speedy" src="/images/logo.png" />
        <hr />
        <cms:ContentBlock ID="cbSubscribe" runat="server" DoInitFill="false" />        
        <asp:Panel ID="pnlSubscribeForm" runat="server">
            <div class="form" id="form_subscribe">
                <h2>Online</h2>
                <asp:CheckBoxList CssClass="SubscribeChkType" ID="chkOnLineTypeList" runat="server">
                    <asp:ListItem Text="Newsletter (monthly)" Value="Newsletter (monthly)"></asp:ListItem>
                    <asp:ListItem Text="Products & Offers (occasionally)" Value="Products & Offers (occasionally)"></asp:ListItem>
                </asp:CheckBoxList>                
                <p>
                    <asp:TextBox ID="txtEmail" TextMode="Email" placeholder="Email Address" required="required" runat="server"></asp:TextBox>                    
                </p>
                <hr />
                <h2>Print</h2>
                <asp:CheckBoxList ID="chkPrintTypeList" CssClass="SubscribeChkType" runat="server">
                    <asp:ListItem Text="Newsletter (monthly)" Value="Newsletter (monthly)"></asp:ListItem>
                    <asp:ListItem Text="Products & Offers (occasionally)" Value="Products & Offers (occasionally)"></asp:ListItem>
                </asp:CheckBoxList>                
                <p>
                    <asp:TextBox ID="txtFirstName" placeholder="First Name" required="required" runat="server"></asp:TextBox>                   
                </p>
                <p>
                    <asp:TextBox ID="txtLastName" placeholder="Last Name" required="required" runat="server"></asp:TextBox>                    
                </p>
                <p>
                    <asp:TextBox ID="txtAddress" placeholder="Address" required="required" runat="server"></asp:TextBox>                    
                </p>
                <p>
                    <asp:TextBox ID="txtCity" placeholder="City" required="required" runat="server"></asp:TextBox>                   
                </p>
                <p>
                    <asp:TextBox ID="txtState" placeholder="State" required="required" runat="server"></asp:TextBox>                    
                </p>
                <p>
                    <asp:TextBox ID="txtSubscribeZipCode" placeholder="Zip Code" required="required" runat="server"></asp:TextBox>                    
                </p>
                <div class="clear">
                </div>
                <hr />
                <asp:Button ID="btnSubscribeFooter" runat="server" Text="Subscribe" OnClick="btnSubscribeFrmFooter_Click" />
            </div>
        </asp:Panel>
    </div>
    <!-- end subscribe_content -->
</div>
<!-- end subscribe_wrapper -->
