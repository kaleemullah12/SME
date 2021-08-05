using SMEForm.Bo;
using SMEForm.Context;
using SMEForm.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEForm
{
    public partial class GenericConflict : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int oldcount = DbContext.Current().TimeSheetsImport_Conflicts.Where(t => t.ImportType == 2).Count();
                if (oldcount > 0)
                {
                    try
                    {
                        BoTimeSheet da = new BoTimeSheet();
                        da.ValidateImport();
                    }
                    catch (Exception ex)
                    {
                        Master.AppendMessage(string.Format("Fail to connect to SQL server: {0}", ex.Message));
                    }
                    var import = DbContext.Current().TimeSheetsImport_Conflicts.Where(t => t.ImportType == 2).ToList();
                    SessionManager.SetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport, import);
                    BindGV();
                    int newcount = oldcount - import.Count;
                   // Master.AppendMessage(string.Format("{0} saved imports detected, {1} of them imported successfully.", oldcount.ToString(), newcount.ToString()));
                }
            }
        }
        public bool IsExistFile(string FileName)
        {
            bool Exist = false;
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("SELECT WorkID FROM hr.TimeSheetsImport where ImportType = 2 and FileName = '" + FileName + "'", con);
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
            if (!fuKFCFile.HasFile)
            {
                Master.AppendMessage("Please select a Generic timesheet file to Import!");
                return;
            }
            if (fuKFCFile.HasFile)
            {
                var IsExist = IsExistFile(fuKFCFile.PostedFiles.FirstOrDefault().FileName);
                if (IsExist)
                {
                    Master.AppendMessage("This File Has Already Uploaded. Plz Rename File Name!");
                    return;
                }

            }
            bool isAllCSV = true;
            foreach (HttpPostedFile file in fuKFCFile.PostedFiles)
            {
                var extension = file.FileName.Split(".".ToCharArray()).Last();
                if (extension.ToLower() != "csv")
                {
                    Master.AppendMessage(string.Format("Generic import only accept csv files. {0} is not a CSV file", file.FileName));
                    isAllCSV = false;
                    return;
                }
            }
            if (!isAllCSV)
                return;
            DataTable tmpTable = CSVParser.GetTimeSheetImportTable_New();
            foreach (HttpPostedFile file in fuKFCFile.PostedFiles)
            {
                var sr = new StreamReader(file.InputStream);
                DataTable dt;
                try
                {
                    dt = CSVParser.ReadKFCFile_ForGeneric(sr.ReadToEnd(), file.FileName);
                    tmpTable.Merge(dt);
                }
                catch
                {
                    throw new ApplicationException(string.Format("File {0} is corrupted, please fix before continue.", file.FileName));
                }
            }
            try
            {
                // var dd = tmpTable.Rows.Count;
                BoTimeSheet da = new BoTimeSheet();
                da.BulkCopyResultsTimeSheetImport(tmpTable);
                da.ValidateImport();
            }
            catch (Exception ex)
            {
                Master.AppendMessage(string.Format("Fail to connect to SQL server: {0}", ex.Message));
            }
            var FileName = fuKFCFile.PostedFiles.FirstOrDefault().FileName;
            var import = DbContext.Current().TimeSheetsImport_Conflicts.Where(t => t.ImportType == 2 && t.FileName == FileName).ToList();
            var ImpConflict = CSVParser.ConflictCount(2, FileName);
            SessionManager.SetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport, import);
            if (import.Count > 0)
            {
                Master.AppendMessage(string.Format(@"" + import.Count + " Record Has Imported Successfully. And " + ImpConflict + " Conflicted Record."));
                BindGV();
            }
            else
                Master.AppendMessage("All import completed successfully.");

        }

        void BindGV()
        {
            var imports = SessionManager.GetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport);
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
            var imports = SessionManager.GetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport);
            var import = imports.Where(i => i.ID == ID).Select(i => i).FirstOrDefault();
            BoTimesheetImport.Delete(import.ID);
            imports.RemoveAt(e.RowIndex);
            SessionManager.SetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport, imports);
            BindGV();
        }
        protected void GVImports_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = (int)e.Keys[0];
            var txtWorkID = GVImports.Rows[e.RowIndex].FindControl("txtWorkID") as TextBox;
            var txtDate = GVImports.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
            var txtStart = GVImports.Rows[e.RowIndex].FindControl("txtStart") as TextBox;
            var txtEnd = GVImports.Rows[e.RowIndex].FindControl("txtEnd") as TextBox;
            var txtHour = GVImports.Rows[e.RowIndex].FindControl("txtPayHour") as TextBox;
            var imports = SessionManager.GetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport);
            var import = imports.Where(i => i.ID == ID).Select(i => i).FirstOrDefault();
            try
            {
                import.WorkID = txtWorkID.Text;
                import.Date = txtDate.Text;
                import.ClockIn = txtStart.Text;
                import.ClockOut = txtEnd.Text;

                TimeSpan start, end;
                DateTime tmpdate;
                if (TimeSpan.TryParse(import.ClockIn, out start) && TimeSpan.TryParse(import.ClockOut, out end) && DateTime.TryParse(import.Date, out tmpdate))
                {
                    if (end > start)
                        txtHour.Text = (end.TotalHours - start.TotalHours).ToString("0.00");
                    else
                        txtHour.Text = (end.TotalHours + 24 - start.TotalHours).ToString("0.00");
                }

                import.WorkHour = txtHour.Text;
                BoTimesheetImport_Conflict ip = new BoTimesheetImport_Conflict(import);
                if (!string.IsNullOrEmpty(ip.Error))
                {
                    Master.AppendMessage(ip.Error);
                }
                else
                {
                    imports.Remove(import);
                    SessionManager.SetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport, imports);
                }
            }
            catch (Exception ex)
            {
                Master.AppendMessage(ex.Message);
            }
            GVImports.EditIndex = -1;
            BindGV();
        }
        protected void GVImports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var import = e.Row.DataItem as TimeSheetsImport_Conflicts;
                if (import.ValidationBitString > 0)
                {
                    e.Row.Attributes.Add("title", BoTimesheetImport_Conflict.GetError(import.ValidationBitString));
                }
            }
        }
        protected void GVImports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVImports.PageIndex = e.NewPageIndex;
            BindGV();
        }
        protected void GVImports_Sorting(object sender, GridViewSortEventArgs e)
        {
            var imports = SessionManager.GetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport);
            IOrderedEnumerable<TimeSheetsImport_Conflicts> emps = imports.OrderBy(i => i.WorkID);
            switch (e.SortExpression)
            {
                case "WorkID":
                    emps = imports.OrderBy(i => i.WorkID);
                    break;
                case "Date":
                    emps = imports.OrderBy(i => i.Date);
                    break;
                case "ClockIn":
                    emps = imports.OrderBy(i => i.ClockIn);
                    break;
                case "ClockOut":
                    emps = imports.OrderBy(i => i.ClockOut);
                    break;
                case "ImportedBy":
                    emps = imports.OrderBy(i => i.ImportedBy);
                    break;
                case "FileName":
                    emps = imports.OrderBy(i => i.FileName);
                    break;
                default:
                    break;
            }
            SessionManager.SetSession<List<TimeSheetsImport_Conflicts>>(SessionKey.SessionKFCImport, emps.ToList());
            BindGV();
        }
    }
}