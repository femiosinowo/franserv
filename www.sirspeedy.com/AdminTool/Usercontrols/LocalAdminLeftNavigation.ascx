<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocalAdminLeftNavigation.ascx.cs" Inherits="AdminTool_Usercontrols_LeftNavigation" %>

<div class="" id="cs_control_1907">
    <div id="CS_Element_adminleftnavmenu" title="" class="CS_Element_Schedule">
        <div class="cs_control CS_Element_Textblock" id="cs_control_1936">
            <p>&nbsp;&nbsp;Franchise Admin&nbsp;<asp:Label ID="lblRole" runat="server"></asp:Label></p>            
        </div>
        <a id="FranchiseAdminLinkbar" name="FranchiseAdminLinkbar"></a><a id="CP_JUMP_1937" name="CP_JUMP_1937"></a>
        <div class="cs_control CS_Element_LinkBar" id="cs_control_1937">
            <ul id="centerManageLinks" runat="server" visible="false">
                <li><a href="/admintool/index.aspx" class="aNav">Local Admin Home</a> </li>
                <li id="centerInfo" runat="server" visible="false"><a href="/admintool/templates/manage-center.aspx" class="aNav">Center Information</a> </li>
                 <li id="banners" runat="server"><a href="/admintool/templates/CenterManageBanners.aspx" class="aNav">Manage Banners</a> </li>
                <li id="whyWeAreDiff" runat="server" visible="false"><a href="/admintool/templates/Why_we_are_diff.aspx" class="aNav">Why We are Different</a> </li>
                <li id="managePSlist" runat="server"><a href="/admintool/templates/CenterManageProductAndServices.aspx" class="aNav">Manage Products And Services</a> </li>
                <li id="promotions" runat="server" visible="false"><a href="/admintool/templates/CenterManagePromos.aspx" class="aNav">Manage Promotions</a> </li>
                <!--<li id="partners" runat="server" visible="false"><a href="/admin/wc/partners/Manage-Partners.cfm" class="aNav">Partners / Affiliates</a> </li>-->
                <li id="testimonials" runat="server" visible="false"><a href="/admintool/templates/Center-Manage-Testimonials.aspx" class="aNav">Manage Testimonials</a> </li>
                <li id="careers" runat="server" visible="false"><a href="/admintool/templates/Manage-Careers.aspx" class="aNav">Manage Careers</a> </li>
                <li id="allProfiles" runat="server" visible="true"><a href="/admintool/templates/AllCenterProfiles.aspx" class="aNav">Manage All Profiles</a> </li>
                <li id="myTeam" runat="server" visible="false"><a href="/admintool/templates/Center-My-Team.aspx" class="aNav">My Team</a> </li>
                <li><a href="/admintool/templates/Center-Manage-Shop.aspx" class="aNav">Manage Shop Content</a> </li>
                <li id="sendAFile" runat="server" visible="false"><a href="/admintool/templates/manage-all-center-sent-files.aspx" class="aNav">Send-A-File</a> </li>
                <li id="requestAQuote" runat="server" visible="false"><a href="/admintool/templates/manage-all-center-quotes.aspx" class="aNav">Request-A-Quote</a> </li>
                <li><a href="/admintool/templates/AllCenterSubscriptions.aspx" class="aNav">Manage All Subscriptions</a> </li>
                <li><a href="/admintool/templates/logout.aspx" class="aNav">Logout</a> </li>
            </ul>
        </div>       
    </div>
</div>
