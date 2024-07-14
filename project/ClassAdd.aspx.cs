using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClassAdd : System.Web.UI.Page
{
    private void DropDownListBind()
    {
        //新建一个连接实例
        SqlConnection DDLConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        DDLConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DDLConn.Open();
        //将两表中数据填充到DDLDataSet对象的表“DeptTable”和“TeacherTable”中
        SqlCommand DeptCmd = new SqlCommand("SELECT DeptID,DeptName FROM TB_Dept", DDLConn);
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(DeptCmd);
        SqlCommand TeacherCmd = new SqlCommand("SELECT TeacherID, TeacherName FROM TB_Teacher", DDLConn);
        SqlDataAdapter TeacherDataAdapter = new SqlDataAdapter(TeacherCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        TeacherDataAdapter.Fill(DDLDataSet, "TeacherTable");
        //系部下拉框绑定 
        this.DeptDDList.DataTextField = "DeptName";
        this.DeptDDList.DataValueField = "DeptID";
        this.DeptDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.DeptDDList.DataBind();
        //班主任下拉框绑定
        this.TeacherDDList.DataTextField = "TeacherName";
        this.TeacherDDList.DataValueField = "TeacherID";
        this.TeacherDDList.DataSource = DDLDataSet.Tables["TeacherTable"];
        this.TeacherDDList.DataBind();
        //关闭数据库连接
        DDLConn.Close();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownListBind();
        }
    }

    protected void InsertBtn_Click(object sender, EventArgs e)
    {
        //构建添加班级记录的INSERT INTO语句 
        string ClassInsertSQL = "INSERT INTO TB_Class(ClassID, ClassName, DeptID, TeacherID) VALUES(";
        ClassInsertSQL = ClassInsertSQL + "'" + this.ClassIDTextBox.Text.Trim() + "',";
        ClassInsertSQL = ClassInsertSQL + "'" + this.ClassNameTextBox.Text.Trim() + "',";
        ClassInsertSQL = ClassInsertSQL + "'" + this.DeptDDList.SelectedValue + "',";
        ClassInsertSQL = ClassInsertSQL + "'" + this.TeacherDDList.SelectedValue + "')";
        //建立一个数据库连接，并从Web.config文件中获取连接字符串，并打开连接
        SqlConnection ClassInsertConn = new SqlConnection();
        ClassInsertConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        ClassInsertConn.Open();
        //执行添加班级记录的SQL语句命令
        SqlCommand ClassInsertCmd = new SqlCommand(ClassInsertSQL, ClassInsertConn);
        ClassInsertCmd.ExecuteNonQuery();
        //关闭数据库连接
        ClassInsertConn.Close();
        //班级记录添加成功后弹出成功对话框，并链接到新网页
        Response.Write("<script language='javascript'>alert('新增班级记录成功');location.href='ClassManage.aspx';</script>");
    }
}