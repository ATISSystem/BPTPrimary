
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
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters
Imports R2CoreTransportationAndLoadNotification.TransportTariffsParameters.Exceptions
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
Imports R2CoreTransportationAndLoadNotification.Caching
Imports Newtonsoft.Json
Imports R2Core.Caching
Imports R2Core.SessionManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoaderTypes
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports R2CoreTransportationAndLoadNotification.Turns.TurnInfo
Imports R2Core.CachHelper
Imports R2CoreTransportationAndLoadNotification.PubSubMessaging
Imports StackExchange.Redis
Imports R2CoreTransportationAndLoadNotification.PredefinedMessages
Imports R2Core.DateTimeProvider


Namespace LoadCapacitor

    Namespace LoadCapacitorAccounting

        Public MustInherit Class R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes
            Public Shared ReadOnly Property None As Int64 = 0
            Public Shared ReadOnly Property Registering As Int64 = 1
            Public Shared ReadOnly Property Editing As Int64 = 2
            Public Shared ReadOnly Property Deleting As Int64 = 3
            Public Shared ReadOnly Property Incrementing As Int64 = 4
            Public Shared ReadOnly Property Decrementing As Int64 = 5
            Public Shared ReadOnly Property Cancelling As Int64 = 6
            Public Shared ReadOnly Property Releasing As Int64 = 7
            Public Shared ReadOnly Property LoadPermissionCancelling As Int64 = 8
            Public Shared ReadOnly Property Allocating As Int64 = 9
            Public Shared ReadOnly Property AllocationCancelling As Int64 = 10
            Public Shared ReadOnly Property FreeLining As Int64 = 11
            Public Shared ReadOnly Property Sedimenting As Int64 = 12
            Public Shared ReadOnly Property RegisteringforTommorow As Int64 = 13
            Public Shared ReadOnly Property ChangingStatusTommorowLoads As Int64 = 14
            Public Shared ReadOnly Property ReRegisteringSedimentedLoad As Int64 = 15
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure

            Public Sub New()
                MyBase.New()
                _nEstelamId = Int64.MinValue
                _AccountType = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.None
                _Amount = Int64.MinValue
                _DateTimeMilladi = Nothing
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
            End Sub

            Public Sub New(ByVal YournEstelamId As Int64, YourAccountType As Int64, YourAmount As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64)
                _nEstelamId = YournEstelamId
                _AccountType = YourAccountType
                _Amount = YourAmount
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
            End Sub

            Public Property nEstelamId As Int64
            Public Property AccountType As Int64
            Public Property Amount As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingTypeStructure

            Public Sub New()
                MyBase.New()
                _AId = Int64.MinValue
                _ATitle = String.Empty
                _Description = String.Empty
                _Color = String.Empty
            End Sub

            Public Sub New(ByVal YourAId As Int64, YourATitle As String, YourDescription As String, YourColor As String)
                _AId = YourAId
                _ATitle = YourATitle
                _Description = YourDescription
                _Color = YourColor
            End Sub

            Public Property AId As Int64
            Public Property ATitle As String
            Public Property Description As String
            Public Property Color As String
        End Class

        Public Class R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
            Private _DateTime As R2DateTime = New R2DateTime()

            Public Function GetTotalLoadCapacitorLoadAllocating(YournEstelamId As Int64) As Int64
                Try
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    Dim AllocatingCount As Int64 = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YournEstelamId & " and AccountType=9", 0, DS, New Boolean).GetRecordsCount()
                    Dim AllocatingCancellingCount As Int64 = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YournEstelamId & " and AccountType=10", 0, DS, New Boolean).GetRecordsCount()
                    Return AllocatingCount - AllocatingCancellingCount
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetTotalLoadCapacitorLoadWhichCanAllocate(YournEstelamId As Int64) As Int64
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                    Return NSSLoadCapacitorLoad.nCarNum - GetTotalLoadCapacitorLoadAllocating(YournEstelamId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub InsertAccounting(ByVal YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim DMilladiFormated = _DateTime.GetCurrentDateTimeMilladi()
                    Dim DShamsiFull = _DateTime.ConvertToShamsiDate(DMilladiFormated)
                    Dim TimeofDate = _DateTime.GetTimeOfDate(DMilladiFormated)
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting(nEstelamId,AccountType,Amount,DateTimeMilladi,DateShamsi,Time,UserId) values(" & YourNSS.nEstelamId & "," & YourNSS.AccountType & "," & YourNSS.Amount & ",'" & DMilladiFormated & "','" & DShamsiFull & "','" & TimeofDate & "'," & YourNSS.UserId & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement

            Private Shared _DateTime As R2DateTime = New R2DateTime()
            Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

            Public Shared Sub InsertAccounting(ByVal YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting(nEstelamId,AccountType,Amount,DateTimeMilladi,DateShamsi,Time,UserId) values(" & YourNSS.nEstelamId & "," & YourNSS.AccountType & "," & YourNSS.Amount & ",'" & _DateTime.GetCurrentDateTimeMilladi() & "','" & _DateTime.GetCurrentShamsiDate() & "','" & _DateTime.GetCurrentTime() & "'," & YourNSS.UserId & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Function GetLoadCapacitorLoadLastAccount(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
                Try
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 AccountType from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YourNSS.nEstelamId & " Order By DateTimeMilladi Desc", 1, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadCapacitorLoadNotFoundException
                    Return DS.Tables(0).Rows(0).Item("AccountType")
                Catch exx As LoadCapacitorLoadNotFoundException
                    Throw exx
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalLoadCapacitorLoadAllocating(YournEstelamId As Int64) As Int64
                Try
                    Dim DS As DataSet
                    Dim AllocatingCount As Int64 = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YournEstelamId & " and AccountType=9", 0, DS, New Boolean).GetRecordsCount()
                    Dim AllocatingCancellingCount As Int64 = InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YournEstelamId & " and AccountType=10", 0, DS, New Boolean).GetRecordsCount()
                    Return AllocatingCount - AllocatingCancellingCount
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalLoadCapacitorLoadWhichCanAllocate(YournEstelamId As Int64) As Int64
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, False)
                    Return NSSLoadCapacitorLoad.nCarNum - GetTotalLoadCapacitorLoadAllocating(YournEstelamId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetNSSLoadCapacitorLoadAccountingType(YourAccountingTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingTypeStructure
                Try
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccountingType Where AId=" & YourAccountingTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadCapacitorAccountingTypeNotFoundException
                    Return New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingTypeStructure(DS.Tables(0).Rows(0).Item("AId"), DS.Tables(0).Rows(0).Item("ATitle").trim, DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("Color").trim)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetAccountings(YournEstelamId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure)
                Try
                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting Where nEstelamId=" & YournEstelamId & " Order By DateTimeMilladi Desc", 1, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("AccountType"), DS.Tables(0).Rows(Loopx).Item("Amount"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("UserId")))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfLoadAccounting(YourAccountingType As Int64, YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                     Select Sum(LoadCapacitorAccounting.Amount) as TotalAmount from  dbtransport.dbo.tbElam as LoadCapacitor 
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as LoadCapacitorAccounting On LoadCapacitor.nEstelamID=LoadCapacitorAccounting.nEstelamId 
                     Where LoadCapacitorAccounting.DateShamsi='" & _DateTime.GetCurrentShamsiDate & "' and  LoadCapacitorAccounting.AccountType=" & YourAccountingType & " and LoadCapacitor.AHId=" & YourAHId & " and LoadCapacitor.AHSGId=" & YourAHSGId & "", 1, DS, New Boolean).GetRecordsCount = 0 Then
                        Return 0
                    Else
                        Return DS.Tables(0).Rows(0).Item("TotalAmount")
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function





        End Class

        'BPTChanged
        Public MustInherit Class R2CoreTransportationAndLoadNotificationLoadAccountingTypes
            Public Shared ReadOnly Property None As Int64 = 0
            Public Shared ReadOnly Property Registering As Int64 = 1
            Public Shared ReadOnly Property Editing As Int64 = 2
            Public Shared ReadOnly Property Deleting As Int64 = 3
            Public Shared ReadOnly Property Incrementing As Int64 = 4
            Public Shared ReadOnly Property Decrementing As Int64 = 5
            Public Shared ReadOnly Property Cancelling As Int64 = 6
            Public Shared ReadOnly Property Releasing As Int64 = 7
            Public Shared ReadOnly Property LoadPermissionCancelling As Int64 = 8
            Public Shared ReadOnly Property Allocating As Int64 = 9
            Public Shared ReadOnly Property AllocationCancelling As Int64 = 10
            Public Shared ReadOnly Property FreeLining As Int64 = 11
            Public Shared ReadOnly Property Sedimenting As Int64 = 12
            Public Shared ReadOnly Property RegisteringforTommorow As Int64 = 13
            Public Shared ReadOnly Property TransferringTommorowLoads As Int64 = 14
            Public Shared ReadOnly Property ReRegisteringSedimentedLoad As Int64 = 15
        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoadAccounting
            Public Property LoadId As Int64
            Public Property AccountTypeId As Int64
            Public Property Amount As Int64
        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoadAccountingManager

            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
            End Sub

            Public Sub InsertAccounting(ByVal YourLoadAccounting As R2CoreTransportationAndLoadNotificationLoadAccounting, YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting(nEstelamId,AccountType,Amount,DateTimeMilladi,DateShamsi,Time,UserId) values(" & YourLoadAccounting.LoadId & "," & YourLoadAccounting.AccountTypeId & "," & YourLoadAccounting.Amount & ",convert(varchar, getdate(), 20),R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()),convert(varchar, getdate(), 8)," & YourSoftwareUserId & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function GetLoadAccountingRecords(YourLoadId As Int64, YourImmediately As Boolean) As String
                Try
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager

                    Dim DS As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlDataAdapter
                        Da.SelectCommand = New SqlCommand(
                        "Select Accountings.nEstelamId as LoadId,AccountingTypes.Color as Color,AccountingTypes.ATitle as AccountingTitle,SoftwareUsers.UserName,Accountings.Amount,Accountings.DateShamsi as ShamsiDate,Accountings.Time 
                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as Accountings
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccountingType as AccountingTypes On Accountings.AccountType=AccountingTypes.AId 
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Accountings.UserId=SoftwareUsers.UserId 
                         Where Accountings.nEstelamId=" & YourLoadId & "
                         Order By Accountings.DateTimeMilladi Desc
                         for JSON PATH")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Accountings.nEstelamId as LoadId,AccountingTypes.Color as Color,AccountingTypes.ATitle as AccountingTitle,SoftwareUsers.UserName,Accountings.Amount,Accountings.DateShamsi as ShamsiDate,Accountings.Time 
                         From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as Accountings
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccountingType as AccountingTypes On Accountings.AccountType=AccountingTypes.AId 
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Accountings.UserId=SoftwareUsers.UserId 
                         Where Accountings.nEstelamId=" & YourLoadId & "
                         Order By Accountings.DateTimeMilladi Desc
                         for JSON PATH", 180, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    End If
                    Return InstancePublicProcedures.GetIntegratedJson(DS)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function
        End Class



    End Namespace

    Namespace LoadCapacitorLoad

        Public Enum LoadCapacitorLoadsListType
            None = 0
            NotSedimented = 1
            Sedimented = 2
            TommorowLoad = 3
            LastButNotTommorowLoad = 4
        End Enum

        Public Enum R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions
            None = 0
            nEstelamId = 1
            TransportCompany = 2
            Product = 3
            TargetCity = 4
            nCarNumKol = 5
            TransportPrice = 6
            LoaderType = 7
            LoadStatus = 8
            TimeElam = 9
            ProductReciever = 10
            Address = 11
            User = 12
            TargetProvince = 13
            Tonaj = 14
        End Enum

        Public Class R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure

            Public Sub New()
                _Province = Nothing
                _NumberOfLoads = Int64.MinValue
            End Sub

            Public Sub New(ByVal YourProvince As R2CoreTransportationAndLoadNotificationStandardProvinceStructure, YourNumberOfLoads As Int64)
                _Province = YourProvince
                _NumberOfLoads = YourNumberOfLoads
            End Sub

            Public Property Province As R2CoreTransportationAndLoadNotificationStandardProvinceStructure
            Public Property NumberOfLoads As Int64
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStatusStructure

            Public Sub New()
                _LoadStatusId = Int64.MinValue
                _LoadStatusName = String.Empty
                _LoadStatusColor = String.Empty
            End Sub

            Public Sub New(ByVal YourLoadStatusId As Int64, YourLoadStatusName As String, YourLoadStatusColor As String)
                _LoadStatusId = YourLoadStatusId
                _LoadStatusName = YourLoadStatusName
                _LoadStatusColor = YourLoadStatusColor
            End Sub

            Public Property LoadStatusId As Int64
            Public Property LoadStatusName As String
            Public Property LoadStatusColor As String
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure

            Public Sub New()
                MyBase.New()
                _nEstelamId = Int64.MinValue
                _nEstelamKey = String.Empty
                _StrBarName = String.Empty
                _nCityCode = Int64.MinValue
                _nTonaj = Double.MinValue
                _nBarCode = Int64.MinValue
                _nCompCode = Int64.MinValue
                _IsSpecialLoad = Boolean.FalseString
                _nTruckType = Int64.MinValue
                _StrAddress = String.Empty
                _nUserId = Int64.MinValue
                _nCarNumKol = Int64.MinValue
                _StrPriceSug = Int64.MinValue
                _StrDescription = String.Empty
                _dDateElam = String.Empty
                _dTimeElam = String.Empty
                _nCarNum = Int64.MinValue
                _LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.None
                _nBarSource = Int64.MinValue
                _AHId = Int64.MinValue
                _AHSGId = Int64.MinValue
                _TPTParams = String.Empty
                _ReRegistered = Boolean.FalseString
                _LoadingPlaceId = Int64.MinValue
                _DischargingPlaceId = Int64.MinValue
            End Sub

            Public Sub New(ByVal YournEstelamId As Int64, ByVal YournEstelamKey As String, YourStrBarName As String, YournCityCode As Int64, YournTonaj As Double, YournBarCode As Int64, YournCompCode As Int64, YourIsSpecialCode As Boolean, YournTruckType As Int64, YourStrAddress As String, YournUserId As Int64, YournCarNumKol As Int64, YourStrPriceSug As Int64, YourStrDescription As String, YourdDateElam As String, YourdTimeElam As String, YournCarNum As Int64, YourLoadStatus As Int64, YournBarSource As Int64, YourAHId As Int64, YourAHSGId As Int64, YourTPTParams As String, YourReRegistered As Boolean, YourLoadingPlaceId As Int64, YourDischargingPlaceId As Int64)
                _nEstelamId = YournEstelamId
                _nEstelamKey = YournEstelamKey
                _StrBarName = YourStrBarName
                _nCityCode = YournCityCode
                _nTonaj = YournTonaj
                _nBarCode = YournBarCode
                _nCompCode = YournCompCode
                _IsSpecialLoad = YourIsSpecialCode
                _nTruckType = YournTruckType
                _StrAddress = YourStrAddress
                _nUserId = YournUserId
                _nCarNumKol = YournCarNumKol
                _StrPriceSug = YourStrPriceSug
                _StrDescription = YourStrDescription
                _dDateElam = YourdDateElam
                _dTimeElam = YourdTimeElam
                _nCarNum = YournCarNum
                _LoadStatus = YourLoadStatus
                _nBarSource = YournBarSource
                _AHId = YourAHId
                _AHSGId = YourAHSGId
                _TPTParams = YourTPTParams
                _ReRegistered = YourReRegistered
                _LoadingPlaceId = YourLoadingPlaceId
                _DischargingPlaceId = YourDischargingPlaceId
            End Sub

            Public Property nEstelamId As Int64
            Public Property nEstelamKey As String
            Public Property StrBarName As String
            Public Property nCityCode As Int64
            Public Property nTonaj As Double
            Public Property nBarCode As Int64
            Public Property nCompCode As Int64
            Public Property IsSpecialLoad As Boolean
            Public Property nTruckType As Int64
            Public Property StrAddress As String
            Public Property nUserId As Int64
            Public Property nCarNumKol As Int64
            Public Property StrPriceSug As Int64
            Public Property StrDescription As String
            Public Property dDateElam() As String
            Public Property dTimeElam() As String
            Public Property nCarNum As Int64
            Public Property LoadStatus As Int64
            Public Property nBarSource As Int64
            Public Property AHId() As Int64
            Public Property AHSGId() As Int64
            Public Property TPTParams As String
            Public Property ReRegistered As Boolean
            Public Property LoadingPlaceId As Int64
            Public Property DischargingPlaceId As Int64
        End Class

        Public Class R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure
            Inherits R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure

            Public Sub New()
                MyBase.New()
                _TransportCompanyTitle = String.Empty
                _GoodTitle = String.Empty
                _LoadSourceTitle = String.Empty
                _LoadTargetTitle = String.Empty
                _LoaderTypeTitle = String.Empty
                _TransportCompanyTel = String.Empty
                _LoadTarget = String.Empty
                _UserName = String.Empty
                _LoadingPlaceTitle = String.Empty
                _DischargingPlaceTitle = String.Empty
            End Sub

            Public Sub New(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourTransportCompanyTitle As String, YourGoodTitle As String, YourLoadSourceTitle As String, YourLoadTargetTitle As String, YourLoaderTypeTitle As String, YourTransportCompanyTel As String, YourLoadTarget As String, YourUserName As String, YourLoadingPlaceTitle As String, YourDischargingPlaceTitle As String)
                MyBase.New(YourNSS.nEstelamId, YourNSS.nEstelamKey, YourNSS.StrBarName, YourNSS.nCityCode, YourNSS.nTonaj, YourNSS.nBarCode, YourNSS.nCompCode, YourNSS.IsSpecialLoad, YourNSS.nTruckType, YourNSS.StrAddress, YourNSS.nUserId, YourNSS.nCarNumKol, YourNSS.StrPriceSug, YourNSS.StrDescription, YourNSS.dDateElam, YourNSS.dTimeElam, YourNSS.nCarNum, YourNSS.LoadStatus, YourNSS.nBarSource, YourNSS.AHId, YourNSS.AHSGId, YourNSS.TPTParams, YourNSS.ReRegistered, YourNSS.LoadingPlaceId, YourNSS.DischargingPlaceId)
                _TransportCompanyTitle = YourTransportCompanyTitle
                _GoodTitle = YourGoodTitle
                _LoadSourceTitle = YourLoadSourceTitle
                _LoadTargetTitle = YourLoadTargetTitle
                _LoaderTypeTitle = YourLoaderTypeTitle
                _TransportCompanyTel = YourTransportCompanyTel
                _LoadTarget = YourLoadTarget
                _UserName = YourUserName
                _LoadingPlaceTitle = YourLoadingPlaceTitle
                _DischargingPlaceTitle = YourDischargingPlaceTitle
            End Sub

            Public Property TransportCompanyTitle As String
            Public Property TransportCompanyTel As String
            Public Property GoodTitle As String
            Public Property LoadSourceTitle As String
            Public Property LoadTargetTitle As String
            Public Property LoaderTypeTitle As String
            Public Property LoadTarget As String
            Public Property UserName As String
            Public Property LoadingPlaceTitle As String
            Public Property DischargingPlaceTitle As String

        End Class

        Public Class R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
            Private _DateTime As New R2DateTime

            Public Function ReportingInformationProviderAnnouncedLoadsReport(YourAHSGId As Int64, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of KeyValuePair(Of String, String))
                'گزارش بار اعلام شده
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourAHSGId)

                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewAnnouncedLoadsReportOrClearanceLoadsReport, YourNSSSoftwareUser.UserId, 0) Then Throw New PermissionException

                    Dim InstanceSqlDataBox As New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           "Select Loads.nBarCode,Products.strGoodName, sum(Loads.nCarNumKol) as nCarNumKol
                            From dbtransport.dbo.tbElam as Loads
                               Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode =Products.strGoodCode 
                            Where Loads.dDateElam ='" & _DateTime.GetCurrentShamsiDate & "' and (LoadStatus<>3 and LoadStatus<>4 and LoadStatus<>6) and Loads.AHSGId=" & YourAHSGId & "
                            Group By Loads.nBarCode, Products.strGoodName", 300, DS, New Boolean)
                    Dim Lst = New List(Of KeyValuePair(Of String, String))
                    Dim StringB As New StringBuilder
                    Dim Total As Int64 = 0
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim ValueHeader = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("strGoodName"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("strGoodName").ToString().Trim()) + vbCrLf
                        StringB.Clear()
                        StringB.Append("تعداد : " + IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("nCarNumKol").ToString().Trim()))
                        Lst.Add(New KeyValuePair(Of String, String)(ValueHeader, StringB.ToString()))
                        Total = Total + IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DBNull.Value), 0, DS.Tables(0).Rows(Loopx).Item("nCarNumKol"))
                    Next
                    If Total <> 0 Then Lst.Add(New KeyValuePair(Of String, String)("جمع کل : ", Total.ToString))
                    Return Lst
                Catch ex As PermissionException
                    Throw ex
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function ReportingInformationProviderClearanceLoadsReport(YourAHSGId As Int64, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of KeyValuePair(Of String, String))
                'گزارش بار آزاد شده
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourAHSGId)

                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewAnnouncedLoadsReportOrClearanceLoadsReport, YourNSSSoftwareUser.UserId, 0) Then Throw New PermissionException

                    Dim InstanceSqlDataBox As New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Loads.nBarCode,Products.strGoodName, Count(*) as Counting
                         from dbtransport.dbo.tbEnterExit as Turns
                            Inner Join dbtransport.dbo.tbElam  as Loads On Turns.nEstelamID=Loads.nEstelamID 
                            Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode =Products.strGoodCode 
                         Where Turns.strExitDate='" & _DateTime.GetCurrentShamsiDate & "' and Turns.LoadPermissionStatus=1 and Loads.AHSGId=" & YourAHSGId & "
                         Group By Loads.nBarCode,Products.strGoodName", 300, DS, New Boolean)
                    Dim Lst = New List(Of KeyValuePair(Of String, String))
                    Dim StringB As New StringBuilder
                    Dim Total As Int64 = 0
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim ValueHeader = IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("strGoodName"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("strGoodName").ToString().Trim()) + vbCrLf
                        StringB.Clear()
                        StringB.Append("تعداد : " + IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("Counting"), DBNull.Value), String.Empty, DS.Tables(0).Rows(Loopx).Item("Counting").ToString().Trim()))
                        Lst.Add(New KeyValuePair(Of String, String)(ValueHeader, StringB.ToString()))
                        Total = Total + IIf(Object.Equals(DS.Tables(0).Rows(Loopx).Item("Counting"), DBNull.Value), 0, DS.Tables(0).Rows(Loopx).Item("Counting"))
                    Next
                    If Total <> 0 Then Lst.Add(New KeyValuePair(Of String, String)("جمع کل : ", Total.ToString))
                    Return Lst
                Catch ex As PermissionException
                    Throw ex
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadCapacitorLoadsfromSubscriptionDB(YourAHId As Int64, YourAHSGId As Int64, YourAHATTypeId As Int64, YournCarNumViewZeroFlag As Boolean, YourLoadStatusLimitation As Boolean, YourOrderingOptions As R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions, Optional YourTransportCompanyId As Int64 = Int64.MinValue, Optional YourProvinceId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim LastAnnounceTime As String
                    If YourAHSGId = Int64.MinValue Then
                        LastAnnounceTime = String.Empty
                    Else
                        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                        LastAnnounceTime = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(YourAHId, YourAHSGId).Time
                    End If
                    Dim Sql As String
                    Sql = "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                           From DBTransport.dbo.TbElam as Elam 
	                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                              Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                              Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                              Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces on City.nProvince=Provinces.ProvinceId 
   							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                           Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and Elam.AHId=" & YourAHId & "" + IIf(YourAHSGId = Int64.MinValue, " ", " and Elam.AHSGId=" & YourAHSGId & "")
                    If YourLoadStatusLimitation Then Sql = Sql + " And Elam.LoadStatus<>3 And Elam.LoadStatus<>4 And Elam.LoadStatus<>6"
                    If Not YournCarNumViewZeroFlag Then Sql = Sql + " And Elam.nCarNum>0"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.UnAnnounceLoads Then Sql = Sql + " And Elam.dTimeElam>='" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.LastAnnounceLoads Then Sql = Sql + " and Elam.dTimeElam<'" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.SedimentedLoads Then Sql = Sql + "  and Elam.LoadStatus=5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.AllOfLoadsWithoutSedimentedLoads Then Sql = Sql + " and Elam.LoadStatus<>5"
                    If YourTransportCompanyId <> Int64.MinValue Then Sql = Sql + " and Elam.nCompCode=" & YourTransportCompanyId & ""
                    If YourProvinceId <> Int64.MinValue Then Sql = Sql + " and Provinces.ProvinceId=" & YourProvinceId & ""
                    Select Case YourOrderingOptions
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nEstelamId
                            Sql = Sql + " Order By Elam.nEstelamId Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Address
                            Sql = Sql + " Order By Elam.strAddress,Elam.nCityCode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoadStatus
                            Sql = Sql + " Order By Elam.LoadStatus Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoaderType
                            Sql = Sql + " Order By LoaderType.LoaderTypeTitle,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Product
                            Sql = Sql + " Order By Product.strGoodName,TransportCompanies.TCTitle"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.ProductReciever
                            Sql = Sql + " Order By Elam.strBarName Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetCity
                            Sql = Sql + " Order By City.strCityName,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TimeElam
                            Sql = Sql + " Order By Elam.dTimeElam Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportCompany
                            Sql = Sql + " Order By TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportPrice
                            Sql = Sql + " Order By Elam.strPriceSug Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.User
                            Sql = Sql + " Order By Elam.nUserID Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nCarNumKol
                            Sql = Sql + " Order By Elam.nCarNumKol Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetProvince
                            Sql = Sql + " Order By City.nProvince,City.strCityName"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Tonaj
                            Sql = Sql + " Order By Elam.nTonaj"
                    End Select
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, Sql, 120, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams"), DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, Nothing, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared LstLoadCapacitorLoadCollection = New Dictionary(Of String, List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure))
            'Public Function GetLoadCapacitorLoadsforApplication(YourRequesterId As Int64, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourAHSGId As Int64, YourLoadStatusId As Int64, YourProvinceId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
            '    Try

            '        Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
            '        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            '        Dim InstancePermissions As New R2CoreInstansePermissionsManager
            '        Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
            '        Dim DSPreInformations As DataSet = Nothing
            '        Dim SeqTId As Int64 = Int64.MinValue
            '        Dim NativenessTypeId As Int64 = Int64.MinValue
            '        If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanViewAllofLoadsfromApplication, YourNSSSoftwareUser.UserId, 0) Then
            '            SeqTId = Turns.SequentialTurns.SequentialTurns.None
            '            NativenessTypeId = R2CoreTransportationAndLoadNotification.TrucksNativeness.TruckNativenessTypes.Native
            '        Else
            '            If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '               "Select Top 1 Cars.CarNativenessTypeId,SequentialTurns.SeqTId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
            '                   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
            '                   Inner Join dbtransport.dbo.TbPerson as Persons On EntityRelations.E2 =Persons.nIDPerson
            '                   Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPerson On Persons.nIDPerson=CarAndPerson.nIDPerson 
            '                   Inner Join dbtransport.dbo.TbCar as Cars On CarAndPerson.nIDCar=Cars.nIDCar 
            '                   Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SequentialTurns.SeqTKeyWord  Collate Arabic_CI_AI_WS 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
            '                Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and EntityRelations.ERTypeId=" & R2CoreParkingSystem.EntityRelations.R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPerson.snRelation=2 and Cars.ViewFlag=1 and SequentialTurns.Active=1 and TruckNativenessTypes.Active=1
            '                      and ((DATEDIFF(SECOND,CarAndPerson.RelationTimeStamp,getdate())<240) Or (CarAndPerson.RelationTimeStamp='2015-01-01 00:00:00.000'))
            '                      and (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Turns.bFlagDriver=0
            '                Order By Turns.nEnterExitId Desc", 300, DSPreInformations, New Boolean).GetRecordsCount = 0 Then Throw New BaseInfFailedException
            '            SeqTId = DSPreInformations.Tables(0).Rows(0).Item("SeqTId")
            '            NativenessTypeId = DSPreInformations.Tables(0).Rows(0).Item("CarNativenessTypeId")
            '        End If

            '        Dim DSLoads As DataSet = Nothing
            '        Dim SqlQuery = "Select Loads.nEstelamId,Loads.StrBarName,Loads.TPTParams,Loads.nTonaj,Loads.StrAddress,Loads.nCarNumKol,Loads.nCarNum,Loads.StrPriceSug,Loads.strDescription,TransportCompanies.TCTitle,Products.strGoodName,LoadSources.strCityName as LoadSource,LoadTargets.strCityName as LoadTarget,TransportCompanies.TCTel,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,LoadsforTurnCancellation.Description AS TurnCancellationDescription
            '                from DBTransport.dbo.TbElam as Loads
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
            '                   Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
            '                   Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
            '                   Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces on LoadTargets.nProvince=Provinces.ProvinceId 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.tblLoadsViewConditions as LoadsViewConditions On Loads.AHSGId=LoadsViewConditions.AHSGId
            '      			   Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation as LoadsforTurnCancellation on Loads.nEstelamID=LoadsforTurnCancellation.nEstelamID
            '                where Loads.dDateElam='" & _DateTime.GetCurrentShamsiDate() & "' and Loads.LoadStatus=" & YourLoadStatusId & " and LoadsViewConditions.LoadStatusId=" & YourLoadStatusId & " and LoadsViewConditions.RequesterId=" & YourRequesterId & " and Loads.nCarNum>0 and Loads.AHSGId=" & YourAHSGId & " and Provinces.ProvinceId=" & YourProvinceId & " and LoadsViewConditions.SeqTId=" & SeqTId & " and LoadsViewConditions.NativenessTypeId=" & NativenessTypeId & "
            '                Order By LoadTargets.StrCityName"
            '        Dim DataChangeStatus As Boolean = False
            '        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '               SqlQuery, 180, DSLoads, DataChangeStatus).GetRecordsCount = 0 Then Throw New NoLoadsorLoadsViewConditionsMismatchException
            '        If DataChangeStatus = True Then
            '            For Loopx As Int64 = 0 To DSLoads.Tables(0).Rows.Count - 1
            '                Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DSLoads.Tables(0).Rows(Loopx).Item("nEstelamId"), DSLoads.Tables(0).Rows(Loopx).Item("TurnCancellationDescription").trim, DSLoads.Tables(0).Rows(Loopx).Item("StrBarName").trim, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("nTonaj"), Nothing, Nothing, Nothing, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("StrAddress").trim, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("nCarNumKol"), DSLoads.Tables(0).Rows(Loopx).Item("StrPriceSug"), DSLoads.Tables(0).Rows(Loopx).Item("strDescription").trim, Nothing, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("nCarNum"), Nothing, Nothing, Nothing, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("TPTParams"), Nothing, Nothing, Nothing), DSLoads.Tables(0).Rows(Loopx).Item("TCTitle").trim, DSLoads.Tables(0).Rows(Loopx).Item("strGoodName").trim, DSLoads.Tables(0).Rows(Loopx).Item("LoadSource").trim, DSLoads.Tables(0).Rows(Loopx).Item("LoadTarget").trim, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("TCTel").trim, Nothing, Nothing, DSLoads.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DSLoads.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
            '            Next
            '            Try
            '                LstLoadCapacitorLoadCollection.Remove(SqlQuery)
            '            Catch ex As ArgumentException
            '            Catch ex As KeyNotFoundException
            '            Catch ex As Exception
            '            End Try
            '            LstLoadCapacitorLoadCollection.Add(SqlQuery, Lst)
            '        End If
            '        Return LstLoadCapacitorLoadCollection(SqlQuery)
            '    Catch ex As NoLoadsorLoadsViewConditionsMismatchException
            '        Throw ex
            '    Catch ex As BaseInfFailedException
            '        Throw
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            Public Function GetLoadCapacitorLoads(YourAHId As Int64, YourAHSGId As Int64, YourAHATTypeId As Int64, YournCarNumViewZeroFlag As Boolean, YourLoadStatusLimitation As Boolean, YourOrderingOptions As R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions, Optional YourTransportCompanyId As Int64 = Int64.MinValue, Optional YourProvinceId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim LastAnnounceTime As String
                    If YourAHSGId = Int64.MinValue Then
                        LastAnnounceTime = String.Empty
                    Else
                        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                        LastAnnounceTime = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(YourAHId, YourAHSGId).Time
                    End If
                    Dim Sql As String
                    Sql = "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle 
                           From DBTransport.dbo.TbElam as Elam 
	                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                              Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                              Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                              Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces on City.nProvince=Provinces.ProvinceId 
   							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                           Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and Elam.AHId=" & YourAHId & "" + IIf(YourAHSGId = Int64.MinValue, " ", " and Elam.AHSGId=" & YourAHSGId & "")
                    If YourLoadStatusLimitation Then Sql = Sql + " And Elam.LoadStatus<>3 And Elam.LoadStatus<>4 And Elam.LoadStatus<>6"
                    If Not YournCarNumViewZeroFlag Then Sql = Sql + " And Elam.nCarNum>0"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.UnAnnounceLoads Then Sql = Sql + " And Elam.dTimeElam>='" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.LastAnnounceLoads Then Sql = Sql + " and Elam.dTimeElam<'" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.SedimentedLoads Then Sql = Sql + "  and Elam.LoadStatus=5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.AllOfLoadsWithoutSedimentedLoads Then Sql = Sql + " and Elam.LoadStatus<>5"
                    If YourTransportCompanyId <> Int64.MinValue Then Sql = Sql + " and Elam.nCompCode=" & YourTransportCompanyId & ""
                    If YourProvinceId <> Int64.MinValue Then Sql = Sql + " and Provinces.ProvinceId=" & YourProvinceId & ""
                    Select Case YourOrderingOptions
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nEstelamId
                            Sql = Sql + " Order By Elam.nEstelamId Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Address
                            Sql = Sql + " Order By Elam.strAddress,Elam.nCityCode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoadStatus
                            Sql = Sql + " Order By Elam.LoadStatus Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoaderType
                            Sql = Sql + " Order By LoaderType.LoaderTypeTitle,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Product
                            Sql = Sql + " Order By Product.strGoodName,TransportCompanies.TCTitle"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.ProductReciever
                            Sql = Sql + " Order By Elam.strBarName Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetCity
                            Sql = Sql + " Order By City.strCityName,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TimeElam
                            Sql = Sql + " Order By Elam.dTimeElam Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportCompany
                            Sql = Sql + " Order By TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportPrice
                            Sql = Sql + " Order By Elam.strPriceSug Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.User
                            Sql = Sql + " Order By Elam.nUserID Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nCarNumKol
                            Sql = Sql + " Order By Elam.nCarNumKol Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetProvince
                            Sql = Sql + " Order By City.nProvince,City.strCityName"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Tonaj
                            Sql = Sql + " Order By Elam.nTonaj"
                    End Select
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, Sql, 0, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, Nothing, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSLoadCapacitorLoad(YournEstelamKey As String, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure
                Try
                    Dim DS As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                           Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.nTruckType,
                                      Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,
                                   Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.
                                   TCTitle,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                               From DBTransport.dbo.TbElam as Elam 
                                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                        Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                        Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                                        Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
     							        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                               Where nEstelamKey='" & YournEstelamKey & "' and LoadStatus<>3")
                        Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                        If Da.Fill(DS) = 0 Then Throw New LoadCapacitorLoadNotFoundException
                    Else
                        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                  "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.nTruckType,
                                      Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,
                                      Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.
                                      TCTitle,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle ,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                               From DBTransport.dbo.TbElam as Elam 
                                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                        Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                        Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                                        Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                               Where nEstelamKey='" & YournEstelamKey & "' and LoadStatus<>3", 300, DS, New Boolean).GetRecordsCount = 0 Then
                            Throw New LoadCapacitorLoadNotFoundException
                        End If
                    End If
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure
                    NSS.nEstelamId = DS.Tables(0).Rows(0).Item("nEstelamId")
                    NSS.nEstelamKey = DS.Tables(0).Rows(0).Item("nEstelamKey")
                    NSS.nTruckType = DS.Tables(0).Rows(0).Item("nTruckType")
                    NSS.StrAddress = DS.Tables(0).Rows(0).Item("StrAddress").trim
                    NSS.nUserId = DS.Tables(0).Rows(0).Item("nUserId")
                    NSS.StrBarName = DS.Tables(0).Rows(0).Item("StrBarName").trim
                    NSS.StrDescription = DS.Tables(0).Rows(0).Item("StrDescription").trim
                    NSS.StrPriceSug = DS.Tables(0).Rows(0).Item("strPriceSug")
                    NSS.dDateElam = DS.Tables(0).Rows(0).Item("dDateElam").TRIM
                    NSS.dTimeElam = DS.Tables(0).Rows(0).Item("dTimeElam").TRIM
                    NSS.nBarCode = DS.Tables(0).Rows(0).Item("nBarCode")
                    NSS.nCarNum = DS.Tables(0).Rows(0).Item("nCarNum")
                    NSS.nCarNumKol = DS.Tables(0).Rows(0).Item("nCarNumKol")
                    NSS.nCityCode = DS.Tables(0).Rows(0).Item("nCityCode")
                    NSS.nTonaj = DS.Tables(0).Rows(0).Item("nTonaj")
                    NSS.nCompCode = DS.Tables(0).Rows(0).Item("nCompCode")
                    NSS.LoadStatus = DS.Tables(0).Rows(0).Item("LoadStatus")
                    NSS.nBarSource = DS.Tables(0).Rows(0).Item("nBarSource")
                    NSS.AHId = DS.Tables(0).Rows(0).Item("AHId")
                    NSS.AHSGId = DS.Tables(0).Rows(0).Item("AHSGId")
                    NSS.TPTParams = DS.Tables(0).Rows(0).Item("TPTParams").trim
                    NSS.ReRegistered = DS.Tables(0).Rows(0).Item("ReRegistered")
                    NSS.LoadingPlaceId = DS.Tables(0).Rows(0).Item("LoadingPlaceId")
                    NSS.DischargingPlaceId = DS.Tables(0).Rows(0).Item("DischargingPlaceId")
                    NSS.TransportCompanyTitle = DS.Tables(0).Rows(0).Item("TCTitle").trim
                    NSS.GoodTitle = DS.Tables(0).Rows(0).Item("strGoodName").trim
                    NSS.LoadSourceTitle = DS.Tables(0).Rows(0).Item("LoadSource").trim
                    NSS.LoadTargetTitle = DS.Tables(0).Rows(0).Item("strCityName").trim
                    NSS.LoaderTypeTitle = DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim
                    NSS.LoadingPlaceTitle = DS.Tables(0).Rows(0).Item("LoadingPlaceTitle").trim
                    NSS.DischargingPlaceTitle = DS.Tables(0).Rows(0).Item("DischargingPlaceTitle").trim
                    Return NSS
                Catch ex As LoadCapacitorLoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSLoadCapacitorLoad(YournEstelamId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure
                Try
                    Dim DS As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                            Select Elam.nEstelamID,nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle 
                            From DBTransport.dbo.TbElam as Elam 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                                Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                            Where nEstelamId=" & YournEstelamId & "")
                        Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                        If Da.Fill(DS) = 0 Then Throw New LoadCapacitorLoadNotFoundException
                    Else
                        Dim InstanseSqlDataBOX = New R2CoreSqlDataBOXManager
                        If Not InstanseSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                                         "Select Elam.nEstelamID,nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle 
                                          From DBTransport.dbo.TbElam as Elam 
                                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                               Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                               Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                                               Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
     							               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                                          Where nEstelamId=" & YournEstelamId & "", 120, DS, New Boolean).GetRecordsCount <> 0 Then
                            Throw New LoadCapacitorLoadNotFoundException
                        End If
                    End If
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure
                    NSS.nEstelamId = YournEstelamId
                    NSS.nEstelamKey = DS.Tables(0).Rows(0).Item("nEstelamKey").trim
                    NSS.nTruckType = DS.Tables(0).Rows(0).Item("nTruckType")
                    NSS.StrAddress = DS.Tables(0).Rows(0).Item("StrAddress").trim
                    NSS.nUserId = DS.Tables(0).Rows(0).Item("nUserId")
                    NSS.StrBarName = DS.Tables(0).Rows(0).Item("StrBarName").trim
                    NSS.StrDescription = DS.Tables(0).Rows(0).Item("StrDescription").trim
                    NSS.StrPriceSug = DS.Tables(0).Rows(0).Item("strPriceSug")
                    NSS.dDateElam = DS.Tables(0).Rows(0).Item("dDateElam").TRIM
                    NSS.dTimeElam = DS.Tables(0).Rows(0).Item("dTimeElam").TRIM
                    NSS.nBarCode = DS.Tables(0).Rows(0).Item("nBarCode")
                    NSS.nCarNum = DS.Tables(0).Rows(0).Item("nCarNum")
                    NSS.nCarNumKol = DS.Tables(0).Rows(0).Item("nCarNumKol")
                    NSS.nCityCode = DS.Tables(0).Rows(0).Item("nCityCode")
                    NSS.nTonaj = DS.Tables(0).Rows(0).Item("nTonaj")
                    NSS.nCompCode = DS.Tables(0).Rows(0).Item("nCompCode")
                    NSS.LoadStatus = DS.Tables(0).Rows(0).Item("LoadStatus")
                    NSS.nBarSource = DS.Tables(0).Rows(0).Item("nBarSource")
                    NSS.AHId = DS.Tables(0).Rows(0).Item("AHId")
                    NSS.AHSGId = DS.Tables(0).Rows(0).Item("AHSGId")
                    NSS.TPTParams = DS.Tables(0).Rows(0).Item("TPTParams")
                    NSS.ReRegistered = DS.Tables(0).Rows(0).Item("ReRegistered")
                    NSS.LoadingPlaceId = DS.Tables(0).Rows(0).Item("LoadingPlaceId")
                    NSS.DischargingPlaceId = DS.Tables(0).Rows(0).Item("DischargingPlaceId")
                    NSS.TransportCompanyTitle = DS.Tables(0).Rows(0).Item("TCTitle").trim
                    NSS.GoodTitle = DS.Tables(0).Rows(0).Item("strGoodName").trim
                    NSS.LoadSourceTitle = DS.Tables(0).Rows(0).Item("LoadSource").trim
                    NSS.LoadTargetTitle = DS.Tables(0).Rows(0).Item("strCityName").trim
                    NSS.LoaderTypeTitle = DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim
                    NSS.LoadingPlaceTitle = DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim
                    NSS.LoadingPlaceTitle = DS.Tables(0).Rows(0).Item("LoadingPlaceTitle").trim
                    NSS.DischargingPlaceTitle = DS.Tables(0).Rows(0).Item("DischargingPlaceTitle").trim
                    Return NSS
                Catch ex As LoadCapacitorLoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsLoadCapacitorLoadTimingReadeyforLoadAllocationRegistering(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourNSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, YourNSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As Boolean
                Dim InstanceAnnouncemenetHall = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Try
                    'کنترل تایمینگ تخصیص بار 
                    If Not InstanceAnnouncemenetHall.IsAnnouncemenetHallAnnounceTimePassed(YourNSSAnnouncementHall.AHId, YourNSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime With {.Time = YourNSSLoadCapacitorLoad.dTimeElam}) Then
                        If Not YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Then Return False
                    End If
                    Return True
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Function IsLoadCapacitorLoadStatusReadeyforLoadAllocationRegistering_(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
                Try
                    'Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    'کنترل وضعیت بار
                    If Not (YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented) Then Return False
                    'کنترل تعداد بار
                    'If InstanceLoadCapacitorAccounting.GetTotalLoadCapacitorLoadWhichCanAllocate(YourNSSLoadCapacitorLoad.nEstelamId) < 1 Then Return False
                    Return True
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsLoadCapacitorLoadStatusReadeyforLoadAllocationRegistering(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
                Try
                    Return IsLoadCapacitorLoadStatusReadeyforLoadAllocationRegistering_(YourNSSLoadCapacitorLoad)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            'Public Function GetProvincesWithNumberOfLoads(YourAHId As Int64, YourAHSGId As Int64, YourLoadCapacitorLoadsListType As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure)
            Private Shared LstNumberOfLoadsOfProvinceCollection = New Dictionary(Of String, List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure))
            'Public Function GetProvincesWithNumberOfLoadsforApplication(YourRequesterId As Int64, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourAHSGId As Int64, YourLoadStatusId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure)
            '    Try
            '        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            '        Dim InstancePermissions As New R2CoreInstansePermissionsManager
            '        Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
            '        Dim DSPreInformations As DataSet = Nothing
            '        Dim SeqTId As Int64 = Int64.MinValue
            '        Dim NativenessTypeId As Int64 = Int64.MinValue

            '        If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanViewAllofLoadsfromApplication, YourNSSSoftwareUser.UserId, 0) Then
            '            SeqTId = Turns.SequentialTurns.SequentialTurns.None
            '            NativenessTypeId = R2CoreTransportationAndLoadNotification.TrucksNativeness.TruckNativenessTypes.Native
            '        Else
            '            If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '               "Select Top 1 Cars.CarNativenessTypeId,SequentialTurns.SeqTId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
            '                   Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
            '                   Inner Join dbtransport.dbo.TbPerson as Persons On EntityRelations.E2 =Persons.nIDPerson
            '                   Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPerson On Persons.nIDPerson=CarAndPerson.nIDPerson 
            '                   Inner Join dbtransport.dbo.TbCar as Cars On CarAndPerson.nIDCar=Cars.nIDCar 
            '                   Inner Join dbtransport.dbo.tbEnterExit as Turns On Cars.nIDCar=Turns.strCardno 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On substring(Turns.OtaghdarTurnNumber,1,1) Collate Arabic_CI_AI_WS=SequentialTurns.SeqTKeyWord  Collate Arabic_CI_AI_WS 
            '                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
            '                Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUsers.UserActive=1 and EntityRelations.ERTypeId=" & R2CoreParkingSystem.EntityRelations.R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and EntityRelations.RelationActive=1 and CarAndPerson.snRelation=2 and Cars.ViewFlag=1 and SequentialTurns.Active=1 and TruckNativenessTypes.Active=1
            '                      and ((DATEDIFF(SECOND,CarAndPerson.RelationTimeStamp,getdate())<240) Or (CarAndPerson.RelationTimeStamp='2015-01-01 00:00:00.000'))
            '                      and (Turns.TurnStatus=1 or Turns.TurnStatus=7 or Turns.TurnStatus=8 or Turns.TurnStatus=9 or Turns.TurnStatus=10) and Turns.bFlagDriver=0
            '                Order By Turns.nEnterExitId Desc", 300, DSPreInformations, New Boolean).GetRecordsCount = 0 Then Throw New BaseInfFailedException
            '            SeqTId = DSPreInformations.Tables(0).Rows(0).Item("SeqTId")
            '            NativenessTypeId = DSPreInformations.Tables(0).Rows(0).Item("CarNativenessTypeId")
            '        End If


            '        Dim DS As DataSet = New DataSet
            '        Dim DataChangeStatus As Boolean = False
            '        Dim SqlQuery = "Select Provinces.ProvinceId, Provinces.ProvinceName,Sum(Loads.nCarNum) as NumberOfLoads
            '                from DBTransport.dbo.tbElam as Loads
            '                Inner join DBTransport.dbo.tbCity as Cities On Loads.nCityCode=Cities.nCityCode  
            '                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On Cities.nProvince=Provinces.ProvinceId 
            '                Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On Loads.AHSGId=AHSGs.AHSGId 
            '                Inner Join R2PrimaryTransportationAndLoadNotification.dbo.tblLoadsViewConditions as LoadsViewConditions On Loads.AHSGId=LoadsViewConditions.AHSGId
            '              Where ltrim(rtrim(Loads.dDateElam))='" & _DateTime.GetCurrentShamsiDate() & "' and Loads.nCarNum>0 and AHSGs.Active=1 and AHSGs.Deleted=0 and Provinces.Active=1 and Provinces.Deleted=0 and
            '                    Loads.LoadStatus=" & YourLoadStatusId & " and LoadsViewConditions.LoadStatusid=" & YourLoadStatusId & " and LoadsViewConditions.RequesterId=" & YourRequesterId & " and Loads.AHSGId=" & YourAHSGId & " and LoadsViewConditions.SeqTId=" & SeqTId & " and LoadsViewConditions.NativenessTypeId=" & NativenessTypeId & "
            '              Group By Provinces.ProvinceId, Provinces.ProvinceName 
            '              Order By Provinces.ProvinceName"
            '        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '               SqlQuery, 180, DS, DataChangeStatus).GetRecordsCount = 0 Then Throw New NoLoadsorLoadsViewConditionsMismatchException
            '        If DataChangeStatus = True Then
            '            Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure)
            '            For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
            '                Dim NSSNumberOfLoadsOfProvince = New R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure
            '                Dim NSSProvince = New R2CoreTransportationAndLoadNotificationStandardProvinceStructure
            '                NSSProvince.ProvinceId = DS.Tables(0).Rows(Loopx).Item("ProvinceId")
            '                NSSProvince.ProvinceTitle = DS.Tables(0).Rows(Loopx).Item("ProvinceName").trim
            '                NSSNumberOfLoadsOfProvince.Province = NSSProvince
            '                NSSNumberOfLoadsOfProvince.NumberOfLoads = DS.Tables(0).Rows(Loopx).Item("NumberOfLoads")
            '                Lst.Add(NSSNumberOfLoadsOfProvince)
            '            Next
            '            Try
            '                LstNumberOfLoadsOfProvinceCollection.Remove(SqlQuery)
            '            Catch ex As ArgumentException
            '            Catch ex As KeyNotFoundException
            '            Catch ex As Exception
            '            End Try
            '            LstNumberOfLoadsOfProvinceCollection.Add(SqlQuery, Lst)
            '        End If
            '        Return LstNumberOfLoadsOfProvinceCollection(SqlQuery)
            '    Catch ex As NoLoadsorLoadsViewConditionsMismatchException
            '        Throw ex
            '    Catch ex As BaseInfFailedException
            '        Throw ex
            '    Catch ex As LoadTargetsforProvinceNotFoundException
            '        Throw ex
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            Public Function IsLoadCapacitorLoadReadeyforLoadPermissionRegistering(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure) As Boolean
                Try
                    'Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    'Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    'Dim InstanceAnnouncementHall = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    'Dim NSSAnnouncementHall = InstanceAnnouncementHall.GetNSSAnnouncementHall(YourNSSLoadCapacitorLoad.AHId)
                    'Dim NSSAnnouncementsubGroup = InstanceAnnouncementHall.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSSLoadCapacitorLoad.nTruckType)
                    ''کنترل زمان ترخیص بار 
                    'If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.LoadPermissionUseTimeHandlingByLoadStatus, YourNSSLoadCapacitorLoad.LoadStatus, 0) Then
                    '    If Not InstanceAnnouncementHall.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, YourNSSLoadCapacitorLoad.dTimeElam)) Then
                    '        If Not YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Then Return False
                    '    End If
                    'End If

                    'کنترل وضعیت بار
                    If Not (YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented) Then Return False
                    'کنترل تعداد بار
                    If YourNSSLoadCapacitorLoad.nCarNum < 1 Then Return False
                    Return True
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub LoadCapacitorLoadTonajValidate(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
                Try
                    'بررسی تناژ بار
                    Dim ComposeSearchString As String = YourNSSLoadCapacitorLoad.AHSGId.ToString + "="
                    Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim AllConfigforAHId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadCapacitorControl, YourNSSLoadCapacitorLoad.AHId), "-")
                    Dim AllConfigforAHSGId = Split(Mid(AllConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsLoadTonajControlActive As Boolean = AllConfigforAHSGId(0)
                    Dim LoadTonajMaximum As Double = AllConfigforAHSGId(1)
                    If IsLoadTonajControlActive And YourNSSLoadCapacitorLoad.nTonaj > LoadTonajMaximum Then Throw New LoadTonajInvalidException
                Catch ex As LoadTonajInvalidException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                    'Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub


        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement
            Private Shared _DateTime As New R2DateTime
            Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

            Public Shared Function GetNSSLoadCapacitorLoadStatus(YourLoadCapacitorLoadStatusId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStatusStructure
                Try
                    Dim DS As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses Where LoadStatusId=" & YourLoadCapacitorLoadStatusId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadCapacitorLoadStatusException
                    Dim NSS As New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStatusStructure
                    NSS.LoadStatusId = YourLoadCapacitorLoadStatusId
                    NSS.LoadStatusName = DS.Tables(0).Rows(0).Item("LoadStatusName").TRIM
                    NSS.LoadStatusColor = DS.Tables(0).Rows(0).Item("LoadStatusColor").TRIM
                    Return NSS
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetLoadCapacitorLoads(YourShamsiDate As R2CoreDateAndTime, YourAHId As Int64, YourAHSGId As Int64, YourAHATTypeId As Int64, YournCarNumViewZeroFlag As Boolean, YourLoadStatusLimitation As Boolean, YourOrderingOptions As R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions, Optional YourTransportCompanyId As Int64 = Int64.MinValue, Optional YourProvinceId As Int64 = Int64.MinValue) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim LastAnnounceTime As String
                    If YourAHSGId = Int64.MinValue Then
                        LastAnnounceTime = String.Empty
                    Else
                        LastAnnounceTime = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetAnnouncemenetHallLastAnnounceTime(YourAHId, YourAHSGId).Time
                    End If
                    Dim DS As DataSet
                    Dim Sql As String
                    Sql = "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle 
                                  From DBTransport.dbo.TbElam as Elam 
	                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                                  Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                                  Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                                  Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces on City.nProvince=Provinces.ProvinceId 
   							      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							      Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                           Where Elam.dDateElam ='" & YourShamsiDate.ShamsiDate & "' and Elam.AHId=" & YourAHId & "" + IIf(YourAHSGId = Int64.MinValue, " ", " and Elam.AHSGId=" & YourAHSGId & "")
                    If YourLoadStatusLimitation Then Sql = Sql + " And Elam.LoadStatus<>3 And Elam.LoadStatus<>4 And Elam.LoadStatus<>6"
                    If Not YournCarNumViewZeroFlag Then Sql = Sql + " And Elam.nCarNum>0"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.UnAnnounceLoads Then Sql = Sql + " And Elam.dTimeElam>='" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.LastAnnounceLoads Then Sql = Sql + " and Elam.dTimeElam<'" & LastAnnounceTime & "' and Elam.LoadStatus<>5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.SedimentedLoads Then Sql = Sql + "  and Elam.LoadStatus=5"
                    If YourAHATTypeId = AnnouncementHallAnnounceTimeTypes.AllOfLoadsWithoutSedimentedLoads Then Sql = Sql + " and Elam.LoadStatus<>5"
                    If YourTransportCompanyId <> Int64.MinValue Then Sql = Sql + " and Elam.nCompCode=" & YourTransportCompanyId & ""
                    If YourProvinceId <> Int64.MinValue Then Sql = Sql + " and Provinces.ProvinceId=" & YourProvinceId & ""
                    Select Case YourOrderingOptions
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nEstelamId
                            Sql = Sql + " Order By Elam.nEstelamId Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Address
                            Sql = Sql + " Order By Elam.strAddress,Elam.nCityCode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoadStatus
                            Sql = Sql + " Order By Elam.LoadStatus Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.LoaderType
                            Sql = Sql + " Order By LoaderType.LoaderTypeTitle,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Product
                            Sql = Sql + " Order By Product.strGoodName,TransportCompanies.TCTitle"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.ProductReciever
                            Sql = Sql + " Order By Elam.strBarName Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetCity
                            Sql = Sql + " Order By City.strCityName,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TimeElam
                            Sql = Sql + " Order By Elam.dTimeElam Desc"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportCompany
                            Sql = Sql + " Order By TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TransportPrice
                            Sql = Sql + " Order By Elam.strPriceSug Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.User
                            Sql = Sql + " Order By Elam.nUserID Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.nCarNumKol
                            Sql = Sql + " Order By Elam.nCarNumKol Desc,TransportCompanies.TCTitle,Elam.nBarcode"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.TargetProvince
                            Sql = Sql + " Order By City.nProvince,City.strCityName"
                        Case R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions.Tonaj
                            Sql = Sql + " Order By Elam.nTonaj"
                    End Select
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, Sql, 0, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, Nothing, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetLoadCapacitorLoadsforMonitoring(YourAHId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim DS As DataSet
                    Dim Sql As String
                    Sql = "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,Product.strGoodName,LoadSources.strCityName as LoadSource,City.strCityName,LoaderType.LoaderTypeTitle,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle 
                           From DBTransport.dbo.TbElam as Elam 
	                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                              Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                              Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                              Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces on City.nProvince=Provinces.ProvinceId 
   							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                           Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and Elam.AHId=" & YourAHId & ""
                    Sql = Sql + " And Elam.LoadStatus<>3 And Elam.LoadStatus<>4 And Elam.LoadStatus<>6"
                    Sql = Sql + " And Elam.nCarNum>0"
                    Sql = Sql + " and Elam.LoadStatus<>5"
                    Sql = Sql + " Order By Product.strGoodName,TransportCompanies.TCTitle"
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, Sql, 0, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, Nothing, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetExistenceNonZeroLoads(YourNSSRequesterSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim SqlString = String.Empty
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofAllLoadsofLoadCapacitor, YourNSSRequesterSoftwareUser.UserId, 0) Then
                    Else
                        SqlString = " and Elam.nUserID In (Select Permissions.EntityIdSecond from R2Primary.dbo.TblPermissions as Permissions 
                                                           Where Permissions.EntityIdFirst=" & YourNSSRequesterSoftwareUser.UserId & " and Permissions.PermissionTypeId=" & R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser & " and Permissions.RelationActive=1)"
                        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                        Try
                            SqlString += " and Elam.nCompCode = " + InstanceTransportCompanies.GetNSSTransportCompnay(YourNSSRequesterSoftwareUser).TCId.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                                 Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                             Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                             Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,ltrim(rtrim(SoftwareUsers.UserName)) as UserName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                          From DBTransport.dbo.TbElam as Elam 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                              Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                              Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                              Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Elam.nUserId=SoftwareUsers.UserId
   							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                          Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and (Elam.LoadStatus<>3 and Elam.LoadStatus<>4 and Elam.LoadStatus<>6) and Elam.nCarNum>0" + SqlString + " Order By Elam.dTimeElam Desc", 0, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetSedimentedLoadCapacitorLoads(YourNSSRequesterSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim SqlString = String.Empty
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofAllLoadsofLoadCapacitor, YourNSSRequesterSoftwareUser.UserId, 0) Then
                    Else
                        SqlString = " and Elam.nUserID In (Select Permissions.EntityIdSecond from R2Primary.dbo.TblPermissions as Permissions 
                                                           Where Permissions.EntityIdFirst=" & YourNSSRequesterSoftwareUser.UserId & " and Permissions.PermissionTypeId=" & R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser & " and Permissions.RelationActive=1)"
                        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                        Try
                            SqlString += " and Elam.nCompCode = " + InstanceTransportCompanies.GetNSSTransportCompnay(YourNSSRequesterSoftwareUser).TCId.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                              Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                          Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                          Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,ltrim(rtrim(SoftwareUsers.UserName)) as UserName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                       From DBTransport.dbo.TbElam as Elam 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                             Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                             Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                             Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                             Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Elam.nUserId=SoftwareUsers.UserId
						     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                       Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and (Elam.LoadStatus=5 or Elam.LoadStatus=2) " + SqlString + " Order By Elam.dTimeElam Desc", 1, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetAllLoadCapacitorLoads(YourNSSRequesterSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim SqlString = String.Empty
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofAllLoadsofLoadCapacitor, YourNSSRequesterSoftwareUser.UserId, 0) Then
                    Else
                        SqlString = " and Elam.nUserID In (Select Permissions.EntityIdSecond from R2Primary.dbo.TblPermissions as Permissions 
                                                           Where Permissions.EntityIdFirst=" & YourNSSRequesterSoftwareUser.UserId & " and Permissions.PermissionTypeId=" & R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser & " and Permissions.RelationActive=1)"
                        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                        Try
                            SqlString += " and Elam.nCompCode = " + InstanceTransportCompanies.GetNSSTransportCompnay(YourNSSRequesterSoftwareUser).TCId.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                              Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                          Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                          Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,ltrim(rtrim(SoftwareUsers.UserName)) as UserName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                       From DBTransport.dbo.TbElam as Elam 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                             Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                             Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                             Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                             Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Elam.nUserId=SoftwareUsers.UserId
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                       Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and (Elam.LoadStatus<>3 and Elam.LoadStatus<>4 and Elam.LoadStatus<>6) " + SqlString + " Order By Elam.dTimeElam Desc", 1, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetLastLoadCapacitorLoads(YourNSSRequesterSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim SqlString = String.Empty
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofAllLoadsofLoadCapacitor, YourNSSRequesterSoftwareUser.UserId, 0) Then
                    Else
                        SqlString = " and Elam.nUserID In (Select Permissions.EntityIdSecond from R2Primary.dbo.TblPermissions as Permissions 
                                                           Where Permissions.EntityIdFirst=" & YourNSSRequesterSoftwareUser.UserId & " and Permissions.PermissionTypeId=" & R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser & " and Permissions.RelationActive=1)"
                        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                        Try
                            SqlString += " and Elam.nCompCode = " + InstanceTransportCompanies.GetNSSTransportCompnay(YourNSSRequesterSoftwareUser).TCId.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Top 200 Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                              Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                          Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                          Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,ltrim(rtrim(SoftwareUsers.UserName)) as UserName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                       From DBTransport.dbo.TbElam as Elam 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                             Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                             Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                             Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                             Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Elam.nUserId=SoftwareUsers.UserId
                         	 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                       Where (Elam.LoadStatus<>3 and Elam.LoadStatus<>4 and Elam.LoadStatus<>6) " + SqlString + " Order By Elam.nEstelamID Desc", 120, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTommorowLoadCapacitorLoads(YourNSSRequesterSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim SqlString = String.Empty
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofAllLoadsofLoadCapacitor, YourNSSRequesterSoftwareUser.UserId, 0) Then
                    Else
                        SqlString = " and Elam.nUserID In (Select Permissions.EntityIdSecond from R2Primary.dbo.TblPermissions as Permissions 
                                                           Where Permissions.EntityIdFirst=" & YourNSSRequesterSoftwareUser.UserId & " and Permissions.PermissionTypeId=" & R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanViewListofLoadsofLoadCapacitorofOtherUser & " and Permissions.RelationActive=1)"
                        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
                        Try
                            SqlString += " and Elam.nCompCode = " + InstanceTransportCompanies.GetNSSTransportCompnay(YourNSSRequesterSoftwareUser).TCId.ToString
                        Catch ex As Exception
                        End Try
                    End If

                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                              Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                          Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                          Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,ltrim(rtrim(SoftwareUsers.UserName)) as UserName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                       From DBTransport.dbo.TbElam as Elam 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                             Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                             Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                             Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                             Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Elam.nUserId=SoftwareUsers.UserId
                         	 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.LoadingPlaceId=DischargingPlaces.LADPlaceId 
                       Where Elam.dDateElam ='" & _DateTime.GetCurrentShamsiDate() & "' and (Elam.LoadStatus=6) " + SqlString + " Order By Elam.dTimeElam Desc", 1, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared Function GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId As Int64, YourAHSGId As Int64, YourAccountType As Int64) As Int64
                Try
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select Sum(LoadCapacitorAccounting.Amount) as TotalAmount from  dbtransport.dbo.tbElam as LoadCapacitor 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorAccounting as LoadCapacitorAccounting On LoadCapacitor.nEstelamID=LoadCapacitorAccounting.nEstelamId 
                         Where LoadCapacitorAccounting.DateShamsi='" & _DateTime.GetCurrentShamsiDate & "' and  LoadCapacitorAccounting.AccountType=" & YourAccountType & " and LoadCapacitor.AHId=" & YourAHId & " and LoadCapacitor.AHSGId=" & YourAHSGId & "", 1, DS, New Boolean).GetRecordsCount = 0 Then
                        Return 0
                    Else
                        If IsDBNull(DS.Tables(0).Rows(0).Item("TotalAmount")) Then
                            Return 0
                        Else
                            Return DS.Tables(0).Rows(0).Item("TotalAmount")
                        End If
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfRegisteredLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Registering)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfTransfferedTomarrowLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.ChangingStatusTommorowLoads)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfRegisteredTomarrowLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.RegisteringforTommorow)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfDeletedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Deleting)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfIncrementedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Incrementing)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfDecrementedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Decrementing)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfFreeLinedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.FreeLining)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfAnnouncedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfRegisteredLoads(YourAHId, YourAHSGId) + GetTotalAmountOfTransfferedTomarrowLoads(YourAHId, YourAHSGId) - GetTotalAmountOfDeletedLoads(YourAHId, YourAHSGId) + GetTotalAmountOfIncrementedLoads(YourAHId, YourAHSGId) - GetTotalAmountOfDecrementedLoads(YourAHId, YourAHSGId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfLoadAllocated(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Allocating)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfLoadAllocationCancelled(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.AllocationCancelling)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfReleasedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Releasing)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfSedimentedLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Sedimenting)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfLoadPermissionCancelledLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfCancelledLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfLoadCapacitorLoadAccounting(YourAHId, YourAHSGId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Cancelling)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetTotalAmountOfReminderOfLoads(YourAHId As Int64, YourAHSGId As Int64) As Int64
                Try
                    Return GetTotalAmountOfAnnouncedLoads(YourAHId, YourAHSGId) - GetTotalAmountOfReleasedLoads(YourAHId, YourAHSGId) - GetTotalAmountOfSedimentedLoads(YourAHId, YourAHSGId) + GetTotalAmountOfLoadPermissionCancelledLoads(YourAHId, YourAHSGId) - GetTotalAmountOfCancelledLoads(YourAHId, YourAHSGId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Shared Function GetAllofTommorowLoads() As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                Try
                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Elam.nEstelamID,Elam.nEstelamKey,Elam.strBarName,Elam.nCityCode,Elam.nTonaj,Elam.nBarcode,Elam.nCompCode,Elam.bFlagbarType,Elam.nTruckType,Elam.strAddress,
                              Elam.nUserID,Elam.nCarNumKol,Elam.strPriceSug,Elam.strDescription,Elam.dDateElam,Elam.dTimeElam,Elam.nCarNum,
	                          Elam.LoadStatus,Elam.nBarSource,Elam.AHId,Elam.AHSGId,Elam.TPTParams,Elam.bflagCarNum as ReRegistered,Elam.LoadingPlaceId,Elam.DischargingPlaceId,TransportCompanies.TCTitle,TransportCompanies.TCTel,
	                          Product.strGoodName,City.strCityName,LoadSources.strCityName as LoadSource,LoaderType.LoaderTypeTitle,LoadStatuses.LoadStatusName,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle
                       From DBTransport.dbo.TbElam as Elam 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Elam.nCompCode=TransportCompanies.TCId 
                             Inner Join dbtransport.dbo.tbProducts as Product On Elam.nBarcode=Product.strGoodCode 
                             Inner Join dbtransport.dbo.tbCity as LoadSources On Elam.nBarSource=LoadSources.nCityCode
                             Inner Join dbtransport.dbo.tbCity as City On Elam.nCityCode=City.nCityCode 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderType On Elam.nTruckType=LoaderType.LoaderTypeId 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Elam.LoadStatus=LoadStatuses.LoadStatusId
                           	 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Elam.LoadingPlaceId=LoadingPlaces.LADPlaceId 
							 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Elam.LoadingPlaceId=DischargingPlaces.LADPlaceId 
                       Where (Elam.LoadStatus=6) Order By Elam.dTimeElam Desc", 1, DS, New Boolean)
                    Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure(DS.Tables(0).Rows(Loopx).Item("nEstelamId"), DS.Tables(0).Rows(Loopx).Item("nEstelamKey"), DS.Tables(0).Rows(Loopx).Item("StrBarName").trim, DS.Tables(0).Rows(Loopx).Item("nCityCode"), DS.Tables(0).Rows(Loopx).Item("nTonaj"), DS.Tables(0).Rows(Loopx).Item("nBarCode"), DS.Tables(0).Rows(Loopx).Item("nCompCode"), DS.Tables(0).Rows(Loopx).Item("bFlagbarType"), DS.Tables(0).Rows(Loopx).Item("nTruckType"), DS.Tables(0).Rows(Loopx).Item("StrAddress").trim, DS.Tables(0).Rows(Loopx).Item("nUserId"), DS.Tables(0).Rows(Loopx).Item("nCarNumKol"), DS.Tables(0).Rows(Loopx).Item("StrPriceSug"), DS.Tables(0).Rows(Loopx).Item("strDescription").trim, DS.Tables(0).Rows(Loopx).Item("dDateElam").trim, DS.Tables(0).Rows(Loopx).Item("dTimeElam").trim, DS.Tables(0).Rows(Loopx).Item("nCarNum"), DS.Tables(0).Rows(Loopx).Item("LoadStatus"), DS.Tables(0).Rows(Loopx).Item("nBarSource"), DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTParams").trim, DS.Tables(0).Rows(Loopx).Item("ReRegistered"), DS.Tables(0).Rows(Loopx).Item("LoadingPlaceId"), DS.Tables(0).Rows(Loopx).Item("DischargingPlaceId")), DS.Tables(0).Rows(Loopx).Item("TCTitle").trim, DS.Tables(0).Rows(Loopx).Item("strGoodName").trim, DS.Tables(0).Rows(Loopx).Item("LoadSource").trim, DS.Tables(0).Rows(Loopx).Item("strCityName").trim, DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("TCTel").trim, DS.Tables(0).Rows(Loopx).Item("StrCityName").trim, Nothing, DS.Tables(0).Rows(Loopx).Item("LoadingPlaceTitle").trim, DS.Tables(0).Rows(Loopx).Item("DischargingPlaceTitle").trim))
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            'Public Shared Function GetProvincesWithNumberOfLoads(YourAHId As Int64, YourAHSGId As Int64, YourLoadCapacitorLoadsListType As LoadCapacitorLoadsListType) As List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure)
            '    Try
            '        Dim AHString = " and AHs.AHId=" & YourAHId & "" + IIf(YourAHSGId = Int64.MinValue, String.Empty, " and AHSGs.AHSGId=" & YourAHSGId & "")
            '        Dim DS As DataSet = New DataSet()
            '        If YourLoadCapacitorLoadsListType = LoadCapacitorLoadsListType.NotSedimented Then
            '            If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
            '             "Select Provinces.ProvinceId, Provinces.ProvinceName,Sum(LoadCapacitor.nCarNum) as NumberOfLoads
            '                      from DBTransport.dbo.tbElam as LoadCapacitor
            '                      Inner join DBTransport.dbo.tbCity as Cities On LoadCapacitor.nCityCode=Cities.nCityCode  
            '                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On Cities.nProvince=Provinces.ProvinceId 
            '                   Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
            '                      Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On LoadCapacitor.AHSGId=AHSGs.AHSGId 
            '                      Where ltrim(rtrim(LoadCapacitor.dDateElam))='" & _DateTime.GetCurrentShamsiDate & "' and LoadCapacitor.bFlag=0 and  (LoadCapacitor.LoadStatus=1 or LoadCapacitor.LoadStatus=2) and LoadCapacitor.nCarNum>0 and
            '                             AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0 and Provinces.ViewFlag=1 and Provinces.Deleted=0" + AHString + " Group By Provinces.ProvinceId, Provinces.ProvinceName Order By Provinces.ProvinceName", 1, DS).GetRecordsCount() = 0 Then Throw New LoadTargetsforProvinceNotFoundException
            '        ElseIf YourLoadCapacitorLoadsListType = LoadCapacitorLoadsListType.Sedimented Then
            '            If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
            '             "Select Provinces.ProvinceId, Provinces.ProvinceName,Sum(LoadCapacitor.nCarNum) as NumberOfLoads 
            '                      from DBTransport.dbo.tbElam as LoadCapacitor
            '                      Inner join DBTransport.dbo.tbCity as Cities On LoadCapacitor.nCityCode=Cities.nCityCode  
            '                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On Cities.nProvince=Provinces.ProvinceId 
            '                   Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
            '                      Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On LoadCapacitor.AHSGId=AHSGs.AHSGId 
            '                      Where ltrim(rtrim(LoadCapacitor.dDateElam))='" & _DateTime.GetCurrentShamsiDate & "' and LoadCapacitor.bFlag=1 and  (LoadCapacitor.LoadStatus=5) and LoadCapacitor.nCarNum>0 and
            '                             AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0 and Provinces.ViewFlag=1 and Provinces.Deleted=0" + AHString + " Group By Provinces.ProvinceId, Provinces.ProvinceName Order By Provinces.ProvinceName", 1, DS).GetRecordsCount() = 0 Then Throw New LoadTargetsforProvinceNotFoundException
            '        ElseIf YourLoadCapacitorLoadsListType = LoadCapacitorLoadsListType.TommorowLoad Then
            '            If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
            '             "Select Provinces.ProvinceId, Provinces.ProvinceName,Sum(LoadCapacitor.nCarNum) as NumberOfLoads 
            '                      from DBTransport.dbo.tbElam as LoadCapacitor
            '                      Inner join DBTransport.dbo.tbCity as Cities On LoadCapacitor.nCityCode=Cities.nCityCode  
            '                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On Cities.nProvince=Provinces.ProvinceId 
            '                   Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs On LoadCapacitor.AHId=AHs.AHId 
            '                      Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On LoadCapacitor.AHSGId=AHSGs.AHSGId 
            '                      Where  LoadCapacitor.bFlag=0 and  (LoadCapacitor.LoadStatus=6) and LoadCapacitor.nCarNum>0 and
            '                             AHs.ViewFlag=1 and AHs.Deleted=0 and AHSGs.ViewFlag=1 and AHSGs.Deleted=0 and Provinces.ViewFlag=1 and Provinces.Deleted=0" + AHString + " Group By Provinces.ProvinceId, Provinces.ProvinceName Order By Provinces.ProvinceName", 1, DS).GetRecordsCount() = 0 Then Throw New LoadTargetsforProvinceNotFoundException
            '        ElseIf YourLoadCapacitorLoadsListType = LoadCapacitorLoadsListType.None Then
            '        End If
            '        Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure)
            '        For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
            '            Dim NSSNumberOfLoadsOfProvince = New R2CoreTransportationAndLoadNotificationStandardNumberOfLoadsOfProvinceStructure
            '            Dim NSSProvince = New R2CoreTransportationAndLoadNotificationStandardProvinceStructure
            '            NSSProvince.ProvinceId = DS.Tables(0).Rows(Loopx).Item("ProvinceId")
            '            NSSProvince.ProvinceTitle = DS.Tables(0).Rows(Loopx).Item("ProvinceName").trim
            '            NSSNumberOfLoadsOfProvince.Province = NSSProvince
            '            NSSNumberOfLoadsOfProvince.NumberOfLoads = DS.Tables(0).Rows(Loopx).Item("NumberOfLoads")
            '            Lst.Add(NSSNumberOfLoadsOfProvince)
            '        Next
            '        Return Lst
            '    Catch ex As LoadTargetsforProvinceNotFoundException
            '        Throw ex
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function


        End Class

        'BPTChanged
        Public MustInherit Class R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses
            Public Shared ReadOnly Property None As Int64 = 0
            Public Shared ReadOnly Property Registered As Int64 = 1
            Public Shared ReadOnly Property FreeLined As Int64 = 2
            Public Shared ReadOnly Property Deleted As Int64 = 3
            Public Shared ReadOnly Property Cancelled As Int64 = 4
            Public Shared ReadOnly Property Sedimented As Int64 = 5
            Public Shared ReadOnly Property RegisteredforTommorow As Int64 = 6

        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoad
            Public Property LoadId As Int64
            Public Property AnnounceDate As String
            Public Property AnnounceTime As String
            Public Property TransportCompanyId As Int64
            Public Property TransportCompanyTitle As String
            Public Property GoodId As Int64
            Public Property GoodTitle As String
            Public Property AnnouncementGroupId As Int64
            Public Property AnnouncementGroupTitle As String
            Public Property AnnouncementSubGroupId As Int64
            Public Property AnnouncementSubGroupTitle As String
            Public Property SourceCityId As Int64
            Public Property SourceCityTitle As String
            Public Property TargetCityId As Int64
            Public Property TargetCityTitle As String
            Public Property LoadingPlaceId As Int64
            Public Property LoadingPlaceTitle As String
            Public Property DischargingPlaceId As Int64
            Public Property DischargingPlaceTitle As String
            Public Property TotalNumber As Int64
            Public Property Tonaj As Double
            Public Property Tariff As String
            Public Property Recipient As String
            Public Property Address As String
            Public Property Description As String
            Public Property LoadStatusId As Int64
            Public Property LoadStatusTitle As String
            Public Property RegisteringUserName As String
            Public Property TPTParams As String
            Public Property TPTParamsJoint As String
        End Class

        'BPTChanged 
        Public Class R2CoreTransportationAndLoadNotificationLoadSimpleModel
            Public Property LoadId As Int64
            Public Property AnnounceDate As String
            Public Property AnnounceTime As String
            Public Property TransportCompanyId As Int64
            Public Property GoodId As Int64
            Public Property AnnouncementGroupId As Int64
            Public Property AnnouncementSubGroupId As Int64
            Public Property SourceCityId As Int64
            Public Property TargetCityId As Int64
            Public Property LoadingPlaceId As Int64
            Public Property DischargingPlaceId As Int64
            Public Property TotalNumber As Int64
            Public Property Tonaj As Double
            Public Property Tariff As Int64
            Public Property Recipient As String
            Public Property Address As String
            Public Property Description As String
            Public Property LoadStatusId As Int64
            Public Property UserId As Int64
            Public Property TPTParams As String
            Public Property TPTParamsJoint As String
        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoadForLoadAllocationProccess
            Public Property LoadId As Int64
            Public Property AnnouncementGroupId As Int64
            Public Property AnnouncementSubGroupId As Int64
            Public Property LoadStatusId As Int64
        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoadManager

            Private _DateTimeService As IR2DateTimeService

            Public Sub New(YourR2DateTimeService As IR2DateTimeService)
                _DateTimeService = YourR2DateTimeService
            End Sub

            Public Function GetLoadReminder(YourLoadId As Int64, YourImmediately As Boolean) As Int64
                Try
                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("Select nCarNum as Reminder from DBTransport.dbo.tbElam as Loads Where Loads.nEstelamID = " & YourLoadId & "")
                        Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                        If Da.Fill(DS) <= 0 Then Throw New LoadNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                               "Select nCarNum as Reminder from DBTransport.dbo.tbElam as Loads Where Loads.nEstelamID = " & YourLoadId & "", 180, DS, New Boolean).GetRecordsCount = 0 Then
                            Throw New LoadNotFoundException
                        End If
                    End If
                    Return DS.Tables(0).Rows(0).Item("Reminder")
                Catch ex As LoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadStatusesForSoftwareUserType(YourSoftwareUser As R2CoreSoftwareUser) As String
                Try
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select LoadStatuses.LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle  from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadStatusesRelationSoftwareUserTypes as LoadStatusesRelationSoftwareUserTypes On LoadStatuses.LoadStatusId=LoadStatusesRelationSoftwareUserTypes.LoadStatusId 
                               Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On LoadStatusesRelationSoftwareUserTypes.SoftwareUserTypeId=SoftwareUserTypes.UTId 
                          Where SoftwareUserTypes.UTId=" & YourSoftwareUser.UserTypeId & " for JSON PATH", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadStatusesForSoftwareUserTypeNotFoundException
                    Return InstancePublicProcedures.GetIntegratedJson(DS)
                Catch ex As LoadStatusesForSoftwareUserTypeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Class PubSubMessageforCachingLoads
                Public Property Query As String
                Public Property CacheKey As String
            End Class

            Public Function GetLoadsforTruckDriver(YourRequesterId As Int64, YourSoftwareUser As R2CoreSoftwareUser, YourAnnouncementSGId As Int64, YourLoadStatusId As Int64) As String
                Try
                    Dim InstanceCache = New R2Core.Caching.R2CoreCacheManager
                    'دریافت اطلاعات نوبت
                    Dim ValueTurnInfo = DirectCast(InstanceCache.GetCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TurnInfo).CacheTypeName + YourSoftwareUser.UserId.ToString, R2CoreTransportationAndLoadNotificationCatchDataBases.TruckDriverInformation), StackExchange.Redis.RedisValue)
                    If ValueTurnInfo.IsNullOrEmpty Then Throw New BaseInfFailedException
                    Dim TurnInfo = JsonConvert.DeserializeObject(Of R2CoreTransportationAndLoadNotificationTurnInfo)(ValueTurnInfo)

                    'دریافت لیست بار از کش
                    Dim CacheKeyLoads = InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.Loads).CacheTypeName + YourRequesterId.ToString + YourAnnouncementSGId.ToString + YourLoadStatusId.ToString + TurnInfo.SeqTId.ToString + TurnInfo.NativenessTypeId.ToString
                    Dim Value = InstanceCache.GetCache(CacheKeyLoads, R2CoreTransportationAndLoadNotificationCatchDataBases.Loads)
                    If DirectCast(Value, StackExchange.Redis.RedisValue).IsNull Then
                        Dim SqlQuery =
                        "Select Provinces.ProvinceName,Provinces.ProvinceId,MyLoads.* from 
                           (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCTel as TCTel,Products.strGoodName as GoodTitle,
                                   Loads.nTonaj as Tonaj,LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,
                                   LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
                                   Loads.nCarNumKol as Total,Loads.StrPriceSug as Tariff,
	                               Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,
                                   Loads.TPTParamsJoint as TPTParamsJoint,LoadsforTurnCancellation.Description as LoadColor
                            from DBTransport.dbo.TbElam as Loads
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                              Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                              Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
                              Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.tblLoadsViewConditions as LoadsViewConditions On Loads.AHSGId=LoadsViewConditions.AHSGId
                              Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation as LoadsforTurnCancellation on Loads.nEstelamID=LoadsforTurnCancellation.nEstelamID
                            Where Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE()) and Loads.LoadStatus=" & YourLoadStatusId & " and LoadsViewConditions.LoadStatusId=" & YourLoadStatusId & " and 
                                  LoadsViewConditions.RequesterId=" & YourRequesterId & " and Loads.nCarNum>0 and Loads.AHSGId=" & YourAnnouncementSGId & " and LoadsViewConditions.SeqTId=" & TurnInfo.SeqTId & " and LoadsViewConditions.NativenessTypeId=" & TurnInfo.NativenessTypeId & ") as myLoads
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  
						 Order By ProvinceName,MyLoads.TargetCityTitle for JSON AUTO"
                        Dim PubSubMessage = New PubSubMessageforCachingLoads With {.Query = SqlQuery, .CacheKey = CacheKeyLoads}
                        Dim _Subscriber = RedisConnectorHelper.Connection.GetSubscriber()
                        _Subscriber.Publish(R2CoreTransportationAndLoadNotificationPubSubChannels.LoadsForTruckDrivers, JsonConvert.SerializeObject(PubSubMessage))
                        Throw New PleaseReTryException
                    Else
                        If JsonConvert.DeserializeObject(Of LoadListCache)(Value.ToString).Result.ToString = "" Then Throw New AnyLoadNotFoundException
                        Return JsonConvert.DeserializeObject(Of LoadListCache)(Value.ToString).Result.ToString
                    End If
                Catch ex As AnyLoadNotFoundException
                    Throw ex
                Catch ex As RedisException
                    Throw ex
                Catch ex As PleaseReTryException
                    Throw ex
                Catch ex As BaseInfFailedException
                    Throw
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Class LoadListCache
                Public Query As String
                Public Result As Object
            End Class

            Public Sub CacheLoads(YourValue As StackExchange.Redis.RedisValue)
                Try
                    Dim InstanceCache = New R2Core.Caching.R2CoreCacheManager
                    Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager

                    Dim PubSubMessage = JsonConvert.DeserializeObject(Of PubSubMessageforCachingLoads)(YourValue)

                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DSLoads As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                                        PubSubMessage.Query, 0, DSLoads, New Boolean).GetRecordsCount = 0 Then
                        Dim aLoadListCache = New LoadListCache With {.Query = PubSubMessage.Query, .Result = ""}
                        InstanceCache.SetCache(PubSubMessage.CacheKey, aLoadListCache, R2CoreTransportationAndLoadNotificationCacheTypes.Loads, R2CoreTransportationAndLoadNotificationCatchDataBases.Loads, True)
                    Else
                        Dim aLoadListCache = New LoadListCache With {.Query = PubSubMessage.Query, .Result = JsonConvert.DeserializeObject(Of Object)(InstancePublicProcedures.GetIntegratedJson(DSLoads))}
                        InstanceCache.SetCache(PubSubMessage.CacheKey, aLoadListCache, R2CoreTransportationAndLoadNotificationCacheTypes.Loads, R2CoreTransportationAndLoadNotificationCatchDataBases.Loads, True)
                    End If
                Catch ex As RedisException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub UpdateLoadLists()
                Try
                    Dim InstanceCache = New R2Core.Caching.R2CoreCacheManager
                    Dim CacheServer = RedisConnectorHelper.GetServer()
                    Dim Keys = CacheServer.Keys(R2CoreTransportationAndLoadNotificationCatchDataBases.Loads)
                    For Each Key In Keys
                        Dim Value = InstanceCache.GetCache(Key, R2CoreTransportationAndLoadNotificationCatchDataBases.Loads).ToString
                        Dim Query = JsonConvert.DeserializeObject(Of LoadListCache)(Value).Query
                        Dim PubSubMessage = New PubSubMessageforCachingLoads With {.Query = Query, .CacheKey = Key}
                        CacheLoads(JsonConvert.SerializeObject(PubSubMessage))
                    Next
                Catch ex As RedisException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            '     Private Shared LoadsCollection = New Dictionary(Of String, String)
            '     Public Function GetLoadsforTruckDriver(YourRequesterId As Int64, YourSoftwareUser As R2CoreSoftwareUser, YourAnnouncementSGId As Int64, YourLoadStatusId As Int64) As String
            '         Try
            '             Dim InstanceCache = New R2Core.Caching.R2CoreCacheManager
            '             Dim Value = DirectCast(InstanceCache.GetCache(InstanceCache.GetCacheType(R2CoreTransportationAndLoadNotificationCacheTypes.TurnInfo).CacheTypeName + YourSoftwareUser.UserId.ToString, R2CoreTransportationAndLoadNotificationCatchDataBases.TruckDriverInformation), StackExchange.Redis.RedisValue)
            '             If Value.IsNullOrEmpty Then Throw New BaseInfFailedException
            '             Dim TurnInfo = JsonConvert.DeserializeObject(Of R2CoreTransportationAndLoadNotificationTurnInfo)(Value)

            '             Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
            '             Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            '             Dim DSLoads As DataSet = Nothing
            '             Dim SqlQuery =
            '                 "Select Provinces.ProvinceName,Provinces.ProvinceId,MyLoads.* from 
            '                    (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,TransportCompanies.TCTel as TCTel,Products.strGoodName as GoodTitle,
            '                            Loads.nTonaj as Tonaj,LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,
            '                            LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
            '                            Loads.nCarNumKol as Total,Loads.StrPriceSug as Tariff,
            '                         Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,
            '                            Loads.TPTParamsJoint as TPTParamsJoint,LoadsforTurnCancellation.Description as LoadColor
            '                     from DBTransport.dbo.TbElam as Loads
            '                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
            '                       Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
            '                       Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
            '                       Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
            '                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
            '                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
            '                       Inner Join R2PrimaryTransportationAndLoadNotification.dbo.tblLoadsViewConditions as LoadsViewConditions On Loads.AHSGId=LoadsViewConditions.AHSGId
            '                       Inner join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadsforTurnCancellation as LoadsforTurnCancellation on Loads.nEstelamID=LoadsforTurnCancellation.nEstelamID
            '                     Where Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE()) and Loads.LoadStatus=" & YourLoadStatusId & " and LoadsViewConditions.LoadStatusId=" & YourLoadStatusId & " and 
            '                           LoadsViewConditions.RequesterId=" & YourRequesterId & " and Loads.nCarNum>0 and Loads.AHSGId=" & YourAnnouncementSGId & " and LoadsViewConditions.SeqTId=" & TurnInfo.SeqTId & " and LoadsViewConditions.NativenessTypeId=" & TurnInfo.NativenessTypeId & ") as myLoads
            '                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  
            'Order By ProvinceName,MyLoads.TargetCityTitle 
            'for JSON AUTO"
            '             Dim DataChangeStatus As Boolean = False
            '             If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '                    SqlQuery, 180, DSLoads, DataChangeStatus).GetRecordsCount = 0 Then Throw New NoLoadsorLoadsViewConditionsMismatchException
            '             If DataChangeStatus = True Then
            '                 Try
            '                     LoadsCollection.Remove(SqlQuery)
            '                 Catch ex As ArgumentException
            '                 Catch ex As KeyNotFoundException
            '                 Catch ex As Exception
            '                 End Try
            '                 LoadsCollection.Add(SqlQuery, InstancePublicProcedures.GetIntegratedJson(DSLoads))
            '             End If
            '             Return LoadsCollection(SqlQuery)
            '         Catch ex As NoLoadsorLoadsViewConditionsMismatchException
            '             Throw ex
            '         Catch ex As BaseInfFailedException
            '             Throw
            '         Catch ex As Exception
            '             Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '         End Try
            '     End Function

            Public Function GetLoadsforTransportCompanies(YourSoftwareUser As R2CoreSoftwareUser, YourAnnouncementGroupId As Int64, YourAnnouncementSubGroupId As Int64, YourInventory As Boolean, YourShamsiDate As String, YourLoadStatusId As Int64, YourLoadSourceId As Int64, YourLoadTargetId As Int64) As String
                Try
                    If YourShamsiDate = String.Empty Then YourShamsiDate = _DateTimeService.GetCurrentShamsiDate

                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DSLoads As DataSet = Nothing
                    'کامپوزیت ساب کووری
                    Dim SubQuery As String = String.Empty
                    If YourAnnouncementGroupId <> Int64.MinValue Then SubQuery += " and Loads.AHSGId=" & YourAnnouncementGroupId & ""
                    If YourInventory = Boolean.FalseString Then SubQuery += " and Loads.nCarNumKol>0"
                    If YourShamsiDate = String.Empty Then
                        SubQuery += " and Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE())"
                    Else
                        SubQuery += " and Loads.dDateElam='" & YourShamsiDate & "'"
                    End If
                    If YourLoadStatusId <> Int64.MinValue Then SubQuery += " and Loads.LoadStatus=" & YourLoadStatusId & ""
                    If YourLoadSourceId <> Int64.MinValue Then SubQuery += " and Loads.nBarSource=" & YourLoadSourceId & ""
                    If YourLoadTargetId <> Int64.MinValue Then SubQuery += " and Loads.nCityCode=" & YourLoadTargetId & ""

                    'کووری اصلی
                    Dim SqlQuery =
                        "Declare @TCId BigInt
                         Select Top 1 @TCId=TransportCompanies.TCId from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TransportCompaniesRelationSoftwareUsers On TransportCompanies.TCId=TransportCompaniesRelationSoftwareUsers.TCId 
                         Where TransportCompaniesRelationSoftwareUsers.UserId=" & YourSoftwareUser.UserId & "

                         Select Provinces.ProvinceName,Provinces.ProvinceId,myLoads.* from 
                           (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,Products.strGoodName as GoodTitle,Loads.nTonaj as Tonaj,
                                   LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,
                                   LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
                                   Loads.nCarNumKol as Total,Loads.nCarNum as Reminder,Loads.StrPriceSug as Tariff,LoadStatuses.LoadStatusName,Loads.dDateElam as AnnounceDate,Loads.dTimeElam as AnnounceTime, 
                                   Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGTitle,Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,
                                   SoftwareUsers.UserName,Loads.TPTParamsJoint as TPTParamsJoint
                            from DBTransport.dbo.TbElam as Loads
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On Loads.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                               Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                               Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
                               Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Loads.LoadStatus=LoadStatuses.LoadStatusId
                               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Loads.nUserId=SoftwareUsers.UserId
                            Where Loads.nCompCode=@TCId " & SubQuery & ") as myLoads
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  
                         Order By ProvinceName,MyLoads.TargetCityTitle 
                         for JSON Auto"
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           SqlQuery, 0, DSLoads, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    Return InstancePublicProcedures.GetIntegratedJson(DSLoads)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadsforFactoriesAndProductionCenters(YourSoftwareUser As R2CoreSoftwareUser, YourTransportCompanyId As Int64, YourAnnouncementGroupId As Int64, YourAnnouncementSubGroupId As Int64, YourInventory As Boolean, YourShamsiDate As String, YourLoadStatusId As Int64, YourLoadSourceId As Int64, YourLoadTargetId As Int64) As String
                Try
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DSLoads As DataSet = Nothing
                    'کامپوزیت ساب کووری
                    Dim SubQuery As String = String.Empty
                    If YourTransportCompanyId <> Int64.MinValue Then SubQuery += " and Loads.nCompCode=" & YourTransportCompanyId & ""
                    If YourAnnouncementGroupId <> Int64.MinValue Then SubQuery += " and Loads.AHSGId=" & YourAnnouncementGroupId & ""
                    If YourInventory = Boolean.FalseString Then SubQuery += " and Loads.nCarNumKol>0"
                    If YourShamsiDate = String.Empty Then
                        SubQuery += " and Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE())"
                    Else
                        SubQuery += " and Loads.dDateElam='" & YourShamsiDate & "'"
                    End If
                    If YourLoadStatusId <> Int64.MinValue Then SubQuery += " and Loads.LoadStatus=" & YourLoadStatusId & ""
                    If YourLoadSourceId <> Int64.MinValue Then SubQuery += " and Loads.nBarSource=" & YourLoadSourceId & ""
                    If YourLoadTargetId <> Int64.MinValue Then SubQuery += " and Loads.nCityCode=" & YourLoadTargetId & ""

                    'کووری اصلی
                    Dim SqlQuery =
                        "Select Provinces.ProvinceName,Provinces.ProvinceId,myLoads.* from 
                           (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,Products.strGoodName as GoodTitle,Loads.nTonaj as Tonaj,
                                   LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,
                                   LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
                                   Loads.nCarNumKol as Total,Loads.nCarNum as Reminder,Loads.StrPriceSug as Tariff,LoadStatuses.LoadStatusName,Loads.dDateElam as LoadIssueDate,Loads.dTimeElam as LoadIssueTime, 
                                   Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGTitle,Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,
                                   SoftwareUsers.UserName,Loads.TPTParamsJoint as TPTParamsJoint
                            from DBTransport.dbo.TbElam as Loads
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On Loads.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                               Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                               Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
                               Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Loads.LoadStatus=LoadStatuses.LoadStatusId
                               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Loads.nUserId=SoftwareUsers.UserId
                            Where Loads.nUserID=" & YourSoftwareUser.UserId & "" & SubQuery & ") as myLoads
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  
                         Order By ProvinceName,MyLoads.TargetCityTitle 
                         for JSON Auto"
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           SqlQuery, 0, DSLoads, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    Return InstancePublicProcedures.GetIntegratedJson(DSLoads)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadsforaAdministrator(YourAnnouncementGroupId As Int64, YourAnnouncementSubGroupId As Int64, Optional YourTransportCompanyId As Int64 = Int64.MinValue, Optional YourInventory As Boolean = False, Optional YourShamsiDate As String = "", Optional YourLoadStatusId As Int64 = Int64.MinValue, Optional YourLoadSourceId As Int64 = Int64.MinValue, Optional YourLoadTargetId As Int64 = Int64.MinValue) As String
                Try
                    Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DSLoads As DataSet = Nothing
                    'کامپوزیت ساب کووری
                    Dim SubQuery As String = String.Empty
                    If YourAnnouncementSubGroupId <> Int64.MinValue Then SubQuery += " and Loads.AHSGId=" & YourAnnouncementSubGroupId & ""
                    If YourTransportCompanyId <> Int64.MinValue Then SubQuery += " and Loads.nCompCode=" & YourTransportCompanyId & ""
                    If YourInventory = Boolean.FalseString Then SubQuery += " and Loads.nCarNumKol>0"
                    If YourShamsiDate = String.Empty Then
                        SubQuery += " and Loads.dDateElam=R2Primary.dbo.BPTCOGregorianToPersian(GETDATE())"
                    Else
                        SubQuery += " and Loads.dDateElam='" & YourShamsiDate & "'"
                    End If
                    If YourLoadStatusId <> Int64.MinValue Then SubQuery += " and Loads.LoadStatus=" & YourLoadStatusId & ""
                    If YourLoadSourceId <> Int64.MinValue Then SubQuery += " and Loads.nBarSource=" & YourLoadSourceId & ""
                    If YourLoadTargetId <> Int64.MinValue Then SubQuery += " and Loads.nCityCode=" & YourLoadTargetId & ""

                    'کووری اصلی
                    Dim SqlQuery =
                        "Select Provinces.ProvinceName,Provinces.ProvinceId,myLoads.* from 
                           (Select Loads.nEstelamId LoadId,TransportCompanies.TCTitle as TCTitle,Products.strGoodName as GoodTitle,Loads.nTonaj as Tonaj,
                                   LoadSources.strCityName as SoureCityTitle,LoadTargets.strCityName as TargetCityTitle,LoadTargets.nProvince as ProvinceId,
                                   LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
                                   Loads.nCarNumKol as Total,Loads.nCarNum as Reminder,Loads.StrPriceSug as Tariff,LoadStatuses.LoadStatusName,Loads.dDateElam as LoadIssueDate,Loads.dTimeElam as LoadIssueTime, 
                                   Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGTitle,Loads.StrBarName as Recipient,Loads.StrAddress as Address,Loads.strDescription as Description,
                                   SoftwareUsers.UserName,Loads.TPTParamsJoint as TPTParamsJoint
                            from DBTransport.dbo.TbElam as Loads
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On Loads.AHSGId=AnnouncementSubGroups.AnnouncementSGId 
                               Inner Join dbtransport.dbo.tbProducts as Products On Loads.nBarcode=Products.strGoodCode 
                               Inner Join dbtransport.dbo.tbCity as LoadSources On Loads.nBarSource=LoadSources.nCityCode
                               Inner Join dbtransport.dbo.tbCity as LoadTargets On Loads.nCityCode=LoadTargets.nCityCode 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                               Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Loads.LoadStatus=LoadStatuses.LoadStatusId
                               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Loads.nUserId=SoftwareUsers.UserId
                            Where 3=3 " & SubQuery & ") as myLoads
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces On MyLoads.ProvinceId=Provinces.ProvinceId  
                         Order By ProvinceName,MyLoads.TargetCityTitle 
                         for JSON Auto"
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           SqlQuery, 0, DSLoads, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                    Return InstancePublicProcedures.GetIntegratedJson(DSLoads)
                Catch ex As AnyNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadForLoadAllocationProccess(YournEstelamId As Int64) As R2CoreTransportationAndLoadNotificationLoadForLoadAllocationProccess
                Try
                    Dim DS As New DataSet
                    Dim InstanseSqlDataBOX = New R2CoreSqlDataBOXManager
                    If InstanseSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select Loads.nEstelamID,loads.AHId,Loads.AHSGId,loads.LoadStatus From DBTransport.dbo.TbElam as Loads Where nEstelamId=" & YournEstelamId & "", 180, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New LoadForLoadAllocationProcessNotFoundException
                    End If
                    Return New R2CoreTransportationAndLoadNotificationLoadForLoadAllocationProccess With {.LoadId = YournEstelamId, .AnnouncementGroupId = DS.Tables(0).Rows(0).Item("AHId"), .AnnouncementSubGroupId = DS.Tables(0).Rows(0).Item("AHSGId"), .LoadStatusId = DS.Tables(0).Rows(0).Item("LoadStatus")}
                Catch ex As LoadForLoadAllocationProcessNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoadSimpleModel(YourLoadId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoadSimpleModel
                Try
                    Dim DS As New DataSet
                    Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsParametersManager

                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                            Select Loads.nEstelamID as LoadId,Loads.dDateElam as AnnounceDate ,Loads.dTimeElam as AnnounceTime,Loads.nCompCode as TransportCompanyId,
                                   loads.nBarcode as GoodId,Loads.AHId as AnnouncementGroupId,Loads.AHSGId as AnnouncementSubGroupId,loads.nBarSource as SourceCityId,loads.nCityCode as TargetCityId,
                                   Loads.LoadingPlaceId as LoadingPlaceId,Loads.DischargingPlaceId as DischargingPlaceId,Loads.nCarNumKol as TotalNumber,Loads.nTonaj as Tonaj,Loads.strPriceSug as Tariff,
                                   Loads.strBarName as Recipient,Loads.strAddress as Address,Loads.strDescription as Description,Loads.LoadStatus as LoadStatusId,Loads.TPTParams as TPTParams,Loads.TPTParamsJoint as TPTParamsJoint,Loads.nUserID as UserId
                            From DBTransport.dbo.tbElam as Loads
                            Where Loads.nEstelamID=" & YourLoadId & "")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(DS) = 0 Then Throw New LoadNotFoundException
                    Else
                        Dim InstanseSqlDataBOX = New R2CoreSqlDataBOXManager
                        If InstanseSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           "Select Loads.nEstelamID as LoadId,Loads.dDateElam as AnnounceDate ,Loads.dTimeElam as AnnounceTime,Loads.nCompCode as TransportCompanyId,
                                   loads.nBarcode as GoodId,Loads.AHId as AnnouncementGroupId,Loads.AHSGId as AnnouncementSubGroupId,loads.nBarSource as SourceCityId,loads.nCityCode as TargetCityId,
                                   Loads.LoadingPlaceId as LoadingPlaceId,Loads.DischargingPlaceId as DischargingPlaceId,Loads.nCarNumKol as TotalNumber,Loads.nTonaj as Tonaj,Loads.strPriceSug as Tariff,
                                   Loads.strBarName as Recipient,Loads.strAddress as Address,Loads.strDescription as Description,Loads.LoadStatus as LoadStatusId,Loads.TPTParams as TPTParams,Loads.TPTParamsJoint as TPTParamsJoint,Loads.nUserID as UserId
                            From DBTransport.dbo.tbElam as Loads
                            Where Loads.nEstelamID=" & YourLoadId & "", 180, DS, New Boolean).GetRecordsCount = 0 Then
                            Throw New LoadNotFoundException
                        End If
                    End If
                    Dim Load = New R2CoreTransportationAndLoadNotificationLoadSimpleModel With
                    {.LoadId = DS.Tables(0).Rows(0).Item("LoadId"), .AnnounceDate = DS.Tables(0).Rows(0).Item("AnnounceDate").trim, .AnnounceTime = DS.Tables(0).Rows(0).Item("AnnounceTime").trim, .TransportCompanyId = DS.Tables(0).Rows(0).Item("TransportCompanyId"),
                     .GoodId = DS.Tables(0).Rows(0).Item("GoodId"), .AnnouncementGroupId = DS.Tables(0).Rows(0).Item("AnnouncementGroupId"), .AnnouncementSubGroupId = DS.Tables(0).Rows(0).Item("AnnouncementSubGroupId"),
                     .SourceCityId = DS.Tables(0).Rows(0).Item("SourceCityId"), .TargetCityId = DS.Tables(0).Rows(0).Item("TargetCityId"), .LoadingPlaceId = DS.Tables(0).Rows(0).Item("LoadingPlaceId"),
                     .DischargingPlaceId = DS.Tables(0).Rows(0).Item("DischargingPlaceId"), .TotalNumber = DS.Tables(0).Rows(0).Item("TotalNumber"), .Tonaj = DS.Tables(0).Rows(0).Item("Tonaj"), .Tariff = DS.Tables(0).Rows(0).Item("Tariff"), .Recipient = DS.Tables(0).Rows(0).Item("Recipient").trim, .Address = DS.Tables(0).Rows(0).Item("Address").trim, .Description = DS.Tables(0).Rows(0).Item("Description").trim,
                     .LoadStatusId = DS.Tables(0).Rows(0).Item("LoadStatusId"), .TPTParams = DS.Tables(0).Rows(0).Item("TPTParams").trim, .TPTParamsJoint = DS.Tables(0).Rows(0).Item("TPTParamsJoint").trim, .UserId = DS.Tables(0).Rows(0).Item("UserId")}
                    Return Load
                Catch ex As LoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetLoad(YournEstelamId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoad
                Try
                    Dim DS As New DataSet

                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                            Select Loads.nEstelamID as LoadId,SoftwareUsers.UserName,Loads.dDateElam as AnnounceDate ,Loads.dTimeElam as AnnounceTime,TransportCompanies.TCId as TransportCompanyId,TransportCompanies.TCTitle as TransportCompanyTitle,
                                   Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Announcements.AnnouncementId as LoadAnnouncementGroupId,Announcements.AnnouncementTitle as LoadAnnouncementGroupTitle,
                       	           AnnouncementsubGroups.AnnouncementSGId as LoadAnnouncementSubGroupId,AnnouncementsubGroups.AnnouncementSGTitle as LoadAnnouncementSubGroupTitle ,
	                               SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,
	                               LoadingPlaces.LADPlaceId as LoadingPlaceId,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceId as DischargingPlaceId,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
	                               Loads.nCarNumKol as TotalNumber,Loads.nTonaj as Tonaj,Loads.strPriceSug as Tariff,Loads.strBarName as Recipient,Loads.strAddress as Address,Loads.strDescription as Description,
                                   LoadStatuses.LoadStatusId as LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Loads.TPTParams as TPTParams,Loads.TPTParamsJoint
                            from DBTransport.dbo.tbElam as Loads
  							       Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Loads.nUserID=SoftwareUsers.UserId  
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                                   Inner Join DBTransport.dbo.tbProducts as Goods On Loads.nBarcode=Goods.strGoodCode 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Loads.AHSGId=AnnouncementsubGroups.AnnouncementSGId
                                   Inner Join DBTransport.dbo.tbCity as TargetCities On Loads.nCityCode=TargetCities.nCityCode 
                                   Inner Join DBTransport.dbo.tbCity as SourceCities On Loads.nBarSource=SourceCities.nCityCode 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Loads.LoadStatus=LoadStatuses.LoadStatusId 
                            Where Loads.nEstelamID=" & YournEstelamId & "")
                        Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                        If Da.Fill(DS) = 0 Then Throw New LoadNotFoundException
                    Else
                        Dim InstanseSqlDataBOX = New R2CoreSqlDataBOXManager
                        If Not InstanseSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select Loads.nEstelamID as LoadId,SoftwareUsers.UserName,Loads.dDateElam as AnnounceDate ,Loads.dTimeElam as AnnounceTime,TransportCompanies.TCId as TransportCompanyId,TransportCompanies.TCTitle as TransportCompanyTitle,
                                   Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Announcements.AnnouncementId as LoadAnnouncementGroupId,Announcements.AnnouncementTitle as LoadAnnouncementGroupTitle,
                       	           AnnouncementsubGroups.AnnouncementSGId as LoadAnnouncementSubGroupId,AnnouncementsubGroups.AnnouncementSGTitle as LoadAnnouncementSubGroupTitle ,
	                               SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,
	                               LoadingPlaces.LADPlaceId as LoadingPlaceId,LoadingPlaces.LADPlaceTitle as LoadingPlaceTitle,DischargingPlaces.LADPlaceId as DischargingPlaceId,DischargingPlaces.LADPlaceTitle as DischargingPlaceTitle,
	                               Loads.nCarNumKol as TotalNumber,Loads.nTonaj as Tonaj,Loads.strPriceSug as Tariff,Loads.strBarName as Recipient,Loads.strAddress as Address,Loads.strDescription as Description,
                                   LoadStatuses.LoadStatusId as LoadStatusId,LoadStatuses.LoadStatusName as LoadStatusTitle,Loads.TPTParams as TPTParams,Loads.TPTParamsJoint
                            from DBTransport.dbo.tbElam as Loads
                           		   Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Loads.nUserID=SoftwareUsers.UserId  
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On Loads.nCompCode=TransportCompanies.TCId 
                                   Inner Join DBTransport.dbo.tbProducts as Goods On Loads.nBarcode=Goods.strGoodCode 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On Loads.AHId=Announcements.AnnouncementId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Loads.AHSGId=AnnouncementsubGroups.AnnouncementSGId
                                   Inner Join DBTransport.dbo.tbCity as TargetCities On Loads.nCityCode=TargetCities.nCityCode 
                                   Inner Join DBTransport.dbo.tbCity as SourceCities On Loads.nBarSource=SourceCities.nCityCode 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LoadingPlaces On Loads.LoadingPlaceId=LoadingPlaces.LADPlaceId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as DischargingPlaces On Loads.DischargingPlaceId=DischargingPlaces.LADPlaceId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoadCapacitorLoadStatuses as LoadStatuses On Loads.LoadStatus=LoadStatuses.LoadStatusId 
                            Where Loads.nEstelamID=" & YournEstelamId & "", 120, DS, New Boolean).GetRecordsCount <> 0 Then
                            Throw New LoadNotFoundException
                        End If
                    End If
                    Dim Load = New R2CoreTransportationAndLoadNotificationLoad With
                    {.LoadId = DS.Tables(0).Rows(0).Item("LoadId"), .AnnounceDate = DS.Tables(0).Rows(0).Item("AnnounceDate").trim, .AnnounceTime = DS.Tables(0).Rows(0).Item("AnnounceTime").trim, .TransportCompanyId = DS.Tables(0).Rows(0).Item("TransportCompanyId"), .TransportCompanyTitle = DS.Tables(0).Rows(0).Item("TransportCompanyTitle").trim,
                     .GoodId = DS.Tables(0).Rows(0).Item("GoodId"), .GoodTitle = DS.Tables(0).Rows(0).Item("GoodTitle").trim, .AnnouncementGroupId = DS.Tables(0).Rows(0).Item("LoadAnnouncementGroupId"), .AnnouncementGroupTitle = DS.Tables(0).Rows(0).Item("LoadAnnouncementGroupTitle").trim, .AnnouncementSubGroupId = DS.Tables(0).Rows(0).Item("LoadAnnouncementSubGroupId"), .AnnouncementSubGroupTitle = DS.Tables(0).Rows(0).Item("LoadAnnouncementSubGroupTitle").trim,
                     .SourceCityId = DS.Tables(0).Rows(0).Item("SourceCityId"), .SourceCityTitle = DS.Tables(0).Rows(0).Item("SourceCityTitle").trim, .TargetCityId = DS.Tables(0).Rows(0).Item("TargetCityId"), .TargetCityTitle = DS.Tables(0).Rows(0).Item("TargetCityTitle").trim, .LoadingPlaceId = DS.Tables(0).Rows(0).Item("LoadingPlaceId"), .LoadingPlaceTitle = DS.Tables(0).Rows(0).Item("LoadingPlaceTitle").trim,
                     .DischargingPlaceId = DS.Tables(0).Rows(0).Item("DischargingPlaceId"), .DischargingPlaceTitle = DS.Tables(0).Rows(0).Item("DischargingPlaceTitle").trim, .TotalNumber = DS.Tables(0).Rows(0).Item("TotalNumber"), .Tonaj = DS.Tables(0).Rows(0).Item("Tonaj"), .Tariff = DS.Tables(0).Rows(0).Item("Tariff"), .Recipient = DS.Tables(0).Rows(0).Item("Recipient").trim, .Address = DS.Tables(0).Rows(0).Item("Address").trim, .Description = DS.Tables(0).Rows(0).Item("Description").trim,
                     .LoadStatusId = DS.Tables(0).Rows(0).Item("LoadStatusId"), .LoadStatusTitle = DS.Tables(0).Rows(0).Item("LoadStatusTitle").trim, .TPTParams = DS.Tables(0).Rows(0).Item("TPTParams").trim, .RegisteringUserName = DS.Tables(0).Rows(0).Item("UserName").trim, .TPTParamsJoint = DS.Tables(0).Rows(0).Item("TPTParamsJoint").trim}
                    Return Load
                Catch ex As LoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub LoadTonajValidate(YourLoad As R2CoreTransportationAndLoadNotificationLoad)
                Try
                    'بررسی تناژ بار
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                    Dim AllConfigforAnnouncementGroupId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadCapacitorControl, YourLoad.AnnouncementGroupId), "-")
                    Dim AllConfigforAnnouncementSGId = Split(Mid(AllConfigforAnnouncementGroupId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllConfigforAnnouncementGroupId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsActive As Boolean = AllConfigforAnnouncementSGId(0)
                    Dim LoadTonajMaximum As Decimal = System.Convert.ToDecimal(AllConfigforAnnouncementSGId(1).Replace(",", "."))
                    If IsActive And YourLoad.Tonaj > LoadTonajMaximum Then Throw New LoadTonajInvalidException
                Catch ex As LoadTonajInvalidException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadTotalValidate(YourLoad As R2CoreTransportationAndLoadNotificationLoad)
                Try
                    'بررسی تناژ بار
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                    Dim AllConfigforAnnouncementGroupId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsLoadCapacitorControl, YourLoad.AnnouncementGroupId), "-")
                    Dim AllConfigforAnnouncementSGId = Split(Mid(AllConfigforAnnouncementGroupId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllConfigforAnnouncementGroupId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsActive As Boolean = AllConfigforAnnouncementSGId(0)
                    Dim LoadTotalMaximum As Double = AllConfigforAnnouncementSGId(2)
                    If IsActive And YourLoad.TotalNumber > LoadTotalMaximum Then Throw New LoadTotalInvalidException
                Catch ex As LoadTotalInvalidException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        'BPTChanged
        Namespace Exceptions
            Public Class NoLoadsorLoadsViewConditionsMismatchException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "بار موجود نیست" + vbCrLf + "عدم تطابق صف نوبت ، بار و وضعیت بار"
                    End Get
                End Property
            End Class

            Public Class LoadStatusesForSoftwareUserTypeNotFoundException
                Inherits BPTException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "برای نوع کاربری مورد نظر لیست وضعیت بار وجود ندارد"
                    End Get
                End Property
            End Class

            Public Class LoadForLoadAllocationProcessNotFoundException
                Inherits BPTException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "بار با کد ارسالی به منظور تخصیص بار وجود ندارد"
                    End Get
                End Property
            End Class

            Public Class LoadNotFoundException
                Inherits BPTException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "بار با کد مورد نظر یافت نشد"
                    End Get
                End Property
            End Class

            Public Class LoadTonajInvalidException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تناژ بار بیش از حد مجاز است"
                    End Get
                End Property
            End Class

            Public Class LoadTotalInvalidException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "مقدار یا تعداد بار مجاز نیست"
                    End Get
                End Property
            End Class

            Public Class AnyLoadNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "باری در سامانه موجود نیست"
                    End Get
                End Property
            End Class

            Public Class AnyLoadNotFoundAndPleaseRetryException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "باری در سامانه موجود نیست" + vbCrLf + "لطفا مجددا تلاش نمایید"
                    End Get
                End Property
            End Class

        End Namespace

    End Namespace

    Namespace LoadCapacitorLoadManipulation
        Public Class R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManipulationManager
            Private _DateTime As New R2DateTime

            'Public Function LoadCapacitorLoadRegistering(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
            '        Dim InstanceLoadingAndDischargingPlaces = New R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces.R2CoreTransportationAndLoadNotificationMClassLoadingAndDischargingPlacesManager
            '        Dim InstanceSpecializedPersianCalendar = New R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager(_DateTime)
            '        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
            '        Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
            '        Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
            '        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
            '        Dim InstanceTransportTariffs = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager
            '        Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
            '        Dim InstanceLoadTargets = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
            '        Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsParametersManager
            '        Dim NSSAnnouncementHall = InstanceAnnouncements.GetAnnouncementHallfromLoadCapacitorLoad(YourNSS)
            '        Dim NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        Dim NSSLoadTargets = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
            '        Dim InstancePermissions = New R2Core.PermissionManagement.R2CoreInstansePermissionsManager
            '        YourNSS.AHId = NSSAnnouncementHall.AHId
            '        YourNSS.AHSGId = NSSAnnouncementsubGroup.AHSGId

            '        'کنترل فعال بودن مبدا بارگیری و محل تخلیه
            '        If InstanceLoadingAndDischargingPlaces.IsActiveLoadingPlace(YourNSS.LoadingPlaceId) = False Then Throw New LoadingPlaceIsUnActiveException
            '        If InstanceLoadingAndDischargingPlaces.IsActiveDischargingPlace(YourNSS.DischargingPlaceId) = False Then Throw New DischargingPlaceIsUnActiveException

            '        'کنترل فعال بودن مبدا و مقصد
            '        If (Not NSSLoadTargets.GetNSSLoadTarget(YourNSS.nCityCode).NSSCity.Active) Or (Not NSSLoadTargets.GetNSSLoadTarget(YourNSS.nBarSource).NSSCity.Active) Then Throw New LoadTargetIsUnActiveException

            '        'کنترل تناژ بار 
            '        If YourNSS.nTonaj > InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 7) Then If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRegisterOrEditLoadsAnyTonaj, YourNSS.nUserId, 0) Then Throw New LoadTonajInvalidException

            '        'Try
            '        '    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
            '        '    InstanceLoadCapacitorLoad.LoadCapacitorLoadTonajValidate(YourNSS)
            '        'Catch ex As Exception
            '        '    Dim InstnaceLogging = New R2CoreInstanceLoggingManager
            '        '    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager
            '        '    InstnaceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Warn, InstnaceLogging.GetNSSLogType(R2CoreLogType.Warn).LogTitle, ex.Message, String.Empty, String.Empty, String.Empty, String.Empty, InstanceSoftwareUsers.GetSystemUserId(), _DateTime.GetCurrentDateTimeMilladi(), Nothing))
            '        'End Try
            '        'بررسی تطابق استان انتخاب شده با زیرگروه اعلام بار
            '        'If Not InstanceAnnouncements.HasRelationBetweenProvinceAndAnnouncementsubGrou(InstanceLoadTargets.GetNSSLoadTarget(YourNSS.nCityCode).NSSCity.nProvince, YourNSS.AHSGId) Then Throw New HasNotRelationBetweenProvinceAndAnnouncementsubGroup
            '        'بررسی اینکه آیا برای زیرگروه مربوطه امکان ثبت بار وجود دارد یا نه
            '        If Not ISActiveLoadCapacitorLoadRegistering(YourNSS) Then Throw New LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
            '        'کنترل تعداد بار
            '        If YourNSS.nCarNumKol > InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, 0) Then Throw New LoadTotalInvalidException
            '        If YourNSS.nCarNumKol < 1 Then Throw New LoadCapacitorLoadnCarNumKolCanNotBeZeroException
            '        'کنترل اکتیو بودن شرکت حمل و نقل
            '        If InstanceTransportCompanies.ISTransportCompanyActive(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(YourNSS.nCompCode, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)) = False Then Throw New TransportCompanyISNotActiveException
            '        'بررسی بار فردا
            '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
            '        Dim AllTommorowConfigforAHId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
            '        Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
            '        Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
            '        Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
            '        Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
            '        Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
            '        Dim TommorowLoadRegisteringFlag As Boolean = False
            '        If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) Then
            '            TommorowLoadRegisteringFlag = True
            '            'If InstanceSpecializedPersianCalendar.IsTomorrowIsHolidayforLoadAnnounce Then If Not IsActiveLoadCapacitorLoadRegisteringInHoliDay(YourNSS) Then Throw New LoadCapacitorLoadRegisteringInHolidayNotAllowedException(NSSAnnouncementsubGroup.AHSGTitle)
            '        Else
            '            If InstanceSpecializedPersianCalendar.IsTodayIsHolidayforLoadAnnounce Then If Not IsActiveLoadCapacitorLoadRegisteringInHoliDay(YourNSS) Then Throw New LoadCapacitorLoadRegisteringInHolidayNotAllowedException(NSSAnnouncementsubGroup.AHSGTitle)
            '            Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
            '            If InstanceAnnouncementTiming.IsTimingActive(YourNSS.AHId, YourNSS.AHSGId) Then
            '                Dim Timing = InstanceAnnouncementTiming.GetTiming(YourNSS.AHId, YourNSS.AHSGId, _DateTime.GetCurrentTime)
            '                If _DateTime.GetCurrentTime() > InstanceAnnouncements.GetAnnouncementHallLeastAnnounceTime(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId).Time Then
            '                    Throw New LoadCapacitorLoadRegisterTimePassedException
            '                End If
            '                If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering Or
            '                   Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
            '                    Throw New LoadCapacitorLoadRegisterTimePassedException
            '                End If
            '            Else
            '                'کنترل اتمام زمان ثبت بار
            '                If _DateTime.GetCurrentTime() > InstanceAnnouncements.GetAnnouncementHallLeastAnnounceTime(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId).Time Then
            '                    Throw New LoadCapacitorLoadRegisterTimePassedException
            '                End If
            '            End If
            '        End If
            '        'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
            '        Dim Tariff As Int64
            '        Try
            '            Tariff = InstanceTransportTariffs.GetNSSTransportTariff(YourNSS).Tariff
            '            Tariff = InstanceTransportTariffs.GetUltimateTransportTariff(YourNSS.AHSGId, YourNSS.nTonaj, Tariff)
            '            Tariff = Tariff + InstanceTransportTariffsParameters.GetTotalofTransportTariffsParameters(YourNSS)
            '        Catch exx As TransportPriceTariffNotFoundException
            '        End Try

            '        'ثبت بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        CmdSql.CommandText = "Select Top 1 nEstelamId From dbtransport.dbo.TbElam with (tablockx) Order By nEstelamId Desc"
            '        CmdSql.ExecuteNonQuery()
            '        CmdSql.CommandText = "Select IDENT_CURRENT('dbtransport.dbo.TbElam') "
            '        Dim nEstelamIdNew As Int64 = CmdSql.ExecuteScalar() + 1
            '        Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
            '        Dim P As SqlClient.SqlParameter
            '        P = New SqlClient.SqlParameter("@nEstelamKey", SqlDbType.NVarChar) : P.Value = Hasher.GenerateSHA256String(nEstelamIdNew)
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrBarName", SqlDbType.VarChar) : P.Value = YourNSS.StrBarName
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCityCode", SqlDbType.Int) : P.Value = YourNSS.nCityCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nTonaj", SqlDbType.Float) : P.Value = YourNSS.nTonaj
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nBarCode", SqlDbType.Int) : P.Value = YourNSS.nBarCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@bFlag", SqlDbType.Bit) : P.Value = False
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCompCode", SqlDbType.Int) : P.Value = YourNSS.nCompCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@bFlagbarType", SqlDbType.Bit) : P.Value = False
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nTruckType", SqlDbType.Int) : P.Value = YourNSS.nTruckType
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCarNum", SqlDbType.Int) : P.Value = YourNSS.nCarNumKol
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrAddress", SqlDbType.VarChar) : P.Value = YourNSS.StrAddress
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nUserId", SqlDbType.Int) : P.Value = YourNSS.nUserId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@dDateElam", SqlDbType.Char) : P.Value = _DateTime.GetCurrentShamsiDate()
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@dTimeElam", SqlDbType.Char) : P.Value = _DateTime.GetCurrentTime()
            '        CmdSql.Parameters.Add(P)
            '        CmdSql.Parameters.AddWithValue("@bflagCarNum", 0)
            '        CmdSql.Parameters.AddWithValue("@strIssueNo", 0)
            '        CmdSql.Parameters.AddWithValue("@strIssueOwner", 0)
            '        CmdSql.Parameters.AddWithValue("@nTonajKol", 0)
            '        P = New SqlClient.SqlParameter("@nCarNumKol", SqlDbType.Int) : P.Value = YourNSS.nCarNumKol
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrPriceSug", SqlDbType.VarChar) : P.Value = Tariff
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrDescription", SqlDbType.VarChar) : P.Value = YourNSS.StrDescription
            '        CmdSql.Parameters.Add(P)
            '        CmdSql.Parameters.AddWithValue("@dDateExit", DBNull.Value)
            '        CmdSql.Parameters.AddWithValue("@dTimeExit", DBNull.Value)
            '        If TommorowLoadRegisteringFlag Then
            '            P = New SqlClient.SqlParameter("@LoadStatus", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow
            '        Else
            '            P = New SqlClient.SqlParameter("@LoadStatus", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered
            '        End If
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nBarSource", SqlDbType.Int) : P.Value = YourNSS.nBarSource
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@AHId", SqlDbType.BigInt) : P.Value = NSSAnnouncementHall.AHId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@AHSGId", SqlDbType.BigInt) : P.Value = NSSAnnouncementsubGroup.AHSGId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@TPTParams", SqlDbType.VarChar) : P.Value = YourNSS.TPTParams
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@LoadingPlaceId", SqlDbType.BigInt) : P.Value = YourNSS.LoadingPlaceId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@DischargingPlaceId", SqlDbType.BigInt) : P.Value = YourNSS.DischargingPlaceId
            '        CmdSql.Parameters.Add(P)
            '        CmdSql.CommandText = "Insert Into dbtransport.dbo.TbElam Values(@nEstelamKey,@StrBarName,@nCityCode,@nTonaj,@nBarCode,@bFlag,@nCompCode,@bFlagbarType,@nTruckType,@nCarNum,@StrAddress,@nUserId,@dDateElam,@dTimeElam,@bflagCarNum,@strIssueNo,@strIssueOwner,@nTonajKol,@nCarNumKol,@nBarSource,@StrPriceSug,@StrDescription,@dDateExit,@dTimeExit,@LoadStatus,@AHId,@AHSGId,@TPTParams,@LoadingPlaceId,@DischargingPlaceId)"
            '        CmdSql.ExecuteNonQuery()
            '        'ثبت اکانت
            '        If TommorowLoadRegisteringFlag Then
            '            InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(nEstelamIdNew, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.RegisteringforTommorow, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourNSS.nUserId))
            '        Else
            '            InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(nEstelamIdNew, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Registering, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourNSS.nUserId))
            '        End If
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

            '        'ثبت بار اعتباری
            '        'Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
            '        'If InstanceTurnCancellation.IsLoadforTurnCancellation(InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(nEstelamIdNew, True)) Then
            '        '    InstanceTurnCancellation.LoadforTurnCancellationRegistering(nEstelamIdNew, True)
            '        'Else
            '        '    InstanceTurnCancellation.LoadforTurnCancellationRegistering(nEstelamIdNew, False)
            '        'End If

            '        'ارسال کد مخزن بار
            '        Return nEstelamIdNew
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is TransportCompanyISNotActiveException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisterTimePassedException _
            '                        OrElse TypeOf ex Is TimingNotReachedException _
            '                        OrElse TypeOf ex Is HasNotRelationBetweenProvinceAndAnnouncementsubGroup _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException _
            '                        OrElse TypeOf ex Is TransportPriceTariffParameterDetailNotFoundException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisteringInHolidayNotAllowedException _
            '                        OrElse TypeOf ex Is LoadTonajInvalidException _
            '                        OrElse TypeOf ex Is LoadingPlaceIsUnActiveException _
            '                        OrElse TypeOf ex Is DischargingPlaceIsUnActiveException _
            '                        OrElse TypeOf ex Is LoadingAndDischargingPlaceNotFoundException _
            '                        OrElse TypeOf ex Is LoadTonajInvalidException _
            '                        OrElse TypeOf ex Is LoadSourceIsUnActiveException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            'Public Sub LoadCapacitorLoadReRegistering(YournEstelamId As Int64, YourSoftwareUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim myCurrentTime = _DateTime.GetCurrentTime()
            '        Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
            '        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
            '        Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
            '        Dim NSSCurrentLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
            '        'کنترل وضعیت بار
            '        If NSSCurrentLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
            '        'کنترل تاریخ اعلام بار
            '        If NSSCurrentLoadCapacitorLoad.dDateElam <> _DateTime.GetCurrentShamsiDate Then Throw New LoadCapacitorLoadReRegisteringNotAllowedException
            '        'کنترل تعداد بار
            '        If NSSCurrentLoadCapacitorLoad.nCarNum < 1 Then Throw New LoadCapacitorLoadReRegisteringNotAllowedBecuasenCarNumException
            '        'کنترل تایم اعلام بار
            '        Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
            '        If InstanceAnnouncementTiming.IsTimingActive(NSSCurrentLoadCapacitorLoad.AHId, NSSCurrentLoadCapacitorLoad.AHSGId) Then
            '            Dim Timing = InstanceAnnouncementTiming.GetTiming(NSSCurrentLoadCapacitorLoad.AHId, NSSCurrentLoadCapacitorLoad.AHSGId, myCurrentTime)
            '            If myCurrentTime > InstanceAnnouncements.GetAnnouncementHallLeastAnnounceTime(NSSCurrentLoadCapacitorLoad.AHId, NSSCurrentLoadCapacitorLoad.AHSGId).Time Then
            '                Throw New LoadCapacitorLoadReRegisteringTimePassedException
            '            End If
            '            If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering Or
            '                   Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
            '                Throw New LoadCapacitorLoadReRegisteringTimePassedException
            '            End If
            '        Else
            '            'کنترل اتمام زمان ثبت بار
            '            If myCurrentTime > InstanceAnnouncements.GetAnnouncementHallLeastAnnounceTime(NSSCurrentLoadCapacitorLoad.AHId, NSSCurrentLoadCapacitorLoad.AHSGId).Time Then
            '                Throw New LoadCapacitorLoadRegisterTimePassedException
            '            End If
            '        End If

            '        'تراکنش اعلام مجدد بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        Dim P As SqlClient.SqlParameter
            '        P = New SqlClient.SqlParameter("@dTimeElam", SqlDbType.Char) : P.Value = myCurrentTime
            '        CmdSql.Parameters.Add(P)
            '        CmdSql.CommandText = "Update dbtransport.dbo.TbElam Set bFlag=0,bflagCarNum=1,dTimeElam=@dTimeElam,LoadStatus=1 Where nEstelamId=" & NSSCurrentLoadCapacitorLoad.nEstelamId & ""
            '        CmdSql.ExecuteNonQuery()
            '        InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSCurrentLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.ReRegisteringSedimentedLoad, NSSCurrentLoadCapacitorLoad.nCarNum, Nothing, Nothing, Nothing, YourSoftwareUserNSS.UserId))
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

            '    Catch ex As LoadCapacitorLoadRegisterTimePassedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As LoadCapacitorLoadReRegisteringTimePassedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As LoadCapacitorLoadReRegisteringNotAllowedBecuasenCarNumException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As LoadCapacitorLoadReRegisteringNotAllowedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As LoadCapacitorLoadNotFoundException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            'Public Sub LoadCapacitorLoadEditing(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim InstanceLoadingAndDischargingPlaces = New R2CoreTransportationAndLoadNotificationMClassLoadingAndDischargingPlacesManager
            '        Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
            '        Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
            '        Dim InstanceConfigurations = New R2CoreInstanceConfigurationManager
            '        Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationInstanceTransportCompaniesManager
            '        Dim InstanceTransportTariffs = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager
            '        Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
            '        Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
            '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
            '        Dim InstanceLoadTargets = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
            '        Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationInstanceTransportTariffsParametersManager
            '        Dim NSSCurrentLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YourNSS.nEstelamId, True)
            '        Dim NSSAnnouncementHall = InstanceAnnouncements.GetAnnouncementHallfromLoadCapacitorLoad(YourNSS)
            '        Dim NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        Dim NSSLoadTargets = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager

            '        YourNSS.AHId = NSSAnnouncementHall.AHId
            '        YourNSS.AHSGId = NSSAnnouncementsubGroup.AHSGId
            '        YourNSS.LoadStatus = NSSCurrentLoadCapacitorLoad.LoadStatus

            '        'کنترل فعال بودن مبدا بارگیری و محل تخلیه
            '        If InstanceLoadingAndDischargingPlaces.IsActiveLoadingPlace(YourNSS.LoadingPlaceId) = False Then Throw New LoadingPlaceIsUnActiveException
            '        If InstanceLoadingAndDischargingPlaces.IsActiveDischargingPlace(YourNSS.DischargingPlaceId) = False Then Throw New DischargingPlaceIsUnActiveException

            '        'کنترل فعال بودن مبدا و مقصد
            '        If (Not NSSLoadTargets.GetNSSLoadTarget(YourNSS.nCityCode).NSSCity.Active) Or (Not NSSLoadTargets.GetNSSLoadTarget(YourNSS.nBarSource).NSSCity.Active) Then Throw New LoadTargetIdNotFoundException

            '        'کنترل تناژ بار
            '        If YourNSS.nTonaj > InstanceConfigurations.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 7) Then If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRegisterOrEditLoadsAnyTonaj, R2CoreGUIMClassGUIManagement.FrmMainMenu.UcUserImage.UCCurrentNSS.UserId, 0) Then Throw New LoadTonajInvalidException

            '        'Try
            '        '    'If YourNSS.nTonaj > 27.5 Then Throw New LoadCapacitorLoadTonajExceededException
            '        '    InstanceLoadCapacitorLoad.LoadCapacitorLoadTonajValidate(YourNSS)
            '        'Catch ex As Exception
            '        '    Dim InstnaceLogging = New R2CoreInstanceLoggingManager
            '        '    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager
            '        '    InstnaceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Warn, InstnaceLogging.GetNSSLogType(R2CoreLogType.Warn).LogTitle, ex.Message, String.Empty, String.Empty, String.Empty, String.Empty, InstanceSoftwareUsers.GetSystemUserId(), _DateTime.GetCurrentDateTimeMilladi(), Nothing))
            '        'End Try

            '        'بررسی تطابق استان انتخاب شده با زیرگروه اعلام بار
            '        'If Not InstanceAnnouncements.HasRelationBetweenProvinceAndAnnouncementsubGroup(InstanceLoadTargets.GetNSSLoadTarget(YourNSS.nCityCode).NSSCity.nProvince, YourNSS.AHSGId) Then Throw New HasNotRelationBetweenProvinceAndAnnouncementsubGroup

            '        'ویرایش گروه اصلی اعلام بار امکان پذیر نیست در صورتی که کاربر اشتباه کرده باشد باید بار را کامل حذف کند یک بار دیگر ثبت نماید
            '        'If NSSCurrentLoadCapacitorLoad.AHId <> YourNSS.AHId Then Throw New LoadCapacitorLoadEditingChangeAHIdNotAllowedException

            '        'بررسی اینکه آیا برای زیرگروه مربوطه امکان ثبت بار وجود دارد یا نه
            '        If Not ISActiveLoadCapacitorLoadRegistering(YourNSS) Then Throw New LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException

            '        'باری که مجددا اعلام بار شده است قابل ویرایش نیست
            '        'If NSSCurrentLoadCapacitorLoad.ReRegistered Then Throw New EditOrDeleteReRegisteredLoadNotAllowedException

            '        'بررسی بار فردا
            '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
            '        Dim AllTommorowConfigforAHId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
            '        Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
            '        Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
            '        Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
            '        Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
            '        Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
            '        'کنترل اتمام زمان ویرایش بار
            '        If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) And YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then
            '        Else
            '            Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
            '            If InstanceAnnouncementTiming.IsTimingActive(YourNSS.AHId, YourNSS.AHSGId) Then
            '                Dim Timing = InstanceAnnouncementTiming.GetTiming(YourNSS.AHId, YourNSS.AHSGId, _DateTime.GetCurrentTime)
            '                If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanEditLoadCapacitorLoadAfterLoadAnnounce, YourUserNSS.UserId, 0) Then
            '                    'امکان ویرایش بار هنگام تخصیص بار فقط توسط کاربر خاص که مجوز دارد
            '                Else
            '                    If InstanceAnnouncements.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, NSSCurrentLoadCapacitorLoad.dTimeElam)) Then
            '                        Throw New LoadCapacitorLoadEditTimePassedException
            '                    End If
            '                End If
            '            End If
            '        End If

            '        'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
            '        Dim Tariff As Int64
            '        Try
            '            Tariff = InstanceTransportTariffs.GetNSSTransportTariff(YourNSS).Tariff
            '            Tariff = InstanceTransportTariffs.GetUltimateTransportTariff(YourNSS.AHSGId, YourNSS.nTonaj, Tariff)
            '            Tariff = Tariff + InstanceTransportTariffsParameters.GetTotalofTransportTariffsParameters(YourNSS)
            '        Catch exx As TransportPriceTariffNotFoundException
            '        End Try

            '        'کنترل وضعیت بار
            '        If YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

            '        'تنظیم مقدار باقیمانده بار
            '        Dim ReminderTotal As Int64 = 0
            '        Dim LastNSS = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YourNSS.nEstelamId, True)
            '        If LastNSS.nCarNumKol = YourNSS.nCarNumKol Then
            '            ReminderTotal = LastNSS.nCarNum
            '        Else
            '            ReminderTotal = YourNSS.nCarNumKol
            '        End If

            '        'کنترل محل تخلیه بار 
            '        If YourNSS.DischargingPlaceId = LoadingAndDischargingPlaces.LoadingAndDischargingPlaces.None Then Throw New DataEntryException

            '        'SqlInjection
            '        Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
            '        InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.StrAddress)
            '        InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.StrBarName)
            '        InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.StrDescription)

            '        'ویرایش بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        Dim P As SqlClient.SqlParameter
            '        P = New SqlClient.SqlParameter("@nCompCode", SqlDbType.Int) : P.Value = YourNSS.nCompCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nBarSource", SqlDbType.Int) : P.Value = YourNSS.nBarSource
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@AHId", SqlDbType.BigInt) : P.Value = NSSAnnouncementHall.AHId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@AHSGId", SqlDbType.BigInt) : P.Value = NSSAnnouncementsubGroup.AHSGId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrBarName", SqlDbType.VarChar) : P.Value = YourNSS.StrBarName
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCityCode", SqlDbType.Int) : P.Value = YourNSS.nCityCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nTonaj", SqlDbType.Float) : P.Value = YourNSS.nTonaj
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nBarCode", SqlDbType.Int) : P.Value = YourNSS.nBarCode
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nTruckType", SqlDbType.Int) : P.Value = YourNSS.nTruckType
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCarNum", SqlDbType.Int) : P.Value = ReminderTotal
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrAddress", SqlDbType.VarChar) : P.Value = YourNSS.StrAddress
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@nCarNumKol", SqlDbType.Int) : P.Value = YourNSS.nCarNumKol
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrPriceSug", SqlDbType.VarChar) : P.Value = Tariff
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@StrDescription", SqlDbType.VarChar) : P.Value = YourNSS.StrDescription
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@TPTParams", SqlDbType.VarChar) : P.Value = YourNSS.TPTParams
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@LoadingPlaceId", SqlDbType.BigInt) : P.Value = YourNSS.LoadingPlaceId
            '        CmdSql.Parameters.Add(P)
            '        P = New SqlClient.SqlParameter("@DischargingPlaceId", SqlDbType.BigInt) : P.Value = YourNSS.DischargingPlaceId
            '        CmdSql.Parameters.Add(P)

            '        If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanEditAnyofLoadCapacitorLoad, YourUserNSS.UserId, 0) Then
            '            CmdSql.CommandText = "Update dbtransport.dbo.TbElam Set nCompCode=@nCompCode,nBarSource=@nBarSource,AHId=@AHId,AHSGId=@AHSGId,StrBarName=@StrBarName,nCityCode=@nCityCode,nTonaj=@nTonaj,nBarCode=@nBarCode,nTruckType=@nTruckType,nCarNum=@nCarNum,StrAddress=@StrAddress,nCarNumKol=@nCarNumKol,StrPriceSug=@StrPriceSug,StrDescription=@StrDescription,TPTParams=@TPTParams,LoadingPlaceId=@LoadingPlaceId,DischargingPlaceId=@DischargingPlaceId Where nEstelamId=" & YourNSS.nEstelamId & ""
            '        Else
            '            If YourUserNSS.UserId = NSSCurrentLoadCapacitorLoad.nUserId Then
            '                CmdSql.CommandText = "Update dbtransport.dbo.TbElam Set nCompCode=@nCompCode,nBarSource=@nBarSource,AHId=@AHId,AHSGId=@AHSGId,StrBarName=@StrBarName,nCityCode=@nCityCode,nTonaj=@nTonaj,nBarCode=@nBarCode,nTruckType=@nTruckType,nCarNum=@nCarNum,StrAddress=@StrAddress,nCarNumKol=@nCarNumKol,StrPriceSug=@StrPriceSug,StrDescription=@StrDescription,TPTParams=@TPTParams,LoadingPlaceId=@LoadingPlaceId,DischargingPlaceId=@DischargingPlaceId Where nEstelamId=" & YourNSS.nEstelamId & ""
            '            Else
            '                CmdSql.CommandText = "Update dbtransport.dbo.TbElam Set AHId=@AHId,AHSGId=@AHSGId,nCityCode=@nCityCode,nTruckType=@nTruckType,StrPriceSug=@StrPriceSug,StrAddress=@StrAddress,LoadingPlaceId=@LoadingPlaceId,DischargingPlaceId=@DischargingPlaceId Where nEstelamId=" & YourNSS.nEstelamId & ""
            '            End If
            '        End If
            '        CmdSql.ExecuteNonQuery()
            '        InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Editing, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        If NSSCurrentLoadCapacitorLoad.nCarNumKol > YourNSS.nCarNumKol Then
            '            InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Decrementing, NSSCurrentLoadCapacitorLoad.nCarNumKol - YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        ElseIf NSSCurrentLoadCapacitorLoad.nCarNumKol < YourNSS.nCarNumKol Then
            '            InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Incrementing, YourNSS.nCarNumKol - NSSCurrentLoadCapacitorLoad.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        End If
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

            '        'ثبت باراعتباری
            '        Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
            '        If InstanceTurnCancellation.IsLoadforTurnCancellation(InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YourNSS.nEstelamId, True)) Then
            '            InstanceTurnCancellation.LoadforTurnCancellationActiving(YourNSS.nEstelamId)
            '        Else
            '            InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourNSS.nEstelamId)
            '        End If


            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadEditTimePassedException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadEditingChangeAHIdNotAllowedException _
            '                        OrElse TypeOf ex Is LoaderTypeRelationAnnouncementHallNotFoundException _
            '                        OrElse TypeOf ex Is LoaderTypeRelationAnnouncementsubGroupNotFoundException _
            '                        OrElse TypeOf ex Is TransportCompanyISNotActiveException _
            '                        OrElse TypeOf ex Is HasNotRelationBetweenProvinceAndAnnouncementsubGroup _
            '                        OrElse TypeOf ex Is SqlInjectionException _
            '                        OrElse TypeOf ex Is TransportPriceTariffParameterDetailNotFoundException _
            '                        OrElse TypeOf ex Is EditOrDeleteReRegisteredLoadNotAllowedException _
            '                        OrElse TypeOf ex Is DataEntryException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadTonajExceededException _
            '                        OrElse TypeOf ex Is LoadingPlaceIsUnActiveException _
            '                        OrElse TypeOf ex Is DischargingPlaceIsUnActiveException _
            '                        OrElse TypeOf ex Is LoadingAndDischargingPlaceNotFoundException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadTonajExceededException _
            '                        OrElse TypeOf ex Is LoadTargetorLoadSourceIsUnActiveException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            Public Function ISActiveLoadCapacitorLoadRegistering(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Try
                    Dim ComposeSearchString As String = YourNSSLoadCapacitorLoad.AHSGId.ToString + "="
                    Dim ALLAHSGsLoadCapacitorLoadManipulationSetting As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, YourNSSLoadCapacitorLoad.AHId), ";")
                    Dim AHSGLoadCapacitorLoadRegisteringActiveStatus As Boolean = Split(Mid(ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")(0)
                    Return AHSGLoadCapacitorLoadRegisteringActiveStatus
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsActiveLoadCapacitorLoadRegisteringInHoliDay(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
                Try
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim ComposeSearchString As String = YourNSSLoadCapacitorLoad.AHSGId.ToString + "="
                    Dim ALLAHSGsLoadCapacitorLoadManipulationSetting As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, YourNSSLoadCapacitorLoad.AHId), ";")
                    Dim AHSGLoadCapacitorLoadRegisteringActiveStatus As Boolean = Split(Mid(ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")(2)
                    Return AHSGLoadCapacitorLoadRegisteringActiveStatus
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub LoadCapacitorLoadCancelling(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    Dim InstancePermisions = New R2Core.PermissionManagement.R2CoreInstansePermissionsManager
                    Dim NSSAnnouncementHall = InstanceAnnouncements.GetNSSAnnouncementHall(YourNSS.AHId)
                    Dim NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
                    'کنترل بار فردا - بار فردا قابل کنسل شدن نیست
                    If YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New UnabletoCancellingorFreeliningTommorowLoadException
                    'کنترل زمان کنسلی بار
                    If Not InstanceAnnouncements.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime With {.Time = YourNSS.dTimeElam}) Then Throw New LoadCapacitorLoadCancelTimeNotReachedException
                    'کنترل مجوز دسترسی کاربر
                    If Not InstancePermisions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanCancellingLoadsViaLoadStatus, YourUserNSS.UserId, YourNSS.LoadStatus) Then Throw New SoftwareUserCanNotCancellingLoadCapacitorLoadException
                    'کنسلی بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    'کنترل وضعیت بار
                    If YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

                    CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & " Where nEstelamId=" & YourNSS.nEstelamId & ""
                    CmdSql.ExecuteNonQuery()
                    'ثبت اکانت
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Cancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'ثبت باراعتباری
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
                    InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourNSS.nEstelamId)


                Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadCancelTimeNotReachedException OrElse TypeOf ex Is SoftwareUserCanNotCancellingLoadCapacitorLoadException
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

            Public Sub LoadCapacitorLoadDeleting(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager

                    'کنترل مجوز حذف بار سیستمی
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanDeleteAnyofLoadCapacitorLoad, YourUserNSS.UserId, 0) Then
                    Else
                        Dim NSSCurrentLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YourNSS.nEstelamId, True)
                        'If NSSCurrentLoadCapacitorLoad.nUserId <> YourUserNSS.UserId Then Throw New SoftwareUserCanNotDeleteLoadCapacitorLoadException
                    End If

                    Dim NSSAnnouncementHall = InstanceAnnouncements.GetNSSAnnouncementHall(YourNSS.AHId)
                    Dim NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)

                    'کنترل اتمام زمان حذف بار
                    'If InstanceAnnouncements.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, YourNSS.dTimeElam)) Then Throw New LoadCapacitorLoadDeleteTimePassedException

                    'باری که مجددا اعلام بار شده است قابل حذف نیست
                    If YourNSS.ReRegistered Then Throw New EditOrDeleteReRegisteredLoadNotAllowedException

                    'بررسی بار فردا
                    Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
                    Dim AllTommorowConfigforAHId As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
                    Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
                    Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
                    Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
                    Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
                    If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) And YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then
                    Else
                        Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                        If InstanceAnnouncementTiming.IsTimingActive(YourNSS.AHId, YourNSS.AHSGId) Then
                            Dim Timing = InstanceAnnouncementTiming.GetTiming(YourNSS.AHId, YourNSS.AHSGId, _DateTime.GetCurrentTime)
                            If InstanceAnnouncements.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime With {.Time = YourNSS.dTimeElam}) Then
                                Throw New LoadCapacitorLoadDeleteTimePassedException
                            End If
                        End If
                    End If

                    'حذف بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    'کنترل وضعیت بار
                    If Not (YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow) Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

                    CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " Where nEstelamId=" & YourNSS.nEstelamId & ""
                    CmdSql.ExecuteNonQuery()
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Deleting, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'ثبت باراعتباری
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
                    InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourNSS.nEstelamId)

                Catch ex As EditOrDeleteReRegisteredLoadNotAllowedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SoftwareUserCanNotDeleteLoadCapacitorLoadException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadDeleteTimePassedException
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

            Public Sub LoadCapacitorLoadFreeLining(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager

                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                    Dim NSSAnnouncementHall = InstanceAnnouncements.GetNSSAnnouncementHall(NSSLoadCapacitorLoad.AHId)
                    Dim NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroupByLoaderTypeId(NSSLoadCapacitorLoad.nTruckType)
                    'کنترل بار فردا - بار فردا قابل آزاد شدن نیست
                    'If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New UnabletoCancellingorFreeliningTommorowLoadException

                    'خط آزاد نمودن بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    'کنترل وضعیت بار
                    If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    'کنترل زمان خط آزاد نمودن بار
                    'If InstanceAnnouncements.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, NSSLoadCapacitorLoad.dTimeElam)) Then Throw New LoadCapacitorLoadFreeLiningTimePassedException

                    CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & " Where nEstelamId=" & NSSLoadCapacitorLoad.nEstelamId & ""
                    CmdSql.ExecuteNonQuery()
                    'ثبت اکانت
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.FreeLining, NSSLoadCapacitorLoad.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
                                   OrElse TypeOf ex Is LoadCapacitorLoadFreeLiningTimePassedException
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

            Private Sub SendSMS(YourTCTitle As String, YourMessage As String)
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim TargetUsers = InstanceSoftwareUsers.GetNSSUser(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 18))
                    Dim LstUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                    LstUsers.Add(TargetUsers)
                    Dim SMSData = New SMSCreationData()
                    SMSData.Data1 = YourTCTitle
                    SMSData.Data2 = YourMessage
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstUsers, R2CoreTransportationAndLoadNotification.SMS.SMSTypes.R2CoreTransportationAndLoadNotificationSMSTypes.ChangeLoadInformation, InstanceSMSHandling.RepeatSMSCreationData(SMSData, LstUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    'If (Not (SMSResultAnalyze <> String.Empty)) Then Throw New Exception
                Catch ex As Exception
                    'Nothing to do
                End Try
            End Sub

            Public Sub ChangeLoadSource(YourMobileNumber As String, YourMessageContent As String)
                '997;882244;21310000
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim nEstelamId As Int64 = YourMessageContent.Split(";")(1)
                    Dim loadSourceId As Int64 = YourMessageContent.Split(";")(2)
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceLoadSources = New R2CoreTransportationAndLoadNotificationMclassLoadSourcesManager
                    'بررسی صحت شهر
                    InstanceLoadSources.GetNSSLoadSource(loadSourceId)
                    'بررسی کاربر درخواست کننده باید ادمین سیستم باشد
                    If YourMobileNumber <> InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 1) Then Throw New UserNotAlllowedException
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim NSSLoadLast = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(nEstelamId, True)
                    CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set nBarSource=" & loadSourceId & " Where nEstelamID=" & nEstelamId & ""
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    Dim NSSLoadCurrent = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(nEstelamId, True)

                    'ثبت باراعتباری
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
                    If InstanceTurnCancellation.IsLoadforTurnCancellation(NSSLoadCurrent.nCityCode, NSSLoadCurrent.nTonaj) Then
                        InstanceTurnCancellation.LoadforTurnCancellationActiving(NSSLoadCurrent.nEstelamId)
                    Else
                        InstanceTurnCancellation.LoadforTurnCancellationUnActiving(NSSLoadCurrent.nEstelamId)
                    End If

                    'ارسال اس ام اس موفق
                    SendSMS(NSSLoadLast.TransportCompanyTitle, "بار " + NSSLoadLast.GoodTitle + " تغییر مبدا از " + NSSLoadLast.LoadSourceTitle + " به " + NSSLoadCurrent.LoadSourceTitle + " و مقصد " + NSSLoadCurrent.LoadTargetTitle)
                Catch ex As LoadSourceIdNotFoundException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As LoadCapacitorLoadNotFoundException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As UserNotExistByMobileNumberException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As UserNotAlllowedException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As Exception
                    SendSMS("بروز خطا", "")
                End Try
            End Sub

            Public Sub ChangeLoadTarget(YourMobileNumber As String, YourMessageContent As String)
                '998;882244;21310000
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim nEstelamId As Int64 = YourMessageContent.Split(";")(1)
                    Dim loadTargetId As Int64 = YourMessageContent.Split(";")(2)
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceLoadTarget = New R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
                    'بررسی صحت شهر
                    InstanceLoadTarget.GetNSSLoadTarget(loadTargetId)
                    'بررسی کاربر درخواست کننده باید ادمین سیستم باشد
                    If YourMobileNumber <> InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 1) Then Throw New UserNotAlllowedException
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim NSSLoadLast = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(nEstelamId, True)
                    CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set nCityCode=" & loadTargetId & " Where nEstelamID=" & nEstelamId & ""
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    Dim NSSLoadCurrent = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(nEstelamId, True)

                    'ثبت باراعتباری
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTime)
                    If InstanceTurnCancellation.IsLoadforTurnCancellation(NSSLoadCurrent.nCityCode, NSSLoadCurrent.nTonaj) Then
                        InstanceTurnCancellation.LoadforTurnCancellationActiving(NSSLoadCurrent.nEstelamId)
                    Else
                        InstanceTurnCancellation.LoadforTurnCancellationUnActiving(NSSLoadCurrent.nEstelamId)
                    End If

                    'ارسال اس ام اس موفق
                    SendSMS(NSSLoadLast.TransportCompanyTitle, "بار " + NSSLoadLast.GoodTitle + " تغییر مقصد از " + NSSLoadLast.LoadTargetTitle + " به " + NSSLoadCurrent.LoadTargetTitle + " و مبدا " + NSSLoadCurrent.LoadSourceTitle)
                Catch ex As LoadTargetIdNotFoundException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As LoadCapacitorLoadNotFoundException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As UserNotExistByMobileNumberException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As UserNotAlllowedException
                    SendSMS("بروز خطا", ex.Message)
                Catch ex As Exception
                    SendSMS("بروز خطا", "")
                End Try
            End Sub

        End Class

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManipulationManagement
            Private Shared _DateTime As New R2DateTime


            'Public Shared Function ISActiveLoadCapacitorLoadRegistering(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Boolean
            '    Try
            '        Dim ComposeSearchString As String = YourNSSLoadCapacitorLoad.AHSGId.ToString + "="
            '        Dim ALLAHSGsLoadCapacitorLoadManipulationSetting As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, YourNSSLoadCapacitorLoad.AHId), ";")
            '        Dim AHSGLoadCapacitorLoadRegisteringActiveStatus As Boolean = Split(Mid(ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, ALLAHSGsLoadCapacitorLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")(0)
            '        Return AHSGLoadCapacitorLoadRegisteringActiveStatus
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            'Public shared Function LoadCapacitorLoadRegistering(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetAnnouncementHallfromLoadCapacitorLoad(YourNSS)
            '        Dim NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        YourNSS.AHId = NSSAnnouncementHall.AHId
            '        YourNSS.AHSGId = NSSAnnouncementsubGroup.AHSGId
            '        'بررسی اینکه آیا برای زیرگروه مربوطه امکان ثبت بار وجود دارد یا نه
            '        If Not ISActiveLoadCapacitorLoadRegistering(YourNSS) Then Throw New LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
            '        'کنترل تعداد بار
            '        If YourNSS.nCarNumKol > R2CoreMClassConfigurationManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, 0) Then Throw New LoadCapacitorLoadNumberOverLimitException
            '        If YourNSS.nCarNumKol < 1 Then Throw New LoadCapacitorLoadnCarNumKolCanNotBeZeroException
            '        'کنترل اکتیو بودن شرکت حمل و نقل
            '        If R2CoreTransportationAndLoadNotificationMClassTransportCompaniesManagement.ISTransportCompanyActive(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(YourNSS.nCompCode, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)) = False Then Throw New TransportCompanyISNotActiveException
            '        'بررسی بار فردا
            '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
            '        Dim AllTommorowConfigforAHId As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
            '        Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
            '        Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
            '        Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
            '        Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
            '        Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
            '        Dim TommorowLoadRegisteringFlag As Boolean = False
            '        If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) Then
            '            TommorowLoadRegisteringFlag = True
            '        Else
            '            'کنترل اتمام زمان ثبت بار
            '            If _DateTime.GetCurrentTime() > R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetAnnouncementHallLeastAnnounceTime(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId).Time Then
            '                Throw New LoadCapacitorLoadRegisterTimePassedException
            '            End If
            '        End If
            '        'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
            '        Dim Tariff As Int64 = YourNSS.StrPriceSug
            '        Try
            '            Dim TariffTemp = R2CoreTransportationAndLoadNotificationMClassTransportTariffsManagement.GetNSSTransportTariff(YourNSS).Tariff
            '            If Tariff < TariffTemp Then Tariff = TariffTemp
            '        Catch exx As TransportPriceTariffNotFoundException
            '        End Try

            '        'ثبت بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        CmdSql.CommandText = "Select Top 1 nEstelamId From dbtransport.dbo.TbElam with (tablockx) Order By nEstelamId Desc"
            '        CmdSql.ExecuteNonQuery()
            '        CmdSql.CommandText = "Select IDENT_CURRENT('dbtransport.dbo.TbElam') "
            '        Dim nEstelamIdNew As Int64 = CmdSql.ExecuteScalar() + 1
            '        Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.MD5Hasher
            '        If TommorowLoadRegisteringFlag Then
            '            CmdSql.CommandText = "Insert Into dbtransport.dbo.TbElam(nEstelamKey,StrBarName,nCityCode,nBarCode,bFlag,nCompCode,nTruckType,nCarNum,StrAddress,nUserId,dDateElam,dTimeElam,nCarNumKol,StrPriceSug,StrDescription,LoadStatus,nBarSource,AHId,AHSGId) Values('" & Hasher.GenerateMD5String(nEstelamIdNew) & "','" & YourNSS.StrBarName & "'," & YourNSS.nCityCode & "," & YourNSS.nBarCode & ",0," & YourNSS.nCompCode & "," & YourNSS.nTruckType & "," & YourNSS.nCarNumKol & ",'" & YourNSS.StrAddress & "'," & YourNSS.nUserId & ",'" & _DateTime.GetCurrentShamsiDate() & "','" & _DateTime.GetCurrentTime() & "'," & YourNSS.nCarNumKol & "," & Tariff & ",'" & YourNSS.StrDescription & "'," & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow & ",21310000," & NSSAnnouncementHall.AHId & "," & NSSAnnouncementsubGroup.AHSGId & ")"
            '        Else
            '            CmdSql.CommandText = "Insert Into dbtransport.dbo.TbElam(nEstelamKey,StrBarName,nCityCode,nBarCode,bFlag,nCompCode,nTruckType,nCarNum,StrAddress,nUserId,dDateElam,dTimeElam,nCarNumKol,StrPriceSug,StrDescription,LoadStatus,nBarSource,AHId,AHSGId) Values('" & Hasher.GenerateMD5String(nEstelamIdNew) & "','" & YourNSS.StrBarName & "'," & YourNSS.nCityCode & "," & YourNSS.nBarCode & ",0," & YourNSS.nCompCode & "," & YourNSS.nTruckType & "," & YourNSS.nCarNumKol & ",'" & YourNSS.StrAddress & "'," & YourNSS.nUserId & ",'" & _DateTime.GetCurrentShamsiDate() & "','" & _DateTime.GetCurrentTime() & "'," & YourNSS.nCarNumKol & "," & Tariff & ",'" & YourNSS.StrDescription & "'," & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & ",21310000," & NSSAnnouncementHall.AHId & "," & NSSAnnouncementsubGroup.AHSGId & ")"
            '        End If
            '        CmdSql.ExecuteNonQuery()
            '        'ثبت اکانت
            '        If TommorowLoadRegisteringFlag Then
            '            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(nEstelamIdNew, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.RegisteringforTommorow, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourNSS.nUserId))
            '        Else
            '            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(nEstelamIdNew, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Registering, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourNSS.nUserId))
            '        End If
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            '        'ارسال کد مخزن بار
            '        Return nEstelamIdNew
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadNumberOverLimitException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is TransportCompanyISNotActiveException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisterTimePassedException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            'Public Shared Sub LoadCapacitorLoadEditing(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = Announcements.R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetAnnouncementHallfromLoadCapacitorLoad(YourNSS)
            '        Dim NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        YourNSS.AHId = NSSAnnouncementHall.AHId
            '        YourNSS.AHSGId = NSSAnnouncementsubGroup.AHSGId
            '        Dim CurrentNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(YourNSS.nEstelamId)
            '        'بررسی بار فردا
            '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
            '        Dim AllTommorowConfigforAHId As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
            '        Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
            '        Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
            '        Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
            '        Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
            '        Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
            '        If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) And YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then
            '        Else
            '            'کنترل اتمام زمان ویرایش بار
            '            If R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, YourNSS.dTimeElam)) Then Throw New LoadCapacitorLoadEditTimePassedException
            '        End If
            '        'کنترل تعداد بار
            '        If YourNSS.nCarNumKol > R2CoreMClassConfigurationManagement.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, 0) Then Throw New LoadCapacitorLoadNumberOverLimitException
            '        If YourNSS.nCarNumKol = 0 Then Throw New LoadCapacitorLoadnCarNumKolCanNotBeZeroException
            '        'کنترل اکتیو بودن شرکت حمل و نقل
            '        If R2CoreTransportationAndLoadNotificationMClassTransportCompaniesManagement.ISTransportCompanyActive(New R2CoreTransportationAndLoadNotificationStandardTransportCompanyStructure(YourNSS.nCompCode, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)) = False Then Throw New TransportCompanyISNotActiveException

            '        'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
            '        Dim Tariff As String = YourNSS.StrPriceSug
            '        Try
            '            Dim TariffTemp = R2CoreTransportationAndLoadNotificationMClassTransportTariffsManagement.GetNSSTransportTariff(YourNSS).Tariff
            '            If Tariff < TariffTemp Then Tariff = TariffTemp
            '        Catch exx As TransportPriceTariffNotFoundException
            '        End Try

            '        'ویرایش بار
            '        Dim CurrentnCarNumKol As Int64 = CurrentNSSLoadCapacitorLoad.nCarNumKol
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        'کنترل وضعیت بار
            '        If CurrentNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Or CurrentNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or CurrentNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

            '        CmdSql.CommandText = "Update dbtransport.dbo.TbElam Set nCompCode=" & YourNSS.nCompCode & ",nBarSource=" & 21310000 & ",AHId=" & NSSAnnouncementHall.AHId & ",AHSGId=" & NSSAnnouncementsubGroup.AHSGId & ",StrBarName='" & YourNSS.StrBarName & "',nCityCode=" & YourNSS.nCityCode & ",nBarCode=" & YourNSS.nBarCode & ",nTruckType=" & YourNSS.nTruckType & ",nCarNum=" & YourNSS.nCarNumKol & ",StrAddress='" & YourNSS.StrAddress & "',nCarNumKol=" & YourNSS.nCarNumKol & ",StrPriceSug='" & Tariff & "',StrDescription='" & YourNSS.StrDescription & "' Where nEstelamId=" & YourNSS.nEstelamId & ""
            '        CmdSql.ExecuteNonQuery()
            '        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Editing, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        If CurrentnCarNumKol > YourNSS.nCarNumKol Then
            '            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Decrementing, CurrentnCarNumKol - YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        ElseIf CurrentnCarNumKol < YourNSS.nCarNumKol Then
            '            R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Incrementing, YourNSS.nCarNumKol - CurrentnCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        End If
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadEditTimePassedException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadNumberOverLimitException _
            '                        OrElse TypeOf ex Is LoadCapacitorLoadnCarNumKolCanNotBeZeroException _
            '                        OrElse TypeOf ex Is TransportCompanyISNotActiveException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            'Public Shared Sub LoadCapacitorLoadDeleting(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementHall(YourNSS.AHId)
            '        Dim NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        'بررسی بار فردا
            '        Dim ComposeSearchString As String = NSSAnnouncementsubGroup.AHSGId.ToString + "="
            '        Dim AllTommorowConfigforAHId As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, NSSAnnouncementHall.AHId), "-")
            '        Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
            '        Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
            '        Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
            '        Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
            '        Dim CurrentTimeforRegistering As String = _DateTime.GetCurrentTime()
            '        If IsTommorowLoadRegisteringActive And (CurrentTimeforRegistering >= TommorowLoadRegisteringFirstTime And CurrentTimeforRegistering <= TommorowLoadRegisteringEndTime) And YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then
            '        Else
            '            'کنترل اتمام زمان حذف بار
            '            If R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, YourNSS.dTimeElam)) Then Throw New LoadCapacitorLoadDeleteTimePassedException
            '        End If

            '        'حذف بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        'کنترل وضعیت بار
            '        If Not (YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow) Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

            '        CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted & " Where nEstelamId=" & YourNSS.nEstelamId & ""
            '        CmdSql.ExecuteNonQuery()
            '        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Deleting, YourNSS.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadDeleteTimePassedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            'Public Shared Sub LoadCapacitorLoadCancelling(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementHall(YourNSS.AHId)
            '        Dim NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSS.nTruckType)
            '        'کنترل بار فردا - بار فردا قابل کنسل شدن نیست
            '        If YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New UnabletoCancellingorFreeliningTommorowLoadException
            '        'کنترل زمان کنسلی بار
            '        If Not R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, YourNSS.dTimeElam)) Then Throw New LoadCapacitorLoadCancelTimeNotReachedException
            '        'حذف بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        'کنترل وضعیت بار
            '        If YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or YourNSS.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

            '        CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled & " Where nEstelamId=" & YourNSS.nEstelamId & ""
            '        CmdSql.ExecuteNonQuery()
            '        'ثبت اکانت
            '        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSS.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Cancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadCancelTimeNotReachedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

            'Public Shared Sub LoadCapacitorLoadFreeLining(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            '    Dim CmdSql As New SqlCommand
            '    CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            '    Try
            '        Dim NSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetNSSLoadCapacitorLoad(YournEstelamId)
            '        Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementHall(NSSLoadCapacitorLoad.AHId)
            '        Dim NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroupByLoaderTypeId(NSSLoadCapacitorLoad.nTruckType)
            '        'کنترل بار فردا - بار فردا قابل آزاد شدن نیست
            '        If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New UnabletoCancellingorFreeliningTommorowLoadException

            '        'خط آزاد نمودن بار
            '        CmdSql.Connection.Open()
            '        CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
            '        'کنترل وضعیت بار
            '        If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
            '        'کنترل زمان خط آزاد نمودن بار
            '        If R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime(Nothing, Nothing, NSSLoadCapacitorLoad.dTimeElam)) Then Throw New LoadCapacitorLoadFreeLiningTimePassedException

            '        CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & " Where nEstelamId=" & NSSLoadCapacitorLoad.nEstelamId & ""
            '        CmdSql.ExecuteNonQuery()
            '        'ثبت اکانت
            '        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.FreeLining, NSSLoadCapacitorLoad.nCarNumKol, Nothing, Nothing, Nothing, YourUserNSS.UserId))
            '        CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            '    Catch ex As Exception When TypeOf ex Is LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException _
            '                       OrElse TypeOf ex Is LoadCapacitorLoadFreeLiningTimePassedException
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw ex
            '    Catch ex As Exception
            '        If CmdSql.Connection.State <> ConnectionState.Closed Then
            '            CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
            '        End If
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Sub

        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationLoadManipulationManager

            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
            End Sub

            Public Function IsActiveLoadRegisteringForAnnouncementSubGroup(YourLoad As R2CoreTransportationAndLoadNotificationLoad) As Boolean
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                Try
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim ALLAnnouncementSGsLoadManipulationSetting As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, YourLoad.AnnouncementGroupId), ";")
                    Dim AnnouncementSGLoadRegisteringActiveStatus As Boolean = Split(Mid(ALLAnnouncementSGsLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, ALLAnnouncementSGsLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")(0)
                    Return AnnouncementSGLoadRegisteringActiveStatus
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsActiveLoadRegisterinInHoliday(YourLoad As R2CoreTransportationAndLoadNotificationLoad) As Boolean
                Try
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim AllAnnouncementSGsLoadManipulationSetting As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadCapacitorLoadManipulationSetting, YourLoad.AnnouncementGroupId), ";")
                    Dim AnnouncementSGLoadRegisteringActiveStatus As Boolean = Split(Mid(AllAnnouncementSGsLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnouncementSGsLoadManipulationSetting.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")(2)
                    Return AnnouncementSGLoadRegisteringActiveStatus
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function LoadRegistering(YourLoad As R2CoreTransportationAndLoadNotificationLoad, YourSoftwareUser As R2CoreSoftwareUser) As Int64
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoadingAndDischargingPlaces = New R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager
                    Dim InstanceLoadTargets = New R2CoreTransportationAndLoadNotificationLoadTargetsManager(_DateTimeService)
                    Dim InstanceLoadSources = New R2CoreTransportationAndLoadNotificationLoadSourcesManager(_DateTimeService)
                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstanceProvincesAndCities = New R2CoreParkingSystemProvincesAndCitiesManager(_DateTimeService)
                    Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService)
                    Dim InstancePermissions = New R2Core.PermissionManagement.R2CoreInstansePermissionsManager
                    Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                    Dim InstanceSpecializedPersianCalendar = New R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager(_DateTimeService)
                    Dim InstanceTransportTariffs = New R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService)
                    Dim InstanceLoaderTypes = New R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService)
                    Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService)
                    Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    'کنترل فعال بودن مبدا بارگیری و محل تخلیه
                    If InstanceLoadingAndDischargingPlaces.IsActiveLoadingPlace(YourLoad.LoadingPlaceId) = False Then Throw New LoadingPlaceIsUnActiveException
                    If InstanceLoadingAndDischargingPlaces.IsActiveDischargingPlace(YourLoad.DischargingPlaceId) = False Then Throw New DischargingPlaceIsUnActiveException

                    'کنترل فعال بودن شهر مبدا
                    If Not InstanceLoadSources.IsActiveLoadSource(YourLoad.SourceCityId) Then
                        Throw New LoadSourceIsUnActiveException
                    End If

                    'کنترل فعال بودن شهر مقصد
                    If Not InstanceLoadTargets.IsActiveLoadTarget(YourLoad.TargetCityId) Then
                        Throw New LoadTargetIsUnActiveException
                    End If

                    'کنترل تناژ بار 
                    'کاربر ادمین مجوز تناژ بدون محدودیت دارد
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRegisterOrEditLoadsAnyTonaj, YourSoftwareUser.UserId, 0) Then
                    Else
                        'R2CoreLoggingManager.WriteInfLog(New R2CoreLog With {.LogTypeId = 0, .Description = YourLoad.Tonaj.ToString(), .MessageDetail1 = YourLoad.AnnouncementGroupId.ToString(), .MessageDetail2 = YourLoad.AnnouncementSubGroupId.ToString(), .MessageDetail3 = "", .UserId = 21}, 21)
                        InstanceLoad.LoadTonajValidate(YourLoad)
                    End If

                    'کنترل تعداد بار 
                    InstanceLoad.LoadTotalValidate(YourLoad)

                    'بررسی تطابق استان انتخاب شده با زیرگروه اعلام بار
                    InstanceAnnouncements.RelationBetweenProvinceAndAnnouncementsubGroupValidate(InstanceProvincesAndCities.GetProvinceIdOfCity(YourLoad.TargetCityId), YourLoad.AnnouncementSubGroupId)

                    'بررسی اینکه آیا برای زیرگروه مربوطه امکان ثبت بار وجود دارد یا نه
                    If Not IsActiveLoadRegisteringForAnnouncementSubGroup(YourLoad) Then Throw New LoadRegisteringNotAllowedforThisAnnouncementsubGroupException

                    'کنترل اکتیو بودن شرکت حمل و نقل
                    If InstanceTransportCompanies.ISTransportCompanyActive(YourLoad.TransportCompanyId) = False Then Throw New TransportCompanyISNotActiveException

                    'بررسی بار فردا
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim AllTommorowConfigforAnnouncementId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, YourLoad.AnnouncementGroupId), "-")
                    Dim AllTommorowConfigforAnnouncementSGId = Split(Mid(AllTommorowConfigforAnnouncementId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAnnouncementId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAnnouncementSGId(0)
                    Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAnnouncementSGId(1)
                    Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAnnouncementSGId(2)
                    Dim currentTime As String = _DateTimeService.GetCurrentTime()
                    Dim TommorowLoadRegisteringFlag As Boolean = False
                    If currentTime >= TommorowLoadRegisteringFirstTime And currentTime <= TommorowLoadRegisteringEndTime Then
                        If IsTommorowLoadRegisteringActive Then
                            TommorowLoadRegisteringFlag = True
                        Else
                            Throw New TommarowLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                        End If
                    End If
                    If TommorowLoadRegisteringFlag Then
                        'اعلام بار فردا برای زیر گروه مورد نظر امکان پذیر است ولی آیا در تاریخ فردا اعلام بار برای زیر گروه مورد نظر تعطیل نیست
                        If InstanceSpecializedPersianCalendar.IsActiveTomorrowLoadAnnounce Then Throw New LoadRegisteringforShamsiDateNotAllowedException(_DateTimeService.GetNextShamsiDateWithoutSlashes)
                    Else
                        'اعلام بار برای امروز و برای زیرگروه مورد نظر فعال است ولی آیا در تاریخ امروز  و برای زیرگروه مورد نظراعلام بار تعطیل نیست
                        If InstanceSpecializedPersianCalendar.IsActiveTodayLoadAnnounce Then Throw New LoadRegisteringforShamsiDateNotAllowedException(_DateTimeService.GetCurrentShamsiDate)

                        Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationAnnouncementTimingManager(_DateTimeService)
                        Dim Timing = InstanceAnnouncementTiming.GetTiming(YourLoad.AnnouncementGroupId, YourLoad.AnnouncementSubGroupId, _DateTimeService.GetCurrentTime)
                        If _DateTimeService.GetCurrentTime() > InstanceAnnouncements.GetAnnouncementLeastAnnounceTime(YourLoad.AnnouncementGroupId, YourLoad.AnnouncementSubGroupId).Time Then
                            Throw New LoadRegisterTimePassedException
                        End If
                        If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering Or
                           Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                            Throw New LoadRegisterTimePassedException
                        End If
                    End If

                    'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
                    Dim Tariff As Int64 = 0
                    Try
                        Dim loaderTypeId = InstanceLoaderTypes.GetLoaderTypeIdfromAnnouncementSGId(YourLoad.AnnouncementSubGroupId)
                        Tariff = InstanceTransportTariffs.GetTransportTariff(YourLoad.SourceCityId, YourLoad.TargetCityId, loaderTypeId, YourLoad.GoodId)
                        Tariff = InstanceTransportTariffs.GetUltimateTransportTariff(YourLoad.AnnouncementSubGroupId, YourLoad.Tonaj, Tariff)
                        Tariff = Tariff + InstanceTransportTariffsParameters.GetTotalofTransportTariffsParameters(YourLoad)
                    Catch exx As TransportPriceTariffNotFoundException
                    End Try

                    'ثبت بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Select Top 1 nEstelamId From dbtransport.dbo.TbElam with (tablockx) Order By nEstelamId Desc"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Select IDENT_CURRENT('dbtransport.dbo.TbElam') "
                    Dim nEstelamIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@nEstelamId", SqlDbType.BigInt) : P.Value = nEstelamIdNew
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nEstelamKey", SqlDbType.NVarChar) : P.Value = Hasher.GenerateSHA256String(nEstelamIdNew)
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrBarName", SqlDbType.NVarChar) : P.Value = YourLoad.Recipient
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nCityCode", SqlDbType.BigInt) : P.Value = YourLoad.TargetCityId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nTonaj", SqlDbType.Float) : P.Value = YourLoad.Tonaj
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nBarCode", SqlDbType.BigInt) : P.Value = YourLoad.GoodId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nCompCode", SqlDbType.BigInt) : P.Value = YourLoad.TransportCompanyId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrAddress", SqlDbType.NVarChar) : P.Value = YourLoad.Address
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nCarNumKol", SqlDbType.Int) : P.Value = YourLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrPriceSug", SqlDbType.VarChar) : P.Value = Tariff
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrDescription", SqlDbType.NVarChar) : P.Value = YourLoad.Description
                    CmdSql.Parameters.Add(P)
                    If TommorowLoadRegisteringFlag Then
                        P = New SqlClient.SqlParameter("@LoadStatus", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow
                    Else
                        P = New SqlClient.SqlParameter("@LoadStatus", SqlDbType.TinyInt) : P.Value = YourLoad.LoadStatusId
                    End If
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nBarSource", SqlDbType.BigInt) : P.Value = YourLoad.SourceCityId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@AHId", SqlDbType.BigInt) : P.Value = YourLoad.AnnouncementGroupId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@AHSGId", SqlDbType.BigInt) : P.Value = YourLoad.AnnouncementSubGroupId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@TPTParams", SqlDbType.VarChar) : P.Value = YourLoad.TPTParams
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@LoadingPlaceId", SqlDbType.BigInt) : P.Value = YourLoad.LoadingPlaceId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@DischargingPlaceId", SqlDbType.BigInt) : P.Value = YourLoad.DischargingPlaceId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@TPTParamsJoint", SqlDbType.NVarChar) : P.Value = InstanceTransportTariffsParameters.GetTransportTariffsComposit(YourLoad.TPTParams)
                    CmdSql.Parameters.Add(P)
                    If TommorowLoadRegisteringFlag Then
                        P = New SqlClient.SqlParameter("@AccountType", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.RegisteringforTommorow
                        CmdSql.Parameters.Add(P)
                    Else
                        P = New SqlClient.SqlParameter("@AccountType", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Registering
                        CmdSql.Parameters.Add(P)
                    End If
                    P = New SqlClient.SqlParameter("@Amount", SqlDbType.NVarChar) : P.Value = YourLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadRegistering"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'ثبت بار اعتباری
                    If InstanceTurnCancellation.IsLoadforTurnCancellation(YourLoad.TargetCityId, YourLoad.Tonaj) Then
                        InstanceTurnCancellation.LoadforTurnCancellationRegistering(nEstelamIdNew, True)
                    Else
                        InstanceTurnCancellation.LoadforTurnCancellationRegistering(nEstelamIdNew, False)
                    End If

                    'ارسال کد مخزن بار
                    Return nEstelamIdNew
                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadingPlaceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As DischargingPlaceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadingAndDischargingPlaceNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadSourceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadTargetIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadTonajInvalidException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As TommarowLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadTotalInvalidException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As HasNotRelationBetweenProvinceAndAnnouncementsubGroup
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadRegisteringforShamsiDateNotAllowedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As TransportCompanyISNotActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadRegisterTimePassedException
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

            Public Sub LoadEditing(YourLoad As R2CoreTransportationAndLoadNotificationLoad, YourSoftwareUser As R2CoreSoftwareUser)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    'SqlInjection
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourLoad.Address)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourLoad.Recipient)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourLoad.Description)

                    Dim InstanceLoadingAndDischargingPlaces = New R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager
                    Dim InstanceLoadTargets = New R2CoreTransportationAndLoadNotificationLoadTargetsManager(_DateTimeService)
                    Dim InstanceLoadSources = New R2CoreTransportationAndLoadNotificationLoadSourcesManager(_DateTimeService)
                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstanceProvincesAndCities = New R2CoreParkingSystemProvincesAndCitiesManager(_DateTimeService)
                    Dim InstanceTransportCompanies = New R2CoreTransportationAndLoadNotificationTransportCompaniesManager(_DateTimeService)
                    Dim InstancePermissions = New R2Core.PermissionManagement.R2CoreInstansePermissionsManager
                    Dim InstanceConfigurationsofAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                    Dim InstanceSpecializedPersianCalendar = New R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager(_DateTimeService)
                    Dim InstanceTransportTariffs = New R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService)
                    Dim InstanceLoaderTypes = New R2CoreTransportationAndLoadNotificationLoaderTypesManager(_DateTimeService)
                    Dim InstanceTransportTariffsParameters = New R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService)
                    Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    Dim LastLoad = InstanceLoad.GetLoadSimpleModel(YourLoad.LoadId, True)

                    'کنترل فعال بودن مبدا بارگیری و محل تخلیه
                    If InstanceLoadingAndDischargingPlaces.IsActiveLoadingPlace(YourLoad.LoadingPlaceId) = False Then Throw New LoadingPlaceIsUnActiveException
                    If InstanceLoadingAndDischargingPlaces.IsActiveDischargingPlace(YourLoad.DischargingPlaceId) = False Then Throw New DischargingPlaceIsUnActiveException

                    'کنترل فعال بودن شهر مبدا
                    If Not InstanceLoadSources.IsActiveLoadSource(YourLoad.SourceCityId) Then
                        Throw New LoadSourceIsUnActiveException
                    End If

                    'کنترل فعال بودن شهر مقصد
                    If Not InstanceLoadTargets.IsActiveLoadTarget(YourLoad.TargetCityId) Then
                        Throw New LoadTargetIsUnActiveException
                    End If

                    'کنترل تناژ بار 
                    'کاربر ادمین مجوز تناژ بدون محدودیت دارد
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRegisterOrEditLoadsAnyTonaj, YourSoftwareUser.UserId, 0) Then
                    Else
                        InstanceLoad.LoadTonajValidate(YourLoad)
                    End If


                    'کنترل تعداد بار 
                    InstanceLoad.LoadTotalValidate(YourLoad)

                    'بررسی تطابق استان انتخاب شده با زیرگروه اعلام بار
                    InstanceAnnouncements.RelationBetweenProvinceAndAnnouncementsubGroupValidate(InstanceProvincesAndCities.GetProvinceIdOfCity(YourLoad.TargetCityId), YourLoad.AnnouncementSubGroupId)

                    'ویرایش گروه اصلی اعلام بار و زیرگروه امکان پذیر نیست در صورتی که کاربر اشتباه کرده باشد باید بار را کامل حذف کند یک بار دیگر ثبت نماید
                    If YourLoad.AnnouncementGroupId <> LastLoad.AnnouncementGroupId Then Throw New ChangeAnnouncementGroupOrAnnouncementSubGroupInEditingNotAllowedException
                    If YourLoad.AnnouncementSubGroupId <> LastLoad.AnnouncementSubGroupId Then Throw New ChangeAnnouncementGroupOrAnnouncementSubGroupInEditingNotAllowedException

                    'بررسی اینکه آیا برای زیرگروه مربوطه امکان ثبت بار وجود دارد یا نه
                    If Not IsActiveLoadRegisteringForAnnouncementSubGroup(YourLoad) Then Throw New LoadRegisteringNotAllowedforThisAnnouncementsubGroupException

                    'کنترل اکتیو بودن شرکت حمل و نقل
                    If InstanceTransportCompanies.ISTransportCompanyActive(YourLoad.TransportCompanyId) = False Then Throw New TransportCompanyISNotActiveException

                    'بررسی بار فردا
                    Dim ComposeSearchString As String = YourLoad.AnnouncementSubGroupId.ToString + "="
                    Dim AllTommorowConfigforAnnouncementId As String() = Split(InstanceConfigurationsofAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, YourLoad.AnnouncementGroupId), "-")
                    Dim AllTommorowConfigforAnnouncementSGId = Split(Mid(AllTommorowConfigforAnnouncementId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAnnouncementId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAnnouncementSGId(0)
                    Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAnnouncementSGId(1)
                    Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAnnouncementSGId(2)
                    Dim currentTime As String = _DateTimeService.GetCurrentTime()
                    Dim TommorowLoadRegisteringFlag As Boolean = False
                    If currentTime >= TommorowLoadRegisteringFirstTime And currentTime <= TommorowLoadRegisteringEndTime Then
                        If IsTommorowLoadRegisteringActive Then
                            TommorowLoadRegisteringFlag = True
                        Else
                            Throw New TommarowLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                        End If
                    End If
                    If TommorowLoadRegisteringFlag Then
                        'اعلام بار فردا برای زیر گروه مورد نظر امکان پذیر است ولی آیا در تاریخ فردا اعلام بار برای زیر گروه مورد نظر تعطیل نیست
                        If InstanceSpecializedPersianCalendar.IsActiveTomorrowLoadAnnounce Then Throw New LoadRegisteringforShamsiDateNotAllowedException(_DateTimeService.GetNextShamsiDateWithoutSlashes)
                    Else
                        'اعلام بار برای امروز و برای زیرگروه مورد نظر فعال است ولی آیا در تاریخ امروز  و برای زیرگروه مورد نظراعلام بار تعطیل نیست
                        If InstanceSpecializedPersianCalendar.IsActiveTodayLoadAnnounce Then Throw New LoadRegisteringforShamsiDateNotAllowedException(_DateTimeService.GetCurrentShamsiDate)

                        Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationAnnouncementTimingManager(_DateTimeService)
                        Dim Timing = InstanceAnnouncementTiming.GetTiming(YourLoad.AnnouncementGroupId, YourLoad.AnnouncementSubGroupId, _DateTimeService.GetCurrentTime)
                        If _DateTimeService.GetCurrentTime() > InstanceAnnouncements.GetAnnouncementLeastAnnounceTime(YourLoad.AnnouncementGroupId, YourLoad.AnnouncementSubGroupId).Time Then
                            Throw New LoadRegisterTimePassedException
                        End If
                        If Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering Or
                           Timing = R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering Then
                            Throw New LoadRegisterTimePassedException
                        End If
                    End If

                    'استاندارد سازی نرخ حمل بر اساس تعرفه های سازمانی
                    Dim Tariff As Int64 = 0
                    Try
                        Dim loaderTypeId = InstanceLoaderTypes.GetLoaderTypeIdfromAnnouncementSGId(YourLoad.AnnouncementSubGroupId)
                        Tariff = InstanceTransportTariffs.GetTransportTariff(YourLoad.SourceCityId, YourLoad.TargetCityId, loaderTypeId, YourLoad.GoodId)
                        Tariff = InstanceTransportTariffs.GetUltimateTransportTariff(YourLoad.AnnouncementSubGroupId, YourLoad.Tonaj, Tariff)
                        Tariff = Tariff + InstanceTransportTariffsParameters.GetTotalofTransportTariffsParameters(YourLoad)
                    Catch exx As TransportPriceTariffNotFoundException
                    End Try

                    'کنترل وضعیت بار
                    If LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then Throw New LoadHandlingNotAllowedBecuaseLoadStatusException

                    'کنترل تاریخ بار
                    If LastLoad.AnnounceDate <> _DateTimeService.GetCurrentShamsiDate Then Throw New LoadRegisterTimePassedException

                    'ثبت بار
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@nEstelamId", SqlDbType.VarChar) : P.Value = YourLoad.LoadId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrBarName", SqlDbType.VarChar) : P.Value = YourLoad.Recipient
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nCityCode", SqlDbType.Int) : P.Value = YourLoad.TargetCityId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nTonaj", SqlDbType.Float) : P.Value = YourLoad.Tonaj
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nBarCode", SqlDbType.Int) : P.Value = YourLoad.GoodId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrAddress", SqlDbType.VarChar) : P.Value = YourLoad.Address
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nCarNumKol", SqlDbType.Int) : P.Value = YourLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrPriceSug", SqlDbType.VarChar) : P.Value = Tariff
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@StrDescription", SqlDbType.VarChar) : P.Value = YourLoad.Description
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@nBarSource", SqlDbType.Int) : P.Value = YourLoad.SourceCityId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@TPTParams", SqlDbType.VarChar) : P.Value = YourLoad.TPTParams
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@LoadingPlaceId", SqlDbType.BigInt) : P.Value = YourLoad.LoadingPlaceId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@DischargingPlaceId", SqlDbType.BigInt) : P.Value = YourLoad.DischargingPlaceId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@TPTParamsJoint", SqlDbType.NVarChar) : P.Value = InstanceTransportTariffsParameters.GetTransportTariffsComposit(YourLoad.TPTParams)
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@AccountType", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Editing
                    CmdSql.Parameters.Add(P)
                    If LastLoad.TotalNumber > YourLoad.TotalNumber Then
                        P = New SqlClient.SqlParameter("@AccountTypeSecond", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Decrementing
                        CmdSql.Parameters.Add(P)
                    ElseIf LastLoad.TotalNumber < YourLoad.TotalNumber Then
                        P = New SqlClient.SqlParameter("@AccountTypeSecond", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Incrementing
                        CmdSql.Parameters.Add(P)
                    Else
                        P = New SqlClient.SqlParameter("@AccountTypeSecond", SqlDbType.NVarChar) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.None
                        CmdSql.Parameters.Add(P)
                    End If
                    CmdSql.Connection.Open()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadEditing"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()

                    'ثبت بار اعتباری
                    If InstanceTurnCancellation.IsLoadforTurnCancellation(YourLoad.TargetCityId, YourLoad.Tonaj) Then
                        InstanceTurnCancellation.LoadforTurnCancellationActiving(YourLoad.LoadId)
                    Else
                        InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourLoad.LoadId)
                    End If

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadingPlaceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As DischargingPlaceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadingAndDischargingPlaceNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadSourceIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadTargetIsUnActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadTonajInvalidException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As TommarowLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadTotalInvalidException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As HasNotRelationBetweenProvinceAndAnnouncementsubGroup
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadRegisteringforShamsiDateNotAllowedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As TransportCompanyISNotActiveException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As LoadRegisterTimePassedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadDeleting(YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstanceAnnouncementTiming = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
                    Dim InstancePermissions = New R2CoreInstansePermissionsManager
                    Dim InstanceLoadAccounting = New R2CoreTransportationAndLoadNotificationLoadAccountingManager(_DateTimeService)
                    Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    Dim LastLoad = InstanceLoad.GetLoadSimpleModel(YourLoadId, True)

                    'کنترل مجوز حذف بار کاربر
                    If InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanDeleteAnyofLoadCapacitorLoad, YourSoftwareUser.UserId, 0) Then
                    Else
                        If LastLoad.UserId <> YourSoftwareUser.UserId Then Throw New SoftwareUserCanNotDeleteLoadException
                    End If

                    'کنترل اتمام زمان حذف بار
                    If InstanceAnnouncements.IsAnnouncemenetAnnounceTimePassed(LastLoad.AnnouncementGroupId, LastLoad.AnnouncementSubGroupId, New R2CoreDateAndTime With {.Time = LastLoad.AnnounceTime}) Then Throw New LoadDeleteTimePassedException

                    'کنترل تاریخ بار
                    If LastLoad.AnnounceDate <> _DateTimeService.GetCurrentShamsiDate Then Throw New LoadDeleteTimePassedException

                    'کنترل وضعیت بار
                    If Not (LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow) Then Throw New LoadHandlingNotAllowedBecuaseLoadStatusException

                    'بررسی بار فردا
                    Dim ComposeSearchString As String = LastLoad.AnnouncementSubGroupId.ToString + "="
                    Dim AllTommorowConfigforAHId As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, LastLoad.AnnouncementGroupId), "-")
                    Dim AllTommorowConfigforAHSGId = Split(Mid(AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllTommorowConfigforAHId.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), ";")
                    Dim IsTommorowLoadRegisteringActive As Boolean = AllTommorowConfigforAHSGId(0)
                    Dim TommorowLoadRegisteringFirstTime As String = AllTommorowConfigforAHSGId(1)
                    Dim TommorowLoadRegisteringEndTime As String = AllTommorowConfigforAHSGId(2)
                    Dim CurrentTime As String = _DateTimeService.GetCurrentTime
                    If IsTommorowLoadRegisteringActive And (CurrentTime >= TommorowLoadRegisteringFirstTime And CurrentTime <= TommorowLoadRegisteringEndTime) And LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then
                    Else
                        Dim Timing = InstanceAnnouncementTiming.GetTiming(LastLoad.AnnouncementGroupId, LastLoad.AnnouncementSubGroupId, _DateTimeService.GetCurrentTime)
                        If InstanceAnnouncements.IsAnnouncemenetAnnounceTimePassed(LastLoad.AnnouncementGroupId, LastLoad.AnnouncementSubGroupId, New R2CoreDateAndTime With {.Time = LastLoad.AnnounceTime}) Then
                            Throw New LoadDeleteTimePassedException
                        End If
                    End If

                    'حذف بار
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@YourLoadId", SqlDbType.BigInt) : P.Value = YourLoadId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Deleting
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourAmount", SqlDbType.BigInt) : P.Value = LastLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadDeleting"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'ثبت باراعتباری
                    InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourLoadId)

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SoftwareUserCanNotDeleteLoadException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadDeleteTimePassedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadHandlingNotAllowedBecuaseLoadStatusException
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

            Public Sub LoadCancelling(YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try

                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstancePermisions = New R2CorePermissionsManager
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    Dim LastLoad = InstanceLoad.GetLoadSimpleModel(YourLoadId, True)

                    'کنترل تاریخ بار
                    If LastLoad.AnnounceDate <> _DateTimeService.GetCurrentShamsiDate Then Throw New LoadCancellingTimePassedException

                    'کنترل وضعیت بار
                    If LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or LastLoad.LoadStatusId = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New LoadHandlingNotAllowedBecuaseLoadStatusException

                    'کنترل زمان کنسلی بار
                    If Not InstanceAnnouncements.IsAnnouncemenetAnnounceTimePassed(LastLoad.AnnouncementGroupId, LastLoad.AnnouncementSubGroupId, New R2CoreDateAndTime With {.Time = LastLoad.AnnounceTime}) Then Throw New LoadCancellingTimeNotReachedException

                    'کنترل مجوز دسترسی کاربر
                    If Not InstancePermisions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanCancellingLoadsViaLoadStatus, YourSoftwareUser.UserId, LastLoad.LoadStatusId) Then Throw New SoftwareUserCanNotCancellingLoadException

                    'کنسلی بار
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@YourLoadId", SqlDbType.BigInt) : P.Value = YourLoadId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Cancelling
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourAmount", SqlDbType.BigInt) : P.Value = LastLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadCancelling"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                    'ثبت باراعتباری
                    InstanceTurnCancellation.LoadforTurnCancellationUnActiving(YourLoadId)

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadCancellingTimePassedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadHandlingNotAllowedBecuaseLoadStatusException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadCancellingTimeNotReachedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SoftwareUserCanNotCancellingLoadException
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

            Public Sub LoadFreeLining(YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstancePermisions = New R2CorePermissionsManager
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    Dim LastLoad = InstanceLoad.GetLoadSimpleModel(YourLoadId, True)

                    'کنترل زمان خط آزاد نمودن بار
                    If InstanceAnnouncements.IsAnnouncemenetAnnounceTimePassed(LastLoad.AnnouncementGroupId, LastLoad.AnnouncementSubGroupId, New R2CoreDateAndTime With {.Time = LastLoad.AnnounceTime}) Then Throw New LoadFreeLiningTimePassedException

                    'کنترل تاریخ بار
                    If LastLoad.AnnounceDate <> _DateTimeService.GetCurrentShamsiDate Then Throw New LoadFreeLiningTimePassedException

                    'کنترل وضعیت بار
                    If LastLoad.LoadStatusId <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Then Throw New LoadHandlingNotAllowedBecuaseLoadStatusException

                    'کنترل مجوز دسترسی کاربر
                    If Not InstancePermisions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanFreeLiningLoadsViaLoadStatus, YourSoftwareUser.UserId, LastLoad.LoadStatusId) Then Throw New SoftwareUserCanNotFreeLiningLoadException

                    'خط آزاد نمودن بار
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@YourLoadId", SqlDbType.BigInt) : P.Value = YourLoadId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.FreeLining
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourAmount", SqlDbType.BigInt) : P.Value = LastLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadFreeLining"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadFreeLiningTimePassedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadHandlingNotAllowedBecuaseLoadStatusException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SoftwareUserCanNotFreeLiningLoadException
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

            Public Sub LoadSedimenting(YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)
                    Dim InstancePermisions = New R2CorePermissionsManager
                    Dim InstanceTurnCancellation As New R2CoreTransportationAndLoadNotificationTurnCancellationManager(_DateTimeService)

                    Dim LastLoad = InstanceLoad.GetLoadSimpleModel(YourLoadId, True)

                    'کنترل تعداد باقیمانده بار
                    If InstanceLoad.GetLoadReminder(YourLoadId, False) = 0 Then Throw New CanNotSedimentLoadBecuaseReminderException

                    'کنترل وضعیت بار
                    If LastLoad.LoadStatusId <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered Then Throw New LoadHandlingNotAllowedBecuaseLoadStatusException

                    'کنترل مجوز دسترسی کاربر
                    If Not InstancePermisions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanSedimentingLoad, YourSoftwareUser.UserId, LastLoad.LoadStatusId) Then Throw New SoftwareUserCanNotSedimentingLoadException

                    'خط آزاد نمودن بار
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@YourLoadId", SqlDbType.BigInt) : P.Value = YourLoadId
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadAccountingTypes.Sedimenting
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourAmount", SqlDbType.BigInt) : P.Value = LastLoad.TotalNumber
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUser.UserId
                    CmdSql.Parameters.Add(P)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.LoadSedimenting"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As LoadNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As LoadHandlingNotAllowedBecuaseLoadStatusException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SoftwareUserCanNotSedimentingLoadException
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

        End Class

        'BPTChanged
        Namespace Exceptions

            Public Class LoadRegisterTimePassedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "زمان ثبت بار برای زیرگروه اعلام بار مورد نظر به پایان رسیده است"
                    End Get
                End Property
            End Class

            Public Class LoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "امکان ثبت بار برای این زیر گروه اعلام بار مجاز نیست "
                    End Get
                End Property
            End Class

            Public Class TommarowLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "امکان ثبت بار فردا برای این زیر گروه اعلام بار مجاز نیست "
                    End Get
                End Property
            End Class

            Public Class LoadRegisteringforShamsiDateNotAllowedException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "اعلام بار برای  " + _Message + " امکان پذیر نیست"
                    End Get
                End Property
            End Class

            Public Class ChangeAnnouncementGroupOrAnnouncementSubGroupInEditingNotAllowedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تغییر گروه و یا زیرگروه اعلام بار امکان پذیر نیست"
                    End Get
                End Property
            End Class

            Public Class LoadHandlingNotAllowedBecuaseLoadStatusException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "وضعیت فعلی بار مانع از اجرای این فرآیند شد"
                    End Get
                End Property
            End Class

            Public Class SoftwareUserCanNotDeleteLoadException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شما مجوز حذف بار را ندارید"
                    End Get
                End Property
            End Class

            Public Class LoadDeleteTimePassedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "زمان حذف بار به پایان رسیده است"
                    End Get
                End Property
            End Class

            Public Class LoadCancellingTimePassedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "زمان کنسلی بار به پایان رسیده است"
                    End Get
                End Property
            End Class

            Public Class LoadCancellingTimeNotReachedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "امکان کنسلی بار فقط پس از اعلام بار امکان پذیر است"
                    End Get
                End Property
            End Class

            Public Class SoftwareUserCanNotCancellingLoadException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شما مجوز کنسلی بار را ندارید"
                    End Get
                End Property
            End Class

            Public Class SoftwareUserCanNotFreeLiningLoadException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شما مجوز خط آزاد کردن بار را ندارید"
                    End Get
                End Property
            End Class

            Public Class SoftwareUserCanNotSedimentingLoadException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شما مجوز رسوب بار را ندارید"
                    End Get
                End Property
            End Class

            Public Class LoadFreeLiningTimePassedException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "زمان خط آزاد کردن بار به پایان رسیده است"
                    End Get
                End Property
            End Class

            Public Class CanNotSedimentLoadBecuaseReminderException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "با توجه به تعداد بار ، فرآیند رسوب بار امکان پذیر نیست"
                    End Get
                End Property
            End Class


        End Namespace


    End Namespace

    Namespace LoadCapacitorLoadOtherThanManipulation
        Public Class R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadOtherThanManipulationManager
            Private _DateTime As New R2DateTime

            Private Sub LoadCapacitorLoadAllocating_(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    'کنترل وضعیت بار
                    If YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    'ثبت اکانت
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Allocating, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                Catch ex As LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadCapacitorLoadAllocating(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                Try
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                    LoadCapacitorLoadAllocating_(NSSLoadCapacitorLoad, YourUserNSS)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadCapacitorLoadAllocating(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    LoadCapacitorLoadAllocating_(YourNSSLoadCapacitorLoad, YourUserNSS)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadCapacitorLoadAllocationCancelling(YournEstelamId As Int64, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    'کنترل وضعیت بار - نیازی نیست
                    'ثبت اکانت
                    InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YournEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.AllocationCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                Catch ex As LoadCapacitorLoadNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadCapacitorLoadReleasing(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure, YourTransaction As SqlCommand, YourCurrentDateTime As R2CoreDateAndTime, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceAnnouncementHall = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    Dim NSSAnnouncementHall = InstanceAnnouncementHall.GetNSSAnnouncementHall(YourNSSLoadCapacitorLoad.AHId)
                    Dim NSSAnnouncementsubGroup = InstanceAnnouncementHall.GetNSSAnnouncementsubGroupByLoaderTypeId(YourNSSLoadCapacitorLoad.nTruckType)

                    'کنترل زمان ترخیص بار 
                    If YourNSSLoadCapacitorLoad.LoadStatus <> R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then
                        If Not InstanceAnnouncementHall.IsAnnouncemenetHallAnnounceTimePassed(NSSAnnouncementHall.AHId, NSSAnnouncementsubGroup.AHSGId, New R2CoreDateAndTime With {.Time = YourNSSLoadCapacitorLoad.dTimeElam}) Then
                            If Not YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined Then Throw New LoadCapacitorLoadReleaseTimeNotReachedException
                        End If
                    End If

                    'ترخیص بار
                    'کنترل وضعیت بار
                    If YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException

                    'کنترل تعداد بار
                    If YourNSSLoadCapacitorLoad.nCarNum < 1 Then Throw New LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException
                    YourTransaction.CommandText = "Update DBTransport.dbo.TbElam Set nCarNum=nCarNum-1,dDateExit='" & YourCurrentDateTime.ShamsiDate & "',dTimeExit='" & YourCurrentDateTime.Time & "' Where nEstelamId=" & YourNSSLoadCapacitorLoad.nEstelamId & ""
                    YourTransaction.ExecuteNonQuery()

                    'ثبت اکانت
                    'InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Releasing, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                Catch ex As LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException
                    Throw ex
                Catch ex As LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    Throw ex
                Catch ex As LoadCapacitorLoadReleaseTimeNotReachedException
                    Throw ex
                Catch ex As GetNSSException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Sub LoadCapacitorLoadPermissionCancelling(YournEstelamId As Int64, YourLoadCapacitorLoadResuscitationFlag As Boolean, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                    Dim InstanceLoadCapacitorAccounting = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorAccountingManager
                    Dim InstanceLoadCapacitorLoadManipulation = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManipulationManager
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                    Dim NSSAnnouncementHall = InstanceAnnouncements.GetNSSAnnouncementHall(NSSLoadCapacitorLoad.AHId)
                    'کنسلی مجوز بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Select nEstelamId from DBTransport.dbo.TbElam  with (tablockx) Where nEstelamId=" & YournEstelamId & ""
                    CmdSql.ExecuteNonQuery()
                    'کنترل وضعیت بار
                    If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Sedimented Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    If YourLoadCapacitorLoadResuscitationFlag Then
                        'بازگردانی بار
                        CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set nCarNum=nCarNum+1 Where nEstelamId=" & NSSLoadCapacitorLoad.nEstelamId & ""
                        InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    Else
                        'عدم بازگردانی بار و کنسلی کل بار باقیمانده
                        InstanceLoadCapacitorLoadManipulation.LoadCapacitorLoadCancelling(NSSLoadCapacitorLoad, YourUserNSS)
                        InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                        InstanceLoadCapacitorAccounting.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Cancelling, NSSLoadCapacitorLoad.nCarNum + 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    End If
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

        Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadOtherThanManipulationManagement
            Private Shared _DateTime As New R2DateTime

            Public Shared Sub LoadCapacitorLoadPermissionCancelling(YournEstelamId As Int64, YourLoadResuscitationFlag As Boolean, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceLoadCapacitorLoad = New R2CoreTransportationAndLoadNotificationInstanceLoadCapacitorLoadManager
                    Dim NSSLoadCapacitorLoad = InstanceLoadCapacitorLoad.GetNSSLoadCapacitorLoad(YournEstelamId, True)
                    Dim NSSAnnouncementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementHall(NSSLoadCapacitorLoad.AHId)
                    'کنسلی مجوز بار
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Select nEstelamId from DBTransport.dbo.TbElam  with (tablockx) Where nEstelamId=" & YournEstelamId & ""
                    CmdSql.ExecuteNonQuery()
                    'کنترل وضعیت بار
                    If NSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    If YourLoadResuscitationFlag Then
                        'بازگردانی بار
                        CmdSql.CommandText = "Update DBTransport.dbo.TbElam Set nCarNum=nCarNum+1,bFlag=1,LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.FreeLined & " Where nEstelamId=" & NSSLoadCapacitorLoad.nEstelamId & ""
                        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                        CmdSql.ExecuteNonQuery()
                    Else
                        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(NSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.LoadPermissionCancelling, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                    End If
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Shared Sub LoadCapacitorLoadAllocating_(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    'کنترل وضعیت بار
                    If YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Cancelled Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Deleted Or YourNSSLoadCapacitorLoad.LoadStatus = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow Then Throw New LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
                    'ثبت اکانت
                    R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(YourNSSLoadCapacitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.Allocating, 1, Nothing, Nothing, Nothing, YourUserNSS.UserId))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Sub LoadCapacitorLoadAllocating(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
                Try
                    LoadCapacitorLoadAllocating_(YourNSSLoadCapacitorLoad, YourUserNSS)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Shared Sub TransferringTommorowLoads()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim CurrentTimeforTransferringTommorowLoads As String = _DateTime.GetCurrentTime()
                    Dim CurrentDateforTransferringTommorowLoads As String = _DateTime.GetCurrentShamsiDate()
                    If Not ((CurrentTimeforTransferringTommorowLoads >= "00:00:00") And (CurrentTimeforTransferringTommorowLoads <= "01:00:00")) Then
                        Return
                    End If
                    'لیست کامل باری که باید انتقال یابد
                    Dim Lst = R2CoreTransportationAndLoadNotificationMClassLoadCapacitorLoadManagement.GetAllofTommorowLoads()
                    'باری برای رسوب وجود ندارد
                    If IsNothing(Lst) Or Lst.Count = 0 Then Return

                    CmdSql.CommandText = "Update dbtransport.dbo.tbElam Set LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered & ",dDateElam='" & CurrentDateforTransferringTommorowLoads & "',dTimeElam='00:00:00'
                                       Where LoadStatus=" & R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow & ""
                    CmdSql.Connection.Open()
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()

                    For Each LoadCapcitorLoad In Lst
                        R2CoreTransportationAndLoadNotificationMClassLoadCapacitorAccountingManagement.InsertAccounting(New R2CoreTransportationAndLoadNotificationStandardLoadCapacitorAccountingStructure(LoadCapcitorLoad.nEstelamId, R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.ChangingStatusTommorowLoads, 1, Nothing, Nothing, Nothing, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser.UserId))
                    Next
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        'BPTChanged
        Public Class R2CoreTransportationAndLoadNotificationChangingStatusOfTommorowLoadsManager

            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourR2DateTimeService As IR2DateTimeService)
                _DateTimeService = YourR2DateTimeService
            End Sub

            Private Sub ChangingStatusOfTommorowLoadsMainProcess(YourSoftwareUserid As Int64)
                Dim CmdSql As New SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim P As SqlClient.SqlParameter
                    P = New SqlClient.SqlParameter("@YourTargetLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.Registered
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourWantedLoadStatusId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorLoadStatuses.RegisteredforTommorow
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourLoadAccountingTypeId", SqlDbType.TinyInt) : P.Value = R2CoreTransportationAndLoadNotificationLoadCapacitorAccountingTypes.ChangingStatusTommorowLoads
                    CmdSql.Parameters.Add(P)
                    P = New SqlClient.SqlParameter("@YourSoftwareUserId", SqlDbType.BigInt) : P.Value = YourSoftwareUserid
                    CmdSql.Parameters.Add(P)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandType = CommandType.StoredProcedure
                    CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.ChangingStatusOfTommorowLoads"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SqlException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub SendSMSChangingStatusOfTommorowLoads()
                Try
                    Dim InstanceConfiguration = New R2CoreConfigurationsManager()
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)

                    'کاربران
                    Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, 2).Split("-")
                    Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                    For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                        LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                    Next
                    'ارسال اس ام اس
                    Dim myData = New SMSCreationData With {.Data1 = String.Empty}
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotificationSMSTypes.ChangingStatusOfTommorowLoadsSucceeded, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub

            Private Shared _ControllingChangingStatusOfTommorowLoadsFlag As Boolean = False
            Public Sub ChangingStatusOfTommorowLoads(YourSoftwareUserId As Int64)
                Try
                    Dim InstanceConfigurations = New R2CoreConfigurationsManager

                    Dim TimeOfDay = _DateTimeService.GetCurrentTickofTime
                    Dim StartTime = TimeSpan.Parse(InstanceConfigurations.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, 0)).Ticks
                    Dim EndTime = TimeSpan.Parse(InstanceConfigurations.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.TommorowLoads, 1)).Ticks
                    If Not (TimeOfDay >= StartTime And TimeOfDay <= EndTime) Then
                        _ControllingChangingStatusOfTommorowLoadsFlag = False
                        Return
                    End If
                    If _ControllingChangingStatusOfTommorowLoadsFlag Then Return

                    ChangingStatusOfTommorowLoadsMainProcess(YourSoftwareUserId)
                    _ControllingChangingStatusOfTommorowLoadsFlag = True
                    SendSMSChangingStatusOfTommorowLoads()
                Catch ex As DataBaseException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

    Namespace Exceptions
        Public Class BaseInfFailedException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوبت در سامانه ندارید" + vbCrLf + "احصاء اطلاعات پایه ناموفق است" + vbCrLf + "با پشتیبانی سامانه تماس بگیرید"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadRegisteringInHolidayNotAllowedException
            Inherits ApplicationException

            Private _Message As String = String.Empty
            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اعلام بار " + _Message + " در روز تعطیل امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class PleaseSelectSpecialLoadRadioButtonException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مشخصات بار را تعیین کنید"
                End Get
            End Property
        End Class

        Public Class UnabletoCancellingorFreeliningTommorowLoadException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "بار فردا قابل کنسل شدن و یا آزادسازی نیست"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت بار با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadnCarNumKolCanNotBeZeroException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقادیر صفر و کمتر از آن برای تعداد بار مجاز نیست"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadRegisterTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان ثبت بار برای زیرگروه اعلام بار مورد نظر به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadReRegisteringTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان اعلام مجدد بار مورد نظر به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadEditTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان ویرایش بار برای زیرگروه اعلام بار مورد نظر به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadEditingChangeAHIdNotAllowedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تغییر گروه اعلام بار امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadDeleteTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان حذف بار به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadCancelTimeNotReachedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان کنسلی بار فقط پس از اعلام بار امکان پذیر است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadReleaseTimeNotReachedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان ترخیص بار فقط پس از اعلام بار امکان پذیر است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadFreeLiningTimePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان خط آزاد نمودن بار فقط قبل از اعلام بار امکان پذیر است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadHandlingNotAllowedBecuaseLoadStatusException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت فعلی بار مانع از اجرای این فرآیند است"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadReleaseNotAllowedBecuasenCarNumException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تعداد باقیمانده بار به صفر رسیده است و امکان ترخیص بار وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadReRegisteringNotAllowedBecuasenCarNumException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تعداد باقیمانده بار به صفر رسیده است و امکان اعلام مجدد بار وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "بار با کدمخزن بار مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorAccountingTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع اکانت مخزن بار با کد مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadRegisteringNotAllowedforThisAnnouncementsubGroupException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان ثبت بار برای گروه اعلام بار مورد نظر فعلا مجاز نیست "
                End Get
            End Property
        End Class

        Public Class SoftwareUserCanNotDeleteLoadCapacitorLoadException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شما مجوز حذف بار را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserCanNotCancellingLoadCapacitorLoadException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شما مجوز کنسلی بار را ندارید"
                End Get
            End Property
        End Class

        Public Class LoadCapacitorLoadReRegisteringNotAllowedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان اعلام مجدد بار مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class EditOrDeleteReRegisteredLoadNotAllowedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان حذف یا ویرایش باری که مجددا اعلام بار شده است وجود ندارد"
                End Get
            End Property
        End Class



    End Namespace

End Namespace
