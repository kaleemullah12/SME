using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;
using SMEForm.Context;
namespace SMEForm
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initCompany();
                initJobDescription();
                initPayType();
                int workID = 0;
                int.TryParse(Request["workID"], out workID);
                if (workID > 0)
                    initControls(workID);
            }
        }
        protected void initControls(int workID)
        {
            divWorkID.Visible = true;

            Employee emp = (from e in DbContext.Current().Employees
                            where e.WorkID == workID
                            select e).First();
            ddlCompany.SelectedValue = emp.Shop.CompanyID.ToString();
            initStore();
            ddlShop.SelectedValue = emp.ShopID.ToString();
            lkDetail.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeList.aspx?companyID={0}&shopID={1}", emp.Shop.CompanyID.ToString(), emp.ShopID.ToString()));
            lkDetail.Visible = true;

            if (BoUser.IsAdmin())
            {
                txtSageID.Visible = true;
                lbSageID.Visible = false;
                txtSageID.Text = emp.SageID.ToString();
            }
            else
            {
                txtSageID.Visible = false;
                lbSageID.Visible = true;
                lbSageID.Text = emp.SageID.ToString();
            }
            lbWorkID.Text = emp.WorkID.ToString();
            txtForeName.Text = emp.Forename;
            txtSurname.Text = emp.Surname;
            txtAddress1.Text = emp.Address1;
            txtAddress2.Text = emp.Address2;
            txtAddress3.Text = emp.Address3;
            txtPostcode.Text = emp.Postcode;

            ddlJob.SelectedValue = emp.JobDescriptionID.ToString();
            ddlPay.SelectedValue = emp.PayTypeID.ToString();
            ddlGender.SelectedValue = emp.Gender == true ? "1" : "0";
            txtDoB.Text = emp.DOB == null ? "" : ((DateTime)emp.DOB).ToString("dd/MM/yyyy");

            
            txtVisaExpire.Text = emp.VisaExpireDate == null ? "" : ((DateTime)emp.VisaExpireDate).ToString("dd/MM/yyyy");
            txtVisaApply.Text = emp.VisaApplyDate == null ? "" : ((DateTime)emp.VisaApplyDate).ToString("dd/MM/yyyy");
            lkVacation.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeVacation.aspx?workID={0}", workID.ToString()));
            lkHoliday.NavigateUrl = ResolveClientUrl(string.Format("~/EmployeeHolidays.aspx?workID={0}", workID.ToString()));
            if (emp.PayTypeID == 4 || emp.PayTypeID == 5)
            {
                lkVacation.Visible = true;
            }
            else
            {
                lkVacation.Visible = false;
            }

            txtStartDate.Text = emp.StartDate == null ? "" : ((DateTime)emp.StartDate).ToString("dd/MM/yyyy");
            txtEndDate.Text = emp.EndDate == null ? "" : ((DateTime)emp.EndDate).ToString("dd/MM/yyyy");
            txtNI.Text = emp.NI;
            txtNotes.Text = emp.Note;
            IsActive.Checked = (bool)emp.IsActive;
            txtActivationDate.Text = emp.EmployeeActivation_Date == null ? "" :  emp.EmployeeActivation_Date.Value.ToString("dd-MMM-yyyy hh:mm:ss tt");
            txtInActivationDate.Text = emp.EmployeeInActivation_Date == null ? "" : emp.EmployeeInActivation_Date.Value.ToString("dd-MMM-yyyy hh:mm:ss tt");

            btnCreateUpdate.Text = "Update";
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            initStore();
        }
        private void initCompany()
        {
            var companies = (from c in DbContext.Current().Companies
                             select c).ToList();
            ddlCompany.Items.Clear();
            ddlCompany.Items.Add(new ListItem("<<Please Select>>", "-1"));
            foreach (var com in companies)
                ddlCompany.Items.Add(new ListItem(com.Name, com.ID.ToString()));
            ddlShop.Items.Clear();
            ddlShop.Items.Add(new ListItem("<<Please Select>>", "-1"));
        }
        private void initStore()
        {
            int companyid = int.Parse(ddlCompany.SelectedValue);
            if (companyid > 0)
            {
                ddlShop.Items.Clear();
                ddlShop.Items.Add(new ListItem("<<Please Select>>", "-1"));
                var shops = (from s in DbContext.Current().Shops
                             where s.CompanyID == companyid
                             orderby s.Name
                             select s).ToList();
                foreach (var shop in shops)
                    ddlShop.Items.Add(new ListItem(shop.Name, shop.ID.ToString()));
            }
        }
        private void initJobDescription()
        {
            var jobDescriptions = (from j in DbContext.Current().JobDescriptions
                                   select j).ToList();
            ddlJob.Items.Clear();
            ddlJob.Items.Add(new ListItem("<<Please Select>>", "-1"));
            foreach(var job in jobDescriptions)
                ddlJob.Items.Add(new ListItem(job.Name, job.ID.ToString()));
        }
        private void initPayType()
        {
            var paytypes = (from p in DbContext.Current().PayTypes
                           select p).ToList();
            ddlPay.Items.Clear();
            ddlPay.Items.Add(new ListItem("<<Please Select>>", "-1"));
            foreach (var pay in paytypes)
                ddlPay.Items.Add(new ListItem(pay.Name, pay.ID.ToString()));
        }
        protected void btnCreateUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int companyID = int.Parse(ddlCompany.SelectedValue);
                int shopID = int.Parse(ddlShop.SelectedValue);
                var newEmp = new Employee();
                
                int workID = 0;
                int.TryParse(Request["workID"], out workID);
                if (workID == 0)
                {
                    var keys = BoEmployee.GetNewWorkIDSageID(companyID, shopID);
                    newEmp.WorkID = keys.Key;
                    newEmp.SageID = keys.Value;
                }
                else
                {
                    Employee emp = (from em in DbContext.Current().Employees
                                    where em.WorkID == workID
                                    select em).First();
                    newEmp.WorkID = emp.WorkID;
                    newEmp.SageID = emp.SageID;
                }

                int sageID = 0;
                if (BoUser.IsAdmin() && txtSageID.Visible && int.TryParse(txtSageID.Text, out sageID))
                    newEmp.SageID = sageID;

                newEmp.ShopID = shopID;
                newEmp.Initial = txtForeName.Text.Substring(0,1).ToUpper();
                newEmp.Forename = txtForeName.Text;
                newEmp.Surname = txtSurname.Text;
                newEmp.Address1 = txtAddress1.Text;
                newEmp.Address2 = txtAddress2.Text;
                newEmp.Address3 = txtAddress3.Text;
                newEmp.Postcode = txtPostcode.Text;

                newEmp.JobDescriptionID = int.Parse(ddlJob.SelectedValue);
                newEmp.PayTypeID = int.Parse(ddlPay.SelectedValue);
                newEmp.Gender = ddlGender.SelectedValue == "1";
                newEmp.DOB = string.IsNullOrEmpty(txtDoB.Text) ? (DateTime?)null : DateTime.ParseExact(txtDoB.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);

                newEmp.VisaExpireDate = string.IsNullOrEmpty(txtVisaExpire.Text) ? (DateTime?)null : DateTime.ParseExact(txtVisaExpire.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                newEmp.VisaApplyDate = string.IsNullOrEmpty(txtVisaApply.Text) ? (DateTime?)null : DateTime.ParseExact(txtVisaApply.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                newEmp.StartDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                newEmp.EndDate = string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime?)null : DateTime.ParseExact(txtEndDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                newEmp.NI = txtNI.Text;
                newEmp.Note = txtNotes.Text;

                //newEmp.IsActive = true;
                newEmp.IsActive = (bool)IsActive.Checked;

                BoEmployee.UpSertEmployee(newEmp);
                DbContext.Current().SaveChanges();

                


                if (workID == 0)
                {
                    lbWorkID.Text = newEmp.WorkID.ToString();
                    lbSageID.Text = newEmp.SageID.ToString();
                    divWorkID.Visible = true;
                    workID = newEmp.WorkID;

                    Master.AppendMessage(string.Format("New employee created. WorkID:{0} and SageID:{1}", newEmp.WorkID.ToString(), newEmp.SageID.ToString()));
                    btnCreateUpdate.Visible = false;
                    btnRestart.Visible = true;
                }
                else
                {
                    Master.AppendMessage("Employee updated successfully!");
                }
                initControls(workID);
            }
            catch(Exception ex)
            {
                Master.AppendMessage("Fail to create new employee, please try again later!");
            }
        }
        protected void btnRestart_Click(object sender, EventArgs e)
        {
            divWorkID.Visible = false;
            txtForeName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtAddress3.Text = string.Empty;
            txtDoB.Text = string.Empty;
            txtPostcode.Text = string.Empty;
            txtVisaExpire.Text = string.Empty;
            txtVisaApply.Text = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtNI.Text = string.Empty;
            initCompany();
            ddlJob.SelectedIndex = -1;
            btnCreateUpdate.Visible = true;
            btnRestart.Visible = false;
        }

        protected void ddlPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            int payType = int.Parse(ddlPay.SelectedValue);
            if (payType == 4 || payType == 5)
            {
                rfVisaExpire.Enabled = true;
                lkVacation.Visible = true;
            }
            else
            {
                rfVisaExpire.Enabled = false;
                lkVacation.Visible = false;
            }
        }


    }
}