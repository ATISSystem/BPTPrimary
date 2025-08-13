
Imports System
Imports System.Drawing
Imports System.Globalization
Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Text

Imports AForge.Video.DirectShow
Imports AForge.Vision.Motion

Imports R2Core
Imports R2Core.SoftwareUserManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.BaseStandardClass
Imports R2Core.ComputersManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.ExceptionManagement
Imports R2Core.FileShareRawGroupsManagement
Imports R2Core.LoggingManagement
Imports R2Core.DesktopProcessesManagement
Imports R2Core.R2PrimaryFileSharingWS
Imports R2Core.MonetarySettingTools
Imports R2CoreGUI
Imports R2CoreLPR.ConfigurationManagement
Imports R2CoreLPR.LicensePlateManagement
Imports R2CoreLPR.LicensePlateRecognizer
Imports R2CoreParkingSystem
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.BlackList
Imports R2CoreParkingSystem.CamerasManagement
Imports R2CoreParkingSystem.Cars
Imports R2CoreParkingSystem.ProvincesAndCities
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.DataBaseManagement
Imports R2CoreParkingSystem.ExceptionManagement
Imports R2CoreParkingSystem.FileShareRawGroupsManagement
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement
Imports R2CoreParkingSystem.EnterExitManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement
Imports R2Core.EntityRelationManagement
Imports R2CoreParkingSystem.EntityRelations
Imports R2Core.PermissionManagement
Imports R2CoreParkingSystem.SoftwareUsersManagement.Exceptions
Imports R2Core.RequesterManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2CoreParkingSystem.PredefinedMessagesManagement
Imports R2CoreParkingSystem.AccountingManagement.ExceptionManagement
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports R2Core.MoneyWallet.MoneyWallet
Imports R2Core.MoneyWallet.Exceptions
Imports R2Core.SMS
Imports R2Core.SMS.Exceptions
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SMS.SMSTypes
Imports R2Core.DateAndTimeManagement.CalendarManagement.PersianCalendar
Imports R2CoreParkingSystem.SMS.SMSControllingMoneyWallet.Exceptions
Imports R2Core.SMS.SMSOwners
Imports R2Core.SMS.SMSOwners.Exceptions
Imports R2Core.SMS.SMSHandling
Imports R2CoreParkingSystem.SMS.SMSOwners
Imports R2Core.SMS.SMSOwners.R2CoreMClassSMSOwnersManager
Imports R2CoreParkingSystem.BlackList.Exceptions
Imports R2CoreParkingSystem.SMS.SMSTypes
Imports R2Core.SMS.SMSHandling.Exceptions
Imports R2CoreParkingSystem.CarsNativeness.Exceptions
Imports R2CoreParkingSystem.CarsNativeness
Imports R2CoreParkingSystem.MoneyWalletManagement.Exceptions
Imports R2Core.RawGroups
Imports System.Runtime.Serialization.Formatters.Binary



Namespace Cars

    Public Class R2StandardCarStructure
        Inherits BaseStandardClass.R2StandardStructure

        Private mynIdCar As Int64
        Private mysnCarType As String
        Private myStrCarNo As String
        Private myStrCarSerialNo As String
        Private mynIdCity As String



#Region "Constructing Management"
        Public Sub New()
            MyBase.New()
            mynIdCar = Int64.MinValue : mysnCarType = "" : myStrCarNo = "" : myStrCarSerialNo = "" : mynIdCity = ""
        End Sub

        Public Sub New(ByVal nIdCarr As Int64, ByVal snCarTypee As String, ByVal StrCarNoo As String, ByVal StrCarSerialNoo As String, ByVal nIdCityy As String)
            MyBase.New(nIdCarr, StrCarNoo)
            mynIdCar = nIdCarr
            mysnCarType = snCarTypee
            myStrCarNo = StrCarNoo
            myStrCarSerialNo = StrCarSerialNoo
            mynIdCity = nIdCityy
        End Sub
