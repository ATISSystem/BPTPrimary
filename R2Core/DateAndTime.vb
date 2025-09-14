


Imports log4net.Layout
Imports Newtonsoft.Json
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateAndTimeManagement.Exceptions
Imports R2Core.DateTimeProvider.Models
Imports R2Core.ExceptionManagement
Imports R2Core.Networking
Imports R2Core.PublicProc
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports System.Xml


Namespace DateTimeProvider

    Namespace Models

        Public Class R2CoreDateTimeProviderResult
            Public Result As Object
        End Class

        Public Class R2CoreDateTimeProviderShamsiDateMonthsToAdd
            Public ShamsiDate As String
            Public MonthsToAdd As Int16
        End Class



    End Namespace

    Public Interface IR2DateTimeService
        Function ConvertToShamsiDate(YourDateTime As DateTime) As String
        Function GetTimeOfDate(YourDateTime As DateTime) As String
        Function GetTickofTime(YourDateTime As DateTime) As Int64
        Function GetCurrentTickofTime() As Int64
        Function GetMilladiDateTimeFromShamsiDate(ByVal YourShamsiDate As String, YourTime As String) As DateTime
        Function GetLastShamsiDate() As String
        Function GetShamsiDateWithAddMonth(YourShamsiDate As String, YourMonthsToAdd As Int16) As String
        Function GetCurrentTime() As String
        Function GetCurrentTimeWithSecond() As String
        Function GetCurrentTimeWithMinute() As String
        Function GetNextShamsiDate() As String
        Function GetNextShamsiDateWithoutSlashes() As String
        Function GetTime(YourDateTimeMilladi As Date) As String
        Function GetCurrentDateAndTime() As Object
        Function GetCurrentShamsiDate() As String
        Function GetCurrentShamsiDateWithoutSlashes() As String
        Function GetCurrentDateTimeMilladi() As DateTime
        Function GetCurrentShamsiSal() As String
        Function CheckShamsiDateSyntax(YourShamsiDate As String) As Boolean
        Function CheckTimeSyntax(YourTime As String) As Boolean
        Function GetPersianMonthName(YourShamsiDate As String) As String
        Function GetDelimetedTime(YourUnDelimetedTime As String) As String
        Function Get6ZeroTime() As String
    End Interface

    Friend Class R2CoreDateTimeProviderAPICaller

        Private _HttpClient As HttpClient = Nothing
        Public Result As Object = Nothing

        Public Sub New()
            Try
                Dim InstanceAPIHelper = New R2CoreAPIHelperManager
                _HttpClient = InstanceAPIHelper.GetHttpClient("APIDateTime")
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub ConvertToShamsiDate(YourDateTime As DateTime)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/ConvertToShamsiDate")
                Dim RequestContent = New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetTimeOfDate(YourDateTime As DateTime)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetTimeOfDate")
                Dim RequestContent = New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetTickofTime(YourDateTime As DateTime)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetTickofTime")
                Dim RequestContent = New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentTickofTime()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentTickofTime")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetMilladiDateTimeFromShamsiDate(ByVal YourShamsiDate As String, YourTime As String)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetMilladiDateTimeFromShamsiDate")
                Dim RequestContent = New R2CoreDateAndTime With {.ShamsiDate = YourShamsiDate, .Time = YourTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetLastShamsiDate()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetLastShamsiDate")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetShamsiDateWithAddMonth(YourShamsiDate As String, YourMonthsToAdd As Int16)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetShamsiDateWithAddMonth")
                Dim RequestContent = New R2CoreDateTimeProviderShamsiDateMonthsToAdd With {.ShamsiDate = YourShamsiDate, .MonthsToAdd = YourMonthsToAdd}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentTime()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentTime")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentTimeWithSecond()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentTimeWithSecond")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentTimeWithMinute()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentTimeWithMinute")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetNextShamsiDate()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetNextShamsiDate")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetNextShamsiDateWithoutSlashes()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetNextShamsiDateWithoutSlashes")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetTime(YourDateTimeMilladi As Date)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetTime")
                Dim RequestContent = New R2CoreDateAndTime With {.DateTimeMilladi = YourDateTimeMilladi}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentDateAndTime()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentDateAndTime")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentShamsiDate()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentShamsiDate")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentShamsiDateWithoutSlashes()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentShamsiDateWithoutSlashes")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentDateTimeMilladi()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentDateTimeMilladi")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetCurrentShamsiSal()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetCurrentShamsiSal")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub CheckShamsiDateSyntax(YourShamsiDate As String)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/CheckShamsiDateSyntax")
                Dim RequestContent = New R2CoreDateAndTime With {.ShamsiDate = YourShamsiDate}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub CheckTimeSyntax(YourTime As String)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/CheckTimeSyntax")
                Dim RequestContent = New R2CoreDateAndTime With {.Time = YourTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetPersianMonthName(YourShamsiDate As String)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetPersianMonthName")
                Dim RequestContent = New R2CoreDateAndTime With {.ShamsiDate = YourShamsiDate}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub GetDelimetedTime(YourUnDelimetedTime As String)
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/GetDelimetedTime")
                Dim RequestContent = New R2CoreDateAndTime With {.Time = YourUnDelimetedTime}
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnableConnectToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Async Sub Get6ZeroTime()
            Try
                Dim Request As HttpRequestMessage = New HttpRequestMessage(HttpMethod.Post, "/api/Get6ZeroTime")
                Dim RequestContent = Nothing
                Request.Content = New StringContent(JsonConvert.SerializeObject(RequestContent), Encoding.UTF8, "application/json")
                Dim Response As HttpResponseMessage = Await _HttpClient.SendAsync(Request)
                If Response.IsSuccessStatusCode Then
                    Dim ResponseContent = Await Response.Content.ReadAsStringAsync()
                    Result = JsonConvert.DeserializeObject(Of R2CoreDateTimeProviderResult)(ResponseContent)
                Else
                    Throw New UnSuccessConnectionToAPIException(Response.Content.ReadAsStringAsync().Result)
                End If
            Catch ex As UnSuccessConnectionToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

    End Class

    Public Class R2DateTimeService
        Implements IR2DateTimeService

        Private InstanceAPICaller = New R2CoreDateTimeProviderAPICaller

        Private Function GetResult() As R2CoreDateTimeProviderResult
            Try
                While (InstanceAPICaller.Result Is Nothing)
                    Threading.Thread.Sleep(10)
                    If InstanceAPICaller.Result IsNot Nothing Then Exit While
                End While
                Return DirectCast(InstanceAPICaller.Result, R2CoreDateTimeProviderResult)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function ConvertToShamsiDate(YourDateTime As Date) As String Implements IR2DateTimeService.ConvertToShamsiDate
            Try
                InstanceAPICaller.ConvertToShamsiDate(YourDateTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetTimeOfDate(YourDateTime As Date) As String Implements IR2DateTimeService.GetTimeOfDate
            Try
                InstanceAPICaller.GetTimeOfDate(YourDateTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetTickofTime(YourDateTime As Date) As Int64 Implements IR2DateTimeService.GetTickofTime
            Try
                InstanceAPICaller.GetTickofTime(YourDateTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTickofTime() As Int64 Implements IR2DateTimeService.GetCurrentTickofTime
            Try
                InstanceAPICaller.GetCurrentTickofTime()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetMilladiDateTimeFromShamsiDate(YourShamsiDate As String, YourTime As String) As DateTime Implements IR2DateTimeService.GetMilladiDateTimeFromShamsiDate
            Try
                InstanceAPICaller.GetMilladiDateTimeFromShamsiDate(YourShamsiDate, YourTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetLastShamsiDate() As String Implements IR2DateTimeService.GetLastShamsiDate
            Try
                InstanceAPICaller.GetLastShamsiDate()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetShamsiDateWithAddMonth(YourShamsiDate As String, YourMonthsToAdd As Short) As String Implements IR2DateTimeService.GetShamsiDateWithAddMonth
            Try
                InstanceAPICaller.GetShamsiDateWithAddMonth(YourShamsiDate, YourMonthsToAdd)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTime() As String Implements IR2DateTimeService.GetCurrentTime
            Try
                InstanceAPICaller.GetCurrentTime()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTimeWithSecond() As String Implements IR2DateTimeService.GetCurrentTimeWithSecond
            Try
                InstanceAPICaller.GetCurrentTimeWithSecond()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTimeWithMinute() As String Implements IR2DateTimeService.GetCurrentTimeWithMinute
            Try
                InstanceAPICaller.GetCurrentTimeWithMinute()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetNextShamsiDate() As String Implements IR2DateTimeService.GetNextShamsiDate
            Try
                InstanceAPICaller.GetNextShamsiDate()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetNextShamsiDateWithoutSlashes() As String Implements IR2DateTimeService.GetNextShamsiDateWithoutSlashes
            Try
                InstanceAPICaller.GetNextShamsiDateWithoutSlashes()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetTime(YourDateTimeMilladi As Date) As String Implements IR2DateTimeService.GetTime
            Try
                InstanceAPICaller.GetTime(YourDateTimeMilladi)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentDateAndTime() As Object Implements IR2DateTimeService.GetCurrentDateAndTime
            Try
                InstanceAPICaller.GetCurrentDateAndTime()
                Return JsonConvert.DeserializeObject(Of R2CoreDateAndTime)(GetResult().Result.ToString())
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiDate() As String Implements IR2DateTimeService.GetCurrentShamsiDate
            Try
                InstanceAPICaller.GetCurrentShamsiDate()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiDateWithoutSlashes() As String Implements IR2DateTimeService.GetCurrentShamsiDateWithoutSlashes
            Try
                InstanceAPICaller.GetCurrentShamsiDateWithoutSlashes()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentDateTimeMilladi() As DateTime Implements IR2DateTimeService.GetCurrentDateTimeMilladi
            Try
                InstanceAPICaller.GetCurrentDateTimeMilladi()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiSal() As String Implements IR2DateTimeService.GetCurrentShamsiSal
            Try
                InstanceAPICaller.GetCurrentShamsiSal()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function CheckShamsiDateSyntax(YourShamsiDate As String) As Boolean Implements IR2DateTimeService.CheckShamsiDateSyntax
            Try
                InstanceAPICaller.CheckShamsiDateSyntax(YourShamsiDate)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function CheckTimeSyntax(YourTime As String) As Boolean Implements IR2DateTimeService.CheckTimeSyntax
            Try
                InstanceAPICaller.CheckTimeSyntax(YourTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetPersianMonthName(YourShamsiDate As String) As String Implements IR2DateTimeService.GetPersianMonthName
            Try
                InstanceAPICaller.GetPersianMonthName(YourShamsiDate)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetDelimetedTime(YourUnDelimetedTime As String) As String Implements IR2DateTimeService.GetDelimetedTime
            Try
                InstanceAPICaller.GetDelimetedTime(YourUnDelimetedTime)
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function Get6ZeroTime() As String Implements IR2DateTimeService.Get6ZeroTime
            Try
                InstanceAPICaller.Get6ZeroTime()
                Return GetResult().Result
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As UnSuccessConnectionToAPIException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

    End Class

End Namespace

Namespace DateAndTimeManagement


    Public NotInheritable Class R2CoreMclassDateAndTimeManagement
        Public Shared Function GetPersianDaysDiffDate(YourDate1 As String, YourDate2 As String) As Int64
            Try
                Dim year1 As Int64 = Convert.ToInt64(YourDate1.Substring(0, 4))
                Dim month1 As Int64 = Convert.ToInt64(YourDate1.Substring(5, 2))
                Dim day1 As Int64 = Convert.ToInt64(YourDate1.Substring(8, 2))

                Dim year2 As Int64 = Convert.ToInt64(YourDate2.Substring(0, 4))
                Dim month2 As Int64 = Convert.ToInt64(YourDate2.Substring(5, 2))
                Dim day2 As Int64 = Convert.ToInt64(YourDate2.Substring(8, 2))

                Dim Calendar As System.Globalization.PersianCalendar = New System.Globalization.PersianCalendar()
                Dim dt1 As DateTime = Calendar.ToDateTime(year1, month1, day1, 0, 0, 0, 0)
                Dim dt2 As DateTime = Calendar.ToDateTime(year2, month2, day2, 0, 0, 0, 0)
                Dim ts As TimeSpan = dt2.Subtract(dt1)
                Return ts.Days
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    'Public Class R2CoreDateAndTime

    '    Private myDateTimeMilladi As DateTime
    '    Private myDateTimeMilladi As String
    '    Private myShamsiDate As String
    '    Private myTime As String

    '    Public Sub New()
    '        MyBase.New()
    '        myDateTimeMilladi = Date.Now : myDateTimeMilladi = String.Empty : myShamsiDate = "" : myTime = ""
    '    End Sub

    '    Public Sub New(ByVal DateTimeMilladii As DateTime, ByVal ShamsiDatel As String, ByVal Timee As String)
    '        myDateTimeMilladi = DateTimeMilladii
    '        myDateTimeMilladi = myDateTimeMilladi.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
    '        myShamsiDate = ShamsiDatel
    '        myTime = Timee
    '    End Sub

    '    Public Property DateTimeMilladi() As DateTime
    '        Get
    '            Return myDateTimeMilladi
    '        End Get
    '        Set(ByVal Value As DateTime)
    '            myDateTimeMilladi = Value
    '        End Set
    '    End Property

    '    Public ReadOnly Property DateTimeMilladi() As String
    '        Get
    '            Return myDateTimeMilladi
    '        End Get
    '    End Property


    '    Public Property ShamsiDate() As String
    '        Get
    '            Return myShamsiDate
    '        End Get
    '        Set(ByVal Value As String)
    '            myShamsiDate = Value
    '        End Set
    '    End Property

    '    Public Property Time() As String
    '        Get
    '            Return myTime
    '        End Get
    '        Set(ByVal Value As String)
    '            myTime = Value
    '        End Set
    '    End Property

    '    Public ReadOnly Property GetShamsiYear() As String
    '        Get
    '            Return Mid(ShamsiDate, 1, 4)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetShamsiMonth() As String
    '        Get
    '            Return Mid(ShamsiDate, 6, 2)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetShamsiDay() As String
    '        Get
    '            Return Mid(ShamsiDate, 9, 2)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetHour() As String
    '        Get
    '            Return Mid(Time, 1, 2)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetMinute() As String
    '        Get
    '            Return Mid(Time, 4, 2)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetSecond() As String
    '        Get
    '            Return Mid(Time, 7, 2)
    '        End Get
    '    End Property

    '    Public ReadOnly Property GetConcatString() As String
    '        Get
    '            Return ShamsiDate.Replace("/", "") + Time.Replace(":", "")
    '        End Get
    '    End Property



    'End Class

    'Public Class R2DateTime
    '    'Dim mycurrenttime As String = Trim(Mid(DateAndTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 8))
    '    Private myOpenConnection As R2PrimarySubscriptionDBSqlConnection = New R2PrimarySubscriptionDBSqlConnection
    '    Private PC As New System.Globalization.PersianCalendar()
    '    Private HC As New System.Globalization.HijriCalendar()

    '    Public Sub New()
    '        'myOpenConnection.GetConnection.Open()
    '    End Sub
    '    Protected Overrides Sub Finalize()
    '        'If myOpenConnection.GetConnection.State <> ConnectionState.Closed Then myOpenConnection.GetConnection.Close()
    '    End Sub
    '    Public Function GetTimeOfDate(ByVal YouDateTime As DateTime) As String
    '        Try
    '            Dim mycurrenttime As String = Trim(Mid(YouDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 8))
    '            Return mycurrenttime
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetTickofTime(YourDateTime As R2CoreDateAndTime) As TimeSpan
    '        Try
    '            Return Date.ParseExact(YourDateTime.DateTimeMilladi, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function

    '    Public Function GetCurrentTickofTime() As TimeSpan
    '        Try
    '            Return Date.ParseExact(GetCurrentDateTimeMilladi, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetMilladiDateTimeFromShamsiDate(ByVal YourShamsiDateFull As String, YourTime As String) As Date
    '        Try
    '            Return PC.ToDateTime(Mid(YourShamsiDateFull, 1, 4), Mid(YourShamsiDateFull, 6, 2), Mid(YourShamsiDateFull, 9, 2), Mid(YourTime, 1, 2), Mid(YourTime, 4, 2), Mid(YourTime, 7, 2), 0)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetMilladiDateTimeFromShamsiDateFormated(ByVal YourShamsiDateFull As String, YourTime As String) As String
    '        Try
    '            Return PC.ToDateTime(Mid(YourShamsiDateFull, 1, 4), Mid(YourShamsiDateFull, 6, 2), Mid(YourShamsiDateFull, 9, 2), Mid(YourTime, 1, 2), Mid(YourTime, 4, 2), Mid(YourTime, 7, 2), 0).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetYesterdayShamsiDate() As String
    '        Try
    '            Return ConvertToShamsiDateFull(DateTime.Now.AddDays(-1))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetNextShamsiMonth(YourShamsiDate As R2CoreDateAndTime, YourNextMonth As Int16) As R2CoreDateAndTime
    '        Try
    '            Dim MilladiTemp As DateTime = GetMilladiDateTimeFromShamsiDate(YourShamsiDate.ShamsiDate, YourShamsiDate.Time)
    '            MilladiTemp = MilladiTemp.AddMonths(YourNextMonth)
    '            Return New R2CoreDateAndTime(Nothing, ConvertToShamsiDateFull(MilladiTemp), Nothing)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Private Function GetSqlServerCurrentDate() As DateTime
    '        Try
    '            Dim CmdsqlDateTime As SqlClient.SqlCommand = New SqlClient.SqlCommand("Select Getdate()")
    '            CmdsqlDateTime.Connection = myOpenConnection.GetConnection
    '            CmdsqlDateTime.Connection.Open()
    '            Dim myDateTime As DateTime = CmdsqlDateTime.ExecuteScalar
    '            CmdsqlDateTime.Connection.Close()
    '            Return myDateTime
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentTimeSecond() As String
    '        Try
    '            Return Trim(Mid(GetSqlServerCurrentDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 8))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentTimeMinute() As String
    '        Try
    '            Return Trim(Mid(GetSqlServerCurrentDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 5))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetNextDateShamsi() As String
    '        Try
    '            Return ConvertToShamsiDateFull(GetCurrentDateTimeMilladi().Today.AddDays(1))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetNextDateShamsiWithoutSlashes() As String
    '        Try
    '            Return ConvertToShamsiDateFull(GetCurrentDateTimeMilladi().Today.AddDays(1)).Replace("/", "")
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentTime() As String
    '        Try
    '            Return Trim(Mid(GetSqlServerCurrentDate.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 8))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentTime(YourDateTime As DateTime)
    '        Return Mid(YourDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), 12, 20)
    '    End Function
    '    Public Function GetCurrentDateAndTime() As R2CoreDateAndTime
    '        Try
    '            Dim D = GetCurrentDateTimeMilladi()
    '            Return New R2CoreDateAndTime(D, ConvertToShamsiDateFull(D), GetCurrentTime(D))
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentShamsiDate() As String
    '        Try
    '            Return ConvertToShamsiDateFull(GetSqlServerCurrentDate())
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentShamsiDateWithoutSlashes() As String
    '        Try
    '            Return GetCurrentShamsiDate.Replace("/", "")
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentDateShamsi() As String
    '        Try
    '            Return Mid(Trim(GetCurrentShamsiDate), 3, 20)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function ConvertToShamsiDateFull(ByVal MilladiDate As DateTime) As String
    '        Try
    '            Dim MM(12) As Byte
    '            Dim yy, y, D, M, sum, a As Int16
    '            Dim m1, d1, y1 As String
    '            MM(1) = 31
    '            MM(2) = 28
    '            MM(3) = 31
    '            MM(4) = 30
    '            MM(5) = 31
    '            MM(6) = 30
    '            MM(7) = 31
    '            MM(8) = 31
    '            MM(9) = 30
    '            MM(10) = 31
    '            MM(11) = 30
    '            MM(12) = 31
    '            D = MilladiDate.Day
    '            M = MilladiDate.Month
    '            y = MilladiDate.Year
    '            yy = y - 1980
    '            yy = yy - Int(yy / 4) * 4
    '            If yy = 0 Then
    '                MM(2) = 29
    '            Else
    '                MM(2) = 28
    '            End If
    '            sum = 0
    '            If M <> 1 Then
    '                For I As Int16 = 1 To M - 1
    '                    sum = sum + MM(I)
    '                Next
    '            End If
    '            sum = sum + D
    '            If yy = 1 Then
    '                sum = sum + 287
    '            Else
    '                sum = sum + 286
    '            End If
    '            If yy = 1 Then
    '                a = 366
    '            Else
    '                a = 365
    '            End If
    '            If sum > a Then
    '                sum = sum - a
    '                y = y + 1
    '            End If
    '            If sum > 186 Then
    '                sum = sum - 186
    '                M = 7 + Int(sum / 30)
    '                D = sum - (Int(sum / 30) * 30)
    '                If D = 0 Then
    '                    D = 30
    '                    M = M - 1
    '                End If
    '            Else
    '                M = 1 + Int(sum / 31)
    '                D = sum - (Int(sum / 31) * 31)
    '                If D = 0 Then
    '                    M = M - 1
    '                    D = 31
    '                End If
    '            End If
    '            y = y - 622
    '            If M < 10 Then
    '                m1 = "0" + Trim(Str(M))
    '            Else
    '                m1 = Trim(Str(M))
    '            End If
    '            If D < 10 Then
    '                d1 = "0" + Trim(Str(D))
    '            Else
    '                d1 = Trim(Str(D))
    '            End If
    '            y1 = Trim(Str(y))
    '            Return (y1 + "/" + m1 + "/" + d1)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentDateTimeMilladi() As DateTime
    '        Try
    '            Return GetSqlServerCurrentDate()
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentDateTimeMilladi() As String
    '        Try
    '            Return GetSqlServerCurrentDate().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentSalShamsi() As String
    '        Try
    '            Return Mid(GetCurrentSalShamsiFull, 1, 2)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetCurrentSalShamsiFull() As String
    '        Try
    '            Return Mid(GetCurrentShamsiDate, 1, 4)
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function ChekShamsiDateSyntax(ByVal Datex As String) As Boolean
    '        Try
    '            Dim mydate As String = Trim(Datex)
    '            Dim mysal As String = Mid(mydate, 1, 4)
    '            Dim mymah As String = Mid(mydate, 6, 2)
    '            Dim myrooz As String = Mid(mydate, 9, 2)
    '            If Len(mydate) <> 10 Then Return False
    '            If Mid(mydate, 5, 1) <> "/" Then Return False
    '            If Mid(mydate, 8, 1) <> "/" Then Return False
    '            If Not ((Val(mymah) >= 1) And (Val(mymah) <= 12)) Then Return False
    '            If ((Val(mymah) >= 1) And (Val(mymah) <= 6)) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= 31)) Then Return False
    '            ElseIf ((Val(mymah) >= 7) And (Val(mymah) <= 11)) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= 30)) Then Return False
    '            ElseIf (Val(mymah) = 12) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.EsfandRooz, 0))) Then Return False
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function ChekDateShamsiSyntax(ByVal Datex As String) As Boolean
    '        Try
    '            Dim mydate As String = Trim(Datex)
    '            Dim mysal As String = Mid(mydate, 1, 2)
    '            Dim mymah As String = Mid(mydate, 4, 2)
    '            Dim myrooz As String = Mid(mydate, 7, 2)
    '            If Len(mydate) <> 8 Then Return False
    '            If Mid(mydate, 3, 1) <> "/" Then Return False
    '            If Mid(mydate, 6, 1) <> "/" Then Return False
    '            If Not ((Val(mymah) >= 1) And (Val(mymah) <= 12)) Then Return False
    '            If ((Val(mymah) >= 1) And (Val(mymah) <= 6)) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= 31)) Then Return False
    '            ElseIf ((Val(mymah) >= 7) And (Val(mymah) <= 11)) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= 30)) Then Return False
    '            ElseIf (Val(mymah) = 12) Then
    '                If Not ((Val(myrooz) >= 1) And (Val(myrooz) <= R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.EsfandRooz, 0))) Then Return False
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function ChekTimeSyntax(ByVal zaman As String) As Boolean
    '        Try
    '            Dim mytime As String = Trim(zaman)
    '            If Len(mytime) <> 8 Then
    '                Return False : Exit Function
    '            End If
    '            If Mid(mytime, 3, 1) <> ":" Then
    '                Return False : Exit Function
    '            End If
    '            If Mid(mytime, 6, 1) <> ":" Then
    '                Return False : Exit Function
    '            End If
    '            If Not ((CInt(Mid(mytime, 1, 2)) >= 0) And (CInt(Mid(mytime, 1, 2)) <= 23)) Then
    '                Return False : Exit Function
    '            End If
    '            If Not ((CInt(Mid(mytime, 4, 2)) >= 0) And (CInt(Mid(mytime, 4, 2)) <= 59)) Then
    '                Return False : Exit Function
    '            End If
    '            If Not ((CInt(Mid(mytime, 7, 2)) >= 0) And (CInt(Mid(mytime, 4, 2)) <= 59)) Then
    '                Return False : Exit Function
    '            End If
    '            Return True
    '        Catch ex As Exception
    '            Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
    '        End Try
    '    End Function
    '    Public Function GetPersianMonthName(ByVal YourDateShamsi As String) As String
    '        Dim Mah As String = Mid(YourDateShamsi, 6, 2)
    '        If Mah = "01" Then Return "فروردین ماه"
    '        If Mah = "02" Then Return "اردیبهشت  ماه"
    '        If Mah = "03" Then Return "خرداد  ماه"
    '        If Mah = "04" Then Return "تیر  ماه"
    '        If Mah = "05" Then Return "مرداد  ماه"
    '        If Mah = "06" Then Return "شهریور  ماه"
    '        If Mah = "07" Then Return "مهر  ماه"
    '        If Mah = "08" Then Return "آبان  ماه"
    '        If Mah = "09" Then Return "آذر  ماه"
    '        If Mah = "10" Then Return "دی  ماه"
    '        If Mah = "11" Then Return "بهمن  ماه"
    '        If Mah = "12" Then Return "اسفند  ماه"
    '    End Function
    '    Public Function GetDelimetedTime(YourUnDelimetedTime As String) As String
    '        Dim InstancePublicProcedures As New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
    '        If YourUnDelimetedTime.Length < 8 Then YourUnDelimetedTime = YourUnDelimetedTime + InstancePublicProcedures.RepeatStr("0", 8 - YourUnDelimetedTime.Length)
    '        Return Mid(YourUnDelimetedTime, 1, 2) + ":" + Mid(YourUnDelimetedTime, 3, 2) + ":" + Mid(YourUnDelimetedTime, 5, 2)
    '    End Function

    '    Public Function Get6ZeroTime() As String
    '        Return "00:00:00"
    '    End Function
    'End Class

    Public Class HafteMahManagement
        'روتين پر کردن کمبو هفته 
        Public Shared Function GetMahNameFromMahCode(ByVal MahCode As String) As String
            If MahCode.Trim = "10" Then
                Return "فروردين"
            ElseIf MahCode.Trim = "11" Then
                Return "ارديبهشت"
            ElseIf MahCode.Trim = "12" Then
                Return "خرداد"
            ElseIf MahCode.Trim = "13" Then
                Return "تير"
            ElseIf MahCode.Trim = "14" Then
                Return "مرداد"
            ElseIf MahCode.Trim = "15" Then
                Return "شهريور"
            ElseIf MahCode.Trim = "16" Then
                Return "مهر"
            ElseIf MahCode.Trim = "17" Then
                Return "آبان"
            ElseIf MahCode.Trim = "18" Then
                Return "آذر"
            ElseIf MahCode.Trim = "19" Then
                Return "دي"
            ElseIf MahCode.Trim = "20" Then
                Return "بهمن"
            ElseIf MahCode.Trim = "21" Then
                Return "اسفند"
            End If
        End Function
        Public Shared Function GetHafteRoozNameFromCode(ByVal RoozCode As String) As String
            If RoozCode.Trim = "10" Then
                Return "شنبه"
            ElseIf RoozCode.Trim = "11" Then
                Return "يکشنبه"
            ElseIf RoozCode.Trim = "12" Then
                Return "دوشنبه"
            ElseIf RoozCode.Trim = "13" Then
                Return "سه شنبه"
            ElseIf RoozCode.Trim = "14" Then
                Return "چهارشنبه"
            ElseIf RoozCode.Trim = "15" Then
                Return "پنجشنبه"
            ElseIf RoozCode.Trim = "16" Then
                Return "جمعه"
            End If
        End Function
        'روتين برگرداندن تعداد روزهاي يک ماه شمسي
        Public Shared Function GetDaysOfMah(ByVal mahcode As String) As Int16
            'کد ماه بين 10 تا 21 است يعني فروردين تا اسفند
            Try
                Dim mymahcode As String = Trim(mahcode)
                If (Len(Trim(mahcode)) <> 2) Or (CInt(mahcode) - 9 < 1) Or (CInt(mahcode) - 9 > 12) Then
                    Throw New Exception("کد ماه شمسي بايد بين اعداد 10 تا 21 باشد")
                End If
                If (CInt(mahcode) - 9 >= 1) And (CInt(mahcode) - 9 <= 6) Then
                    Return 31 : Exit Function
                End If
                If (CInt(mahcode) - 9 >= 7) And (CInt(mahcode) - 9 <= 11) Then
                    Return 30 : Exit Function
                End If
                If (CInt(mahcode) - 9 = 12) Then Return R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.EsfandRooz, 0)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function


    End Class

    Namespace CalendarManagement

        Namespace PersianCalendar
            Public Enum PersianCalendarType
                None = 0
                Holiday = 1
            End Enum

            Public Class R2CoreStandardPersianCalendarStructure
                Inherits BaseStandardClass.R2StandardStructure
                Public Sub New()
                    _HId = Int64.MinValue
                    _DateShamsi = String.Empty
                    _PCType = Int16.MinValue
                End Sub

                Public Sub New(ByVal YourHId As Int64, YourDateShamsi As String, YourPCType As String)
                    _HId = YourHId
                    _DateShamsi = YourDateShamsi
                    _PCType = YourPCType
                End Sub

                Public Property HId As Int64
                Public Property DateShamsi As String
                Public Property PCType As Int16

            End Class

            Public Class R2CoreInstanceDateAndTimePersianCalendarManager
                Private _DateTime = New R2DateTime

                Public Function GetHoliDayNumber(ByVal YourShamsiDate1 As String, ByVal YourShamsiDate2 As String) As UInteger
                    Try
                        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                        Dim Ds As DataSet
                        InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) AS Counting from R2Primary.dbo.TblPersianCalendar where (dateshamsi>'" & YourShamsiDate1 & "') and (dateshamsi<'" & YourShamsiDate2 & "')  and PCType=1", 1, Ds, New Boolean)
                        Return Ds.Tables(0).Rows(0).Item("Counting")
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

                Public Function GetforThisMonth(YourDateTime As R2CoreDateAndTime) As List(Of R2CoreStandardPersianCalendarStructure)
                    Try
                        Dim DSPersianCallendar As New DataSet
                        Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                        InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                         "Select * From R2Primary.Dbo.TblPersianCalendar Where SUBSTRING(DateShamsi,1,7)='" & Mid(YourDateTime.ShamsiDate, 1, 7) & "' Order By DateShamsi ", 3600, DSPersianCallendar, New Boolean)
                        Dim Lst = New List(Of R2CoreStandardPersianCalendarStructure)
                        For Loopx As Int64 = 0 To DSPersianCallendar.Tables(0).Rows.Count - 1
                            Dim PersianCalendar = New R2CoreStandardPersianCalendarStructure(DSPersianCallendar.Tables(0).Rows(Loopx).Item("HId"), DSPersianCallendar.Tables(0).Rows(Loopx).Item("DateShamsi").trim, DSPersianCallendar.Tables(0).Rows(Loopx).Item("PCType"))
                            Lst.Add(PersianCalendar)
                        Next
                        Return Lst
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

            End Class

            Public NotInheritable Class R2CoreDateAndTimePersianCalendarManagement
                Private Shared InstanceSqlDataBOX = New R2CoreSqlDataBOXManager

                Public Shared Function GetHoliDayNumber(ByVal YourShamsiDate1 As String, ByVal YourShamsiDate2 As String) As UInteger
                    Try
                        Dim Ds As DataSet
                        InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Count(*) AS Counting from R2Primary.dbo.TblPersianCalendar where (dateshamsi>'" & YourShamsiDate1 & "') and (dateshamsi<'" & YourShamsiDate2 & "')  and PCType=1", 1, Ds, New Boolean)
                        Return Ds.Tables(0).Rows(0).Item("Counting")
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function

                Public Shared Function GetLastNonHoliday(YourNSSDateTime As R2CoreDateAndTime) As R2CoreDateAndTime
                    Try
                        Dim Ds As DataSet
                        InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblPersianCalendar Order By DateShamsi Desc", 3600, Ds, New Boolean)
                        Dim Record As DataRow
                        Record = Ds.Tables(0).Select("DateShamsi < '" & YourNSSDateTime.ShamsiDate & "' and PCType=0", "DateShamsi Desc")(0)
                        Return New R2CoreDateAndTime With {.ShamsiDate = Record.Item("DateShamsi")}
                    Catch ex As Exception
                        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                    End Try
                End Function



            End Class

        End Namespace


    End Namespace

    'BPTChange
    Namespace Exceptions

        Public Class TimeSyntaxNotValidException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "فرمت ساعت نادرست است"
                End Get
            End Property
        End Class

        Public Class FirstDateShamsiInRangeWithoutHolidayException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "یافتن اولین تاریخ در محدوده درخواستی برای روزهای بدون تعطیل با خطای اساسی مواجه شد"
                End Get
            End Property
        End Class

        'BPTChange
        Public Class ShamsiDateSyntaxNotValidException
            Inherits ApplicationException

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "تاریخ شمسی نادرست است"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Class R2CoreDateAndTime
        Public DateTimeMilladi As DateTime
        Public ShamsiDate As String
        Public Time As String
    End Class

    'BPTChanged
    Public Class R2DateTime
        Private PC As New System.Globalization.PersianCalendar()
        Private HC As New System.Globalization.HijriCalendar()

        Public Sub New()
        End Sub

        Public Function ConvertToShamsiDate(ByVal YourMilladiDate As DateTime) As String
            Try
                Dim MM(12) As Byte
                Dim yy, y, D, M, sum, a As Int16
                Dim m1, d1, y1 As String
                MM(1) = 31
                MM(2) = 28
                MM(3) = 31
                MM(4) = 30
                MM(5) = 31
                MM(6) = 30
                MM(7) = 31
                MM(8) = 31
                MM(9) = 30
                MM(10) = 31
                MM(11) = 30
                MM(12) = 31
                D = YourMilladiDate.Day
                M = YourMilladiDate.Month
                y = YourMilladiDate.Year
                yy = y - 1980
                yy = yy - Int(yy / 4) * 4
                If yy = 0 Then
                    MM(2) = 29
                Else
                    MM(2) = 28
                End If
                sum = 0
                If M <> 1 Then
                    For I As Int16 = 1 To M - 1
                        sum = sum + MM(I)
                    Next
                End If
                sum = sum + D
                If yy = 1 Then
                    sum = sum + 287
                Else
                    sum = sum + 286
                End If
                If yy = 1 Then
                    a = 366
                Else
                    a = 365
                End If
                If sum > a Then
                    sum = sum - a
                    y = y + 1
                End If
                If sum > 186 Then
                    sum = sum - 186
                    M = 7 + Int(sum / 30)
                    D = sum - (Int(sum / 30) * 30)
                    If D = 0 Then
                        D = 30
                        M = M - 1
                    End If
                Else
                    M = 1 + Int(sum / 31)
                    D = sum - (Int(sum / 31) * 31)
                    If D = 0 Then
                        M = M - 1
                        D = 31
                    End If
                End If
                y = y - 622
                If M < 10 Then
                    m1 = "0" + Trim(Str(M))
                Else
                    m1 = Trim(Str(M))
                End If
                If D < 10 Then
                    d1 = "0" + Trim(Str(D))
                Else
                    d1 = Trim(Str(D))
                End If
                y1 = Trim(Str(y))
                Return (y1 + "/" + m1 + "/" + d1)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetTimeOfDate(ByVal YouDateTime As DateTime) As String
            Try
                Return YouDateTime.ToString("HH:mm:ss")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetTickofTime(YourDateTime As DateTime) As TimeSpan
            Try
                Return YourDateTime.TimeOfDay
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTickofTime() As TimeSpan
            Try
                Return DateTime.Now.TimeOfDay
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetMilladiDateTimeFromShamsiDate(ByVal YourShamsiDate As String, YourTime As String) As Date
            Try
                Return PC.ToDateTime(Mid(YourShamsiDate, 1, 4), Mid(YourShamsiDate, 6, 2), Mid(YourShamsiDate, 9, 2), Mid(YourTime, 1, 2), Mid(YourTime, 4, 2), Mid(YourTime, 7, 2), 0).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetLastShamsiDate() As String
            Try
                Return ConvertToShamsiDate(DateTime.Now.AddDays(-1))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetShamsiDateWithAddMonth(YourShamsiDate As String, YourMonthsToAdd As Int16) As String
            Try
                Dim MilladiTemp As DateTime = GetMilladiDateTimeFromShamsiDate(YourShamsiDate, "00:00:00")
                MilladiTemp = MilladiTemp.AddMonths(YourMonthsToAdd)
                Return ConvertToShamsiDate(MilladiTemp)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTime() As String
            Try
                Return Date.Now.ToString("HH:mm:ss")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTimeWithSecond() As String
            Try
                Return Date.Now.ToString("HH:mm:ss")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentTimeWithMinute() As String
            Try
                Return Date.Now.ToString("HH:mm")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetNextShamsiDate() As String
            Try
                Return ConvertToShamsiDate(Date.Now.AddDays(1))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNextShamsiDateWithoutSlashes() As String
            Try
                Return GetNextShamsiDate.Replace("/", "")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTime(YourDateTime As DateTime) As String
            Try
                Return YourDateTime.ToString("HH:mm:ss")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetCurrentDateAndTime() As R2CoreDateAndTime
            Try
                Return New R2CoreDateAndTime With {.DateTimeMilladi = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture), .ShamsiDate = ConvertToShamsiDate(DateTime.Now), .Time = GetCurrentTime()}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiDate() As String
            Try
                Return ConvertToShamsiDate(DateTime.Now)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiDateWithoutSlashes() As String
            Try
                Return GetCurrentShamsiDate.Replace("/", "")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentDateTimeMilladi() As DateTime
            Try
                Return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetCurrentShamsiSal() As String
            Try
                Return Mid(GetCurrentShamsiDate, 1, 4)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function CheckShamsiDateSyntax(ByVal YourShamsiDate As String) As Boolean
            Try
                Dim InstanceConfigurations = New R2CoreConfigurationsManager
                Dim pattern = "^[1-4]\d{3}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|31|([1-2][0-9])|(0[1-9]))))$"
                Dim Regex = New Regex(pattern)
                If Not Regex.IsMatch(YourShamsiDate) Then Return False
                Dim ShamsiDate As String = Trim(YourShamsiDate)
                Dim Sal As String = Mid(ShamsiDate, 1, 4)
                Dim Maah As String = Mid(ShamsiDate, 6, 2)
                Dim Rooz As String = Mid(ShamsiDate, 9, 2)
                If Not ((Val(Maah) >= 1) And (Val(Maah) <= 12)) Then Return False
                If ((Val(Maah) >= 1) And (Val(Maah) <= 6)) Then
                    If Not ((Val(Rooz) >= 1) And (Val(Rooz) <= 31)) Then Return False
                ElseIf ((Val(Maah) >= 7) And (Val(Maah) <= 11)) Then
                    If Not ((Val(Rooz) >= 1) And (Val(Rooz) <= 30)) Then Return False
                ElseIf (Val(Maah) = 12) Then
                    If Not ((Val(Rooz) >= 1) And (Val(Rooz) <= InstanceConfigurations.GetConfigInt64(R2CoreConfigurations.EsfandRooz, 0))) Then Return False
                End If
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function CheckTimeSyntax(ByVal YourTime As String) As Boolean
            Try
                Dim Pattern = "([01]\d|2[0-3]):[0-5]\d:[0-5]\d"
                Dim Regex = New Regex(Pattern)
                If Not Regex.IsMatch(YourTime) Then Return False
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetPersianMonthName(ByVal YourDateShamsi As String) As String
            Try
                Dim Maah As String = Mid(YourDateShamsi, 6, 2)

                If Maah = "01" Then
                    Return "فروردین ماه"
                ElseIf Maah = "02" Then
                    Return "اردیبهشت  ماه"
                ElseIf Maah = "03" Then
                    Return "خرداد  ماه"
                ElseIf Maah = "04" Then
                    Return "تیر  ماه"
                ElseIf Maah = "05" Then
                    Return "مرداد  ماه"
                ElseIf Maah = "06" Then
                    Return "شهریور  ماه"
                ElseIf Maah = "07" Then
                    Return "مهر  ماه"
                ElseIf Maah = "08" Then
                    Return "آبان  ماه"
                ElseIf Maah = "09" Then
                    Return "آذر  ماه"
                ElseIf Maah = "10" Then
                    Return "دی  ماه"
                ElseIf Maah = "11" Then
                    Return "بهمن  ماه"
                ElseIf Maah = "12" Then
                    Return "اسفند  ماه"
                Else
                    Throw New ShamsiDateSyntaxNotValidException
                End If
            Catch ex As ShamsiDateSyntaxNotValidException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetDelimetedTime(YourUnDelimetedTime As String) As String
            Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
            If YourUnDelimetedTime.Length < 8 Then YourUnDelimetedTime = YourUnDelimetedTime + InstancePublicProcedures.RepeatStr("0", 8 - YourUnDelimetedTime.Length)
            Return Mid(YourUnDelimetedTime, 1, 2) + ":" + Mid(YourUnDelimetedTime, 3, 2) + ":" + Mid(YourUnDelimetedTime, 5, 2)
        End Function

        Public Function Get6ZeroTime() As String
            Return "00:00:00"
        End Function
    End Class


End Namespace
