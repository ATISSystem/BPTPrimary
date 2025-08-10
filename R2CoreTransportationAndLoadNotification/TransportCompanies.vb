
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.EntityRelationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.LoggingManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PermissionManagement
Imports R2Core.PermissionManagement.Exceptions
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.PublicProc
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SiteIsBusy
Imports R2Core.SMS.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.WebProcessesManagement.Exceptions
Imports R2CoreGUI
Imports R2CoreParkingSystem.BlackList
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreParkingSystem.ProvincesAndCities.Execption
Imports R2CoreParkingSystem.Drivers
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.Announcements.Exceptions
Imports R2CoreTransportationAndLoadNotification.AnnouncementTiming
Imports R2CoreTransportationAndLoadNotification.CalendarManagement.SpecializedPersianCalendar
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.DriverSelfDeclaration.Exceptions
Imports R2CoreTransportationAndLoadNotification.FileShareRawGroupsManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.FailedLoadAllocationPrinting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorAccounting
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadOtherThanManipulation
Imports R2CoreTransportationAndLoadNotification.LoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces
Imports R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission
Imports R2CoreTransportationAndLoadNotification.LoadPermission.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadPermission.LoadPermissionPrinting
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation
Imports R2CoreTransportationAndLoadNotification.LoadSedimentation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadSources
Imports R2CoreTransportationAndLoadNotification.LoadSources.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadTargets
Imports R2CoreTransportationAndLoadNotification.LoadTargets.Exceptions
Imports R2CoreTransportationAndLoadNotification.Logging
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.RequesterManagement
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportCompanies
Imports R2CoreTransportationAndLoadNotification.TransportCompanies.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportTariffs
Imports R2CoreTransportationAndLoadNotification.TransportTariffs.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckLoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness
Imports R2CoreTransportationAndLoadNotification.TurnAttendance
Imports R2CoreTransportationAndLoadNotification.TurnAttendance.Exceptions
Imports R2CoreTransportationAndLoadNotification.TurnCancellation
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports R2CoreTransportationAndLoadNotification.TravelTime
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions



