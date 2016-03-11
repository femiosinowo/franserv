<%@ Control Language="C#" AutoEventWireup="true" CodeFile="JobSearch.ascx.cs" Inherits="UserControls_JobSearch" %>

<link type="text/css" href="/css/deprecated/jquery.datatable.css" rel="stylesheet" />
<script type="text/javascript" src="/js/jquery-datatable.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var oTable = $('#center-jobs').dataTable({
            "aaSorting": [], //disabling default sort
            "sPaginationType": "full_numbers",
            "aoColumns": [null, null, null, null, { "bSearchable": true, "bVisible": false }],
            "fnDrawCallback": function () {
                $('#center-jobs_previous').addClass('cta-button-text');
                $("#center-jobs_previous").html('<span>Prev</span>');
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
<div class="job_search_wrapper  clearfix">
        <div class="job_search clearfix">
            <div class="container_24">
                <div class="grid_24" id="job_search">
                    <div id="job_search_table_header" class="grid_24 table_hf">
                        <h2>Find a Job</h2>
                        <div class="form" id="job_search_form">
                            <fieldset id="searchbox">
                                <input type="search" id="keyword" placeholder="Enter Keyword" name="keyword"/>
                                <input type="search" id="location" placeholder="Enter Location" name="location"/>
                            </fieldset>
                            <fieldset id="job_search_type">
                                <input type="radio" id="search_all" name="search_type" checked="checked"/>
                                <label for="search_all">All</label>
                                <input type="radio" name="search_type" id="search_ft"/>
                                <label for="search_ft">Full Time</label>
                                <input type="radio" name="search_type" id="search_pt"/>
                                <label for="search_pt">Part Time</label>
                            </fieldset>
                        </div>
                    </div>
                    <!-- end job_search_table_header -->
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
                        <asp:ListView ID="lvJobs" runat="server">
                            <LayoutTemplate>
                                <tbody>
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                 </tbody>
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
                    </table>
                    <div class="clear"></div>
                    <div id="job_search_table_footer" class="grid_24 table_hf">
                        <div id="prev_btn" class="grid_3 table_nav_btn">
                            <div id="previousBtn" class="cta-button-wrap purple dark_bg">                                
                            </div>
                        </div>
                        <!-- table_nav_btn -->
                        <div class="grid_18">
                            <div class="table_pager">                                
                            </div>
                            <!-- end table_pager -->
                        </div>
                        <!-- end grid_18 -->
                        <div id="next_btn" class="grid_3 table_nav_btn">
                            <div id="nextBtn" class="cta-button-wrap purple dark_bg">                                
                            </div>
                        </div>
                        <!-- table_nav_btn -->
                    </div>
                    <!-- end job_search_table_footer -->
                </div>
                <!--end grid_24-->
            </div>
            <!-- end container_24  -->
        </div>
        <!--end job_search -->
    </div>