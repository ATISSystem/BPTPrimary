
Imports System.Reflection

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement

Namespace ReportsManagement

    Public MustInherit Class R2CoreReports
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly UsersChargeReport As Int64 = 1
        Public Shared ReadOnly PersonnelEnterExitReport As Int64 = 5

    End Class

    Public Class R2StandardReportStructure

        Private myRId As Int64
        Private myRName As String
        Private myRFullName As String
        Private myAssemblyFullPath As String
        Private myReportServerURL As String
        Private myReportServerPath As String
        Private myReportServerCredential As String
        Private myDescription As String

#Region "Constructing Management"
        Public Sub New()
            MyBase.New()
            myRId = 0 : myRName = "" : myRFullName = "" : myAssemblyFullPath = "" : myReportServerURL = "" : myReportServerPath = "" : myReportServerCredential = "" : myDescription = ""
        End Sub
        Public Sub New(ByVal YourRId As Int64, ByVal YourRName As String, ByVal YourRFullName As String, ByVal YourAssemblyFullPath As String, YourReportServerURL As String, YourReportServerPath As String, YourReportServerCredential As String, YourDescription As String)
            MyBase.New()
            myRId = YourRId
            myRName = YourRName
            myRFullName = YourRFullName
            myAssemblyFullPath = YourAssemblyFullPath
            myReportServerURL = YourReportServerURL
            myReportServerPath = YourReportServerPath
            myReportServerCredential = YourReportServerCredential
            myDescription = YourDescription
        End Sub
#End Region

#Region "Properting Management"
        Public Property RId() As Int64
            Get
                Return myRId
            End Get
            Set(ByVal Value As Int64)
                myRId = Value
            End Set
        End Property

        Public Property RName() As String
            Get
                Return myRName
            End Get
            Set(ByVal Value As String)
                myRName = Value
            End Set
        End Property

        Public Property RFullName() As String
            Get
                Return myRFullName
            End Get
            Set(ByVal Value As String)
                myRFullName = Value
            End Set
        End Property

        Public Property AssemblyFullPath() As String
            Get
                Return myAssemblyFullPath
            End Get
            Set(ByVal Value As String)
                myAssemblyFullPath = Value
            End Set
        End Property

        Public Property ReportServerURL() As String
            Get
                Return myReportServerURL
            End Get
            Set(ByVal Value As String)
                myReportServerURL = Value
            End Set
        End Property

        Public Property ReportServerPath() As String
            Get
                Return myReportServerPath
            End Get
            Set(ByVal Value As String)
                myReportServerPath = Value
            End Set
        End Property

        Public Property ReportServerCredential() As String
            Get
                Return myReportServerCredential
            End Get
            Set(ByVal Value As String)
                myReportServerCredential = Value
            End Set
        End Property

#End Region


    End Class

    Public NotInheritable Class R2CoreMClassReportsManagement

        Private Shared _DateTimeService As IDateTimeService = New R2DateTimeService
        Private Shared InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)

        Public Shared Function GetNSSReport(YourRId As String) As R2StandardReportStructure
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblReports Where RId='" & YourRId & "'", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New GetNSSException
                Else
                    Return New R2StandardReportStructure(Ds.Tables(0).Rows(0).Item("RId"), Ds.Tables(0).Rows(0).Item("RName").trim, Ds.Tables(0).Rows(0).Item("RFullName").trim, Ds.Tables(0).Rows(0).Item("AssemblyFullPath").trim, Ds.Tables(0).Rows(0).Item("ReportServerURL").trim, Ds.Tables(0).Rows(0).Item("ReportServerPath").trim, Ds.Tables(0).Rows(0).Item("ReportServerCredential").trim, Ds.Tables(0).Rows(0).Item("Description").trim)
                End If
            Catch exx As GetNSSException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class



End Namespace
