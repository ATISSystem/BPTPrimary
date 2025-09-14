



Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreTransportationAndLoadNotification.LoadTargets.Exceptions
Imports System.Reflection

Namespace LoadTargets

    Public Class R2CoreTransportationAndLoadNotificationStandardProvinceStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            ProvinceId = Int64.MinValue
            ProvinceTitle = String.Empty
        End Sub

        Public Sub New(ByVal YourProvinceId As Int64, YourProvinceTitle As String)
            MyBase.New(YourProvinceId, YourProvinceTitle)
            ProvinceId = YourProvinceId
            ProvinceTitle = YourProvinceTitle
        End Sub

        Public Property ProvinceId As Int64
        Public Property ProvinceTitle As String

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _NSSCity = Nothing
            _TargetTravelLength = Int64.MinValue
        End Sub

        Public Sub New(ByVal YourNSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure)
            MyBase.New(YourNSSCity.nCityCode, YourNSSCity.StrCityName)
            _NSSCity = YourNSSCity
            _TargetTravelLength = YourNSSCity.nDistance
        End Sub

        Public Property NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure
        Public Property TargetTravelLength As Int64

    End Class

    Public Class R2CoreTransportationAndLoadNotificationMclassLoadTargetsManager
        Private _DateTime As R2DateTime = New R2DateTime

        Public Function GetNSSLoadTarget(YournIdTarget As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
            Try
                Dim NSSCity = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdTarget)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(NSSCity)
            Catch ex As GetNSSException
                Throw New LoadTargetIdNotFoundException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMclassLoadTargetsManagement
        Private Shared _DateTime As R2DateTime = New R2DateTime

        Public Shared Function GetNSSLoadTarget(YournIdTarget As Int64) As R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure
            Try
                Dim NSSCity As R2CoreParkingSystemMClassCitys.R2StandardCityStructure = R2CoreParkingSystemMClassCitys.GetNSSCity(YournIdTarget)
                If NSSCity Is Nothing Then Throw New GetNSSException
                Return New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(NSSCity)
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadTargets_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchforLeftCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(X)).ToList()
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoadTargets_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure)
            Try
                Dim LstCitys As List(Of R2CoreParkingSystemMClassCitys.R2StandardCityStructure) = R2CoreParkingSystemMClassCitys.GetListOfCitys_SearchIntroCharacters(YourSearchString)
                Dim Lst As List(Of R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure) = LstCitys.Select(Function(X) New R2CoreTransportationAndLoadNotificationStandardLoadTargetStructure(X)).ToList()
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

        Public Class LoadTargetsforProvinceNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقصدی برای استان مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadTargetIdNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شهری با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class LoadTargetIsUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقصد غیر فعال است"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoadTargetsManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function IsActiveLoadTarget(YourLoadTargetId As Int64) As Boolean
            Try
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Active from DBTransport.dbo.tbCity Where nCityCode=" & YourLoadTargetId & "", 32767, DS, New Boolean)
                Return Convert.ToBoolean(DS.Tables(0).Rows(0).Item("Active"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class


End Namespace
