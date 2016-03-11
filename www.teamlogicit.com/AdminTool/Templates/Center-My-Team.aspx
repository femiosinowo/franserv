<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Center-My-Team.aspx.cs" Inherits="AdminTool_Templates_Center_My_Team" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />    
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //add Banners scripts  
        $(function () {
            $('.btnMyTeam').click(function (e) {
                var status = false;
                var selectedBanners = $(".selectedTeam");

                var jq = $([1]);
                var ids = '';
                for (var i = 0; i < selectedBanners.length; i++) {
                    jq.context = jq[0] = selectedBanners[i];
                    var html = jq.html();
                    if (html != '') {
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }
                }
                $('.hddnSelectedEmployees').val(ids);

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
    <div id="drag">
        <asp:Panel ID="pnlMyTeam" runat="server">
            <div id="centerInfo" runat="server" visible="false">
               <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Business Id: </strong><asp:Label ID="lblCenterId" runat="server" />
            </div>
            <h3>Choose "Our Team" Featured Profiles</h3>
            <p>To feature team members, click on the profile under the "Available Team" column and drag<br />
                and drop the profile onto the "Selected Team" column.&nbsp; Then click Save.&nbsp;&nbsp;</p>
            <p>To change the order in which profiles appear, click on the profile and then move it up or down<br />
                and release the profile at the desired position.&nbsp; Then click Save.</p>
            <p>To add new employees or manage your team members, please click on <a href="/admintool/templates/AllCenterProfiles.aspx">Manage All Profiles</a> . </p>
            <br />
            <asp:Label ID="lblError" CssClass=" errorMessage" runat="server"></asp:Label>
            <div class="dragSection">
                <table>
                    <colgroup>
                        <col width="240" />
                        <col width="240" />
                    </colgroup>
                    <tr>
                        <th colspan="1" class="mark" title="Message line">Avaliable Team</th>
                        <th colspan="1" class="mark" title="Message line">Selected Team</th>
                        <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                    </tr>
                    <asp:ListView ID="lvCenterTeam" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="availTeam"><%#Eval("AvailableTeam") %></td>
                                <td class="selectedTeam"><%#Eval("SelectedTeam") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
            </div>
            <br />
            <asp:Button ID="btnMyTeam" CssClass="btnMyTeam" runat="server" Text="Save" OnClick="btnMyTeam_Click" />
            <input type="hidden" id="hddnSelectedEmployees" class="hddnSelectedEmployees" runat="server" value="" />
            <div class="errorMessage" style="display: none;">
                <p>Please select atleast One Employee!!!</p>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSaveTeamMsg" CssClass=" successMessage" runat="server" Visible="false">
            <p>Team saved Successfully.</p>
        </asp:Panel>
        <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
    </div>
</asp:Content>

