


Imports System.ComponentModel.Design
Imports System.Reflection
Imports System.Windows.Forms
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports TWSClassLibrary.LoggingManagement
Imports TWSClassLibrary.NobatsManagement

Namespace TWSManagement

    Public Class TruckTraceModel
        Public Property TruckId As Int64
        Public Property ProcessTitle As String
        Public Property TravelTime As Short
        Public Property IssueDate As String
        Public Property IssueTime As String
        Public Property RealDate As String
        Public Property RealTime As String
        Public Property TraceWriter As String
        Public Property TerminalName As String
    End Class

    Public Enum TruckTrace
        None = 0
        NoPelakSerialFound = 1
        NoRecordFound = 2
        OK = 3
    End Enum


    Public Class PayanehClassLibraryTWSManager

        Private _TWS As TerminalsWebService.TerminalsWebService = New TerminalsWebService.TerminalsWebService()
        Private _R2DateTimeService As IR2DateTimeService

        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Private Function AuthenticationId() As String
            Try
                Return "Biinfo878"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTruckTrace(YourPelak As String, YourSerial As String) As List(Of TruckTraceModel)
            Try
                Dim InfDS As New DataSet
                Dim TruckTrace As TruckTrace = _TWS.WebMethodGetTruckTrace(AuthenticationId, YourPelak, YourSerial, 50, InfDS)
                If TruckTrace = TruckTrace.NoPelakSerialFound Then
                    Throw New AnyNotFoundException
                ElseIf TruckTrace = TruckTrace.NoRecordFound Then
                    Throw New AnyNotFoundException
                ElseIf TruckTrace = TruckTrace.OK Then
                    Dim Lst As List(Of TruckTraceModel) = New List(Of TruckTraceModel)
                    For loopx As Int32 = InfDS.Tables(0).Rows.Count - 1 To 0 Step -1
                        Dim Detail = New TruckTraceModel With {.TruckId = InfDS.Tables(0).Rows(0).Item("truckid"), .ProcessTitle = TWSClassNobatsManagement.GetStatusNameByCode(InfDS.Tables(0).Rows(loopx).Item("status")), .TravelTime = InfDS.Tables(0).Rows(loopx).Item("TravelTime"), .TerminalName = InfDS.Tables(0).Rows(loopx).Item("TerminalName"), .IssueDate = _R2DateTimeService.ConvertToShamsiDate(InfDS.Tables(0).Rows(loopx).Item("sodoortime")), .IssueTime = _R2DateTimeService.GetTimeOfDate(InfDS.Tables(0).Rows(loopx).Item("sodoortime")), .RealDate = _R2DateTimeService.ConvertToShamsiDate(InfDS.Tables(0).Rows(loopx).Item("DateReal")), .RealTime = _R2DateTimeService.GetTimeOfDate(InfDS.Tables(0).Rows(loopx).Item("DateReal")), .TraceWriter = InfDS.Tables(0).Rows(loopx).Item("TraceWriter")}
                        Lst.Add(Detail)
                    Next
                    Return Lst
                Else
                    Throw New AnyNotFoundException
                End If
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class



End Namespace