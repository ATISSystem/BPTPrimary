

Imports R2CoreParkingSystem.Logging

Namespace Logging

    'BPTChanged
    Public MustInherit Class PayanehClassLibraryLogType
        Inherits R2CoreTransportationAndLoadNotification.Logging.R2CoreTransportationAndLoadNotificationLogTypes

        Public Shared ReadOnly Property AutomaticTurnRegistering As Int64 = 1
        Public Shared ReadOnly Property TurnsCancellation As Int64 = 2
    End Class

End Namespace
