using APICommon;
using APICommon.Models;
using APITransportation.Models.Truck;
using APITransportation.Models.Turn;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreParkingSystem.Cars;
using R2CoreParkingSystem.TrafficCardsManagement;
using R2CoreTransportationAndLoadNotification.Announcements;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.Trucks;
using R2CoreTransportationAndLoadNotification.TrucksNativeness;
using R2CoreTransportationAndLoadNotification.Turns;
using R2CoreTransportationAndLoadNotification.Turns.TurnAccounting;
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
        [Route("api/GetTop10TruckTurns")]
        public HttpResponseMessage GetTop10TruckTurns()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTruckId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTurns.GetTop10TruckTurns(TruckId, true), Encoding.UTF8, "application/json");
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
        [Route("api/GetTurnAccounting")]
        public HttpResponseMessage GetTurnAccounting()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TurnId = Content.TurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnAccounting = new R2CoreTransportationAndLoadNotificationTurnAccountingManager(new R2DateTimeService());

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTurnAccounting.GetTurnAccounting(TurnId, true), Encoding.UTF8, "application/json");
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
        [Route("api/TurnCancellation")]
        public HttpResponseMessage TurnCancellation()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TurnId = Content.TurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());
                InstanceTurns.TurnCancellationByUser(TurnId,  UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
        [Route("api/TurnResuscitation")]
        public HttpResponseMessage TurnResuscitation()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTurnId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var TurnId = Content.TurnId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurns = new R2CoreTransportationAndLoadNotificationTurnsManager(new R2DateTimeService());
                InstanceTurns.TurnResuscitationByUser(TurnId,  UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
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
