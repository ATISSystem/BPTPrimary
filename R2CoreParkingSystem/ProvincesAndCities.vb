

Imports R2Core
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SQLInjectionPrevention
Imports R2CoreParkingSystem.DataBaseManagement
Imports R2CoreParkingSystem.ProvincesAndCities.Execption
Imports R2CoreParkingSystem.ProvincesAndCities.R2CoreParkingSystemMClassCitys
Imports System.Data.SqlClient
Imports System.Reflection

Namespace ProvincesAndCities


    Public Class R2CoreParkingSystemMClassCitys

        Public Shared IRANCityCode As String = "99960000"
        Public Shared _DateTimeService As New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetNSSCity(YournCityCode As Int64) As R2StandardCityStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from dbtransport.dbo.TbCity Where nCityCode=" & YournCityCode & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Return New R2StandardCityStructure(Ds.Tables(0).Rows(0).Item("nCityCode"), Ds.Tables(0).Rows(0).Item("StrCityName").trim, Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDistance") / 25), Ds.Tables(0).Rows(0).Item("nProvince"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"))
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSCity(YourCityTitle As String) As R2StandardCityStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from dbtransport.dbo.TbCity Where strCityName like '%" & YourCityTitle.Trim() & "%'", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Return New R2StandardCityStructure(Ds.Tables(0).Rows(0).Item("nCityCode"), Ds.Tables(0).Rows(0).Item("StrCityName").trim, Convert.ToInt64(Ds.Tables(0).Rows(0).Item("nDistance") / 25), Ds.Tables(0).Rows(0).Item("nProvince"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"))
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Class R2StandardCityStructure
            Inherits BaseStandardClass.R2StandardStructure

            Private mynCityCode As Int64
            Private myStrCityName As String
            Private mynDistance As Int64
            Private mynProvince As Int64
            Private _Active As Boolean
            Private _ViewFlag As Boolean


#Region "Constructing Management"
            Public Sub New()
                MyBase.New()
                mynCityCode = 0 : myStrCityName = "" : mynDistance = 0 : mynProvince = 0 : _Active = False : _ViewFlag = False
            End Sub

            Public Sub New(ByVal YournCityCode As Int64, ByVal YourStrCityName As String, ByVal YournDistance As Int64, ByVal YournProvince As Int64, YourActive As Boolean, YourViewFlag As Boolean)
                MyBase.New(YournCityCode, YourStrCityName)
                mynCityCode = YournCityCode
                myStrCityName = YourStrCityName
                mynDistance = YournDistance
                mynProvince = YournProvince
                _Active = YourActive
                _ViewFlag = YourViewFlag
            End Sub
#End Region
#Region "Properting Management"
            Public Property nCityCode() As Int64
                Get
                    Return mynCityCode
                End Get
                Set(ByVal Value As Int64)
                    mynCityCode = Value
                End Set
            End Property
            Public Property StrCityName() As String
                Get
                    Return myStrCityName.Trim()
                End Get
                Set(ByVal Value As String)
                    myStrCityName = Value
                End Set
            End Property
            Public Property nDistance() As Int64
                Get
                    Return mynDistance
                End Get
                Set(ByVal Value As Int64)
                    mynDistance = Value
                End Set
            End Property
            Public Property nProvince() As Int64
                Get
                    Return mynProvince
                End Get
                Set(ByVal Value As Int64)
                    mynProvince = Value
                End Set
            End Property
            Public Property ViewFlag() As Boolean
                Get
                    Return _ViewFlag
                End Get
                Set(ByVal Value As Boolean)
                    _ViewFlag = Value
                End Set
            End Property
            Public Property Active() As Boolean
                Get
                    Return _Active
                End Get
                Set(ByVal Value As Boolean)
                    _Active = Value
                End Set
            End Property

