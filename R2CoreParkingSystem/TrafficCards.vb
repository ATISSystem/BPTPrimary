


Imports R2Core
Imports R2Core.BaseClasses
Imports R2Core.BaseStandardClass
Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.GeneralConfiguration
Imports R2Core.PublicProcedures
Imports R2Core.RFIDCards
Imports R2Core.SoftwareUserManagement
Imports R2CoreParkingSystem.AccountingManagement
Imports R2CoreParkingSystem.ConfigurationManagement
Imports R2CoreParkingSystem.GeneralConfiguration
Imports R2CoreParkingSystem.MoneyWalletManagement
Imports R2CoreParkingSystem.TrafficCardsManagement.ExceptionManagement
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection

Namespace TrafficCardsManagement

    Public Enum TerafficCardType
        None = 0 : Savari = 1 : Tereili = 2 : TenCharkh = 3 : SixCharkh = 4
    End Enum

    Public Class R2CoreParkingSystemStandardTrafficCardStructure

        Private myCardId As String
        Private myCardNo As String
        Private myCharge As Int64
        Private myUserIdSabt As Byte
        Private myUserIdEdit As Byte
        Private myPelakType As Byte
        Private myPelak As String
        Private mySerial As String
        Private myNoMoney As Boolean
        Private myActive As Boolean
        Private myCompanyName As String
        Private myNameFamily As String
        Private myMobile As String
        Private myAddress As String
        Private myTel As String
        Private myTahvilg As String
        Private myDateTimeMilladiSabt As DateTime
        Private myDateTimeMilladiEdit As DateTime
        Private myDateShamsiSabt As String
        Private myDateShamsiEdit As String
        Private myCardType As TerafficCardType
        Private myTempCardType As TrafficTempCardType



#Region "Constructing Management"
        Public Sub New()
            MyBase.New()
            myCardId = "" : myCardNo = "" : myCharge = 0 : myUserIdSabt = 0 : myUserIdEdit = 0 : myPelakType = 0 : myPelak = ""
            mySerial = "" : myNoMoney = False : myActive = False : myCompanyName = "" : myNameFamily = "" : myMobile = ""
            myAddress = "" : myTel = "" : myTahvilg = "" : myDateTimeMilladiSabt = Now : myDateTimeMilladiEdit = Now : myDateShamsiSabt = "" : myDateShamsiEdit = ""
            myCardType = TerafficCardType.None : myTempCardType = TrafficTempCardType.None
        End Sub
        Public Sub New(ByVal CardIdd As String, ByVal CardNoo As String, ByVal Chargee As Int32, ByVal UserIdSabtt As Byte, ByVal UserIdEditt As Byte, ByVal PelakTypee As Byte, ByVal Pelakk As String, ByVal Seriall As String, ByVal NoMoneyy As Boolean, ByVal Activee As Boolean, ByVal CompanyNamee As String, ByVal NameFamilyy As String, ByVal Mobilee As String, ByVal Addresss As String, ByVal Tell As String, ByVal Tahvilgg As String, ByVal DateTimeMilladiSabtt As DateTime, ByVal DateTimeMilladiEditt As DateTime, ByVal DateShamsiSabtt As String, ByVal DateShamsiEditt As String, ByVal CardTypee As TerafficCardType, ByVal TempCardTypee As TrafficTempCardType)
            MyBase.New()
            myCardId = CardIdd
            myCardNo = CardNoo
            myCharge = Chargee
            myUserIdSabt = UserIdSabtt
            myUserIdEdit = UserIdEditt
            myPelakType = PelakTypee
            myPelak = Pelakk
            mySerial = Seriall
            myNoMoney = NoMoneyy
            myActive = Activee
            myCompanyName = CompanyNamee
            myNameFamily = NameFamilyy
            myMobile = Mobilee
            myAddress = Addresss
            myTel = Tell
            myTahvilg = Tahvilgg
            myDateTimeMilladiSabt = DateTimeMilladiSabtt
            myDateTimeMilladiEdit = DateTimeMilladiEditt
            myDateShamsiSabt = DateShamsiSabtt
            myDateShamsiEdit = DateShamsiEditt
            myCardType = CardTypee
            myTempCardType = TempCardTypee
        End Sub
