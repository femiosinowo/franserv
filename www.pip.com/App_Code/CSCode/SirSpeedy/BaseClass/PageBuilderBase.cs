using System;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// This is the base class for all page builder pages
    /// </summary>
    public class PageBuilderBase : Ektron.Cms.PageBuilder.PageBuilder
    {
        public PageBuilderBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }        
        
        #region Public Methods

        public override void Error(string message)
        {
            jsAlert(message);
        }
        
        public override void Notify(string message)
        {
            jsAlert(message);
        }

        public void jsAlert(string message)
        {
            Literal lit = new Literal();
            lit.Text = "<script type=\"\" language=\"\">{0}</script>";
            lit.Text = string.Format(lit.Text, "alert('" + message + "');");
            Form.Controls.Add(lit);
        }

        #endregion Public Methods       
        
    }
}