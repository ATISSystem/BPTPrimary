

Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Net.NetworkInformation
Imports System.Reflection
Imports System.Web.Services.Protocols
Imports System.Xml

Imports R2Core.ExceptionManagement
Imports R2Core.Networking.Exceptions
Imports R2Core.PredefinedMessagesManagement

Namespace Networking

    'BPTChanged
    Public Class R2CoreNetworkingManager

        Public Shared Function IsInternetAvailable() As Boolean
            Try
                If My.Computer.Network.Ping("217.218.127.127") Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As PingException
                Throw New InternetIsnotAvailableException
            Catch ex As InvalidOperationException
                Throw New InternetIsnotAvailableException
            Catch ex As Exception
                Throw New InternetIsnotAvailableException(ex.Message)
            End Try
        End Function

        Public Shared Function IsThisSiteAvailable(YourSiteURLorIP As String) As Boolean
            Try
                If My.Computer.Network.Ping(YourSiteURLorIP) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As PingException
                Throw New ConnectionIsNotAvailableException
            Catch ex As InvalidOperationException
                Throw New ConnectionIsNotAvailableException
            Catch ex As Exception
                Throw New ConnectionIsNotAvailableException(ex.Message)
            End Try
        End Function


    End Class

    'BPTChanged
    Namespace Exceptions

        Public Class InternetIsnotAvailableException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.InternetIsnotAvailableException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.InternetIsnotAvailableException).MsgId
            End Sub

            Public Sub New(YourMessage As String)
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.InternetIsnotAvailableException).MsgContent + vbCrLf + YourMessage
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.InternetIsnotAvailableException).MsgId
            End Sub

        End Class

        Public Class ConnectionIsNotAvailableException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ConnectionIsNotAvailableException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ConnectionIsNotAvailableException).MsgId
            End Sub

            Public Sub New(YourMessage As String)
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ConnectionIsNotAvailableException).MsgContent + vbCrLf + YourMessage
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ConnectionIsNotAvailableException).MsgId
            End Sub

        End Class


    End Namespace


End Namespace