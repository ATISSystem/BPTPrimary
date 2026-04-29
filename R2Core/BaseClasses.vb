

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Reflection

Namespace BaseClasses
    Public Class R2CoreEnums

        Public Enum EditLevel
            LowLevel = 1
            HighLevel = 2
        End Enum

        Public Enum SortOrder
            Code = 1
            Name = 2
        End Enum
    End Class

    Public Class R2CoreImage

        Private Property CurrentImage As Image = Nothing
        Public Sub New(YourImage As Image)
            Try
                CurrentImage = YourImage
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Public Sub New(YourByteArray As Byte())
            Try
                CurrentImage = ConvertByteArrayToImage(YourByteArray)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Sub

        Private Function ConvertByteArrayToImage(YourByteArray As Byte()) As Image
            Try
                If Not YourByteArray Is Nothing Then
                    Using MS As New MemoryStream(YourByteArray, 0, YourByteArray.Length)
                        MS.Write(YourByteArray, 0, YourByteArray.Length)
                        Return New Bitmap(Image.FromStream(MS))
                    End Using
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Private Function ConvertImageToByteArray(YourImage As Image) As Byte()
            Try
                Dim MS As New MemoryStream()
                Dim Photo As Bitmap = New Bitmap(YourImage, New Size(YourImage.Width, YourImage.Height))
                Photo.Save(MS, ImageFormat.Jpeg)
                Return MS.GetBuffer()
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetImage() As Image
            Try
                Return CurrentImage
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

        Public Function GetImageByte() As Byte()
            Try
                Return ConvertImageToByteArray(CurrentImage)
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function



    End Class

    Public Class R2CoreFile

        Public Sub New()
            FileName = String.Empty
        End Sub

        Public Sub New(YourFileName As String)
            FileName = YourFileName
        End Sub

        Public Property FileName() As String

    End Class



End Namespace
