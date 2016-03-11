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
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline portfolio">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Local - Portfolio  -->
                    <cms:ContentBlock ID="cbPortfolioDescp" runat="server" DoInitFill="false" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>

    <div class="insights_case_studies_wrapper  clearfix">
        <div class="insights_case_studies clearfix portfolio">                      
            <div class="container_24">
                <div class="grid_24">                    
                    <asp:ListView ID="lvPhotos" runat="server" >                                        
                        <ItemTemplate>
                             <div class="grid_6 cs_container">
                                <a class="cs_image" href="#">
                                    <img alt="<%# Eval("Title") %>" src="<%# Eval("PhotosetSmallUrl") %>" />
                                </a>
                                <!-- end cs_image_01 -->
                                <div class="cs_image_content_wrapper">
                                    <div id="content-<%# Eval("PhotosetId") %>" class="cs_image_content setFirstImg">
                                        <a class="fancybox" data-content-id="content-<%# Eval("PhotosetId") %>" id="<%# Eval("PhotosetId") %>" rel="group-<%# Eval("PhotosetId") %>" href="<%# GetFirstImageOfAlbum(Eval("PhotosetId").ToString()) %>">
                                            <img alt="More Details" src="/images/button_whitebg-red_arrow.png" />
                                        </a>
                                        <span class="waitImg" style="display:none;"><img src="/Workarea/images/application/ajax-loader_circle_lg.gif" alt="ajax-loader" /></span>                                        
                                         <div class="cs_image_content_desc">
                                            <h3><%# Eval("Title") %></h3>
                                            <p><%# Eval("Description") %></p>
                                        </div>
                                        <%# GetFirstImageTitleDesp(Eval("PhotosetId").ToString()) %>
                                        <!-- end cs_image_content_desc -->                                        
                                    </div>                                    
                                    <div style="display:none;" class="portfolioImages"></div>
                                    <!-- end cs_image_01_content -->
                                </div>
                                <!-- end cs_image_01_content_wrapper -->
                            </div>
                            <!-- grid_6 -->
                        </ItemTemplate>
                    </asp:ListView>                    
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
            <div class="clear"></div>
            <div class="load_more_wrapper" id="loadMorePhotos" runat="server">
                <div class="cta-button-wrap purple">                    
                    <asp:LinkButton ID="btnLoadMore" runat="server" CssClass="cta-button-text" OnClick="btnLoadMore_Click" ><span>LOAD MORE</span></asp:LinkButton>
                    <asp:HiddenField ID="hdnDisplayCount" runat="server" Value="0" />
                </div>
                <!-- end cta-button-wrap -->
            </div>
            <!-- end load_more_wrapper -->
        </div>
        <!--end insights_case_studies -->
    </div>    
</asp:Content>
