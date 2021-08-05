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
    public partial class MailSetup : System.Web.UI.Page
    {

        void get_MailSetup()

            {
                string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("select * from MailSetup", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtsmtp.Text = rdr["Smtp"].ToString();
                    txtemail.Text = rdr["Email"].ToString();
                    txtport.Text = rdr["SendPort"].ToString();
                    txtuserid.Text = rdr["UserID"].ToString();
                    //txtpassword.Text = 

                    //txtpassword.Attributes["type"] = "text";
                    //txtpassword.Text = rdr["Password"].ToString();
                    //txtpassword.Attributes["type"] = "password";


                    txtpassword.Attributes.Add("value", rdr["Password"].ToString());


                }
                con.Close();
            }
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                get_MailSetup();
            }

        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into MailSetup(Smtp,Email,SendPort,UserID,Password) values('" + txtsmtp.Text + "','" + txtemail.Text + "','" + txtport.Text + "','" + txtuserid.Text + "','" + txtpassword.Text + "')", con);
            SqlCommand cmdDel = new SqlCommand("delete from MailSetup", con);
            con.Open();
            cmdDel.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            get_MailSetup();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Created Successfully');</script>");
            
        }
    }
}