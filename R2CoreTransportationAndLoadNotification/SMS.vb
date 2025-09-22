


Imports R2Core.SMS.SMSHandling
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.SMS.SMSTypes
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation
Imports System.Reflection

Namespace SMS

    Namespace SMSTypes
        Public MustInherit Class R2CoreTransportationAndLoadNotificationSMSTypes
            Inherits R2CoreParkingSystemSMSTypes

            Public Shared ReadOnly Property LoadAllocationsLoadPermissionRegisteringFailed = 4
            Public Shared ReadOnly Property SendingTurnNumberSMS = 9
            Public Shared ReadOnly Property SendingLoadPermissionIssuedInfSMS = 10
            Public Shared ReadOnly Property LoadingAndDischargingPLacesChangeStatus = 18
            Public Shared ReadOnly Property TransportCompanyChangeActiveStatus = 19
            Public Shared ReadOnly Property ChangeLoadInformation = 20
            Public Shared ReadOnly Property FactoryAndProductionCenterChangeActiveStatus = 22
            Public Shared ReadOnly Property ChangingStatusOfTommorowLoadsSucceeded = 23

        End Class

    End Namespace

    Namespace RecivedSMSCodes
        Public MustInherit Class RecivedSMSCodes
            Public Shared ReadOnly Property ChangeLoadSource = 4
            Public Shared ReadOnly Property ChangeLoadTarget = 5
        End Class
    End Namespace

    Namespace SMSHandling

        Public Class R2CoreTransportationAndLoadNotificationChangeLoadSource
            Inherits RecievedSMSHandler


            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                    Dim InstanceLoadManipulation = New R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService)
                    InstanceLoadManipulation.ChangeLoadSource(MobileNumber, SMSContent)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public Class R2CoreTransportationAndLoadNotificationChangeLoadTarget
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                    Dim InstanceLoadManipulation = New R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService)
                    InstanceLoadManipulation.ChangeLoadTarget(MobileNumber, SMSContent)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

End Namespace
