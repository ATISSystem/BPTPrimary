
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.VisualBasic.ApplicationServices
Imports Newtonsoft.Json
Imports R2Core.BaseStandardClass
Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.EntityRelationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.LoggingManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PermissionManagement
Imports R2Core.PermissionManagement.Exceptions
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.PublicProc
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SiteIsBusy
Imports R2Core.SMS.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.WebProcessesManagement.Exceptions
Imports R2CoreGUI
Imports R2CoreParkingSystem.BlackList
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreParkingSystem.ProvincesAndCities.Execption
Imports R2CoreParkingSystem.Drivers
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.Announcements.Exceptions
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports R2CoreTransportationAndLoadNotification.CalendarManagement.SpecializedPersianCalendar
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.DriverSelfDeclaration.Exceptions
Imports R2CoreTransportationAndLoadNotification.FileShareRawGroupsManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.FailedLoadAllocationPrinting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorAccounting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadOtherThanManipulation
Imports R2CoreTransportationAndLoadNotification.LoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces
Imports R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission
Imports R2CoreTransportationAndLoadNotification.LoadPermission.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission.LoadPermissionPrinting
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadSources
Imports R2CoreTransportationAndLoadNotification.LoadSources.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadTargets
Imports R2CoreTransportationAndLoadNotification.LoadTargets.Exceptions
Imports R2CoreTransportationAndLoadNotification.Logging
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.RequesterManagement
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportCompanies
Imports R2CoreTransportationAndLoadNotification.TransportCompanies.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportTariffs
Imports R2CoreTransportationAndLoadNotification.TransportTariffs.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckLoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness
Imports R2CoreTransportationAndLoadNotification.TurnAttendance
Imports R2CoreTransportationAndLoadNotification.TurnAttendance.Exceptions
Imports R2CoreTransportationAndLoadNotification.TurnCancellation
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports R2CoreTransportationAndLoadNotification.TravelTime
Imports R2CoreTransportationAndLoadNotification.TurnRegisterRequest
Imports R2CoreTransportationAndLoadNotification.Turns.TurnPrinting
Imports R2Core.ComputerMessagesManagement
Imports R2CoreTransportationAndLoadNotification.ComputerMessages
Imports R2CoreTransportationAndLoadNotification.EntityRelations
Imports System.CodeDom
Imports R2CoreParkingSystem.DataBaseManagement
Imports R2CoreTransportationAndLoadNotification.Turns.TurnAccounting
Imports R2CoreParkingSystem.AccountingManagement


