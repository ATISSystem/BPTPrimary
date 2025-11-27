

Imports R2Core.ComputersManagement
Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.RFIDCardsManagement.Exceptions
Imports R2Core.SoftwareUserManagement
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Reflection
Imports System.Windows.Forms

Namespace RFIDCardsManagement

    Public Class R2CoreStandardRFIDCardStructure
        Inherits BaseStandardClass.R2StandardStructure

        Private _CardId As String
        Private _CardNo As String
        Private _Active As Boolean
        Private _DateTimeMilladiSabt As DateTime
        Private _DateTimeMilladiEdit As DateTime
        Private _DateShamsiSabt As String
        Private _DateShamsiEdit As String



#Region "Constructing Management"
        Public Sub New()
            MyBase.New()
            _CardId = "" : _CardNo = "" : _Active = False : _DateTimeMilladiSabt = "" : _DateTimeMilladiEdit = "" : _DateShamsiSabt = "" : _DateShamsiEdit = ""
        End Sub
        Public Sub New(ByVal CardIdd As String, ByVal CardNoo As String, ByVal Activee As Boolean, ByVal DateTimeMilladiSabtt As DateTime, ByVal DateTimeMilladiEditt As DateTime, ByVal DateShamsiSabtt As String, ByVal DateShamsiEditt As String)
            MyBase.New(CardIdd, CardNoo)
            _CardId = CardIdd
            _CardNo = CardNoo
            _Active = Activee
            _DateTimeMilladiSabt = DateTimeMilladiSabtt
            _DateTimeMilladiEdit = DateTimeMilladiEditt
            _DateShamsiSabt = DateShamsiSabtt
            _DateShamsiEdit = DateShamsiEditt
        End Sub
#End Region
#Region "Properting Management"
        Public Property CardId() As String
            Get
                Return _CardId
            End Get
            Set(ByVal Value As String)
                _CardId = Value
            End Set
        End Property
        Public Property CardNo() As String
            Get
                Return _CardNo
            End Get
            Set(ByVal Value As String)
                _CardNo = Value
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
        Public Property DateTimeMilladiSabt() As DateTime
            Get
                Return _DateTimeMilladiSabt
            End Get
            Set(ByVal Value As DateTime)
                _DateTimeMilladiSabt = Value
            End Set
        End Property
        Public Property DateTimeMilladiEdit() As DateTime
            Get
                Return _DateTimeMilladiEdit
            End Get
            Set(ByVal Value As DateTime)
                _DateTimeMilladiEdit = Value
            End Set
        End Property
        Public Property DateShamsiSabt() As String
            Get
                Return _DateShamsiSabt
            End Get
            Set(ByVal Value As String)
                _DateShamsiSabt = Value
            End Set
        End Property
        Public Property DateShamsiEdit() As String
            Get
                Return _DateShamsiEdit
            End Get
            Set(ByVal Value As String)
                _DateShamsiEdit = Value
            End Set
        End Property
