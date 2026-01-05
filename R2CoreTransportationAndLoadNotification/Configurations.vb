


Imports R2Core.ConfigurationManagement
Imports R2Core.ConfigurationOfDevices
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.PublicProc
Imports R2CoreParkingSystem.GeneralConfiguration
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement.Exceptions
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text

'BPTChanged
Namespace GeneralConfiguration
    Public MustInherit Class R2CoreTransportationAndLoadNotificationGeneralConfigurations
        Inherits R2CoreParkingSystemGeneralConfigurations

        Public Shared ReadOnly Property TWS As Int64 = 51
        Public Shared ReadOnly Property BaseTurnsCancellationSetting As Int64 = 56
        Public Shared ReadOnly Property BaseTransportationAndLoadNotificationSetting As Int64 = 58
        Public Shared ReadOnly Property AutomaticTurnRegistering As Int64 = 61
        Public Shared ReadOnly Property LoadSedimentationAutomatedJob As Int64 = 63
        Public Shared ReadOnly Property TommorowLoads As Int64 = 71
        Public Shared ReadOnly Property LoadingDischargingPlaces As Int64 = 93
        Public Shared ReadOnly Property TruckDriversAutomatedJobs As Int64 = 95
        Public Shared ReadOnly Property LoadListsPreparingAutomatedJob As Int64 = 98



    End Class

End Namespace

