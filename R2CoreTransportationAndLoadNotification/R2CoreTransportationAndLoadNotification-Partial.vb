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
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions


Namespace TruckLoaderTypes

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationTruckLoaderTypeManagement
        Public Shared Function GetTonajMax(YourTruckLoaderName As String) As Int64
            Try
                Dim DS As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 nTonaj from dbtransport.dbo.tbCarType Where strCarName='" & YourTruckLoaderName.Trim() & "'", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return DS.Tables(0).Rows(0).Item("nTonaj")
                Else
                    Throw New TruckLoaderTypeNotFoundException
                End If
            Catch ex As TruckLoaderTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Namespace Exceptions
        Public Class TruckLoaderTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

End Namespace

Namespace BlackIPs
    Public MustInherit Class R2CoreTransportationAndLoadNotificationBlackIPTypes

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceBlackIPsManager
        Private _DateTime As New R2DateTime


    End Class


End Namespace

Namespace MobileProcessesManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationMobileProcesses
        Public Shared ReadOnly TruckDriverInformation As Int64 = 1
        Public Shared ReadOnly TruckInformation As Int64 = 2
        Public Shared ReadOnly Top5Turns As Int64 = 3
        Public Shared ReadOnly LoadCapacitor As Int64 = 4
        Public Shared ReadOnly LoadAllocation As Int64 = 5
        Public Shared ReadOnly LoadAllocationPriorityManagement As Int64 = 6
        Public Shared ReadOnly LoadPermissionsIssuedOrderByPriorityReportPage As Int64 = 9
        Public Shared ReadOnly RealTimeTurnRegisterRequest As Int64 = 11

    End Class

End Namespace

