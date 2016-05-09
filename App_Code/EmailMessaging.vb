Imports Microsoft.VisualBasic

Public Class EmailMessaging

    Public Function SendEmailNotice(emailAddr As String, message As String, fromName As String) As Boolean
        If Not emailAddr = String.Empty Then
            Dim SmtpServer As New Net.Mail.SmtpClient
            Dim mail As New Net.Mail.MailMessage
            SmtpServer.Host = "mail.qcci.com"
            SmtpServer.Credentials = New System.Net.NetworkCredential("qcciservices", "B5n6,t89")
            mail.From = New Net.Mail.MailAddress("admin@qcci.com")
            mail.To.Add(emailAddr)
            mail.Subject = "New Order Submitted Confirmation"
            mail.Body = message
            Try
                SmtpServer.Send(mail)
            Catch ex As Exception
                Return False
            End Try
            Return True
        End If
        Return False
    End Function

End Class
