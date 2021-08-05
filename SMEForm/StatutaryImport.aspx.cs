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
    public partial class StatutaryImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGV();
            }
        }
        public bool IsExistFile(string FileName)
        {
            bool Exist = false;
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("SELECT WorkID FROM StatutaryLeaves where  FileName = '" + FileName + "'", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            var data = rdr.Read();
            con.Close();
            if (data == true)
            {
                Exist = true;
            }
            return Exist;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!StatutaryFiles.HasFile)
            {
                Master.AppendMessage("Please select a Statutary file to Import!");
                return;
            }
            bool isAllCSV = true;
            foreach (HttpPostedFile file in StatutaryFiles.PostedFiles)
            {
                var extension = file.FileName.Split(".".ToCharArray()).Last();
                if (extension.ToLower() != "csv")
                {
                    Master.AppendMessage(string.Format("Statutary import only accept csv files. {0} is not a CSV file", file.FileName));
                    isAllCSV = false;
                    return;
                }
            }
            if (StatutaryFiles.HasFile)
            {
                var IsExist = IsExistFile(StatutaryFiles.PostedFiles.FirstOrDefault().FileName);
                if (IsExist)
                {
                    Master.AppendMessage("This File Has Already Uploaded. Plz Rename File Name!");
                    return;
                }

            }
            if (!isAllCSV)
                return;
            DataTable tmpTable = CSVParser.GetStatutaryImportTable_New();
            foreach (HttpPostedFile file in StatutaryFiles.PostedFiles)
            {
                var sr = new StreamReader(file.InputStream);
                DataTable dt;
                try
                {
                    dt = CSVParser.ReadStatutaryFile_New(sr.ReadToEnd(), file.FileName);
                    // dt = CSVParser.ReadKFCFile(sr.ReadToEnd(), file.FileName);
                    tmpTable.Merge(dt);

                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            var WorkID = Convert.ToInt32(item["WorkID"]);
                            var FromDate = item["FromDate"].ToString();
                            var ToDate = item["ToDate"].ToString();
                            var RMFID =Convert.ToInt64(item["RMFID"].ToString());
                            var LeaveYear =Convert.ToInt32(item["LeaveYear"].ToString());
                            var SSPSMP = item["SSPSMP"].ToString();
                            var EmpName = item["EmpName"].ToString();
                            var FileName = item["FileName"].ToString();
                            var CreatedBy = item["CreatedBy"].ToString();
                            if (WorkID > 0 && FromDate != null && ToDate != null && RMFID>0 && SSPSMP!="" && EmpName!="")
                            {
                                DeleteExistWorkId(WorkID, FromDate, ToDate,RMFID,LeaveYear,SSPSMP,EmpName,FileName,CreatedBy);
                            }

                        }
                    }

                }
                catch
                {
                    throw new ApplicationException(string.Format("File {0} is corrupted, please fix before continue.", file.FileName));
                }
            }
            try
            {
                //BoStatutary da = new BoStatutary();
                //da.BulkCopyResultsStatutaryImport(tmpTable);
                //da.ValidateImport();

            }
            catch (Exception ex)
            {
                Master.AppendMessage(string.Format("Fail to connect to SQL server: {0}", ex.Message));
            }
            var FileNames = StatutaryFiles.PostedFiles.FirstOrDefault().FileName;
            var import = DbContext.Current().StatutaryLeaves.ToList();
            var ImpConflict = CSVParser.StatConflictCount(FileNames);
            if (import.Count > 0)
            {
                Master.AppendMessage(string.Format(@"" + import.Count + " Record Has Imported Successfully. And " + ImpConflict + " Conflicted Record."));
                BindGV();
              
            }
        }

        
        public void DeleteExistWorkId(int? WorkID,string FromDate,string ToDate,Int64 RMFID,int LeaveYear,string SSPSMP,string EmpName,string FileName,string CreatedBy)
        {
            string str = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("Sp_WorkIDExistStatutary", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@RMFID", RMFID);
            cmd.Parameters.AddWithValue("@WorkID", WorkID);
            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@ToDate", ToDate);
            cmd.Parameters.AddWithValue("@LeaveYear", LeaveYear);
            cmd.Parameters.AddWithValue("@SSPSMP", SSPSMP);
            cmd.Parameters.AddWithValue("@EmpName", EmpName);
            cmd.Parameters.AddWithValue("@FileName", FileName);
            cmd.Parameters.AddWithValue("@Createby", CreatedBy);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        void BindGV()
        {
            var imports = DbContext.Current().StatutaryLeaves.ToList();
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
            SqlCommand cmd = new SqlCommand("delete from StatutaryLeaves where ID='" + id + "' ", cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        /// <summary>
        /// Later
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GVImports_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = (int)e.Keys[0];
            var txRMFID = GVImports.Rows[e.RowIndex].FindControl("txtRmfID") as TextBox;
            var txtWorkID = GVImports.Rows[e.RowIndex].FindControl("txtWorkID") as TextBox;
            var FromDate = GVImports.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
            var ToDate = GVImports.Rows[e.RowIndex].FindControl("txtDate2") as TextBox;
            var txtLeaveYear = GVImports.Rows[e.RowIndex].FindControl("txtLeaveYear") as TextBox;
            var txtSSPSMP = GVImports.Rows[e.RowIndex].FindControl("txtSSPSMP") as TextBox;
            var txtEmpName = GVImports.Rows[e.RowIndex].FindControl("txtEmpName") as TextBox;
            var txtFileName = GVImports.Rows[e.RowIndex].FindControl("txtFileName") as TextBox;

            try
            {
                var RMFID = Int64.Parse(txRMFID.Text);
                var WorkID = int.Parse(txtWorkID.Text);
                var FromDateC = DateTime.ParseExact(FromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                var ToDateC = DateTime.ParseExact(ToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                var LeaveYear = int.Parse(txtLeaveYear.Text);
                var SSPSMP = txtSSPSMP.Text;
                var EmpName = txtEmpName.Text;
                var FileName = txtFileName.Text;
                
                if (WorkID > 0 && RMFID >0 && FromDateC != null && ToDateC != null && LeaveYear > 0 && FileName !=null)
                {
                    UpdateHolidaysRecord(ID, RMFID, WorkID, FromDateC, ToDateC, LeaveYear, SSPSMP, EmpName, FileName);
                }

            }
            catch (Exception ex)
            {
                Master.AppendMessage(ex.Message);
            }
            GVImports.EditIndex = -1;
            BindGV();
        }
        public void UpdateHolidaysRecord(int? id,Int64? RMFID, int? workID, string FromDate, string ToDate, int? LeaveYear, string SSPSMP,string EmpName,string FileName)
        {
            string str = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand("update StatutaryLeaves set WorkID='" + workID + "' , FromDate='" + FromDate + "' , ToDate='" + ToDate + "' , LeaveYear='" + LeaveYear + "' , SSPSMP='" + SSPSMP + "'  , EmpName='" + EmpName + "'  , FileName='" + FileName + "' where ID='" + id + "' ", cn);
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