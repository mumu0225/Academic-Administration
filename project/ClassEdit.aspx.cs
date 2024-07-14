using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClassEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //绑定下拉列表框中的数据
            DropDownListBind();
            //获取网页传递过来的要更新班级记录的班级编码的值
            string ClassID = Request.QueryString["ClassID"];
            //新建一个连接实例
            SqlConnection ClassConn = new SqlConnection();
            //从Web.config文件获取数据库连接字符串
            ClassConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            ClassConn.Open();
            //新建ClassDataSet对象，并将班级表中的数据填充到ClassDataSet对象的表“ClassTable”中
            SqlCommand ClassCmd = new SqlCommand("SELECT * FROM TB_Class WHERE ClassID = " + "'" + ClassID + "'", ClassConn);
            SqlDataAdapter ClassDataAdapter = new SqlDataAdapter(ClassCmd);
            DataSet ClassDataSet = new DataSet();
            ClassDataAdapter.Fill(ClassDataSet, "ClassTable");
            ClassConn.Close();
            //将ClassDataSet中"ClassTable"表的Rows[i][j]，即第i行j列的值分别赋给相应的组件
            this.ClassIDTextBox.Text = ClassDataSet.Tables["ClassTable"].Rows[0][0].ToString();
            this.ClassNameTextBox.Text = ClassDataSet.Tables["ClassTable"].Rows[0][1].ToString();
            this.DeptDDList.SelectedValue = ClassDataSet.Tables["ClassTable"].Rows[0][2].ToString();
            this.TeacherDDList.SelectedValue = ClassDataSet.Tables["ClassTable"].Rows[0][3].ToString();
        }
    }
    private void DropDownListBind()
    {
        //新建一个连接实例
        SqlConnection DDLConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        DDLConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DDLConn.Open();
        //将系部和班级两表中的数据填充到DDLDataSet对象的表“DeptTable”和“TeacherTable”中
        SqlCommand DeptCmd = new SqlCommand("SELECT DeptID,DeptName FROM TB_Dept", DDLConn);
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(DeptCmd);
        SqlCommand TeacherCmd = new SqlCommand("SELECT TeacherID,TeacherName FROM TB_Teacher", DDLConn);
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

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        //构建添加班级记录的UPDATE语句
        string ClassUpdateSQL = "UPDATE TB_Class SET ClassID=";
        ClassUpdateSQL = ClassUpdateSQL + "'" + this.ClassIDTextBox.Text.Trim() + "',";
        ClassUpdateSQL = ClassUpdateSQL + "ClassName='" + this.ClassNameTextBox.Text.Trim() + "',";
        ClassUpdateSQL = ClassUpdateSQL + "DeptID='" + this.DeptDDList.SelectedValue + "',";
        ClassUpdateSQL = ClassUpdateSQL + "TeacherID='" + this.TeacherDDList.SelectedValue + "' ";
        ClassUpdateSQL = ClassUpdateSQL + "WHERE ClassID=" + "'" + Request.QueryString["ClassID"] + "'";
        //创建并打开数据库连接，从Web.config从获取连接字符串
        SqlConnection ClassUpdateConn = new SqlConnection();
        ClassUpdateConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        ClassUpdateConn.Open();
        //执行UPDATE语句
        SqlCommand ClassUpdateCmd = new SqlCommand(ClassUpdateSQL, ClassUpdateConn);
        ClassUpdateCmd.ExecuteNonQuery();
        //关闭数据库连接
        ClassUpdateConn.Close();
        //显示更新成功对话框，并链接到ClassManage.aspx网页
        Response.Write("<script language='javascript'>alert('更新班级记录成功');location.href = 'ClassManage.aspx';</script>");
    }
}