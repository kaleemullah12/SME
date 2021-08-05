using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;
using SMEForm.Context;
using SMEForm.Helper;

namespace SMEForm
{
    public partial class EmployeeWorktimes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGV(); 
            }
            int workID = int.Parse(Request["workID"]);
            DbContext.Current().UpdateHolidayForWorkID(workID);
            DbContext.Current().SaveChanges();
        }
        void BindGV()
        {
            int workID = int.Parse(Request["workID"]);
            DateTime latestBackDate = DateTime.Today.AddDays(-36);
            var workTimes = (from wt in DbContext.Current().WorkTimes
                             where wt.WorkID == workID && wt.Date > latestBackDate
                             orderby wt.Date descending
                             select wt).ToList();
            gvWorkTime.DataSource = workTimes;
            gvWorkTime.DataBind();
            Employee emp = (from em in DbContext.Current().Employees
                            where em.WorkID == workID
                            select em).First();
            lkDetail.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeList.aspx?companyID={0}&shopID={1}", emp.Shop.CompanyID.ToString(), emp.ShopID.ToString()));
        }
        protected void gvWorkTime_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvWorkTime.EditIndex = e.NewEditIndex;
            BindGV();
        }
        protected void gvWorkTime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvWorkTime.EditIndex = -1;
            BindGV();
        }
        protected void gvWorkTime_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)e.Keys[0];
            WorkTime workTime = (from wt in DbContext.Current().WorkTimes
                                 where wt.ID == ID
                                 select wt).First();
            DbContext.Current().WorkTimes.DeleteObject(workTime);
            DbContext.Current().SaveChanges();
            BindGV();
        }
        protected void gvWorkTime_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = (int)e.Keys[0];
            var txtDate = gvWorkTime.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
            var txtStart = gvWorkTime.Rows[e.RowIndex].FindControl("txtStart") as TextBox;
            var txtEnd = gvWorkTime.Rows[e.RowIndex].FindControl("txtEnd") as TextBox;
            WorkTime workTime = (from wt in DbContext.Current().WorkTimes
                                 where wt.ID == ID
                                 select wt).First();
            double workhour = 0;
            try
            {
                TimeSpan start, end;
                DateTime date = DateTime.Today;
                DateTime StartDate = DateTime.Today;
                DateTime EndDate = DateTime.Today;
                if (TimeSpan.TryParse(txtStart.Text, out start) && TimeSpan.TryParse(txtEnd.Text, out end) && DateTime.TryParse(txtDate.Text, out date))
                {
                    if (end > start)
                    {
                        StartDate = date.Add(start);
                        EndDate = date.Add(end);
                        workhour = (end.TotalHours - start.TotalHours);
                    }
                    else
                    {
                        StartDate = date.Add(start);
                        EndDate = date.AddDays(1).Add(end);
                        workhour = (end.TotalHours + 24 - start.TotalHours);
                    }
                }
                else
                {
                    throw new ApplicationException("Inputs are invalid.");
                }
                int workID = int.Parse(Request["workID"]);
                Employee emp = (from em in DbContext.Current().Employees
                                where em.WorkID == workID
                                select em).First();
                int weekType = emp.Shop.BrandID == 2 ? 5 : 7;

                WorkWeek ww = (from w in DbContext.Current().WorkWeeks
                               where w.StartDate <= date && w.EndDate >= date && w.Type == (byte)weekType
                               select w).First();

                var ClockOverlaps = (from c in DbContext.Current().WorkTimes
                                     where c.WorkID == workID && c.Date == date && c.ID != ID
                                     where (c.ClockIn >= StartDate && c.ClockIn < EndDate) || (c.ClockOut <= EndDate && c.ClockOut > StartDate) || (c.ClockIn <= StartDate && c.ClockOut >= EndDate)
                                     select c).ToList();
                if (ClockOverlaps.Count > 0)
                    throw new ApplicationException("There are existing timesheet overlaps, please try a different time.");

                workTime.Date = date;
                workTime.ClockIn = StartDate;
                workTime.ClockOut = EndDate;
                workTime.WeekID = ww.ID;
                workTime.WorkHour = (decimal)workhour;
                workTime.ModifiedTime = DateTime.Now;
                workTime.ModifiedBy = HttpContext.Current.User.Identity.Name;
                DbContext.Current().SaveChanges();
            }
            catch (Exception ex)
            {
                Master.AppendMessage(ex.Message);
            }
            gvWorkTime.EditIndex = -1;
            BindGV();
        }
        protected void gvWorkTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvWorkTime_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWorkTime.PageIndex = e.NewPageIndex;
            BindGV();
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            double workhour = 0;
            try
            {
                TimeSpan start, end;
                DateTime date = DateTime.Today;
                DateTime StartDate = DateTime.Today;
                DateTime EndDate = DateTime.Today;
                if (TimeSpan.TryParse(txtClockin.Text, out start) && TimeSpan.TryParse(txtClockout.Text, out end) && DateTime.TryParse(txtDate.Text, out date))
                {
                    if (end > start)
                    {
                        StartDate = date.Add(start);
                        EndDate = date.Add(end);
                        workhour = (end.TotalHours - start.TotalHours);
                    }
                    else
                    {
                        StartDate = date.Add(start);
                        EndDate = date.AddDays(1).Add(end);
                        workhour = (end.TotalHours + 24 - start.TotalHours);
                    }
                }
                else
                {
                    throw new ApplicationException("Inputs are invalid.");
                }
                int workID = int.Parse(Request["workID"]);

                var ClockOverlaps = (from c in DbContext.Current().WorkTimes
                                     where c.WorkID == workID && c.Date == date
                                     where (c.ClockIn >= StartDate && c.ClockIn < EndDate) || (c.ClockOut <= EndDate && c.ClockOut > StartDate) || (c.ClockIn <= StartDate && c.ClockOut >= EndDate)
                                     select c).ToList();
                if (ClockOverlaps.Count > 0)
                    throw new ApplicationException("There are existing timesheet overlaps, please try a different time.");

                Employee emp = (from em in DbContext.Current().Employees
                                where em.WorkID == workID
                                select em).First();
                int weekType = emp.Shop.BrandID == 2 ? 5 : 7;

                WorkWeek ww = (from w in DbContext.Current().WorkWeeks
                               where w.StartDate <= date && w.EndDate >= date && w.Type == (byte)weekType
                               select w).First();

                WorkTime workTime = new WorkTime();
                workTime.Date = date;
                workTime.WorkID = emp.WorkID;
                workTime.WeekID = ww.ID;
                workTime.ShopID = emp.ShopID;
                workTime.ClockIn = StartDate;
                workTime.ClockOut = EndDate;
                workTime.WorkHour = (decimal)workhour;
                workTime.CreatedTime = DateTime.Now;
                workTime.CreatedBy = HttpContext.Current.User.Identity.Name;
                workTime.IsClear = false;
                DbContext.Current().WorkTimes.AddObject(workTime);
                DbContext.Current().SaveChanges();
                BindGV();
            }
            catch (Exception ex)
            {
                Master.AppendMessage(ex.Message);
            }

        }
    }
}