



Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports System.Reflection

Namespace TurnCancellation
    Public Class R2CoreTransportationAndLoadNotificationTurnCancellationManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Private Const TurnCancellationLoadColor = "OrangeRed"
        Private Const NonTurnCancellationLoadColor = "Green"

        Private Function IsLoadTargetforTurnCancellation(YourLoadTargetId As Int64) As Boolean
            Try
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select Top 1 LoadTargetId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadTargetsforTurnCancellation 
                        Where LoadTargetId=" & YourLoadTargetId & " and Active=1", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function IsLoadforTurnCancellation(YourLoadTargetCityId As Int64, YourTonaj As Double) As Boolean
            Try
                Dim InstanceConfigurations As New R2CoreInstanceConfigurationManager(_DateTimeService)
                If IsLoadTargetforTurnCancellation(YourLoadTargetCityId) Then
                    If YourTonaj <= InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 8) Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadforTurnCancellationRegistering(YourLoadId As Int64, YourCancellationFlag As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim myDescription As String = String.Empty
                If YourCancellationFlag Then
                    myDescription = TurnCancellationLoadColor
                Else
                    myDescription = NonTurnCancellationLoadColor
                End If
                CmdSql.Connection.Open()
                CmdSql.CommandText = " Insert into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation(nEstelamId,Description,DateShamsi,DateTimeMilladi,Time,Active) Values(" & YourLoadId & ",'" & myDescription & "','" & _DateTimeService.GetCurrentShamsiDate & "','" & _DateTimeService.GetCurrentDateTimeMilladi & "','" & _DateTimeService.GetCurrentTime & "'," & IIf(YourCancellationFlag, 1, 0) & ")"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadforTurnCancellationActiving(nEstelamId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation Set Active=1,Description='" & TurnCancellationLoadColor & "' Where nEstelamId=" & nEstelamId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadforTurnCancellationUnActiving(nEstelamId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation Set Active=0,Description='" & NonTurnCancellationLoadColor & "' Where nEstelamId=" & nEstelamId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class



End Namespace
