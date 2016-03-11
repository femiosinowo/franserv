using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml;
using Ektron.DxH.Common;
using Ektron.Cms.Common;
using Ektron.DxH.Client;
using Ektron.DxH.Common.Objects;
using Ektron.DxH.Common.Events;
using Ektron.DxH.Common.Operations;
using Ektron.DxH.Common.Contracts;
using Ektron.Cms.Framework.Settings.DxH;
using Ektron.Cms.Settings.DxH;
using Ektron.DxH.Common.Connectors;
using System.Threading.Tasks;

namespace Ektron.Cms.Content.Targeting.Rules
{
    #region DXH Templates

    public abstract class DXHRuleTemplate : RuleTemplate
    {
        protected static Cache RuleCache
        {
            get
            {
                return HttpRuntime.Cache ?? null;
            }
        }


        string _salesforceType = null;
        StringOperatorField _fldOperator = null;
        protected DXHHelper _dxhHelper;
        protected string _crmObjectType = null;

        public DXHRuleTemplate(string salesforceType)
            : base(salesforceType)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
            _salesforceType = salesforceType;
        }

        public DXHRuleTemplate(string salesforceType, DXHHelper dxhHelper, string crmObjectType)
            : base(dxhHelper.ConnectorName + ":" + crmObjectType + ":" + salesforceType)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
            _salesforceType = salesforceType;
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
        }

        public override LocalizableValue Group
        {
            get { return new LocalizableValue(this._dxhHelper.ConnectorName + "Properties", this._dxhHelper.ConnectorName + " " + this._crmObjectType, ""); }
        }

        public override string Text
        {
            get { return String.Format("User's {0} {{operator}} {{value}}", EscapeRuleText(_salesforceType)); }
        }

        public override string Title
        {
            get { return String.Format("{0}", EscapeRuleText(_salesforceType)); }
        }

        public DXHHelper DXHHelper
        { 
            get { return _dxhHelper; } 
        }

        public string CRMObjectId
        {
            get 
            {
                string id = "";
                if (DXHHelper.UserConnections.Count > 0)
                {
                    id = DXHHelper.UserConnections[0].ExternalUserKey;
                }
                return id;
            }
        }

        protected string GetCachKey(Rule rule, string crmId)
        {
            return string.Format("Ektron|DxH|Targeting|RuleID|{0}|ParentID|{1}|TemplateID|{2}|CrmID|{3}",
                rule.ID.ToString(),
                rule.ParentID.ToString(),
                rule.RuleTemplateID.ToString(),
                crmId);
    }

        protected static void GetAndCacheActivity(string crmId, string cacheKey, DXHHelper helper)
        {
            Action factoryMethod = delegate()
            {
                RuleCache.Insert(cacheKey, helper.GetActivity(crmId));
            };
            Task.Factory.StartNew(factoryMethod);
        }

        protected static void GetAndCacheLatestActivityDate(string crmId, string crmObjectType, string cacheKey, DXHHelper helper)
        {
            Action factoryMethod = delegate()
            {
                RuleCache.Insert(cacheKey, helper.GetLatestActivityDate(crmObjectType, crmId));
            };
            Task.Factory.StartNew(factoryMethod);
        }

        protected static void GetAndCacheObjectValue(string crmId, string crmObjectType, string crmFieldName, string cacheKey, DXHHelper helper)
        {
            Action factoryMethod = delegate()
            {
                RuleCache.Insert(cacheKey, helper.GetObjectValue(crmObjectType, crmId, crmFieldName));
            };
            Task.Factory.StartNew(factoryMethod);
        }
    }

    public class DXHMarketingAutomationActivitiesTemplate : DXHRuleTemplate
    {
        StringOperatorField _fldOperator = null;
        string _fieldName = "";
        string _crmFieldName = "";

        public DXHMarketingAutomationActivitiesTemplate(string activityProperty)
            : base(activityProperty)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
        }
        public DXHMarketingAutomationActivitiesTemplate(string fieldName, DXHHelper dxhHelper, string crmObjectType, string crmFieldName)
            : base(fieldName, dxhHelper, crmObjectType)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
            _fieldName = fieldName;
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
            this._crmFieldName = crmFieldName;
        }

        public override bool Evaluate(Rule rule)
        {
            string crmId = CRMObjectId;
            string targetValue = rule.Values["value"];
            bool rtnVal = false;

            if (!string.IsNullOrEmpty(crmId))
            {
                try
                {
                    var cachKey = this.GetCachKey(rule, crmId);
                    var activityList = RuleCache.Get(cachKey) as List<ObjectInstance>;// DXHHelper.GetActivity(crmId);

                    if (activityList != null)
                    {
                    foreach (ObjectInstance activityRecord in activityList)
                    {
                        string activityType = "";
                        string activitySummary = "";
                        Ektron.DxH.Common.Objects.Field field;
                        field = activityRecord.Fields.Find(x => x.Id == "activityType");
                        if (field != null)
                            activityType = field.Value.ToString();
                        field = activityRecord.Fields.Find(x => x.Id == "mktgAssetName");
                        if (field != null)
                            activitySummary = field.Value.ToString();

                        if (activityType.ToLower() == this._crmFieldName.ToLower() && activitySummary.ToLower().Contains(targetValue.ToLower()))
                        {   // This activity has been performed!
                                rtnVal = true;
                        }
                    }
                    }
                    else
                    {
                        GetAndCacheActivity(crmId, cachKey, this.DXHHelper);
                    }
                    
                }
                catch (Exception ex)
                {
                    EventLog.WriteEntry("CMS400", string.Format("An Error Occurred: {0}<br />", ex.StackTrace), EventLogEntryType.Error);
                    return rtnVal;
                }
                return rtnVal;
            }

            // If we get to here, then we never found the activity performed.  Return false;
            return false;
        }

        private Dictionary<string, Field> _fields = null;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (null == _fields)
                {
                    _fields = new Dictionary<string, Field>();
                    this.AddField(_fldOperator);
                    this.AddStringField("value", "Value", "");
                }
                return _fields;
            }
        }

        public override string Text
        {
            get { return this._crmObjectType + " " + _fieldName + " {operator} {value}"; }
        }
    }

    public class DXHLastConversionDateTemplate : DXHRuleTemplate
    {
        NumericOperatorField _fldOperator = null;

        public DXHLastConversionDateTemplate(string sfProductsOwnedProperty)
            : base("Last Conversion Date")
        {
            _fldOperator = new NumericOperatorField("operator", "operator", "");
        }
        public DXHLastConversionDateTemplate(string leadScoreType, DXHHelper dxhHelper, string crmObjectType)
            : base("Last Conversion Date", dxhHelper, crmObjectType)
        {
            _fldOperator = new NumericOperatorField("operator", "operator", "");
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
        }

        public override bool Evaluate(Rule rule)
        {
            string crmId = CRMObjectId;
            bool rtnVal = false;

            if (!string.IsNullOrEmpty(crmId))
            {
                string op = rule.Values["operator"];
                string targetValue = rule.Values["value"];
                string crmValue = string.Empty;

                try
                {
                    bool inRange = false;
                    var cacheKey = this.GetCachKey(rule, crmId);
                    crmValue = RuleCache.Get(cacheKey) as string;
                    //DXHHelper.GetLatestActivityDate(this._crmObjectType, crmId);
                    if (!string.IsNullOrEmpty(crmValue))
                    {
                    DateTime lastConversionDate = EkFunctions.ReadDbDate(crmValue);
                    if (lastConversionDate == DateTime.MinValue)
                        lastConversionDate = DateTime.MaxValue;
                    switch (targetValue)
                    {
                        case "LastDay":
                        case "1DayAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 1);
                            break;
                        case "1WeekAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 7);
                            break;
                        case "1MonthAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 30);
                            break;
                        case "3MonthsAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 90);
                            break;
                        case "6MonthsAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 180);
                            break;
                        case "1YearAgo":
                            inRange = ((DateTime.Now - lastConversionDate).Days <= 365);
                            break;
                        case "MoreThan1Year":
                            inRange = ((DateTime.Now - lastConversionDate).Days > 365);
                            break;
                    }
                    // inRange = 
                        switch (op)
                    {
                            case "EQ":
                                rtnVal = inRange;
                                break;
                            case "NE":
                                rtnVal = !inRange;
                                break;
                    }
                }
                    else
                    {
                        GetAndCacheLatestActivityDate(crmId, this._crmObjectType, cacheKey, this.DXHHelper);
                    }
                }
                catch (Exception ex)
                {
                    EkException.LogException(ex);
                    return rtnVal;
                }
            }
            return rtnVal;
        }

        private Dictionary<string, Field> _fields = null;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (null == _fields)
                {
                    _fields = new Dictionary<string, Field>();
                    this.AddField(_fldOperator);
                    EnumField field = new EnumField(new LocalizableValue("sfProduct", "Product", ""));

                    field.Options.Add(new LocalizableValue("LastDay", "Within the Last Day", ""));
                    field.Options.Add(new LocalizableValue("1WeekAgo", "1 Week Ago", ""));
                    field.Options.Add(new LocalizableValue("1MonthAgo", "1 Month Ago", ""));
                    field.Options.Add(new LocalizableValue("3MonthsAgo", "3 Months Ago", ""));
                    field.Options.Add(new LocalizableValue("6MonthsAgo", "6 Months Ago", ""));
                    field.Options.Add(new LocalizableValue("1YearAgo", "1 Year Ago", ""));
                    field.Options.Add(new LocalizableValue("MoreThan1Year", "More Than 1 Year Ago", ""));
                    _fields.Add("value", field);
                }
                return _fields;
            }
        }

        public override string Text
        {
            get { return "Last Conversion Date {operator} {value}"; }
        }
    }

	public class DXHNumericTemplate : DXHRuleTemplate
    {
        NumericOperatorField _fldOperator = null;
		string _fieldName = "";
        string _crmFieldName = "";

        public DXHNumericTemplate(string fieldName)
            : base(fieldName)
        {
            _fldOperator = new NumericOperatorField("operator", "operator", "");
        }
        public DXHNumericTemplate(string fieldName, DXHHelper dxhHelper, string crmObjectType, string crmFieldName)
            : base(fieldName, dxhHelper, crmObjectType)
        {
            _fldOperator = new NumericOperatorField("operator", "operator", "");
            _fieldName = fieldName;
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
            this._crmFieldName = crmFieldName;
        }

        public override bool Evaluate(Rule rule)
        {
            string crmId = CRMObjectId;
            bool rtnVal = false;

            // If we don't have their SF ID, then the rule will always return false
            if (!string.IsNullOrEmpty(crmId))
            {
                string op = rule.Values["operator"];
                double targetValue = 0;
                double crmValue = 0;
                double.TryParse(rule.Values["value"], out targetValue);

                try
                {
                    var cacheKey = this.GetCachKey(rule, crmId);
                    var objectVal = RuleCache.Get(cacheKey) as string;
                    if (!string.IsNullOrEmpty(objectVal))
                    {
                        crmValue = EkFunctions.ReadDoubleValue(objectVal, 0);
                        rtnVal = _fldOperator.Evaluate(crmValue, op, targetValue);
                }
                    else
                    {
                        GetAndCacheObjectValue(crmId, this._crmObjectType, _crmFieldName, cacheKey, this.DXHHelper);
                    }
                }
                catch (Exception ex)
                {
                    EkException.LogException(ex);
                    return rtnVal;
                }
            }

            // If we get to here, then we never found a match.  Return false;
            return rtnVal;
        }

        private Dictionary<string, Field> _fields = null;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (null == _fields)
                {
                    _fields = new Dictionary<string, Field>();
                    this.AddField(_fldOperator);
                    this.AddStringField("value", "Value", "");
                }
                return _fields;
            }
        }

        public override string Text
        {
            get { return this._crmObjectType + " " + _fieldName + " {operator} {value}"; }
        }
    }
	
    public class DXHStringTemplate : DXHRuleTemplate
    {
        StringOperatorField _fldOperator = null;
        string _fieldName = "";
        string _crmFieldName = "";

        public DXHStringTemplate(string fieldName)
            : base(fieldName)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
        }

        public DXHStringTemplate(string fieldName, DXHHelper dxhHelper, string crmObjectType, string crmFieldName)
            : base(fieldName, dxhHelper, crmObjectType)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
            _fieldName = fieldName;
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
            this._crmFieldName = crmFieldName;
        }

        public override bool Evaluate(Rule rule)
        {
            string crmId = CRMObjectId;
            bool rtnVal = false;

            // If we don't have their CRM ID, then the rule will always return false
            if (!string.IsNullOrEmpty(crmId))
            {
                string op = rule.Values["operator"];
                string targetValue = rule.Values["value"];
                string crmValue = string.Empty;

                try
                {
                    var cacheKey = this.GetCachKey(rule, crmId);
                    crmValue = RuleCache.Get(cacheKey) as string;
                    if (!string.IsNullOrEmpty(crmValue))
                    {
                        rtnVal = _fldOperator.Evaluate(crmValue.Trim().ToLower(), op, targetValue.Trim().ToLower());
                    }
                    else
                    {
                        GetAndCacheObjectValue(crmId, this._crmObjectType, _crmFieldName, cacheKey, this.DXHHelper);
                    }

                }
                catch (Exception ex)
                {
                    EkException.LogException(ex);
                    return rtnVal;
                }
            }

            // If we get to here, then we never found a match.  Return false;
            return rtnVal;
        }

        private Dictionary<string, Field> _fields = null;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (null == _fields)
                {
                    _fields = new Dictionary<string, Field>();
                    this.AddField(_fldOperator);
                    this.AddStringField("value", "Value", "");
                }
                return _fields;
            }
        }

        public override string Text
        {
            get { return this._crmObjectType + " " + _fieldName + " {operator} {value}"; }
        }
    }

    public class DXHPickListTemplate : DXHRuleTemplate
    {
        StringOperatorField _fldOperator = null;
        string _picklistName = "";
        string _crmFieldName = "";

        public DXHPickListTemplate(string picklistName)
            : base(picklistName)
        {
            _picklistName = picklistName;
            _fldOperator = new StringOperatorField("operator", "operator", "");
        }
        public DXHPickListTemplate(string picklistName, DXHHelper dxhHelper, string crmObjectType, string crmFieldName)
            : base(picklistName, dxhHelper, crmObjectType)
        {
            _fldOperator = new StringOperatorField("operator", "operator", "");
            _picklistName = picklistName;
            this._dxhHelper = dxhHelper;
            this._crmObjectType = crmObjectType;
            this._crmFieldName = crmFieldName;
        }

        public override bool Evaluate(Rule rule)
        {
            string crmId = CRMObjectId;
            bool rtnVal = false;
            // If we don't have their ID, then the rule will always return false
            if (!string.IsNullOrEmpty(crmId))
            {
                string op = rule.Values["operator"];
                string targetValue = rule.Values["value"];
                var cacheKey = this.GetCachKey(rule, crmId);

                var currentValue = RuleCache.Get(cacheKey) as string;

                if (!string.IsNullOrEmpty(currentValue))
                {
                    rtnVal = _fldOperator.Evaluate(targetValue.Trim().ToLower(), op, currentValue.Trim().ToLower());
                }
                else
                {
                    GetAndCacheObjectValue(crmId, this._crmObjectType, _crmFieldName, cacheKey, this.DXHHelper);
                }
            }

            // If we get to here, then we never found a match.  Return false;
            return rtnVal;
        }

        private Dictionary<string, Field> _fields = null;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (null == _fields)
                {
                    _fields = new Dictionary<string, Field>();
                    this.AddField(_fldOperator);
                    EnumField field = new EnumField(new LocalizableValue("crmPicklist", _picklistName, ""));

                    Dictionary<string, string> valueList = DXHHelper.GetFieldPickListDefinitions(this._crmObjectType, _picklistName, _crmFieldName);
                    foreach (KeyValuePair<string, string> option in valueList)
                    {
                        field.Options.Add(new LocalizableValue(option.Value, option.Key, ""));
                    }
                    _fields.Add("value", field);
                }
                return _fields;
            }
        }

        public override string Text
        {
            get { return this._crmObjectType + " " + _picklistName + " {operator} {value}"; }
        }
    }

    #endregion

    public class DXHHelper
    {
        protected const string _helperCacheKey = "Ektron.CMS.DXH.DXHHelper.{0}.{1}";
        private ContextBusClient _client;
        private bool _enabled;
        private string _connectorName = "Salesforce";
        private List<DxHUserConnectionData> _visitorConnectionList;
        private List<Connection> _connectionList;
        private ConnectionManagerClient DxHConnectionManager;

        private static Cache HelperCache
        {
            get
            {
                return HttpRuntime.Cache ?? null;
            }
        }

        public DXHHelper(ContextBusClient dxhClient, List<DxHUserConnectionData> visitorConnections, string connectorName, bool editMode)
        {
            this.DxHConnectionManager = new Ektron.DxH.Client.ConnectionManagerClient(DxHUtils.ContextBusEndpoint);
            _connectorName = connectorName;
            // _visitorConnectionList = visitorConnections.FindAll(x => x.ConnectorName == connectorName);
            _connectionList = this.GetConnectionList().FindAll(connection =>
            {
                return connection.ConnectorName == _connectorName;
            });  //
            if (_connectionList.Count > 0)
            {
                _visitorConnectionList = visitorConnections.FindAll(x => x.ConnectionName == _connectionList[0].Name);
            }
            ContextBusClient _cachedClient = HelperCache.Get(GetCacheKey("DXHClient", connectorName)) as ContextBusClient;
            bool loggedIn = _cachedClient != null;
            if (loggedIn)
                _client = _cachedClient;
            else
                _client = dxhClient;

            string connectionName = "";
            if (_connectionList.Count > 0)
            {
                connectionName = _connectionList[0].Name;
                if (!string.IsNullOrEmpty(connectionName))
                {
                    _enabled = true;
                    try
                    {
                        if (!loggedIn || editMode)
                        {
                            DXHClient.Login(connectionName, ConnectorName);
                            HelperCache.Add(GetCacheKey("DXHClient", connectorName), DXHClient, null, ConnectionCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                        }
                    }
                    catch
                    {
                        _enabled = false;
                    }
                }
            }
        }

        private List<Connection> GetConnectionList()
        {
            var cacheKey = "Ektron|DxH|Helper|ConnectionList";
            var rtnVal = HelperCache.Get(cacheKey) as List<Connection>;
            if (rtnVal == null)
            {
                rtnVal = this.DxHConnectionManager.GetAll().ToList();
                HelperCache.Add(cacheKey, rtnVal, null, ConnectionCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return rtnVal;
        }
        public ContextBusClient DXHClient 
        {
            get 
            { 
                return _client;
            }
        }

        private ConnectionManagerClient _ConnectionManagerClient;
        public ConnectionManagerClient ConnectionManagerClient
        {
            get
            {
                if (null == _ConnectionManagerClient)
                {
                    _ConnectionManagerClient = new ConnectionManagerClient();
                }
                return _ConnectionManagerClient;
            }
        }
        


        public bool Enabled { get { return _enabled; } }
        public string ConnectorName { get { return _connectorName; } }
        public List<DxHUserConnectionData> UserConnections { get { return _visitorConnectionList; } }
        public List<Connection> Connections { get { return _connectionList; } }
        public List<ConnectionParam> GetConnectionParameters(string connectionName, string connectorName)
        {
            var parameters = HelperCache.Get(GetCacheKey("ConnectionParameters", connectionName + "." + connectorName)) as List<ConnectionParam>;
            if (parameters == null)
            {
                parameters = ConnectionManagerClient.LoadConnection(connectionName, connectorName).ToList();
                HelperCache.Add(GetCacheKey("ConnectionParameters", connectionName + "." + connectorName), parameters, null, PersistentCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return parameters;
        }
        public List<FlyweightObjectDefinition> GetObjectDefinitionNameList()
        {
            var objList = HelperCache.Get(GetCacheKey("ObjectDefinitionNameList", ConnectorName)) as List<FlyweightObjectDefinition>;
            if (objList == null)
            {
                objList = DXHClient.GetObjectDefinitionNameList(ConnectorName);
                HelperCache.Add(GetCacheKey("ObjectDefinitionNameList", ConnectorName), objList, null, PersistentCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return objList;
        }
        public List<ObjectDefinition> GetObjectPropeties(List<FlyweightObjectDefinition> objFlyList)
        {
            string objName = "";
            if (objFlyList.Count > 0)
                objName = objFlyList[0].Id;

            var objList = HelperCache.Get(GetCacheKey(ConnectorName + ".ObjectPropeties", objName)) as List<ObjectDefinition>;
            if (objList == null)
            {
                objList = DXHClient.GetObjectDefinitionList(objFlyList, ConnectorName);
                HelperCache.Add(GetCacheKey(ConnectorName + ".ObjectPropeties", objName), objList, null, PersistentCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            return objList;
        }
        public Dictionary<string, string> GetFieldPickListDefinitions(string objectName, string fieldName, string crmFieldName)
        {
            Dictionary<string, string> fieldOptions = new Dictionary<string, string>();
            ObjectDefinition objDef = GetObjectPropeties(objectName);
            List<FieldDefinition> pickListFlds = objDef.Fields.FindAll(f => f.DataType.Picklist != null && (f.DisplayName == fieldName || f.Id == crmFieldName));
            if (pickListFlds.Count > 0)
            {
                FieldDefinition fld = pickListFlds[0];
                //... show the fields Id and its picklist key/value pairs...
                string fieldId = fld.Id;
                Ektron.DxH.Common.Objects.FieldType.NameValuePairs fldPickList = fld.DataType.Picklist;

                foreach (string k in fldPickList.Keys)
                {
                    string picklistId = k;
                    object picklistValue = fldPickList[k];
                    fieldOptions.Add(picklistId, picklistValue.ToString());
                }
            }
            return fieldOptions;
        }
        public string GetObjectValue(string objectName, string objectId, string fieldName)
        {
            string fieldValue = "";
            if (UserConnections.Count > 0)
            {
                ObjectInstance obj = GetObject(objectName, objectId);
                fieldValue = GetObjectValue(obj, fieldName);
            }
            return fieldValue;
        }

        private string GetObjectValue(ObjectInstance obj, string fieldName)
        {
            return GetObjectValue(obj, fieldName, false);
        }
        private string GetObjectValue(ObjectInstance obj, string fieldName, bool useId)
        {
            string fieldValue = "";
            Ektron.DxH.Common.Objects.Field field = obj.Fields.SingleOrDefault(f => (f.Id == fieldName || f.DisplayName == fieldName));
            if (field != null)
            {
                fieldValue = field.Value.ToString();
            }
            return fieldValue;
        }
        public ObjectDefinition GetObjectPropeties(string objectName)
        {
            List<FlyweightObjectDefinition> rqstd = new List<FlyweightObjectDefinition>();
            FlyweightObjectDefinition objDef = GetObjectDefinition(objectName);
            if (objDef != null)
                rqstd.Add(objDef);
            List<ObjectDefinition> objDefinitions = GetObjectPropeties(rqstd);
            if (objDefinitions.Count > 0)
                return objDefinitions[0];
            else
                return null;

        }
        private FlyweightObjectDefinition GetObjectDefinition(string objectName)
        {
            List<FlyweightObjectDefinition> flys = GetObjectDefinitionNameList();
            FlyweightObjectDefinition obj = flys.SingleOrDefault(f => f.Id.ToLower() == objectName.ToLower());
            return obj;
        }
        private ObjectInstance GetObject(string objectName, string objectId)
        {
            ObjectInstance obj = null;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();
            keyValues.Add("", objectId);
            obj = HelperCache.Get(GetCacheKey(objectName, objectId)) as ObjectInstance;
            if (obj == null)
            {
                try
                {
                    obj = DXHClient.GetObjectInstance(GetObjectPropeties(objectName), keyValues, ConnectorName);
                    HelperCache.Add(GetCacheKey(objectName, objectId), obj, null, TransientCacheExpirationDateTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch (Exception ex)
                {
                    EkException.LogException(ex);
                }
            }
            return obj;
        }
        public List<ObjectInstance> GetActivity(string objectId)
        {
            List<ObjectInstance> activityList = new List<ObjectInstance>();
            if (ConnectorName == "Marketo")
            {
                OperationInstance op = new OperationInstance();
                op.Id = "GetLeadActivitiesByUser";
                op.InputValues = new List<Ektron.DxH.Common.Objects.Field>();
                op.InputValues.Add(new Ektron.DxH.Common.Objects.Field(new FieldDefinition("userid", "", null, false, false))
                {
                    Value = objectId
                });
                op.InputValues.Add(new Ektron.DxH.Common.Objects.Field(new FieldDefinition("campaign", "", null, false, false))
                {
                    Value = ""
                });
                List<Ektron.DxH.Common.Objects.Field> response = DXHClient.InvokeOperation(op, ConnectorName);
                for (int i = 0; i < response.Count; i += 10)
                {
                    ObjectInstance act = new ObjectInstance(new ObjectDefinition("ActivityRecord"));
                    for (int j = 0; j < 10; j++)
                    {
                        act.Fields.Add(response[i + j]);
                    }
                    activityList.Add(act);
                }
            }
            return activityList;
        }
            public string GetLatestActivityDate(string objectName, string objectId)
        {
            string val = "";
            ObjectInstanceList objList = null;
            if (this.ConnectorName == "Salesforce")
            {
                string objDefId = string.Format("{0} where WhoId = '{1}' limit 1", "Task", objectId);
                ObjectDefinition objDef = new ObjectDefinition(objDefId);
                objDef.Fields.Add(new FieldDefinition("LastModifiedDate", "", null, false, false));
                try
                {
                    //PagingToken<List<ObjectInstance>> pagingToken = new PagingToken<List<ObjectInstance>>()
                    //pagingToken.CurrentPage = 1;

                    ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter(objDef);

                    criteria.Paging = new PagingInformation(-1);
                    criteria.Paging.CurrentPage = 1;
                    objList = DXHClient.GetObjectInstanceList(objDef, criteria, ConnectorName);
                    if (objList.Results.Count > 0)
                    {
                        ObjectInstance obj = objList.Results[0];
                        val = GetObjectValue(obj, "LastModifiedDate", true);
                    }
                }
                catch
                {
                    val = "";
                }
            }
            return val;
        }

        private string GetCacheKey(string objectType, string objectId)
        {
            return string.Format(_helperCacheKey, objectType, objectId);
        }

        private DateTime TransientCacheExpirationDateTime
        {
            get
            {
                return DateTime.Now.AddMinutes(6);
            }
        }
        private DateTime ConnectionCacheExpirationDateTime
        {
            get
            {
                return DateTime.Now.AddMinutes(15);
            }
        }
        private DateTime PersistentCacheExpirationDateTime
        {
            get
            {
                if (DateTime.Now.Hour < 6)
                    return DateTime.Today.AddHours(6);
                else
                    return DateTime.Today.AddDays(1).AddHours(6);
            }
        } 
    }

    public class TargetingRulesConfiguration : List<DXHConnectorConfiguration>
    {
        #region Class Configuration

        protected static string _configurationCacheKey = "Ektron.CMS.DXH.TargetingRulesConfiguration";
        protected static readonly object lockObj = new object();
        private static double _cacheMinutes = 5;
        protected static string filename = "dxhTargetingRules.config";

        #endregion

        #region Retrieve Methods

        public static TargetingRulesConfiguration Current()
        {
            TargetingRulesConfiguration currentConfig = (TargetingRulesConfiguration)HttpContext.Current.Cache[_configurationCacheKey];
            if (currentConfig == null)
            {
                lock (lockObj)
                {
                    currentConfig = ReadValues(System.IO.Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, filename));
                    HttpContext.Current.Cache.Add(_configurationCacheKey, currentConfig, new CacheDependency(HttpContext.Current.Request.PhysicalApplicationPath + filename), DateTime.MaxValue, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                }
            }
            return currentConfig;
        }

        #endregion

        #region Private helpers

        private static TargetingRulesConfiguration ReadValues(string filePath)
        {
            TargetingRulesConfiguration config = new TargetingRulesConfiguration();
            XmlDocument doc = null;
            try
            {
                doc = new XmlDocument();
                doc.Load(filePath);
                config.AddRange(
                    GetConnector(doc.SelectNodes("//Connector"))
                );
            }
            catch (System.IO.FileNotFoundException e)
            {
                return new TargetingRulesConfiguration();
            }
            catch (Exception ex)
            {
                // invalid config
                return null;
            }
            return config;
        }

        static List<DXHConnectorConfiguration> GetConnector(XmlNodeList nodeList)
        {
            List<DXHConnectorConfiguration> connectorList = new List<DXHConnectorConfiguration>();
            foreach (XmlNode node in nodeList)
            {
                string connectorName = node.Attributes["name"].Value;
                DXHConnectorConfiguration connector = new DXHConnectorConfiguration() { Name = connectorName };
                connector.AddRange(GetConnectorObject(node.ChildNodes));
                connectorList.Add(connector);
            }
            return connectorList;
        }
        static List<DXHConnectorObjectConfiguration> GetConnectorObject(XmlNodeList nodeList)
        {
            List<DXHConnectorObjectConfiguration> objectList = new List<DXHConnectorObjectConfiguration>();
            foreach (XmlNode node in nodeList)
            {
                string objName = node.Attributes["name"].Value;
                DXHConnectorObjectConfiguration obj = new DXHConnectorObjectConfiguration() { Name = objName };
                obj.AddRange(GetConnectorObjectProperty(node.ChildNodes));
                objectList.Add(obj);
            }
            return objectList;
        }
        static List<DXHConnectorObjectPropertyConfiguration> GetConnectorObjectProperty(XmlNodeList nodeList)
        {
            List<DXHConnectorObjectPropertyConfiguration> propList = new List<DXHConnectorObjectPropertyConfiguration>();
            foreach (XmlNode node in nodeList)
            {
                string ruleName = node.Attributes["name"].Value;
                string dxhFieldName = node.Attributes["value"].Value;
                propList.Add(
                    new DXHConnectorObjectPropertyConfiguration()
                    {
                        RuleName = ruleName,
                        DXHFieldName = dxhFieldName
                    }
                );
            }
            return propList;
        }
        #endregion

    }

    /// <summary>
    /// Items in the TargetingRulesConfiguration.
    /// </summary>
    public class DXHConnectorConfiguration : List<DXHConnectorObjectConfiguration>
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Items in the DXHConnectorObjectConfiguration.
    /// </summary>
    public class DXHConnectorObjectConfiguration : List<DXHConnectorObjectPropertyConfiguration>
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Items in the DXHConnectorObjectConfiguration.
    /// </summary>
    public class DXHConnectorObjectPropertyConfiguration
    {
        public string RuleName { get; set; }
        public string DXHFieldName { get; set; }
    }
}

