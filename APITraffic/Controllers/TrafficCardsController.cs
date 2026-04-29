
using APICommon.Models;
using APITraffic.Models;
using Newtonsoft.Json;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.Logging;
using R2CoreParkingSystem.TrafficCardsManagement;
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
    public class TrafficCardsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public TrafficCardsController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/RegisteringTrafficCard")]
        public HttpResponseMessage RegisteringTrafficCard([FromBody] APITrafficSessionIdTrafficCardNoTrafficCardTypeIdTrafficCardTempTypeId Content)
        {
            try
            {
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficCardNo = Content.TrafficCardNo;
                var TrafficCardTypeId= Content.TrafficCardTypeId;
                var TrafficCardTempTypeId = Content.TrafficCardTempTypeId;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficCards.RegisteringTrafficCard(TrafficCardNo, TrafficCardTypeId, TrafficCardTempTypeId,User.UserId );

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreParkingSystemLogTypes.RegisteringTrafficCard, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(TrafficCardNo) + ":" + TrafficCardNo, MessageDetail2 = nameof(TrafficCardTypeId) + ":" + TrafficCardTypeId, MessageDetail3= nameof(TrafficCardTempTypeId) + ":" + TrafficCardTempTypeId, UserId = User.UserId });

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
        [Route("api/GetTrafficCard")]
        public HttpResponseMessage GetTrafficCard([FromBody] APITrafficSessionIdTrafficCardNo Content)
        {
            try
            {
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficCardNo = Content.TrafficCardNo;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTrafficCards.GetTrafficCard(TrafficCardNo)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/EditingTrafficCard")]
        public HttpResponseMessage EditingTrafficCard([FromBody] APITrafficSessionIdRawTrafficCard Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTrafficCard = Content.RawTrafficCard;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficCards.EditingTrafficCard(RawTrafficCard);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreParkingSystemLogTypes.EditingTrafficCard, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawTrafficCard.TrafficCardId) + ":" + RawTrafficCard.TrafficCardId,MessageDetail2= nameof(RawTrafficCard.TrafficCardNo) + ":" + RawTrafficCard.TrafficCardNo, UserId = User.UserId });

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
        [Route("api/GetTrafficCardTypes")]
        public HttpResponseMessage GetTrafficCardTypes([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTrafficCards.GetTrafficCardTypesJSON(true ), Encoding.UTF8, "application/json");
                return response;
            }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTrafficCardTempTypes")]
        public HttpResponseMessage GetTrafficCardTempTypes([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTrafficCards.GetTrafficCardTempTypesJSON(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/RegisteringTrafficCardType")]
        public HttpResponseMessage RegisteringTrafficCardType([FromBody] APITrafficSessionIdTrafficCardTypeTitle Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TrafficCardTypeTitle = Content.TrafficCardTypeTitle;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficCards.RegisteringTrafficCardType(TrafficCardTypeTitle);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreParkingSystemLogTypes.RegisteringTrafficCardType, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(TrafficCardTypeTitle) + ":" + TrafficCardTypeTitle, UserId = User.UserId });

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
        [Route("api/EditingTrafficCardType")]
        public HttpResponseMessage EditingTrafficCardType([FromBody] APITrafficSessionIdRawTrafficCardType Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceTrafficCards = new R2CoreParkingSystemTrafficCardsManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTrafficCardType = Content.RawTrafficCardType;

                var User = InstanceSession.ConfirmSession(SessionId);

                InstanceTrafficCards.EditingTrafficCardType(RawTrafficCardType);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreParkingSystemLogTypes.EditingTrafficCardType, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawTrafficCardType.TrafficCardTypeId) + ":" + RawTrafficCardType.TrafficCardTypeId, UserId = User.UserId });

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
