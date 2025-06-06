using Newtonsoft.Json;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;


namespace APICommon
{
    public class APICommon
    {

        public HttpResponseMessage CreateErrorContentMessage(Exception YourException)
        {
            var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            response.Content = new StringContent(JsonConvert.SerializeObject(new { ErrorMessage = YourException.Message, ErrorMessageCode = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.None).MsgId }), Encoding.UTF8, "application/json");
            return response;
        }

        public HttpResponseMessage CreateErrorContentMessage(BPTException YourException)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            response.Content = new StringContent(JsonConvert.SerializeObject(new { ErrorMessage = YourException.Message, ErrorMessageCode = YourException.MessageCode }), Encoding.UTF8, "application/json");
            return response;
        }

        public HttpResponseMessage CreateErrorContentMessage(SoapException YourException)
        {
            var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            response.Content = new StringContent(JsonConvert.SerializeObject(new { ErrorMessage = YourException.Message.Split('\n')[0].Split(':')[1], ErrorMessageCode = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.BPTSoapException).MsgId }), Encoding.UTF8, "application/json");
            return response;
        }

    }
}
