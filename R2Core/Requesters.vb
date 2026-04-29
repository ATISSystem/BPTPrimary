



Imports System.Data.SqlClient
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.PublicProcedures
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SQLInjectionPrevention


'BPTChanged
Namespace RequestersManagement

    'BPTChanged
    Public Class R2CoreRequestersManager
        Private InstanceSqlDataBox As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBox = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetRequesters(YourSearchString As String, YourImmediately As Boolean) As String
            Try
                Dim InstanceSQLInjectionPrevention = New R2CoreSQLInjectionPreventionManager(_DateTimeService)
                InstanceSQLInjectionPrevention.GeneralAuthorization(YourSearchString)

                Dim Ds As New DataSet
                Dim InstancePublicProcedures = New R2CorePublicProceduresManager
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
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As SqlException
                Throw R2CoreDatabaseManager.GetEquivalenceMessage(ex)
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
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As AnyNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public MustInherit Class R2CoreRequesters
        Public Shared ReadOnly None As Int64 = 0
    End Class

    'BPTChanged
    Public MustInherit Class R2CoreRequesterTypes
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly Desktop As Int64 = 1
        Public Shared ReadOnly Web As Int64 = 2
        Public Shared ReadOnly Mobile As Int64 = 3
        Public Shared ReadOnly BPTService As Int64 = 4
        Public Shared ReadOnly SoapService As Int64 = 5
    End Class

End Namespace

