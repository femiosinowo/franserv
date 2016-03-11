﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="manage-all-center-quotes.aspx.cs" Inherits="AdminTool_Templates_manage_all_quotes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>
    <h3>Manage All Request to Quotes:</h3>   
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
    <asp:GridView ID="GridView1" CssClass="centerData" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901" AutoGenerateDeleteButton="true"
        RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000" OnRowDeleting="GridView1_RowDeleting"
        runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging">
         <Columns>                         
                <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="LastName" HeaderText="Last Name" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="JobTitle" HeaderText="Job Title" HeaderStyle-Width="100px" /> 
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="WebSite" HeaderText="WebSite" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="MobileNumber" HeaderText="Mobile Number" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="ProjectDescription" HeaderText="Project Description" HeaderStyle-Width="100px" /> 
                <asp:HyperLinkField DataTextField="UploadedFileId" DataNavigateUrlFields="UploadedFileId"
                    DataNavigateUrlFormatString="/Workarea/DownloadAsset.aspx?id={0}" HeaderText ="Uploaded File" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="ProjectBudget" HeaderText="Project Budget" HeaderStyle-Width="150px" />
                <asp:BoundField DataField="DateSubmitted" HeaderText="Date Submitted" HeaderStyle-Width="150px" />                
                <asp:BoundField DataField="QuoteId" ItemStyle-CssClass="hidden" HeaderText="" HeaderStyle-Width="150px" />                                               
            </Columns>
        <EmptyDataTemplate>
            <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                 <tr style="color: White; background-color: #6A6A6A;">
                        <th scope="col" style="width: 100px;">First Name
                        </th>
                        <th scope="col" style="width: 100px;">Last Name
                        </th>
                        <th scope="col" style="width: 100px;">Email
                        </th>
                        <th scope="col" style="width: 100px;">Job Title
                        </th>
                        <th scope="col" style="width: 100px;">Company Name
                        </th>                        
                        <th scope="col" style="width: 100px;">WebSite
                        </th>                       
                        <th scope="col" style="width: 150px;">Mobile Number
                        </th>
                        <th scope="col" style="width: 150px;">Project Name
                        </th>
                        <th scope="col" style="width: 100px;">Project Description
                        </th>                          
                        <th scope="col" style=" display:none;">Uploaded File
                        </th>      
                        <th scope="col" style="width: 150px;">Project Budget
                        </th>                                                 
                        <th scope="col" style=" display:none;">Date Submitted
                        </th>                
                    </tr>
                <tr>
                    <td colspan="99" align="center">No records found for the search criteria.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
</asp:Content>

