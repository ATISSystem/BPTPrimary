using APICommon.Models;
using APITraffic.Models;
using Newtonsoft.Json;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.Logging;
using R2CoreParkingSystem.TrafficCosts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITraffic.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TrafficCostsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public TrafficCostsController()
        {
            try
            {
                _DateTimeService = new R2DateTimeService();
                _loggerService = new R2Core.LoggingManagement.R2CorenLogService();
                _Networking = new Networking();
            }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetTrafficCosts")]
        public HttpResponseMessage GetTrafficCosts([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceTrafficCosts = new R2CoreParkingSystemTrafficCostsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTrafficCosts.GetTrafficCostsJSON(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/RegisteringTrafficCost")]
        public HttpResponseMessage RegisteringTrafficCost([FromBody] APITrafficSessionIdRawTrafficCost Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficCosts = new R2CoreParkingSystemTrafficCostsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTrafficCost = Content.RawTrafficCost;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficCosts.RegisteringTrafficCost(RawTrafficCost);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreParkingSystemLogTypes.RegisteringTrafficCost, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawTrafficCost.TrafficCardTypeId) + ":" + RawTrafficCost.TrafficCardTypeId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