'BPTChanged
Namespace ConfigurationOfLoadAnnouncement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncements
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property AnnouncementAnnounceTime As Int64 = 1
        Public Shared ReadOnly Property LoadSedimentation As Int64 = 2
        Public Shared ReadOnly Property AnnouncementsAutomaticProcessesTiming As Int64 = 3
        Public Shared ReadOnly Property LoadCapacitor As Int64 = 4
        Public Shared ReadOnly Property TommorowLoads As Int64 = 5
        Public Shared ReadOnly Property AutomaticTurnRegistering As Int64 = 6



    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationRawConfigurationOfLoadAnnouncement
        Public COLAId As Int64
        Public COLAName As String
        Public COLATitle As String
        Public AnnouncementId As Int64
        Public AnnouncementSGId As Int64
        Public COLAIndex As Int64
        Public COLAIndexTitle As String
        Public Description As String
        Public COLAValue As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncementManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllConfigurationOfLoadAnnouncement(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select ConfigurationOfLoadAnnouncement.COLAId,ConfigurationOfLoadAnnouncement.COLAName,ConfigurationOfLoadAnnouncement.COLATitle ,
                                ConfigurationOfLoadAnnouncement.AnnouncementId,Announcements.AnnouncementTitle,ConfigurationOfLoadAnnouncement.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,ConfigurationOfLoadAnnouncement.COLAIndex ,
	                            ConfigurationOfLoadAnnouncement.COLAIndexTitle,ConfigurationOfLoadAnnouncement.Description ,ConfigurationOfLoadAnnouncement.COLAValue  
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement as ConfigurationOfLoadAnnouncement 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On  ConfigurationOfLoadAnnouncement.AnnouncementId=Announcements.AnnouncementId 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On ConfigurationOfLoadAnnouncement.AnnouncementSGId=AnnouncementSubGroups.AnnouncementSGId 
                         Where ConfigurationOfLoadAnnouncement.Active=1
                         Order By ConfigurationOfLoadAnnouncement.COLAId,ConfigurationOfLoadAnnouncement.COLAIndex,
                                  ConfigurationOfLoadAnnouncement.AnnouncementId ,ConfigurationOfLoadAnnouncement.AnnouncementSGId 
                         for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select ConfigurationOfLoadAnnouncement.COLAId,ConfigurationOfLoadAnnouncement.COLAName,ConfigurationOfLoadAnnouncement.COLATitle ,
                                ConfigurationOfLoadAnnouncement.AnnouncementId,Announcements.AnnouncementTitle,ConfigurationOfLoadAnnouncement.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,ConfigurationOfLoadAnnouncement.COLAIndex ,
	                            ConfigurationOfLoadAnnouncement.COLAIndexTitle,ConfigurationOfLoadAnnouncement.Description ,ConfigurationOfLoadAnnouncement.COLAValue  
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement as ConfigurationOfLoadAnnouncement 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On  ConfigurationOfLoadAnnouncement.AnnouncementId=Announcements.AnnouncementId 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On ConfigurationOfLoadAnnouncement.AnnouncementSGId=AnnouncementSubGroups.AnnouncementSGId 
                         Where ConfigurationOfLoadAnnouncement.Active=1
                         Order By ConfigurationOfLoadAnnouncement.COLAId,ConfigurationOfLoadAnnouncement.COLAIndex,
                                  ConfigurationOfLoadAnnouncement.AnnouncementId ,ConfigurationOfLoadAnnouncement.AnnouncementSGId 
                         for JSON Path", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ConfigurationOfLoadAnnouncementRegistering(YourRawConfigurationOfLoadAnnouncement As R2CoreTransportationAndLoadNotificationRawConfigurationOfLoadAnnouncement)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement(COLAId,COLAName,COLATitle,AnnouncementId,AnnouncementSGId,COLAIndex,COLAIndexTitle,Description,COLAValue,Template,Active)
                                      Values(@COLAId,@COLAName,@COLATitle,@AnnouncementId,@AnnouncementSGId,@COLAIndex,@COLAIndexTitle,@Description,@COLAValue,0,1)"
                CmdSql.Parameters.Add("@COLAId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.COLAId
                CmdSql.Parameters.Add("@COLAName", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.COLAName
                CmdSql.Parameters.Add("@COLATitle", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.COLATitle
                CmdSql.Parameters.Add("@AnnouncementId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.AnnouncementId
                CmdSql.Parameters.Add("@AnnouncementSGId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.AnnouncementSGId
                CmdSql.Parameters.Add("@COLAIndex", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.COLAIndex
                CmdSql.Parameters.Add("@COLAIndexTitle", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.COLAIndexTitle
                CmdSql.Parameters.Add("@Description", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.Description
                CmdSql.Parameters.Add("@COLAValue", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.COLAValue
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ConfigurationOfLoadAnnouncementEditing(YourRawConfigurationOfLoadAnnouncement As R2CoreTransportationAndLoadNotificationRawConfigurationOfLoadAnnouncement)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement 
                                      Set COLAValue=@COLAValue Where COLAId=@COLAId and COLAIndex=@COLAIndex and AnnouncementId=@AnnouncementId and AnnouncementSGId=@AnnouncementSGId"
                CmdSql.Parameters.Add("@COLAValue", SqlDbType.NVarChar).Value = YourRawConfigurationOfLoadAnnouncement.COLAValue
                CmdSql.Parameters.Add("@COLAId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.COLAId
                CmdSql.Parameters.Add("@COLAIndex", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.COLAIndex
                CmdSql.Parameters.Add("@AnnouncementId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.AnnouncementId
                CmdSql.Parameters.Add("@AnnouncementSGId", SqlDbType.BigInt).Value = YourRawConfigurationOfLoadAnnouncement.AnnouncementSGId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ConfigurationOfLoadAnnouncementDeleting(YourCOLAId As Int64, YourCOLAIndex As Int64, YourAnnouncementId As Int64, YourAnnouncementSGId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement  
                                      Where COLAId=@COLAId and COLAIndex=@COLAIndex and AnnouncementId=@AnnouncementId and AnnouncementSGId=@AnnouncementSGId"
                CmdSql.Parameters.Add("@COLAId", SqlDbType.BigInt).Value = YourCOLAId
                CmdSql.Parameters.Add("@COLAIndex", SqlDbType.BigInt).Value = YourCOLAIndex
                CmdSql.Parameters.Add("@AnnouncementId", SqlDbType.BigInt).Value = YourAnnouncementId
                CmdSql.Parameters.Add("@AnnouncementSGId", SqlDbType.BigInt).Value = YourAnnouncementSGId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetBooleanConfiguration(YourCOLAId As Int64, YourCOLAIndex As Int64, YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As Boolean
            Try
                Return GetConfiguration(YourCOLAId, YourCOLAIndex, YourAnnouncementId, YourAnnouncementSGId)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetInt64Configuration(YourCOLAId As Int64, YourCOLAIndex As Int64, YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As Int64
            Try
                Return GetConfiguration(YourCOLAId, YourCOLAIndex, YourAnnouncementId, YourAnnouncementSGId)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetStringConfiguration(YourCOLAId As Int64, YourCOLAIndex As Int64, YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As String
            Try
                Return GetConfiguration(YourCOLAId, YourCOLAIndex, YourAnnouncementId, YourAnnouncementSGId)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Function GetConfiguration(YourCOLAId As Int64, YourCOLAIndex As Int64, YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As Object
            Try
                Dim DS As DataSet
                InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select COLAValue  from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement 
                    Where COLAId=" & YourCOLAId & " and COLAIndex=" & YourCOLAIndex & " and AnnouncementId=" & YourAnnouncementId & " and AnnouncementSGId=" & YourAnnouncementSGId & "", 32767, DS, New Boolean)
                Return DS.Tables(0).Rows(0).Item("COLAValue")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class


End Namespace


Namespace ConfigurationsManagement
    Public MustInherit Class R2CoreTransportationAndLoadNotificationConfigurations
        Inherits R2CoreConfigurations
        Public Shared ReadOnly Property TurnControlling As Int64 = 34
        'Public Shared ReadOnly Property TWS As Int64 = 51
        'Public Shared ReadOnly Property AnnouncementHallMonitoring As Int64 = 52
        'Public Shared ReadOnly Property LoadCapacitorLoadManipulationSetting As Int64 = 54
        'Public Shared ReadOnly Property AnnouncementHallAnnounceTime As Int64 = 55
        'Public Shared ReadOnly Property TurnsCancellationSetting As Int64 = 56
        'Public Shared ReadOnly Property AnnouncementsTruckDriverAttendance As Int64 = 57
        'Public Shared ReadOnly Property DefaultTransportationAndLoadNotificationConfigs As Int64 = 58
        Public Shared ReadOnly Property AnnouncementsLoadAllocationsLoadPermissionRegisteringSetting As Int64 = 59
        'Public Shared ReadOnly Property AnnouncementsTurnRegisteringSetting As Int64 = 61
        Public Shared ReadOnly Property AnnouncementsLoadPermissionsSetting As Int64 = 62
        'Public Shared ReadOnly Property AnnouncementsLoadSedimentationSetting As Int64 = 63
        'Public Shared ReadOnly Property AnnouncementsAutomaticProcessesTiming As Int64 = 68
        Public Shared ReadOnly Property AnnouncementsLoadAllocationSetting As Int64 = 69
        'Public Shared ReadOnly Property TommorowLoads As Int64 = 71
        Public Shared ReadOnly Property DriverSelfDeclarationSetting As Int64 = 86
        Public Shared ReadOnly Property XXX As Int64 = 88
        Public Shared ReadOnly Property IndigenousTrucks As Int64 = 89
        'Public Shared ReadOnly Property AnnouncementsLoadCapacitorControl As Int64 = 91
        Public Shared ReadOnly Property BillOfLading As Int64 = 92
        'Public Shared ReadOnly Property LoadingDischargingPlaces As Int64 = 93
        'Public Shared ReadOnly Property TruckDriversAutomatedJobsService As Int64 = 95
        'Public Shared ReadOnly Property LoadListsPreparingAutomatedJob As Int64 = 98
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        'Public Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Ds.Tables(0).Rows(0).Item("CValue")
        '    Catch ex As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId).trim
        '    Catch ex As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex).trim
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadViewCondition
        Public LoadViewConditionId As Int64
        Public AnnouncementId As Int64
        Public AnnouncementTitle As String
        Public AnnouncementSGId As Int64
        Public AnnouncementSGTitle As String
        Public SequentialTurnId As Int64
        Public SequentialTurnTitle As String
        Public TruckNativenessTypeId As Int64
        Public TruckNativenessTypeTitle As String
        Public LoadStatusId As Int64
        Public LoadStatusTitle As String
        Public RequesterId As Int64
        Public RequesterTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadAllocationCondition
        Public LoadAllocationConditionId As Int64
        Public AnnouncementId As Int64
        Public AnnouncementTitle As String
        Public AnnouncementSGId As Int64
        Public AnnouncementSGTitle As String
        Public SequentialTurnId As Int64
        Public SequentialTurnTitle As String
        Public TruckNativenessTypeId As Int64
        Public TruckNativenessTypeTitle As String
        Public LoadStatusId As Int64
        Public LoadStatusTitle As String
        Public RequesterId As Int64
        Public RequesterTitle As String
        Public TurnStatusId As Int64
        Public TurnStatusTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationConfigurationsManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllLoadViewConditions(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager()

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select LoadsViewConditions.OId as LoadViewConditionId,Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,
                                SequentialTurns.SeqTId as SequentialTurnId,SequentialTurns.SeqTTitle as SequentialTurnTitle,TruckNativenessTypes.NId as TruckNativenessTypeId,TruckNativenessTypes.NTItle as TruckNativenessTypeTitle,LoadStatuses.LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Requesters.RqId as RequesterId,Requesters.RqTitle as RequesterTitle 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsViewConditions as LoadsViewConditions
                            Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblAnnouncementSubGroups as AnnouncementSubGroups On LoadsViewConditions.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSubGroups.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On  LoadsViewConditions.SeqTId=SequentialTurns.SeqTId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On LoadsViewConditions.NativenessTypeId=TruckNativenessTypes.NId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On LoadsViewConditions.LoadStatusId=LoadStatuses.LoadStatusId 
                            Inner Join R2Primary.dbo.TblRequesters as Requesters On LoadsViewConditions.RequesterId=Requesters.RqId 
                         Order By AnnouncementSubGroups.AnnouncementSGId,SequentialTurns.SeqTId,TruckNativenessTypes.NId,LoadStatuses.LoadStatusId,Requesters.RqId for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select LoadsViewConditions.OId as LoadViewConditionId,Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,
                                SequentialTurns.SeqTId as SequentialTurnId,SequentialTurns.SeqTTitle as SequentialTurnTitle,TruckNativenessTypes.NId as TruckNativenessTypeId,TruckNativenessTypes.NTItle as TruckNativenessTypeTitle,LoadStatuses.LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Requesters.RqId as RequesterId,Requesters.RqTitle as RequesterTitle 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsViewConditions as LoadsViewConditions
                            Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblAnnouncementSubGroups as AnnouncementSubGroups On LoadsViewConditions.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSubGroups.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On  LoadsViewConditions.SeqTId=SequentialTurns.SeqTId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On LoadsViewConditions.NativenessTypeId=TruckNativenessTypes.NId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On LoadsViewConditions.LoadStatusId=LoadStatuses.LoadStatusId 
                            Inner Join R2Primary.dbo.TblRequesters as Requesters On LoadsViewConditions.RequesterId=Requesters.RqId 
                         Order By AnnouncementSubGroups.AnnouncementSGId,SequentialTurns.SeqTId,TruckNativenessTypes.NId,LoadStatuses.LoadStatusId,Requesters.RqId for JSON Path", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Sub LoadViewConditionRegistering(YourLoadViewCondition As R2CoreTransportationAndLoadNotificationLoadViewCondition)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsViewConditions(AHSGId,SeqTId,NativenessTypeId,LoadStatusId,RequesterId)
                                      Values(@AHSGId,@SeqTId,@NativenessTypeId,@LoadStatusId,@RequesterId"
                CmdSql.Parameters.Add("@AHSGId", SqlDbType.BigInt ).Value =YourLoadViewCondition.AnnouncementSGId
                CmdSql.Parameters.Add("@SeqTId", SqlDbType.BigInt ).Value =YourLoadViewCondition.SequentialTurnId
                CmdSql.Parameters.Add("@NativenessTypeId", SqlDbType.BigInt ).Value =YourLoadViewCondition.TruckNativenessTypeId
                CmdSql.Parameters.Add("@LoadStatusId", SqlDbType.BigInt ).Value = YourLoadViewCondition.LoadStatusId
                CmdSql.Parameters.Add("@RequesterId", SqlDbType.BigInt).Value = YourLoadViewCondition.RequesterId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadViewConditionEditing(YourLoadViewCondition As R2CoreTransportationAndLoadNotificationLoadViewCondition)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsViewConditions Set AHSGId=@AHSGId,SeqTId=@SeqTId,NativenessTypeId=@NativenessTypeId,LoadStatusId=@LoadStatusId,RequesterId=@RequesterId Where OId=@OId"
                CmdSql.Parameters.Add("@AHSGId", SqlDbType.BigInt).Value = YourLoadViewCondition.AnnouncementSGId
                CmdSql.Parameters.Add("@SeqTId", SqlDbType.BigInt).Value = YourLoadViewCondition.SequentialTurnId
                CmdSql.Parameters.Add("@NativenessTypeId", SqlDbType.BigInt).Value = YourLoadViewCondition.TruckNativenessTypeId
                CmdSql.Parameters.Add("@LoadStatusId", SqlDbType.BigInt).Value = YourLoadViewCondition.RequesterId
                CmdSql.Parameters.Add("@RequesterId", SqlDbType.BigInt).Value = YourLoadViewCondition.LoadStatusId
                CmdSql.Parameters.Add("@OId", SqlDbType.BigInt).Value = YourLoadViewCondition.LoadViewConditionId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadViewConditionDeleting(YourLoadViewConditionId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsViewConditions Where OId=@OId"
                CmdSql.Parameters.Add("@OId", SqlDbType.BigInt).Value = YourLoadViewConditionId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetAllLoadAllocationConditions(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager()

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select LoadsAllocationConditions.OId as LoadAllocationConditionId,Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,
                                SequentialTurns.SeqTId as SequentialTurnId,SequentialTurns.SeqTTitle as SequentialTurnTitle,TruckNativenessTypes.NId as TruckNativenessTypeId,TruckNativenessTypes.NTItle as TruckNativenessTypeTitle,
                                LoadStatuses.LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Requesters.RqId as RequesterId,Requesters.RqTitle as RequesterTitle,TurnStatuses.TurnStatusId,TurnStatuses.Description as TurnStatusTitle 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions as LoadsAllocationConditions
                            Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblAnnouncementSubGroups as AnnouncementSubGroups On LoadsAllocationConditions.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSubGroups.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On  LoadsAllocationConditions.SeqTId=SequentialTurns.SeqTId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On LoadsAllocationConditions.NativenessTypeId=TruckNativenessTypes.NId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On LoadsAllocationConditions.LoadStatusId=LoadStatuses.LoadStatusId 
                            Inner Join R2Primary.dbo.TblRequesters as Requesters On LoadsAllocationConditions.RequesterId=Requesters.RqId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On LoadsAllocationConditions.TurnStatusId=TurnStatuses.TurnStatusId 
                         Order By AnnouncementSubGroups.AnnouncementSGId,SequentialTurns.SeqTId,TruckNativenessTypes.NId,LoadStatuses.LoadStatusId,Requesters.RqId,TurnStatuses.TurnStatusId for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select LoadsAllocationConditions.OId as LoadAllocationConditionId,Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle,
                                SequentialTurns.SeqTId as SequentialTurnId,SequentialTurns.SeqTTitle as SequentialTurnTitle,TruckNativenessTypes.NId as TruckNativenessTypeId,TruckNativenessTypes.NTItle as TruckNativenessTypeTitle,
                                LoadStatuses.LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Requesters.RqId as RequesterId,Requesters.RqTitle as RequesterTitle,TurnStatuses.TurnStatusId,TurnStatuses.Description as TurnStatusTitle 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions as LoadsAllocationConditions
                            Inner Join R2PrimaryTransportationAndLoadNotification.DBO.TblAnnouncementSubGroups as AnnouncementSubGroups On LoadsAllocationConditions.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSubGroups.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On  LoadsAllocationConditions.SeqTId=SequentialTurns.SeqTId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On LoadsAllocationConditions.NativenessTypeId=TruckNativenessTypes.NId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On LoadsAllocationConditions.LoadStatusId=LoadStatuses.LoadStatusId 
                            Inner Join R2Primary.dbo.TblRequesters as Requesters On LoadsAllocationConditions.RequesterId=Requesters.RqId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnStatuses as TurnStatuses On LoadsAllocationConditions.TurnStatusId=TurnStatuses.TurnStatusId 
                         Order By AnnouncementSubGroups.AnnouncementSGId,SequentialTurns.SeqTId,TruckNativenessTypes.NId,LoadStatuses.LoadStatusId,Requesters.RqId,TurnStatuses.TurnStatusId for JSON Path", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Sub LoadAllocationConditionRegistering(YourLoadAllocationCondition As R2CoreTransportationAndLoadNotificationLoadAllocationCondition)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions(AHSGId,SeqTId,NativenessTypeId,LoadStatusId,RequesterId,TurnStatusId)
                                      Values(" & YourLoadAllocationCondition.AnnouncementSGId & "," & YourLoadAllocationCondition.SequentialTurnId & "," & YourLoadAllocationCondition.TruckNativenessTypeId & "," & YourLoadAllocationCondition.LoadStatusId & "," & YourLoadAllocationCondition.RequesterId & "," & YourLoadAllocationCondition.TurnStatusId & ")"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationConditionEditing(YourLoadAllocationCondition As R2CoreTransportationAndLoadNotificationLoadAllocationCondition)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions Set AHSGId=@AHSGId,SeqTId=@SeqTId,NativenessTypeId=@NativenessTypeId,LoadStatusId=@LoadStatusId,RequesterId=@RequesterId,TurnStatusId=@TurnStatusId Where OId=@OId"
                CmdSql.Parameters.Add("@AHSGId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.AnnouncementSGId
                CmdSql.Parameters.Add("@SeqTId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.SequentialTurnId
                CmdSql.Parameters.Add("@NativenessTypeId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.TruckNativenessTypeId
                CmdSql.Parameters.Add("@LoadStatusId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.LoadStatusId
                CmdSql.Parameters.Add("@RequesterId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.RequesterId
                CmdSql.Parameters.Add("@TurnStatusId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.TurnStatusId
                CmdSql.Parameters.Add("@OId", SqlDbType.BigInt).Value = YourLoadAllocationCondition.LoadAllocationConditionId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadAllocationConditionDeleting(YourLoadAllocationConditionId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsAllocationConditions Where OId=@OId"
                CmdSql.Parameters.Add("@OId", SqlDbType.BigInt).Value = YourLoadAllocationConditionId
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        'Public Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Ds.Tables(0).Rows(0).Item("CValue")
        '    Catch ex As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId).trim
        '    Catch ex As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex).trim
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement
        'Inherits R2CoreMClassConfigurationManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        'Public Shared Function GetConfigOnLine(YourCId As Int64, YourAHId As Int64) As Object
        '    Try
        '        Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
        '        Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "")
        '        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection()
        '        If Da.Fill(Ds) = 0 Then Throw New GetDataException
        '        Return Ds.Tables(0).Rows(0).Item("CValue").trim
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
        '            Throw New ConfigurationOfAnnouncementHallNotFoundException
        '        End If
        '        Return Ds.Tables(0).Rows(0).Item("CValue")
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex).trim
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
        '    Try
        '        Return GetConfig(YourCId, YourAHId).trim
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigInt32(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int32
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigDouble(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Double
        '    Try
        '        Dim xRong As Double = GetConfig(YourCId, YourAHId, YourIndex)
        '        Return xRong
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetConfigByte(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Byte
        '    Try
        '        Return GetConfig(YourCId, YourAHId, YourIndex)
        '    Catch exx As ConfigurationOfAnnouncementHallNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourCValue As String)
        '    Dim CmdSql As New SqlCommand
        '    CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '    Try
        '        CmdSql.Connection.Open()
        '        CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Set CValue = '" & YourCValue & "' Where CId=" & YourCId & " and AHId=" & YourAHId & ""
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.Connection.Close()
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        'Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourIndex As Int64, YourCValue As String)
        '    Try
        '        Dim CurrentConfigValue As String = GetConfigOnLine(YourCId, YourAHId)
        '        Dim CurrentConfigValueSplitted As String() = Split(CurrentConfigValue, ";")
        '        Dim SB As New StringBuilder
        '        For Loopx As Int64 = 0 To CurrentConfigValueSplitted.Length - 1
        '            If Loopx = YourIndex Then
        '                SB.Append(YourCValue)
        '            Else
        '                SB.Append(CurrentConfigValueSplitted(Loopx))
        '            End If
        '            If Loopx < CurrentConfigValueSplitted.Length - 1 Then SB.Append(";")
        '        Next
        '        SetConfiguration(YourCId, YourAHId, SB.ToString())
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub


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

