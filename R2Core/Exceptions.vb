

Imports R2Core.DatabaseManagement
Imports R2Core.PredefinedMessagesManagement
Imports System.Reflection
Imports System.Runtime.Remoting.Contexts
Imports System.Web.Services.Protocols
Imports System.Xml

Namespace ExceptionManagement

    Public Class BPTException
        Inherits ApplicationException

        Protected _Message As String
        Protected _MessageCode As Int64
        Protected InstancePredefinedMessages As R2CoreMClassPredefinedMessagesManager

        Public Sub New()
            InstancePredefinedMessages = New R2CoreMClassPredefinedMessagesManager
        End Sub

        Public Sub New(YourException As Exception)
            _Message = YourException.Message
            _MessageCode = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.None).MsgId
        End Sub

        Public Overrides ReadOnly Property Message As String
            Get
                Return _Message
            End Get
        End Property

        Public ReadOnly Property MessageCode As Int64
            Get
                Return _MessageCode
            End Get
        End Property
    End Class

    Public Class BPTSoapException
        Inherits BPTException

        Public Sub New(YourSoapException As SoapException)
            _Message = YourSoapException.Message
            _MessageCode = InstancePredefinedMessages.GetNSS(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.BPTSoapException).MsgId
        End Sub
    End Class

    Public MustInherit Class R2CoreMClassExceptionsManagement
        Public Shared Function GetSqlExceptionMessage(YourSqlExceptionId As Int64) As String
            Try
                Dim Ds As DataSet
                If R2ClassSqlDataBOXManagement.GetDataBOX(New R2PrimarySqlConnection, "Select Top 1 Message from R2Primary.dbo.TblSqlExceptions Where SEId=" & YourSqlExceptionId & " Order By SEId Asc", 3600, Ds, New Boolean).GetRecordsCount() = 0 Then Throw New SqlExceptionNotFoundException
                Return Ds.Tables(0).Rows(0).Item("Message")
            Catch ex As SqlExceptionNotFoundException
                Throw ex
            Catch ex As Exception
                Throw New Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + vbCrLf + ex.Message)
            End Try
        End Function

    End Class

    Public Class UserNotAlllowedException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "کاربر مجاز به اجرای این فرآیند نیست"
            End Get
        End Property
    End Class

    Public Class SqlExceptionNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اس کیو ال اکسپشن با کد شاخص مورد نظر یافت نشد"
            End Get
        End Property
    End Class

    Public Class InvalidEntryAmountException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "مقدار وارد شده نادرست است"
            End Get
        End Property
    End Class

    Public Class InvalidEntryNumberException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "تعداد وارد شده نادرست است"
            End Get
        End Property
    End Class

    Public Class GetNSSException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اطلاعات مورد نظر یافت نشد"
            End Get
        End Property
    End Class

    Public Class GetDataException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "داده های مورد نظر یافت نشد"
            End Get
        End Property
    End Class

    Public Class DataEntryException
        Inherits ApplicationException

        Private _Message = String.Empty

        Public Sub New()
            _Message = "اطلاعات وارد شده صحیح نیست،کلیه اطلاعات وارد شده را بررسی کنید"
        End Sub

        Public Sub New(YourMessage As String)
            _Message = YourMessage
        End Sub

        Public Overrides ReadOnly Property Message As String
            Get
                Return _Message
            End Get
        End Property
    End Class

    Public Class FileNotFoundException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "فایل موجود نیست"
            End Get
        End Property
    End Class

    Public Class UserNotAllowedRunThisProccessException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "کاربر مجوز دسترسی ندارد"
            End Get
        End Property
    End Class

    Public Class UploadedImageSizeExeededException
        Inherits ApplicationException
        Public Overrides ReadOnly Property Message As String
            Get
                Return "اندازه تصویر ارسال شده بیش از حد مجاز است"
            End Get
        End Property
    End Class

    Public Class PleaseReTryException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.PleaseReTryException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.PleaseReTryException).MsgId
        End Sub
    End Class

    Public Class AnyNotFoundException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.AnyNotFoundException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.AnyNotFoundException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class R2CoreExceptionsManager

    End Class


End Namespace
