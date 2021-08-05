using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;
using SMEForm.Helper;

namespace SMEForm.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            BoUser user = new BoUser();
            if (user.Get(LoginUser.UserName, LoginUser.Password) != null)
            {
                e.Authenticated = true;
            }
            else
                e.Authenticated = false;
        }
    }
}
