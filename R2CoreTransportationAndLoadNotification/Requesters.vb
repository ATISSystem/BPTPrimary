


Imports R2CoreParkingSystem.RequesterManagement

Namespace RequesterManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationRequesters
        Inherits R2CoreParkingSystemRequesters

        Public Shared ReadOnly TurnRegisterRequestController_RealTimeTurnRegisterRequest As Int64 = 1
        Public Shared ReadOnly TurnRegisterRequestController_EmergencyTurnRegisterRequest As Int64 = 2
        Public Shared ReadOnly TurnRegisterRequestController_ResuscitationReserveTurn As Int64 = 3
        Public Shared ReadOnly TurnRegisterRequestController_ReserveTurnRegisterRequest As Int64 = 4

    End Class



End Namespace
