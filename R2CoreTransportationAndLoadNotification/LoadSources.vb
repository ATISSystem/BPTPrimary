

Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreTransportationAndLoadNotification.LoadSources.Exceptions
Imports System.Reflection

Namespace LoadSources

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _NSSCity = Nothing
        End Sub

        Public Sub New(ByVal YourNSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure)
            MyBase.New(YourNSSCity.nCityCode, YourNSSCity.StrCityName)
            _NSSCity = YourNSSCity
        End Sub

        Public Property NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadSourcesManager
        Public Function GetNSSLoadSource(YournIdSource As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
            Try
                Dim NSSCity = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdSource)
                If NSSCity Is Nothing Then Throw New LoadSourceIdNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(NSSCity)
            Catch exx As GetNSSException
                Throw New LoadSourceIdNotFoundException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadSourcesManagement
        Public Shared Function GetNSSLoadSource(YournIdSource As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure
            Try
                Dim NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdSource)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(NSSCity)
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadSources_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchIntroCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadSources_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchforLeftCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadSourceStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Namespace Exceptions

        Public Class LoadSourceIdNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شهری با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class LoadSourceIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مبدا غیر فعال است"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public NotInheritable Class R2CoreTransportationAndLoadNotificationLoadSourcesManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function IsActiveLoadSource(YourLoadSourceId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Active from DBTransport.dbo.tbCity Where nCityCode=" & YourLoadSourceId & "", 32767, DS, New Boolean)
                Return Convert.ToBoolean(DS.Tables(0).Rows(0).Item("Active"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace
