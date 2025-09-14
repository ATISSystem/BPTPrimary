


Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.TransportTariffs.Exceptions
Imports System.Data.SqlClient
Imports System.Reflection

Namespace TransportTariffs


    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTariffStructure

        Public Sub New()
            MyBase.New()
            _AHId = 0
            _AHSGId = 0
            _TargetCityId = Int64.MinValue
            _Tariff = 0
            _DateTimeMilladi = Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _OActive = False
        End Sub

        Public Sub New(ByVal YourAHId As Int64, YourAHSGId As Int64, YourTargetCityId As Int64, YourTariff As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourOActive As Boolean)
            _AHId = YourAHId
            _AHSGId = YourAHSGId
            _TargetCityId = YourTargetCityId
            _Tariff = YourTariff
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _OActive = YourOActive
        End Sub


        Public Property AHId As Int64
        Public Property AHSGId As Int64
        Public Property TargetCityId As Int64
        Public Property Tariff As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property OActive As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager
        Private _DateTimeService As New R2DateTimeService

        Public Sub TransportTariffRegistering(YourTargetCityId As Int64, YourSourceCityId As Int64, YourAHId As Int64, YourAHSGId As Int64, YourTariff As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set OActive=0
                                      where SourceCityId=" & YourSourceCityId & " and TargetCityId=" & YourTargetCityId & " and AHId=" & YourAHId & " and AHSGId=" & YourAHSGId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs(AHId,AHSGId,SourceCityId,TargetCityId,Tariff,DateTimeMilladi,DateShamsi,Time,OActive)
                                      values(" & YourAHId & "," & YourAHSGId & "," & YourSourceCityId & "," & YourTargetCityId & "," & YourTariff & ",'" & _DateTimeService.GetCurrentDateTimeMilladi & "','" & _DateTimeService.GetCurrentShamsiDate & "','" & _DateTimeService.GetCurrentTime & "',1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTransportTariff(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardTransportTariffStructure
            Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Where (AHId=" & YourNSS.AHId & ") and (AHSGId=" & YourNSS.AHSGId & ") and (TargetCityId=" & YourNSS.nCityCode & ") and (SourceCityId=" & YourNSS.nBarSource & ") and OActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportPriceTariffNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTariffStructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TargetCityId"), DS.Tables(0).Rows(0).Item("Tariff"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("OActive"))
            Catch exx As TransportPriceTariffNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUltimateTransportTariff(YourAHSGId As Int64, YournTonaj As Double, YourTariff As Int64) As Double
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Parameters.Add("@AHSGId", SqlDbType.BigInt) : CmdSql.Parameters("@AHSGId").Value = YourAHSGId
                CmdSql.Parameters.Add("@Tariff", SqlDbType.BigInt) : CmdSql.Parameters("@Tariff").Value = YourTariff
                CmdSql.Parameters.Add("@Tonaj", SqlDbType.Float) : CmdSql.Parameters("@Tonaj").Value = YournTonaj
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.GetUltimateTransportTariff"
                Dim Tariff = CmdSql.ExecuteScalar
                CmdSql.Connection.Close()
                Return Tariff
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassTransportTariffsManagement
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Shared Function GetNSSTransportTariff(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardTransportTariffStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Where (AHId=" & YourNSS.AHId & ") and (AHSGId=" & YourNSS.AHSGId & ") and (TargetCityId=" & YourNSS.nCityCode & ") and OActive=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportPriceTariffNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTariffStructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TargetCityId"), DS.Tables(0).Rows(0).Item("Tariff"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("OActive"))
            Catch exx As TransportPriceTariffNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    'BPTChanged
    Namespace Exceptions
        Public Class TransportPriceTariffNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نرخ حمل یا تعرفه برای مسیر مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportTariff
        Public Property SourceCityId As Int64
        Public Property SourceCityTitle As String
        Public Property TargetCityId As Int64
        Public Property TargetCityTitle As String
        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property GoodId As Int64
        Public Property GoodTitle As String
        Public Property Tariff As Int64
        Public Property BaseTonnag As Double
        Public Property CalculationReference As String
        Public Property Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportTariffsManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetTariffs(YourLoaderTypeId As Int64, YourGoodId As Int64, YourSourceCityId As Int64, YourTargetCityId As Int64, YourImmediately As Boolean) As String
            Try
                Dim Ds As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,
                            Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Tariffs.Tariff as Tariff,Tariffs.BaseTonnag,Tariffs. CalculationReference,Tariffs.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs as Tariffs
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On Tariffs.LoaderTypeId=LoaderTypes.LoaderTypeId 
                        Inner Join DBTransport.dbo.tbProducts as Goods On Tariffs.GoodId=Goods.strGoodCode 
                        Inner Join DBTransport.dbo.tbCity as SourceCities On Tariffs.SourceCityId=SourceCities.nCityCode 
                        Inner join DBTransport.dbo.tbCity as TargetCities On Tariffs.TargetCityId=TargetCities.nCityCode 
                     Where ((" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and 3=3) or 
                           (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & "))  and
	                       Tariffs.Active=1 and Tariffs.Deleted=0
                     for json path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "
                     Select SourceCities.nCityCode as SourceCityId,SourceCities.StrCityName as SourceCityTitle,TargetCities.nCityCode as TargetCityId,TargetCities.StrCityName as TargetCityTitle,LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,
                            Goods.strGoodCode as GoodId,Goods.strGoodName as GoodTitle,Tariffs.Tariff as Tariff,Tariffs.BaseTonnag,Tariffs. CalculationReference,Tariffs.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs as Tariffs
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes On Tariffs.LoaderTypeId=LoaderTypes.LoaderTypeId 
                        Inner Join DBTransport.dbo.tbProducts as Goods On Tariffs.GoodId=Goods.strGoodCode 
                        Inner Join DBTransport.dbo.tbCity as SourceCities On Tariffs.SourceCityId=SourceCities.nCityCode 
                        Inner join DBTransport.dbo.tbCity as TargetCities On Tariffs.TargetCityId=TargetCities.nCityCode 
                     Where ((" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and 3=3) or 
                           (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "=-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "=-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "=-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.TargetCityId=" & YourTargetCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "=-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & ") or 
	                       (" & YourLoaderTypeId & "<>-1 and " & YourGoodId & "<>-1 and " & YourSourceCityId & "<>-1 and " & YourTargetCityId & "<>-1 and Tariffs.LoaderTypeId=" & YourLoaderTypeId & " and Tariffs.GoodId=" & YourGoodId & " and Tariffs.SourceCityId=" & YourSourceCityId & " and Tariffs.TargetCityId=" & YourTargetCityId & "))  and
	                       Tariffs.Active=1 and Tariffs.Deleted=0
                     for json path", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub TariffsRegistering(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs(SourceCityId,TargetCityId,LoaderTypeId,GoodId,Tariff,BaseTonnag,CalculationReference,DateShamsi,ViewFlag,Active,Deleted)
                                          Values(" & YourTariffs(Loopx).SourceCityId & "," & YourTariffs(Loopx).TargetCityId & "," & YourTariffs(Loopx).LoaderTypeId & "," & YourTariffs(Loopx).GoodId & "," & YourTariffs(Loopx).Tariff & "," & YourTariffs(Loopx).BaseTonnag & ",'" & YourTariffs(Loopx).CalculationReference & "','" & _DateTimeService.GetCurrentShamsiDate & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TariffsRegistering(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff), YourAddPercentage As Double)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'غیرفعال کردن تعرفه های فعال
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
                'فعال کردن تعرفه های جدید
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs(SourceCityId,TargetCityId,LoaderTypeId,GoodId,Tariff,BaseTonnag,CalculationReference,DateShamsi,ViewFlag,Active,Deleted)
                                          Values(" & YourTariffs(Loopx).SourceCityId & "," & YourTariffs(Loopx).TargetCityId & "," & YourTariffs(Loopx).LoaderTypeId & "," & YourTariffs(Loopx).GoodId & "," & YourTariffs(Loopx).Tariff + (YourAddPercentage * YourTariffs(Loopx).Tariff / 100) & "," & YourTariffs(Loopx).BaseTonnag & ",'" & YourTariffs(Loopx).CalculationReference & "','" & _DateTimeService.GetCurrentShamsiDate & "',1,1,0)"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TariffsDeactivate(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'غیرفعال کردن تعرفه های فعال
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TariffsDeleting(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'حذف کردن تعرفه ها
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Deleted=1,Active=0
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub TariffsEditing(YourTariffs As List(Of R2CoreTransportationAndLoadNotificationTransportTariff))
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                'ویرایش کردن تعرفه ها
                For Loopx As Int64 = 0 To YourTariffs.Count - 1
                    CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs Set Tariff=" & YourTariffs(Loopx).Tariff & ",BaseTonnag=" & YourTariffs(Loopx).BaseTonnag & "
                                          Where SourceCityId=" & YourTariffs(Loopx).SourceCityId & " and TargetCityId=" & YourTariffs(Loopx).TargetCityId & " and LoaderTypeId=" & YourTariffs(Loopx).LoaderTypeId & " and GoodId=" & YourTariffs(Loopx).GoodId & " and Active=1"
                    CmdSql.ExecuteNonQuery()
                Next
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetTransportTariff(YourSourceCityId As Int64, YourTargetCityId As Int64, YourLoaderTypeId As Int64, YourGoodId As Int64) As Int64
            Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                     "Select Top 1 TransportPriceTariffs.Tariff from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTariffs as TransportPriceTariffs
                      Where TransportPriceTariffs.SourceCityId=" & YourSourceCityId & " and TransportPriceTariffs.TargetCityId=" & YourTargetCityId & " and LoaderTypeId=" & YourLoaderTypeId & " and GoodId=" & YourGoodId & " and TransportPriceTariffs.Active=1", 32767, DS, New Boolean).GetRecordsCount() = 0 Then Throw New TransportPriceTariffNotFoundException
                Return DS.Tables(0).Rows(0).Item("Tariff")
            Catch exx As TransportPriceTariffNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetUltimateTransportTariff(YourAnnouncementSGId As Int64, YournTonaj As Double, YourTariff As Int64) As Double
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Parameters.Add("@AnnouncementSGId", SqlDbType.BigInt) : CmdSql.Parameters("@AnnouncementSGId").Value = YourAnnouncementSGId
                CmdSql.Parameters.Add("@Tariff", SqlDbType.BigInt) : CmdSql.Parameters("@Tariff").Value = YourTariff
                CmdSql.Parameters.Add("@Tonaj", SqlDbType.Float) : CmdSql.Parameters("@Tonaj").Value = YournTonaj
                CmdSql.CommandType = CommandType.StoredProcedure
                CmdSql.CommandText = "R2PrimaryTransportationAndLoadNotification.dbo.GetUltimateTransportTariff"
                Dim Tariff = CmdSql.ExecuteScalar
                CmdSql.Connection.Close()
                Return Tariff
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class



End Namespace
