



Imports System.Data.SqlClient
Imports System.Reflection
Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProc
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SQLInjectionPrevention

Namespace RequesterManagement

    Public MustInherit Class R2CoreRequesterTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly Desktop As Int64 = 1
        Public Shared ReadOnly Web As Int64 = 2
        Public Shared ReadOnly Mobile As Int64 = 3
    End Class

    Public MustInherit Class R2CoreRequesters
        Public Shared ReadOnly None As Int64 = 0
    End Class

    Public Class R2StandardRequesterStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            RqId = Int64.MinValue
            RqName = String.Empty
            RqTitle = String.Empty
            RqTypeId = Int64.MinValue
            Description = String.Empty
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourRqId As Int64, ByVal YourRqName As String, ByVal YourRqTitle As String, ByVal YourRqTypeId As Int64, ByVal YourDescription As String, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourRqId, YourRqName.Trim())
            RqId = YourRqId
            RqName = YourRqName
            RqTitle = YourRqTitle
            RqTypeId = YourRqTypeId
            Description = YourDescription
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property RqId As Int64
        Public Property RqName As String
        Public Property RqTitle As String
        Public Property RqTypeId As Int64
        Public Property Description As String
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

End Namespace

'BPTChanged
Namespace RequestersManagement

    Public Class R2CoreRequestersManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IR2DateTimeService
        Public Sub New(YourDateTimeService As IR2DateTimeService)
            _DateTimeService = YourDateTimeService
            InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
        End Sub

        Public Function GetRequesters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                Dim Ds As New DataSet
                Dim InstancePublicProcedures = New R2CoreInstancePublicProceduresManager
                If YourImmediately Then
                    Dim Da As New SqlClient.SqlDataAdapter
                    Da.SelectCommand = New SqlCommand("
                             Select RqId as RequesterId,RqTitle as RequesterTitle from R2Primary.dbo.TblRequesters 
                             Where RqTitle Like N'%" & YourSearchString & "%' And Active=1 order by RequesterId for JSON Path")
                    Da.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                    If Da.Fill(Ds) <= 0 Then Throw New AnyNotFoundException
                Else
                    If InstanceSqlDataBox.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "
                             Select RqId as RequesterId,RqTitle as RequesterTitle from R2Primary.dbo.TblRequesters 
                             Where RqTitle Like N'%" & YourSearchString & "%' And Active=1 order by RequesterId for JSON Path", 32767, Ds, New Boolean).GetRecordsCount = 0 Then Throw New AnyNotFoundException
                End If
                Return InstancePublicProcedures.GetIntegratedJson(Ds)
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAllRequesters(YourImmediately As Boolean) As String
            Try
                Return GetRequesters(String.Empty, YourImmediately)
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class



End Namespace
