﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="CenterManagePromos.aspx.cs" Inherits="AdminTool_Templates_CenterManagePromos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //add promotional scripts  
        $(document).ready(function () {
            $('#errorMessage').hide();

            $('.btnPromotional').click(function (e) {               
                var status = false;
                var imagesSelected = 0;
                var selectedPromos = $(".selectedPromo");

                var largeImagePromo = $(".selectedPromo").find("a.LargeImage");
                var smallImagePromo = $(".selectedPromo").find("a.SmallImage");
                
                if (largeImagePromo.length > 0 && smallImagePromo.length > 0)
                {
                    $('#errorMessage').show();
                    $('#errorMessage').html('Please Select ONLY one large Image or Two small images');
                    status = false;
                }
                else if (largeImagePromo.length > 1) {
                    $('#errorMessage').show();
                    $('#errorMessage').html('Please Select ONLY One large Image');
                    status = false;
                }
                else if (smallImagePromo.length > 2) {
                    $('#errorMessage').show();
                    $('#errorMessage').html('Please Select ONLY Two Small Images');
                    status = false;
                }
                else if (largeImagePromo.length == 0 && smallImagePromo.length < 2) {
                    $('#errorMessage').show();
                    $('#errorMessage').html('Please Select Two Small Images or One large image');
                    status = false;
                }
                else if (largeImagePromo.length == 1) {
                    $('#errorMessage').hide();
                    imagesSelected = 1;
                    status = true;
                }
                else if (smallImagePromo.length == 2) {
                    $('#errorMessage').hide();
                    imagesSelected = 2;
                    status = true;
                }
                else if (largeImagePromo.length == 0 && smallImagePromo.length == 0) {
                    $('#errorMessage').hide();
                    status = true;
                }

                //var img1Link = $('.Image1Link').val();
                //var img2Link = $('.Image2Link').val();
                //if (imagesSelected == 2)
                //{                    
                //    if (img1Link == '' || img2Link == '') {
                //        $('#errorMessage').show();
                //        $('#errorMessage').html('Please provide the links for the selected Promo images.');
                //        status = false;
                //    }
                //}
                //else if (imagesSelected == 1) {
                //    if (img1Link == '') {
                //        $('#errorMessage').show();
                //        $('#errorMessage').html('Please provide the link for the selected Promo images in the first textbox.');
                //        status = false;
                //    }
                //}

                var jq = $([1]);
                var ids = '';
                if (selectedPromos.length > 0 && status) {
                    for (var i = 0; i < selectedPromos.length; i++) {
                        jq.context = jq[0] = selectedPromos[i];
                        var html = jq.html();
                        if (html != '') {
                            ids = ids + jq.find('span').attr('id') + ',';                           
                        }
                    }
                    $('.hdnSelectedPromos').val(ids);
                }

                return status; //promos are optional
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>
    <h3>Manage Promotions</h3>
    <div id="drag">
        <asp:Panel ID="pnlPromotions" runat="server">
            <p class="note">Drag & drop the Promotions content from left column to right column.</p>
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
                        <th colspan="1" class="mark" title="Message line">Available Promotionals</th>
                        <th colspan="1" class="mark" title="Message line">Selected Promotionals</th>
                        <th style="display: none;" colspan="3" id="message" class="mark" title="Message line">Message line</th>
                    </tr>
                    <asp:ListView ID="lvPromotionals" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="availPromo"><%#Eval("AvailablebPromo") %></td>
                                <td class="selectedPromo"><%#Eval("SelectedPromo") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </table>
                <br />
                <br />
                <table style="width:482px;">
                     <tr>
                        <td>Expire Date (Optional):</td>
                         <td><ektronUI:Datepicker ID="promoExpireDate" runat="server"></ektronUI:Datepicker></td>
                    </tr>
                    <tr>
                        <td>Link field for Image-1:</td>
                         <td><asp:TextBox CssClass="Image1Link" ID="txtImage1Link" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                         <td>Link field for Image-2:</td>
                         <td><asp:TextBox CssClass="Image2Link" ID="txtImage2Link" runat="server"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnPromotional" CssClass="btnPromotional" runat="server" Text="Continue" OnClick="btnPromotional_Click" />
            <input type="hidden" id="hdnSelectedPromos" class="hdnSelectedPromos" runat="server" value="" />
            <asp:HiddenField ID="hdnCenterId" runat="server" Value="" />
            <br />
            <div class="errorMessage" id="errorMessage" style="display:none;"></div>
            <div id="promoUpdateMsg" class="successMessage" runat="server" visible="false">
                Promotions data saved Successfully!!!
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoPromoResults" runat="server" Visible="false">
            <p>No Promotionals are available for the Center.</p>
        </asp:Panel>
    </div>
</asp:Content>
