<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuAdd.aspx.cs" Inherits="StuAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1280px;
            height: 627px;
            font-size: x-large;
        }
        .auto-style2 {
            width: 67%;
            height: 348px;
        }
        .auto-style3 {
            width: 350px;
        }
        .auto-style4 {
            width: 350px;
            font-size: medium;
        }
        .auto-style5 {
            font-size: medium;
        }
        .auto-style6 {
            width: 350px;
            font-size: medium;
            height: 23px;
        }
        .auto-style7 {
            height: 23px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" class="auto-style1" aria-orientation="horizontal">
        学生信息添加<br />
        <br />
        <br />
        <table align="center" class="auto-style2">
            <tr>
                <td class="auto-style4">学号</td>
                <td>
                    <asp:TextBox ID="StuIDTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">姓名</td>
                <td class="auto-style7">
                    <asp:TextBox ID="StuNameTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">入学年份</td>
                <td>
                    <asp:TextBox ID="EnrollYearTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">毕业年份</td>
                <td>
                    <asp:TextBox ID="GradYearTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">系部</td>
                <td>
                    <asp:DropDownList ID="DeptDDList" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="DeptDDList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">班级</td>
                <td>
                    <asp:DropDownList ID="ClassDDList" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">性别</td>
                <td aria-orientation="vertical" class="auto-style5">
                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Sex" Text="男" Checked="True" />
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Sex" Text="女" />
                </td>
            </tr>
            <tr>
                <td class="auto-style4">生日</td>
                <td style="text-align: center">
                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" CssClass="auto-style5" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="220px">
                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">密码</td>
                <td>
                    <asp:TextBox ID="PassTextBox" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">地址</td>
                <td>
                    <asp:TextBox ID="AddressTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">邮编</td>
                <td>
                    <asp:TextBox ID="ZipCodeTextBox" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="InsertBtn" runat="server" Height="22px" Text="添加" Width="80px" OnClick="InsertBtn_Click" />
                </td>
            </tr>
        </table>
        <br />
            <a href="StuManage.aspx">学生信息管理</a>
    </form>
</body>
</html>
