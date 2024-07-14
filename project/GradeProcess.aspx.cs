using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GradeProcess : System.Web.UI.Page
{
    String teacherId;
    protected void Page_Load(object sender, EventArgs e)
    {
        teacherId = Session["TeacherID"].ToString();
        if (!Page.IsPostBack)
        {
            DropDownListBind();
        }
        
    }
    private void DropDownListBind()
    {
        SqlConnection CourseClassConn = new SqlConnection();
        CourseClassConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        CourseClassConn.Open();
        //调用存储过程“SP_CourseClassQuery”
        SqlCommand CourseClassCmd = new SqlCommand("SP_CourseClassQuery", CourseClassConn);
        CourseClassCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程需要的参数
        CourseClassCmd.Parameters.Add("@TeacherID", SqlDbType.Char, 6).Value = teacherId;
        SqlDataAdapter CourseClassDataAdapter = new SqlDataAdapter(CourseClassCmd);
        DataSet DDLDataSet = new DataSet();
        CourseClassDataAdapter.Fill(DDLDataSet, "CourseClassTable");
        //课程班下拉框绑定
        this.CourseClassDDList.DataTextField = "CCName";
        this.CourseClassDDList.DataValueField = "CourseClassID";
        this.CourseClassDDList.DataSource = DDLDataSet.Tables["CourseClassTable"];
        this.CourseClassDDList.DataBind();
        //关闭数据库连接，显示查询按钮，隐藏GradeGView和成绩处理按钮
        CourseClassConn.Close();
        QueryBtn.Enabled = true;
        GradeGView.Visible = false;
        GradeProcBtn.Visible = false;
    }

    protected void QueryBtn_Click(object sender, EventArgs e)
    {
        GradeGView.Visible = true;
        GradeGViewDataBind();
        GradeProcBtn.Visible = true;
    }

    private void GradeGViewDataBind()
    {
        //创建字符串变量CourseClassID，用以获取某个教师的课程班编码值
        string CourseClassID = this.CourseClassDDList.SelectedValue.ToString();
        //新建一个连接实例
        SqlConnection CCGradeProcConn = new SqlConnection();
        CCGradeProcConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        CCGradeProcConn.Open();
        //如果表中还没有对应课程班成绩记录，生成空白成绩单
        SqlCommand GradeQueryCmd = new SqlCommand("SELECT * FROM TB_Grade WHERE CourseClassID = " + "'" + CourseClassID + "'", CCGradeProcConn);
        SqlDataReader GradeDataReader = GradeQueryCmd.ExecuteReader();
        GradeDataReader.Read();
        if (!GradeDataReader.HasRows)
        {
            GradeDataReader.Close();
            //调用存储过程“SP_MakeGradeSheet”生成空白成绩表单
            SqlCommand MakeGradeSheetCmd = new SqlCommand("SP_MakeGradeSheet", CCGradeProcConn);
            MakeGradeSheetCmd.CommandType = CommandType.StoredProcedure;
            MakeGradeSheetCmd.Parameters.Add("@CourseClassID", SqlDbType.Char, 10).Value = CourseClassID;
            MakeGradeSheetCmd.ExecuteNonQuery();
        }
        else
            GradeDataReader.Close();
        //调用存储过程"SP_CourseClassGradeQuery"进行课程班成绩查询
        SqlCommand CourseClassCmd = new SqlCommand("SP_CourseClassGradeQuery", CCGradeProcConn);
        CourseClassCmd.CommandType = CommandType.StoredProcedure;
        CourseClassCmd.Parameters.Add("@CourseClassID", SqlDbType.Char, 10).Value = CourseClassID;
        //新建QueryDS对象，并将成绩表中的数据填充到DDLDataSet对象的表“CCGradeTable”中
        SqlDataAdapter CourseClassDataAdapter = new SqlDataAdapter(CourseClassCmd);
        DataSet QueryDS = new DataSet();
        CourseClassDataAdapter.Fill(QueryDS, "CCGradeTable");
        //在GradeGView中将课程班成绩绑定
        this.GradeGView.DataSource = QueryDS.Tables["CCGradeTable"];
        this.GradeGView.DataBind();
        for (int i = 0; i < this.GradeGView.Rows.Count; i++)
        {
            //定义三个TextBox和一个CheckBox对象，用来处理三个成绩和成绩锁定标志
            TextBox CTextBox = (TextBox)this.GradeGView.Rows[i].FindControl("TBoxCommonScore");
            TextBox MTextBox = (TextBox)this.GradeGView.Rows[i].FindControl("TBoxMiddleScore");
            TextBox LTextBox = (TextBox)this.GradeGView.Rows[i].FindControl("TBoxLastScore");
            CheckBox LockCheckBox = (CheckBox)this.GradeGView.Rows[i].FindControl("CBoxLockFlag");
            CTextBox.Text = QueryDS.Tables["CCGradeTable"].Rows[i]["CommonScore"].ToString();
            MTextBox.Text = QueryDS.Tables["CCGradeTable"].Rows[i]["MiddleScore"].ToString();
            LTextBox.Text = QueryDS.Tables["CCGradeTable"].Rows[i]["LastScore"].ToString();
            //如果锁定标志位“L”，则成绩行和成绩处理按钮为非激活
            if (QueryDS.Tables["CCGradeTable"].Rows[i]["LockFlag"].ToString() == "L")
            {
                this.GradeGView.Rows[i].Enabled = false;
                LockCheckBox.Checked = true;
                GradeProcBtn.Enabled = false;
            }
            else
                GradeProcBtn.Enabled = true;
        }
        //关闭数据库连接
        CCGradeProcConn.Close();
    }

    protected void GradeProcBtn_Click(object sender, EventArgs e)
    {
        int intGradeSeedID;
        float fltCommonScore, fltMiddleScore, fltLastScore;
        //如果GradeGView中有记录，则进行数据更新处理
        if (this.GradeGView.Rows.Count > 0)
        {
            SqlConnection GradeUpdateConn = new SqlConnection();
            GradeUpdateConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            GradeUpdateConn.Open();
            for (int i = 0; i < this.GradeGView.Rows.Count; i++)
            {
                //4个变量，分别从GradeGView获取成绩记录标识种子、平时、期中和期末成绩的值
                intGradeSeedID = Convert.ToInt32(this.GradeGView.DataKeys[i].Value);
                fltCommonScore = Convert.ToSingle(((TextBox)this.GradeGView.Rows[i].FindControl("TBoxCommonScore")).Text);
                fltMiddleScore = Convert.ToSingle(((TextBox)this.GradeGView.Rows[i].FindControl("TBoxMiddleScore")).Text);
                fltLastScore = Convert.ToSingle(((TextBox)this.GradeGView.Rows[i].FindControl("TBoxLastScore")).Text);
                //构建更新成绩记录的UPDATE语句
                string GradeUpdateSQL = "UPDATE TB_Grade SET CommonScore=";
                GradeUpdateSQL = GradeUpdateSQL + fltCommonScore;
                GradeUpdateSQL = GradeUpdateSQL + ",MiddleScore=" + fltMiddleScore;
                GradeUpdateSQL = GradeUpdateSQL + ",LastScore=" + fltLastScore;
                GradeUpdateSQL = GradeUpdateSQL + " WHERE GradeSeedID=" + intGradeSeedID;
                //Response.Write(GradeUpdateSQL);
                //将录入的课程班成绩更新到表“TB_Grade”中
                SqlCommand GradeUpdateCmd = new SqlCommand(GradeUpdateSQL, GradeUpdateConn);
                GradeUpdateCmd.ExecuteNonQuery();
            }
            //调用方法TotalScoreProc()进行总评成绩处理
            TotalScoreProc();
            //关闭数据库连接
            GradeUpdateConn.Close();
            //调用方法GradeGViewDataBind()进行数据重新绑定，并显示成绩处理成功对话框
            GradeGViewDataBind();
            Response.Write("<SCRIPT language='javascript'>alert('成绩处理成功并刷新！'); </SCRIPT>");
        }
    }

    private void TotalScoreProc()
    {
        SqlConnection GradeProcConn = new SqlConnection();
        GradeProcConn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
        GradeProcConn.Open();
        //调用存储过程SP_GradeProc
        SqlCommand SelectCourseCmd = new SqlCommand("SP_GradeProc", GradeProcConn);
        //说明SqlCommand类型是个存储过程
        SelectCourseCmd.CommandType = CommandType.StoredProcedure;
        //添加存储过程的参数,CourseClassID
        SelectCourseCmd.Parameters.Add("@CourseClassID", SqlDbType.Char, 10).Value = this.CourseClassDDList.SelectedValue.ToString();
        SelectCourseCmd.ExecuteNonQuery();  //执行存储过程
        GradeProcConn.Close();    //关闭数据库连接
    }

    protected void CourseClassDDList_TextChanged(object sender, EventArgs e)
    {
        GradeGView.Visible = false;
        GradeProcBtn.Visible = false;
    }
}