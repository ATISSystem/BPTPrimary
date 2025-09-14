using APITransportation.Models;
using APITransportation.Models.Tariff;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.FactoriesAndProductionCentersManagement;
using R2CoreTransportationAndLoadNotification.TransportTariffs;
using System;
using System.Collections.Generic;
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
    public class TariffsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetTariffs")]
        public HttpResponseMessage GetTariffs([FromBody] APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityIdGoodId Content )
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
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
        [Route("api/TariffsRegistering")]
        public HttpResponseMessage TariffsRegistering([FromBody] APITransportationSessionIdTariffs Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsRegistering(Tariffs);
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
        [Route("api/TariffsRegisteringWithAddPercentage")]
        public HttpResponseMessage TariffsRegisteringWithAddPercentage([FromBody] APITransportationSessionIdTariffsWithAddPercentage Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;
                var AddPercentage = Content.AddPercentage;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsRegistering(Tariffs, AddPercentage);
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
        [Route("api/TariffsDeactivate")]
        public HttpResponseMessage TariffsDeactivate([FromBody] APITransportationSessionIdTariffs Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsDeactivate(Tariffs);
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

        [HttpPost]
        [Route("api/TariffsDeleting")]
        public HttpResponseMessage TariffsDeleting([FromBody] APITransportationSessionIdTariffs Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsDeleting(Tariffs);
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

        [HttpPost]
        [Route("api/TariffsEditing")]
        public HttpResponseMessage TariffsEditing([FromBody] APITransportationSessionIdTariffs Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(_DateTimeService);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationTransportTariffsManager(_DateTimeService);
                InstanceTransportTariffs.TariffsEditing(Tariffs);
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

    }
}
