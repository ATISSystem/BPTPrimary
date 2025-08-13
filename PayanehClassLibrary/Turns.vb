
Imports System.Data.SqlClient
Imports System.Reflection


Imports PayanehClassLibrary.CarTrucksManagement
Imports PayanehClassLibrary.ConfigurationManagement
Imports PayanehClassLibrary.DataBaseManagement
Imports PayanehClassLibrary.DriverTrucksManagement
Imports PayanehClassLibrary.LoadNotification.LoadPermission
Imports R2Core
Imports R2Core.PublicProc
Imports R2Core.ConfigurationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.LoggingManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadPermission
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.TurnPrintRequest
Imports PayanehClassLibrary.DriverTrucksManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports TWSClassLibrary.TDBClientManagement
Imports PayanehClassLibrary.Logging
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports PayanehClassLibrary.TurnRegisterRequest
Imports R2CoreTransportationAndLoadNotification.TransportCompanies
Imports R2CoreTransportationAndLoadNotification.TerraficCardsManagement
Imports R2Core.PermissionManagement
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports PayanehClassLibrary.RequesterManagement
Imports PayanehClassLibrary.CarTruckNobatManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2Core.PermissionManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2Core.MoneyWallet.Exceptions
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports R2Core.SMS.SMSHandling
Imports PayanehClassLibrary.SMS.SMSTypes
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports PayanehClassLibrary.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.CalendarManagement.SpecializedPersianCalendar
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness.Exceptions
Imports PayanehClassLibrary.CarTruckNobatManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2CoreTransportationAndLoadNotification.TurnRegisterRequest
Imports R2CoreTransportationAndLoadNotification.Turns.TurnAccounting
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions


