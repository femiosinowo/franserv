using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Ektron.Cms.PageBuilder;
using Ektron.Cms;
using System.ComponentModel;
using Ektron.Newtonsoft.Json;
using Ektron.Cms.User;
using Ektron.Cms.Interfaces.Context;
using Ektron.Cms.Framework.UI;
using Ektron.Newtonsoft.Json.Converters;
using System.Text;


[DefaultBindingProperty("Columns")]
public partial class ColumnDisplay : System.Web.UI.UserControl, IPostBackEventHandler
{
	public class DeleteColumnEventArgs : EventArgs
	{
		private int _index;
		private Guid _guid;

		public int Index { get { return _index; } set { _index = value; } }
		public Guid Guid { get { return _guid; } set { _guid = value; } }

		public DeleteColumnEventArgs(int index, Guid guid)
		{
			this._index = index;
			this._guid = guid;
		}
	}

	private List<ColumnDisplayData> _columns = new List<ColumnDisplayData>();
	public List<ColumnDisplayData> Columns
	{
		get { return _columns; }
		set
		{
			_columns = value;
            if (this.IsUXEnabled)
            {
                uxRepColumns.DataSource = _columns;
                uxRepColumns.DataBind();
            }
            else
            {
                repColumns.DataSource = _columns;
                repColumns.DataBind();
            }
		}
	}

	private WidgetHost _host;
	public WidgetHost WidgetHost
	{
		get { return _host; }
		set { _host = value; }
	}

    public bool IsEditable { get; set; }

	public event EventHandler<DeleteColumnEventArgs> DeleteColumn;

