using APITraffic.Models;
using Newtonsoft.Json;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.Traffic;
using R2CoreParkingSystem.TrafficCardsManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITraffic.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TrafficController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;

        public TrafficController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/RegisteringTraffic")]
        public HttpResponseMessage RegisteringTraffic([FromBody] APITrafficSessionIdTrafficGateIdTrafficCardNoTrafficPicture Content)
        {
            try
            {
                var InstanceTraffic = new R2CoreParkingSystemTrafficManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficCardNo = Content.TrafficCardNo;
                var TrafficGateId = Content.TrafficGateId;
                var TrafficPicture = Content.TrafficPicture;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTraffic.RegisteringTraffic(TrafficGateId, TrafficCardNo, TrafficPicture)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTrafficRecords")]
        public HttpResponseMessage GetTrafficRecords([FromBody] APITrafficSessionIdTrafficCardId Content)
        {
            try
            {
                var InstanceTraffic = new R2CoreParkingSystemTrafficManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficCardId = Content.TrafficCardId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTraffic.GetTrafficRecords(TrafficCardId, true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
