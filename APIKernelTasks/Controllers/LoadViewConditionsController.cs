using APICommon.Models;
using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.DatabaseManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using R2CoreTransportationAndLoadNotification.ConfigurationsManagement;
using R2CoreTransportationAndLoadNotification.TrucksNativeness;
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

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class LoadViewConditionsController : ApiController
    {
        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService;

        public LoadViewConditionsController()
        {
            try { _DateTimeService = new R2DateTimeService(); }
            catch (FileNotExistException ex)
            { throw ex; }
            catch (Exception ex)
            { throw new Exception(MethodBase.GetCurrentMethod().ReflectedType.FullName + "." + MethodBase.GetCurrentMethod().Name + ex.Message); }
        }

        [HttpPost]
        [Route("api/GetAllLoadViewConditions")]
        public HttpResponseMessage GetAllLoadViewConditions([FromBody] APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurations.GetAllLoadViewConditions(true), Encoding.UTF8, "application/json");
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
        [Route("api/LoadViewConditionRegistering")]
        public HttpResponseMessage LoadViewConditionRegistering([FromBody] APIKernelTasksSessionIdRawLoadViewCondition Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawLoadViewCondition = Content.RawLoadViewCondition;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadViewConditionRegistering(RawLoadViewCondition);

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
        [Route("api/LoadViewConditionEditing")]
        public HttpResponseMessage LoadViewConditionEditing([FromBody] APIKernelTasksSessionIdRawLoadViewCondition Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var RawLoadViewCondition = Content.RawLoadViewCondition;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadViewConditionEditing(RawLoadViewCondition);

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
        [Route("api/LoadViewConditionDeleting")]
        public HttpResponseMessage LoadViewConditionDeleting([FromBody] APIKernelTasksSessionIdLoadViewConditionId Content)
        {
            try
            {
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager(_DateTimeService);

                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;
                var LoadViewConditionId = Content.LoadViewConditionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreTransportationAndLoadNotificationConfigurationsManager(_DateTimeService);
                InstanceConfigurations.LoadViewConditionDeleting(LoadViewConditionId);

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
