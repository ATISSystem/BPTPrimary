


Imports Newtonsoft.Json
Imports StackExchange.Redis
Imports System.CodeDom
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web

Imports R2Core.CachHelper
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.GeneralConfiguration
Imports R2Core.ExceptionManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.PredefinedMessagesManagement.Exceptions

Namespace CachHelper

    Public Class RedisConnectorHelper

        Private InstanceGeneralConfiguration As R2CoreGeneralConfigurationManager
        Private _DateTimeService As IDateTimeService

        Public Sub New()
            Try
                _DateTimeService = New R2DateTimeService
                InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Private ReadOnly Property RedisHost As String
            Get
                Try
                    Return InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.Caching, 0)
                Catch ex As FileNotExistException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Get
        End Property

        Public ReadOnly lazyConnection As New Lazy(Of ConnectionMultiplexer)(
        Function()
            Try
                Return ConnectionMultiplexer.Connect(RedisHost)
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function)

        Public ReadOnly Property Connection As ConnectionMultiplexer
            Get
                Try
                    Return lazyConnection.Value
                Catch ex As RedisException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property GetServer As IServer
            Get
                Try
                    Dim EndPoint = Connection.GetEndPoints().First()
                    Return Connection.GetServer(EndPoint)
                Catch ex As RedisException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

    End Class

End Namespace

Namespace Caching

    Public MustInherit Class R2CoreCatchDataBases
        Public Shared ReadOnly Property SoftwareUserSessions As Int64 = 0
        Public Shared ReadOnly Property Carousels As Int64 = 2
        Public Shared ReadOnly Property GeneralAnnounceSMSRequests As Int64 = 4
    End Class

    Public MustInherit Class R2CoreCacheTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Session As Int64 = 1
        Public Shared ReadOnly Property Carousel As Int64 = 3
        Public Shared ReadOnly Property SoftwareUserVerify As Int64 = 5
        Public Shared ReadOnly Property GeneralAnnounceSMSRequest As Int64 = 6
    End Class

    Public Class R2CoreRawCacheType
        Public Property CacheTypeId As Int64
        Public Property CacheTypeName As String
        Public Property CacheTime As Int64
        Public Property Active As Boolean
    End Class

    Public Class R2CoreCacheManager

        Private _DateTimeService As IDateTimeService
        Private _RCH As RedisConnectorHelper

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                _RCH = New RedisConnectorHelper
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared _Cache(100) As IDatabase
        Private Function GetDataBase(YourDataBaseId As Integer) As IDatabase
            Try
                If _Cache(YourDataBaseId) Is Nothing Then _Cache(YourDataBaseId) = _RCH.Connection.GetDatabase(YourDataBaseId)
                Return _Cache(YourDataBaseId)
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function GetCacheType(YourCacheTypeId As Int64) As R2CoreRawCacheType
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblCacheTypes Where CacheTypeId=" & YourCacheTypeId & "", 10000, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New CacheTypeNotFoundException
                Else
                    Return New R2CoreRawCacheType With {.CacheTypeId = DS.Tables(0).Rows(0).Item("CacheTypeId"), .CacheTypeName = DS.Tables(0).Rows(0).Item("CacheTypeName").trim, .CacheTime = DS.Tables(0).Rows(0).Item("CacheTime"), .Active = DS.Tables(0).Rows(0).Item("Active")}
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As CacheTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function ExistCache(YourKeyId As String, YourDataBaseId As Integer) As Boolean
            Try

                Dim Cache = _RCH.Connection.GetDatabase(YourDataBaseId)
                If Cache.KeyExists(YourKeyId) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub SetCache(YourKeyId As String, YourCacheValue As Object, YourCacheTypeId As Int64, YourDataBaseId As Integer, YourIndefiniteTimeSpan As Boolean)
            Try
                Dim Cache = _RCH.Connection.GetDatabase(YourDataBaseId)
                If Not YourIndefiniteTimeSpan Then
                    Cache.StringSet(YourKeyId, JsonConvert.SerializeObject(YourCacheValue), TimeSpan.FromSeconds(GetCacheType(YourCacheTypeId).CacheTime))
                Else
                    Cache.StringSet(YourKeyId, JsonConvert.SerializeObject(YourCacheValue))
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As CacheTypeNotFoundException
                Throw ex
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetCache(YourKeyId As String, YourDataBaseId As Integer) As Object
            Try
                Return GetDataBase(YourDataBaseId).StringGet(YourKeyId)
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub RemoveCache(YourCacheKey As String, YourDataBaseId As Integer)
            Try
                Dim Cache = _RCH.Connection.GetDatabase(YourDataBaseId)
                Cache.KeyDelete(YourCacheKey)
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class CacheTypeNotFoundException
        Inherits BPTException
        Public Sub New()
            Try
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CacheTypeNotFound).MsgId
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CacheTypeNotFound).MsgContent
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
    End Class

    Public Class CacheNotFoundException
        Inherits BPTException
        Public Sub New()
            Try
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CacheNotFound).MsgId
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CacheNotFound).MsgContent
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
    End Class

End Namespace

Namespace PubSubMessaging

    Public MustInherit Class R2CorePubSubChannels
        Public Shared ReadOnly Property None As String = "None"
        Public Shared ReadOnly Property UserAuthenticated As String = "UserAuthenticated"
        Public Shared ReadOnly Property Logging As String = "Logging"
        Public Shared ReadOnly Property GeneralAnnounceSMSRequest As String = "GeneralAnnounceSMSRequest"

    End Class


End Namespace