Namespace Turns

    Public Enum TurnType
        None = 0
        Temporary = 1 'موقتی
        Permanent = 2 'دائمی
    End Enum

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnStatusStructure
        Public Sub New()
            _TurnStatusId = Int64.MinValue
            _TurnStatusTitle = String.Empty
            _TurnStatusColor = String.Empty
        End Sub

        Public Sub New(ByVal YourTurnStatusId As Int64, ByVal YourTurnStatusTitle As String, YourTurnStatusColor As String)
            _TurnStatusId = YourTurnStatusId
            _TurnStatusTitle = YourTurnStatusTitle
            _TurnStatusColor = YourTurnStatusColor
        End Sub

        Public Property TurnStatusId As Int64
        Public Property TurnStatusTitle As String
        Public Property TurnStatusColor As String
        Public Property Description As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnStructure
        Public Sub New()
            MyBase.New()
            _nEnterExitId = Int64.MinValue
            _EnterDate = String.Empty
            _EnterTime = String.Empty
            _NSSTruckDriver = Nothing
            _bFlagDriver = True
            _nUserIdEnter = Int64.MinValue
            _BillOfLadingNumber = String.Empty
            _OtaghdarTurnNumber = String.Empty
            _StrCardNo = Int64.MinValue
            _TurnStatus = Turns.TurnStatuses.None
            _RegisteringTimeStamp = DateTime.Now
        End Sub

        Public Sub New(ByVal YournEnterExitId As Int64, ByVal YourEnterDate As String, YourEnterTime As String, YourNSSTruckDriver As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure, YourbFlagDriver As Boolean, YournUserIdEnter As Int64, YourBillOfLadingNumber As String, YourOtaghdarTurnNumber As String, YourStrCardNo As Int64, YourTurnStatus As Int64, YourRegisteringTimeStamp As DateTime)
            _nEnterExitId = YournEnterExitId
            _EnterDate = YourEnterDate
            _EnterTime = YourEnterTime
            _NSSTruckDriver = YourNSSTruckDriver
            _bFlagDriver = YourbFlagDriver
            _nUserIdEnter = YournUserIdEnter
            _BillOfLadingNumber = YourBillOfLadingNumber
            _OtaghdarTurnNumber = YourOtaghdarTurnNumber
            _StrCardNo = YourStrCardNo
            _TurnStatus = YourTurnStatus
            _RegisteringTimeStamp = YourRegisteringTimeStamp
        End Sub

        Public Property nEnterExitId As Int64
        Public Property EnterDate As String
        Public Property EnterTime As String
        Public Property NSSTruckDriver As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
        Public Property bFlagDriver As Boolean
        Public Property nUserIdEnter As Int64
        Public Property BillOfLadingNumber As String
        Public Property OtaghdarTurnNumber As String
        Public Property StrCardNo As Int64
        Public Property TurnStatus As Int64
        Public Property RegisteringTimeStamp As DateTime

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
        Inherits R2CoreTransportationAndLoadNotificationStandardTurnStructure
        Public Sub New()
            MyBase.New()
            _LicensePlatePString = String.Empty
            _TurnStatusTitle = String.Empty
            _UserName = String.Empty
            _TruckDriver = String.Empty
            _LastChangedDate = String.Empty
        End Sub

        Public Sub New(ByVal YournEnterExitId As Int64, ByVal YourEnterDate As String, YourEnterTime As String, YourNSSTruckDriver As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure, YourbFlagDriver As Boolean, YournUserIdEnter As Int64, YourBillOfLadingNumber As String, YourOtaghdarTurnNumber As String, YourStrCardNo As Int64, YourTurnStatus As Int64, YourRegisteringTimeStamp As DateTime, YourLicensePlatePString As String, YourTurnStatusTitle As String, YourUserName As String, YourTruckDriver As String, YourLastChangedDate As String)
            MyBase.New(YournEnterExitId, YourEnterDate, YourEnterTime, YourNSSTruckDriver, YourbFlagDriver, YournUserIdEnter, YourBillOfLadingNumber, YourOtaghdarTurnNumber, YourStrCardNo, YourTurnStatus, YourRegisteringTimeStamp)
            _LicensePlatePString = YourLicensePlatePString
            _TurnStatusTitle = YourTurnStatusTitle
            _UserName = YourUserName
            _TruckDriver = YourTruckDriver
            _LastChangedDate = YourLastChangedDate
        End Sub

        Public Property LicensePlatePString As String
        Public Property TurnStatusTitle As String
        Public Property UserName As String
        Public Property TruckDriver As String
        Public Property LastChangedDate As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnCreditStructure
        Public Sub New()
            MyBase.New()
            SeqTId = Int64.MinValue
            UserId = Int64.MinValue
            SignificantTurnId = Int64.MinValue
            OtaghdarTurnNumber = String.Empty
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            Time = String.Empty
            Active = Boolean.FalseString
            ViewFlag = Boolean.FalseString
            Deleted = Boolean.TrueString
        End Sub

        Public Sub New(YourSeqTId As Int64, YourUserId As Int64, YourSignificantTurnId As Int64, YourOtaghdarTurnNumber As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            SeqTId = YourSeqTId
            UserId = YourUserId
            SignificantTurnId = YourSignificantTurnId
            OtaghdarTurnNumber = YourOtaghdarTurnNumber
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Time = YourTime
            Active = YourActive
            ViewFlag = YourViewFlag
            Deleted = YourDeleted
        End Sub

        Public Property SeqTId As Int64
        Public Property UserId As Int64
        Public Property SignificantTurnId As Int64
        Public Property OtaghdarTurnNumber As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardActiveTurnStructure
        Inherits R2CoreTransportationAndLoadNotificationStandardTurnStructure

        Public Sub New()
            MyBase.New
            TruckSmartCardNo = String.Empty
        End Sub

        Public Sub New(YourTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourTruckSmartCardNo As String, YourPelak As String, YourSerial As String)
            MyBase.New(YourTurn.nEnterExitId, YourTurn.EnterDate, YourTurn.EnterTime, YourTurn.NSSTruckDriver, YourTurn.bFlagDriver, YourTurn.nUserIdEnter, YourTurn.BillOfLadingNumber, YourTurn.OtaghdarTurnNumber, YourTurn.StrCardNo, YourTurn.TurnStatus, YourTurn.RegisteringTimeStamp)
            _TruckSmartCardNo = YourTruckSmartCardNo
            _Pelak = YourPelak
            _Serial = YourSerial
        End Sub

        Public Property TruckSmartCardNo As String
        Public Property Pelak As String
        Public Property Serial As String


    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTurnsManager
        Private _DateTime As New R2DateTime

        Public Function GetAllOfCurrentActiveTurns() As List(Of R2CoreTransportationAndLoadNotificationStandardActiveTurnStructure)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select  Turns.*,Cars.strBodyNo as TruckSmartCardNo,Cars.strCarNo as Pelak,Cars.strCarSerialNo as Serial  from dbtransport.dbo.tbEnterExit as Turns
                                 Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                                 Where Cars.ViewFlag=1 and (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10)
                                 Order By Turns.nEnterExitId", 0, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyActiveTurnNotExistException
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardActiveTurnStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardActiveTurnStructure(New R2CoreTransportationAndLoadNotification.Turns.R2CoreTransportationAndLoadNotificationStandardTurnStructure(DS.Tables(0).Rows(Loopx).Item("nEnterExitId"), DS.Tables(0).Rows(Loopx).Item("StrEnterDate").trim, DS.Tables(0).Rows(Loopx).Item("StrEnterTime").trim, InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("nDriverCode")), False), DS.Tables(0).Rows(Loopx).Item("bFlagDriver"), DS.Tables(0).Rows(Loopx).Item("nUserIdEnter"), DS.Tables(0).Rows(Loopx).Item("BillOfLadingNumber").trim, DS.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").trim, DS.Tables(0).Rows(Loopx).Item("StrCardNo"), DS.Tables(0).Rows(Loopx).Item("TurnStatus"), DS.Tables(0).Rows(Loopx).Item("RegisteringTimeStamp")), DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"), DS.Tables(0).Rows(Loopx).Item("Pelak"), DS.Tables(0).Rows(Loopx).Item("Serial")))
                Next
                Return Lst
            Catch ex As AnyActiveTurnNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTurnStatus(YourTurnStatusId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnStatusStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses Where TurnStatusId=" & YourTurnStatusId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnStatusNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnStatusStructure = New R2CoreTransportationAndLoadNotificationStandardTurnStatusStructure
                NSS.TurnStatusId = Ds.Tables(0).Rows(0).Item("TurnStatusId")
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle")
                NSS.TurnStatusColor = Ds.Tables(0).Rows(0).Item("TurnStatusColor")
                NSS.Description = Ds.Tables(0).Rows(0).Item("Description")
                Return NSS
            Catch ex As TurnStatusNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetFirstActiveTurn(YourNSSSequentialTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim DS As DataSet = Nothing
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                  Select Top 1 * from dbtransport.dbo.tbEnterExit as Turns
                  Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") 
                        and substring(Turns.OtaghdarTurnNumber,1,1)='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' 
                  Order By Turns.nEnterExitId", 300, DS, New Boolean).GetRecordsCount = 0 Then Throw New FirstActiveTurnNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTurnStructure(DS.Tables(0).Rows(0).Item("nEnterExitId"), DS.Tables(0).Rows(0).Item("StrEnterDate").trim, DS.Tables(0).Rows(0).Item("StrEnterTime").trim, InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(DS.Tables(0).Rows(0).Item("nDriverCode")), False), DS.Tables(0).Rows(0).Item("bFlagDriver"), DS.Tables(0).Rows(0).Item("nUserIdEnter"), DS.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, DS.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, DS.Tables(0).Rows(0).Item("StrCardNo"), DS.Tables(0).Rows(0).Item("TurnStatus"), DS.Tables(0).Rows(0).Item("RegisteringTimeStamp"))
            Catch ex As FirstActiveTurnNotFoundException
                Throw New FirstActiveTurnNotFoundException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLastActiveTurn(YourNSSCar As R2StandardCarStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from dbtransport.dbo.TbEnterExit Where StrCardNo=" & YourNSSCar.nIdCar & " and (bFlagDriver=0) Order By nEnterExitId desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New LastActiveTurnNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTurnStructure(Ds.Tables(0).Rows(0).Item("nEnterExitId"), Ds.Tables(0).Rows(0).Item("StrEnterDate"), Ds.Tables(0).Rows(0).Item("StrEnterTime"), InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), False), Ds.Tables(0).Rows(0).Item("bFlagDriver"), Ds.Tables(0).Rows(0).Item("nUserIdEnter"), Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, Ds.Tables(0).Rows(0).Item("StrCardNo"), Ds.Tables(0).Rows(0).Item("TurnStatus"), Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp"))
            Catch ex As LastActiveTurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurns(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select  (Select Count(*) from dbtransport.dbo.tbEnterExit as TurnsX
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On SUBSTRING(TurnsX.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS
                             Where SeqT.Active=1 and SeqT.Deleted=0 and SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS=SUBSTRING(DataBox.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS and TurnsX.nEnterExitId<DataBox.nEnterExitId and (TurnsX.TurnStatus=1 or TurnsX.TurnStatus=7 or TurnsX.TurnStatus=8 or TurnsX.TurnStatus=9 or TurnsX.TurnStatus=10)) as TurnDistanceToValidity,DataBox.*
                    from
                      (Select Top 5 Turns.nEnterExitId,Turns.StrEnterDate,Turns.StrEnterTime,Turns.nDriverCode,Turns.bFlagDriver,Turns.nUserIdEnter,Turns.OtaghdarTurnNumber,Turns.StrCardNo,
                                    Turns.TurnStatus,Cars.strCarNo +'-'+ Cars.strCarSerialNo as LPString,Persons.strPersonFullName,TurnStatuses.TurnStatusTitle,SoftwareUsers.UserName as Username,Turns.RegisteringTimeStamp
                       from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
	                     Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1
	                     Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                         Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                         Inner Join dbtransport.dbo.tbCarType as CarTypes On Cars.snCarType=CarTypes.snCarType 
                         Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
                         Inner Join R2Primary.dbo.TblSoftwareUsers as TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                       Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 
                             and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
	                   Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc) as DataBox
                    Order By DataBox.nEnterExitId Desc", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                    NSS.nEnterExitId = Ds.Tables(0).Rows(Loopx).Item("nEnterExitId")
                    NSS.EnterDate = Ds.Tables(0).Rows(Loopx).Item("StrEnterDate")
                    NSS.EnterTime = Ds.Tables(0).Rows(Loopx).Item("StrEnterTime")
                    Dim InstanceTruckDriver = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                    NSS.NSSTruckDriver = InstanceTruckDriver.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nDriverCode")), False)
                    NSS.bFlagDriver = Ds.Tables(0).Rows(Loopx).Item("bFlagDriver")
                    NSS.nUserIdEnter = Ds.Tables(0).Rows(Loopx).Item("nUserIdEnter")
                    NSS.TurnStatus = Ds.Tables(0).Rows(Loopx).Item("TurnStatus")
                    NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").ToString + " - " + IIf(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.IsTurnReadeyforLoadAllocationRegistering(NSS), Ds.Tables(0).Rows(Loopx).Item("TurnDistanceToValidity").ToString, "اعتبار ندارد")
                    NSS.StrCardNo = Ds.Tables(0).Rows(Loopx).Item("StrCardNo")
                    NSS.LicensePlatePString = Ds.Tables(0).Rows(Loopx).Item("LPString").trim
                    NSS.TruckDriver = Ds.Tables(0).Rows(Loopx).Item("strPersonFullName").trim
                    NSS.TurnStatusTitle = Ds.Tables(0).Rows(Loopx).Item("TurnStatusTitle").trim
                    NSS.UserName = Ds.Tables(0).Rows(Loopx).Item("Username").trim
                    NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(Loopx).Item("RegisteringTimeStamp")
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurns(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select Top 5 Turns.nEnterExitId,Turns.StrEnterDate,Turns.StrEnterTime,Turns.nDriverCode,Turns.bFlagDriver,Turns.nUserIdEnter,Turns.OtaghdarTurnNumber,Turns.StrCardNo,
                                      Turns.TurnStatus,Turns.strElamDate , Cars.strCarNo +'-'+ Cars.strCarSerialNo as LPString,Turns.strDriverName,TurnStatus.TurnStatusTitle,TurnStatus.Description,TurnCreatorUsers.UserName as Username,Turns.RegisteringTimeStamp
                    From dbtransport.dbo.tbEnterExit as Turns
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On Turns.TurnStatus=TurnStatus.TurnStatusId
                       Inner Join R2Primary.DBO.TblSoftwareUsers AS TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                       Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                    Where Cars.ViewFlag = 1 And Cars.nIDCar = " & YourNSSTruck.NSSCar.nIdCar & " Order By Turns.nEnterExitId Desc", 120, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TurnNotFoundException
                End If
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                    NSS.nEnterExitId = Ds.Tables(0).Rows(Loopx).Item("nEnterExitId")
                    NSS.EnterDate = Ds.Tables(0).Rows(Loopx).Item("StrEnterDate")
                    NSS.EnterTime = Ds.Tables(0).Rows(Loopx).Item("StrEnterTime")
                    Dim InstanceTruckDriver = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                    NSS.NSSTruckDriver = InstanceTruckDriver.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nDriverCode")), False)
                    NSS.bFlagDriver = Ds.Tables(0).Rows(Loopx).Item("bFlagDriver")
                    NSS.nUserIdEnter = Ds.Tables(0).Rows(Loopx).Item("nUserIdEnter")
                    NSS.TurnStatus = Ds.Tables(0).Rows(Loopx).Item("TurnStatus")
                    NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").ToString
                    NSS.StrCardNo = Ds.Tables(0).Rows(Loopx).Item("StrCardNo")
                    NSS.LicensePlatePString = Ds.Tables(0).Rows(Loopx).Item("LPString").trim
                    NSS.TruckDriver = Ds.Tables(0).Rows(Loopx).Item("strDriverName").trim
                    NSS.TurnStatusTitle = Ds.Tables(0).Rows(Loopx).Item("TurnStatusTitle").trim
                    NSS.UserName = Ds.Tables(0).Rows(Loopx).Item("Username").trim
                    NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(Loopx).Item("RegisteringTimeStamp")
                    If Not DBNull.Value.Equals(Ds.Tables(0).Rows(Loopx).Item("strelamdate")) Then
                        NSS.LastChangedDate = Ds.Tables(0).Rows(Loopx).Item("strelamdate").trim
                    End If
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTurn(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select Top 1 TurnCreatorUsers.UserName,TurnStatus.TurnStatusTitle,Persons.strPersonFullName,Cars.strCarNo+'-'+Cars.strCarSerialNo as LPString,Turns.nEnterExitId,Turns.strEnterDate,Turns.strEnterTime,Turns.nDriverCode,Turns.bFlagDriver,Turns.nUserIdEnter,Turns.OtaghdarTurnNumber,Turns.strCardno,Turns.TurnStatus,Turns.RegisteringTimeStamp,Turns.strElamDate
                   from dbtransport.dbo.tbEnterExit as Turns
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On Turns.TurnStatus=TurnStatus.TurnStatusId
                       Inner Join R2Primary.DBO.TblSoftwareUsers AS TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                       Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
                       Inner Join dbtransport.dbo.TbDriver as Drivers On CarAndPersons.nIDPerson=Drivers.nIDDriver 
	                   Inner Join dbtransport.dbo.TbPerson as Persons On Persons.nIDPerson=Drivers.nIDDriver 
	                   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Drivers.nIDDriver=EntityRelations.E2 
	                   Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On EntityRelations.E1=SoftwareUsers.UserId 
                     Where (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Cars.ViewFlag=1  and CarAndPersons.snRelation=2
                           and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 
                           and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                     Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure = New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                NSS.NSSTruckDriver = InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), False)
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.TurnStatus = Ds.Tables(0).Rows(0).Item("TurnStatus")
                NSS.LicensePlatePString = Ds.Tables(0).Rows(0).Item("LPString").trim
                NSS.TruckDriver = Ds.Tables(0).Rows(0).Item("strPersonFullName").trim
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim
                NSS.UserName = Ds.Tables(0).Rows(0).Item("Username").trim
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                If Not DBNull.Value.Equals(Ds.Tables(0).Rows(0).Item("strelamdate")) Then
                    NSS.LastChangedDate = Ds.Tables(0).Rows(0).Item("strelamdate").trim
                End If
                Return NSS
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSLastTurn(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select Top 1 TurnCreatorUsers.UserName,TurnStatus.TurnStatusTitle,Persons.strPersonFullName,Cars.strCarNo+'-'+Cars.strCarSerialNo as LPString,Turns.nEnterExitId,Turns.strEnterDate,Turns.strEnterTime,Turns.nDriverCode,Turns.bFlagDriver,Turns.nUserIdEnter,Turns.OtaghdarTurnNumber,Turns.strCardno,Turns.TurnStatus,Turns.RegisteringTimeStamp,Turns.strElamDate
                   from dbtransport.dbo.tbEnterExit as Turns
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On Turns.TurnStatus=TurnStatus.TurnStatusId
                       Inner Join R2Primary.DBO.TblSoftwareUsers AS TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                       Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
                       Inner Join dbtransport.dbo.TbDriver as Drivers On CarAndPersons.nIDPerson=Drivers.nIDDriver 
	                   Inner Join dbtransport.dbo.TbPerson as Persons On Persons.nIDPerson=Drivers.nIDDriver 
	                   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Drivers.nIDDriver=EntityRelations.E2 
	                   Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On EntityRelations.E1=SoftwareUsers.UserId 
                     Where Cars.ViewFlag=1  and CarAndPersons.snRelation=2
                           and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 
                           and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                     Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc", 5, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure = New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                NSS.NSSTruckDriver = InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), False)
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.TurnStatus = Ds.Tables(0).Rows(0).Item("TurnStatus")
                NSS.LicensePlatePString = Ds.Tables(0).Rows(0).Item("LPString").trim
                NSS.TruckDriver = Ds.Tables(0).Rows(0).Item("strPersonFullName").trim
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim
                NSS.UserName = Ds.Tables(0).Rows(0).Item("Username").trim
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                If Not DBNull.Value.Equals(Ds.Tables(0).Rows(0).Item("strelamdate")) Then
                    NSS.LastChangedDate = Ds.Tables(0).Rows(0).Item("strelamdate").trim
                End If
                Return NSS
            Catch ex As TruckDriverNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsTurnReadeyforLoadAllocationRegistering(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
            Try
                Return IsTurnReadeyforLoadAllocationRegistering_(YourNSSTurn)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Function IsTurnReadeyforLoadAllocationRegistering_(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
            Try

                If YourNSSTurn.TurnStatus = TurnStatuses.Registered Or YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationUser Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Sub LoadAllocationRegistering_(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                'کنترل وضعیت نوبت
                If Not (YourNSSTurn.TurnStatus = TurnStatuses.Registered Or YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationUser) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " Where nEnterExitId=" & YourNSSTurn.nEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationRegistering(YourTurnId As Int64)
            Try
                Dim NSSTurn = GetNSSTurn(YourTurnId)
                LoadAllocationRegistering_(NSSTurn)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationRegistering(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Try
                LoadAllocationRegistering_(YourNSSTurn)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function IsTurnReadyforLoadAllocationCancelling(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Try
                If YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadPermissionRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.TurnExpired Or
                   YourNSSTurn.TurnStatus = TurnStatuses.CancelledLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.CancelledSystem Or
                   YourNSSTurn.TurnStatus = TurnStatuses.CancelledUnderScore Or YourNSSTurn.TurnStatus = TurnStatuses.CancelledUser Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsTurnReadyforLoadAllocationChangePriority(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Try
                If YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadPermissionRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.TurnExpired Or
                   YourNSSTurn.TurnStatus = TurnStatuses.CancelledLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.CancelledSystem Or
                   YourNSSTurn.TurnStatus = TurnStatuses.CancelledUnderScore Or YourNSSTurn.TurnStatus = TurnStatuses.CancelledUser Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadAllocationCancelling(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                'کنترل وضعیت نوبت
                If Not (YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or
                        YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or
                        YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationUser) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " Where nEnterExitId=" & YourNSSTurn.nEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As TurnNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTruck(YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 StrCardNo from dbtransport.dbo.TbEnterExit Where nEnterExitId=" & YourTurnId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New RelationBetweenTurnAndTruckNotFoundException
                End If
                Return InstanceTrucks.GetNSSTruck(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("StrCardNo")))
            Catch exx As RelationBetweenTurnAndTruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsTurnReadeyforLoadPermissionRegistering(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
            Try
                If YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationUser Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadPermissionRegistering(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourTransaction As SqlCommand)
            Try
                'کنترل وضعیت نوبت
                If Not (YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                YourTransaction.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & YourNSSTurn.nEnterExitId & ""
                YourTransaction.ExecuteNonQuery()
            Catch ex As GetNSSException
                Throw ex
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadPermissionCancelling(YourTurnId As Int64, YourResuscitationFlag As Boolean)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YourTurnId)
                'کنترل وضعیت نوبت
                If Not (NSSTurn.TurnStatus = TurnStatuses.UsedLoadPermissionRegistered) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                CmdSql.Connection.Open()
                If YourResuscitationFlag = True Then
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & ",bFlag=0,bFlagDriver=0 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                Else
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledLoadPermissionCancelled & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                End If
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTurn(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Top 1 * from dbtransport.dbo.TbEnterExit Where StrCardNo=" & YourNSSTruck.NSSCar.nIdCar & " and (bFlagDriver=0) Order By nEnterExitId desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TurnNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTurnStructure(Ds.Tables(0).Rows(0).Item("nEnterExitId"), Ds.Tables(0).Rows(0).Item("StrEnterDate").trim, Ds.Tables(0).Rows(0).Item("StrEnterTime").trim, InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), True), Ds.Tables(0).Rows(0).Item("bFlagDriver"), Ds.Tables(0).Rows(0).Item("nUserIdEnter"), Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, Ds.Tables(0).Rows(0).Item("StrCardNo"), Ds.Tables(0).Rows(0).Item("TurnStatus"), Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp"))
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTurn(YourNSSTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Top 1 Turns.* from dbtransport.dbo.tbEnterExit as Turns
                        Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On EntityRelations.E2=TurnRegisterRequests.TRRId 
                      Where EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=1 and TurnRegisterRequests.TRRId=" & YourNSSTurnRegisteringRequest.TRRId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TurnNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTurnStructure(Ds.Tables(0).Rows(0).Item("nEnterExitId"), Ds.Tables(0).Rows(0).Item("StrEnterDate").trim, Ds.Tables(0).Rows(0).Item("StrEnterTime").trim, InstanceTruckDrivers.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), False), Ds.Tables(0).Rows(0).Item("bFlagDriver"), Ds.Tables(0).Rows(0).Item("nUserIdEnter"), Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, Ds.Tables(0).Rows(0).Item("StrCardNo"), Ds.Tables(0).Rows(0).Item("TurnStatus"), Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp"))
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnRegisteringTimeStampWithTurnType(YourTurnType As TurnType) As R2StandardDateAndTimeStructure
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim TurnRegisteringTimeStamp As DateTime = IIf(YourTurnType = TurnType.Permanent, InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 4), _DateTime.GetCurrentDateTimeMilladi)
                Return New R2StandardDateAndTimeStructure(TurnRegisteringTimeStamp, Nothing, Nothing)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSLastTurnCredit(YourSeqTId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnCreditStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                    "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCredits 
                     Where SeqTId=" & YourSeqTId & " and Active=1 and Deleted=0 
                     Order By DateTimeMilladi Desc", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LastTurnCreditNotFoundException
                Else
                    Return New R2CoreTransportationAndLoadNotificationStandardTurnCreditStructure(DS.Tables(0).Rows(0).Item("SeqTId"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("SignificantTurnId"), DS.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As LastTurnCreditNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSSoftwareUser(YourTurn As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure) As R2CoreStandardSoftwareUserStructure
            Try
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1
	                     Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                         Inner Join dbtransport.dbo.TbPerson as Persons On Drivers.nIDDriver=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                         Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                         Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                      Where Turns.nEnterExitId=" & YourTurn.nEnterExitId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 
                             and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
	                   Order By SoftwareUsers.UserId DESC", 300, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New SoftWareUserNotFoundException
                End If
                Dim InstanceSoftwareUsers = New R2Core.SoftwareUserManagement.R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Return InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
            Catch ex As SoftWareUserNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTurn(YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select Users.UserName,TurnStatus.TurnStatusTitle,Person.strPersonFullName,Car.strCarNo+'-'+car.strCarSerialNo as LPString, EnterExit.nEnterExitId,EnterExit.strEnterDate,EnterExit.strEnterTime,EnterExit.nDriverCode,EnterExit.bFlagDriver,EnterExit.nUserIdEnter,EnterExit.OtaghdarTurnNumber,EnterExit.strCardno,EnterExit.TurnStatus,EnterExit.RegisteringTimeStamp
                            from dbtransport.dbo.tbEnterExit as EnterExit 
                               Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                               Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On EnterExit.TurnStatus=TurnStatus.TurnStatusId
                               Inner Join R2Primary.DBO.TblSoftwareUsers AS Users On EnterExit.nUserIdEnter=Users.UserId Where EnterExit.nEnterExitId=" & YourTurnId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure = New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                Dim InstanceTruckDriver = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                NSS.NSSTruckDriver = InstanceTruckDriver.GetNSSTruckDriver(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDriverCode")), False)
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.TurnStatus = Ds.Tables(0).Rows(0).Item("TurnStatus")
                NSS.LicensePlatePString = Ds.Tables(0).Rows(0).Item("LPString").trim
                NSS.TruckDriver = Ds.Tables(0).Rows(0).Item("strPersonFullName").trim
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim
                NSS.UserName = Ds.Tables(0).Rows(0).Item("Username").trim
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                Return NSS
            Catch exx As TurnNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTurnsManagement
        Private Shared _DateTime As New R2DateTime

        Public Shared Function ExistActiveTurn(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                     "Select * from dbtransport.dbo.tbEnterExit as Turns
                      Where (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Turns.bFlagDriver=0 and
                             Turns.strCardno=" & YourNSSTruck.NSSCar.nIdCar & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTurn(YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                         "Select Users.UserName,TurnStatus.TurnStatusTitle,Person.strPersonFullName,Car.strCarNo+'-'+car.strCarSerialNo as LPString, EnterExit.nEnterExitId,EnterExit.strEnterDate,EnterExit.strEnterTime,EnterExit.nDriverCode,EnterExit.bFlagDriver,EnterExit.nUserIdEnter,EnterExit.OtaghdarTurnNumber,EnterExit.strCardno,EnterExit.TurnStatus,EnterExit.RegisteringTimeStamp
                            from dbtransport.dbo.tbEnterExit as EnterExit 
                               Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                               Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On EnterExit.TurnStatus=TurnStatus.TurnStatusId
                               Inner Join R2Primary.DBO.TblSoftwareUsers AS Users On EnterExit.nUserIdEnter=Users.UserId Where EnterExit.nEnterExitId=" & YourTurnId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure = New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                NSS.NSSTruckDriver = R2CoreTransportationAndLoadNotificationMClassTruckDriversManagement.GetNSSTruckDriver(Ds.Tables(0).Rows(0).Item("nDriverCode"))
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.TurnStatus = Ds.Tables(0).Rows(0).Item("TurnStatus")
                NSS.LicensePlatePString = Ds.Tables(0).Rows(0).Item("LPString").trim
                NSS.TruckDriver = Ds.Tables(0).Rows(0).Item("strPersonFullName").trim
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim
                NSS.UserName = Ds.Tables(0).Rows(0).Item("Username").trim
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                Return NSS
            Catch exx As TurnNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTurn(YourSeqTId As Int64, YourTurnId As String, YourTargetYearFull As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim CurrentSalShamsiFull As String = _DateTime.GetCurrentSalShamsiFull
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                          "Select Users.UserName,TurnStatus.TurnStatusTitle,Person.strPersonFullName,Car.strCarNo+'-'+car.strCarSerialNo as LPString, EnterExit.nEnterExitId,EnterExit.strEnterDate,EnterExit.strEnterTime,EnterExit.nDriverCode,EnterExit.bFlagDriver,EnterExit.nUserIdEnter,EnterExit.OtaghdarTurnNumber,EnterExit.strCardno,EnterExit.TurnStatus,EnterExit.RegisteringTimeStamp
                                from dbtransport.dbo.tbEnterExit as EnterExit 
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On SUBSTRING(EnterExit.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS= SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS
                                  Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                  Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On EnterExit.TurnStatus=TurnStatus.TurnStatusId
                                  Inner Join R2Primary.DBO.TblSoftwareUsers AS Users On EnterExit.nUserIdEnter=Users.UserId 
                                Where SeqT.SeqTId=" & YourSeqTId & " and SUBSTRING(EnterExit.OtaghdarTurnNumber,2,4)='" & YourTargetYearFull & "' and ltrim(rtrim(SUBSTRING(EnterExit.OtaghdarTurnNumber,7,20)))='" & YourTurnId & "'", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure = New R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                NSS.NSSTruckDriver = R2CoreTransportationAndLoadNotificationMClassTruckDriversManagement.GetNSSTruckDriver(Ds.Tables(0).Rows(0).Item("nDriverCode"))
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.TurnStatus = Ds.Tables(0).Rows(0).Item("TurnStatus")
                NSS.LicensePlatePString = Ds.Tables(0).Rows(0).Item("LPString").trim
                NSS.TruckDriver = Ds.Tables(0).Rows(0).Item("strPersonFullName").trim
                NSS.TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim
                NSS.UserName = Ds.Tables(0).Rows(0).Item("Username").trim
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                Return NSS
            Catch exx As TurnNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub LoadPermissionRegistering(YourTurnId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                Dim NSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YourTurnId)
                'کنترل وضعیت نوبت
                If Not (NSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select Top 1 nEnterExitId From dbtransport.dbo.TbEnterExit with (tablockx) Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As GetNSSException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
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

        Public Shared Sub LoadPermissionCancelling(YourTurnId As Int64, YourResuscitationFlag As Boolean)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                'کنترل وضعیت نوبت
                If Not (NSSTurn.TurnStatus = TurnStatuses.UsedLoadPermissionRegistered) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                'تغییر وضعیت نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select Top 1 nEnterExitId From dbtransport.dbo.TbEnterExit with (tablockx) Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                If YourResuscitationFlag = True Then
                    If NSSTurn.RegisteringTimeStamp <> "2015-01-01 00:00:00.000" Then Throw New UnableResucitationTemporayTurnException
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & ",bFlag=0,bFlagDriver=0 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                Else
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledLoadPermissionCancelled & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                End If
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As UnableResucitationTemporayTurnException
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

        'Public Shared Sub TurnExpirationCancelling(YourTurnId As Int64)
        '    Dim CmdSql As New SqlCommand
        '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
        '    Try
        '        Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
        '        'کنترل وضعیت نوبت
        '        If Not (NSSTurn.TurnStatus = TurnStatuses.Registered Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationUser) Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException

        '        'تغییر وضعیت نوبت
        '        CmdSql.Connection.Open()
        '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
        '        CmdSql.CommandText = "Select Top 1 nEnterExitId From dbtransport.dbo.TbEnterExit with (tablockx) Where nEnterExitId=" & YourTurnId & ""
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.TurnExpired & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
        '        CmdSql.ExecuteNonQuery()
        '        'ارسال تاییدیه صدور مجوز به آنلاین
        '        TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo, R2CoreTransportationAndLoadNotificationMclassLoadTargetsManagement.GetNSSLoadTarget(NSSLoadCapacitorLoad.nCityCode).TargetTravelLength)

        '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

        '    Catch ex As Exception
        '        If CmdSql.Connection.State <> ConnectionState.Closed Then
        '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '        End If
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        Public Shared Function GetNSSTruck(YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 StrCardNo from dbtransport.dbo.TbEnterExit Where nEnterExitId=" & YourTurnId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New RelationBetweenTurnAndTruckNotFoundException
                End If
                Return R2CoreTransportationAndLoadNotificationMClassTrucksManagement.GetNSSTruck(Ds.Tables(0).Rows(0).Item("StrCardNo"))
            Catch exx As RelationBetweenTurnAndTruckNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsTurnReadeyforLoadPermissionRegistering(YourTurnId As Int64) As Boolean
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                If NSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationUser Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Shared Function IsTurnReadeyforLoadAllocationRegistering_(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
            Try

                If YourNSSTurn.TurnStatus = TurnStatuses.Registered Or YourNSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or YourNSSTurn.TurnStatus = TurnStatuses.ResuscitationUser Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsTurnReadeyforLoadAllocationRegistering(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
            Try
                Return IsTurnReadeyforLoadAllocationRegistering_(YourNSSTurn)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsTerraficCardTypeforTurnRegisteringActive(YourNSS As R2CoreParkingSystemStandardTrafficCardStructure) As Boolean
            Try
                Dim Ds As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 TurnRegisteringActive from R2PrimaryTransportationAndLoadNotification.dbo.TblTerraficCardTypesforTurnRegistering Where TerraficCardTypeId=" & YourNSS.CardType & "", 3600, Ds, New Boolean)
                Return Ds.Tables(0).Rows(0).Item("TurnRegisteringActive")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsTerraficCardTypeforTurnRegistering(YourNSS As R2CoreParkingSystemStandardTrafficCardStructure) As Boolean
            Try
                Dim Ds As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 TurnRegistering from R2PrimaryTransportationAndLoadNotification.dbo.TblTerraficCardTypesforTurnRegistering Where TerraficCardTypeId=" & YourNSS.CardType & "", 3600, Ds, New Boolean)
                Return Ds.Tables(0).Rows(0).Item("TurnRegistering")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public MustInherit Class TurnStatuses
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Registered As Int64 = 1
        Public Shared ReadOnly Property CancelledUser As Int64 = 2
        Public Shared ReadOnly Property CancelledUnderScore As Int64 = 3
        Public Shared ReadOnly Property CancelledSystem As Int64 = 4
        Public Shared ReadOnly Property CancelledLoadPermissionCancelled As Int64 = 5
        Public Shared ReadOnly Property UsedLoadPermissionRegistered As Int64 = 6
        Public Shared ReadOnly Property UsedLoadAllocationRegistered As Int64 = 7
        Public Shared ReadOnly Property ResuscitationLoadPermissionCancelled As Int64 = 8
        Public Shared ReadOnly Property ResuscitationLoadAllocationCancelled As Int64 = 9
        Public Shared ReadOnly Property ResuscitationUser As Int64 = 10
        Public Shared ReadOnly Property TurnExpired As Int64 = 11

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurn
        Public Property TurnId As Int64
        Public Property TruckId As Int64
        Public Property TurnIssueDate As String
        Public Property TurnIssueTime As String
        Public Property TurnDescription As String
        Public Property TurnIssueSoftwareUserId As Int64
        Public Property TurnLastChangedSoftwareUserId As Int64
        Public Property TruckDriverName As String
        Public Property bFlag As Boolean
        Public Property bFlagDriver As Boolean
        Public Property TurnLastChangedDate As String
        Public Property TurnLastChangedTime As String
        Public Property TruckDriverId As Int64
        Public Property BillOfLadingNumber As String
        Public Property SequentialTurnNumber As String
        Public Property TurnStatusId As Int64
        Public Property RegisteringTimeStamp As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnExtended
        Public Property TurnId As Int64
        Public Property TurnIssueDate As String
        Public Property TurnIssueTime As String
        Public Property TruckDriver As String
        Public Property SoftwareUserName As String
        Public Property BillOfLadingNumber As String
        Public Property OtaghdarTurnNumber As String
        Public Property TurnStatusTitle As String
        Public Property TurnStatusDescription As String
        Public Property DateOfLastChanged As String
        Public Property SequentialTurnTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnCost
        Public SequentialTurnId As Int64
        Public SelfGoverCost As Int64
        Public TruckerAssociationCost As Int64
        Public ReadOnly Property TotalCost As Int64
            Get
                Return SelfGoverCost + TruckerAssociationCost
            End Get
        End Property
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnsManager

        Private _R2DateTimeService As IR2DateTimeService

        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Function GetLastActiveTurnfromTruckId(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnExtended
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select Top 1 Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,SoftwareUsers.UserName as SoftwareUserName,
                                     Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,TurnStatuses.TurnStatusTitle as TurnStatusTitle,TurnStatuses.Description as TurnStatusDescription,
                                     Turns.strElamDate as DateOfLastChanged,SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from DBTransport.dbo.tbEnterExit as Turns
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Turns.nUserIdEnter=SoftwareUsers.UserId   
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses on Turns.TurnStatus=TurnStatuses.TurnStatusId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns on substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                        Where Turns.StrCardNo=" & YourTruckId & "  and (Turns.TurnStatus=" & Turns.TurnStatuses.Registered & " or Turns.TurnStatus=" & Turns.TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationUser & ")
                        Order By nEnterExitId Desc")


                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 1 Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,SoftwareUsers.UserName as SoftwareUserName,
                                     Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,TurnStatuses.TurnStatusTitle as TurnStatusTitle,TurnStatuses.Description as TurnStatusDescription,
                                     Turns.strElamDate as DateOfLastChanged,SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from DBTransport.dbo.tbEnterExit as Turns
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Turns.nUserIdEnter=SoftwareUsers.UserId   
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses on Turns.TurnStatus=TurnStatuses.TurnStatusId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns on substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                        Where Turns.StrCardNo=" & YourTruckId & "  and (Turns.TurnStatus=" & Turns.TurnStatuses.Registered & " or Turns.TurnStatus=" & Turns.TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & Turns.TurnStatuses.ResuscitationUser & ")
                        Order By nEnterExitId Desc", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTurnExtended With {
                .TurnId = Ds.Tables(0).Rows(0).Item("TurnId"), .TurnIssueDate = Ds.Tables(0).Rows(0).Item("TurnIssueDate").trim, .TurnIssueTime = Ds.Tables(0).Rows(0).Item("TurnIssueTime").trim,
                .TruckDriver = Ds.Tables(0).Rows(0).Item("TruckDriver").trim, .SoftwareUserName = Ds.Tables(0).Rows(0).Item("SoftwareUserName"), .BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim,
                .OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, .TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim, .TurnStatusDescription = Ds.Tables(0).Rows(0).Item("TurnStatusDescription").trim,
                .DateOfLastChanged = Ds.Tables(0).Rows(0).Item("DateOfLastChanged").trim, .SequentialTurnTitle = Ds.Tables(0).Rows(0).Item("SequentialTurnTitle").trim}
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLastTurnfromTruckId(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnExtended
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select Top 1 Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,SoftwareUsers.UserName as SoftwareUserName,
                                     Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,TurnStatuses.TurnStatusTitle as TurnStatusTitle,TurnStatuses.Description as TurnStatusDescription,
                                     Turns.strElamDate as DateOfLastChanged,SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from DBTransport.dbo.tbEnterExit as Turns
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Turns.nUserIdEnter=SoftwareUsers.UserId   
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses on Turns.TurnStatus=TurnStatuses.TurnStatusId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns on substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                        Where Turns.StrCardNo=" & YourTruckId & "  Order By nEnterExitId Desc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 1 Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,SoftwareUsers.UserName as SoftwareUserName,
                                     Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,TurnStatuses.TurnStatusTitle as TurnStatusTitle,TurnStatuses.Description as TurnStatusDescription,
                                     Turns.strElamDate as DateOfLastChanged,SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from DBTransport.dbo.tbEnterExit as Turns
                          Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Turns.nUserIdEnter=SoftwareUsers.UserId   
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses on Turns.TurnStatus=TurnStatuses.TurnStatusId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns on substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                        Where Turns.StrCardNo=" & YourTruckId & "  Order By nEnterExitId Desc", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                End If

                Return New R2CoreTransportationAndLoadNotificationTurnExtended With {
                .TurnId = Ds.Tables(0).Rows(0).Item("TurnId"), .TurnIssueDate = Ds.Tables(0).Rows(0).Item("TurnIssueDate").trim, .TurnIssueTime = Ds.Tables(0).Rows(0).Item("TurnIssueTime").trim,
                .TruckDriver = Ds.Tables(0).Rows(0).Item("TruckDriver").trim, .SoftwareUserName = Ds.Tables(0).Rows(0).Item("SoftwareUserName"), .BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim,
                .OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, .TurnStatusTitle = Ds.Tables(0).Rows(0).Item("TurnStatusTitle").trim, .TurnStatusDescription = Ds.Tables(0).Rows(0).Item("TurnStatusDescription").trim,
                .DateOfLastChanged = Ds.Tables(0).Rows(0).Item("DateOfLastChanged").trim, .SequentialTurnTitle = Ds.Tables(0).Rows(0).Item("SequentialTurnTitle").trim}
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTop10TruckTurns(YourTruckId As Int64, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select Top 10  Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,
                                   TurnCreatorUsers.UserName as SoftwareUserName,Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,
			                       TurnStatus.TurnStatusTitle as TurnStatusTitle,TurnStatus.Description as TurnStatusDescription,Turns.strElamDate as DateOfLastChanged,
			                       SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from dbtransport.dbo.tbEnterExit as Turns
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On Substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On Turns.TurnStatus=TurnStatus.TurnStatusId
                          Inner Join R2Primary.DBO.TblSoftwareUsers AS TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                        Where Turns.StrCardNo=" & YourTruckId & " Order By Turns.nEnterExitId Desc
                        for JSON path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 10  Turns.nEnterExitId as TurnId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDriverName as TruckDriver,
                                   TurnCreatorUsers.UserName as SoftwareUserName,Turns.BillOfLadingNumber as BillOfLadingNumber,Turns.OtaghdarTurnNumber as OtaghdarTurnNumber,
			                       TurnStatus.TurnStatusTitle as TurnStatusTitle,TurnStatus.Description as TurnStatusDescription,Turns.strElamDate as DateOfLastChanged,
			                       SequentialTurns.SeqTTitle as SequentialTurnTitle
                        from dbtransport.dbo.tbEnterExit as Turns
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On Substring(Turns.OtaghdarTurnNumber,1,1)=SequentialTurns.SeqTKeyWord  
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatus On Turns.TurnStatus=TurnStatus.TurnStatusId
                          Inner Join R2Primary.DBO.TblSoftwareUsers AS TurnCreatorUsers On Turns.nUserIdEnter=TurnCreatorUsers.UserId 
                        Where Turns.StrCardNo=" & YourTruckId & " Order By Turns.nEnterExitId Desc
                        for JSON path", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTop5TruckTurns(YourSoftwareUserId As Int64) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select  (Select Count(*) from dbtransport.dbo.tbEnterExit as TurnsX
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On SUBSTRING(TurnsX.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS
                             Where SeqT.Active=1 and SeqT.Deleted=0 and SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS=SUBSTRING(DataBox.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS and TurnsX.nEnterExitId<DataBox.nEnterExitId and (TurnsX.TurnStatus=1 or TurnsX.TurnStatus=7 or TurnsX.TurnStatus=8 or TurnsX.TurnStatus=9 or TurnsX.TurnStatus=10)) as TurnDistanceToValidity,
							 DataBox.OtaghdarTurnNumber as SequentialTurn,DataBox.strEnterDate as TurnIssueDate,DataBox.strEnterTime as TurnIssueTime,DataBox.TurnStatusTitle,DataBox.LPString,DataBox.strPersonFullName as TruckDriverName 
                    from
                      (Select Top 5 Turns.nEnterExitId,Turns.StrEnterDate,Turns.StrEnterTime,Turns.nDriverCode,Turns.bFlagDriver,Turns.nUserIdEnter,Turns.OtaghdarTurnNumber,Turns.StrCardNo,
                                    Turns.TurnStatus,Cars.strCarNo +'-'+ Cars.strCarSerialNo as LPString,Persons.strPersonFullName,TurnStatuses.TurnStatusTitle,SoftwareUsers.UserName as Username,Turns.RegisteringTimeStamp
                       from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
	                     Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1
                         Inner Join dbtransport.dbo.TbPerson as Persons On EntityRelations.E2=Persons.nIDPerson 
                         Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Persons.nIDPerson=CarAndPersons.nIDPerson
                         Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                         Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
                       Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 
                             and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
	                   Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc) as DataBox
                    Order By DataBox.nEnterExitId Desc for json path", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurn(YourTurnId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurn
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                         "Select Turns.nEnterExitId as TurnId,Turns.strCardno as TruckId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDesc as TurnDescription,
                                 Turns.nUserIdEnter as TurnIssueSoftwareUserId,Turns.nUserIdExit as TurnLastChangedSoftwareUserId,Turns.strDriverName as TruckDriverName,Turns.bFlag,Turns.bFlagDriver,
                    	         Turns.strElamDate as TurnLastChangedDate,Turns.strElamTime as TurnLastChangedTime,Turns.nDriverCode as TruckDriverId,Turns.BillOfLadingNumber as BillOfLadingNumber,
	                             Turns.OtaghdarTurnNumber as SequentialTurnNumber,Turns.TurnStatus as TurnStatusId,Turns.RegisteringTimeStamp as RegisteringTimeStamp
	                      from DBTransport.dbo.tbEnterExit as Turns
                          Where Turns.nEnterExitId=" & YourTurnId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select Turns.nEnterExitId as TurnId,Turns.strCardno as TruckId,Turns.strEnterDate as TurnIssueDate,Turns.strEnterTime as TurnIssueTime,Turns.strDesc as TurnDescription,
                                 Turns.nUserIdEnter as TurnIssueSoftwareUserId,Turns.nUserIdExit as TurnLastChangedSoftwareUserId,Turns.strDriverName as TruckDriverName,Turns.bFlag,Turns.bFlagDriver,
                    	         Turns.strElamDate as TurnLastChangedDate,Turns.strElamTime as TurnLastChangedTime,Turns.nDriverCode as TruckDriverId,Turns.BillOfLadingNumber as BillOfLadingNumber,
	                             Turns.OtaghdarTurnNumber as SequentialTurnNumber,Turns.TurnStatus as TurnStatusId,Turns.RegisteringTimeStamp as RegisteringTimeStamp
	                      from DBTransport.dbo.tbEnterExit as Turns
                          Where Turns.nEnterExitId=" & YourTurnId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTurn With {.TurnId = Ds.Tables(0).Rows(0).Item("TurnId"), .TruckId = Ds.Tables(0).Rows(0).Item("TruckId"), .TurnIssueDate = Ds.Tables(0).Rows(0).Item("TurnIssueDate").trim,
                      .TurnIssueTime = Ds.Tables(0).Rows(0).Item("TurnIssueTime").trim, .TurnDescription = Ds.Tables(0).Rows(0).Item("TurnDescription").trim, .TurnIssueSoftwareUserId = Ds.Tables(0).Rows(0).Item("TurnIssueSoftwareUserId"),
                      .TurnLastChangedSoftwareUserId = Ds.Tables(0).Rows(0).Item("TurnLastChangedSoftwareUserId"), .TruckDriverName = Ds.Tables(0).Rows(0).Item("TruckDriverName").trim,
                      .bFlag = Ds.Tables(0).Rows(0).Item("bFlag"), .bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver"), .TurnLastChangedDate = Ds.Tables(0).Rows(0).Item("TurnLastChangedDate").trim,
                      .TurnLastChangedTime = Ds.Tables(0).Rows(0).Item("TurnLastChangedTime").trim, .TruckDriverId = Ds.Tables(0).Rows(0).Item("TruckDriverId"), .BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim,
                      .SequentialTurnNumber = Ds.Tables(0).Rows(0).Item("SequentialTurnNumber").trim, .TurnStatusId = Ds.Tables(0).Rows(0).Item("TurnStatusId"), .RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")}
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TurnCancellationByUser(YourTurnId As Int64, YourSoftwareUserId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurnAccounting = New R2CoreTransportationAndLoadNotificationTurnAccountingManager(New R2DateTimeService)
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager

                Dim Turn = GetTurn(YourTurnId, True)
                If Not (Turn.TurnStatusId = TurnStatuses.Registered Or Turn.TurnStatusId = TurnStatuses.UsedLoadAllocationRegistered Or Turn.TurnStatusId = TurnStatuses.ResuscitationLoadPermissionCancelled Or Turn.TurnStatusId = TurnStatuses.ResuscitationLoadAllocationCancelled Or Turn.TurnStatusId = TurnStatuses.ResuscitationUser) Then Throw New TurnCancellationNotPossibleBecuaseTurnStatusException

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update DBTransport.dbo.tbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUser & ",nUserIdExit=" & YourSoftwareUserId & ",bFlag=1,bFlagDriver=1,strElamDate='" & _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "',strElamTime='" & _R2DateTimeService.DateTimeServ.GetCurrentTime & "'
                                      Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                'اکانتینگ نوبت
                InstanceTurnAccounting.TurnAccountingRegistering(YourTurnId, TurnAccounting.TurnAccouningTypes.TurnCancellation, YourSoftwareUserId, CmdSql)
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال پیام ابطال نوبت به سرویس نوبت آنلاین
                'بررسی فعال بودن نوبت آنلاین
                If InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TWS, 0) Then
                    Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(New R2DateTimeService)
                    Dim Truck = InstanceTrucks.GetTruck(Turn.TruckId, False)
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(Truck.Pelak, Truck.Serial)
                End If
            Catch ex As TurnNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TurnCancellationNotPossibleBecuaseTurnStatusException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TruckNotFoundException
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

        Public Function TurnResuscitationByUser(YourTurnId As Int64, YourSoftwareUserId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurnAccounting = New R2CoreTransportationAndLoadNotificationTurnAccountingManager(New R2DateTimeService)
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim Turn = GetTurn(YourTurnId, True)
                If Not (Turn.TurnStatusId = TurnStatuses.CancelledUser Or Turn.TurnStatusId = TurnStatuses.CancelledSystem Or Turn.TurnStatusId = TurnStatuses.CancelledLoadPermissionCancelled) Then Throw New TurnResuscitationNotPossibleBecuaseTurnStatusException

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationUser & ",nUserIdExit=" & YourSoftwareUserId & ",bFlag=1,bFlagDriver=0 ,strElamDate='" & _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "',strElamTime='" & _R2DateTimeService.DateTimeServ.GetCurrentTime & "'
                                      Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                'اکانتینگ نوبت
                InstanceTurnAccounting.TurnAccountingRegistering(YourTurnId, TurnAccounting.TurnAccouningTypes.TurnResuscitation, YourSoftwareUserId, CmdSql)
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال پیام ابطال نوبت به سرویس نوبت آنلاین
                'بررسی فعال بودن نوبت آنلاین
                If InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TWS, 0) Then
                    Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(New R2DateTimeService)
                    Dim Truck = InstanceTrucks.GetTruck(Turn.TruckId, False)
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.AddNobat(Truck.Pelak, Truck.Serial)
                End If
            Catch ex As TurnResuscitationNotPossibleBecuaseTurnStatusException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TruckNotFoundException
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
        End Function

        Public Function ExistActiveTurn(YourTruckId As Int64, YourImmediately As Boolean)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select nEnterExitId from dbtransport.dbo.tbEnterExit as Turns
                         Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & ") and 
                               Turns.bFlagDriver=0 and Turns.strCardno=" & YourTruckId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select * from dbtransport.dbo.tbEnterExit as Turns
                      Where (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Turns.bFlagDriver=0 and
                             Turns.strCardno=" & YourTruckId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HasTruckDriverTurn(YourTruckDriverId As Int64, YorImmediately As Boolean) As Boolean
            Try
                Dim IntanceDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YorImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select Top 1 nEnterExitId from dbtransport.dbo.TbEnterExit 
                         Where nDriverCode=" & YourTruckDriverId & " and bFlagDriver=0 Order By nEnterExitId desc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    If IntanceDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Top 1 nEnterExitId from dbtransport.dbo.TbEnterExit 
                         Where nDriverCode=" & YourTruckDriverId & " and bFlagDriver=0 Order By nEnterExitId desc", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetPossibleTruckTravelTime(YourTruckId As Int64) As Int64
            Try
                Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationLoadPermissionManager
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_R2DateTimeService)
                Dim InstanceTravelTime = New R2CoreTransportationAndLoadNotificationTravelTimeManager
                Dim Load = InstanceLoadPermission.GetTruckLastLoadWhichPermissioned(YourTruckId, True)
                Dim LoaderTypeId = InstanceTrucks.GetTruck(YourTruckId, True).LoaderTypeId

                Dim TravelTime As Int16 = InstanceTravelTime.GetTravelTime(LoaderTypeId, Load.SourceCityId, Load.TargetCityId, True).TravelTime
                Dim DateTimeMilladiExit As DateTime = _R2DateTimeService.DateTimeServ.GetMilladiDateTimeFromDateShamsiFull(InstanceLoadPermission.GetTruckLastLoadPermission(YourTruckId, False).LoadPermissionDate, InstanceLoadPermission.GetTruckLastLoadPermission(YourTruckId, False).LoadPermissionTime)
                Dim Diff As Int64 = R2CoreMClassPublicProcedures.GetDateDiff(DateInterval.Minute, DateTimeMilladiExit, _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladiFormated()) + 1
                Return (Diff - TravelTime * 60)
            Catch ex As TruckHasNotAnyLoadPermissionException
                Return 0
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckTravelTimeFormated(YourTruckId As Int64) As String
            Try
                Dim TReminder As Int64 = Math.Abs(GetPossibleTruckTravelTime(YourTruckId))
                Dim THours As Int64 = Int(TReminder / 60)
                Dim TMinutes As Int64 = TReminder - THours * 60
                Dim TString As String = String.Empty
                If THours <> 0 Then TString = THours.ToString() + " ساعت "
                If TMinutes <> 0 Then TString = TString + vbCrLf + TMinutes.ToString() + " دقیقه "
                Return TString
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnCost(YourSequentialTurnId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnCost
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts Where SeqTId=" & YourSequentialTurnId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnCostNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts Where SeqTId=" & YourSequentialTurnId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New TurnCostNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTurnCost With {.SequentialTurnId = Ds.Tables(0).Rows(0).Item("SeqTId"), .SelfGoverCost = Ds.Tables(0).Rows(0).Item("SelfGoverCost"), .TruckerAssociationCost = Ds.Tables(0).Rows(0).Item("TruckerAssociationCost")}
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnCost(ByVal YourMoneyWallet As R2CoreMoneyWallet, YourSequentialTurnId As Int64) As Int64
            Try
                Dim InstanceTrafficCards = New R2CoreParkingSystemTrafficCardsManager(New R2DateTimeService)
                Dim TerraficCard = InstanceTrafficCards.GetTrafficCard(YourMoneyWallet.MoneyWalletId)
                Dim TerraficCost = InstanceTrafficCards.GetTerraficCost(TerraficCard.CardTypeId, False)
                Dim TurnCost = GetTurnCost(YourSequentialTurnId, False)

                'احراز معیار محاسبه
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & TerraficCard.CardId & ") and (MblghA<>0) and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.EnterType & " or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.SherkatHazinehNobat & " ) order by DateMilladiA desc")
                Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then Return TurnCost.SelfGoverCost

                'تاخیر تا آخرین اکانت نوبت
                Dim Tavaghof As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladi())
                If Tavaghof >= TerraficCost.DelayFromEntry Then
                    Return TurnCost.SelfGoverCost
                Else
                    Da.SelectCommand.CommandText = "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & TerraficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc"
                    Ds.Tables.Clear()
                    If Da.Fill(Ds) <= 0 Then Return 0
                    Dim TavaghofLastTurn As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladi())
                    If TavaghofLastTurn <= Tavaghof Then
                        Return TurnCost.SelfGoverCost
                    Else
                        Return 0
                    End If
                End If
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As TerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnRegisteringTimeStampWithTurnType(YourTurnType As TurnType) As R2StandardDateAndTimeStructure
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim TurnRegisteringTimeStamp As DateTime = IIf(YourTurnType = TurnType.Permanent, InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 4), _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladi)
                Return New R2StandardDateAndTimeStructure(TurnRegisteringTimeStamp, Nothing, Nothing)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurn(YourTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationTurnRegisterRequest, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurn
            Try
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As New DataSet

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select Top 1 Turns.* from dbtransport.dbo.tbEnterExit as Turns
                           Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On EntityRelations.E2=TurnRegisterRequests.TRRId 
                         Where EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=1 and TurnRegisterRequests.TRRId=" & YourTurnRegisteringRequest.TRRId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select Top 1 Turns.* from dbtransport.dbo.tbEnterExit as Turns
                        Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On EntityRelations.E2=TurnRegisterRequests.TRRId 
                      Where EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=1 and TurnRegisterRequests.TRRId=" & YourTurnRegisteringRequest.TRRId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New TurnNotFoundException
                    End If
                End If
                Return New R2CoreTransportationAndLoadNotificationTurn With {.TurnId = Ds.Tables(0).Rows(0).Item("nEnterExitId"), .TurnIssueDate = Ds.Tables(0).Rows(0).Item("StrEnterDate").trim, .TurnIssueTime = Ds.Tables(0).Rows(0).Item("StrEnterTime").trim, .TruckDriverId = Ds.Tables(0).Rows(0).Item("nDriverCode"), .bFlag = Ds.Tables(0).Rows(0).Item("bFlag"), .bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver"), .TurnIssueSoftwareUserId = Ds.Tables(0).Rows(0).Item("nUserIdEnter"), .BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, .SequentialTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, .TruckId = Ds.Tables(0).Rows(0).Item("StrCardNo"), .TurnStatusId = Ds.Tables(0).Rows(0).Item("TurnStatus"), .RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp"), .TurnLastChangedSoftwareUserId = Ds.Tables(0).Rows(0).Item("nUserIdExit"), .TurnDescription = Ds.Tables(0).Rows(0).Item("strDesc").trim, .TruckDriverName = Ds.Tables(0).Rows(0).Item("strDriverName").trim, .TurnLastChangedDate = Ds.Tables(0).Rows(0).Item("strElamDate").trim, .TurnLastChangedTime = Ds.Tables(0).Rows(0).Item("strElamTime").trim}
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetReserveTurn(YourTurnRegisterRequestId As Int64, YourTurnType As TurnType, YourSoftwareUserId As Int64) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_R2DateTimeService)
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationTruckDriversManager
                Dim InstanceSequential = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)

                'ناوگان پیش فرض 
                Dim Truck = InstanceTrucks.GetDefaultTruck()
                'راننده ناوگان پیش فرض
                Dim TruckDriver = InstanceTruckDrivers.GetDefaultTruckDriver()
                'تسلسل نوبت پیش فرض
                Dim SequentialTurn = InstanceSequential.GetSequentialTurn(SequentialTurns.SequentialTurns.None, False)
                'شاخص درخواست صدور نوبت
                Dim TRR = InstanceTurnRegisterRequest.GetTurnRegisterRequest(YourTurnRegisterRequestId, True)
                'تراکنش صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'شاخص اصلی نوبت برای تمام انواع ناوگان یکدست و یکپارچه محاسبه می شود و سیاست یکسانی دارد
                CmdSql.CommandText = "Select top 1 nEnterExitId from dbtransport.dbo.tbEnterExit with (tablockx) order by nEnterExitId desc"
                Dim newTurnId As Int64 = CmdSql.ExecuteScalar + 1

                Dim SequentialTurnId As Int64 = Int64.MinValue
                Dim SequentialTurnId_ As String = String.Empty
                CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from tbenterexit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() & "' order by OtaghdarTurnNumber desc"
                SequentialTurnId = CmdSql.ExecuteScalar + 1
                SequentialTurnId_ = SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() + "/" + InstancePublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim()
                Dim TurnRegisteringTimeStamp = InstanceTurns.GetTurnRegisteringTimeStampWithTurnType(YourTurnType)

                'ثبت نوبت ناوگان باری
                CmdSql.CommandText = "Insert Into dbtransport.dbo.TbEnterExit(nEnterExitId,StrCardNo,StrEnterDate,StrEnterTime,StrDesc,bEnterExit,nUserIdEnter,nUserIdExit,StrDriverName,bFlag,bFlagDriver,strElamDate,strElamTime,nDriverCode,BillOfLadingNumber,OtaghdarTurnNumber,TurnStatus,LoadPermissionStatus,RegisteringTimeStamp) 
                                      Values(" & newTurnId & "," & Truck.TruckId & ",'" & _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull() & "','" & _R2DateTimeService.DateTimeServ.GetCurrentTime() & "','" & TRR.Description & "',0," & YourSoftwareUserId & "," & YourSoftwareUserId & ",'" & TruckDriver.NameFamily & "',1,1,'" & String.Empty & "','" & String.Empty & "'," & TruckDriver.DriverId & ",'','" & SequentialTurnId_ & "'," & TurnStatuses.CancelledSystem & "," & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & ",'" & TurnRegisteringTimeStamp.DateTimeMilladiFormated & "')"
                CmdSql.ExecuteNonQuery()

                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Return newTurnId
            Catch ex As Exception When TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Namespace TurnAccounting
        Public MustInherit Class TurnAccouningTypes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property TurnIssue = 1
            Public Shared ReadOnly Property TurnResuscitation = 2
            Public Shared ReadOnly Property TurnCancellation = 3


        End Class

        Public Class R2CoreTransportationAndLoadNotificationTurnAccountingManager

            Private _DateTimeService As IR2DateTimeService = New R2DateTimeService

            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
            End Sub

            Public Sub TurnAccountingRegistering(YourTurnId As Int64, YourAccountingTypeId As Int64, YourSoftwareUserId As Int64, YourCommand As SqlClient.SqlCommand)
                Try
                    YourCommand.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnAccounting(nEnterExitId,AccountingTypeId,UserId,DateTimeMilladi,DateShamsi,Time)
                                          Values(" & YourTurnId & "," & YourAccountingTypeId & "," & YourSoftwareUserId & ",'" & _DateTimeService.DateTimeServ.GetCurrentDateTimeMilladiFormated & "','" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "','" & _DateTimeService.DateTimeServ.GetCurrentTime & "')"
                    YourCommand.ExecuteNonQuery()
                Catch ex As SqlException
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function GetTurnAccounting(YourTurnId As Int64, YourImmediately As Boolean) As String
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim Ds As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand(
                        "Select Turns.nEnterExitId as TurnId,Turns.OtaghdarTurnNumber as SequentialTurnId,TurnAccountings.DateShamsi,TurnAccountings.Time,TurnAccountingTypes.AccountingTypeTitle,SoftwareUsers.UserName  from DBTransport.dbo.tbEnterExit as Turns
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnAccounting as TurnAccountings On Turns.nEnterExitId=TurnAccountings.nEnterExitId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnAccountingTypes as TurnAccountingTypes On TurnAccountings.AccountingTypeId=TurnAccountingTypes.AccountingTypeId 
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On TurnAccountings.UserId=SoftwareUsers.UserId 
                         Where Turns.nEnterExitId=" & YourTurnId & " for JSON path")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Turns.nEnterExitId as TurnId,Turns.OtaghdarTurnNumber as SequentialTurnId,TurnAccountings.DateShamsi,TurnAccountings.Time,TurnAccountingTypes.AccountingTypeTitle,SoftwareUsers.UserName  from DBTransport.dbo.tbEnterExit as Turns
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnAccounting as TurnAccountings On Turns.nEnterExitId=TurnAccountings.nEnterExitId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnAccountingTypes as TurnAccountingTypes On TurnAccountings.AccountingTypeId=TurnAccountingTypes.AccountingTypeId 
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On TurnAccountings.UserId=SoftwareUsers.UserId 
                         Where Turns.nEnterExitId=" & YourTurnId & " for JSON path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(Ds)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

    End Namespace

    Namespace TurnPrinting
        Public Structure R2CoreTransportationAndLoadNotificationTurnPrintingInf
            Dim TurnId As Int64
            Dim TurnDate As String
            Dim TurnTime As String
            Dim MoneyWalletInventory As Int64
            Dim TruckDriver As String
            Dim LoaderType As String
            Dim LoaderFixType As String
            Dim TruckLP As String
            Dim TruckLPSerial As String
            Dim UserName As String
            Dim TerraficCard As String
            Dim TurnSequentialId As String
            Dim BlackList As String
            Dim AnnouncementHallTitle As String
            Dim AnnouncementsubGroupTitle As String
            Dim Description As String
        End Structure

        Public Class R2CoreTransportationAndLoadNotificationMClassTurnPrintingManager
            Private WithEvents _PrintDocumentNobat As PrintDocument = New PrintDocument()
            Private TurnPrintingInf As R2CoreTransportationAndLoadNotificationTurnPrintingInf

            Private Function GetTurnPrintingInf(YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationTurnPrintingInf
                Try
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Top 1 EnterExit.strDesc as Description,EnterExit.nEnterExitId,EnterExit.strEnterDate,EnterExit.strEnterTime,RFIDCard.CardNo,RFIDCard.Charge,
                                    Person.strPersonFullName,LoaderType.LoaderTypeTitle,LoaderTypeFixStatus.LoaderTypeFixStatusTitle,Car.strCarNo,Car.strCarSerialNo,
	                                SoftwareUser.UserName,Substring(EnterExit.OtaghdarTurnNumber,7,20) as SequentialTurnId
	                                ,IsNull((Select Top 1 StrDesc from dbtransport.dbo.tbblacklist Where nTruckNo Collate Arabic_CI_AS_WS=Car.strCarNo Collate Arabic_CI_AS_WS and nPlakSerial Collate Arabic_CI_AS_WS=Car.strCarSerialNo Collate Arabic_CI_AS_WS and nPlakPlac=Car.nIDCity and flaga=0 Order By nID Desc),'') as BlackList,
                                    AH.AHTitle,AHSG.AHSGTitle
                       from dbtransport.dbo.tbEnterExit as EnterExit
                            Inner Join R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars as RFIDCardRCar On EnterExit.strCardno=RFIDCardRCar.nCarId
                            Inner Join R2Primary.dbo.TblRFIDCards as RFIDCard On RFIDCardRCar.CardId=RFIDCard.CardId
                            Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                            Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                            Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblLoaderTypes AS LoaderType On  LoaderType.LoaderTypeId=Car.snCarType
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatus On LoaderType.LoaderTypeFixStatusId=LoaderTypeFixStatus.LoaderTypeFixStatusId
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUser On EnterExit.nUserIdEnter=SoftwareUser.UserId
          					Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGRCar On EnterExit.strCardno=AHSGRCar.CarId
							Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHSGRCar.AHSGId=AHSG.AHSGId
							Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups AS AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
							Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                       Where EnterExit.nEnterExitId=" & YourTurnId & " and RFIDCardRCar.RelationActive=1 
                             and ((DATEDIFF(SECOND,RFIDCardRCar.RelationTimeStamp,getdate())<240) or (RFIDCardRCar.RelationTimeStamp='2015-01-01 00:00:00.000'))  
                             and ((DATEDIFF(SECOND,AHSGRCar.RelationTimeStamp,getdate())<240) or (AHSGRCar.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                             and AHRAHSG.RelationActive=1 and AHSGRCar.RelationActive=1 
                       Order By AHSGRCar.Priority Asc,RFIDCardRCar.RelationId Desc,AHSGRCar.RelationId Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TurnPrintingInfNotFoundException
                    Dim TurnPrintingInf As New R2CoreTransportationAndLoadNotificationTurnPrintingInf
                    TurnPrintingInf.TurnId = DS.Tables(0).Rows(0).Item("nEnterExitId")
                    TurnPrintingInf.TurnDate = DS.Tables(0).Rows(0).Item("strEnterDate").trim
                    TurnPrintingInf.TurnTime = DS.Tables(0).Rows(0).Item("strEnterTime").trim
                    TurnPrintingInf.MoneyWalletInventory = DS.Tables(0).Rows(0).Item("Charge")
                    TurnPrintingInf.TruckDriver = DS.Tables(0).Rows(0).Item("strPersonFullName").trim
                    TurnPrintingInf.LoaderType = DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim
                    TurnPrintingInf.LoaderFixType = DS.Tables(0).Rows(0).Item("LoaderTypeFixStatusTitle").trim
                    TurnPrintingInf.TruckLP = DS.Tables(0).Rows(0).Item("strCarNo").trim
                    TurnPrintingInf.TruckLPSerial = DS.Tables(0).Rows(0).Item("strCarSerialNo").trim
                    TurnPrintingInf.UserName = DS.Tables(0).Rows(0).Item("UserName").trim
                    TurnPrintingInf.TerraficCard = DS.Tables(0).Rows(0).Item("CardNo").trim
                    TurnPrintingInf.TurnSequentialId = DS.Tables(0).Rows(0).Item("SequentialTurnId")
                    TurnPrintingInf.BlackList = DS.Tables(0).Rows(0).Item("BlackList").trim
                    TurnPrintingInf.AnnouncementHallTitle = DS.Tables(0).Rows(0).Item("AHTitle").trim
                    TurnPrintingInf.AnnouncementsubGroupTitle = DS.Tables(0).Rows(0).Item("AHSGTitle").trim
                    TurnPrintingInf.Description = DS.Tables(0).Rows(0).Item("Description").trim
                    Return TurnPrintingInf
                Catch ex As TurnPrintingInfNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub TurnPrint(YourTurnId As Int64)
                Try
                    Dim InstanceConfigurationOfComputers = New R2CoreMClassConfigurationOfComputersManager
                    Dim InstanceComputers = New R2CoreMClassComputersManager
                    If InstanceConfigurationOfComputers.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TurnControlling, InstanceComputers.GetNSSCurrentComputer.MId, 2) Then
                        TurnPrintingInf = GetTurnPrintingInf(YourTurnId)
                        'چاپ قبض نوبت
                        _PrintDocumentNobat.Print()
                    End If
                Catch ex As TurnPrintingInfNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentNobat_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentNobat.BeginPrint
            End Sub

            Private Sub _PrintDocumentNobat_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentNobat.EndPrint
            End Sub

            Private Sub _PrintDocumentNobat_PrintPage_Printing(ByVal X As Int16, ByVal Y As Int16, ByVal E As System.Drawing.Printing.PrintPageEventArgs)
                Try
                    Dim myPaperSizeHalf As Integer = _PrintDocumentNobat.PrinterSettings.DefaultPageSettings.PaperSize.Width / 4
                    Dim myStrFontPersonFullName As Font = New System.Drawing.Font("Badr", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMax As Font = New System.Drawing.Font("Badr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMin As Font = New System.Drawing.Font("Badr", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myDigFont As Font = New System.Drawing.Font("Alborz Titr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                    Dim myStrFontAnnouncementHallName As Font = New System.Drawing.Font("Badr", 26.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontAnnouncementsubGroupName As Font = New System.Drawing.Font("Badr", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    E.Graphics.DrawString(TurnPrintingInf.TurnDate, myStrFontMax, Brushes.DarkBlue, X, Y)
                    ''E.Graphics.DrawString(TurnPrintingInf.TurnId, myDigFont, Brushes.DarkBlue, X + 350, Y)
                    E.Graphics.DrawString(IIf(TurnPrintingInf.TurnSequentialId = String.Empty, "", Convert.ToInt64(TurnPrintingInf.TurnSequentialId)), myDigFont, Brushes.DarkBlue, X + 350, Y)
                    E.Graphics.DrawString(TurnPrintingInf.TurnTime, myStrFontMax, Brushes.DarkBlue, X, Y + 30)
                    E.Graphics.DrawString(R2CoreMClassPublicProcedures.ParseSignDigitToTashString(TurnPrintingInf.MoneyWalletInventory), myStrFontMax, Brushes.DarkBlue, X + 400, Y + 30)
                    E.Graphics.DrawString(TurnPrintingInf.TruckDriver, myStrFontPersonFullName, Brushes.DarkBlue, X + 280, Y + 50)
                    E.Graphics.DrawString(TurnPrintingInf.Description, myStrFontMax, Brushes.DarkBlue, X, Y + 150)
                    E.Graphics.DrawString(TurnPrintingInf.AnnouncementHallTitle, myStrFontAnnouncementHallName, Brushes.DarkBlue, X + 130, Y + 180)
                    E.Graphics.DrawString(TurnPrintingInf.AnnouncementsubGroupTitle, myStrFontAnnouncementsubGroupName, Brushes.DarkBlue, X + 150, Y + 215)
                    E.Graphics.DrawString(TurnPrintingInf.LoaderFixType, myStrFontMax, Brushes.DarkBlue, X, Y + 70)
                    E.Graphics.DrawString(TurnPrintingInf.LoaderType, myStrFontMax, Brushes.DarkBlue, X, Y + 90)
                    E.Graphics.DrawString(TurnPrintingInf.TruckLPSerial + "-", myStrFontMax, Brushes.DarkBlue, X + 260, Y + 90)
                    Dim a As Char() = TurnPrintingInf.TruckLP.ToCharArray()
                    E.Graphics.DrawString(a(4), myStrFontMax, Brushes.DarkBlue, X + 290, Y + 90)
                    E.Graphics.DrawString(a(5), myStrFontMax, Brushes.DarkBlue, X + 300, Y + 90)
                    E.Graphics.DrawString(a(3), myStrFontMax, Brushes.DarkBlue, X + 310, Y + 90)
                    E.Graphics.DrawString(a(0), myStrFontMax, Brushes.DarkBlue, X + 325, Y + 90)
                    E.Graphics.DrawString(a(1), myStrFontMax, Brushes.DarkBlue, X + 335, Y + 90)
                    E.Graphics.DrawString(a(2), myStrFontMax, Brushes.DarkBlue, X + 345, Y + 90)
                    E.Graphics.DrawString(TurnPrintingInf.UserName, myStrFontMax, Brushes.DarkBlue, X, Y + 110)
                    E.Graphics.DrawString(TurnPrintingInf.TerraficCard, myStrFontMax, Brushes.DarkBlue, X + 400, Y + 110)
                    ''E.Graphics.DrawString(IIf(TurnPrintingInf.TurnSequentialId = String.Empty, "", Convert.ToInt64(TurnPrintingInf.TurnSequentialId)), myDigFont, Brushes.DarkBlue, X, Y + 150)
                    E.Graphics.DrawString(TurnPrintingInf.BlackList, myStrFontMin, Brushes.DarkBlue, X, Y + 170)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _PrintDocumentNobat.PrintPage
                Try
                    _PrintDocumentNobat_PrintPage_Printing(150, 40, e)
                Catch ex As Exception
                    MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

    Namespace TurnPrintRequest

        Public MustInherit Class TurnPrintRequestTypes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property NoCost = 1
            Public Shared ReadOnly Property Replica = 2
        End Class

        Public Class R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManager

            Public Sub NoCostTurnPrintRequest(YourTurnId As Int64, YourTurnPrintRedirect As Boolean)
                Try
                    TurnPrintRequest(YourTurnId, YourTurnPrintRedirect)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub TurnPrintRequest(YourTurnId As Int64, YourTurnPrintRedirect As Boolean)
                Try
                    Dim InstanceTurnPrinting = New R2CoreTransportationAndLoadNotificationMClassTurnPrintingManager
                    Dim InstanceConfigurationOfComputers = New R2CoreMClassConfigurationOfComputersManager
                    Dim InstanceComputers = New R2CoreMClassComputersManager
                    Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                    If Not YourTurnPrintRedirect Then
                        InstanceTurnPrinting.TurnPrint(YourTurnId)
                    Else
                        If InstanceConfigurationOfComputers.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TurnControlling, InstanceComputers.GetNSSCurrentComputer.MId, 0) = True Then
                            Dim DataStruct As DataStruct = New DataStruct()
                            DataStruct.Data1 = YourTurnId
                            Dim NSSTruck = InstanceTurns.GetNSSTruck(YourTurnId)
                            Dim InstanceComputerMessages = New R2CoreMClassComputerMessagesManager
                            InstanceComputerMessages.SendComputerMessage(New R2StandardComputerMessageStructure(Nothing, NSSTruck.NSSCar.GetCarPelakSerialComposit(), R2CoreTransportationAndLoadNotificationComputerMessageTypes.TurnPrint, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DataStruct))
                        End If
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManagement

            Public Shared Sub NoCostTurnPrintRequest(YourTurnId As Int64, YourTurnPrintRedirect As Boolean)
                Try
                    TurnPrintRequest(YourTurnId, YourTurnPrintRedirect)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Sub ReplicaTurnPrintRequest(YourTurnId As Int64, YourTurnPrintRedirect As Boolean)
                Try
                    TurnPrintRequest(YourTurnId, YourTurnPrintRedirect)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Shared Sub TurnPrintRequest(YourTurnId As Int64, YourTurnPrintRedirect As Boolean)
                Try
                    Dim InstanceTurnPrinting = New R2CoreTransportationAndLoadNotificationMClassTurnPrintingManager
                    If Not YourTurnPrintRedirect Then
                        InstanceTurnPrinting.TurnPrint(YourTurnId)
                    Else
                        If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TurnControlling, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True Then
                            Dim DataStruct As DataStruct = New DataStruct()
                            DataStruct.Data1 = YourTurnId
                            Dim NSSTruck = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YourTurnId)
                            R2CoreMClassComputerMessagesManagement.SendComputerMessage(New R2StandardComputerMessageStructure(Nothing, NSSTruck.NSSCar.GetCarPelakSerialComposit(), R2CoreTransportationAndLoadNotificationComputerMessageTypes.TurnPrint, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DataStruct))
                        End If
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub


        End Class

    End Namespace

    Namespace SequentialTurns

        Public MustInherit Class SequentialTurns
            Public Shared ReadOnly Property None As Int64 = 0
            Public Shared ReadOnly Property SequentialTurnOtaghdar As Int64 = 1
            Public Shared ReadOnly Property SequentialTurnZobi As Int64 = 2
            Public Shared ReadOnly Property SequentialTurnShahri As Int64 = 3
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
            Public Sub New()
                _SequentialTurnId = Int64.MinValue
                _SequentialTurnTitle = String.Empty
                _SequentialTurnColor = String.Empty
                _SequentialTurnKeyWord = String.Empty
                _Active = False
                _ViewFlag = False
                _Deleted = True
            End Sub

            Public Sub New(ByVal YourSequentialTurnId As Int64, ByVal YourSequentialTurnTitle As String, YourSequentialTurnColor As String, YourSequentialTurnKeyWord As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
                _SequentialTurnId = YourSequentialTurnId
                _SequentialTurnTitle = YourSequentialTurnTitle
                _SequentialTurnColor = YourSequentialTurnColor
                _SequentialTurnKeyWord = YourSequentialTurnKeyWord
                _Active = YourActive
                _ViewFlag = YourViewFlag
                _Deleted = YourDeleted
            End Sub

            Public Property SequentialTurnId As Int64
            Public Property SequentialTurnTitle As String
            Public Property SequentialTurnColor As String
            Public Property SequentialTurnKeyWord As String
            Public Property Active As Boolean
            Public Property ViewFlag As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager

            Public Function GetSequentialTurns() As List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                Try
                    Dim Ds As DataSet
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Distinct SeqT.SeqTId,SeqT.SeqTTitle,SeqT.SeqTColor,SeqT.SeqTKeyWord,SeqT.Active,SeqT.ViewFlag,SeqT.Deleted 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                         Where SeqT.Deleted=0 and SeqT.Active=1", 3600, Ds, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(Ds.Tables(0).Rows(Loopx).Item("SeqTId"), Ds.Tables(0).Rows(Loopx).Item("SeqTTitle").trim, Ds.Tables(0).Rows(Loopx).Item("SeqTColor").trim, Ds.Tables(0).Rows(Loopx).Item("SeqTKeyWord"), Ds.Tables(0).Rows(Loopx).Item("ViewFlag"), Ds.Tables(0).Rows(Loopx).Item("Active"), Ds.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSequentialTurns(YourNSSComputer As R2CoreStandardComputerStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                Try
                    Dim DS As DataSet
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Distinct SeqT.SeqTId,SeqT.SeqTTitle,SeqT.SeqTColor,SeqT.SeqTKeyWord,SeqT.Active,SeqT.ViewFlag,SeqT.Deleted from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT On SeqT.SeqTId=AHRSeqT.SeqTId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRSeqT.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationComputers as AHRComp On AH.AHId=AHRComp.AHId
                            Inner Join R2Primary.dbo.TblComputers as Comp On AHRComp.ComId=Comp.MId
                          Where AHRComp.RelationActive=1 and AH.Deleted=0 and AHRSeqT.RelationActive=1 and SeqT.Deleted=0 and SeqT.ViewFlag=1 and Comp.MId=" & YourNSSComputer.MId & "", 3600, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(Loopx).Item("SeqTId"), DS.Tables(0).Rows(Loopx).Item("SeqTTitle").trim, DS.Tables(0).Rows(Loopx).Item("SeqTColor").trim, DS.Tables(0).Rows(Loopx).Item("SeqTKeyWord"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSequentialTurn(YourSeqTId As Int64) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.DBO.TblSequentialTurns Where SeqTId=" & YourSeqTId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New SequentialTurnNotFoundException
                    Return New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(0).Item("SeqTId"), DS.Tables(0).Rows(0).Item("SeqTTitle").trim, DS.Tables(0).Rows(0).Item("SeqTColor").trim, DS.Tables(0).Rows(0).Item("SeqTKeyWord").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSequentialTurn(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim SeqTurnKeyWord = Mid(YourNSSTurn.OtaghdarTurnNumber, 1, 1)
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.DBO.TblSequentialTurns Where SeqTKeyWord='" & SeqTurnKeyWord & "'", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New SequentialTurnNotFoundException
                    Return New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(0).Item("SeqTId"), DS.Tables(0).Rows(0).Item("SeqTTitle").trim, DS.Tables(0).Rows(0).Item("SeqTColor").trim, DS.Tables(0).Rows(0).Item("SeqTKeyWord").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSequentialTurn(YourNSSAHSG As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                               "Select Top 1 SeqTId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationSequentialTurns as AHSGRSeqT
                                Where AHSGId=" & YourNSSAHSG.AHSGId & " and RelationActive=1 Order By RId Desc", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New SequentialTurnNotFoundException
                    Return GetNSSSequentialTurn(Convert.ToInt64(DS.Tables(0).Rows(0).Item("SeqTId")))
                Catch ex As SequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassSequentialTurnsManagement
            Public Shared Function GetNSSSequentialTurn(YourSeqTId As Int64) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.DBO.TblSequentialTurns Where SeqTId=" & YourSeqTId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New SequentialTurnNotFoundException
                    Return New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(0).Item("SeqTId"), DS.Tables(0).Rows(0).Item("SeqTTitle").trim, DS.Tables(0).Rows(0).Item("SeqTColor").trim, DS.Tables(0).Rows(0).Item("SeqTKeyWord").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetNSSSequentialTurn(YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "
                          Select Top 1 SeqTs.SeqTId from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqTs
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqTs On SeqTs.SeqTId=AHRSeqTs.SeqTId 
	                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs on AHRSeqTs.AHId=AHs.AHId 
                          Where AHRSeqTs.RelationActive=1 and AHs.AHId=" & YourNSSAnnouncementHall.AHId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New GetDataException
                    Return GetNSSSequentialTurn(Convert.ToInt64(DS.Tables(0).Rows(0).Item("SeqTId")))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetNSSSequentialTurn(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                       "Select  Top 1 SeqT.* from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT On SeqT.SeqTId=AHRSeqT.SeqTId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRSeqT.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AH.AHId=AHRAHSG.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHRAHSG.AHSGId=AHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars as AHSGRCar On AHSG.AHSGId=AHSGRCar.AHSGId
                            Inner Join dbtransport.dbo.TbCar as Car On AHSGRCar.CarId=Car.nIDCar
                        Where AHRSeqT.RelationActive=1 and AH.Deleted=0 and AHRAHSG.RelationActive=1 and AHSG.Deleted=0 and AHSGRCar.RelationActive=1 and Car.nIDCar=" & YourNSSTruck.NSSCar.nIdCar & " and Car.ViewFlag=1 
                              and ((DATEDIFF(SECOND,AHSGRCar.RelationTimeStamp,getdate())<240) or (AHSGRCar.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                        Order By AHSGRCar.Priority Asc,AHSGRCar.RelationId Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckRelatedSequentialTurnNotFoundException
                    Return New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(0).Item("SeqTId"), DS.Tables(0).Rows(0).Item("SeqTTitle").trim, DS.Tables(0).Rows(0).Item("SeqTColor").trim, DS.Tables(0).Rows(0).Item("SeqTKeyWord").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As TruckRelatedSequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetSequentialTurns(YourNSSComputer As R2CoreStandardComputerStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                Try
                    Dim DS As DataSet
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Distinct SeqT.SeqTId,SeqT.SeqTTitle,SeqT.SeqTColor,SeqT.SeqTKeyWord,SeqT.Active,SeqT.ViewFlag,SeqT.Deleted from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT On SeqT.SeqTId=AHRSeqT.SeqTId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRSeqT.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationComputers as AHRComp On AH.AHId=AHRComp.AHId
                            Inner Join R2Primary.dbo.TblComputers as Comp On AHRComp.ComId=Comp.MId
                          Where AHRComp.RelationActive=1 and AH.Deleted=0 and AHRSeqT.RelationActive=1 and SeqT.Deleted=0 and SeqT.ViewFlag=1 and Comp.MId=" & YourNSSComputer.MId & "", 3600, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure(DS.Tables(0).Rows(Loopx).Item("SeqTId"), DS.Tables(0).Rows(Loopx).Item("SeqTTitle").trim, DS.Tables(0).Rows(Loopx).Item("SeqTColor").trim, DS.Tables(0).Rows(Loopx).Item("SeqTKeyWord"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetNSSSequentialTurn(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
                Try
                    Dim DS As DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                               "Select Top 1 SeqT.SeqTId from dbtransport.dbo.tbEnterExit as EnterExit 
                                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On SUBSTRING(EnterExit.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS
                                     Where EnterExit.nEnterExitId=" & YourNSSTurn.nEnterExitId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TurnNotFoundException
                    Return GetNSSSequentialTurn(Convert.ToInt64(DS.Tables(0).Rows(0).Item("SeqTId")))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function


        End Class

        Namespace Exceptions
            Public Class SequentialTurnNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تسلسل نوبت با شماره شاخص مورد نظر وجود ندارد"
                    End Get
                End Property
            End Class

            Public Class TruckRelatedSequentialTurnNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "برای ناوگان ، تسلسل نوبت مرتبط یافت نشد"
                    End Get
                End Property
            End Class

            Public Class SequentialTurnIsNotActiveException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تسلسل نوبت مورد نظر غیرفعال است"
                    End Get
                End Property
            End Class

            Public Class AnySequentialTurnDoNotSelectedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "صف نوبت مورد نظر را انتخاب و یا مشخص نمایید"
                    End Get
                End Property
            End Class

        End Namespace

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationSequentialTurn
            Public Property SeqTurnId As Int64
            Public Property SeqTurnTitle As String
            Public Property SeqTurnKeyWord As String
            Public Property Active As Boolean
        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationSequentialTurnsManager
            Public Function GetSequentialTurns(YourSearchString As String, YourImmediately As Boolean) As String
                Try
                    Dim Ds As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                             Select SeqT.SeqTId as SeqTurnId,SeqT.SeqTTitle as SeqTurnTitle,SeqT.SeqTKeyWord as SeqTurnKeyWord,SeqT.Active as Active
                             from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                             Where SeqT.SeqTTitle Like N'%" & YourSearchString & "%' and SeqT.Deleted=0 Order By SeqT.SeqTId
                             for json path")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                             Select SeqT.SeqTId as SeqTurnId,SeqT.SeqTTitle as SeqTurnTitle,SeqT.SeqTKeyWord as SeqTurnKeyWord,SeqT.Active as Active
                             from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT
                             Where SeqT.SeqTTitle Like N'%" & YourSearchString & "%' SeqT.Deleted=0 Order By SeqT.SeqTId
                             for json path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(Ds)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSequentialTurn(YourSeqTId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationSequentialTurn
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager

                    Dim DS As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("Select Top 1 * from R2PrimaryTransportationAndLoadNotification.DBO.TblSequentialTurns Where SeqTId=" & YourSeqTId & "")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(DS) <= 0 Then Throw New SequentialTurnNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                                                          "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.DBO.TblSequentialTurns Where SeqTId=" & YourSeqTId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New SequentialTurnNotFoundException
                    End If
                    Return New R2CoreTransportationAndLoadNotificationSequentialTurn With {.SeqTurnId = DS.Tables(0).Rows(0).Item("SeqTId"), .SeqTurnTitle = DS.Tables(0).Rows(0).Item("SeqTTitle").trim, .SeqTurnKeyWord = DS.Tables(0).Rows(0).Item("SeqTKeyWord").trim, .Active = DS.Tables(0).Rows(0).Item("Active")}
                Catch ex As SequentialTurnNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSequentialTurnsByLoaderTypeId(YourLoaderTypeId As Int64, YourImmediately As Boolean) As String
                Try
                    Dim Ds As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                             Select SeqTs.SeqTId as SeqTurnId,SeqTs.SeqTTitle as SeqTurnTitle,SeqTs.SeqTKeyWord as SeqTurnKeyWord,SeqTs.Active as Active
                                from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqTs
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes as SequentialTurnsRelationLoaderTypes On SeqTs.SeqTId=SequentialTurnsRelationLoaderTypes.SeqTId  
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On SequentialTurnsRelationLoaderTypes.LoaderTypeId=LoaderTypes.LoaderTypeId 
                             Where LoaderTypes.LoaderTypeId=" & YourLoaderTypeId & " and SeqTs.Deleted=0 and LoaderTypes.Deleted=0 Order By SeqTs.SeqTId
                             for json path")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                             Select SeqTs.SeqTId as SeqTurnId,SeqTs.SeqTTitle as SeqTurnTitle,SeqTs.SeqTKeyWord as SeqTurnKeyWord,SeqTs.Active as Active
                                from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqTs
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes as SequentialTurnsRelationLoaderTypes On SeqTs.SeqTId=SequentialTurnsRelationLoaderTypes.SeqTId  
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On SequentialTurnsRelationLoaderTypes.LoaderTypeId=LoaderTypes.LoaderTypeId 
                             Where LoaderTypes.LoaderTypeId=" & YourLoaderTypeId & " and SeqTs.Deleted=0 and LoaderTypes.Deleted=0 Order By SeqTs.SeqTId
                             for json path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(Ds)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub SequentialTurnRegistering(YourRawSeqT As R2CoreTransportationAndLoadNotificationSequentialTurn)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Select Top 1 SeqTId From R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns with (tablockx) Order By SeqTId Desc"
                    Dim SeqTIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns(SeqTId,SeqTTitle,SeqTKeyWord,Active,ViewFlag,Deleted)
                                          Values(" & SeqTIdNew & ",'" & YourRawSeqT.SeqTurnTitle & "','" & YourRawSeqT.SeqTurnKeyWord & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub SequentialTurnEditing(YourRawSeqT As R2CoreTransportationAndLoadNotificationSequentialTurn)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns
                                          Set SeqTTitle='" & YourRawSeqT.SeqTurnTitle & "',SeqTKeyWord='" & YourRawSeqT.SeqTurnKeyWord & "',Active=" & IIf(YourRawSeqT.Active, 1, 0) & "
                                          Where SeqTId=" & YourRawSeqT.SeqTurnId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub SequentialTurnDeleting(YourSeqTId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns
                                          Where SeqTId=" & YourSeqTId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function GetSequentialTurnsRelationLoaderTypes(YourSeqTId As Int64, YourImmediately As Boolean) As String
                Try
                    Dim Ds As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                             Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes as SequentialTurnsRelationLoaderTypes On SequentialTurns.SeqTId=SequentialTurnsRelationLoaderTypes.SeqTId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On SequentialTurnsRelationLoaderTypes.LoaderTypeId=LoaderTypes.LoaderTypeId 
                             Where SequentialTurns.SeqTId=" & YourSeqTId & " and SequentialTurns.Deleted=0 and LoaderTypes.Deleted=0
                             for JSON Auto")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                             Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes as SequentialTurnsRelationLoaderTypes On SequentialTurns.SeqTId=SequentialTurnsRelationLoaderTypes.SeqTId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On SequentialTurnsRelationLoaderTypes.LoaderTypeId=LoaderTypes.LoaderTypeId 
                             Where SequentialTurns.SeqTId=" & YourSeqTId & " and SequentialTurns.Deleted=0 and LoaderTypes.Deleted=0
                             for JSON Auto", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(Ds)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub SequentialTurnRelationLoaderTypeDeleting(YourSeqTId As Int64, YourLoaderTypeId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes
                                          Where SeqTId=" & YourSeqTId & " and LoaderTypeId =" & YourLoaderTypeId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub SequentialTurnRelationLoaderTypeRegistering(YourSeqTId As Int64, YourLoaderTypeId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationLoaderTypes(SeqTId,LoaderTypeId) 
                                          Values(" & YourSeqTId & "," & YourLoaderTypeId & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function GetSequentialTurnRelationAnnouncementSubGroups(YourSeqTId As Int64, YourImmediately As Boolean) As String
                Try
                    Dim Ds As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                             Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle
                             from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationAnnouncementSubGroups as SequentialTurnsRelationAnnouncementSubGroups On SequentialTurns.SeqTId=SequentialTurnsRelationAnnouncementSubGroups.SeqTId 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On SequentialTurnsRelationAnnouncementSubGroups.AnnouncementSGId =AnnouncementSubGroups.AnnouncementSGId 
                             Where SequentialTurns.SeqTId=" & YourSeqTId & " and SequentialTurns.Deleted=0 and AnnouncementSubGroups.Deleted=0
                             for JSON Auto")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                             Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle
                             from R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationAnnouncementSubGroups as SequentialTurnsRelationAnnouncementSubGroups On SequentialTurns.SeqTId=SequentialTurnsRelationAnnouncementSubGroups.SeqTId 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On SequentialTurnsRelationAnnouncementSubGroups.AnnouncementSGId =AnnouncementSubGroups.AnnouncementSGId 
                             Where SequentialTurns.SeqTId=" & YourSeqTId & " and SequentialTurns.Deleted=0 and AnnouncementSubGroups.Deleted=0
                             for JSON Auto", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(Ds)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub SequentialTurnRelationAnnouncementSubGroupDeleting(YourSeqTId As Int64, YourAnnouncementSGId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationAnnouncementSubGroups
                                      Where SeqTId=" & YourSeqTId & " and AnnouncementSGId=" & YourAnnouncementSGId & ""
                    CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub SequentialTurnRelationAnnouncementSubGroupRegistering(YourSeqTId As Int64, YourAnnouncementSGId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurnsRelationAnnouncementSubGroups(SeqTId,AnnouncementSGId)
                                      Values(" & YourSeqTId & "," & YourAnnouncementSGId & ")"
                    CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

    Namespace TurnRegisteringCostTariffs


    End Namespace

    Namespace TurnExpiration
        Public Class R2CoreTransportationAndLoadNotificationMClassTurnExpirationManager
            Private _DateTime As New R2DateTime

            Public Sub TurnCreditRegistering(YourNSSSeqTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCredits Set Active=0 Where Active=1 and SeqTId=" & YourNSSSeqTurn.SequentialTurnId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCredits(SeqTId,SignificantTurnId,OtaghdarTurnNumber,DateTimeMilladi,DateShamsi,Time,UserId,Active,ViewFlag,Deleted)
                                          Values(" & YourNSSSeqTurn.SequentialTurnId & "," & YourNSSTurn.nEnterExitId & ",'" & YourNSSTurn.OtaghdarTurnNumber.Trim & "','" & _DateTime.GetCurrentDateTimeMilladiFormated & "','" & _DateTime.GetCurrentDateShamsiFull & "','" & _DateTime.GetCurrentTime & "'," & YourNSSSoftwareUser.UserId & ",1,1,0)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTurnExpirationManagement
            Private Shared _DateTime As New R2DateTime

            Public Shared Sub TurnsExpiration()
                Try
                    Dim Ds As DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Turns.nEnterExitId,TRRequests.TRRId from dbtransport.dbo.tbEnterExit as Turns
                         Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1
                         Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblTurnRegisterRequests AS TRRequests ON EntityRelations.E2=TRRequests.TRRId
                       Where (TurnStatus=1 or TurnStatus=8 or TurnStatus=9 or TurnStatus=10) and EntityRelations.RelationActive=1", 1, Ds, New Boolean).GetRecordsCount = 0 Then Return

                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Try
                            Dim NSSTurn = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nEnterExitId")))
                            Dim NSSTurnRegisterRequest = R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManagement.GetNSSTurnRegisterRequest(Ds.Tables(0).Rows(Loopx).Item("TRRId"))
                            If NSSTurnRegisterRequest.TRRTypeId = TurnRegisterRequestTypes.RealTime Then
                                If IsTurnOfRealTimeTurnRegisterRequestExpired(NSSTurn) Then ChangeTurnStatusToExpiration(NSSTurn)
                            ElseIf NSSTurnRegisterRequest.TRRTypeId = TurnRegisterRequestTypes.Emergency Then
                                If IsTurnOfEmergencyTurnRegisterRequestExpired(NSSTurn) Then ChangeTurnStatusToExpiration(NSSTurn)
                            ElseIf NSSTurnRegisterRequest.TRRTypeId = TurnRegisterRequestTypes.Reserve Then
                                If IsTurnOfReservedTurnRegisterRequestExpired(NSSTurn) Then ChangeTurnStatusToExpiration(NSSTurn)
                            Else
                                Throw New TurnRegisterRequestTypeNotFoundException
                            End If
                        Catch ex As Exception
                            R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, R2CoreTransportationAndLoadNotificationLogType.TurnExpirationFailed, ex.Message, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Nothing, Nothing, Nothing))
                        End Try
                    Next

                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Shared Sub ChangeTurnStatusToExpiration(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
                Try


                Catch ex As Exception

                End Try
            End Sub

            Private Shared Function IsTurnOfRealTimeTurnRegisterRequestExpired(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
                Try

                Catch ex As Exception

                End Try
            End Function

            Private Shared Function IsTurnOfEmergencyTurnRegisterRequestExpired(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
                Try

                Catch ex As Exception

                End Try
            End Function

            Private Shared Function IsTurnOfReservedTurnRegisterRequestExpired(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As Boolean
                Try

                Catch ex As Exception

                End Try
            End Function

        End Class


    End Namespace

    Namespace Exceptions
        Public Class AnyActiveTurnNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "هیچ نوبت فعالی در سامانه وجود ندارد"
                End Get
            End Property
        End Class

        Public Class UnableResucitationTemporayTurnException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان احیاء نوبت موقت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TurnPrintingInfNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات تکمیلی مرتبط با نوبت مورد نظر برای چاپ نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TurnNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درحال حاضر نوبتی در سامانه وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TurnHandlingNotAllowedBecuaseTurnStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت فعلی نوبت مانع از اجرای این فرآیند شد"
                End Get
            End Property
        End Class

        Public Class RelationBetweenTurnAndTruckNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ناوگانی مرتبط با نوبت یافت نشد"
                End Get
            End Property
        End Class

        Public Class TurnRegisterRequestTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع درخواست صدور نوبت با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TurnRegisterRequestNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درخواست صدور نوبت با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserRelatedTurnNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری (راننده) مرتبط با نوبت مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class RequesterNotAllowTurnIssueBySeqTException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درخواست کننده ، مجوز درخواست صدور نوبت برای تسلسل نوبت مورد نظر را ندارد"
                End Get
            End Property
        End Class

        Public Class RequesterNotAllowTurnIssueByLastLoadPermissionedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درخواست کننده ، مجوز درخواست صدور نوبت با توجه به نوع آخرین بار دریافت شده را ندارد"
                End Get
            End Property
        End Class

        Public Class FirstActiveTurnNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره اولین نوبت دارای اعتبار یافت نشد"
                End Get
            End Property
        End Class

        Public Class LastActiveTurnNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "آخرین نوبت فعال ناوگان یافت نشد"
                End Get
            End Property
        End Class

        Public Class LastTurnCreditNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "آخرین شماره اعتبار نوبت یافت نشد"
                End Get
            End Property
        End Class

        Public Class TurnStatusNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "رکوردی مبنی بر شاخص وضعیت نوبت در سامانه وجود ندارد"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class TurnCancellationNotPossibleBecuaseTurnStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "با توجه به وضعیت نوبت ، ابطال نوبت امکان پذیر نمی باشد"
                End Get
            End Property
        End Class

        Public Class TurnResuscitationNotPossibleBecuaseTurnStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "با توجه به وضعیت نوبت ، احیاء نوبت امکان پذیر نمی باشد"
                End Get
            End Property
        End Class

        Public Class TurnCostNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعاتی درخصوص هزینه های نوبت یافت نشد"
                End Get
            End Property
        End Class




    End Namespace

End Namespace
