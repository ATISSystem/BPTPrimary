﻿
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Timers

Imports R2Core.ConfigurationManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.RFIDCardsManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreGUI
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreLPR.LicensePlateManagement
Imports R2CoreLPR.LicensePlateRecognizer
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.AuthenticationManagement
Imports R2CoreParkingSystem.CamerasManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.City
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.DriverMonitor
Imports R2Core.ComputersManagement
Imports R2Core.LoggingManagement
Imports R2Core.DesktopProcessesManagement
Imports PayanehClassLibrary.CarTruckNobatManagement
Imports PayanehClassLibrary.CarTrucksManagement
Imports PayanehClassLibrary.ConfigurationManagement
Imports PayanehClassLibrary.DriverTruckPresentManagement
Imports PayanehClassLibrary.DriverTrucksManagement
Imports PayanehClassLibrary.Logging
Imports PayanehClassLibrary.ProcessesManagement
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports PayanehClassLibrary.TruckersAssociationControllingMoneyWallet
Imports PayanehClassLibrary.TruckersAssociationControllingMoneyWallet.Exceptions
Imports PayanehClassLibrary.RequesterManagement
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports PayanehClassLibrary.CarTruckNobatManagement.Exceptions
Imports PayanehClassLibrary.TurnRegisterRequest
Imports PayanehClassLibrary.LoadNotification.LoadPermission
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports PayanehClassLibrary.DriverTrucksManagement.Exceptions
Imports R2CoreParkingSystem.Logging
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports PayanehClassLibrary.TurnRegisterRequest.Exceptions
Imports R2CoreParkingSystem.SMS.SMSControllingMoneyWallet
Imports R2CoreParkingSystem.SMS.SMSControllingMoneyWallet.Exceptions

Public Class FrmcEnterExit
    Inherits FrmcGeneral


    Private WithEvents _DateTime As R2DateTime = New R2DateTime
    Private _NSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure = Nothing
    Private WithEvents _TimerClearLastReadedTeraficCard As New System.Timers.Timer
    Private InstanceLogging = New R2CoreInstanceLoggingManager
    Private myLP = New R2StandardLicensePlateStructure("", "", "", R2PelakType.None)
    Private myEnterExitRequest = R2EnterExitRequestType.None


