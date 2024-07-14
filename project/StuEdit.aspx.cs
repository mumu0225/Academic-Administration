using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //绑定下拉列表框中的数据
            DropDownListBind();
            //获取网页传递过来的要更新班级记录的学号的值
            string StuID = Request.QueryString["StuID"];
            //新建一个连接实例
            SqlConnection StuConn = new SqlConnection();
            //从Web.config文件获取数据库连接字符串
            StuConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            StuConn.Open();
            //新建StuDataSet对象，并将班级表中的数据填充到StuDataSet对象的表“StuTable”中
            SqlCommand StuCmd = new SqlCommand("SELECT * FROM TB_Student WHERE StuID = " + "'" + StuID + "'", StuConn);
            SqlDataAdapter StuDataAdapter = new SqlDataAdapter(StuCmd);
            DataSet StuDataSet = new DataSet();
            StuDataAdapter.Fill(StuDataSet, "StuTable");
            StuConn.Close();
            //将StuDataSet中"ClassTable"表的Rows[i][j]，即第i行j列的值分别赋给相应的组件
            this.StuIDTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][0].ToString();
            this.StuNameTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][1].ToString();
            this.EnrollYearTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][2].ToString();
            this.GradYearTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][3].ToString();
            this.DeptDDList.SelectedValue = StuDataSet.Tables["StuTable"].Rows[0][4].ToString();
            this.ClassDDList.SelectedValue = StuDataSet.Tables["StuTable"].Rows[0][5].ToString();
            if(StuDataSet.Tables["StuTable"].Rows[0][6].ToString().Equals("M"))
            {
                RadioButton1.Checked = true;
            }
            else
            {
                RadioButton2.Checked = true;
            }
            this.Calendar1.SelectedDate = (DateTime)StuDataSet.Tables["StuTable"].Rows[0][7];
            this.PassTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][8].ToString();
            this.AddressTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][9].ToString();
            this.ZipCodeTextBox.Text = StuDataSet.Tables["StuTable"].Rows[0][10].ToString();
            
        }
    }
    private void DropDownListBind()
    {
        //新建一个连接实例
        SqlConnection DDLConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        DDLConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DDLConn.Open();
        SqlCommand DeptCmd = new SqlCommand("SELECT DeptID,DeptName FROM TB_Dept", DDLConn);
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(DeptCmd);
        SqlCommand ClassCmd = new SqlCommand("SELECT ClassID,ClassName FROM TB_Class", DDLConn);
        SqlDataAdapter TeacherDataAdapter = new SqlDataAdapter(ClassCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        TeacherDataAdapter.Fill(DDLDataSet, "Class  Table");
        //系部下拉框绑定 
        this.DeptDDList.DataTextField = "DeptName";
        this.DeptDDList.DataValueField = "DeptID";
        this.DeptDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.DeptDDList.DataBind();
        //班主任下拉框绑定
        this.ClassDDList.DataTextField = "ClassName";
        this.ClassDDList.DataValueField = "ClassID";
        this.ClassDDList.DataSource = DDLDataSet.Tables["ClassTable"];
        this.ClassDDList.DataBind();
        //关闭数据库连接
        DDLConn.Close();
    }

    protected void DeptDDList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //新建一个连接实例
        SqlConnection StuConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        StuConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuConn.Open();
        //调用存储过程“SP_DeptClassQuery”
        SqlCommand StuCmd = new SqlCommand("SP_DeptClassQuery", StuConn);
        //说明SqlCommand类型是个存储过程
        StuCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        StuCmd.Parameters.Add("@DeptID", SqlDbType.Char, 6).Value = this.DeptDDList.SelectedValue.ToString();
        //新建DDLDataSet对象，并将系部表中的数据填充到DDLDataSet对象的表“DeptTable”中
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(StuCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        //课程班下拉框绑定
        this.ClassDDList.DataTextField = "ClassName";
        this.ClassDDList.DataValueField = "ClassID";
        this.ClassDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.ClassDDList.DataBind();
        //关闭数据库连接
        StuConn.Close();
    }

    protected void UpdateBtn_Click(object sender, EventArgs e)
    {
        if (this.StuIDTextBox.Text == "" || this.StuNameTextBox.Text == "")
        {
            Response.Write("<script language='javascript'>alert('学号、姓名不得为空！');</script>");
        }
        else
        {
            //构建添加学生记录的UPDATE语句
            string StuUpdateSQL = "UPDATE TB_Student SET StuID=";
            StuUpdateSQL = StuUpdateSQL + "'" + this.StuIDTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "StuName='" + this.StuNameTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "EnrollYear='" + this.EnrollYearTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "GradYear='" + this.GradYearTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "DeptID='" + this.DeptDDList.SelectedValue + "',";
            StuUpdateSQL = StuUpdateSQL + "ClassID='" + this.ClassDDList.SelectedValue + "',";
            if (RadioButton1.Checked)
            {
                StuUpdateSQL = StuUpdateSQL + "Sex='M',";
            }
            else
            {
                StuUpdateSQL = StuUpdateSQL + "Sex='F',";
            }
            StuUpdateSQL = StuUpdateSQL + "Birthday='" + this.Calendar1.SelectedDate.ToString("yyyy-MM-dd 00:00:00") + "',";
            StuUpdateSQL = StuUpdateSQL + "SPassword='" + this.PassTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "StuAddress='" + this.AddressTextBox.Text.Trim() + "',";
            StuUpdateSQL = StuUpdateSQL + "ZipCode='" + this.ZipCodeTextBox.Text.Trim() + "'";
            StuUpdateSQL = StuUpdateSQL + "WHERE StuID=" + "'" + Request.QueryString["StuID"] + "'";
            //创建并打开数据库连接，从Web.config从获取连接字符串
            SqlConnection StuUpdateConn = new SqlConnection();
            StuUpdateConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            StuUpdateConn.Open();
            //执行UPDATE语句
            SqlCommand ClassUpdateCmd = new SqlCommand(StuUpdateSQL, StuUpdateConn);
            ClassUpdateCmd.ExecuteNonQuery();
            //关闭数据库连接
            StuUpdateConn.Close();
            //显示更新成功对话框，并链接到StuManage.aspx网页
            Response.Write("<script language='javascript'>alert('更新学生记录成功');location.href = 'StuManage.aspx';</script>");
        }
    }

    protected void DeptDDList_Load(object sender, EventArgs e)
    {
        //新建一个连接实例
        SqlConnection StuConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        StuConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        StuConn.Open();
        //调用存储过程“SP_DeptClassQuery”
        SqlCommand StuCmd = new SqlCommand("SP_DeptClassQuery", StuConn);
        //说明SqlCommand类型是个存储过程
        StuCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        StuCmd.Parameters.Add("@DeptID", SqlDbType.Char, 6).Value = this.DeptDDList.SelectedValue.ToString();
        //新建DDLDataSet对象，并将系部表中的数据填充到DDLDataSet对象的表“DeptTable”中
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(StuCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        //课程班下拉框绑定
        this.ClassDDList.DataTextField = "ClassName";
        this.ClassDDList.DataValueField = "ClassID";
        this.ClassDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.ClassDDList.DataBind();
        //关闭数据库连接
        StuConn.Close();
    }
}