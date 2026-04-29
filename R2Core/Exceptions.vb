

Imports R2Core.DatabaseManagement
Imports R2Core.DateTimeProvider
Imports R2Core.PredefinedMessagesManagement
Imports System.Reflection
Imports System.Runtime.Remoting.Contexts
Imports System.Web.Services.Protocols
Imports System.Xml

Namespace ExceptionManagement

    'BPTChanged
    Public Class R2CoreRawExceptionMessage
        Public ErrorMessage As String
        Public ErrorMessageCode As Int64
    End Class

    'BPTChanged
    Public Class R2CoreExceptionsManager

    End Class

    'BPTChanged
    Public Class BPTException
        Inherits ApplicationException

        Protected _Message As String
        Protected _MessageCode As Int64
        Protected InstancePredefinedMessages As R2CorePredefinedMessagesManager
        Protected _DateTimeService As IDateTimeService
        Public Sub New()
            _DateTimeService = New R2DateTimeService
            InstancePredefinedMessages = New R2CorePredefinedMessagesManager(_DateTimeService)
        End Sub

        Public Sub New(YourException As Exception)
            InstancePredefinedMessages = New R2CorePredefinedMessagesManager(_DateTimeService)
            _Message = YourException.Message
            _MessageCode = R2CorePredefinedMessages.None
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

    'BPTChanged
    Public Class BPTSoapException
        Inherits BPTException

        Public Sub New(YourSoapException As SoapException)
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.BPTSoapException).MsgContent + vbCrLf + YourSoapException.Message
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2Core.PredefinedMessagesManagement.R2CorePredefinedMessages.BPTSoapException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class UserNotAlllowedException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UserNotAlllowedException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UserNotAlllowedException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class DataEntryException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.DataEntryException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.DataEntryException).MsgId
        End Sub

        Public Sub New(YourMessage As String)
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.DataEntryException).MsgContent + vbCrLf + YourMessage
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.DataEntryException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class FileNotExistException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotExistException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotExistException).MsgId
        End Sub

        Public Sub New(YourFilePath As String)
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotExistException).MsgContent + vbCrLf + YourFilePath
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.FileNotExistException).MsgId
        End Sub

    End Class

    'BPTChanged
    Public Class UploadedImageSizeExeededException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UploadedImageSizeExeededException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UploadedImageSizeExeededException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class PleaseReTryException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PleaseReTryException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.PleaseReTryException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class AnyNotFoundException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.AnyNotFoundException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.AnyNotFoundException).MsgId
        End Sub
    End Class

    'BPTChanged
    Public Class UnableConnectToAPIException
        Inherits BPTException

        Public Sub New()
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UnableConnectToAPIException).MsgContent
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UnableConnectToAPIException).MsgId
        End Sub

        Public Sub New(YourAPI As String)
            _Message = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UnableConnectToAPIException).MsgContent + vbCrLf + YourAPI
            _MessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.UnableConnectToAPIException).MsgId
        End Sub
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

End Namespace
