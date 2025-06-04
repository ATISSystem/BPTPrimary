using Newtonsoft.Json;
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
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            if (YourException.GetType() == typeof(SoapException))
            { response.Content = new StringContent(JsonConvert.SerializeObject(YourException.Message.Split('\n')[0].Split(':')[1]), Encoding.UTF8, "application/json"); }
            else
            { response.Content = new StringContent(JsonConvert.SerializeObject(YourException.Message), Encoding.UTF8, "application/json"); }
            return response;
        }
    }
}
