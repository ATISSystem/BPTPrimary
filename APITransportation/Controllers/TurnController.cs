using APICommon;
using APITransportation.Models.Truck;
using APITransportation.Models.Turn;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.Cars;
using R2CoreParkingSystem.TrafficCardsManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.Trucks;
using R2CoreTransportationAndLoadNotification.TrucksNativeness;
using R2CoreTransportationAndLoadNotification.Turns;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TurnController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/xxx")]
        public HttpResponseMessage xxx()
        {
            try
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(string.Empty), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }


        }




    }
}
