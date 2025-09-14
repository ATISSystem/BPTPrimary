

Imports System.Data.OleDb
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Windows.Forms

Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2Core.ExceptionManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.BlackList
Imports R2CoreParkingSystem.Cars
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.Announcements.Exceptions
Imports R2CoreTransportationAndLoadNotification.ComputerMessages
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorAccounting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadOtherThanManipulation
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission
Imports R2CoreTransportationAndLoadNotification.LoadPermission.Exceptions
Imports R2CoreTransportationAndLoadNotification.Goods.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.Logging
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.TurnAttendance.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TurnAttendance
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2Core.SMS.SMSHandling
Imports R2CoreTransportationAndLoadNotification.LoadSources
Imports System.Net
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports R2Core.PublicProc
Imports R2CoreGUI
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.Exceptions
Imports R2CoreTransportationAndLoadNotification.DriverSelfDeclaration.Exceptions
Imports R2CoreTransportationAndLoadNotification.RequesterManagement
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports Newtonsoft.Json
Imports R2Core.CachHelper
Imports R2CoreTransportationAndLoadNotification.PubSubMessaging
Imports StackExchange.Redis
Imports R2Core.DateTimeProvider



Namespace LoadPermission

    Public Enum R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation
        None = 0
        TransportCompany = 1
        AnnouncementHall = 2
    End Enum

    Public MustInherit Class R2CoreTransportationAndLoadNotificationLoadPermissionStatuses
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Registered As Int64 = 1
        Public Shared ReadOnly Property Cancelled As Int64 = 2
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure

        Public Sub New()
            _LoadPermissionStatusId = Int64.MinValue
            _LoadPermissionStatusTitle = String.Empty
            _LoadPermissionStatusColor = String.Empty
        End Sub

        Public Sub New(ByVal YourLoadPermissionStatusId As Int64, YourLoadPermissionStatusTitle As String, YourLoadPermissionStatusColor As String)
            _LoadPermissionStatusId = YourLoadPermissionStatusId
            _LoadPermissionStatusTitle = YourLoadPermissionStatusTitle
            _LoadPermissionStatusColor = YourLoadPermissionStatusColor
        End Sub

        Public Property LoadPermissionStatusId As Int64
        Public Property LoadPermissionStatusTitle As String
        Public Property LoadPermissionStatusColor As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure
        Public Sub New()
            MyBase.New()
            _nEstelamId = Long.MinValue
            _TurnId = Long.MinValue
            _LoadPermissionDate = String.Empty
            _LoadPermissionTime = String.Empty
            _LoadPermissionRegisteringLocation = String.Empty
            _UserId = Long.MinValue
            _LoadPermissionStatusId = Long.MinValue
        End Sub

        Public Sub New(ByVal YournEstelamId As Int64, ByVal YourTurnId As Int64, YourLoadPermissionDate As String, YourLoadPermissionTime As String, YourLoadPermissionRegisteringLocation As String, YourUserId As Int64, YourLoadPermissionStatusid As Int64)
            _nEstelamId = YournEstelamId
            _TurnId = YourTurnId
            _LoadPermissionDate = YourLoadPermissionDate
            _LoadPermissionTime = YourLoadPermissionTime
            _LoadPermissionRegisteringLocation = YourLoadPermissionRegisteringLocation
            _UserId = YourUserId
            _LoadPermissionStatusId = YourLoadPermissionStatusid
        End Sub

        Public Property nEstelamId As Int64
        Public Property TurnId As Int64
        Public Property LoadPermissionDate As String
        Public Property LoadPermissionTime As String
        Public Property LoadPermissionRegisteringLocation As String
        Public Property UserId As Int64
        Public Property LoadPermissionStatusId As Int64

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
        Inherits R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure

        Public Sub New()
            MyBase.New()
            _TransportCompanyTitle = String.Empty
            _GoodTitle = String.Empty
            _LoadTargetTitle = String.Empty
            _StrDescription = String.Empty
            _Truck = String.Empty
            _TruckDriver = String.Empty
            _LoadPermissionStatusTitle = String.Empty
            _SequentialTurnNumber = String.Empty
        End Sub

        Public Sub New(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure, ByVal YourTransportCompanyTitle As String, ByVal YourGoodTitle As String, YourLoadTargetTitle As String, YourStrDescription As String, YourTruck As String, YourTruckDriver As String, YourLoadPermissionStatusTitle As String, YourSequentialTurnNumber As String)
            MyBase.New(YourNSS.nEstelamId, YourNSS.TurnId, YourNSS.LoadPermissionDate, YourNSS.LoadPermissionTime, YourNSS.LoadPermissionRegisteringLocation, YourNSS.UserId, YourNSS.LoadPermissionStatusId)
            _TransportCompanyTitle = YourTransportCompanyTitle
            _GoodTitle = YourGoodTitle
            _LoadTargetTitle = YourLoadTargetTitle
            _StrDescription = YourStrDescription
            _Truck = YourTruck
            _TruckDriver = YourTruckDriver
            _LoadPermissionStatusTitle = YourLoadPermissionStatusTitle
            _SequentialTurnNumber = YourSequentialTurnNumber
        End Sub

        Public Property TransportCompanyTitle As String
        Public Property GoodTitle As String
        Public Property LoadTargetTitle As String
        Public Property StrDescription As String
        Public Property Truck As String
        Public Property TruckDriver As String
        Public Property LoadPermissionStatusTitle As String
        Public Property SequentialTurnNumber As String

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure
        Inherits R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure

        Public Sub New()
            MyBase.New()
            _StrDescription = String.Empty
            _Truck = String.Empty
            _TruckSmartCardNo = String.Empty
            _TruckDriver = String.Empty
            _StrDrivingLicenceNo = String.Empty
            _StrNationalCode = String.Empty
            _StrSmartCardNo = String.Empty
            _IssuedLocation = String.Empty
            _Mobile = String.Empty
            _strAddress = String.Empty
        End Sub

        Public Sub New(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure, ByVal YourStrDescription As String, ByVal YourTruck As String, ByVal YourTruckSmartCardNo As String, ByVal YourTruckDriver As String, ByVal YourStrDrivingLicenceNo As String, ByVal YourStrNationalCode As String, ByVal YourStrSmartcardNo As String, ByVal YourIssuedLocation As String, YourMobile As String, YourAddress As String)
            MyBase.New(YourNSS.nEstelamId, YourNSS.TurnId, YourNSS.LoadPermissionDate, YourNSS.LoadPermissionTime, YourNSS.LoadPermissionRegisteringLocation, YourNSS.UserId, YourNSS.LoadPermissionStatusId)
            _StrDescription = YourStrDescription
            _Truck = YourTruck
            _TruckSmartCardNo = YourTruckSmartCardNo
            _TruckDriver = YourTruckDriver
            _StrDrivingLicenceNo = YourStrDrivingLicenceNo
            _StrNationalCode = YourStrNationalCode
            _StrSmartCardNo = YourStrSmartcardNo
            _IssuedLocation = YourIssuedLocation
            _Mobile = YourMobile
            _strAddress = YourAddress
        End Sub

        Public Property StrDescription As String
        Public Property Truck As String
        Public Property TruckSmartCardNo As String
        Public Property TruckDriver As String
        Public Property StrDrivingLicenceNo As String
        Public Property StrNationalCode As String
        Public Property StrSmartCardNo As String
        Public Property IssuedLocation As String
        Public Property Mobile As String
        Public Property strAddress As String

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager
        Private _DateTime As New R2DateTime

        Public Function GetTotalLoadPermissions(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                Return InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select LoadAllocations.LAId from dbtransport.dbo.tbEnterExit as Turns
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId 
                       Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentShamsiDate & "' and LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & " and Turns.strCardno=" & YourNSSTruck.NSSCar.nIdCar & "", 0, DS, New Boolean).GetRecordsCount
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ReportingInformationProviderLoadPermissionsIssuedOrderByPriorityReport(YourAHSGId As Int64) As List(Of KeyValuePair(Of String, String))
            'گزارش مجوزهای صادر شده برای نوبت ها به ترتیب زمان صدور مجوز و اولویت انتخابی
            Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTime)
            Dim InstanceSqlDataBox As New R2CoreSqlDataBOXManager
            Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
            Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
            Dim NSSAHSG = InstanceAnnouncements.GetNSSAnnouncementsubGroup(YourAHSGId)
            Dim NSSSeqT = InstanceSequentialTurns.GetNSSSequentialTurn(NSSAHSG)
            Try
                'Dim SqlString = "Select Top 2000 LoadAllocations.DateShamsi as ActionDateShamsi,LoadAllocations.Time as ActionTime,Turns.OtaghdarTurnNumber,ltrim(rtrim(Replace(Persons.strPersonFullName ,';',' '))) as PersonFullName,Trucks.strCarNo+'-'+Trucks.strCarSerialNo as Truck,LoadAllocations.LAId,LoadAllocations.Priority,Loads.nEstelamID,Loads.nTonaj,
                '                                 Products.strGoodName,LoadTargets.strCityName,LoadingPlaces.LADPlaceTitle as LoadingPlace,DischargingPlaces.LADPlaceTitle as DischargingPlace,Loads.TPTParams,Turns.strExitDate+'-'+strExitTime as LoadPermissionDateTime,TransportCompanies.TCTitle,AnnouncementsubGroups.AHSGTitle,Loads.strDescription,Loads.strAddress,Loads.strBarName  
                '                   from dbtransport.dbo.tbEnterExit as Turns
                '                    Inner Join dbtransport.dbo.tbElam as Loads On Turns.nEstelamID=Loads.nEstelamID
                '                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Loads.AHSGId=AnnouncementsubGroups.AHSGId 
                '                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                '                    Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode
                '                    Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode
                '                    Inner Join dbtransport.dbo.TbPerson as Persons On Turns.nDriverCode=Persons.nIDPerson
                '                    Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver
                '                    Inner Join dbtransport.dbo.TbCar as Trucks On Turns.strCardno=Trucks.nIDCar
                '                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId
                '                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId
                'Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                ' Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                '                   Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentShamsiDate() & "' and Turns.TurnStatus=6 and Turns.LoadPermissionStatus=1 and  LoadAllocations.LAStatusId=2 and AnnouncementsubGroups.AHSGId=" & YourAHSGId & " 
                '                   Order By LoadAllocations.TurnId,LoadAllocations.Priority"
                Dim SqlString = "Select Top 2000 LoadAllocations.DateShamsi as ActionDateShamsi,LoadAllocations.Time as ActionTime,Turns.OtaghdarTurnNumber,ltrim(rtrim(Replace(Persons.strPersonFullName ,';',' '))) as PersonFullName,Trucks.strCarNo+'-'+Trucks.strCarSerialNo as Truck,LoadAllocations.LAId,LoadAllocations.Priority,Loads.nEstelamID,Loads.nTonaj,
                                                 Products.strGoodName,LoadTargets.strCityName,LoadingPlaces.LADPlaceTitle as LoadingPlace,DischargingPlaces.LADPlaceTitle as DischargingPlace,Loads.TPTParams,Turns.strExitDate+'-'+strExitTime as LoadPermissionDateTime,TransportCompanies.TCTitle,AnnouncementsubGroups.AHSGTitle,Loads.strDescription,Loads.strAddress,Loads.strBarName  
                                   from dbtransport.dbo.tbEnterExit as Turns
                                    Inner Join dbtransport.dbo.tbElam as Loads On Turns.nEstelamID=Loads.nEstelamID
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Loads.AHSGId=AnnouncementsubGroups.AHSGId 
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                                    Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode
                                    Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode
                                    Inner Join dbtransport.dbo.TbPerson as Persons On Turns.nDriverCode=Persons.nIDPerson
                                    Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver
                                    Inner Join dbtransport.dbo.TbCar as Trucks On Turns.strCardno=Trucks.nIDCar
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId
                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                                   Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentShamsiDate() & "' and Turns.TurnStatus=6 and Turns.LoadPermissionStatus=1 and  LoadAllocations.LAStatusId=2 and AnnouncementsubGroups.AHSGId=" & YourAHSGId & " 
                                         and Turns.nEnterExitId>(Select Top 1 nEnterExitId from dbtransport.dbo.tbEnterExit as Turns Where TurnStatus=3 and substring(OtaghdarTurnNumber,1,1)='" & NSSSeqT.SequentialTurnKeyWord & "' Order By nEnterExitId Desc)
                                   Order By LoadAllocations.TurnId,LoadAllocations.Priority"

                Dim Ds As DataSet
                InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, SqlString, 120, Ds, New Boolean)
                Dim Lst = New List(Of KeyValuePair(Of String, String))
                Dim StringB As New StringBuilder
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    'Dim ValueHeader = IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("PersonFullName"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("PersonFullName").ToString().Trim()) + "  نوبت : " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").ToString().Trim()) + vbCrLf
                    Dim ValueHeader = "  نوبت : " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").ToString().Trim()) + vbCrLf
                    StringB.Clear()
                    StringB.Append("زمان صدور مجوز :" + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("ActionTime"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("ActionTime").ToString().Trim()) + "-" + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("ActionDateShamsi"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("ActionDateShamsi").ToString().Trim()) + vbCrLf)
                    StringB.Append(IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strGoodName"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strGoodName").ToString().Trim()) + vbCrLf + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strCityName"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strCityName").ToString().Trim()) + vbCrLf)
                    'StringB.Append(IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strCityName"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strCityName").ToString().Trim()) + vbCrLf)
                    'StringB.Append("شرکت حمل و نقل : " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("TCTitle"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("TCTitle").ToString().Trim()) + vbCrLf)
                    StringB.Append("کدبار: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("nEstelamID"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("nEstelamID").ToString().Trim()) + vbCrLf + " تخصیص: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("LAId"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("LAId").ToString().Trim()) + " اولویت : " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("Priority"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("Priority").ToString().Trim()) + vbCrLf)
                    'StringB.Append("توضیحات بار: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strDescription"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strDescription").ToString().Trim()) + " " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strAddress"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strAddress").ToString().Trim()) + " " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("strBarName"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("strBarName").ToString().Trim()) + vbCrLf)
                    StringB.Append("تناژ بار: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("nTonaj"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("nTonaj").ToString().Trim()) + vbCrLf)
                    'StringB.Append("محل بارگیری: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("LoadingPlace"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("LoadingPlace").ToString().Trim()) + vbCrLf)
                    'StringB.Append("محل تخلیه: " + IIf(Object.Equals(Ds.Tables(0).Rows(Loopx).Item("DischargingPlace"), DBNull.Value), String.Empty, Ds.Tables(0).Rows(Loopx).Item("DischargingPlace").ToString().Trim()) + vbCrLf)
                    'Dim TPTParamsTemp = InstanceTransportTariffsParameters.GetTransportTariffsComposit(Ds.Tables(0).Rows(Loopx).Item("TPTParams").ToString().Trim())
                    'If TPTParamsTemp <> String.Empty Then StringB.Append("پارامترهای موثر: " + TPTParamsTemp)
                    Lst.Add(New KeyValuePair(Of String, String)(ValueHeader, StringB.ToString()))
                Next
                Return Lst
            Catch ex As SequentialTurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadPermissionRegistering(YourNSSLoadAllocation As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourCurrentDateTime As R2CoreDateAndTime, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Dim InstanceTruck = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim InstanceTruckDriver = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim IntanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
                Dim InstanceBlackList = New R2CoreParkingSystemInstanceBlackListManager
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Dim InstanceLoadCapacitorLoadOtherThanManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadOtherThanManipulationManager
                'Dim NSSTruck = InstanceTurns.GetNSSTruck(YourNSSLoadAllocation.TurnId)
                'Dim NSSTruckDriver = InstanceTruckDriver.GetNSSTruckDriver(InstanceCars.GetnIdPersonFirst(NSSTruck.NSSCar.nIdCar), False)
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YourNSSLoadAllocation.TurnId)
                Dim InstanceTurnAttendance = New R2CoreTransportationAndLoadNotificationInstanceTurnAttendanceManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager

                'کنترل لیست سیاه
                'Dim BlackList = InstanceBlackList.GetBlackList(New R2StandardCarStructure(NSSTruck.NSSCar.nIdCar, NSSTruck.NSSCar.snCarType, NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo, NSSTruck.NSSCar.nIdCity), R2CoreParkingSystemMClassBlackList.R2CoreParkingSystemBlackListType.ActiveBlackLists)
                'If BlackList.Count <> 0 Then Throw New LoadPermisionRegisteringFailedBecauseBlackListException(BlackList(0).StrDesc)

                'کنترل حاضری راننده باری
                'If InstanceConfigurationOfAnnouncements.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSLoadCapacitorLoad.AHId, 0) = True Then
                '    InstanceTurnAttendance.IsAmountOfTurnPresentsEnough(InstanceAnnouncements.GetNSSAnnouncementHall(YourNSSLoadCapacitorLoad.AHId), YourNSSLoadAllocation.TurnId)
                'End If

                '**********************  Important  14040502 سامانه جامع **************************
                'باید به جای در متد تخصیص بار اینجا ایجاد گردد
                'کنترل تعداد مجوز بارگیری آزاد شده
                'If InstanceLoadPermission.GetTotalLoadPermissions(NSSTruck) = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 5) Then Throw New TruckTotalLoadPermissionReachedException


                'کنترل وضعیت بار در مخزن بار
                If Not IntanceLoadCapacitorLoad.IsLoadCapacitorLoadReadeyforLoadPermissionRegistering(YourNSSLoadCapacitorLoad) Then Throw New LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException

                'کنترل وضعیت نوبت
                If Not InstanceTurns.IsTurnReadeyforLoadPermissionRegistering(NSSTurn) Then Throw New LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException

                'کنترل تعداد مجوز صادر شده
                'Dim Da As New SqlClient.SqlDataAdapter
                'Dim DSAllocate As New DataSet
                'Da.SelectCommand = New SqlClient.SqlCommand("Select LAId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Where TurnId=" & NSSTurn.nEnterExitId & " and LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & "")
                'Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                'If Da.Fill(DSAllocate) <> 0 Then Throw New ExeededNumberofLoadPermisionsWithOneTurnException

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select nEstelamId from DBTransport.dbo.TbElam  with (tablockx) Where nEstelamId=" & YourNSSLoadAllocation.nEstelamId & ""
                CmdSql.ExecuteScalar()
                CmdSql.CommandText = "Select nEnterExitId from DBTransport.dbo.TbEnterExit  with (tablockx) Where nEnterExitId=" & NSSTurn.nEnterExitId & ""
                CmdSql.ExecuteScalar()
                InstanceTurns.LoadPermissionRegistering(NSSTurn, CmdSql)
                InstanceLoadCapacitorLoadOtherThanManipulation.LoadCapacitorLoadReleasing(YourNSSLoadCapacitorLoad, CmdSql, YourCurrentDateTime, YourUserNSS)
                CmdSql.CommandText = "Update DBTransport.dbo.TbEnterExit Set bDelAutomated=1,StrBarnameNo=" & IIf(InstanceSoftwareUsers.GetNSSUser(YourNSSLoadAllocation.UserId).UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany, R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation.TransportCompany, R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation.AnnouncementHall) & ",StrExitDate='" & YourCurrentDateTime.ShamsiDate & "',StrExitTime='" & YourCurrentDateTime.Time & "',nCityCode=" & YourNSSLoadCapacitorLoad.nCityCode & ",nBarCode=" & YourNSSLoadCapacitorLoad.nBarCode & ",bEnterExit=1,nUserIdExit=" & YourUserNSS.UserId & ",nCompCode=" & YourNSSLoadCapacitorLoad.nCompCode & ",nEstelamId=" & YourNSSLoadCapacitorLoad.nEstelamId & ",nCarNum=" & YourNSSLoadCapacitorLoad.nCarNum - 1 & ",LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered & " Where nEnterExitId=" & YourNSSLoadAllocation.TurnId & ""
                CmdSql.ExecuteNonQuery()
                'ارسال تاییدیه صدور مجوز به آنلاین
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTWSCapacitor(TruckId,TWSStatusId,ShamsiDate,Time)
                                      Values(" & NSSTurn.StrCardNo & "," & TWSClassLibrary.NobatsManagement.NobatsStatus.Sodoor & ",'" & YourCurrentDateTime.ShamsiDate & "','" & YourCurrentDateTime.Time & "')"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال تاییدیه صدور مجوز به آنلاین
                'TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.Sodoor(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo, R2CoreTransportationAndLoadNotificationMclassLoadTargetsManagement.GetNSSLoadTarget(YourNSSLoadCapacitorLoad.nCityCode).TargetTravelLength)

                'ارسال اس ام اس آزاد سازی بار
                'Try
                '    Dim InstanceSoftwareUsers_TruckDriver = New R2CoreParkingSystemInstanceSoftwareUsersManager
                '    SendingLoadPermissionSMS(NSSTruck, YourCurrentDateTime, YourNSSLoadCapacitorLoad, InstanceSoftwareUsers_TruckDriver.GetNSSSoftwareUser(NSSTruck.NSSCar))
                'Catch ex As Exception
                'End Try
            Catch ex As Exception When TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException OrElse TypeOf ex Is PresentNotRegisteredInLast30MinuteException OrElse TypeOf ex Is PresentsNotEnoughException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseBlackListException OrElse TypeOf ex Is LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException OrElse TypeOf ex Is LoadCapacitorLoadReleaseTimeNotReachedException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is GetDataException OrElse TypeOf ex Is AnnouncementsubGroupNotFoundException OrElse TypeOf ex Is AnnouncementsubGroupRelationTruckNotExistException OrElse TypeOf ex Is TruckTotalLoadPermissionReachedException OrElse TypeOf ex Is ExeededNumberofLoadPermisionsWithOneTurnException
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

        Private Sub SendingLoadPermissionSMS(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourCurrentDateTime As R2CoreDateAndTime, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourCurrentDateTime.ShamsiDate + " " + YourCurrentDateTime.Time, .Data2 = YourNSSLoadCapacitorLoad.GoodTitle + " - " + YourNSSLoadCapacitorLoad.LoadTargetTitle, .Data3 = YourNSSLoadCapacitorLoad.TransportCompanyTitle}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreTransportationAndLoadNotificationSMSTypes.SendingLoadPermissionIssuedInfSMS, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                'If Not SMSResultAnalyze = String.Empty Then Throw New 
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSLoadPermission(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                            "Select EnterExit.OtaghdarTurnNumber as SequentialTurnNumber,EnterExit.nEstelamID,EnterExit.nEnterExitId,EnterExit.strExitDate,EnterExit.strExitTime,EnterExit.strBarnameNo,EnterExit.nUserIdExit,EnterExit.LoadPermissionStatus,
                                    TransportCompany.TCTitle,Product.strGoodName,City.strCityName,Elam.strDescription,Car.strCarNo+'-'+strCarSerialNo as Truck,Person.strPersonFullName,LoadPermissionStatus.LoadPermissionStatusTitle from dbtransport.dbo.tbEnterExit as EnterExit 
                                           Inner Join dbtransport.dbo.tbElam as Elam On EnterExit.nEstelamID=Elam.nEstelamID
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatus On EnterExit.LoadPermissionStatus=LoadPermissionStatus.LoadPermissionStatusId
                             Where EnterExit.nEnterExitId=" & YourTurnId & " and EnterExit.nEstelamID=" & YournEstelamId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadPermisionNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure(DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("nEnterExitId"), DS.Tables(0).Rows(0).Item("StrExitDate").trim, DS.Tables(0).Rows(0).Item("StrExitTime").trim, DS.Tables(0).Rows(0).Item("strBarnameNo"), DS.Tables(0).Rows(0).Item("nUserIdExit"), DS.Tables(0).Rows(0).Item("LoadPermissionStatus")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("strDescription").trim, DS.Tables(0).Rows(0).Item("Truck").trim, DS.Tables(0).Rows(0).Item("strPersonFullName").trim, DS.Tables(0).Rows(0).Item("LoadPermissionStatusTitle").trim, DS.Tables(0).Rows(0).Item("SequentialTurnNumber").trim)
            Catch exx As LoadPermisionNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadPermissionCancelling(YournEstelamId As Int64, YourTurnId As Int64, YourTurnResuscitationFlag As Boolean, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
                Dim NSSLoadPermission = GetNSSLoadPermission(YournEstelamId, YourTurnId)
                Dim NSSLoadAllocation = InstanceLoadAllocation.GetNSSLoadAllocation(NSSLoadPermission.nEstelamId, NSSLoadPermission.TurnId)
                'کنترل وضعیت مجوز
                If Not NSSLoadPermission.LoadPermissionStatusId = R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered Then Throw New LoadPermissionCancellingNotAllowedBecuaseLoadPermissionStatusException
                'احیاء نوبت یا کنسلی نوبت به دلیل عدم احیاء
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                InstanceTurns.LoadPermissionCancelling(YourTurnId, YourTurnResuscitationFlag)
                InstanceLoadAllocation.ChangeLoadAllocationStatus(NSSLoadAllocation.LAId, R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set LANote='کنسلی مجوز'  Where LAId=" & NSSLoadAllocation.LAId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update DBTransport.dbo.TbEnterExit Set LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Cancelled & " Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'کنسلی مجوز بار ضمن ارسال وضعیت بازگردانی بار یا کنسلی بار
                If NSSLoadAllocation.DateShamsi < _DateTime.GetCurrentShamsiDate Then
                    'بار رسوب شده است و نیازی به عملیات خاصی نیست
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YournEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                ElseIf NSSLoadAllocation.DateShamsi = _DateTime.GetCurrentShamsiDate Then
                    Dim InstanceLoadCapacitorLoadOtherThanManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadOtherThanManipulationManager
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadOtherThanManipulationManagement.LoadCapacitorLoadPermissionCancelling(YournEstelamId, YourTurnResuscitationFlag, YourUserNSS)
                Else
                    Throw New GetDataException
                End If

            Catch exx As LoadPermissionCancellingNotAllowedBecuaseLoadPermissionStatusException
                Throw exx
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetTruckLastLoadWhichPermissioned(YourNSSCarTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Top 1 nEstelamID from dbtransport.dbo.tbEnterExit
                       Where strCardno = " & YourNSSCarTruck.NSSCar.nIdCar & " And isnull(nestelamid,0)<>0 and TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " 
                       Order By nEnterExitId Desc", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckHasNotAnyLoadPermissionException
                Return InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(Convert.ToInt64(DS.Tables(0).Rows(0).Item("nEstelamID")), True)
            Catch ex As TruckHasNotAnyLoadPermissionException
                Throw ex
            Catch ex As LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HaveLoadingPermission(YourLAId As Int64, YourTCompanyId As String) As String
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourLAId)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourTCompanyId)
                Dim SB As New StringBuilder
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select  Top 1 LAId,Persons.strNationalCode as TruckDriverNationalCode,Cars.strBodyNo as TruckSmartCardNo,SourceCities.nOCityCode as LoadSourceId,TargetCities.nOCityCode as LoadTargetId,Products.OstrGoodCode as GoodId,LoadAllocations.DateShamsi,LoadAllocations.DateTimeMilladi,TransportCompanies.TCOrganizationCode from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                               Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID
							   inner join dbtransport.dbo.tbCity as SourceCities on Loads.nBarSource=SourceCities.nCityCode
							   inner join dbtransport.dbo.tbCity as TargetCities on Loads.nCityCode =TargetCities.nCityCode
							   inner join dbtransport.dbo.tbProducts as Products on Loads.nBarcode =Products.strGoodCode 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId  
                               Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId
                               Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                               Inner Join dbtransport.dbo.TbPerson as Persons On Turns.nDriverCode=Persons.nIDPerson  
                             Where LoadAllocations.LAId=" & YourLAId & "", 0, DS, New Boolean).GetRecordsCount() <> 0 Then
                    'If DateDiff(DateInterval.Hour, DateTime.Parse(DS.Tables(0).Rows(0).Item("DateTimeMilladi").ToString), _DateTime.GetCurrentDateTimeMilladi) > 72 Then Throw New LoadingPermissionIdInvalidException
                    If DS.Tables(0).Rows(0).Item("TCOrganizationCode").ToString <> YourTCompanyId Then Throw New LoadingPermissionIdIncorrectException
                    SB.Append(DS.Tables(0).Rows(0).Item("TruckDriverNationalCode").ToString + ";")
                    SB.Append(DS.Tables(0).Rows(0).Item("TruckSmartCardNo").ToString + ";")
                    SB.Append(DS.Tables(0).Rows(0).Item("LoadSourceId").ToString + ";")
                    SB.Append(DS.Tables(0).Rows(0).Item("LoadTargetId").ToString + ";")
                    SB.Append(DS.Tables(0).Rows(0).Item("GoodId").ToString)
                    Return "Ok;" + SB.ToString
                Else
                    Throw New LoadingPermissionNotFoundException
                End If
            Catch ex As SqlInjectionException
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdInvalidException
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdIncorrectException
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionNotFoundException
                Return "NotOk;" + ex.Message
            Catch ex As Exception
                Return "NotOk;خطای سیستم"
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function WriteBillofLadingId(YourLAId As Int64, YourTCompanyId As String, YourBillofLadingId As String) As String
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourLAId)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourTCompanyId)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourBillofLadingId)
                Dim SB As New StringBuilder
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select Top 1 LAId,LoadAllocations.TurnId,LoadAllocations.DateShamsi,LoadAllocations.DateTimeMilladi,TransportCompanies.TCOrganizationCode from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                               Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId  
                               Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId
                               Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                               Inner Join dbtransport.dbo.TbPerson as Persons On Turns.nDriverCode=Persons.nIDPerson  
                             Where LoadAllocations.LAId=" & YourLAId & "", 0, DS, New Boolean).GetRecordsCount() <> 0 Then
                    If DateDiff(DateInterval.Hour, DateTime.Parse(DS.Tables(0).Rows(0).Item("DateTimeMilladi").ToString), _DateTime.GetCurrentDateTimeMilladi) > 72 Then Throw New LoadingPermissionIdInvalidException
                    If DS.Tables(0).Rows(0).Item("TCOrganizationCode").ToString <> YourTCompanyId Then Throw New LoadingPermissionIdIncorrectException
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Update DBTransport.dbo.tbEnterExit Set BillOfLadingNumber='" & YourBillofLadingId & "' Where nEnterExitId=" & Convert.ToInt64(DS.Tables(0).Rows(0).Item("TurnId")) & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return "Ok;"
                Else
                    Throw New LoadingPermissionNotFoundException
                End If
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdInvalidException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdIncorrectException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;خطای سیستم"
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function CancellationBillofLading(YourLAId As Int64, YourTCompanyId As String) As String
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourLAId)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourTCompanyId)
                Dim SB As New StringBuilder
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select Top 1 LAId,LoadAllocations.TurnId,LoadAllocations.DateShamsi,LoadAllocations.DateTimeMilladi,TransportCompanies.TCOrganizationCode from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                               Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId  
                               Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId
                               Inner Join dbtransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                               Inner Join dbtransport.dbo.TbPerson as Persons On Turns.nDriverCode=Persons.nIDPerson  
                             Where LoadAllocations.LAId=" & YourLAId & "", 0, DS, New Boolean).GetRecordsCount() <> 0 Then
                    'If DateDiff(DateInterval.Hour, DateTime.Parse(DS.Tables(0).Rows(0).Item("DateTimeMilladi").ToString), _DateTime.GetCurrentDateTimeMilladi) > 72 Then Throw New LoadingPermissionIdInvalidException
                    If DS.Tables(0).Rows(0).Item("TCOrganizationCode").ToString <> YourTCompanyId Then Throw New LoadingPermissionIdIncorrectException
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Update DBTransport.dbo.tbEnterExit Set BillOfLadingNumber=BillOfLadingNumber + '- D'  Where nEnterExitId=" & Convert.ToInt64(DS.Tables(0).Rows(0).Item("TurnId")) & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return "Ok;"
                Else
                    Throw New LoadingPermissionNotFoundException
                End If
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdInvalidException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionIdIncorrectException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As LoadingPermissionNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Return "NotOk;" + ex.Message
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                'Catch ex As Exception
                '    If CmdSql.Connection.State <> ConnectionState.Closed Then
                '        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                '    End If
                '    Return "NotOk;خطای سیستم"
                '    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadPermissionManagement
        Private Shared _DateTime As New R2DateTime
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Shared Function GetNSSLoadPermission(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select Top 1 EnterExit.OtaghdarTurnNumber as SequentialTurnNumber,EnterExit.nEstelamID,EnterExit.nEnterExitId,EnterExit.strExitDate,EnterExit.strExitTime,EnterExit.strBarnameNo,LoadAllocations.UserId,EnterExit.LoadPermissionStatus,
                                    TransportCompany.TCTitle,Product.strGoodName,City.strCityName,Elam.strDescription,Car.strCarNo+'-'+strCarSerialNo as Truck,Person.strPersonFullName,LoadPermissionStatus.LoadPermissionStatusTitle from dbtransport.dbo.tbEnterExit as EnterExit 
                                           Inner Join dbtransport.dbo.tbElam as Elam On EnterExit.nEstelamID=Elam.nEstelamID
           								   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations on EnterExit.nEnterExitId=LoadAllocations.TurnId and Elam.nEstelamID=LoadAllocations.nEstelamId 
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatus On EnterExit.LoadPermissionStatus=LoadPermissionStatus.LoadPermissionStatusId
                             Where EnterExit.nEnterExitId=" & YourTurnId & " and EnterExit.nEstelamID=" & YournEstelamId & " Order By LoadAllocations.DateTimeMilladi Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadPermisionNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure(DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("nEnterExitId"), DS.Tables(0).Rows(0).Item("StrExitDate").trim, DS.Tables(0).Rows(0).Item("StrExitTime").trim, DS.Tables(0).Rows(0).Item("strBarnameNo"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("LoadPermissionStatus")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("strDescription").trim, DS.Tables(0).Rows(0).Item("Truck").trim, DS.Tables(0).Rows(0).Item("strPersonFullName").trim, DS.Tables(0).Rows(0).Item("LoadPermissionStatusTitle").trim, DS.Tables(0).Rows(0).Item("SequentialTurnNumber").trim)
            Catch exx As LoadPermisionNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoadPermissionStatus(YourLoadPermissionStatusId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure
            Try
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses Where LoadPermissionStatusId=" & YourLoadPermissionStatusId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadPermissionStatusNotFoundException
                Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure
                NSS.LoadPermissionStatusId = YourLoadPermissionStatusId
                NSS.LoadPermissionStatusTitle = DS.Tables(0).Rows(0).Item("LoadPermissionStatustitle").TRIM
                NSS.LoadPermissionStatusColor = DS.Tables(0).Rows(0).Item("LoadPermissionStatusColor").TRIM
                Return NSS
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSPrimaryTurn(YournEstelamId As Int64, YourExcludedTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnStructure
            Try
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 LoadAllocations.TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                          Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId 
                       Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                       Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentShamsiDate() & "' and (LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " or LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed & ") and LoadAllocations.nEstelamId =" & YournEstelamId & " and  LoadAllocations.LAStatusId<>" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled & " and 
                             (Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and (Turns.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & " or Turns.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Cancelled & ") and Turns.nEnterExitId > " & YourExcludedTurnId & " and
                             (Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & " or Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & " or Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & ") and Loads.nCarNum>=1 and
                        	 Turns.nEnterExitId not in (Select TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocationsX where LoadAllocationsX.nEstelamId =" & YournEstelamId & "  and LoadAllocationsX.LAStatusId=5)
                       Order By LoadAllocations.TurnId Asc", 0, DS, New Boolean).GetRecordsCount = 0 Then Throw New PrimaryTurnNotFoundException
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Return InstanceTurns.GetNSSTurn(Convert.ToInt64(DS.Tables(0).Rows(0).Item("TurnId")))
            Catch ex As PrimaryTurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsLoadPermisionRegisteringRunning() As Boolean
            Try

            Catch ex As Exception

            End Try
        End Function

        Public Shared Sub LoadPermissionCancelling(YournEstelamId As Int64, YourTurnId As Int64, YourTurnResuscitationFlag As Boolean, YourLoadResuscitationFlag As Boolean, YourDescription As String, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim NSSLoadPermission = GetNSSLoadPermission(YournEstelamId, YourTurnId)
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                Dim NSSLoadAllocation = R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetNSSLoadAllocation(NSSLoadPermission.nEstelamId, NSSLoadPermission.TurnId)

                'کنترل زمان 
                Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                If InstanceAnnouncementTiming.IsTimingActive(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId) Then
                    Dim Timing = InstanceAnnouncementTiming.GetTiming(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId, _DateTime.GetCurrentTime)
                    If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering Then
                        Throw New LoadPermisionCancellationTimePassedException
                    End If
                End If

                'کنترل وضعیت مجوز
                If Not NSSLoadPermission.LoadPermissionStatusId = R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered Then Throw New LoadPermissionCancellingNotAllowedBecuaseLoadPermissionStatusException
                'احیاء نوبت یا کنسلی نوبت به دلیل عدم احیاء
                R2CoreTransportationAndLoadNotificationMClassTurnsManagement.LoadPermissionCancelling(YourTurnId, YourTurnResuscitationFlag)
                R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.ChangeLoadAllocationStatus(NSSLoadAllocation.LAId, R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set LANote='کنسلی مجوز - '+'" & YourDescription & "'  Where LAId=" & NSSLoadAllocation.LAId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update DBTransport.dbo.TbEnterExit Set LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Cancelled & " Where nEnterExitId=" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'کنسلی مجوز بار ضمن ارسال وضعیت بازگردانی بار یا کنسلی بار
                If NSSLoadAllocation.DateShamsi < _DateTime.GetCurrentShamsiDate Then
                    'بار رسوب شده است و نیازی به عملیات خاصی نیست
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                ElseIf NSSLoadAllocation.DateShamsi = _DateTime.GetCurrentShamsiDate Then
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadOtherThanManipulationManagement.LoadCapacitorLoadPermissionCancelling(YournEstelamId, YourLoadResuscitationFlag, YourUserNSS)
                Else
                    Throw New GetDataException
                End If
            Catch ex As LoadPermisionCancellationTimePassedException
                Throw ex
            Catch ex As UnableResucitationTemporayTurnException
                Throw ex
            Catch exx As LoadPermissionCancellingNotAllowedBecuaseLoadPermissionStatusException
                Throw exx
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetLoadPermissionsIssued(YournEstelamId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Turns.nEstelamID,Turns.nEnterExitId,Turns.strExitDate,Turns.strExitTime,Turns.strBarnameNo,Turns.nUserIdExit,Turns.LoadPermissionStatus,
                               Elam.strDescription,Car.strCarNo+'-'+strCarSerialNo as Truck,Car.StrBodyNo as TruckSmartCardNo,Person.strPersonFullName as TruckDriver,Drivers.strDrivingLicenceNo,Person.strNationalCode,
	                           Drivers.strSmartcardNo,LPILs.LPILTitle as IssuedLocation,Person.strIDNO as Mobile,Person.strAddress as Address
	                    from dbtransport.dbo.tbEnterExit as Turns 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionIssuingLocations as LPILs On Turns.strBarnameNo=LPILs.LPILId
                           Inner Join dbtransport.dbo.tbElam as Elam On Turns.nEstelamID=Elam.nEstelamID
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode
                           Inner Join dbtransport.dbo.TbCar as Car On Turns.strCardno=Car.nIDCar
                           Inner Join dbtransport.dbo.TbPerson as Person On Turns.nDriverCode=Person.nIDPerson
	                       Inner Join dbtransport.dbo.TbDriver as Drivers On Person.nIDPerson=Drivers.nIDDriver
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatus On Turns.LoadPermissionStatus=LoadPermissionStatus.LoadPermissionStatusId
                        Where Elam.nEstelamId=" & YournEstelamId & " and Turns.LoadPermissionStatus=1
                        Order By Turns.StrExitDate,Turns.StrExitTime", 1, DS, New Boolean)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtended_Structure(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamID"), DS.Tables(0).Rows(Loopx).Item("nEnterExitId"), DS.Tables(0).Rows(Loopx).Item("strExitDate").trim, DS.Tables(0).Rows(Loopx).Item("strExitTime").trim, DS.Tables(0).Rows(Loopx).Item("strBarnameNo"), DS.Tables(0).Rows(Loopx).Item("nUserIdExit"), DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatus")), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("Truck").trim, DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo").trim, DS.Tables(0).Rows(Loopx).Item("TruckDriver").trim, DS.Tables(0).Rows(Loopx).Item("strDrivingLicenceNo").trim, DS.Tables(0).Rows(Loopx).Item("strNationalCode").trim, DS.Tables(0).Rows(Loopx).Item("strSmartcardNo").trim, DS.Tables(0).Rows(Loopx).Item("IssuedLocation").trim, DS.Tables(0).Rows(Loopx).Item("Mobile").trim, DS.Tables(0).Rows(Loopx).Item("Address").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadPermissions(YourAHId As Int64, YourAHSGId As Int64, YourLoadPermissionStatusId As Int64, YourLoadPermissionLocation As R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation, Optional YourTransportCompanyId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select EnterExit.OtaghdarTurnNumber as SequentialTurnNumber,EnterExit.nEstelamID,EnterExit.nEnterExitId,EnterExit.strExitDate,EnterExit.strExitTime,EnterExit.strBarnameNo,EnterExit.nUserIdExit,EnterExit.LoadPermissionStatus,
                               TransportCompany.TCTitle,Product.strGoodName,City.strCityName,Elam.strDescription,Car.strCarNo+'-'+strCarSerialNo as Truck,Person.strPersonFullName,LoadPermissionStatus.LoadPermissionStatusTitle
                        from dbtransport.dbo.tbEnterExit as EnterExit 
                               Inner Join dbtransport.dbo.tbElam as Elam On EnterExit.nEstelamID=Elam.nEstelamID
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                               Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                               Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode
                               Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                               Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatus On EnterExit.LoadPermissionStatus=LoadPermissionStatus.LoadPermissionStatusId
                        Where EnterExit.strExitDate='" & _DateTime.GetCurrentShamsiDate() & "' and Elam.AHId=" & YourAHId & " and Elam.AHSGId=" & YourAHSGId & " and EnterExit.LoadPermissionStatus=" & YourLoadPermissionStatusId & " and EnterExit.strBarnameNo=" & YourLoadPermissionLocation & IIf(YourTransportCompanyId = Int64.MinValue, String.Empty, " and TransportCompany.TCId=" & YourTransportCompanyId & "") & " 
                        Order By EnterExit.StrExitDate Desc,EnterExit.StrExitTime Desc", 1, DS, New Boolean)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamID"), DS.Tables(0).Rows(Loopx).Item("nEnterExitId"), DS.Tables(0).Rows(Loopx).Item("strExitDate").trim, DS.Tables(0).Rows(Loopx).Item("strExitTime").trim, DS.Tables(0).Rows(Loopx).Item("strBarnameNo"), DS.Tables(0).Rows(Loopx).Item("nUserIdExit"), DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatus")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("Truck").trim, DS.Tables(0).Rows(Loopx).Item("strPersonFullName").trim, DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle").trim, DS.Tables(0).Rows(Loopx).Item("SequentialTurnNumber").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadPermissionStatuses() As List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses Order By LoadPermissionStatusId", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadPermissionStatusStructure(DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusId"), DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle").trim, DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusColor").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSFinalLoadPermission(YourAHId As Int64, YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                    "Select Top 1 LoadCapacitor.nEstelamID,Turns.nEnterExitId from dbtransport.dbo.tbElam as LoadCapacitor 
                         Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadCapacitor.nEstelamID=Turns.nEstelamID 
                     Where LoadCapacitor.dDateElam='" & _DateTime.GetCurrentShamsiDate & "' and LoadCapacitor.AHId=" & YourAHId & " and LoadCapacitor.AHSGId=" & YourAHSGId & " and Turns.LoadPermissionStatus=1
                     Order By Turns.StrExitDate Desc,Turns.StrExitTime Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Return Nothing
                Return GetNSSLoadPermission(Ds.Tables(0).Rows(0).Item("nEstelamID"), Ds.Tables(0).Rows(0).Item("nEnterExitId"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace LoadPermissionPrinting

        Public Structure R2CoreTransportationAndLoadNotificationLoadPermissionPrintingInf
            Dim LoadAllocationId As Int64
            Dim nEstelamId As Int64
            Dim TurnId As String
            Dim LoadPermissionDate As String
            Dim LoadPermissionTime As String
            Dim TransportCompany As String
            Dim CarType As String
            Dim LoaderType As String
            Dim TruckLP As String
            Dim TruckLPSerial As String
            Dim TruckSmartCardNo As String
            Dim TruckDriver As String
            Dim TruckDriverNationalCode As String
            Dim TruckDriverDrivingLicenseNo As String
            Dim TruckDriverMobileNo As String
            Dim GoodName As String
            Dim TargetCity As String
            Dim SourceCity As String
            Dim TransportPrice As String
            Dim LoadCapacitorLoadDescription As String
            Dim UserName As String
            Dim OtherNote As String
            Dim Tonaj As String
            Dim LoadingAndDischargingPlace As String
        End Structure

        Public Class R2CoreTransportationAndLoadNotificationInstanceLoadPermissionPrintingManager
            Private WithEvents _PrintDocumentPermission As PrintDocument = New PrintDocument()
            Private _PPDS As R2CoreTransportationAndLoadNotificationLoadPermissionPrintingInf

            Public Function GetLoadPermissionPrintingInf(YourLoadAllocationId As Int64, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationLoadPermissionPrintingInf
                Try
                    Dim InstanceSqlDataBox = New R2CoreSqlDataBOXManager
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsParametersManager
                    Dim DS As DataSet
                    InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                          "Select LoadAllocation.LAId,LoadAllocation.nEstelamId,Substring(EnterExit.OtaghdarTurnNumber,7,20) as TurnId,EnterExit.strExitDate,EnterExit.strExitTime
                                  ,TransportCompany.TCTitle,LoaderType.LoaderTypeTitle,CarType.strCarName as CarType,Car.strCarNo as Truck,Car.strCarSerialNo as TruckSerial,Car.strBodyNo,Person.strPersonFullName,Person.strNationalCode
	                              ,Driver.strDrivingLicenceNo,Person.strIDNO as MobileNo,Product.strGoodName,CityTarget.strCityName as TargetCity,CitySource.strCityName as SourceCity,Elam.nTonaj,Elam.strPriceSug,Elam.strDescription,Elam.StrAddress,Elam.strBarName,Elam.TPTParams,SoftwareUser.UserName,CityTarget.nDistance/25 as TravelLength
                                  ,LoadingPlaces.LADPlaceTitle as LoadingPlace,DischargingPlaces.LADPlaceTitle as DischargingPlace
                           from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocation
                                Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                                Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar 
                                Inner Join dbtransport.dbo.tbCarType as CarType On Car.snCarType=CarType.snCarType 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Car.snCarType=LoaderType.LoaderTypeId
								Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                Inner Join dbtransport.dbo.TbDriver as Driver On EnterExit.nDriverCode=Driver.nIDDriver
                                Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                                Inner Join dbtransport.dbo.tbCity as CityTarget On Elam.nCityCode=CityTarget.nCityCode
                                Inner Join dbtransport.dbo.tbCity as CitySource On Elam.nBarSource=CitySource.nCityCode
                                Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUser On LoadAllocation.UserId=SoftwareUser.UserId
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
	  						    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                           Where LoadAllocation.LAId=" & YourLoadAllocationId & "", 1, DS, New Boolean)
                    Dim LoadPermissionPrintingInf As New R2CoreTransportationAndLoadNotificationLoadPermissionPrintingInf()
                    LoadPermissionPrintingInf.LoadAllocationId = DS.Tables(0).Rows(0).Item("LAId")
                    LoadPermissionPrintingInf.nEstelamId = DS.Tables(0).Rows(0).Item("nEstelamId")
                    LoadPermissionPrintingInf.TurnId = DS.Tables(0).Rows(0).Item("TurnId")
                    LoadPermissionPrintingInf.LoadPermissionDate = DS.Tables(0).Rows(0).Item("strExitDate").trim
                    LoadPermissionPrintingInf.LoadPermissionTime = DS.Tables(0).Rows(0).Item("strExitTime").trim
                    LoadPermissionPrintingInf.TransportCompany = DS.Tables(0).Rows(0).Item("TCTitle").trim
                    LoadPermissionPrintingInf.CarType = DS.Tables(0).Rows(0).Item("CarType").trim
                    LoadPermissionPrintingInf.LoaderType = DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim
                    LoadPermissionPrintingInf.TruckLP = DS.Tables(0).Rows(0).Item("Truck").trim
                    LoadPermissionPrintingInf.TruckLPSerial = DS.Tables(0).Rows(0).Item("TruckSerial").trim
                    LoadPermissionPrintingInf.TruckSmartCardNo = DS.Tables(0).Rows(0).Item("strBodyNo").trim
                    LoadPermissionPrintingInf.TruckDriver = DS.Tables(0).Rows(0).Item("strPersonFullName").trim
                    LoadPermissionPrintingInf.TruckDriverNationalCode = DS.Tables(0).Rows(0).Item("strNationalCode").trim
                    LoadPermissionPrintingInf.TruckDriverDrivingLicenseNo = DS.Tables(0).Rows(0).Item("strDrivingLicenceNo").trim
                    LoadPermissionPrintingInf.TruckDriverMobileNo = DS.Tables(0).Rows(0).Item("MobileNo").trim
                    LoadPermissionPrintingInf.GoodName = DS.Tables(0).Rows(0).Item("strGoodName").trim
                    LoadPermissionPrintingInf.TargetCity = DS.Tables(0).Rows(0).Item("TargetCity").trim
                    LoadPermissionPrintingInf.SourceCity = DS.Tables(0).Rows(0).Item("SourceCity").trim
                    LoadPermissionPrintingInf.TransportPrice = DS.Tables(0).Rows(0).Item("strPriceSug").trim
                    LoadPermissionPrintingInf.LoadCapacitorLoadDescription = DS.Tables(0).Rows(0).Item("strDescription").trim & " " & DS.Tables(0).Rows(0).Item("StrAddress").trim & " " & DS.Tables(0).Rows(0).Item("StrBarName").trim
                    Dim TPTParamsTemp = InstanceTransportTariffsParameters.GetTransportTariffsComposit(DS.Tables(0).Rows(0).Item("TPTParams").trim)
                    If TPTParamsTemp <> String.Empty Then LoadPermissionPrintingInf.LoadCapacitorLoadDescription += "" & vbCrLf & " پارامترهای موثر در حمل بار : " + TPTParamsTemp
                    LoadPermissionPrintingInf.UserName = YourNSSUser.UserName.Trim
                    LoadPermissionPrintingInf.OtherNote = Convert.ToString(DS.Tables(0).Rows(0).Item("TravelLength")) + "مدت سفر:"
                    LoadPermissionPrintingInf.Tonaj = Convert.ToString(DS.Tables(0).Rows(0).Item("nTonaj"))
                    LoadPermissionPrintingInf.LoadingAndDischargingPlace = DS.Tables(0).Rows(0).Item("LoadingPlace").trim + " - " + DS.Tables(0).Rows(0).Item("DischargingPlace").trim
                    Return LoadPermissionPrintingInf
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub PrintLoadPermission(YourLoadAllocationId As Int64, YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    _PPDS = GetLoadPermissionPrintingInf(YourLoadAllocationId, YourNSSUser)
                    'چاپ مجوز
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(_PPDS.nEstelamId, True)
                    Dim ComposeSearchString As String = NSSLoadCapacitorLoad.AHSGId.ToString + "="
                    Dim AllAnnouncementHallPrinters As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, NSSLoadCapacitorLoad.AHId, 3), "-")
                    Dim AnnouncementsubGroupPrinter As String = Mid(AllAnnouncementHallPrinters.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnouncementHallPrinters.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length)
                    '_PrintDocumentPermission.PrinterSettings.PrinterName = AnnouncementsubGroupPrinter.Trim
                    _PrintDocumentPermission.PrinterSettings.PrinterName = "HP LaserJet Professional P1102 (redirected 2)"
                    _PrintDocumentPermission.DefaultPageSettings.PaperSize = _PrintDocumentPermission.PrinterSettings.PaperSizes(4)
                    _PrintDocumentPermission.DefaultPageSettings.PaperSource = _PrintDocumentPermission.PrinterSettings.PaperSources(2)
                    _PrintDocumentPermission.Print()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentPermission_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentPermission.BeginPrint
            End Sub

            Private Sub _PrintDocumentPermission_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentPermission.EndPrint
            End Sub

            Private Sub A4Printing(ByVal YourEventArgs As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Int16, ByVal Y As Int16)
                Try
                    Dim myPaperSizeHalf As Integer = _PrintDocumentPermission.PrinterSettings.DefaultPageSettings.PaperSize.Width / 4
                    Dim myStrFontSuperMax As Font = New System.Drawing.Font("Badr", 22.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMax As Font = New System.Drawing.Font("Badr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMin As Font = New System.Drawing.Font("Badr", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myDigFont As Font = New System.Drawing.Font("Alborz Titr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                    'مرحله اول
                    YourEventArgs.Graphics.DrawRectangle(New Pen(Brushes.Black, 5), X, Y, 700, 400)
                    YourEventArgs.Graphics.DrawImage(My.Resources.QRCodeLight, X + 40, Y + 60, 120, 120)
                    YourEventArgs.Graphics.DrawString(" پايانه اميرکبير اصفهان ", myStrFontSuperMax, Brushes.DarkBlue, X + 250, Y)
                    YourEventArgs.Graphics.DrawString(" ((مجوز بارگيری)) ", myStrFontSuperMax, Brushes.DarkBlue, X + 280, Y + 50)
                    YourEventArgs.Graphics.DrawString(" تاريخ صدور: " + _PPDS.LoadPermissionDate, myStrFontMax, Brushes.DarkBlue, X + 30, Y)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y)
                    YourEventArgs.Graphics.DrawString(" شماره مجوز: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y + 30)
                    YourEventArgs.Graphics.DrawString(" ساعت صدور: " + _PPDS.LoadPermissionTime, myStrFontMax, Brushes.DarkBlue, X + 30, Y + 30)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y + 60)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 130)
                    Dim a As Char()
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontMax, Brushes.DarkBlue, X + 120, Y + 170)
                        a = _PPDS.TruckLP.ToCharArray()
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontMax, Brushes.DarkBlue, X + 150, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontMax, Brushes.DarkBlue, X + 160, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontMax, Brushes.DarkBlue, X + 170, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontMax, Brushes.DarkBlue, X + 180, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontMax, Brushes.DarkBlue, X + 190, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(2) + " به شماره پلاک ", myStrFontMax, Brushes.DarkBlue, X + 200, Y + 170)
                    End If
                    YourEventArgs.Graphics.DrawString(" بدين وسيله يک دستگاه " + _PPDS.LoaderType, myStrFontMax, Brushes.DarkBlue, X + 400, Y + 170)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriverDrivingLicenseNo + " دارای گواهينامه به شماره ", myStrFontMax, Brushes.DarkBlue, X + 50, Y + 210)
                    YourEventArgs.Graphics.DrawString(" به رانندگی آقای " + _PPDS.TruckDriver, myStrFontMax, Brushes.DarkBlue, X + 350, Y + 210)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 240)
                    YourEventArgs.Graphics.DrawString(" از " + _PPDS.SourceCity, myStrFontMax, Brushes.DarkBlue, X + 300, Y + 240)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 240)
                    YourEventArgs.Graphics.DrawString(_PPDS.TransportPrice + " با نرخ ", myStrFontMax, Brushes.DarkBlue, X + 450, Y + 270)
                    YourEventArgs.Graphics.DrawString(" ريال با مسئوليت آن شرکت/موسسه معرفی می گردد ", myStrFontMax, Brushes.DarkBlue, X + 100, Y + 270)
                    YourEventArgs.Graphics.DrawString(_PPDS.LoadCapacitorLoadDescription, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 290)
                    'E.Graphics.DrawString("توجه : ", myStrFontMax, Brushes.DarkBlue, X + 650, Y + 310)
                    YourEventArgs.Graphics.DrawString(" توجه: مجوز فوق پس از صدور تعويض نخواهد شد - دريافت نوبت بعدی از پايانه به شرط انجام سفر امکان پذير است", myStrFontMax, Brushes.DarkBlue, X + 10, Y + 340)
                    YourEventArgs.Graphics.DrawString("نام کاربر : " + _PPDS.UserName, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 360)
                    YourEventArgs.Graphics.DrawString(" طول سفر :" + _PPDS.OtherNote + " ساعت", myStrFontMax, Brushes.DarkBlue, X + 300, Y + 360)
                    'مرحله دوم
                    Y = Y + 550
                    YourEventArgs.Graphics.DrawRectangle(New Pen(Brushes.Black, 5), X, Y, 700, 400)
                    YourEventArgs.Graphics.DrawImage(My.Resources.QRCodeLight, X + 40, Y + 60, 120, 120)
                    YourEventArgs.Graphics.DrawString(" پايانه اميرکبير اصفهان ", myStrFontSuperMax, Brushes.DarkBlue, X + 250, Y)
                    YourEventArgs.Graphics.DrawString(" ((مجوز بارگيری)) ", myStrFontSuperMax, Brushes.DarkBlue, X + 280, Y + 50)
                    YourEventArgs.Graphics.DrawString(" تاريخ صدور: " + _PPDS.LoadPermissionDate, myStrFontMax, Brushes.DarkBlue, X + 30, Y)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y)
                    YourEventArgs.Graphics.DrawString(" شماره مجوز: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y + 30)
                    YourEventArgs.Graphics.DrawString(" ساعت صدور: " + _PPDS.LoadPermissionTime, myStrFontMax, Brushes.DarkBlue, X + 30, Y + 30)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 520, Y + 60)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 130)
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontMax, Brushes.DarkBlue, X + 120, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontMax, Brushes.DarkBlue, X + 150, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontMax, Brushes.DarkBlue, X + 160, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontMax, Brushes.DarkBlue, X + 170, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontMax, Brushes.DarkBlue, X + 180, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontMax, Brushes.DarkBlue, X + 190, Y + 170)
                        YourEventArgs.Graphics.DrawString(a(2) + " به شماره پلاک ", myStrFontMax, Brushes.DarkBlue, X + 200, Y + 170)
                    End If
                    YourEventArgs.Graphics.DrawString(" بدين وسيله يک دستگاه " + _PPDS.LoaderType, myStrFontMax, Brushes.DarkBlue, X + 400, Y + 170)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriverDrivingLicenseNo + " دارای گواهينامه به شماره ", myStrFontMax, Brushes.DarkBlue, X + 50, Y + 210)
                    YourEventArgs.Graphics.DrawString(" به رانندگی آقای " + _PPDS.TruckDriver, myStrFontMax, Brushes.DarkBlue, X + 350, Y + 210)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 240)
                    YourEventArgs.Graphics.DrawString(" از " + _PPDS.SourceCity, myStrFontMax, Brushes.DarkBlue, X + 300, Y + 240)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 240)
                    YourEventArgs.Graphics.DrawString(_PPDS.TransportPrice + " با نرخ ", myStrFontMax, Brushes.DarkBlue, X + 450, Y + 270)
                    YourEventArgs.Graphics.DrawString(" ريال با مسئوليت آن شرکت/موسسه معرفی می گردد ", myStrFontMax, Brushes.DarkBlue, X + 100, Y + 270)
                    YourEventArgs.Graphics.DrawString(_PPDS.LoadCapacitorLoadDescription, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 290)
                    'E.Graphics.DrawString("توجه : ", myStrFontMax, Brushes.DarkBlue, X + 650, Y + 310)
                    YourEventArgs.Graphics.DrawString(" توجه: مجوز فوق پس از صدور تعويض نخواهد شد - دريافت نوبت بعدی از پايانه به شرط انجام سفر امکان پذير است", myStrFontMax, Brushes.DarkBlue, X + 10, Y + 340)
                    YourEventArgs.Graphics.DrawString("نام کاربر : " + _PPDS.UserName, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 360)
                    YourEventArgs.Graphics.DrawString(" طول سفر :" + _PPDS.OtherNote + " ساعت", myStrFontMax, Brushes.DarkBlue, X + 300, Y + 360)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub A5Printing(ByVal YourEventArgs As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Int16, ByVal Y As Int16)
                Try
                    Dim myPaperSizeHalf As Integer = _PrintDocumentPermission.PrinterSettings.DefaultPageSettings.PaperSize.Width / 4
                    Dim myStrFontSuperMax As Font = New System.Drawing.Font("Badr", 18, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontUpperMax As Font = New System.Drawing.Font("Badr", 16, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMax As Font = New System.Drawing.Font("Badr", 14, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMin As Font = New System.Drawing.Font("Badr", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myDigFont As Font = New System.Drawing.Font("Alborz Titr", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                    'مرحله اول
                    YourEventArgs.Graphics.DrawRectangle(New Pen(Brushes.Black, 1), X, Y, 520, 350)
                    YourEventArgs.Graphics.DrawImage(My.Resources.QRCodeLight, X + 30, Y + 60, 75, 75)
                    YourEventArgs.Graphics.DrawString(" پايانه اميرکبير اصفهان ", myStrFontSuperMax, Brushes.DarkBlue, X + 180, Y)
                    YourEventArgs.Graphics.DrawString(" ((مجوز بارگيری)) ", myStrFontSuperMax, Brushes.DarkBlue, X + 195, Y + 30)
                    YourEventArgs.Graphics.DrawString(" تاريخ صدور: " + _PPDS.LoadPermissionDate, myStrFontMin, Brushes.DarkBlue, X + 15, Y + 20)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 20)
                    YourEventArgs.Graphics.DrawString(" شماره مجوز: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 40)
                    YourEventArgs.Graphics.DrawString(" ساعت صدور: " + _PPDS.LoadPermissionTime, myStrFontMin, Brushes.DarkBlue, X + 15, Y + 40)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 60)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontSuperMax, Brushes.DarkBlue, X + 160, Y + 80)
                    Dim a As Char()
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        a = _PPDS.TruckLP.ToCharArray()
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontSuperMax, Brushes.DarkBlue, X + 20, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontSuperMax, Brushes.DarkBlue, X + 60, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontSuperMax, Brushes.DarkBlue, X + 70, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontSuperMax, Brushes.DarkBlue, X + 80, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontSuperMax, Brushes.DarkBlue, X + 90, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontSuperMax, Brushes.DarkBlue, X + 100, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(2) + " پلاک ", myStrFontSuperMax, Brushes.DarkBlue, X + 110, Y + 120)
                    End If
                    YourEventArgs.Graphics.DrawString(" بدين وسيله يک دستگاه " + _PPDS.LoaderType, myStrFontMin, Brushes.DarkBlue, X + 300, Y + 120)
                    YourEventArgs.Graphics.DrawString(" به رانندگی آقای " + _PPDS.TruckDriver, myStrFontSuperMax, Brushes.DarkBlue, X + 150, Y + 140)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriverDrivingLicenseNo + " دارای گواهينامه به شماره ", myStrFontMin, Brushes.DarkBlue, X + 20, Y + 170)
                    YourEventArgs.Graphics.DrawString(" از " + _PPDS.SourceCity, myStrFontMax, Brushes.DarkBlue, X + 250, Y + 180)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 20, Y + 180)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 330, Y + 210)
                    YourEventArgs.Graphics.DrawString(_PPDS.TransportPrice + " با نرخ ", myStrFontMin, Brushes.DarkBlue, X + 350, Y + 240)
                    YourEventArgs.Graphics.DrawString("            ", myStrFontMin, Brushes.DarkBlue, X + 350, Y + 240)

                    YourEventArgs.Graphics.DrawString(" ريال با مسئوليت آن شرکت/موسسه معرفی می گردد ", myStrFontMin, Brushes.DarkBlue, X + 125, Y + 240)
                    YourEventArgs.Graphics.DrawString(_PPDS.LoadCapacitorLoadDescription, myStrFontMax, Brushes.DarkBlue, X + 20, Y + 260)
                    YourEventArgs.Graphics.DrawString("نام کاربر : " + _PPDS.UserName, myStrFontMin, Brushes.DarkBlue, X + 50, Y + 280)
                    YourEventArgs.Graphics.DrawString(" طول سفر :" + _PPDS.OtherNote + " ساعت", myStrFontMin, Brushes.DarkBlue, X + 165, Y + 280)
                    'مرحله دوم
                    Y = Y + 400
                    YourEventArgs.Graphics.DrawRectangle(New Pen(Brushes.Black, 1), X, Y, 520, 350)
                    YourEventArgs.Graphics.DrawImage(My.Resources.QRCodeLight, X + 30, Y + 60, 75, 75)
                    YourEventArgs.Graphics.DrawString(" پايانه اميرکبير اصفهان ", myStrFontSuperMax, Brushes.DarkBlue, X + 180, Y)
                    YourEventArgs.Graphics.DrawString(" ((مجوز بارگيری)) ", myStrFontSuperMax, Brushes.DarkBlue, X + 195, Y + 30)
                    YourEventArgs.Graphics.DrawString(" تاريخ صدور: " + _PPDS.LoadPermissionDate, myStrFontMin, Brushes.DarkBlue, X + 15, Y + 20)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 20)
                    YourEventArgs.Graphics.DrawString(" شماره مجوز: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 40)
                    YourEventArgs.Graphics.DrawString(" ساعت صدور: " + _PPDS.LoadPermissionTime, myStrFontMin, Brushes.DarkBlue, X + 15, Y + 40)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 360, Y + 60)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontSuperMax, Brushes.DarkBlue, X + 160, Y + 80)
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        a = _PPDS.TruckLP.ToCharArray()
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontSuperMax, Brushes.DarkBlue, X + 20, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontSuperMax, Brushes.DarkBlue, X + 60, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontSuperMax, Brushes.DarkBlue, X + 70, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontSuperMax, Brushes.DarkBlue, X + 80, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontSuperMax, Brushes.DarkBlue, X + 90, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontSuperMax, Brushes.DarkBlue, X + 100, Y + 120)
                        YourEventArgs.Graphics.DrawString(a(2) + " پلاک ", myStrFontSuperMax, Brushes.DarkBlue, X + 110, Y + 120)
                    End If
                    YourEventArgs.Graphics.DrawString(" بدين وسيله يک دستگاه " + _PPDS.LoaderType, myStrFontMin, Brushes.DarkBlue, X + 300, Y + 120)
                    YourEventArgs.Graphics.DrawString(" به رانندگی آقای " + _PPDS.TruckDriver, myStrFontSuperMax, Brushes.DarkBlue, X + 150, Y + 140)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriverDrivingLicenseNo + " دارای گواهينامه به شماره ", myStrFontMin, Brushes.DarkBlue, X + 20, Y + 170)
                    YourEventArgs.Graphics.DrawString(" از " + _PPDS.SourceCity, myStrFontMax, Brushes.DarkBlue, X + 250, Y + 180)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 20, Y + 180)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 330, Y + 210)
                    YourEventArgs.Graphics.DrawString(_PPDS.TransportPrice + " با نرخ ", myStrFontMin, Brushes.DarkBlue, X + 350, Y + 240)
                    YourEventArgs.Graphics.DrawString("            ", myStrFontMin, Brushes.DarkBlue, X + 350, Y + 240)
                    YourEventArgs.Graphics.DrawString(" ريال با مسئوليت آن شرکت/موسسه معرفی می گردد ", myStrFontMin, Brushes.DarkBlue, X + 125, Y + 240)
                    YourEventArgs.Graphics.DrawString(_PPDS.LoadCapacitorLoadDescription, myStrFontMax, Brushes.DarkBlue, X + 20, Y + 260)
                    YourEventArgs.Graphics.DrawString("نام کاربر : " + _PPDS.UserName, myStrFontMin, Brushes.DarkBlue, X + 50, Y + 280)
                    YourEventArgs.Graphics.DrawString(" طول سفر :" + _PPDS.OtherNote + " ساعت", myStrFontMin, Brushes.DarkBlue, X + 165, Y + 280)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentPermission_PrintPage_Printing(ByVal X As Int16, ByVal Y As Int16, ByVal E As System.Drawing.Printing.PrintPageEventArgs)
                Try
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim ConfiguredPageType As String = InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(_PPDS.nEstelamId, True).AHId, 0)
                    If [Enum].Parse(GetType(System.Drawing.Printing.PaperKind), ConfiguredPageType) = System.Drawing.Printing.PaperKind.A5 Then
                        A5Printing(E, 10, 20)
                    ElseIf [Enum].Parse(GetType(System.Drawing.Printing.PaperKind), ConfiguredPageType) = System.Drawing.Printing.PaperKind.A4 Then
                        A4Printing(E, 50, 80)
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentPermission_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _PrintDocumentPermission.PrintPage
                Try
                    _PrintDocumentPermission_PrintPage_Printing(0, 0, e)
                Catch ex As Exception
                    MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub


        End Class

    End Namespace

    Namespace Exceptions

        Public Class ExeededNumberofLoadPermisionsWithOneTurnException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "صدور بیش از یک مجوز با یک نوبت امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadingPermissionNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز مورد نظر در سامانه آتیس یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadingPermissionIdInvalidException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز با شماره مورد نظر در سامانه آتیس اعتبار ندارد"
                End Get
            End Property
        End Class

        Public Class LoadingPermissionIdIncorrectException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره مجوز سامانه آتیس نادرست است"
                End Get
            End Property
        End Class

        Public Class LoadPermisionRegisteringFailedBecauseBlackListException
            Inherits ApplicationException
            Private _Message As String = String.Empty
            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return _Message
                End Get
            End Property
        End Class

        Public Class LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان صدور مجوز به دلیل وضعیت نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان صدور مجوز به دلیل وضعیت بار در مخزن بار وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadPermisionNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز بارگیری با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadPermissionStatusNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت مجوزبارگیری با کدشاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadPermissionCancellingNotAllowedBecuaseLoadPermissionStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت فعلی مجوز بارگیری مانع از اجرای این فرآیند شد"
                End Get
            End Property
        End Class

        Public Class TruckHasNotAnyLoadPermissionException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "برای ناوگان تاکنون هیچگونه مجوز بار ثبت نشده است"
                End Get
            End Property
        End Class

        Public Class PrimaryTurnNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پیش نوبت معتبری یافت نشد " + vbCrLf + "با توجه به شرایط تخصیص بار ، وضعیت بار مورد نظر و وضعیت نوبت ها ، پیش نوبت معتبر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TruckTotalLoadPermissionReachedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تعداد مجوز صادرشده امروز برای ناوگان به حداکثر رسیده است "
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class LoadPermisionCancellationTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "با توجه به زمانبندی اعلام بار امکان کنسلی مجوز بار در حال حاضر وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadPermission
        Public Property LoadId As Int64
        Public Property TurnId As Int64
        Public Property LoadPermissionDate As String
        Public Property LoadPermissionTime As String
        Public Property LoadPermissionRegisteringLocation As String
        Public Property UserId As Int64
        Public Property LoadPermissionStatusId As Int64

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadPermissionManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetTruckLastLoadWhichPermissioned(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoad
            Try
                Dim DS As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                      "Select Top 1 nEstelamID from dbtransport.dbo.tbEnterExit
                       Where strCardno = " & YourTruckId & " And isnull(nestelamid, 0) <> 0 And TurnStatus = " & TurnStatuses.UsedLoadPermissionRegistered & " 
                       Order By nEnterExitId Desc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckHasNotAnyLoadPermissionException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 nEstelamID from dbtransport.dbo.tbEnterExit
                       Where strCardno = " & YourTruckId & " And isnull(nestelamid,0)<>0 and TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " 
                       Order By nEnterExitId Desc", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TruckHasNotAnyLoadPermissionException
                End If
                Return InstanceLoad.GetLoad(Convert.ToInt64(DS.Tables(0).Rows(0).Item("nEstelamID")), YourImmediately)
            Catch ex As TruckHasNotAnyLoadPermissionException
                Throw ex
            Catch ex As LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckLastLoadPermission(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoadPermission
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select Top 1  Permission.nEstelamID as LoadId,Permission.nEnterExitId as TurnId,Permission.strExitDate as LoadPermissionDate,Permission.strExitTime as LoadPermissionTime ,
                                       Permission.strBarnameNo as LoadPermissionRegisteringLocation ,Permission.nUserIdExit as UserId,Permission.LoadPermissionStatus as LoadPermissionStatusId
                         from DBTransport.dbo.tbEnterExit as Permission
                         Where Permission.strCardno=" & YourTruckId & " and Permission.TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " and  and Permission.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered & " 
                         Order By nEnterExitId Desc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckHasNotAnyLoadPermissionException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Top 1  Permission.nEstelamID as LoadId,Permission.nEnterExitId as TurnId,Permission.strExitDate as LoadPermissionDate,Permission.strExitTime as LoadPermissionTime ,
                                       Permission.strBarnameNo as LoadPermissionRegisteringLocation ,Permission.nUserIdExit as UserId,Permission.LoadPermissionStatus as LoadPermissionStatusId
                         from DBTransport.dbo.tbEnterExit as Permission
                         Where Permission.strCardno=" & YourTruckId & " and Permission.TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " and  and Permission.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered & " 
                         Order By nEnterExitId Desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New TruckHasNotAnyLoadPermissionException
                End If
                Return New R2CoreTransportationAndLoadNotificationLoadPermission With {.LoadId = DS.Tables(0).Rows(0).Item("LoadId"), .TurnId = DS.Tables(0).Rows(0).Item("TurnId"), .UserId = DS.Tables(0).Rows(0).Item("UserId"), .LoadPermissionDate = DS.Tables(0).Rows(0).Item("LoadPermissionDate"), .LoadPermissionTime = DS.Tables(0).Rows(0).Item("LoadPermissionTime"), .LoadPermissionRegisteringLocation = DS.Tables(0).Rows(0).Item("LoadPermissionRegisteringLocation"), .LoadPermissionStatusId = DS.Tables(0).Rows(0).Item("LoadPermissionStatusId")}
            Catch ex As TruckHasNotAnyLoadPermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadPermissions(YourLoadId As Int64) As String
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select Loads.nEstelamID as LoadId,Products.strGoodName as GoodTitle,LoadSourceCities.StrCityName as LoadSourceCity,LoadTargetCities.StrCityName as LoadTargetCity,
                          Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGTitle,Companies.strCompName as TransportCompany,Loads.strBarName as Recipient,
	                      Loads.strAddress as Address,Loads.strDescription as Description,LoadRegisteringSoftwareUsers.UserName as LoadRegisteringUser,LoadAllocationSoftwareUsers.UserName as LoadAllocationUser,
                          LoadAllocations.LAId as LoadAllocationId,Cars.strCarNo+' - '+Cars.strCarSerialNo as LicensePlate,Cars.strBodyNo as SmartCardNo,Drivers.strPersonFullName as TruckDriver,Drivers.strNationalCode as NationalCode,
	                      Drivers.strIDNO as MobileNumber,LoadAllocations.DateShamsi as ShamsiDate,LoadAllocations.Time as Time,Turns.OtaghdarTurnNumber as SequentialTurn,LoadAllocations.LANote as Note,
	                      LoadAllocationStatuses.LoadAllocationStatusTitle ,Loads.TPTParamsJoint 
                   from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
                       Inner Join DBTransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                       Inner Join DBTransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId 
                       Inner Join DBTransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                       Inner Join DBTransport.dbo.tbCity as LoadSourceCities On Loads.nBarSource=LoadSourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity as LoadTargetCities On Loads.nCityCode=LoadTargetCities.nCityCode 
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On Loads.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                       Inner Join DBTransport.dbo.tbCompany as Companies On Loads.nCompCode=Companies.nCompCode 
                       Inner Join R2Primary.dbo.TblSoftwareUsers as LoadRegisteringSoftwareUsers On Loads.nUserID=LoadRegisteringSoftwareUsers.UserId 
                       Inner Join R2Primary.dbo.TblSoftwareUsers as LoadAllocationSoftwareUsers On LoadAllocations.UserId=LoadAllocationSoftwareUsers.UserId 
                       Inner Join DBTransport.dbo.TbCar as Cars On Turns.strCardno=Cars.nIDCar 
                       Inner Join DBTransport.dbo.TbPerson as Drivers On Turns.nDriverCode=Drivers.nIDPerson 
                   Where (LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & " or LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled & ") and LoadAllocations.nEstelamId=" & YourLoadId & "
                   Order By LoadAllocations.LAId Desc for JSON PATH", 180, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadPermissions(YourAnnouncementGroupId As Int64, YourAnnouncementSubGroupId As Int64) As String
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select Loads.nEstelamID as LoadId,Loads.nTonaj as Tonaj,Turns.OtaghdarTurnNumber as SequentialTurn,LoadAllocations.LAId as LoadAllocationId,LoadAllocations.Priority,LoadAllocations.DateShamsi as ShamsiDate,LoadAllocations.Time,
                          Products.strGoodName as GoodTitle,LoadSourceCities.StrCityName as LoadSourceCity,LoadTargetCities.StrCityName as LoadTargetCity
                   from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                       Inner Join DBTransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                       Inner Join DBTransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId 
                       Inner Join DBTransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                       Inner Join DBTransport.dbo.tbCity as LoadSourceCities On Loads.nBarSource=LoadSourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity as LoadTargetCities On Loads.nCityCode=LoadTargetCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCompany as Companies On Loads.nCompCode=Companies.nCompCode 
                   Where Loads.AHId=" & YourAnnouncementGroupId & " and Loads.AHSGId=" & YourAnnouncementSubGroupId & " and LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & " and  
                         LoadAllocations.DateShamsi=R2Primary.DBO.BPTCOGregorianToPersian(GETDATE())
                   Order By LoadAllocations.TurnId  for JSON PATH", 180, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadPersmissionCancelling(YourLoadAllocationId As Int64, YourDescription As String, YourTurnResusitution As Boolean, YourLoadResusitution As Boolean, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationAnnouncementTimingManager(_DateTimeService)
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService)
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)

                'کنترل زمان 
                Dim Load = InstanceLoad.GetLoadSimpleModel(InstanceLoadAllocation.GetLoadIdfromLoadAllocationId(YourLoadAllocationId), True)
                Dim TurnId = InstanceLoadAllocation.GetTurnIdfromLoadAllocationId(YourLoadAllocationId)
                Dim Timing = InstanceAnnouncementTiming.GetTiming(Load.AnnouncementGroupId, Load.AnnouncementSubGroupId, _DateTimeService.GetCurrentTime)
                If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering Then
                    Throw New LoadPermisionCancellationTimePassedException
                End If

                'تراکنش کنسلی مجوز
                Dim Param As SqlClient.SqlParameter
                Param = New SqlParameter("@YourUserId", SqlDbType.BigInt) : Param.Value = YourSoftwareUser.UserId
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourLAId", SqlDbType.BigInt) : Param.Value = YourLoadAllocationId
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourCancellationDescription", SqlDbType.NVarChar) : Param.Value = YourDescription
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTurnResuscitationFlag", SqlDbType.Bit) : Param.Value = YourTurnResusitution
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourLoadResuscitationFlag", SqlDbType.Bit) : Param.Value = YourLoadResusitution
                CmdSql.Parameters.Add(Param)
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadPermissionCancelling"
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()

                'PubSubMessaging
                Dim _Subscriber = RedisConnectorHelper.Connection.GetSubscriber()
                _Subscriber.Publish(R2CoreTransportationAndLoadNotificationPubSubChannels.TurnInfo, JsonConvert.SerializeObject(TurnId))

            Catch ex As RedisException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As LoadAllocationsNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As LoadNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As LoadPermisionCancellationTimePassedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class


End Namespace
