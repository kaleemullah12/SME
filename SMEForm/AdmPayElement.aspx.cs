using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMEForm.Context;

namespace SMEForm
{
    public partial class AdmPayElement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initPayType();
                initPayElement();
                initXRef();
            }
        }
        private void initPayType()
        {
            ddlPayType.Items.Clear();
            ddlPayType.DataSource = DbContext.Current().PayTypes.ToList();
            ddlPayType.DataTextField = "Name";
            ddlPayType.DataValueField = "ID";
            ddlPayType.DataBind();
            ddlPayType.SelectedIndex = 0;
        }
        private void initPayElement()
        {
            cblPayElement.Items.Clear();
            cblPayElement.DataSource = DbContext.Current().PayElements.ToList();
            cblPayElement.DataTextField = "Name";
            cblPayElement.DataValueField = "ID";
            cblPayElement.DataBind();
        }
        private void initXRef()
        {
            int payTypeID = int.Parse(ddlPayType.SelectedValue);
            var selectPayElements = (from pex in DbContext.Current().PayTypeXrefElements
                                     where pex.PayTypeID == payTypeID
                                     select pex.PayElementID).ToList();
            foreach (ListItem item in cblPayElement.Items)
                if (selectPayElements.Contains(int.Parse(item.Value)))
                    item.Selected = true;
                else
                    item.Selected = false;
        }
        protected void ddlPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            initXRef();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in cblPayElement.Items)
            {
                int payTypeID = int.Parse(ddlPayType.SelectedValue);
                int payElementID = int.Parse(item.Value);
                var xref = DbContext.Current().PayTypeXrefElements.Where(x => x.PayTypeID == payTypeID && x.PayElementID == payElementID).FirstOrDefault();
                if (xref != null && !item.Selected)
                    DbContext.Current().PayTypeXrefElements.DeleteObject(xref);
                if (xref == null && item.Selected)
                    DbContext.Current().PayTypeXrefElements.AddObject(new PayTypeXrefElement
                    {
                        PayElementID = payElementID,
                        PayTypeID = payTypeID
                    });
            }
            DbContext.Current().SaveChanges();
            Master.AppendMessage("Save complete!");
        }
    }
}