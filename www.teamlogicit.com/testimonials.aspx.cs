using System;
using System.Linq;
using System.Text;
using TeamLogic.CMS;

public partial class testimonials : System.Web.UI.Page
{
    private int defaultCount = 10;
    public string HowWeCanHelpBackgroundImage = "/images/here_to_help_testimonials_bkg.jpg";
       
    protected void Page_Init(object sender, EventArgs e)
    {
        cbTestimonialsIntro.DefaultContentID = ConfigHelper.GetValueLong("TestimonialPageDescriptionCId");
        cbTestimonialsIntro.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbTestimonialsIntro.Fill();

        cbHereToHelp.DefaultContentID = ConfigHelper.GetValueLong("TestimonialHowWeCanHelpYouCId");
        cbHereToHelp.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbHereToHelp.Fill();

        if (cbHereToHelp.EkItem != null && !string.IsNullOrEmpty(cbHereToHelp.EkItem.Image) && cbHereToHelp.EkItem.Image.ToLower().IndexOf("workarea") <= -1)
            HowWeCanHelpBackgroundImage = cbHereToHelp.EkItem.Image;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetTestimonials(defaultCount);
        }

        here_to_help_img.Attributes.Add("data-image", HowWeCanHelpBackgroundImage);
        here_to_help_img.Attributes.Add("data-image-mobile", HowWeCanHelpBackgroundImage);
    }
    
    protected void LoadMoreLinkButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            hdnPreviousCount.Value = hdnDisplayCount.Value;
            int requestCount;
            int.TryParse(hdnDisplayCount.Value, out requestCount);
            requestCount = requestCount + defaultCount;
            this.GetTestimonials(requestCount);

            //to overcome the overlapping of the bottom image with new content loaded.
            string script = "var iScroll = $(window).scrollTop();iScroll = iScroll + 200;$('html, body').animate({ scrollTop: iScroll}, 1000);";
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "appendLocationId", "<script type=\"text/javascript\">" + script + "</script>", false);
        }
    }

    private void GetTestimonials(int reqCount)
    {
        var allCenterTestimoials = SiteDataManager.GetTestimonials();
        if(allCenterTestimoials != null && allCenterTestimoials.Any())
        {
            var testimonials = from t in allCenterTestimoials
                               select new
                               {
                                   FirstName = t.FirstName,
                                   LastName = t.LastName,
                                   Title = t.Title != null ?  t.Title : "",
                                   ImagePath = t.PicturePath != null && t.PicturePath != string.Empty ? "<img src=\"" + t.PicturePath + "\" alt=\"\" width=\"125\" height=\"125\" />" : "",
                                   Statement = t.Statement,
                                   submittedDate = t.Created_Date,
                                   company=t.Organization,
                                   phonenumber = t.PhoneNumber,
                                   emailAdress= t.EmailAddress
                               };
            uxTestimonials.DataSource = testimonials.Take(reqCount);
            uxTestimonials.DataBind();

            hdnDisplayCount.Value = reqCount.ToString();
            if (testimonials.Count() <= reqCount)
                loadMoreNews.Visible = false;           
        }
        else
        {
            pnlNoTestimonials.Visible = true;
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