using APITransportation.Models;
using APITransportation.Models.Tarrif;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
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

        [HttpPost]
        [Route("api/GetTariffs")]
        public HttpResponseMessage GetTariffs()
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdLoaderTypeIdSourceCityIdTargetCityIdGoodId>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                Int64 LoaderTypeId = Content.LoaderTypeId == 0 ? -1 : Content.LoaderTypeId;
                Int64 SourceCityId = Content.SourceCityId == 0 ? -1 : Content.SourceCityId;
                Int64 TargetCityId = Content.TargetCityId == 0 ? -1 : Content.TargetCityId;
                Int64 GoodId = Content.GoodId == 0 ? -1 : Content.GoodId;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();

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
        public HttpResponseMessage TariffsRegistering()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTariffs>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();
                InstanceTransportTariffs.TariffsRegistering(Tariffs);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent, Encoding.UTF8, "application/json");
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
        public HttpResponseMessage TariffsRegisteringWithAddPercentage()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTariffsWithAddPercentage>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;
                var AddPercentage = Content.AddPercentage;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();
                InstanceTransportTariffs.TariffsRegistering(Tariffs, AddPercentage);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent, Encoding.UTF8, "application/json");
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
        public HttpResponseMessage TariffsDeactivate()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTariffs>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();
                InstanceTransportTariffs.TariffsDeactivate(Tariffs);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent, Encoding.UTF8, "application/json");
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
        public HttpResponseMessage TariffsDeleting()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTariffs>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();
                InstanceTransportTariffs.TariffsDeleting(Tariffs);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent, Encoding.UTF8, "application/json");
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
        public HttpResponseMessage TariffsEditing()
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var InstanceSession = new R2CoreSessionManager();
                var InstanceSoftwareUsers = new R2CoreInstanseSoftwareUsersManager(new R2DateTimeService());
                var Content = JsonConvert.DeserializeObject<APITransportationSessionIdTariffs>(Request.Content.ReadAsStringAsync().Result);
                var SessionId = Content.SessionId;
                var Tariffs = Content.Tariffs;

                var UserId = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffs = new R2CoreTransportationAndLoadNotificationInstanceTransportTariffsManager();
                InstanceTransportTariffs.TariffsEditing(Tariffs);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent, Encoding.UTF8, "application/json");
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
