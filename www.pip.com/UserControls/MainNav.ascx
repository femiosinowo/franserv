<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MainNav.ascx.cs" Inherits="UserControls_MainNav" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<!-- mmm Main Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Main Nav (both) mmm -->
<div class="main_nav_wrapper  clearfix">
    <div class="main_nav">
        <div class="container_24">
            <div class="grid_20">
                <div class="main-navigation-wrap clearfix">
                    <div class="main-navigation clearfix">
                        <a href="/" class="logo">
                            <CMS:ContentBlock ID="cbLogo" runat="server" DoInitFill="false" SuppressWrapperTags="true" />
                        </a>
                        <!--logo-->
                        <div class="main-navigation clearfix">
                            <div class="desktop-nav">
                                <ul>
                                    <li class="desktop-nav-link no-hover"><a class="products-and-services-link" href="#">
                                        <asp:Literal ID="ltrProductsServices" runat="server" /></a>
                                        <div class="megamenu-outer-wrap clearfix" style="display: none;">
                                            <div class="">
                                                <div class="megamenu-wrap">
                                                    <div class="megamenu">
                                                        <!-- START - PRODUCTS AND SERVICES MEGA PANEL - START - PRODUCTS AND SERVICES MEGA PANEL - START -->
                                                        <!-- START - PRODUCTS AND SERVICES MEGA PANEL - START - PRODUCTS AND SERVICES MEGA PANEL - START -->
                                                        <section class="mega-panel-wrap" id="products-and-services-menu">
                                                            <div class="mega-panel">
                                                                <div class="lvl-2-container">

                                                                    <div class="container_24">
                                                                        <div class="grid_24">
                                                                            <a href="/product-services/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    PRODUCTS &amp; SERVICES<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                        </div>
                                                                        <!--grid_24-->
                                                                    </div>
                                                                    <!--container_24-->

                                                                    <div class="container_24 clearfix">
                                                                        <div class="grid_16">
                                                                            <div class="lvl-2-list-wrap">
                                                                                <ul class="lvl-2-list floater-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxProductAndServicesMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item raised-block more-what"><a class="more-what" href="<%# Eval("hrefText") %>">
                                                                                                <span class="item-content">
                                                                                                    <img alt="" class="stretchy-pic" height="70" width="125" src="<%# Eval("imageSRC") %>">
                                                                                                    <span class="item-text">
                                                                                                        <%# Eval("title") %></span> </span></a></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- grid_16 -->
                                                                        <div class="grid_8">
                                                                            <div class="lvl-2-text-wrap raised-block">
                                                                                <div class="lvl-2-text">
                                                                                    <asp:Repeater runat="server" ID="UxPSMMSide">
                                                                                        <ItemTemplate>
                                                                                            <img alt="" class="stretchy-pic" src="<%# Eval("imageSRC") %>" />
                                                                                            <span class="title-t1">
                                                                                                <%# Eval("title") %></span>
                                                                                            <p class="elegant-t2">
                                                                                                <em>
                                                                                                    <%# Eval("tagline") %></em>
                                                                                            </p>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                    <a class="title-t3" href="/product-services/">VIEW ALL PRODUCTS &amp; SERVICES</a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <!-- grid_8 -->
                                                                    </div>
                                                                    <!-- container_24 -->

                                                                </div>
                                                                <!-- lvl-2-container -->
                                                            </div>
                                                            <!-- mega-panel -->
                                                        </section>
                                                        <!-- mega-panel-wrap -->
                                                        <!-- END - PRODUCTS AND SERVICES MEGA PANEL - END - PRODUCTS AND SERVICES MEGA PANEL - END -->
                                                        <!-- END - PRODUCTS AND SERVICES MEGA PANEL - END - PRODUCTS AND SERVICES MEGA PANEL - END -->
                                                    </div>
                                                    <!-- megamenu -->
                                                </div>
                                                <!-- megamenu-wrap -->
                                            </div>
                                            <!--end -->
                                        </div>
                                        <!--end -->
                                    </li>
                                    <li id="flickrPortfolio" runat="server" visible="false" class="desktop-nav-link no-hover">
                                        <a href="#" class="portfolio-link">
                                            <asp:Literal ID="ltrPortfolio" runat="server" /></a>
                                        <div class="megamenu-outer-wrap clearfix" style="display: none;">
                                            <div class="">
                                                <div class="megamenu-wrap">
                                                    <div class="megamenu">
                                                        <!-- START PORTFOLIO MEGA PANEL - START - PORTFOLIO MEGA PANEL - START -->
                                                        <!-- START PORTFOLIO MEGA PANEL - START - PORTFOLIO MEGA PANEL - START -->
                                                        <section id="portfolio-menu" class="mega-panel-wrap">
                                                            <div class="mega-panel">
                                                                <div class="lvl-2-container">
                                                                    <div class="container_24 clearfix">
                                                                        <div class="grid_24">
                                                                            <a class="lvl-2-title" href="/portfolio/">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    ALL PORTFOLIO<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <asp:ListView ID="lvPortfolio" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <div class="grid_6">
                                                                                        <div class="lvl-2-container">
                                                                                            <div class="raised-block clearfix">
                                                                                                <div class="portfolio-menu-img-wrapper">
                                                                                                    <img src="<%# Eval("PhotosetSmallUrl") %>" class="wide-only" alt="<%# Eval("Title") %>" />
                                                                                                </div>
                                                                                            </div>
                                                                                            <!-- end raised-block -->
                                                                                            <div class="lvl-2-text-wrap">
                                                                                                <p class="portfolio-title"><%# Eval("Title") %></p>
                                                                                                <p class="portfolio-desc">
                                                                                                    <%# Eval("Description") %>
                                                                                                </p>
                                                                                                <!--$todo: have to work on detail view -->
                                                                                                <a href="<%# FormatUrl(Eval("PhotosetId")) %>" class="more-what">VIEW</a>
                                                                                            </div>
                                                                                            <!-- end lvl-2-text-wrap -->
                                                                                        </div>
                                                                                        <!-- lvl-2-container -->
                                                                                    </div>
                                                                                    <!-- grid_6  -->
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                            <!-- grid_6  -->
                                                                        </div>
                                                                        <!-- grid_24 -->
                                                                    </div>
                                                                    <!-- container_24 -->
                                                                </div>
                                                                <!-- lvl-2-container -->
                                                            </div>
                                                            <!-- mega-panel -->
                                                        </section>
                                                        <!-- mega-panel-wrap -->
                                                        <!-- END - PORTFOLIO MEGA PANEL - END -PORTFOLIO MEGA PANEL - END -->
                                                        <!-- END - PORTFOLIO MEGA PANEL - END - PORTFOLIO MEGA PANEL - END -->
                                                    </div>
                                                    <!-- megamenu -->
                                                </div>
                                                <!-- megamenu-wrap -->
                                            </div>
                                            <!--end grid_24-->
                                        </div>
                                        <!--end container_24-->
                                    </li>
                                    <li class="desktop-nav-link no-hover"><a class="about-us-link" href="#">
                                        <asp:Literal ID="ltrAboutUs" runat="server" /></a>
                                        <div class="megamenu-outer-wrap clearfix" style="display: none;">
                                            <div class="">
                                                <div class="megamenu-wrap">
                                                    <div class="megamenu">
                                                        <!-- START ABOUT US MEGA PANEL - START - ABOUT US MEGA PANEL - START -->
                                                        <!-- START ABOUT US MEGA PANEL - START - ABOUT US MEGA PANEL - START -->
                                                        <section class="mega-panel-wrap" id="about-us-menu">
                                                            <div class="mega-panel">
                                                                <div class="container_24 clearfix">
                                                                    <div class="grid_7">
                                                                        <div class="lvl-2-container" id="companyInfo" runat="server">
                                                                            <a href="/company-info/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    COMPANY INFO<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-text-wrap">
                                                                                <div class="lvl-2-text company-info">
                                                                                    <asp:Repeater runat="server" ID="UxCompanyInfoMM">
                                                                                        <ItemTemplate>
                                                                                            <p>
                                                                                                <%# Eval("teaser") %>
                                                                                            </p>
                                                                                            <a class="more-what" href="/company-info/">Learn More</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                                <!-- lvl-2-text -->
                                                                            </div>
                                                                            <!-- lvl-2-text-wrap -->
                                                                        </div>
                                                                        <div class="lvl-2-container" id="aboutSirSpeedy" visible="false" runat="server">
                                                                            <a href="/company-info/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    COMPANY INFO<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-text-wrap">
                                                                                <div class="lvl-2-text company-info">
                                                                                    <asp:Repeater runat="server" ID="UxAboutUs">
                                                                                        <ItemTemplate>
                                                                                            <p>
                                                                                                <%# Eval("teaser") %>
                                                                                            </p>
                                                                                            <a class="more-what" href="/company-info/">Learn More</a>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                                <!-- lvl-2-text -->
                                                                            </div>
                                                                            <!-- lvl-2-text-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_7  -->
                                                                    <div class="grid_1">
                                                                        <span>&nbsp;</span>
                                                                    </div>
                                                                    <div class="grid_8">
                                                                        <div class="lvl-2-container" id="managementSection" runat="server">
                                                                            <a href="/company-info/management-team/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    MANAGEMENT<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-list-wrap megaMenuManagementPics" id="management-pics-wrap">
                                                                                <ul class="lvl-2-list floater-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxManagementTeamMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item raised-block more-what"><a class="more-what" href="/company-info/management-team/">
                                                                                                <span class="item-content">
                                                                                                    <img alt="" class="wide-only" height="97" src="<%# Eval("ImageSRC") %>" width="97" />
                                                                                                    <span class="item-text">
                                                                                                        <%# Eval("firstName") %><br />
                                                                                                        <%# Eval("lastName") %></span> </span></a></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <div class="lvl-2-container" id="whyWeAreDiff" runat="server" visible="false">
                                                                            <a class="lvl-2-title" href="/why-we-are-different/">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    WHY WE ARE DIFFERENT<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-text-wrap">
                                                                                <div class="lvl-2-text company-info">
                                                                                    <p>
                                                                                        <asp:Literal ID="ltrWhyWeAreDiff" runat="server"></asp:Literal>
                                                                                    </p>
                                                                                    <a href="/why-we-are-different/" class="more-what">Learn More</a>
                                                                                </div>
                                                                                <!-- lvl-2-text -->
                                                                            </div>
                                                                            <!-- lvl-2-text-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_8 -->
                                                                    <div class="grid_1">
                                                                        <span>&nbsp;</span>
                                                                    </div>
                                                                    <div class="grid_7 omega">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/news/" id="newsLink" runat="server" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    NEWS<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-list-wrap">
                                                                                <ul class="lvl-2-list bloggy-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxNewsMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item"><span class="item-content"><span class="sml-date">
                                                                                                <%# Eval("date") %>
                                                                                                |
                                                                                                <%# Eval("city") %>,
                                                                                                <%# Eval("state") %></span> <a class="title" href="<%# Eval("hrefText") %>">
                                                                                                    <%# Eval("title")%></a> <span class="desc"></span><a class="more-what" href="<%# Eval("hrefText") %>">More</a> </span></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_7 -->
                                                                </div>
                                                                <!-- container_24 -->
                                                                <div class="container_24 clearfix">
                                                                    <div class="grid_7">
                                                                        <div class="lvl-2-container" id="nationalPartners" runat="server">
                                                                            <a href="/company-info/partners/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    PARTNERS<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-list-wrap raised-block" id="partner-pics-wrap">
                                                                                <ul class="lvl-2-list floater-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxPartnersMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item <%# Eval("cssClass") %>"><a href="<%# Eval("url") %>"><span class="item-content">
                                                                                                <img alt="<%# Eval("alt") %>" class="wide-only" height="50" src="<%# Eval("imgSRC") %>"
                                                                                                    width="60">
                                                                                                <span class="narrow-only title">Partner
                                                                                                    <%# Eval("counter") %></span> </span></a></li>

                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <div class="lvl-2-container" id="nationalAboutUsImg" runat="server" visible="false">
                                                                            <asp:Literal ID="aboutUsImg" runat="server"></asp:Literal>
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_6  -->
                                                                    <div class="grid_1">
                                                                        <span>&nbsp;</span>
                                                                    </div>
                                                                    <!-- grid_2 -->
                                                                    <div class="grid_8">
                                                                        <div class="lvl-2-container" id="history" runat="server">
                                                                            <a href="/company-info/history/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    HISTORY<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-text-wrap raised-block">
                                                                                <div class="lvl-2-text">
                                                                                    <CMS:ContentBlock ID="cbHistoryText" runat="server" DoInitFill="false" />
                                                                                </div>
                                                                                <!-- lvl-2-text -->
                                                                            </div>
                                                                            <!-- lvl-2-text-wrap -->
                                                                        </div>
                                                                        <div class="lvl-2-container" id="ourTeamContent" runat="server" visible="false">
                                                                            <a class="lvl-2-title" href="/why-we-are-different/#our_team_main">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    OUR TEAM<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div id="management-pics-wrap" class="lvl-2-list-wrap megaMenuManagementPics">
                                                                                <ul class="lvl-2-list floater-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxOurTeam">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item raised-block more-what"><a class="more-what" href="/why-we-are-different/#our_team_main">
                                                                                                <span class="item-content">
                                                                                                    <img alt="" class="wide-only" height="100" src="<%# Eval("ImageSRC") %>" width="100">
                                                                                                    <span class="item-text">
                                                                                                        <%# Eval("firstName") %><br />
                                                                                                        <%# Eval("lastName") %></span> </span></a></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_8 -->

                                                                    <div class="grid_1">
                                                                        <span>&nbsp;</span>
                                                                    </div>
                                                                    <!-- grid_2 -->
                                                                    <div class="grid_7 omega">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/in-the-media/" id="inTheMediaLink" runat="server" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    IN THE MEDIA<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-list-wrap">
                                                                                <ul class="lvl-2-list bloggy-type clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxInTheMediaMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item"><span class="item-content"><span class="sml-date">
                                                                                                <%# Eval("date") %>
                                                                                                |
                                                                                                <%# Eval("source") %></span> <a class="title" href="<%# Eval("url") %>" target="_blank">
                                                                                                    <%# Eval("title") %></a> <span class="desc"></span><a class="more-what" href="<%# Eval("url") %>" target="_blank">More</a> </span></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->

                                                                    </div>
                                                                    <!-- grid_6 -->

                                                                </div>
                                                                <!-- container_24 -->


                                                            </div>
                                                            <!-- mega-panel -->
                                                        </section>
                                                        <!-- mega-panel-wrap -->
                                                        <!-- END - ABOUT US MEGA PANEL - END - ABOUT US MEGA PANEL - END -->
                                                        <!-- END - ABOUT US MEGA PANEL - END - ABOUT US MEGA PANEL - END -->
                                                    </div>
                                                    <!-- megamenu -->
                                                </div>
                                                <!-- megamenu-wrap -->
                                            </div>
                                            <!--end grid_24-->
                                        </div>
                                        <!--end container_24-->
                                    </li>
                                    <li class="desktop-nav-link no-hover"><a class="insights-link" href="#">
                                        <asp:Literal ID="ltrInSights" runat="server" /></a>
                                        <div class="megamenu-outer-wrap clearfix" style="display: none;">
                                            <div class="">
                                                <div class="megamenu-wrap">
                                                    <div class="megamenu">
                                                        <!-- START - INSIGHT MEGA PANEL - START - INSIGHT MEGA PANEL - START -->
                                                        <!-- START - INSIGHT MEGA PANEL - START - INSIGHT MEGA PANEL - START -->
                                                        <section class="mega-panel-wrap" id="insight-menu">
                                                            <div class="mega-panel">

                                                                <div class="container_24 clearfix">

                                                                    <div class="grid_12">

                                                                        <div class="lvl-2-container">
                                                                            <a href="/briefs-whitepapers/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    ALL BRIEFS &amp; WHITEPAPERS<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->

                                                                            <div class="lvl-2-list-wrap">
                                                                                <ul class="lvl-2-list bloggy-type and-graphic clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxBriefsWHitepapersMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item"><span class="item-content"><a href="<%# Eval("hrefText") %>">
                                                                                                <span class="graphic-wrap">
                                                                                                    <img alt="" class="wide-only" height="110" src="<%# Eval("imgSRC") %>" width="90">
                                                                                                </span><span class="blurb-wrap"><span class="title">
                                                                                                    <%# Eval("title") %></span> <span class="desc">
                                                                                                        <%# Eval("teaserMM") %></span> <span class="more-what">More</span> </span>
                                                                                            </a></span></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_11  -->
                                                                    <div class="grid_12 omega">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/case-studies/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    ALL CASE STUDIES <span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-text-wrap" id="all-case-studies-wrap">
                                                                                <div class="big-case-square-wrap">
                                                                                    <asp:Repeater runat="server" ID="UxBigCaseStudiesMM">
                                                                                        <ItemTemplate>
                                                                                            <a class="raised-block clearfix" href="<%# Eval("hrefText") %>">
                                                                                                <div>
                                                                                                    <img class="stretchy-pic wide-only" alt="" src="<%# Eval("imgSRC") %>" />
                                                                                                </div>
                                                                                                <span class="title-t2"><%# Eval("title") %></span>
                                                                                                <span class="wide-only more-what">More</span>
                                                                                            </a>

                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                                <div class="small-case-squares-wrap">
                                                                                    <asp:Repeater runat="server" ID="UxSmallCaseStudiesMM">
                                                                                        <ItemTemplate>
                                                                                            <a href="<%# Eval("hrefText") %>" class="raised-block clearfix">
                                                                                                <div>
                                                                                                    <img alt="" class="stretchy-pic wide-only" src="<%# Eval("imgSRC") %>" />                                                                                                        
                                                                                                </div>
                                                                                                <span class="title-t4"><%# Eval("title")%></span>
                                                                                                <span class="wide-only more-what">More</span>
                                                                                            </a>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </div>
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_11 -->
                                                                </div>
                                                                <!-- container_24 -->
                                                            </div>
                                                            <!-- mega-panel -->
                                                        </section>
                                                        <!-- mega-panel-wrap -->
                                                        <!-- END - INSIGHT MEGA PANEL - END - INSIGHT MEGA PANEL - END -->
                                                        <!-- END - INSIGHT MEGA PANEL - END - INSIGHT MEGA PANEL - END -->
                                                    </div>
                                                    <!-- megamenu -->
                                                </div>
                                                <!-- megamenu-wrap -->
                                            </div>
                                        </div>
                                    </li>
                                    <li class="desktop-nav-link no-hover"><a class="join-our-team-link" href="#">
                                        <asp:Literal ID="ltrJoinOurTeam" runat="server" /></a>
                                        <div class="megamenu-outer-wrap clearfix" style="display: none;">
                                            <div class="">
                                                <div class="megamenu-wrap">
                                                    <div class="megamenu">
                                                        <!-- START - JOIN OUR TEAM MEGA PANEL - START - JOIN OUR TEAM MEGA PANEL - START -->
                                                        <!-- START - JOIN OUR TEAM MEGA PANEL - START - JOIN OUR TEAM MEGA PANEL - START -->
                                                        <%--<section class="mega-panel-wrap" id="join-menu">--%>
                                                        <section id="find-location-home-menu" class="mega-panel-wrap">
                                                            <div class="mega-panel">
                                                                <div class="container_24 clearfix">
                                                                    <div class="grid_6">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/join-our-team/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    JOIN OUR TEAM<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <CMS:ContentBlock ID="cbWhyWorkWithUs" runat="server" DoInitFill="false" />
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_6  -->
                                                                    <div class="grid_2">
                                                                        <span class="divider">&nbsp;</span>
                                                                    </div>
                                                                    <!-- grid_2 -->
                                                                    <div class="grid_8">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/job-profiles/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    JOB PROFILES<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <div class="lvl-2-list-wrap" id="job-profiles-wrap">
                                                                                <ul class="lvl-2-list bloggy-type and-graphic clearfix">
                                                                                    <asp:Repeater runat="server" ID="UxJoinOurTeamMM">
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item"><span class="item-content raised-block clearfix"><a href="<%# Eval("hrefText") %>">
                                                                                                <span class="graphic-wrap">
                                                                                                    <img alt="" class="stretchy-pic" src="<%# Eval("imgSRC") %>" />                                                                                                       
                                                                                                </span><span class="blurb-wrap"><span class="title"><%# Eval("name") %></span> <span class="desc"><%# Eval("teaserMM") %></span><span class="more-what">More</span>
                                                                                                </span></a></span></li>
                                                                                        </ItemTemplate>
                                                                                    </asp:Repeater>
                                                                                </ul>
                                                                                <!-- lvl-2-list -->
                                                                            </div>
                                                                            <!-- lvl-2-list-wrap -->
                                                                        </div>
                                                                        <!-- lvl-2-container -->

                                                                    </div>
                                                                    <!-- grid_8  -->

                                                                    <div class="grid_2">
                                                                        <span class="divider">&nbsp;</span>
                                                                    </div>
                                                                    <!-- grid_2 -->
                                                                    <div class="grid_6 omega">
                                                                        <div class="lvl-2-container">
                                                                            <a href="/job-search/" class="lvl-2-title">
                                                                                <div class="lvl-2-title-wrap">
                                                                                    FIND A JOB<span class="arrow-plus-minus">&nbsp;</span>
                                                                                </div>
                                                                            </a>
                                                                            <!--lvl-2-title-wrap -->
                                                                            <asp:Panel ID="plnNationalJobs" runat="server">
                                                                                <div class="lvl-2-list-wrap">
                                                                                    <span class="title-t3">Recent Jobs</span>
                                                                                    <asp:ListView ID="lvJobsList" runat="server">
                                                                                        <LayoutTemplate>
                                                                                            <ul class="lvl-2-list bloggy-type clearfix">
                                                                                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                                                                            </ul>
                                                                                        </LayoutTemplate>
                                                                                        <ItemTemplate>
                                                                                            <li class="lvl-2-item">
                                                                                                <span class="item-content">
                                                                                                    <span class="sml-date"><%# DateTime.Parse(Eval("DatePosted").ToString()).ToString("MMM dd, yyyy") %> | <%# Eval("Location") %>.</span>
                                                                                                    <a class="title" href="/job-description/?jobid=<%# Eval("JobId") %>"><%# Eval("Title") %></a>
                                                                                                    <span class="desc"></span>
                                                                                                    <a class="more-what" href="/job-description/?jobid=<%# Eval("JobId") %>">View Job</a>
                                                                                                </span>
                                                                                            </li>
                                                                                        </ItemTemplate>
                                                                                    </asp:ListView>
                                                                                    <!-- lvl-2-list -->
                                                                                </div>
                                                                                <!-- lvl-2-list-wrap -->
                                                                                <div class="lvl-2-text-wrap raised-block">
                                                                                    <CMS:ContentBlock ID="cbAmazingPeople" runat="server" DoInitFill="false" />
                                                                                    <a class="more-what bigger-text" href="/job-search/">Start Your Job Search</a>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="plnLocalJobs" Visible="false" runat="server">
       
                                                                                     <asp:ListView ID="lvJobsListLocal" runat="server">
                                                                                <LayoutTemplate>
                                                                                    <ul class="lvl-2-list bloggy-type clearfix">
                                                                                        <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                                                                    </ul>
                                                                                </LayoutTemplate>
                                                                                <ItemTemplate>
                                                                                    <li class="lvl-2-item">
                                                                                        <span class="item-content">
                                                                                            <span class="sml-date"><%# DateTime.Parse(Eval("DatePosted").ToString()).ToString("MMM dd, yyyy") %> | <%# Eval("Location") %>.</span>
                                                                                            <a class="title" href="<%# Eval("Link") %>"><%# Eval("Title") %></a>
                                                                                            <span class="desc"></span>
                                                                                            <a class="more-what" href="<%# Eval("Link") %>">View Job</a>
                                                                                        </span>
                                                                                    </li>
                                                                                </ItemTemplate>
                                                                            </asp:ListView>
                                                                            <!-- lvl-2-list -->                                                             <%--              <div class="lvl-2-text-wrap raised-block">
                                                                                    <CMS:ContentBlock ID="cbFransJobImage" runat="server" DoInitFill="false" />
                                                                                </div>--%>
                                                                                <div class="lvl-2-text-wrap">
                                                                                    <div class="lvl-2-text-wrap ">
                                                                                        <asp:Literal ID="ltrAmazingPeople" runat="server"></asp:Literal>
                                                                                    </div>
                                                                                    <a href="/job-search/" class="more-what bigger-text">Start Your Job Search</a>
                                                                                </div>
 
                                                                                                                                                     </asp:Panel>
                                                                        </div>
                                                                        <!-- lvl-2-container -->
                                                                    </div>
                                                                    <!-- grid_6 -->
                                                                </div>
                                                                <!-- container_24 -->
                                                            </div>
                                                            <!-- mega-panel -->
                                                        </section>
                                                        <!-- mega-panel-wrap -->
                                                        <!-- END - JOIN OUR TEAM MEGA PANEL - END - JOIN OUR TEAM MEGA PANEL - END -->
                                                        <!-- END - JOIN OUR TEAM MEGA PANEL - END - JOIN OUR TEAM MEGA PANEL - END -->
                                                    </div>
                                                    <!-- megamenu -->
                                                </div>
                                                <!-- megamenu-wrap -->
                                            </div>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                            <!-- desktop-nav -->
                            <div class="mobile-nav">
                                <a id="menu_button">MENU</a>
                            </div>
                            <!-- mobile-nav -->
                        </div>
                        <!-- mobile-nav -->
                    </div>
                    <!-- main-navigation -->
                </div>
                <!-- main-navigation-wrap -->
                <div class="minimenu-wrap">
                    <div id="minimenu">
                        <ul id="navigation_list" role="navigation">
                            <li>
                                <div class="lvl-2-title-wrap">
                                    <a href="#" class="lvl-2-title">
                                        <asp:Literal ID="ltrMblProductServices" runat="server" /></a><a href="#" class="arrow-plus-minus">&#43;</a>
                                </div>
                                <ul class="lvl-3-list">
                                    <li><a href="/product-services/">All
                                        <asp:Literal ID="ltrMblSubProductServices" runat="server" /></a></li>
                                    <asp:ListView ID="listViewProductsServices" runat="server">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <li><a href="<%# Eval("hrefText") %>">
                                                <%# Eval("title") %></a></li>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </ul>
                            </li>
                            <!-- secondary navigation -->
                            <asp:ListView ID="listViewSecondaryNav" runat="server">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <li>
                                        <div class="lvl-2-title-wrap">
                                            <a href="<%# FormatMobileMenuLink(Eval("Href").ToString()) %>" class="lvl-2-title">
                                                <%# Eval("Text") %></a><a href="#" class="arrow-plus-minus">&#43;</a>
                                        </div>
                                        <asp:ListView ID="listViewSecondLevel" DataSource='<%# Eval("Items")%>' runat="server">
                                            <LayoutTemplate>
                                                <ul class="lvl-3-list">
                                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li><a href="<%# FormatMobileMenuLink(Eval("Href").ToString()) %>">
                                                    <%# Eval("Text") %></a></li>
                                            </ItemTemplate>
                                        </asp:ListView>
                                    </li>
                                </ItemTemplate>
                            </asp:ListView>
                            <!--$todo: have to change the links or work on creating some mobile pages -->
                            <li>
                                <div class="lvl-2-title-wrap">
                                    <a href="#request_quote_national" class="mm_utility_link lvl-2-title">Request a Quote</a>
                                </div>
                            </li>
                            <li>
                                <div class="lvl-2-title-wrap">
                                    <a href="#send_file" class="mm_utility_link lvl-2-title">Send a File</a>
                                </div>
                            </li>
                            <li>
                                <div class="lvl-2-title-wrap">
                                    <a href="#franchise_national" class="mm_utility_link lvl-2-title">Franchise Opportunities</a>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- mini-menu-wrap -->
            </div>
            <!-- end grid 20 -->
            <div class="grid_4">
                <asp:Panel ID="pnlNationalFindLocation" runat="server">
                    <div class="find-location-wrap">
                        <ul>
                            <li class="desktop-nav-link no-hover"><a class="find-location-link" id="find_location_link" href="#find_location">Find a
                            <span>Location</span></a>
                                <div class="megamenu-outer-wrap clearfix">
                                    <div class="">
                                        <div class="megamenu-wrap">
                                            <div class="megamenu">
                                                <!-- START - FIND LOCATION MEGA PANEL - START - FIND LOCATION MEGA PANEL - START -->
                                                <!-- START - FIND LOCATION MEGA PANEL - START - FIND LOCATION MEGA PANEL - START -->
                                                <div class="mega-panel-wrap" id="join-menu">
                                                    <!-- section -->
                                                    <!-- mmmmmmmmmmmm FIND LOCATION (NATIONAL) mmmmmmmmmmmmmm -->
                                                   <%-- <script src="../js/find-location.js"></script>--%>
                                                   <%-- <script type="text/javascript">
                                                        $(document).ready(function () {
                                                            loadMap();
                                                        })

                                                    </script>--%>
                                                    <div id="find_location_map_wrapper" class="clearfix">
                                                        <div id="find_location_map_ajaxImg">
                                                            <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                                                        </div>
                                                        <div id="find_location_map"></div>
                                                    </div>
                                                   <%-- <div class="utility_content national clearfix" id="find_location">--%>
                                                       <%-- <div id="find_location_map_wrapper" class="clearfix">
                                                            <div id="find_location_map_ajaxImg">
                                                                <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" />
                                                            </div>
                                                            <div id="find_location_map">
                                                            </div>
                                                        </div>--%>
                                                        <!-- end find_location_map_wrapper -->
                                                        <div id="find_location_searchbox_wrapper">
                                                            <div class="container_24">
                                                                <div class="grid_8">
                                                                    <div id="find_location_searchbox" class="request_quote clearfix">
                                                                        <h2>Find a Location</h2>
                                                                        <div class="prefix_2 grid_20 suffix_2">
                                                                            <div class="form" id="find_location_form">
                                                                                <asp:TextBox ID="txtFindLocation" CssClass="txtFindLocation" runat="server" placeholder="Enter City, State, Zip"></asp:TextBox>
                                                                                <!-- <span class="required">*</span> -->
                                                                                <%--<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtFindLocation"
                                                                                    CssClass="required"></asp:RequiredFieldValidator>--%>
                                                                                <select class="custom-select" id="distance">
                                                                                   <option selected="selected" value="25">25</option>                                        
                                                                                   <option value="50">50</option>
                                                                                   <option value="100">100</option>
                                                                                   <option value="25">200</option>
                                                                                </select>
                                                                                <!-- <span class="required">*</span> -->
                                                                                <asp:TextBox ID="txtFindLocationChoose" CssClass="txtFindLocationChoose" runat="server"
                                                                                    Style="display: none;"></asp:TextBox>
                                                                                <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtFindLocationChoose"
                                                                                    CssClass="required" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                                                <div class="clear">
                                                                                </div>
                                                                                <input type="submit" id="my_location_search_btn" value="Find Now" />
                                                                                <span class="utilityNavFindAllLoc"><a href="/find-locator/#all_locations">See All Locations</a></span>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                            <!-- end rq_pick_location -->
                                                                        </div>
                                                                        <!-- end grid -->
                                                                    </div>
                                                                    <!-- end find_location_searchbox -->
                                                                </div>
                                                                <!--end grid 24-->
                                                            </div>
                                                            <!-- end container_24 -->
                                                        </div>
                                                        <!-- end find_location_searchbox_wrapper -->
                                                        <div class="clear"></div>
                                                        <!-- end find_location_menu_wrapper -->
                                                   <%-- </div>--%>
                                                    <!-- end find_location -->
                                                </div>
                                                <!-- mega-panel-wrap -->
                                                <!-- END - FIND LOCATION MEGA PANEL - END - FIND LOCATION MEGA PANEL - END -->
                                                <!-- END - FIND LOCATION MEGA PANEL - END - FIND LOCATION MEGA PANEL - END -->
                                            </div>
                                            <!-- megamenu -->
                                        </div>
                                        <!-- megamenu-wrap -->
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <!--find_location_wrap -->
                </asp:Panel>
                <asp:Panel ID="pnlLocalContactUs" runat="server" Visible="false">
                    <div class="contact-us-wrap">
                        <ul>
                            <li><a href="/Contact-Us/">Contact <span>Us</span></a></li>
                        </ul>
                    </div>
                    <!--contact-us_wrap -->
                </asp:Panel>
            </div>
            <!--grid 4-->
        </div>
        <!-- end container_24 -->
    </div>
    <!-- end main_nav -->
