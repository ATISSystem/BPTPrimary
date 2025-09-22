using APICommon.Models;
using APITransportation.Models.Turn;
using Newtonsoft.Json;
using PayanehClassLibrary.TurnRegisterRequest;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreParkingSystem.MoneyWalletManagement;
using R2CoreParkingSystem.MoneyWalletManagement.Exceptions;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.TurnRegisterRequest;
using R2CoreTransportationAndLoadNotification.Turns;
using R2CoreTransportationAndLoadNotification.Turns.Exceptions;
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
    public class TurnRegisterRequestController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private R2DateTimeService _DateTimeService;

        public TurnRegisterRequestController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/RealTimeTurnRegisterRequest")]
        public HttpResponseMessage RealTimeTurnRegisterRequest([FromBody] APITransportationSessionIdTruckIdSequentialTurnId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var SequentialTurnId = Content.SequentialTurnId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnRegisterRequest = new PayanehClassLibraryTurnRegisterRequestManager(_DateTimeService);
                Int64 TurnId = 0;
                InstanceTurnRegisterRequest.RealTimeTurnRegisterRequest(SequentialTurnId, TruckId, ref TurnId, R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_RealTimeTurnRegisterRequest, TurnType.Permanent, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (MoneyWalletNotExistException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TurnCostNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/EmergencyTurnRegisterRequest")]
        public HttpResponseMessage EmergencyTurnRegisterRequest([FromBody] APITransportationSessionIdTruckIdSequentialTurnIdDescription Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var SequentialTurnId = Content.SequentialTurnId;
                var Description = Content.Description;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnRegisterRequest = new PayanehClassLibraryTurnRegisterRequestManager(_DateTimeService);
                Int64 TurnId = 0;
                InstanceTurnRegisterRequest.EmergencyTurnRegisterRequest(SequentialTurnId, TruckId, ref TurnId, Description, R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_EmergencyTurnRegisterRequest, TurnType.Permanent, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (MoneyWalletNotExistException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TurnCostNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/ResuscitationReserveTurn")]
        public HttpResponseMessage ResuscitationReserveTurn([FromBody] APITransportationSessionIdTruckIdSequentialTurnIdDateTime Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var SequentialTurnId = Content.SequentialTurnId;
                var ShamsiDate = Content.ShamsiDate;
                var Time = Content.Time;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnRegisterRequestTransportation = new R2CoreTransportationAndLoadNotificationTurnRegisterRequestManager(_DateTimeService);
                var InstanceTurnRegisterRequestPayaneh = new PayanehClassLibraryTurnRegisterRequestManager(_DateTimeService);
                var TRR = InstanceTurnRegisterRequestTransportation.GetTurnRegisteringRequestWithReservedDateTime(new R2CoreDateAndTime { DateTimeMilladi = DateTime.Now, ShamsiDate = ShamsiDate, Time = Time }, true);
                Int64 TurnId = 0;
                InstanceTurnRegisterRequestPayaneh.ResuscitationReserveTurn(SequentialTurnId, TRR.TRRId, TruckId, R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_ResuscitationReserveTurn, TurnType.Permanent, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (MoneyWalletNotExistException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TurnCostNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
        [Route("api/ReserveTurnRegisterRequest")]
        public HttpResponseMessage ReserveTurnRegisterRequest([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTurnRegisterRequest = new PayanehClassLibraryTurnRegisterRequestManager(_DateTimeService);
                InstanceTurnRegisterRequest.ReserveTurnRegisterRequest(R2CoreTransportationAndLoadNotificationRequesters.TurnRegisterRequestController_ReserveTurnRegisterRequest, TurnType.Permanent, User.UserId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json"); return response;
            }
            catch (MoneyWalletNotExistException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TurnCostNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
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
