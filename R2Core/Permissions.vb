
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PermissionManagement.Exceptions
Imports R2Core.PredefinedMessagesManagement

Namespace PermissionManagement

    'BPTChanged
    Public MustInherit Class R2CorePermissionTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly R2CorePermissionTypesXX As Int64 = 1
        Public Shared ReadOnly SoftwareUsersAccessWebProcesses As Int64 = 2
        Public Shared ReadOnly UserCanSendSoftwareUserShenasehPasswordViaSMS As Int64 = 7
        Public Shared ReadOnly R2CorePermissionTypesXXX As Int64 = 8
        Public Shared ReadOnly R2CorePermissionTypesXXXX As Int64 = 9
        Public Shared ReadOnly R2CorePermissionTypesXXXXX As Int64 = 22
        Public Shared ReadOnly R2CorePermissionTypesXXXXXX As Int64 = 30
    End Class

    'BPTChanged
    Public Class R2CorePermissionsManager

        Private _DateTimeService As IDateTimeService
        Private InstanseSqlDataBOX As R2CoreSqlDataBOXManager

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                InstanseSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisteringPermissions(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdsSecond As String())
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "Delete R2Primary.dbo.TblPermissions Where PermissionTypeId=" & YourPermissionTypeId & " and EntityIdFirst=" & YourEntityIdFirst & ""
                Cmdsql.ExecuteNonQuery()
                For Loopx As Int64 = 0 To YourEntityIdsSecond.Count - 1
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblPermissions(EntityIdFirst,EntityIdSecond,PermissionTypeId,RelationActive) 
                                          Values(" & YourEntityIdFirst & "," & YourEntityIdsSecond(Loopx) & "," & YourPermissionTypeId & ",1)"
                    Cmdsql.ExecuteNonQuery()
                Next
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisteringPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Cmdsql.Connection.Open()
                Cmdsql.CommandText = "Delete R2Primary.dbo.TblPermissions Where PermissionTypeId=" & YourPermissionTypeId & " and EntityIdFirst=" & YourEntityIdFirst & ""
                Cmdsql.ExecuteNonQuery()
                Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblPermissions(EntityIdFirst,EntityIdSecond,PermissionTypeId,RelationActive) 
                                          Values(" & YourEntityIdFirst & ",0," & YourPermissionTypeId & ",1)"
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Connection.Close()
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function ExistPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanseSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select * from R2Primary.dbo.TblPermissions as Permissions 
                       Where Permissions.PermissionTypeId=" & YourPermissionTypeId & " and Permissions.RelationActive=1 and Permissions.EntityIdFirst=" & YourEntityIdFirst & " and Permissions.EntityIdSecond=" & YourEntityIdSecond & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    'BPTChanged
    Namespace Exceptions

        'BPTChanged
        Public Class PermissionException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PermissionException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PermissionException).MsgId
            End Sub
        End Class

    End Namespace

End Namespace
