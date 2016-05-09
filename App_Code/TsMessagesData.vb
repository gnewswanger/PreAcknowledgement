Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Data

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Public Class TsMessagesData
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function SectionItemMessages(sectionItemIdStr As String) As String
        Dim adptr As New DataSetTsMessagesTableAdapters.upGetTsMessageBySectionItemsIdTableAdapter
        Dim tbl As DataSetTsMessages.upGetTsMessageBySectionItemsIdDataTable
        tbl = adptr.GetData(Guid.Parse(sectionItemIdStr))
        Dim dv As New DataView(tbl)
        dv.RowFilter = "MessageStatus NOT LIKE 'Withdrawn'"
        Dim row As DataSetTsMessages.upGetTsMessageBySectionItemsIdRow
        Dim outData As New List(Of Dictionary(Of String, Object))
        For i As Integer = 0 To dv.Count - 1
            row = dv(i).Row
            Dim msgData As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            If row IsNot Nothing Then
                msgData.Add("MessageID", row.MessageID)
                msgData.Add("MessageText", row.MessageText.Trim)
                msgData.Add("MessageStatus", row.MessageStatus)
            End If
            outData.Add(msgData)
        Next
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return serializer.Serialize(outData)
    End Function

    <WebMethod()> _
    Public Function SiMessageResponses(messageIdStr As String) As String
        Dim adptr As New DataSetTsMessagesTableAdapters.upGetTsMessageResponseByMsgIdTableAdapter
        Dim tbl As DataSetTsMessages.upGetTsMessageResponseByMsgIdDataTable
        tbl = adptr.GetData(Guid.Parse(messageIdStr))
        Dim row As DataSetTsMessages.upGetTsMessageResponseByMsgIdRow
        Dim outData As New List(Of Dictionary(Of String, Object))
        For i As Integer = 0 To tbl.Rows.Count - 1
            row = adptr.GetData(System.Guid.Parse(messageIdStr)).Rows(i)
            Dim msgData As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            If row IsNot Nothing Then
                msgData.Add("ResponseID", row.ResponseID)
                msgData.Add("MessageID", row.MessageID)
                msgData.Add("ResponseText", row.ResponseText.Trim)
                msgData.Add("ResponseStatus", row.MessageStatus)
            End If
            outData.Add(msgData)
        Next
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Return serializer.Serialize(outData)
    End Function

End Class