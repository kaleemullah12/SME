using SMEForm.Bo;
using SMEForm.Context;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEForm
{
    public partial class EmployeeStatutary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGV();
                int workid = int.Parse(Request["workID"]);
            }
            int workID = int.Parse(Request["workID"]);
            DbContext.Current().UpdateHolidayForWorkID(workID);
            DbContext.Current().SaveChanges();
        }
        protected override void OnPreRender(EventArgs e)
        {
            InitHolidayDisplay();
            base.OnPreRender(e);
        }
        private void InitHolidayDisplay()
        {
            int workid = int.Parse(Request["workID"]);
            var emp = DbContext.Current().Employees.Where(e => e.WorkID == workid).First();
            lbHolidayAccrual.Text = emp.HolidayEarn.ToString();
            lblHolidayBooked.Text = emp.HolidaySpend.ToString();
            if (emp.HolidaySpend > emp.HolidayEarn)
            {
                lblHolidayBooked.ForeColor = System.Drawing.Color.Red;
                lblHolidayBooked.ToolTip = "This employee has booked more holiday than Accrual!";
            }
            else
            {
                lblHolidayBooked.ForeColor = System.Drawing.Color.Black;
                lblHolidayBooked.ToolTip = "";
            }
            lkDetail.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeList.aspx?companyID={0}&shopID={1}", emp.Shop.CompanyID.ToString(), emp.ShopID.ToString()));
        }
        private void bindGV()
        {
            int workid = int.Parse(Request["workID"]);
            BoStatutaryL hol = new BoStatutaryL();
            gvHolidays.DataSource = hol.GetEmployeeStatList(workid);
            gvHolidays.DataBind();
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            //if (!ValidateBooking(txtFromDate.Text, txtToDate.Text))
            //{
            //    Master.AppendMessage("There is not enough Statutary for this booking!");
            //    return;
            //}
            DateTime from, to;
            from = DateTime.ParseExact(txtFromDate.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);// DateTime.Parse(txtFromDate.Text);
           // txtStartDate.Text = emp.StartDate == null ? "" : ();
           // from = txtFromDate.Text.ToString("dd/MM/yyyy");
            to = DateTime.ParseExact(txtToDate.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);// DateTime.Parse(txtToDate.Text);
            int workid = int.Parse(Request["workID"]);
            StatutaryLeaf newHoliday = new StatutaryLeaf();
            newHoliday.FromDate = from;
            newHoliday.ToDate = to;
            newHoliday.WorkID = workid;
            BoStatutaryL hol = new BoStatutaryL();
            hol.UpsertStatutary(newHoliday);
            bindGV();
        }
        private bool ValidateBooking(string fromDate, string toDate)
        {
            bool valid = false;
            //int workid = int.Parse(Request["workID"]);
            //StatutaryLeaf e = (from emp in DbContext.Current().StatutaryLeaves
            //              where emp.WorkID == workid
            //              select emp).FirstOrDefault();
            //int daycount = 0;
            //DateTime from, to;
            //from = Convert.ToDateTime(fromDate);
            //to =Convert.ToDateTime(toDate);
            //while (from <= to)
            //{
            //    if (from.DayOfWeek != DayOfWeek.Sunday || (from.DayOfWeek == DayOfWeek.Saturday))
            //        daycount++;
            //    from = from.AddDays(1);
            //}
            //decimal? bookhours = 0;
            //if (e.PayTypeID == 1)
            //    bookhours = (decimal)(daycount * 7.5);
            //else
            //    bookhours = (decimal)(daycount * 7.8);
            //if (e.HolidayEarn - e.HolidaySpend >= bookhours)
            //    valid = true;

            return valid;
        }
        protected void gvHolidays_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvHolidays.EditIndex = e.NewEditIndex;
            bindGV();
        }
        protected void gvHolidays_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvHolidays.EditIndex = -1;
            bindGV();
        }
        protected void gvHolidays_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int holidayID = (int)e.Keys[0];
            var txtFrom = gvHolidays.Rows[e.RowIndex].FindControl("txtFromDate") as TextBox;
            var txtTo = gvHolidays.Rows[e.RowIndex].FindControl("txtToDate") as TextBox;
            //if (!ValidateBooking(txtFrom.Text, txtTo.Text))
            //{
            //    Master.AppendMessage("There is not enough Statutary for this booking!");
            //    return;
            //}
            DateTime from, to;
            from = DateTime.ParseExact(txtFrom.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            to = DateTime.ParseExact(txtTo.Text, "dd/mm/yyyy", CultureInfo.InvariantCulture);

            StatutaryLeaf holiday = new StatutaryLeaf();
            holiday.ID = holidayID;
            holiday.FromDate = from;
            holiday.ToDate = to;
            BoStatutaryL hol = new BoStatutaryL();
            hol.UpsertStatutary(holiday);
            gvHolidays.EditIndex = -1;
            bindGV();
        }
        protected void gvHolidays_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int holidayID = (int)e.Keys[0];
            BoStatutaryL hol = new BoStatutaryL();
            hol.DeleteStatutary(holidayID);
            bindGV();
        }
    }
}