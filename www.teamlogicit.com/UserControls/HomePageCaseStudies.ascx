<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageCaseStudies.ascx.cs" Inherits="UserControls_HomePageCaseStudies" %>


<asp:ListView ID="lvCaseStudies" runat="server">
    <LayoutTemplate>
        <ul>
            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li>
            <div class="case_studies_div case_studies_<%# Container.DataItemIndex + 1 %>" style="background-image: url('<%# Eval("MainImagePath") %>');">
                <a href="<%# Eval("Link") %>">
                    <div class="case_studies_content">
                        <img src="<%# Eval("ImagePath") %>" />
                        <h3><%# Eval("Title") %></h3>
                        <h4><%# Eval("SubTitle") %></h4>
                    </div>
                    <!--case studies content-->
                </a>
            </div>
        </li>
    </ItemTemplate>
</asp:ListView>