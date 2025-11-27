

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.LoggingManagement
Imports R2Core.PublicProc
Imports R2Core.RawGroups
Imports R2Core.SoftwareUserManagement
Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports System.Web.Services.Protocols
Imports System.Web.UI
Imports System.Xml

Namespace ConfigurationManagement

    Public MustInherit Class R2CoreConfigurations
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property DigitsFontInformation As Int64 = 1
        Public Shared ReadOnly Property GrdsCellColor As Int64 = 2
        Public Shared ReadOnly Property JPGBitmap As Int64 = 3
        Public Shared ReadOnly Property EsfandRooz As Int64 = 4
        Public Shared ReadOnly Property DocumentsPath As Int64 = 5
        Public Shared ReadOnly Property RFIDCardReadersType As Int64 = 6
        Public Shared ReadOnly Property RFIDCardReaderSetting As Int64 = 7
        Public Shared ReadOnly Property RFIDCardNotConfirmMessage As Int64 = 8
        Public Shared ReadOnly Property FrmcDialogMessage As Int64 = 9
        Public Shared ReadOnly Property RFIDCardDefultBuffer As Int64 = 10
        Public Shared ReadOnly Property SmsSystemSetting As Int64 = 11
        Public Shared ReadOnly Property ProgrammerDebugFlag As Int64 = 21
        Public Shared ReadOnly Property Suprema As Int64 = 24
        Public Shared ReadOnly Property Dermalog As Int64 = 25
        Public Shared ReadOnly Property VerifyIdentifyFPUCEnable As Int64 = 27
        Public Shared ReadOnly Property SaveFingerPrintsPath As Int64 = 32
        Public Shared ReadOnly Property FirstPageColor As Int64 = 42
        Public Shared ReadOnly Property ApplicationDomainDisplayTitle As Int64 = 43
        Public Shared ReadOnly Property SystemDisplayTitle As Int64 = 44
        Public Shared ReadOnly Property UCProcessGroup As Int64 = 45
        Public Shared ReadOnly Property UCProcessColor As Int64 = 46
        Public Shared ReadOnly Property PersonnelAttendanceSystem As Int64 = 47
        Public Shared ReadOnly Property InvalidRFIDCards As Int64 = 49
        Public Shared ReadOnly Property UCUCComputerMessageCollection As Int64 = 50
        Public Shared ReadOnly Property GovernmentVat As Int64 = 60
        Public Shared ReadOnly Property MonetaryCreditSupplySources As Int64 = 65
        Public Shared ReadOnly Property MonetarySettingTools As Int64 = 66
        Public Shared ReadOnly Property AttachedPoses As Int64 = 67
        Public Shared ReadOnly Property R2PrimaryAutomatedJobsSetting As Int64 = 70
        Public Shared ReadOnly Property SoftwareUserTypesAccessMobileProcesses As Int64 = 72
        Public Shared ReadOnly Property SoftwareUserTypesRelationMobileProcessGroups As Int64 = 73
        Public Shared ReadOnly Property PublicMessagesforSoftWareUsers As Int64 = 74
        Public Shared ReadOnly Property DefaultConfigurationOfSoftwareUserSecurity As Int64 = 76
        Public Shared ReadOnly Property PublicSecurityConfiguration As Int64 = 77
        Public Shared ReadOnly Property SqlInjectionPrevention As Int64 = 78
        Public Shared ReadOnly Property ZarrinPalPaymentGate As Int64 = 79
        Public Shared ReadOnly Property SoftwareUserTypesAccessWebProcesses As Int64 = 80
        Public Shared ReadOnly Property SoftwareUserTypesRelationWebProcessGroups As Int64 = 81
        Public Shared ReadOnly Property EmailSystem As Int64 = 83
        Public Shared ReadOnly Property ShepaPaymentGate As Int64 = 84
        Public Shared ReadOnly Property SiteIsBusy As Int64 = 87
        Public Shared ReadOnly Property LoggingAutomatedJobsSetting As Int64 = 88
        Public Shared ReadOnly Property AqayepardakhtPaymentGate As Int64 = 94
        Public Shared ReadOnly Property Carousels As Int64 = 96
        Public Shared ReadOnly Property Caching As Int64 = 97


    End Class

    Public Class R2CoreStandardConfigurationStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            CId = 0
            CName = String.Empty
            CValue = String.Empty
            Orientation = String.Empty
            Description = String.Empty
        End Sub

        Public Sub New(ByVal YourCId As Int64, ByVal YourCNamee As String, ByVal YourCValue As String, ByVal YourOrientation As String, YourDescription As String)
            MyBase.New(YourCId, YourCNamee)
            CId = YourCId
            CName = YourCNamee
            CValue = YourCValue
            Orientation = YourOrientation
            Description = YourDescription
        End Sub

        Public Property CId As Int64

        Public Property CName As String

        Public Property CValue As String

        Public Property Orientation As String

        Public Property Description As String

    End Class

    Public Class R2CoreInstanceConfigurationManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub


        Public Function GetConfigInt64(YourCId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigBoolean(YourCId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourIndex).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64) As String
            Try
                Return GetConfig(YourCId).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourIndex As Int64, YourDisposeCounter As Int64) As Object
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", YourDisposeCounter, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public MustInherit Class R2CoreMClassConfigurationManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Shared Function GetConfigOnLine(YourCId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Ds.Tables(0).Rows(0).Item("CValue").trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigRealTime(YourCId As Int64, YourIndex As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then Throw New GetDataException
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 2000, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfig(YourCId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigBoolean(YourCId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourIndex).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64) As String
            Try
                Return GetConfig(YourCId).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt32(YourCId As Int64, YourIndex As Int64) As Int32
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigInt64(YourCId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Shared myMainDatabaseName As String = Nothing
        Private Shared myDefaultConnectionString As String = Nothing
        Private Shared mySubscriptionDBConnectionString As String = Nothing
        Private Shared myComputerCode As String


        Public Shared Sub FillPublicVariables()
            Try
                Dim fs As New System.IO.FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\Timciens.txt", IO.FileMode.Open, IO.FileAccess.Read)
                Dim sr As New System.IO.StreamReader(fs)
                sr.BaseStream.Seek(0, IO.SeekOrigin.Begin)
                Dim FirstLine = sr.ReadLine
                myComputerCode = Convert.ToInt64(Mid(FirstLine, 3, 3))
                Dim DSSubscriptionDB = Split(Mid(FirstLine, 8, 20), "$")(0)
                Dim PasswordSubscriptionDB = Split(Mid(FirstLine, 26, 100), "$")(0)
                myMainDatabaseName = Split(Mid(sr.ReadLine, 31, 100), "$")(0)
                Dim DSTemp = Split(Mid(sr.ReadLine, 15, 100), "$")(0)
                Dim PasswordTemp = Split(Mid(sr.ReadLine, 4, 100), "$")(0)
                myDefaultConnectionString = "Data Source=@DS;Initial Catalog=@IC;Persist Security Info=True;User ID=sa;Password=@Password".Replace("@DS", DSTemp).Replace("@Password", PasswordTemp)
                mySubscriptionDBConnectionString = "Data Source=@DS;Initial Catalog=@IC;Persist Security Info=True;User ID=sa;Password=@Password".Replace("@DS", DSSubscriptionDB).Replace("@Password", PasswordSubscriptionDB)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Shared Function GetMainDatabaseName() As String
            Try
                If myMainDatabaseName = Nothing Then FillPublicVariables()
                '    myMainDatabaseName = GetAppConfigValue(ApplicationRegistryType.Publiq, "MainDatabaseName", "R2PrimaryMainDataBaseName")
                '    If myMainDatabaseName Is Nothing Then
                '        Throw New Exception("اشکال در محتوای رجیستری")
                '    End If
                'End If
                Return myMainDatabaseName
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetDefaultConnectionString() As String
            Try
                If myDefaultConnectionString = Nothing Then FillPublicVariables()
                '    myDefaultConnectionString = GetAppConfigValue(ApplicationRegistryType.Publiq, "DefaultConnectionString", "DefaultConnectionString")
                '    If myDefaultConnectionString Is Nothing Then
                '        Throw New Exception("اشکال در محتوای رجیستری")
                '    End If
                'End If
                Return myDefaultConnectionString
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetSubscriptionDBConnectionString() As String
            Try
                If mySubscriptionDBConnectionString = Nothing Then FillPublicVariables()
                Return mySubscriptionDBConnectionString
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Shared Function GetComputerCode() As String
            Try
                If myComputerCode = Nothing Then FillPublicVariables()
                '    myComputerCode = R2CoreMClassConfigurationManagement.GetAppConfigValue(ApplicationRegistryType.Publiq, "ComputerCode", "ComputerCode")
                '    If myComputerCode Is Nothing Then
                '        Throw New Exception("اشکال در محتوای رجیستری")
                '    End If
                'End If
                Return myComputerCode
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigurations() As List(Of R2CoreStandardConfigurationStructure)
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblConfigurations Order by CId", 1, Ds, New Boolean)
                Dim Lst As List(Of R2CoreStandardConfigurationStructure) = New List(Of R2CoreStandardConfigurationStructure)
                For Loopx As Int64 = Ds.Tables(0).Rows.Count - 1 To 0 Step -1
                    Lst.Add(New R2CoreStandardConfigurationStructure(Ds.Tables(0).Rows(Loopx).Item("CId"), Ds.Tables(0).Rows(Loopx).Item("CName").trim, Ds.Tables(0).Rows(Loopx).Item("CValue").trim, Ds.Tables(0).Rows(Loopx).Item("Orientation").trim, Ds.Tables(0).Rows(Loopx).Item("Description").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub SetConfiguration(YourNSS As R2CoreStandardConfigurationStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurations Set CValue = '" & YourNSS.CValue & "' Where CId=" & YourNSS.CId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub SetConfiguration(YourCId As Int64, YourIndex As Int64, YourCValue As String)
            Try
                Dim CurrentConfigValue As String = GetConfigOnLine(YourCId)
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
                SetConfiguration(New R2CoreStandardConfigurationStructure(YourCId, Nothing, SB.ToString(), Nothing, Nothing))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    'BPTChanged
    Public Class R2CoreConfigurationsManager


        Private R2PrimaryFSWS = New R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetConfigBoolean(YourCId As Int64, YourIndex As Int64) As Boolean
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigInt64(YourCId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourIndex).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigString(YourCId As Int64) As String
            Try
                Return GetConfig(YourCId).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Ds.Tables(0).Rows(0).Item("CValue")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigurations() As String
            Try
                Dim DS As DataSet
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select CId,ltrim(rtrim(CTitle)) as CTitle,ltrim(rtrim(CValue)) as CValue,ltrim(rtrim(Description)) as Description from R2Primary.dbo.TblConfigurations as [Configurations] Order By CId for JSON Path", 0, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigurationOfMachines() As String
            Try
                Dim DS As DataSet
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select ConfigurationsOfComputers.CId,ltrim(rtrim([Configurations].CTitle)) as CTitle,ConfigurationsOfComputers.MId as MId,ltrim(rtrim(Computers.[MName])) as MTitle,ltrim(rtrim(ConfigurationsOfComputers.CValue)) as CValue,ltrim(rtrim([Configurations].Description)) as Description 
                    from R2Primary.dbo.TblConfigurationsOfComputers as ConfigurationsOfComputers
                        Inner Join R2Primary.dbo.TblConfigurations as [Configurations] On ConfigurationsOfComputers.CId=[Configurations].CId 
	                    Inner Join R2Primary.dbo.TblComputers as Computers On ConfigurationsOfComputers.MId=Computers.MId 
                    Order By ConfigurationsOfComputers.CId,ConfigurationsOfComputers.MId 
                    for JSON Path", 0, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigurationOfAnnouncementGroups() As String
            Try
                Dim DS As DataSet
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select ConfigurationsOfAnnouncements.CId,ltrim(rtrim([Configurations].CTitle)) as CTitle,ConfigurationsOfAnnouncements.AHId as AnnouncementGroupId,ltrim(rtrim(Announcements.AnnouncementTitle)) as AnnouncementTitle,ltrim(rtrim(ConfigurationsOfAnnouncements.CValue)) as CValue,ltrim(rtrim([Configurations].Description)) as Description 
                    from R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements as ConfigurationsOfAnnouncements
                         Inner Join R2Primary.dbo.TblConfigurations as [Configurations] On ConfigurationsOfAnnouncements.CId=[Configurations].CId 
	                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On ConfigurationsOfAnnouncements.AHId=Announcements.AnnouncementId  
                    Order By ConfigurationsOfAnnouncements.CId,ConfigurationsOfAnnouncements.AHId
                    for JSON Path", 0, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function GetConfigurationHelper(YourCId As Int64, YourSoftwareUser As R2CoreSoftwareUser) As Byte()
        '    Try
        '        Return R2PrimaryFSWS.WebMethodGetFile(R2CoreRawGroups.ConfigurationHelpers, YourCId.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 0), R2PrimaryFSWS.WebMethodLogin(YourSoftwareUser.UserShenaseh, YourSoftwareUser.UserPassword))
        '    Catch ex As SoapException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Sub SetConfigurations(YourCId As Int64, YourCValue As String)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try

                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurations Set CValue='" & YourCValue & "' Where CId=" & YourCId & ""
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SetConfigurationOfMachines(YourCId As Int64, YourMachineId As Int64, YourCValue As String)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try

                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurationsOfComputers Set CValue='" & YourCValue & "' Where CId=" & YourCId & " and MId=" & YourMachineId & ""
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Sub SetConfigurationOfAnnouncementGroups(YourCId As Int64, YourAnnouncementId As Int64, YourCValue As String)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Set CValue='" & YourCValue & "' Where CId=" & YourCId & " and AHId=" & YourAnnouncementId & ""
                CmdSql.Connection.Open()
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

    End Class

    'BPTChanged
    Public MustInherit Class R2CoreConfigurationManagement

        Private Shared _DateTimeService As New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetConfigString(YourCId As Int64, YourIndex As Int64) As String
            Try
                Return GetConfig(YourCId, YourIndex).trim
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreAPIHelperManager

        Public Shared APIDateTime As HttpClient = Nothing

        Public Function GetAPIDateTime() As HttpClient
            Try
                If APIDateTime IsNot Nothing Then Return APIDateTime
                Return GetHttpClient("APIDateTime")
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Function GetHttpClient(YourTargetAPIName As String) As HttpClient
            Try
                'XML Config File
                Dim Doc = New XmlDocument()
                Dim APPFolder = AppDomain.CurrentDomain.BaseDirectory
                Dim FileName = "Configuration.Config"
                Dim FilePath = Path.Combine(APPFolder, FileName)
                If File.Exists(FilePath) Then
                    Doc.Load(FilePath)
                Else
                    Throw New FileNotExistException(FilePath)
                End If
                Dim TargetAPI As XmlNode = Doc.SelectSingleNode("/Configurations/APIs/" + YourTargetAPIName)
                Dim Uri = TargetAPI("URI").InnerText

                'Http Client
                Dim baseUri = New Uri(Uri)
                Dim HttpClient = New HttpClient()
                HttpClient.BaseAddress = baseUri
                HttpClient.DefaultRequestHeaders.Clear()
                HttpClient.DefaultRequestHeaders.ConnectionClose = False
                HttpClient.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                Try
                    ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = 60 * 1000
                Catch ex As Exception
                End Try
                Return HttpClient
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function


    End Class

    'BPTChanged
    Public MustInherit Class R2CoreConectionStringsManager

        'XML Config File
        Private Shared Doc As XmlDocument = Nothing
        Private Shared Sub PrepareDoc()
            Try
                If Doc Is Nothing Then
                    Dim APPFolder = AppDomain.CurrentDomain.BaseDirectory
                    Dim FileName = "Configuration.Config"
                    Dim FilePath = Path.Combine(APPFolder, FileName)
                    If File.Exists(FilePath) Then
                        Doc = New XmlDocument
                        Doc.Load(FilePath)
                    Else
                        Throw New FileNotExistException(FilePath)
                    End If
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Private Shared Function GetTransactionDBConnectionString(YourDBName As String) As String
            Try
                Dim SMS As XmlNode = Nothing
                Do While SMS Is Nothing
                    PrepareDoc()
                    SMS = Doc.SelectSingleNode("/Configurations/ConnectionStrings/" + YourDBName)
                Loop
                Return SMS("TransactionConnectionString").InnerText
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared Function GetSubscriptionDBConnectionString(YourDBName As String) As String
            Try
                Dim SMS As XmlNode = Nothing
                Do While SMS Is Nothing
                    PrepareDoc()
                    SMS = Doc.SelectSingleNode("/Configurations/ConnectionStrings/" + YourDBName)
                Loop
                SMS = Doc.SelectSingleNode("/Configurations/ConnectionStrings/" + YourDBName)
                Return SMS("SubscriptionConnectionString").InnerText
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared R2PrimarySMSSystemTransactionDBConnectionString = String.Empty
        Private Shared R2PrimarySMSSystemSubscriptionDBConnectionString = String.Empty
        Public Shared Function GetR2PrimarySMSSystemConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If R2PrimarySMSSystemTransactionDBConnectionString = String.Empty Then
                        R2PrimarySMSSystemTransactionDBConnectionString = GetTransactionDBConnectionString("R2PrimarySMSSystem")
                    End If
                    Return R2PrimarySMSSystemTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If R2PrimarySMSSystemSubscriptionDBConnectionString = String.Empty Then
                        R2PrimarySMSSystemSubscriptionDBConnectionString = GetSubscriptionDBConnectionString("R2PrimarySMSSystem")
                    End If
                    Return R2PrimarySMSSystemSubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared R2PrimaryTransactionDBConnectionString = String.Empty
        Private Shared R2PrimarySubscriptionDBConnectionString = String.Empty
        Public Shared Function GetR2PrimaryConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If R2PrimaryTransactionDBConnectionString = String.Empty Then
                        R2PrimaryTransactionDBConnectionString = GetTransactionDBConnectionString("R2Primary")
                    End If
                    Return R2PrimaryTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If R2PrimarySubscriptionDBConnectionString = String.Empty Then
                        R2PrimarySubscriptionDBConnectionString = GetSubscriptionDBConnectionString("R2Primary")
                    End If
                    Return R2PrimarySubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared DBTransportTransactionDBConnectionString = String.Empty
        Private Shared DBTransportSubscriptionDBConnectionString = String.Empty
        Public Shared Function GetDBTransportConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If DBTransportTransactionDBConnectionString = String.Empty Then
                        DBTransportTransactionDBConnectionString = GetTransactionDBConnectionString("DBTransport")
                    End If
                    Return DBTransportTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If DBTransportSubscriptionDBConnectionString = String.Empty Then
                        DBTransportSubscriptionDBConnectionString = GetSubscriptionDBConnectionString("DBTransport")
                    End If
                    Return DBTransportSubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared R2PrimaryParkingSystemTransactionDBConnectionString = String.Empty
        Private Shared R2PrimaryParkingSystemSubscriptionDBConnectionString = String.Empty
        Public Shared Function GetR2PrimaryParkingSystemConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If R2PrimaryParkingSystemTransactionDBConnectionString = String.Empty Then
                        R2PrimaryParkingSystemTransactionDBConnectionString = GetTransactionDBConnectionString("R2PrimaryParkingSystem")
                    End If
                    Return R2PrimaryParkingSystemTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If R2PrimaryParkingSystemSubscriptionDBConnectionString = String.Empty Then
                        R2PrimaryParkingSystemSubscriptionDBConnectionString = GetSubscriptionDBConnectionString("R2PrimaryParkingSystem")
                    End If
                    Return R2PrimaryParkingSystemSubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared R2PrimaryTransportationAndLoadNotificationTransactionDBConnectionString = String.Empty
        Private Shared R2PrimaryTransportationAndLoadNotificationSubscriptionDBConnectionString = String.Empty
        Public Shared Function GetR2PrimaryTransportationAndLoadNotificationConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If R2PrimaryTransportationAndLoadNotificationTransactionDBConnectionString = String.Empty Then
                        R2PrimaryTransportationAndLoadNotificationTransactionDBConnectionString = GetTransactionDBConnectionString("R2PrimaryTransportationAndLoadNotification")
                    End If
                    Return R2PrimaryTransportationAndLoadNotificationTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If R2PrimaryTransportationAndLoadNotificationSubscriptionDBConnectionString = String.Empty Then
                        R2PrimaryTransportationAndLoadNotificationSubscriptionDBConnectionString = GetSubscriptionDBConnectionString("R2PrimaryTransportationAndLoadNotification")
                    End If
                    Return R2PrimaryTransportationAndLoadNotificationSubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Private Shared TDBClientTransactionDBConnectionString = String.Empty
        Private Shared TDBClientSubscriptionDBConnectionString = String.Empty
        Public Shared Function GetTDBClientConnectionString(YourDBType As R2PrimaryDBType) As String
            Try
                If YourDBType = R2PrimaryDBType.TransactionDB Then
                    If TDBClientTransactionDBConnectionString = String.Empty Then
                        TDBClientTransactionDBConnectionString = GetTransactionDBConnectionString("TDBClient")
                    End If
                    Return TDBClientTransactionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.SubscriptionDB Then
                    If TDBClientSubscriptionDBConnectionString = String.Empty Then
                        TDBClientSubscriptionDBConnectionString = GetSubscriptionDBConnectionString("TDBClient")
                    End If
                    Return TDBClientSubscriptionDBConnectionString
                ElseIf YourDBType = R2PrimaryDBType.None Then
                    Throw New Exception("DBType Invalid")
                Else
                    Throw New Exception("DBType Invalid")
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class


End Namespace

Namespace GeneralConfiguration

    Public MustInherit Class R2CoreGeneralConfigurations
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Pictures As Int64 = 1


    End Class

    Public Class R2CoreRawGeneralConfiguration
        Public CId As Int64
        Public CTitle As String
        Public CName As String
        Public CIndex As Int64
        Public CIndexTitle As String
        Public CValue As String
        Public Description As String
    End Class

    Public Class R2CoreGeneralConfigurationManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllOfConfigurations(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select CId,CTitle,CName,CIndex,CIndexTitle,CValue,Description from R2Primary.dbo.TblGeneralConfiguration Where Active=1
                         Order By CId,CIndex for Json Auto")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select CId,CTitle,CName,CIndex,CIndexTitle,CValue,Description from R2Primary.dbo.TblGeneralConfiguration 
                         Order By CId,CIndex for Json Auto", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub GeneralConfigurationEditing(YourRawGeneralConfigurationCId As R2CoreRawGeneralConfiguration)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblGeneralConfiguration Set CValue='" & YourRawGeneralConfigurationCId.CValue & "' Where CId=" & YourRawGeneralConfigurationCId.CId & " and CIndex=" & YourRawGeneralConfigurationCId.CIndex & ""
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

Namespace ConfigurationOfDevices

    Public MustInherit Class R2CoreConfigurationsOfDevices
        Public Shared ReadOnly Property None As Int64 = 0


    End Class

    Public Class R2CoreRawConfigurationOfDevice
        Public CODId As Int64
        Public CODName As String
        Public CODTitle As String
        Public DeviceId As Int64
        Public DeviceTitle As String
        Public CODIndex As Int64
        Public CODIndexTitle As String
        Public Description As String
        Public CODValue As String
    End Class

    Public Class R2CoreConfigurationOfDevicesManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllConfigurationOfDevices(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select ConfigurationOfDevices.CODId,ConfigurationOfDevices.CODName,ConfigurationOfDevices.CODTitle,ConfigurationOfDevices.CODIndex,ConfigurationOfDevices.CODIndexTitle,ConfigurationOfDevices.DeviceId,Devices.DeviceTitle,ConfigurationOfDevices.Description,ConfigurationOfDevices.CODValue 
                         from R2Primary.dbo.TblConfigurationOfDevices as ConfigurationOfDevices
                           Inner Join R2Primary.dbo.TblDevices as Devices On  ConfigurationOfDevices.DeviceId=Devices.DeviceId 
                         Where  ConfigurationOfDevices.Active=1
                         Order by ConfigurationOfDevices.CODId,ConfigurationOfDevices.DeviceId,ConfigurationOfDevices.CODIndex 
                         for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select ConfigurationOfDevices.CODId,ConfigurationOfDevices.CODName,ConfigurationOfDevices.CODTitle,ConfigurationOfDevices.CODIndex,ConfigurationOfDevices.CODIndexTitle,ConfigurationOfDevices.DeviceId,Devices.DeviceTitle,ConfigurationOfDevices.Description,ConfigurationOfDevices.CODValue 
                         from R2Primary.dbo.TblConfigurationOfDevices as ConfigurationOfDevices
                           Inner Join R2Primary.dbo.TblDevices as Devices On  ConfigurationOfDevices.DeviceId=Devices.DeviceId 
                         Where  ConfigurationOfDevices.Active=1
                         Order by ConfigurationOfDevices.CODId,ConfigurationOfDevices.DeviceId,ConfigurationOfDevices.CODIndex 
                         for JSON Path", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ConfigurationOfDeviceEditing(YourRawConfigurationOfDevice As R2CoreRawConfigurationOfDevice)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurationOfDevices Set CODValue='" & YourRawConfigurationOfDevice.CODValue & "' 
                                      Where CODId=" & YourRawConfigurationOfDevice.CODId & " AND DeviceId=" & YourRawConfigurationOfDevice.DeviceId & " AND CODIndex=" & YourRawConfigurationOfDevice.CODIndex & ""
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

        Public Sub ConfigurationOfDeviceRegistering(YourRawConfigurationOfDevice As R2CoreRawConfigurationOfDevice)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblConfigurationOfDevices(CODId,CODName,CODTitle,DeviceId,CODIndex,CODIndexTitle,Description,CODValue,Template,Active)
                                      Values(" & YourRawConfigurationOfDevice.CODId & ",'" & YourRawConfigurationOfDevice.CODName & "','" & YourRawConfigurationOfDevice.CODTitle & "'," & YourRawConfigurationOfDevice.DeviceId & "," & YourRawConfigurationOfDevice.CODIndex & ",'" & YourRawConfigurationOfDevice.CODIndexTitle & "','" & YourRawConfigurationOfDevice.Description & "','" & YourRawConfigurationOfDevice.CODValue & "',0,1)"
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

        Public Sub ConfigurationOfDeviceDeleting(YourRawConfigurationOfDevice As R2CoreRawConfigurationOfDevice)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2Primary.dbo.TblConfigurationOfDevices Where CODId=" & YourRawConfigurationOfDevice.CODId & " and CODIndex=" & YourRawConfigurationOfDevice.CODIndex & " and DeviceId=" & YourRawConfigurationOfDevice.DeviceId & ""
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



