using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEForm
{
    public partial class Setup : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void AppendMessage(string msg)
        {
            Master.AppendMessage(msg);
        }
    }
}