Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
Imports R2Core.BaseStandardClass
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
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions


Namespace TruckLoaderTypes

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationTruckLoaderTypeManagement
        Public Shared Function GetTonajMax(YourTruckLoaderName As String) As Int64
            Try
                Dim DS As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 nTonaj from dbtransport.dbo.tbCarType Where strCarName='" & YourTruckLoaderName.Trim() & "'", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return DS.Tables(0).Rows(0).Item("nTonaj")
                Else
                    Throw New TruckLoaderTypeNotFoundException
                End If
            Catch ex As TruckLoaderTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Namespace Exceptions
        Public Class TruckLoaderTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

End Namespace

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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                Return InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select LoadAllocations.LAId from dbtransport.dbo.tbEnterExit as Turns
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId 
                       Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "' and LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & " and Turns.strCardno=" & YourNSSTruck.NSSCar.nIdCar & "", 0, DS, New Boolean).GetRecordsCount
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function ReportingInformationProviderLoadPermissionsIssuedOrderByPriorityReport(YourAHSGId As Int64) As List(Of KeyValuePair(Of String, String))
            'گزارش مجوزهای صادر شده برای نوبت ها به ترتیب زمان صدور مجوز و اولویت انتخابی
            Dim InstanceTransportTarrifsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTarrifsParametersManager
            Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
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
                '                   Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull() & "' and Turns.TurnStatus=6 and Turns.LoadPermissionStatus=1 and  LoadAllocations.LAStatusId=2 and AnnouncementsubGroups.AHSGId=" & YourAHSGId & " 
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
                                   Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull() & "' and Turns.TurnStatus=6 and Turns.LoadPermissionStatus=1 and  LoadAllocations.LAStatusId=2 and AnnouncementsubGroups.AHSGId=" & YourAHSGId & " 
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
                    'Dim TPTParamsTemp = InstanceTransportTarrifsParameters.GetTransportTarrifsComposit(Ds.Tables(0).Rows(Loopx).Item("TPTParams").ToString().Trim())
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

        Public Sub LoadPermissionRegistering(YourNSSLoadAllocation As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourCurrentDateTime As R2StandardDateAndTimeStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
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
                CmdSql.CommandText = "Update DBTransport.dbo.TbEnterExit Set bDelAutomated=1,StrBarnameNo=" & IIf(InstanceSoftwareUsers.GetNSSUser(YourNSSLoadAllocation.UserId).UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany, R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation.TransportCompany, R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation.AnnouncementHall) & ",StrExitDate='" & YourCurrentDateTime.DateShamsiFull & "',StrExitTime='" & YourCurrentDateTime.Time & "',nCityCode=" & YourNSSLoadCapacitorLoad.nCityCode & ",nBarCode=" & YourNSSLoadCapacitorLoad.nBarCode & ",bEnterExit=1,nUserIdExit=" & YourUserNSS.UserId & ",nCompCode=" & YourNSSLoadCapacitorLoad.nCompCode & ",nEstelamId=" & YourNSSLoadCapacitorLoad.nEstelamId & ",nCarNum=" & YourNSSLoadCapacitorLoad.nCarNum - 1 & ",LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Registered & " Where nEnterExitId=" & YourNSSLoadAllocation.TurnId & ""
                CmdSql.ExecuteNonQuery()
                'ارسال تاییدیه صدور مجوز به آنلاین
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTWSCapacitor(TruckId,TWSStatusId,ShamsiDate,Time)
                                      Values(" & NSSTurn.StrCardNo & "," & TWSClassLibrary.NobatsManagement.NobatsStatus.Sodoor & ",'" & YourCurrentDateTime.DateShamsiFull & "','" & YourCurrentDateTime.Time & "')"
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

        Private Sub SendingLoadPermissionSMS(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourCurrentDateTime As R2StandardDateAndTimeStructure, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourCurrentDateTime.DateShamsiFull + " " + YourCurrentDateTime.Time, .Data2 = YourNSSLoadCapacitorLoad.GoodTitle + " - " + YourNSSLoadCapacitorLoad.LoadTargetTitle, .Data3 = YourNSSLoadCapacitorLoad.TransportCompanyTitle}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreTransportationAndLoadNotificationSMSTypes.SendingLoadPermissionIssuedInfSMS, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                'If Not SMSResultAnalyze = String.Empty Then Throw New 
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSLoadPermission(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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
                If NSSLoadAllocation.DateShamsi < _DateTime.GetCurrentDateShamsiFull Then
                    'بار رسوب شده است و نیازی به عملیات خاصی نیست
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YournEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                ElseIf NSSLoadAllocation.DateShamsi = _DateTime.GetCurrentDateShamsiFull Then
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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

        Public Shared Function GetNSSLoadPermission(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadPermissionExtendedStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses Where LoadPermissionStatusId=" & YourLoadPermissionStatusId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadPermissionStatusNotFoundException
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 LoadAllocations.TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                          Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId 
                       Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                       Where LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull() & "' and (LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " or LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed & ") and LoadAllocations.nEstelamId =" & YournEstelamId & " and  LoadAllocations.LAStatusId<>" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled & " and 
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
                If NSSLoadAllocation.DateShamsi < _DateTime.GetCurrentDateShamsiFull Then
                    'بار رسوب شده است و نیازی به عملیات خاصی نیست
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                ElseIf NSSLoadAllocation.DateShamsi = _DateTime.GetCurrentDateShamsiFull Then
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
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
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
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
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
                        Where EnterExit.strExitDate='" & _DateTime.GetCurrentDateShamsiFull() & "' and Elam.AHId=" & YourAHId & " and Elam.AHSGId=" & YourAHSGId & " and EnterExit.LoadPermissionStatus=" & YourLoadPermissionStatusId & " and EnterExit.strBarnameNo=" & YourLoadPermissionLocation & IIf(YourTransportCompanyId = Int64.MinValue, String.Empty, " and TransportCompany.TCId=" & YourTransportCompanyId & "") & " 
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
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses Order By LoadPermissionStatusId", 3600, DS, New Boolean)
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                    "Select Top 1 LoadCapacitor.nEstelamID,Turns.nEnterExitId from dbtransport.dbo.tbElam as LoadCapacitor 
                         Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadCapacitor.nEstelamID=Turns.nEstelamID 
                     Where LoadCapacitor.dDateElam='" & _DateTime.GetCurrentDateShamsiFull & "' and LoadCapacitor.AHId=" & YourAHId & " and LoadCapacitor.AHSGId=" & YourAHSGId & " and Turns.LoadPermissionStatus=1
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
                    Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceTransportTarrifsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTarrifsParametersManager
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
                    Dim TPTParamsTemp = InstanceTransportTarrifsParameters.GetTransportTarrifsComposit(DS.Tables(0).Rows(0).Item("TPTParams").trim)
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

        Public Class LoadPermisionCancellationTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "با توجه به زمانبندی اعلام بار امکان کنسلی مجوز بار در حال حاضر وجود ندارد"
                End Get
            End Property
        End Class

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
        Public Function GetTruckLastLoadWhichPermissioned(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoad
            Try
                Dim DS As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
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

    End Class


End Namespace

Namespace LoadSedimentation

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupSedimentingConfigurationStructure

        Public Sub New()
            MyBase.New()
            _SedimentingStartTime = Nothing
            _SedimentingEndTime = Nothing
            _SedimentingDelationMinutes = Long.MinValue
            _SedimentingActive = False
        End Sub

        Public Sub New(ByVal YourSedimentingStartTime As R2StandardDateAndTimeStructure, YourSedimentingEndTime As R2StandardDateAndTimeStructure, YourSedimentingDelationMinutes As Int64, YourSedimentingActive As Boolean)
            _SedimentingStartTime = YourSedimentingStartTime
            _SedimentingEndTime = YourSedimentingEndTime
            _SedimentingDelationMinutes = YourSedimentingDelationMinutes
            _SedimentingActive = YourSedimentingActive
        End Sub


        Public Property SedimentingStartTime As R2StandardDateAndTimeStructure
        Public Property SedimentingEndTime As R2StandardDateAndTimeStructure
        Public Property SedimentingDelationMinutes As Int64
        Public Property SedimentingActive As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManager
        Private Shared _DateTime As New R2DateTime

        Public Sub SedimentingProcess()
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim Lst = InstanceAnnouncements.GetAnnouncementsAnnouncementsubGroupsJOINT()
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AHId, AHSGId As Int64
                For Each C In Lst
                    Try
                        AHId = C.NSSAnnounementHall.AHId
                        AHSGId = C.NSSAnnouncementsubGroup.AHSGId
                        'کنترل فعال بودن فرآیند رسوب برای زیرگروه مورد نظر
                        If Not IsActiveSedimenting(AHId, AHSGId) Then Continue For
                        'کنترل تایمینگ - آیا رسوب بار برای زیرگروه مورد نظر فرارسیده است
                        If InstanceAnnouncementTiming.IsTimingActive(AHId, AHSGId) Then
                            If Not ((InstanceAnnouncementTiming.GetTiming(AHId, AHSGId, CurrentTime) = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering) Or (InstanceAnnouncementTiming.GetTiming(AHId, AHSGId, CurrentTime) = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InSedimenting)) Then
                                Continue For
                            End If
                        Else
                            Continue For
                        End If
                        'کنترل این که ممکن است هنوز آزاد سازی بار تمام نشده باشد لذا رسوب بار نباید انجام گیرد حتی اگر زمان رسوب بار فرارسیده باشد
                        Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
                        If InstanceLoadAllocation.GetLoadAllocationsforLoadPermissionRegistering(AHId, AHSGId).Count <> 0 Then Continue For
                        'لیست کامل باری که باید رسوب گردد
                        'موجودی داشته باشد و حذف کنسل و رسوب شده نباشد بار فردا هم نباشد 
                        Dim InstanceLoadCapacitor = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                        Dim LstLoads = InstanceLoadCapacitor.GetLoadCapacitorLoads(AHId, AHSGId, AnnouncementHallAnnounceTimeTypes.LastAnnounceLoads, False, True, R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nEstelamId)
                        If IsNothing(LstLoads) Or LstLoads.Count = 0 Then Continue For
                        'رسوب بار
                        Dim LastAnnounceTime = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(AHId, AHSGId).Time
                        CmdSql.Connection.Open()
                        CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set bFlag=1,LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " 
                                              Where (dDateElam='" & _DateTime.GetCurrentDateShamsiFull & "') and (LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & " or LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & ") and AHId=" & AHId & " and AHSGId=" & AHSGId & " and (dTimeElam<='" & LastAnnounceTime & "') and nCarNum>0"
                        CmdSql.ExecuteNonQuery()
                        CmdSql.Connection.Close()
                        'ثبت اکانتینگ
                        Dim InstanceAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                        For Each LoadCapcitorLoad In LstLoads
                            InstanceAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(LoadCapcitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting, LoadCapcitorLoad.nCarNum, Nothing, Nothing, Nothing, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId))
                        Next
                    Catch ex As Exception
                        If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                        R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, R2CoreTransportationAndLoadNotificationLogType.LoadCapacitorSedimentingFailed, ex.Message, "AHId:" + AHId.ToString, "AHSGId:" + AHSGId.ToString, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
                    End Try
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Function IsActiveSedimenting(YourAHId As Int64, YourAHSGId As Int64) As Boolean
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllSedimentingTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadSedimentationSetting, YourAHId), "&")
                Dim SedimentingStatus = Split(Mid(AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")(0)
                Return SedimentingStatus
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SedimentingLoadCapacitorLoad(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                If NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented And NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted And NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Then
                    CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set bFlag=1,LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " 
                                          Where (nEstelamId=" & YournEstelamId & ") and (LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & ")"
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YournEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                End If
            Catch ex As LoadCapacitorLoadNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function HowManyMinutesPassedfromSedimentation(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as LoadCapacitorAccounting
                      Where LoadCapacitorAccounting.nEstelamId=" & YourNSS.nEstelamId & " and LoadCapacitorAccounting.AccountType=" & R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting & " Order By LoadCapacitorAccounting.DateTimeMilladi desc", 360, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LoadIsNotSedimentedException
                Else
                    Return DateDiff(DateInterval.Minute, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), _DateTime.GetCurrentDateTimeMilladi())
                End If
            Catch ex As LoadIsNotSedimentedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HowManyMinutesMustPassedfromSedimentation(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim ComposeSearchString As String = YourNSS.AHSGId.ToString + "="
                Dim AllSedimentingTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadSedimentationSetting, YourNSS.AHId), "&")
                Dim MinutesMustPassedfromSedimenting = Split(Mid(AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")(1)
                Return MinutesMustPassedfromSedimenting
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManagement
        Private Shared _DateTime As New R2DateTime



    End Class

    Namespace Exceptions
        Public Class LoadIsNotSedimentedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "بار مورد نظر هنوز به مرحله رسوب نرسیده است"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace LoadTargets

    Public Class R2CoreTransportationAndLoadNotificationStandardProvinceStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            ProvinceId = Int64.MinValue
            ProvinceTitle = String.Empty
        End Sub

        Public Sub New(ByVal YourProvinceId As Int64, YourProvinceTitle As String)
            MyBase.New(YourProvinceId, YourProvinceTitle)
            ProvinceId = YourProvinceId
            ProvinceTitle = YourProvinceTitle
        End Sub

        Public Property ProvinceId As Int64
        Public Property ProvinceTitle As String

    End Class

    Public Interface ILoadTargets

    End Interface

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _NSSCity = Nothing
            _TargetTravelLength = Int64.MinValue
        End Sub

        Public Sub New(ByVal YourNSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure)
            MyBase.New(YourNSSCity.nCityCode, YourNSSCity.StrCityName)
            _NSSCity = YourNSSCity
            _TargetTravelLength = YourNSSCity.nDistance
        End Sub

        Public Property NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure
        Public Property TargetTravelLength As Int64

    End Class

    Public Class R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
        Private _DateTime As R2DateTime = New R2DateTime

        Public Function GetNSSLoadTarget(YournIdTarget As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
            Try
                Dim NSSCity = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdTarget)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(NSSCity)
            Catch ex As GetNSSException
                Throw New LoadTargetIdNotFoundException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadTargetsManagement
        Private Shared _DateTime As R2DateTime = New R2DateTime

        Public Shared Function GetNSSLoadTarget(YournIdTarget As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
            Try
                Dim NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdTarget)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(NSSCity)
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadTargets_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchforLeftCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadTargets_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchIntroCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions

        Public Class LoadTargetsforProvinceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقصدی برای استان مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadTargetIdNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شهری با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace



End Namespace

Namespace LoadSources

    Public Interface ILoadSources

    End Interface

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _NSSCity = Nothing
        End Sub

        Public Sub New(ByVal YourNSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure)
            MyBase.New(YourNSSCity.nCityCode, YourNSSCity.StrCityName)
            _NSSCity = YourNSSCity
        End Sub

        Public Property NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadSourcesManager
        Public Function GetNSSLoadSource(YournIdSource As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
            Try
                Dim NSSCity = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdSource)
                If NSSCity Is Nothing Then Throw New LoadSourceIdNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(NSSCity)
            Catch exx As GetNSSException
                Throw New LoadSourceIdNotFoundException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadSourcesManagement
        Public Shared Function GetNSSLoadSource(YournIdSource As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
            Try
                Dim NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdSource)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(NSSCity)
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadSources_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchIntroCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadSources_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchforLeftCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Namespace Exceptions

        Public Class LoadSourceIdNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شهری با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class



    End Namespace


End Namespace

Namespace LoaderTypes


    Public Class R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _LoaderTypeId = Int64.MinValue
            _LoaderTypeTitle = String.Empty
            _LoaderTypeOrganizationId = Int64.MinValue
            _LoaderTypeFixStatusId = Int64.MinValue
            _ViewFlag = False
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourLoaderTypeId As Int64, YourLoaderTypeTitle As String, YourLoaderTypeOrganizationId As Int64, YourLoaderTypeFixStatusId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourLoaderTypeId, YourLoaderTypeTitle)
            _LoaderTypeId = YourLoaderTypeId
            _LoaderTypeTitle = YourLoaderTypeTitle
            _LoaderTypeOrganizationId = YourLoaderTypeOrganizationId
            _LoaderTypeFixStatusId = YourLoaderTypeFixStatusId
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property LoaderTypeOrganizationId As Int64
        Public Property LoaderTypeFixStatusId As Int64
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoaderType
        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property LoaderTypeOrganizationId As Int64
        Public Property LoaderTypeFixStatusId As Int64
        Public Property LoaderTypeFixStatusTitle As String
        Public Property Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoaderTypesManager
        Public Function GetLoaderTypes_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeTitle Like N'%" & YourSearchString & "%' and LoaderTypes.Deleted=0 for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                    "Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeTitle Like N'%" & YourSearchString & "%' and LoaderTypes.Deleted=0 for json path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoaderType(YourLoaderTypeId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoaderType
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeId = " & YourLoaderTypeId & " and LoaderTypes.Deleted=0")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeId = " & YourLoaderTypeId & " and LoaderTypes.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationLoaderType With {.LoaderTypeId = Ds.Tables(0).Rows(0).Item("LoaderTypeId"), .LoaderTypeTitle = Ds.Tables(0).Rows(0).Item("LoaderTypeTitle").trim, .LoaderTypeOrganizationId = Ds.Tables(0).Rows(0).Item("LoaderTypeOrganizationId"), .LoaderTypeFixStatusId = Ds.Tables(0).Rows(0).Item("LoaderTypeFixStatusId"), .LoaderTypeFixStatusTitle = Ds.Tables(0).Rows(0).Item("LoaderTypeFixStatusTitle"), .Active = Ds.Tables(0).Rows(0).Item("Active")}
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoaderTypeChangeActivate(YourLoaderTypeId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim LoaderType = GetLoaderType(YourLoaderTypeId, True)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes 
                                      Set Active=" & IIf(LoaderType.Active, 0, 1) & " Where LoaderTypeId=" & YourLoaderTypeId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetLoaderTypeBySoftwareUser(YourSoftwareUserId As Int64) As R2CoreTransportationAndLoadNotificationLoaderType
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(New R2DateTimeService)
                Dim Truck = InstanceTrucks.GetTruckBySoftwareUser(YourSoftwareUserId, False)
                Return GetLoaderType(Truck.LoaderTypeId, False)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoaderTypesManagement
        Public Shared Function GetLoaderTypes_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where Left(LoaderTypeTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' and Deleted=0 and Active=1 Order By LoaderTypeTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(Loopx).Item("LoaderTypeId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoaderTypes_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where LoaderTypeTitle Like '%" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "%' and Deleted=0 and Active=1 and ViewFlag=1 Order By LoaderTypeTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(Loopx).Item("LoaderTypeId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoaderType(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadTypeNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(0).Item("LoaderTypeId"), DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(0).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(0).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As LoadTypeNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Namespace Exceptions
        Public Class LoadTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace BlackIPs
    Public MustInherit Class R2CoreTransportationAndLoadNotificationBlackIPTypes

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceBlackIPsManager
        Private _DateTime As New R2DateTime


    End Class


End Namespace

Namespace MobileProcessesManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationMobileProcesses
        Public Shared ReadOnly TruckDriverInformation As Int64 = 1
        Public Shared ReadOnly TruckInformation As Int64 = 2
        Public Shared ReadOnly Top5Turns As Int64 = 3
        Public Shared ReadOnly LoadCapacitor As Int64 = 4
        Public Shared ReadOnly LoadAllocation As Int64 = 5
        Public Shared ReadOnly LoadAllocationPriorityManagement As Int64 = 6
        Public Shared ReadOnly LoadPermissionsIssuedOrderByPriorityReportPage As Int64 = 9
        Public Shared ReadOnly RealTimeTurnRegisterRequest As Int64 = 11

    End Class

End Namespace

Namespace DriverSelfDeclaration

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _DSDId = Int64.MinValue
            _DSDName = String.Empty
            _DSDTitle = String.Empty
            _DefaultValue = String.Empty
            _PersianKeyboard = Boolean.FalseString
            _IsNumeric = Boolean.FalseString
            _DecimalPoint = Boolean.FalseString
            _HasAttachement = Boolean.FalseString
            _Min = String.Empty
            _Max = String.Empty
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _Active = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourDSDId As Int64, YourDSDName As String, YourDSDTitle As String, YourDefaultValue As String, YourPersianKeyboard As Boolean, YourIsNumeric As Boolean, YourDecimalPoint As Boolean, YourHasAttachement As Boolean, YourMin As String, YourMax As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourDSDId, YourDSDName)
            _DSDId = YourDSDId
            _DSDName = YourDSDName
            _DSDTitle = YourDSDTitle
            _DefaultValue = YourDefaultValue
            _PersianKeyboard = YourPersianKeyboard
            _IsNumeric = YourIsNumeric
            _DecimalPoint = YourDecimalPoint
            _HasAttachement = YourHasAttachement
            _Min = YourMin
            _Max = YourMax
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _Active = YourActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property DSDId As Int64
        Public Property DSDName As String
        Public Property DSDTitle As String
        Public Property DefaultValue As String
        Public Property PersianKeyboard As Boolean
        Public Property IsNumeric As Boolean
        Public Property DecimalPoint As Boolean
        Public Property HasAttachement As Boolean
        Public Property Min As String
        Public Property Max As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _DSDId = Int64.MinValue
            _DSDName = String.Empty
            _DSDTitle = String.Empty
            _DefaultValue = String.Empty
            _DSDValue = String.Empty
            _PersianKeyboard = Boolean.FalseString
            _IsNumeric = Boolean.FalseString
            _DecimalPoint = Boolean.FalseString
            _HasAttachement = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourDSDId As Int64, YourDSDName As String, YourDSDTitle As String, YourDefaultValue As String, YourDSDValue As String, YourPersianKeyboard As Boolean, YourIsNumeric As Boolean, YourDecimalPoint As Boolean, YourHasAttachement As Boolean)
            MyBase.New(YourDSDId, YourDSDName)
            _DSDId = YourDSDId
            _DSDName = YourDSDName
            _DSDTitle = YourDSDTitle
            _DefaultValue = YourDefaultValue
            _DSDValue = YourDSDValue
            _PersianKeyboard = YourPersianKeyboard
            _IsNumeric = YourIsNumeric
            _DecimalPoint = YourDecimalPoint
            _HasAttachement = YourHasAttachement
        End Sub

        Public Property DSDId As Int64
        Public Property DSDName As String
        Public Property DSDTitle As String
        Public Property DefaultValue As String
        Public Property DSDValue As String
        Public Property PersianKeyboard As Boolean
        Public Property IsNumeric As Boolean
        Public Property DecimalPoint As Boolean
        Public Property HasAttachement As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationManager
        Private _R2PrimaryFSWS As R2PrimaryFileSharingWebService = New R2PrimaryFileSharingWebService()
        Private _DateTime As New R2DateTime

        Private Function GetDSDIdFull(YourDSDId As Int64) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Return InstancePublicProcedures.RepeatStr("0", 2 - YourDSDId.ToString().Length) + YourDSDId.ToString()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ControlforExpiredDSDs(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Try
                Dim D = _DateTime.GetCurrentDateTime

                'بررسی منقضی شدن اعتبار مشخصات خوداظهاری
                Dim DS As DataSet
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top 1 DateTimeMilladi from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations 
                           Where nIdCar = " & YourNSSTruck.NSSCar.nIdCar & " Order By DateTimeMilladi desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                Else
                    Dim Diff = DateDiff(DateInterval.Day, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), D.DateTimeMilladi)
                    If Diff > InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 1) Then
                        Throw New DSDsDayLimitExpiredException
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ControlforDSDChangingDayLimit(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Try
                Dim D = _DateTime.GetCurrentDateTime

                'کنترل زمان انتظار برای تغییر مشخصات خوداظهاری  
                Dim DS As DataSet
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top 1 DateTimeMilladi from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations 
                           Where nIdCar = " & YourNSSTruck.NSSCar.nIdCar & " Order By DateTimeMilladi desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                Else
                    Dim Diff = DateDiff(DateInterval.Day, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), D.DateTimeMilladi)
                    If Diff < InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 0) Then Throw New DayLimitforDSDDataEntryException(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 0) - Diff)
                End If
            Catch ex As DayLimitforDSDDataEntryException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Function GetNSSDriverSelfDeclarationParameter(YourDSDId As Int64) As R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters Where DSDId=" & YourDSDId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New DriverSelfDeclarationParameterNotFoundException
                Return New R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure(DS.Tables(0).Rows(0).Item("DSDId"), DS.Tables(0).Rows(0).Item("DSDName").trim, DS.Tables(0).Rows(0).Item("DSDTitle").trim, DS.Tables(0).Rows(0).Item("DefaultValue").trim, DS.Tables(0).Rows(0).Item("PersianKeyboard"), DS.Tables(0).Rows(0).Item("IsNumeric"), DS.Tables(0).Rows(0).Item("DecimalPoint"), DS.Tables(0).Rows(0).Item("HasAttachement"), DS.Tables(0).Rows(0).Item("Min").trim, DS.Tables(0).Rows(0).Item("Max").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As DriverSelfDeclarationParameterNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetDeclarations(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourImmediately As Boolean) As List(Of R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure)
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlClient.SqlCommand("Select DataBox.DSDId,DataBox.DSDName,DataBox.DSDTitle,DataBox.DefaultValue,DataBox.PersianKeyboard,DataBox.IsNumeric,DataBox.DecimalPoint,DataBox.HasAttachement,ISNULL(DataBox.DSDValue,'') as DSDValue from 
                                                                    (Select DSDParams.DSDId,DSDParams.DSDName,DSDParams.DSDTitle,DSDParams.DefaultValue,DSDParams.PersianKeyboard,DSDParams.IsNumeric,DSDParams.DecimalPoint,DSDParams.HasAttachement,DataBox.DSDValue from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters as DSDParams
                                                                      left outer  Join 
                                                                    (Select * from R2PrimaryTransportationAndLoadNotification.DBO.TblDriverSelfDeclarations as DSDs Where DSDs.nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and DSDs.RelationActive=1) as DataBox On DSDParams.DSDId=DataBox.DSDId 
                                                                      Where DSDParams.Active=1) as DataBox Order By DataBox.DSDId")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    Da.Fill(DS)
                Else
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select DataBox.DSDId,DataBox.DSDName,DataBox.DSDTitle,DataBox.DefaultValue,DataBox.PersianKeyboard,DataBox.IsNumeric,DataBox.DecimalPoint,DataBox.HasAttachement,ISNULL(DataBox.DSDValue,'') as DSDValue from 
                             (Select DSDParams.DSDId,DSDParams.DSDName,DSDParams.DSDTitle,DSDParams.DefaultValue,DSDParams.PersianKeyboard,DSDParams.IsNumeric,DSDParams.DecimalPoint,DSDParams.HasAttachement,DataBox.DSDValue from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters as DSDParams
                                left outer  Join 
                                  (Select * from R2PrimaryTransportationAndLoadNotification.DBO.TblDriverSelfDeclarations as DSDs Where DSDs.nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and DSDs.RelationActive=1) as DataBox On DSDParams.DSDId=DataBox.DSDId 
                                   Where DSDParams.Active=1) as DataBox Order By DataBox.DSDId", 3600, DS, New Boolean)
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure(DS.Tables(0).Rows(Loopx).Item("DSDId"), DS.Tables(0).Rows(Loopx).Item("DSDName"), DS.Tables(0).Rows(Loopx).Item("DSDTitle"), DS.Tables(0).Rows(Loopx).Item("DefaultValue"), DS.Tables(0).Rows(Loopx).Item("DSDValue"), DS.Tables(0).Rows(Loopx).Item("PersianKeyboard"), DS.Tables(0).Rows(Loopx).Item("IsNumeric"), DS.Tables(0).Rows(Loopx).Item("DecimalPoint"), DS.Tables(0).Rows(Loopx).Item("HasAttachement")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SetDeclarations(YourDSDs As String, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim D = _DateTime.GetCurrentDateTime

                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourDSDs)

                'بررسی محدودیت زمان برای تغییرات
                ControlforDSDChangingDayLimit(YourNSSTruck)

                'ثبت مشخصات
                Dim DSDArray = YourDSDs.Split("|")
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations Set RelationActive=0 Where nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1"
                CmdSql.ExecuteNonQuery()
                For Loopx As Int16 = 0 To DSDArray.Length - 1
                    Dim DSDId = DSDArray(Loopx).Split(":")(0)
                    Dim DSDValue = DSDArray(Loopx).Split(":")(1)
                    If DSDValue.Trim = String.Empty Then Throw New DriverSelfDeclarationsEmtpyNotAllowdException
                    Dim NSSDSDParameter = GetNSSDriverSelfDeclarationParameter(DSDId)
                    If NSSDSDParameter.Min = String.Empty And NSSDSDParameter.Max = String.Empty Then
                        'بدون محدودیت
                    ElseIf NSSDSDParameter.Min <> String.Empty And NSSDSDParameter.Max <> String.Empty Then
                        'کنترل با محدوده عددی
                        Dim DSDValueTemp As Int64 = Convert.ToInt64(Val(DSDValue))
                        If DSDValueTemp < Convert.ToInt64(Val(NSSDSDParameter.Min)) Or DSDValueTemp > Convert.ToInt64(Val(NSSDSDParameter.Max)) Then
                            Throw New DSDDataOutofRangeException(NSSDSDParameter.DSDTitle)
                        End If
                    ElseIf NSSDSDParameter.Min <> String.Empty And NSSDSDParameter.Max = String.Empty Then
                        'کنترل مقادیر مجاز
                        Dim ValidEntries = NSSDSDParameter.Min.Split(";")
                        If Not ValidEntries.Contains(DSDValue) Then Throw New DSDDataOutofRangeException(NSSDSDParameter.DSDTitle)
                    End If
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations(nIdCar,DSDId,DSDValue,DateTimeMilladi,DateShamsi,Time,UserId,RelationActive)
                                            Values(" & YourNSSTruck.NSSCar.nIdCar & "," & NSSDSDParameter.DSDId & ",'" & DSDValue & "','" & D.DateTimeMilladiFormated & "','" & D.DateShamsiFull & "','" & D.Time & "'," & YourNSSSoftwareUser.UserId & ",1)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As DSDDataOutofRangeException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DayLimitforDSDDataEntryException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DriverSelfDeclarationsEmtpyNotAllowdException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DriverSelfDeclarationParameterNotFoundException
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

        Public Function GetAllowedLoadingCapacity(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As String
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select R2PrimaryTransportationAndLoadNotification.dbo.GetAllowedLoadingCapacity(" & YourNSSTruck.NSSCar.nIdCar & ") as Allowed", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New DSDBaseInfNotFoundException
                End If
                If DBNull.Value.Equals(DS.Tables(0).Rows(0).Item("Allowed")) Then Throw New DSDBaseInfNotFoundException
                Return DS.Tables(0).Rows(0).Item("Allowed")
            Catch ex As DSDBaseInfNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SaveDSDImage(YourDSDId As Int64, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourBase64StringImage As String)
            Try
                Dim InstanceConfiguration = New R2Core.ConfigurationManagement.R2CoreInstanceConfigurationManager
                If YourBase64StringImage.Length > InstanceConfiguration.GetConfigInt64(R2Core.ConfigurationManagement.R2CoreConfigurations.JPGBitmap, 4) Then Throw New UploadedImageSizeExeededException
                _R2PrimaryFSWS.WebMethodSaveFile(R2CoreTransportationAndLoadNotificationRawGroups.DriverSelfDeclarations, New R2CoreFile(GetDSDIdFull(YourDSDId) + YourNSSTruck.NSSCar.nIdCar.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)).FileName, Convert.FromBase64String(YourBase64StringImage), _R2PrimaryFSWS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword))
            Catch ex As UploadedImageSizeExeededException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class DriverSelfDeclarationParameterNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر خوداظهاری راننده با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class DriverSelfDeclarationsEmtpyNotAllowdException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات را تکمیل کرده و سپس ارسال نمایید"
                End Get
            End Property
        End Class

        Public Class DayLimitforDSDDataEntryException
            Inherits ApplicationException

            Private _DayLimit = Int16.MaxValue

            Public Sub New(YourDayLimit As Int16)
                _DayLimit = YourDayLimit
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان ارسال مشخصات خود اظهاری تا " + _DayLimit.ToString + " روز دیگر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class DSDsDayLimitExpiredException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مدت زمان اعتبار مشخصات خود اظهاری ناوگان منقضی شده است"
                End Get
            End Property
        End Class

        Public Class DSDsNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات خوداظهاری ناوگان یافت نشد"
                End Get
            End Property
        End Class

        Public Class DSDBaseInfNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات پایه در فرآیند خوداظهاری مبنی بر مشخصات وارد شده یافت نشد"
                End Get
            End Property
        End Class

        Public Class DSDDataOutofRangeException
            Inherits ApplicationException

            Private _Message = String.Empty

            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقادیر غیر مجاز - " + _Message
                End Get
            End Property
        End Class


    End Namespace

End Namespace

Namespace LoadingAndDischargingPlaces

    Public MustInherit Class LoadingAndDischargingPlaces
        Public Shared ReadOnly Property None = 1000
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _LADPlaceId = Int64.MinValue
            _LADPlaceTitle = String.Empty
            _LADPlaceTel = String.Empty
            _LADPlaceAddress = String.Empty
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _LoadingActive = Boolean.FalseString
            _DischargingActive = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourLADPlaceId As Int64, YourLADPlaceTitle As String, YourLADPlaceTel As String, YourLADPlaceAddress As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourLoadingActive As Boolean, YourDischargingActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourLADPlaceId, YourLADPlaceTitle)
            _LADPlaceId = YourLADPlaceId
            _LADPlaceTitle = YourLADPlaceTitle
            _LADPlaceTel = YourLADPlaceTel
            _LADPlaceAddress = YourLADPlaceAddress
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _LoadingActive = YourLoadingActive
            _DischargingActive = YourDischargingActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property LADPlaceId As Int64
        Public Property LADPlaceTitle As String
        Public Property LADPlaceTel As String
        Public Property LADPlaceAddress As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property LoadingActive As Boolean
        Public Property DischargingActive As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    'BPTChanged
    Public Class RawLoadingAndDischargingPlace
        Public Property LADPlaceId As Int64
        Public Property LADPlaceTitle As String
        Public Property LADPlaceTel As String
        Public Property LADPlaceAddress As String
        Public Property LoadingActive As Boolean
        Public Property DischargingActive As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationMClassLoadingAndDischargingPlacesManager
        Private _DateTime As New R2DateTime
        Private _LoadingPlaceEquivalent = "مبدا بارگیری"
        Private _DischargingPlaceEquivalent = "محل تخلیه"

        Public Function GetNSSLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure
            Try
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure = Nothing
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(Ds) <> 0 Then NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                    NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
                Return NSS
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUnActiveLoadingAndDischargingPlaces() As List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where (LoadingActive=0 or DischargingActive=0) and Deleted=0 Order By LADPlaceTitle", 300, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(DS.Tables(0).Rows(Loopx).Item("LADPlaceId"), DS.Tables(0).Rows(Loopx).Item("LADPlaceTitle").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("Time").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("LoadingActive"), DS.Tables(0).Rows(Loopx).Item("DischargingActive"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadingAndDischargingPlaces_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where Left(LADPlaceTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' and ViewFlag=1 and Deleted=0 Order By LADPlaceTitle", 300, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(DS.Tables(0).Rows(Loopx).Item("LADPlaceId"), DS.Tables(0).Rows(Loopx).Item("LADPlaceTitle").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("Time").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(0).Item("LoadingActive"), DS.Tables(0).Rows(0).Item("DischargingActive"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadingAndDischargingPlaceDelete(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set Deleted=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadingPlaceChangeActiveStatus(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim CurrentStatus = GetNSSLoadingAndDischargingPlace(YourNSSLADPlace.LADPlaceId, True).LoadingActive
                If CurrentStatus Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=0 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendSMSLoadingAndDischargingPLacesChangeStatus(YourNSSLADPlace.LADPlaceTitle, _LoadingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not CurrentStatus))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DischargingPlaceChangeActiveStatus(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim CurrentStatus = GetNSSLoadingAndDischargingPlace(YourNSSLADPlace.LADPlaceId, True).DischargingActive
                If CurrentStatus Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=0 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendSMSLoadingAndDischargingPLacesChangeStatus(YourNSSLADPlace.LADPlaceTitle, _DischargingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not CurrentStatus))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared Sub SendSMSLoadingAndDischargingPLacesChangeStatus(YourLoadingDischargingPlaceTitle As String, YourLoadingDischargingNote As String, YourStatus As String)
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceMoneyWalletManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                'کنترل فعال بودن سرویس اس ام اس
                If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotification.ConfigurationsManagement.R2CoreTransportationAndLoadNotificationConfigurations.LoadingDischargingPlaces, 2).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'کانتنت پیام
                Dim myData = New SMSCreationData
                myData.Data1 = YourLoadingDischargingPlaceTitle
                myData.Data2 = YourLoadingDischargingNote
                myData.Data3 = YourStatus
                'ارسال اس ام اس
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotification.SMS.SMSTypes.R2CoreTransportationAndLoadNotificationSMSTypes.LoadingAndDischargingPLacesChangeStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New LoadingAndDischargingSendSMSFailedException
            Catch ex As LoadingAndDischargingSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function LoadingAndDischargingPlaceRegister(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                If YourNSSLADPlace.LADPlaceTitle = String.Empty Then Return Nothing
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _DateTime.GetCurrentDateTime
                If YourNSSLADPlace.LADPlaceId = 0 Then
                    CmdSql.CommandText = "Select Top 1 LADPlaceId From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces with (tablockx) Order By LADPlaceId Desc"
                    Dim LADPlaceIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    YourNSSLADPlace.LADPlaceId = LADPlaceIdNew
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces(LADPlaceId,LADPlaceTitle,DateTimeMilladi,DateShamsi,Time,UserId,LoadingActive,DischargingActive,ViewFlag,Deleted)
                                            Values(" & LADPlaceIdNew & ",'" & YourNSSLADPlace.LADPlaceTitle & "','" & D.DateTimeMilladiFormated & "','" & D.DateShamsiFull & "','" & D.Time & "'," & YourNSSSoftwareUser.UserId & ",1,1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Else
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LADPlaceTitle='" & YourNSSLADPlace.LADPlaceTitle & "' Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return YourNSSLADPlace.LADPlaceId
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsActiveLoadingPlace(YourLoadingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                If NSS.LoadingActive = False Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsActiveDischargingPlace(YourDischargingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourDischargingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                If NSS.DischargingActive = False Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function GetLoadingAndDischargingPlaces_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceTitle like N'%" & YourSearchString & "%' and Deleted=0
                        Order By LADPlaceTitle 
                        for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceTitle like N'%" & YourSearchString & "%' and Deleted=0
                        Order By LADPlaceTitle 
                        for json path", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadingAndDischargingPlace(YourLADPlaceId As Int64, YourImmediately As Boolean) As RawLoadingAndDischargingPlace
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                                                       from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                                                       Where LADPlaceId=" & YourLADPlaceId & " and Deleted=0
                                                       Order By LADPlaceTitle")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceId=" & YourLADPlaceId & " and Deleted=0
                        Order By LADPlaceTitle", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New LoadingAndDischargingPlaceNotFoundException
                    End If
                End If
                Return New RawLoadingAndDischargingPlace With {.LADPlaceId = DS.Tables(0).Rows(0).Item("LADPlaceId"), .LADPlaceTitle = DS.Tables(0).Rows(0).Item("LADPlaceTitle").trim, .LADPlaceTel = DS.Tables(0).Rows(0).Item("LADPlaceTel").trim, .LADPlaceAddress = DS.Tables(0).Rows(0).Item("LADPlaceAddress").trim, .LoadingActive = DS.Tables(0).Rows(0).Item("LoadingActive"), .DischargingActive = DS.Tables(0).Rows(0).Item("DischargingActive")}
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function LoadingAndDischargingPlaceRegister(YourRawLoadingAndDischargingPlace As RawLoadingAndDischargingPlace) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                If YourRawLoadingAndDischargingPlace.LADPlaceTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _DateTime.GetCurrentDateTime
                CmdSql.CommandText = "Select Top 1 LADPlaceId From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces with (tablockx) Order By LADPlaceId Desc"
                Dim LADPlaceIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces(LADPlaceId,LADPlaceTitle,LADPlaceTel,LADPlaceAddress,DateTimeMilladi,DateShamsi,Time,UserId,LoadingActive,DischargingActive,ViewFlag,Deleted)
                                            Values(" & LADPlaceIdNew & ",'" & YourRawLoadingAndDischargingPlace.LADPlaceTitle & "','" & YourRawLoadingAndDischargingPlace.LADPlaceTel & "','" & YourRawLoadingAndDischargingPlace.LADPlaceAddress & "','" & D.DateTimeMilladiFormated & "','" & D.DateShamsiFull & "','" & D.Time & "'," & InstanceSoftwareUsers.GetSystemUserId & ",1,1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return LADPlaceIdNew
            Catch ex As DataEntryException
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

        Public Sub LoadingAndDischargingPlaceUpdating(YourLoadingAndDischargingPlace As RawLoadingAndDischargingPlace)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                If YourLoadingAndDischargingPlace.LADPlaceTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LADPlaceTitle='" & YourLoadingAndDischargingPlace.LADPlaceTitle & "',LADPlaceTel='" & YourLoadingAndDischargingPlace.LADPlaceTel & "',LADPlaceAddress='" & YourLoadingAndDischargingPlace.LADPlaceAddress & "' Where LADPlaceId=" & YourLoadingAndDischargingPlace.LADPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As DataEntryException
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

        Public Sub LoadingAndDischargingPlaceDelete(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set Deleted=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadingPlaceChangeActiveStatus(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim LADPlace = GetLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId, True)
                If LADPlace.LoadingActive Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=0 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendLoadingAndDischargingPLacesChangeStatusSMS(LADPlace.LADPlaceTitle, _LoadingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not LADPlace.LoadingActive))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DischargingPlaceChangeActiveStatus(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim LADPlace = GetLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId, True)
                If LADPlace.DischargingActive Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=0 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendLoadingAndDischargingPLacesChangeStatusSMS(LADPlace.LADPlaceTitle, _DischargingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not LADPlace.DischargingActive))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SendLoadingAndDischargingPLacesChangeStatusSMS(YourLoadingDischargingPlaceTitle As String, YourLoadingDischargingNote As String, YourStatus As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadingDischargingPlaces, 2).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'کانتنت پیام
                Dim myData = New SMSCreationData With {.Data1 = YourLoadingDischargingPlaceTitle, .Data2 = YourLoadingDischargingNote, .Data3 = YourStatus}
                'ارسال اس ام اس
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotification.SMS.SMSTypes.R2CoreTransportationAndLoadNotificationSMSTypes.LoadingAndDischargingPLacesChangeStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New LoadingAndDischargingSendSMSFailedException
            Catch ex As LoadingAndDischargingSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class LoadingAndDischargingSendSMSFailedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس با خطا مواجه شد"
                End Get
            End Property
        End Class

        Public Class LoadingAndDischargingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری یا محل تخلیه با کد شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری با مشخصات مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class DischargingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "محل تخلیه با مشخصات مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadingPlaceIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری مجاز نیست"
                End Get
            End Property
        End Class

        Public Class DischargingPlaceIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "محل تخلیه مجاز نیست"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

'BPTChanged
Namespace LoadAnnouncementPlaces
    Public Class R2CoreTransportationAndLoadNotificationLoadAnnouncementPlacesManager
        Public Function GetLoadAnnouncementPlaces() As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select LoadAnnouncementPlaces.LAPTitle,LoadAnnouncementPlaces.LAPIconName,LoadAnnouncementPlaces.LAPURL,LoadAnnouncementPlaces.Description 
                  from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAnnouncementPlaces as LoadAnnouncementPlaces 
                  Where Active=1 Order By LAPId for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New SoftwareUserHasNotAnyWebProcessPermissionException
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class
End Namespace

'BPTChanged
Namespace FactoriesAndProductionCentersManagement

    Public Class RawFactoryAndProductionCenter
        Public FPCId As Int64
        Public FPCTitle As String
        Public FPCTel As String
        Public FPCAddress As String
        Public FPCManagerMobileNumber As String
        Public FPCManagerNameFamily As String
        Public EmailAddress As String
        Public Active As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager
        Private _R2DateTimeService As New R2DateTimeService

        Public Function HasFactoryAndProductionCenterMoneyWallet(YourFactoryAndProductionCenterId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 1 MoneyWallets.CardId from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets as FPCRMoneyWallets On MoneyWallets.CardId=FPCRMoneyWallets.CardId 
                        Where MoneyWallets.Active=1 and FPCRMoneyWallets.RelationActive=1 and FPCRMoneyWallets.FPCId=" & YourFactoryAndProductionCenterId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetFactoriesAndProductionCenters_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                               FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                        Where FPCTitle Like N'%" & YourSearchString & "%' and FPCs.Deleted=0
                        Order By FPCTitle
                        for json auto")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                               FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                        Where FPCTitle Like N'%" & YourSearchString & "%' and FPCs.Deleted=0
                        Order By FPCTitle
                        for json auto", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetFactoryAndProductionCenter(YourFactoryAndProductionCenterId As Int64, YourImmediately As Boolean) As RawFactoryAndProductionCenter
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                                                         FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                                                       From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                                                       Where FPCId = " & YourFactoryAndProductionCenterId & " and FPCs.Deleted=0")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                                     "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                                                         FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                                                      From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                                                      Where FPCId = " & YourFactoryAndProductionCenterId & " and FPCs.Deleted=0", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnyNotFoundException
                End If
                Return New RawFactoryAndProductionCenter With {.FPCId = DS.Tables(0).Rows(0).Item("FPCId"), .FPCTitle = DS.Tables(0).Rows(0).Item("FPCTitle").trim, .FPCTel = DS.Tables(0).Rows(0).Item("FPCTel").trim, .FPCAddress = DS.Tables(0).Rows(0).Item("FPCAddress").trim, .FPCManagerMobileNumber = DS.Tables(0).Rows(0).Item("FPCManagerMobileNumber").trim, .FPCManagerNameFamily = DS.Tables(0).Rows(0).Item("FPCManagerNameFamily").trim, .EmailAddress = DS.Tables(0).Rows(0).Item("EmailAddress").trim, .Active = DS.Tables(0).Rows(0).Item("Active")}
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function FactoryAndProductionCenterRegister(YourRawFactoryAndProductionCenter As RawFactoryAndProductionCenter) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_R2DateTimeService, Nothing)
                If YourRawFactoryAndProductionCenter.FPCTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _R2DateTimeService.DateTimeServ.GetCurrentDateTime
                CmdSql.CommandText = "Select Top 1 FPCId From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters with (tablockx) Order By FPCId Desc"
                Dim FPCIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters(FPCId,FPCTitle,FPCTel,FPCAddress,FPCManagerMobileNumber,FPCManagerNameFamily,EmailAddress,ViewFlag,Active,Deleted)
                                            Values(" & FPCIdNew & ",'" & YourRawFactoryAndProductionCenter.FPCTitle & "','" & YourRawFactoryAndProductionCenter.FPCTel & "','" & YourRawFactoryAndProductionCenter.FPCAddress & "','" & YourRawFactoryAndProductionCenter.FPCManagerMobileNumber & "','" & YourRawFactoryAndProductionCenter.FPCManagerNameFamily & "','" & YourRawFactoryAndProductionCenter.EmailAddress & "',1,1,0)"
                CmdSql.ExecuteNonQuery()

                Dim UserId As Int64 = Int64.MinValue
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & FPCIdNew & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                Else
                    'ایجاد کاربر
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourRawFactoryAndProductionCenter.FPCManagerMobileNumber, .UserActive = True, .UserName = YourRawFactoryAndProductionCenter.FPCTitle, .UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters}, False, InstanceSoftwareUsers.GetSystemUser.UserId)
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & FPCIdNew & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                    'ایجاد دسترسی ها
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters.ToString + ":"
                    Dim AllofProcessGroupsIds As String()
                    Dim AllofProcessesIds As String()
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی , ارسال به مدیریت مجوز
                    Dim AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofProcessGroupsIds)
                    End If

                    ' کیف پول 
                    'ایجاد کیف پول کاربر
                    Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager
                    Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                    CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets(CardId,FPCId) Values(" & MoneyWalletId & "," & FPCIdNew & ")"
                    CmdSql.ExecuteNonQuery()

                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return FPCIdNew
            Catch ex As SMSTypeIdNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As UserNotExistByMobileNumberException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SoftwareUserMobileNumberBelongsToSomeoneElseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DataEntryException
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

        Public Sub DeleteFactoryAndProductionCenter(YourFPCId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters Set Deleted=0
                                      Where FPCId=" & YourFPCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub EditFactoryAndProductionCenter(YourRawFactoryAndProductionCenter As RawFactoryAndProductionCenter)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, Nothing)
                If YourRawFactoryAndProductionCenter.FPCTitle = String.Empty Then Throw New DataEntryException

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters 
                                      Set FPCTitle='" & YourRawFactoryAndProductionCenter.FPCTitle & "',FPCTel='" & YourRawFactoryAndProductionCenter.FPCTel & "',FPCAddress='" & YourRawFactoryAndProductionCenter.FPCAddress & "',FPCManagerNameFamily='" & YourRawFactoryAndProductionCenter.FPCManagerNameFamily & "',FPCManagerMobileNumber='" & YourRawFactoryAndProductionCenter.FPCManagerMobileNumber & "',EmailAddress='" & YourRawFactoryAndProductionCenter.EmailAddress & "'
                                      Where FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                CmdSql.ExecuteNonQuery()

                Dim UserId As Int64 = Int64.MinValue
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException

                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & YourRawFactoryAndProductionCenter.FPCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                Else
                    'ایجاد کاربر
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourRawFactoryAndProductionCenter.FPCManagerMobileNumber, .UserActive = True, .UserName = YourRawFactoryAndProductionCenter.FPCTitle, .UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters}, False, InstanceSoftwareUsers.GetSystemUser.UserId)

                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & YourRawFactoryAndProductionCenter.FPCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()

                    'ایجاد دسترسی ها
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters.ToString + ":"
                    Dim AllofProcessGroupsIds As String()
                    Dim AllofProcessesIds As String()
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی و ارسال به مدیریت مجوز
                    Dim AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofProcessGroupsIds)
                    End If

                    'بررسی کیف پول 
                    Dim HasMoneyWallet = HasFactoryAndProductionCenterMoneyWallet(YourRawFactoryAndProductionCenter.FPCId)
                    If Not HasMoneyWallet Then
                        'ایجاد کیف پول کاربر
                        Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager
                        Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                        CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets 
                                              Where CardId=" & MoneyWalletId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets(CardId,FPCId) Values(" & MoneyWalletId & "," & YourRawFactoryAndProductionCenter.FPCId & ")"
                        CmdSql.ExecuteNonQuery()
                    End If
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SMSTypeIdNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As UserNotExistByMobileNumberException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SoftwareUserMobileNumberBelongsToSomeoneElseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DataEntryException
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

        Public Function GetSoftwareUserIdfromFactoryAndProductionCenterId(YourFactoryAndProductionCenterId As Int64, YourImmediate As Boolean) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers as FPCRelationSoftwareUsers on SoftwareUsers.UserId=FPCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FactoriesAndProductionCenters on FPCRelationSoftwareUsers.FPCId=FactoriesAndProductionCenters.FPCId 
                        where FPCRelationSoftwareUsers.FPCId=" & YourFactoryAndProductionCenterId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New FactoryAndProductionCenterNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers as FPCRelationSoftwareUsers on SoftwareUsers.UserId=FPCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FactoriesAndProductionCenters on FPCRelationSoftwareUsers.FPCId=FactoriesAndProductionCenters.FPCId 
                        where FPCRelationSoftwareUsers.FPCId=" & YourFactoryAndProductionCenterId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New FactoryAndProductionCenterNotFoundException
                End If
                Return Convert.ToInt64(DS.Tables(0).Rows(0).Item("UserId"))
            Catch ex As FactoryAndProductionCenterNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub FactoryAndProductionCenterChangeActiveStatus(YourFactoryAndProductionCenterId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim SoftwareUserId = GetSoftwareUserIdfromFactoryAndProductionCenterId(YourFactoryAndProductionCenterId, True)
                Dim FactoryAndProductionCenter = GetFactoryAndProductionCenter(YourFactoryAndProductionCenterId, True)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters 
                                      Set Active=" & IIf(FactoryAndProductionCenter.Active, 0, 1) & "
                                      Where FPCId=" & YourFactoryAndProductionCenterId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserActive=" & IIf(FactoryAndProductionCenter.Active, 0, 1) & " Where Userid=" & SoftwareUserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                SendFactoryAndProductionCenterChangeActiveStatusSMS(FactoryAndProductionCenter.FPCTitle, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not FactoryAndProductionCenter.Active))
            Catch ex As FactoryAndProductionCenterNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As AnyNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TransportCompanyNotFoundException
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

        Public Sub SendFactoryAndProductionCenterChangeActiveStatusSMS(YourFPCTitle As String, YourStatus As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 9).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'ارسال اس ام اس
                Dim myData = New SMSCreationData With {.Data1 = YourFPCTitle, .Data2 = YourStatus}
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotificationSMSTypes.FactoryAndProductionCenterChangeActiveStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class FactoryAndProductionCenterNotFoundException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement.R2CoreTransportationAndLoadNotificationPredefinedMessages.FactoryAndProductionCenterNotFoundException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement.R2CoreTransportationAndLoadNotificationPredefinedMessages.FactoryAndProductionCenterNotFoundException).MsgId
        End Sub
    End Class


End Namespace









