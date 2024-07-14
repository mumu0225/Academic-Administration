using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClassManage : System.Web.UI.Page
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
        SqlCommand DeptCmd = new SqlCommand("SELECT DeptID,DeptName FROM TB_Dept", DDLConn);
        SqlDataAdapter DeptDataAdapter = new SqlDataAdapter(DeptCmd);
        DataSet DDLDataSet = new DataSet();
        DeptDataAdapter.Fill(DDLDataSet, "DeptTable");
        //系部下拉框绑定
        this.DeptDDList.DataTextField = "DeptName";
        this.DeptDDList.DataValueField = "DeptID";
        this.DeptDDList.DataSource = DDLDataSet.Tables["DeptTable"];
        this.DeptDDList.DataBind();
        this.DeptDDList.Items.Insert(0, new ListItem("===所有系部===", "全部"));
        //关闭数据库连接
        DDLConn.Close();
    }
    private void GridViewDataBind()
    {
        SqlConnection ClassConn = new SqlConnection();
        ClassConn.ConnectionString =
        ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        ClassConn.Open();
        SqlCommand ClassSelectCmd = new SqlCommand("SELECT ClassID, ClassName, DeptName, TeacherName FROM TB_Class TC, TB_Dept TD, TB_Teacher TT WHERE TC.DeptID = TD.DeptID AND TC.TeacherID = TT.TeacherID", ClassConn);
        SqlDataAdapter ClassAdapter = new SqlDataAdapter(ClassSelectCmd);
        DataSet ClassDS = new DataSet();
        ClassAdapter.Fill(ClassDS, "ClassTable");
        ClassConn.Close();
        this.ClassGView.DataSource = ClassDS.Tables["ClassTable"];
        this.ClassGView.DataBind();
    }

    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        string QuerySQL = "SELECT ClassID,ClassName,DeptName,TeacherName FROM TB_Class TC,TB_Dept TD, TB_Teacher TT WHERE TC.DeptID = TD.DeptID AND TC.TeacherID = TT.TeacherID"; 
        if (this.DeptDDList.SelectedValue != "全部")
        {
            QuerySQL += " AND TC.DeptID='" + this.DeptDDList.SelectedValue + "'";
        }
        SqlConnection QueryConn = new SqlConnection();
        QueryConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        QueryConn.Open();
        SqlCommand QueryCmd = new SqlCommand(QuerySQL, QueryConn);
        SqlDataAdapter QueryAdapter = new SqlDataAdapter(QueryCmd);
        DataSet QueryDS = new DataSet();
        QueryAdapter.Fill(QueryDS);
        QueryConn.Close();
        this.ClassGView.DataSource = QueryDS.Tables[0].DefaultView;
        this.ClassGView.DataBind();
    }

    protected void ClassGView_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ClassID = this.ClassGView.DataKeys[e.RowIndex].Value.ToString();
        SqlConnection DeleteConn = new SqlConnection();
        DeleteConn.ConnectionString =
        ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        DeleteConn.Open();
        SqlCommand DeleteCmd = new SqlCommand("DELETE FROM TB_Class WHERE  ClassID = '" + ClassID + "'", DeleteConn);
        DeleteCmd.ExecuteNonQuery();
        DeleteConn.Close();
        Response.Write("<script language='javascript'>alert('删除班级记录成功');</script>");
        GridViewDataBind();
    }
}