using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SoftwareUser
{
    public class SoftwareUserManager
    {
        public HttpResponseMessage CreateErrorContentMessage(Exception YourException)
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            response.Content = new StringContent(JsonConvert.SerializeObject(YourException.Message), Encoding.UTF8, "application/json");
            return response;
        }

    }
}