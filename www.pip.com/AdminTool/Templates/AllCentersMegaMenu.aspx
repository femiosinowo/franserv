<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="AllCentersMegaMenu.aspx.cs" Inherits="AdminTool_Templates_AllCenters" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <div>
        <h3>Manage All Centers Mega Menu:</h3> 
        <br />
        <div class="AlphabetPager">
            <asp:Repeater ID="rptAlphabets" runat="server">
                <ItemTemplate>
                    <asp:LinkButton runat="server" Text='<%#Eval("Value")%>' Visible='<%# !Convert.ToBoolean(Eval("Selected"))%>'
                        OnClick="Alphabet_Click" />
                    <asp:Label runat="server" Text='<%#Eval("Value")%>' Visible='<%# Convert.ToBoolean(Eval("Selected"))%>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <br />
        <asp:GridView ID="GridView1" CssClass="centerData" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901" AllowSorting="true"
            RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
            runat="server" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" OnSorting="GridView1_Sorting">
            <Columns>
                <asp:HyperLinkField DataTextField="CenterName" SortExpression="CenterName" DataNavigateUrlFields="FransId"
                    DataNavigateUrlFormatString="/AdminTool/Templates/Manage-Mega-Menu.aspx?centerid={0}&type=edit" HeaderText ="Center Name" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="FranservId" SortExpression="FranservId" HeaderText="Franserv ID" />
                <asp:BoundField DataField="PhoneNumber" SortExpression="PhoneNumber" HeaderText="Phone Number" HeaderStyle-Width="150px" />                
                <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="SendAFileEmail" SortExpression="SendAFileEmail" HeaderText="Send A File Email" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="RequestAQuoteEmail" SortExpression="RequestAQuoteEmail" HeaderText="Request A Quote Email" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="Address1" SortExpression="Address1" HeaderText="Address1" HeaderStyle-Width="150px" />                
                <asp:BoundField DataField="City" SortExpression="City" HeaderText="City" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="State" SortExpression="State" HeaderText="State" HeaderStyle-Width="100px" />
                <%--<asp:BoundField DataField="StateFullName" HeaderText="State Full Name" HeaderStyle-Width="150px" />--%>
                <asp:BoundField DataField="Zipcode" SortExpression="Zipcode" HeaderText="Zipcode" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="Country" SortExpression="Country" HeaderText="Country" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="HoursOfOperation" SortExpression="HoursOfOperation" HeaderText="Hours Of Operation" HeaderStyle-Width="100px" />
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                    <tr style="color: White; background-color: #6A6A6A;">
                        <th scope="col" style="width: 150px;">Center Name
                        </th>
                        <th scope="col" style="width: 100px;">Franserv ID
                        </th>
                        <th scope="col" style="width: 150px;">Phone Number
                        </th>                       
                        <th scope="col" style="width: 100px;">Email
                        </th>
                        <th scope="col" style="width: 100px;">Send A File Email
                        </th>
                        <th scope="col" style="width: 150px;">Request A Quote Email
                        </th>
                        <th scope="col" style="width: 150px;">Address1
                        </th>                        
                        <th scope="col" style="width: 100px;">City
                        </th>
                        <th scope="col" style="width: 100px;">State
                        </th>
                        <%--<th scope="col" style="width: 150px;">State Full Name
                        </th>--%>
                        <th scope="col" style="width: 100px;">Zipcode
                        </th>
                        <th scope="col" style="width: 100px;">Country
                        </th>
                        <th scope="col" style="width: 100px;">Hours Of Operation
                        </th>
                    </tr>
                    <tr>
                        <td colspan="99" align="center">No records found for the search criteria.
                        </td>
                    </tr>
                </table>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

