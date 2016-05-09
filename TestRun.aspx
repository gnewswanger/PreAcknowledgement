<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TestRun.aspx.vb" Inherits="PreAcknowledge.TestRun" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
        <legend>Select Start Page</legend>
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem Value="MessageMain.aspx" Selected="True">Message Page</asp:ListItem>
                <asp:ListItem Value="ResponseMain.aspx">Response Page</asp:ListItem>
                <asp:ListItem Value="TsMessageCenter.aspx">Message Center</asp:ListItem>
                <asp:ListItem Value="MessageDashboard.aspx">Message Dashboard</asp:ListItem>
            </asp:RadioButtonList>
        </fieldset>
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="Start" />
    </div>
    <div>
        <asp:Button ID="Button2" runat="server" Text="File Search" />
    </div>
    <div>
        <asp:Button ID="Button3" runat="server" Text="Backlog Screen" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
