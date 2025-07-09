
Imports System.Data.OleDb
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.Net.NetworkInformation
Imports System.Text
Imports System.Windows.Forms

Imports R2Core.BaseStandardClass
Imports R2Core.ComputerMessagesManagement
Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2Core.ExceptionManagement
Imports R2Core.FileShareRawGroupsManagement
Imports R2Core.LoggingManagement
Imports R2Core.NetworkInternetManagement.Exceptions
Imports R2Core.DesktopProcessesManagement
Imports R2Core.PublicProc
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.ReportsManagement
Imports R2Core.SoftwareUserManagement
Imports R2Core.EntityRelationManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreParkingSystem.BlackList
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.Drivers
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
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
Imports R2CoreTransportationAndLoadNotification.LoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportCompanies
Imports R2CoreTransportationAndLoadNotification.TransportCompanies.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadTargets
Imports R2CoreTransportationAndLoadNotification.TransportTariffs
Imports R2CoreTransportationAndLoadNotification.TransportTariffs.Exceptions
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
Imports R2CoreTransportationAndLoadNotification.Turns.TurnPrinting
Imports R2CoreTransportationAndLoadNotification.Turns.TurnRegisterRequest
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports R2CoreTransportationAndLoadNotification.LoadTargets.Exceptions
Imports R2CoreTransportationAndLoadNotification.Rmto
Imports R2CoreTransportationAndLoadNotification.TruckLoaderTypes
Imports R2CoreTransportationAndLoadNotification.TruckLoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.EntityRelations
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.RequesterManagement
Imports R2CoreParkingSystem.PermissionManagement
Imports R2CoreParkingSystem.EntityManagement
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports R2Core.MoneyWallet.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters.Exceptions
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2CoreParkingSystem.SMS.SMSTypes
Imports R2CoreParkingSystem
Imports R2CoreParkingSystem.PredefinedMessagesManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness
Imports R2CoreTransportationAndLoadNotification.Goods
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2CoreTransportationAndLoadNotification.LoadSources
Imports System.Net
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement

Namespace Rmto
    Public MustInherit Class RmtoWebService
        Private Shared Service As RmtoWS.PKG_RMTO_WSService
        Public Shared RmtoURL As String = "http://smartcard.rmto.ir"

        Public Enum InfoType
            GET_DRIVER_BY_SHC = 0
            GET_DRIVER_BY_SHM = 1
            GET_DRIVER_BY_SHP = 2
            GET_FREIGHTER_BY_SHC = 3
            GET_FREIGHTER_BY_VIN = 4
            GET_FREIGHTER_BY_SHP = 5
            GET_PASSENGER_BY_SHC = 6
            GET_PASSENGER_BY_VIN = 7
            GET_PASSENGER_BY_SHP = 8
        End Enum

        Private Shared Function GetInf(ByVal YourInfoType As InfoType, ByVal YourDFP As String) As String()
            Try
                Dim InstanceLogging = New R2CoreInstanceLoggingManager
                If Not R2Core.NetworkInternetManagement.R2CoreMClassComputerMessagesManagement.IsInternetAvailable() Then
                    Throw New InternetIsnotAvailableException
                End If

                Authentication()

                'If InstanceLogging.GetNSSLogType(R2CoreTransportationAndLoadNotificationLogType.RmtoWebServiceRequest).Active Then
                '    InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreTransportationAndLoadNotificationLogType.RmtoWebServiceRequest, InstanceLogging.GetNSSLogType(R2CoreTransportationAndLoadNotificationLogType.RmtoWebServiceRequest).LogTitle, "InfoType=" + YourInfoType.ToString(), "YourDFP=" + YourDFP, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
                'End If
                Dim Result() As String
                If YourInfoType = InfoType.GET_DRIVER_BY_SHC Then
                    Result = Service.RMTO_WEB_SERVICES(YourDFP, "", "", "", "", "", "", "", "", "", "2043148", 41, "Biinfo878").Split(";")
                    'Return Service.RMTO_WEB_SERVICES("Biinfo878", 41, "2043148", "", "", "", "", "", "", "", "", "", YourDFP).Split(";")
                ElseIf YourInfoType = InfoType.GET_DRIVER_BY_SHM Then
                    Result = Service.RMTO_WEB_SERVICES(YourDFP, "", "", "", "", "", "", "", "", "", "2043148", 3, "Biinfo878").Split(";")
                    'Return Service.RMTO_WEB_SERVICES("Biinfo878", 3, "2043148", "", "", "", "", "", "", "", "", "", YourDFP).Split(";")
                ElseIf YourInfoType = InfoType.GET_FREIGHTER_BY_SHC Then
                    Result = Service.RMTO_WEB_SERVICES(YourDFP, "", "", "", "", "", "", "", "", "", "2043148", 4, "Biinfo878").Split(";")
                    'Return Service.RMTO_WEB_SERVICES("Biinfo878", 4, "2043148", "", "", "", "", "", "", "", "", "", YourDFP).Split(";")
                ElseIf YourInfoType = InfoType.GET_FREIGHTER_BY_VIN Then
                    Result = Service.RMTO_WEB_SERVICES(YourDFP, "", "", "", "", "", "", "", "", "", "2043148", 6, "Biinfo878").Split(";")
                    'Return Service.RMTO_WEB_SERVICES("Biinfo878", 6, "2043148", "", "", "", "", "", "", "", "", "", YourDFP).Split(";")
                Else
                    Throw New RMTOWebServiceSmartCardInvalidException
                End If
                If Result(0) = "-1" Then
                    Throw New RMTOWebServiceSmartCardInvalidException
                End If
                Return Result

            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As RMTOWebServiceSmartCardInvalidException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetNSSTruckDriver(YourNationalCode As String) As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
            Try
                Dim ComposeString As String() = GetInf(InfoType.GET_DRIVER_BY_SHM, YourNationalCode)
                Dim NSSTruckDriver As New R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure
                NSSTruckDriver.NSSDriver = New R2StandardDriverStructure()
                NSSTruckDriver.StrSmartCardNo = ComposeString(1).Split(":")(1)
                NSSTruckDriver.NSSDriver.StrFatherName = ComposeString(6).Split(":")(1)
                NSSTruckDriver.NSSDriver.StrNationalCode = ComposeString(3).Split(":")(1)
                NSSTruckDriver.NSSDriver.StrPersonFullName = ComposeString(4).Split(":")(1) + ";" + ComposeString(5).Split(":")(1)
                NSSTruckDriver.NSSDriver.strDrivingLicenceNo = ComposeString(9).Split(":")(1)
                Return NSSTruckDriver
            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As RMTOWebServiceSmartCardInvalidException
                Throw ex
            Catch ex As GetDataException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function ISTruckDriverSmartCardActive(YourSmartCardNo As String) As Boolean
            Try
                Dim ComposeString As String() = GetInf(InfoType.GET_DRIVER_BY_SHC, YourSmartCardNo)
                Dim Active As Boolean = ComposeString(11).Split(":")(1)
                Return Active
            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As RMTOWebServiceSmartCardInvalidException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetNSSTruck(YourSmartCardNo As String) As R2CoreTransportationAndLoadNotificationStandardTruckStructure
            Try
                Dim ComposeString As String() = GetInf(InfoType.GET_FREIGHTER_BY_SHC, YourSmartCardNo)
                Dim NSSTruck As New R2CoreTransportationAndLoadNotificationStandardTruckStructure
                NSSTruck.NSSCar = New R2StandardCarStructure()
                NSSTruck.SmartCardNo = YourSmartCardNo
                NSSTruck.NSSCar.StrCarNo = ComposeString(5).Split(":")(1) + ComposeString(6).Split(":")(1) + ComposeString(7).Split(":")(1)
                NSSTruck.NSSCar.StrCarSerialNo = ComposeString(3).Split(":")(1)
                NSSTruck.NSSCar.nIdCity = R2CoreParkingSystemMClassCitys.GetnCityCodeFromStrCityName(ComposeString(4).Split(":")(1))
                NSSTruck.NSSCar.snCarType = ComposeString(12).Split(":")(1)
                Return NSSTruck
            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As RMTOWebServiceSmartCardInvalidException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As GetDataException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function ISTruckSmartCardActive(YourSmartCardNo As String) As Boolean
            Try
                Dim ComposeString As String() = GetInf(InfoType.GET_FREIGHTER_BY_SHC, YourSmartCardNo)
                Dim Active As Boolean = ComposeString(8).Split(":")(1)
                Return Active
            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As RMTOWebServiceSmartCardInvalidException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Private Shared Sub Authentication()
            Try
                If Not R2Core.NetworkInternetManagement.R2CoreMClassComputerMessagesManagement.IsInternetAvailable() Then
                    Throw New InternetIsnotAvailableException
                End If
                ''Dim UserName As String = "tr_web_service"
                ''Dim Password As String = "tr_web_service123"
                Dim UserName As String = "Biinfo878"
                Dim Password As String = "2043148"
                Service = New RmtoWS.PKG_RMTO_WSService()
                Dim Credential As System.Net.CredentialCache = New System.Net.CredentialCache
                Credential.Add(New Uri(Service.Url), "Basic", New System.Net.NetworkCredential(UserName, Password))
                Service.Credentials = Credential
            Catch ex As ConnectionIsNotAvailableException
                Throw ex
            Catch ex As InternetIsnotAvailableException
                Throw ex
            Catch ex As WebException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class RMTOWebServiceSmartCardInvalidException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "شماره وارد شده نادرست است یا سایت کارت هوشمند در دسترس نیست"
            End Get
        End Property
    End Class

    Public Class RMTOSmartCardSiteIsNotAvailableException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "دریافت خطا از سمت وب سرویس سازمان"
            End Get
        End Property
    End Class


