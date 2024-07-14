using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class SelectCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //建立数据库连接，从Web.config文件获取数据库连接字符串
            SqlConnection CourseClassConn = new SqlConnection();
            CourseClassConn.ConnectionString =
            ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            CourseClassConn.Open();
            //调用存储过程
            SqlCommand CourseClassCmd = new SqlCommand("SP_StuCourseClass", CourseClassConn);
            //说明SqlCommand类型是个存储过程 
            CourseClassCmd.CommandType = CommandType.StoredProcedure;
            //添加存储过程的参数,从全局Session变量获取学号值
            CourseClassCmd.Parameters.Add("@StuID", SqlDbType.Char, 8).Value = Session["StuID"].ToString();
            SqlDataAdapter StuGradeAdapter = new SqlDataAdapter(CourseClassCmd);
            //将SqlDataAdapter对象中的数据填充到DataSet对象的表"StuSelectCourseTable"
            DataSet StuCourseClassDS = new DataSet();
            StuGradeAdapter.Fill(StuCourseClassDS, "StuSelectCourseTable");
            //关闭数据库连接
            CourseClassConn.Close();
            //绑定数据到GridView显示
            this.CourseClassGView.DataSource = StuCourseClassDS.Tables["StuSelectCourseTable"];
            this.CourseClassGView.DataBind();
        }
    }

    protected void StuSelectBtn_Click(object sender, EventArgs e)
    {
        string CourseClassIDs; //定义存放勾选课程班编码的字符串变量
        CourseClassIDs = "";   //初始化字符串变量
        //通过循环遍历所有课程班记录，被勾选的将其课程班编码放入字符串变量中
        for (int i = 0; i < this.CourseClassGView.Rows.Count; i++)
        {
            CheckBox CheckedBox = (CheckBox)this.CourseClassGView.Rows[i].FindControl("CBoxCourseClass");
            if (CheckedBox.Checked)
            {
                if (CourseClassIDs == "")
                    CourseClassIDs = this.CourseClassGView.DataKeys[i].Value.ToString();
                else
                    CourseClassIDs = CourseClassIDs + "," + this.CourseClassGView.DataKeys[i].Value.ToString();
            }
        }
        if (CourseClassIDs == "")
        {
            //没有勾选课程班，则弹出提示信息框
            Response.Write("<SCRIPT language='javascript'>alert('请先选择课程！'); </ SCRIPT > ");
        }
        else
        {
            //Response.Write(CourseClassIDs);  //测试显示选中的课程班编码
            //调用SQL Server中的存储过程进行课程选修
            SqlConnection SelectCourseConn = new SqlConnection();
            SelectCourseConn.ConnectionString =
            ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            SelectCourseConn.Open();
            SqlCommand SelectCourseCmd = new SqlCommand("SP_SelectCourse", SelectCourseConn);
            //说明SqlCommand类型是个存储过程
            SelectCourseCmd.CommandType = CommandType.StoredProcedure;
            //添加存储过程的参数,从全局Session变量获取学号，从CourseClassIDs得到选中的课程班信息
            SelectCourseCmd.Parameters.Add("@StuID", SqlDbType.Char, 8).Value =
            Session["StuID"].ToString();
            SelectCourseCmd.Parameters.Add("@CourseClassIDs", SqlDbType.VarChar, 100).Value = CourseClassIDs;
            SelectCourseCmd.ExecuteNonQuery();  //执行存储过程
            SelectCourseConn.Close();    //关闭数据库连接
            Response.Write("<SCRIPT language='javascript'>alert('课程选修成功！'); </SCRIPT>");
            Response.Redirect("ReturnCourse.aspx");
        }
    }
}