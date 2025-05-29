using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Text;
using APITransportation.Models;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.LoadAnnouncementPlaces;
using System.Web.Services.Protocols;


namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadAnnouncementPlacesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpGet]
        [Route("api/GetLoadAnnouncementPlaces")]
        public HttpResponseMessage GetLoadAnnouncementPlaces()
        {
            try
            {
                //var InstanceSession = new R2CoreSessionManager();
                //var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                //var Content = JsonConvert.DeserializeObject<R2CoreSessionIdStructure>(Request.Content.ReadAsStringAsync().Result);
                //var SessionId = Content.SessionId;

                //var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadAnnouncementPlaces = new R2CoreTransportationAndLoadNotificationLoadAnnouncementPlacesManager();
                var LoadAnnouncementPlaces = InstanceLoadAnnouncementPlaces.GetLoadAnnouncementPlaces();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(LoadAnnouncementPlaces, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }


        }

    }
}