End Namespace

Namespace ReportManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationReports
        Inherits R2CoreReports

        Public Shared ReadOnly BillOfLadingControlReport As Int64 = 29
        Public Shared ReadOnly BillOfLadingControlInfractionsReport As Int64 = 30

    End Class

    Public Class R2CoreTransportationAndLoadNotificationReportsManagement
        Private Shared _DateTime As New R2DateTime

        Public Shared Sub ReportingInformationProviderBillOfLadingControlReport(YourBLCId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimaryReportsSqlConnection).GetConnection()
            Try
                Dim _DateTime As R2DateTime = New R2DateTime
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Delete R2PrimaryReports.dbo.TblBillOfLadingControlsReport" : CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into R2PrimaryReports.dbo.TblBillOfLadingControlsReport
                                    Select BLC.BLCId,BLC.BLCTitle,BLC.TCOId,BLC.TCOTitle,BLC.DateShamsi,BLC.Time,SoftwareUser.Username,'' as ReportDateTime,
                                           BLCDetails.BLCIndex,BLCDetails.BLNo,BLCDetails.BlSerial,BLCDetails.BLDateShamsi,BLCDetails.BLTime,BLCDetails.BLSenderTitle,BLCDetails.BLSenderNationalCode,BLCDetails.BLReceiverTitle,
	                                       BLCDetails.BLReceiverNationalCode,BLCDetails.BLFirstTruckDriver,BLCDetails.BLTruckNo,BLCDetails.BLTruckSerialNo,BLCDetails.BLTruckSmartCardNo,BLCDetails.BLPrice,
	                                       BLCDetails.BLSourceTitle,BLCDetails.BLTargetTitle,BLCDetails.BLGoodTitle,BLCDetails.BLWeight,BLCDetails.BLLoaderTypeTitle
                                    from R2PrimaryTransportationAndLoadNotification.dbo.TblBillOfLadingControls as BLC
                                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblBillOfLadingControlDetails as BLCDetails On BLC.BLCId=BLCDetails.BLCId
                                       Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUser On BLC.UserId=SoftwareUser.UserId
                                    Where BLC.BLCId=" & YourBLCId & " Order By BLCDetails.BLCIndex "
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update R2PrimaryReports.dbo.TblBillOfLadingControlsReport Set ReportDateTime='" & _DateTime.GetCurrentDateShamsiFull().Replace("/", "") + " - " + _DateTime.GetCurrentTime() & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub ReportingInformationProviderBillOfLadingControlInfractionReport(YourBLCIId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimaryReportsSqlConnection).GetConnection()
            Try
                Dim _DateTime As R2DateTime = New R2DateTime
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Delete R2PrimaryReports.dbo.TblBillOfLadingControlInfractionsReport" : CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into R2PrimaryReports.dbo.TblBillOfLadingControlInfractionsReport
                                          Select BLCInfraction.BLCIId,BLCInfraction.BLCId,BLC.BLCTitle,BLCInfraction.DateShamsi,BLCInfraction.Time,SoftwareUser.UserName,BLCInfraction.Note,'' as ReportDateTime,
                                                 BLCInfractionDetail.BLCIIndex,BLCInfractionDetail.TruckAnalyze,BLCInfractionDetail.TonajAnalyze,BLCInfractionDetail.SenderAnalyze,BLCInfractionDetail.RecieverAnalyze,
	                                             BLCInfractionDetail.SameSenderRecieverAnalyze,BLCInfractionDetail.LoadSourceInvalid 
                                          from R2PrimaryTransportationAndLoadNotification.dbo.TblBillOfLadingControlInfractions as BLCInfraction
                                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblBillOfLadingControlInfractionDetails as BLCInfractionDetail On BLCInfraction.BLCIId=BLCInfractionDetail.BLCIId
                                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblBillOfLadingControls as BLC On BLCInfraction.BLCId=BLC.BLCId
                                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUser On BLCInfraction.UserId=SoftwareUser.UserId
                                          Where BLCInfraction.BLCIId=" & YourBLCIId & " Order By BLCInfractionDetail.BLCIIndex"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update R2PrimaryReports.dbo.TblBillOfLadingControlInfractionsReport Set ReportDateTime='" & _DateTime.GetCurrentDateShamsiFull().Replace("/", "") + " - " + _DateTime.GetCurrentTime() & "'"
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




End Namespace

Namespace Logging

    Public MustInherit Class R2CoreTransportationAndLoadNotificationLogType
        Inherits R2CoreLogType

        Public Shared ReadOnly Property LoadCapacitorSedimentingFailed As Int64 = 11
        Public Shared ReadOnly Property TurnExpirationFailed As Int64 = 12
        Public Shared ReadOnly Property RmtoWebServiceRequest As Int64 = 13
        Public Shared ReadOnly Property LoadAllocationsAccessStatistics As Int64 = 14
        Public Shared ReadOnly Property TransferringTommorowLoads As Int64 = 15
        Public Shared ReadOnly Property ATISMobileMoneyWalletsCharging As Int64 = 16
    End Class

End Namespace

Namespace ComputerMessages

    Public MustInherit Class R2CoreTransportationAndLoadNotificationComputerMessageTypes
        Inherits R2Core.ComputerMessagesManagement.R2CoreComputerMessageTypes
        Public Shared ReadOnly EmergencyTurnRegisterRequestConfirmation As Int64 = 2
        Public Shared ReadOnly TurnPrint As Int64 = 3
        Public Shared ReadOnly ReserveTurnRegisterRequestConfirmation As Int64 = 7
    End Class

End Namespace

Namespace FileShareRawGroupsManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationRawGroups
        Inherits R2Core.FileShareRawGroupsManagement.R2CoreRawGroups

        Public Shared ReadOnly TurnRegisterRequestAttachements As Int64 = 5

        Public Shared ReadOnly DriverSelfDeclarations As Int64 = 8

    End Class

End Namespace

