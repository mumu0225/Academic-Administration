<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1172px;
            height: 581px;
            font-size: xx-large;
        }
        .auto-style2 {
            width: 44%;
        }
        .auto-style3 {
            font-size: medium;
        }
        .auto-style4 {
            width: 161px;
        }
        .auto-style5 {
            height: 24px;
        }
    </style>
</head>
<body style="text-align: center; width: 100%;">
    <form id="form1" runat="server" class="auto-style1" style="width: 100%">
        用户登录<br />
        <br />
        <asp:Panel ID="LoginPanel" runat="server">
            <table align="center" class="auto-style2">
                <tbody class="auto-style3">
                    <tr>
                        <td class="auto-style4">用户名：</td>
                        <td>
                            <asp:TextBox ID="UsernameTextBox" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">密　码：</td>
                        <td>
                            <asp:TextBox ID="PassTextBox" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="auto-style5">
                            <asp:RadioButton ID="teacher" runat="server" GroupName="Character" Text="教师" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="student" runat="server" GroupName="Character" Text="学生" />
                            &nbsp;&nbsp;
                            <asp:RadioButton ID="admin" runat="server" GroupName="Character" Text="管理员" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="LoginBtn" runat="server" OnClick="LoginBtn_Click" Text="登录" Width="80px" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ResetBtn" runat="server" OnClick="ResetBtn_Click" Text="重置" Width="80px" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="TeacherPanel" runat="server" Visible="False">
            <asp:Label ID="TeacherLoginLabel" runat="server" CssClass="auto-style3" Text="Label"></asp:Label>
            <br />
            <br />
            <a href="GradeProcess.aspx"><span class="auto-style3">录入成绩</span></a><span class="auto-style3"><br /> </span><a href="TeacherGradeQuery.aspx"><span class="auto-style3">成绩查询</span></a>
        </asp:Panel>
        <br />
        <asp:Panel ID="StuPanel" runat="server" Visible="False">
            <asp:Label ID="StuLoginLabel" runat="server" Text="Label" CssClass="auto-style3"></asp:Label>
            <br />
            <br />
            <a href="SelectCourse.aspx"><span class="auto-style3">课程选修</span></a><span class="auto-style3"><br /> </span><a href="StuGradeQuery.aspx"><span class="auto-style3">成绩查询</span></a>
        </asp:Panel>
        <br />
        <asp:Panel ID="AdminPanel" runat="server" Visible="False">
            <asp:Label ID="AdminLoginLabel" runat="server" Text="Label" CssClass="auto-style3"></asp:Label>
            <br />
            <br />
            <a href="ClassManage.aspx"><span class="auto-style3">班级信息管理</span></a><span class="auto-style3"><br /> </span><a href="StuManage.aspx"><span class="auto-style3">学生信息管理</span></a>
        </asp:Panel>
    </form>
</body>
</html>
