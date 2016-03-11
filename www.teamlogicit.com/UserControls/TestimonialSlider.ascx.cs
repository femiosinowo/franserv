using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TeamLogic.CMS;

public partial class UserControls_TestimonialSlider : System.Web.UI.UserControl
{    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.FillTestimonials();
        }
    }

    public void FillTestimonials()
    {
        var allTestimonials = SiteDataManager.GetTestimonials();
        if (allTestimonials != null && allTestimonials.Any())
        {
            var selectedTestimonals = allTestimonials.Where(x=>x.IsAboutUs == true).Take(4);
            lvTestimonials.DataSource = selectedTestimonals;
            lvTestimonials.DataBind();
        }
    }

    protected string FormatLastName(string firstName, string lastName)
    {
        return Utility.FormatTestimonialLastName(firstName, lastName);
    }

    protected string FormatTitleCompany(object title, object company)
    {
        return Utility.FormatTestimonialTitleAndCompany(title, company);
    }

    protected string FormatContactDetails(object emailAdress, object phonenumber)
    {
        return Utility.FormatTestimonialContactDetails(emailAdress, phonenumber);
    }
}