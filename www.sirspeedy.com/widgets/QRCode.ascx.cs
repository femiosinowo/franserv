using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ektron.Cms.Widget;
using Ektron.Cms.Common;
using Ektron.Cms;
using Ektron.Cms.API;

public partial class widgets_QRCode : System.Web.UI.UserControl, IWidget
{

    [WidgetDataMember("200x200")]
    public string QRCodeSize { get; set; }

    [WidgetDataMember("")]
    public string QRCodeData { get; set; }

    IWidgetHost _host;
    protected EkMessageHelper m_refMsg;
    protected ContentAPI m_refContentApi = new ContentAPI();

    protected void Page_Init(object sender, EventArgs e)
    {
        string sitepath = new CommonApi().SitePath;
        JS.RegisterJSInclude(this, JS.ManagedScript.EktronJS);
        JS.RegisterJSInclude(this, JS.ManagedScript.EktronModalJS);
        Css.RegisterCss(this, Css.ManagedStyleSheet.EktronModalCss);
        _host = Ektron.Cms.Widget.WidgetHost.GetHost(this);
        m_refMsg = m_refContentApi.EkMsgRef;
        _host.Title = m_refMsg.GetMessage("lbl QR Code");
        _host.Edit += new EditDelegate(EditEvent);
        _host.Maximize += new MaximizeDelegate(delegate() { Visible = true; });
        _host.Minimize += new MinimizeDelegate(delegate() { Visible = false; });
        _host.Create += new CreateDelegate(delegate() { EditEvent(""); });
        CancelButton.Text = m_refMsg.GetMessage("btn cancel");
        SaveButton.Text = m_refMsg.GetMessage("btn save");
        PreRender += new EventHandler(delegate(object PreRenderSender, EventArgs Evt) { SetOutput(); });
        ViewSet.SetActiveView(View);
    }

    void EditEvent(string settings)
    {
        QRDataRaido.SelectedIndex = 0;
        QRCustomDataText.Text = string.Empty;
        if (!string.IsNullOrWhiteSpace(QRCodeData))
        {
            QRDataRaido.SelectedIndex = 1;
            QRCustomDataText.Text = QRCodeData;
        }
        ViewSet.SetActiveView(Edit);
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        QRCodeSize = SizeList.SelectedValue;
        QRCodeData = string.Empty;
        if (QRDataRaido.SelectedIndex != 0)
        {
            QRCodeData = QRCustomDataText.Text;
        }
        _host.SaveWidgetDataMembers();
        ViewSet.SetActiveView(View);
    }

    protected void SetOutput()
    {
        if (string.IsNullOrWhiteSpace(QRCodeData))
        {
            QRCodeData = Request.Url.AbsoluteUri;
        }
        OutputLabel.Text = @"<img src=""https://chart.googleapis.com/chart?cht=qr&chs=" + QRCodeSize + @"&chl=" + QRCodeData + @""" alt=""QR Code"" />";
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        ViewSet.SetActiveView(View);
    }
}