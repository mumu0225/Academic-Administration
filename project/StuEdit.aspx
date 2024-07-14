<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StuEdit.aspx.cs" Inherits="StuEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
        }
        .auto-style2 {
            font-size: medium;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server" class="auto-style1">
        学生信息更新<br />
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
                    <asp:DropDownList ID="DeptDDList" runat="server" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="DeptDDList_SelectedIndexChanged" OnLoad="DeptDDList_Load">
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
                <td aria-orientation="vertical" class="auto-style2">
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
                    <asp:TextBox ID="PassTextBox" runat="server"></asp:TextBox>
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
                    <asp:Button ID="UpdateBtn" runat="server" Height="22px" Text="更新" Width="80px" OnClick="UpdateBtn_Click"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
