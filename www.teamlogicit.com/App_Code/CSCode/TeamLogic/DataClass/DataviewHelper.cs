using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace TeamLogic.CMS
{
    /// <summary>
    /// This class is used to help with any kind of DataTable operation
    /// </summary>
    public class DataviewHelper
    {
        public DataviewHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable SelectTopDataRow(DataTable dt, int count)
        {
            DataTable dtn = dt.Clone();
            int dtLength = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (i < dtLength)
                {
                    dtn.ImportRow(dt.Rows[i]);
                }
            }

            return dtn;
        }
    }
}