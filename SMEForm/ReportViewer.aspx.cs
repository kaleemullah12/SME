using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SMEForm.Helper;
namespace SMEForm
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ShowReport();
        }
        private void ShowReport()
        {
            string reportName = Request["name"];
            if (!string.IsNullOrEmpty(reportName))
            {
                ReportViewer1.ServerReport.ReportServerUrl = new Uri(Util.ReportServerURL);
                ReportViewer1.ServerReport.ReportServerCredentials = new MyReportServerConnection();
                ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                ReportViewer1.ServerReport.ReportPath = Util.ReportFolder + reportName;
                List<ReportParameter> repParList = new List<ReportParameter>();
                ReportViewer1.ServerReport.SetParameters(repParList);
            }
        }
    }
}