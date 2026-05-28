using APITransportation.Models;
using APITransportation.Models.Tariff;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.Logging;
using R2CoreTransportationAndLoadNotification.TransportTariffs;
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
using System.Web.Services.Protocols;

namespace APITransportation.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TariffsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public TariffsController()
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
        [Route("api/GetTariffs")]
        public HttpResponseMessage GetTariffs([FromBody] APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityIdGoodId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                Int64 LoaderTypeId = Content.LoaderTypeId == 0 ? -1 : Content.LoaderTypeId;
                Int64 SourceCityId = Content.SourceCityId == 0 ? -1 : Content.SourceCityId;
                Int64 TargetCityId = Content.TargetCityId == 0 ? -1 : Content.TargetCityId;
                Int64 GoodId = Content.GoodId == 0 ? -1 : Content.GoodId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTransportTariffs.GetTariffs(LoaderTypeId, GoodId, SourceCityId, TargetCityId, true), Encoding.UTF8, "application/json");
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
        [Route("api/TariffRegistering")]
        public HttpResponseMessage TariffRegistering([FromBody] APITransportationSessionIdTariff Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariff = Content.Tariff;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffRegistering(Tariff);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TariffRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Tariff.SourceCityId) + ":" + Tariff.SourceCityId+" "+ nameof(Tariff.TargetCityId ) + ":" + Tariff.TargetCityId , MessageDetail2 = nameof(Tariff.GoodId ) + ":" + Tariff.GoodId  + " " + nameof(Tariff.LoaderTypeId ) + ":" + Tariff.LoaderTypeId , MessageDetail3= nameof(Tariff.Tariff ) + ":" + Tariff.Tariff , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TariffsRegisteringWithAddPercentage")]
        public HttpResponseMessage TariffsRegisteringWithAddPercentage([FromBody] APITransportationSessionIdTariffsWithAddPercentage Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;
                var AddPercentage = Content.AddPercentage;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsRegistering(Tariffs, AddPercentage);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TariffsRegisteringWithAddPercentage, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Tariffs) + ":" + Tariffs.Count,  MessageDetail2= nameof(AddPercentage) + ":" + AddPercentage , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TariffsDeactivate")]
        public HttpResponseMessage TariffsDeactivate([FromBody] APITransportationSessionIdTariffs Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsDeactivate(Tariffs);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TariffsDeactivate, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Tariffs) + ":" + Tariffs.Count  , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TariffDeleting")]
        public HttpResponseMessage TariffDeleting([FromBody] APITransportationSessionIdTariff Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariff = Content.Tariff;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffDeleting(Tariff);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TariffDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Tariff.SourceCityId) + ":" + Tariff.SourceCityId + " " + nameof(Tariff.TargetCityId) + ":" + Tariff.TargetCityId, MessageDetail2 = nameof(Tariff.GoodId) + ":" + Tariff.GoodId + " " + nameof(Tariff.LoaderTypeId) + ":" + Tariff.LoaderTypeId, UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
        [Route("api/TariffEditing")]
        public HttpResponseMessage TariffEditing([FromBody] APITransportationSessionIdTariff Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariff = Content.Tariff;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffEditing(Tariff);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.TariffEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(Tariff.SourceCityId) + ":" + Tariff.SourceCityId+" "+ nameof(Tariff.TargetCityId ) + ":" + Tariff.TargetCityId , MessageDetail2= nameof(Tariff.GoodId ) + ":" + Tariff.GoodId +" "+ nameof(Tariff.LoaderTypeId ) + ":" + Tariff.LoaderTypeId , UserId = User.UserId });

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetPredefinedMessage (R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
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
