<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="CenterManageProductAndServices.aspx.cs" Inherits="AdminTool_Templates_CenterManageProductAndServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="/AdminTool/css/styles-redips.css" />
    <script type="text/javascript" src="/AdminTool/js/redips-drag-min.js"></script>
    <script type="text/javascript" src="/AdminTool/js/script-redips.js"></script>
    <script type="text/javascript">
        //add product & services scripts  
        $(function () {
            $('.btnProductServices').click(function (e) {                
                var status = false;
                var status1 = false;
                var status2 = false;
                var selectedPS = $('.hddnPSIds').val();

                if (selectedPS == '') {
                    $('.errorMessage').show();
                    status1 = false;
                }
                else {
                    $('.errorMessage').hide();
                    status1 = true;
                }

                //get all ids on the final click to confirm on server side
                var selectedPSCat = $('.selectedPS');
                var jq = $([1]);
                var ids = '';
                for (var i = 0; i < selectedPSCat.length; i++) {
                    jq.context = jq[0] = selectedPSCat[i];
                    var html = jq.html();
                    if (html != undefined && html != '') {
                        if (jq.find('span').attr('id') != undefined) {
                            ids = ids + jq.find('span').attr('id') + ',';
                            status2 = true;
                        }
                    }
                }
                $('.hddnPSFinalSelectedIds').val(ids);

                if (status1 == false || status2 == false) {
                    status = false; $('.errorMessage').show();
                }
                else {
                    status = true; $('.errorMessage').hide();
                }

                return status;
            });

            $('.savePSSubCategory').click(function (e) {                
                var activeSubCategory = $(".activeSubCategory");
                if (activeSubCategory.length > 0) {
                    var jqc = $([1]);
                    var idsSub = '';
                    for (var i = 0; i < activeSubCategory.length; i++) {
                        jqc.context = jqc[0] = activeSubCategory[i];
                        var html = jqc.html();
                        if (html != '') {
                            var id = idsSub + jqc.find('span').attr('id');
                            idsSub = id + ',';
                        }
                    }
                    var currentCategorySel = $('.hddnCurrentDraggedCate').val();
                    if (currentCategorySel != "") {
                        idsSub = currentCategorySel + ';' + idsSub.slice(0, -1);
                        var currentHdnnVal = $('.hddnPSIds').val();
                        if (currentHdnnVal.indexOf(currentCategorySel) > -1) {
                            var selectedHddnIds = currentHdnnVal.split("|");
                            var updatedVal;
                            for (var i = 0; i < selectedHddnIds.length; i++) {
                                if (selectedHddnIds[i].indexOf(currentCategorySel) > -1) {
                                    if (updatedVal)
                                        updatedVal = updatedVal + '|' + idsSub;
                                    else
                                        updatedVal = idsSub;
                                }
                                else {
                                    if (updatedVal)
                                        updatedVal = updatedVal + '|' + selectedHddnIds[i];
                                    else
                                        updatedVal = selectedHddnIds[i];
                                }
                            }
                        }
                        else {
                            if (currentHdnnVal)
                                updatedVal = currentHdnnVal + '|' + idsSub;
                            else
                                updatedVal = idsSub;
                        }
                        $('.hddnPSIds').val(updatedVal);
                        $('#saveSubCategoryMessage').show();
                    }
                }
            });

        });

        function OnSelectedTDClick(item) {          
            if ((item.children[0]) && (item.children[0].children[0])) {
                var eleId = item.children[0].children[0].id;
                if (eleId) {
                    $('#saveSubCategoryMessage').hide();
                    $('.hddnCurrentDraggedCate').val(eleId);
                    $(".table2 tr:not(:first-child)").remove();
                    if ($('.' + eleId).length > 0) {
                        $('.' + eleId).each(function () {
                            var html = $(this).context.outerHTML;
                            $('.table2').append('<tr><td class="inActiveSubCategory">' + html + '</td><td class="activeSubCategory"></td></tr>');
                        });
                    }
                    else {
                        $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
                        $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
                        $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
                        $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
                        $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
                    }

                    //check if previously selected item is clicked, if so auto populate the selected values
                    var selectedPSIds = $('.hddnPSIds').val();
                    if ((selectedPSIds) && (selectedPSIds != '')) {
                        var idIndex = selectedPSIds.indexOf(eleId);
                        if (idIndex > -1) {
                            var selectedHddnIds = selectedPSIds.split("|");
                            for (var i = 0; i < selectedHddnIds.length; i++) {
                                if (selectedHddnIds[i].indexOf(eleId) > -1) {
                                    var selectedIds = selectedHddnIds[i].split(";");
                                    if (selectedIds[1]) {                                        
                                        var selectedSubCatIds = selectedIds[1].split(",");                                       
                                        for (var i = 0; i < selectedSubCatIds.length; i++) {
                                            var id = selectedSubCatIds[i];
                                            if (document.getElementById(id)) {
                                                var subCatHtml = document.getElementById(id).outerHTML;
                                                $('.table2').find('#' + id).parent().parent().remove();
                                                $('.table2 > tbody > tr').eq(i).after('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory">' + subCatHtml + '</td></tr>');
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    redipsInit();
                }
            }
        }

        function OnAvailCategoryTDClick(item) {
            $('#saveSubCategoryMessage').hide();
            $(".table2 tr:not(:first-child)").remove();
            $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
            $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
            $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
            $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
            $('.table2').append('<tr><td class="inActiveSubCategory"></td><td class="activeSubCategory"></td></tr>');
            $('.hddnCurrentDraggedCate').val('');

            //check if previously selected item is moved to available list if so remove from hidden list
            if ((item.children[0]) && (item.children[0].children[0])) {
                var eleId = item.children[0].children[0].id;
                if (eleId) {
                    var selectedPSIds = $('.hddnPSIds').val();
                    if ((selectedPSIds) && (selectedPSIds != '')) {
                        var idIndex = selectedPSIds.indexOf(eleId);
                        if (idIndex > -1) {
                            var selectedHddnIds = selectedPSIds.split("|");
                            var updatedVal;
                            for (var i = 0; i < selectedHddnIds.length; i++) {
                                if (selectedHddnIds[i].indexOf(eleId) < 0) {
                                    if (updatedVal)
                                        updatedVal = updatedVal + '|' + selectedHddnIds[i];
                                    else
                                        updatedVal = selectedHddnIds[i];
                                }
                            }
                            $('.hddnPSIds').val(updatedVal);
                        }
                    }
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <asp:Panel ID="pnlProductsServices" CssClass="pnlProductsServices" runat="server">
        <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>
        <h3>Manage Products & Services</h3>
        <div align="left">
            To save Product & Services:            
            <ol>
                <li>Drag the Main Products & Services Category on the left column.</li>
                <li>Click on Selected Products & Services main Cateogy to see the Sub-categories.</li>
                <li>Drag the Sub-Categories on the left column.</li>
                <li>Click on <strong>'Save'</strong> button to save the selected Main Category & Sub-Category selection before moving out to other Main Category.</li>
                <li>Repeat above steps to select <strong>EACH</strong> Main & sub-Categories of Products & Services.</li>
                <li>Finally Click on 'Update Products & Services' button to save all the Selections.<br /></li>
            </ol>
        </div>
        <asp:Label ID="lblError" runat="server" CssClass="errorMessage"></asp:Label>
        <div>
            <div id="drag1">
                <div class="dragSection">
                    <table id="table1" class="table1">
                        <colgroup>
                            <col width="240" />
                            <col width="240" />
                        </colgroup>
                        <tr>
                            <th colspan="1" class="mark" title="Message line">Available Products & Services</th>
                            <th colspan="1" class="mark" title="Message line">Selected Products & Services</th>
                        </tr>
                        <asp:ListView ID="lvAvailPs" runat="server">
                            <LayoutTemplate>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td onclick="OnAvailCategoryTDClick(this);" class="availPS">
                                        <%#Eval("AvailableCategory") %>
                                    </td>
                                    <td onclick="OnSelectedTDClick(this);" class="selectedPS">
                                        <%#Eval("SelectedCategory") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </table>
                </div>
            </div>
            <div id="drag2">
                <div class="dragSection">
                    <table class="table2">
                        <colgroup>
                            <col width="240" />
                            <col width="240" />
                        </colgroup>
                        <tr class="table2Heading">
                            <th colspan="1" class="mark" title="Message line">InActive P & S Sub-Category</th>
                            <th colspan="1" class="mark" title="Message line">Active P & S Sub-Category</th>
                        </tr>
                        <tr>
                            <td class="inActiveSubCategory"></td>
                            <td class="activeSubCategory"></td>
                        </tr>
                        <tr>
                            <td class="inActiveSubCategory"></td>
                            <td class="activeSubCategory"></td>
                        </tr>
                        <tr>
                            <td class="inActiveSubCategory"></td>
                            <td class="activeSubCategory"></td>
                        </tr>
                        <tr>
                            <td class="inActiveSubCategory"></td>
                            <td class="activeSubCategory"></td>
                        </tr>
                        <tr>
                            <td class="inActiveSubCategory"></td>
                            <td class="activeSubCategory"></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </div>
        </div>
        <br />
        <br />
        <table class="psButtons">
            <tr>
                <td>
                    <div>
                        <input type="button" value="Save" id="savePSSubCategory" class="savePSSubCategory" />
                        <span id="saveSubCategoryMessage" style="display: none; font-size: 10px; color: green;">Selected Sub-Categories Saved!!!</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <asp:Button ID="btnProductServices" CssClass="btnProductServices" runat="server" Text="Update Products & Services" OnClick="btnProductServices_Click" />
                        <input type="hidden" id="hddnPSIds" class="hddnPSIds" runat="server" value="" />
                        <input type="hidden" id="hddnPSFinalSelectedIds" class="hddnPSFinalSelectedIds" runat="server" value="" />
                        <input type="hidden" id="hddnCurrentDraggedCate" class="hddnCurrentDraggedCate" value="" />
                    </div>
                </td>
            </tr>
        </table>
        <div class="errorMessage" style="display: none;">
            <p>Please select atleast One category!!!</p>
        </div>
        <div id="psUpdateMsg" class="successMessage" runat="server" visible="false">
            Products & Services data saved Successfully!!!
        </div>
        <div id="hddnSubCategoryCnts" runat="server" style="display: none;">
            <asp:Literal ID="ltrSubCat" runat="server" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNoPSResults" runat="server" Visible="false">
        <p>No Products & Services are available for the Center.</p>
    </asp:Panel>
    <asp:HiddenField ID="hdnCenterId" runat="server" Value="" />
</asp:Content>

