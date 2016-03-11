using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ektron.Cms.Widget;
using Ektron.Cms.PageBuilder;
using System.Collections.Generic;
using Ektron.Cms;
using Ektron.Newtonsoft.Json;
using System.Text.RegularExpressions;
using Ektron.Cms.Widget.Multivariate;
using Ektron.Newtonsoft.Json.Converters;
using Ektron.Cms.User;
using Ektron.Cms.Interfaces.Context;
using Ektron.Cms.Framework.UI;


#region ux support


public class UXVariantAction
{
    public string Action { get; set; }
    public string ColumnGuid { get; set; }
}

#endregion


public partial class widgets_MultivariateSection : System.Web.UI.UserControl, IMultivariateSectionView, IPostBackEventHandler
{
    private MultivariateSectionController _controller;

    private IWidgetHost _host = null;
    private List<Guid> _columns = new List<Guid>();
    private int _selectedIndex = -1;
    private bool _updateIndex = false;
    private bool _experimentActive = false;

    public Page ViewPage
    {
        get { return Page; }
        set { Page = value; }
    }

    public MultivariateSectionController Controller
    {
        get { return _controller; }
        set { _controller = value; }
    }

    public bool UpdateIndex
    {
        get
        {
            return _updateIndex;
        }

        set
        {
            _updateIndex = value;
            int index = _updateIndex ? _selectedIndex : -1;
            string intiID = this.IsUXEnabled ? uxMultivariateSectionWrapper.ClientID : multivariate.ClientID;
            Ektron.Cms.API.JS.RegisterJSBlock(this,
                String.Format("Ektron.Widget.MultivariateSection.Init('{0}',{1},{2},{3})",
                    intiID,
                    _columns.Count,
                    index,
                    Ektron.Newtonsoft.Json.JsonConvert.SerializeObject(_columns)),
                "jsblock" + ClientID);
        }
    }

    public List<DisplayColumnData> DisplayedColumns
    {
        get
        {
            List<DisplayColumnData> columns;
            if (this.IsUXEnabled)
            {
                columns = uxRepColumns.DataSource as List<DisplayColumnData>;
            }
            else
            {
                columns = repColumns.DataSource as List<DisplayColumnData>;
            }
            return columns;
        }

        set
        {
            if (this.IsUXEnabled)
            {
                uxRepColumns.DataSource = value;
                uxRepColumns.DataBind();
            }
            else
            {
                repColumns.DataSource = value;
                repColumns.DataBind();
            }
        }
    }

    public bool IsSliderVisible
    {
        get
        {
            bool isVisible = slider.Visible;
            if (this.IsUXEnabled)
            {
                isVisible = uxSlider.Visible;
            }
            return isVisible;
        }
        set
        { uxSlider.Visible = slider.Visible = value; }
    }

    [WidgetDataMember()]
    public List<Guid> Columns
    {
        get
        {
            return _columns;
        }

        set
        {
            _columns = value;
        }
    }

    [WidgetDataMember(-1)]
    public int SelectedIndex { get { return _selectedIndex; } set { _selectedIndex = value; } }


    #region UX Support

