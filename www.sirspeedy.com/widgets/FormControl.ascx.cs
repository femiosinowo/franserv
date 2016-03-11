using System;
using System.Globalization;
using System.Web.UI;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Framework.Content;
using Ektron.Cms.PageBuilder;
using Ektron.Cms.Widget;

namespace widgets
{
    public partial class WidgetsFormControl : UserControl, IWidget
    {
        #region properties

        // Localizable Strings.
        private const string WidgetTitle = "Form Control Widget";
        private const string InvalidFormId = "Invalid Form Id";
        public string DynamicParameter = "ekfrm";
        private long _formId;

        [WidgetDataMember(0)]
        public long FormId { get { return _formId; } set { _formId = value; } }

        EkRequestInformation _requestInformation;
        Ektron.Cms.PageBuilder.WidgetHost _host;

        protected string AppPath;
        protected int LangType;
        protected string UniqueId;
        protected FormData FormSource { get; set; }

        private EkRequestInformation RequestInformation
        {
            get
            {
                if (_requestInformation == null)
                {
                    _requestInformation = ObjectFactory.GetRequestInfoProvider().GetRequestInformation();
                }
                return _requestInformation;
            }
        }

        #endregion

        /// <summary>
        /// Edit Widget Event
        /// </summary>
        /// <param name="settings"></param>
        void EditEvent(string settings)
        {
            try
            {
                ViewSet.SetActiveView(Edit);

                if (FormId > 0)
                {
                    uxFormId.Text = FormId.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception e)
            {
                errorLb.Text = e.Message + e.Data + e.StackTrace + e.Source + e.ToString();
                _host.Title = _host.Title + " error";
                ViewSet.SetActiveView(View);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _host = Ektron.Cms.Widget.WidgetHost.GetHost(this) as Ektron.Cms.PageBuilder.WidgetHost;
            if (_host != null)
            {
                _host.Title = WidgetTitle;
                _host.Edit += new EditDelegate(EditEvent);
                _host.Maximize += new MaximizeDelegate(delegate() { Visible = true; });
                _host.Minimize += new MinimizeDelegate(delegate() { Visible = false; });
                _host.Create += new CreateDelegate(delegate() { EditEvent(""); });
                _host.ExpandOptions = Expandable.ExpandOnEdit;
            }

            this.EnableViewState = false;
            uxFormControl.DefaultFormId = FormId;
            Page.ClientScript.GetPostBackEventReference(SaveButton, "");
            AppPath = RequestInformation.ApplicationPath;
            LangType = RequestInformation.ContentLanguage;
            MainView();
            ViewSet.SetActiveView(View);
        }

        /// <summary>
        /// Main View (Display)
        /// </summary>
        protected void MainView()
        {
            long formId = -1;
            if (FormId > -1 && !IsPostBack)
            {
                if (this.FormId == 0 && !string.IsNullOrWhiteSpace(DynamicParameter))
                {
                    if (Request[DynamicParameter] != null && long.TryParse(Request[DynamicParameter], out formId))
                    {
                        this.FormId = formId;
                    }
                }
                else if (this.FormId > 0)
                {
                    var formManager = new FormManager();
                    var langId = (!string.IsNullOrEmpty(formManager.RequestInformation.ContentLanguage.ToString(CultureInfo.InvariantCulture)) &&
                                  formManager.RequestInformation.ContentLanguage > 0)
                                     ? formManager.RequestInformation.ContentLanguage
                                     : formManager.RequestInformation.DefaultContentLanguage;
                    FormSource = formManager.GetItem(FormId, langId) ?? new FormData();

                    if (FormSource.Id > 0)
                    {
                        this.FormId = FormSource.Id;
                    }
                }
            }
            else if (Page.Request["ekfrm"] != null && long.TryParse(Page.Request["ekfrm"], out formId))
            {
                this.FormId = formId;
            }

            if (this.FormId > 0)
            {
                uxFormControl.Visible = true;
                uxFormControl.DefaultFormId = this.FormId;
            }
        }

        /// <summary>
        /// Save Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            long formId = 0;
            if (long.TryParse(uxFormId.Text, out formId))
            {
                this.FormId = formId;
                _host.SaveWidgetDataMembers();
                MainView();
            }
            else
            {
                uxFormId.Text = "";
                editError.Text = InvalidFormId;
            }

            ViewSet.SetActiveView(View);
        }

        /// <summary>
        /// Cancel Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            MainView();
            ViewSet.SetActiveView(View);
        }
    }
}
