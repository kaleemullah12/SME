using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEForm
{
    public partial class Alert : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("select * from Alert where Type='Visa'", con);
            SqlCommand cmd1 = new SqlCommand("select * from Alert where Type='Awol'", con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                txtdays.Text = rdr["Days"].ToString();
                txtName.Text = rdr["TaskName"].ToString();
                txtPath.Text = rdr["Path"].ToString();
                txtMail.Text = rdr["mailto"].ToString();
                txtSubject.Text = rdr["mailsubject"].ToString();
                txtbody.Text = rdr["mailbody"].ToString();
            }
            con.Close();
            con.Open();
            SqlDataReader rdr1 = cmd1.ExecuteReader();

            while (rdr1.Read())
            {
                txtdays1.Text = rdr1["Days"].ToString();
                txtName1.Text = rdr1["TaskName"].ToString();
                txtpath1.Text = rdr1["Path"].ToString();
                txtMail2.Text = rdr1["mailto"].ToString();
                txtSubject2.Text = rdr1["mailsubject"].ToString();
                txtbody2.Text = rdr1["mailbody"].ToString();
            }
            con.Close();
        }
        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into Alert(Days,TaskName,Path,Type,mailto,mailsubject,mailbody) values('" + txtdays.Text + "','" + txtName.Text + "','" + txtPath.Text + "','Visa','"+txtMail.Text + "','"+txtSubject.Text + "','"+txtbody.Text + "')", con);
            SqlCommand cmdDel = new SqlCommand("delete from Alert where Type='Visa' ", con);
            con.Open();
            cmdDel.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Created Successfully');</script>");
            LoadData();
        }
        protected void btnCreateNew12_click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into Alert(Days,TaskName,Path,Type,mailto,mailsubject,mailbody) values('" + txtdays1.Text + "','" + txtName1.Text + "','" + txtpath1.Text + "','Awol','" + txtMail2.Text + "','" + txtSubject2.Text + "','" + txtbody2.Text + "')", con);
            SqlCommand cmdDel = new SqlCommand("delete from Alert where Type='Awol' ", con);
            con.Open();
            cmdDel.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Created Successfully');</script>");
            LoadData();
        }
    }
}