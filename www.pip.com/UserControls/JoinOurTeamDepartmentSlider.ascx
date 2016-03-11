<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JoinOurTeamDepartmentSlider.ascx.cs"
    Inherits="UserControls_JoinOurTeamDepartmentSlider" %>

<div class="grid_18 content-block col-height-equal">
            <asp:Repeater runat="server" ID="UxDepartmentSlider">
                <ItemTemplate>
                     <div class="grid_8 job_profile_wrapper">
                            <div class="job_profile_2 clearfix">
                                <div class="profile_img">
                                    <img src="<%# Eval("imgSRC")%>" alt="<%# Eval("name")%>" />
                                </div>
                                <!-- end shop_img -->
                                <div class="profile_text">
                                    <h4>
                                        <span><%# Eval("name")%></span></h4>
                                    <p><%# Eval("abstract")%></p>
                                    <a href="<%# Eval("hreftext")%>" class="cta-button-text cta-button-wrap black-btn"><span>Learn more</span>
                                    </a>
                                    <!-- end cta-button-wrap -->
                                </div>
                                <!-- end profile_text clearfix -->
                            </div>
                            <!-- end job_profile_2 clearfix -->
                        </div>
                        <!-- end job_profile_wrapper -->
                    <%--<li>
                        <img class="inner-border" src="<%# Eval("imgSRC")%>" alt="<%# Eval("name")%>">
                        <h3 class="gray">
                            <%# Eval("name")%></h3>
                        <p>
                            <%# Eval("abstract")%></p>
                        <div class="cta-button-wrap purple small">
                            <a class="cta-button-text" href="<%# Eval("hreftext")%>"><span>Learn More</span></a>
                        </div>
                        <!-- button -->
                    </li>--%>
                </ItemTemplate>
            </asp:Repeater>
    </div>
    <!-- slider-wrapper -->
