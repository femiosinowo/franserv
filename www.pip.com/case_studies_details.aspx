<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.master" AutoEventWireup="true"
    CodeFile="case_studies_details.aspx.cs" Inherits="case_studies_details" %>

<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="cphMainContent" runat="Server">
    <div id="case-studies">
        <!-- mmm Header Image (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Header Image mmm -->
        <div class="header_image_wrapper clearfix">
            <div class="header_image_content">
                <div class="header_image">
                    <CMS:ContentBlock ID="cbCaseStudiesDetailsHeaderImage" runat="server" />
                    <%--<img src="images/headers/header_2.jpg" alt="">--%>
                </div>
                <!-- header image-->
            </div>
            <!-- end header_image_content -->
        </div>
        <!-- end header_image_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Sub Nav (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Sub Nav (both) mmm -->
        <div class="sub_navigation_wrapper  clearfix">
            <div class="sub_navigation insights">
                <div id="sub_navigation">
                    <div class="menu-title-block">
                        <div id="insights-menu-h2">
                            <h2 id="menu-case-studies">
                                Case Studies</h2>
                            <h2 id="menu-briefs-whitepapers">
                                Briefs &amp; Whitepapers</h2>
                        </div>
                    </div>
                    <div class="menu-items-block">
                        <div class="lvl-2-title-wrap" id="mobile-nav-header">
                            <a href="#" class="lvl-2-title" id="page-title"></a><a href="#" class="arrow-plus-minus">
                                &nbsp;</a>
                        </div>
                        <!-- end lvl-2-title-wrap -->
                        <!-- About Us National -->
                        <ul id="insights-desktop-nav">
                            <li class="case-studies-link active"><a href="/Case-Studies/">Case Studies</a></li>
                            <li class="briefs-whitepapers-link"><a href="/Briefs-Whitepapers/">Briefs &amp;
                                Whitepapers</a></li>
                        </ul>
                    </div>
                </div>
                <!--end container_24-->
            </div>
            <!-- end sub_nav -->
        </div>
        <!-- end sub_nav_wrapper-->
        <div class="clear">
        </div>
        <!-- mmm Case Studies Details mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Case Studies Details Info mmm -->
        <div class="case_studies_content_wrapper main_about_us clearfix" style="background-color: white;">
            <div class="case_studies_content clearfix">
                <div class="container_24" id="case_studies_content">
                    <div class="grid_6 headline-block col-height-equal">
                        <div class="int-headline-block int-block-1">
                            <div class="headline-content-outer">
                                <div class="headline-content-inner">
                                    <span class="headline-block-icon-black"></span>
                                    <CMS:ContentBlock ID="cbCaseStudiesDetailsSideContent" runat="server" />
                                </div>
                                <!--headline content-->
                            </div>
                            <!--headline content-->
                        </div>
                    </div>
                    <!-- headline block-->
                    <div class="grid_18 content-block col-height-equal">
                        <div class="prefix_1 grid_22 suffix_1 clearfix" id="case_studies_details_main">
                            <asp:Repeater ID="UxCaseStudies" runat="server">
                                <ItemTemplate>
                                    <div class="case_studies_details_header">
                                        <div class="grid_15">
                                            <h2>
                                                <%#Eval("title") %></h2>
                                            <p>
                                                <%#Eval("desc") %></p>
                                        </div>
                                        <div class="grid_9">
                                            <script type="text/javascript">
                                                function myPrintFunction() {
                                                    window.print();
                                                }
                                            </script>
                                            <ul class="ps-btns">
                                                <li class="print-btn"><a onclick="myPrintFunction();" href="javascript:void('0')"><span>Print</span></a></li>
                                                <li class="email-btn"><a href="javascript:void('0')"><span class="st_email"></span></a></li>
                                                <li class="share-btn"><a href="javascript:void('0')"><span class="st_sharethis_custom">Share</span></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="clear">
                            </div>
                            <div id="scribd_widget_container">
                                <div id='embedded_doc'>
                                    <%--<a href='http://www.scribd.com'>Scribd</a></div>
                                       <img src="images/scribd-placeholder.jpg" alt="Scribd Placeholder">--%>
                            </div>
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
                        <!--end grid_24-->
                        <div class="clear">
                        </div>
                    </div>
                    <!-- grid 18 content block-->
                </div>
            <!--end container_24-->
        </div>
        <!--end iinsights_case_studies_details -->
    </div>
    <!-- end insights_case_studies_details_wrapper -->
    <div class="clear">
    </div>
    </div>
</asp:Content>
