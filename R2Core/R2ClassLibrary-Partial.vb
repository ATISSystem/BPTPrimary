﻿
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Timers
Imports System.ComponentModel
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Net.Mail
Imports System.Threading
Imports RestSharp
Imports Newtonsoft
Imports Newtonsoft.Json

Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MonetaryCreditSupplySources
Imports R2Core.LoggingManagement
Imports R2Core.BaseStandardClass
Imports R2Core.DateAndTimeManagement
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.EntityRelationManagement.Exceptions
Imports R2Core.PredefinedMessagesManagement.Exceptions
Imports R2Core.SMS
Imports R2Core.SMS.Exceptions
Imports R2Core.EntityRelationManagement
Imports R2Core.MobileProcessesManagement.Exceptions
Imports R2Core.WebProcessesManagement.Exceptions
Imports R2Core.BlackIPs.Exceptions
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.SecurityAlgorithmsManagement.ExpressionValidationAlgorithms.Exceptions
Imports R2Core.PermissionManagement.Exceptions

Imports PcPosDll
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SecurityAlgorithmsManagement.AESAlgorithms
Imports R2Core.MonetarySupply
Imports R2Core.Email.Exceptions
Imports R2Core.PermissionManagement
Imports R2Core.SiteIsBusy.Exceptions
Imports R2Core.SMS.SMSTypes.Exceptions
Imports R2Core.SMS.SMSTypes
Imports R2Core.SMS.SMSHandling.Exceptions
Imports R2Core.SMS.SMSSendRecive
Imports R2Core.SMS.SMSOwners.Exceptions
Imports R2Core.SMS.SMSOwners
Imports R2Core.SMS.SendSMSCodes.Exceptions
Imports R2Core.SMS.SendSMSCodes
Imports R2Core.SMS.RecivedSMSCodes.Exceptions
Imports R2Core.SMS.RecivedSMSCodes
Imports R2Core.SMS.SMSHandling
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2Core.SecurityAlgorithmsManagement.Captcha
Imports R2Core.SecurityAlgorithmsManagement.Hashing
Imports System.Web
Imports R2Core.Caching
Imports StackExchange.Redis
Imports RestSharp.Serialization.Json
Imports Newtonsoft.Json.Linq

Namespace MonetarySupply


    Public Enum MonetarySupplyResult
        None = 0
        Success = 1
        UnSuccess = 2
    End Enum

    Public Enum MonetarySupplyType
        None = 0
        PaymentRequest = 1
        VerificationRequest = 2
    End Enum

    Public Class R2CoreMonetarySupply

        Private _CurrentNSS As R2CoreStandardMonetaryCreditSupplySourceStructure = Nothing
        Private _Amount As Int64
        Public Event MonetarySupplySuccessEvent(YourMonetarySupplyType As MonetarySupplyType, TransactionId As Int64, Amount As Int64, SupplyReport As String)
        Public Event MonetarySupplyUnSuccessEvent(YourMonetarySupplyType As MonetarySupplyType, TransactionId As Int64, Amount As Int64, SupplyReport As String)
        Private WithEvents _MonetaryCreditSupplySource As R2CoreMonetaryCreditSupplySource = Nothing
        Private WithEvents _MonetarySupplyWatcher As System.Timers.Timer = New System.Timers.Timer

        Public Sub New(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64)
            Try
                _CurrentNSS = YourNSS
                _Amount = YourAmount
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub StartSupply()
            Try
                _MonetaryCreditSupplySource = R2CoreMonetaryCreditSupplySourcesManagement.GetMonetaryCreditSupplySourceInstance(_CurrentNSS, _Amount)
                _MonetaryCreditSupplySource.Initialize()
                _MonetaryCreditSupplySource.DoCreditSupply()
                _MonetarySupplyWatcher.Interval = 100
                _MonetarySupplyWatcher.Enabled = True
                _MonetarySupplyWatcher.Start()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub StartVerification(YourAuthority As String)
            Try
                _MonetaryCreditSupplySource = R2CoreMonetaryCreditSupplySourcesManagement.GetMonetaryCreditSupplySourceInstance(_CurrentNSS, _Amount)
                _MonetaryCreditSupplySource.Initialize()
                _MonetaryCreditSupplySource.DoVerification(YourAuthority)
                _MonetarySupplyWatcher.Interval = 100
                _MonetarySupplyWatcher.Enabled = True
                _MonetarySupplyWatcher.Start()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Sub _MonetarySupplyWatcher_Elapsed(sender As Object, e As ElapsedEventArgs) Handles _MonetarySupplyWatcher.Elapsed
            Try
                If _MonetaryCreditSupplySource.MonetaryCreditSupplyResult <> MonetarySupplyResult.None Then
                    _MonetarySupplyWatcher.Enabled = False
                    _MonetarySupplyWatcher.Stop()
                    If _MonetaryCreditSupplySource.MonetaryCreditSupplyResult = MonetarySupplyResult.Success Then
                        If _MonetaryCreditSupplySource.MonetarySupplyType = MonetarySupplyType.PaymentRequest Then
                            RaiseEvent MonetarySupplySuccessEvent(MonetarySupplyType.PaymentRequest, _MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                        ElseIf _MonetaryCreditSupplySource.MonetarySupplyType = MonetarySupplyType.VerificationRequest Then
                            RaiseEvent MonetarySupplySuccessEvent(MonetarySupplyType.VerificationRequest, _MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                        End If
                    Else
                        If _MonetaryCreditSupplySource.MonetarySupplyType = MonetarySupplyType.PaymentRequest Then
                            RaiseEvent MonetarySupplyUnSuccessEvent(MonetarySupplyType.PaymentRequest, _MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                        ElseIf _MonetaryCreditSupplySource.MonetarySupplyType = MonetarySupplyType.VerificationRequest Then
                            RaiseEvent MonetarySupplyUnSuccessEvent(MonetarySupplyType.VerificationRequest, _MonetaryCreditSupplySource.TransactionId, _MonetaryCreditSupplySource.Amount, _MonetaryCreditSupplySource.SupplyReport)
                        End If
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub
    End Class

End Namespace

Namespace MonetaryCreditSupplySources

    ' When new MonetaryCreditSupplySource must to add to ApplicationDomain then 
    ' 1-R2primary.dbo.TblMonetaryCreditSupplySources
    ' 2-R2Primary.dbo.TblConfigurationOfComputers for 65 Config
    ' 3-Developmnet of Classes - Inheritance of R2CoreMonetaryCreditSupplySource

    Public MustInherit Class R2CoreMonetaryCreditSupplySources
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Cash As Int64 = 1
        Public Shared ReadOnly Property Pos As Int64 = 2
        Public Shared ReadOnly Property ZarrinPalPaymentGate As Int64 = 3
        Public Shared ReadOnly Property ShepaPaymentGate As Int64 = 4
        Public Shared ReadOnly Property AqayepardakhtPaymentGate As Int64 = 5


    End Class

    Public Class R2CoreStandardMonetaryCreditSupplySourceStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            MCSSId = 0
            MCSSName = String.Empty
            MCSSTitle = String.Empty
            MCSSColor = Color.Navy
            AssemblyDll = String.Empty
            AssemblyPath = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.TrueString
        End Sub

        Public Sub New(ByVal YourMCSSId As Int64, ByVal YourMCSSName As String, ByVal YourMCSSTitle As String, ByVal YourMCSSColor As Color, YourAssemblyPath As String, YourAssemblyDll As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourMCSSId, YourMCSSName)
            MCSSId = YourMCSSId
            MCSSName = YourMCSSName
            MCSSTitle = YourMCSSTitle
            MCSSColor = YourMCSSColor
            AssemblyDll = YourAssemblyDll
            AssemblyPath = YourAssemblyPath
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property MCSSId As Int64
        Public Property MCSSName As String
        Public Property MCSSTitle As String
        Public Property MCSSColor As Color
        Public Property AssemblyDll As String
        Public Property AssemblyPath As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreMClassMonetaryCreditSupplySourcesManager

        Public Function GetNSSMonetaryCreditSupplySource(YourMCSSId As String) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX As New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where MCSSId=" & YourMCSSId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardMonetaryCreditSupplySourceStructure(Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("MCSSName").trim, Ds.Tables(0).Rows(0).Item("MCSSTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MCSSColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetMonetaryCreditSupplySources() As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX As New R2CoreInstanseSqlDataBOXManager
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where ViewFlag=1 and Active=1 and Deleted=0 ", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardMonetaryCreditSupplySourceStructure(DS.Tables(0).Rows(Loopx).Item("MCSSId"), DS.Tables(0).Rows(Loopx).Item("MCSSName").trim, DS.Tables(0).Rows(Loopx).Item("MCSSTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MCSSColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim InstanceConfigurationOfComputers As New R2CoreMClassConfigurationOfComputersManager
                Dim InstanceComputers As New R2CoreMClassComputersManager
                Dim ConfigValue As String = InstanceConfigurationOfComputers.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, InstanceComputers.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Return GetNSSMonetaryCreditSupplySource(ConfigValue.Split("@")(0))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim InstanceConfigurationOfComputers As New R2CoreMClassConfigurationOfComputersManager
                Dim InstanceComputers As New R2CoreMClassComputersManager
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                Dim ConfigValue As String = InstanceConfigurationOfComputers.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, InstanceComputers.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
                For Loopx As Int16 = 0 To Bitmap.Count - 1
                    Dim NSS As R2CoreStandardMonetaryCreditSupplySourceStructure = GetNSSMonetaryCreditSupplySource(Bitmap(Loopx).Split(":")(0))
                    NSS.Active = Bitmap(Loopx).Split(":")(1)
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetMonetaryCreditSupplySourceInstance(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64) As R2CoreMonetaryCreditSupplySource
            Try
                Dim AssemblyClassName As String = YourNSS.AssemblyPath
                Dim Instance As Object = Activator.CreateInstance(Type.GetType(AssemblyClassName), New Object() {YourAmount})
                'Dim Instance As Object = Activator.CreateInstance(Type.GetType("R2Core.MonetaryCreditSupplySources.ZarrinPalPaymentGate.R2CoreZarrinPalPaymentGate,R2Core"), New Object() {YourAmount})
                Return Instance
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySourcesManagement
        Public Shared Function GetNSSMonetaryCreditSupplySource(YourMCSSId As String) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim Ds As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where MCSSId=" & YourMCSSId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardMonetaryCreditSupplySourceStructure(Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("MCSSName").trim, Ds.Tables(0).Rows(0).Item("MCSSTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MCSSColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetaryCreditSupplySources() As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where ViewFlag=1 and Active=1 and Deleted=0 ", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardMonetaryCreditSupplySourceStructure(DS.Tables(0).Rows(Loopx).Item("MCSSId"), DS.Tables(0).Rows(Loopx).Item("MCSSName").trim, DS.Tables(0).Rows(Loopx).Item("MCSSTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MCSSColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Return GetNSSMonetaryCreditSupplySource(ConfigValue.Split("@")(0))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
                For Loopx As Int16 = 0 To Bitmap.Count - 1
                    Dim NSS As R2CoreStandardMonetaryCreditSupplySourceStructure = GetNSSMonetaryCreditSupplySource(Bitmap(Loopx).Split(":")(0))
                    NSS.Active = Bitmap(Loopx).Split(":")(1)
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetaryCreditSupplySourceInstance(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64) As R2CoreMonetaryCreditSupplySource
            Try
                Dim AssemblyClassName As String = YourNSS.AssemblyPath
                Dim Instance As Object = Activator.CreateInstance(Type.GetType(AssemblyClassName), New Object() {YourAmount})
                'Dim Instance As Object = Activator.CreateInstance(Type.GetType("R2Core.MonetaryCreditSupplySources.ShepaPaymentGate.R2CoreShepaPaymentGate,R2Core"), New Object() {YourAmount})
                Return Instance
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySource
        Protected _DateTime As R2DateTime = New R2DateTime

        Public Sub New(YourAmount As Int64)
            Try
                _Amount = YourAmount
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Protected _Amount As Int64 = 0
        <Browsable(False)>
        Public ReadOnly Property Amount As Int64
            Get
                Return _Amount
            End Get
        End Property

        Protected _SupplyReport As String = String.Empty
        <Browsable(False)>
        Public ReadOnly Property SupplyReport As String
            Get
                Return _SupplyReport
            End Get
        End Property

        Protected _TransactionId As Int64 = 0
        <Browsable(False)>
        Public ReadOnly Property TransactionId As Int64
            Get
                Return _TransactionId
            End Get
        End Property

        Protected _MonetaryCreditSupplyResult As MonetarySupply.MonetarySupplyResult = MonetarySupply.MonetarySupplyResult.None
        <Browsable(False)>
        Public ReadOnly Property MonetaryCreditSupplyResult As MonetarySupply.MonetarySupplyResult
            Get
                Return _MonetaryCreditSupplyResult
            End Get
        End Property

        Protected _MonetarySupplyType As MonetarySupplyType = MonetarySupply.MonetarySupplyType.None
        <Browsable(False)>
        Public ReadOnly Property MonetarySupplyType As MonetarySupply.MonetarySupplyType
            Get
                Return _MonetarySupplyType
            End Get
        End Property

        Public MustOverride Sub DoCreditSupply()

        Public MustOverride Sub DoVerification(YourAuthority As String)

        Public MustOverride Sub Initialize()

        Public MustOverride Sub Dispose()


    End Class

    Namespace Cash

        Public Class R2CoreCash
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource


            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                _SupplyReport = "عملیات موفق"
                _TransactionId = Convert.ToUInt64(_DateTime.GetCurrentDateShamsiFull.Replace("/", "") + _DateTime.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
                _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
            End Sub

            Public Overrides Sub Dispose()
            End Sub

            Public Overrides Sub DoVerification(YourAuthority As String)
                _SupplyReport = "عملیات موفق"
                _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
            End Sub
        End Class

    End Namespace

    Namespace Pos

        Namespace PCPos

            Public Class R2CorePCPos
                Inherits R2CoreMonetaryCreditSupplySource

                Private TargetedPosDevice As PosDevice = New PosDevice
                Public WithEvents _PCPOS As PcPosBusiness = New PcPosBusiness
                Public WithEvents _SearchPos As PcPosDiscovery = New PcPosDiscovery
                Public Event SearchAsyncCompleted(ResultCount As Int16, Description As String)

#Region "General Properties"

                Public ReadOnly Property GetTargetPosIPAddress As String
                    Get
                        Dim InstanceConfigurationOfComputers As New R2CoreMClassConfigurationOfComputersManager
                        Dim InstanceComputers As New R2CoreMClassComputersManager
                        Return InstanceConfigurationOfComputers.GetConfigString(R2CoreConfigurations.AttachedPoses, InstanceComputers.GetNSSCurrentComputer.MId, 0)
                    End Get
                End Property

#End Region

#Region "Subroutins And Functions"

                Public Sub New(YourAmount As Int64)
                    MyBase.New(YourAmount)
                    Try
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Sub LoggingPosResult(YourPosResult As PosResult)
                    Try
                        Dim InstanceLogging As New R2CoreInstanceLoggingManager
                        Dim InstanceSoftwareUsers As New R2CoreInstanseSoftwareUsersManager
                        Dim DataStruct As DataStruct = GetPosResultComposit(YourPosResult)
                        _TransactionId = DataStruct.Data1
                        InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.Note, "پوز - خرید", DataStruct.Data1, DataStruct.Data2, DataStruct.Data3, DataStruct.Data4, DataStruct.Data5, InstanceSoftwareUsers.GetNSSSystemUser().UserId, _DateTime.GetCurrentDateTimeMilladiFormated(), _DateTime.GetCurrentDateShamsiFull))
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Function GetPosResultComposit(YourPosResult As PosResult) As R2Core.BaseStandardClass.DataStruct
                    Try
                        Dim TransactionId As Long = Convert.ToUInt64(_DateTime.GetCurrentDateShamsiFull.Replace("/", "") + _DateTime.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
                        Dim DataStruct As DataStruct = New DataStruct
                        DataStruct.Data1 = TransactionId
                        DataStruct.Data2 = YourPosResult.PcPosStatusCode.ToString + "-" + YourPosResult.PcPosStatus + "-" + YourPosResult.OptionalField + "-" + YourPosResult.Amount
                        DataStruct.Data3 = YourPosResult.ResponseCode + "-" + YourPosResult.Rrn + "-" + YourPosResult.CardNo
                        DataStruct.Data4 = YourPosResult.TerminalId + "-" + YourPosResult.ProcessingCode + "-" + YourPosResult.TransactionNo
                        DataStruct.Data5 = YourPosResult.TransactionDate + "-" + YourPosResult.TransactionTime
                        Return DataStruct
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

                Public Function SearchPCPosAsync()
                    Try
                        _SearchPos.SearchPcPosByIp(100, 2000, GetTargetPosIPAddress)
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

#End Region

#Region "Events"
#End Region

#Region "Event Handlers"

                Private Sub _PCPOS_OnSaleResult(sender As Object, e As PosResult) Handles _PCPOS.OnSaleResult
                    Try
                        LoggingPosResult(e)
                        If e.ResponseCode = "00" Then
                            _SupplyReport = e.PcPosStatus
                            _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                            _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                        Else
                            _SupplyReport = "شکست عملیات هنگام ارتباط با دستگاه پوز"
                            _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                            _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                        End If
                    Catch ex As Exception
                        MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Private Sub _SearchPos_OnSearchPcPos(sender As Object, e As IEnumerable(Of PosDevice)) Handles _SearchPos.OnSearchPcPos
                    Try
                        If e.Count < 1 Then
                            TargetedPosDevice = Nothing
                            RaiseEvent SearchAsyncCompleted(e.Count, (New Exceptions.PCPosWithTargetIpNotFoundException).Message)
                        Else
                            TargetedPosDevice = e(0)
                        End If
                    Catch ex As Exception
                        MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

#End Region

#Region "Override Methods"
                Public Overrides Sub Initialize()
                    Try
                        TargetedPosDevice.IpAddress = GetTargetPosIPAddress
                        TargetedPosDevice.Port = 8888
                        'SearchPCPosAsync()
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try

                End Sub

                Public Overrides Sub DoCreditSupply()
                    Try
                        _PCPOS = New PcPosBusiness
                        _PCPOS.Amount = Amount
                        _PCPOS.RetryTimeOut = New Integer() {1800000, 1800000, 1800000}
                        _PCPOS.ResponseTimeout = New Integer() {1800000, 1800000, 1800000}
                        _PCPOS.Ip = TargetedPosDevice.IpAddress
                        _PCPOS.Port = TargetedPosDevice.Port
                        _PCPOS.ConnectionType = PcPosConnectionType.Lan
                        _PCPOS.AsyncSaleTransaction()
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Sub

                Public Overrides Sub Dispose()
                    Try
                        _PCPOS.Dispose()
                    Catch ex As Exception
                    End Try
                    Try
                        _SearchPos.Dispose()
                    Catch ex As Exception
                    End Try
                End Sub

                Public Overrides Sub DoVerification(YourAuthority As String)
                    _SupplyReport = "عملیات موفق"
                    _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                    _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                End Sub




#End Region

#Region "Abstract Methods"
#End Region

#Region "Implemented Members"
#End Region

            End Class

            Namespace Exceptions
                Public Class PCPosWithTargetIpNotFoundException
                    Inherits ApplicationException
                    Public Overrides ReadOnly Property Message As String
                        Get
                            Return "دستگاه پوز آماده نیست و یا ارتباط با آن فعلا مقدور نمی باشد"
                        End Get
                    End Property
                End Class

            End Namespace

        End Namespace

    End Namespace

    Namespace ZarrinPalPaymentGate

        Public Class R2CoreZarrinPalPaymentGate
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource

            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim requesturl As String
                    If (_Amount = 200000) Then
                        requesturl = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 1) + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() +
                        "&callback_url=" + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 3) +
                        "&description=" + "پرداخت خودگردان" +
                        "&metadata[0]=" + String.Empty + "& metadata[1]=" + String.Empty
                    Else
                        requesturl = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 1) + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() +
                        "&callback_url=" + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 3) +
                        "&description=" + "درخواست پرداخت-زرین پال-آتیس" +
                        "&metadata[0]=" + String.Empty + "& metadata[1]=" + String.Empty
                    End If

                    Dim client = New RestClient(requesturl)
                    client.Timeout = -1
                    Dim request = New RestRequest(Method.POST)
                    request.AddHeader("accept", "application/json")
                    request.AddHeader("content-type", "application/json")
                    Dim requestresponse As IRestResponse = client.Execute(request)
                    Dim jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content)
                    Dim errorscode = jo("errors").ToString()
                    Dim jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content)
                    Dim dataauth = jodata("data").ToString()
                    If dataauth <> "[]" Then
                        _SupplyReport = jodata("data")("authority").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        _SupplyReport = errorscode
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub

            Public Overrides Sub Dispose()
            End Sub

            Public Overrides Sub DoVerification(YourAuthority As String)
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim url As String = String.Empty
                    If (_Amount = 200000) Then
                        url = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 4) + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() + "&authority=" + YourAuthority
                    Else
                        url = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 4) + InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() + "&authority=" + YourAuthority
                    End If
                    Dim client = New RestClient(url)
                    client.Timeout = -1
                    Dim request = New RestRequest(Method.POST)
                    request.AddHeader("accept", "application/json")
                    request.AddHeader("content-type", "application/json")
                    Dim response As IRestResponse = client.Execute(request)
                    Dim jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Content)
                    Dim Data = jodata("data").ToString()
                    Dim jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content)
                    Dim errors = jo("errors").ToString()
                    If (Data <> "[]") Then
                        _SupplyReport = jodata("data")("ref_id").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        _SupplyReport = errors
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub



        End Class

    End Namespace

    Namespace ShepaPaymentGate

        Public Class R2CoreShepaPaymentGate
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource

            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()
            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim Merchant As ShepaMerchant.Merchant = New ShepaMerchant.Merchant
                    Dim Data = Merchant.requestToken(InstanceConfiguration.GetConfigString(R2CoreConfigurations.ShepaPaymentGate, 0), _Amount.ToString(), InstanceConfiguration.GetConfigString(R2CoreConfigurations.ShepaPaymentGate, 3), "", "", "", "0", "", "")
                    If Data.success Then
                        _SupplyReport = Data.result.token
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        _SupplyReport = Data.error.ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub

            Public Overrides Sub Dispose()
            End Sub

            Public Overrides Sub DoVerification(YourAuthority As String)
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim Merchant As ShepaMerchant.Merchant = New ShepaMerchant.Merchant
                    Dim responce = Merchant.verifyPayment(InstanceConfiguration.GetConfigString(R2CoreConfigurations.ShepaPaymentGate, 0), YourAuthority, _Amount.ToString())
                    If responce.success Then
                        _SupplyReport = responce.result.refid
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        _SupplyReport = responce.error.ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class


    End Namespace

    Namespace AqayepardakhtPaymentGate

        Public Class R2CoreAqayepardakhtPaymentGate
            Inherits MonetaryCreditSupplySources.R2CoreMonetaryCreditSupplySource

            Public Sub New(YourAmount As Int64)
                MyBase.New(YourAmount)
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim requesturl As String
                    requesturl = InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 1) + "&pin=" + InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 0) + "&amount=" + _Amount.ToString() +
                    "&callback=" + InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 3) +
                    "&description=" + "پرداخت آتیس"
                    Dim client = New RestClient(requesturl)
                    client.Timeout = -1
                    Dim request = New RestRequest(Method.POST)
                    request.AddHeader("accept", "application/json")
                    request.AddHeader("content-type", "application/json")
                    Dim requestresponse As IRestResponse = client.Execute(request)
                    Dim jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content)
                    Dim status = jo("status").ToString()
                    If status = "success" Then
                        Dim jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content)
                        _SupplyReport = jodata("transid").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        Dim jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content)
                        _SupplyReport = jodata("code").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.PaymentRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub

            Public Overrides Sub Dispose()
            End Sub

            Public Overrides Sub DoVerification(YourAuthority As String)
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager()
                    Dim url As String = String.Empty
                    url = InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 4) + "&pin=" + InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 0) + "&amount=" + _Amount.ToString() + "&transid=" + YourAuthority
                    Dim client = New RestClient(url)
                    client.Timeout = -1
                    Dim request = New RestRequest(Method.POST)
                    request.AddHeader("accept", "application/json")
                    request.AddHeader("content-type", "application/json")
                    Dim response As IRestResponse = client.Execute(request)
                    Dim jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Content)
                    Dim status = jodata("status").ToString()
                    If (status = "success") Then
                        _SupplyReport = jodata("code").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.Success
                    Else
                        _SupplyReport = jodata("code").ToString()
                        _MonetarySupplyType = MonetarySupply.MonetarySupplyType.VerificationRequest
                        _MonetaryCreditSupplyResult = MonetarySupply.MonetarySupplyResult.UnSuccess
                    End If
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

    End Namespace



