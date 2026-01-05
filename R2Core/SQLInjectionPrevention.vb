


Imports R2Core.ConfigurationManagement
Imports R2Core.DateTimeProvider
Imports R2Core.GeneralConfiguration
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports System.Reflection

Namespace SQLInjectionPrevention

    Public Class R2CoreSQLInjectionPreventionManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
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



End Namespace
