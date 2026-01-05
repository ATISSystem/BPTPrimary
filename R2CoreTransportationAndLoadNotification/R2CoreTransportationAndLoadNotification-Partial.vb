Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Reflection
Imports System.Text
Imports System.Windows.Forms
Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.DriverSelfDeclaration.Exceptions
Imports R2CoreTransportationAndLoadNotification.FileShareRawGroupsManagement
Imports R2CoreTransportationAndLoadNotification.TruckLoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2Core.DateTimeProvider
Imports R2Core.SQLInjectionPrevention



Namespace TruckLoaderTypes

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationTruckLoaderTypeManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        Public Shared Function GetTonajMax(YourTruckLoaderName As String) As Int64
            Try
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 nTonaj from dbtransport.dbo.tbCarType Where strCarName='" & YourTruckLoaderName.Trim() & "'", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
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