End Namespace

Namespace MonetarySettingTools

    'When new MonetarySettingTool must to add to ApplicationDomain then 
    ' 1-R2primary.dbo.TblMonetarySettingTools
    ' 2-R2Primary.dbo.TblConfigurationOfComputers for 66 Config
    ' 3-Developmnet of Classes - Inheritance of UCMonetarySettingToolInstrument

    Public MustInherit Class R2CoreMonetarySettingTools
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property UserPad As Int64 = 1
        Public Shared ReadOnly Property R2PrimaryWebService As Int64 = 3
    End Class

    Public Class R2CoreStandardMonetarySettingToolStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            MSTId = 0
            MSTName = String.Empty
            MSTTitle = String.Empty
            MSTColor = Color.Navy
            AssemblyDll = String.Empty
            AssemblyPath = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.TrueString
        End Sub

        Public Sub New(ByVal YourMSTId As Int64, ByVal YourMSTName As String, ByVal YourMSTTitle As String, ByVal YourMSTColor As Color, YourAssemblyPath As String, YourAssemblyDll As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourMSTId, YourMSTName)
            MSTId = YourMSTId
            MSTName = YourMSTName
            MSTTitle = YourMSTTitle
            MSTColor = YourMSTColor
            AssemblyDll = YourAssemblyDll
            AssemblyPath = YourAssemblyPath
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property MSTId As Int64
        Public Property MSTName As String
        Public Property MSTTitle As String
        Public Property MSTColor As Color
        Public Property AssemblyDll As String
        Public Property AssemblyPath As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public MustInherit Class R2CoreMonetarySettingToolsManagement
        Public Shared Function GetNSSMonetarySettingTool(YourMSTId As String) As R2CoreStandardMonetarySettingToolStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools  Where MSTId=" & YourMSTId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2CoreStandardMonetarySettingToolStructure(Ds.Tables(0).Rows(0).Item("MSTId"), Ds.Tables(0).Rows(0).Item("MSTName").trim, Ds.Tables(0).Rows(0).Item("MSTTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MSTColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetarySettingTools() As List(Of R2CoreStandardMonetarySettingToolStructure)
            Try
                Dim DS As DataSet
                R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools Where ViewFlag=1 and Active=1 and Deleted=0", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreStandardMonetarySettingToolStructure(DS.Tables(0).Rows(Loopx).Item("MSTId"), DS.Tables(0).Rows(Loopx).Item("MSTName").trim, DS.Tables(0).Rows(Loopx).Item("MSTTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MSTColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetarySettingToolStructure
            Try
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Return GetNSSMonetarySettingTool(ConfigValue.Split("@")(0))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetarySettingToolStructure)
            Try
                Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
                Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
                Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
                For Loopx As Int16 = 0 To Bitmap.Count - 1
                    Dim NSS As R2CoreStandardMonetarySettingToolStructure = GetNSSMonetarySettingTool(Bitmap(Loopx).Split(":")(0))
                    NSS.Active = Bitmap(Loopx).Split(":")(1)
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMonetarySettingToolInstrumentInstance(YourNSS As R2CoreStandardMonetarySettingToolStructure) As Object
            Try
                Dim PathDll As String = Application.StartupPath + "\" + YourNSS.AssemblyDll.Trim
                Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
                Dim ProjAndForm As String = YourNSS.AssemblyPath.Trim()
                Dim FormInstanceType = Assembly_.GetType(ProjAndForm)
                Dim objForm = CType(Activator.CreateInstance(FormInstanceType), Object)
                Return objForm
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace

Namespace SessionManagement

    Public Class R2CoreStandardSessionCaptchaBitMapStructure

        Public Sub New()
            MyBase.New()
            _SessionId = String.Empty
            _Captcha = New Byte() {}
        End Sub

        Public Sub New(YourSessionId As String, YourCaptcha As Byte())
            _SessionId = YourSessionId
            _Captcha = YourCaptcha
        End Sub

        Public Property SessionId As String
        Public Property Captcha As Byte()

    End Class

    Public Class R2CoreStandardSessionCaptchaWordStructure

        Public Sub New()
            MyBase.New()
            _SessionId = String.Empty
            _Captcha = String.Empty
        End Sub

        Public Sub New(YourSessionId As String, YourCaptcha As String)
            _SessionId = YourSessionId
            _Captcha = YourCaptcha
        End Sub

        Public Property SessionId As String
        Public Property Captcha As String

    End Class

    Public Class R2CoreSessionManager
        Private _DateTime As New R2DateTime

        Public Function GetNewSessionId() As String
            Try
                Dim InstanceAESAlgorithms = New AESAlgorithmsManager
                Dim InstanceMD5Hasher = New MD5Hasher
                Dim Instance = New R2Core.DateAndTimeManagement.R2DateTime
                Dim SessionId = InstanceMD5Hasher.GenerateMD5String(_DateTime.GetCurrentDateTimeMilladi) + InstanceAESAlgorithms.GetSalt(12)
                Return SessionId
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function StartSession() As R2CoreStandardSessionCaptchaBitMapStructure
            Try
                Dim InstanceCaptcha = New R2CoreInstanceCaptchaManager
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceCaching = New R2CoreCacheManager
                Dim CaptchaWord = InstanceCaptcha.GenerateFakeWordNumeric(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))
                Dim CaptchaBitMap = InstanceCaptcha.GetCaptcha(CaptchaWord)
                Dim SessionId = GetNewSessionId()
                InstanceCaching.SetCache(InstanceCaching.GetNSSCacheKey(R2CoreCacheKeys.Session).KeyName + SessionId, New R2CoreStandardSessionCaptchaWordStructure(SessionId, CaptchaWord))
                Return New R2CoreStandardSessionCaptchaBitMapStructure(SessionId, CaptchaBitMap)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ConfirmSession(YourSessionId As String) As String
            Try
                Dim InstanceCacheKeys = New Caching.R2CoreCacheManager
                Dim CachKey = InstanceCacheKeys.GetNSSCacheKey(Caching.R2CoreCacheKeys.Session).KeyName + YourSessionId
                Dim Content = JsonConvert.DeserializeObject(Of R2CoreStandardSessionAPIKeyStructure)(InstanceCacheKeys.GetCache(CachKey).ToString)
                Return Content.APIKey
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

End Namespace

