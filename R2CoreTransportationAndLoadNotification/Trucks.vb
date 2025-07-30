

Imports System.Data.SqlClient
Imports System.Reflection

Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PublicProc
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.Drivers
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.Announcements.Exceptions
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions


Namespace Trucks
    Public Class R2CoreTransportationAndLoadNotificationStandardTruckStructure
        Public Sub New()
            MyBase.New()
            _NSSCar = Nothing
            _SmartCardNo = String.Empty
        End Sub

        Public Sub New(ByVal YourNSSCar As R2StandardCarStructure, ByVal YourSmartCardNo As String)
            _NSSCar = YourNSSCar
            _SmartCardNo = YourSmartCardNo
        End Sub

        Public Property NSSCar() As R2StandardCarStructure
        Public Property SmartCardNo() As String

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTrucksManager

        Dim _DateTimeService As New R2DateTimeService

        Public Function GetNSSTruck(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Top 1 Cars.nIDCar,CarTypes.LoaderTypeName,strCarName,Cars.StrBodyNo
                          from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                           Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                           Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                           Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                           Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                           Inner Join dbtransport.dbo.tbCarType as CarTypes On Cars.snCarType=CarTypes.snCarType 
                         Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and
                               EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000'))  
                         Order By CarAndPersons.nIDCarAndPerson Desc", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
                NSS.NSSCar = InstanceCars.GetNSSCar(DS.Tables(0).Rows(0).Item("nIdCar"))
                NSS.NSSCar.snCarType = DS.Tables(0).Rows(0).Item("LoaderTypeName").trim() + " - " + DS.Tables(0).Rows(0).Item("strCarName").trim()
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch ex As GetNSSException
                Throw ex
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementsubGroupsTitle(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of String)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select AHSGs.AHSGTitle from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars AS AHSGsCars
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On AHSGsCars.AHSGId=AHSGs.AHSGId 
                         Where AHSGsCars.CarId=" & YourNSSTruck.NSSCar.nIdCar & " and AHSGsCars.RelationActive=1 and AHSGs.Deleted=0
                               and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                         Order By Priority Asc,AHSGsCars.RelationId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New AnnouncementsubGroupRelationTruckNotExistException
                End If
                Dim Lst As List(Of String) = New List(Of String)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(Ds.Tables(0).Rows(Loopx).Item("AHSGTitle").trim)
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruck(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim DS As New DataSet
                Dim Da As New SqlClient.SqlDataAdapter
                If YourImmediately Then
                    Da.SelectCommand = New SqlCommand("Select Trucks.* from dbtransport.dbo.tbEnterExit as Turns 
                                                         Inner Join dbtransport.dbo.TbCar as Trucks On Turns.strCardno=Trucks.nIDCar 
                                                       Where Turns.nEnterExitId=" & YourNSSTurn.nEnterExitId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(DS) = 0 Then Throw New TruckNotFoundException
                Else
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select Trucks.* from dbtransport.dbo.tbEnterExit as Turns 
                             Inner Join dbtransport.dbo.TbCar as Trucks On Turns.strCardno=Trucks.nIDCar 
                          Where Turns.nEnterExitId=" & YourNSSTurn.nEnterExitId & "", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                End If
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = New R2StandardCarStructure(DS.Tables(0).Rows(0).Item("nIdCar"), DS.Tables(0).Rows(0).Item("snCarType"), DS.Tables(0).Rows(0).Item("StrCarNo").trim, DS.Tables(0).Rows(0).Item("StrCarSerialNo").trim, DS.Tables(0).Rows(0).Item("nIdCity"))
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruck(YourNSSLoadAllocation As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim DS As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select Cars.* from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                            Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId
                            Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar
                          Where LoadAllocations.LAId=" & YourNSSLoadAllocation.LAId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TruckNotFoundException
                End If
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = New R2StandardCarStructure(DS.Tables(0).Rows(0).Item("nIdCar"), DS.Tables(0).Rows(0).Item("snCarType"), DS.Tables(0).Rows(0).Item("StrCarNo").trim, DS.Tables(0).Rows(0).Item("StrCarSerialNo").trim, DS.Tables(0).Rows(0).Item("nIdCity"))
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementsubGroups(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourImmediately As Boolean) As List(Of Int64)
            Try
                Dim Ds As New DataSet
                Dim Da As New SqlClient.SqlDataAdapter
                If YourImmediately Then
                    Da.SelectCommand = New SqlCommand(
                        "Select AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGsCars 
                         Where CarId=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1
                               and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                         Order By Priority Asc,AHSGsCars.RelationId Desc")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(Ds) = 0 Then Throw New AnnouncementsubGroupRelationTruckNotExistException
                Else
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGsCars 
                         Where CarId=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1
                               and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                         Order By Priority Asc,AHSGsCars.RelationId Desc", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New AnnouncementsubGroupRelationTruckNotExistException
                    End If
                End If
                Dim Lst As List(Of Int64) = New List(Of Int64)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(Ds.Tables(0).Rows(Loopx).Item("AHSGId"))
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementsubGroup(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            Try
                Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Dim Ds As New DataSet
                If InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                   "Select Top 1 AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGsCars  
                    Where CarId=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1 and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000'))
                    Order By Priority Asc,AHSGsCars.RelationId Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New AnnouncementsubGroupRelationTruckNotExistException
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(InstanceAnnouncements.GetNSSAnnouncementsubGroup(Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("AHSGId"))))
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsExistCarTruckWithLicensePlate(YourDirtyTruckNSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure, ByRef TruckId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                    "Select Top 1 nIdCar,StrCarNo,StrCarSerialNo from dbtransport.dbo.TbCar Where strCarNo='" & YourDirtyTruckNSS.NSSCar.StrCarNo & "' and strCarSerialNo='" & YourDirtyTruckNSS.NSSCar.StrCarSerialNo & "' and ViewFlag=1 Order By nIDCar Desc", 0, DS, New Boolean).GetRecordsCount <> 0 Then
                    TruckId = DS.Tables(0).Rows(0).Item("nIdCar")
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckWithLicensePlate(YourDirtyTruckNSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 nIdCar,StrCarNo,StrCarSerialNo from dbtransport.dbo.TbCar Where strCarNo='" & YourDirtyTruckNSS.NSSCar.StrCarNo & "' and strCarSerialNo='" & YourDirtyTruckNSS.NSSCar.StrCarSerialNo & "' and ViewFlag=1 Order By nIDCar Desc", 0, DS, New Boolean).GetRecordsCount = 0 Then Throw New TruckNotFoundException
                Return GetNSSTruck(Convert.ToInt64(DS.Tables(0).Rows(0).Item("nIdCar")))
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSDefaultTruck() As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Return GetNSSTruck(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 1))
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruck(YourTruckId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from dbtransport.dbo.TbCar Where nIdCar=" & YourTruckId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = InstanceCars.GetNSSCar(Convert.ToInt64(DS.Tables(0).Rows(0).Item("nIdCar")))
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTrucksManagement
        Public Shared Function GetNSSDefaultTruck() As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Return GetNSSTruck(R2CoreMClassConfigurationManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 1))
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTruck(YourTruckId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from dbtransport.dbo.TbCar Where nIdCar=" & YourTruckId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = R2CoreParkingSystemMClassCars.GetNSSCar(DS.Tables(0).Rows(0).Item("nIdCar"))
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTruck(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Top 1 Cars.nIDCar,CarTypes.LoaderTypeName,strCarName,Cars.StrBodyNo
                          from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                           Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                           Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                           Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                           Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                           Inner Join dbtransport.dbo.tbCarType as CarTypes On Cars.snCarType=CarTypes.snCarType 
                         Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and
                               EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000'))   
                         Order By CarAndPersons.nIDCarAndPerson Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = R2CoreParkingSystemMClassCars.GetNSSCar(DS.Tables(0).Rows(0).Item("nIdCar"))
                NSS.NSSCar.snCarType = DS.Tables(0).Rows(0).Item("LoaderTypeName").trim() + " - " + DS.Tables(0).Rows(0).Item("strCarName").trim()
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementsubGroup(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            Try
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                   "Select Top 1 AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGsCars  
                    Where CarId=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1 and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000'))  
                    Order By Priority Asc,AHSGsCars.RelationId Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New AnnouncementsubGroupRelationTruckNotExistException
                End If
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroup(Ds.Tables(0).Rows(Loopx).Item("AHSGId")))
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementsubGroupsTitle(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of String)
            Try
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select AHSGs.AHSGTitle from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars AS AHSGsCars
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On AHSGsCars.AHSGId=AHSGs.AHSGId 
                         Where AHSGsCars.CarId=" & YourNSSTruck.NSSCar.nIdCar & " and AHSGsCars.RelationActive=1 and AHSGs.Deleted=0
                               and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                         Order By Priority Asc,AHSGsCars.RelationId Desc", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New AnnouncementsubGroupRelationTruckNotExistException
                End If
                Dim Lst As List(Of String) = New List(Of String)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(Ds.Tables(0).Rows(Loopx).Item("AHSGTitle").trim)
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementsubGroups(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of Int64)
            Try
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGsCars
                         Where CarId=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1
                               and ((DATEDIFF(SECOND,AHSGsCars.RelationTimeStamp,getdate())<240) or (AHSGsCars.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                         Order By Priority Asc,AHSGsCars.RelationId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New AnnouncementsubGroupRelationTruckNotExistException
                End If
                Dim Lst As List(Of Int64) = New List(Of Int64)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(Ds.Tables(0).Rows(Loopx).Item("AHSGId"))
                Next
                Return Lst
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTruck(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "
                 Select Trucks.nIDCar,Trucks.StrBodyNo from dbtransport.dbo.tbEnterExit as Turns Inner Join dbtransport.dbo.TbCar as Trucks On Turns.strCardno=Trucks.nIDCar 
                  Where Turns.nEnterExitId=" & YourNSSTurn.nEnterExitId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTruckStructure = New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSS.NSSCar = R2CoreParkingSystemMClassCars.GetNSSCar(DS.Tables(0).Rows(0).Item("nIdCar"))
                NSS.SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo")
                Return NSS
            Catch exx As TruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementsubGroup(YourTruckId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure
            Try
                Return GetNSSAnnouncementsubGroup(GetNSSTruck(YourTruckId))(0)
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub DisabledAllTruckRelationAnnouncementsubGroups(YourTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars Set RelationActive=0 Where CarId=" & YourTruck.NSSCar.nIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub SetTruckRelationAnnouncementsubGroup(YourTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourNSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Try
                    If GetAnnouncementsubGroups(YourTruck).Where(Function(x) x = YourNSSAnnouncementsubGroup.AHSGId).Count <> 0 Then Exit Sub
                Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Catch ex As Exception
                    Throw ex
                End Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars Set RelationActive=0 
                                        Where CarId=" & YourTruck.NSSCar.nIdCar & " and AHSGId=" & YourNSSAnnouncementsubGroup.AHSGId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select Top 1 AnnouncementsubGroupsRelationCars.Priority from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AnnouncementsubGroupsRelationCars
                                      Where AnnouncementsubGroupsRelationCars.CarId=" & YourTruck.NSSCar.nIdCar & " and AnnouncementsubGroupsRelationCars.RelationActive=1 
                                      and ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) or (RelationTimeStamp='2015-01-01 00:00:00.000'))
                                      Order By AnnouncementsubGroupsRelationCars.Priority Desc,RelationId Desc"
                Dim PriorityAnnouncementsubGroups = CmdSql.ExecuteScalar + 1
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars(CarId,AHSGId,RelationActive,Priority,RelationTimeStamp) Values(" & YourTruck.NSSCar.nIdCar & "," & YourNSSAnnouncementsubGroup.AHSGId & ",1," & PriorityAnnouncementsubGroups & ",'2015-01-01 00:00:00.000')"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Shared Sub SetTruckRelationAnnouncementHall(YourTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure)
        '    Dim CmdSql As New SqlCommand
        '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
        '    Try
        '        Dim Lst = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetAnnouncementsAnnouncementsubGroupsJOINT().Where(Function(x) x.NSSAnnounementHall.AHId = YourNSSAnnouncementHall.AHId)
        '        CmdSql.Connection.Open()
        '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction
        '        For Loopx As Int16 = 0 To Lst.Count - 1
        '            CmdSql.CommandText = "Select Top 1 AnnouncementsubGroupsRelationCars.Priority from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AnnouncementsubGroupsRelationCars
        '                                   Where AnnouncementsubGroupsRelationCars.CarId=" & YourTruck.NSSCar.nIdCar & " and AnnouncementsubGroupsRelationCars.RelationActive=1
        '                                   Order By AnnouncementsubGroupsRelationCars.Priority Desc"
        '            Dim PriorityAnnouncementsubGroups = CmdSql.ExecuteScalar + 1
        '            CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars Set RelationActive=0 
        '                                Where CarId=" & YourTruck.NSSCar.nIdCar & " and AHSGId=" & Lst(Loopx).NSSAnnouncementsubGroup.AHSGId & ""
        '            CmdSql.ExecuteNonQuery()
        '            CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars(CarId,AHSGId,RelationActive,Priority) Values(" & YourTruck.NSSCar.nIdCar & "," & Lst(Loopx).NSSAnnouncementsubGroup.AHSGId & ",1," & PriorityAnnouncementsubGroups & ")"
        '            CmdSql.ExecuteNonQuery()
        '        Next
        '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
        '    Catch ex As Exception
        '        If CmdSql.Connection.State <> ConnectionState.Closed Then
        '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '        End If
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub




    End Class

    Namespace Exceptions
        Public Class TruckNotFoundException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.TruckNotFoundException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.TruckNotFoundException).MsgId
            End Sub
        End Class

        Public Class AnnouncementsubGroupRelationTruckNotExistException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات پایه - ارتباط بین ناوگان باری با زیرگروه سالن اعلام بار مشخص نیست"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    <System.Xml.Serialization.XmlTypeAttribute()>
    Public Class R2CoreTransportationAndLoadNotificationTruck
        Public TruckId As Int64
        Public LoaderTypeId As Int64
        Public Pelak As String
        Public Serial As String
        Public SmartCardNo As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationComposedTruckInf
        Public Truck As R2CoreTransportationAndLoadNotificationTruck
        Public TruckDriver As R2CoreTransportationAndLoadNotificationTruckDriver
        Public Turn As R2CoreTransportationAndLoadNotificationTurnExtended
        Public MoneyWallet As R2CoreMoneyWallet
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTrucksManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetTruck(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTruck
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select * from dbtransport.dbo.TbCar Where nIdCar=" & YourTruckId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select * from dbtransport.dbo.TbCar Where nIdCar=" & YourTruckId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTruck With {.TruckId = DS.Tables(0).Rows(0).Item("nIdCar"), .LoaderTypeId = DS.Tables(0).Rows(0).Item("snCarType"), .SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo").trim, .Pelak = DS.Tables(0).Rows(0).Item("strCarNo").trim, .Serial = DS.Tables(0).Rows(0).Item("strCarSerialNo").trim}
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckBySmartCardNo(YourSmartCardNo As String) As R2CoreTransportationAndLoadNotificationTruck
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSmartCardNo)

                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
               "Select Top 1 * from dbtransport.dbo.TbCar Where StrBodyNo='" & YourSmartCardNo.Trim() & "' and ViewFlag=1 Order By nIdCar Desc",
                                                          3600, Ds, New Boolean).GetRecordsCount() <> 0 Then
                    Return New R2CoreTransportationAndLoadNotificationTruck With {.TruckId = Ds.Tables(0).Rows(0).Item("nIdCar"), .Pelak = Ds.Tables(0).Rows(0).Item("StrCarNo").trim, .Serial = Ds.Tables(0).Rows(0).Item("StrCarSerialNo").trim, .LoaderTypeId = Ds.Tables(0).Rows(0).Item("snCarType"), .SmartCardNo = Ds.Tables(0).Rows(0).Item("StrBodyNo").trim}
                Else
                    Throw New TruckNotFoundException
                End If
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckBySoftwareUser(YourSoftwareUserId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTruck
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select Top 1 Cars.*
                    from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                      Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                      Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                      Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                      Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                    Where SoftwareUsers.UserId=" & YourSoftwareUserId & "  and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                          EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & "  and EntityRelations.RelationActive=1 and 
	                      Cars.ViewFlag=1 and 
	                      CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000'))  
                          Order By CarAndPersons.nIDCarAndPerson Desc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select Top 1 Cars.*
                    from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                      Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                      Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                      Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                      Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                    Where SoftwareUsers.UserId=" & YourSoftwareUserId & "  and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and 
                          EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & "  and EntityRelations.RelationActive=1 and 
	                      Cars.ViewFlag=1 and 
	                      CarAndPersons.snRelation=2 and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000'))  
                          Order By CarAndPersons.nIDCarAndPerson Desc", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTruck With {.TruckId = DS.Tables(0).Rows(0).Item("nIdCar"), .LoaderTypeId = DS.Tables(0).Rows(0).Item("snCarType"), .SmartCardNo = DS.Tables(0).Rows(0).Item("StrBodyNo").trim, .Pelak = DS.Tables(0).Rows(0).Item("strCarNo").trim, .Serial = DS.Tables(0).Rows(0).Item("strCarSerialNo").trim}
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Select Cars.nIDCar as [Car.TruckId],Cars.snCarType as [Car.LoaderTypeId],Cars.strCarNo as [Car.Pelak],Cars.strCarSerialNo as [Car.Serial],Cars.strBodyNo as [Car.SmartCardNo],
        '       Drivers.nIDDriver as [Driver.DriverId],Persons.strPersonFullName as [Driver.NameFamily],Persons.strNationalCode as [Driver.NationalCode],Persons.strIDNO as [Driver.MobileNumber],
        '    Persons.strFatherName as [Driver.FatherName],Drivers.strDrivingLicenceNo as [Driver.DrivingLicenseNo],Persons.strAddress as [Driver.address],Drivers.strSmartcardNo as [Driver.SmartCardNo],
        '    MoneyWallets.CardId as [MoneyWallet.CardId],MoneyWallets.CardNo as [MoneyWallet.CardNo],MoneyWallets.Charge as [MoneyWallet.Balance]
        'from DBTransport.dbo.TbCar as Cars
        '   Inner Join DBTransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar
        '   Inner Join DBTransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
        '   Inner Join DBTransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
        '   Inner Join R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars as TrafficCardsRelationCars On cars.nIDCar=TrafficCardsRelationCars.nCarId 
        '   Inner Join R2Primary.dbo.TblRFIDCards as MoneyWallets On TrafficCardsRelationCars.CardId=MoneyWallets.CardId 
        'Where Cars.strBodyNo = '15161718' and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 
        'for Json Auto

        Public Function GetComposedTruckInf(YourTruckId As Int64, YourLastTurnIsActive As Boolean, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationComposedTruckInf
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_DateTimeService)
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationTruckDriversManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)

                Dim Truck = InstanceTrucks.GetTruck(YourTruckId, YourImmediately)
                Dim TruckDriver As R2CoreTransportationAndLoadNotificationTruckDriver
                Dim Turn As R2CoreTransportationAndLoadNotificationTurnExtended
                Dim MoneyWallet As R2CoreMoneyWallet
                Try
                    TruckDriver = InstanceTruckDrivers.GetTruckDriverfromTruckId(YourTruckId, YourImmediately)
                Catch ex As TruckDriverNotFoundException
                    TruckDriver = Nothing
                Catch ex As Exception
                    Throw ex
                End Try
                Try
                    If YourLastTurnIsActive Then
                        Turn = InstanceTurns.GetLastActiveTurnfromTruckId(YourTruckId, YourImmediately)
                    Else
                        Turn = InstanceTurns.GetLastTurnfromTruckId(YourTruckId, YourImmediately)
                    End If
                Catch ex As TurnNotFoundException
                    Turn = Nothing
                Catch ex As Exception
                    Throw ex
                End Try
                Try
                    MoneyWallet = InstanceMoneyWallet.GetMoneyWalletfromCarId(YourTruckId, YourImmediately)
                Catch ex As MoneyWalletNotExistException
                    MoneyWallet = Nothing
                Catch ex As Exception
                    Throw ex
                End Try
                Return New R2CoreTransportationAndLoadNotificationComposedTruckInf With {.Truck = Truck, .TruckDriver = TruckDriver, .Turn = Turn, .MoneyWallet = MoneyWallet}
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SetComposedTruckInf(YourTruckId As Int64, YourTruckDriverId As Int64, YourCardId As Int64, Optional YourTurnId As Int64 = Int64.MinValue)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceTrafficCardsManager
                Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
                Dim InstanceDrivers = New R2CoreParkingSystemInstanceDriversManager
                Dim MoneyWallet = InstanceMoneyWallet.GetNSSTrafficCard(YourCardId)
                Dim TruckDriver = InstanceDrivers.GetNSSDriver(YourTruckDriverId)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                ' حذف روابط قبلی کیف پول با پلاک های دیگر
                CmdSql.CommandText = "Update R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Set RelationActive=0 Where CardId=" & MoneyWallet.CardId & ""
                CmdSql.ExecuteNonQuery()
                'حذف روابط قبلی پلاک با کیف پول دیگر
                CmdSql.CommandText = "Update R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Set RelationActive=0 Where nCarId=" & YourTruckId & ""
                CmdSql.ExecuteNonQuery()
                'ایجاد رابطه جدید کیف پول و پلاک
                CmdSql.CommandText = "Insert into R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars(CardId,nCarId,RelationActive,RelationTimeStamp) Values(" & MoneyWallet.CardId & "," & YourTruckId & ",1,'2015-01-01 00:00:00.000')"
                CmdSql.ExecuteNonQuery()
                'حذف ارتباط راننده در صورت وجود با ناوگان ها
                CmdSql.CommandText = "Delete Dbtransport.dbo.TbCarAndPerson Where nIdPerson=" & YourTruckDriverId & ""
                CmdSql.ExecuteNonQuery()
                'حذف ارتباط پلاک  با راننده ها
                CmdSql.CommandText = "Delete Dbtransport.dbo.TbCarAndPerson Where nIdCar=" & YourTruckId & ""
                CmdSql.ExecuteNonQuery()
                'ایجاد رابطه جدید راننده با پلاک
                CmdSql.CommandText = "Insert into Dbtransport.dbo.TbCarAndPerson(nIdCar,nIdPerson,snRelation,dDate,RelationTimeStamp) Values(" & YourTruckId & "," & YourTruckDriverId & ",2,'" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "','2015-01-01 00:00:00.000')"
                CmdSql.ExecuteNonQuery()
                'تغییر کد ونام راننده و کد ناوگان در جدول نوبت
                If YourTurnId <> InstancePublicProcedures.GetNullIntEquivalentforIds Then
                    CmdSql.CommandText = "Update Dbtransport.dbo.TbEnterExit Set StrDriverName='" & TruckDriver.StrPersonFullName & "',nDriverCode=" & TruckDriver.nIdPerson & ",StrCardNo=" & YourTruckId & " Where  nEnterExitId=" & YourTurnId & ""
                    CmdSql.ExecuteNonQuery()
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As GetNSSException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TerraficCardNotFoundException
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

        Public Function GetDefaultTruck() As R2CoreTransportationAndLoadNotificationTruck
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Return GetTruck(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 1), False)
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class


End Namespace
