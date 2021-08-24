using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Excel;
using System.IO;
using SMEForm.Context;
using SMEForm.Bo;
using System.Data;
using SMEForm.Helper;

namespace SMEForm
{
    public partial class EmployeeImportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                initCompany();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (!fuSageFile.HasFile)
            {
                Master.AppendMessage("Please select a Sage employee file to Import!");
                return;
            }
            var extension = fuSageFile.FileName.Split(".".ToCharArray()).Last();
            if (extension.ToLower() != "csv")
            {
                Master.AppendMessage("Please select a csv file!");
                return;
            }
            int rowCount = 1;
            int newCount = 0;
            int updateCount = 0;
            var sr = new StreamReader(fuSageFile.PostedFile.InputStream);
            DataTable dt;
            try
            {
                dt = CSVParser.CSVtoDataTable(sr.ReadToEnd(), true);
            }
            catch
            {
                throw new ApplicationException(string.Format("File {0} is corrupted, please fix before continue.", fuSageFile.FileName));
            }
            int sageID = 0, workID = 0, shopid = 0;
            string initial, forename, lastname, address1, address2, address3, postcode, tel, email, mobile, gender, nation, ethic, taxcode, niCat, ni, analysis1, analysis2;
            bool disabled,Active;
            DateTime dob, startDate;
            try
            {
                rowCount++;
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        
                        workID = int.Parse(dr["Works Reference"].ToString());
                    }
                    catch
                    {
                        throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate work reference.", fuSageFile.FileName, rowCount.ToString()));
                    }
                    if (workID > 0)
                    {
                        try
                        {
                            sageID = int.Parse(dr["Employee Reference"].ToString());
                        }
                        catch
                        {
                            throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate sage ID.", fuSageFile.FileName, rowCount.ToString()));
                        }
                        try
                        {
                            startDate = DateTime.Parse(dr["Work Start Date"].ToString());
                        }
                        catch
                        {
                            throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate start working date.", fuSageFile.FileName, rowCount.ToString()));
                        }
                        try
                        {
                            dob = DateTime.Parse(dr["Date of Birth"].ToString());
                        }
                        catch
                        {
                            throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate date of birth.", fuSageFile.FileName, rowCount.ToString()));
                        }
                        try
                        {
                            int temp = int.Parse(dr["Disabled"].ToString());
                            disabled = temp > 0;
                        }
                        catch
                        {
                            throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate value to indicate disable, the value has to be 1 or 0.", fuSageFile.FileName, rowCount.ToString()));
                        }
                        try
                        {
                            shopid = int.Parse(dr["Department Reference"].ToString());
                        }
                        catch
                        {
                            throw new ApplicationException(string.Format("File {0} at Row {1} doesn't contain a validate departmentID.", fuSageFile.FileName, rowCount.ToString()));
                        }

                        initial = dr["Initial"].ToString();
                        lastname = dr["Surname"].ToString();
                        forename = dr["Forename"].ToString();
                        address1 = dr["Address 1"].ToString();
                        Active = int.Parse(dr["Status"].ToString())>0?true:false;
                        address2 = dr["Address 2"].ToString();
                        address3 = dr["Address 3"].ToString();
                        postcode = dr["Post Code"].ToString();
                        tel = dr["Telephone Number"].ToString();
                        mobile = dr["Mobile Number"].ToString();
                        email = dr["E-mail Address"].ToString();
                        gender = dr["Gender"].ToString();
                        nation = dr["Nationality"].ToString();
                        ethic = dr["Ethnic Origin"].ToString();
                        taxcode = dr["Tax Code"].ToString();
                        niCat = dr["NI Category"].ToString();
                        ni = dr["NI Number"].ToString();
                        analysis1 = dr["Analysis 1"].ToString();
                        analysis2 = dr["Analysis 2"].ToString();
                        int jobID = 2;
                        if (!string.IsNullOrEmpty(analysis1) && analysis1.ToLower().Trim() == "student")
                            jobID = 1;
                        if (!string.IsNullOrEmpty(analysis1) && analysis1.ToLower().Contains("manager"))
                            jobID = 3;
                        Employee emp = new Employee
                        {
                            WorkID = workID,
                            SageID = sageID,
                            StartDate = startDate,
                            DOB = dob,
                            Disabled = disabled,
                            Initial = initial,
                            Forename = forename,
                            Surname = lastname,
                            Address1 = address1,
                            Address2 = address2,
                            Address3 = address3,
                            Postcode = postcode,
                            IsActive=Active,
                            Tel = tel,
                            Mobile = mobile,
                            Email = email,
                            Gender = (gender.ToLower() == "male"),
                            Nationality = nation,
                            Ethnic = ethic,
                            TaxCode = taxcode,
                            NICat = niCat,
                            NI = ni,
                            JobDescriptionID = jobID,
                            ShopID = shopid
                        };
                        if (BoEmployee.UpSertEmployee(emp))
                            newCount++;
                        else
                            updateCount++;
                    }
                    DbContext.Current().SaveChanges();
                }
                
                Master.AppendMessage(string.Format(@"{0} new records are created, {1} existing record updated.", newCount.ToString(), updateCount.ToString()));
                SessionManager.SetSession<List<Employee>>(SessionKey.SessionEmployeeImport, BoEmployee.GetAllEmployees());
                BindGV();
            }
            catch (Exception ex)
            {
                Master.AppendMessage(workID.ToString() + " " + ex.Message);
            }
        }
        void BindGV()
        {
            List<Employee> employees = SessionManager.GetSession<List<Employee>>(SessionKey.SessionEmployeeImport);
            GVImports.DataSource = employees;
            GVImports.DataBind();
        }
        protected void GVImports_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            GVImports.EditIndex = -1;
            BindGV();
        }
        protected void GVImports_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVImports.EditIndex = e.NewEditIndex;
            BindGV();
        }
        //protected void GVImports_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    int workID = (int)e.Keys[0];
        //    var lblSageID = GVImports.Rows[e.RowIndex].FindControl("lblSageID") as Label;
        //    var txtForename = GVImports.Rows[e.RowIndex].FindControl("txtForename") as TextBox;
        //    var txtSurname = GVImports.Rows[e.RowIndex].FindControl("txtSurname") as TextBox;
        //    var ddlJobDesc = GVImports.Rows[e.RowIndex].FindControl("ddlJobDesc") as DropDownList;
        //    var ddlShop = GVImports.Rows[e.RowIndex].FindControl("ddlShop") as DropDownList;
        //    var txtVisaExpire = GVImports.Rows[e.RowIndex].FindControl("txtVisaExpire") as TextBox;
        //    DateTime? to = null;
        //    DateTime? from = null;
        //    DateTime? expire = null;
        //    if(!string.IsNullOrEmpty(txtVisaExpire.Text))
        //        expire = DateTime.Parse(txtVisaExpire.Text);
        //    try
        //    {
        //        BoEmployee.UpdateEmployee(int.Parse(lblSageID.Text), int.Parse(ddlShop.SelectedValue), int.Parse(ddlJobDesc.SelectedValue),
        //            workID, txtForename.Text, txtSurname.Text, from, to, expire);
        //    }
        //    catch (Exception ex)
        //    {
        //        Master.AppendMessage(ex.Message);
        //    }
        //    GVImports.EditIndex = -1;
        //    List<Employee> employees = SessionManager.GetSession<List<Employee>>(SessionKey.SessionEmployeeImport);
        //    var emp = (from em in employees
        //               where em.WorkID == workID
        //               select em).First();
        //    emp.Forename = txtForename.Text;
        //    emp.Surname = txtSurname.Text;
        //    emp.JobDescriptionID = int.Parse(ddlJobDesc.SelectedValue);
        //    emp.ShopID = int.Parse(ddlShop.SelectedValue);
        //    emp.HolidayStartDate = from;
        //    emp.HolidayEndDate = to;
        //    emp.VisaExpireDate = expire;
        //    SessionManager.SetSession<List<Employee>>(SessionKey.SessionEmployeeImport, employees);

        //    BindGV();
        //}
        protected void GVImports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == GVImports.EditIndex)
            {
                DropDownList ddlJobDesc = e.Row.FindControl("ddlJobDesc") as DropDownList;
                DropDownList ddlShop = e.Row.FindControl("ddlShop") as DropDownList;
                int companyID = int.Parse(ddlCompany.SelectedValue);
                if (ddlJobDesc != null && ddlShop != null)
                {
                    ddlJobDesc.DataSource = DbContext.Current().JobDescriptions.ToList();
                    ddlJobDesc.DataTextField = "Name";
                    ddlJobDesc.DataValueField = "ID";
                    ddlJobDesc.DataBind();
                    ddlJobDesc.SelectedValue = ((Employee)e.Row.DataItem).JobDescriptionID.ToString();

                    var shops = (from s in DbContext.Current().Shops
                                where s.CompanyID == companyID
                                orderby s.Name
                                select s).ToList();
                    ddlShop.DataSource = shops;
                    ddlShop.DataTextField = "Name";
                    ddlShop.DataValueField = "ID";
                    ddlShop.DataBind();
                    ddlShop.SelectedValue = ((Employee)e.Row.DataItem).ShopID.ToString();
                }
            }
        }
        protected void GVImports_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int workID = (int)e.Keys[0];
            BoEmployee.DeleteEmployee(workID);
            List<Employee> employees = SessionManager.GetSession<List<Employee>>(SessionKey.SessionEmployeeImport);
            var emp = (from em in employees
                       where em.WorkID == workID
                       select em).First();
            employees.Remove(emp);
            SessionManager.SetSession<List<Employee>>(SessionKey.SessionEmployeeImport, employees);
            BindGV();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Refresh()
        {
            int companyid = int.Parse(ddlCompany.SelectedValue);
            int shopid = int.Parse(ddlShop.SelectedValue);
            if (companyid > 0)
            {
                SessionManager.SetSession<List<Employee>>(SessionKey.SessionEmployeeImport, BoEmployee.GetEmployeesbyComapny(companyid, shopid));
                BindGV();
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

            if (!string.IsNullOrEmpty(Request["companyID"]))
            {
                ddlCompany.SelectedValue = Request["companyID"];
                if (!string.IsNullOrEmpty(Request["shopID"]))
                {
                    initStore();
                    ddlShop.SelectedValue = Request["shopID"];
                }
                Refresh();
            }
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
        protected void GVImports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVImports.PageIndex = e.NewPageIndex;
            BindGV();
        }
        protected void GVImports_Sorting(object sender, GridViewSortEventArgs e)
        {
            var imports = SessionManager.GetSession<List<Employee>>(SessionKey.SessionEmployeeImport);
            IOrderedEnumerable<Employee> emps = imports.OrderBy(em => em.SageID);
            switch (e.SortExpression)
            {
                case "SageID":
                    emps = imports.OrderBy(em => em.SageID);
                    break;
                case "WorkID":
                    emps = imports.OrderBy(em => em.WorkID);
                    break;
                case "PaymentRefID":
                    emps = imports.OrderBy(em => em.JobDescriptionID);
                    break;
                case "Forename":
                    emps = imports.OrderBy(em => em.Forename);
                    break;
                case "Surname":
                    emps = imports.OrderBy(em => em.Surname);
                    break;
                case "ShopID":
                    emps = imports.OrderBy(em => em.ShopID);
                    break;
                case "VisaExpireDate":
                    emps = imports.OrderBy(em => em.VisaExpireDate);
                    break;
                default:
                    break;
            }
            SessionManager.SetSession<List<Employee>>(SessionKey.SessionEmployeeImport, emps.ToList());
            BindGV();
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            initStore();
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeDetails.aspx");
        }
    }
}