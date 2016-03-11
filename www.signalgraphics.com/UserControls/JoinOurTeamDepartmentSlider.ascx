<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JoinOurTeamDepartmentSlider.ascx.cs"
    Inherits="UserControls_JoinOurTeamDepartmentSlider" %>
<%@ OutputCache Duration="21600" VaryByParam="None" %>

<div id="job_profile_slider">
    <div class="slider-wrapper">
        <ul>
            <asp:Repeater runat="server" ID="UxDepartmentSlider">
                <ItemTemplate>
                    <li>
                        <img class="inner-border" src="<%# Eval("imgSRC")%>" alt="<%# Eval("name")%>">
                        <h3 class="gray">
                            <%# Eval("name")%></h3>
                        <p>
                            <%# Eval("abstract")%>
                        </p>
                        <div class="cta-button-wrap purple small">
                            <a class="cta-button-text" href="<%# Eval("hreftext")%>"><span>Learn More</span></a>
                        </div>
                        <!-- button -->
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <!-- slider-wrapper -->
</div>
