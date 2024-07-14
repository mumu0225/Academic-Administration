<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GradeProcess.aspx.cs" Inherits="GradeProcess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 408px;
        }
        .auto-style1 {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style1" style="text-align: center">
        教师课程班成绩录入与处理<br />
        <br />
        <asp:DropDownList ID="CourseClassDDList" runat="server" AutoPostBack="True" Width="400px" OnTextChanged="CourseClassDDList_TextChanged">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="QueryBtn" runat="server" Text="确定" Width="80px" OnClick="QueryBtn_Click" />
        <br />
        <br />
        <asp:GridView ID="GradeGView" runat="server" DataKeyNames="GradeSeedID" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" HorizontalAlign="Center">
            <AlternatingRowStyle BackColor="#DCDCDC" />
            <Columns>
                <asp:BoundField DataField="GradeSeedID" HeaderText="成绩编码" Visible="False"/>
                <asp:BoundField DataField="StuID" HeaderText="学号" />
                <asp:BoundField DataField="StuName" HeaderText="姓名" />
                <asp:TemplateField HeaderText="平时成绩">
                    <ItemTemplate>
                    <asp:TextBox ID="TBoxCommonScore" Width="60" Text="0" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期中成绩">
                    <ItemTemplate>
                        <asp:TextBox ID="TBoxMiddleScore" Width="60" Text="0" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="期末成绩">
                    <ItemTemplate>
                        <asp:TextBox ID="TBoxLastScore" Width="60" Text="0" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalScore" HeaderText="总评成绩" />
                <asp:TemplateField HeaderText="锁定">
                    <ItemTemplate>
                        <asp:CheckBox ID="CBoxLockFlag" Enabled="False" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
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
        <asp:Button ID="GradeProcBtn" runat="server" Text="成绩处理" Width="80px" OnClick="GradeProcBtn_Click" />
    </form>
</body>
</html>
