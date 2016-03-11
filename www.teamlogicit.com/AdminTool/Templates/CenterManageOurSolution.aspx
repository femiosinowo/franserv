<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="CenterManageOurSolution.aspx.cs" Inherits="AdminTool_Templates_CenterManagePromos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //add Banners scripts  
        $(function () {
            $('.btnBanners').click(function (e) {               
                var status = false;
                var selectedBanners = $(".selectedBanners");

                var jq = $([1]);
                var ids = '';
                var count = 6;                

                for (var i = 0; i < selectedBanners.length; i++) {
                    jq.context = jq[0] = selectedBanners[i];
                    var html = jq.html();
                    if (html != '' && count > 0) {
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                        count--;
                    }
                }
                $('.hddnSelectedBanners').val(ids);

                if (status == false)
                    $('.errorMessage').show();
                else
                    $('.errorMessage').hide();

                return status;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
     <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Business Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>    
    <h3>Manage Featured Solutions</h3>
    <br />       
    
<div id="drag">
        <asp:Panel ID="pnlPromotions" runat="server">
            <p class="note">Drag & drop the Featured Solutions content from left column to right column. <br /> The maximum number of Featured Solutions that can be selected is six.</p>           
            <div class="dragSection">
                <p class="errorMessage">
                    <asp:Label ID="lblError" CssClass="errorMessage" runat="server" Visible="false" />
                </p>
                <table>
                    <colgroup>
                        <col width="240" />
                        <col width="240" />
                    </colgroup>
                    <tr>
                        <th colspan="1" class="mark" title="Message line">Available Featured Solutions</th>
                        <th colspan="1" class="mark" title="Message line">Selected Featured Solutions</th>
                        <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                    </tr>
                    <asp:ListView ID="lvBanners" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="availBanners"><%#Eval("AvailableBanner") %></td>
                                <td class="selectedBanners"><%#Eval("SelectedBanner") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
            </div>
            <br />
            <asp:Button ID="btnBanners" CssClass="btnBanners" runat="server" Text="Save" OnClick="btnBanners_Click" />
            <input type="hidden" id="hddnSelectedBanners" class="hddnSelectedBanners" runat="server" value="" />
            <asp:HiddenField ID="hdnCenterId" runat="server" Value="" />
            <br />            
            <div class="errorMessage" style="display: none;">
                        <p>Please select atleast One category!!!</p>
                    </div>
            <div id="bannersUpdateMsg" class="successMessage" runat="server" visible="false">
                Manage Featured Solutions data saved Successfully!!!
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoBannersResults" runat="server" Visible="false">
            <p>No Manage Featured Solutions content are available for the Center.</p>
        </asp:Panel>
    </div>
</asp:Content>

