using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;
using SMEForm.Context;
using SMEForm.Helper;

namespace SMEForm
{
    public partial class PizzaExport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                init4Week();
                initPayElementGroup();
            }
        }

        protected void btnRunExportAll_Click(object sender, EventArgs e)
        {
            BoSagePayroll payroll = new BoSagePayroll();
            int weekid = int.Parse(ddlWeek.SelectedValue);
            int fromWeekid = weekid - 3;
            var week = (from w in DbContext.Current().WorkWeeks
                        where w.ID == weekid
                        select w).First();
            var fromWeek = (from w in DbContext.Current().WorkWeeks
                            where w.ID == fromWeekid
                            select w).First();
            string folderPath = string.Format("{0}\\Exports\\Pizza({1})Week-{2}-to-{3}", Util.ApplicationPath, week.Year.ToString(), fromWeek.Week.ToString(), week.Week.ToString());
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            liExportSummary.Text = string.Format(@"<p>Following files have been created in Costa{0}-{1} subfolder:</p>", week.Year.ToString(), week.Week.ToString());
            foreach (var company in DbContext.Current().Companies.Where(c => c.BrandID == 3).ToList())
            {
                DataTable dt = payroll.GetByCompanyByInterval(fromWeekid, weekid, company.ID);
                if (dt.Rows.Count > 0)
                {
                    string filePath = string.Format("{0}\\Company({1})Week-{2}-to-{3}.csv", folderPath, company.ID.ToString(), fromWeek.Week.ToString(), week.Week.ToString());
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                    File.WriteAllText(filePath, Util.tblToCsv(dt));
                    liExportSummary.Text += string.Format(@"<p>{2} : Company({0})Week-{1}-to-{2}.csv</p>", company.ID.ToString(), fromWeek.Week.ToString(), week.Week.ToString());
                }
            }
        }
        private void initWeek()
        {
            var stopDay = DateTime.Now.AddDays(-35);
            var weeks = (from w in DbContext.Current().WorkWeeks
                         where w.EndDate > stopDay && w.StartDate < DateTime.Now && w.Type == 7
                         select w).ToList();
            ddlWeek.Items.Clear();
            foreach (var week in weeks)
                ddlWeek.Items.Add(new ListItem(string.Format("Year:{0}-Week:{1}. ({2}-{3})", week.Year.ToString(), week.Week.ToString(), week.StartDate.ToShortDateString(), week.EndDate.ToShortDateString()), week.ID.ToString()));
            ddlWeek.SelectedIndex = weeks.Count - 1;
        }
        private void init4Week()
        {
            var stopDay = DateTime.Now.AddDays(-35);
            var weeks = (from w in DbContext.Current().WorkWeeks
                         where w.EndDate > stopDay && w.StartDate < DateTime.Now && w.Type == 7
                         select w).ToList();
            ddlWeek.Items.Clear();
            foreach (var week in weeks)
            {
                DateTime fourWeekStartDay = week.EndDate.AddDays(-27);
                ddlWeek.Items.Add(new ListItem(string.Format("Year:{0}-({1}-{2})", week.Year.ToString(), fourWeekStartDay.ToShortDateString(), week.EndDate.ToShortDateString()), week.ID.ToString()));
            }
            ddlWeek.SelectedIndex = weeks.Count - 1;
        }
        private void initPayElementGroup()
        {
            ddlPayElementGroup.Items.Clear();
            var payElementGroups = DbContext.Current().PayElementGroups.ToList();
            foreach (var peg in payElementGroups)
                ddlPayElementGroup.Items.Add(new ListItem(peg.Name, peg.ID.ToString()));
        }

    }
}