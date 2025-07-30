using APITransportation.LoadCapacitor.Models.LoadCapacitor;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.TransportTarrifsParameters.Exceptions;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITransportation.LoadCapacitor.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadCapacitorController : ApiController
    {

        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetLoadsforTruckDriver")]
        public HttpResponseMessage GetLoadsforTruckDriver([FromBody] APITransportationSessionIdAnnouncementSGIdLoadStatusId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementSGId = Content.AnnouncementSGId;
                var LoadStatusId = Content.LoadStatusId;
                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadCapacitorLoad = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService());
                var Loads = InstanceLoadCapacitorLoad.GetLoadsforTruckDriver(R2CoreTransportationAndLoadNotificationRequesters.LoadCapacitorController_GetLoadsforTruckDriver, User, AnnouncementSGId, LoadStatusId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(Loads, Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTarrifParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetLoad")]
        public HttpResponseMessage GetLoad()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var AES = new AESAlgorithmsManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdLoadId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadCapacitorLoad = new R2CoreTransportationAndLoadNotificationLoadManager(new R2DateTimeService());
                InstanceLoadCapacitorLoad.GetLoad(LoadId, true);


                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceLoadCapacitorLoad.GetLoad(LoadId, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTarrifParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }
    }
}