Namespace DriverSelfDeclaration

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _DSDId = Int64.MinValue
            _DSDName = String.Empty
            _DSDTitle = String.Empty
            _DefaultValue = String.Empty
            _PersianKeyboard = Boolean.FalseString
            _IsNumeric = Boolean.FalseString
            _DecimalPoint = Boolean.FalseString
            _HasAttachement = Boolean.FalseString
            _Min = String.Empty
            _Max = String.Empty
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _Active = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourDSDId As Int64, YourDSDName As String, YourDSDTitle As String, YourDefaultValue As String, YourPersianKeyboard As Boolean, YourIsNumeric As Boolean, YourDecimalPoint As Boolean, YourHasAttachement As Boolean, YourMin As String, YourMax As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourDSDId, YourDSDName)
            _DSDId = YourDSDId
            _DSDName = YourDSDName
            _DSDTitle = YourDSDTitle
            _DefaultValue = YourDefaultValue
            _PersianKeyboard = YourPersianKeyboard
            _IsNumeric = YourIsNumeric
            _DecimalPoint = YourDecimalPoint
            _HasAttachement = YourHasAttachement
            _Min = YourMin
            _Max = YourMax
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _Active = YourActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property DSDId As Int64
        Public Property DSDName As String
        Public Property DSDTitle As String
        Public Property DefaultValue As String
        Public Property PersianKeyboard As Boolean
        Public Property IsNumeric As Boolean
        Public Property DecimalPoint As Boolean
        Public Property HasAttachement As Boolean
        Public Property Min As String
        Public Property Max As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _DSDId = Int64.MinValue
            _DSDName = String.Empty
            _DSDTitle = String.Empty
            _DefaultValue = String.Empty
            _DSDValue = String.Empty
            _PersianKeyboard = Boolean.FalseString
            _IsNumeric = Boolean.FalseString
            _DecimalPoint = Boolean.FalseString
            _HasAttachement = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourDSDId As Int64, YourDSDName As String, YourDSDTitle As String, YourDefaultValue As String, YourDSDValue As String, YourPersianKeyboard As Boolean, YourIsNumeric As Boolean, YourDecimalPoint As Boolean, YourHasAttachement As Boolean)
            MyBase.New(YourDSDId, YourDSDName)
            _DSDId = YourDSDId
            _DSDName = YourDSDName
            _DSDTitle = YourDSDTitle
            _DefaultValue = YourDefaultValue
            _DSDValue = YourDSDValue
            _PersianKeyboard = YourPersianKeyboard
            _IsNumeric = YourIsNumeric
            _DecimalPoint = YourDecimalPoint
            _HasAttachement = YourHasAttachement
        End Sub

        Public Property DSDId As Int64
        Public Property DSDName As String
        Public Property DSDTitle As String
        Public Property DefaultValue As String
        Public Property DSDValue As String
        Public Property PersianKeyboard As Boolean
        Public Property IsNumeric As Boolean
        Public Property DecimalPoint As Boolean
        Public Property HasAttachement As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationManager
        Private _R2PrimaryFSWS = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
        Private _DateTime As New R2DateTime

        Private Function GetDSDIdFull(YourDSDId As Int64) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Return InstancePublicProcedures.RepeatStr("0", 2 - YourDSDId.ToString().Length) + YourDSDId.ToString()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ControlforExpiredDSDs(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Try
                Dim D = _DateTime.GetCurrentDateTime

                'بررسی منقضی شدن اعتبار مشخصات خوداظهاری
                Dim DS As DataSet
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top 1 DateTimeMilladi from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations 
                           Where nIdCar = " & YourNSSTruck.NSSCar.nIdCar & " Order By DateTimeMilladi desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                Else
                    Dim Diff = DateDiff(DateInterval.Day, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), D.DateTimeMilladi)
                    If Diff > InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 1) Then
                        Throw New DSDsDayLimitExpiredException
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ControlforDSDChangingDayLimit(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure)
            Try
                Dim D = _DateTime.GetCurrentDateTime

                'کنترل زمان انتظار برای تغییر مشخصات خوداظهاری  
                Dim DS As DataSet
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top 1 DateTimeMilladi from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations 
                           Where nIdCar = " & YourNSSTruck.NSSCar.nIdCar & " Order By DateTimeMilladi desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                Else
                    Dim Diff = DateDiff(DateInterval.Day, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), D.DateTimeMilladi)
                    If Diff < InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 0) Then Throw New DayLimitforDSDDataEntryException(InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DriverSelfDeclarationSetting, 0) - Diff)
                End If
            Catch ex As DayLimitforDSDDataEntryException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Function GetNSSDriverSelfDeclarationParameter(YourDSDId As Int64) As R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters Where DSDId=" & YourDSDId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New DriverSelfDeclarationParameterNotFoundException
                Return New R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationParameterStructure(DS.Tables(0).Rows(0).Item("DSDId"), DS.Tables(0).Rows(0).Item("DSDName").trim, DS.Tables(0).Rows(0).Item("DSDTitle").trim, DS.Tables(0).Rows(0).Item("DefaultValue").trim, DS.Tables(0).Rows(0).Item("PersianKeyboard"), DS.Tables(0).Rows(0).Item("IsNumeric"), DS.Tables(0).Rows(0).Item("DecimalPoint"), DS.Tables(0).Rows(0).Item("HasAttachement"), DS.Tables(0).Rows(0).Item("Min").trim, DS.Tables(0).Rows(0).Item("Max").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As DriverSelfDeclarationParameterNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetDeclarations(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourImmediately As Boolean) As List(Of R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure)
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlClient.SqlCommand("Select DataBox.DSDId,DataBox.DSDName,DataBox.DSDTitle,DataBox.DefaultValue,DataBox.PersianKeyboard,DataBox.IsNumeric,DataBox.DecimalPoint,DataBox.HasAttachement,ISNULL(DataBox.DSDValue,'') as DSDValue from 
                                                                    (Select DSDParams.DSDId,DSDParams.DSDName,DSDParams.DSDTitle,DSDParams.DefaultValue,DSDParams.PersianKeyboard,DSDParams.IsNumeric,DSDParams.DecimalPoint,DSDParams.HasAttachement,DataBox.DSDValue from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters as DSDParams
                                                                      left outer  Join 
                                                                    (Select * from R2PrimaryTransportationAndLoadNotification.DBO.TblDriverSelfDeclarations as DSDs Where DSDs.nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and DSDs.RelationActive=1) as DataBox On DSDParams.DSDId=DataBox.DSDId 
                                                                      Where DSDParams.Active=1) as DataBox Order By DataBox.DSDId")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    Da.Fill(DS)
                Else
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select DataBox.DSDId,DataBox.DSDName,DataBox.DSDTitle,DataBox.DefaultValue,DataBox.PersianKeyboard,DataBox.IsNumeric,DataBox.DecimalPoint,DataBox.HasAttachement,ISNULL(DataBox.DSDValue,'') as DSDValue from 
                             (Select DSDParams.DSDId,DSDParams.DSDName,DSDParams.DSDTitle,DSDParams.DefaultValue,DSDParams.PersianKeyboard,DSDParams.IsNumeric,DSDParams.DecimalPoint,DSDParams.HasAttachement,DataBox.DSDValue from R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarationParameters as DSDParams
                                left outer  Join 
                                  (Select * from R2PrimaryTransportationAndLoadNotification.DBO.TblDriverSelfDeclarations as DSDs Where DSDs.nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and DSDs.RelationActive=1) as DataBox On DSDParams.DSDId=DataBox.DSDId 
                                   Where DSDParams.Active=1) as DataBox Order By DataBox.DSDId", 3600, DS, New Boolean)
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationInstanceDriverSelfDeclarationExtendedStructure(DS.Tables(0).Rows(Loopx).Item("DSDId"), DS.Tables(0).Rows(Loopx).Item("DSDName"), DS.Tables(0).Rows(Loopx).Item("DSDTitle"), DS.Tables(0).Rows(Loopx).Item("DefaultValue"), DS.Tables(0).Rows(Loopx).Item("DSDValue"), DS.Tables(0).Rows(Loopx).Item("PersianKeyboard"), DS.Tables(0).Rows(Loopx).Item("IsNumeric"), DS.Tables(0).Rows(Loopx).Item("DecimalPoint"), DS.Tables(0).Rows(Loopx).Item("HasAttachement")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SetDeclarations(YourDSDs As String, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim D = _DateTime.GetCurrentDateTime

                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourDSDs)

                'بررسی محدودیت زمان برای تغییرات
                ControlforDSDChangingDayLimit(YourNSSTruck)

                'ثبت مشخصات
                Dim DSDArray = YourDSDs.Split("|")
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations Set RelationActive=0 Where nIdCar=" & YourNSSTruck.NSSCar.nIdCar & " and RelationActive=1"
                CmdSql.ExecuteNonQuery()
                For Loopx As Int16 = 0 To DSDArray.Length - 1
                    Dim DSDId = DSDArray(Loopx).Split(":")(0)
                    Dim DSDValue = DSDArray(Loopx).Split(":")(1)
                    If DSDValue.Trim = String.Empty Then Throw New DriverSelfDeclarationsEmtpyNotAllowdException
                    Dim NSSDSDParameter = GetNSSDriverSelfDeclarationParameter(DSDId)
                    If NSSDSDParameter.Min = String.Empty And NSSDSDParameter.Max = String.Empty Then
                        'بدون محدودیت
                    ElseIf NSSDSDParameter.Min <> String.Empty And NSSDSDParameter.Max <> String.Empty Then
                        'کنترل با محدوده عددی
                        Dim DSDValueTemp As Int64 = Convert.ToInt64(Val(DSDValue))
                        If DSDValueTemp < Convert.ToInt64(Val(NSSDSDParameter.Min)) Or DSDValueTemp > Convert.ToInt64(Val(NSSDSDParameter.Max)) Then
                            Throw New DSDDataOutofRangeException(NSSDSDParameter.DSDTitle)
                        End If
                    ElseIf NSSDSDParameter.Min <> String.Empty And NSSDSDParameter.Max = String.Empty Then
                        'کنترل مقادیر مجاز
                        Dim ValidEntries = NSSDSDParameter.Min.Split(";")
                        If Not ValidEntries.Contains(DSDValue) Then Throw New DSDDataOutofRangeException(NSSDSDParameter.DSDTitle)
                    End If
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblDriverSelfDeclarations(nIdCar,DSDId,DSDValue,DateTimeMilladi,DateShamsi,Time,UserId,RelationActive)
                                            Values(" & YourNSSTruck.NSSCar.nIdCar & "," & NSSDSDParameter.DSDId & ",'" & DSDValue & "','" & D.DateTimeMilladiFormated & "','" & D.DateShamsiFull & "','" & D.Time & "'," & YourNSSSoftwareUser.UserId & ",1)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As DSDDataOutofRangeException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DayLimitforDSDDataEntryException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DriverSelfDeclarationsEmtpyNotAllowdException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SqlInjectionException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As DriverSelfDeclarationParameterNotFoundException
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

        Public Function GetAllowedLoadingCapacity(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As String
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select R2PrimaryTransportationAndLoadNotification.dbo.GetAllowedLoadingCapacity(" & YourNSSTruck.NSSCar.nIdCar & ") as Allowed", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New DSDBaseInfNotFoundException
                End If
                If DBNull.Value.Equals(DS.Tables(0).Rows(0).Item("Allowed")) Then Throw New DSDBaseInfNotFoundException
                Return DS.Tables(0).Rows(0).Item("Allowed")
            Catch ex As DSDBaseInfNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SaveDSDImage(YourDSDId As Int64, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourBase64StringImage As String)
            Try
                Dim InstanceConfiguration = New R2Core.ConfigurationManagement.R2CoreInstanceConfigurationManager
                If YourBase64StringImage.Length > InstanceConfiguration.GetConfigInt64(R2Core.ConfigurationManagement.R2CoreConfigurations.JPGBitmap, 4) Then Throw New UploadedImageSizeExeededException
                _R2PrimaryFSWS.WebMethodSaveFile(R2CoreTransportationAndLoadNotificationRawGroups.DriverSelfDeclarations, New R2CoreFile(GetDSDIdFull(YourDSDId) + YourNSSTruck.NSSCar.nIdCar.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)).FileName, Convert.FromBase64String(YourBase64StringImage), _R2PrimaryFSWS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword))
            Catch ex As UploadedImageSizeExeededException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class DriverSelfDeclarationParameterNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر خوداظهاری راننده با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class DriverSelfDeclarationsEmtpyNotAllowdException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات را تکمیل کرده و سپس ارسال نمایید"
                End Get
            End Property
        End Class

        Public Class DayLimitforDSDDataEntryException
            Inherits ApplicationException

            Private _DayLimit = Int16.MaxValue

            Public Sub New(YourDayLimit As Int16)
                _DayLimit = YourDayLimit
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "امکان ارسال مشخصات خود اظهاری تا " + _DayLimit.ToString + " روز دیگر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class DSDsDayLimitExpiredException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مدت زمان اعتبار مشخصات خود اظهاری ناوگان منقضی شده است"
                End Get
            End Property
        End Class

        Public Class DSDsNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات خوداظهاری ناوگان یافت نشد"
                End Get
            End Property
        End Class

        Public Class DSDBaseInfNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات پایه در فرآیند خوداظهاری مبنی بر مشخصات وارد شده یافت نشد"
                End Get
            End Property
        End Class

        Public Class DSDDataOutofRangeException
            Inherits ApplicationException

            Private _Message = String.Empty

            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقادیر غیر مجاز - " + _Message
                End Get
            End Property
        End Class


    End Namespace

End Namespace











