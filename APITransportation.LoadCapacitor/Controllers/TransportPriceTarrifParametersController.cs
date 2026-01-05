using APICommon.Models;
using APITransportation.LoadCapacitor.Models.LoadCapacitor;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters.Exceptions;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITransportation.LoadCapacitor.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class TransportPriceTarrifParametersController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public TransportPriceTarrifParametersController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetListofTransportTariffsParams")]
        public HttpResponseMessage GetListofTransportTariffsParams([FromBody] APITransportationLoadCapacitorSessionIdTPTParams Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TPTParams = Content.TPTParams;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTransportTariffsParameters.GetListofTransportTariffsParams(TPTParams)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetListofTransportTariffsParamsByAnnouncementSGId")]
        public HttpResponseMessage GetListofTransportTariffsParamsByAnnouncementSGId([FromBody] APITransportationLoadCapacitorSessionIdAnnouncementSGId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementSGId = Convert.ToInt64(Content.AnnouncementSGId);

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTransportTariffsParameters.GetListofTransportTariffsParamsByAnnouncementSGId(AnnouncementSGId)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch(TransportPriceTariffParameterDetailsforAHSGNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTPTParams")]
        public HttpResponseMessage GetTPTParams([FromBody] APITransportationLoadCapacitorSessionIdListofTransportTariffsParams Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var ListofTransportTariffsParams = Content.ListofTransportTariffsParams;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { TPTParams = InstanceTransportTariffsParameters.GetTPTParams(ListofTransportTariffsParams) }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetAllTPTParams")]
        public HttpResponseMessage GetAllTransportPriceTarrifParameters([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTransportTariffsParameters.GetTransportPriceTarrifParametersJSON(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TransportPriceTarrifParameterRegistering")]
        public HttpResponseMessage TransportPriceTarrifParameterRegistering([FromBody] APITransportationLoadCapacitorSessionIdRawTPTParam Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTPTParam = Content.RawTPTParam;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
                InstanceTransportTariffsParameters.TransportPriceTarrifParameterRegistering(RawTPTParam);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TransportPriceTarrifParameterEditing")]
        public HttpResponseMessage TransportPriceTarrifParameterEditing([FromBody] APITransportationLoadCapacitorSessionIdRawTPTParam Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTPTParam = Content.RawTPTParam;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
                InstanceTransportTariffsParameters.TransportPriceTarrifParameterEditing(RawTPTParam);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TransportPriceTarrifParameterDeleting")]
        public HttpResponseMessage TransportPriceTarrifParameterDeleting([FromBody] APITransportationLoadCapacitorSessionIdTPTPId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var TPTPId = Content.TPTPId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
                InstanceTransportTariffsParameters.TransportPriceTarrifParameterDeleting(TPTPId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetAllTPTParamsDetails")]
        public HttpResponseMessage GetAllTPTParamsDetails([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceTransportTariffsParameters.GetTransportPriceTarrifParametersDetailsJSON(true), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TransportPriceTarrifParameterDetailRegistering")]
        public HttpResponseMessage TransportPriceTarrifParameterDetailRegistering([FromBody] APITransportationLoadCapacitorSessionIdRawTPTParamDetail Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTPTParamDetail = Content.RawTPTParamDetail;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
                InstanceTransportTariffsParameters.TransportPriceTarrifParameterDetailRegistering(RawTPTParamDetail);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/TransportPriceTarrifParameterDetailEditing")]
        public HttpResponseMessage TransportPriceTarrifParameterDetailEditing([FromBody] APITransportationLoadCapacitorSessionIdRawTPTParamDetail Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawTPTParamDetail = Content.RawTPTParamDetail;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);
                InstanceTransportTariffsParameters.TransportPriceTarrifParameterDetailEditing(RawTPTParamDetail);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.RegisteringInformationSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetTPTParameters")]
        public HttpResponseMessage GetAllTPTParameters([FromBody] APICommonSessionIdSearchString Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var SearchString = Content.SearchString;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceTransportTariffsParameters = new R2CoreTransportationAndLoadNotificationTransportTariffsParametersManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceTransportTariffsParameters.GetTPTParams_SearchIntroCharacters(SearchString, true)), Encoding.UTF8, "application/json");
                return response;
            }
            catch (RedisException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (TransportPriceTariffParameterDetailNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (LoadCapacitorLoadNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

    }
}