#End Region
#Region "Properting Management"
        Public Property nIdCar() As Int64
            Get
                Return mynIdCar
            End Get
            Set(ByVal Value As Int64)
                mynIdCar = Value
            End Set
        End Property
        Public Property snCarType() As String
            Get
                Return mysnCarType
            End Get
            Set(ByVal Value As String)
                mysnCarType = Value
            End Set
        End Property
        Public Property StrCarNo() As String
            Get
                Return myStrCarNo.Trim()
            End Get
            Set(ByVal Value As String)
                myStrCarNo = Value
            End Set
        End Property
        Public Property StrCarSerialNo() As String
            Get
                Return myStrCarSerialNo.Trim()
            End Get
            Set(ByVal Value As String)
                myStrCarSerialNo = Value
            End Set
        End Property
        Public Property nIdCity() As String
            Get
                Return mynIdCity.Trim()
            End Get
            Set(ByVal Value As String)
                mynIdCity = Value
            End Set
        End Property
#End Region
#Region "Subroutins And Functions"
        Public Function GetCarPelakSerialComposit() As String
            Return StrCarNo.Trim & "-" & StrCarSerialNo.Trim
        End Function
#End Region



    End Class

    Public Class R2CoreParkingSystemInstanceCarsManager
        Public Function GetNSSCar(YournIdCar As String) As R2StandardCarStructure
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select * from dbtransport.dbo.TbCar Where nIdCar=" & YournIdCar & "")
                Da.SelectCommand.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Dim NSS As R2StandardCarStructure = New R2StandardCarStructure
                    NSS.nIdCar = Ds.Tables(0).Rows(0).Item("nIdCar")
                    NSS.snCarType = Ds.Tables(0).Rows(0).Item("snCarType")
                    NSS.StrCarNo = Ds.Tables(0).Rows(0).Item("StrCarNo")
                    NSS.StrCarSerialNo = Ds.Tables(0).Rows(0).Item("StrCarSerialNo")
                    NSS.nIdCity = Ds.Tables(0).Rows(0).Item("nIdCity")
                    Return NSS
                Else
                    Throw New GetNSSException
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetnIdCarFromCardId(YourCardId As String) As Int64
            Try
                Dim Da As New SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 nCarId from R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Where (CardId='" & YourCardId & "') and (RelationActive=1) and ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By RelationId Desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item("nCarId")
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetnIdPersonFirst(YournIdCar As Int64) As Int64
            Try
                Dim DS As New DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2ClassSqlConnectionSepas,
                             "Select Top 1 nIdPerson from dbtransport.dbo.TbCarAndPerson where (nIdCar=" & YournIdCar & ") and (snRelation=2) And ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) Or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By nIDCarAndPerson Desc", 300, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return DS.Tables(0).Rows(0).Item("nIdPerson")
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetCardIdFromnIdCar(YournCarId As Int64) As Int64
            Try
                Dim Da As New SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CardId from R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Where (nCarId=" & YournCarId & ") and (RelationActive=1) and ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By RelationId Desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item("CardId")
                Else
                    Throw New RelatedTerraficCardNotFoundException
                End If
            Catch ex As RelatedTerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public Class R2CoreParkingSystemMClassCars

        Private Shared _DateTime As New R2DateTime
        Private Shared _R2PrimaryFSWS = New R2Core.R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService

        Public Shared Function IsExistCar(YourNSS As R2StandardCarStructure) As Boolean
            Try
                Dim DS As New DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2ClassSqlConnectionSepas, "Select StrCarNo,StrCarSerialNo from dbtransport.dbo.TbCar Where StrCarNo='" & YourNSS.StrCarNo & "' and StrCarSerialNo='" & YourNSS.StrCarSerialNo & "' and nIdCity=" & YourNSS.nIdCity & "", 1, DS, New Boolean).GetRecordsCount <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSCar(YournIdCar As String) As R2StandardCarStructure
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select * from dbtransport.dbo.TbCar Where nIdCar=" & YournIdCar & "")
                Da.SelectCommand.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Dim NSS As R2StandardCarStructure = New R2StandardCarStructure
                    NSS.nIdCar = Ds.Tables(0).Rows(0).Item("nIdCar")
                    NSS.snCarType = Ds.Tables(0).Rows(0).Item("snCarType")
                    NSS.StrCarNo = Ds.Tables(0).Rows(0).Item("StrCarNo")
                    NSS.StrCarSerialNo = Ds.Tables(0).Rows(0).Item("StrCarSerialNo")
                    NSS.nIdCity = Ds.Tables(0).Rows(0).Item("nIdCity")
                    Return NSS
                Else
                    Throw New RelatedTerraficCardNotFoundException
                End If
            Catch exx As RelatedTerraficCardNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub UpdateCar(YourNSS As R2StandardCarStructure)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim mynIdCar As Int64 = YourNSS.nIdCar
                CmdSql.CommandText = "Update dbtransport.dbo.tbCar Set snCarType=" & YourNSS.snCarType & ",StrCarNo='" & YourNSS.StrCarNo & "',StrCarSerialNo='" & YourNSS.StrCarSerialNo & "',nIdCity=" & YourNSS.nIdCity & " Where nIdCar=" & mynIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub DeleteCar(YourNSS As R2StandardCarStructure)
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                Dim mynIdCar As Int64 = YourNSS.nIdCar
                CmdSql.CommandText = "Delete dbtransport.dbo.tbCar Where nIdCar=" & mynIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function GetnIdCarFromCardId(YourCardId As String) As Int64
            Try
                Dim Da As New SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 nCarId from R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Where (CardId='" & YourCardId & "') and (RelationActive=1) and ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By RelationId Desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item("nCarId")
                Else
                    Throw New R2CoreParkingSystemRelatedCarNotExistException
                End If
            Catch ex As R2CoreParkingSystemRelatedCarNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCardIdFromnIdCar(YournCarId As Int64) As Int64
            Try
                Dim Da As New SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 CardId from R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Where (nCarId=" & YournCarId & ") and (RelationActive=1) and ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By RelationId Desc")
                Da.SelectCommand.Connection = (New R2PrimarySqlConnection).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item("CardId")
                Else
                    Throw New RelatedTerraficCardNotFoundException
                End If
            Catch ex As RelatedTerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetnIdPersonFirst(YournIdCar As Int64) As Int64
            Try
                Dim da As New SqlDataAdapter : Dim ds As New DataSet
                da.SelectCommand = New SqlCommand("Select Top 1 nIdPerson from dbtransport.dbo.TbCarAndPerson where (nIdCar=" & YournIdCar & ") and (snRelation=2) And ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) Or (RelationTimeStamp='2015-01-01 00:00:00.000')) Order By nIDCarAndPerson Desc")
                da.SelectCommand.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
                ds.Tables.Clear()
                If da.Fill(ds) <> 0 Then
                    Return ds.Tables(0).Rows(0).Item("nIdPerson")
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetnIdPersonSecond(YournIdCar As Int64) As Int64
            Try
                Dim da As New SqlDataAdapter : Dim ds As New DataSet
                da.SelectCommand = New SqlCommand("Select Top 1 nIdPerson from dbtransport.dbo.TbCarAndPerson where (nIdCar=" & YournIdCar & ") and (snRelation=3) And ((DATEDIFF(SECOND,RelationTimeStamp,getdate())<240) Or (RelationTimeStamp='2015-01-01 00:00:00.000'))  Order By nIDCarAndPerson Desc")
                da.SelectCommand.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
                ds.Tables.Clear()
                If da.Fill(ds) <> 0 Then
                    Return ds.Tables(0).Rows(0).Item("nIdPerson")
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetnIdCar(YourLP As R2StandardLicensePlateStructure) As Int64
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlCommand("Select Top 1 nIdCar from dbtransport.dbo.TbCar Where StrCarNo='" & YourLP.Pelak & "' and StrCarSerialNo='" & YourLP.Serial & "' and nIdCity=" & R2CoreParkingSystemMClassCitys.GetnCityCodeFromStrCityName(YourLP.City) & " Order By nIdCar Desc")
                Da.SelectCommand.Connection = (New R2ClassSqlConnectionSepas).GetConnection()
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return Ds.Tables(0).Rows(0).Item("nIdCar")
                Else
                    Throw New GetDataException
                End If
            Catch exx As GetDataException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCarEnterExitImage(YourNSSCar As R2StandardCarStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreImage
            Try
                Dim _NSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(R2CoreParkingSystemMClassCars.GetCardIdFromnIdCar(YourNSSCar.nIdCar))
                Dim CarImage As R2CoreImage
                Try
                    CarImage = GetCarImage(New R2CoreFile(R2CoreParkingSystemMClassEnterExitManagement.GetCarLastEnterExitId(_NSSTerafficCard).ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)), YourNSSUser)
                    Return CarImage
                Catch ex As CarHasNoEnterExitRecordException
                Catch exx As R2CoreParkingSystemCarImageNotExistException
                End Try
                If _NSSTerafficCard.CardType = TerafficCardType.Savari Then
                    Return New R2CoreImage(My.Resources.DefaultCarImageSavari)
                ElseIf _NSSTerafficCard.CardType = TerafficCardType.Tereili Then
                    Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                ElseIf _NSSTerafficCard.CardType = TerafficCardType.SixCharkh Or _NSSTerafficCard.CardType = TerafficCardType.TenCharkh Then
                    Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCarEnterExitImage(YourNSSTerraficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreImage
            Try
                Dim CarImage As R2CoreImage
                Try
                    CarImage = GetCarImage(New R2CoreFile(R2CoreParkingSystemMClassEnterExitManagement.GetCarLastEnterExitId(YourNSSTerraficCard).ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)), YourNSSUser)
                    Return CarImage
                Catch exx As R2CoreParkingSystemCarImageNotExistException
                    If YourNSSTerraficCard.CardType = TerafficCardType.Savari Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageSavari)
                    ElseIf YourNSSTerraficCard.CardType = TerafficCardType.Tereili Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                    ElseIf YourNSSTerraficCard.CardType = TerafficCardType.SixCharkh Or YourNSSTerraficCard.CardType = TerafficCardType.TenCharkh Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetCarEnterExitImage(YourNSSEnterExit As R2StandardEnterExitStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreImage
            Try
                Dim CarImage As R2CoreImage
                Try
                    CarImage = GetCarImage(New R2CoreFile(YourNSSEnterExit.EnterExitId.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)), YourNSSUser)
                    Return CarImage
                Catch exx As R2CoreParkingSystemCarImageNotExistException
                    Dim NSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure
                    NSSTerafficCard = R2CoreParkingSystemMClassTrafficCardManagement.GetNSSTrafficCard(YourNSSEnterExit.CardNoEnter)
                    If NSSTerafficCard.CardType = TerafficCardType.Savari Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageSavari)
                    ElseIf NSSTerafficCard.CardType = TerafficCardType.Tereili Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                    ElseIf NSSTerafficCard.CardType = TerafficCardType.SixCharkh Or NSSTerafficCard.CardType = TerafficCardType.TenCharkh Then
                        Return New R2CoreImage(My.Resources.DefaultCarImageTereilli)
                    End If
                Catch ex As Exception
                    Throw ex
                End Try
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub SaveCarEnterExitImage(YourCarImage As R2CoreImage, YourNSSEnterExit As R2StandardEnterExitStructure, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Try
                SaveCarImage(YourCarImage, New R2CoreFile(YourNSSEnterExit.EnterExitId.ToString() + R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.JPGBitmap, 2)), YourNSSUser)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared Sub SaveCarImage(YourCarImage As R2CoreImage, YourFileInf As R2CoreFile, YourNSSUser As R2CoreStandardSoftwareUserStructure)
            Try
                _R2PrimaryFSWS.WebMethodSaveFile(R2CoreParkingSystemRawGroups.CarImages, YourFileInf.FileName, YourCarImage.GetImageByte(), _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared Function GetCarImage(YourFileInf As R2CoreFile, YourNSSUser As R2CoreStandardSoftwareUserStructure) As R2CoreImage
            Try
                If _R2PrimaryFSWS.WebMethodIOFileExist(R2CoreParkingSystemRawGroups.CarImages, YourFileInf.FileName, _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword)) = True Then
                    Dim bf As BinaryFormatter = New BinaryFormatter()
                    Dim MS As MemoryStream = New MemoryStream()
                    bf.Serialize(MS, _R2PrimaryFSWS.WebMethodGetFile(R2CoreParkingSystemRawGroups.CarImages, YourFileInf.FileName, _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword)))
                    Return New R2CoreImage(MS.ToArray())
                Else
                    Throw New R2CoreParkingSystemCarImageNotExistException
                End If
            Catch exx As R2CoreParkingSystemCarImageNotExistException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Sub CreateRelationBetweenTerafficCardAndCar(YourNSSTerraficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourNSSCar As R2StandardCarStructure)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2Core.DatabaseManagement.R2PrimarySqlConnection).GetConnection()
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction()
                CmdSql.CommandText = "Update R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars Set RelationActive=0 Where CardId=" & YourNSSTerraficCard.CardId & " or nCarId=" & YourNSSCar.nIdCar & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Insert into R2PrimaryParkingSystem.dbo.TblTrafficCardsRelationCars(CardId,nCarId,RelationActive,RelationTimeStamp) Values(" & YourNSSTerraficCard.CardId & "," & YourNSSCar.nIdCar & ",1,'2015-01-01 00:00:00.000')"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub


    End Class

    Public Class CarNotExistException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "خودروی مورد نظر یافت نشد"
            End Get
        End Property
    End Class

    Public Class DuplicateCarExistException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "خودرو با مشخصات مورد نظر قبلا وجود دارد"
            End Get
        End Property
    End Class

    Public Class R2CoreParkingSystemCarImageNotExistException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "تصویر خودروی مورد نظر یافت نشد"
            End Get
        End Property
    End Class

    Public Class R2CoreParkingSystemRelatedCarNotExistException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعات خودروی مرتبط با کارت تردد یافت نشد" + vbCrLf + "به واحد ثبت مشخصات مراجعه نمایید"
            End Get
        End Property
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemCarsManager

        Private _R2DateTimeService As IR2DateTimeService
        Public Sub New(YourR2DateTimeService As IR2DateTimeService)
            _R2DateTimeService = YourR2DateTimeService
        End Sub

        Public Function InsertCar(YourNSS As R2StandardCarStructure) As Int64
            Dim CmdSql As SqlCommand = New SqlCommand
            CmdSql.Connection = (New DataBaseManagement.R2ClassSqlConnectionSepas).GetConnection()
            Try
                Dim InstanceSoftwareUserService = New SoftwareUserService(Nothing)
                Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager
                Dim AllCarSerials = Split(InstanceConfiguration.GetConfigString(R2CoreParkingSystemConfigurations.IndigenousCars, 1), ";")
                Dim CarSerials() = Split(AllCarSerials(0), "-")
                Dim NativenessType As Int16 = 0
                If CarSerials.Contains(YourNSS.StrCarSerialNo) Then
                    NativenessType = R2CoreParkingSystem.CarsNativeness.CarNativenessTypes.Native
                Else
                    NativenessType = R2CoreParkingSystem.CarsNativeness.CarNativenessTypes.UnNative
                End If
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select top 1 * from dbtransport.dbo.tbcar with (tablockx)" : CmdSql.ExecuteNonQuery()
                CmdSql.CommandText = "Select IDENT_CURRENT('dbtransport.dbo.tbCar')"
                Dim mynIdCar As Int64 = CmdSql.ExecuteScalar + 1
                CmdSql.CommandText = "Insert Into dbtransport.dbo.tbCar(snCarType,StrCarNo,StrCarSerialNo,nIdCity,StrBodyNo,nUserID,strFanniDate,CarNativenessTypeId,CarNativenessExpireDate,ViewFlag) Values(" & YourNSS.snCarType & ",'" & YourNSS.StrCarNo & "','" & YourNSS.StrCarSerialNo & "'," & YourNSS.nIdCity & ",'" & mynIdCar & "'," & InstanceSoftwareUserService.SystemUserId & ",'" & _R2DateTimeService.DateTimeServ.GetCurrentDateShamsiFull & "'," & NativenessType & ",'',1)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
                Return mynIdCar
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then
                    CmdSql.Transaction.Rollback() : CmdSql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class


End Namespace