#End Region
#Region "Properting Management"
        Public Property CardId() As String
            Get
                Return myCardId.Trim
            End Get
            Set(ByVal Value As String)
                myCardId = Value
            End Set
        End Property
        Public Property CardNo() As String
            Get
                Return myCardNo.Trim()
            End Get
            Set(ByVal Value As String)
                myCardNo = Value
            End Set
        End Property
        Public Property Charge() As Int32
            Get
                Return myCharge
            End Get
            Set(ByVal Value As Int32)
                myCharge = Value
            End Set
        End Property
        Public Property UserIdSabt() As Byte
            Get
                Return myUserIdSabt
            End Get
            Set(ByVal Value As Byte)
                myUserIdSabt = Value
            End Set
        End Property
        Public Property UserIdEdit() As Byte
            Get
                Return myUserIdEdit
            End Get
            Set(ByVal Value As Byte)
                myUserIdEdit = Value
            End Set
        End Property
        Public Property PelakType() As Byte
            Get
                Return myPelakType
            End Get
            Set(ByVal Value As Byte)
                myPelakType = Value
            End Set
        End Property
        Public Property Pelak() As String
            Get
                Return myPelak.Trim()
            End Get
            Set(ByVal Value As String)
                myPelak = Value
            End Set
        End Property
        Public Property Serial() As String
            Get
                Return mySerial.Trim()
            End Get
            Set(ByVal Value As String)
                mySerial = Value
            End Set
        End Property
        Public Property NoMoney() As Boolean
            Get
                Return myNoMoney
            End Get
            Set(ByVal Value As Boolean)
                myNoMoney = Value
            End Set
        End Property
        Public Property Active() As Boolean
            Get
                Return myActive
            End Get
            Set(ByVal Value As Boolean)
                myActive = Value
            End Set
        End Property
        Public Property CompanyName() As String
            Get
                Return myCompanyName.Trim()
            End Get
            Set(ByVal Value As String)
                myCompanyName = Value
            End Set
        End Property
        Public Property NameFamily() As String
            Get
                Return myNameFamily.Trim()
            End Get
            Set(ByVal Value As String)
                myNameFamily = Value
            End Set
        End Property
        Public Property Mobile() As String
            Get
                Return myMobile.Trim()
            End Get
            Set(ByVal Value As String)
                myMobile = Value
            End Set
        End Property
        Public Property Address() As String
            Get
                Return myAddress.Trim()
            End Get
            Set(ByVal Value As String)
                myAddress = Value
            End Set
        End Property
        Public Property Tel() As String
            Get
                Return myTel.Trim()
            End Get
            Set(ByVal Value As String)
                myTel = Value
            End Set
        End Property
        Public Property Tahvilg() As String
            Get
                Return myTahvilg.Trim()
            End Get
            Set(ByVal Value As String)
                myTahvilg = Value
            End Set
        End Property
        Public Property DateTimeMilladiSabt() As DateTime
            Get
                Return myDateTimeMilladiSabt
            End Get
            Set(ByVal Value As DateTime)
                myDateTimeMilladiSabt = Value
            End Set
        End Property
        Public Property DateTimeMilladiEdit() As DateTime
            Get
                Return myDateTimeMilladiEdit
            End Get
            Set(ByVal Value As DateTime)
                myDateTimeMilladiEdit = Value
            End Set
        End Property
        Public Property DateShamsiSabt() As String
            Get
                Return myDateShamsiSabt.Trim()
            End Get
            Set(ByVal Value As String)
                myDateShamsiSabt = Value
            End Set
        End Property
        Public Property DateShamsiEdit() As String
            Get
                Return myDateShamsiEdit.Trim()
            End Get
            Set(ByVal Value As String)
                myDateShamsiEdit = Value
            End Set
        End Property
        Public Property CardType As TerafficCardType
            Get
                Return myCardType
            End Get
            Set(ByVal value As TerafficCardType)
                myCardType = value
            End Set
        End Property
        Public Property TempCardType As TrafficTempCardType
            Get
                Return myTempCardType
            End Get
            Set(ByVal value As TrafficTempCardType)
                myTempCardType = value
            End Set
        End Property
