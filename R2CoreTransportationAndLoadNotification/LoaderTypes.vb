



Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.SQLInjectionPrevention
Imports R2Core.SQLInjectionPrevention
Imports R2CoreTransportationAndLoadNotification.LoaderTypes.Exceptions
Imports R2CoreTransportationAndLoadNotification.Trucks
Imports R2CoreTransportationAndLoadNotification.Trucks.Exceptions
Imports System.Data.SqlClient
Imports System.Reflection

Namespace LoaderTypes


    Public Class R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            _LoaderTypeId = Int64.MinValue
            _LoaderTypeTitle = String.Empty
            _LoaderTypeOrganizationId = Int64.MinValue
            _LoaderTypeFixStatusId = Int64.MinValue
            _ViewFlag = False
            _Active = False
            _Deleted = False
        End Sub

        Public Sub New(ByVal YourLoaderTypeId As Int64, YourLoaderTypeTitle As String, YourLoaderTypeOrganizationId As Int64, YourLoaderTypeFixStatusId As Int64, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourLoaderTypeId, YourLoaderTypeTitle)
            _LoaderTypeId = YourLoaderTypeId
            _LoaderTypeTitle = YourLoaderTypeTitle
            _LoaderTypeOrganizationId = YourLoaderTypeOrganizationId
            _LoaderTypeFixStatusId = YourLoaderTypeFixStatusId
            _ViewFlag = YourViewFlag
            _Active = YourActive
            _Deleted = YourDeleted
        End Sub

        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property LoaderTypeOrganizationId As Int64
        Public Property LoaderTypeFixStatusId As Int64
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean

    End Class

    Public NotInheritable Class R2CoreTransportationAndLoadNotificationMClassLoaderTypesManagement
        Private Shared _DateTimeService As New R2DateTimeService
        Private Shared InstanceSqlDataBOX As New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetLoaderTypes_SearchforLeftCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where Left(LoaderTypeTitle," & YourSearchString.Length & ")='" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "' and Deleted=0 and Active=1 Order By LoaderTypeTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(Loopx).Item("LoaderTypeId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetLoaderTypes_SearchIntroCharacters(YourSearchString As String) As List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim Lst As New List(Of R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure)
                Dim DS As DataSet = Nothing
                InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where LoaderTypeTitle Like '%" & YourSearchString.Replace("ی", "ي").Replace("ک", "ك") & "%' and Deleted=0 and Active=1 and ViewFlag=1 Order By LoaderTypeTitle", 3600, DS, New Boolean)
                For Loopx As Int64 = 0 To DS.Tables(0).Rows.Count - 1
                    Lst.Add(New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(Loopx).Item("LoaderTypeId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeTitle"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(Loopx).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(Loopx).Item("ViewFlag"), DS.Tables(0).Rows(Loopx).Item("Active"), DS.Tables(0).Rows(Loopx).Item("Deleted")))
                Next
                Return Lst
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Shared Function GetNSSLoaderType(YourLoaderTypeId As Int64) As R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select Top 1 * From R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes Where LoaderTypeId=" & YourLoaderTypeId & "", 3600, DS, New Boolean).GetRecordsCount() = 0 Then Throw New LoadTypeNotFoundException
                Return New R2CoreTransportationAndLoadNotificationStandardLoaderTypeStructure(DS.Tables(0).Rows(0).Item("LoaderTypeId"), DS.Tables(0).Rows(0).Item("LoaderTypeTitle").trim, DS.Tables(0).Rows(0).Item("LoaderTypeOrgnizationId"), DS.Tables(0).Rows(0).Item("LoaderTypeFixStatusId"), DS.Tables(0).Rows(0).Item("ViewFlag"), DS.Tables(0).Rows(0).Item("Active"), DS.Tables(0).Rows(0).Item("Deleted"))
            Catch exx As LoadTypeNotFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function


    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoaderType
        Public Property LoaderTypeId As Int64
        Public Property LoaderTypeTitle As String
        Public Property LoaderTypeOrganizationId As Int64
        Public Property LoaderTypeFixStatusId As Int64
        Public Property LoaderTypeFixStatusTitle As String
        Public Property Active As Boolean
    End Class

    'BPTChanged
    Public Class R2CoreTransportationAndLoadNotificationLoaderTypesManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetLoaderTypes_SearchIntroCharacters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim DS As New DataSet
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeTitle Like N'%" & YourSearchString & "%' and LoaderTypes.Deleted=0 for json path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(DS) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                    "Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeTitle Like N'%" & YourSearchString & "%' and LoaderTypes.Deleted=0 for json path", 3600, DS, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(DS)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoaderType(YourLoaderTypeId As Int64, YourImmediately As Boolean) As R2CoreTransportationAndLoadNotificationLoaderType
            Try
                Dim Ds As New DataSet
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager

                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeId = " & YourLoaderTypeId & " and LoaderTypes.Deleted=0")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "
                     Select LoaderTypes.LoaderTypeId,LoaderTypes.LoaderTypeTitle,LoaderTypes.LoaderTypeOrganizationId,LoaderTypes.LoaderTypeFixStatusId,LoaderTypeFixStatuses.LoaderTypeFixStatusTitle,LoaderTypes.Active
                     from R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes as LoaderTypes
                        Inner Join R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypeFixStatuses as LoaderTypeFixStatuses On LoaderTypes.LoaderTypeFixStatusId=LoaderTypeFixStatuses.LoaderTypeFixStatusId 
                     Where LoaderTypes.LoaderTypeId = " & YourLoaderTypeId & " and LoaderTypes.Deleted=0", 3600, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return New R2CoreTransportationAndLoadNotificationLoaderType With {.LoaderTypeId = Ds.Tables(0).Rows(0).Item("LoaderTypeId"), .LoaderTypeTitle = Ds.Tables(0).Rows(0).Item("LoaderTypeTitle").trim, .LoaderTypeOrganizationId = Ds.Tables(0).Rows(0).Item("LoaderTypeOrganizationId"), .LoaderTypeFixStatusId = Ds.Tables(0).Rows(0).Item("LoaderTypeFixStatusId"), .LoaderTypeFixStatusTitle = Ds.Tables(0).Rows(0).Item("LoaderTypeFixStatusTitle"), .Active = Ds.Tables(0).Rows(0).Item("Active")}
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub LoaderTypeChangeActivate(YourLoaderTypeId As Int64)
            Dim CmdSql As New SqlClient.SqlCommand
            CmdSql.Connection = R2PrimarySqlConnection.GetTransactionDBConnection
            Try
                Dim LoaderType = GetLoaderType(YourLoaderTypeId, True)
                CmdSql.Connection.Open()
                CmdSql.CommandText = "Update R2PrimaryTransportationAndLoadNotification.dbo.TblLoaderTypes 
                                      Set Active=" & IIf(LoaderType.Active, 0, 1) & " Where LoaderTypeId=" & YourLoaderTypeId & ""
                CmdSql.ExecuteNonQuery()
                CmdSql.Connection.Close()
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetLoaderTypeBySoftwareUser(YourSoftwareUserId As Int64) As R2CoreTransportationAndLoadNotificationLoaderType
            Try
                Dim InstanceTrucks = New R2CoreTransportationAndLoadNotificationTrucksManager(_DateTimeService)
                Dim Truck = InstanceTrucks.GetTruckBySoftwareUser(YourSoftwareUserId, False)
                Return GetLoaderType(Truck.LoaderTypeId, False)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As TruckNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetLoaderTypeIdfromAnnouncementSGId(YourAnnouncementSGId As Int64) As Int64
            Try
                Dim DS As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection,
                  "Select Top 1 LoaderTypeId from R2PrimaryTransportationAndLoadNotification.dbo.TblAnnouncementSubGroupsRelationLoaderTypes as AnnouncementSubGroupsRelationLoaderTypes
                   Where AnnouncementSGId=" & YourAnnouncementSGId & "", 32767, DS, New Boolean).GetRecordsCount = 0 Then Throw New LoadTypeIdfromAnnouncementSGIdNotFoundException
                Return DS.Tables(0).Rows(0).Item("LoaderTypeId")
            Catch ex As LoadTypeIdfromAnnouncementSGIdNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Namespace Exceptions
        Public Class LoadTypeNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "نوع بارگیر مورد نظر یافت نشد"
                End Get
            End Property
        End Class

        Public Class LoadTypeIdfromAnnouncementSGIdNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "هیچ نوع بارگیری مرتبط با زیرگروه اعلام بار مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace
