<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClassAdd.aspx.cs" Inherits="ClassAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 552px;
            width: 1310px;
            font-size: x-large;
        }
        .auto-style1 {
            width: 34%;
            height: 159px;
        }
        .auto-style2 {
            height: 28px;
            font-size: medium;
        }
        .auto-style3 {
            width: 277px;
        }
        .auto-style4 {
            height: 28px;
            width: 277px;
            font-size: medium;
        }
        .auto-style5 {
            font-size: xx-large;
        }
        .auto-style6 {
            font-size: medium;
        }
    </style>
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
        <span class="auto-style5">班级信息添加</span><br />
        <br />
        <table align="center" class="auto-style1">
            <tr>
                <td class="auto-style6">班级编码</td>
                <td>
                    <asp:TextBox ID="ClassIDTextBox" runat="server" CssClass="auto-style6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">班级名称</td>
                <td>
                    <asp:TextBox ID="ClassNameTextBox" runat="server" CssClass="auto-style6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">系部</td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DeptDDList" runat="server" Width="150px" AutoPostBack="True" CssClass="auto-style6">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">班主任</td>
                <td>
                    <asp:DropDownList ID="TeacherDDList" runat="server" Width="150px" AutoPostBack="True" CssClass="auto-style6">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="auto-style6">
                <td class="auto-style3">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="InsertBtn" runat="server" Text="添加" Width="80px" OnClick="InsertBtn_Click" CssClass="auto-style6" />
                </td>
            </tr>
        </table>
        <br />
        <span style="mso-bidi-font-size: 12.0pt; font-family: 宋体; mso-ascii-font-family: &quot;Times New Roman&quot;; mso-hansi-font-family: &quot;Times New Roman&quot;; mso-bidi-font-family: &quot;Times New Roman&quot;; mso-font-kerning: 1.0pt; mso-ansi-language: EN-US; mso-fareast-language: ZH-CN; mso-bidi-language: AR-SA"><br />
        </span>
        <a href="ClassManage.aspx">班级信息维护</a>
    </form>
</body>
</html>