#End Region


        End Class

        Public Shared Function GetDSCity() As DataSet
            Try
                Dim Ds As New DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select StrCityName from dbtransport.dbo.TbCity Order by StrCityName", 1000, Ds, New Boolean)
                Return Ds
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCityNameFromnCityCode(YourCityCode As String) As String
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select StrCityName from dbtransport.dbo.TbCity Where nCityCode=" & YourCityCode & "", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item(0)
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetnCityCodeFromStrCityName(YourCityName As String) As String
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select top 1 nCityCode from dbtransport.dbo.TbCity Where Viewflag=1 and Deleted=0 and StrCityName='" & YourCityName & "'", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item(0)
                Else
                    Throw New GetDataException
                End If
            Catch ex As GetDataException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetListOfCitys_SearchIntroCharacters(YourSearchStr As String) As List(Of R2StandardCityStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchStr)
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from dbtransport.dbo.TbCity Where Viewflag=1 and Deleted=0 and StrCityNAME LIKE '%" & YourSearchStr.Replace("ی", "ي").Replace("ک", "ك") & "%' and ViewFlag=1 Order by nCityCode", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Dim Lst As List(Of R2StandardCityStructure) = New List(Of R2StandardCityStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim NSS As R2StandardCityStructure = New R2StandardCityStructure(Ds.Tables(0).Rows(Loopx).Item("nCityCode"), Ds.Tables(0).Rows(Loopx).Item("StrCityName").trim, Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nDistance") / 25), Ds.Tables(0).Rows(Loopx).Item("nProvince"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetListOfCitys_SearchforLeftCharacters(YourSearchStr As String) As List(Of R2StandardCityStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchStr)
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from dbtransport.dbo.TbCity Where Left(StrCityNAME," & YourSearchStr.Length & ")='" & YourSearchStr.Replace("ی", "ي").Replace("ک", "ك") & "' and ViewFlag=1 Order by nCityCode", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Dim Lst As List(Of R2StandardCityStructure) = New List(Of R2StandardCityStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim NSS As R2StandardCityStructure = New R2StandardCityStructure(Ds.Tables(0).Rows(Loopx).Item("nCityCode"), Ds.Tables(0).Rows(Loopx).Item("StrCityName").trim, Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nDistance") / 25), Ds.Tables(0).Rows(Loopx).Item("nProvince"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetListOfCitysWhichDistanceIsZero() As List(Of R2StandardCityStructure)
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from dbtransport.dbo.TbCity Where nDistance=0 Order by StrCityName", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                End If
                Dim Lst As List(Of R2StandardCityStructure) = New List(Of R2StandardCityStructure)
                For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
                    Dim NSS As R2StandardCityStructure = New R2StandardCityStructure(Ds.Tables(0).Rows(Loopx).Item("nCityCode"), Ds.Tables(0).Rows(Loopx).Item("StrCityName").trim, Convert.ToInt64(Ds.Tables(0).Rows(Loopx).Item("nDistance") / 25), Ds.Tables(0).Rows(Loopx).Item("nProvince"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("ViewFlag"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub ChangeCityDistance(YourNSS As R2StandardCityStructure, YourDistance As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbTransport.dbo.TbCity Set nDistance=" & YourDistance * 25 & " Where nCityCode=" & YourNSS.nCityCode & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub ChangeCityProvince(YourNSS As R2StandardCityStructure, YourProvince As Int64)
            Dim CmdSql As New SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update dbTransport.dbo.TbCity Set nProvince=" & YourProvince & " Where nCityCode=" & YourNSS.nCityCode & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub



    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemCity
        Public CityId As Int64
        Public CityTitle As String
        Public ProvinceId As Int64
        Public Active As Boolean
        Public ViewFlag As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemProvincesAndCitiesManager

        Private _DateTimeService As IR2DateTimeService
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetCityNameFromnCityCode(YourCityCode As String) As String
            Try
                Dim Ds As New DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select StrCityName from dbtransport.dbo.TbCity Where nCityCode=" & YourCityCode & "", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item(0)
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'BPTChanged
        Public Function GetListOfCitys_SearchIntroCharacters(YourSearchStr As String, YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchStr)

                Dim Ds As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                           "Select Provinces.ProvinceId as ProvinceId,Provinces.ProvinceName as ProvinceName,Provinces.Active as ProvinceActive,Cities.nCityCode as CityCode,Cities.StrCityName as CityTitle,Cities.Active as CityActive
                              from R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces
                                Inner Join DBTransport.dbo.tbCity as Cities On Provinces.ProvinceId=Cities.nProvince 
                            Where Provinces.Deleted=0 and Cities.Deleted=0 and StrCityName like N'%" & YourSearchStr & "%' Order By Provinces.ProvinceId
                            for json auto")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                           "Select Provinces.ProvinceId as ProvinceId,Provinces.ProvinceName as ProvinceName,Provinces.Active as ProvinceActive,Cities.nCityCode as CityCode,Cities.StrCityName as CityTitle,Cities.Active as CityActive
                              from R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces as Provinces
                                Inner Join DBTransport.dbo.tbCity as Cities On Provinces.ProvinceId=Cities.nProvince 
                            Where Provinces.Deleted=0 and Cities.Deleted=0 and StrCityName like N'%" & YourSearchStr & "%' Order By Provinces.ProvinceId
                            for json auto", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub ChangeActiveStatusOfProvince(YourProvinceId As Int64, YourProvineActive As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblProvinces Set Active=" & IIf(YourProvineActive, 1, 0) & " 
                                      Where ProvinceId=" & YourProvinceId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub ChangeActiveStatusOfCity(YourCityId As Int64, YourCityActive As Boolean)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update DBTransport.dbo.tbCity Set Active=" & IIf(YourCityActive, 1, 0) & " 
                                      Where nCityCode=" & YourCityId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetProvinceIdOfCity(YourCityId As Int64) As Int64
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                     "Select nProvince from dbtransport.dbo.TbCity Where nCityCode=" & YourCityId & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New CityNotFoundException
                End If
                Return Ds.Tables(0).Rows(0).Item("nProvince")
            Catch ex As CityNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Function

        Public Function GetCity(YourCityId As Int64) As R2CoreParkingSystemCity
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from dbtransport.dbo.TbCity Where nCityCode=" & YourCityId & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New CityNotFoundException
                End If
                Return New R2CoreParkingSystemCity With {.CityId = Ds.Tables(0).Rows(0).Item("nCityCode"), .CityTitle = Ds.Tables(0).Rows(0).Item("StrCityName").trim, .ProvinceId = Ds.Tables(0).Rows(0).Item("nProvince"), .Active = Ds.Tables(0).Rows(0).Item("Active"), .ViewFlag = Ds.Tables(0).Rows(0).Item("ViewFlag")}
            Catch ex As CityNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Namespace Execption
        Public Class CityNotFoundException
            Inherits BPTException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "شهر مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

End Namespace
