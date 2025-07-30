

Imports Microsoft.SqlServer
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports R2Core.Caching
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports System.Reflection
Imports System.Windows.Forms
Imports StackExchange.Redis
Imports R2Core.SessionManagement
Imports R2CoreTransportationAndLoadNotification.Caching
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports R2Core.PermissionManagement

Namespace SoftwareUserManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationSoftwareUserTypes
        Inherits R2CoreParkingSystemSoftwareUserTypes

        Public Shared ReadOnly Property TruckOwner As Int64 = 4
        Public Shared ReadOnly Property TruckersAssociation As Int64 = 5
        Public Shared ReadOnly Property TransportCompaniesAssociation As Int64 = 6
        Public Shared ReadOnly Property TransportCompany As Int64 = 7
        Public Shared ReadOnly Property WareHouses As Int64 = 8
        Public Shared ReadOnly Property BillOfLadingIssuingCompany As Int64 = 14
        Public Shared ReadOnly Property FactoriesAndProductionCenters As Int64 = 15
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceSoftwareUsersManager
        Public Function GetNSSSoftwareUser(YourTransportCompanyId As Int64) As R2CoreStandardSoftwareUserStructure
            Dim InstanceSqlDataBOX As New R2CoreInstanseSqlDataBOXManager
            Dim InstanceSoftwareUser As New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCsRSoftwareUsers On SoftwareUsers.UserId=TCsRSoftwareUsers.UserId 
                          Where SoftwareUsers.Deleted=0 and TCsRSoftwareUsers.RelationActive=1 and TCsRSoftwareUsers.TCId=" & YourTransportCompanyId & "  Order by SoftwareUsers.UserId", 0, Ds, New Boolean).GetRecordsCount = 0 Then Throw New SoftwareUserRelatedThisTransportCompanyNotFoundException
                Return InstanceSoftwareUser.GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
            Catch ex As SoftwareUserRelatedThisTransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Namespace Exceptions
        Public Class SoftwareUserRelatedThisTransportCompanyNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مشخصات کاربری مرتبط  با شرکت حمل و نقل مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserRelatedThisTruckNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مشخصات کاربری مرتبط  با ناوگان مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTruckDriverTurnInfo
        Public SeqTId As Int64
        Public NativenessTypeId As Int64
        Public TurnId As Int64
        Public TurnStatusId As Int64
    End Class

    Public Class R2CoreTransportationAndLoadNotificationSoftwareUsersManager

        Public Sub SetTruckDriversTurnInfo()
            Try
                Dim InstanceCache = New R2CoreCacheManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePermissions As New R2CoreInstansePermissionsManager

                Dim Server = RedisConnectorHelper.GetServer
                Dim Keys = Server.Keys(R2CoreCatchDataBases.SoftwareUserSessions)

                For Each Key In Keys
                    Dim Content = JsonConvert.DeserializeObject(Of R2CoreSessionIdSoftwareUser)(InstanceCache.GetCache(Key, R2CoreCatchDataBases.SoftwareUserSessions).ToString)
                    If Content.SoftWareUser Is Nothing Or Content.SessionId = String.Empty Then Continue For

                    'کنترل وجود اطلاعات در کش
                    If InstanceCache.ExistCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TruckDriverTurnInfo).CacheTypeName + Content.SoftWareUser.UserId.ToString(), R2CoreTransportationAndLoadNotificationCatchDataBases.TruckDriverInformation) Then Continue For

                    'دریافت اطلاعات راننده از کش
                    Dim TurnInfo As R2CoreTransportationAndLoadNotificationTruckDriverTurnInfo = Nothing
                    'فقط برای سشن رانندگان این فرآیند اجرا می گردد
                    If Content.SoftWareUser.UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.Driver Then
                        Dim DSPreInformations As DataSet = Nothing
                        Dim DataChanged As Boolean
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                                  "Select Top 1 Turns.nEnterExitId as TurnId,Turns.TurnStatus as TurnStatusId,Cars.CarNativenessTypeId as NativenessTypeId,SequentialTurns.SeqTId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                                      Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                                      Inner Join dbtransport.dbo.TbPerson as Persons On EntityRelations.E2 =Persons.nIDPerson
                                      Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPerson On Persons.nIDPerson=CarAndPerson.nIDPerson 
                                      Inner Join dbtransport.dbo.TbCar as Cars On CarAndPerson.nIDCar=Cars.nIDCar 
                                      Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SequentialTurns.SeqTKeyWord  Collate Arabic_CI_AI_WS 
                                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
                                   Where SoftwareUsers.UserId=" & Content.SoftWareUser.UserId & " and SoftwareUsers.UserActive=1 and EntityRelations.ERTypeId=" & R2CoreParkingSystem.EntityRelations.R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPerson.snRelation=2 and Cars.ViewFlag=1 and SequentialTurns.Active=1 and TruckNativenessTypes.Active=1
                                       and ((DATEDIFF(SECOND,CarAndPerson.RelationTimeStamp,getdate())<240) Or (CarAndPerson.RelationTimeStamp='2015-01-01 00:00:00.000'))
                                       and (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Turns.bFlagDriver=0
                                   Order By Turns.nEnterExitId Desc", 300, DSPreInformations, DataChanged).GetRecordsCount <> 0 Then
                            TurnInfo = New R2CoreTransportationAndLoadNotificationTruckDriverTurnInfo With {.TurnId = DSPreInformations.Tables(0).Rows(0).Item("TurnId"), .TurnStatusId = DSPreInformations.Tables(0).Rows(0).Item("TurnStatusId"), .SeqTId = DSPreInformations.Tables(0).Rows(0).Item("SeqTId"), .NativenessTypeId = DSPreInformations.Tables(0).Rows(0).Item("NativenessTypeId")}
                        End If
                        If TurnInfo IsNot Nothing Then
                            InstanceCache.RemoveCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TruckDriverTurnInfo).CacheTypeName + Content.SoftWareUser.UserId.ToString())
                            InstanceCache.SetCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TruckDriverTurnInfo).CacheTypeName + Content.SoftWareUser.UserId.ToString(), TurnInfo, R2CoreTransportationAndLoadNotificationCacheTypes.TruckDriverTurnInfo, R2CoreTransportationAndLoadNotificationCatchDataBases.TruckDriverInformation)
                        End If
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class


End Namespace
