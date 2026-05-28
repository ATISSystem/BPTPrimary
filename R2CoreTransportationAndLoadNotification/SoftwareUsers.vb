

Imports Microsoft.SqlServer
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Reflection
Imports System.Windows.Forms
Imports StackExchange.Redis

Imports R2Core.Caching
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports R2Core.SessionManagement
Imports R2CoreTransportationAndLoadNotification.Caching
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports R2Core.PermissionManagement
Imports R2Core.CachHelper
Imports R2Core.DateTimeProvider
Imports R2Core.LoggingManagement

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

        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Function GetNSSSoftwareUser(YourTransportCompanyId As Int64) As R2CoreStandardSoftwareUserStructure
            Dim InstanceSoftwareUser As New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
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


    'BPTChanged
    Namespace Exceptions

        Public Class SoftwareUserRelatedThisTransportCompanyNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مشخصات کاربری مرتبط  با شرکت حمل و نقل مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        'BPTChanged
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
    Public Class R2CoreTransportationAndLoadNotificationSoftwareUsersManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Private _ILogger As ILogger
        Private _RCH As RedisConnectorHelper

        Public Sub New(YourR2DateTimeService As IDateTimeService)
            _DateTimeService = YourR2DateTimeService
            _ILogger = New R2CorenLogService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            _RCH = New RedisConnectorHelper
        End Sub

        Public Function GetSoftwareUser(YourTurnId As Int64, Immediately As Boolean) As R2CoreSoftwareUser
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                         "Select Top 1 SoftwareUsers.UserId From R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
    	                     Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1
                             Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                             Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                             Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                             Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
							 Inner Join DBTransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                          Where SoftwareUsers.UserActive = 1 And SoftwareUsers.Deleted = 0 And EntityRelations.ERTypeId = 2 And EntityRelations.RelationActive = 1 And Cars.ViewFlag = 1 And CarAndPersons.snRelation = 2 
                             And ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) Or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000'))  
						   	 And Turns.nEnterExitId=" & YourTurnId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New SoftwareUserRelatedThisTruckNotFoundException
                Return InstanceSoftwareUsers.GetUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")), False)
            Catch ex As SoftwareUserRelatedThisTransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class



End Namespace
