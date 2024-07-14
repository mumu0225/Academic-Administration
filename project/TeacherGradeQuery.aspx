<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherGradeQuery.aspx.cs" Inherits="TeacherGradeQuery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 445px;
            width: 1562px;
        }
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            margin-left: 0px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" class="auto-style1">
        教师课程班成绩查询<br />
        <br />
&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="CourseClassDDList" runat="server" Width="300px" AutoPostBack="True" CssClass="auto-style2">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="QueryBtn" runat="server" OnClick="QueryBtn_Click" Text="确定" Width="80px" />
        <br />
        <br />
        <asp:GridView ID="GradeGView" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="StuID" HeaderText="学号" />
                <asp:BoundField DataField="StuName" HeaderText="姓名" />
                <asp:BoundField DataField="CommonScore" HeaderText="平时成绩" />
                <asp:BoundField DataField="MiddleScore" HeaderText="期中成绩" />
                <asp:BoundField DataField="LastScore" HeaderText="期末成绩" />
                <asp:BoundField DataField="TotalScore" HeaderText="总评成绩" />
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
    </form>
</body>
</html>
