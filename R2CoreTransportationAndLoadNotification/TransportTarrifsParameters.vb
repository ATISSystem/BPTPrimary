


Imports R2Core.ConfigurationManagement
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.PublicProc
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreTransportationAndLoadNotification.TransportTarrifsParameters.Exceptions
Imports System.Reflection
Imports System.Text

Namespace TransportTarrifsParameters

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersStructure

        Public Sub New()
            MyBase.New()
            _TPTPId = Int64.MinValue
            _TPTPName = String.Empty
            _TPTPTitle = String.Empty
            _TPTPColor = String.Empty
            _Core = String.Empty
            _UserId = Int64.MinValue
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _ViewFlag = Boolean.FalseString
            _Active = Boolean.FalseString
            _Deleted = Boolean.FalseString
        End Sub

        Public Sub New(YourTPTPId As Int64, YourTPTPName As String, YourTPTPTitle As String, YourTPTPColor As String, YourCore As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _TPTPId = YourTPTPId
            _TPTPName = YourTPTPName
            _TPTPTitle = YourTPTPTitle
            _TPTPColor = YourTPTPColor
            _Core = YourCore
            _UserId = YourUserId
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property TPTPId As Int64
        Public Property TPTPName As String
        Public Property TPTPTitle As String
        Public Property TPTPColor As String
        Public Property Core As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure

        Public Sub New()
            MyBase.New()
            _TPTPDId = Int64.MinValue
            _AHSGId = Int64.MinValue
            _TPTPId = Int64.MinValue
            _Mblgh = Int64.MaxValue
            _DateTimeMilladi = DateTime.Now
            _DateShamsi = String.Empty
            _Time = String.Empty
            _RelationActive = Boolean.FalseString
            _Checked = Boolean.FalseString
            _TPTPTitle = String.Empty
        End Sub

        Public Sub New(YourTPTPDId As Int64, YourAHSGId As Int64, YourTPTPId As Int64, YourMblgh As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourTime As String, YourRelationActive As Boolean, YourChecked As Boolean, YourTPTPTitle As String)
            _TPTPDId = YourTPTPDId
            _AHSGId = YourAHSGId
            _TPTPId = YourTPTPId
            _Mblgh = YourMblgh
            _DateTimeMilladi = YourDateTimeMilladi
            _DateShamsi = YourDateShamsi
            _Time = YourTime
            _RelationActive = YourRelationActive
            _Checked = YourChecked
            _TPTPTitle = YourTPTPTitle
        End Sub

        Public Property TPTPDId As Int64
        Public Property AHSGId As Int64
        Public Property TPTPId As Int64
        Public Property Mblgh As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property RelationActive As Boolean
        Public Property Checked As Boolean
        Public Property TPTPTitle As String
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceTransportTarrifsParametersManager

        Public Function GetNSSTransportTarrifsParameterDetail(YourTTPDId As Int64) As R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 TransportPriceTarrifsParameters.TPTPTitle,Details.* from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where Details.TPTPDId=" & YourTTPDId & "", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure(DS.Tables(0).Rows(0).Item("TPTPDId"), DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("TPTPId"), DS.Tables(0).Rows(0).Item("Mblgh"), DS.Tables(0).Rows(0).Item("DateTimeMilladi"), DS.Tables(0).Rows(0).Item("DateShamsi"), DS.Tables(0).Rows(0).Item("Time"), DS.Tables(0).Rows(0).Item("RelationActive"), False, DS.Tables(0).Rows(0).Item("TPTPTitle").trim)
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParams(YourTPTParams As String) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
                If Params(0).Trim = String.Empty Then Return Lst
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTParamDetailId)
                    NSS.Checked = Checked
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParams(YourNSSAHSG As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select TransportPriceTarrifsParameters.TPTPTitle,Details.*,0 as Checked from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AHSGId=" & YourNSSAHSG.AHSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId ", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows().Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardTransportTarrifsParametersDetailsStructure(DS.Tables(0).Rows(Loopx).Item("TPTPDId"), DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("TPTPId"), DS.Tables(0).Rows(Loopx).Item("Mblgh"), DS.Tables(0).Rows(Loopx).Item("DateTimeMilladi"), DS.Tables(0).Rows(Loopx).Item("DateShamsi"), DS.Tables(0).Rows(Loopx).Item("Time"), DS.Tables(0).Rows(Loopx).Item("RelationActive"), DS.Tables(0).Rows(Loopx).Item("Checked"), DS.Tables(0).Rows(Loopx).Item("TPTPTitle")))
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function HaveAnyTransportTarrifsParams(YourNSSAHSG As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure) As Boolean
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select TransportPriceTarrifsParameters.TPTPId from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                          Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AHSGId=" & YourNSSAHSG.AHSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId ", 3600, DS, New Boolean).GetRecordsCount = 0 Then
                    Return False
                End If
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalofTransportTarrifsParameters(YourNSS As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As Int64
            Try
                Dim Total As Int64 = 0
                Dim Params = YourNSS.TPTParams.Split(";")
                If Params(0) = String.Empty Then Return 0
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTPDId As Int64 = Params(Loopx).Split(":")(0)
                    Dim Checked As Boolean = Params(Loopx).Split(":")(1)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTPDId)
                    If Checked Then
                        Total = Total + NSS.Mblgh
                    End If
                Next
                Return Total
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportTarrifsComposit(YourTPTParams As String) As String
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                If Params(0).Trim = String.Empty Then Return String.Empty
                Dim SB = New StringBuilder
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetNSSTransportTarrifsParameterDetail(TPTParamDetailId)
                    If Checked Then SB.AppendLine(NSS.TPTPTitle + " : " + IIf(NSS.Mblgh = 0, "توافقی", NSS.Mblgh.ToString))
                Next
                Return SB.ToString
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function
    End Class

    Namespace Exceptions
        Public Class TransportPriceTarrifParameterDetailsforAHSGNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر موثر در تعرفه حمل مرتبط  با زیرگروه اعلام بار با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class TransportPriceTarrifParameterDetailsNotAdjustedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامترهای موثر در تعرفه حمل به درستی انتخاب و تنظیم نشده اند"
                End Get
            End Property
        End Class

        'BPTChanged
        Public Class TransportPriceTarrifParameterDetailNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "پارامتر موثر در تعرفه حمل با شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class


    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTPTParamsDetails
        Public Property TPTPDId As Int64
        Public Property TPTPTitle As String
        Public Property Cost As Int64
        Public Property Checked As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportTarrifsParametersDetails

        Public Property TPTPDId As Int64
        Public Property AnnouncementSGId As Int64
        Public Property TPTPId As Int64
        Public Property Mblgh As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property Time As String
        Public Property RelationActive As Boolean
        Public Property Checked As Boolean
        Public Property TPTPTitle As String
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationTransportTarrifsParametersManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetListofTransportTarrifsParams(YourTPTParams As String) As List(Of R2CoreTransportationAndLoadNotificationTransportTarrifsParametersDetails)
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationTransportTarrifsParametersDetails)
                If Params(0).Trim = String.Empty Then Return Lst
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetTransportTarrifsParameterDetail(TPTParamDetailId)
                    NSS.Checked = Checked
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportTarrifsParameterDetail(YourTPTPDId As Int64) As R2CoreTransportationAndLoadNotificationTransportTarrifsParametersDetails
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Top 1 TransportPriceTarrifsParameters.TPTPTitle,Details.* from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AnnouncementSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where Details.TPTPDId=" & YourTPTPDId & "", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTransportTarrifsParametersDetails With {.TPTPDId = DS.Tables(0).Rows(0).Item("TPTPDId"), .AnnouncementSGId = DS.Tables(0).Rows(0).Item("AHSGId"), .TPTPId = DS.Tables(0).Rows(0).Item("TPTPId"), .Mblgh = DS.Tables(0).Rows(0).Item("Mblgh"), .DateTimeMilladi = DS.Tables(0).Rows(0).Item("DateTimeMilladi"), .DateShamsi = DS.Tables(0).Rows(0).Item("DateShamsi"), .Time = DS.Tables(0).Rows(0).Item("Time"), .RelationActive = DS.Tables(0).Rows(0).Item("RelationActive"), .TPTPTitle = DS.Tables(0).Rows(0).Item("TPTPTitle").trim, .Checked = False}
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportTarrifsDetail(YourTPTPDId As Int64) As R2CoreTransportationAndLoadNotificationTPTParamsDetails
            Try
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Details.TPTPDId,TransportPriceTarrifsParameters.TPTPTitle,Details.Mblgh as Cost,0 as Checked
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AHSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where Details.TPTPDId=" & YourTPTPDId & "", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationTPTParamsDetails With {.TPTPDId = DS.Tables(0).Rows(0).Item("TPTPDId"), .Cost = DS.Tables(0).Rows(0).Item("Mblgh"), .TPTPTitle = DS.Tables(0).Rows(0).Item("TPTPTitle").trim, .Checked = False}
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTotalofTransportTarrifsParameters(YourLoad As R2CoreTransportationAndLoadNotificationLoad) As Int64
            Try
                Dim Total As Int64 = 0
                Dim Params = YourLoad.TPTParams.Split(";")
                If Params(0) = String.Empty Then Return 0
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTPDId As Int64 = Params(Loopx).Split(":")(0)
                    Dim Checked As Boolean = Params(Loopx).Split(":")(1)
                    Dim NSS = GetTransportTarrifsParameterDetail(TPTPDId)
                    If Checked Then
                        Total = Total + NSS.Mblgh
                    End If
                Next
                Return Total
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTransportTarrifsComposit(YourTPTParams As String) As String
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim Params As String() = YourTPTParams.Split(";")
                If Params(0).Trim = String.Empty Then Return String.Empty
                Dim SB = New StringBuilder
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim NSS = GetTransportTarrifsParameterDetail(TPTParamDetailId)
                    If Checked Then SB.AppendLine(NSS.TPTPTitle + " : " + IIf(NSS.Mblgh = 0, "توافقی", NSS.Mblgh.ToString))
                Next
                Return SB.ToString
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParamsJSON(YourAnnouncementSGId As Int64) As String
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Details.TPTPDId,TransportPriceTarrifsParameters.TPTPTitle,Details.Mblgh as Cost,0 as Checked
                       from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AnnouncementSGId =" & YourAnnouncementSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId for JSON Path", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParamsByAnnouncementSGId(YourAnnouncementSGId As Int64) As List(Of R2CoreTransportationAndLoadNotificationTPTParamsDetails)
            Try
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As DataSet
                Dim InstanceSqlDataBOX = New R2CoreInstanseSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                      "Select Details.TPTPDId,TransportPriceTarrifsParameters.TPTPTitle,Details.Mblgh as Cost,0 as Checked
                       from R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParametersDetails as Details
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AnnouncementsubGroups On Details.AHSGId=AnnouncementsubGroups.AnnouncementSGId 
                            Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblTransportPriceTarrifsParameters as TransportPriceTarrifsParameters On Details.TPTPId=TransportPriceTarrifsParameters.TPTPId 
                       Where AnnouncementsubGroups.AnnouncementSGId =" & YourAnnouncementSGId & " AND AnnouncementsubGroups.Active=1 AND Details.RelationActive=1 AND TransportPriceTarrifsParameters.Active=1 AND TransportPriceTarrifsParameters.Deleted=0
                       Order By TransportPriceTarrifsParameters.TPTPId for JSON Path", 32767, DS, New Boolean).GetRecordsCount = 0 Then
                    Throw New TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                End If
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationTPTParamsDetails)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationTPTParamsDetails With {.TPTPDId = DS.Tables(0).Rows(Loopx).Item("TPTPDId"), .TPTPTitle = DS.Tables(0).Rows(Loopx).Item("TPTPTitle"), .Cost = DS.Tables(0).Rows(Loopx).Item("Cost"), .Checked = False})
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailsforAHSGNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetListofTransportTarrifsParamsforLoad(YourLoaId As Int64) As List(Of R2CoreTransportationAndLoadNotificationTPTParamsDetails)
            ' Standard String : TPTPDId1:0;TPTPDId2:1;.....
            Try
                Dim InstanceLoad = New R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService)
                Dim Load = InstanceLoad.GetLoadSimpleModel(YourLoaId, True)
                Dim Params As String() = Load.TPTParams.Split(";")
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationTPTParamsDetails)
                If Params(0).Trim = String.Empty Then Return Lst
                For Loopx As Int64 = 0 To Params.Length - 1
                    Dim TPTParamDetailId As Int64 = Params(Loopx).Split(":")(0).Trim
                    Dim Checked As Boolean = IIf(Params(Loopx).Split(":")(1).Trim = "1", True, False)
                    Dim Details = GetTransportTarrifsDetail(TPTParamDetailId)
                    Details.Checked = Checked
                    Lst.Add(Details)
                Next
                Return Lst
            Catch ex As TransportPriceTarrifParameterDetailNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace
