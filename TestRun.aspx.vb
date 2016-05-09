Namespace PreAcknowledge
    Partial Class TestRun
        Inherits System.Web.UI.Page

        Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
            Select Case RadioButtonList1.SelectedIndex
                Case 0
                    Server.Transfer(RadioButtonList1.SelectedValue + "?SectionId=405AC940-7185-4CD5-A5D5-295FA5CD1144")
                Case 1
                    Server.Transfer(RadioButtonList1.SelectedValue + "?SectionId=405AC940-7185-4CD5-A5D5-295FA5CD1144")
                Case 2
                    Server.Transfer(RadioButtonList1.SelectedValue)
                Case 3
                    Server.Transfer(RadioButtonList1.SelectedValue)

            End Select
        End Sub
    End Class
End Namespace