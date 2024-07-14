<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherQueryGrade.aspx.cs" Inherits="TeacherQueryGrade" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 399px;
        }
        .auto-style1 {}
        .auto-style2 {
            font-size: x-large;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" class="auto-style1">
        <span class="auto-style2">教师成绩查询</span><br />
        <br />
        <asp:Label ID="TeacherIDNameLabel" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="TeacherGradeGView" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#DCDCDC" />
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
