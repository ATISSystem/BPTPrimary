
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Text
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.LoggingManagement
Imports R2Core.BaseStandardClass
Imports R2Core.DateAndTimeManagement
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SMS.Exceptions
Imports R2Core.EntityRelationManagement

Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SMS.SMSTypes
Imports R2Core.SMS.SMSHandling.Exceptions
Imports R2Core.SMS.SMSSendRecive
Imports R2Core.SMS.SMSOwners.Exceptions
Imports R2Core.SMS.SMSOwners
Imports R2Core.SMS.SendSMSCodes.Exceptions
Imports R2Core.SMS.SendSMSCodes
Imports R2Core.SMS.RecivedSMSCodes.Exceptions
Imports R2Core.SMS.RecivedSMSCodes
Imports R2Core.SMS.SMSHandling
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2Core.DateTimeProvider
Imports System.Globalization
Imports R2Core.ExceptionManagement
Imports R2Core.SQLInjectionPrevention


Namespace SMS

    Namespace SendSMSCodes
        Public MustInherit Class R2CoreSendSMSCodes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property Success = 1
            Public Shared ReadOnly Property InvalidMobileNumber = 2
            Public Shared ReadOnly Property SMSOwnerIsSendingActiveIsfalse = 3
            Public Shared ReadOnly Property InsufficientSMSMoneyWalletCharge = 4
            Public Shared ReadOnly Property SMSOwnerNotExist = 5
            Public Shared ReadOnly Property DelayToStartSMSSending = 6

        End Class

        Public Class R2CoreStandardSendSMSCodeStructure

            Public Sub New()
                MyBase.New()
                _SendSMSCodeId = Int64.MinValue
                _SendSMSCodeName = String.Empty
                _SendSMSCodeTitle = String.Empty
                _Description = String.Empty
                _SendSMSCode = String.Empty
                _DateTimeMilladi = DateAndTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSendSMSCodeId As Int64, YourSendSMSCodeName As String, YourSendSMSCodeTitle As String, YourDescription As String, YourSendSMSCode As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SendSMSCodeId = YourSendSMSCodeId
                _SendSMSCodeName = YourSendSMSCodeName
                _SendSMSCodeTitle = YourSendSMSCodeTitle
                _Description = YourDescription
                _SendSMSCode = YourSendSMSCode
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SendSMSCodeId As Int64
            Public Property SendSMSCodeName As String
            Public Property SendSMSCodeTitle As String
            Public Property Description As String
            Public Property SendSMSCode As String
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreSendSMSCodeManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetNSSSendSMSCode(YourSendSMSCodeId As Int64) As R2CoreStandardSendSMSCodeStructure
                Try
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                         "Select * from R2PrimarySMSSystem.dbo.TblSendSMSCodes Where SendSMSCodeId=" & YourSendSMSCodeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New SendSMSCodeIdNotFoundExcption
                    Else
                        Return New R2CoreStandardSendSMSCodeStructure(DS.Tables(0).Rows(0).Item("SendSMSCodeId"), DS.Tables(0).Rows(0).Item("SendSMSCodeName").trim, DS.Tables(0).Rows(0).Item("SendSMSCodeTitle").trim, DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("SendSMSCode").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    End If
                Catch ex As SendSMSCodeIdNotFoundExcption
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SendSMSCodeIdNotFoundExcption
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "کد شاخص مرتبط با اعلان وضعیت ارسال اس ام اس موجود نیست"
                    End Get
                End Property

            End Class

        End Namespace

    End Namespace

    Namespace RecivedSMSCodes
        Public MustInherit Class RecivedSMSCodes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property ActivateSMSOwner = 1
            Public Shared ReadOnly Property UNActivateSMSOwner = 2
        End Class

        Namespace Exceptions
            Public Class SMSRecivedCodeforSMSCodeNotFoundException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "کد ارسالی در اس ام اس دریافتی تعریف نشده است"
                    End Get
                End Property

            End Class

        End Namespace

        'BPTChanged
        Public Class R2CoreRecivedSMSCode
            Public Property SMSRCId As Int64
            Public Property SMSRCName As String
            Public Property SMSRCTitle As String
            Public Property SMSCode As String
            Public Property EndMinutes As Int64
            Public Property Core As String
            Public Property AssemblyDll As String
            Public Property AssemblyPath As String
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        'BPTChanged
        Public Class R2CoreRecivedSMSCodesManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetRecivedSMSCode(YourSMSCode As String) As R2CoreRecivedSMSCode
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSMSCode)

                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSRecivedCoding Where SMSCode='" & Mid(YourSMSCode.Trim, 1, 3) & "' and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New SMSRecivedCodeforSMSCodeNotFoundException
                    Return New R2CoreRecivedSMSCode With {.SMSRCId = DS.Tables(0).Rows(0).Item("SMSRCId"), .SMSRCName = DS.Tables(0).Rows(0).Item("SMSRCName").trim, .SMSRCTitle = DS.Tables(0).Rows(0).Item("SMSRCTitle").trim, .SMSCode = DS.Tables(0).Rows(0).Item("SMSCode").trim, .EndMinutes = DS.Tables(0).Rows(0).Item("EndMinutes"), .Core = DS.Tables(0).Rows(0).Item("Core").trim, .AssemblyDll = DS.Tables(0).Rows(0).Item("AssemblyDll").trim, .AssemblyPath = DS.Tables(0).Rows(0).Item("AssemblyPath").trim, .DateTimeMilladi = DS.Tables(0).Rows(0).Item("DateTimeMilladi"), .DateShamsi = DS.Tables(0).Rows(0).Item("DateShamsi"), .Time = DS.Tables(0).Rows(0).Item("Time"), .UserId = DS.Tables(0).Rows(0).Item("UserId"), .ViewFlag = DS.Tables(0).Rows(0).Item("ViewFlag"), .Active = DS.Tables(0).Rows(0).Item("Active"), .Deleted = DS.Tables(0).Rows(0).Item("Deleted")}
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As SMSRecivedCodeforSMSCodeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class



    End Namespace

    Namespace SMSHandling


        Public Class R2CoreStandardSMSStructure
            Public Sub New()
                MyBase.New()
                SmsId = Int64.MinValue
                MobileNumber = String.Empty
                Message = String.Empty
                EndMinutes = Int64.MinValue
                DateTimeMilladi = Now.Date
                Active = Boolean.FalseString
                DateShamsi = String.Empty
                Time = String.Empty
            End Sub

            Public Sub New(ByVal YourSmsId As Int64, ByVal YourMobileNumber As String, ByVal YourMessage As String, ByVal YourEndMinutes As Int64, ByVal YourDateTimeMilladi As DateTime, ByVal YourActive As Boolean, ByVal YourDateShamsi As String, ByVal YourTime As String)
                SmsId = YourSmsId
                MobileNumber = YourMobileNumber
                Message = YourMessage
                EndMinutes = YourEndMinutes
                DateTimeMilladi = YourDateTimeMilladi
                Active = YourActive
                DateShamsi = YourDateShamsi
                Time = YourTime
            End Sub

            Public Property SmsId As Int64
            Public Property MobileNumber As String
            Public Property Message As String
            Public Property EndMinutes As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property Active As Boolean
            Public Property DateShamsi As String
            Public Property Time As String
        End Class

        Public Structure SMSCreationData
            Public Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8, Data9, Data10 As String
        End Structure

        Public Class R2CoreSMSHandlingManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetSMSResultAnalyze(YourLst As List(Of KeyValuePair(Of Int64, String))) As String
                Try
                    Dim SB = New StringBuilder
                    Dim InstanceSendSMSCode = New R2CoreSendSMSCodeManager(_DateTimeService)
                    For LoopxLst As Int64 = 0 To YourLst.Count - 1
                        If YourLst(LoopxLst).Key = SendSMSCodes.R2CoreSendSMSCodes.Success Then
                            SB.Append(String.Empty)
                        Else
                            SB.AppendLine(YourLst(LoopxLst).Value + "  " + InstanceSendSMSCode.GetNSSSendSMSCode(YourLst(LoopxLst).Key).Description)
                        End If
                    Next
                    Return SB.ToString
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function SendSMS(YourSoftwareUsers As List(Of R2CoreStandardSoftwareUserStructure), YourSMSTypeId As Int64, YourSMSCreationData As List(Of SMSCreationData), YourChekDelayFromActivation As Boolean) As List(Of KeyValuePair(Of Int64, String))
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    'کنترل فعال بودن سرویس اس ام اس
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

                    Dim LstResult = New List(Of KeyValuePair(Of Int64, String))
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    'بررسی معادل بودن تعداد اعضاء لیست ها
                    If YourSoftwareUsers.Count <> YourSMSCreationData.Count Then Throw New CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    'بررسی پارامترهای ورودی
                    Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager(_DateTimeService)
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager(_DateTimeService)
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourSMSTypeId)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    'CmdSql.CommandText = "Select Top 1 SMSId from R2PrimarySMSSystem.dbo.TblSMSWareHouse with (tablockx) Order By SMSId Desc"
                    'CmdSql.ExecuteNonQuery()
                    Dim SMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    For Loopx As Int64 = 0 To YourSoftwareUsers.Count - 1
                        Try
                            SMSOwner = InstanceSMSOwners.GetNSSSMSOwner(YourSoftwareUsers(Loopx), False)
                        Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerNotExist, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        Catch ex As Exception
                            Throw ex
                        End Try
                        If YourSoftwareUsers(Loopx).MobileNumber.Trim.Length <> InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 7) Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InvalidMobileNumber, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If Not SMSOwner.IsSendingActive Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerIsSendingActiveIsfalse, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If NSSSMSType.Price <> 0 And SMSOwner.ReminderCharge <> 0 Then
                            If SMSOwner.ReminderCharge - SMSOwner.ReminderHolder <= NSSSMSType.Price Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InsufficientSMSMoneyWalletCharge, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        If YourChekDelayFromActivation Then
                            If DateDiff(DateInterval.Minute, SMSOwner.DateTimeMilladi, myCurrentDateTime.DateTimeMilladi) < InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 16) Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.DelayToStartSMSSending, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        Dim SMSContent = GetCompositedSMSCreationData(NSSSMSType, YourSMSCreationData(Loopx))
                        Dim SMS = New R2CoreStandardSMSStructure(Nothing, YourSoftwareUsers(Loopx).MobileNumber, SMSContent, NSSSMSType.SMSMinutes, Nothing, Nothing, Nothing, Nothing)
                        CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & SMS.MobileNumber & "','" & SMS.Message & "'," & SMS.EndMinutes & ",'" & myCurrentDateTime.DateTimeMilladi & "',1,'" & myCurrentDateTime.ShamsiDate & "'," & R2CoreSMSSendReciveType.ForSend & ")"
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set ReminderHolder=ReminderHolder+" & NSSSMSType.Price & "
                                              Where SMSOwnerUserId=" & YourSoftwareUsers(Loopx).UserId & " and IsSendingActive=1"
                        CmdSql.ExecuteNonQuery()
                        LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.Success, YourSoftwareUsers(Loopx).MobileNumber))
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return LstResult
                Catch ex As CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SMSTypeIdNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SmsSystemIsDisabledException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function SendSMS(YourNSSSMSTypeid As Int64, YourNSSUserTypeId As Int64, YourSMSCreationData As SMSCreationData) As List(Of KeyValuePair(Of Int64, String))
                Try
                    Dim InstanceSMSOwners = New R2Core.SMS.SMSOwners.R2CoreMClassSMSOwnersManager(_DateTimeService)
                    Dim LstSoftwareUsers = InstanceSMSOwners.GetNSSSoftwareUsers(YourNSSUserTypeId)
                    Return SendSMS(LstSoftwareUsers, YourNSSSMSTypeid, RepeatSMSCreationData(YourSMSCreationData, LstSoftwareUsers.Count), True)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Sub RecivedSMSHandling(YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                     "Select * from R2PrimarySMSSystem.dbo.TblSMSWareHouse as SMSWareHouse
                          Inner Join R2PrimarySMSSystem.DBO.TblSMSRecivedCoding as SMSRecivedCoding On Substring(SMSWareHouse.Message,1,3)=SMSRecivedCoding.SMSCode 
                      Where SMSWareHouse.Active=1 and SMSWareHouse.SmsType=" & R2CoreSMSSendReciveType.Recived & " and SMSRecivedCoding.Active=1 and SMSRecivedCoding.Deleted=0", 0, DS, New Boolean).GetRecordsCount = 0 Then Return
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim PathDll As String = Application.StartupPath + "\" + DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim
                        Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
                        Dim HandlerClassPath As String = DS.Tables(0).Rows(Loopx).Item("AssemblyPath").Trim()
                        Dim HandlerInstanceType = Assembly_.GetType(HandlerClassPath)
                        Dim HandlerObj = CType(Activator.CreateInstance(HandlerInstanceType), RecievedSMSHandler)
                        HandlerObj.MobileNumber = DS.Tables(0).Rows(Loopx).Item("MobileNumber")
                        HandlerObj.UserId = YourSoftwareUserId
                        HandlerObj.SMSContent = DS.Tables(0).Rows(Loopx).Item("Message").trim
                        Try
                            HandlerObj.Handling()
                        Catch ex As Exception
                        End Try
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0 Where SMSId=" & Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("SMSId")) & ""
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Sub SMSGarbage()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0
                                          Where Active=1 and DATEDIFF(MINUTE ,DateTimeMilladi,GETDATE()) > EndMinutes"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Function RepeatSMSCreationData(YourSMSCreationData As SMSCreationData, YourCount As Int64) As List(Of SMSCreationData)
                Try
                    Dim Lst = New List(Of SMSCreationData)
                    For Loopx As Int64 = 0 To YourCount - 1
                        Lst.Add(YourSMSCreationData)
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function GetCompositedSMSCreationData(YourNSSSMSType As R2CoreStandardSMSTypeStructure, YourData As SMSCreationData) As String
                Try
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager(_DateTimeService)
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourNSSSMSType.SMSTypeId)
                    Dim SMSContent = NSSSMSType.SMSTypeContent.Replace("Data1", YourData.Data1).Replace("Data2", YourData.Data2).Replace("Data3", YourData.Data3).Replace("Data4", YourData.Data4).Replace("Data5", YourData.Data5).Replace("Data6", YourData.Data6).Replace("Data7", YourData.Data7).Replace("Data8", YourData.Data8).Replace("Data9", YourData.Data9).Replace("Data10", YourData.Data10)
                    Return SMSContent
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function


        End Class

        Namespace Exceptions

            Public Class MobileNumberInvallidException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شماره موبایل " + _Message.Trim + " نادرست است"
                    End Get
                End Property

            End Class

            Public Class CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تعداد کاربران و تعداد داده ارسالی برای ارسال اس ام اس معادل نیستند"
                    End Get
                End Property
            End Class

            Public Class SMSResultException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return _Message.Trim
                    End Get
                End Property

            End Class


        End Namespace

        'BPTChanged
        Public MustInherit Class RecievedSMSHandler
            Public Event HandlingEvent()
            Protected _DateTimeService As R2DateTimeService

            Public Sub New()
                Try
                    _DateTimeService = New R2DateTimeService
                Catch ex As FileNotExistException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Protected _MobileNumber As String = String.Empty
            Public Property MobileNumber As String
                Get
                    Return _MobileNumber
                End Get
                Set(value As String)
                    _MobileNumber = value
                End Set
            End Property

            Protected _SMSContent As String = String.Empty
            Public Property SMSContent As String
                Get
                    Return _SMSContent
                End Get
                Set(value As String)
                    _SMSContent = value
                End Set
            End Property

            Protected _UserId As Int64 = Nothing
            Public Property UserId As Int64
                Get
                    Return _UserId
                End Get
                Set(value As Int64)
                    _UserId = value
                End Set
            End Property

            Public Sub Handling()
                RaiseEvent HandlingEvent()
            End Sub

        End Class

        'BPTChanged
        Public Class R2CoreSMSHandlerManager

            Private _DateTimeService As IR2DateTimeService
            Private _SoftwareUserService As ISoftwareUserService
            Private _InstanceSqlDataBOX As R2CoreSqlDataBOXManager

            Public Sub New(YourR2DateTimeService As IR2DateTimeService, YourSoftwareUserService As ISoftwareUserService)
                _DateTimeService = YourR2DateTimeService
                _SoftwareUserService = YourSoftwareUserService
                _InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetSMSResultAnalyze(YourLst As List(Of KeyValuePair(Of Int64, String))) As String
                Try
                    Dim SB = New StringBuilder
                    Dim InstanceSendSMSCode = New R2CoreSendSMSCodeManager(_DateTimeService)
                    For LoopxLst As Int64 = 0 To YourLst.Count - 1
                        If YourLst(LoopxLst).Key = SendSMSCodes.R2CoreSendSMSCodes.Success Then
                            SB.Append(String.Empty)
                        Else
                            SB.AppendLine(YourLst(LoopxLst).Value + "  " + InstanceSendSMSCode.GetNSSSendSMSCode(YourLst(LoopxLst).Key).Description)
                        End If
                    Next
                    Return SB.ToString
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function SendSMS(YourSoftwareUsers As List(Of R2CoreSoftwareUser), YourSMSTypeId As Int64, YourSMSCreationData As List(Of SMSCreationData), YourChekDelayFromActivation As Boolean) As List(Of KeyValuePair(Of Int64, String))
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    'کنترل فعال بودن سرویس اس ام اس
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

                    Dim LstResult = New List(Of KeyValuePair(Of Int64, String))
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    'بررسی معادل بودن تعداد اعضاء لیست ها
                    If YourSoftwareUsers.Count <> YourSMSCreationData.Count Then Throw New CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    'بررسی پارامترهای ورودی
                    Dim InstanceSMSOwners = New R2CoreSMSOwnersManager(_DateTimeService, _SoftwareUserService)
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager(_DateTimeService)
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourSMSTypeId)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    'CmdSql.CommandText = "Select Top 1 SMSId from R2PrimarySMSSystem.dbo.TblSMSWareHouse with (tablockx) Order By SMSId Desc"
                    'CmdSql.ExecuteNonQuery()
                    Dim SMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    For Loopx As Int64 = 0 To YourSoftwareUsers.Count - 1
                        Try
                            SMSOwner = InstanceSMSOwners.GetSMSOwner(YourSoftwareUsers(Loopx), False)
                        Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerNotExist, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        Catch ex As Exception
                            Throw ex
                        End Try
                        If YourSoftwareUsers(Loopx).MobileNumber.Trim.Length <> InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 7) Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InvalidMobileNumber, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If Not SMSOwner.IsSendingActive Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerIsSendingActiveIsfalse, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If NSSSMSType.Price <> 0 And SMSOwner.ReminderCharge <> 0 Then
                            If SMSOwner.ReminderCharge - SMSOwner.ReminderHolder <= NSSSMSType.Price Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InsufficientSMSMoneyWalletCharge, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        If YourChekDelayFromActivation Then
                            If DateDiff(DateInterval.Minute, SMSOwner.DateTimeMilladi, myCurrentDateTime.DateTimeMilladi) < InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 16) Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.DelayToStartSMSSending, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        Dim SMSContent = GetCompositedSMSCreationData(NSSSMSType, YourSMSCreationData(Loopx))
                        Dim SMS = New R2CoreStandardSMSStructure(Nothing, YourSoftwareUsers(Loopx).MobileNumber, SMSContent, NSSSMSType.SMSMinutes, Nothing, Nothing, Nothing, Nothing)
                        CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & SMS.MobileNumber & "','" & SMS.Message & "'," & SMS.EndMinutes & ",convert(varchar, getdate(), 20),1, R2Primary.DBO.BPTCOGregorianToPersian(GETDATE())," & R2CoreSMSSendReciveType.ForSend & ")"
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set ReminderHolder=ReminderHolder+" & NSSSMSType.Price & "
                                              Where SMSOwnerUserId=" & YourSoftwareUsers(Loopx).UserId & " and IsSendingActive=1"
                        CmdSql.ExecuteNonQuery()
                        LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.Success, YourSoftwareUsers(Loopx).MobileNumber))
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return LstResult
                Catch ex As CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SMSTypeIdNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SmsSystemIsDisabledException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function GetCompositedSMSCreationData(YourNSSSMSType As R2CoreStandardSMSTypeStructure, YourData As SMSCreationData) As String
                Try
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager(_DateTimeService)
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourNSSSMSType.SMSTypeId)
                    Dim SMSContent = NSSSMSType.SMSTypeContent.Replace("Data1", YourData.Data1).Replace("Data2", YourData.Data2).Replace("Data3", YourData.Data3).Replace("Data4", YourData.Data4).Replace("Data5", YourData.Data5).Replace("Data6", YourData.Data6).Replace("Data7", YourData.Data7).Replace("Data8", YourData.Data8).Replace("Data9", YourData.Data9).Replace("Data10", YourData.Data10)
                    Return SMSContent
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Sub SendSMSFree(YourMobileNumber As String, YourSMSCreationData As SMSCreationData, YourSMSTypes As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager(_DateTimeService)
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourSMSTypes)
                    Dim SMSContent = GetCompositedSMSCreationData(NSSSMSType, YourSMSCreationData)
                    Dim SMS = New R2CoreStandardSMSStructure(Nothing, YourMobileNumber, SMSContent, NSSSMSType.SMSMinutes, Nothing, Nothing, Nothing, Nothing)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & SMS.MobileNumber & "','" & SMS.Message & "'," & SMS.EndMinutes & ",'" & myCurrentDateTime.DateTimeMilladi & "',1,'" & myCurrentDateTime.ShamsiDate & "'," & R2CoreSMSSendReciveType.ForSend & ")"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As SMSTypeIdNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Sub SMSGarbageCollector()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0
                                          Where Active=1 and DATEDIFF(MINUTE ,DateTimeMilladi,'" & _DateTimeService.GetCurrentDateTimeMilladi.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "') > EndMinutes"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Sub RecivedSMSHandling(YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim DS As New DataSet
                    If _InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                     "Select * from R2PrimarySMSSystem.dbo.TblSMSWareHouse as SMSWareHouse
                          Inner Join R2PrimarySMSSystem.DBO.TblSMSRecivedCoding as SMSRecivedCoding On Substring(SMSWareHouse.Message,1,3)=SMSRecivedCoding.SMSCode 
                      Where SMSWareHouse.Active=1 and SMSWareHouse.SmsType=" & R2CoreSMSSendReciveType.Recived & " and SMSRecivedCoding.Active=1 and SMSRecivedCoding.Deleted=0", 120, DS, New Boolean).GetRecordsCount = 0 Then Return
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim PathDll As String = Application.StartupPath + "\" + DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim
                        Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
                        Dim HandlerClassPath As String = DS.Tables(0).Rows(Loopx).Item("AssemblyPath").Trim()
                        Dim HandlerInstanceType = Assembly_.GetType(HandlerClassPath)
                        Dim HandlerObj = CType(Activator.CreateInstance(HandlerInstanceType), RecievedSMSHandler)
                        HandlerObj.MobileNumber = DS.Tables(0).Rows(Loopx).Item("MobileNumber")
                        HandlerObj.UserId = YourSoftwareUserId
                        HandlerObj.SMSContent = DS.Tables(0).Rows(Loopx).Item("Message").trim
                        Try
                            HandlerObj.Handling()
                        Catch ex As Exception
                        End Try
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0 Where SMSId=" & Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("SMSId")) & ""
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Function RepeatSMSCreationData(YourSMSCreationData As SMSCreationData, YourCount As Int64) As List(Of SMSCreationData)
                Try
                    Dim Lst = New List(Of SMSCreationData)
                    For Loopx As Int64 = 0 To YourCount - 1
                        Lst.Add(YourSMSCreationData)
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class

    End Namespace

    Namespace SMSSendRecive

        Public Enum R2CoreSMSSendReciveType
            None = 0
            ForSend = 1
            Recived = 2
        End Enum

        'Public Class R2CoreSMSSendingManager
        '    Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
        '    Private _DateTime As New R2DateTime

        '    Public Sub Sending(YourNSSUser As R2CoreStandardSoftwareUserStructure)
        '        Dim CmdSql As New SqlClient.SqlCommand
        '        CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
        '        Try
        '            Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
        '            If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
        '            'Dim InstanceLogging = New R2CoreInstanceLoggingManager

        '            'ایجاد لیست اس ام اس ها
        '            Dim DS As DataSet
        '            Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
        '            If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
        '                 "Select * from R2PrimarySMSSystem.dbo.TblSmsWareHouse where Active=1 and SmsType=" & R2CoreSMSSendReciveType.ForSend & " and DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=EndMinutes and Message<>''", 0, DS, New Boolean).GetRecordsCount = 0 Then Return
        '            Dim LstMessage() As String = New String(DS.Tables(0).Rows.Count - 1) {}
        '            Dim LstMobileNumber() As String = New String(DS.Tables(0).Rows.Count - 1) {}
        '            Dim LstSMSId() As Int64 = New Int64(DS.Tables(0).Rows.Count - 1) {}
        '            For LoopSMSs As Int64 = 0 To DS.Tables(0).Rows.Count - 1
        '                LstMessage(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("Message").trim
        '                LstMobileNumber(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("MobileNumber").trim
        '                LstSMSId(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("SMSId")
        '            Next

        '            'ارسال اس ام اس ها
        '            Dim SentSmsStatus() As Long = _SepahanSMS.SendSms(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), LstMessage, LstMobileNumber, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4),
        '                                          net.sepahansms.SendType.DynamicText, net.sepahansms.SmsMode.SaveInPhone)

        '            'غیرفعال کردن اس ام اس های ارسال شده و بدون خطا
        '            CmdSql.Connection.Open()
        '            CmdSql.Transaction = CmdSql.Connection.BeginTransaction
        '            For LoopSMSs As Int64 = 0 To SentSmsStatus.Count - 1
        '                Dim mySMSId = LstSMSId(LoopSMSs)
        '                If SentSmsStatus(LoopSMSs) > 0 Then
        '                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSmsWareHouse Set Active=0 where SmsId=" & mySMSId & ""
        '                    CmdSql.ExecuteNonQuery()
        '                Else
        '                    'InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.SendSMSResult, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), LstSMSId(LoopSMSs).ToString(), SentSmsStatus(LoopSMSs).ToString(), String.Empty, String.Empty, String.Empty, YourNSSUser.UserId, Nothing, Nothing))
        '                End If
        '            Next
        '            CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

        '        Catch ex As SmsSystemIsDisabledException
        '            If CmdSql.Connection.State <> ConnectionState.Closed Then
        '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '            End If
        '            Throw ex
        '        Catch ex As Exception
        '            If CmdSql.Connection.State <> ConnectionState.Closed Then
        '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '            End If
        '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '        End Try
        '    End Sub

        'End Class

        'Public Class R2CoreSMSRecivingManager
        '    Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
        '    Private _DateTimeService As IR2DateTimeService
        '    Public Sub New(YourDateTimeService As IR2DateTimeService)
        '        _DateTimeService = YourDateTimeService
        '    End Sub

        '    Public Sub Reciving()
        '        Dim CmdSql As New SqlClient.SqlCommand
        '        CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
        '        Try

        '            Dim CurrentDateTime = _DateTimeService.GetCurrentDateAndTime

        '            'دریافتی
        '            Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
        '            If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

        '            Dim ReceiveSmsOneLine = _SepahanSMS.GetReceiveSMSWithNumber(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), net.sepahansms.ReceiveType.UnRead, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4), 10, "", "")
        '            If ReceiveSmsOneLine.Count = 0 Then Exit Sub

        '            CmdSql.Connection.Open()
        '            CmdSql.Transaction = CmdSql.Connection.BeginTransaction
        '            CmdSql.CommandText = "Select Top 1 SMSId from R2PrimarySMSSystem.dbo.TblSMSWareHouse with (tablockx) Order By SMSId Desc"
        '            CmdSql.ExecuteNonQuery()
        '            For loopx As Int32 = 0 To ReceiveSmsOneLine.Count - 1
        '                Dim SmsText As String = ReceiveSmsOneLine(loopx).SmsText
        '                Dim MobileNumber As String = ReceiveSmsOneLine(loopx).FromNumber
        '                Dim myDate As String = ReceiveSmsOneLine(loopx).Date
        '                Try
        '                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
        '                    InstanceSQLInjectionPrevention.GeneralAuthorization(SmsText)
        '                    InstanceSQLInjectionPrevention.GeneralAuthorization(MobileNumber)
        '                    InstanceSQLInjectionPrevention.GeneralAuthorization(myDate)
        '                Catch ex As SqlInjectionException
        '                    Continue For
        '                Catch ex As Exception
        '                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '                End Try
        '                Dim InstanceRecivedSMSCodes = New R2CoreRecivedSMSCodesManager(_DateTimeService)
        '                Dim NSSRecivedSMSCode As R2CoreStandardRecivedSMSCodeStructure = Nothing
        '                Try
        '                    NSSRecivedSMSCode = InstanceRecivedSMSCodes.GetNSSRecivedSMSCode(SmsText)
        '                Catch ex As SMSRecivedCodeforSMSCodeNotFoundException
        '                    Continue For
        '                Catch ex As Exception
        '                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '                End Try
        '                CmdSql.CommandText = "Insert into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & MobileNumber & "','" & SmsText & "'," & NSSRecivedSMSCode.EndMinutes & ",'" & CurrentDateTime.DateTimeMilladi & "',1,'" & CurrentDateTime.ShamsiDate & "'," & R2CoreSMSSendReciveType.Recived & ")"
        '                CmdSql.ExecuteNonQuery()
        '            Next
        '            CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

        '        Catch ex As SmsSystemIsDisabledException
        '            If CmdSql.Connection.State <> ConnectionState.Closed Then
        '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '            End If
        '            Throw ex
        '        Catch ex As Exception
        '            If CmdSql.Connection.State <> ConnectionState.Closed Then
        '                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
        '            End If
        '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '        End Try

        '    End Sub

        'End Class

        Namespace Exceptions
            Public Class SmsMessageEmptyException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "در پیام ارسالی متنی وجود ندارد"
                    End Get
                End Property
            End Class

        End Namespace

        'BPTChanged
        Public Class R2CoreSMSSendingManager
            Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService

            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Sub Sending()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

                    'ایجاد لیست اس ام اس ها
                    Dim DS As DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                         "Select * from R2PrimarySMSSystem.dbo.TblSmsWareHouse where Active=1 and SmsType=" & R2CoreSMSSendReciveType.ForSend & " and DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=EndMinutes and Message<>''", 0, DS, New Boolean).GetRecordsCount = 0 Then Return
                    Dim LstMessage() As String = New String(DS.Tables(0).Rows.Count - 1) {}
                    Dim LstMobileNumber() As String = New String(DS.Tables(0).Rows.Count - 1) {}
                    Dim LstSMSId() As Int64 = New Int64(DS.Tables(0).Rows.Count - 1) {}
                    For LoopSMSs As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstMessage(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("Message").trim
                        LstMobileNumber(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("MobileNumber").trim
                        LstSMSId(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("SMSId")
                    Next

                    'ارسال اس ام اس ها
                    Dim SentSmsStatus() As Long = _SepahanSMS.SendSms(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), LstMessage, LstMobileNumber, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4),
                                                  net.sepahansms.SendType.DynamicText, net.sepahansms.SmsMode.SaveInPhone)

                    'غیرفعال کردن اس ام اس های ارسال شده و بدون خطا
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For LoopSMSs As Int64 = 0 To SentSmsStatus.Count - 1
                        Dim SMSId = LstSMSId(LoopSMSs)
                        If SentSmsStatus(LoopSMSs) > 0 Then
                            CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSmsWareHouse Set Active=0 where SmsId=" & SMSId & ""
                            CmdSql.ExecuteNonQuery()
                        Else
                            R2CoreLoggingManager.RegisterLog(R2Core.PubSubMessaging.R2CorePubSubChannels.Logging, New Exception((New SendingSMSFailedException(SMSId)).Message))
                        End If
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SmsSystemIsDisabledException
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
        Public Class R2CoreSMSRecivingManager
            Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService

            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Sub Reciving()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    'دریافتی
                    Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

                    Dim ReceiveSmsOneLine = _SepahanSMS.GetReceiveSMSWithNumber(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), net.sepahansms.ReceiveType.UnRead, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4), 10, "", "")
                    If ReceiveSmsOneLine.Count = 0 Then Exit Sub

                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For loopx As Int32 = 0 To ReceiveSmsOneLine.Count - 1
                        Dim SmsText As String = ReceiveSmsOneLine(loopx).SmsText
                        Dim MobileNumber As String = ReceiveSmsOneLine(loopx).FromNumber
                        Dim myDate As String = ReceiveSmsOneLine(loopx).Date
                        Try
                            Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                            InstanceSQLInjectionPrevention.GeneralAuthorization(SmsText)
                            InstanceSQLInjectionPrevention.GeneralAuthorization(MobileNumber)
                            InstanceSQLInjectionPrevention.GeneralAuthorization(myDate)
                        Catch ex As SqlInjectionException
                            R2CoreLoggingManager.RegisterLog(R2Core.PubSubMessaging.R2CorePubSubChannels.Logging, New Exception((New SqlInjectionException()).Message))
                            Continue For
                        End Try
                        Dim InstanceRecivedSMSCodes = New R2CoreRecivedSMSCodesManager(_DateTimeService)
                        Dim NSSRecivedSMSCode As R2CoreRecivedSMSCode = Nothing
                        Try
                            NSSRecivedSMSCode = InstanceRecivedSMSCodes.GetRecivedSMSCode(SmsText)
                        Catch ex As SqlInjectionException
                            R2CoreLoggingManager.RegisterLog(R2Core.PubSubMessaging.R2CorePubSubChannels.Logging, New Exception((New SqlInjectionException()).Message))
                            Continue For
                        Catch ex As SMSRecivedCodeforSMSCodeNotFoundException
                            Continue For
                        End Try
                        CmdSql.CommandText = "Insert into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & MobileNumber & "','" & SmsText & "'," & NSSRecivedSMSCode.EndMinutes & ",convert(varchar, getdate(), 20),1,R2Primary.DBO.BPTCOGregorianToPersian(GETDATE())," & R2CoreSMSSendReciveType.Recived & ")"
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As SmsSystemIsDisabledException
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

    End Namespace

    Namespace SMSTypes

        Public MustInherit Class R2CoreSMSTypes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property SoftwareUserSecurity = 2
            Public Shared ReadOnly Property ApplicationActivationCode = 3
            Public Shared ReadOnly Property SMSControllingMoneyWallet = 7
            Public Shared ReadOnly Property PleaseCharge = 8
            Public Shared ReadOnly Property ATISMobileAppDownloadLink = 14
            Public Shared ReadOnly Property ActivateSMSOwnerSuccess = 16
            Public Shared ReadOnly Property SoftwareUserSecurityFree = 21
        End Class

        Public Class R2CoreStandardSMSTypeStructure
            Inherits R2StandardStructure

            Public Sub New()
                MyBase.New()
                _SMSTypeId = Int64.MinValue
                _SMSTypeName = String.Empty
                _SMSTypeTitle = String.Empty
                _SMSTypeColor = Color.Black
                _Price = Int64.MinValue
                _SMSTypeContent = String.Empty
                _SMSMinutes = Int64.MinValue
                _Core = String.Empty
                _UserId = Int64.MinValue
                _DateTimeMilladi = DateAndTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSTypeId As Int64, YourSMSTypeName As String, YourSMSTypeTitle As String, YourSMSTypeColor As Color, YourPrice As Int64, YourSMSTypeContent As String, YourSMSMinutes As Int64, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                MyBase.New(YourSMSTypeId, YourSMSTypeTitle)
                _SMSTypeId = YourSMSTypeId
                _SMSTypeName = YourSMSTypeName
                _SMSTypeTitle = YourSMSTypeTitle
                _SMSTypeColor = YourSMSTypeColor
                _Price = YourPrice
                _SMSTypeContent = YourSMSTypeContent
                _SMSMinutes = YourSMSMinutes
                _Core = YourCore
                _UserId = YourUserId
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSTypeId As Int64
            Public Property SMSTypeName As String
            Public Property SMSTypeTitle As String
            Public Property SMSTypeColor As Color
            Public Property Price As Int64
            Public Property SMSTypeContent As String
            Public Property SMSMinutes As Int64
            Public Property Core As String
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreMClassSMSTypesManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetNSSSMSType(YourSMSTypeId As Int64) As R2CoreStandardSMSTypeStructure
                Try
                    Dim DS As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                            "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSTypes Where SMSTypeId=" & YourSMSTypeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New SMSTypeIdNotFoundException
                    Return New R2CoreStandardSMSTypeStructure(YourSMSTypeId, DS.Tables(0).Rows(0).Item("SMSTypeName").trim, DS.Tables(0).Rows(0).Item("SMSTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSTypeColor").trim), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("SMSTypeContent").trim, DS.Tables(0).Rows(0).Item("SMSMinutes"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SMSTypeIdNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreStandardSMSTypeStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Lst As New List(Of R2CoreStandardSMSTypeStructure)
                    Dim DS As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                       "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes Where SMSTypeTitle Like '%" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "%' 
                        and ViewFlag=1 Order By SMSTypeTitle", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(DS.Tables(0).Rows(Loopx).Item("SMSTypeId"), DS.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim, DS.Tables(0).Rows(Loopx).Item("Price"), DS.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, DS.Tables(0).Rows(Loopx).Item("SMSMinutes"), DS.Tables(0).Rows(Loopx).Item("Core").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreStandardSMSTypeStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Lst As New List(Of R2CoreStandardSMSTypeStructure)
                    Dim DS As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                             "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes 
                              Where Left(SMSTypeTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' Order By SMSTypeTitle", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(DS.Tables(0).Rows(Loopx).Item("SMSTypeId"), DS.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim, DS.Tables(0).Rows(Loopx).Item("Price"), DS.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, DS.Tables(0).Rows(Loopx).Item("SMSMinutes"), DS.Tables(0).Rows(Loopx).Item("Core").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes(YourSearchString As String) As List(Of R2StandardStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Ds As DataSet
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes Where Active=1 And ViewFlag=1 And SMSTypeTitle Like  '%" & YourSearchString & "%' Order By SMSTypeTitle ", 3600, Ds, New Boolean)
                    Dim Lst As New List(Of R2StandardStructure)
                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(Ds.Tables(0).Rows(Loopx).Item("SMSTypeId"), Ds.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, Ds.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, Color.FromName(Ds.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim), Ds.Tables(0).Rows(Loopx).Item("Price"), Ds.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, Ds.Tables(0).Rows(Loopx).Item("SMSMinutes"), Ds.Tables(0).Rows(Loopx).Item("Core").trim, Ds.Tables(0).Rows(Loopx).Item("UserId"), Ds.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), Ds.Tables(0).Rows(Loopx).Item("DateShamsi"), Ds.Tables(0).Rows(Loopx).Item("Time"), Ds.Tables(0).Rows(Loopx).Item("ViewFlag"), Ds.Tables(0).Rows(Loopx).Item("Active"), Ds.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SMSTypeIdNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شماره شاخص نوع اس ام اس نادرست است"
                    End Get
                End Property
            End Class

        End Namespace

    End Namespace

    Namespace SMSOwners

        Public Class R2CoreStandardSMSOwnerStructure

            Public Sub New()
                MyBase.New()
                _SMSOwnerUserId = Int64.MinValue
                _SMSOTypeId = Int64.MinValue
                _ReminderCharge = Int64.MinValue
                _ReminderHolder = Int64.MinValue
                _IsSendingActive = Boolean.FalseString
                _PleaseCharge = Boolean.FalseString
                _DateTimeMilladi = DateTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSOwnerUserId As Int64, YourSMSOTypeId As Int64, YourReminderCharge As Int64, YourReminderHolder As Int64, YourIsSendingActive As Boolean, YourPleaseCharge As Boolean, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SMSOwnerUserId = YourSMSOwnerUserId
                _SMSOTypeId = YourSMSOTypeId
                _ReminderCharge = YourReminderCharge
                _ReminderHolder = YourReminderHolder
                _IsSendingActive = YourIsSendingActive
                _PleaseCharge = YourPleaseCharge
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSOwnerUserId As Int64
            Public Property SMSOTypeId As Int64
            Public Property ReminderCharge As Int64
            Public Property ReminderHolder As Int64
            Public Property IsSendingActive As Boolean
            Public Property PleaseCharge As Boolean
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreStandardSMSOwnerTypeStructure

            Public Sub New()
                MyBase.New()
                _SMSOTypeId = Int64.MinValue
                _SMSOTypeName = String.Empty
                _SMSOTypeTitle = String.Empty
                _SMSOTypeColor = Color.Black
                _Price = Int64.MinValue
                _PriceMinusCommission = Int64.MinValue
                _PriceMinusCommissionMinusVAT = Int64.MinValue
                _Core = String.Empty
                _UserId = Int64.MinValue
                _DateTimeMilladi = DateTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSOTypeId As Int64, YourSMSOTypeName As String, YourSMSOTypeTitle As String, YourSMSOTypeColor As Color, YourPrice As Int64, YourPriceMinusCommission As Int64, YourPriceMinusCommissionMinusVAT As Int64, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SMSOTypeId = YourSMSOTypeId
                _SMSOTypeName = YourSMSOTypeName
                _SMSOTypeTitle = YourSMSOTypeTitle
                _SMSOTypeColor = YourSMSOTypeColor
                _Price = YourPrice
                _PriceMinusCommission = YourPriceMinusCommission
                _PriceMinusCommissionMinusVAT = YourPriceMinusCommissionMinusVAT
                _Core = YourCore
                _UserId = YourUserId
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSOTypeId As Int64
            Public Property SMSOTypeName As String
            Public Property SMSOTypeTitle As String
            Public Property SMSOTypeColor As Color
            Public Property Price As Int64
            Public Property PriceMinusCommission As Int64
            Public Property PriceMinusCommissionMinusVAT As Int64
            Public Property Core As String
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreMClassSMSOwnerTypesManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetNSSSMSOwnerType(YourSMSOwnerTypeId As Int64) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select * from R2PrimarySMSSystem.dbo.TblSMSOwnerTypes Where SMSOTypeId=" & YourSMSOwnerTypeId & " and Active=1 and Deleted=0", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeNotFoundException
                    End If
                Catch ex As SMSOwnerTypeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSMSOwnerTypeBySoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                          "Select * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                            Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On SoftwareUsers.UserTypeId=SoftwareUserTypes.UTId 
                            Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUserTypes.UTId=EntityRelations.E1 
                            Inner Join R2PrimarySMSSystem.dbo.TblSMSOwnerTypes as SMSOwnerTypes On EntityRelations.E2=SMSOwnerTypes.SMSOTypeId 
                           Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUserTypes.Active=1 and SoftwareUserTypes.Deleted=0 and EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUserType_SMSOwnerType & " and SMSOwnerTypes.Active=1", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeBySoftwareUserNotFoundException
                    End If
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Public Class R2CoreMClassSMSOwnersManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Sub RegisteringSMSOwner(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set IsSendingActive=0 Where SMSOwnerUserId=" & YourNSSSMSOwner.SMSOwnerUserId & " and IsSendingActive=1"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSOwners(SMSOwnerUserId,SMSOTypeId,ReminderCharge,ReminderHolder,IsSendingActive,PleaseCharge,DateTimeMilladi,DateShamsi,Time,UserId,ViewFlag,Active,Deleted)
                                          Values(" & YourNSSSMSOwner.SMSOwnerUserId & "," & YourNSSSMSOwner.SMSOTypeId & "," & YourNSSSMSOwner.ReminderCharge & "," & YourNSSSMSOwner.ReminderHolder & ",1,0,'" & myCurrentDateTime.DateTimeMilladi & "','" & myCurrentDateTime.ShamsiDate & "','" & myCurrentDateTime.Time & "'," & YourNSSUser.UserId & ",1,1,0)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Structure SMSOwnerCurrentState
                Public IsSendingActive As Boolean
                Public TextToView As String
                Public TextToViewColor As String
            End Structure
            Public Function GetNSSSMSOwner(YourSoftwareUser As R2CoreStandardSoftwareUserStructure, YourImmediately As Boolean) As R2CoreStandardSMSOwnerStructure
                Try
                    Dim DS As New DataSet
                    Dim Da As New SqlClient.SqlDataAdapter
                    If YourImmediately Then
                        Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc")
                        Da.SelectCommand.Connection = R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection
                        If Da.Fill(DS) <> 0 Then Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 60, DS, New Boolean).GetRecordsCount <> 0 Then
                            Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Else
                            Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                        End If
                    End If
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSMSOwnerCurrentState(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As SMSOwnerCurrentState
                Try
                    Dim NSSSMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Dim TextToView = String.Empty
                    Dim TextToViewColor = String.Empty
                    Try
                        NSSSMSOwner = GetNSSSMSOwner(YourNSSSoftwareUser, True)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        TextToView = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14)
                        TextToViewColor = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12)
                        Return New SMSOwnerCurrentState With {.IsSendingActive = False, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                    Catch ex As Exception
                        Throw ex
                    End Try
                    TextToView = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 15), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14))
                    TextToViewColor = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 13), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12))
                    Return New SMSOwnerCurrentState With {.IsSendingActive = NSSSMSOwner.IsSendingActive, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSMSOwnerCurrentState(YourSoftwareUserId As Int64) As SMSOwnerCurrentState
                Try
                    Dim NSS As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim InstanceSoftwareUser = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    Return GetNSSSMSOwnerCurrentState(InstanceSoftwareUser.GetNSSUser(YourSoftwareUserId))
                Catch ex As UserIdNotExistException
                    Throw ex
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsActiveSystemAccountingforSendSMS() As Boolean
                Try
                    'کنترل کسر هزینه فعال است یا نه
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 10) Then Return False
                    Return True
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSoftwareUsers(YourNSSUserTypeId As Int64) As List(Of R2CoreStandardSoftwareUserStructure)
                Try
                    Dim LstUsers As New List(Of R2CoreStandardSoftwareUserStructure)
                    Dim DS As New DataSet
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                  "Select Distinct SoftwareUsers.* from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners 
                     Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId =SoftwareUsers.UserId 
                   Where SoftwareUsers.UserTypeId=" & YourNSSUserTypeId & " and SMSOwners.IsSendingActive=1 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 ", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode"), DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce"), DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce"), DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha"), DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return LstUsers
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerReminderCreditPercent(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure) As Int64
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Return InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) * YourNSSSMSOwner.ReminderCharge / 100
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared _SendSMSOwnersPleaseChargeFlag As Boolean = False
            Public Sub SendSMSOwnersPleaseCharge()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Dim InstancePersianCallendar = New R2CoreInstanceDateAndTimePersianCalendarManager(_DateTimeService)
                    'کنترل زمان اجرای فرآیند بر اساس کانفیگ
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    Dim TimeOfDay = _DateTimeService.GetTickofTime(myCurrentDateTime.DateTimeMilladi)
                    Dim StTime = TimeSpan.Parse("00:00:00").Ticks
                    Dim EndTime = TimeSpan.Parse("00:05:00").Ticks
                    Dim ConfigTime = TimeSpan.Parse(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 9)).Ticks
                    If TimeOfDay >= StTime And TimeOfDay <= EndTime Then
                        _SendSMSOwnersPleaseChargeFlag = False
                        Return
                    ElseIf TimeOfDay <= ConfigTime Then
                        Return
                    Else
                    End If
                    'این فرآیند در روز فقط باید یکبار اجرا گردد و نه بیشتر
                    'خط کد زیر یعنی فرآیند امروز قبلا در بازه معین اجرا یکبار اجرا شده است
                    If _SendSMSOwnersPleaseChargeFlag Then Return
                    'لیست مالکان اس ام اس که برایشان قبلا اس ام اس پلیز شارژ نرفته است و باید برود
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                           "Select * from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners
                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId=SoftwareUsers.UserId 
                            Where SMSOwners.IsSendingActive=1 and SMSOwners.ReminderCharge<>0 and ((SMSOwners.ReminderCharge-SMSOwners.ReminderHolder)<=(SMSOwners.ReminderCharge * " & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) & " / 100))
                                  and SMSOwners.PleaseCharge=0 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        _SendSMSOwnersPleaseChargeFlag = True
                        Return
                    End If

                    'تغییر فیلد پلیز شارژ به ارسال شده
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim UserId As Int64 = DS.Tables(0).Rows(Loopx).Item("SMSOwnerUserId")
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set PleaseCharge=1 Where SMSOwnerUserId=" & UserId & " and IsSendingActive=1 and PleaseCharge=0"
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    _SendSMSOwnersPleaseChargeFlag = True

                    'ارسال پیام به مالکان اس ام اس
                    Dim LstUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode").trim, DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce").trim, DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce").trim, DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha").trim, DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_DateTimeService)
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstUsers, R2CoreSMSTypes.PleaseCharge, InstanceSMSHandling.RepeatSMSCreationData(New SMSCreationData With {.Data1 = String.Empty}, LstUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SMSOwnerSendingPleaseChargeMessageFailedException(SMSResultAnalyze)
                Catch ex As SMSOwnerSendingPleaseChargeMessageFailedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

        End Class

        Namespace Exceptions

            Public Class SMSOwnerChargeMessageException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "سرویس اس ام اس شارژ ندارد" + vbCrLf + "لطفا سرویس اس ام اس را شارژ نمایید"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerForSoftwareUserDoNotRegisteredException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "فعال سازی اس ام اس برای کاربر مورد انجام نشده است"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerHasCreditYetException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "سرویس اس ام اس کاربر قبلا فعال شده است و هنوز معتبر است"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerTypeNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "نوع مالک اس ام اس با شاخص مورد نظر یافت نشد"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerTypeBySoftwareUserNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "نوع مالک اس ام اس مبنی بر نوع کاربرارسالی با شاخص مورد نظر یافت نشد"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerIsSendActiveIsFalseException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "وضعیت ارسال اس ام اس برای مالک آن " + _Message.Trim + "  غیر فعال است"
                    End Get
                End Property

            End Class

            Public Class SMSOwnerSendingPleaseChargeMessageFailedException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "ارسال اس ام اس لطفا شارژ کنید برای مالک آن با مشکل مواجه شد " + vbCrLf + _Message.Trim
                    End Get
                End Property

            End Class

        End Namespace

        'BPTChanged
        Public Class R2CoreSMSOwnersManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Private _SoftwareUserService As ISoftwareUserService
            Public Sub New(YourR2DateTimeService As IR2DateTimeService, YourSoftwareUserService As ISoftwareUserService)
                _SoftwareUserService = YourSoftwareUserService
                _DateTimeService = YourR2DateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Sub RegisteringSMSOwner(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim myCurrentDateTime = _DateTimeService.GetCurrentDateAndTime
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set IsSendingActive=0 Where SMSOwnerUserId=" & YourNSSSMSOwner.SMSOwnerUserId & " and IsSendingActive=1"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSOwners(SMSOwnerUserId,SMSOTypeId,ReminderCharge,ReminderHolder,IsSendingActive,PleaseCharge,DateTimeMilladi,DateShamsi,Time,UserId,ViewFlag,Active,Deleted)
                                          Values(" & YourNSSSMSOwner.SMSOwnerUserId & "," & YourNSSSMSOwner.SMSOTypeId & "," & YourNSSSMSOwner.ReminderCharge & "," & YourNSSSMSOwner.ReminderHolder & ",1,0,'" & myCurrentDateTime.DateTimeMilladi & "','" & myCurrentDateTime.ShamsiDate & "','" & myCurrentDateTime.Time & "'," & _SoftwareUserService.UserId & ",1,1,0)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Structure SMSOwnerCurrentState
                Public IsSendingActive As Boolean
                Public TextToView As String
                Public TextToViewColor As String
            End Structure

            Public Function GetSMSOwner(YourSoftwareUser As R2CoreSoftwareUser, YourImmediately As Boolean) As R2CoreStandardSMSOwnerStructure
                Try
                    Dim DS As New DataSet
                    Dim Da As New SqlClient.SqlDataAdapter
                    If YourImmediately Then
                        Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc")
                        Da.SelectCommand.Connection = R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection
                        If Da.Fill(DS) <> 0 Then Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 60, DS, New Boolean).GetRecordsCount <> 0 Then
                            Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Else
                            Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                        End If
                    End If
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwner(YourSoftwareUserId As Int64, YourImmediately As Boolean) As R2CoreStandardSMSOwnerStructure
                Try
                    Dim DS As New DataSet
                    Dim Da As New SqlClient.SqlDataAdapter
                    If YourImmediately Then
                        Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc")
                        Da.SelectCommand.Connection = R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection
                        If Da.Fill(DS) <> 0 Then Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourSoftwareUserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 60, DS, New Boolean).GetRecordsCount <> 0 Then
                            Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Else
                            Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                        End If
                    End If
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerCurrentState(YourSoftwareUserId As Int64, YourImmediately As Boolean) As SMSOwnerCurrentState
                Try
                    Dim NSSSMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Dim TextToView = String.Empty
                    Dim TextToViewColor = String.Empty
                    Try
                        NSSSMSOwner = GetSMSOwner(YourSoftwareUserId, YourImmediately)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        TextToView = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14)
                        TextToViewColor = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12)
                        Return New SMSOwnerCurrentState With {.IsSendingActive = False, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                    Catch ex As Exception
                        Throw ex
                    End Try
                    TextToView = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 15), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14))
                    TextToViewColor = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 13), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12))
                    Return New SMSOwnerCurrentState With {.IsSendingActive = NSSSMSOwner.IsSendingActive, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSoftwareUsers(YourUserTypeId As Int64) As List(Of R2CoreSoftwareUser)
                Try
                    Dim LstUsers As New List(Of R2CoreSoftwareUser)
                    Dim DS As New DataSet
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                         "Select Distinct SoftwareUsers.* from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners 
                            Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId =SoftwareUsers.UserId 
                          Where SoftwareUsers.UserTypeId=" & YourUserTypeId & " and SMSOwners.IsSendingActive=1 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 ", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreSoftwareUser With {.UserId = DS.Tables(0).Rows(Loopx).Item("UserId"), .ApiKey = DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, .APIKeyExpiration = DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), .UserName = DS.Tables(0).Rows(Loopx).Item("UserName").trim, .UserShenaseh = DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, .UserPassword = DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, .UserPasswordExpiration = DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), .UserPinCode = DS.Tables(0).Rows(Loopx).Item("UserPinCode"), .UserCanCharge = DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), .UserActive = DS.Tables(0).Rows(Loopx).Item("UserActive"), .UserTypeId = DS.Tables(0).Rows(Loopx).Item("UserTypeId"), .MobileNumber = DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim})
                    Next
                    Return LstUsers
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerCurrentState(YourSoftwareUser As R2CoreSoftwareUser) As SMSOwnerCurrentState
                Try
                    Dim NSSSMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Dim TextToView = String.Empty
                    Dim TextToViewColor = String.Empty
                    Try
                        NSSSMSOwner = GetSMSOwner(YourSoftwareUser, True)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        TextToView = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14)
                        TextToViewColor = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12)
                        Return New SMSOwnerCurrentState With {.IsSendingActive = False, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                    Catch ex As Exception
                        Throw ex
                    End Try
                    TextToView = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 15), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14))
                    TextToViewColor = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 13), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12))
                    Return New SMSOwnerCurrentState With {.IsSendingActive = NSSSMSOwner.IsSendingActive, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerReminderCreditPercent(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure) As Int64
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    Return InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) * YourNSSSMSOwner.ReminderCharge / 100
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared _SendSMSOwnersPleaseChargeFlag As Boolean = False
            Public Sub SendSMSOwnersPleaseCharge()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySMSSystemSqlConnection.GetTransactionDBConnection
                Try
                    Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)
                    Dim InstancePersianCallendar = New R2CorePersianCalendarManager(_DateTimeService)

                    'کنترل زمان اجرای فرآیند بر اساس کانفیگ
                    Dim TimeOfDay = _DateTimeService.GetCurrentTickofTime
                    Dim StartTime = TimeSpan.Parse("00:00:00").Ticks
                    Dim EndTime = TimeSpan.Parse("00:05:00").Ticks
                    Dim ConfigTime = TimeSpan.Parse(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 9)).Ticks
                    If TimeOfDay >= StartTime And TimeOfDay <= EndTime Then
                        _SendSMSOwnersPleaseChargeFlag = False
                        Return
                    ElseIf TimeOfDay <= ConfigTime Then
                        Return
                    Else
                    End If

                    'این فرآیند در روز فقط باید یکبار اجرا گردد و نه بیشتر
                    'خط کد زیر یعنی فرآیند امروز قبلا در بازه معین اجرا یکبار اجرا شده است
                    If _SendSMSOwnersPleaseChargeFlag Then Return

                    'لیست مالکان اس ام اس که برایشان قبلا اس ام اس پلیز شارژ نرفته است و باید برود
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                           "Select * from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners
                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId=SoftwareUsers.UserId 
                            Where SMSOwners.IsSendingActive=1 and SMSOwners.ReminderCharge<>0 and ((SMSOwners.ReminderCharge-SMSOwners.ReminderHolder)<=(SMSOwners.ReminderCharge * " & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) & " / 100))
                                  and SMSOwners.PleaseCharge=0 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0", 600, DS, New Boolean).GetRecordsCount = 0 Then
                        _SendSMSOwnersPleaseChargeFlag = True
                        Return
                    End If

                    'تغییر فیلد پلیز شارژ به ارسال شده
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim UserId As Int64 = DS.Tables(0).Rows(Loopx).Item("SMSOwnerUserId")
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set PleaseCharge=1 Where SMSOwnerUserId=" & UserId & " and IsSendingActive=1 and PleaseCharge=0"
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    _SendSMSOwnersPleaseChargeFlag = True

                    'ارسال پیام به مالکان اس ام اس
                    Dim LstUsers = New List(Of R2CoreSoftwareUser)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreSoftwareUser With {.UserId = DS.Tables(0).Rows(Loopx).Item("UserId"), .ApiKey = DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, .APIKeyExpiration = DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), .UserName = DS.Tables(0).Rows(Loopx).Item("UserName").trim, .UserShenaseh = DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, .UserPassword = DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, .UserPasswordExpiration = DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), .UserPinCode = DS.Tables(0).Rows(Loopx).Item("UserPinCode").trim, .UserCanCharge = DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), .UserActive = DS.Tables(0).Rows(Loopx).Item("UserActive"), .UserTypeId = DS.Tables(0).Rows(Loopx).Item("UserTypeId"), .MobileNumber = DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim})
                    Next
                    Dim InstanceSMSHandler = New R2CoreSMSHandlerManager(_DateTimeService, Nothing)
                    Dim SMSResult = InstanceSMSHandler.SendSMS(LstUsers, R2CoreSMSTypes.PleaseCharge, InstanceSMSHandler.RepeatSMSCreationData(New SMSCreationData With {.Data1 = String.Empty}, LstUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandler.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SMSOwnerSendingPleaseChargeMessageFailedException(SMSResultAnalyze)
                Catch ex As SMSOwnerSendingPleaseChargeMessageFailedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

        End Class

        'BPTChanged
        Public Class R2CoreSMSOwnerTypesManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function GetNSSSMSOwnerType(YourSMSOwnerTypeId As Int64) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection, "Select * from R2PrimarySMSSystem.dbo.TblSMSOwnerTypes Where SMSOTypeId=" & YourSMSOwnerTypeId & " and Active=1 and Deleted=0", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeNotFoundException
                    End If
                Catch ex As SMSOwnerTypeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerTypeBySoftwareUser(YourSoftwareUserId As Int64) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySMSSystemSqlConnection.GetSubscriptionDBConnection,
                          "Select * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                            Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On SoftwareUsers.UserTypeId=SoftwareUserTypes.UTId 
                            Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUserTypes.UTId=EntityRelations.E1 
                            Inner Join R2PrimarySMSSystem.dbo.TblSMSOwnerTypes as SMSOwnerTypes On EntityRelations.E2=SMSOwnerTypes.SMSOTypeId 
                           Where SoftwareUsers.UserId=" & YourSoftwareUserId & " and SoftwareUserTypes.Active=1 and SoftwareUserTypes.Deleted=0 and EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUserType_SMSOwnerType & " and SMSOwnerTypes.Active=1", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeBySoftwareUserNotFoundException
                    End If
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try


            End Function

        End Class


    End Namespace

    Namespace SMSResultLogging

        Public Class R2CoreLoggingSMSResultManager
            'Public Sub Logging(YourSMSResult As List(Of KeyValuePair(Of Int64, String)), YourNSSUser As R2CoreStandardSoftwareUserStructure)
            '    Try
            '        Dim InstanceLogging = New R2CoreInstanceLoggingManager
            '        Dim InstanceSendSMSCode = New R2CoreSendSMSCodeManager
            '        For Loopx As Int64 = 0 To YourSMSResult.Count - 1
            '            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.SendSMSResult, InstanceLogging.GetNSSLogType(R2CoreLogType.SendSMSResult).LogTitle, InstanceSendSMSCode.GetNSSSendSMSCode(YourSMSResult(Loopx).Key).SendSMSCodeTitle, YourSMSResult(Loopx).Value, String.Empty, String.Empty, String.Empty, YourNSSUser.UserId, Nothing, Nothing))
            '        Next
            '    Catch ex As Exception

            '    End Try
            'End Sub
        End Class
    End Namespace

    Namespace Exceptions

        Public Class SmsSystemIsDisabledException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سیستم اس ام اس از طریق جدول پیکربندی غیر فعال است"
                End Get
            End Property
        End Class

        Public Class SendingSMSFailedException
            Inherits BPTException

            Private _SMSId As Int64

            Public Sub New(YourSMSId As Int64)
                _SMSId = YourSMSId
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "هنگام ارسال اس ام اس با کد " + _SMSId.ToString + "مشکلی وجود دارد"
                End Get
            End Property
        End Class


    End Namespace

End Namespace
