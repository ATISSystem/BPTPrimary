using APICommon.Models;
using APITraffic.Models;
using Newtonsoft.Json;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.TrafficCosts;
using R2CoreParkingSystem.TrafficGates;
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
    public class TrafficGatesController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;

        public TrafficGatesController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetTrafficGates")]
        public HttpResponseMessage GetTrafficGates([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceTrafficGates = new R2CoreParkingSystemTrafficGatesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTrafficGates.GetTrafficGatesJSON(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/RegisteringTrafficGate")]
        public HttpResponseMessage RegisteringTrafficGate([FromBody] APITrafficSessionIdRawTrafficGate Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficGates = new R2CoreParkingSystemTrafficGatesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTrafficGate = Content.RawTrafficGate;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficGates.RegisteringTrafficGate(RawTrafficGate);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/ActivateTrafficGate")]
        public HttpResponseMessage ActivateTrafficGate([FromBody] APITrafficSessionIdTrafficGateId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficGates = new R2CoreParkingSystemTrafficGatesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficGateId = Content.TrafficGateId;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficGates.ActivateTrafficGate(TrafficGateId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/UnActivateTrafficGate")]
        public HttpResponseMessage UnActivateTrafficGate([FromBody] APITrafficSessionIdTrafficGateId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficGates = new R2CoreParkingSystemTrafficGatesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficGateId = Content.TrafficGateId;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficGates.UnActivateTrafficGate(TrafficGateId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTrafficGate")]
        public HttpResponseMessage GetTrafficGate([FromBody] APITrafficSessionIdTrafficGateId Content)
        {
            try
            {
                var InstanceTrafficGates = new R2CoreParkingSystemTrafficGatesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficGateId = Content.TrafficGateId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTrafficGates.GetTrafficGate(TrafficGateId, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }


    }
}
