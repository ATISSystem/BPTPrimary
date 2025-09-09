using APICommon.Models;
using APITransportation.LoadCapacitor.Models.LoadCapacitor;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SecurityAlgorithmsManagement.AESAlgorithms;
using R2Core.SessionManagement;
using R2Core.SoftwareUserManagement;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.Exceptions;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoad;
using R2CoreTransportationAndLoadNotification.LoadCapacitor.LoadCapacitorLoadManipulation;
using R2CoreTransportationAndLoadNotification.RequesterManagement;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters;
using R2CoreTransportationAndLoadNotification.TransportTariffsParameters.Exceptions;
using R2CoreTransportationAndLoadNotification.TruckDrivers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

namespace APITransportation.LoadCapacitor.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadCapacitorController : ApiController
    {

        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();

        [HttpPost]
        [Route("api/GetLoadsforTruckDriver")]
        public HttpResponseMessage GetLoadsforTruckDriver([FromBody] APITransportationLoadCapacitorSessionIdAnnouncementSGIdLoadStatusId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementSGId = Content.AnnouncementSGId;
                var LoadStatusId = Content.LoadStatusId;
                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);
                var Loads = InstanceLoad.GetLoadsforTruckDriver(R2CoreTransportationAndLoadNotificationRequesters.LoadCapacitorController_GetLoadsforTruckDriver, User, AnnouncementSGId, LoadStatusId);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(Loads, Encoding.UTF8, "application/json");
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
        [Route("api/GetLoadStatusesForSoftwareUserType")]
        public HttpResponseMessage GetLoadStatusesForSoftwareUserType([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoad.GetLoadStatusesForSoftwareUserType(User), Encoding.UTF8, "application/json");
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
        [Route("api/GetLoadsforTransportCompanies")]
        public HttpResponseMessage GetLoadsforTransportCompanies([FromBody] APITransportationLoadCapacitorSessionIdPlus8Parameters Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementGroupId = Content.AnnouncementGroupId;
                var AnnouncementSubGroupId = Content.AnnouncementSubGroupId;
                var Inventory = Content.Inventory;
                var ShamsiDate = Content.ShamsiDate;
                var LoadStatusId = Content.LoadStatusId;
                var LoadSourceCityId = Content.LoadSourceCityId;
                var LoadTargetCityId = Content.LoadTargetCityId;

                if (AnnouncementGroupId == 0) { AnnouncementGroupId = Int64.MinValue; }
                if (AnnouncementSubGroupId == 0) { AnnouncementSubGroupId = Int64.MinValue; }
                if (LoadStatusId == 0) { LoadStatusId = Int64.MinValue; }
                if (LoadSourceCityId == 0) { LoadSourceCityId = Int64.MinValue; }
                if (LoadTargetCityId == 0) { LoadTargetCityId = Int64.MinValue; }

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoad.GetLoadsforTransportCompanies(User, AnnouncementGroupId, AnnouncementSubGroupId, Inventory, ShamsiDate, LoadStatusId, LoadSourceCityId, LoadTargetCityId), Encoding.UTF8, "application/json");
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
        [Route("api/GetLoadsforFactoriesAndProductionCenters")]
        public HttpResponseMessage GetLoadsforFactoriesAndProductionCenters([FromBody] APITransportationLoadCapacitorSessionIdPlus8Parameters Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementGroupId = Content.AnnouncementGroupId;
                var AnnouncementSubGroupId = Content.AnnouncementSubGroupId;
                var TransportCompanyId = Content.TransportCompanyId;
                var Inventory = Content.Inventory;
                var ShamsiDate = Content.ShamsiDate;
                var LoadStatusId = Content.LoadStatusId;
                var LoadSourceCityId = Content.LoadSourceCityId;
                var LoadTargetCityId = Content.LoadTargetCityId;

                if (AnnouncementGroupId == 0) { AnnouncementGroupId = Int64.MinValue; }
                if (AnnouncementSubGroupId == 0) { AnnouncementSubGroupId = Int64.MinValue; }
                if (TransportCompanyId == 0) { TransportCompanyId = Int64.MinValue; }
                if (LoadStatusId == 0) { LoadStatusId = Int64.MinValue; }
                if (LoadSourceCityId == 0) { LoadSourceCityId = Int64.MinValue; }
                if (LoadTargetCityId == 0) { LoadTargetCityId = Int64.MinValue; }

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoad.GetLoadsforFactoriesAndProductionCenters(User, TransportCompanyId, AnnouncementGroupId, AnnouncementSubGroupId, Inventory, ShamsiDate, LoadStatusId, LoadSourceCityId, LoadTargetCityId), Encoding.UTF8, "application/json");
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
        [Route("api/GetLoadsforAdministrator")]
        public HttpResponseMessage GetLoadsforAdministrator([FromBody] APITransportationLoadCapacitorSessionIdPlus8Parameters Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var AnnouncementGroupId = Content.AnnouncementGroupId;
                var AnnouncementSubGroupId = Content.AnnouncementSubGroupId;
                var TransportCompanyId = Content.TransportCompanyId;
                var Inventory = Content.Inventory;
                var ShamsiDate = Content.ShamsiDate;
                var LoadStatusId = Content.LoadStatusId;
                var LoadSourceCityId = Content.LoadSourceCityId;
                var LoadTargetCityId = Content.LoadTargetCityId;

                if (AnnouncementGroupId == 0) { AnnouncementGroupId = Int64.MinValue; }
                if (AnnouncementSubGroupId == 0) { AnnouncementSubGroupId = Int64.MinValue; }
                if (TransportCompanyId == 0) { TransportCompanyId = Int64.MinValue; }
                if (LoadStatusId == 0) { LoadStatusId = Int64.MinValue; }
                if (LoadSourceCityId == 0) { LoadSourceCityId = Int64.MinValue; }
                if (LoadTargetCityId == 0) { LoadTargetCityId = Int64.MinValue; }

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceLoad.GetLoadsforaAdministrator(AnnouncementGroupId, AnnouncementSubGroupId, TransportCompanyId, Inventory, ShamsiDate, LoadStatusId, LoadSourceCityId, LoadTargetCityId), Encoding.UTF8, "application/json");
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
        [Route("api/GetLoad")]
        public HttpResponseMessage GetLoad([FromBody] APITransportationLoadCapacitorSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoad = new R2CoreTransportationAndLoadNotificationLoadManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstanceLoad.GetLoad(LoadId, true)), Encoding.UTF8, "application/json");
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
        [Route("api/LoadRegistering")]
        public HttpResponseMessage LoadRegistering([FromBody] APITransportationLoadCapacitorSessionIdLoad Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var Load = Content.Load;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                var newLoadId = InstanceLoadManipulation.LoadRegistering(Load, User);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(new { newLoadId = newLoadId }), Encoding.UTF8, "application/json");
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
        [Route("api/LoadEditing")]
        public HttpResponseMessage LoadEditing([FromBody] APITransportationLoadCapacitorSessionIdLoad Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var Load = Content.Load;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                InstanceLoadManipulation.LoadEditing(Load, User);

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
        [Route("api/LoadDeleting")]
        public HttpResponseMessage LoadDeleting([FromBody] APITransportationLoadCapacitorSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                InstanceLoadManipulation.LoadDeleting(LoadId, User);

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
        [Route("api/LoadCancelling")]
        public HttpResponseMessage LoadCancelling([FromBody] APITransportationLoadCapacitorSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                InstanceLoadManipulation.LoadCancelling(LoadId, User);

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
        [Route("api/LoadFreeLining")]
        public HttpResponseMessage LoadFreeLining([FromBody] APITransportationLoadCapacitorSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                InstanceLoadManipulation.LoadFreeLining(LoadId, User);

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
        [Route("api/LoadSedimenting")]
        public HttpResponseMessage LoadSedimenting([FromBody] APITransportationLoadCapacitorSessionIdLoadId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var LoadId = Content.LoadId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceLoadManipulation = new R2CoreTransportationAndLoadNotificationLoadManipulationManager(_DateTimeService);
                InstanceLoadManipulation.LoadSedimenting(LoadId, User);

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



















    }

}
