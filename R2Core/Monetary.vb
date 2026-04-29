
Imports RestSharp
Imports System.ComponentModel
Imports System.Drawing
Imports System.Reflection
Imports System.Timers
Imports System.Windows.Forms
Imports PcPosDll

Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationOfDevices
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.MonetaryCreditSupplySources
Imports R2Core.MonetarySupply

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
            MyBase.New()
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


        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetNSSMonetaryCreditSupplySource(YourMCSSId As String) As R2CoreStandardMonetaryCreditSupplySourceStructure
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where MCSSId=" & YourMCSSId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
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

        'Public Function GetMonetaryCreditSupplySources() As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '    Try
        '        Dim DS As DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where ViewFlag=1 and Active=1 and Deleted=0 ", 3600, DS, New Boolean)
        '        Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '        For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
        '            Lst.Add(New R2CoreStandardMonetaryCreditSupplySourceStructure(DS.Tables(0).Rows(Loopx).Item("MCSSId"), DS.Tables(0).Rows(Loopx).Item("MCSSName").trim, DS.Tables(0).Rows(Loopx).Item("MCSSTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MCSSColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetaryCreditSupplySourceStructure
        '    Try
        '        Dim InstanceConfigurationOfDevices = New R2CoreConfigurationOfDevicesManager(_DateTimeService)
        '        Dim InstanceComputers As New R2CoreMClassComputersManager(_DateTimeService)
        '        Dim ConfigValue As String = InstanceConfigurationOfDevices.GetStringConfiguration(R2CoreConfigurations.MonetaryCreditSupplySources, InstanceComputers.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Return GetNSSMonetaryCreditSupplySource(ConfigValue.Split("@")(0))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '    Try
        '        Dim InstanceConfigurationOfComputers As New R2CoreMClassConfigurationOfComputersManager(_DateTimeService)
        '        Dim InstanceComputers As New R2CoreMClassComputersManager(_DateTimeService)
        '        Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '        Dim ConfigValue As String = InstanceConfigurationOfComputers.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, InstanceComputers.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
        '        For Loopx As Int16 = 0 To Bitmap.Count - 1
        '            Dim NSS As R2CoreStandardMonetaryCreditSupplySourceStructure = GetNSSMonetaryCreditSupplySource(Bitmap(Loopx).Split(":")(0))
        '            NSS.Active = Bitmap(Loopx).Split(":")(1)
        '            Lst.Add(NSS)
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetMonetaryCreditSupplySourceInstance(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64) As R2CoreMonetaryCreditSupplySource
        '    Try
        '        Dim AssemblyClassName As String = YourNSS.AssemblyPath
        '        Dim Instance As Object = Activator.CreateInstance(Type.GetType(AssemblyClassName), New Object() {YourAmount})
        '        'Dim Instance As Object = Activator.CreateInstance(Type.GetType("R2Core.MonetaryCreditSupplySources.ZarrinPalPaymentGate.R2CoreZarrinPalPaymentGate,R2Core"), New Object() {YourAmount})
        '        Return Instance
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySourcesManagement
        Private Shared _DateTimeService = New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)


        'Public Shared Function GetNSSMonetaryCreditSupplySource(YourMCSSId As String) As R2CoreStandardMonetaryCreditSupplySourceStructure
        '    Try
        '        Dim Ds As New DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where MCSSId=" & YourMCSSId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
        '            Throw New GetNSSException
        '        Else
        '            Return New R2CoreStandardMonetaryCreditSupplySourceStructure(Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("MCSSName").trim, Ds.Tables(0).Rows(0).Item("MCSSTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MCSSColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
        '        End If
        '    Catch exx As GetNSSException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetMonetaryCreditSupplySources() As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '    Try
        '        Dim DS As DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetaryCreditSupplySources Where ViewFlag=1 and Active=1 and Deleted=0 ", 3600, DS, New Boolean)
        '        Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '        For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
        '            Lst.Add(New R2CoreStandardMonetaryCreditSupplySourceStructure(DS.Tables(0).Rows(Loopx).Item("MCSSId"), DS.Tables(0).Rows(Loopx).Item("MCSSName").trim, DS.Tables(0).Rows(Loopx).Item("MCSSTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MCSSColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetaryCreditSupplySourceStructure
        '    Try
        '        Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Return GetNSSMonetaryCreditSupplySource(ConfigValue.Split("@")(0))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '    Try
        '        Dim Lst As New List(Of R2CoreStandardMonetaryCreditSupplySourceStructure)
        '        Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetaryCreditSupplySources, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
        '        For Loopx As Int16 = 0 To Bitmap.Count - 1
        '            Dim NSS As R2CoreStandardMonetaryCreditSupplySourceStructure = GetNSSMonetaryCreditSupplySource(Bitmap(Loopx).Split(":")(0))
        '            NSS.Active = Bitmap(Loopx).Split(":")(1)
        '            Lst.Add(NSS)
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        Public Shared Function GetMonetaryCreditSupplySourceInstance(YourNSS As R2CoreStandardMonetaryCreditSupplySourceStructure, YourAmount As Int64) As R2CoreMonetaryCreditSupplySource
            Try
                Dim AssemblyClassName As String = YourNSS.AssemblyPath
                Dim Instance As Object = Activator.CreateInstance(Type.GetType(AssemblyClassName), New Object() {YourAmount, _DateTimeService})
                'Dim Instance As Object = Activator.CreateInstance(Type.GetType("R2Core.MonetaryCreditSupplySources.ShepaPaymentGate.R2CoreShepaPaymentGate,R2Core"), New Object() {YourAmount})
                Return Instance
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public MustInherit Class R2CoreMonetaryCreditSupplySource

        Protected _DateTimeService As IDateTimeService
        Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService)
            Try
                _Amount = YourAmount
                _DateTimeService = YourDateTimeService
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

            Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService)
                MyBase.New(YourAmount, YourDateTimeService)
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                _SupplyReport = "عملیات موفق"
                _TransactionId = Convert.ToUInt64(_DateTimeService.GetCurrentShamsiDate.Replace("/", "") + _DateTimeService.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
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

            Public Class PCPosData
                Public Data1 As String
                Public Data2 As String
                Public Data3 As String
                Public Data4 As String
                Public Data5 As String
            End Class

            Public Class R2CorePCPos
                Inherits R2CoreMonetaryCreditSupplySource

                Private TargetedPosDevice As PosDevice = New PosDevice
                Public WithEvents _PCPOS As PcPosBusiness = New PcPosBusiness
                Public WithEvents _SearchPos As PcPosDiscovery = New PcPosDiscovery
                Public Event SearchAsyncCompleted(ResultCount As Int16, Description As String)
                Public _DeviceId As Int64

                Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService, YourDeviceId As Int64)
                    MyBase.New(YourAmount, YourDateTimeService)
                    _DeviceId = YourDeviceId
                End Sub

                Public ReadOnly Property GetTargetPosIPAddress(YourDeviceId As Int64) As String
                    Get
                        Dim InstanceConfigurationOfDevices = New R2CoreConfigurationOfDevicesManager(_DateTimeService)
                        Return InstanceConfigurationOfDevices.GetStringConfiguration(R2CoreConfigurationsOfDevices.AttachedPoses, YourDeviceId, 0)
                    End Get
                End Property

                Private Function GetPosResultComposit(YourPosResult As PosResult) As PCPosData
                    Try
                        Dim TransactionId As Long = Convert.ToUInt64(_DateTimeService.GetCurrentShamsiDate.Replace("/", "") + _DateTimeService.GetCurrentTime.Replace(":", "") + Int((1000 - 100 + 1) * Rnd() + 100).ToString)
                        Dim DataStruct = New PCPosData
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
                        _SearchPos.SearchPcPosByIp(100, 2000, GetTargetPosIPAddress(_DeviceId))
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

                Private Sub _PCPOS_OnSaleResult(sender As Object, e As PosResult) Handles _PCPOS.OnSaleResult
                    Try
                        'LoggingPosResult(e)
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

                Public Overrides Sub Initialize()
                    Try
                        TargetedPosDevice.IpAddress = GetTargetPosIPAddress(_DeviceId)
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

            Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService)
                MyBase.New(YourAmount, YourDateTimeService)
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                    Dim requesturl As String
                    If (_Amount = 200000) Then
                        requesturl = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 1) + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() +
                        "&callback_url=" + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 3) +
                        "&description=" + "پرداخت خودگردان" +
                        "&metadata[0]=" + String.Empty + "& metadata[1]=" + String.Empty
                    Else
                        requesturl = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 1) + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() +
                        "&callback_url=" + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 3) +
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
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                    Dim url As String = String.Empty
                    If (_Amount = 200000) Then
                        url = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 4) + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() + "&authority=" + YourAuthority
                    Else
                        url = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 4) + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ZarrinPalPaymentGate, 0) + "&amount=" + _Amount.ToString() + "&authority=" + YourAuthority
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

            Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService)
                MyBase.New(YourAmount, YourDateTimeService)
            End Sub

            Public Overrides Sub Initialize()
            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                    Dim Merchant As ShepaMerchant.Merchant = New ShepaMerchant.Merchant
                    Dim Data = Merchant.requestToken(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ShepaPaymentGate, 0), _Amount.ToString(), InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ShepaPaymentGate, 3), "", "", "", "0", "", "")
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
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                    Dim Merchant As ShepaMerchant.Merchant = New ShepaMerchant.Merchant
                    Dim responce = Merchant.verifyPayment(InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.ShepaPaymentGate, 0), YourAuthority, _Amount.ToString())
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

            Public Sub New(YourAmount As Int64, YourDateTimeService As IDateTimeService)
                MyBase.New(YourAmount, YourDateTimeService)
            End Sub

            Public Overrides Sub Initialize()

            End Sub

            Public Overrides Sub DoCreditSupply()
                Try
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                    Dim requesturl As String
                    requesturl = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.AqayepardakhtPaymentGate, 1) + "&pin=" + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.AqayepardakhtPaymentGate, 0) + "&amount=" + (_Amount / 10).ToString() +
                    "&callback=" + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.AqayepardakhtPaymentGate, 3) +
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
                    Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)

                    Dim url As String = String.Empty
                    url = InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.AqayepardakhtPaymentGate, 4) + "&pin=" + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreGeneralConfigurations.AqayepardakhtPaymentGate, 0) + "&amount=" + (_Amount / 10).ToString() + "&transid=" + YourAuthority
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
            MyBase.New()
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

        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        'Public Shared Function GetNSSMonetarySettingTool(YourMSTId As String) As R2CoreStandardMonetarySettingToolStructure
        '    Try
        '        Dim Ds As DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools  Where MSTId=" & YourMSTId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
        '            Throw New GetNSSException
        '        Else
        '            Return New R2CoreStandardMonetarySettingToolStructure(Ds.Tables(0).Rows(0).Item("MSTId"), Ds.Tables(0).Rows(0).Item("MSTName").trim, Ds.Tables(0).Rows(0).Item("MSTTitle").trim, Color.FromName(Ds.Tables(0).Rows(0).Item("MSTColor").trim), Ds.Tables(0).Rows(0).Item("AssemblyPath").trim, Ds.Tables(0).Rows(0).Item("AssemblyDll").trim, Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("Deleted"))
        '        End If
        '    Catch exx As GetNSSException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetMonetarySettingTools() As List(Of R2CoreStandardMonetarySettingToolStructure)
        '    Try
        '        Dim DS As DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblMonetarySettingTools Where ViewFlag=1 and Active=1 and Deleted=0", 3600, DS, New Boolean)
        '        Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
        '        For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
        '            Lst.Add(New R2CoreStandardMonetarySettingToolStructure(DS.Tables(0).Rows(Loopx).Item("MSTId"), DS.Tables(0).Rows(Loopx).Item("MSTName").trim, DS.Tables(0).Rows(Loopx).Item("MSTTitle").trim, Color.FromName(DS.Tables(0).Rows(Loopx).Item("MSTColor").trim), DS.Tables(0).Rows(Loopx).Item("AssemblyPath").trim, DS.Tables(0).Rows(Loopx).Item("AssemblyDll").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetThisComputerDefaultNSS(YourConfigurationIndex As Int16) As R2CoreStandardMonetarySettingToolStructure
        '    Try
        '        Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Return GetNSSMonetarySettingTool(ConfigValue.Split("@")(0))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetThisComputerCollectionBitMap(YourConfigurationIndex As Int16) As List(Of R2CoreStandardMonetarySettingToolStructure)
        '    Try
        '        Dim Lst As New List(Of R2CoreStandardMonetarySettingToolStructure)
        '        Dim ConfigValue As String = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.MonetarySettingTools, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, YourConfigurationIndex)
        '        Dim Bitmap() As String = ConfigValue.Split("@")(1).Split("-")
        '        For Loopx As Int16 = 0 To Bitmap.Count - 1
        '            Dim NSS As R2CoreStandardMonetarySettingToolStructure = GetNSSMonetarySettingTool(Bitmap(Loopx).Split(":")(0))
        '            NSS.Active = Bitmap(Loopx).Split(":")(1)
        '            Lst.Add(NSS)
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetMonetarySettingToolInstrumentInstance(YourNSS As R2CoreStandardMonetarySettingToolStructure) As Object
        '    Try
        '        Dim PathDll As String = Application.StartupPath + "\" + YourNSS.AssemblyDll.Trim
        '        Dim Assembly_ As Assembly = Assembly.LoadFrom(PathDll)
        '        Dim ProjAndForm As String = YourNSS.AssemblyPath.Trim()
        '        Dim FormInstanceType = Assembly_.GetType(ProjAndForm)
        '        Dim objForm = CType(Activator.CreateInstance(FormInstanceType), Object)
        '        Return objForm
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

End Namespace
