

Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.EntityRelations
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports System.Data.SqlClient
Imports System.Reflection
Imports R2Core.ConfigurationManagement
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions
Imports R2Core.PredefinedMessagesManagement
Imports R2CoreParkingSystem.PredefinedMessagesManagement
Imports R2CoreParkingSystem.MoneyWalletChargeManagement.Exceptions
Imports R2Core.MonetaryCreditSupplySources
Imports R2Core.MoneyWallet.PaymentRequests
Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json
Imports R2Core.PublicProc
Imports R2CoreParkingSystem.UserChargeProcessManagement
Imports R2Core.DateTimeProvider

Namespace MoneyWalletManagement

    Public Enum BagPayType
        None = 0
        AddMoney = 1
        MinusMoney = 2
    End Enum

    Public Class R2CoreParkingSystemInstanceMoneyWalletManager
        Private _DateTime As R2DateTime = New R2DateTime

        Public Function GetMoneyWalletCharge(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("select top 1 charge from R2Primary.dbo.tblrfidcards where ltrim(rtrim(cardno))='" & YourNSSTrafficCard.CardNo & "' order by cardID DESC")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                ds.Tables.Clear()
                If Da.Fill(ds) <> 0 Then
                    Return ds.Tables(0).Rows(0).Item("charge")
                Else
                    Throw New MoneyWalletNotExistException
                End If
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ActMoneyWalletNextStatus(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourBagType As BagPayType, YourMblgh As Int64, YourAccountCode As R2CoreParkingSystemAccountings, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager()
            Dim InstanceAccounting = New R2CoreParkingSystemInstanceAccountingManager()
            Try
                Dim myNSSCar As R2StandardCarStructure = Nothing
                Try
                    myNSSCar = InstanceCars.GetNSSCar(InstanceCars.GetnIdCarFromCardId(YourNSSTrafficCard.CardId))
                Catch ex As R2CoreParkingSystemRelatedCarNotExistException
                Catch ex As RelatedTerraficCardNotFoundException
                Catch exx As GetDataException
                Catch ex As GetNSSException
                End Try
                Dim myMoneyWalletCurrentCharge As Int64 = GetMoneyWalletCharge(YourNSSTrafficCard)
                Dim myMoneyWalletReminder As Int64
                If YourBagType = BagPayType.AddMoney Then
                    myMoneyWalletReminder = myMoneyWalletCurrentCharge + YourMblgh
                ElseIf YourBagType = BagPayType.MinusMoney Then
                    myMoneyWalletReminder = myMoneyWalletCurrentCharge - YourMblgh
                End If
                InstanceAccounting.InsertAccounting(New R2StandardEnterExitAccountingStructure(YourNSSTrafficCard, YourAccountCode, _DateTime.GetCurrentShamsiDate(), _DateTime.GetCurrentTime(), _DateTime.GetCurrentDateTimeMilladi(), myNSSCar, R2CoreMClassConfigurationManagement.GetComputerCode(), YourMblgh, YourUserNSS.UserId, myMoneyWalletCurrentCharge, myMoneyWalletReminder))
                AddMinusMoneyWallet(YourNSSTrafficCard, YourBagType, YourMblgh)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AddMinusMoneyWallet(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourBagPayType As BagPayType, YourMblgh As Int64)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                If YourBagPayType = BagPayType.AddMoney Then
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge+" & YourMblgh & " where CardNo='" & YourNSSTrafficCard.CardNo & "'"
                Else
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge-" & YourMblgh & " where CardNo='" & YourNSSTrafficCard.CardNo & "'"
                End If
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    Public Class R2CoreParkingSystemMClassMoneyWalletManagement

        Private Shared _DateTime As R2DateTime = New R2DateTime

        Public Shared Sub ActMoneyWalletNextStatus(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourBagType As BagPayType, YourMblgh As Int64, YourAccountCode As R2CoreParkingSystemAccountings, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Try
                Dim myNSSCar As R2StandardCarStructure = Nothing
                Try
                    myNSSCar = R2CoreParkingSystemMClassCars.GetNSSCar(R2CoreParkingSystemMClassCars.GetnIdCarFromCardId(YourNSSTrafficCard.CardId))
                Catch ex As R2CoreParkingSystemRelatedCarNotExistException
                Catch ex As RelatedTerraficCardNotFoundException
                Catch exx As GetDataException
                Catch ex As GetNSSException
                End Try
                Dim myMoneyWalletCurrentCharge As Int64 = GetMoneyWalletCharge(YourNSSTrafficCard)
                Dim myMoneyWalletReminder As Int64
                If YourBagType = BagPayType.AddMoney Then
                    myMoneyWalletReminder = myMoneyWalletCurrentCharge + YourMblgh
                ElseIf YourBagType = BagPayType.MinusMoney Then
                    myMoneyWalletReminder = myMoneyWalletCurrentCharge - YourMblgh
                End If
                R2CoreParkingSystemMClassAccountingManagement.InsertAccounting(New R2StandardEnterExitAccountingStructure(YourNSSTrafficCard, YourAccountCode, _DateTime.GetCurrentShamsiDate(), _DateTime.GetCurrentTime(), _DateTime.GetCurrentDateTimeMilladi(), myNSSCar, R2CoreMClassConfigurationManagement.GetComputerCode(), YourMblgh, YourUserNSS.UserId, myMoneyWalletCurrentCharge, myMoneyWalletReminder))
                AddMinusMoneyWallet(YourNSSTrafficCard, YourBagType, YourMblgh)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetMoneyWalletCharge(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Int64
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("select top 1 charge from R2Primary.dbo.tblrfidcards where ltrim(rtrim(cardno))='" & YourNSSTrafficCard.CardNo & "' order by cardID DESC")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                ds.Tables.Clear()
                If Da.Fill(ds) <> 0 Then
                    Return ds.Tables(0).Rows(0).Item("charge")
                Else
                    Throw New MoneyWalletNotExistException
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub AddMinusMoneyWallet(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourBagPayType As BagPayType, YourMblgh As Int64)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                If YourBagPayType = BagPayType.AddMoney Then
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge+" & YourMblgh & " where CardNo='" & YourNSSTrafficCard.CardNo & "'"
                Else
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge-" & YourMblgh & " where CardNo='" & YourNSSTrafficCard.CardNo & "'"
                End If
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetMoneyWalletReminder(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourBagPayType As BagPayType, YourMblgh As Int64) As Int64
            Try
                If YourBagPayType = BagPayType.AddMoney Then
                    Return GetMoneyWalletCharge(YourNSSTrafficCard) + YourMblgh
                ElseIf YourBagPayType = BagPayType.MinusMoney Then
                    Return GetMoneyWalletCharge(YourNSSTrafficCard) - YourMblgh
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMoneyWalletAllMoney(YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourAccountType As R2CoreParkingSystemAccountings, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Try
                Dim myAllMoney As Int64 = GetMoneyWalletCharge(YourNSSTrafficCard)
                If myAllMoney >= 0 Then
                    ActMoneyWalletNextStatus(YourNSSTrafficCard, BagPayType.MinusMoney, myAllMoney, YourAccountType, YourUserNSS)
                Else
                    ActMoneyWalletNextStatus(YourNSSTrafficCard, BagPayType.AddMoney, -myAllMoney, YourAccountType, YourUserNSS)
                End If
                Return myAllMoney
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Shared Sub TransferSourceMoneyWalletChargeToSecond(ByVal YourNSSSourceTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, ByVal YourNSSSecondTrafficCard As R2CoreParkingSystemStandardRFIDCardStructure)
        '    Try
        '        Dim myAllMoney as Int64= GetMoneyWalletAllMoney(YourNSSSourceTrafficCard,R2CoreParkingSystemAccountings.TransferallChargeToAnother)
        '        ActMoneyWalletNextStatus(YourNSSSecondTrafficCard,BagPayType.AddMoney,myAllMoney,R2CoreParkingSystemAccountings.TransferallChargeToAnother)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub

    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemMoneyWalletManager

        Private _R2DateTimeService As IR2DateTimeService
        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Function GetMoneyWallet(YourSoftwareuser As R2CoreSoftwareUser) As R2CoreMoneyWallet
            Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            Dim InstanceMoneyWallet = New R2Core.MoneyWallet.MoneyWallet.R2CoreMoneyWalletManager
            Try
                Dim SqlString = String.Empty
                If YourSoftwareuser.UserTypeId = 1 Or YourSoftwareuser.UserTypeId = 2 Then
                    SqlString = "select  Top 1 SoftwareUsersRelationMoneyWallet.CardId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                                    Inner Join R2Primary.dbo.TblSoftwareUsersRelationMoneyWallet as SoftwareUsersRelationMoneyWallet On SoftwareUsers.UserId=SoftwareUsersRelationMoneyWallet.UserId 
                                 Where SoftwareUsers.UserId=" & YourSoftwareuser.UserId & "  and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and SoftwareUsersRelationMoneyWallet.RelationActive=1"
                ElseIf YourSoftwareuser.UserTypeId = 3 Then
                    SqlString = "Select Top 1 TCardsRCar.CardId
                    from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                          Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On SoftwareUsers.UserId=EntityRelations.E1 
                          Inner Join dbtransport.dbo.TbDriver as Drivers On EntityRelations.E2=Drivers.nIDDriver 
                          Inner Join dbtransport.dbo.TbCarAndPerson as CarAndPersons On Drivers.nIDDriver=CarAndPersons.nIDPerson
                          Inner Join dbtransport.dbo.TbCar as Cars On CarAndPersons.nIDCar=Cars.nIDCar 
                          Inner Join R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars as TCardsRCar On Cars.nIDCar=TCardsRCar.nCarId 
                    Where SoftwareUsers.UserId=" & YourSoftwareuser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and EntityRelations.RelationActive=1 and  
                              EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.SoftwareUser_Driver & " and Cars.ViewFlag=1 and TCardsRCar.RelationActive=1 and CarAndPersons.snRelation=2 
                              and ((DATEDIFF(SECOND,TCardsRCar.RelationTimeStamp,getdate())<240) or (TCardsRCar.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                              and ((DATEDIFF(SECOND,CarAndPersons.RelationTimeStamp,getdate())<240) or (CarAndPersons.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                    Order By CarAndPersons.nIDCarAndPerson Desc,TCardsRCar.RelationId Desc,TCardsRCar.RelationTimeStamp Desc"
                ElseIf YourSoftwareuser.UserTypeId = 7 Then
                    SqlString = "select  Top 1 TransportCompaniesRelationMoneyWallets.CardId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationSoftwareUsers as TransportCompaniesRelationSoftwareUsers On SoftwareUsers.UserId=TransportCompaniesRelationSoftwareUsers.UserId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TransportCompaniesRelationSoftwareUsers.TCId=TransportCompanies.TCId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TransportCompaniesRelationMoneyWallets On TransportCompanies.TCId=TransportCompaniesRelationMoneyWallets.TCId 
                                 Where SoftwareUsers.UserId=" & YourSoftwareuser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and TransportCompanies.Active=1"
                ElseIf YourSoftwareuser.UserTypeId = 15 Then
                    SqlString = "select  Top 1 FPCsRelationMoneyWallets.CardId from R2Primary.dbo.TblSoftwareUsers as SoftwareUsers
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationSoftwareUsers as FPCsRelationSoftwareUsers On SoftwareUsers.UserId=FPCsRelationSoftwareUsers.UserId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCenters as FPCs On FPCsRelationSoftwareUsers.FPCId=FPCs.FPCId 
                                   Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblFactoriesAndProductionCentersRelationMoneyWallets as FPCsRelationMoneyWallets On FPCs.FPCId=FPCsRelationMoneyWallets.FPCId 
                                 Where SoftwareUsers.UserId=" & YourSoftwareuser.UserId & " and SoftwareUsers.UserActive=1 and SoftwareUsers.Deleted=0 and FPCs.Active=1"
                End If
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, SqlString, 300, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanceMoneyWallet.GetMoneyWallet(Convert.ToInt64(Ds.Tables(0).Rows(0).Item("CardId")), True)
                Else
                    Throw New SoftwareUserMoneyWalletNotFoundException
                End If
            Catch ex As SoftwareUserMoneyWalletNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetMoneyWalletfromCarId(YourTruckId As Int64, YourImmediately As Boolean) As R2CoreMoneyWallet
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstanceTrafficCards = New R2CoreParkingSystemInstanceTrafficCardsManager

                Dim SqlString = "Select Top 1 RFIDCards.CardId,RFIDCards.CardNo,RFIDCards.Charge from dbtransport.dbo.TbCar as Cars 
                                    Inner Join R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars as TCardsRCar On Cars.nIDCar=TCardsRCar.nCarId 
                                    Inner Join R2Primary.dbo.TblRFIDCards as RFIDCards On TCardsRCar.CardId=RFIDCards.CardId 
                                 Where Cars.ViewFlag=1 and Cars.nIDCar=" & YourTruckId & " and
                                    TCardsRCar.RelationActive=1 and ((DATEDIFF(SECOND,TCardsRCar.RelationTimeStamp,getdate())<240) or (TCardsRCar.RelationTimeStamp='2015-01-01 00:00:00.000')) 
                                 Order By TCardsRCar.RelationId Desc,TCardsRCar.RelationTimeStamp Desc"
                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(SqlString)
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New MoneyWalletNotExistException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, SqlString, 300, Ds, New Boolean).GetRecordsCount = 0 Then Throw New MoneyWalletNotExistException
                End If
                Return New R2CoreMoneyWallet With {.MoneyWalletId = Ds.Tables(0).Rows(0).Item("CardId"), .MoneyWalletCode = Ds.Tables(0).Rows(0).Item("CardNo"), .Balance = Ds.Tables(0).Rows(0).Item("Charge")}
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetMoneyWalletfromTransportCompanyId(YourTransportCompanyId As Int64) As R2CoreMoneyWallet
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                       "Select Top 1 MoneyWallets.CardId as MoneyWalletId,MoneyWallets.CardNo as MoneyWalletCode,MoneyWallets.Charge as Balance
                        from R2Primary.dbo.TblRFIDCards as MoneyWallets 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompaniesRelationMoneyWallets as TCRMoneyWallets On MoneyWallets.CardId=TCRMoneyWallets.CardId 
                           Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportCompanies as TransportCompanies On TCRMoneyWallets.TCId=TransportCompanies.TCId 
                        Where MoneyWallets.Active=1 and TransportCompanies.TCId=" & YourTransportCompanyId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New RelatedMoneyWalletNotFoundException
                Return New R2CoreMoneyWallet With {.MoneyWalletId = DS.Tables(0).Rows(0).Item("MoneyWalletId"), .MoneyWalletCode = DS.Tables(0).Rows(0).Item("MoneyWalletCode").trim, .Balance = DS.Tables(0).Rows(0).Item("Balance")}
            Catch ex As RelatedMoneyWalletNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetMoneyWalletCharge(YourMoneyWalletId As Int64) As Int64
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("select charge from R2Primary.dbo.tblrfidcards where CardId=" & YourMoneyWalletId & "")
                Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection()
                ds.Tables.Clear()
                If Da.Fill(ds) <> 0 Then
                    Return ds.Tables(0).Rows(0).Item("charge")
                Else
                    Throw New MoneyWalletNotExistException
                End If
            Catch ex As MoneyWalletNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ActMoneyWalletNextStatus(YourMoneyWalletId As Int64, YourBagType As BagPayType, YourMblgh As Int64, YourAccountCode As Int64, YourSoftwareUserId As Int64)
            Dim InstanceCars = New R2CoreParkingSystemInstanceCarsManager()
            Dim InstanceAccounting = New R2CoreParkingSystemAccountingManager(New R2DateTimeService)
            Try
                Dim MoneyWalletCurrentCharge As Int64 = GetMoneyWalletCharge(YourMoneyWalletId)
                Dim myMoneyWalletReminder As Int64
                If YourBagType = BagPayType.AddMoney Then
                    myMoneyWalletReminder = MoneyWalletCurrentCharge + YourMblgh
                ElseIf YourBagType = BagPayType.MinusMoney Then
                    myMoneyWalletReminder = MoneyWalletCurrentCharge - YourMblgh
                End If
                InstanceAccounting.InsertAccounting(New R2CoreParkingSystemAccounting With {.AccountingTypeId = YourAccountCode, .MoneyWalletId = YourMoneyWalletId, .Mblgh = YourMblgh, .SoftwareUserId = YourSoftwareUserId})
                AddMinusMoneyWallet(YourMoneyWalletId, YourBagType, YourMblgh)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AddMinusMoneyWallet(YourMoneyWalletId As Int64, YourBagPayType As BagPayType, YourMblgh As Int64)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                If YourBagPayType = BagPayType.AddMoney Then
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge+" & YourMblgh & " where CardId=" & YourMoneyWalletId & ""
                Else
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards set Charge=charge-" & YourMblgh & " where CardId=" & YourMoneyWalletId & ""
                End If
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    'BPTChanged
    Namespace Exceptions

        Public Class MoneyWalletNotExistException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کیف پول مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class MoneyWalletCurrentChargeNotEnoughException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "موجودی کیف پول کافی نیست"
                End Get
            End Property
        End Class

        Public Class RelatedMoneyWalletNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کیف پول مرتبط یافت نشد"
                End Get
            End Property
        End Class


    End Namespace
End Namespace

Namespace MoneyWalletChargeManagement

    Public Class R2StandardMoneyWalletChargeStructure

        Public Sub New()
            MyBase.New()
            _NSSTrafficCard = Nothing
            _Mblgh = Int64.MinValue
            _UserId = Int64.MinValue
            _Mobile = String.Empty
            _DateTimeMilladi = String.Empty
            _DateShamsi = String.Empty
            _TimeCharge = String.Empty
            _Radifx = Int32.MinValue
            _Tash = Int64.MinValue
        End Sub

        Public Sub New(ByVal YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, ByVal YourMblgh As Int64, ByVal YourUserId As Int64, ByVal YourMobile As String, ByVal YourDateTimeMilladi As DateTime, ByVal YourDateShamsi As String, ByVal YourTash As Int64, ByVal YourRadifx As Int32, ByVal YourTimeCharge As String)
            _NSSTrafficCard = YourNSSTrafficCard
            _Mblgh = YourMblgh
            _UserId = YourUserId
            _Mobile = YourMobile
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Radifx = YourRadifx
            _TimeCharge = YourTimeCharge
            _Tash = YourTash
        End Sub

        Public Property NSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure

        Public Property Radifx As Int32

        Public Property DateShamsi As String

        Public Property TimeCharge As String

        Public Property DateTimeMilladi As DateTime

        Public Property Mblgh As Int64

        Public Property UserId As Int64

        Public Property Mobile As String

        Public Property Tash As Int64


    End Class

    Public Class R2StandardMoneyWalletChargeExtendedStructure
        Inherits R2StandardMoneyWalletChargeStructure

        Public Sub New()
            MyBase.New()
            _UserName = String.Empty
        End Sub

        Public Sub New(ByVal YourNSSMoneyWalletCharge As R2StandardMoneyWalletChargeStructure, YourUserName As String)
            MyBase.New(YourNSSMoneyWalletCharge.NSSTrafficCard, YourNSSMoneyWalletCharge.Mblgh, YourNSSMoneyWalletCharge.UserId, YourNSSMoneyWalletCharge.Mobile, YourNSSMoneyWalletCharge.DateTimeMilladi, YourNSSMoneyWalletCharge.DateShamsi, YourNSSMoneyWalletCharge.Tash, YourNSSMoneyWalletCharge.Radifx, YourNSSMoneyWalletCharge.TimeCharge)
            _UserName = YourUserName
        End Sub

        Public Property UserName As String

    End Class

    Public Class R2CoreParkingSystemInstanceMoneyWalletChargeManager
        Private _DateTime As R2DateTime = New R2DateTime()

        Public Sub SabtCharge(ByVal MoneyWalletCharge As R2StandardMoneyWalletChargeStructure)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "select top 1 radifx from R2Primary.dbo.TblMoneyWalletCharges where CardId='" & MoneyWalletCharge.NSSTrafficCard.CardId & "' order by radifx desc"
                Dim myRadifx As Int32 = Cmdsql.ExecuteScalar + 1
                Cmdsql.CommandText = "insert into R2Primary.dbo.TblMoneyWalletCharges(CardId,Radifx,DateShamsi,TimeCharge,DateTimeMilladi,Mblgh,UserId,Mobile,Tash) values('" & MoneyWalletCharge.NSSTrafficCard.CardId & "'," & myRadifx & ",'" & _DateTime.GetCurrentShamsiDate() & "','" & _DateTime.GetCurrentTime() & "','" & _DateTime.GetCurrentDateTimeMilladi() & "'," & MoneyWalletCharge.Mblgh & "," & MoneyWalletCharge.UserId & ",'" & MoneyWalletCharge.Mobile & "'," & MoneyWalletCharge.Tash & ")"
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetMoneyWalletChargeCollection(ByVal YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourTotalNumberofRecordsRequested As Int64) As List(Of R2StandardMoneyWalletChargeExtendedStructure)
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top " & YourTotalNumberofRecordsRequested & " SoftWare.UserName,Charge.* from R2Primary.dbo.TblMoneyWalletCharges AS Charge 
                               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftWare On Charge.UserId=SoftWare.UserId
                             Where Charge.CardId='" & YourNSSTrafficCard.CardId & "' Order by DateTimeMilladi Desc", 0, Ds, New Boolean)
                Dim Lst = New List(Of R2StandardMoneyWalletChargeExtendedStructure)
                For loopx As Int16 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(New R2StandardMoneyWalletChargeExtendedStructure(New R2StandardMoneyWalletChargeStructure(YourNSSTrafficCard, Ds.Tables(0).Rows(loopx).Item("mblgh"), Ds.Tables(0).Rows(loopx).Item("userid"), Ds.Tables(0).Rows(0).Item("Mobile").trim, Ds.Tables(0).Rows(loopx).Item("DateTimeMilladi"), Ds.Tables(0).Rows(loopx).Item("DateShamsi").trim, Ds.Tables(0).Rows(loopx).Item("Tash"), Ds.Tables(0).Rows(loopx).Item("Radifx"), Ds.Tables(0).Rows(loopx).Item("TimeCharge").trim), Ds.Tables(0).Rows(loopx).Item("UserName").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public Class R2CoreParkingSystemMClassMoneyWalletChargeManagement

        Private Shared _DateTime As R2DateTime = New R2DateTime()
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Shared Sub SabtCharge(ByVal MoneyWalletCharge As R2StandardMoneyWalletChargeStructure)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "select top 1 radifx from R2Primary.dbo.TblMoneyWalletCharges where CardId='" & MoneyWalletCharge.NSSTrafficCard.CardId & "' order by radifx desc"
                Dim myRadifx As Int32 = Cmdsql.ExecuteScalar + 1
                Cmdsql.CommandText = "insert into R2Primary.dbo.TblMoneyWalletCharges(CardId,Radifx,DateShamsi,TimeCharge,DateTimeMilladi,Mblgh,UserId,Mobile,Tash) values('" & MoneyWalletCharge.NSSTrafficCard.CardId & "'," & myRadifx & ",'" & _DateTime.GetCurrentShamsiDate() & "','" & _DateTime.GetCurrentTime() & "','" & _DateTime.GetCurrentDateTimeMilladi() & "'," & MoneyWalletCharge.Mblgh & "," & MoneyWalletCharge.UserId & ",'" & MoneyWalletCharge.Mobile & "'," & MoneyWalletCharge.Tash & ")"
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function HaveMoneyWalletChargeSavabegh(ByVal YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As Boolean
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "select cardid from R2Primary.dbo.TblMoneyWalletCharges where cardid=" & YourTrafficCard.CardId & "", 1, Ds, New Boolean).GetRecordsCount = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetMoneyWalletChargeCollection(ByVal YourNSSTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourTotalNumberofRecordsRequested As Int64) As List(Of R2StandardMoneyWalletChargeExtendedStructure)
            Try
                Dim Lst As List(Of R2StandardMoneyWalletChargeExtendedStructure) = New List(Of R2StandardMoneyWalletChargeExtendedStructure)
                Dim Ds As New DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                          "Select Top " & YourTotalNumberofRecordsRequested & " SoftWare.UserName,Charge.* from R2Primary.dbo.TblMoneyWalletCharges AS Charge 
                               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftWare On Charge.UserId=SoftWare.UserId
                             Where Charge.CardId='" & YourNSSTrafficCard.CardId & "' Order by DateTimeMilladi Desc", 1, Ds, New Boolean)
                For loopx As Int16 = 0 To Ds.Tables(0).Rows.Count - 1
                    Lst.Add(New R2StandardMoneyWalletChargeExtendedStructure(New R2StandardMoneyWalletChargeStructure(YourNSSTrafficCard, Ds.Tables(0).Rows(loopx).Item("mblgh"), Ds.Tables(0).Rows(loopx).Item("userid"), Ds.Tables(0).Rows(0).Item("Mobile").trim, Ds.Tables(0).Rows(loopx).Item("DateTimeMilladi"), Ds.Tables(0).Rows(loopx).Item("DateShamsi").trim, Ds.Tables(0).Rows(loopx).Item("Tash"), Ds.Tables(0).Rows(loopx).Item("Radifx"), Ds.Tables(0).Rows(loopx).Item("TimeCharge").trim), Ds.Tables(0).Rows(loopx).Item("UserName").trim))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class


    Namespace MoneyWalletChargingAmountsManagement

        Public Class R2CoreParkingSystemMoneyWalletChargingAmountStructure

            Public Sub New()
                MyBase.New()
                _MWCAId = Int64.MinValue
                _MWCAName = String.Empty
                _MWCATitle = String.Empty
                _MWCARial = Int64.MinValue
                _UserId = Int64.MinValue
                _DateTimeMilladi = Now
                _DateShamsi = String.Empty
                _Time = String.Empty
                _Active = Boolean.FalseString
                _ViewFlag = Boolean.FalseString
                _Deleted = Boolean.FalseString
            End Sub

            Public Sub New(YourMWCAId As Int64, YourMWCAName As String, YourMWCATitle As String, YourMWCARial As Int64, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourActive As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
                _MWCAId = YourMWCAId
                _MWCAName = YourMWCAName
                _MWCATitle = YourMWCATitle
                _MWCARial = YourMWCARial
                _UserId = YourUserId
                _DateTimeMilladi = YourDateTimeMilladi
                _DateShamsi = YourDateShamsi
                _Time = YourTime
                _Active = YourActive
                _ViewFlag = YourViewFlag
                _Deleted = YourDeleted
            End Sub

            Public Property MWCAId As Int64
            Public Property MWCAName As String
            Public Property MWCATitle As String
            Public Property MWCARial As Int64
            Public Property UserId As Int64
            Public Property DateTimeMilladi As DateTime
            Public Property DateShamsi As String
            Public Property Time As String
            Public Property Active As Boolean
            Public Property ViewFlag As Boolean
            Public Property Deleted As Boolean


        End Class

        Public Class R2CoreParkingSystemMoneyWalletChargingAmountsManager

            'Public Function GetActiveAmounts(YourRequesterId As Int64) As List(Of R2CoreParkingSystemMoneyWalletChargingAmountStructure)
            '    Try
            '        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            '        Dim DS As DataSet
            '        InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
            '            "Select MoneyWalletChargingAmounts.* from R2Primary.dbo.TblMoneyWalletChargingAmounts as MoneyWalletChargingAmounts 
            '                Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On MoneyWalletChargingAmounts.MWCAId=EntityRelations.E2 
            '             Where EntityRelations.ERTypeId=" & R2CoreParkingSystemEntityRelationTypes.Requester_MWCA & " and EntityRelations.RelationActive=1 
            '                   and MoneyWalletChargingAmounts.Active=1 and MoneyWalletChargingAmounts.Deleted=0 
            '                   and EntityRelations.E1=" & RequesterManagement.R2CoreParkingSystemRequesters.WCMoneyWalletCharging & " Order By MoneyWalletChargingAmounts.MWCARial", 3600, DS, New Boolean)
            '        Dim Lst = New List(Of R2CoreParkingSystemMoneyWalletChargingAmountStructure)
            '        For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
            '            Lst.Add(New R2CoreParkingSystemMoneyWalletChargingAmountStructure(DS.Tables(0).Rows(Loopx).Item("MWCAId"), DS.Tables(0).Rows(Loopx).Item("MWCAName").trim, DS.Tables(0).Rows(Loopx).Item("MWCATitle").trim, DS.Tables(0).Rows(Loopx).Item("MWCARial"), DS.Tables(0).Rows(Loopx).Item("UserId"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DS.Tables(0).Rows(Loopx).Item("Time").trim, DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
            '        Next
            '        Return Lst
            '    Catch ex As Exception
            '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            '    End Try
            'End Function

            Public Function GetNSSAmount(YourMWCAId As Int64) As R2CoreParkingSystemMoneyWalletChargingAmountStructure
                Try
                    Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                    Dim DS As DataSet
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                        "Select Top 1 MoneyWalletChargingAmounts.* from R2Primary.dbo.TblMoneyWalletChargingAmounts as MoneyWalletChargingAmounts 
                         Where MoneyWalletChargingAmounts.MWCAId=" & YourMWCAId & " and MoneyWalletChargingAmounts.Deleted=0", 3600, DS, New Boolean)
                    Return New R2CoreParkingSystemMoneyWalletChargingAmountStructure(DS.Tables(0).Rows(0).Item("MWCAId"), DS.Tables(0).Rows(0).Item("MWCAName").trim, DS.Tables(0).Rows(0).Item("MWCATitle").trim, DS.Tables(0).Rows(0).Item("MWCARial"), DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim, DS.Tables(0).Rows(0).Item("Time").trim, DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Deleted"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class



    End Namespace

    'BPTChanged
    Public Class R2CoreParkingSystemMoneyWalletChargeManager

        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        'مبلغ امانت به واحد ریال است
        Public Function PaymentRequest(YourAmount As Int64, YourUserId As Int64) As String
            Try
                Dim InstanceConfiguration = New R2Core.ConfigurationManagement.R2CoreInstanceConfigurationManager
                Dim MaxChargeAmount = InstanceConfiguration.GetConfigInt64(R2CoreParkingSystemConfigurations.MoneyWalletCharge, 0)
                Dim Amount = YourAmount * 10
                If Amount > MaxChargeAmount Then Throw New ChargingAmountInvalidException

                Dim WS = New R2Core.R2PrimaryWS.R2PrimaryWebService()
                Dim PayId = WS.WebMethodPaymentRequest(R2CoreMonetaryCreditSupplySources.AqayepardakhtPaymentGate, Amount, YourUserId, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword))
                'Dim PayId = WS.WebMethodPaymentRequest(R2CoreMonetaryCreditSupplySources.ZarrinPalPaymentGate, YourAmount, YourUserId, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword))
                'Dim PayId = WS.WebMethodPaymentRequest(R2CoreMonetaryCreditSupplySources.ShepaPaymentGate, YourAmount, YourUserId, WS.WebMethodLogin(R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserShenaseh, R2CoreMClassSoftwareUsersManagement.GetNSSSystemUser().UserPassword))
                Dim InstancePaymentRequests = New R2CoreInstansePaymentRequestsManager()
                Dim NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId)
                While ((NSSPaymentRequest.Authority = String.Empty) And (NSSPaymentRequest.PaymentErrors = String.Empty))
                    System.Threading.Thread.Sleep(500) : NSSPaymentRequest = InstancePaymentRequests.GetNSSPayment(PayId)
                End While
                If NSSPaymentRequest.Authority <> String.Empty Then
                    Return JsonConvert.SerializeObject(New With {.Authority = NSSPaymentRequest.Authority, .PaymentURI = InstanceConfiguration.GetConfigString(R2CoreConfigurations.AqayepardakhtPaymentGate, 2)})
                    'Return JsonConvert.SerializeObject(New With {.Authority = NSSPaymentRequest.Authority, .PaymentURI = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ZarrinPalPaymentGate, 2)})
                    'Return JsonConvert.SerializeObject(New With {.Authority = NSSPaymentRequest.Authority, .PaymentURI = InstanceConfiguration.GetConfigString(R2CoreConfigurations.ShepaPaymentGate, 2)})
                Else
                    Throw New PaymentWebServiceConnectingException(NSSPaymentRequest.PaymentErrors)
                End If
            Catch ex As PaymentWebServiceConnectingException
                Throw ex
            Catch ex As ChargingAmountInvalidException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetDefaultAmounts() As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                  "Select AmountTitle,Amount from R2Primary.dbo.TblDefaultMoneyWalletChargingAmounts Order By Amount for json auto", 32767, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetMoneyWalletChargeRecords(ByVal YourMoneyWalletId As Int64) As String
            Try
                Dim Ds As New DataSet
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                          "Select Top 100  Charges.DateShamsi as ShamsiDate,Charges.TimeCharge as Time,Charges.Mblgh as Amount,SoftwareUsers.UserName as UserName
                           from R2Primary.dbo.TblMoneyWalletCharges AS Charges 
                              Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Charges.UserId=SoftwareUsers.UserId
                           Where Charges.CardId=" & YourMoneyWalletId & " Order by Charges.DateTimeMilladi Desc for json path", 300, Ds, New Boolean).GetRecordsCount = 0 Then
                    Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUserChargeFunction(YourUserId As Int64, YourDate1 As String, YourDate2 As String, YourTime1 As String, YourTime2 As String) As String
            Try
                Dim PublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim myConcat1 As String = YourDate1 + YourTime1
                Dim myConcat2 As String = YourDate2 + YourTime2
                Dim Lst As List(Of R2StandardUserChargeProcessStructure) = New List(Of R2StandardUserChargeProcessStructure)
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                      "Select Top 100 Charges.DateShamsi as ShamsiDate,Charges.TimeCharge as Time,Charges.Mblgh as Amount,MoneyWallets.CardNo 
                       From R2Primary.dbo.TblMoneyWalletCharges as Charges
                          Inner Join R2Primary.dbo.TblRFIDCards as MoneyWallets On Charges.CardId=MoneyWallets.CardId
                       Where (Charges.DateShamsi+Charges.TimeCharge>='" & myConcat1 & "') and (Charges.DateShamsi+Charges.TimeCharge<='" & myConcat2 & "') and UserId=" & YourUserId & "
                       Order by DateTimeMilladi Desc for json path", 0, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                Return PublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalAmountofUserFunction(YourUserId As Int64, YourDate1 As String, YourDate2 As String, YourTime1 As String, YourTime2 As String) As Int64
            Try
                Dim myConcat1 As String = YourDate1 + YourTime1
                Dim myConcat2 As String = YourDate2 + YourTime2
                Dim DS As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                    "Select sum(Charges.Mblgh) as Total 
                     From R2Primary.dbo.TblMoneyWalletCharges as Charges
                        Inner Join R2Primary.dbo.TblRFIDCards as MoneyWallets On Charges.CardId=MoneyWallets.CardId
                     Where (Charges.DateShamsi+Charges.TimeCharge>='" & myConcat1 & "') and (Charges.DateShamsi+Charges.TimeCharge<='" & myConcat2 & "') and UserId= " & YourUserId & "", 0, DS, New Boolean).GetRecordsCount() <> 0 Then
                    If Object.Equals(DBNull.Value, DS.Tables(0).Rows(0).Item("Total")) Then
                        Return 0
                    Else
                        Return Convert.ToInt64(DS.Tables(0).Rows(0).Item("Total"))
                    End If
                Else
                    Return 0
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    'BPTChanged
    Namespace Exceptions
        Public Class ChargingAmountInvalidException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CoreParkingSystemPredefinedMessages.AmountInvalid).MsgContent
                End Get
            End Property
        End Class

        Public Class PaymentWebServiceConnectingException
            Inherits ApplicationException

            Private _Message As String
            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return (New R2CoreMClassPredefinedMessagesManager).GetNSS(R2CorePredefinedMessages.WebServiceConnectingException).MsgContent + vbCrLf + _Message
                End Get
            End Property
        End Class

    End Namespace



End Namespace

