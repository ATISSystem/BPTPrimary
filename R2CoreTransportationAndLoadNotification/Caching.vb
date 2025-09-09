



Imports R2CoreParkingSystem.Caching

Namespace Caching

    Public MustInherit Class R2CoreTransportationAndLoadNotificationCatchDataBases
        Inherits R2CoreParkingSystemCatchDataBases

        Public Shared ReadOnly Property TruckDriverInformation As Int64 = 1
        Public Shared ReadOnly Property Loads As Int64 = 3

    End Class

    Public MustInherit Class R2CoreTransportationAndLoadNotificationCacheTypes
        Inherits R2CoreParkingSystemCacheTypes

        Public Shared ReadOnly Property TurnInfo = 2
        Public Shared ReadOnly Property Loads = 4
    End Class


End Namespace
