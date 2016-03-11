<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true"
    CodeFile="thank_you.aspx.cs" Inherits="thank_you" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <script type="text/javascript">
        function DoPostForLogOut() {
            __doPostBack('<%=log_out.ClientID %>', 'onClickLogOut');
        }
    </script>
    <!-- mmm Thank You Content (National) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Thank You Content (National) mmm -->
    <div class="about_us_company_wrapper  clearfix">
        <div class="about_us_company clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <div class="container_24">
                        <div class="grid_24">
                            <h2 class="headline">Thank You!</h2>
                            <h3>
                                <CMS:ContentBlock ID="cbContentTitle" runat="server" DoInitFill="false" />
                            </h3>
                            <p>
                                <CMS:ContentBlock ID="cbThankYouContent" runat="server" DoInitFill="false" />
                            </p>
                            <div class="form" id="btnLogOutSection" runat="server" visible="false">
                                <%--cta-button-wrap--%>
                                <hr />
                                <asp:Button ID="log_out" CssClass="cta-button-text" runat="server" Text="LOG OUT" OnClientClick="javascript:DoPostForLogOut()" UseSubmitBehavior="true" />
                                <%--<a class="cta-button-text" href="#"><span>LOG OUT</span></a>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end grid_24 -->
            </div>
            <!--end grid_24-->
        </div>
        <!--end container_24-->
    </div>
    <div class="clear"></div>
    <iframe id="iframeFile" runat="server" style="display: none;"></iframe>
</asp:Content>
