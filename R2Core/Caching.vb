


Imports Newtonsoft.Json
Imports R2Core.CachHelper
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports StackExchange.Redis
Imports System.Reflection
Imports System.Web

Namespace CachHelper
    Public NotInheritable Class RedisConnectorHelper

        Private Shared ReadOnly Property RedisHost As String
            Get
                Return R2CoreConfigurationManagement.GetConfigString(R2Core.ConfigurationManagement.R2CoreConfigurations.Caching, 0)
            End Get
        End Property

        Public Shared ReadOnly lazyConnection As New Lazy(Of ConnectionMultiplexer)(
        Function()
            Try
                Return ConnectionMultiplexer.Connect(RedisHost)
            Catch ex As RedisException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function)

        Public Shared ReadOnly Property Connection As ConnectionMultiplexer
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

        Public Shared ReadOnly Property GetServer As IServer
            Get
                Try
                    Return Connection.GetServer(RedisHost)
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
    End Class

    Public MustInherit Class R2CoreCacheTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Session As Int64 = 1
        Public Shared ReadOnly Property Carousel As Int64 = 3
    End Class

    Public Class R2CoreStandardCacheTypeStructure

        Public Sub New()
            MyBase.New()
            _CacheTypeId = Int64.MinValue
            _CacheTypeName = String.Empty
            _CacheTime = String.Empty
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _Core = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourCacheTypeId As Int64, YourCacheTypeName As String, YourCacheTime As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourCore As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _CacheTypeId = YourCacheTypeId
            _CacheTypeName = YourCacheTypeName
            _CacheTime = YourCacheTime
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _Core = YourCore
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property CacheTypeId As Int64
        Public Property CacheTypeName As String
        Public Property CacheTime As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property Core As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreCacheManager

        Public Function GetCacheType(YourCacheTypeId As Int64) As R2CoreStandardCacheTypeStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2Primary.dbo.TblCacheTypes Where CacheTypeId=" & YourCacheTypeId & "", 10000, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New CacheTypeNotFoundException

                Else
                    Return New R2CoreStandardCacheTypeStructure(DS.Tables(0).Rows(0).Item("CacheTypeId"), DS.Tables(0).Rows(0).Item("CacheTypeName").trim, DS.Tables(0).Rows(0).Item("CacheTime"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As CacheTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function ExistCache(YourKeyId As String, YourDataBaseId As Integer) As Boolean
            Try
                Dim Cache = CachHelper.RedisConnectorHelper.Connection.GetDatabase(YourDataBaseId)
                If Cache.KeyExists(YourKeyId) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub SetCache(YourKeyId As String, YourCacheValue As Object, YourCacheTypeId As Int64, YourDataBaseId As Integer, YourIndefiniteTimeSpan As Boolean)
            Try
                Dim Cache = CachHelper.RedisConnectorHelper.Connection.GetDatabase(YourDataBaseId)
                If Not YourIndefiniteTimeSpan Then
                    Cache.StringSet(YourKeyId, JsonConvert.SerializeObject(YourCacheValue), TimeSpan.FromSeconds(GetCacheType(YourCacheTypeId).CacheTime))
                Else
                    Cache.StringSet(YourKeyId, JsonConvert.SerializeObject(YourCacheValue))
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetCache(YourKeyId As String, YourDataBaseId As Integer) As Object
            Try
                Dim Cache = CachHelper.RedisConnectorHelper.Connection.GetDatabase(YourDataBaseId)
                Return Cache.StringGet(YourKeyId)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub RemoveCache(YourCacheKey As String, YourDataBaseId As Integer)
            Try
                Dim Cache = CachHelper.RedisConnectorHelper.Connection.GetDatabase(YourDataBaseId)
                Cache.KeyDelete(YourCacheKey)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class CacheTypeNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "کلید شاخص بافرینگ یا کش در سیستم وجود ندارد"
            End Get
        End Property
    End Class

    Public Class CacheNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعلت مورد نظر در کش سیستم یافت نشد"
            End Get
        End Property
    End Class

End Namespace

Namespace PubSubMessaging

    Public MustInherit Class R2CorePubSubChannels
        Public Shared ReadOnly Property None As String = "None"
        Public Shared ReadOnly Property UserAuthenticated As String = "UserAuthenticated"
        Public Shared ReadOnly Property Logging As String = "Logging"
    End Class


End Namespace
