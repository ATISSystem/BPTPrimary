

Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2Core.WebProcessesManagement.Exceptions
Imports R2CoreTransportationAndLoadNotification.Exceptions
Imports System.Reflection

Namespace LoadAnnouncementPlaces
    Public Class R2CoreTransportationAndLoadNotificationLoadAnnouncementPlacesManager
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Function GetLoadAnnouncementPlaces(YourProvinceId As Int64) As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet

                '"Select LoadAnnouncementPlaces.LAPTitle,LoadAnnouncementPlaces.LAPIconName,LoadAnnouncementPlaces.LAPURL,LoadAnnouncementPlaces.Description 
                '  from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAnnouncementPlaces as LoadAnnouncementPlaces 
                '  Where Active=1 and ProvinceId=" & YourProvinceId & " Order By LAPId for JSON Auto"

                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                 "Select * 
                  from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAnnouncementPlaces as LoadAnnouncementPlaces 
                  Where Active=1 Order By LAPId for JSON Auto", 32767, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New LoadAnnouncementPlacesForThisProvinceNotFoundException
                End If
            Catch ex As LoadAnnouncementPlacesForThisProvinceNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class
End Namespace

Namespace Exceptions

    Public Class LoadAnnouncementPlacesForThisProvinceNotFoundException
        Inherits BPTException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اعلام باری مرتبط با استان مورد نظر یافت نشد"
            End Get
        End Property
    End Class

End Namespace
