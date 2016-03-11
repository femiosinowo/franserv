<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Manage-Mega-Menu.aspx.cs" Inherits="AdminTool_Templates_Manage_Mega_Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //script for complete page
        $(document).ready(function () {
            $("#tabs").tabs({activate: function() {
                var selectedTab = $('#tabs').tabs('option', 'active');
                //$("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
            },
                active: <%= hdnSelectedTab.Value %>
                });           
        });

    </script> 
    <script type="text/javascript">
        //add Employees scripts  
        $(function () {             
            $('.btnOurTeam').click(function (e) { 
                var status = false;   
                var selectedNews = $(".selectedEmployees");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedNews.length; i++){
                    jq.context = jq[0] = selectedNews[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedOurTeam').val(ids); 
                
                if(status == false)
                    $('.ourTeamErrorMessage').show();
                else
                    $('.ourTeamErrorMessage').hide();

                return status;
            });
        });
    </script>
    <script type="text/javascript">
        //add Flickr scripts  
        $(function () {             
            $('.btnFlickr').click(function (e) { 
                var status = false;   
                var selectedNews = $(".selectedFlickr");               

                var jq = $([1]);
                var ids = '';
                for(var i=0; i<selectedNews.length; i++){
                    jq.context = jq[0] = selectedNews[i];
                    var html = jq.html();
                    if(html != '')
                    {                       
                        ids = ids + jq.find('span').attr('id') + ',';
                        status = true;
                    }                   
                } 
                $('.hddnSelectedProfilePic').val(ids); 
                
                if(status == false)
                    $('.flickrErrorMessage').show();
                else
                    $('.flickrErrorMessage').hide();

                return status;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" Runat="Server">
    <div>
        <asp:Literal ID="ltrBreadcrumb" runat="server"></asp:Literal>         
        <h3 runat="server">Manage Mega Menu Data:</h3>
        <div id="tabs">
            <ul class="addCenterTabs">               
                <li><a href="#tabs-1">Our Team</a></li>
                <li><a href="#tabs-2">Portfolio via Flickr</a></li>               
            </ul>
            <div id="drag">                
                <div id="tabs-1">
                    <asp:Panel ID="pnlOurTeam" runat="server">
                        <p class="note">Drag & drop the Employees from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Avaliable Employees</th>
                                    <th colspan="1" class="mark" title="Message line">Selected Employees</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvEmployees" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availEmployees"><%#Eval("AvailableEmployees") %></td>
                                            <td class="selectedEmployees"><%#Eval("SelectedEmployees") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnOurTeam" CssClass="btnOurTeam" runat="server" Text="Save" OnClick="btnOurTeam_Click" />
                        <input type="hidden" id="hddnSelectedOurTeam" class="hddnSelectedOurTeam" runat="server" value="" />
                        <div class="ourTeamErrorMessage errorMessage" style="display: none;">
                            <p>Please select atleast One Employee!!!</p>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="noEmployeesInTeam" runat="server" Visible="false">
                        <p>
                            There are no employees found for this Center. Please Add/Create employees through "Manage All Profiles".
                        </p>
                    </asp:Panel>
                </div>
                <div id="tabs-2">
                    <asp:Panel ID="pnlFlickr" runat="server">
                        <p class="note">Drag & drop the Flickr's PhotoSet Cover Image from left column to right column.</p>
                        <div class="dragSection">
                            <table>
                                <colgroup>
                                    <col width="240" />
                                    <col width="240" />
                                </colgroup>
                                <tr>
                                    <th colspan="1" class="mark" title="Message line">Avaliable PhotoSet Images</th>
                                    <th colspan="1" class="mark" title="Message line">Selected PhotoSet Images</th>
                                    <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                                </tr>
                                <asp:ListView ID="lvFlickr" runat="server">
                                    <LayoutTemplate>
                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="availFlickr"><%#Eval("AvailableFlickr") %></td>
                                            <td class="selectedFlickr"><%#Eval("SelectedFlickr") %></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </table>
                        </div>
                        <br />
                        <asp:Button ID="btnFlickr" CssClass="btnFlickr" runat="server" Text="Save" OnClick="btnFlickr_Click" />
                        <div id="m3DataSaveMsg" runat="server" class="successMessage" visible="false">Mega Menu Data Saved!!!</div>
                         <input type="hidden" id="hddnSelectedProfilePic" class="hddnSelectedProfilePic" runat="server" value="" />
                        <div class="flickrErrorMessage errorMessage" style="display: none;">
                            <p>Please select atleast One PhotoSet Image!!!</p>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNoThirdPartyData" runat="server" Visible="false">
                        <p>No Portfolio via Flickr User Id found. Please go to Edit Center and update the Third Party data.</p>
                    </asp:Panel>
                </div>
                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            </div>
        </div>
 </div>
</asp:Content>

