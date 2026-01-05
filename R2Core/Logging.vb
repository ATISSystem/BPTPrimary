


Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.LoggingManagement.ExceptionManagement
Imports System.Drawing
Imports System.Reflection
Imports NLog
Imports StackExchange.Redis
Imports R2Core.CachHelper
Imports Newtonsoft.Json
Imports R2Core.MobileProcessesManagement.Exceptions
Imports Newtonsoft.Json.Linq
Imports R2Core.PubSubMessaging
Imports System.Security.Cryptography
Imports R2Core.DateTimeProvider

Namespace LoggingManagement

    'BPTChanged
    Public Interface ILogger
        Sub RegisterInfLog(RawLog As R2CoreRawLog)
        Sub RegisterExceptionLog(Exception As Exception)
        Sub WriteInfLog(RawLog As R2CoreRawLog)
        Sub WriteErrorLog(Exception As Exception)
    End Interface

    'BPTChanged
    Public Class R2CoreRawLog
        Public LogTypeId As Int64
        Public Description As String
        Public MessageDetail1 As String
        Public MessageDetail2 As String
        Public MessageDetail3 As String
        Public UserId As Int64
    End Class

    'BPTChanged
    Public MustInherit Class R2CoreLogTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property UserSuccessLogin As Int64 = 3
        Public Shared ReadOnly Property UserUnSuccessLogin As Int64 = 4

    End Class

    'BPTChanged
    Public Class R2CoreLog
        Public LogId As Int64
        Public LogTypeId As Int64
        Public Description As String
        Public MessageDetail1 As String
        Public MessageDetail2 As String
        Public MessageDetail3 As String
        Public UserId As Int64
        Public DateTimeMilladi As DateTime
        Public DateShamsi As String
        Public Time As String
    End Class

    'BPTChanged
    Public Class R2CorenLogService
        Implements ILogger

        Private _Logger As Logger = Nothing
        Private _Subscriber As ISubscriber = Nothing
        Private _RCH = New RedisConnectorHelper

        Public Sub RegisterInfLog(YourRawLog As R2CoreRawLog) Implements ILogger.RegisterInfLog
            Try
                _Subscriber = _RCH.Connection.GetSubscriber()
                _Subscriber.Publish(R2CorePubSubChannels.Logging, JsonConvert.SerializeObject(YourRawLog))
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub RegisterExceptionLog(YourException As Exception) Implements ILogger.RegisterExceptionLog
            Try
                _Subscriber = _RCH.Connection.GetSubscriber()
                _Subscriber.Publish(R2CorePubSubChannels.Logging, JsonConvert.SerializeObject(YourException))
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub WriteInfLog(YourLog As R2CoreRawLog) Implements ILogger.WriteInfLog
            Try
                If _Logger Is Nothing Then _Logger = LogManager.GetCurrentClassLogger()
                _Logger.WithProperty("LogTypeId", YourLog.LogTypeId).WithProperty("MessageDetail1", YourLog.MessageDetail1).WithProperty("MessageDetail2", YourLog.MessageDetail2).WithProperty("MessageDetail3", YourLog.MessageDetail3).WithProperty("UserId", YourLog.UserId).Info(YourLog.Description)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub WriteErrorLog(YourException As Exception) Implements ILogger.WriteErrorLog
            Try
                If _Logger Is Nothing Then _Logger = LogManager.GetCurrentClassLogger()
                _Logger.Error(YourException, YourException.Message)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

    'BPTChanged
    Public Class R2CoreLoggingManager


        Private _Logger As ILogger

        Public Sub New()
            _Logger = New R2CorenLogService
        End Sub

        Public Sub Logger(Yourvalue As StackExchange.Redis.RedisValue)
            Try
                Dim RawLog = JsonConvert.DeserializeObject(Of R2CoreRawLog)(Yourvalue)
                If (RawLog.LogTypeId = R2Core.LoggingManagement.R2CoreLogTypes.None) Then
                    Dim Err = JsonConvert.DeserializeObject(Of Exception)(Yourvalue)
                    _Logger.WriteErrorLog(Err)
                Else
                    _Logger.WriteInfLog(RawLog)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

    'BPTChanged
    Namespace ExceptionManagement

    End Namespace



End Namespace
