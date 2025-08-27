



Imports Newtonsoft.Json
Imports R2Core.BaseStandardClass
Imports R2Core.DatabaseManagement
Imports R2Core.DateAndTimeManagement
Imports R2Core.EntityRelationManagement
Imports R2Core.PublicProc
Imports R2Core.SoftwareUserManagement
Imports R2Core.WebProcessesManagement.Exceptions
Imports System.Drawing
Imports System.Reflection

Namespace WebProcessesManagement

    Public Class R2StandardWebProcessStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PId = Int64.MinValue
            PName = String.Empty
            PTitle = String.Empty
            TargetWfProcess = String.Empty
            Description = String.Empty
            PIconName = String.Empty
            PBackColor = Color.Black
            PForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPId As Int64, ByVal YourPName As String, ByVal YourPTitle As String, ByVal YourTargetWfProcess As String, ByVal YourDescription As String, YourPIconName As String, YourPBackColor As Color, YourPForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPId, YourPName.Trim())
            PId = YourPId
            PName = YourPName
            PTitle = YourPTitle
            TargetWfProcess = YourTargetWfProcess
            Description = YourDescription
            PIconName = YourPIconName
            PBackColor = YourPBackColor
            PForeColor = YourPForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PId As String
        Public Property PName As String
        Public Property PTitle As String
        Public Property TargetWfProcess As String
        Public Property Description As String
        Public Property PIconName As String
        Public Property PBackColor As Color
        Public Property PForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    Public Class R2StandardWebProcessGroupStructure
        Inherits R2StandardStructure

        Public Sub New()
            MyBase.New()
            PGId = Int64.MinValue
            PGName = String.Empty
            PGTitle = String.Empty
            PGIconName = String.Empty
            PGBackColor = Color.Black
            PGForeColor = Color.Black
            UserId = Int64.MinValue
            DateTimeMilladi = Now
            DateShamsi = String.Empty
            ViewFlag = Boolean.FalseString
            Active = Boolean.FalseString
            Deleted = Boolean.FalseString
        End Sub

        Public Sub New(ByVal YourPGId As Int64, ByVal YourPGName As String, ByVal YourPGTitle As String, YourPGIconName As String, YourPGBackColor As Color, YourPGForeColor As Color, YourUserId As Int64, YourDateTimeMilladi As DateTime, YourDateShamsi As String, YourViewFlag As Boolean, YourActive As Boolean, YourDeleted As Boolean)
            MyBase.New(YourPGId, YourPGName.Trim())
            PGId = YourPGId
            PGName = YourPGName
            PGTitle = YourPGTitle
            PGIconName = YourPGIconName
            PGBackColor = YourPGBackColor
            PGForeColor = YourPGForeColor
            UserId = YourUserId
            DateTimeMilladi = YourDateTimeMilladi
            DateShamsi = YourDateShamsi
            ViewFlag = YourViewFlag
            Active = YourActive
            Deleted = YourDeleted
        End Sub

        Public Property PGId As String
        Public Property PGName As String
        Public Property PGTitle As String
        Public Property PGIconName As String
        Public Property PGBackColor As Color
        Public Property PGForeColor As Color
        Public Property UserId As Int64
        Public Property DateTimeMilladi As DateTime
        Public Property DateShamsi As String
        Public Property ViewFlag As Boolean
        Public Property Active As Boolean
        Public Property Deleted As Boolean





    End Class

    Public Class R2CoreWP
        Public PId As Int64
        Public PTitle As String
        Public Description As String
        Public PIconName As String
        Public PAccess As Boolean
    End Class

    Public Class R2CoreWPG
        Public PGId As Int64
        Public PGTitle As String
        Public PGIconName As String
        Public WebProcesses As List(Of R2CoreWP)
        Public PGAccess As Boolean
    End Class

    Public Class R2CoreWebProcessesManager
        Public Function GetWebProcesses(YourUserId As Int64) As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select WebProcessGroups.PGId,WebProcessGroups.PGTitle,WebProcessGroups.PGIconName,WebProcesses.PId,WebProcesses.PTitle,WebProcesses.PName,WebProcesses.Description,WebProcesses.PIconName,WebProcesses.PForeColor as ForeColor,WebProcesses.PBackColor as BackColor  
                  from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                         Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
                         Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroups On SoftwareUserWebProcessGroup.E2=WebProcessGroups.PGId
						 Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroups.PGId=WebProcessGroupWebProcess.E1 
						 Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses on  WebProcessGroupWebProcess.E2=WebProcesses.PId 
						 Inner Join R2Primary.dbo.TblPermissions as [Permissions] On   WebProcesses.PId=[Permissions].EntityIdSecond and SoftwareUser.UserId=[Permissions].EntityIdFirst  
                  Where SoftwareUser.UserId=" & YourUserId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0and 
				        SoftwareUserWebProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & " and SoftwareUserWebProcessGroup.RelationActive=1 and  
						WebProcessGroups.ViewFlag=1 and  WebProcessGroups.Active=1 and WebProcessGroups.Deleted=0 and
						WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & " and WebProcessGroupWebProcess.RelationActive=1 and
						WebProcesses.Active=1 and WebProcesses.ViewFlag=1 and WebProcesses.Deleted=0 and
						[Permissions].PermissionTypeId=" & R2Core.PermissionManagement.R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & " and [Permissions].RelationActive=1
			      Order By WebProcessGroups.PGId,WebProcesses.PId 
				  for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New SoftwareUserHasNotAnyWebProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyWebProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetVeryUsefulWebProcesses(YourUserd As R2CoreSoftwareUser) As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select WebProcessGroups.PGId,WebProcessGroups.PGTitle,WebProcessGroups.PGIconName,WebProcesses.PId,WebProcesses.PTitle,WebProcesses.PName,
                         WebProcesses.Description,WebProcesses.PIconName,WebProcesses.PForeColor as ForeColor,WebProcesses.PBackColor as BackColor 
                  from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                         Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
                         Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroups On SoftwareUserWebProcessGroup.E2=WebProcessGroups.PGId
						 Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroups.PGId=WebProcessGroupWebProcess.E1 
						 Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses on  WebProcessGroupWebProcess.E2=WebProcesses.PId 
						 Inner Join R2Primary.dbo.TblPermissions as [Permissions] On   WebProcesses.PId=[Permissions].EntityIdSecond and SoftwareUser.UserId=[Permissions].EntityIdFirst  
                         Inner Join R2Primary.dbo.TblVeryUsefulProcesses as VeryUsefulProcesses On WebProcesses.PId=VeryUsefulProcesses.WebProcessId 
                  Where SoftwareUser.UserId=" & YourUserd.UserId & "  and VeryUsefulProcesses.UserTypeId=" & YourUserd.UserTypeId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0and 
				        SoftwareUserWebProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & " and SoftwareUserWebProcessGroup.RelationActive=1 and  
						WebProcessGroups.ViewFlag=1 and  WebProcessGroups.Active=1 and WebProcessGroups.Deleted=0 and
						WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & " and WebProcessGroupWebProcess.RelationActive=1 and
						WebProcesses.Active=1 and WebProcesses.ViewFlag=1 and WebProcesses.Deleted=0 and
						[Permissions].PermissionTypeId=" & R2Core.PermissionManagement.R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & " and [Permissions].RelationActive=1
			      Order By WebProcessGroups.PGId,WebProcesses.PId 
				  for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New SoftwareUserHasNotAnyVeryUsefulWebProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyVeryUsefulWebProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetTaskBarWebProcesses(YourUserd As R2CoreSoftwareUser) As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select WebProcessGroups.PGId,WebProcessGroups.PGTitle,WebProcessGroups.PGIconName,WebProcesses.PId,WebProcesses.PTitle,WebProcesses.PName,
                         WebProcesses.Description,WebProcesses.PIconName,WebProcesses.PForeColor as ForeColor,WebProcesses.PBackColor as BackColor 
                  from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
                         Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
                         Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroups On SoftwareUserWebProcessGroup.E2=WebProcessGroups.PGId
						 Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroups.PGId=WebProcessGroupWebProcess.E1 
						 Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses on  WebProcessGroupWebProcess.E2=WebProcesses.PId 
						 Inner Join R2Primary.dbo.TblPermissions as [Permissions] On   WebProcesses.PId=[Permissions].EntityIdSecond and SoftwareUser.UserId=[Permissions].EntityIdFirst  
                         Inner Join R2Primary.dbo.TblTaskBarProcesses as TaskBarProcesses On WebProcesses.PId=TaskBarProcesses.WebProcessId 
                  Where SoftwareUser.UserId=" & YourUserd.UserId & "  and TaskBarProcesses.UserTypeId=" & YourUserd.UserTypeId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0and 
				        SoftwareUserWebProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & " and SoftwareUserWebProcessGroup.RelationActive=1 and  
						WebProcessGroups.ViewFlag=1 and  WebProcessGroups.Active=1 and WebProcessGroups.Deleted=0 and
						WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & " and WebProcessGroupWebProcess.RelationActive=1 and
						WebProcesses.Active=1 and WebProcesses.ViewFlag=1 and WebProcesses.Deleted=0 and
						[Permissions].PermissionTypeId=" & R2Core.PermissionManagement.R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & " and [Permissions].RelationActive=1
			      Order By WebProcessGroups.PGId,WebProcesses.PId 
				  for json auto", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Return InstanccePublicProcedures.GetIntegratedJson(Ds)
                Else
                    Throw New SoftwareUserHasNotAnyTaskBarWebProcessPermissionException
                End If
            Catch exx As SoftwareUserHasNotAnyTaskBarWebProcessPermissionException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetAllOfWebProcessGroupsWebProcesses(YourSoftwareUserId As Int64) As String
            Try
                Dim InstanccePublicProcedures = New R2CoreInstancePublicProceduresManager
                Dim InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(New R2DateTimeService, Nothing)
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
                 "Select WebProcessGroups.PGId,WebProcessGroups.PGTitle,WebProcessGroups.PGIconName,WebProcesses.PId,WebProcesses.PTitle,WebProcesses.PName,WebProcesses.Description,WebProcesses.PIconName from R2Primary.dbo.TblWebProcessGroups as WebProcessGroups 
                     Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroups.PGId=WebProcessGroupWebProcess.E1 
                     Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses on  WebProcessGroupWebProcess.E2=WebProcesses.PId 
                  Where WebProcessGroups.ViewFlag=1 and  WebProcessGroups.Active=1 and WebProcessGroups.Deleted=0 and
   	                 WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & " and WebProcessGroupWebProcess.RelationActive=1 and
	                 WebProcesses.Active=1 and WebProcesses.ViewFlag=1 and WebProcesses.Deleted=0 
                     Order By WebProcessGroups.PGId,WebProcesses.PId", 0, Ds, New Boolean).GetRecordsCount <> 0 Then
                    Dim LstWPG = New List(Of R2CoreWPG)
                    Dim Index As Int64 = 0
                    While Index <= Ds.Tables(0).Rows.Count - 1
                        Dim WPG = New R2CoreWPG
                        WPG.PGId = Ds.Tables(0).Rows(Index).Item("PGId")
                        WPG.PGTitle = Ds.Tables(0).Rows(Index).Item("PGTitle")
                        WPG.PGIconName = Ds.Tables(0).Rows(Index).Item("PGIconName")
                        WPG.PGAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessGroupAccess(YourSoftwareUserId, WPG.PGId)
                        Dim LstWP = New List(Of R2CoreWP)
                        While WPG.PGId = Ds.Tables(0).Rows(Index).Item("PGId")
                            Dim WP = New R2CoreWP
                            WP.PId = Ds.Tables(0).Rows(Index).Item("PId")
                            WP.PTitle = Ds.Tables(0).Rows(Index).Item("PTitle")
                            WP.Description = Ds.Tables(0).Rows(Index).Item("Description")
                            WP.PIconName = Ds.Tables(0).Rows(Index).Item("PIconName")
                            WP.PAccess = InstanceSoftwareUsers.GetSoftwareUserWebProcessAccess(YourSoftwareUserId, WP.PId)
                            LstWP.Add(WP)
                            Index += 1
                            If Index > Ds.Tables(0).Rows.Count - 1 Then Exit While
                        End While
                        WPG.WebProcesses = LstWP
                        LstWPG.Add(WPG)
                    End While
                    Return JsonConvert.SerializeObject(LstWPG)
                Else
                    Throw New NotAnyWebProcessGroupWebProcessFoundException
                End If
            Catch exx As NotAnyWebProcessGroupWebProcessFoundException
                Throw exx
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        'Public Function GetWebProcesses() As List(Of R2CoreWebProcesseStructure)
        '    Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '            "Select  WebProcesses.PId,WebProcesses.PTitle,WebProcesses.Description from R2Primary.dbo.TblWebProcessGroups as WebProcessGroups
        '              inner join R2Primary.dbo.TblEntityRelations as EntityRelations on WebProcessGroups.PGId = EntityRelations.E1 
        '              inner join R2Primary.dbo.TblWebProcesses as WebProcesses on EntityRelations.E2=WebProcesses.PId 
        '             where EntityRelations.ERTypeId=5 and EntityRelations.RelationActive=1 and WebProcessGroups.Active=1 and WebProcessGroups.ViewFlag=1 and WebProcessGroups.Deleted=0
        '                   and WebProcesses.Active=1 and WebProcesses.Deleted=0 and WebProcesses.ViewFlag =1
        '             order by WebProcessGroups.PGId ", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Dim Lst As New List(Of R2CoreWebProcesseStructure)
        '            For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
        '                Dim NSS As New R2CoreWebProcesseStructure
        '                NSS.WPId = Ds.Tables(0).Rows(Loopx).Item("PId")
        '                NSS.WPTitle = Ds.Tables(0).Rows(Loopx).Item("PTitle").Trim
        '                NSS.WPDescription = Ds.Tables(0).Rows(Loopx).Item("Description").Trim
        '                Lst.Add(NSS)
        '            Next
        '            Return Lst
        '        Else
        '            Throw New WebProccessGroupHasNotAnyWebProcessRelationException
        '        End If
        '    Catch exx As WebProccessGroupHasNotAnyWebProcessRelationException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

        'Public Function GetWebProcceseName(YourWebProcessId As Int64) As String
        'Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '            "select * from R2Primary.dbo.TblWebProcesses where PId=" & YourWebProcessId & "", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Return Ds.Tables(0).Rows(0).Item("PName").trim
        '        Else
        '            Throw New WebProccessNotFoundException
        '        End If
        '    Catch exx As WebProccessNotFoundException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function


        'Public Shared Function GetWebProcesses(YourNSSSoftwareUser As R2CoreStandardSoftwareUserStructure) As List(Of R2StandardWebProcessStructure)
        '    Try
        '        Dim Ds As DataSet
        '        If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection,
        '         "Select WebProcesses.* 
        '           from R2Primary.dbo.TblSoftwareUsers as SoftwareUser
        '             Inner Join R2Primary.dbo.TblEntityRelations as SoftwareUserWebProcessGroup On SoftwareUser.UserId=SoftwareUserWebProcessGroup.E1 
        '             Inner Join R2Primary.dbo.TblWebProcessGroups as WebProcessGroup On SoftwareUserWebProcessGroup.E2=WebProcessGroup.PGId 
        '             Inner Join R2Primary.dbo.TblEntityRelations as WebProcessGroupWebProcess On WebProcessGroup.PGId=WebProcessGroupWebProcess.E1  
        '             Inner Join R2Primary.dbo.TblWebProcesses as WebProcesses On WebProcessGroupWebProcess.E2=WebProcesses.PId 
        '             Inner Join R2Primary.dbo.TblPermissions  as Permission On WebProcesses.PId=Permission.EntityIdSecond 
        '           Where SoftwareUser.UserId=" & YourNSSSoftwareUser.UserId & " and SoftwareUser.UserActive=1 and SoftwareUser.Deleted=0 and SoftwareUserWebProcessGroup.ERTypeId=" & R2CoreEntityRelationTypes.SoftwareUser_WebProcessGroup & "
        '                 and SoftwareUserWebProcessGroup.RelationActive=1 and  WebProcessGroup.ViewFlag=1 and  WebProcessGroup.Active=1 and WebProcessGroup.Deleted=0 and WebProcessGroupWebProcess.ERTypeId=" & R2CoreEntityRelationTypes.WebProcessGroup_WebProcess & "  
        '                 and WebProcessGroupWebProcess.RelationActive=1 and WebProcesses.ViewFlag=1 and WebProcesses.Active=1 and WebProcesses.Deleted=0 
        '                 and Permission.RelationActive=1 and Permission.EntityIdFirst=" & YourNSSSoftwareUser.UserId & " and PermissionTypeId=" & R2CorePermissionTypes.SoftwareUsersAccessWebProcesses & "
        '           Order By WebProcessGroup.PGId,WebProcesses.PId ", 3600, Ds, New Boolean).GetRecordsCount <> 0 Then
        '            Dim Lst As New List(Of R2StandardWebProcessStructure)
        '            For Loopx As Int64 = 0 To Ds.Tables(0).Rows.Count - 1
        '                Dim NSS As New R2StandardWebProcessStructure
        '                NSS.PId = Ds.Tables(0).Rows(Loopx).Item("PId")
        '                NSS.PName = Ds.Tables(0).Rows(Loopx).Item("PName")
        '                NSS.PTitle = "  " + Ds.Tables(0).Rows(Loopx).Item("PTitle").ToString().Trim
        '                NSS.Description = Ds.Tables(0).Rows(Loopx).Item("Description").trim
        '                NSS.PForeColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PForeColor").trim)
        '                NSS.PBackColor = Color.FromName(Ds.Tables(0).Rows(Loopx).Item("PBackColor").trim)
        '                NSS.TargetWfProcess = Ds.Tables(0).Rows(Loopx).Item("TargetWfProcess").trim
        '                NSS.UserId = Ds.Tables(0).Rows(Loopx).Item("UserId")
        '                NSS.DateTimeMilladi = Ds.Tables(0).Rows(Loopx).Item("DateTimeMilladi")
        '                NSS.DateShamsi = Ds.Tables(0).Rows(Loopx).Item("DateShamsi").trim
        '                NSS.Active = Ds.Tables(0).Rows(Loopx).Item("Active")
        '                NSS.ViewFlag = Ds.Tables(0).Rows(Loopx).Item("ViewFlag")
        '                NSS.Deleted = Ds.Tables(0).Rows(Loopx).Item("Deleted")
        '                Lst.Add(NSS)
        '            Next
        '            Return Lst
        '        Else
        '            Throw New SoftwareUserHasNotAnyWebProcessPermissionException
        '        End If
        '    Catch exx As SoftwareUserHasNotAnyWebProcessPermissionException
        '        Throw exx
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function

    End Class

    Namespace Exceptions
        Public Class NotAnyWebProcessGroupWebProcessFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "هیچ گونه اطلاعاتی برای گروه منو و یا منوی کاربر در سامانه یافت نشد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotAnyWebProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از منوهای سایت را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotAnyVeryUsefulWebProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از منوهای پر کاربرد را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotAnyTaskBarWebProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از منوهای نوار پر کاربرد را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotWebProcessPermissionException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به منوی مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotAnyWebProcessGroupRelationException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به هیچ یک از گروه منوهای سامانه را ندارید"
                End Get
            End Property
        End Class

        Public Class SoftwareUserHasNotWebProcessGroupRelationException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "مجوز دسترسی به گروه منوی مورد نظر وجود ندارد"
                End Get
            End Property
        End Class

        Public Class WebProccessGroupHasNotAnyWebProcessRelationException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "گروه منوی مورد نظر دارای زیر منوی تعریف شده ای نیست"
                End Get
            End Property
        End Class

        Public Class WebProccessNotFoundException
            Inherits ApplicationException
            Public Overrides ReadOnly Property Message As String
                Get
                    Return "منو با کد شاخص مورد نظر یافت نشد"
                End Get
            End Property
        End Class

    End Namespace


End Namespace
