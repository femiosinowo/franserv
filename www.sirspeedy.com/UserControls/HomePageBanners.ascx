<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HomePageBanners.ascx.cs" Inherits="UserControls_HomePageBanners" %>
<!-- ****Please make sure vary by custom parameter is unique to the user control*** -->
<%@ OutputCache Duration="21600" VaryByParam="None" VaryByCustom="HomePageBanners" %>

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
                            <li id="slide_<%# Eval("index") %>"> <%--<%#Eval("backImageSRC") %>--%>
                                <div class="slider-background-image" <%#Eval("backImageSRC") %>>
                                <div class="slide_container">                                    
                                    <div class="flex-caption">
                                        <h4><a href="<%# Eval("url") %>" target="<%# Eval("target") %>" style="color:#ffffff;"><%# Eval("text") %></a></h4>
                                       <div class="cta">
                                           <%# Eval("subtitle") %>
                                       </div>
                                    </div>
                                    <!--end class flex-caption-->
                                </div>
                                <!--end class slide_container-->
                                </div>
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
