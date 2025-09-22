

Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Reflection
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2CoreLPR.LicensePlateManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.AccountingManagement.ExceptionManagement
Imports R2Core.DateTimeProvider


Namespace AccountingManagement
    Public Enum R2CoreParkingSystemAccountings
        NoneType = 0
        EnterType = 1
        ExitType = 2
        ChargeType = 3
        AnjomanHazinehNobat = 4 'هزینه نوبت انجمن - تریلی و اطاقدار 
        AnjomanHazinehSodorMojavezUpTo72Saat = 5 'در سالن
        AnjomanHazinehSodorMojavezKiosk = 6 'در کیوسک
        AnjomanKargariHazinehNobat = 7 'هزینه نوبت انجمن کارگری
        XXX2 = 8
        HazinehKart = 9 'هزینه کارت تردد
        TransferallChargeToAnother = 10 'انتقال همه موجودی کارت
        SherkatHazinehNobat = 11 'منظور هزینه شرکت وقتی نوبت در اتاق کامپیوتر صادر می گردد
        BlackList = 12 'لیست سیاه
        ExitTemp = 13 'خروج موقت
        ExitSystem = 14 'خروج توسط سیستم
        MoneyWalletReturnAmount = 15 'بازگشت مبلغ مازاد کسر شده
        PrintCopyOfTurn = 16 'چاپ قبض نوبت المثنی
        RegisteringHandyBills = 17 'ثبت قبوض دستی پاکینگ
        SherkatChangeDriverTruck = 18  'هزینه تغییر نام راننده ناوگان باری - شرکت
        SherkatChangeCarTruckNumberPlate = 19 'هزینه تغییر پلاک ناوگان باری - شرکت
        AnjomanChangeDriverTruck = 20  'هزینه تغییر نام راننده ناوگان باری - انجمن
        AnjomanChangeCarTruckNumberPlate = 21 'هزینه تغییر پلاک ناوگان باری - انجمن
        TruckersAssociationControllingMoneyWallet = 22 'کارکرد کیف پول کنترلی کامیونداران
        XXX3 = 23 '
        XXX4 = 24 '
        SoftwareUserSMSOwnerServiceCost = 25 'هزینه فعال سازی سرویس اس ام اس
        SMSControllingMoneyWallet = 26 'کارکرد کیف پول کنترلی اس ام اس
    End Enum

    Public Class R2StandardEnterExitAccountingStructure

        Private _NSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure
        Private _EEAccountingProcessType As R2CoreParkingSystemAccountings
        Private _DateShamsiA As String
        Private _TimeA As String
        Private _DateTimeMilladiA As DateTime
        Private _NSSCar As R2StandardCarStructure
        Private _MaabarCode As String
        Private _MblghA As Int64
        Private _UserIdA As Int64
        Private _CurrentChargeA As Int64
        Private _ReminderChargeA As Int64

#Region "Constructing Management"
        Public Sub New()
            MyBase.New()
            _NSSTrafficCard = Nothing : _EEAccountingProcessType = R2CoreParkingSystemAccountings.NoneType
            _DateShamsiA = "" : _TimeA = "" : _DateTimeMilladiA = Nothing : _NSSCar = Nothing
            _MaabarCode = "" : _MblghA = 0 : _UserIdA = 0 : _CurrentChargeA = 0 : _ReminderChargeA = 0
        End Sub
        Public Sub New(ByVal YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, ByVal YourEEAccountingProcessType As R2CoreParkingSystemAccountings, ByVal YourDateShamsiA As String, ByVal YourTimeA As String, ByVal YourDateTimeMilladiA As DateTime, ByVal YourNSSCar As R2StandardCarStructure, ByVal YourMaabarCode As String, ByVal YourMblghA As Int64, ByVal YourUserIdA As Int64, ByVal YourCurrentChargeA As Int64, ByVal YourReminderChargeA As Int64)
            _NSSTrafficCard = YourNSSTrafficCard
            _EEAccountingProcessType = YourEEAccountingProcessType
            _DateShamsiA = YourDateShamsiA
            _TimeA = YourTimeA
            _DateTimeMilladiA = YourDateTimeMilladiA
            _NSSCar = YourNSSCar
            _MaabarCode = YourMaabarCode
            _MblghA = YourMblghA
            _UserIdA = YourUserIdA
            _CurrentChargeA = YourCurrentChargeA
            _ReminderChargeA = YourReminderChargeA
        End Sub
