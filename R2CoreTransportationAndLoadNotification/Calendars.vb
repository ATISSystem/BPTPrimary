

Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateAndTimeManagement.Exceptions
Imports R2Core.DateTimeProvider
Imports System.Reflection

Namespace CalendarManagement
    Namespace SpecializedPersianCalendar
        Public Class R2CoreTransportationAndLoadNotificationSpecializedPersianCalendarManager

            Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager
            Private _DateTimeService As IR2DateTimeService
            Public Sub New(YourDateTimeService As IR2DateTimeService)
                _DateTimeService = YourDateTimeService
            End Sub

            Public Function GetFirstDateShamsiInRangeWithoutHoliday(YourTopBaseDateShamsi As String, YourTotalDay As Int64) As String
                Try
                    If Not _DateTimeService.CheckShamsiDateSyntax(YourTopBaseDateShamsi) Then Throw New ShamsiDateSyntaxNotValidException
                    Dim Ds As DataSet = Nothing
                    Dim Count = InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                             Where DateShamsi<
                                   (Select Top 1 * from 
                                       (Select Top " & YourTotalDay & " DateShamsi from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                                        Where PCType=0 and DateShamsi<='" & YourTopBaseDateShamsi & "' Order By DateShamsi desc) as DataBox
                                    Order By DateShamsi)
                             Order By DateShamsi Desc", 3600, Ds, New Boolean).GetRecordsCount()
                    If Count <> YourTotalDay Then Throw New FirstDateShamsiInRangeWithoutHolidayException
                    Return Ds.Tables(0).Rows(Count - 1).Item("DateShamsi")
                Catch ex As ShamsiDateSyntaxNotValidException
                    Throw ex
                Catch ex As FirstDateShamsiInRangeWithoutHolidayException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            Public Function IsTodayIsHolidayforLoadAnnounce() As Boolean
                Try
                    Dim Ds As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 LoadAnnounce from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTimeService.GetCurrentShamsiDate & "' 
                               Order By HId Desc", 3600, Ds, New Boolean)
                    Return Ds.Tables(0).Rows(0).Item("LoadAnnounce")
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            'BPTChanged
            Public Function IsActiveTomorrowLoadAnnounce() As Boolean
                Try
                    Dim Ds As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 LoadAnnounce from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTimeService.GetNextShamsiDate & "' Order By HId Desc", 32767, Ds, New Boolean)
                    Return Ds.Tables(0).Rows(0).Item("LoadAnnounce")
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function

            Public Function IsActiveTodayLoadAnnounce() As Boolean
                Try
                    Dim Ds As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 LoadAnnounce from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTimeService.GetCurrentShamsiDate & "' Order By HId Desc", 32767, Ds, New Boolean)
                    Return Ds.Tables(0).Rows(0).Item("LoadAnnounce")
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function


            Public Function IsTodayIsHoliday() As Boolean
                Try
                    Dim Ds As DataSet = Nothing
                    InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                            "Select Top 1 PCTYpe from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportationLoadNotificationSpecializedPersianCalendar 
                               Where DateShamsi='" & _DateTimeService.GetCurrentShamsiDate & "' Order By HId Desc", 32767, Ds, New Boolean)
                    Return Convert.ToBoolean(Ds.Tables(0).Rows(0).Item("PCTYpe"))
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
                End Try
            End Function


        End Class


    End Namespace

End Namespace
