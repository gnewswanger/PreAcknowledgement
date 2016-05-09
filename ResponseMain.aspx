<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResponseMain.aspx.vb" Inherits="PreAcknowledge.ResponseMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/MsgMain.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/MsgMain.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_wrapper">
        <div class="page_header">
            <asp:Image ID="DealerNetLogo" runat="server" ImageUrl="Images/Logo/DealerNet.png" />
            <asp:Image ID="QcciLogo" runat="server" ImageUrl="Images/Logo/QCCIlogo-web-blk.jpg" />
        </div>
        <div class="headerStripe">
            <asp:Button ID="Button1" runat="server" Text="Return" />
        </div>
        <div class="page_content">
            <div id="TimeLine">
                <label title="Timeline">
                    Time Line:
                </label>
            </div>
            <div id="JobInfo">
                <table>
                    <thead>
                        <tr>
                            <th>
                                Order Number
                            </th>
                            <th>
                                Construction
                            </th>
                            <th>
                                Project Name
                            </th>
                            <th>
                                Section Name
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="LabelOrderNo" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelConstruct" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelProject" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelSection" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="contentBlock">
                <div class="sectionItems">
                    <div>
                        <asp:Button ID="btnExpandAll" runat="server" CssClass="aspButtons" Text="Expand All" />
                        <asp:Button ID="btnCollapseAll" runat="server" CssClass="aspButtons" Text="Collapse All" />
                        <asp:Button ID="btnAddEdit" runat="server" CssClass="aspButtons" Text="Add/Edit Responses" />
                        <asp:HiddenField ID="HFSectionItemId" runat="server" />
                    </div>
                    <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" RootNodeStyle-CssClass="treeRoot" SelectedNodeStyle-CssClass="treeSelected" NodeStyle-CssClass="treeNodes" EnableClientScript="False">
                    </asp:TreeView>
                </div>
                <div class="messages">
                    <div class="messageListBlock">
                        <ol id="MessageList">
                            <li>Click on item to display messages.</li>
                        </ol>
                    </div>
                    <div class="messageResponseBlock hideBlock">
                        <ul id="MsgResponseList">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
