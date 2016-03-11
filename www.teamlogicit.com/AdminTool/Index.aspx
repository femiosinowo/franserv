<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="AdminTool_Templates_Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <div class="" id="cs_control_1473">
        <div id="CS_Element_adminmaincontainer" title="" class="CS_Element_Schedule">
            <a id="textblocknohdr5654" name="textblocknohdr5654"></a><a id="CP_JUMP_5654" name="CP_JUMP_5654"></a>
            <div class="cs_control CS_Element_Textblock" id="cs_control_5654">
                <div class="CS_Textblock_Text">
                    <asp:Panel ID="pnlSuperAdmin" Visible="false" runat="server">
                         <h3>Super Admin</h3>
                        <p>This section of the Admin Tool is for Super admins only. In this section, super users will be able to add categories, add content, add images, add newsroom content, etc.</p>
                    </asp:Panel>
                    <asp:Panel ID="pnlLocalAdmin" Visible="false" runat="server">
                        <h3>Franchise Admin</h3>
                        <p>This section of the Admin Tool is for Local admins only.</p>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
