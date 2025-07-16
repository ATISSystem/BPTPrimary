Imports System.Reflection


Imports R2Core
Imports R2Core.SoftwareUserManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.SMS
Imports R2Core.SMS.Exceptions
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SMS.SMSTypes
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2CoreParkingSystem.SMS.SMSControllingMoneyWallet.Exceptions
Imports R2Core.SMS.SMSOwners
Imports R2Core.SMS.SMSOwners.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2CoreParkingSystem.SMS.SMSOwners
Imports R2Core.SMS.SMSOwners.R2CoreMClassSMSOwnersManager
Imports R2Core.SMS.SMSHandling.Exceptions



Namespace SMS

    Namespace SMSTypes
        Public MustInherit Class R2CoreParkingSystemSMSTypes
            Inherits R2CoreSMSTypes
            Public Shared ReadOnly Property BlackListRecordAddedAllown = 11
            Public Shared ReadOnly Property EntryExitAllownSMS = 13

        End Class

    End Namespace

    Namespace SMSHandling
        Public Class R2CoreParkingSystemActivateSMSOwnerByCodeInLine
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, New SoftwareUserService(Nothing))
                    Dim InstanceSMSOwners = New R2CoreParkingSystemSMSOwnersManager(New SoftwareUserService(Nothing), New R2DateTimeService)
                    InstanceSMSOwners.ActivateSMSOwner(InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(_MobileNumber), False).UserId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class

        Public Class R2CoreParkingSystemUnActivateSMSOwnerByCodeInLine
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Dim InstanceSMSOwners = New R2CoreParkingSystemMClassSMSOwnersManager(New SoftwareUserService(Nothing), New R2DateTimeService)
                    InstanceSMSOwners.UnActivateSMSOwner(InstanceSoftwareUsers.GetNSSUserUnChangeable(New R2CoreSoftwareUserMobile(_MobileNumber)))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class

    End Namespace

    Namespace SMSControllingMoneyWallet

        Public MustInherit Class R2CoreParkingSystemMClassSMSControllingMoneyWalletManagement

            Private Shared _DateTime As New R2DateTime

            Public Shared Function GetNSSMoneyWallet() As R2CoreParkingSystemStandardTrafficCardStructure
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Return R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 8))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared _ControllingMoneyWalletAccountingExcecutedFlag As Boolean = False
            Public Shared Sub ControllingMoneyWalletAccounting(YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                    Dim InstancePersianCallendar = New R2CoreInstanceDateAndTimePersianCalendarManager
                    'کنترل زمان اجرای فرآیند بر اساس کانفیگ
                    Dim myCurrentDateTime = _DateTime.GetCurrentDateTime
                    Dim TimeOfDay = _DateTime.GetTickofTime(myCurrentDateTime)
                    Dim StTime = TimeSpan.Parse("00:00:00")
                    Dim EndTime = TimeSpan.Parse("00:05:00")
                    Dim ConfigTime = TimeSpan.Parse(InstanceConfigurations.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 9))
                    If TimeOfDay >= StTime And TimeOfDay <= EndTime Then
                        _ControllingMoneyWalletAccountingExcecutedFlag = False
                        Return
                    ElseIf TimeOfDay <= ConfigTime Then
                        Return
                    Else
                    End If
                    'این فرآیند در روز فقط باید یکبار اجرا گردد و نه بیشتر
                    'خط کد زیر یعنی فرآیند امروز قبلا در بازه معین اجرا یکبار اجرا شده است
                    If _ControllingMoneyWalletAccountingExcecutedFlag Then Return
                    'طبق کانفیگ سیستم کلا اکانتینگ فعال باشد یا نه
                    If Not InstanceConfigurations.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 10) Then Return
                    'آغاز فرآیند اکانتینگ
                    'آخرین اکانت ثبت شده
                    Dim NSSLastAccounting = R2CoreParkingSystemMClassAccountingManagement.GetNSSLastAccounting(R2CoreParkingSystemAccountings.SMSControllingMoneyWallet)
                    'امروز یک مرتبه اکانت ثبت شده پس نیازی به ثبت مجدد نیست
                    If NSSLastAccounting.DateShamsiA = myCurrentDateTime.DateShamsiFull Then Return
                    Dim Amount As Int64 = GetAmountforSMSControllingMoneyWallet()
                    'کسر پورسانت شرکت عامل
                    'هزینه شرکت عامل قبلا هنگام فعال سازی اس ام اس کاربر در فیلد ریمایندرشارژ در جدول مالکان اس ام اس منظور شده است
                    'ثبت اکانت
                    Dim NSSControllingMoneyWallet = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 8))
                    R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSControllingMoneyWallet, BagPayType.MinusMoney, Amount, R2CoreParkingSystemAccountings.SMSControllingMoneyWallet, YourNSSUser)

                    _ControllingMoneyWalletAccountingExcecutedFlag = True

                    'ارسال اس ام اس موجودی کیف پول
                    SendSMSSMSControllingMoneyWallet()
                Catch ex As SendSMSControllingMoneyWalletFailedException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Shared Sub SendSMSSMSControllingMoneyWallet()
                Try
                    Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceMoneyWalletManager
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    'کنترل فعال بودن سرویس اس ام اس
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
                    'لیست مقاصد و کاربران
                    Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 17).Split("-")
                    Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                        LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                    Next

                    'موجودی کیف پول
                    Dim myData = New SMSCreationData
                    Dim NSSControllingMoneyWallet = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 8))
                    myData.Data1 = InstanceMoneyWallet.GetMoneyWalletCharge(NSSControllingMoneyWallet)

                    'ارسال اس ام اس
                    If InstanceMoneyWallet.GetMoneyWalletCharge(NSSControllingMoneyWallet) = 0 Then Return
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreSMSTypes.SMSControllingMoneyWallet, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSControllingMoneyWalletFailedException(SMSResultAnalyze)
                Catch ex As SendSMSControllingMoneyWalletFailedException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Sub DoControlforControllingMoneyWallet()
                Try
                    Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                    Dim NSS = GetNSSMoneyWallet()
                    Dim Amount = R2CoreParkingSystemMClassMoneyWalletManagement.GetMoneyWalletCharge(NSS)
                    If Amount < InstanceConfigurations.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 11) Then
                        Throw New SMSControllingMoneyWalletCriticalAmountReachedException
                    End If
                Catch ex As SMSControllingMoneyWalletCriticalAmountReachedException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Function GetAmountforSMSControllingMoneyWallet() As Int64
                Try
                    Dim NSSLast = R2CoreParkingSystemMClassAccountingManagement.GetNSSLastAccounting(R2CoreParkingSystemAccountings.SMSControllingMoneyWallet)
                    Dim Ds As New DataSet
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                                  "Select Sum(SMSOwnerTypes.PriceMinusCommission) as Amount from R2PrimarySMSSystem.dbo.TblSMSOwners As SMSOwners
                                        Inner Join R2PrimarySMSSystem.dbo.TblSMSOwnerTypes as SMSOwnerTypes On SMSOwners.SMSOTypeId=SMSOwnerTypes.SMSOTypeId 
                                   Where SMSOwners.DateTimeMilladi Between '" & _DateTime.GetMilladiDateTimeFromDateShamsiFullFormated(NSSLast.DateShamsiA, NSSLast.TimeA) & "' and '" & _DateTime.GetCurrentDateTimeMilladiFormated() & "'
                                         And SMSOwnerTypes.Active=1 and SMSOwnerTypes.Deleted=0 and SMSOwners.Active=1 and SMSOwners.Deleted=0", 0, Ds, New Boolean).GetRecordsCount = 0 Then
                        Return 0
                    Else
                        Return IIf(Ds.Tables(0).Rows(0).Item("Amount").Equals(System.DBNull.Value), 0, Ds.Tables(0).Rows(0).Item("Amount"))
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SMSControllingMoneyWalletCriticalAmountReachedException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "کیف پول کنترلی اس ام اس نیاز به شارژ دارد"
                    End Get
                End Property
            End Class

            Public Class SendSMSControllingMoneyWalletFailedException
                Inherits ApplicationException

                Private _Message As String
                Public Sub New(YourMessage As String)
                    _Message = vbCrLf + YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "ارسال اس ام اس کیف پول کنترلی اس ام اس با مشکل مواجه شد" + _Message
                    End Get
                End Property
            End Class


        End Namespace

    End Namespace

    Namespace SMSOwners

        Public Class R2CoreParkingSystemMClassSMSOwnersManager

            Private _SoftwareUserService As ISoftwareUserService
            Private _R2DateTimeService As IR2DateTimeService
            Public Sub New(YourSoftwareUserService As ISoftwareUserService, YourR2DateTimeService As IR2DateTimeService)
                _SoftwareUserService = YourSoftwareUserService
                _R2DateTimeService = YourR2DateTimeService
            End Sub

            'Private Sub SMSOwnerAccounting(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            '    Try
            '        'کنترل کسر هزینه اس ام اس
            '        Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager
            '        If Not InstanceSMSOwners.IsActiveSystemAccountingforSendSMS() Then Return

            '        'احراز نوع مالک اس ام اس
            '        Dim InstanceSMSOwnerTypes = New R2CoreMClassSMSOwnerTypesManager
            '        Dim NSSSMSOwnerType = InstanceSMSOwnerTypes.GetNSSSMSOwnerTypeBySoftwareUser(YourNSSSoftwareUser)

            '        'کنترل موجودی کیف پول
            '        Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(New R2DateTimeService)
            '        Dim MoneyWallet = InstanceMoneyWallet.GetMoneyWallet(YourNSSSoftwareUser)
            '        If NSSMoneyWallet.Charge < NSSSMSOwnerType.Price Then
            '            Throw New MoneyWalletCurrentChargeNotEnoughException
            '        End If
            '        'کسر هزینه فعال سازی اس ام اس مبنی بر نوع کاربر
            '        InstanceMoneyWallet.ActMoneyWalletNextStatus(MoneyWallet.MoneyWalletId, BagPayType.MinusMoney, NSSSMSOwnerType.Price, R2CoreParkingSystemAccountings.SoftwareUserSMSOwnerServiceCost, YourNSSUser.UserId)
            '    Catch ex As MoneyWalletCurrentChargeNotEnoughException
            '        Throw ex
            '    Catch ex As SoftwareUserMoneyWalletNotFoundException
            '        Throw ex
            '    Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
            '        Throw ex
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            Private Sub SendingSMSActivateSMSOwner(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                    Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                    Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = String.Empty}}
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2Core.SMS.SMSTypes.R2CoreSMSTypes.ActivateSMSOwnerSuccess, LstCreationData, True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SMSResultException(SMSResultAnalyze)
                Catch ex As SMSResultException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub UnActivateSMSOwner(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set IsSendingActive=0
                                          Where SMSOwnerUserId=" & YourNSSSoftwareUser.UserId & " and IsSendingActive=1"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            'Public Sub ChangeSMSOwnerCurrentState(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            '    Try
            '        Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager
            '        Dim CState As SMSOwnerCurrentState
            '        Try
            '            CState = InstanceSMSOwners.GetNSSSMSOwnerCurrentState(YourNSSSoftwareUser)
            '        Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
            '            ActivateSMSOwner(YourNSSSoftwareUser, YourNSSUser)
            '            Return
            '        Catch ex As Exception
            '            Throw ex
            '        End Try
            '        If CState.IsSendingActive Then
            '            UnActivateSMSOwner(YourNSSSoftwareUser)
            '        Else
            '            ActivateSMSOwner(YourNSSSoftwareUser, YourNSSUser)
            '        End If
            '    Catch ex As MoneyWalletCurrentChargeNotEnoughException
            '        Throw ex
            '    Catch ex As SoftwareUserMoneyWalletNotFoundException
            '        Throw ex
            '    Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
            '        Throw ex
            '    Catch ex As SMSOwnerHasCreditYetException
            '        Throw ex
            '    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
            '        Throw ex
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub



        End Class

        'BPTChanged
        Public Class R2CoreParkingSystemSMSOwnersManager

            Private _SoftwareUserService As ISoftwareUserService
            Private _R2DateTimeService As IR2DateTimeService
            Public Sub New(YourSoftwareUserService As ISoftwareUserService, YourR2DateTimeService As IR2DateTimeService)
                _SoftwareUserService = YourSoftwareUserService
                _R2DateTimeService = YourR2DateTimeService
            End Sub

            'BPTChanged
            Public Sub ChangeSMSOwnerCurrentState(YourSoftwareUserId As Int64)
                Try
                    Dim InstanceSMSOwners = New R2CoreSMSOwnersManager(_SoftwareUserService, _R2DateTimeService)
                    Dim CState As R2CoreSMSOwnersManager.SMSOwnerCurrentState
                    Try
                        CState = InstanceSMSOwners.GetSMSOwnerCurrentState(YourSoftwareUserId, True)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        ActivateSMSOwner(YourSoftwareUserId)
                        Return
                    Catch ex As Exception
                        Throw ex
                    End Try
                    If CState.IsSendingActive Then
                        UnActivateSMSOwner(YourSoftwareUserId)
                    Else
                        ActivateSMSOwner(YourSoftwareUserId)
                    End If
                Catch ex As MoneyWalletCurrentChargeNotEnoughException
                    Throw ex
                Catch ex As SoftwareUserMoneyWalletNotFoundException
                    Throw ex
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As SMSOwnerHasCreditYetException
                    Throw ex
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            'BPTChanged
            Public Sub ActivateSMSOwner(YourSoftwareUserId As Int64)
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceSoftwareUser = New R2CoreSoftwareUsersManager(_R2DateTimeService, _SoftwareUserService)
                    Dim InstanceSMSOwnerTypes = New R2CoreSMSOwnerTypesManager
                    Dim InstanceSMSOwners = New R2CoreSMSOwnersManager(_SoftwareUserService, _R2DateTimeService)
                    Dim SMSOwnerType = InstanceSMSOwnerTypes.GetSMSOwnerTypeBySoftwareUser(YourSoftwareUserId)
                    Dim SMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim SoftwareUser = InstanceSoftwareUser.GetUser(YourSoftwareUserId, False)

                    'فرآیند فعال سازی مالک اس ام اس
                    Try
                        SMSOwner = InstanceSMSOwners.GetSMSOwner(YourSoftwareUserId, True)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        SMSOwnerAccounting(SoftwareUser)
                        InstanceSMSOwners.RegisteringSMSOwner(New R2CoreStandardSMSOwnerStructure(YourSoftwareUserId, SMSOwnerType.SMSOTypeId, SMSOwnerType.PriceMinusCommissionMinusVAT, 0, True, False, Nothing, Nothing, Nothing, _SoftwareUserService.UserId, True, True, False))
                        Return
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try

                    Dim SMSOwnerReminderCreditPercent = InstanceSMSOwners.GetSMSOwnerReminderCreditPercent(SMSOwner)
                    Dim SMSOwnerReminderCredit = SMSOwner.ReminderCharge - SMSOwner.ReminderHolder + SMSOwnerType.PriceMinusCommissionMinusVAT
                    'مالک اس ام اس قبلا فعال است یا خیر
                    If SMSOwner.IsSendingActive Then
                        If (SMSOwner.ReminderCharge - SMSOwner.ReminderHolder) >= SMSOwnerReminderCreditPercent Then
                            Throw New SMSOwnerHasCreditYetException
                        Else
                            SMSOwnerAccounting(SoftwareUser)
                            InstanceSMSOwners.RegisteringSMSOwner(New R2CoreStandardSMSOwnerStructure(YourSoftwareUserId, SMSOwnerType.SMSOTypeId, SMSOwnerReminderCredit, 0, True, False, Nothing, Nothing, Nothing, _SoftwareUserService.UserId, True, True, False))
                        End If
                    Else
                        SMSOwnerAccounting(SoftwareUser)
                        InstanceSMSOwners.RegisteringSMSOwner(New R2CoreStandardSMSOwnerStructure(YourSoftwareUserId, SMSOwnerType.SMSOTypeId, SMSOwnerReminderCredit, 0, True, False, Nothing, Nothing, Nothing, _SoftwareUserService.UserId, True, True, False))
                    End If

                    'ارسال اس ام اس فعال سازی
                    Try
                        SendingSMSActivateSMSOwner(SoftwareUser)
                    Catch ex As Exception
                    End Try

                Catch ex As MoneyWalletCurrentChargeNotEnoughException
                    Throw ex
                Catch ex As SoftwareUserMoneyWalletNotFoundException
                    Throw ex
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As SMSOwnerHasCreditYetException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            'BPTChanged
            Private Sub SMSOwnerAccounting(YourSoftwareUser As R2CoreSoftwareUser)
                Try
                    'کنترل کسر هزینه اس ام اس
                    Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager
                    If Not InstanceSMSOwners.IsActiveSystemAccountingforSendSMS() Then Return

                    'احراز نوع مالک اس ام اس
                    Dim InstanceSMSOwnerTypes = New R2CoreSMSOwnerTypesManager
                    Dim NSSSMSOwnerType = InstanceSMSOwnerTypes.GetSMSOwnerTypeBySoftwareUser(YourSoftwareUser.UserId)

                    'کنترل موجودی کیف پول
                    Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(New R2DateTimeService)
                    Dim MoneyWallet = InstanceMoneyWallet.GetMoneyWallet(YourSoftwareUser)
                    If MoneyWallet.Balance < NSSSMSOwnerType.Price Then
                        Throw New MoneyWalletCurrentChargeNotEnoughException
                    End If
                    'کسر هزینه فعال سازی اس ام اس مبنی بر نوع کاربر
                    InstanceMoneyWallet.ActMoneyWalletNextStatus(MoneyWallet.MoneyWalletId, BagPayType.MinusMoney, NSSSMSOwnerType.Price, R2CoreParkingSystemAccountings.SoftwareUserSMSOwnerServiceCost, _SoftwareUserService.UserId)
                Catch ex As MoneyWalletCurrentChargeNotEnoughException
                    Throw ex
                Catch ex As SoftwareUserMoneyWalletNotFoundException
                    Throw ex
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            'BPTChanged
            Public Sub UnActivateSMSOwner(YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set IsSendingActive=0
                                          Where SMSOwnerUserId=" & YourSoftwareUserId & " and IsSendingActive=1"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            'BPTChanged
            Private Sub SendingSMSActivateSMSOwner(YourSoftwareUser As R2CoreSoftwareUser)
                Try
                    Dim InstanceSMSHandling = New R2CoreSMSHandlerManager(_R2DateTimeService, _SoftwareUserService)
                    Dim LstUser = New List(Of R2CoreSoftwareUser) From {YourSoftwareUser}
                    Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = String.Empty}}
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2Core.SMS.SMSTypes.R2CoreSMSTypes.ActivateSMSOwnerSuccess, LstCreationData, True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SMSResultException(SMSResultAnalyze)
                Catch ex As SMSResultException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub


        End Class

    End Namespace

End Namespace
