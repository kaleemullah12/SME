using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Context;

namespace SMEForm
{
    public partial class Shops : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.CheckAdminRight();
                initCompany();
                initBrand();
                BindGV();
            }
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            Shop s = new Shop();
            try
            {
                s.ID = int.Parse(txtID.Text);
                s.Name = txtName.Text;
                s.BrandID = int.Parse(ddlBrand.SelectedValue);
                s.CompanyID = int.Parse(ddlCompany.SelectedValue);
                DbContext.Current().Shops.AddObject(s);
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to create shop, please makes sure to use a unique number for ID across all companies!");
            }
            BindGV();
        }

        private void initBrand()
        {
            ddlBrand.Items.Clear();
            ddlBrand.DataSource = DbContext.Current().Brands.ToList();
            ddlBrand.DataTextField = "Name";
            ddlBrand.DataValueField = "ID";
            ddlBrand.DataBind();
        }
        private void initCompany()
        {
            ddlCompany.Items.Clear();
            ddlCompany.DataSource = DbContext.Current().Companies.ToList();
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "ID";
            ddlCompany.DataBind();

            int companyID = 1;
            int.TryParse(Request["companyID"], out companyID);
            ddlCompany.SelectedValue = companyID.ToString();
        }
        private void BindGV()
        {
            int companyID = 1;
            int.TryParse(ddlCompany.SelectedValue, out companyID);
            var shops = (from s in DbContext.Current().Shops
                         where s.CompanyID == companyID
                         select s).ToList();
            gvCompanies.DataSource = shops;
            gvCompanies.DataBind();
        }
        protected void gvCompanies_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCompanies.EditIndex = e.NewEditIndex;
            BindGV();
        }
        protected void gvCompanies_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int shopID = (int)e.Keys[0];
            var txtName = gvCompanies.Rows[e.RowIndex].FindControl("txtName") as TextBox;

            try
            {
                Shop s = (from shop in DbContext.Current().Shops
                          where shop.ID == shopID
                          select shop).FirstOrDefault();
                s.Name = txtName.Text;
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to update shop, please try again later!");
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
            int shopID = (int)e.Keys[0];
            try
            {
                Shop s = (from shop in DbContext.Current().Shops
                          where shop.ID == shopID
                          select shop).FirstOrDefault();
                DbContext.Current().Shops.DeleteObject(s);
                DbContext.Current().SaveChanges();
            }
            catch
            {
                Master.AppendMessage("Fail to delete shop, please try again later!");
            }
            gvCompanies.EditIndex = -1;
            BindGV();
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGV();
        }
    }
}