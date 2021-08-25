using SMEForm.Bo;
using SMEForm.Context;
using SMEForm.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEForm
{
    public partial class HolidaysImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGV();
            }
        }
       
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!HolidaysFile.HasFile)
            {
                Master.AppendMessage("Please select a Holidays file to Import!");
                return;
            }
         
            bool isAllCSV = true;
            foreach (HttpPostedFile file in HolidaysFile.PostedFiles)
            {
                var extension = file.FileName.Split(".".ToCharArray()).Last();
                if (extension.ToLower() != "csv")
                {
                    Master.AppendMessage(string.Format("Holidays import only accept csv files. {0} is not a CSV file", file.FileName));
                    isAllCSV = false;
                    return;
                }
            }
            if (!isAllCSV)
                return;
            DataTable tmpTable = CSVParser.GetHolidaysImportTable_New();
            foreach (HttpPostedFile file in HolidaysFile.PostedFiles)
            {
                var sr = new StreamReader(file.InputStream);
                DataTable dt;
                try
                {
                    dt = CSVParser.ReadHolidaysFile_New(sr.ReadToEnd(), file.FileName);
                    // dt = CSVParser.ReadKFCFile(sr.ReadToEnd(), file.FileName);
                    tmpTable.Merge(dt);

                    if (dt!=null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            var WorkID =Convert.ToInt32(item["WorkID"]);
                            var FromDate = item["FromDate"].ToString();
                            var ToDate = item["ToDate"].ToString();
                            if (WorkID>0 && FromDate!=null && ToDate!=null)
                            {
                                DeleteExistWorkId(WorkID, FromDate, ToDate);
                            }
                         
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException(string.Format("File {0} is corrupted, please fix before continue.", file.FileName));
                }
            }
            try
            {
               // BoHolidays da = new BoHolidays();
               // da.BulkCopyResultsHolidaysImport(tmpTable);
               // da.ValidateImport();

            }
            catch (Exception ex)
            {
                Master.AppendMessage(string.Format("Fail to connect to SQL server: {0}", ex.Message));
            }
            var import = DbContext.Current().Holidays.ToList();
            var ImpConflict = CSVParser.HolidayConflictCount();
            SessionManager.SetSession<List<Holiday>>(SessionKey.SessionCurrentHolidays, import);
            if (import.Count > 0)
            {
                // Master.AppendMessage(string.Format(@"{0} rows can not be imported becasue of conflicts.", import.Count.ToString()));
                Master.AppendMessage(string.Format(@"" + import.Count + " Record Has Imported Successfully. And " + ImpConflict + " Conflicted Record."));

                BindGV();
               
            }
           
        }
        public void DeleteExistWorkId(int workID,string FromDate,string ToDate)
        {
            string str = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Sp_WorkIDExistHoliday", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@WorkID", workID);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@Createby", "");
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        void BindGV()
        {
            var imports = DbContext.Current().Holidays.ToList();
            GVImports.DataSource = imports;
            GVImports.DataBind();
        }
        protected void GVImports_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVImports.EditIndex = e.NewEditIndex;
            BindGV();
        }
        protected void GVImports_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GVImports.EditIndex = -1;
            BindGV();
        }
        protected void GVImports_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)e.Keys[0];

            DeleteHoliday(ID);
            BindGV();
        }
        public void DeleteHoliday(int? id)
        {
            string str = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("delete from HR.Holiday where ID='" + id + "' ", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        protected void GVImports_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = (int)e.Keys[0];
            var txtWorkID = GVImports.Rows[e.RowIndex].FindControl("txtWorkID") as TextBox;
            var FromDate = GVImports.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
            var ToDate = GVImports.Rows[e.RowIndex].FindControl("txtDate2") as TextBox;
            var txtHours = GVImports.Rows[e.RowIndex].FindControl("txtHours") as TextBox;
            var txtDays = GVImports.Rows[e.RowIndex].FindControl("txtdays") as TextBox;
          
            try
            {
                var WorkID =int.Parse(txtWorkID.Text);
                var FromDateC = DateTime.ParseExact(FromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                //Convert.ToDateTime(FromDate.Text).ToString("MM/dd/yyyy");
                var ToDateC = DateTime.ParseExact(ToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                var DaysC =int.Parse(txtDays.Text);
                var HoursC =decimal.Parse(txtHours.Text);

               
               
                if (WorkID>0 && FromDateC!=null && ToDateC!=null && DaysC>0 && HoursC>0)
                {
                    UpdateHolidaysRecord(ID,WorkID,FromDateC,ToDateC,DaysC,HoursC);
                }
               
            }
            catch (Exception ex)
            {
                Master.AppendMessage(ex.Message);
            }
            GVImports.EditIndex = -1;
            BindGV();
        }
        public void UpdateHolidaysRecord(int? id,int? workID,string FromDate,string ToDate,int? Days,decimal? Hours)
        {
            string str = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("update HR.Holiday set WorkID='"+workID+ "' , FromDate='"+ FromDate + "' , ToDate='"+ToDate+ "' , Hours='"+Hours+ "' , Days='"+Days+"' where ID='"+id+"' ", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        protected void GVImports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var import = e.Row.DataItem as Holiday;
                //if (import.va > 0)
                //{
                //    e.Row.Attributes.Add("title", Holiday.GetError(import.ValidationBitString));
                //}
            }
        }
        protected void GVImports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVImports.PageIndex = e.NewPageIndex;
            BindGV();
        }
        protected void GVImports_Sorting(object sender, GridViewSortEventArgs e)
        {
            var imports = DbContext.Current().Holidays.ToList();
            IOrderedEnumerable<Holiday> HolidaysList = imports.OrderBy(i => i.WorkID);
            switch (e.SortExpression)
            {
                case "WorkID":
                    HolidaysList = imports.OrderBy(i => i.WorkID);
                    break;
                case "FromDate":
                    HolidaysList = imports.OrderBy(i => i.FromDate);
                    break;
                case "ToDate":
                    HolidaysList = imports.OrderBy(i => i.ToDate);
                    break;
                case "Hours":
                    HolidaysList = imports.OrderBy(i => i.Hours);
                    break;
                case "Days":
                    HolidaysList = imports.OrderBy(i => i.Days);
                    break;
                default:
                    break;
            }
           BindGV();
        }

        protected void GVImports_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}