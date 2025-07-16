using APICommon.Models;
using APITransportation.Models;
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
using R2CoreParkingSystem.Drivers;
using R2CoreParkingSystem.TrafficCardsManagement;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using R2CoreTransportationAndLoadNotification.Trucks;
using R2CoreTransportationAndLoadNotification.Trucks.Exceptions;
using R2CoreTransportationAndLoadNotification.TrucksNativeness;
using R2CoreTransportationAndLoadNotification.Turns;
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
    public class TruckController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();

        [HttpPost]
        [Route("api/GetTruckfromRMTO")]
        public HttpResponseMessage GetTruckfromRMTO([FromBody] APITransportationSessionIdSmartCardNo Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var SmartCardNo = Content.SmartCardNo;

                var User = InstanceSession.ConfirmSession(SessionId);

                PayanehWebService.PayanehWebService _WS = new PayanehWebService.PayanehWebService();
                var Truck = _WS.WebMethodGetTruckBySmartCarNofromRMTO(SmartCardNo, _WS.WebMethodLogin(InstanceSoftwareUsers.GetNSSSystemUser().UserShenaseh, InstanceSoftwareUsers.GetNSSSystemUser().UserPassword));

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(Truck), Encoding.UTF8, "application/json");
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
        [Route("api/GetTruckfromWebSite")]
        public HttpResponseMessage GetTruckfromWebSite([FromBody] APITransportationSessionIdSmartCardNo Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var SmartCardNo = Content.SmartCardNo;

                var User = InstanceSession.ConfirmSession(SessionId);

                var Truck = InstanceTrucks.GetTruckBySmartCardNo(SmartCardNo);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(Truck), Encoding.UTF8, "application/json");
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
        [Route("api/GetTruckBySoftwareUser")]
        public HttpResponseMessage GetTruckBySoftwareUser([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService());
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var Truck = InstanceTrucks.GetTruckBySoftwareUser(User.UserId, true );

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(Truck), Encoding.UTF8, "application/json");
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
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckNativeness = new R2CoreTransportationAndLoadNotificationsTruckNativenessManager();
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
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var InstanceTruckDrivers = new R2CoreTransportationAndLoadNotificationInstanceTruckDriversManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var TruckNativenessExpireDate = Content.TruckNativenessExpireDate;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTruckNativeness = new R2CoreTransportationAndLoadNotificationsTruckNativenessManager();
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

        [HttpPost]
        [Route("api/GetComposedTruckInf")]
        public HttpResponseMessage GetComposedTruckInf([FromBody] APITransportationSessionIdTruckId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService());
                var ComposedTruckInf = InstanceTrucks.GetComposedTruckInf(TruckId, true, true);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(ComposedTruckInf), Encoding.UTF8, "application/json");
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
        [Route("api/SetComposedTruckInf")]
        public HttpResponseMessage SetComposedTruckInf([FromBody] APITransportationSessionIdTruckIdTruckDriverIdTurnIdMoneyWalletId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;
                var TruckDriverId = Content.TruckDriverId;
                var TurnId = Content.TurnId;
                var MoneyWalletId = Content.MoneyWalletId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService());
                InstanceTrucks.SetComposedTruckInf(TruckId, TruckDriverId, MoneyWalletId, TurnId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Message = InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent }), Encoding.UTF8, "application/json");
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

        [HttpPost]
        [Route("api/GetComposedTruckInfForTurnIssue")]
        public HttpResponseMessage GetComposedTruckInfForTurnIssue([FromBody] APITransportationSessionIdTruckId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TruckId = Content.TruckId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTrucks = new R2CoreTransportationAndLoadNotificationTrucksManager(new R2DateTimeService());
                var ComposedTruckInfForTurnIssue = InstanceTrucks.GetComposedTruckInf(TruckId, false, true);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(ComposedTruckInfForTurnIssue), Encoding.UTF8, "application/json");
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
