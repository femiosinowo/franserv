using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SirSpeedy.CMS;
using System.Text;
using Ektron.Cms.Instrumentation;
using System.Web.Script.Serialization;

public partial class UserControls_UtilityNav : System.Web.UI.UserControl
{
    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        cbFransOpport.DefaultContentID = ConfigHelper.GetValueLong("FranchiseOpportunityContentId");
        cbFransOpport.CacheInterval = ConfigHelper.GetValueLong("smallCacheInterval");
        cbFransOpport.Fill();

        SendAFile();

        if (!Page.IsPostBack)
        {
            //if (Session["username"] != null)
            //{
            //    uxSendAFile.Visible = false;
            //    temp.Attributes.Add("class", "user_logged_in");
            //}

            if (FransDataManager.IsFranchiseSelected() || !string.IsNullOrEmpty(Request.QueryString["FransId"]))
            {
                pnlLocal.Visible = true;
                pnlLocalLeft.Visible = true;
                // uxRequestLocal.Visible = true;
                pnlNational.Visible = false;
                pnlNationalLeft.Visible = false;
                franchise_ops.Visible = false;
                // uxRequestNational.Visible = false;
                ltrContactUnmber.Text = FransDataManager.GetContactNumber();
                HyperLink2.NavigateUrl = String.Format("http://{0}/{1}/requestquote/", Request.ServerVariables["SERVER_NAME"],
                    FransDataManager.GetFranchiseId());

            }
            else
            {
                pnlNationalLeft.Visible = true;
                pnlLocalLeft.Visible = false;
                pnlNational.Visible = true;
                franchise_ops.Visible = true;
                pnlNational.Visible = true;
                //uxRequestLocal.Visible = false;
                pnlLocal.Visible = false;
                // GetFranchiseDetails();
            }
        }
    }

    private void SendAFile()
    {
        Ektron.Cms.UserAPI userApi = new Ektron.Cms.UserAPI();

        if (HttpContext.Current.Session["username"] != null || HttpContext.Current.Session["useremail"] != null ||
            HttpContext.Current.Session["externalLogin"] != null || HttpContext.Current.Session["twitterLogin"] != null ||
            userApi.UserId > 0)
        {
            // uxSendAFile.Visible = false;
            //  temp.Attributes.Add("class", "user_logged_in");

            string centerid = String.Empty;
            if (Session["userCenterId"] != null)
                centerid = Session["userCenterId"].ToString();

            string safLink = (!String.IsNullOrWhiteSpace(centerid)) ? String.Format("?centerId={0}", centerid) : String.Empty;
            //if (!String.IsNullOrWhiteSpace(centerid)) centerid = centerid + "/";

            if ((userApi.UserId > 0) && (!String.IsNullOrWhiteSpace(centerid)))
            {
                //if user is logged in and local is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/{0}/send-a-file/{1}", centerid, safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/{0}/send-a-file/{1}", centerid, safLink));
            }
            else if ((userApi.UserId > 0) && (String.IsNullOrWhiteSpace(centerid)))
            {
                //if user is logged in and local is not selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/send-a-file/"));
                localsendafilelink.Attributes.Add("href", String.Format("/send-a-file/"));
            }
            else if ((Session["username"] != null || Session["useremail"] != null || Session["externalLogin"] != null) && (!String.IsNullOrWhiteSpace(centerid)))
            {
                //third party login & local center is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/{0}/social-send-a-file/{1}", centerid, safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/{0}/social-send-a-file/{1}", centerid, safLink));
            }
            else if (Session["username"] != null || Session["useremail"] != null || Session["externalLogin"] != null)
            {
                //third party login & no local center is selected
                nationalsendafilelink.Attributes.Add("href", String.Format("/social-send-a-file/"));
                localsendafilelink.Attributes.Add("href", String.Format("/social-send-a-file/"));
            }
            else
            {
                //default to national saf page
                nationalsendafilelink.Attributes.Add("href", String.Format("/send-a-file/{0}", safLink));
                localsendafilelink.Attributes.Add("href", String.Format("/send-a-file/{0}", safLink));
            }
            nationalsendafilelink.CssClass = String.Empty;
            localsendafilelink.CssClass = String.Empty;
        }
        else
        {
            localsendafilelink.Attributes.Add("href", "#send_file");
            nationalsendafilelink.Attributes.Add("href", "#send_file");
        }
    }

}