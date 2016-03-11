<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true" CodeFile="portfolio.aspx.cs" Inherits="portfolio" %>

<%@ Register TagPrefix="cms" Namespace="Ektron.Cms.Controls" Assembly="Ektron.Cms.Controls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMainHead" runat="Server">
    <script type="text/javascript" src="/js/SirSpeedy.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('.portfolio-images').length) {
                $('.portfolio-images').each(function () {
                    $(this).find('.grid_6:first').addClass('alpha');
                    $(this).find('.grid_6:last').addClass('omega');
                });
            }

            //check querystring
            var url = window.location.href;
            var setId = GetQueryStringParam('setid', url);
            if ((setId) && (setId != 0)) {
                GetImages(setId);
                $('#' + setId).click();
            }

            $('.setFirstImg a').on("click", function (e) {
                var setId = $(this).attr("id");
                $('.waitImg').show();
                GetImages(setId);
                $('.waitImg').hide();
            });


            function GetQueryStringParam(name, url) {
                if (!url) {
                    url = window.location.href;
                }
                var results = new RegExp('[\\?&]' + name + '=([^&#]*)').exec(url);
                if (!results) {
                    return 0;
                }
                return results[1] || 0;
            }

            function GetImages(sId) {
                $('.portfolioImages').html('');
                $.ajax({
                    type: "POST",
                    url: "/portfolio.aspx/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: "{'setId':'" + sId + "'}",
                    async: false,
                    cache: false,
                    success: function (response) {
                        if ((response) && (response.d)) {
                            $('.portfolioImages').html(response.d);
                        }
                    },
                    error: function (request, status, error) {
                        // $('#top-loading-image').hide();                
                    }
                });
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div class="site_container local" id="portfolio">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <%--<img src="/images/headers/header_1.jpg" alt=""> --%>
                    <cms:ContentBlock ID="cbPortfolioHeaderImage" runat="server" DynamicParameter="id"
                        CacheInterval="300" />
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image_content -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear"></div>
        <!-- mmm Portfolio Content mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Portfolio Content mmm -->
        <div class="portfolio_content_wrapper clearfix">
            <div class="portfolio_content clearfix">
                <div id="portfolio_content" class="container_24">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block headline-block-black int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <cms:ContentBlock ID="cbPotfolioSideContent" runat="server" />
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!-- end grid 6 -->
                    <div class="grid_18 content-block col-height-equal">
                        <div class="grid_24" id="portfolio_main">
                            <asp:ListView ID="lvPhotos" runat="server">
                                <ItemTemplate>
                                    <div class="grid_8 cs_container">
                                        <div class="cs_image" href="#">
                                            <img alt="<%# Eval("Title") %>" src="<%# Eval("PhotosetSmallUrl") %>" />
                                            <div class="cs_image_content_wrapper">
                                                <div class="cs_image_content setFirstImg">
                                                    <h3><%# Eval("Title") %></h3>
                                                    <p><%# Eval("Description") %></p>
                                                    <a class="fancybox" data-content-id="content-<%# Eval("PhotosetId") %>" id="<%# Eval("PhotosetId") %>" rel="group-<%# Eval("PhotosetId") %>" href="<%# GetFirstImageOfAlbum(Eval("PhotosetId").ToString()) %>"><%--rel="group-<%# Eval("PhotosetId") %>"--%>
                                                        <%--<img alt="More Details" src="/images/button_whitebg-red_arrow.png" />--%>
                                                        <div class="cta-button-wrap white-orange-btn">
                                                            <span>View</span>
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="cs_image_content_desc_wrapper">
                                            <div class="cs_image_content" id="content-<%# Eval("PhotosetId") %>">
                                                <span class="waitImg" style="display: none;">
                                                    <img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" /></span>
                                                <div class="cs_image_content_desc">
                                                    <h3><%# Eval("Title") %></h3>
                                                    <p><%# Eval("Description") %></p>
                                                </div>
                                                <%# GetFirstImageTitleDesp(Eval("PhotosetId").ToString()) %> 
                                                <!-- end cs_image_01_content -->
                                            </div>
                                            <div style="display:none;" class="portfolioImages"></div>
                                            <!-- end cs_image_01_content_wrapper -->
                                        </div>
                                        <!-- end cs_image_01 -->

                                    </div>
                                    <!-- grid_8 -->
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
                    <!-- end grid 18 -->
                    <div class="clear"></div>
                    <div class="load_more_wrapper" id="loadMorePhotos" runat="server">
                        <div class="cta-button-wrap purple">
                            <asp:LinkButton ID="btnLoadMore" runat="server" CssClass="cta-button-text" OnClick="btnLoadMore_Click"><span>LOAD MORE</span></asp:LinkButton>
                            <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
                        </div>
                        <!-- end cta-button-wrap -->
                    </div>
                    <!-- end load_more_wrapper -->
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
</asp:Content>
