Imports System.Data

Namespace PreAcknowledge
    Partial Class TimeLineControl
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Me.TLBenchMarks.InnerHtml = Me.GenerateTimeLine(Session("SectionId").ToString)
        End Sub

        Private Function GenerateTimeLine(sectionIdStr As String) As String
            Dim tl As New TimeLine.TimeLine(Guid.Parse(sectionIdStr))
            If tl.HasExistingTimeline Then
                Dim tblTl As DataTable = tl.TimelineDataTable
                Dim view As New DataView(tblTl)
                view.Sort = "Sequence"
                If Session("TlRowFilter") IsNot Nothing Then
                    view.RowFilter = Session("TlRowFilter").ToString
                Else
                    view.RowFilter = String.Empty
                End If
                Dim i As Integer = 1
                Dim htmlStr As String = String.Empty
                For Each rowview As DataRowView In view
                    htmlStr += "<div id='MarkName" + i.ToString + "'>" + rowview.Row("Name").ToString + "<div class='markValue" + "'>" + rowview.Row("Date").ToShortDateString + "</div></div>"
                    i = i + 1
                Next
                Return htmlStr
            End If
            Return ""
        End Function
    End Class
End Namespace