#Region "General Properties"
#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            InitializeSpecial()
            FrmRefresh()
            NewEnterExitRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Protected Overrides Sub SetNSSProcess()
        Try
            SetProcess(R2CoreMClassDesktopProcessesManagement.GetNSSProcess(PayanehClassLibraryProcesses.FrmcEnterExit))
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Protected Sub FrmRefresh()
        UcucSequentialTurnCollection.UCRefreshGeneral()
    End Sub

    Private Sub NewEnterExitRefresh()
        Try
            UcCarPresenter.UCRefresh()
            UcTerafficCardPresenter.UCRefresh()
            UcCarAndTrafficCard.UCRefresh()
            UcMoneyWallet.UCRefresh()
            UcCarImage.UCRefresh()
            UcMoneyWalletCharge.UCRefresh()
            UcMoneyWalletCharge.SendToBack()
            UcCarImage.UCSetMarginColor(Color.White)
            UcBlackListCompositBlackListViewer.Visible = False
            _TimerClearLastReadedTeraficCard.Interval = R2CoreMClassConfigurationOfComputersManagement.GetConfigInt64(R2CoreParkingSystemConfigurations.FrmcEnterExitSetting, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0)
            myLP = New R2StandardLicensePlateStructure("", "", "", R2PelakType.None)
            myEnterExitRequest = R2EnterExitRequestType.None
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub DoProccess(YourCardNo As String, YourChekforTerraficCardRelation As Boolean)
        Try
            Dim NowDateTime = _DateTime.GetCurrentDateTimeMilladi
            Dim myCurrentDateTime = New R2StandardDateAndTimeStructure(NowDateTime, _DateTime.ConvertToShamsiDateFull(NowDateTime), _DateTime.GetCurrentTime(NowDateTime))
            'بروز رساني صفحه تردد
            NewEnterExitRefresh()
            _NSSTrafficCard = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(YourCardNo)
            UcTerafficCardPresenter.UCShowTrafficCard(_NSSTrafficCard)

            'وضعیت تردد در ابتدای فرآیند تنظیم می گردد
            Dim myEnterStatus = R2EnterStatus.NormalEnter
            Dim myExitStatus = R2ExitStatus.NormalExit

            'تردد از نوع خروج است يا ورود و شاخص آخرين تردد
            Dim myEnterExitId As Int64
            myEnterExitRequest = R2CoreParkingSystemMClassEnterExitManagement.GetEnterExitRequestType(_NSSTrafficCard, myEnterExitId)

            'کیف پول کنترلی کامیونداران
            If myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                TruckersAssociationControllingMoneyWalletManagement.DoControlforControllingMoneyWallet()
            End If

            'کیف پول کنترلی اس ام اس
            If myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                R2CoreParkingSystemMClassSMSControllingMoneyWalletManagement.DoControlforControllingMoneyWallet()
            End If

            'نمایش موجودی کیف پول مرتبط با کارت تردد و در صورت رایگان بودن نام شرکت مرتبط
            UcMoneyWallet.UCViewMoneyWalletOnlyCharge(_NSSTrafficCard)
            If _NSSTrafficCard.NoMoney = True Then
                FrmcViewLocalMessage(_NSSTrafficCard.CompanyName + " - " + "تردد آزاد")
                If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then myEnterStatus = R2EnterStatus.EnterNoMoney
                If myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then myExitStatus = R2ExitStatus.ExitNoMony
            End If

            '  فعال بودن يا نبودن كارت
            If _NSSTrafficCard.Active = False Then Throw New TrafficCardNotActiveException

            'کنترل نوع کارت هنگام تردد که مطابق نوع کارت معبر باشد
            If R2CoreParkingSystemMClassTrafficCardManagement.IsTrafficCardTypeSupported(YourCardNo) = False Then
                Dim CName As String = R2CoreParkingSystemMClassTrafficCardManagement.GetTrafficCardTypeName(YourCardNo)
                Throw New TrafficCardTypeNotSupportedOnThisComputerException(CName)
            End If

            'کنترل تطابق نوع کارت و وضعیت ورود و خروج آن با شرایط تنظیم شده برای معبر
            If R2CoreParkingSystemMClassEnterExitManagement.IsTrafficCardEnterExitStatusWithMaabarMatch(myEnterExitRequest, _NSSTrafficCard) = False Then Throw New TrafficCardNotMatchWithThisGateException

            'کنترل ورود و خروج با نوع کارت خوان
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest And R2CoreRFIDCardReaderInterface.GetRFType() = R2CoreRFIDCardReader.RFType.RFType2 Then
                R2CoreRFIDCardReaderInterface.ControlLeds(0, 1)
                Throw New TrafficCardMustReadingBySecondRFReaderException
            ElseIf myEnterExitRequest = R2EnterExitRequestType.ExitRequest And R2CoreRFIDCardReaderInterface.GetRFType = R2CoreRFIDCardReader.RFType.RFType2 Then
                R2CoreRFIDCardReader.ControlLeds(1, 0)
            End If

            'عکس گرفتن و نمایش تصویر خودرو
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.Camera1, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True Then
                    UcCarImage.UCTakeAndViewImage(R2TwoCamera.Camera1)
                Else
                    UcCarImage.UCViewDefaultCarImage(_NSSTrafficCard.CardType)
                End If
            Else
                UcCarImage.UCViewCarEnterExitImage(_NSSTrafficCard)
            End If

            '  احراز پلاک از طریق رابطه پلاک با کارت تردد و سپس در صورت عدم موفقیت از طریق پلاک خوانی
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                Try
                    If YourChekforTerraficCardRelation Then
                        UcCarAndTrafficCard.UCSetTerafficCard(_NSSTrafficCard)
                        myLP = UcCarAndTrafficCard.UCGetLP()
                    End If
                Catch ex As Exception When TypeOf ex Is CarNotExistException OrElse TypeOf ex Is R2CoreParkingSystemRelatedCarNotExistException
                    'دریافت هوشمند از کاربر برای کارت تردد باری
                    If _NSSTrafficCard.CardType = TerafficCardType.Tereili Or _NSSTrafficCard.CardType = TerafficCardType.SixCharkh Or _NSSTrafficCard.CardType = TerafficCardType.TenCharkh Then
                        UcCarImage.UCViewColor(Color.White)
                        UcCarImage.UCSetMarginColor(Color.White)
                        UcCarTruckUpdateInf.Visible = True
                        UcCarTruckUpdateInf.UCRefreshGeneral()
                        UcCarTruckUpdateInf.BringToFront()
                        UcCarTruckUpdateInf.UCFocus()
                        StartReading()
                        Exit Sub
                    End If
                    'پلاک خوانی
                    Dim LPTemp As R2StandardLicensePlateStructure = UcCarImage.UCGetLP()
                    If Object.Equals(LPTemp, Nothing) Then
                    Else
                        myLP = LPTemp
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            ElseIf myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                Dim myLPLinked As R2StandardLicensePlateStructure = R2CoreParkingSystemMClassEnterExitManagement.GetLPfromEnterExit(_NSSTrafficCard)
                If myLPLinked IsNot Nothing Then
                    myLP = myLPLinked
                    Try
                        UcCarAndTrafficCard.UCSetCar(myLP)
                    Catch ex As Exception
                        FrmcViewLocalMessage("Location_1_FrmcEnterExit__RFIDCardReadedEvent:" + ex.Message)
                    End Try
                End If
            End If

            'نمایش مشخصات خودرو ائم از پلاک و سریال
            Try
                Dim nIDcar As Int64 = R2CoreParkingSystemMClassCars.GetnIdCar(myLP)
                UcCarPresenter.UCViewCarInformation(R2CoreParkingSystemMClassCars.GetNSSCar(nIDcar))
            Catch exx As GetDataException
            Catch ex As Exception
                FrmcViewLocalMessage("Location_2_FrmcEnterExit__RFIDCardReadedEvent:" + ex.Message)
            End Try

            'بررسي ليست سياه
            Try
                Dim nIDcar As Int64 = R2CoreParkingSystemMClassCars.GetnIdCar(myLP)
                Dim NSSCarforBlackList As R2StandardCarStructure = R2CoreParkingSystemMClassCars.GetNSSCar(nIDcar)
                If (Not (Object.Equals(NSSCarforBlackList, Nothing))) Then
                    UcBlackListCompositBlackListViewer.UCViewInformation(NSSCarforBlackList)
                    UcBlackListCompositBlackListViewer.UCForceToVisable()
                End If
            Catch exx As GetDataException
            Catch exxx As GetNSSException
            Catch ex As Exception
            End Try

            'اعلان پلاک و سریال به سرویس ارسال اس ام اس
            Try
                R2CoreParkingSystemMClassEnterExitManagement.EntryExitAllownSMSControlling(myLP.Pelak, myLP.Serial)
            Catch ex As Exception
            End Try

            'محاسبه هزينه تردد
            Dim myMblgh As Int64 = 0
            Dim myTavaghof As Int64 = 0
            myMblgh = R2CoreParkingSystemMClassEnterExitManagement.GetEnterExitMblgh(_NSSTrafficCard, myEnterExitRequest, myTavaghof)
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                UcMoneyWallet.UCViewMoneyWalletNextStatus(_NSSTrafficCard, BagPayType.MinusMoney, myMblgh, R2CoreParkingSystemAccountings.EnterType)
            ElseIf myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                UcMoneyWallet.UCViewMoneyWalletNextStatus(_NSSTrafficCard, BagPayType.MinusMoney, myMblgh, R2CoreParkingSystemAccountings.ExitType)
            End If

            'كنترل شارژ موجود نسبت به هزينه تردد
            If (UcMoneyWallet.UCGetReminderCharge < 0) And (myEnterExitRequest = R2EnterExitRequestType.EnterRequest) Then
                Dim myMessage As String = "كارت تردد شما فاقد شارژ مورد نياز است" + vbCrLf + "به واحد شارژ مراجعه نماييد"
                R2CoreParkingSystemMClassDriverMonitor.UpdateDriverMonitorInfForMaabar(New R2CoreParkingSystemDriverMonitorStructure(R2CoreMClassConfigurationManagement.GetComputerCode(), _NSSTrafficCard.CardId, UcMoneyWallet.UCGetMoneyWalletCurrentCharge, myMblgh, UcMoneyWallet.UCGetReminderCharge, myMessage, 18, Color.Red, myLP, myCurrentDateTime.DateTimeMilladiFormated))
            End If
            If (UcMoneyWallet.UCGetReminderCharge < 0) And (myEnterExitRequest = R2EnterExitRequestType.ExitRequest) Then
                InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "كارت تردد شما فاقد شارژ مورد نياز است - هنگام خروج", _NSSTrafficCard.CardNo, 0, 0, 0, 0, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
                If (R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.ChargeActiveOnThisLocation, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True) And (R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserCanCharge = True) Then
                    ChangeMenuStatus("PnlMoneyWalletCharge", False)
                    ''PnlMoneyWalletCharge.Enabled = False
                    UcMoneyWalletCharge.UCPrepare(_NSSTrafficCard, Math.Abs(UcMoneyWallet.UCGetReminderCharge))
                    UcMoneyWalletCharge.BringToFront()
                    UcMoneyWalletCharge.Focus()
                    R2CoreParkingSystemMClassDriverMonitor.UpdateDriverMonitorInfForMaabar(New R2CoreParkingSystemDriverMonitorStructure(R2CoreMClassConfigurationManagement.GetComputerCode(), _NSSTrafficCard.CardId, UcMoneyWallet.UCGetMoneyWalletCurrentCharge, myMblgh, UcMoneyWallet.UCGetReminderCharge, "كارت تردد شما فاقد شارژ مورد نياز است", 18, Color.Red, myLP, myCurrentDateTime.DateTimeMilladiFormated))
                    Exit Try
                Else
                    Dim myMessage As String = "كارت تردد شما فاقد شارژ مورد نياز است" + vbCrLf + "به واحد شارژ مراجعه نماييد"
                    _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, myMessage, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
                    R2CoreParkingSystemMClassDriverMonitor.UpdateDriverMonitorInfForMaabar(New R2CoreParkingSystemDriverMonitorStructure(R2CoreMClassConfigurationManagement.GetComputerCode(), _NSSTrafficCard.CardId, UcMoneyWallet.UCGetMoneyWalletCurrentCharge, myMblgh, UcMoneyWallet.UCGetReminderCharge, myMessage, 18, Color.Red, myLP, myCurrentDateTime.DateTimeMilladiFormated))
                    Exit Try
                End If
            End If

            'بررسی اینکه خودرو با کارتی تردد کرده است که خروج نشده باشد و با کارت حاضر متفاوت باشد
            'در شرایطی که کارت خروج نشده در ورودی می توان کارت جدید داد تا قبلی را بسوزاند و موجودی را به کارت جدید منتقل کند
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                If myLP.Pelak <> "" Or myLP.Serial <> "" Or myLP.City <> "" Or myLP.PelakType <> R2PelakType.None Then
                    Dim LastEnterExitId As Int64
                    Dim LastTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure = R2CoreParkingSystemMClassEnterExitManagement.GetLastTrafficCardWhichNotExited(myLP, LastEnterExitId)
                    If LastTrafficCard IsNot Nothing Then
                        If _NSSTrafficCard.CardNo <> LastTrafficCard.CardNo Then
                            R2CoreParkingSystemMClassTrafficCardManagement.DisallowTerafficCard(LastTrafficCard)
                            R2CoreParkingSystemMClassEnterExitManagement.UpdateForExit(New R2StandardEnterExitStructure(LastEnterExitId, Now, "", "", R2CaptureType.None, R2CameraType.None, Nothing, "", 0, R2EnterStatus.None, 0, 0, Nothing, myCurrentDateTime.DateTimeMilladiFormated, myCurrentDateTime.DateShamsiFull, myCurrentDateTime.Time, R2CaptureType.None, R2CameraType.None, Nothing, LastTrafficCard.CardNo, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, R2ExitStatus.SystemExit, 0, R2CoreMClassConfigurationManagement.GetComputerCode, myLP, True))
                            Dim LastTrafficCardCharge As Int64 = R2CoreParkingSystemMClassMoneyWalletManagement.GetMoneyWalletAllMoney(LastTrafficCard, R2CoreParkingSystemAccountings.ExitSystem, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
                            R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(_NSSTrafficCard, BagPayType.AddMoney, LastTrafficCardCharge, R2CoreParkingSystemAccountings.TransferallChargeToAnother, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS)
                        End If
                    End If
                End If
            End If

            'تراكنش ثبت اطلاعات تردد و کیف پول
            Try
                If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                    Dim myEnterExitIDTemp As UInt64 = R2CoreParkingSystemMClassEnterExitManagement.InsertEnterExit(New R2StandardEnterExitStructure(0, myCurrentDateTime.DateTimeMilladiFormated, myCurrentDateTime.DateShamsiFull, myCurrentDateTime.Time, R2CaptureType.AniiCapture, R2CameraType.EnterCamera, Nothing, _NSSTrafficCard.CardNo, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, myEnterStatus, myMblgh, R2CoreMClassConfigurationManagement.GetComputerCode, myLP, myCurrentDateTime.DateTimeMilladi, myCurrentDateTime.DateShamsiFull, myCurrentDateTime.Time, R2CaptureType.None, R2CameraType.None, Nothing, "", 0, R2ExitStatus.None, 0, 0, New R2StandardLicensePlateStructure, False))
                    'ذخیره سازی تصویر
                    If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.Camera1, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True Then
                        If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.SaveCarPicture, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True Then
                            UcCarImage.UCSaveCarEnterExitImage(New R2StandardEnterExitStructure(myEnterExitIDTemp, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                        End If
                    End If
                    UcMoneyWallet.UCViewandActMoneyWalletNextStatus(_NSSTrafficCard, BagPayType.MinusMoney, UcMoneyWallet.UCGetMblgh, R2CoreParkingSystemAccountings.EnterType)
                    InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت موفقیت آمیز تردد - هنگام ورود", _NSSTrafficCard.CardNo, 0, 0, 0, 0, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, myCurrentDateTime.DateTimeMilladiFormated, myCurrentDateTime.DateShamsiFull))
                ElseIf myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                    R2CoreParkingSystemMClassEnterExitManagement.UpdateForExit(New R2StandardEnterExitStructure(myEnterExitId, Now, "", "", R2CaptureType.None, R2CameraType.None, Nothing, "", 0, R2EnterStatus.None, 0, 0, Nothing, myCurrentDateTime.DateTimeMilladiFormated, myCurrentDateTime.DateShamsiFull, myCurrentDateTime.Time, R2CaptureType.AniiCapture, R2CameraType.Bidirectional, Nothing, _NSSTrafficCard.CardNo, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, myExitStatus, UcMoneyWallet.UCGetMblgh, R2CoreMClassConfigurationManagement.GetComputerCode(), myLP, True))
                    UcMoneyWallet.UCViewandActMoneyWalletNextStatus(_NSSTrafficCard, BagPayType.MinusMoney, UcMoneyWallet.UCGetMblgh, R2CoreParkingSystemAccountings.ExitType)
                    InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت موفقیت آمیز تردد - هنگام خروج", _NSSTrafficCard.CardNo, 0, 0, 0, 0, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, myCurrentDateTime.DateTimeMilladiFormated, myCurrentDateTime.DateShamsiFull))
                End If
            Catch ex As Exception
                Throw New Exception("تراکنش ثبت تردد" + vbCrLf + ex.Message)
            End Try

            'تراکنش موفق
            UcCarImage.UCDrawLP(myLP.GetLPString())
            If myEnterExitRequest = R2EnterExitRequestType.EnterRequest Then
                'ترسیم مستطیل محدوده
                If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.Camera1, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0) = True Then
                    UcCarImage.UCDrawRangeRectangle()
                End If
                Dim myMessage As String = "ورود امكان پذير است"
                R2CoreParkingSystemMClassDriverMonitor.UpdateDriverMonitorInfForMaabar(New R2CoreParkingSystemDriverMonitorStructure(R2CoreMClassConfigurationManagement.GetComputerCode, _NSSTrafficCard.CardId, UcMoneyWallet.UCGetMoneyWalletCurrentCharge, UcMoneyWallet.UCGetMblgh, UcMoneyWallet.UCGetReminderCharge, myMessage, 22, Color.Green, myLP, myCurrentDateTime.DateTimeMilladiFormated))
                UcCarImage.UCSetMarginColor(Color.GreenYellow)
                'فرآیند صدور نوبت ناوگان باری
                If (UcTurnRegisterRequestConfirmation.UCChkTruckNobat = True) And R2CoreTransportationAndLoadNotificationMClassTurnsManagement.IsTerraficCardTypeforTurnRegisteringActive(_NSSTrafficCard) Then
                    Dim NSSTruckTemp = R2CoreTransportationAndLoadNotificationMClassTrucksManagement.GetNSSTruck(R2CoreParkingSystemMClassCars.GetnIdCarFromCardId(_NSSTrafficCard.CardId))
                    'کنترل حضور ناوگان در پارکینگ - درصورتی که طبق کانفیگ باید حضورداشته باشد ولی حضور نداشته باشد آنگاه اکسپشن پرتاب می گردد
                    LoadNotificationLoadPermissionManagement.DoControlforTruckPresentInParkingAndLastLoadPermission(NSSTruckTemp)
                    Dim TurnId As Int64 = Int64.MinValue
                    Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
                    Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(UcucSequentialTurnCollection.UCCurrentNSS, NSSTruckTemp, False, False, TurnId, PayanehClassLibraryRequesters.FrmcEnterExit, TurnType.Permanent, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS, False)
                    _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.SuccessProccess, "نوبت صادر شد" & vbCrLf & "شماره درخواست : " + TurnRegisterRequestId.ToString & vbCrLf & "شماره نوبت :" + TurnId.ToString, String.Empty, FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
                End If
                UcTurnRegisterRequestConfirmation.UCChkTruckNobat = True
                R2CoreRFIDCardReaderInterface.BeepRFIDCardReader(0)
            ElseIf myEnterExitRequest = R2EnterExitRequestType.ExitRequest Then
                Dim myMessage As String = "خروج امكان پذير است"
                R2CoreParkingSystemMClassDriverMonitor.UpdateDriverMonitorInfForMaabar(New R2CoreParkingSystemDriverMonitorStructure(R2CoreMClassConfigurationManagement.GetComputerCode, _NSSTrafficCard.CardId, UcMoneyWallet.UCGetMoneyWalletCurrentCharge, UcMoneyWallet.UCGetMblgh, UcMoneyWallet.UCGetReminderCharge, myMessage, 22, Color.Green, myLP, myCurrentDateTime.DateTimeMilladiFormated))
                R2CoreParkingSystemMClassEnterExitManagement.OpenGate(myEnterExitRequest)
                If (R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreParkingSystemConfigurations.SepasSystem, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 1) = True) And (UcMoneyWallet.UCGetReminderCharge > 10000) Then
                    UcMoneyWallet.UCPrintBillan(UcMoneyWallet.PrintType.Reminder)
                End If
                UcCarImage.UCSetMarginColor(Color.Red)
                R2CoreRFIDCardReaderInterface.BeepRFIDCardReader(0)
                ChangeMenuStatus("PnlMoneyWalletCharge", True)
            End If
        Catch ex As Exception When TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                            OrElse TypeOf ex Is AnySequentialTurnDoNotSelectedException _
                            OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                            OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                            OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                            OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                            OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                            OrElse TypeOf ex Is GetNobatException _
                            OrElse TypeOf ex Is GetNSSException _
                            OrElse TypeOf ex Is GetDataException _
                            OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                            OrElse TypeOf ex Is TruckersAssociationControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is SMSControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                            OrElse TypeOf ex Is TruckNotFoundException _
                            OrElse TypeOf ex Is SequentialTurnNotFoundException _
                            OrElse TypeOf ex Is TruckDriverNotFoundException _
                            OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is TerraficCardNotFoundException _
                            OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                            OrElse TypeOf ex Is TrafficCardNotActiveException _
                            OrElse TypeOf ex Is TrafficCardTypeNotSupportedOnThisComputerException _
                            OrElse TypeOf ex Is TrafficCardNotMatchWithThisGateException _
                            OrElse TypeOf ex Is TrafficCardMustReadingBySecondRFReaderException _
                            OrElse TypeOf ex Is R2CoreParkingSystemCameraFailException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is CarNotExistException _
                            OrElse TypeOf ex Is R2CoreParkingSystemRelatedCarNotExistException _
                            OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
            UcucSequentialTurnCollection.UCRefreshGeneral()
            Throw ex
        Catch ex As Exception
            UcucSequentialTurnCollection.UCRefreshGeneral()
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
        UcucSequentialTurnCollection.UCRefreshGeneral()
    End Sub

    Public Sub PleaseReserveTurn()
        Try
            Dim InstanceComputers = New R2CoreMClassComputersManager
            Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
            Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager
            InstanceTurnRegisterRequest.ReserveTurnRegisterRequest(InstanceComputers.GetNSSCurrentComputer().MId, TurnType.Permanent, InstanceSoftwareUsers.GetNSSSystemUser)
        Catch ex As UserCanNotRequestReserveTurnRegisteringException
        Catch ex As ComputerCanNotRequestReserveTurnRegisteringException
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"


    Private Sub FrmcEnterExit__RFIDCardStartToReadEvent() Handles Me._RFIDCardStartToReadEvent
    End Sub

    Private _LastReadedCardNo As String = String.Empty
    Private Sub FrmcEnterExit__RFIDCardReadedEvent(CardNo As String) Handles Me._RFIDCardReadedEvent
        Try
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            'کنترل این که کارت روی کارت خوان نماند
            If _LastReadedCardNo = CardNo Then
                Exit Try
            Else
                _LastReadedCardNo = CardNo
                _TimerClearLastReadedTeraficCard.Stop()
                _TimerClearLastReadedTeraficCard.Enabled = True
                _TimerClearLastReadedTeraficCard.Start()
            End If
            PleaseReserveTurn()
            DoProccess(CardNo, True)
            UcucSequentialTurnCollection.UCRefreshGeneral()
        Catch ex As Exception When TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                            OrElse TypeOf ex Is AnySequentialTurnDoNotSelectedException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                            OrElse TypeOf ex Is TruckNotFoundException _
                            OrElse TypeOf ex Is SequentialTurnNotFoundException _
                            OrElse TypeOf ex Is TruckDriverNotFoundException _
                            OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                            OrElse TypeOf ex Is TruckersAssociationControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is SMSControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is TerraficCardNotFoundException _
                            OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                            OrElse TypeOf ex Is TrafficCardNotActiveException _
                            OrElse TypeOf ex Is TrafficCardTypeNotSupportedOnThisComputerException _
                            OrElse TypeOf ex Is TrafficCardNotMatchWithThisGateException _
                            OrElse TypeOf ex Is TrafficCardMustReadingBySecondRFReaderException _
                            OrElse TypeOf ex Is R2CoreParkingSystemCameraFailException _
                            OrElse TypeOf ex Is CarNotExistException _
                            OrElse TypeOf ex Is R2CoreParkingSystemRelatedCarNotExistException _
                            OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException _
                            OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                            OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                            OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                            OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                            OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                            OrElse TypeOf ex Is GetNobatException _
                            OrElse TypeOf ex Is GetNSSException _
                            OrElse TypeOf ex Is GetDataException _
                            OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, ex.Message, String.Empty, FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, True)
        Catch ex As Exception When TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException OrElse TypeOf ex Is CarIsNotPresentInParkingException OrElse TypeOf ex Is SequentialTurnIsNotActiveException OrElse TypeOf ex Is TurnPrintingInfNotFoundException OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat OrElse TypeOf ex Is GetNobatException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, True)
        Catch ex As Exception When TypeOf ex Is GetNobatExceptionCarTruckHasNobat
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Information, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Nothing, True)
        Catch ex As Exception
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "ثبت تردد در فرآیند اصلی با خطا مواجه شد", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me, False)
        End Try

        Try
            StartReading()
            UcucSequentialTurnCollection.UCRefreshGeneral()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "خطا در عملکرد دستگاه کارت خوان", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try


        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub UcMoneyWalletCharge_UCMoneyWalletChargedEvent(Mblgh As Long) Handles UcMoneyWalletCharge.UCMoneyWalletChargedEvent
        Try
            Threading.Thread.Sleep(1000)
            UcMoneyWallet.UCViewMoneyWalletOnlyCharge(_NSSTrafficCard)
            UcMoneyWalletCharge.SendToBack()
            FrmcEnterExit__RFIDCardReadedEvent(_NSSTrafficCard.CardNo)
            StartReading()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcMoneyWalletCharge_UCMoneyWalletChargeUserCanceledEvent() Handles UcMoneyWalletCharge.UCMoneyWalletChargeUserCanceledEvent
        Try
            Threading.Thread.Sleep(2000)
            UcMoneyWalletCharge.SendToBack()
            StartReading()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub FrmcEnterExit_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        UcMoneyWalletCharge.DisposeResources()
        UcCarImage.DisposeResources()
    End Sub

    Private Sub FrmcEnterExit__UCDisposeRequest() Handles Me._UCDisposeRequest
        Try
            _TimerClearLastReadedTeraficCard.Dispose()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub _TimerClearLastReadedTeraficCard_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _TimerClearLastReadedTeraficCard.Elapsed
        Try
            _TimerClearLastReadedTeraficCard.Enabled = False
            _TimerClearLastReadedTeraficCard.Stop()
            _LastReadedCardNo = String.Empty
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcMoneyWalletChargePnlMoneyWalletCharge_UCMoneyWalletChargeRFIDCardReadedEvent(CardNo As String) Handles UcMoneyWalletChargePnlMoneyWalletCharge.UCMoneyWalletChargeRFIDCardReadedEvent
        Try
            UcMoneyWalletChargeSavabeghCollectionPnlMoneyWalletCharge.UCRefresh()
            _NSSTrafficCard = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(CardNo)
        Catch exx As GetNSSException
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, "کارت تردد قابل شناسایی نیست", "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcMoneyWalletChargePnlMoneyWalletCharge_UCMoneyWalletChargedEvent(Mblgh As Long) Handles UcMoneyWalletChargePnlMoneyWalletCharge.UCMoneyWalletChargedEvent
        Try
            UcMoneyWalletChargeSavabeghCollectionPnlMoneyWalletCharge.UCViewChargeSavabegh(_NSSTrafficCard)
            UcMoneyWalletChargePnlMoneyWalletCharge.StartReading()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub FrmcEnterExit__MenuRunCompletedEvent(UC As UCMenu) Handles Me._MenuRunCompletedEvent

    End Sub

    Private Sub FrmcEnterExit__MenuRunRequestedEvent(UC As UCMenu) Handles Me._MenuRunRequestedEvent
        Try
            If UC.UCNSSMenu.MenuPanel = "PnlEnterExit" Then
                StartReading()
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlMoneyWalletCharge" Then
                UcMoneyWalletChargePnlMoneyWalletCharge.StartReading()
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlAccounting" Then
                UcAccountingCollection.UCViewAccounting(_NSSTrafficCard)
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlMoneyWalletChargeSavabegh" Then
                UcTerafficCardPresenterPnlMoneyWalletChargeSavabegh.UCShowTrafficCard(_NSSTrafficCard)
                UcMoneyWalletChargeSavabeghCollection.UCViewChargeSavabegh(_NSSTrafficCard)
            ElseIf UC.UCNSSMenu.MenuPanel = "PnlCameraViewTest" Then
                UcCarImage.UCTakeAndViewImage(R2TwoCamera.Camera1)
            End If
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcCarTruckUpdateInf_UCViewCarTruckInformationCompletedEvent(CarId As String) Handles UcCarTruckUpdateInf.UCViewCarTruckInformationCompletedEvent
        Try
            'ایجاد رابطه بین ناوگان و کارت تردد
            UcCarTruckUpdateInf.Visible = False
            UcCarTruckUpdateInf.SendToBack()
            R2CoreParkingSystemMClassCars.CreateRelationBetweenTerafficCardAndCar(_NSSTrafficCard, R2CoreParkingSystemMClassCars.GetNSSCar(CarId))
            R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, PayanehClassLibraryLogType.CarTruckUpdateInfSuccess, "موفقیت در آپدیت اطلاعات ناوگان باری", _NSSTrafficCard.CardNo, UcCarTruckUpdateInf.UcCarTruck.UCGetNSS.StrBodyNo, UcCarTruckUpdateInf.UcCarTruck.UCGetNSS.NSSCar.GetCarPelakSerialComposit(), 0, 0, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            DoProccess(_NSSTrafficCard.CardNo, True)
        Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                            OrElse TypeOf ex Is AnySequentialTurnDoNotSelectedException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                            OrElse TypeOf ex Is TruckNotFoundException _
                            OrElse TypeOf ex Is SequentialTurnNotFoundException _
                            OrElse TypeOf ex Is TruckDriverNotFoundException _
                            OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                            OrElse TypeOf ex Is TruckersAssociationControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is SMSControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                            OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                            OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                            OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                            OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                            OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                            OrElse TypeOf ex Is GetNobatException _
                            OrElse TypeOf ex Is GetNSSException _
                            OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is TerraficCardNotFoundException _
                            OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                            OrElse TypeOf ex Is TrafficCardNotActiveException _
                            OrElse TypeOf ex Is TrafficCardTypeNotSupportedOnThisComputerException _
                            OrElse TypeOf ex Is TrafficCardNotMatchWithThisGateException _
                            OrElse TypeOf ex Is TrafficCardMustReadingBySecondRFReaderException _
                            OrElse TypeOf ex Is R2CoreParkingSystemCameraFailException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is CarNotExistException _
                            OrElse TypeOf ex Is R2CoreParkingSystemRelatedCarNotExistException _
                            OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, False)
        Catch ex As Exception When TypeOf ex Is GetNobatExceptionCarTruckHasNobat
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Information, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Nothing, True)
        Catch ex As Exception
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
        StartReading()
    End Sub

    Private Sub UcCarTruckUpdateInf_UCViewCarTruckInformationNotCompletedEvent(Message As String) Handles UcCarTruckUpdateInf.UCViewCarTruckInformationNotCompletedEvent
        Try
            UcCarTruckUpdateInf.Visible = False
            UcCarTruckUpdateInf.SendToBack()
            R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, PayanehClassLibraryLogType.CarTruckUpdateInfNotSuccess, "عدم موفقیت در آپدیت اطلاعات ناوگان باری", _NSSTrafficCard.CardNo, "SmartCardNo=" + UcCarTruckUpdateInf.UcCarTruck.UcNumberStrBodyNoSearch.UCValue.ToString, 0, 0, Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            DoProccess(_NSSTrafficCard.CardNo, False)
        Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                            OrElse TypeOf ex Is AnySequentialTurnDoNotSelectedException _
                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                            OrElse TypeOf ex Is TruckNotFoundException _
                            OrElse TypeOf ex Is SequentialTurnNotFoundException _
                            OrElse TypeOf ex Is TruckDriverNotFoundException _
                            OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                            OrElse TypeOf ex Is TruckersAssociationControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is SMSControllingMoneyWalletCriticalAmountReachedException _
                            OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                            OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                            OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                            OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                            OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                            OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                            OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                            OrElse TypeOf ex Is GetNobatException _
                            OrElse TypeOf ex Is GetNSSException _
                            OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is TerraficCardNotFoundException _
                            OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                            OrElse TypeOf ex Is TrafficCardNotActiveException _
                            OrElse TypeOf ex Is TrafficCardTypeNotSupportedOnThisComputerException _
                            OrElse TypeOf ex Is TrafficCardNotMatchWithThisGateException _
                            OrElse TypeOf ex Is TrafficCardMustReadingBySecondRFReaderException _
                            OrElse TypeOf ex Is R2CoreParkingSystemCameraFailException _
                            OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                            OrElse TypeOf ex Is CarNotExistException _
                            OrElse TypeOf ex Is R2CoreParkingSystemRelatedCarNotExistException _
                            OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Warning, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Me, False)
        Catch ex As Exception When TypeOf ex Is GetNobatExceptionCarTruckHasNobat
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.Information, ex.Message, "", FrmcMessageDialog.MessageType.PersianMessage, Nothing, Nothing, True)
        Catch ex As Exception
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", _NSSTrafficCard.CardNo, myLP.GetLPString, myEnterExitRequest.ToString, String.Empty, ex.Message, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
        StartReading()
    End Sub

    Private Sub UcTurnRegisterRequestConfirmation_UCUserChangedStatusEvent(Status As Boolean) Handles UcTurnRegisterRequestConfirmation.UCUserChangedStatusEvent
        Try
            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreParkingSystemLogType.EntryExit, "ثبت تردد خودرو", String.Empty, String.Empty, String.Empty, String.Empty, "TurnRegisterRequestConfirmation-StatusChangedTo:" + Status.ToString, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, Nothing, Nothing))
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub


#End Region

#Region "Override Methods"
#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region


End Class