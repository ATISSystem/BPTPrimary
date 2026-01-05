


Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.PermissionManagement.Exceptions
Imports System.Drawing
Imports System.Reflection

Namespace PermissionManagement

    Public Class R2StandardPermissionTypeStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            PTId = Int64.MinValue
            PTName = String.Empty
            PTTitle = String.Empty
            PTColor = Color.White
            EntityTableId1 = Int64.MinValue
            EntityTableId2 = Int64.MinValue
            Description = String.Empty
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourPTId As Int64, YourPTName As String, YourPTTitle As String, YourPTColor As Color, YourEntityTableId1 As Int64, YourEntityTableId2 As Int64, YourDescription As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPTId, YourPTName)
            PTId = YourPTId
            PTName = YourPTName
            PTTitle = YourPTTitle
            PTColor = YourPTColor
            EntityTableId1 = YourEntityTableId1
            EntityTableId2 = YourEntityTableId2
            Description = YourDescription
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PTId As Int64
        Public Property PTName As String
        Public Property PTTitle As String
        Public Property PTColor As Color
        Public Property EntityTableId1 As Int64
        Public Property EntityTableId2 As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardPermissionStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            PId = Int64.MinValue
            EntityIdFirst = Int64.MinValue
            EntityIdSecond = Int64.MinValue
            PermissionTypeId = Int64.MinValue
            RelationActive = Boolean.FalseString
        End Sub

        Public Sub New(YourPId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64, YourPermissionTypeId As Int64, YourRelationActive As Boolean)
            MyBase.New(YourPId, String.Empty)
            PId = YourPId
            EntityIdFirst = YourEntityIdFirst
            EntityIdSecond = YourEntityIdSecond
            PermissionTypeId = YourPermissionTypeId
            RelationActive = YourRelationActive
        End Sub

        Public Property PId As Int64
        Public Property EntityIdFirst As Int64
        Public Property EntityIdSecond As Int64
        Public Property PermissionTypeId As Int64
        Public Property RelationActive As Boolean

    End Class

    Public Class R2CoreInstansePermissionsManager

        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Function ExistPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select * from R2Primary.dbo.TblPermissions as Permissions 
                       Where Permissions.PermissionTypeId=" & YourPermissionTypeId & " and Permissions.RelationActive=1 and Permissions.EntityIdFirst=" & YourEntityIdFirst & " and Permissions.EntityIdSecond=" & YourEntityIdSecond & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreMClassPermissionsManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Shared Sub RegisteringPermissions(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdsSecond As String())
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
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Shared Function ExistPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64) As Boolean
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
        '              "Select * from R2Primary.dbo.TblPermissions as Permissions 
        '               Where Permissions.PermissionTypeId=" & YourPermissionTypeId & " and Permissions.RelationActive=1 and Permissions.EntityIdFirst=" & YourEntityIdFirst & " and Permissions.EntityIdSecond=" & YourEntityIdSecond & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
        '            Return False
        '        Else
        '            Return True
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    Namespace Exceptions
        Public Class PermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

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

        Private InstanseSqlDataBOX = New R2CoreSqlDataBOXManager(New R2DateTimeService)

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
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class


End Namespace
