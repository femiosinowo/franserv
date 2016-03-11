<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearch.ascx.cs" Inherits="UserControls_JobSearch" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<!-- mmm Search Careers (shared) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Search Careers (shared) mmm -->
    <div id="search_careers" class="search_careers clearfix">
        <div class="container_24">
            <div class="grid_24">
                <div class="search_header">
                    <h2 class="headline">search careers</h2>
                    <div class="form" id="job_search_form">
                        <fieldset id="searchbox">
                            <div class="key_word">
                                <asp:TextBox ID="txtkeyword" CssClass="txtkeyword" runat="server" placeholder="Enter Keyword*" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv" runat="server" ValidationGroup="Keyword" ForeColor="Red"
                                     ControlToValidate="txtkeyword"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnSearchKeyword" CssClass="btnCustomSearch" ValidationGroup="Keyword" runat="server" Text="" OnClick="btnSearchKeyword_Click" /> 
                            </div>
                            <div class="location">
                                <asp:TextBox ID="txtLocation" runat="server" placeholder="Enter Location*" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv2" runat="server" ValidationGroup="Location" ForeColor="Red"
                                     ControlToValidate="txtLocation"></asp:RequiredFieldValidator>
                                <asp:Button ID="btnSearchLocation" CssClass="btnCustomSearch" runat="server" ValidationGroup="Location" Text="" OnClick="btnSearchLocation_Click" />                                 
                            </div>
                        </fieldset>
                    </div>
                    <!--//job_search_form-->
                </div>
                <!--//search_header-->
            </div>
            <!--//grid_24-->
        </div>
        <!--container 24-->
    </div>
    <!--search_careers-->
    <div class="job_search clearfix">
        <div class="container_24">
            <div class="grid_24">
                <table class="jobs_table">
                    <thead>
                        <tr>
                            <th class="job_position active">
                            <h3>Position</h3>
                            </th>
                            <th class="job_field">
                                <h3>Job Profile</h3>
                            </th>
                            <th class="job_location">
                                <h3>Location</h3>
                            </th>
                            <th class="job_posted">
                                <h3>Posted</h3>
                            </th>
                        </tr>                        
                    </thead>
                     <asp:ListView ID="lvJobs" runat="server">
                            <LayoutTemplate>
                                <tbody>
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                 </tbody>
                            </LayoutTemplate>
                            <ItemTemplate>
                               <tr class="acc_content">
                                    <td><span class="jobs-arrow"></span><%# Eval("Title") %></td>
                                    <td><%# Eval("ProfileType") %></td>
                                    <td><%# Eval("Location") %></td>
                                    <td><%# Eval("PostedDate") %></td>                               
                               </tr>
                                <tr>
                                    <td colspan="2" class="job_details_content">
                                      <p><%# Eval("JobDescription") %></p>
                                      <div class="apply square_button"><a href="<%# Eval("JobURL") %>">Apply to this Job</a></div>
                                    </td>
                                    <td colspan="2" class="details_meta">
                                        <img src="<%# Eval("GoogleMapImage") %>" alt="" />
                                        <h3>Location</h3>
                                        <p>
                                           <%# Eval("AddressLine1") %><br/>
                                           <%# Eval("City") %>, <%# Eval("State") %> <%# Eval("ZipCode") %>
                                        </p>
                                        <h3>Contact</h3>
                                        <ul>
                                            <li><%# Eval("ContactFirstName") %> <%# Eval("ContactLastName") %></li>
                                            <li class="telephone"><a href="tel:<%# Eval("WorkPhoneNumber") %>"><%# Eval("WorkPhoneNumber") %></a></li>
                                            <!--<li class="mobile"><a href="tel:"></a></li>-->
                                        </ul>
                                        <h3>How to Apply</h3>
                                         <%# Eval("HowToAppyText") %>
                                        <div class="apply square_button"><a href="<%# Eval("JobURL") %>">Apply Now</a></div>
                                  </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>                    
                </table>
                <div class="clear"></div>
            </div>
        </div>
        <div class="container_24">
            <div class="grid_24">
                <div class="pagination">
                    <asp:DataPager ID="dataPager1" ClientIDMode="Static" runat="server" PageSize="10" PagedControlID="lvJobs"
                        OnPreRender="ListPager_PreRender">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="previous square_button"  PreviousPageText="Previous" RenderDisabledButtonsAsLabels="true"
                                ShowFirstPageButton="false" ShowPreviousPageButton="true" ShowLastPageButton="false"
                                ShowNextPageButton="false" />                              
                              <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="page_number" CurrentPageLabelCssClass="page_number current"
                                  NextPreviousButtonCssClass="command" />                              
                            <asp:NextPreviousPagerField ButtonCssClass="next square_button" NextPageText="Next"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="false" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                    <!--<span class="previous square_button"><a href="#">Previous</a></span>
                    <div class="page_numbers">
                        <span class="current page_number">01</span>
                        <span class="page_number"><a href="#">02</a></span>
                        <span class="page_number"><a href="#">03</a></span>
                        <span class="page_number"><a href="#">04</a></span>
                    </div>
                    //.page_numbers
                    <span class="next square_button"><a href="#">Next</a></span>-->
                </div>
                <!--//.pagination-->
            </div>
        </div>
        <div class="clear"></div>
    </div>
    <CMS:ContentBlock ID="cbHowToApplyText" runat="server" DoInitFill="false" />
    <!--//.job_search-->