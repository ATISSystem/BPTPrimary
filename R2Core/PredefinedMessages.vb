
Imports System.Drawing
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PredefinedMessagesManagement.Exceptions

Namespace PredefinedMessagesManagement

    'BPTChanged
    Public MustInherit Class R2CorePredefinedMessages
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly IPISBlack As Int64 = 1
        Public Shared ReadOnly BlackIPTypeNotFound As Int64 = 2
        Public Shared ReadOnly MobileNumberIsInvalid As Int64 = 3
        Public Shared ReadOnly UploadedImageSizeExeededException As Int64 = 6
        Public Shared ReadOnly FileNotExistException As Int64 = 7
        Public Shared ReadOnly DataEntryException As Int64 = 8
        Public Shared ReadOnly CacheTypeNotFound As Int64 = 9
        Public Shared ReadOnly CaptchaInvalid As Int64 = 10
        Public Shared ReadOnly CacheNotFound As Int64 = 11
        Public Shared ReadOnly PredefinedMessageNotFound As Int64 = 12
        Public Shared ReadOnly SystemBugDataBaseEquivalenceMessage As Int64 = 13
        Public Shared ReadOnly ExceptionfromDataBase As Int64 = 14
        Public Shared ReadOnly EditingSoftWareUserSuccessed As Int64 = 18
        Public Shared ReadOnly ActivateSMSOwnerSuccessed As Int64 = 19
        Public Shared ReadOnly SendWebSiteLink As Int64 = 20
        Public Shared ReadOnly ChangeSoftwareUserWebProcessGroupAccessSuccessed As Int64 = 21
        Public Shared ReadOnly ChangeSoftwareUserWebProcessAccess As Int64 = 22
        Public Shared ReadOnly RegisteringInformationSuccessed As Int64 = 23
        Public Shared ReadOnly ProcessSuccessed As Int64 = 24
        Public Shared ReadOnly SessionOverException As Int64 = 25
        Public Shared ReadOnly BPTSoapException As Int64 = 26
        Public Shared ReadOnly AnyNotFoundException As Int64 = 28
        Public Shared ReadOnly MoneyWalletNotFoundException As Int64 = 30
        Public Shared ReadOnly WebServiceConnectingException As Int64 = 31
        Public Shared ReadOnly UserHasAlreadyBeenAuthenticated As Int64 = 32
        Public Shared ReadOnly PleaseReTryException As Int64 = 33
        Public Shared ReadOnly NewUserPasswordSentSuccessfully As Int64 = 34
        Public Shared ReadOnly PleaseEnterOTPCode As Int64 = 35
        Public Shared ReadOnly OTPCodeInvalid As Int64 = 36
        Public Shared ReadOnly UserNotAlllowedException As Int64 = 37
        Public Shared ReadOnly UnableConnectToAPIException As Int64 = 38
        Public Shared ReadOnly SoftwareUserMoneyWalletNotFoundException As Int64 = 39
        Public Shared ReadOnly PermissionException As Int64 = 40
        Public Shared ReadOnly InternetIsnotAvailableException As Int64 = 41
        Public Shared ReadOnly ConnectionIsNotAvailableException As Int64 = 42
        Public Shared ReadOnly FileNotFoundInRawGroupsException As Int64 = 43
        Public Shared ReadOnly RawGroupNotFoundException As Int64 = 44
        Public Shared ReadOnly CaptchaWordNotCorrectException As Int64 = 45
        Public Shared ReadOnly ExchangeKeyNotExistException As Int64 = 46
        Public Shared ReadOnly ExchangeKeyTimeRangePassedException As Int64 = 47
        Public Shared ReadOnly SqlInjectionException As Int64 = 48
        Public Shared ReadOnly SendSMSCodeIdNotFoundExcption As Int64 = 49
        Public Shared ReadOnly SMSRecivedCodeforSMSCodeNotFoundException As Int64 = 50
        Public Shared ReadOnly ProgressInfNotFoundException As Int64 = 51
        Public Shared ReadOnly MobileNumberInvallidException As Int64 = 52


    End Class

    'BPTChanged
    Public Class R2CoreRawPredefinedMessage
        Public MsgId As Int64
        Public MsgTitle As String
        Public MsgContent As String
        Public Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CorePredefinedMessagesManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'BPTChanged
        Public Function GetPredefinedMessage(YourPredefinedMessageId As Int64) As R2CoreRawPredefinedMessage
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select MsgId,MsgTitle,MsgContent,Active from R2Primary.dbo.TblPredefinedMessages Where MsgId  = " & YourPredefinedMessageId & " And Active=1 And Deleted=0", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New PredefinedMessageNotFoundException
                Return New R2CoreRawPredefinedMessage With {.MsgId = Ds.Tables(0).Rows(0).Item("MsgId"), .MsgTitle = Ds.Tables(0).Rows(0).Item("MsgTitle").trim, .MsgContent = Ds.Tables(0).Rows(0).Item("MsgContent").trim, .Active = Ds.Tables(0).Rows(0).Item("Active")}
            Catch ex As DataBaseException
                Throw ex
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    'BPTChanged
    Namespace Exceptions
        Public Class PredefinedMessageNotFoundException
            Inherits BPTException

            Public Sub New()
                Try
                    _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PredefinedMessageNotFound).MsgId
                    _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PredefinedMessageNotFound).MsgContent
                Catch ex As FileNotExistException
                    Throw ex
                Catch ex As PredefinedMessageNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class


    End Namespace

End Namespace
