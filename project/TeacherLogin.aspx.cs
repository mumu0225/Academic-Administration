using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginSubmitBtn_Click(object sender, EventArgs e)
    {
        if ((TeacherIDTextBox.Text == "") || (PassTextBox.Text == ""))
        {
            Response.Write("<SCRIPT language='javascript'>alert('工号或密码不得为空！！！'); </SCRIPT>");
        }
        else
        {
            string TeacherID = this.TeacherIDTextBox.Text;
            string TeacherPassword = this.PassTextBox.Text;
            SqlConnection TeachingWebConn = new SqlConnection("server=LAPTOP-QC26KDDS;database=DB_TeachingMS;Trusted_Connection=SSPI");
            TeachingWebConn.Open();
            SqlCommand LoginCmd = new SqlCommand("SELECT * FROM TB_Teacher WHERE TeacherID='" + TeacherID + "' AND TPassword='" + TeacherPassword + "'", TeachingWebConn);
            SqlDataReader RsLogin = LoginCmd.ExecuteReader();
            RsLogin.Read();
            if (RsLogin.HasRows)
            {
                LoginOKLabel.Text = RsLogin["TeacherName"].ToString() + ",欢迎您登录成功！";
                this.loginPanel.Visible = false;
                this.LoginOKPanel.Visible = true;
                Session["TeacherID"] = RsLogin["TeacherID"].ToString();
                Session["TeacherName"] = RsLogin["TeacherName"].ToString();
            }
            else
            {
                Response.Write("<SCRIPT language='javascript'>alert('工号或密码错误！！！'); </SCRIPT>");
            }
            RsLogin.Close();
            TeachingWebConn.Close();
        }
    }

    protected void LoginRestBtn_Click(object sender, EventArgs e)
    {
        TeacherIDTextBox.Text = "";
        PassTextBox.Text = "";
    }
}