

Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Web.UI.Design.WebControls

Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.EntityRelationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.PermissionManagement
Imports R2Core.PublicProc
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SQLInjectionPrevention
Imports R2CoreParkingSystem.Drivers
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions


Namespace TruckDrivers
    Public Class R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
        Public Sub New()
            MyBase.New()
            _NSSDriver = Nothing
            _StrSmartCardNo = String.Empty
        End Sub

        Public Sub New(ByVal YourNSSDriver As R2StandardDriverStructure, ByVal YourStrSmartCardNo As String)
            _NSSDriver = YourNSSDriver
            _StrSmartCardNo = YourStrSmartCardNo
        End Sub

        Public Property NSSDriver() As R2StandardDriverStructure
        Public Property StrSmartCardNo() As String

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager

        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Function GetNSSTruckDriver(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                         Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Persons.nIDPerson=CarAndPersons.nIDPerson
                      Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                            EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 
                            and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                            Order By CarAndPersons.nIDCarAndPerson Desc", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure = New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                Dim InstanceDrivers = New R2CoreParkingSystemInstanceDriversManager
                NSS.NSSDriver = InstanceDrivers.GetNSSDriver(DS.Tables(0).Rows(0).Item("nIdDriver"))
                NSS.StrSmartCardNo = DS.Tables(0).Rows(0).Item("StrSmartCardNo")
                Return NSS
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckDriver(YourTruckDriverId As Int64, YourImediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                          "Select Top 1 * from dbtransport.dbo.TbDriver as Drivers 
                             Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                           Where Drivers.nIDDriver=" & YourTruckDriverId & " Order By Drivers.nIDDriver Desc", IIf(YourImediately, 0, 300), DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                NSS.NSSDriver = New R2StandardDriverStructure(DS.Tables(0).Rows(0).Item("nIDDriver"), DS.Tables(0).Rows(0).Item("strPersonFullName").trim, DS.Tables(0).Rows(0).Item("strNationalCode").trim, DS.Tables(0).Rows(0).Item("strFatherName").trim, DS.Tables(0).Rows(0).Item("strAddress").trim, DS.Tables(0).Rows(0).Item("strIDNO").trim, DS.Tables(0).Rows(0).Item("strDrivingLicenceNo").trim)
                NSS.StrSmartCardNo = DS.Tables(0).Rows(0).Item("StrSmartCardNo").trim
                Return NSS
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckDriverWithTruckId(YourTruckId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 Drivers.nIdDriver,Drivers.StrSmartCardNo from dbtransport.dbo.TbCar as Cars
                              Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
                              Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
                              Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
                      Where  Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and CarAndPersons.snRelation=2  
                             and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                      Order By CarAndPersons.nIDCarAndPerson Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure = New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                NSS.NSSDriver = R2CoreParkingSystemMClassDrivers.GetNSSDriver(DS.Tables(0).Rows(0).Item("nIdDriver"))
                NSS.StrSmartCardNo = DS.Tables(0).Rows(0).Item("StrSmartCardNo")
                Return NSS
            Catch ex As DriverNotFoundException
                Throw ex
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckDriverWithLicensePlate(YourDirtyNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim TruckId As Int64 = Nothing
                If InstanceTrucks.IsExistCarTruckWithLicensePlate(YourDirtyNSSTruck, TruckId) Then
                    Return GetNSSTruckDriverWithTruckId(TruckId)
                Else
                    Throw New TruckNotFoundException
                End If
            Catch ex As Exception When TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is DriverNotFoundException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSDefaultTruckDriver() As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                Return GetNSSTruckDriver(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 2), True)
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTruckDriversManagement

        Private Shared _DateTimeService As New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetNSSDefaultTruckDriver() As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Return GetNSSTruckDriver(R2CoreMClassConfigurationManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 2))
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTruckDriver(YourTruckDriverId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from dbtransport.dbo.TbDriver Where nIdDriver=" & YourTruckDriverId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure = New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                NSS.NSSDriver = R2CoreParkingSystemMClassDrivers.GetNSSDriver(DS.Tables(0).Rows(0).Item("nIdDriver"))
                NSS.StrSmartCardNo = DS.Tables(0).Rows(0).Item("StrSmartCardNo")
                Return NSS
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTruckDriver(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers As SoftwareUsers
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                         Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Persons.nIDPerson=CarAndPersons.nIDPerson
                      Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                            EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPersons.snRelation=2 
                            and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                            Order By CarAndPersons.nIDCarAndPerson Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure = New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                NSS.NSSDriver = R2CoreParkingSystemMClassDrivers.GetNSSDriver(DS.Tables(0).Rows(0).Item("nIdDriver"))
                NSS.StrSmartCardNo = DS.Tables(0).Rows(0).Item("StrSmartCardNo")
                Return NSS
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsExistTruckDriver(YourMobileNumber As String) As Boolean
            Try
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select nIDPerson from dbtransport.dbo.TbPerson Where strIDNO='" & YourMobileNumber.Trim & "'", 1, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub TruckDriverMobileEditing(YourSmartCardNo As String, YourMobileNumber As String)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbPerson Set strIDNO='" & YourMobileNumber & "' 
                                      Where nIDPerson=(Select Top 1 nIDDriver from dbtransport.dbo.TbDriver Where strSmartcardNo='" & YourSmartCardNo & "' Order By nIDDriver Desc)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class TruckDriverNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "راننده ناوگان باری با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class SoftwareUserfromTruckDriverNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری بر اساس مشخصات راننده یافت نشد "
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    <System.Xml.Serialization.XmlTypeAttribute()>
    Public Class R2CoreTransportationAndLoadNotificationTruckDriver
        Public DriverId As Int64
        Public NameFamily As String
        Public NationalCode As String
        Public MobileNumber As String
        Public FatherName As String
        Public DrivingLicenseNo As String
        Public Address As String
        Public SmartCardNo As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTruckDriversManager

        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Function GetTruckDriverBySoftwareUser(YourSoftwareUserId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTruckDriver
            Try
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                     "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                         Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Persons.nIDPerson=CarAndPersons.nIDPerson
                      Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                            EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 
                            and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                            Order By CarAndPersons.nIDCarAndPerson Desc")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckDriverNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                         Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Persons.nIDPerson=CarAndPersons.nIDPerson
                      Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                            EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 
                            and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                            Order By CarAndPersons.nIDCarAndPerson Desc", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTruckDriver With
                         {.DriverId = DS.Tables(0).Rows(0).Item("nIdDriver"),
                          .NameFamily = DS.Tables(0).Rows(0).Item("strPersonFullName").trim,
                          .MobileNumber = DS.Tables(0).Rows(0).Item("strIDNO").trim,
                          .NationalCode = DS.Tables(0).Rows(0).Item("strNationalCode").trim,
                          .FatherName = DS.Tables(0).Rows(0).Item("strFatherName").trim,
                          .Address = DS.Tables(0).Rows(0).Item("strAddress").trim,
                          .DrivingLicenseNo = DS.Tables(0).Rows(0).Item("strDrivingLicenceNo").trim,
                          .SmartCardNo = DS.Tables(0).Rows(0).Item("strSmartcardNo").trim}
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckDriverfromTruckId(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTruckDriver
            Try
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                     "Select Top 1 Drivers.nIdDriver,Persons.strPersonFullName ,Persons.strIDNO ,Persons.strNationalCode,Persons.strFatherName ,Persons.strAddress,Drivers.strDrivingLicenceNo,Drivers.strSmartcardNo , Drivers.StrSmartCardNo
                      from dbtransport.dbo.TbCar as Cars
                              Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
                              Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
                              Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
                      Where  Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and CarAndPersons.snRelation=2  
                             and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                      Order By CarAndPersons.nIDCarAndPerson Desc")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckDriverNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 Drivers.nIdDriver,Persons.strPersonFullName ,Persons.strIDNO ,Persons.strNationalCode,Persons.strFatherName ,Persons.strAddress,Drivers.strDrivingLicenceNo,Drivers.strSmartcardNo , Drivers.StrSmartCardNo
                      from dbtransport.dbo.TbCar as Cars
                              Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
                              Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
                              Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
                      Where  Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and CarAndPersons.snRelation=2  
                             and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                      Order By CarAndPersons.nIDCarAndPerson Desc", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                End If

                Return New R2CoreTransportationAndLoadNotificationTruckDriver With
                    {.DriverId = DS.Tables(0).Rows(0).Item("nIdDriver"),
                     .NameFamily = DS.Tables(0).Rows(0).Item("strPersonFullName").trim,
                     .MobileNumber = DS.Tables(0).Rows(0).Item("strIDNO").trim,
                     .NationalCode = DS.Tables(0).Rows(0).Item("strNationalCode").trim,
                     .FatherName = DS.Tables(0).Rows(0).Item("strFatherName").trim,
                     .Address = DS.Tables(0).Rows(0).Item("strAddress").trim,
                     .DrivingLicenseNo = DS.Tables(0).Rows(0).Item("strDrivingLicenceNo").trim,
                     .SmartCardNo = DS.Tables(0).Rows(0).Item("strSmartcardNo").trim}
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckDriverJSON(YourTruckDriverId As Int64) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select Top 1 Drivers.nIDDriver as DriverId,Persons.strPersonFullName as NameFamily,Persons.strNationalCode as NationalCode,Persons.strIDNO as MobileNumber,Persons.strFatherName as FatherName,Drivers.strDrivingLicenceNo as DrivingLicenceNo,Persons.strAddress as Address,Drivers.strSmartcardNo as SmartcardNo
                              from dbtransport.dbo.TbDriver as Drivers 
                                 Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                           Where Drivers.nIDDriver=" & YourTruckDriverId & " Order By Drivers.nIDDriver Desc for json path", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckDriver(YourTruckDriverId As Int64) As R2CoreTransportationAndLoadNotificationTruckDriver
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select Top 1 Drivers.nIDDriver as DriverId,Persons.strPersonFullName as NameFamily,Persons.strNationalCode as NationalCode,Persons.strIDNO as MobileNumber,Persons.strFatherName as FatherName,Drivers.strDrivingLicenceNo as DrivingLicenceNo,Persons.strAddress as Address,Drivers.strSmartcardNo as SmartcardNo
                              from dbtransport.dbo.TbDriver as Drivers 
                                 Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                           Where Drivers.nIDDriver=" & YourTruckDriverId & " Order By Drivers.nIDDriver Desc", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckDriverNotFoundException
                Return New R2CoreTransportationAndLoadNotificationTruckDriver With {.DriverId = DS.Tables(0).Rows(0).Item("DriverId"), .NameFamily = DS.Tables(0).Rows(0).Item("NameFamily").Trim, .NationalCode = DS.Tables(0).Rows(0).Item("NationalCode").Trim, .MobileNumber = DS.Tables(0).Rows(0).Item("MobileNumber").Trim, .FatherName = DS.Tables(0).Rows(0).Item("FatherName").Trim, .DrivingLicenseNo = DS.Tables(0).Rows(0).Item("DrivingLicenceNo").Trim, .Address = DS.Tables(0).Rows(0).Item("Address").Trim, .SmartCardNo = DS.Tables(0).Rows(0).Item("SmartCardNo").Trim}
            Catch exx As TruckDriverNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub RegisteringTruckDriverMobileNumber(YourTruckDriverId As Int64, YourMobileNumber As String)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, Nothing)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim UserId As Int64
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.Driver Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException
                    CmdSql.CommandText = "Update dbtransport.dbo.tbPerson Set strIDNO='" & YourMobileNumber & "' Where nIdPerson=" & YourTruckDriverId & ""
                    CmdSql.ExecuteNonQuery()
                    'ایجاد رابطه کاربر موجود و راننده موجود
                    R2CoreMClassEntityRelationManagement.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver, UserId, YourTruckDriverId, Nothing), RelationDeactiveTypes.BothDeactive)
                Else
                    Dim NSSDriver = R2CoreParkingSystemMClassDrivers.GetNSSDriver(YourTruckDriverId)
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourMobileNumber, .UserActive = True, .UserName = NSSDriver.StrPersonFullName, .UserTypeId = R2CoreParkingSystemSoftwareUserTypes.Driver}, False, InstanceSoftwareUsers.GetSystemUser.UserId)
                    CmdSql.CommandText = "Update dbtransport.dbo.tbPerson Set strIDNO='" & YourMobileNumber & "' Where nIdPerson=" & YourTruckDriverId & ""
                    CmdSql.ExecuteNonQuery()
                    'ایجاد رابطه کاربر موجود و راننده موجود
                    R2CoreMClassEntityRelationManagement.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver, UserId, YourTruckDriverId, Nothing), RelationDeactiveTypes.BothDeactive)
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی برای نوع کاربر راننده و ارسال به مدیریت مجوز
                    Dim ComposeSearchString As String = R2CoreParkingSystemSoftwareUserTypes.Driver.ToString + ":"
                    Dim AllofSoftwareUserTypes1 As String() = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    Dim AllofWebProcessesIds As String() = Split(Mid(AllofSoftwareUserTypes1.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes1.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofWebProcessesIds)
                    'به دست آوردن لیست گروههای فرآیند وب برای نوع کاربر راننده و ارسال آن به مدیریت روابط نهادی
                    Dim AllofSoftwareUserTypes2 As String() = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    Dim AllofWebProcessGroupsIds As String() = Split(Mid(AllofSoftwareUserTypes2.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes2.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofWebProcessGroupsIds)
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DriverNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SMSTypeIdNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SoftwareUserMobileNumberBelongsToSomeoneElseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Function GetSoftwareUserIdfromTruckDriverId(YourTruckDriverId As Int64) As Int64
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select SoftwareUsers.UserId from DBTransport.dbo.TbDriver as Drivers
                          Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Drivers.nIDDriver=EntityRelations.E2 
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On EntityRelations.E1=SoftwareUsers.UserId  
                        where Drivers.nIDDriver=" & YourTruckDriverId & " and EntityRelations.ERTypeId=" & R2CoreParkingSystem.EntityRelations.R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and SoftwareUsers.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New SoftwareUserfromTruckDriverNotFoundException
                Else
                    Return Ds.Tables(0).Rows(0).Item("UserId")
                End If
            Catch ex As SoftwareUserfromTruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSoftwareUserfromTruckDriverId(YourTruckDriverId As Int64) As R2CoreSoftwareUser
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select * from DBTransport.dbo.TbDriver as Drivers
                          Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Drivers.nIDDriver=EntityRelations.E2 
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On EntityRelations.E1=SoftwareUsers.UserId  
                        where Drivers.nIDDriver=" & YourTruckDriverId & " and EntityRelations.ERTypeId=" & R2CoreParkingSystem.EntityRelations.R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and SoftwareUsers.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New SoftwareUserfromTruckDriverNotFoundException
                Else
                    Return New R2CoreSoftwareUser With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .ApiKey = Ds.Tables(0).Rows(0).Item("ApiKey").trim, .APIKeyExpiration = Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .UserShenaseh = Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, .UserPassword = Ds.Tables(0).Rows(0).Item("UserPassword").trim, .UserPasswordExpiration = Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), .UserPinCode = Ds.Tables(0).Rows(0).Item("UserPinCode"), .UserCanCharge = Ds.Tables(0).Rows(0).Item("UserCanCharge"), .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId"), .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim}
                End If
            Catch ex As SoftwareUserfromTruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckDriverfromNationalCode(YourNationalCode As String, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTruckDriver
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourNationalCode)

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                         "Select Top 1 D.nIDDriver as DriverId,P.strPersonFullName as NameFamily,P.strNationalCode as NationalCode,p.strIDNO as MobileNumber,P.strFatherName as FatherName,d.strDrivingLicenceNo as DrivingLicenceNo,P.strAddress as Address,d.strSmartcardNo as SmartCardNo
                             from dbtransport.dbo.TbPerson as P inner join dbtransport.dbo.TbDriver as D On P.nIDPerson=D.nIDDriver 
                           Where P.StrNationalCode='" & YourNationalCode & "' Order By P.nIDPerson Desc")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TruckDriverNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                          "Select Top 1 D.nIDDriver as DriverId,P.strPersonFullName as NameFamily,P.strNationalCode as NationalCode,p.strIDNO as MobileNumber,P.strFatherName as FatherName,d.strDrivingLicenceNo as DrivingLicenceNo,P.strAddress as Address,d.strSmartcardNo as SmartCardNo
                             from dbtransport.dbo.TbPerson as P inner join dbtransport.dbo.TbDriver as D On P.nIDPerson=D.nIDDriver 
                           Where P.StrNationalCode='" & YourNationalCode & "' Order By P.nIDPerson Desc", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                        Throw New TruckDriverNotFoundException
                    End If
                End If
                Return New R2CoreTransportationAndLoadNotificationTruckDriver With {.DriverId = Ds.Tables(0).Rows(0).Item("DriverId"), .NameFamily = Ds.Tables(0).Rows(0).Item("NameFamily").trim, .NationalCode = Ds.Tables(0).Rows(0).Item("NationalCode").trim, .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim, .FatherName = Ds.Tables(0).Rows(0).Item("FatherName").trim, .DrivingLicenseNo = Ds.Tables(0).Rows(0).Item("DrivingLicenceNo").trim, .Address = Ds.Tables(0).Rows(0).Item("Address").trim, .SmartCardNo = Ds.Tables(0).Rows(0).Item("SmartCardNo").trim}
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetDefaultTruckDriver() As R2CoreTransportationAndLoadNotificationTruckDriver
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                Return GetTruckDriver(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 2))
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace
