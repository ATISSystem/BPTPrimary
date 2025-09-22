


Imports System.Reflection
Imports PayanehClassLibrary.ConfigurationManagement
Imports PayanehClassLibrary.ReportsManagement
Imports PayanehClassLibrary.SMS.SMSTypes
Imports PayanehClassLibrary.TruckersAssociationControllingMoneyWallet.Exceptions
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.SMS.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.AccountingManagement.ExceptionManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions
Imports R2CoreParkingSystem.SMS.SMSControllingMoneyWallet.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement

Namespace TruckersAssociationControllingMoneyWallet

    'BPTChanged
    Public Class TruckersAssociationControllingMoneyWalletManager

        Private _DateTimeService As R2DateTimeService
        Private _InstanceSqlDataBOX As R2CoreSqlDataBOXManager

        Public Sub New(YourDateTimeService As R2DateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                _InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Private Sub SendSMSTruckersAssociationControllingMoneyWallet(YourTruckersAssociationControllingMoneyWalletId As Int64)
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)
                Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)

                'لیست مقاصد و کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 7).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreSoftwareUser)
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetUser(Convert.ToInt64(TargetUsers(LoopxUsers)), False))
                Next

                'موجودی کیف پول
                Dim CreationData = New SMSCreationData
                CreationData.Data1 = InstanceMoneyWallet.GetMoneyWalletCharge(YourTruckersAssociationControllingMoneyWalletId)

                'ارسال اس ام اس
                If CreationData.Data1 = 0 Then Return
                Dim InstanceSMSHandler = New R2CoreSMSHandlerManager(_DateTimeService, Nothing)
                Dim SMSResult = InstanceSMSHandler.SendSMS(LstSoftwareUsers, PayanehClassLibrarySMSTypes.TruckersAssociationControllingMoneyWallet, InstanceSMSHandler.RepeatSMSCreationData(CreationData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandler.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New TruckersAssociationControllingMoneyWalletSendSMSFailedException(SMSResultAnalyze)
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As SmsSystemIsDisabledException
                Throw ex
            Catch ex As TruckersAssociationControllingMoneyWalletSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetMoneyWallet() As R2Core.MoneyWallet.MoneyWallet.R2CoreMoneyWallet
            Try
                Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)
                Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager(_DateTimeService)
                Return InstanceMoneyWallet.GetMoneyWallet(InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 4), False)
            Catch ex As MoneyWalletNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAmountforTruckersAssociationControllingMoneyWallet(YourLastAccounting As R2CoreParkingSystemAccounting) As Int64
            Try
                Dim Ds As New DataSet
                If _InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                                  "Select Sum(MblghA) as Amount from R2Primary.dbo.TblAccounting 
                                   Where DateMilladiA Between '" & _DateTimeService.GetMilladiDateTimeFromShamsiDate(YourLastAccounting.DateShamsi, YourLastAccounting.Time) & "' and '" & _DateTimeService.GetCurrentDateTimeMilladi() & "'
                                         And (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & " Or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehSodorMojavezUpTo72Saat & " Or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehSodorMojavezKiosk & " Or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.PrintCopyOfTurn & " Or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanChangeCarTruckNumberPlate & " Or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanChangeDriverTruck & ")
	                                     And ISNULl(Deleted,0)<>1", 0, Ds, New Boolean).GetRecordsCount = 0 Then
                    Return 0
                Else
                    Return IIf(Ds.Tables(0).Rows(0).Item("Amount").Equals(System.DBNull.Value), 0, Ds.Tables(0).Rows(0).Item("Amount"))
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Shared _ControllingMoneyWalletExcecutedFlag As Boolean = False
        Public Sub ControllingMoneyWalletAccounting(YourUserId As Int64)
            Try
                Dim InstanceConfigurations = New R2CoreConfigurationsManager(_DateTimeService)
                Dim InstancePersianCallendar = New R2CorePersianCalendarManager(_DateTimeService)
                Dim InstanceAccounting = New R2CoreParkingSystemAccountingManager(_DateTimeService)
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)

                'کنترل زمان اجرای فرآیند بر اساس کانفیگ
                Dim TimeOfDay = _DateTimeService.GetCurrentTickofTime
                Dim StTime = TimeSpan.Parse("00:00:00").Ticks
                Dim EndTime = TimeSpan.Parse("00:05:00").Ticks
                Dim ConfigTime = TimeSpan.Parse(InstanceConfigurations.GetConfigString(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 6)).Ticks
                If TimeOfDay >= StTime And TimeOfDay <= EndTime Then
                    _ControllingMoneyWalletExcecutedFlag = False
                    Return
                ElseIf TimeOfDay <= ConfigTime Then
                    Return
                Else
                End If

                'این فرآیند در روز فقط باید یکبار اجرا گردد و نه بیشتر
                'خط کد زیر یعنی فرآیند امروز قبلا در بازه معین اجرا یکبار اجرا شده است
                If _ControllingMoneyWalletExcecutedFlag Then Return

                'طبق کانفیگ سیستم کلا اکانتینگ فعال باشد یا نه
                If Not InstanceConfigurations.GetConfigBoolean(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 1) Then Return

                'آغاز فرآیند اکانتینگ
                'آخرین اکانت ثبت شده
                Dim LastAccounting = InstanceAccounting.GetLastAccounting(R2CoreParkingSystemAccountings.TruckersAssociationControllingMoneyWallet)

                'امروز یک مرتبه اکانت ثبت شده پس نیازی به ثبت مجدد نیست
                If LastAccounting.DateShamsi = _DateTimeService.GetCurrentShamsiDate Then Return

                'کسر هزینه شرکت خودگردان
                'درصد شرکت خودگردان - ارزش افزوده
                Dim Amount As Int64 = GetAmountforTruckersAssociationControllingMoneyWallet(LastAccounting)
                If InstanceConfigurations.GetConfigBoolean(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 2) Then
                    Dim CostofSelfGoverning = InstanceConfigurations.GetConfigInt64(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 3)
                    Amount = Amount * (100 / (100 + CostofSelfGoverning))
                End If
                Dim MoneyWalletId = GetMoneyWallet.MoneyWalletId
                InstanceMoneyWallet.ActMoneyWalletNextStatus(MoneyWalletId, BagPayType.MinusMoney, Amount, R2CoreParkingSystemAccountings.TruckersAssociationControllingMoneyWallet, YourUserId)
                _ControllingMoneyWalletExcecutedFlag = True
                SendSMSTruckersAssociationControllingMoneyWallet(MoneyWalletId)
            Catch ex As LastAccountingRecordforAccountingTypeIdNotFoundException
                Throw ex
            Catch ex As MoneyWalletNotFoundException
                Throw ex
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As SmsSystemIsDisabledException
                Throw ex
            Catch ex As SendSMSControllingMoneyWalletFailedException
                Throw ex
            Catch ex As TruckersAssociationControllingMoneyWalletSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DoControlforControllingMoneyWallet()
            Try
                Dim InstanceConfigurations = New R2CoreConfigurationsManager(_DateTimeService)
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)
                Dim Inventory = InstanceMoneyWallet.GetMoneyWalletCharge(GetMoneyWallet.MoneyWalletId)
                If Inventory < InstanceConfigurations.GetConfigInt64(PayanehClassLibraryConfigurations.TruckersAssociationControllingMoneyWallet, 5) Then
                    Throw New TruckersAssociationControllingMoneyWalletCriticalAmountReachedException
                End If
            Catch ex As TruckersAssociationControllingMoneyWalletCriticalAmountReachedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    'BPTChanged
    Namespace Exceptions
        Public Class TruckersAssociationControllingMoneyWalletCriticalAmountReachedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کیف پول کنترلی کامیونداران نیاز به شارژ دارد"
                End Get
            End Property
        End Class

        Public Class TruckersAssociationControllingMoneyWalletSendSMSFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس کیف پول کنترلی کامیونداران با مشکل مواجه شد" + _Message
                End Get
            End Property
        End Class

    End Namespace


End Namespace
