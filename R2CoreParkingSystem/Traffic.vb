

Imports System.Drawing
Imports System.Reflection
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.PublicProc

Namespace Traffic

    'BPTChanged
    Public Class R2CoreParkingSystemTraffic
        Public EntryExit As String
        Public EntryExitColor As Color
        Public TrafficPicture As Byte()
        Public MoneyWallet As R2CoreMoneyWallet
        Public TrafficStoppageCost As Int64
        Public Payable As Int64
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficManager
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Const Entry As String = "Entry"
        Public Const Exittance As String = "Exit"

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function RegisteringTraffic(YourTrafficGateId As Int64, YourTrafficCardNo As String, YourTrafficPicture As Byte()) As R2CoreParkingSystemTraffic
            Dim x As New R2CoreMoneyWalletManager(_DateTimeService)


            Return New R2CoreParkingSystemTraffic With {.EntryExit = Entry, .EntryExitColor = Color.Red, .MoneyWallet = x.GetMoneyWallet(90, True), .Payable = 10, .TrafficPicture = Nothing, .TrafficStoppageCost = 1010}

        End Function

        Public Function GetTrafficRecords(YourTrafficCardId As Int64, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlClient.SqlCommand(
                   "Select EntryExitId,EntryShamsiDate,EntryTime,EntryTrafficCards.CardNo as EntryTrafficCardNo,EntrySoftwareUsers.UserName as EntrySoftwareUser,EntryCost,EntryTrafficGates.GateTitle as EntryGateTitle ,
                           ExitShamsiDate,ExitTime,ExitTrafficCards.CardNo as ExitTrafficCardNo,ExitSoftwareUsers.UserName as ExitSoftwareUser,ExitCost,ExitTrafficGates.GateTitle as ExitGateTitle,FlagA  
                    from R2PrimaryParkingSystem.dbo.TblEntryExit as EntryExit
                        Inner Join R2Primary.dbo.TblRFIDCards as EntryTrafficCards On EntryExit.EntryTrafficCardId =EntryTrafficCards.CardId 
                        Inner Join R2Primary.dbo.TblRFIDCards as ExitTrafficCards On EntryExit.ExitTrafficCardId=ExitTrafficCards.CardId 
                        Inner Join R2Primary.dbo.TblSoftwareUsers as EntrySoftwareUsers On EntryExit.EntryUserId=EntrySoftwareUsers.UserId 
                        Inner Join R2Primary.dbo.TblSoftwareUsers as ExitSoftwareUsers On EntryExit.ExitUserId=ExitSoftwareUsers.UserId 
                        Inner Join R2PrimaryParkingSystem.dbo.TblTrafficGates as EntryTrafficGates On EntryExit.EntryGateId=EntryTrafficGates.GateId
                        Inner Join R2PrimaryParkingSystem.dbo.TblTrafficGates as ExitTrafficGates On EntryExit.ExitGateId=ExitTrafficGates.GateId
                    Where EntryTrafficCardId=" & YourTrafficCardId & " Order By EntryDateTimeMilladi for JSON PATH")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select EntryExitId,EntryShamsiDate,EntryTime,EntryTrafficCards.CardNo as EntryTrafficCardNo,EntrySoftwareUsers.UserName as EntrySoftwareUser,EntryCost,EntryTrafficGates.GateTitle as EntryGateTitle ,
                           ExitShamsiDate,ExitTime,ExitTrafficCards.CardNo as ExitTrafficCardNo,ExitSoftwareUsers.UserName as ExitSoftwareUser,ExitCost,ExitTrafficGates.GateTitle as ExitGateTitle,FlagA  
                    from R2PrimaryParkingSystem.dbo.TblEntryExit as EntryExit
                        Inner Join R2Primary.dbo.TblRFIDCards as EntryTrafficCards On EntryExit.EntryTrafficCardId =EntryTrafficCards.CardId 
                        Inner Join R2Primary.dbo.TblRFIDCards as ExitTrafficCards On EntryExit.ExitTrafficCardId=ExitTrafficCards.CardId 
                        Inner Join R2Primary.dbo.TblSoftwareUsers as EntrySoftwareUsers On EntryExit.EntryUserId=EntrySoftwareUsers.UserId 
                        Inner Join R2Primary.dbo.TblSoftwareUsers as ExitSoftwareUsers On EntryExit.ExitUserId=ExitSoftwareUsers.UserId 
                        Inner Join R2PrimaryParkingSystem.dbo.TblTrafficGates as EntryTrafficGates On EntryExit.EntryGateId=EntryTrafficGates.GateId
                        Inner Join R2PrimaryParkingSystem.dbo.TblTrafficGates as ExitTrafficGates On EntryExit.ExitGateId=ExitTrafficGates.GateId
                    Where EntryTrafficCardId=" & YourTrafficCardId & " Order By EntryDateTimeMilladi for JSON PATH", 32727, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace