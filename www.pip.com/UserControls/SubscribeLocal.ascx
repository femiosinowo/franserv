<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubscribeLocal.ascx.cs"
    Inherits="UserControls_subscribeLocal" %>
<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<style type="text/css">
    .SubscribeChkType {
        float: left;
    }
</style>
<script type="text/javascript">
    function ValidateOnlineTypeList(source, args) {
        var chkListModules = document.getElementById('<%= chkOnLineTypeList.ClientID %>');
        var chkListinputs = chkListModules.getElementsByTagName("input");
        for (var i = 0; i < chkListinputs.length; i++) {
            if (chkListinputs[i].checked) {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }

    function ValidatePrintTypeList(source, args) {
        var chkListModules = document.getElementById('<%= chkPrintTypeList.ClientID %>');
        var chkListinputs = chkListModules.getElementsByTagName("input");
        for (var i = 0; i < chkListinputs.length; i++) {
            if (chkListinputs[i].checked) {
                args.IsValid = true;
                return;
            }
        }
        args.IsValid = false;
    }

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
        <cms:ContentBlock ID="cbLogo" runat="server" DoInitFill="false" />
        <hr />
       <cms:ContentBlock ID="cbSubscribe" runat="server" DoInitFill="false" />
        <asp:Panel ID="pnlSubscribeForm" DefaultButton="btnSubscribe" runat="server">
            <div class="form" id="form_subscribe">
                <h2>Online</h2>
                <asp:CheckBoxList CssClass="SubscribeChkType" ID="chkOnLineTypeList" runat="server">
                    <asp:ListItem Text="Newsletter (monthly)" Value="Newsletter (monthly)"></asp:ListItem>
                    <asp:ListItem Text="Products & Offers (occasionally)" Value="Products & Offers (occasionally)"></asp:ListItem>
                </asp:CheckBoxList>
                <asp:CustomValidator Style="float: left;" runat="server" ID="CustomValidator1" ClientValidationFunction="ValidateOnlineTypeList"
                    ErrorMessage="*" CssClass="errorMessage" ValidationGroup="SubscribeFrm" ForeColor="Red"></asp:CustomValidator>
                <p>
                    <asp:TextBox ID="txtEmail" TextMode="Email" placeholder="Email Address" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="errorMessage" ID="requiredFieldValidator2" runat="server" ControlToValidate="txtEmail"
                        ValidationGroup="SubscribeFrm" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <hr />
                <h2>Print</h2>
                <asp:CheckBoxList ID="chkPrintTypeList" CssClass="SubscribeChkType" runat="server">
                    <asp:ListItem Text="Newsletter (monthly)" Value="Newsletter (monthly)"></asp:ListItem>
                    <asp:ListItem Text="Products & Offers (occasionally)" Value="Products & Offers (occasionally)"></asp:ListItem>
                </asp:CheckBoxList>
                <asp:CustomValidator Style="float: left;" runat="server" ID="CustomValidator2" ClientValidationFunction="ValidatePrintTypeList"
                    ErrorMessage="*" CssClass="errorMessage" ValidationGroup="SubscribeFrm" ForeColor="Red"></asp:CustomValidator>
                <p>
                    <asp:TextBox ID="txtFirstName" placeholder="First Name" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator4" runat="server" ControlToValidate="txtFirstName"
                        ValidationGroup="SubscribeFrm" CssClass="errorMessage" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:TextBox ID="txtLastName" placeholder="Last Name" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator5" runat="server" ControlToValidate="txtLastName"
                        ValidationGroup="SubscribeFrm" CssClass="errorMessage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:TextBox ID="txtAddress" placeholder="Address" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator6" runat="server" ControlToValidate="txtAddress"
                        ValidationGroup="SubscribeFrm" ForeColor="Red" CssClass="errorMessage" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:TextBox ID="txtCity" placeholder="City" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator7" runat="server" ControlToValidate="txtCity"
                        ValidationGroup="SubscribeFrm" ForeColor="Red" CssClass="errorMessage" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:TextBox ID="txtState" placeholder="State" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator8" runat="server" ControlToValidate="txtState"
                        ValidationGroup="SubscribeFrm" ForeColor="Red" CssClass="errorMessage" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:TextBox ID="txtZipCode" placeholder="Zip Code" required="required" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredFieldValidator9" runat="server" ControlToValidate="txtZipCode"
                        ValidationGroup="SubscribeFrm" ForeColor="Red" CssClass="errorMessage" ErrorMessage="*"></asp:RequiredFieldValidator>
                </p>
                <div class="clear">
                </div>
                <hr />
                <asp:Button ID="btnSubscribe" runat="server" Text="Subscribe" ValidationGroup="SubscribeFrm" OnClick="btnSubscribeFrm_Click" />
            </div>
        </asp:Panel>
    </div>
    <!-- end subscribe_content -->
</div>
<!-- end subscribe_wrapper -->
