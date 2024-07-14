<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeacherLogin.aspx.cs" Inherits="TeacherLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 383px;
            width: 1280px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" aria-busy="True" style="text-align: center">
        教师登录模块<br />
        <br />
        <asp:Panel ID="loginPanel" runat="server" Height="142px">
            <asp:Label ID="Label1" runat="server" Text="工号："></asp:Label>
            <asp:TextBox ID="TeacherIDTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="密码："></asp:Label>
            <asp:TextBox ID="PassTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="LoginSubmitBtn" runat="server" Text="提交" OnClick="LoginSubmitBtn_Click" />
            &nbsp;
            <asp:Button ID="LoginRestBtn" runat="server" Text="重置" OnClick="LoginRestBtn_Click" />
        </asp:Panel>
        <br />
        <asp:Panel ID="LoginOKPanel" runat="server" Height="92px" Visible="False">
            <asp:Label ID="LoginOKLabel" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <a href="GradeProcess.aspx">录入成绩</a><br /> <a href="TeacherQueryGrade.aspx">成绩查询</a></asp:Panel>
    </form>
</body>
</html>
