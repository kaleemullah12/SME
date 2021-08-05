using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace SMEForm.Helper
{
    public class Util
    {
        public static bool Overlaps(TimeSpan s1, TimeSpan e1, TimeSpan s2, TimeSpan e2)
        {
            return ((s1 >= s2 && s1 < e2) || (e1 <= e2 && e1 > s2) || (s1 <= s2 && e1 >= e2));
        }
        public static string tblToCsv(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            return sb.ToString();
        }
        public static string ApplicationPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ExportFolder"].ToString();
            }
        }
        public static string ReportServerURL
        {
            get
            {
                return ConfigurationManager.AppSettings["ReportServerURL"].ToString();
            }
        }
        public static string ReportFolder
        {
            get
            {
                return ConfigurationManager.AppSettings["ReportFolder"].ToString();
            }
        }
        public static string RSUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["RSUserName"].ToString();
            }
        }
        public static string RSDomain
        {
            get
            {
                return ConfigurationManager.AppSettings["RSDomain"].ToString();
            }
        }
        public static string RSPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["RSPassword"].ToString();
            }
        }
    }
}