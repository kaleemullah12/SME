using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using CsvHelper;
namespace SMEForm.Helper
{
    public class CSVParser
    {
       public static string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
        public static DataTable CSVtoDataTable(string fileString, bool includeEmpty)
        {
            DataTable dt = new DataTable();
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                string[] columns = sr.ReadLine().Split(",".ToCharArray());
                foreach (var columnName in columns)
                    dt.Columns.Add(new DataColumn(columnName));
                int cols = columns.Length;
                int numNulls = 0;
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = false;
                    while (csv.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < cols; i++)
                        {
                            string value = csv.GetField(i);
                            dr[i] = value;
                            if (string.IsNullOrEmpty(value))
                                numNulls++;
                        }
                        if (numNulls < cols / 2 || includeEmpty)
                            dt.Rows.Add(dr);
                        numNulls = 0;
                    }
                }
            }
            return dt;
        }
        public static DataTable GetTimeSheetImportTable_New()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("WorkID"));
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("ClockIn"));
            dt.Columns.Add(new DataColumn("ClockOut"));
            dt.Columns.Add(new DataColumn("WorkHour"));
            dt.Columns.Add(new DataColumn("ImportedBy"));
            dt.Columns.Add(new DataColumn("ImportedTime"));
            dt.Columns.Add(new DataColumn("ValidationBitString"));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("IsImported"));
            dt.Columns.Add(new DataColumn("ImportType"));
            dt.Columns.Add(new DataColumn("Firstname"));
            dt.Columns.Add(new DataColumn("Lastname"));
            return dt;
        }

        public static DataTable GetHolidaysImportTable_New()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("WorkID"));
            dt.Columns.Add(new DataColumn("FromDate"));
            dt.Columns.Add(new DataColumn("ToDate"));
            dt.Columns.Add(new DataColumn("IsActive"));
            dt.Columns.Add(new DataColumn("CreatedBy"));
            dt.Columns.Add(new DataColumn("CreatedTime"));
            dt.Columns.Add(new DataColumn("ModifiedBy"));
            dt.Columns.Add(new DataColumn("ModifiedTime"));
            dt.Columns.Add(new DataColumn("Hours"));
            dt.Columns.Add(new DataColumn("Days"));
            dt.Columns.Add(new DataColumn("IsClear"));
            dt.Columns.Add(new DataColumn("IsFullPay"));
            return dt;
        }

        public static DataTable GetStatutaryImportTable_New()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("WorkID"));
            dt.Columns.Add(new DataColumn("RMFID"));
            dt.Columns.Add(new DataColumn("FromDate"));
            dt.Columns.Add(new DataColumn("ToDate"));
            dt.Columns.Add(new DataColumn("LeaveYear"));
            dt.Columns.Add(new DataColumn("SSPSMP"));
            dt.Columns.Add(new DataColumn("EmpName"));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("CreatedBy"));
            dt.Columns.Add(new DataColumn("CreatedTime"));
            return dt;
        }

        public static DataTable GetTimeSheetImportTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add(new DataColumn("WorkID"));
            dt.Columns.Add(new DataColumn("Date"));
            dt.Columns.Add(new DataColumn("ClockIn"));
            dt.Columns.Add(new DataColumn("ClockOut"));
            dt.Columns.Add(new DataColumn("WorkHour"));
            dt.Columns.Add(new DataColumn("ImportedBy"));
            dt.Columns.Add(new DataColumn("ImportedTime"));
            dt.Columns.Add(new DataColumn("ValidationBitString"));
            dt.Columns.Add(new DataColumn("FileName"));
            dt.Columns.Add(new DataColumn("IsImported"));
            dt.Columns.Add(new DataColumn("ImportType"));
            dt.Columns.Add(new DataColumn("Firstname"));
            dt.Columns.Add(new DataColumn("Lastname"));
            return dt;
        }
       public static DataTable ReadGenericFile(string fileString, string fileName)
        {
            DataTable dt = GetTimeSheetImportTable();
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    csv.Read();
                    csv.Read();
                    csv.Read();
                    List<string> dates = new List<string>();
                    dates.Add(csv.GetField(7));
                    dates.Add(csv.GetField(9));
                    dates.Add(csv.GetField(11));
                    dates.Add(csv.GetField(13));
                    dates.Add(csv.GetField(15));
                    dates.Add(csv.GetField(17));
                    dates.Add(csv.GetField(19));
                    csv.Read();
                    while (csv.Read())
                    {
                        for (int i = 0; i < 7; i++ )
                        {
                            DataRow dr = dt.NewRow();
                            string workID = csv.GetField(5);
                            string firstname = csv.GetField(4);
                            string lastname = string.Empty;
                            string date = dates[i];
                            string clockin = csv.GetField(7 + i*2);
                            string clockout = csv.GetField(8 + i*2);
                            string workhour = string.Empty;
                            TimeSpan start, end;
                            DateTime tmpdate;
                            if (TimeSpan.TryParse(clockin, out start) && TimeSpan.TryParse(clockout, out end) && DateTime.TryParse(date, out tmpdate))
                            {
                                if (end > start)
                                    workhour = (end.TotalHours - start.TotalHours).ToString();
                                else
                                    workhour = (end.TotalHours + 24 - start.TotalHours).ToString();
                                date = tmpdate.ToString("dd-MMM-yyyy");
                            }

                            if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(clockin) &&
                                !string.IsNullOrEmpty(clockout) && !string.IsNullOrEmpty(workhour))
                            {
                                dr["WorkID"] = workID;
                                dr["Date"] = date;
                                dr["ClockIn"] = clockin;
                                dr["ClockOut"] = clockout;
                                dr["WorkHour"] = workhour;
                                dr["ImportedBy"] = HttpContext.Current.User.Identity.Name;
                                dr["FileName"] = fileName;
                                dr["ImportType"] = 2;
                                dr["Firstname"] = firstname;
                                dr["Lastname"] = lastname;
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            return dt;
        }
        public static DataTable ReadKFCFile_New(string fileString, string fileName)
        {
            DataTable dt = GetTimeSheetImportTable();
            //Get Excel Info
           // string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("select * from ImpExcelRef where CompanyType='KFC'", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            var txtworkId = 0;
            var txtdate = 0;
            var txtclockin = 0;
            var txtclockout = 0;
            var txtfirstName = 0;
            var txtworkHour = 0;
            while (rdr.Read())
            {
                txtworkId = Convert.ToInt32(rdr["WorkID"]);
                txtdate = Convert.ToInt32(rdr["Date"]);
                txtclockin = Convert.ToInt32(rdr["ClockIn"]);
                txtclockout= Convert.ToInt32(rdr["ClockOut"]);
                txtfirstName= Convert.ToInt32(rdr["FirstName"]);
                txtworkHour = Convert.ToInt32(rdr["WorkHour"]);
            }
            
          
            con.Close();
            //
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    while (csv.Read())
                    {
                        DataRow dr = dt.NewRow();
                        string workID = csv.GetField(txtworkId);
                        string date = csv.GetField(txtdate);
                        string clockin = csv.GetField(txtclockin);
                        string clockout = csv.GetField(txtclockout);
                        string workhour = csv.GetField(txtworkHour); 
                        string firstname = csv.GetField(txtfirstName);
                        string lastname = string.Empty;
                        TimeSpan start, end;
                        DateTime tmpdate;
                        if (TimeSpan.TryParse(clockin, out start) && TimeSpan.TryParse(clockout, out end) && DateTime.TryParse(date, out tmpdate))
                        {
                            if (end > start)
                                workhour = (end.TotalHours - start.TotalHours).ToString();
                            else
                                workhour = (end.TotalHours + 24 - start.TotalHours).ToString();
                            date = tmpdate.ToString("dd-MMM-yyyy");
                        }

                        if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(clockin) &&
                            !string.IsNullOrEmpty(clockout) && !string.IsNullOrEmpty(workhour))
                        {
                            dr["WorkID"] = workID;
                            dr["Date"] = date;
                            dr["ClockIn"] = clockin;
                            dr["ClockOut"] = clockout;
                            dr["WorkHour"] = workhour;
                            dr["ImportedBy"] = HttpContext.Current.User.Identity.Name;
                            dr["FileName"] = fileName;
                            dr["ImportType"] = 1;
                            dr["Firstname"] = firstname;
                            dr["Lastname"] = lastname;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            ConflictSave(workID, date, clockin, clockout, workhour, HttpContext.Current.User.Identity.Name, fileName, 1, firstname, lastname);
                        }
                    }
                }
            }
            return dt;
        }
        public static string StatConflictCount(string File)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(" SELECT count(WorkID) as count FROM StatutaryLeaves_Conflict where FileName='"+File+"' ", con);
            con.Open();
            var Count = "0";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Count = rdr["count"].ToString();
            }
            return Count;
        }
        public static string HolidayConflictCount()
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(" SELECT count(WorkID) as count FROM hr.Holiday_conflict ", con);
            con.Open();
            var Count = "0";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Count = rdr["count"].ToString();
            }
            return Count;
        }
        public static string ConflictCount(int? ImpType,string FileName)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(" SELECT count(WorkID) as count FROM hr.TimeSheetsImport_conflicts where ImportType = "+ ImpType + " "
                          +"and FileName = '"+FileName+"'", con);
            con.Open();
            var Count = "0";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Count = rdr["count"].ToString();
            }
            return Count;
        }
        public static DataTable ReadHolidaysFile_New(string fileString, string fileName)
        {
            DataTable dt = GetHolidaysImportTable_New();
            //Get Excel Info
            //string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            //SqlConnection con = new SqlConnection(constr);
            //SqlCommand cmd = new SqlCommand("select * from ImpExcelRef where CompanyType='KFC'", con);
            //con.Open();
            //SqlDataReader rdr = cmd.ExecuteReader();
            //var txtworkId = 0;
            //var txtdate = 0;
            //var txtclockin = 0;
            //var txtclockout = 0;
            //var txtfirstName = 0;
            //var txtworkHour = 0;
            //while (rdr.Read())
            //{
            //    txtworkId = Convert.ToInt32(rdr["WorkID"]);
            //    txtdate = Convert.ToInt32(rdr["Date"]);
            //    txtclockin = Convert.ToInt32(rdr["ClockIn"]);
            //    txtclockout = Convert.ToInt32(rdr["ClockOut"]);
            //    txtfirstName = Convert.ToInt32(rdr["FirstName"]);
            //    txtworkHour = Convert.ToInt32(rdr["WorkHour"]);
            //}


            //con.Close();

            //
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    CultureInfo culture = new CultureInfo("en-US");
                    while (csv.Read())
                    {

                        DataRow dr = dt.NewRow();
                        string workID = csv.GetField(0);
                        DateTime HolidayFrom = Convert.ToDateTime(csv.GetField(2));
                        DateTime HolidayTo = Convert.ToDateTime(csv.GetField(3));
                        string Year = csv.GetField(4);
                        double Days = (HolidayTo - HolidayFrom).TotalDays+1;
                        string Name = csv.GetField(5);
                        string FileName = csv.GetField(6);
                        string ImportBy = csv.GetField(7);

                      
                     

                        if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(ImportBy) &&
                            !string.IsNullOrEmpty(Year)  && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(FileName))
                        {
                            dr["WorkID"] =int.Parse(workID);
                            dr["FromDate"] = HolidayFrom;
                            dr["ToDate"] = HolidayTo;
                            dr["IsActive"] = "true";
                            dr["CreatedBy"] = HttpContext.Current.User.Identity.Name;
                            dr["CreatedTime"] = DateTime.Now;
                            dr["ModifiedBy"] = null;
                            dr["ModifiedTime"] = null;
                            dr["Days"] =int.Parse(Days.ToString());
                            dr["Hours"] = Days*7.80;
                            dr["IsClear"] = "false";
                            dr["IsFullPay"] = "false";
                            //dr["ImportBy"] = ImportBy;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            HolidaysConflictSave(workID, HolidayFrom, HolidayTo, "true", HttpContext.Current.User.Identity.Name, DateTime.Now, int.Parse(Days.ToString()), Days * 7.80, "false", "false");
                        }
                    }
                }
            }
            return dt;
        }
        public static void HolidaysConflictSave(string WorkID, DateTime? HolidayFrom, DateTime? HolidayTo, string IsActive,  string username, DateTime? CreateTime, int? Days, double? Hours, string isClear,string isFullPay)
        {

            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into Hr.Holiday_Conflict(WorkID,FromDate,ToDate,IsActive,CreatedBy,CreatedTime,Hours,Days,IsClear,IsFullPay) values('" + WorkID + "','" + HolidayFrom + "','" + HolidayTo + "','" + IsActive + "','" + username + "','" + CreateTime + "'," + Days + ",'" + Hours + "','" + isClear + "','"+ isFullPay + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static DataTable ReadStatutaryFile_New(string fileString, string fileName)
        {
            DataTable dt = GetStatutaryImportTable_New();
            //Get Excel Info
            //string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            //SqlConnection con = new SqlConnection(constr);
            //SqlCommand cmd = new SqlCommand("select * from ImpExcelRef where CompanyType='KFC'", con);
            //con.Open();
            //SqlDataReader rdr = cmd.ExecuteReader();
            //var txtworkId = 0;
            //var txtdate = 0;
            //var txtclockin = 0;
            //var txtclockout = 0;
            //var txtfirstName = 0;
            //var txtworkHour = 0;
            //while (rdr.Read())
            //{
            //    txtworkId = Convert.ToInt32(rdr["WorkID"]);
            //    txtdate = Convert.ToInt32(rdr["Date"]);
            //    txtclockin = Convert.ToInt32(rdr["ClockIn"]);
            //    txtclockout = Convert.ToInt32(rdr["ClockOut"]);
            //    txtfirstName = Convert.ToInt32(rdr["FirstName"]);
            //    txtworkHour = Convert.ToInt32(rdr["WorkHour"]);
            //}


            //con.Close();

            //
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    CultureInfo culture = new CultureInfo("en-US");
                    while (csv.Read())
                    {

                        DataRow dr = dt.NewRow();
                        string workID = csv.GetField(0);
                        string RMFID = csv.GetField(1);
                        DateTime DateFrom = Convert.ToDateTime(csv.GetField(2));
                        DateTime DateTo = Convert.ToDateTime(csv.GetField(3));
                        string LYear = csv.GetField(4);
                        string SSPSMP = csv.GetField(5);
                        string EmpName = csv.GetField(6);
                        string FileName = csv.GetField(7);
                        string CreatedBy = csv.GetField(8);

                        if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(RMFID) &&
                            !string.IsNullOrEmpty(SSPSMP) && !string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(EmpName))
                        {
                            dr["WorkID"] = int.Parse(workID);
                            dr["RMFID"] = RMFID;
                            dr["FromDate"] = DateFrom;
                            dr["ToDate"] = DateTo;
                            dr["LeaveYear"] =int.Parse(LYear);
                            dr["SSPSMP"] = SSPSMP;
                            dr["EmpName"] = EmpName;
                            dr["FileName"] = FileName;
                            dr["CreatedBy"] = HttpContext.Current.User.Identity.Name;
                            dr["CreatedTime"] = DateTime.Now;
                            //dr["ImportBy"] = ImportBy;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            //ConflictSave(workID, RMFID, date, clockin, clockout, workhour, HttpContext.Current.User.Identity.Name, fileName, 2, firstname, lastname);
                        }
                    }
                }
            }
            return dt;
        }


        public static DataTable ReadKFCFile_ForGeneric(string fileString, string fileName)
        {
            DataTable dt = GetTimeSheetImportTable();
            //Get Excel Info
           // string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("select * from ImpExcelRef where CompanyType='Generic'", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            var txtworkId = 0;
            var txtdate = 0;
            var txtclockin = 0;
            var txtclockout = 0;
            var txtfirstName = 0;
            var txtworkHour = 0;
            while (rdr.Read())
            {
                txtworkId = Convert.ToInt32(rdr["WorkID"]);
                txtdate = Convert.ToInt32(rdr["Date"]);
                txtclockin = Convert.ToInt32(rdr["ClockIn"]);
                txtclockout = Convert.ToInt32(rdr["ClockOut"]);
                txtfirstName = Convert.ToInt32(rdr["FirstName"]);
                txtworkHour = Convert.ToInt32(rdr["WorkHour"]);
            }


            con.Close();
            //
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    while (csv.Read())
                    {
                        DataRow dr = dt.NewRow();
                        string workID = csv.GetField(txtworkId);
                        string date = csv.GetField(txtdate);
                        string clockin = csv.GetField(txtclockin);
                        string clockout = csv.GetField(txtclockout);
                        string workhour = csv.GetField(txtworkHour);
                        string firstname = csv.GetField(txtfirstName);
                        string lastname = string.Empty;
                        TimeSpan start, end;
                        DateTime tmpdate;
                        if (TimeSpan.TryParse(clockin, out start) && TimeSpan.TryParse(clockout, out end) && DateTime.TryParse(date, out tmpdate))
                        {
                            if (end > start)
                                workhour = (end.TotalHours - start.TotalHours).ToString();
                            else
                                workhour = (end.TotalHours + 24 - start.TotalHours).ToString();
                            date = tmpdate.ToString("dd-MMM-yyyy");
                        }

                        if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(clockin) &&
                            !string.IsNullOrEmpty(clockout) && !string.IsNullOrEmpty(workhour))
                        {
                            dr["WorkID"] = workID;
                            dr["Date"] = date;
                            dr["ClockIn"] = clockin;
                            dr["ClockOut"] = clockout;
                            dr["WorkHour"] = workhour;
                            dr["ImportedBy"] = HttpContext.Current.User.Identity.Name;
                            dr["FileName"] = fileName;
                            dr["ImportType"] = 2;
                            dr["Firstname"] = firstname;
                            dr["Lastname"] = lastname;
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            ConflictSave(workID, date, clockin, clockout, workhour, HttpContext.Current.User.Identity.Name, fileName,2, firstname, lastname);
                        }
                    }
                }
            }
            return dt;
        }

        public static void ConflictSave(string WorkID, string Date, string clockIn, string clockout, string workhour, string username, string filename, int importty, string firstname, string lastname)
        {
           
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into Hr.TimeSheetsImport_Conflicts(WorkID,Date,ClockIn,ClockOut,FirstName,WorkHour,ImportType,ImportedBy,FileName) values('" + WorkID + "','" + Date + "','" + clockIn + "','" + clockout + "','" + firstname + "','" + workhour + "',"+ importty + ",'"+username+"','"+filename+"')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static DataTable ReadKFCFile(string fileString, string fileName)
        {
            DataTable dt = GetTimeSheetImportTable();
            //int numRows = fileString.Split('\n').Length;
            using (StringReader sr = new StringReader(fileString))
            {
                using (CsvReader csv = new CsvReader(sr))
                {
                    csv.Configuration.HasHeaderRecord = true;
                    while (csv.Read())
                    {
                        DataRow dr = dt.NewRow();
                        string workID = csv.GetField(11);
                        string date = csv.GetField(14);
                        string clockin = csv.GetField(15);
                        string clockout = csv.GetField(16);
                        string workhour = string.Empty;
                        string firstname = csv.GetField(12);
                        string lastname = string.Empty;
                        TimeSpan start, end;
                        DateTime tmpdate;
                        if (TimeSpan.TryParse(clockin, out start) && TimeSpan.TryParse(clockout, out end) && DateTime.TryParse(date, out tmpdate))
                        {
                            if (end > start)
                                workhour = (end.TotalHours - start.TotalHours).ToString();
                            else
                                workhour = (end.TotalHours + 24 - start.TotalHours).ToString();
                            date = tmpdate.ToString("dd-MMM-yyyy");
                        }

                        if (!string.IsNullOrEmpty(workID) && !string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(clockin) &&
                            !string.IsNullOrEmpty(clockout) && !string.IsNullOrEmpty(workhour))
                        {
                            dr["WorkID"] = workID;
                            dr["Date"] = date;
                            dr["ClockIn"] = clockin;
                            dr["ClockOut"] = clockout;
                            dr["WorkHour"] = workhour;
                            dr["ImportedBy"] = HttpContext.Current.User.Identity.Name;
                            dr["FileName"] = fileName;
                            dr["ImportType"] = 1;
                            dr["Firstname"] = firstname;
                            dr["Lastname"] = lastname;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            return dt;
        }
    }
}