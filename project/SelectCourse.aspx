<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectCourse.aspx.cs" Inherits="SelectCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 474px;
        }
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1" style="text-align: center">
        网上课程选修<br />
        <br />
        <asp:GridView ID="CourseClassGView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" HorizontalAlign="Center" DataKeyNames="CourseClassID">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:TemplateField HeaderText="勾选">
                    <EditItemTemplate>
                        <asp:CheckBox ID="CBoxCourseClass" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CBoxCourseClass" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CourseClassID" HeaderText="课程班编码" Visible="False" />
                <asp:BoundField HeaderText="课程名称" DataField="CourseName" />
                <asp:BoundField HeaderText="任课教师" DataField="TeacherName" />
                <asp:BoundField HeaderText="教学地点" DataField="TeachingPlace" />
                <asp:BoundField HeaderText="教学时间" DataField="TeachingTime" />
                <asp:BoundField HeaderText="允许选修数" DataField="MaxNumber" />
                <asp:BoundField HeaderText="已选数" DataField="SelectedNumber" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#0000A9" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#000065" />
        </asp:GridView>
        <br />
        <asp:Button ID="StuSelectBtn" runat="server" OnClick="StuSelectBtn_Click" Text="确定" Width="80px" />
        <br />
        <a href="ReturnCourse.aspx">课程退选</a><br />
    </form>
</body>
</html>
