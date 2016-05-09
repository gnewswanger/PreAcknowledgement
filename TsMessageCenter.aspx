<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TsMessageCenter.aspx.vb"
    Inherits="PreAcknowledge.TsMessageCenter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/MsgMain.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/MsgAddEdit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="page_wrapper">
        <div class="page_header">
            <asp:Image ID="DealerNetLogo" runat="server" ImageUrl="Images/Logo/DealerNet.png" />
            <asp:Image ID="QcciLogo" runat="server" ImageUrl="Images/Logo/QCCIlogo-web-blk.jpg" />
            <label>
                <span>Message Center</span></label>
        </div>
        <div class="headerStripe">
        </div>
        <div class="page_content">
            <div class="repeaterDataBlock">
                <asp:Repeater ID="Repeater1" runat="server">
                    <HeaderTemplate>
                        <div class="repeaterHeader">
                            <label class="lineNo"> No. </label>
                            <label>Project Name</label><label>Section Name</label><label class="linkLabel">Messages</label>
                        </div>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <div class="repeaterRowBlock">
                            <label class="lineNo"> <%#((Container).ItemIndex + 1).ToString()%>.)</label>
                            <label>
                                <%#Container.DataItem("ProjectName")%></label>
                            <label>
                                <%#Container.DataItem("SectionName")%></label>
                            <label class="linkLabel"><asp:LinkButton ID="LinkMessages" runat="server" CommandName="GoMessages">View Messages</asp:LinkButton></label>
                            <asp:HiddenField ID="hfSectionId" runat="server" Value='<%#Container.DataItem("SectionID")%>'/>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
