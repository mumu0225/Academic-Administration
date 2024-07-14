using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StuManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DropDownListBind();
            GridViewDataBind();
        }
    }
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
        //系部下拉框绑定
        this.ClassDDList.DataTextField = "ClassName";
        this.ClassDDList.DataValueField = "ClassID";
        this.ClassDDList.DataSource = DDLDataSet.Tables["ClassTable"];
        this.ClassDDList.DataBind();
        this.ClassDDList.Items.Insert(0, new ListItem("===所有班级===", "全部"));
        //关闭数据库连接
        DDLConn.Close();
    }
    private void GridViewDataBind()
    {
        SqlConnection ClassConn = new SqlConnection();
        ClassConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        ClassConn.Open();
        SqlCommand StuSelectCmd = new SqlCommand("SELECT StuID, StuName, EnrollYear, GradYear, DeptName, ClassName, Sex, Birthday, StuAddress, ZipCode FROM TB_Student TS, TB_Dept TD, TB_Class TC WHERE TS.DeptID = TD.DeptID AND TS.ClassID = TC.ClassID", ClassConn);
        SqlDataAdapter ClassAdapter = new SqlDataAdapter(StuSelectCmd);
        DataSet StuDS = new DataSet();
        ClassAdapter.Fill(StuDS, "StuTable");
        // 遍历数据表中的每一行，对"Sex"列的值进行转换
        foreach (DataRow row in StuDS.Tables["StuTable"].Rows)
        {
            if (row["Sex"].ToString().Equals("F", StringComparison.OrdinalIgnoreCase))
            {
                row["Sex"] = "女";
            }
            else if (row["Sex"].ToString().Equals("M", StringComparison.OrdinalIgnoreCase))
            {
                row["Sex"] = "男";
            }
        }
        ClassConn.Close();
        this.StuGView.DataSource = StuDS.Tables["StuTable"];
        this.StuGView.DataBind();
    }

    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        string QuerySQL = "SELECT StuID, StuName, EnrollYear, GradYear, DeptName, ClassName, Sex, Birthday, StuAddress, ZipCode FROM TB_Student TS, TB_Dept TD, TB_Class TC WHERE TS.DeptID = TD.DeptID AND TS.ClassID = TC.ClassID";
        if (this.ClassDDList.SelectedValue != "全部")
        {
            QuerySQL += " AND TS.ClassID='" + this.ClassDDList.SelectedValue + "'";
        }
        SqlConnection QueryConn = new SqlConnection();
        QueryConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        QueryConn.Open();
        SqlCommand QueryCmd = new SqlCommand(QuerySQL, QueryConn);
        SqlDataAdapter QueryAdapter = new SqlDataAdapter(QueryCmd);
        DataSet QueryDS = new DataSet();
        QueryAdapter.Fill(QueryDS);
        QueryConn.Close();
        this.StuGView.DataSource = QueryDS.Tables[0].DefaultView;
        this.StuGView.DataBind();
    }

    protected void StuGView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string StuID = this.StuGView.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection DeleteConn = new SqlConnection();
        DeleteConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DeleteConn.Open();
        SqlCommand DeleteCmd = new SqlCommand("DELETE FROM TB_Student WHERE  StuID = '" + StuID + "'", DeleteConn);
        DeleteCmd.ExecuteNonQuery();
        DeleteConn.Close();
        Response.Write("<script language='javascript'>alert('删除学生记录成功');</script>");
        GridViewDataBind();
    }
}