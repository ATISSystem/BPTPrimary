
Imports System.Globalization
Imports System.Reflection
Imports System.Text

Imports R2Core.DatabaseManagement

Namespace PublicProcedures

    'BPTChanged
    Public Class R2CorePublicProceduresManager

        Private _R2PrimaryFSWS As R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService

        Public Sub New()
            Try
                _R2PrimaryFSWS = New R2PrimaryFileSharingWebService.R2PrimaryFileSharingWebService
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetNullIntEquivalentforIds() As Int64
            Return -1
        End Function

        Public Function GetIntegratedJson(YourDataSet As DataSet) As String
            Try
                Dim SB As New StringBuilder
                For Loopx = 0 To YourDataSet.Tables(0).Rows.Count - 1
                    SB.Append(YourDataSet.Tables(0).Rows(Loopx).Item(0))
                Next
                Return SB.ToString
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        Public Function GetBooleanVariablePersianEquivalent(YourBooleanVariable As Boolean) As String
            If YourBooleanVariable Then
                Return "فعال"
            Else
                Return "غیر فعال"
            End If
        End Function

        Public Function RepeatStr(ByVal Str As String, ByVal Counts As Int16) As String
            Dim myStr As String = ""
            Try
                For loopx As Int16 = 1 To Counts
                    myStr += Str
                Next
                Return myStr
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function

        'Public Sub SaveFile(YourRawGroupId As Int64, YourFileName As String, YourFile As Byte(), YourNSSUser As R2CoreStandardSoftwareUserStructure)
        '    Try
        '        _R2PrimaryFSWS.WebMethodSaveFile(YourRawGroupId, YourFileName, YourFile, _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Sub

        'Public Function IsExistFile(YourRawGroupId As Int64, YourFileName As String, YourNSSUser As R2CoreStandardSoftwareUserStructure) As Boolean
        '    Try
        '        Return _R2PrimaryFSWS.WebMethodIOFileExist(YourRawGroupId, YourFileName, _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Public Function GetFile(YourRawGroupId As Int64, YourFileName As String, YourNSSUser As R2CoreStandardSoftwareUserStructure) As Byte()
        '    Try
        '        Return _R2PrimaryFSWS.WebMethodGetFile(YourRawGroupId, YourFileName, _R2PrimaryFSWS.WebMethodLogin(YourNSSUser.UserShenaseh, YourNSSUser.UserPassword))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Public Function ParseSignDigitToSignString(ByVal YourDig As Int64) As String
        '    Try
        '        If YourDig < 0 Then
        '            Return R2MakeCamaYourDigit(Math.Abs(YourDig)) + "-"
        '        ElseIf YourDig > 0 Then
        '            Return R2MakeCamaYourDigit(Math.Abs(YourDig))
        '        ElseIf YourDig = 0 Then
        '            Return R2MakeCamaYourDigit(Math.Abs(YourDig))
        '        Else
        '            Throw New ArgumentException
        '        End If
        '    Catch ex As ArgumentException
        '        Throw ex
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Public Function R2MakeCamaYourDigit(ByVal xdig As UInt64) As String
        '    Dim myDigit As String
        '    Dim yourDigit As String
        '    Try
        '        myDigit = xdig
        '        If Len(myDigit) <= 3 Then Return xdig
        '        yourDigit = ""
        '        For loopx As Int16 = 1 To Math.Ceiling(Len(myDigit) / 3)
        '            If loopx <> Math.Ceiling(Len(myDigit) / 3) Then
        '                yourDigit = "," + Mid(myDigit, (Len(myDigit) - 3 * loopx) + 1, 3) + yourDigit
        '            Else
        '                yourDigit = Mid(myDigit, 1, Len(myDigit) - (Math.Ceiling(Len(myDigit) / 3) - 1) * 3) + yourDigit
        '            End If
        '        Next
        '        Return yourDigit
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'رنگ سیه و سفید مناسب یک پس زمینه
        'Public Function IdealBlackWhiteTextColor(YourBackGroundColor As Color) As Color
        '    Dim nThreshold As Int64 = 105
        '    Dim bgDelta As Int64 = Convert.ToInt32((YourBackGroundColor.R * 0.299) + (YourBackGroundColor.G * 0.587) + (YourBackGroundColor.B * 0.114))
        '    Dim foreColor As Color = IIf(255 - bgDelta < nThreshold, Color.Black, Color.White)
        '    Return foreColor
        'End Function

        'Private DaDiff As New SqlClient.SqlDataAdapter
        'Private DsDiff As New DataSet
        'Public Function GetDateDiff(YourDateInterval As Microsoft.VisualBasic.DateInterval, ByVal YourMilladiDate1 As Date, ByVal YourMilladiDate2 As Date) As Long
        '    Try
        '        If YourDateInterval = DateInterval.Day Then
        '            DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Day ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
        '        ElseIf YourDateInterval = DateInterval.Minute Then
        '            DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Minute ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
        '        ElseIf YourDateInterval = DateInterval.Hour Then
        '            DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Hour ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
        '        ElseIf YourDateInterval = DateInterval.Second Then
        '            DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Second ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
        '        End If
        '        DaDiff.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
        '        DsDiff.Tables.Clear()
        '        DaDiff.Fill(DsDiff)
        '        Return DsDiff.Tables(0).Rows(0).Item("DD")
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
        '    End Try
        'End Function



    End Class

    Public Class R2CoreMClassPublicProcedures
        'رنگ سیه و سفید مناسب یک پس زمینه
        'Public Shared Function IdealBlackWhiteTextColor(YourBackGroundColor As Color) As Color
        '    Dim nThreshold As Int64 = 105
        '    Dim bgDelta As Int64 = Convert.ToInt32((YourBackGroundColor.R * 0.299) + (YourBackGroundColor.G * 0.587) + (YourBackGroundColor.B * 0.114))
        '    Dim foreColor As Color = IIf(255 - bgDelta < nThreshold, Color.Black, Color.White)
        '    Return foreColor
        'End Function

        'توليد تاريخ بعدي از يك تاريخ شمسي
        'Public Shared Function GetNextShamsiDate(ByVal YourShamsiDate As String) As String
        '    Try
        '        Dim myTempDate As String = YourShamsiDate  'Example 1394/02/31
        '        Dim myD As Int16 = Mid(myTempDate, 9, 2)
        '        Dim myM As Int16 = Mid(myTempDate, 6, 2)
        '        Dim myY As Int16 = Mid(myTempDate, 1, 4)
        '        If myM >= 1 And myM <= 6 Then
        '            If myD < 31 Then
        '                myD += 1
        '            Else
        '                myD = 1
        '                myM += 1
        '            End If
        '        ElseIf myM >= 7 And myM <= 11 Then
        '            If myD < 30 Then
        '                myD += 1
        '            Else
        '                myD = 1
        '                myM += 1
        '            End If
        '        ElseIf myM = 12 Then
        '            If myD < R2CoreMClassConfigurationManagement.GetConfig(R2CoreConfigurations.EsfandRooz, 0) Then
        '                myD += 1
        '            Else
        '                myD = 1
        '                myM = 1
        '                myY += 1
        '            End If
        '        End If
        '        Dim myDS As String = R2CoreMClassPublicProcedures.RepeatStr("0", 2 - Len(myD.ToString.Trim)) + myD.ToString.Trim
        '        Dim myMS As String = R2CoreMClassPublicProcedures.RepeatStr("0", 2 - Len(myM.ToString.Trim)) + myM.ToString.Trim
        '        Return myY.ToString.Trim + "/" + myMS.Trim + "/" + myDS.Trim
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function
        'تبديل يک عدد به حروف
        'Public Shared Function R2MakeHoroof(ByVal yournum As String) As String
        '    Try
        '        Dim mynum As String = Trim(yournum).Replace(",", "")
        '        Dim makan As Int16
        '        Dim dig As String
        '        Dim varhichy As String
        '        Dim finalstr As String = ""
        '        For loopx As Int16 = 1 To Len(mynum)
        '            makan = Len(mynum) - (loopx - 1)
        '            dig = Mid(mynum, loopx, 1)
        '            If makan = 12 Then
        '                If dig = "9" Then finalstr = " نهصد "
        '                If dig = "8" Then finalstr = " هشتصد "
        '                If dig = "7" Then finalstr = " هفتصد "
        '                If dig = "6" Then finalstr = " ششصد "
        '                If dig = "5" Then finalstr = " پانصد "
        '                If dig = "4" Then finalstr = " چهارصد "
        '                If dig = "3" Then finalstr = " سيصد "
        '                If dig = "2" Then finalstr = " دويست "
        '                If dig = "1" Then finalstr = " يکصد "
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 11 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نود "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتاد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتاد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شصت "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنجاه "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهل "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سي "
        '                If dig = "2" Then finalstr = finalstr + "و" + " بيست "
        '                If dig = "1" Then
        '                    Dim my10makandig As String = Mid(mynum, 3, 1)
        '                    If my10makandig = "9" Then finalstr = finalstr + "و" + " نوزده " + " ميليارد "
        '                    If my10makandig = "8" Then finalstr = finalstr + "و" + " هجده " + " ميليارد "
        '                    If my10makandig = "7" Then finalstr = finalstr + "و" + " هفده " + " ميليارد "
        '                    If my10makandig = "6" Then finalstr = finalstr + "و" + " شانزده " + " ميليارد "
        '                    If my10makandig = "5" Then finalstr = finalstr + "و" + " پانزده " + " ميليارد "
        '                    If my10makandig = "4" Then finalstr = finalstr + "و" + " چهارده " + " ميليارد "
        '                    If my10makandig = "3" Then finalstr = finalstr + "و" + " سيزده " + " ميليارد "
        '                    If my10makandig = "2" Then finalstr = finalstr + "و" + " دوازده " + " ميليارد "
        '                    If my10makandig = "1" Then finalstr = finalstr + "و" + " يازده " + " ميليارد "
        '                    If my10makandig = "0" Then finalstr = finalstr + "و" + " ده " + " ميليارد "
        '                    loopx += 1
        '                End If
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 10 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نه " + " ميليارد "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشت " + " ميليارد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفت " + " ميليارد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شش " + " ميليارد "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنج " + " ميليارد "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهار " + " ميليارد "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سه " + " ميليارد "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دو " + " ميليارد "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يک " + " ميليارد "
        '                If dig = "0" Then
        '                    Try
        '                        If Mid(mynum, loopx - 2, 3) <> 0 Then
        '                            finalstr = finalstr + " ميليارد "
        '                        End If
        '                    Catch ex As Exception
        '                        finalstr = finalstr + " ميليارد "
        '                    End Try
        '                End If
        '            End If
        '            If makan = 9 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نهصد "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتصد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتصد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " ششصد "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پانصد "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهارصد "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سيصد "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دويست "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يکصد "
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 8 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نود "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتاد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتاد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شصت "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنجاه "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهل "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سي "
        '                If dig = "2" Then finalstr = finalstr + "و" + " بيست "
        '                If dig = "1" Then
        '                    Dim my7makandig As String = Mid(mynum, Len(mynum) - 6, 1)
        '                    If my7makandig = "9" Then finalstr = finalstr + "و" + " نوزده " + " ميليون "
        '                    If my7makandig = "8" Then finalstr = finalstr + "و" + " هجده " + " ميليون "
        '                    If my7makandig = "7" Then finalstr = finalstr + "و" + " هفده " + " ميليون "
        '                    If my7makandig = "6" Then finalstr = finalstr + "و" + " شانزده " + " ميليون "
        '                    If my7makandig = "5" Then finalstr = finalstr + "و" + " پانزده " + " ميليون "
        '                    If my7makandig = "4" Then finalstr = finalstr + "و" + " چهارده " + " ميليون "
        '                    If my7makandig = "3" Then finalstr = finalstr + "و" + " سيزده " + " ميليون "
        '                    If my7makandig = "2" Then finalstr = finalstr + "و" + " دوازده " + " ميليون "
        '                    If my7makandig = "1" Then finalstr = finalstr + "و" + " يازده " + " ميليون "
        '                    If my7makandig = "0" Then finalstr = finalstr + "و" + " ده " + " ميليون "
        '                    loopx += 1
        '                End If
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 7 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نه " + " ميليون "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشت " + " ميليون "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفت " + " ميليون "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شش " + " ميليون "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنج " + " ميليون "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهار " + " ميليون "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سه " + " ميليون "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دو " + " ميليون "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يک " + " ميليون "
        '                If dig = "0" Then
        '                    Try
        '                        If Mid(mynum, loopx - 2, 3) <> 0 Then
        '                            finalstr = finalstr + " ميليون "
        '                        End If
        '                    Catch ex As Exception
        '                        finalstr = finalstr + " ميليون "
        '                    End Try
        '                End If
        '            End If
        '            If makan = 6 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نهصد "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتصد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتصد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " ششصد "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پانصد "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهارصد "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سيصد "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دويست "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يکصد "
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 5 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نود "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتاد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتاد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شصت "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنجاه "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهل "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سي "
        '                If dig = "2" Then finalstr = finalstr + "و" + " بيست "
        '                If dig = "1" Then
        '                    Dim my4makandig As String = Mid(mynum, Len(mynum) - 3, 1)
        '                    If my4makandig = "9" Then finalstr = finalstr + "و" + " نوزده " + " هزار "
        '                    If my4makandig = "8" Then finalstr = finalstr + "و" + " هجده " + " هزار "
        '                    If my4makandig = "7" Then finalstr = finalstr + "و" + " هفده " + " هزار "
        '                    If my4makandig = "6" Then finalstr = finalstr + "و" + " شانزده " + " هزار "
        '                    If my4makandig = "5" Then finalstr = finalstr + "و" + " پانزده " + " هزار "
        '                    If my4makandig = "4" Then finalstr = finalstr + "و" + " چهارده " + " هزار "
        '                    If my4makandig = "3" Then finalstr = finalstr + "و" + " سيزده " + " هزار "
        '                    If my4makandig = "2" Then finalstr = finalstr + "و" + " دوازده " + " هزار "
        '                    If my4makandig = "1" Then finalstr = finalstr + "و" + " يازده " + " هزار "
        '                    If my4makandig = "0" Then finalstr = finalstr + "و" + " ده " + " هزار "
        '                    loopx += 1
        '                End If
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 4 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نه " + " هزار "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشت " + " هزار "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفت " + " هزار "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شش " + " هزار "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنج " + " هزار "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهار " + " هزار "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سه " + " هزار "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دو " + " هزار "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يک " + " هزار "
        '                If dig = "0" Then
        '                    Try
        '                        If Mid(mynum, loopx - 2, 3) <> 0 Then
        '                            finalstr = finalstr + " هزار "
        '                        End If
        '                    Catch ex As Exception
        '                        finalstr = finalstr + " هزار "
        '                    End Try

        '                End If
        '            End If

        '            If makan = 3 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نهصد "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتصد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتصد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " ششصد "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پانصد "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهارصد "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سيصد "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دويست "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يکصد"
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 2 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نود "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشتاد "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفتاد "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شصت "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنجاه "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهل "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سي "
        '                If dig = "2" Then finalstr = finalstr + "و" + " بيست "
        '                If dig = "1" Then
        '                    Dim my1makandig As String = Mid(mynum, Len(mynum), 1)
        '                    If my1makandig = "9" Then finalstr = finalstr + "و" + " نوزده "
        '                    If my1makandig = "8" Then finalstr = finalstr + "و" + " هجده "
        '                    If my1makandig = "7" Then finalstr = finalstr + "و" + " هفده "
        '                    If my1makandig = "6" Then finalstr = finalstr + "و" + " شانزده "
        '                    If my1makandig = "5" Then finalstr = finalstr + "و" + " پانزده "
        '                    If my1makandig = "4" Then finalstr = finalstr + "و" + " چهارده "
        '                    If my1makandig = "3" Then finalstr = finalstr + "و" + " سيزده "
        '                    If my1makandig = "2" Then finalstr = finalstr + "و" + " دوازده "
        '                    If my1makandig = "1" Then finalstr = finalstr + "و" + " يازده "
        '                    If my1makandig = "0" Then finalstr = finalstr + "و" + " ده "
        '                    loopx += 1
        '                End If
        '                If dig = "0" Then varhichy = ""
        '            End If
        '            If makan = 1 Then
        '                If dig = "9" Then finalstr = finalstr + "و" + " نه "
        '                If dig = "8" Then finalstr = finalstr + "و" + " هشت "
        '                If dig = "7" Then finalstr = finalstr + "و" + " هفت "
        '                If dig = "6" Then finalstr = finalstr + "و" + " شش "
        '                If dig = "5" Then finalstr = finalstr + "و" + " پنج "
        '                If dig = "4" Then finalstr = finalstr + "و" + " چهار "
        '                If dig = "3" Then finalstr = finalstr + "و" + " سه "
        '                If dig = "2" Then finalstr = finalstr + "و" + " دو "
        '                If dig = "1" Then finalstr = finalstr + "و" + " يک "
        '                If dig = "0" Then varhichy = ""
        '            End If
        '        Next
        '        If Mid(finalstr, 1, 1) = "و" Then
        '            finalstr = Mid(finalstr, 2, Len(finalstr))
        '        End If
        '        Return finalstr
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function
        'تابع تکرار کردن يک کاراکتر يا رشته
        'Public Shared Function RepeatStr(ByVal Str As String, ByVal Counts As Int16) As String
        '    Dim myStr As String = ""
        '    Try
        '        For loopx As Int16 = 1 To Counts
        '            myStr += Str
        '        Next
        '        Return myStr
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function
        'تابع برعکس کردن يک رشته
        'Public Shared Function RevertStr(ByVal strr As String) As String
        '    Dim mystr As String
        '    Try
        '        For loopx As Int16 = strr.Length To 1 Step -1
        '            mystr = mystr + Mid(strr, loopx, 1)
        '        Next
        '        Return mystr
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function
        'روتين برگرداندن وضعيت کليد کپس لوک با استفاده از API
        'Private Declare Function GetKeyState Lib "user32.dll" (ByVal nVirtKey As Long) As Int16
        'Public Shared Function Getcapslockvz() As Boolean
        '    Try
        '        If (GetKeyState(&H14) And &H1) Then
        '            Return True
        '        Else
        '            Return False
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function
        'روتين تغيير زبان صفحه کليد با استفاده از API
        'Public Shared Function Setkeyboardlayout(ByVal YourInputLanguageType As R2Enums.InputLanguageType) As Boolean
        '    Try
        '        Return SetKeybLayout(YourInputLanguageType)
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function Setkeyboardlayout(ByVal layout As String) As Boolean
        '    Try
        '        Return SetKeybLayout(IIf(layout = "English", R2Enums.InputLanguageType.English, R2Enums.InputLanguageType.Persian))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Private Declare Function LoadKeyboardLayout Lib "user32" Alias "LoadKeyboardLayoutA" (ByVal pwszKLID As String, ByVal flags As Integer) As Integer
        'Private Shared Function SetKeybLayout(YourInputLanguageType As R2Enums.InputLanguageType) As Boolean
        '    Try
        '        Dim mypwszKLID As String = IIf(YourInputLanguageType = R2Enums.InputLanguageType.English, "00000409", "00000429")
        '        Dim myload As String = LoadKeyboardLayout(mypwszKLID, &H1)
        '        If myload = "" Then
        '            Return False
        '        Else
        '            Return True
        '        End If
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'حذف نقطه از مقادير عددي که بعد از نقطه 0 است
        'Public Shared Function R2ErasePoint(ByVal mypointdg As Double) As String
        '    Dim mydg As String = Trim(Str(mypointdg))
        '    If InStr(".", mydg) <= 0 Then Return mydg
        '    If Len(mydg < 3) Then Return mydg
        '    If (Mid(mydg, Len(mydg) - 1, 1) = ".") And (Mid(mydg, Len(mydg), 1) = "0") Then
        '        Return Mid(mydg, 1, Len(mydg) - 2)
        '    Else
        '        Return mydg
        '    End If
        'End Function
        'تابع کامادار کردن يک رشته عددي
        Public Shared Function R2MakeCamaYourDigit(ByVal xdig As UInt64) As String
            Dim myDigit As String
            Dim yourDigit As String
            Try
                myDigit = xdig
                If Len(myDigit) <= 3 Then Return xdig
                yourDigit = ""
                For loopx As Int16 = 1 To Math.Ceiling(Len(myDigit) / 3)
                    If loopx <> Math.Ceiling(Len(myDigit) / 3) Then
                        yourDigit = "," + Mid(myDigit, (Len(myDigit) - 3 * loopx) + 1, 3) + yourDigit
                    Else
                        yourDigit = Mid(myDigit, 1, Len(myDigit) - (Math.Ceiling(Len(myDigit) / 3) - 1) * 3) + yourDigit
                    End If
                Next
                Return yourDigit
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function
        'تفسير منفي يا مثبت بودن يك مبلغ
        Public Shared Function ParseSignDigitToTashString(ByVal YourDig As Int64) As String
            Try
                If YourDig < 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig)) + "  بدهكار  "
                If YourDig > 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig)) + "  بستانكار  "
                If YourDig = 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig)) + "  تسويه  "
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
            End Try
        End Function
        'تفسير منفي يا مثبت بودن يك مبلغ
        'Public Shared Function ParseSignDigitToSignString(ByVal YourDig As Int64) As String
        '    Try
        '        If YourDig < 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig)) + "-"
        '        If YourDig > 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig))
        '        If YourDig = 0 Then Return R2MakeCamaYourDigit(Math.Abs(YourDig))
        '    Catch ex As Exception
        '        Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
        '    End Try
        'End Function

        'Public Shared Function GetInvertColor(YourColor As Color) As Color
        '    Return Color.FromArgb(YourColor.ToArgb() Xor &HFFFFFF)
        'End Function

        Private Shared DaDiff As New SqlClient.SqlDataAdapter
        Private Shared DsDiff As New DataSet
        Public Shared Function GetDateDiff(YourDateInterval As Microsoft.VisualBasic.DateInterval, ByVal YourMilladiDate1 As Date, ByVal YourMilladiDate2 As Date) As Long
            Try
                If YourDateInterval = DateInterval.Day Then
                    DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Day ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
                ElseIf YourDateInterval = DateInterval.Minute Then
                    DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Minute ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
                ElseIf YourDateInterval = DateInterval.Hour Then
                    DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Hour ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
                ElseIf YourDateInterval = DateInterval.Second Then
                    DaDiff.SelectCommand = New SqlClient.SqlCommand("select DateDiff(Second ,'" & YourMilladiDate1.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ,'" & YourMilladiDate2.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) & "' ) AS DD")
                End If
                DaDiff.SelectCommand.Connection = R2PrimarySqlConnection.GetSubscriptionDBConnection
                DsDiff.Tables.Clear()
                DaDiff.Fill(DsDiff)
                Return DsDiff.Tables(0).Rows(0).Item("DD")
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

End Namespace
