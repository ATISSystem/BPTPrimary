


Imports System.CodeDom
Imports System.Reflection
Imports System.Web.Services.Protocols
Imports System.Web.UI.WebControls
Imports Newtonsoft.Json
Imports R2Core.Caching
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.PublicProc
Imports R2Core.SoftwareUserManagement

Namespace Carousels

    Public Class R2CoreCarousel
        Public CId As Int64
        Public CTitle As String
        Public URL As String
        Public Description As String
        Public DateTimeMilladi As DateTime
        Public ShamsiDate As String
        Public Time As String
        Public Active As Boolean
        Public Picture As Byte()
    End Class

    Public Class R2CoreCarouselsManager

        Private _WS As R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetCarousels(YourAllCarousels As Boolean) As String
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourAllCarousels Then
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select CId,CTitle,URL,Description,DateTimeMilladi,ShamsiDate,Time,Active from R2Primary.dbo.TblCarousels Where Deleted=0 Order By CId for JSON Path", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New AnyNotFoundException
                    End If
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                        "Select CId,CTitle,URL,Description,DateTimeMilladi,ShamsiDate,Time,Active from R2Primary.dbo.TblCarousels Where Active=1 and Deleted=0 Order By CId for JSON Path", 0, DS, New Boolean).GetRecordsCount = 0 Then
                        Throw New AnyNotFoundException
                    End If
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetCarousel(YourCId As Int64, YourSoftwareUser As R2CoreSoftwareUser) As Byte()
            Try
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)

                Return _WS.WebMethodGetFile(RawGroups.R2CoreRawGroups.Carousels, YourCId.ToString + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreConfigurations.JPGBitmap, 0), _WS.WebMethodLogin(YourSoftwareUser.UserShenaseh, YourSoftwareUser.UserPassword))
            Catch ex As SoapException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub CarouselRegistering(YourCarousel As R2CoreCarousel, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 CId From R2Primary.dbo.TblCarousels with (tablockx) Order By CId Desc"
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('R2Primary.dbo.TblCarousels') "
                Dim CIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2Primary.dbo.TblCarousels(CTitle,URL,Description,DateTimeMilladi,ShamsiDate,Time,Active,ViewFlag,Deleted)
                                      Values(@CTitle,@URL,@Description,convert(varchar, getdate(), 20),R2Primary.DBO.BPTCOGregorianToPersian(GETDATE()),convert(varchar, getdate(), 8),1,1,0)"
                CmdSql.Parameters.Add("@CTitle", SqlDbType.NVarChar).Value = YourCarousel.CTitle
                CmdSql.Parameters.Add("@URL", SqlDbType.NVarChar).Value = YourCarousel.URL
                CmdSql.Parameters.Add("@Description", SqlDbType.NVarChar).Value = YourCarousel.Description
                CmdSql.ExecuteNonQuery()
                _WS.WebMethodSaveFile(RawGroups.R2CoreRawGroups.Carousels, CIdNew.ToString + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreConfigurations.JPGBitmap, 0), YourCarousel.Picture, _WS.WebMethodLogin(YourSoftwareUser.UserShenaseh, YourSoftwareUser.UserPassword))
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                CarouselsCachePreparing(YourSoftwareUser)
            Catch ex As SoapException
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub CarouselEditing(YourCarousel As R2CoreCarousel, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)

                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblCarousels Set CTitle=@CTitle,URL=@URL,Description=@Description Where CId=@CId"
                CmdSql.Parameters.Add("@CTitle", SqlDbType.NVarChar).Value = YourCarousel.CTitle
                CmdSql.Parameters.Add("@URL", SqlDbType.NVarChar).Value = YourCarousel.URL
                CmdSql.Parameters.Add("@Description", SqlDbType.NVarChar).Value = YourCarousel.Description
                CmdSql.Parameters.Add("@CId", SqlDbType.BigInt).Value = YourCarousel.CId
                CmdSql.ExecuteNonQuery()
                If YourCarousel.Picture IsNot Nothing Then
                    _WS.WebMethodDeleteFile(RawGroups.R2CoreRawGroups.Carousels, YourCarousel.CId.ToString + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreConfigurations.JPGBitmap, 0), _WS.WebMethodLogin(YourSoftwareUser.UserShenaseh, YourSoftwareUser.UserPassword))
                    _WS.WebMethodSaveFile(RawGroups.R2CoreRawGroups.Carousels, YourCarousel.CId.ToString + InstanceGeneralConfiguration.GetStringConfiguration(R2CoreConfigurations.JPGBitmap, 0), YourCarousel.Picture, _WS.WebMethodLogin(YourSoftwareUser.UserShenaseh, YourSoftwareUser.UserPassword))
                End If
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                CarouselsCachePreparing(YourSoftwareUser)
            Catch ex As SoapException
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub CarouselDeleting(YourCarouselId As Int64, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblCarousels Set Active=0, Deleted = 1 Where CId=@CId"
                CmdSql.Parameters.Add("@CId", SqlDbType.BigInt).Value = YourCarouselId
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                CarouselsCachePreparing(YourSoftwareUser)
            Catch ex As SoapException
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw ex
            Catch ex As Exception
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub CarouselChangeActiveStatus(YourCarouselId As Int64, YourActive As Boolean, YourSoftwareUser As R2CoreSoftwareUser)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Update R2Primary.dbo.TblCarousels Set Active=@Active Where CId=@CId"
                CmdSql.Parameters.Add("@Active", SqlDbType.Bit).Value = IIf(YourActive, 1, 0)
                CmdSql.Parameters.Add("@CId", SqlDbType.BigInt).Value = YourCarouselId
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                CarouselsCachePreparing(YourSoftwareUser)
            Catch ex As Exception
                CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub CarouselsCachePreparing(YourSoftwareUser As R2CoreSoftwareUser)
            Try
                Dim InstanceCache = New R2CoreCacheManager(_DateTimeService)

                Dim Carousels = JsonConvert.DeserializeObject(Of List(Of R2CoreCarousel))(GetCarousels(False))
                Dim Lst As New List(Of Object)
                For Each Carousel In Carousels
                    Dim CarouselCache = New With {.CId = Carousel.CId, .URL = Carousel.URL, .Picture = GetCarousel(Carousel.CId, YourSoftwareUser)}
                    Lst.Add(CarouselCache)
                Next
                InstanceCache.SetCache(InstanceCache.GetCacheType(R2CoreCacheTypes.Carousel).CacheTypeName, Lst, R2CoreCacheTypes.Carousel, R2Core.Caching.R2CoreCatchDataBases.Carousels, True)
            Catch ex As SoapException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetCarouselsForViewing() As String
            Try
                Dim InstanceCache = New R2CoreCacheManager(_DateTimeService)
                Dim Carousel = InstanceCache.GetCache(InstanceCache.GetCacheType(R2CoreCacheTypes.Carousel).CacheTypeName, R2CoreCatchDataBases.Carousels).ToString
                If Carousel Is Nothing Then Throw New AnyNotFoundException
                Return Carousel
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

End Namespace

Namespace Exceptions


End Namespace