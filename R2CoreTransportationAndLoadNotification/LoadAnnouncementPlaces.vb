

Imports R2Core.DatabaseManagement
Imports R2Core.PublicProc
Imports R2Core.WebProcessesManagement.Exceptions
Imports System.Reflection

Namespace LoadAnnouncementPlaces
    Public Class R2CoreTransportationAndLoadNotificationLoadAnnouncementPlacesManager
        Public Function GetLoadAnnouncementPlaces() As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select LoadAnnouncementPlaces.LAPTitle,LoadAnnouncementPlaces.LAPIconName,LoadAnnouncementPlaces.LAPURL,LoadAnnouncementPlaces.Description 
                  from R2PrimaryTransportationAndLoadNotification.dbo.TblLoadAnnouncementPlaces as LoadAnnouncementPlaces 
                  Where Active=1 Order By LAPId for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New SoftwareUserHasNotAnyWebProcessPermissionException
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class
End Namespace
