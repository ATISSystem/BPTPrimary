

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
Imports R2Core.PublicProc
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SiteIsBusy
Imports R2Core.SMS.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2Core.SQLInjectionPrevention
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadingAndDischargingPlaces.Exceptions



Namespace LoadingAndDischargingPlaces

    Public MustInherit Class LoadingAndDischargingPlaces
        Public Shared ReadOnly Property None = 1000
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _LADPlaceId = Int64.MinValue
            _LADPlaceTitle = String.Empty
            _LADPlaceTel = String.Empty
            _LADPlaceAddress = String.Empty
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _LoadingActive = Boolean.FalseString
            _DischargingActive = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourLADPlaceId As Int64, YourLADPlaceTitle As String, YourLADPlaceTel As String, YourLADPlaceAddress As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourLoadingActive As Boolean, YourDischargingActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourLADPlaceId, YourLADPlaceTitle)
            _LADPlaceId = YourLADPlaceId
            _LADPlaceTitle = YourLADPlaceTitle
            _LADPlaceTel = YourLADPlaceTel
            _LADPlaceAddress = YourLADPlaceAddress
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _LoadingActive = YourLoadingActive
            _DischargingActive = YourDischargingActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property LADPlaceId As Int64
        Public Property LADPlaceTitle As String
        Public Property LADPlaceTel As String
        Public Property LADPlaceAddress As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property LoadingActive As Boolean
        Public Property DischargingActive As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    Namespace Exceptions
        Public Class LoadingAndDischargingSendSMSFailedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس با خطا مواجه شد"
                End Get
            End Property
        End Class

        Public Class LoadingAndDischargingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری یا محل تخلیه با کد شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class LoadingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری با مشخصات مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class DischargingPlaceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "محل تخلیه با مشخصات مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        'BPTChnaged
        Public Class LoadingPlaceIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا بارگیری مجاز نیست"
                End Get
            End Property
        End Class

        Public Class DischargingPlaceIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "محل تخلیه مجاز نیست"
                End Get
            End Property
        End Class

    End Namespace

    Public Class R2CoreTransportationAndLoadNotificationMClassLoadingAndDischargingPlacesManager

        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)
        Private _LoadingPlaceEquivalent = "مبدا بارگیری"
        Private _DischargingPlaceEquivalent = "محل تخلیه"

        Public Function GetNSSLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure
            Try
                Dim NSS As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure = Nothing
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
                    If Da.Fill(Ds) <> 0 Then NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                    NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
                Return NSS
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUnActiveLoadingAndDischargingPlaces() As List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where (LoadingActive=0 or DischargingActive=0) and Deleted=0 Order By LADPlaceTitle", 300, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(DS.Tables(0).Rows(Loopx).Item("LADPlaceId"), DS.Tables(0).Rows(Loopx).Item("LADPlaceTitle").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("Time").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("LoadingActive"), DS.Tables(0).Rows(Loopx).Item("DischargingActive"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadingAndDischargingPlaces_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where Left(LADPlaceTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' and ViewFlag=1 and Deleted=0 Order By LADPlaceTitle", 300, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(DS.Tables(0).Rows(Loopx).Item("LADPlaceId"), DS.Tables(0).Rows(Loopx).Item("LADPlaceTitle").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, DS.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("Time").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(0).Item("LoadingActive"), DS.Tables(0).Rows(0).Item("DischargingActive"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoadingAndDischargingPlaceDelete(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set Deleted=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadingPlaceChangeActiveStatus(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim CurrentStatus = GetNSSLoadingAndDischargingPlace(YourNSSLADPlace.LADPlaceId, True).LoadingActive
                If CurrentStatus Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=0 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendSMSLoadingAndDischargingPLacesChangeStatus(YourNSSLADPlace.LADPlaceTitle, _LoadingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not CurrentStatus))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DischargingPlaceChangeActiveStatus(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim CurrentStatus = GetNSSLoadingAndDischargingPlace(YourNSSLADPlace.LADPlaceId, True).DischargingActive
                If CurrentStatus Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=0 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=1 Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendSMSLoadingAndDischargingPLacesChangeStatus(YourNSSLADPlace.LADPlaceTitle, _DischargingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not CurrentStatus))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Sub SendSMSLoadingAndDischargingPLacesChangeStatus(YourLoadingDischargingPlaceTitle As String, YourLoadingDischargingNote As String, YourStatus As String)
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceMoneyWalletManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                'کنترل فعال بودن سرویس اس ام اس
                If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotification.ConfigurationsManagement.R2CoreTransportationAndLoadNotificationConfigurations.LoadingDischargingPlaces, 2).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'کانتنت پیام
                Dim myData = New SMSCreationData
                myData.Data1 = YourLoadingDischargingPlaceTitle
                myData.Data2 = YourLoadingDischargingNote
                myData.Data3 = YourStatus
                'ارسال اس ام اس
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_DateTimeService)
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotification.SMS.SMSTypes.R2CoreTransportationAndLoadNotificationSMSTypes.LoadingAndDischargingPLacesChangeStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New LoadingAndDischargingSendSMSFailedException
            Catch ex As LoadingAndDischargingSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function LoadingAndDischargingPlaceRegister(YourNSSLADPlace As R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                If YourNSSLADPlace.LADPlaceTitle = String.Empty Then Return Nothing
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _DateTimeService.GetCurrentDateAndTime
                If YourNSSLADPlace.LADPlaceId = 0 Then
                    CmdSql.CommandText = "Select Top 1 LADPlaceId From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces with (tablockx) Order By LADPlaceId Desc"
                    Dim LADPlaceIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    YourNSSLADPlace.LADPlaceId = LADPlaceIdNew
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces(LADPlaceId,LADPlaceTitle,DateTimeMilladi,DateShamsi,Time,UserId,LoadingActive,DischargingActive,ViewFlag,Deleted)
                                            Values(" & LADPlaceIdNew & ",'" & YourNSSLADPlace.LADPlaceTitle & "','" & D.DateTimeMilladi & "','" & D.ShamsiDate & "','" & D.Time & "'," & YourNSSSoftwareUser.UserId & ",1,1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Else
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LADPlaceTitle='" & YourNSSLADPlace.LADPlaceTitle & "' Where LADPlaceId=" & YourNSSLADPlace.LADPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return YourNSSLADPlace.LADPlaceId
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsActiveLoadingPlace(YourLoadingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                If NSS.LoadingActive = False Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsActiveDischargingPlace(YourDischargingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourDischargingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardLoadingAndDischargingPlacesStructure(Ds.Tables(0).Rows(0).Item("LADPlaceId"), Ds.Tables(0).Rows(0).Item("LADPlaceTitle").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceTel").TRIM, Ds.Tables(0).Rows(0).Item("LADPlaceAddress").TRIM, Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("Time").trim, Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("LoadingActive"), Ds.Tables(0).Rows(0).Item("DischargingActive"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                If NSS.DischargingActive = False Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    'BPTChanged
    Public Class RawLoadingAndDischargingPlace
        Public Property LADPlaceId As Int64
        Public Property LADPlaceTitle As String
        Public Property LADPlaceTel As String
        Public Property LADPlaceAddress As String
        Public Property LoadingActive As Boolean
        Public Property DischargingActive As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadingAndDischargingPlacesManager

        Private _DateTimeService As New R2DateTimeService
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)
        Private _LoadingPlaceEquivalent = "مبدا بارگیری"
        Private _DischargingPlaceEquivalent = "محل تخلیه"

        Public Function GetLoadingAndDischargingPlaces_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                       "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceTitle like N'%" & YourSearchString & "%' and Deleted=0
                        Order By LADPlaceTitle 
                        for json path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceTitle like N'%" & YourSearchString & "%' and Deleted=0
                        Order By LADPlaceTitle 
                        for json path", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoadingAndDischargingPlace(YourLADPlaceId As Int64, YourImmediately As Boolean) As RawLoadingAndDischargingPlace
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                                                       from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                                                       Where LADPlaceId=" & YourLADPlaceId & " and Deleted=0
                                                       Order By LADPlaceTitle")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select LADPlaces.LADPlaceId,LADPlaces.LADPlaceTitle,LADPlaces.LADPlaceTel,LADPlaces.LADPlaceAddress,LADPlaces.LoadingActive,LADPlaces.DischargingActive
                        from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces as LADPlaces
                        Where LADPlaceId=" & YourLADPlaceId & " and Deleted=0
                        Order By LADPlaceTitle", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New LoadingAndDischargingPlaceNotFoundException
                    End If
                End If
                Return New RawLoadingAndDischargingPlace With {.LADPlaceId = DS.Tables(0).Rows(0).Item("LADPlaceId"), .LADPlaceTitle = DS.Tables(0).Rows(0).Item("LADPlaceTitle").trim, .LADPlaceTel = DS.Tables(0).Rows(0).Item("LADPlaceTel").trim, .LADPlaceAddress = DS.Tables(0).Rows(0).Item("LADPlaceAddress").trim, .LoadingActive = DS.Tables(0).Rows(0).Item("LoadingActive"), .DischargingActive = DS.Tables(0).Rows(0).Item("DischargingActive")}
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function LoadingAndDischargingPlaceRegister(YourRawLoadingAndDischargingPlace As RawLoadingAndDischargingPlace) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                If YourRawLoadingAndDischargingPlace.LADPlaceTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim D = _DateTimeService.GetCurrentDateAndTime
                CmdSql.CommandText = "Select Top 1 LADPlaceId From R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces with (tablockx) Order By LADPlaceId Desc"
                Dim LADPlaceIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces(LADPlaceId,LADPlaceTitle,LADPlaceTel,LADPlaceAddress,DateTimeMilladi,DateShamsi,Time,UserId,LoadingActive,DischargingActive,ViewFlag,Deleted)
                                            Values(" & LADPlaceIdNew & ",'" & YourRawLoadingAndDischargingPlace.LADPlaceTitle & "','" & YourRawLoadingAndDischargingPlace.LADPlaceTel & "','" & YourRawLoadingAndDischargingPlace.LADPlaceAddress & "','" & D.DateTimeMilladi & "','" & D.ShamsiDate & "','" & D.Time & "'," & InstanceSoftwareUsers.GetSystemUserId & ",1,1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return LADPlaceIdNew
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

        Public Sub LoadingAndDischargingPlaceUpdating(YourLoadingAndDischargingPlace As RawLoadingAndDischargingPlace)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                If YourLoadingAndDischargingPlace.LADPlaceTitle = String.Empty Then Throw New DataEntryException
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LADPlaceTitle='" & YourLoadingAndDischargingPlace.LADPlaceTitle & "',LADPlaceTel='" & YourLoadingAndDischargingPlace.LADPlaceTel & "',LADPlaceAddress='" & YourLoadingAndDischargingPlace.LADPlaceAddress & "' Where LADPlaceId=" & YourLoadingAndDischargingPlace.LADPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
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

        Public Sub LoadingAndDischargingPlaceDelete(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update  R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set Deleted=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoadingPlaceChangeActiveStatus(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim LADPlace = GetLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId, True)
                If LADPlace.LoadingActive Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=0 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set LoadingActive=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendLoadingAndDischargingPLacesChangeStatusSMS(LADPlace.LADPlaceTitle, _LoadingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not LADPlace.LoadingActive))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DischargingPlaceChangeActiveStatus(YourLoadingAndDischargingPlaceId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim LADPlace = GetLoadingAndDischargingPlace(YourLoadingAndDischargingPlaceId, True)
                If LADPlace.DischargingActive Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=0 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Else
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Set DischargingActive=1 Where LADPlaceId=" & YourLoadingAndDischargingPlaceId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If
                'ارسال اس ام اس
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                SendLoadingAndDischargingPLacesChangeStatusSMS(LADPlace.LADPlaceTitle, _DischargingPlaceEquivalent, InstancePublicProcedures.GetBooleanVariablePersianEquivalent(Not LADPlace.DischargingActive))
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SendLoadingAndDischargingPLacesChangeStatusSMS(YourLoadingDischargingPlaceTitle As String, YourLoadingDischargingNote As String, YourStatus As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                'کاربران
                Dim TargetUsers = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.LoadingDischargingPlaces, 2).Split("-")
                Dim LstSoftwareUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                For LoopxUsers As Int64 = 0 To TargetUsers.Count - 1
                    LstSoftwareUsers.Add(InstanceSoftwareUsers.GetNSSUser(Convert.ToInt64(TargetUsers(LoopxUsers))))
                Next
                'کانتنت پیام
                Dim myData = New SMSCreationData With {.Data1 = YourLoadingDischargingPlaceTitle, .Data2 = YourLoadingDischargingNote, .Data3 = YourStatus}
                'ارسال اس ام اس
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_DateTimeService)
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstSoftwareUsers, R2CoreTransportationAndLoadNotification.SMS.SMSTypes.R2CoreTransportationAndLoadNotificationSMSTypes.LoadingAndDischargingPLacesChangeStatus, InstanceSMSHandling.RepeatSMSCreationData(myData, LstSoftwareUsers.Count), True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New LoadingAndDischargingSendSMSFailedException
            Catch ex As LoadingAndDischargingSendSMSFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function IsActiveLoadingPlace(YourLoadingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select LoadingActive from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourLoadingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Return Ds.Tables(0).Rows(0).Item("LoadingActive")
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsActiveDischargingPlace(YourDischargingPlaceId As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select DischargingActive from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadingAndDischargingPlaces Where LADPlaceId=" & YourDischargingPlaceId & "", 300, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New LoadingAndDischargingPlaceNotFoundException
                Return Ds.Tables(0).Rows(0).Item("DischargingActive")
            Catch ex As LoadingAndDischargingPlaceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class



End Namespace

