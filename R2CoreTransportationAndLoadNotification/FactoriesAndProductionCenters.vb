

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
Imports R2Core.DateTimeProvider
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
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.SoftwareUserManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.TransportCompanies
Imports R2CoreTransportationAndLoadNotification.TransportCompanies.Exceptions


Namespace FactoriesAndProductionCentersManagement

    Public Class RawFactoryAndProductionCenter
        Public FPCId As Int64
        Public FPCTitle As String
        Public FPCTel As String
        Public FPCAddress As String
        Public FPCManagerMobileNumber As String
        Public FPCManagerNameFamily As String
        Public EmailAddress As String
        Public Active As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationFactoriesAndProductionCentersManager
        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Function HasFactoryAndProductionCenterMoneyWallet(YourFactoryAndProductionCenterId As Int64) As Boolean
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select Top 1 MoneyWallets.CardId from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets as FPCRMoneyWallets On MoneyWallets.CardId=FPCRMoneyWallets.CardId 
                        Where MoneyWallets.Active=1 and FPCRMoneyWallets.RelationActive=1 and FPCRMoneyWallets.FPCId=" & YourFactoryAndProductionCenterId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetFactoriesAndProductionCenters_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                               FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                        Where FPCTitle Like N'%" & YourSearchString & "%' and FPCs.Deleted=0
                        Order By FPCTitle
                        for json auto")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                               FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                        From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                        Where FPCTitle Like N'%" & YourSearchString & "%' and FPCs.Deleted=0
                        Order By FPCTitle
                        for json auto", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetFactoryAndProductionCenter(YourFactoryAndProductionCenterId As Int64, YourImmediately As Boolean) As RawFactoryAndProductionCenter
            Try
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                                                         FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                                                       From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                                                       Where FPCId = " & YourFactoryAndProductionCenterId & " and FPCs.Deleted=0")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                                                     "Select FPCs.FPCId as FPCId,FPCs.FPCTitle as FPCTitle,FPCs.FPCTel as FPCTel,FPCs.FPCAddress as FPCAddress,FPCs.FPCManagerMobileNumber as FPCManagerMobileNumber,
                                                         FPCs.FPCManagerNameFamily as FPCManagerNameFamily,FPCs.EmailAddress as EmailAddress,FPCs.Active as Active
                                                      From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs
                                                      Where FPCId = " & YourFactoryAndProductionCenterId & " and FPCs.Deleted=0", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnyNotFoundException
                End If
                Return New RawFactoryAndProductionCenter With {.FPCId = DS.Tables(0).Rows(0).Item("FPCId"), .FPCTitle = DS.Tables(0).Rows(0).Item("FPCTitle").trim, .FPCTel = DS.Tables(0).Rows(0).Item("FPCTel").trim, .FPCAddress = DS.Tables(0).Rows(0).Item("FPCAddress").trim, .FPCManagerMobileNumber = DS.Tables(0).Rows(0).Item("FPCManagerMobileNumber").trim, .FPCManagerNameFamily = DS.Tables(0).Rows(0).Item("FPCManagerNameFamily").trim, .EmailAddress = DS.Tables(0).Rows(0).Item("EmailAddress").trim, .Active = DS.Tables(0).Rows(0).Item("Active")}
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function FactoryAndProductionCenterRegister(YourRawFactoryAndProductionCenter As RawFactoryAndProductionCenter) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                If YourRawFactoryAndProductionCenter.FPCTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _DateTimeService.GetCurrentDateAndTime
                CmdSql.CommandText = "Select Top 1 FPCId From R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters with (tablockx) Order By FPCId Desc"
                Dim FPCIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters(FPCId,FPCTitle,FPCTel,FPCAddress,FPCManagerMobileNumber,FPCManagerNameFamily,EmailAddress,ViewFlag,Active,Deleted)
                                            Values(" & FPCIdNew & ",'" & YourRawFactoryAndProductionCenter.FPCTitle & "','" & YourRawFactoryAndProductionCenter.FPCTel & "','" & YourRawFactoryAndProductionCenter.FPCAddress & "','" & YourRawFactoryAndProductionCenter.FPCManagerMobileNumber & "','" & YourRawFactoryAndProductionCenter.FPCManagerNameFamily & "','" & YourRawFactoryAndProductionCenter.EmailAddress & "',1,1,0)"
                CmdSql.ExecuteNonQuery()

                Dim UserId As Int64 = Int64.MinValue
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException
                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & FPCIdNew & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                Else
                    'ایجاد کاربر
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourRawFactoryAndProductionCenter.FPCManagerMobileNumber, .UserActive = True, .UserName = YourRawFactoryAndProductionCenter.FPCTitle, .UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters}, False, InstanceSoftwareUsers.GetSystemUser.UserId)
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & FPCIdNew & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                    'ایجاد دسترسی ها
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters.ToString + ":"
                    Dim AllofProcessGroupsIds As String()
                    Dim AllofProcessesIds As String()
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی , ارسال به مدیریت مجوز
                    Dim AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofProcessGroupsIds)
                    End If

                    ' کیف پول 
                    'ایجاد کیف پول کاربر
                    Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager(_DateTimeService)
                    Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                    CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets(CardId,FPCId) Values(" & MoneyWalletId & "," & FPCIdNew & ")"
                    CmdSql.ExecuteNonQuery()

                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return FPCIdNew
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
            Catch ex As DataEntryException
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

        Public Sub DeleteFactoryAndProductionCenter(YourFPCId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters Set Deleted=0
                                      Where FPCId=" & YourFPCId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As DataBaseException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub EditFactoryAndProductionCenter(YourRawFactoryAndProductionCenter As RawFactoryAndProductionCenter)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, Nothing)
                If YourRawFactoryAndProductionCenter.FPCTitle = String.Empty Then Throw New DataEntryException

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters 
                                      Set FPCTitle='" & YourRawFactoryAndProductionCenter.FPCTitle & "',FPCTel='" & YourRawFactoryAndProductionCenter.FPCTel & "',FPCAddress='" & YourRawFactoryAndProductionCenter.FPCAddress & "',FPCManagerNameFamily='" & YourRawFactoryAndProductionCenter.FPCManagerNameFamily & "',FPCManagerMobileNumber='" & YourRawFactoryAndProductionCenter.FPCManagerMobileNumber & "',EmailAddress='" & YourRawFactoryAndProductionCenter.EmailAddress & "'
                                      Where FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                CmdSql.ExecuteNonQuery()

                Dim UserId As Int64 = Int64.MinValue
                If InstanceSoftwareUsers.IsExistSoftwareUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), UserId, True) Then
                    If InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(YourRawFactoryAndProductionCenter.FPCManagerMobileNumber), True).UserTypeId <> R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters Then Throw New SoftwareUserMobileNumberBelongsToSomeoneElseException

                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & YourRawFactoryAndProductionCenter.FPCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()
                Else
                    'ایجاد کاربر
                    UserId = InstanceSoftwareUsers.RegisteringSoftwareUser(New R2CoreRawSoftwareUserStructure With {.UserId = Nothing, .MobileNumber = YourRawFactoryAndProductionCenter.FPCManagerMobileNumber, .UserActive = True, .UserName = YourRawFactoryAndProductionCenter.FPCTitle, .UserTypeId = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters}, False, InstanceSoftwareUsers.GetSystemUser.UserId)

                    CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers
                                          Where UserId=" & UserId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers(FPCId,UserId) Values(" & YourRawFactoryAndProductionCenter.FPCId & "," & UserId & ")"
                    CmdSql.ExecuteNonQuery()

                    'ایجاد دسترسی ها
                    Dim ComposeSearchString As String = R2CoreTransportationAndLoadNotificationSoftwareUserTypes.FactoriesAndProductionCenters.ToString + ":"
                    Dim AllofProcessGroupsIds As String()
                    Dim AllofProcessesIds As String()
                    'به دست آوردن لیست فرآیندهای وب قابل دسترسی و ارسال به مدیریت مجوز
                    Dim AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesAccessWebProcesses), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessesIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassPermissionsManagement.RegisteringPermissions(R2CorePermissionTypes.SoftwareUsersAccessWebProcesses, UserId, AllofProcessesIds)
                    End If
                    'به دست آوردن لیست گروههای فرآیند وب و ارسال آن به مدیریت روابط نهادی
                    AllofSoftwareUserTypes = Split(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.SoftwareUserTypesRelationWebProcessGroups), ";")
                    If Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length) <> String.Empty Then
                        AllofProcessGroupsIds = Split(Mid(AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllofSoftwareUserTypes.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ",")
                        R2CoreMClassEntityRelationManagement.RegisteringEntityRelations(R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup, UserId, AllofProcessGroupsIds)
                    End If

                    'بررسی کیف پول 
                    Dim HasMoneyWallet = HasFactoryAndProductionCenterMoneyWallet(YourRawFactoryAndProductionCenter.FPCId)
                    If Not HasMoneyWallet Then
                        'ایجاد کیف پول کاربر
                        Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager(_DateTimeService)
                        Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                        CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets 
                                              Where CardId=" & MoneyWalletId & " or FPCId=" & YourRawFactoryAndProductionCenter.FPCId & ""
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets(CardId,FPCId) Values(" & MoneyWalletId & "," & YourRawFactoryAndProductionCenter.FPCId & ")"
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
            Catch ex As DataEntryException
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

        Public Function GetSoftwareUserIdfromFactoryAndProductionCenterId(YourFactoryAndProductionCenterId As Int64, YourImmediate As Boolean) As Int64
            Try
                Dim DS As New DataSet
                If YourImmediate Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers as FPCRelationSoftwareUsers on SoftwareUsers.UserId=FPCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FactoriesAndProductionCenters on FPCRelationSoftwareUsers.FPCId=FactoriesAndProductionCenters.FPCId 
                        where FPCRelationSoftwareUsers.FPCId=" & YourFactoryAndProductionCenterId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New FactoryAndProductionCenterNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers as FPCRelationSoftwareUsers on SoftwareUsers.UserId=FPCRelationSoftwareUsers.UserId 
                        inner join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FactoriesAndProductionCenters on FPCRelationSoftwareUsers.FPCId=FactoriesAndProductionCenters.FPCId 
                        where FPCRelationSoftwareUsers.FPCId=" & YourFactoryAndProductionCenterId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New FactoryAndProductionCenterNotFoundException
                End If
                Return Convert.ToInt64(DS.Tables(0).Rows(0).Item("UserId"))
            Catch ex As FactoryAndProductionCenterNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub FactoryAndProductionCenterChangeActiveStatus(YourFactoryAndProductionCenterId As Int64)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Dim SoftwareUserId = GetSoftwareUserIdfromFactoryAndProductionCenterId(YourFactoryAndProductionCenterId, True)
                Dim FactoryAndProductionCenter = GetFactoryAndProductionCenter(YourFactoryAndProductionCenterId, True)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters 
                                      Set Active=" & IIf(FactoryAndProductionCenter.Active, 0, 1) & "
                                      Where FPCId=" & YourFactoryAndProductionCenterId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserActive=" & IIf(FactoryAndProductionCenter.Active, 0, 1) & " Where Userid=" & SoftwareUserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                SendFactoryAndProductionCenterChangeActiveStatusSMS(FactoryAndProductionCenter.FPCTitle, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not FactoryAndProductionCenter.Active))
            Catch ex As FactoryAndProductionCenterNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As AnyNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As TransportCompanyNotFoundException
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

        Public Sub SendFactoryAndProductionCenterChangeActiveStatusSMS(YourFPCTitle As String, YourStatus As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 9).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'ارسال اس ام اس
                Dim myData = New SMSCreationData With {.Data1 = YourFPCTitle, .Data2 = YourStatus}
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_DateTimeService)
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotificationSMSTypes.FactoryAndProductionCenterChangeActiveStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class FactoryAndProductionCenterNotFoundException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotification.PredefinedMessages.R2CoreTransportationAndLoadNotificationPredefinedMessages.FactoryAndProductionCenterNotFoundException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotification.PredefinedMessages.R2CoreTransportationAndLoadNotificationPredefinedMessages.FactoryAndProductionCenterNotFoundException).MsgId
        End Sub
    End Class


End Namespace
