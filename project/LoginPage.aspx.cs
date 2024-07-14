using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoginPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void ResetBtn_Click(object sender, EventArgs e)
    {
        this.UsernameTextBox.Text = "";
        this.PassTextBox.Text = "";
        this.teacher.Checked = false;
        this.student.Checked = false;
        this.admin.Checked = false;
    }

    protected void LoginBtn_Click(object sender, EventArgs e)
    {
        if (UsernameTextBox.Text == "" || PassTextBox.Text == "")  
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('用户名和密码不得为空！');", true);
        }
        else if (teacher.Checked == false && student.Checked == false && admin.Checked == false)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请选择登录身份！');", true);
        }
        else
        {
            String UserID = UsernameTextBox.Text;
            String Password = PassTextBox.Text;
            SqlConnection LoginConn = new SqlConnection();
            LoginConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            LoginConn.Open();
            String SQLString;
            if (teacher.Checked == true)
            {
                SQLString = "SELECT * FROM TB_Teacher WHERE TeacherID='" + UserID + "' AND TPassword='" + Password + "'";
            }
            else if (student.Checked == true)
            {
                SQLString = "SELECT * FROM TB_Student WHERE StuID='" + UserID + "' AND SPassword='" + Password + "'";
            }
            else
            {
                SQLString = "SELECT * FROM TB_Admin WHERE AdminID='" + UserID + "' AND APassword='" + Password + "'";
            }
            SqlCommand LoginCmd = new SqlCommand(SQLString, LoginConn); 
            SqlDataReader RsLogin = LoginCmd.ExecuteReader();
            RsLogin.Read();
            if (RsLogin.HasRows)
            {
                if (teacher.Checked == true)
                {
                    TeacherLoginLabel.Text = RsLogin["TeacherName"].ToString() + ",欢迎您登录成功！";
                    this.TeacherPanel.Visible = true;
                    Session["TeacherID"] = RsLogin["TeacherID"].ToString();
                    Session["TeacherName"] = RsLogin["TeacherName"].ToString();
                }
                else if (student.Checked == true)
                {
                    StuLoginLabel.Text = RsLogin["StuName"].ToString() + ",欢迎您登录成功！";
                    this.StuPanel.Visible = true;
                    Session["StuID"] = RsLogin["StuID"].ToString();
                    Session["StuName"] = RsLogin["StuName"].ToString();
                }
                else
                {
                    AdminLoginLabel.Text = RsLogin["AdminName"].ToString() + ",欢迎您登录成功！";
                    this.AdminPanel.Visible = true;
                    Session["AdminID"] = RsLogin["AdminID"].ToString();
                    Session["AdminName"] = RsLogin["AdminName"].ToString();
                }
                LoginPanel.Visible = false;
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('用户名或密码错误！');", true);
            }
            RsLogin.Close();
            LoginConn.Close();
        }
    }
}