

Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.EntityRelationManagement.Exceptions

Namespace EntityRelationManagement

    Public Enum RelationDeactiveTypes
        None = 0
        E1Deactive = 1
        E2Deactive = 2
        BothDeactive = 3
    End Enum

    Public MustInherit Class R2CoreEntityRelationTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly SoftwareUser_MobileProcessGroup As Int64 = 3
        Public Shared ReadOnly MobileProcessGroup_MobileProcess As Int64 = 4
        Public Shared ReadOnly WebProcessGroup_WebProcess As Int64 = 5
        Public Shared ReadOnly SoftwareUser_WebProcessGroup As Int64 = 6
        Public Shared ReadOnly OwnerShips_Personnels As Int64 = 7
        Public Shared ReadOnly OwnerShips_Computers As Int64 = 8
        Public Shared ReadOnly SoftwareUserType_SMSOwnerType As Int64 = 10

    End Class

    Public Class R2StandardEntityRelationTypeStructure

        Public Sub New()
            MyBase.New()
            _ERTypeId = Int64.MinValue
            _ERTypeName = String.Empty
            _ERTypeTitle = String.Empty
            _Color = Color.Empty
            _Core = String.Empty
            _UserId = Int64.MinValue
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourERTypeId As Int64, YourERTypeName As String, YourERTypeTitle As String, YourColor As Color, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New()
            _ERTypeId = YourERTypeId
            _ERTypeName = YourERTypeName
            _ERTypeTitle = YourERTypeTitle
            _Color = YourColor
            _Core = YourCore
            _UserId = YourUserId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property ERTypeId As Int64
        Public Property ERTypeName As String
        Public Property ERTypeTitle As String
        Public Property Color As Color
        Public Property Core As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardEntityRelationStructure

        Public Sub New()
            MyBase.New()
            _ERId = Int64.MinValue
            _ERTypeId = Int64.MinValue
            _E1 = Int64.MinValue
            _E2 = Int64.MinValue
            _RelationActive = Boolean.FalseString
        End Sub

        Public Sub New(YourERId As Int64, YourERTypeId As Int64, YourE1 As Int64, YourE2 As Int64, YourRelationActive As Boolean)
            MyBase.New()
            _ERId = YourERId
            _ERTypeId = YourERTypeId
            _E1 = YourE1
            _E2 = YourE2
            _RelationActive = YourRelationActive
        End Sub

        Public Property ERId As Int64
        Public Property ERTypeId As Int64
        Public Property E1 As Int64
        Public Property E2 As Int64
        Public Property RelationActive As Boolean

    End Class

    Public Class R2CoreMClassEntityRelationManager

        Public Function RegisteringEntityRelation(YourNSSEntityRelation As R2StandardEntityRelationStructure, YourDeactive As RelationDeactiveTypes) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                If YourDeactive = RelationDeactiveTypes.E1Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourNSSEntityRelation.E1 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.E2Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E2=" & YourNSSEntityRelation.E2 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.BothDeactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where (E1=" & YourNSSEntityRelation.E1 & " or E2=" & YourNSSEntityRelation.E2 & ") and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.None Then
                End If
                CmdSql.ExecuteNonQuery()

                CmdSql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblEntityRelations') "
                Dim ERIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive) Values(" & YourNSSEntityRelation.ERTypeId & "," & YourNSSEntityRelation.E1 & "," & YourNSSEntityRelation.E2 & ",1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return ERIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreMClassEntityRelationManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Shared Function GetNSSEntityRelationType(YourEntityRelationTypeId As Int64) As R2StandardEntityRelationTypeStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblEntityRelationTypes as ERTypes Where ERTypes.ERTypeId=" & YourEntityRelationTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationTypeNotFoundException
                Dim NSS = New R2StandardEntityRelationTypeStructure
                NSS.ERTypeId = Ds.Tables(0).Rows(0).Item("ERTypeId")
                NSS.ERTypeName = Ds.Tables(0).Rows(0).Item("ERTypeName").TRIM
                NSS.ERTypeTitle = Ds.Tables(0).Rows(0).Item("ERTypeTitle").TRIM
                NSS.Color = Color.FromName(Ds.Tables(0).Rows(0).Item("Color").TRIM)
                NSS.Core = Ds.Tables(0).Rows(0).Item("Core").trim
                NSS.UserId = Ds.Tables(0).Rows(0).Item("UserId")
                NSS.DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi")
                NSS.DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi").TRIM
                NSS.ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")
                NSS.Active = Ds.Tables(0).Rows(0).Item("Active")
                NSS.Deleted = Ds.Tables(0).Rows(0).Item("Deleted")
                Return NSS
            Catch ex As EntityRelationTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSEntityRelation(YourEntityRelationId As Int64) As R2StandardEntityRelationStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblEntityRelations as ERs Where ERs.ERId=" & YourEntityRelationId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                Dim NSS = New R2StandardEntityRelationStructure
                NSS.ERId = Ds.Tables(0).Rows(0).Item("ERId")
                NSS.ERTypeId = Ds.Tables(0).Rows(0).Item("ERTypeId")
                NSS.E1 = Ds.Tables(0).Rows(0).Item("E1")
                NSS.E2 = Ds.Tables(0).Rows(0).Item("E2")
                NSS.RelationActive = Ds.Tables(0).Rows(0).Item("RelationActive")
                Return NSS
            Catch ex As EntityRelationNotFoundException

            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function RegisteringEntityRelation(YourNSSEntityRelation As R2StandardEntityRelationStructure, YourDeactive As RelationDeactiveTypes) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                'If YourDeactive = RelationDeactiveTypes.E1Deactive Then
                '    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourNSSEntityRelation.E1 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                'ElseIf YourDeactive = RelationDeactiveTypes.E2Deactive Then
                '    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E2=" & YourNSSEntityRelation.E2 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                'ElseIf YourDeactive = RelationDeactiveTypes.BothDeactive Then
                '    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where (E1=" & YourNSSEntityRelation.E1 & " or E2=" & YourNSSEntityRelation.E2 & ") and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                'ElseIf YourDeactive = RelationDeactiveTypes.None Then
                'End If
                'CmdSql.ExecuteNonQuery()

                CmdSql.CommandText = "Delete R2Primary.dbo.TblEntityRelations Where (E1=" & YourNSSEntityRelation.E1 & " or E2=" & YourNSSEntityRelation.E2 & ") and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                CmdSql.ExecuteNonQuery()

                CmdSql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblEntityRelations') "
                Dim ERIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive) Values(" & YourNSSEntityRelation.ERTypeId & "," & YourNSSEntityRelation.E1 & "," & YourNSSEntityRelation.E2 & ",1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return ERIdNew
            Catch ex As SqlException
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

        Public Shared Function GetNSSEntityRelation(YourERTypeId As Int64, YourERId1 As Int64, YourERId2 As Int64) As R2StandardEntityRelationStructure
            Try
                Dim Ds As DataSet
                If YourERId1 = Int64.MinValue Then
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E2=" & YourERId2 & " and ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                ElseIf YourERId2 = Int64.MinValue Then
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E1=" & YourERId1 & " and ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E1=" & YourERId1 & " and ERs.E2=" & YourERId2 & " ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                End If
            Catch ex As EntityRelationNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function RegisteringEntityRelations(YourEntityRelationTypeId As Int64, YourE1Id As Int64, YourE2Ids As String()) As Int64
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "Delete R2Primary.dbo.TblEntityRelations Where ERTypeId=" & YourEntityRelationTypeId & " and E1=" & YourE1Id & ""
                Cmdsql.ExecuteNonQuery()
                For Loopx As Int64 = 0 To YourE2Ids.Count - 1
                    Cmdsql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                    Cmdsql.ExecuteNonQuery()
                    Dim ERIdNew As Int64 = Cmdsql.ExecuteScalar() + 1
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive)
                                          Values(" & YourEntityRelationTypeId & "," & YourE1Id & "," & YourE2Ids(Loopx) & ",1)"
                    Cmdsql.ExecuteNonQuery()
                Next
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class EntityRelationTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع رابطه نهادی با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class EntityRelationNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "رابطه نهادی با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace
