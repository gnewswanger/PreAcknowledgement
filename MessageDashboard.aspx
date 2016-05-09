<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MessageDashboard.aspx.vb"
    Inherits="PreAcknowledge.MessageDashboard" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/MessageDashboard.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/MessageDashBoard.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" 
                Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <div class="page_wrapper">
        <div class="page_header">
            <asp:Image ID="QcciLogo" runat="server" ImageUrl="Images/Logo/QCCIlogo-web-blk.jpg" />
            <label><span>Message Center Dashboard</span></label>
        </div>
        <div class="headerStripe">
        </div>
        <div class="page_content">
            <div class="dataGrid">
                <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="ObjectDataSource1" GroupPanelPosition="Top" 
                    ResolvedRenderMode="Classic" CellSpacing="-1" GridLines="Both">
                    <MasterTableView DataKeyNames="SectionID" DataSourceID="ObjectDataSource1">
                        <DetailTables>
                            <telerik:GridTableView runat="server" DataKeyNames="MessageID" 
                                DataSourceID="ObjectDataSource2">
                                <DetailTables>
                                    <telerik:GridTableView runat="server" DataSourceID="ObjectDataSource3">
                                        <ParentTableRelation>
                                            <telerik:GridRelationFields DetailKeyField="MessageID" 
                                                MasterKeyField="MessageID" />
                                        </ParentTableRelation>
                                        <Columns>
                                            <telerik:GridBoundColumn AllowFiltering="False" DataField="ResponseText" 
                                                FilterControlAltText="Filter column column" HeaderText="Response Text" 
                                                UniqueName="column">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </telerik:GridTableView>
                                </DetailTables>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="MessageText" 
                                        FilterControlAltText="Filter column column" HeaderText="MessageText" 
                                        UniqueName="column">
                                        <ItemStyle Width="75%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="BaseItemName" 
                                        FilterControlAltText="Filter column2 column" HeaderText="Item Name" 
                                        UniqueName="column2">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn FilterControlAltText="Filter column1 column" 
                                        HeaderText="Attachments" Text="Attachments" UniqueName="column1">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn DataField="SectionItemID" DataType="System.Guid" 
                                        Display="False" FilterControlAltText="Filter column3 column" 
                                        UniqueName="column3">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </telerik:GridTableView>
                        </DetailTables>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ProjectNo" 
                                FilterControlAltText="Filter ProjectNo column" HeaderText="ProjectNo" 
                                ReadOnly="True" SortExpression="ProjectNo" UniqueName="ProjectNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SectionNo" 
                                FilterControlAltText="Filter SectionNo column" HeaderText="SectionNo" 
                                ReadOnly="True" SortExpression="SectionNo" UniqueName="SectionNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MakeAfterNo" 
                                FilterControlAltText="Filter MakeAfterNo column" HeaderText="MakeAfterNo" 
                                ReadOnly="True" SortExpression="MakeAfterNo" UniqueName="MakeAfterNo">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SectionName" 
                                FilterControlAltText="Filter SectionName column" HeaderText="SectionName" 
                                ReadOnly="True" SortExpression="SectionName" UniqueName="SectionName">
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn AllowSorting="False" 
                                FilterControlAltText="Filter GoToLinkCol column" HeaderText="Go To" 
                                HeaderTooltip="Go to Message Board for selected Section" 
                                ImageUrl="Images/GoTo.png" UniqueName="GoToLinkCol">
                                <HeaderStyle CssClass="goToLinkCol" />
                                <ItemStyle CssClass="goToLinkCol" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" DataField="FullName" 
                                FilterControlAltText="Filter ContactNameCol column" 
                                HeaderText="Primary Contact" UniqueName="ContactNameCol">
                            </telerik:GridBoundColumn>
                            <telerik:GridButtonColumn FilterControlAltText="Filter column column" 
                                Text="Email Notice" UniqueName="EmailNoticeCol" HeaderText="Email Notice" 
                                CommandName="EmailNotice" >
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="PrimaryContactID" Display="False" 
                                FilterControlAltText="Filter column column" UniqueName="ContactIDCol">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="GetData" 
                    TypeName="DataSetTsMessagesTableAdapters.upGetTsMessageResponseByMsgIdTableAdapter" 
                    UpdateMethod="Update">
                    <InsertParameters>
                        <asp:Parameter DbType="Guid" Name="ResponseID" />
                        <asp:Parameter DbType="Guid" Name="MessageID" />
                        <asp:Parameter Name="ResponseText" Type="String" />
                        <asp:Parameter DbType="Guid" Name="ResponseStatusID" />
                    </InsertParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="RadGrid1" DbType="Guid" Name="MessageID" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter DbType="Guid" Name="ResponseID" />
                        <asp:Parameter DbType="Guid" Name="MessageID" />
                        <asp:Parameter Name="ResponseText" Type="String" />
                        <asp:Parameter DbType="Guid" Name="ResponseStatusID" />
                    </UpdateParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                    TypeName="DataSetTsMessagesTableAdapters.upGetTsMessageBySectionIDTableAdapter">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="RadGrid1" DbType="Guid" Name="SectionId" 
                            PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                    TypeName="DataSetTsMessagesTableAdapters.upGetTsMsgUnresolvedByContactTableAdapter">
                    <SelectParameters>
                        <asp:Parameter DbType="Guid" Name="ContactUserID" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="EmailNotice" class="modalWindow">
                <div>
                    <div id="ContactInfo">
                    
                    </div>
                    <label>
                        Edit Email Message:
                    </label>
                    <textarea id="TextArea1" runat="server" rows="2" cols="60"></textarea>
                </div>
                <div id="TsDiv">
                    <label class="labelDesc">
                        TS Name:
                    </label>
                    <asp:TextBox ID="TextTsName" runat="server"></asp:TextBox>
                    <div>
                        <label class="labelDesc">
                            TS Email:
                        </label>
                        <asp:TextBox ID="TextTsEmail" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="buttonBlock">
                    <asp:Button ID="BtnSendMsg" runat="server" CssClass="aspButtons" Text="Send" />
                    <input type="button" id="BtnCancelSendMsg" class="aspButtons" onclick="FadeModalDialog()" value="Cancel" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
