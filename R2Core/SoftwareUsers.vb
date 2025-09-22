


Imports Newtonsoft.Json
Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.EntityRelationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PermissionManagement
Imports R2Core.SecurityAlgorithmsManagement.PasswordStrength
Imports R2Core.SecurityAlgorithmsManagement.AESAlgorithms
Imports R2Core.SecurityAlgorithmsManagement.Captcha
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.ExpressionValidationAlgorithms
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SessionManagement
Imports R2Core.SMS.SMSHandling
Imports R2Core.SMS.SMSOwners
Imports R2Core.SMS.SMSTypes
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SoftwareUserManagement.Exceptions
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
Imports Microsoft.VisualBasic.ApplicationServices
Imports R2Core.PermissionManagement.Exceptions
Imports R2Core.Caching
Imports R2Core.RawGroups
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Web
Imports R2Core.CachHelper
Imports StackExchange.Redis
Imports R2Core.PubSubMessaging
Imports R2Core.LoggingManagement
Imports R2Core.DateTimeProvider
Imports R2Core.SQLInjectionPrevention

Namespace SoftwareUserManagement

    Public Enum R2CoreSoftwareUsersOrderingOptions
        None = 0
        UserName = 1
        MobileNumber = 2
    End Enum

    Public MustInherit Class R2CoreSoftwareUserTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property DesktopUser As Int64 = 1
        Public Shared ReadOnly Property SysAdmin As Int64 = 2

    End Class

    Public Class R2CoreStandardSoftwareUserTypeStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            UTId = Int64.MinValue
            UTName = String.Empty
            UTTitle = String.Empty
            UTColor = Color.White
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourUTId As Int64, YourUTName As String, YourUTTitle As String, YourUTColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourUTId, YourUTName)
            UTId = YourUTId
            UTName = YourUTName
            UTTitle = YourUTTitle
            UTColor = YourUTColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property UTId As Int64
        Public Property UTName As String
        Public Property UTTitle As String
        Public Property UTColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreStandardSoftwareUserStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            UserId = Int64.MinValue
            ApiKey = String.Empty
            APIKeyExpiration = String.Empty
            UserName = String.Empty
            UserShenaseh = String.Empty
            UserPassword = String.Empty
            UserPasswordExpiration = String.Empty
            UserPinCode = String.Empty
            UserCanCharge = Boolean.FalseString
            UserActive = Boolean.FalseString
            UserTypeId = Int64.MinValue
            MobileNumber = String.Empty
            UserStatus = String.Empty
            VerificationCode = String.Empty
            VerificationCodeTimeStamp = String.Empty
            VerificationCodeCount = Int64.MaxValue
            Nonce = String.Empty
            NonceTimeStamp = DateTime.Now
            NonceCount = Int64.MaxValue
            PersonalNonce = String.Empty
            PersonalNonceTimeStamp = DateTime.Now
            Captcha = String.Empty
            CaptchaValid = Boolean.FalseString
            UserCreatorId = Int64.MinValue
            DateTimeMilladi = DateTime.Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourUserId As Int64, YourApiKey As String, YourAPIKeyExpiration As String,
                       ByVal YourUserName As String, ByVal YourUserShenaseh As String, ByVal YourUserPassword As String,
                       YourUserPasswordExpiration As String, YourUserPinCode As String, ByVal YourUserCanCharge As Boolean,
                       ByVal YourUserActive As Boolean, YourUserTypeId As Int64, YourMobileNumber As String, YourUserStatus As String,
                       YourVerificationCode As String, YourVerificationCodeTimeStamp As DateTime, YourVerificationCodeCount As Int64, YourNonce As String,
                       YourNonceTimeStamp As DateTime, YourNonceCount As Int64, YourPersonalNonce As String, YourPersonalNonceTimeStamp As DateTime,
                       YourCaptcha As String, YourCaptchaValid As Boolean, YourUserCreatorId As Int64,
                       YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourUserId, YourUserName)
            UserId = YourUserId
            ApiKey = YourApiKey
            APIKeyExpiration = YourAPIKeyExpiration
            UserName = YourUserName
            UserShenaseh = YourUserShenaseh
            UserPassword = YourUserPassword
            UserPasswordExpiration = YourUserPasswordExpiration
            UserPinCode = YourUserPinCode
            UserCanCharge = YourUserCanCharge
            UserActive = YourUserActive
            UserTypeId = YourUserTypeId
            MobileNumber = YourMobileNumber
            UserStatus = YourUserStatus
            VerificationCode = YourVerificationCode
            VerificationCodeTimeStamp = YourVerificationCodeTimeStamp
            VerificationCodeCount = YourVerificationCodeCount
            Nonce = YourNonce
            NonceTimeStamp = YourNonceTimeStamp
            NonceCount = YourNonceCount
            PersonalNonce = YourPersonalNonce
            PersonalNonceTimeStamp = YourPersonalNonceTimeStamp
            Captcha = YourCaptcha
            CaptchaValid = YourCaptchaValid
            UserCreatorId = YourUserCreatorId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Deleted = YourDeleted
        End Sub

        Public Property UserId As Int64
        Public Property ApiKey As String
        Public Property APIKeyExpiration As String
        Public Property UserName As String
        Public Property UserShenaseh As String
        Public Property UserPassword As String
        Public Property UserPasswordExpiration As String
        Public Property UserPinCode() As String
        Public Property UserCanCharge() As Boolean
        Public Property UserActive() As Boolean
        Public Property UserTypeId() As Int64
        Public Property MobileNumber As String
        Public Property UserStatus As String
        Public Property VerificationCode As String
        Public Property VerificationCodeTimeStamp As DateTime
        Public Property VerificationCodeCount As Int64
        Public Property Nonce As String
        Public Property NonceTimeStamp As DateTime
        Public Property NonceCount As Int64
        Public Property PersonalNonce As String
        Public Property PersonalNonceTimeStamp As DateTime
        Public Property Captcha As String
        Public Property CaptchaValid As Boolean
        Public Property UserCreatorId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreSoftwareUserMobile
        Public Sub New(YourMobileNumber As String)
            SoftwareUserMobileNumber = YourMobileNumber
        End Sub

        Private _SoftwareUserMobileNumber = String.Empty
        Public Property SoftwareUserMobileNumber As String
            Get
                Return _SoftwareUserMobileNumber
            End Get
            Set(value As String)
                _SoftwareUserMobileNumber = value
            End Set
        End Property
    End Class

    Public Class R2CoreInstanseSoftwareUsersManager

        Private _R2DateTimeService As IR2DateTimeService
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_R2DateTimeService)
        End Sub

        Public Function GetNSSUser(YourApiKey As String) As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where ApiKey='" & YourApiKey & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserNotExistByApiKeyException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As UserNotExistByApiKeyException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSUserChangeableData(YourSoftWareUserMobile As R2CoreSoftwareUserMobile) As R2CoreStandardSoftwareUserStructure
            Try
                Dim InstanceEVA = New ExpressionValidationAlgorithmsManager
                InstanceEVA.ValidationMobileNumber(YourSoftWareUserMobile.SoftwareUserMobileNumber)
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                       "Select Top 1 UserId,ApiKey,APIKeyExpiration,UserShenaseh,UserPassword,UserPasswordExpiration,UserTypeId,MobileNumber,UserStatus,VerificationCode,VerificationCodeTimeStamp,VerificationCodeCount,Nonce,NonceTimeStamp,NonceCount,PersonalNonce,PersonalNonceTimeStamp,Captcha,CaptchaValid
                        from R2Primary.dbo.TblSoftwareUsers Where MobileNumber='" & YourSoftWareUserMobile.SoftwareUserMobileNumber & "' Order By UserId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserNotExistByMobileNumberException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration").trim, Nothing, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration").trim, Nothing, Nothing, Nothing, Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce").trim, Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce").trim, Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha").trim, Ds.Tables(0).Rows(0).Item("CaptchaValid"), Nothing, Nothing, Nothing, Nothing, Nothing)
            Catch exx As UserNotExistByMobileNumberException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSUserUnChangeable(YourSoftWareUserMobile As R2CoreSoftwareUserMobile) As R2CoreStandardSoftwareUserStructure
            Try
                Dim InstanceEVA = New ExpressionValidationAlgorithmsManager
                InstanceEVA.ValidationMobileNumber(YourSoftWareUserMobile.SoftwareUserMobileNumber)
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers Where MobileNumber='" & YourSoftWareUserMobile.SoftwareUserMobileNumber & "' Order By UserId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserNotExistByMobileNumberException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode").trim, Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce").trim, Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce").trim, Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha").trim, Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As UserNotExistByMobileNumberException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSUser(YourUserId As Int64) As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourUserId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserIdNotExistException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSystemUserId() As Int64
            Try
                Return 1
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSSystemUser() As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select UserId from R2Primary.dbo.TblSoftwareUsers Where UserId=1", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserIdNotExistException
                End If
                Return GetNSSUser(System.Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function RegisteringMobileNumber(YourMobileNumber As String) As R2CoreStandardSoftwareUserStructure
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                'SqlInjectionPrevention
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_R2DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourMobileNumber)

                Dim InstanceAESAlgorithms As New AESAlgorithmsManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim VerificationCode As String = InstanceAESAlgorithms.GenerateVerificationCode(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 9))
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select Top 1 SoftwareUsers.UserId,SoftwareUsers.MobileNumber from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers 
                    Where SoftwareUsers.MobileNumber='" & YourMobileNumber & "' and UserActive=1 
                    Order By SoftwareUsers.UserId Desc", 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers 
                                          Set VerificationCode='" & VerificationCode & "',VerificationCodeTimeStamp='" & _R2DateTimeService.GetCurrentDateTimeMilladi & "',VerificationCodeCount=" & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 4) & " 
                                          Where UserId=" & Ds.Tables(0).Rows(0).Item("UserId") & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                    SMSSendApplicationActivationCode(GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId"))), VerificationCode)
                    Return GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
                Else
                    Throw New MobileNumberNotFoundException
                End If
            Catch ex As SendSMSVerificationCodeFailedException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As MobileNumberNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LogoutSoftwareUser(YourUserId As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserStatus='logout' Where UserId=" & YourUserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub LoginSoftwareUser(YourMobileNumber As String)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                'SqlInjectionPrevention
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_R2DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourMobileNumber)

                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim APIKeyExpiration As String = _R2DateTimeService.GetShamsiDateWithAddMonth(_R2DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 1))
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserStatus='login',APIKeyExpiration='" & APIKeyExpiration & "' Where MobileNumber='" & YourMobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AuthenticationUserByMobileNumber(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                         "Select UserActive from R2Primary.dbo.TblSoftwareUsers where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New UserNotExistException
                Else
                    If DS.Tables(0).Rows(0).Item("UserActive") = False Then Throw New UserIsNotActiveException
                End If
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNonceforSoftwareUser(YourSoftwareUserMobile As R2CoreSoftwareUserMobile) As String
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceAESAlgorithms = New AESAlgorithmsManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim Nonce = InstanceAESAlgorithms.GetNonce(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.PublicSecurityConfiguration, 0))
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set Nonce='" & Nonce & "',NonceCount=" & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.PublicSecurityConfiguration, 1) & ",NonceTimeStamp='" & _R2DateTimeService.GetCurrentDateTimeMilladi & "' Where MobileNumber='" & YourSoftwareUserMobile.SoftwareUserMobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                Return Nonce
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub DecreaseNonceCountforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                AuthenticationUserByMobileNumber(YourNSSSoftwareUser)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set NonceCount=NonceCount-1 Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DecreaseVerificationCodeCountforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                AuthenticationUserByMobileNumber(YourNSSSoftwareUser)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set VerificationCodeCount=VerificationCodeCount-1 Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetPersonalNonceforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As String
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceAESAlgorithms = New AESAlgorithmsManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim PersonalNonce = InstanceAESAlgorithms.GetNonce(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 3))
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set PersonalNonce='" & PersonalNonce & "',PersonalNonceTimeStamp='" & _R2DateTimeService.GetCurrentDateTimeMilladi & "' Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                Return PersonalNonce
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetCaptchaforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As String
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceCaptcha = New R2CoreInstanceCaptchaManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim Captcha = InstanceCaptcha.GenerateFakeWord(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set Captcha='" & Captcha & "',CaptchaValid=1 Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                Return Captcha
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetCaptchaNumericforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As String
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceCaptcha = New R2CoreInstanceCaptchaManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                Dim Captcha = InstanceCaptcha.GenerateFakeWordNumeric(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set Captcha='" & Captcha & "',CaptchaValid=1 Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
                Return Captcha
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub CaptchaInvalidateforSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                AuthenticationUserByMobileNumber(YourNSSSoftwareUser)
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set Captcha='',CaptchaValid=0 Where MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SoftwareUserVerificationCodeInjection(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers 
                                      Set VerificationCode='" & InstanceConfiguration.GetConfigString(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 12) & "',VerificationCodeTimeStamp='" & _R2DateTimeService.GetCurrentDateTimeMilladi & "',VerificationCodeCount=" & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 4) & " 
                                      Where UserId=" & YourNSSSoftwareUser.UserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        'Public Function GetNSSSoftwareUserType(YourSoftwareUserType As String) As R2CoreStandardSoftwareUserStructure
        '    Try
        '        Dim Instanse = New R2CoreSqlDataBOXManager
        '        Dim Ds As DataSet
        '        If Instanse.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where ApiKey='" & YourApiKey & "'", 0, Ds).GetRecordsCount() = 0 Then
        '            Throw New UserNotExistByApiKeyException
        '        End If
        '        Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
        '    Catch exx As UserNotExistByApiKeyException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Function GetSoftwareUsers_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreStandardSoftwareUserStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardSoftwareUserStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2Primary.dbo.TblSoftwareUsers Where Left(UserName," & YourSearchString.Length & ")='" & YourSearchString & "' and Deleted=0 and UserActive=1 Order By UserName", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode"), DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce"), DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce"), DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha"), DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSoftwareUsers_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreStandardSoftwareUserStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardSoftwareUserStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2Primary.dbo.TblSoftwareUsers Where UserName Like '%" & YourSearchString & "%' and Deleted=0 and UserActive=1 Order By UserName", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode"), DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce"), DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce"), DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha"), DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSVirtualChargingSoftwareUser() As R2CoreStandardSoftwareUserStructure
            Dim InstanceSoftwareUser As New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
            Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                         "Select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                          Where SoftwareUsers.UserId=" & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 13) & " and SoftwareUsers.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New UserIdNotExistException
                Return InstanceSoftwareUser.GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSSelfGoverningChargingSoftwareUser() As R2CoreStandardSoftwareUserStructure
            Dim InstanceSoftwareUser As New R2CoreInstanseSoftwareUsersManager(_R2DateTimeService)
            Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_R2DateTimeService)
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                         "Select Top 1 SoftwareUsers.UserId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                          Where SoftwareUsers.UserId=" & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 14) & " and SoftwareUsers.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New UserIdNotExistException
                Return InstanceSoftwareUser.GetNSSUser(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")))
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SendUserSecurity(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_R2DateTimeService)
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourNSSSoftwareUser.UserShenaseh, .Data2 = YourNSSSoftwareUser.UserPassword}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreSMSTypes.SoftwareUserSecurity, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSSoftwareUserSecurityFailedException(SMSResultAnalyze)
            Catch ex As SendSMSSoftwareUserSecurityFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SMSSendApplicationActivationCode(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourVerificationCode As String)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_R2DateTimeService)
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourVerificationCode}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreSMSTypes.ApplicationActivationCode, LstCreationData, False)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSVerificationCodeFailedException(SMSResultAnalyze)
            Catch ex As SendSMSVerificationCodeFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2CoreMClassSoftwareUsersManagement

        Private Shared _DateTimeService As New R2DateTimeService
        Private Shared R2PrimaryFSWS = New R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Property GetNoneUserId As Int64 = 0

        Public Shared Function RegisteringSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Dim AES As New R2Core.SecurityAlgorithmsManagement.AESAlgorithms.AESAlgorithmsManager
            Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
            Try
                If YourNSSSoftwareUser.MobileNumber.Trim = String.Empty Then Throw New SoftwareUserMobileNumberNeedforRegisteringException

                'اگر کاربر یبا شماره موبایل موجود بود رجیستر جدید امکان پذیر نیست
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Try
                    If YourNSSSoftwareUser.MobileNumber = "09130000000" Then Exit Try
                    InstanceSoftwareUsers.GetNSSUserUnChangeable(New R2CoreSoftwareUserMobile(YourNSSSoftwareUser.MobileNumber))
                    Throw New SoftwareUserMobileNumberAlreadyExistException
                Catch ex As UserNotExistByMobileNumberException
                Catch ex As Exception
                    Throw ex
                End Try

                Dim InstanceConfiguration As New R2CoreInstanceConfigurationManager(_DateTimeService)
                Dim APIKeyExpiration As String = _DateTimeService.GetShamsiDateWithAddMonth(_DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 1))
                Dim UserPasswordExpiration As String = _DateTimeService.GetShamsiDateWithAddMonth(_DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 2))
                Dim myShenaseh As String = AES.GetRandomNumericCode(10000, 99999)
                Dim myPassword As String = AES.GetRandomNumericCode(10000000, 99999999)
                Dim UIdSalt As String = AES.GetSalt(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 0))
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 UserId from R2Primary.dbo.TblSoftwareUsers with (tablockx) order by UserId desc"
                CmdSql.ExecuteNonQuery()
                Dim myUserId As Int64 = CmdSql.ExecuteScalar + 1
                Dim APIKey = Hasher.GenerateSHA256String(myUserId.ToString + UIdSalt)

                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblSoftwareUsers(UserId,ApiKey,APIKeyExpiration,UserName,UserShenaseh,UserPassword,UserPasswordExpiration,UserPinCode,UserCanCharge,UserActive,UserTypeId,MobileNumber,UserStatus,VerificationCode,VerificationCodeTimeStamp,VerificationCodeCount,Nonce,NonceTimeStamp,NonceCount,PersonalNonce,PersonalNonceTimeStamp,Captcha,CaptchaValid,UserCreatorId,DateTimeMilladi,DateShamsi,ViewFlag,Deleted)
                                      Values(" & myUserId & ",'" & APIKey & "','" & APIKeyExpiration & "','" & YourNSSSoftwareUser.UserName & "','" & myShenaseh & "','" & myPassword & "','" & UserPasswordExpiration & "','" & YourNSSSoftwareUser.UserPinCode & "'," & IIf(YourNSSSoftwareUser.UserCanCharge, 1, 0) & "," & IIf(YourNSSSoftwareUser.UserActive, 1, 0) & "," & YourNSSSoftwareUser.UserTypeId & ",'" & YourNSSSoftwareUser.MobileNumber & "','logout','','" & _DateTimeService.GetCurrentDateTimeMilladi & "',0,'','" & _DateTimeService.GetCurrentDateTimeMilladi & "','','" & _DateTimeService.GetCurrentDateTimeMilladi & "',0,'',0," & YourNSSUser.UserId & ",'" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentShamsiDate & "'," & IIf(YourNSSSoftwareUser.ViewFlag, 1, 0) & ",0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return myUserId
            Catch ex As SoftwareUserMobileNumberAlreadyExistException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw ex
            Catch ex As SoftwareUserMobileNumberNeedforRegisteringException
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

        Public Shared Sub EditingSoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                If YourNSSSoftwareUser.MobileNumber.Trim = String.Empty Then Throw New SoftwareUserMobileNumberNeedforRegisteringException

                Try
                    Dim NSSSoftwareUser = InstanceSoftwareUsers.GetNSSUserUnChangeable(New R2CoreSoftwareUserMobile(YourNSSSoftwareUser.MobileNumber))
                    If NSSSoftwareUser.UserId <> YourNSSSoftwareUser.UserId Then Throw New SoftwareUserMobileNumberAlreadyExistException
                Catch ex As UserNotExistByMobileNumberException
                Catch ex As Exception
                    Throw ex
                End Try

                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers 
                        Set UserName='" & YourNSSSoftwareUser.UserName & "',UserPinCode='" & YourNSSSoftwareUser.UserPinCode & "',UserCanCharge=" & IIf(YourNSSSoftwareUser.UserCanCharge, 1, 0) & ",UserActive=" & IIf(YourNSSSoftwareUser.UserActive, 1, 0) & ",UserTypeId=" & YourNSSSoftwareUser.UserTypeId & ",MobileNumber='" & YourNSSSoftwareUser.MobileNumber & "',UserCreatorId=" & YourNSSUser.UserId & " 
                        Where UserId=" & YourNSSSoftwareUser.UserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SoftwareUserMobileNumberAlreadyExistException
                Throw ex
            Catch ex As SoftwareUserMobileNumberNeedforRegisteringException
                Throw ex
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function IsUserRegistered(YourUserNSS As R2CoreStandardSoftwareUserStructure) As Boolean
            Try
                AuthenticationUserbyShenasehPassword(YourUserNSS)
                Return True
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As UserNotExistException
                Return False
            Catch ex As UserIsNotActiveException
                Return False
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub AuthenticationUserbyShenasehPassword(YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourUserNSS.UserShenaseh)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourUserNSS.UserPassword)

                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers where ltrim(rtrim(UserShenaseh))='" & YourUserNSS.UserShenaseh & "' and ltrim(rtrim(UserPassword))='" & YourUserNSS.UserPassword & "'", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New UserNotExistException
                Else
                    If DS.Tables(0).Rows(0).Item("UserActive") = False Then Throw New UserIsNotActiveException
                End If
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub AuthenticationUserByPinCode(YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers where UserPinCode='" & YourUserNSS.UserPinCode & "'", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New UserNotExistException
                Else
                    If DS.Tables(0).Rows(0).Item("UserActive") = False Then Throw New UserIsNotActiveException
                End If
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetNSSSystemUser() As R2CoreStandardSoftwareUserStructure
            Try
                Return GetNSSUser(1)
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSUser(YourUserId As Int64) As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourUserId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserIdNotExistException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As UserIdNotExistException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSUser(YourUserShenaseh As String, YourUserPassword As String) As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserShenaseh='" & YourUserShenaseh.Trim() & "' and UserPassword='" & YourUserPassword.Trim() & "'", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSUser(YourPinCode As String) As R2CoreStandardSoftwareUserStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserPinCode='" & YourPinCode.Trim() & "'", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Return New R2CoreStandardSoftwareUserStructure(Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("ApiKey").trim, Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), Ds.Tables(0).Rows(0).Item("UserName").trim, Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, Ds.Tables(0).Rows(0).Item("UserPassword").trim, Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), Ds.Tables(0).Rows(0).Item("UserPinCode"), Ds.Tables(0).Rows(0).Item("UserCanCharge"), Ds.Tables(0).Rows(0).Item("UserActive"), Ds.Tables(0).Rows(0).Item("UserTypeId"), Ds.Tables(0).Rows(0).Item("MobileNumber").trim, Ds.Tables(0).Rows(0).Item("UserStatus").trim, Ds.Tables(0).Rows(0).Item("VerificationCode").trim, Ds.Tables(0).Rows(0).Item("VerificationCodeTimeStamp"), Ds.Tables(0).Rows(0).Item("VerificationCodeCount"), Ds.Tables(0).Rows(0).Item("Nonce"), Ds.Tables(0).Rows(0).Item("NonceTimeStamp"), Ds.Tables(0).Rows(0).Item("NonceCount"), Ds.Tables(0).Rows(0).Item("PersonalNonce"), Ds.Tables(0).Rows(0).Item("PersonalNonceTimeStamp"), Ds.Tables(0).Rows(0).Item("Captcha"), Ds.Tables(0).Rows(0).Item("CaptchaValid"), Ds.Tables(0).Rows(0).Item("UserCreatorId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetUserImage(YourNSSUserRequest As R2CoreStandardSoftwareUserStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreImage
            Try

                Dim bf As BinaryFormatter = New BinaryFormatter()
                Dim MS As MemoryStream = New MemoryStream()
                bf.Serialize(MS, R2PrimaryFSWS.WebMethodGetFile(R2CoreRawGroups.UserImages, YourNSSUserRequest.UserId.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 0), R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword)))
                Return New R2CoreImage(MS.ToArray())
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub ChangeUserPassword(YourNSS As R2CoreStandardSoftwareUserStructure)
            Dim cmdsql As New SqlClient.SqlCommand
            cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                'SqlInjectionPrevention
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.UserPassword)

                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                Dim PS As PasswordStrength = New PasswordStrength
                PS.SetPassword(YourNSS.UserPassword)
                If (PS.GetPasswordStrength() = "Strong") Or (PS.GetPasswordStrength() = "Very Strong") Then
                    Dim UserPasswordExpiration As String = _DateTimeService.GetShamsiDateWithAddMonth(_DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 2))
                    cmdsql.Connection.Open()
                    cmdsql.CommandText = "update R2Primary.dbo.TblSoftwareUsers Set UserPassword='" & YourNSS.UserPassword & "',UserPasswordExpiration='" & UserPasswordExpiration & "' Where UserId=" & YourNSS.UserId & ""
                    cmdsql.ExecuteNonQuery()
                    cmdsql.Connection.Close()
                Else
                    Throw New PasswordStrengthRejectedException
                End If
            Catch ex As PasswordStrengthRejectedException
                Throw ex
            Catch ex As Exception
                If cmdsql.Connection.State <> ConnectionState.Closed Then cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions

        Public Class SoftwareUserMobileNumberBelongsToSomeoneElseException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره موبایل متعلق به دیگری است و مجاز نیست"
                End Get
            End Property
        End Class

        Public Class SoftwareUserMobileNumberAlreadyExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با شماره موبایل مورد نظر قبلا ثبت شده است"
                End Get
            End Property
        End Class

        Public Class SoftwareUserPasswordExpiredException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان اعتبار رمز عبور کاربر منقضی شده است"
                End Get
            End Property
        End Class

        Public Class SoftwareUserMobileNumberNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربر با شماره موبایل مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserMobileNumberNeedforRegisteringException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ورود شماره موبایل برای ثبت کاربر الزامی است"
                End Get
            End Property
        End Class

        Public Class MobileNumberNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره موبایل در بانک اطلاعات یافت نشد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserNotMatchException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کد تبادل نادرست است"
                End Get
            End Property
        End Class

        Public Class UserNotExistByApiKeyException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با کلید خصوصی ارسالی یافت نشد"
                End Get
            End Property
        End Class

        Public Class UserNotExistByMobileNumberException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با شماره موبایل مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class UserNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با مشخصات ارسالی یافت نشد"
                End Get
            End Property
        End Class

        Public Class UserLast5DigitNotMatchingException
            Inherits ApplicationException
            'رمز شخصی همان پسوورد کاربری است
            'به دلیل جلوگیری از هکر و امنیت کاربر ، رمز شخصی توسط خود کاربر ارسال می گردد 
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "خطای امنیتی کد : 39 رمز شخصی به درستی وارد نشده است"
                End Get
            End Property
        End Class

        Public Class UserIsNotActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "حساب کاربر مورد نظر در حال حاضر غیر فعال است"
                End Get
            End Property
        End Class

        Public Class UserIdNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با کد ارسالی در سیستم وجود ندارد"
                End Get
            End Property
        End Class

        Public Class PasswordStrengthRejectedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "رمز عبور کاربر می بایست حداقل 8 حرف و شامل حروف کوچک و بزرگ و اعداد باشد"
                End Get
            End Property
        End Class

        Public Class SendSMSSoftwareUserSecurityFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس مشخصات کاربر با خطا مواجه شد" + _Message
                End Get
            End Property
        End Class

        Public Class SendSMSATISMobileAppDownloadLinkFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس لینک دانلود اپلیکیشن آتیس موبایل با خطا مواجه شد" + _Message
                End Get
            End Property
        End Class

        Public Class SendSMSVerificationCodeFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال کد فعال سازی و یا تغییر رمز با خطا مواجه شد" + _Message
                End Get
            End Property
        End Class

        Public Class SoftWareUserNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربری با مشخصات مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class SendSMSWebSiteLinkFailedException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = vbCrLf + YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "ارسال اس ام اس لینک وب سایت با خطا مواجه شد" + _Message
                End Get
            End Property
        End Class

        Public Class UserHasAlreadyBeenAuthenticated
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.UserHasAlreadyBeenAuthenticated).MsgContent
                _MessageCode = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.UserHasAlreadyBeenAuthenticated).MsgId
            End Sub

        End Class


    End Namespace

    'BPTChanged
    Public Class R2CoreSoftwareUsersManager

        Private _DateTimeService As IR2DateTimeService
        Private _SoftwareUserService As ISoftwareUserService
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Public Sub New(YourDateTimeService As IR2DateTimeService, YourSoftwareUserService As ISoftwareUserService)
            _DateTimeService = YourDateTimeService
            _SoftwareUserService = YourSoftwareUserService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetUser(YourSoftWareUserMobile As R2CoreSoftwareUserMobile, YourImmediately As Boolean) As R2CoreSoftwareUser
            Try
                Dim InstanceEVA = New ExpressionValidationAlgorithmsManager
                InstanceEVA.ValidationMobileNumber(YourSoftWareUserMobile.SoftwareUserMobileNumber)
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select Top 1 * from R2Primary.dbo.TblSoftwareUsers Where MobileNumber='" & YourSoftWareUserMobile.SoftwareUserMobileNumber & "' Order By UserId Desc")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New UserNotExistByMobileNumberException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblSoftwareUsers Where MobileNumber='" & YourSoftWareUserMobile.SoftwareUserMobileNumber & "' Order By UserId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New UserNotExistByMobileNumberException
                    End If
                End If
                Return New R2CoreSoftwareUser With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .ApiKey = Ds.Tables(0).Rows(0).Item("ApiKey").trim, .APIKeyExpiration = Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .UserShenaseh = Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, .UserPassword = Ds.Tables(0).Rows(0).Item("UserPassword").trim, .UserPasswordExpiration = Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), .UserPinCode = Ds.Tables(0).Rows(0).Item("UserPinCode").trim, .UserCanCharge = Ds.Tables(0).Rows(0).Item("UserCanCharge"), .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId"), .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim}
            Catch exx As UserNotExistByMobileNumberException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetRawSoftwareUser(YourSoftwareUserId As Int64, YourImmediately As Boolean) As R2CoreRawSoftwareUserStructure
            Try
                Dim InstanceSMSOwners = New R2CoreSMSOwnersManager(_DateTimeService, _SoftwareUserService)
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select UserId,UserName,MobileNumber,UserTypeId,UserActive from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourSoftwareUserId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New UserIdNotExistException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourSoftwareUserId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New UserIdNotExistException
                End If
                Return New R2CoreRawSoftwareUserStructure With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim, .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId"), .SMSOwnerActive = InstanceSMSOwners.GetSMSOwnerCurrentState(YourSoftwareUserId, YourImmediately).IsSendingActive}
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUser(YourSoftwareUserId As Int64, YourImmediately As Boolean) As R2CoreSoftwareUser
            Try
                Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager(_DateTimeService)
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                         "Select UserId,ApiKey,APIKeyExpiration,UserName,UserShenaseh,UserPassword,UserPasswordExpiration,UserPinCode,UserCanCharge,UserActive,UserTypeId,MobileNumber from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourSoftwareUserId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New UserIdNotExistException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                          "Select UserId,ApiKey,APIKeyExpiration,UserName,UserShenaseh,UserPassword,UserPasswordExpiration,UserPinCode,UserCanCharge,UserActive,UserTypeId,MobileNumber from R2Primary.dbo.TblSoftwareUsers Where UserId=" & YourSoftwareUserId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New UserIdNotExistException
                    End If
                End If
                Return New R2CoreSoftwareUser With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .ApiKey = Ds.Tables(0).Rows(0).Item("ApiKey").trim, .APIKeyExpiration = Ds.Tables(0).Rows(0).Item("APIKeyExpiration").trim, .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .UserShenaseh = Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, .UserPassword = Ds.Tables(0).Rows(0).Item("UserPassword").trim, .UserPasswordExpiration = Ds.Tables(0).Rows(0).Item("UserPasswordExpiration").trim, .UserPinCode = Ds.Tables(0).Rows(0).Item("UserPinCode").trim, .UserCanCharge = Ds.Tables(0).Rows(0).Item("UserCanCharge"), .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId"), .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim}
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSoftwareUserExtended(YourSoftwareUser As R2CoreSoftwareUser, YourImmediately As Boolean) As R2CoreSoftwareUserExtended
            Try
                Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager(_DateTimeService)
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select SoftwareUsers.UserId,SoftwareUsers.ApiKey,SoftwareUsers.APIKeyExpiration,SoftwareUsers.UserName,SoftwareUsers.UserShenaseh,SoftwareUsers.UserPassword,SoftwareUsers.UserPasswordExpiration,SoftwareUsers.UserPinCode,SoftwareUsers.UserCanCharge,SoftwareUsers.UserActive,SoftwareUsers.UserTypeId,SoftwareUsers.MobileNumber,SoftwareUserTypes.UTTitle as SoftwareUserTypeTitle 
                         from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                            Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On  SoftwareUsers.UserTypeId = SoftwareUserTypes.UTId 
                         Where SoftwareUsers.UserId=" & YourSoftwareUser.UserId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New UserIdNotExistException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select SoftwareUsers.UserId,SoftwareUsers.ApiKey,SoftwareUsers.APIKeyExpiration,SoftwareUsers.UserName,SoftwareUsers.UserShenaseh,SoftwareUsers.UserPassword,SoftwareUsers.UserPasswordExpiration,SoftwareUsers.UserPinCode,SoftwareUsers.UserCanCharge,SoftwareUsers.UserActive,SoftwareUsers.UserTypeId,SoftwareUsers.MobileNumber,SoftwareUserTypes.UTTitle as SoftwareUserTypeTitle 
                         from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                            Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On  SoftwareUsers.UserTypeId = SoftwareUserTypes.UTId 
                         Where SoftwareUsers.UserId=" & YourSoftwareUser.UserId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New UserIdNotExistException
                    End If
                End If
                Return New R2CoreSoftwareUserExtended With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim, .SoftwareUserTypeTitle = Ds.Tables(0).Rows(0).Item("SoftwareUserTypeTitle").trim, .ApiKey = Ds.Tables(0).Rows(0).Item("ApiKey").trim, .APIKeyExpiration = Ds.Tables(0).Rows(0).Item("APIKeyExpiration").trim, .UserPassword = Ds.Tables(0).Rows(0).Item("UserPassword").trim, .UserPasswordExpiration = Ds.Tables(0).Rows(0).Item("UserPasswordExpiration").trim, .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserCanCharge = Ds.Tables(0).Rows(0).Item("UserCanCharge"), .UserPinCode = Ds.Tables(0).Rows(0).Item("UserPinCode").trim, .UserShenaseh = Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId")}
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUser(YourUserShenaseh As String, YourUserPassword As String) As R2CoreSoftwareUser
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblSoftwareUsers Where UserShenaseh='" & YourUserShenaseh.Trim() & "' and UserPassword='" & YourUserPassword.Trim() & "'", 300, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New SoftWareUserNotFoundException
                End If
                Return New R2CoreSoftwareUser With {.UserId = Ds.Tables(0).Rows(0).Item("UserId"), .ApiKey = Ds.Tables(0).Rows(0).Item("ApiKey").trim, .APIKeyExpiration = Ds.Tables(0).Rows(0).Item("APIKeyExpiration"), .UserName = Ds.Tables(0).Rows(0).Item("UserName").trim, .UserShenaseh = Ds.Tables(0).Rows(0).Item("UserShenaseh").trim, .UserPassword = Ds.Tables(0).Rows(0).Item("UserPassword").trim, .UserPasswordExpiration = Ds.Tables(0).Rows(0).Item("UserPasswordExpiration"), .UserPinCode = Ds.Tables(0).Rows(0).Item("UserPinCode"), .UserCanCharge = Ds.Tables(0).Rows(0).Item("UserCanCharge"), .UserActive = Ds.Tables(0).Rows(0).Item("UserActive"), .UserTypeId = Ds.Tables(0).Rows(0).Item("UserTypeId"), .MobileNumber = Ds.Tables(0).Rows(0).Item("MobileNumber").trim}
            Catch ex As SoftWareUserNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ConfirmUser(YourSessionId As String, YourUserShenaseh As String, YourUserPassword As String, YourCaptcha As String)
            Try
                Dim InstanceCacheKeys = New Caching.R2CoreCacheManager(_DateTimeService)
                Dim CachKey = InstanceCacheKeys.GetCacheType(Caching.R2CoreCacheTypes.Session).CacheTypeName + YourSessionId
                Dim CacheValue = DirectCast(InstanceCacheKeys.GetCache(CachKey, R2CoreCatchDataBases.SoftwareUserSessions), StackExchange.Redis.RedisValue)
                If CacheValue.IsNullOrEmpty Then Throw New SessionOverException
                Dim Content = JsonConvert.DeserializeObject(Of R2CoreStandardSessionCaptchaWordStructure)(CacheValue.ToString)
                If YourCaptcha = Content.Captcha Then
                    Dim SessionIdSoftwareUser = New R2CoreSessionIdSoftwareUser
                    SessionIdSoftwareUser.SessionId = YourSessionId
                    SessionIdSoftwareUser.SoftWareUser = GetUser(YourUserShenaseh, YourUserPassword)
                    InstanceCacheKeys.RemoveCache(CachKey, R2CoreCatchDataBases.SoftwareUserSessions)
                    InstanceCacheKeys.SetCache(CachKey, SessionIdSoftwareUser, R2CoreCacheTypes.Session, R2CoreCatchDataBases.SoftwareUserSessions, False)

                    'PubSubMessage-UserAuthenticated
                    Dim _Subscriber = RedisConnectorHelper.Connection.GetSubscriber()
                    _Subscriber.Publish(R2CorePubSubChannels.UserAuthenticated, JsonConvert.SerializeObject(SessionIdSoftwareUser.SoftWareUser))
                Else
                    Throw New CaptchaWordNotCorrectException
                End If
            Catch ex As SessionOverException
                Throw ex
            Catch ex As CaptchaWordNotCorrectException
                Throw ex
            Catch ex As SoftWareUserNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetSoftwareUserTypes() As String
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select UTId,UTTitle from R2Primary.dbo.TblSoftwareUserTypes Where Active=1 and ViewFlag=1 Order By UTId for json auto", 3600, Ds, New Boolean)
                Return Ds.Tables(0).Rows(0).Item(0)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Function GetaNewSoftwareUserPassword() As String
            Try
                Dim AES As New AESAlgorithmsManager
                Dim InstanceConfiguration As New R2CoreInstanceConfigurationManager(_DateTimeService)
                Return AES.GenerateRandomStringAlphabetic(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 15))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function RegisteringSoftwareUser(YourRawSoftwareUser As R2CoreRawSoftwareUserStructure, YourCreateMoneyWallet As Boolean, YourCreatorUserId As Int64) As Int64
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceSoftwareUser = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                Dim AES As New R2Core.SecurityAlgorithmsManagement.AESAlgorithms.AESAlgorithmsManager
                Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
                Dim InstanceConfiguration As New R2CoreInstanceConfigurationManager(_DateTimeService)
                Dim APIKeyExpiration As String = _DateTimeService.GetShamsiDateWithAddMonth(_DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 1))
                Dim UserPasswordExpiration As String = APIKeyExpiration
                Dim Shenaseh As String = Hasher.GenerateSHA256String(YourRawSoftwareUser.MobileNumber)
                Dim PasswordTemp = GetaNewSoftwareUserPassword()
                Dim Password As String = Hasher.GenerateSHA256String(PasswordTemp)
                Dim UIdSalt As String = AES.GetSalt(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 0))

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 UserId from R2Primary.dbo.TblSoftwareUsers with (tablockx) order by UserId desc"
                CmdSql.ExecuteNonQuery()
                Dim myUserId As Int64 = CmdSql.ExecuteScalar + 1
                Dim APIKey = Hasher.GenerateSHA256String(myUserId.ToString + UIdSalt)

                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblSoftwareUsers(UserId,ApiKey,APIKeyExpiration,UserName,UserShenaseh,UserPassword,UserPasswordExpiration,UserPinCode,UserCanCharge,UserActive,UserTypeId,MobileNumber,UserStatus,VerificationCode,VerificationCodeTimeStamp,VerificationCodeCount,Nonce,NonceTimeStamp,NonceCount,PersonalNonce,PersonalNonceTimeStamp,Captcha,CaptchaValid,UserCreatorId,DateTimeMilladi,DateShamsi,ViewFlag,Deleted)
                                      Values(" & myUserId & ",'" & APIKey & "','" & APIKeyExpiration & "','" & YourRawSoftwareUser.UserName & "','" & Shenaseh & "','" & Password & "','" & UserPasswordExpiration & "','',0,1," & YourRawSoftwareUser.UserTypeId & ",'" & YourRawSoftwareUser.MobileNumber & "','logout','','" & _DateTimeService.GetCurrentDateTimeMilladi & "',0,'','" & _DateTimeService.GetCurrentDateTimeMilladi & "','','" & _DateTimeService.GetCurrentDateTimeMilladi & "',0,'',0," & YourCreatorUserId & ",'" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentShamsiDate & "',1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                If YourCreateMoneyWallet Then
                    'ایجاد کیف پول کاربر
                    Dim InstanceMoneyWallet = New R2CoreMoneyWalletManager(_DateTimeService)
                    Dim MoneyWalletId = InstanceMoneyWallet.CreateNewMoneyWallet()
                    'ایجاد روابط
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Insert Into R2Primary.dbo.TblSoftwareUsersRelationMoneyWallet(UserId,CardId,RelationActive) Values(" & myUserId & "," & MoneyWalletId & ",1)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                End If

                'ارسال شناسه و رمز عبور
                Dim InstanceSMSHandling = New R2CoreSMSHandlerManager(_DateTimeService, _SoftwareUserService)
                InstanceSMSHandling.SendSMSFree(YourRawSoftwareUser.MobileNumber, New SMSCreationData With {.Data1 = YourRawSoftwareUser.MobileNumber, .Data2 = PasswordTemp}, R2Core.SMS.SMSTypes.R2CoreSMSTypes.SoftwareUserSecurityFree)

                Return myUserId
            Catch ex As SMSTypeIdNotFoundException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
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
        End Function

        Public Sub EditSoftwareUser(YourRawSoftwareUser As R2CoreRawSoftwareUserStructure)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserName='" & YourRawSoftwareUser.UserName & "',MobileNumber='" & YourRawSoftwareUser.MobileNumber & "',UserTypeId=" & YourRawSoftwareUser.UserTypeId & ",UserActive=" & IIf(YourRawSoftwareUser.UserActive, 1, 0) & " Where UserId=" & YourRawSoftwareUser.UserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function ResetSoftwareUserPassword(YourSoftwareUserId As Int64, YourUserId As Int64) As R2CoreSoftWareUserSecurity
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CorePermissionTypes.UserCanSendSoftwareUserShenasehPasswordViaSMS, YourUserId, 0) Then Throw New UserNotAllowedRunThisProccessException

                Dim Hasher = New R2Core.SecurityAlgorithmsManagement.Hashing.SHAHasher
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                Dim InstanceSoftwareUser = New R2CoreSoftwareUsersManager(_DateTimeService, _SoftwareUserService)

                Dim SoftwareUser = GetUser(YourSoftwareUserId, False)
                Dim newPassword As String = GetaNewSoftwareUserPassword()
                Dim UserPasswordExpiration As String = _DateTimeService.GetShamsiDateWithAddMonth(_DateTimeService.GetCurrentShamsiDate, InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 2))
                SoftwareUser.UserShenaseh = SoftwareUser.MobileNumber
                SoftwareUser.UserPassword = newPassword

                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblSoftwareUsers Set UserPassword='" & Hasher.GenerateSHA256String(newPassword) & "',UserPasswordExpiration='" & UserPasswordExpiration & "'  Where UserId=" & YourSoftwareUserId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()

                InstanceSoftwareUser.SendUserSecurity(SoftwareUser)
                Dim SoftWareUserSecurity = New R2CoreSoftWareUserSecurity
                SoftWareUserSecurity.UserShenaseh = SoftwareUser.MobileNumber
                SoftWareUserSecurity.UserPassword = newPassword
                Return SoftWareUserSecurity
            Catch ex As PermissionException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function SoftwareUserForgetPassword(YourSoftwareUserMobileNumber As String) As String
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceSoftwareUser = New R2CoreInstanseSoftwareUsersManager(_DateTimeService)
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                Dim InstanceSession = New R2CoreSessionManager

                Dim AES = New AESAlgorithmsManager
                Dim InstanceCacheKeys = New Caching.R2CoreCacheManager(_DateTimeService)
                Dim NSSSoftwareUser = InstanceSoftwareUser.GetNSSUserUnChangeable(New R2CoreSoftwareUserMobile(YourSoftwareUserMobileNumber))

                Dim SessionIdVerificationCode = New R2CoreSessionIdUserIdVerificationCode
                SessionIdVerificationCode.UserId = NSSSoftwareUser.UserId
                SessionIdVerificationCode.VerificationCode = AES.GenerateVerificationCode(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 9))
                SessionIdVerificationCode.SessionId = InstanceSession.GetNewSessionId
                Dim CachKey = InstanceCacheKeys.GetCacheType(R2CoreCacheTypes.SoftwareUserVerify).CacheTypeName + SessionIdVerificationCode.SessionId
                InstanceCacheKeys.RemoveCache(CachKey, R2CoreCatchDataBases.SoftwareUserSessions)
                InstanceCacheKeys.SetCache(CachKey, SessionIdVerificationCode, R2CoreCacheTypes.SoftwareUserVerify, R2CoreCatchDataBases.SoftwareUserSessions, False)

                SendSMSSoftwareUserVerificationCode(NSSSoftwareUser, SessionIdVerificationCode.VerificationCode)

                Return SessionIdVerificationCode.SessionId
            Catch ex As SessionOverException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub VerifySoftwareUserVerificationCode(YourSessionId As String, YourVerificationCode As String)
            Try
                Dim InstanceCache = New Caching.R2CoreCacheManager(_DateTimeService)

                Dim CachKey = InstanceCache.GetCacheType(Caching.R2CoreCacheTypes.SoftwareUserVerify).CacheTypeName + YourSessionId
                Dim CacheValue = DirectCast(InstanceCache.GetCache(CachKey, R2CoreCatchDataBases.SoftwareUserSessions), StackExchange.Redis.RedisValue)
                If CacheValue.IsNullOrEmpty Then Throw New SessionOverException

                Dim SessionIdUserIdVerificationCode = JsonConvert.DeserializeObject(Of R2CoreSessionIdUserIdVerificationCode)(CacheValue)
                If SessionIdUserIdVerificationCode.VerificationCode = YourVerificationCode Then
                    ResetSoftwareUserPassword(SessionIdUserIdVerificationCode.UserId, GetSystemUserId)
                End If
            Catch ex As SessionOverException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetSystemUserId() As Int64
            Try
                Return 1
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSystemUser() As R2CoreSoftwareUser
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select UserId from R2Primary.dbo.TblSoftwareUsers Where UserId=1", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New UserIdNotExistException
                End If
                Return GetUser(System.Convert.ToInt64(Ds.Tables(0).Rows(0).Item("UserId")), False)
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSoftwareUserWebProcessGroupAccess(YourSoftwareUserId As Int64, YourWPGId As Int64) As Boolean
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                               "Select Top 1 RelationActive from R2Primary.dbo.TblEntityRelations Where E1=" & YourSoftwareUserId & " and E2=" & YourWPGId & " and ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & " Order By ERId Desc", 0, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return DS.Tables(0).Rows(0).Item("RelationActive")
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetSoftwareUserWebProcessAccess(YourSoftwareUserId As Int64, YourWPId As Int64) As Boolean
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                               "Select Top 1 RelationActive from R2Primary.dbo.TblPermissions Where EntityIdFirst=" & YourSoftwareUserId & " And EntityIdSecond=" & YourWPId & " and PermissionTypeId=" & R2Core.PermissionManagement.R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & "Order By PId Desc", 0, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return DS.Tables(0).Rows(0).Item("RelationActive")
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ChangeSoftwareUserWebProcessGroupAccess(YourSoftwareUserId As Int64, YourWPGId As Int64, YourStatus As Boolean)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                If Not YourStatus Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourSoftwareUserId & " and E2=" & YourWPGId & " and ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & ""
                    CmdSql.ExecuteNonQuery()
                Else
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourSoftwareUserId & " and E2=" & YourWPGId & " and ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId, E1, E2, RelationActive) Values(" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & "," & YourSoftwareUserId & "," & YourWPGId & "," & IIf(YourStatus, 1, 0) & ")"
                    CmdSql.ExecuteNonQuery()
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Broken Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Broken Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ChangeSoftwareUserWebProcessAccess(YourSoftwareUserId As Int64, YourWPId As Int64, YourStatus As Boolean)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                If Not YourStatus Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblPermissions Set RelationActive=0 Where EntityIdFirst=" & YourSoftwareUserId & " and EntityIdSecond=" & YourWPId & " and PermissionTypeId=" & R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & ""
                    CmdSql.ExecuteNonQuery()
                Else
                    CmdSql.CommandText = "Update R2Primary.dbo.TblPermissions Set RelationActive=0 Where EntityIdFirst=" & YourSoftwareUserId & " and EntityIdSecond=" & YourWPId & " and PermissionTypeId=" & R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2Primary.dbo.TblPermissions(EntityIdFirst, EntityIdSecond, PermissionTypeId, RelationActive) Values(" & YourSoftwareUserId & "," & YourWPId & "," & R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & "," & IIf(YourStatus, 1, 0) & ")"
                    CmdSql.ExecuteNonQuery()
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Broken Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Broken Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SendSMSSoftwareUserVerificationCode(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourVerificationCode As String)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlingManager(_DateTimeService)
                Dim LstUser = New List(Of R2CoreStandardSoftwareUserStructure) From {YourNSSSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourVerificationCode}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreSMSTypes.ApplicationActivationCode, LstCreationData, False)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSVerificationCodeFailedException(SMSResultAnalyze)
            Catch ex As SendSMSVerificationCodeFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function IsExistSoftwareUser(YourSoftwareUserMobile As R2CoreSoftwareUserMobile, ByRef UserId As Int64, YourImmediately As Boolean) As Boolean
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSoftwareUserMobile.SoftwareUserMobileNumber)
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select UserId from R2Primary.dbo.TblSoftwareUsers where ltrim(rtrim(MobileNumber))='" & YourSoftwareUserMobile.SoftwareUserMobileNumber & "'")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then
                        Return False
                    Else
                        UserId = DS.Tables(0).Rows(0).Item("UserId")
                        Return True
                    End If
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                                "Select UserId from R2Primary.dbo.TblSoftwareUsers where ltrim(rtrim(MobileNumber))='" & YourSoftwareUserMobile.SoftwareUserMobileNumber & "'", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Return False
                    Else
                        UserId = DS.Tables(0).Rows(0).Item("UserId")
                        Return True
                    End If
                End If
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SendWebSiteLink(YourSoftwareUser As R2CoreSoftwareUser)
            Try
                Dim InstanceSMSHandler = New R2CoreSMSHandlerManager(_DateTimeService, _SoftwareUserService)
                Dim LstUser = New List(Of R2CoreSoftwareUser) From {YourSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = String.Empty}}
                Dim SMSResult = InstanceSMSHandler.SendSMS(LstUser, R2CoreSMSTypes.ATISMobileAppDownloadLink, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandler.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSWebSiteLinkFailedException(SMSResultAnalyze)
            Catch ex As SendSMSWebSiteLinkFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub SendUserSecurity(YourSoftwareUser As R2CoreSoftwareUser)
            Try
                Dim InstanceSMSHandling = New R2CoreSMSHandlerManager(_DateTimeService, _SoftwareUserService)
                Dim LstUser = New List(Of R2CoreSoftwareUser) From {YourSoftwareUser}
                Dim LstCreationData = New List(Of SMSCreationData) From {New SMSCreationData With {.Data1 = YourSoftwareUser.UserShenaseh, .Data2 = YourSoftwareUser.UserPassword}}
                Dim SMSResult = InstanceSMSHandling.SendSMS(LstUser, R2CoreSMSTypes.SoftwareUserSecurity, LstCreationData, True)
                Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                If Not SMSResultAnalyze = String.Empty Then Throw New SendSMSSoftwareUserSecurityFailedException(SMSResultAnalyze)
            Catch ex As SendSMSSoftwareUserSecurityFailedException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AuthenticationUserByPinCode(YourUser As R2CoreSoftwareUser)
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select UserActive from R2Primary.dbo.TblSoftwareUsers where UserPinCode='" & YourUser.UserPinCode & "'", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New UserNotExistException
                Else
                    If Convert.ToBoolean(DS.Tables(0).Rows(0).Item("UserActive")) = False Then Throw New UserIsNotActiveException
                End If
            Catch ex As UserIsNotActiveException
                Throw ex
            Catch ex As UserNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class


    'BPTChanged
    Public Class R2CoreSoftwareUser
        Inherits BaseStandardClass.R2StandardStructure
        Public Property UserId As Int64
        Public Property ApiKey As String
        Public Property APIKeyExpiration As String
        Public Property UserName As String
        Public Property UserShenaseh As String
        Public Property UserPassword As String
        Public Property UserPasswordExpiration As String
        Public Property UserPinCode As String
        Public Property UserCanCharge As Boolean
        Public Property UserActive As Boolean
        Public Property UserTypeId As Int64
        Public Property MobileNumber As String
    End Class

    'BPTChanged
    Public Class R2CoreSoftwareUserExtended
        Inherits R2CoreSoftwareUser
        Public Property SoftwareUserTypeTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreSoftwareUserComposedInf
        Public Property SoftwareUserExtended As R2CoreSoftwareUserExtended
        Public Property MoneyWallet As R2CoreMoneyWallet
    End Class

    'BPTChanged
    Public Class R2CoreSessionIdSoftwareUser
        Public Property SessionId As String
        Public Property SoftWareUser As R2CoreSoftwareUser
    End Class

    'BPTChanged
    Public Class R2CoreSessionIdUserIdVerificationCode
        Public Property SessionId As String
        Public Property UserId As Int64
        Public Property VerificationCode As String
    End Class

    'BPTChanged
    Public Class R2CoreRawSoftwareUserStructure
        Public UserId As Int64
        Public UserName As String
        Public MobileNumber As String
        Public UserTypeId As Int64
        Public UserActive As Boolean
        Public SMSOwnerActive As Boolean
        Public UserTypeTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreSoftWareUserSecurity
        Public UserShenaseh As String
        Public UserPassword As String
    End Class

    'BPTChanged
    Public Interface ISoftwareUserService
        ReadOnly Property UserId As Int64
        ReadOnly Property SystemUserId As Int64
    End Interface

    'BPTChanged
    Public Class SoftwareUserService
        Implements ISoftwareUserService

        Private _UserId As Int64
        Public Sub New(YourUserId As Int64)
            _UserId = YourUserId
        End Sub

        Public ReadOnly Property UserId As Long Implements ISoftwareUserService.UserId
            Get
                Return _UserId
            End Get
        End Property

        Public ReadOnly Property SystemUserId As Long Implements ISoftwareUserService.SystemUserId
            Get
                Return 1
            End Get
        End Property

    End Class



End Namespace