    private IUser user;
    public bool IsUXUser
    {
        get
        {
            bool isUxUser = false;
            if (user == null)
            {
                user = ObjectFactory.GetUser();
            }
            if (user.IsLoggedIn && user.IsCmsUser)
            {
                isUxUser = true;
            }
            return isUxUser;
        }
    }
    public UXDropZoneData UXDropZoneData { get; set; }
    protected bool IsUXEnabled
    {
        get
        {
            ICmsContextService cmsContextService = ServiceFactory.CreateCmsContextService();
            return cmsContextService.IsDeviceHTML5 && cmsContextService.IsToolbarEnabledForTemplate;
        }
    }
    protected string GetColumnLabel(RepeaterItem e)
    {
        return this.GetLocalResourceObject("MultivariateSectionLabel").ToString() + " " + (e.ItemIndex + 1).ToString();
    }
    protected bool IsEditMode
    {
        get
        {
            return ((this.Page as PageBuilder).Status == Mode.Editing);
        }
    }
    protected string GetColumnData(RepeaterItem e, int count)
    {
        string data = String.Empty;
        DisplayColumnData column = (DisplayColumnData)e.DataItem;

        if (this.IsUXEnabled && this.IsUXUser)
        {
            data = " data-ux-pagebuilder-column-data='";

            UXColumnData columnData = new UXColumnData();
            columnData.ID = column.Column.columnID.ToString();
            columnData.CssClass = column.Column.CssClass;
            columnData.ColumnGUID = column.Column.Guid.ToString();
            columnData.UnitName = column.Column.unit.ToString();
            columnData.Width = column.Column.width.ToString();
            columnData.CssFramework = column.Column.CssFramework;
            columnData.Index = e.ItemIndex;

            if (count > 1)
            {
                UXDropZoneAction removeColumnAction = new UXDropZoneAction();
                removeColumnAction.Action = UXDropZoneActionType.RemoveColumn;
                removeColumnAction.Text = this.GetLocalResourceObject("Remove").ToString();
                removeColumnAction.Title = this.GetLocalResourceObject("Remove").ToString();
                removeColumnAction.Href = "#RemoveColumn";

                UXVariantAction removeAction = new UXVariantAction();
                removeAction.Action = "RemoveVariant";
                removeAction.ColumnGuid = column.Column.Guid.ToString();
                removeColumnAction.Callback = (this.Page as PageBuilder).GetUXPostBackEventReference(this, Ektron.Newtonsoft.Json.JsonConvert.SerializeObject(removeAction));

                columnData.Actions.Add(removeColumnAction);
            }
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DefaultValueHandling = DefaultValueHandling.Include;
            data += HttpUtility.HtmlAttributeEncode(JsonConvert.SerializeObject(columnData, Formatting.None, settings)) + "'";
        }
        return data;
    }
    public void RaisePostBackEvent(string eventArgument)
    {
        UXVariantAction action = (UXVariantAction)Ektron.Newtonsoft.Json.JsonConvert.DeserializeObject<UXVariantAction>(eventArgument);
        if (action != null)
        {
            switch (action.Action)
            {
                case "RemoveVariant":
                    _controller.RemoveVariation(this.DisplayedColumns.Find(c => c.Column.Guid == Guid.Parse(action.ColumnGuid)));
                    _host.SaveWidgetDataMembers();
                    break;
            }
        }
    }
    private string DropZoneID(string idType)
    {
        string id = String.Empty;
        Control parent = this.Parent;
        if (!(parent is HtmlForm))
        {
            do
            {
                if ((parent as DropZone) != null)
                {
                    switch (idType)
                    {
                        case "UniqueID":
                            id = parent.FindControl("uxDropZone").UniqueID;
                            break;
                        case "ClientID":
                            id = parent.FindControl("uxDropZone").ClientID;
                            break;
                    }
                    break;
                }
                if (parent != null)
                    parent = parent.Parent;
                else
                    break;
            } while (!(parent is System.Web.UI.HtmlControls.HtmlForm));
        }
        return id;
    }
    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
        SiteAPI m_refSiteApi = new SiteAPI();
        Ektron.Cms.Common.EkMessageHelper m_refMsg = m_refSiteApi.EkMsgRef;
        if (Page as PageBuilder == null)
        {
            multivariate.InnerHtml = m_refMsg.GetMessage("lbl cannot start experiments in a dashboard.");
            return;
        }

        PageBuilder pbPage = (Page as PageBuilder);
        //pbPage.Pagedata.Widgets
        IMultivariateExperimentModel expModel = new MultivariateExperimentModel();
        List<MultivariateExperimentData> experiments = expModel.FindByExperimentPageID(pbPage.Pagedata.pageID);
        _experimentActive = (experiments.Count > 0);

        Ektron.Cms.Widget.IWidgetHost host = Ektron.Cms.Widget.WidgetHost.GetHost(this);
        host.Title = "Multivariate Testing Section";
        _host = host;

