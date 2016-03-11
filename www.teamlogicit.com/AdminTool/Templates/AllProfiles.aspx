﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="AllProfiles.aspx.cs" Inherits="AdminTool_Templates_AllProfiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" Runat="Server">
    <style type="text/css">
        .hidden{
            display:none;
        }        
        table th:nth-child(15){
           display:none;
        }
    </style>
    <div>
        <h3 align="left">Manage All Profiles</h3>
        <a href="/AdminTool/Templates/Profile.aspx">Add Profile</a>
        <br />
        <br />
        <br />
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
        <asp:GridView ID="GridView1" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901" AllowSorting="true" OnSorting="GridView1_Sorting"
            RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
            runat="server" OnRowDeleting="GridView1_RowDeleting" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging">
            <Columns>
                <asp:HyperLinkField DataTextField="UserName" SortExpression="UserName" DataNavigateUrlFields="EmployeeId"
                    DataNavigateUrlFormatString="/AdminTool/Templates/Profile.aspx?userid={0}&type=edit" HeaderText ="User Name" HeaderStyle-Width="150px" />
                <asp:TemplateField HeaderText="FranservId" HeaderStyle-Width="150px">
	                <ItemTemplate>
		                <%# GetFranservId(Eval("FransId").ToString())%>
	                </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FirstName" SortExpression="FirstName" HeaderText="First Name" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="LastName" SortExpression="LastName" HeaderText="Last Name" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="MobileNumber" SortExpression="MobileNumber" HeaderText="Mobile Number" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="WorkPhone" SortExpression="WorkPhone" HeaderText="Work Phone" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="FaxPhone" SortExpression="FaxPhone" HeaderText="Fax Phone" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="Title" SortExpression="Title" HeaderText="Title" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="Roles" SortExpression="Roles" HeaderText="Roles" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="Gender" SortExpression="Gender" HeaderText="Gender" HeaderStyle-Width="100px" /> 
                <asp:BoundField DataField="IsActive" SortExpression="IsActive" HeaderText="IsActive" HeaderStyle-Width="100px" /> 
                <asp:BoundField DataField="FransId" SortExpression="FransId" HeaderText="Center Id" HeaderStyle-Width="150px" /> 
                <asp:CommandField ShowDeleteButton="true" HeaderText="Delete" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="EmployeeId" SortExpression="EmployeeId" ItemStyle-CssClass="hidden" HeaderText="" HeaderStyle-Width="150px" />                                               
            </Columns>
            <EmptyDataTemplate>
                <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                    <tr style="color: White; background-color: #6A6A6A;">
                        <th scope="col" style="width: 150px;">User Name
                        </th>   
                        <th scope="col" style="width: 150px;">FranservId
                        </th>                     
                        <th scope="col" style="width: 100px;">First Name
                        </th>
                        <th scope="col" style="width: 100px;">Last Name
                        </th>
                        <th scope="col" style="width: 100px;">Email
                        </th>
                        <th scope="col" style="width: 150px;">Mobile Number
                        </th>
                        <th scope="col" style="width: 150px;">Work Phone
                        </th>
                        <th scope="col" style="width: 100px;">Fax Phone
                        </th>
                        <th scope="col" style="width: 100px;">Title
                        </th>
                        <th scope="col" style="width: 100px;">Roles
                        </th>                        
                        <th scope="col" style="width: 100px;">Gender
                        </th>
                        <th scope="col" style="width: 100px;">IsActive
                        </th>   
                        <th scope="col" style="width: 150px;">Center Id
                        </th>
                        <th scope="col" style=" display:none;">UserId
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

