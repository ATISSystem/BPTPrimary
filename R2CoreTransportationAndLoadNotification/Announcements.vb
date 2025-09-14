
Imports System.Data.SqlClient
Imports System.Reflection

Imports R2Core.DateAndTimeManagement
Imports R2Core.BaseStandardClass
Imports R2Core.ComputersManagement
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2CoreTransportationAndLoadNotification.Announcements
Imports R2CoreTransportationAndLoadNotification.Announcements.Exceptions
Imports R2CoreTransportationAndLoadNotification.ConfigurationsManagement
Imports R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad
Imports R2CoreGUI
Imports R2Core.DateTimeProvider


Namespace Announcements

    Public MustInherit Class Announcements
        Public Shared ReadOnly Property None = 0
        Public Shared ReadOnly Property General = 1
        Public Shared ReadOnly Property Zobi = 2
        Public Shared ReadOnly Property Anbari = 3
        Public Shared ReadOnly Property Otaghdar = 4
        Public Shared ReadOnly Property Shahri = 5
    End Class

    Public MustInherit Class AnnouncementsubGroups
        Public Shared ReadOnly Property None = 0
        Public Shared ReadOnly Property Khavar = 1
        Public Shared ReadOnly Property Otaghdar6 = 2
        Public Shared ReadOnly Property Otaghdar10 = 3
        Public Shared ReadOnly Property Anbari = 4
        Public Shared ReadOnly Property AnbariShemsh = 5
        Public Shared ReadOnly Property AnbariSaderati = 6
        Public Shared ReadOnly Property Zobi = 7
        Public Shared ReadOnly Property ZobiShemsh = 8
        Public Shared ReadOnly Property ZobiSaderati = 9
        Public Shared ReadOnly Property AnbarRol = 10
        Public Shared ReadOnly Property ZobiRol = 11
        Public Shared ReadOnly Property Shahri = 12
        Public Shared ReadOnly Property ShahriRol = 13
        Public Shared ReadOnly Property WareHouseOutShahri = 14
        Public Shared ReadOnly Property WareHouseShahri = 15

    End Class

    Public MustInherit Class AnnouncementHallAnnounceTimeTypes
        Public Shared ReadOnly Property None = 0
        Public Shared ReadOnly Property LastAnnounceLoads = 1
        Public Shared ReadOnly Property UnAnnounceLoads = 2
        Public Shared ReadOnly Property SedimentedLoads = 3
        Public Shared ReadOnly Property AllOfLoadsWithoutSedimentedLoads = 4

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure

        Public Sub New()
            MyBase.New()
            _AHId = 0
            _AHTitle = String.Empty
            _AHColor = String.Empty
            _ViewFlag = False
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourAHId As Int64, YourAHTitle As String, YourAHColor As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _AHId = YourAHId
            _AHTitle = YourAHTitle
            _AHColor = YourAHColor
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub


        Public Property AHId As Int64
        Public Property AHTitle As String
        Public Property AHColor As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure

        Public Sub New()
            MyBase.New()
            _AHSGId = 0
            _AHSGTitle = String.Empty
            _ViewFlag = False
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourAHSGId As Int64, YourAHSGTitle As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            _AHSGId = YourAHSGId
            _AHSGTitle = YourAHSGTitle
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub


        Public Property AHSGId As Int64
        Public Property AHSGTitle As String
        Public Property ViewFlag() As Boolean
        Public Property Active() As Boolean
        Public Property Deleted() As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _AHATTypeId = Int64.MinValue
            _AHATTypeTitle = String.Empty
            _AHATTypeColor = String.Empty
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourAHATTypeId As Int64, YourAHATTypeTitle As String, YourAHATTypeColor As String, YourDeleted As Boolean)
            _AHATTypeId = YourAHATTypeId
            _AHATTypeTitle = YourAHATTypeTitle
            _AHATTypeColor = YourAHATTypeColor
            _Deleted = YourDeleted
        End Sub


        Public Property AHATTypeId As Int64
        Public Property AHATTypeTitle As String
        Public Property AHATTypeColor As String
        Public Property Deleted As Boolean

    End Class

    Public Class R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure

        Public Sub New()
            MyBase.New()
            _NSSAnnounementHall = Nothing
            _NSSAnnouncementsubGroup = Nothing
        End Sub

        Public Sub New(ByVal YourNSSAnnounementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure, YourNSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            _NSSAnnounementHall = YourNSSAnnounementHall
            _NSSAnnouncementsubGroup = YourNSSAnnouncementsubGroup
        End Sub

        Public Property NSSAnnounementHall As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
        Public Property NSSAnnouncementsubGroup As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure

    End Class


    Public Class R2CoreTransportationAndLoadNotificationAnnouncementSubGroup
        Public Property AnnouncementSGId As Int64
        Public Property AnnouncementSGTitle As String
        Public Property Active As Boolean
    End Class

    Public Class R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
        Private _DateTime As New R2DateTime
        Private InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Function GetAnnouncemenetHallLastAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Try
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAHId, YourAHSGId)
                If CurrentTime < AnnounceTimes(0) Then Return New R2CoreDateAndTime With {.Time = "00:00:00"}
                If CurrentTime >= AnnounceTimes(AnnounceTimes.Count - 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(AnnounceTimes.Count - 1)}
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If CurrentTime >= AnnounceTimes(Loopx) And CurrentTime < AnnounceTimes(Loopx + 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(Loopx)}
                    Else
                        Throw New Exception("تنظیمات زمان اعلام بار را در پیکربندی سیستم کنترل نمایید")
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementHallAnnounceTimes(YourAHId As Int64, YourAHSGId As Int64) As List(Of String)
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return AllAnnounceTimesofAnnouncementsubGroup.ToList
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementsubGroup(YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups Where AHSGId=" & YourAHSGId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementsubGroupNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure(DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("AHSGTitle").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch ex As AnnouncementsubGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementHall(YourAHId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements Where AHId=" & YourAHId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementHallNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHTitle").trim, DS.Tables(0).Rows(0).Item("AHColor").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementHallFirstAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return New R2CoreDateAndTime With {.Time = AllAnnounceTimesofAnnouncementsubGroup(0)}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsAnnouncemenetHallAnnounceTimePassed(YourAHId As Int64, YourAHSGId As Int64, YourDateTime As R2CoreDateAndTime) As Boolean
            Try
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAHId, YourAHSGId)
                If YourDateTime.Time < AnnounceTimes(0) Then If CurrentTime < AnnounceTimes(0) Then Return False Else Return True
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If YourDateTime.Time >= AnnounceTimes(Loopx) And YourDateTime.Time < AnnounceTimes(Loopx + 1) Then
                            If CurrentTime < AnnounceTimes(Loopx + 1) Then Return False Else Return True
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementHallfromLoadCapacitorLoad(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Return GetNSSAnnouncementHallByLoaderTypeId(YourNSSLoadCapacitorLoad.nTruckType)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementHallLeastAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return New R2CoreDateAndTime With {.Time = AllAnnounceTimesofAnnouncementsubGroup(AllAnnounceTimesofAnnouncementsubGroup.Count - 1)}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementsAnnouncementsubGroupsJOINT() As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure)

            Try
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                 Select AHs.AHId,AHSGs.AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs
                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHs.AHId=AHRAHSG.AHId 
                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On AHRAHSG.AHSGId=AHSGs.AHSGId 
                 Where AHs.Deleted=0 and AHs.ViewFlag=1 and AHRAHSG.RelationActive=1 and AHSGs.Deleted=0 and AHSGs.ViewFlag=1
                 Order By AHs.AHId,AHSGs.AHSGId", 3600, DS, New Boolean)
                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure)
                For Loopx As Int32 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS = New R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure
                    NSS.NSSAnnounementHall = InstanceAnnouncements.GetNSSAnnouncementHall(Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("AHId")))
                    NSS.NSSAnnouncementsubGroup = InstanceAnnouncements.GetNSSAnnouncementsubGroup(Convert.ToInt64(DS.Tables(0).Rows(Loopx).Item("AHSGId")))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementHallByAnnouncementsubGroup(YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBox.GetDataBOX(New R2PrimarySqlConnection,
                             "Select Top 1 AH.AHId,AH.AHTitle,AH.AHColor,AH.Active,AH.Deleted,AH.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AH.AHId=AHRAHSG.AHId
                                 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHRAHSG.AHSGId=AHSG.AHSGId
                                Where AHSG.AHSGId = " & YourAHSGId & " and AHSG.Deleted=0 and AHSG.ViewFlag=1 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and AH.ViewFlag=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementHallNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHTitle").trim, DS.Tables(0).Rows(0).Item("AHColor").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsThisAnnounceTimeLeastAnnounceTime(YourAHId As String, YourAHSGId As String, YourTime As String) As Boolean
            Try
                Dim LastATime = GetAnnouncementHallLeastAnnounceTime(YourAHId, YourAHSGId).Time
                If LastATime = YourTime Then Return True
                Return False
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncements() As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                 "Select AH.AHId,AH.AHTitle,AH.AHColor,AH.Active,AH.Deleted,AH.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationComputers as AHRComp On AH.AHId=AHRComp.AHId
                                    Where AHRComp.ComId=" & R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId & " and AHRComp.RelationActive=1 and AH.Deleted=0 and ah.ViewFlag=1 ", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHTitle").trim, DS.Tables(0).Rows(Loopx).Item("AHColor").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementsubGroups(YourAHId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                  "Select AHSG.AHSGId,AHSG.AHSGTitle,AHSG.Active,AHSG.Deleted,AHSG.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AH.AHId=AHRAHSG.AHId
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHRAHSG.AHSGId=AHSG.AHSGId
                                   Where AH.AHId = " & YourAHId & " and AHSG.Deleted=0 and AHSG.ViewFlag=1 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and AH.ViewFlag=1", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure(DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("AHSGTitle").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementsubGroupByLoaderTypeId(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                              Select Top 1 AHSG.AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LT 
                                 inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationLoaderTypes as AHSGRLT On LT.LoaderTypeId=AHSGRLT.LoaderTypeId
                                 inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHSGRLT.AHSGId=AHSG.AHSGId
                              Where LT.Deleted=0 and AHSGRLT.RelationActive=1 and AHSG.Deleted=0 and LT.LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoaderTypeRelationAnnouncementsubGroupNotFoundException
                Return GetNSSAnnouncementsubGroup(Convert.ToInt64(DS.Tables(0).Rows(0).Item("AHSGId")))
            Catch exx As LoaderTypeRelationAnnouncementsubGroupNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetNSSAnnouncementHallByLoaderTypeId(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                           Select Top 1 AH.AHId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LT 
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationLoaderTypes as AHSGRLT On LT.LoaderTypeId=AHSGRLT.LoaderTypeId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHSGRLT.AHSGId=AHSG.AHSGId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                           Where LT.Deleted=0 and AHSGRLT.RelationActive=1 and AHSG.Deleted=0 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and LT.LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoaderTypeRelationAnnouncementHallNotFoundException
                Return GetNSSAnnouncementHall(Convert.ToInt64(DS.Tables(0).Rows(0).Item("AHId")))
            Catch exx As LoaderTypeRelationAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class


    Public MustInherit Class R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement
        Private Shared _DateTime As New R2DateTime
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager

        Public Shared Function GetAnnouncementsAnnouncementsubGroupsJOINT() As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                 Select AHs.AHId,AHSGs.AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AHs
                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHs.AHId=AHRAHSG.AHId 
                    Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSGs On AHRAHSG.AHSGId=AHSGs.AHSGId 
                 Where AHs.Deleted=0 and AHs.ViewFlag=1 and AHRAHSG.RelationActive=1 and AHSGs.Deleted=0 and AHSGs.ViewFlag=1
                 Order By AHs.AHId,AHSGs.AHSGId", 3600, DS, New Boolean)

                Dim Lst = New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure)
                For Loopx As Int32 = 0 To DS.Tables(0).Rows.Count - 1
                    Dim NSS = New R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnouncementsubGroupJOINTStructure
                    NSS.NSSAnnounementHall = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementHall(DS.Tables(0).Rows(Loopx).Item("AHId"))
                    NSS.NSSAnnouncementsubGroup = R2CoreTransportationAndLoadNotificationMClassAnnouncementsManagement.GetNSSAnnouncementsubGroup(DS.Tables(0).Rows(Loopx).Item("AHSGId"))
                    Lst.Add(NSS)
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementHall(YourAHId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements Where AHId=" & YourAHId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementHallNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHTitle").trim, DS.Tables(0).Rows(0).Item("AHColor").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementHallByAnnouncementsubGroup(YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                             "Select Top 1 AH.AHId,AH.AHTitle,AH.AHColor,AH.Active,AH.Deleted,AH.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AH.AHId=AHRAHSG.AHId
                                 Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHRAHSG.AHSGId=AHSG.AHSGId
                                Where AHSG.AHSGId = " & YourAHSGId & " and AHSG.Deleted=0 and AHSG.ViewFlag=1 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and AH.ViewFlag=1", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementHallNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(0).Item("AHId"), DS.Tables(0).Rows(0).Item("AHTitle").trim, DS.Tables(0).Rows(0).Item("AHColor").trim, DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementsubGroup(YourAHSGId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups Where AHSGId=" & YourAHSGId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementsubGroupNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure(DS.Tables(0).Rows(0).Item("AHSGId"), DS.Tables(0).Rows(0).Item("AHSGTitle"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementsubGroupNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementHallAnnounceTimeType(YourAHATTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 * from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementHallAnnounceTimeTypes Where AHATTypeId=" & YourAHATTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New AnnouncementHallAnnounceTimeTypeNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure(DS.Tables(0).Rows(0).Item("AHATTypeId"), DS.Tables(0).Rows(0).Item("AHATTypeTitle"), DS.Tables(0).Rows(0).Item("AHATTypeColor"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As AnnouncementHallAnnounceTimeTypeNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementHallByLoaderTypeId(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                           Select Top 1 AH.AHId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LT 
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationLoaderTypes as AHSGRLT On LT.LoaderTypeId=AHSGRLT.LoaderTypeId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHSGRLT.AHSGId=AHSG.AHSGId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AHSG.AHSGId=AHRAHSG.AHSGId
                            inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH On AHRAHSG.AHId=AH.AHId
                           Where LT.Deleted=0 and AHSGRLT.RelationActive=1 and AHSG.Deleted=0 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and LT.LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoaderTypeRelationAnnouncementHallNotFoundException
                Return GetNSSAnnouncementHall(DS.Tables(0).Rows(0).Item("AHId"))
            Catch exx As LoaderTypeRelationAnnouncementHallNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSAnnouncementsubGroupByLoaderTypeId(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                              Select Top 1 AHSG.AHSGId from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LT 
                                 inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroupsRelationLoaderTypes as AHSGRLT On LT.LoaderTypeId=AHSGRLT.LoaderTypeId
                                 inner join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHSGRLT.AHSGId=AHSG.AHSGId
                              Where LT.Deleted=0 and AHSGRLT.RelationActive=1 and AHSG.Deleted=0 and LT.LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoaderTypeRelationAnnouncementsubGroupNotFoundException
                Return GetNSSAnnouncementsubGroup(DS.Tables(0).Rows(0).Item("AHSGId"))
            Catch exx As LoaderTypeRelationAnnouncementsubGroupNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallfromLoadCapacitorLoad(YourNSSLoadCapacitorLoad As R2CoreTransportationAndLoadNotificationStandardLoadCapacitorLoadStructure) As R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure
            Try
                Return GetNSSAnnouncementHallByLoaderTypeId(YourNSSLoadCapacitorLoad.nTruckType)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallLeastAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return New R2CoreDateAndTime With {.Time = AllAnnounceTimesofAnnouncementsubGroup(AllAnnounceTimesofAnnouncementsubGroup.Count - 1)}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallFirstAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return New R2CoreDateAndTime With {.Time = AllAnnounceTimesofAnnouncementsubGroup(0)}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallAnnounceTimes(YourAHId As Int64, YourAHSGId As Int64) As List(Of String)
            Try
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return AllAnnounceTimesofAnnouncementsubGroup.ToList
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallAnnounceTimes(YourAHId As Int64) As List(Of String)
            Try
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAHId), ";")
                Return AllAnnounceTimesofAnnouncementHall.ToList
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsAnnouncemenetHallAnnounceTimePassed(YourAHId As Int64, YourAHSGId As Int64, YourDateTime As R2CoreDateAndTime) As Boolean
            Try
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAHId, YourAHSGId)
                If YourDateTime.Time < AnnounceTimes(0) Then If CurrentTime < AnnounceTimes(0) Then Return False Else Return True
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If YourDateTime.Time >= AnnounceTimes(Loopx) And YourDateTime.Time < AnnounceTimes(Loopx + 1) Then
                            If CurrentTime < AnnounceTimes(Loopx + 1) Then Return False Else Return True
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncemenetHallLastAnnounceTime(YourAHId As Int64, YourAHSGId As Int64) As R2CoreDateAndTime
            Try
                Dim CurrentTime As String = _DateTime.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAHId, YourAHSGId)
                If CurrentTime < AnnounceTimes(0) Then Return New R2CoreDateAndTime With {.Time = "00:00:00"}
                If CurrentTime >= AnnounceTimes(AnnounceTimes.Count - 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(AnnounceTimes.Count - 1)}
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If CurrentTime >= AnnounceTimes(Loopx) And CurrentTime < AnnounceTimes(Loopx + 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(Loopx)}
                    Else
                        Throw New Exception("تنظیمات زمان اعلام بار را در پیکربندی سیستم کنترل نمایید")
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncements() As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                 "Select AH.AHId,AH.AHTitle,AH.AHColor,AH.Active,AH.Deleted,AH.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                  Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationComputers as AHRComp On AH.AHId=AHRComp.AHId
                                    Where AHRComp.ComId=" & R2CoreMClassComputersManagement.GetNSSCurrentComputer.MId & " and AHRComp.RelationActive=1 and AH.Deleted=0 and ah.ViewFlag=1 ", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardAnnouncementstructure(DS.Tables(0).Rows(Loopx).Item("AHId"), DS.Tables(0).Rows(Loopx).Item("AHTitle").trim, DS.Tables(0).Rows(Loopx).Item("AHColor").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementsubGroups(YourAHId As Int64) As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                  "Select AHSG.AHSGId,AHSG.AHSGTitle,AHSG.Active,AHSG.Deleted,AHSG.ViewFlag from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as AH 
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementsubGroups as AHRAHSG On AH.AHId=AHRAHSG.AHId
                                     Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsubGroups as AHSG On AHRAHSG.AHSGId=AHSG.AHSGId
                                   Where AH.AHId = " & YourAHId & " and AHSG.Deleted=0 and AHSG.ViewFlag=1 and AHRAHSG.RelationActive=1 and AH.Deleted=0 and AH.ViewFlag=1", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardAnnouncementsubGroupStructure(DS.Tables(0).Rows(Loopx).Item("AHSGId"), DS.Tables(0).Rows(Loopx).Item("AHSGTitle").trim, DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetAnnouncementHallAnnounceTimeTypes() As List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure)
            Try
                Dim DS As DataSet
                InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection,
                                           "Select AHATType.AHATTypeId,AHATType.AHATTypeTitle,AHATType.AHATTypeColor,AHATType.Deleted from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementHallAnnounceTimeTypes as AHATType 
                                               Where AHATType.Deleted=0 Order By AHATType.AHATTypeId", 3600, DS, New Boolean)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardAnnouncementHallAnnounceTimeTypeStructure(DS.Tables(0).Rows(Loopx).Item("AHATTypeId"), DS.Tables(0).Rows(Loopx).Item("AHATTypeTitle").trim, DS.Tables(0).Rows(Loopx).Item("AHATTypeColor").trim, DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function IsActiveTurnRegisteringIssueControlforAnnouncementHall(YourAHId As Int64, YourAHSGId As Int64) As Boolean
            Try
                Dim IssueControl As String() = Split(R2CoreTransportationAndLoadNotificationMClassConfigurationOfAnnouncementsManagement.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsTurnRegisteringSetting, YourAHId, 0), "-")
                Return Split(IssueControl.Where(Function(x) YourAHSGId = Split(x, ":")(0))(0), ":")(1) = "1"
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Namespace Exceptions
        Public Class LoaderTypeRelationAnnouncementHallNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر با هیچ یک از سالن های اعلام بار مرتبط نیست"
                End Get
            End Property
        End Class

        Public Class LoaderTypeRelationAnnouncementsubGroupNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر با هیچ یک از زیرگروه های سالن های اعلام بار مرتبط نیست"
                End Get
            End Property
        End Class

        Public Class AnnouncementHallNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سالن اعلام بار با اطلاعات مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class AnnouncementsubGroupNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زیرگروه سالن اعلام بار با اطلاعات مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class AnnouncementHallAnnounceTimeTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع زمان اعلام بار با کدشاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class AnnouncementHallNotSelectedException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "سالن اعلام بار مورد نظر انتخاب نشده است"
                End Get
            End Property
        End Class

        Public Class AnnouncementsubGroupUnActiveException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "زیر گروه اعلام بار مورد نظر فعال نیست"
                End Get
            End Property
        End Class

        Public Class HasNotRelationBetweenProvinceAndAnnouncementsubGroup
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مقصد با زیرگروه اعلام بار مطابقت ندارد"
                End Get
            End Property
        End Class

    End Namespace

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationAnnouncement
        Public Property AnnouncementId As Int64
        Public Property AnnouncementTitle As String
        Public Property Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationAnnouncementsManager

        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        'BPTChanged
        Public Function GetAnnouncements(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSqlDataBox = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                      Select Announcements.AnnouncementId,Announcements.AnnouncementTitle,Announcements.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements As Announcements  
                      Where Announcements.AnnouncementTitle Like N'%" & YourSearchString & "%' and Announcements.Deleted=0 
                      for JSON path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select Announcements.AnnouncementId,Announcements.AnnouncementTitle,Announcements.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements As Announcements  
                      Where Announcements.AnnouncementTitle Like N'%" & YourSearchString & "%' and Announcements.Deleted=0 
                      for JSON path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AnnouncementRegistering(YourRawAnnouncement As R2CoreTransportationAndLoadNotificationAnnouncement)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 AnnouncementId From R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements with (tablockx) Order By AnnouncementId Desc"
                Dim AnnouncementIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements(AnnouncementId,AnnouncementTitle,ViewFlag,Active,Deleted)
                                          Values(" & AnnouncementIdNew & ",'" & YourRawAnnouncement.AnnouncementTitle & "',1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
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
        End Sub

        Public Sub AnnouncementEditing(YourRawAnnouncement As R2CoreTransportationAndLoadNotificationAnnouncement)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements Set AnnouncementTitle='" & YourRawAnnouncement.AnnouncementTitle & "',Active=" & IIf(YourRawAnnouncement.Active, 1, 0) & "
                                      Where AnnouncementId=" & YourRawAnnouncement.AnnouncementId & ""

                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AnnouncementDeleting(YourAnnouncementId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements Set Deleted=1,Active=0
                                      Where AnnouncementId=" & YourAnnouncementId & ""
                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetAnnouncementSGs(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSqlDataBox = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                      Select AnnouncementSGs.AnnouncementSGId ,AnnouncementSGs.AnnouncementSGTitle ,AnnouncementSGs.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSGs 
                      Where AnnouncementSGs.AnnouncementSGTitle Like N'%" & YourSearchString & "%' and AnnouncementSGs.Deleted=0 
                      for JSON path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select AnnouncementSGs.AnnouncementSGId ,AnnouncementSGs.AnnouncementSGTitle ,AnnouncementSGs.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSGs 
                      Where AnnouncementSGs.AnnouncementSGTitle Like N'%" & YourSearchString & "%' and AnnouncementSGs.Deleted=0 
                      for JSON path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementSGsByAnnouncementId(YourAnnouncementId As Int64, YourImmediately As Boolean) As String
            Try
                Dim InstanceSqlDataBox = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2Core.PublicProc.R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                      Select AnnouncementSGs.AnnouncementSGId ,AnnouncementSGs.AnnouncementSGTitle ,AnnouncementSGs.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSGs 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSGs.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                      Where Announcements.AnnouncementId=" & YourAnnouncementId & " and AnnouncementSGs.Deleted=0 
                      for JSON path")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select AnnouncementSGs.AnnouncementSGId ,AnnouncementSGs.AnnouncementSGTitle ,AnnouncementSGs.Active 
                      from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSGs 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as AnnouncementsRelationAnnouncementSubGroups On AnnouncementSGs.AnnouncementSGId=AnnouncementsRelationAnnouncementSubGroups.AnnouncementSGId 
                         Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements On AnnouncementsRelationAnnouncementSubGroups.AnnouncementId=Announcements.AnnouncementId 
                      Where Announcements.AnnouncementId=" & YourAnnouncementId & " and AnnouncementSGs.Deleted=0 
                      for JSON path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsThisAnnounceTimeLeastAnnounceTime(YourAnnouncementGroupId As String, YourAnnouncementSGId As String, YourTime As String) As Boolean
            Try
                Dim LastATime = GetAnnouncementLeastAnnounceTime(YourAnnouncementGroupId, YourAnnouncementSGId).Time
                If LastATime = YourTime Then Return True
                Return False
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + "." + ex.Message)
            End Try
        End Function

        Public Sub AnnouncementSGRegistering(YourRawAnnouncementSG As R2CoreTransportationAndLoadNotificationAnnouncementSubGroup)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.Transaction = CmdSql.Connection.BeginTransaction
                CmdSql.CommandText = "Select Top 1 AnnouncementSGId From R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups with (tablockx) Order By AnnouncementSGId Desc"
                Dim AnnouncementSGIdNew As Int64 = CmdSql.ExecuteScalar() + 1
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups(AnnouncementSGId,AnnouncementSGTitle,ViewFlag,Active,Deleted)
                                          Values(" & AnnouncementSGIdNew & ",'" & YourRawAnnouncementSG.AnnouncementSGTitle & "',1,1,0)"
                CmdSql.ExecuteNonQuery()
                CmdSql.Transaction.Commit() : CmdSql.Connection.Close()
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
        End Sub

        Public Sub AnnouncementSGEditing(YourRawAnnouncementSG As R2CoreTransportationAndLoadNotificationAnnouncementSubGroup)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups Set AnnouncementSGTitle='" & YourRawAnnouncementSG.AnnouncementSGTitle & "',Active=" & IIf(YourRawAnnouncementSG.Active, 1, 0) & "
                                      Where AnnouncementSGId=" & YourRawAnnouncementSG.AnnouncementSGId & ""

                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AnnouncementSGDeleting(YourAnnouncementSGId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups Set Deleted=1,Active=0
                                      Where AnnouncementSGId=" & YourAnnouncementSGId & ""

                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetAnnouncementRelationAnnouncementSubGroups(YourAnnouncementId As Int64, YourImmediately As Boolean) As String
            Try
                Dim InstanceSqlDataBox = New R2CoreSqlDataBOXManager
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                         Select Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle
                            from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as ARASG On Announcements.AnnouncementId=ARASG.AnnouncementId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On ARASG.AnnouncementSGId=AnnouncementSubGroups.AnnouncementSGId 
                         Where Announcements.AnnouncementId=" & YourAnnouncementId & "
                         for JSON Auto")
                    Da.SelectCommand.Connection = (New R2PrimarySubscriptionDBSqlConnection).GetConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(New R2PrimarySubscriptionDBSqlConnection,
                     "Select Announcements.AnnouncementId,Announcements.AnnouncementTitle,AnnouncementSubGroups.AnnouncementSGId,AnnouncementSubGroups.AnnouncementSGTitle
                            from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncements as Announcements
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups as ARASG On Announcements.AnnouncementId=ARASG.AnnouncementId 
                              Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroups as AnnouncementSubGroups On ARASG.AnnouncementSGId=AnnouncementSubGroups.AnnouncementSGId 
                      Where Announcements.AnnouncementId=" & YourAnnouncementId & "
                      for JSON Auto", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AnnouncementRelationAnnouncementSubGroupDeleting(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Delete R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups
                                      Where AnnouncementId=" & YourAnnouncementId & " and AnnouncementSGId=" & YourAnnouncementSGId & ""
                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub AnnouncementRelationAnnouncementSubGroupRegistering(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = (New R2PrimarySqlConnection).GetConnection
            Try
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Insert Into R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementsRelationAnnouncementSubGroups(AnnouncementId,AnnouncementSGId)
                                      Values(" & YourAnnouncementId & "," & YourAnnouncementSGId & ")"
                CmdSql.ExecuteNonQuery() : CmdSql.Connection.Close()
            Catch ex As SqlException
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                If CmdSql.Connection.State <> ConnectionState.Closed Then CmdSql.Connection.Close()
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub RelationBetweenProvinceAndAnnouncementsubGroupValidate(YourProvinceId As Int64, YourAnnouncementSGId As Int64)
            Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "
                      Select ProvinceId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroupsRelationProvinces 
                      Where ProvinceId=" & YourProvinceId & " and AnnouncementSGId=" & YourAnnouncementSGId & "", 32767, DS, New Boolean).GetRecordsCount() = 0 Then Throw New HasNotRelationBetweenProvinceAndAnnouncementsubGroup
            Catch ex As HasNotRelationBetweenProvinceAndAnnouncementsubGroup
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetAnnouncementHallAnnounceTimes(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As List(Of String)
            Try
                Dim ComposeSearchString As String = YourAnnouncementSGId.ToString + "="
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAnnouncementId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return AllAnnounceTimesofAnnouncementsubGroup.ToList
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncemenetHallLastAnnounceTime(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As R2CoreDateAndTime
            Try
                Dim CurrentTime As String = _DateTimeService.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAnnouncementId, YourAnnouncementSGId)
                If CurrentTime < AnnounceTimes(0) Then Return New R2CoreDateAndTime With {.Time = "00:00:00"}
                If CurrentTime >= AnnounceTimes(AnnounceTimes.Count - 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(AnnounceTimes.Count - 1)}
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If CurrentTime >= AnnounceTimes(Loopx) And CurrentTime < AnnounceTimes(Loopx + 1) Then Return New R2CoreDateAndTime With {.Time = AnnounceTimes(Loopx)}
                    Else
                        Throw New Exception("تنظیمات زمان اعلام بار را در پیکربندی سیستم کنترل نمایید")
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAnnouncementLeastAnnounceTime(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64) As R2CoreDateAndTime
            Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
            Try
                Dim ComposeSearchString As String = YourAnnouncementSGId.ToString + "="
                Dim AllAnnounceTimesofAnnouncementHall As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementHallAnnounceTime, YourAnnouncementId), ";")
                Dim AllAnnounceTimesofAnnouncementsubGroup = Split(Mid(AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0), ComposeSearchString.Length + 1, AllAnnounceTimesofAnnouncementHall.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0).Length), "-")
                Return New R2CoreDateAndTime With {.Time = AllAnnounceTimesofAnnouncementsubGroup(AllAnnounceTimesofAnnouncementsubGroup.Count - 1)}
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function IsAnnouncemenetAnnounceTimePassed(YourAnnouncementGroupId As Int64, YourAnnouncementSGId As Int64, YourDateTime As R2CoreDateAndTime) As Boolean
            Try
                Dim CurrentTime As String = _DateTimeService.GetCurrentTime()
                Dim AnnounceTimes As List(Of String) = GetAnnouncementHallAnnounceTimes(YourAnnouncementGroupId, YourAnnouncementSGId)
                If YourDateTime.Time < AnnounceTimes(0) Then If CurrentTime < AnnounceTimes(0) Then Return False Else Return True
                For Loopx As Int64 = 0 To AnnounceTimes.Count - 1
                    If Loopx < AnnounceTimes.Count - 1 Then
                        If YourDateTime.Time >= AnnounceTimes(Loopx) And YourDateTime.Time < AnnounceTimes(Loopx + 1) Then
                            If CurrentTime < AnnounceTimes(Loopx + 1) Then Return False Else Return True
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class


End Namespace

Namespace AnnouncementTiming

    Public Enum R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming
        None = 0
        StartLoadAllocationRegistering = 1
        InLoadAllocationRegistering = 2
        StartLoadPermissionRegistering = 3
        InLoadPermissionRegistering = 4
        EndofLoadAllocationRegistering = 5
        InBeforAllProcesses = 6 'قبل از اعلام بار
        InMiddleOfProcesses = 7 'در وسط دو اعلام بار
        InEndOfAllProcesses = 8 'بعد از همه اعلام بارها
        StartAutomaticTurnRegistering = 9 'در 5 ثانیه اول صدور خودکار نوبت ها
        InAutomaticTurnRegistering = 10 'در اسلایس صدور خودکار نوبت ها
        StartSedimenting = 11 'در 5 ثانیه اول رسوب بار
        InSedimenting = 12 'در اسلایس رسوب بار
    End Enum

    Public Class R2CoreTransportationAndLoadNotificationInstanceAnnouncementTimingManager
        Private _DateTime As New R2DateTime

        Public Function IsTimingActive(YourAHId As Int64, YourAHSGId As Int64) As Boolean
            Try
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnouncementsAutomaticProcessesTiming As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsAutomaticProcessesTiming, YourAHId), ";")
                Dim AllAnnouncementsAutomaticProcessesTimingSubGroup As String = AllAnnouncementsAutomaticProcessesTiming.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0)
                Return Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(0)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTiming(YourAHId As Int64, YourAHSGId As Int64, YourTime As String) As R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming
            Try
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceConfigurationOfAnnouncementsManager
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationInstanceAnnouncementsManager

                Dim ComposeSearchString As String = YourAHSGId.ToString + "="
                Dim AllAnnouncementsAutomaticProcessesTiming As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsAutomaticProcessesTiming, YourAHId), ";")
                Dim AllAnnouncementsAutomaticProcessesTimingSubGroup As String = AllAnnouncementsAutomaticProcessesTiming.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0)
                Dim TruckDriverLoadAllocationTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(0)
                Dim LoadPermissionsAutomaticJobTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(1)
                Dim AutomaticTurnRegisteringTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(2)
                Dim SedimentingTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(3)
                Dim AnnouncemenetHallLastAnnounceTime As String = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(YourAHId, YourAHSGId).Time
                Dim O1 As TimeSpan = TimeSpan.Parse(YourTime)
                Dim O2 As TimeSpan = TimeSpan.Parse(AnnouncemenetHallLastAnnounceTime)
                Dim SliceLoadPermissionTiming As Int64 = LoadPermissionsAutomaticJobTiming * 60
                Dim SliceTruckDriverLoadAllocationTiming As Int64 = TruckDriverLoadAllocationTiming * 60
                Dim SliceSedimentingTiming As Int64 = SedimentingTiming * 60
                Dim SliceAutomaticTurnRegisteringTiming As Int64 = AutomaticTurnRegisteringTiming * 60
                'قبل از شروع اعلام بار
                If AnnouncemenetHallLastAnnounceTime = "00:00:00" Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InBeforAllProcesses
                'در 5 ثانیه اول شروع تخصیص بار
                If O1.TotalSeconds >= O2.TotalSeconds And O1.TotalSeconds < O2.TotalSeconds + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering
                'در اسلایس تخصیص بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering
                'در 5 ثانیه اول شروع صدور مجوز 
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadPermissionRegistering
                'در اسلایس صدور مجوز
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering
                'در 5 ثانیه اول شروع صدور نوبت خودکار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartAutomaticTurnRegistering
                'در اسلایس صدور نوبت خودکار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InAutomaticTurnRegistering
                'در 5 ثانیه اول شروع رسوب بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartSedimenting
                'در اسلایس رسوب بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 + SliceSedimentingTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InSedimenting
                'پایان همه فرآیندها
                If InstanceAnnouncements.IsThisAnnounceTimeLeastAnnounceTime(YourAHId, YourAHSGId, AnnouncemenetHallLastAnnounceTime) Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InEndOfAllProcesses
                'در وسط دو اعلام بار قرار داریم البته بعد از تخصیص و صدور مجوز و صدور خودکار نوبت ها
                Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InMiddleOfProcesses
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationAnnouncementTimingManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
        End Sub

        Public Function GetTiming(YourAnnouncementId As Int64, YourAnnouncementSGId As Int64, YourTime As String) As R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming
            Try
                Dim InstanceConfigurationOfAnnouncements = New R2CoreTransportationAndLoadNotificationConfigurationOfAnnouncementsManager(_DateTimeService)
                Dim InstanceAnnouncements = New R2CoreTransportationAndLoadNotificationAnnouncementsManager(_DateTimeService)

                Dim ComposeSearchString As String = YourAnnouncementSGId.ToString + "="
                Dim AllAnnouncementsAutomaticProcessesTiming As String() = Split(InstanceConfigurationOfAnnouncements.GetConfigString(R2CoreTransportationAndLoadNotificationConfigurations.AnnouncementsAutomaticProcessesTiming, YourAnnouncementId), ";")
                Dim AllAnnouncementsAutomaticProcessesTimingSubGroup As String = AllAnnouncementsAutomaticProcessesTiming.Where(Function(x) Mid(x, 1, ComposeSearchString.Length) = ComposeSearchString)(0)
                Dim TruckDriverLoadAllocationTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(0)
                Dim LoadPermissionsAutomaticJobTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(1)
                Dim AutomaticTurnRegisteringTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(2)
                Dim SedimentingTiming As Int64 = Split(AllAnnouncementsAutomaticProcessesTimingSubGroup, "=")(1).Split("-")(1).Split(",")(3)
                Dim AnnouncemenetHallLastAnnounceTime As String = InstanceAnnouncements.GetAnnouncemenetHallLastAnnounceTime(YourAnnouncementId, YourAnnouncementSGId).Time
                Dim O1 As TimeSpan = TimeSpan.Parse(YourTime)
                Dim O2 As TimeSpan = TimeSpan.Parse(AnnouncemenetHallLastAnnounceTime)
                Dim SliceLoadPermissionTiming As Int64 = LoadPermissionsAutomaticJobTiming * 60
                Dim SliceTruckDriverLoadAllocationTiming As Int64 = TruckDriverLoadAllocationTiming * 60
                Dim SliceSedimentingTiming As Int64 = SedimentingTiming * 60
                Dim SliceAutomaticTurnRegisteringTiming As Int64 = AutomaticTurnRegisteringTiming * 60
                'قبل از شروع اعلام بار
                If AnnouncemenetHallLastAnnounceTime = "00:00:00" Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InBeforAllProcesses
                'در 5 ثانیه اول شروع تخصیص بار
                If O1.TotalSeconds >= O2.TotalSeconds And O1.TotalSeconds < O2.TotalSeconds + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadAllocationRegistering
                'در اسلایس تخصیص بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadAllocationRegistering
                'در 5 ثانیه اول شروع صدور مجوز 
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartLoadPermissionRegistering
                'در اسلایس صدور مجوز
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InLoadPermissionRegistering
                'در 5 ثانیه اول شروع صدور نوبت خودکار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartAutomaticTurnRegistering
                'در اسلایس صدور نوبت خودکار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InAutomaticTurnRegistering
                'در 5 ثانیه اول شروع رسوب بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.StartSedimenting
                'در اسلایس رسوب بار
                If O1.TotalSeconds >= O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 And O1.TotalSeconds < O2.TotalSeconds + 5 + SliceTruckDriverLoadAllocationTiming + 5 + SliceLoadPermissionTiming + 5 + SliceAutomaticTurnRegisteringTiming + 5 + SliceSedimentingTiming Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InSedimenting
                'پایان همه فرآیندها
                If InstanceAnnouncements.IsThisAnnounceTimeLeastAnnounceTime(YourAnnouncementId, YourAnnouncementSGId, AnnouncemenetHallLastAnnounceTime) Then Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InEndOfAllProcesses
                'در وسط دو اعلام بار قرار داریم البته بعد از تخصیص و صدور مجوز و صدور خودکار نوبت ها
                Return R2CoreTransportationAndLoadNotificationVirtualAnnouncementTiming.InMiddleOfProcesses
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace
