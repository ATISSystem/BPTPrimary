



Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports System.Reflection

Namespace ConfigurationManagement

    'BPTChanged
    Public MustInherit Class PayanehClassLibraryConfigurations
        Inherits R2CoreTransportationAndLoadNotificationConfigurations

        Public Shared ReadOnly Property Clock4 As Int64 = 22
        Public Shared ReadOnly Property SalonFingerPrint As Int64 = 26
        Public Shared ReadOnly Property TariffsPayaneh As Int64 = 31
        Public Shared ReadOnly Property ElamBarMonitoringInterval As Int64 = 33
        Public Shared ReadOnly Property TariffsPayanehKiosk As Int64 = 53
        Public Shared ReadOnly Property PayanehAmirKabirAutomatedJobsSetting As Int64 = 64
        Public Shared ReadOnly Property TruckersAssociationControllingMoneyWallet As Int64 = 75
        Public Shared ReadOnly Property DriversAssociationControllingMoneyWallet As Int64 = 99
    End Class

End Namespace
