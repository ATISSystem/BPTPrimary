

Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProcedures
Imports R2Core.RFIDCards
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports System.Data.SqlClient
Imports System.Reflection

Namespace TrafficCosts

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficCost
        Public TrafficCardTypeId As Int16
        Public EntryBaseCost As Int64
        Public NoCostStoppageDuration As Int16
        Public ExcessStoppageDuration As Int16
        Public ExcessStoppageCost As Int64
        Public Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficCostsManager
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetTrafficCostsJSON(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CorePublicProceduresManager()
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                   "Select TrafficCardTypeId,EntryBaseCost,NoCostStoppageDuration,ExcessStoppageDuration,ExcessStoppageCost 
                    from R2PrimaryParkingSystem.dbo.TblTrafficCosts 
                    Where Active=1
                    Order By TrafficCardTypeId for JSON AUTO")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select TrafficCardTypeId,EntryBaseCost,NoCostStoppageDuration,ExcessStoppageDuration,ExcessStoppageCost 
                    from R2PrimaryParkingSystem.dbo.TblTrafficCosts 
                    Where Active=1
                    Order By TrafficCardTypeId for JSON AUTO", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub RegisteringTrafficCost(YourRawTrafficCost As R2CoreParkingSystemTrafficCost)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "Update R2PrimaryParkingSystem.dbo.TblTrafficCosts Set Active=0 Where TrafficCardTypeId=@TrafficCardTypeId"
                Cmdsql.Parameters.Add("@TrafficCardTypeId", SqlDbType.BigInt).Value = YourRawTrafficCost.TrafficCardTypeId
                Cmdsql.ExecuteNonQuery()
                Cmdsql.CommandText = "Insert Into R2PrimaryParkingSystem.dbo.TblTrafficCosts(TrafficCardTypeId,EntryBaseCost,NoCostStoppageDuration,ExcessStoppageDuration,ExcessStoppageCost,Active)
                                      Values(@TrafficCardTypeId,@EntryBaseCost,@NoCostStoppageDuration,@ExcessStoppageDuration,@ExcessStoppageCost,@Active)"
                Cmdsql.Parameters.Add("@TrafficCardTypeId", SqlDbType.BigInt).Value = YourRawTrafficCost.TrafficCardTypeId
                Cmdsql.Parameters.Add("@EntryBaseCost", SqlDbType.BigInt).Value = YourRawTrafficCost.EntryBaseCost
                Cmdsql.Parameters.Add("@NoCostStoppageDuration", SqlDbType.Int).Value = YourRawTrafficCost.NoCostStoppageDuration
                Cmdsql.Parameters.Add("@ExcessStoppageDuration", SqlDbType.Int).Value = YourRawTrafficCost.ExcessStoppageDuration
                Cmdsql.Parameters.Add("@ExcessStoppageCost", SqlDbType.BigInt).Value = YourRawTrafficCost.ExcessStoppageCost
                Cmdsql.Parameters.Add("@Active", SqlDbType.Bit).Value = IIf(YourRawTrafficCost.Active, 1, 0)
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetTrafficCost(YourTrafficCardTypeId As Short, YourImmediately As Boolean) As R2CoreParkingSystemTrafficCost
            Try
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select * from R2PrimaryParkingSystem.dbo.TblTrafficCosts Where TrafficCardTypeId=" & YourTrafficCardTypeId & " and Active=1")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TrafficCostNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                      "Select * from R2PrimaryParkingSystem.dbo.TblTerraficCosts Where TblTrafficCosts=" & YourTrafficCardTypeId & " and Active=1", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New TrafficCostNotFoundException
                End If
                Return New R2CoreParkingSystemTrafficCost With {.TrafficCardTypeId = Ds.Tables(0).Rows(0).Item("TrafficCardTypeId"), .EntryBaseCost = Ds.Tables(0).Rows(0).Item("EntryBaseCost"), .NoCostStoppageDuration = Ds.Tables(0).Rows(0).Item("NoCostStoppageDuration"), .ExcessStoppageCost = Ds.Tables(0).Rows(0).Item("ExcessStoppageCost"), .ExcessStoppageDuration = Ds.Tables(0).Rows(0).Item("ExcessStoppageDuration")}
            Catch ex As TrafficCostNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class TrafficCostNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعاتی درخصوص هزینه های تردد یافت نشد"
            End Get
        End Property
    End Class

End Namespace