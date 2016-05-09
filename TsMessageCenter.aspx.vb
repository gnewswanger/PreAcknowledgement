Imports System.Data

Namespace PreAcknowledge
    Partial Class TsMessageCenter
        Inherits System.Web.UI.Page

        Protected adptrMsg As New DataSetTsMessagesTableAdapters.upGetTsMsgUnresolvedByContactTableAdapter
        Protected tblMsg As DataSetTsMessages.upGetTsMsgUnresolvedByContactDataTable

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                'If (Request.Cookies("UserName") Is Nothing) Then
                '    Server.Transfer("UserLogin.aspx")
                'End If
                'Session("LoggedInUserID") = Guid.Parse(Request.QueryString("UserId"))
                'Me.IntitializeList(Session("LoggedInUserID"))
            End If
        End Sub

        Private Sub IntitializeList(ByVal userId As Guid)
            tblMsg = adptrMsg.GetData(userId)
            Dim view As DataView = New DataView(Me.tblMsg)
            Dim tblList As DataTable
            tblList = view.ToTable(True, "ProjectName", "SectionName", "SectionID")
            Me.Repeater1.DataSource = tblList
            Me.Repeater1.DataBind()
        End Sub

        Protected Sub Repeater1_ItemCommand(source As Object, e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles Repeater1.ItemCommand
            If e.CommandName = "GoMessages" Then
                Dim hfCtrl As HiddenField = e.Item.FindControl("HfSectionId")
                Session("SelectedID") = hfCtrl.Value
                Server.Transfer("ResponseMain.aspx?SectionId=" + hfCtrl.Value.ToString)
            End If
        End Sub
    End Class
End Namespace
