


Imports R2Core.ConfigurationManagement
Imports R2Core.ConfigurationOfDevices
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement.Exceptions
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text

Namespace ConfigurationsManagement
    Public MustInherit Class R2CoreTransportationAndLoadNotificationConfigurations
        Inherits R2CoreConfigurations
        Public Shared ReadOnly Property TurnControlling As Int64 = 34
        Public Shared ReadOnly Property TWS As Int64 = 51
        Public Shared ReadOnly Property AnnouncementHallMonitoring As Int64 = 52
        Public Shared ReadOnly Property LoadCapacitorLoadManipulationSetting As Int64 = 54
        Public Shared ReadOnly Property AnnouncementHallAnnounceTime As Int64 = 55
        Public Shared ReadOnly Property TurnsCancellationSetting As Int64 = 56
        Public Shared ReadOnly Property AnnouncementsTruckDriverAttendance As Int64 = 57
        Public Shared ReadOnly Property DefaultTransportationAndLoadNotificationConfigs As Int64 = 58
        Public Shared ReadOnly Property AnnouncementsLoadAllocationsLoadPermissionRegisteringSetting As Int64 = 59
        Public Shared ReadOnly Property AnnouncementsTurnRegisteringSetting As Int64 = 61
        Public Shared ReadOnly Property AnnouncementsLoadPermissionsSetting As Int64 = 62
        Public Shared ReadOnly Property AnnouncementsLoadSedimentationSetting As Int64 = 63
        Public Shared ReadOnly Property AnnouncementsAutomaticProcessesTiming As Int64 = 68
        Public Shared ReadOnly Property AnnouncementsLoadAllocationSetting As Int64 = 69
        Public Shared ReadOnly Property TommorowLoads As Int64 = 71
        Public Shared ReadOnly Property DriverSelfDeclarationSetting As Int64 = 86
        Public Shared ReadOnly Property XXX As Int64 = 88
        Public Shared ReadOnly Property IndigenousTrucks As Int64 = 89
        Public Shared ReadOnly Property AnnouncementsLoadCapacitorControl As Int64 = 91
        Public Shared ReadOnly Property BillOfLading As Int64 = 92
        Public Shared ReadOnly Property LoadingDischargingPlaces As Int64 = 93
        Public Shared ReadOnly Property TruckDriversAutomatedJobsService As Int64 = 95
        Public Shared ReadOnly Property LoadListsPreparingAutomatedJob As Int64 = 98
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId).trim
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId).trim
            Catch ex As ConfigurationOfAnnouncementHallNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement
        'Inherits R2CoreMClassConfigurationManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Shared Function GetConfigOnLine(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection()
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Ds.Tables(0).Rows(0).Item("CValue").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64, YourAHId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Where CId=" & YourCId & " and AHId=" & YourAHId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New ConfigurationOfAnnouncementHallNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourAHId As Int64) As String
            Try
                Return GetConfig(YourCId, YourAHId).trim
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt32(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int32
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigBoolean(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt64(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigDouble(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Double
            Try
                Dim xRong As Double = GetConfig(YourCId, YourAHId, YourIndex)
                Return xRong
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigByte(YourCId As Int64, YourAHId As Int64, YourIndex As Int64) As Byte
            Try
                Return GetConfig(YourCId, YourAHId, YourIndex)
            Catch exx As ConfigurationOfAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourCValue As String)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Set CValue = '" & YourCValue & "' Where CId=" & YourCId & " and AHId=" & YourAHId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Overloads Shared Sub SetConfiguration(YourCId As Int64, YourAHId As Int64, YourIndex As Int64, YourCValue As String)
            Try
                Dim CurrentConfigValue As String = GetConfigOnLine(YourCId, YourAHId)
                Dim CurrentConfigValueSplitted As String() = Split(CurrentConfigValue, ";")
                Dim SB As New StringBuilder
                For Loopx As Int64 = 0 To CurrentConfigValueSplitted.Length - 1
                    If Loopx = YourIndex Then
                        SB.Append(YourCValue)
                    Else
                        SB.Append(CurrentConfigValueSplitted(Loopx))
                    End If
                    If Loopx < CurrentConfigValueSplitted.Length - 1 Then SB.Append(";")
                Next
                SetConfiguration(YourCId, YourAHId, SB.ToString())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


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

'BPTChanged
Namespace ConfigurationOfLoadAnnouncement

    'BPTChanged
    Public MustInherit Class R2CoreTransportationAndLoadNotificationConfigurationOfLoadAnnouncement
        Public Shared ReadOnly Property None As Int64 = 0


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
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationOfLoadAnnouncement(COLAId,COLAName,COLATitle,AnnouncementId,AnnouncementSGId,COLAIndex,
        	                                                                                       COLAIndexTitle,Description,COLAValue,Template,Active)
                                      Values(" & YourRawConfigurationOfLoadAnnouncement.COLAId & ",'" & YourRawConfigurationOfLoadAnnouncement.COLAName & "','" & YourRawConfigurationOfLoadAnnouncement.COLATitle & "',
                                             " & YourRawConfigurationOfLoadAnnouncement.AnnouncementId & "," & YourRawConfigurationOfLoadAnnouncement.AnnouncementSGId & ",
                                             " & YourRawConfigurationOfLoadAnnouncement.COLAIndex & ",'" & YourRawConfigurationOfLoadAnnouncement.COLAIndexTitle & "','" & YourRawConfigurationOfLoadAnnouncement.Description & "',
                                             '" & YourRawConfigurationOfLoadAnnouncement.COLAValue & "',0,1)"
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
                                      Set COLAValue='" & YourRawConfigurationOfLoadAnnouncement.COLAValue & "'
                                      Where COLAId=" & YourRawConfigurationOfLoadAnnouncement.COLAId & " and COLAIndex=" & YourRawConfigurationOfLoadAnnouncement.COLAIndex & " and AnnouncementId=" & YourRawConfigurationOfLoadAnnouncement.AnnouncementId & " and AnnouncementSGId=" & YourRawConfigurationOfLoadAnnouncement.AnnouncementSGId & ""
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
                                      Where COLAId=" & YourCOLAId & " and COLAIndex=" & YourCOLAIndex & " and AnnouncementId=" & YourAnnouncementId & " and AnnouncementSGId=" & YourAnnouncementSGId & ""
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


End Namespace
