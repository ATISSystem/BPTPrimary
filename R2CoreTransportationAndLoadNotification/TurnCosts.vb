

Imports System.Data.SqlClient
Imports System.Reflection
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PublicProc
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreParkingSystem.TrafficCosts
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions

Namespace TurnCosts

    Public Class R2CoreTransportationAndLoadNotificationTurnCost
        Public SequentialTurnId As Int64
        Public SelfGoverCost As Int64
        Public TruckersAssociationCost As Int64
        Public TruckDriversAssociationCost As Int64
        Public ReadOnly Property TotalCost As Int64
            Get
                Return SelfGoverCost + TruckersAssociationCost + TruckDriversAssociationCost
            End Get
        End Property
    End Class

    Public Class R2CoreTransportationAndLoadNotificationRawTurnCost
        Public SeqTurnId As Int64
        Public SelfGoverCost As Int64
        Public TruckersAssociationCost As Int64
        Public TruckDriversAssociationCost As Int64
    End Class

    Public Class R2CoreTransportationAndLoadNotificationTurnCostManager
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetAllTurnCosts(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlClient.SqlCommand(
                        "Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,TurnCosts.SelfGoverCost,TurnCosts.TruckersAssociationCost,TurnCosts.TruckDriversAssociationCost 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts as TurnCosts
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On TurnCosts.SeqTId=SequentialTurns.SeqTId 
                         Order By SeqTTitle for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select SequentialTurns.SeqTId as SeqTurnId,SequentialTurns.SeqTTitle as SeqTurnTitle,TurnCosts.SelfGoverCost,TurnCosts.TruckersAssociationCost,TurnCosts.TruckDriversAssociationCost 
                         from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts as TurnCosts
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblSequentialTurns as SequentialTurns On TurnCosts.SeqTId=SequentialTurns.SeqTId 
                         Order By SeqTTitle for JSON Path", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TurnCostRegistering(YourTurnCost As R2CoreTransportationAndLoadNotificationRawTurnCost)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts(SeqTId,SelfGoverCost,TruckersAssociationCost,TruckDriversAssociationCost)
                                      Values(" & YourTurnCost.SeqTurnId & "," & YourTurnCost.SelfGoverCost & "," & YourTurnCost.TruckersAssociationCost & "," & YourTurnCost.TruckDriversAssociationCost & ")
"
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TurnCostDeleting(YourSeqTurnId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts Where SeqTId=" & YourSeqTurnId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetTurnCost(YourSequentialTurnId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnCost
            Try
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts Where SeqTId=" & YourSequentialTurnId & "")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnCostNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                  "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnCosts Where SeqTId=" & YourSequentialTurnId & "", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New TurnCostNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTurnCost With {.SequentialTurnId = Ds.Tables(0).Rows(0).Item("SeqTId"), .SelfGoverCost = Ds.Tables(0).Rows(0).Item("SelfGoverCost"), .TruckersAssociationCost = Ds.Tables(0).Rows(0).Item("TruckersAssociationCost"), .TruckDriversAssociationCost = Ds.Tables(0).Rows(0).Item("TruckDriversAssociationCost")}
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnCost(ByVal YourMoneyWallet As R2CoreMoneyWallet, YourSequentialTurnId As Int64) As Int64
            Try
                Dim InstanceTrafficCards = New R2CoreParkingSystemTrafficCardsManager(New R2DateTimeService)
                Dim InstanceTrafficCosts = New R2CoreParkingSystemTrafficCostsManager(New R2DateTimeService)
                Dim TrafficCard = InstanceTrafficCards.GetTrafficCard(YourMoneyWallet.MoneyWalletId)
                Dim TrafficCost = InstanceTrafficCosts.GetTrafficCost(TrafficCard.TrafficCardId, False)
                Dim TurnCost = GetTurnCost(YourSequentialTurnId, False)

                'احراز معیار محاسبه
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & TrafficCard.TrafficCardId & ") and (MblghA<>0) and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.EnterType & " or EEAccountingProcessType=" & R2CoreParkingSystemAccountings.SherkatHazinehNobat & " ) order by DateMilladiA desc")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) = 0 Then Return TurnCost.SelfGoverCost

                'تاخیر تا آخرین اکانت نوبت
                Dim Tavaghof As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTimeService.GetCurrentDateTimeMilladi())
                If Tavaghof >= TrafficCost.NoCostStoppageDuration Then
                    Return TurnCost.SelfGoverCost
                Else
                    Da.SelectCommand.CommandText = "Select Top 1 DateMilladiA from R2Primary.dbo.TblAccounting Where (CardId=" & TrafficCard.TrafficCardId & ") and (EEAccountingProcessType=" & R2CoreParkingSystemAccountings.AnjomanHazinehNobat & ") order by DateMilladiA desc"
                    Ds.Tables.Clear()
                    If Da.Fill(Ds) <= 0 Then Return 0
                    Dim TavaghofLastTurn As Int64 = DateDiff(DateInterval.Hour, Ds.Tables(0).Rows(0).Item("DateMilladiA"), _DateTimeService.GetCurrentDateTimeMilladi())
                    If TavaghofLastTurn <= Tavaghof Then
                        Return TurnCost.SelfGoverCost
                    Else
                        Return 0
                    End If
                End If
            Catch ex As TurnCostNotFoundException
                Throw ex
            Catch ex As TerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class
End Namespace