Imports Telerik.Web.UI
Imports System.Data
Imports System.Security
Imports System.Security.Principal

Namespace PreAcknowledge
    Partial Class MessageDashboard
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Dim adUser As String = Me.HasDealerImpersonationPermission


        End Sub

        Private Function GetGroups(ByVal userName As String) As List(Of String)
            Dim result As New List(Of String)
            Dim wi As WindowsIdentity = New WindowsIdentity(userName)

            For Each group As IdentityReference In wi.Groups
                Try
                    result.Add(group.Translate(GetType(NTAccount)).ToString())
                Catch ex As Exception
                End Try
            Next

            result.Sort()
            Return result
        End Function

        Private Function HasDealerImpersonationPermission() As String
            Dim UserIdentityInfo As System.Security.Principal.WindowsIdentity
            Dim strMsg As String
            Try
                UserIdentityInfo = HttpContext.Current.User.Identity
                If UserIdentityInfo IsNot Nothing Then
                    strMsg = "User Name: " & UserIdentityInfo.Name & vbCrLf
                    strMsg = strMsg & " Token: " & UserIdentityInfo.Token.ToString() & vbCrLf

                    Dim groups As List(Of String) = GetGroups(UserIdentityInfo.Name.Split("\"c, "/"c)(1))
                    Dim permittedADGroupsAdpr As New DataSetDealerContactTableAdapters.upGetADGroupsWithImpersonationsTableAdapter
                    Dim tbl As DataSetDealerContact.upGetADGroupsWithImpersonationsDataTable
                    tbl = permittedADGroupsAdpr.GetData()
                    For Each Group As String In groups
                        Dim dv As New DataView(tbl)
                        dv.RowFilter = "GroupName = '" + Group + "'"
                        If dv.Count > 0 Then
                            Return UserIdentityInfo.Name
                        End If
                    Next
                    'GetGroups("tleed")
                End If

            Catch ex As Exception
                Return String.Empty
            End Try
            Return String.Empty
        End Function

        Protected Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
            If TypeOf e.Item Is GridDataItem Then
                If e.CommandName = "EmailNotice" Then
                    Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                    Dim cntId As TableCell = dataItem("ContactIDCol")
                    Dim prjNo As TableCell = dataItem("ProjectNo")
                    Dim sectNo As TableCell = dataItem("SectionNo")
                    Dim maNo As TableCell = dataItem("MakeAfterNo")
                    Dim jobNo As String = prjNo.Text + "." + sectNo.Text + "." + maNo.Text
                    ClientScript.RegisterStartupScript(Me.GetType(), "DisplayEmailNotice", "<script>DisplayEmailNotice('" + cntId.Text + "','" + jobNo + "');</script>")
                End If

            End If
        End Sub

        Protected Sub BtnSendMsg_Click(sender As Object, e As System.EventArgs) Handles BtnSendMsg.Click
            ClientScript.RegisterStartupScript(Me.GetType(), "EmailTestAlert", "alert('This is just a test. No email is sent');", True)
        End Sub

        Protected Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
            If (TypeOf (e.Item) Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                Dim sectId As String = dataItem.GetDataKeyValue("SectionID").ToString
                Dim link As TableCell = dataItem("GoToLinkCol")
                CType(link.Controls(0), HyperLink).NavigateUrl = "MessageMain.aspx?SectionId=" + sectId
            End If
        End Sub
    End Class
End Namespace
