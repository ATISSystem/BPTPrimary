
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Reflection
Imports R2Core.BaseStandardClass
Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.SoftwareUserManagement
Imports R2CoreTransportationAndLoadNotification.Turns.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns
Imports R2CoreTransportationAndLoadNotification.EntityRelations
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreTransportationAndLoadNotification.Turns
Imports R2Core.EntityRelationManagement
Imports R2Core.ExceptionManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions
Imports R2CoreTransportationAndLoadNotification.TruckDrivers.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports R2CoreTransportationAndLoadNotification.Turns.SequentialTurns.Exceptions
Imports R2Core.DateTimeProvider


Namespace TurnRegisterRequest

    Public MustInherit Class TurnRegisterRequestTypes
        Public Shared ReadOnly Property None = 0
        Public Shared ReadOnly Property RealTime = 1
        Public Shared ReadOnly Property Emergency = 2
        Public Shared ReadOnly Property Reserve = 3
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestTypeStructure

        Public Sub New()
            _TRRTypeId = Int64.MinValue
            _TRRTypeName = String.Empty
            _TRRTypeTitle = String.Empty
            _TRRTypeColor = Color.Black
            _TurnExpirationHours = Int16.MinValue
            _Acitve = Boolean.FalseString
            _ViewFlag = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourTRRTypeId As Int64, ByVal YourTRRTypeName As String, YourTRRTypeTitle As String, YourTRRTypeColor As Color, YourTurnExpirationHours As Int16, YourAcitve As Boolean, YourViewFlag As Boolean, YourDeleted As Boolean)
            _TRRTypeId = YourTRRTypeId
            _TRRTypeName = YourTRRTypeName
            _TRRTypeTitle = YourTRRTypeTitle
            _TRRTypeColor = YourTRRTypeColor
            _TurnExpirationHours = YourTurnExpirationHours
            _Acitve = YourAcitve
            _ViewFlag = YourViewFlag
            _Deleted = YourDeleted
        End Sub

        Public Property TRRTypeId As Int64
        Public Property TRRTypeName As String
        Public Property TRRTypeTitle As String
        Public Property TRRTypeColor As Color
        Public Property TurnExpirationHours As Int16
        Public Property Acitve As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
        Public Sub New()
            _TRRId = Int64.MinValue
            _TRRTypeId = Int64.MinValue
            _TruckId = Int64.MinValue
            _Description = String.Empty
            _UserId = Int64.MinValue
            _ComputerId = Int64.MinValue
            _DateTimeMilladi = Now
            DateShamsi = String.Empty
        End Sub

        Public Sub New(ByVal YourTRRId As Int64, ByVal YourTRRTypeId As Int64, YourTruckId As Int64, YourDescription As String, YourUserId As Int64, YourComputerId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String)
            _TRRId = YourTRRId
            _TRRTypeId = YourTRRTypeId
            _TruckId = YourTruckId
            _Description = YourDescription
            _UserId = YourUserId
            _ComputerId = YourComputerId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
        End Sub

        Public Property TRRId As Int64
        Public Property TRRTypeId As Int64
        Public Property TruckId As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property ComputerId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManager
        Private _DateTime As New R2DateTime
        Private _R2PrimaryFSWS = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService

        Public Function GetNSSTurnRegisterRequestType(YourTurnRegisterRequestTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestTypeStructure
            Try
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequestTypes as TRRTypes Where TRRTypes.TRRTypeId=" & YourTurnRegisterRequestTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestTypeNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestTypeStructure
                NSS.TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId")
                NSS.TRRTypeName = Ds.Tables(0).Rows(0).Item("TRRTypeName").trim
                NSS.TRRTypeTitle = Ds.Tables(0).Rows(0).Item("TRRTypeTitle").trim
                NSS.TRRTypeColor = Color.FromName(Ds.Tables(0).Rows(0).Item("TRRTypeColor").trim)
                NSS.TurnExpirationHours = Ds.Tables(0).Rows(0).Item("TurnExpirationHours")
                NSS.ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")
                NSS.Acitve = Ds.Tables(0).Rows(0).Item("Active")
                NSS.Deleted = Ds.Tables(0).Rows(0).Item("Deleted")
                Return NSS
            Catch ex As TurnRegisterRequestTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function TurnRegisterRequestRegistering(YourNSSTRR As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure, YourAttachement As R2CoreImage, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                Dim InstanceComputers = New R2CoreMClassComputersManager
                'تراکنش ثبت درخواست صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select Top 1 TRRId From R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests With (tablockx) Order By TRRId Desc"
                CmdSql.ExecuteNonQuery()
                Dim TRRIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests(TRRId,TRRTypeId,TruckId,Description,UserId,ComputerId,DateTimeMilladi,DateShamsi) Values(" & TRRIdNew & "," & YourNSSTRR.TRRTypeId & "," & YourNSSTRR.TruckId & ",'" & YourNSSTRR.Description & "'," & YourUserNSS.UserId & "," & InstanceComputers.GetNSSCurrentComputer.MId & ",'" & _DateTime.GetCurrentDateTimeMilladi() & "','" & _DateTime.GetCurrentShamsiDate() & "')"
                CmdSql.ExecuteNonQuery()
                If YourAttachement IsNot Nothing Then SaveTurnRegisterRequestAttachement(YourAttachement, TRRIdNew, YourUserNSS)
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'ارسال کد درخواست
                Return TRRIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Sub SaveTurnRegisterRequestAttachement(YourAttachement As R2CoreImage, YourTRRId As Int64, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim FileInf = New R2CoreFile(YourTRRId.ToString() + InstanceConfiguration.GetConfigString(R2CoreConfigurations.JPGBitmap, 2))
                _R2PrimaryFSWS.WebMethodSaveFile(FileShareRawGroupsManagement.R2CoreTransportationAndLoadNotificationRawGroups.TurnRegisterRequestAttachements, FileInf.FileName, YourAttachement.GetImageByte(), _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTurnRegisterRequest(YourTurnRegisterRequestId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
            Try
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TRRs Where TRRs.TRRId=" & YourTurnRegisterRequestId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
                NSS.TRRId = Ds.Tables(0).Rows(0).Item("TRRId")
                NSS.TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId")
                NSS.TruckId = Ds.Tables(0).Rows(0).Item("TruckId")
                NSS.UserId = Ds.Tables(0).Rows(0).Item("UserId")
                NSS.Description = Ds.Tables(0).Rows(0).Item("Description").trim
                NSS.ComputerId = Ds.Tables(0).Rows(0).Item("ComputerId")
                NSS.DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi")
                NSS.DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi")
                Return NSS
            Catch ex As TurnRegisterRequestNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSTurnRegisteringRequestWithReservedDateTime(YourDateTime As R2CoreDateAndTime) As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
            Try
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationInstanceSequentialTurnsManager
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                           "Select Top 1 Turns.strEnterDate,Turns.strEnterTime,Turns.nEnterExitId,TurnRegisterRequests.* from dbtransport.dbo.tbEnterExit as Turns
                             Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On TurnRegisterRequests.TRRId=EntityRelations.E2 
                            Where Turns.strEnterDate>='" & YourDateTime.ShamsiDate & "' and Turns.strEnterTime>='" & YourDateTime.Time & "' and EntityRelations.ERTypeId=" & R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest & " 
                                  and EntityRelations.RelationActive=1 and TurnRegisterRequests.TRRTypeId=" & TurnRegisterRequestTypes.Reserve & " and Substring(Turns.OtaghdarTurnNumber,1,1)='" & InstanceSequentialTurns.GetNSSSequentialTurn(Turns.SequentialTurns.SequentialTurns.None).SequentialTurnKeyWord & "'
                            Order By Turns.nEnterExitId Asc", 0, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TurnRegisterRequestNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure(DS.Tables(0).Rows(0).Item("TRRId"), DS.Tables(0).Rows(0).Item("TRRTypeId"), DS.Tables(0).Rows(0).Item("TruckId"), DS.Tables(0).Rows(0).Item("Description").trim, DS.Tables(0).Rows(0).Item("UserId"), DS.Tables(0).Rows(0).Item("ComputerId"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi").trim)
            Catch ex As TurnRegisterRequestNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTurnRegisterRequestManagement
        Private Shared _DateTime As New R2DateTime
        Private Shared _R2PrimaryFSWS = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Shared Function GetNSSTurnRegisterRequestType(YourTurnRegisterRequestTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestTypeStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequestTypes as TRRTypes Where TRRTypes.TRRTypeId=" & YourTurnRegisterRequestTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestTypeNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestTypeStructure
                NSS.TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId")
                NSS.TRRTypeName = Ds.Tables(0).Rows(0).Item("TRRTypeName").trim
                NSS.TRRTypeTitle = Ds.Tables(0).Rows(0).Item("TRRTypeTitle").trim
                NSS.TRRTypeColor = Color.FromName(Ds.Tables(0).Rows(0).Item("TRRTypeColor").trim)
                NSS.TurnExpirationHours = Ds.Tables(0).Rows(0).Item("TurnExpirationHours")
                NSS.ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")
                NSS.Acitve = Ds.Tables(0).Rows(0).Item("Active")
                NSS.Deleted = Ds.Tables(0).Rows(0).Item("Deleted")
                Return NSS
            Catch ex As TurnRegisterRequestTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSTurnRegisterRequest(YourTurnRegisterRequestId As Int64) As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TRRs Where TRRs.TRRId=" & YourTurnRegisterRequestId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestNotFoundException
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
                NSS.TRRId = Ds.Tables(0).Rows(0).Item("TRRId")
                NSS.TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId")
                NSS.TruckId = Ds.Tables(0).Rows(0).Item("TruckId")
                NSS.UserId = Ds.Tables(0).Rows(0).Item("UserId")
                NSS.Description = Ds.Tables(0).Rows(0).Item("Description").trim
                NSS.ComputerId = Ds.Tables(0).Rows(0).Item("ComputerId")
                NSS.DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi")
                NSS.DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi")
                Return NSS
            Catch ex As TurnRegisterRequestNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Shared Sub SaveTurnRegisterRequestAttachement(YourAttachement As R2CoreImage, YourTRRId As Int64, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Try
                Dim FileInf = New R2CoreFile(YourTRRId.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2))
                _R2PrimaryFSWS.WebMethodSaveFile(FileShareRawGroupsManagement.R2CoreTransportationAndLoadNotificationRawGroups.TurnRegisterRequestAttachements, FileInf.FileName, YourAttachement.GetImageByte(), _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function TurnRegisterRequestRegistering(YourNSSTRR As R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure, YourAttachement As R2CoreImage, YourUserNSS As R2CoreStandardSoftwareUserStructure) As Int64
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                'تراکنش ثبت درخواست صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select Top 1 TRRId From R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests With (tablockx) Order By TRRId Desc"
                CmdSql.ExecuteNonQuery()
                Dim TRRIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests(TRRId,TRRTypeId,TruckId,Description,UserId,ComputerId,DateTimeMilladi,DateShamsi) Values(" & TRRIdNew & "," & YourNSSTRR.TRRTypeId & "," & YourNSSTRR.TruckId & ",'" & YourNSSTRR.Description & "'," & YourUserNSS.UserId & "," & R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId & ",'" & _DateTime.GetCurrentDateTimeMilladi() & "','" & _DateTime.GetCurrentShamsiDate() & "')"
                CmdSql.ExecuteNonQuery()
                If YourAttachement IsNot Nothing Then SaveTurnRegisterRequestAttachement(YourAttachement, TRRIdNew, YourUserNSS)
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'ارسال کد درخواست
                Return TRRIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnRegisterRequestType
        Public Property TRRTypeId As Int64
        Public Property TRRTypeName As String
        Public Property TRRTypeTitle As String
        Public Property TRRTypeColor As Color
        Public Property TurnExpirationHours As Int16
        Public Property Acitve As Boolean
        Public Property ViewFlag As Boolean
        Public Property Deleted As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnRegisterRequest
        Public Property TRRId As Int64
        Public Property TRRTypeId As Int64
        Public Property TruckId As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property RequesterId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager
        Private _R2DateTimeService As IR2DateTimeService
        Private _R2PrimaryFSWS = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService

        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Function GetTurnRegisterRequest(YourTurnRegisterRequestId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnRegisterRequest
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TRRs Where TRRs.TRRId=" & YourTurnRegisterRequestId & "")
                    Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnRegisterRequestNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TRRs Where TRRs.TRRId=" & YourTurnRegisterRequestId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestNotFoundException
                End If
                Dim NSS = New R2CoreTransportationAndLoadNotificationStandardTurnRegisterRequestStructure
                Return New R2CoreTransportationAndLoadNotificationTurnRegisterRequest With {.TRRId = Ds.Tables(0).Rows(0).Item("TRRId"), .TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId"), .TruckId = Ds.Tables(0).Rows(0).Item("TruckId"), .UserId = Ds.Tables(0).Rows(0).Item("UserId"), .Description = Ds.Tables(0).Rows(0).Item("Description").trim, .RequesterId = Ds.Tables(0).Rows(0).Item("RequesterId"), .DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi"), .DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi"), .Time = Ds.Tables(0).Rows(0).Item("Time")}
            Catch ex As TurnRegisterRequestNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnRegisteringRequestWithReservedDateTime(YourDateTime As R2CoreDateAndTime, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationTurnRegisterRequest
            Try
                Dim InstanceSequentialTurns = New R2CoreTransportationAndLoadNotificationSequentialTurnsManager
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim Ds As New DataSet

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                           "Select Top 1 Turns.strEnterDate,Turns.strEnterTime,Turns.nEnterExitId,TurnRegisterRequests.* from dbtransport.dbo.tbEnterExit as Turns
                             Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On TurnRegisterRequests.TRRId=EntityRelations.E2 
                            Where Turns.strEnterDate>='" & YourDateTime.ShamsiDate & "' and Turns.strEnterTime>='" & YourDateTime.Time & "' and EntityRelations.ERTypeId=" & R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest & " 
                                  and EntityRelations.RelationActive=1 and TurnRegisterRequests.TRRTypeId=" & TurnRegisterRequestTypes.Reserve & " and Substring(Turns.OtaghdarTurnNumber,1,1)='" & InstanceSequentialTurns.GetSequentialTurn(Turns.SequentialTurns.SequentialTurns.None, False).SeqTurnKeyWord & "'
                            Order By Turns.nEnterExitId Asc")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New TurnRegisterRequestNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                               "Select Top 1 Turns.strEnterDate,Turns.strEnterTime,Turns.nEnterExitId,TurnRegisterRequests.* from dbtransport.dbo.tbEnterExit as Turns
                             Inner Join R2Primary.dbo.TblEntityRelations as EntityRelations On Turns.nEnterExitId=EntityRelations.E1 
                             Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests as TurnRegisterRequests On TurnRegisterRequests.TRRId=EntityRelations.E2 
                            Where Turns.strEnterDate>='" & YourDateTime.ShamsiDate & "' and Turns.strEnterTime>='" & YourDateTime.Time & "' and EntityRelations.ERTypeId=" & R2CoreTransportationAndLoadNotificationEntityRelationTypes.Turn_TurnRegisterRequest & " 
                                  and EntityRelations.RelationActive=1 and TurnRegisterRequests.TRRTypeId=" & TurnRegisterRequestTypes.Reserve & " and Substring(Turns.OtaghdarTurnNumber,1,1)='" & InstanceSequentialTurns.GetSequentialTurn(Turns.SequentialTurns.SequentialTurns.None, False).SeqTurnKeyWord & "'
                            Order By Turns.nEnterExitId Asc", 3600, Ds, New Boolean).GetRecordsCount = 0 Then
                        Throw New TurnRegisterRequestNotFoundException
                    End If
                End If
                Return New R2CoreTransportationAndLoadNotificationTurnRegisterRequest With {
                        .TRRId = Ds.Tables(0).Rows(0).Item("TRRId"),
                        .TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId"),
                        .TruckId = Ds.Tables(0).Rows(0).Item("TruckId"),
                        .Description = Ds.Tables(0).Rows(0).Item("Description").trim,
                        .UserId = Ds.Tables(0).Rows(0).Item("UserId"),
                        .RequesterId = Ds.Tables(0).Rows(0).Item("RequesterId"),
                        .DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi"),
                        .DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi").trim,
                        .Time = Ds.Tables(0).Rows(0).Item("Time").trim}
            Catch ex As TurnRegisterRequestNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Sub SaveTurnRegisterRequestAttachement(YourAttachement As R2CoreImage, YourTRRId As Int64)
            Try
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_R2DateTimeService, Nothing)
                Dim FileInf = New R2CoreFile(YourTRRId.ToString() + InstanceConfiguration.GetConfigString(R2CoreConfigurations.JPGBitmap, 2))
                _R2PrimaryFSWS.WebMethodSaveFile(FileShareRawGroupsManagement.R2CoreTransportationAndLoadNotificationRawGroups.TurnRegisterRequestAttachements, FileInf.FileName, YourAttachement.GetImageByte(), _R2PrimaryFSWS.WebMethodLogin(InstanceSoftwareUsers.GetSystemUser.UserShenaseh, InstanceSoftwareUsers.GetSystemUser.UserPassword))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function TurnRegisterRequestRegistering(YourTRR As R2CoreTransportationAndLoadNotificationTurnRegisterRequest, YourAttachement As R2CoreImage, YourSoftwaeUserId As Int64, YourRequesterId As Int64) As Int64
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection()
            Try
                'تراکنش ثبت درخواست صدور نوبت
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Select Top 1 TRRId From R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests With (tablockx) Order By TRRId Desc"
                CmdSql.ExecuteNonQuery()
                Dim TRRIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequests(TRRId,TRRTypeId,TruckId,Description,UserId,RequesterId,DateTimeMilladi,DateShamsi,Time) 
                                      Values(" & TRRIdNew & "," & YourTRR.TRRTypeId & "," & YourTRR.TruckId & ",'" & YourTRR.Description & "'," & YourTRR.UserId & "," & YourRequesterId & ",'" & _R2DateTimeService.GetCurrentDateTimeMilladi() & "','" & _R2DateTimeService.GetCurrentShamsiDate() & "','" & _R2DateTimeService.GetCurrentTime & "')"
                CmdSql.ExecuteNonQuery()
                If YourAttachement IsNot Nothing Then SaveTurnRegisterRequestAttachement(YourAttachement, TRRIdNew)
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                'ارسال کد درخواست
                Return TRRIdNew
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTurnRegisterRequestType(YourTurnRegisterRequestTypeId As Int64) As R2CoreTransportationAndLoadNotificationTurnRegisterRequestType
            Try
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2PrimaryTransportationAndLoadNotification.dbo.TblTurnRegisterRequestTypes as TRRTypes Where TRRTypes.TRRTypeId=" & YourTurnRegisterRequestTypeId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New TurnRegisterRequestTypeNotFoundException
                Return New R2CoreTransportationAndLoadNotificationTurnRegisterRequestType With {.TRRTypeId = Ds.Tables(0).Rows(0).Item("TRRTypeId"), .TRRTypeName = Ds.Tables(0).Rows(0).Item("TRRTypeName").trim, .TRRTypeColor = Color.FromName(Ds.Tables(0).Rows(0).Item("TRRTypeColor").trim), .TRRTypeTitle = Ds.Tables(0).Rows(0).Item("TRRTypeTitle").trim, .TurnExpirationHours = Ds.Tables(0).Rows(0).Item("TurnExpirationHours"), .ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag"), .Acitve = Ds.Tables(0).Rows(0).Item("Active"), .Deleted = Ds.Tables(0).Rows(0).Item("Deleted")}
            Catch ex As TurnRegisterRequestTypeNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

End Namespace
