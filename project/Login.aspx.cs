using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginRestBtn_Click(object sender, EventArgs e)
    {
        StuIDTextBox.Text = "";
        PassTextBox.Text = "";
    }

    protected void LoginSubmitBtn_Click(object sender, EventArgs e)
    {
        if ((StuIDTextBox.Text == "") || (PassTextBox.Text == ""))
        {
            Response.Write("<SCRIPT language='javascript'>alert('学号或密码不得为空！！！'); </SCRIPT>");
        }
        else
        {
            string StuID = this.StuIDTextBox.Text;
            string StuPassword = this.PassTextBox.Text;
            SqlConnection TeachingWebConn = new SqlConnection("server=LAPTOP-QC26KDDS;database=DB_TeachingMS;Trusted_Connection=SSPI");
            TeachingWebConn.Open();
            SqlCommand LoginCmd = new SqlCommand("SELECT * FROM TB_Student WHERE StuID='" + StuID + "' AND SPassword='" + StuPassword + "'", TeachingWebConn);
            SqlDataReader RsLogin = LoginCmd.ExecuteReader();
            RsLogin.Read();
            if (RsLogin.HasRows)
            {
                LoginOKLabel.Text = RsLogin["StuName"].ToString() + ",欢迎您登录成功！";
                this.loginPanel.Visible = false;
                this.LoginOKPanel.Visible = true;
                Session["StuID"] = RsLogin["StuID"].ToString();
                Session["StuName"] = RsLogin["StuName"].ToString();
            }
            else
            {
                Response.Write("<SCRIPT language='javascript'>alert('学号或密码错误！！！'); </SCRIPT>");
            }
            RsLogin.Close();
            TeachingWebConn.Close();
        }
    }
}