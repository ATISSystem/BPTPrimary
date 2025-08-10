


Imports R2Core.BaseStandardClass
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.PredefinedMessagesManagement
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.TrucksNativeness.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Reflection

Namespace TrucksNativeness

    Public Class TruckNativenessTypes
        Public Shared ReadOnly Property None As Int64 = 0
        Public Shared ReadOnly Property Native As Int64 = 1
        Public Shared ReadOnly Property UnNative As Int64 = 2
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure
        Inherits R2StandardStructure
        Public Sub New()
            MyBase.New()
            _NId = Int64.MinValue
            _NName = String.Empty
            _NTitle = String.Empty
            _NColor = Color.Red
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _Active = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.TrueString
        End Sub

        Public Sub New(YourNId As Int64, YourNName As String, YourNTitle As String, YourNColor As Color, YourDateTimeMilladi As DateTime, YOurDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            MyBase.New(YourNId, YourNName)
            _NId = YourNId
            _NName = YourNName
            _NTitle = YourNTitle
            _NColor = YourNColor
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YOurDateShamsi
            _Time = YourTime
            _Active = YourActive
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property NId As Int64
        Public Property NName As String
        Public Property NTitle As String
        Public Property NColor As Drawing.Color
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property Active As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean



    End Class

    Public Structure R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
        Dim TruckNativenessTypeId As Int64
        Dim TruckNativenessExpireDate As R2StandardDateAndTimeStructure
    End Structure

    'BPTChanged
    Public Structure R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
        Dim TruckNativenessTypeId As Int64
        Dim TruckNativenessTypeTitle As String
        Dim TruckNativenessExpireDate As String
    End Structure

    Public Class R2CoreTransportationAndLoadNotificationsTruckNativenessManager
        Private _DateTime As New R2DateTime

        Public Function GetNSSTruckNativeness(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
            Try
                If YourNSSTruck Is Nothing Then Throw New TruckNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessStructure
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                    Da.SelectCommand = New SqlCommand("Select CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar Where nIDCar=" & YourNSSTruck.NSSCar.nIdCar & "")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TruckNotFoundException
                    NSS.TruckNativenessTypeId = Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CarNativenessTypeId"))
                    NSS.TruckNativenessExpireDate = New R2StandardDateAndTimeStructure(Nothing, Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, Nothing)
                    Return NSS
                Else
                    'If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar Where nIDCar=" & YourNSSTruck.NSSCar.nIdCar & "", 3600, Ds).GetRecordsCount() = 0 Then Throw New TruckNotFoundException
                    Dim DSCarsNativeness As New DataSet
                    R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Case nIDCar,CarNativenessTypeId,CarNativenessExpireDate From  dbtransport.dbo.TbCar", 3600, DSCarsNativeness, New Boolean)
                    Dim DR() = DSCarsNativeness.Tables(0).Select("nIDCar=" + YourNSSTruck.NSSCar.nIdCar)
                    NSS.TruckNativenessTypeId = Convert.ToInt64(DR(0).Item("CarNativenessTypeId"))
                    NSS.TruckNativenessExpireDate = New R2StandardDateAndTimeStructure(Nothing, DR(0).Item("CarNativenessExpireDate").trim, Nothing)
                    Return NSS
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTruckNativenessType(YourTruckNativenessTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes Where NId=" & YourTruckNativenessTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TruckNativenessTypeNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTruckNativenessTypeStructure(Ds.Tables(0).Rows(0).Item("NId"), Ds.Tables(0).Rows(0).Item("NName").TRIM, Ds.Tables(0).Rows(0).Item("NTitle").TRIM, Color.FromName(Ds.Tables(0).Rows(0).Item("NColor").TRIM), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"), Ds.Tables(0).Rows(0).Item("Deleted"))
                Return NSS
            Catch ex As TruckNativenessTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetTruckNativenessType(YourTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Int64
            Try
                If IsTruckIndigenous(YourTruck) Then
                    Return TruckNativenessTypes.Native
                Else
                    Return TruckNativenessTypes.UnNative
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Function IsTruckIndigenous(YourNSSTruck As R2CoreTransportationAndLoadNotificationStandardTruckStructure) As Boolean
            Try
                If YourNSSTruck Is Nothing Then Throw New TruckNotFoundException
                Dim InstanceSqlDataBox As New R2CoreInstanseSqlDataBOXManager
                Dim Ds As DataSet = Nothing
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select CarNativenessTypeId,CarNativenessExpireDate from DBtransport.dbo.TbCar
                   Where StrCarNo='" & YourNSSTruck.NSSCar.StrCarNo & "' and StrCarSerialNo='" & YourNSSTruck.NSSCar.StrCarSerialNo & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TruckNotFoundException
                End If
                If Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CarNativenessTypeId")) = TruckNativenessTypes.Native Then
                    If Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").ToString.Trim = String.Empty Then
                        Return True
                    ElseIf Ds.Tables(0).Rows(0).Item("CarNativenessExpireDate").ToString.Trim > _DateTime.GetCurrentDateShamsiFull() Then
                        Return True
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Function IsTruckIndigenous(YourTurn As R2CoreTransportationAndLoadNotificationStandardTurnExtendedStructure) As Boolean
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Return IsTruckIndigenous(InstanceTrucks.GetNSSTruck(YourTurn, False))
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function GetTruckNativeness(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
            Try
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                Dim NSS As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                             "Select Cars.CarNativenessTypeId,Cars.CarNativenessExpireDate ,TruckNativenessTypes.NTItle  from DBTransport.dbo.TbCar as Cars
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
                              Where Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and TruckNativenessTypes.Deleted=0")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New TruckNotFoundException
                    NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure With {.TruckNativenessTypeId = DS.Tables(0).Rows(0).Item("CarNativenessTypeId"), .TruckNativenessExpireDate = DS.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, .TruckNativenessTypeTitle = DS.Tables(0).Rows(0).Item("NTItle").trim}
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                             "Select Cars.CarNativenessTypeId,Cars.CarNativenessExpireDate ,TruckNativenessTypes.NTItle  from DBTransport.dbo.TbCar as Cars
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTruckNativenessTypes as TruckNativenessTypes On Cars.CarNativenessTypeId=TruckNativenessTypes.NId 
                              Where Cars.nIDCar=" & YourTruckId & " and Cars.ViewFlag=1 and TruckNativenessTypes.Deleted=0", 3600, DS, New Boolean).GetRecordsCount <> 0 Then
                        NSS = New R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure With {.TruckNativenessTypeId = DS.Tables(0).Rows(0).Item("CarNativenessTypeId"), .TruckNativenessExpireDate = DS.Tables(0).Rows(0).Item("CarNativenessExpireDate").trim, .TruckNativenessTypeTitle = DS.Tables(0).Rows(0).Item("NTItle").trim}
                    Else
                        Throw New TruckNotFoundException
                    End If
                End If
                Return NSS
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ChangeTruckNativeness(YourTruckId As Int64, YourTruckNativenessExpireDate As String) As R2CoreTransportationAndLoadNotificationsTruckNativenessExtendedStructure
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                Dim InstanceTruckNativeness = New R2CoreTransportationAndLoadNotificationsTruckNativenessManager
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationInstanceTrucksManager
                Dim NSSTruck = InstanceTrucks.GetNSSTruck(YourTruckId)
                'کنترل تغییر وضعیت بومی گری ناوگان بومی با پلاک بومی - که البته امکان پذیر نیست
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim IndigenousTrucks() = InstanceConfiguration.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.IndigenousTrucks, 1).Split("-")
                If IndigenousTrucks.Contains(NSSTruck.NSSCar.StrCarSerialNo) Then Throw New IndigenousTruckChangeNativnessFailedException
                'تغییر وضعیت بومی گری
                Dim newTruckNativenessTypeId = TrucksNativeness.TruckNativenessTypes.None
                Dim TruckNativeness = GetTruckNativeness(YourTruckId, True)
                If TruckNativeness.TruckNativenessTypeId = TruckNativenessTypes.Native Then
                    newTruckNativenessTypeId = TruckNativenessTypes.UnNative
                ElseIf TruckNativeness.TruckNativenessTypeId = TruckNativenessTypes.UnNative Then
                    newTruckNativenessTypeId = TruckNativenessTypes.Native
                Else
                    Throw New TruckNativenessTypeNotValidException
                End If
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update dbtransport.dbo.TbCar Set CarNativenessTypeId=" & newTruckNativenessTypeId & ",CarNativenessExpireDate='" & YourTruckNativenessExpireDate & "' Where nIdCar=" & NSSTruck.NSSCar.nIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                TruckNativeness.TruckNativenessExpireDate = YourTruckNativenessExpireDate
                TruckNativeness.TruckNativenessTypeId = newTruckNativenessTypeId
                TruckNativeness.TruckNativenessTypeTitle = InstanceTruckNativeness.GetNSSTruckNativenessType(newTruckNativenessTypeId).NTitle
                Return TruckNativeness
            Catch ex As IndigenousTruckChangeNativnessFailedException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As TruckNativenessTypeNotValidException
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
        End Function

    End Class

    Namespace Exceptions

        Public Class IndigenousTruckChangeNativnessFailedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تغییر وضعیت ناوگان امکان پذیر نیست"
                End Get
            End Property
        End Class

        Public Class NonIndigenousTrucksException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Dim InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
                    Return InstancePredefinedMessages.GetNSS(R2CoreTransportationAndLoadNotificationPredefinedMessages.UnIndigenousTrucks).MsgContent
                End Get
            End Property
        End Class

        Public Class TruckNativenessTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات بومی گری با کد شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class TruckNativenessTypeNotValidException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "خدرو دارای اطلاعات بومی گری غیر مجاز و تعریف نشده است"
                End Get
            End Property
        End Class

    End Namespace


End Namespace
