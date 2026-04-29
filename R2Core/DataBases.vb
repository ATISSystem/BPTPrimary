


Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection
Imports System.Windows.Forms

Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.LoggingManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.PredefinedMessagesManagement.Exceptions

Namespace DatabaseManagement

    'BPTChanged
    Public Class R2ClassSqlDataBOX
        Private _SqlConnection As SqlConnection
        Private _DisposeCounter As Int16
        Private _StartTime As DateTime
        Private _SqlString As String
        Private _DS As New DataSet
        Private _RecordsCount As Int64
        Private _DateTimeService As IDateTimeService

        Public Sub New()
            Try
                _DateTimeService = New R2DateTimeService
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Sub New(ByVal YourSqlCnn As SqlConnection, ByVal YourDisposeCounter As Int16, ByVal YourSqlString As String, YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                _SqlConnection = YourSqlCnn
                _DisposeCounter = YourDisposeCounter
                _StartTime = _DateTimeService.GetCurrentDateTimeMilladi.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                _SqlString = YourSqlString
                Dim Da As New SqlClient.SqlDataAdapter
                Da.SelectCommand = New SqlClient.SqlCommand(_SqlString)
                Da.SelectCommand.Connection = _SqlConnection
                _DS.Tables.Clear()
                _RecordsCount = Da.Fill(_DS)
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Sub RenewData()
            Try
                _StartTime = _DateTimeService.GetCurrentDateTimeMilladi.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                Dim Da As New SqlClient.SqlDataAdapter
                Da.SelectCommand = New SqlClient.SqlCommand(_SqlString)
                Da.SelectCommand.Connection = _SqlConnection
                _DS.Tables.Clear()
                _RecordsCount = Da.Fill(_DS)
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetRecordsCount() As Int64
            Return _RecordsCount
        End Function

        Public Function GetDS() As DataSet
            Return _DS
        End Function

        Public Function GetDisposeCounter() As Int16
            Return _DisposeCounter
        End Function

        Public Function GetStartTime() As DateTime
            Return _StartTime
        End Function

        Public Function GetSqlString() As String
            Return _SqlString
        End Function

        Public Function GetSqlConnection() As SqlClient.SqlConnection
            Return _SqlConnection
        End Function

    End Class

    'BPTChanged
    Public Enum R2PrimaryDBType
        None = 0
        TransactionDB = 1
        SubscriptionDB = 2
    End Enum

    'BPTChanged
    Public MustInherit Class R2PrimarySMSSystemSqlConnection

        Public Shared Function GetTransactionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetR2PrimarySMSSystemConnectionString(R2PrimaryDBType.TransactionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetSubscriptionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetR2PrimarySMSSystemConnectionString(R2PrimaryDBType.SubscriptionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function
    End Class

    'BPTChanged
    Public MustInherit Class R2PrimarySqlConnection

        Public Shared Function GetTransactionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetR2PrimaryConnectionString(R2PrimaryDBType.TransactionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetSubscriptionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetR2PrimaryConnectionString(R2PrimaryDBType.SubscriptionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function
    End Class

    'BPTChanged
    Public MustInherit Class TDBClientSqlConnection
        Public Shared Function GetTransactionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetTDBClientConnectionString(R2PrimaryDBType.TransactionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetSubscriptionDBConnection() As SqlConnection
            Try
                Return New SqlClient.SqlConnection(R2CoreConectionStringsManager.GetTDBClientConnectionString(R2PrimaryDBType.SubscriptionDB))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function
    End Class

    'BPTChanged
    Public NotInheritable Class R2CoreDatabaseManager

        Private Shared InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private Shared _DateTimeService As IDateTimeService

        Public Shared Function GetEquivalenceMessage(YourException As SqlException) As DataBaseException
            Try
                'When Connect to Instance Not Applicable
                If YourException.Errors(0).Number = 2 Then Throw New DataBaseException("ارتباط با بانک اطلاعات امکان پذیر نیست")

                _DateTimeService = New R2DateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)

                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select EquivalenceMessage from R2Primary.dbo.TblDatebaseErrorCodesEquivalence Where DatabaseErrorCode=" & YourException.Errors(0).Number & "", 32767, DS, New Boolean).GetRecordsCount <> 0 Then
                    Throw New DataBaseException(DS.Tables(0).Rows(0).Item("EquivalenceMessage"))
                Else
                    Throw New DataBaseEquivalenceMessageNotFoundException
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As DataBaseEquivalenceMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreSqlDataBOXManager

        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                GarbageCollector.Initialize()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetDataBOX(ByVal SqlCnn As SqlConnection, ByVal SqlString As String, ByVal DisposeCounter As Int16, ByRef DS As DataSet, ByRef DataChangeStatus As Boolean) As R2ClassSqlDataBOX
            Try
                If DisposeCounter > 0 Then
                    If GarbageCollector._Dict.ContainsKey(SqlString) Then
                        If DateDiff(DateInterval.Second, GarbageCollector._Dict(SqlString).GetStartTime, DateTime.Now) >= GarbageCollector._Dict(SqlString).GetDisposeCounter Then
                            GarbageCollector._Dict(SqlString).RenewData()
                            DataChangeStatus = True
                        Else
                            DataChangeStatus = False
                        End If
                        DS = GarbageCollector._Dict(SqlString).GetDS
                        Return GarbageCollector._Dict(SqlString)
                    Else
                        Dim SqlDataBox As R2ClassSqlDataBOX
                        Try
                            SqlDataBox = New R2ClassSqlDataBOX(SqlCnn, DisposeCounter, SqlString, _DateTimeService)
                        Catch ex As Exception
                            Throw ex
                        End Try

                        Try
                            GarbageCollector._Dict.Add(SqlString, SqlDataBox)
                        Catch ex As Exception
                        End Try

                        DataChangeStatus = True
                        DS = GarbageCollector._Dict(SqlString).GetDS
                        Return GarbageCollector._Dict(SqlString)
                    End If
                ElseIf DisposeCounter = 0 Then
                    Dim R2ClassSqlDataBOX As R2ClassSqlDataBOX
                    R2ClassSqlDataBOX = New R2ClassSqlDataBOX(SqlCnn, DisposeCounter, SqlString, _DateTimeService)
                    DS = R2ClassSqlDataBOX.GetDS
                    Return R2ClassSqlDataBOX
                Else
                    Throw New Exception("Error:YourDisposeCounter < 0")
                End If

            Catch ex As DataBaseException
                Throw ex
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public NotInheritable Class GarbageCollector
        Public Shared _Dict As Dictionary(Of String, R2ClassSqlDataBOX) = New Dictionary(Of String, R2ClassSqlDataBOX)
        Public Shared WithEvents _GarbageCollector As System.Timers.Timer = Nothing

        Public Shared Sub Initialize()
            Try
                If _Dict Is Nothing Then _Dict = New Dictionary(Of String, R2ClassSqlDataBOX)
                If _GarbageCollector Is Nothing Then
                    _GarbageCollector = New Timers.Timer(1800000) '30 Minutes
                    _GarbageCollector.Enabled = True
                    _GarbageCollector.Start()
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Private Shared Sub GarbageCollectorHandler() Handles _GarbageCollector.Elapsed
            Try
                _GarbageCollector.Stop()
                _GarbageCollector.Enabled = False
                Dim RemoveLst = New List(Of String)
                For Each Key In _Dict.Keys
                    If DateDiff(DateInterval.Second, _Dict(Key).GetStartTime, DateTime.Now) >= _Dict(Key).GetDisposeCounter + 300 Then 'After 5 Minutes
                        RemoveLst.Add(Key)
                    End If
                Next
                For Loopx As Int64 = 0 To RemoveLst.Count - 1
                    _Dict.Remove(RemoveLst(Loopx))
                Next
                _GarbageCollector.Enabled = True
                _GarbageCollector.Start()
            Catch ex As Exception
                _GarbageCollector.Enabled = True
                _GarbageCollector.Start()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    'BPTChanged
    Public Class DataBaseEquivalenceMessageNotFoundException
        Inherits BPTException

        Public Sub New()
            Try
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.SystemBugDataBaseEquivalenceMessage).MsgId
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.SystemBugDataBaseEquivalenceMessage).MsgContent
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
    End Class

    'BPTChanged
    Public Class DataBaseException
        Inherits BPTException

        Public Sub New(YourMessage As String)
            Try
                _MessageCode = R2CorePredefinedMessages.ExceptionfromDataBase
                _Message = YourMessage
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
    End Class

    'Public Enum R2OpenCloseConnectionStatus
    '    OpenCloseYes = 0
    '    OpenCloseNo = 1
    'End Enum

    'Public Class R2CoreMClassDatabaseManagement
    '    'Public Shared Function GetOLEDbConnectionString(ByVal FileName As String) As String
    '    '    Dim Builder As New OleDb.OleDbConnectionStringBuilder
    '    '    If IO.Path.GetExtension(FileName).ToUpper = ".XLS" Then
    '    '        Builder.Provider = "Microsoft.Jet.OLEDB.4.0"
    '    '        Builder.Add("Extended Properties", "Excel 8.0;IMEX=1;HDR=No;")
    '    '    Else
    '    '        Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
    '    '        Builder.Add("Extended Properties", "Excel 12.0;")
    '    '        Builder.Add("Extended Properties", "Excel 12.0;IMEX=1;HDR=No;")
    '    '    End If
    '    '    Builder.DataSource = FileName
    '    '    Return Builder.ConnectionString
    '    'End Function

    '    'Public Shared Function GetOLEDbConnectionString(ByVal FileName As String, ByVal Header As String) As String
    '    '    Dim Builder As New OleDb.OleDbConnectionStringBuilder
    '    '    If IO.Path.GetExtension(FileName).ToUpper = ".XLS" Then
    '    '        Builder.Provider = "Microsoft.Jet.OLEDB.4.0"
    '    '        Builder.Add("Extended Properties", String.Format("Excel 8.0;IMEX=1;HDR={0};", Header))
    '    '    Else
    '    '        Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
    '    '        Builder.Add("Extended Properties", String.Format("Excel 12.0;IMEX=1;HDR={0};", Header))
    '    '    End If
    '    '    Builder.DataSource = FileName
    '    '    Return Builder.ConnectionString
    '    'End Function

    '    'Public Shared Function DocPaths() As String
    '    '    Try
    '    '        Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\pic common"
    '    '    Catch ex As Exception
    '    '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '    '    End Try
    '    'End Function

    '    'Public Shared Function RptPaths() As String
    '    '    Try
    '    '        Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\rpt"
    '    '    Catch ex As Exception
    '    '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '    '    End Try
    '    'End Function

    '    'Public Shared Function IconsPath() As String
    '    '    Try
    '    '        Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\atlas icons"
    '    '    Catch ex As Exception
    '    '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '    '    End Try
    '    'End Function

    '    'Public Shared Function EmzaPaths() As String
    '    '    Try
    '    '        Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\emza secret"
    '    '    Catch ex As Exception
    '    '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '    '    End Try
    '    'End Function

    '    'Public Shared Function PersonelPicPath() As String
    '    '    Try
    '    '        Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\personelpic"
    '    '    Catch ex As Exception
    '    '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '    '    End Try
    '    'End Function

    '    'Private Shared _OpenConnection As SqlClient.SqlConnection = R2PrimarySqlConnection.GetTransactionDBConnection
    '    'Private Shared WithEvents ConnectionTimer As New System.Timers.Timer(60000)

    '    'Public Sub ForceConnectionToOpen(YourTimerInterval As Int64)
    '    '    Try
    '    '        _OpenConnection.Close()
    '    '        _OpenConnection.Open()
    '    '        ConnectionTimer.Interval = YourTimerInterval
    '    '        ConnectionTimer.Enabled = True
    '    '        ConnectionTimer.Start()
    '    '    Catch ex As Exception
    '    '        MessageBox.Show(ex.Message)
    '    '    End Try
    '    'End Sub

    '    'Public Shared Function GetOpenConnection() As SqlConnection
    '    '    Try
    '    '        If _OpenConnection.State <> ConnectionState.Closed Then Return _OpenConnection
    '    '        _OpenConnection.Open()
    '    '        ConnectionTimer.Enabled = True
    '    '        ConnectionTimer.Start()
    '    '        Return _OpenConnection
    '    '    Catch ex As Exception
    '    '        MessageBox.Show(ex.Message)
    '    '    End Try
    '    'End Function

    '    'Private Shared Sub ConnectionTimerHandler() Handles ConnectionTimer.Elapsed
    '    '    Try
    '    '        ConnectionTimer.Enabled = False
    '    '        ConnectionTimer.Stop()
    '    '        _OpenConnection.Close()
    '    '    Catch ex As Exception
    '    '        MessageBox.Show(ex.Message)
    '    '    End Try
    '    'End Sub

    'End Class

    'BPTChanged

End Namespace
