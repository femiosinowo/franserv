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
using System.Text;
using Ektron.ASM.EkHttpDavHandler;
using Ektron.Cms.Settings.Mobile;

public partial class Widgets_ResponsiveImage : System.Web.UI.UserControl, IWidget
{

    #region properties

    private long _ContentBlockId;
    private string _toolTip = "";
    private int _imageBorder = 0;

    [WidgetDataMember(0)]
    public long ContentBlockId { get { return _ContentBlockId; } set { _ContentBlockId = value; } }
    [WidgetDataMember("")]
    public string ToolTip { get { return _toolTip; } set { _toolTip = value; } }
    [WidgetDataMember(0)]
    public int ImageBorder { get { return _imageBorder; } set { _imageBorder = value; } }

    #endregion

    IWidgetHost _host;
    protected ContentAPI _api;
    protected string appPath;
    protected string sitePath;
    protected int langType;

    protected string uniqueId
    {
        get { return (ViewState[this.ID + "uniqueid"] == null) ? "" : (string)ViewState[this.ID + "uniqueid"]; }
        set { ViewState[this.ID + "uniqueid"] = value; }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        _host = Ektron.Cms.Widget.WidgetHost.GetHost(this);
        _host.Title = "Responsive Image Widget";
        _host.Edit += new EditDelegate(EditEvent);
        _host.Maximize += new MaximizeDelegate(delegate() { Visible = true; });
        _host.Minimize += new MinimizeDelegate(delegate() { Visible = false; });
        _host.Create += new CreateDelegate(delegate() { EditEvent(""); });
        _host.ExpandOptions = Expandable.ExpandOnEdit;
        PreRender += new EventHandler(delegate(object PreRenderSender, EventArgs Evt) { MainView(); });
        _api = new ContentAPI();
        appPath = _api.AppPath;
        sitePath = _api.SitePath.TrimEnd('/');
        langType = _api.RequestInformationRef.ContentLanguage;
        CreateUniqueId();
        ViewSet.SetActiveView(View);
    }

    void EditEvent(string settings)
    {
        try
        {
            string webserviceURL = sitePath + "/widgets/responsiveimage/ImageHandler.ashx";
            string mediaManagerPath = appPath + "/mediamanager.aspx?scope=images&upload=true&retfield=" + hdnImageSource.ClientID + "&showthumb=false&autonav=0";
            // Register JS
            JS.RegisterJSInclude(this, sitePath + "/widgets/responsiveimage/modernizr.js", "modernizrJS");
            JS.RegisterJSInclude(this, JS.ManagedScript.EktronJS);
            JS.RegisterJSInclude(this, sitePath + "/widgets/responsiveimage/behavior.js", "ImageWidgetBehaviorJS");
            JS.RegisterJSInclude(this, sitePath + "/widgets/responsiveimage/Image.js", "ImageJS");
            // Insert CSS Links
            Css.RegisterCss(this, sitePath + "/widgets/responsiveimage/ImageStyle.css", "ImageWidgetCSS");

            toolTip.Text = ToolTip;

            if (!this.AdaptiveImagesConfigured())
            {
                this.DisplayEditError("Please <a href=\"http://documentation.ektron.com/cms400/v9.00/reference/Web//ektronreferenceweb.html#cshid=mobile/mobile.htm#adaptive\" target=\"_new\">configure adaptive images</a> to fully leverage this widget.");
            }

            ViewSet.SetActiveView(Edit);

            string mediaManagerUrlPartial = appPath + "/mediamanager.aspx?scope=images&upload=true&retfield=" + hdnImageSource.ClientID + "&showthumb=false&autonav=";
            hdnMediaManagerPath.Value = mediaManagerUrlPartial.Replace("//", "/");
            
            if (ContentBlockId > 0)
            {

                //load & set selected folder path
                Ektron.Cms.API.Library lib = new Library();
                Ektron.Cms.LibraryData ld = lib.GetLibraryItem(ContentBlockId);
                if (!ReferenceEquals(ld, null))
                {
                    // correct for "//" in libData.FileName
                    ld = FixLibraryDataFileName(ld);
                    
                    long folderid = ld.ParentId;
                    hdnContentId.Value = ld.Id.ToString();
                    hdnFolderId.Value = folderid.ToString();

                    imageSource.Text = ld.FileName.ToString();
                    imageThumb.ImageUrl = ld.FileName.ToString();
                    mediaManagerPath = mediaManagerUrlPartial + folderid.ToString();

                    while (folderid != 0)
                    {
                        folderid = _api.GetParentIdByFolderId(folderid);

                    }
                }
                imageThumb.Visible = true;
                noThumb.Visible = false;
            }
            else
            {
                imageThumb.Visible = false;
                noThumb.Visible = true;
            }
            mediaManagerIframe.Attributes.Add("src", mediaManagerPath);
        }
        catch (Exception e)
        {
            errorLb.Text = e.Message + e.Data + e.StackTrace + e.Source + e.ToString();
            _host.Title = _host.Title + " error";
            ViewSet.SetActiveView(View);
        }
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        Int64 cid = 0;
        Int64.TryParse(hdnContentId.Value, out cid);
        if (cid > 0)
        {
            ContentBlockId = cid;
            ToolTip = toolTip.Text;
            int imgBorder = 0;
            int.TryParse(txtBorder.Text, out imgBorder);
            ImageBorder = imgBorder;
            _host.SaveWidgetDataMembers();
            //generate images 
            Ektron.Cms.LibraryData libData = new LibraryData();
            Ektron.Cms.ContentAPI contentApi = new ContentAPI();
            Ektron.Cms.API.Library lib = new Library();
            libData = lib.GetLibraryItem(ContentBlockId);
            if (!ReferenceEquals(libData, null))
            {

                Ektron.ASM.EkHttpDavHandler.AdaptiveImageProcessor.Instance.ProcessImageForAllConfig(contentApi.RequestInformationRef, HttpContext.Current.Server.MapPath(libData.FileName));

            }

            MainView();

        }

        ViewSet.SetActiveView(View);

    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        ViewSet.SetActiveView(View);
    }

