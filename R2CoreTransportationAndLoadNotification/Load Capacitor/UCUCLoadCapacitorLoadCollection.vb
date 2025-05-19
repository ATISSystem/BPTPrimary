﻿
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Imports R2CoreGUI
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad

Public Class UCUCLoadCapacitorLoadCollection
    Inherits UCGeneralExtended

    Private ReadOnly _FrmMessageDialog As New FrmcMessageDialog
    Public Event UCSelectedNSSChangedEvent(NSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure)
    Public Event UCOrderingOptionChangedEvent(OrderingOption As R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions)
    Public Event UCViewInformationCompletedEvent()


#Region "General Properties"

    Private _UCTimerInterval As Int64=1
    Public Property UCTimerInterval() As Int64
        Get
            Return _UCTimerInterval
        End Get
        Set(value As Int64)
            _UCTimerInterval = value

        End Set
    End Property

#End Region

#Region "Subroutins And Functions"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private WithEvents _TTimer As System.Windows.Forms.Timer
    Private Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure)
    Private _Counting As Int64
    Private _Counter As Int64
    Public Sub UCViewCollection(YourCollection As List(Of R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadExtendedStructure))
        Try
            UCRefresh()
            Lst = YourCollection
            If Lst.Count < 1 Then Exit Sub
            _Counting = Lst.Count
            _Counter = Lst.Count - 1

            _TTimer = New Timer()
            _TTimer.Interval = UCTimerInterval
            _TTimer.Enabled = True
            _TTimer.Start()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Delegate Sub UCViewCollectionDelegate()
    Private Sub UCViewCollection()
        Try
            ''Cursor.Current = Cursors.WaitCursor
            Dim UC As UCViewerNSSLoadCapacitorLoadExtended = New UCViewerNSSLoadCapacitorLoadExtended()
            UC.UCViewNSS(Lst(_Counter))
            UC.Dock = DockStyle.Top
            AddHandler UC.UCClickedEvent, AddressOf UCs_UCClickedEvent
            AddHandler UC.UCOrderingOptionChanged, AddressOf UCs_UCOrderingOptionChanged
            If PnlUCs.InvokeRequired Then
                PnlUCs.Invoke(New UCViewCollectionDelegate(AddressOf UCViewCollection))
            Else
                PnlUCs.Controls.Add(UC)
            End If
        Catch ex As Exception
            ''Cursor.Current = Cursors.Default
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
        ''Cursor.Current = Cursors.Default
    End Sub

    Public Overloads Sub UCRefreshGeneral()
        Try
            UCRefresh()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Private Sub UCRefresh()
        Try
            PnlUCs.SuspendLayout()
            PnlUCs.Controls.Clear()
            PnlUCs.ResumeLayout()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"
    Private Sub _TTimer_Tick(sender As Object, e As EventArgs) Handles _TTimer.Tick
        Try
            _TTimer.Enabled = False
            _TTimer.Stop()

            UCViewCollection()

            _Counter = _Counter - 1
            If _Counter = -1 Then
                RaiseEvent UCViewInformationCompletedEvent()
                Exit Sub
            End If

            _TTimer.Enabled = True
            _TTimer.Start()
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UCs_UCClickedEvent(UC As UCLoadCapacitorLoad)
        Try
            RaiseEvent UCSelectedNSSChangedEvent(UC.UCNSSCurrent)
        Catch ex As Exception
            _FrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UCs_UCOrderingOptionChanged(OrderingOption As R2CoreTransportationAndLoadNotificationLoadCapacitorLoadOrderingOptions)
        Try
            RaiseEvent UCOrderingOptionChangedEvent(OrderingOption)
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
