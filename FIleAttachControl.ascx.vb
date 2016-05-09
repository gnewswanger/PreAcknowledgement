Imports Telerik.Web.UI
Imports System.Data
Imports System.IO

Namespace PreAcknowledge
    Partial Class FileAttachControl
        Inherits System.Web.UI.UserControl

        Public MessageID As HiddenField

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        End Sub

        Protected Function FileUpload1_FileUploaded(ByVal sender As Object, ByVal e As Telerik.Web.UI.FileUploadedEventArgs) As Boolean Handles FileUpload1.FileUploaded
            Dim MapPath As System.String = String.Empty
            Dim UploadFilename As System.String = String.Empty

            If e.UploadResult.FileName <> "" Then
                Dim adptrMsg As New DataSetTsMessagesTableAdapters.upGetTsMessageBySectionItemsIdTableAdapter
                Dim tblMsg As DataSetTsMessages.upGetTsMessageBySectionItemsIdDataTable = adptrMsg.GetDataByMessageID(Guid.Parse(Me.MessageID.Value.ToString))
                Dim adptrJobno As New DataSetSalesOrderTableAdapters.upGetJobNumber_SectionIdTableAdapter
                Dim jobno As String = adptrJobno.GetData(tblMsg.Rows(0).Item("SectionID")).Rows(0)("JobNumber")
                Dim AppSettings As System.Configuration.AppSettingsReader = New System.Configuration.AppSettingsReader()
                MapPath = Server.MapPath("~/" + AppSettings.GetValue("PreAcknowAttachPath", GetType(String)) + jobno + "/")
                If Not Directory.Exists(MapPath) Then
                    Directory.CreateDirectory(Path.GetDirectoryName(MapPath))
                End If

                UploadFilename = MapPath + e.UploadResult.FileName
                Try
                    Dim file As UploadedFile = e.File
                    file.SaveAs(UploadFilename)
                    Me.WriteFileToDatabase(jobno + "/",
                                e.UploadResult.FileName, Guid.Parse(Me.MessageID.Value.ToString), Guid.Parse(tblMsg.Rows(0).Item("DialogID").ToString))
                Catch exc As Exception
                    Return False
                End Try
                Return True
            End If
            Return False
        End Function

        Protected Function WriteFileToDatabase(path As String, filename As String, msgID As Guid, dlgId As Guid) As Boolean
            Try
                Dim adptrAttch As New DataSetTsMessagesTableAdapters.upGetAttachmentsByMsgIdTableAdapter
                Dim tblAttch As DataSetTsMessages.upGetAttachmentsByMsgIdDataTable = adptrAttch.GetData(msgID)
                Dim row As DataSetTsMessages.upGetAttachmentsByMsgIdRow = tblAttch.NewRow
                row.AttachID = Guid.NewGuid
                row.DialogID = dlgId
                row.AttachFilename = filename
                row.AttachDescription = ""
                row.AttachPath = path
                tblAttch.Rows.Add(row)
                adptrAttch.Update(tblAttch)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType, "DisplayAttachedFiles", "DisplayAttachedFiles('" + msgID.ToString + "')", True)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class
End Namespace