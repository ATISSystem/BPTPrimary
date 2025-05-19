﻿
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms

Imports R2Core.ComputersManagement
Imports R2CoreGUI
Imports R2CoreTransportationAndLoadNotification.AnnouncementHalls
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions

Public Class UCUCSequentialTurnCollection
    Inherits UCGeneral

    Public Event UCCurrentNSSChangedEvent()

#Region "General Properties"

    Private _UCCurrentNSS As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure = Nothing
    <Browsable(False)>
    Public Property UCCurrentNSS() As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure
        Get
            If _UCCurrentNSS Is Nothing Then Throw New AnySequentialTurnDoNotSelectedException
            Return _UCCurrentNSS
        End Get
        Set(value As R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure)
            _UCCurrentNSS = value
            If value IsNot Nothing Then
                RaiseEvent UCCurrentNSSChangedEvent()
                Return
            End If
            If value Is Nothing Then
                UcLabelSeqTTitle.UCValue = String.Empty
                Return
            End If
        End Set
    End Property

#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Try
            UCRefreshGeneral()
            UCViewCollection()
            If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreTransportationAndLoadNotificationConfigurations.TurnControlling, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 1) = False Then
                Me.Visible = False
            End If
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try

    End Sub

    Public Overrides Sub UCRefreshGeneral()
        Try
            UCCurrentNSS = Nothing
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

    Public Sub UCViewCollection()
        Try
            Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardSequentialTurnStructure) = R2CoreTransportationAndLoadNotificationMClassSequentialTurnsManagement.GetSequentialTurns(R2CoreMClassComputersManagement.GetNSSCurrentComputer)
            PnlUCs.SuspendLayout()
            PnlUCs.Controls.Clear()
            For Loopx As Int64 = Lst.Count - 1 To 0 Step -1
                Dim UC As New UCViewerNSSSequentialTurn
                UC.UCViewNSS(Lst(Loopx))
                UC.Dock = DockStyle.Right
                AddHandler UC.UCClickedEvent, AddressOf UCs_UCClickedEvent
                UC.Size = New Size(TextRenderer.MeasureText(Lst(Loopx).SequentialTurnTitle.Trim, UC.UCFont).Width + 20, UC.Height)
                PnlUCs.Controls.Add(UC)
            Next
            PnlUCs.ResumeLayout()
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        End Try
    End Sub

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

    Private Sub UCs_UCClickedEvent(SenderUC As UCSequentialTurn)
        Try
            UcLabelSeqTTitle.UCValue = SenderUC.UCNSSCurrent.SequentialTurnTitle
            UCCurrentNSS = SenderUC.UCNSSCurrent
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
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