        _controller = new MultivariateSectionController(this);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page as PageBuilder == null)
        {
            return;
        }

        string sitepath = new CommonApi().SitePath;

        Ektron.Cms.API.JS.RegisterJSInclude(this, Ektron.Cms.API.JS.ManagedScript.EktronJS);
        Ektron.Cms.API.JS.RegisterJSInclude(this, Ektron.Cms.API.JS.ManagedScript.EktronUICoreJS);
        Ektron.Cms.API.JS.RegisterJSInclude(this, Ektron.Cms.API.JS.ManagedScript.EktronUISliderJS);
        Ektron.Cms.API.Css.RegisterCss(this, Ektron.Cms.API.Css.ManagedStyleSheet.EktronJQueryUiDefaultCss);
        Ektron.Cms.API.Css.RegisterCss(this, sitepath + "widgets/MultivariateSection/css/MultivariateSection.css", "MultivariateSectionCSS");
        Ektron.Cms.API.JS.RegisterJSInclude(this, sitepath + "widgets/MultivariateSection/js/MultivariateSection.js", "WidgetMultivariateSection");

        SiteAPI m_refSiteApi = new SiteAPI();
        Ektron.Cms.Common.EkMessageHelper m_refMsg = m_refSiteApi.EkMsgRef;
        btnAddVariation.Text = m_refMsg.GetMessage("btn add");
        addVariant.Attributes.Add("title", m_refMsg.GetMessage("lbl add variation"));
        uxAddVariant.Attributes.Add("title", m_refMsg.GetMessage("lbl add variation"));
        addVariant.Attributes.Add("alt", m_refMsg.GetMessage("lbl add variation"));
        uxAddVariant.Attributes.Add("alt", m_refMsg.GetMessage("lbl add variation"));
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Page as PageBuilder == null)
        {
            return;
        }

        if (this.IsUXEnabled)
        {
            this.UXDropZoneData = new UXDropZoneData();
            this.UXDropZoneData.ID = DropZoneID("ClientID");
            this.UXDropZoneData.MarkupID = DropZoneID("UniqueID");
            this.UXDropZoneData.IsDropZoneEditable = true;
            this.UXDropZoneData.IsMasterZone = false;
            uxDropZone.Attributes.Clear();
            uxDropZone.Attributes.Add("data-ux-pagebuilder-dropzone-data", JsonConvert.SerializeObject(this.UXDropZoneData));

            if (!Shared.ReportControllerLoaded && (Page as PageBuilder).Status == Mode.Editing)
            {
                DisplayedColumns = new List<DisplayColumnData>();
                IsSliderVisible = false;
                SiteAPI m_refSiteApi = new SiteAPI();
                Ektron.Cms.Common.EkMessageHelper m_refMsg = m_refSiteApi.EkMsgRef;
                uxNoMultivariateExperimentWidgetErrorMessage.Visible = true;
                uxNoMultivariateExperimentWidgetErrorMessage.InnerText = m_refMsg.GetMessage("lbl please drop a multivariate experiment widget on this page.");
            }
        }

        // sending "-1" as the current index tells the javascript to use
        // its saved index instead of updating with the one from the 
        // server side.
        // otherwise, the javascript will shift the column slider so that 
        // it points to the column with the selected index received from 
        // the server.
        // "_updateIndex" should only be set when the position of the 
        // slider should be changed (i.e. on adding or removing a 
        // variation).
        PageBuilder pbPage = (Page as PageBuilder);
        //pbPage.Pagedata.Widgets

        SiteAPI siteApi = new SiteAPI();
        Ektron.Cms.Common.EkMessageHelper messageApi = siteApi.EkMsgRef;
        uxAddVariant.Visible = addVariant.Visible = btnAddVariation.Visible = !_experimentActive && (pbPage.Status == Mode.Editing) && Shared.IsABTester == true;
        _controller.PreRender();
        if (!Shared.ReportControllerLoaded && (Page as PageBuilder).Status == Mode.Editing)
        {
            DisplayedColumns = new List<DisplayColumnData>();
            IsSliderVisible = false;
            litDebugOutput.Text = messageApi.GetMessage("lbl please drop a multivariate experiment widget on this page.");
        }
        else if (litDebugOutput.Text != "")
        {
            litDebugOutput.Text = "";
            Control upd = this;
            while ((upd as UpdatePanel) == null)
            {
                upd = upd.Parent;
            }
            UpdatePanel panel = upd as UpdatePanel;
            if (panel != null)
            {
                panel.Update();
            }
        }

        uxUXSwitch.SetActiveView(this.IsUXEnabled ? uxUXView : uxOriginalView);
    }

    protected void btnAddVariation_Click(object sender, EventArgs e)
    {
        _controller.AddVariation();
        _host.SaveWidgetDataMembers();
    }

    protected void btnNextVariation_Click(object sender, EventArgs e)
    {
        _controller.NextVariation();
    }

    protected void btnPreviousVariation_Click(object sender, EventArgs e)
    {
        _controller.PreviousVariation();
    }

    protected void repColumns_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DisplayColumnData columnData = (DisplayColumnData)e.Item.DataItem;
        ColumnData thiscol = columnData.Column;
        List<Ektron.Cms.PageBuilder.WidgetData> mywidgets = columnData.Widgets;
        mywidgets.Sort(delegate(Ektron.Cms.PageBuilder.WidgetData left, Ektron.Cms.PageBuilder.WidgetData right) { return left.Order.CompareTo(right.Order); });

        if (this.IsUXEnabled)
        {
            Repeater uxControlColumn = (e.Item.FindControl("uxControlColumn") as Repeater);
            uxControlColumn.ItemDataBound += new RepeaterItemEventHandler(controlcolumn_ItemDataBound);

            uxControlColumn.DataSource = mywidgets;
            uxControlColumn.DataBind();
        }
        else
        {
            ContentAPI _api = new ContentAPI();
            string appPath = _api.AppPath;
            SiteAPI m_refSiteApi = new SiteAPI();
            Ektron.Cms.Common.EkMessageHelper m_refMsg = m_refSiteApi.EkMsgRef;

            HtmlImage imgresizecolumn = (e.Item.FindControl("imgresizecolumn") as HtmlImage);
            HtmlImage imgremcolumn = (e.Item.FindControl("imgremcolumn") as HtmlImage);
            HtmlAnchor lbResizeColumn = (e.Item.FindControl("lbResizeColumn") as HtmlAnchor);
            LinkButton btnDeleteColumn = (e.Item.FindControl("btnDeleteColumn") as LinkButton);
            Repeater controlcolumn = (e.Item.FindControl("controlcolumn") as Repeater);

            HtmlControl zonediv = (e.Item.FindControl("zone") as HtmlControl);
            HtmlControl column = (e.Item.FindControl("column") as HtmlControl);
            HtmlControl headerItem = (e.Item.FindControl("headerItem") as HtmlControl);

            //image paths
            //(e.Item.FindControl("imgleftcorner") as HtmlImage).Src = appPath + "/PageBuilder/PageControls/images/column_leftcorner.png";
            //(e.Item.FindControl("imgrightcorner") as HtmlImage).Src = appPath + "/PageBuilder/PageControls/images/column_rightcorner.png";
            imgresizecolumn.Src = appPath + "/PageBuilder/PageControls/" + (Page as PageBuilder).Theme + "images/edit_off.png";
            imgresizecolumn.Alt = lbResizeColumn.Title = m_refMsg.GetMessage("lbl pagebuilder resize");
            imgremcolumn.Src = appPath + "/PageBuilder/PageControls/" + (Page as PageBuilder).Theme + "images/icon_close.png";
            imgremcolumn.Alt = btnDeleteColumn.Attributes["title"] = m_refMsg.GetMessage("generic delete title");

            lbResizeColumn.Visible = false;
            btnDeleteColumn.Visible = true;
            lbResizeColumn.Title = imgresizecolumn.Alt.ToString();

            headerItem.Visible = ((Page as PageBuilder).Status == Mode.Editing) && !_experimentActive;

            btnDeleteColumn.Click += new EventHandler(delegate(object delSender, EventArgs delArgs)
            {
                _controller.RemoveVariation(columnData);
                _host.SaveWidgetDataMembers();
            });

            column.Attributes.Add("columnid", thiscol.columnID.ToString());
            column.Attributes.Add("columnguid", thiscol.Guid.ToString());

            zonediv.Style.Add("width", "100%");

            zonediv.Style.Remove("display");

            if ((Page as PageBuilder).Status != Mode.Editing)
            {
                zonediv.Style.Remove("width");
            }

            if ((Page as PageBuilder).Status != Mode.Editing || Shared.IsABTester == false || _experimentActive == true)
            {
                zonediv.Attributes["class"] = "PBViewing PBNonsortable";
            }
            else
            {
                zonediv.Attributes.Add("dropzoneid", columnData.DropZoneID);
                zonediv.Attributes["class"] = "PBColumn";
                zonediv.Attributes["class"] += " PBUnsizable";
            }

            controlcolumn.ItemDataBound += new RepeaterItemEventHandler(controlcolumn_ItemDataBound);

            controlcolumn.DataSource = mywidgets;
            controlcolumn.DataBind();
        }
    }

    protected void controlcolumn_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType != ListItemType.Header && item.ItemType != ListItemType.Footer && (item.DataItem as Ektron.Cms.PageBuilder.WidgetData) != null)
        {
            Ektron.Cms.PageBuilder.WidgetData w = item.DataItem as Ektron.Cms.PageBuilder.WidgetData;
            WidgetHostCtrl ctrl = (WidgetHostCtrl)item.FindControl("WidgetHost");
            ctrl.ColumnID = w.ColumnID;
            ctrl.ColumnGuid = w.ColumnGuid;
            ctrl.SortOrder = w.Order;
            ctrl.WidgetHost_Load();
        }
    }
}