Imports System.Runtime.Caching

Namespace PreAcknowledge
    Partial Class ResponseMain
        Inherits System.Web.UI.Page


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Session("SectionId") = Request.QueryString("SectionId")
            If Request.QueryString("SectionItemId") IsNot Nothing Then
                Server.Transfer("ResponseAddEdit.aspx?SectionId=" + Session("SectionId") + "&SectionItemId=" + Request.QueryString("SectionItemId"))
            End If
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
                Dim childNode As TreeNode
                childNode = New TreeNode(item.ItemModel, item.ItemId.ToString)
                For Each detail As String In item.ItemInfo
                    childNode.ChildNodes.Add(New TreeNode(detail))
                Next
                TreeView1.Nodes.Add(childNode)
            Next
        End Sub

        Protected Sub btnExpandAll_Click(sender As Object, e As EventArgs) Handles btnExpandAll.Click
            Me.TreeView1.ExpandAll()
        End Sub

        Protected Sub btnCollapseAll_Click(sender As Object, e As EventArgs) Handles btnCollapseAll.Click
            Me.TreeView1.CollapseAll()
        End Sub

        Protected Sub btnAddEdit_Click(sender As Object, e As System.EventArgs) Handles btnAddEdit.Click
            If Not (Me.HFSectionItemId.Value Is Nothing OrElse Me.HFSectionItemId.Value = "") Then
                Dim url As String = "ResponseAddEdit.aspx?SectionId=" + Session("SectionId") + "&SectionItemId=" + Me.HFSectionItemId.Value
                Server.Transfer(url)
            End If
        End Sub

        Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
            Server.Transfer("TestRun.aspx")
        End Sub
    End Class
End Namespace