</div>
<!-- end main_nav_wrapper-->
<div class="clear">
</div>

<script type="text/javascript">
    //$(document).ready(function () {
        // find location code starts here
        $('#my_location_search_btn').click(function (e) {
            e.preventDefault();
            //enter_location           
            var validation = DoValidationForLocation();
            if (validation) {
                var address = $('.txtFindLocation').val();
                address = address.replace(' ', '+');
                var distance = $('#find_location_form .transformSelectDropdown .selected span').text();
                window.location.href = '/Find-Locator/?location=' + address + '&distance=' + distance + '';
            }
        });

        function DoValidationForLocation() {
            var status = false;
            var locationStatus = false;
            var distanceStatus = false;

            if ($('.txtFindLocation').val() != '') {
                locationStatus = true; $('.txtFindLocation').removeClass('requiredField');
            }
            else {
                $('.txtFindLocation').addClass('requiredField');
                locationStatus = false;
            }

            var distanceVal = $('#find_location_form .transformSelectDropdown .selected span').text();
            if (distanceVal != undefined && distanceVal != 'Distance') {
                distanceStatus = true; $('#find_location_form .transformSelect li:first').removeClass('requiredField');
            }
            else {
                $('#find_location_form .transformSelect li:first').addClass('requiredField');
                distanceStatus = false;
            }

            if (locationStatus == false || distanceStatus == false)
                status = false;
            else
                status = true;

            return status;
        }

        //find location code ends here

        $('.btnRequestQuote').click(function (e) {
            var status = false;
            var status1 = false;
            var status2 = false;

            //do validation
            if ($(".terms_conditions input:checkbox").is(':checked')) {
                $('.termsLocal').hide();
                status1 = true;
            }
            else {
                $('.termsLocal').show();
                status1 = false;
            }

            var description = $('.descProjectNational').val();
            if (description != "") {
                //$('.descProjectNational').removeClass('required');
                status2 = true;
            }
            else {
                //$('.desc_project').addClass('requiredField');
                status2 = false;
            }

            if (status1 == false || status2 == false)
                status = false
            else
                status = true;


            return status;
        });
    //});
</script>
