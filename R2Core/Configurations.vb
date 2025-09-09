

Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.RawGroups
Imports R2Core.SoftwareUserManagement
Imports System.Data.SqlClient
Imports System.Reflection
Imports System.Text
Imports System.Web.Services.Protocols

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
        Public Function GetConfigInt64(YourCId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", YourDisposeCounter, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public MustInherit Class R2CoreMClassConfigurationManagement

        Public Shared Function GetConfigOnLine(YourCId As Int64) As Object
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
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
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 2000, Ds, New Boolean).GetRecordsCount = 0 Then
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
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
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
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblConfigurations Order by CId", 1, Ds, New Boolean)
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
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
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

        Public Function GetConfigInt64(YourCId As Int64, YourIndex As Int64) As Int64
            Try
                Return GetConfig(YourCId, YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New GetDataException
                End If
                Return Split(Ds.Tables(0).Rows(0).Item("CValue"), ";")(YourIndex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfig(YourCId As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                   "Select CId,ltrim(rtrim(CTitle)) as CTitle,ltrim(rtrim(CValue)) as CValue,ltrim(rtrim(Description)) as Description from R2Primary.dbo.TblConfigurations as [Configurations] Order By CId for JSON Path", 0, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetConfigurationOfMachines() As String
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
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
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
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
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try

                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurations Set CValue='" & YourCValue & "' Where CId=" & YourCId & ""
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
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
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try

                CmdSql.CommandText = "Update R2Primary.dbo.TblConfigurationsOfComputers Set CValue='" & YourCValue & "' Where CId=" & YourCId & " and MId=" & YourMachineId & ""
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
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
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try

                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblConfigurationsOfAnnouncements Set CValue='" & YourCValue & "' Where CId=" & YourCId & " and AHId=" & YourAnnouncementId & ""
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
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

        Public Shared Function GetConfig(YourCId As Int64, YourIndex As Int64) As Object
            Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 CValue from R2Primary.dbo.TblConfigurations Where CId=" & YourCId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then
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

End Namespace
