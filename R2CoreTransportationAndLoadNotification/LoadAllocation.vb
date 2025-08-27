
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
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters.Exceptions
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
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.TurnAccounting
Imports Newtonsoft.Json
Imports R2CoreTransportationAndLoadNotification.Caching
Imports System.CodeDom
Imports R2Core.MoneyWallet.Exceptions
Imports R2CoreParkingSystem.EnterExitManagement



Namespace LoadAllocation

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure

        Public Sub New()
            _LoadAllocationStatusId = Int64.MinValue
            _LoadAllocationStatusTitle = String.Empty
            _LoadAllocationStatusColor = String.Empty
            _ViewFlag = False
        End Sub

        Public Sub New(ByVal YourLoadAllocationStatusId As Int64, YourLoadAllocationStatusTitle As String, YourLoadAllocationStatusColor As String, YourViewFlag As Boolean)
            _LoadAllocationStatusId = YourLoadAllocationStatusId
            _LoadAllocationStatusTitle = YourLoadAllocationStatusTitle
            _LoadAllocationStatusColor = YourLoadAllocationStatusColor
            _ViewFlag = YourViewFlag
        End Sub

        Public Property LoadAllocationStatusId As Int64
        Public Property LoadAllocationStatusTitle As String
        Public Property LoadAllocationStatusColor As String
        Public Property ViewFlag As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure
        Public Sub New()
            MyBase.New()
            _LAId = Long.MinValue
            _nEstelamId = Long.MinValue
            _TurnId = Long.MinValue
            _LAStatusId = Long.MinValue
            _LANote = String.Empty
            _Priority = Int16.MinValue
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Long.MinValue
        End Sub

        Public Sub New(ByVal YourLoadAllocationId As Int64, ByVal YournEstelamId As Int64, ByVal YourTurnId As Int64, YourLAStatusId As Int64, YourLANote As String, YourPriority As Int16, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64)
            _LAId = YourLoadAllocationId
            _nEstelamId = YournEstelamId
            _TurnId = YourTurnId
            _LAStatusId = YourLAStatusId
            _LANote = YourLANote
            _Priority = YourPriority
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
        End Sub

        Public Property LAId As Int64
        Public Property nEstelamId As Int64
        Public Property TurnId As Int64
        Public Property LAStatusId As Int64
        Public Property LANote As String
        Public Property Priority As Int16
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure
        Inherits R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure

        Public Sub New()
            MyBase.New()
            _TCTitle = String.Empty
            _GoodTitle = String.Empty
            _LoadTargetTitle = String.Empty
            _Truck = String.Empty
            _TruckDriverFullNameFamily = String.Empty
            _LoadAllocationStatus = String.Empty
            _StrDescription = String.Empty
            _LAStatusColor = Color.White
        End Sub

        Public Sub New(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure, YourTransportCompanyTitle As String, YourGoodTitle As String, YourLoadTargetTitle As String, YourTruck As String, YourTruckDriver As String, YourLoadAllocationStatus As String, YourStrDescription As String, YourLAStatusColor As Color)
            MyBase.New(YourNSS.LAId, YourNSS.nEstelamId, YourNSS.TurnId, YourNSS.LAStatusId, YourNSS.LANote, YourNSS.Priority, YourNSS.DateTimeMilladi, YourNSS.DateShamsi, YourNSS.Time, YourNSS.UserId)
            _TCTitle = YourTransportCompanyTitle
            _GoodTitle = YourGoodTitle
            _LoadTargetTitle = YourLoadTargetTitle
            _Truck = YourTruck
            _TruckDriverFullNameFamily = YourTruckDriver
            _LoadAllocationStatus = YourLoadAllocationStatus
            _StrDescription = YourStrDescription
            _LAStatusColor = YourLAStatusColor
        End Sub

        Public Property TCTitle As String
        Public Property GoodTitle As String
        Public Property LoadTargetTitle As String
        Public Property Truck As String
        Public Property TruckDriverFullNameFamily As String
        Public Property LoadAllocationStatus As String
        Public Property StrDescription As String
        Public Property LAStatusColor As Color

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure
        Inherits R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure

        Public Sub New()
            MyBase.New()
            LoadCapacitorLoadnEstelamId = String.Empty
            LoadCapacitorLoadTargetTitle = String.Empty
            LoadCapacitorLoadTargetTravelength = String.Empty
            LoadCapacitorLoadGoodTitle = String.Empty
            LoadCapacitorLoadnCarNumKol = String.Empty
            LoadCapacitorLoadnTonaj = String.Empty
            LoadCapacitorLoadStrPriceSug = String.Empty
            LoadCapacitorLoadStrDescription = String.Empty
            LoadCapacitorLoadStrBarName = String.Empty
            LoadCapacitorLoadLoaderTypeTitle = String.Empty
            LoadCapacitorLoadStrAddress = String.Empty
            LoadCapacitorLoaddDateElam = String.Empty
            LoadCapacitorLoaddTimeElam = String.Empty
            LoadCapacitorLoadStatusTitle = String.Empty
            LoadCapacitorLoadAHTitle = String.Empty
            LoadCapacitorLoadAHSGTitle = String.Empty
            TPTParams = String.Empty
            LoadingPlace = String.Empty
            DischargingPlace = String.Empty
            TransportCompanyTitle = String.Empty
            TransportCompanyTel = String.Empty
            TurnnEnterExitId = String.Empty
            TurnOtaghdarTurnNumber = String.Empty
            TurnEnterDate = String.Empty
            TurnEnterTime = String.Empty
            TurnStrDesc = String.Empty
            TurnStatusTitle = String.Empty
            TruckDriver = String.Empty
            TruckDriverSmartCardNo = String.Empty
            TruckDriverMobileNumber = String.Empty
            TruckLPString = String.Empty
            TruckSmartCardNo = String.Empty
            LoadPermissionDate = String.Empty
            LoadPermissionTime = String.Empty
            LoadPermissionRegisteringLocation = String.Empty
            LoadPermissionStatusTitle = String.Empty
            LoadAllocationId = String.Empty
            LoadAllocationStatusTitle = String.Empty
            LoadAllocationNote = String.Empty
            LoadAllocationDateShamsi = String.Empty
            LoadAllocationTime = String.Empty
            LoadAllocationStatusColor = Color.Red
            LoadAllocationPriority = String.Empty
        End Sub

        Public Sub New(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure, YourLoadCapacitorLoadnEstelamId As String, YourLoadCapacitorLoadTargetTitle As String, YourLoadCapacitorLoadTargetTravelength As String, YourLoadCapacitorLoadGoodTitle As String, YourLoadCapacitorLoadnCarNumKol As String, YourLoadCapacitorLoadnTonaj As String, YourLoadCapacitorLoadStrPriceSug As String, YourLoadCapacitorLoadStrDescription As String, YourLoadCapacitorLoadStrBarName As String, YourLoadCapacitorLoadLoaderTypeTitle As String, YourLoadCapacitorLoadStrAddress As String, YourLoadCapacitorLoaddDateElam As String, YourLoadCapacitorLoaddTimeElam As String, YourLoadCapacitorLoadStatusTitle As String, YourLoadCapacitorLoadAHTitle As String, YourLoadCapacitorLoadAHSGTitle As String, YourTPTParams As String, YourLoadingPlace As String, YourDischargingPlace As String, YourTransportCompanyTitle As String, YourTransportCompanyTel As String, YourTurnnEnterExitId As String, YourTurnOtaghdarTurnNumber As String, YourTurnEnterDate As String, YourTurnEnterTime As String, YourTurnStrDesc As String, YourTurnStatusTitle As String, YourTruckDriver As String, YourTruckDriverSmartCardNo As String, YourTruckDriverMobileNumber As String, YourTruckLPString As String, YourTruckSmartCardNo As String, YourLoadPermissionDate As String, YourLoadPermissionTime As String, YourLoadPermissionRegisteringLocation As String, YourLoadPermissionStatusTitle As String, YourLoadAllocationId As String, YourLoadAllocationStatusTitle As String, YourLoadAllocationNote As String, YourLoadAllocationDateShamsi As String, YourLoadAllocationTime As String, YourLoadAllocationStatusColor As Color, YourLoadAllocationPriority As String)
            MyBase.New(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(YourNSS.LAId, YourNSS.nEstelamId, YourNSS.TurnId, YourNSS.LAStatusId, YourNSS.LANote, YourNSS.Priority, YourNSS.DateTimeMilladi, YourNSS.DateShamsi, YourNSS.Time, YourNSS.UserId), YourTransportCompanyTitle, YourLoadCapacitorLoadGoodTitle, YourLoadCapacitorLoadTargetTitle, YourTruckLPString, YourTruckDriver, YourLoadAllocationStatusTitle, YourLoadCapacitorLoadStrDescription, YourLoadAllocationStatusColor)
            LoadCapacitorLoadnEstelamId = YourLoadCapacitorLoadnEstelamId
            LoadCapacitorLoadTargetTitle = YourLoadCapacitorLoadTargetTitle
            LoadCapacitorLoadTargetTravelength = LoadCapacitorLoadTargetTravelength
            LoadCapacitorLoadGoodTitle = YourLoadCapacitorLoadGoodTitle
            LoadCapacitorLoadnCarNumKol = YourLoadCapacitorLoadnCarNumKol
            LoadCapacitorLoadnTonaj = YourLoadCapacitorLoadnTonaj
            LoadCapacitorLoadStrPriceSug = YourLoadCapacitorLoadStrPriceSug
            LoadCapacitorLoadStrDescription = YourLoadCapacitorLoadStrDescription
            LoadCapacitorLoadStrBarName = YourLoadCapacitorLoadStrBarName
            LoadCapacitorLoadLoaderTypeTitle = YourLoadCapacitorLoadLoaderTypeTitle
            LoadCapacitorLoadStrAddress = YourLoadCapacitorLoadStrAddress
            LoadCapacitorLoaddDateElam = YourLoadCapacitorLoaddDateElam
            LoadCapacitorLoaddTimeElam = YourLoadCapacitorLoaddTimeElam
            LoadCapacitorLoadStatusTitle = YourLoadCapacitorLoadStatusTitle
            LoadCapacitorLoadAHTitle = YourLoadCapacitorLoadAHTitle
            LoadCapacitorLoadAHSGTitle = YourLoadCapacitorLoadAHSGTitle
            TPTParams = YourTPTParams
            LoadingPlace = YourLoadingPlace
            DischargingPlace = YourDischargingPlace
            TransportCompanyTitle = YourTransportCompanyTitle
            TransportCompanyTel = YourTransportCompanyTel
            TurnnEnterExitId = YourTurnnEnterExitId
            TurnOtaghdarTurnNumber = YourTurnOtaghdarTurnNumber
            TurnEnterDate = YourTurnEnterDate
            TurnEnterTime = YourTurnEnterTime
            TurnStrDesc = YourTurnStrDesc
            TurnStatusTitle = YourTurnStatusTitle
            TruckDriver = YourTruckDriver
            TruckDriverSmartCardNo = YourTruckDriverSmartCardNo
            TruckDriverMobileNumber = YourTruckDriverMobileNumber
            TruckLPString = YourTruckLPString
            TruckSmartCardNo = YourTruckSmartCardNo
            LoadPermissionDate = YourLoadPermissionDate
            LoadPermissionTime = YourLoadPermissionTime
            LoadPermissionRegisteringLocation = YourLoadPermissionRegisteringLocation
            LoadPermissionStatusTitle = YourLoadPermissionStatusTitle
            LoadAllocationId = YourLoadAllocationId
            LoadAllocationStatusTitle = YourLoadAllocationStatusTitle
            LoadAllocationNote = YourLoadAllocationNote
            LoadAllocationDateShamsi = YourLoadAllocationDateShamsi
            LoadAllocationTime = YourLoadAllocationTime
            LoadAllocationStatusColor = YourLoadAllocationStatusColor
            LoadAllocationPriority = YourLoadAllocationPriority
        End Sub

        Public Property LoadCapacitorLoadnEstelamId As String
        Public Property LoadCapacitorLoadTargetTitle As String
        Public Property LoadCapacitorLoadTargetTravelength As String
        Public Property LoadCapacitorLoadGoodTitle As String
        Public Property LoadCapacitorLoadnCarNumKol As String
        Public Property LoadCapacitorLoadnTonaj As String
        Public Property LoadCapacitorLoadStrPriceSug As String
        Public Property LoadCapacitorLoadStrDescription As String
        Public Property LoadCapacitorLoadStrBarName As String
        Public Property LoadCapacitorLoadLoaderTypeTitle As String
        Public Property LoadCapacitorLoadStrAddress As String
        Public Property LoadCapacitorLoaddDateElam() As String
        Public Property LoadCapacitorLoaddTimeElam() As String
        Public Property LoadCapacitorLoadStatusTitle As String
        Public Property LoadCapacitorLoadAHTitle() As String
        Public Property LoadCapacitorLoadAHSGTitle() As String
        Public Property TPTParams As String
        Public Property LoadingPlace As String
        Public Property DischargingPlace As String
        Public Property TransportCompanyTitle As String
        Public Property TransportCompanyTel As String
        Public Property TurnnEnterExitId As String
        Public Property TurnOtaghdarTurnNumber As String
        Public Property TurnEnterDate As String
        Public Property TurnEnterTime As String
        Public Property TurnStrDesc As String
        Public Property TurnStatusTitle As String
        Public Property TruckDriver As String
        Public Property TruckDriverSmartCardNo As String
        Public Property TruckDriverMobileNumber As String
        Public Property TruckLPString As String
        Public Property TruckSmartCardNo As String
        Public Property LoadPermissionDate As String
        Public Property LoadPermissionTime As String
        Public Property LoadPermissionRegisteringLocation As String
        Public Property LoadPermissionStatusTitle As String
        Public Property LoadAllocationId As String
        Public Property LoadAllocationStatusTitle As String
        Public Property LoadAllocationNote As String
        Public Property LoadAllocationDateShamsi As String
        Public Property LoadAllocationTime As String
        Public Property LoadAllocationStatusColor As Color
        Public Property LoadAllocationPriority As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
        Private _DateTime As New R2DateTime
        Private InstanceLogging As New R2CoreInstanceLoggingManager
        Private InstanceSiteIsBusy = New R2CoreSiteIsBusyManager

        Public Function GetLoadAllocationsforTruckDriver(Optional YourSoftwareUserId As Int64 = Int64.MinValue, Optional YourTurnId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
            Try
                Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsParametersManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
   "Declare @LastTurnId int
    Select Top 1 @LastTurnId=Turns.nEnterExitId 
	from  R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
	   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
       Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
       Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
       Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
    Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2 
          and (Turns.TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " or Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ")	
          and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
    Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc
    Select LoadCapacitor.TPTParams,LoadingPlaces.LADPlaceTitle as LoadingPlace,DischargingPlaces.LADPlaceTitle as DischargingPlace,LoadCapacitor.nEstelamID as LoadCapacitorLoadnEstelamId,Targets.strCityName as LoadCapacitorLoadTargetTitle,Products.strGoodName as  LoadCapacitorLoadGoodTitle,LoadCapacitor.nCarNumKol as LoadCapacitorLoadnCarNumKol,LoadCapacitor.nTonaj as LoadCapacitorLoadnTonaj,LoadCapacitor.strPriceSug as LoadCapacitorLoadStrPriceSug,LoadCapacitor.strDescription as LoadCapacitorLoadStrDescription,LoaderTypes.LoaderTypeTitle as LoadCapacitorLoadLoaderTypeTitle,
	       LoadCapacitor.strAddress as LoadCapacitorLoadStrAddress,LoadCapacitor.strBarName as LoadCapacitorLoadstrBarName,LoadCapacitor.dTimeElam as LoadCapacitorLoaddTimeElam,LoadCapacitor.dDateElam  as  LoadCapacitorLoaddDateElam,LoadCapacitorLoadStatuses.LoadStatusName as LoadCapacitorLoadStatusTitle,AHs.AHTitle as LoadCapacitorLoadAHTitle,AHSGs.AHSGTitle as LoadCapacitorLoadAHSGTitle,TransportCompanies.TCTitle as  TransportCompanyTitle,TransportCompanies.TCTel as  TransportCompanyTel,Turns.nEnterExitId as  TurnnEnterExitId,
	       Turns.OtaghdarTurnNumber as TurnOtaghdarTurnNumber,Turns.strEnterDate as TurnEnterDate,Turns.strEnterTime as TurnEnterTime,Turns.strDesc as TurnStrDesc,TurnStatuses.TurnStatusTitle as TurnStatusTitle,Persons.strPersonFullName as TruckDriver,Drivers.strSmartcardNo  as TruckDriverSmartCardNo,Persons.strIDNO as TruckDriverMobileNumber,Cars.strCarNo+'-'+Cars.strCarSerialNo as TruckLPString,Cars.strBodyNo as TruckSmartCardNo,Turns.strExitDate as LoadPermissionDate,
           Turns.strExitTime as LoadPermissionTime,LoadPermissionStatuses.LoadPermissionStatusTitle as LoadPermissionStatusTitle,Turns.strBarnameNo as LoadPermissionRegisteringLocation,LoadAllocations.LAId as  LoadAllocationId,LoadAllocationStatuses.LoadAllocationStatusTitle as  LoadAllocationStatusTitle,LoadAllocations.LANote as LoadAllocationNote,LoadAllocations.DateShamsi as LoadAllocationDateShamsi,LoadAllocations.Time as LoadAllocationTime,
           LoadAllocationStatuses.LoadAllocationStatusColor as LoadAllocationStatusColor,LoadAllocations.Priority as LoadAllocationPriority,LoadAllocations.LAStatusId as LoadAllocationStatusId,LoadAllocations.UserId as LoadAllocationUserId,LoadAllocations.DateTimeMilladi as LoadAllocationDateTimeMilladi,Cast(Targets.nDistance/25 as int) as TargetTravellength   
    from dbtransport.dbo.tbEnterExit as Turns
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
       Inner Join dbtransport.dbo.tbElam as LoadCapacitor On LoadAllocations.nEstelamId=LoadCapacitor.nEstelamID 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On LoadCapacitor.LoadingPlaceId=LoadingPlaces.LADPlaceId 
	   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On LoadCapacitor.DischargingPlaceId=DischargingPlaces.LADPlaceId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs on LoadCapacitor.AHSGId=AHSGs.AHSGId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadCapacitorLoadStatuses On LoadCapacitor.LoadStatus=LoadCapacitorLoadStatuses.LoadStatusId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On LoadCapacitor.nTruckType=LoaderTypes.LoaderTypeId 
       Inner Join dbtransport.dbo.tbProducts as Products On LoadCapacitor.nBarcode=Products.strGoodCode 
       Inner Join dbtransport.dbo.tbCity as Targets On LoadCapacitor.nCityCode=Targets.nCityCode 
       Inner Join dbtransport.dbo.TbCar as Cars on Turns.strCardno=Cars.nIDCar 
       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
       Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
       Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On LoadCapacitor.nCompCode=TransportCompanies.TCId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatuses On Turns.LoadPermissionStatus=LoadPermissionStatuses.LoadPermissionStatusId 
    Where Turns.nEnterExitId=" & IIf(YourTurnId = Int64.MinValue, "@LastTurnId", YourTurnId) & " and (LoadAllocations.LAStatusId=1 or LoadAllocations.LAStatusId=2 or LoadAllocations.LAStatusId=3 or LoadAllocations.LAStatusId=5) and CarAndPersons.snRelation=2 and AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0 and LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "'  
          and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
    Order By CarAndPersons.nIDCarAndPerson Desc,LoadAllocations.LAStatusId Asc,LoadAllocations.Priority Asc", 0, DS, New Boolean)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure
                    NSS.LAId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.nEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.TurnId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.LAStatusId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"))
                    NSS.LANote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.Priority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), Int16.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    NSS.DateTimeMilladi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"), DBNull.Value), Now, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"))
                    NSS.DateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.Time = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.UserId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"))
                    NSS.TCTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.GoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.Truck = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckDriverFullNameFamily = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.LoadAllocationStatus = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.StrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LAStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadCapacitorLoadnEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.LoadCapacitorLoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.LoadCapacitorLoadTargetTravelength = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TargetTravellength"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TargetTravellength"))
                    NSS.LoadCapacitorLoadGoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadCapacitorLoadnCarNumKol = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"))
                    NSS.LoadCapacitorLoadnTonaj = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"))
                    NSS.LoadCapacitorLoadStrPriceSug = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"))
                    NSS.LoadCapacitorLoadStrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LoadCapacitorLoadStrBarName = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"))
                    NSS.LoadCapacitorLoadLoaderTypeTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"))
                    NSS.LoadCapacitorLoadStrAddress = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"))
                    NSS.LoadCapacitorLoaddDateElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"))
                    NSS.LoadCapacitorLoaddTimeElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"))
                    NSS.LoadCapacitorLoadStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"))
                    NSS.LoadCapacitorLoadAHTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"))
                    NSS.LoadCapacitorLoadAHSGTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"))
                    NSS.TPTParams = InstanceTransportTariffsParameters.GetTransportTariffsComposit(DS.Tables(0).Rows(Loopx).Item("TPTParams"))
                    NSS.LoadingPlace = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadingPlace"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadingPlace"))
                    NSS.DischargingPlace = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("DischargingPlace"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("DischargingPlace"))
                    NSS.TransportCompanyTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.TransportCompanyTel = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"))
                    NSS.TurnnEnterExitId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.TurnOtaghdarTurnNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"))
                    NSS.TurnEnterDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"))
                    NSS.TurnEnterTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"))
                    NSS.TurnStrDesc = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"))
                    NSS.TurnStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"))
                    NSS.TruckDriver = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.TruckDriverSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"))
                    NSS.TruckDriverMobileNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"))
                    NSS.TruckLPString = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"))
                    NSS.LoadPermissionDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"))
                    NSS.LoadPermissionTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"))
                    NSS.LoadPermissionRegisteringLocation = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"))
                    NSS.LoadPermissionStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"))
                    NSS.LoadAllocationId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.LoadAllocationStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.LoadAllocationNote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.LoadAllocationDateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.LoadAllocationTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.LoadAllocationStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadAllocationPriority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As LoadAllocationNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadPermissionsViaLicensePlate(YourLPPelak As String, YourLPSerial As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim NSSDirtyTruck = New R2CoreTransportationAndLoadNotificationStandardTruckStructure(New R2StandardCarStructure(Nothing, Nothing, YourLPPelak, YourLPSerial, Nothing), Nothing)
                NSSDirtyTruck = InstanceTrucks.GetNSSTruckWithLicensePlate(NSSDirtyTruck)

                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select Top 100 LoadCapacitor.nEstelamID as LoadCapacitorLoadnEstelamId,Targets.strCityName as LoadCapacitorLoadTargetTitle,Products.strGoodName as  LoadCapacitorLoadGoodTitle,LoadCapacitor.nCarNumKol as LoadCapacitorLoadnCarNumKol,LoadCapacitor.nTonaj as LoadCapacitorLoadnTonaj,LoadCapacitor.strPriceSug as LoadCapacitorLoadStrPriceSug,LoadCapacitor.strDescription as LoadCapacitorLoadStrDescription,LoaderTypes.LoaderTypeTitle as LoadCapacitorLoadLoaderTypeTitle,
                        	      LoadCapacitor.strAddress as LoadCapacitorLoadStrAddress,LoadCapacitor.strBarName as LoadCapacitorLoadstrBarName,LoadCapacitor.dTimeElam as LoadCapacitorLoaddTimeElam,LoadCapacitor.dDateElam  as  LoadCapacitorLoaddDateElam,LoadCapacitorLoadStatuses.LoadStatusName as LoadCapacitorLoadStatusTitle,AHs.AHTitle as LoadCapacitorLoadAHTitle,AHSGs.AHSGTitle as LoadCapacitorLoadAHSGTitle,TransportCompanies.TCTitle as  TransportCompanyTitle,TransportCompanies.TCTel as  TransportCompanyTel,Turns.nEnterExitId as  TurnnEnterExitId,
	                              Turns.OtaghdarTurnNumber as TurnOtaghdarTurnNumber,Turns.strEnterDate as TurnEnterDate,Turns.strEnterTime as TurnEnterTime,Turns.strDesc as TurnStrDesc,TurnStatuses.TurnStatusTitle as TurnStatusTitle,Turns.strDriverName as TruckDriver,Drivers.strSmartcardNo  as TruckDriverSmartCardNo,Persons.strIDNO as TruckDriverMobileNumber,Cars.strCarNo+'-'+Cars.strCarSerialNo as TruckLPString,Cars.strBodyNo as TruckSmartCardNo,Turns.strExitDate as LoadPermissionDate,
                                  Turns.strExitTime as LoadPermissionTime,LoadPermissionStatuses.LoadPermissionStatusTitle as LoadPermissionStatusTitle,Turns.strBarnameNo as LoadPermissionRegisteringLocation,LoadAllocations.LAId as  LoadAllocationId,LoadAllocationStatuses.LoadAllocationStatusTitle as  LoadAllocationStatusTitle,LoadAllocations.LANote as LoadAllocationNote,LoadAllocations.DateShamsi as LoadAllocationDateShamsi,LoadAllocations.Time as LoadAllocationTime,
                                  LoadAllocationStatuses.LoadAllocationStatusColor as LoadAllocationStatusColor,LoadAllocations.Priority as LoadAllocationPriority,LoadAllocations.LAStatusId as LoadAllocationStatusId,LoadAllocations.UserId as LoadAllocationUserId,LoadAllocations.DateTimeMilladi as LoadAllocationDateTimeMilladi,Cast(Targets.nDistance/25 as int) as TargetTravellength   
                    from dbtransport.dbo.tbEnterExit as Turns
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
                      Inner Join dbtransport.dbo.tbElam as LoadCapacitor On LoadAllocations.nEstelamId=LoadCapacitor.nEstelamID 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs on LoadCapacitor.AHSGId=AHSGs.AHSGId 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadCapacitorLoadStatuses On LoadCapacitor.LoadStatus=LoadCapacitorLoadStatuses.LoadStatusId 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On LoadCapacitor.nTruckType=LoaderTypes.LoaderTypeId 
                      Inner Join dbtransport.dbo.tbProducts as Products On LoadCapacitor.nBarcode=Products.strGoodCode 
                      Inner Join dbtransport.dbo.tbCity as Targets On LoadCapacitor.nCityCode=Targets.nCityCode 
                      Inner Join dbtransport.dbo.TbCar as Cars on Turns.strCardno=Cars.nIDCar 
                      Inner Join (Select Top 1 * from dbtransport.dbo.TbCarAndPerson as CarAndPerson
                                  Where  CarAndPerson.nIDCar=" & NSSDirtyTruck.NSSCar.nIdCar & "
                                  Order By CarAndPerson.RelationTimeStamp Desc) as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar  
                      Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
                      Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On LoadCapacitor.nCompCode=TransportCompanies.TCId 
                      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatuses On Turns.LoadPermissionStatus=LoadPermissionStatuses.LoadPermissionStatusId 
                    Where Turns.strCardno=" & NSSDirtyTruck.NSSCar.nIdCar & " and LoadAllocations.LAStatusId=2 and CarAndPersons.snRelation=2 and AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0   
                    Order By LoadAllocations.DateTimeMilladi desc", 120, DS, New Boolean)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure
                    NSS.LAId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.nEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.TurnId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.LAStatusId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"))
                    NSS.LANote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.Priority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), Int16.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    NSS.DateTimeMilladi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"), DBNull.Value), Now, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"))
                    NSS.DateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.Time = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.UserId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"))
                    NSS.TCTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.GoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.Truck = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckDriverFullNameFamily = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.LoadAllocationStatus = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.StrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LAStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadCapacitorLoadnEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.LoadCapacitorLoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.LoadCapacitorLoadTargetTravelength = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TargetTravellength"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TargetTravellength"))
                    NSS.LoadCapacitorLoadGoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadCapacitorLoadnCarNumKol = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"))
                    NSS.LoadCapacitorLoadnTonaj = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"))
                    NSS.LoadCapacitorLoadStrPriceSug = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"))
                    NSS.LoadCapacitorLoadStrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LoadCapacitorLoadStrBarName = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"))
                    NSS.LoadCapacitorLoadLoaderTypeTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"))
                    NSS.LoadCapacitorLoadStrAddress = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"))
                    NSS.LoadCapacitorLoaddDateElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"))
                    NSS.LoadCapacitorLoaddTimeElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"))
                    NSS.LoadCapacitorLoadStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"))
                    NSS.LoadCapacitorLoadAHTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"))
                    NSS.LoadCapacitorLoadAHSGTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"))
                    NSS.TransportCompanyTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.TransportCompanyTel = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"))
                    NSS.TurnnEnterExitId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.TurnOtaghdarTurnNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"))
                    NSS.TurnEnterDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"))
                    NSS.TurnEnterTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"))
                    NSS.TurnStrDesc = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"))
                    NSS.TurnStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"))
                    NSS.TruckDriver = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.TruckDriverSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"))
                    NSS.TruckDriverMobileNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"))
                    NSS.TruckLPString = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"))
                    NSS.LoadPermissionDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"))
                    NSS.LoadPermissionTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"))
                    NSS.LoadPermissionRegisteringLocation = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"))
                    NSS.LoadPermissionStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"))
                    NSS.LoadAllocationId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.LoadAllocationStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.LoadAllocationNote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.LoadAllocationDateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.LoadAllocationTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.LoadAllocationStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadAllocationPriority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As LoadAllocationNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function LoadAllocationRegistering(YournEstelamId As Int64, YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourRequesterId As Int64, YourLoadAllocationIdRequested As Boolean, YourImmediately As Boolean) As Int64
        '    Dim CmdSql As New SqlClient.SqlCommand
        '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
        '    Try
        '        Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
        '        Dim InstanceAnnouncementHall = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
        '        Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
        '        Dim InstanceTruck = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
        '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
        '        Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
        '        Dim InstanceDriverSelfDeclaration = New DriverSelfDeclaration.R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationManager
        '        Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
        '        Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
        '        Dim InstanceLoadCapacitorLoadOtherThanManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadOtherThanManipulationManager
        '        Dim InstanceBlackList = New R2CoreParkingSystemInstanceBlackListManager
        '        Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager
        '        Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager

        '        Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, YourImmediately)
        '        Dim NSSAnnouncementsubGroup = InstanceAnnouncementHall.GetNSSAnnouncementsubGroup(NSSLoadCapacitorLoad.AHSGId)
        '        Dim NSSAnnouncementHall = InstanceAnnouncementHall.GetNSSAnnouncementHall(NSSLoadCapacitorLoad.AHId)
        '        If NSSAnnouncementsubGroup.Active = 0 Then Throw New AnnouncementsubGroupUnActiveException
        '        Dim NSSTruck = InstanceTruck.GetNSSTruck(YourNSSTurn, YourImmediately)

        '        'کنترل مجوز دسترسی رکستر برای تخصیص بار برای بار با زیرگروه مورد نظر
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequestersAllowAHSGIdLoadAllocationRegistering, YourRequesterId, NSSAnnouncementsubGroup.AHSGId) Then Throw New RequesterHasNotPermissionforLoadAllocationRegisteringException

        '        'کنترل مجوز دسترسی رکستر برای تخصیص بار با توجه به وضعیت بار
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterAllowLoadAllocationByLoadStatus, YourRequesterId, NSSLoadCapacitorLoad.LoadStatus) Then Throw New RequesterHasNotPermissionforLoadAllocationRegisteringException

        '        'کنترل لیست سیاه
        '        Dim NSSBlackList As R2StandardBlackListStructure = Nothing
        '        Dim HasBlackList = InstanceBlackList.HasCarBlackList(NSSTruck.NSSCar, NSSBlackList)
        '        If HasBlackList Then Throw New LoadAllocationNotAllowedBecauseCarHasBlackListException(NSSBlackList.StrDesc)

        '        'بررسی بار فردا
        '        If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New UnableAllocatingTommorowLoadException

        '        'بررسی این که تخصیص بار برای زیرگروه مورد نظر فعال باشد
        '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
        '        Dim AllAHSGConfig As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadAllocationSetting, NSSAnnouncementHall.AHId), ";")
        '        Dim AHSGIdConfig As Boolean = Split(Mid(AllAHSGConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAHSGConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ":")(0)
        '        If Not AHSGIdConfig Then Throw New LoadAllocationNotAllowedBecuaseAHSGLoadAllocationIsUnactiveException

        '        'کنترل مجوز کنترل تایمینگ در تخصیص بار با توجه به وضعیت بار
        '        If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.LoadAllocationUseTimeHandlingByLoadStatus, NSSLoadCapacitorLoad.LoadStatus, 0) Then
        '            'آیا زمان تخصیص بار برای زیرگروه مورد نظر فرارسیده است
        '            If InstanceTiming.IsTimingActive(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId) Then
        '                If InstanceTiming.GetTiming(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId, _DateTime.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
        '                    Throw New LoadAllocationTimeNotReachedException
        '                End If
        '            Else
        '                'کنترل تایمینگ بار در مخزن بار
        '                If Not InstanceLoadCapacitorLoad.IsLoadCapacitorLoadTimingReadeyforLoadAllocationRegistering(NSSLoadCapacitorLoad, NSSAnnouncementHall, NSSAnnouncementsubGroup) Then Throw New LoadAllocationRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException
        '            End If
        '            'Else
        '            '    'کنترل تایمینگ بار در مخزن بار
        '            '    If Not InstanceLoadCapacitorLoad.IsLoadCapacitorLoadTimingReadeyforLoadAllocationRegistering(NSSLoadCapacitorLoad, NSSAnnouncementHall, NSSAnnouncementsubGroup) Then Throw New LoadAllocationRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException
        '        Else
        '            'کنترل و جلوگیری از تخصیص بار اگر برای نوبت مجوز صادر شده
        '            Dim Da As New SqlClient.SqlDataAdapter : Dim DSAllocate As New DataSet
        '            Da.SelectCommand = New SqlClient.SqlCommand("Select LAId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Where TurnId=" & YourNSSTurn.nEnterExitId & " and LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & "")
        '            Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
        '            If Da.Fill(DSAllocate) <> 0 Then Throw New LastLoadPermissionIssuedforThisTurnException
        '        End If

        '        'کنترل وضعیت بار در مخزن بار
        '        'بارهای با وضعیت ثبت شده ، خط آزاد و رسوب شده قابل تخصیص هستند
        '        If Not InstanceLoadCapacitorLoad.IsLoadCapacitorLoadStatusReadeyforLoadAllocationRegistering(NSSLoadCapacitorLoad) Then Throw New LoadAllocationRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException

        '        ''کنترل تطابق تسلسل نوبت ناوگان با گروه سالنی بار
        '        'If IsLoadCapacitorLoadAHSGMatchWithSequentialTurnOfTurn(YournEstelamId, YourNSSTurn.nEnterExitId, YourImmediately) = False Then Throw New LoadCapacitorLoadLoaderTypeViaSequentialTurnOfTurnNotAllowedException

        '        'کنترل تطابق ناوگان با بار انتخاب شده
        '        If InstanceTruck.GetAnnouncementsubGroups(NSSTruck, YourImmediately).Where(Function(x) x = NSSLoadCapacitorLoad.AHSGId).Count = 0 Then
        '            Throw New LoadCapacitorLoadAHSGIdViaTruckAHSGIdNotAllowedException
        '        End If

        '        'کنترل وضعیت نوبت
        '        If Not InstanceTurns.IsTurnReadeyforLoadAllocationRegistering(YourNSSTurn) Then Throw New LoadAllocationRegisteringFailedBecauseTurnIsNotReadyException

        '        'کنترل تکراری نبودن تخصیص بار - تخصیص فعال
        '        'If ExistRegisteredLoadAllocation(YournEstelamId, YourNSSTurn.nEnterExitId) Then Throw New RegisteredLoadAllocationIsRepetitiousException

        '        'کنترل تعداد مجوز بارگیری آزاد شده
        '        If InstanceLoadPermission.GetTotalLoadPermissions(NSSTruck) = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 5) Then Throw New TruckTotalLoadPermissionReachedException

        '        'کنترل ثبت اطلاعات خوداظهاری توسط راننده
        '        If InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.IndigenousTrucks, 0) Then
        '            If (YourRequesterId <> 4) And (Not InstanceTruckNativeness.IsTruckIndigenous(NSSTruck)) Then If Not (InstanceDriverSelfDeclaration.GetDeclarations(NSSTruck, False).Count > 0) Then Throw New DSDsNotFoundException
        '        End If

        '        'کنترل گذشت مدت زمان معین از زمان رسوب بار و مجوز برای رکستر خاص
        '        Try
        '            If (NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented) And (NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined) Then Exit Try
        '            If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanAllocateSedimentedLoadInTimeRange, YourRequesterId, 0) Then Exit Try
        '            Dim InstanceLoadSedimentation = New R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManager
        '            'موقتا غیر فعال شد 14040409
        '            'If InstanceLoadSedimentation.HowManyMinutesPassedfromSedimentation(NSSLoadCapacitorLoad) > InstanceLoadSedimentation.HowManyMinutesMustPassedfromSedimentation(NSSLoadCapacitorLoad) Then
        '            '    If YourRequesterId <> R2CoreTransportationAndLoadNotificationRequesters.WcLoadCapacitorLoadAllocationLoadPermissionIssue Then Throw New RequesterCanNotAllocateSedimentedLoadInTimeRangeException
        '            'Else
        '            '    If YourRequesterId <> R2CoreTransportationAndLoadNotificationRequesters.ATISRestfullLoadAllocationRegisteringAgent Then Throw New RequesterCanNotAllocateSedimentedLoadInTimeRangeException
        '            'End If
        '        Catch ex As LoadIsNotSedimentedException
        '        Catch ex As Exception
        '            Throw ex
        '        End Try

        '        CmdSql.Connection.Open()
        '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
        '        Dim LAIdNew As Int64 = 0
        '        If YourLoadAllocationIdRequested Then
        '            CmdSql.CommandText = "Select Top 1 LAId From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations with (tablockx) Order By LAId Desc"
        '            CmdSql.ExecuteNonQuery()
        '            CmdSql.CommandText = "Select IDENT_CURRENT('R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations') "
        '            LAIdNew = CmdSql.ExecuteScalar() + 1
        '        End If
        '        CmdSql.CommandText = "Select Top 1 LoadAllocations.Priority from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
        '                              Where LoadAllocations.TurnId=" & YourNSSTurn.nEnterExitId & " and LoadAllocations.LAStatusId=1 and LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "'
        '                              Order By LoadAllocations.Priority Desc"
        '        Dim Obj = CmdSql.ExecuteScalar
        '        Dim Priority As Int16 = IIf(Object.Equals(Obj, Nothing), 1, Convert.ToInt16(Obj) + 1)
        '        Dim CurrentDateTime = _DateTime.GetCurrentDateTime
        '        CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations(nEstelamId,TurnId,LAStatusId,LANote,Priority,DateTimeMilladi,DateShamsi,Time,UserId) Values(" & YournEstelamId & "," & YourNSSTurn.nEnterExitId & "," & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & ",''," & Priority & ",'" & CurrentDateTime.DateTimeMilladiFormated & "','" & CurrentDateTime.DateShamsiFull & "','" & CurrentDateTime.Time & "'," & YourUserNSS.UserId & ")"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
        '        InstanceTurns.LoadAllocationRegistering(YourNSSTurn)
        '        InstanceLoadCapacitorLoadOtherThanManipulation.LoadCapacitorLoadAllocating(NSSLoadCapacitorLoad, YourUserNSS)

        '        RePrioritize(YourNSSTurn, CurrentDateTime.DateShamsiFull)
        '        Return LAIdNew
        '    Catch ex As TruckNotFoundException
        '        If CmdSql.Connection.State <> ConnectionState.Closed Then
        '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '        End If
        '        If InstanceLogging.GetNSSLogType(R2CoreTransportationAndLoadNotificationLogType.Warn).Active Then
        '            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreTransportationAndLoadNotificationLogType.Warn, InstanceLogging.GetNSSLogType(R2CoreTransportationAndLoadNotificationLogType.Warn).LogTitle, "TurnId=" + YourNSSTurn.nEnterExitId.ToString, String.Empty, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladi(), Nothing))
        '        End If
        '        Throw ex
        '    Catch ex As Exception When TypeOf ex Is AnnouncementsubGroupUnActiveException _
        '        OrElse TypeOf ex Is AnnouncementsubGroupRelationTruckNotExistException _
        '        OrElse TypeOf ex Is AnnouncementsubGroupNotFoundException _
        '        OrElse TypeOf ex Is LoadAllocationRegisteringReachedEndTimeException _
        '        OrElse TypeOf ex Is LoadAllocationMaximumAllowedNumberReachedException _
        '        OrElse TypeOf ex Is LoadCapacitorLoadAHSGIdViaTruckAHSGIdNotAllowedException _
        '        OrElse TypeOf ex Is LoadAllocationRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException _
        '        OrElse TypeOf ex Is LoadAllocationRegisteringFailedBecauseTurnIsNotReadyException _
        '        OrElse TypeOf ex Is LoadCapacitorLoadLoaderTypeViaSequentialTurnOfTurnNotAllowedException _
        '        OrElse TypeOf ex Is LoadAllocationNotAllowedBecuaseAHSGLoadAllocationIsUnactiveException _
        '        OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
        '        OrElse TypeOf ex Is RegisteredLoadAllocationIsRepetitiousException _
        '        OrElse TypeOf ex Is RequesterHasNotPermissionforLoadAllocationRegisteringException _
        '        OrElse TypeOf ex Is LoadAllocationNotAllowedBecauseCarHasBlackListException _
        '        OrElse TypeOf ex Is TimingNotReachedException _
        '        OrElse TypeOf ex Is TurnNotFoundException _
        '        OrElse TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
        '        OrElse TypeOf ex Is UnableAllocatingTommorowLoadException _
        '        OrElse TypeOf ex Is TruckTotalLoadPermissionReachedException _
        '        OrElse TypeOf ex Is LastLoadPermissionIssuedforThisTurnException _
        '        OrElse TypeOf ex Is RequesterCanNotAllocateSedimentedLoadInTimeRangeException _
        '        OrElse TypeOf ex Is LoadAllocationTimeNotReachedException _
        '        OrElse TypeOf ex Is DSDsNotFoundException

        '        If CmdSql.Connection.State <> ConnectionState.Closed Then
        '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '        End If
        '        Throw ex
        '    Catch ex As Exception
        '        If CmdSql.Connection.State <> ConnectionState.Closed Then
        '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '        End If
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Sub LoadAllocationCancelling(YourLoadAllocationId As Int64, YourCancellingStatus As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceLoadCapacitorLoadOtherThanManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadOtherThanManipulationManager

                Dim NSSLoadAllocation = GetNSSLoadAllocation(YourLoadAllocationId)
                Dim NSSTurn = InstanceTurns.GetNSSTurn(NSSLoadAllocation.TurnId)
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(NSSLoadAllocation.nEstelamId, False)
                'آیا زمان تخصیص بار و کنسلی تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If YourCancellingStatus = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.CancelledUser Then
                    If InstanceAnnouncementTiming.IsTimingActive(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId) Then
                        If InstanceAnnouncementTiming.GetTiming(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId, _DateTime.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                            Throw New TimingNotReachedException
                        End If
                    End If
                End If
                'کنترل وضعیت تخصیص بار
                If Not NSSLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered Then Throw New LoadAllocationCancellingNotAllowedBecauseLoadAllocationStatusException
                'محدودیت نوبت در کنسلی تخصیص
                If Not InstanceTurns.IsTurnReadyforLoadAllocationCancelling(NSSTurn) Then Throw New LoadAllocationCancellingNotAllowedBecauseTurnStatusException
                Dim NSSLoadAllocationStatus = GetNSSLoadAllocationStatus(YourCancellingStatus)
                Dim DMilladiFormated = _DateTime.GetCurrentDateTimeMilladiFormated()
                Dim DShamsiFull = _DateTime.ConvertToShamsiDateFull(DMilladiFormated)
                Dim TimeofDate = _DateTime.GetTimeOfDate(DMilladiFormated)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                                        Set LAStatusId=" & YourCancellingStatus & ",LANote='" & NSSLoadAllocationStatus.LoadAllocationStatusTitle & "',
                                            DateTimeMilladi='" & DMilladiFormated & "',DateShamsi='" & DShamsiFull & "',Time='" & TimeofDate & "'  
                                      Where LAId=" & YourLoadAllocationId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                InstanceTurns.LoadAllocationCancelling(NSSTurn)
                InstanceLoadCapacitorLoadOtherThanManipulation.LoadCapacitorLoadAllocationCancelling(NSSLoadAllocation.nEstelamId, YourUserNSS)
                RePrioritize(NSSTurn, DShamsiFull)
            Catch ex As Exception When TypeOf ex Is TimingNotReachedException _
                                OrElse TypeOf ex Is LoadAllocationNotFoundException _
                                OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException _
                                OrElse TypeOf ex Is TurnNotFoundException _
                                OrElse TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
                                OrElse TypeOf ex Is LoadAllocationCancellingNotAllowedBecauseLoadAllocationStatusException _
                                OrElse TypeOf ex Is LoadAllocationCancellingNotAllowedBecauseTurnStatusException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSLoadAllocationStatus(YourLoadAllocationStatusId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses Where LoadAllocationStatusId=" & YourLoadAllocationStatusId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadAllocationStatusNotFoundException
                Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure
                NSS.LoadAllocationStatusId = YourLoadAllocationStatusId
                NSS.LoadAllocationStatusTitle = DS.Tables(0).Rows(0).Item("LoadAllocationStatustitle").TRIM
                NSS.LoadAllocationStatusColor = DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor").TRIM
                NSS.ViewFlag = DS.Tables(0).Rows(0).Item("ViewFlag")
                Return NSS
            Catch ex As LoadAllocationStatusNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsLoadCapacitorLoadAHSGMatchWithSequentialTurnOfTurn(YournEstelamId As Int64, YournEnterExitId As Int64, YourImmediately As Boolean)
            Try
                Dim Ds As New DataSet
                Dim Da As New SqlClient.SqlDataAdapter
                If YourImmediately Then
                    Da.SelectCommand = New SqlCommand(
                        "Select * from dbtransport.dbo.tbElam as LoadCapacitor
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On LoadCapacitor.AHSGId=AHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT oN AH.AHId=AHRSeqT.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On AHRSeqT.SeqTId=SeqT.SeqTId
                            Inner Join dbtransport.dbo.tbEnterExit as Turns On SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS=Substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS
                        Where AHRAHSG.RelationActive=1 and AHRSeqT.RelationActive=1 and Turns.nEnterExitId=" & YournEnterExitId & " and LoadCapacitor.nEstelamID=" & YournEstelamId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(Ds) = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select * from dbtransport.dbo.tbElam as LoadCapacitor
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On LoadCapacitor.AHSGId=AHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT oN AH.AHId=AHRSeqT.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On AHRSeqT.SeqTId=SeqT.SeqTId
                            Inner Join dbtransport.dbo.tbEnterExit as Turns On SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS=Substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS
                        Where AHRAHSG.RelationActive=1 and AHRSeqT.RelationActive=1 and Turns.nEnterExitId=" & YournEnterExitId & " and LoadCapacitor.nEstelamID=" & YournEstelamId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ExistRegisteredLoadAllocation(YournEstelamId As Int64, YourTurnId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.nEstelamId=" & YournEstelamId & " and LoadAllocations.TurnId=" & YourTurnId & " and 
                            LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " and 
                            LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "'", 0, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalNumberOfRegisteredLoadAllocations(YourTurnId As Int64) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Count(*) As Counting from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.TurnId=" & YourTurnId & " and LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " and LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "'", 0, DS, New Boolean)
                Return DS.Tables(0).Rows(0).Item("Counting")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnIdfromLoadAllocationId(YourLoadAllocationId As Int64) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                        "Select LoadAllocations.TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations 
                                         Where LoadAllocations.LAId=" & YourLoadAllocationId & "", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                Return DS.Tables(0).Rows(0).Item("TurnId")
            Catch exx As LoadAllocationNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSLoadAllocation(YourLoadAllocationId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                        "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.LAId=" & YourLoadAllocationId & "", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(0).Item("LAId"), DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("TurnId"), DS.Tables(0).Rows(0).Item("LAStatusId"), DS.Tables(0).Rows(0).Item("LANote").trim, DS.Tables(0).Rows(0).Item("Priority"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("Truck"), DS.Tables(0).Rows(0).Item("StrPersonFullName"), DS.Tables(0).Rows(0).Item("LoadAllocationStatusTitle"), DS.Tables(0).Rows(0).Item("StrDescription"), Color.FromName(DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor")))
            Catch exx As LoadAllocationNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub IncreasePriority(YourLAId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager

                Dim NSSThisLoadAllocation = GetNSSLoadAllocation(YourLAId)
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(NSSThisLoadAllocation.nEstelamId, False)
                Dim NSSTurn = InstanceTurns.GetNSSTurn(NSSThisLoadAllocation.TurnId)
                'آیا زمان تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If InstanceTiming.IsTimingActive(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId) Then
                    If InstanceTiming.GetTiming(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId, _DateTime.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                        Throw New TimingNotReachedException
                    End If
                End If
                'کنترل وضعیت تخصیص
                If Not NSSThisLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered Then Throw New LoadAllocationChangePriorityNotAllowedBecauseLoadAllocationStatusException
                'محدودیت نوبت در تغییر اولویت تخصیص
                If Not InstanceTurns.IsTurnReadyforLoadAllocationChangePriority(NSSTurn) Then Throw New LoadAllocationChangePriorityNotAllowedBecuaseTurnStatusException
                Dim DShamsifull = _DateTime.GetCurrentDateShamsiFull
                Dim DS As DataSet
                If InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                        Where LoadAllocations.TurnId=" & NSSThisLoadAllocation.TurnId & " and LoadAllocations.LAStatusId=1 and 
                              LoadAllocations.Priority<" & NSSThisLoadAllocation.Priority & "  and LoadAllocations.DateShamsi='" & DShamsifull & "'  
                        Order By LoadAllocations.Priority Desc", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Exit Sub
                Else
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & DS.Tables(0).Rows(0).Item("Priority") & " Where LAId=" & NSSThisLoadAllocation.LAId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & NSSThisLoadAllocation.Priority & " Where LAId=" & DS.Tables(0).Rows(0).Item("LAId") & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    RePrioritize(NSSTurn, DShamsifull)
                End If
            Catch ex As LoadAllocationChangePriorityNotAllowedBecauseLoadAllocationStatusException
                Throw ex
            Catch ex As LoadAllocationChangePriorityNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As TimingNotReachedException
                Throw ex
            Catch ex As LoadAllocationNotFoundException
                Throw ex
            Catch ex As LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DecreasePriority(YourLAId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager

                Dim NSSThisLoadAllocation = GetNSSLoadAllocation(YourLAId)
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(NSSThisLoadAllocation.nEstelamId, False)
                Dim NSSTurn = InstanceTurns.GetNSSTurn(NSSThisLoadAllocation.TurnId)
                'آیا زمان تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If InstanceTiming.IsTimingActive(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId) Then
                    If InstanceTiming.GetTiming(NSSLoadCapacitorLoad.AHId, NSSLoadCapacitorLoad.AHSGId, _DateTime.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                        Throw New TimingNotReachedException
                    End If
                End If
                'کنترل وضعیت تخصیص
                If Not NSSThisLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered Then Throw New LoadAllocationChangePriorityNotAllowedBecauseLoadAllocationStatusException
                'محدودیت نوبت در تغییر اولویت تخصیص
                If Not InstanceTurns.IsTurnReadyforLoadAllocationChangePriority(NSSTurn) Then Throw New LoadAllocationChangePriorityNotAllowedBecuaseTurnStatusException
                Dim DShamsifull = _DateTime.GetCurrentDateShamsiFull
                Dim DS As DataSet
                If InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                        Where LoadAllocations.TurnId=" & NSSThisLoadAllocation.TurnId & " and LoadAllocations.LAStatusId=1 and 
                              LoadAllocations.Priority>" & NSSThisLoadAllocation.Priority & " and LoadAllocations.DateShamsi='" & DShamsifull & "'
                        Order By LoadAllocations.Priority Asc", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Exit Sub
                Else
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & DS.Tables(0).Rows(0).Item("Priority") & " Where LAId=" & NSSThisLoadAllocation.LAId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & NSSThisLoadAllocation.Priority & " Where LAId=" & DS.Tables(0).Rows(0).Item("LAId") & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    RePrioritize(NSSTurn, DShamsifull)
                End If
            Catch ex As LoadAllocationChangePriorityNotAllowedBecauseLoadAllocationStatusException
                Throw ex
            Catch ex As LoadAllocationChangePriorityNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As TimingNotReachedException
                Throw ex
            Catch ex As LoadAllocationNotFoundException
                Throw
            Catch ex As LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RePrioritize(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure, YourDateShamsiFull As String)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select LoadAllocations.LAId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                        Where LoadAllocations.TurnId=" & YourNSSTurn.nEnterExitId & " and LoadAllocations.LAStatusId=1 and 
                              LoadAllocations.DateShamsi='" & YourDateShamsiFull & "' 
                        Order By LoadAllocations.Priority Asc", 0, DS, New Boolean)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & Loopx + 1 & " Where LAId=" & DS.Tables(0).Rows(Loopx).Item("LAId") & ""
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetLoadAllocationsforLoadPermissionRegistering(Optional YourAHId As Int64 = Int64.MinValue, Optional YourAHSGId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
            Try
                Dim ComposeQueryAHId = IIf(YourAHId = Int64.MinValue, String.Empty, " And Elam.AHId=" & YourAHId & "")
                Dim ComposeQueryAHSGId = IIf(YourAHSGId = Int64.MinValue, String.Empty, " And Elam.AHSGId=" & YourAHSGId & "")
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                    Select * from (Select Top 1000000 LoadAllocation.Priority, LoadAllocation.LAId, LoadAllocation.nEstelamId, LoadAllocation.TurnId, LoadAllocation.LAStatusId, LoadAllocation.LANote, LoadAllocation.DateTimeMilladi, LoadAllocation.DateShamsi, LoadAllocation.Time, LoadAllocation.UserId
                                  From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                    Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                    Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On Elam.AHId=AHs.AHId 
                                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On Elam.AHSGId =AHSGs.AHSGId  
                                  Where LoadAllocation.DateShamsi ='" & _DateTime.GetCurrentDateShamsiFull() & "' and LoadAllocation.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & "" + ComposeQueryAHId + ComposeQueryAHSGId + " and Elam.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " Order By LoadAllocation.TurnId Asc, LoadAllocation.Priority Asc) as DataBox1
                    Union all
                    Select * from (Select Top 1000000 LoadAllocation.Priority, LoadAllocation.LAId, LoadAllocation.nEstelamId, LoadAllocation.TurnId, LoadAllocation.LAStatusId, LoadAllocation.LANote, LoadAllocation.DateTimeMilladi, LoadAllocation.DateShamsi, LoadAllocation.Time, LoadAllocation.UserId
                                   From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                     Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                     Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On Elam.AHId=AHs.AHId 
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On Elam.AHSGId =AHSGs.AHSGId  
                                  Where LoadAllocation.DateShamsi ='" & _DateTime.GetCurrentDateShamsiFull() & "' and LoadAllocation.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & "" + ComposeQueryAHId + ComposeQueryAHSGId + " and Elam.LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " Order By LoadAllocation.TurnId Asc, LoadAllocation.Priority Asc) as DataBox2", 0, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(Loopx).Item("LAId"), DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("TurnId"), DS.Tables(0).Rows(Loopx).Item("LAStatusId"), DS.Tables(0).Rows(Loopx).Item("LANote").trim, DS.Tables(0).Rows(Loopx).Item("Priority"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("UserId")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadAllocationLoadPermissionRegistering(YourNSSLoadAllocation As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourCurrentDateTime As R2StandardDateAndTimeStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager
                'کنترل وضعیت تخصیص بار
                Try
                    If YourNSSLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.CancelledUser Or YourNSSLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded Or YourNSSLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.CancelledSystem Or YourNSSLoadAllocation.LAStatusId = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled Then Throw New LoadAllocationLoadPermissionRegisteringNotAllowedBecauseLoadAllocationStatusException
                Catch ex As LoadAllocationLoadPermissionRegisteringNotAllowedBecauseLoadAllocationStatusException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception("Location:1:" + ex.Message)
                End Try

                Try
                    InstanceLoadPermission.LoadPermissionRegistering(YourNSSLoadAllocation, YourNSSLoadCapacitorLoad, YourCurrentDateTime, YourUserNSS)
                    Try
                        CmdSql.Connection.Open()
                        'CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                        '                      Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & ",LANote='مجوز صادر شده' Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        'CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                        '                      Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & ",LANote='مجوز صادر شده',Time='" & YourCurrentDateTime.Time & "' Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                                              Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & ",LANote='مجوز صادر شده',DateTimeMilladi='" & YourCurrentDateTime.DateTimeMilladiFormated & "',DateShamsi='" & YourCurrentDateTime.DateShamsiFull & "',Time='" & YourCurrentDateTime.Time & "' 
                                              Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        CmdSql.ExecuteNonQuery()
                        CmdSql.Connection.Close()
                    Catch ex As Exception
                        Throw New Exception("Location:2:" + ex.Message)
                    End Try
                Catch ex As Exception When TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException OrElse TypeOf ex Is PresentNotRegisteredInLast30MinuteException OrElse TypeOf ex Is PresentsNotEnoughException OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseBlackListException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException OrElse TypeOf ex Is LoadCapacitorLoadReleaseTimeNotReachedException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is GetDataException OrElse TypeOf ex Is AnnouncementsubGroupNotFoundException OrElse TypeOf ex Is AnnouncementsubGroupRelationTruckNotExistException OrElse TypeOf ex Is TruckTotalLoadPermissionReachedException OrElse TypeOf ex Is ExeededNumberofLoadPermisionsWithOneTurnException
                    'مارک تخصیص به حالت فی لد Falied
                    Try
                        CmdSql.Connection.Open()
                        'CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                        '                          Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed & ",LANote='" & ex.Message & "'  Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        'CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                        '                      Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionSucceeded & ",LANote='مجوز صادر شده',Time='" & YourCurrentDateTime.Time & "' Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                                                  Set LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed & ",LANote='" & ex.Message & "',
                                                      DateTimeMilladi='" & YourCurrentDateTime.DateTimeMilladiFormated & "',DateShamsi='" & YourCurrentDateTime.DateShamsiFull & "',Time='" & YourCurrentDateTime.Time & "' 
                                          Where LAId=" & YourNSSLoadAllocation.LAId & ""
                        CmdSql.ExecuteNonQuery()
                        CmdSql.Connection.Close()
                    Catch exy As Exception
                        Throw New Exception("Location:3:" + exy.Message)
                    End Try
                    Throw ex
                End Try
            Catch ex As Exception When TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
                                       OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException _
                                       OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException _
                                       OrElse TypeOf ex Is PresentNotRegisteredInLast30MinuteException _
                                       OrElse TypeOf ex Is PresentsNotEnoughException _
                                       OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseBlackListException _
                                       OrElse TypeOf ex Is GetNSSException _
                                       OrElse TypeOf ex Is LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException _
                                       OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
                                       OrElse TypeOf ex Is LoadCapacitorLoadReleaseTimeNotReachedException _
                                       OrElse TypeOf ex Is LoadAllocationLoadPermissionRegisteringNotAllowedBecauseLoadAllocationStatusException _
                                       OrElse TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
                                       OrElse TypeOf ex Is GetNSSException _
                                       OrElse TypeOf ex Is GetDataException _
                                       OrElse TypeOf ex Is AnnouncementsubGroupNotFoundException _
                                       OrElse TypeOf ex Is AnnouncementsubGroupRelationTruckNotExistException _
                                       OrElse TypeOf ex Is TimingNotReachedException _
                                       OrElse TypeOf ex Is TruckTotalLoadPermissionReachedException _
                                       OrElse TypeOf ex Is ExeededNumberofLoadPermisionsWithOneTurnException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function LoadAllocationsLoadPermissionRegistering(YourUserNSS As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim InstanceLoadPermissionPrinting = New R2CoreTransportationAndLoadNotificationInstanceLoadPermissionPrintingManager
                Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                Dim FailedResultLst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)()

                'جلوگیری از خواندن تخصیص هایی که هنوز در بانک آفلاین(ریپلیکیشن) تغییر نکرده اند 
                Threading.Thread.Sleep(60000)

                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure) = GetLoadAllocationsforLoadPermissionRegistering()
                If Lst.Count > 1000 Then
                    'فعال کردن ایز بیزی
                    InstanceSiteIsBusy.ActivateSiteIsBusy()
                End If

                Dim CurrentDateTime = New R2StandardDateAndTimeStructure(_DateTime.GetCurrentDateTimeMilladi, _DateTime.GetCurrentDateShamsiFull, _DateTime.GetCurrentTime)
                For Loopx As Int64 = 0 To Lst.Count - 1
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(Lst(Loopx).nEstelamId, True)
                    Dim AHId As Int64 = NSSLoadCapacitorLoad.AHId
                    Dim AHSGId As Int64 = NSSLoadCapacitorLoad.AHSGId

                    'کنترل مجوز کنترل تایمینگ در آزادسازی بار با توجه به وضعیت بار
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.LoadPermissionUseTimeHandlingByLoadStatus, NSSLoadCapacitorLoad.LoadStatus, 0) Then
                        'کنترل تایمینگ - آیا زمان صدور مجوز برای زیرگروه سالن مورد نظر فرارسیده است
                        If InstanceTiming.IsTimingActive(AHId, AHSGId) Then
                            If InstanceTiming.GetTiming(AHId, AHSGId, CurrentDateTime.Time) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering Then
                                Continue For
                            End If
                        End If
                    End If

                    Dim NSSLoadAllocation = Lst(Loopx)
                    Try
                        LoadAllocationLoadPermissionRegistering(NSSLoadAllocation, NSSLoadCapacitorLoad, CurrentDateTime, YourUserNSS)
                        'If InstanceConfigurationOfAnnouncements.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, NSSLoadCapacitorLoad.AHId, 2) Then
                        '    InstanceLoadPermissionPrinting.PrintLoadPermission(Lst(Loopx).LAId, YourUserNSS)
                        '    Threading.Thread.Sleep(InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadAllocationsLoadPermissionRegisteringSetting, 0))
                        'End If
                    Catch ex As Exception When TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
                                          OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException _
                                          OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseTurnIsNotReadyException _
                                          OrElse TypeOf ex Is PresentNotRegisteredInLast30MinuteException _
                                          OrElse TypeOf ex Is PresentsNotEnoughException _
                                          OrElse TypeOf ex Is LoadPermisionRegisteringFailedBecauseBlackListException _
                                          OrElse TypeOf ex Is GetNSSException _
                                          OrElse TypeOf ex Is TimingNotReachedException _
                                          OrElse TypeOf ex Is LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException _
                                          OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
                                          OrElse TypeOf ex Is LoadCapacitorLoadReleaseTimeNotReachedException _
                                          OrElse TypeOf ex Is LoadAllocationLoadPermissionRegisteringNotAllowedBecauseLoadAllocationStatusException _
                                          OrElse TypeOf ex Is TurnHandlingNotAllowedBecuaseTurnStatusException _
                                          OrElse TypeOf ex Is GetNSSException _
                                          OrElse TypeOf ex Is GetDataException _
                                          OrElse TypeOf ex Is AnnouncementsubGroupNotFoundException _
                                          OrElse TypeOf ex Is AnnouncementsubGroupRelationTruckNotExistException _
                                          OrElse TypeOf ex Is TruckTotalLoadPermissionReachedException _
                                          OrElse TypeOf ex Is ExeededNumberofLoadPermisionsWithOneTurnException
                        Try
                            If NSSLoadAllocation.LAStatusId <> R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed Then
                                If InstanceConfigurationOfAnnouncements.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, NSSLoadCapacitorLoad.AHId, 2) Then
                                    Dim InstanceFailedLoadAllocationAnnouncePrinting = New R2CoreTransportationAndLoadNotificationInstanceFailedLoadAllocationAnnouncePrintingManager
                                    InstanceFailedLoadAllocationAnnouncePrinting.PrintFailedLoadAllocation(Lst(Loopx).LAId)
                                    Threading.Thread.Sleep(InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadAllocationsLoadPermissionRegisteringSetting, 0))
                                End If
                            End If
                            Lst(Loopx).LANote = ex.Message
                            FailedResultLst.Add(Lst(Loopx))
                        Catch exx As Exception When TypeOf ex Is GetNSSException OrElse TypeOf ex Is GetDataException
                        End Try
                    End Try
                Next

                'غیر فعال کردن ایز بیزی
                InstanceSiteIsBusy.DeActivateSiteIsBusy()

                Lst = Nothing

                Return FailedResultLst
            Catch ex As Exception
                'غیر فعال کردن ایز بیزی
                InstanceSiteIsBusy.DeActivateSiteIsBusy()

                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SendLoadAllocationsLoadPermissionRegisteringFailedSMS()
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {InstanceSoftwareUsers.GetNSSUserUnChangeable(New R2CoreSoftwareUserMobile(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 1)))}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = String.Empty}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreTransportationAndLoadNotificationSMSTypes.LoadAllocationsLoadPermissionRegisteringFailed, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSLoadAllocationsLoadPermissionRegisteringFailedFailedException(SMSResultAnalyze)
            Catch ex As SendSMSLoadAllocationsLoadPermissionRegisteringFailedFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Function GetNSSLoadAllocation(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure
            Try
                Dim InstanceSqlDataBOX As New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                                          "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.nEstelamId=" & YournEstelamId & " and LoadAllocation.TurnId=" & YourTurnId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(0).Item("LAId"), DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("TurnId"), DS.Tables(0).Rows(0).Item("LAStatusId"), DS.Tables(0).Rows(0).Item("LANote").trim, DS.Tables(0).Rows(0).Item("Priority"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("Truck"), DS.Tables(0).Rows(0).Item("StrPersonFullName"), DS.Tables(0).Rows(0).Item("LoadAllocationStatusTitle"), DS.Tables(0).Rows(0).Item("StrDescription"), Color.FromName(DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor")))
            Catch exx As LoadAllocationNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ChangeLoadAllocationStatus(YourLoadAllocationId As Int64, YourLoadAllocationStatusId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim DMilladiFormated = _DateTime.GetCurrentDateTimeMilladiFormated()
                Dim DShamsiFull = _DateTime.ConvertToShamsiDateFull(DMilladiFormated)
                Dim TimeofDate = _DateTime.GetTimeOfDate(DMilladiFormated)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations
                                        Set LAStatusId=" & YourLoadAllocationStatusId & ",DateTimeMilladi='" & DMilladiFormated & "',DateShamsi='" & DShamsiFull & "',Time='" & TimeofDate & "'
                                      Where LAId=" & YourLoadAllocationId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                RePrioritize(InstanceTurns.GetNSSTurn(GetTurnIdfromLoadAllocationId(YourLoadAllocationId)), DShamsiFull)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement
        Private Shared _DateTime As New R2DateTime


        Public Shared Function GetLoadAllocationsforTruckDriver(Optional YourSoftwareUserId As Int64 = Int64.MinValue, Optional YourTurnId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
   "Declare @LastTurnId int
    Select Top 1 @LastTurnId=Turns.nEnterExitId 
	from  R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
	   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
       Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
       Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
       Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
    Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and Cars.ViewFlag=1 and CarAndPersons.snRelation=2  
          and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
          Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc
    Select LoadCapacitor.nEstelamID as LoadCapacitorLoadnEstelamId,Targets.strCityName as LoadCapacitorLoadTargetTitle,Products.strGoodName as  LoadCapacitorLoadGoodTitle,LoadCapacitor.nCarNumKol as LoadCapacitorLoadnCarNumKol,LoadCapacitor.nTonaj as LoadCapacitorLoadnTonaj,LoadCapacitor.strPriceSug as LoadCapacitorLoadStrPriceSug,LoadCapacitor.strDescription as LoadCapacitorLoadStrDescription,LoaderTypes.LoaderTypeTitle as LoadCapacitorLoadLoaderTypeTitle,
	       LoadCapacitor.strAddress as LoadCapacitorLoadStrAddress,LoadCapacitor.strBarName as LoadCapacitorLoadstrBarName,LoadCapacitor.dTimeElam as LoadCapacitorLoaddTimeElam,LoadCapacitor.dDateElam  as  LoadCapacitorLoaddDateElam,LoadCapacitorLoadStatuses.LoadStatusName as LoadCapacitorLoadStatusTitle,AHs.AHTitle as LoadCapacitorLoadAHTitle,AHSGs.AHSGTitle as LoadCapacitorLoadAHSGTitle,TransportCompanies.TCTitle as  TransportCompanyTitle,TransportCompanies.TCTel as  TransportCompanyTel,Turns.nEnterExitId as  TurnnEnterExitId,
	       Turns.OtaghdarTurnNumber as TurnOtaghdarTurnNumber,Turns.strEnterDate as TurnEnterDate,Turns.strEnterTime as TurnEnterTime,Turns.strDesc as TurnStrDesc,TurnStatuses.TurnStatusTitle as TurnStatusTitle,Persons.strPersonFullName as TruckDriver,Drivers.strSmartcardNo  as TruckDriverSmartCardNo,Persons.strIDNO as TruckDriverMobileNumber,Cars.strCarNo+'-'+Cars.strCarSerialNo as TruckLPString,Cars.strBodyNo as TruckSmartCardNo,Turns.strExitDate as LoadPermissionDate,
           Turns.strExitTime as LoadPermissionTime,LoadPermissionStatuses.LoadPermissionStatusTitle as LoadPermissionStatusTitle,Turns.strBarnameNo as LoadPermissionRegisteringLocation,LoadAllocations.LAId as  LoadAllocationId,LoadAllocationStatuses.LoadAllocationStatusTitle as  LoadAllocationStatusTitle,LoadAllocations.LANote as LoadAllocationNote,LoadAllocations.DateShamsi as LoadAllocationDateShamsi,LoadAllocations.Time as LoadAllocationTime,
           LoadAllocationStatuses.LoadAllocationStatusColor as LoadAllocationStatusColor,LoadAllocations.Priority as LoadAllocationPriority,LoadAllocations.LAStatusId as LoadAllocationStatusId,LoadAllocations.UserId as LoadAllocationUserId,LoadAllocations.DateTimeMilladi as LoadAllocationDateTimeMilladi   ,Cast(Targets.nDistance/25 as int) as TargetTravellength
    from dbtransport.dbo.tbEnterExit as Turns
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations On Turns.nEnterExitId=LoadAllocations.TurnId
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
       Inner Join dbtransport.dbo.tbElam as LoadCapacitor On LoadAllocations.nEstelamId=LoadCapacitor.nEstelamID 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs on LoadCapacitor.AHSGId=AHSGs.AHSGId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadCapacitorLoadStatuses On LoadCapacitor.LoadStatus=LoadCapacitorLoadStatuses.LoadStatusId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On LoadCapacitor.nTruckType=LoaderTypes.LoaderTypeId 
       Inner Join dbtransport.dbo.tbProducts as Products On LoadCapacitor.nBarcode=Products.strGoodCode 
       Inner Join dbtransport.dbo.tbCity as Targets On LoadCapacitor.nCityCode=Targets.nCityCode 
       Inner Join dbtransport.dbo.TbCar as Cars on Turns.strCardno=Cars.nIDCar 
       Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Cars.nIDCar=CarAndPersons.nIDCar 
       Inner Join dbtransport.dbo.TbPerson as Persons On CarAndPersons.nIDPerson=Persons.nIDPerson 
       Inner Join dbtransport.dbo.TbDriver as Drivers On Persons.nIDPerson=Drivers.nIDDriver 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On LoadCapacitor.nCompCode=TransportCompanies.TCId 
       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadPermissionStatuses as LoadPermissionStatuses On Turns.LoadPermissionStatus=LoadPermissionStatuses.LoadPermissionStatusId 
    Where Turns.nEnterExitId=" & IIf(YourTurnId = Int64.MinValue, "@LastTurnId", YourTurnId) & " and (LoadAllocations.LAStatusId=1 or LoadAllocations.LAStatusId=2 or LoadAllocations.LAStatusId=3 or LoadAllocations.LAStatusId=5) and CarAndPersons.snRelation=2 and AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0 and LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "' 
          and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
    Order By CarAndPersons.nIDCarAndPerson Desc,LoadAllocations.LAStatusId Asc,LoadAllocations.Priority Asc", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationsNotFoundException
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure) = New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedforTruckDriverStructure
                    NSS.LAId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.nEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.TurnId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.LAStatusId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"))
                    NSS.LANote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.Priority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), Int16.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    NSS.DateTimeMilladi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"), DBNull.Value), Now, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateTimeMilladi"))
                    NSS.DateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.Time = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.UserId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"), DBNull.Value), Int64.MinValue, DS.Tables(0).Rows(Loopx).Item("LoadAllocationUserId"))
                    NSS.TCTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.GoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.Truck = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckDriverFullNameFamily = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.LoadAllocationStatus = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.StrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LAStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadCapacitorLoadnEstelamId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnEstelamId"))
                    NSS.LoadCapacitorLoadTargetTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadTargetTitle"))
                    NSS.LoadCapacitorLoadTargetTravelength = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TargetTravellength"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TargetTravellength"))
                    NSS.LoadCapacitorLoadGoodTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadGoodTitle"))
                    NSS.LoadCapacitorLoadnCarNumKol = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnCarNumKol"))
                    NSS.LoadCapacitorLoadnTonaj = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadnTonaj"))
                    NSS.LoadCapacitorLoadStrPriceSug = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrPriceSug"))
                    NSS.LoadCapacitorLoadStrDescription = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrDescription"))
                    NSS.LoadCapacitorLoadStrBarName = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrBarName"))
                    NSS.LoadCapacitorLoadLoaderTypeTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadLoaderTypeTitle"))
                    NSS.LoadCapacitorLoadStrAddress = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStrAddress"))
                    NSS.LoadCapacitorLoaddDateElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddDateElam"))
                    NSS.LoadCapacitorLoaddTimeElam = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoaddTimeElam"))
                    NSS.LoadCapacitorLoadStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadStatusTitle"))
                    NSS.LoadCapacitorLoadAHTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHTitle"))
                    NSS.LoadCapacitorLoadAHSGTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadCapacitorLoadAHSGTitle"))
                    NSS.TransportCompanyTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTitle"))
                    NSS.TransportCompanyTel = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TransportCompanyTel"))
                    NSS.TurnnEnterExitId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnnEnterExitId"))
                    NSS.TurnOtaghdarTurnNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnOtaghdarTurnNumber"))
                    NSS.TurnEnterDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterDate"))
                    NSS.TurnEnterTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnEnterTime"))
                    NSS.TurnStrDesc = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStrDesc"))
                    NSS.TurnStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TurnStatusTitle"))
                    NSS.TruckDriver = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriver"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriver"))
                    NSS.TruckDriverSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverSmartCardNo"))
                    NSS.TruckDriverMobileNumber = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckDriverMobileNumber"))
                    NSS.TruckLPString = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckLPString"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckLPString"))
                    NSS.TruckSmartCardNo = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("TruckSmartCardNo"))
                    NSS.LoadPermissionDate = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionDate"))
                    NSS.LoadPermissionTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionTime"))
                    NSS.LoadPermissionRegisteringLocation = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionRegisteringLocation"))
                    NSS.LoadPermissionStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadPermissionStatusTitle"))
                    NSS.LoadAllocationId = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationId"))
                    NSS.LoadAllocationStatusTitle = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"))
                    NSS.LoadAllocationNote = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationNote"))
                    NSS.LoadAllocationDateShamsi = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationDateShamsi"))
                    NSS.LoadAllocationTime = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationTime"))
                    NSS.LoadAllocationStatusColor = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"), DBNull.Value), Color.Red, Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").TRIM))
                    NSS.LoadAllocationPriority = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("LoadAllocationPriority"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As LoadAllocationsNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadAllocations(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
            Try
                Dim LST = GetLoadAllocationsforTruckDriver(Int64.MinValue, YourNSSTurn.nEnterExitId)
                Return LST.Cast(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)().ToList()
            Catch ex As SoftwareUserRelatedTurnNotFoundException
                Throw ex
            Catch ex As LoadAllocationsNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoadAllocation(YourLoadAllocationId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                                        "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.LAId=" & YourLoadAllocationId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(0).Item("LAId"), DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("TurnId"), DS.Tables(0).Rows(0).Item("LAStatusId"), DS.Tables(0).Rows(0).Item("LANote").trim, DS.Tables(0).Rows(0).Item("Priority"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("Truck"), DS.Tables(0).Rows(0).Item("StrPersonFullName"), DS.Tables(0).Rows(0).Item("LoadAllocationStatusTitle"), DS.Tables(0).Rows(0).Item("StrDescription"), Color.FromName(DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor")))
            Catch exx As LoadAllocationNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoadAllocation(YournEstelamId As Int64, YourTurnId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                                                          "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.nEstelamId=" & YournEstelamId & " and LoadAllocation.TurnId=" & YourTurnId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(0).Item("LAId"), DS.Tables(0).Rows(0).Item("nEstelamId"), DS.Tables(0).Rows(0).Item("TurnId"), DS.Tables(0).Rows(0).Item("LAStatusId"), DS.Tables(0).Rows(0).Item("LANote").trim, DS.Tables(0).Rows(0).Item("Priority"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId")), DS.Tables(0).Rows(0).Item("TCTitle").trim, DS.Tables(0).Rows(0).Item("strGoodName").trim, DS.Tables(0).Rows(0).Item("strCityName").trim, DS.Tables(0).Rows(0).Item("Truck"), DS.Tables(0).Rows(0).Item("StrPersonFullName"), DS.Tables(0).Rows(0).Item("LoadAllocationStatusTitle"), DS.Tables(0).Rows(0).Item("StrDescription"), Color.FromName(DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor")))
            Catch exx As LoadAllocationNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoadAllocationStatus(YourLoadAllocationStatusId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure
            Try
                Dim DS As DataSet = Nothing
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses Where LoadAllocationStatusId=" & YourLoadAllocationStatusId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadAllocationStatusNotFoundException
                Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure
                NSS.LoadAllocationStatusId = YourLoadAllocationStatusId
                NSS.LoadAllocationStatusTitle = DS.Tables(0).Rows(0).Item("LoadAllocationStatustitle").TRIM
                NSS.LoadAllocationStatusColor = DS.Tables(0).Rows(0).Item("LoadAllocationStatusColor").TRIM
                NSS.ViewFlag = DS.Tables(0).Rows(0).Item("ViewFlag")
                Return NSS
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function ExistRegisteredLoadAllocation(YournEstelamId As Int64, YourTurnId As Int64) As Boolean
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                     "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.nEstelamId=" & YournEstelamId & " and LoadAllocations.TurnId=" & YourTurnId & " and 
                            LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " and 
                            LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "'", 0, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetTotalNumberOfRegisteredLoadAllocations(YourTurnId As Int64) As Int64
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Count(*) As Counting from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.TurnId=" & YourTurnId & " and LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & "", 0, DS, New Boolean)
                Return DS.Tables(0).Rows(0).Item("Counting")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub ChangeLoadAllocationStatus(YourLoadAllocationId As Int64, YourLoadAllocationStatusId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations
                                        Set LAStatusId=" & YourLoadAllocationStatusId & ",DateTimeMilladi='" & _DateTime.GetCurrentDateTimeMilladiFormated & "',DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "',Time='" & _DateTime.GetCurrentTime & "'
                                      Where LAId=" & YourLoadAllocationId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                RePrioritize(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetNSSLoadAllocation(YourLoadAllocationId).TurnId))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetTotalNumberOfLoadAllocationWhichBlindfold(YournEstelamId As Int64) As Int64 'تخصیص بلاتکلیف
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Count(*) as Count from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations 
                         Where nEstelamId=" & YournEstelamId & " and LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & "", 0, DS, New Boolean).GetRecordsCount() = 0 Then Return 0
                Return DS.Tables(0).Rows(0).Item(("Count"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsLoadCapacitorLoadAHSGMatchWithSequentialTurnOfTurn(YournEstelamId As Int64, YournEnterExitId As Int64)
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                       "Select * from dbtransport.dbo.tbElam as LoadCapacitor
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On LoadCapacitor.AHSGId=AHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationSequentialTurns as AHRSeqT oN AH.AHId=AHRSeqT.AHId
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SeqT On AHRSeqT.SeqTId=SeqT.SeqTId
                            Inner Join dbtransport.dbo.tbEnterExit as Turns On SeqT.SeqTKeyWord Collate Arabic_CI_AI_WS=Substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS
                        Where AHRAHSG.RelationActive=1 and AHRSeqT.RelationActive=1 and Turns.nEnterExitId=" & YournEnterExitId & " and LoadCapacitor.nEstelamID=" & YournEstelamId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadAllocations(YourAHId As Int64, YourAHSGId As Int64, YourLoadAllocationStatusId As Int64, Optional YourTransportCompanyId As Int64 = Int64.MinValue, Optional YournEstelamId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
            Try
                Dim DS As DataSet
                If YournEstelamId = Int64.MinValue Then
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull() & "' and LoadAllocation.LAStatusId=" & YourLoadAllocationStatusId & " and Elam.AHId=" & YourAHId & " and Elam.AHSGId=" & YourAHSGId & "" & IIf(YourTransportCompanyId = Int64.MinValue, String.Empty, " and TransportCompanies.TCId=" & YourTransportCompanyId & "") & " 
                                         Order By EnterExit.nEnterExitId Desc", 1, DS, New Boolean)
                Else
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,Car.strCarNo+'-'+Car.strCarSerialNo as Truck,Person.strPersonFullName,LoadAllocationStatus.LoadAllocationStatusTitle,LoadAllocationStatus.LoadAllocationStatusColor,ELAM.strDescription
                                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                                           Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                           Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                           Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                           Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                           Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar
                                           Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatus On LoadAllocation.LAStatusId=LoadAllocationStatus.LoadAllocationStatusId
                                         Where LoadAllocation.nEstelamId=" & YournEstelamId & " Order By EnterExit.nEnterExitId Desc", 1, DS, New Boolean)
                End If
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(Loopx).Item("LAId"), DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("TurnId"), DS.Tables(0).Rows(Loopx).Item("LAStatusId"), DS.Tables(0).Rows(Loopx).Item("LANote").trim, DS.Tables(0).Rows(Loopx).Item("Priority"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("UserId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("Truck"), DS.Tables(0).Rows(Loopx).Item("StrPersonFullName"), DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle"), DS.Tables(0).Rows(Loopx).Item("StrDescription"), Color.FromName(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor"))))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadAllocationsforLoadPermissionRegistering(YourAHId As Int64, YourAHSGId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "
                      Select LoadAllocation.Priority,LoadAllocation.LAId,LoadAllocation.nEstelamId,LoadAllocation.TurnId,LoadAllocation.LAStatusId,LoadAllocation.LANote,LoadAllocation.DateTimeMilladi,LoadAllocation.DateShamsi,LoadAllocation.Time,LoadAllocation.UserId
                             From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations  as LoadAllocation 
                               Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                               Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On Elam.AHId=AHs.AHId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On Elam.AHSGId =AHSGs.AHSGId  
                             Where LoadAllocation.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull() & "' and LoadAllocation.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " and (AHs.AHId=" & YourAHId & ") and (AHSGs.AHSGId=" & YourAHSGId & ") 
                             Order By TurnId Asc,LoadAllocation.Priority Asc", 0, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStructure(DS.Tables(0).Rows(Loopx).Item("LAId"), DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("TurnId"), DS.Tables(0).Rows(Loopx).Item("LAStatusId"), DS.Tables(0).Rows(Loopx).Item("LANote").trim, DS.Tables(0).Rows(Loopx).Item("Priority"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("UserId")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadAllocationStatuses() As List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses Where ViewFlag=1 Order By LoadAllocationStatusId", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadAllocationStatusStructure(DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusId"), DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusTitle").trim, DS.Tables(0).Rows(Loopx).Item("LoadAllocationStatusColor").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub RePrioritize(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                       "Select LoadAllocations.LAId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                        Where LoadAllocations.TurnId=" & YourNSSTurn.nEnterExitId & " and LoadAllocations.LAStatusId=1 and 
                              LoadAllocations.DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "' 
                        Order By LoadAllocations.Priority Asc", 0, DS, New Boolean)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Set Priority=" & Loopx + 1 & " Where LAId=" & DS.Tables(0).Rows(Loopx).Item("LAId") & ""
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Shared Function IsActiveLoadAllocationRegisteringforThisAnnouncementsubGroup(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
        '    Try
        '        var NSSLoadCapacitorLoad = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(YournEstelamId);
        '        String ComposeSearchString = NSSLoadCapacitorLoad.AHSGId.ToString() + "=";
        '        String[] AllAnnouncementsLoadAllocationSetting = R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(Convert.ToInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadAllocationSetting), NSSLoadCapacitorLoad.AHId,);
        '        Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
        '        Return New R2StandardDateAndTimeStructure(Nothing, Nothing, AllAnnounceTimesofAnnouncementsubGroup(AllAnnounceTimesofAnnouncementsubGroup.Count - 1))

        '    Catch ex As Exception

        '    End Try
        'End Function

    End Class

    Namespace FailedLoadAllocationPrinting

        Public Structure R2CoreTransportationAndLoadNotificationFailedLoadAllocationPrintingInf
            Dim LoadAllocationId As Int64
            Dim nEstelamId As Int64
            Dim TurnId As String
            Dim TransportCompany As String
            Dim TruckLP As String
            Dim TruckLPSerial As String
            Dim TruckDriver As String
            Dim GoodName As String
            Dim TargetCity As String
            Dim OtherNote As String
        End Structure

        Public Class R2CoreTransportationAndLoadNotificationInstanceFailedLoadAllocationAnnouncePrintingManager
            Private WithEvents _PrintDocumentFailedLoadAllocation As PrintDocument = New PrintDocument()
            Private _PPDS As R2CoreTransportationAndLoadNotificationFailedLoadAllocationPrintingInf

            Private Function GetFailedLoadAllocationPrintingInf(YourLoadAllocationId As Int64) As R2CoreTransportationAndLoadNotificationFailedLoadAllocationPrintingInf
                Try
                    Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                          "Select LoadAllocation.LAId,LoadAllocation.nEstelamId,Substring(EnterExit.OtaghdarTurnNumber,7,20) as TurnId,TransportCompany.TCTitle,Car.strCarNo as Truck,Car.strCarSerialNo as TruckSerial,Person.strPersonFullName,Product.strGoodName,CityTarget.strCityName as TargetCity,LoadAllocation.LANote
                           from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocation
                                Inner Join dbtransport.dbo.tbEnterExit as EnterExit On LoadAllocation.TurnId=EnterExit.nEnterExitId
                                Inner Join dbtransport.dbo.tbElam as Elam On LoadAllocation.nEstelamId=Elam.nEstelamID
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompany On Elam.nCompCode=TransportCompany.TCId
                                Inner Join dbtransport.dbo.TbCar as Car On EnterExit.strCardno=Car.nIDCar 
								Inner Join dbtransport.dbo.TbPerson as Person On EnterExit.nDriverCode=Person.nIDPerson
                                Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode
                                Inner Join dbtransport.dbo.tbCity as CityTarget On Elam.nCityCode=CityTarget.nCityCode
                           Where LoadAllocation.LAId=" & YourLoadAllocationId & "", 1, DS, New Boolean)
                    Dim FailedLoadAllocationPrintingInf As New R2CoreTransportationAndLoadNotificationFailedLoadAllocationPrintingInf()
                    FailedLoadAllocationPrintingInf.LoadAllocationId = DS.Tables(0).Rows(0).Item("LAId")
                    FailedLoadAllocationPrintingInf.nEstelamId = DS.Tables(0).Rows(0).Item("nEstelamId")
                    FailedLoadAllocationPrintingInf.TurnId = DS.Tables(0).Rows(0).Item("TurnId")
                    FailedLoadAllocationPrintingInf.TransportCompany = DS.Tables(0).Rows(0).Item("TCTitle").trim
                    FailedLoadAllocationPrintingInf.TruckLP = DS.Tables(0).Rows(0).Item("Truck").trim
                    FailedLoadAllocationPrintingInf.TruckLPSerial = DS.Tables(0).Rows(0).Item("TruckSerial").trim
                    FailedLoadAllocationPrintingInf.TruckDriver = DS.Tables(0).Rows(0).Item("strPersonFullName").trim
                    FailedLoadAllocationPrintingInf.GoodName = DS.Tables(0).Rows(0).Item("strGoodName").trim
                    FailedLoadAllocationPrintingInf.TargetCity = DS.Tables(0).Rows(0).Item("TargetCity").trim
                    FailedLoadAllocationPrintingInf.OtherNote = DS.Tables(0).Rows(0).Item("LANote").trim
                    Return FailedLoadAllocationPrintingInf
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub PrintFailedLoadAllocation(YourLoadAllocationId As Int64)
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    _PPDS = GetFailedLoadAllocationPrintingInf(YourLoadAllocationId)
                    'چاپ مجوز
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(_PPDS.nEstelamId, True)
                    Dim ComposeSearchString As String = NSSLoadCapacitorLoad.AHSGId.ToString + "="
                    Dim AllAnnouncementHallPrinters As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, NSSLoadCapacitorLoad.AHId, 3), "-")
                    Dim AnnouncementsubGroupPrinter As String = Mid(AllAnnouncementHallPrinters.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnouncementHallPrinters.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length)
                    _PrintDocumentFailedLoadAllocation.PrinterSettings.PrinterName = AnnouncementsubGroupPrinter.Trim
                    _PrintDocumentFailedLoadAllocation.DefaultPageSettings.PaperSize = _PrintDocumentFailedLoadAllocation.PrinterSettings.PaperSizes(4)
                    _PrintDocumentFailedLoadAllocation.DefaultPageSettings.PaperSource = _PrintDocumentFailedLoadAllocation.PrinterSettings.PaperSources(2)
                    _PrintDocumentFailedLoadAllocation.Print()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentFailedLoadAllocation_BeginPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentFailedLoadAllocation.BeginPrint
            End Sub

            Private Sub _PrintDocumentFailedLoadAllocation_EndPrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles _PrintDocumentFailedLoadAllocation.EndPrint
            End Sub

            Private Sub A4Printing(ByVal YourEventArgs As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Int16, ByVal Y As Int16)
                Try
                    Dim myPaperSizeHalf As Integer = _PrintDocumentFailedLoadAllocation.PrinterSettings.DefaultPageSettings.PaperSize.Width / 2
                    Dim myStrFontSuperMax As Font = New System.Drawing.Font("Badr", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMax As Font = New System.Drawing.Font("Badr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMin As Font = New System.Drawing.Font("Badr", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myDigFont As Font = New System.Drawing.Font("Alborz Titr", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                    YourEventArgs.Graphics.DrawString("گواهی عدم صدور مجوز بارگيری", myStrFontMax, Brushes.DarkBlue, myPaperSizeHalf - (New Size(TextRenderer.MeasureText("گواهی عدم صدور مجوز بارگيری", myStrFontMax))).Width / 2, Y)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriver, myStrFontSuperMax, Brushes.DarkBlue, myPaperSizeHalf - (New Size(TextRenderer.MeasureText(_PPDS.TruckDriver, myStrFontSuperMax))).Width / 2, Y + 20)
                    Dim a As Char()
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontMax, Brushes.DarkBlue, X + 20, Y + 60)
                        a = _PPDS.TruckLP.ToCharArray()
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontMax, Brushes.DarkBlue, X + 50, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontMax, Brushes.DarkBlue, X + 60, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontMax, Brushes.DarkBlue, X + 70, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontMax, Brushes.DarkBlue, X + 80, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontMax, Brushes.DarkBlue, X + 90, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(2), myStrFontMax, Brushes.DarkBlue, X + 100, Y + 60)
                    End If
                    YourEventArgs.Graphics.DrawString(_PPDS.OtherNote, myStrFontMax, Brushes.DarkBlue, myPaperSizeHalf - (New Size(TextRenderer.MeasureText(_PPDS.OtherNote, myStrFontMax))).Width / 2, Y + 100)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 120)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 450, Y + 140)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 140)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 450, Y + 160)
                    YourEventArgs.Graphics.DrawString("کد مخزن بار: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 300, Y + 160)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 150, Y + 160)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub A5Printing(ByVal YourEventArgs As System.Drawing.Printing.PrintPageEventArgs, ByVal X As Int16, ByVal Y As Int16)
                Try
                    Dim myPaperSizeHalf As Integer = _PrintDocumentFailedLoadAllocation.PrinterSettings.DefaultPageSettings.PaperSize.Width / 2
                    Dim myStrFontSuperMax As Font = New System.Drawing.Font("Badr", 18, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMax As Font = New System.Drawing.Font("Badr", 14, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myStrFontMin As Font = New System.Drawing.Font("Badr", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
                    Dim myDigFont As Font = New System.Drawing.Font("Alborz Titr", 14, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                    YourEventArgs.Graphics.DrawString("گواهی عدم صدور مجوز بارگيری", myStrFontMax, Brushes.DarkBlue, X + 160, Y)
                    YourEventArgs.Graphics.DrawString(_PPDS.TruckDriver, myStrFontSuperMax, Brushes.DarkBlue, X + 100, Y + 20)
                    Dim a As Char()
                    If _PPDS.TruckLP.Trim <> String.Empty Then
                        YourEventArgs.Graphics.DrawString(_PPDS.TruckLPSerial + "-", myStrFontMax, Brushes.DarkBlue, X + 20, Y + 60)
                        a = _PPDS.TruckLP.ToCharArray()
                        YourEventArgs.Graphics.DrawString(a(4), myStrFontMax, Brushes.DarkBlue, X + 50, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(5), myStrFontMax, Brushes.DarkBlue, X + 60, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(3), myStrFontMax, Brushes.DarkBlue, X + 70, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(0), myStrFontMax, Brushes.DarkBlue, X + 80, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(1), myStrFontMax, Brushes.DarkBlue, X + 90, Y + 60)
                        YourEventArgs.Graphics.DrawString(a(2), myStrFontMax, Brushes.DarkBlue, X + 100, Y + 60)
                    End If
                    YourEventArgs.Graphics.DrawString(_PPDS.OtherNote, myStrFontMax, Brushes.DarkBlue, X + 100, Y + 100)
                    YourEventArgs.Graphics.DrawString("شرکت/موسسه محترم: " + _PPDS.TransportCompany, myStrFontMax, Brushes.DarkBlue, X + 250, Y + 120)
                    YourEventArgs.Graphics.DrawString(" جهت حمل " + _PPDS.GoodName, myStrFontMax, Brushes.DarkBlue, X + 250, Y + 140)
                    YourEventArgs.Graphics.DrawString(" به مقصد " + _PPDS.TargetCity, myStrFontMax, Brushes.DarkBlue, X + 20, Y + 140)
                    YourEventArgs.Graphics.DrawString(" شماره تخصيص: " + _PPDS.LoadAllocationId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 330, Y + 160)
                    YourEventArgs.Graphics.DrawString("کد مخزن بار: " + _PPDS.nEstelamId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 180, Y + 160)
                    YourEventArgs.Graphics.DrawString(" شماره نوبت: " + _PPDS.TurnId.ToString(), myStrFontMax, Brushes.DarkBlue, X + 30, Y + 160)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentFailedLoadAllocation_PrintPage_Printing(ByVal X As Int16, ByVal Y As Int16, ByVal E As System.Drawing.Printing.PrintPageEventArgs)
                Try
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim ConfiguredPageType As String = InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadPermissionsSetting, InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(_PPDS.nEstelamId, True).AHId, 0)
                    If [Enum].Parse(GetType(System.Drawing.Printing.PaperKind), ConfiguredPageType) = System.Drawing.Printing.PaperKind.A5 Then
                        A5Printing(E, 15, 20)
                    ElseIf [Enum].Parse(GetType(System.Drawing.Printing.PaperKind), ConfiguredPageType) = System.Drawing.Printing.PaperKind.A4 Then
                        A4Printing(E, 50, 80)
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub _PrintDocumentFailedLoadAllocation_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles _PrintDocumentFailedLoadAllocation.PrintPage
                Try
                    _PrintDocumentFailedLoadAllocation_PrintPage_Printing(0, 0, e)
                Catch ex As Exception
                    MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

    'BPTChanged
    Public MustInherit Class R2CoreTransportationAndLoadNotificationLoadAllocationStatuses
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Registered As Int64 = 1
        Public Shared ReadOnly Property PermissionSucceeded As Int64 = 2
        Public Shared ReadOnly Property PermissionFailed As Int64 = 3
        Public Shared ReadOnly Property CancelledUser As Int64 = 4
        Public Shared ReadOnly Property PermissionCancelled As Int64 = 5
        Public Shared ReadOnly Property CancelledSystem As Int64 = 6
    End Class

    'BPTChanged
    Namespace Exceptions

        Public Class LoadAllocationRegisteringReachedEndTimeException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان تخصیص بار به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class RequesterHasNotPermissionforLoadAllocationRegisteringException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درخواست کننده مجوز تخصیص این بار را ندارد"
                End Get
            End Property
        End Class

        Public Class RequesterCanNotAllocateSedimentedLoadInTimeRangeException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "درخواست کننده مجوز تخصیص بار رسوبی در این زمان را ندارد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationNotAllowedBecuaseAHSGLoadAllocationIsUnactiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تخصیص بار برای زیرگروه بار مورد نظر ، درحال حاضر غیرفعال است"
                End Get
            End Property
        End Class

        Public Class LoadAllocationChangePriorityNotAllowedBecuaseTurnStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان تغییر اولویت تخصیص بار به دلیل وضعیت نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationChangePriorityNotAllowedBecauseLoadAllocationStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان تغییر اولویت به دلیل وضعیت تخصیص وجود ندارد"
                End Get
            End Property
        End Class

        Public Class UnableAllocatingTommorowLoadException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "بار فردا قابلیت صدور تخصیص ندارد"
                End Get
            End Property
        End Class

        Public Class TimingNotReachedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "انجام این فرآیند تا لحظاتی دیگر امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadAllocationRegisteringFailedBecauseTurnIsNotReadyException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان تخصیص بار به دلیل وضعیت فعلی نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationRegisteringFailedBecauseLoadCapacitorLoadIsNotReadyException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان تخصیص بار به دلیل وضعیت فعلی بار وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationsNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سابقه تخصیص بار یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationStatusNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت تخصیص بار با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationCancellingNotAllowedBecauseLoadAllocationStatusException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کنسلی تخصیص بار با توجه به وضعیت فعلی تخصیص امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadAllocationCancellingNotAllowedBecauseTurnStatusException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کنسلی تخصیص بار با توجه به وضعیت فعلی نوبت امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadAllocationLoadPermissionRegisteringNotAllowedBecauseLoadAllocationStatusException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "صدور مجوز برای تخصیص بار با توجه به وضعیت فعلی تخصیص بارامکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadLoaderTypeViaSequentialTurnOfTurnNotAllowedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان تخصیص بار به نوبت مورد نظر به دلیل عدم تطابق تسلسل نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadAHSGIdViaTruckAHSGIdNotAllowedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مشخصات ناوگان با بار انتخاب شده تطابق ندارد"
                End Get
            End Property
        End Class

        Public Class RegisteredLoadAllocationIsRepetitiousException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تخصیص مورد نظر تکراری است"
                End Get
            End Property
        End Class

        Public Class LoadAllocationMaximumAllowedNumberReachedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سقف تعداد تخصیص کامل شده است"
                End Get
            End Property
        End Class

        Public Class LoadAllocationNotAllowedBecauseCarHasBlackListException
            Inherits ApplicationException
            Private _Message As String = String.Empty
            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان صدور تخصیص وجود ندارد" + vbCrLf + _Message + vbCrLf + "خودرو در لیست سیاه قرار دارد"
                End Get
            End Property
        End Class

        Public Class LoadAllocationIdNotPairWithDriverException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    'عدم مطابقت شماره تخصیص ارسالی با حساب کاربر مرتبط با راننده
                    Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.LoadAllocationIdNotPairWithDriver).MsgContent
                End Get
            End Property
        End Class

        Public Class LastLoadPermissionIssuedforThisTurnException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "قبلا برای این نوبت مجوز بارگیری صادر شده است"
                End Get
            End Property
        End Class

        Public Class SendSMSLoadAllocationsLoadPermissionRegisteringFailedFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس اعلان خرابی سرویس آزادسازی بار با خطا مواجه شد" + _Message
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class LoadAllocationConditionsNotEstablishedException
            Inherits BPTException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "عدم برقراری شرایط تخصیص بار" + "عدم تطابق صف نوبت ، وضعیت بار ، زیرگروه اعلام بار ، درخواست کننده تخصیص و یا وضعیت نوبت"
                End Get
            End Property
        End Class

        Public Class LoadAllocationTimeNotReachedException
            Inherits BPTException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان انتخاب بار آغاز نشده است"
                End Get
            End Property
        End Class

        Public Class LoadAllocationNotFoundException
            Inherits BPTException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تخصیص با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadAllocation
        Public Property LAId As Int64
        Public Property LoadId As Int64
        Public Property TurnId As Int64
        Public Property LAStatusId As Int64
        Public Property LANote As String
        Public Property Priority As Int16
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadAllocationManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Private Sub LoadAllocationConditionControl(YourAHSGId As Int64, YourSeqTId As Int64, YourNativenessTypeId As Int64, YourLoadStatusId As Int64, YourRequesterId As Int64, YourTurnStatusId As Int64)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select OId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions as LoadsAllocationConditions
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On LoadsAllocationConditions.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On LoadsAllocationConditions.SeqTId=SequentialTurns.SeqTId 
                        Inner Join R2Primary.dbo.TblRequesters as Requesters On LoadsAllocationConditions.RequesterId=Requesters.RqId 
                      Where LoadsAllocationConditions.AHSGId=" & YourAHSGId & " and LoadsAllocationConditions.SeqTId=" & YourSeqTId & " and LoadsAllocationConditions.NativenessTypeId=" & YourNativenessTypeId & " and   
                            LoadsAllocationConditions.LoadStatusId=" & YourLoadStatusId & " and LoadsAllocationConditions.RequesterId=" & YourRequesterId & " and LoadsAllocationConditions.TurnStatusId=" & YourTurnStatusId & "  and
                            AnnouncementSubGroups.Active=1 and SequentialTurns.Active=1 and Requesters.Active=1", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LoadAllocationConditionsNotEstablishedException
                End If
            Catch ex As LoadAllocationConditionsNotEstablishedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationRegistering(YourLoadId As Int64, YourTurnId As Int64, YourSeqTId As Int64, YourNativenessTypeId As Int64, YourTurnStatusId As Int64, YourRequesterId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)

                'اطلاعات بار
                Dim Load = InstanceLoad.GetLoadForLoadAllocationProccess(YourLoadId)
                'کنترل برقراری شرایط تخصیص بار
                LoadAllocationConditionControl(Load.AnnouncementSubGroupId, YourSeqTId, YourNativenessTypeId, Load.LoadStatusId, YourRequesterId, YourTurnStatusId)
                'کنترل مجوز کنترل تایمینگ در تخصیص بار با توجه به وضعیت بار
                If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.LoadAllocationUseTimeHandlingByLoadStatus, Load.LoadStatusId, 0) Then
                    'آیا زمان تخصیص بار برای زیرگروه مورد نظر فرارسیده است
                    If InstanceTiming.GetTiming(Load.AnnouncementGroupId, Load.AnnouncementSubGroupId, _DateTimeService.DateTimeServ.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                        Throw New LoadAllocationTimeNotReachedException
                    End If
                End If

                Dim Param As SqlClient.SqlParameter
                Param = New SqlParameter("@YourTurnId", SqlDbType.BigInt) : Param.Value = YourTurnId
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTargetTurnStatusId", SqlDbType.TinyInt) : Param.Value = TurnStatuses.UsedLoadAllocationRegistered
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTurnAccountingTypeId", SqlDbType.TinyInt) : Param.Value = TurnAccouningTypes.LoadAllocation
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourLoadId", SqlDbType.BigInt) : Param.Value = YourLoadId
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : Param.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Allocating
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourLoadAllocationStatusId", SqlDbType.TinyInt) : Param.Value = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourUserId", SqlDbType.BigInt) : Param.Value = YourSoftwareUser.UserId
                CmdSql.Parameters.Add(Param)
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadAllocationRegistering"
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As LoadAllocationTimeNotReachedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As LoadAllocationConditionsNotEstablishedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As LoadForLoadAllocationProcessNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationRegisteringForTruckDriver(YourSoftwareUser As R2CoreSoftwareUser, YourLoadId As Int64, YourRequesterId As Int64)
            Try
                Dim InstanceCache = New R2Core.Caching.R2CoreCacheManager
                Dim Value = DirectCast(InstanceCache.GetCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TruckDriverTurnInfo).CacheTypeName + YourSoftwareUser.UserId.ToString, R2CoreTransportationAndLoadNotificationCatchDataBases.TruckDriverInformation), StackExchange.Redis.RedisValue)
                If Value.IsNullOrEmpty Then Throw New BaseInfFailedException
                Dim TurnInfo = JsonConvert.DeserializeObject(Of R2CoreTransportationAndLoadNotificationTruckDriverTurnInfo)(Value)

                LoadAllocationRegistering(YourLoadId, TurnInfo.TurnId, TurnInfo.SeqTId, TurnInfo.NativenessTypeId, TurnInfo.TurnStatusId, YourRequesterId, YourSoftwareUser)

            Catch ex As DataBaseException
                Throw ex
            Catch ex As BaseInfFailedException
                Throw ex
            Catch ex As LoadAllocationTimeNotReachedException
                Throw ex
            Catch ex As LoadAllocationConditionsNotEstablishedException
                Throw ex
            Catch ex As LoadForLoadAllocationProcessNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationRegisteringforAdministrator(YourTruckId As Int64, YourTruckDriverId As Int64, YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser, YourRequesterId As Int64)
            Try
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService)
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager()
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)

                Dim Turn = InstanceTurns.GetLastActiveTurnfromTruckId(YourTruckId, False)
                Dim SeqTId = InstanceSequentialTurns.GetSequentialTurnIdfromTurn(Turn)

                InstanceLoadAllocation.LoadAllocationRegistering(YourLoadId, Turn.TurnId, SeqTId, InstanceTruckNativeness.GetTruckNativeness(YourTruckId, True).TruckNativenessTypeId, TurnStatuses.Registered, YourRequesterId, YourSoftwareUser)

            Catch ex As DataBaseException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As SequentialTurnNotFoundException
                Throw ex
            Catch ex As LoadAllocationTimeNotReachedException
                Throw ex
            Catch ex As LoadAllocationConditionsNotEstablishedException
                Throw ex
            Catch ex As LoadForLoadAllocationProcessNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetTruckDriverLoadAllocationsForRepriority(YourSoftwareUser As R2CoreSoftwareUser) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Declare @LastTurnId int
                   Select Top 1 @LastTurnId=Turns.nEnterExitId 
                   from  R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                     Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                     Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                     Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                     Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                     Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                   Where SoftwareUsers.UserId=" & YourSoftwareUser.UserId & " and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPersons.snRelation=2 and
                         (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                         ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                   Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc

                   Select LoadAllocations.LAId as LAId,LoadAllocations.Priority as Priority,LoadAllocationStatuses.LoadAllocationStatusColor,LoadAllocations.nEstelamId as LoadId,Products.strGoodName as GoodTitle,SourceCities.StrCityName SourceCityTitle,TargetCities.StrCityName as TargetCityTitle,Loads.nTonaj as Tonaj,
                          Companies.strCompName as TransportCompanyTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,Loads.strDescription as Description,Loads.strAddress as Address,Loads.strBarName as Recipient,Loads.TPTParamsJoint     
                   from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
                     Inner Join DBTransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                     Inner Join DBTransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                     Inner Join DBTransport.dbo.tbCity as SourceCities On Loads.nBarSource=SourceCities.nCityCode 
                     Inner Join DBTransport.dbo.tbCity as TargetCities On Loads.nCityCode=TargetCities.nCityCode 
                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId
                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId
                     Inner Join DBTransport.dbo.tbCompany as Companies On Loads.nCompCode=Companies.nCompCode 
                   Where LoadAllocations.TurnId=@LastTurnId and LoadAllocations.DateShamsi=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE()) and LoadAllocations.LAStatusId=" & LoadAllocation.R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & "
                   Order By LoadAllocations.Priority Asc for JSON PATH", 0, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckDriverLoadAllocationsRecords(YourSoftwareUser As R2CoreSoftwareUser) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Declare @LastTurnId int
                   Select Top 1 @LastTurnId=Turns.nEnterExitId 
                   from  R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                     Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                     Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                     Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                     Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                     Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
                   Where SoftwareUsers.UserId=" & YourSoftwareUser.UserId & " and EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPersons.snRelation=2 and
                         (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                         ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                   Order By CarAndPersons.nIDCarAndPerson Desc,Turns.nEnterExitId Desc

                   Select LoadAllocations.LAId as LAId,LoadAllocations.Priority as Priority,LoadAllocationStatuses.LoadAllocationStatusColor,LoadAllocations.nEstelamId as LoadId,Products.strGoodName as GoodTitle,SourceCities.StrCityName SourceCityTitle,TargetCities.StrCityName as TargetCityTitle,Loads.nTonaj as Tonaj,
                          Companies.strCompName as TransportCompanyTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,Loads.strDescription as Description,Loads.strAddress as Address,Loads.strBarName as Recipient,Loads.TPTParamsJoint     
                   from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
           			 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocationStatuses as LoadAllocationStatuses On LoadAllocations.LAStatusId=LoadAllocationStatuses.LoadAllocationStatusId 
                     Inner Join DBTransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                     Inner Join DBTransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                     Inner Join DBTransport.dbo.tbCity as SourceCities On Loads.nBarSource=SourceCities.nCityCode 
                     Inner Join DBTransport.dbo.tbCity as TargetCities On Loads.nCityCode=TargetCities.nCityCode 
                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId
                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId
                     Inner Join DBTransport.dbo.tbCompany as Companies On Loads.nCompCode=Companies.nCompCode 
                   Where LoadAllocations.TurnId=@LastTurnId and LoadAllocations.DateShamsi=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE())
                   Order By LoadAllocations.LAStatusId Asc for JSON PATH", 60, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadAllocation(YourLAId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoadAllocation
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                     "Select LoadAllocations.LAId,LoadAllocations.nEstelamId as LoadId,LoadAllocations.TurnId,LoadAllocations.LAStatusId,LoadAllocations.LANote,LoadAllocations.Priority,
                             LoadAllocations.DateTimeMilladi,LoadAllocations.DateShamsi,LoadAllocations.Time,LoadAllocations.UserId 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.LAId=" & YourLAId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New LoadAllocationNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select LoadAllocations.LAId,LoadAllocations.nEstelamId as LoadId,LoadAllocations.TurnId,LoadAllocations.LAStatusId,LoadAllocations.LANote,LoadAllocations.Priority,
                             LoadAllocations.DateTimeMilladi,LoadAllocations.DateShamsi,LoadAllocations.Time,LoadAllocations.UserId 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                      Where LoadAllocations.LAId=" & YourLAId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadAllocationNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationLoadAllocation With {.LAId = DS.Tables(0).Rows(0).Item("LAId"), .LAStatusId = DS.Tables(0).Rows(0).Item("LAStatusId"), .LANote = DS.Tables(0).Rows(0).Item("LANote").trim, .LoadId = DS.Tables(0).Rows(0).Item("LoadId"), .TurnId = DS.Tables(0).Rows(0).Item("TurnId"), .Priority = DS.Tables(0).Rows(0).Item("Priority"), .UserId = DS.Tables(0).Rows(0).Item("UserId"), .DateTimeMilladi = DS.Tables(0).Rows(0).Item("DateTimeMilladi"), .DateShamsi = DS.Tables(0).Rows(0).Item("DateShamsi"), .Time = DS.Tables(0).Rows(0).Item("Time")}
            Catch ex As LoadAllocationNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTravelTimeforLoadAllocation(YourSoftwareUserId As Int64, YourLAId As Int64) As Int64
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(New R2DateTimeService)
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)
                Dim InstanceTravelTime = New R2CoreTransportationAndLoadNotificationTravelTimeManager()
                Dim LoadAllocatin = GetLoadAllocation(YourLAId, False)
                Dim Truck = InstanceTrucks.GetTruckBySoftwareUser(YourSoftwareUserId, False)
                Dim Load = InstanceLoad.GetLoadSimpleModel(LoadAllocatin.LoadId, True)
                Return InstanceTravelTime.GetTravelTime(Truck.LoaderTypeId, Load.SourceCityId, Load.TargetCityId, False).TravelTime
            Catch ex As LoadAllocationNotFoundException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As LoadNotFoundException
                Throw ex
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub IncreasePriority(YourLAId As Int64, YourLoadId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)

                'اطلاعات بار
                Dim Load = InstanceLoad.GetLoadForLoadAllocationProccess(YourLoadId)

                'آیا زمان تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If InstanceTiming.GetTiming(Load.AnnouncementGroupId, Load.AnnouncementSubGroupId, _DateTimeService.DateTimeServ.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                    Throw New TimingNotReachedException
                End If

                Dim Param As SqlClient.SqlParameter
                Param = New SqlParameter("@YourLAId", SqlDbType.BigInt) : Param.Value = YourLAId
                CmdSql.Parameters.Add(Param)
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.IncreasePriorityOfLoadAllocation"
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As TimingNotReachedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DecreasePriority(YourLAId As Int64, YourLoadId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)

                'اطلاعات بار
                Dim Load = InstanceLoad.GetLoadForLoadAllocationProccess(YourLoadId)

                'آیا زمان تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If InstanceTiming.GetTiming(Load.AnnouncementGroupId, Load.AnnouncementSubGroupId, _DateTimeService.DateTimeServ.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                    Throw New TimingNotReachedException
                End If

                Dim Param As SqlClient.SqlParameter
                Param = New SqlParameter("@YourLAId", SqlDbType.BigInt) : Param.Value = YourLAId
                CmdSql.Parameters.Add(Param)
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.DecreasePriorityOfLoadAllocation"
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As TimingNotReachedException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationCancelling(YourLAId As Int64, YourLoadId As Int64, YourCancellStatusId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(New R2DateTimeService)

                'اطلاعات بار
                Dim Load = InstanceLoad.GetLoadForLoadAllocationProccess(YourLoadId)

                'آیا زمان تخصیص بار برای زیرگروه سالن مورد نظر فرارسیده است
                If InstanceTiming.GetTiming(Load.AnnouncementGroupId, Load.AnnouncementSubGroupId, _DateTimeService.DateTimeServ.GetCurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                    Throw New TimingNotReachedException
                End If

                Dim Param As SqlClient.SqlParameter
                Param = New SqlParameter("@YourLAId", SqlDbType.BigInt) : Param.Value = YourLAId
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTargetLoadAllocationStatusId", SqlDbType.TinyInt) : Param.Value = R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.CancelledUser
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTargetTurnStatusId", SqlDbType.TinyInt) : Param.Value = TurnStatuses.ResuscitationLoadAllocationCancelled
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourTargetLoadAccountingTypeId", SqlDbType.TinyInt) : Param.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.AllocationCancelling
                CmdSql.Parameters.Add(Param)
                Param = New SqlParameter("@YourUserId", SqlDbType.BigInt) : Param.Value = YourSoftwareUser.UserId
                CmdSql.Parameters.Add(Param)
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadAllocationCancelling"
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As TimingNotReachedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetLoadIdfromLoadAllocationId(YourLoadAllocationId As Int64) As Int64
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select nEstelamId as LoadId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Where LAId=" & YourLoadAllocationId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LoadAllocationsNotFoundException
                End If
                Return DS.Tables(0).Rows(0).Item("LoadId")
            Catch ex As LoadAllocationsNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnIdfromLoadAllocationId(YourLoadAllocationId As Int64) As Int64
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations Where LAId=" & YourLoadAllocationId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LoadAllocationsNotFoundException
                End If
                Return DS.Tables(0).Rows(0).Item("TurnId")
            Catch ex As LoadAllocationsNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetPrimaryTurn(YourLoadId As Int64, YourExcludedTurnId As Int64) As R2CoreTransportationAndLoadNotificationTurn
            Try
                Dim DS As DataSet = Nothing
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 LoadAllocations.TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocations
                          Inner Join dbtransport.dbo.tbEnterExit as Turns On LoadAllocations.TurnId=Turns.nEnterExitId 
                          Inner Join dbtransport.dbo.tbElam as Loads On LoadAllocations.nEstelamId=Loads.nEstelamID 
                       Where LoadAllocations.DateShamsi='" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull() & "' and (LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.Registered & " or LoadAllocations.LAStatusId=" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionFailed & ") and LoadAllocations.nEstelamId =" & YourLoadId & " and  LoadAllocations.LAStatusId<>" & R2CoreTransportationAndLoadNotificationLoadAllocationStatuses.PermissionCancelled & " and 
                             (Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and (Turns.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & " or Turns.LoadPermissionStatus=" & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.Cancelled & ") and Turns.nEnterExitId > " & YourExcludedTurnId & " and
                             (Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & " or Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & " or Loads.LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & ") and Loads.nCarNum>=1 and
                        	 Turns.nEnterExitId not in (Select TurnId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAllocations as LoadAllocationsX where LoadAllocationsX.nEstelamId =" & YourLoadId & "  and LoadAllocationsX.LAStatusId=5)
                       Order By LoadAllocations.TurnId Asc", 0, DS, New Boolean).GetRecordsCount = 0 Then Throw New PrimaryTurnNotFoundException
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                Return InstanceTurns.GetTurn(Convert.ToInt64(DS.Tables(0).Rows(0).Item("TurnId")), True)
            Catch ex As PrimaryTurnNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function LoadAllocateToOther(YourLoadAllocationId As Int64, YourRequesterId As Int64, YourSoftwareUser As R2CoreSoftwareUser) As R2CoreTransportationAndLoadNotificationTurn
            Try
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService)
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                'تخصیص به راننده دیگر
                Dim LoadId = InstanceLoadAllocation.GetLoadIdfromLoadAllocationId(YourLoadAllocationId)
                Dim TurnId = InstanceLoadAllocation.GetTurnIdfromLoadAllocationId(YourLoadAllocationId)
                Dim PrimaryTurn As R2CoreTransportationAndLoadNotificationTurn
                PrimaryTurn = GetPrimaryTurn(LoadId, TurnId)
                Dim TurnInfo = InstanceTurns.GetTurnInfo(PrimaryTurn.TurnId)
                InstanceLoadAllocation.LoadAllocationRegistering(LoadId, PrimaryTurn.TurnId, TurnInfo.SeqTId, TurnInfo.NativenessTypeId, TurnInfo.TurnStatusId, YourRequesterId, YourSoftwareUser)
                Return PrimaryTurn

            Catch ex As LoadAllocationsNotFoundException
                Throw ex
            Catch ex As PrimaryTurnNotFoundException
                Throw ex
            Catch ex As TurnInfoNotFoundException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As LoadAllocationTimeNotReachedException
                Throw ex
            Catch ex As LoadAllocationConditionsNotEstablishedException
                Throw ex
            Catch ex As LoadForLoadAllocationProcessNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

    End Class



End Namespace
