Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class TsMessagesData
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function SectionItemMessages(sectionItemIdStr As String) As String
        Dim adptr As New DataSetTsMessagesTableAdapters.upGetTsMessageBySectionItemsIdTableAdapter
        Dim tbl As DataSetTSMessages.upGetTsMessageBySectionItemsIdDataTable
        tbl = adptr.GetData(Guid.Parse(sectionItemIdStr))
        Dim row As DataSetTSMessages.upGetTsMessageBySectionItemsIdRow
        Dim outData As List(Of Dictionary(Of String, Object))
        For Each row In tbl.Rows
            row = adptr.GetData(System.Guid.Parse(sectionItemIdStr)).Rows(0)
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

End Class