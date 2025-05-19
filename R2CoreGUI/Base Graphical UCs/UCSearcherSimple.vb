﻿
Imports System.ComponentModel
Imports System.Reflection

Public Class UCSearcherSimple
    Inherits UCGeneral

    Public Event UC13PressedEvent(SearchString As String)
    Public Event UCEmptyStringRefreshRequestedEvent()


#Region "General Properties"

    Private _TextDisabled As String = String.Empty
    <Browsable(True)>
    Public Property UCTextDisabled() As String
        Get
            Return _TextDisabled
        End Get
        Set(value As String)
            _TextDisabled = value
            UcPersianTextBox.UCValue = value
            UcPersianTextBox.UCForeColor=Color.DarkGray
        End Set
    End Property

    Private _UCForeColor As Color = Color.Black
    Public Property UCForeColor() As Color
        Get
            Return _UCForeColor
        End Get
        Set(value As Color)
            _UCForeColor = value
            UcPersianTextBox.UCForeColor = value
        End Set
    End Property

#End Region

#Region "Subroutins And Functions"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Function UCGetCurrentSerachingText() As String
        Return UcPersianTextBox.UCValue.Trim()
    End Function

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

    Private Sub UcPersianTextBox_UC13PressedEvent(PersianText As String) Handles UcPersianTextBox.UC13PressedEvent
        Try
            RaiseEvent UC13PressedEvent(UcPersianTextBox.UCValue)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub PicReturn_Click(sender As Object, e As EventArgs) Handles PicReturn.Click
        Try
            RaiseEvent UC13PressedEvent(UcPersianTextBox.UCValue)
        Catch ex As Exception
            UCFrmMessageDialog.ViewDialogMessage(FrmcMessageDialog.DialogColorType.ErrorType, MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message, "", FrmcMessageDialog.MessageType.ErrorMessage, Nothing, Me)
        End Try
    End Sub

    Private Sub UCSearcherSimple_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        UcPersianTextBox.Focus()
    End Sub

    Private Sub UcPersianTextBox_UCGotFocusEvent() Handles UcPersianTextBox.UCGotFocusEvent
        UcPersianTextBox.UCRefresh()
        UcPersianTextBox.UCForeColor = UCForeColor
    End Sub

    Private Sub PicRefresh_Click(sender As Object, e As EventArgs) Handles PicRefresh.Click
        Try
            RaiseEvent UCEmptyStringRefreshRequestedEvent
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
