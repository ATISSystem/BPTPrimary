
Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text

Namespace ComputersManagement
    Public Class R2CoreStandardComputerStructure
        Inherits BaseStandardClass.R2StandardStructure



        Public Sub New()
            MyBase.New()
            MId = Int64.MinValue
            MName = String.Empty
            MDisplayTitle = String.Empty
            MLocation = String.Empty
            ViewFlag = Boolean.FalseString
            OActive = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourMId As Int64, ByVal YourMName As String, ByVal YourMDisplayTitle As String, ByVal YourMLocation As String, YourViewFlag As Boolean, YourOActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourMId, YourMName)
            MId = YourMId
            MName = YourMName
            MDisplayTitle = YourMDisplayTitle
            MLocation = YourMLocation
            OActive = YourOActive
            ViewFlag = YourViewFlag
            Deleted = YourDeleted
        End Sub



        Public Property MId As Int64
        Public Property MName As String
        Public Property MDisplayTitle As String
        Public Property MLocation As String
        Public Property ViewFlag As Boolean
        Public Property OActive As Boolean
        Public Property Deleted As Boolean


    End Class

    Public Class R2CoreMClassComputersManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetNSSComputer(YourMId As String) As R2CoreStandardComputerStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where MId='" & YourMId & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(0).Item("MId"), Ds.Tables(0).Rows(0).Item("MName").trim, Ds.Tables(0).Rows(0).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(0).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSCurrentComputer() As R2CoreStandardComputerStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where MId=" & R2CoreMClassConfigurationManagement.GetComputerCode() & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(0).Item("MId"), Ds.Tables(0).Rows(0).Item("MName").trim, Ds.Tables(0).Rows(0).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(0).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetComputers(YourSearchString As String) As List(Of R2StandardStructure)
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where OActive=1 and ViewFlag=1 and MDisplayTitle Like  '%" & YourSearchString & "%' Order By MId", 3600, Ds, New Boolean)
                Dim Lst As New List(Of R2StandardStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(Loopx).Item("MId"), Ds.Tables(0).Rows(Loopx).Item("MName").trim, Ds.Tables(0).Rows(Loopx).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(Loopx).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public Class R2CoreMClassComputersManagement

        Private Shared _DateTimeService As IR2DateTimeService = New R2DateTimeService
        Private Shared InstanceSqlDataBOX As R2CoreSqlDataBOXManager


        Public Shared Function GetNSSComputer(YourMId As String) As R2CoreStandardComputerStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where MId='" & YourMId & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(0).Item("MId"), Ds.Tables(0).Rows(0).Item("MName").trim, Ds.Tables(0).Rows(0).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(0).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSCurrentComputer() As R2CoreStandardComputerStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where MId=" & R2CoreMClassConfigurationManagement.GetComputerCode() & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(0).Item("MId"), Ds.Tables(0).Rows(0).Item("MName").trim, Ds.Tables(0).Rows(0).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(0).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetComputers(YourSearchString As String) As List(Of R2StandardStructure)
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblComputers Where OActive=1 and ViewFlag=1 and MDisplayTitle Like  '%" & YourSearchString & "%' Order By MId", 3600, Ds, New Boolean)
                Dim Lst As New List(Of R2StandardStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardComputerStructure(Ds.Tables(0).Rows(Loopx).Item("MId"), Ds.Tables(0).Rows(Loopx).Item("MName").trim, Ds.Tables(0).Rows(Loopx).Item("MDisplayTitle").trim, Ds.Tables(0).Rows(Loopx).Item("MLocation").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("OActive"), Ds.Tables(0).Rows(0).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public Class R2CoreStandardConfigurationOfComputerStructure
        Inherits R2CoreStandardConfigurationStructure

        Public Sub New()
            MyBase.New()
            ComId = 0
            CValue = String.Empty
        End Sub

        Public Sub New(YourNSSConfiguration As R2CoreStandardConfigurationStructure, ByVal YourComId As Int64, ByVal YourCValue As String)
            MyBase.New(YourNSSConfiguration.CId, YourNSSConfiguration.CName, YourNSSConfiguration.CValue, YourNSSConfiguration.Orientation, YourNSSConfiguration.Description)
            ComId = YourComId
            CValue = YourCValue
        End Sub

        Public Property ComId As Int64

        Public Property CValue As String

    End Class

    Public Class R2CoreMClassConfigurationOfComputersManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetConfigOnLine(YourCId As Int64, YourMId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Ds.Tables(0).Rows(0).Item("CValue").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function GetConfigRealTime(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Object
        '    Try
        '        Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
        '        Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "")
        '        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '        Ds.Tables.Clear()
        '        If Da.Fill(Ds) = 0 Then Throw New GetDataException
        '        Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Function GetConfig(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourMId, YourIndex).trim
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt32(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Int32
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigBoolean(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt64(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigDouble(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Double
            Try
                Dim xRong As Double = GetConfig(YourCId, YourMId, YourIndex)
                Return xRong
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigByte(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Byte
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function GetConfigurationOfComputer(YourMId As Int64) As List(Of R2CoreStandardConfigurationOfComputerStructure)
        '    Try
        '        Dim Ds As DataSet
        '        Dim InstanceSqlDataBOX As New R2CoreSqlDataBOXManager
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
        '            "Select Configs.CId,Configs.CName,Configs.Description,Configs.Orientation,ConfigOfComps.CValue,ConfigOfComps.MId from R2Primary.dbo.TblConfigurations as Configs 
        '               Inner Join R2Primary.dbo.TblConfigurationsOfComputers as ConfigOfComps On Configs.CId=ConfigOfComps.CId
        '             Where ConfigOfComps.MId=" & YourMId & " Order By CId", 1, Ds)
        '        Dim Lst As New List(Of R2CoreStandardConfigurationOfComputerStructure)
        '        For Loopx As Int64 = Ds.Tables(0).Rows.Count - 1 To 0 Step -1
        '            Lst.Add(New R2CoreStandardConfigurationOfComputerStructure(New R2CoreStandardConfigurationStructure(Ds.Tables(0).Rows(Loopx).Item("CId"), Ds.Tables(0).Rows(Loopx).Item("CName").trim, Ds.Tables(0).Rows(Loopx).Item("CValue").trim, Ds.Tables(0).Rows(Loopx).Item("Orientation").trim, Ds.Tables(0).Rows(Loopx).Item("Description").trim), YourMId, Ds.Tables(0).Rows(Loopx).Item("CValue").trim))
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Sub SetConfigurationOfComputer(YourNSS As R2CoreStandardConfigurationOfComputerStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurationsOfComputers Set CValue = '" & YourNSS.CValue & "' Where CId=" & YourNSS.CId & " and MId=" & YourNSS.ComId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SetConfigurationOfComputer(YourCId As Int64, YourMId As Int64, YourIndex As Int64, YourCValue As String)
            Try
                Dim CurrentConfigValue As String = GetConfigOnLine(YourCId, YourMId)
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
                SetConfigurationOfComputer(New R2CoreStandardConfigurationOfComputerStructure(New R2CoreStandardConfigurationStructure(YourCId, String.Empty, Nothing, String.Empty, String.Empty), YourMId, SB.ToString()))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    Public NotInheritable Class R2CoreMClassConfigurationOfComputersManagement
        'Inherits R2CoreMClassConfigurationManagement

        Private Shared _DateTimeService As IR2DateTimeService = New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetConfigOnLine(YourCId As Int64, YourMId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Ds.Tables(0).Rows(0).Item("CValue").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Shared Function GetConfigRealTime(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Object
        '    Try
        '        Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
        '        Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "")
        '        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection()
        '        Ds.Tables.Clear()
        '        If Da.Fill(Ds) = 0 Then Throw New GetDataException
        '        Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Shared Function GetConfig(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurationsOfComputers Where CId=" & YourCId & " and MId=" & YourMId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourMId, YourIndex).trim
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt32(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Int32
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigBoolean(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt64(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigDouble(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Double
            Try
                Dim xRong As Double = GetConfig(YourCId, YourMId, YourIndex)
                Return xRong
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigByte(YourCId As Int64, YourMId As Int64, YourIndex As Int64) As Byte
            Try
                Return GetConfig(YourCId, YourMId, YourIndex)
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigurationOfComputer(YourMId As Int64) As List(Of R2CoreStandardConfigurationOfComputerStructure)
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                    "Select Configs.CId,Configs.CName,Configs.Description,Configs.Orientation,ConfigOfComps.CValue,ConfigOfComps.MId from R2Primary.dbo.TblConfigurations as Configs 
                       Inner Join R2Primary.dbo.TblConfigurationsOfComputers as ConfigOfComps On Configs.CId=ConfigOfComps.CId
                     Where ConfigOfComps.MId=" & YourMId & " Order By CId", 3600, Ds, New Boolean)
                Dim Lst As New List(Of R2CoreStandardConfigurationOfComputerStructure)
                For Loopx As Int64 = Ds.Tables(0).Rows.Count - 1 To 0 Step -1
                    Lst.Add(New R2CoreStandardConfigurationOfComputerStructure(New R2CoreStandardConfigurationStructure(Ds.Tables(0).Rows(Loopx).Item("CId"), Ds.Tables(0).Rows(Loopx).Item("CName").trim, Ds.Tables(0).Rows(Loopx).Item("CValue").trim, Ds.Tables(0).Rows(Loopx).Item("Orientation").trim, Ds.Tables(0).Rows(Loopx).Item("Description").trim), YourMId, Ds.Tables(0).Rows(Loopx).Item("CValue").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub SetConfigurationOfComputer(YourNSS As R2CoreStandardConfigurationOfComputerStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurationsOfComputers Set CValue = '" & YourNSS.CValue & "' Where CId=" & YourNSS.CId & " and MId=" & YourNSS.ComId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub SetConfigurationOfComputer(YourCId As Int64, YourMId As Int64, YourIndex As Int64, YourCValue As String)
            Try
                Dim CurrentConfigValue As String = GetConfigOnLine(YourCId, YourMId)
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
                SetConfigurationOfComputer(New R2CoreStandardConfigurationOfComputerStructure(New R2CoreStandardConfigurationStructure(YourCId, String.Empty, Nothing, String.Empty, String.Empty), YourMId, SB.ToString()))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class



End Namespace

Namespace Devices
    Public Class R2CoreRawDevice
        Public DeviceId As Int64?
        Public DeviceTitle As String
        Public DeviceLocation As String
        Public Active As Boolean
    End Class


    Public Class R2CoreDeviceManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllDevices(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                    "Select DeviceId,DeviceTitle,DeviceLocation,Active from R2Primary.dbo.TblDevices 
                         Where ViewFlag=1 and Deleted=0 Order By DeviceId for Json Auto")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                    "Select DeviceId,DeviceTitle,DeviceLocation,Active from R2Primary.dbo.TblDevices 
                         Where ViewFlag=1 and Deleted=0 Order By DeviceId for Json Auto", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub DeviceRegistering(YourRawDevice As R2CoreRawDevice)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 DeviceId from R2Primary.dbo.TblDevices with (tablockx) order by DeviceId desc"
                CmdSql.ExecuteNonQuery()
                Dim DeviceId As Int64 = CmdSql.ExecuteScalar + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblDevices(DeviceId,DeviceTitle,DeviceLocation,Active,ViewFlag,Deleted)
                                          Values(" & DeviceId & ",'" & YourRawDevice.DeviceTitle & "','" & YourRawDevice.DeviceLocation & "'," & IIf(YourRawDevice.Active, 1, 0) & ",1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DeviceEditing(YourRawDevice As R2CoreRawDevice)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblDevices Set DeviceTitle='" & YourRawDevice.DeviceTitle & "',DeviceLocation='" & YourRawDevice.DeviceLocation & "',Active=" & IIf(YourRawDevice.Active, 1, 0) & "  Where DeviceId=" & YourRawDevice.DeviceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DeviceDeleting(YourDeviceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblDevices Set Deleted=1 Where DeviceId=" & YourDeviceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

End Namespace
