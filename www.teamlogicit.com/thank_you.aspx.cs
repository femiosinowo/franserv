using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TeamLogic.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.IO;

public partial class thank_you : PageBase
{
    /// <summary>
    /// Page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string eventTarget = Convert.ToString(Request.Params.Get("__EVENTTARGET"));
        string eventArgument = Convert.ToString(Request.Params.Get("__EVENTARGUMENT"));

        if (eventArgument == "onClickLogOut")
            btnLogout_Click(sender, e);

        if (Session["username"] != null)
            btnLogOutSection.Visible = true;


        if(!Page.IsPostBack)
        {
            string type = (Request.QueryString["type"] != null) ? Request.QueryString["type"] : String.Empty ;

            if (type.Equals("logout"))
            {
                log_out.Visible = false;
            }

            this.ProcessThanksMessage();           
        }
    }

    /// <summary>
    /// to process the thank you message as per query string
    /// </summary>
    private void ProcessThanksMessage()
    {        
        long contentId = ConfigHelper.GetValueLong("ThankYouParagraphContentId");//default generic message

        if (Request.QueryString.HasKeys())
        {
            if (!string.IsNullOrEmpty(Request.QueryString["type"]))
            {
                string type = Request.QueryString["type"];
                if (type == "requestaquote")
                {
                    contentId = ConfigHelper.GetValueLong("RequestQuoteThanksContentId");
                }
                else if (type == "jobapp")
                {
                    contentId = ConfigHelper.GetValueLong("ApplyForJobThanksContentId");
                }
                else if (type == "sendafile")
                {
                    contentId = ConfigHelper.GetValueLong("SendAFileThanksContentId");
                }
                else if (type == "contactUs")
                {
                    contentId = ConfigHelper.GetValueLong("ContactUsThanksContentId");
                }
                else if (type == "whitepaper")
                {
                    contentId = ConfigHelper.GetValueLong("WhitepaperThanksContentId");
                    if (!string.IsNullOrEmpty(Request.QueryString["pdfPath"]))
                    {
                        string pdfPath = Request.QueryString["pdfPath"];
                        iframeFile.Src = "/pdfDownload.aspx?pdfPath=" + pdfPath;                       
                    }
                }
                else if(type == "subscribe")
                {
                    contentId = ConfigHelper.GetValueLong("SubscribeThanksContentId");
                }
                //else if (type == "requestconsultation")
                //{
                //    contentId = ConfigHelper.GetValueLong("RequestConsultationThanksContentId");
                //}
                else
                {
                    contentId = ConfigHelper.GetValueLong("ThankYouParagraphContentId");
                }
            }
        }

        cbContentTitle.DefaultContentID = ConfigHelper.GetValueLong("ThankYouSubTitleContentId");
        cbContentTitle.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbContentTitle.Fill();

        cbThankYouContent.DefaultContentID = contentId;
        cbThankYouContent.CacheInterval = ConfigHelper.GetValueLong("longCacheInterval");
        cbThankYouContent.Fill();
    }
            

    /// <summary>
    /// log out button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLogout_Click(object sender, EventArgs e)
    {

        string selectedCenter = FransDataManager.GetFranchiseId();

        Session.Remove("username");
        Session.Remove("userFirstName");
        Session.Remove("userLastName");
        Session.Remove("useremail");
        Session.Remove("userJobTitle");
        Session.Remove("userCompanyName");
        Session.Remove("userPhoneNumber");
        Session.Remove("userCenterId"); 
        Session.Remove("externalLogin");
        Session.Remove("twitterLogin");

        Response.Redirect("/thank-you/?type=logout&centerId=" + selectedCenter);
    }
        
}