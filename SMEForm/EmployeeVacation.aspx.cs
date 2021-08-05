using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;
using SMEForm.Context;
namespace SMEForm
{
    public partial class EmployeeVacation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGV();
                lkDetail.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeDetails.aspx?workID={0}", Request["workID"]));
            }
        }
        private void bindGV()
        {
            int workid = int.Parse(Request["workID"]);
            BoVacation va = new BoVacation();
            gvVacations.DataSource = va.GetEmployeeVacations(workid);
            gvVacations.DataBind();
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            DateTime from, to;
            from = DateTime.Parse(txtFromDate.Text);
            to = DateTime.Parse(txtToDate.Text);
            int workid = int.Parse(Request["workID"]);
            Vacation newVacation = new Vacation();
            newVacation.FromDate = from;
            newVacation.ToDate = to;
            newVacation.WorkID = workid;
            BoVacation va = new BoVacation();
            va.UpsertVacation(newVacation);
            bindGV();
        }
        protected void gvHolidays_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvVacations.EditIndex = e.NewEditIndex;
            bindGV();
        }
        protected void gvHolidays_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvVacations.EditIndex = -1;
            bindGV();
        }
        protected void gvHolidays_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int vacationID = (int)e.Keys[0];
            var txtFrom = gvVacations.Rows[e.RowIndex].FindControl("txtFromDate") as TextBox;
            var txtTo = gvVacations.Rows[e.RowIndex].FindControl("txtToDate") as TextBox;
            DateTime from, to;
            from = DateTime.Parse(txtFrom.Text);
            to = DateTime.Parse(txtTo.Text);
            Vacation vacation = new Vacation();
            vacation.ID = vacationID;
            vacation.FromDate = from;
            vacation.ToDate = to;
            BoVacation va = new BoVacation();
            va.UpsertVacation(vacation);
            gvVacations.EditIndex = -1;
            bindGV();
        }
        protected void gvHolidays_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int vacationID = (int)e.Keys[0];
            BoVacation va = new BoVacation();
            va.DeleteVacation(vacationID);
            bindGV();
        }
    }
}