<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestimonialSlider.ascx.cs" Inherits="UserControls_TestimonialSlider" %>

<div id="large_testimonial_slider">
    <div class="slider-wrapper">
        <asp:ListView ID="lvTestimonials" runat="server">
            <LayoutTemplate>
                <ul class="bxslider-large">
                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                </ul>
            </LayoutTemplate>
            <ItemTemplate>
                <li class="testimonial-quote">
                    <img style='<%# Eval("PicturePath") == null || Eval("PicturePath").ToString() == "" ? "display:none;" : "display:block;" %>' class="testimonialImg" width="125" height="125" src="<%# Eval("PicturePath") %>" alt="Testimonial" />
                    <h3>/ <%# Eval("Statement") %></h3>
                    <span class="testimonial-author">- <%# Eval("FirstName") %> <%# FormatLastName(Eval("FirstName").ToString(), Eval("LastName").ToString()) %>
                        <%# FormatTitleCompany(Eval("Title") , Eval("Organization")) %>
                        <%# FormatContactDetails( Eval("EmailAddress"),Eval("PhoneNumber")) %>
                    </span>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </div>
    <!--//slider_wrapper-->
</div>
<!--//large_testimonial_slider-->
