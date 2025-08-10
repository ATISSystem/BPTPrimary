

Imports System.Reflection
Imports PayanehClassLibrary.CarTruckNobatManagement.Exceptions
Imports PayanehClassLibrary.DriverTrucksManagement.Exceptions
Imports PayanehClassLibrary.Turns
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions

Namespace LoadAllocations

    Public Class PayanehClassLibraryLoadAllocationsManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Sub LoadAllocationRegisteringforTransportCompany(YourTruckId As Int64, YourTruckDriverId As Int64, YourLoadId As Int64, YourSoftwareUser As R2CoreSoftwareUser, YourRequesterId As Int64)
            Try
                Dim InstanceLoadAllocation = New R2CoreTransportationAndLoadNotificationLoadAllocationManager(_DateTimeService)
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager()
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager

                Dim Load = InstanceLoad.GetLoadSimpleModel(YourLoadId, False)
                Dim SeqTId As Int64
                Dim TurnId As Int64
                Try
                    Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_DateTimeService)
                    Dim Turn = InstanceTurns.GetLastActiveTurnfromTruckId(YourTruckId, False)
                    TurnId = Turn.TurnId
                    SeqTId = InstanceSequentialTurns.GetSequentialTurnIdfromTurn(Turn)
                Catch ex As TurnNotFoundException
                    Dim InstanceTurn = New PayanehClassLibraryTurnManager(_DateTimeService)
                    SeqTId = InstanceSequentialTurns.GetSequentialTurnIdByAnnouncementSGId(Load.AnnouncementSubGroupId)
                    TurnId = InstanceTurn.GetTurnofKiosk(SeqTId, YourTruckId, YourTruckDriverId, YourRequesterId, YourSoftwareUser)
                Catch ex As Exception
                    Throw ex
                End Try

                InstanceLoadAllocation.LoadAllocationRegistering(YourLoadId, TurnId, SeqTId, InstanceTruckNativeness.GetTruckNativeness(YourTruckId, True).TruckNativenessTypeId, TurnStatuses.Registered, YourRequesterId, YourSoftwareUser)

            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As LoadNotFoundException
                Throw ex
            Catch ex As SequentialTurnByAnnouncementSGIdNotFoundException
                Throw ex
            Catch ex As TurnNotFoundException
                Throw ex
            Catch ex As SoftwareUserMoneyWalletNotFoundException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
                                OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
                                OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
                                OrElse TypeOf ex Is CarIsNotPresentInParkingException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
                                OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
                                OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
                                OrElse TypeOf ex Is GetNobatException _
                                OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException _
                                OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                                OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                                OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException _
                                OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
                                OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

End Namespace


