using APICommon;
using APITransportation.Models;
using APITransportation.PayanehWebService;
using Newtonsoft.Json;
using R2Core.DateAndTimeManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2Core.WebProcessesManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
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
    public class TruckDriverController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private PayanehWebService.PayanehWebService _WS = new PayanehWebService.PayanehWebService();

        [HttpPost]
        [Route("api/GetTruckDriverfromRMTO")]
        public HttpResponseMessage GetTruckDriverfromRMTO()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var AES = new AESAlgorithmsManager ();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckDriverNationalCode>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckDriverNationalCode = Content.TruckDriverNationalCode;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var TruckDriverId = _WS.WebMethodGetDriverTruckByNationalCodefromRMTO(TruckDriverNationalCode, _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));
                var TruckDriver = InstanceTruckDrivers.GetTruckDriver(TruckDriverId, true);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(TruckDriver, Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
