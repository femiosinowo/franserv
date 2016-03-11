using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Ektron.Cms;
using Ektron.DxH.Client;
using Ektron.DxH.Common.Connectors;
using Ektron.DxH.Common.Events;
using Ektron.DxH.Common.Objects;

/// <summary>
/// Summary description for SharePointClientImportHelper
/// </summary>
public class SharePointClientImportHelper
{
    #region constructors

    public SharePointClientImportHelper() { }

    #endregion

    #region members

    private static SharePointClientImportHelper _instance = null;
    private static int CurrentPage = 1;
    public Thread MainThread = null;
    public static string AdapterName = string.Empty;
    public static string ConnectionId = string.Empty;
    public static ObjectDefinition SharePointObjectDefinition = null;
    public static string WorkflowName = string.Empty;
    public static int ContextBusThreadCount = 5;
    public static int TotalPages = 1;

    #endregion

    #region public properties

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static SharePointClientImportHelper GetInstance()
    {
        if (_instance == null)
            _instance = new SharePointClientImportHelper();
        return _instance;
    }

    #endregion

    #region public methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sharepointConnectionId"></param>
    /// <param name="adaptername"></param>
    /// <param name="objectdefinitionId"></param>
    public void StartImport(string sharepointConnectionId, string adaptername, string workflowName, ObjectDefinition objectdefinition)
    {

        ConnectionId = sharepointConnectionId;
        AdapterName = adaptername;
        SharePointObjectDefinition = objectdefinition;
        WorkflowName = workflowName;
        MainThread = new Thread(SharePointClientImportHelper.Start);
        MainThread.Priority = ThreadPriority.Lowest;
        MainThread.SetApartmentState(ApartmentState.STA);//Creating a single threaded apartment.
        MainThread.Start();
        Thread.Sleep(100);

    }

    #endregion

    #region

    private static void Start()
    {
        try
        {
            CurrentPage = 1;
            TotalPages = 1;
            ContextBusThreadCount = 5;
            ObjectInstanceList spItemList = null;
            ContextBusClient contextbusClient = new ContextBusClient();
            EventManagerClient eventClient = new EventManagerClient();
            contextbusClient.Login(ConnectionId, AdapterName);
            ObjectInstanceCriteriaFilter criteria = new ObjectInstanceCriteriaFilter(SharePointObjectDefinition);
            criteria.AddFilter("FSObjType", ObjectInstanceCriteriaFilterOperator.EqualTo, 0);
            criteria.AddFilter("FileDirRef", ObjectInstanceCriteriaFilterOperator.EqualTo, "EntireListWithPagination");
            criteria.Paging = new PagingInformation();
            criteria.Paging.CurrentPage = CurrentPage;
            criteria.Paging.RecordsPerPage = ContextBusThreadCount;

            while (CurrentPage <= TotalPages)
            {
                try
                {
                    spItemList = contextbusClient.GetObjectInstanceList(SharePointObjectDefinition, criteria, AdapterName);
                    TotalPages = spItemList.Paging.TotalPages;
                }
                catch (Exception exp)
                {
                    EkException.LogException(exp);
                }
                if (spItemList != null && spItemList.Results.Any())
                {
                    foreach (ObjectInstance spItem in spItemList.Results)
                    {
                        spItem.Fields.Where(p => p.Id.Equals("DxHSource")).FirstOrDefault().Value = "FromEktron";
                        EventInstance itemEvent = new EventInstance()
                        {
                            Id = WorkflowName,
                            Payload = spItem
                        };

                        eventClient.RaiseEvent(itemEvent);
                    }
                }
                CurrentPage++;
                criteria.Paging.CurrentPage = CurrentPage;
            }

            contextbusClient.Logout(AdapterName);

            //Forcing a garbage collection
            GC.Collect();
            GC.Collect();
            GC.Collect();
        }

        catch (ThreadAbortException tx)
        {
            //Threadaborts are ok...ASP.Net aborts thread when unloading application
            EkException.LogException(tx, System.Diagnostics.EventLogEntryType.Warning);
        }
        catch (Exception ex)
        {
            EkException.LogException(ex);
        }
        Thread.Sleep(1000);
    }

    #endregion
}