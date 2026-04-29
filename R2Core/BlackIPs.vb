

Imports System.Drawing
Imports System.Reflection
Imports System.Text

Imports R2Core.BlackIPs.Exceptions
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.SQLInjectionPrevention

Namespace BlackIPs

    Public Class R2StandardBlackIPTypeStructure

        Public Sub New()
            MyBase.New()
            BlackIPTypeId = Int64.MinValue
            BlackIPName = String.Empty
            LockMinutes = Int64.MinValue
            StrategyQuery = String.Empty
            Color = Color.Black
            Description = String.Empty
            DateTimeMilladi = DateTime.Now
            DateShamsi = String.Empty
            Time = String.Empty
            Active = Boolean.FalseString
            Viewflag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourBlackIPTypeId As Int64, ByVal YourBlackIPName As String, ByVal YourLockMinutes As Int64, YourStrategyQuery As String, ByVal YourColor As Color, ByVal YourDescription As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewflag As Boolean, YourDeleted As Boolean)
            MyBase.New()
            BlackIPTypeId = YourBlackIPTypeId
            BlackIPName = YourBlackIPName
            LockMinutes = YourLockMinutes
            StrategyQuery = YourStrategyQuery
            Color = YourColor
            Description = YourDescription
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Time = YourTime
            Active = YourActive
            Viewflag = YourViewflag
            Deleted = YourDeleted
        End Sub


        Public Property BlackIPTypeId As Int64
        Public Property BlackIPName As String
        Public Property LockMinutes As Int64
        Public Property StrategyQuery As String
        Public Property Color As Color
        Public Property Description As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property Viewflag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardBlackIPStructure

        Public Sub New()
            MyBase.New()
            BlackIPId = Int64.MinValue
            BlackIP = String.Empty
            TypeId = Int64.MinValue
            LockStatus = Boolean.FalseString
            LockMinutes = Int64.MinValue
            DateTimeMilladi = DateTime.Now
            DateShamsi = String.Empty
            Time = String.Empty
            Active = Boolean.FalseString
            Viewflag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourBlackIPId As Int64, ByVal YourBlackIP As String, ByVal YourTypeId As Int64, ByVal YourLockStatus As Boolean, ByVal YourLockMinutes As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewflag As Boolean, YourDeleted As Boolean)
            MyBase.New()
            BlackIPId = YourBlackIPId
            BlackIP = YourBlackIP
            TypeId = YourTypeId
            LockStatus = YourLockStatus
            LockMinutes = YourLockMinutes
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Time = YourTime
            Active = YourActive
            Viewflag = YourViewflag
            Deleted = YourDeleted
        End Sub

        Public Property BlackIPId As Int64
        Public Property BlackIP As String
        Public Property TypeId As Int64
        Public Property LockStatus As Boolean
        Public Property LockMinutes As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property Viewflag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreInstanceBlackIPsManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Sub DoStrategyControl()
            Try
                'اجرای استراتژی ها
                Dim DsBlackIPTypes As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                    "Select BlackIPTypeId,StrategyQuery 
                     from R2Primary.dbo.TblBlackIPTypes 
                     Where Active=1 and Deleted=0", 3600, DsBlackIPTypes, New Boolean)
                For Loopx As Int64 = 0 To DsBlackIPTypes.Tables(0).Rows.Count - 1
                    Dim Ds As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, DsBlackIPTypes.Tables(0).Rows(Loopx).Item("StrategyQuery"), 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                        Dim SB As New StringBuilder
                        For Loopy As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                            SB.Append(Ds.Tables(0).Rows(Loopy).Item("BlackIP").trim).Append(";")
                        Next
                        RegisterBlackIPs(Left(SB.ToString, SB.ToString.Length - 1), DsBlackIPTypes.Tables(0).Rows(Loopx).Item("BlackIPTypeId"))
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSBlackIPType(YourBlackIPTypeId As Int64) As R2StandardBlackIPTypeStructure
            Try
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblBlackIPTypes Where BlackIPTypeId=" & YourBlackIPTypeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New BlackIPTypeNotFoundException
                Return New R2StandardBlackIPTypeStructure(YourBlackIPTypeId, DS.Tables(0).Rows(0).Item("BlackIPName").trim, DS.Tables(0).Rows(0).Item("LockMinutes"), DS.Tables(0).Rows(0).Item("StrategyQuery").trim, Color.FromName(DS.Tables(0).Rows(0).Item("Color").trim), DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As BlackIPTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Sub RegisterBlackIP(YourNSS As R2StandardBlackIPStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblBlackIPs(BlackIP,TypeId,LockStatus,LockMinutes,DateTimeMilladi,DateShamsi,Time,Active,Viewflag,Deleted) Values('" & YourNSS.BlackIP & "'," & YourNSS.TypeId & ",1," & YourNSS.LockMinutes & ",'" & _DateTimeService.GetCurrentDateTimeMilladi & "','" & _DateTimeService.GetCurrentShamsiDate() & "','" & _DateTimeService.GetCurrentTime & "',1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisterBlackIP(YourIP As String, YourTypeId As Int64)
            Try
                RegisterBlackIP(New R2StandardBlackIPStructure(0, YourIP, YourTypeId, True, GetNSSBlackIPType(YourTypeId).LockMinutes, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisterBlackIPs(YourBlackIPs As String, YourBlackIPTypeId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim NSSBlackIPTypeId = GetNSSBlackIPType(YourBlackIPTypeId)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim BlackIPs As String() = YourBlackIPs.Split(";")
                For Each BIP As String In BlackIPs
                    CmdSql.CommandText = "IF NOT EXISTS(SELECT 1 FROM R2Primary.dbo.TblBlackIPs Where BlackIP='" & BIP & "' and LockStatus=1 and DATEDIFF(minute,DateTimeMilladi,getdate())<=LockMinutes)
                                               Insert Into R2Primary.dbo.TblBlackIPs(BlackIP,TypeId,LockStatus,LockMinutes,DateTimeMilladi,DateShamsi,Time,Active,Viewflag,Deleted) Values('" & BIP & "'," & NSSBlackIPTypeId.BlackIPTypeId & ",1," & NSSBlackIPTypeId.LockMinutes & ",'" & _DateTimeService.GetCurrentDateTimeMilladi & "','" & _DateTimeService.GetCurrentShamsiDate() & "','" & _DateTimeService.GetCurrentTime & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function IsBlackIPActive(YourNSS As R2StandardBlackIPStructure)
            Try
                'فراموش نشود باید بررسی شود و فعال شود
                Return False
                'SqlInjectionPrevention
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.BlackIP)

                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select Top 1 BlackIPId from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourNSS.BlackIP & "' and LockStatus=1 and DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes", 0, DS, New Boolean).GetRecordsCount = 0 Then Return False
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AuthorizationIP(YourIP As String)
            Try
                If IsBlackIPActive(New R2StandardBlackIPStructure(0, YourIP, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)) Then Throw New AuthorizationIPIPIsBlackException
            Catch ex As AuthorizationIPIPIsBlackException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub UnLockBlackIP(YourNSS As R2StandardBlackIPStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblBlackIPs Set LockStatus=0 Where BlackIP='" & YourNSS.BlackIP & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetBlackIPBlackRecords(YourIP As String, Optional YourTypeId As Int64 = Int64.MinValue) As List(Of R2StandardBlackIPStructure)
            Try
                Dim Lst As New List(Of R2StandardBlackIPStructure)
                Dim DS As DataSet = Nothing
                If YourTypeId = Int64.MinValue Then
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select * from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourIP & "' and LockStatus=1 and 
                               DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes order By DateTimeMilladi Desc", 0, DS, New Boolean)
                Else
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select * from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourIP & "' and LockStatus=1 and TypeId=" & YourTypeId & " and 
                               DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes order By DateTimeMilladi Desc", 0, DS, New Boolean)
                End If
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS = New R2StandardBlackIPStructure(DS.Tables(0).Rows(Loopx).Item("BlackIPId"), DS.Tables(0).Rows(Loopx).Item("BlackIP").trim, DS.Tables(0).Rows(Loopx).Item("TypeId"), DS.Tables(0).Rows(Loopx).Item("LockStatus"), DS.Tables(0).Rows(Loopx).Item("LockMinutes"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class BlackIPTypeNotFoundException
            Inherits BPTException

            Public Overrides ReadOnly Property Message As String
                Get
                    'نوع آی پی سیاه با شاخص مورد نظر یافت نشد
                    Return InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.BlackIPTypeNotFound).MsgContent
                End Get
            End Property
        End Class

        Public Class AuthorizationIPIPIsBlackException
            Inherits BPTException

            Public Overrides ReadOnly Property Message As String
                Get
                    'آی پی در لیست سیاه قرار دارد
                    Return InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.IPISBlack).MsgContent
                End Get
            End Property
        End Class

    End Namespace


End Namespace