#End Region
#Region "Properting Management"
        Public Property NSSTrafficCard() As R2CoreParkingSystemStandardTrafficCardStructure
            Get
                Return _NSSTrafficCard
            End Get
            Set(ByVal Value As R2CoreParkingSystemStandardTrafficCardStructure)
                _NSSTrafficCard = Value
            End Set
        End Property
        Public Property EEAccountingProcessType() As R2CoreParkingSystemAccountings
            Get
                Return _EEAccountingProcessType
            End Get
            Set(ByVal Value As R2CoreParkingSystemAccountings)
                _EEAccountingProcessType = Value
            End Set
        End Property
        Public Property DateShamsiA() As String
            Get
                Return _DateShamsiA
            End Get
            Set(ByVal Value As String)
                _DateShamsiA = Value
            End Set
        End Property
        Public Property TimeA() As String
            Get
                Return _TimeA
            End Get
            Set(ByVal Value As String)
                _TimeA = Value
            End Set
        End Property
        Public Property DateTimeMilladiA() As DateTime
            Get
                Return _DateTimeMilladiA
            End Get
            Set(ByVal Value As DateTime)
                _DateTimeMilladiA = Value
            End Set
        End Property
        Public Property NSSCar() As R2StandardCarStructure
            Get
                Return _NSSCar
            End Get
            Set(ByVal Value As R2StandardCarStructure)
                _NSSCar = Value
            End Set
        End Property
        Public Property MaabarCode() As String
            Get
                Return _MaabarCode
            End Get
            Set(ByVal Value As String)
                _MaabarCode = Value
            End Set
        End Property
        Public Property MblghA() As Int64
            Get
                Return _MblghA
            End Get
            Set(ByVal Value As Int64)
                _MblghA = Value
            End Set
        End Property
        Public Property UserIdA() As Int64
            Get
                Return _UserIdA
            End Get
            Set(ByVal Value As Int64)
                _UserIdA = Value
            End Set
        End Property
        Public Property CurrentChargeA() As Int64
            Get
                Return _CurrentChargeA
            End Get
            Set(ByVal Value As Int64)
                _CurrentChargeA = Value
            End Set
        End Property
        Public Property ReminderChargeA() As Int64
            Get
                Return _ReminderChargeA
            End Get
            Set(ByVal Value As Int64)
                _ReminderChargeA = Value
            End Set
        End Property
