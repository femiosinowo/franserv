<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageBanners.ascx.cs" Inherits="UserControls_HomePageBanners" %>

<%--<script type='text/javascript'>
    var liElement = document.getElementsByTagName('li');
    for (index = liElement.length - 1; index >= 0; index--) {
        liElement[index].style.background-image = <%=this.backImageSRC %>;
    }
</script>--%>

<!-- mmm Rotating Banner (both) mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm Rotating Banner (both) mmm -->
<div class="main_rotator_wrapper  clearfix">
   
    <div class="main_rotator">
        <!-- rotator section mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm rotator section -->
        <div id="top_slider">
      
            <div class="flexslider">
                <ul class="slides">
                    <asp:Repeater runat="server" ID="UxHomeSlider">
                        <ItemTemplate>
                            <li id="slide_<%# Eval("index") %>"> 
                                <%--<div class="slider-background-image" <%#Eval("backImageSRC") %>>--%>
                                <div class="slide_container">
                                    <%--<div class="flex-image" style="<%# Eval("styleimage") %>">                                        
                                        <a href="<%# Eval("url") %>" <%# Eval("target") %>><%# Eval("imageSRC") %></a>
                                    </div>--%>
                                    
                                    <div class="flex-image" >                                        
                                        <img src='<%# Eval("backImageSRC") %>'/>
                                    </div>

                                    <!-- end flex-image -->
                                    <div class="flex-caption" style="<%# Eval("stylecaption") %>">
                                        <a href="<%# Eval("url") %>" <%# Eval("target") %> style="text-decoration:none !important;"><h4><%# Eval("text") %></h4></a>
                                       <p>
                                           <%# Eval("subtitle") %>
                                       <p>
                                       <a class="cta-button-text" href="<%# Eval("url") %>" <%# Eval("target") %>>
					                    <div class="cta-button-wrap white-btn">
						                    <span>Learn more</span>
						                </div>
					                </a>
                                    </div>
                                    <!--end class flex-caption-->
                                </div>
                                <!--end class slide_container-->
                               <%-- </div>--%>
                                <!--end class slide-background-image-->
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <!--end flexslider-->
        </div>
        <!--end #top_slider section-->
        <div class="clear"></div>
    </div>
    <!-- end main_rotator -->
</div>
<!--end main_rotator_wrapper-->
<div class="clear"></div>
