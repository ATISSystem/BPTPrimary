
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
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters.Exceptions
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
        Inherits R2Core.RawGroups.R2CoreRawGroups

        Public Shared ReadOnly TurnRegisterRequestAttachements As Int64 = 5

        Public Shared ReadOnly DriverSelfDeclarations As Int64 = 8

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

Namespace PermissionManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationPermissionTypes
        Inherits R2CoreParkingSystemPermissionTypes

        Public Shared ReadOnly XXX As Int64 = 3
        Public Shared ReadOnly SoftwareUserCanSedimentingLoad As Int64 = 4
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
        Public Shared ReadOnly SoftwareUserCanFreeLiningLoadsViaLoadStatus As Int64 = 29
        Public Shared ReadOnly UserCanRegisterOrEditLoadsAnyTonaj As Int64 = 31
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

Namespace PredefinedMessagesManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationPredefinedMessages
        Public Shared ReadOnly LoadAllocationIdNotPairWithDriver As Int64 = 4
        Public Shared ReadOnly UnIndigenousTrucks As Int64 = 15
        Public Shared ReadOnly TruckNotFoundException As Int64 = 27
        Public Shared ReadOnly FactoryAndProductionCenterNotFoundException As Int64 = 29

    End Class


End Namespace

Namespace TWS
    Public Class R2CoreTransportationAndLoadNotificationsTWSManager

        Private _dateTime As New R2DateTime

        Public Sub TWSCapacitorSendLoadPermissions()
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceLoadTargets = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
                Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblTWSCapacitor 
                   Where TWSStatusId=" & TWSClassLibrary.NobatsManagement.NobatsStatus.Sodoor & " and ShamsiDate='" & _dateTime.GetCurrentDateShamsiFull & "'", 0, DS, New Boolean).GetRecordsCount = 0 Then Exit Try

                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim TruckId As Int64 = DS.Tables(0).Rows(Loopx).Item("TruckId")
                    Dim TruckNSS = InstanceTrucks.GetNSSTruck(TruckId)
                    Dim LastPermissioned = InstanceLoadPermission.GetTruckLastLoadWhichPermissioned(TruckNSS)
                    'ارسال تاییدیه صدور مجوز به آنلاین
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.Sodoor(TruckNSS.NSSCar.StrCarNo, TruckNSS.NSSCar.StrCarSerialNo, InstanceLoadTargets.GetNSSLoadTarget(LastPermissioned.nCityCode).TargetTravelLength)
                Next

                'پاک کردن اطلاعات
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTWSCapacitor 
                                          Where ShamsiDate='" & _dateTime.GetCurrentDateShamsiFull & "' and TWSStatusId=" & TWSClassLibrary.NobatsManagement.NobatsStatus.Sodoor & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()

            Catch ex As TruckHasNotAnyLoadPermissionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As LoadCapacitorLoadNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As GetNSSException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As TruckNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

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