Namespace SecurityAlgorithmsManagement


    Public Class ExchangeKey
        Public Sub New()
            ExchangeKey = Int64.MinValue
            UserId = Int64.MinValue
            StartDateTime = Now
        End Sub

        Public Sub New(YourExchangeKey As Int64, YourUserId As Int64, YourStartDateTime As DateTime)
            ExchangeKey = YourExchangeKey
            UserId = YourUserId
            StartDateTime = YourStartDateTime
        End Sub

        Public ExchangeKey As Int64
        Public UserId As Int64
        Public StartDateTime As DateTime
    End Class

    Public Class ExchangeKeyManager

        Private _LstUsers As List(Of ExchangeKey) = New List(Of ExchangeKey)

        Public Sub New()
        End Sub

        Public Function Login(YourUserShenaseh As String, YourUserPassword As String) As Int64
            Try
                R2CoreMClassSoftwareUsersManagement.AuthenticationUserbyShenasehPassword(New R2CoreStandardSoftwareUserStructure(Nothing, Nothing, Nothing, Nothing, YourUserShenaseh, YourUserPassword, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
                Dim NSS = R2CoreMClassSoftwareUsersManagement.GetNSSUser(YourUserShenaseh, YourUserPassword)
                If _LstUsers.Exists(Function(x) x.UserId = NSS.UserId) Then
                    _LstUsers.Where(Function(x) x.UserId = NSS.UserId)(0).StartDateTime = Now
                    Return _LstUsers.Where(Function(x) x.UserId = NSS.UserId)(0).ExchangeKey
                End If
                Dim EKTemp = R2CoreMClassSecurityAlgorithmsManagement.GetNewExchangeKey()
                _LstUsers.Add(New ExchangeKey(EKTemp, NSS.UserId, Now))
                Return EKTemp
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception When TypeOf (ex) Is UserIsNotActiveException OrElse TypeOf (ex) Is UserNotExistException OrElse TypeOf (ex) Is GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AuthenticationExchangeKey(YourExchangeKey As Int64)
            Try
                If _LstUsers.Exists(Function(x) x.ExchangeKey = YourExchangeKey) Then
                    If DateDiff(DateInterval.Minute, _LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).StartDateTime, Now) <= 1 Then
                    Else
                        _LstUsers.Remove(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0))
                        Throw New ExchangeKeyTimeRangePassedException
                    End If
                Else
                    Throw New ExchangeKeyNotExistException
                End If
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSUser(YourExchangeKey As Int64) As R2CoreStandardSoftwareUserStructure
            Try
                AuthenticationExchangeKey(YourExchangeKey)
                Return R2CoreMClassSoftwareUsersManagement.GetNSSUser(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).UserId)
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreMClassSecurityAlgorithmsManagement
        Public Shared Function GetNewExchangeKey() As Int64
            Try
                Dim RandGen As New Random
                Return RandGen.Next(10000, 1000000)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Hashing
        Public Class SHAHasher

            Public Function GenerateSHA256String(ByVal inputString) As String
                Dim sha256 As SHA256 = SHA256Managed.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
                Dim hash As Byte() = sha256.ComputeHash(bytes)
                Dim stringBuilder As New StringBuilder()

                For i As Integer = 0 To hash.Length - 1
                    stringBuilder.Append(hash(i).ToString("x2"))
                Next

                Return stringBuilder.ToString()
            End Function

            Public Function GenerateSHA512String(ByVal inputString) As String
                Dim sha512 As SHA512 = SHA512Managed.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
                Dim hash As Byte() = sha512.ComputeHash(bytes)
                Dim stringBuilder As New StringBuilder()

                For i As Integer = 0 To hash.Length - 1
                    stringBuilder.Append(hash(i).ToString("x2"))
                Next
                Return stringBuilder.ToString()
            End Function

            Public Function ComputeSha256Hash(YourrawData As String) As String
                Try
                    Dim sha256Hash As SHA256 = SHA256.Create()
                    Dim bytes As Byte() = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(YourrawData))
                    Dim builder As StringBuilder = New StringBuilder()
                    For i As Int64 = 0 To bytes.Length - 1
                        builder.Append(bytes(i).ToString("x2"))
                    Next
                    Return builder.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Public Class MD5Hasher
            Public Function GenerateMD5String(ByVal inputString) As String
                Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider
                Dim bytesToHash() As Byte = System.Text.Encoding.UTF8.GetBytes(inputString)
                bytesToHash = md5Obj.ComputeHash(bytesToHash)
                Dim strResult As String = ""
                For Each b As Byte In bytesToHash
                    strResult += b.ToString("x2")
                Next
                Return strResult
            End Function
        End Class

    End Namespace

    Namespace AESAlgorithms
        Public Class AESAlgorithmsManager

            Private ValidChars As String = "%$#@^&!~()-+*=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Private Function GenerateRandomString(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim RandomString = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        RandomString.Append(ValidChars(Random.Next(0, ValidChars.Length - 1)))
                    Next
                    Return RandomString.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private VerificationCodeValidChars As String = "%$#@^!~()-+*=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Public Function GenerateVerificationCode(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim SBVerificationCode = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        SBVerificationCode.Append(VerificationCodeValidChars(Random.Next(0, VerificationCodeValidChars.Length - 1)))
                    Next
                    Return SBVerificationCode.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNonce(YourLength As Int32) As String
                Try
                    Return GenerateRandomString(YourLength)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSalt(YourLength As Int32) As String
                Try
                    Return GenerateRandomString(YourLength)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function Encrypt(YourInput As String, YourKey As String) As String
                Try

                    Dim inputArray As Byte() = UTF8Encoding.UTF8.GetBytes(YourInput)
                    Dim AES As AesCryptoServiceProvider = New AesCryptoServiceProvider
                    AES.KeySize = 256
                    AES.Key = UTF8Encoding.UTF8.GetBytes(YourKey)
                    AES.Mode = CipherMode.ECB
                    AES.Padding = PaddingMode.PKCS7
                    AES.IV = {150, 9, 112, 39, 32, 5, 136, 254, 251, 43, 44, 191, 217, 236, 3, 106}
                    Dim cTransform As ICryptoTransform = AES.CreateEncryptor()
                    Dim resultArray As Byte() = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length)
                    AES.Clear()
                    Return Convert.ToBase64String(resultArray, 0, resultArray.Length)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function Decrypt(YourInput As String, YourKey As String) As String
                Try
                    Dim inputArray As Byte() = Convert.FromBase64String(YourInput)
                    Dim AES As AesCryptoServiceProvider = New AesCryptoServiceProvider()
                    AES.KeySize = 256
                    AES.Key = UTF8Encoding.UTF8.GetBytes(YourKey)
                    AES.Mode = CipherMode.ECB
                    AES.Padding = PaddingMode.PKCS7
                    AES.IV = {150, 9, 112, 39, 32, 5, 136, 254, 251, 43, 44, 191, 217, 236, 3, 106}
                    Dim cTransform As ICryptoTransform = AES.CreateDecryptor()
                    Dim resultArray As Byte() = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length)
                    AES.Clear()
                    Return UTF8Encoding.UTF8.GetString(resultArray)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetRandomNumericCode(YourFirstValue As Int64, YourSecondValue As Int64) As Int64
                Try
                    Thread.Sleep(1)
                    Dim RandGen As New Random(DateTime.Now.Millisecond)
                    Return RandGen.Next(YourFirstValue, YourSecondValue)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function





        End Class

    End Namespace

    Namespace Captcha

        Public Class ImageRawData
            Public IRawData As Byte()
        End Class

        Public Class R2CoreInstanceCaptchaManager

            Dim bmpCaptcha As Bitmap
            Dim iBMPHeight As Integer = 50
            Dim iBMPWidth As Integer = 150
            Dim sLeftMargin As Single = 5
            Dim sTopMargin As Single = 10
            Dim g As Graphics
            Dim sWord As String
            Dim sLetter As String
            Dim sfLetter As SizeF
            Dim rfLetter As RectangleF
            Dim sX1 As Single = 0
            Dim sY1 As Single = 0
            Dim sX2 As Single = 0
            Dim sY2 As Single = 0
            Dim sTemp As Single
            Dim iAngle As Integer
            Dim sOffset As Single

            Public Function GenerateCaptcha(ByVal sWord As String) As Bitmap
                Dim ixr As Integer
                bmpCaptcha = New Bitmap(iBMPWidth, iBMPHeight,
                        Drawing.Imaging.PixelFormat.Format16bppRgb555)
                g = Graphics.FromImage(bmpCaptcha)
                Dim drawBackground As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(drawBackground, New Rectangle(0, 0, iBMPWidth, iBMPHeight))

                ' Create font and brush.
                Dim drawFont As New System.Drawing.Font("Alborz Titr", 16, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                Dim drawBrush As New SolidBrush(Color.Red)
                Dim strFormat As New StringFormat(StringFormatFlags.FitBlackBox)
                sfLetter = New SizeF(30, 30)
                ' Draw string to screen.
                For ixr = 0 To sWord.Length - 1
                    sLetter = sWord.Substring(ixr, 1)
                    g = Graphics.FromImage(bmpCaptcha)
                    rfLetter = New RectangleF(sLeftMargin, sTopMargin, 30, 30)
                    sfLetter = g.MeasureString(sLetter, drawFont, sfLetter, strFormat)
                    iAngle = RndInterval(0, 20) - 10
                    With rfLetter
                        sOffset = sLeftMargin * Math.Tan(iAngle * Math.PI / 180)
                        .Y = sTopMargin - sOffset
                        .Width = sfLetter.Width + 10
                        .Height = sfLetter.Height
                    End With
                    g.RotateTransform(iAngle)
                    g.DrawString(sLetter, drawFont, drawBrush, rfLetter)
                    sLeftMargin += sfLetter.Width + 2
                Next
                Dim drawPen As Pen = New Pen(Color.Crimson, 1)
                For ixr = 0 To 3
                    sX1 = sX2
                    Do While Math.Abs(sX1 - sX2) < iBMPWidth * 0.5
                        sX1 = RndInterval(2, iBMPWidth - 2)
                        sX2 = RndInterval(2, iBMPWidth - 2)
                    Loop
                    sY1 = sY2
                    Do While Math.Abs(sY1 - sY2) < iBMPHeight * 0.5
                        sY1 = RndInterval(2, iBMPHeight - 2)
                        sY2 = RndInterval(2, iBMPHeight - 2)
                    Loop
                    If RndInterval(0, 2) > 1 Then
                        sTemp = sX1
                        sX1 = sX2
                        sX2 = sTemp
                    End If
                    If RndInterval(0, 2) > 1 Then
                        sTemp = sY1
                        sY1 = sY2
                        sY2 = sTemp
                    End If

                    g.DrawLine(drawPen, sX1, sY1, sX2, sY2)
                Next
                g.Dispose()
                Return bmpCaptcha
            End Function

            Public Class AESAlgorithms

                Public Function ImageToBytes(value As Image) As Byte()
                    Dim converter As New ImageConverter()
                    Dim arr As Byte() = CType(converter.ConvertTo(value, GetType(Byte())), Byte())
                    Return arr
                End Function

                Public Function BytesToImage(value As Byte()) As Image
                    Using ms As New MemoryStream(value)
                        Return Image.FromStream(ms)
                    End Using
                End Function

                Public Function EncodeBytes(value As Byte()) As String
                    Return Convert.ToBase64String(value)
                End Function

                Public Function DecodeBytes(value As String) As Byte()
                    Return Convert.FromBase64String(value)
                End Function

                Public Function StringHash(value As Byte()) As String
                    Using sha256 As SHA256 = SHA256.Create()
                        Dim hashBytes As Byte() = sha256.ComputeHash(value)
                        Dim sb As New StringBuilder()
                        For i As Integer = 0 To hashBytes.Length - 1
                            sb.Append(hashBytes(i).ToString("X2"))
                        Next
                        Return sb.ToString().ToLower()
                    End Using
                End Function

            End Class

            Public Class JsonImage
                Private _AESAlgorithms As New AESAlgorithms()

                Public Property hash As String = String.Empty
                Public Property len As Integer = 0
                Public Property image As String = String.Empty

                Public Sub New()
                End Sub

                Public Sub New(value As Image)
                    Dim img_sources As Byte() = _AESAlgorithms.ImageToBytes(value)
                    hash = _AESAlgorithms.StringHash(img_sources)
                    len = img_sources.Length
                    image = _AESAlgorithms.EncodeBytes(img_sources)
                End Sub

                Public Function GetRawData() As Byte()
                    Dim data As Byte() = _AESAlgorithms.DecodeBytes(image)

                    If data.Length <> len Then
                        Throw New Exception("Error data len")
                    End If

                    If Not _AESAlgorithms.StringHash(data).Equals(hash) Then
                        Throw New Exception("Error hash")
                    End If

                    Return data
                End Function
            End Class

            Public Function GetCaptcha(ByVal sWord As String) As Byte()
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    'Dim Captcha = GenerateFakeWordNumeric(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))
                    Dim CaptchaImage = GenerateCaptcha(sWord)
                    Dim IImage As ImageRawData = New ImageRawData
                    IImage.IRawData = (New JsonImage(CaptchaImage)).GetRawData()
                    Return IImage.IRawData
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function GenerateFakeWord(ByVal iLengthRequired As Integer) As String
                Dim sVowels As String = "AEIOU"
                Dim sConsonants As String = "BCDFGHJKLMNPQRSTVWXYZ"
                Dim iNbrVowels As Integer
                Dim iNbrConsonants As Integer
                Dim sWord As String
                Dim bUseVowel As Boolean
                Dim iWordLength As Integer
                Dim sPattern As String
                iNbrVowels = 0
                iNbrConsonants = 0
                bUseVowel = False
                sWord = ""
                Randomize(DateTime.UtcNow.Millisecond)
                For iWordLength = 1 To iLengthRequired
                    If (iWordLength = 2) Or ((iLengthRequired > 1) And (iWordLength = iLengthRequired)) Then
                        bUseVowel = Not bUseVowel
                    ElseIf (iNbrVowels < 2) And (iNbrConsonants < 2) Then
                        bUseVowel = ((Rnd(1) * 2) > 1)
                    ElseIf (iNbrVowels < 2) Then
                        bUseVowel = True
                    ElseIf (iNbrConsonants < 2) Then
                        bUseVowel = False
                    End If

                    sPattern = IIf(bUseVowel, sVowels, sConsonants)
                    sWord = sWord & sPattern.Substring(Int(Rnd(1) * sPattern.Length), 1)
                    If bUseVowel Then
                        iNbrVowels = iNbrVowels + 1
                        iNbrConsonants = 0
                    Else
                        iNbrVowels = 0
                        iNbrConsonants = iNbrConsonants + 1
                    End If
                Next
                Return sWord
            End Function

            Public Function GenerateFakeWordNumeric(ByVal iLengthRequired As Integer) As String
                Dim sVowels As String = "98765"
                Dim sConsonants As String = "0123456789"
                Dim iNbrVowels As Integer
                Dim iNbrConsonants As Integer
                Dim sWord As String
                Dim bUseVowel As Boolean
                Dim iWordLength As Integer
                Dim sPattern As String
                iNbrVowels = 0
                iNbrConsonants = 0
                bUseVowel = False
                sWord = ""
                Randomize(DateTime.UtcNow.Millisecond)
                For iWordLength = 1 To iLengthRequired
                    If (iWordLength = 2) Or ((iLengthRequired > 1) And (iWordLength = iLengthRequired)) Then
                        bUseVowel = Not bUseVowel
                    ElseIf (iNbrVowels < 2) And (iNbrConsonants < 2) Then
                        bUseVowel = ((Rnd(1) * 2) > 1)
                    ElseIf (iNbrVowels < 2) Then
                        bUseVowel = True
                    ElseIf (iNbrConsonants < 2) Then
                        bUseVowel = False
                    End If

                    sPattern = IIf(bUseVowel, sVowels, sConsonants)
                    sWord = sWord & sPattern.Substring(Int(Rnd(1) * sPattern.Length), 1)
                    If bUseVowel Then
                        iNbrVowels = iNbrVowels + 1
                        iNbrConsonants = 0
                    Else
                        iNbrVowels = 0
                        iNbrConsonants = iNbrConsonants + 1
                    End If
                Next
                Return sWord
            End Function

            Private Function RndInterval(ByVal iMin As Integer, ByVal iMax As Integer) As Integer
                Randomize()
                Return Int(((iMax - iMin + 1) * Rnd()) + iMin)
            End Function

        End Class

    End Namespace

    Namespace SQLInjectionPrevention

        Public Class R2CoreSQLInjectionPreventionManager
            Public Sub GeneralAuthorization(YourParam As String)
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim SqlInjectionPreventionKeywords = Split(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SqlInjectionPrevention, 0), " ")
                    Dim Wanted = YourParam.ToLower().Split(" ")
                    For Each Str As String In Wanted
                        If SqlInjectionPreventionKeywords.Any(Function(s) Str.Equals(s)) Or (New String() {";"}).Any(Function(s) Str.Equals(s)) Then
                            Throw New SqlInjectionException
                        End If
                    Next
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class



    End Namespace

    Namespace PasswordStrength
        Public Class PasswordStrength
            Private dtDetails As DataTable
            Private PreviousPassword As String = ""

            Public Sub SetPassword(pwd As String)
                If (PreviousPassword <> pwd) Then
                    PreviousPassword = pwd
                    CheckPasswordWithDetails(PreviousPassword)
                End If
            End Sub

            Public Function GetPasswordScore() As Integer
                If dtDetails IsNot Nothing Then
                    Return dtDetails.Rows(0)(5)
                Else
                    Return 0
                End If
            End Function

            Public Function GetPasswordStrength() As String
                If dtDetails IsNot Nothing Then
                    Return dtDetails.Rows(0)(3)
                Else
                    Return "Unknown"
                End If
            End Function

            Public Function GetStrengthDetails() As DataTable
                Return dtDetails
            End Function

            Private Sub CheckPasswordWithDetails(pwd As String)
                Dim nScore = 0
                Dim sComplexity As String = ""
                Dim iUpperCase As Integer = 0
                Dim iLowerCase As Integer = 0
                Dim iDigit As Integer = 0
                Dim iSymbol As Integer = 0
                Dim iRepeated As Integer = 1
                Dim htRepeated As Hashtable = New Hashtable()
                Dim iMiddle As Integer = 0
                Dim iMiddleEx As Integer = 1
                Dim ConsecutiveMode As Integer = 0
                Dim iConsecutiveUpper As Integer = 0
                Dim iConsecutiveLower As Integer = 0
                Dim iConsecutiveDigit As Integer = 0
                Dim iLevel As Integer = 0
                Dim sAlphas As String = "abcdefghijklmnopqrstuvwxyz"
                Dim sNumerics As String = "01234567890"
                Dim nSeqAlpha As Integer = 0
                Dim nSeqChar As Integer = 0
                Dim nSeqNumber As Integer = 0

                CreateDetailsTable()
                iLevel += 1
                Dim drScore As DataRow = AddDetailsRow(iLevel, "Score", "", "", 0, 0)

                ' Scan password
                For Each ch As Char In pwd.ToCharArray()
                    ' Count digits
                    If (Char.IsDigit(ch)) Then
                        iDigit += 1
                        If (ConsecutiveMode = 3) Then iConsecutiveDigit += 1
                        ConsecutiveMode = 3
                    End If

                    ' Count uppercase characters
                    If (Char.IsUpper(ch)) Then
                        iUpperCase += 1
                        If (ConsecutiveMode = 1) Then iConsecutiveUpper += 1
                        ConsecutiveMode = 1
                    End If

                    ' Count lowercase characters
                    If (Char.IsLower(ch)) Then
                        iLowerCase += 1
                        If (ConsecutiveMode = 2) Then iConsecutiveLower += 1
                        ConsecutiveMode = 2
                    End If

                    ' Count symbols
                    If (Char.IsSymbol(ch) Or Char.IsPunctuation(ch)) Then
                        iSymbol += 1
                        ConsecutiveMode = 0
                    End If

                    ' Count repeated letters 
                    If (Char.IsLetter(ch)) Then
                        If (htRepeated.Contains(Char.ToLower(ch))) Then
                            iRepeated += 1
                        Else
                            htRepeated.Add(Char.ToLower(ch), 0)
                        End If
                        If (iMiddleEx > 1) Then iMiddle = iMiddleEx - 1
                    End If

                    If (iUpperCase > 0 Or iLowerCase > 0) Then
                        If (Char.IsDigit(ch) Or Char.IsSymbol(ch)) Then iMiddleEx += 1
                    End If
                Next

                ' Check for sequential alpha string patterns (forward And reverse) 
                For s As Integer = 0 To 23 - 1
                    Dim sFwd As String = sAlphas.Substring(s, 3)
                    Dim sRev As String = strReverse(sFwd)
                    If (pwd.ToLower().IndexOf(sFwd) <> -1 Or pwd.ToLower().IndexOf(sRev) <> -1) Then
                        nSeqAlpha += 1
                        nSeqChar += 1
                    End If
                Next

                ' Check for sequential numeric string patterns (forward And reverse)
                For s As Integer = 0 To 8 - 1
                    Dim sFwd As String = sNumerics.Substring(s, 3)
                    Dim sRev As String = strReverse(sFwd)
                    If (pwd.ToLower().IndexOf(sFwd) <> -1 Or pwd.ToLower().IndexOf(sRev) <> -1) Then
                        nSeqNumber += 1
                        nSeqChar += 1
                    End If
                Next

                ' Calcuate score
                iLevel += 1
                AddDetailsRow(iLevel, "Additions", "", "", 0, 0)

                ' Score += 4 * Password Length
                nScore = 4 * pwd.Length
                iLevel += 1
                AddDetailsRow(iLevel, "Password Length", "Flat", "(n*4)", pwd.Length, pwd.Length * 4)

                ' if we have uppercase letetrs Score +=(number of uppercase letters *2)
                If (iUpperCase > 0) Then
                    nScore += ((pwd.Length - iUpperCase) * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, ((pwd.Length - iUpperCase) * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, 0)
                End If

                ' if we have lowercase letetrs Score +=(number of lowercase letters *2)
                If (iLowerCase > 0) Then
                    nScore += ((pwd.Length - iLowerCase) * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, ((pwd.Length - iLowerCase) * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, 0)
                End If

                ' Score += (Number of digits *4)
                nScore += (iDigit * 4)
                iLevel += 1
                AddDetailsRow(iLevel, "Numbers", "Cond", "+(n*4)", iDigit, (iDigit * 4))

                ' Score += (Number of Symbols * 6)
                nScore += (iSymbol * 6)
                iLevel += 1
                AddDetailsRow(iLevel, "Symbols", "Flat", "+(n*6)", iSymbol, (iSymbol * 6))

                ' Score += (Number of digits Or symbols in middle of password *2)
                nScore += (iMiddle * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Middle Numbers or Symbols", "Flat", "+(n*2)", iMiddle, (iMiddle * 2))

                'requirments
                Dim requirments As Integer = 0
                If (pwd.Length >= 8) Then requirments += 1     ' Min password length
                If (iUpperCase > 0) Then requirments += 1      ' Uppercase letters
                If (iLowerCase > 0) Then requirments += 1      ' Lowercase letters
                If (iDigit > 0) Then requirments += 1          ' Digits
                If (iSymbol > 0) Then requirments += 1         ' Symbols

                ' If we have more than 3 requirments then
                If (requirments > 3) Then
                    ' Score += (requirments *2) 
                    nScore += (requirments * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Requirments", "Flat", "+(n*2)", requirments, (requirments * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Requirments", "Flat", "+(n*2)", requirments, 0)
                End If


                ' Deductions
                iLevel += 1
                AddDetailsRow(iLevel, "Deductions", "", "", 0, 0)

                ' If only letters then score -=  password length
                If (iDigit = 0 And iSymbol = 0) Then
                    nScore -= pwd.Length
                    iLevel += 1
                    AddDetailsRow(iLevel, "Letters only", "Flat", "-n", pwd.Length, -pwd.Length)
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Letters only", "Flat", "-n", 0, 0)
                End If

                ' If only digits then score -=  password length
                If (iDigit = pwd.Length) Then
                    nScore -= pwd.Length
                    iLevel += 1
                    AddDetailsRow(iLevel, "Numbers only", "Flat", "-n", pwd.Length, -pwd.Length)
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Numbers only", "Flat", "-n", 0, 0)
                End If

                ' If repeated letters used then score -= (iRepeated * (iRepeated - 1));
                If (iRepeated > 1) Then
                    nScore -= (iRepeated * (iRepeated - 1))
                    iLevel += 1
                    AddDetailsRow(iLevel, "Repeat Characters (Case Insensitive)", "Incr", "-(n(n-1))", iRepeated, -(iRepeated * (iRepeated - 1)))
                End If

                ' If Consecutive uppercase letters then score -= (iConsecutiveUpper * 2);
                nScore -= (iConsecutiveUpper * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Uppercase Letters", "Flat", "-(n*2)", iConsecutiveUpper, -(iConsecutiveUpper * 2))

                ' If Consecutive lowercase letters then score -= (iConsecutiveUpper * 2);
                nScore -= (iConsecutiveLower * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Lowercase Letters", "Flat", "-(n*2)", iConsecutiveLower, -(iConsecutiveLower * 2))

                ' If Consecutive digits used then score -= (iConsecutiveDigits* 2);
                nScore -= (iConsecutiveDigit * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Numbers", "Flat", "-(n*2)", iConsecutiveDigit, -(iConsecutiveDigit * 2))

                ' If password contains sequence of letters then score -= (nSeqAlpha * 3)
                nScore -= (nSeqAlpha * 3)
                iLevel += 1
                AddDetailsRow(iLevel, "Sequential Letters (3+)", "Flat", "-(n*3)", nSeqAlpha, -(nSeqAlpha * 3))

                ' If password contains sequence of digits then score -= (nSeqNumber * 3)
                nScore -= (nSeqNumber * 3)
                iLevel += 1
                AddDetailsRow(iLevel, "Sequential Numbers (3+)", "Flat", "-(n*3)", nSeqNumber, -(nSeqNumber * 3))

                ' Determine complexity based on overall score 
                If (nScore > 100) Then
                    nScore = 100
                ElseIf (nScore < 0) Then
                    nScore = 0
                End If
                If (nScore >= 0 And nScore < 20) Then
                    sComplexity = "Very Weak"
                ElseIf (nScore >= 20 And nScore < 40) Then
                    sComplexity = "Weak"
                ElseIf (nScore >= 40 And nScore < 60) Then
                    sComplexity = "Good"
                ElseIf (nScore >= 60 And nScore < 80) Then
                    sComplexity = "Strong"
                ElseIf (nScore >= 80 And nScore <= 100) Then
                    sComplexity = "Very Strong"
                End If

                ' Store score And complexity in dataset
                drScore(5) = nScore
                drScore(3) = sComplexity
                dtDetails.AcceptChanges()
            End Sub

            Private Sub CreateDetailsTable()
                dtDetails = New DataTable("PasswordDetails")
                dtDetails.Columns.Add(New DataColumn("Level", System.Type.GetType("System.Int32")))
                dtDetails.Columns.Add(New DataColumn("Description", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Type", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Rate", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Count", System.Type.GetType("System.Int32")))
                dtDetails.Columns.Add(New DataColumn("Bonus", System.Type.GetType("System.Int32")))
            End Sub

            Private Function AddDetailsRow(Id As Integer, Description As String, Type As String, Rate As String, Count As Integer, Bouns As Integer) As DataRow
                Dim dr As DataRow = dtDetails.NewRow()
                dr(0) = Id
                dr(1) = Description
                dr(2) = Type
                dr(3) = Rate
                dr(4) = Count
                dr(5) = Bouns
                dtDetails.Rows.Add(dr)
                dtDetails.AcceptChanges()
                Return dr
            End Function

            Private Function strReverse(str As String) As String
                Dim newstring As String = ""
                For s As Integer = 0 To str.Length - 1
                    newstring = str(s) + newstring
                Next
                Return newstring
            End Function

        End Class


    End Namespace

    Namespace ExpressionValidationAlgorithms
        Public Class ExpressionValidationAlgorithmsManager
            Public Sub ValidationMobileNumber(YourValue As String)
                Try
                    If YourValue.Length <> 11 Then Throw New MobileNumberIsInvalidException
                    If Not IsNumeric(YourValue) Then Throw New MobileNumberIsInvalidException
                Catch ex As MobileNumberIsInvalidException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class

        Namespace Exceptions
            Public Class MobileNumberIsInvalidException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        'شماره موبایل صحیح نیست
                        Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CorePredefinedMessages.MobileNumberIsInvalid).MsgContent
                    End Get
                End Property
            End Class

        End Namespace

    End Namespace

    Namespace Exceptions

        Public Class CaptchaWordNotCorrectException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "متن تصویر امنیتی به درستی وارد نشده است"
                End Get
            End Property
        End Class

        Public Class ExchangeKeyNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کلید تبادل مورد نظر نامعتبر نیست"
                End Get
            End Property
        End Class

        Public Class ExchangeKeyTimeRangePassedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مدت زمان مجاز به پایان رسیده است"
                End Get
            End Property
        End Class

        Public Class SqlInjectionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوعی از ارتباط مخرب شناسایی شد"
                End Get
            End Property
        End Class


    End Namespace


End Namespace

Namespace PredefinedMessagesManagement

    Public MustInherit Class R2CorePredefinedMessages
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly IPISBlack As Int64 = 1
        Public Shared ReadOnly BlackIPTypeNotFound As Int64 = 2
        Public Shared ReadOnly MobileNumberIsInvalid As Int64 = 3
        Public Shared ReadOnly MobileNumberNotFoundException As Int64 = 6
        Public Shared ReadOnly SecurityHashInvalid As Int64 = 7
        Public Shared ReadOnly SoftwareUserAPIKeyExpired As Int64 = 8
        Public Shared ReadOnly SoftwareUserIsLogout As Int64 = 9
        Public Shared ReadOnly CaptchaInvalid As Int64 = 10
        Public Shared ReadOnly NonceExpired As Int64 = 11
        Public Shared ReadOnly SoftWareUserPasswordExpired As Int64 = 12
        Public Shared ReadOnly VerificationCodeExpired As Int64 = 13
        Public Shared ReadOnly PersonalNonceExpired As Int64 = 14



    End Class

    Public Class R2CoreStandardPredefinedMessageStructure

        Public Sub New()
            MyBase.New()
            MsgId = Int64.MinValue
            MsgName = String.Empty
            MsgTitle = String.Empty
            MsgContent = String.Empty
            Description = String.Empty
            InputLanguageType = Int16.MinValue
            MsgColor = Color.White
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            Active = Boolean.FalseString
            ViewFlag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourMsgId As Int64, YourMsgName As String, YourMsgTitle As String, YourMsgContent As String, YourDescription As String, YourInputLanguageType As Int16, YourMsgColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New()
            MsgId = YourMsgId
            MsgName = YourMsgName
            MsgTitle = YourMsgTitle
            MsgContent = YourMsgContent
            Description = YourDescription
            InputLanguageType = YourInputLanguageType
            MsgColor = YourMsgColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Active = YourActive
            ViewFlag = YourViewFlag
            Deleted = YourDeleted
        End Sub

        Public Property MsgId As Int64
        Public Property MsgName As String
        Public Property MsgTitle As String
        Public Property MsgContent As String
        Public Property Description As String
        Public Property InputLanguageType As Int16
        Public Property MsgColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public NotInheritable Class R2CoreMClassPredefinedMessagesManagement

        Public Shared Function GetNSS(YourMSGId As Int64) As R2CoreStandardPredefinedMessageStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblPredefinedMessages Where MsgId  = " & YourMSGId & " And Deleted=0", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New PredefinedMessageNotFoundException
                Dim NSS = New R2CoreStandardPredefinedMessageStructure
                NSS.MsgId = Ds.Tables(0).Rows(0).Item("MsgId")
                NSS.MsgName = Ds.Tables(0).Rows(0).Item("MsgName").trim
                NSS.MsgTitle = Ds.Tables(0).Rows(0).Item("MsgTitle").trim
                NSS.MsgContent = Ds.Tables(0).Rows(0).Item("MsgContent").trim
                NSS.Description = Ds.Tables(0).Rows(0).Item("Description").trim
                NSS.InputLanguageType = Ds.Tables(0).Rows(0).Item("InputLanguageType")
                NSS.MsgColor = Color.FromName(Ds.Tables(0).Rows(0).Item("MsgColor").trim)
                NSS.UserId = Ds.Tables(0).Rows(0).Item("UserId")
                NSS.DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi")
                NSS.DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi").trim
                NSS.ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")
                NSS.Active = Ds.Tables(0).Rows(0).Item("Active")
                NSS.Deleted = Ds.Tables(0).Rows(0).Item("Deleted")
                Return NSS
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function



    End Class

    Public Class R2CoreMClassPredefinedMessagesManager

        Public Function GetNSS(YourMSGId As Int64) As R2CoreStandardPredefinedMessageStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblPredefinedMessages Where MsgId  = " & YourMSGId & " And Deleted=0", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New PredefinedMessageNotFoundException
                Return New R2CoreStandardPredefinedMessageStructure(Ds.Tables(0).Rows(0).Item("MsgId"), Ds.Tables(0).Rows(0).Item("MsgName").trim, Ds.Tables(0).Rows(0).Item("MsgTitle").trim, Ds.Tables(0).Rows(0).Item("MsgContent").trim, Ds.Tables(0).Rows(0).Item("Description").trim, Ds.Tables(0).Rows(0).Item("InputLanguageType"), Color.FromName(Ds.Tables(0).Rows(0).Item("MsgColor").trim), Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As PredefinedMessageNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function



    End Class

    Namespace Exceptions
        Public Class PredefinedMessageNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پیام از پیش تعریف شده ای با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class


    End Namespace

End Namespace

Namespace EntityManagement

    Public MustInherit Class R2CoreEntities
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly SoftwareUsers As Int64 = 1
        Public Shared ReadOnly MobileProcesses As Int64 = 2
        Public Shared ReadOnly WebProcesses As Int64 = 3
        Public Shared ReadOnly Requesters As Int64 = 4
        Public Shared ReadOnly Computers As Int64 = 8
    End Class

    Public Class R2StandardEntityRelationTypeStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            EntId = Int64.MinValue
            EntName = String.Empty
            EntTitle = String.Empty
            TargetTable = String.Empty
            EntColor = Color.White
            IdFieldName = String.Empty
            TitleFieldName = String.Empty
            OrderFieldName = String.Empty
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourEntId As Int64, YourEntName As String, YourEntTitle As String, YourTargetTable As String, YourEntColor As Color, YourIdFieldName As String, YourTitleFieldName As String, YourOrderFieldName As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourEntId, YourEntName)
            EntId = YourEntId
            EntName = YourEntName
            EntTitle = YourEntTitle
            TargetTable = YourTargetTable
            EntColor = YourEntColor
            IdFieldName = YourIdFieldName
            TitleFieldName = YourTitleFieldName
            OrderFieldName = YourOrderFieldName
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property EntId As Int64
        Public Property EntName As String
        Public Property EntTitle As String
        Public Property TargetTable As String
        Public Property EntColor As Color
        Public Property IdFieldName As String
        Public Property TitleFieldName As String
        Public Property OrderFieldName As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class


End Namespace

Namespace EntityRelationManagement

    Public Enum RelationDeactiveTypes
        None = 0
        E1Deactive = 1
        E2Deactive = 2
        BothDeactive = 3
    End Enum

    Public MustInherit Class R2CoreEntityRelationTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly SoftwareUser_MobileProcessGroup As Int64 = 3
        Public Shared ReadOnly MobileProcessGroup_MobileProcess As Int64 = 4
        Public Shared ReadOnly WebProcessGroup_WebProcess As Int64 = 5
        Public Shared ReadOnly SoftwareUser_WebProcessGroup As Int64 = 6
        Public Shared ReadOnly OwnerShips_Personnels As Int64 = 7
        Public Shared ReadOnly OwnerShips_Computers As Int64 = 8
        Public Shared ReadOnly SoftwareUserType_SMSOwnerType As Int64 = 10

    End Class

    Public Class R2StandardEntityRelationTypeStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            _ERTypeId = Int64.MinValue
            _ERTypeName = String.Empty
            _ERTypeTitle = String.Empty
            _Color = Color.Empty
            _Core = String.Empty
            _UserId = Int64.MinValue
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourERTypeId As Int64, YourERTypeName As String, YourERTypeTitle As String, YourColor As Color, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourERTypeId, YourERTypeName)
            _ERTypeId = YourERTypeId
            _ERTypeName = YourERTypeName
            _ERTypeTitle = YourERTypeTitle
            _Color = YourColor
            _Core = YourCore
            _UserId = YourUserId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property ERTypeId As Int64
        Public Property ERTypeName As String
        Public Property ERTypeTitle As String
        Public Property Color As Color
        Public Property Core As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardEntityRelationStructure

        Public Sub New()
            MyBase.New()
            _ERId = Int64.MinValue
            _ERTypeId = Int64.MinValue
            _E1 = Int64.MinValue
            _E2 = Int64.MinValue
            _RelationActive = Boolean.FalseString
        End Sub

        Public Sub New(YourERId As Int64, YourERTypeId As Int64, YourE1 As Int64, YourE2 As Int64, YourRelationActive As Boolean)
            MyBase.New()
            _ERId = YourERId
            _ERTypeId = YourERTypeId
            _E1 = YourE1
            _E2 = YourE2
            _RelationActive = YourRelationActive
        End Sub

        Public Property ERId As Int64
        Public Property ERTypeId As Int64
        Public Property E1 As Int64
        Public Property E2 As Int64
        Public Property RelationActive As Boolean

    End Class

    Public Class R2CoreMClassEntityRelationManager

        Public Function RegisteringEntityRelation(YourNSSEntityRelation As R2StandardEntityRelationStructure, YourDeactive As RelationDeactiveTypes) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                If YourDeactive = RelationDeactiveTypes.E1Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourNSSEntityRelation.E1 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.E2Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E2=" & YourNSSEntityRelation.E2 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.BothDeactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where (E1=" & YourNSSEntityRelation.E1 & " or E2=" & YourNSSEntityRelation.E2 & ") and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.None Then
                End If
                CmdSql.ExecuteNonQuery()

                CmdSql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblEntityRelations') "
                Dim ERIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive) Values(" & YourNSSEntityRelation.ERTypeId & "," & YourNSSEntityRelation.E1 & "," & YourNSSEntityRelation.E2 & ",1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return ERIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreMClassEntityRelationManagement
        Public Shared Function GetNSSEntityRelationType(YourEntityRelationTypeId As Int64) As R2StandardEntityRelationTypeStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblEntityRelationTypes as ERTypes Where ERTypes.ERTypeId=" & YourEntityRelationTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationTypeNotFoundException
                Dim NSS = New R2StandardEntityRelationTypeStructure
                NSS.ERTypeId = Ds.Tables(0).Rows(0).Item("ERTypeId")
                NSS.ERTypeName = Ds.Tables(0).Rows(0).Item("ERTypeName").TRIM
                NSS.ERTypeTitle = Ds.Tables(0).Rows(0).Item("ERTypeTitle").TRIM
                NSS.Color = Color.FromName(Ds.Tables(0).Rows(0).Item("Color").TRIM)
                NSS.Core = Ds.Tables(0).Rows(0).Item("Core").trim
                NSS.UserId = Ds.Tables(0).Rows(0).Item("UserId")
                NSS.DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi")
                NSS.DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi").TRIM
                NSS.ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")
                NSS.Active = Ds.Tables(0).Rows(0).Item("Active")
                NSS.Deleted = Ds.Tables(0).Rows(0).Item("Deleted")
                Return NSS
            Catch ex As EntityRelationTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSEntityRelation(YourEntityRelationId As Int64) As R2StandardEntityRelationStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblEntityRelations as ERs Where ERs.ERId=" & YourEntityRelationId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                Dim NSS = New R2StandardEntityRelationStructure
                NSS.ERId = Ds.Tables(0).Rows(0).Item("ERId")
                NSS.ERTypeId = Ds.Tables(0).Rows(0).Item("ERTypeId")
                NSS.E1 = Ds.Tables(0).Rows(0).Item("E1")
                NSS.E2 = Ds.Tables(0).Rows(0).Item("E2")
                NSS.RelationActive = Ds.Tables(0).Rows(0).Item("RelationActive")
                Return NSS
            Catch ex As EntityRelationNotFoundException

            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function RegisteringEntityRelation(YourNSSEntityRelation As R2StandardEntityRelationStructure, YourDeactive As RelationDeactiveTypes) As Int64
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                If YourDeactive = RelationDeactiveTypes.E1Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E1=" & YourNSSEntityRelation.E1 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.E2Deactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where E2=" & YourNSSEntityRelation.E2 & " and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.BothDeactive Then
                    CmdSql.CommandText = "Update R2Primary.dbo.TblEntityRelations Set RelationActive=0 Where (E1=" & YourNSSEntityRelation.E1 & " or E2=" & YourNSSEntityRelation.E2 & ") and ERTypeId=" & YourNSSEntityRelation.ERTypeId & ""
                ElseIf YourDeactive = RelationDeactiveTypes.None Then
                End If
                CmdSql.ExecuteNonQuery()

                CmdSql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblEntityRelations') "
                Dim ERIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive) Values(" & YourNSSEntityRelation.ERTypeId & "," & YourNSSEntityRelation.E1 & "," & YourNSSEntityRelation.E2 & ",1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return ERIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSEntityRelation(YourERTypeId As Int64, YourERId1 As Int64, YourERId2 As Int64) As R2StandardEntityRelationStructure
            Try
                Dim Ds As DataSet
                If YourERId1 = Int64.MinValue Then
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E2=" & YourERId2 & " and ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                ElseIf YourERId2 = Int64.MinValue Then
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E1=" & YourERId1 & " and ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                Else
                    If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 ERId from R2Primary.dbo.TblEntityRelations as ERs Where ERs.E1=" & YourERId1 & " and ERs.E2=" & YourERId2 & " ERs.ERTypeId=" & YourERTypeId & " and RelationActive=1 Order By ERId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New EntityRelationNotFoundException
                    Return GetNSSEntityRelation(Ds.Tables(0).Rows(0).Item("ERId"))
                End If
            Catch ex As EntityRelationNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function RegisteringEntityRelations(YourEntityRelationTypeId As Int64, YourE1Id As Int64, YourE2Ids As String()) As Int64
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To YourE2Ids.Count - 1
                    Cmdsql.CommandText = "Select Top 1 ERId From R2Primary.dbo.TblEntityRelations With (tablockx) Order By ERId Desc"
                    Cmdsql.ExecuteNonQuery()
                    Dim ERIdNew As Int64 = Cmdsql.ExecuteScalar() + 1
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblEntityRelations(ERTypeId,E1,E2,RelationActive)
                                          Values(" & YourEntityRelationTypeId & "," & YourE1Id & "," & YourE2Ids(Loopx) & ",1)"
                    Cmdsql.ExecuteNonQuery()
                Next
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class EntityRelationTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع رابطه نهادی با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class EntityRelationNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "رابطه نهادی با شماره شاخص مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace PermissionManagement

    Public MustInherit Class R2CorePermissionTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly SoftwareUsersAccessMobileProcesses As Int64 = 1
        Public Shared ReadOnly SoftwareUsersAccessWebProcesses As Int64 = 2
        Public Shared ReadOnly UserCanSendSoftwareUserShenasehPasswordViaSMS As Int64 = 7
        Public Shared ReadOnly UserCanViewAndPrintUserShenasehPassword As Int64 = 8
        Public Shared ReadOnly UserCanInject123VerificationCodeforAppActivation As Int64 = 9
        Public Shared ReadOnly SoftwareUserCanActivateUnactivateSMSOwner As Int64 = 22
        Public Shared ReadOnly UserCanChargeSMSControllingMoneyWallet As Int64 = 30
    End Class

    Public Class R2StandardPermissionTypeStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            PTId = Int64.MinValue
            PTName = String.Empty
            PTTitle = String.Empty
            PTColor = Color.White
            EntityTableId1 = Int64.MinValue
            EntityTableId2 = Int64.MinValue
            Description = String.Empty
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourPTId As Int64, YourPTName As String, YourPTTitle As String, YourPTColor As Color, YourEntityTableId1 As Int64, YourEntityTableId2 As Int64, YourDescription As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPTId, YourPTName)
            PTId = YourPTId
            PTName = YourPTName
            PTTitle = YourPTTitle
            PTColor = YourPTColor
            EntityTableId1 = YourEntityTableId1
            EntityTableId2 = YourEntityTableId2
            Description = YourDescription
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PTId As Int64
        Public Property PTName As String
        Public Property PTTitle As String
        Public Property PTColor As Color
        Public Property EntityTableId1 As Int64
        Public Property EntityTableId2 As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardPermissionStructure
        Inherits BaseStandardClass.R2StandardStructure

        Public Sub New()
            MyBase.New()
            PId = Int64.MinValue
            EntityIdFirst = Int64.MinValue
            EntityIdSecond = Int64.MinValue
            PermissionTypeId = Int64.MinValue
            RelationActive = Boolean.FalseString
        End Sub

        Public Sub New(YourPId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64, YourPermissionTypeId As Int64, YourRelationActive As Boolean)
            MyBase.New(YourPId, String.Empty)
            PId = YourPId
            EntityIdFirst = YourEntityIdFirst
            EntityIdSecond = YourEntityIdSecond
            PermissionTypeId = YourPermissionTypeId
            RelationActive = YourRelationActive
        End Sub

        Public Property PId As Int64
        Public Property EntityIdFirst As Int64
        Public Property EntityIdSecond As Int64
        Public Property PermissionTypeId As Int64
        Public Property RelationActive As Boolean

    End Class

    Public Class R2CoreInstansePermissionsManager
        Public Function ExistPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64) As Boolean
            Try
                Dim Instanse = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If Instanse.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select * from R2Primary.dbo.TblPermissions as Permissions 
                       Where Permissions.PermissionTypeId=" & YourPermissionTypeId & " and Permissions.RelationActive=1 and Permissions.EntityIdFirst=" & YourEntityIdFirst & " and Permissions.EntityIdSecond=" & YourEntityIdSecond & "", 32000, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As PermissionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreMClassPermissionsManagement

        Public Shared Sub RegisteringPermissions(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdsSecond As String())
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To YourEntityIdsSecond.Count - 1
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblPermissions(EntityIdFirst,EntityIdSecond,PermissionTypeId,RelationActive) 
                                          Values(" & YourEntityIdFirst & "," & YourEntityIdsSecond(Loopx) & "," & YourPermissionTypeId & ",1)"
                    Cmdsql.ExecuteNonQuery()
                Next
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function ExistPermission(YourPermissionTypeId As Int64, YourEntityIdFirst As Int64, YourEntityIdSecond As Int64) As Boolean
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                      "Select * from R2Primary.dbo.TblPermissions as Permissions 
                       Where Permissions.PermissionTypeId=" & YourPermissionTypeId & " and Permissions.RelationActive=1 and Permissions.EntityIdFirst=" & YourEntityIdFirst & " and Permissions.EntityIdSecond=" & YourEntityIdSecond & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class PermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی وجود ندارد"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace WebProcessesManagement

    Public Class R2StandardWebProcessStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PId = Int64.MinValue
            PName = String.Empty
            PTitle = String.Empty
            TargetWfProcess = String.Empty
            Description = String.Empty
            PBackColor = Color.Black
            PForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPId As Int64, ByVal YourPName As String, ByVal YourPTitle As String, ByVal YourTargetWfProcess As String, ByVal YourDescription As String, YourPBackColor As Color, YourPForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPId, YourPName.Trim())
            PId = YourPId
            PName = YourPName
            PTitle = YourPTitle
            TargetWfProcess = YourTargetWfProcess
            Description = YourDescription
            PBackColor = YourPBackColor
            PForeColor = YourPForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PId As String
        Public Property PName As String
        Public Property PTitle As String
        Public Property TargetWfProcess As String
        Public Property Description As String
        Public Property PBackColor As Color
        Public Property PForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    Public Class R2StandardWebProcessGroupStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PGId = Int64.MinValue
            PGName = String.Empty
            PGTitle = String.Empty
            PGBackColor = Color.Black
            PGForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPGId As Int64, ByVal YourPGName As String, ByVal YourPGTitle As String, YourPGBackColor As Color, YourPGForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPGId, YourPGName.Trim())
            PGId = YourPGId
            PGName = YourPGName
            PGTitle = YourPGTitle
            PGBackColor = YourPGBackColor
            PGForeColor = YourPGForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PGId As String
        Public Property PGName As String
        Public Property PGTitle As String
        Public Property PGBackColor As Color
        Public Property PGForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    'Public Class R2CoreWebProcesseGroupsStructure
    '    Public WGId As Int64
    '    Public WGTitle As String
    'End Class

    'Public Class R2CoreWebProcesseStructure
    '    Public WPId As Int64
    '    Public WPTitle As String
    '    Public WPDescription As String
    'End Class

    Public Class R2CoreWebProcessesManager
        Public Function GetWebProcesses(YourAPIKey As String) As String
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select WebProcessGroups.PGTitle,WebProcesses.PTitle,WebProcesses.PName from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                         Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
                         Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroups On SoftwareUserWebProcessGroup.E2=WebProcessGroups.PGId
						 Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroups.PGId=WebProcessGroupWebProcess.E1 
						 Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses on  WebProcessGroupWebProcess.E2=WebProcesses.PId 
						 Inner Join R2Primary.dbo.TblPermissions as [Permissions] On   WebProcesses.PId=[Permissions].EntityIdSecond  
                  Where SoftwareUser.APIKey='" & YourAPIKey & "' and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0and 
				        SoftwareUserWebProcessGroup.ERTypeId=" & R2Core.EntityRelationManagement.R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & " and SoftwareUserWebProcessGroup.RelationActive=1 and  
						WebProcessGroups.ViewFlag=1 and  WebProcessGroups.Active=1 and WebProcessGroups.Deleted=0 and
						WebProcessGroupWebProcess.RelationActive=1 and
						WebProcesses.Active=1 and WebProcesses.ViewFlag=1 and WebProcesses.Deleted=0 and
						[Permissions].PermissionTypeId=" & R2Core.PermissionManagement.R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & "
			      Order By WebProcessGroups.PGId,WebProcesses.PId 
				  for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item(0)
                Else
                    Throw New SoftwareUserHasNotAnyWebProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyWebProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function GetWebProcesses() As List(Of R2CoreWebProcesseStructure)
        '    Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '            "Select  WebProcesses.PId,WebProcesses.PTitle,WebProcesses.Description from R2Primary.dbo.TblWebProcessGroups as WebProcessGroups
        '              inner join R2Primary.dbo.TblEntityRelations as EntityRelations on WebProcessGroups.PGId = EntityRelations.E1 
        '              inner join R2Primary.dbo.TblWebProcesses as WebProcesses on EntityRelations.E2=WebProcesses.PId 
        '             where EntityRelations.ERTypeId=5 and EntityRelations.RelationActive=1 and WebProcessGroups.Active=1 and WebProcessGroups.ViewFlag=1 and WebProcessGroups.Deleted=0
        '                   and WebProcesses.Active=1 and WebProcesses.Deleted=0 and WebProcesses.ViewFlag =1
        '             order by WebProcessGroups.PGId ", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Dim Lst As New List(Of R2CoreWebProcesseStructure)
        '            For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
        '                Dim NSS As New R2CoreWebProcesseStructure
        '                NSS.WPId = Ds.Tables(0).Rows(Loopx).Item("PId")
        '                NSS.WPTitle = Ds.Tables(0).Rows(Loopx).Item("PTitle").Trim
        '                NSS.WPDescription = Ds.Tables(0).Rows(Loopx).Item("Description").Trim
        '                Lst.Add(NSS)
        '            Next
        '            Return Lst
        '        Else
        '            Throw New WebProccessGroupHasNotAnyWebProcessRelationException
        '        End If
        '    Catch exx As WebProccessGroupHasNotAnyWebProcessRelationException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetWebProcceseName(YourWebProcessId As Int64) As String
        'Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '            "select * from R2Primary.dbo.TblWebProcesses where PId=" & YourWebProcessId & "", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item("PName").trim
        '        Else
        '            Throw New WebProccessNotFoundException
        '        End If
        '    Catch exx As WebProccessNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function


        'Public Shared Function GetWebProcesses(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2StandardWebProcessStructure)
        '    Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '         "Select WebProcesses.* 
        '           from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
        '             Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
        '             Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroup On SoftwareUserWebProcessGroup.E2=WebProcessGroup.PGId 
        '             Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroup.PGId=WebProcessGroupWebProcess.E1  
        '             Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses On WebProcessGroupWebProcess.E2=WebProcesses.PId 
        '             Inner Join R2Primary.dbo.TblPermissions  as Permission On WebProcesses.PId=Permission.EntityIdSecond 
        '           Where SoftwareUser.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0 and SoftwareUserWebProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & "
        '                 and SoftwareUserWebProcessGroup.RelationActive=1 and  WebProcessGroup.ViewFlag=1 and  WebProcessGroup.Active=1 and WebProcessGroup.Deleted=0 and WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & "  
        '                 and WebProcessGroupWebProcess.RelationActive=1 and WebProcesses.ViewFlag=1 and WebProcesses.Active=1 and WebProcesses.Deleted=0 
        '                 and Permission.RelationActive=1 and Permission.EntityIdFirst=" & YourNSSSoftwareUser.UserId & " and PermissionTypeId=" & R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & "
        '           Order By WebProcessGroup.PGId,WebProcesses.PId ", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Dim Lst As New List(Of R2StandardWebProcessStructure)
        '            For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
        '                Dim NSS As New R2StandardWebProcessStructure
        '                NSS.PId = Ds.Tables(0).Rows(Loopx).Item("PId")
        '                NSS.PName = Ds.Tables(0).Rows(Loopx).Item("PName")
        '                NSS.PTitle = "  " + Ds.Tables(0).Rows(Loopx).Item("PTitle").ToString().Trim
        '                NSS.Description = Ds.Tables(0).Rows(Loopx).Item("Description").trim
        '                NSS.PForeColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PForeColor").trim)
        '                NSS.PBackColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PBackColor").trim)
        '                NSS.TargetWfProcess = Ds.Tables(0).Rows(Loopx).Item("TargetWfProcess").trim
        '                NSS.UserId = Ds.Tables(0).Rows(Loopx).Item("UserId")
        '                NSS.DateTimeMilladi = Ds.Tables(0).Rows(Loopx).Item("DateTimeMilladi")
        '                NSS.DateShamsi = Ds.Tables(0).Rows(Loopx).Item("DateShamsi").trim
        '                NSS.Active = Ds.Tables(0).Rows(Loopx).Item("Active")
        '                NSS.ViewFlag = Ds.Tables(0).Rows(Loopx).Item("ViewFlag")
        '                NSS.Deleted = Ds.Tables(0).Rows(Loopx).Item("Deleted")
        '                Lst.Add(NSS)
        '            Next
        '            Return Lst
        '        Else
        '            Throw New SoftwareUserHasNotAnyWebProcessPermissionException
        '        End If
        '    Catch exx As SoftwareUserHasNotAnyWebProcessPermissionException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    Namespace Exceptions
        Public Class SoftwareUserHasNotAnyWebProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از منوهای سایت را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotAnyWebProcessGroupRelationException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از گروه منوهای سایت را ندارید"
                End Get
            End Property
        End Class

        Public Class WebProccessGroupHasNotAnyWebProcessRelationException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "گروه منوی مورد نظر دارای زیر گروه تعریف شده ای نیست"
                End Get
            End Property
        End Class

        Public Class WebProccessNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "منو با کد شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace MobileProcessesManagement

    Public MustInherit Class R2CoreMobileProcesses
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly UserLast5Digit = 8
        Public Shared ReadOnly Advertising = 9
    End Class

    Public Class R2StandardMobileProcessStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PId = Int64.MinValue
            PName = String.Empty
            PTitle = String.Empty
            TargetMobilePage = String.Empty
            TargetMobilePageDelegate = String.Empty
            Description = String.Empty
            PBackColor = Color.Black
            PForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPId As Int64, ByVal YourPName As String, ByVal YourPTitle As String, ByVal YourTargetMobilePage As String, YourTargetMobilePageDelegate As String, ByVal YourDescription As String, YourPBackColor As Color, YourPForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPId, YourPName.Trim())
            PId = YourPId
            PName = YourPName
            PTitle = YourPTitle
            TargetMobilePage = YourTargetMobilePage
            TargetMobilePageDelegate = YourTargetMobilePageDelegate
            Description = YourDescription
            PBackColor = YourPBackColor
            PForeColor = YourPForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PId As String
        Public Property PName As String
        Public Property PTitle As String
        Public Property TargetMobilePage As String
        Public Property TargetMobilePageDelegate As String
        Public Property Description As String
        Public Property PBackColor As Color
        Public Property PForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    Public Class R2StandardMobileProcessGroupStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PGId = Int64.MinValue
            PGName = String.Empty
            PGTitle = String.Empty
            PGBackColor = Color.Black
            PGForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPGId As Int64, ByVal YourPGName As String, ByVal YourPGTitle As String, YourPGBackColor As Color, YourPGForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPGId, YourPGName.Trim())
            PGId = YourPGId
            PGName = YourPGName
            PGTitle = YourPGTitle
            PGBackColor = YourPGBackColor
            PGForeColor = YourPGForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PGId As String
        Public Property PGName As String
        Public Property PGTitle As String
        Public Property PGBackColor As Color
        Public Property PGForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    Public Class R2CoreInstanceMobileProcessesManager
        Public Function GetMobileProcesses(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2StandardMobileProcessStructure)
            Try
                Dim Instanse = New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet
                If Instanse.GetDataBOX(New R2PrimarySqlConnection,
                             "Select PId,PName,PTitle,TargetMobilePage,TargetMobilePageDelegate,Description,PForeColor,PBackColor 
                              from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                                 Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserMobileProcessGroup On SoftwareUser.UserId=SoftwareUserMobileProcessGroup.E1 
                                 Inner Join R2Primary.dbo.TblMobileProcessGroups as MobileProcessGroup On SoftwareUserMobileProcessGroup.E2=MobileProcessGroup.PGId 
                                 Inner Join R2Primary.dbo.TblEntityRelations as MobileProcessGroupMobileProcess On MobileProcessGroup.PGId=MobileProcessGroupMobileProcess.E1  
                                 Inner Join R2Primary.dbo.TblMobileProcesses as MobileProcesses On MobileProcessGroupMobileProcess.E2=MobileProcesses.PId 
                              Where SoftwareUser.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0 and SoftwareUserMobileProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_MobileProcessGroup & "
                                    and SoftwareUserMobileProcessGroup.RelationActive=1 and  MobileProcessGroup.ViewFlag=1 and  MobileProcessGroup.Active=1 and MobileProcessGroup.Deleted=0 and MobileProcessGroupMobileProcess.ERTypeId=" & R2CoreEntityRelationTypes.MobileProcessGroup_MobileProcess & " 
                                    and MobileProcessGroupMobileProcess.RelationActive=1 and MobileProcesses.ViewFlag=1 and MobileProcesses.Active=1 and MobileProcesses.Deleted=0 
                              Order By MobileProcessGroup.PGId,MobileProcesses.PId ", 32000, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Dim Lst As New List(Of R2StandardMobileProcessStructure)
                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Dim NSS As New R2StandardMobileProcessStructure
                        NSS.PId = Ds.Tables(0).Rows(Loopx).Item("PId")
                        NSS.PName = Ds.Tables(0).Rows(Loopx).Item("PName")
                        NSS.PTitle = "  " + Ds.Tables(0).Rows(Loopx).Item("PTitle").ToString().Trim
                        NSS.TargetMobilePage = Ds.Tables(0).Rows(Loopx).Item("TargetMobilePage").trim
                        NSS.TargetMobilePageDelegate = Ds.Tables(0).Rows(Loopx).Item("TargetMobilePageDelegate").trim
                        NSS.Description = Ds.Tables(0).Rows(Loopx).Item("Description").trim
                        NSS.PForeColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PForeColor").trim)
                        NSS.PBackColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PBackColor").trim)
                        Lst.Add(NSS)
                    Next
                    Return Lst
                Else
                    Throw New SoftwareUserHasNotAnyMobileProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyMobileProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreMClassMobileProcessesManagement

        Public Shared Function GetMobileProcesses(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2StandardMobileProcessStructure)
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                             "Select PId,PName,PTitle,TargetMobilePage,TargetMobilePageDelegate,Description,PForeColor,PBackColor 
                              from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                                 Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserMobileProcessGroup On SoftwareUser.UserId=SoftwareUserMobileProcessGroup.E1 
                                 Inner Join R2Primary.dbo.TblMobileProcessGroups as MobileProcessGroup On SoftwareUserMobileProcessGroup.E2=MobileProcessGroup.PGId 
                                 Inner Join R2Primary.dbo.TblEntityRelations as MobileProcessGroupMobileProcess On MobileProcessGroup.PGId=MobileProcessGroupMobileProcess.E1  
                                 Inner Join R2Primary.dbo.TblMobileProcesses as MobileProcesses On MobileProcessGroupMobileProcess.E2=MobileProcesses.PId 
                              Where SoftwareUser.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0 and SoftwareUserMobileProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_MobileProcessGroup & "
                                    and SoftwareUserMobileProcessGroup.RelationActive=1 and  MobileProcessGroup.ViewFlag=1 and  MobileProcessGroup.Active=1 and MobileProcessGroup.Deleted=0 and MobileProcessGroupMobileProcess.ERTypeId=" & R2CoreEntityRelationTypes.MobileProcessGroup_MobileProcess & " 
                                    and MobileProcessGroupMobileProcess.RelationActive=1 and MobileProcesses.ViewFlag=1 and MobileProcesses.Active=1 and MobileProcesses.Deleted=0 
                              Order By MobileProcessGroup.PGId,MobileProcesses.PId ", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Dim Lst As New List(Of R2StandardMobileProcessStructure)
                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Dim NSS As New R2StandardMobileProcessStructure
                        NSS.PId = Ds.Tables(0).Rows(Loopx).Item("PId")
                        NSS.PName = Ds.Tables(0).Rows(Loopx).Item("PName")
                        NSS.PTitle = "  " + Ds.Tables(0).Rows(Loopx).Item("PTitle").ToString().Trim
                        NSS.TargetMobilePage = Ds.Tables(0).Rows(Loopx).Item("TargetMobilePage").trim
                        NSS.TargetMobilePageDelegate = Ds.Tables(0).Rows(Loopx).Item("TargetMobilePageDelegate").trim
                        NSS.Description = Ds.Tables(0).Rows(Loopx).Item("Description").trim
                        NSS.PForeColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PForeColor").trim)
                        NSS.PBackColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PBackColor").trim)
                        Lst.Add(NSS)
                    Next
                    Return Lst
                Else
                    Throw New SoftwareUserHasNotAnyMobileProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyMobileProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function




    End Class

    Namespace Exceptions
        Public Class SoftwareUserHasNotAnyMobileProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از منوهای اپلیکیشن را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotPermissionforThisMobileProcessException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به این فرآیند را ندارید"
                End Get
            End Property
        End Class



    End Namespace

End Namespace

Namespace WhatsAppMessenger

End Namespace

Namespace RequesterManagement

    Public MustInherit Class R2CoreRequesterTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly Desktop As Int64 = 1
        Public Shared ReadOnly Web As Int64 = 2
        Public Shared ReadOnly Mobile As Int64 = 3
    End Class

    Public MustInherit Class R2CoreRequesters
        Public Shared ReadOnly None As Int64 = 0
    End Class

    Public Class R2StandardRequesterStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            RqId = Int64.MinValue
            RqName = String.Empty
            RqTitle = String.Empty
            RqTypeId = Int64.MinValue
            Description = String.Empty
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourRqId As Int64, ByVal YourRqName As String, ByVal YourRqTitle As String, ByVal YourRqTypeId As Int64, ByVal YourDescription As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourRqId, YourRqName.Trim())
            RqId = YourRqId
            RqName = YourRqName
            RqTitle = YourRqTitle
            RqTypeId = YourRqTypeId
            Description = YourDescription
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property RqId As Int64
        Public Property RqName As String
        Public Property RqTitle As String
        Public Property RqTypeId As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class


