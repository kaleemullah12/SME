using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;

namespace SMEForm.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }
        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            string username = HttpContext.Current.User.Identity.Name;
            BoUser user = new BoUser();
            if(user.ChangePassword(username, CurrentPassword.Text, NewPassword.Text))
                Response.Redirect("ChangePasswordSuccess.aspx");
            else
            {
                CustomValidator cv = new CustomValidator();
                cv.IsValid = false;
                cv.ErrorMessage = "Change password was not successful, please try again.";
                cv.CssClass = "failureNotification";
                cv.ValidationGroup = "ChangeUserPasswordValidationGroup";
                this.Page.Validators.Add(cv);
            }
        }
    }
}
