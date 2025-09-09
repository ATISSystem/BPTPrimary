

Imports R2CoreParkingSystem.PubSubMessages



'BPTChanged
Namespace PubSubMessaging

    Public MustInherit Class R2CoreTransportationAndLoadNotificationPubSubChannels
        Inherits R2CoreParkingSystemPubSubChannels

        Public Shared ReadOnly Property TurnInfo As String = "TurnInfo"
        Public Shared ReadOnly Property LoadsForTruckDrivers As String = "LoadsForTruckDrivers"

    End Class


End Namespace
