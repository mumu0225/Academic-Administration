using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuClassGradeQuery : System.Web.UI.Page
{
    private void DropDownListBind()
    {
        //新建一个连接实例
        SqlConnection DDLConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        DDLConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DDLConn.Open();
        SqlCommand ClassCmd = new SqlCommand("SELECT ClassID,ClassName FROM TB_Class", DDLConn);
        SqlDataAdapter ClassDataAdapter = new SqlDataAdapter(ClassCmd);
        DataSet DDLDataSet = new DataSet();
        ClassDataAdapter.Fill(DDLDataSet, "ClassTable");

        this.CourseClassDDList.DataTextField = "ClassName";
        this.CourseClassDDList.DataValueField = "ClassID";
        this.CourseClassDDList.DataSource = DDLDataSet.Tables["ClassTable"];
        this.CourseClassDDList.DataBind();
        this.CourseClassDDList.Items.Insert(0, new ListItem("===所有班级===", "全部"));

        DDLConn.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownListBind();
        }
    }

    protected void CourseClassDDList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //新建一个连接实例
        SqlConnection StuConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        StuConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuConn.Open();
        //调用存储过程“SP_ClassStuQuery”
        SqlCommand StuCmd = new SqlCommand("SP_ClassStuQuery", StuConn);
        //说明SqlCommand类型是个存储过程
        StuCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        StuCmd.Parameters.Add("@ClassID", SqlDbType.Char, 6).Value = this.CourseClassDDList.SelectedValue.ToString();
        //新建DDLDataSet对象，并将课程班表中的数据填充到DDLDataSet对象的表“CourseClassTable”中
        SqlDataAdapter StuDataAdapter = new SqlDataAdapter(StuCmd);
        DataSet DDLDataSet = new DataSet();
        StuDataAdapter.Fill(DDLDataSet, "StuTable");
        //课程班下拉框绑定
        this.StuNameDDList.DataTextField = "StuName";
        this.StuNameDDList.DataValueField = "StuID";
        this.StuNameDDList.DataSource = DDLDataSet.Tables["StuTable"];
        this.StuNameDDList.DataBind();
        //关闭数据库连接
        StuConn.Close();
    }

    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        //新建一个连接实例
        SqlConnection StuGradeConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        StuGradeConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuGradeConn.Open();
        //调用存储过程“SP_StuGradeQuery”
        SqlCommand StuGradeCmd = new SqlCommand("SP_StuGradeQuery", StuGradeConn);
        //说明SqlCommand类型是个存储过程
        StuGradeCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        StuGradeCmd.Parameters.Add("@StuID", SqlDbType.Char, 8).Value = this.StuNameDDList.SelectedValue.ToString();
        //将成绩表中的数据填充到DDLDataSet对象的表“CCGradeTable”中
        SqlDataAdapter StuGradeAdapter = new SqlDataAdapter(StuGradeCmd);
        DataSet QueryDS = new DataSet();
        StuGradeAdapter.Fill(QueryDS, "StuGradeTable");
        //GradeGView数据绑定
        this.GradeGView.DataSource = QueryDS.Tables["StuGradeTable"];
        this.GradeGView.DataBind();
        //关闭数据库连接
        StuGradeConn.Close();
    }
}