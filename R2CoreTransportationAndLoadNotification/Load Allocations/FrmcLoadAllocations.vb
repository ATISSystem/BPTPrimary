﻿
Imports System.Reflection

Imports R2Core.DesktopProcessesManagement
Imports R2CoreGUI
Imports R2CoreTransportationAndLoadNotification.LoadAllocation
Imports R2CoreTransportationAndLoadNotification.LoadAllocation.Exceptions
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.ProcessesManagement
Imports R2CoreTransportationAndLoadNotification.Turns

Public Class FrmcLoadAllocations
    Inherits FrmcGeneral

#Region "General Properties"
#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            InitializeSpecial()
            FrmRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try

    End Sub

    Protected Overrides Sub SetNSSProcess()
        Try
            SetProcess(R2CoreMClassDesktopProcessesManagement.GetNSSProcess(R2CoreTransportationAndLoadNotificationProcesses.FrmcLoadAllocations))
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub FrmRefresh()
        Try
            UcViewerCurrentLoadsStatistics.UCRefreshGeneral()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub ViewLoadAllocations(YourNSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure)
        Try
            UcucLoadAllocationCollection.UCRefreshGeneral()
            UcucLoadAllocationCollection.UCViewCollection(R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetLoadAllocations(YourNSSTurn))
        Catch ex As LoadAllocationsNotFoundException
            FrmcViewLocalMessage(ex.Message)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub




#End Region

#Region "Events"
#End Region

#Region "Event Handlers"
    Private Sub UcucLoadCapacitorLoadCollectionAdvance_UCSelectedNSSChangedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) Handles UcucLoadCapacitorLoadCollectionAdvance.UCSelectedNSSChangedEvent
        Try
            UcLoadAllocationManipulationByLoadCapacitorLoads.UCViewNSSLoadCapacitorLoad(NSS)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private _NSSTurnforViewLoadAllocationCollection As R2CoreTransportationAndLoadNotificationStandardTurnStructure = Nothing
    Private Sub UcLoadAllocationManipulationByLoadCapacitorLoads_UCTurnIdEnteredEvent(NSSTurn As R2CoreTransportationAndLoadNotificationStandardTurnStructure) Handles UcLoadAllocationManipulationByLoadCapacitorLoads.UCTurnIdEnteredEvent
        Try
            _NSSTurnforViewLoadAllocationCollection = NSSTurn
            ViewLoadAllocations(NSSTurn)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcucLoadAllocationCollection_UCDecreasedPriorityEvent() Handles UcucLoadAllocationCollection.UCDecreasedPriorityEvent
        Try
            UcucLoadAllocationCollection.UCRefreshGeneral()
            UcucLoadAllocationCollection.UCViewCollection(R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetLoadAllocations(_NSSTurnforViewLoadAllocationCollection))
        Catch ex As LoadAllocationsNotFoundException
            FrmcViewLocalMessage(ex.Message)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcucLoadAllocationCollection_UCIncreasedPriorityEvent() Handles UcucLoadAllocationCollection.UCIncreasedPriorityEvent
        Try
            UcucLoadAllocationCollection.UCRefreshGeneral()
            UcucLoadAllocationCollection.UCViewCollection(R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetLoadAllocations(_NSSTurnforViewLoadAllocationCollection))
        Catch ex As LoadAllocationsNotFoundException
            FrmcViewLocalMessage(ex.Message)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UcucLoadAllocationCollection_UCLoadAllocationCancelationEvent() Handles UcucLoadAllocationCollection.UCLoadAllocationCancelationEvent
        Try
            UcucLoadAllocationCollection.UCRefreshGeneral()
            UcucLoadAllocationCollection.UCViewCollection(R2CoreTransportationAndLoadNotificationMClassLoadAllocationManagement.GetLoadAllocations(_NSSTurnforViewLoadAllocationCollection))
        Catch ex As LoadAllocationsNotFoundException
            FrmcViewLocalMessage(ex.Message)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub


#End Region

#Region "Override Methods"
#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region


End Class