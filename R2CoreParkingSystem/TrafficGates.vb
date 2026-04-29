

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProcedures
Imports R2CoreParkingSystem.TrafficCosts
Imports System.Data.SqlClient
Imports System.Reflection

Namespace TrafficGates

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficGate
        Public GateId As Int16
        Public GateTitle As String
        Public GateLocation As String
        Public Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficGatesManager
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetTrafficGatesJSON() As String
            Try
                Dim InstancePublicProcedures = New R2CorePublicProceduresManager()

                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select GateId,GateTitle,GateLocation,Active
                    from R2PrimaryParkingSystem.dbo.TblTrafficGates
                    Order By GateId for JSON AUTO", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub RegisteringTrafficGate(YourTrafficGate As R2CoreParkingSystemTrafficGate)
            Try
                'با توجه به این که نام گیت یونیک است در اینجا باید یک رکرود جدید ثبت شود

            Catch ex As Exception

            End Try
        End Sub

        Public Sub ActivateTrafficGate(YourTrafficGateId As Int64)
            Try
            Catch ex As Exception

            End Try
        End Sub

        Public Sub UnActivateTrafficGate(YourTrafficGateId As Int64)
            Try
            Catch ex As Exception

            End Try
        End Sub

        Public Function GetTrafficGate(YourTrafficGateId As Int64, YourImmediately As Boolean) As R2CoreParkingSystemTrafficGate
            Try
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select GateId,GateTitle,GateLocation,Active 
                                                       from R2PrimaryParkingSystem.dbo.TblTrafficGates Where GateId=" & YourTrafficGateId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TrafficGateNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                                                      "Select GateId,GateTitle,GateLocation,Active 
                                                       from R2PrimaryParkingSystem.dbo.TblTrafficGates Where GateId=" & YourTrafficGateId & "", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New TrafficGateNotFoundException
                End If
                Return New R2CoreParkingSystemTrafficGate With {.GateId = Ds.Tables(0).Rows(0).Item("GateId"), .GateTitle = Ds.Tables(0).Rows(0).Item("GateTitle").trim, .GateLocation = Ds.Tables(0).Rows(0).Item("GateLocation").trim, .Active = Ds.Tables(0).Rows(0).Item("Active")}
            Catch ex As TrafficGateNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class TrafficGateNotFoundException
        Inherits BPTException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعاتی درخصوص گیت تردد یافت نشد"
            End Get
        End Property
    End Class

End Namespace