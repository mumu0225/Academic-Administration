using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuGradeQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StuIDNameLabel.Text = "<学号：" + Session["StuID"].ToString() +"  姓名：" + Session["StuName"].ToString() + ">";
        //建立数据库连接，从Web.config文件获取数据库连接字符串
        SqlConnection StuGradeConn = new SqlConnection();
        StuGradeConn.ConnectionString =
        ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuGradeConn.Open();
        //创建查询SQL语句，并用SqlDataAdapter对象获取数据 
        SqlCommand StuGradeCmd = new SqlCommand("SELECT CourseName 课程名称, TotalScore 课程成绩, RetestScore 补考成绩 FROM TB_Grade TG join TB_Course TC on TG.CourseID = TC.CourseID AND StuID = '" + Session["StuID"].ToString() + "'", 
        StuGradeConn);
        SqlDataAdapter StuGradeAdapter = new SqlDataAdapter(StuGradeCmd);
        //将SqlDataAdapter对象中的数据填充到DataSet对象的表"StuGradeTable"中
        DataSet StuGradeDS = new DataSet();
        StuGradeAdapter.Fill(StuGradeDS, "StuGradeTable");
        //关闭数据库连接
        StuGradeConn.Close();
        //绑定数据到GridView显示
        this.StuGradeGView.DataSource = StuGradeDS.Tables["StuGradeTable"];
        this.StuGradeGView.DataBind();
    }
}