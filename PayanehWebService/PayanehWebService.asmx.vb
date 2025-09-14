
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Reflection


Imports R2Core.BaseStandardClass
Imports R2Core.ComputerMessagesManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.Cars
Imports PayanehClassLibrary.ReportsManagement
Imports PayanehClassLibrary.TransportCompanies
Imports PayanehClassLibrary.CarTruckNobatManagement
Imports PayanehClassLibrary.CarTrucksManagement
Imports PayanehClassLibrary.DriverTrucksManagement
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports PayanehClassLibrary.HumanManagement.Personnel
Imports System.Xml
Imports R2CoreTransportationAndLoadNotification.Rmto
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports System.ServiceModel
Imports System.Net


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class PayanehWebService
    Inherits System.Web.Services.WebService

    Private Shared _ExchangeKeyManager As New ExchangeKeyManager

    <WebMethod()>
    Public Function WebMethodLogin(YourUserShenaseh As String, YourUserPassword As String) As Int64
        Try
            Return _ExchangeKeyManager.Login(YourUserShenaseh, YourUserPassword)
        Catch ex As SqlInjectionException
            Throw New Exception("شناسه یا رمز عبور قابل پذیرش نیست")
        Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Function WebMethodGetDSPersonnelFingerPrints(YourSalFull As String, YourMonthCodeFull As String, YourComputerId As Int64, YourExchangeKey As Int64) As DataSet
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            Return PayanehClassLibraryMClassPersonnelAttendanceManagement.GetDSPersonelFingerPrints(YourSalFull, YourMonthCodeFull, YourComputerId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerLoadPermissionsByAHSGs(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAHId As Int64, YourAHSGId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderLoadPermissionIssuedByAHSGs(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourAHId, YourAHSGId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerLoadPermissionsBySeqTs(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourSeqTId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderLoadPermissionIssuedBySeqTs(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourSeqTId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerSedimentedLoadsReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAnnouncementHallId As Int64, YourSedimentedLoadsReportType As Int32, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderSedimentedLoadsReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourAnnouncementHallId, CType(YourSedimentedLoadsReportType, PayanehClassLibraryMClassReportsManagement.SedimentedLoadsReportType))
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerTruckersAssociationFinancialReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTruckersAssociationFinancialReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2})
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerContractorCompanyFinancialReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourVatStatus As Boolean, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderContractorCompanyFinancialReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourVatStatus)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerDriverTruckLoadsReport(YourDriverId As Int64, YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderDriverTruckLoadsReport(YourDriverId, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2})
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerCapacitorLoadsforAnnounceReport(YourAnnouncementHallId As Int64, YourAnnouncementsubGroupId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderCapacitorLoadsforAnnounceReport(YourAnnouncementHallId, YourAnnouncementsubGroupId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerCapacitorLoadsTransportCompaniesRegisteredLoadsReport(YourAHId As Int64, YourAHSGId As Int64, YourCompanyCode As Int64, YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourTargetCityId As Int64, YourSoftwareUserId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderCapacitorLoadsCompanyRegisteredLoadsReport(YourAHId, YourAHSGId, YourCompanyCode, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourTargetCityId, YourSoftwareUserId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerAnnouncementsPerformanceReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAHId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderAnnouncementHallPerformanceReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, PayanehClassLibrary.AnnouncementsManagement.Announcements.PayanehClassLibraryAnnouncementsManagement.GetNSSAnnouncementHall(YourAHId))
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerAnnouncementsPerformanceGeneralStatisticsReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderAnnouncementHallPerformanceGeneralStatisticsReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2})
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerTruckDriversWaitingToGetLoadPermissionByAHSGs(YourAnnouncementsubGroupId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTruckDriversWaitingToGetLoadPermissionByAHSGs(R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroup(YourAnnouncementsubGroupId))
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerTruckDriversWaitingToGetLoadPermissionBySeqts(YourSeqTId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTruckDriversWaitingToGetLoadPermissionBySeqTs(InstanceSequentialTurns.GetNSSSequentialTurn(YourSeqTId))
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerTrucksAverageOfSleepDaysToGetLoadPermissionReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAnnouncementHallId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTrucksAverageOfSleepDaysToGetLoadPermissionReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourAnnouncementHallId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerTravelLengthOfLoadTargetsReport(YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTravelLengthOfLoadTargetsReport()
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationProviderTransportPriceTariffsReport(YourAnnouncementHallId As Int64, YourAnnouncementsubGroupId As Int64, YourOActiveStatus As Boolean, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderTransportPriceTariffsReport(YourAnnouncementHallId, YourAnnouncementsubGroupId, YourOActiveStatus)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationProviderIndigenousTrucksWithUNNativeLPReport(YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderIndigenousTrucksWithUNNativeLPReport()
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerClearanceLoadsReportReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAHId As Int64, YourAHSGId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderClearanceLoadsReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourAHId, YourAHSGId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerAnnounceLoadsReportReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourAHId As Int64, YourAHSGId As Int64, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderAnnouncedLoadsReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2}, YourAHId, YourAHSGId)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Sub WebMethodReportingInformationPrividerSaleOfCommissionSMSReport(YourDateTimeMilladi1 As DateTime, YourShamsiDate1 As String, YourTime1 As String, YourDateTimeMilladi2 As DateTime, YourShamsiDate2 As String, YourTime2 As String, YourExchangeKey As Int64)
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            PayanehClassLibraryMClassReportsManagement.ReportingInformationProviderSaleOfCommissionSMSReport(New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi1, .ShamsiDate = YourShamsiDate1, .Time = YourTime1}, New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi2, .ShamsiDate = YourShamsiDate2, .Time = YourTime2})
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    <WebMethod()>
    Public Function WebMethodCarTruckHasTurn(YourPelak As String, YourSerial As String, YourExchangeKey As Int64) As Boolean
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            Dim myTruck
            Return R2CoreTransportationAndLoadNotificationMClassTurnsManagement.ExistActiveTurn(myTruck)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Function

    <WebMethod()>
    Public Function WebMethodISCompanyActive(YourCompanyCode As Int64, YourExchangeKey As Int64) As Boolean
        Try
            _ExchangeKeyManager.AuthenticationExchangeKey(YourExchangeKey)
            Return TransportCompaniesLoadCapacitorLoadManipulation.ISCompanyActive(YourCompanyCode)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw ex
        Catch ex As ExchangeKeyNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Function

    'BPTChanged
    <WebMethod()>
    Public Function WebMethodGetTruckDriverByNationalCodefromRMTO(YourNationalCode As String, YourExchangeKey As Int64) As R2CoreTransportationAndLoadNotificationTruckDriver
        Try
            Dim NSS = _ExchangeKeyManager.GetNSSUser(YourExchangeKey)
            Return PayanehClassLibraryMClassDriverTrucksManagement.GetDriverTruckfromRMTOAndInsertUpdateLocalDataBaseByNationalCode(YourNationalCode)
        Catch ex As SoftwareUserMobileNumberAlreadyExistException
            Throw GetSoapExeption(ex)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw GetSoapExeption(ex)
        Catch ex As ExchangeKeyNotExistException
            Throw GetSoapExeption(ex)
        Catch ex As GetNSSException
            Throw GetSoapExeption(ex)
        Catch ex As RMTOSmartCardSiteIsNotAvailableException
            Throw GetSoapExeption(ex)
        Catch ex As WebException
            Throw GetSoapExeption(New RMTOSmartCardSiteIsNotAvailableException)
        Catch ex As Exception
            Throw GetSoapExeption(ex)
        End Try
    End Function

    <WebMethod()>
    Public Function WebMethodGetTruckBySmartCarNofromRMTO(YourSmartCardNo As String, YourExchangeKey As Int64) As R2CoreTransportationAndLoadNotificationTruck
        Try
            Dim NSS = _ExchangeKeyManager.GetNSSUser(YourExchangeKey)
            Return PayanehClassLibraryMClassCarTrucksManagement.GetNSSTruckBySmartCardNoWithUpdatingfromRMTO(YourSmartCardNo)
        Catch ex As RMTOSmartCardSiteIsNotAvailableException
            Throw GetSoapExeption(ex)
        Catch ex As ExchangeKeyTimeRangePassedException
            Throw GetSoapExeption(ex)
        Catch ex As ExchangeKeyNotExistException
            Throw GetSoapExeption(ex)
        Catch ex As GetNSSException
            Throw GetSoapExeption(ex)
        Catch ex As WebException
            Throw GetSoapExeption(New RMTOSmartCardSiteIsNotAvailableException)
        Catch ex As GetDataException
            Throw GetSoapExeption(ex)
        Catch ex As Exception
            Throw GetSoapExeption(ex)
        End Try
    End Function

    Private Function GetSoapExeption(YourException As Exception) As SoapException
        Dim SoapEx As New SoapException(YourException.Message, SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, (New XmlDocument).CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace))
        Return SoapEx
    End Function



End Class

