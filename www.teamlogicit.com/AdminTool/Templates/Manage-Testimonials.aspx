<%@ Page Title="" Language="C#" MasterPageFile="~/AdminTool/MasterPages/admin.master" AutoEventWireup="true" CodeFile="Manage-Testimonials.aspx.cs" Inherits="AdminTool_Templates_Manage_Testimonials" %>

<asp:Content ID="Content2" ContentPlaceHolderID="mainCPH" runat="Server">
    <style type="text/css">
        .hiddenCareerColumn {
            display: none;
        }

        table th:nth-child(13) {
            display: none;
        }
    </style>
    <div id="centerInfo" runat="server" visible="false">
            <strong>Name: </strong> <asp:Label ID="lblCenterName" runat="server" /> &nbsp;&nbsp; <strong>Center Id: </strong><asp:Label ID="lblCenterId" runat="server" />
        </div>
    <h3 align="left">Manage Testimonials&nbsp;</h3>
    <p align="left">This&nbsp;section&nbsp;enables&nbsp;you to&nbsp; manage the&nbsp;testimonials on the National website.</p>
    <br />
    <br />
    <asp:GridView ID="GridView1" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901" AllowSorting="true" OnSorting="GridView1_Sorting"
        RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
        runat="server" AutoGenerateColumns="false" PageSize="30" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging">
        <Columns>
            <asp:HyperLinkField DataTextField="Title" SortExpression="Title" DataNavigateUrlFields="TestimonialId"
                DataNavigateUrlFormatString="/AdminTool/Templates/EditTestimonial.aspx?id={0}" HeaderText="Title" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="FranservId" SortExpression="FranservId" HeaderText="FranservId" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="FirstName" SortExpression="FirstName" HeaderText="First Name" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="LastName" SortExpression="LastName" HeaderText="Last Name" HeaderStyle-Width="150px" />
            <asp:CheckBoxField ReadOnly="true" DataField="IsNational" SortExpression="IsNational" HeaderText="IsNational" HeaderStyle-Width="150px" />
             <asp:CheckBoxField ReadOnly="true" DataField="IsAboutUs" SortExpression="IsAboutUs" HeaderText="IsAboutUs" HeaderStyle-Width="150px" />
            <asp:CheckBoxField ReadOnly="true" DataField="IsManageIT" SortExpression="IsManageIT" HeaderText="IsManageIT" HeaderStyle-Width="150px" />
            <asp:CheckBoxField ReadOnly="true" DataField="IsProjectExpert" SortExpression="IsProjectExpert" HeaderText="IsConsultingProjects" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="Organization" SortExpression="Organization" HeaderText="Company" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="EmailAddress" SortExpression="EmailAddress" HeaderText="Email Address" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="PhoneNumber" SortExpression="PhoneNumber" HeaderText="Phone Number" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="Created_Date" SortExpression="Created_Date" HeaderText="Created Date" HeaderStyle-Width="150px" />
            <asp:BoundField DataField="TestimonialId" SortExpression="TestimonialId" ItemStyle-CssClass="hiddenCareerColumn" HeaderText="" HeaderStyle-Width="150px" />
        </Columns>
        <EmptyDataTemplate>
            <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                <tr style="color: White; background-color: #6A6A6A;">
                    <th scope="col" style="width: 150px;">Title</th>
                    <th scope="col" style="width: 150px;">FranservId</th>
                    <th scope="col" style="width: 150px;">First Name</th>
                    <th scope="col" style="width: 150px;">Last Name</th>
                    <th scope="col" style="width: 150px;">IsNational</th>
                    <th scope="col" style="width: 150px;">IsAboutUs</th>
                    <th scope="col" style="width: 150px;">IsManageIT</th>
                    <th scope="col" style="width: 150px;">IsConsultingProjects</th>
                    <th scope="col" style="width: 150px;">Company</th>
                    <th scope="col" style="width: 150px;">Email Address</th>
                    <th scope="col" style="width: 150px;">Phone Number</th>
                    <th scope="col" style="width: 150px;">Created Date</th>
                    <th scope="col" style="width: 150px;">TestimonialId</th>
                </tr>
                <tr>
                    <td colspan="99" align="center">No records found for the search criteria.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:HiddenField ID="hddnCenterId" runat="server" Value="" />
</asp:Content>

