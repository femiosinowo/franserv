using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ektron.Cms;
using Ektron.Cms.Common;
using Ektron.Cms.Content;
using System.Configuration;
using Ektron.Cms.Organization;
using Ektron.Cms.Instrumentation;
using Ektron.Cms.Framework.Settings.UrlAliasing;
using Ektron.Cms.Settings.UrlAliasing.DataObjects;

namespace SignalGraphics.CMS
{
    /// <summary>
    /// Summary description for AliasHelper
    /// </summary>
    public class AliasHelper
    {
        private static AliasManager aMngr = new AliasManager( Ektron.Cms.Framework.ApiAccessMode.Admin);

        public AliasHelper()
        {
            //constructor
        }

        public static AliasData GetItem(long aliasId)
        {
            return aMngr.GetItem(aliasId);
        }

        public static AliasData GetAlias(long contentId, int langId = 1033, EkEnumeration.TargetType targetType = EkEnumeration.TargetType.Content)
        {
            return aMngr.GetAlias(contentId, langId, targetType);
        }

        public static List<AliasData> GetList(AliasCriteria aliasCriteria)
        {
            return aMngr.GetList(aliasCriteria);
        } 

        public static AliasData Add(AliasData aliasData)
        {
            return aMngr.Add(aliasData);
        }

        public static AliasData Update(AliasData aliasData)
        {
            return aMngr.Update(aliasData);
        }

        public static void Delete(long aliasId)
        {
            aMngr.Delete(aliasId);
        }

        public static void AddManualAlias(string alias, long contentId, bool IsDefault = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(alias) && contentId > 0)
                {
                    AliasData aliasdata = new AliasData();
                    aliasdata.Type = EkEnumeration.AliasRuleType.Manual;
                    aliasdata.TargetType = EkEnumeration.TargetType.Content;
                    aliasdata.LanguageId = 1033;
                    aliasdata.IsEnabled = true;
                    aliasdata.IsDefault = IsDefault;
                    aliasdata.Alias = alias;
                    aliasdata.QueryStringAction = EkEnumeration.QueryStringActionType.Append;
                    aliasdata.TargetId = contentId;
                    AliasHelper.Add(aliasdata);
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }

        public static void UpdateManualAlias(string alias, long contentId, bool isDefault = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(alias) && contentId > 0)
                {
                    AliasCriteria aCriteria = new AliasCriteria();
                    aCriteria.AddFilter(AliasProperty.TargetId, CriteriaFilterOperator.EqualTo, contentId);
                    var aliasList = GetList(aCriteria);
                    if (aliasList != null && aliasList.Any())
                    {
                        var aliasdata = aliasList[0];
                        if (!aliasdata.Alias.Contains(alias))
                        {
                            aliasdata.Alias = alias;
                            aliasdata.IsDefault = isDefault;
                            aliasdata.IsEnabled = true;
                            aliasdata.QueryStringAction = EkEnumeration.QueryStringActionType.Append;
                            aliasdata.TargetType = EkEnumeration.TargetType.Content;
                            aliasdata.Type = EkEnumeration.AliasRuleType.Manual;
                            Update(aliasdata);
                            Log.WriteInfo("Alias " + alias + " updated for the content with Id: " + contentId + "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteError(ex);
            }
        }
    }
}