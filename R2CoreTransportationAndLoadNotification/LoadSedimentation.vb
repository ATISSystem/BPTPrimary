


Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.LoggingManagement
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorAccounting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation.Exceptions
Imports R2CoreTransportationAndLoadNotification.Logging
Imports System.Reflection

Namespace LoadSedimentation

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupSedimentingConfigurationStructure

        Public Sub New()
            MyBase.New()
            _SedimentingStartTime = Nothing
            _SedimentingEndTime = Nothing
            _SedimentingDelationMinutes = Long.MinValue
            _SedimentingActive = False
        End Sub

        Public Sub New(ByVal YourSedimentingStartTime As R2CoreDateAndTime, YourSedimentingEndTime As R2CoreDateAndTime, YourSedimentingDelationMinutes As Int64, YourSedimentingActive As Boolean)
            _SedimentingStartTime = YourSedimentingStartTime
            _SedimentingEndTime = YourSedimentingEndTime
            _SedimentingDelationMinutes = YourSedimentingDelationMinutes
            _SedimentingActive = YourSedimentingActive
        End Sub


        Public Property SedimentingStartTime As R2CoreDateAndTime
        Public Property SedimentingEndTime As R2CoreDateAndTime
        Public Property SedimentingDelationMinutes As Int64
        Public Property SedimentingActive As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManager
        Private Shared _DateTime As New R2DateTime

        Public Sub SedimentingProcess()
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                Dim Lst = InstanceAnnouncements.GetAnnouncementsAnnouncementsubGroupsJOINT()
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AHId, AHSGId As Int64
                For Each C In Lst
                    Try
                        AHId = C.NSSAnnounementHall.AHId
                        AHSGId = C.NSSAnnouncementsubGroup.AHSGId
                        'کنترل فعال بودن فرآیند رسوب برای زیرگروه مورد نظر
                        If Not IsActiveSedimenting(AHId, AHSGId) Then Continue For
                        'کنترل تایمینگ - آیا رسوب بار برای زیرگروه مورد نظر فرارسیده است
                        If InstanceAnnouncementTiming.IsTimingActive(AHId, AHSGId) Then
                            If Not ((InstanceAnnouncementTiming.GetTiming(AHId, AHSGId, CurrentTime) = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering) Or (InstanceAnnouncementTiming.GetTiming(AHId, AHSGId, CurrentTime) = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InSedimenting)) Then
                                Continue For
                            End If
                        Else
                            Continue For
                        End If
                        'کنترل این که ممکن است هنوز آزاد سازی بار تمام نشده باشد لذا رسوب بار نباید انجام گیرد حتی اگر زمان رسوب بار فرارسیده باشد
                        Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationInstanceLoadAllocationManager
                        If InstanceLoadAllocation.GetLoadAllocationsforLoadPermissionRegistering(AHId, AHSGId).Count <> 0 Then Continue For
                        'لیست کامل باری که باید رسوب گردد
                        'موجودی داشته باشد و حذف کنسل و رسوب شده نباشد بار فردا هم نباشد 
                        Dim InstanceLoadCapacitor = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                        Dim LstLoads = InstanceLoadCapacitor.GetLoadCapacitorLoads(AHId, AHSGId, AnnouncementHallAnnounceTimeTypes.LastAnnounceLoads, False, True, R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nEstelamId)
                        If IsNothing(LstLoads) Or LstLoads.Count = 0 Then Continue For
                        'رسوب بار
                        Dim LastAnnounceTime = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(AHId, AHSGId).Time
                        CmdSql.Connection.Open()
                        CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set bFlag=1,LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " 
                                              Where (dDateElam='" & _DateTime.GetCurrentShamsiDate & "') and (LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & " or LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & ") and AHId=" & AHId & " and AHSGId=" & AHSGId & " and (dTimeElam<='" & LastAnnounceTime & "') and nCarNum>0"
                        CmdSql.ExecuteNonQuery()
                        CmdSql.Connection.Close()
                        'ثبت اکانتینگ
                        Dim InstanceAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                        For Each LoadCapcitorLoad In LstLoads
                            InstanceAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(LoadCapcitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting, LoadCapcitorLoad.nCarNum, Nothing, Nothing, Nothing, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId))
                        Next
                    Catch ex As Exception
                        If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                        'R2CoreMClassLoggingManagement.LogRegister(New R2CoreStandardLoggingStructure(Nothing, R2CoreTransportationAndLoadNotificationLogType.LoadCapacitorSedimentingFailed, ex.Message, "AHId:" + AHId.ToString, "AHSGId:" + AHSGId.ToString, String.Empty, String.Empty, String.Empty, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId, Nothing, Nothing))
                    End Try
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Function IsActiveSedimenting(YourAHId As Int64, YourAHSGId As Int64) As Boolean
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllSedimentingTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadSedimentationSetting, YourAHId), "&")
                Dim SedimentingStatus = Split(Mid(AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")(0)
                Return SedimentingStatus
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SedimentingLoadCapacitorLoad(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                If NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented And NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted And NSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Then
                    CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set bFlag=1,LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " 
                                          Where (nEstelamId=" & YournEstelamId & ") and (LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & ")"
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YournEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                End If
            Catch ex As LoadCapacitorLoadNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function HowManyMinutesPassedfromSedimentation(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as LoadCapacitorAccounting
                      Where LoadCapacitorAccounting.nEstelamId=" & YourNSS.nEstelamId & " and LoadCapacitorAccounting.AccountType=" & R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting & " Order By LoadCapacitorAccounting.DateTimeMilladi desc", 360, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New LoadIsNotSedimentedException
                Else
                    Return DateDiff(DateInterval.Minute, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), _DateTime.GetCurrentDateTimeMilladi())
                End If
            Catch ex As LoadIsNotSedimentedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HowManyMinutesMustPassedfromSedimentation(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim ComposeSearchString As String = YourNSS.AHSGId.ToString + "="
                Dim AllSedimentingTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadSedimentationSetting, YourNSS.AHId), "&")
                Dim MinutesMustPassedfromSedimenting = Split(Mid(AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllSedimentingTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")(1)
                Return MinutesMustPassedfromSedimenting
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadSedimentationManagement
        Private Shared _DateTime As New R2DateTime



    End Class

    Namespace Exceptions
        Public Class LoadIsNotSedimentedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "بار مورد نظر هنوز به مرحله رسوب نرسیده است"
                End Get
            End Property
        End Class

    End Namespace



End Namespace
