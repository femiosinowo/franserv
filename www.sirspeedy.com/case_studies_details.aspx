<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="case_studies_details.aspx.cs" Inherits="case_studies_details" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <!-- mmm Subpage Tagline  mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Tagline mmm -->
    <div class="subpage_tagline_wrapper  clearfix">
        <div class="subpage_tagline case_studies">
            <div class="container_24">
                <div class="prefix_1 grid_22 suffix_1">
                    <!-- Case Studies -->
                   <CMS:ContentBlock ID="cbTagLine" runat="server" DefaultContentID="1194" CacheInterval="300" />
                </div>
                <!--end refix_1 grid_22 suffix_1 -->
            </div>
            <!-- end container_24 -->
        </div>
        <!-- end main_tagline -->
    </div>
    <!-- end main_tagline_wrapper -->
    <div class="clear">
    </div>
    <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
    <div class="sub_navigation_wrapper clearfix">
        <div class="sub_navigation case_studies">
            <div class="container_24">
                <div class="grid_24">
                    <div class="lvl-2-title-wrap" id="mobile-nav-header">
                        <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                            &nbsp;</a>
                    </div>
                    <!-- end lvl-2-title-wrap -->
                    <!-- Insights -->
                    <ul id="insights-desktop-nav">
                        <li class="case-studies-link active"><a href="/case-studies/">Case Studies</a></li>
                        <li class="briefs-wp-link"><a href="/briefs-whitepapers/">Briefs &amp; Whitepapers</a></li>
                    </ul>
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!-- end sub_nav -->
    </div>
    <!-- end sub_nav_wrapper-->
    <div class="clear">
    </div>
    <!-- mmm Case Studies Details mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Case Studies Details Info mmm -->
    <div class="insights_case_studies_details_wrapper  clearfix">
        <div class="insights_case_studies_details main_content clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <script type="text/javascript">
                        function myPrintFunction() {
                            window.print();
                        }
                    </script>
                    <h2 class="bottom-divider">
                        <asp:Literal ID="ltrTitle" runat="server"></asp:Literal><span><asp:Literal ID="ltrDescription" runat="server"></asp:Literal></span></h2>
                    <div id="scribd_widget_container">
                        <!-- <div id='embedded_doc'><a href='http://www.scribd.com'>Scribd</a></div> -->                   
                        <script type="text/javascript" src='http://www.scribd.com/javascripts/scribd_api.js'></script>
                        <div id='embedded_doc'><a href='http://www.scribd.com'>Scribd</a></div>
                        <script type='text/javascript'>
                            var scribd_doc = scribd.Document.getDoc(<%=this.uploadDocId%>, <%=this.accesskey%>);
                            var onDocReady = function (e) {
                                scribd_doc.api.setZoom(.5);
                            }
                            scribd_doc.addEventListener('docReady', onDocReady);
                            scribd_doc.addParam('jsapi_version', 2);
                            scribd_doc.addParam('height', 600);
                            scribd_doc.addParam('width', 400);
                            scribd_doc.addParam('public', true);
                            scribd_doc.write('embedded_doc');
                        </script>
                    </div>
                    <!-- end scribd_widget_container -->
                    <div class="email_print_share white">
                        <ul>
                            <li><a onclick="myPrintFunction();">
                                <img alt="Print" src="/images/doc-print-white.png"></a></li>
                            <li><span class='st_email'></span>
                                <li><span class="st_sharethis_custom"></span></li>
                        </ul>
                    </div>
                    <!-- end email_print_share white -->
                </div>
                <div class="grid_24">
                    
                </div>
                <!--end grid_24-->
            </div>
            <!--end container_24-->
        </div>
        <!--end iinsights_case_studies_details -->
    </div>
    <!-- end insights_case_studies_details_wrapper -->
    <div class="clear">
    </div>
</asp:Content>
