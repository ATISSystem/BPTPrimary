


Imports System.Net.Http
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports System.Reflection
Imports Newtonsoft.Json
Imports R2Core.DateAndTimeManagement
Imports System.Text
Imports R2Core.DateTimeProvider.Models
Imports R2Core.DateTimeProvider
Imports System.Threading.Tasks
Imports R2Core.SoftwareUserManagement

Public Class TestAPIs
    Private _HttpClient As HttpClient = Nothing

    Public Sub New()
        Try
            Dim InstanceAPIHelper = New R2CoreAPIHelperManager
            _HttpClient = InstanceAPIHelper.GetAPIKernelTasks()
        Catch ex As FileNotExistException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Sub

    Public Async Function GetTimeOfDate(YourDateTime As DateTime) As Threading.Tasks.Task(Of String)
        Try
            Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetTimeOfDate")
            Dim RequestContent = New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTime}
            Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
            Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
            If Response.IsSuccessStatusCode Then
                Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                Return (JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)).Result
            Else
                Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
            End If
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function

    Public Function TestGetTimeOfDate(YourDateTime As Date) As String
        Try
            Return Task.Run(Function() GetTimeOfDate(YourDateTime)).Result
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function

    Public Async Function GetSoftwareUser(YourSoftwareUserMobileNumber As String) As Threading.Tasks.Task(Of R2CoreRawSoftwareUserStructure)
        Try
            Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetSoftWareUser")
            Dim RequestContent = New With {.SessionId = "21b7287aa201b1a2986097b6d3192cd5zFmdVCTUt=5A", .SoftwareUserMobileNumber = "09132043148"}
            Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
            Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
            If Response.IsSuccessStatusCode Then
                Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                Return (JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)).Result
            Else
                Throw New AggregateException(JsonConvert.DeserializeObject(Of R2CoreRawExceptionMessage)(Response.Content.ReadAsStringAsync().Result).ErrorMessage)
            End If
        Catch ex As AggregateException
            Throw ex
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function

    Public Function TestGetSoftwareUser(YourSoftwareUserMobileNumber As String) As R2CoreRawSoftwareUserStructure
        Try
            Return Task.Run(Function() GetSoftwareUser(YourSoftwareUserMobileNumber)).Result
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As AggregateException
            Throw New AggregateException(ex.InnerException.Message)
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function

    Public Async Function GetAllDevices(YourImmediately As Boolean) As Threading.Tasks.Task(Of String)
        Try
            Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetAllDevices")
            Dim RequestContent = New With {.SessionId = "437afc8d06bf702a78480fac3c5a8719&pbg@IXogyDd"}
            Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
            Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
            If Response.IsSuccessStatusCode Then
                Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                Return ResponseContent
                'Return (JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)).Result
            Else
                Throw New AggregateException(JsonConvert.DeserializeObject(Of R2CoreRawExceptionMessage)(Response.Content.ReadAsStringAsync().Result).ErrorMessage)
            End If
        Catch ex As AggregateException
            Throw ex
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function

    Public Function TestGetAllDevices(YourImmediately As Boolean) As String
        Try
            Return Task.Run(Function() GetAllDevices(YourImmediately)).Result
        Catch ex As UnableConnectToAPIException
            Throw ex
        Catch ex As AggregateException
            Throw New AggregateException(ex.InnerException.Message)
        Catch ex As Exception
            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        End Try
    End Function


End Class
