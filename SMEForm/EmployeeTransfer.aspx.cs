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
    public partial class EmployeeTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                initCompany();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            initStore();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0, shopID = 0;
            int.TryParse(ddlCompany.SelectedValue, out companyID);
            int.TryParse(ddlShop.SelectedValue, out shopID);
            if (companyID <= 0 || shopID <= 0)
            {
                Master.AppendMessage("Please select a company and a shop!");
                return;
            }
            try
            {
                var keys = BoEmployee.GetNewWorkIDSageID(companyID, shopID);
                int oldWorkID = int.Parse(Request["workID"]);
                var oldEmp = (from em in DbContext.Current().Employees
                              where em.WorkID == oldWorkID
                              select em).First();
                var newEmp = new Employee();
                newEmp.WorkID = keys.Key;
                newEmp.SageID = keys.Value;
                newEmp.ShopID = shopID;
                newEmp.JobDescriptionID = oldEmp.JobDescriptionID;
                newEmp.Initial = oldEmp.Initial;
                newEmp.Forename = oldEmp.Forename;
                newEmp.Surname = oldEmp.Surname;
                newEmp.Address1 = oldEmp.Address1;
                newEmp.Address2 = oldEmp.Address2;
                newEmp.Address3 = oldEmp.Address3;
                newEmp.Postcode = oldEmp.Postcode;
                newEmp.Tel = oldEmp.Tel;
                newEmp.Mobile = oldEmp.Mobile;
                newEmp.Email = oldEmp.Email;
                newEmp.Gender = oldEmp.Gender;
                newEmp.DOB = oldEmp.DOB;
                newEmp.Disabled = oldEmp.Disabled;
                newEmp.Nationality = oldEmp.Nationality;
                newEmp.Ethnic = oldEmp.Ethnic;
                newEmp.TaxCode = oldEmp.TaxCode;
                newEmp.NICat = oldEmp.NICat;
                newEmp.NI = oldEmp.NI;
                newEmp.StartDate = oldEmp.StartDate;
                newEmp.EndDate = oldEmp.EndDate;
                newEmp.HolidayStartDate = oldEmp.HolidayStartDate;
                newEmp.HolidayEndDate = oldEmp.HolidayEndDate;
                newEmp.VisaExpireDate = oldEmp.VisaExpireDate;
                newEmp.IsActive = true;
                newEmp.OriWorkID = oldEmp.WorkID;
                BoEmployee.UpSertEmployee(newEmp);

                DbContext.Current().SaveChanges();
                BoEmployee.DeleteEmployee(oldWorkID);
                Master.AppendMessage(string.Format("Employee has been transfered to {0} - {1}.", ddlCompany.SelectedItem.Text, ddlShop.SelectedItem.Text));
            }
            catch
            {
                Master.AppendMessage("Failed to transfer the employee, please try again later.");
            }
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
    }
}