<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterNav.ascx.cs" Inherits="UserControls_FooterNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<%@ Register Src="~/UserControls/SocialIcons.ascx" TagPrefix="ux" TagName="SocialIcons" %>

<!-- mmm Footer Wrapper (nat) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Footer Wrapper (nat) mmm -->
<div class="footer_wrapper">
    <div class="footer clearfix">
        <div class="container_24">
            <div class="grid_24">
              <CMS:FlexMenu ID="footerMenu" runat="server" WrapTag="div" AutoCollapseBranches="True" Visible="false"
                    StartCollapsed="True" EnableMouseOverPopUp="True" EnableSmartOpen="False" StartLevel="1" MenuDepth="2" EnableAjax="False"
                    SuppressAddEdit="true" />
                <asp:Literal ID="ltrMenuData" runat="server"></asp:Literal>               
                <!--footer col 2-->
                <div class="footer_col footer_col_3">
                    <ul>
                        <li>Follow					     
                           <ux:SocialIcons ID="header" runat="server" />
                        </li>
                        <asp:Literal ID="ltrCopyRight" runat="server"></asp:Literal>
                        <cms:ContentBlock ID="cbCopyRight" runat="server" SuppressWrapperTags="true" WrapTag="span" Visible="false" DoInitFill="false" />                       
                    </ul>
                    <ul>
                        <li>Latest Tweets
                            <div id="twitter">
                                <a class="twitter-timeline" id="twitterWidget" runat="server" href="https://twitter.com/TeamLogicIT" data-chrome="nofooter transparent noheader noscrollbar" data-tweet-limit="2" data-link-color="#fa9709" data-widget-id="506917924578160640">Tweets by @TeamLogicIT</a>
                                <script type="text/javascript">!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>
                            </div>
                            <!-- #twitter -->
                        </li>
                    </ul>
                    <a href="/locations/" id="findLocationFooter" runat="server" visible="false" class="footer_location">Find a Location</a>
                    <a href="javascript:void('0')" id="subscribeFooter" runat="server" visible="false" class="subscribeFooter subscribe footer_location">Subscribe</a>
                </div>
                <!--footer col 3-->
            </div>
            <!-- grid 24-->
        </div>
        <!--container 24-->
    </div>
    <!-- footer-->
</div>
<!--footer_wrapper-->
