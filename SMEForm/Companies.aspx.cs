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
    public partial class Companies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.CheckAdminRight();
                BindGV();
            }
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            Company c = new Company();
            try
            {
                c.ID = int.Parse(txtID.Text);
                c.Name = txtName.Text;
                DbContext.Current().Companies.AddObject(c);
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to create company, please makes sure to use a unique number for ID!");
            }
            BindGV();
        }
        private void BindGV()
        {
            gvCompanies.DataSource = DbContext.Current().Companies.ToList();
            gvCompanies.DataBind();
        }
        protected void gvCompanies_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCompanies.EditIndex = e.NewEditIndex;
            BindGV();
        }
        protected void gvCompanies_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int companyID = (int)e.Keys[0];
            var txtName = gvCompanies.Rows[e.RowIndex].FindControl("txtName") as TextBox;

            try
            {
                Company c = (from com in DbContext.Current().Companies
                             where com.ID == companyID
                             select com).FirstOrDefault();
                c.Name = txtName.Text;
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to update company, please try again later!");
            }
            gvCompanies.EditIndex = -1;
            BindGV();
        }
        protected void gvCompanies_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvCompanies.EditIndex = -1;
            BindGV();
        }
        protected void gvCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gvCompanies_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int companyID = (int)e.Keys[0];
            try
            {
                Company c = (from com in DbContext.Current().Companies
                             where com.ID == companyID
                             select com).FirstOrDefault();
                DbContext.Current().Companies.DeleteObject(c);
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to delete company, please make sure there is no shops associated!");
            }
            gvCompanies.EditIndex = -1;
            BindGV();
        }
    }
}