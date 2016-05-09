Imports System.Data
Imports System.Runtime.Caching
Imports Telerik.Web.UI

Namespace PreAcknowledge

    Partial Class MessageMain
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Session("SectionId") = Request.QueryString("SectionId")
            'If Request.QueryString("SectionItemId") IsNot Nothing Then
            '    Server.Transfer("MessageAddEdit.aspx?SectionId=" + Session("SectionId") + "&SectionItemId=" + Request.QueryString("SectionItemId"))
            'End If
            If Not IsPostBack Then
                Me.ClearCache()
                Me.PopulateJobInfo()
            End If
        End Sub

        Protected Function orderClass() As OrderInfoClass
            Dim myCache As MemoryCache = MemoryCache.Default
            If myCache("OrderInfo") Is Nothing Then
                myCache.Add("OrderInfo", New OrderInfoClass(Guid.Parse(Session("SectionId"))), DateTimeOffset.Now.AddSeconds(3600.0))
            End If
            Return myCache("OrderInfo")
        End Function

        Private Sub ClearCache()
            Dim myCache As MemoryCache = MemoryCache.Default
            myCache.Remove("OrderInfo")
        End Sub

        Private Sub PopulateJobInfo()
            Me.LabelOrderNo.Text = orderClass.OrderNoText
            Me.LabelConstruct.Text = orderClass.OrderConstructionType
            Me.LabelProject.Text = orderClass.ProjectName
            Me.LabelSection.Text = orderClass.SectionName
            Me.PopulateTreeView()
        End Sub

        Private Sub PopulateTreeView()
            For Each item As OrderInfoClass.ItemInfoClass In Me.orderClass.SectionItems
                Dim childNode As RadTreeNode
                childNode = New RadTreeNode(item.ItemModel, item.ItemId.ToString)
                For Each detail As String In item.ItemInfo
                    childNode.Nodes.Add(New RadTreeNode(detail))
                Next
                RadTreeViewMsg.Nodes.Add(childNode)
            Next
        End Sub

        Protected Sub btnExpandAll_Click(sender As Object, e As EventArgs) Handles btnExpandAll.Click
            Me.RadTreeViewMsg.ExpandAllNodes()
        End Sub

        Protected Sub btnCollapseAll_Click(sender As Object, e As EventArgs) Handles btnCollapseAll.Click
            Me.RadTreeViewMsg.CollapseAllNodes()
        End Sub

        'Protected Sub btnAddEdit_Click(sender As Object, e As System.EventArgs) Handles btnAddEdit.Click
        '    Dim url As String = "MessageAddEdit.aspx?SectionId=" + Session("SectionId") + "&SectionItemId=" + Me.HFSectionItemId.Value
        '    Server.Transfer(url)
        'End Sub

        Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
            Server.Transfer("TestRun.aspx")
        End Sub
    End Class
End Namespace