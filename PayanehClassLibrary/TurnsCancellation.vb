


Imports PayanehClassLibrary.CarTrucksManagement
Imports R2Core.ConfigurationManagement
Imports System.Reflection
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.PermissionManagement
Imports R2Core.PermissionManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.CalendarManagement.SpecializedPersianCalendar
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2Core.PubSubMessaging
Imports R2Core.LoggingManagement
Imports PayanehClassLibrary.Logging
Imports R2Core.GeneralConfiguration
Imports R2CoreTransportationAndLoadNotification.GeneralConfiguration
Imports R2CoreTransportationAndLoadNotification
Imports R2Core.SQLInjectionPrevention

Namespace TurnsCancellation

    'BPTChanged
    Public Class PayanehClassLibraryTurnsCancellationManager

        Private _DateTimeService As IDateTimeService
        Private _InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _InstanceLogging As R2CoreLoggingManager
        Private _loggerService As ILogger


        Public Sub New(YourR2DateTimeService As IDateTimeService)
            _DateTimeService = YourR2DateTimeService
            _InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
            _InstanceLogging = New R2CoreLoggingManager
            _loggerService = New R2Core.LoggingManagement.R2CorenLogService()
        End Sub

        Private Sub TurnsCancellationByLoadTargetMethod(YourSequentialTurn As R2CoreTransportationAndLoadNotificationSequentialTurn, YourTurnId As Int64, YourSoftwareUserId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim DSTurns As DataSet
                Dim TotalTurns = _InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select nEnterExitId from dbtransport.dbo.TbEnterExit
                        Where (TurnStatus = " & TurnStatuses.Registered & " Or TurnStatus = " & TurnStatuses.UsedLoadAllocationRegistered & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadAllocationCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadPermissionCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationUser & ")
                              and Substring(OtaghdarTurnNumber,1,1)='" & YourSequentialTurn.SeqTurnKeyWord & "' and nEnterExitId<" & YourTurnId & "", 0, DSTurns, New Boolean).GetRecordsCount
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update dbtransport.dbo.TbEnterExit Set TurnStatus=" & TurnStatuses.CancelledUnderScore & ",bFlag=1,bFlagDriver=1,strElamDate='" & _DateTimeService.GetCurrentShamsiDate & "',strElamTime='" & _DateTimeService.GetCurrentTime & "',nUserIdExit=" & YourSoftwareUserId & " 
                                      Where (TurnStatus = " & TurnStatuses.Registered & " Or TurnStatus = " & TurnStatuses.UsedLoadAllocationRegistered & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadAllocationCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationLoadPermissionCancelled & " Or TurnStatus = " & TurnStatuses.ResuscitationUser & ") 
                                      and Substring(OtaghdarTurnNumber, 1, 1) ='" & YourSequentialTurn.SeqTurnKeyWord & "' and nEnterExitId<" & YourTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'در آنلاین
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                For LoopOnLine As Int64 = 0 To DSTurns.Tables(0).Rows.Count - 1
                    Try
                        Dim Truck = InstanceTurns.GetTruckByTurnId(DSTurns.Tables(0).Rows(LoopOnLine).Item("nEnterExitId"))
                        TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(Truck.Pelak, Truck.Serial)
                    Catch ex As RelationBetweenTurnAndTruckNotFoundException
                    Catch ex As Exception
                    End Try
                Next

                'لاگ آمار ابطال
                _loggerService.RegisterInfLog(New R2CoreRawLog With {.LogTypeId = PayanehClassLibraryLogType.TurnsCancellation, .Description = String.Empty, .MessageDetail1 = NameOf(YourSequentialTurn) + ":" + YourSequentialTurn.SeqTurnTitle, .MessageDetail2 = NameOf(TotalTurns) + ":" + TotalTurns.ToString, .UserId = YourSoftwareUserId})

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

        Private Sub TurnsCancellationBy3DaysMethod(YourSequentialTurn As R2CoreTransportationAndLoadNotificationSequentialTurn, YourSoftwareUserId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                Dim DSTurns As DataSet
                Dim TotalTurns = _InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select nEnterExitId from dbtransport.dbo.tbEnterExit as Turns
                        Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                              Substring(Turns.OtaghdarTurnNumber,1,1)='" & YourSequentialTurn.SeqTurnKeyWord & "' and
                              ((Turns.strCardno in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 5) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & _DateTimeService.GetCurrentShamsiDate & "' Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
   	                           )
	                           or
	                           (Turns.strCardno not in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 6) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<='" & _DateTimeService.GetCurrentShamsiDate & "' Order By DateShamsi desc) as DataBox
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
                      Set TurnStatus=" & TurnStatuses.CancelledUnderScore & ",bFlag=1,bFlagDriver=1,strElamDate= R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()),strElamTime=convert(varchar, getdate(), 8),nUserIdExit=" & YourSoftwareUserId & " 
                      Where nEnterExitId In   
                       (Select nEnterExitId from dbtransport.dbo.tbEnterExit as Turns
                        Where (Turns.TurnStatus=" & TurnStatuses.Registered & " or Turns.TurnStatus=" & TurnStatuses.UsedLoadAllocationRegistered & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadAllocationCancelled & "  or Turns.TurnStatus=" & TurnStatuses.ResuscitationLoadPermissionCancelled & " or Turns.TurnStatus=" & TurnStatuses.ResuscitationUser & ") and
                              Substring(Turns.OtaghdarTurnNumber,1,1)='" & YourSequentialTurn.SeqTurnKeyWord & "' and
                              ((Turns.strCardno in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 5) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<=R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()) Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
   	                           )
	                           or
	                           (Turns.strCardno not in (Select Distinct nIDCar from dbtransport.dbo.TbCar Where CarNativenessTypeId=1) and
	                            Turns.strEnterDate Collate Arabic_CI_AI_WS<=(Select Top 1 DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                             Where DateShamsi<
                                                                                (Select Top 1 DateShamsi from 
                                                                                  (Select Top " & InstanceGeneralConfiguration.GetInt64Configuration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 6) & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                                                                   Where PCType=0 and DateShamsi<=R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()) Order By DateShamsi desc) as DataBox
                                                                                 Order By DateShamsi)
                                                                             Order By DateShamsi Desc)
 	                           )
                              )
                       )"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'در آنلاین
                    Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                    For LoopOnLine As Int64 = 0 To DSTurns.Tables(0).Rows.Count - 1
                        Try
                            Dim Truck = InstanceTurns.GetTruckByTurnId(DSTurns.Tables(0).Rows(LoopOnLine).Item("nEnterExitId"))
                            TWSClassLibrary.TDBClientManagement.TWSClassTDBClientManagement.DelNobat(Truck.Pelak, Truck.Serial)
                        Catch ex As RelationBetweenTurnAndTruckNotFoundException
                        Catch ex As Exception
                        End Try
                    Next

                    'لاگ آمار ابطال
                    _loggerService.RegisterInfLog(New R2CoreRawLog With {.LogTypeId = PayanehClassLibraryLogType.TurnsCancellation, .Description = String.Empty, .MessageDetail1 = NameOf(YourSequentialTurn) + ":" + YourSequentialTurn.SeqTurnTitle, .MessageDetail2 = NameOf(TotalTurns) + ":" + TotalTurns.ToString, .UserId = YourSoftwareUserId})

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

        Public Sub TurnsCancellation(YourSoftwareUserId As Int64)
            Try
                Dim InstancePermissions = New R2CorePermissionsManager(_DateTimeService)
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanExcecuteTurnsCancellation, YourSoftwareUserId, 0) Then Throw New PermissionException

                Dim InstanceSpecializedPersianCalendar = New R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager(_DateTimeService)
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService)
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)

                'طبق کانفیگ سیستم کلا ابطال نوبت ها فعال باشد یا نه
                If Not InstanceGeneralConfiguration.GetBooleanConfiguration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 0) Then Return

                'ابطال نوبت ها در روزهای تعطیل صورت نمی گیرد
                If InstanceSpecializedPersianCalendar.IsTodayIsHoliday() Then Return

                'کنترل این که ممکن است هنوز آزاد سازی بار تمام نشده باشد لذا ابطال نوبت ها نباید انجام گیرد حتی اگر زمان ابطال نوبت ها فرارسیده باشد
                If InstanceLoadAllocation.DoesExistAnyLoadAllocationforLoadPermissionRegistering() Then Exit Sub

                'ابطال نوبت ها
                Dim LstSeqTs = InstanceSequentialTurns.GetSequentialTurns()
                For Loopx As Int64 = 0 To LstSeqTs.Count - 1
                    Dim ComposeSearchString As String = LstSeqTs(Loopx).SeqTurnId.ToString + "="
                    Dim AllSeqTConfig As String() = Split(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreTransportationAndLoadNotificationGeneralConfigurations.BaseTurnsCancellationSetting, 2), "-")
                    Dim ConfigSeqTTurnsCancellationActiveFlag As Boolean = Split(Mid(AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "|")(0)
                    Dim ConfigSeqTTurnsCancellationStartSendingTime As String = Split(Mid(AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSeqTConfig.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "|")(1)
                    If Not ConfigSeqTTurnsCancellationActiveFlag Then Continue For
                    If _DateTimeService.GetCurrentTime < ConfigSeqTTurnsCancellationStartSendingTime Then Continue For
                    TurnsCancellationBy3DaysMethod(LstSeqTs(Loopx), YourSoftwareUserId)
                Next
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub



    End Class

End Namespace