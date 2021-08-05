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
    public partial class ExcelInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("select * from ImpExcelRef where CompanyType='KFC'", con);
                SqlCommand cmd1 = new SqlCommand("select * from ImpExcelRef where CompanyType='Generic'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    txtWorkID.Text = rdr["WorkID"].ToString();
                    txtDate.Text = rdr["Date"].ToString();
                    txtClockIn.Text = rdr["ClockIn"].ToString();
                    txtClockOut.Text = rdr["ClockOut"].ToString();
                    txtFirstName.Text = rdr["FirstName"].ToString();
                    txtWorkHour.Text = rdr["WorkHour"].ToString();
                }
                con.Close();
                con.Open();
                SqlDataReader rdr1 = cmd1.ExecuteReader();

                while (rdr1.Read())
                {
                    TextBox1.Text = rdr1["WorkID"].ToString();
                    TextBox2.Text = rdr1["Date"].ToString();
                    TextBox3.Text = rdr1["ClockIn"].ToString();
                    TextBox4.Text = rdr1["ClockOut"].ToString();
                    TextBox5.Text = rdr1["FirstName"].ToString();
                    TextBox6.Text = rdr1["WorkHour"].ToString();
                    TextBox7.Text = rdr1["StartRowNo"].ToString();
                }
                con.Close();
            }
            
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into ImpExcelRef(WorkID,Date,ClockIn,ClockOut,FirstName,WorkHour,CompanyType) values('" + txtWorkID.Text + "','" + txtDate.Text + "','" + txtClockIn.Text + "','" + txtClockOut.Text + "','" + txtFirstName.Text + "','"+txtWorkHour.Text+"','KFC')", con);
            SqlCommand cmdDel = new SqlCommand("delete from ImpExcelRef where CompanyType='KFC'", con);
            con.Open();
            cmdDel.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Created Successfully');</script>");
            //Console.WriteLine("Record Created Successfully");
        }

        protected void btnCreateNew1_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SMEConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("insert into ImpExcelRef(WorkID,Date,ClockIn,ClockOut,FirstName,WorkHour,CompanyType,StartRowNo) values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','Generic','"+TextBox7.Text+"')", con);
            SqlCommand cmdDel = new SqlCommand("delete from ImpExcelRef where CompanyType='Generic'", con);
            con.Open();
            cmdDel.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            con.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record Created Successfully');</script>");

        }
    }
}