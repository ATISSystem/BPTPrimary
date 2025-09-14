


Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection
Imports System.Windows.Forms
Imports R2Core.ConfigurationManagement

Namespace DatabaseManagement

    Public Enum R2OpenCloseConnectionStatus
        OpenCloseYes = 0
        OpenCloseNo = 1
    End Enum

    Public MustInherit Class R2ClassSqlConnection

        Protected DefaultConnectionString As String = R2CoreMClassConfigurationManagement.GetDefaultConnectionString
        Protected SubscriptionDBConnectionString As String = R2CoreMClassConfigurationManagement.GetSubscriptionDBConnectionString
        Protected _Connection As SqlClient.SqlConnection = Nothing

        Public Sub New()
            Try
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetConnection() As System.Data.SqlClient.SqlConnection
            Try
                Return _Connection
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class

    Public Class R2PrimarySqlConnection
        Inherits R2ClassSqlConnection

        Public Sub New()
            MyBase.New()
            Try
                _Connection = New SqlClient.SqlConnection(DefaultConnectionString.Replace("@IC", R2CoreMClassConfigurationManagement.GetMainDatabaseName))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2PrimarySubscriptionDBSqlConnection
        Inherits R2ClassSqlConnection

        Public Sub New()
            MyBase.New()
            Try
                _Connection = New SqlClient.SqlConnection(SubscriptionDBConnectionString.Replace("@IC", R2CoreMClassConfigurationManagement.GetMainDatabaseName))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2PrimaryReportsSqlConnection
        Inherits R2ClassSqlConnection

        Public Sub New()
            MyBase.New()
            Try
                _Connection = New SqlClient.SqlConnection(DefaultConnectionString.Replace("@IC", R2CoreMClassConfigurationManagement.GetMainDatabaseName + "Reports"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2ClassSqlDataBOX
        Dim myR2ClassSqlConnection As R2ClassSqlConnection
        Dim myDisposeCounter As Int16
        Dim myStartTime As DateTime
        Dim mySqlString As String
        Dim myDS As New DataSet
        Dim myRecordsCount As Int64
        Public Sub New()
        End Sub
        Public Sub New(ByVal Sqlcnn As R2ClassSqlConnection, ByVal DisposeCounterr As Int16, ByVal SqlStringg As String)
            Try
                myR2ClassSqlConnection = Sqlcnn
                myDisposeCounter = DisposeCounterr
                myStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                mySqlString = SqlStringg
                'پركردن ديتاست
                Dim da As New SqlClient.SqlDataAdapter
                da.SelectCommand = New SqlClient.SqlCommand(mySqlString)
                da.SelectCommand.Connection = myR2ClassSqlConnection.GetConnection()
                'da.SelectCommand.Connection = R2MClassDatabaseManagement.GetOpenConnection()
                myDS.Tables.Clear()
                myRecordsCount = da.Fill(myDS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Sub RenewData()
            Try
                myStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
                Dim da As New SqlClient.SqlDataAdapter
                da.SelectCommand = New SqlClient.SqlCommand(mySqlString)
                da.SelectCommand.Connection = myR2ClassSqlConnection.GetConnection
                myDS.Tables.Clear()
                myRecordsCount = da.Fill(myDS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetRecordsCount() As Int64
            Return myRecordsCount
        End Function

        Public Function GetDS() As DataSet
            Return myDS
        End Function

        Public Function DisposeCounter() As Int16
            Return myDisposeCounter
        End Function

        Public Function StartTime() As DateTime
            Return myStartTime
        End Function

        Public Function SqlString() As String
            Return mySqlString
        End Function

        Public Function SqlConnection() As SqlClient.SqlConnection
            Return myR2ClassSqlConnection.GetConnection
        End Function

    End Class

    'Public Class R2CoreInstanseSqlDataBOXManager
    '    Private yourSqlcnn As R2ClassSqlConnection = Nothing
    '    Private yourSqlString As String = Nothing
    '    Private yourDisposeCounter As Int16 = Nothing

    '    Public Function GetDataBOX(ByVal Sqlcnn As R2ClassSqlConnection, ByVal SqlString As String, ByVal DisposeCounter As Int16, ByRef DS As DataSet, ByRef DataChangeStatus As Boolean) As R2ClassSqlDataBOX
    '        Try
    '            yourSqlcnn = Sqlcnn
    '            yourSqlString = SqlString : yourDisposeCounter = DisposeCounter
    '            Dim myIndex As Int32 = R2ClassSqlDataBOXManagement.myList.FindIndex(AddressOf FindDataBox)
    '            Dim myR2ClassSqlDataBOX As R2ClassSqlDataBOX
    '            If yourDisposeCounter > 0 Then
    '                If myIndex >= 0 Then
    '                    Dim myCurrentDateTime As DateTime = Date.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
    '                    If (R2ClassSqlDataBOXManagement.myList.Item(myIndex).DisposeCounter <> 0) And (DateAndTime.DateDiff(DateInterval.Second, R2ClassSqlDataBOXManagement.myList.Item(myIndex).StartTime, myCurrentDateTime) >= R2ClassSqlDataBOXManagement.myList.Item(myIndex).DisposeCounter) Then
    '                        R2ClassSqlDataBOXManagement.myList.Item(myIndex).RenewData()
    '                        DataChangeStatus = True
    '                    Else
    '                        DataChangeStatus = False
    '                    End If
    '                    DS = R2ClassSqlDataBOXManagement.myList.Item(myIndex).GetDS
    '                    Return R2ClassSqlDataBOXManagement.myList.Item(myIndex)
    '                Else
    '                    myR2ClassSqlDataBOX = New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString)
    '                    DataChangeStatus = True
    '                    R2ClassSqlDataBOXManagement.myList.Add(myR2ClassSqlDataBOX)
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return myR2ClassSqlDataBOX
    '                End If
    '            ElseIf yourDisposeCounter = 0 Then
    '                myR2ClassSqlDataBOX = New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString)
    '                DataChangeStatus = True
    '                If myIndex >= 0 Then
    '                    R2ClassSqlDataBOXManagement.myList.Item(myIndex) = Nothing
    '                    R2ClassSqlDataBOXManagement.myList.Item(myIndex) = myR2ClassSqlDataBOX
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return R2ClassSqlDataBOXManagement.myList.Item(myIndex)
    '                Else
    '                    R2ClassSqlDataBOXManagement.myList.Add(myR2ClassSqlDataBOX)
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return myR2ClassSqlDataBOX
    '                End If
    '            ElseIf yourDisposeCounter < 0 Then
    '                Throw New Exception("Error:yourDisposeCounter < 0")
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function

    '    Private Function FindDataBox(ByVal DataBox As R2ClassSqlDataBOX) As Boolean
    '        Try
    '            If DataBox.SqlString = yourSqlString And DataBox.SqlConnection.ConnectionString = yourSqlcnn.GetConnection().ConnectionString Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function

    'End Class

    'Public NotInheritable Class R2ClassSqlDataBOXManagement
    '    Public Shared myList As New Generic.List(Of R2ClassSqlDataBOX)
    '    Private Shared yourSqlcnn As R2ClassSqlConnection = Nothing
    '    Private Shared yourSqlString As String = Nothing
    '    Private Shared yourDisposeCounter As Int16 = Nothing

    '    Public Shared Function GetDataBOX(ByVal Sqlcnn As R2ClassSqlConnection, ByVal SqlString As String, ByVal DisposeCounter As Int32, ByRef DS As DataSet, ByRef DataChangeStatus As Boolean) As R2ClassSqlDataBOX
    '        Try
    '            yourSqlcnn = Sqlcnn
    '            yourSqlString = SqlString : yourDisposeCounter = DisposeCounter
    '            Dim myIndex As Int32 = myList.FindIndex(AddressOf FindDataBox)
    '            Dim myR2ClassSqlDataBOX As R2ClassSqlDataBOX
    '            If yourDisposeCounter > 0 Then
    '                If myIndex >= 0 Then
    '                    Dim myCurrentDateTime As DateTime = Date.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
    '                    If (myList.Item(myIndex).DisposeCounter <> 0) And (DateAndTime.DateDiff(DateInterval.Second, myList.Item(myIndex).StartTime, myCurrentDateTime) >= myList.Item(myIndex).DisposeCounter) Then
    '                        myList.Item(myIndex).RenewData()
    '                        DataChangeStatus = True
    '                    Else
    '                        DataChangeStatus = False
    '                    End If
    '                    DS = myList.Item(myIndex).GetDS
    '                    Return myList.Item(myIndex)
    '                Else
    '                    myR2ClassSqlDataBOX = New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString)
    '                    DataChangeStatus = True
    '                    myList.Add(myR2ClassSqlDataBOX)
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return myR2ClassSqlDataBOX
    '                End If
    '            ElseIf yourDisposeCounter = 0 Then
    '                myR2ClassSqlDataBOX = New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString)
    '                DataChangeStatus = True
    '                If myIndex >= 0 Then
    '                    myList.Item(myIndex) = Nothing
    '                    myList.Item(myIndex) = myR2ClassSqlDataBOX
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return myList.Item(myIndex)
    '                Else
    '                    myList.Add(myR2ClassSqlDataBOX)
    '                    DS = myR2ClassSqlDataBOX.GetDS
    '                    Return myR2ClassSqlDataBOX
    '                End If
    '            ElseIf yourDisposeCounter < 0 Then
    '                Throw New Exception("Error:yourDisposeCounter < 0")
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function

    '    Private Shared Function FindDataBox(ByVal DataBox As R2ClassSqlDataBOX) As Boolean
    '        Try
    '            If DataBox.SqlString = yourSqlString And DataBox.SqlConnection.ConnectionString = yourSqlcnn.GetConnection().ConnectionString Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function

    'End Class

    Public Class R2CoreMClassDatabaseManagement
        Public Shared Function GetOLEDbConnectionString(ByVal FileName As String) As String
            Dim Builder As New OleDb.OleDbConnectionStringBuilder
            If IO.Path.GetExtension(FileName).ToUpper = ".XLS" Then
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0"
                Builder.Add("Extended Properties", "Excel 8.0;IMEX=1;HDR=No;")
            Else
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
                Builder.Add("Extended Properties", "Excel 12.0;")
                Builder.Add("Extended Properties", "Excel 12.0;IMEX=1;HDR=No;")
            End If
            Builder.DataSource = FileName
            Return Builder.ConnectionString
        End Function

        Public Shared Function GetOLEDbConnectionString(ByVal FileName As String, ByVal Header As String) As String
            Dim Builder As New OleDb.OleDbConnectionStringBuilder
            If IO.Path.GetExtension(FileName).ToUpper = ".XLS" Then
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0"
                Builder.Add("Extended Properties", String.Format("Excel 8.0;IMEX=1;HDR={0};", Header))
            Else
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0"
                Builder.Add("Extended Properties", String.Format("Excel 12.0;IMEX=1;HDR={0};", Header))
            End If
            Builder.DataSource = FileName
            Return Builder.ConnectionString
        End Function

        Public Shared Function DocPaths() As String
            Try
                Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\pic common"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function RptPaths() As String
            Try
                Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\rpt"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function IconsPath() As String
            Try
                Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\atlas icons"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function EmzaPaths() As String
            Try
                Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\emza secret"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function PersonelPicPath() As String
            Try
                Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.DocumentsPath, 0) + "\personelpic"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared _OpenConnection As SqlClient.SqlConnection = (New R2PrimarySqlConnection).GetConnection
        Private Shared WithEvents ConnectionTimer As New System.Timers.Timer(60000)

        Public Sub ForceConnectionToOpen(YourTimerInterval As Int64)
            Try
                _OpenConnection.Close()
                _OpenConnection.Open()
                ConnectionTimer.Interval = YourTimerInterval
                ConnectionTimer.Enabled = True
                ConnectionTimer.Start()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Public Shared Function GetOpenConnection() As SqlConnection
            Try
                If _OpenConnection.State <> ConnectionState.Closed Then Return _OpenConnection
                _OpenConnection.Open()
                ConnectionTimer.Enabled = True
                ConnectionTimer.Start()
                Return _OpenConnection
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Function

        Private Shared Sub ConnectionTimerHandler() Handles ConnectionTimer.Elapsed
            Try
                ConnectionTimer.Enabled = False
                ConnectionTimer.Stop()
                _OpenConnection.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

    End Class

    'BPTChanged

    Public NotInheritable Class R2CoreDatabaseManager
        Public Sub New()

        End Sub

        Public Shared Function GetEquivalenceMessage(YourException As SqlException) As DataBaseException
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select EquivalenceMessage from R2Primary.dbo.TblDatebaseErrorCodesEquivalence Where DatabaseErrorCode=" & YourException.Errors(0).Number & "", 32767, DS, New Boolean).GetRecordsCount <> 0 Then
                    Throw New DataBaseException(DS.Tables(0).Rows(0).Item("EquivalenceMessage"))
                Else
                    Throw New DataBaseEquivalenceMessageNotFoundException
                End If
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

        Public Sub New()
            GarbageCollector.Initialize()
        End Sub

        Public Function GetDataBOX(ByVal Sqlcnn As R2ClassSqlConnection, ByVal SqlString As String, ByVal DisposeCounter As Int16, ByRef DS As DataSet, ByRef DataChangeStatus As Boolean) As R2ClassSqlDataBOX
            Try
                If DisposeCounter > 0 Then
                    If GarbageCollector._Dict.ContainsKey(SqlString) Then
                        If DateDiff(DateInterval.Second, GarbageCollector._Dict(SqlString).StartTime, DateTime.Now) >= GarbageCollector._Dict(SqlString).DisposeCounter Then
                            GarbageCollector._Dict(SqlString).RenewData()
                            DataChangeStatus = True
                        Else
                            DataChangeStatus = False
                        End If
                        DS = GarbageCollector._Dict(SqlString).GetDS
                        Return GarbageCollector._Dict(SqlString)
                    Else
                        GarbageCollector._Dict.Add(SqlString, New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString))
                        DataChangeStatus = True
                        DS = GarbageCollector._Dict(SqlString).GetDS
                        Return GarbageCollector._Dict(SqlString)
                    End If
                ElseIf DisposeCounter = 0 Then
                    Dim R2ClassSqlDataBOX As R2ClassSqlDataBOX
                    R2ClassSqlDataBOX = New R2ClassSqlDataBOX(Sqlcnn, DisposeCounter, SqlString)
                    DS = R2ClassSqlDataBOX.GetDS
                    Return R2ClassSqlDataBOX
                Else
                    Throw New Exception("Error:YourDisposeCounter < 0")
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public NotInheritable Class GarbageCollector
        Public Shared _Dict As Dictionary(Of String, R2ClassSqlDataBOX) = Nothing
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
                    If DateDiff(DateInterval.Second, _Dict(Key).StartTime, DateTime.Now) >= _Dict(Key).DisposeCounter + 300 Then 'After 5 Minutes
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
        Inherits ApplicationException

        Public Overrides ReadOnly Property Message As String
            Get
                Return "خطای سیستم - معادلی برای خطای بازگشتی از بانک اطلاعات یافت نشد"
            End Get
        End Property
    End Class

    'BPTChanged
    Public Class DataBaseException
        Inherits ApplicationException

        Private _Message As String
        Public Sub New(YourMessage As String)
            _Message = YourMessage
        End Sub

        Public Overrides ReadOnly Property Message As String
            Get
                Return _Message
            End Get
        End Property
    End Class


End Namespace
