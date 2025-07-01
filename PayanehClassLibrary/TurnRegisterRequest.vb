
Imports System.Reflection


Imports PayanehClassLibrary.CarTruckNobatManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.ExceptionManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.EntityRelationManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.EntityRelations
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports PayanehClassLibrary.DriverTrucksManagement.Exceptions
Imports R2Core.PermissionManagement
Imports R2CoreTransportationAndLoadNotification.PermissionManagement
Imports PayanehClassLibrary.TurnRegisterRequest.Exceptions
Imports PayanehClassLibrary.CarTruckNobatManagement.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports PayanehClassLibrary.Turns
Imports R2CoreTransportationAndLoadNotification.TurnRegisterRequest
Imports R2Core.DatabaseManagement


Namespace TurnRegisterRequest

    Public NotInheritable Class PayanehClassLibraryMClassTurnRegisterRequestManager
        Private _DateTime As New R2DateTime

        'Public Sub RealTimeTurnRegisterRequestWithLicensePlate(YourLPPelak As String, YourLPSerial As String, YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourRequesterId As Int64, YourTurnType As TurnType)
        '    Try
        '        Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
        '        InstanceSQLInjectionPrevention.GeneralAuthorization(YourLPPelak)
        '        InstanceSQLInjectionPrevention.GeneralAuthorization(YourLPSerial)

        '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.SoftwareUserCanSendRealTimeTurnRegisteringRequestWithLicensePlate, YourNSSSoftwareUser.UserId, 0) Then Throw New PermissionException

        '        Dim InstanceCarTrucks = New PayanehClassLibraryMClassCarTrucksManager
        '        Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
        '        Dim InstanceTurnRegisterRequest = New PayanehClassLibraryMClassTurnRegisterRequestManager
        '        Dim NSSTruck = New R2CoreTransportationAndLoadNotificationStandardTruckStructure(New R2StandardCarStructure(Nothing, Nothing, YourLPPelak, YourLPSerial, Nothing), Nothing)
        '        Dim TruckId As Int64 = Nothing
        '        If InstanceCarTrucks.IsExistCarTruckWithLicensePlate(New R2StandardCarTruckStructure(NSSTruck.NSSCar, Nothing), TruckId) Then
        '            NSSTruck = InstanceTrucks.GetNSSTruck(TruckId)
        '            LoadNotificationLoadPermissionManagement.DoControlforTruckPresentInParkingAndLastLoadPermission(NSSTruck)
        '            Dim TurnId As Int64
        '            InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(New R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, NSSTruck, False, True, TurnId, YourRequesterId, YourTurnType, YourNSSSoftwareUser, False)
        '        Else
        '            Throw New TruckNotFoundException
        '        End If
        '    Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
        '                        OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
        '                        OrElse TypeOf ex Is TerraficCardNotFoundException _
        '                        OrElse TypeOf ex Is DriverTruckInformationNotExistException _
        '                        OrElse TypeOf ex Is SqlInjectionException _
        '                        OrElse TypeOf ex Is PermissionException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        Public Function IsMoneyWalletInventoryIsEnoughForTurnRegistering(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Boolean
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemInstanceMoneyWalletManager
                Dim InstanceCarTruckNobat = New PayanehClassLibraryMClassCarTruckNobatManager
                If InstanceMoneyWallet.GetMoneyWalletCharge(YourNSSTrafficCard) < InstanceCarTruckNobat.GetHazinehSodoorNobat(YourNSSTrafficCard) Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function RealTimeTurnRegisterRequest(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourTurnPrintRedirect As Boolean, YourMoneyWalletInventoryControl As Boolean, ByRef YourTurnId As Int64, YourRequesterId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourTWSForce As Boolean) As Int64
        '    Try
        '        Dim InstanceTrafficCard = New R2CoreParkingSystemInstanceTrafficCardsManager
        '        Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        '        Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
        '        Dim NSSTrafficCard = InstanceTrafficCard.GetNSSTrafficCard(InstanceCars.GetCardIdFromnIdCar(YourNSSTruck.NSSCar.nIdCar))
        '        'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
        '        If YourMoneyWalletInventoryControl Then If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(NSSTrafficCard) Then Throw New MoneyWalletCurrentChargeNotEnoughException
        '        'ثبت درخواست صدور نوبت بلادرنگ
        '        Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure(Nothing, TurnRegisterRequestTypes.RealTime, YourNSSTruck.NSSCar.nIdCar, InstanceTurnRegisterRequest.GetNSSTurnRegisterRequestType(TurnRegisterRequestTypes.RealTime).TRRTypeTitle, Nothing, Nothing, Nothing, Nothing), Nothing, YourUserNSS)
        '        'ثبت نوبت
        '        Dim Strategy = New RealTimeTurnRegisteringStrategy(YourNSSSeqT, NSSTrafficCard, InstanceTurnRegisterRequest.GetNSSTurnRegisterRequest(TurnRegisterRequestId), YourUserNSS, YourRequesterId, YourTurnType, YourTWSForce)
        '        Strategy.DoStrategy()
        '        YourTurnId = Strategy.GetNSSTurn.nEnterExitId
        '        'ثبت رابطه نوبت با درخواست صدور نوبت از طریق فضانام مدیریت روابط نهادی
        '        Dim InstanceEntityRelation = New R2CoreMClassEntityRelationManager
        '        InstanceEntityRelation.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest, YourTurnId, TurnRegisterRequestId, Nothing), RelationDeactiveTypes.BothDeactive)
        '        'درخواست چاپ نوبت
        '        'Dim InstanceTurnPrintRequest = New R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManager
        '        'InstanceTurnPrintRequest.NoCostTurnPrintRequest(YourTurnId, YourTurnPrintRedirect)

        '        Return TurnRegisterRequestId
        '    Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
        '                        OrElse TypeOf ex Is DriverTruckInformationNotExistException _
        '                        OrElse TypeOf ex Is RelatedTerraficCardNotFoundException _
        '                        OrElse TypeOf ex Is LoadCapacitorLoadNotFoundException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function EmergencyTurnRegisterRequest(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourMoneyWalletInventoryControl As Boolean, YourEmergencyTurnRegisterRequestNote As String, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
        '    Try
        '        Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
        '        Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
        '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRequestEmergencyTurnRegistering, YourUserNSS.UserId, 0) Then Throw New UserCanNotRequestEmergencyTurnRegisteringException
        '        Dim NSSTrafficCard = InstanceTrafficCards.GetNSSTrafficCard(InstanceCars.GetCardIdFromnIdCar(YourNSSTruck.NSSCar.nIdCar))
        '        'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
        '        If YourMoneyWalletInventoryControl Then If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(NSSTrafficCard) Then Throw New MoneyWalletCurrentChargeNotEnoughException
        '        'ثبت درخواست صدور نوبت اضطراری
        '        Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        '        Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure(Nothing, TurnRegisterRequestTypes.Emergency, YourNSSTruck.NSSCar.nIdCar, InstanceTurnRegisterRequest.GetNSSTurnRegisterRequestType(TurnRegisterRequestTypes.Emergency).TRRTypeTitle & " " & YourEmergencyTurnRegisterRequestNote, Nothing, Nothing, Nothing, Nothing), Nothing, YourUserNSS)
        '        'ارسال پیام تایید درخواست صدور نوبت اضطراری
        '        Dim DataStruct As DataStruct = New DataStruct()
        '        DataStruct.Data1 = YourNSSTruck.NSSCar.nIdCar
        '        DataStruct.Data2 = TurnRegisterRequestId
        '        Dim InstanceComputerMessages = New R2CoreMClassComputerMessagesManager
        '        InstanceComputerMessages.SendComputerMessage(New R2StandardComputerMessageStructure(Nothing, YourNSSTruck.NSSCar.GetCarPelakSerialComposit() + " " + YourEmergencyTurnRegisterRequestNote, R2CoreTransportationAndLoadNotificationComputerMessageTypes.EmergencyTurnRegisterRequestConfirmation, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, DataStruct))
        '        Return TurnRegisterRequestId
        '    Catch ex As Exception When TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '        OrElse TypeOf ex Is UserCanNotRequestEmergencyTurnRegisteringException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Note YourDataStruct: Data1=nCarId Data2=TurnRegisterRequestId
        'Public Sub EmergencyTurnRegister(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourDataStruct As DataStruct, YourTurnPrintRedirect As Boolean, YourRequesterId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure, YourTWSForce As Boolean)
        '    Try
        '        Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
        '        Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
        '        Dim NSSTrafficCard = InstanceTrafficCards.GetNSSTrafficCard(InstanceCars.GetCardIdFromnIdCar(YourDataStruct.Data1))
        '        'ثبت نوبت
        '        Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        '        Dim Strategy = New RealTimeTurnRegisteringStrategy(YourNSSSeqT, NSSTrafficCard, InstanceTurnRegisterRequest.GetNSSTurnRegisterRequest(YourDataStruct.Data2), YourUserNSS, YourRequesterId, YourTurnType, YourTWSForce)
        '        Strategy.DoStrategy()
        '        Dim TurnId = Strategy.GetNSSTurn.nEnterExitId
        '        'ثبت رابطه نوبت با درخواست صدور نوبت از طریق فضانام مدیریت روابط نهادی
        '        Dim InstanceEntityRelation = New R2CoreMClassEntityRelationManager
        '        InstanceEntityRelation.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest, TurnId, YourDataStruct.Data2, Nothing), RelationDeactiveTypes.BothDeactive)
        '        'درخواست چاپ نوبت
        '        Dim InstanceTurnPrintRequest = New R2CoreTransportationAndLoadNotificationMClassTurnPrintRequestManager
        '        InstanceTurnPrintRequest.NoCostTurnPrintRequest(TurnId, YourTurnPrintRedirect)
        '    Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is TurnPrintingInfNotFoundException _
        '                        OrElse TypeOf ex Is DriverTruckInformationNotExistException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

        'Public Function ReserveTurnRegisterRequest(YourComputerId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
        '    Try
        '        Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
        '        'کنترل مجوز
        '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRequestReserveTurnRegistering, YourUserNSS.UserId, 0) Then Throw New UserCanNotRequestReserveTurnRegisteringException
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.ComputerCanRequestReserveTurnRegistering, YourComputerId, 0) Then Throw New ComputerCanNotRequestReserveTurnRegisteringException
        '        'ثبت درخواست صدور نوبت رزرو
        '        Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        '        Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure(Nothing, TurnRegisterRequestTypes.Reserve, InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 1), InstanceTurnRegisterRequest.GetNSSTurnRegisterRequestType(TurnRegisterRequestTypes.Reserve).TRRTypeTitle, Nothing, Nothing, Nothing, Nothing), Nothing, YourUserNSS)
        '        'ثبت نوبت
        '        Dim InstanceCarTruckNobat = New PayanehClassLibraryMClassCarTruckNobatManager
        '        Dim TurnId = PayanehClassLibraryMClassCarTruckNobatManagement.GetReserveTurn(TurnRegisterRequestId, YourTurnType, YourUserNSS)
        '        'ثبت رابطه نوبت با درخواست صدور نوبت از طریق فضانام مدیریت روابط نهادی
        '        R2CoreMClassEntityRelationManagement.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest, TurnId, TurnRegisterRequestId, Nothing), RelationDeactiveTypes.BothDeactive)

        '        Return TurnRegisterRequestId
        '    Catch ex As Exception When TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
        '                        OrElse TypeOf ex Is UserCanNotRequestReserveTurnRegisteringException _
        '                        OrElse TypeOf ex Is ComputerCanNotRequestReserveTurnRegisteringException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException

        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function ResuscitationReserveTurn(YourNSSSeqT As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure, YourTurnRegisterRequestId As Int64, YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourMoneyWalletInventoryControl As Boolean, YourRequesterId As Int64, YourTurnType As TurnType, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
        '    Try
        '        Dim InstancePermissions = New R2CoreInstansePermissionsManager
        '        If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanResuscitationReserveTurn, YourUserNSS.UserId, 0) Then Throw New UserCanNotResuscitationReserveTurnException

        '        Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        '        Dim NSSTRR = InstanceTurnRegisterRequest.GetNSSTurnRegisterRequest(YourTurnRegisterRequestId)
        '        If NSSTRR.TRRTypeId <> TurnRegisterRequestTypes.Reserve Then Throw New TurnRegisteringRequestIdIsNotaReserveTypeException
        '        If DateDiff(DateInterval.Hour, NSSTRR.DateTimeMilladi, _DateTime.GetCurrentDateTimeMilladi) > InstanceTurnRegisterRequest.GetNSSTurnRegisterRequestType(NSSTRR.TRRTypeId).TurnExpirationHours Then Throw New TurnRegisteringRequestDateTimeExpiredException

        '        Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager
        '        Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager
        '        Dim NSSTrafficCard = InstanceTrafficCards.GetNSSTrafficCard(InstanceCars.GetCardIdFromnIdCar(YourNSSTruck.NSSCar.nIdCar))

        '        'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
        '        If YourMoneyWalletInventoryControl Then If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(NSSTrafficCard) Then Throw New MoneyWalletCurrentChargeNotEnoughException

        '        'ثبت نوبت
        '        Dim Strategy = New ReserveTurnRegisteringStrategy(YourNSSSeqT, NSSTrafficCard, InstanceTurnRegisterRequest.GetNSSTurnRegisterRequest(YourTurnRegisterRequestId), YourUserNSS, YourRequesterId, YourTurnType, False)
        '        Strategy.DoStrategy()
        '        Return Strategy.GetNSSTurn.nEnterExitId
        '    Catch ex As Exception When TypeOf ex Is RequesterNotAllowTurnIssueBySeqTException _
        '                        OrElse TypeOf ex Is RequesterNotAllowTurnIssueByLastLoadPermissionedException _
        '                        OrElse TypeOf ex Is TruckRelatedSequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is CarIsNotPresentInParkingException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckIsTankTreiler _
        '                        OrElse TypeOf ex Is CarTruckTravelLengthNotOverYetException _
        '                        OrElse TypeOf ex Is GetNobatExceptionCarTruckHasNobat _
        '                        OrElse TypeOf ex Is GetNobatException _
        '                        OrElse TypeOf ex Is SequentialTurnIsNotActiveException _
        '                        OrElse TypeOf ex Is TruckNotFoundException _
        '                        OrElse TypeOf ex Is SequentialTurnNotFoundException _
        '                        OrElse TypeOf ex Is TruckDriverNotFoundException _
        '                        OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
        '                        OrElse TypeOf ex Is GetNSSException _
        '                        OrElse TypeOf ex Is GetDataException _
        '                        OrElse TypeOf ex Is TurnRegisteringRequestIdIsNotaReserveTypeException _
        '                        OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
        '                        OrElse TypeOf ex Is TurnNotFoundException _
        '                        OrElse TypeOf ex Is ReserveTurnAlreadyUsedException _
        '                        OrElse TypeOf ex Is TurnRegisteringRequestDateTimeExpiredException _
        '                        OrElse TypeOf ex Is UserCanNotResuscitationReserveTurnException _
        '                        OrElse TypeOf ex Is DriverTruckInformationNotExistException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function


    End Class

    Namespace Exceptions
        Public Class UserCanNotResuscitationReserveTurnException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربر مجوز احیاء نوبت رزرو را ندارد"
                End Get
            End Property
        End Class

        Public Class UserCanNotRequestEmergencyTurnRegisteringException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربر مجوز درخواست صدور نوبت اضطراری ندارد"
                End Get
            End Property
        End Class


        Public Class UserCanNotRequestReserveTurnRegisteringException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کاربر مجوز درخواست صدور نوبت رزرو را ندارد"
                End Get
            End Property
        End Class

        Public Class RequesterCanNotRequestReserveTurnRegisteringException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "از این طریق امکان درخواست صدور نوبت رزرو وجود ندارد"
                End Get
            End Property
        End Class

        Public Class TurnRegisteringRequestIdIsNotaReserveTypeException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شماره درخواست ، نوعی از درخواست صدور نوبت رزرو نیست"
                End Get
            End Property
        End Class

        Public Class TurnRegisteringRequestDateTimeExpiredException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زمان اعتبار نوبت رزرو به اتمام رسیده است"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public Class PayanehClassLibraryTurnRegisterRequestManager

        Private _R2DateTimeService As IR2DateTimeService
        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Function IsMoneyWalletInventoryIsEnoughForTurnRegistering(YourTruckId As Int64, YourSequentialTurnId As Int64) As Boolean
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_R2DateTimeService)
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)
                Dim TurnCost = InstanceTurns.GetTurnCost(YourSequentialTurnId, False)
                If InstanceMoneyWallet.GetMoneyWalletCharge(InstanceMoneyWallet.GetMoneyWalletfromCarId(YourTruckId, False).MoneyWalletId) < TurnCost.TotalCost Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function RealTimeTurnRegisterRequest(YourSeqTId As Int64, YourTruckId As Int64, ByRef YourTurnId As Int64, YourRequesterId As Int64, YourTurnType As TurnType, YourSoftwareUserId As Int64) As Int64
            Try
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
                If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(YourTruckId, YourSeqTId) Then Throw New MoneyWalletCurrentChargeNotEnoughException
                'ثبت درخواست صدور نوبت بلادرنگ
                Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationTurnRegisterRequest With {.TRRId = Nothing, .TRRTypeId = TurnRegisterRequestTypes.RealTime, .TruckId = YourTruckId, .Description = InstanceTurnRegisterRequest.GetTurnRegisterRequestType(TurnRegisterRequestTypes.RealTime).TRRTypeTitle, .UserId = YourSoftwareUserId, .RequesterId = YourRequesterId, .DateTimeMilladi = Nothing, .DateShamsi = Nothing, .Time = Nothing}, Nothing, YourSoftwareUserId, YourRequesterId)
                'ثبت نوبت
                Dim Strategy = New Turns.RealTimeTurnRegisteringStrategy(YourSeqTId, YourTruckId, TurnRegisterRequestId, YourSoftwareUserId, YourRequesterId, YourTurnType)
                Strategy.DoStrategy()
                YourTurnId = Strategy.GetTurnId
                'ثبت رابطه نوبت با درخواست صدور نوبت از طریق فضانام مدیریت روابط نهادی
                Dim InstanceEntityRelation = New R2CoreMClassEntityRelationManager
                InstanceEntityRelation.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest, YourTurnId, TurnRegisterRequestId, Nothing), RelationDeactiveTypes.BothDeactive)

                Return TurnRegisterRequestId
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
        End Function

        Public Function ReserveTurnRegisterRequest(YourRequesterId As Int64, YourTurnType As TurnType, YourSoftwareUserId As Int64) As Int64
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceTurns = New R2CoreTransportationAndLoadNotificationTurnsManager(_R2DateTimeService)
                'کنترل مجوز
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRequestReserveTurnRegistering, YourSoftwareUserId, 0) Then Throw New UserCanNotRequestReserveTurnRegisteringException
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.RequesterCanRequestReserveTurnRegistering, YourRequesterId, 0) Then Throw New RequesterCanNotRequestReserveTurnRegisteringException
                'ثبت درخواست صدور نوبت رزرو
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationTurnRegisterRequest With {.TRRId = Nothing, .TRRTypeId = TurnRegisterRequestTypes.Reserve, .TruckId = InstanceConfiguration.GetConfigInt64(R2CoreTransportationAndLoadNotificationConfigurations.DefaultTransportationAndLoadNotificationConfigs, 1), .Description = InstanceTurnRegisterRequest.GetTurnRegisterRequestType(TurnRegisterRequestTypes.Reserve).TRRTypeTitle, .UserId = YourSoftwareUserId, .RequesterId = YourRequesterId, .DateTimeMilladi = Nothing, .DateShamsi = Nothing, .Time = Nothing}, Nothing, YourSoftwareUserId, YourRequesterId)
                'ثبت نوبت
                Dim TurnId = InstanceTurns.GetReserveTurn(TurnRegisterRequestId, YourTurnType, YourSoftwareUserId)
                'ثبت رابطه نوبت با درخواست صدور نوبت از طریق فضانام مدیریت روابط نهادی
                Dim InstanceEntityRelation = New R2CoreMClassEntityRelationManager
                InstanceEntityRelation.RegisteringEntityRelation(New R2StandardEntityRelationStructure(Nothing, R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest, TurnId, TurnRegisterRequestId, Nothing), RelationDeactiveTypes.BothDeactive)

                Return TurnRegisterRequestId
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UserCanNotRequestReserveTurnRegisteringException
                Throw ex
            Catch ex As RequesterCanNotRequestReserveTurnRegisteringException
                Throw ex
            Catch ex As Exception When TypeOf ex Is TurnRegisterRequestTypeNotFoundException _
                                OrElse TypeOf ex Is UserCanNotRequestReserveTurnRegisteringException _
                                OrElse TypeOf ex Is RequesterCanNotRequestReserveTurnRegisteringException _
                                OrElse TypeOf ex Is TruckNotFoundException _
                                OrElse TypeOf ex Is SequentialTurnNotFoundException _
                                OrElse TypeOf ex Is TruckDriverNotFoundException _
                                OrElse TypeOf ex Is TurnRegisterRequestNotFoundException _
                                OrElse TypeOf ex Is GetNSSException _
                                OrElse TypeOf ex Is GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ResuscitationReserveTurn(YourSeqTId As Int64, YourTurnRegisterRequestId As Int64, YourTruckId As Int64, YourRequesterId As Int64, YourTurnType As TurnType, YourSotwareUserId As Int64) As Int64
            Try
                'کنترل مجوز کاربر
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanResuscitationReserveTurn, YourSotwareUserId, 0) Then Throw New UserCanNotResuscitationReserveTurnException

                'کنترل نوع درخواست و تاریخ انقضاء 
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                Dim TRR = InstanceTurnRegisterRequest.GetTurnRegisterRequest(YourTurnRegisterRequestId, False)
                If TRR.TRRTypeId <> TurnRegisterRequestTypes.Reserve Then Throw New TurnRegisteringRequestIdIsNotaReserveTypeException
                If DateDiff(DateInterval.Hour, TRR.DateTimeMilladi, _R2DateTimeService.DateTimeServ.GetCurrentDateTimeMilladi) > InstanceTurnRegisterRequest.GetTurnRegisterRequestType(TRR.TRRTypeId).TurnExpirationHours Then Throw New TurnRegisteringRequestDateTimeExpiredException

                'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
                If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(YourTruckId, YourSeqTId) Then Throw New MoneyWalletCurrentChargeNotEnoughException

                'ثبت نوبت
                Dim Strategy = New ReserveTurnRegisteringStrategy(YourSeqTId, YourTruckId, YourTurnRegisterRequestId, YourSotwareUserId, YourRequesterId, YourTurnType)
                Strategy.DoStrategy()
                Return Strategy.GetTurnId
            Catch ex As DataBaseException
                Throw ex
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As TurnRegisteringRequestIdIsNotaReserveTypeException
                Throw ex
            Catch ex As UserCanNotResuscitationReserveTurnException
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
                                OrElse TypeOf ex Is TurnRegisteringRequestIdIsNotaReserveTypeException _
                                OrElse TypeOf ex Is MoneyWalletCurrentChargeNotEnoughException _
                                OrElse TypeOf ex Is TurnNotFoundException _
                                OrElse TypeOf ex Is ReserveTurnAlreadyUsedException _
                                OrElse TypeOf ex Is TurnRegisteringRequestDateTimeExpiredException _
                                OrElse TypeOf ex Is UserCanNotResuscitationReserveTurnException _
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function EmergencyTurnRegisterRequest(YourSeqTId As Int64, YourTruckId As Int64, ByRef YourTurnId As Int64, YourEmergencyTurnRegisterRequestNote As String, YourRequesterId As Int64, YourTurnType As TurnType, YourSoftwareUserId As Int64) As Int64
            Try
                'کنترل مجوز درخواست نوبت اضطراری
                Dim InstancePermissions = New R2CoreInstansePermissionsManager
                If Not InstancePermissions.ExistPermission(R2CoreTransportationAndLoadNotificationPermissionTypes.UserCanRequestEmergencyTurnRegistering, YourSoftwareUserId, 0) Then Throw New UserCanNotRequestEmergencyTurnRegisteringException
                'کنترل موجودی کیف پول برای هزینه صدور نوبت - درصورتی که موجودی کافی نباشداکسپشن پرتاب می گردد 
                If Not IsMoneyWalletInventoryIsEnoughForTurnRegistering(YourTruckId, YourSeqTId) Then Throw New MoneyWalletCurrentChargeNotEnoughException
                'ثبت درخواست و صدور نوبت اضطراری
                Dim InstanceTurnRegisterRequest = New R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_R2DateTimeService)
                Dim TurnRegisterRequestId = InstanceTurnRegisterRequest.TurnRegisterRequestRegistering(New R2CoreTransportationAndLoadNotificationTurnRegisterRequest With {.TRRId = Nothing, .TRRTypeId = TurnRegisterRequestTypes.Emergency, .Description = InstanceTurnRegisterRequest.GetTurnRegisterRequestType(TurnRegisterRequestTypes.Emergency).TRRTypeTitle + " " + Left(YourEmergencyTurnRegisterRequestNote, 500), .TruckId = YourTruckId, .RequesterId = YourRequesterId, .UserId = YourSoftwareUserId, .DateTimeMilladi = Nothing, .DateShamsi = Nothing, .Time = Nothing}, Nothing, YourSoftwareUserId, YourRequesterId)
                Dim Strategy = New RealTimeTurnRegisteringStrategy(YourSeqTId, YourTruckId, TurnRegisterRequestId, YourSoftwareUserId, YourRequesterId, YourTurnType)
                Strategy.DoStrategy()
                YourTurnId = Strategy.GetTurnId

                Return TurnRegisterRequestId
            Catch ex As DataBaseException
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
                                OrElse TypeOf ex Is DriverTruckInformationNotExistException
                Throw ex
            Catch ex As TurnRegisterRequestTypeNotFoundException
                Throw ex
            Catch ex As MoneyWalletCurrentChargeNotEnoughException
                Throw ex
            Catch ex As UserCanNotRequestEmergencyTurnRegisteringException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class


End Namespace