Namespace CarTruckNobatManagement

    Public Class R2StandardCarTruckNobatStructure
        Private _nEnterExitId As Int64
        Private _EnterDate As String
        Private _EnterTime As String
        Private _NSSDriverTruck As R2StandardDriverTruckStructure
        Private _bFlagDriver As Boolean
        Private _NUserIdEnter As Int64
        Private _BillOfLadingNumber As String
        Private _OtaghdarTurnNumber As String
        Private _StrCardNo As Int64
        Private _RegisteringTimeStamp As DateTime


        Public Sub New()
            MyBase.New()
            _nEnterExitId = 0 : _EnterDate = "" : _EnterTime = "" : _NSSDriverTruck = Nothing : _bFlagDriver = True : _NUserIdEnter = 0 : _BillOfLadingNumber = String.Empty : _OtaghdarTurnNumber = "" : _StrCardNo = 0 : _RegisteringTimeStamp = Now
        End Sub

        Public Sub New(ByVal YournEnterExitId As Int64, ByVal YourEnterDate As String, YourEnterTime As String, YourNSSDriverTruck As R2StandardDriverTruckStructure, YourbFlagDriver As Boolean, YournUserIdEnter As Int64, YourBillOfLadingNumber As String, YourOtaghdarTurnNumber As String, YourStrCardNo As Int64, YourRegisteringTimeStamp As DateTime)
            _nEnterExitId = YournEnterExitId
            _EnterDate = YourEnterDate
            _EnterTime = YourEnterTime
            _NSSDriverTruck = YourNSSDriverTruck
            _bFlagDriver = YourbFlagDriver
            _NUserIdEnter = YournUserIdEnter
            _BillOfLadingNumber = YourBillOfLadingNumber
            _OtaghdarTurnNumber = YourOtaghdarTurnNumber
            _StrCardNo = YourStrCardNo
            _RegisteringTimeStamp = YourRegisteringTimeStamp
        End Sub

        Public Property nEnterExitId() As Int64
            Get
                Return _nEnterExitId
            End Get
            Set(ByVal Value As Int64)
                _nEnterExitId = Value
            End Set
        End Property
        Public Property EnterDate() As String
            Get
                Return _EnterDate
            End Get
            Set(ByVal Value As String)
                _EnterDate = Value
            End Set
        End Property
        Public Property EnterTime() As String
            Get
                Return _EnterTime
            End Get
            Set(ByVal Value As String)
                _EnterTime = Value
            End Set
        End Property
        Public Property NSSDriverTruck() As R2StandardDriverTruckStructure
            Get
                Return _NSSDriverTruck
            End Get
            Set(ByVal Value As R2StandardDriverTruckStructure)
                _NSSDriverTruck = Value
            End Set
        End Property
        Public Property bFlagDriver() As Boolean
            Get
                Return _bFlagDriver
            End Get
            Set(ByVal Value As Boolean)
                _bFlagDriver = Value
            End Set
        End Property
        Public Property nUserIdEnter() As Int64
            Get
                Return _NUserIdEnter
            End Get
            Set(ByVal Value As Int64)
                _NUserIdEnter = Value
            End Set
        End Property
        Public Property BillOfLadingNumber() As String
            Get
                Return _BillOfLadingNumber
            End Get
            Set(ByVal Value As String)
                _BillOfLadingNumber = Value
            End Set
        End Property
        Public Property OtaghdarTurnNumber() As String
            Get
                Return _OtaghdarTurnNumber
            End Get
            Set(ByVal Value As String)
                _OtaghdarTurnNumber = Value
            End Set
        End Property
        Public ReadOnly Property OtaghdarTurnNumberSummary() As String
            Get
                Return Mid(_OtaghdarTurnNumber, 7, 20)
            End Get
        End Property
        Public Property StrCardNo() As Int64
            Get
                Return _StrCardNo
            End Get
            Set(ByVal Value As Int64)
                _StrCardNo = Value
            End Set
        End Property
        Public Property RegisteringTimeStamp() As DateTime
            Get
                Return _RegisteringTimeStamp
            End Get
            Set(ByVal Value As DateTime)
                _RegisteringTimeStamp = Value
            End Set
        End Property




    End Class

    Public Structure PayanehClassLibraryTurnDetails
        Public nEnterExitId As Int64
        Public TruckLPString As String
        Public StrEnterDate As String
        Public StrEnterTime As String
        Public TruckDriver As String
        Public OtaghdarSeqTurnNumber As String
        Public TurnStatusTitle As String
    End Structure

    Public Class PayanehClassLibraryMClassCarTruckNobatManager
        Private _DateTime As New DateAndTimeManagement.R2DateTime

        Public Sub TurnsCancellationByLoadTargetMethod(YourNSSSequentialTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourTurnId As Int64, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DSTurns As DataSet = Nothing
                Dim TotalTurns = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select nEnterExitId from dbtransport.dbo.TbEnterExit
                        Where (TurnStatus = " & TurnStatuses.Registered & " Or TurnStatus = " & TurnStatuses.UsedLoadAllocationRegistered & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadAllocationCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadPermissionCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationUser & ")
                              and Substring(OtaghdarTurnNumber,1,1)='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' and nEnterExitId<" & YourTurnId & "", 0, DSTurns, New Boolean).GetRecordsCount
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUnderScore & ",bFlag=1,bFlagDriver=1,strElamDate='" & _DateTime.GetCurrentDateShamsiFull & "',strElamTime='" & _DateTime.GetCurrentTime & "',nUserIdExit=" & YourNSSSoftwareUser.UserId & " 
                                      Where (TurnStatus = " & TurnStatuses.Registered & " Or TurnStatus = " & TurnStatuses.UsedLoadAllocationRegistered & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadAllocationCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadPermissionCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationUser & ") 
                                      and Substring(OtaghdarTurnNumber, 1, 1) ='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' and nEnterExitId<" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'در آنلاین
                For LoopOnLine As Int64 = 0 To DSTurns.Tables(0).Rows.Count - 1
                    Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(DSTurns.Tables(0).Rows(LoopOnLine).Item("nEnterExitId")).NSSCar.nIdCar)
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSCarTruck.NSSCar.StrCarNo, NSSCarTruck.NSSCar.StrCarSerialNo)
                Next
                R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, PayanehClassLibraryLogType.TurnsCancellation, "کنسل کردن گروهی نوبت ها", "SeqT=" + YourNSSSequentialTurn.SequentialTurnTitle, "TotalTurn=" + TotalTurns.ToString, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
            Catch ex As SqlInjectionException
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

        Private Sub TurnsCancellationBy3DaysMethod(YourNSSSequentialTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourTimeOfDay As R2StandardDateAndTimeStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                Dim DSTurns As DataSet = Nothing
                Dim TotalTurns = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select nEnterExitId from dbtransport.dbo.tbEnterExit as Turns
                        Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                              Substring(Turns.OtaghdarTurnNumber,1,1)='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' and
                              ((Turns.strCardno in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 5) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & YourTimeOfDay.DateShamsiFull & "' Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
   	                           )
	                           or
	                           (Turns.strCardno not in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 6) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & YourTimeOfDay.DateShamsiFull & "' Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
 	                           )
                              )
                        order by nEnterExitId Desc", 0, DSTurns, New Boolean).GetRecordsCount
                If TotalTurns > 0 Then
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText =
                     "Update dbtransport.dbo.TbEnterExit
                      Set TurnStatus=" & TurnStatuses.CancelledUnderScore & ",bFlag=1,bFlagDriver=1,strElamDate='" & YourTimeOfDay.DateShamsiFull & "',strElamTime='" & YourTimeOfDay.Time & "',nUserIdExit=" & YourNSSSoftwareUser.UserId & " 
                      Where nEnterExitId In   
                       (Select nEnterExitId from dbtransport.dbo.tbEnterExit as Turns
                        Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                              Substring(Turns.OtaghdarTurnNumber,1,1)='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' and
                              ((Turns.strCardno in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 5) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & YourTimeOfDay.DateShamsiFull & "' Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
   	                           )
	                           or
	                           (Turns.strCardno not in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 6) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & YourTimeOfDay.DateShamsiFull & "' Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
 	                           )
                              )
                       )"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    'در آنلاین
                    For LoopOnLine As Int64 = 0 To DSTurns.Tables(0).Rows.Count - 1
                        Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(DSTurns.Tables(0).Rows(LoopOnLine).Item("nEnterExitId")).NSSCar.nIdCar)
                        TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSCarTruck.NSSCar.StrCarNo, NSSCarTruck.NSSCar.StrCarSerialNo)
                    Next
                    'Dim TurnsSB = New StringBuilder
                    'For LoopxLog As Int64 = 0 To DSTurns.Tables(0).Rows.Count - 1
                    '    TurnsSB.Append(DSTurns.Tables(0).Rows(LoopxLog).Item("nEnterExitId")).Append(",")
                    'Next
                    R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, PayanehClassLibraryLogType.TurnsCancellation, "کنسل کردن گروهی نوبت ها", "SeqT=" + YourNSSSequentialTurn.SequentialTurnTitle, "TotalTurn=" + TotalTurns.ToString, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
                End If
            Catch ex As SqlInjectionException
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

        Public Sub TurnsCancellation(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanExcecuteTurnCancellation, YourNSSSoftwareUser.UserId, 0) Then Throw New PermissionException

                Dim InstanceSpecializedPersianCalendar = New R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager(_DateTime)
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
                Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                Dim TimeOfDay = _DateTime.GetCurrentDateTime()
                'طبق کانفیگ سیستم کلا ابطال نوبت ها فعال باشد یا نه
                If Not InstanceConfigurations.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 0) Then Return

                'ابطال نوبت ها در روزهای تعطیل صورت نمی گیرد
                If InstanceSpecializedPersianCalendar.IsTodayIsHoliday() Then Return

                'کنترل این که ممکن است هنوز آزاد سازی بار تمام نشده باشد لذا ابطال نوبت ها نباید انجام گیرد حتی اگر زمان ابطال نوبت ها فرارسیده باشد
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
                If InstanceLoadAllocation.GetLoadAllocationsforLoadPermissionRegistering().Count <> 0 Then Exit Sub
                'ابطال نوبت ها
                Dim LstSeqTs = InstanceSequentialTurns.GetSequentialTurns()
                For Loopx As Int64 = 0 To LstSeqTs.Count - 1
                    Dim ComposeSearchString As String = LstSeqTs(Loopx).SequentialTurnId.ToString + "="
                    Dim AllSeqTConfig As String() = Split(InstanceConfigurations.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 2), "-")
                    Dim ConfigSeqTTurnsCancellationActiveFlag As Boolean = Split(Mid(AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "|")(0)
                    Dim ConfigSeqTTurnsCancellationStartSendingTime As String = Split(Mid(AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "|")(1)
                    If Not ConfigSeqTTurnsCancellationActiveFlag Then Continue For
                    If TimeOfDay.Time < ConfigSeqTTurnsCancellationStartSendingTime Then Continue For
                    TurnsCancellationBy3DaysMethod(LstSeqTs(Loopx), YourNSSSoftwareUser, TimeOfDay)
                Next
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TurnCancellation(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourOtaghdarTurnNumber As String, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourOtaghdarTurnNumber)

                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim Truck = InstanceTrucks.GetNSSTruck(YourNSSSoftwareUser)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUser & ",bFlag=1,bFlagDriver=1,strElamDate='" & _DateTime.GetCurrentDateShamsiFull & "',strElamTime='" & _DateTime.GetCurrentTime & "',nUserIdExit=" & YourNSSUser.UserId & " 
                                          Where StrCardNo=" & Truck.NSSCar.nIdCar & " and OtaghdarTurnNumber='" & YourOtaghdarTurnNumber & "' and (TurnStatus=" & TurnStatuses.Registered & " or TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or TurnStatus=" & TurnStatuses.ResuscitationUser & ")"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()

                'ارسال ابطالی به آنلاین نوبت
                TWSClassTDBClientManagement.DelNobat(Truck.NSSCar.StrCarNo, Truck.NSSCar.StrCarSerialNo)
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As UserNotExistByMobileNumberException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TurnCancellationWithLicensePlate(YourLPPelak As String, YourLPSerial As String, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourTurnStatuses As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourLPPelak)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourLPSerial)

                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanExcecuteTurnCancellationWithLicensePlate, YourNSSSoftwareUser.UserId, 0) Then Throw New PermissionException

                Dim InstanceCarTrucks = New PayanehClassLibraryMClassCarTrucksManager
                Dim NSSTruck = New R2CoreTransportationAndLoadNotificationStandardTruckStructure(New R2StandardCarStructure(Nothing, Nothing, YourLPPelak, YourLPSerial, Nothing), Nothing)
                Dim TruckId As Int64 = Nothing
                If InstanceCarTrucks.IsExistCarTruckWithLicensePlate(New R2StandardCarTruckStructure(NSSTruck.NSSCar, Nothing), TruckId) Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & YourTurnStatuses & ",bFlag=1,bFlagDriver=1,nUserIdExit=" & YourNSSSoftwareUser.UserId & ",strElamDate='" & _DateTime.GetCurrentDateShamsiFull & "',strElamTime='" & _DateTime.GetCurrentTime & "'
                                          Where StrCardNo=" & TruckId & " and (TurnStatus=" & TurnStatuses.Registered & " or TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & " or TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & " or TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or TurnStatus=" & TurnStatuses.ResuscitationUser & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    If R2CoreMClassConfigurationManagement.GetConfigBoolean(PayanehClassLibraryConfigurations.TWS, 0) Then
                        TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo)
                    End If
                Else
                    Throw New TruckNotFoundException
                End If
            Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                                OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                                OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                                OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                                OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                                OrElse TypeOf ex Is GetNobatException _
                                OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException _
                                OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                                OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                                OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                                OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                                OrElse TypeOf ex Is TerraficCardNotFoundException _
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                                OrElse TypeOf ex Is SqlInjectionException _
                                OrElse TypeOf ex Is PermissionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Function GetTurnofKiosk(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourNSSTruckDriver As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure, YourNSSTransportCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure, YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourTWSForce As Boolean) As Int64
        '    Dim CmdSql As SqlCommand = New SqlCommand
        '    CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
        '    Try
        '        Dim InstanceTerraficCards = New R2CoreTransportationAndLoadNotificationInstanceTerraficCardsManager
        '        Dim NSSMoneyWallet = InstanceTerraficCards.GetNSSTerafficCard(YourNSSTransportCompany)
        '        Dim CurrentDateTimeMilladiFormated = _DateTime.GetCurrentDateTimeMilladiFormated
        '        Dim CurrentDateShamsiFull = _DateTime.GetCurrentDateShamsiFull
        '        'تراکنش ثبت روابط موقت ناوگان و راننده باری و کیف پول و زیرگروه اعلام بار
        '        'کلیه روابط به صورت موقت ایجاد می گردند و از طریق فیلد تایم استمپ در کل سیستم ایزوله می شوند و شناسایی می گردند
        '        CmdSql.Connection.Open()
        '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction
        '        CmdSql.CommandText = "Insert into dbtransport.dbo.TbCarAndPerson(nIDCar,nIDPerson,snRelation,dDate,strDesc,RelationTimeStamp)
        '          				      Values(" & YourNSSTruck.NSSCar.nIdCar & "," & YourNSSTruckDriver.NSSDriver.nIdPerson & ",2,'" & CurrentDateShamsiFull & "','TempRelation','" & CurrentDateTimeMilladiFormated & "')"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.CommandText = "Insert Into R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars(CardId, nCarId, RelationActive, RelationTimeStamp) 
        '          				      Values(" & NSSMoneyWallet.CardId & "," & YourNSSTruck.NSSCar.nIdCar & ",1,'" & CurrentDateTimeMilladiFormated & "')"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationCars(CarId,AHSGId,Priority,RelationActive,RelationTimeStamp)
        '  				          Values(" & YourNSSTruck.NSSCar.nIdCar & "," & YourNSSLoadCapacitorLoad.AHSGId & ",1,1,'" & CurrentDateTimeMilladiFormated & "')"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
        '        Dim TurnId = Int64.MinValue
        '        Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
        '        Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(YourNSSSeqT, YourNSSTruck, True, True, TurnId, PayanehClassLibraryRequesters.GetTurnofKiosk, TurnType.Temporary, YourUserNSS, YourTWSForce)
        '        Return TurnId
        '    Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
        '                        OrElse TypeOf ex Is SoftwareUserMoneyWalletNotFoundException _
        '                        OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
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

        Public Function GetHazinehSodoorNobat(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                If YourNSSTerafficCard.CardType = TerafficCardType.Tereili Then Return GetSherkatHazinehNobatMblgh(YourNSSTerafficCard) + InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 1) + InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4)
                'If YourNSSTerafficCard.CardType = TerafficCardType.SixCharkh Or YourNSSTerafficCard.CardType = TerafficCardType.TenCharkh Then Return GetSherkatHazinehNobatMblgh(YourNSSTerafficCard) + InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSherkatHazinehNobatMblgh(ByVal YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                'این روتین فعلا فقط مخصوص تریلی است که اقدام به صدور نوبت در اتاق کامپیوترر می کند - محاسبه هزینه متفاوت از تردد نیست
                'در صورتی که ناوگان در محدوده 72 ساعت از صدور نوبت باشد(نه تردد) و مجددا نوبت بخواهد هزینه دارد - مثلا بار تهران می برد و قبل 72 برمیگردد نوبت بزند
                'مبلغ پایه هزینه صدور نوبت برای شرکت از کانفیگ
                Dim mySherkatHazinehNobat As Int64
                Dim CurrentDateTimeMilladi = _DateTime.GetCurrentDateTimeMilladi
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If YourTrafficCard.CardType = TerafficCardType.Tereili Then mySherkatHazinehNobat = InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 0)
                If YourTrafficCard.CardType = TerafficCardType.SixCharkh Then mySherkatHazinehNobat = InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 5)
                If YourTrafficCard.CardType = TerafficCardType.TenCharkh Then mySherkatHazinehNobat = InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 6)
                'احراز معیار محاسبه
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (MblghA<>0) and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.EnterType & " or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.SherkatHazinehNobat & " ) order by DateMilladiA desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then Return mySherkatHazinehNobat
                Dim Tavaghof As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), CurrentDateTimeMilladi)
                If YourTrafficCard.CardType = TerafficCardType.Tereili Then
                    If Tavaghof >= InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 2) Then
                        Return mySherkatHazinehNobat
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), CurrentDateTimeMilladi)
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                ElseIf YourTrafficCard.CardType = TerafficCardType.SixCharkh Then
                    If Tavaghof >= InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 11) Then
                        Return mySherkatHazinehNobat
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), CurrentDateTimeMilladi)
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                ElseIf YourTrafficCard.CardType = TerafficCardType.TenCharkh Then
                    If Tavaghof >= InstanceConfiguration.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 12) Then
                        Return mySherkatHazinehNobat
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), CurrentDateTimeMilladi)
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSCarTruckNobat(YournEnterExitId As Int64) As R2StandardCarTruckNobatStructure
            Try
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceDriverTrucks = New PayanehClassLibraryMClassDriverTrucksManager
                If InstanceSqlDataBOX.GetDataBOX(New DataBaseManagement.R2ClassSqlConnectionSepas, "Select Top 1 * from dbtransport.dbo.TbEnterExit Where nEnterExitId=" & YournEnterExitId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Dim NSS As R2StandardCarTruckNobatStructure = New R2StandardCarTruckNobatStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                NSS.NSSDriverTruck = InstanceDriverTrucks.GetNSSDriverTruckbyDriverId(Ds.Tables(0).Rows(0).Item("nDriverCode"))
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                Return NSS
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetReserveTurn(YourTurnRegisterRequestId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                'ناوگان پیش فرض 
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim NSSTruck = InstanceTrucks.GetNSSDefaultTruck()
                'راننده ناوگان پیش فرض
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
                Dim NSSDriverTruck = InstanceTruckDrivers.GetNSSDefaultTruckDriver()
                'تسلسل نوبت پیش فرض
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
                Dim NSSSeqT = InstanceSequentialTurns.GetNSSSequentialTurn(SequentialTurns.None)
                'شاخص درخواست صدور نوبت
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
                Dim NSSTRR = InstanceTurnRegisterRequest.GetNSSTurnRegisterRequest(YourTurnRegisterRequestId)
                'تراکنش صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'شاخص اصلی نوبت برای تمام انواع ناوگان یکدست و یکپارچه محاسبه می شود و سیاست یکسانی دارد
                CmdSql.CommandText = "Select top 1 nEnterExitId from dbtransport.dbo.tbEnterExit with (tablockx) order by nEnterExitId desc"
                Dim mynIdEnterExit As Int64 = CmdSql.ExecuteScalar + 1

                Dim SequentialTurnId As Int64 = Int64.MinValue
                Dim SequentialTurnId_ As String = String.Empty
                CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from tbenterexit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & NSSSeqT.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() & "' order by OtaghdarTurnNumber desc"
                SequentialTurnId = CmdSql.ExecuteScalar + 1
                SequentialTurnId_ = NSSSeqT.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() + "/" + R2CoreMClassPublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim()
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim TurnRegisteringTimeStamp = InstanceTurns.GetTurnRegisteringTimeStampWithTurnType(YourTurnType)

                'ثبت نوبت ناوگان باری
                CmdSql.CommandText = "Insert Into dbtransport.dbo.TbEnterExit(nEnterExitId,StrCardNo,StrEnterDate,StrEnterTime,StrDesc,bEnterExit,nUserIdEnter,StrDriverName,bFlag,bFlagDriver,nDriverCode,BillOfLadingNumber,OtaghdarTurnNumber,TurnStatus,LoadPermissionStatus,RegisteringTimeStamp) Values(" & mynIdEnterExit & "," & NSSTruck.NSSCar.nIdCar & ",'" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime() & "','" & NSSTRR.Description & "',0," & YourUserNSS.UserId & ",'" & NSSDriverTruck.NSSDriver.StrPersonFullName & "',1,1," & NSSDriverTruck.NSSDriver.nIdPerson & ",'','" & SequentialTurnId_ & "'," & TurnStatuses.CancelledSystem & "," & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & ",'" & TurnRegisteringTimeStamp.DateTimeMilladiFormated & "')"
                CmdSql.ExecuteNonQuery()

                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Return mynIdEnterExit
            Catch ex As Exception When TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SetbFlagDriverToTrue(YournEnterExitId As Int64, DoSendtoTWSFlag As Boolean, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim InstanceCarTrucks = New PayanehClassLibraryMClassCarTrucksManager
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUser & ",bFlag=1,bFlagDriver=1,nUserIdExit=" & YourNSSSoftwareUser.UserId & " Where nEnterExitId=" & YournEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                If DoSendtoTWSFlag Then
                    Dim NSSCarTruck As R2StandardCarTruckStructure = InstanceCarTrucks.GetNSSCarTruckByCarId(InstanceTurns.GetNSSTruck(YournEnterExitId).NSSCar.nIdCar)
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSCarTruck.NSSCar.StrCarNo, NSSCarTruck.NSSCar.StrCarSerialNo)
                End If
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetAllofActiveTurns(YourNSSSequentialTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure) As List(Of PayanehClassLibraryTurnDetails)
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2ClassSqlConnectionSepas,
                     "Select Turns.nEnterExitId,Cars.strCarNo+'-'+Cars.strCarSerialNo as TruckLPString,Turns.strEnterDate,Turns.strEnterTime,Turns.strDriverName,OtaghdarTurnNumber,TurnStatuses.TurnStatusTitle 
                       from dbtransport.dbo.TbEnterExit as Turns
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On SUBSTRING(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS = SequentialTurns.SeqTKeyWord  Collate Arabic_CI_AI_WS
                          Inner Join dbtransport.dbo.tbCar AS Cars On Turns.strCardno=Cars.nIDCar 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On Turns.TurnStatus=TurnStatuses.TurnStatusId 
                       Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ")
                             and Substring(OtaghdarTurnNumber,1,1)='" & YourNSSSequentialTurn.SequentialTurnKeyWord & "' and SequentialTurns.Active=1 and SequentialTurns.Deleted=0 and Cars.ViewFlag=1
                       Order By nEnterExitId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetDataException
                End If
                Dim Lst = New List(Of PayanehClassLibraryTurnDetails)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim ST = New PayanehClassLibraryTurnDetails
                    ST.nEnterExitId = Ds.Tables(0).Rows(Loopx).Item("nEnterExitId")
                    ST.TruckLPString = Ds.Tables(0).Rows(Loopx).Item("TruckLPString").trim
                    ST.StrEnterDate = Ds.Tables(0).Rows(Loopx).Item("strEnterDate").trim
                    ST.StrEnterTime = Ds.Tables(0).Rows(Loopx).Item("strEnterTime").trim
                    ST.TruckDriver = Ds.Tables(0).Rows(Loopx).Item("strDriverName").trim
                    ST.OtaghdarSeqTurnNumber = Ds.Tables(0).Rows(Loopx).Item("OtaghdarTurnNumber").trim
                    ST.TurnStatusTitle = Ds.Tables(0).Rows(Loopx).Item("TurnStatusTitle").trim
                    Lst.Add(ST)
                Next
                Return Lst
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ResuscitationNonCreditTurn(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure, YourUserId As Int64)
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim mySoftWareUser = InstanceTurns.GetNSSSoftwareUser(YourNSSTurn)

                'کنترل فعال بودن و غیرفعال بودن سرویس
                Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
                If Not InstanceConfigurations.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 7) Then Throw New ResuscitationReserveTurnServiceIsUnactiveException

                'کنترل بومی و غیر بومی ناوگان
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                If Not InstanceTruckNativeness.IsTruckIndigenous(YourNSSTurn) Then Throw New NonIndigenousTrucksException

                'کنترل وضعیت نوبت
                If YourNSSTurn.TurnStatus <> TurnStatuses.CancelledUnderScore Then Throw New TurnHandlingNotAllowedBecuaseTurnStatusException

                'کنترل موجودی
                Dim InstanceTrafficCard = New R2CoreParkingSystemInstanceTrafficCardsManager
                Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
                Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
                Dim NSSTrafficCard = InstanceTrafficCard.GetNSSTrafficCard(InstanceCars.GetCardIdFromnIdCar(YourNSSTurn.StrCardNo))
                If Not InstanceTurnRegisterRequest.IsMoneyWalletInventoryIsEnoughForTurnRegistering(NSSTrafficCard) Then Throw New MoneyWalletCurrentChargeNotEnoughException

                'کنترل محدوده زمانی مجاز برای درخواست احیا نوبت
                If YourNSSTurn.LastChangedDate = _DateTime.GetCurrentDateShamsiFull Then Throw New ResuscitationReserveTurnEndTimeReachedException
                If YourNSSTurn.LastChangedDate <> _DateTime.GetCurrentDateShamsiFull Then
                    If _DateTime.GetCurrentTime > InstanceConfigurations.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 8) Then Throw New ResuscitationReserveTurnEndTimeReachedException
                End If

                'کنترل تاریخ تغییر وضعیت نوبت ، درخواست میتواند در همان روز و یا حداکثر تا روز کاری بعد ارسال گردد به سامانه
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Count(*) as Counting From R2PrimaryTransportationAndLoadNotification.Dbo.TblTransportationLoadNotificationSpecializedPersianCalendar AS SpecializedPersianCalendar
                            Where SpecializedPersianCalendar.DateShamsi > '" & YourNSSTurn.EnterDate & "' and SpecializedPersianCalendar.DateShamsi < '" & _DateTime.GetCurrentDateShamsiFull & "' and PCType=0 ", 3600, Ds, New Boolean)
                Dim CancellingDates = InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotification.ConfigurationsManagement.R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 5)
                If Ds.Tables(0).Rows(0).Item("Counting") <> CancellingDates Then Throw New ResuscitationReserveTurnEndDateReachedException

                'هزینه نوبت انجمن و شرکت
                Dim CostOfTurnRegistering As Int64 = PayanehClassLibraryMClassCarTruckNobatManagement.GetSherkatHazinehNobatMblgh(NSSTrafficCard)
                If CostOfTurnRegistering > 0 Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, CostOfTurnRegistering, R2CoreParkingSystemAccountings.SherkatHazinehNobat, R2CoreMClassSoftwareUsersManagement.GetNSSUser(YourUserId))
                If NSSTrafficCard.CardType = TerafficCardType.Tereili Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 1), R2CoreParkingSystemAccountings.AnjomanHazinehNobat, R2Core.SoftwareUserManagement.R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser)
                If NSSTrafficCard.CardType = TerafficCardType.SixCharkh Or NSSTrafficCard.CardType = TerafficCardType.TenCharkh Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4), R2CoreParkingSystemAccountings.AnjomanHazinehNobat, R2Core.SoftwareUserManagement.R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser)

                'احیاء نوبت
                PayanehClassLibraryMClassCarTruckNobatManagement.SetbFlagDriverToFalse(YourNSSTurn.nEnterExitId, True)
            Catch ex As NonIndigenousTrucksException
                Throw ex
            Catch ex As ResuscitationReserveTurnEndDateReachedException
                Throw ex
            Catch ex As ResuscitationReserveTurnEndTimeReachedException
                Throw ex
            Catch ex As ResuscitationReserveTurnServiceIsUnactiveException
                Throw ex
            Catch ex As MoneyWalletCurrentChargeNotEnoughException
                Throw ex
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Sub SendingSMSResuscitationNonCreditTurn(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourMessage As String)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourMessage}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, PayanehClassLibrarySMSTypes.ResuscitationNonCreditTurn, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                'If Not SMSResultAnalyze = String.Empty Then Throw New 
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ResuscitationNonCreditTurn(YourSofttWareUser As R2CoreStandardSoftwareUserStructure, YourUserId As Int64)
            Try
                Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager

                Dim NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = Nothing
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager()
                NSSTurn = InstanceTurns.GetNSSLastTurn(YourSofttWareUser)
                ResuscitationNonCreditTurn(NSSTurn, YourUserId)

                'ارسال پیام به راننده 
                Try
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, InstancePredefinedMessages.GetNSS(PayanehClassLibraryPredefinedMessages.ResuscitationNonCreditTurnSuccess).MsgContent)
                Catch ex As Exception
                End Try
            Catch ex As NonIndigenousTrucksException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.UnIndigenousTrucks).MsgContent)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As ResuscitationReserveTurnEndDateReachedException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As ResuscitationReserveTurnEndTimeReachedException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As ResuscitationReserveTurnServiceIsUnactiveException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, InstancePredefinedMessages.GetNSS(PayanehClassLibraryPredefinedMessages.ResuscitationNonCreditTurnServiceDisable).MsgContent)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As MoneyWalletCurrentChargeNotEnoughException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As TruckDriverNotFoundException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As TurnNotFoundException
                Try
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    SendingSMSResuscitationNonCreditTurn(YourSofttWareUser, ex.Message)
                Catch exSendingSMS As Exception
                End Try
            Catch ex As Exception
            End Try
        End Sub


    End Class

    Public Class PayanehClassLibraryMClassCarTruckNobatManagement

        Private Shared _DateTime As New DateAndTimeManagement.R2DateTime

        Private Shared Function GetReplicaTurnPrintRequestCost(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure)
            Try
                'هزینه چاپ قبض نوبت  - المثنی
                Dim ReplicaTurnPrintCost As Int64
                If YourNSSTerafficCard.CardType = TerafficCardType.Tereili Then ReplicaTurnPrintCost = R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 3)
                If YourNSSTerafficCard.CardType = TerafficCardType.SixCharkh Or YourNSSTerafficCard.CardType = TerafficCardType.TenCharkh Then ReplicaTurnPrintCost = R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4)
                Return ReplicaTurnPrintCost
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub ReplicaTurnPrintRequest(YourNSSCarTruckNobat As R2StandardCarTruckNobatStructure, YourTurnPrintRedirect As Boolean, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Try
                Dim NSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(R2CoreParkingSystemMClassCars.GetCardIdFromnIdCar(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YourNSSCarTruckNobat.nEnterExitId).NSSCar.nIdCar))
                'کنترل موجودی کیف پول به منظور کسر هزینه چاپ قبض نوبت  - المثنی
                Dim ReplicaTurnPrintCost = GetReplicaTurnPrintRequestCost(NSSTerafficCard)
                If R2CoreParkingSystemMClassMoneyWalletManagement.GetMoneyWalletCharge(NSSTerafficCard) < ReplicaTurnPrintCost Then
                    Throw New MoneyWalletCurrentChargeNotEnoughException
                End If
                R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTerafficCard, BagPayType.MinusMoney, ReplicaTurnPrintCost, R2CoreParkingSystemAccountings.PrintCopyOfTurn, YourUserNSS)
                R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManagement.ReplicaTurnPrintRequest(YourNSSCarTruckNobat.nEnterExitId, YourTurnPrintRedirect)
            Catch ex As Exception When TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException OrElse TypeOf ex Is GetNSSException OrElse TypeOf ex Is GetDataException OrElse TypeOf ex Is MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub NoCostTurnPrintRequest(YourNSSCarTruckNobat As R2StandardCarTruckNobatStructure, YourTurnPrintRedirect As Boolean)
            Try
                R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManagement.NoCostTurnPrintRequest(YourNSSCarTruckNobat.nEnterExitId, YourTurnPrintRedirect)
            Catch ex As Exception When TypeOf ex Is GetNSSException OrElse TypeOf ex Is GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetCarTravellength(YournIdCar As Int64) As Int64
            Try
                Dim Da As New SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 * from dbtransport.dbo.TbEnterExit as E 
                                                     Inner join TbCity as C on E.nCityCode=C.nCityCode 
                                                   Where E.StrCardNo=" & YournIdCar & " and ((E.TurnStatus=" & TurnStatuses.UsedLoadPermissionRegistered & ")  or (E.TurnStatus=" & TurnStatuses.CancelledLoadPermissionCancelled & "))
                                                   Order By nEnterExitId Desc")
                Da.SelectCommand.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then
                    Return 0
                Else
                    Dim MinuteTravelLenght As Int64 = (Ds.Tables(0).Rows(0).Item("nDistance") / 25) * 60
                    Dim DateTimeMilladiExit As DateTime = _DateTime.GetMilladiDateTimeFromDateShamsiFull(Ds.Tables(0).Rows(0).Item("StrExitDate"), Ds.Tables(0).Rows(0).Item("StrExitTime"))
                    Dim Diff As Int64 = R2CoreMClassPublicProcedures.GetDateDiff(DateInterval.Minute, DateTimeMilladiExit, _DateTime.GetCurrentDateTimeMilladiFormated())
                    Return (Diff - MinuteTravelLenght)
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCarTravellengthFormated(YournIdCar As Int64) As String
            Try
                Dim TLenght As Int64 = Math.Abs(GetCarTravellength(YournIdCar))
                Dim THours As Int64 = Int(TLenght / 60)
                Dim TMinutes As Int64 = TLenght - THours * 60
                Dim TString As String = String.Empty
                If THours <> 0 Then TString = THours.ToString() + " ساعت "
                If TMinutes <> 0 Then TString = TString + vbCrLf + TMinutes.ToString() + " دقیقه "
                Return TString
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsDriverHasNobat(YournIdPerson As Int64) As Boolean
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 nEnterExitId from dbtransport.dbo.TbEnterExit Where nDriverCode=" & YournIdPerson & " and bFlagDriver=0 Order By nEnterExitId desc")
                Da.SelectCommand.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSCarTruckNobat(YournEnterExitId As Int64) As R2StandardCarTruckNobatStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New DataBaseManagement.R2ClassSqlConnectionSepas, "Select Top 1 * from dbtransport.dbo.TbEnterExit Where nEnterExitId=" & YournEnterExitId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Dim NSS As R2StandardCarTruckNobatStructure = New R2StandardCarTruckNobatStructure
                NSS.nEnterExitId = Ds.Tables(0).Rows(0).Item("nEnterExitId")
                NSS.EnterDate = Ds.Tables(0).Rows(0).Item("StrEnterDate")
                NSS.EnterTime = Ds.Tables(0).Rows(0).Item("StrEnterTime")
                NSS.NSSDriverTruck = PayanehClassLibraryMClassDriverTrucksManagement.GetNSSDriverTruckbyDriverId(Ds.Tables(0).Rows(0).Item("nDriverCode"))
                NSS.bFlagDriver = Ds.Tables(0).Rows(0).Item("bFlagDriver")
                NSS.nUserIdEnter = Ds.Tables(0).Rows(0).Item("nUserIdEnter")
                NSS.BillOfLadingNumber = Ds.Tables(0).Rows(0).Item("BillOfLadingNumber")
                NSS.OtaghdarTurnNumber = Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber")
                NSS.StrCardNo = Ds.Tables(0).Rows(0).Item("StrCardNo")
                NSS.RegisteringTimeStamp = Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp")
                Return NSS
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetReserveTurn(YourTurnRegisterRequestId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                'ناوگان پیش فرض 
                Dim NSSTruck = R2CoreTransportationAndLoadNotificationMClassTrucksManagement.GetNSSDefaultTruck()
                'راننده ناوگان پیش فرض
                Dim NSSDriverTruck = R2CoreTransportationAndLoadNotificationMClassTruckDriversManagement.GetNSSDefaultTruckDriver()
                'تسلسل نوبت پیش فرض
                Dim NSSSeqT = R2CoreTransportationAndLoadNotificationMClassSequentialTurnsManagement.GetNSSSequentialTurn(SequentialTurns.None)
                'شاخص درخواست صدور نوبت
                Dim NSSTRR = R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManagement.GetNSSTurnRegisterRequest(YourTurnRegisterRequestId)
                'تراکنش صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'شاخص اصلی نوبت برای تمام انواع ناوگان یکدست و یکپارچه محاسبه می شود و سیاست یکسانی دارد
                CmdSql.CommandText = "Select top 1 nEnterExitId from dbtransport.dbo.tbEnterExit with (tablockx) order by nEnterExitId desc"
                Dim mynIdEnterExit As Int64 = CmdSql.ExecuteScalar + 1

                Dim SequentialTurnId As Int64 = Int64.MinValue
                Dim SequentialTurnId_ As String = String.Empty
                CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from tbenterexit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & NSSSeqT.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() & "' order by OtaghdarTurnNumber desc"
                SequentialTurnId = CmdSql.ExecuteScalar + 1
                SequentialTurnId_ = NSSSeqT.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() + "/" + R2CoreMClassPublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim()
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim TurnRegisteringTimeStamp = InstanceTurns.GetTurnRegisteringTimeStampWithTurnType(YourTurnType)

                'ثبت نوبت ناوگان باری
                CmdSql.CommandText = "Insert Into dbtransport.dbo.TbEnterExit(nEnterExitId,StrCardNo,StrEnterDate,StrEnterTime,StrDesc,bEnterExit,nUserIdEnter,StrDriverName,bFlag,bFlagDriver,nDriverCode,BillOfLadingNumber,OtaghdarTurnNumber,TurnStatus,LoadPermissionStatus,RegisteringTimeStamp) Values(" & mynIdEnterExit & "," & NSSTruck.NSSCar.nIdCar & ",'" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime() & "','" & NSSTRR.Description & "',0," & YourUserNSS.UserId & ",'" & NSSDriverTruck.NSSDriver.StrPersonFullName & "',1,1," & NSSDriverTruck.NSSDriver.nIdPerson & ",'','" & SequentialTurnId_ & "'," & TurnStatuses.CancelledSystem & "," & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & ",'" & TurnRegisteringTimeStamp.DateTimeMilladiFormated & "')"
                CmdSql.ExecuteNonQuery()

                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Return mynIdEnterExit
            Catch ex As Exception When TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback()
                    CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetHazinehSodoorNobat(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                If YourNSSTerafficCard.CardType = TerafficCardType.Tereili Then Return GetSherkatHazinehNobatMblgh(YourNSSTerafficCard) + R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 1)
                If YourNSSTerafficCard.CardType = TerafficCardType.SixCharkh Or YourNSSTerafficCard.CardType = TerafficCardType.TenCharkh Then Return GetSherkatHazinehNobatMblgh(YourNSSTerafficCard) + R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetSherkatHazinehNobatMblgh(ByVal YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                'این روتین فعلا فقط مخصوص تریلی است که اقدام به صدور نوبت در اتاق کامپیوترر می کند - محاسبه هزینه متفاوت از تردد نیست
                'در صورتی که ناوگان در محدوده 72 ساعت از صدور نوبت باشد(نه تردد) و مجددا نوبت بخواهد هزینه دارد - مثلا بار تهران می برد و قبل 72 برمیگردد نوبت بزند
                'مبلغ پایه هزینه صدور نوبت برای شرکت از کانفیگ
                Dim mySherkatHazinehNobat As Int64
                If YourTrafficCard.CardType = TerafficCardType.Tereili Then mySherkatHazinehNobat = R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 0)
                If YourTrafficCard.CardType = TerafficCardType.SixCharkh Then mySherkatHazinehNobat = R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 5)
                If YourTrafficCard.CardType = TerafficCardType.TenCharkh Then mySherkatHazinehNobat = R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 6)
                'احراز معیار محاسبه
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (MblghA<>0) and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.EnterType & " or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.SherkatHazinehNobat & " ) order by DateMilladiA desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then Return mySherkatHazinehNobat
                Dim Tavaghof As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTime.GetCurrentDateTimeMilladi())
                If YourTrafficCard.CardType = TerafficCardType.Tereili Then
                    If Tavaghof >= R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 2) Then
                        Return mySherkatHazinehNobat
                    Else
                        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTime.GetCurrentDateTimeMilladi())
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                ElseIf YourTrafficCard.CardType = TerafficCardType.SixCharkh Then
                    If Tavaghof >= R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 11) Then
                        Return mySherkatHazinehNobat
                    Else
                        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTime.GetCurrentDateTimeMilladi())
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                ElseIf YourTrafficCard.CardType = TerafficCardType.TenCharkh Then
                    If Tavaghof >= R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 12) Then
                        Return mySherkatHazinehNobat
                    Else
                        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & YourTrafficCard.CardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc", 1, Ds, New Boolean).GetRecordsCount <> 0 Then
                            Dim TavaghofLastNobat As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTime.GetCurrentDateTimeMilladi())
                            If TavaghofLastNobat <= Tavaghof Then
                                Return mySherkatHazinehNobat
                            Else
                                Return 0
                            End If
                        Else
                            Return 0
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function SetbFlagDriverToTrue(YournEnterExitId As Int64, DoSendtoTWSFlag As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YournEnterExitId)
                If NSSTurn.TurnStatus = TurnStatuses.Registered Or NSSTurn.TurnStatus = TurnStatuses.UsedLoadAllocationRegistered Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadAllocationCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationLoadPermissionCancelled Or NSSTurn.TurnStatus = TurnStatuses.ResuscitationUser Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUser & ",bFlag=1,bFlagDriver=1 Where nEnterExitId=" & YournEnterExitId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    If DoSendtoTWSFlag Then
                        Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YournEnterExitId).NSSCar.nIdCar)
                        TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSCarTruck.NSSCar.StrCarNo, NSSCarTruck.NSSCar.StrCarSerialNo)
                    End If
                Else
                    Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                End If
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub TempTurnsCancellation()
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                'آیا سرویس ابطال نوبت های موقت بر اساس کانفیگ فعال است یا نه
                If Not InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 3) Then Return
                Dim myCurrentDateShamsi = _DateTime.GetCurrentDateShamsiFull
                Dim TotalTurns As Int64
                Dim DS As DataSet
                TotalTurns = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select nEnterExitId from dbtransport.dbo.tbEnterExit
                      Where strEnterDate='" & myCurrentDateShamsi & "' and (TurnStatus=1 or TurnStatus=7 or TurnStatus=8 or TurnStatus=9 or TurnStatus=10) 
                            and DATEDIFF(MINUTE,RegisteringTimeStamp,GETDATE())>" & InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 4) & " and RegisteringTimeStamp<>'2015-01-01 00:00:00.000'", 0, DS, New Boolean).GetRecordsCount
                If TotalTurns <> 0 Then
                    CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit Set TurnStatus=" & TurnStatuses.CancelledSystem & ",bFlag=1,bFlagDriver=1,nUserIdExit=1
                                          Where strEnterDate='" & myCurrentDateShamsi & "' and (TurnStatus=1 or TurnStatus=7 or TurnStatus=8 or TurnStatus=9 or TurnStatus=10)
                                                and DATEDIFF(MINUTE,RegisteringTimeStamp,GETDATE())>" & InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnCancellationSetting, 4) & " and RegisteringTimeStamp<>'2015-01-01 00:00:00.000'"
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, PayanehClassLibraryLogType.TurnsCancellation, "کنسل کردن نوبت های موقت", "TotalTurns=" + TotalTurns.ToString, String.Empty, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
                End If
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function EmergencySetbFlagDriverToFalse(YournEnterExitId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YournEnterExitId)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationUser & ",bFlag=1,bFlagDriver=0 Where nEnterExitId=" & YournEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function SetbFlagDriverToFalse(YournEnterExitId As Int64, DoSendtoTWSFlag As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
                Dim NSSTurn = InstanceTurns.GetNSSTurn(YournEnterExitId)
                If NSSTurn.TurnStatus = TurnStatuses.UsedLoadPermissionRegistered Then
                    Throw New TurnHandlingNotAllowedBecuaseTurnStatusException
                End If
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.ResuscitationUser & ",bFlag=1,bFlagDriver=0 Where nEnterExitId=" & YournEnterExitId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                If DoSendtoTWSFlag Then
                    Dim NSSCarTruck As R2StandardCarTruckStructure = PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(R2CoreTransportationAndLoadNotificationMClassTurnsManagement.GetNSSTruck(YournEnterExitId).NSSCar.nIdCar)
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(NSSCarTruck.NSSCar.StrCarNo, NSSCarTruck.NSSCar.StrCarSerialNo)
                End If
            Catch ex As TurnHandlingNotAllowedBecuaseTurnStatusException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNobatDateShamsi(YournEnterExitId As Int64) As String
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 StrEnterDate,StrEnterTime From dbtransport.dbo.TbEnterExit Where nenterexitid=" & YournEnterExitId & "")
                Da.SelectCommand.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then
                    Throw New NobatNotExistException
                Else
                    Return Ds.Tables(0).Rows(0).Item("StrEnterDate").trim
                End If
            Catch exx As NobatNotExistException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNobatTime(YournEnterExitId As Int64) As String
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 StrEnterDate,StrEnterTime From dbtransport.dbo.TbEnterExit Where nenterexitid=" & YournEnterExitId & "")
                Da.SelectCommand.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then
                    Throw New NobatNotExistException
                Else
                    Return Ds.Tables(0).Rows(0).Item("StrEnterTime").trim
                End If
            Catch exx As NobatNotExistException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNobatDateMilladi(YournEnterExitId As Int64) As DateTime
            Try
                Return _DateTime.GetMilladiDateTimeFromDateShamsiFull(GetNobatDateShamsi(YournEnterExitId), GetNobatTime(YournEnterExitId))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNobatDifrenceDayToToday(YournEnterExitId As Int64) As Int64
            Try
                Return R2CoreMClassPublicProcedures.GetDateDiff(DateInterval.Day, GetNobatDateMilladi(YournEnterExitId), _DateTime.GetCurrentDateTimeMilladiFormated())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLastActiveNSSNobat(YourNSSCar As R2StandardCarStructure) As R2StandardCarTruckNobatStructure
            Try
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2ClassSqlConnectionSepas, "Select Top 1 * from dbtransport.dbo.TbEnterExit Where StrCardNo=" & YourNSSCar.nIdCar & " and (bFlagDriver=0) Order By nEnterExitId desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNobatException("ناوگان نوبت ندارد")
                End If
                Return New R2StandardCarTruckNobatStructure(Ds.Tables(0).Rows(0).Item("nEnterExitId"), Ds.Tables(0).Rows(0).Item("StrEnterDate"), Ds.Tables(0).Rows(0).Item("StrEnterTime"), PayanehClassLibraryMClassDriverTrucksManagement.GetNSSDriverTruckbyDriverId(Ds.Tables(0).Rows(0).Item("nDriverCode")), Ds.Tables(0).Rows(0).Item("bFlagDriver"), Ds.Tables(0).Rows(0).Item("nUserIdEnter"), Ds.Tables(0).Rows(0).Item("BillOfLadingNumber").trim, Ds.Tables(0).Rows(0).Item("OtaghdarTurnNumber").trim, Ds.Tables(0).Rows(0).Item("StrCardNo"), Ds.Tables(0).Rows(0).Item("RegisteringTimeStamp"))
            Catch exx As GetNobatException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsCarTruckTankTreiler(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Boolean
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from PayanehAmirKabir.dbo.TblTankTrailers Where Deleted=0 and OActive=1 and Pelak='" & YourNSSTruck.NSSCar.StrCarNo & "' and Serial='" & YourNSSTruck.NSSCar.StrCarSerialNo & "'", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub ChangeCarTruckTankTreilerStatus(YourNSS As R2StandardCarTruckStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                CmdSql.Connection.Open()
                If IsCarTruckTankTreiler(InstanceTrucks.GetNSSTruck(YourNSS.NSSCar.nIdCar)) Then
                    CmdSql.CommandText = "Update PayanehAmirKabir.dbo.TblTankTrailers Set OActive=0 Where ltrim(rtrim(Pelak))='" & YourNSS.NSSCar.StrCarNo & "' and ltrim(rtrim(Serial))= '" & YourNSS.NSSCar.StrCarSerialNo & "'"
                Else
                    CmdSql.CommandText = "Update PayanehAmirKabir.dbo.TblTankTrailers Set OActive=1 Where ltrim(rtrim(Pelak))='" & YourNSS.NSSCar.StrCarNo & "' and ltrim(rtrim(Serial))= '" & YourNSS.NSSCar.StrCarSerialNo & "'"
                End If
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Shared Sub AutomaticTurnRegistering()
        '    Try
        '        'ابتدا تولید رشته ساب کوری
        '        'گروه های اعلام بار
        '        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
        '        Dim Announcements = InstanceAnnouncements.GetAnnouncements()
        '        Dim SubQuery = String.Empty
        '        For LoopAnnouncements As Int16 = 0 To Announcements.Count - 1
        '            Dim AHSGsConfig = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnRegisteringSetting, Announcements(LoopAnnouncements).AHId, 0), "-")
        '            'زیرگروه های اعلام بار
        '            Dim AnnouncementsSubGroups = InstanceAnnouncements.GetAnnouncementsubGroups(Announcements(LoopAnnouncements).AHId)
        '            For LoopAnnouncementsSubGroups As Int16 = 0 To AnnouncementsSubGroups.Count - 1
        '                Dim LoopIndex = LoopAnnouncementsSubGroups
        '                Dim IsActiveAutomaticTurnRegistering As Boolean = Split(AHSGsConfig.Where(Function(x) AnnouncementsSubGroups(LoopIndex).AHSGId = Split(x, ":")(0))(0), ":")(2)
        '                If IsActiveAutomaticTurnRegistering Then
        '                    'Dim AHSGId = AnnouncementsSubGroups(LoopIndex).AHSGId
        '                    'Dim AHId = InstanceAnnouncements.GetNSSAnnouncementHallByAnnouncementsubGroup(AHSGId).AHId
        '                    'Dim AHSGIdLastAnnounceTime = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(AHId, AHSGId)
        '                    SubQuery = SubQuery + " or (AHSGId=" & AnnouncementsSubGroups(LoopIndex).AHSGId.ToString() & ")"
        '                End If
        '            Next
        '        Next

        '        'بدست آوردن لیست نوبت هایی که مجوز برایشان  صادر شده و باید نوبت برایشان صادر شود
        '        Dim Query = "Select Turns.strBarnameNo,Turns.nEnterExitId,Turns.strCardno,Loads.AHId,Loads.AHSGId,Loads.LoadStatus from dbtransport.dbo.tbEnterExit as Turns
        '                               Inner Join dbtransport.dbo.tbElam as Loads On Turns.nEstelamID=Loads.nEstelamID 
        '                         Where  Turns.strCardno not in (Select strCardno from dbtransport.dbo.tbEnterExit Where (TurnStatus=1 or TurnStatus=7 or TurnStatus=8 or TurnStatus=9 or TurnStatus=10) and strEnterDate='" & _DateTime.GetCurrentDateShamsiFull & "') and 
        '                                Turns.bDelAutomated=1 and Turns.TurnStatus = 6 And Turns.strExitDate ='" & _DateTime.GetCurrentDateShamsiFull & "' and (2=3" + SubQuery + ")" + " Order By Turns.nEnterExitId Asc"
        '        Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
        '        Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
        '        Dim InstanceTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
        '        Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
        '        Dim InstanceLogging = New R2CoreInstanceLoggingManager
        '        Dim CurrentTime = _DateTime.GetCurrentTime()
        '        Dim DsTurns As DataSet = Nothing
        '        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, Query, 0, DsTurns, New Boolean).GetRecordsCount <> 0 Then
        '            If InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).Active Then
        '                InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, PayanehClassLibraryLogType.AutomaticTurnRegistering, InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).LogTitle, "TotalLoadPermissions=" + DsTurns.Tables(0).Rows.Count.ToString, String.Empty, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladi(), Nothing))
        '            End If
        '            Dim TotalTurnsSuccessed As Int64 = 0
        '            For LoopTurns As Int64 = 0 To DsTurns.Tables(0).Rows.Count - 1
        '                Dim AHId As Int64 = DsTurns.Tables(0).Rows(LoopTurns).Item("AHId")
        '                Dim AHSGId As Int64 = DsTurns.Tables(0).Rows(LoopTurns).Item("AHSGId")
        '                Dim nIdCar As Int64 = DsTurns.Tables(0).Rows(LoopTurns).Item("strCardno")
        '                Dim nEnterExitId As Int64 = DsTurns.Tables(0).Rows(LoopTurns).Item("nEnterExitId")
        '                Dim LoadStatus As Int64 = DsTurns.Tables(0).Rows(LoopTurns).Item("LoadStatus")
        '                Try
        '                    'کنترل زمان اجرای فرآیند
        '                    Dim myCurrentDateTime = _DateTime.GetCurrentDateTime
        '                    Dim TimeOfDay = _DateTime.GetTickofTime(myCurrentDateTime)
        '                    If Convert.ToInt64((TimeOfDay.Ticks - TimeSpan.Parse("00:45:00").Ticks) \ TimeSpan.Parse("00:30:00").Ticks) Mod 2 <> 0 Then Exit Try

        '                    'If LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then
        '                    '    If InstanceTiming.IsTimingActive(AHId, AHSGId) Then
        '                    '        'If Convert.ToInt32(DsTurns.Tables(0).Rows(LoopTurns).Item("strBarnameNo")) <> R2CoreTransportationAndLoadNotificationLoadPermissionRegisteringLocation.TransportCompany Then
        '                    '        If InstanceTiming.GetTiming(AHId, AHSGId, CurrentTime) <> R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InAutomaticTurnRegistering Then
        '                    '            Continue For
        '                    '        End If
        '                    '        'End If
        '                    '    End If
        '                    'End If

        '                    'کنترل حضور ناوگان در پارکینگ - درصورتی که طبق کانفیگ باید حضورداشته باشد ولی حضور نداشته باشد آنگاه اکسپشن پرتاب می گردد
        '                    Dim NSSCarTruck = New R2CoreTransportationAndLoadNotificationStandardTruckStructure(New R2StandardCarStructure(nIdCar, Nothing, Nothing, Nothing, Nothing), Nothing)
        '                    Dim TurnId As Int64 = Int64.MinValue
        '                    Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
        '                    Dim NSSSeqT = InstanceSequentialTurns.GetNSSSequentialTurn(InstanceAnnouncements.GetNSSAnnouncementsubGroup(AHSGId))
        '                    Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(NSSSeqT, NSSCarTruck, True, True, TurnId, PayanehClassLibraryRequesters.AutomaticTurnRegistering, TurnType.Permanent, InstanceSoftwareUsers.GetNSSSystemUser(), False)
        '                    TotalTurnsSuccessed += 1
        '                Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
        '                        OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
        '                        OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
        '                    'If InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).Active Then
        '                    '    InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, PayanehClassLibraryLogType.AutomaticTurnRegistering, InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).LogTitle, "nIdCar=" + nIdCar.ToString(), ex.Message, "CurrentTurnId=" + nEnterExitId.ToString(), "AHId=" + AHId.ToString(), "AHSGId=" + AHSGId.ToString(), R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladi(), Nothing))
        '                    'End If
        '                End Try
        '            Next
        '            If InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).Active Then
        '                InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, PayanehClassLibraryLogType.AutomaticTurnRegistering, InstanceLogging.GetNSSLogType(PayanehClassLibraryLogType.AutomaticTurnRegistering).LogTitle, "TotalTurnsSuccessed=" + TotalTurnsSuccessed.ToString(), String.Empty, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladi(), Nothing))
        '            End If

        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub


    End Class

    'Public Class TurnRegisteringStrategy

    '    Protected _DateTime As New R2DateTime
    '    Protected Event DoStrategyRequestedEvent()
    '    Protected NSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure = Nothing
    '    Protected NSSTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure = Nothing
    '    Protected NSSSequentialTurn As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure = Nothing
    '    Protected NSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure = Nothing
    '    Protected NSSDriverTruck As R2CoreTransportationAndLoadNotificationStandardTruckDriverStructure = Nothing
    '    Protected NSSSoftwareUser As R2CoreStandardSoftwareUserStructure = Nothing
    '    Protected NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure = Nothing
    '    Protected TurnType As TurnType = TurnType.None
    '    Private CostOfTurnRegistering As Int64 = Int64.MinValue

    '    Public ReadOnly Property GetCostOfTurnRegistering As Int64
    '        Get
    '            Return CostOfTurnRegistering
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetNSSTurn() As R2CoreTransportationAndLoadNotificationStandardTurnStructure
    '        Get
    '            Return NSSTurn
    '        End Get
    '    End Property

    '    Public Sub New()
    '    End Sub

    '    Public Sub New(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourRequesterId As Int64, YourTurnType As TurnType, YourTWSForce As Boolean)
    '        Try
    '            NSSTrafficCard = YourNSSTrafficCard
    '            NSSTurnRegisteringRequest = YourNSSTurnRegisteringRequest
    '            NSSSoftwareUser = YourUserNSS
    '            TurnType = YourTurnType
    '            NSSSequentialTurn = YourNSSSeqT

    '            Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
    '            Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager
    '            'احراز کد ناوگان از رابطه کارت و پلاک
    '            Try
    '                NSSTruck = InstanceTrucks.GetNSSTruck(PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(R2CoreParkingSystemMClassCars.GetnIdCarFromCardId(YourNSSTrafficCard.CardId)).NSSCar.nIdCar)
    '            Catch ex As GetDataException
    '                Throw New GetNobatException("صدور نوبت امکان پذیر نیست" + vbCrLf + "مشخصات کارت تردد و پلاک ناوگان متصل نیستند")
    '            End Try

    '            'آیا تسلسل نوبت فعال است
    '            If Not NSSSequentialTurn.Active Then
    '                Throw New SequentialTurnIsNotActiveException
    '            End If

    '            'کنترل مجوز درخواست صدور نوبت توسط رکستر خاص بر اساس تسلسل نوبت
    '            Dim InstancePermissions = New R2CoreInstansePermissionsManager
    '            If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanSendRequestforTurnIssueBySeqT, YourRequesterId, NSSSequentialTurn.SequentialTurnId) Then Throw New RequesterNotAllowTurnIssueBySeqTException

    '            'کنترل مجوز درخواست صدور نوبت توسط رکستر خاص بر اساس نوع آخرین بار دریافت شده
    '            Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationInstanceLoadPermissionManager
    '            Dim NSSLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure = Nothing
    '            Try
    '                NSSLoad = InstanceLoadPermission.GetTruckLastLoadWhichPermissioned(NSSTruck)
    '                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanSendRequestforTurnIssueByLastLoadPermissioned, YourRequesterId, NSSLoad.AHSGId) Then Throw New RequesterNotAllowTurnIssueByLastLoadPermissionedException
    '            Catch ex As RequesterNotAllowTurnIssueByLastLoadPermissionedException
    '                Throw ex
    '            Catch ex As LoadCapacitorLoadNotFoundException
    '                Throw ex
    '            Catch ex As TruckHasNotAnyLoadPermissionException
    '            Catch ex As Exception
    '                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '            End Try

    '            'احراز راننده ناوگان از رابطه ناوگان راننده
    '            Try
    '                NSSDriverTruck = InstanceTruckDrivers.GetNSSTruckDriver(PayanehClassLibraryMClassDriverTrucksManagement.GetNSSDriverTruckbyDriverId(R2CoreParkingSystemMClassCars.GetnIdPersonFirst(NSSTruck.NSSCar.nIdCar)).NSSDriver.nIdPerson, True)
    '            Catch ex As DriverTruckInformationNotExistException
    '                Throw ex
    '            End Try

    '            'کنترل تانکر مخزندار
    '            If PayanehClassLibraryMClassCarTruckNobatManagement.IsCarTruckTankTreiler(NSSTruck) Then
    '                R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Warn, "ناوگان موردنظر در لیست تانکرهای مخزندار قرار دارد", NSSTrafficCard.CardNo, 0, 0, 0, 0, YourUserNSS.UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
    '                Throw New GetNobatExceptionCarTruckIsTankTreiler
    '            End If

    '            'کنترل دارابودن نوبت در آنلاین
    '            Dim TWSMessage As String = TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.ExistNobat(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo)
    '            If TWSMessage.Trim <> String.Empty Then
    '                If Not YourTWSForce Then
    '                    R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Fail, "صدور نوبت امکان پذیر نیست" + vbCrLf + TWSMessage, NSSTrafficCard.CardNo, 0, 0, 0, 0, YourUserNSS.UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
    '                    Throw New GetNobatException("صدور نوبت امکان پذیر نیست" + vbCrLf + TWSMessage)
    '                End If
    '            End If

    '            'کنتر دارا بودن نوبت فعال قبلی
    '            If R2CoreTransportationAndLoadNotificationMClassTurnsManagement.ExistActiveTurn(NSSTruck) Then
    '                R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Note, "ناوگان نوبت دارد", NSSTrafficCard.CardNo, 0, 0, 0, 0, YourUserNSS.UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
    '                Throw New GetNobatExceptionCarTruckHasNobat
    '            End If

    '            'کنترل اینکه راننده نوبت دیگر نداشته باشد
    '            If PayanehClassLibraryMClassCarTruckNobatManagement.IsDriverHasNobat(NSSDriverTruck.NSSDriver.nIdPerson) = True Then
    '                R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Fail, "راننده با ناوگان دیگر نوبت دارد", NSSTrafficCard.CardNo, 0, 0, 0, 0, YourUserNSS.UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
    '                Throw New GetNobatException("صدور نوبت امکان پذیر نیست" + vbCrLf + "راننده با ناوگان دیگر نوبت دارد")
    '            End If

    '            'کنترل طول سفر
    '            Try
    '                Dim TravelLength As Int64 = PayanehClassLibraryMClassCarTruckNobatManagement.GetCarTravellength(NSSTruck.NSSCar.nIdCar)
    '                'Dim NSSLOadCapacitor = LoadNotificationLoadPermissionManagement.GetCapacitorLoadLoadByCarTruckLastLoadPermissionByCarTruck(PayanehClassLibraryMClassCarTrucksManagement.GetNSSCarTruckByCarId(NSSTruck.NSSCar.nIdCar))
    '                If TravelLength < 0 Then
    '                    R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Fail, PayanehClassLibraryMClassCarTruckNobatManagement.GetCarTravellengthFormated(NSSTruck.NSSCar.nIdCar) + vbCrLf + "طول سفر ناوگان به پایان نرسیده است", NSSTrafficCard.CardNo, 0, 0, 0, 0, YourUserNSS.UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
    '                    Throw New CarTruckTravelLengthNotOverYetException(PayanehClassLibraryMClassCarTruckNobatManagement.GetCarTravellengthFormated(NSSTruck.NSSCar.nIdCar))
    '                End If
    '            Catch ex As CarTruckHasNotAnyLoadPermissionException
    '            End Try

    '        Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
    '                            OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
    '                            OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
    '                            OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
    '                            OrElse TypeOf ex Is CarIsNotPresentInParkingException _
    '                            OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
    '                            OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
    '                            OrElse TypeOf ex Is GetNSSException _
    '                            OrElse TypeOf ex Is GetDataException _
    '                            OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
    '                            OrElse TypeOf ex Is GetNobatException _
    '                            OrElse TypeOf ex Is TruckNotFoundException _
    '                            OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
    '                            OrElse TypeOf ex Is TruckDriverNotFoundException _
    '                            OrElse TypeOf ex Is DriverTruckInformationNotExistException _
    '                            OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
    '            Throw ex
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    '    Public Sub DoStrategy()
    '        Try
    '            RaiseEvent DoStrategyRequestedEvent()
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    '    Public Sub DoStrategyComplementaryWorks()
    '        Try
    '            'هزینه نوبت انجمن و شرکت
    '            CostOfTurnRegistering = PayanehClassLibraryMClassCarTruckNobatManagement.GetSherkatHazinehNobatMblgh(NSSTrafficCard)
    '            If CostOfTurnRegistering > 0 Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, CostOfTurnRegistering, R2CoreParkingSystemAccountings.SherkatHazinehNobat, NSSSoftwareUser)
    '            If NSSTrafficCard.CardType = TerafficCardType.Tereili Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 1), R2CoreParkingSystemAccountings.AnjomanHazinehNobat, NSSSoftwareUser)
    '            If NSSTrafficCard.CardType = TerafficCardType.SixCharkh Or NSSTrafficCard.CardType = TerafficCardType.TenCharkh Then R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(NSSTrafficCard, BagPayType.MinusMoney, R2CoreMClassConfigurationManagement.GetConfigInt64(PayanehClassLibraryConfigurations.TariffsPayaneh, 4), R2CoreParkingSystemAccountings.AnjomanHazinehNobat, NSSSoftwareUser)

    '            'ارسال نوبت ناوگان برای سیستم آنلاین
    '            If NSSTrafficCard.CardType = TerafficCardType.Tereili And R2CoreMClassConfigurationManagement.GetConfigBoolean(PayanehClassLibraryConfigurations.TWS, 0) Then
    '                TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.AddNobat(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo)
    '            End If
    '            If NSSTrafficCard.CardType = TerafficCardType.SixCharkh Or NSSTrafficCard.CardType = TerafficCardType.TenCharkh And R2CoreMClassConfigurationManagement.GetConfigBoolean(PayanehClassLibraryConfigurations.TWS, 1) Then
    '                TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.AddNobat(NSSTruck.NSSCar.StrCarNo, NSSTruck.NSSCar.StrCarSerialNo)
    '            End If

    '            'ارسال اس ام اس نوبت
    '            Try
    '                Dim InstanceSoftwareUsers = New R2CoreParkingSystemInstanceSoftwareUsersManager
    '                SendingSMSTurn(InstanceSoftwareUsers.GetNSSSoftwareUser(NSSTruck.NSSCar))
    '            Catch ex As Exception
    '            End Try

    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub SendingSMSTurn(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
    '        Try
    '            Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
    '            Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
    '            Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = NSSTurn.EnterDate + " " + NSSTurn.EnterTime, .Data2 = NSSTurn.OtaghdarTurnNumber}}
    '            Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreTransportationAndLoadNotificationSMSTypes.SendingTurnNumberSMS, LstCreationData, True)
    '            Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
    '            'If Not SMSResultAnalyze = String.Empty Then Throw New 
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    'End Class

    'Public Class RealTimeTurnRegisteringStrategy
    '    Inherits TurnRegisteringStrategy

    '    Public Sub New(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourRequesterId As Int64, YourTurnType As TurnType, YourTWSForce As Boolean)
    '        MyBase.New(YourNSSSeqT, YourNSSTrafficCard, YourNSSTurnRegisteringRequest, YourUserNSS, YourRequesterId, YourTurnType, YourTWSForce)
    '    End Sub

    '    Private Sub DoStrategyRequestedEvent_Handler() Handles MyClass.DoStrategyRequestedEvent
    '        Dim CmdSql As SqlCommand = New SqlCommand
    '        CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
    '        Try
    '            Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
    '            'تراکنش صدور نوبت
    '            CmdSql.Connection.Open()
    '            CmdSql.Transaction = CmdSql.Connection.BeginTransaction
    '            'شاخص اصلی نوبت برای تمام انواع ناوگان یکدست و یکپارچه محاسبه می شود و سیاست یکسانی دارد
    '            CmdSql.CommandText = "Select top 1 nEnterExitId from dbtransport.dbo.tbEnterExit with (tablockx) order by nEnterExitId desc"
    '            Dim mynIdEnterExit As Int64 = CmdSql.ExecuteScalar + 1

    '            Dim SequentialTurnId As Int64 = Int64.MinValue
    '            Dim SequentialTurnId_ As String = String.Empty
    '            CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from dbtransport.dbo.tbenterexit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & NSSSequentialTurn.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() & "' order by OtaghdarTurnNumber desc"
    '            Dim TurnIdTemp As String = CmdSql.ExecuteScalar
    '            If TurnIdTemp = String.Empty Then
    '                SequentialTurnId = 1
    '            Else
    '                SequentialTurnId = Split(TurnIdTemp, ":")(0) + 1
    '            End If
    '            SequentialTurnId_ = NSSSequentialTurn.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() + "/" + R2CoreMClassPublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim()
    '            Dim TurnRegisteringTimeStamp = InstanceTurns.GetTurnRegisteringTimeStampWithTurnType(TurnType)
    '            'ثبت نوبت ناوگان باری
    '            CmdSql.CommandText = "Insert Into dbtransport.dbo.TbEnterExit(nEnterExitId,StrCardNo,StrEnterDate,StrEnterTime,StrDesc,bEnterExit,nUserIdEnter,StrDriverName,bFlag,bFlagDriver,nDriverCode,BillOfLadingNumber,OtaghdarTurnNumber,TurnStatus,LoadPermissionStatus,RegisteringTimeStamp) Values(" & mynIdEnterExit & "," & NSSTruck.NSSCar.nIdCar & ",'" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime() & "','" & NSSTurnRegisteringRequest.Description & "',0," & NSSSoftwareUser.UserId & ",'" & NSSDriverTruck.NSSDriver.StrPersonFullName & "',0,0," & NSSDriverTruck.NSSDriver.nIdPerson & ",'','" & SequentialTurnId_ & "'," & TurnStatuses.Registered & "," & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & ",'" & TurnRegisteringTimeStamp.DateTimeMilladiFormated & "')"
    '            CmdSql.ExecuteNonQuery()

    '            'bDelAutomated Used in AutomatedTurnRegistering for Control
    '            CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit Set bDelAutomated=0 Where bDelAutomated=1 and strCardno=" & NSSTruck.NSSCar.nIdCar & ""
    '            CmdSql.ExecuteNonQuery()

    '            CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
    '            NSSTurn = InstanceTurns.GetNSSTurn(mynIdEnterExit)
    '            DoStrategyComplementaryWorks()
    '        Catch ex As Exception When TypeOf ex Is TurnNotFoundException OrElse TypeOf ex Is ReserveTurnAlreadyUsedException
    '            If CmdSql.Connection.State <> ConnectionState.Closed Then
    '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
    '            End If
    '            Throw ex
    '        Catch ex As Exception
    '            If CmdSql.Connection.State <> ConnectionState.Closed Then
    '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
    '            End If
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    'End Class

    'Public Class ReserveTurnRegisteringStrategy
    '    Inherits TurnRegisteringStrategy

    '    Public Sub New(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSTurnRegisteringRequest As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourRequesterId As Int64, YourTurnType As TurnType, YourTWSForce As Boolean)
    '        MyBase.New(YourNSSSeqT, YourNSSTrafficCard, YourNSSTurnRegisteringRequest, YourUserNSS, YourRequesterId, YourTurnType, YourTWSForce)
    '    End Sub

    '    Private Sub DoStrategyRequestedEvent_Handler() Handles MyClass.DoStrategyRequestedEvent
    '        Dim CmdSql As SqlCommand = New SqlCommand
    '        CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
    '        Try
    '            Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationInstanceTurnsManager
    '            NSSTurn = InstanceTurns.GetNSSTurn(NSSTurnRegisteringRequest)
    '            If NSSTurn.TurnStatus <> TurnStatuses.CancelledSystem Then Throw New ReserveTurnAlreadyUsedException
    '            'تراکنش صدور نوبت
    '            CmdSql.Connection.Open()
    '            CmdSql.Transaction = CmdSql.Connection.BeginTransaction
    '            'محاسبه تسلسل نوبت
    '            Dim SequentialTurnId As Int64 = Int64.MinValue
    '            Dim SequentialTurnId_ As String = String.Empty
    '            CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from dbtransport.dbo.tbEnterExit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & NSSSequentialTurn.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() & "' and nEnterExitId<" & NSSTurn.nEnterExitId & " order by OtaghdarTurnNumber desc"
    '            Dim TurnIdTemp As String = CmdSql.ExecuteScalar
    '            If TurnIdTemp = String.Empty Then Throw New ResuscitationReserveTurnFailedException
    '            SequentialTurnId = Split(TurnIdTemp, ":")(0)
    '            SequentialTurnId_ = NSSSequentialTurn.SequentialTurnKeyWord.Trim + _DateTime.GetCurrentSalShamsiFull() + "/" + R2CoreMClassPublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim() + ":" + NSSTurn.nEnterExitId.ToString
    '            Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
    '            Dim TurnRegisteringTimeStamp = IIf(TurnType = TurnType.Permanent, InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 4), _DateTime.GetCurrentDateTimeMilladiFormated)
    '            'احیاء نوبت رزرو
    '            CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit 
    '                                  Set strCardno=" & NSSTruck.NSSCar.nIdCar & ",strDriverName='" & NSSDriverTruck.NSSDriver.StrPersonFullName & "',strDesc=strDesc+' - " & NSSSoftwareUser.UserName & "',bFlag=0,bFlagDriver=0,nDriverCode=" & NSSDriverTruck.NSSDriver.nIdPerson & ",OtaghdarTurnNumber='" & SequentialTurnId_ & "',TurnStatus=" & TurnStatuses.Registered & ",RegisteringTimeStamp='" & TurnRegisteringTimeStamp & "'
    '                                  Where nEnterExitId = " & NSSTurn.nEnterExitId & ""
    '            CmdSql.ExecuteNonQuery()
    '            'bDelAutomated Used in AutomatedTurnRegistering for Control
    '            CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit Set bDelAutomated=0 Where bDelAutomated=1 and strCardno=" & NSSTruck.NSSCar.nIdCar & ""
    '            CmdSql.ExecuteNonQuery()

    '            CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
    '            NSSTurn = InstanceTurns.GetNSSTurn(NSSTurn.nEnterExitId)
    '            DoStrategyComplementaryWorks()
    '        Catch ex As Exception When TypeOf ex Is TurnNotFoundException OrElse TypeOf ex Is ReserveTurnAlreadyUsedException OrElse TypeOf ex Is ResuscitationReserveTurnFailedException
    '            If CmdSql.Connection.State <> ConnectionState.Closed Then
    '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
    '            End If
    '            Throw ex
    '        Catch ex As Exception
    '            If CmdSql.Connection.State <> ConnectionState.Closed Then
    '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
    '            End If
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Sub

    'End Class

    Namespace Exceptions
        Public Class ResuscitationReserveTurnEndDateReachedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "محدوده احیا نوبت زیر اعتبار به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class ResuscitationReserveTurnEndTimeReachedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان احیا نوبت زیر اعتبار به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class ResuscitationReserveTurnFailedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان احیاء نوبت رزرو وجود ندارد"
                End Get
            End Property
        End Class

        Public Class ResuscitationReserveTurnServiceIsUnactiveException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سرویس احیا نوبت زیر اعتبار غیر فعال است"
                End Get
            End Property
        End Class

        Public Class GetNobatException
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

        Public Class NobatNotExistException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره نوبت مورد نظر در سیستم ثبت نشده است"
                End Get
            End Property
        End Class

        Public Class GetNobatExceptionCarTruckIsTankTreiler
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ناوگان مورد نظر در لیست تانکرهای مخزندار قرار دارد" + vbCrLf + "امکان صدور نوبت وجود ندارد"
                End Get
            End Property
        End Class

        Public Class GetNobatExceptionCarTruckHasNobat
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ناوگان نوبت دارد"
                End Get
            End Property
        End Class

        Public Class ViewCarTruckTurnsTerraficCardNotSupportException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع کارت تردد مطابقت ندارد"
                End Get
            End Property
        End Class

        Public Class CarTruckTravelLengthNotOverYetException
            Inherits ApplicationException
            Private _ConsumerMessage As String = String.Empty

            Public Sub New(YourMessage As String)
                _ConsumerMessage = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "طول سفر ناوگان باری به پایان نرسیده است" + vbCrLf + _ConsumerMessage
                End Get
            End Property

            Public ReadOnly Property WithoutConsumerMessage As String
                Get
                    Return "طول سفر ناوگان باری به پایان نرسیده است"
                End Get
            End Property

        End Class

        Public Class ReserveTurnAlreadyUsedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوبت رزرو مرتبط با شماره درخواست قبلا احیاء شده است"
                End Get
            End Property
        End Class

        Public Class LastTurnIdWhichCancelledDuringTurnsCancellationProcessNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "آخرین شماره اعتبار،باطل شده طی فرآیند ابطال نوبت ها ، وجود ندارد "
                End Get
            End Property
        End Class


        'BPTChanged
        Public Class CarTruckTravelTimeNotOverYetException
            Inherits ApplicationException
            Private _ConsumerMessage As String = String.Empty

            Public Sub New(YourMessage As String)
                _ConsumerMessage = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "طول سفر ناوگان باری به پایان نرسیده است" + vbCrLf + _ConsumerMessage
                End Get
            End Property

            Public ReadOnly Property WithoutConsumerMessage As String
                Get
                    Return "طول سفر ناوگان باری به پایان نرسیده است"
                End Get
            End Property

        End Class

    End Namespace


End Namespace

'BPTChanged
Namespace Turns

    Public Class PayanehClassLibraryTurnManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _DateTimeService = YourR2DateTimeService
        End Sub

        Public Function GetTurnofKiosk(YourSeqTId As Int64, YourTruckId As Int64, YourTruckDriverId As Int64, YourRequesterId As Int64, YourSoftwareUser As R2CoreSoftwareUser) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)
                Dim MoneyWallet = InstanceMoneyWallet.GetMoneyWallet(YourSoftwareUser)
                'تراکنش ثبت روابط موقت ناوگان و راننده باری و کیف پول و زیرگروه اعلام بار
                'کلیه روابط به صورت موقت ایجاد می گردند و از طریق فیلد تایم استمپ در کل سیستم ایزوله می شوند و شناسایی می گردند
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Insert into dbtransport.dbo.TbCarAndPerson(nIDCar,nIDPerson,snRelation,dDate,strDesc,RelationTimeStamp)
                  				      Values(" & YourTruckId & "," & YourTruckDriverId & ",2,R2Primary.dbo.BPTCOGregorianToPersian(GETDATE()),'TempRelation',convert(varchar, getdate(), 20))"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars(CardId, nCarId, RelationActive, RelationTimeStamp) 
                  				      Values(" & MoneyWallet.MoneyWalletId & "," & YourTruckId & ",1,convert(varchar, getdate(), 20))"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Dim TurnId = Int64.MinValue
                Dim InstanceTurnRegisterRequest = New PayanehClassLibraryTurnRegisterRequestManager(_DateTimeService)
                Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(YourSeqTId, YourTruckId, TurnId, YourRequesterId, TurnType.Temporary, YourSoftwareUser.UserId)
                Return TurnId
            Catch ex As SoftwareUserMoneyWalletNotFoundException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                                OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                                OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                                OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                                OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                                OrElse TypeOf ex Is GetNobatException _
                                OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException _
                                OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                                OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                                OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                                OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                                OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
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

    Public Class TurnRegisteringStrategy

        Protected _R2DateTimeService As IR2DateTimeService
        Protected Event DoStrategyRequestedEvent()
        Protected MoneyWallet As R2CoreMoneyWallet
        Protected TurnRegisteringRequest As R2CoreTransportationAndLoadNotificationTurnRegisterRequest
        Protected SequentialTurn As R2CoreTransportationAndLoadNotificationSequentialTurn
        Protected TurnSeqT As String
        Protected TurnId As Int64
        Protected Truck As R2CoreTransportationAndLoadNotificationTruck
        Protected TruckDriver As R2CoreTransportationAndLoadNotificationTruckDriver
        Protected SoftwareUser As R2CoreSoftwareUser
        Protected TurnType As TurnType = TurnType.None
        Protected TurnCost As R2CoreTransportationAndLoadNotificationTurnCost

        Public ReadOnly Property GetTurnCost As Int64
            Get
                Return TurnCost.SelfGoverCost + TurnCost.TruckerAssociationCost
            End Get
        End Property

        Public ReadOnly Property GetTurnId As Int64
            Get
                Return TurnId
            End Get
        End Property

        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Sub New(YourR2DateTimeService As IR2DateTimeService, YourSequentialTurnId As Int64, YourTruckId As Int64, YourTurnRegisteringRequestId As Int64, YourSoftwareUserId As Int64, YourRequesterId As Int64, YourTurnType As TurnType)
            Try
                _R2DateTimeService = YourR2DateTimeService
                TurnType = YourTurnType

                Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationLoadPermissionManager(_R2DateTimeService)
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_R2DateTimeService)
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationTruckDriversManager
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_R2DateTimeService)
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)
                Dim InstanceTurnRegister = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_R2DateTimeService, Nothing)

                'کاربر درخواست دهنده
                SoftwareUser = InstanceSoftwareUsers.GetUser(YourSoftwareUserId, False)

                'احراز راننده ناوگان
                TruckDriver = InstanceTruckDrivers.GetTruckDriverfromTruckId(YourTruckId, True)

                'احراز ناوگان
                Truck = InstanceTrucks.GetTruck(YourTruckId, True)

                'احراز کیف پول ناوگان
                MoneyWallet = InstanceMoneyWallet.GetMoneyWalletfromCarId(YourTruckId, True)

                'آیا تسلسل نوبت فعال است
                SequentialTurn = InstanceSequentialTurns.GetSequentialTurn(YourSequentialTurnId, True)
                If Not SequentialTurn.Active Then
                    Throw New SequentialTurnIsNotActiveException
                End If

                'احراز درخواست
                TurnRegisteringRequest = InstanceTurnRegister.GetTurnRegisterRequest(YourTurnRegisteringRequestId, True)

                'کنترل مجوز درخواست صدور نوبت توسط رکستر خاص بر اساس تسلسل نوبت
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanSendRequestforTurnIssueBySeqT, YourRequesterId, YourSequentialTurnId) Then Throw New RequesterNotAllowTurnIssueBySeqTException

                'کنترل مجوز درخواست صدور نوبت توسط رکستر خاص بر اساس نوع آخرین بار دریافت شده
                Try
                    Dim Load = InstanceLoadPermission.GetTruckLastLoadWhichPermissioned(YourTruckId, True)
                    If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanSendRequestforTurnIssueByLastLoadPermissioned, YourRequesterId, Load.AnnouncementSubGroupId) Then Throw New RequesterNotAllowTurnIssueByLastLoadPermissionedException
                Catch ex As TruckHasNotAnyLoadPermissionException
                Catch ex As Exception
                    Throw New Exception("CheckForTruckLastLoadWhichPermissioned:" + ex.Message)
                End Try

                'کنترل دارابودن نوبت در نوبت آنلاین
                If InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TWS, 0) Then
                    'Dim TWSMessage As String = TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.ExistNobat(Truck.Pelak, Truck.Serial)
                    'If TWSMessage.Trim <> String.Empty Then
                    '    Throw New GetNobatException("صدور نوبت امکان پذیر نیست" + vbCrLf + TWSMessage)
                    'End If
                End If

                'کنتر دارا بودن نوبت فعال قبلی
                If InstanceTurns.ExistActiveTurn(YourTruckId, True) Then Throw New GetNobatExceptionCarTruckHasNobat

                'کنترل اینکه راننده نوبت دیگر نداشته باشد
                If InstanceTurns.HasTruckDriverTurn(TruckDriver.DriverId, True) = True Then Throw New GetNobatException("صدور نوبت امکان پذیر نیست" + vbCrLf + "راننده با ناوگان دیگر نوبت دارد")

                'کنترل طول سفر
                Try
                    Dim TravelTime As Int64 = InstanceTurns.GetPossibleTruckTravelTime(YourTruckId)
                    If TravelTime < 0 Then Throw New CarTruckTravelTimeNotOverYetException(InstanceTurns.GetTruckTravelTimeFormated(YourTruckId))
                Catch ex As CarTruckHasNotAnyLoadPermissionException
                End Try
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                                OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                                OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                                OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                                OrElse TypeOf ex Is GetNobatException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                                OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DoStrategy()
            Try
                RaiseEvent DoStrategyRequestedEvent()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DoStrategyComplementaryWorks()
            Try
                Dim InstanceLoadPermission = New R2CoreTransportationAndLoadNotificationLoadPermissionManager(_R2DateTimeService)
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_R2DateTimeService)
                Dim InstanceTruckDrivers = New R2CoreTransportationAndLoadNotificationTruckDriversManager
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_R2DateTimeService)
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(New R2DateTimeService)

                'هزینه نوبت انجمن و شرکت
                TurnCost = InstanceTurns.GetTurnCost(SequentialTurn.SeqTurnId, False)
                If TurnCost.SelfGoverCost > 0 Then InstanceMoneyWallet.ActMoneyWalletNextStatus(MoneyWallet.MoneyWalletId, BagPayType.MinusMoney, TurnCost.SelfGoverCost, R2CoreParkingSystemAccountings.SherkatHazinehNobat, SoftwareUser.UserId)
                If TurnCost.TruckerAssociationCost > 0 Then InstanceMoneyWallet.ActMoneyWalletNextStatus(MoneyWallet.MoneyWalletId, BagPayType.MinusMoney, TurnCost.TruckerAssociationCost, R2CoreParkingSystemAccountings.AnjomanHazinehNobat, SoftwareUser.UserId)

                'ارسال نوبت ناوگان برای سیستم آنلاین
                If InstanceConfiguration.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TWS, 0) Then
                    TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.AddNobat(Truck.Pelak, Truck.Serial)
                End If

                'ارسال اس ام اس نوبت
                Try
                    Dim InstanceSoftwareUsers = New R2CoreParkingSystemInstanceSoftwareUsersManager
                    SendingSMSTurn(InstanceSoftwareUsers.GetNSSSoftwareUser(TruckDriver.DriverId))
                Catch ex As Exception
                End Try

            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Sub SendingSMSTurn(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull + " " + _R2DateTimeService.DateTimeServ.GetCurrentTime, .Data2 = TurnSeqT}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreTransportationAndLoadNotificationSMSTypes.SendingTurnNumberSMS, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                'If Not SMSResultAnalyze = String.Empty Then Throw New 
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class RealTimeTurnRegisteringStrategy
        Inherits TurnRegisteringStrategy

        Public Sub New(YourSeqTId As Int64, YourTruckId As Int64, YourTurnRegisteringRequestId As Int64, YourSoftwareUserId As Int64, YourRequesterId As Int64, YourTurnType As TurnType)
            MyBase.New(New R2DateTimeService, YourSeqTId, YourTruckId, YourTurnRegisteringRequestId, YourSoftwareUserId, YourRequesterId, YourTurnType)
        End Sub

        Private Sub DoStrategyRequestedEvent_Handler() Handles MyClass.DoStrategyRequestedEvent
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceTurnAccounting = New R2CoreTransportationAndLoadNotificationTurnAccountingManager(New R2DateTimeService)

                'تراکنش صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'شاخص اصلی نوبت برای تمام انواع بارگیرها یکدست و یکپارچه محاسبه می شود و سیاست یکسانی دارد
                CmdSql.CommandText = "Select top 1 nEnterExitId from dbtransport.dbo.tbEnterExit with (tablockx) order by nEnterExitId desc"
                Dim mynIdEnterExit As Int64 = CmdSql.ExecuteScalar + 1
                TurnId = mynIdEnterExit

                Dim SequentialTurnId As Int64 = Int64.MinValue
                Dim SequentialTurnId_ As String = String.Empty
                CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from dbtransport.dbo.tbenterexit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() & "' order by OtaghdarTurnNumber desc"
                Dim TurnIdTemp As String = CmdSql.ExecuteScalar
                If TurnIdTemp = String.Empty Then
                    SequentialTurnId = 1
                Else
                    SequentialTurnId = Split(TurnIdTemp, ":")(0) + 1
                End If
                SequentialTurnId_ = SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() + "/" + InstancePublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim()
                TurnSeqT = SequentialTurnId_
                Dim TurnRegisteringTimeStamp = InstanceTurns.GetTurnRegisteringTimeStampWithTurnType(TurnType)
                'ثبت نوبت ناوگان باری
                CmdSql.CommandText = "Insert Into dbtransport.dbo.TbEnterExit(nEnterExitId,StrCardNo,StrEnterDate,StrEnterTime,StrDesc,bEnterExit,nUserIdEnter,nUserIdExit,StrDriverName,bFlag,bFlagDriver,strElamDate,strElamTime,nDriverCode,BillOfLadingNumber,OtaghdarTurnNumber,TurnStatus,LoadPermissionStatus,RegisteringTimeStamp) 
                                      Values(" & mynIdEnterExit & "," & Truck.TruckId & ",'" & _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull() & "','" & _R2DateTimeService.DateTimeServ.GetCurrentTime() & "','" & TurnRegisteringRequest.Description & "',0," & SoftwareUser.UserId & "," & SoftwareUser.UserId & ",'" & TruckDriver.NameFamily & "',0,0,'" & String.Empty & "','" & String.Empty & "'," & TruckDriver.DriverId & ",'','" & SequentialTurnId_ & "'," & TurnStatuses.Registered & "," & R2CoreTransportationAndLoadNotificationLoadPermissionStatuses.None & ",'" & TurnRegisteringTimeStamp.DateTimeMilladiFormated & "')"
                CmdSql.ExecuteNonQuery()
                'اکانتینگ نوبت
                InstanceTurnAccounting.TurnAccountingRegistering(mynIdEnterExit, TurnAccounting.TurnAccouningTypes.TurnIssue, SoftwareUser.UserId, CmdSql)

                'bDelAutomated Used in AutomatedTurnRegistering for Control
                CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit Set bDelAutomated=0 Where bDelAutomated=1 and strCardno=" & Truck.TruckId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                DoStrategyComplementaryWorks()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception When TypeOf ex Is TurnNotFoundException OrElse TypeOf ex Is ReserveTurnAlreadyUsedException
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

    End Class

    Public Class ReserveTurnRegisteringStrategy
        Inherits TurnRegisteringStrategy

        Public Sub New(YourSeqTId As Int64, YourTruckId As Int64, YourTurnRegisteringRequestId As Int64, YourSoftwareUserId As Int64, YourRequesterId As Int64, YourTurnType As TurnType)
            MyBase.New(New R2DateTimeService, YourSeqTId, YourTruckId, YourTurnRegisteringRequestId, YourSoftwareUserId, YourRequesterId, YourTurnType)
        End Sub

        Private Sub DoStrategyRequestedEvent_Handler() Handles MyClass.DoStrategyRequestedEvent
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceTurnAccounting = New R2CoreTransportationAndLoadNotificationTurnAccountingManager(New R2DateTimeService)

                Dim Turn = InstanceTurns.GetTurn(TurnRegisteringRequest, True)
                TurnId = Turn.TurnId
                If Turn.TurnStatusId <> TurnStatuses.CancelledSystem Then Throw New ReserveTurnAlreadyUsedException
                'تراکنش صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'محاسبه تسلسل نوبت
                Dim SequentialTurnId As Int64 = Int64.MinValue
                Dim SequentialTurnId_ As String = String.Empty
                CmdSql.CommandText = "Select top 1 Substring(OtaghdarTurnNumber,7,20) from dbtransport.dbo.tbEnterExit with (tablockx) Where Substring(OtaghdarTurnNumber,1,5)='" & SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() & "' and nEnterExitId<" & Turn.TurnId & " order by OtaghdarTurnNumber desc"
                Dim TurnIdTemp As String = CmdSql.ExecuteScalar
                If TurnIdTemp = String.Empty Then Throw New ResuscitationReserveTurnFailedException
                SequentialTurnId = Split(TurnIdTemp, ":")(0)
                SequentialTurnId_ = SequentialTurn.SeqTurnKeyWord.Trim + _R2DateTimeService.DateTimeServ.GetCurrentSalShamsiFull() + "/" + InstancePublicProcedures.RepeatStr("0", 6 - SequentialTurnId.ToString().Trim().Length) + SequentialTurnId.ToString().Trim() + ":" + Turn.TurnId.ToString
                TurnSeqT = SequentialTurnId_

                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim TurnRegisteringTimeStamp = IIf(TurnType = TurnType.Permanent, InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 4), _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladiFormated)
                'احیاء نوبت رزرو
                CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit 
                                      Set strCardno=" & Truck.TruckId & ",strDriverName='" & TruckDriver.NameFamily & "',strDesc=strDesc+' - " & SoftwareUser.UserName & "',bFlag=0,bFlagDriver=0,nDriverCode=" & TruckDriver.DriverId & ",OtaghdarTurnNumber='" & SequentialTurnId_ & "',TurnStatus=" & TurnStatuses.Registered & ",RegisteringTimeStamp='" & TurnRegisteringTimeStamp & "'
                                      Where nEnterExitId = " & Turn.TurnId & ""
                CmdSql.ExecuteNonQuery()
                'اکانتینگ نوبت
                InstanceTurnAccounting.TurnAccountingRegistering(Turn.TurnId, TurnAccounting.TurnAccouningTypes.TurnIssue, SoftwareUser.UserId, CmdSql)

                'bDelAutomated Used in AutomatedTurnRegistering for Control
                CmdSql.CommandText = "Update dbtransport.dbo.tbEnterExit Set bDelAutomated=0 Where bDelAutomated=1 and strCardno=" & Truck.TruckId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                DoStrategyComplementaryWorks()
            Catch ex As SqlException
                R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception When TypeOf ex Is TurnNotFoundException OrElse TypeOf ex Is ReserveTurnAlreadyUsedException OrElse TypeOf ex Is ResuscitationReserveTurnFailedException
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

    End Class


End Namespace
