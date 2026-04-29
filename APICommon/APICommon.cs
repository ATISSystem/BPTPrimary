using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;


namespace APICommon
{
    public class APICommon
    {
        private R2DateTimeService _DateTimeService;

        public APICommon()
        { _DateTimeService = new R2DateTimeService(); }

        public HttpResponseMessage CreateErrorContentMessage(Exception YourException)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = YourException.Message, ErrorMessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.None).MsgId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage =((BPTException)ex).Message, ErrorMessageCode = ((BPTException)ex).MessageCode}), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = ex.Message, ErrorMessageCode = 0 }), Encoding.UTF8, "application/json");
                return response;
            }
        }

        public HttpResponseMessage CreateErrorContentMessage(BPTException YourException)
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = YourException.Message, ErrorMessageCode = YourException.MessageCode }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = ex.Message, ErrorMessageCode = 0 }), Encoding.UTF8, "application/json");
                return response;
            }
        }

        public HttpResponseMessage CreateErrorContentMessage(SoapException YourException)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = YourException.Message.Split('\n')[0].Split(':')[1], ErrorMessageCode = InstancePredefinedMessages.GetPredefinedMessage(R2CorePredefinedMessages.BPTSoapException).MsgId }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = ((BPTException)ex).Message, ErrorMessageCode = ((BPTException)ex).MessageCode }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(new R2CoreRawExceptionMessage { ErrorMessage = ex.Message, ErrorMessageCode = 0 }), Encoding.UTF8, "application/json");
                return response;
            }
        }


    }
}
