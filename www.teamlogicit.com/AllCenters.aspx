<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="AllCenters.aspx.cs" Inherits="AllCenters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMainContent" runat="Server">
    <style type="text/css">
        .hidden {
            display: none;
        }

        table th:nth-child(12) {
            display: none;
        }

        .AlphabetPager a {
            margin: 0 5px;
        }

        .centerDataPage a {
            color: blue;
        }
    </style>
    <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix portfolio">
            <div class="container_24">
                <div class="grid_24 centerDataPage">
                    <asp:Panel ID="pnlAllCenterData" runat="server">
                        <h3>All Center Data:</h3>
                        <asp:Label ID="lblError" CssClass="errorMessage" runat="server"></asp:Label>
                        <br />
                        <asp:GridView ID="GridView1" CssClass="centerData" HeaderStyle-BackColor="#6A6A6A" HeaderStyle-ForeColor="#FE9901"
                            RowStyle-BackColor="#EDEDED" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
                            runat="server" AutoGenerateColumns="false" PageSize="10" AllowPaging="true">
                            <Columns>
                                <asp:HyperLinkField DataTextField="CenterName" DataNavigateUrlFields="FransId"
                                    DataNavigateUrlFormatString="/AllCenters.aspx?centerid={0}&type=edit" HeaderText="Center Name" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="SendAFileEmail" HeaderText="Send A File Email" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="RequestAQuoteEmail" HeaderText="Request A Quote Email" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="Address1" HeaderText="Address1" HeaderStyle-Width="150px" />
                                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="Zipcode" HeaderText="Zipcode" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="Country" HeaderText="Country" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="HoursOfOperation" HeaderText="Hours Of Operation" HeaderStyle-Width="100px" />
                                <asp:BoundField DataField="FransId" ItemStyle-CssClass="hidden" HeaderText="" HeaderStyle-Width="150px" />
                            </Columns>
                            <EmptyDataTemplate>
                                <table cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                    <tr style="color: White; background-color: #6A6A6A;">
                                        <th scope="col" style="width: 150px;">Center Name
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
                                        <th scope="col" style="width: 100px;">Zipcode
                                        </th>
                                        <th scope="col" style="width: 100px;">Country
                                        </th>
                                        <th scope="col" style="width: 100px;">Hours Of Operation
                                        </th>
                                        <th scope="col" style="width: 100px;">FransId
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="99" align="center">No records found for the search criteria.
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </asp:Panel>
                    <asp:Panel ID="pnlCenterDetails" runat="server" Visible="false">
                        <a href="/">Home</a> >> <a href="/AllCenters.aspx">All Centers</a> >> Center Info
                        <h3>Center Information:</h3>
                        <p class="errorMessage">
                            <asp:Label ID="Label1" runat="server" Visible="false" />
                        </p>
                        <table width="60%" border="1" cellpadding="2" summary="">
                            <tbody>
                                <tr>
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <strong>
                                            <label for="lblName">Name:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <strong>
                                            <label for="lblAddress1">Address 1:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblAddress1" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <label for="lblAddress2">Address 2:</label></span></td>
                                    <td>
                                        <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <strong>
                                            <label for="lblCity">City:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblCity" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <strong>
                                            <label for="lblState">State:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblState" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="lblZipCode">Zip:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblZipCode" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline"><span class="CS_Form_Label_Baseline">
                                        <strong>
                                            <label for="lblCountry">Country:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="lblPhone">Phone:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="CS_Form_Label_Baseline">
                                            <strong>
                                                <label for="lblFax">Fax:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblFax" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="CS_Form_Label_Baseline">
                                            <strong>
                                                <label for="lblWorkingHours">Working Hours:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblWorkingHours" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="lblCenterEmail">Center Email:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCenterEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="lblContactFirstName">Center Contact FirstName:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContactFirstName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Label_Baseline">
                                        <span class="CS_Form_Label_Baseline"><strong>
                                            <label for="lblContactLastName">Center Contact LastName:</label></strong></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblContactLastName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="lblSendAFileEmail"><strong>Send-A-File Email:</strong></label></span></td>
                                    <td>
                                        <asp:Label ID="lblSendAFileEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="CS_Form_Required_Baseline"><span class="CS_Form_Required_Baseline">
                                        <label for="lblRequestAQuote"><strong>Request a Quote Email:</strong></label></span></td>
                                    <td>
                                        <asp:Label ID="lblRequestAQuote" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblFlickrId">Flickr UserId:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblFlickrId" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblTwitterFeedURL">Twitter Feed URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblTwitterFeedURL" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblFacebookUrl">Facebook URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblFacebookUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblTwitterUrl">Twitter URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblTwitterUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblGooglePlusUrl">Google+ URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblGooglePlusUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblLinkedinUrl">LinkedIn URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblLinkedinUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblStumbleUponUrl">Stumble Upon URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblStumbleUponUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblFlickrUrl">Flickr URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblFlickrUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblYoutubeUrl">Youtube URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblYoutubeUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span>
                                        <strong>
                                            <label for="lblMarketingTangoUrl">Marketing Tango URL:</label></strong></span></td>
                                    <td>
                                        <asp:Label ID="lblMarketingTangoUrl" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

