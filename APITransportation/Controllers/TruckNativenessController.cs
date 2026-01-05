using APICommon.Models;
using APITransportation.Models.Truck;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.RequestersManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.Trucks.Exceptions;
using R2CoreTransportationAndLoadNotification.TrucksNativeness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TruckNativenessController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public TruckNativenessController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetTruckNativenessTypes")]
        public HttpResponseMessage GetTruckNativenessTypes([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckNativeness = new R2CoreTransportationAndLoadNotificationsTruckNativenessManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTruckNativeness.GetTruckNativenessTypes(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTruckNativeness")]
        public HttpResponseMessage GetTruckNativeness([FromBody] APITransportationSessionIdTruckId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckNativeness = new R2CoreTransportationAndLoadNotificationsTruckNativenessManager(_DateTimeService );
                var TruckNativenessExtended = InstanceTruckNativeness.GetTruckNativeness(TruckId, true);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(TruckNativenessExtended), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TruckNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ChangeTruckNativeness")]
        public HttpResponseMessage ChangeTruckNativeness([FromBody] APITransportationSessionIdTruckIdTruckNativenessExpireDate Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var TruckNativenessExpireDate = Content.TruckNativenessExpireDate;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckNativeness = new R2CoreTransportationAndLoadNotificationsTruckNativenessManager(_DateTimeService );
                var TruckNativenessExtended = InstanceTruckNativeness.ChangeTruckNativeness(TruckId, TruckNativenessExpireDate);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(TruckNativenessExtended), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