Namespace ConfigurationsManagement
    Public MustInherit Class R2CoreTransportationAndLoadNotificationConfigurations
        Inherits R2CoreConfigurations
        Public Shared ReadOnly Property TurnControlling As Int64 = 34
        Public Shared ReadOnly Property TWS As Int64 = 51
        Public Shared ReadOnly Property AnnouncementHallMonitoring As Int64 = 52
        Public Shared ReadOnly Property LoadCapacitorLoadManipulationSetting As Int64 = 54
        Public Shared ReadOnly Property AnnouncementHallAnnounceTime As Int64 = 55
        Public Shared ReadOnly Property AnnouncementsTurnCancellationSetting As Int64 = 56
        Public Shared ReadOnly Property AnnouncementsTruckDriverAttendance As Int64 = 57
        Public Shared ReadOnly Property DefaultTransportationAndLoadNotificationConfigs As Int64 = 58
        Public Shared ReadOnly Property AnnouncementsLoadAllocationsLoadPermissionRegisteringSetting As Int64 = 59
        Public Shared ReadOnly Property AnnouncementsTurnRegisteringSetting As Int64 = 61
        Public Shared ReadOnly Property AnnouncementsLoadPermissionsSetting As Int64 = 62
        Public Shared ReadOnly Property AnnouncementsLoadSedimentationSetting As Int64 = 63
        Public Shared ReadOnly Property AnnouncementsAutomaticProcessesTiming As Int64 = 68
        Public Shared ReadOnly Property AnnouncementsLoadAllocationSetting As Int64 = 69
        Public Shared ReadOnly Property TommorowLoads As Int64 = 71
        Public Shared ReadOnly Property DriverSelfDeclarationSetting As Int64 = 86
        Public Shared ReadOnly Property XXX As Int64 = 88
        Public Shared ReadOnly Property IndigenousTrucks As Int64 = 89
        Public Shared ReadOnly Property AnnouncementsLoadCapacitorControl As Int64 = 91
        Public Shared ReadOnly Property BillOfLading As Int64 = 92
        Public Shared ReadOnly Property LoadingDischargingPlaces As Int64 = 93


    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
        Public Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId).trim
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement
        'Inherits R2CoreMClassConfigurationManagement

        Public Shared Function GetConfigOnLine(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "")
                Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection()
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Ds.Tables(0).Rows(0).Item("CValue").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt32(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int32
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigDouble(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Double
            Try
                Dim xRong As Double = GetConfig(YourCId, YourAHId, YourIndex)
                Return xRong
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigByte(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Byte
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourCValue As String)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Set CValue = '" & YourCValue & "' Where CId=" & YourCId & " and AHId=" & YourAHId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourIndex As Int64, YourCValue As String)
            Try
                Dim CurrentConfigValue As String = GetConfigOnLine(YourCId, YourAHId)
                Dim CurrentConfigValueSplitted As String() = Split(CurrentConfigValue, ";")
                Dim SB As New StringBuilder
                For Loopx As Int64 = 0 To CurrentConfigValueSplitted.Length - 1
                    If Loopx = YourIndex Then
                        SB.Append(YourCValue)
                    Else
                        SB.Append(CurrentConfigValueSplitted(Loopx))
                    End If
                    If Loopx < CurrentConfigValueSplitted.Length - 1 Then SB.Append(";")
                Next
                SetConfiguration(YourCId, YourAHId, SB.ToString())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    Namespace Exceptions
        Public Class ConfigurationOfAnnouncementHallNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پیکربندی مرتبط با سالن اعلام بار یافت نشد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace TurnCancellation
    Public Class R2CoreTransportationAndLoadNotificationTurnCancellationManager
        Private _DateTime As New R2DateTime
        Private Const TurnCancellationLoadColor = "OrangeRed"
        Private Const NonTurnCancellationLoadColor = "Green"

        Private Function IsLoadTargetforTurnCancellation(YourLoadTargetId As Int64) As Boolean
            Try
                Dim DS As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 1 LoadTargetId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadTargetsforTurnCancellation 
                        Where LoadTargetId=" & YourLoadTargetId & " and Active=1", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsLoadforTurnCancellation(YourNSSLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
            Try
                Dim InstanceConfigurations As New R2CoreInstanceConfigurationManager
                If IsLoadTargetforTurnCancellation(YourNSSLoad.nCityCode) Then
                    If YourNSSLoad.nTonaj <= InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 8) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadforTurnCancellationRegistering(nEstelamId As Int64, YourCancellationFlag As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim myDescription As String = String.Empty
                If YourCancellationFlag Then
                    myDescription = TurnCancellationLoadColor
                Else
                    myDescription = NonTurnCancellationLoadColor
                End If
                CmdSql.Connection.Open()
                CmdSql.CommandText = " Insert into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation(nEstelamId,Description,DateShamsi,DateTimeMilladi,Time,Active) Values(" & nEstelamId & ",'" & myDescription & "','" & _DateTime.GetCurrentDateShamsiFull & "','" & _DateTime.GetCurrentDateTimeMilladiFormated & "','" & _DateTime.GetCurrentTime & "'," & IIf(YourCancellationFlag, 1, 0) & ")"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadforTurnCancellationActiving(nEstelamId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation Set Active=1,Description='" & TurnCancellationLoadColor & "' Where nEstelamId=" & nEstelamId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadforTurnCancellationUnActiving(nEstelamId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation Set Active=0,Description='" & NonTurnCancellationLoadColor & "' Where nEstelamId=" & nEstelamId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class



End Namespace

Namespace TurnAttendance

    Public Class R2CoreTransportationAndLoadNotificationInstanceTurnAttendanceManager
        Private _DateTime As New R2DateTime

        Public Function IsPresentRegisteredInLast30Minute(YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As UInt64) As Boolean
            Try
                Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim Ds As DataSet
                If InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateTimeMilladi From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckDriverPresent Where (NobatId=" & YourTurnId & ") and (DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "') Order By DateTimeMilladi Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Dim Last30Minute As Int64 = InstanceConfigurationOfAnnouncements.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 1)
                    If DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), _DateTime.GetCurrentDateTimeMilladi) <= Last30Minute Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnDateTimeDiferenceToToday(YourTurnId As Int64) As Int64
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YourTurnId)
                Dim TurnDateTime As String = _DateTime.GetMilladiDateTimeFromDateShamsiFull(NSSTurn.EnterDate, NSSTurn.EnterTime)
                Return InstancePublicProcedures.GetDateDiff(DateInterval.Day, TurnDateTime, _DateTime.GetCurrentDateTimeMilladiFormated())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalNumberOfPresentRegistered(YourTurnId As Int64) As Int64
            Try
                Dim InstanceSqlDataBox = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YourTurnId)
                Dim DS As DataSet
                Return InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) AS M From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckDriverPresent Where (NobatId=" & YourTurnId & ") And (DateShamsi<>'" & NSSTurn.EnterDate & "') GROUP BY DateShamsi", 1, DS, New Boolean).GetRecordsCount()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalNumberOfNeededPresent(YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As Int64) As UInt16
            Try
                Dim InstanceTruck = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                Dim InstanceDateAndTimePersianCalendar = New R2CoreInstanceDateAndTimePersianCalendarManager
                Dim NSSTruck = InstanceTruck.GetNSSTruck(YourTurnId)
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YourTurnId)
                'درصورتی که کنترل حاضری سالن مورد نظر غیرفعال باشد تعداد حاضری مورد نیاز 0 است
                If Not InstanceConfigurationOfAnnouncements.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 0) Then Return 0
                'درصورتی که در محدوده خاصی زمانی کانفیگ شده عدم کنترل حاضری سالن مورد نظر باشیم تعداد حاضری مورد نیاز 0 است
                Dim PresentControlStartTime As String = InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 3)
                Dim PresentControlEndTime As String = InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 2)
                If Not ((_DateTime.GetCurrentTime() >= PresentControlStartTime) And (_DateTime.GetCurrentTime() <= PresentControlEndTime)) Then Return 0
                'اگر ناوگان بومی باشد یا بومی با پلاک غیربومی باشد تعداد حاضری مورد نیاز از کانفیگ بدست می آید
                'درغیر اینصورت طبق فرمول پیشنهاد شده انجام می شود
                If InstanceTruckNativeness.IsTruckIndigenous(NSSTruck) Then
                    Return InstanceConfigurationOfAnnouncements.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 4)
                Else
                    If YourNSSAnnouncementHall.AHId = Announcements.Announcements.Zobi Then
                        'غير بومي ها در سالن ذوبی به تعداد اختلاف تاريخ صدور مجوز با تاريخ صدور نوبت باید حاضری داشته باشند
                        Return GetTurnDateTimeDiferenceToToday(YourTurnId) - InstanceDateAndTimePersianCalendar.GetHoliDayNumber(NSSTurn.EnterDate, _DateTime.GetCurrentDateShamsiFull)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Anbari Then
                        Return InstanceConfigurationOfAnnouncements.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Otaghdar Then
                        Return InstanceConfigurationOfAnnouncements.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Shahri Then
                        Return InstanceConfigurationOfAnnouncements.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    End If
                End If
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsAmountOfTurnPresentsEnough(ByVal YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As UInt64) As Boolean
            Try
                'آیا آخرین حاضری نوبت در 30 دقیقه یا 3 ساعت اخیر مطابق پیکربندی سیستم ثبت شده است یا نه
                If IsPresentRegisteredInLast30Minute(YourNSSAnnouncementHall, YourTurnId) = False Then Throw New PresentNotRegisteredInLast30MinuteException
                'روزی که نوبت صادر شده نیازی به حاضری نیست
                If GetTurnDateTimeDiferenceToToday(YourTurnId) = 0 Then Return True
                'کنترل تعداد حاضری نوبت
                If GetTotalNumberOfPresentRegistered(YourTurnId) >= GetTotalNumberOfNeededPresent(YourNSSAnnouncementHall, YourTurnId) Then
                    Return True
                Else
                    Throw New PresentsNotEnoughException
                End If
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch exxx As PresentsNotEnoughException
                Throw exxx
            Catch exx As PresentNotRegisteredInLast30MinuteException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public MustInherit Class R2CoreTransportationAndLoadNotificationMClassTurnAttendanceManagement
        Private Shared _DateTime As New R2DateTime

        Public Shared Function GetTotalNumberOfPresentRegistered(YourTurnId As Int64) As Int64
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                Dim DS As DataSet
                Return R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) AS M From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckDriverPresent Where (NobatId=" & YourTurnId & ") And (DateShamsi<>'" & NSSTurn.EnterDate & "') GROUP BY DateShamsi", 1, DS, New Boolean).GetRecordsCount()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsPresentRegisteredInLast30Minute(YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As UInt64) As Boolean
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateTimeMilladi From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckDriverPresent Where (NobatId=" & YourTurnId & ") and (DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "') Order By DateTimeMilladi Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Dim Last30Minute As Int64 = R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 1)
                    If DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), _DateTime.GetCurrentDateTimeMilladi) <= Last30Minute Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetTurnDateTimeDiferenceToToday(YourTurnId As Int64) As Int64
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                Dim TurnDateTime As String = _DateTime.GetMilladiDateTimeFromDateShamsiFull(NSSTurn.EnterDate, NSSTurn.EnterTime)
                Return R2CoreMClassPublicProcedures.GetDateDiff(DateInterval.Day, TurnDateTime, _DateTime.GetCurrentDateTimeMilladiFormated())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsPresentsContinuous(ByVal YourTurnId As UInt64) As Boolean
            Try
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                If GetTotalNumberOfPresentRegistered(YourTurnId) + 1 >= GetTurnDateTimeDiferenceToToday(YourTurnId) - R2CoreDateAndTimePersianCalendarManagement.GetHoliDayNumber(NSSTurn.EnterDate, _DateTime.GetCurrentDateShamsiFull) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsThisDayTruckDriverPresentRegistered(ByVal YourTurnId As UInt64) As Boolean
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckDriverPresent Where (NobatId=" & YourTurnId & ") and (DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "')", 1, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetTotalNumberOfNeededPresent(YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As Int64) As UInt16
            Try
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                Dim NSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YourTurnId)
                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTurn(YourTurnId)
                'درصورتی که کنترل حاضری سالن مورد نظر غیرفعال باشد تعداد حاضری مورد نیاز 0 است
                If Not R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 0) Then Return 0
                'درصورتی که در محدوده خاصی زمانی کانفیگ شده عدم کنترل حاضری سالن مورد نظر باشیم تعداد حاضری مورد نیاز 0 است
                Dim PresentControlStartTime As String = R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 3)
                Dim PresentControlEndTime As String = R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 2)
                If Not ((_DateTime.GetCurrentTime() >= PresentControlStartTime) And (_DateTime.GetCurrentTime() <= PresentControlEndTime)) Then Return 0
                'اگر ناوگان بومی باشد یا بومی با پلاک غیربومی باشد تعداد حاضری مورد نیاز از کانفیگ بدست می آید
                'درغیر اینصورت طبق فرمول پیشنهاد شده انجام می شود
                If InstanceTruckNativeness.IsTruckIndigenous(NSSTruck) Then
                    Return R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 4)
                Else
                    If YourNSSAnnouncementHall.AHId = Announcements.Announcements.Zobi Then
                        'غير بومي ها در سالن ذوبی به تعداد اختلاف تاريخ صدور مجوز با تاريخ صدور نوبت باید حاضری داشته باشند
                        Return GetTurnDateTimeDiferenceToToday(YourTurnId) - R2CoreDateAndTimePersianCalendarManagement.GetHoliDayNumber(NSSTurn.EnterDate, _DateTime.GetCurrentDateShamsiFull)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Anbari Then
                        Return R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Otaghdar Then
                        Return R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    ElseIf YourNSSAnnouncementHall.AHId = Announcements.Announcements.Shahri Then
                        Return R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTruckDriverAttendance, YourNSSAnnouncementHall.AHId, 5)
                    End If
                End If
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsAmountOfTurnPresentsEnough(ByVal YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, ByVal YourTurnId As UInt64) As Boolean
            Try
                'آیا آخرین حاضری نوبت در 30 دقیقه یا 3 ساعت اخیر مطابق پیکربندی سیستم ثبت شده است یا نه
                If IsPresentRegisteredInLast30Minute(YourNSSAnnouncementHall, YourTurnId) = False Then Throw New PresentNotRegisteredInLast30MinuteException
                'روزی که نوبت صادر شده نیازی به حاضری نیست
                If GetTurnDateTimeDiferenceToToday(YourTurnId) = 0 Then Return True
                'کنترل تعداد حاضری نوبت
                If GetTotalNumberOfPresentRegistered(YourTurnId) >= GetTotalNumberOfNeededPresent(YourNSSAnnouncementHall, YourTurnId) Then
                    Return True
                Else
                    Throw New PresentsNotEnoughException
                End If
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As AnnouncementsubGroupRelationTruckNotExistException
                Throw ex
            Catch exxx As PresentsNotEnoughException
                Throw exxx
            Catch exx As PresentNotRegisteredInLast30MinuteException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Namespace Exceptions
        Public Class PresentNotRegisteredInLast30MinuteException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "حاضري امروز راننده در محدوده زماني نزديك به صدور مجوز بار نيست"
                End Get
            End Property
        End Class

        Public Class PresentsNotEnoughException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "حاضري برای نوبت به تعداد کافی وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace ProcessesManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationProcesses
        Inherits R2CoreDesktopProcesses

        Public Shared ReadOnly FrmcLoadPermissions As Int64 = 24
        Public Shared ReadOnly FrmcLoadCapacitor As Int64 = 44
        Public Shared ReadOnly FrmcLoadAllocations As Int64 = 45
        Public Shared ReadOnly FrmcBillOfLadingControl As Int64 = 62
        Public Shared ReadOnly FrmcTruckDriverLoadAllocationsPriorityApplied As Int64 = 63
        Public Shared ReadOnly FrmcLoadCapacitorMonitoring As Int64 = 64
        Public Shared ReadOnly FrmcTransportCompniesManipulation As Int64 = 68
        Public Shared ReadOnly FrmcMoneyWalletChargeByTransportCompany As Int64 = 72
        Public Shared ReadOnly FrmcLoadingAndDischargingPlaces As Int64 = 75

    End Class




