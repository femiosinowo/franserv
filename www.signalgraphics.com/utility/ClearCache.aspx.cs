using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Utility_ClearCache : System.Web.UI.Page
{
    List<string> cacheItemsToRemove = new List<string>();

    protected void Page_Load(object sender, EventArgs e)
    {
        Ektron.Cms.API.User.User uApi = new Ektron.Cms.API.User.User();
        if (uApi.UserId > 0 && uApi.IsAdmin())
        {
            pnlAdmin.Visible = true;
            pnlAnonymous.Visible = false;            
        }
        else
        {
            pnlAnonymous.Visible = true;
            pnlAdmin.Visible = false;
        }        
    }

    protected void btnClearCache_Click(object sender, EventArgs e)
    {
        //loop through all cache items
        foreach (DictionaryEntry c in System.Web.HttpContext.Current.Cache)
        {
            var key = c.Key as string;
            if (!string.IsNullOrEmpty(key))
                System.Web.HttpContext.Current.Cache.Remove(key);
        }
        //also remove out put cache
        HttpRuntime.UnloadAppDomain();
        ltrStatus.Text = "All items in the cache is removed.";
    }
   
}