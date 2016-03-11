using System;
using System.Linq;
using Ektron.Cms.Instrumentation;
using System.Collections.Generic;
using System.Text;

using TeamLogic.CMS;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class Locator : MasterBase
{
    public string BodyClass { get; set; }

    /// <summary>
    /// page Init event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Init(object sender, EventArgs e)
    {
        long contentId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["sid"]))
            long.TryParse(Request.QueryString["sid"], out contentId);
        if (!string.IsNullOrEmpty(Request.QueryString["pageid"]))
            long.TryParse(Request.QueryString["pageid"], out contentId);
        else if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            long.TryParse(Request.QueryString["id"], out contentId);

        uxMetaDataTitles.DefaultContentID = contentId;
        uxMetaDataTitles.Fill();

        string reqCenterId = FransDataManager.CreateActiveFransSession();
        hddnCenterId.Value = reqCenterId;

        if (!string.IsNullOrEmpty(reqCenterId))
            siteContainer.Attributes.Add("class", "site_container local");
    }


    /// <summary>
    /// page Load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.addBodyClass(BodyClass);
        
        if(FransDataManager.IsFranchiseSelected())
        {
            uxSubscribe.Visible = true;
            uxRequestCall.Visible = true;
        }
    }   

    private void addBodyClass(string className)
    {
        //get current body class
        string currentClass = siteContainer.Attributes["class"];
        //if the class name is populated AND there is NO existing body classes
        if (className != null && !string.IsNullOrEmpty(className) && string.IsNullOrEmpty(currentClass))
        {
            siteContainer.Attributes.Add("class", className);
        }
        //if the class name is populated AND there is existing body classes
        else if (className != null && !string.IsNullOrEmpty(className) && !string.IsNullOrEmpty(currentClass))
        {
            //split the new classes 
            string[] newClasses = className.Split(' ');
            //attempt to split the current classes
            string[] currentClasses = (!string.IsNullOrEmpty(currentClass)) ? siteContainer.Attributes["class"].Split(' ') : null;
            //get the unique classes (we don't want to add classes that already exist)
            List<string> uniqueClasses = newClasses.Where(x => !currentClasses.Any(y => y.ToLower() == x.ToLower()) && x != string.Empty).ToList();
            //create a string builder to house the final additions
            StringBuilder newClass = new StringBuilder();
            //add new classes to the string builder
            foreach (string item in uniqueClasses)
            {
                newClass.Append(" ");
                newClass.Append(item);
            }
            siteContainer.Attributes.Add("class", currentClass + newClass.ToString());
        }
    }
       
}
