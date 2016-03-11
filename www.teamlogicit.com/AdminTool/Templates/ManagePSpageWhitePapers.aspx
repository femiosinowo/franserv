<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="ManagePSpageWhitePapers.aspx.cs" Inherits="AdminTool_Templates_ManagePSpageWhitePapers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //add Briefs and white papers scripts  
        $(function () {
            $('.btnBWP').click(function (e) {
                var status = false;
                var selectedNews = $(".selectedbwp");

                var jq = $([1]);
                var ids = '';
                for (var i = 0; i < selectedNews.length; i++) {
                    jq.context = jq[0] = selectedNews[i];
                    var html = jq.html();
                    if (html != '') {
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }
                }
                $('.hddnSelectedBWP').val(ids);

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
    <h3>Manage Briefs & White Papers content for Product & Services Page</h3>
    <asp:Panel ID="pnlBWP" runat="server">
        <div id="drag">
            <p class="note">Drag & drop the Briefs and White Papers content from left column to right column.</p>
            <asp:Label ID="lblError" runat="server" CssClass="errorMessage"></asp:Label>
            <div class="dragSection">
                <table>
                    <colgroup>
                        <col width="240" />
                        <col width="240" />
                    </colgroup>
                    <tr>
                        <th colspan="1" class="mark" title="Message line">Available Briefs & White Papers</th>
                        <th colspan="1" class="mark" title="Message line">Selected Briefs & White Papers</th>
                        <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                    </tr>
                    <asp:ListView ID="lvBWP" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="availbwp"><%#Eval("Availablebwp") %></td>
                                <td class="selectedbwp"><%#Eval("Selectedbwp") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
            </div>
            <br />
            <asp:Button ID="btnBWP" CssClass="btnBWP" runat="server" Text="Continue" OnClick="btnBWP_Click" />
            <input type="hidden" id="hddnSelectedBWP" class="hddnSelectedBWP" runat="server" value="" />
            <div class="errorMessage" style="display: none;">
                <p>Please select atleast One Briefs & White Papers!!!</p>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAddPromoMsg" runat="server" Visible="false">
        <p>Briefs & White Papers saved Successfully.</p>
    </asp:Panel>
    <asp:Panel ID="pnlNoData" runat="server" Visible="false">
        <p>No Briefs & White Papers are available for the Center.</p>
    </asp:Panel>
    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
    <br />
    <br />
</asp:Content>

