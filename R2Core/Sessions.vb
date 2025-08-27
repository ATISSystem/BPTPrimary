



Imports Newtonsoft.Json
Imports R2Core.Caching
Imports R2Core.ConfigurationManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.AESAlgorithms
Imports R2Core.SecurityAlgorithmsManagement.Captcha
Imports R2Core.SecurityAlgorithmsManagement.Hashing
Imports R2Core.SoftwareUserManagement
Imports System.Reflection
Imports System.Runtime.InteropServices.WindowsRuntime

Namespace SessionManagement


    Public Class R2CoreStandardSessionCaptchaBitMapStructure

        Public Sub New()
            MyBase.New()
            _SessionId = String.Empty
            _Captcha = New Byte() {}
        End Sub

        Public Sub New(YourSessionId As String, YourCaptcha As Byte())
            _SessionId = YourSessionId
            _Captcha = YourCaptcha
        End Sub

        Public Property SessionId As String
        Public Property Captcha As Byte()

    End Class

    Public Class R2CoreStandardSessionCaptchaWordStructure

        Public Sub New()
            MyBase.New()
            _SessionId = String.Empty
            _Captcha = String.Empty
        End Sub

        Public Sub New(YourSessionId As String, YourCaptcha As String)
            _SessionId = YourSessionId
            _Captcha = YourCaptcha
        End Sub

        Public Property SessionId As String
        Public Property Captcha As String

    End Class

    Public Class R2CoreSessionManager
        Private _DateTime As New R2DateTime

        Public Function GetNewSessionId() As String
            Try
                Dim InstanceAESAlgorithms = New AESAlgorithmsManager
                Dim InstanceMD5Hasher = New MD5Hasher
                Dim Instance = New R2Core.DateAndTimeManagement.R2DateTime
                Dim SessionId = InstanceMD5Hasher.GenerateMD5String(_DateTime.GetCurrentDateTimeMilladi) + InstanceAESAlgorithms.GetSalt(12)
                Return SessionId
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function StartSession() As R2CoreStandardSessionCaptchaBitMapStructure
            Try
                Dim InstanceCaptcha = New R2CoreInstanceCaptchaManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceCache = New R2CoreCacheManager
                'Dim CaptchaWord = InstanceCaptcha.GenerateFakeWordNumeric(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))

                Dim CaptchaWord = "ABCDE"

                Dim CaptchaBitMap = InstanceCaptcha.GetCaptcha(CaptchaWord)
                Dim SessionId = GetNewSessionId()
                InstanceCache.SetCache(InstanceCache.GetCacheType(R2CoreCacheTypes.Session).CacheTypeName + SessionId, New R2CoreStandardSessionCaptchaWordStructure(SessionId, CaptchaWord), R2CoreCacheTypes.Session, R2CoreCatchDataBases.SoftwareUserSessions, False)
                Return New R2CoreStandardSessionCaptchaBitMapStructure(SessionId, CaptchaBitMap)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ConfirmSession(YourSessionId As String) As R2CoreSoftwareUser
            Try
                Dim InstanceCache = New Caching.R2CoreCacheManager
                Dim CachKey = InstanceCache.GetCacheType(Caching.R2CoreCacheTypes.Session).CacheTypeName + YourSessionId
                Dim Value = InstanceCache.GetCache(CachKey, R2CoreCatchDataBases.SoftwareUserSessions).ToString
                If Value Is Nothing Then Throw New SessionOverException
                Dim Content = JsonConvert.DeserializeObject(Of R2CoreSessionIdSoftwareUser)(Value)
                If Content.SoftWareUser Is Nothing Then Throw New SessionOverException
                Return Content.SoftWareUser
            Catch ex As SessionOverException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public Class SessionOverException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.SessionOverException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.SessionOverException).MsgId
        End Sub
    End Class


End Namespace
