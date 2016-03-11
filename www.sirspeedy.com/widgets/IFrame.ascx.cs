using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Ektron.Cms.Widget;
using Ektron.Cms;
using Ektron.Cms.API;
using Ektron.Cms.Common;
using Ektron.Cms.PageBuilder;
using System.Text.RegularExpressions;


public partial class Widgets_IFrame : System.Web.UI.UserControl, IWidget
{

    #region properties
    private string _IFrameUrl;
    private string _WidgetTitle;
    private bool _ScrollX;
    private bool _ScrollY;
    [WidgetDataMember("")]
    public string IFrameUrl { get { return _IFrameUrl; } set { _IFrameUrl = value; } }
    [WidgetDataMember("")]
    public string WidgetTitle { get { return _WidgetTitle; } set { _WidgetTitle = value; } }
    [WidgetDataMember(false)]
    public bool ScrollX { get { return _ScrollX; } set { _ScrollX = value; } }
    [WidgetDataMember(false)]
    public bool ScrollY { get { return _ScrollY; } set { _ScrollY = value; } }
    #endregion

    IWidgetHost _host;
    private ContentAPI m_refContentApi = new ContentAPI();
    protected EkMessageHelper m_refMsg;

    protected void Page_Init(object sender, EventArgs e)
    {
        _host = Ektron.Cms.Widget.WidgetHost.GetHost(this);
        m_refMsg = m_refContentApi.EkMsgRef;
        _host.Title = m_refMsg.GetMessage("lbl iframe widget");
        _host.Edit += new EditDelegate(EditEvent);
        _host.Maximize += new MaximizeDelegate(delegate() { Visible = true; });
        _host.Minimize += new MinimizeDelegate(delegate() { Visible = false; });
        _host.Create += new CreateDelegate(delegate() { EditEvent(""); });
        PreRender += new EventHandler(delegate(object PreRenderSender, EventArgs Evt) { SetOutput(); });
        lblNote.Text = "";
        CancelButton.Text = m_refMsg.GetMessage("btn cancel");
        SaveButton.Text = m_refMsg.GetMessage("btn save");  

        ViewSet.SetActiveView(View);
    }

    void EditEvent(string settings)
    {

         try
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), @"WidgetEnterCheck" + _host.WidgetInfo.ID.ToString(), "<script type='text/javascript'>function checkForEnter" + _host.WidgetInfo.ID.ToString() + @"(e, saveButtonID){var evt = e ? e : window.event;if(evt.keyCode == 13){document.forms[0].onsubmit = function () { return false; }; evt.cancelBubble = true; if (evt.stopPropagation) evt.stopPropagation(); return false; }}</script>", false);
            IFrameURLTextBox.Attributes.Add("onkeydown", @"javascript:checkForEnter" + _host.WidgetInfo.ID.ToString() + @"(event, '" + SaveButton.ClientID + "')");
            WidgetTitleTextBox.Attributes.Add("onkeydown", @"javascript:checkForEnter" + _host.WidgetInfo.ID.ToString() + @"(event, '" + SaveButton.ClientID + "')");
            IFrameURLTextBox.Text = IFrameUrl;
            WidgetTitleTextBox.Text = WidgetTitle;
            ScrollYCheckBox.Checked = ScrollY;
            ScrollXCheckBox.Checked = ScrollX;
            ViewSet.SetActiveView(Edit);
            lblNote.Text = m_refMsg.GetMessage("lbl note: some pages do not allow their content to be wrapped inside an iframe, and may either redirect the page or spawn a new window.");

        }
        catch
        {
            ViewSet.SetActiveView(View);
        }
        ViewSet.SetActiveView(Edit);
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
       try
        {

            if (!IFrameURLTextBox.Text.Substring(0, 4).Equals("http"))
            {
                IFrameURLTextBox.Text = "http://" + IFrameURLTextBox.Text;
            }
            IFrameUrl = IFrameURLTextBox.Text;
            WidgetTitle = WidgetTitleTextBox.Text;
            ScrollX = ScrollXCheckBox.Checked;
            ScrollY = ScrollYCheckBox.Checked;
            lblNote.Text = "";
 
 
        }
        catch
        {
            ViewSet.SetActiveView(View);
        }
        _host.SaveWidgetDataMembers();
        ViewSet.SetActiveView(View);
    }

    protected void SetOutput()
    {
       try
        {
            
            string style = "max-width:99%;";
            if (!ScrollY)
            {
                style += "overflow-y:hidden;";
            }
            if (!ScrollX)
            {
                style += "overflow-x:hidden;";
            }
            lbData.Text = @"<iframe src=""" + IFrameUrl.Trim() + @""" alt=""" + WidgetTitle.Trim() + @""" style=""" + style + @"""/>" + m_refMsg.GetMessage("lbl your browser does not support iframes") + "</iframe><br/>";

            _host.Title = Server.HtmlEncode(WidgetTitle);
          
        }
        catch
        {
            ViewSet.SetActiveView(Edit);
        } 
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        ViewSet.SetActiveView(View);
    }

   

}


