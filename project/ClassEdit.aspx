<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassEdit.aspx.cs" Inherits="ClassEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 1255px;
            height: 524px;
            font-size: x-large;
        }
        .auto-style3 {
            font-size: medium;
        }
        .auto-style4 {
            font-size: medium;
            height: 28px;
            width: 200px;
        }
        .auto-style5 {
            height: 28px;
        }
        .auto-style6 {
            font-size: medium;
            width: 200px;
        }
        .auto-style7 {
            width: 200px;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <div class="auto-style1">
            班级信息更新<br />
            <br />
            <br />
            <table align="center" class="auto-style2">
                <tr>
                    <td class="auto-style6">班级编码：</td>
                    <td>
                        <asp:TextBox ID="ClassIDTextBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7"><span class="auto-style3">班级名称：</span></td>
                    <td>
                        <asp:TextBox ID="ClassNameTextBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">系部：</td>
                    <td class="auto-style5">
                        <asp:DropDownList ID="DeptDDList" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">班主任：</td>
                    <td>
                        <asp:DropDownList ID="TeacherDDList" runat="server" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="UpdateBtn" runat="server" Height="22px" OnClick="UpdateBtn_Click" Text="更新" Width="80px" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