    public ColumnDisplay()
    {
        IsEditable = true;
        this.UXDropZoneData = new UXDropZoneData();
    }

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
        string data = String.Empty;
        ColumnDisplayData column = e.DataItem as ColumnDisplayData;
        if (String.IsNullOrEmpty(column.Caption))
        {
            data = e.ItemIndex.ToString();
        }
        else
        {
            data = column.Caption;
        }
        return data;
    }
    protected string GetColumnData(RepeaterItem e)
    {
        string data = String.Empty;
        ColumnDisplayData column = e.DataItem as ColumnDisplayData;

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

            UXDropZoneAction removeColumnAction = new UXDropZoneAction();
            removeColumnAction.Action = UXDropZoneActionType.RemoveColumn;
            removeColumnAction.Text = this.GetLocalResourceObject("Remove").ToString();
            removeColumnAction.Title = this.GetLocalResourceObject("Remove").ToString();
            removeColumnAction.Href = "#RemoveColumn";
            removeColumnAction.Callback = @"__doPostBack('" + this.UniqueID + @"', '{""Action"":""RemoveColumn"", ""ColumnGuid"": """ + column.Column.Guid.ToString() + @"""}');";
            columnData.Actions.Add(removeColumnAction);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DefaultValueHandling = DefaultValueHandling.Include;
            data += HttpUtility.HtmlAttributeEncode(JsonConvert.SerializeObject(columnData, Formatting.None, settings)) + "'";
        }
        return data;
    }
    private class UXPostBackArgs
    {
        public string Action { get; set; }
        public string ColumnGuid { get; set; }
    }

    public void RaisePostBackEvent(string eventArgument)
    {
        UXPostBackArgs args = (UXPostBackArgs)Ektron.Newtonsoft.Json.JsonConvert.DeserializeObject<UXPostBackArgs>(eventArgument);
        if (args.Action == "RemoveColumn")
        {
            var guid = Guid.Parse(args.ColumnGuid);
            var index = this.Columns.FindIndex((columnDisplayData) => columnDisplayData.Column.Guid == guid);
            if (this.DeleteColumn != null) this.DeleteColumn(this, new DeleteColumnEventArgs(index, guid));
            this._host.RemoveColumn(Guid.Parse(args.ColumnGuid));
        }
    }

    #endregion

    protected override void OnPreRender(EventArgs e)
    {
        if (this.IsUXEnabled)
        {
            this.UXDropZoneData.ID = this.ID;
            this.UXDropZoneData.MarkupID = DropZoneID(this.WidgetHost.PBWidgetInfo.DropID);
            this.UXDropZoneData.IsDropZoneEditable = true;
            this.UXDropZoneData.IsMasterZone = false;
            uxDropZone.Attributes.Clear();
            uxDropZone.Attributes.Add("data-ux-pagebuilder-dropzone-data", JsonConvert.SerializeObject(this.UXDropZoneData));
        }

        uxUXSwitch.SetActiveView(this.IsUXEnabled ? uxUXView : uxOriginalView);
        base.OnPreRender(e);
    }

    private string DropZoneID(string dropId)
    {
        string clientID = String.Empty;
        Control parent = this.Parent;
        if (!(parent is HtmlForm))
        {
            do
            {
                if ((parent as DropZone) != null)
                {
                    clientID = parent.FindControl("uxDropZone").UniqueID;
                    break;
                }
                if (parent != null)
                    parent = parent.Parent;
                else
                    break;
            } while (!(parent is System.Web.UI.HtmlControls.HtmlForm));
        }
        return clientID;
    }

	#region Widget-in-Widget Code
	protected void repColumns_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
        ColumnDisplayData dataItem = (ColumnDisplayData)e.Item.DataItem;
        ColumnData thiscol = dataItem.Column;

        List<Ektron.Cms.PageBuilder.WidgetData> mywidgets = (Page as PageBuilder).Pagedata.Widgets.FindAll(delegate(Ektron.Cms.PageBuilder.WidgetData w) { return w.ColumnID == thiscol.columnID && w.ColumnGuid == thiscol.Guid && w.DropID == _host.ZoneID; });
        mywidgets.Sort(delegate(Ektron.Cms.PageBuilder.WidgetData left, Ektron.Cms.PageBuilder.WidgetData right) { return left.Order.CompareTo(right.Order); });

        if (this.IsUXEnabled)
        {
            DropDownList position = e.Item.FindControl("uxConditionPosition") as DropDownList;
            if (position != null)
            {
                List<ListItem> items = new List<ListItem>();
                ListItem item;
                int columnCounter = 0;
                foreach (ColumnDisplayData column in this.Columns)
                {
                    item = new ListItem();
                    item.Text = (columnCounter + 1).ToString();
                    item.Value = (columnCounter + 1).ToString();
                    if (columnCounter == e.Item.ItemIndex)
                    {
                        item.Selected = true;
                    }
                    items.Add(item);
                    columnCounter = columnCounter + 1;
                }
                position.Items.AddRange(items.ToArray());
            }

            Repeater uxControlColumnRepeater = (e.Item.FindControl("uxControlColumn") as Repeater);
            uxControlColumnRepeater.ItemDataBound += new RepeaterItemEventHandler(controlcolumn_ItemDataBound);
            uxControlColumnRepeater.DataSource = mywidgets;
            uxControlColumnRepeater.DataBind();
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
            Label headerCaption = (e.Item.FindControl("HeaderCaption") as Label);

            //image paths
            //(e.Item.FindControl("imgleftcorner") as HtmlImage).Src = appPath + "/PageBuilder/PageControls/images/column_leftcorner.png";
            //(e.Item.FindControl("imgrightcorner") as HtmlImage).Src = appPath + "/PageBuilder/PageControls/images/column_rightcorner.png";
            imgresizecolumn.Src = appPath + "/PageBuilder/PageControls/" + (Page as PageBuilder).Theme + "images/edit_off.png";
            imgresizecolumn.Alt = lbResizeColumn.Title = m_refMsg.GetMessage("lbl pagebuilder resize");
            //imgremcolumn.Src = appPath + "/PageBuilder/PageControls/" + (Page as PageBuilder).Theme + "images/icon_close.png";
            //imgremcolumn.Alt = btnDeleteColumn.Attributes["title"] = m_refMsg.GetMessage("generic delete title");

            lbResizeColumn.Visible = false;
            btnDeleteColumn.Visible = true;
            lbResizeColumn.Title = imgresizecolumn.Alt.ToString();

            headerItem.Visible = ((Page as PageBuilder).Status == Mode.Editing);

            if (headerCaption != null)
            {
                headerCaption.Text = dataItem.Caption;
            }

            //cool
            btnDeleteColumn.Click += new EventHandler(delegate(object delSender, EventArgs delArgs)
            {
                if (this.DeleteColumn != null)
                    this.DeleteColumn(delSender, new DeleteColumnEventArgs(e.Item.ItemIndex, thiscol.Guid));
                _host.RemoveColumn(thiscol.Guid);
            });

            column.Attributes.Add("columnid", thiscol.columnID.ToString());
            column.Attributes.Add("columnguid", thiscol.Guid.ToString());

            zonediv.Style.Add("width", "100%");

            if ((Page as PageBuilder).Status != Mode.Editing || !IsEditable)
            {
                zonediv.Attributes["class"] = "PBViewing";
            }
            else
            {
                zonediv.Attributes.Add("dropzoneid", _host.ZoneID);
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
	#endregion
}

public class ColumnDisplayData
{
	public ColumnDisplayData() { }
	public ColumnDisplayData(ColumnData data) : this(data, "") { }
	public ColumnDisplayData(ColumnData data, String Caption)
	{
		_data = data;
		_caption = Caption;
	}
	private ColumnData _data = null;
	public ColumnData Column
	{
		get { return _data; }
		set { _data = value; }
	}

	private String _caption = String.Empty;
	public String Caption
	{
		get { return _caption; }
		set { _caption = value; }
	}
}