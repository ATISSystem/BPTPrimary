


Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.MonetaryCreditSupplySources
Imports R2Core.MonetarySupply
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.AESAlgorithms
Imports R2Core.SoftwareUserManagement
Imports System.Data.SqlClient
Imports System.Reflection

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

        'BPTChanged
        Public Class R2CoreMoneyWallet
            Public MoneyWalletId As Int64
            Public MoneyWalletCode As String
            Public Balance As Int64
        End Class

        'BPTChanged
        Public Class R2CoreMoneyWalletManager

            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Public Function CreateNewMoneyWallet() As Int64
                Dim Cmdsql As New SqlClient.SqlCommand
                Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
                Try
                    Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                    Dim InstanceSoftwareUserService = New SoftwareUserService(Nothing)
                    Dim AES = New AESAlgorithmsManager

                    Cmdsql.Connection.Open()
                    Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                    Cmdsql.CommandText = "Select Top 1 CardId from R2Primary.dbo.TblRfidCards with (tablockx) Order by Cardid Desc"
                    Cmdsql.ExecuteNonQuery()
                    Dim myCardID As Int64 = Cmdsql.ExecuteScalar + 1
                    Dim cardNONew = InstancePublicProcedures.RepeatStr("0", 7 - myCardID.ToString.Length) + myCardID.ToString + AES.GetSalt(3)
                    Cmdsql.CommandText = "Insert Into R2Primary.dbo.tblrfidcards(CardId,CardNo,Charge,UserIdSabt,UserIdEdit,PelakType,Pelak,Serial,NoMoney,Active,CompanyName,NameFamily,Mobile,Address,Tel,Tahvilg,DateTimeMilladiSabt,DateTimeMilladiEdit,DateShamsiSabt,DateShamsiEdit,CardType,TempCardType) 
                                      values(" & myCardID & ",'" & cardNONew & "',0," & InstanceSoftwareUserService.SystemUserId & "," & InstanceSoftwareUserService.SystemUserId & ",0,'','',0,1,'','','','','','','" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentShamsiDate() & "','" & _DateTimeService.GetCurrentShamsiDate() & "',0,0)"
                    Cmdsql.ExecuteNonQuery()
                    Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
                    Return myCardID
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
                Catch ex As MoneyWalletNotFoundException
                    Throw ex
                Catch ex As Exception
                    Throw ex
                End Try
            End Function

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

            Private WithEvents MS As MonetarySupply.R2CoreMonetarySupply = Nothing
            Private PayId As Int64
            Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            End Sub

            Private Function PaymentRequestRegistering(YourMCSSId As Int64, YourAmount As Int64, YourSoftwareUserId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
                Try
                    Dim InstanceSoftwareUsers = New R2CoreInstanseSoftwareUsersManager(New R2DateTimeService)
                    CmdSql.Connection.Open()
                    CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                    CmdSql.CommandText = "Select Top 1 PayId From R2Primary.dbo.TblPaymentRequests with (tablockx) Order By PayId Desc"
                    CmdSql.ExecuteNonQuery()
                    CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblPaymentRequests') "
                    Dim PayIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                    CmdSql.CommandText = "Insert Into R2Primary.dbo.TblPaymentRequests(MCSSId,SoftwareUserId,Amount,Authority,TransactionId,RefId,PaymentErrors,VerificationErrors,VerificationCount,UserId,DateTimeMilladi,DateShamsi,Time) 
                                          Values(" & YourMCSSId & "," & YourSoftwareUserId & "," & YourAmount & ",'','','','','',1," & InstanceSoftwareUsers.GetNSSSystemUser().UserId & ",'" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentShamsiDate() & "','" & _DateTimeService.GetCurrentTime() & "')"
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
                    Dim Ds As DataSet
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblPaymentRequests Where Authority='" & YourAuthority & "'", 0, Ds, New Boolean)
                    Return New R2StandardPaymentRequestStructure(Ds.Tables(0).Rows(0).Item("PayId"), Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("SoftwareUserId"), Ds.Tables(0).Rows(0).Item("Amount"), Ds.Tables(0).Rows(0).Item("Authority").trim, Ds.Tables(0).Rows(0).Item("TransactionId").trim, Ds.Tables(0).Rows(0).Item("RefId").trim, Ds.Tables(0).Rows(0).Item("PaymentErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationCount"), Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Sub DecreaseVerificationCount(YourPayId As Int64)
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
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
                    Dim Ds As DataSet
                    InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblPaymentRequests Where PayId=" & YourPayId & "", 0, Ds, New Boolean)
                    Return New R2StandardPaymentRequestStructure(Ds.Tables(0).Rows(0).Item("PayId"), Ds.Tables(0).Rows(0).Item("MCSSId"), Ds.Tables(0).Rows(0).Item("SoftwareUserId"), Ds.Tables(0).Rows(0).Item("Amount"), Ds.Tables(0).Rows(0).Item("Authority").trim, Ds.Tables(0).Rows(0).Item("TransactionId").trim, Ds.Tables(0).Rows(0).Item("RefId").trim, Ds.Tables(0).Rows(0).Item("PaymentErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationErrors").trim, Ds.Tables(0).Rows(0).Item("VerificationCount"), Ds.Tables(0).Rows(0).Item("UserId"), Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), Ds.Tables(0).Rows(0).Item("DateShamsi"), Ds.Tables(0).Rows(0).Item("Time"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function PaymentRequest(YourMCSSId As Int64, YourAmount As Int64, YourSoftwareUserId As Int64) As Int64
                Try
                    PayId = PaymentRequestRegistering(YourMCSSId, YourAmount, YourSoftwareUserId)
                    Dim InstanceMCSS As New R2CoreMClassMonetaryCreditSupplySourcesManager(_DateTimeService)
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
                    Dim InstanceMCSS = New R2CoreMClassMonetaryCreditSupplySourcesManager(_DateTimeService)
                    MS = New R2CoreMonetarySupply(InstanceMCSS.GetNSSMonetaryCreditSupplySource(YourMCSSId), Amount)
                    MS.StartVerification(YourAuthority)
                    Return PayId
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private Sub MS_MonetarySupplySuccessEvent(YourMonetarySupplyType As MonetarySupplyType, TransactionId As Long, Amount As Long, SupplyReport As String) Handles MS.MonetarySupplySuccessEvent
                Dim CmdSql As New SqlClient.SqlCommand
                CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
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
                CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
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

        'BPTChanged
        Public Class MoneyWalletNotFoundException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.MoneyWalletNotFoundException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.MoneyWalletNotFoundException).MsgId
            End Sub
        End Class

    End Namespace


End Namespace