End Namespace

Namespace BlackIPs

    Public Class R2StandardBlackIPTypeStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            BlackIPTypeId = Int64.MinValue
            BlackIPName = String.Empty
            LockMinutes = Int64.MinValue
            StrategyQuery = String.Empty
            Color = Color.Black
            Description = String.Empty
            DateTimeMilladi = DateTime.Now
            DateShamsi = String.Empty
            Time = String.Empty
            Active = Boolean.FalseString
            Viewflag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourBlackIPTypeId As Int64, ByVal YourBlackIPName As String, ByVal YourLockMinutes As Int64, YourStrategyQuery As String, ByVal YourColor As Color, ByVal YourDescription As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewflag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourBlackIPTypeId, YourBlackIPName.Trim())
            BlackIPTypeId = YourBlackIPTypeId
            BlackIPName = YourBlackIPName
            LockMinutes = YourLockMinutes
            StrategyQuery = YourStrategyQuery
            Color = YourColor
            Description = YourDescription
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Time = YourTime
            Active = YourActive
            Viewflag = YourViewflag
            Deleted = YourDeleted
        End Sub


        Public Property BlackIPTypeId As Int64
        Public Property BlackIPName As String
        Public Property LockMinutes As Int64
        Public Property StrategyQuery As String
        Public Property Color As Color
        Public Property Description As String
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property Viewflag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2StandardBlackIPStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            BlackIPId = Int64.MinValue
            BlackIP = String.Empty
            TypeId = Int64.MinValue
            LockStatus = Boolean.FalseString
            LockMinutes = Int64.MinValue
            DateTimeMilladi = DateTime.Now
            DateShamsi = String.Empty
            Time = String.Empty
            Active = Boolean.FalseString
            Viewflag = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourBlackIPId As Int64, ByVal YourBlackIP As String, ByVal YourTypeId As Int64, ByVal YourLockStatus As Boolean, ByVal YourLockMinutes As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewflag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourBlackIPId, YourBlackIP.Trim())
            BlackIPId = YourBlackIPId
            BlackIP = YourBlackIP
            TypeId = YourTypeId
            LockStatus = YourLockStatus
            LockMinutes = YourLockMinutes
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            Time = YourTime
            Active = YourActive
            Viewflag = YourViewflag
            Deleted = YourDeleted
        End Sub

        Public Property BlackIPId As Int64
        Public Property BlackIP As String
        Public Property TypeId As Int64
        Public Property LockStatus As Boolean
        Public Property LockMinutes As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property Viewflag As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreInstanceBlackIPsManager
        Private _DateTime As New R2DateTime

        Public Sub DoStrategyControl()
            Try
                'اجرای استراتژی ها
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DsBlackIPTypes As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                    "Select BlackIPTypeId,StrategyQuery 
                     from R2Primary.dbo.TblBlackIPTypes 
                     Where Active=1 and Deleted=0", 3600, DsBlackIPTypes, New Boolean)
                For Loopx As Int64 = 0 To DsBlackIPTypes.Tables(0).Rows.Count - 1
                    Dim Ds As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, DsBlackIPTypes.Tables(0).Rows(Loopx).Item("StrategyQuery"), 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                        Dim SB As New StringBuilder
                        For Loopy As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                            SB.Append(Ds.Tables(0).Rows(Loopy).Item("BlackIP").trim).Append(";")
                        Next
                        RegisterBlackIPs(Left(SB.ToString, SB.ToString.Length - 1), DsBlackIPTypes.Tables(0).Rows(Loopx).Item("BlackIPTypeId"))
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSBlackIPType(YourBlackIPTypeId As Int64) As R2StandardBlackIPTypeStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2Primary.dbo.TblBlackIPTypes Where BlackIPTypeId=" & YourBlackIPTypeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New BlackIPTypeNotFoundException
                Return New R2StandardBlackIPTypeStructure(YourBlackIPTypeId, DS.Tables(0).Rows(0).Item("BlackIPName").trim, DS.Tables(0).Rows(0).Item("LockMinutes"), DS.Tables(0).Rows(0).Item("StrategyQuery").trim, Color.FromName(DS.Tables(0).Rows(0).Item("Color").trim), DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As BlackIPTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Sub RegisterBlackIP(YourNSS As R2StandardBlackIPStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblBlackIPs(BlackIP,TypeId,LockStatus,LockMinutes,DateTimeMilladi,DateShamsi,Time,Active,Viewflag,Deleted) Values('" & YourNSS.BlackIP & "'," & YourNSS.TypeId & ",1," & YourNSS.LockMinutes & ",'" & _DateTime.GetCurrentDateTimeMilladiFormated & "','" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime & "',1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisterBlackIP(YourIP As String, YourTypeId As Int64)
            Try
                RegisterBlackIP(New R2StandardBlackIPStructure(0, YourIP, YourTypeId, True, GetNSSBlackIPType(YourTypeId).LockMinutes, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RegisterBlackIPs(YourBlackIPs As String, YourBlackIPTypeId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim NSSBlackIPTypeId = GetNSSBlackIPType(YourBlackIPTypeId)
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim BlackIPs As String() = YourBlackIPs.Split(";")
                For Each BIP As String In BlackIPs
                    CmdSql.CommandText = "IF NOT EXISTS(SELECT 1 FROM R2Primary.dbo.TblBlackIPs Where BlackIP='" & BIP & "' and LockStatus=1 and DATEDIFF(minute,DateTimeMilladi,getdate())<=LockMinutes)
                                               Insert Into R2Primary.dbo.TblBlackIPs(BlackIP,TypeId,LockStatus,LockMinutes,DateTimeMilladi,DateShamsi,Time,Active,Viewflag,Deleted) Values('" & BIP & "'," & NSSBlackIPTypeId.BlackIPTypeId & ",1," & NSSBlackIPTypeId.LockMinutes & ",'" & _DateTime.GetCurrentDateTimeMilladiFormated & "','" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function IsBlackIPActive(YourNSS As R2StandardBlackIPStructure)
            Try
                'فراموش نشود باید بررسی شود و فعال شود
                Return False
                'SqlInjectionPrevention
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourNSS.BlackIP)

                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet = Nothing
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Top 1 BlackIPId from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourNSS.BlackIP & "' and LockStatus=1 and DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes", 0, DS, New Boolean).GetRecordsCount = 0 Then Return False
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AuthorizationIP(YourIP As String)
            Try
                If IsBlackIPActive(New R2StandardBlackIPStructure(0, YourIP, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)) Then Throw New AuthorizationIPIPIsBlackException
            Catch ex As AuthorizationIPIPIsBlackException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub UnLockBlackIP(YourNSS As R2StandardBlackIPStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2Primary.dbo.TblBlackIPs Set LockStatus=0 Where BlackIP='" & YourNSS.BlackIP & "'"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetBlackIPBlackRecords(YourIP As String, Optional YourTypeId As Int64 = Int64.MinValue) As List(Of R2StandardBlackIPStructure)
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim Lst As New List(Of R2StandardBlackIPStructure)
                Dim DS As DataSet = Nothing
                If YourTypeId = Int64.MinValue Then
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select * from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourIP & "' and LockStatus=1 and 
                               DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes order By DateTimeMilladi Desc", 0, DS, New Boolean)
                Else
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                        "Select * from R2Primary.dbo.TblBlackIPs 
                         Where BlackIP='" & YourIP & "' and LockStatus=1 and TypeId=" & YourTypeId & " and 
                               DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=LockMinutes order By DateTimeMilladi Desc", 0, DS, New Boolean)
                End If
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS = New R2StandardBlackIPStructure(DS.Tables(0).Rows(Loopx).Item("BlackIPId"), DS.Tables(0).Rows(Loopx).Item("BlackIP").trim, DS.Tables(0).Rows(Loopx).Item("TypeId"), DS.Tables(0).Rows(Loopx).Item("LockStatus"), DS.Tables(0).Rows(Loopx).Item("LockMinutes"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class BlackIPTypeNotFoundException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    'نوع آی پی سیاه با شاخص مورد نظر یافت نشد
                    Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CorePredefinedMessages.BlackIPTypeNotFound).MsgContent
                End Get
            End Property
        End Class

        Public Class AuthorizationIPIPIsBlackException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    'آی پی در لیست سیاه قرار دارد
                    Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CorePredefinedMessages.IPISBlack).MsgContent
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace MoneyWallet

    Namespace MoneyWallet

        Public Class R2CoreStandardMoneyWalletStructure
            Inherits R2StandardStructure

            Public Sub New()
                MyBase.New()
                MoneyWalletId = Int64.MinValue
                Charge = Int64.MinValue
                MoneyWalletTypeId = Int64.MinValue
                UserId = Int64.MinValue
                DateTimeMilladi = Now
                DateShamsi = String.Empty
                Time = String.Empty
                Active = Boolean.FalseString
                ViewFlag = Boolean.FalseString
                Deleted = Boolean.TrueString
            End Sub

            Public Sub New(YourMoneyWalletId As Int64, YourCharge As Int64, YourMoneyWalletTypeId As Int64, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
                MyBase.New(YourMoneyWalletId, YourMoneyWalletId)
                MoneyWalletId = YourMoneyWalletId
                Charge = YourCharge
                MoneyWalletTypeId = YourMoneyWalletTypeId
                UserId = YourUserId
                DateTimeMilladi = YourDateTimeMilladi
                DateShamsi = YourDateShamsi
                Time = YourTime
                Active = YourActive
                ViewFlag = YourViewFlag
                Deleted = YourDeleted
            End Sub

            Public Property MoneyWalletId As Int64
            Public Property Charge As Int64
            Public Property MoneyWalletTypeId As Int64
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property Active As Boolean
            Public Property ViewFlag As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreMClassMoneyWalletManager

        End Class

    End Namespace

    Namespace PaymentRequests

        Public Class R2StandardPaymentRequestStructure

            Public Sub New()
                MyBase.New()
                PayId = Int64.MinValue
                MCSSId = Int64.MinValue
                SoftwareUserId = Int64.MinValue
                Amount = Int64.MinValue
                Authority = String.Empty
                TransactionId = String.Empty
                RefId = String.Empty
                PaymentErrors = String.Empty
                VerificationErrors = String.Empty
                VerificationCount = String.Empty
                UserId = Int64.MinValue
                DateTimeMilladi = Now
                DateShamsi = String.Empty
                Time = String.Empty
            End Sub

            Public Sub New(YourPayId As Int64, YourMCSSId As Int64, YourSoftwareUserId As Int64, YourAmount As Int64, YourAuthority As String, YourTransactionId As String, YourRefId As String, YourPaymentErrors As String, YourVerificationErrors As String, YourVerificationCount As Byte, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String)
                MyBase.New
                PayId = YourPayId
                MCSSId = YourMCSSId
                SoftwareUserId = YourSoftwareUserId
                Amount = YourAmount
                Authority = YourAuthority
                TransactionId = YourTransactionId
                RefId = YourRefId
                PaymentErrors = YourPaymentErrors
                VerificationErrors = YourVerificationErrors
                VerificationCount = YourVerificationCount
                UserId = YourUserId
                DateTimeMilladi = YourDateTimeMilladi
                DateShamsi = YourDateShamsi
                Time = YourTime
            End Sub


            Public Property PayId As Int64
            Public Property MCSSId As Int64 'MonetaryCreditSupplySource
            Public Property SoftwareUserId As Int64 'Payment for This UserId Relation MoneyWallet
            Public Property Amount As Int64
            Public Property Authority As String
            Public Property TransactionId As String
            Public Property RefId As String
            Public Property PaymentErrors As String
            Public Property VerificationErrors As String
            Public Property VerificationCount As Byte
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String

        End Class

        Public Class R2CoreInstansePaymentRequestsManager
            Private _DateTime As New R2DateTime
            Private WithEvents MS As MonetarySupply.R2CoreMonetarySupply = Nothing
            Private PayId As Int64

            Private Function PaymentRequestRegistering(YourMCSSId As Int64, YourAmount As Int64, YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Select Top 1 PayId From R2Primary.dbo.TblPaymentRequests with (tablockx) Order By PayId Desc"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblPaymentRequests') "
                    Dim PayIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    CmdSql.CommandText = "Insert Into R2Primary.dbo.TblPaymentRequests(MCSSId,SoftwareUserId,Amount,Authority,TransactionId,RefId,PaymentErrors,VerificationErrors,VerificationCount,UserId,DateTimeMilladi,DateShamsi,Time) 
                                          Values(" & YourMCSSId & "," & YourSoftwareUserId & "," & YourAmount & ",'','','','','',1," & InstanceSoftwareUsers.GetNSSSystemUser().UserId & ",'" & _DateTime.GetCurrentDateTimeMilladiFormated() & "','" & _DateTime.GetCurrentDateShamsiFull() & "','" & _DateTime.GetCurrentTime() & "')"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return PayIdNew
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSPayment(YourAuthority As String) As R2StandardPaymentRequestStructure
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblPaymentRequests Where Authority='" & YourAuthority & "'", 0, Ds, New Boolean)
                    Return New R2StandardPaymentRequestStructure(Ds.Tables(0).Rows(0).Item("PayId"), Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("SoftwareUserId"), Ds.Tables(0).Rows(0).Item("Amount"), Ds.Tables(0).Rows(0).Item("Authority").trim, Ds.Tables(0).Rows(0).Item("TransactionId").trim, Ds.Tables(0).Rows(0).Item("RefId").trim, Ds.Tables(0).Rows(0).Item("PaymentErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationCount"), Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub DecreaseVerificationCount(YourPayId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2Primary.dbo.TblPaymentRequests Set VerificationCount=VerificationCount-1 Where PayId=" & YourPayId & ""
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function GetNSSPayment(YourPayId As Int64) As R2StandardPaymentRequestStructure
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim Ds As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblPaymentRequests Where PayId=" & YourPayId & "", 0, Ds, New Boolean)
                    Return New R2StandardPaymentRequestStructure(Ds.Tables(0).Rows(0).Item("PayId"), Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("SoftwareUserId"), Ds.Tables(0).Rows(0).Item("Amount"), Ds.Tables(0).Rows(0).Item("Authority").trim, Ds.Tables(0).Rows(0).Item("TransactionId").trim, Ds.Tables(0).Rows(0).Item("RefId").trim, Ds.Tables(0).Rows(0).Item("PaymentErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationCount"), Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function PaymentRequest(YourMCSSId As Int64, YourAmount As Int64, YourSoftwareUserId As Int64) As Int64
                Try
                    PayId = PaymentRequestRegistering(YourMCSSId, YourAmount, YourSoftwareUserId)
                    Dim InstanceMCSS As New R2CoreMClassMonetaryCreditSupplySourcesManager
                    MS = New R2CoreMonetarySupply(InstanceMCSS.GetNSSMonetaryCreditSupplySource(YourMCSSId), YourAmount)
                    MS.StartSupply()
                    Return PayId
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function VerificationRequest(YourMCSSId As Int64, YourAuthority As String) As Int64
                Try
                    PayId = GetNSSPayment(YourAuthority).PayId
                    Dim Amount = GetNSSPayment(YourAuthority).Amount
                    Dim InstanceMCSS = New R2CoreMClassMonetaryCreditSupplySourcesManager
                    MS = New R2CoreMonetarySupply(InstanceMCSS.GetNSSMonetaryCreditSupplySource(YourMCSSId), Amount)
                    MS.StartVerification(YourAuthority)
                    Return PayId
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Sub MS_MonetarySupplySuccessEvent(YourMonetarySupplyType As MonetarySupplyType, TransactionId As Long, Amount As Long, SupplyReport As String) Handles MS.MonetarySupplySuccessEvent
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    If YourMonetarySupplyType = MonetarySupplyType.PaymentRequest Then
                        CmdSql.CommandText = "Update R2Primary.dbo.TblPaymentRequests Set Authority='" & SupplyReport & "',TransactionId='" & TransactionId & "' Where PayId=" & PayId & ""
                    ElseIf YourMonetarySupplyType = MonetarySupplyType.VerificationRequest Then
                        CmdSql.CommandText = "Update R2Primary.dbo.TblPaymentRequests Set RefId='" & SupplyReport & "' Where PayId=" & PayId & ""
                    Else
                        Throw New Exception
                    End If
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Private Sub MS_MonetarySupplyUnSuccessEvent(YourMonetarySupplyType As MonetarySupplyType, TransactionId As Long, Amount As Long, SupplyReport As String) Handles MS.MonetarySupplyUnSuccessEvent
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    CmdSql.Connection.Open()
                    If YourMonetarySupplyType = MonetarySupplyType.PaymentRequest Then
                        CmdSql.CommandText = "Update R2Primary.dbo.TblPaymentRequests Set PaymentErrors='" & IIf(SupplyReport <> String.Empty, SupplyReport, "Empty Error") & "' Where PayId=" & PayId & ""
                    ElseIf YourMonetarySupplyType = MonetarySupplyType.VerificationRequest Then
                        CmdSql.CommandText = "Update R2Primary.dbo.TblPaymentRequests Set VerificationErrors='" & IIf(SupplyReport <> String.Empty, SupplyReport, "Empty Error") & "' Where PayId=" & PayId & ""
                    Else
                        Throw New Exception
                    End If
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub


        End Class


    End Namespace

    Namespace MoneyWalletCharging

        Public Class R2CoreInstanceMoneyWalletChargingManager
            Private _DateTime As New R2DateTime


        End Class


    End Namespace

    Namespace Exceptions
        Public Class SoftwareUserMoneyWalletNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کیف پول مرتبط با کاربر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace SMS

    Namespace SendSMSCodes
        Public MustInherit Class R2CoreSendSMSCodes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property Success = 1
            Public Shared ReadOnly Property InvalidMobileNumber = 2
            Public Shared ReadOnly Property SMSOwnerIsSendingActiveIsfalse = 3
            Public Shared ReadOnly Property InsufficientSMSMoneyWalletCharge = 4
            Public Shared ReadOnly Property SMSOwnerNotExist = 5
            Public Shared ReadOnly Property DelayToStartSMSSending = 6

        End Class

        Public Class R2CoreStandardSendSMSCodeStructure

            Public Sub New()
                MyBase.New()
                _SendSMSCodeId = Int64.MinValue
                _SendSMSCodeName = String.Empty
                _SendSMSCodeTitle = String.Empty
                _Description = String.Empty
                _SendSMSCode = String.Empty
                _DateTimeMilladi = DateAndTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSendSMSCodeId As Int64, YourSendSMSCodeName As String, YourSendSMSCodeTitle As String, YourDescription As String, YourSendSMSCode As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SendSMSCodeId = YourSendSMSCodeId
                _SendSMSCodeName = YourSendSMSCodeName
                _SendSMSCodeTitle = YourSendSMSCodeTitle
                _Description = YourDescription
                _SendSMSCode = YourSendSMSCode
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SendSMSCodeId As Int64
            Public Property SendSMSCodeName As String
            Public Property SendSMSCodeTitle As String
            Public Property Description As String
            Public Property SendSMSCode As String
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreSendSMSCodeManager
            Public Function GetNSSSendSMSCode(YourSendSMSCodeId As Int64) As R2CoreStandardSendSMSCodeStructure
                Try
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                         "Select * from R2PrimarySMSSystem.dbo.TblSendSMSCodes Where SendSMSCodeId=" & YourSendSMSCodeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New SendSMSCodeIdNotFoundExcption
                    Else
                        Return New R2CoreStandardSendSMSCodeStructure(DS.Tables(0).Rows(0).Item("SendSMSCodeId"), DS.Tables(0).Rows(0).Item("SendSMSCodeName").trim, DS.Tables(0).Rows(0).Item("SendSMSCodeTitle").trim, DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("SendSMSCode").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    End If
                Catch ex As SendSMSCodeIdNotFoundExcption
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SendSMSCodeIdNotFoundExcption
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "کد شاخص مرتبط با اعلان وضعیت ارسال اس ام اس موجود نیست"
                    End Get
                End Property

            End Class

        End Namespace

    End Namespace

    Namespace RecivedSMSCodes
        Public MustInherit Class RecivedSMSCodes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property ActivateSMSOwner = 1
            Public Shared ReadOnly Property UNActivateSMSOwner = 2
        End Class

        Public Class R2CoreStandardRecivedSMSCodeStructure

            Public Sub New()
                MyBase.New()
                _SMSRCId = Int64.MinValue
                _SMSRCName = String.Empty
                _SMSRCTitle = String.Empty
                _SMSCode = String.Empty
                _EndMinutes = Int64.MinValue
                _Core = String.Empty
                _AssemblyDll = String.Empty
                _AssemblyPath = String.Empty
                _DateTimeMilladi = DateAndTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSRCId As Int64, YourSMSRCName As String, YourSMSRCTitle As String, YourSMSCode As String, YourEndMinutes As Int64, YourCore As String, YourAssemblyDll As String, YourAssemblyPath As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SMSRCId = YourSMSRCId
                _SMSRCName = YourSMSRCName
                _SMSRCTitle = YourSMSRCTitle
                _SMSCode = YourSMSCode
                _EndMinutes = YourEndMinutes
                _Core = YourCore
                _AssemblyDll = YourAssemblyDll
                _AssemblyPath = YourAssemblyPath
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub
            Public Property SMSRCId As Int64
            Public Property SMSRCName As String
            Public Property SMSRCTitle As String
            Public Property SMSCode As String
            Public Property EndMinutes As Int64
            Public Property Core As String
            Public Property AssemblyDll As String
            Public Property AssemblyPath As String
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreRecivedSMSCodesManager
            Public Function GetNSSRecivedSMSCode(YourSMSCode As String) As R2CoreStandardRecivedSMSCodeStructure
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSMSCode)

                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSRecivedCoding Where SMSCode='" & Mid(YourSMSCode.Trim, 1, 3) & "' and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New SMSRecivedCodeforSMSCodeNotFoundException
                    Return New R2CoreStandardRecivedSMSCodeStructure(DS.Tables(0).Rows(0).Item("SMSRCId"), DS.Tables(0).Rows(0).Item("SMSRCName").trim, DS.Tables(0).Rows(0).Item("SMSRCTitle").trim, DS.Tables(0).Rows(0).Item("SMSCode").trim, DS.Tables(0).Rows(0).Item("EndMinutes"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("AssemblyDll").trim, DS.Tables(0).Rows(0).Item("AssemblyPath").trim, DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As SMSRecivedCodeforSMSCodeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SMSRecivedCodeforSMSCodeNotFoundException
                Inherits ApplicationException

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "کد ارسالی در اس ام اس دریافتی تعریف نشده است"
                    End Get
                End Property

            End Class

        End Namespace

    End Namespace

    Namespace SMSHandling

        Public MustInherit Class RecievedSMSHandler
            Public Event HandlingEvent()


            Public Sub New()
                Try
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Protected _MobileNumber As String = String.Empty
            Public Property MobileNumber As String
                Get
                    Return _MobileNumber
                End Get
                Set(value As String)
                    _MobileNumber = value
                End Set
            End Property

            Protected _SMSContent As String = String.Empty
            Public Property SMSContent As String
                Get
                    Return _SMSContent
                End Get
                Set(value As String)
                    _SMSContent = value
                End Set
            End Property

            Protected _NSSUser As R2CoreStandardSoftwareUserStructure = Nothing
            Public Property NSSUser As R2CoreStandardSoftwareUserStructure
                Get
                    Return _NSSUser
                End Get
                Set(value As R2CoreStandardSoftwareUserStructure)
                    _NSSUser = value
                End Set
            End Property

            Public Sub Handling()
                RaiseEvent HandlingEvent()
            End Sub

        End Class

        Public Class R2CoreStandardSMSStructure
            Public Sub New()
                MyBase.New()
                SmsId = Int64.MinValue
                MobileNumber = String.Empty
                Message = String.Empty
                EndMinutes = Int64.MinValue
                DateTimeMilladi = Now.Date
                Active = Boolean.FalseString
                DateShamsi = String.Empty
                Time = String.Empty
            End Sub

            Public Sub New(ByVal YourSmsId As Int64, ByVal YourMobileNumber As String, ByVal YourMessage As String, ByVal YourEndMinutes As Int64, ByVal YourDateTimeMilladi As DateTime, ByVal YourActive As Boolean, ByVal YourDateShamsi As String, ByVal YourTime As String)
                SmsId = YourSmsId
                MobileNumber = YourMobileNumber
                Message = YourMessage
                EndMinutes = YourEndMinutes
                DateTimeMilladi = YourDateTimeMilladi
                Active = YourActive
                DateShamsi = YourDateShamsi
                Time = YourTime
            End Sub

            Public Property SmsId As Int64
            Public Property MobileNumber As String
            Public Property Message As String
            Public Property EndMinutes As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property Active As Boolean
            Public Property DateShamsi As String
            Public Property Time As String
        End Class

        Public Structure SMSCreationData
            Public Data1, Data2, Data3, Data4, Data5, Data6, Data7, Data8, Data9, Data10 As String
        End Structure

        Public Class R2CoreSMSHandlingManager

            Private _DateTime As New R2DateTime

            Public Function GetSMSResultAnalyze(YourLst As List(Of KeyValuePair(Of Int64, String))) As String
                Try
                    Dim SB = New StringBuilder
                    Dim InstanceSendSMSCode = New R2CoreSendSMSCodeManager
                    For LoopxLst As Int64 = 0 To YourLst.Count - 1
                        If YourLst(LoopxLst).Key = SendSMSCodes.R2CoreSendSMSCodes.Success Then
                            SB.Append(String.Empty)
                        Else
                            SB.AppendLine(YourLst(LoopxLst).Value + "  " + InstanceSendSMSCode.GetNSSSendSMSCode(YourLst(LoopxLst).Key).Description)
                        End If
                    Next
                    Return SB.ToString
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function SendSMS(YourSoftwareUsers As List(Of R2CoreStandardSoftwareUserStructure), YourSMSTypeId As Int64, YourSMSCreationData As List(Of SMSCreationData), YourChekDelayFromActivation As Boolean) As List(Of KeyValuePair(Of Int64, String))
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim LstResult = New List(Of KeyValuePair(Of Int64, String))
                    Dim myCurrentDateTime = _DateTime.GetCurrentDateTime
                    'بررسی معادل بودن تعداد اعضاء لیست ها
                    If YourSoftwareUsers.Count <> YourSMSCreationData.Count Then Throw New CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    'بررسی پارامترهای ورودی
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceSMSOwners = New R2CoreMClassSMSOwnersManager
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourSMSTypeId)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    'CmdSql.CommandText = "Select Top 1 SMSId from R2PrimarySMSSystem.dbo.TblSMSWareHouse with (tablockx) Order By SMSId Desc"
                    'CmdSql.ExecuteNonQuery()
                    Dim SMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    For Loopx As Int64 = 0 To YourSoftwareUsers.Count - 1
                        Try
                            SMSOwner = InstanceSMSOwners.GetNSSSMSOwner(YourSoftwareUsers(Loopx), False)
                        Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerNotExist, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        Catch ex As Exception
                            Throw ex
                        End Try
                        If YourSoftwareUsers(Loopx).MobileNumber.Trim.Length <> InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 7) Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InvalidMobileNumber, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If Not SMSOwner.IsSendingActive Then
                            LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.SMSOwnerIsSendingActiveIsfalse, YourSoftwareUsers(Loopx).MobileNumber))
                            Continue For
                        End If
                        If NSSSMSType.Price <> 0 And SMSOwner.ReminderCharge <> 0 Then
                            If SMSOwner.ReminderCharge - SMSOwner.ReminderHolder <= NSSSMSType.Price Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.InsufficientSMSMoneyWalletCharge, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        If YourChekDelayFromActivation Then
                            If DateDiff(DateInterval.Minute, SMSOwner.DateTimeMilladi, myCurrentDateTime.DateTimeMilladi) < InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 16) Then
                                LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.DelayToStartSMSSending, YourSoftwareUsers(Loopx).MobileNumber))
                                Continue For
                            End If
                        End If
                        Dim SMSContent = GetCompositedSMSCreationData(NSSSMSType, YourSMSCreationData(Loopx))
                        Dim SMS = New R2CoreStandardSMSStructure(Nothing, YourSoftwareUsers(Loopx).MobileNumber, SMSContent, NSSSMSType.SMSMinutes, Nothing, Nothing, Nothing, Nothing)
                        CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & SMS.MobileNumber & "','" & SMS.Message & "'," & SMS.EndMinutes & ",'" & myCurrentDateTime.DateTimeMilladiFormated & "',1,'" & myCurrentDateTime.DateShamsiFull & "'," & R2CoreSMSSendReciveType.ForSend & ")"
                        CmdSql.ExecuteNonQuery()
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set ReminderHolder=ReminderHolder+" & NSSSMSType.Price & "
                                              Where SMSOwnerUserId=" & YourSoftwareUsers(Loopx).UserId & " and IsSendingActive=1"
                        CmdSql.ExecuteNonQuery()
                        LstResult.Add(New KeyValuePair(Of Long, String)(R2CoreSendSMSCodes.Success, YourSoftwareUsers(Loopx).MobileNumber))
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    Return LstResult
                Catch ex As CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As SMSTypeIdNotFoundException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function SendSMS(YourNSSSMSTypeid As Int64, YourNSSUserTypeId As Int64, YourSMSCreationData As SMSCreationData) As List(Of KeyValuePair(Of Int64, String))
                Try
                    Dim InstanceSMSOwners = New R2Core.SMS.SMSOwners.R2CoreMClassSMSOwnersManager
                    Dim LstSoftwareUsers = InstanceSMSOwners.GetNSSSoftwareUsers(YourNSSUserTypeId)
                    Return SendSMS(LstSoftwareUsers, YourNSSSMSTypeid, RepeatSMSCreationData(YourSMSCreationData, LstSoftwareUsers.Count), True)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Sub RecivedSMSHandling(YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select * from R2PrimarySMSSystem.dbo.TblSMSWareHouse as SMSWareHouse
                          Inner Join R2PrimarySMSSystem.DBO.TblSMSRecivedCoding as SMSRecivedCoding On Substring(SMSWareHouse.Message,1,3)=SMSRecivedCoding.SMSCode 
                      Where SMSWareHouse.Active=1 and SMSWareHouse.SmsType=" & R2CoreSMSSendReciveType.Recived & " and SMSRecivedCoding.Active=1 and SMSRecivedCoding.Deleted=0", 0, DS, New Boolean).GetRecordsCount = 0 Then Return
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim PathDll As String = Application.StartupPath + "\" + DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim
                        Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
                        Dim HandlerClassPath As String = DS.Tables(0).Rows(Loopx).Item("AssemblyPath").Trim()
                        Dim HandlerInstanceType = Assembly_.GetType(HandlerClassPath)
                        Dim HandlerObj = CType(Activator.CreateInstance(HandlerInstanceType), RecievedSMSHandler)
                        HandlerObj.MobileNumber = DS.Tables(0).Rows(Loopx).Item("MobileNumber")
                        HandlerObj.NSSUser = YourNSSUser
                        HandlerObj.SMSContent = DS.Tables(0).Rows(Loopx).Item("Message").trim
                        Try
                            HandlerObj.Handling()
                        Catch ex As Exception
                        End Try
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0 Where SMSId=" & Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("SMSId")) & ""
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Sub SMSGarbage()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    CmdSql.Connection.Open()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSWareHouse Set Active=0
                                          Where Active=1 and DATEDIFF(MINUTE ,DateTimeMilladi,GETDATE()) > EndMinutes"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

            Public Function RepeatSMSCreationData(YourSMSCreationData As SMSCreationData, YourCount As Int64) As List(Of SMSCreationData)
                Try
                    Dim Lst = New List(Of SMSCreationData)
                    For Loopx As Int64 = 0 To YourCount - 1
                        Lst.Add(YourSMSCreationData)
                    Next
                    Return Lst
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function GetCompositedSMSCreationData(YourNSSSMSType As R2CoreStandardSMSTypeStructure, YourData As SMSCreationData) As String
                Try
                    Dim InstanceSMSTypes = New R2CoreMClassSMSTypesManager
                    Dim NSSSMSType = InstanceSMSTypes.GetNSSSMSType(YourNSSSMSType.SMSTypeId)
                    Dim SMSContent = NSSSMSType.SMSTypeContent.Replace("Data1", YourData.Data1).Replace("Data2", YourData.Data2).Replace("Data3", YourData.Data3).Replace("Data4", YourData.Data4).Replace("Data5", YourData.Data5).Replace("Data6", YourData.Data6).Replace("Data7", YourData.Data7).Replace("Data8", YourData.Data8).Replace("Data9", YourData.Data9).Replace("Data10", YourData.Data10)
                    Return SMSContent
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions

            Public Class MobileNumberInvallidException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شماره موبایل " + _Message.Trim + " نادرست است"
                    End Get
                End Property

            End Class

            Public Class CreateSMSFailedArrayofSoftwareUserNotEqualtoArrayofSMSCreationDataException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "تعداد کاربران و تعداد داده ارسالی برای ارسال اس ام اس معادل نیستند"
                    End Get
                End Property
            End Class

            Public Class SMSResultException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return _Message.Trim
                    End Get
                End Property

            End Class


        End Namespace


    End Namespace

    Namespace SMSSendRecive

        Public Enum R2CoreSMSSendReciveType
            None = 0
            ForSend = 1
            Recived = 2
        End Enum

        Public Class R2CoreSMSSendingManager
            Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
            Private _DateTime As New R2DateTime

            Public Sub Sending(YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException
                    Dim InstanceLogging = New R2CoreInstanceLoggingManager

                    'ایجاد لیست اس ام اس ها
                    Dim DS As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select * from R2PrimarySMSSystem.dbo.TblSmsWareHouse where Active=1 and SmsType=" & R2CoreSMSSendReciveType.ForSend & " and DATEDIFF(MINUTE,DateTimeMilladi,GETDATE())<=EndMinutes and Message<>''", 0, DS, New Boolean).GetRecordsCount = 0 Then Return
                    Dim LstMessage() As String = New String(DS.Tables(0).Rows.Count - 1) {}
                    Dim LstMobileNumber() As String = New String(DS.Tables(0).Rows.Count - 1) {}
                    Dim LstSMSId() As Int64 = New Int64(DS.Tables(0).Rows.Count - 1) {}
                    For LoopSMSs As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstMessage(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("Message").trim
                        LstMobileNumber(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("MobileNumber").trim
                        LstSMSId(LoopSMSs) = DS.Tables(0).Rows(LoopSMSs).Item("SMSId")
                    Next

                    'ارسال اس ام اس ها
                    Dim SentSmsStatus() As Long = _SepahanSMS.SendSms(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), LstMessage, LstMobileNumber, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4),
                                                  net.sepahansms.SendType.DynamicText, net.sepahansms.SmsMode.SaveInPhone)

                    'غیرفعال کردن اس ام اس های ارسال شده و بدون خطا
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For LoopSMSs As Int64 = 0 To SentSmsStatus.Count - 1
                        Dim mySMSId = LstSMSId(LoopSMSs)
                        If SentSmsStatus(LoopSMSs) > 0 Then
                            CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSmsWareHouse Set Active=0 where SmsId=" & mySMSId & ""
                            CmdSql.ExecuteNonQuery()
                        Else
                            InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.SendSMSResult, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), LstSMSId(LoopSMSs).ToString(), SentSmsStatus(LoopSMSs).ToString(), String.Empty, String.Empty, String.Empty, YourNSSUser.UserId, Nothing, Nothing))
                        End If
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SmsSystemIsDisabledException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

        End Class

        Public Class R2CoreSMSRecivingManager
            Private _SepahanSMS As New net.sepahansms.smsSendWebServiceforPHP()
            Private _DateTime As New R2DateTime

            Public Sub Reciving()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try

                    Dim CurrentDateTime = _DateTime.GetCurrentDateTime

                    'دریافتی
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.SmsSystemSetting, 0) Then Throw New SmsSystemIsDisabledException

                    Dim ReceiveSmsOneLine = _SepahanSMS.GetReceiveSMSWithNumber(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 3), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 5), net.sepahansms.ReceiveType.UnRead, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 4), 10, "", "")
                    If ReceiveSmsOneLine.Count = 0 Then Exit Sub

                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    CmdSql.CommandText = "Select Top 1 SMSId from R2PrimarySMSSystem.dbo.TblSMSWareHouse with (tablockx) Order By SMSId Desc"
                    CmdSql.ExecuteNonQuery()
                    For loopx As Int32 = 0 To ReceiveSmsOneLine.Count - 1
                        Dim SmsText As String = ReceiveSmsOneLine(loopx).SmsText
                        Dim MobileNumber As String = ReceiveSmsOneLine(loopx).FromNumber
                        Dim myDate As String = ReceiveSmsOneLine(loopx).Date
                        Try
                            Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                            InstanceSQLInjectionPrevention.GeneralAuthorization(SmsText)
                            InstanceSQLInjectionPrevention.GeneralAuthorization(MobileNumber)
                            InstanceSQLInjectionPrevention.GeneralAuthorization(myDate)
                        Catch ex As SqlInjectionException
                            Continue For
                        Catch ex As Exception
                            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                        End Try
                        Dim InstanceRecivedSMSCodes = New R2CoreRecivedSMSCodesManager
                        Dim NSSRecivedSMSCode As R2CoreStandardRecivedSMSCodeStructure = Nothing
                        Try
                            NSSRecivedSMSCode = InstanceRecivedSMSCodes.GetNSSRecivedSMSCode(SmsText)
                        Catch ex As SMSRecivedCodeforSMSCodeNotFoundException
                            Continue For
                        Catch ex As Exception
                            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                        End Try
                        CmdSql.CommandText = "Insert into R2PrimarySMSSystem.dbo.TblSMSWareHouse(MobileNumber,Message,EndMinutes,DateTimeMilladi,Active,DateShamsi,SmsType) values('" & MobileNumber & "','" & SmsText & "'," & NSSRecivedSMSCode.EndMinutes & ",'" & CurrentDateTime.DateTimeMilladiFormated & "',1,'" & CurrentDateTime.DateShamsiFull & "'," & R2CoreSMSSendReciveType.Recived & ")"
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()

                Catch ex As SmsSystemIsDisabledException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try

            End Sub

        End Class

        Namespace Exceptions
            Public Class SmsMessageEmptyException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "در پیام ارسالی متنی وجود ندارد"
                    End Get
                End Property
            End Class

        End Namespace

    End Namespace

    Namespace SMSTypes

        Public MustInherit Class R2CoreSMSTypes
            Public Shared ReadOnly Property None = 0
            Public Shared ReadOnly Property SoftwareUserSecurity = 2
            Public Shared ReadOnly Property ApplicationActivationCode = 3
            Public Shared ReadOnly Property SMSControllingMoneyWallet = 7
            Public Shared ReadOnly Property PleaseCharge = 8
            Public Shared ReadOnly Property ATISMobileAppDownloadLink = 14
            Public Shared ReadOnly Property ActivateSMSOwnerSuccess = 16

        End Class

        Public Class R2CoreStandardSMSTypeStructure
            Inherits R2StandardStructure

            Public Sub New()
                MyBase.New()
                _SMSTypeId = Int64.MinValue
                _SMSTypeName = String.Empty
                _SMSTypeTitle = String.Empty
                _SMSTypeColor = Color.Black
                _Price = Int64.MinValue
                _SMSTypeContent = String.Empty
                _SMSMinutes = Int64.MinValue
                _Core = String.Empty
                _UserId = Int64.MinValue
                _DateTimeMilladi = DateAndTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSTypeId As Int64, YourSMSTypeName As String, YourSMSTypeTitle As String, YourSMSTypeColor As Color, YourPrice As Int64, YourSMSTypeContent As String, YourSMSMinutes As Int64, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                MyBase.New(YourSMSTypeId, YourSMSTypeTitle)
                _SMSTypeId = YourSMSTypeId
                _SMSTypeName = YourSMSTypeName
                _SMSTypeTitle = YourSMSTypeTitle
                _SMSTypeColor = YourSMSTypeColor
                _Price = YourPrice
                _SMSTypeContent = YourSMSTypeContent
                _SMSMinutes = YourSMSMinutes
                _Core = YourCore
                _UserId = YourUserId
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSTypeId As Int64
            Public Property SMSTypeName As String
            Public Property SMSTypeTitle As String
            Public Property SMSTypeColor As Color
            Public Property Price As Int64
            Public Property SMSTypeContent As String
            Public Property SMSMinutes As Int64
            Public Property Core As String
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreMClassSMSTypesManager

            Public Function GetNSSSMSType(YourSMSTypeId As Int64) As R2CoreStandardSMSTypeStructure
                Try
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim DS As DataSet = Nothing
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                            "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSTypes Where SMSTypeId=" & YourSMSTypeId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New SMSTypeIdNotFoundException
                    Return New R2CoreStandardSMSTypeStructure(YourSMSTypeId, DS.Tables(0).Rows(0).Item("SMSTypeName").trim, DS.Tables(0).Rows(0).Item("SMSTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSTypeColor").trim), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("SMSTypeContent").trim, DS.Tables(0).Rows(0).Item("SMSMinutes"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As SMSTypeIdNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreStandardSMSTypeStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Lst As New List(Of R2CoreStandardSMSTypeStructure)
                    Dim DS As DataSet = Nothing
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                       "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes Where SMSTypeTitle Like '%" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "%' 
                        and ViewFlag=1 Order By SMSTypeTitle", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(DS.Tables(0).Rows(Loopx).Item("SMSTypeId"), DS.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim, DS.Tables(0).Rows(Loopx).Item("Price"), DS.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, DS.Tables(0).Rows(Loopx).Item("SMSMinutes"), DS.Tables(0).Rows(Loopx).Item("Core").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreStandardSMSTypeStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Lst As New List(Of R2CoreStandardSMSTypeStructure)
                    Dim DS As DataSet = Nothing
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                             "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes 
                              Where Left(SMSTypeTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' Order By SMSTypeTitle", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(DS.Tables(0).Rows(Loopx).Item("SMSTypeId"), DS.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim, DS.Tables(0).Rows(Loopx).Item("Price"), DS.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, DS.Tables(0).Rows(Loopx).Item("SMSMinutes"), DS.Tables(0).Rows(Loopx).Item("Core").trim, DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSTypes(YourSearchString As String) As List(Of R2StandardStructure)
                Try
                    Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager
                    InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                    Dim Ds As DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * From R2PrimarySMSSystem.dbo.TblSMSTypes Where Active=1 And ViewFlag=1 And SMSTypeTitle Like  '%" & YourSearchString & "%' Order By SMSTypeTitle ", 3600, Ds, New Boolean)
                    Dim Lst As New List(Of R2StandardStructure)
                    For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                        Lst.Add(New R2CoreStandardSMSTypeStructure(Ds.Tables(0).Rows(Loopx).Item("SMSTypeId"), Ds.Tables(0).Rows(Loopx).Item("SMSTypeName").trim, Ds.Tables(0).Rows(Loopx).Item("SMSTypeTitle").trim, Color.FromName(Ds.Tables(0).Rows(Loopx).Item("SMSTypeColor").trim), Ds.Tables(0).Rows(Loopx).Item("Price"), Ds.Tables(0).Rows(Loopx).Item("SMSTypeContent").trim, Ds.Tables(0).Rows(Loopx).Item("SMSMinutes"), Ds.Tables(0).Rows(Loopx).Item("Core").trim, Ds.Tables(0).Rows(Loopx).Item("UserId"), Ds.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), Ds.Tables(0).Rows(Loopx).Item("DateShamsi"), Ds.Tables(0).Rows(Loopx).Item("Time"), Ds.Tables(0).Rows(Loopx).Item("ViewFlag"), Ds.Tables(0).Rows(Loopx).Item("Active"), Ds.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return Lst
                Catch ex As SqlInjectionException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        Namespace Exceptions
            Public Class SMSTypeIdNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "شماره شاخص نوع اس ام اس نادرست است"
                    End Get
                End Property
            End Class

        End Namespace

    End Namespace

    Namespace SMSOwners

        Public Class R2CoreStandardSMSOwnerStructure

            Public Sub New()
                MyBase.New()
                _SMSOwnerUserId = Int64.MinValue
                _SMSOTypeId = Int64.MinValue
                _ReminderCharge = Int64.MinValue
                _ReminderHolder = Int64.MinValue
                _IsSendingActive = Boolean.FalseString
                _PleaseCharge = Boolean.FalseString
                _DateTimeMilladi = DateTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _UserId = Int64.MinValue
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSOwnerUserId As Int64, YourSMSOTypeId As Int64, YourReminderCharge As Int64, YourReminderHolder As Int64, YourIsSendingActive As Boolean, YourPleaseCharge As Boolean, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SMSOwnerUserId = YourSMSOwnerUserId
                _SMSOTypeId = YourSMSOTypeId
                _ReminderCharge = YourReminderCharge
                _ReminderHolder = YourReminderHolder
                _IsSendingActive = YourIsSendingActive
                _PleaseCharge = YourPleaseCharge
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _UserId = YourUserId
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSOwnerUserId As Int64
            Public Property SMSOTypeId As Int64
            Public Property ReminderCharge As Int64
            Public Property ReminderHolder As Int64
            Public Property IsSendingActive As Boolean
            Public Property PleaseCharge As Boolean
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property UserId As Int64
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreStandardSMSOwnerTypeStructure

            Public Sub New()
                MyBase.New()
                _SMSOTypeId = Int64.MinValue
                _SMSOTypeName = String.Empty
                _SMSOTypeTitle = String.Empty
                _SMSOTypeColor = Color.Black
                _Price = Int64.MinValue
                _PriceMinusCommission = Int64.MinValue
                _PriceMinusCommissionMinusVAT = Int64.MinValue
                _Core = String.Empty
                _UserId = Int64.MinValue
                _DateTimeMilladi = DateTime.Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _ViewFlag = Boolean.FalseString
                _Active = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourSMSOTypeId As Int64, YourSMSOTypeName As String, YourSMSOTypeTitle As String, YourSMSOTypeColor As Color, YourPrice As Int64, YourPriceMinusCommission As Int64, YourPriceMinusCommissionMinusVAT As Int64, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
                _SMSOTypeId = YourSMSOTypeId
                _SMSOTypeName = YourSMSOTypeName
                _SMSOTypeTitle = YourSMSOTypeTitle
                _SMSOTypeColor = YourSMSOTypeColor
                _Price = YourPrice
                _PriceMinusCommission = YourPriceMinusCommission
                _PriceMinusCommissionMinusVAT = YourPriceMinusCommissionMinusVAT
                _Core = YourCore
                _UserId = YourUserId
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _ViewFlag = YourViewFlag
                _Active = YourActive
                _Deleted = YourDeleted
            End Sub

            Public Property SMSOTypeId As Int64
            Public Property SMSOTypeName As String
            Public Property SMSOTypeTitle As String
            Public Property SMSOTypeColor As Color
            Public Property Price As Int64
            Public Property PriceMinusCommission As Int64
            Public Property PriceMinusCommissionMinusVAT As Int64
            Public Property Core As String
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property ViewFlag As Boolean
            Public Property Active As Boolean
            Public Property Deleted As Boolean

        End Class

        Public Class R2CoreMClassSMSOwnerTypesManager
            Private _DateTime As New R2DateTime

            Public Function GetNSSSMSOwnerType(YourSMSOwnerTypeId As Int64) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * from R2PrimarySMSSystem.dbo.TblSMSOwnerTypes Where SMSOTypeId=" & YourSMSOwnerTypeId & " and Active=1 and Deleted=0", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeNotFoundException
                    End If
                Catch ex As SMSOwnerTypeNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSMSOwnerTypeBySoftwareUser(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As R2CoreStandardSMSOwnerTypeStructure
                Try
                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select * from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                            Inner Join R2Primary.dbo.TblSoftwareUserTypes as SoftwareUserTypes On SoftwareUsers.UserTypeId=SoftwareUserTypes.UTId 
                            Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUserTypes.UTId=EntityRelations.E1 
                            Inner Join R2PrimarySMSSystem.dbo.TblSMSOwnerTypes as SMSOwnerTypes On EntityRelations.E2=SMSOwnerTypes.SMSOTypeId 
                           Where SoftwareUsers.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUserTypes.Active=1 and SoftwareUserTypes.Deleted=0 and EntityRelations.RelationActive=1 and EntityRelations.ERTypeId=10 and SMSOwnerTypes.Active=1", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        Return New R2CoreStandardSMSOwnerTypeStructure(DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("SMSOTypeName").trim, DS.Tables(0).Rows(0).Item("SMSOTypeTitle").trim, Color.FromName(DS.Tables(0).Rows(0).Item("SMSOTypeColor")), DS.Tables(0).Rows(0).Item("Price"), DS.Tables(0).Rows(0).Item("PriceMinusCommission"), DS.Tables(0).Rows(0).Item("PriceMinusCommissionMinusVAT"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                    Else
                        Throw New SMSOwnerTypeBySoftwareUserNotFoundException
                    End If
                Catch ex As SMSOwnerTypeBySoftwareUserNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try


            End Function

        End Class

        Public Class R2CoreMClassSMSOwnersManager
            Private _DateTime As New R2DateTime

            Public Function GetNSSSMSOwner(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure, YourImmediately As Boolean) As R2CoreStandardSMSOwnerStructure
                Try
                    Dim DS As New DataSet
                    Dim Da As New SqlClient.SqlDataAdapter
                    If YourImmediately Then
                        Da.SelectCommand = New SqlClient.SqlCommand("Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourNSSSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc")
                        Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                        If Da.Fill(DS) <> 0 Then Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                    Else
                        Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                        If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2PrimarySMSSystem.dbo.TblSMSOwners Where SMSOwnerUserId=" & YourNSSSoftwareUser.UserId & " and Active=1 and Deleted=0 Order By DateTimeMilladi Desc", 60, DS, New Boolean).GetRecordsCount <> 0 Then
                            Return New R2CoreStandardSMSOwnerStructure(DS.Tables(0).Rows(0).Item("SMSOwnerUserId"), DS.Tables(0).Rows(0).Item("SMSOTypeId"), DS.Tables(0).Rows(0).Item("ReminderCharge"), DS.Tables(0).Rows(0).Item("ReminderHolder"), DS.Tables(0).Rows(0).Item("IsSendingActive"), DS.Tables(0).Rows(0).Item("PleaseCharge"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                        Else
                            Throw New SMSOwnerForSoftwareUserDoNotRegisteredException
                        End If
                    End If
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub RegisteringSMSOwner(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
                Try
                    Dim myCurrentDateTime = _DateTime.GetCurrentDateTime
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set IsSendingActive=0 Where SMSOwnerUserId=" & YourNSSSMSOwner.SMSOwnerUserId & " and IsSendingActive=1"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Insert Into R2PrimarySMSSystem.dbo.TblSMSOwners(SMSOwnerUserId,SMSOTypeId,ReminderCharge,ReminderHolder,IsSendingActive,PleaseCharge,DateTimeMilladi,DateShamsi,Time,UserId,ViewFlag,Active,Deleted)
                                          Values(" & YourNSSSMSOwner.SMSOwnerUserId & "," & YourNSSSMSOwner.SMSOTypeId & "," & YourNSSSMSOwner.ReminderCharge & "," & YourNSSSMSOwner.ReminderHolder & ",1,0,'" & myCurrentDateTime.DateTimeMilladiFormated & "','" & myCurrentDateTime.DateShamsiFull & "','" & myCurrentDateTime.Time & "'," & YourNSSUser.UserId & ",1,1,0)"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Structure SMSOwnerCurrentState
                Public IsSendingActive As Boolean
                Public TextToView As String
                Public TextToViewColor As String
            End Structure

            Public Function GetNSSSMSOwnerCurrentState(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As SMSOwnerCurrentState
                Try
                    Dim NSSSMSOwner As R2CoreStandardSMSOwnerStructure = Nothing
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim TextToView = String.Empty
                    Dim TextToViewColor = String.Empty
                    Try
                        NSSSMSOwner = GetNSSSMSOwner(YourNSSSoftwareUser, True)
                    Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                        TextToView = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14)
                        TextToViewColor = InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12)
                        Return New SMSOwnerCurrentState With {.IsSendingActive = False, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                    Catch ex As Exception
                        Throw ex
                    End Try
                    TextToView = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 15), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 14))
                    TextToViewColor = IIf(NSSSMSOwner.IsSendingActive, InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 13), InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 12))
                    Return New SMSOwnerCurrentState With {.IsSendingActive = NSSSMSOwner.IsSendingActive, .TextToView = TextToView, .TextToViewColor = TextToViewColor}
                Catch ex As SMSOwnerForSoftwareUserDoNotRegisteredException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function IsActiveSystemAccountingforSendSMS() As Boolean
                Try
                    'کنترل کسر هزینه فعال است یا نه
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 10) Then Return False
                    Return True
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNSSSoftwareUsers(YourNSSUserTypeId As Int64) As List(Of R2CoreStandardSoftwareUserStructure)
                Try
                    Dim LstUsers As New List(Of R2CoreStandardSoftwareUserStructure)
                    Dim DS As New DataSet
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select Distinct SoftwareUsers.* from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners 
                     Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId =SoftwareUsers.UserId 
                   Where SoftwareUsers.UserTypeId=" & YourNSSUserTypeId & " and SMSOwners.IsSendingActive=1 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 ", 3600, DS, New Boolean)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode"), DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce"), DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce"), DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha"), DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Return LstUsers
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSMSOwnerReminderCreditPercent(YourNSSSMSOwner As R2CoreStandardSMSOwnerStructure) As Int64
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Return InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) * YourNSSSMSOwner.ReminderCharge / 100
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Shared _SendSMSOwnersPleaseChargeFlag As Boolean = False
            Public Sub SendSMSOwnersPleaseCharge()
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
                Try
                    Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                    Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                    Dim InstancePersianCallendar = New R2CoreInstanceDateAndTimePersianCalendarManager
                    'کنترل زمان اجرای فرآیند بر اساس کانفیگ
                    Dim myCurrentDateTime = _DateTime.GetCurrentDateTime
                    Dim TimeOfDay = _DateTime.GetTickofTime(myCurrentDateTime)
                    Dim StTime = TimeSpan.Parse("00:00:00")
                    Dim EndTime = TimeSpan.Parse("00:05:00")
                    Dim ConfigTime = TimeSpan.Parse(InstanceConfiguration.GetConfigString(R2CoreConfigurations.SmsSystemSetting, 9))
                    If TimeOfDay >= StTime And TimeOfDay <= EndTime Then
                        _SendSMSOwnersPleaseChargeFlag = False
                        Return
                    ElseIf TimeOfDay <= ConfigTime Then
                        Return
                    Else
                    End If
                    'این فرآیند در روز فقط باید یکبار اجرا گردد و نه بیشتر
                    'خط کد زیر یعنی فرآیند امروز قبلا در بازه معین اجرا یکبار اجرا شده است
                    If _SendSMSOwnersPleaseChargeFlag Then Return
                    'لیست مالکان اس ام اس که برایشان قبلا اس ام اس پلیز شارژ نرفته است و باید برود
                    Dim DS As New DataSet
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                           "Select * from R2PrimarySMSSystem.dbo.TblSMSOwners as SMSOwners
                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On SMSOwners.SMSOwnerUserId=SoftwareUsers.UserId 
                            Where SMSOwners.IsSendingActive=1 and SMSOwners.ReminderCharge<>0 and ((SMSOwners.ReminderCharge-SMSOwners.ReminderHolder)<=(SMSOwners.ReminderCharge * " & InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.SmsSystemSetting, 6) & " / 100))
                                  and SMSOwners.PleaseCharge=0 and SMSOwners.Active=1 and SMSOwners.Deleted=0 and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        _SendSMSOwnersPleaseChargeFlag = True
                        Return
                    End If

                    'تغییر فیلد پلیز شارژ به ارسال شده
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        Dim UserId As Int64 = DS.Tables(0).Rows(Loopx).Item("SMSOwnerUserId")
                        CmdSql.CommandText = "Update R2PrimarySMSSystem.dbo.TblSMSOwners Set PleaseCharge=1 Where SMSOwnerUserId=" & UserId & " and IsSendingActive=1 and PleaseCharge=0"
                        CmdSql.ExecuteNonQuery()
                    Next
                    CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                    _SendSMSOwnersPleaseChargeFlag = True

                    'ارسال پیام به مالکان اس ام اس
                    Dim LstUsers = New List(Of R2CoreStandardSoftwareUserStructure)
                    For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                        LstUsers.Add(New R2CoreStandardSoftwareUserStructure(DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("ApiKey").trim, DS.Tables(0).Rows(Loopx).Item("APIKeyExpiration"), DS.Tables(0).Rows(Loopx).Item("UserName").trim, DS.Tables(0).Rows(Loopx).Item("UserShenaseh").trim, DS.Tables(0).Rows(Loopx).Item("UserPassword").trim, DS.Tables(0).Rows(Loopx).Item("UserPasswordExpiration"), DS.Tables(0).Rows(Loopx).Item("UserPinCode").trim, DS.Tables(0).Rows(Loopx).Item("UserCanCharge"), DS.Tables(0).Rows(Loopx).Item("UserActive"), DS.Tables(0).Rows(Loopx).Item("UserTypeId"), DS.Tables(0).Rows(Loopx).Item("MobileNumber").trim, DS.Tables(0).Rows(Loopx).Item("UserStatus").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCode").trim, DS.Tables(0).Rows(Loopx).Item("VerificationCodeTimeStamp"), DS.Tables(0).Rows(Loopx).Item("VerificationCodeCount"), DS.Tables(0).Rows(Loopx).Item("Nonce").trim, DS.Tables(0).Rows(Loopx).Item("NonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("NonceCount"), DS.Tables(0).Rows(Loopx).Item("PersonalNonce").trim, DS.Tables(0).Rows(Loopx).Item("PersonalNonceTimeStamp"), DS.Tables(0).Rows(Loopx).Item("Captcha").trim, DS.Tables(0).Rows(Loopx).Item("CaptchaValid"), DS.Tables(0).Rows(Loopx).Item("UserCreatorId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                    Next
                    Dim InstanceSMSHandling = New R2CoreSMSHandlingManager
                    Dim SMSResult = InstanceSMSHandling.SendSMS(LstUsers, R2CoreSMSTypes.PleaseCharge, InstanceSMSHandling.RepeatSMSCreationData(New SMSCreationData With {.Data1 = String.Empty}, LstUsers.Count), True)
                    Dim SMSResultAnalyze = InstanceSMSHandling.GetSMSResultAnalyze(SMSResult)
                    If Not SMSResultAnalyze = String.Empty Then Throw New SMSOwnerSendingPleaseChargeMessageFailedException(SMSResultAnalyze)
                Catch ex As SMSOwnerSendingPleaseChargeMessageFailedException
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw ex
                Catch ex As Exception
                    If CmdSql.Connection.State <> ConnectionState.Closed Then
                        CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Sub

        End Class

        Namespace Exceptions

            Public Class SMSOwnerChargeMessageException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "سرویس اس ام اس شارژ ندارد" + vbCrLf + "لطفا سرویس اس ام اس را شارژ نمایید"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerForSoftwareUserDoNotRegisteredException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "فعال سازی اس ام اس برای کاربر مورد انجام نشده است"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerHasCreditYetException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "سرویس اس ام اس کاربر قبلا فعال شده است و هنوز معتبر است"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerTypeNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "نوع مالک اس ام اس با شاخص مورد نظر یافت نشد"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerTypeBySoftwareUserNotFoundException
                Inherits ApplicationException
                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "نوع مالک اس ام اس مبنی بر نوع کاربرارسالی با شاخص مورد نظر یافت نشد"
                    End Get
                End Property
            End Class

            Public Class SMSOwnerIsSendActiveIsFalseException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "وضعیت ارسال اس ام اس برای مالک آن " + _Message.Trim + "  غیر فعال است"
                    End Get
                End Property

            End Class

            Public Class SMSOwnerSendingPleaseChargeMessageFailedException
                Inherits ApplicationException

                Private _Message As String = String.Empty
                Public Sub New(YourMessage As String)
                    _Message = YourMessage
                End Sub

                Public Overrides ReadOnly Property Message As String
                    Get
                        Return "ارسال اس ام اس لطفا شارژ کنید برای مالک آن با مشکل مواجه شد " + vbCrLf + _Message.Trim
                    End Get
                End Property

            End Class

        End Namespace

    End Namespace

    Namespace SMSResultLogging

        Public Class R2CoreLoggingSMSResultManager
            Public Sub Logging(YourSMSResult As List(Of KeyValuePair(Of Int64, String)), YourNSSUser As R2CoreStandardSoftwareUserStructure)
                Try
                    Dim InstanceLogging = New R2CoreInstanceLoggingManager
                    Dim InstanceSendSMSCode = New R2CoreSendSMSCodeManager
                    For Loopx As Int64 = 0 To YourSMSResult.Count - 1
                        InstanceLogging.LogRegister(New R2CoreStandardLoggingStructure(0, R2CoreLogType.SendSMSResult, InstanceLogging.GetNSSLogType(R2CoreLogType.SendSMSResult).LogTitle, InstanceSendSMSCode.GetNSSSendSMSCode(YourSMSResult(Loopx).Key).SendSMSCodeTitle, YourSMSResult(Loopx).Value, String.Empty, String.Empty, String.Empty, YourNSSUser.UserId, Nothing, Nothing))
                    Next
                Catch ex As Exception

                End Try
            End Sub
        End Class
    End Namespace

    Namespace Exceptions

        Public Class SmsSystemIsDisabledException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سیستم اس ام اس از طریق جدول پیکربندی غیر فعال است"
                End Get
            End Property
        End Class



    End Namespace

End Namespace

Namespace Email

    Public Class R2CoreEmailManager

        Public Sub SendEmailWithTXTTypeAttachment(YourReciever As String, YourAttachment As StringBuilder, YourSubject As String, YourBody As String, YourAttachmentFileName As String)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                If Not InstanceConfiguration.GetConfigBoolean(R2CoreConfigurations.EmailSystem, 0) Then Throw New R2CoreEmailSystemIsNotActiveException

                Dim myByteArray = System.Text.Encoding.UTF8.GetBytes(YourAttachment.ToString)
                Dim ms = New MemoryStream(myByteArray)
                Dim Credentials = New System.Net.NetworkCredential(InstanceConfiguration.GetConfigString(R2CoreConfigurations.EmailSystem, 2), InstanceConfiguration.GetConfigString(R2CoreConfigurations.EmailSystem, 3))
                Dim SmtpClient = New SmtpClient()
                SmtpClient.Host = InstanceConfiguration.GetConfigString(R2CoreConfigurations.EmailSystem, 1)
                SmtpClient.Port = 587
                SmtpClient.EnableSsl = True
                SmtpClient.Credentials = Credentials
                Dim mail = New MailMessage()
                mail.From = New MailAddress(InstanceConfiguration.GetConfigString(R2CoreConfigurations.EmailSystem, 2))
                mail.To.Add(YourReciever)
                mail.Attachments.Add(New Attachment(ms, YourAttachmentFileName))
                mail.Subject = YourSubject
                mail.Body = YourBody
                SmtpClient.Send(mail)
            Catch ex As R2CoreEmailSystemIsNotActiveException
                Throw ex
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class R2CoreEmailSystemIsNotActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سرویس ایمیل غیر فعال است"
                End Get
            End Property
        End Class

    End Namespace


End Namespace

Namespace SiteIsBusy

    Public Class R2CoreSiteIsBusyManager
        Public Sub ActivateSiteIsBusy()
            Try
                R2CoreMClassConfigurationManagement.SetConfiguration(R2CoreConfigurations.SiteIsBusy, 0, "1")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DeActivateSiteIsBusy()
            Try
                R2CoreMClassConfigurationManagement.SetConfiguration(R2CoreConfigurations.SiteIsBusy, 0, "0")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetSiteIsBusy() As Boolean
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim SiteIsBusy As Boolean = InstanceConfiguration.GetConfig(R2CoreConfigurations.SiteIsBusy, 0, 60)
                If SiteIsBusy Then Return True Else Return False
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SiteIsBusy()
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim mySiteIsBusy As Boolean = InstanceConfiguration.GetConfig(R2CoreConfigurations.SiteIsBusy, 0, 60)
                If mySiteIsBusy Then Throw New R2CoreSiteIsBusyException
            Catch ex As R2CoreSiteIsBusyException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

    End Class

    Namespace Exceptions
        Public Class R2CoreSiteIsBusyException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "لحظاتی منتظر بمانید"
                End Get
            End Property
        End Class

    End Namespace

End Namespace

Namespace Caching

    Public MustInherit Class R2CoreCacheKeys
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Session As Int64 = 1
    End Class

    Public Class R2CoreStandardCacheKeyStructure

        Public Sub New()
            MyBase.New()
            _KeyId = Int64.MinValue
            _KeyName = String.Empty
            _CacheTime = String.Empty
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _UserId = Int64.MinValue
            _Core = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourKeyId As Int64, YourKeyName As String, YourCacheTime As String, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourUserId As Int64, YourCore As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _KeyId = YourKeyId
            _KeyName = YourKeyName
            _CacheTime = YourCacheTime
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _UserId = YourUserId
            _Core = YourCore
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property KeyId As Int64
        Public Property KeyName As String
        Public Property CacheTime As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property UserId As Int64
        Public Property Core As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public NotInheritable Class RedisConnectorHelper
        Private Shared ReadOnly lazyConnection As New Lazy(Of ConnectionMultiplexer)(
        Function()
            Return ConnectionMultiplexer.Connect("192.168.1.4:6379")
        End Function
    )

        Public Shared ReadOnly Property Connection As ConnectionMultiplexer
            Get
                Return lazyConnection.Value
            End Get
        End Property
    End Class

    Public Class R2CoreCacheManager

        Public Function GetNSSCacheKey(YourKeyId As Int64) As R2CoreStandardCacheKeyStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2Primary.dbo.TblCacheKeys Where KeyId=" & YourKeyId& & "", 10000, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New CacheKeyNotFoundException

                Else
                    Return New R2CoreStandardCacheKeyStructure(DS.Tables(0).Rows(0).Item("KeyId"), DS.Tables(0).Rows(0).Item("KeyName").trim, DS.Tables(0).Rows(0).Item("CacheTime"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("Core").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
                End If
            Catch ex As CacheKeyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub SetCache(YourKeyId As String, YourCacheValue As Object)
            Try
                Dim Cache = RedisConnectorHelper.Connection.GetDatabase
                Cache.StringSet(YourKeyId, JsonConvert.SerializeObject(YourCacheValue), TimeSpan.FromSeconds(GetNSSCacheKey(R2CoreCacheKeys.Session).CacheTime))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetCache(YourKeyId As String) As Object
            Try
                Dim Cache = RedisConnectorHelper.Connection.GetDatabase
                Return Cache.StringGet(YourKeyId)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Sub RemoveCache(YourCacheKey As String)
            Try
                Dim Cache = RedisConnectorHelper.Connection.GetDatabase
                Cache.KeyDelete(YourCacheKey)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class CacheKeyNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "کلید شاخص بافرینگ یا کش در سیستم وجود ندارد"
            End Get
        End Property
    End Class

End Namespace


