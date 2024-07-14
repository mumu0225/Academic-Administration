using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownListBind();
        }
    }
    private void DropDownListBind()
    {
        //新建一个连接实例
        SqlConnection DDLConn = new SqlConnection();
        //从Web.config文件获取数据库连接字符串
        DDLConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DDLConn.Open();
        //将两表中数据填充到DDLDataSet对象的表“DeptTable”和“ClassTable”中
        SqlCommand DeptCmd = new SqlCommand("SELECT DeptID,DeptName FROM TB_Dept", DDLConn);
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(DeptCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        //系部下拉框绑定 
        this.DeptDDList.DataTextField = "DeptName";
        this.DeptDDList.DataValueField = "DeptID";
        this.DeptDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.DeptDDList.DataBind();
        //关闭数据库连接
        DDLConn.Close();
        this.DeptDDList.Items.Insert(0, new ListItem("===请选择系部===", null));
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

    protected void InsertBtn_Click(object sender, EventArgs e)
    {
        if(this.StuIDTextBox.Text==""||this.StuNameTextBox.Text=="")
        {
            Response.Write("<script language='javascript'>alert('学号、姓名不得为空！');</script>");
        }
        else
        {
            //构建添加学生记录的INSERT INTO语句 
            string StuInsertSQL = "INSERT INTO TB_Student(StuID, StuName, EnrollYear, GradYear, DeptID, ClassID, Sex, Birthday, SPassword, StuAddress, ZipCode) VALUES(";
            StuInsertSQL = StuInsertSQL + "'" + this.StuIDTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.StuNameTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.EnrollYearTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.GradYearTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.DeptDDList.SelectedValue + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.ClassDDList.SelectedValue + "',";
            if (this.RadioButton1.Checked)
            {
                StuInsertSQL = StuInsertSQL + "'M',";
            }
            else if(this.RadioButton2.Checked)
            {
                StuInsertSQL = StuInsertSQL + "'F',";
            }
            StuInsertSQL = StuInsertSQL + "'" + this.Calendar1.SelectedDate.ToString("yyyy-MM-dd 00:00:00") + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.PassTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.AddressTextBox.Text.Trim() + "',";
            StuInsertSQL = StuInsertSQL + "'" + this.ZipCodeTextBox.Text.Trim() + "')";
            //建立一个数据库连接，并从Web.config文件中获取连接字符串，并打开连接
            SqlConnection StuInsertConn = new SqlConnection();
            StuInsertConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            StuInsertConn.Open();
            //执行添加班级记录的SQL语句命令
            SqlCommand StuInsertCmd = new SqlCommand(StuInsertSQL, StuInsertConn);
            StuInsertCmd.ExecuteNonQuery();
            //关闭数据库连接
            StuInsertConn.Close();
            //班级记录添加成功后弹出成功对话框，并链接到新网页
            Response.Write("<script language='javascript'>alert('新增学生记录成功');location.href='StuManage.aspx';</script>");
        }
    }
}