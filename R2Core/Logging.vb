


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

Namespace LoggingManagement

    Public MustInherit Class R2CoreLogType
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Note As Int64 = 1
        Public Shared ReadOnly Property Warn As Int64 = 2
        Public Shared ReadOnly Property Fail As Int64 = 3
        Public Shared ReadOnly Property Info As Int64 = 4
        Public Shared ReadOnly Property SuccessfulUserLogin As Int64 = 5
        Public Shared ReadOnly Property Print As Int64 = 6
        Public Shared ReadOnly Property RegisterRecords As Int64 = 7
        Public Shared ReadOnly Property Delete As Int64 = 8
        Public Shared ReadOnly Property Update As Int64 = 9
        Public Shared ReadOnly Property CameraError As Int64 = 10
        Public Shared ReadOnly Property TimeStamp As Int64 = 11
        Public Shared ReadOnly Property SendSMSResult As Int64 = 67
    End Class

    Public Class R2CoreStandardLogTypeStructure

        Public Sub New()
            _LogId = Int64.MinValue
            _LogName = String.Empty
            _LogTitle = String.Empty
            _LogColor = Color.White
            _Core = String.Empty
            _AssemblyDll = String.Empty
            _AssemblyPath = String.Empty
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourLogId As Int64, YourLogName As String, YourLogTitle As String, YourLogColor As Color, YourCore As String, YourAssemblyDll As String, YourAssemblyPath As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourDeleted As Boolean)
            _LogId = YourLogId
            _LogName = YourLogName
            _LogTitle = YourLogTitle
            _LogColor = YourLogColor
            _Core = YourCore
            _AssemblyDll = YourAssemblyDll
            _AssemblyPath = YourAssemblyPath
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property LogId As Int64
        Public Property LogName As String
        Public Property LogTitle As String
        Public Property LogColor As Color
        Public Property Core As String
        Public Property AssemblyDll As String
        Public Property AssemblyPath As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreStandardLoggingStructure

        Public Sub New()
            MyBase.New()
            _LogId = Int64.MinValue
            _LogType = R2CoreLogType.None
            _Sharh = String.Empty
            _Optional1 = String.Empty
            _Optional2 = String.Empty
            _Optional3 = String.Empty
            _Optional4 = String.Empty
            _Optional5 = String.Empty
            _UserId = Int64.MinValue
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
        End Sub

        Public Sub New(ByVal YourLogId As Int64, ByVal YourLogType As Int64, ByVal YourSharh As String, YourOptional1 As String, YourOptional2 As String, YourOptional3 As String, YourOptional4 As String, YourOptional5 As String, ByVal YourUserId As Int64, ByVal YourDateTimeMilladi As DateTime, ByVal YourDateShamsi As String)
            _LogId = YourLogId
            _LogType = YourLogType
            _Sharh = YourSharh
            _Optional1 = YourOptional1
            _Optional2 = YourOptional2
            _Optional3 = YourOptional3
            _Optional4 = YourOptional4
            _Optional5 = YourOptional5
            _UserId = YourUserId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
        End Sub



        Public Property LogId() As Int64
        Public Property LogType() As Int64
        Public Property Sharh() As String
        Public Property Optional1() As String
        Public Property Optional2() As String
        Public Property Optional3() As String
        Public Property Optional4() As String
        Public Property Optional5() As String
        Public Property UserId() As Int64
        Public Property DateShamsi() As String
        Public Property DateTimeMilladi() As DateTime




    End Class

    Public Class R2CoreStandardLoggingExtendedStructure
        Inherits R2CoreStandardLoggingStructure
        Public Sub New()
            MyBase.New()
            _UserName = String.Empty
            _LogTypeColor = Color.Empty
        End Sub

        Public Sub New(ByVal YourNSSLog As R2CoreStandardLoggingStructure, ByVal YourUserName As String, ByVal YourLogTypeColor As Color)
            MyBase.New(YourNSSLog.LogId, YourNSSLog.LogType, YourNSSLog.Sharh, YourNSSLog.Optional1, YourNSSLog.Optional2, YourNSSLog.Optional3, YourNSSLog.Optional4, YourNSSLog.Optional5, YourNSSLog.UserId, YourNSSLog.DateTimeMilladi, YourNSSLog.DateShamsi)
            _UserName = YourUserName
            _LogTypeColor = YourLogTypeColor
        End Sub

        Public Property UserName As String
        Public Property LogTypeColor As Color

    End Class

    'BPTChanged
    Public Class R2CoreRawLog
        Public LogTypeId As Int64
        Public Description As String
        Public MessageDetail1 As String
        Public MessageDetail2 As String
        Public MessageDetail3 As String
        Public UserId As Int64
    End Class

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


    Public MustInherit Class R2CoreLoggingManager

        Private Shared _Logger As Logger = Nothing
        Private Shared _Subscriber As ISubscriber = Nothing

        Public Shared Sub RegisterLog(YourPubSubChannel As RedisChannel, YourRawLog As R2CoreRawLog)
            Try
                If _Subscriber Is Nothing Then _Subscriber = RedisConnectorHelper.Connection.GetSubscriber()
                _Subscriber.Publish(YourPubSubChannel, JsonConvert.SerializeObject(YourRawLog))
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub RegisterLog(YourPubSubChannel As RedisChannel, YourException As Exception)
            Try
                If _Subscriber Is Nothing Then _Subscriber = RedisConnectorHelper.Connection.GetSubscriber()
                _Subscriber.Publish(YourPubSubChannel, JsonConvert.SerializeObject(YourException))
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub WriteInfLog(YourLog As R2CoreRawLog)
            Try
                If _Logger Is Nothing Then _Logger = LogManager.GetCurrentClassLogger()
                _Logger.WithProperty("LogTypeId", YourLog.LogTypeId).WithProperty("MessageDetail1", YourLog.MessageDetail1).WithProperty("MessageDetail2", YourLog.MessageDetail2).WithProperty("MessageDetail3", YourLog.MessageDetail3).WithProperty("UserId", YourLog.UserId).Info(YourLog.Description)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub WriteErrorLog(YourException As Exception)
            Try
                If _Logger Is Nothing Then _Logger = LogManager.GetCurrentClassLogger()
                _Logger.Error(YourException, YourException.Message)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Shared Sub Logger(Yourvalue As StackExchange.Redis.RedisValue)
            Try
                Dim RawLog = JsonConvert.DeserializeObject(Of R2CoreRawLog)(Yourvalue)
                If (RawLog.LogTypeId = R2Core.LoggingManagement.R2CoreLogType.None) Then
                    Dim Err = JsonConvert.DeserializeObject(Of Exception)(Yourvalue)
                    WriteErrorLog(Err)
                Else
                    WriteInfLog(RawLog)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

    'BPTChanged
    Namespace ExceptionManagement
        Public Class LoggingProcessForExceptionLogFailedException
            Inherits BPTException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ثبت لاگ خطا با مشکل مواجه شد"
                End Get
            End Property
        End Class

        Public Class LoggingProcessForInfLogFailedException
            Inherits BPTException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ثبت لاگ با مشکل مواجه شد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace
