<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Center-Manage-Shop.aspx.cs" Inherits="AdminTool_Templates_Center_Manage_Testimonials" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server"> 
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>   
    <h3 align="left">Manage Shops Content&nbsp;</h3>
    <p align="left">This&nbsp;section&nbsp;enables&nbsp;you to manage the&nbsp;Shops on your website.</p>   
    <br />
    <br />
    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901"
        RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
        runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:HyperLinkField DataTextField="Title" DataNavigateUrlFields="Id"
                DataNavigateUrlFormatString="/AdminTool/Templates/EditShop.aspx?id={0}" HeaderText="Title" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="Teaser" HeaderText="Teaser" HeaderStyle-Width="150px" />
            <asp:CheckBoxField ReadOnly="true" DataField="IsActive" HeaderText="IsActive" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="Link" HeaderText="Link" HeaderStyle-Width="150px" />                      
        </Columns>
        <EmptyDataTemplate>
            <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                <tr style="color: White; background-color: #6A6A6A;">
                    <th scope="col" style="width: 150px;">Title</th>
                    <th scope="col" style="width: 150px;">Teaser</th>    
                    <th scope="col" style="width: 150px;">IsActive</th>                   
                    <th scope="col" style="width: 150px;">Link</th>
                </tr>
                <tr>
                    <td colspan="99" align="center">No records found for the search criteria.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
</asp:Content>