End Namespace

Namespace EntityRelations

    Public MustInherit Class R2CoreTransportationAndLoadNotificationEntityRelationTypes
        Inherits R2CoreEntityRelationTypes
        Public Shared ReadOnly Turn_TurnRegisterRequest As Int64 = 1
    End Class

End Namespace

Namespace TerraficCardsManagement

    Public Class R2CoreTransportationAndLoadNotificationInstanceTerraficCardsManager
        Public Function GetNSSTerafficCard(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreParkingSystemStandardTrafficCardStructure
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
            Try
                Dim Ds As New DataSet
                If YourNSSSoftwareUser.UserTypeId = R2CoreParkingSystemSoftwareUserTypes.Driver Then
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 TCardsRCar.CardId
                        from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                          Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                          Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                          Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                          Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                          Inner Join R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars as TCardsRCar On Cars.nIDCar=TCardsRCar.nCarId 
                        Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.RelationActive=1 and  
                              EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and Cars.ViewFlag=1 and TCardsRCar.RelationActive=1 and CarAndPersons.snRelation=2 
                              and ((DATEDIFF(SECOND,TCardsRCar.RelationTimeStamp,getdate())<240) or (TCardsRCar.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                              and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                        Order By CarAndPersons.nIDCarAndPerson Desc,TCardsRCar.RelationId Desc,TCardsRCar.RelationTimeStamp Desc", 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                        Return InstanceTrafficCards.GetNSSTrafficCard(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CardId")))
                    Else
                        Throw New SoftwareUserMoneyWalletNotFoundException
                    End If
                ElseIf YourNSSSoftwareUser.UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany Then
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Top 1 TransportCompaniesRelationMoneyWallets.CardId from R2Primary.dbo.TblSoftwareUsers As SoftwareUsers
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TransportCompaniesRelationSoftwareUsers On SoftwareUsers.UserId=TransportCompaniesRelationSoftwareUsers.UserId 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TransportCompaniesRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TransportCompaniesRelationMoneyWallets On TransportCompanies.TCId=TransportCompaniesRelationMoneyWallets.TransportCompanyId 
                         Where SoftwareUsers.UserId =" & YourNSSSoftwareUser.UserId & " And SoftwareUsers.UserActive = 1 And SoftwareUsers.Deleted = 0 And TransportCompaniesRelationSoftwareUsers.RelationActive = 1 And TransportCompaniesRelationMoneyWallets.RelationActive = 1 
                         Order By TransportCompaniesRelationMoneyWallets.RelationId Desc", 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                        Return InstanceTrafficCards.GetNSSTrafficCard(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CardId")))
                    Else
                        Throw New SoftwareUserMoneyWalletNotFoundException
                    End If
                Else
                    Throw New SoftwareUserMoneyWalletNotFoundException
                End If
            Catch ex As SoftwareUserMoneyWalletNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTerafficCard(YourNSSTransprortCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure) As R2CoreParkingSystemStandardTrafficCardStructure
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 MoneyWallets.CardId 
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TCsRMoneyWallets On TransportCompanies.TCId=TCsRMoneyWallets.TransportCompanyId 
   			  			   Inner Join R2Primary.dbo.TblRFIDCards as MoneyWallets On TCsRMoneyWallets.CardId=MoneyWallets.CardId  
                        Where MoneyWallets.Active=1 and TransportCompanies.Deleted=0 and  TCsRMoneyWallets.RelationActive=1 and TransportCompanies.TCId=" & YourNSSTransprortCompany.TCId & "", 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanceTrafficCards.GetNSSTrafficCard(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CardId")))
                Else
                    Throw New SoftwareUserMoneyWalletNotFoundException
                End If
            Catch ex As SoftwareUserMoneyWalletNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTerraficCardsManagement

    End Class


End Namespace

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


End Namespace

Namespace PermissionManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationPermissionTypes
        Inherits R2CoreParkingSystemPermissionTypes

        Public Shared ReadOnly RequestersAllowAHSGIdLoadAllocationRegistering As Int64 = 3
        Public Shared ReadOnly RequesterAllowLoadAllocationByLoadStatus As Int64 = 4
        Public Shared ReadOnly LoadAllocationUseTimeHandlingByLoadStatus As Int64 = 5
        Public Shared ReadOnly LoadPermissionUseTimeHandlingByLoadStatus As Int64 = 6
        Public Shared ReadOnly RequesterCanSendRequestforTurnIssueBySeqT As Int64 = 10
        Public Shared ReadOnly UserCanRequestReserveTurnRegistering As Int64 = 11
        Public Shared ReadOnly RequesterCanRequestReserveTurnRegistering As Int64 = 12
        Public Shared ReadOnly UserCanResuscitationReserveTurn As Int64 = 13
        Public Shared ReadOnly UserCanRequestEmergencyTurnRegistering As Int64 = 14
        Public Shared ReadOnly UserCanEditLoadCapacitorLoadAfterLoadAnnounce As Int64 = 15
        Public Shared ReadOnly RequesterCanSendRequestforTurnIssueByLastLoadPermissioned As Int64 = 16
        Public Shared ReadOnly SoftwareUserCanExcecuteTurnCancellation As Int64 = 17
        Public Shared ReadOnly SoftwareUserCanSendRealTimeTurnRegisteringRequestWithLicensePlate As Int64 = 18
        Public Shared ReadOnly SoftwareUserCanSendTruckorTruckDriverChangeRequest As Int64 = 19
        Public Shared ReadOnly SoftwareUserCanExcecuteTurnCancellationWithLicensePlate As Int64 = 20
        Public Shared ReadOnly SoftwareUserCanViewAnnouncedLoadsReportOrClearanceLoadsReport As Int64 = 21
        Public Shared ReadOnly SoftwareUserCanDeleteAnyofLoadCapacitorLoad As Int64 = 23
        Public Shared ReadOnly SoftwareUserCanViewListofAllTransportCompanies As Int64 = 24
        Public Shared ReadOnly SoftwareUserCanViewListofAllLoadsofLoadCapacitor As Int64 = 25
        Public Shared ReadOnly SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser As Int64 = 26
        Public Shared ReadOnly SoftwareUserCanEditAnyofLoadCapacitorLoad As Int64 = 27
        Public Shared ReadOnly SoftwareUserCanCancellingLoadsViaLoadStatus As Int64 = 28
        Public Shared ReadOnly RequesterCanAllocateSedimentedLoadInTimeRange As Int64 = 29
        Public Shared ReadOnly UserCanRegisterOrEditLoadsAnyTonaj As Int64 = 31
        Public Shared ReadOnly UserCanViewAllofLoadsfromApplication As Int64 = 32
    End Class

