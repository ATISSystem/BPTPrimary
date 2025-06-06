using APICommon;
using Newtonsoft.Json;
using R2Core.ExceptionManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;


namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TransportationController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetCaptcha")]
        public HttpResponseMessage Getxxx()
        {
            try
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty ), Encoding.UTF8, "application/json");
                return response;
            }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
