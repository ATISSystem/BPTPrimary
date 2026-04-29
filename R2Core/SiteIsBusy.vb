


Imports R2Core.DateTimeProvider

Namespace SiteIsBusy

    Public Class R2CoreSiteIsBusyManager

        Private _DateTimeService As New R2DateTimeService

        'Public Sub ActivateSiteIsBusy()
        '    Try
        '        R2CoreMClassConfigurationManagement.SetConfiguration(R2CoreConfigurations.SiteIsBusy, 0, "1")
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        'Public Sub DeActivateSiteIsBusy()
        '    Try
        '        R2CoreMClassConfigurationManagement.SetConfiguration(R2CoreConfigurations.SiteIsBusy, 0, "0")
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        'Public Function GetSiteIsBusy() As Boolean
        '    Try
        '        Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
        '        Dim SiteIsBusy As Boolean = InstanceConfiguration.GetConfig(R2CoreConfigurations.SiteIsBusy, 0, 60)
        '        If SiteIsBusy Then Return True Else Return False
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Sub SiteIsBusy()
        '    Try
        '        Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
        '        Dim mySiteIsBusy As Boolean = InstanceConfiguration.GetConfig(R2CoreConfigurations.SiteIsBusy, 0, 60)
        '        If mySiteIsBusy Then Throw New R2CoreSiteIsBusyException
        '    Catch ex As R2CoreSiteIsBusyException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

    End Class

    Namespace Exceptions
        Public Class R2CoreSiteIsBusyException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "لحظاتی منتظر بمانید"
                End Get
            End Property
        End Class

    End Namespace

End Namespace
