using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Bo;

namespace SMEForm
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void AppendMessage(string msg)
        {
            if (string.IsNullOrEmpty(hidMsg.Value))
                hidMsg.Value = msg;
            else
                hidMsg.Value += Environment.NewLine + msg;
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}
