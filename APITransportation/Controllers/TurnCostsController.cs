using APICommon.Models;
using APITransportation.Models.Truck;
using APITransportation.Models.TurnCost;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.TurnCosts;
using R2CoreTransportationAndLoadNotification.Turns;
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
    public class TurnCostsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;

        public TurnCostsController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetAllTurnCosts")]
        public HttpResponseMessage GetAllTurnCosts([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnCost = new R2CoreTransportationAndLoadNotificationTurnCostManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTurnCost.GetAllTurnCosts(true), Encoding.UTF8, "application/json");
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
        [Route("api/TurnCostRegistering")]
        public HttpResponseMessage TurnCostRegistering([FromBody] APITransportationSessionIdRawTurnCost Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTurnCost = Content.RawTurnCost;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnCost = new R2CoreTransportationAndLoadNotificationTurnCostManager(_DateTimeService);
                InstanceTurnCost.TurnCostRegistering(RawTurnCost);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TurnCostDeleting")]
        public HttpResponseMessage TurnCostDeleting([FromBody] APITransportationSessionIdSeqTurnId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SeqTurnId = Content.SeqTurnId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnCost = new R2CoreTransportationAndLoadNotificationTurnCostManager(_DateTimeService);
                InstanceTurnCost.TurnCostDeleting(SeqTurnId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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


    }
}
