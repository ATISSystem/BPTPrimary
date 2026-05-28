

Imports System.CodeDom
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading
Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider

Imports R2Core.ExceptionManagement
Imports R2Core.PredefinedMessagesManagement
Imports R2Core.SecurityAlgorithmsManagement.Exceptions
Imports R2Core.SecurityAlgorithmsManagement.ExpressionValidationAlgorithms.Exceptions
Imports R2Core.SoftwareUserManagement
Imports R2Core.SoftwareUserManagement.Exceptions
Imports R2Core.SQLInjectionPrevention

Namespace SecurityAlgorithmsManagement


    'BPTChanged
    Public Class ExchangeKey
        Public Sub New()
            ExchangeKey = Int64.MinValue
            UserId = Int64.MinValue
            StartDateTime = Now
        End Sub

        Public Sub New(YourExchangeKey As Int64, YourUserId As Int64, YourStartDateTime As DateTime)
            ExchangeKey = YourExchangeKey
            UserId = YourUserId
            StartDateTime = YourStartDateTime
        End Sub

        Public ExchangeKey As Int64
        Public UserId As Int64
        Public StartDateTime As DateTime
    End Class

    'BPTChanged
    Public Class ExchangeKeyManager

        Private _LstUsers As List(Of ExchangeKey)
        Private _DateTimeService As IDateTimeService
        Private _SoftwareUserService As ISoftwareUserService
        Private InstanceSoftwareUsers As R2CoreSoftwareUsersManager
        Private InstanceSecurityAlgorithms As R2CoreSecurityAlgorithmsManager

        Public Sub New(YourDateTimeService As IDateTimeService, YourSoftwareUserService As ISoftwareUserService)
            _LstUsers = New List(Of ExchangeKey)
            _DateTimeService = YourDateTimeService
            _SoftwareUserService = YourSoftwareUserService
            InstanceSoftwareUsers = New R2CoreSoftwareUsersManager(_DateTimeService, _SoftwareUserService)
            InstanceSecurityAlgorithms = New R2CoreSecurityAlgorithmsManager
        End Sub

        Public Function Login(YourUserShenaseh As String, YourUserPassword As String) As Int64
            Try
                Dim SoftwareUser = InstanceSoftwareUsers.GetUser(YourUserShenaseh, YourUserPassword)
                If _LstUsers.Exists(Function(x) x.UserId = SoftwareUser.UserId) Then
                    _LstUsers.Where(Function(x) x.UserId = SoftwareUser.UserId)(0).StartDateTime = Now
                    Return _LstUsers.Where(Function(x) x.UserId = SoftwareUser.UserId)(0).ExchangeKey
                End If
                Dim EKTemp = InstanceSecurityAlgorithms.GetNewExchangeKey()
                _LstUsers.Add(New ExchangeKey(EKTemp, SoftwareUser.UserId, Now))
                Return EKTemp
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As SqlInjectionException
                Throw ex
            Catch ex As SoftWareUserNotFoundException
                Throw ex
            Catch ex As UserIsNotActiveException
                Throw ex
            Catch ex As UserNotExistException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Sub AuthenticationExchangeKey(YourExchangeKey As Int64)
            Try
                If _LstUsers.Exists(Function(x) x.ExchangeKey = YourExchangeKey) Then
                    If DateDiff(DateInterval.Minute, _LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).StartDateTime, Now) <= 1 Then
                    Else
                        _LstUsers.Remove(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0))
                        Throw New ExchangeKeyTimeRangePassedException
                    End If
                Else
                    Throw New ExchangeKeyNotExistException
                End If
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Function GetUser(YourExchangeKey As Int64) As R2CoreSoftwareUser
            Try
                AuthenticationExchangeKey(YourExchangeKey)
                Return InstanceSoftwareUsers.GetUser(_LstUsers.Where(Function(x) x.ExchangeKey = YourExchangeKey)(0).UserId, False)
            Catch ex As FileNotExistException
                Throw ex
            Catch ex As DataBaseException
                Throw ex
            Catch ex As UserIdNotExistException
                Throw ex
            Catch ex As ExchangeKeyTimeRangePassedException
                Throw ex
            Catch ex As ExchangeKeyNotExistException
                Throw ex
            Catch ex As GetNSSException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Public Class R2CoreSecurityAlgorithmsManager
        Public Function GetNewExchangeKey() As Int64
            Try
                Dim RandGen As New Random
                Return RandGen.Next(10000, 1000000)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    'BPTChanged
    Namespace Hashing

        'BPTChanged
        Public Class SHAHasher

            Public Function GenerateSHA256String(ByVal inputString) As String
                Try
                    Dim sha256 As SHA256 = SHA256Managed.Create()
                    Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(inputString)
                    Dim hash As Byte() = sha256.ComputeHash(bytes)
                    Dim stringBuilder As New StringBuilder()

                    For i As Integer = 0 To hash.Length - 1
                        stringBuilder.Append(hash(i).ToString("x2"))
                    Next

                    Return stringBuilder.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GenerateSHA512String(ByVal inputString) As String
                Try
                    Dim sha512 As SHA512 = SHA512Managed.Create()
                    Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(inputString)
                    Dim hash As Byte() = sha512.ComputeHash(bytes)
                    Dim stringBuilder As New StringBuilder()

                    For i As Integer = 0 To hash.Length - 1
                        stringBuilder.Append(hash(i).ToString("x2"))
                    Next
                    Return stringBuilder.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function ComputeSha256Hash(YourrawData As String) As String
                Try
                    Dim sha256Hash As SHA256 = SHA256.Create()
                    Dim bytes As Byte() = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(YourrawData))
                    Dim builder As StringBuilder = New StringBuilder()
                    For i As Int64 = 0 To bytes.Length - 1
                        builder.Append(bytes(i).ToString("x2"))
                    Next
                    Return builder.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

        End Class

        'BPTChanged
        Public Class MD5Hasher
            Public Function GenerateMD5String(ByVal inputString) As String
                Try
                    Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider
                    Dim bytesToHash() As Byte = System.Text.Encoding.UTF8.GetBytes(inputString)
                    bytesToHash = md5Obj.ComputeHash(bytesToHash)
                    Dim strResult As String = ""
                    For Each b As Byte In bytesToHash
                        strResult += b.ToString("x2")
                    Next
                    Return strResult
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function
        End Class

    End Namespace

    'BPTChanged
    Namespace AESAlgorithms
        'BPTChanged
        Public Class AESAlgorithmsManager

            Private ValidChars As String
            Private ValidAlphabetic As String
            Private ValidOTP As String

            Public Sub New()
                ValidChars = "%$#@^&!~()-+*=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
                ValidAlphabetic = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
                ValidOTP = "123456789"
            End Sub

            Public Function GenerateRandomString(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim RandomString = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        RandomString.Append(ValidChars(Random.Next(0, ValidChars.Length - 1)))
                    Next
                    Return RandomString.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GenerateRandomStringAlphabetic(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim RandomString = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        RandomString.Append(ValidAlphabetic(Random.Next(0, ValidAlphabetic.Length - 1)))
                    Next
                    Return RandomString.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Private VerificationCodeValidChars As String = "%$#@^!~()-+*=abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"
            Public Function GenerateVerificationCode(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim SBVerificationCode = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        SBVerificationCode.Append(VerificationCodeValidChars(Random.Next(0, VerificationCodeValidChars.Length - 1)))
                    Next
                    Return SBVerificationCode.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GenerateOTPCode(YourLength As Int32) As String
                Try
                    Dim Random As Random = New Random(DateTime.UtcNow.Millisecond)
                    Dim SBOTPCode = New StringBuilder()
                    For Loopi As Int32 = 0 To YourLength - 1
                        SBOTPCode.Append(ValidOTP(Random.Next(0, ValidOTP.Length - 1)))
                    Next
                    Return SBOTPCode.ToString()
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetNonce(YourLength As Int32) As String
                Try
                    Return GenerateRandomString(YourLength)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetSalt(YourLength As Int32) As String
                Try
                    Return GenerateRandomString(YourLength)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function Encrypt(YourInput As String, YourKey As String) As String
                Try

                    Dim inputArray As Byte() = UTF8Encoding.UTF8.GetBytes(YourInput)
                    Dim AES As AesCryptoServiceProvider = New AesCryptoServiceProvider
                    AES.KeySize = 256
                    AES.Key = UTF8Encoding.UTF8.GetBytes(YourKey)
                    AES.Mode = CipherMode.ECB
                    AES.Padding = PaddingMode.PKCS7
                    AES.IV = {150, 9, 112, 39, 32, 5, 136, 254, 251, 43, 44, 191, 217, 236, 3, 106}
                    Dim cTransform As ICryptoTransform = AES.CreateEncryptor()
                    Dim resultArray As Byte() = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length)
                    AES.Clear()
                    Return Convert.ToBase64String(resultArray, 0, resultArray.Length)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function Decrypt(YourInput As String, YourKey As String) As String
                Try
                    Dim inputArray As Byte() = Convert.FromBase64String(YourInput)
                    Dim AES As AesCryptoServiceProvider = New AesCryptoServiceProvider()
                    AES.KeySize = 256
                    AES.Key = UTF8Encoding.UTF8.GetBytes(YourKey)
                    AES.Mode = CipherMode.ECB
                    AES.Padding = PaddingMode.PKCS7
                    AES.IV = {150, 9, 112, 39, 32, 5, 136, 254, 251, 43, 44, 191, 217, 236, 3, 106}
                    Dim cTransform As ICryptoTransform = AES.CreateDecryptor()
                    Dim resultArray As Byte() = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length)
                    AES.Clear()
                    Return UTF8Encoding.UTF8.GetString(resultArray)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function

            Public Function GetRandomNumericCode(YourFirstValue As Int64, YourSecondValue As Int64) As Int64
                Try
                    Thread.Sleep(1)
                    Dim RandGen As New Random(DateTime.Now.Millisecond)
                    Return RandGen.Next(YourFirstValue, YourSecondValue)
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Function





        End Class

    End Namespace

    'BPTChanged
    Namespace Captcha

        Public Class ImageRawData
            Public IRawData As Byte()
        End Class

        Public Class R2CoreCaptchaManager

            Dim bmpCaptcha As Bitmap
            Dim iBMPHeight As Integer = 50
            Dim iBMPWidth As Integer = 200
            Dim sLeftMargin As Single = 1
            Dim sTopMargin As Single = 1
            Dim g As Graphics
            Dim sWord As String
            Dim sLetter As String
            Dim sfLetter As SizeF
            Dim rfLetter As RectangleF
            Dim sX1 As Single = 0
            Dim sY1 As Single = 0
            Dim sX2 As Single = 0
            Dim sY2 As Single = 0
            Dim sTemp As Single
            Dim iAngle As Integer
            Dim sOffset As Single

            Public Function GenerateCaptcha(ByVal sWord As String) As Bitmap
                Dim ixr As Integer
                bmpCaptcha = New Bitmap(iBMPWidth, iBMPHeight,
                        Drawing.Imaging.PixelFormat.Format16bppRgb555)
                g = Graphics.FromImage(bmpCaptcha)
                Dim drawBackground As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(drawBackground, New Rectangle(0, 0, iBMPWidth, iBMPHeight))

                ' Create font and brush.
                Dim drawFont As New System.Drawing.Font("Alborz Titr", 32, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
                Dim drawBrush As New SolidBrush(Color.Red)
                Dim strFormat As New StringFormat(StringFormatFlags.FitBlackBox)
                sfLetter = New SizeF(40, 40)
                ' Draw string to screen.
                For ixr = 0 To sWord.Length - 1
                    sLetter = sWord.Substring(ixr, 1)
                    g = Graphics.FromImage(bmpCaptcha)
                    rfLetter = New RectangleF(sLeftMargin, sTopMargin, 30, 30)
                    sfLetter = g.MeasureString(sLetter, drawFont, sfLetter, strFormat)
                    iAngle = RndInterval(0, 20) - 10
                    With rfLetter
                        sOffset = sLeftMargin * Math.Tan(iAngle * Math.PI / 180)
                        .Y = sTopMargin - sOffset
                        .Width = sfLetter.Width + 10
                        .Height = sfLetter.Height
                    End With
                    g.RotateTransform(iAngle)
                    g.DrawString(sLetter, drawFont, drawBrush, rfLetter)
                    sLeftMargin += sfLetter.Width + 2
                Next
                Dim drawPen As Pen = New Pen(Color.Crimson, 2)
                For ixr = 0 To 3
                    sX1 = sX2
                    Do While Math.Abs(sX1 - sX2) < iBMPWidth * 0.5
                        sX1 = RndInterval(2, iBMPWidth - 2)
                        sX2 = RndInterval(2, iBMPWidth - 2)
                    Loop
                    sY1 = sY2
                    Do While Math.Abs(sY1 - sY2) < iBMPHeight * 0.5
                        sY1 = RndInterval(2, iBMPHeight - 2)
                        sY2 = RndInterval(2, iBMPHeight - 2)
                    Loop
                    If RndInterval(0, 2) > 1 Then
                        sTemp = sX1
                        sX1 = sX2
                        sX2 = sTemp
                    End If
                    If RndInterval(0, 2) > 1 Then
                        sTemp = sY1
                        sY1 = sY2
                        sY2 = sTemp
                    End If

                    g.DrawLine(drawPen, sX1, sY1, sX2, sY2)
                Next
                g.Dispose()
                Return bmpCaptcha
            End Function

            Public Class AESAlgorithms

                Public Function ImageToBytes(value As Image) As Byte()
                    Dim converter As New ImageConverter()
                    Dim arr As Byte() = CType(converter.ConvertTo(value, GetType(Byte())), Byte())
                    Return arr
                End Function

                Public Function BytesToImage(value As Byte()) As Image
                    Using ms As New MemoryStream(value)
                        Return Image.FromStream(ms)
                    End Using
                End Function

                Public Function EncodeBytes(value As Byte()) As String
                    Return Convert.ToBase64String(value)
                End Function

                Public Function DecodeBytes(value As String) As Byte()
                    Return Convert.FromBase64String(value)
                End Function

                Public Function StringHash(value As Byte()) As String
                    Using sha256 As SHA256 = SHA256.Create()
                        Dim hashBytes As Byte() = sha256.ComputeHash(value)
                        Dim sb As New StringBuilder()
                        For i As Integer = 0 To hashBytes.Length - 1
                            sb.Append(hashBytes(i).ToString("X2"))
                        Next
                        Return sb.ToString().ToLower()
                    End Using
                End Function

            End Class

            Public Class JsonImage
                Private _AESAlgorithms As New AESAlgorithms()

                Public Property hash As String = String.Empty
                Public Property len As Integer = 0
                Public Property image As String = String.Empty

                Public Sub New()
                End Sub

                Public Sub New(value As Image)
                    Dim img_sources As Byte() = _AESAlgorithms.ImageToBytes(value)
                    hash = _AESAlgorithms.StringHash(img_sources)
                    len = img_sources.Length
                    image = _AESAlgorithms.EncodeBytes(img_sources)
                End Sub

                Public Function GetRawData() As Byte()
                    Dim data As Byte() = _AESAlgorithms.DecodeBytes(image)

                    If data.Length <> len Then
                        Throw New Exception("Error data len")
                    End If

                    If Not _AESAlgorithms.StringHash(data).Equals(hash) Then
                        Throw New Exception("Error hash")
                    End If

                    Return data
                End Function
            End Class

            Public Function GetCaptcha(ByVal sWord As String) As Byte()
                Try
                    'Dim InstanceConfiguration = New R2CoreInstanceConfigurationManager(_DateTimeService)
                    'Dim Captcha = GenerateFakeWordNumeric(InstanceConfiguration.GetConfigInt64(R2CoreConfigurations.DefaultConfigurationOfSoftwareUserSecurity, 6))
                    Dim CaptchaImage = GenerateCaptcha(sWord)
                    Dim IImage As ImageRawData = New ImageRawData
                    IImage.IRawData = (New JsonImage(CaptchaImage)).GetRawData()
                    Return IImage.IRawData
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message)
                End Try
            End Function

            Public Function GenerateFakeWord(ByVal iLengthRequired As Integer) As String
                Dim sVowels As String = "AEIOU"
                Dim sConsonants As String = "BCDFGHJKLMNPQRSTVWXYZ"
                Dim iNbrVowels As Integer
                Dim iNbrConsonants As Integer
                Dim sWord As String
                Dim bUseVowel As Boolean
                Dim iWordLength As Integer
                Dim sPattern As String
                iNbrVowels = 0
                iNbrConsonants = 0
                bUseVowel = False
                sWord = ""
                Randomize(DateTime.UtcNow.Millisecond)
                For iWordLength = 1 To iLengthRequired
                    If (iWordLength = 2) Or ((iLengthRequired > 1) And (iWordLength = iLengthRequired)) Then
                        bUseVowel = Not bUseVowel
                    ElseIf (iNbrVowels < 2) And (iNbrConsonants < 2) Then
                        bUseVowel = ((Rnd(1) * 2) > 1)
                    ElseIf (iNbrVowels < 2) Then
                        bUseVowel = True
                    ElseIf (iNbrConsonants < 2) Then
                        bUseVowel = False
                    End If

                    sPattern = IIf(bUseVowel, sVowels, sConsonants)
                    sWord = sWord & sPattern.Substring(Int(Rnd(1) * sPattern.Length), 1)
                    If bUseVowel Then
                        iNbrVowels = iNbrVowels + 1
                        iNbrConsonants = 0
                    Else
                        iNbrVowels = 0
                        iNbrConsonants = iNbrConsonants + 1
                    End If
                Next
                Return sWord
            End Function

            Public Function GenerateFakeWordNumeric(ByVal iLengthRequired As Integer) As String
                Dim sVowels As String = "98765"
                Dim sConsonants As String = "0123456789"
                Dim iNbrVowels As Integer
                Dim iNbrConsonants As Integer
                Dim sWord As String
                Dim bUseVowel As Boolean
                Dim iWordLength As Integer
                Dim sPattern As String
                iNbrVowels = 0
                iNbrConsonants = 0
                bUseVowel = False
                sWord = ""
                Randomize(DateTime.UtcNow.Millisecond)
                For iWordLength = 1 To iLengthRequired
                    If (iWordLength = 2) Or ((iLengthRequired > 1) And (iWordLength = iLengthRequired)) Then
                        bUseVowel = Not bUseVowel
                    ElseIf (iNbrVowels < 2) And (iNbrConsonants < 2) Then
                        bUseVowel = ((Rnd(1) * 2) > 1)
                    ElseIf (iNbrVowels < 2) Then
                        bUseVowel = True
                    ElseIf (iNbrConsonants < 2) Then
                        bUseVowel = False
                    End If

                    sPattern = IIf(bUseVowel, sVowels, sConsonants)
                    sWord = sWord & sPattern.Substring(Int(Rnd(1) * sPattern.Length), 1)
                    If bUseVowel Then
                        iNbrVowels = iNbrVowels + 1
                        iNbrConsonants = 0
                    Else
                        iNbrVowels = 0
                        iNbrConsonants = iNbrConsonants + 1
                    End If
                Next
                Return sWord
            End Function

            Private Function RndInterval(ByVal iMin As Integer, ByVal iMax As Integer) As Integer
                Randomize()
                Return Int(((iMax - iMin + 1) * Rnd()) + iMin)
            End Function

        End Class

    End Namespace

    Namespace PasswordStrength
        Public Class PasswordStrength
            Private dtDetails As DataTable
            Private PreviousPassword As String = ""

            Public ReadOnly Property VeryWeak As String = "Very Weak"
            Public ReadOnly Property Weak As String = "Weak"
            Public ReadOnly Property Good As String = "Good"
            Public ReadOnly Property Strong As String = "Strong"
            Public ReadOnly Property VeryStrong As String = "Very Strong"

            Public Sub SetPassword(pwd As String)
                If (PreviousPassword <> pwd) Then
                    PreviousPassword = pwd
                    CheckPasswordWithDetails(PreviousPassword)
                End If
            End Sub

            Public Function GetPasswordScore() As Integer
                If dtDetails IsNot Nothing Then
                    Return dtDetails.Rows(0)(5)
                Else
                    Return 0
                End If
            End Function

            Public Function GetPasswordStrength() As String
                If dtDetails IsNot Nothing Then
                    Return dtDetails.Rows(0)(3)
                Else
                    Return "Unknown"
                End If
            End Function

            Public Function GetStrengthDetails() As DataTable
                Return dtDetails
            End Function

            Private Sub CheckPasswordWithDetails(pwd As String)
                Dim nScore = 0
                Dim sComplexity As String = ""
                Dim iUpperCase As Integer = 0
                Dim iLowerCase As Integer = 0
                Dim iDigit As Integer = 0
                Dim iSymbol As Integer = 0
                Dim iRepeated As Integer = 1
                Dim htRepeated As Hashtable = New Hashtable()
                Dim iMiddle As Integer = 0
                Dim iMiddleEx As Integer = 1
                Dim ConsecutiveMode As Integer = 0
                Dim iConsecutiveUpper As Integer = 0
                Dim iConsecutiveLower As Integer = 0
                Dim iConsecutiveDigit As Integer = 0
                Dim iLevel As Integer = 0
                Dim sAlphas As String = "abcdefghijklmnopqrstuvwxyz"
                Dim sNumerics As String = "01234567890"
                Dim nSeqAlpha As Integer = 0
                Dim nSeqChar As Integer = 0
                Dim nSeqNumber As Integer = 0

                CreateDetailsTable()
                iLevel += 1
                Dim drScore As DataRow = AddDetailsRow(iLevel, "Score", "", "", 0, 0)

                ' Scan password
                For Each ch As Char In pwd.ToCharArray()
                    ' Count digits
                    If (Char.IsDigit(ch)) Then
                        iDigit += 1
                        If (ConsecutiveMode = 3) Then iConsecutiveDigit += 1
                        ConsecutiveMode = 3
                    End If

                    ' Count uppercase characters
                    If (Char.IsUpper(ch)) Then
                        iUpperCase += 1
                        If (ConsecutiveMode = 1) Then iConsecutiveUpper += 1
                        ConsecutiveMode = 1
                    End If

                    ' Count lowercase characters
                    If (Char.IsLower(ch)) Then
                        iLowerCase += 1
                        If (ConsecutiveMode = 2) Then iConsecutiveLower += 1
                        ConsecutiveMode = 2
                    End If

                    ' Count symbols
                    If (Char.IsSymbol(ch) Or Char.IsPunctuation(ch)) Then
                        iSymbol += 1
                        ConsecutiveMode = 0
                    End If

                    ' Count repeated letters 
                    If (Char.IsLetter(ch)) Then
                        If (htRepeated.Contains(Char.ToLower(ch))) Then
                            iRepeated += 1
                        Else
                            htRepeated.Add(Char.ToLower(ch), 0)
                        End If
                        If (iMiddleEx > 1) Then iMiddle = iMiddleEx - 1
                    End If

                    If (iUpperCase > 0 Or iLowerCase > 0) Then
                        If (Char.IsDigit(ch) Or Char.IsSymbol(ch)) Then iMiddleEx += 1
                    End If
                Next

                ' Check for sequential alpha string patterns (forward And reverse) 
                For s As Integer = 0 To 23 - 1
                    Dim sFwd As String = sAlphas.Substring(s, 3)
                    Dim sRev As String = strReverse(sFwd)
                    If (pwd.ToLower().IndexOf(sFwd) <> -1 Or pwd.ToLower().IndexOf(sRev) <> -1) Then
                        nSeqAlpha += 1
                        nSeqChar += 1
                    End If
                Next

                ' Check for sequential numeric string patterns (forward And reverse)
                For s As Integer = 0 To 8 - 1
                    Dim sFwd As String = sNumerics.Substring(s, 3)
                    Dim sRev As String = strReverse(sFwd)
                    If (pwd.ToLower().IndexOf(sFwd) <> -1 Or pwd.ToLower().IndexOf(sRev) <> -1) Then
                        nSeqNumber += 1
                        nSeqChar += 1
                    End If
                Next

                ' Calcuate score
                iLevel += 1
                AddDetailsRow(iLevel, "Additions", "", "", 0, 0)

                ' Score += 4 * Password Length
                nScore = 4 * pwd.Length
                iLevel += 1
                AddDetailsRow(iLevel, "Password Length", "Flat", "(n*4)", pwd.Length, pwd.Length * 4)

                ' if we have uppercase letetrs Score +=(number of uppercase letters *2)
                If (iUpperCase > 0) Then
                    nScore += ((pwd.Length - iUpperCase) * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, ((pwd.Length - iUpperCase) * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Uppercase Letters", "Cond/Incr", "+((len-n)*2)", iUpperCase, 0)
                End If

                ' if we have lowercase letetrs Score +=(number of lowercase letters *2)
                If (iLowerCase > 0) Then
                    nScore += ((pwd.Length - iLowerCase) * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, ((pwd.Length - iLowerCase) * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Lowercase Letters", "Cond/Incr", "+((len-n)*2)", iLowerCase, 0)
                End If

                ' Score += (Number of digits *4)
                nScore += (iDigit * 4)
                iLevel += 1
                AddDetailsRow(iLevel, "Numbers", "Cond", "+(n*4)", iDigit, (iDigit * 4))

                ' Score += (Number of Symbols * 6)
                nScore += (iSymbol * 6)
                iLevel += 1
                AddDetailsRow(iLevel, "Symbols", "Flat", "+(n*6)", iSymbol, (iSymbol * 6))

                ' Score += (Number of digits Or symbols in middle of password *2)
                nScore += (iMiddle * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Middle Numbers or Symbols", "Flat", "+(n*2)", iMiddle, (iMiddle * 2))

                'requirments
                Dim requirments As Integer = 0
                If (pwd.Length >= 8) Then requirments += 1     ' Min password length
                If (iUpperCase > 0) Then requirments += 1      ' Uppercase letters
                If (iLowerCase > 0) Then requirments += 1      ' Lowercase letters
                If (iDigit > 0) Then requirments += 1          ' Digits
                If (iSymbol > 0) Then requirments += 1         ' Symbols

                ' If we have more than 3 requirments then
                If (requirments > 3) Then
                    ' Score += (requirments *2) 
                    nScore += (requirments * 2)
                    iLevel += 1
                    AddDetailsRow(iLevel, "Requirments", "Flat", "+(n*2)", requirments, (requirments * 2))
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Requirments", "Flat", "+(n*2)", requirments, 0)
                End If


                ' Deductions
                iLevel += 1
                AddDetailsRow(iLevel, "Deductions", "", "", 0, 0)

                ' If only letters then score -=  password length
                If (iDigit = 0 And iSymbol = 0) Then
                    nScore -= pwd.Length
                    iLevel += 1
                    AddDetailsRow(iLevel, "Letters only", "Flat", "-n", pwd.Length, -pwd.Length)
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Letters only", "Flat", "-n", 0, 0)
                End If

                ' If only digits then score -=  password length
                If (iDigit = pwd.Length) Then
                    nScore -= pwd.Length
                    iLevel += 1
                    AddDetailsRow(iLevel, "Numbers only", "Flat", "-n", pwd.Length, -pwd.Length)
                Else
                    iLevel += 1
                    AddDetailsRow(iLevel, "Numbers only", "Flat", "-n", 0, 0)
                End If

                ' If repeated letters used then score -= (iRepeated * (iRepeated - 1));
                If (iRepeated > 1) Then
                    nScore -= (iRepeated * (iRepeated - 1))
                    iLevel += 1
                    AddDetailsRow(iLevel, "Repeat Characters (Case Insensitive)", "Incr", "-(n(n-1))", iRepeated, -(iRepeated * (iRepeated - 1)))
                End If

                ' If Consecutive uppercase letters then score -= (iConsecutiveUpper * 2);
                nScore -= (iConsecutiveUpper * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Uppercase Letters", "Flat", "-(n*2)", iConsecutiveUpper, -(iConsecutiveUpper * 2))

                ' If Consecutive lowercase letters then score -= (iConsecutiveUpper * 2);
                nScore -= (iConsecutiveLower * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Lowercase Letters", "Flat", "-(n*2)", iConsecutiveLower, -(iConsecutiveLower * 2))

                ' If Consecutive digits used then score -= (iConsecutiveDigits* 2);
                nScore -= (iConsecutiveDigit * 2)
                iLevel += 1
                AddDetailsRow(iLevel, "Consecutive Numbers", "Flat", "-(n*2)", iConsecutiveDigit, -(iConsecutiveDigit * 2))

                ' If password contains sequence of letters then score -= (nSeqAlpha * 3)
                nScore -= (nSeqAlpha * 3)
                iLevel += 1
                AddDetailsRow(iLevel, "Sequential Letters (3+)", "Flat", "-(n*3)", nSeqAlpha, -(nSeqAlpha * 3))

                ' If password contains sequence of digits then score -= (nSeqNumber * 3)
                nScore -= (nSeqNumber * 3)
                iLevel += 1
                AddDetailsRow(iLevel, "Sequential Numbers (3+)", "Flat", "-(n*3)", nSeqNumber, -(nSeqNumber * 3))

                ' Determine complexity based on overall score 
                If (nScore > 100) Then
                    nScore = 100
                ElseIf (nScore < 0) Then
                    nScore = 0
                End If
                If (nScore >= 0 And nScore < 20) Then
                    sComplexity = "Very Weak"
                ElseIf (nScore >= 20 And nScore < 40) Then
                    sComplexity = "Weak"
                ElseIf (nScore >= 40 And nScore < 60) Then
                    sComplexity = "Good"
                ElseIf (nScore >= 60 And nScore < 80) Then
                    sComplexity = "Strong"
                ElseIf (nScore >= 80 And nScore <= 100) Then
                    sComplexity = "Very Strong"
                End If

                ' Store score And complexity in dataset
                drScore(5) = nScore
                drScore(3) = sComplexity
                dtDetails.AcceptChanges()
            End Sub

            Private Sub CreateDetailsTable()
                dtDetails = New DataTable("PasswordDetails")
                dtDetails.Columns.Add(New DataColumn("Level", System.Type.GetType("System.Int32")))
                dtDetails.Columns.Add(New DataColumn("Description", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Type", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Rate", System.Type.GetType("System.String")))
                dtDetails.Columns.Add(New DataColumn("Count", System.Type.GetType("System.Int32")))
                dtDetails.Columns.Add(New DataColumn("Bonus", System.Type.GetType("System.Int32")))
            End Sub

            Private Function AddDetailsRow(Id As Integer, Description As String, Type As String, Rate As String, Count As Integer, Bouns As Integer) As DataRow
                Dim dr As DataRow = dtDetails.NewRow()
                dr(0) = Id
                dr(1) = Description
                dr(2) = Type
                dr(3) = Rate
                dr(4) = Count
                dr(5) = Bouns
                dtDetails.Rows.Add(dr)
                dtDetails.AcceptChanges()
                Return dr
            End Function

            Private Function strReverse(str As String) As String
                Dim newstring As String = ""
                For s As Integer = 0 To str.Length - 1
                    newstring = str(s) + newstring
                Next
                Return newstring
            End Function



        End Class



    End Namespace

    'BPTChanged
    Namespace ExpressionValidationAlgorithms
        Public Class ExpressionValidationAlgorithmsManager
            Public Sub ValidationMobileNumber(YourValue As String)
                Try
                    If YourValue.Length <> 11 Then Throw New MobileNumberIsInvalidException
                    If Not IsNumeric(YourValue) Then Throw New MobileNumberIsInvalidException
                Catch ex As MobileNumberIsInvalidException
                    Throw ex
                Catch ex As Exception
                    Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
                End Try
            End Sub
        End Class

        Namespace Exceptions
            'BPTChanged
            Public Class MobileNumberIsInvalidException
                Inherits BPTException

                Public Sub New()
                    _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.MobileNumberIsInvalid).MsgContent
                    _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.MobileNumberIsInvalid).MsgId
                End Sub
            End Class

        End Namespace

    End Namespace

    'BPTChanged
    Namespace Exceptions

        Public Class CaptchaWordNotCorrectException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CaptchaWordNotCorrectException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.CaptchaWordNotCorrectException).MsgId
            End Sub
        End Class

        Public Class ExchangeKeyNotExistException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ExchangeKeyNotExistException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ExchangeKeyNotExistException).MsgId
            End Sub
        End Class

        Public Class ExchangeKeyTimeRangePassedException
            Inherits BPTException

            Public Sub New()
                _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ExchangeKeyNotExistException).MsgContent
                _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.ExchangeKeyNotExistException).MsgId
            End Sub
        End Class



    End Namespace


End Namespace