#End Region


    End Class

    Public Class R2CoreParkingSystemInstanceTrafficCardsManager
        Private ReadOnly _DateTimeService As R2DateTimeService
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager


        Public Sub New()
            Try
                _DateTimeService = New R2DateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTrafficCard(ByVal YourCardId As Int64) As R2CoreParkingSystemStandardTrafficCardStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where CardId=" & YourCardId & "", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TerraficCardNotFoundException
                Else
                    Return New R2CoreParkingSystemStandardTrafficCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Charge"), Ds.Tables(0).Rows(0).Item("UserIdSabt"), Ds.Tables(0).Rows(0).Item("UserIdEdit"), Ds.Tables(0).Rows(0).Item("PelakType"), Ds.Tables(0).Rows(0).Item("Pelak"), Ds.Tables(0).Rows(0).Item("Serial"), Ds.Tables(0).Rows(0).Item("NoMoney"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("CompanyName"), Ds.Tables(0).Rows(0).Item("NameFamily"), Ds.Tables(0).Rows(0).Item("Mobile"), Ds.Tables(0).Rows(0).Item("Address"), Ds.Tables(0).Rows(0).Item("Tel"), Ds.Tables(0).Rows(0).Item("Tahvilg"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"), Ds.Tables(0).Rows(0).Item("CardType"), Ds.Tables(0).Rows(0).Item("TempCardType"))
                End If
            Catch exx As TerraficCardNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub UpdatingTrafficCard(ByVal YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourEditLevel As R2CoreEnums.EditLevel)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Cmdsql.Connection.Open()
                If YourEditLevel = R2CoreEnums.EditLevel.LowLevel Then
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards Set UserIdEdit=" & YourNSSTerafficCard.UserIdEdit & ",Pelak='" & YourNSSTerafficCard.Pelak & "',Serial='" & YourNSSTerafficCard.Serial & "',CompanyName='" & YourNSSTerafficCard.CompanyName & "',NameFamily='" & YourNSSTerafficCard.NameFamily & "',Mobile='" & YourNSSTerafficCard.Mobile & "',Address='" & YourNSSTerafficCard.Address & "',Tel='" & YourNSSTerafficCard.Tel & "',Tahvilg='" & YourNSSTerafficCard.Tahvilg & "',CardType=" & YourNSSTerafficCard.CardType & ",TempCardType=" & YourNSSTerafficCard.TempCardType & ",DateTimeMilladiEdit='" & _DateTimeService.GetCurrentDateTimeMilladi() & "',DateShamsiEdit='" & _DateTimeService.GetCurrentShamsiDate() & "' Where CardNo='" & YourNSSTerafficCard.CardNo & "'"
                ElseIf YourEditLevel = R2CoreEnums.EditLevel.HighLevel Then
                    Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards Set UserIdEdit=" & YourNSSTerafficCard.UserIdEdit & ",Pelak='" & YourNSSTerafficCard.Pelak & "',Serial='" & YourNSSTerafficCard.Serial & "',NoMoney=" & IIf(YourNSSTerafficCard.NoMoney = True, 1, 0) & ",Active=" & IIf(YourNSSTerafficCard.Active = True, 1, 0) & ",CompanyName='" & YourNSSTerafficCard.CompanyName & "',NameFamily='" & YourNSSTerafficCard.NameFamily & "',Mobile='" & YourNSSTerafficCard.Mobile & "',Address='" & YourNSSTerafficCard.Address & "',Tel='" & YourNSSTerafficCard.Tel & "',Tahvilg='" & YourNSSTerafficCard.Tahvilg & "',CardType=" & YourNSSTerafficCard.CardType & ",TempCardType=" & YourNSSTerafficCard.TempCardType & ",DateTimeMilladiEdit='" & _DateTimeService.GetCurrentDateTimeMilladi() & "',DateShamsiEdit='" & _DateTimeService.GetCurrentShamsiDate() & "' Where CardNo='" & YourNSSTerafficCard.CardNo & "'"
                End If
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNSSTrafficCard(ByVal YourCardNo As String) As R2CoreParkingSystemStandardTrafficCardStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where Cardno='" & YourCardNo & "' Order By ltrim(rtrim(CardId)) Desc", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TerraficCardNotFoundException
                Else
                    Return New R2CoreParkingSystemStandardTrafficCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Charge"), Ds.Tables(0).Rows(0).Item("UserIdSabt"), Ds.Tables(0).Rows(0).Item("UserIdEdit"), Ds.Tables(0).Rows(0).Item("PelakType"), Ds.Tables(0).Rows(0).Item("Pelak"), Ds.Tables(0).Rows(0).Item("Serial"), Ds.Tables(0).Rows(0).Item("NoMoney"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("CompanyName"), Ds.Tables(0).Rows(0).Item("NameFamily"), Ds.Tables(0).Rows(0).Item("Mobile"), Ds.Tables(0).Rows(0).Item("Address"), Ds.Tables(0).Rows(0).Item("Tel"), Ds.Tables(0).Rows(0).Item("Tahvilg"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"), Ds.Tables(0).Rows(0).Item("CardType"), Ds.Tables(0).Rows(0).Item("TempCardType"))
                End If
            Catch ex As TerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Public Class R2CoreParkingSystemMClassTrafficCardManagement
        Private Shared ReadOnly _DateTimeService = New R2DateTimeService()
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        'Public Shared Function IsTerafficCardNoConfirm(ByVal CardNo As String) As Boolean
        '    Try
        '        GetNSSTrafficCard(CardNo)
        '        Return True
        '    Catch exx As GetNSSException
        '        Return False
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Sub TrafficCardSabt(ByRef TrafficCard As R2CoreParkingSystemStandardTrafficCardStructure)
        '    Dim Cmdsql As New SqlClient.SqlCommand
        '    Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '    Try
        '        Cmdsql.Connection.Open()
        '        Cmdsql.CommandText = "insert into R2Primary.dbo.tblrfidcards(CardId,CardNo,Charge,UseriDSabt,UserIdEdit,PelakType,Pelak,Serial,NoMoney,Active,CompanyName,NameFamily,Mobile,Address,Tel,Tahvilg,DateTimeMilladiSabt,DateTimeMilladiEdit,DateShamsiSabt,DateShamsiEdit,CardType,TempCardType) values('" & TrafficCard.CardId & "','" & TrafficCard.CardNo & "'," & TrafficCard.Charge & "," & TrafficCard.UserIdSabt & "," & TrafficCard.UserIdEdit & "," & TrafficCard.PelakType & ",'" & TrafficCard.Pelak & "','" & TrafficCard.Serial & "'," & IIf(TrafficCard.NoMoney = True, 1, 0) & "," & IIf(TrafficCard.Active = True, 1, 0) & ",'" & TrafficCard.CompanyName & "','" & TrafficCard.NameFamily & "','" & TrafficCard.Mobile & "','" & TrafficCard.Address & "','" & TrafficCard.Tel & "','" & TrafficCard.Tahvilg & "','" & TrafficCard.DateTimeMilladiSabt.ToString("yyyy-MM-dd HH:mm:ss") & "','" & TrafficCard.DateTimeMilladiEdit.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "','" & TrafficCard.DateShamsiSabt & "','" & TrafficCard.DateShamsiEdit & "'," & TrafficCard.CardType & "," & TrafficCard.TempCardType & ")"
        '        Cmdsql.ExecuteNonQuery()
        '        Cmdsql.Connection.Close()
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub
        'Public Shared Sub UpdateTrafficCardType(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure)
        '    Dim CmdSql As New SqlCommand
        '    CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '    Try
        '        CmdSql.Connection.Open()
        '        CmdSql.CommandText = "Update R2Primary.dbo.TblRfidCards Set CardType=" & YourNSSTerafficCard.CardType & ",TempCardType=" & YourNSSTerafficCard.TempCardType & " Where CardNo='" & YourNSSTerafficCard.CardNo & "'"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.Connection.Close()
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub
        'Public Shared Sub TerafficCardEdit(ByVal YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourEditLevel As R2Enums.EditLevel)
        '    Dim Cmdsql As New SqlClient.SqlCommand
        '    Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
        '    Try
        '        Cmdsql.Connection.Open()
        '        If YourEditLevel = R2Enums.EditLevel.LowLevel Then
        '            Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards Set UserIdEdit=" & YourNSSTerafficCard.UserIdEdit & ",Pelak='" & YourNSSTerafficCard.Pelak & "',Serial='" & YourNSSTerafficCard.Serial & "',CompanyName='" & YourNSSTerafficCard.CompanyName & "',NameFamily='" & YourNSSTerafficCard.NameFamily & "',Mobile='" & YourNSSTerafficCard.Mobile & "',Address='" & YourNSSTerafficCard.Address & "',Tel='" & YourNSSTerafficCard.Tel & "',Tahvilg='" & YourNSSTerafficCard.Tahvilg & "',CardType=" & YourNSSTerafficCard.CardType & ",TempCardType=" & YourNSSTerafficCard.TempCardType & ",DateTimeMilladiEdit='" & _DateTimeService.GetCurrentDateTimeMilladi() & "',DateShamsiEdit='" & _DateTimeService.GetCurrentShamsiDate() & "' Where CardNo='" & YourNSSTerafficCard.CardNo & "'"
        '        ElseIf YourEditLevel = R2Enums.EditLevel.HighLevel Then
        '            Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards Set UserIdEdit=" & YourNSSTerafficCard.UserIdEdit & ",Pelak='" & YourNSSTerafficCard.Pelak & "',Serial='" & YourNSSTerafficCard.Serial & "',NoMoney=" & IIf(YourNSSTerafficCard.NoMoney = True, 1, 0) & ",Active=" & IIf(YourNSSTerafficCard.Active = True, 1, 0) & ",CompanyName='" & YourNSSTerafficCard.CompanyName & "',NameFamily='" & YourNSSTerafficCard.NameFamily & "',Mobile='" & YourNSSTerafficCard.Mobile & "',Address='" & YourNSSTerafficCard.Address & "',Tel='" & YourNSSTerafficCard.Tel & "',Tahvilg='" & YourNSSTerafficCard.Tahvilg & "',CardType=" & YourNSSTerafficCard.CardType & ",TempCardType=" & YourNSSTerafficCard.TempCardType & ",DateTimeMilladiEdit='" & _DateTimeService.GetCurrentDateTimeMilladi() & "',DateShamsiEdit='" & _DateTimeService.GetCurrentShamsiDate() & "' Where CardNo='" & YourNSSTerafficCard.CardNo & "'"
        '        End If
        '        Cmdsql.ExecuteNonQuery()
        '        Cmdsql.Connection.Close()
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub
        Public Shared Function GetNSSTrafficCard(ByVal CardNo As String) As R2CoreParkingSystemStandardTrafficCardStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where Cardno='" & CardNo & "' Order By CardId Desc", 0, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TerraficCardNotFoundException
                Else
                    Return New R2CoreParkingSystemStandardTrafficCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Charge"), Ds.Tables(0).Rows(0).Item("UserIdSabt"), Ds.Tables(0).Rows(0).Item("UserIdEdit"), Ds.Tables(0).Rows(0).Item("PelakType"), Ds.Tables(0).Rows(0).Item("Pelak"), Ds.Tables(0).Rows(0).Item("Serial"), Ds.Tables(0).Rows(0).Item("NoMoney"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("CompanyName"), Ds.Tables(0).Rows(0).Item("NameFamily"), Ds.Tables(0).Rows(0).Item("Mobile"), Ds.Tables(0).Rows(0).Item("Address"), Ds.Tables(0).Rows(0).Item("Tel"), Ds.Tables(0).Rows(0).Item("Tahvilg"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"), Ds.Tables(0).Rows(0).Item("CardType"), Ds.Tables(0).Rows(0).Item("TempCardType"))
                End If
            Catch exx As TerraficCardNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        'Public Shared Function GetTrafficCardMobile(ByVal CardNo As String) As String
        '    Try
        '        Dim Da As New SqlClient.SqlDataAdapter : Dim ds As New DataSet
        '        Da.SelectCommand = New SqlClient.SqlCommand("select mobile from R2Primary.dbo.tblrfidcards where ltrim(rtrim(cardno))='" & CardNo & "' order by cardno")
        '        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '        ds.Tables.Clear()
        '        If Da.Fill(ds) <> 0 Then
        '            Return ds.Tables(0).Rows(0).Item("mobile").TRIM
        '        Else
        '            Throw New Exception("كارت تردد مورد نظر در سرور يافت نشد")
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        Public Shared Function GetNSSTrafficCard(ByVal YourCardId As Int64) As R2CoreParkingSystemStandardTrafficCardStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where CardId=" & YourCardId & "", 1, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TerraficCardNotFoundException
                Else
                    Return New R2CoreParkingSystemStandardTrafficCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Charge"), Ds.Tables(0).Rows(0).Item("UserIdSabt"), Ds.Tables(0).Rows(0).Item("UserIdEdit"), Ds.Tables(0).Rows(0).Item("PelakType"), Ds.Tables(0).Rows(0).Item("Pelak"), Ds.Tables(0).Rows(0).Item("Serial"), Ds.Tables(0).Rows(0).Item("NoMoney"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("CompanyName"), Ds.Tables(0).Rows(0).Item("NameFamily"), Ds.Tables(0).Rows(0).Item("Mobile"), Ds.Tables(0).Rows(0).Item("Address"), Ds.Tables(0).Rows(0).Item("Tel"), Ds.Tables(0).Rows(0).Item("Tahvilg"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"), Ds.Tables(0).Rows(0).Item("CardType"), Ds.Tables(0).Rows(0).Item("TempCardType"))
                End If
            Catch ex As TerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        'Public Shared Function IsTrafficCardTypeSupported(ByVal CardNo As String) As Boolean
        '    Try
        '        Dim T As R2CoreParkingSystemStandardTrafficCardStructure = GetNSSTrafficCard(CardNo)
        '        Dim A As String() = Split(R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreParkingSystemConfigurations.TerafficCardsTypeSupport, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0), "-")
        '        If A.Contains(T.CardType) = True Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetTrafficCardTypeName(ByVal YourCardNo As String) As String
        '    Dim T As R2CoreParkingSystemStandardTrafficCardStructure = GetNSSTrafficCard(YourCardNo)
        '    Select Case T.CardType
        '        Case TerafficCardType.Savari
        '            Return "سواری"
        '        Case TerafficCardType.SixCharkh
        '            Return "دو محور - شش چرخ"
        '        Case TerafficCardType.TenCharkh
        '            Return "سه محور - ده چرخ"
        '        Case TerafficCardType.Tereili
        '            Return "تريلی"
        '        Case TerafficCardType.None
        '            Return "نامعلوم"
        '    End Select
        'End Function
        'Public Shared Function GetTrafficCardTempTypeName(ByVal CardNo As String) As String
        '    Dim T As R2CoreParkingSystemStandardTrafficCardStructure = GetNSSTrafficCard(CardNo)
        '    Select Case T.TempCardType
        '        Case TrafficTempCardType.Temp
        '            Return "موقت"
        '        Case TrafficTempCardType.NoTemp
        '            Return "دائمی"
        '    End Select
        'End Function
        'Public Shared Function GetTrafficCardsTeadadReal() As Int64
        '    Try
        '        Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
        '        Da.SelectCommand = New SqlClient.SqlCommand("SELECT COUNT(*) FROM R2Primary.dbo.TblRFIDCards GROUP BY CardNo")
        '        Da.SelectCommand.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '        Ds.Tables.Clear()
        '        Return Da.Fill(Ds)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Sub DisallowTerafficCard(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure)
        '    Dim CmdSql As New SqlClient.SqlCommand
        '    CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
        '    Try
        '        CmdSql.Connection.Open()
        '        CmdSql.CommandText = "Update R2Primary.dbo.TblRFIDCards Set NoMoney=0,Active=0 Where Cardno='" & YourNSSTerafficCard.CardNo & "'"
        '        CmdSql.ExecuteNonQuery()
        '        CmdSql.Connection.Close()
        '    Catch ex As Exception
        '        If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub
        'Public Shared Sub TerafficCardInitialRegister(YourNSSTerafficCard As R2CoreParkingSystemStandardTrafficCardStructure, YourUserNSS As R2CoreStandardSoftwareUserStructure)
        '    Try
        '        UpdateTrafficCardType(YourNSSTerafficCard)
        '        'کسر هزینه کارت تردد در صورتی که دائمی باشد و نه موقت
        '        If YourNSSTerafficCard.TempCardType = TrafficTempCardType.NoTemp Then
        '            R2CoreParkingSystemMClassMoneyWalletManagement.ActMoneyWalletNextStatus(YourNSSTerafficCard, BagPayType.MinusMoney, R2CoreMClassConfigurationManagement.GetConfigInt64(R2CoreParkingSystemConfigurations.TariffsMeselanius, 2), R2CoreParkingSystemAccountings.HazinehKart, YourUserNSS)
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Sub
        'Public Shared Function GetDSTerafficCardType() As DataSet
        '    Try
        '        Dim Ds As New DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TypeName,TypeCode from R2PrimaryParkingSystem.dbo.TblTerafficCardType Order by TypeCode", 1000, Ds, New Boolean)
        '        Return Ds
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetTerafficCardTypeNameFromTypeCode(YourTypeCode As Int64) As String
        '    Try
        '        Dim Ds As New DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TypeName from R2PrimaryParkingSystem.dbo.TblTerafficCardType  Where TypeCode=" & YourTypeCode & "", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item(0).trim
        '        Else
        '            Throw New GetDataException
        '        End If
        '    Catch exx As GetDataException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetTerafficCardTypeCodeFromTypeName(YourTypeName As String) As Int64
        '    Try
        '        Dim Ds As New DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TypeCode from R2PrimaryParkingSystem.dbo.TblTerafficCardType  Where ltrim(rtrim(TypeName))='" & YourTypeName.Trim & "'", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item(0)
        '        Else
        '            Throw New GetDataException
        '        End If
        '    Catch exx As GetDataException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetDSTrafficTempCardType() As DataSet
        '    Try
        '        Dim Ds As New DataSet
        '        InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TempTypeName,TempTypeCode from R2PrimaryParkingSystem.dbo.TblTrafficTempCardType Order by TempTypeCode", 1000, Ds, New Boolean)
        '        Return Ds
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetTrafficTempCardTypeNameFromTempTypeCode(YourTempTypeCode As Int64) As String
        '    Try
        '        Dim Ds As New DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TempTypeName from R2PrimaryParkingSystem.dbo.TblTrafficTempCardType  Where TempTypeCode=" & YourTempTypeCode & "", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item(0).trim
        '        Else
        '            Throw New GetDataException
        '        End If
        '    Catch exx As GetDataException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function
        'Public Shared Function GetTrafficTempCardTypeCodeFromTempTypeName(YourTempTypeName As String) As Int64
        '    Try
        '        Dim Ds As New DataSet
        '        If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select TempTypeCode from R2PrimaryParkingSystem.dbo.TblTrafficTempCardType  Where ltrim(rtrim(TempTypeName))='" & YourTempTypeName.Trim & "'", 10, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item(0)
        '        Else
        '            Throw New GetDataException
        '        End If
        '    Catch exx As GetDataException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function


    End Class

    Namespace ExceptionManagement

        Public Class RelatedTerraficCardNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کارت تردد مرتبط یافت نشد"
                End Get
            End Property
        End Class

        Public Class TerraficCardNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "کارت تردد یافت نشد"
                End Get
            End Property
        End Class

        Public Class TrafficCardNotActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "كارت تردد غير فعال است " + vbCrLf + "استفاده از اين كارت ممنوع و غير مجاز است"
                End Get
            End Property
        End Class

        Public Class TrafficCardTypeNotSupportedOnThisComputerException
            Inherits ApplicationException

            Private _Message As String = String.Empty

            Public Sub New(YourMessage As String)
                _Message = YourMessage
            End Sub

            Public Overrides ReadOnly Property Message As String
                Get
                    Return "این نوع کارت تردد در این کامپیوتر پشتيبانی نمی شود" + vbCrLf + _Message
                End Get
            End Property
        End Class

        Public Class TrafficCardNotMatchWithThisGateException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "وضعیت ورود یا خروج کارت با این معبر مطابقت ندارد"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Enum TrafficTempCardType
        None = 0 : NoTemp = 1 : Temp = 2
    End Enum

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficCardType
        Public TrafficCardTypeId As Int64
        Public TrafficCardTypeTitle As String
        Public Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficCard
        Public TrafficCardId As Int64
        Public TrafficCardNo As String
        Public TrafficCardTypeId As Int64
        Public TrafficCardTempTypeId As Int64
        Public NoMoney As Boolean
        Public Active As Boolean
        Public Mobile As String
        Public Recipient As String
        Public Address As String
        Public Tel As String
    End Class

    'BPTChanged
    Public Class R2CoreParkingSystemTrafficCardsManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService
        Public Sub New(YourDateTimeService As IDateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Sub RegisteringTrafficCard(YourTrafficCardNo As String, YourTrafficCardTypeId As Int64, YourTrafficCardTempTypeId As Int16, YourSoftwareUserId As Int64)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Dim InstanceMoneyWallet = New R2CoreParkingSystemMoneyWalletManager(_DateTimeService)
                Dim InstanceGeneralConfiguration = New R2CoreGeneralConfigurationManager(_DateTimeService)
                Dim InstanceRFIDCards = New R2CoreRFIDCardsManager(_DateTimeService)
                Dim newCardId = InstanceRFIDCards.RFIDCardRegistering(YourTrafficCardNo, YourSoftwareUserId)
                Cmdsql.Connection.Open()
                Cmdsql.CommandText = "Update R2Primary.dbo.TblRfidCards Set CardType=@CardType,TempCardType=@TempCardType Where CardId=@CardId"
                Cmdsql.Parameters.Add("@CardType", SqlDbType.BigInt).Value = YourTrafficCardTypeId
                Cmdsql.Parameters.Add("@TempCardType", SqlDbType.BigInt).Value = YourTrafficCardTempTypeId
                Cmdsql.Parameters.Add("@CardId", SqlDbType.BigInt).Value = newCardId
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Connection.Close()
                If YourTrafficCardTempTypeId = TrafficTempCardType.NoTemp Then
                    InstanceMoneyWallet.ActMoneyWalletNextStatus(newCardId, BagPayType.MinusMoney, InstanceGeneralConfiguration.GetInt64Configuration(R2CoreParkingSystemGeneralConfigurations.TerafficAndTerafficCard, 2), R2CoreParkingSystemAccountings.HazinehKart, YourSoftwareUserId)
                End If
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try

        End Sub

        Public Function GetTrafficCard(YourTrafficCardNo As String) As R2CoreParkingSystemTrafficCard
            Return New R2CoreParkingSystemTrafficCard With {.TrafficCardId = 1000, .TrafficCardNo = YourTrafficCardNo, .TrafficCardTypeId = 1, .TrafficCardTempTypeId = 1, .Active = True, .NoMoney = True, .Address = String.Empty, .Mobile = String.Empty, .Recipient = String.Empty, .Tel = String.Empty}
        End Function

        Public Function GetTrafficCard(ByVal YourTrafficCardId As Int64, YourImmadiately As Boolean) As R2CoreParkingSystemTrafficCard
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblRFIDCards Where CardId=" & YourTrafficCardId & "", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New TerraficCardNotFoundException
                Else
                    Return New R2CoreParkingSystemTrafficCard With {.TrafficCardId = Ds.Tables(0).Rows(0).Item("CardId"), .TrafficCardNo = Ds.Tables(0).Rows(0).Item("CardNo"), .TrafficCardTypeId = Ds.Tables(0).Rows(0).Item("CardType"), .TrafficCardTempTypeId = Ds.Tables(0).Rows(0).Item("TempCardType"), .Active = Ds.Tables(0).Rows(0).Item("Active"), .NoMoney = Ds.Tables(0).Rows(0).Item("NoMoney"), .Address = Ds.Tables(0).Rows(0).Item("Address"), .Mobile = Ds.Tables(0).Rows(0).Item("Mobile"), .Recipient = Ds.Tables(0).Rows(0).Item("NameFamily"), .Tel = Ds.Tables(0).Rows(0).Item("Tel")}
                End If
            Catch ex As TerraficCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTrafficCardTypesJSON(YourImmediately As Boolean) As String
            Try
                Dim InstancePublicProcedures = New R2CorePublicProceduresManager()

                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand(
                        "Select TypeId as TrafficCardTypeId,TypeTitle as TrafficCardTypeTitle,Active from R2PrimaryParkingSystem.dbo.TblTrafficCardTypes 
                         Order By TypeId for JSON AUTO")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select TypeId as TrafficCardTypeId,TypeTitle as TrafficCardTypeTitle,Active from R2PrimaryParkingSystem.dbo.TblTrafficCardTypes 
                    Order By TypeId for JSON AUTO", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTrafficCardTempTypesJSON() As String
            Try
                Dim InstancePublicProcedures = New R2CorePublicProceduresManager()

                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                   "Select TempTypeCode as TrafficCardTempTypeId,TempTypeTitle as TrafficCardTempTypeTitle  
                    from R2PrimaryParkingSystem.dbo.TblTrafficTempCardTypes Where Active=1
                    Order By TempTypeCode for JSON AUTO", 32767, DS, New Boolean)
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub EditingTrafficCard(YourRawTrafficCard As R2CoreParkingSystemTrafficCard)

        End Sub

        Public Sub RegisteringTrafficCardType(YourTrafficCardTypeTitle As String)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "Select Top 1 TypeId From R2PrimaryParkingSystem.dbo.TblTrafficCardTypes with (tablockx) Order By TypeId Desc"
                Cmdsql.ExecuteNonQuery()
                Dim newCardTypeId As Int64 = Cmdsql.ExecuteScalar() + 1
                Cmdsql.CommandText = "Insert Into R2PrimaryParkingSystem.dbo.TblTrafficCardTypes(TypeId,TypeTitle,ViewFlag,Active,Deleted) 
                                      Values(@TypeId,@TypeTitle,1,1,0)"
                Cmdsql.Parameters.Add("@TypeId", SqlDbType.BigInt).Value = newCardTypeId
                Cmdsql.Parameters.Add("@TypeTitle", SqlDbType.NVarChar).Value = YourTrafficCardTypeTitle
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End If
            End Try
        End Sub

        Public Sub EditingTrafficCardType(YourRawTrafficCardType As R2CoreParkingSystemTrafficCardType)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.CommandText = "Update R2PrimaryParkingSystem.dbo.TblTrafficCardTypes Set TypeTitle=@TypeTitle,Active=@Active Where TypeId=@TypeId"
                Cmdsql.Parameters.Add("@TypeTitle", SqlDbType.NVarChar).Value = YourRawTrafficCardType.TrafficCardTypeTitle
                Cmdsql.Parameters.Add("@Active", SqlDbType.Bit).Value = IIf(YourRawTrafficCardType.Active, 1, 0)
                Cmdsql.Parameters.Add("@TypeId", SqlDbType.BigInt).Value = YourRawTrafficCardType.TrafficCardTypeId
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Connection.Close()
            Catch ex As SqlException
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub



    End Class


End Namespace