#End Region


    End Class

    Public Class R2CoreRFIDCardReader
        Public Declare Function MF_GetDLL_Ver Lib "MF_API.dll" (ByRef rVER As Byte) As Short
        Public Declare Function MF_InitComm Lib "MF_API.dll" (ByVal portname As String, ByVal baud As Integer) As Integer
        Public Declare Function MF_ControlBuzzer Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef BeepTime As Short) As Integer
        Public Declare Function MF_DeviceReset Lib "MF_API.dll" (ByVal DeviceAddr As Short) As Integer
        Public Declare Function MF_ExitComm Lib "MF_API.dll" () As Integer
        Public Declare Function MF_GetDevice_Ver Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef ver As Byte) As Integer
        Public Declare Function MF_SetDeviceBaud Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal baud As Integer) As Integer
        Public Declare Function MF_SetDeviceAddr Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal addr As Short) As Integer
        Public Declare Function MF_ControlLED Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal LED1 As Short, ByVal LED2 As Short) As Integer
        Public Declare Function MF_GetDeviceAddr Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef addr As Byte) As Integer
        Public Declare Function MF_SetDeviceSNR Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal snr As String) As Integer
        Public Declare Function MF_GetDeviceSNR Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef snr As Byte) As Integer
        Public Declare Function MF_SetRF_ON Lib "MF_API.dll" (ByVal DeviceAddr As Short) As Integer
        Public Declare Function MF_SetRF_OFF Lib "MF_API.dll" (ByVal DeviceAddr As Short) As Integer
        Public Declare Function MF_SetWiegandMode Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal mode As Short, ByVal alarm As Short) As Integer
        '''''''''''''''''''''''''''''''''''card reading functions''''''''''''''''''''''''''''''''''''''''''
        Public Declare Function MF_Request Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal mode As Short, ByRef CardType As Byte) As Integer
        Public Declare Function MF_Anticoll Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef snr As Byte) As Integer
        Public Declare Function MF_Halt Lib "MF_API.dll" (ByVal DeviceAddr As Short) As Integer
        Public Declare Function MF_Select Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef snr As Byte) As Integer
        Public Declare Function MF_LoadKey Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByRef key As Byte) As Integer

        Public Declare Function MF_LoadKeyFromEE Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal KeyType As Short, ByVal KeyNum As Short) As Integer
        Public Declare Function MF_StoreKeyToEE Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal KeyAB As Short, ByVal KeyAdd As Short, ByRef key As Byte) As Integer
        Public Declare Function MF_Authentication Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal AuthType As Short, ByVal block As Short, ByRef snr As Byte) As Integer
        Public Declare Function MF_Read Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal block As Short, ByVal numbers As Short, ByRef databuff As Byte) As Integer
        Public Declare Function MF_Write Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal block As Short, ByVal numbers As Short, ByRef databuff As Byte) As Integer
        Public Declare Function MF_Value Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal valoption As Short, ByRef value As Byte) As Integer
        Public Declare Function MF_transfer Lib "MF_API.dll" (ByVal DeviceAddr As Short, ByVal block As Short) As Integer

        Public DLL_version(32) As Byte
        Public portN(3) As Byte
        Public Dver(32) As Byte
        Public Daddress As Byte
        Public Dsn(7) As Byte
        Public cardT(2) As Byte
        Public cardSN(3) As Byte
        Public Ckey(5) As Byte
        Public databuffer(16) As Byte
        Public value(3) As Byte
        Public Dbuffer(63) As Byte
        Public Function hex2dec(ByVal inpt As String) As Short
            'On Error Resume Next
            If Len(inpt) = 1 Then inpt = "0" & inpt
            Select Case (Mid(inpt, 1, 1))
                Case "A" : hex2dec = hex2dec + 10 * 16
                Case "a" : hex2dec = hex2dec + 10 * 16
                Case "B" : hex2dec = hex2dec + 11 * 16
                Case "b" : hex2dec = hex2dec + 11 * 16
                Case "C" : hex2dec = hex2dec + 12 * 16
                Case "c" : hex2dec = hex2dec + 12 * 16
                Case "D" : hex2dec = hex2dec + 13 * 16
                Case "d" : hex2dec = hex2dec + 13 * 16
                Case "E" : hex2dec = hex2dec + 14 * 16
                Case "e" : hex2dec = hex2dec + 14 * 16
                Case "F" : hex2dec = hex2dec + 15 * 16
                Case "f" : hex2dec = hex2dec + 15 * 16
                Case Else : hex2dec = hex2dec + CDbl(Mid(inpt, 1, 1)) * 16
            End Select
            Select Case (Mid(inpt, 2, 1))
                Case "A" : hex2dec = hex2dec + 10
                Case "a" : hex2dec = hex2dec + 10
                Case "B" : hex2dec = hex2dec + 11
                Case "b" : hex2dec = hex2dec + 11
                Case "C" : hex2dec = hex2dec + 12
                Case "c" : hex2dec = hex2dec + 12
                Case "D" : hex2dec = hex2dec + 13
                Case "d" : hex2dec = hex2dec + 13
                Case "E" : hex2dec = hex2dec + 14
                Case "e" : hex2dec = hex2dec + 14
                Case "F" : hex2dec = hex2dec + 15
                Case "f" : hex2dec = hex2dec + 15
                Case Else : hex2dec = hex2dec + CDbl(Mid(inpt, 2, 1))
            End Select
        End Function
        '  Declare Function MF_InitComm1 Lib "MF_API.dll" Alias "MF_InitComm" (ByVal portname As String, ByVal baud As Long) As Long
        ' Public Declare Function MF_InitComm Lib "MF_API.dll" (ByVal portname As String, ByVal baud As Integer) As Integer
        'Declare Function MF_ExitComm Lib "MF_API.dll" Alias "MF_ExitComm" () As Long
        ' Declare Function MF_InitComm Lib "MF_API.dll" Alias "MF_InitComm" (ByVal portname As String, ByVal baud As Long) As Long
        ''Private Enum RFIDCardReaderOpenClosePort
        ''    Open = 1
        ''    Close = 2
        ''End Enum

        Public Shared Function ControlLeds(ByVal YourLed1 As Short, ByVal YourLed2 As Short) As Integer
            Return MF_ControlLED(0, YourLed1, YourLed2)
        End Function

        Private Function ControlBuzzer(ByRef BeepTime As Short) As Integer
            Return MF_ControlBuzzer(0, BeepTime)
        End Function

        Public Enum RFType
            None = 0
            RFType1 = 1
            RFType2 = 2
        End Enum
        ''Private Function Open_Close_Port(ByVal OC As RFIDCardReaderOpenClosePort) As Integer
        Private Function Open_Close_Port(ByVal YourRFType As RFType) As Integer
            Dim PortName As String
            MF_ExitComm()
            If YourRFType = RFType.RFType1 Then
                PortName = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.RFIDCardReadersType, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0)
                If (PortName.Trim()).ToUpper() <> "NONE" Then
                    Return MF_InitComm(PortName, 9600)
                End If
            End If
            If YourRFType = RFType.RFType2 Then
                PortName = R2CoreMClassConfigurationOfComputersManagement.GetConfigString(R2CoreConfigurations.RFIDCardReadersType, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 1)
                If (PortName.Trim()).ToUpper() <> "NONE" Then
                    Return MF_InitComm(PortName, 9600)
                End If
            End If
        End Function
        Private Function Request(ByRef CardType As String) As Integer
            Dim myState As Integer = MF_Request(0, 0, cardT(0))
            If myState <> 0 Then
                Return myState
            Else
                CardType = cardT(0) & cardT(1)
                Return 0
            End If
        End Function
        Private Function Anticol(ByRef CardNo As String) As Integer
            Dim myState As Integer = MF_Anticoll(0, cardSN(0))
            If myState = 0 Then
                CardNo = Hex(cardSN(0)) & Hex(cardSN(1)) & Hex(cardSN(2)) & Hex(cardSN(3))
                Return 0
            Else
                Return myState
            End If
        End Function
        Private Function Select_Card() As Integer
            Return MF_Select(0, cardSN(0))
        End Function
        Private Function Load_Key() As Integer
            Ckey(0) = 255 : Ckey(1) = 255 : Ckey(2) = 255 : Ckey(3) = 255 : Ckey(4) = 255 : Ckey(5) = 255
            Return MF_LoadKey(0, Ckey(0))
        End Function
        Private Function Authentication() As Integer
            Return MF_Authentication(0, 0, 0, cardSN(0))
        End Function
        Private Function Read_Block(ByRef BufferBlock As String) As Integer
            Dim myState As Integer = MF_Read(0, 0, 1, databuffer(0))
            If myState = 0 Then
                BufferBlock = Hex(databuffer(0)) & " " & Hex(databuffer(1)) & " " & Hex(databuffer(2)) & " " & Hex(databuffer(3)) & " " & Hex(databuffer(4)) & " " & Hex(databuffer(5)) & " " & Hex(databuffer(6)) & " " & Hex(databuffer(7)) & " " & Hex(databuffer(8)) & " " & Hex(databuffer(9)) & " " & Hex(databuffer(10)) & " " & Hex(databuffer(11)) & " " & Hex(databuffer(12)) & " " & Hex(databuffer(13)) & " " & Hex(databuffer(14)) & " " & Hex(databuffer(15))
                Return 0
            Else
                Return myState
            End If
        End Function
        Private Function Write_Block(ByRef Buf As Byte()) As Integer
            Return MF_Write(0, 0, 1, Buf(0))
        End Function

        Private myRFType As RFType = RFType.None
        Public ReadOnly Property GetRFType As RFType
            Get
                Return myRFType
            End Get
        End Property
        Public Function StartAnticol(ByRef CardNo As String) As Integer
            Dim Status As Integer
            Try
                ''Open_Close_Port(RFIDCardReaderOpenClosePort.Open)
                If Open_Close_Port(RFType.RFType1) = 0 Then
                    myRFType = RFType.RFType1
                    Request(New String("A"))
                    Status = Anticol(CardNo)
                    If Status = 0 Then Return Status
                End If
                ''If Open_Close_Port(RFType.RFType2) = 0 Then
                ''    myRFType = RFType.RFType2
                ''    Request(New String("A"))
                ''    Status = Anticol(CardNo)
                ''    Return Status
                ''End If
                Return -1  'هیچ دستگاهی وجود ندارد یا کارتی خوانده نشد
            Catch ex As Exception
                Throw New Exception("R2RFIDCardReader.StartAnticol()." + ex.Message.ToString)
            End Try
        End Function
        Public Function StartReadBuffer(ByRef myBuffer As String) As Integer
            Try
                ''Open_Close_Port(RFIDCardReaderOpenClosePort.Open)
                Open_Close_Port(RFType.RFType1)
                Request(New String("A"))
                Select_Card()
                Load_Key()
                Authentication()
                Return Read_Block(myBuffer)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        Public Function StartWriteBuffer(ByRef Buffer As Byte()) As Integer
            Try
                ''Open_Close_Port(RFIDCardReaderOpenClosePort.Open)
                Open_Close_Port(RFType.RFType1)
                Request(New String("A"))
                Select_Card()
                Load_Key()
                Authentication()
                Return Write_Block(Buffer)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        Public Function ControlBuzzerOFReader(ByRef BeepTime As Short) As Integer
            Try
                Return ControlBuzzer(BeepTime)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Public Interface R2CoreRFIDCardRequester
        Sub R2RFIDCardReaderStartToRead()
        Sub R2RFIDCardReaded(CardNo As String)
        Sub R2RFIDCardReaderWarning(MessageWarning As String)
    End Interface

    Public MustInherit Class R2CoreRFIDCardReaderInterface

        Public Enum InterfaceMode
            TestForRFIDCardConfirm = 0
            NoTestForRFIDCardConfirm = 1
        End Enum
        Private Shared _Requester As R2CoreRFIDCardRequester = Nothing
        Private Shared WithEvents ReaderTimer As System.Windows.Forms.Timer = New System.Windows.Forms.Timer
        Private Shared RFIDCardReader As R2CoreRFIDCardReader = New R2CoreRFIDCardReader
        Private Shared _CurrentCardNo As String = Nothing
        Private Shared _InterfaceMode As InterfaceMode

        Public Shared Sub DisposeResources()
            ReaderTimer.Stop()
            ReaderTimer.Enabled = False
        End Sub

        Public Shared Sub StartReading(YourRequester As R2CoreRFIDCardRequester, YourInterfaceMode As InterfaceMode)
            Try
                'ذخیره رفرنس درخواست کننده
                _Requester = YourRequester
                _InterfaceMode = YourInterfaceMode
                'اطلاع رسانی به درخواست کننده
                YourRequester.R2RFIDCardReaderStartToRead()
                'شروع تایمر
                ReaderTimer.Enabled = True
                ReaderTimer.Interval = 1000
                ReaderTimer.Start()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Sub StopReading()
            Try
                ReaderTimer.Enabled = False
                ReaderTimer.Stop()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Shared Function StartReadBuffer(ByVal YourBuffer As String) As Integer
            Return RFIDCardReader.StartReadBuffer(YourBuffer)
        End Function

        Public Shared Function StartWriteBuffer(ByVal YourBuffer As String) As Integer
            RFIDCardReader.StartWriteBuffer(System.Text.Encoding.Unicode.GetBytes(YourBuffer))
        End Function

        Public Shared Sub BeepRFIDCardReader(ByVal BeepTime As Short)
            Try
                If R2CoreMClassConfigurationOfComputersManagement.GetConfigBoolean(R2CoreConfigurations.RFIDCardReaderSetting, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 2) = True Then RFIDCardReader.ControlBuzzerOFReader(BeepTime)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Shared Sub ReaderTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ReaderTimer.Tick
            Try
                'شبیه سازی خواندن کارت آراف
                Dim SimulNo As String = R2CoreMClassConfigurationOfComputersManagement.GetConfig(R2CoreConfigurations.RFIDCardReaderSetting, R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId, 0).Trim
                If SimulNo <> "" Then
                    ReaderTimer.Stop()
                    _Requester.R2RFIDCardReaded(SimulNo)
                    Exit Sub
                End If

                If RFIDCardReader.StartAnticol(_CurrentCardNo) = 0 Then
                    ReaderTimer.Stop()

                    Dim InavidRFIDCards As String() = R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.InvalidRFIDCards, 0).Split("-")
                    If InavidRFIDCards.Contains(_CurrentCardNo) Then
                        MessageBox.Show("کابل دستگاه کارت خوان را جدا نموده و مجددا متصل کنید")
                        ReaderTimer.Start()
                    End If

                    If _InterfaceMode = InterfaceMode.TestForRFIDCardConfirm Then
                        If R2CoreMClassRFIDCardManagement.IsRFIDCardNoConfirm(_CurrentCardNo) = True Then
                            _Requester.R2RFIDCardReaded(_CurrentCardNo)
                            Exit Sub
                        Else
                            ReaderTimer.Start()
                            _Requester.R2RFIDCardReaderWarning(R2CoreMClassConfigurationManagement.GetConfigString(R2CoreConfigurations.RFIDCardNotConfirmMessage, 0))
                        End If
                    Else
                        _Requester.R2RFIDCardReaded(_CurrentCardNo)
                        Exit Sub
                    End If
                Else
                End If
            Catch ex As Exception
                ReaderTimer.Stop()
                MessageBox.Show(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message.ToString)
            End Try
        End Sub

        Public Shared Function GetRFType() As R2CoreRFIDCardReader.RFType
            Return RFIDCardReader.GetRFType
        End Function

        Public Shared ReadOnly Property CurrentCardNo As String
            Get
                Return _CurrentCardNo
            End Get
        End Property

        Public Shared Function ControlLeds(ByVal YourLed1 As Short, ByVal YourLed2 As Short) As Integer
            Return RFIDCardReader.ControlLeds(YourLed1, YourLed2)
        End Function


    End Class

    Public Class R2CoreMClassRFIDCardManagement

        Private Shared _DateTimeService As IR2DateTimeService
        Private Shared InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Shared Function IsRFIDCardNoConfirm(ByVal CardNo As String) As Boolean
            Try
                Dim Ds As DataSet = New DataSet() : Dim Da As New SqlDataAdapter
                Da.SelectCommand = New SqlCommand("select Top 1 CardId from R2Primary.dbo.TblRfidCards Where ltrim(rtrim(CardNo))='" & CardNo.Trim() & "' order by Cardid Desc")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                Ds.Tables.Clear()
                If Da.Fill(Ds) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        Public Shared Sub RFIDCardInitialRegister(YourCardNo As String, YourUserNSS As R2CoreStandardSoftwareUserStructure)
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                If IsRFIDCardNoConfirm(YourCardNo) = True Then
                    Throw New Exception("کارت مورد نظر قبلا ثبت شده است")
                End If
                Dim Ds As DataSet = New DataSet() : Dim Da As New SqlDataAdapter
                Da.SelectCommand = New SqlCommand("select Top 1 CardId from R2Primary.dbo.TblRfidCards order by Cast(Cardid as int) Desc")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                Ds.Tables.Clear()
                Dim myCardID As String
                If Da.Fill(Ds) <> 0 Then
                    myCardID = Ds.Tables(0).Rows(0).Item("CardId") + 1
                Else
                    myCardID = "1"
                End If
                Cmdsql.Connection.Open()
                Cmdsql.CommandText = "insert into R2Primary.dbo.tblrfidcards(CardId,CardNo,Charge,UserIdSabt,UserIdEdit,PelakType,Pelak,Serial,NoMoney,Active,CompanyName,NameFamily,Mobile,Address,Tel,Tahvilg,DateTimeMilladiSabt,DateTimeMilladiEdit,DateShamsiSabt,DateShamsiEdit,CardType,TempCardType) values(" & myCardID & ",'" & YourCardNo & "',0," & YourUserNSS.UserId & "," & YourUserNSS.UserId & ",0,'','',0,1,'','','','','','','" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentDateTimeMilladi() & "','" & _DateTimeService.GetCurrentShamsiDate() & "','" & _DateTimeService.GetCurrentShamsiDate() & "',0,0)"
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Connection.Close()
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then Cmdsql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub
        Public Shared Function GetNSSRFIDCard(ByVal YourCardNo As String) As R2CoreStandardRFIDCardStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where ltrim(rtrim(CardNo))='" & YourCardNo & "' Order By CardId Desc", 1, Ds, New Boolean).GetRecordsCount = 0 Then Throw New RFIdCardNotFoundException
                Return New R2CoreStandardRFIDCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"))
            Catch ex As RFIdCardNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        Public Shared Function GetNSSRFIDCard(ByVal YourCardId As Int64) As R2CoreStandardRFIDCardStructure
            Try
                Dim Ds As DataSet
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * from R2Primary.dbo.TblRFIDCards Where ltrim(rtrim(CardId))=" & YourCardId & "", 1, Ds, New Boolean)
                Return New R2CoreStandardRFIDCardStructure(Ds.Tables(0).Rows(0).Item("CardId"), Ds.Tables(0).Rows(0).Item("CardNo"), Ds.Tables(0).Rows(0).Item("Active"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiSabt"), Ds.Tables(0).Rows(0).Item("DateTimeMilladiEdit"), Ds.Tables(0).Rows(0).Item("DateShamsiSabt"), Ds.Tables(0).Rows(0).Item("DateShamsiEdit"))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
        Public Shared Function GetRFIDCardsTeadadReal() As Int64
            Try
                Dim Da As New SqlClient.SqlDataAdapter : Dim Ds As New DataSet
                Da.SelectCommand = New SqlClient.SqlCommand("SELECT COUNT(*) FROM R2Primary.dbo.TblRFIDCards GROUP BY CardNo")
                Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                Ds.Tables.Clear()
                Return Da.Fill(Ds)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class RFIdCardNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "اطلاعات وارد شده نادرست است"
                End Get
            End Property
        End Class

    End Namespace
End Namespace

Namespace RFIDCards

    Public Class R2CoreRFIDCardsManager
        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService

        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function RFIDCardRegistering(YourCardNo As String, YourSoftwareUserId As Int64) As Int64
            Dim Cmdsql As New SqlClient.SqlCommand
            Cmdsql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection()
            Try
                Cmdsql.Connection.Open()
                Cmdsql.Transaction = Cmdsql.Connection.BeginTransaction
                Cmdsql.CommandText = "Select Top 1 CardId From R2Primary.dbo.TblRFIDCards with (tablockx) Order By CardId Desc"
                Cmdsql.ExecuteNonQuery()
                Dim newCardId As Int64 = Cmdsql.ExecuteScalar() + 1
                Cmdsql.CommandText = "Insert Into R2Primary.dbo.TblRFIDCards(CardId,CardNo,UseriDSabt,UserIdEdit) 
                                      values(" & newCardId & ",'" & YourCardNo & "'," & YourSoftwareUserId & "," & YourSoftwareUserId & ")"
                Cmdsql.ExecuteNonQuery()
                Cmdsql.Transaction.Commit() : Cmdsql.Connection.Close()
                Return newCardId
            Catch ex As Exception
                If Cmdsql.Connection.State <> ConnectionState.Closed Then
                    Cmdsql.Transaction.Rollback() : Cmdsql.Connection.Close()
                End If
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class



End Namespace
