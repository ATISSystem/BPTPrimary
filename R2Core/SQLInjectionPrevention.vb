


Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports System.Reflection
Imports System.Web.Services.Protocols

Namespace SQLInjectionPrevention

    Public Class R2CoreSQLInjectionPreventionManager

        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Sub GeneralAuthorization(YourParam As String)
            Try
                If YourParam.Trim = String.Empty Then Return
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                Dim SqlInjectionPreventionKeywords = Split(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.SqlInjectionPrevention, 0), " ")
                Dim Wanted = YourParam.ToLower().Split(" ")
                For Each Str As String In Wanted
                    If SqlInjectionPreventionKeywords.Any(Function(s) Str.Equals(s)) Or (New String() {";"}).Any(Function(s) Str.Equals(s)) Then
                        Throw New SqlInjectionException
                    End If
                Next
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
    End Class

    Public Class SqlInjectionException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.SqlInjectionException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.SqlInjectionException).MsgId
        End Sub
    End Class

End Namespace
