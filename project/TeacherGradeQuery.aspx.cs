using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeacherGradeQuery : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            String TeacherID = Session["TeacherID"].ToString();
            //新建一个连接实例
            SqlConnection CourseClassConn = new SqlConnection();
            //从Web.config文件获取数据库连接字符串
            CourseClassConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            CourseClassConn.Open();
            //调用存储过程“SP_CourseClassQuery”
            SqlCommand CourseClassCmd = new SqlCommand("SP_CourseClassQuery", CourseClassConn);
            //说明SqlCommand类型是个存储过程
            CourseClassCmd.CommandType = CommandType.StoredProcedure;
            //添加存储过程需要的参数
            CourseClassCmd.Parameters.Add("@TeacherID", SqlDbType.Char, 6).Value = TeacherID;
            //新建DDLDataSet对象，并将课程班表中的数据填充到DDLDataSet对象的表“CourseClassTable”中
            SqlDataAdapter CourseClassDataAdapter = new SqlDataAdapter(CourseClassCmd);
            DataSet DDLDataSet = new DataSet();
            CourseClassDataAdapter.Fill(DDLDataSet, "CourseClassTable");
            //课程班下拉框绑定
            this.CourseClassDDList.DataTextField = "CCName";
            this.CourseClassDDList.DataValueField = "CourseClassID";
            this.CourseClassDDList.DataSource = DDLDataSet.Tables["CourseClassTable"];
            this.CourseClassDDList.DataBind();
            //关闭数据库连接
            CourseClassConn.Close();
        }
    }

    protected void TeacherDDList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        //新建一个连接实例
        SqlConnection CourseClassGradeConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        CourseClassGradeConn.ConnectionString =
        ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        CourseClassGradeConn.Open();
        //调用存储过程“SP_CourseClassGradeQuery”
        SqlCommand CourseClassCmd = new SqlCommand("SP_CourseClassGradeQuery", CourseClassGradeConn);
        //说明SqlCommand类型是个存储过程
        CourseClassCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        CourseClassCmd.Parameters.Add("@CourseClassID", SqlDbType.Char, 10).Value = this.CourseClassDDList.SelectedValue.ToString();
        //将成绩表中的数据填充到DDLDataSet对象的表“CCGradeTable”中
        SqlDataAdapter CourseClassDataAdapter = new SqlDataAdapter(CourseClassCmd);
        DataSet QueryDS = new DataSet();
        CourseClassDataAdapter.Fill(QueryDS, "CCGradeTable");
        //GradeGView数据绑定
        this.GradeGView.DataSource = QueryDS.Tables["CCGradeTable"];
        this.GradeGView.DataBind();
        //关闭数据库连接
        CourseClassGradeConn.Close();
    }
}