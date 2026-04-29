


Imports System.IO
Imports System.Reflection
Imports System.Web.Services.Protocols

Imports R2Core.BaseClasses
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.ExceptionManagement
Imports R2Core.Exceptions
Imports R2Core.LoggingManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.SoftwareUserManagement

Namespace RawGroups


    'BPTChanged
    Public Enum R2CoreRawGroupsAccessType
        None = 0
        FileShare = 1
        FTP = 2
    End Enum

    'BPTChanged
    Public Class R2CoreRawGroup
        Public RGId As Int64
        Public RGName As String
        Public RGFullPath As String
        Public AccessType As R2CoreRawGroupsAccessType
        Public Description As String
    End Class

    'BPTChanged
    Public Class R2CoreRawGroupsManager

        Private InstanceSqlDataBOX As R2CoreSqlDataBOXManager
        Private _DateTimeService As IDateTimeService

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                _DateTimeService = YourDateTimeService
                InstanceSqlDataBOX = New R2CoreSqlDataBOXManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetRawGroup(YourRawGroupId As Int64) As R2CoreRawGroup
            Try
                Dim Ds As DataSet
                If InstanceSqlDataBOX.GetDataBOX(R2PrimarySqlConnection.GetSubscriptionDBConnection, "Select * from R2Primary.dbo.TblRawGroups Where RGId=" & YourRawGroupId & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New RawGroupNotFoundException
                Else
                    Return New R2CoreRawGroup With {.RGId = Ds.Tables(0).Rows(0).Item("RGId"), .RGName = Ds.Tables(0).Rows(0).Item("RGName").trim, .RGFullPath = Ds.Tables(0).Rows(0).Item("RGFullPath").trim,
                        .AccessType = Ds.Tables(0).Rows(0).Item("AccessType"), .Description = Ds.Tables(0).Rows(0).Item("Description").trim}
                End If
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    'BPTChanged
    Public Class R2CoreRawGroupFileHolder

        Protected Property RawGroupId As Int64
        Protected Property RawGroupFile As R2CoreFile
        Protected Property RawGroupByteArray As Byte()
        Private _loggerService As ILogger
        Private _SoftwareUserService As ISoftwareUserService
        Private _DateTimeService As IDateTimeService
        Private InstanceRawGroupsManager As R2CoreRawGroupsManager

        Public Sub New(YourDateTimeService As IDateTimeService)
            Try
                RawGroupId = R2CoreRawGroups.None
                RawGroupFile = Nothing
                RawGroupByteArray = Nothing
                _DateTimeService = YourDateTimeService
                InstanceRawGroupsManager = New R2CoreRawGroupsManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub New(YourRawGroupId As Int64, YourFile As R2CoreFile, YourByteArray As Byte(), YourDateTimeService As IDateTimeService)
            Try
                RawGroupId = YourRawGroupId
                RawGroupFile = YourFile
                RawGroupByteArray = YourByteArray
                _DateTimeService = YourDateTimeService
                InstanceRawGroupsManager = New R2CoreRawGroupsManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub New(YourRawGroupId As Int64, YourFile As R2CoreFile, YourDateTimeService As IDateTimeService)
            Try
                RawGroupId = YourRawGroupId
                RawGroupFile = YourFile
                _DateTimeService = YourDateTimeService
                InstanceRawGroupsManager = New R2CoreRawGroupsManager(_DateTimeService)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Function GetFullPath(YourRawGroupId As Int64, YourFile As R2CoreFile) As String
            Try
                Dim RawGroup = InstanceRawGroupsManager.GetRawGroup(YourRawGroupId)
                Return RawGroup.RGFullPath + "\" + YourFile.FileName
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SaveFile()
            Try
                Using FS As FileStream = New FileStream(GetFullPath(RawGroupId, RawGroupFile), FileMode.Create, FileAccess.Write)
                    FS.Write(RawGroupByteArray, 0, RawGroupByteArray.Length)
                End Using
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub GetFile(ByRef YourByteArray As Byte())
            Try
                Using FS As FileStream = New FileStream(GetFullPath(RawGroupId, RawGroupFile), FileMode.Open, FileAccess.Read)
                    YourByteArray = New Byte(FS.Length) {}
                    FS.Read(YourByteArray, 0, FS.Length)
                End Using
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As IO.FileNotFoundException
                Throw New FileNotFoundInRawGroupsException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function ExistIOFile() As Boolean
            Try
                Return IO.File.Exists(GetFullPath(RawGroupId, RawGroupFile))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub DeleteFile()
            Try
                IO.File.Delete(GetFullPath(RawGroupId, RawGroupFile))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As IO.FileNotFoundException
                Throw New FileNotFoundInRawGroupsException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DeleteFileButKeepDeleted()
            Try
                IO.File.Move(GetFullPath(RawGroupId, RawGroupFile), GetFullPath(RawGroupId, New R2CoreFile(RawGroupFile.FileName + ".del")))
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UnableConnectToAPIException
                Throw ex
            Catch ex As RawGroupNotFoundException
                Throw ex
            Catch ex As IO.FileNotFoundException
                Throw New FileNotFoundInRawGroupsException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub



    End Class

    Public MustInherit Class R2CoreRawGroups
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly UserImages As Int64 = 1
        Public Shared ReadOnly PersonnelImages As Int64 = 4
        Public Shared ReadOnly UploadedFiles As Int64 = 6
        Public Shared ReadOnly Carousels As Int64 = 9

    End Class

End Namespace

Namespace Exceptions

    'BPTChanged
    Public Class FileNotFoundInRawGroupsException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotFoundInRawGroupsException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotFoundInRawGroupsException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class RawGroupNotFoundException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotFoundInRawGroupsException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotFoundInRawGroupsException).MsgId
        End Sub
    End Class

End Namespace