End Namespace

Namespace EntityManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationEntities
        Inherits R2CoreParkingSystemEntities

        Public Shared ReadOnly AnnouncementsubGroups As Int64 = 5
        Public Shared ReadOnly LoadCapacitorLoadStatuses As Int64 = 6
        Public Shared ReadOnly SequentialTurns As Int64 = 7
        Public Shared ReadOnly IndigenousTrucksTypes As Int64 = 9

    End Class



End Namespace

Namespace CalendarManagement
    Namespace SpecializedPersianCalendar
        Public Class R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager

            Private _DateTime As New R2DateTime

            Public Function GetFirstDateShamsiInRangeWithoutHoliday(YourTopBaseDateShamsi As String, YourTotalDay As Int64) As String
                Try
                    If Not _DateTime.ChekDateShamsiFullSyntax(YourTopBaseDateShamsi) Then Throw New ShamsiDateSyntaxNotValidException
                    Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet = Nothing
                    Dim Count = R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                             Where DateShamsi<
                                   (Select Top 1 * from 
                                       (Select Top " & YourTotalDay & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                        Where PCType=0 and DateShamsi<='" & YourTopBaseDateShamsi & "' Order By DateShamsi desc) as DataBox
                                    Order By DateShamsi)
                             Order By DateShamsi Desc", 3600, Ds, New Boolean).GetRecordsCount()
                    If Count <> YourTotalDay Then Throw New FirstDateShamsiInRangeWithoutHolidayException
                    Return Ds.Tables(0).Rows(Count - 1).Item("DateShamsi")
                Catch ex As ShamsiDateSyntaxNotValidException
                    Throw ex
                Catch ex As FirstDateShamsiInRangeWithoutHolidayException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            Public Function IsTodayIsHolidayforLoadAnnounce() As Boolean
                Try
                    Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet = Nothing
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 LoadAnnounce from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "' 
                               Order By HId Desc", 3600, Ds, New Boolean)
                    Return Ds.Tables(0).Rows(0).Item("LoadAnnounce")
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            Public Function IsTomorrowIsHolidayforLoadAnnounce() As Boolean
                Try
                    Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet = Nothing
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 LoadAnnounce from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTime.GetNextDateShamsi & "' 
                               Order By HId Desc", 3600, Ds, New Boolean)
                    Return Ds.Tables(0).Rows(0).Item("LoadAnnounce")
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            Public Function IsTodayIsHoliday() As Boolean
                Try
                    Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet = Nothing
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 PCTYpe from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTime.GetCurrentDateShamsiFull & "' 
                               Order By HId Desc", 3600, Ds, New Boolean)
                    Return Convert.ToBoolean(Ds.Tables(0).Rows(0).Item("PCTYpe"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function


        End Class


    End Namespace

End Namespace

Namespace SMS

    Namespace SMSTypes
        Public MustInherit Class R2CoreTransportationAndLoadNotificationSMSTypes
            Inherits R2CoreParkingSystemSMSTypes

            Public Shared ReadOnly Property LoadAllocationsLoadPermissionRegisteringFailed = 4
            Public Shared ReadOnly Property SendingTurnNumberSMS = 9
            Public Shared ReadOnly Property SendingLoadPermissionIssuedInfSMS = 10
            Public Shared ReadOnly Property LoadingAndDischargingPLacesChangeStatus = 18
            Public Shared ReadOnly Property TransportCompanyChangeActiveStatus = 19
            Public Shared ReadOnly Property ChangeLoadInformation = 20
            Public Shared ReadOnly Property FactoryAndProductionCenterChangeActiveStatus = 22
        End Class

    End Namespace

    Namespace RecivedSMSCodes
        Public MustInherit Class RecivedSMSCodes
            Public Shared ReadOnly Property ChangeLoadSource = 4
            Public Shared ReadOnly Property ChangeLoadTarget = 5
        End Class
    End Namespace

    Namespace SMSHandling

        Public Class R2CoreTransportationAndLoadNotificationChangeLoadSource
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Dim LoadCapacitorLoadManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManipulationManager
                    LoadCapacitorLoadManipulation.ChangeLoadSource(MobileNumber, SMSContent)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public Class R2CoreTransportationAndLoadNotificationChangeLoadTarget
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Dim LoadCapacitorLoadManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManipulationManager
                    LoadCapacitorLoadManipulation.ChangeLoadTarget(MobileNumber, SMSContent)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

End Namespace

Namespace TrucksNativeness

    Public Class TruckNativenessTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Native As Int64 = 1
        Public Shared ReadOnly Property UnNative As Int64 = 2
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure
        Inherits R2StandardStructure
        Public Sub New()
            MyBase.New()
            _NId = Int64.MinValue
            _NName = String.Empty
            _NTitle = String.Empty
            _NColor = Color.Red
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _Active = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.TrueString
        End Sub

        Public Sub New(YourNId As Int64, YourNName As String, YourNTitle As String, YourNColor As Color, YourDateTimeMilladi As DateTime, YOurDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourNId, YourNName)
            _NId = YourNId
            _NName = YourNName
            _NTitle = YourNTitle
            _NColor = YourNColor
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YOurDateShamsi
            _Time = YourTime
            _Active = YourActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property NId As Int64
        Public Property NName As String
        Public Property NTitle As String
        Public Property NColor As Drawing.Color
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean



    End Class

    Public Structure R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
        Dim TruckNativenessTypeId As Int64
        Dim TruckNativenessExpireDate As R2StandardDateAndTimeStructure
    End Structure

    'BPTChanged
    Public Structure R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
        Dim TruckNativenessTypeId As Int64
        Dim TruckNativenessTypeTitle As String
        Dim TruckNativenessExpireDate As String
    End Structure

    Public Class R2CoreTransportationAndLoadNotificationsTruckNativenessManager
        Private _DateTime As New R2DateTime

        Public Function GetNSSTruckNativeness(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
            Try
                If YourNSSTruck Is Nothing Then Throw New TruckNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                    Da.SelectCommand = New SqlCommand("Select CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar Where nIDCar=" & YourNSSTruck.NSSCar.nIdCar & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TruckNotFoundException
                    NSS.TruckNativenessTypeId = Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CarNativenessTypeId"))
                    NSS.TruckNativenessExpireDate = New R2StandardDateAndTimeStructure(Nothing, Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, Nothing)
                    Return NSS
                Else
                    'If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar Where nIDCar=" & YourNSSTruck.NSSCar.nIdCar & "", 3600, Ds).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                    Dim DSCarsNativeness As New DataSet
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Case nIDCar,CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar", 3600, DSCarsNativeness, New Boolean)
                    Dim DR() = DSCarsNativeness.Tables(0).Select("nIDCar=" + YourNSSTruck.NSSCar.nIdCar)
                    NSS.TruckNativenessTypeId = Convert.ToInt64(DR(0).Item("CarNativenessTypeId"))
                    NSS.TruckNativenessExpireDate = New R2StandardDateAndTimeStructure(Nothing, DR(0).Item("CarNativenessExpireDate").trim, Nothing)
                    Return NSS
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckNativenessType(YourTruckNativenessTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes Where NId=" & YourTruckNativenessTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNativenessTypeNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure(Ds.Tables(0).Rows(0).Item("NId"), Ds.Tables(0).Rows(0).Item("NName").TRIM, Ds.Tables(0).Rows(0).Item("NTitle").TRIM, Color.FromName(Ds.Tables(0).Rows(0).Item("NColor").TRIM), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                Return NSS
            Catch ex As TruckNativenessTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetTruckNativenessType(YourTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Int64
            Try
                If IsTruckIndigenous(YourTruck) Then
                    Return TruckNativenessTypes.Native
                Else
                    Return TruckNativenessTypes.UnNative
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Function IsTruckIndigenous(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Boolean
            Try
                If YourNSSTruck Is Nothing Then Throw New TruckNotFoundException
                Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet = Nothing
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select CarNativenessTypeId,CarNativenessExpireDate from DBtransport.dbo.TbCar
                   Where StrCarNo='" & YourNSSTruck.NSSCar.StrCarNo & "' and StrCarSerialNo='" & YourNSSTruck.NSSCar.StrCarSerialNo & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TruckNotFoundException
                End If
                If Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CarNativenessTypeId")) = TruckNativenessTypes.Native Then
                    If Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").ToString.Trim = String.Empty Then
                        Return True
                    ElseIf Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").ToString.Trim > _DateTime.GetCurrentDateShamsiFull() Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Function IsTruckIndigenous(YourTurn As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure) As Boolean
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Return IsTruckIndigenous(InstanceTrucks.GetNSSTruck(YourTurn, False))
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function GetTruckNativeness(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim NSS As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                             "Select Cars.CarNativenessTypeId,Cars.CarNativenessExpireDate ,TruckNativenessTypes.NTItle  from DBTransport.dbo.TbCar as Cars
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
                              Where Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and TruckNativenessTypes.Deleted=0")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckNotFoundException
                    NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure With {.TruckNativenessTypeId = DS.Tables(0).Rows(0).Item("CarNativenessTypeId"), .TruckNativenessExpireDate = DS.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, .TruckNativenessTypeTitle = DS.Tables(0).Rows(0).Item("NTItle").trim}
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                             "Select Cars.CarNativenessTypeId,Cars.CarNativenessExpireDate ,TruckNativenessTypes.NTItle  from DBTransport.dbo.TbCar as Cars
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
                              Where Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and TruckNativenessTypes.Deleted=0", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure With {.TruckNativenessTypeId = DS.Tables(0).Rows(0).Item("CarNativenessTypeId"), .TruckNativenessExpireDate = DS.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, .TruckNativenessTypeTitle = DS.Tables(0).Rows(0).Item("NTItle").trim}
                    Else
                        Throw New TruckNotFoundException
                    End If
                End If
                Return NSS
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ChangeTruckNativeness(YourTruckId As Int64, YourTruckNativenessExpireDate As String) As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim NSSTruck = InstanceTrucks.GetNSSTruck(YourTruckId)
                'کنترل تغییر وضعیت بومی گری ناوگان بومی با پلاک بومی - که البته امکان پذیر نیست
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim IndigenousTrucks() = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.IndigenousTrucks, 1).Split("-")
                If IndigenousTrucks.Contains(NSSTruck.NSSCar.StrCarSerialNo) Then Throw New IndigenousTruckChangeNativnessFailedException
                'تغییر وضعیت بومی گری
                Dim newTruckNativenessTypeId = TrucksNativeness.TruckNativenessTypes.None
                Dim TruckNativeness = GetTruckNativeness(YourTruckId, True)
                If TruckNativeness.TruckNativenessTypeId = TruckNativenessTypes.Native Then
                    newTruckNativenessTypeId = TruckNativenessTypes.UnNative
                ElseIf TruckNativeness.TruckNativenessTypeId = TruckNativenessTypes.UnNative Then
                    newTruckNativenessTypeId = TruckNativenessTypes.Native
                Else
                    Throw New TruckNativenessTypeNotValidException
                End If
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update dbtransport.dbo.TbCar Set CarNativenessTypeId=" & newTruckNativenessTypeId & ",CarNativenessExpireDate='" & YourTruckNativenessExpireDate & "' Where nIdCar=" & NSSTruck.NSSCar.nIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                TruckNativeness.TruckNativenessExpireDate = YourTruckNativenessExpireDate
                TruckNativeness.TruckNativenessTypeId = newTruckNativenessTypeId
                TruckNativeness.TruckNativenessTypeTitle = InstanceTruckNativeness.GetNSSTruckNativenessType(newTruckNativenessTypeId).NTitle
                Return TruckNativeness
            Catch ex As IndigenousTruckChangeNativnessFailedException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As TruckNativenessTypeNotValidException
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

    End Class

    Namespace Exceptions

        Public Class IndigenousTruckChangeNativnessFailedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تغییر وضعیت ناوگان امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class NonIndigenousTrucksException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    Return InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.UnIndigenousTrucks).MsgContent
                End Get
            End Property
        End Class

        Public Class TruckNativenessTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات بومی گری با کد شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class TruckNativenessTypeNotValidException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "خدرو دارای اطلاعات بومی گری غیر مجاز و تعریف نشده است"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace PredefinedMessagesManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationPredefinedMessages
        Public Shared ReadOnly LoadAllocationIdNotPairWithDriver As Int64 = 4
        Public Shared ReadOnly UnIndigenousTrucks As Int64 = 15
        Public Shared ReadOnly TruckNotFoundException As Int64 = 27
        Public Shared ReadOnly FactoryAndProductionCenterNotFoundException As Int64 = 29

    End Class


End Namespace

Namespace Goods
    Public Class R2CoreTransportationAndLoadNotificationStandardGoodStructure
        Inherits R2StandardStructure
        Public Sub New()
            MyBase.New()
            _GoodId = Int64.MinValue
            _GoodName = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourGoodId As Int64, ByVal YourGoodName As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourGoodId, YourGoodName)
            _GoodId = YourGoodId
            _GoodName = YourGoodName
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property GoodId As Int64
        Public Property GoodName() As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassGoodsManagement
        Public Shared Function GetNSSGood(YourGoodId As Int64) As R2CoreTransportationAndLoadNotificationStandardGoodStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from dbtransport.dbo.TbProducts Where StrGoodCode=" & YourGoodId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New GoodNotFoundException
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardGoodStructure = New R2CoreTransportationAndLoadNotificationStandardGoodStructure(Ds.Tables(0).Rows(0).Item("StrGoodCode"), Ds.Tables(0).Rows(0).Item("StrGoodName").TRIM, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                Return NSS
            Catch exx As GoodNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetGoods_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardGoodStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardGoodStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select StrGoodCode,StrGoodName,ViewFlag,Active,Deleted From DBTransport.dbo.TbProducts Where StrGoodName Like '%" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "%' and ViewFlag=1 Order By StrGoodCode", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardGoodStructure(DS.Tables(0).Rows(Loopx).Item("StrGoodCode"), DS.Tables(0).Rows(Loopx).Item("StrGoodName"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetGoods_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardGoodStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardGoodStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select StrGoodCode,StrGoodName,ViewFlag,Active,Deleted  From DBTransport.dbo.TbProducts Where Left(StrGoodName," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' Order By StrGoodCode", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardGoodStructure(DS.Tables(0).Rows(Loopx).Item("StrGoodCode"), DS.Tables(0).Rows(Loopx).Item("StrGoodName"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Namespace Exceptions
        Public Class GoodNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کالا با کد شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

'BPTChanged
Namespace Products

    Public Class R2CoreTransportationAndLoadNotificationProductsManager
        Public Function GetProducts_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                           "Select ProductTypes.ProductTypeId as ProductTypeId,ProductTypes.ProductTypeTitle as ProductTypeTitle,ProductTypes.Active as ProductTypeActive,
                                   Products.strGoodCode as ProductId,Products.strGoodName as ProductTitle,Products.Active as ProductActive
                            from DBTransport.dbo.tbProductTypes as ProductTypes
                                Inner Join DBTransport.dbo.tbProducts as Products On ProductTypes.ProductTypeId=Products.ProductTypeId 
                            Where ProductTypes.Deleted=0 and Products.Deleted=0 and Products.strGoodName Like N'%" & YourSearchString & "%'
                            Order By ProductTypes.ProductTypeId 
                            for json auto")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           "Select ProductTypes.ProductTypeId as ProductTypeId,ProductTypes.ProductTypeTitle as ProductTypeTitle,ProductTypes.Active as ProductTypeActive,
                                   Products.strGoodCode as ProductId,Products.strGoodName as ProductTitle,Products.Active as ProductActive
                            from DBTransport.dbo.tbProductTypes as ProductTypes
                                Inner Join DBTransport.dbo.tbProducts as Products On ProductTypes.ProductTypeId=Products.ProductTypeId 
                            Where ProductTypes.Deleted=0 and Products.Deleted=0 and Products.strGoodName Like N'%" & YourSearchString & "%'
                            Order By ProductTypes.ProductTypeId 
                            for json auto", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ChangeActiveStatusOfProductType(YourProductTypeId As Int64, YourProductTypeActive As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update DBTransport.dbo.tbProductTypes Set Active=" & IIf(YourProductTypeActive, 1, 0) & " 
                                      Where ProductTypeId=" & YourProductTypeId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ChangeActiveStatusOfProduct(YourProductId As Int64, YourProductActive As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update DBTransport.dbo.tbProducts Set Active=" & IIf(YourProductActive, 1, 0) & " 
                                      Where strGoodCode=" & YourProductId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class



End Namespace

'BPTChanged
Namespace TravelTime

    Public Class R2CoreTransportationAndLoadNotificationTravelTime
        Public Property LoaderTypeId As Int64
        Public Property SourceCityId As Int64
        Public Property SourceCityName As String
        Public Property TargetCityId As Int64
        Public Property TargetCityName As String
        Public Property TravelTime As Int16
        Public Property Active As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationTravelTimeManager
        Public Function GetTravelTimes(YourLoaderTypeId As Int64, YourSourceCityId As Int64, YourTargetCityId As Int64, YourImmediately As Boolean) As String
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle as LoaderTypeTitle,TravelTimes.SourceCityId,SourceCities.StrCityName SourceCityName,TravelTimes.TargetCityId,TargetCities.StrCityName as TargetCityName,TravelTimes.TravelTime,TravelTimes.Active
                     From R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes as TravelTimes
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On TravelTimes.LoaderTypeId=LoaderTypes.LoaderTypeId
                       Inner Join DBTransport.dbo.tbCity As SourceCities On TravelTimes.SourceCityId=SourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity As TargetCities On TravelTimes.TargetCityId=TargetCities.nCityCode 
                     Where (" & YourSourceCityId & "=-1 And " & YourTargetCityId & "<>-1 And TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") Or 
                           (" & YourSourceCityId & "<>-1 And " & YourTargetCityId & "=-1 And TravelTimes.SourceCityId=" & YourSourceCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") Or  
	                       (" & YourSourceCityId & "<>-1 And " & YourTargetCityId & "<>-1 And TravelTimes.SourceCityId=" & YourSourceCityId & " And TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") or
	                       (" & YourSourceCityId & "=-1 And " & YourTargetCityId & "=-1 And 2=3) and TravelTimes.Deleted=0
                     for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle as LoaderTypeTitle,TravelTimes.SourceCityId,SourceCities.StrCityName SourceCityName,TravelTimes.TargetCityId,TargetCities.StrCityName as TargetCityName,TravelTimes.TravelTime,TravelTimes.Active
                     From R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes as TravelTimes
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On TravelTimes.LoaderTypeId=LoaderTypes.LoaderTypeId
                       Inner Join DBTransport.dbo.tbCity As SourceCities On TravelTimes.SourceCityId=SourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity As TargetCities On TravelTimes.TargetCityId=TargetCities.nCityCode 
                     Where (" & YourSourceCityId & "=-1 And " & YourTargetCityId & "<>-1 And TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") Or 
                           (" & YourSourceCityId & "<>-1 And " & YourTargetCityId & "=-1 And TravelTimes.SourceCityId=" & YourSourceCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") Or  
	                       (" & YourSourceCityId & "<>-1 And " & YourTargetCityId & "<>-1 And TravelTimes.SourceCityId=" & YourSourceCityId & " And TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.LoaderTypeId=" & YourLoaderTypeId & ") or
	                       (" & YourSourceCityId & "=-1 And " & YourTargetCityId & "=-1 And 2=3) and TravelTimes.Deleted=0
                     for json path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTravelTime(YourLoaderTypeId As Int64, YourSourceCityId As Int64, YourTargetCityId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTravelTime
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select Top 1 LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle as LoaderTypeTitle,TravelTimes.SourceCityId,SourceCities.StrCityName SourceCityName,TravelTimes.TargetCityId,TargetCities.StrCityName as TargetCityName,TravelTimes.TravelTime,TravelTimes.Active
                     From R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes as TravelTimes
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On TravelTimes.LoaderTypeId=LoaderTypes.LoaderTypeId
                       Inner Join DBTransport.dbo.tbCity As SourceCities On TravelTimes.SourceCityId=SourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity As TargetCities On TravelTimes.TargetCityId=TargetCities.nCityCode 
                     Where TravelTimes.LoaderTypeId=" & YourLoaderTypeId & " and TravelTimes.SourceCityId=" & YourSourceCityId & " and TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.Deleted=0")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                     Select  Top 1 LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle as LoaderTypeTitle,TravelTimes.SourceCityId,SourceCities.StrCityName SourceCityName,TravelTimes.TargetCityId,TargetCities.StrCityName as TargetCityName,TravelTimes.TravelTime,TravelTimes.Active
                     From R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes as TravelTimes
                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On TravelTimes.LoaderTypeId=LoaderTypes.LoaderTypeId
                       Inner Join DBTransport.dbo.tbCity As SourceCities On TravelTimes.SourceCityId=SourceCities.nCityCode 
                       Inner Join DBTransport.dbo.tbCity As TargetCities On TravelTimes.TargetCityId=TargetCities.nCityCode 
                     Where TravelTimes.LoaderTypeId=" & YourLoaderTypeId & " and TravelTimes.SourceCityId=" & YourSourceCityId & " and TravelTimes.TargetCityId=" & YourTargetCityId & " and TravelTimes.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTravelTime With {.LoaderTypeId = Ds.Tables(0).Rows(0).Item("LoaderTypeId"), .SourceCityId = Ds.Tables(0).Rows(0).Item("SourceCityId"), .SourceCityName = Ds.Tables(0).Rows(0).Item("SourceCityName").trim, .TargetCityId = Ds.Tables(0).Rows(0).Item("TargetCityId"), .TargetCityName = Ds.Tables(0).Rows(0).Item("TargetCityName").trim, .TravelTime = Ds.Tables(0).Rows(0).Item("TravelTime"), .Active = Ds.Tables(0).Rows(0).Item("Active")}
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TravelTimeRegistering(YourTravelTime As R2CoreTransportationAndLoadNotificationTravelTime)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes(LoaderTypeId,SourceCityId,TargetCityId,TravelTime,ViewFlag,Active,Deleted)
                                          Values(" & YourTravelTime.LoaderTypeId & "," & YourTravelTime.SourceCityId & "," & YourTravelTime.TargetCityId & "," & YourTravelTime.TravelTime & ",1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TravelTimeDeleteting(YourTravelTime As R2CoreTransportationAndLoadNotificationTravelTime)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes 
                                      Where LoaderTypeId=" & YourTravelTime.LoaderTypeId & " and SourceCityId=" & YourTravelTime.SourceCityId & " and TargetCityId=" & YourTravelTime.TargetCityId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TravelTimeEditing(YourTravelTime As R2CoreTransportationAndLoadNotificationTravelTime)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes Set TravelTime =" & YourTravelTime.TravelTime & " 
                                      Where LoaderTypeId=" & YourTravelTime.LoaderTypeId & " and SourceCityId=" & YourTravelTime.SourceCityId & " and TargetCityId=" & YourTravelTime.TargetCityId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TravelTimeChangeActivateStatus(YourTravelTime As R2CoreTransportationAndLoadNotificationTravelTime)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim TravelTime = GetTravelTime(YourTravelTime.LoaderTypeId, YourTravelTime.SourceCityId, YourTravelTime.TargetCityId, True)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTravelTimes 
                                      Set Active=" & IIf(TravelTime.Active, 0, 1) & " Where LoaderTypeId=" & YourTravelTime.LoaderTypeId & " and SourceCityId=" & YourTravelTime.SourceCityId & " and TargetCityId=" & YourTravelTime.TargetCityId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

End Namespace

Namespace TransportTariffs

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportTariff
        Public Property SourceCityId As Int64
        Public Property SourceCityTitle As String
        Public Property TargetCityId As Int64
        Public Property TargetCityTitle As String
        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property GoodId As Int64
        Public Property GoodTitle As String
        Public Property Tariff As Int64
        Public Property BaseTonnag As Double
        Public Property CalculationReference As String
        Public Property Active As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTarrifStructure

        Public Sub New()
            MyBase.New()
            _AHId = 0
            _AHSGId = 0
            _TargetCityId = Int64.MinValue
            _Tarrif = 0
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _OActive = False
        End Sub

        Public Sub New(ByVal YourAHId As Int64, YourAHSGId As Int64, YourTargetCityId As Int64, YourTarrif As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourOActive As Boolean)
            _AHId = YourAHId
            _AHSGId = YourAHSGId
            _TargetCityId = YourTargetCityId
            _Tarrif = YourTarrif
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _OActive = YourOActive
        End Sub


        Public Property AHId As Int64
        Public Property AHSGId As Int64
        Public Property TargetCityId As Int64
        Public Property Tarrif As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property OActive As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager
        Private _DateTimeService As New R2DateTimeService

        Public Sub TransportTarrifRegistering(YourTargetCityId As Int64, YourSourceCityId As Int64, YourAHId As Int64, YourAHSGId As Int64, YourTarrif As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifs Set OActive=0
                                      where SourceCityId=" & YourSourceCityId & " and TargetCityId=" & YourTargetCityId & " and AHId=" & YourAHId & " and AHSGId=" & YourAHSGId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifs(AHId,AHSGId,SourceCityId,TargetCityId,Tarrif,DateTimeMilladi,DateShamsi,Time,OActive)
                                      values(" & YourAHId & "," & YourAHSGId & "," & YourSourceCityId & "," & YourTargetCityId & "," & YourTarrif & ",'" & _DateTimeService.DateTimeServ.GetCurrentDateTimeMilladiFormated & "','" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "','" & _DateTimeService.DateTimeServ.GetCurrentTime & "',1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTransportTarrif(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardTransportTarrifStructure
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifs Where (AHId=" & YourNSS.AHId & ") and (AHSGId=" & YourNSS.AHSGId & ") and (TargetCityId=" & YourNSS.nCityCode & ") and (SourceCityId=" & YourNSS.nBarSource & ") and OActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportPriceTarrifNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTarrifStructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TargetCityId"), DS.Tables(0).Rows(0).Item("Tarrif"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("OActive"))
            Catch exx As TransportPriceTarrifNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUltimateTransportTarrif(YourAHSGId As Int64, YournTonaj As Double, YourTarrif As Int64) As Double
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Parameters.Add("@AHSGId", SqlDbType.BigInt) : CmdSql.Parameters("@AHSGId").Value = YourAHSGId
                CmdSql.Parameters.Add("@Tarrif", SqlDbType.BigInt) : CmdSql.Parameters("@Tarrif").Value = YourTarrif
                CmdSql.Parameters.Add("@Tonaj", SqlDbType.Float) : CmdSql.Parameters("@Tonaj").Value = YournTonaj
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.GetUltimateTransportTarrif"
                Dim Tarrif = CmdSql.ExecuteScalar
                CmdSql.Connection.Close()
                Return Tarrif
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function GetTariffs(YourLoaderTypeId As Int64, YourGoodId As Int64, YourSourceCityId As Int64, YourTargetCityId As Int64, YourImmediately As Boolean) As String
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,
                            Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Tariffs.Tariff as Tariff,Tariffs.BaseTonnag,Tariffs. CalculationReference,Tariffs.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs as Tariffs
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On Tariffs.LoaderTypeId=LoaderTypes.LoaderTypeId 
                        Inner Join DBTransport.dbo.tbProducts as Goods On Tariffs.GoodId=Goods.strGoodCode 
                        Inner Join DBTransport.dbo.tbCity as SourceCities On Tariffs.SourceCityId=SourceCities.nCityCode 
                        Inner join DBTransport.dbo.tbCity as TargetCities On Tariffs.TargetCityId=TargetCities.nCityCode 
                     Where ((" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and 3=3) or 
                           (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & "))  and
	                       Tariffs.Active=1 and Tariffs.Deleted=0
                     for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                     Select SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,
                            Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Tariffs.Tariff as Tariff,Tariffs.BaseTonnag,Tariffs. CalculationReference,Tariffs.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs as Tariffs
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On Tariffs.LoaderTypeId=LoaderTypes.LoaderTypeId 
                        Inner Join DBTransport.dbo.tbProducts as Goods On Tariffs.GoodId=Goods.strGoodCode 
                        Inner Join DBTransport.dbo.tbCity as SourceCities On Tariffs.SourceCityId=SourceCities.nCityCode 
                        Inner join DBTransport.dbo.tbCity as TargetCities On Tariffs.TargetCityId=TargetCities.nCityCode 
                     Where ((" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and 3=3) or 
                           (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & "))  and
	                       Tariffs.Active=1 and Tariffs.Deleted=0
                     for json path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TariffsRegistering(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs(SourceCityId,TargetCityId,LoaderTypeId,GoodId,Tariff,BaseTonnag,CalculationReference,DateShamsi,ViewFlag,Active,Deleted)
                                          Values(" & YourTariffs(Loopx).SourceCityId & "," & YourTariffs(Loopx).TargetCityId & "," & YourTariffs(Loopx).LoaderTypeId & "," & YourTariffs(Loopx).GoodId & "," & YourTariffs(Loopx).Tariff & "," & YourTariffs(Loopx).BaseTonnag & ",'" & YourTariffs(Loopx).CalculationReference & "','" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
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

        Public Sub TariffsRegistering(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff), YourAddPercentage As Double)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'غیرفعال کردن تعرفه های فعال
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
                'فعال کردن تعرفه های جدید
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs(SourceCityId,TargetCityId,LoaderTypeId,GoodId,Tariff,BaseTonnag,CalculationReference,DateShamsi,ViewFlag,Active,Deleted)
                                          Values(" & YourTariffs(Loopx).SourceCityId & "," & YourTariffs(Loopx).TargetCityId & "," & YourTariffs(Loopx).LoaderTypeId & "," & YourTariffs(Loopx).GoodId & "," & YourTariffs(Loopx).Tariff + (YourAddPercentage * YourTariffs(Loopx).Tariff / 100) & "," & YourTariffs(Loopx).BaseTonnag & ",'" & YourTariffs(Loopx).CalculationReference & "','" & _DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
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

        Public Sub TariffsDeactivate(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'غیرفعال کردن تعرفه های فعال
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
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

        Public Sub TariffsDeleting(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'حذف کردن تعرفه ها
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Deleted=1,Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
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

        Public Sub TariffsEditing(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'ویرایش کردن تعرفه ها
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Tariff=" & YourTariffs(Loopx).Tariff & ",BaseTonnag=" & YourTariffs(Loopx).BaseTonnag & "
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
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

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTransportTarrifsManagement
        Public Shared Function GetNSSTransportTarrif(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardTransportTarrifStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifs Where (AHId=" & YourNSS.AHId & ") and (AHSGId=" & YourNSS.AHSGId & ") and (TargetCityId=" & YourNSS.nCityCode & ") and OActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportPriceTarrifNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTarrifStructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TargetCityId"), DS.Tables(0).Rows(0).Item("Tarrif"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("OActive"))
            Catch exx As TransportPriceTarrifNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Namespace Exceptions
        Public Class TransportPriceTarrifNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نرخ حمل یا تعرفه برای مسیر مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace TransportTarrifsParameters

    Public MustInherit Class R2CoreTransportationAndLoadNotificationTransportTarrifsParameters
        Inherits R2CoreConfigurations
        Public Shared Shadows ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Two_Baskool As Int64 = 1
        Public Shared ReadOnly Property Three_Baskool As Int64 = 2
        Public Shared ReadOnly Property Four_Baskool As Int64 = 3
        Public Shared ReadOnly Property LastNight As Int64 = 4
        Public Shared ReadOnly Property Two_LoadingLocation As Int64 = 5
        Public Shared ReadOnly Property Three_LoadingLocation As Int64 = 6
        Public Shared ReadOnly Property Four_LoadingLocation As Int64 = 7
        Public Shared ReadOnly Property Project As Int64 = 9
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersStructure

        Public Sub New()
            MyBase.New()
            _TPTPId = Int64.MinValue
            _TPTPName = String.Empty
            _TPTPTitle = String.Empty
            _TPTPColor = String.Empty
            _Core = String.Empty
            _UserId = Int64.MinValue
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourTPTPId As Int64, YourTPTPName As String, YourTPTPTitle As String, YourTPTPColor As String, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _TPTPId = YourTPTPId
            _TPTPName = YourTPTPName
            _TPTPTitle = YourTPTPTitle
            _TPTPColor = YourTPTPColor
            _Core = YourCore
            _UserId = YourUserId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property TPTPId As Int64
        Public Property TPTPName As String
        Public Property TPTPTitle As String
        Public Property TPTPColor As String
        Public Property Core As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure

        Public Sub New()
            MyBase.New()
            _TPTPDId = Int64.MinValue
            _AHSGId = Int64.MinValue
            _TPTPId = Int64.MinValue
            _Mblgh = Int64.MaxValue
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _RelationActive = Boolean.FalseString
            _Checked = Boolean.FalseString
            _TPTPTitle = String.Empty
        End Sub

        Public Sub New(YourTPTPDId As Int64, YourAHSGId As Int64, YourTPTPId As Int64, YourMblgh As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourRelationActive As Boolean, YourChecked As Boolean, YourTPTPTitle As String)
            _TPTPDId = YourTPTPDId
            _AHSGId = YourAHSGId
            _TPTPId = YourTPTPId
            _Mblgh = YourMblgh
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _RelationActive = YourRelationActive
            _Checked = YourChecked
            _TPTPTitle = YourTPTPTitle
        End Sub

        Public Property TPTPDId As Int64
        Public Property AHSGId As Int64
        Public Property TPTPId As Int64
        Public Property Mblgh As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property RelationActive As Boolean
        Public Property Checked As Boolean
        Public Property TPTPTitle As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTransportTarrifsParametersManager

        Public Function GetNSSTransportTarrifsParameterDetail(YourTTPDId As Int64) As R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 TransportPriceTarrifsParameters.TPTPTitle,Details.* from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where Details.TPTPDId=" & YourTTPDId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure(DS.Tables(0).Rows(0).Item("TPTPDId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TPTPId"), DS.Tables(0).Rows(0).Item("Mblgh"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("RelationActive"), False, DS.Tables(0).Rows(0).Item("TPTPTitle").trim)
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParams(YourTPTParams As String) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
                If Params(0).Trim = String.Empty Then Return Lst
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTParamDetailId)
                    NSS.Checked = Checked
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParams(YourNSSAHSG As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select TransportPriceTarrifsParameters.TPTPTitle,Details.*,0 as Checked from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AHSGId=" & YourNSSAHSG.AHSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId ", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows().Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure(DS.Tables(0).Rows(Loopx).Item("TPTPDId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTPId"), DS.Tables(0).Rows(Loopx).Item("Mblgh"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("RelationActive"), DS.Tables(0).Rows(Loopx).Item("Checked"), DS.Tables(0).Rows(Loopx).Item("TPTPTitle")))
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HaveAnyTransportTarrifsParams(YourNSSAHSG As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As Boolean
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select TransportPriceTarrifsParameters.TPTPId from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AHSGId=" & YourNSSAHSG.AHSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId ", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalofTransportTarrifsParameters(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim Total As Int64 = 0
                Dim Params = YourNSS.TPTParams.Split(";")
                If Params(0) = String.Empty Then Return 0
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTPDId As Int64 = Params(Loopx).Split(":")(0)
                    Dim Checked As Boolean = Params(Loopx).Split(":")(1)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTPDId)
                    If Checked Then
                        Total = Total + NSS.Mblgh
                    End If
                Next
                Return Total
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportTarrifsComposit(YourTPTParams As String) As String
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                If Params(0).Trim = String.Empty Then Return String.Empty
                Dim SB = New StringBuilder
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTParamDetailId)
                    If Checked Then SB.AppendLine(NSS.TPTPTitle + " : " + IIf(NSS.Mblgh = 0, "توافقی", NSS.Mblgh.ToString))
                Next
                Return SB.ToString
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Namespace Exceptions
        Public Class TransportPriceTarrifParameterDetailNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر موثر در تعرفه حمل با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class TransportPriceTarrifParameterDetailsforAHSGNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر موثر در تعرفه حمل مرتبط  با زیرگروه اعلام بار با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class TransportPriceTarrifParameterDetailsNotAdjustedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامترهای موثر در تعرفه حمل به درستی انتخاب و تنظیم نشده اند"
                End Get
            End Property
        End Class

    End Namespace

End Namespace











