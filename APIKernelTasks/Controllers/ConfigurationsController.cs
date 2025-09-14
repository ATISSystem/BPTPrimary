using APIKernelTasks.Models;
using Newtonsoft.Json;
using R2Core.ConfigurationManagement;
using R2Core.DatabaseManagement;
using R2Core.DateAndTimeManagement;
using R2Core.DateTimeProvider;
using R2Core.ExceptionManagement;
using R2Core.PredefinedMessagesManagement;
using R2Core.SessionManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace APIKernelTasks.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    public class ConfigurationsController : ApiController
    {

        private APICommon.APICommon _APICommon = new APICommon.APICommon();
        private IR2DateTimeService _DateTimeService = new R2DateTimeService();


        [HttpPost]
        [Route("api/GetConfigurations")]
        public HttpResponseMessage GetConfigurations([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurations.GetConfigurations(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetConfigurationOfMachines")]
        public HttpResponseMessage GetConfigurationOfMachines([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurations.GetConfigurationOfMachines(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetConfigurationOfAnnouncementGroups")]
        public HttpResponseMessage GetConfigurationOfAnnouncementGroups([FromBody] APICommon.Models.APICommonSessionId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var SessionId = Content.SessionId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(InstanceConfigurations.GetConfigurationOfAnnouncementGroups(), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SetConfigurations")]
        public HttpResponseMessage SetConfigurations([FromBody] APIKernelTasksSessionIdCIdCValue Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var CId = Content.CId;
                var CValue = Content.CValue;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();
                InstanceConfigurations.SetConfigurations(CId, CValue);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SetConfigurationOfMachines")]
        public HttpResponseMessage SetConfigurationOfMachines([FromBody] APIKernelTasksSessionIdCIdMIdCValue Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var CId = Content.CId;
                var MId = Content.MId;
                var CValue = Content.CValue;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();
                InstanceConfigurations.SetConfigurationOfMachines(CId, MId, CValue);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/SetConfigurationOfAnnouncementGroups")]
        public HttpResponseMessage SetConfigurationOfAnnouncementGroups([FromBody] APIKernelTasksSessionIdCIdAnnouncementIdCValue Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var CId = Content.CId;
                var AnnouncementId = Content.AnnouncementId;
                var CValue = Content.CValue;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();
                InstanceConfigurations.SetConfigurationOfAnnouncementGroups(CId, AnnouncementId, CValue);

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(JsonConvert.SerializeObject(InstancePredefinedMessages.GetNSS(R2CorePredefinedMessages.ProcessSuccessed).MsgContent), Encoding.UTF8, "application/json");
                return response;
            }
            catch (DataBaseException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (AnyNotFoundException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (SessionOverException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (Exception ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
        }

        [HttpPost]
        [Route("api/GetConfigurationHelper")]
        public HttpResponseMessage GetConfigurationHelper([FromBody] APIKernelTasksSessionIdCId Content)
        {
            try
            {
                var InstanceSession = new R2CoreSessionManager();
                var InstancePredefinedMessages = new R2CoreMClassPredefinedMessagesManager();
                var SessionId = Content.SessionId;
                var CId = Content.CId;

                var User = InstanceSession.ConfirmSession(SessionId);

                var InstanceConfigurations = new R2CoreConfigurationsManager();
                ;

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                //response.Content = new StringContent(JsonConvert.SerializeObject(new { Image = InstanceConfigurations.GetConfigurationHelper(CId, User) }), Encoding.UTF8, "application/json");
                response.Content = new StringContent(JsonConvert.SerializeObject(new { Image = 123 }), Encoding.UTF8, "application/json");
                return response;
            }
            catch (SoapException ex)
            { return _APICommon.CreateErrorContentMessage(ex); }
            catch (DataBaseException ex)
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
