using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;

namespace SMEForm
{
    public partial class Admin1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void CheckAdminRight()
        {
            if (!BoUser.IsAdmin())
            {
                AdminContent.Visible = false;
                Master.AppendMessage("Sorry, Current login has no admin rights, please logout and login again as a different user and try again.");
            }
        }
        public void AppendMessage(string msg)
        {
            Master.AppendMessage(msg);
        }
    }
}