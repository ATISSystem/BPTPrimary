

Imports System.Data.SqlClient
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.PublicProcedures
Imports R2Core.SecurityAlgorithmsManagement.AESAlgorithms
Imports R2Core.SoftwareUserManagement

Namespace MoneyWallet

    'BPTChanged
    Namespace MoneyWallet

        'BPTChanged
        Public Class R2CoreMoneyWallet
            Public MoneyWalletId As Int64
            Public MoneyWalletCode As String
            Public Balance As Int64
        End Class

        'BPTChanged
        Public Class R2CoreMoneyWalletManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IDateTimeService
            Private _SoftwareUserService As SoftwareUserService

            Public Sub New(YourDateTimeService As IDateTimeService)
                Try
                    _DateTimeService = YourDateTimeService
                    InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
                    _SoftwareUserService = New SoftwareUserService(Nothing)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub

            Public Function CreateNewMoneyWallet() As Int64
                Dim Cmdsql As New SqlClient.SqlCommand
                Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
                Try
                    Dim InstancePublicProcedures = New R2CorePublicProceduresManager
                    Dim AES = New AESAlgorithmsManager

                    Cmdsql.Connection.Open()
                    Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                    Cmdsql.CommandText = "Select Top 1 CardId from R2Primary.dbo.TblRfidCards with (tablockx) Order by Cardid Desc"
                    Cmdsql.ExecuteNonQuery()
                    Dim myCardID As Int64 = Cmdsql.ExecuteScalar + 1
                    Dim cardNONew = InstancePublicProcedures.RepeatStr("0", 7 - myCardID.ToString.Length) + myCardID.ToString + AES.GetSalt(3)
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.tblrfidcards(CardId,CardNo,Charge,UserIdSabt,UserIdEdit,PelakType,Pelak,Serial,NoMoney,Active,CompanyName,NameFamily,Mobile,Address,Tel,Tahvilg,DateTimeMilladiSabt,DateTimeMilladiEdit,DateShamsiSabt,DateShamsiEdit,CardType,TempCardType) 
                                      values(" & myCardID & ",'" & cardNONew & "',0," & _SoftwareUserService.SystemUserId & "," & _SoftwareUserService.SystemUserId & ",0,'','',0,1,'','','','','','',convert(varchar, getdate(), 20),convert(varchar, getdate(), 20),R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()),R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()),0,0)"
                    Cmdsql.ExecuteNonQuery()
                    Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
                    Return myCardID
                Catch ex As SqlException
                    If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As Exception
                    If Cmdsql.Connection.State <> ConnectionState.Closed Then
                        Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                    End If
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetMoneyWallet(YourMoneyWalletId As Int64, YourImmediately As Boolean) As R2CoreMoneyWallet
                Try
                    Dim DS As New DataSet
                    If YourImmediately Then
                        Dim Da As New SqlClient.SqlDataAdapter
                        Da.SelectCommand = New SqlCommand("
                               Select MoneyWallets.CardId As MoneyWalletId , MoneyWallets.CardNo As MoneyWalletCode , MoneyWallets.Charge As Balance  
                               From R2Primary.dbo.TblRFIDCards as MoneyWallets
                               Where MoneyWallets.CardId = " & YourMoneyWalletId & "")
                        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                        If Da.Fill(DS) <= 0 Then Throw New MoneyWalletNotFoundException
                    Else
                        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                              "Select MoneyWallets.CardId As MoneyWalletId , MoneyWallets.CardNo As MoneyWalletCode , MoneyWallets.Charge As Balance
                               From R2Primary.dbo.TblRFIDCards as MoneyWallets
                               Where MoneyWallets.CardId = " & YourMoneyWalletId & "", 0, DS, New Boolean).GetRecordsCount = 0 Then
                            Throw New MoneyWalletNotFoundException
                        End If
                    End If
                    Return New R2CoreMoneyWallet With {.MoneyWalletId = DS.Tables(0).Rows(0).Item("MoneyWalletId"), .MoneyWalletCode = DS.Tables(0).Rows(0).Item("MoneyWalletCode"), .Balance = DS.Tables(0).Rows(0).Item("Balance")}
                Catch ex As SqlException
                    Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
                Catch ex As FileNotExistException
                    Throw ex
                Catch ex As MoneyWalletNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

        End Class

    End Namespace

    'BPTChanged
    Namespace MoneyWalletCharging

        Public Class R2CoreMoneyWalletChargingManager

        End Class


    End Namespace

    'BPTChanged
    Namespace Exceptions

        'BPTChanged
        Public Class SoftwareUserMoneyWalletNotFoundException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.SoftwareUserMoneyWalletNotFoundException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.SoftwareUserMoneyWalletNotFoundException).MsgId
            End Sub
        End Class

        'BPTChanged
        Public Class MoneyWalletNotFoundException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.MoneyWalletNotFoundException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.MoneyWalletNotFoundException).MsgId
            End Sub
        End Class

    End Namespace


End Namespace
