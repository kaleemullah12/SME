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
    public partial class ClearWeeks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVCompany.DataSource = DbContext.Current().Companies.OrderBy(c => c.ID);
                GVCompany.DataBind();
            }
        }
        protected void GVCompany_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var c = (Company)e.Row.DataItem;
                DropDownList ddlWeek = (DropDownList)e.Row.FindControl("ddlWeek");                
                if (ddlWeek != null)
                    if (c.BrandID == 2)
                        initCostaWeek(ddlWeek);
                    else
                        initWeek(ddlWeek);
            }
        }
        private void initWeek(DropDownList ddlWeek)
        {
            if (!SessionManager.CheckSession(SessionKey.SessionCurrentWeeks))
            {
                var stopDay = DateTime.Now.AddDays(-35);
                var weeks = (from w in DbContext.Current().WorkWeeks
                             where w.EndDate > stopDay && w.StartDate < DateTime.Now && w.Type == 7
                             select w).ToList();
                SessionManager.SetSession<List<WorkWeek>>(SessionKey.SessionCurrentWeeks, weeks);
            }
            var workweeks = SessionManager.GetSession<List<WorkWeek>>(SessionKey.SessionCurrentWeeks);
            ddlWeek.Items.Clear();
            foreach (var week in workweeks)
                ddlWeek.Items.Add(new ListItem(string.Format("Year:{0}-Week:{1}. ({2}-{3})", week.Year.ToString(), week.Week.ToString(), week.StartDate.ToShortDateString(), week.EndDate.ToShortDateString()), week.ID.ToString()));
            ddlWeek.SelectedIndex = workweeks.Count - 1;
        }
        private void initCostaWeek(DropDownList ddlWeek)
        {
            var stopDay = DateTime.Now.AddDays(-35);
            var weeks = (from w in DbContext.Current().WorkWeeks
                         where w.EndDate > stopDay && w.StartDate < DateTime.Now && w.Type == 5
                         select w).ToList();
            ddlWeek.Items.Clear();
            foreach (var week in weeks)
                ddlWeek.Items.Add(new ListItem(string.Format("Year:{0}-Week:{1}. ({2}-{3})", week.Year.ToString(), week.Week.ToString(), week.StartDate.ToShortDateString(), week.EndDate.ToShortDateString()), week.ID.ToString()));
            ddlWeek.SelectedIndex = weeks.Count - 1;
        }

        protected void GVCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Clear")
            {
                var row = (e.CommandSource as Button).NamingContainer as GridViewRow;
                int companyID = int.Parse(e.CommandArgument.ToString());
                DropDownList ddlWeek = row.FindControl("ddlWeek") as DropDownList;
                int weekID = int.Parse(ddlWeek.SelectedValue);
                BoWorkTime da = new BoWorkTime();
                da.ClearByWeekByCompany(weekID, companyID);
                Master.AppendMessage(string.Format("All existing timesheets for company {0} and {1} has been marked as clear and will not show in export anymore!", companyID.ToString(), ddlWeek.SelectedItem.Text));
            }
        }
    }
}