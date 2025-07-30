



Imports R2CoreParkingSystem.Caching

Namespace Caching

    Public MustInherit Class R2CoreTransportationAndLoadNotificationCatchDataBases
        Inherits R2CoreParkingSystemCatchDataBases

        Public Shared ReadOnly Property TruckDriverInformation As Int64 = 1
    End Class

    Public MustInherit Class R2CoreTransportationAndLoadNotificationCacheTypes
        Inherits R2CoreParkingSystemCacheTypes

        Public Shared ReadOnly Property TruckDriverTurnInfo = 2
    End Class


End Namespace
