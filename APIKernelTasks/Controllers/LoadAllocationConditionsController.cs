using APICommon.Models;
using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.LoggingManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.Logging;
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

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadAllocationConditionsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IDateTimeService _DateTimeService;
        private ILogger _loggerService;
        private Networking _Networking;

        public LoadAllocationConditionsController()
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
        [Route("api/GetAllLoadAllocationConditions")]
        public HttpResponseMessage GetAllLoadAllocationConditions([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurations.GetAllLoadAllocationConditions(true), Encoding.UTF8, "application/json");
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
        [Route("api/LoadAllocationConditionRegistering")]
        public HttpResponseMessage LoadAllocationConditionRegistering([FromBody] APIKernelTasksSessionIdRawLoadAllocationCondition Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawLoadAllocationCondition = Content.RawLoadAllocationCondition;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadAllocationConditionRegistering(RawLoadAllocationCondition);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoadAllocationConditionRegistering, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawLoadAllocationCondition.AnnouncementSGId) + ":" + RawLoadAllocationCondition.AnnouncementSGId, MessageDetail2 = nameof(RawLoadAllocationCondition.SequentialTurnId) + ":" + RawLoadAllocationCondition.SequentialTurnId + " " + nameof(RawLoadAllocationCondition.TruckNativenessTypeId) + ":" + RawLoadAllocationCondition.TruckNativenessTypeId, MessageDetail3 = nameof(RawLoadAllocationCondition.LoadStatusId) + ":" + RawLoadAllocationCondition.LoadStatusId + " " + nameof(RawLoadAllocationCondition.RequesterId) + ":" + RawLoadAllocationCondition.RequesterId + " " + nameof(RawLoadAllocationCondition.TurnStatusId) + ":" + RawLoadAllocationCondition.TurnStatusId, UserId = User.UserId });

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
        [Route("api/LoadAllocationConditionEditing")]
        public HttpResponseMessage LoadAllocationConditionEditing([FromBody] APIKernelTasksSessionIdRawLoadAllocationCondition Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawLoadAllocationCondition = Content.RawLoadAllocationCondition;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadAllocationConditionEditing(RawLoadAllocationCondition);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoadAllocationConditionEditing, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(RawLoadAllocationCondition.LoadAllocationConditionId) + ":" + RawLoadAllocationCondition.LoadAllocationConditionId+" "+ nameof(RawLoadAllocationCondition.AnnouncementSGId) + ":" + RawLoadAllocationCondition.AnnouncementSGId, MessageDetail2= nameof(RawLoadAllocationCondition.SequentialTurnId ) + ":" + RawLoadAllocationCondition.SequentialTurnId+" "+ nameof(RawLoadAllocationCondition.TruckNativenessTypeId) + ":" + RawLoadAllocationCondition.TruckNativenessTypeId, MessageDetail3= nameof(RawLoadAllocationCondition.LoadStatusId ) + ":" + RawLoadAllocationCondition.LoadStatusId+" "+ nameof(RawLoadAllocationCondition.RequesterId ) + ":" + RawLoadAllocationCondition.RequesterId+" "+ nameof(RawLoadAllocationCondition.TurnStatusId ) + ":" + RawLoadAllocationCondition.TurnStatusId, UserId = User.UserId });

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
        [Route("api/LoadAllocationConditionDeleting")]
        public HttpResponseMessage LoadAllocationConditionDeleting([FromBody] APIKernelTasksSessionIdLoadAllocationConditionId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CorePredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadAllocationConditionId = Content.LoadAllocationConditionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadAllocationConditionDeleting(LoadAllocationConditionId);

                _loggerService.RegisterInfLog(new R2CoreRawLog { LogTypeId = R2CoreTransportationAndLoadNotificationLogTypes.LoadAllocationConditionDeleting, Description = _Networking.GetClientIpAddress(HttpContext.Current), MessageDetail1 = nameof(LoadAllocationConditionId) + ":" + LoadAllocationConditionId, UserId = User.UserId });

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

    }
}
