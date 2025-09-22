


Imports R2Core.ConfigurationManagement
Imports R2Core.DateTimeProvider
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
                Dim InstanceConfiguration = New R2CoreConfigurationsManager(_DateTimeService)
                Dim SqlInjectionPreventionKeywords = Split(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SqlInjectionPrevention, 0), " ")
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