#End Region


    End Class

    Public Class R2StandardEnterExitAccountingExtendedStructure
        Inherits R2StandardEnterExitAccountingStructure

        Public Sub New()
            MyBase.New
            _ColorName = String.Empty
            _AccountName = String.Empty
            _ComputerName = String.Empty
            _UserName = String.Empty
        End Sub

        Public Sub New(YourNSS As R2StandardEnterExitAccountingStructure, YourColorName As String, YourAccountName As String, YourComputerName As String, YourUserName As String)
            MyBase.New(YourNSS.NSSTrafficCard, YourNSS.EEAccountingProcessType, YourNSS.DateShamsiA, YourNSS.TimeA, YourNSS.DateTimeMilladiA, YourNSS.NSSCar, YourNSS.MaabarCode, YourNSS.MblghA, YourNSS.UserIdA, YourNSS.CurrentChargeA, YourNSS.ReminderChargeA)
            _ColorName = YourColorName
            _AccountName = YourAccountName
            _ComputerName = YourComputerName
            _UserName = YourUserName
        End Sub

        Public Property ColorName() As String
        Public Property AccountName() As String
        Public Property ComputerName() As String
        Public Property UserName() As String

    End Class

    Public Class R2CoreParkingSystemInstanceAccountingManager

        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Function GetAccountingCollection(YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourTotalNumberofAccounts As Int64) As List(Of R2StandardEnterExitAccountingExtendedStructure)
            Try
                Dim Lst = New List(Of R2StandardEnterExitAccountingExtendedStructure)
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top " & YourTotalNumberofAccounts & " Accounting.EEAccountingProcessType,Accountings.AColor,Accountings.AName,Accounting.TimeA,Accounting.DateShamsiA,Accounting.CurrentChargeA,Accounting.MblghA,Accounting.ReminderChargeA,Computers.MDisplayTitle,SoftwareUsers.UserName,Accounting.DateMilladiA,Accounting.maabarcode,Accounting.userida from R2Primary.dbo.TblAccounting as Accounting
                         Inner Join  R2Primary.dbo.TblAccountingCodingTypes as Accountings On Accounting.EEAccountingProcessType=Accountings.ACode
                         Inner Join R2Primary.dbo.TblComputers as Computers On Accounting.MaabarCode=Computers.MId
                         Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Accounting.UserIdA=SoftwareUsers.UserId
                      Where Accounting.CardId=" & YourTrafficCard.CardId & "  Order by Accounting.DateMilladiA Desc", 0, Ds, New Boolean)
                For Loopx As Int16 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim myEEAProcessType As String = Ds.Tables(0).Rows(Loopx).Item("EEAccountingProcessType")
                    Dim myDateShamsiA As String = Ds.Tables(0).Rows(Loopx).Item("dateshamsia")
                    Dim myTimeA As String = Ds.Tables(0).Rows(Loopx).Item("TimeA")
                    Dim myDateTimeMilladiA As DateTime = Ds.Tables(0).Rows(Loopx).Item("datemilladia")
                    Dim myNSSCar As R2StandardCarStructure = New R2StandardCarStructure
                    Dim myMaabarCode As String = Ds.Tables(0).Rows(Loopx).Item("maabarcode")
                    Dim myMblghA As Int64 = Ds.Tables(0).Rows(Loopx).Item("mblgha")
                    Dim myUserIdA As Int64 = Ds.Tables(0).Rows(Loopx).Item("userida")
                    Dim myCurrentChargeA As Int64 = Ds.Tables(0).Rows(Loopx).Item("currentchargea")
                    Dim myReminderChargeA As Int64 = Ds.Tables(0).Rows(Loopx).Item("reminderchargea")
                    Lst.Add(New R2StandardEnterExitAccountingExtendedStructure(New R2StandardEnterExitAccountingStructure(YourTrafficCard, myEEAProcessType, myDateShamsiA, myTimeA, myDateTimeMilladiA, myNSSCar, myMaabarCode, myMblghA, myUserIdA, myCurrentChargeA, myReminderChargeA), Ds.Tables(0).Rows(Loopx).Item("AColor").trim, Ds.Tables(0).Rows(Loopx).Item("AName").trim, Ds.Tables(0).Rows(Loopx).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(Loopx).Item("UserName").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub InsertAccounting(ByVal YourEEAcounting As R2StandardEnterExitAccountingStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                If YourEEAcounting.NSSCar Is Nothing Then
                    CmdSql.CommandText = "insert into R2Primary.dbo.TblAccounting(CardId,EEAccountingProcessType,DateShamsiA,TimeA,DateMilladiA,PelakA,SerialA,CityA,PelakTypeA,MaabarCode,MblghA,UseridA,CurrentChargeA,ReminderChargeA) values('" & YourEEAcounting.NSSTrafficCard.CardId & "'," & YourEEAcounting.EEAccountingProcessType & ",'" & YourEEAcounting.DateShamsiA & "','" & YourEEAcounting.TimeA & "','" & YourEEAcounting.DateTimeMilladiA.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "','','','" & Now.Millisecond.ToString() + Rnd().ToString() & "',0,'" & YourEEAcounting.MaabarCode & "'," & YourEEAcounting.MblghA & "," & YourEEAcounting.UserIdA & "," & YourEEAcounting.CurrentChargeA & "," & YourEEAcounting.ReminderChargeA & ")"
                Else
                    CmdSql.CommandText = "insert into R2Primary.dbo.TblAccounting(CardId,EEAccountingProcessType,DateShamsiA,TimeA,DateMilladiA,PelakA,SerialA,CityA,PelakTypeA,MaabarCode,MblghA,UseridA,CurrentChargeA,ReminderChargeA) values('" & YourEEAcounting.NSSTrafficCard.CardId & "'," & YourEEAcounting.EEAccountingProcessType & ",'" & YourEEAcounting.DateShamsiA & "','" & YourEEAcounting.TimeA & "','" & YourEEAcounting.DateTimeMilladiA.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "','" & YourEEAcounting.NSSCar.StrCarNo & "','" & YourEEAcounting.NSSCar.StrCarSerialNo & "','" & R2CoreParkingSystemMClassCitys.GetCityNameFromnCityCode(YourEEAcounting.NSSCar.nIdCity) & "'," & R2PelakType.None & ",'" & R2CoreMClassConfigurationManagement.GetComputerCode() & "'," & YourEEAcounting.MblghA & "," & YourEEAcounting.UserIdA & "," & YourEEAcounting.CurrentChargeA & "," & YourEEAcounting.ReminderChargeA & ")"
                End If
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2CoreParkingSystemMClassAccountingManagement

        Private Shared _DateTimeService = New R2DateTimeService()
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetAccountingNamebyAccountingCode(ByVal YourAccountingCode As R2CoreParkingSystemAccountings) As String
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select AName from R2Primary.dbo.TblAccountingCodingTypes Where ACode=" & YourAccountingCode & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Ds.Tables(0).Rows(0).Item("AName").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub InsertAccounting(ByVal YourEEAcounting As R2StandardEnterExitAccountingStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                If YourEEAcounting.NSSCar Is Nothing Then
                    CmdSql.CommandText = "insert into R2Primary.dbo.TblAccounting(CardId,EEAccountingProcessType,DateShamsiA,TimeA,DateMilladiA,PelakA,SerialA,CityA,PelakTypeA,MaabarCode,MblghA,UseridA,CurrentChargeA,ReminderChargeA) values('" & YourEEAcounting.NSSTrafficCard.CardId & "'," & YourEEAcounting.EEAccountingProcessType & ",'" & YourEEAcounting.DateShamsiA & "','" & YourEEAcounting.TimeA & "','" & YourEEAcounting.DateTimeMilladiA.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "','','','" & Now.Millisecond.ToString() + Rnd().ToString() & "',0,'" & YourEEAcounting.MaabarCode & "'," & YourEEAcounting.MblghA & "," & YourEEAcounting.UserIdA & "," & YourEEAcounting.CurrentChargeA & "," & YourEEAcounting.ReminderChargeA & ")"
                Else
                    CmdSql.CommandText = "insert into R2Primary.dbo.TblAccounting(CardId,EEAccountingProcessType,DateShamsiA,TimeA,DateMilladiA,PelakA,SerialA,CityA,PelakTypeA,MaabarCode,MblghA,UseridA,CurrentChargeA,ReminderChargeA) values('" & YourEEAcounting.NSSTrafficCard.CardId & "'," & YourEEAcounting.EEAccountingProcessType & ",'" & YourEEAcounting.DateShamsiA & "','" & YourEEAcounting.TimeA & "','" & YourEEAcounting.DateTimeMilladiA.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "','" & YourEEAcounting.NSSCar.StrCarNo & "','" & YourEEAcounting.NSSCar.StrCarSerialNo & "','" & R2CoreParkingSystemMClassCitys.GetCityNameFromnCityCode(YourEEAcounting.NSSCar.nIdCity) & "'," & R2PelakType.None & ",'" & R2CoreMClassConfigurationManagement.GetComputerCode() & "'," & YourEEAcounting.MblghA & "," & YourEEAcounting.UserIdA & "," & YourEEAcounting.CurrentChargeA & "," & YourEEAcounting.ReminderChargeA & ")"
                End If
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetAccountingColorbyAccountingCode(ByVal YourAccountingCode As R2CoreParkingSystemAccountings) As String
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select AColor from R2Primary.dbo.TblAccountingCodingTypes Where ACode=" & YourAccountingCode & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Ds.Tables(0).Rows(0).Item("AColor").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAccountingCollection(YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourTotalNumberofAccounts As Int64) As List(Of R2StandardEnterExitAccountingExtendedStructure)
            Try
                Dim Lst As List(Of R2StandardEnterExitAccountingExtendedStructure) = New List(Of R2StandardEnterExitAccountingExtendedStructure)
                Dim Ds As DataSet = New DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top " & YourTotalNumberofAccounts & " Accounting.EEAccountingProcessType,Accountings.AColor,Accountings.AName,Accounting.TimeA,Accounting.DateShamsiA,Accounting.CurrentChargeA,Accounting.MblghA,Accounting.ReminderChargeA,Computers.MName,SoftwareUsers.UserName,Accounting.DateMilladiA,Accounting.maabarcode,Accounting.userida from R2Primary.dbo.TblAccounting as Accounting
                         Inner Join  R2Primary.dbo.TblAccountingCodingTypes as Accountings On Accounting.EEAccountingProcessType=Accountings.ACode
                         Inner Join R2Primary.dbo.TblComputers as Computers On Accounting.MaabarCode=Computers.MId
                         Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Accounting.UserIdA=SoftwareUsers.UserId
                      Where Accounting.CardId=" & YourTrafficCard.CardId & "  Order by Accounting.DateMilladiA Desc", 1, Ds, New Boolean)
                For Loopx As Int16 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim myEEAProcessType As String = Ds.Tables(0).Rows(Loopx).Item("EEAccountingProcessType")
                    Dim myDateShamsiA As String = Ds.Tables(0).Rows(Loopx).Item("dateshamsia")
                    Dim myTimeA As String = Ds.Tables(0).Rows(Loopx).Item("TimeA")
                    Dim myDateTimeMilladiA As DateTime = Ds.Tables(0).Rows(Loopx).Item("datemilladia")
                    Dim myNSSCar As R2StandardCarStructure = New R2StandardCarStructure
                    Dim myMaabarCode As String = Ds.Tables(0).Rows(Loopx).Item("maabarcode")
                    Dim myMblghA As Int64 = Ds.Tables(0).Rows(Loopx).Item("mblgha")
                    Dim myUserIdA As Int64 = Ds.Tables(0).Rows(Loopx).Item("userida")
                    Dim myCurrentChargeA As Int64 = Ds.Tables(0).Rows(Loopx).Item("currentchargea")
                    Dim myReminderChargeA As Int64 = Ds.Tables(0).Rows(Loopx).Item("reminderchargea")
                    Lst.Add(New R2StandardEnterExitAccountingExtendedStructure(New R2StandardEnterExitAccountingStructure(YourTrafficCard, myEEAProcessType, myDateShamsiA, myTimeA, myDateTimeMilladiA, myNSSCar, myMaabarCode, myMblghA, myUserIdA, myCurrentChargeA, myReminderChargeA), Ds.Tables(0).Rows(Loopx).Item("AColor").trim, Ds.Tables(0).Rows(Loopx).Item("AName").trim, Ds.Tables(0).Rows(Loopx).Item("MName").trim, Ds.Tables(0).Rows(Loopx).Item("UserName").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLastAccounting(YourAccountingCodeType As Int64) As R2StandardEnterExitAccountingStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 * from R2Primary.dbo.TblAccounting as Accounting
                      Where Accounting.EEAccountingProcessType=" & YourAccountingCodeType & "
                      Order By Accounting.DateMilladiA Desc", 0, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return New R2StandardEnterExitAccountingStructure(R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(Convert.ToInt64(DS.Tables(0).Rows(0).Item("CardId"))), DS.Tables(0).Rows(0).Item("EEAccountingProcessType"), DS.Tables(0).Rows(0).Item("DateShamsiA"), DS.Tables(0).Rows(0).Item("TimeA"), DS.Tables(0).Rows(0).Item("DateMilladiA"), Nothing, DS.Tables(0).Rows(0).Item("MaabarCode"), DS.Tables(0).Rows(0).Item("MblghA"), DS.Tables(0).Rows(0).Item("UserIdA"), DS.Tables(0).Rows(0).Item("CurrentChargeA"), DS.Tables(0).Rows(0).Item("ReminderChargeA"))
                Else
                    Throw New LastAccountingRecordforAccountingTypeIdNotFoundException
                End If
            Catch ex As LastAccountingRecordforAccountingTypeIdNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemAccounting
        Public MoneyWalletId As Int64
        Public AccountingTypeId As Int64
        Public DateShamsi As String
        Public Time As String
        Public DateTimeMilladi As DateTime
        Public Mblgh As Int64
        Public SoftwareUserId As Int64
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemAccountingManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Sub InsertAccounting(ByVal YourAccounting As R2CoreParkingSystemAccounting)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "insert into R2Primary.dbo.TblAccounting(CardId,EEAccountingProcessType,DateShamsiA,TimeA,DateMilladiA,PelakA,SerialA,CityA,PelakTypeA,MaabarCode,MblghA,UseridA,CurrentChargeA,ReminderChargeA) values(" & YourAccounting.MoneyWalletId & "," & YourAccounting.AccountingTypeId & ",'" & _DateTimeService.GetCurrentShamsiDate & "','" & _DateTimeService.GetCurrentTime & "','" & _DateTimeService.GetCurrentDateTimeMilladi & "','','','',0,0," & YourAccounting.Mblgh & "," & YourAccounting.SoftwareUserId & ",0,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetMoneyWalletTransactions(YourMoneyWalletIdCard As Int64) As String
            Try
                Dim Ds As DataSet
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 100 Accountings.AName as TransactionTitle,Accountings.AColor as TransactionColor,Accounting.DateShamsiA as ShamsiDate,Accounting.TimeA as Time,Accounting.CurrentChargeA as CurrentBalance,Accounting.MblghA as Amount,Accounting.ReminderChargeA as Reminder,SoftwareUsers.UserName as UserName
                      from R2Primary.dbo.TblAccounting as Accounting
                        Inner Join  R2Primary.dbo.TblAccountingCodingTypes as Accountings On Accounting.EEAccountingProcessType=Accountings.ACode
                        Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Accounting.UserIdA=SoftwareUsers.UserId
                      Where Accounting.CardId=" & YourMoneyWalletIdCard & "
                      Order by Accounting.DateMilladiA Desc
                      for json path", 300, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLastAccounting(YourAccountingTypeId As Int64) As R2CoreParkingSystemAccounting
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select Top 1 * from R2Primary.dbo.TblAccounting as Accounting
                      Where Accounting.EEAccountingProcessType=" & YourAccountingTypeId & "
                      Order By Accounting.DateMilladiA Desc", 0, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return New R2CoreParkingSystemAccounting With {.AccountingTypeId = DS.Tables(0).Rows(0).Item("EEAccountingProcessType"), .MoneyWalletId = DS.Tables(0).Rows(0).Item("CardId"), .Mblgh = DS.Tables(0).Rows(0).Item("MblghA"), .SoftwareUserId = DS.Tables(0).Rows(0).Item("UserIdA"), .DateTimeMilladi = DS.Tables(0).Rows(0).Item("DateMilladiA"), .DateShamsi = DS.Tables(0).Rows(0).Item("DateShamsiA"), .Time = DS.Tables(0).Rows(0).Item("TimeA")}
                Else
                    Throw New LastAccountingRecordforAccountingTypeIdNotFoundException
                End If
            Catch ex As LastAccountingRecordforAccountingTypeIdNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace ExceptionManagement

        Public Class LastAccountingRecordforAccountingTypeIdNotFoundException
            Inherits BPTException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "رکوردی برای نوع اکانتینگ مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace


