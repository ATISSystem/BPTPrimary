


Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider

Namespace Logging

    'BPTChanged
    Public MustInherit Class R2CoreParkingSystemLogTypes
        Inherits R2Core.LoggingManagement.R2CoreLogTypes

        Public Shared ReadOnly Property EditingTrafficCardType As Int64 = 42
        Public Shared ReadOnly Property RegisteringTrafficCardType As Int64 = 43
        Public Shared ReadOnly Property EditingTrafficCard As Int64 = 44
        Public Shared ReadOnly Property RegisteringTrafficCard As Int64 = 45
        Public Shared ReadOnly Property RegisteringTrafficCost As Int64 = 46
        Public Shared ReadOnly Property UnActivateTrafficGate As Int64 = 47
        Public Shared ReadOnly Property ActivateTrafficGate As Int64 = 48
        Public Shared ReadOnly Property RegisteringTrafficGate As Int64 = 49
        Public Shared ReadOnly Property ChangeActivateStatusOfCity As Int64 = 76
        Public Shared ReadOnly Property ChangeActivateStatusOfProvince As Int64 = 77



    End Class

    Public Class R2CoreParkingSystemLogManager
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(New R2DateTimeService)

        'Public Function GetEntryExitLogsWithTrafficCard(YourTrafficCard As R2CoreParkingSystemStandardTrafficCardStructure) As List(Of R2CoreStandardLoggingExtendedStructure)
        '    Try
        '        Dim Ds As New DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
        '             "Select Top 50 Logs.*,SoftwareUsers.UserName,LoggingTypes.LogColor from R2PrimaryLogging.dbo.TblLogging as Logs
        '               Inner Join R2Primary.dbo.TblSoftwareUsers as SoftwareUsers On Logs.Userid=SoftwareUsers.UserId 
        '               Inner Join R2PrimaryLogging.dbo.TblLoggingTypes as LoggingTypes On Logs.LogType=LoggingTypes.LogId 
        '              Where Optional1 Like '%" & YourTrafficCard.CardNo & "%' order by DateTimeMilladi Desc", 0, Ds, New Boolean)
        '        Dim Lst As New List(Of R2CoreStandardLoggingExtendedStructure)
        '        For Loopx As Int64 = 1 To Ds.Tables(0).Rows.Count - 1
        '            Dim NSSLog = New R2CoreStandardLoggingExtendedStructure(New R2CoreStandardLoggingStructure(Ds.Tables(0).Rows(Loopx).Item("logid"), Ds.Tables(0).Rows(Loopx).Item("LogType"), Ds.Tables(0).Rows(Loopx).Item("sharh").trim, Ds.Tables(0).Rows(Loopx).Item("optional1").trim, Ds.Tables(0).Rows(Loopx).Item("optional2").trim, Ds.Tables(0).Rows(Loopx).Item("optional3").trim, Ds.Tables(0).Rows(Loopx).Item("optional4").trim, Ds.Tables(0).Rows(Loopx).Item("optional5").trim, Ds.Tables(0).Rows(Loopx).Item("userid"), Ds.Tables(0).Rows(Loopx).Item("datetimemilladi"), Ds.Tables(0).Rows(Loopx).Item("dateshamsi").trim), Ds.Tables(0).Rows(Loopx).Item("UserName").trim, Color.FromName(Ds.Tables(0).Rows(Loopx).Item("LogColor").trim))
        '            Lst.Add(NSSLog)
        '        Next
        '        Return Lst
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class


End Namespace


