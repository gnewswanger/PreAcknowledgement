Imports Microsoft.VisualBasic

Public Class OrderInfoClass

    Protected adptrSection As New DataSetSalesOrderTableAdapters.upGetSectionWithDetails_SectionID2TableAdapter
    Protected adptrSectionItems As New DataSetSalesOrderTableAdapters.upGetSectionItems_SectionItemIDTableAdapter
    Protected adptrItemModels As New DataSetSalesOrderTableAdapters.upGetItemModels_SectionItemIDTableAdapter
    Protected adptrItemNotes As New DataSetVFPTableAdapters.sojnotes01TableAdapter
    Public SectionItems As New List(Of ItemInfoClass)
    Protected SectionId As Guid
    Public OrderNoText As String = String.Empty
    Public OrderConstructionType As String = String.Empty
    Public ProjectName As String = String.Empty
    Public SectionName As String = String.Empty

    Public Class ItemInfoClass
        Public ItemId As Guid
        Public ItemNo As String = String.Empty
        Public ItemModel As String = String.Empty
        Public ItemInfo As New List(Of String)
    End Class

    Public Sub New(sectId As Guid)
        Me.SectionId = sectId
        Me.InitializeItemClassList()
    End Sub

    Private Sub InitializeItemClassList()
        Dim tblSection As DataSetSalesOrder.upGetSectionWithDetails_SectionID2DataTable
        tblSection = Me.adptrSection.GetData(Me.SectionId)
        Dim tblRow As DataSetSalesOrder.upGetSectionWithDetails_SectionID2Row = tblSection.Rows(0)
        If tblRow.ShortName.Trim = "" Then
            Me.OrderNoText = "(not submitted)"
        Else
            Me.OrderNoText = tblRow.ShortName.Trim
        End If
        Me.OrderConstructionType = tblRow.ConstructionType.Trim
        Me.ProjectName = tblRow.ProjectName.Trim
        Me.SectionName = tblRow.LongName.Trim
        Dim adptrItems As New DataSetSalesOrderTableAdapters.upGetSectionItems_SectionID2TableAdapter
        Dim tblItems As DataSetSalesOrder.upGetSectionItems_SectionID2DataTable = adptrItems.GetData(Me.SectionId)
        For Each row As DataSetSalesOrder.upGetSectionItems_SectionID2Row In tblItems.Rows
            Dim itemclass As New ItemInfoClass
            itemclass.ItemId = row.SectionItemID
            itemclass.ItemModel = row.ReferenceNum.ToString.Trim + ", " + row.BaseItemCode.ToString.Trim + ", " + row.BaseItemName.ToString.Trim
            Dim SectionItemRow As DataSetSalesOrder.upGetSectionItems_SectionID2Row
            SectionItemRow = row
            Me.PopulateEntireSection(itemclass, SectionItemRow)
            Me.SectionItems.Add(itemclass)
        Next
    End Sub

    Private Sub PopulateEntireSection(itemClass As ItemInfoClass, SectionItemRow As DataSetSalesOrder.upGetSectionItems_SectionID2Row)
        Dim adptrQuotes As New DataSetTsMessagesTableAdapters.QuoteTableAdapter
        Dim tblQuotes As New DataSetTsMessages.QuoteDataTable
        Dim quoteRow As DataSetTsMessages.QuoteRow
        Dim quoteStatus As System.String = ""

        Dim adptrQuotesDetail As New DataSetTsMessagesTableAdapters.QuoteDetailTableAdapter
        Dim tblQuotesDetail As New DataSetTsMessages.QuoteDetailDataTable
        Dim quoteDetailRow As DataSetTsMessages.QuoteDetailRow

        Dim itemNotesRow As DataSetTsMessages.UpGetItemNotes_SectionItemIDRow

        ' load the item models
        Dim tblItemModels As DataSetSalesOrder.upGetItemModels_SectionItemIDDataTable
        tblItemModels = adptrItemModels.GetData(SectionItemRow.SectionItemID)

        ' load the notes
        Dim tblItemNotes As DataSetVFP.sojnotes01DataTable
        tblItemNotes = adptrItemNotes.GetData(SectionItemRow.ReferenceNum)

        Dim i As System.Int32 = 0

        If SectionItemRow.IsQuoteIDNull() Then
            i = 0
            For Each itemNotesRow In tblItemNotes.Rows
                Dim note As String = itemNotesRow.SectionNote.Trim
                If note.Trim <> "Quoted Option" Then
                    i = i + 1
                    If i = 1 Then
                        itemClass.ItemInfo.Add(IIf(tblItemModels.Count > 1, "Combined Cabinet", SectionItemRow.BaseItemName.Trim))
                    End If
                    itemClass.ItemInfo.Add(itemNotesRow.SectionNote.Trim)
                End If
            Next
        Else
            If Not SectionItemRow.IsQuoteIDNull Then
                'get quote information 
                tblQuotes.Clear()
                adptrQuotes.Fill(tblQuotes, SectionItemRow.QuoteID)
                If tblQuotes.Count > 0 Then
                    quoteRow = tblQuotes.Item(0)
                    tblQuotesDetail.Clear()
                    adptrQuotesDetail.FillByQuoteID(tblQuotesDetail, SectionItemRow.QuoteID)
                    quoteStatus = QuoteStatusLabel(quoteRow.QuoteStatusId)
                    If Not quoteRow.IsExpirationDateNull Then
                        If Date.Now > quoteRow.ExpirationDate Then
                            quoteStatus = "Expired"
                        End If
                    End If
                    itemClass.ItemInfo.Add("Quote #" + quoteRow.QuoteNo.ToString + "  " + quoteRow.JobName.Trim + "  " + quoteStatus)

                    For Each quoteDetailRow In tblQuotesDetail
                        itemClass.ItemInfo.Add(quoteDetailRow.Detail.Trim)
                    Next
                End If
            Else
                itemClass.ItemInfo.Add(IIf(tblItemModels.Count > 1, "Combined Cabinet", SectionItemRow.BaseItemName.Trim))
            End If

        End If
        If SectionItemRow.SpecialInstructions.Trim > "" Then
            itemClass.ItemInfo.Add(SectionItemRow.SpecialInstructions.Trim)
        End If
    End Sub

    Private Function QuoteStatusLabel(ByVal pQuoteStatusId As System.Int32) As System.String
        Dim retString As System.String = String.Empty
        If pQuoteStatusId = 1 Then
            retString = "Dealer Estimated Price"
        ElseIf pQuoteStatusId = 2 Then
            retString = "Pending"
        ElseIf pQuoteStatusId = 3 Then
            retString = "In Process"
        ElseIf pQuoteStatusId = 5 Then
            retString = "Revised"
        Else
            retString = "Completed"
        End If
        Return retString
    End Function
End Class
