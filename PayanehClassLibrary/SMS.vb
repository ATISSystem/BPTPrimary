

Imports PayanehClassLibrary.CarTruckNobatManagement
Imports PayanehClassLibrary.Turns
Imports R2Core.SMS.SMSHandling
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.SMS.SMSTypes
Imports System.Reflection

Namespace SMS

    Namespace SMSTypes
        Public MustInherit Class PayanehClassLibrarySMSTypes
            Inherits R2CoreTransportationAndLoadNotificationSMSTypes

            Public Shared ReadOnly Property TruckersAssociationControllingMoneyWallet = 6
            Public Shared ReadOnly Property ResuscitationNonCreditTurn = 15


        End Class

    End Namespace

    Namespace RecivedSMSCodes
        Public MustInherit Class RecivedSMSCodes
            Public Shared ReadOnly Property ResuscitationNonCreditTurn = 3
        End Class
    End Namespace

    'BPTChanged
    Namespace SMSHandling

        'BPTChanged
        Public Class PayanehClassLibraryResuscitationNonCreditTurn
            Inherits RecievedSMSHandler

            Public Sub New()
                MyBase.New()
            End Sub

            'BPTChanged
            Private Sub HandlingEvent_Handler() Handles MyBase.HandlingEvent
                Try
                    Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, Nothing)
                    Dim InstanceTurn = New PayanehClassLibraryTurnManager(_DateTimeService)
                    InstanceTurn.ResuscitationNonCreditTurn(InstanceSoftwareUsers.GetUser(New R2CoreSoftwareUserMobile(_MobileNumber), False), _UserId)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace

End Namespace
