<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MessageMain.aspx.vb" Inherits="PreAcknowledge.MessageMain" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="TimeLineControl.ascx" tagname="Timeline" tagprefix="UC1" %>
<%@ Register src="FileAttachControl.ascx" tagname="Attachments" tagprefix="UC1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=10" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/MsgMain.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.11.3.min.js"></script>
    <script src="Scripts/MsgMain.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server" 
        EnablePageMethods="True">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div class="page_wrapper">
        <div class="page_header">
            <asp:Image ID="DealerNetLogo" runat="server" ImageUrl="Images/Logo/DealerNet.png" />
            <asp:Image ID="QcciLogo" runat="server" ImageUrl="Images/Logo/QCCIlogo-web-blk.jpg" />
        </div>
        <div class="headerStripe">
            <asp:Button ID="Button1" runat="server" Text="Return" Visible="False" />
        </div>
        <div class="page_content">
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
            <UC1:Timeline ID="Timeline1" runat="server" />
            <div class="contentBlock">
                <div class="sectionItems">
                    <div>
                        <asp:Button ID="btnExpandAll" runat="server" CssClass="aspButtons" OnClientClick="treeExpandAllNodes()"
                            Text="Expand All" />
                        <asp:Button ID="btnCollapseAll" runat="server" CssClass="aspButtons" OnClientClick="treeCollapseAllNodes()"
                            Text="Collapse All" />
                        <asp:HiddenField ID="HFSectionItemId" runat="server" />
                    </div>
                    <telerik:RadTreeView ID="RadTreeViewMsg" runat="server" ResolvedRenderMode="Classic"
                        ClientIDMode="Static" OnClientNodeClicked="NodeClick">
                    </telerik:RadTreeView>
                </div>
                <div class="messages">
                    <asp:CheckBox ID="IncludeWDMsgCheck" runat="server" Text="Include Withdrawn Messages" />
                    <div class="messageListBlock">
                        <asp:Button ID="BtnNewMsg" runat="server" CssClass="aspButtons hideElement" Text="Add Message" />
                        <ul id="MessageList">
                            <li>Click on item to display messages. </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div id="MsgEditDlg" class="modalWindow">
            <div>
                <label for="DropDownStatus">
                    Select Status:</label>
                <asp:DropDownList ID="DropDownStatus" runat="server" DataSourceID="SqlDataSource1"
                    DataTextField="MessageStatus" DataValueField="StatusID" CssClass="aspDropDown">
                </asp:DropDownList>
            </div>
            <div class="buttonBlock">
                <asp:Button ID="BtnSaveMsgEdit" runat="server" CssClass="aspButtons" Text="Save" />
                <asp:Button ID="BtnCancelMsgEdit" runat="server" CssClass="aspButtons" Text="Cancel" />
            </div>
            <asp:HiddenField ID="HfMsgId" runat="server" Value='' />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:QCCOrderEntryConnectionString %>"
                SelectCommand="upGetTsMessageStatusList" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:Parameter DefaultValue="Msg" Name="MsgOrRspOrBoth" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
        <div id="MsgAddDlg" class="modalWindow">
            <div style="display: none;">
                <label>
                    Message Date:
                </label>
                <telerik:RadDatePicker ID="MsgDatePicker" runat="server" Culture="en-US" ResolvedRenderMode="Classic"
                    ZIndex="17000">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                    </Calendar>
                    <DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy" LabelWidth="40%">
                        <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                        <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                        <FocusedStyle Resize="None"></FocusedStyle>
                        <DisabledStyle Resize="None"></DisabledStyle>
                        <InvalidStyle Resize="None"></InvalidStyle>
                        <HoveredStyle Resize="None"></HoveredStyle>
                        <EnabledStyle Resize="None"></EnabledStyle>
                    </DateInput>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                </telerik:RadDatePicker>
            </div>
            <div>
                <label>
                    Write Message:
                </label>
                <textarea id="TextArea1" runat="server" rows="2" cols="60"></textarea>
            </div>
            <div class="buttonBlock">
                <asp:Button ID="BtnSaveMsgAdd" runat="server" CssClass="aspButtons" Text="Save" />
                <asp:Button ID="BtnCancelMsgAdd" runat="server" CssClass="aspButtons" Text="Cancel" />
            </div>
        </div>
        <div id="FileAttachments" class="modalWindow">
            <UC1:Attachments ID="Attachments1" runat="server" />
        </div>
    </div>
    </form>
</body>
<script type="text/javascript" language="javascript">
    function treeExpandAllNodes() {
        var treeView = $find("<%= RadTreeViewMsg.ClientID %>");
        var nodes = treeView.get_allNodes();
        for (var i = 0; i < nodes.length; i++) {

            if (nodes[i].get_nodes() != null) {
                nodes[i].expand();
            }
        }
    }
    function treeCollapseAllNodes() {
        var treeView = $find("<%= RadTreeViewMsg.ClientID %>");
        var nodes = treeView.get_allNodes();
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].get_nodes() != null) {
                nodes[i].collapse();
            }
        }
    }
</script>
</html>
