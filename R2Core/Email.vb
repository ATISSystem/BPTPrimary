


Imports R2Core.ConfigurationManagement
Imports R2Core.DateTimeProvider
Imports R2Core.Email.Exceptions
Imports R2Core.GeneralConfiguration
Imports System.IO
Imports System.Net.Mail
Imports System.Text

Namespace Email

    Public Class R2CoreEmailManager

        Private _DateTimeService As New R2DateTimeService

        Public Sub SendEmailWithTXTTypeAttachment(YourReciever As String, YourAttachment As StringBuilder, YourSubject As String, YourBody As String, YourAttachmentFileName As String)
            Try
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                If Not InstanceGeneralConfiguration.GetBooleanConfiguration(R2CoreGeneralConfigurations.EmailSystem, 0) Then Throw New R2CoreEmailSystemIsNotActiveException

                Dim myByteArray = System.Text.Encoding.UTF8.GetBytes(YourAttachment.ToString)
                Dim ms = New MemoryStream(myByteArray)
                Dim Credentials = New System.Net.NetworkCredential(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.EmailSystem, 2), InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.EmailSystem, 3))
                Dim SmtpClient = New SmtpClient()
                SmtpClient.Host = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.EmailSystem, 1)
                SmtpClient.Port = 587
                SmtpClient.EnableSsl = True
                SmtpClient.Credentials = Credentials
                Dim mail = New MailMessage()
                mail.From = New MailAddress(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.EmailSystem, 2))
                mail.To.Add(YourReciever)
                mail.Attachments.Add(New Attachment(ms, YourAttachmentFileName))
                mail.Subject = YourSubject
                mail.Body = YourBody
                SmtpClient.Send(mail)
            Catch ex As R2CoreEmailSystemIsNotActiveException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class R2CoreEmailSystemIsNotActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سرویس ایمیل غیر فعال است"
                End Get
            End Property
        End Class

    End Namespace


End Namespace
