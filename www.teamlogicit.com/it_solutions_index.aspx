<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Secondary.master" AutoEventWireup="true" CodeFile="it_solutions_index.aspx.cs" Inherits="it_solutions_index" %>
<%@ Register Assembly="Ektron.Cms.Controls" Namespace="Ektron.Cms.Controls" TagPrefix="CMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainHeaderPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div class="it_how_work_wrapper clearfix">     	
		<div class="it_how_we_work clearfix">
           <div class="container_24">
      			<div class="grid_24">      				
                    <CMS:ContentBlock ID="cbHowWeWork" runat="server" DoInitFill="false" />                   
                </div><!-- grid 24-->      
      		</div><!--container 24-->
     	</div><!--// it_how_we_work-->		
     </div>
    <div class="clear"></div>
    <div class="our_solutions_wrapper" id="OurSolutions">
        <div class="it_our_solutions clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">featured solutions</h2>
                </div>
            </div>
            <asp:ListView ID="lvSolutionsContent" runat="server">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <ItemTemplate>
                    <%# Eval("startHTML") %>
                    <div class="grid_10 <%# Eval("cssClass") %>_2">
                        <img src="<%# Eval("imgSRC") %>">
                        <h3 class="sub_headline"><%# Eval("title") %></h3>
                        <p><%# Eval("desc") %></p>
                        <div class="square_button"><a href="<%# Eval("hreftext") %>">Learn More</a></div>
                    </div>
                    <%# Eval("endHTML") %>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>    
    <div class="clear"></div>
    <div class="complete_solutions_wrapper" id="CompleteSolutions">
        <div class="it_complete_solutions clearfix">
            <div class="container_24">
                <div class="grid_24">
                    <h2 class="headline">complete solutions</h2>
                    <asp:ListView ID="lvCompleteSolutionsContent" runat="server">
                        <LayoutTemplate>
                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <%# Eval("startHTML") %>
                            <div class="grid_6 <%# Eval("cssClass") %>">
                                <div class="complete_solutions_content">
                                    <img src="<%# Eval("imgSRC") %>">
                                    <span><%# Eval("title") %></span>
                                    <p>
                                        
                                    </p>
                                    <div class="square_button"><a href="<%# Eval("hreftext") %>">More</a></div>
                                </div>
                            </div>
                            <%# Eval("endHTML") %>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!--// it_how_we_work-->
    </div>
    <div class="clear"></div>
    <div data-height="670" data-width="1600" runat="server" data-image="/images/lets_connect_bg.jpg" id="lets_connect" class="img-holder-connect"></div>
    <div class="lets_connect_local clearfix">
        <div class="lets_connect_content">
            <div class="container_24">
                <div class="grid_24">
                    <CMS:ContentBlock ID="cbLetsConnect" runat="server" SuppressWrapperTags="true" DoInitFill="false" />
                </div>
                <!-- grid 24-->
            </div>
            <!--container 24-->
        </div>
        <!-- lets_connect_content -->
    </div>
    <div class="clear"></div>
</asp:Content>