Namespace TransportCompanies

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _TCId = Int64.MinValue
            _TCTitle = String.Empty
            _TCOrganizationCode = String.Empty
            _TCCityId = Int64.MinValue
            _TCColor = String.Empty
            _TCTel = String.Empty
            _TCManagerNameFamily = String.Empty
            _TCManagerMobileNumber = String.Empty
            _EmailAddress = String.Empty
            _ViewFlag = False
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourTCId As Int64, YourTCTitle As String, YourTCOrganizationCode As String, YourTCCityId As Int64, YourTColor As String, YourTCTel As String, YourTCManagerNameFamily As String, YourTCManagerMobileNumber As String, YourEmailAddress As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourTCId, YourTCTitle)
            _TCId = YourTCId
            _TCTitle = YourTCTitle
            _TCOrganizationCode = YourTCOrganizationCode
            _TCCityId = YourTCCityId
            _TCColor = YourTColor
            _TCTel = YourTCTel
            _TCManagerNameFamily = YourTCManagerNameFamily
            _TCManagerMobileNumber = YourTCManagerMobileNumber
            _EmailAddress = YourEmailAddress
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Function GetColorFromARGB() As Color
            Try
                Return Color.FromArgb(Split(TCColor, ";")(0), Split(TCColor, ";")(1), Split(TCColor, ";")(2), Split(TCColor, ";")(3))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Property TCId As Int64
        Public Property TCTitle As String
        Public Property TCOrganizationCode As String
        Public Property TCCityId As Int64
        Public Property TCColor As String
        Public Property TCTel As String
        Public Property TCManagerNameFamily As String
        Public Property TCManagerMobileNumber As String
        Public Property EmailAddress As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager

        Private _R2DateTimeService As New R2DateTimeService

        Public Function ISTransportCompanyActive(YourNSSTransportCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure) As Boolean
            Dim InstanceqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Active from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCId = " & YourNSSTransportCompany.TCId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return DS.Tables(0).Rows(0).Item("Active")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTransportCompany(YourTransportCompanyId As Int64, YourImmediate As Boolean) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCId=" & YourTransportCompanyId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCId=" & YourTransportCompanyId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(0).Item("TCId"), DS.Tables(0).Rows(0).Item("TCTitle"), DS.Tables(0).Rows(0).Item("TCOrganizationCode"), DS.Tables(0).Rows(0).Item("TCCityId"), DS.Tables(0).Rows(0).Item("TCColor").trim, DS.Tables(0).Rows(0).Item("TCTel").trim, DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(0).Item("EmailAddress").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTransportCompany(YourNSSMoneyWallet As R2CoreParkingSystemStandardTrafficCardStructure) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 TransportCompanies.TCId,TransportCompanies.TCTitle,TransportCompanies.TCOrganizationCode,TransportCompanies.TCCityId,TransportCompanies.TCColor,TransportCompanies.TCTel,
                                     TransportCompanies.TCManagerNameFamily,TransportCompanies.TCManagerMobileNumber,TransportCompanies.ViewFlag,TransportCompanies.Active,TransportCompanies.Deleted,TransportCompanies.EmailAddress 
                        from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TCRMoneyWallets On MoneyWallets.CardId=TCRMoneyWallets.CardId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TCRMoneyWallets.TransportCompanyId=TransportCompanies.TCId 
                        Where MoneyWallets.Active=1 and TCRMoneyWallets.RelationActive=1 and TransportCompanies.Deleted=0 and MoneyWallets.CardId=" & YourNSSMoneyWallet.CardId & "
                        Order By MoneyWallets.CardId Desc,TransportCompanies.TCId Desc", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(0).Item("TCId"), DS.Tables(0).Rows(0).Item("TCTitle"), DS.Tables(0).Rows(0).Item("TCOrganizationCode"), DS.Tables(0).Rows(0).Item("TCCityId"), DS.Tables(0).Rows(0).Item("TCColor").trim, DS.Tables(0).Rows(0).Item("TCTel").trim, DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(0).Item("EmailAddress").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSMoneyWalletforTransportCompany(YourNSSTransportCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure) As R2CoreParkingSystemStandardTrafficCardStructure
            Try
                Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 MoneyWallets.CardId from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TCRMoneyWallets On MoneyWallets.CardId=TCRMoneyWallets.CardId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TCRMoneyWallets.TransportCompanyId=TransportCompanies.TCId 
                        Where MoneyWallets.Active=1 and TCRMoneyWallets.RelationActive=1 and TransportCompanies.Deleted=0 and TransportCompanies.TCId=" & YourNSSTransportCompany.TCId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New MoneyWalletNotExistException
                Return InstanceTrafficCards.GetNSSTrafficCard(Convert.ToInt64(DS.Tables(0).Rows(0).Item("CardId")))
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub RegisteringTransportCompanyMoneyWalletRelation(YourNSSTransportCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure, YourMoneyWallet As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Try
                    Cmdsql.Connection.Open()
                    Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                    Cmdsql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets Set RelationActive=0 Where CardId=" & YourMoneyWallet.CardId & " or TransportCompanyId=" & YourNSSTransportCompany.TCId & ""
                    Cmdsql.ExecuteNonQuery()
                    Cmdsql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets(CardId,TransportCompanyId,RelationActive) Values(" & YourMoneyWallet.CardId & "," & YourNSSTransportCompany.TCId & ",1)"
                    Cmdsql.ExecuteNonQuery()
                    Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
                Catch ex As Exception
                    If Cmdsql.Connection.State <> ConnectionState.Closed Then
                        Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                    End If
                    Throw New Exception(ex.Message)
                End Try
                Try
                    Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
                    Dim NSSMoneyWallet = InstanceTrafficCards.GetNSSTrafficCard(System.Convert.ToInt64(YourMoneyWallet.CardId))
                    NSSMoneyWallet.Pelak = String.Empty
                    NSSMoneyWallet.Serial = String.Empty
                    NSSMoneyWallet.UserIdEdit = YourNSSUser.UserId
                    NSSMoneyWallet.NoMoney = False
                    NSSMoneyWallet.Active = True
                    NSSMoneyWallet.CompanyName = YourNSSTransportCompany.TCTitle
                    NSSMoneyWallet.NameFamily = YourNSSTransportCompany.TCManagerNameFamily
                    NSSMoneyWallet.Mobile = YourNSSTransportCompany.TCManagerMobileNumber
                    NSSMoneyWallet.Tel = YourNSSTransportCompany.TCTel
                    NSSMoneyWallet.Tahvilg = YourNSSTransportCompany.TCManagerNameFamily
                    NSSMoneyWallet.CardType = TerafficCardType.Tereili
                    NSSMoneyWallet.TempCardType = TerafficTempCardType.NoTemp
                    InstanceTrafficCards.UpdatingTrafficCard(NSSMoneyWallet, R2Core.R2Enums.EditLevel.HighLevel)
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTransportCompnay(YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                 "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRUser On TransportCompanies.TCId=TCRUser.TCId
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On TCRUser.UserId=SoftwareUsers.UserId
                          Where SoftwareUsers.UserId=" & YourNSSUser.UserId & " and RelationActive=1", 0, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(0).Item("TCId"), DS.Tables(0).Rows(0).Item("TCTitle"), DS.Tables(0).Rows(0).Item("TCOrganizationCode"), DS.Tables(0).Rows(0).Item("TCCityId"), DS.Tables(0).Rows(0).Item("TCColor").trim, DS.Tables(0).Rows(0).Item("TCTel").trim, DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(0).Item("EmailAddress").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTransportCompany(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Top 1 TransportCompanies.*  From dbtransport.dbo.tbElam as Loads
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                         Where Loads.nEstelamID=" & YourNSSLoadCapacitorLoad.nEstelamId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(0).Item("TCId"), DS.Tables(0).Rows(0).Item("TCTitle"), DS.Tables(0).Rows(0).Item("TCOrganizationCode"), DS.Tables(0).Rows(0).Item("TCCityId"), DS.Tables(0).Rows(0).Item("TCColor").trim, DS.Tables(0).Rows(0).Item("TCTel").trim, DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(0).Item("EmailAddress").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TransportCompanyChangeActiveStatus(YourNSS As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                Dim TCSoftwareusers = InstanceTransportCompanies.GetNSSTCSoftwareusers(YourNSS.TCId, True)
                Dim TCNSS = InstanceTransportCompanies.GetNSSTransportCompany(YourNSS.TCId, True)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies 
                                      Set Active=" & IIf(TCNSS.Active, 0, 1) & "
                                      Where TCId=" & YourNSS.TCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update dbtransport.dbo.tbCompany 
                                      Set Active=" & IIf(TCNSS.Active, 0, 1) & "
                                      Where nCompCode =" & YourNSS.TCId & ""
                CmdSql.ExecuteNonQuery()
                For Loopx As Int64 = 0 To TCSoftwareusers.Count - 1
                    CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserActive=" & IIf(TCNSS.Active, 0, 1) & " Where Userid=" & TCSoftwareusers(Loopx).UserId & ""
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                SendSMSTransportCompanyChangeActiveStatus(TCNSS.TCTitle, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not TCNSS.Active))

            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared Sub SendSMSTransportCompanyChangeActiveStatus(YourTCTitle As String, YourStatus As String)
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceMoneyWalletManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                'کنترل فعال بودن سرویس اس ام اس
                If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 6).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'کانتنت پیام
                Dim myData = New SMSCreationData
                myData.Data1 = YourTCTitle
                myData.Data2 = YourStatus
                'ارسال اس ام اس
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotificationSMSTypes.TransportCompanyChangeActiveStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New LoadingAndDischargingSendSMSFailedException
            Catch ex As LoadingAndDischargingSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTCSoftwareuser(YourTransportCompanyId As Int64, YourImmediate As Boolean) As R2CoreStandardSoftwareUserStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & " and TCRelationSoftwareUsers.RelationActive=1")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & " and TCRelationSoftwareUsers.RelationActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Return InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(DS.Tables(0).Rows(0).Item("UserId")))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTCSoftwareusers(YourTransportCompanyId As Int64, YourImmediate As Boolean) As List(Of R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("select SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & " and TCRelationSoftwareUsers.RelationActive=1")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "select SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & " and TCRelationSoftwareUsers.RelationActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Dim Lst As New List(Of R2CoreStandardSoftwareUserStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("UserId"))))
                Next
                Return Lst
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTransportCompaniesManagement

        Public Shared Function RegisteringTransportCompany(YourNSS As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                If YourNSS.TCTitle.Trim = String.Empty Or YourNSS.TCOrganizationCode.Trim = String.Empty Or YourNSS.TCTel.Trim = String.Empty Or YourNSS.TCManagerNameFamily.Trim = String.Empty Or YourNSS.TCManagerMobileNumber.Trim = String.Empty Then
                    Throw New DataEntryException
                End If
                'ایجاد کاربر مرتبط به شرکت حمل و نقل
                Dim myUserId = R2CoreMClassSoftwareUsersManagement.RegisteringSoftwareUser(New R2CoreStandardSoftwareUserStructure(Nothing, Nothing, Nothing, YourNSS.TCTitle, Nothing, Nothing, Nothing, String.Empty, True, True, R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany, YourNSS.TCManagerMobileNumber, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, YourUserNSS.UserId, Nothing, Nothing, True, Nothing), YourUserNSS)
                'ثبت شرکت حمل و نقل
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 TCId  from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies with (tablockx) Order By TCId Desc " : CmdSql.ExecuteNonQuery()
                Dim myTCId As Int64 = CmdSql.ExecuteScalar + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies(TCId,TCTitle,TCOrganizationCode,TCCityId,TCColor,TCTel,TCManagerNameFamily,TCManagerMobileNumber,EmailAddress,ViewFlag,Active,Deleted) Values(" & myTCId & ",'" & YourNSS.TCTitle & "','" & YourNSS.TCOrganizationCode & "'," & YourNSS.TCCityId & ",'200;200;60;50','" & YourNSS.TCTel & "','" & YourNSS.TCManagerNameFamily & "','" & YourNSS.TCManagerMobileNumber & "','" & YourNSS.EmailAddress & "'," & IIf(YourNSS.ViewFlag, 1, 0) & "," & IIf(YourNSS.Active, 1, 0) & ",0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into dbtransport.dbo.tbCompany(nCompCode,strCompName,nCompCityCode,Active,ViewFlag,Deleted) Values(" & myTCId & ",'" & YourNSS.TCTitle & "'," & YourNSS.TCCityId & "," & IIf(YourNSS.Active, 1, 0) & "," & IIf(YourNSS.ViewFlag, 1, 0) & ",0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers(TCId,UserId,RelationActive) Values(" & myTCId & "," & myUserId & ",1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'به دست آوردن لیست فرآیندهای موبایلی قابل دسترسی برای نوع کاربر شرکت حمل و نقل و ارسال به مدیریت مجوز
                Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany.ToString + ":"
                Dim AllofProcessGroupsIds As String()
                Dim AllofProcessesIds As String()
                Dim AllofSoftwareUserTypes As String() = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessMobileProcesses), ";")
                If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                    AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessMobileProcesses, myUserId, AllofProcessesIds)
                End If
                'به دست آوردن لیست گروههای فرآیند موبایلی برای نوع کاربر شرکت حمل و نقل و ارسال آن به مدیریت روابط نهادی
                AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationMobileProcessGroups), ";")
                If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                    AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_MobileProcessGroup, myUserId, AllofProcessGroupsIds)
                End If
                'به دست آوردن لیست فرآیندهای وب قابل دسترسی برای نوع کاربر شرکت حمل و نقل و ارسال به مدیریت مجوز
                ComposeSearchString = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany.ToString + ":"
                AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                    AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, myUserId, AllofProcessesIds)
                End If
                'به دست آوردن لیست گروههای فرآیند وب برای نوع کاربر شرکت حمل و نقل و ارسال آن به مدیریت روابط نهادی
                AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                    AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                    R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, myUserId, AllofProcessGroupsIds)
                End If
                Return myTCId
            Catch ex As SoftwareUserMobileNumberAlreadyExistException
                Throw ex
            Catch ex As DataEntryException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub UpdatingTransportCompany(YourNSS As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                If YourNSS.TCTitle.Trim = String.Empty Or YourNSS.TCOrganizationCode.Trim = String.Empty Or YourNSS.TCTel.Trim = String.Empty Or YourNSS.TCManagerNameFamily.Trim = String.Empty Or YourNSS.TCManagerMobileNumber.Trim = String.Empty Then
                    Throw New DataEntryException
                End If
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies 
                                      Set TCTitle='" & YourNSS.TCTitle & "',TCOrganizationCode='" & YourNSS.TCOrganizationCode & "',TCCityId=" & YourNSS.TCCityId & ",TCTel='" & YourNSS.TCTel & "',TCManagerNameFamily='" & YourNSS.TCManagerNameFamily & "',TCManagerMobileNumber='" & YourNSS.TCManagerMobileNumber & "',EmailAddress='" & YourNSS.EmailAddress & "',ViewFlag=" & IIf(YourNSS.ViewFlag, 1, 0) & ",Active=" & IIf(YourNSS.Active, 1, 0) & "
                                      Where TCId=" & YourNSS.TCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update dbtransport.dbo.tbCompany 
                                      Set strCompName='" & YourNSS.TCTitle & "',nCompCityCode=" & YourNSS.TCCityId & ",ViewFlag=" & IIf(YourNSS.ViewFlag, 1, 0) & ",Active=" & IIf(YourNSS.Active, 1, 0) & "
                                      Where nCompCode =" & YourNSS.TCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Dim NSSUser As R2CoreStandardSoftwareUserStructure
                Try
                    Dim InstanceSoftwareUsers = New R2CoreTransportationAndLoadNotificationInstanceSoftwareUsersManager
                    NSSUser = InstanceSoftwareUsers.GetNSSSoftwareUser(YourNSS.TCId)
                    R2CoreMClassSoftwareUsersManagement.EditingSoftwareUser(New R2CoreStandardSoftwareUserStructure(NSSUser.UserId, Nothing, Nothing, YourNSS.TCTitle, Nothing, Nothing, Nothing, NSSUser.UserPinCode, NSSUser.UserCanCharge, YourNSS.Active, NSSUser.UserTypeId, YourNSS.TCManagerMobileNumber, NSSUser.UserStatus, String.Empty, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, YourNSSUser.UserId, Nothing, Nothing, NSSUser.ViewFlag, NSSUser.Deleted), YourNSSUser)
                Catch ex As SoftwareUserRelatedThisTransportCompanyNotFoundException
                    'ایجاد کاربر مرتبط به شرکت حمل و نقل
                    Dim myUserId = R2CoreMClassSoftwareUsersManagement.RegisteringSoftwareUser(New R2CoreStandardSoftwareUserStructure(Nothing, Nothing, Nothing, YourNSS.TCTitle, Nothing, Nothing, Nothing, String.Empty, True, True, R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany, YourNSS.TCManagerMobileNumber, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, YourNSSUser.UserId, Nothing, Nothing, True, Nothing), YourNSSUser)
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers(TCId,UserId,RelationActive) Values(" & YourNSS.TCId & "," & myUserId & ",1)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    'به دست آوردن لیست فرآیندهای موبایلی قابل دسترسی برای نوع کاربر شرکت حمل و نقل و ارسال به مدیریت مجوز
                    Dim AllofProcessesIds As String()
                    Dim AllofProcessGroupsIds As String()
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany.ToString + ":"
                    Dim AllofSoftwareUserTypes As String() = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessMobileProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessMobileProcesses, myUserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند موبایلی برای نوع کاربر شرکت حمل و نقل و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationMobileProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_MobileProcessGroup, myUserId, AllofProcessGroupsIds)
                    End If
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی برای نوع کاربر شرکت حمل و نقل و ارسال به مدیریت مجوز
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, myUserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب برای نوع کاربر شرکت حمل و نقل و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, myUserId, AllofProcessGroupsIds)
                    End If
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                End Try
            Catch ex As SoftwareUserMobileNumberAlreadyExistException
                Throw ex
            Catch ex As DataEntryException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub DeletingTransportCompany(YourTCId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Set ViewFlag=0,Active=0,Deleted=1 Where TCId=" & YourTCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update dbtransport.dbo.tbCompany Set ViewFlag=0,Active=0,Deleted=1 Where nCompCode=" & YourTCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetTransportCompanies_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
            Try
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where Left(TCTitle," & YourSearchString.Length & ")='" & YourSearchString & "' and Deleted=0 and ViewFlag=1 Order By TCTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(Loopx).Item("TCId"), DS.Tables(0).Rows(Loopx).Item("TCTitle"), DS.Tables(0).Rows(Loopx).Item("TCOrganizationCode"), DS.Tables(0).Rows(Loopx).Item("TCCityId"), DS.Tables(0).Rows(Loopx).Item("TCColor").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("EmailAddress").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetTransportCompanies_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
            Try
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCTitle Like '%" & YourSearchString & "%' and Deleted=0 and ViewFlag=1 Order By TCTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(Loopx).Item("TCId"), DS.Tables(0).Rows(Loopx).Item("TCTitle"), DS.Tables(0).Rows(Loopx).Item("TCOrganizationCode"), DS.Tables(0).Rows(Loopx).Item("TCCityId"), DS.Tables(0).Rows(Loopx).Item("TCColor").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("EmailAddress").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetTransportCompaniesFullOfWork(YourViewFlagControlStatus As Boolean, YourActiveControlStatus As Boolean, YourWorkDate1 As R2StandardDateAndTimeStructure, YourWorkDate2 As R2StandardDateAndTimeStructure, YourAHId As Int64, YourAHSGId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
            Try
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure)
                Dim SqlString As String = "Select TransportCompanies.* from (Select nCompCode,Sum(nCarNumKol) as Suming from dbtransport.dbo.tbElam Where (dDateElam>='" & YourWorkDate1.DateShamsiFull & "') and (dDateElam<='" & YourWorkDate2.DateShamsiFull & "') and AHId=" & YourAHId & " and AHSGId=" & YourAHSGId & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " Group By nCompCode) as Companies
                                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Companies.nCompCode=TransportCompanies.TCId
                                           Where Deleted=0"
                If YourViewFlagControlStatus Then SqlString = SqlString + " and ViewFlag=1"
                If YourActiveControlStatus Then SqlString = SqlString + " and Active=1"
                SqlString = SqlString + " Order By Companies.Suming Desc"
                Dim DS As DataSet = Nothing
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, SqlString, 1, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(Loopx).Item("TCId"), DS.Tables(0).Rows(Loopx).Item("TCTitle"), DS.Tables(0).Rows(Loopx).Item("TCOrganizationCode"), DS.Tables(0).Rows(Loopx).Item("TCCityId"), DS.Tables(0).Rows(Loopx).Item("TCColor").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(Loopx).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("EmailAddress").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTransportCompanyByOrganizationId(YourTransportCompanyOrganizationId As Int64) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCOrganizationCode=" & YourTransportCompanyOrganizationId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(DS.Tables(0).Rows(0).Item("TCId"), DS.Tables(0).Rows(0).Item("TCTitle"), DS.Tables(0).Rows(0).Item("TCOrganizationCode"), DS.Tables(0).Rows(0).Item("TCCityId"), DS.Tables(0).Rows(0).Item("TCColor").trim, DS.Tables(0).Rows(0).Item("TCTel").trim, DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, DS.Tables(0).Rows(0).Item("EmailAddress").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function ISTransportCompanyActive(YourNSSTransportCompany As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure) As Boolean
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Active from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCId = " & YourNSSTransportCompany.TCId & "", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return DS.Tables(0).Rows(0).Item("Active")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function ExistComputerInTranportCompanies(YourComputerId As Int64) As Boolean
            Try
                Dim DS As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select ComId from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationComputers Where ComId=" & YourComputerId & " and RelationActive=1", 1, DS, New Boolean).GetRecordsCount = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTransportCompanyFullOfWorkCompany(YourWorkDate1 As R2StandardDateAndTimeStructure, YourWorkDate2 As R2StandardDateAndTimeStructure, YourAHId As Int64, YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure
            Try
                Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 nCompCode from (Select nCompCode,Sum(nCarNumKol) as Suming from dbtransport.dbo.tbElam Where (dDateElam>='" & YourWorkDate1.DateShamsiFull & "') and (dDateElam<='" & YourWorkDate2.DateShamsiFull & "') and AHId=" & YourAHId & " and AHSGId=" & YourAHSGId & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented & " and LoadStatus<>" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow & " Group By nCompCode) as Companies  Order By Companies.Suming Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return InstanceTransportCompanies.GetNSSTransportCompany(DS.Tables(0).Rows(0).Item("nCompCode"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Namespace Exceptions

        Public Class TransportCompanyISNotActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کد شرکت حمل و نقل مورد نظر غیرفعال است"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class TransportCompanyNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات شرکت حمل و نقل یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public Class RawTransportCompany
        Public TCId As Int64
        Public TCTitle As String
        Public TCOrganizationCode As String
        Public TCCityTitle As String
        Public TCTel As String
        Public TCManagerMobileNumber As String
        Public TCManagerNameFamily As String
        Public EmailAddress As String
        Public Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportCompaniesManager

        Private _DateTimeService As New R2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        'BPTChanged
        Public Function HasTransportCompanyMoneyWallet(YourTransportCompanyId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select Top 1 MoneyWallets.CardId from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TCRMoneyWallets On MoneyWallets.CardId=TCRMoneyWallets.CardId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TCRMoneyWallets.TCId=TransportCompanies.TCId 
                        Where MoneyWallets.Active=1 and TransportCompanies.Deleted=0 and TransportCompanies.TCId=" & YourTransportCompanyId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportCompanies_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                               TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	               TransportCompanies.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                           Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                        Where TCTitle Like N'%" & YourSearchString & "%' and TransportCompanies.Deleted=0
                        Order By TCTitle
                        for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                       "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                               TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	               TransportCompanies.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                           Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                        Where TCTitle Like N'%" & YourSearchString & "%' and TransportCompanies.Deleted=0
                        Order By TCTitle
                        for json path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportCompanyJSON(YourTCId As Int64, YourImmediate As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                                                        TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	                                        TransportCompanies.Active as Active
                                                            From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                                                              Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                                                       Where TCId=" & YourTCId & " and TransportCompanies.Deleted=0
                                                       Order By TCTitle
                                                       for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                       "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                               TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	               TransportCompanies.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                           Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                        Where TCId=" & YourTCId & " and TransportCompanies.Deleted=0
                        Order By TCTitle
                        for json path", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New TransportCompanyNotFoundException
                    End If

                End If

                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportCompany(YourTransportCompanyId As Int64, YourImmediately As Boolean) As RawTransportCompany
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                                                           TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	                                           TransportCompanies.Active as Active
                                                               From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                                                                 Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                                                       Where TCId=" & YourTransportCompanyId & " and TransportCompanies.Deleted=0
                                                       Order By TCTitle")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                                     "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                                                           TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
            	                                           TransportCompanies.Active as Active
                                                               From R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                                                                 Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                                                       Where TCId=" & YourTransportCompanyId & " and TransportCompanies.Deleted=0
                                                       Order By TCTitle", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Return New RawTransportCompany With {.TCId = DS.Tables(0).Rows(0).Item("TCId"), .TCTitle = DS.Tables(0).Rows(0).Item("TCTitle").trim, .TCOrganizationCode = DS.Tables(0).Rows(0).Item("TCOrganizationCode").trim, .TCCityTitle = DS.Tables(0).Rows(0).Item("TCCityTitle").trim, .TCTel = DS.Tables(0).Rows(0).Item("TCTel").trim, .TCManagerMobileNumber = DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, .TCManagerNameFamily = DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, .EmailAddress = DS.Tables(0).Rows(0).Item("EmailAddress").trim, .Active = DS.Tables(0).Rows(0).Item("Active")}
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub EditTransportCompany(YourTransportCompany As RawTransportCompany)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, Nothing)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies 
                                      Set TCTel='" & YourTransportCompany.TCTel & "',TCManagerNameFamily='" & YourTransportCompany.TCManagerNameFamily & "',TCManagerMobileNumber='" & YourTransportCompany.TCManagerMobileNumber & "',EmailAddress='" & YourTransportCompany.EmailAddress & "'
                                      Where TCId=" & YourTransportCompany.TCId & ""
                CmdSql.ExecuteNonQuery()

                Dim UserId As Int64 = Int64.MinValue
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourTransportCompany.TCManagerMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourTransportCompany.TCManagerMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers
                                          Where UserId=" & UserId & " or TCId=" & YourTransportCompany.TCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers(TCId,UserId) Values(" & YourTransportCompany.TCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                Else
                    'ایجاد کاربر مرتبط به شرکت حمل و نقل
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourTransportCompany.TCManagerMobileNumber, .UserActive = True, .UserName = YourTransportCompany.TCTitle, .UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany}, False, InstanceSoftwareUsers.GetSystemUser.UserId)
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers
                                          Where UserId=" & UserId & " or TCId=" & YourTransportCompany.TCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers(TCId,UserId) Values(" & YourTransportCompany.TCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                    'ایجاد دسترسی ها
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.TransportCompany.ToString + ":"
                    Dim AllofProcessGroupsIds As String()
                    Dim AllofProcessesIds As String()
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی برای نوع کاربر شرکت حمل و نقل و ارسال به مدیریت مجوز
                    Dim AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب برای نوع کاربر شرکت حمل و نقل و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofProcessGroupsIds)
                    End If

                    'بررسی کیف پول 
                    Dim HasMoneyWallet = HasTransportCompanyMoneyWallet(YourTransportCompany.TCId)
                    If Not HasMoneyWallet Then
                        'ایجاد کیف پول کاربر
                        Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager
                        Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                        CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets Where CardId=" & MoneyWalletId & " or TCId=" & YourTransportCompany.TCId & ""
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets(CardId,TCId) Values(" & MoneyWalletId & "," & YourTransportCompany.TCId & ")"
                        CmdSql.ExecuteNonQuery()
                    End If
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SMSTypeIdNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As UserNotExistByMobileNumberException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SoftwareUserMobileNumberBelongsToSomeoneElseException
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
        End Sub

        Public Function GetSoftwareUserIdfromTransportCompanyId(YourTransportCompanyId As Int64, YourImmediate As Boolean) As Int64
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on SoftwareUsers.UserId=TCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies on TCRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                        where TCRelationSoftwareUsers.TCId=" & YourTransportCompanyId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Return Convert.ToInt64(DS.Tables(0).Rows(0).Item("UserId"))
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportCompanyfromSoftwareUser(YourSoftWareUser As R2CoreSoftwareUser, YourImmediately As Boolean) As RawTransportCompany
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                                TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
                                TransportCompanies.Active as Active
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                            Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on TransportCompanies.TCId=TCRelationSoftwareUsers.TCId  
                            Inner join R2Primary.DBO.TblSoftwareUsers as SoftwareUsers on TCRelationSoftwareUsers.UserId =SoftwareUsers.UserId 
                            Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                         Where SoftwareUsers.UserId=" & YourSoftWareUser.UserId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TransportCompanyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select TransportCompanies.TCId as TCId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCOrganizationCode as TCOrganizationCode,Cities.StrCityName as TCCityTitle,
                                TransportCompanies.TCTel as TCTel,TransportCompanies.TCManagerMobileNumber as TCManagerMobileNumber,TransportCompanies.TCManagerNameFamily,TransportCompanies.EmailAddress as EmailAddress,
                                TransportCompanies.Active as Active
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                            Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TCRelationSoftwareUsers on TransportCompanies.TCId=TCRelationSoftwareUsers.TCId  
                            Inner join R2Primary.DBO.TblSoftwareUsers as SoftwareUsers on TCRelationSoftwareUsers.UserId =SoftwareUsers.UserId 
                            Inner Join DBTransport.dbo.tbCity as Cities On TransportCompanies.TCCityId=Cities.nCityCode 
                         Where SoftwareUsers.UserId=" & YourSoftWareUser.UserId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                End If
                Return New RawTransportCompany With {.TCId = DS.Tables(0).Rows(0).Item("TCId"), .TCTitle = DS.Tables(0).Rows(0).Item("TCTitle").trim, .TCOrganizationCode = DS.Tables(0).Rows(0).Item("TCOrganizationCode").trim, .TCCityTitle = DS.Tables(0).Rows(0).Item("TCCityTitle").trim, .TCTel = DS.Tables(0).Rows(0).Item("TCTel").trim, .TCManagerMobileNumber = DS.Tables(0).Rows(0).Item("TCManagerMobileNumber").trim, .TCManagerNameFamily = DS.Tables(0).Rows(0).Item("TCManagerNameFamily").trim, .EmailAddress = DS.Tables(0).Rows(0).Item("EmailAddress").trim, .Active = DS.Tables(0).Rows(0).Item("Active")}
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ISTransportCompanyActive(YourTransportCompanyId As Int64) As Boolean
            Dim InstanceqlDataBOX = New R2CoreInstanseSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Active from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies Where TCId = " & YourTransportCompanyId & "", 300, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportCompanyNotFoundException
                Return DS.Tables(0).Rows(0).Item("Active")
            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TransportCompanyChangeActiveStatus(YourTransportCompanyId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService)
                Dim SoftwareUserId = GetSoftwareUserIdfromTransportCompanyId(YourTransportCompanyId, True)
                Dim TransportCompany = InstanceTransportCompanies.GetTransportCompany(YourTransportCompanyId, True)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies 
                                      Set Active=" & IIf(TransportCompany.Active, 0, 1) & "
                                      Where TCId=" & YourTransportCompanyId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update dbtransport.dbo.tbCompany 
                                      Set Active=" & IIf(TransportCompany.Active, 0, 1) & "
                                      Where nCompCode =" & YourTransportCompanyId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserActive=" & IIf(TransportCompany.Active, 0, 1) & " Where Userid=" & SoftwareUserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                SendTransportCompanyChangeActiveStatusSMS(TransportCompany.TCTitle, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not TransportCompany.Active))

            Catch ex As TransportCompanyNotFoundException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SendTransportCompanyChangeActiveStatusSMS(YourTCTitle As String, YourStatus As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 6).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'ارسال اس ام اس
                Dim myData = New SMSCreationData With {.Data1 = YourTCTitle, .Data2 = YourStatus}
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotificationSMSTypes.TransportCompanyChangeActiveStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub







    End Class




End Namespace
