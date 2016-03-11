<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearch.ascx.cs" Inherits="UserControls_JobSearch" EnableViewState="true" %>

<link type="text/css" href="/css/jquery.datatable.css" rel="stylesheet" />
<script type="text/javascript" src="/js/jquery-datatable.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var oTable = $('#center-jobs').dataTable({
            "aaSorting": [], //disabling default sort
            "sPaginationType": "full_numbers",
            "aoColumns": [null, null, null, null, { "bSearchable": true, "bVisible": false}],
            "fnDrawCallback": function () {
                $('#center-jobs_previous').addClass('cta-button-text');
                $("#center-jobs_previous").html('<span>Prev<span>ious</span></span>');
                $("#center-jobs_previous").appendTo("#previousBtn");

                $('#center-jobs_paginate').appendTo('.table_pager');

                $('#center-jobs_next').addClass('cta-button-text');
                $('#center-jobs_next').html('<span>Next</span>');
                $("#center-jobs_next").appendTo("#nextBtn");
            }
        });

        $("input#location").keyup(function (event) {
            SearchByLocation(this.value);
        });

        $("input#keyword").keyup(function (event) {
            oTable.fnFilter(this.value, null);
        });

        $('input:radio[id="search_all"]').change(function () {
            if ($(this).is(':checked')) {
                oTable.fnFilter('', 4);
            }
        });

        $('input:radio[id="search_ft"]').change(function () {
            if ($(this).is(':checked')) {
                oTable.fnFilter('full time', 4);
            }
        });

        $('input:radio[id="search_pt"]').change(function () {
            if ($(this).is(':checked')) {
                oTable.fnFilter('part time', 4);
            }
        });

        $('#statementTxtJobSearch').click(function () {
            var searchTerm = $(this).find('span#jobCategory').attr('class');
            $('input#location').val(searchTerm);
            SearchByJobField(searchTerm);
        });

        function SearchByJobField(searchTerm) {
            var colIndex = 1;
            oTable.fnFilter(searchTerm, colIndex);
        }

        function SearchByLocation(searchTerm) {
            var colIndex = 2;
            oTable.fnFilter(searchTerm, colIndex);
        }
    });
</script>
<div class="job_search_content_wrapper main_about_us clearfix" style="background-color: White;">
    <div class="job_search_content clearfix">
        <div id="job_search_content" class="container_24">
            <div class="grid_6 headline-block col-height-equal">
                <div class="int-headline-block headline-block-black int-block-1">
                    <div class="headline-content-outer">
                        <div class="headline-content-inner">
                            <span class="headline-block-icon-black"></span>
                            <h2 class="headline headline-search">
                                Refine Search</h2>
                            <h2 class="headline headline-other">
                                Search Jobs</h2>
                        </div>
                        <!--headline content-->
                    </div>
                    <!--headline content-->
                </div>
            </div>
            <!--grid_6-->
            <div class="grid_18 content-block col-height-equal">
                <div class="form" id="job_search_form">
                    <fieldset id="searchbox">
                        <input type="search" id="keyword" placeholder="Enter Keyword" name="keyword" />
                        <input type="search" id="location" placeholder="Enter Location" name="location" />
                    </fieldset>
                    <fieldset id="job_search_type">
                        <input type="radio" id="search_all" name="search_type" checked="checked" />
                        <label for="search_all">
                            All</label>
                        <input type="radio" name="search_type" id="search_ft" />
                        <label for="search_ft">
                            Full Time</label>
                        <input type="radio" name="search_type" id="search_pt" />
                        <label for="search_pt">
                            Part Time</label>
                    </fieldset>
                </div>
            </div>
            <!--content block-->
            <div class="clear">
            </div>
            <div class="job_search_results"> 
                <asp:ListView ID="lvJobs" runat="server">
                    <LayoutTemplate>
                        <table id="center-jobs">
                            <thead>
                                <tr>
                                    <th>Position<span></span></th>
                                    <th>Job Field<span></span></th>
                                    <th>Location<span></span></th>
                                    <th>Posted<span></span></th>
                                    <th>JobType<span></span></th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><a href="<%# Eval("href") %>"><%# Eval("Title") %></a></td>
                            <td><%# Eval("ProfileType") %></td>
                            <td><%# Eval("Location") %></td>
                            <td><%# Eval("PostedDate") %></td>
                            <td><%# Eval("JobType") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>   
                <div class="clear"></div>             
                <div id="job_results_footer" class="grid_24 table_hf">
                    <div class="grid_3 table_nav_btn" id="prev_btn">
                        <div id="previousBtn" class="cta-button-wrap white-orange-btn">                            
                        </div>
                    </div>
                    <!-- table_nav_btn -->
                    <div class="grid_18">
                        <div class="table_pager">
                        </div>
                        <!-- end table_pager -->
                    </div>
                    <!-- end grid_18 -->
                    <div class="grid_3 table_nav_btn" id="next_btn">
                        <div id="nextBtn" class="cta-button-wrap white-orange-btn">                            
                        </div>
                    </div>
                    <!-- table_nav_btn -->
                </div>
                <!-- end job_search_table_footer -->
            </div>
            <!--job_search_content-->
        </div>
        <!--end join_team_content -->
    </div>
    <!-- end join_team_content_wrapper -->
    <div class="clear">
    </div>
</div>
<!-- end container_24  -->

<!--end job_search -->
