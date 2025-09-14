


Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.ExceptionManagement
Imports R2Core.Exceptions
Imports R2Core.SoftwareUserManagement
Imports System.IO
Imports System.Reflection

Namespace RawGroups


    'BPTChanged
    Public Enum R2CoreRawGroupsAccessType
        None = 0
        FileShare = 1
        FTP = 2
    End Enum

    Public Class R2CoreRawGroup
        Inherits BaseStandardClass.R2StandardStructure

        Public RGId As Int64
        Public RGName As String
        Public RGFullPath As String
        Public AccessType As R2CoreRawGroupsAccessType
        Public CoreName As String
        Public DateTimeMilladi As DateTime
        Public DateShamsi As String
        Public Description As String
        Public UserId As Int64

    End Class

    Public MustInherit Class R2CoreRawGroups
        Public Shared ReadOnly None As Int64 = 0
        Public Shared ReadOnly UserImages As Int64 = 1
        Public Shared ReadOnly PersonnelImages As Int64 = 4
        Public Shared ReadOnly UploadedFiles As Int64 = 6
        Public Shared ReadOnly Carousels As Int64 = 9

    End Class

    Public Class R2CoreRawGroupsManager

        Public Shared Function GetRawGroup(YourRawGroupId As Int64) As R2CoreRawGroup
            Try
                Dim Ds As DataSet
                Dim InstanceSqlDataBOX = New R2CoreSqlDataBOXManager
                If InstanceSqlDataBOX.GetDataBOX(New R2PrimarySqlConnection, "Select * from R2Primary.dbo.TblRawGroups Where RGId=" & YourRawGroupId & "", 32767, Ds, New Boolean).GetRecordsCount() = 0 Then
                    Throw New R2CoreRawGroupNotFoundException
                Else
                    Return New R2CoreRawGroup With {.RGId = Ds.Tables(0).Rows(0).Item("RGId"), .RGName = Ds.Tables(0).Rows(0).Item("RGName").trim, .RGFullPath = Ds.Tables(0).Rows(0).Item("RGFullPath").trim,
                        .AccessType = Ds.Tables(0).Rows(0).Item("AccessType"), .CoreName = Ds.Tables(0).Rows(0).Item("CoreName").trim, .DateTimeMilladi = Ds.Tables(0).Rows(0).Item("DateTimeMilladi"),
                        .DateShamsi = Ds.Tables(0).Rows(0).Item("DateShamsi").trim, .Description = Ds.Tables(0).Rows(0).Item("Description").trim, .UserId = Ds.Tables(0).Rows(0).Item("UserId")}
                End If
            Catch ex As R2CoreRawGroupNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Public Class R2CoreRawGroupFileHolder

        Protected Property RawGroupId As Int64 = R2CoreRawGroups.None
        Protected Property RawGroupFile As R2CoreFile = Nothing
        Protected Property RawGroupByteArray As Byte() = Nothing

        Public Sub New(YourRawGroupId As Int64, YourFile As R2CoreFile, YourByteArray As Byte())
            Try
                RawGroupId = YourRawGroupId
                RawGroupFile = YourFile
                RawGroupByteArray = YourByteArray
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub New(YourRawGroupId As Int64, YourFile As R2CoreFile)
            Try
                RawGroupId = YourRawGroupId
                RawGroupFile = YourFile
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Function GetFullPath(YourRawGroupId As Int64, YourFile As R2CoreFile) As String
            Try
                Dim RawGroup = R2CoreRawGroupsManager.GetRawGroup(YourRawGroupId)
                Return RawGroup.RGFullPath + "\" + YourFile.FileName
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub SaveFile()
            Try
                Using FS As FileStream = New FileStream(GetFullPath(RawGroupId, RawGroupFile), FileMode.Create, FileAccess.Write)
                    FS.Write(RawGroupByteArray, 0, RawGroupByteArray.Length)
                End Using
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
            Catch ex As IO.FileNotFoundException
                Throw New R2CoreFileNotFoundInRawGroupsException
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function ExistIOFile() As Boolean
            Try
                Return IO.File.Exists(GetFullPath(RawGroupId, RawGroupFile))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub DeleteFile()
            Try
                IO.File.Delete(GetFullPath(RawGroupId, RawGroupFile))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub DeleteFileButKeepDeleted()
            Try
                IO.File.Move(GetFullPath(RawGroupId, RawGroupFile), GetFullPath(RawGroupId, New R2CoreFile(RawGroupFile.FileName + ".del")))
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub



    End Class

End Namespace

Namespace Exceptions
    Public Class R2CoreFileNotFoundInRawGroupsException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "فایل مورد نظر موجود نیست"
            End Get
        End Property
    End Class

    Public Class R2CoreRawGroupNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعاتی مرتبط با ساختار مورد نظر یافت نشد"
            End Get
        End Property
    End Class

End Namespace
