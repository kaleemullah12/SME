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
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Master.CheckAdminRight();
                initRole();
                BindGV();
            }
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            User user = new User();
            try
            {
                user.Username = txtUsername.Text;
                user.Password = txtPassword.Text;
                user.Email = txtEmail.Text;
                user.RoleID = int.Parse(ddlRole.SelectedValue);
                BoUser.UpSertUser(user);
            }
            catch
            {
                Master.AppendMessage("Fail to create new User, please ensure username is unique.");
            }
            BindGV();
        }

        private void initRole()
        {
            ddlRole.Items.Clear();
            ddlRole.DataSource = DbContext.Current().UserRoles.ToList();
            ddlRole.DataTextField = "Description";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
        }
        private void BindGV()
        {
            gvUsers.DataSource = BoUser.GetAllUsers();
            gvUsers.DataBind();
        }
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindGV();
        }
        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userID = (int)e.Keys[0];
            var txtPassword = gvUsers.Rows[e.RowIndex].FindControl("txtPassword") as TextBox;
            var txtEmail = gvUsers.Rows[e.RowIndex].FindControl("txtEmail") as TextBox;
            var ddlRole = gvUsers.Rows[e.RowIndex].FindControl("ddlRole") as DropDownList;
            User user = new User();
            try
            {
                user.ID = userID;
                user.Password = txtPassword.Text;
                user.Email = txtEmail.Text;
                user.RoleID = int.Parse(ddlRole.SelectedValue);
                BoUser.UpSertUser(user);
            }
            catch
            {
                Master.AppendMessage("Fail to update User, please ensure username hasn't already been used for another account.");
            }
            gvUsers.EditIndex = -1;
            BindGV();
        }
        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvUsers.EditIndex = -1;
            BindGV();
        }
        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == gvUsers.EditIndex)
            {
                DropDownList ddlRole = e.Row.FindControl("ddlRole") as DropDownList;
                if (ddlRole != null)
                {
                    ddlRole.Items.Clear();
                    ddlRole.DataSource = DbContext.Current().UserRoles.ToList();
                    ddlRole.DataTextField = "Description";
                    ddlRole.DataValueField = "ID";
                    ddlRole.DataBind();

                    int roleID = 1;
                    int.TryParse(((User)e.Row.DataItem).RoleID.ToString(), out roleID);
                    ddlRole.SelectedValue = roleID.ToString();
                }
            }
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int userID = (int)e.Keys[0];
            try
            {
                BoUser.DeleteUser(userID);
            }
            catch
            {
                Master.AppendMessage("Fail to delete User, please try again later.");
            }
            gvUsers.EditIndex = -1;
            BindGV();
        }
    }
}