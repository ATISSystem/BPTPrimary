

Imports R2Core.ExceptionManagement
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Xml

Namespace Networking
    Public Class R2CoreAPIHelperManager
        Public Function GetHttpClient(YourTargetAPIName As String) As HttpClient
            Try
                'XML Config File
                Dim Doc = New XmlDocument()
                Dim APPFolder = AppDomain.CurrentDomain.BaseDirectory
                Dim FileName = "APIs.Config"
                Dim FilePath = Path.Combine(APPFolder, FileName)
                If File.Exists(FilePath) Then
                    Doc.Load(FilePath)
                Else
                    Throw New FileNotExistException(FilePath)
                End If
                Dim APIDateTime As XmlNode = Doc.SelectSingleNode("/APIs/" + YourTargetAPIName)
                Dim Uri = APIDateTime("URI").InnerText

                'Http Client
                Dim baseUri = New Uri(Uri)
                Dim HttpClient = New HttpClient()
                HttpClient.BaseAddress = baseUri
                HttpClient.DefaultRequestHeaders.Clear()
                HttpClient.DefaultRequestHeaders.ConnectionClose = False
                HttpClient.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                Try
                    ServicePointManager.FindServicePoint(baseUri).ConnectionLeaseTimeout = 60 * 1000
                Catch ex As Exception
                End Try
                Return HttpClient
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class
End Namespace