    #region support
    protected void CreateUniqueId()
    {
        if (uniqueId == null || uniqueId == "")
        {
            uniqueId = System.Guid.NewGuid().ToString();
        }
    }

    protected Ektron.Cms.LibraryData FixLibraryDataFileName(Ektron.Cms.LibraryData libData)
    {
        // correct for "//" in libData.FileName
        libData.FileName = libData.FileName.Replace("//", "/");
        return libData;
    }
    #endregion

    protected void MainView()
    {
        if (ContentBlockId > 0)
        {
            JS.RegisterJSInclude(this, sitePath + "/widgets/responsiveimage/jquery-picture-min.js", "jquerypicturejs");
            IAdaptiveImageSetting settingMgr = Ektron.Cms.ObjectFactory.Get<IAdaptiveImageSetting>();
            Ektron.Cms.LibraryData libData = new LibraryData();
            Ektron.Cms.API.Library lib = new Library();
            libData = lib.GetLibraryItem(ContentBlockId);
            if (!ReferenceEquals(libData, null))
            {
                // correct for "//" in libData.FileName
                libData = FixLibraryDataFileName(libData);
                string title = libData.Title;
                string strTooltip = ToolTip.Trim() == "" ? libData.Title : ToolTip.Trim();
                string strHtml = string.Empty;
                string[] filePathArray = libData.FileName.Split("/".ToCharArray());
                string filename = filePathArray[filePathArray.Length - 1];
                AdaptiveImageSettingData settingData = settingMgr.GetSettings();
                int pnteri = 0;

                //output figure tag
                if (!string.IsNullOrEmpty(filename))
                {
                    AdaptiveImageProcessor adaptiveImageconfig = AdaptiveImageProcessor.Instance;
                    for (int i = 0; i < adaptiveImageconfig.BreakPoints.Count; i++)
                    {
                        DeviceBreakpointData config = adaptiveImageconfig.BreakPoints[i];

                        if (config.AdaptiveImageData != null && config.AdaptiveImageData.Enabled)
                        {
                            if (pnteri < 1)
                            {
                                strHtml += @" data-media=""" + libData.FileName.Replace(filename, filename + "?" + settingData.DirectAccessQueryString + "=" + config.AdaptiveImageData.FileLabel) + @"""";
                                pnteri++;
                            }

                            if (i == adaptiveImageconfig.BreakPoints.Count - 1)
                            {
                                strHtml += @" data-media" + (config.AdaptiveImageData.Width + 1).ToString() + @"=""" + libData.FileName.Replace(filename, filename) + @"""";
                            }
                            else
                            {
                                if (adaptiveImageconfig.BreakPoints[i + 1].AdaptiveImageData != null && adaptiveImageconfig.BreakPoints[i + 1].AdaptiveImageData.Enabled)
                                {
                                    strHtml += @" data-media" + (config.AdaptiveImageData.Width + 1).ToString() + @"=""" + libData.FileName.Replace(filename, filename + "?" + settingData.DirectAccessQueryString + "=" + adaptiveImageconfig.BreakPoints[i + 1].AdaptiveImageData.FileLabel) + @"""";
                                }
                            }
                        }
                    }

                    // Only if 1+ breakpoints are configured for adaptive images, will there be anything to show in the figure tag
                    if (!string.IsNullOrEmpty(strHtml))
                    {
                        strHtml = @"<figure id=""picture" + uniqueId + @""" style=""border: solid " + ImageBorder + @"px"" title=""" + strTooltip + @""" " + strHtml;
                        strHtml += @"><noscript>";
                        strHtml += @"    <img src=""" + libData.FileName + @""" style=""border: solid " + ImageBorder + @"px"" />";
                        strHtml += @"</noscript>";
                        strHtml += @"</figure>";
                        JS.RegisterJSBlock(this, @"$ektron('#picture" + uniqueId + "').picture();", "ShowMeThePicture");
                    }
                    else
                    {
                        // Output as a standard image if no adaptive images are configured
                        strHtml += @"<img src=""" + libData.FileName + @""" />";
                    }
                }


                ltrImage.Text = strHtml;
                ltrImage.Visible = true;
            }

            phContent.Visible = true;
            phHelpText.Visible = false;
        }
        else
        {
            phContent.Visible = false;
            phHelpText.Visible = true;
        }


        if (!(_host == null || _host.IsEditable == false))
        {
            divHelpText.Visible = true;
        }
        else
        {
            divHelpText.Visible = false;
        }
    }

    /// <summary>
    /// Checks whether any breakpoint has adaptives images configured and enabled.
    /// </summary>
    /// <returns>True if a breakpoint has adaptive images configured and enabled; otherwise false.</returns>
    private bool AdaptiveImagesConfigured()
    {
        return AdaptiveImageProcessor.Instance.BreakPoints.Exists(bp => bp.AdaptiveImageData != null && bp.AdaptiveImageData.Enabled);
    }

    private void DisplayEditError(string error)
    {
        this.uxEditError.Visible = true;
        this.uxEditErrorText.Text = error;
    }
}
