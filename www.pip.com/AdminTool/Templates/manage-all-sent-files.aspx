﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="manage-all-sent-files.aspx.cs" Inherits="AdminTool_Templates_manage_all_sent_files" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <style type="text/css">
        .hidden{
            display:none;
        }     
        table th:nth-child(17){
           display:none;
        }      
        .thirdPartyLinks{
            width:150px !important;
            word-wrap:break-word;
        }   
        .UploadedFileId 
        {
            width:190px;
        }  
        .UploadedFileId a
        {
            display:block;
            word-wrap:normal;
        }      
    </style>
    <h3>Manage All Send-A-Files</h3>   
    <p>
        File attachments sent through Send-A-File are available for download from the server for 30 days. After this time the attached file will be deleted, but the upload history will remain indefinitely.
    </p>
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
    <asp:GridView ID="GridView1" CssClass="centerData" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901" AutoGenerateDeleteButton="False"
        RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000" OnRowDeleting="GridView1_RowDeleting"
        runat="server" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" AllowSorting="true" OnSorting="GridView1_Sorting" OnPageIndexChanging="OnPageIndexChanging">
        <Columns>            
            <asp:BoundField DataField="FirstName" SortExpression="FirstName" HeaderText="First Name" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="LastName" SortExpression="LastName" HeaderText="Last Name" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="Email" SortExpression="Email" HeaderText="Email" ItemStyle-Width="50px" HeaderStyle-Width="50px" />
            <asp:BoundField DataField="JobTitle" SortExpression="JobTitle" HeaderText="Job Title" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="CompanyName" SortExpression="CompanyName" HeaderText="Company Name" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="WebSite" SortExpression="WebSite" HeaderText="Web Site" HeaderStyle-Width="100px" />            
            <asp:BoundField DataField="ProjectName" SortExpression="ProjectName" HeaderText="Project Name" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="ProjectDueDate" SortExpression="ProjectDueDate" HeaderText="Project Due Date" HeaderStyle-Width="100px" />            
            <asp:BoundField DataField="ProjectQuantity" SortExpression="ProjectQuantity" HeaderText="Project Quantity" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="ProjectDescription" SortExpression="ProjectDescription" HeaderText="Project Description" ItemStyle-Width="100px" HeaderStyle-Width="100px" />
            <asp:TemplateField HeaderText="Uploaded File(s)" SortExpression="UploadedFileId" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <div class="UploadedFileId"><%# Eval("UploadedFileId") %></div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Download All Files" ItemStyle-Width="100px" HeaderStyle-Width="100px">
                <ItemTemplate>
                    <div>
                        <a href="/AdminTool/Templates/DownloadZipFiles.aspx?id=<%# Eval("SendFileId") %>&type=saf">Download All Files</a>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Third Party Uploaded File Link" SortExpression="UploadedFileExternalLinks" ItemStyle-Width="150px" HeaderStyle-Width="150px">
                <ItemTemplate>
                    <div class="thirdPartyLinks"><%# Eval("UploadedFileExternalLinks") %></div>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="DateSubmitted" SortExpression="DateSubmitted" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date Submitted" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="CenterId" SortExpression="CenterId" HeaderText="Center Id" HeaderStyle-Width="100px" />
            <asp:BoundField DataField="SendFileId" ItemStyle-CssClass="hidden" HeaderText="" HeaderStyle-Width="150px" />
             <asp:CommandField ShowDeleteButton="True" /> 
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
                    <th scope="col" style="width: 100px;">Web Site
                    </th>                   
                    <th scope="col" style="width: 150px;">Project Name
                    </th>
                    <th scope="col" style="width: 150px;">Project Due Date
                    </th>
                    <th scope="col" style="width: 150px;">Project Quantity
                    </th>
                    <th scope="col" style="width: 100px;">Project Description
                    </th>
                    <th scope="col" style="display: none;">Uploaded File(s)
                    </th>
                    <th scope="col" style="display: none;">Download All Files
                    </th>
                    <th scope="col" style="display: none;">Third Party Uploaded File Link
                    </th>                    
                    <th scope="col" style="display: none;">Date Submitted
                    </th>
                     <th scope="col" style="width: 150px;">Center Id
                    </th>
                </tr>
                <tr>
                    <td colspan="99" align="center">No records found for the search criteria.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>

