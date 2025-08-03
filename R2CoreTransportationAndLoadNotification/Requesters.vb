


Imports R2CoreParkingSystem.RequesterManagement

Namespace RequesterManagement

    Public MustInherit Class R2CoreTransportationAndLoadNotificationRequesters
        Inherits R2CoreParkingSystemRequesters

        Public Shared ReadOnly TurnRegisterRequestController_RealTimeTurnRegisterRequest As Int64 = 1
        Public Shared ReadOnly TurnRegisterRequestController_EmergencyTurnRegisterRequest As Int64 = 2
        Public Shared ReadOnly TurnRegisterRequestController_ResuscitationReserveTurn As Int64 = 3
        Public Shared ReadOnly TurnRegisterRequestController_ReserveTurnRegisterRequest As Int64 = 4
        Public Shared ReadOnly LoadCapacitorController_GetLoadsforTruckDriver As Int64 = 5
        Public Shared ReadOnly LoadAllocationController_LoadAllocationRegisteringforTruckDriver As Int64 = 6
        Public Shared ReadOnly LoadAllocationController_LoadAllocationRegisteringforTransportCompany As Int64 = 7

    End Class



End Namespace
