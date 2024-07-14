using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherQueryGrade : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TeacherIDNameLabel.Text = "<工号：" + Session["TeacherID"].ToString() + "  姓名：" + Session["TeacherName"].ToString() + ">";
        //建立数据库连接，从Web.config文件获取数据库连接字符串
        SqlConnection StuGradeConn = new SqlConnection();
        StuGradeConn.ConnectionString =
        ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuGradeConn.Open();
        //创建查询SQL语句，并用SqlDataAdapter对象获取数据 
        SqlCommand StuGradeCmd = new SqlCommand("SELECT cc.CourseClassID 课程班ID, c.CourseName 课程名, ROUND(AVG(g.TotalScore),2) 平均成绩 FROM TB_CourseClass cc INNER JOIN TB_Course c ON cc.CourseID = c.CourseID INNER JOIN TB_Grade g ON cc.CourseClassID = g.CourseClassID WHERE cc.TeacherID = '" + Session["TeacherID"].ToString() + "'" + "GROUP BY cc.CourseClassID, c.CourseName", StuGradeConn);
        SqlDataAdapter StuGradeAdapter = new SqlDataAdapter(StuGradeCmd);
        //将SqlDataAdapter对象中的数据填充到DataSet对象的表"StuGradeTable"中
        DataSet StuGradeDS = new DataSet();
        StuGradeAdapter.Fill(StuGradeDS, "StuGradeTable");
        //关闭数据库连接
        StuGradeConn.Close();
        //绑定数据到GridView显示
        this.TeacherGradeGView.DataSource = StuGradeDS.Tables["StuGradeTable"];
        this.TeacherGradeGView.DataBind();
    }
}